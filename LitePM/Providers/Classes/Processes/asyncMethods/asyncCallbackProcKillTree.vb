﻿
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcKillTree

    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal msg As String, ByVal actionN As Integer)

    Public Sub New(ByVal deg As HasKilled)
        _deg = deg
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public newAction As Integer
        Public Sub New(ByVal pi As Integer, ByVal act As Integer)
            newAction = act
            pid = pi
        End Sub
    End Structure

    Public Sub Process(ByVal thePoolObj As Object)

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If Program.Connection.IsConnected Then

            Select Case Program.Connection.Type
                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessKillTree, pObj.pid)
                        Program.Connection.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                Case Else
                    ' Local
                    _deg.Invoke(recursiveKill(pObj.pid), Native.Api.Win32.GetLastError, pObj.newAction)

            End Select
        End If
    End Sub

    ' For 'Kill process tree'
    Private Function recursiveKill(ByVal pid As Integer) As Boolean
        Static success As Boolean = True

        ' Kill process
        success = success And Native.Objects.Process.KillProcessById(pid)

        ' Get all items...
        Dim _dico2 As New List(Of Integer)
        _dico2 = Native.Objects.Process.EnumerateChildProcessesById(pid)

        ' Recursive kill
        For Each t As Integer In _dico2
            recursiveKill(t)
        Next

        Return success
    End Function

End Class
