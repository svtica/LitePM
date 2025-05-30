
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcKillByMethod

    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasKilled)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public method As Native.Api.Enums.KillMethod
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal meth As Native.Api.Enums.KillMethod, _
                       ByVal act As Integer)
            newAction = act
            pid = pi
            method = meth
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If Program.Connection.IsConnected Then

            Select Case Program.Connection.Type
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessKillByMethod, pObj.pid, pObj.method)
                        Program.Connection.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI


                Case Else
                    ' Local
                    Dim ret As Boolean = Native.Objects.Process.KillProcessByMethod(pObj.pid, pObj.method)
                    _deg.Invoke(ret, pObj.pid, Native.Api.Win32.GetLastError, pObj.newAction)
            End Select
        End If
    End Sub

End Class
