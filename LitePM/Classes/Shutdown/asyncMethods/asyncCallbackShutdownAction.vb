﻿
' Lite Process Monitor


Option Strict On
Imports Native.Api.Enums

Public Class asyncCallbackShutdownAction

    Private con As cShutdownConnection
    Private _deg As HasShutdowned

    Public Delegate Sub HasShutdowned(ByVal Success As Boolean, ByVal type As ShutdownType, ByVal msg As String)

    Public Sub New(ByVal deg As HasShutdowned, ByRef shutConnection As cShutdownConnection)
        _deg = deg
        con = shutConnection
    End Sub

    Public Structure poolObj
        Public type As ShutdownType
        Public force As Boolean
        Public Sub New(ByVal _type As ShutdownType, _
                       ByVal _force As Boolean)
            force = _force
            type = _type
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
                    Dim order As cSocketData.OrderType
                    Select Case pObj.type
                        Case ShutdownType.Lock
                            order = cSocketData.OrderType.GeneralCommandLock
                        Case ShutdownType.Logoff
                            order = cSocketData.OrderType.[GeneralCommandLogoff]
                        Case ShutdownType.Poweroff
                            order = cSocketData.OrderType.GeneralCommandPoweroff
                        Case ShutdownType.Restart
                            order = cSocketData.OrderType.GeneralCommandRestart
                        Case ShutdownType.Shutdown
                            order = cSocketData.OrderType.GeneralCommandShutdown
                        Case ShutdownType.Sleep
                            order = cSocketData.OrderType.GeneralCommandSleep
                        Case ShutdownType.Hibernate
                            order = cSocketData.OrderType.GeneralCommandHibernate
                    End Select
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, order, pObj.force)
                    Program.Connection.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Could not launch shutdown command")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Dim msg As String = ""
                Dim ret As Boolean = _
                    Wmi.Objects.cSystem.ShutdownRemoteComputer(pObj.type, pObj.force, _
                                                               con.wmiSearcher, msg)
                Try
                    _deg.Invoke(ret, pObj.type, msg)
                Catch ex As Exception
                    _deg.Invoke(False, pObj.type, ex.Message)
                End Try

            Case Else
                ' Local
                Dim ret As Boolean
                Select Case pObj.type
                    Case ShutdownType.Lock
                        ret = cSystem.Lock
                    Case ShutdownType.Logoff
                        ret = cSystem.Logoff(pObj.force)
                    Case ShutdownType.Poweroff
                        ret = cSystem.Poweroff(pObj.force)
                    Case ShutdownType.Restart
                        ret = cSystem.Restart(pObj.force)
                    Case ShutdownType.Shutdown
                        ret = cSystem.Shutdown(pObj.force)
                    Case ShutdownType.Sleep
                        ret = cSystem.Sleep(pObj.force)
                    Case ShutdownType.Hibernate
                        ret = cSystem.Hibernate(pObj.force)
                End Select
                _deg.Invoke(ret, pObj.type, Native.Api.Win32.GetLastError)
        End Select
    End Sub

End Class
