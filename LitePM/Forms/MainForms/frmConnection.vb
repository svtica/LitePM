﻿
' Lite Process Monitor


Option Strict On

Imports Common.Misc
Imports Native.Api.Enums


Public Class frmConnection

    Private REMOTE_PORT As Integer
    Private WithEvents _formConnectionReference As cConnection

    Public Delegate Sub AddItemToReceivedDataList(ByRef dat As cSocketData)

    Private _localDesc As String = "Local connection monitors all processes and services running on the local machine."
    Private _wmiDesc As String = "Remote connection via WMI monitors all processes and services running on a remote machine. You will need a username and password of the remote machine, WMI needs to be installed on both machines (your machine and the remote machine), and your firewall have to accept the connection. Furthermore, not all informations and actions on your processes are available. If possible, you should use 'remote via LitePM server' instead."
    Private _serverDes As String = "Remote connection via WMI LitePM server monitors all processes and services running on a remote machine. You will need the IP address of the remote machine, and the associated port must be available (you might need to configure your firewall). You will also need to run LitePMserv.exe on the remote machine. This is the best way, if possible, to monitor a remote machine."
    Private _snapshotDesc As String = "System Snapshot File allows to display informations about all processes and services which were running when the snapshot file has been saved."

    Public Sub New(ByRef connection As cConnection)
        InitializeComponent()
        _formConnectionReference = connection
    End Sub

    Private Sub optLocal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLocal.CheckedChanged
        Call changeInfos()
    End Sub

    Private Sub frmConnection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub frmConnection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Call HideWithEchapKey(Me)
        Call changeInfos()
        Native.Functions.Misc.SetTheme(Me.lvData.Handle)

        SetToolTip(Me.txtDesc, "Description of the type of connection")
        SetToolTip(Me.txtServerIP, "Name (or IP) of the server machine")
        SetToolTip(Me.txtServerMachine, "Name of the remote machine")
        SetToolTip(Me.txtServerPassword, "Specify a password of an account of the remote machine")
        SetToolTip(Me.txtServerUser, "Specify the user name of an account of the remote machine")
        SetToolTip(Me.cbShutdown, "Shutdown action")
        SetToolTip(Me.cmdCancel, "Hide this window")
        SetToolTip(Me.cmdConnect, "Connect or disconnect from the machine")
        SetToolTip(Me.cmdShutdown, "Shutdown the specified machine")
        SetToolTip(Me.chkForceShutdown, "Force (or not) the shutdown")
        SetToolTip(Me.optLocal, "Local connection")
        SetToolTip(Me.optServer, "Remote connection with the use of a server")
        SetToolTip(Me.optWMI, "Remote connection with the use of WMI")
        SetToolTip(Me.txtPort, "Port to use to connect to remote machine")
        SetToolTip(Me.cmdTerminal, "Start Microsoft terminal service client")
        SetToolTip(Me.cmdShowDatas, "Show/hide list of data received from remote machine")
        SetToolTip(Me.optSnapshot, "Use a 'system snaphost file' to display informations")
        SetToolTip(Me.txtSSFile, "Path of the System Snapshot File.")
        SetToolTip(Me.cmdBrowseSSFile, "Select the System Snapshot File.")
        SetToolTip(Me.cmdSSFileInfos, "Show information about the System Snapshot File.")

        Me.txtPort.Text = CStr(My.Settings.RemotePort)
        Me.txtServerMachine.Text = My.Settings.RemoteMachineNameW
        Me.txtServerIP.Text = My.Settings.RemoteMachineName
        Me.txtServerUser.Text = My.Settings.RemoteMachineUserW
        Me.cmdTerminal.Enabled = IO.File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.System) & "\mstsc.exe")

    End Sub

    Private Sub optServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optServer.CheckedChanged
        Call changeInfos()
    End Sub

    Private Sub optWMI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optWMI.CheckedChanged
        Call changeInfos()
    End Sub

    Private Sub changeInfos()

        Me.gpServer.Visible = optServer.Checked
        Me.lvData.Enabled = Me.optServer.Checked
        Me.gpWMI.Visible = optWMI.Checked
        Me.gpSnapshot.Visible = optSnapshot.Checked
        Me.cmdSSFileInfos.Enabled = Program.Connection.Type = cConnection.TypeOfConnection.SnapshotFile AndAlso Program.Connection.IsConnected

        If optLocal.Checked Then
            Me.txtDesc.Text = _localDesc
            Me.gpShutdown.Enabled = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected AndAlso _formConnectionReference.Type = cConnection.TypeOfConnection.LocalConnection)
        ElseIf optServer.Checked Then
            Me.txtDesc.Text = _serverDes
            Me.gpShutdown.Enabled = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected AndAlso _formConnectionReference.Type = cConnection.TypeOfConnection.RemoteConnectionViaSocket)
        ElseIf optSnapshot.Checked Then
            Me.txtDesc.Text = _snapshotDesc
            Me.gpShutdown.Enabled = False
        Else
            Me.txtDesc.Text = _wmiDesc
            Me.gpShutdown.Enabled = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected AndAlso _formConnectionReference.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI)
        End If

        Call updateShutdown()
    End Sub

    Private Sub updateShutdown()
        Static _oldType As cConnection.TypeOfConnection
        Static _oldConnected As Boolean = True

        If optLocal.Checked Then
            If _formConnectionReference IsNot Nothing AndAlso (_oldType <> _formConnectionReference.Type OrElse _formConnectionReference.IsConnected <> _oldConnected) Then
                Me.cbShutdown.Items.Clear()
                Dim _items() As String = {"Restart", "Shutdown", "Poweroff", "Sleep", "Logoff", "Lock"}
                Me.cbShutdown.Items.AddRange(_items)
                Me.gpShutdown.Text = "Shutdown local system"
            End If
            Me.txtDesc.Text = _localDesc
            Me.gpShutdown.Enabled = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected AndAlso _formConnectionReference.Type = cConnection.TypeOfConnection.LocalConnection)
            _oldType = cConnection.TypeOfConnection.LocalConnection
        ElseIf optServer.Checked Then
            If _formConnectionReference IsNot Nothing AndAlso (_oldType <> _formConnectionReference.Type OrElse _formConnectionReference.IsConnected <> _oldConnected) Then
                Me.cbShutdown.Items.Clear()
                Dim _items() As String = {"Restart", "Shutdown", "Poweroff", "Sleep", "Logoff", "Lock"}
                Me.cbShutdown.Items.AddRange(_items)
                Me.gpShutdown.Text = "Shutdown remote system via socket"
            End If
            Me.txtDesc.Text = _serverDes
            Me.gpShutdown.Enabled = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected AndAlso _formConnectionReference.Type = cConnection.TypeOfConnection.RemoteConnectionViaSocket)
            _oldType = cConnection.TypeOfConnection.RemoteConnectionViaSocket
        ElseIf optSnapshot.Checked Then
            ' Nothing updated here
        Else
            If _formConnectionReference IsNot Nothing AndAlso (_oldType <> _formConnectionReference.Type OrElse _formConnectionReference.IsConnected <> _oldConnected) Then
                Me.cbShutdown.Items.Clear()
                Dim _items() As String = {"Restart", "Shutdown", "Poweroff", "Logoff"}
                Me.cbShutdown.Items.AddRange(_items)
                Me.gpShutdown.Text = "Shutdown remote system via WMI"
            End If
            Me.txtDesc.Text = _wmiDesc
            Me.gpShutdown.Enabled = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected AndAlso _formConnectionReference.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI)
            _oldType = cConnection.TypeOfConnection.RemoteConnectionViaWMI
        End If

        _oldConnected = (_formConnectionReference IsNot Nothing AndAlso _formConnectionReference.IsConnected)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.LocalConnection
                Me.optLocal.Checked = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                Me.optServer.Checked = True
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                Me.optWMI.Checked = True
        End Select
        Me.Hide()
        Call updateShutdown()
    End Sub

    ' Here we (dis)connect !
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConnect.Click

        Dim clientIp As String = ""

        Dim _connType As cConnection.TypeOfConnection
        If optLocal.Checked Then
            _connType = cConnection.TypeOfConnection.LocalConnection
        ElseIf optServer.Checked Then
            _connType = cConnection.TypeOfConnection.RemoteConnectionViaSocket
        ElseIf optSnapshot.Checked Then
            _connType = cConnection.TypeOfConnection.SnapshotFile
        Else
            _connType = cConnection.TypeOfConnection.RemoteConnectionViaWMI
        End If

        If _formConnectionReference.IsConnected Then
            Me.Text = "Disconnecting from current machine..."
            Call _frmMain.DisconnectFromMachine()
        Else
            With Program.Connection
                .Type = _connType
                If _connType = cConnection.TypeOfConnection.RemoteConnectionViaSocket Then

                    ' First we have to get the available NICs
                    Dim nics As List(Of Native.Api.Structs.NicDescription) = Common.Misc.GetNics
                    If nics.Count = 0 Then
                        ' No network card !
                        Misc.ShowMsg("Connection to remote machine", "Failed to connect to remote machine." & " You cannot connect to the server as no network card is installed on your machine.", MessageBoxButtons.OK)
                        Exit Sub
                    ElseIf nics.Count = 1 Then
                        ' Only one card, OK !
                        clientIp = nics(0).Ip
                    Else
                        ' User have to choose the card to use
                        Dim frm As New frmChooseClientIp(nics)
                        frm.ShowDialog()
                        If frm.DialogResult <> Windows.Forms.DialogResult.OK Then
                            Exit Sub
                        End If
                        clientIp = frm.ChosenIp
                    End If

                    .SocketParameters = New cConnection.SocketConnectionParameters(Me.txtServerIP.Text, REMOTE_PORT, clientIp)
                ElseIf _connType = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                    .WmiParameters = New cConnection.WmiConnectionParameters(Me.txtServerMachine.Text, Me.txtServerUser.Text, Me.txtServerPassword.SecureText)
                ElseIf _connType = cConnection.TypeOfConnection.SnapshotFile Then
                    .SnapshotFile = Me.txtSSFile.Text
                End If
            End With
            Me.Text = "Connecting to machine..."
            Call _frmMain.ConnectToMachine()
        End If

    End Sub

    ' Change the caption of the button 'Connect/Disconnect'
    Private Sub ChangeCaption()
        If _formConnectionReference.IsConnected Then
            Me.cmdConnect.Text = "Disconnect"
            Me.Text = "Connected"
        Else
            Me.cmdConnect.Text = "Connect"
            Me.Text = "Disconnected"
        End If
        Call changeInfos()
    End Sub

    ' BAD WAY (because of withevents, this is raised JUST WHEN Program.Connection.Connect
    ' is call. BAD THING (should wait asyncMethod, but there are LOTS of asyncMethids
    ' (one for each lvItem).
    'Private Sub _formConnectionReference_Connected() Handles _formConnectionReference.Connected
    '    Call ChangeCaption()
    'End Sub
    'Private Sub _formConnectionReference_Disconnected() Handles _formConnectionReference.Disconnected
    '    Call ChangeCaption()
    'End Sub

    ' BAD WAY -> should kick all timers from project (see commented lines about delegates
    ' just below)
    Private Sub Timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer.Tick
        Call ChangeCaption()
    End Sub

    Private Sub frmConnection_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        Me.Timer.Enabled = Me.Visible
    End Sub

    'Private Sub _formConnectionReference_Connected() Handles _formConnectionReference.Connected
    '    Try
    '        Dim _deg As New degActivateShutdown(AddressOf ActivateShutdown)
    '        Me.Invoke(_deg, True)
    '    Catch ex As Exception
    '        '
    '    End Try
    'End Sub

    'Private Sub _formConnectionReference_Disconnected() Handles _formConnectionReference.Disconnected
    '    Try
    '        Dim _deg As New degActivateShutdown(AddressOf ActivateShutdown)
    '        Me.Invoke(_deg, False)
    '    Catch ex As Exception
    '        '
    '    End Try
    'End Sub

    'Private Delegate Sub degActivateShutdown(ByVal value As Boolean)
    'Private Sub ActivateShutdown(ByVal value As Boolean)
    '    Me.gpShutdown.Enabled = value
    'End Sub

    Private Sub txtPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPort.TextChanged
        My.Settings.RemotePort = CInt(Val(Me.txtPort.Text))
        REMOTE_PORT = My.Settings.RemotePort
    End Sub

