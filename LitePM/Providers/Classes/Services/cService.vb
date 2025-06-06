﻿
' Lite Process Monitor









'




'



Option Strict On

Imports Common.Misc

Public Class cService
    Inherits cGeneralObject

    Private Const NO_DEPENDENCIES As String = ""

    Private _firstRefresh As Boolean = True
    Private _serviceInfos As serviceInfos
    Private __dep As String

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As serviceInfos)
        _serviceInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Service
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As serviceInfos
        Get
            Return _serviceInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As serviceInfos)
        _serviceInfos.Merge(Thr)
    End Sub

    ' Return true if the state has changed
    Public Function HasChanged(ByRef infos As serviceInfos) As Boolean
        Dim b As Boolean = (_firstRefresh OrElse Me.Infos.State <> infos.State OrElse Me.Infos.ProcessId <> infos.ProcessId)
        _firstRefresh = False
        Return b
    End Function

    ' Refresh Config
    ' (used for Reanalize)
    Public Sub Refresh()
        Select Case Program.Connection.Type

            Case cConnection.TypeOfConnection.LocalConnection
                Native.Objects.Service.GetServiceConfigByName(ServiceProvider.ServiceControlManaherHandle, _
                                                        Me.Infos.Name, Me.Infos, True)

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' Everything is retrieved each time in wmi mode...

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' Everything is retrieved each time in socket mode...

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Won't refresh in snapshot mode...

        End Select
    End Sub


