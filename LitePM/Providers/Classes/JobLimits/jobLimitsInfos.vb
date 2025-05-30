
' Lite Process Monitor









'




'



Option Strict On

<Serializable()> Public Class jobLimitInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _name As String
    Private _desc As String
    Private _value As String
    Private _valueObj As Object
    Private _jobName As String

#End Region

#Region "Read only properties"

    Public ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property
    Public ReadOnly Property Value() As String
        Get
            Return _value
        End Get
    End Property
    Public ReadOnly Property ValueObject() As Object
        Get
            Return _valueObj
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Return _desc
        End Get
    End Property
    Public Property JobName() As String
        Get
            Return _jobName
        End Get
        Friend Set(ByVal value As String)
            _jobName = value
        End Set
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Return _name
        End Get
    End Property

#End Region



    ' Public


    ' Constructor of this class
    Public Sub New()
        '
    End Sub
    Public Sub New(ByVal name As String, ByVal desc As String, ByVal value As String, ByVal valObj As Object, ByVal job As String)
        _name = name
        _desc = desc
        _value = value
        _valueObj = valObj
        _jobName = job
    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As jobLimitInfos)
        With newI
            _value = .Value
            _valueObj = .ValueObject
        End With
    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(1) As String

        s(0) = "Value"
        s(1) = "Description"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Limit"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
