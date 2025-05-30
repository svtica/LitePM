
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackThreadSuspend

    Private _deg As HasSuspended

    Public Delegate Sub HasSuspended(ByVal Success As Boolean, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasSuspended)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public id As Integer
        Public newAction As Integer
        Public pid As Integer
        Public Sub New(ByVal _id As Integer, _
                       ByVal action As Integer, Optional _
                       ByVal processId As Integer = 0)
            newAction = action
            id = _id
            pid = processId
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If Program.Connection.IsConnected = False Then
            Exit Sub
        End If

        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ThreadSuspend, pObj.pid, pObj.id)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Thread.SuspendThreadById(pObj.id)
                _deg.Invoke(ret, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
