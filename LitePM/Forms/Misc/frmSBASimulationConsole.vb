
' Lite Process Monitor

Option Strict On

Public Class frmSBASimulationConsole

    Private Sub frmSBASimulationConsole_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
    End Sub

    Private Sub frmSBASimulationConsole_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Native.Api.NativeFunctions.EnableMenuItem(Native.Api.NativeFunctions.GetSystemMenu(Me.Handle, False), Native.Api.NativeConstants.SC_CLOSE, Native.Api.NativeConstants.MF_GRAYED)
        Native.Functions.Misc.SetTheme(lv.Handle)
        Native.Functions.Misc.SetListViewAsDoubleBuffered(Me.lv)
    End Sub

    Private Sub ClearConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearConsoleToolStripMenuItem.Click
        Me.lv.Items.Clear()
    End Sub
End Class