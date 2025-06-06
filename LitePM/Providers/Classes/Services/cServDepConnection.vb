﻿
' Lite Process Monitor









'




'



Option Strict On

Public Class cServDepConnection
    Inherits cGeneralConnection

    Public Enum DependenciesToget
        ServiceWhichDependsFromMe
        DependenciesOfMe
    End Enum

    Friend Shared instanceId As Integer = 1
    Private _instanceId As Integer = 1
    Dim _servEnum As asyncCallbackServDepEnumerate

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection, ByRef de As HasEnumeratedEventHandler)
        MyBase.New(ControlWhichGetInvoked, Conn)
        instanceId += 1
        _instanceId = instanceId
        _servEnum = New asyncCallbackServDepEnumerate(_control, de, Me, _instanceId)
    End Sub


#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, serviceInfos), ByVal errorMessage As String, ByVal forII As Integer, ByVal type As cServDepConnection.DependenciesToget)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler
    ' Public HasEnumerated As HasEnumeratedEventHandler

#End Region

#Region "Description of the type of connection"

    ' Connection
    Protected Overrides Sub asyncConnect(ByVal useless As Object)

        ' Connect
        Select Case _conObj.Type
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
        Select Case _conObj.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
            Case Else
                ' Local
                _connected = False
                If Disconnected IsNot Nothing AndAlso _control.Created Then _
                    _control.Invoke(Disconnected, True)
        End Select
    End Sub

#End Region

#Region "Enumerate services"

    ' Enumerate services
    Public Function Enumerate(ByVal name As String, ByVal typ As DependenciesToget, Optional ByVal forInstanceId As Integer = -1) As Integer
        Call Threading.ThreadPool.QueueUserWorkItem(New _
                System.Threading.WaitCallback(AddressOf _
                _servEnum.Process), New _
                asyncCallbackServDepEnumerate.poolObj(name, typ, forInstanceId))
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
        If Program.Connection.IsConnected = False OrElse Program.Connection.Type <> cConnection.TypeOfConnection.RemoteConnectionViaSocket Then
            Exit Sub
        End If

        If data Is Nothing Then
            Trace.WriteLine("Serialization error")
            Exit Sub
        End If

        If data.Type = cSocketData.DataType.RequestedList AndAlso _
            data.Order = cSocketData.OrderType.RequestServDepList Then
            If _instanceId = data.InstanceId Then
                ' OK it is for me
                _servEnum.GotListFromSocket(data.GetList, data.GetKeys, CType(data.Param2, DependenciesToget))
            End If
        End If
    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
