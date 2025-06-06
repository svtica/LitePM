﻿
Option Strict On

Imports System.Xml
Imports System.Xml.Serialization
Imports System.IO

<Serializable()> _
Public Class cSnapshot

#Region "Private attributes"

    ' List of processes
    Private _processes As New Dictionary(Of String, processInfos)

    ' List of services
    Private _services As New Dictionary(Of String, serviceInfos)

    ' List of network connections 
    Private _networkConnections As New Dictionary(Of String, networkInfos)

    ' List of jobs
    Private _jobs As New Dictionary(Of String, jobInfos)

    ' List of tasks
    Private _tasks As New Dictionary(Of String, windowInfos)

    ' List of modules (PID <-> Dico)
    Private _modules As New Dictionary(Of Integer, Dictionary(Of String, moduleInfos))

    ' List of windows (PID <-> Dico)
    Private _windows As New Dictionary(Of Integer, Dictionary(Of String, windowInfos))

    ' List of threads (PID <-> Dico)
    Private _threads As New Dictionary(Of Integer, Dictionary(Of String, threadInfos))

    ' List of privileges (PID <-> Dico)
    Private _privileges As New Dictionary(Of Integer, Dictionary(Of String, privilegeInfos))

    ' List of memory regions (PID <-> Dico)
    Private _memRegions As New Dictionary(Of Integer, Dictionary(Of String, memRegionInfos))

    ' List of job limits (jobName <-> Dico)
    Private _jobLimits As New Dictionary(Of String, Dictionary(Of String, jobLimitInfos))

    ' List of heaps (PID <-> Dico)
    Private _heaps As New Dictionary(Of Integer, Dictionary(Of String, heapInfos))

    ' List of envvariables (PID <-> Dico)
    Private _envV As New Dictionary(Of Integer, Dictionary(Of String, envVariableInfos))

    ' List of handles (PID <-> Dico)
    Private _handles As New Dictionary(Of Integer, Dictionary(Of String, handleInfos))

    ' Version of file type
    Private _fileVersion As String

    ' Date of snapshot
    Private _date As Date

    ' System infos
    Private _infos As SnapshotSystemInfo

#End Region

#Region "Constructors & destructor"

    Public Sub New(ByRef ssFile As String)
        ' Build from file

        ' Serialization part :
        Dim b() As Byte = System.IO.File.ReadAllBytes(ssFile)
        Dim newObj As cSnapshot = cSerialization.DeserializeObject(Of cSnapshot)(b)

        ' Set objects
        With newObj
            Me.Processes = .Processes
            Me.Services = .Services
            Me.NetworkConnections = .NetworkConnections
            Me.Jobs = .Jobs
            Me.Tasks = .Tasks
            Me.Modules = .Modules
            Me.Windows = .Windows
            Me.Threads = .Threads
            Me.Privileges = .Privileges
            Me.MemoryRegions = .MemoryRegions
            Me.JobLimits = .JobLimits
            Me.Heaps = .Heaps
            Me.EnvironnementVariables = .EnvironnementVariables
            Me.Handles = .Handles
            Me.FileVersion = .FileVersion
            Me.Date = .Date
            Me.SystemInformation = .SystemInformation
        End With

    End Sub

    Public Sub New()
        ' Empty snapshot file
    End Sub

    Public Overrides Function ToString() As String
        Return "Snapshot, computer : " & Me.SystemInformation.ComputerName & ", date : " & Me.Date.ToLongDateString & "-" & Me.Date.ToLongTimeString
    End Function

#End Region

#Region "Properties about the snapshot"

    ' File type version
    Public Property FileVersion() As String
        Get
            Return _fileVersion
        End Get
        Set(ByVal value As String)
            _fileVersion = value
        End Set
    End Property

    ' System info
    Public Property SystemInformation() As SnapshotSystemInfo
        Get
            Return _infos
        End Get
        Set(ByVal value As SnapshotSystemInfo)
            _infos = value
        End Set
    End Property

    ' File date
    Public Property [Date]() As Date
        Get
            Return _date
        End Get
        Set(ByVal value As Date)
            _date = value
        End Set
    End Property

#End Region

