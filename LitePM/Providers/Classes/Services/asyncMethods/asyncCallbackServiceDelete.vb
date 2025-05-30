
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackServiceDelete

    Private _deg As HasDeleted

    Public Delegate Sub HasDeleted(ByVal Success As Boolean, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasDeleted)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public name As String
        Public newAction As Integer
        Public Sub New(ByVal nam As String, _
                       ByVal act As Integer)
            name = nam
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ServiceDelete, pObj.name)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' TODO

            Case Else
                ' Local
                Dim res As Boolean = Native.Objects.Service.DeleteServiceByName(pObj.name, _
                                                            ServiceProvider.ServiceControlManaherHandle)
                _deg.Invoke(res, pObj.name, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
