
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcMinidump

    Private _deg As HasCreatedDump

    Public Delegate Sub HasCreatedDump(ByVal Success As Boolean, ByVal pid As Integer, ByVal file As String, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasCreatedDump)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public file As String
        Public dumpOpt As Native.Api.NativeEnums.MiniDumpType
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal fil As String, ByVal opt As Native.Api.NativeEnums.MiniDumpType, ByVal act As Integer)
            newAction = act
            file = fil
            dumpOpt = opt
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

                Try
                    Dim ret As Boolean = Native.Objects.Process.CreateMiniDumpFileById(pObj.pid, _
                                                                    pObj.file, pObj.dumpOpt)
                    _deg.Invoke(ret, pObj.pid, pObj.file, Native.Api.Win32.GetLastError, _
                                pObj.newAction)
                Catch ex As Exception
                    ' Access denied, or...
                    _deg.Invoke(False, pObj.pid, pObj.file, ex.Message, pObj.newAction)
                End Try
        End Select
    End Sub

End Class
