﻿
' Lite Process Monitor


Option Strict On

Imports System.Net
Imports Common.Misc


Public Class frmServer

    Private WithEvents sock As New AsynchronousSocketListener
    Private _state As SOCK_STATE = SOCK_STATE.Disconnected

    Private Enum SOCK_STATE As Integer
        Connected
        WaitingConnection
        Disconnected
    End Enum

    Private theConnection As cConnection = Program.Connection
    Private _servdepCon As New cServDepConnection(Me, theConnection, New cServDepConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedServDep))
    Private _searchCon As New cSearchConnection(Me, theConnection, New cSearchConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedSearch))

    ' Connect to local machine
    Private Sub connectLocal()

        ' Set connection
        Try
            With theConnection
                .Type = cConnection.TypeOfConnection.LocalConnection
                .Connect()
            End With

            _searchCon.ConnectionObj = theConnection
            _servdepCon.ConnectionObj = theConnection

            _searchCon.Connect()
            _servdepCon.Connect()

        Catch ex As Exception
            Misc.ShowError(ex, "Unable to connect")
        End Try

    End Sub

#Region "Has enumerated lists"

    Private _TheIdToSend As String = ""
    Private Sub HasEnumeratedEnvVar(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, envVariableInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestEnvironmentVariableList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetEnvVarList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate environnement variables")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate environnement variables : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedHeaps(ByVal news As List(Of String), ByVal dels As List(Of String), ByVal Dico As Dictionary(Of String, heapInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestHeapList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetHeapList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate heap list")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate heap list : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedJobLimits(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, jobLimitInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestJobLimits)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetJobLimitsList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate job limits")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate job limits")
        End If

    End Sub

    Private Sub HasEnumeratedJobs(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, jobInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestJobList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetJobList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate jobs")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate jobs : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedLog(ByVal _dicoNew As List(Of String), ByVal _dicoDels As Dictionary(Of String, logItemInfos), ByVal _dicoDel As List(Of String), ByVal Dico As Dictionary(Of String, logItemInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestLogList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetLogList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate log items")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate log items : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedServDep(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String, ByVal instanceId As Integer, ByVal type As cServDepConnection.DependenciesToget)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestServDepList, Nothing, type)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetServiceList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate service dependencies")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate service dependencies")
        End If

    End Sub

    Private Sub HasEnumeratedMemoryReg(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, memRegionInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestMemoryRegionList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetMemoryRegList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate memory regions")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate memory regions : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedProcess(ByVal newPids As List(Of Integer), ByVal delPids As List(Of Integer), ByVal Dico As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestProcessList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetProcessList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate processes")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate processes : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedPrivilege(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, privilegeInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestPrivilegesList)
                cDat.InstanceId = instanceId   ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetPrivilegeList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate privileges")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate privileges : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedService(ByVal newNames As List(Of String), ByVal delServices As List(Of String), ByVal Dico As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestServiceList)
                cDat.InstanceId = instanceId
                cDat._id = _TheIdToSend
                cDat.SetServiceList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate services")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate services : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedThread(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, threadInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestThreadList)
                cDat.InstanceId = instanceId    ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetThreadList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate threads")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate threads : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedModule(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, moduleInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestModuleList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetModuleList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate modules")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate modules : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedHandle(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, handleInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestHandleList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetHandleList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate handles")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate handles : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedNetwork(ByVal news As List(Of String), ByVal dels As List(Of String), ByVal Dico As Dictionary(Of String, networkInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestNetworkConnectionList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetNetworkList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate network connections")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate network connections : " & res.ErrorMessage)
        End If

    End Sub

    Private Sub HasEnumeratedSearch(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, searchInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestSearchList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetSearchList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to search the string")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to search the string")
        End If

    End Sub

    Private Sub HasEnumeratedWindows(ByVal newNames As List(Of String), ByVal delVars As List(Of String), ByVal Dico As Dictionary(Of String, windowInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

        If Program.Parameters.ModeServer = False Then
            Exit Sub
        End If

        If res.Success Then
            Try
                Dim cDat As New cSocketData(cSocketData.DataType.RequestedList, cSocketData.OrderType.RequestWindowList)
                cDat.InstanceId = instanceId  ' The instance which requested the list
                cDat._id = _TheIdToSend
                cDat.SetWindowsList(Dico)
                sock.Send(cDat)
            Catch ex As Exception
                Misc.ShowError(ex, "Unable to enumerate windows")
            End Try
        Else
            ' Send an error
            Misc.ShowError("Unable to enumerate windows : " & res.ErrorMessage)
        End If

    End Sub
#End Region

    Private Sub frmServeur_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Try
        sock.Disconnect()
        'Catch ex As Exception
        '    '
        'End Try
    End Sub

    Private Delegate Sub ChangeConnectState(ByVal state As SOCK_STATE)
    Private Sub handlerChangeConnectState(ByVal state As SOCK_STATE)
#If RELEASE_MODE Then
        'Select Case state
        '    Case SOCK_STATE.Connected
        '        Me.cmdConnection.Text = "Disconnect !"
        '        Me.Text = "LitePM remote process (connected)"
        '    Case SOCK_STATE.Disconnected
        '        Me.cmdConnection.Text = "Connect !"
        '        Me.Text = "LitePM remote process (disconnected)"
        '    Case SOCK_STATE.WaitingConnection
        '        Me.cmdConnection.Text = "Disconnect !"
        '        Me.Text = "LitePM remote process (waiting for client to connect...)"
        'End Select
#End If
    End Sub
    Private Sub sock_ConnexionAccepted() Handles sock.Connected
        _state = SOCK_STATE.Connected
        Dim h As New ChangeConnectState(AddressOf handlerChangeConnectState)
        h.Invoke(SOCK_STATE.Connected)
    End Sub
    Private Sub sock_Disconnected() Handles sock.Disconnected
        _state = SOCK_STATE.Disconnected
        Dim h As New ChangeConnectState(AddressOf handlerChangeConnectState)
        h.Invoke(SOCK_STATE.Disconnected)
    End Sub
    Private Sub sock_Waiting() Handles sock.WaitingForConnection
        _state = SOCK_STATE.WaitingConnection
        Dim h As New ChangeConnectState(AddressOf handlerChangeConnectState)
        h.Invoke(SOCK_STATE.WaitingConnection)
    End Sub

    Private Sub sock_ReceivedData(ByRef cData As cSocketData) Handles sock.ReceivedData
        Try

            Dim ret As Boolean = True       ' Return for the functions (orders)

            If cData Is Nothing Then
                Trace.WriteLine("Serialization error")
                Exit Sub
            End If

            Dim _forInstanceId As Integer = cData.InstanceId
            Dim _idToSend As String = cData._id
            _TheIdToSend = _idToSend

            ' Add item to history
            Me.Invoke(New addItemHandler(AddressOf addItem), cData)

            ' Extract the type of information we have to send
            If cData.Type = cSocketData.DataType.Order Then

                ' ===== Request lists and informations
                Select Case cData.Order
                    Case cSocketData.OrderType.RequestProcessList
                        Call ProcessProvider.Update(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestNetworkConnectionList
                        Call NetworkConnectionsProvider.Update(_forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestJobList
                        Call JobProvider.Update(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestJobLimits
                        Dim name As String = CStr(cData.Param1)
                        Call JobLimitsProvider.Update(name, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestServiceList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim all As Boolean = CBool(cData.Param2)
                        Call ServiceProvider.Update(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestModuleList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call ModuleProvider.Update(pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestThreadList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call ThreadProvider.Update(pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestHandleList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call HandleProvider.Update(pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestWindowList
                        Call WindowProvider.Update(True, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestSearchList
                        Dim st As String = CStr(cData.Param1)
                        Dim include As Native.Api.Enums.GeneralObjectType = CType(cData.Param2, Native.Api.Enums.GeneralObjectType)
                        Dim _case As Boolean = CBool(cData.Param3)
                        Call _searchCon.Enumerate(st, _case, include, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestPrivilegesList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call PrivilegeProvider.Update(pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestEnvironmentVariableList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim peb As IntPtr = CType(cData.Param2, IntPtr)
                        Call EnvVariableProvider.Update(pid, peb, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestMemoryRegionList
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Call MemRegionProvider.Update(pid, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestServDepList
                        Dim name As String = CStr(cData.Param1)
                        Dim type As cServDepConnection.DependenciesToget = CType(cData.Param2, cServDepConnection.DependenciesToget)
                        Call _servdepCon.Enumerate(name, type, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestProcessorCount
                        Dim procCount As Integer = cSystemInfo.GetProcessorCount
                        Try
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ReturnProcessorCount, procCount)
                            cDat._id = _idToSend
                            sock.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not request processor count")
                        End Try
                        Exit Sub
                    Case cSocketData.OrderType.RequestLogList
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim infos As Native.Api.Enums.LogItemType = CType(cData.Param2, Native.Api.Enums.LogItemType)
                        Call LogProvider.Update(pid, infos, _forInstanceId)
                        Exit Sub
                    Case cSocketData.OrderType.RequestHeapList
                        Dim pid As Integer = CInt(cData.Param1)
                        Call HeapProvider.Update(pid, _forInstanceId)
                        Exit Sub
                End Select



                ' ===== Process functions
                Select Case cData.Order
                    Case cSocketData.OrderType.ProcessCreateNew
                        Try
                            Dim s As String = CStr(cData.Param1)
                            Dim pid As Integer = Shell(s, AppWinStyle.NormalFocus)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not create a new process")
                        End Try
                    Case cSocketData.OrderType.ProcessReanalize
                        ProcessProvider.RemoveProcessesFromListOfNewProcesses(CType(cData.Param1, Integer()))
                    Case cSocketData.OrderType.ProcessChangeAffinity
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim aff As Integer = CType(cData.Param2, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).SetAffinity(aff)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process affinity")
                        End Try
                    Case cSocketData.OrderType.ProcessChangePriority
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim level As System.Diagnostics.ProcessPriorityClass = CType(cData.Param2, ProcessPriorityClass)
                        Try
                            ProcessProvider.GetProcessById(pid).SetPriority(level)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process priority")
                        End Try
                    Case cSocketData.OrderType.ProcessDecreasePriority
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).DecreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process priority")
                        End Try
                    Case cSocketData.OrderType.ProcessIncreasePriority
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).IncreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change process priority")
                        End Try
                    Case cSocketData.OrderType.ProcessKill
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).Kill()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not kill process")
                        End Try
                    Case cSocketData.OrderType.ProcessKillByMethod
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim method As Native.Api.Enums.KillMethod = CType(cData.Param2, Native.Api.Enums.KillMethod)
                        Try
                            ProcessProvider.GetProcessById(pid).KillByMethod(method)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not kill process by method")
                        End Try
                    Case cSocketData.OrderType.ProcessKillTree
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).KillProcessTree()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not kill process tree")
                        End Try
                    Case cSocketData.OrderType.ProcessReduceWorkingSet
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).EmptyWorkingSetSize()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not reduce process' working set size")
                        End Try
                    Case cSocketData.OrderType.ProcessResume
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).ResumeProcess()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not resume process")
                        End Try
                    Case cSocketData.OrderType.ProcessSuspend
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Try
                            ProcessProvider.GetProcessById(pid).SuspendProcess()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not suspend process")
                        End Try
                End Select



                ' ===== Windows functions
                Select Case cData.Order
                    Case cSocketData.OrderType.WindowBringToFront
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim value As Boolean = CBool(cData.Param2)
                        Call (New cWindow(hWnd)).BringToFront(value)
                    Case cSocketData.OrderType.WindowClose
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Close()
                    Case cSocketData.OrderType.WindowDisable
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Enabled = False
                    Case cSocketData.OrderType.WindowEnable
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Enabled = True
                    Case cSocketData.OrderType.WindowFlash
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Flash()
                    Case cSocketData.OrderType.WindowHide
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Hide()
                    Case cSocketData.OrderType.WindowMaximize
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Maximize()
                    Case cSocketData.OrderType.WindowMinimize
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Minimize()
                    Case cSocketData.OrderType.WindowSetAsActiveWindow
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).SetAsActiveWindow()
                    Case cSocketData.OrderType.WindowSetAsForegroundWindow
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).SetAsForegroundWindow()
                    Case cSocketData.OrderType.WindowSetCaption
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim s As String = CStr(cData.Param2)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Caption = s
                    Case cSocketData.OrderType.WindowSetOpacity
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim o As Byte = CByte(cData.Param2)
                        Dim w As cWindow = New cWindow(hWnd)
                        w.Opacity = o
                    Case cSocketData.OrderType.WindowSetPositions
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Dim r As Native.Api.NativeStructs.Rect = CType(cData.Param2, Native.Api.NativeStructs.Rect)
                        Call (New cWindow(hWnd)).SetPositions(r)
                    Case cSocketData.OrderType.WindowShow
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).Show()
                    Case cSocketData.OrderType.WindowStopFlashing
                        Dim hWnd As IntPtr = CType(cData.Param1, IntPtr)
                        Call (New cWindow(hWnd)).StopFlashing()
                End Select



                ' ===== Service functions
                Select Case cData.Order
                    Case cSocketData.OrderType.ServiceDelete
                        Dim name As String = CStr(cData.Param1)
                        Try
                            ServiceProvider.GetServiceByName(name).DeleteService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not delete service")
                        End Try
                    Case cSocketData.OrderType.ServicePause
                        Dim name As String = CStr(cData.Param1)
                        Try
                            ServiceProvider.GetServiceByName(name).PauseService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not pause service")
                        End Try
                    Case cSocketData.OrderType.ServiceChangeServiceStartType
                        Dim name As String = CStr(cData.Param1)
                        Dim type As Native.Api.NativeEnums.ServiceStartType = CType(cData.Param2, Native.Api.NativeEnums.ServiceStartType)
                        Try
                            ServiceProvider.GetServiceByName(name).SetServiceStartType(type)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change service start type")
                        End Try
                    Case cSocketData.OrderType.ServiceResume
                        Dim name As String = CStr(cData.Param1)
                        Try
                            ServiceProvider.GetServiceByName(name).ResumeService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not resume service")
                        End Try
                    Case cSocketData.OrderType.ServiceStart
                        Dim name As String = CStr(cData.Param1)
                        Try
                            ServiceProvider.GetServiceByName(name).StartService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not start service")
                        End Try
                    Case cSocketData.OrderType.ServiceStop
                        Dim name As String = CStr(cData.Param1)
                        Try
                            ServiceProvider.GetServiceByName(name).StopService()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not stop service")
                        End Try
                End Select



                ' ===== Thread functions
                Select Case cData.Order
                    Case cSocketData.OrderType.ThreadDecreasePriority
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), True)).DecreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change thread priority")
                        End Try
                    Case cSocketData.OrderType.ThreadIncreasePriority
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), True)).IncreasePriority()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change thread priority")
                        End Try
                    Case cSocketData.OrderType.ThreadResume
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).ThreadResume()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not resume thread")
                        End Try
                    Case cSocketData.OrderType.ThreadSetAffinity
                        'TODO
                    Case cSocketData.OrderType.ThreadSetPriority
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Dim level As Integer = CInt(cData.Param3)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).SetPriority(CType(level, ThreadPriorityLevel))
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not set thread priority")
                        End Try
                    Case cSocketData.OrderType.ThreadSuspend
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).ThreadSuspend()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not suspend thread")
                        End Try
                    Case cSocketData.OrderType.ThreadTerminate
                        Dim pid As Integer = CInt(cData.Param1)
                        Dim tid As Integer = CInt(cData.Param2)
                        Try
                            Dim sti As New Native.Api.NativeStructs.SystemThreadInformation
                            sti.ClientId = New Native.Api.NativeStructs.ClientId(pid, tid)
                            Call (New cThread(New threadInfos(sti), False)).ThreadTerminate()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not terminate thread")
                        End Try
                End Select



                ' ===== Other functions
                Select Case cData.Order
                    Case cSocketData.OrderType.JobTerminate
                        Dim name As String = CType(cData.Param1, String)
                        Try
                            cJob.SharedLRTerminateJob(name)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not terminate job")
                        End Try
                    Case cSocketData.OrderType.JobSetLimits
                        Dim name As String = CType(cData.Param1, String)
                        Dim l1 As Native.Api.NativeStructs.JobObjectBasicUiRestrictions = CType(cData.Param2, Native.Api.NativeStructs.JobObjectBasicUiRestrictions)
                        Dim l2 As Native.Api.NativeStructs.JobObjectExtendedLimitInformation = CType(cData.Param3, Native.Api.NativeStructs.JobObjectExtendedLimitInformation)
                        Try
                            cJob.SharedLRSetLimits(name, l1, l2)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not set job limits")
                        End Try
                    Case cSocketData.OrderType.JobAddProcessToJob
                        Dim name As String = CType(cData.Param1, String)
                        Dim pid() As Integer = CType(cData.Param2, Integer())
                        Try
                            cJob.SharedLRAddProcess(name, pid)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not add process to job")
                        End Try
                    Case cSocketData.OrderType.MemoryFree
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim address As IntPtr = CType(cData.Param2, IntPtr)
                        Dim size As IntPtr = CType(cData.Param3, IntPtr)
                        Dim type As Native.Api.NativeEnums.MemoryState = CType(cData.Param4, Native.Api.NativeEnums.MemoryState)
                        Try
                            cMemRegion.SharedLRFree(pid, address, size, type)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not free memory region")
                        End Try
                    Case cSocketData.OrderType.MemoryChangeProtectionType
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim address As IntPtr = CType(cData.Param2, IntPtr)
                        Dim size As IntPtr = CType(cData.Param3, IntPtr)
                        Dim type As Native.Api.NativeEnums.MemoryProtectionType = CType(cData.Param4, Native.Api.NativeEnums.MemoryProtectionType)
                        Try
                            cMemRegion.SharedLRChangeProtection(pid, address, size, type)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change memory region protection type")
                        End Try
                    Case cSocketData.OrderType.HandleClose
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim handle As IntPtr = CType(cData.Param2, IntPtr)
                        Try
                            cHandle.SharedLRCloseHandle(pid, handle)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not close handle")
                        End Try
                    Case cSocketData.OrderType.ModuleUnload
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim address As IntPtr = CType(cData.Param2, IntPtr)
                        Try
                            cProcess.SharedRLUnLoadModuleFromProcess(pid, address)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not unload module")
                        End Try
                    Case cSocketData.OrderType.PrivilegeChangeStatus
                        Dim pid As Integer = CType(cData.Param1, Integer)
                        Dim name As String = CType(cData.Param2, String)
                        Dim status As Native.Api.NativeEnums.SePrivilegeAttributes = CType(cData.Param3, Native.Api.NativeEnums.SePrivilegeAttributes)
                        Try
                            cPrivilege.LocalChangeStatus(pid, name, status)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not change privilege status")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandHibernate
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Hibernate(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not hibernate system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandLock
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Lock()
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not lock system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandLogoff
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Logoff(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not logoff system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandPoweroff
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Poweroff(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not poweroff system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandRestart
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Restart(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not restart system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandShutdown
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Shutdown(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not shutdown system")
                        End Try
                    Case cSocketData.OrderType.GeneralCommandSleep
                        Dim force As Boolean = CBool(cData.Param1)
                        Try
                            cSystem.Sleep(force)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not sleep system")
                        End Try
                    Case cSocketData.OrderType.TcpClose
                        Dim local As IPEndPoint = CType(cData.Param1, IPEndPoint)
                        Dim remote As IPEndPoint = CType(cData.Param2, IPEndPoint)
                        Try
                            cNetwork.LocalCloseTCP(local, remote)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Could not close TCP connection")
                        End Try
                End Select


            End If

        Catch ex As Exception
            Misc.ShowError(ex, "Could not process client request")
        End Try
    End Sub

    Private Sub frmServeur_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Program.Parameters.ModeHidden Then
            Me.Left = Pref.LEFT_POSITION_HIDDEN
            Me.ShowInTaskbar = False
        End If

        Native.Functions.Misc.SetTheme(Me.lvServer.Handle)

        SetToolTip(Me.txtIp, "Available IP of this machine")

        ' Set some handlers
        AddHandler ProcessProvider.GotRefreshed, AddressOf HasEnumeratedProcess
        AddHandler ServiceProvider.GotRefreshed, AddressOf HasEnumeratedService
        AddHandler EnvVariableProvider.GotRefreshed, AddressOf HasEnumeratedEnvVar
        AddHandler HeapProvider.GotRefreshed, AddressOf HasEnumeratedHeaps
        AddHandler NetworkConnectionsProvider.GotRefreshed, AddressOf HasEnumeratedNetwork
        AddHandler PrivilegeProvider.GotRefreshed, AddressOf HasEnumeratedPrivilege
        AddHandler WindowProvider.GotRefreshed, AddressOf HasEnumeratedWindows
        AddHandler JobProvider.GotRefreshed, AddressOf HasEnumeratedJobs
        AddHandler ModuleProvider.GotRefreshed, AddressOf HasEnumeratedModule
        AddHandler ThreadProvider.GotRefreshed, AddressOf HasEnumeratedThread
        AddHandler MemRegionProvider.GotRefreshed, AddressOf HasEnumeratedMemoryReg
        AddHandler HandleProvider.GotRefreshed, AddressOf HasEnumeratedHandle
        AddHandler LogProvider.GotRefreshed, AddressOf HasEnumeratedLog
        'sock.ConnexionAccepted = New AsynchronousServer.ConnexionAcceptedEventHandle(AddressOf sock_ConnexionAccepted)
        'sock.Disconnected = New AsynchronousServer.DisconnectedEventHandler(AddressOf sock_Disconnected)
        'sock.SentData = New AsynchronousServer.SentDataEventHandler(AddressOf sock_SentData)

        connectLocal()

        Dim s() As String = GetIpv4Ips()
        If (s Is Nothing) OrElse s.Length = 0 Then
            Me.txtIp.Text = "Error while trying to retrieve local IP address."
        ElseIf s.Length = 1 Then
            Me.txtIp.Text = "You will have to configure LitePM with this IP : " & s(0)
        Else
            Me.txtIp.Text = "You have more than one network card, so you will have to use one of these IP addresses to configure LitePM : " & vbNewLine
            For Each x As String In s
                Me.txtIp.Text &= x & vbNewLine
            Next
            Me.txtIp.Text = Me.txtIp.Text.Substring(0, Me.txtIp.Text.Length - 2)
        End If

        ' Connect 
        Call ConnectNow()

    End Sub

    Private Delegate Sub addItemHandler(ByRef dat As cSocketData)
    Private Sub addItem(ByRef dat As cSocketData)
        Dim it As New ListViewItem(Date.Now.ToLongDateString & " - " & Date.Now.ToLongTimeString)
        it.SubItems.Add(dat.ToString)
        Me.lvServer.Items.Add(it)
    End Sub

    Private Sub ConnectNow()
        ' Connect or disconnect the socket (server)
        Dim t As New System.Threading.WaitCallback(AddressOf conDegCallBack)
        Call Threading.ThreadPool.QueueUserWorkItem(t, Nothing)
    End Sub

    Private Sub conDegCallBack(ByVal obj As Object)
        'Try
        If _state = SOCK_STATE.Disconnected Then
            sock.Connect(Parameters.RemotePort)
        Else
            sock.Disconnect()
        End If
        'Catch ex As Exception
        '    Misc.ShowDebugError(ex)
        'End Try
    End Sub

    ' Send an error message to the client
    Public Sub SendErrorToClient(ByVal _ex As cError)
        Try
            Dim cDat As New cSocketData(cSocketData.DataType.ErrorOnServer, , New SerializableException(_ex))
            sock.Send(cDat)
        Catch ex As Exception
            ' FAILED !!
            Misc.ShowDebugError(ex)
        End Try
    End Sub

End Class
