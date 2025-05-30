
' Lite Process Monitor


Option Strict On


Public Class cError
    Inherits Exception


    ' Private attributes


    ' Custom message
    Private _customMessage As String



    ' Public properties


    ' Custom message
    Public ReadOnly Property CustomMessage() As String
        Get
            Return _customMessage
        End Get
    End Property



    ' Other public

    <Serializable()> _
    Public Class SerializableException
        Public Sub New(ByVal ex As cError)

        End Sub
    End Class


    ' Public functions


    ' Constructor
    Public Sub New(ByVal message As String, ByVal exception As Exception)
        MyBase.New(exception.Message, exception)
        _customMessage = message
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
        _customMessage = Nothing
    End Sub

    ' Show message
    Public Sub ShowMessage(Optional ByVal forceClassical As Boolean = False)
        If Program.Parameters.ModeServer = False Then
            ' The we have to display our error as a message box
            Misc.ShowMsg("An handled error occured",
                         CustomMessage &
                         "Detailed information : " & Message,
                         MessageBoxButtons.OK)
        Else
            ' Then we have to send the error to the client
            _frmServer.SendErrorToClient(Me)
        End If
    End Sub


End Class
