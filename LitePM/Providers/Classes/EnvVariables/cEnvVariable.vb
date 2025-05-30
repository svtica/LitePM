
' Lite Process Monitor









'




'



Option Strict On

Public Class cEnvVariable
    Inherits cGeneralObject

    Private _envInfos As envVariableInfos


#Region "Constructors & destructor"

    Public Sub New(ByRef infos As envVariableInfos)
        _envInfos = infos
        _TypeOfObject = Native.Api.Enums.GeneralObjectType.EnvironmentVariable
    End Sub

#End Region

#Region "Normal properties"

    Public ReadOnly Property Infos() As envVariableInfos
        Get
            Return _envInfos
        End Get
    End Property

#End Region

    ' Merge current infos and new infos
    Public Sub Merge(ByRef Thr As envVariableInfos)
        _envInfos.Merge(Thr)
    End Sub

#Region "Get information overriden methods"

    ' Return list of available properties
    Public Overrides Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Return envVariableInfos.GetAvailableProperties(includeFirstProp, sorted)
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
            Case "Variable"
                res = Me.Infos.Variable
            Case "Value"
                res = Me.Infos.Value
        End Select

        Return res
    End Function
    Public Overrides Function GetInformation(ByVal info As String, ByRef res As String) As Boolean

        ' Old values (from last refresh)
        Static _old_ObjectCreationDate As String = ""
        Static _old_PendingTaskCount As String = ""
        Static _old_Variable As String = ""
        Static _old_Value As String = ""

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
            Case "Variable"
                res = Me.Infos.Variable
                If res = _old_Variable Then
                    hasChanged = False
                Else
                    _old_Variable = res
                End If
            Case "Value"
                res = Me.Infos.Value
                If res = _old_Value Then
                    hasChanged = False
                Else
                    _old_Value = res
                End If
        End Select

        Return hasChanged
    End Function

#End Region

End Class
