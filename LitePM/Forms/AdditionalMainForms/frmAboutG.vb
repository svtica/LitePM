
' Lite Process Monitor


Option Strict On
Imports Common.Misc


Public Class frmAboutG

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmAboutG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)
        SetToolTip(Me.btnOK, "Close this window")
        Me.lblVersion.Text = My.Application.Info.Version.ToString
        Me.lblDate.Text = "Sept 2024"

    End Sub

End Class