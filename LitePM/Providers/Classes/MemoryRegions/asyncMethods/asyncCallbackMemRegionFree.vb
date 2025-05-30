
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackMemRegionFree

    Private _deg As HasFreed

    Public Delegate Sub HasFreed(ByVal Success As Boolean, ByVal pid As Integer, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasFreed)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public address As IntPtr
        Public size As IntPtr
        Public type As Native.Api.NativeEnums.MemoryState
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal ad As IntPtr, _
                       ByVal siz As IntPtr, _
                       ByVal typ As Native.Api.NativeEnums.MemoryState, _
                       ByVal act As Integer)
            address = ad
            newAction = act
            size = siz
            type = typ
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.MemoryFree, pObj.pid, pObj.address, pObj.size, pObj.type)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.MemRegion.FreeMemory(pObj.pid, _
                                                    pObj.address, pObj.size, pObj.type)
                _deg.Invoke(ret, pObj.pid, pObj.address, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
