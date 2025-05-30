
Option Strict On


Public Class frmScripting

    Private _saved As Boolean = False
    Private _scriptFilePath As String

    Private Sub frmScripting_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _saved = False AndAlso Me.rtb.TextLength > 0 Then
            ' Wanna save before exiting ?
            If Misc.ShowMsg("Current script is unsaved. Are you sure you want to exit without saving the file?", "Exit", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
                Exit Sub
            End If
        End If
        ' Save position & size
        Pref.SaveFormPositionAndSize(Me, "PSfrmScripting")
    End Sub

    Private Sub frmScripting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Init position & size
        Pref.LoadFormPositionAndSize(Me, "PSfrmScripting")
    End Sub

    Private Sub MenuItemOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemOpen.Click
        Call Me.cmdOpen_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSave.Click
        Call Me.cmdSave_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExit.Click
        Me.Close()
    End Sub

    Private Sub MenuItemVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemVerify.Click
        Call Me.cmdCheckScript_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemExecute.Click
        Call Me.cmdExecute_Click(Nothing, Nothing)
    End Sub

#Region "Save/Open"

    Private Sub MenuItemSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSaveAs.Click
        ' Here we "save as" the file
        Dim sFile As String = Nothing
        With Me.SaveFileDialog
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                sFile = .FileName
                Try
                    ' Save file
                    System.IO.File.WriteAllText(sFile, Me.rtb.Text, System.Text.Encoding.Default)
                    _scriptFilePath = sFile
                    _saved = True
                Catch ex As Exception
                    Misc.ShowError(ex, "Could not save the script file")
                End Try
            End If
        End With
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        ' Open a new script
        If _saved OrElse Me.rtb.TextLength = 0 Then
            ' Then we open a new file
            Dim sFile As String = Nothing
            With Me.OpenFileDialog
                Dim rep As DialogResult = .ShowDialog
                If rep = Windows.Forms.DialogResult.OK Then
                    sFile = .FileName
                    Try
                        rtb.Text = System.IO.File.ReadAllText(sFile, System.Text.Encoding.Default)
                        _saved = True
                        _scriptFilePath = sFile
                    Catch ex As Exception
                        Misc.ShowError(ex, "Could not open the script file")
                    End Try
                End If
            End With
        Else
            ' Have to confirm before opening a new script
            If Misc.ShowMsg("Do you want to save the current script?", "Open a new script", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call Me.cmdSave_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        ' Save current script
        If _saved = False AndAlso _scriptFilePath = Nothing Then
            ' The save as
            Call Me.MenuItemSaveAs_Click(Nothing, Nothing)
        Else
            ' Then simply save
            Try
                System.IO.File.WriteAllText(_scriptFilePath, Me.rtb.Text, System.Text.Encoding.Default)
                _saved = True
            Catch ex As Exception
                Misc.ShowError(ex, "Could not save the script file")
            End Try
        End If
    End Sub

#End Region


    Private Sub cmdCheckScript_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCheckScript.Click
        ' Check script
        If _saved = False OrElse Me.rtb.TextLength = 0 Then
            Misc.ShowMsg("Check if script is valid" & " Cannot check script.", "Please save the script first.", MessageBoxButtons.OK)
            Exit Sub
        End If
        Dim _engine As New Scripting.Engine(_scriptFilePath)
        Dim _res As String = ""
        If _engine.Verify(_res) Then
            Misc.ShowMsg("Check script", "Script is OK.", MessageBoxButtons.OK)
            Exit Sub
        Else
            Misc.ShowMsg("Check script" & " Script is not valid", "Message : " & _res, MessageBoxButtons.OK)
            Exit Sub
        End If
    End Sub

    Private Sub cmdExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExecute.Click
        ' Execute script

        ' Check it before executing it
        If _saved = False OrElse Me.rtb.TextLength = 0 Then
            Misc.ShowMsg("Check if script is valid" & " Cannot check script.", "Please save the script first.", MessageBoxButtons.OK)
            Exit Sub
        End If
        Dim _engine As New Scripting.Engine(_scriptFilePath)
        Dim _res As String = ""
        If _engine.Verify(_res) = False Then
            Misc.ShowMsg("Check script" & " Script is not valid", "Message : " & _res, MessageBoxButtons.OK)
            Exit Sub
        End If

        ' Now we execute it
        _engine.Execute()

    End Sub

    Private Sub rtb_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb.TextChanged
        _saved = False
    End Sub
End Class