
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackHandleUnload

    Private _deg As HasUnloadedHandle

    Public Delegate Sub HasUnloadedHandle(ByVal Success As Boolean, ByVal pid As Integer, ByVal handle As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasUnloadedHandle)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public handle As IntPtr
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal hand As IntPtr, _
                       ByVal act As Integer)
            handle = hand
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.HandleClose, pObj.pid, pObj.handle)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Integer = Native.Objects.Handle.CloseProcessLocalHandle(pObj.pid, _
                                                                                   pObj.handle)
                _deg.Invoke(ret <> 0, pObj.pid, pObj.handle, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
