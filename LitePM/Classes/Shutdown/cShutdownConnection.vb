
' Lite Process Monitor


Option Strict On
Imports System.Management

Public Class cShutdownConnection
    Inherits cGeneralConnection

    Public Sub New(ByVal ControlWhichGetInvoked As Control, ByRef Conn As cConnection)
        MyBase.New(ControlWhichGetInvoked, Conn)
    End Sub

#Region "Events, delegate, invoke..."

    Public Delegate Sub ConnectedEventHandler(ByVal Success As Boolean)
    Public Delegate Sub DisconnectedEventHandler(ByVal Success As Boolean)

    Public Connected As ConnectedEventHandler
    Public Disconnected As DisconnectedEventHandler

#End Region

#Region "Description of the type of connection"

    ' Connect
    Protected Overrides Sub asyncConnect(ByVal useless As Object)

        ' Connect
        Select Case _conObj.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' When we are here, the socket IS CONNECTED
                _sock = ConnectionObj.Socket
                _connected = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                Dim __con As New ConnectionOptions
                __con.Impersonation = ImpersonationLevel.Impersonate
                __con.Password = Common.Misc.SecureStringToCharArray(_conObj.WmiParameters.password)
                __con.Username = _conObj.WmiParameters.userName

                Try
                    wmiSearcher = New Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
                    wmiSearcher.Scope = New Management.ManagementScope("\\" & _conObj.WmiParameters.serverName & "\root\cimv2", __con)
                    _connected = True
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

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

    Protected Overrides Sub asyncDisconnect(ByVal useless As Object)
        Select Case _conObj.Type
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

#Region "Sock events"

    Protected Overrides Sub _sock_Connected() Handles _sock.Connected
        _connected = True
    End Sub

    Protected Overrides Sub _sock_Disconnected() Handles _sock.Disconnected
        _connected = False
    End Sub

    Protected Shadows Sub _sock_ReceivedData(ByRef data As cSocketData) Handles _sock.ReceivedData
        '
    End Sub

    Protected Overrides Sub _sock_SentData() Handles _sock.SentData
        '
    End Sub

#End Region

End Class
