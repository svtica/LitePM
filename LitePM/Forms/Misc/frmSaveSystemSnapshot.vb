
' Lite Process Monitor

Option Strict On
Imports Common.Misc

Public Class frmSaveSystemSnapshot

    Private _optionsDisplayed As Boolean = False
    Private _option As Native.Api.Enums.SnapshotObject = Native.Api.Enums.SnapshotObject.All

    Private Sub frmWindowsList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        SetToolTip(Me.cmdBrowse, "Select target file")
        SetToolTip(Me.txtSSFile, "Target file")
        SetToolTip(Me.cmdCreate, "Save the snapshot now")
        SetToolTip(Me.cmdExit, "Close this window")
        SetToolTip(Me.lstOptions, "Objects to include to Snapshot file")

        Native.Functions.Misc.SetTheme(Me.lstOptions.Handle)

        Me.txtWarning.Text = "Be careful !" & vbNewLine & vbNewLine & "If you plan to post the file on a forum asking someone to analyze your system, keep in mind that important information can be transmitted (eg list of processes/services, list of files opened by the system...etc.)."

        ' Check all items in lstOptions
        For i As Integer = 0 To Me.lstOptions.Items.Count - 1
            Me.lstOptions.SetItemChecked(i, True)
            ' Do not check "mem region" by default cause it requires a lot a
            ' memory to save into a file (~1MB)
            Dim s As String = CStr(Me.lstOptions.Items(i))
            If s = "MemoryRegions" OrElse s = "Heaps" Then
                Me.lstOptions.SetItemChecked(i, False)
            End If
        Next
    End Sub

    Private Sub cmdCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCreate.Click

        Dim res As String = Nothing

        Me.lblState.Text = "Creating snapshot..."
        Me.Enabled = False

        ' Create empty snapshot file
        Dim snap As New cSnapshot250

        ' Get options
        Dim options As Native.Api.Enums.SnapshotObject
        For Each i As Integer In Me.lstOptions.CheckedIndices
            options = options Or _
                DirectCast([Enum].Parse(GetType(Native.Api.Enums.SnapshotObject), _
                                        CStr(Me.lstOptions.Items(i))), _
                                        Native.Api.Enums.SnapshotObject)
        Next

        ' Build it
        If snap.CreateSnapshot(Program.Connection, options, res) = False Then
            ' Failed
            Misc.ShowMsg("Snapshot creation", "Could not build snapshot.", MessageBoxButtons.OK)
            Me.Enabled = True
            Exit Sub
        End If

        ' Save it
        Me.lblState.Text = "Saving snapshot..."
        If snap.SaveSnapshot(Me.txtSSFile.Text, res) = False Then
            ' Failed
            Misc.ShowMsg("Snapshot creation", "Could not save snapshot.", MessageBoxButtons.OK)
            Me.Enabled = True
            Exit Sub
        End If

        Dim sSize As String = ""
        Try
            Dim size As Long = My.Computer.FileSystem.GetFileInfo(Me.txtSSFile.Text).Length
            sSize = GetFormatedSize(size)
        Catch ex As Exception
            sSize = "unknown"
            Misc.ShowDebugError(ex)
        End Try
        Me.lblState.Text = "Created a " & sSize & " size file."
        Me.Enabled = True

    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        With Me.SaveFileDialog
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.txtSSFile.Text = .FileName
            End If
        End With
    End Sub

    Private Sub txtSSFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSSFile.TextChanged
        Me.cmdCreate.Enabled = (Me.txtSSFile.Text IsNot Nothing) AndAlso (Me.txtSSFile.Text.Length > 0)
    End Sub

    Private Sub cmdOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOptions.Click
        _optionsDisplayed = Not (_optionsDisplayed)
        If _optionsDisplayed Then
            Me.Width = 484
            Me.cmdOptions.Text = "< Options"
        Else
            Me.Width = 310
            Me.cmdOptions.Text = "Options >"
        End If
    End Sub

End Class