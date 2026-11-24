
' Lite Process Monitor


Option Strict On
Imports Common.Misc

Public Class frmError

    Private _canClose As Boolean = False

    Public Sub New(ByVal e As Exception)

        InitializeComponent()

        ' Create a log
        Dim s As String = Program.ErrorLog(e)
        s &= vbNewLine & vbNewLine
        s &= "Modules : "
        For Each mdl As ProcessModule In System.Diagnostics.Process.GetCurrentProcess.Modules
            s &= vbNewLine & vbTab & mdl.ModuleName
            s &= vbNewLine & vbTab & vbTab & "Path : " & mdl.FileName
            If mdl.FileVersionInfo IsNot Nothing Then
                s &= vbNewLine & vbTab & vbTab & "Version : " & mdl.FileVersionInfo.FileVersion
            End If
        Next

        Me.txtReport.Text = s

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdQuit.Click
        Native.Api.NativeFunctions.ExitProcess(0)
    End Sub

    Private Sub frmError_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = Not (_canClose)
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContinue.Click
        _canClose = True
        Me.Close()
    End Sub

    Private Sub frmError_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetToolTip(Me.cmdQuit, "Terminate LitePM immediatly")
        SetToolTip(Me.cmdContinue, "Cloe this window and try to continue the execution of LitePM. It's probably gonna crash")
        SetToolTip(Me.cmdIgnore, "Hide this window and ignore the error")
        SetToolTip(Me.cmdCopy, "Copy the error report to the clipboard")
    End Sub

    Private Sub cmdIgnore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdIgnore.Click
        Me.Hide()
    End Sub

    Private Sub cmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopy.Click
        Try
            My.Computer.Clipboard.SetText(Me.txtReport.Text)
        Catch ex As Exception
            Misc.ShowMsg(Localization.Format("Could not copy the error report to the clipboard." & vbCrLf & "{0}", ex.Message), "Copy error report", MessageBoxButtons.OK)
        End Try
    End Sub

End Class
