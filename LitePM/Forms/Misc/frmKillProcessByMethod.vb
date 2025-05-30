
' Lite Process Monitor


Option Strict On

Imports Common.Misc

Public Class frmKillProcessByMethod

    Private _proc As cProcess
    Public Property ProcessToKill() As cProcess
        Get
            Return _proc
        End Get
        Set(ByVal value As cProcess)
            _proc = value
        End Set
    End Property

    Private Sub frmKillProcessByMethod_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.Misc.CloseWithEchapKey(Me)
        SetToolTip(Me.lstMethods, "List of available methods to kill the process")
        SetToolTip(Me.cmdExit, "Exit")
        SetToolTip(Me.cmdKill, "Kill process with selected methods")
        Native.Functions.Misc.SetTheme(Me.lstMethods.Handle)

        ' Add all methods to the listview
        Me.lstMethods.Items.Add("Use NtTerminateProcess").Tag = Native.Api.Enums.KillMethod.NtTerminate
        Me.lstMethods.Items.Add("Terminate all threads").Tag = Native.Api.Enums.KillMethod.ThreadTerminate
        If cEnvironment.SupportsGetNextThreadProcessFunctions Then
            Me.lstMethods.Items.Add("Terminate all threads (other method)").Tag = Native.Api.Enums.KillMethod.ThreadTerminate_GetNextThread
        End If
        'Me.lstMethods.Items.Add("Create remote thread and call ExitProcess").Tag = Native.Api.Enums.KillMethod.CreateRemoteThread
        Me.lstMethods.Items.Add("Close all handles").Tag = Native.Api.Enums.KillMethod.CloseAllHandles
        Me.lstMethods.Items.Add("Close all windows").Tag = Native.Api.Enums.KillMethod.CloseAllWindows
        Me.lstMethods.Items.Add("Assign process to a job and terminate job").Tag = Native.Api.Enums.KillMethod.TerminateJob
        Me.lstMethods.Items.Add("All methods").Tag = Native.Api.Enums.KillMethod.All

        Me.Text = Me.Text.Replace("[PID]", _proc.Infos.ProcessId.ToString).Replace("[NAME]", _proc.Infos.Name)
    End Sub

    Private Sub cmdKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKill.Click
        Dim methods As Native.Api.Enums.KillMethod
        For Each it As ListViewItem In Me.lstMethods.CheckedItems
            If CType(it.Tag, Native.Api.Enums.KillMethod) <> Native.Api.Enums.KillMethod.All Then
                methods = methods Or CType(it.Tag, Native.Api.Enums.KillMethod)
            End If
        Next

        If WarnDangerousAction("Are you sure you want to kill this process ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        ' Now call method to kill
        _proc.KillByMethod(methods)
    End Sub

    Private Sub lstMethods_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles lstMethods.ItemChecked

        Me.cmdKill.Enabled = (Me.lstMethods.CheckedItems.Count > 0)

        If CType(e.Item.Tag, Native.Api.Enums.KillMethod) = Native.Api.Enums.KillMethod.All Then
            If e.Item.Checked Then
                For Each it As ListViewItem In Me.lstMethods.Items
                    it.Checked = True
                Next
            End If
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

End Class