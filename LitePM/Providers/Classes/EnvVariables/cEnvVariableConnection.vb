﻿
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Windows.Forms
Imports System.Management
Imports System.Net.Sockets
Imports System.Text

Public Class cEnvVariableConnection
    Inherits cGeneralConnection

    Friend Shared instanceId As Integer = 1
    Private _instanceId As Integer = 1
    Private _envVarEnum As asyncCallbackEnvVariableEnumerate

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection, ByRef de As HasEnumeratedEventHandler)
        MyBase.New(ControlWhichGetInvoked, Conn)
        instanceId += 1
        _instanceId = instanceId
        _envVarEnum = New asyncCallbackEnvVariableEnumerate(_control, de, Me, _instanceId)
    End Sub

#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, envVariableInfos), ByVal errorMessage As String, ByVal InstanceId As Integer)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler
    Public HasEnumerated As HasEnumeratedEventHandler

#End Region

#Region "Description of the type of connection"

    ' Connection
    Protected Overrides Sub asyncConnect(ByVal useless As Object)

        ' Connect
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' When we are here, the socket IS CONNECTED
                _sock = ConnectionObj.Socket
                _connected = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                _connected = True
                Try
                    If Connected IsNot Nothing AndAlso _control.Created Then _
                        _control.Invoke(Connected, True)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
        End Select

    End Sub

    ' Disconnect
    Protected Overrides Sub asyncDisconnect(ByVal useless As Object)
        Select Case _conObj.ConnectionType
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case Else
                ' Local
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
        End Select
    End Sub

#End Region

#Region "Enumerate privileges"

    ' Enumerate threads
    Public Function Enumerate(ByVal getFixedInfos As Boolean, ByRef pid As Integer, ByVal peb As IntPtr, Optional ByVal forInstanceId As Integer = -1) As Integer
        Dim _pe As IntPtr = peb
        If _pe = Native.Api.NativeConstants.InvalidHandleValue Then
            Native.Objects.Process.SemCurrentProcesses.WaitOne()
            _pe = Native.Objects.Process.CurrentProcesses(pid.ToString).Infos.PebAddress
            Native.Objects.Process.SemCurrentProcesses.Release()
        End If
        Call Threading.ThreadPool.QueueUserWorkItem(New  _
            System.Threading.WaitCallback(AddressOf _
            _envVarEnum.Process), New  _
            asyncCallbackEnvVariableEnumerate.poolObj(pid, _pe, forInstanceId))
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
            data.Order = cSocketData.OrderType.RequestEnvironmentVariableList Then
            If _instanceId = data.InstanceId Then
                ' OK it is for me
                _envVarEnum.GotListFromSocket(data.GetList, data.GetKeys)
            End If
        End If
    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
