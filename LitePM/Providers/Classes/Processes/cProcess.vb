﻿
' Lite Process Monitor









'




'



Option Strict On
Imports Common.Misc
Imports Native.Api.Structs

Public Class cProcess
    Inherits cGeneralObject
    Implements IDisposable

#Region "History structure"

    Private Shared _histBuff As Integer = 1000      ' Number of items to save (default)

    Private Shared ReadOnly Property HistSize() As Integer
        Get
            ' Here's the included variables
            'Friend _TimeLine As New List(Of Long)
            'Friend _dicoProcMem As New SortedList(Of Integer, ProcMemInfo)
            'Friend _dicoProcTimes As New SortedList(Of Integer, ProcTimeInfo)
            'Friend _dicoProcIO As New SortedList(Of Integer, ProcIoInfo)
            'Friend _dicoProcMisc As New SortedList(Of Integer, ProcMiscInfo)
            'Friend _dicoProcIODelta As New SortedList(Of Integer, ProcIoInfo)
            Static size As Integer = 0
            ' Calculate it once
            If size = 0 Then
                ' Indexes of dico
                size += 5 * Runtime.InteropServices.Marshal.SizeOf(GetType(Integer))
                ' TimeLine
                size += Runtime.InteropServices.Marshal.SizeOf(GetType(Long))
                ' _dicoProcMem
                size += Runtime.InteropServices.Marshal.SizeOf(GetType(ProcMemInfo))
                ' _dicoProcTimes
                size += Runtime.InteropServices.Marshal.SizeOf(GetType(ProcTimeInfo))
                ' _dicoProcIO and _dicoProcIODelta
                size += 2 * Runtime.InteropServices.Marshal.SizeOf(GetType(ProcIoInfo))
                ' _dicoProcMisc
                size += Runtime.InteropServices.Marshal.SizeOf(GetType(ProcMiscInfo))
            End If
            Return size
        End Get
    End Property
    Public Shared WriteOnly Property BuffSize() As Integer
        Set(ByVal value As Integer)
            _histBuff = CInt(value / HistSize)
        End Set
    End Property

#End Region

    Public Event HasMerged()

    Private _processInfos As processInfos
    Private _processors As Integer = 1       ' By default we consider that there is only one processor

    Private _parentName As String = vbNullString
    Private _cpuUsage As Double
    Private _ioDelta As Native.Api.NativeStructs.IoCounters

    ' Save informations about performance
    Friend _dicoProcMem As New SortedList(Of Integer, ProcMemInfo)
    Friend _dicoProcTimes As New SortedList(Of Integer, ProcTimeInfo)
    Friend _dicoProcIO As New SortedList(Of Integer, ProcIoInfo)
    Friend _dicoProcMisc As New SortedList(Of Integer, ProcMiscInfo)
    Friend _dicoProcIODelta As New SortedList(Of Integer, ProcIoInfo)
    Friend _TimeLine As New List(Of Long)

    Private _handleQueryInfo As IntPtr
    Private _tokenHandle As IntPtr

    ' Informations which will be refreshed each call to 'merge'
    Private _elevation As Native.Api.Enums.ElevationType
    Private _isInJob As Boolean
    Private _isBeingDebugged As Boolean
    Private _isCritical As Boolean
    Private _isBoostEnabled As Boolean
    Private _isWow64Process As Boolean

    Private Shared _hlProcessBeingDebugged As Boolean
    Private Shared _hlProcessInJob As Boolean
    Private Shared _hlProcessSystem As Boolean
    Private Shared _hlProcessOwned As Boolean
    Private Shared _hlProcessService As Boolean
    Private Shared _hlProcessCritical As Boolean
    Private Shared _hlProcessElevated As Boolean
    Private Shared _hlProcessBeingDebuggedColor As Color
    Private Shared _hlProcessInJobColor As Color
    Private Shared _hlProcessSystemColor As Color
    Private Shared _hlProcessOwnedColor As Color
    Private Shared _hlProcessServiceColor As Color
    Private Shared _hlProcessCriticalColor As Color
    Private Shared _hlProcessElevatedColor As Color

