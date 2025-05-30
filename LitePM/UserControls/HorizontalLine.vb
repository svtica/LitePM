
' Lite Process Monitor









'




'




Option Strict On

Public Class HorizontalLine
    Inherits System.Windows.Forms.Control

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Me.Height = 1
    End Sub

End Class