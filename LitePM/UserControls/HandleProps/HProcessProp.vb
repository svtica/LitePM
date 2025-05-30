
' Lite Process Monitor









'




'




Option Strict On

Public Class HProcessProp
    Inherits HXXXProp

    ' ProcessId of the process opened with the handle
    Private _pid As Integer
    Private _proc As cProcess

    Public Sub New(ByVal handle As cHandle)
        MyBase.New(handle)
        InitializeComponent()

        Try
            ' Extract the processId from the name of the handle
            ' The string is like : processName (processId)
            Dim nam As String = Me.TheHandle.Infos.Name
            Dim n2 As Integer = nam.IndexOf("(", nam.Length - 8)
            Dim n1 As Integer = nam.IndexOf(")", nam.Length - 2)

            If n2 > 0 AndAlso n1 > 0 Then
                _pid = Integer.Parse(nam.Substring(n2 + 1, n1 - n2 - 1))
                _proc = ProcessProvider.GetProcessById(_pid)
            Else
                _pid = 0
                _proc = Nothing
            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    ' Refresh infos
    Public Overrides Sub RefreshInfos()
        Me.cmdOpen.Enabled = (_proc IsNot Nothing)
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Dim frm As New frmProcessInfo
        frm.SetProcess(_proc)
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub HKeyProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.SetToolTip(Me.cmdOpen, "Show details about the process")
    End Sub
End Class
