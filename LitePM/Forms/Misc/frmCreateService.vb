﻿
' Lite Process Monitor


Option Strict On

Public Class frmCreateService

    Private Sub frmAddToJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Common.Misc.SetToolTip(Me.OK_Button, "Create service")
        Common.Misc.SetToolTip(Me.Cancel_Button, "Cancel")
        Common.Misc.SetToolTip(Me.txtArgs, "Arguments used in command line")
        Common.Misc.SetToolTip(Me.txtDisplayName, "Service display name")
        Common.Misc.SetToolTip(Me.txtMachine, "Machine name")
        Common.Misc.SetToolTip(Me.txtPath, "Path of the executable file")
        Common.Misc.SetToolTip(Me.txtServerPassword, "Password")
        Common.Misc.SetToolTip(Me.txtServiceName, "Service name")
        Common.Misc.SetToolTip(Me.txtUser, "User name")
        Common.Misc.SetToolTip(Me.cbErrControl, "Error control")
        Common.Misc.SetToolTip(Me.cbServType, "Service type")
        Common.Misc.SetToolTip(Me.cbStartType, "Service start type")
        Common.Misc.SetToolTip(Me.cmdBrowse, "Browse for executable file")
        Common.Misc.SetToolTip(Me.optLocal, "Create service on the local machine")
        Common.Misc.SetToolTip(Me.optRemote, "Create service on a remote machine")

        Common.Misc.CloseWithEchapKey(Me)

        Me.cbErrControl.SelectedIndex = 1
        Me.cbServType.SelectedIndex = 4
        Me.cbStartType.SelectedIndex = 3

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim params As New Native.Api.Structs.ServiceCreationParams

        Me.OK_Button.Enabled = False
        Me.Cancel_Button.Enabled = False

        ' Create service
        With params
            .Arguments = Me.txtArgs.Text
            .DisplayName = Me.txtDisplayName.Text
            .ErrorControl = Native.Functions.Service.GetServiceErrorControlFromString(Me.cbErrControl.Text)
            .FilePath = Me.txtPath.Text
            .ServiceName = Me.txtServiceName.Text
            .StartType = Native.Functions.Service.GetServiceStartTypeFromString(Me.cbStartType.Text)
            .Type = Native.Functions.Service.GetServiceTypeFromString(Me.cbServType.Text)
            If Me.optRemote.Checked Then
                .RegMachine = Me.txtMachine.Text
                .RegPassword = Me.txtServerPassword.SecureText
                .RegUser = Me.txtUser.Text
            End If
        End With
        If Native.Objects.Service.CreateService(params) Then
            Me.Close()
        Else
            Misc.ShowMsg("Create service", "Failed to create the service." & " Informations : " & Native.Api.Win32.GetLastError, MessageBoxButtons.OK)
        End If

        Me.OK_Button.Enabled = True
        Me.Cancel_Button.Enabled = True
    End Sub

    Private Sub optLocal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLocal.CheckedChanged
        Me.txtUser.Enabled = optRemote.Checked
        Me.txtMachine.Enabled = optRemote.Checked
        Me.txtServerPassword.Enabled = optRemote.Checked
        Me.lblMachine.Enabled = optRemote.Checked
        Me.lblPwd.Enabled = optRemote.Checked
        Me.lblUser.Enabled = optRemote.Checked
    End Sub

    Private Sub optRemote_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optRemote.CheckedChanged
        Call optLocal_CheckedChanged(Nothing, Nothing)
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        ' Open a file
        With Me.openDial
            .Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*"
            .Title = "Select service executable"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtPath.Text = .FileName
            End If
        End With
    End Sub
End Class
