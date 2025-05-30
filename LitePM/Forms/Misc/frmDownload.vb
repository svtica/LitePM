
' Lite Process Monitor

Option Strict On

Public Class frmDownload

    Private WithEvents _download As cDownload
    Public Property DownloadObject() As cDownload
        Get
            Return _download
        End Get
        Set(ByVal value As cDownload)
            _download = value
        End Set
    End Property

    Private Sub _download_CompleteCallback(ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles _download.CompleteCallback
        Misc.ShowMsg("Download", "Download complete.", MessageBoxButtons.OK)
        Me.Cancel_Button.Text = "OK"
        cFile.OpenDirectory(Me._download.Destination)
    End Sub

    Private Sub _download_ProgressCallback(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs) Handles _download.ProgressCallback
        Try
            Dim s As String = "Downloaded " & CStr(Math.Round(e.BytesReceived / 1024, 3)) & "KB out of " & CStr(Math.Round(e.TotalBytesToReceive / 1024, 3)) & "KB"
            Me.Text = "Downloading last update....  " & CStr(e.ProgressPercentage) & " %"
            With pgb
                .Maximum = 100
                .Minimum = 0
                .Value = e.ProgressPercentage
            End With
            Me.lblProgress.Text = s
            Application.DoEvents()

            'If e.ProgressPercentage = 100 Then
            '    Call _download_CompleteCallback(Nothing)
            'End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Public Sub StartDownload(ByVal dest As String)
        Me.Text = "Downloading in preparation..."
        Me.pgb.Value = 0
        Me.txtPath.Text = dest
        Me.pgb.Maximum = 1
        Me.Text = "Waiting for download..."
        Application.DoEvents()
        _download.StartDownload()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Try
            _download.Cancel()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
        Me.Close()
    End Sub

    Private Sub frmDownload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.SetToolTip(Me.Cancel_Button, "Cancel download")
    End Sub
End Class