#Region "Other properties"

    ' List of processes
    Public Property Processes() As Dictionary(Of String, processInfos)
        Get
            Return _processes
        End Get
        Set(ByVal value As Dictionary(Of String, processInfos))
            _processes = value
        End Set
    End Property

    ' List of services
    Public Property Services() As Dictionary(Of String, serviceInfos)
        Get
            Return _services
        End Get
        Set(ByVal value As Dictionary(Of String, serviceInfos))
            _services = value
        End Set
    End Property
    Public ReadOnly Property ServicesByProcessId(ByVal processId As Integer) As Dictionary(Of String, serviceInfos)
        Get
            Dim ret As New Dictionary(Of String, serviceInfos)
            If Me.Services IsNot Nothing Then
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, serviceInfos) In Me.Services
                    If pair.Value.ProcessId = processId Then
                        ret.Add(pair.Key, pair.Value)
                    End If
                Next
            End If
            Return ret
        End Get
    End Property

    ' List of network connections
    Public Property NetworkConnections() As Dictionary(Of String, networkInfos)
        Get
            Return _networkConnections
        End Get
        Set(ByVal value As Dictionary(Of String, networkInfos))
            _networkConnections = value
        End Set
    End Property
    Public ReadOnly Property NetworkConnectionsByProcessId(ByVal processId As Integer) As Dictionary(Of String, networkInfos)
        Get
            Dim ret As New Dictionary(Of String, networkInfos)
            If Me.NetworkConnections IsNot Nothing Then
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, networkInfos) In Me.NetworkConnections
                    If pair.Value.ProcessId = processId Then
                        ret.Add(pair.Key, pair.Value)
                    End If
                Next
            End If
            Return ret
        End Get
    End Property

    ' List of jobs
    Public Property Jobs() As Dictionary(Of String, jobInfos)
        Get
            Return _jobs
        End Get
        Set(ByVal value As Dictionary(Of String, jobInfos))
            _jobs = value
        End Set
    End Property

    ' List of tasks
    Public Property Tasks() As Dictionary(Of String, windowInfos)
        Get
            Return _tasks
        End Get
        Set(ByVal value As Dictionary(Of String, windowInfos))
            _tasks = value
        End Set
    End Property

    ' List of modules
    Public Property ModulesByProcessId(ByVal processId As Integer) As Dictionary(Of String, moduleInfos)
        Get
            If _modules.ContainsKey(processId) Then
                Return _modules(processId)
            Else
                Return New Dictionary(Of String, moduleInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, moduleInfos))
            If _modules.ContainsKey(processId) Then
                _modules(processId) = value
            Else
                _modules.Add(processId, value)
            End If
        End Set
    End Property

    ' List of windows
    Public Property WindowsByProcessId(ByVal processId As Integer) As Dictionary(Of String, windowInfos)
        Get
            If _windows.ContainsKey(processId) Then
                Return _windows(processId)
            Else
                Return New Dictionary(Of String, windowInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, windowInfos))
            If _windows.ContainsKey(processId) Then
                _windows(processId) = value
            Else
                _windows.Add(processId, value)
            End If
        End Set
    End Property

    ' List of threads
    Public Property ThreadsByProcessId(ByVal processId As Integer) As Dictionary(Of String, threadInfos)
        Get
            If _threads.ContainsKey(processId) Then
                Return _threads(processId)
            Else
                Return New Dictionary(Of String, threadInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, threadInfos))
            If _threads.ContainsKey(processId) Then
                _threads(processId) = value
            Else
                _threads.Add(processId, value)
            End If
        End Set
    End Property

    ' List of privileges
    Public Property PrivilegesByProcessId(ByVal processId As Integer) As Dictionary(Of String, privilegeInfos)
        Get
            If _privileges.ContainsKey(processId) Then
                Return _privileges(processId)
            Else
                Return New Dictionary(Of String, privilegeInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, privilegeInfos))
            If _privileges.ContainsKey(processId) Then
                _privileges(processId) = value
            Else
                _privileges.Add(processId, value)
            End If
        End Set
    End Property

    ' List of mem regions
    Public Property MemoryRegionsByProcessId(ByVal processId As Integer) As Dictionary(Of String, memRegionInfos)
        Get
            If _memRegions.ContainsKey(processId) Then
                Return _memRegions(processId)
            Else
                Return New Dictionary(Of String, memRegionInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, memRegionInfos))
            If _memRegions.ContainsKey(processId) Then
                _memRegions(processId) = value
            Else
                _memRegions.Add(processId, value)
            End If
        End Set
    End Property

    ' List of job limits
    Public Property JobLimitsByJobName(ByVal jobName As String) As Dictionary(Of String, jobLimitInfos)
        Get
            If _jobLimits.ContainsKey(jobName) Then
                Return _jobLimits(jobName)
            Else
                Return New Dictionary(Of String, jobLimitInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, jobLimitInfos))
            If _jobLimits.ContainsKey(jobName) Then
                _jobLimits(jobName) = value
            Else
                _jobLimits.Add(jobName, value)
            End If
        End Set
    End Property

    ' List of heaps
    Public Property HeapsByProcessId(ByVal processId As Integer) As Dictionary(Of String, heapInfos)
        Get
            If _heaps.ContainsKey(processId) Then
                Return _heaps(processId)
            Else
                Return New Dictionary(Of String, heapInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, heapInfos))
            If _heaps.ContainsKey(processId) Then
                _heaps(processId) = value
            Else
                _heaps.Add(processId, value)
            End If
        End Set
    End Property

    ' List of handles
    Public Property HandlesByProcessId(ByVal processId As Integer) As Dictionary(Of String, handleInfos)
        Get
            If _handles.ContainsKey(processId) Then
                Return _handles(processId)
            Else
                Return New Dictionary(Of String, handleInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, handleInfos))
            If _handles.ContainsKey(processId) Then
                _handles(processId) = value
            Else
                _handles.Add(processId, value)
            End If
        End Set
    End Property

    ' List of env variables
    Public Property EnvironnementVariablesByProcessId(ByVal processId As Integer) As Dictionary(Of String, envVariableInfos)
        Get
            If _envV.ContainsKey(processId) Then
                Return _envV(processId)
            Else
                Return New Dictionary(Of String, envVariableInfos)
            End If
        End Get
        Set(ByVal value As Dictionary(Of String, envVariableInfos))
            If _envV.ContainsKey(processId) Then
                _envV(processId) = value
            Else
                _envV.Add(processId, value)
            End If
        End Set
    End Property

