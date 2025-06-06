﻿
' Lite Process Monitor









'




'



Option Strict On

Public Class cPrivilege
    Inherits cGeneralObject

    Private _privilegeInfos As privilegeInfos


#Region "Constructors & destructor"

    Public Sub New(ByRef infos As privilegeInfos)
        _privilegeInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Privilege
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As privilegeInfos
        Get
            Return _privilegeInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As privilegeInfos)
        _privilegeInfos.Merge(Thr)
    End Sub

#Region "All available actions"

    ' Change status
    Private _changeST As asyncCallbackPrivilegeChangeStatus
    Public Function ChangeStatus(ByVal status As Native.Api.NativeEnums.SePrivilegeAttributes) As Integer

        If _changeST Is Nothing Then
            _changeST = New asyncCallbackPrivilegeChangeStatus(New asyncCallbackPrivilegeChangeStatus.HasChangedStatus(AddressOf changeStatusDone))
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _changeST.Process)
        Dim newAction As Integer = cGeneralObject.GetActionCount

        AddPendingTask(newAction, t)
        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackPrivilegeChangeStatus.poolObj(Me.Infos.ProcessId, Me.Infos.Name, status, newAction))

    End Function
    Private Sub changeStatusDone(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)
        If Success = False Then
            Misc.ShowError("Could not change privilege (" & name & ") status : " & msg)
        End If
        RemovePendingTask(actionNumber)
    End Sub

#End Region

#Region "Local shared method"

    Public Shared Function LocalChangeStatus(ByVal pid As Integer, ByVal name As String, ByVal status As Native.Api.NativeEnums.SePrivilegeAttributes) As Boolean
        Return Native.Objects.Token.SetPrivilegeStatusByProcessId(pid, name, status)
    End Function

#End Region

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return privilegeInfos.GetAvailableProperties(includeFirstProp, sorted)
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
            Case "Status"
                res = Me.Infos.Status.ToString
            Case "Description"
                res = Me.Infos.Description
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Name As String = ""
        Static _old_Status As String = ""
        Static _old_Description As String = ""

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
            Case "Status"
                res = Me.Infos.Status.ToString
                If res = _old_Status Then
                    hasChanged = False
                Else
                    _old_Status = res
                End If
            Case "Description"
                res = Me.Infos.Description
                If res = _old_Description Then
                    hasChanged = False
                Else
                    _old_Description = res
                End If
        End Select

        Return hasChanged
    End Function

#End Region

    ' Return backcolor of current item
    Public Overrides Function GetBackColor() As System.Drawing.Color

        Select Case Me.Infos.Status
            Case Native.Api.NativeEnums.SePrivilegeAttributes.Enabled
                Return Color.FromArgb(224, 240, 224)
            Case Native.Api.NativeEnums.SePrivilegeAttributes.EnabledByDefault, Native.Api.NativeEnums.SePrivilegeAttributes.DisabledByDefault
                Return Color.FromArgb(192, 240, 192)
            Case Native.Api.NativeEnums.SePrivilegeAttributes.Disabled
                Return Color.FromArgb(240, 224, 224)
            Case Native.Api.NativeEnums.SePrivilegeAttributes.Removed
                Return Color.FromArgb(240, 192, 192)
            Case Else
                Return MyBase.GetBackColor
        End Select

    End Function

End Class