#Region "Shutdown functions"

    Private _shutdownAction As asyncCallbackShutdownAction
    Public Function ShutdownAction(ByVal type As ShutdownType, ByVal force As Boolean) As Integer

        If _shutdownAction Is Nothing Then
            _shutdownAction = New asyncCallbackShutdownAction(New asyncCallbackShutdownAction.HasShutdowned(AddressOf shutdownDone), Program._frmMain._shutdownConnection)
        End If

        Dim t As New System.Threading.WaitCallback(AddressOf _shutdownAction.Process)

        Call Threading.ThreadPool.QueueUserWorkItem(t, New _
            asyncCallbackShutdownAction.poolObj(type, force))

    End Function
    Private Sub shutdownDone(ByVal Success As Boolean, ByVal type As ShutdownType, ByVal msg As String)
        If Success = False Then
            Misc.ShowMsg("Shutdown command", "Could not send " & type.ToString & " command." & msg, MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub cmdShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShutdown.Click
        Select Case Me.cbShutdown.Text
            Case "Restart"
                Call ShutdownAction(ShutdownType.Restart, Me.chkForceShutdown.Checked)
            Case "Shutdown"
                Call ShutdownAction(ShutdownType.Shutdown, Me.chkForceShutdown.Checked)
            Case "Poweroff"
                Call ShutdownAction(ShutdownType.Poweroff, Me.chkForceShutdown.Checked)
            Case "Sleep"
                Call ShutdownAction(ShutdownType.Sleep, Me.chkForceShutdown.Checked)
            Case "Logoff"
                Call ShutdownAction(ShutdownType.Logoff, Me.chkForceShutdown.Checked)
            Case "Lock"
                Call ShutdownAction(ShutdownType.Lock, Me.chkForceShutdown.Checked)
        End Select
    End Sub

#End Region

    Private Sub cmdTerminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTerminal.Click
        cFile.ShellOpenFile(System.Environment.GetFolderPath(Environment.SpecialFolder.System) & "\mstsc.exe", Me.Handle)
    End Sub

    Private Sub txtServerMachine_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServerMachine.TextChanged
        My.Settings.RemoteMachineNameW = Me.txtServerMachine.Text
    End Sub

    Private Sub txtServerUser_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServerUser.TextChanged
        My.Settings.RemoteMachineUserW = Me.txtServerUser.Text
    End Sub

    Private Sub txtServerIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtServerIP.TextChanged
        My.Settings.RemoteMachineName = Me.txtServerIP.Text
    End Sub

    Private Sub cmdShowDatas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdShowDatas.Click
        If Me.Width = 643 Then
            Me.Width = 347
            Me.cmdShowDatas.Text = "Show received data"
        Else
            Me.Width = 643
            Me.cmdShowDatas.Text = "Hide received data"
        End If
    End Sub

    ' Add an item to the list of data received from remote server
    Public Sub impAddItemToReceivedDataList(ByRef dat As cSocketData)
        Dim it As New ListViewItem(Date.Now.ToLongDateString & " - " & Date.Now.ToLongTimeString)
        it.SubItems.Add(dat.ToString)
        Me.lvData.Items.Add(it)
    End Sub

    Private Sub cmdBrowseSSFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseSSFile.Click
        Dim ret As DialogResult = Me.openFile.ShowDialog
        If ret = Windows.Forms.DialogResult.OK Then
            Me.txtSSFile.Text = Me.openFile.FileName
        End If
    End Sub

    Private Sub cmdSSFileInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSSFileInfos.Click
        ' Display informations about the snapshot
        Dim snap As cSnapshot250 = Program.Connection.Snapshot
        If snap IsNot Nothing Then
            Dim s As String = vbNewLine & "FILE INFO" & vbNewLine & vbNewLine & _
                    "Date : " & snap.Date & vbNewLine & _
                    "FileVersion : " & snap.FileVersion & vbNewLine & vbNewLine
            If snap.SystemInformation IsNot Nothing Then
                s &= "SYSTEM INFO" & vbNewLine & vbNewLine & _
                    "ComputerName : " & snap.SystemInformation.ComputerName & vbNewLine & _
                    "UserName : " & snap.SystemInformation.UserName & vbNewLine & _
                    "Culture : " & snap.SystemInformation.Culture.ToString & vbNewLine & _
                    "IntPtrSize : " & snap.SystemInformation.IntPtrSize & vbNewLine & _
                    "OSFullName : " & snap.SystemInformation.OSFullName & vbNewLine & _
                    "OSPlatform : " & snap.SystemInformation.OSPlatform & vbNewLine & _
                    "OSVersion : " & snap.SystemInformation.OSVersion & vbNewLine & _
                    "AvailablePhysicalMemory : " & GetFormatedSize(snap.SystemInformation.AvailablePhysicalMemory) & vbNewLine & _
                    "AvailableVirtualMemory : " & GetFormatedSize(snap.SystemInformation.AvailableVirtualMemory) & vbNewLine & _
                    "TotalPhysicalMemory : " & GetFormatedSize(snap.SystemInformation.TotalPhysicalMemory) & vbNewLine & _
                    "TotalVirtualMemory : " & GetFormatedSize(snap.SystemInformation.TotalVirtualMemory)
            End If
            Misc.ShowMsg("System Snapshot File information", "Informations about the file " & Program.Connection.SnapshotFile, MessageBoxButtons.OK)
        End If
    End Sub
End Class