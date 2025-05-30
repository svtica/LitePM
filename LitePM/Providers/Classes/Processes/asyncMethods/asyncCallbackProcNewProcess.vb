
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcNewProcess

    Private _deg As HasCreated

    Public Delegate Sub HasCreated(ByVal Success As Boolean, ByVal path As String, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasCreated)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public path As String
        Public newAction As Integer
        Public Sub New(ByVal s As String, ByVal act As Integer)
            newAction = act
            path = s
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessCreateNew, pObj.path)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim msg As String = ""
                Dim res As Boolean = Wmi.Objects.Process.CreateNewProcessByPath(pObj.path, ProcessProvider.wmiSearcher, msg)
                Try
                    _deg.Invoke(res, pObj.path, msg, pObj.newAction)
                Catch ex As Exception
                    _deg.Invoke(False, pObj.path, ex.Message, pObj.newAction)
                End Try

            Case Else
                ' Local
                ' OK, normally the local startNewProcess is not done here
                ' because of RunBox need
                Dim pid As Integer = Shell(pObj.path)
                _deg.Invoke(pid <> 0, pObj.path, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class