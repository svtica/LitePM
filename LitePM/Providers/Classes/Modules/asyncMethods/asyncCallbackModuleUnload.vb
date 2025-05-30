
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackModuleUnload

    Private _deg As HasUnloadedModule

    Public Delegate Sub HasUnloadedModule(ByVal Success As Boolean, ByVal pid As Integer, ByVal name As String, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasUnloadedModule)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public name As String
        Public newAction As Integer
        Public baseA As IntPtr
        Public Sub New(ByVal pi As Integer, _
                       ByVal nam As String, _
                       ByVal add As IntPtr, _
                       ByVal act As Integer)
            name = nam
            baseA = add
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ModuleUnload, pObj.pid, pObj.baseA)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.Module.UnloadModuleByAddress(pObj.baseA, _
                                                                                 pObj.pid)
                _deg.Invoke(ret, pObj.pid, pObj.name, _
                            Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
