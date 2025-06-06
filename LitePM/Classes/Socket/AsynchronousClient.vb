﻿
' Lite Process Monitor

Option Strict On
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp
Imports System.Threading
Imports RemotingServerClient
Imports MsdnMag.Remoting

Public Class AsynchronousClient

    Public Delegate Sub ReceivedDataEventHandler(ByRef data As cSocketData)
    Public Delegate Sub SentDataEventHandler()
    Public Delegate Sub DisconnectedEventHandler()
    Public Delegate Sub ConnectedEventHandler()
    Public Delegate Sub SocketErrorHandler()

    Public Event ReceivedData As ReceivedDataEventHandler
    Public Event SentData As SentDataEventHandler
    Public Event Disconnected As DisconnectedEventHandler
    Public Event Connected As ConnectedEventHandler
    Public Event SocketError As SocketErrorHandler

    ' Channel to connect
    Private _theChannel As TcpChannel

    Private _uniqueClientKey As String = "cDat._id"

    Private _ServerTalk As ServerTalk = Nothing
    ' this object lives on the server
    Private _CallbackSink As CallbackSink = Nothing
    ' this object lives here on the client

    Private _connected As Boolean = False

    Public ReadOnly Property IsConnected() As Boolean
        Get
            Return _connected
        End Get
    End Property

    Private Structure poolObjConnect
        Public ServerName As String
        Public ClientIp As String
        Public Port As Integer
        Public Sub New(ByVal aServer As String, ByVal aPort As Integer, ByVal aClient As String)
            ServerName = aServer
            Port = aPort
            ClientIp = aClient
        End Sub
    End Structure

    Public Sub Connect(ByVal serverName As String, ByVal port As Integer, ByVal clientIp As String)
        ThreadPool.QueueUserWorkItem(AddressOf pvtConnect, New poolObjConnect(serverName, port, clientIp))
    End Sub

    Public Sub Disconnect()
        ServerTalk.ClientToServerQueue.Clear()
        _ServerTalk.SendMessageToServer(New CommsInfo("clientDisconnect"))
        ChannelServices.UnregisterChannel(_theChannel)
        _connected = False
        RaiseEvent Disconnected()
    End Sub

    Public Sub Send(ByVal dat As cSocketData)
        ' Add the object to send into the list (queue)
        'semQueue.WaitOne()
        dat._id = _uniqueClientKey
        ThreadPool.QueueUserWorkItem(AddressOf pvtSend, CObj(dat))
    End Sub


    Private Sub pvtConnect(ByVal context As Object)
        Try
            Dim pObj As poolObjConnect = CType(context, poolObjConnect)
            ' creates a client object that 'lives' here on the client.
            _CallbackSink = New CallbackSink()
            ' hook into the event exposed on the Sink object so we can transfer a server 
            ' message through to this class.
            AddHandler _CallbackSink.OnHostToClient, AddressOf CallbackSink_OnHostToClient
            ' Register a client channel so the server can communicate back - it needs a channel
            ' opened for the callback to the CallbackSink object that is anchored on the client!
            Dim channel As TcpChannel = Nothing
            Try
                ' Now we'll create a channel for each network card interface
                Dim ht As New Hashtable()
                ht("name") = "ClientChannel"
                ht("port") = pObj.Port + 3
                ht("bindTo") = pObj.ClientIp

                ' now create and register our custom TcpChannel 
                Dim serverFormatter As New BinaryServerFormatterSinkProvider
                serverFormatter.Next = New MsdnMag.Remoting.SecureServerChannelSinkProvider()
                serverFormatter.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
                channel = New TcpChannel(ht, Nothing, serverFormatter)
                ChannelServices.RegisterChannel(channel, False)

                ' Save the channel, so we'll be able to unregister it when disconnecting
                _theChannel = channel

                ' now create a transparent proxy to the server component
                Dim obj As Object = Activator.GetObject(GetType(ServerTalk), "tcp://" & pObj.ServerName & ":" & pObj.Port.ToString & "/TalkIsGood")
                ' cast returned object
                _ServerTalk = DirectCast(obj, ServerTalk)
                ' Register ourselves to the server with a callback to the client sink.
                _ServerTalk.RegisterHostToClient("client", New delCommsInfo(AddressOf _CallbackSink.HandleToClient))
                _connected = True
                RaiseEvent Connected()

            Catch ex As Exception
                ' Already exists (reconnection)
                Misc.ShowDebugError(ex)
            End Try
        Catch ex As Exception
            _connected = False
            RaiseEvent Disconnected()
        End Try
    End Sub

    Private Sub pvtSend(ByVal dat As Object)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = cSerialization.GetSerializedObject(CType(dat, cSocketData))

        Try
            _ServerTalk.SendMessageToServer(New CommsInfo(byteData))
            RaiseEvent SentData()
        Catch ex As Exception
            RaiseEvent Disconnected()
        End Try
    End Sub

    Private Sub CallbackSink_OnHostToClient(ByVal info As CommsInfo)
        ' Received a message
        Dim cDat As cSocketData = cSerialization.DeserializeObject(info.Data)
        RaiseEvent ReceivedData(cDat)
    End Sub
End Class