#End Region

#Region "Private properties"

    ' List of modules
    Private Property Modules() As Dictionary(Of Integer, Dictionary(Of String, moduleInfos))
        Get
            Return _modules
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, moduleInfos)))
            _modules = value
        End Set
    End Property

    ' List of windows
    Private Property Windows() As Dictionary(Of Integer, Dictionary(Of String, windowInfos))
        Get
            Return _windows
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, windowInfos)))
            _windows = value
        End Set
    End Property

    ' List of threads
    Private Property Threads() As Dictionary(Of Integer, Dictionary(Of String, threadInfos))
        Get
            Return _threads
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, threadInfos)))
            _threads = value
        End Set
    End Property

    ' List of privileges
    Private Property Privileges() As Dictionary(Of Integer, Dictionary(Of String, privilegeInfos))
        Get
            Return _privileges
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, privilegeInfos)))
            _privileges = value
        End Set
    End Property

    ' List of mem regions
    Private Property MemoryRegions() As Dictionary(Of Integer, Dictionary(Of String, memRegionInfos))
        Get
            Return _memRegions
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, memRegionInfos)))
            _memRegions = value
        End Set
    End Property

    ' List of mem regions
    Private Property JobLimits() As Dictionary(Of String, Dictionary(Of String, jobLimitInfos))
        Get
            Return _jobLimits
        End Get
        Set(ByVal value As Dictionary(Of String, Dictionary(Of String, jobLimitInfos)))
            _jobLimits = value
        End Set
    End Property

    ' List of heaps
    Private Property Heaps() As Dictionary(Of Integer, Dictionary(Of String, heapInfos))
        Get
            Return _heaps
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, heapInfos)))
            _heaps = value
        End Set
    End Property

    ' List of handles
    Private Property [Handles]() As Dictionary(Of Integer, Dictionary(Of String, handleInfos))
        Get
            Return _handles
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, handleInfos)))
            _handles = value
        End Set
    End Property

    ' List of env variables
    Private Property EnvironnementVariables() As Dictionary(Of Integer, Dictionary(Of String, envVariableInfos))
        Get
            Return _envV
        End Get
        Set(ByVal value As Dictionary(Of Integer, Dictionary(Of String, envVariableInfos)))
            _envV = value
        End Set
    End Property

#End Region