#Region "Properties"

    ' Get the performance dictionnaries
    Public ReadOnly Property DicoPerfMem() As SortedList(Of Integer, ProcMemInfo)
        Get
            Return _dicoProcMem
        End Get
    End Property
    Public ReadOnly Property DicoPerfIO() As SortedList(Of Integer, ProcIoInfo)
        Get
            Return _dicoProcIO
        End Get
    End Property
    Public ReadOnly Property DicoPerfIODelta() As SortedList(Of Integer, ProcIoInfo)
        Get
            Return _dicoProcIODelta
        End Get
    End Property
    Public ReadOnly Property DicoPerfTimes() As SortedList(Of Integer, ProcTimeInfo)
        Get
            Return _dicoProcTimes
        End Get
    End Property

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As processInfos)
        _processInfos = infos
        _processors = ProcessProvider.ProcessorCount
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Process
        ' Get a handle if local
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            _handleQueryInfo = Native.Objects.Process.GetProcessHandleById(infos.ProcessId, Native.Objects.Process.ProcessQueryMinRights)
            _tokenHandle = Native.Objects.Token.GetProcessTokenHandleByProcessHandle(_handleQueryInfo, Native.Security.TokenAccess.Query)
        End If
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        MyBase.Finalize()
    End Sub

    Protected Overrides Sub Finalize()
        ' Close a handle if local
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            If _handleQueryInfo.IsNotNull Then
                Native.Objects.General.CloseHandle(_handleQueryInfo)
            End If
        End If
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As processInfos
        Get
            Return _processInfos
        End Get
    End Property

    Public ReadOnly Property ProcessorCount() As Integer
        Get
            Return _processors
        End Get
    End Property

    ' Different kind of property, because it's changed by the call
    ' of aVariable = aProcess.CpuUsage
    Public ReadOnly Property CpuUsage() As Double
        Get
            Return _cpuUsage
        End Get
    End Property

    Public ReadOnly Property IODelta() As Native.Api.NativeStructs.IoCounters
        Get
            Return _ioDelta
        End Get
    End Property

    Public ReadOnly Property IsBeingDebugged() As Boolean
        Get
            Return _isBeingDebugged
        End Get
    End Property

    Public ReadOnly Property IsInJob() As Boolean
        Get
            Return _isInJob
        End Get
    End Property

    Public ReadOnly Property IsCriticalProcess() As Boolean
        Get
            Return _isCritical
        End Get
    End Property

    Public ReadOnly Property IsBoostEnabled() As Boolean
        Get
            Return _isBoostEnabled
        End Get
    End Property

    Public ReadOnly Property ElevationType() As Native.Api.Enums.ElevationType
        Get
            Return _elevation
        End Get
    End Property

    Public ReadOnly Property IsOwnedProcess() As Boolean
        Get
            Return (Common.Objects.Token.CurrentUserName = _processInfos.DomainName & "\" & _processInfos.UserName)
        End Get
    End Property

    Public ReadOnly Property IsSystemProcess() As Boolean
        Get
            'TODO : localization of "NT AUTHORITY" -> now uses only UserName
            'Return _processInfos.DomainName & "\" & _processInfos.UserName = "NT AUTHORITY\SYSTEM"
            Return _processInfos.UserName = "SYSTEM"
        End Get
    End Property

    Public ReadOnly Property IsServiceProcess() As Boolean
        Get
            'TODO
        End Get
    End Property

    Public ReadOnly Property IsWow64Process() As Boolean
        Get
            Return _isWow64Process
        End Get
    End Property

    Public ReadOnly Property HaveFullControl() As Boolean
        Get
            Return _handleQueryInfo.IsNotNull AndAlso Len(_processInfos.Path) > 0
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Private _refrehNumber As Integer = 0

    Public Sub ClearHistory()
        SyncLock _dicoProcIO
            _dicoProcMem.Clear()
            _dicoProcTimes.Clear()
            _dicoProcIO.Clear()
            _dicoProcIODelta.Clear()
            _dicoProcMisc.Clear()
            _refrehNumber = 0
        End SyncLock
    End Sub

    Public Sub Merge(ByRef Proc As processInfos)

        ' Here we do some refreshment
        ' TOCHANGE
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            If _handleQueryInfo.IsNotNull Then
                Call Native.Objects.Token.GetProcessElevationTypeByTokenHandle(_tokenHandle, _elevation)   ' Elevation type
                _isInJob = Native.Objects.Process.IsProcessInJob(_handleQueryInfo)
                _isBeingDebugged = Native.Objects.Process.IsDebuggerPresent(_handleQueryInfo)
                _isWow64Process = Native.Objects.Process.IsWow64Process(_handleQueryInfo)
            End If
        End If

        'TODO
        'Private _isCritical As Boolean
        'Private _isBoostEnabled As Boolean
        'IsService ??

        ' Refresh numerical infos
        Call refreshCpuUsage()
        Call refreshIODelta()

        _refrehNumber += 1   ' This is the key for the history

        ' Get date in ms
        Dim _now As Long = Date.Now.Ticks

        _processInfos.Merge(Proc)
        Call RefreshSpecialInformations()

        ' Remove items from history if buffer is full
        Dim d As Integer = _refrehNumber - _histBuff
        If _histBuff > 0 AndAlso d > 0 Then
            _dicoProcMem.Remove(d)
            _dicoProcTimes.Remove(d)
            _dicoProcIO.Remove(d)
            _dicoProcIODelta.Remove(d)
            _dicoProcMisc.Remove(d)
            _TimeLine.RemoveAt(0)
        End If

        ' Store history
        SyncLock _dicoProcIO
            _dicoProcMem.Add(_refrehNumber, New ProcMemInfo(_now, Me.Infos.MemoryInfos))
            _dicoProcTimes.Add(_refrehNumber, New ProcTimeInfo(_now, Me.Infos.UserTime, Me.Infos.KernelTime))
            _dicoProcIO.Add(_refrehNumber, New ProcIoInfo(_now, Me.Infos.IOValues))
            _dicoProcIODelta.Add(_refrehNumber, New ProcIoInfo(_now, _ioDelta))
            _dicoProcMisc.Add(_refrehNumber, New ProcMiscInfo(_now, Me.Infos.GdiObjects, Me.Infos.UserObjects, _
                     100 * Me.CpuUsage, 100 * Me.Infos.AverageCpuUsage))
        End SyncLock
        _TimeLine.Add(Date.Now.Ticks)

        RaiseEvent HasMerged()
    End Sub
    Public Sub Merge(ByRef Proc As Native.Api.Structs.SystemProcessInformation64)
        _processInfos.Merge(New processInfos(Proc))
        Call RefreshSpecialInformations()
    End Sub

#Region "Special informations (GDI, affinity)"

    ' Refresh some non fixed infos
    ' For now IT IS NOT ASYNC
    ' Because create ~50 threads/sec is not really cool
    Private WithEvents asyncNonFixed As asyncCallbackProcGetNonFixedInfos
    Private Sub RefreshSpecialInformations()
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                If asyncNonFixed Is Nothing Then
                    asyncNonFixed = New asyncCallbackProcGetNonFixedInfos(Me.Infos.ProcessId, _handleQueryInfo)
                End If
                asyncNonFixed.Process()
        End Select
    End Sub
    Private Sub nonFixedInfosGathered(ByVal infos As asyncCallbackProcGetNonFixedInfos.TheseInfos) Handles asyncNonFixed.GatheredInfos
        Me.Infos.UserObjects = infos.userO
        Me.Infos.GdiObjects = infos.gdiO
        Me.Infos.AffinityMask = infos.affinity
    End Sub

#End Region

#Region "All actions on process (kill...)"

    ' Create dump file
    Private _createDumpF As asyncCallbackProcMinidump
    Public Sub CreateDumpFile(ByVal file As String, ByVal opt As Native.Api.NativeEnums.MiniDumpType)

        If _createDumpF Is Nothing Then
            _createDumpF = New asyncCallbackProcMinidump(New asyncCallbackProcMinidump.HasCreatedDump(AddressOf createdMinidump))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _createDumpF.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcMinidump.poolObj(Me.Infos.ProcessId, file, opt, newAction))

    End Sub
    Private Sub createdMinidump(ByVal Success As Boolean, ByVal pid As Integer, ByVal file As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not create minidump file for process " & pid.ToString & " : " & msg)
        Else
            Misc.ShowMsg("Mini dump file", "The dump file has been created successfully.", MessageBoxButtons.OK)
        End If
        RemovePendingTask(actionNumber)
    End Sub


    ' Set priority
    Private _setPriorityP As asyncCallbackProcSetPriority
    Public Function SetPriority(ByVal level As ProcessPriorityClass) As Integer

        If _setPriorityP Is Nothing Then
            _setPriorityP = New asyncCallbackProcSetPriority(New asyncCallbackProcSetPriority.HasSetPriority(AddressOf setPriorityDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setPriorityP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcSetPriority.poolObj(Me.Infos.ProcessId, level, newAction))

    End Function
    Private Sub setPriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not set priority to process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Kill a process
    Private _killP As asyncCallbackProcKill
    Public Function Kill() As Integer

        If _killP Is Nothing Then
            _killP = New asyncCallbackProcKill(New asyncCallbackProcKill.HasKilled(AddressOf killDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcKill.poolObj(Me.Infos.ProcessId, newAction))

    End Function
    Private Sub killDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not kill process " & Me.Infos.Name & " (" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Kill a process by method
    Private _killPM As asyncCallbackProcKillByMethod
    Public Function KillByMethod(ByVal method As Native.Api.Enums.KillMethod) As Integer

        If _killPM Is Nothing Then
            _killPM = New asyncCallbackProcKillByMethod(New asyncCallbackProcKillByMethod.HasKilled(AddressOf killDoneM))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killPM.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcKillByMethod.poolObj(Me.Infos.ProcessId, method, newAction))

    End Function
    Private Sub killDoneM(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not kill process by method " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Decrease priority
    Private _decP As asyncCallbackProcDecreasePriority
    Public Function DecreasePriority() As Integer

        If _decP Is Nothing Then
            _decP = New asyncCallbackProcDecreasePriority(New asyncCallbackProcDecreasePriority.HasDecreasedPriority(AddressOf decreasePriorityDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _decP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcDecreasePriority.poolObj(Me.Infos.ProcessId, Me.Infos.Priority, newAction))

    End Function
    Private Sub decreasePriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not set priority to process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Increase priority
    Private _incP As asyncCallbackProcIncreasePriority
    Public Function IncreasePriority() As Integer

        If _incP Is Nothing Then
            _incP = New asyncCallbackProcIncreasePriority(New asyncCallbackProcIncreasePriority.HasIncreasedPriority(AddressOf increasePriorityDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _incP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcIncreasePriority.poolObj(Me.Infos.ProcessId, Me.Infos.Priority, newAction))

    End Function
    Private Sub increasePriorityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not set priority to process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Suspend a process
    Private _suspP As asyncCallbackProcSuspend
    Public Function SuspendProcess() As Integer

        If _suspP Is Nothing Then
            _suspP = New asyncCallbackProcSuspend(New asyncCallbackProcSuspend.HasSuspended(AddressOf suspendDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _suspP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcSuspend.poolObj(Me.Infos.ProcessId, newAction))

    End Function
    Private Sub suspendDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not suspend process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Resume a process
    Private _resuP As asyncCallbackProcResume
    Public Function ResumeProcess() As Integer

        If _resuP Is Nothing Then
            _resuP = New asyncCallbackProcResume(New asyncCallbackProcResume.HasResumed(AddressOf resumeDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _resuP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcResume.poolObj(Me.Infos.ProcessId, newAction))

    End Function
    Private Sub resumeDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not resume process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Kill a process tree
    Private _killTP As asyncCallbackProcKillTree
    Public Function KillProcessTree() As Integer

        If _killTP Is Nothing Then
            _killTP = New asyncCallbackProcKillTree(New asyncCallbackProcKillTree.HasKilled(AddressOf recursiveKillDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _killTP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcKillTree.poolObj(Me.Infos.ProcessId, newAction))

    End Function
    Private Sub recursiveKillDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not kill process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Empty working set size
    Private _emptyP As asyncCallbackProcEmptyWorkingSet
    Public Function EmptyWorkingSetSize() As Integer

        If _emptyP Is Nothing Then
            _emptyP = New asyncCallbackProcEmptyWorkingSet(New asyncCallbackProcEmptyWorkingSet.HasReducedWorkingSet(AddressOf emptyWorkingSetSizeDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _emptyP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcEmptyWorkingSet.poolObj(Me.Infos.ProcessId, newAction))

    End Function
    Private Sub emptyWorkingSetSizeDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not empty working set of process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Change affinity
    Private _setAffinityP As asyncCallbackProcSetAffinity
    Public Function SetAffinity(ByVal affinity As Integer) As Integer

        If _setAffinityP Is Nothing Then
            _setAffinityP = New asyncCallbackProcSetAffinity(New asyncCallbackProcSetAffinity.HasSetAffinity(AddressOf setAffinityDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setAffinityP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcSetAffinity.poolObj(Me.Infos.ProcessId, affinity, newAction))

    End Function
    Private Sub setAffinityDone(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not set affinity to process " & Me.Infos.Name & "(" & Me.Infos.ProcessId.ToString & ") : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return processInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        res = NO_INFO_RETRIEVED
        Select Case info
            Case "ParentPID"
                res = Me.Infos.ParentProcessId.ToString
            Case "PID"
                res = Me.Infos.ProcessId.ToString
            Case "UserName"
                If My.Settings.ShowUserGroupDomain AndAlso Len(Me.Infos.DomainName) > 0 Then
                    res = Me.Infos.DomainName & "\" & Me.Infos.UserName
                Else
                    res = Me.Infos.UserName
                End If
            Case "CpuUsage"
                res = GetFormatedPercentage(Me.CpuUsage)
            Case "KernelCpuTime"
                Dim ts As Date = New Date(Me.Infos.KernelTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "UserCpuTime"
                Dim ts As Date = New Date(Me.Infos.UserTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "TotalCpuTime"
                Dim ts As Date = New Date(Me.Infos.ProcessorTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "StartTime"
                If Me.Infos.StartTime > 0 Then
                    Dim ts As Date = New Date(Me.Infos.StartTime)
                    res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
                End If
            Case "WorkingSet"
                res = GetFormatedSize(Me.Infos.MemoryInfos.WorkingSetSize)
            Case "PeakWorkingSet"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PeakWorkingSetSize)
            Case "PageFaultCount"
                res = Me.Infos.MemoryInfos.PageFaultCount.ToString
            Case "PagefileUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PagefileUsage)
            Case "PeakPagefileUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PeakPagefileUsage)
            Case "QuotaPeakPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPeakPagedPoolUsage)
            Case "QuotaPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPagedPoolUsage)
            Case "QuotaPeakNonPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPeakNonPagedPoolUsage)
            Case "QuotaNonPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaNonPagedPoolUsage)
            Case "Priority"
                res = Me.Infos.Priority.ToString
            Case "Path"
                res = Me.Infos.Path
            Case "Description"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileDescription
                End If
            Case "Copyright"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.LegalCopyright
                End If
            Case "Version"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileVersion
                End If
            Case "Company"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.CompanyName
                End If
            Case "Name"
                res = Me.Infos.Name
            Case "GdiObjects"
                res = Me.Infos.GdiObjects.ToString
            Case "UserObjects"
                res = Me.Infos.UserObjects.ToString
            Case "RunTime"
                Dim ts As New Date(Date.Now.Ticks - Me.Infos.StartTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
            Case "AffinityMask"
                res = Me.Infos.AffinityMask.ToString
            Case "AverageCpuUsage"
                res = GetFormatedPercentage(Me.Infos.AverageCpuUsage, forceZeroDisplay:=True)
            Case "CommandLine"
                res = Me.Infos.CommandLine
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount.ToString
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount.ToString
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount.ToString
            Case "ReadTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.ReadTransferCount)
            Case "WriteTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.WriteTransferCount)
            Case "OtherTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.OtherTransferCount)
            Case "ReadOperationCountDelta"
                res = _ioDelta.ReadOperationCount.ToString
            Case "WriteOperationCountDelta"
                res = _ioDelta.WriteOperationCount.ToString
            Case "OtherOperationCountDelta"
                res = _ioDelta.OtherOperationCount.ToString
            Case "ReadTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.ReadTransferCount)
            Case "WriteTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.WriteTransferCount)
            Case "OtherTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.OtherTransferCount)
            Case "TotalIoDelta"
                res = GetFormatedSizePerSecond(_ioDelta.OtherTransferCount + _ioDelta.WriteTransferCount + _ioDelta.ReadTransferCount)
            Case "HandleCount"
                res = Me.Infos.HandleCount.ToString
            Case "ThreadCount"
                res = Me.Infos.ThreadCount.ToString
            Case "InJob"
                res = Me.IsInJob.ToString
            Case "Elevation"
                res = Me.ElevationType.ToString
            Case "BeingDebugged"
                res = Me.IsBeingDebugged.ToString
            Case "OwnedProcess"
                res = Me.IsOwnedProcess.ToString
            Case "SystemProcess"
                res = Me.IsSystemProcess.ToString
            Case "ServiceProcess"
                res = Me.IsServiceProcess.ToString
            Case "CriticalProcess"
                res = Me.IsCriticalProcess.ToString
            Case "IsWow64"
                res = Me.IsWow64Process.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_ParentPID As String = ""
        Static _old_PID As String = ""
        Static _old_UserName As String = ""
        Static _old_CpuUsage As String = ""
        Static _old_KernelCpuTime As String = ""
        Static _old_UserCpuTime As String = ""
        Static _old_TotalCpuTime As String = ""
        Static _old_StartTime As String = ""
        Static _old_WorkingSet As String = ""
        Static _old_PeakWorkingSet As String = ""
        Static _old_PageFaultCount As String = ""
        Static _old_PagefileUsage As String = ""
        Static _old_PeakPagefileUsage As String = ""
        Static _old_QuotaPeakPagedPoolUsage As String = ""
        Static _old_QuotaPagedPoolUsage As String = ""
        Static _old_QuotaPeakNonPagedPoolUsage As String = ""
        Static _old_QuotaNonPagedPoolUsage As String = ""
        Static _old_Priority As String = ""
        Static _old_Path As String = ""
        Static _old_Description As String = ""
        Static _old_Copyright As String = ""
        Static _old_Version As String = ""
        Static _old_CompanyName As String = ""
        Static _old_Name As String = ""
        Static _old_GdiObjects As String = ""
        Static _old_UserObjects As String = ""
        Static _old_RunTime As String = ""
        Static _old_AffinityMask As String = ""
        Static _old_AverageCpuUsage As String = ""
        Static _old_CommandLine As String = ""
        Static _old_ReadOperationCount As String = ""
        Static _old_WriteOperationCount As String = ""
        Static _old_OtherOperationCount As String = ""
        Static _old_ReadTransferCount As String = ""
        Static _old_WriteTransferCount As String = ""
        Static _old_OtherTransferCount As String = ""
        Static _old_ReadOperationCountDelta As String = ""
        Static _old_WriteOperationCountDelta As String = ""
        Static _old_OtherOperationCountDelta As String = ""
        Static _old_ReadTransferCountDelta As String = ""
        Static _old_WriteTransferCountDelta As String = ""
        Static _old_OtherTransferCountDelta As String = ""
        Static _old_TotalIoDelta As String = ""
        Static _old_HandleCount As String = ""
        Static _old_ThreadCount As String = ""
        Static _old_InJob As String = ""
        Static _old_Elevation As String = ""
        Static _old_BeingDebugged As String = ""
        Static _old_OwnedProcess As String = ""
        Static _old_SystemProcess As String = ""
        Static _old_ServiceProcess As String = ""
        Static _old_CriticalProcess As String = ""
        Static _old_IsWow64Process As String = ""

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                Return False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                Return False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        res = NO_INFO_RETRIEVED
        Select Case info
            Case "ParentPID"
                res = Me.Infos.ParentProcessId.ToString
                If res = _old_ParentPID Then
                    hasChanged = False
                Else
                    _old_ParentPID = res
                End If
            Case "PID"
                res = Me.Infos.ProcessId.ToString
                If res = _old_PID Then
                    hasChanged = False
                Else
                    _old_PID = res
                End If
            Case "UserName"
                If My.Settings.ShowUserGroupDomain AndAlso Len(Me.Infos.DomainName) > 0 Then
                    res = Me.Infos.DomainName & "\" & Me.Infos.UserName
                Else
                    res = Me.Infos.UserName
                End If
                If res = _old_UserName Then
                    hasChanged = False
                Else
                    _old_UserName = res
                End If
            Case "CpuUsage"
                res = GetFormatedPercentage(Me.CpuUsage)
                If res = _old_CpuUsage Then
                    hasChanged = False
                Else
                    _old_CpuUsage = res
                End If
            Case "KernelCpuTime"
                Dim ts As Date = New Date(Me.Infos.KernelTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_KernelCpuTime Then
                    hasChanged = False
                Else
                    _old_KernelCpuTime = res
                End If
            Case "UserCpuTime"
                Dim ts As Date = New Date(Me.Infos.UserTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_UserCpuTime Then
                    hasChanged = False
                Else
                    _old_UserCpuTime = res
                End If
            Case "TotalCpuTime"
                Dim ts As Date = New Date(Me.Infos.ProcessorTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_TotalCpuTime Then
                    hasChanged = False
                Else
                    _old_TotalCpuTime = res
                End If
            Case "StartTime"
                If Me.Infos.StartTime > 0 Then
                    Dim ts As Date = New Date(Me.Infos.StartTime)
                    res = ts.ToLongDateString & " -- " & ts.ToLongTimeString
                End If
                If res = _old_StartTime Then
                    hasChanged = False
                Else
                    _old_StartTime = res
                End If
            Case "WorkingSet"
                res = GetFormatedSize(Me.Infos.MemoryInfos.WorkingSetSize)
                If res = _old_WorkingSet Then
                    hasChanged = False
                Else
                    _old_WorkingSet = res
                End If
            Case "PeakWorkingSet"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PeakWorkingSetSize)
                If res = _old_PeakWorkingSet Then
                    hasChanged = False
                Else
                    _old_PeakWorkingSet = res
                End If
            Case "PageFaultCount"
                res = Me.Infos.MemoryInfos.PageFaultCount.ToString
                If res = _old_PageFaultCount Then
                    hasChanged = False
                Else
                    _old_PageFaultCount = res
                End If
            Case "PagefileUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PagefileUsage)
                If res = _old_PagefileUsage Then
                    hasChanged = False
                Else
                    _old_PagefileUsage = res
                End If
            Case "PeakPagefileUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.PeakPagefileUsage)
                If res = _old_PeakPagefileUsage Then
                    hasChanged = False
                Else
                    _old_PeakPagefileUsage = res
                End If
            Case "QuotaPeakPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPeakPagedPoolUsage)
                If res = _old_QuotaPeakPagedPoolUsage Then
                    hasChanged = False
                Else
                    _old_QuotaPeakPagedPoolUsage = res
                End If
            Case "QuotaPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPagedPoolUsage)
                If res = _old_QuotaPagedPoolUsage Then
                    hasChanged = False
                Else
                    _old_QuotaPagedPoolUsage = res
                End If
            Case "QuotaPeakNonPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaPeakNonPagedPoolUsage)
                If res = _old_QuotaPeakNonPagedPoolUsage Then
                    hasChanged = False
                Else
                    _old_QuotaPeakNonPagedPoolUsage = res
                End If
            Case "QuotaNonPagedPoolUsage"
                res = GetFormatedSize(Me.Infos.MemoryInfos.QuotaNonPagedPoolUsage)
                If res = _old_QuotaNonPagedPoolUsage Then
                    hasChanged = False
                Else
                    _old_QuotaNonPagedPoolUsage = res
                End If
            Case "Priority"
                res = Me.Infos.Priority.ToString
                If res = _old_Priority Then
                    hasChanged = False
                Else
                    _old_Priority = res
                End If
            Case "Path"
                res = Me.Infos.Path
                If res = _old_Path Then
                    hasChanged = False
                Else
                    _old_Path = res
                End If
            Case "Description"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileDescription
                End If
                If res = _old_Description Then
                    hasChanged = False
                Else
                    _old_Description = res
                End If
            Case "Copyright"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.LegalCopyright
                End If
                If res = _old_Copyright Then
                    hasChanged = False
                Else
                    _old_Copyright = res
                End If
            Case "Version"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.FileVersion
                End If
                If res = _old_Version Then
                    hasChanged = False
                Else
                    _old_Version = res
                End If
            Case "Company"
                If Me.Infos.FileInfo IsNot Nothing Then
                    res = Me.Infos.FileInfo.CompanyName
                End If
                If res = _old_CompanyName Then
                    hasChanged = False
                Else
                    _old_CompanyName = res
                End If
            Case "Name"
                res = Me.Infos.Name
                If Me.IsWow64Process Then
                    res &= " * 32"
                End If
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "GdiObjects"
                res = Me.Infos.GdiObjects.ToString
                If res = _old_GdiObjects Then
                    hasChanged = False
                Else
                    _old_GdiObjects = res
                End If
            Case "UserObjects"
                res = Me.Infos.UserObjects.ToString
                If res = _old_UserObjects Then
                    hasChanged = False
                Else
                    _old_UserObjects = res
                End If
            Case "RunTime"
                Dim ts As New Date(Date.Now.Ticks - Me.Infos.StartTime)
                res = String.Format("{0:00}", ts.Hour) & ":" & _
                    String.Format("{0:00}", ts.Minute) & ":" & _
                    String.Format("{0:00}", ts.Second) & ":" & _
                    String.Format("{000}", ts.Millisecond)
                If res = _old_RunTime Then
                    hasChanged = False
                Else
                    _old_RunTime = res
                End If
            Case "AffinityMask"
                res = Me.Infos.AffinityMask.ToString
                If res = _old_AffinityMask Then
                    hasChanged = False
                Else
                    _old_AffinityMask = res
                End If
            Case "AverageCpuUsage"
                res = GetFormatedPercentage(Me.Infos.AverageCpuUsage, forceZeroDisplay:=True)
                If res = _old_AverageCpuUsage Then
                    hasChanged = False
                Else
                    _old_AverageCpuUsage = res
                End If
            Case "CommandLine"
                res = Me.Infos.CommandLine
                If res = _old_CommandLine Then
                    hasChanged = False
                Else
                    _old_CommandLine = res
                End If
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount.ToString
                If res = _old_ReadOperationCount Then
                    hasChanged = False
                Else
                    _old_ReadOperationCount = res
                End If
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount.ToString
                If res = _old_WriteOperationCount Then
                    hasChanged = False
                Else
                    _old_WriteOperationCount = res
                End If
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount.ToString
                If res = _old_OtherOperationCount Then
                    hasChanged = False
                Else
                    _old_OtherOperationCount = res
                End If
            Case "ReadTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.ReadTransferCount)
                If res = _old_ReadTransferCount Then
                    hasChanged = False
                Else
                    _old_ReadTransferCount = res
                End If
            Case "WriteTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.WriteTransferCount)
                If res = _old_WriteTransferCount Then
                    hasChanged = False
                Else
                    _old_WriteTransferCount = res
                End If
            Case "OtherTransferCount"
                res = GetFormatedSize(Me.Infos.IOValues.OtherTransferCount)
                If res = _old_OtherTransferCount Then
                    hasChanged = False
                Else
                    _old_OtherTransferCount = res
                End If
            Case "ReadOperationCountDelta"
                res = _ioDelta.ReadOperationCount.ToString
                If res = _old_ReadOperationCountDelta Then
                    hasChanged = False
                Else
                    _old_ReadOperationCountDelta = res
                End If
            Case "WriteOperationCountDelta"
                res = _ioDelta.WriteOperationCount.ToString
                If res = _old_WriteOperationCountDelta Then
                    hasChanged = False
                Else
                    _old_WriteOperationCountDelta = res
                End If
            Case "OtherOperationCountDelta"
                res = _ioDelta.OtherOperationCount.ToString
                If res = _old_OtherOperationCountDelta Then
                    hasChanged = False
                Else
                    _old_OtherOperationCountDelta = res
                End If
            Case "ReadTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.ReadTransferCount)
                If res = _old_ReadTransferCountDelta Then
                    hasChanged = False
                Else
                    _old_ReadTransferCountDelta = res
                End If
            Case "WriteTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.WriteTransferCount)
                If res = _old_WriteTransferCountDelta Then
                    hasChanged = False
                Else
                    _old_WriteTransferCountDelta = res
                End If
            Case "OtherTransferCountDelta"
                res = GetFormatedSizePerSecond(_ioDelta.OtherTransferCount)
                If res = _old_OtherTransferCountDelta Then
                    hasChanged = False
                Else
                    _old_OtherTransferCountDelta = res
                End If
            Case "TotalIoDelta"
                res = GetFormatedSizePerSecond(_ioDelta.OtherTransferCount + _ioDelta.ReadTransferCount + _ioDelta.WriteTransferCount)
                If res = _old_TotalIoDelta Then
                    hasChanged = False
                Else
                    _old_TotalIoDelta = res
                End If
            Case "HandleCount"
                res = Me.Infos.HandleCount.ToString
                If res = _old_HandleCount Then
                    hasChanged = False
                Else
                    _old_HandleCount = res
                End If
            Case "ThreadCount"
                res = Me.Infos.ThreadCount.ToString
                If res = _old_ThreadCount Then
                    hasChanged = False
                Else
                    _old_ThreadCount = res
                End If
            Case "InJob"
                res = Me.IsInJob.ToString
                If res = _old_InJob Then
                    hasChanged = False
                Else
                    _old_InJob = res
                End If
            Case "Elevation"
                res = Me.ElevationType.ToString
                If res = _old_Elevation Then
                    hasChanged = False
                Else
                    _old_Elevation = res
                End If
            Case "BeingDebugged"
                res = Me.IsBeingDebugged.ToString
                If res = _old_BeingDebugged Then
                    hasChanged = False
                Else
                    _old_BeingDebugged = res
                End If
            Case "OwnedProcess"
                res = Me.IsOwnedProcess.ToString
                If res = _old_OwnedProcess Then
                    hasChanged = False
                Else
                    _old_OwnedProcess = res
                End If
            Case "SystemProcess"
                res = Me.IsSystemProcess.ToString
                If res = _old_SystemProcess Then
                    hasChanged = False
                Else
                    _old_SystemProcess = res
                End If
            Case "ServiceProcess"
                res = Me.IsServiceProcess.ToString
                If res = _old_ServiceProcess Then
                    hasChanged = False
                Else
                    _old_ServiceProcess = res
                End If
            Case "CriticalProcess"
                res = Me.IsCriticalProcess.ToString
                If res = _old_CriticalProcess Then
                    hasChanged = False
                Else
                    _old_CriticalProcess = res
                End If
            Case "IsWow64"
                res = Me.IsWow64Process.ToString
                If res = _old_IsWow64Process Then
                    hasChanged = False
                Else
                    _old_IsWow64Process = res
                End If
        End Select

        Return hasChanged
    End Function

    ' Retrieve informations by its name (numerical)
    Public Function GetInformationNumerical(ByVal infoName As String) As Double
        Dim res As Double = 0

        Select Case infoName
            Case "KernelCpuTime"
                res = Me.Infos.KernelTime
            Case "UserCpuTime"
                res = Me.Infos.UserTime
            Case "TotalCpuTime"
                res = Me.Infos.ProcessorTime
            Case "WorkingSet"
                res = Me.Infos.MemoryInfos.WorkingSetSize
            Case "PeakWorkingSet"
                res = Me.Infos.MemoryInfos.PeakWorkingSetSize
            Case "PageFaultCount"
                res = Me.Infos.MemoryInfos.PageFaultCount
            Case "PagefileUsage"
                res = Me.Infos.MemoryInfos.PagefileUsage
            Case "PeakPagefileUsage"
                res = Me.Infos.MemoryInfos.PeakPagefileUsage
            Case "QuotaPeakPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaPeakPagedPoolUsage
            Case "QuotaPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaPagedPoolUsage
            Case "QuotaPeakNonPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaPeakNonPagedPoolUsage
            Case "QuotaNonPagedPoolUsage"
                res = Me.Infos.MemoryInfos.QuotaNonPagedPoolUsage
            Case "GdiObjects"
                res = Me.Infos.GdiObjects
            Case "UserObjects"
                res = Me.Infos.UserObjects
            Case "ReadOperationCount"
                res = Me.Infos.IOValues.ReadOperationCount
            Case "WriteOperationCount"
                res = Me.Infos.IOValues.WriteOperationCount
            Case "OtherOperationCount"
                res = Me.Infos.IOValues.OtherOperationCount
            Case "ReadTransferCount"
                res = Me.Infos.IOValues.ReadTransferCount
            Case "WriteTransferCount"
                res = Me.Infos.IOValues.WriteTransferCount
            Case "OtherTransferCount"
                res = Me.Infos.IOValues.OtherTransferCount
            Case "ReadOperationCountDelta"
                res = _ioDelta.ReadOperationCount
            Case "WriteOperationCountDelta"
                res = _ioDelta.WriteOperationCount
            Case "OtherOperationCountDelta"
                res = _ioDelta.OtherOperationCount
            Case "ReadTransferCountDelta"
                res = _ioDelta.ReadTransferCount
            Case "WriteTransferCountDelta"
                res = _ioDelta.WriteTransferCount
            Case "OtherTransferCountDelta"
                res = _ioDelta.OtherTransferCount
            Case "TotalIoDelta"
                res = _ioDelta.OtherTransferCount + _ioDelta.WriteTransferCount + _ioDelta.ReadTransferCount
            Case "CpuUsage"
                res = 100 * Me.CpuUsage
            Case "AverageCpuUsage"
                res = 100 * Me.Infos.AverageCpuUsage
            Case "HandleCount"
                res = Me.Infos.HandleCount
        End Select

        Return res
    End Function

#End Region

#Region "Shared function"

    ' Return Process name
    Public Shared Function GetProcessName(ByVal pid As Integer) As String
        Dim cp As cProcess = ProcessProvider.GetProcessById(pid)
        If cp IsNot Nothing Then
            Return cp.Infos.Name
        Else
            Return NO_INFO_RETRIEVED
        End If
    End Function

    ' Kill a process
    Private Shared _sharedKillP As asyncCallbackProcKill
    Public Shared Function SharedLRKill(ByVal pid As Integer) As Integer

        If _sharedKillP Is Nothing Then
            _sharedKillP = New asyncCallbackProcKill(New asyncCallbackProcKill.HasKilled(AddressOf killDoneShared))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _sharedKillP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcKill.poolObj(pid, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub killDoneShared(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not kill process " & pid.ToString & " : " & msg)
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

    ' Start a process
    Private Shared _newSharedP As asyncCallbackProcNewProcess
    Public Shared Function SharedRLStartNewProcess(ByVal path As String) As Integer

        If _newSharedP Is Nothing Then
            _newSharedP = New asyncCallbackProcNewProcess(New asyncCallbackProcNewProcess.HasCreated(AddressOf newProcessDoneShared))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _newSharedP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcNewProcess.poolObj(path, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub newProcessDoneShared(ByVal Success As Boolean, ByVal path As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not start process " & path & " : " & msg)
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub


    ' Unload a module from a process
    Private Shared _unloadMSharedP As asyncCallbackProcUnloadModule
    Public Shared Function SharedRLUnLoadModuleFromProcess(ByVal pid As Integer, ByVal ModuleBaseAddress As IntPtr) As Integer

        If _unloadMSharedP Is Nothing Then
            _unloadMSharedP = New asyncCallbackProcUnloadModule(New asyncCallbackProcUnloadModule.HasUnloadedModule(AddressOf unloadModuleDoneShared))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _unloadMSharedP.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackProcUnloadModule.poolObj(pid, ModuleBaseAddress, newAction))

        AddSharedPendingTask(newAction, t)
    End Function
    Private Shared Sub unloadModuleDoneShared(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not unload module from process " & pid.ToString & " : " & msg)
        End If
        RemoveSharedPendingTask(actionNumber)
    End Sub

#End Region

#Region "Shared functions (local)"

    Public Shared Function LocalKill(ByVal pid As Integer) As Boolean
        Return Native.Objects.Process.KillProcessById(pid)
    End Function

#End Region

    ' Retrieve a long array with all available values from dictionnaries
    Public Function GetHistory(ByVal infoName As String) As Double()
        Dim ret() As Double

        Select Case infoName
            Case "KernelCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcTimeInfo In _dicoProcTimes.Values
                    ret(x) = t.kernel
                    x += 1
                Next
            Case "UserCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcTimeInfo In _dicoProcTimes.Values
                    ret(x) = t.user
                    x += 1
                Next
            Case "TotalCpuTime"
                ReDim ret(_dicoProcTimes.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcTimeInfo In _dicoProcTimes.Values
                    ret(x) = t.total
                    x += 1
                Next
            Case "WorkingSet"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.WorkingSetSize
                    x += 1
                Next
            Case "PeakWorkingSet"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.PeakWorkingSetSize
                    x += 1
                Next
            Case "PageFaultCount"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.PageFaultCount
                    x += 1
                Next
            Case "PagefileUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.PagefileUsage
                    x += 1
                Next
            Case "PeakPagefileUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.PeakPagefileUsage
                    x += 1
                Next
            Case "QuotaPeakPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPeakPagedPoolUsage
                    x += 1
                Next
            Case "QuotaPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPagedPoolUsage
                    x += 1
                Next
            Case "QuotaPeakNonPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaPeakNonPagedPoolUsage
                    x += 1
                Next
            Case "QuotaNonPagedPoolUsage"
                ReDim ret(_dicoProcMem.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMemInfo In _dicoProcMem.Values
                    ret(x) = t.mem.QuotaNonPagedPoolUsage
                    x += 1
                Next
            Case "ReadOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIO.Values
                    ret(x) = CLng(t.io.ReadOperationCount)
                    x += 1
                Next
            Case "WriteOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIO.Values
                    ret(x) = CLng(t.io.WriteOperationCount)
                    x += 1
                Next
            Case "OtherOperationCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIO.Values
                    ret(x) = CLng(t.io.OtherOperationCount)
                    x += 1
                Next
            Case "ReadTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIO.Values
                    ret(x) = CLng(t.io.ReadTransferCount)
                    x += 1
                Next
            Case "WriteTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIO.Values
                    ret(x) = CLng(t.io.WriteTransferCount)
                    x += 1
                Next
            Case "OtherTransferCount"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIO.Values
                    ret(x) = CLng(t.io.OtherTransferCount)
                    x += 1
                Next
            Case "GdiObjects"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMiscInfo In _dicoProcMisc.Values
                    ret(x) = t.gdiO
                    x += 1
                Next
            Case "UserObjects"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMiscInfo In _dicoProcMisc.Values
                    ret(x) = t.userO
                    x += 1
                Next
            Case "CpuUsage"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMiscInfo In _dicoProcMisc.Values
                    ret(x) = t.cpuUsage
                    x += 1
                Next
            Case "AverageCpuUsage"
                ReDim ret(_dicoProcMisc.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcMiscInfo In _dicoProcMisc.Values
                    ret(x) = t.averageCpuUsage
                    x += 1
                Next
            Case "ReadOperationCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.ReadOperationCount)
                    x += 1
                Next
            Case "WriteOperationCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.WriteOperationCount)
                    x += 1
                Next
            Case "OtherOperationCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.OtherOperationCount)
                    x += 1
                Next
            Case "ReadTransferCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.ReadTransferCount)
                    x += 1
                Next
            Case "WriteTransferCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.WriteTransferCount)
                    x += 1
                Next
            Case "OtherTransferCountDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.OtherTransferCount)
                    x += 1
                Next
            Case "TotalIoDelta"
                ReDim ret(_dicoProcIO.Count - 1)
                Dim x As Integer = 0
                For Each t As ProcIoInfo In _dicoProcIODelta.Values
                    ret(x) = CLng(t.io.OtherTransferCount + t.io.ReadTransferCount + t.io.WriteTransferCount)
                    x += 1
                Next
            Case Else
                ReDim ret(0)
        End Select

        Return ret
    End Function

    ' Return timeline history
    Public Function GetTimeLine() As List(Of Long)
        Return _TimeLine
    End Function

    ' Refresh CPU usage once
    Private Sub refreshCpuUsage()
        Static oldDate As Long = Date.Now.Ticks
        Static oldProcTime As Long = Me.Infos.ProcessorTime

        Dim currDate As Long = Date.Now.Ticks
        Dim proctime As Long = Me.Infos.ProcessorTime

        Dim diff As Long = currDate - oldDate
        Dim procDiff As Long = proctime - oldProcTime

        oldProcTime = proctime
        oldDate = currDate

        If diff > 0 AndAlso _processors > 0 Then
            _cpuUsage = procDiff / diff / _processors
        Else
            _cpuUsage = 0
        End If
    End Sub

    ' Refresh IO delta once
    Private Sub refreshIODelta()
        Static oldDate As Long = Date.Now.Ticks
        Static oldIO As Native.Api.NativeStructs.IoCounters = Me.Infos.IOValues

        Dim currDate As Long = Date.Now.Ticks
        Dim ioValues As Native.Api.NativeStructs.IoCounters = Me.Infos.IOValues

        Dim diff As Long = currDate - oldDate
        Dim ioDiff As Native.Api.NativeStructs.IoCounters
        With ioDiff
            .OtherOperationCount = ioValues.OtherOperationCount - oldIO.OtherOperationCount
            .OtherTransferCount = ioValues.OtherTransferCount - oldIO.OtherTransferCount
            .ReadOperationCount = ioValues.ReadOperationCount - oldIO.ReadOperationCount
            .ReadTransferCount = ioValues.ReadTransferCount - oldIO.ReadTransferCount
            .WriteOperationCount = ioValues.WriteOperationCount - oldIO.WriteOperationCount
            .WriteTransferCount = ioValues.WriteTransferCount - oldIO.WriteTransferCount
        End With

        oldIO = ioValues
        oldDate = currDate

        If diff > 0 Then
            With _ioDelta
                .OtherOperationCount = CULng(ioDiff.OtherOperationCount)
                .OtherTransferCount = CULng((10000000 / diff) * ioDiff.OtherTransferCount)
                .ReadOperationCount = CULng(ioDiff.ReadOperationCount)
                .ReadTransferCount = CULng((10000000 / diff) * ioDiff.ReadTransferCount)
                .WriteOperationCount = CULng(ioDiff.WriteOperationCount)
                .WriteTransferCount = CULng((10000000 / diff) * ioDiff.WriteTransferCount)
            End With
        Else
            With _ioDelta
                .OtherOperationCount = 0
                .OtherTransferCount = 0
                .ReadOperationCount = 0
                .ReadTransferCount = 0
                .WriteOperationCount = 0
                .WriteTransferCount = 0
            End With
        End If
    End Sub

#Region "Highlightings management"

    ' Set highlightings
    Public Shared Sub SetHighlightings(ByVal debug As Boolean, ByVal job As Boolean, _
                                       ByVal elev As Boolean, ByVal critic As Boolean, _
                                       ByVal owned As Boolean, ByVal system As Boolean, _
                                       ByVal service As Boolean)
        _hlProcessBeingDebugged = debug
        _hlProcessCritical = critic
        _hlProcessElevated = elev
        _hlProcessInJob = job
        _hlProcessOwned = owned
        _hlProcessService = service
        _hlProcessSystem = system
    End Sub
    Public Shared Sub SetHighlightingsColor(ByVal debug As Color, ByVal job As Color, _
                                       ByVal elev As Color, ByVal critic As Color, _
                                       ByVal owned As Color, ByVal system As Color, _
                                       ByVal service As Color)
        _hlProcessBeingDebuggedColor = debug
        _hlProcessCriticalColor = critic
        _hlProcessElevatedColor = elev
        _hlProcessInJobColor = job
        _hlProcessOwnedColor = owned
        _hlProcessServiceColor = service
        _hlProcessSystemColor = system
    End Sub

    ' Return backcolor
    Public Overrides Function GetBackColor() As System.Drawing.Color
        If _hlProcessCritical AndAlso Me.IsCriticalProcess Then
            Return _hlProcessCriticalColor
        ElseIf _hlProcessBeingDebugged AndAlso Me.IsBeingDebugged Then
            Return _hlProcessBeingDebuggedColor
        ElseIf _hlProcessElevated AndAlso Me.ElevationType = Native.Api.Enums.ElevationType.Full Then
            Return _hlProcessElevatedColor
        ElseIf _hlProcessInJob AndAlso Me.IsInJob Then
            Return _hlProcessInJobColor
        ElseIf _hlProcessService AndAlso Me.IsServiceProcess Then
            Return _hlProcessServiceColor
        ElseIf _hlProcessOwned AndAlso Me.IsOwnedProcess Then
            Return _hlProcessOwnedColor
        ElseIf _hlProcessSystem AndAlso Me.IsSystemProcess Then
            Return _hlProcessSystemColor
        End If
        Return MyBase.GetBackColor()
    End Function

    ' Return forecolor
    Public Overrides Function GetForeColor() As System.Drawing.Color
        If Me.HaveFullControl OrElse _
                    Program.Connection.Type <> _
                    cConnection.TypeOfConnection.LocalConnection Then
            Return NON_BLACK_COLOR
        Else
            Return Drawing.Color.Gray
        End If
    End Function

#End Region

End Class
