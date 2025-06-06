﻿
' Lite Process Monitor









'




'



Option Strict On
Imports Native.Api

Namespace Native.Objects

    Public Class [Module]



        ' Private constants




        ' Private attributes


        ' Used to store list of kernel drivers loaded
        Private Shared memAllocDrivers As New Memory.MemoryAlloc(&H1000)



        ' Public properties


        ' Return the kernel file path
        Public Shared ReadOnly Property KernelFilePath() As String
            Get
                ' Retrieve the file path only once
                Static _ret As String = Nothing
                If _ret Is Nothing Then
                    ' First module in list of loaded drivers
                    Dim _dico As Dictionary(Of String, moduleInfos) = [Module].EnumerateKernelModules(1)
                    For Each it As moduleInfos In _dico.Values
                        _ret = it.Path
                    Next
                    If _ret Is Nothing Then
                        _ret = NO_INFO_RETRIEVED
                    End If
                End If
                Return _ret
            End Get
        End Property



        ' Other public




        ' Public functions


        ' Unload a module
        Public Shared Function UnloadModuleByAddress(ByVal address As IntPtr, ByVal pid As Integer) As Boolean
            Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(pid, _
                                                            Security.ProcessAccess.CreateThread)
            ' Create a remote thread a call FreeLibrary
            If hProc.IsNotNull Then
                Dim kernel32 As IntPtr = _
                        Native.Api.NativeFunctions.GetModuleHandle("kernel32.dll")
                Dim freeLibrary As IntPtr = _
                        Native.Api.NativeFunctions.GetProcAddress(kernel32, "FreeLibrary")
                Dim threadId As Integer
                Dim ret As IntPtr = _
                        Native.Api.NativeFunctions.CreateRemoteThread(hProc, _
                                                IntPtr.Zero, 0, freeLibrary, _
                                                address, 0, threadId)
                Return (ret.IsNotNull)
            Else
                Return False
            End If
        End Function

        ' Enumerate modules
        Public Shared Function EnumerateModulesWow64ByProcessId(ByVal pid As Integer, _
                Optional ByVal noFileInfo As Boolean = False) As Dictionary(Of String, moduleInfos)

            Dim retDico As New Dictionary(Of String, moduleInfos)
            Dim buf2 As New DebugBuffer

            ' Query modules info
            buf2.Query(pid, NativeEnums.RtlQueryProcessDebugInformationFlags.Modules Or NativeEnums.RtlQueryProcessDebugInformationFlags.Modules32)

            ' Get debug information
            Dim debugInfo As NativeStructs.DebugInformation = buf2.GetDebugInformation

            If debugInfo.ModuleInformation.IsNotNull Then

                Dim modInfo As New Memory.MemoryAlloc(debugInfo.ModuleInformation)
                Dim mods As NativeStructs.RtlProcessModules = modInfo.ReadStruct(Of NativeStructs.RtlProcessModules)()

                For i As Integer = 0 To mods.NumberOfModules - 1
                    Dim modu As NativeStructs.RtlProcessModuleInformation = modInfo.ReadStruct(Of NativeStructs.RtlProcessModuleInformation)(NativeStructs.RtlProcessModules.ModulesOffset, i)
                    Dim key As String = modu.ImageBase.ToString
                    ' PERFISSUE ??
                    If retDico.ContainsKey(key) = False Then
                        retDico.Add(key, New moduleInfos(modu, pid, noFileInfo))
                    End If
                Next

                ' modInfo.Free()

            End If

            buf2.Dispose()

            Return retDico

        End Function

        ' Enumerate modules
        Public Shared Function EnumerateModulesByProcessId(ByVal pid As Integer, _
                Optional ByVal noFileInfo As Boolean = False) As Dictionary(Of String, moduleInfos)

            ' If it's the SYSTEM process, we return list of loaded drivers
            If pid = 4 Then
                Return EnumerateKernelModules()
            End If

            ' Retrieve modules of a process (uses PEB_LDR_DATA structs)
            Dim retDico As New Dictionary(Of String, moduleInfos)

            Dim hProc As IntPtr
            Dim peb As IntPtr
            Dim loaderDatePtr As IntPtr

            ' Open a reader to access memory !
            Using reader As New ProcessMemReader(pid)
                hProc = reader.ProcessHandle

                If hProc.IsNotNull Then

                    peb = reader.GetPebAddress

                    ' PEB struct documented here
                    ' http://undocumented.ntinternals.net/UserMode/Undocumented%20Functions/NT%20Objects/Process/PEB.html

                    ' Get address of LoaderData pointer
                    peb = peb.Increment(NativeStructs.Peb_LoaderDataOffset)
                    loaderDatePtr = reader.ReadIntPtr(peb)

                    ' PEB_LDR_DATA documented here
                    ' http://msdn.microsoft.com/en-us/library/aa813708(VS.85).aspx
                    Dim ldrData As New Native.Api.NativeStructs.PebLdrData
                    ldrData = CType(reader.ReadStruct(Of Native.Api.NativeStructs.PebLdrData)(loaderDatePtr), _
                                Native.Api.NativeStructs.PebLdrData)

                    ' Now navigate into structure
                    Dim curObj As IntPtr = ldrData.InLoadOrderModuleList.Flink
                    Dim firstObj As IntPtr = curObj
                    Dim dllName As String
                    Dim dllPath As String
                    Dim curEntry As Native.Api.NativeStructs.LdrDataTableEntry
                    Dim i As Integer = 0

                    Do While curObj.IsNotNull

                        If (i > 0 AndAlso curObj = firstObj) Then
                            Exit Do
                        End If

                        ' Read LoaderData entry
                        curEntry = CType(reader.ReadStruct(Of Native.Api.NativeStructs.LdrDataTableEntry)(curObj), _
                                        Native.Api.NativeStructs.LdrDataTableEntry)

                        If (curEntry.DllBase.IsNotNull) Then

                            ' Retrive the path/name of the dll
                            dllPath = reader.ReadUnicodeString(curEntry.FullDllName)
                            If dllPath Is Nothing Then
                                dllPath = NO_INFO_RETRIEVED
                            End If
                            dllName = reader.ReadUnicodeString(curEntry.BaseDllName)
                            If dllName Is Nothing Then
                                dllName = NO_INFO_RETRIEVED
                            End If

                            ' Add to dico
                            ' Key is path-pid-baseAddress
                            Dim _key As String = dllPath.ToString & "-" & pid.ToString & "-" & curEntry.DllBase.ToString
                            If retDico.ContainsKey(_key) = False Then
                                retDico.Add(_key, New moduleInfos(curEntry, pid, dllPath, dllName, noFileInfo))
                            End If

                        End If

                        ' Next entry
                        curObj = curEntry.InLoadOrderLinks.Flink
                        i += 1
                    Loop

                End If

            End Using

            Return retDico

        End Function

        ' Enumerate kernel modules
        Public Shared Function EnumerateKernelModules(Optional ByVal itemToGet As Integer = Integer.MaxValue) As Dictionary(Of String, moduleInfos)

            SyncLock memAllocDrivers

                ' Dico to return
                Dim retDico As New Dictionary(Of String, moduleInfos)
                Dim res As UInteger
                Dim length As Integer
                Dim count As Integer = 0

                ' Query
                res = NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemModuleInformation, _
                                        memAllocDrivers.Pointer, _
                                        memAllocDrivers.Size, _
                                        length)

                ' Resize if necessary
                If res = NativeConstants.STATUS_INFO_LENGTH_MISMATCH Then
                    memAllocDrivers.ResizeNew(length)
                    NativeFunctions.NtQuerySystemInformation(NativeEnums.SystemInformationClass.SystemModuleInformation, _
                                        memAllocDrivers.Pointer, _
                                        memAllocDrivers.Size, _
                                        length)
                End If

                ' Get list of modules from memory
                Dim modules As NativeStructs.RtlProcessModules = _
                        memAllocDrivers.ReadStruct(Of NativeStructs.RtlProcessModules)()

                For x As Integer = 0 To modules.NumberOfModules - 1
                    Dim modu As NativeStructs.RtlProcessModuleInformation = _
                            memAllocDrivers.ReadStruct(Of NativeStructs.RtlProcessModuleInformation)(NativeStructs.RtlProcessModules.ModulesOffset, x)

                    ' Add to dico
                    ' Key is baseAddress
                    Dim _key As String = modu.ImageBase.ToString
                    If retDico.ContainsKey(_key) = False Then
                        ' "System" process has always the same pid : 4
                        retDico.Add(_key, New moduleInfos(modu, 4))
                        count += 1
                        If count >= itemToGet Then
                            ' Ok, return items
                            Return retDico
                        End If
                    End If
                Next

                Return retDico

            End SyncLock

        End Function



        ' Private functions



    End Class

End Namespace
