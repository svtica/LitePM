
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackMemRegionDump

    Private _deg As HasDumped

    Public Delegate Sub HasDumped(ByVal Success As Boolean, ByVal file As String, ByVal address As IntPtr, ByVal msg As String, ByVal actionNumber As Integer)

    Public Sub New(ByVal deg As HasDumped)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public address As IntPtr
        Public file As String
        Public size As IntPtr
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, _
                       ByVal ad As IntPtr, _
                       ByVal fil As String, _
                       ByVal siz As IntPtr, _
                       ByVal act As Integer)
            address = ad
            newAction = act
            file = fil
            size = siz
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

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                Dim ret As Boolean = Native.Objects.MemRegion.DumpMemory(pObj.pid, _
                                                    pObj.address, pObj.size, pObj.file)
                _deg.Invoke(ret, pObj.file, pObj.address, Native.Api.Win32.GetLastError, pObj.newAction)
        End Select
    End Sub

End Class
