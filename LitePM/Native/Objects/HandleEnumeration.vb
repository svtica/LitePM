
' Lite Process Monitor









'




'





' Thanks to ShareVB for the KernelMemory driver.
' http://www.vbfrance.com/codes/LISTER-HANDLES-FICHIERS-CLE-REGISTRES-OUVERTS-PROGRAMME-NT_39333.aspx

Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Api

Namespace Native.Objects

    Public Class HandleEnumeration


        ' Private constants


        ' IoCode for kernel function
        Private Const IOCTL_KERNELMEMORY_GETOBJECTNAME As Integer = &H80002004



        ' Private attributes


        ' Some mem allocation for buffer of handles
        Private memAllocPIDs As New Native.Memory.MemoryAlloc(&H100)

        ' Some other mem allocations 
        Private BufferObjType As New Native.Memory.MemoryAlloc(512)
        Private BufferObjName As New Native.Memory.MemoryAlloc(512)
        Private BufferObjBasic As New Native.Memory.MemoryAlloc(Marshal.SizeOf(GetType(NativeStructs.ObjectBasicInformation)))

        ' Dictionnary which contains ObjectTypeNumber <-> ObjectTypeName
        Private _dicoObjectTypeNumber As New Dictionary(Of Integer, String)

        ' Currently opened driver
        Private hProcess As IntPtr
        Private lastPID As Integer

        ' Use driver or not ?
        Private useDriver As Boolean

        ' List of handles
        Private m_Files() As handleInfos

        ' Number of handles
        Private m_cHandles As Integer

        ' Handle to the driver
        Private hDriver As IntPtr

        ' Driver control class
        Private driver As Native.Driver.DriverCtrl



        ' Public properties


        ' Handle count
        Public ReadOnly Property Count() As Integer
            Get
                Return m_cHandles
            End Get
        End Property




        ' Public functions


        ' Refresh list of handles
        ' One PIDs or a list of PIDs
        Public Sub Refresh(Optional ByVal oneProcessId As Integer = -1)
            ' oneProcessId = -1 <=> all processes
            CreateQueryHandlesBuffer(oneProcessId)
        End Sub
        Public Sub Refresh(ByVal PIDs() As Integer)
            CreateQueryHandlesBuffer(PIDs)
        End Sub


        ' Properties of a handle
        Public Function IsNotNull(ByVal dwIndex As Integer) As Boolean  ' Item is null ?
            Return m_Files(dwIndex) IsNot Nothing
        End Function
        Public Function GetHandleInfos(ByVal dwIndex As Integer) As handleInfos ' All infos
            Return m_Files(dwIndex)
        End Function
        Public Function GetObjectName(ByVal dwIndex As Integer) As String   ' Name
            Return m_Files(dwIndex).Name
        End Function
        Public Function GetNameInformation(ByVal dwIndex As Integer) As String ' Type 
            Return m_Files(dwIndex).Type
        End Function
        Public Function GetProcessName(ByVal dwIndex As Integer) As String  ' Proprietary process name
            Return GetProcessNameFromPID(m_Files(dwIndex).ProcessId)
        End Function
        Public Function GetProcessID(ByVal dwIndex As Integer) As Integer   ' Proprietary process ID
            Return m_Files(dwIndex).ProcessId
        End Function
        Public Function GetHandle(ByVal dwIndex As Integer) As IntPtr  ' Handle itself
            Return m_Files(dwIndex).Handle
        End Function
        Public Function GetObjectAddress(ByVal dwIndex As Integer) As IntPtr  ' Obj address
            Return m_Files(dwIndex).ObjectAddress
        End Function
        Public Function GetObjectCount(ByVal dwIndex As Integer) As Integer ' Obj count
            Return m_Files(dwIndex).ObjectCount
        End Function
        Public Function GetGrantedAccess(ByVal dwIndex As Integer) As Native.Security.StandardRights  ' Access to the object
            Return m_Files(dwIndex).GrantedAccess
        End Function
        Public Function GetAttributes(ByVal dwIndex As Integer) As UInteger ' Attributes
            Return m_Files(dwIndex).Attributes
        End Function
        Public Function GetHandleCount(ByVal dwIndex As Integer) As Integer ' Count
            Return m_Files(dwIndex).HandleCount
        End Function
        Public Function GetPointerCount(ByVal dwIndex As Integer) As UInteger ' Number of references to the pointer to this object
            Return m_Files(dwIndex).PointerCount
        End Function
        Public Function GetCreateTime(ByVal dwIndex As Integer) As Decimal ' Creation time of object
            Return m_Files(dwIndex).CreateTime
        End Function
        Public Function GetPagedPoolUsage(ByVal dwIndex As Integer) As Integer ' Paged pool usage
            Return m_Files(dwIndex).PagedPoolUsage
        End Function
        Public Function GetNonPagedPoolUsage(ByVal dwIndex As Integer) As Integer  ' Non-paged pool usage
            Return m_Files(dwIndex).NonPagedPoolUsage
        End Function

        ' Close this instance (close some handles and the kernel if necessary)
        Public Sub Close()
            Class_Terminate_Renamed()
        End Sub

        ' Constructor
        Public Sub New(ByVal useKDriver As Boolean)
            MyBase.New()
            useDriver = useKDriver

            ' If we use the driver, we install it
            If useDriver Then

                Dim ret As Boolean

                ' Instanciate driverCtrl class
                driver = New Native.Driver.DriverCtrl

                With driver

                    ' Configure the driver
                    .ServiceDisplayName = "KernelMemory"
                    .ServiceErrorType = NativeEnums.ServiceErrorControl.Normal
                    .ServiceFileName = My.Application.Info.DirectoryPath & "\KernelMemory.sys"
                    .ServiceName = "KernelMemory"
                    .ServiceStartType() = NativeEnums.ServiceStartType.DemandStart
                    .ServiceType = NativeEnums.ServiceType.KernelDriver

                    ' Register service
                    ret = .InstallService()

                    ' Start it
                    ret = .StartService()

                    ' Get a handle to the driver
                    hDriver = .OpenDriver
                End With
            End If

        End Sub



        ' Private functions


        ' Create a buffer containing handles
        Private Sub CreateQueryHandlesBuffer(Optional ByVal oneProcessId As Integer = -1)
            Dim Length As Integer
            Dim x As Integer
            Dim ret As Integer
            Dim Handle As NativeStructs.SystemHandleEntry

            Length = memAllocPIDs.Size
            ' While length is too small
            Do While NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, _
                                memAllocPIDs.Pointer, memAllocPIDs.Size, ret) = NativeConstants.STATUS_INFO_LENGTH_MISMATCH
                ' Resize buffer
                Length = Length * 2
                memAllocPIDs.ResizeNew(Length)
            Loop

            ' Get the number of handles 
            m_cHandles = memAllocPIDs.ReadStruct(Of NativeStructs.SystemHandleInformation).HandleCount

            Dim procIdOffsetInStruct As Integer = Native.Api.NativeStructs.SystemHandleInformation_ProcessIdOffset
            Dim structSize As Integer = Marshal.SizeOf(GetType(NativeStructs.SystemHandleEntry))
            Dim handlesOffset As Integer = NativeStructs.SystemHandleInformation.HandlesOffset

            ' Resize our array
            ReDim m_Files(m_cHandles - 1)
            For x = 0 To m_cHandles - 1
                ' Do not retrieve the whole struct cause it requires too much CPU
                ' Just retrieve the processId for now
                Dim pid As Integer = memAllocPIDs.ReadInt32(procIdOffsetInStruct + _
                                                                    handlesOffset + x * structSize)
                ' Only if handle belongs to specified process
                If oneProcessId = -1 OrElse oneProcessId = pid Then
                    ' Ok, now we get the whole struct !
                    ' &h4 offset because of HandleCount on 4 first bytes
                    Handle = memAllocPIDs.ReadStruct(Of NativeStructs.SystemHandleEntry)(handlesOffset, x)

                    m_Files(X) = RetrieveObject(Handle)
                End If
            Next


            ' Close handle to last used process
            CloseProcessForHandle()

        End Sub

        ' Create a buffer containing handles
        Private Sub CreateQueryHandlesBuffer(ByVal PIDs As Integer())

            If PIDs Is Nothing Then
                ReDim m_Files(-1)
                m_cHandles = 0
                Exit Sub
            End If

            Dim Length As Integer
            Dim X As Integer
            Dim ret As Integer
            Dim Handle As NativeStructs.SystemHandleEntry

            Length = memAllocPIDs.Size
            ' While length is too small
            Do While NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemHandleInformation, _
                                memAllocPIDs.Pointer, memAllocPIDs.Size, ret) = NativeConstants.STATUS_INFO_LENGTH_MISMATCH
                ' Resize buffer
                Length = Length * 2
                memAllocPIDs.ResizeNew(Length)
            Loop

            ' Get the number of handles
            m_cHandles = memAllocPIDs.ReadStruct(Of NativeStructs.SystemHandleInformation).HandleCount

            Dim procIdOffsetInStruct As Integer = Native.Api.NativeStructs.SystemHandleInformation_ProcessIdOffset
            Dim structSize As Integer = Marshal.SizeOf(GetType(NativeStructs.SystemHandleEntry))
            Dim handlesOffset As Integer = NativeStructs.SystemHandleInformation.HandlesOffset

            ' Resize our array
            ReDim m_Files(m_cHandles - 1)
            For X = 0 To m_cHandles - 1
                ' Do not retrieve the whole struct cause it requires too much CPU
                ' Just retrieve the processId for now
                Dim pid As Integer = memAllocPIDs.ReadInt32(procIdOffsetInStruct + _
                                                                    handlesOffset + X * structSize)
                ' Only if handle belongs to specified process
                For Each __pid As Integer In PIDs
                    If pid = __pid Then
                        ' Ok, now we get the whole struct !
                        ' &h4 offset because of HandleCount on 4 first bytes
                        Handle = memAllocPIDs.ReadStruct(Of NativeStructs.SystemHandleEntry)(handlesOffset, X)

                        m_Files(X) = RetrieveObject(Handle)
                    End If
                Next
            Next

            ' Close handle of last process
            CloseProcessForHandle()

        End Sub

        ' Open a handle to process ProcessId
        ' Used to duplicate handles of handles owned by the process
        Private Sub OpenProcessForHandle(ByVal ProcessID As Integer)
            If ProcessID <> lastPID Then
                NativeFunctions.CloseHandle(hProcess)
                hProcess = Native.Objects.Process.GetProcessHandleById(ProcessID, _
                                                            Security.ProcessAccess.DupHandle)
                lastPID = ProcessID
            End If
        End Sub

        ' Close handle to the process (PID = lastPID)
        Private Sub CloseProcessForHandle()
            NativeFunctions.CloseHandle(hProcess)
            hProcess = IntPtr.Zero
            lastPID = 0
        End Sub

        ' Create buffer with all informations about our handle
        Private Function RetrieveObject(ByRef Handle As _
                                        NativeStructs.SystemHandleEntry) As handleInfos
            Dim ret As Integer
            Dim hHandle As IntPtr
            Dim ObjBasic As NativeStructs.ObjectBasicInformation
            Dim ObjType As NativeStructs.ObjectTypeInformation
            Dim ObjName As NativeStructs.ObjectNameInformation
            Dim m_ObjectTypeName As String
            Dim m_ObjectName As String

            ' Create the instance of the structure we'll return
            Dim h As New handleInfos

            ' Open an handle to the process which owns our handle
            OpenProcessForHandle(Handle.ProcessId)

            ' Duplicate the handle in our process with same access
            NativeFunctions.DuplicateHandle(hProcess, New IntPtr(Handle.Handle), _
                                            NativeFunctions.GetCurrentProcess, _
                                            hHandle, 0, False, _
                                            NativeEnums.DuplicateOptions.SameAccess)

            ' If we failed... we failed.
            If hHandle.IsNull Then
                Return h
            End If

            ' Get Basic infos about object
            NativeFunctions.NtQueryObject(hHandle, _
                                NativeEnums.ObjectInformationClass.ObjectAttributes, _
                                BufferObjBasic.Pointer, Marshal.SizeOf(ObjBasic), ret)
            ObjBasic = BufferObjBasic.ReadStruct(Of NativeStructs.ObjectBasicInformation)(0)

            ' Get Type infos about object
            ' We try to retieve this from the dictionary. If it's not possible, we
            ' have to get it using NtQueryObject and we save the info into the dico
            ' Note : We assume that this dictionary is always protected by
            ' semProtectEnum (Handle.vb)
            If _dicoObjectTypeNumber.ContainsKey(Handle.ObjectTypeNumber) Then
                m_ObjectTypeName = _dicoObjectTypeNumber(Handle.ObjectTypeNumber)
            Else
                ' Have to retrieve this using NtQueryObject...
                NativeFunctions.NtQueryObject(hHandle, _
                                    NativeEnums.ObjectInformationClass.ObjectTypeInformation, _
                                    BufferObjType.Pointer, _
                                    BufferObjType.Size, ret)
                ObjType = BufferObjType.ReadStruct(Of NativeStructs.ObjectTypeInformation)(0)
                m_ObjectTypeName = Marshal.PtrToStringUni(ObjType.Name.Buffer)
                _dicoObjectTypeNumber(Handle.ObjectTypeNumber) = m_ObjectTypeName
            End If


            NativeFunctions.ZeroMemory(BufferObjName, New IntPtr(&H200))

            ' Get the name of the object
            If m_ObjectTypeName = "File" Then
                ' Have to use our kernel if it is a file
                ' = requ�te pour obtenir le nom d'un handle
                ' hDevice : handle du driver KernelMemory
                ' dwIoControlCode : IOCTL_KERNELMEMORY_GETOBJECTNAME
                ' lpInBuffer : une structure SYSTEM_HANDLE_INFORMATION contenant les infos sur le handle
                ' nInBufferSize : taille de la structure SYSTEM_HANDLE_INFORMATION
                ' lpOutBuffer : tampon d'une taille suffisante pour contenir le nom du handle (au moins MAX_PATH caract�res)
                ' nOutBufferSize : taille de ce tampon
                ' lpBytesReturned : taille des donn�es retourn�e (sauf erreur : nOutBufferSize)
                ' lpOverlapped : nul
                ' renvoie ERROR_SUCCESS ou ERROR_BUFFER_TOO_SMALL
                NativeFunctions.DeviceIoControl(hDriver, IOCTL_KERNELMEMORY_GETOBJECTNAME, _
                                                        Handle, 16, BufferObjName.Pointer, _
                                                        512, ret, IntPtr.Zero)
                ObjName = BufferObjName.ReadStruct(Of NativeStructs.ObjectNameInformation)(0)
                ' TODO64 -> x64 not compatible !!!!!
                m_ObjectName = Marshal.PtrToStringUni(BufferObjName.Pointer.Increment(8))
            Else
                ' Not a file, so we query handle name withNtQueryObject
                NativeFunctions.NtQueryObject(hHandle, _
                                NativeEnums.ObjectInformationClass.ObjectNameInformation, _
                                BufferObjName.Pointer, BufferObjName.Size, ret)
                ObjName = BufferObjName.ReadStruct(Of NativeStructs.ObjectNameInformation)(0)
                m_ObjectName = Marshal.PtrToStringUni(ObjName.Name.Buffer)
            End If


            ' Get the name of the handle for differents objects
            If m_ObjectTypeName = "File" Then
                ' Get DOS path
                m_ObjectName = Common.Misc.DeviceDriveNameToDosDriveName(m_ObjectName)
            ElseIf m_ObjectTypeName = "Key" Then
                ' Get key as a standard key (not internal)
                m_ObjectName = GetKeyName(m_ObjectName)
            ElseIf m_ObjectTypeName = "Process" Then
                ' If it's a process, we retrieve processID from handle
                Dim i As Integer = NativeFunctions.GetProcessId(hHandle)
                m_ObjectName = GetProcessNameFromPID(i) & " (" & CStr(i) & ")"
            ElseIf m_ObjectTypeName = "Thread" AndAlso cEnvironment.SupportsGetThreadIdFunction Then
                ' Have to get thread ID, and then, Process ID
                ' These functions are only present in a VISTA OS
                Dim i As Integer = NativeFunctions.GetThreadId(hHandle)
                Dim i2 As Integer = NativeFunctions.GetProcessIdOfThread(hHandle)
                m_ObjectName = GetProcessNameFromPID(i2) & " (" & CStr(i2) & ")" & " - " & CStr(i)
            End If

            ' Close the duplicated handle
            NativeFunctions.CloseHandle(hHandle)

            ' Return all informations we've got
            With h
                ._Attributes = CType(ObjBasic.Attributes, NativeEnums.HandleFlags)
                ._CreateTime = ObjBasic.CreateTime
                ._Flags = Handle.Flags
                ._GenericAll = ObjType.GenericMapping.GenericAll
                ._GenericExecute = ObjType.GenericMapping.GenericExecute
                ._GenericRead = ObjType.GenericMapping.GenericRead
                ._GenericWrite = ObjType.GenericMapping.GenericWrite
                ._GrantedAccess = Handle.GrantedAccess
                ._Handle = New IntPtr(Handle.Handle)
                ._HandleCount = ObjType.TotalNumberOfHandles
                ._InvalidAttributes = ObjType.InvalidAttributes
                ._MaintainHandleDatabase = ObjType.MaintainTypeList
                ._NameInformation = m_ObjectTypeName
                ._NonPagedPoolUsage = ObjType.NonPagedPoolUsage
                ._ObjectAddress = Handle.Object
                ._ObjectCount = ObjType.TotalNumberOfObjects
                ._ObjectName = m_ObjectName
                ._PagedPoolUsage = ObjType.PagedPoolUsage
                ._PeakHandleCount = ObjType.HighWaterNumberOfHandles
                ._PeakObjectCount = ObjType.HighWaterNumberOfObjects
                ._PointerCount = ObjBasic.PointerCount
                ._PoolType = ObjType.PoolType
                ._ProcessID = Handle.ProcessId
                ._Unknown = ObjType.MaintainHandleCount
                ._ValidAccess = ObjType.ValidAccess
                ._ObjectTypeNumber = Handle.ObjectTypeNumber
            End With

            Return h
        End Function

        ' Return key name from internal key name
        Private Function GetKeyName(ByVal strInternalKey As String) As String
            'HKEY_CURRENT_CONFIG
            strInternalKey = Replace(strInternalKey, "\REGISTRY\MACHINE\SYSTEM\CURRENTCONTROLSET\HARDWARE PROFILES\CURRENT", "HKCC")
            'HKEY_CLASSES_ROOT
            strInternalKey = Replace(strInternalKey, "\REGISTRY\MACHINE\SOFTWARE\CLASSES", "HKCR")
            'HKEY_USERS
            strInternalKey = Replace(strInternalKey, "\REGISTRY\USER\S", "HKU\S")
            'HKEY_LOCAL_MACHINE
            strInternalKey = Replace(strInternalKey, "\REGISTRY\MACHINE", "HKLM")
            'HKEY_CURRENT_USER
            strInternalKey = Replace(strInternalKey, "\REGISTRY\USER", "HKCU")
            'on renvoie
            Return strInternalKey
        End Function

        ' Return process name from process ID
        Private Function GetProcessNameFromPID(ByVal ProcessID As Integer) As String
            Return cProcess.GetProcessName(ProcessID)
        End Function

        ' Called when we want to terminate this instance
        Private Sub Class_Terminate_Renamed()
            ' Close handle to the "current process"
            CloseProcessForHandle()
            ' Close handle to the driver
            NativeFunctions.CloseHandle(hDriver)
            ' Stop the driver & remove it
            Try
                If driver IsNot Nothing Then
                    driver.StopService()
                    driver.RemoveService()
                    driver = Nothing
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End Sub

        Protected Overrides Sub Finalize()
            Class_Terminate_Renamed()
            MyBase.Finalize()
        End Sub

    End Class

End Namespace