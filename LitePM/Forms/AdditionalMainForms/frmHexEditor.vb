
' Lite Process Monitor


Option Strict On



Public Class frmHexEditor

    Public WithEvents _hex As New MemoryHexEditor
    Private _pid As Integer
    Private _region As MemoryHexEditor.MemoryRegion

    Public Sub SetPidAndRegion(ByVal pid As Integer, ByVal region As MemoryHexEditor.MemoryRegion)
        _pid = pid
        _region = region
        _hex.NewProc(_region, _pid)
    End Sub

    Private Sub frmHexEditor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Common.Misc.CloseWithEchapKey(Me)

        With _hex
            .BackColor = Color.White
            .Location = New Point(0, 0)
            .Size = New Size(Me.Width, Me.Height)
            .Dock = DockStyle.Left
        End With

        Me.Controls.Add(_hex)

    End Sub
End Class