
' Lite Process Monitor


Option Strict On

Imports Common.Misc

Public Class frmWindowPosition

    ' Default values
    Private defR As Native.Api.NativeStructs.Rect

    ' New positions (defR must be initialized by cmdOk_Click)
    Public ReadOnly Property NewRect() As Native.Api.NativeStructs.Rect
        Get
            Return defR
        End Get
    End Property

    ' Define current position of form
    Public Sub SetCurrentPositions(ByVal r As Native.Api.NativeStructs.Rect)
        defR = r
        Me.txtHeight.Text = CStr(r.Bottom - r.Top)
        Me.txtLeft.Text = CStr(r.Left)
        Me.txtTop.Text = CStr(r.Top)
        Me.txtWidth.Text = CStr(r.Right - r.Left)
    End Sub

    Private Sub cmdDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefault.Click
        Call SetCurrentPositions(Me.defR)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        With defR
            .Bottom = CInt(Val(Me.txtTop.Text) + Val(Me.txtHeight.Text))
            .Left = CInt(Val(Me.txtLeft.Text))
            .Right = CInt(Val(Me.txtLeft.Text) + Val(Me.txtWidth.Text))
            .Top = CInt(Val(Me.txtTop.Text))
        End With
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmWindowPosition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        SetToolTip(Me.txtHeight, "Height of the form")
        SetToolTip(Me.txtLeft, "Left position of the form")
        SetToolTip(Me.txtWidth, "Width of the form")
        SetToolTip(Me.txtTop, "Top position of the form")
        SetToolTip(Me.cmdDefault, "Reset values")
        SetToolTip(Me.cmdOK, "Validate values")
        SetToolTip(Me.cmdCenter, "Center on screen")

    End Sub

    Private Sub cmdCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCenter.Click
        Dim w As Integer = CInt(Val(Me.txtWidth.Text))
        Dim h As Integer = CInt(Val(Me.txtHeight.Text))
        Dim l As Integer = (Screen.PrimaryScreen.Bounds.Width - w) \ 2
        Dim t As Integer = (Screen.PrimaryScreen.Bounds.Height - h) \ 2
        Me.txtLeft.Text = l.ToString
        Me.txtTop.Text = t.ToString
    End Sub
End Class