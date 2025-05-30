
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcSuspend

    Private _deg As HasSuspended

    Public Delegate Sub HasSuspended(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasSuspended)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal act As Integer)
            newAction = act
            pid = pi
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessSuspend, pObj.pid)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Process.SuspendProcessById(pObj.pid)
                _deg.Invoke(ret, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