#Region "All actions on snapshot"

    ' Save the snapshot as SSFile
    Public Function SaveSnapshot(ByVal ssFile As String, ByRef msg As String) As Boolean

        ' Create empty new file
        Try
            System.IO.File.Open(ssFile, IO.FileMode.Create).Close()
        Catch ex As Exception
            ' Could not create file
            msg = ex.Message
            Return False
        End Try

        ' Serialize current instance
        Dim b() As Byte
        Try
            b = cSerialization.GetSerializedObject(Me)
        Catch ex As Exception
            ' Could not serialize as XML file
            msg = ex.Message
            Misc.ShowDebugError(ex)
            Return False
        End Try

        ' Create file
        Try
            System.IO.File.WriteAllBytes(ssFile, b)
        Catch ex As Exception
            ' Could not create file
            msg = ex.Message
            Return False
        End Try

        ' If we're here, evrything is OK
        Return True

    End Function

    ' Create the snapshot
    Public Function CreateSnapshot(ByVal connection As cConnection, ByVal options As Native.Api.Enums.SnapshotObject, ByRef msg As String) As Boolean
        Dim b As Boolean = True
        Select Case connection.ConnectionType
            Case cConnection.TypeOfConnection.LocalConnection
                b = Me.LocalBuild(msg, options)
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                b = Me.SocketBuild(msg, options)
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                b = Me.WmiBuild(msg, options)
            Case cConnection.TypeOfConnection.SnapshotFile
                ' Nothing here
            Case Else
                ' Nothing here
        End Select

        Return b
    End Function

#End Region

