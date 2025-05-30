
' Lite Process Monitor









'




'




Option Strict On

Public Class HKeyProp
    Inherits HXXXProp

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()
    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Me.cmdOpen.Enabled = (Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection)
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Common.Misc.NavigateToRegedit(Me.TheHandle.Infos.Name)
    End Sub

    Private Sub HKeyProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.SetToolTip(Me.cmdOpen, "Open the key in regedit")
    End Sub
End Class
