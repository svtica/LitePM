
' Lite Process Monitor









'




'




Option Strict On

Public Class Pref



    ' Private constants




    ' Private attributes




    ' Public properties




    ' Other public


    ' Value of 'left property' to hide a form
    Public Const LEFT_POSITION_HIDDEN As Integer = -200

    ' Message displayed when LitePM starts for the first time
    Public Const MessageFirstStartOfLitePM As String = "This is the first time you run LitePM. Please remember that it is still a beta version so there are some bugs and some missing functionnalities :-)" & vbNewLine & vbNewLine & "You should run LitePM as an administrator in order to fully control your processes. Please take care using this LitePM because you will be able to do some irreversible things if you kill or modify some system processes... Use it at your own risks !" & vbNewLine & vbNewLine & "Please let me know any of your ideas of improvement or new functionnalities in LitePM's sourceforge.net project page ('Help' pannel) :-)" & vbNewLine & vbNewLine & "This message won't be shown anymore :-)"




    ' Public functions


    ' Save
    Public Sub Save()
        My.Settings.Save()
    End Sub

    ' Set to default
    Public Sub SetDefault()
        My.Settings.Reset()
        My.Settings.Save()
        Me.Apply()
    End Sub

    ' Apply pref
    Public Sub Apply()
        Static first As Boolean = True
        _frmMain.timerProcess.Interval = CInt(My.Settings.ProcessInterval * Program.Connection.RefreshmentCoefficient)
        _frmMain.timerServices.Interval = CInt(My.Settings.ServiceInterval * Program.Connection.RefreshmentCoefficient)
        _frmMain.timerNetwork.Interval = CInt(My.Settings.NetworkInterval * Program.Connection.RefreshmentCoefficient)
        _frmMain.timerTask.Interval = CInt(My.Settings.TaskInterval * Program.Connection.RefreshmentCoefficient)
        _frmMain.timerTrayIcon.Interval = CInt(My.Settings.TrayInterval * Program.Connection.RefreshmentCoefficient)
        _frmMain.timerJobs.Interval = CInt(My.Settings.JobInterval * Program.Connection.RefreshmentCoefficient)
        Select Case My.Settings.Priority
            Case 0
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.Idle
            Case 1
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.BelowNormal
            Case 2
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.Normal
            Case 3
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.AboveNormal
            Case 4
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.High
            Case 5
                Process.GetCurrentProcess.PriorityClass = ProcessPriorityClass.RealTime
        End Select
        handleList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        handleList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        memRegionList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        memRegionList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        windowList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        windowList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        moduleList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        moduleList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        mainNetworkConnectionsList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        mainNetworkConnectionsList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        mainProcessList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        mainProcessList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        mainServiceList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        mainServiceList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        mainTaskList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        mainTaskList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        threadList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        threadList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        mainJobList.DELETED_ITEM_COLOR = Color.FromArgb(My.Settings.DeletedItemColor)
        mainJobList.NEW_ITEM_COLOR = Color.FromArgb(My.Settings.NewItemColor)
        _frmMain.Tray.Visible = My.Settings.ShowTrayIcon
        _frmMain.StatusBar.Visible = My.Settings.ShowStatusBar

        ' Common tasks on first time
        If first Then
            first = False

            ' Ribbon style ?
            Call _frmMain.permuteMenuStyle(My.Settings.UseRibbonStyle)

            ' Update ?
            If My.Settings.UpdateAuto Then
                Program.Updater.CheckUpdates(True)
            End If

            _frmMain.TopMost = My.Settings.TopMost
            _frmMain.butAlwaysDisplay.Checked = My.Settings.TopMost
            _frmMain.Visible = Not (My.Settings.StartHidden)
            _frmMain.MenuItemMainAlwaysVisible.Checked = My.Settings.TopMost
            _frmMain.MenuItemNotifNP.Checked = My.Settings.NotifyNewProcesses
            _frmMain.MenuItemNotifDS.Checked = My.Settings.NotifyDeletedServices
            _frmMain.MenuItemNotifNS.Checked = My.Settings.NotifyNewServices
            _frmMain.MenuItemNotifTP.Checked = My.Settings.NotifyTerminatedProcesses
            'If My.Settings.StartHidden Then
            '    _frmMain.Hide()
            '    _frmMain.Left = LEFT_POSITION_HIDDEN
            'End If 'HIDDEN
            '_frmMain.ShowInTaskbar = Not (My.Settings.StartHidden)
        End If

        ' Highlightings
        With My.Settings
            cThread.SetHighlightings(.EnableHighlightingSuspendedThread)
            cThread.SetHighlightingsColor(.HighlightingColorSuspendedThread)
            cModule.SetHighlightings(.EnableHighlightingRelocatedModule)
            cModule.SetHighlightingsColor(.HighlightingColorRelocatedModule)
            cProcess.SetHighlightings(.EnableHighlightingBeingDebugged, _
                                      .EnableHighlightingJobProcess, _
                                      .EnableHighlightingElevated, _
                                      .EnableHighlightingCriticalProcess, _
                                      .EnableHighlightingOwnedProcess, _
                                      .EnableHighlightingSystemProcess, _
                                      .EnableHighlightingServiceProcess)
            cProcess.SetHighlightingsColor(.HighlightingColorBeingDebugged, _
                                           .HighlightingColorJobProcess, _
                                           .HighlightingColorElevatedProcess, _
                                           .HighlightingColorCriticalProcess, _
                                           .HighlightingColorOwnedProcess, _
                                           .HighlightingColorSystemProcess, _
                                           .HighlightingColorServiceProcess)
        End With
    End Sub

    ' Display columns of a listview (previously saved)
    Public Shared Sub LoadListViewColumns(ByVal lv As customLV, ByVal name As String)


        ' Here is an example of column description :
        ' col1?width1?index1?alignment1$col2?width2?index2?alignment2$...
        Dim s As String = ""
        Try
            s = CStr(My.Settings(name))
        Catch ex As Exception
            Trace.WriteLine(ex.Message)
        End Try

        If s Is Nothing OrElse s.Length < 3 Then
            Trace.WriteLine("could not read column configuration for a listview" & vbNewLine & name & "  " & getColumnDesc(lv))
            Exit Sub
        End If

        lv.BeginUpdate()
        lv.ReorganizeColumns = True
        lv.Columns.Clear()

        Dim res() As String = Split(s, "$")
        For Each column As String In res
            If Len(column) > 0 Then
                Dim obj() As String = Split(column, "?")
                Dim col As ColumnHeader = lv.Columns.Add(obj(0), CInt(Val((obj(1)))))
                If obj.Length < 4 Then
                    col.TextAlign = HorizontalAlignment.Left
                Else
                    Try
                        col.TextAlign = CType([Enum].Parse(GetType(HorizontalAlignment), obj(3)), HorizontalAlignment)
                    Catch ex As Exception
                        col.TextAlign = HorizontalAlignment.Left
                    End Try
                End If
            End If
        Next

        For u As Integer = 0 To lv.Columns.Count - 1
            For Each column As String In res
                Dim obj() As String = Split(column, "?")
                If lv.Columns(u).Text = obj(0) Then
                    lv.Columns(u).DisplayIndex = CInt(Val(obj(2)))
                    Exit For
                End If
            Next
        Next

        lv.ReorganizeColumns = False
        lv.EndUpdate()
    End Sub

    ' Save columns list of a listview
    Public Shared Sub SaveListViewColumns(ByVal lv As ListView, ByVal name As String)

        Dim s As String = ""

        For Each it As ColumnHeader In lv.Columns
            s &= it.Text.Replace("< ", "").Replace("> ", "") & "?" & it.Width.ToString & "?" & it.DisplayIndex.ToString & "?" & it.TextAlign.ToString & "$"
        Next

        My.Settings(name) = s

    End Sub

    ' Load position & size of a form
    Public Shared Sub LoadFormPositionAndSize(ByVal form As Form, ByVal name As String)
        ' Example : X|Y|W|H
        If My.Settings.RememberPosAndSize Then
            Try
                Dim value As String = CStr(My.Settings(name))
                Dim b() As String = value.Split(CChar("|"))
                Dim bb() As Integer = {Integer.Parse(b(0)), _
                                       Integer.Parse(b(1)), _
                                       Integer.Parse(b(2)), _
                                       Integer.Parse(b(3))}
                If bb(0) + bb(1) + bb(2) + bb(3) = 0 Then
                    ' Then we center the form !
                    With form
                        .Left = (Screen.PrimaryScreen.Bounds.Width - .Width) \ 2
                        .Top = (Screen.PrimaryScreen.Bounds.Height - .Height) \ 2
                    End With
                Else
                    With form
                        .Left = Integer.Parse(b(0))
                        .Top = Integer.Parse(b(1))
                        .Width = Integer.Parse(b(2))
                        .Height = Integer.Parse(b(3))
                    End With
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End If
    End Sub

    ' Save position & size of a form
    Public Shared Sub SaveFormPositionAndSize(ByVal form As Form, ByVal name As String)
        ' Example : X|Y|W|H
        If form IsNot Nothing Then
            If form.WindowState <> FormWindowState.Minimized Then
                If My.Settings.RememberPosAndSize Then
                    Try
                        Dim res As String = String.Format("{0}|{1}|{2}|{3}", _
                                                          form.Left.ToString, _
                                                          form.Top.ToString, _
                                                          form.Width.ToString, _
                                                          form.Height.ToString)
                        My.Settings(name) = res
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try
                End If
            End If
        End If
    End Sub




    ' Private functions


    ' Get current configuration of columns of a listview
    ' (only used for debug)
    Private Shared Function getColumnDesc(ByVal lv As ListView) As String
        Dim s As String = ""

        For Each it As ColumnHeader In lv.Columns
            s &= it.Text.Replace("< ", "").Replace("> ", "") & "?" & it.Width.ToString & "$"
        Next

        Return s
    End Function

End Class
