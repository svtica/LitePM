
' Lite Process Monitor









'




'



Option Strict On

Public Class cLogItem
    Inherits cGeneralObject

    Private _logInfos As logItemInfos

#Region "Constructors & destructor"

    Public Sub New(ByRef infos As logItemInfos)
        _logInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.Log
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As logItemInfos
        Get
            Return _logInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByVal Thr As logItemInfos)
        _logInfos.Merge(Thr)
    End Sub

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return logItemInfos.GetAvailableProperties(includeFirstProp, sorted)
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
            Case "Type"
                res = Me.Infos.Type
            Case "Date & Time", "Date && Time"
                res = Me.Infos.DateTime.ToLongDateString & " - " & Me.Infos.DateTime.ToLongTimeString
            Case "Description"
                res = Me.Infos.Description
            Case "State"
                res = Me.Infos.State.ToString
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Type As String = ""
        Static _old_DateTime As String = ""
        Static _old_Description As String = ""
        Static _old_State As String = ""

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
            Case "Type"
                res = Me.Infos.Type
                If res = _old_Type Then
                    hasChanged = False
                Else
                    _old_Type = res
                End If
            Case "Date & Time", "Date && Time"
                res = Me.Infos.DateTime.ToLongDateString & " - " & Me.Infos.DateTime.ToLongTimeString
                If res = _old_DateTime Then
                    hasChanged = False
                Else
                    _old_DateTime = res
                End If
            Case "Description"
                res = Me.Infos.Description
                If res = _old_Description Then
                    hasChanged = False
                Else
                    _old_Description = res
                End If
            Case "State"
                res = Me.Infos.State.ToString
                If res = _old_State Then
                    hasChanged = False
                Else
                    _old_State = res
                End If
        End Select

        Return hasChanged
    End Function

#End Region

End Class
