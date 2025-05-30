
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcSetAffinity

    Private _deg As HasSetAffinity

    Public Delegate Sub HasSetAffinity(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasSetAffinity)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public level As Integer
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal lvl As Integer, ByVal act As Integer)
            newAction = act
            level = lvl
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessChangeAffinity, pObj.pid, pObj.level)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Process.SetProcessAffinityById(pObj.pid, pObj.level)
                _deg.Invoke(ret, Native.Api.Win32.GetLastError, pObj.newAction)

        End Select
    End Sub

End Class
