
' Lite Process Monitor









'




'



Option Strict On

<Serializable()> Public Class envVariableInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _procId As Integer
    Private _variable As String
    Private _value As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _procId
        End Get
    End Property
    Public ReadOnly Property Variable() As String
        Get
            Return _variable
        End Get
    End Property
    Public ReadOnly Property Value() As String
        Get
            Return _value
        End Get
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Return _variable
        End Get
    End Property

#End Region



    ' Public


    ' Constructor of this class
    Public Sub New(ByRef variable As String, ByVal value As String, ByVal pid As Integer)

        _procId = pid
        _variable = variable
        _value = value

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As envVariableInfos)
        _value = newI.Value
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(0) As String

        s(0) = "Value"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Variable"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