#Region "Private functions"

    ' Local building
    Private Function LocalBuild(ByRef msg As String, ByVal options As Native.Api.Enums.SnapshotObject) As Boolean

        Try

            ' Informations about system & ssfile
            Me.Date = System.DateTime.Now
            Me.FileVersion = My.Application.Info.Version.ToString
            Me.SystemInformation = New SnapshotSystemInfo(Program.Connection)


            ' Processes
            ' We HAVE to get the list cause some other informations depend on it
            Me.Processes = asyncCallbackProcEnumerate.SharedLocalSyncEnumerate(True, New asyncCallbackProcEnumerate.poolObj(0, asyncCallbackProcEnumerate.ProcessEnumMethode.VisibleProcesses))

            ' Services
            If (options And Native.Api.Enums.SnapshotObject.[Services]) = Native.Api.Enums.SnapshotObject.[Services] Then
                Me.Services = asyncCallbackServiceEnumerate.SharedLocalSyncEnumerate(New asyncCallbackServiceEnumerate.poolObj(0, True, True, 0), _frmMain.lvServices.ServiceConnection)
            End If

            ' Network connections
            If (options And Native.Api.Enums.SnapshotObject.[NetworkConnections]) = Native.Api.Enums.SnapshotObject.[NetworkConnections] Then
                Me.NetworkConnections = asyncCallbackNetworkEnumerate.SharedLocalSyncEnumerate(New asyncCallbackNetworkEnumerate.poolObj(Nothing, True, 0))
            End If

            ' Jobs
            If (options And Native.Api.Enums.SnapshotObject.[Jobs]) = Native.Api.Enums.SnapshotObject.[Jobs] Then
                Me.Jobs = asyncCallbackJobEnumerate.SharedLocalSyncEnumerate
            End If

            ' Tasks
            If (options And Native.Api.Enums.SnapshotObject.[Tasks]) = Native.Api.Enums.SnapshotObject.[Tasks] Then
                Me.Tasks = asyncCallbackTaskEnumerate.SharedLocalSyncEnumerate
            End If

            ' Modules
            If (options And Native.Api.Enums.SnapshotObject.[Modules]) = Native.Api.Enums.SnapshotObject.[Modules] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim _dico As Dictionary(Of String, moduleInfos) = asyncCallbackModuleEnumerate.SharedLocalSyncEnumerate(New asyncCallbackModuleEnumerate.poolObj(proc.ProcessId, 0))
                    Me.ModulesByProcessId(proc.ProcessId) = _dico
                Next
            End If

            ' Windows
            If (options And Native.Api.Enums.SnapshotObject.[Windows]) = Native.Api.Enums.SnapshotObject.[Windows] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim _dico As Dictionary(Of String, windowInfos) = asyncCallbackWindowEnumerate.SharedLocalSyncEnumerate(New asyncCallbackWindowEnumerate.poolObj(proc.ProcessId, False, True, 0))
                    Me.WindowsByProcessId(proc.ProcessId) = _dico
                Next
            End If

            ' Threads
            If (options And Native.Api.Enums.SnapshotObject.[Threads]) = Native.Api.Enums.SnapshotObject.[Threads] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim _dico As Dictionary(Of String, threadInfos) = asyncCallbackThreadEnumerate.SharedLocalSyncEnumerate(New asyncCallbackThreadEnumerate.poolObj(proc.ProcessId, 0))
                    Me.ThreadsByProcessId(proc.ProcessId) = _dico
                Next
            End If

            ' Privileges
            If (options And Native.Api.Enums.SnapshotObject.[Privileges]) = Native.Api.Enums.SnapshotObject.[Privileges] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim pid As Integer = proc.ProcessId
                    Dim _dico As Dictionary(Of String, privilegeInfos) = asyncCallbackPrivilegesEnumerate.SharedLocalSyncEnumerate(New asyncCallbackPrivilegesEnumerate.poolObj(pid, 0))
                    Me.PrivilegesByProcessId(pid) = _dico
                Next
            End If

            ' Memory regions
            If (options And Native.Api.Enums.SnapshotObject.[MemoryRegions]) = Native.Api.Enums.SnapshotObject.[MemoryRegions] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim pid As Integer = proc.ProcessId
                    Dim _dico As Dictionary(Of String, memRegionInfos) = asyncCallbackMemRegionEnumerate.SharedLocalSyncEnumerate(New asyncCallbackMemRegionEnumerate.poolObj(pid, 0))
                    Me.MemoryRegionsByProcessId(pid) = _dico
                Next
            End If

            ' Job limits
            If (options And Native.Api.Enums.SnapshotObject.[JobLimits]) = Native.Api.Enums.SnapshotObject.[JobLimits] Then
                For Each job As jobInfos In Me.Jobs.Values
                    Dim name As String = job.Name
                    Dim _dico As Dictionary(Of String, jobLimitInfos) = asyncCallbackJobLimitsEnumerate.SharedLocalSyncEnumerate(New asyncCallbackJobLimitsEnumerate.poolObj(name, 0))
                    Me.JobLimitsByJobName(name) = _dico
                Next
            End If

            ' Heaps
            ' TODO (have to fix heap enumeration before implenting it)
            If (options And Native.Api.Enums.SnapshotObject.[Heaps]) = Native.Api.Enums.SnapshotObject.[Heaps] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim pid As Integer = proc.ProcessId
                    If pid > 0 Then
                        'Dim _dico As Dictionary(Of String, heapInfos) = asyncCallbackHeapEnumerate.SharedLocalSyncEnumerate(New asyncCallbackHeapEnumerate.poolObj(pid, 0))
                        'Me.HeapsByProcessId(pid) = _dico
                    End If
                Next
            End If

            ' Envvariables
            If (options And Native.Api.Enums.SnapshotObject.[EnvironmentVariables]) = Native.Api.Enums.SnapshotObject.[EnvironmentVariables] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim pid As Integer = proc.ProcessId
                    Using reader As New ProcessMemReader(pid)
                        Dim peb As IntPtr = reader.GetPebAddress
                        Dim _dico As Dictionary(Of String, envVariableInfos) = asyncCallbackEnvVariableEnumerate.SharedLocalSyncEnumerate(New asyncCallbackEnvVariableEnumerate.poolObj(pid, peb, 0))
                        Me.EnvironnementVariablesByProcessId(pid) = _dico
                    End Using
                Next
            End If

            ' Handles
            If (options And Native.Api.Enums.SnapshotObject.[Handles]) = Native.Api.Enums.SnapshotObject.[Handles] Then
                For Each proc As processInfos In Me.Processes.Values
                    Dim _dico As Dictionary(Of String, handleInfos) = asyncCallbackHandleEnumerate.SharedLocalSyncEnumerate(New asyncCallbackHandleEnumerate.poolObj(proc.ProcessId, True, 0))
                    Me.HandlesByProcessId(proc.ProcessId) = _dico
                Next
            End If

        Catch ex As Exception
            msg = ex.Message
            Misc.ShowDebugError(ex)
            Return False
        End Try

        Return True

    End Function


    ' WMI building
    Private Function WmiBuild(ByRef msg As String, ByVal options As Native.Api.Enums.SnapshotObject) As Boolean

        Try
            ' Processes

            ' Services

        Catch ex As Exception
            msg = ex.Message
            Misc.ShowDebugError(ex)
            Return False
        End Try

        Return True
    End Function


    ' Remote server building
    Private Function SocketBuild(ByRef msg As String, ByVal options As Native.Api.Enums.SnapshotObject) As Boolean

        Try
            ' Processes

            ' Services

        Catch ex As Exception
            msg = ex.Message
            Misc.ShowDebugError(ex)
            Return False
        End Try

        Return True
    End Function

#End Region

End Class
