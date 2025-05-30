
' Lite Process Monitor









'




'




Option Strict On
Imports System.Net

Public Class asyncCallbackNetworkCloseConnection

    Private _deg As HasClosedConnection

    Public Delegate Sub HasClosedConnection(ByVal Success As Boolean, ByVal local As IPEndPoint, ByVal remote As IPEndPoint, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasClosedConnection)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public local As IPEndPoint
        Public remote As IPEndPoint
        Public newAction As Integer
        Public Sub New(ByVal loc As IPEndPoint, _
                       ByVal remo As IPEndPoint, _
                       ByVal act As Integer)
            local = loc
            remote = remo
            newAction = act
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.TcpClose, pObj.local, pObj.remote)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local

                Dim ret As Integer = Native.Objects.Network.CloseTcpConnectionByIPEndPoints(pObj.local, pObj.remote)
                _deg.Invoke(ret = 0, pObj.local, pObj.remote, Native.Api.Win32.GetLastError, pObj.newAction)

        End Select
    End Sub

End Class
