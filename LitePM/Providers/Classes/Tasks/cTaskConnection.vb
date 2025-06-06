﻿
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Windows.Forms
Imports System.Management
Imports System.Net.Sockets
Imports System.Text

Public Class cTaskConnection
    Inherits cWindowConnection

    Private _instanceId As Integer = 1
    Dim _taskEnum As asyncCallbackTaskEnumerate

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection, ByRef de As HasEnumeratedEventHandler)
        MyBase.New(ControlWhichGetInvoked, Conn, de)
        instanceId += 1
        _instanceId = instanceId
        _taskEnum = New asyncCallbackTaskEnumerate(_control, de, Me, _instanceId)
    End Sub

#Region "Enumerate threads"

    ' Enumerate threads
    Public Overloads Function Enumerate(ByVal getFixedInfos As Boolean, Optional ByVal forInstanceId As Integer = -1) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
                System.Threading.WaitCallback(AddressOf _
                _taskEnum.Process), New  _
                asyncCallbackTaskEnumerate.poolObj(forInstanceId))
    End Function

#End Region

#Region "Sock events"

    Protected Overrides Sub _sock_Connected() Handles _sock.Connected
        _connected = True
    End Sub

    Protected Overrides Sub _sock_Disconnected() Handles _sock.Disconnected
        _connected = False
    End Sub

    Protected Shadows Sub _sock_ReceivedData(ByRef data As cSocketData) Handles _sock.ReceivedData

        ' Exit immediately if not connected
        If Program.Connection.IsConnected = False OrElse Program.Connection.ConnectionType <> cConnection.TypeOfConnection.RemoteConnectionViaSocket Then
            Exit Sub
        End If

        If data Is Nothing Then
            Trace.WriteLine("Serialization error")
            Exit Sub
        End If

        If data.Type = cSocketData.DataType.RequestedList AndAlso _
            data.Order = cSocketData.OrderType.RequestTaskList Then
            If _instanceId = data.InstanceId Then
                ' OK it is for me
                _taskEnum.GotListFromSocket(data.GetList, data.GetKeys)
            End If
        End If
    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