#Region "All actions on services (start...)"

    ' Start service
    Private _startServ As asyncCallbackServiceStart
    Public Function StartService() As Integer

        If _startServ Is Nothing Then
            _startServ = New asyncCallbackServiceStart(New asyncCallbackServiceStart.HasStarted(AddressOf startServiceDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _startServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackServiceStart.poolObj(Me.Infos.Name, newAction))

    End Function
    Private Sub startServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not start service " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub


    ' Delete a service
    Private _deleteServ As asyncCallbackServiceDelete
    Public Function DeleteService() As Integer

        If _deleteServ Is Nothing Then
            _deleteServ = New asyncCallbackServiceDelete(New asyncCallbackServiceDelete.HasDeleted(AddressOf deleteServiceDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _deleteServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackServiceDelete.poolObj(Me.Infos.Name, newAction))

    End Function
    Private Sub deleteServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not delete service " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub


    ' Pause a service
    Private _pauseServ As asyncCallbackServicePause
    Public Function PauseService() As Integer

        If _pauseServ Is Nothing Then
            _pauseServ = New asyncCallbackServicePause(New asyncCallbackServicePause.HasPaused(AddressOf pauseServiceDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _pauseServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackServicePause.poolObj(Me.Infos.Name, newAction))

    End Function
    Private Sub pauseServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not pause service " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Resume a service
    Private _resumeServ As asyncCallbackServiceResume
    Public Function ResumeService() As Integer

        If _resumeServ Is Nothing Then
            _resumeServ = New asyncCallbackServiceResume(New asyncCallbackServiceResume.HasResumed(AddressOf resumeServiceDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _resumeServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackServiceResume.poolObj(Me.Infos.Name, newAction))

    End Function
    Private Sub resumeServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not resume service " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Stop a service
    Private _stopServ As asyncCallbackServiceStop
    Public Function StopService() As Integer

        If _stopServ Is Nothing Then
            _stopServ = New asyncCallbackServiceStop(New asyncCallbackServiceStop.HasStopped(AddressOf stopServiceDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _stopServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackServiceStop.poolObj(Me.Infos.Name, newAction))

    End Function
    Private Sub stopServiceDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not stop service " & name & " : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

    ' Set service start type
    Private _setStartTypeServ As asyncCallbackServiceSetStartType
    Public Sub SetServiceStartType(ByVal type As Native.Api.NativeEnums.ServiceStartType)

        If _setStartTypeServ Is Nothing Then
            _setStartTypeServ = New asyncCallbackServiceSetStartType(New asyncCallbackServiceSetStartType.HasChangedStartType(AddressOf setServiceStartTypeDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _setStartTypeServ.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackServiceSetStartType.poolObj(Me.Infos.Name, type, newAction))

    End Sub
    Private Sub setServiceStartTypeDone(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not change start type of service " & name & " : " & msg)
        Else
            Call Me.Refresh()
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return serviceInfos.GetAvailableProperties(includeFirstProp, sorted)
    End Function

    ' Retrieve informations by its name
    Public Overrides Function GetInformation(ByVal info As String) As String
        Dim res As String = NO_INFO_RETRIEVED

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
        End If

        Select Case info
            Case "Name"
                res = Me.Infos.Name
            Case "DisplayName"
                res = Me.Infos.DisplayName
            Case "ServiceType"
                res = Me.Infos.ServiceType.ToString
            Case "ImagePath"
                res = GetRealPath(Me.Infos.ImagePath)
            Case "ErrorControl"
                res = Me.Infos.ErrorControl.ToString
            Case "StartType"
                res = Me.Infos.StartType.ToString
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
            Case "ProcessName"
                res = Me.Infos.ProcessName
            Case "LoadOrderGroup"
                res = Me.Infos.LoadOrderGroup
            Case "ServiceStartName"
                res = Me.Infos.ServiceStartName
            Case "State"
                res = Me.Infos.State.ToString
            Case "Description"
                res = Me.Infos.Description
            Case "DiagnosticMessageFile"
                res = Me.Infos.DiagnosticMessageFile
            Case "ObjectName"
                res = Me.Infos.ObjectName
            Case "Process"
                If Me.Infos.ProcessId > 0 Then
                    res = cProcess.GetProcessName(Me.Infos.ProcessId) & " -- " & Me.Infos.ProcessId.ToString
                End If
            Case "Dependencies"
                If __dep Is Nothing Then
                    If Me.Infos.Dependencies IsNot Nothing Then
                        For Each s As String In Me.Infos.Dependencies
                            __dep &= s & "   "
                        Next
                    Else
                        __dep = NO_DEPENDENCIES
                    End If
                    If __dep Is Nothing Then
                        __dep = NO_DEPENDENCIES
                    End If
                End If
                res = __dep
            Case "TagID"
                res = Me.Infos.TagID.ToString
            Case "ServiceFlags"
                res = Me.Infos.ServiceFlags.ToString
            Case "WaitHint"
                res = Me.Infos.WaitHint.ToString
            Case "CheckPoint"
                res = Me.Infos.CheckPoint.ToString
            Case "Win32ExitCode"
                res = Me.Infos.Win32ExitCode.ToString
            Case "ServiceSpecificExitCode"
                res = Me.Infos.ServiceSpecificExitCode.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Win32ExitCode As String = ""
        Static _old_ServiceSpecificExitCode As String = ""
        Static _old_Name As String = ""
        Static _old_DisplayName As String = ""
        Static _old_ServiceType As String = ""
        Static _old_ImagePath As String = ""
        Static _old_ErrorControl As String = ""
        Static _old_StartType As String = ""
        Static _old_ProcessId As String = ""
        Static _old_ProcessName As String = ""
        Static _old_LoadOrderGroup As String = ""
        Static _old_ServiceStartName As String = ""
        Static _old_State As String = ""
        Static _old_DiagnosticMessageFile As String = ""
        Static _old_WaitHint As String = ""
        Static _old_CheckPoint As String = ""
        Static _old_TagID As String = ""
        Static _old_Description As String = ""
        Static _old_ObjectName As String = ""
        Static _old_Process As String = ""
        Static _old_Dependencies As String = ""
        Static _old_ServiceFlags As String = ""
        Static _path As String = NO_INFO_RETRIEVED

        Dim hasChanged As Boolean = True

        If info = "ObjectCreationDate" Then
            res = _objectCreationDate.ToLongDateString & " -- " & _objectCreationDate.ToLongTimeString
            If res = _old_ObjectCreationDate Then
                hasChanged = False
            Else
                _old_ObjectCreationDate = res
                Return True
            End If
        ElseIf info = "PendingTaskCount" Then
            res = PendingTaskCount.ToString
            If res = _old_PendingTaskCount Then
                hasChanged = False
            Else
                _old_PendingTaskCount = res
                Return True
            End If
        End If

        Select Case info
            Case "Name"
                res = Me.Infos.Name
                If res = _old_Name Then
                    hasChanged = False
                Else
                    _old_Name = res
                End If
            Case "DisplayName"
                res = Me.Infos.DisplayName
                If res = _old_DisplayName Then
                    hasChanged = False
                Else
                    _old_DisplayName = res
                End If
            Case "ServiceType"
                res = Me.Infos.ServiceType.ToString
                If res = _old_ServiceType Then
                    hasChanged = False
                Else
                    _old_ServiceType = res
                End If
            Case "ImagePath"
                If _path = NO_INFO_RETRIEVED Then
                    _path = GetRealPath(Me.Infos.ImagePath)
                End If
                res = _path
                If res = _old_ImagePath Then
                    hasChanged = False
                Else
                    _old_ImagePath = res
                End If
            Case "ErrorControl"
                res = Me.Infos.ErrorControl.ToString
                If res = _old_ErrorControl Then
                    hasChanged = False
                Else
                    _old_ErrorControl = res
                End If
            Case "StartType"
                res = Me.Infos.StartType.ToString
                If res = _old_StartType Then
                    hasChanged = False
                Else
                    _old_StartType = res
                End If
            Case "ProcessId"
                res = Me.Infos.ProcessId.ToString
                If res = _old_ProcessId Then
                    hasChanged = False
                Else
                    _old_ProcessId = res
                End If
            Case "ProcessName"
                res = Me.Infos.ProcessName
                If res = _old_ProcessName Then
                    hasChanged = False
                Else
                    _old_ProcessName = res
                End If
            Case "LoadOrderGroup"
                res = Me.Infos.LoadOrderGroup
                If res = _old_LoadOrderGroup Then
                    hasChanged = False
                Else
                    _old_LoadOrderGroup = res
                End If
            Case "ServiceStartName"
                res = Me.Infos.ServiceStartName
                If res = _old_ServiceStartName Then
                    hasChanged = False
                Else
                    _old_ServiceStartName = res
                End If
            Case "State"
                res = Me.Infos.State.ToString
                If res = _old_State Then
                    hasChanged = False
                Else
                    _old_State = res
                End If
            Case "Description"
                res = Me.Infos.Description
                If res = _old_Description Then
                    hasChanged = False
                Else
                    _old_Description = res
                End If
            Case "DiagnosticMessageFile"
                res = Me.Infos.DiagnosticMessageFile
                If res = _old_DiagnosticMessageFile Then
                    hasChanged = False
                Else
                    _old_DiagnosticMessageFile = res
                End If
            Case "ObjectName"
                res = Me.Infos.ObjectName
                If res = _old_ObjectName Then
                    hasChanged = False
                Else
                    _old_ObjectName = res
                End If
            Case "Process"
                If Me.Infos.ProcessId > 0 Then
                    res = Me.Infos.ProcessId.ToString
                End If
                If res = _old_Process Then
                    hasChanged = False
                Else
                    _old_Process = res
                End If
            Case "Dependencies"
                If __dep Is Nothing Then
                    If Me.Infos.Dependencies IsNot Nothing Then
                        For Each s As String In Me.Infos.Dependencies
                            __dep &= s & "   "
                        Next
                    Else
                        __dep = NO_DEPENDENCIES
                    End If
                    If __dep Is Nothing Then
                        __dep = NO_DEPENDENCIES
                    End If
                End If
                res = __dep
                If res = _old_Dependencies Then
                    hasChanged = False
                Else
                    _old_Dependencies = res
                End If
            Case "TagID"
                res = Me.Infos.TagID.ToString
                If res = _old_TagID Then
                    hasChanged = False
                Else
                    _old_TagID = res
                End If
            Case "ServiceFlags"
                res = Me.Infos.ServiceFlags.ToString
                If res = _old_ServiceFlags Then
                    hasChanged = False
                Else
                    _old_ServiceFlags = res
                End If
            Case "WaitHint"
                res = Me.Infos.WaitHint.ToString
                If res = _old_WaitHint Then
                    hasChanged = False
                Else
                    _old_WaitHint = res
                End If
            Case "CheckPoint"
                res = Me.Infos.CheckPoint.ToString
                If res = _old_CheckPoint Then
                    hasChanged = False
                Else
                    _old_CheckPoint = res
                End If
            Case "Win32ExitCode"
                res = Me.Infos.Win32ExitCode.ToString
                If res = _old_Win32ExitCode Then
                    hasChanged = False
                Else
                    _old_Win32ExitCode = res
                End If
            Case "ServiceSpecificExitCode"
                res = Me.Infos.ServiceSpecificExitCode.ToString
                If res = _old_ServiceSpecificExitCode Then
                    hasChanged = False
                Else
                    _old_ServiceSpecificExitCode = res
                End If
        End Select

        Return hasChanged
    End Function


#End Region

End Class
