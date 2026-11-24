
' Lite Process Monitor

' Base class of the windows of LitePM: when a window loads, its texts are
' translated (Localization.Apply) and the light or dark display is applied
' (Theme.Apply), before its own Load handlers run. Nothing happens in the
' Visual Studio designer.

Option Strict On

Imports System.ComponentModel
Imports System.Windows.Forms

Public Class LocalizedForm
    Inherits Form

    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        If Not Me.DesignMode AndAlso LicenseManager.UsageMode <> LicenseUsageMode.Designtime Then
            Localization.Apply(Me)
            Theme.Apply(Me)
        End If
        MyBase.OnLoad(e)
    End Sub

End Class
