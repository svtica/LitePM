﻿
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackProcKill

    Private _deg As HasKilled

    Public Delegate Sub HasKilled(ByVal Success As Boolean, ByVal pid As Integer, ByVal msg As String, ByVal actionN As Integer)

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
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.ProcessKill, pObj.pid)
                        Program.Connection.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                    Dim msg As String = ""
                    Dim ret As Boolean = _
                        Wmi.Objects.Process.KillProcessById(pObj.pid, ProcessProvider.wmiSearcher, msg)

                    Try
                        _deg.Invoke(ret, pObj.pid, msg, pObj.newAction)
                    Catch ex As Exception
                        _deg.Invoke(False, pObj.pid, ex.Message, pObj.newAction)
                    End Try

                Case Else
                    ' Local
                    Dim ret As Boolean = Native.Objects.Process.KillProcessById(pObj.pid)
                    _deg.Invoke(ret, pObj.pid, Native.Api.Win32.GetLastError, pObj.newAction)
            End Select
        End If
    End Sub

End Class
