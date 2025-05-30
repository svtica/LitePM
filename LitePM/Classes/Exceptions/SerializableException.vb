
' Lite Process Monitor

Option Strict On

<Serializable()> _
Public Class SerializableException


    ' Private attributes


    ' All error fields
    Private _customMessage As String
    Private _message As String
    'Private _helpL As String
    'Private _source As String
    'Private _stack As String



    ' Public properties


    ' Custom message
    Public ReadOnly Property CustomMessage() As String
        Get
            Return _customMessage
        End Get
    End Property

    ' Normal message
    Public ReadOnly Property Message() As String
        Get
            Return _message
        End Get
    End Property



    ' Public functions


    ' Constructors
    Public Sub New(ByVal exception As cError)
        _customMessage = exception.CustomMessage
        _message = exception.Message
        '_helpL = exception.HelpLink
        '_source = exception.Source
        '_stack = exception.StackTrace
    End Sub
    Public Sub New(ByVal exception As Exception)
        _customMessage = Nothing
        _message = exception.Message
        '_helpL = exception.HelpLink
        '_source = exception.Source
        '_stack = exception.StackTrace
    End Sub

    ' Return a standard exception
    Public Function GetException() As Exception
        Return New Exception(_message & vbNewLine & _customMessage)
    End Function



End Class
