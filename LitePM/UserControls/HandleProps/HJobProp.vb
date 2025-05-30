
' Lite Process Monitor









'




'




Option Strict On

Public Class HJobProp
    Inherits HXXXProp

    ' Associated job
    Private _job As cJob

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()

        ' Try to get the job
        _job = cJob.GetJobByName(Me.TheHandle.Infos.Name)

    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Me.cmdOpen.Enabled = (_job IsNot Nothing)
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Dim frm As New frmJobInfo
        frm.SetJob(_job)
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub HKeyProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.SetToolTip(Me.cmdOpen, "Show details about the job")
    End Sub
End Class
