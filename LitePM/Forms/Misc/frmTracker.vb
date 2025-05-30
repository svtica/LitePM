
' Lite Process Monitor


Option Strict On

Imports Common.Misc

Public Class frmTracker

    Private Sub frmTracker_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CloseWithEchapKey(Me)
        SetToolTip(Me.cmdGoBug, "Navigate to the bug tracker")
        SetToolTip(Me.cmdGoFeed, "Send a feed back")
        SetToolTip(Me.cmdGoSug, "Navigate to the forums of LitePM on sourceforge.net")

        Dim s As String = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036\deflangfe1036\deftab708{\fonttbl{\f0\fswiss\fprq2\fcharset0 Arial;}}"
        s &= "{\colortbl ;\red255\green0\blue0;\red0\green0\blue255;}"
        s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\lang1033\f0\fs22 If you have any suggestion or question or idea of improvement/new feature about\i  \ul Lite Process Monitor\ulnone\i0 , please feel free to contact me (use one of the method below: tracker, forum, email).\par"
        s &= "\par"
        s &= "\cf1 If you find a bug, \cf0\b PLEASE\b0  use the sourceforge.net tracker and specify, if possible (it would be very helpful for me) these informations :\par"
        s &= "\pard\fi-360\li720 -\tab the version of LitePM you are using\par"
        s &= "-\tab the operating system you are using\par"
        s &= "-\tab\b a description of the bug you found \par"
        s &= "\b0 -\tab\b how to reproduce it\b0  (if possible, it would be so helpful !)\par"
        s &= "\pard\par"
        s &= " Any feed back is appreciated !\par"
        s &= "}"
        Me.rtb.Rtf = s
    End Sub

    Private Sub cmdGoBug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoBug.Click
        cFile.ShellOpenFile(Me.txtBug.Text, Me.Handle)
    End Sub

    Private Sub cmdGoSug_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoSug.Click
        cFile.ShellOpenFile(Me.txtSug.Text, Me.Handle)
    End Sub

    Private Sub cmdGoFeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoFeed.Click
        cFile.ShellOpenFile("mailto:YetAnotherProcessMonitor@gmail.com", Me.Handle)
    End Sub
End Class