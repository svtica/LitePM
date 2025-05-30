
' Lite Process Monitor


Option Strict On
Imports Common.Misc

Public Class frmNewVersionAvailable

    Private _infos As cUpdate.NewReleaseInfos

    Public Sub New(ByVal infos As cUpdate.NewReleaseInfos)
        InitializeComponent()
        _infos = infos
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmAboutG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        SetToolTip(Me.cmdDownload, "Download the new version now")
        SetToolTip(Me.btnOK, "Close this window")

        With _infos
            Dim desc As String = .Description
            If desc IsNot Nothing Then
                desc = desc.Replace(Chr(10), vbNewLine)
            End If
            Me.lblVersion.Text = .Version
            Me.lblDate.Text = .Date
            Me.txtDesc.Text = desc
            Me.lblCaption.Text = .Caption
            Me.lblType.Text = .Type
        End With

    End Sub

    Private Sub cmdDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDownload.Click
        cFile.ShellOpenFile(_infos.Url, Me.Handle)
    End Sub
End Class