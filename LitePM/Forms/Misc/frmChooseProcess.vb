
' Lite Process Monitor


Option Strict On

Public Class frmChooseProcess

    Private _cproc As cProcess

    Public ReadOnly Property SelectedProcess() As cProcess
        Get
            Return _cproc
        End Get
    End Property

    Private Sub timerProcRefresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcRefresh.Tick
        lvProcess.UpdateTheItems()
    End Sub

    Private Sub lvProcess_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvProcess.DoubleClick
        If lvProcess.SelectedItems.Count > 0 Then
            _cproc = lvProcess.GetSelectedItem
            Me.Close()
        End If
    End Sub

    Private Sub frmChooseProcess_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.timerProcRefresh.Enabled = False
    End Sub

    Private Sub frmChooseProcess_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Common.Misc.CloseWithEchapKey(Me)

        Native.Functions.Misc.SetTheme(Me.lvProcess.Handle)
        lvProcess.UpdateTheItems()
    End Sub
End Class