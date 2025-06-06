
' Lite Process Monitor


Option Strict On
Imports Common.Misc
Imports System.Windows.Forms.Ribbon

Public Class frmMain

    ' Private attributes

    Private _ribbonStyle As Boolean = True
    Private _local As Boolean = True
    Private _notWMI As Boolean = True
    Private _notSnapshotMode As Boolean = True
    Private _callExitWhenExitLitePM As Boolean = True
    Private handlesToRefresh() As Integer
    Private threadsToRefresh() As Integer
    Private modulesToRefresh() As Integer
    Private windowsToRefresh() As Integer
    Private cSelFile As cFile


    ' Public & properties

    Public Property CallExitWhenExitLitePM() As Boolean
        Get
            Return _callExitWhenExitLitePM
        End Get
        Set(ByVal value As Boolean)
            _callExitWhenExitLitePM = value
        End Set
    End Property

    Public procfilter As String
    Public _shutdownConnection As New cShutdownConnection(Me, Program.Connection)

    ' Constants

    Private Const SIZE_FOR_STRING As Integer = 4


    ' Form functions

    ' Refresh File informations
    Public Sub refreshFileInfos(ByVal file As String)

        Dim s As String = ""

        cSelFile = New cFile(file, True)

        If IO.File.Exists(file) Then

            ' Set dates to datepickers
            Me.DTcreation.Value = cSelFile.CreationTime
            Me.DTlastAccess.Value = cSelFile.LastAccessTime
            Me.DTlastModification.Value = cSelFile.LastWriteTime

            ' Set attributes
            Dim att As System.IO.FileAttributes = cSelFile.Attributes()
            Me.chkFileArchive.Checked = ((att And IO.FileAttributes.Archive) = IO.FileAttributes.Archive)
            Me.chkFileCompressed.Checked = ((att And IO.FileAttributes.Compressed) = IO.FileAttributes.Compressed)
            Me.chkFileHidden.Checked = ((att And IO.FileAttributes.Hidden) = IO.FileAttributes.Hidden)
            Me.chkFileReadOnly.Checked = ((att And IO.FileAttributes.ReadOnly) = IO.FileAttributes.ReadOnly)
            Me.chkFileSystem.Checked = ((att And IO.FileAttributes.System) = IO.FileAttributes.System)
            Me.chkFileNormal.Checked = ((att And IO.FileAttributes.Normal) = IO.FileAttributes.Normal)
            Me.chkFileContentNotIndexed.Checked = ((att And IO.FileAttributes.NotContentIndexed) = IO.FileAttributes.NotContentIndexed)
            Me.chkFileEncrypted.Checked = ((att And IO.FileAttributes.Encrypted) = IO.FileAttributes.Encrypted)

            ' Clean string list
            Me.lvFileString.Items.Clear()
            Me.lvFileString.Items.Add("Click on 'Others->Show file strings' to retrieve file strings")

            s &= "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}{\f1\fswiss\fcharset0 Arial;}}"
            s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   "
            s &= "\b File basic properties\b0\par"
            s &= "\tab File name :\tab\tab " & cSelFile.Name & "\par"
            s &= "\tab Parent directory :\tab " & cSelFile.ParentDirectory & "\par"
            s &= "\tab Extension :\tab\tab " & cSelFile.FileExtension & "\par"
            s &= "\tab Creation date :\tab\tab " & cSelFile.DateCreated & "\par"
            s &= "\tab Last access date :\tab " & cSelFile.DateLastAccessed & "\par"
            s &= "\tab Last modification date :\tab " & cSelFile.DateLastModified & "\par"
            s &= "\tab Size :\tab\tab\tab " & cSelFile.FileSize & " Bytes -- " & Math.Round(cSelFile.FileSize / 1024, 3) & " KB" & " -- " & Math.Round(cSelFile.FileSize / 1024 / 1024, 3) & "MB\par"
            s &= "\tab Compressed size :\tab " & cSelFile.CompressedFileSize & " Bytes -- " & Math.Round(cSelFile.CompressedFileSize / 1024, 3) & " KB" & " -- " & Math.Round(cSelFile.CompressedFileSize / 1024 / 1024, 3) & "MB\par\par"
            s &= "\b File advances properties\b0\par"
            s &= "\tab File type :\tab\tab " & cSelFile.FileType & "\par"
            s &= "\tab Associated program :\tab " & cSelFile.FileAssociatedProgram & "\par"
            s &= "\tab Short name :\tab\tab " & cSelFile.ShortName & "\par"
            s &= "\tab Short path :\tab\tab " & cSelFile.ShortPath & "\par"
            s &= "\tab Directory depth :\tab\tab " & cSelFile.DirectoryDepth & "\par"
            s &= "\tab File available for read :\tab " & cSelFile.FileAvailableForWrite & "\par"
            s &= "\tab File available for write :\tab " & cSelFile.FileAvailableForWrite & "\par\par"
            s &= "\b Attributes\b0\par"
            s &= "\tab Archive :\tab\tab " & cSelFile.IsArchive & "\par"
            s &= "\tab Compressed :\tab\tab " & cSelFile.IsCompressed & "\par"
            s &= "\tab Device :\tab\tab\tab " & cSelFile.IsDevice & "\par"
            s &= "\tab Directory :\tab\tab " & cSelFile.IsDirectory & "\par"
            s &= "\tab Encrypted :\tab\tab " & cSelFile.IsEncrypted & "\par"
            s &= "\tab Hidden :\tab\tab\tab " & cSelFile.IsHidden & "\par"
            s &= "\tab Normal :\tab\tab\tab " & cSelFile.IsNormal & "\par"
            s &= "\tab Not content indexed :\tab " & cSelFile.IsNotContentIndexed & "\par"
            s &= "\tab Offline :\tab\tab\tab " & cSelFile.IsOffline & "\par"
            s &= "\tab Read only :\tab\tab " & cSelFile.IsReadOnly & "\par"
            s &= "\tab Reparse file :\tab\tab " & cSelFile.IsReparsePoint & "\par"
            s &= "\tab Fragmented :\tab\tab " & cSelFile.IsSparseFile & "\par"
            s &= "\tab System :\tab\tab " & cSelFile.IsSystem & "\par"
            s &= "\tab Temporary :\tab\tab " & cSelFile.IsTemporary & "\par\par"
            s &= "\b File version infos\b0\par"

            If cSelFile.FileVersion IsNot Nothing Then
                If cSelFile.FileVersion.Comments IsNot Nothing AndAlso cSelFile.FileVersion.Comments.Length > 0 Then _
                    s &= "\tab Comments :\tab\tab " & cSelFile.FileVersion.Comments & "\par"
                If cSelFile.FileVersion.CompanyName IsNot Nothing AndAlso cSelFile.FileVersion.CompanyName.Length > 0 Then _
                    s &= "\tab CompanyName :\tab\tab " & cSelFile.FileVersion.CompanyName & "\par"
                If CStr(cSelFile.FileVersion.FileBuildPart).Length > 0 Then _
                    s &= "\tab FileBuildPart :\tab\tab " & CStr(cSelFile.FileVersion.FileBuildPart) & "\par"
                If cSelFile.FileVersion.FileDescription IsNot Nothing AndAlso cSelFile.FileVersion.FileDescription.Length > 0 Then _
                    s &= "\tab FileDescription :\tab\tab " & cSelFile.FileVersion.FileDescription & "\par"
                If CStr(cSelFile.FileVersion.FileMajorPart).Length > 0 Then _
                    s &= "\tab FileMajorPart :\tab\tab " & CStr(cSelFile.FileVersion.FileMajorPart) & "\par"
                If CStr(cSelFile.FileVersion.FileMinorPart).Length > 0 Then _
                    s &= "\tab FileMinorPart :\tab\tab " & cSelFile.FileVersion.FileMinorPart & "\par"
                If CStr(cSelFile.FileVersion.FilePrivatePart).Length > 0 Then _
                    s &= "\tab FilePrivatePart :\tab\tab " & cSelFile.FileVersion.FilePrivatePart & "\par"
                If cSelFile.FileVersion.FileVersion IsNot Nothing AndAlso cSelFile.FileVersion.FileVersion.Length > 0 Then _
                    s &= "\tab FileVersion :\tab\tab " & cSelFile.FileVersion.FileVersion & "\par"
                If cSelFile.FileVersion.InternalName IsNot Nothing AndAlso cSelFile.FileVersion.InternalName.Length > 0 Then _
                    s &= "\tab InternalName :\tab\tab " & cSelFile.FileVersion.InternalName & "\par"
                If CStr(cSelFile.FileVersion.IsDebug).Length > 0 Then _
                    s &= "\tab IsDebug :\tab\tab " & cSelFile.FileVersion.IsDebug & "\par"
                If CStr(cSelFile.FileVersion.IsPatched).Length > 0 Then _
                    s &= "\tab IsPatched :\tab\tab " & cSelFile.FileVersion.IsPatched & "\par"
                If CStr(cSelFile.FileVersion.IsPreRelease).Length > 0 Then _
                    s &= "\tab IsPreRelease :\tab\tab " & cSelFile.FileVersion.IsPreRelease & "\par"
                If CStr(cSelFile.FileVersion.IsPrivateBuild).Length > 0 Then _
                    s &= "\tab IsPrivateBuild :\tab\tab " & cSelFile.FileVersion.IsPrivateBuild & "\par"
                If CStr(cSelFile.FileVersion.IsSpecialBuild).Length > 0 Then _
                    s &= "\tab IsSpecialBuild :\tab\tab " & cSelFile.FileVersion.IsSpecialBuild & "\par"
                If cSelFile.FileVersion.Language IsNot Nothing AndAlso cSelFile.FileVersion.Language.Length > 0 Then _
                    s &= "\tab Language :\tab\tab " & cSelFile.FileVersion.Language & "\par"
                If cSelFile.FileVersion.LegalCopyright IsNot Nothing AndAlso cSelFile.FileVersion.LegalCopyright.Length > 0 Then _
                    s &= "\tab LegalCopyright :\tab\tab " & cSelFile.FileVersion.LegalCopyright & "\par"
                If cSelFile.FileVersion.LegalTrademarks IsNot Nothing AndAlso cSelFile.FileVersion.LegalTrademarks.Length > 0 Then _
                    s &= "\tab LegalTrademarks :\tab " & cSelFile.FileVersion.LegalTrademarks & "\par"
                If cSelFile.FileVersion.OriginalFilename IsNot Nothing AndAlso cSelFile.FileVersion.OriginalFilename.Length > 0 Then _
                    s &= "\tab OriginalFilename :\tab\tab " & cSelFile.FileVersion.OriginalFilename & "\par"
                If cSelFile.FileVersion.PrivateBuild IsNot Nothing AndAlso cSelFile.FileVersion.PrivateBuild.Length > 0 Then _
                    s &= "\tab PrivateBuild :\tab\tab " & cSelFile.FileVersion.PrivateBuild & "\par"
                If CStr(cSelFile.FileVersion.ProductBuildPart).Length > 0 Then _
                    s &= "\tab ProductBuildPart :\tab " & cSelFile.FileVersion.ProductBuildPart & "\par"
                If CStr(cSelFile.FileVersion.ProductMajorPart).Length > 0 Then _
                    s &= "\tab ProductMajorPart :\tab " & cSelFile.FileVersion.ProductMajorPart & "\par"
                If CStr(cSelFile.FileVersion.ProductMinorPart).Length > 0 Then _
                    s &= "\tab Comments :\tab\tab " & cSelFile.FileVersion.ProductMinorPart & "\par"
                If cSelFile.FileVersion.ProductName IsNot Nothing AndAlso cSelFile.FileVersion.ProductName.Length > 0 Then _
                    s &= "\tab ProductName :\tab\tab " & cSelFile.FileVersion.ProductName & "\par"
                If CStr(cSelFile.FileVersion.ProductPrivatePart).Length > 0 Then _
                    s &= "\tab ProductPrivatePart :\tab " & cSelFile.FileVersion.ProductPrivatePart & "\par"
                If cSelFile.FileVersion.ProductVersion IsNot Nothing AndAlso cSelFile.FileVersion.ProductVersion.Length > 0 Then _
                    s &= "\tab ProductVersion :\tab\tab " & cSelFile.FileVersion.ProductVersion & "\par"
                If cSelFile.FileVersion.SpecialBuild IsNot Nothing AndAlso cSelFile.FileVersion.SpecialBuild.Length > 0 Then _
                    s &= "\tab SpecialBuild :\tab\tab " & cSelFile.FileVersion.SpecialBuild & "\par"
            End If

            ' Icons
            Try
                pctFileBig.Image = GetIcon(file, False).ToBitmap
                pctFileSmall.Image = GetIcon(file, True).ToBitmap
            Catch ex As Exception
                pctFileSmall.Image = My.Resources.exe16
                pctFileBig.Image = My.Resources.exe32
            End Try


            s &= "\f1\fs20\par"
            s &= "}"
        Else
            s &= "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}{\f1\fswiss\fcharset0 Arial;}}"
            s &= "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b " & "File does not exist !\b0\par"
            s &= "\f1\fs20\par"
            s &= "}"
        End If


        rtb3.Rtf = s

    End Sub

    ' Refresh service list
    Public Sub refreshServiceList()

        ' Update list
        ServiceProvider.Update(False, Me.lvServices.InstanceId)

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            If Me.Ribbon.ActiveTab.Text = "Services" Then
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvServices.Items.Count) & " services running"
            End If
        End If

    End Sub

    ' Refresh job list
    Public Sub refreshJobList()

        ' Update list
        JobProvider.Update(False, Me.lvJob.InstanceId)

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            If Me.Ribbon.ActiveTab.Text = "Jobs" Then
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvJob.Items.Count) & " jobs running"
            End If
        End If

    End Sub

    ' Refresh process list in listview
    Public Sub refreshProcessList()

        ' Update list of processes
        ProcessProvider.Update(False, Me.lvProcess.InstanceId)

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Processes" Or ss = "Monitor" Or ss = "Misc" Or ss = "Help" Or ss = "File" Then
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
            End If
        End If

    End Sub

    'Fiter process list
    Public Sub filterProcessList(procfilter As String)
        Try
            Call refreshProcessList()
            _tab.SelectedTab = pageProcesses
            txtSearch.Focus()
            txtSearch.Text = procfilter
            txtSearch_TextChanged(Nothing, Nothing)
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    Private Sub timerProcess_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerProcess.Tick
        Call refreshProcessList()
    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Static bFirst As Boolean = True
        If bFirst Then
            bFirst = False
            Native.Functions.Misc.SetTheme(Me.lvProcess.Handle)
            Native.Functions.Misc.SetTheme(Me.lvMonitorReport.Handle)
            Native.Functions.Misc.SetTheme(Me.lvNetwork.Handle)
            Native.Functions.Misc.SetTheme(Me.lvTask.Handle)
            Native.Functions.Misc.SetTheme(Me.lvSearchResults.Handle)
            Native.Functions.Misc.SetTheme(Me.lvServices.Handle)
            Native.Functions.Misc.SetTheme(Me.tv.Handle)
            Native.Functions.Misc.SetTheme(Me.tv2.Handle)
            Native.Functions.Misc.SetTheme(Me.tvMonitor.Handle)
            Native.Functions.Misc.SetTheme(Me.lvFileString.Handle)
            Native.Functions.Misc.SetTheme(Me.lvJob.Handle)
        End If
        Call frmMain_Resize(Nothing, Nothing)
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If My.Settings.HideWhenClosed AndAlso Program.MustCloseWithCloseButton = False Then
            Me.Hide()
            e.Cancel = True
            Exit Sub
        End If

        If Me.CallExitWhenExitLitePM Then

            ' This avoid to call ExitLitePM recursively when exiting
            Me.CallExitWhenExitLitePM = False

            ' Save position & size
            Pref.SaveFormPositionAndSize(Me, "PSfrmMain")

            ' Exit
            Call ExitLitePM()
        End If

    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Program.Parameters.ModeHidden Then
            Me.Left = Pref.LEFT_POSITION_HIDDEN
            Me.ShowInTaskbar = False
        End If
        If My.Settings.StartHidden Then
            ' If Ribbon is displayed, there is a problem : Form is automatically shown...
            ' So we have to remove it from taskbar and move it to the left to hide it
            ' If Ribbon is not displayed, we can't hide from taskbar else icons won't
            ' be displayed in MainMenu...
            If My.Settings.UseRibbonStyle Then
                Me.Left = Pref.LEFT_POSITION_HIDDEN
                Me.ShowInTaskbar = False
            End If
            Me.Hide()
        End If

        ' Set some handlers
        AddHandler ProcessProvider.GotNewItems, AddressOf Me.processCreated
        AddHandler ProcessProvider.GotDeletedItems, AddressOf Me.processDeleted
        AddHandler ServiceProvider.GotNewItems, AddressOf Me.serviceCreated
        AddHandler ServiceProvider.GotDeletedItems, AddressOf Me.serviceDeleted

        ' For now, SBA is removed from menu...
        Me.Ribbon.OrbDropDown.MenuItems.Remove(Me.orbMenuSBA)
        ' For now, scripting is removed from menu...
        Me.Ribbon.QuickAcessToolbar.Items.Remove(Me.butScripting)
        Me.MenuItemSYSTEMFILE.MenuItems.Remove(Me.MenuItemSystemScripting)
        ' For now, remove Tracert/WhoIs menus
        Me.MenuItemNetworkTools.MenuItems.Remove(Me.MenuItemNetworkRoute)
        Me.MenuItemNetworkTools.MenuItems.Remove(Me.MenuItemNetworkWhoIs)

        ' Disable 'start as admin' if we are not on Vista or above
        If cEnvironment.SupportsUac = False _
                OrElse cEnvironment.GetElevationType <> Native.Api.Enums.ElevationType.Limited Then
            Me.Ribbon.OrbDropDown.MenuItems.Remove(Me.orbStartElevated)
            Me.MenuItemSYSTEMFILE.MenuItems.Remove(Me.MenuItemRunAsAdmin)
        Else
            ' If we display standard menus, we add the icon
            If My.Settings.UseRibbonStyle = False Then
                'Me.VistaMenu.SetImage(Me.MenuItemRunAsAdmin, cEnvironment.GetUacShieldImage)
            End If
        End If

        Me.timerProcess.Enabled = False
        Dim t As Integer = Native.Api.Win32.GetElapsedTime

        Me.containerSystemMenu.Panel1Collapsed = True
        Me.Tray.Visible = True
        Me.Tray.ContextMenu = Me.mnuMain
        Me.rtb3.AllowDrop = True

        ' Set tray icon counters
        TrayIcon.SetCounter(1, Color.Red, Color.FromArgb(120, 0, 0))
        TrayIcon.SetCounter(2, Color.LightGreen, Color.FromArgb(0, 120, 0))

        PROCESSOR_COUNT = Program.SystemInfo.ProcessorCount

        With Me.graphMonitor
            .ColorMemory1 = Color.Yellow
            .ColorMemory2 = Color.Red
            .ColorMemory3 = Color.Orange
        End With

        ' Create tooltips
        SetToolTip(Me.lblResCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.lblResCount2, "Number of results. Click on the number to view results")
        SetToolTip(Me.lblTaskCountResult, "Number of results. Click on the number to view results")
        SetToolTip(Me.txtSearchTask, "Enter text here to search a task")
        SetToolTip(Me.txtSearch, "Enter text here to search a process")
        SetToolTip(Me.txtSearchResults, "Enter text here to search into the results")
        SetToolTip(Me.lblResultsCount, "Number of results. Click on the number to view results")
        SetToolTip(Me.tvMonitor, "Monitoring items")
        SetToolTip(Me.chkMonitorLeftAuto, "Setting to display graph. See help for details")
        SetToolTip(Me.chkMonitorRightAuto, "Setting to display graph. See help for details")
        SetToolTip(Me.dtMonitorL, "Setting to display graph. See help for details")
        SetToolTip(Me.dtMonitorR, "Setting to display graph. See help for details")
        SetToolTip(Me.txtMonitorNumber, "Setting to display graph. See help for details")
        SetToolTip(Me.cmdFileClipboard, "Copy file informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style)")
        SetToolTip(Me.DTcreation, "Date of creation")
        SetToolTip(Me.DTlastAccess, "Date of last access")
        SetToolTip(Me.DTlastModification, "Date of last modification")
        SetToolTip(Me.cmdSetFileDates, "Set these dates")
        SetToolTip(Me.chkFileArchive, "File is archive")
        SetToolTip(Me.chkFileCompressed, "File is compressed")
        SetToolTip(Me.chkFileContentNotIndexed, "File is indexed")
        SetToolTip(Me.chkFileEncrypted, "File is encrypted")
        SetToolTip(Me.chkFileHidden, "File is hidden")
        SetToolTip(Me.chkFileNormal, "File is normal")
        SetToolTip(Me.chkFileReadOnly, "File is read only")
        SetToolTip(Me.chkFileSystem, "File is system")
        SetToolTip(Me.txtServiceSearch, "Enter text here to search a service")
        SetToolTip(Me.cmdCopyServiceToCp, "Copy services informations to clipboard. Use left click to copy as text, right click to copy as rtf (preserve text style)")
        SetToolTip(Me.lblServicePath, "Path of the main executable of the service")
        SetToolTip(Me.tv, "Selected service depends on these services")
        SetToolTip(Me.tv2, "This services depend on selected service")
        SetToolTip(Me.chkSearchProcess, "Include processes in search")
        SetToolTip(Me.chkSearchServices, "Include services in search")
        SetToolTip(Me.chkSearchWindows, "Include windows in search")
        SetToolTip(Me.chkSearchCase, "Search is case sensitive or not")
        SetToolTip(Me.chkSearchEnvVar, "Include environement variables in search")
        SetToolTip(Me.chkSearchHandles, "Include handles in search")
        SetToolTip(Me.chkSearchModules, "Include modules in search")
        SetToolTip(Me.lvFileString, "List of strings in file. Middle click to copy to clipboard.")

        ' Init columns
        Pref.LoadListViewColumns(Me.lvProcess, "COLmain_process")
        Pref.LoadListViewColumns(Me.lvTask, "COLmain_task")
        Pref.LoadListViewColumns(Me.lvServices, "COLmain_service")
        Pref.LoadListViewColumns(Me.lvNetwork, "COLmain_network")

        ' Init position & size
        Pref.LoadFormPositionAndSize(Me, "PSfrmMain")

        ' Connect to the local machine
        Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection
        Call ConnectToMachine()

        Me.timerMonitoring.Enabled = True
        Me.timerProcess.Enabled = True
        Me.timerTask.Enabled = True
        Me.timerNetwork.Enabled = True
        Me.timerStateBasedActions.Enabled = True
        Me.timerTrayIcon.Enabled = True
        Me.timerServices.Enabled = True
        Me.timerStatus.Enabled = True
        Me.timerJobs.Enabled = True

        If Me.lvProcess.Items.Count > 1 Then
            Call Me.lvProcess.Focus()
            Me.lvProcess.Items(Me.lvProcess.Items.Count - 1).Selected = True
            Me.lvProcess.Items(Me.lvProcess.Items.Count - 1).EnsureVisible()
        End If

        t = Native.Api.Win32.GetElapsedTime - t

        Trace.WriteLine("Loaded in " & CStr(t) & " ms.")
        Call refreshTaskList()


        ' Add some submenus (Copy to clipboard)
        For Each ss As String In jobInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyJob.MenuItems.Add(ss, AddressOf MenuItemCopyJob_Click)
        Next
        For Each ss As String In networkInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyNetwork.MenuItems.Add(ss, AddressOf MenuItemCopyNetwork_Click)
        Next
        For Each ss As String In searchInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopySearch.MenuItems.Add(ss, AddressOf MenuItemCopySearch_Click)
        Next
        For Each ss As String In serviceInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyService.MenuItems.Add(ss, AddressOf MenuItemCopyService_Click)
        Next
        For Each ss As String In taskInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyTask.MenuItems.Add(ss, AddressOf MenuItemCopyTask_Click)
        Next
        For Each ss As String In processInfos.GetAvailableProperties(True, True)
            Me.MenuItemCopyProcess.MenuItems.Add(ss, AddressOf MenuItemCopyProcess_Click)
        Next

#If RELEASE_MODE = 0 Then
        Dim frm As New frmServer
        frm.TopMost = _frmMain.TopMost
        frm.Show()
#End If

    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        Try
            ' If we do not use Ribbon mode, we hide Ribbon. Else, we add tabs
            If My.Settings.UseRibbonStyle Then
                With _tab
                    _tab.Dock = DockStyle.None
                    _tab.Top = -24
                    _tab.Left = -2
                    _tab.Width = Me.Width - 12
                    _tab.Height = Me.Height - Me.Ribbon.Height - Me.StatusBar.Height - 15
                    _tab.Region = New Region(New RectangleF(_tab.Left, _tab.SelectedTab.Top, _tab.SelectedTab.Width + 5, _tab.SelectedTab.Height))
                    _tab.Refresh()
                End With
            Else
                Me._main.Panel1Collapsed = True
            End If

            If My.Settings.HideWhenMinimized AndAlso Me.WindowState = FormWindowState.Minimized Then
                Me.Hide()
            End If

            For Each t As TabPage In _tab.TabPages
                t.Hide()
            Next

            ' File resizement
            Me.txtFile.Width = Me.Width - 260

            _tab.SelectedTab.Show()

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try
    End Sub

    Private Sub timerServices_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerServices.Tick
        Me.refreshServiceList()
    End Sub

    Private Sub Tray_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Tray.MouseDoubleClick
        ' If ribbon is used, then the main form is to the left of the screen and
        ' not shown in taskbar
        If My.Settings.UseRibbonStyle Then
            If Me.Left = Pref.LEFT_POSITION_HIDDEN Then
                Me.CenterToScreen()
            End If
            Me.ShowInTaskbar = True
        End If
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.Show()
    End Sub

    Private Sub frmMain_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Shown

        ' Select tab to activate
        Dim tabToShow As String = My.Settings.SelectedTab
        If My.Settings.ShowFixedTab Then
            tabToShow = My.Settings.FixedTab
        End If
        For Each tab As RibbonTab In Me.Ribbon.Tabs
            If tab.Text = tabToShow Then
                Me.Ribbon.ActiveTab = tab
                Exit For
            End If
        Next

        Call Ribbon_MouseMove(Nothing, Nothing)
        Call Me.frmMain_Resize(Nothing, Nothing)
    End Sub

    Private Sub butKill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butKillProcess.Click
        If WarnDangerousAction("Are you sure you want to kill these processes ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.Kill()
        Next
    End Sub

    Private Sub butAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAbout.Click
        Dim frm As New frmAboutG
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub butProcessRerfresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessRerfresh.Click
        Me.refreshProcessList()
    End Sub

    Private Sub butServiceRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceRefresh.Click
        Me.refreshServiceList()
    End Sub

    Private Sub butServiceFileProp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceFileProp.Click
        Call Me.MenuItemServFileProp_Click(Nothing, Nothing)
    End Sub

    Private Sub butServiceOpenDir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceOpenDir.Click
        Call Me.MenuItemServOpenDir_Click(Nothing, Nothing)
    End Sub

    Private Sub butStopProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStopProcess.Click
        If WarnDangerousAction("Are you sure you want to suspend these processes ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SuspendProcess()
        Next
    End Sub

    Private Sub butProcessAffinity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessAffinity.Click
        Call Me.MenuItemProcAff_Click(Nothing, Nothing)
    End Sub

    Private Sub butResumeProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butResumeProcess.Click
        ' Resume selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.ResumeProcess()
        Next
    End Sub

    Private Sub butIdle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butIdle.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.Idle)
        Next
    End Sub

    Private Sub butHigh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHigh.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub butNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNormal.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub butRealTime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butRealTime.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.RealTime)
        Next
    End Sub

    Private Sub butBelowNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butBelowNormal.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub butAboveNormal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAboveNormal.Click
        ' Set priority to selected processes
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.SetPriority(ProcessPriorityClass.AboveNormal)
        Next
    End Sub

    Private Sub butStopService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStopService.Click
        If WarnDangerousAction("Are you sure you want to stop these services ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StopService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butStartService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butStartService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StartService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butPauseService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPauseService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.PauseService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butAutomaticStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAutomaticStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.AutoStart)
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butDisabledStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDisabledStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.StartDisabled)
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butOnDemandStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOnDemandStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.DemandStart)
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub butResumeService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butResumeService.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.ResumeService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub Ribbon_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Ribbon.MouseClick
        If Me.lvServices.Items.Count = 0 Then
            If Me.Ribbon.ActiveTab.Text = "Services" Then
                ' First display of service tab
                Call refreshServiceList()
            End If
        ElseIf Me.lvProcess.Items.Count = 0 Then
            If Me.Ribbon.ActiveTab.Text = "Processes" Then
                ' First display of process tab
                Call refreshProcessList()
            End If
        End If
    End Sub

    Public Sub Ribbon_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Ribbon.MouseMove
        ' Static currentText As String = vbNullString
        Static bHelpShown As Boolean = False


        Select Case Ribbon.ActiveTab.Text
            Case "Jobs"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvJob.Items.Count) & " jobs running"
                _tab.SelectedTab = Me.pageJobs
            Case "Services"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvServices.Items.Count) & " services running"
                _tab.SelectedTab = Me.pageServices
            Case "Processes"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageProcesses
            Case "Help"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageHelp
            Case "File"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageFile
            Case "Search"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvSearchResults.Items.Count) & " search results"
                _tab.SelectedTab = Me.pageSearch
            Case "Monitor"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                _tab.SelectedTab = Me.pageMonitor
            Case "Tasks"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvTask.Items.Count) & " tasks running"
                _tab.SelectedTab = Me.pageTasks
            Case "Network"
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvNetwork.Items.Count) & " connections opened"
                _tab.SelectedTab = Me.pageNetwork
        End Select
        'End If

    End Sub

    Private Sub butDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDownload.Click
        cFile.ShellOpenFile("https://", Me.Handle)
    End Sub

    Private Sub frmMain_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.VisibleChanged

    End Sub

    Private Sub butProcessGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessGoogle.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Application.DoEvents()
            Call SearchInternet(cp.Infos.Name, Me.Handle)
        Next
    End Sub

    Private Sub butServiceGoogle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceGoogle.Click
        Dim it As ListViewItem
        For Each it In Me.lvServices.SelectedItems
            Application.DoEvents()
            Call SearchInternet(it.Text, Me.Handle)
        Next
    End Sub

    Private Sub butServiceFileDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceFileDetails.Click
        If Me.lvServices.SelectedItems.Count > 0 Then
            Dim s As String = Me.lvServices.GetSelectedItem.GetInformation("ImagePath")
            If IO.File.Exists(s) = False Then
                s = cFile.IntelligentPathRetrieving2(s)
            End If
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub butUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butUpdate.Click
        ' Check for updates manually
        ' No silent mode, so it will cause a messagebox to be displayed
        Program.Updater.CheckUpdates(False)
    End Sub

    Private Sub butSearchGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSearchGo.Click
        Call goSearch(Me.txtSearchString.TextBoxText)
    End Sub

    Private Sub txtSearchString_TextBoxTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchString.TextBoxTextChanged
        Dim b As Boolean = (txtSearchString.TextBoxText IsNot Nothing)
        If b Then
            b = b And txtSearchString.TextBoxText.Length > 0
        End If
        Me.butSearchGo.Enabled = b
    End Sub

    Private Sub butSearchSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSearchSaveReport.Click
        Dim frm As New frmSaveReport
        With frm
            .TopMost = _frmMain.TopMost
            .ListviewToSave = Me.lvSearchResults
            .ShowDialog()
        End With
    End Sub

    Private Sub butFileProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileProperties.Click
        Call cFile.ShowFileProperty(Me.txtFile.Text, Me.Handle)
    End Sub

    Private Sub butFileShowFolderProperties_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileShowFolderProperties.Click
        Call cFile.ShowFileProperty(IO.Directory.GetParent(Me.txtFile.Text).FullName, Me.Handle)
    End Sub

    Private Sub butFileOpenDir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileOpenDir.Click
        Call cFile.OpenDirectory(Me.txtFile.Text)
    End Sub

    Private Sub butOpenFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOpenFile.Click
        ' Open a file
        openDial.Filter = "All files (*.*)|*.*"
        openDial.Title = "Open a file to retrieve details"
        If openDial.ShowDialog = Windows.Forms.DialogResult.OK Then
            If IO.File.Exists(openDial.FileName) Then
                DisplayDetailsFile(openDial.FileName)
            End If
        End If
    End Sub

    Private Sub butFileRelease_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRelease.Click
        Dim frm As New frmFileRelease
        With frm
            .file = Me.txtFile.Text
            .TopMost = _frmMain.TopMost
            Call .ShowDialog()
        End With
    End Sub

    Private Sub butFileGoogleSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileGoogleSearch.Click
        Application.DoEvents()
        Call SearchInternet(cFile.GetFileName(Me.txtFile.Text), Me.Handle)
    End Sub

    Private Sub butFileEncrypt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileEncrypt.Click
        Try
            cSelFile.Encrypt()
            Misc.ShowMsg("File encryption", "Encryption done.", MessageBoxButtons.OK)
        Catch ex As Exception
            Misc.ShowError(ex, "Encryption failed")
        End Try
    End Sub

    Private Sub butFileDecrypt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileDecrypt.Click
        Try
            cSelFile.Decrypt()
            Misc.ShowMsg("File decryption", "Decryption done.", MessageBoxButtons.OK)
        Catch ex As Exception
            Misc.ShowError(ex, "Decryption failed")
        End Try
    End Sub

    Private Sub butFileRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRefresh.Click
        Call DisplayDetailsFile(Me.txtFile.Text)
    End Sub

    Private Sub butMoveFileToTrash_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMoveFileToTrash.Click
        cSelFile.MoveToTrash()
    End Sub

    Private Sub butFileSeeStrings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileSeeStrings.Click
        Call DisplayFileStrings(Me.lvFileString, Me.txtFile.Text)
    End Sub

    Private Function RemoveAttribute(ByVal attributesToRemove As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.Attributes()
        Return attributes And Not (attributesToRemove)
    End Function
    Private Function AddAttribute(ByVal attributesToAdd As IO.FileAttributes) As IO.FileAttributes
        Dim attributes As IO.FileAttributes = cSelFile.Attributes
        Return attributes Or attributesToAdd
    End Function

    Private Sub butFileOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileOpen.Click
        Call cFile.ShellOpenFile(Me.txtFile.Text, Me.Handle)
    End Sub

    ' Display file strings
    Public Sub DisplayFileStrings(ByVal lst As ListView, ByVal file As String)
        Dim s As String = vbNullString
        Dim sCtemp As String = vbNullString
        Dim x As Integer = 1
        Dim bTaille As Integer
        Dim lLen As Integer

        If IO.File.Exists(file) Then

            lst.Items.Clear()

            ' Retrieve entire file in memory
            ' Warn user if file is up to 2MB
            Try

                If FileLen(file) > 2000000 Then
                    If Misc.ShowMsg("File size is greater than 2MB. It is not recommended to open a large file, do you want to continue?",
                    "Show file strings",
                    MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                        lvFileString.Items.Add("Click on 'Others->Show file strings' to retrieve file strings")
                        Exit Sub
                    End If
                End If

                s = IO.File.ReadAllText(file)

            Catch ex As Exception
                Misc.ShowError(ex, "Could not read file")
            End Try


            ' Desired minimum size for a string
            bTaille = SIZE_FOR_STRING

            ' A char is considered as part of a string if its value is between 32 and 122
            lLen = Len(s)

            ' Lock listbox
            lst.BeginUpdate()

            ' Ok, parse file
            Do Until x > lLen

                If Char.IsLetterOrDigit(s.Chars(x - 1)) Then
                    ' Valid char
                    sCtemp &= s.Chars(x - 1)
                Else
                    'sCtemp = LTrim$(sCtemp)
                    'sCtemp = RTrim$(sCtemp)
                    If Len(sCtemp) > bTaille Then
                        lst.Items.Add(sCtemp)
                    End If
                    sCtemp = vbNullString
                End If

                x += 1
            Loop

            ' Last item
            If Len(sCtemp) > bTaille Then
                lst.Items.Add(sCtemp)
            End If

            ' Unlock listbox
            lst.EndUpdate()
        End If

    End Sub

    Private Sub butMonitoringAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitoringAdd.Click
        Dim frm As New frmAddProcessMonitor(Program.Connection)
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    ' Add a monitoring item
    Public Sub AddMonitoringItem(ByVal it As cMonitor)

        ' Check if a node with same category and instance exists
        Dim nExistingItem As TreeNode = Nothing
        Dim n As TreeNode
        For Each n In Me.tvMonitor.Nodes.Item(0).Nodes
            If CStr(IIf(Len(it.InstanceName) > 0, it.InstanceName & " - ", vbNullString)) &
                        it.CategoryName = n.Text Then
                nExistingItem = n
                Exit For
            End If
        Next

        If nExistingItem Is Nothing Then
            ' New sub item
            Dim n1 As New TreeNode
            With n1
                .Text = CStr(IIf(Len(it.InstanceName) > 0, it.InstanceName & " - ",
                   vbNullString)) & it.CategoryName
                .ImageIndex = 0
                .SelectedImageIndex = 0
            End With

            Dim ncpu As New TreeNode
            With ncpu
                .Text = it.CounterName
                .ImageKey = "sub"
                .SelectedImageKey = "sub"
                .Tag = it
            End With
            n1.Nodes.Add(ncpu)

            Me.tvMonitor.Nodes.Item(0).Nodes.Add(n1)
        Else
            ' Use existing sub item
            Dim ncpu As New TreeNode
            With ncpu
                .Text = it.CounterName
                .ImageKey = "sub"
                .SelectedImageKey = "sub"
                .Tag = it
            End With

            nExistingItem.Nodes.Add(ncpu)
        End If

    End Sub

    Private Sub butMonitorStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorStart.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                If tvMonitor.SelectedNode.Parent.Parent IsNot Nothing Then
                    ' Subsub item
                    Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                    it.StartMonitoring()
                    Call tvMonitor_AfterSelect(Nothing, Nothing)
                Else
                    ' Sub item
                    Dim n As TreeNode
                    For Each n In tvMonitor.SelectedNode.Nodes
                        Dim it As cMonitor = CType(n.Tag, cMonitor)
                        it.StartMonitoring()
                    Next
                End If
            Else
                ' All items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    Dim n2 As TreeNode
                    For Each n2 In n.Nodes
                        Dim it As cMonitor = CType(n2.Tag, cMonitor)
                        it.StartMonitoring()
                    Next
                Next
            End If
        End If
        Call UpdateMonitoringLog()
    End Sub

    Private Sub butMonitorStop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorStop.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                If tvMonitor.SelectedNode.Parent.Parent IsNot Nothing Then
                    ' Subsub item
                    Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                    it.StopMonitoring()
                    Call tvMonitor_AfterSelect(Nothing, Nothing)
                Else
                    ' Sub item
                    Dim n As TreeNode
                    For Each n In tvMonitor.SelectedNode.Nodes
                        Dim it As cMonitor = CType(n.Tag, cMonitor)
                        it.StopMonitoring()
                    Next
                End If
            Else
                ' All items
                Dim n As TreeNode
                For Each n In tvMonitor.SelectedNode.Nodes
                    Dim n2 As TreeNode
                    For Each n2 In n.Nodes
                        Dim it As cMonitor = CType(n2.Tag, cMonitor)
                        it.StopMonitoring()
                    Next
                Next
            End If
        End If
        Call UpdateMonitoringLog()
    End Sub

    ' Powerful recursive method to unload all cMonitor items in subnodes
    Private Sub RemoveSubNode(ByRef nod As TreeNode, ByRef n As TreeNodeCollection)
        Dim subn As TreeNode
        For Each subn In n
            RemoveSubNode(subn, subn.Nodes)
        Next
        ' It's a monitor sub item
        If nod.ImageKey = "sub" Then
            Dim it As cMonitor = CType(nod.Tag, cMonitor)
            If it IsNot Nothing Then it.Dispose()
            it = Nothing
        End If
    End Sub

    Private Sub butMonitoringRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitoringRemove.Click
        If tvMonitor.SelectedNode IsNot Nothing Then
            RemoveSubNode(tvMonitor.SelectedNode, tvMonitor.SelectedNode.Nodes)
            Dim nn As TreeNodeCollection = tvMonitor.SelectedNode.Nodes
            Dim cnn As Integer = nn.Count
            For i As Integer = cnn - 1 To 0 Step -1
                nn.Item(i).Remove()
            Next
            If tvMonitor.SelectedNode.Parent IsNot Nothing Then
                tvMonitor.SelectedNode.Remove()
            End If

            ' Remove all single items (no sub)
            nn = Me.tvMonitor.Nodes.Item(0).Nodes
            cnn = nn.Count
            For i As Integer = cnn - 1 To 0 Step -1
                If nn.Item(i).Nodes.Count = 0 Then
                    nn.Item(i).Remove()
                End If
            Next
        End If
        Call UpdateMonitoringLog()
    End Sub

    ' Display stats in graph
    Private Sub ShowMonitorStats(ByVal it As cMonitor, ByVal key1 As String, ByVal key2 As String,
        ByVal key3 As String)

        Me.timerMonitoring.Interval = it.Interval

        If it.Enabled = False Then
            Dim g As Graphics = Me.graphMonitor.CreateGraphics
            With g
                .Clear(Color.Black)
                .DrawString("You have to start monitoring.", Me.Font, Brushes.White, 0, 0)
                .Dispose()
            End With
            Exit Sub
        End If

        ' Get values from monitor item
        Dim v() As Graph.ValueItem
        Dim cCol As New Collection
        cCol = it.GetMonitorItems()

        ' Limit DT pickers
        Me.dtMonitorL.MaxDate = Date.Now
        Me.dtMonitorL.MinDate = it.MonitorCreationDate
        Me.dtMonitorR.MaxDate = Me.dtMonitorL.MaxDate
        Me.dtMonitorR.MinDate = Me.dtMonitorL.MinDate

        If cCol.Count > 0 Then

            ReDim v(cCol.Count)
            Dim c As cMonitor.MonitorStructure
            Dim i As Integer = 0

            For Each c In cCol
                If i < v.Length Then
                    v(i).y = CLng(c.value)
                    v(i).x = c.time
                    i += 1
                End If
            Next

            ReDim Preserve v(cCol.Count - 1)

            With Me.graphMonitor

                ' Set max and min (depends and dates chosen)
                If Me.chkMonitorLeftAuto.Checked And Me.chkMonitorRightAuto.Checked Then
                    ' Then no one fixed
                    .ViewMin = CInt(Math.Max(0, i - CInt(Val(Me.txtMonitorNumber.Text))))
                    .ViewMax = i - 1
                ElseIf Me.chkMonitorRightAuto.Checked Then
                    ' Then left fixed
                    .ViewMin = findViewIntegerFromDate(Me.dtMonitorL.Value, v, it)
                    .ViewMax = findViewMaxFromMin(.ViewMin, v)
                ElseIf Me.chkMonitorLeftAuto.Checked Then
                    ' Then right fixed
                    .ViewMax = findViewIntegerFromDate(Me.dtMonitorR.Value, v, it)
                    .ViewMin = findViewLast(.ViewMax)
                Else
                    ' Then both fixed
                    .ViewMax = findViewIntegerFromDate(Me.dtMonitorR.Value, v, it)
                    .ViewMin = findViewIntegerFromDate(Me.dtMonitorL.Value, v, it)
                End If

                .SetValues(v)
                .dDate = it.MonitorCreationDate
                .EnableGraph = True
                Call .Refresh()
            End With
        End If
    End Sub

    Private Sub timerMonitoring_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerMonitoring.Tick
        Call tvMonitor_AfterSelect(Nothing, Nothing)
    End Sub

    ' Return an integer that corresponds to a time in a monitor from a date
    Private Function findViewIntegerFromDate(ByVal d As Date, ByVal v() As Graph.ValueItem,
        ByVal monitor As cMonitor) As Integer

        Dim it As Graph.ValueItem
        Dim l As Long = d.Ticks
        Dim start As Long = monitor.MonitorCreationDate.Ticks
        Dim o As Integer = 0
        For Each it In v
            If (start + 10000 * it.x) >= l Then
                Return o
            End If
            o += 1
        Next

        Return CInt(v.Length - 1)
    End Function

    ' Return an integer that corresponds to min + txtMAX.value iterations
    Private Function findViewMaxFromMin(ByVal min As Integer, ByVal v() As Graph.ValueItem) As Integer
        Return Math.Min(v.Length - 1, min + CInt(Val(Me.txtMonitorNumber.Text)))
    End Function

    ' Return element of array with a distance of txtMAX.value to the end of the array
    Private Function findViewLast(ByVal max As Integer) As Integer
        Dim lMax As Integer = CInt(Val(Me.txtMonitorNumber.Text))
        Return Math.Max(0, max - lMax)
    End Function

    Private Sub butDeleteFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDeleteFile.Click
        cSelFile.WindowsKill()
    End Sub

    Private Sub butFileMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileMove.Click
        With Me.FolderChooser
            .Description = "Select new location"
            .SelectedPath = cFile.GetParentDir(cSelFile.Path)
            .ShowNewFolderButton = True
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFile.Text = cSelFile.WindowsMove(.SelectedPath)
                Call Me.refreshFileInfos(cSelFile.Path)
            End If
        End With
    End Sub

    Private Sub butFileCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileCopy.Click
        With Me.saveDial
            .AddExtension = True
            .FileName = cSelFile.Name
            .Filter = "All (*.*)|*.*"
            .InitialDirectory = cSelFile.GetParentDir
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                cSelFile.WindowCopy(.FileName)
            End If
        End With
    End Sub

    Private Sub butFileRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFileRename.Click
        Dim s As String = CInputBox("New name (name+extension) ?", "Select a new file name", cFile.GetFileName(cSelFile.Path))
        If s Is Nothing OrElse s.Equals(String.Empty) Then Exit Sub
        Me.txtFile.Text = cSelFile.WindowsRename(s)
        Call Me.refreshFileInfos(cSelFile.Path)
    End Sub

    Private Sub butServiceReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceReport.Click
        Dim frm As New frmSaveReport
        With frm
            .TopMost = _frmMain.TopMost
            .ListviewToSave = Me.lvServices
            .ShowDialog()
        End With
    End Sub

    Private Sub butAlwaysDisplay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butAlwaysDisplay.Click
        Call changeTopMost()
    End Sub

    Private Sub butPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butPreferences.Click
        Dim frm As New frmPreferences
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub butProcessDisplayDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessDisplayDetails.Click
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            Dim frm As New frmProcessInfo
            frm.SetProcess(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub lvServices_GotAnError(ByVal origin As String, ByVal msg As String) Handles lvServices.GotAnError
        Misc.ShowError("Error : " & msg & vbNewLine & "Origin : " & origin & vbNewLine & vbNewLine & "LitePM will be disconnected from the machine.")
        Call Me.DisconnectFromMachine()
    End Sub

    ' Refresh  task list in listview
    Public Sub refreshTaskList()

        ' Update list
        WindowProvider.Update(True, Me.lvTask.InstanceId)
        Me.lvTask.UpdateTheItems()

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Tasks" Then
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvTask.Items.Count) & " tasks running"
            End If
        End If

    End Sub

    Private Sub timerTask_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timerTask.Tick
        Call refreshTaskList()
    End Sub

    Private Sub butTaskRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTaskRefresh.Click
        Call refreshTaskList()
    End Sub

    Private Sub butTaskShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTaskShow.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.SetAsForegroundWindow()
        Next
    End Sub

    Private Sub butTaskEndTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butTaskEndTask.Click
        ' Close task
        If WarnDangerousAction("Are you sure you want to terminate these tasks ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.Close()
        Next
    End Sub

    Private Sub refreshNetworkList()

        ' Update current connections list
        NetworkConnectionsProvider.Update(Me.lvNetwork.InstanceId)

        If Me.Ribbon IsNot Nothing AndAlso Me.Ribbon.ActiveTab IsNot Nothing Then
            Dim ss As String = Me.Ribbon.ActiveTab.Text
            If ss = "Network" Then
                Me.Text = "Lite Process Monitor -- " & CStr(Me.lvNetwork.Items.Count) & " connections opened"
            End If
        End If
    End Sub

    Private Sub butNetworkRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNetworkRefresh.Click
        Call refreshNetworkList()
    End Sub

    Private Sub timerTrayIcon_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerTrayIcon.Tick
        ' Refresh infos
        Call Program.SystemInfo.RefreshInfo()

        Dim _cpuUsage As Double = Program.SystemInfo.CpuUsage
        Dim _physMemUsage As Double = Program.SystemInfo.PhysicalMemoryPercentageUsage
        Dim d As New Decimal(Decimal.Multiply(Program.SystemInfo.TotalPhysicalMemory, New Decimal(_physMemUsage)))

        If _cpuUsage > 1 Then _cpuUsage = 1

        Dim s As String = "CPU usage : " & CStr(Math.Round(100 * _cpuUsage, 3)) & " %"
        s &= vbNewLine & "Phys. mem. usage : " & GetFormatedSize(d) & " (" & CStr(Math.Round(100 * _physMemUsage, 3)) & " %)"

        Me.Tray.Text = s

        Program.TrayIcon.AddValue(1, _cpuUsage)
        Program.TrayIcon.AddValue(2, _physMemUsage)
    End Sub

    ' Update monitoring log
    Public Sub UpdateMonitoringLog()
        '        Dim s As String

        If Me.tvMonitor.Nodes.Item(0).Nodes.Count > 0 Then

            Me.lvMonitorReport.Items.Clear()
            Me.lvMonitorReport.BeginUpdate()
            For Each n As TreeNode In Me.tvMonitor.Nodes.Item(0).Nodes
                For Each n2 As TreeNode In n.Nodes

                    Dim it As cMonitor = CType(n2.Tag, cMonitor)

                    Dim k As String = n.Text
                    Try
                        Dim g As New ListViewGroup(k, k)
                        g.HeaderAlignment = HorizontalAlignment.Center
                        Me.lvMonitorReport.Groups.Add(g)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try

                    Dim lvit As New ListViewItem(it.CounterName)
                    lvit.SubItems.Add(it.MachineName)
                    lvit.SubItems.Add(it.MonitorCreationDate.ToLongDateString & " -- " & it.MonitorCreationDate.ToLongTimeString)
                    If it.LastStarted.Ticks > 0 Then
                        lvit.SubItems.Add(it.LastStarted.ToLongDateString & " -- " & it.LastStarted.ToLongTimeString)
                    Else
                        lvit.SubItems.Add("Not yet started")
                    End If
                    lvit.SubItems.Add(it.Enabled.ToString)
                    lvit.SubItems.Add(it.Interval.ToString)

                    lvit.Group = Me.lvMonitorReport.Groups.Item(k)
                    Me.lvMonitorReport.Items.Add(lvit)

                Next
            Next

            Me.lvMonitorReport.EndUpdate()
            Me.lvMonitorReport.BringToFront()

        Else
            Me.txtMonitoringLog.Text = "No process monitored." & vbNewLine & "Click on 'Add' button to monitor a process."
            Me.txtMonitoringLog.SelectionLength = 0
            Me.txtMonitoringLog.SelectionStart = 0
            Me.txtMonitoringLog.BringToFront()
        End If

    End Sub

    Private Sub butSaveProcessReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveProcessReport.Click
        Dim frm As New frmSaveReport
        With frm
            .TopMost = _frmMain.TopMost
            .ListviewToSave = Me.lvProcess
            .ShowDialog()
        End With
    End Sub

    ' Permute style of menus
    Public Sub permuteMenuStyle(ByVal ribbonStyle As Boolean)

        ' Change selected tab of tabStrip
        _ribbonStyle = ribbonStyle

        _main.Panel1Collapsed = Not (_ribbonStyle)

        Me.MenuItemSYSTEMFILE.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMOPT.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMTOOLS.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMSYSTEM.Visible = Not (_ribbonStyle)
        Me.MenuItemSYSTEMHEL.Visible = Not (_ribbonStyle)

        Call Me.frmMain_Resize(Nothing, Nothing)
    End Sub

    Private Sub txtServiceSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServiceSearch.Click
        Call txtServiceSearch_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub txtServiceSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServiceSearch.TextChanged
        Dim it As ListViewItem
        Dim comp As String = Me.txtServiceSearch.Text.ToLowerInvariant
        For Each it In Me.lvServices.Items
            Dim add As Boolean = False
            For Each subit As ListViewItem.ListViewSubItem In it.SubItems
                Dim ss As String = subit.Text
                If subit IsNot Nothing Then
                    If InStr(ss.ToLowerInvariant, comp, CompareMethod.Binary) > 0 Then
                        add = True
                        Exit For
                    End If
                End If
            Next
            If add = False Then
                it.Group = lvServices.Groups(0)
            Else
                it.Group = lvServices.Groups(1)
            End If
        Next
        Me.lblResCount2.Text = CStr(lvServices.Groups(1).Items.Count) & " result(s)"
    End Sub

    Public Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Call txtSearch_TextChanged(Nothing, Nothing)
    End Sub

    Public Sub txtSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim it As ListViewItem
        Dim comp As String = Me.txtSearch.Text.ToLowerInvariant
        For Each it In Me.lvProcess.Items
            Dim add As Boolean = False
            For Each subit As ListViewItem.ListViewSubItem In it.SubItems
                Dim ss As String = subit.Text
                If subit IsNot Nothing Then
                    If InStr(ss.ToLowerInvariant, comp, CompareMethod.Binary) > 0 Then
                        add = True
                        Exit For
                    End If
                End If
            Next
            If add = False Then
                it.Group = lvProcess.Groups(0)
            Else
                it.Group = lvProcess.Groups(1)
            End If
        Next
        Me.lblResCount.Text = CStr(lvProcess.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub txtFile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFile.TextChanged
        Dim b As Boolean = IO.File.Exists(Me.txtFile.Text)
        Me.RBFileDelete.Enabled = b
        Me.RBFileKillProcess.Enabled = False 'TOCHANGE
        Me.RBFileOnline.Enabled = b
        Me.RBFileOther.Enabled = b
        Me.RBFileOthers.Enabled = b
    End Sub

    Private Sub tvMonitor_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvMonitor.AfterSelect

        Me.graphMonitor.EnableGraph = False

        If tvMonitor.SelectedNode Is Nothing Then Exit Sub

        If tvMonitor.SelectedNode.Parent IsNot Nothing Then
            If tvMonitor.SelectedNode.Parent.Name = tvMonitor.Nodes.Item(0).Name Then
                ' Then we have selected a process
                Me.butMonitorStart.Enabled = True
                Me.butMonitorStop.Enabled = True
                Dim g As Graphics = Me.graphMonitor.CreateGraphics
                With g
                    .Clear(Color.Black)
                    .DrawString("Select in the treeview a counter.", Me.Font, Brushes.White, 0, 0)
                    .Dispose()
                End With
            Else
                Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
                Me.butMonitorStart.Enabled = Not (it.Enabled)
                Me.butMonitorStop.Enabled = it.Enabled

                ' We have selected a sub item -> display values in graph
                Dim sKey As String = tvMonitor.SelectedNode.Text
                Call ShowMonitorStats(it, sKey, "", "")
            End If
        Else
            ' The we can start/stop all items
            Me.butMonitorStart.Enabled = True
            Me.butMonitorStop.Enabled = True
            Dim g As Graphics = Me.graphMonitor.CreateGraphics
            With g
                .Clear(Color.Black)
                .DrawString("Select in the treeview an item and then a counter.", Me.Font, Brushes.White, 0, 0)
                .Dispose()
            End With
        End If

        Me.MenuItemMonitorStart.Enabled = Me.butMonitorStart.Enabled
        Me.MenuItemMonitorStop.Enabled = Me.butMonitorStop.Enabled
    End Sub

    Private Sub tvProc_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvProc.AfterCollapse
        Me.lvProcess.Items(0).Group = Me.lvProcess.Groups(1)
    End Sub

    Private Sub rtb2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb2.TextChanged
        Me.cmdCopyServiceToCp.Enabled = (rtb2.Rtf.Length > 0)
    End Sub

    Private Sub lvTask_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvTask.DoubleClick
        If MenuItemTaskSelProc.Enabled Then _
        Call Me.MenuItemTaskSelProc_Click(Nothing, Nothing)
    End Sub

    Private Sub lvTask_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvTask.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvTask)
    End Sub

    Private Sub lvServices_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvServices.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Me.butServiceDetails_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Delete Then
            If _notWMI Then
                Call butDeleteService_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub lvServices_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            For Each it As cService In Me.lvServices.GetSelectedItems
                Dim frm As New frmServiceInfo
                frm.SetService(it)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        End If
    End Sub

    Private Sub lvServices_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvServices)
    End Sub

    Private Sub lvServices_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvServices.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvServices.SelectedItems IsNot Nothing _
                    AndAlso Me.lvServices.SelectedItems.Count > 0)

            If _notSnapshotMode Then
                If lvServices.SelectedItems.Count = 1 Then
                    Dim cSe As cService = Me.lvServices.GetSelectedItem
                    Dim start As Native.Api.NativeEnums.ServiceStartType = cSe.Infos.StartType
                    Dim state As Native.Api.NativeEnums.ServiceState = cSe.Infos.State
                    Dim acc As Native.Api.NativeEnums.ServiceAccept = cSe.Infos.AcceptedControl

                    Me.MenuItemServPause.Text = IIf(state = Native.Api.NativeEnums.ServiceState.Running, "Pause", "Resume").ToString
                    MenuItemServPause.Enabled = (acc And Native.Api.NativeEnums.ServiceAccept.PauseContinue) = Native.Api.NativeEnums.ServiceAccept.PauseContinue
                    MenuItemServStart.Enabled = Not (state = Native.Api.NativeEnums.ServiceState.Running)
                    Me.MenuItemServStop.Enabled = (acc And Native.Api.NativeEnums.ServiceAccept.Stop) = Native.Api.NativeEnums.ServiceAccept.Stop

                    Me.MenuItemServDisabled.Checked = (start = Native.Api.NativeEnums.ServiceStartType.StartDisabled)
                    MenuItemServDisabled.Enabled = Not (MenuItemServDisabled.Checked)
                    MenuItemServAutoStart.Checked = (start = Native.Api.NativeEnums.ServiceStartType.AutoStart)
                    MenuItemServAutoStart.Enabled = Not (MenuItemServAutoStart.Checked)
                    MenuItemServOnDemand.Checked = (start = Native.Api.NativeEnums.ServiceStartType.DemandStart)
                    MenuItemServOnDemand.Enabled = Not (MenuItemServOnDemand.Checked)
                    MenuItemServStartType.Enabled = True
                ElseIf lvServices.SelectedItems.Count > 1 Then
                    MenuItemServPause.Text = "Pause"
                    MenuItemServPause.Enabled = True
                    MenuItemServStart.Enabled = True
                    MenuItemServStop.Enabled = True
                    MenuItemServDisabled.Checked = True
                    MenuItemServDisabled.Enabled = True
                    MenuItemServAutoStart.Checked = True
                    MenuItemServAutoStart.Enabled = True
                    MenuItemServOnDemand.Checked = True
                    MenuItemServOnDemand.Enabled = True
                    MenuItemServStartType.Enabled = True
                ElseIf lvServices.SelectedItems.Count = 0 Then
                    MenuItemServPause.Text = "Pause"
                    MenuItemServPause.Enabled = False
                    MenuItemServStart.Enabled = False
                    MenuItemServStop.Enabled = False
                    MenuItemServDisabled.Checked = False
                    MenuItemServDisabled.Enabled = False
                    MenuItemServAutoStart.Checked = False
                    MenuItemServAutoStart.Enabled = False
                    MenuItemServOnDemand.Checked = False
                    MenuItemServOnDemand.Enabled = False
                    MenuItemServStartType.Enabled = False
                End If
            Else
                MenuItemServPause.Text = "Pause"
                MenuItemServPause.Enabled = False
                MenuItemServStart.Enabled = False
                MenuItemServStop.Enabled = False
                MenuItemServDisabled.Checked = False
                MenuItemServDisabled.Enabled = False
                MenuItemServAutoStart.Checked = False
                MenuItemServAutoStart.Enabled = False
                MenuItemServOnDemand.Checked = False
                MenuItemServOnDemand.Enabled = False
                MenuItemServStartType.Enabled = False
            End If

            Me.MenuItemServFileDetails.Enabled = selectionIsNotNothing AndAlso _local AndAlso Me.lvServices.SelectedItems.Count = 1
            Me.MenuItemServFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServSearch.Enabled = selectionIsNotNothing
            Me.MenuItemServDepe.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemServSelService.Enabled = selectionIsNotNothing
            Me.MenuItemServReanalize.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemCopyService.Enabled = selectionIsNotNothing
            Me.MenuItemServDelete.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode

            Me.mnuService.Show(Me.lvServices, e.Location)
        End If
    End Sub

    Private Sub lvServices_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvServices.SelectedIndexChanged
        ' New service selected
        If lvServices.SelectedItems.Count = 1 Then
            Try
                Dim cS As cService = Me.lvServices.GetSelectedItem

                Me.lblServiceName.Text = "Service name : " & cS.Infos.Name
                Me.lblServicePath.Text = "Service path : " & cS.GetInformation("ImagePath")

                ' Description
                Dim s As String = vbNullString
                Dim description As String = vbNullString
                Dim diagnosticsMessageFile As String = cS.Infos.DiagnosticMessageFile
                Dim group As String = cS.Infos.LoadOrderGroup
                Dim objectName As String = cS.Infos.ObjectName
                Dim sp As String = cS.GetInformation("ImagePath")

                ' OK it's not the best way to retrive the description...
                ' (if @ -> extract string to retrieve description)
                Dim sTemp As String = cS.Infos.Description
                If InStr(sTemp, "@", CompareMethod.Binary) > 0 Then
                    description = Native.Objects.File.GetResourceStringFromFile(sTemp)
                Else
                    description = sTemp
                End If
                description = Replace(cS.Infos.Description, "\", "\\")


                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b Service properties\b0\par"
                s = s & "\tab Name :\tab\tab\tab " & cS.Infos.Name & "\par"
                s = s & "\tab Common name :\tab\tab " & cS.Infos.DisplayName & "\par"
                If Len(sp) > 0 Then s = s & "\tab Path :\tab\tab\tab " & Replace(cS.GetInformation("ImagePath"), "\", "\\") & "\par"
                If Len(description) > 0 Then s = s & "\tab Description :\tab\tab " & description & "\par"
                If Len(group) > 0 Then s = s & "\tab Group :\tab\tab\tab " & group & "\par"
                If Len(objectName) > 0 Then s = s & "\tab ObjectName :\tab\tab " & objectName & "\par"
                If Len(diagnosticsMessageFile) > 0 Then s = s & "\tab DiagnosticsMessageFile :\tab\tab " & diagnosticsMessageFile & "\par"
                s = s & "\tab State :\tab\tab\tab " & cS.Infos.State.ToString & "\par"
                s = s & "\tab Startup :\tab\tab " & cS.Infos.StartType.ToString & "\par"
                If cS.Infos.ProcessId > 0 Then s = s & "\tab Owner process :\tab\tab " & cS.Infos.ProcessId & "-- " & cProcess.GetProcessName(cS.Infos.ProcessId) & "\par"
                s = s & "\tab Service type :\tab\tab " & cS.Infos.ServiceType.ToString & "\par"

                s = s & "}"

                rtb2.Rtf = s

                ' Treeviews (only if we are in local mode)
                If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
                    With tv
                        .RootService = cS.Infos.Name
                        .InfosToGet = cServDepConnection.DependenciesToget.DependenciesOfMe
                        .UpdateItems()
                    End With
                    With tv2
                        .RootService = cS.Infos.Name
                        .InfosToGet = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
                        .UpdateItems()
                    End With
                Else
                    tv.ClearItems()
                    tv.SafeAdd("No auto refresh for remote monitoring")
                    tv2.ClearItems()
                    tv2.SafeAdd("No auto refresh for remote monitoring")
                End If

            Catch ex As Exception
                Dim s As String = ""
                Dim er As Exception = ex

                s = "{\rtf1\ansi\ansicpg1252\deff0\deflang1036{\fonttbl{\f0\fswiss\fprq2\fcharset0 Tahoma;}}"
                s = s & "{\*\generator Msftedit 5.41.21.2508;}\viewkind4\uc1\pard\f0\fs18   \b An error occured\b0\par"
                s = s & "\tab Message :\tab " & er.Message & "\par"
                s = s & "\tab Source :\tab\tab " & er.Source & "\par"
                If Len(er.HelpLink) > 0 Then s = s & "\tab Help link :\tab " & er.HelpLink & "\par"
                s = s & "}"

                rtb2.Rtf = s
            End Try

        End If
    End Sub

    Private Sub lvSearchResults_HasRefreshed() Handles lvSearchResults.HasRefreshed
        Me.butSearchGo.Enabled = True
        Me.butSearchSaveReport.Enabled = True
        Me.MenuItemSearchNew.Enabled = True
        If Ribbon.ActiveTab.Text = "Search" Then
            Me.Text = "Lite Process Monitor -- " & CStr(Me.lvSearchResults.Items.Count) & " search results"
        End If
    End Sub

    Private Sub lvSearchResults_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSearchResults.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvSearchResults)
    End Sub

    Private Sub lvProcess_GotAnError(ByVal origin As String, ByVal msg As String) Handles lvProcess.GotAnError
        Misc.ShowError("Error : " & msg & vbNewLine & "Origin : " & origin & vbNewLine & vbNewLine & "LitePM will be disconnected from the machine.")
        Call Me.DisconnectFromMachine()
    End Sub

#Region "Notifications (new/deleted process/service)"

    Private Sub processCreated(ByVal pids As List(Of Integer), ByVal newItems As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
        If res.Success AndAlso instanceId = Me.lvProcess.InstanceId Then    ' Associated with lvProcess
            For Each id As Integer In pids
                Dim item As cProcess = ProcessProvider.GetProcessById(id)
                If item IsNot Nothing AndAlso ProcessProvider.FirstRefreshDone Then
                    Program.Log.AppendLine("Process created : " & item.Infos.Name & " (" & item.Infos.ProcessId & ")")
                    If Me.MenuItemTaskSelProc.Enabled = False Then
                        MenuItemTaskSelProc.Enabled = True
                    End If
                    If My.Settings.NotifyNewProcesses Then
                        Dim text As String = "Name : " & item.Infos.Name & " (" & item.Infos.ProcessId.ToString & ")"
                        If item.Infos.ParentProcessId > 0 Then
                            text &= vbNewLine & "Parent : " &
                                cProcess.GetProcessName(item.Infos.ParentProcessId) & " (" &
                                cProcess.GetProcessName(item.Infos.ParentProcessId) & ")"
                        End If
                        If item.Infos.FileInfo IsNot Nothing Then
                            text &= vbNewLine & "Company : " & item.Infos.FileInfo.CompanyName &
                                vbNewLine & "Description : " & item.Infos.FileInfo.FileDescription
                        End If
                        With Me.Tray
                            .BalloonTipText = text
                            .BalloonTipIcon = ToolTipIcon.Info
                            .BalloonTipTitle = "A new process has been started"
                            .ShowBalloonTip(3000)
                        End With
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub processDeleted(ByVal processes As Dictionary(Of Integer, processInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
        If res.Success AndAlso instanceId = Me.lvProcess.InstanceId Then    ' Associated with lvProcess
            For Each id As Integer In processes.Keys
                Dim item As processInfos = processes(id)
                If item IsNot Nothing Then
                    Program.Log.AppendLine("Process deleted : " & item.Name & " (" & item.ProcessId & ")")
                    If My.Settings.NotifyTerminatedProcesses Then
                        Dim text As String = "Name : " & item.Name & " (" & item.ProcessId.ToString & ")"
                        If item.ParentProcessId > 0 Then
                            text &= vbNewLine & "Parent : " &
                                cProcess.GetProcessName(item.ParentProcessId) & " (" &
                                cProcess.GetProcessName(item.ParentProcessId) & ")"
                        End If
                        If item.FileInfo IsNot Nothing Then
                            text &= vbNewLine & "Company : " & item.FileInfo.CompanyName &
                                vbNewLine & "Description : " & item.FileInfo.FileDescription
                        End If
                        With Me.Tray
                            .BalloonTipText = text
                            .BalloonTipIcon = ToolTipIcon.Info
                            .BalloonTipTitle = "A process has been terminated"
                            .ShowBalloonTip(3000)
                        End With
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub serviceCreated(ByVal names As List(Of String), ByVal newItems As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
        If res.Success AndAlso instanceId = Me.lvServices.InstanceId Then    ' Associated with lvProcess
            For Each serviceName As String In names
                Dim item As cService = ServiceProvider.GetServiceByName(serviceName)
                If item IsNot Nothing AndAlso ServiceProvider.FirstRefreshDone Then
                    Program.Log.AppendLine("Service created : " & item.Infos.Name & " (" & item.Infos.ProcessId & ")")
                    If My.Settings.NotifyNewServices Then
                        Dim text As String = "Name : " & item.Infos.Name
                        If item.Infos.ProcessId > 0 Then
                            text &= " (" & item.Infos.ProcessId.ToString & ")"
                        End If
                        If item.Infos.FileInfo IsNot Nothing Then
                            text &= vbNewLine & "Company : " & item.Infos.FileInfo.CompanyName &
                                vbNewLine & "Description : " & item.Infos.FileInfo.FileDescription
                        End If
                        With Me.Tray
                            .BalloonTipText = text
                            .BalloonTipIcon = ToolTipIcon.Info
                            .BalloonTipTitle = "A new service has been created"
                            .ShowBalloonTip(3000)
                        End With
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub serviceDeleted(ByVal names As Dictionary(Of String, serviceInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
        If res.Success AndAlso instanceId = Me.lvServices.InstanceId Then    ' Associated with lvProcess
            For Each serviceName As String In names.Keys
                Dim item As serviceInfos = names(serviceName)
                If item IsNot Nothing Then
                    Program.Log.AppendLine("Service deleted : " & item.Name & " (" & item.ProcessId & ")")
                    If My.Settings.NotifyDeletedServices Then
                        Dim text As String = "Name : " & item.Name
                        If item.ProcessId > 0 Then
                            text &= " (" & item.ProcessId.ToString & ")"
                        End If
                        If item.FileInfo IsNot Nothing Then
                            text &= vbNewLine & "Company : " & item.FileInfo.CompanyName &
                                vbNewLine & "Description : " & item.FileInfo.FileDescription
                        End If
                        With Me.Tray
                            .BalloonTipText = text
                            .BalloonTipIcon = ToolTipIcon.Info
                            .BalloonTipTitle = "A service has been deleted"
                            .ShowBalloonTip(3000)
                        End With
                    End If
                End If
            Next
        End If
    End Sub

#End Region

    Private Sub lvProcess_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvProcess.KeyDown
        If e.KeyCode = Keys.Delete And Me.lvProcess.SelectedItems.Count > 0 Then
            If WarnDangerousAction("Are you sure you want to kill these processes ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
            For Each it As cProcess In Me.lvProcess.GetSelectedItems
                it.Kill()
            Next
        ElseIf e.KeyCode = Keys.Enter And Me.lvProcess.SelectedItems.Count > 0 Then
            Call Me.butProcessDisplayDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvProcess_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Call Me.butProcessDisplayDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvProcess_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseDown
        Common.Misc.CopyLvToClip(e, Me.lvProcess)
    End Sub

    Private Sub lvProcess_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvProcess.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim p As Integer = -1
            If Me.lvProcess.SelectedItems Is Nothing Then
                Me.MenuItemProcPIdle.Checked = False
                Me.MenuItemProcPN.Checked = False
                Me.MenuItemProcPAN.Checked = False
                Me.MenuItemProcPBN.Checked = False
                Me.MenuItemProcPH.Checked = False
                Me.MenuItemProcPRT.Checked = False
                Exit Sub
            End If
            If Me.lvProcess.SelectedItems.Count = 1 Then
                p = Me.lvProcess.GetSelectedItem.Infos.Priority
            End If
            Me.MenuItemProcPIdle.Checked = (p = ProcessPriorityClass.Idle)
            Me.MenuItemProcPN.Checked = (p = ProcessPriorityClass.Normal)
            Me.MenuItemProcPAN.Checked = (p = ProcessPriorityClass.AboveNormal)
            Me.MenuItemProcPBN.Checked = (p = ProcessPriorityClass.BelowNormal)
            Me.MenuItemProcPH.Checked = (p = ProcessPriorityClass.High)
            Me.MenuItemProcPRT.Checked = (p = ProcessPriorityClass.RealTime)

            Dim selectionIsNotNothing As Boolean = (Me.lvProcess.SelectedItems IsNot Nothing AndAlso Me.lvProcess.SelectedItems.Count > 0)
            Me.MenuItemProcOther.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemProcKill.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemProcPriority.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemProcReanalize.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemProcResume.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemProcKillT.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemProcStop.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemProcResume.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemProcSFileProp.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSOpenDir.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcSSearch.Enabled = selectionIsNotNothing
            Me.MenuItemProcSDep.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemCopyProcess.Enabled = selectionIsNotNothing
            Me.MenuItemProcSFileDetails.Enabled = (selectionIsNotNothing AndAlso _local AndAlso Me.lvProcess.SelectedItems.Count = 1)
            Me.MenuItemProcDump.Enabled = selectionIsNotNothing AndAlso _local
            Me.MenuItemProcAff.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemProcWSS.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemProcKillByMethod.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode

            ' Job menuitems
            Me.MenuItemProcJob.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            If Me.lvProcess.SelectedItems.Count <> 1 Then
                Me.MenuItemJobMng.Enabled = True
            Else
                ' We currently can not get process job by id synchronously
                ' if we are using remote monitoring
                If _local Then
                    Me.MenuItemJobMng.Enabled = (cJob.GetProcessJobById(Me.lvProcess.GetSelectedItem.Infos.ProcessId) IsNot Nothing)
                    Me.MenuItemProcAddToJob.Enabled = Not (Me.MenuItemJobMng.Enabled)
                Else
                    Me.MenuItemProcAddToJob.Enabled = True
                    Me.MenuItemJobMng.Enabled = False
                End If
            End If

            Me.mnuProcess.Show(Me.lvProcess, e.Location)
        End If
    End Sub

    Private Sub lstFileString_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvFileString.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Dim s As String = vbNullString
            Dim it As ListViewItem
            Dim x As Integer = 0
            For Each it In Me.lvFileString.SelectedItems
                s &= it.Text
                x += 1
                If Not (x = Me.lvFileString.SelectedItems.Count) Then s &= vbNewLine
            Next
            If Not (s = vbNullString) Then My.Computer.Clipboard.SetText(s, TextDataFormat.UnicodeText)
        End If
    End Sub

    Private Sub lblTaskCountResult_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblTaskCountResult.MouseDown
        If Me.lvTask.Groups(1).Items.Count > 0 Then
            Me.lvTask.Focus()
            Me.lvTask.EnsureVisible(Me.lvTask.Groups(1).Items(0).Index)
            Me.lvTask.SelectedItems.Clear()
            Me.lvTask.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub txtSearchTask_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchTask.TextChanged
        Dim it As ListViewItem
        Dim comp As String = Me.txtSearchTask.Text.ToLowerInvariant
        For Each it In Me.lvTask.Items
            Dim add As Boolean = False
            For Each subit As ListViewItem.ListViewSubItem In it.SubItems
                Dim ss As String = subit.Text
                If subit IsNot Nothing Then
                    If InStr(ss.ToLowerInvariant, comp, CompareMethod.Binary) > 0 Then
                        add = True
                        Exit For
                    End If
                End If
            Next
            If add = False Then
                it.Group = lvTask.Groups(0)
            Else
                it.Group = lvTask.Groups(1)
            End If
        Next
        Me.lblTaskCountResult.Text = CStr(lvTask.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblResCount2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResCount2.Click
        If Me.lvServices.Groups(1).Items.Count > 0 Then
            Me.lvServices.Focus()
            Me.lvServices.EnsureVisible(Me.lvServices.Groups(1).Items(0).Index)
            Me.lvServices.SelectedItems.Clear()
            Me.lvServices.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub lblResCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResCount.Click
        If Me.lvProcess.Groups(1).Items.Count > 0 Then
            Me.lvProcess.Focus()
            Me.lvProcess.EnsureVisible(Me.lvProcess.Groups(1).Items(0).Index)
            Me.lvProcess.SelectedItems.Clear()
            Me.lvProcess.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub graphMonitor_OnZoom(ByVal leftVal As Integer, ByVal rightVal As Integer) Handles graphMonitor.OnZoom
        ' Change dates and set view as fixed (left and right)
        Try
            Dim it As cMonitor = CType(tvMonitor.SelectedNode.Tag, cMonitor)
            Dim l As New Date(it.MonitorCreationDate.Ticks + leftVal * 10000)
            Dim r As New Date(it.MonitorCreationDate.Ticks + rightVal * 10000)
            Me.dtMonitorL.Value = l
            Me.dtMonitorR.Value = r
            Me.chkMonitorLeftAuto.Checked = False
            Me.chkMonitorRightAuto.Checked = False
        Catch ex As Exception
            Misc.ShowError(ex, "Could not zoom on graph")
        End Try
    End Sub

    Private Sub chkFileArchive_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileArchive.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileArchive.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.Archive)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.Archive)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileArchive.Checked = Not (Me.chkFileArchive.Checked)
        End Try
    End Sub

    Private Sub chkFileHidden_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileHidden.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileHidden.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.Hidden)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.Hidden)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileHidden.Checked = Not (Me.chkFileHidden.Checked)
        End Try
    End Sub

    Private Sub chkFileReadOnly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileReadOnly.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileReadOnly.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.ReadOnly)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.ReadOnly)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileReadOnly.Checked = Not (Me.chkFileReadOnly.Checked)
        End Try
    End Sub

    Private Sub chkFileContentNotIndexed_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileContentNotIndexed.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileContentNotIndexed.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.NotContentIndexed)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.NotContentIndexed)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileContentNotIndexed.Checked = Not (Me.chkFileContentNotIndexed.Checked)
        End Try
    End Sub

    Private Sub chkFileNormal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileNormal.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileNormal.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.Normal)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.Normal)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileNormal.Checked = Not (Me.chkFileNormal.Checked)
        End Try
    End Sub

    Private Sub chkFileSystem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFileSystem.CheckedChanged
        Static accessed As Boolean = False
        If accessed Then
            accessed = False
            Exit Sub
        End If
        Try
            If Me.chkFileSystem.Checked Then
                cSelFile.Attributes = AddAttribute(IO.FileAttributes.System)
            Else
                cSelFile.Attributes = RemoveAttribute(IO.FileAttributes.System)
            End If
        Catch ex As Exception
            accessed = True
            Me.chkFileSystem.Checked = Not (Me.chkFileSystem.Checked)
        End Try
    End Sub

    Private Sub chkMonitorLeftAuto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMonitorLeftAuto.CheckedChanged
        Me.dtMonitorL.Enabled = Not (Me.chkMonitorLeftAuto.Checked)
        Me.txtMonitorNumber.Enabled = Not (Me.chkMonitorLeftAuto.Checked = False And Me.chkMonitorRightAuto.Checked = False)
        Me.lblMonitorMaxNumber.Enabled = Me.txtMonitorNumber.Enabled
    End Sub

    Private Sub chkMonitorRightAuto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMonitorRightAuto.CheckedChanged
        Me.dtMonitorR.Enabled = Not (Me.chkMonitorRightAuto.Checked)
        Me.txtMonitorNumber.Enabled = Not (Me.chkMonitorLeftAuto.Checked = False And Me.chkMonitorRightAuto.Checked = False)
        Me.lblMonitorMaxNumber.Enabled = Me.txtMonitorNumber.Enabled
    End Sub

    Private Sub chkSearchProcess_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSearchProcess.CheckedChanged
        Me.chkSearchModules.Enabled = (Me.chkSearchProcess.Checked)
        Me.chkSearchEnvVar.Enabled = (Me.chkSearchProcess.Checked)
    End Sub

    Private Sub cmdCopyServiceToCp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopyServiceToCp.Click
        If Me.rtb2.Text.Length > 0 Then
            My.Computer.Clipboard.SetText(Me.rtb2.Text, TextDataFormat.Text)
        End If
    End Sub

    Private Sub cmdCopyServiceToCp_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdCopyServiceToCp.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.rtb2.Rtf.Length > 0 Then
                My.Computer.Clipboard.SetText(Me.rtb2.Rtf, TextDataFormat.Rtf)
            End If
        End If
    End Sub

    Private Sub cmdFileClipboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFileClipboard.Click
        If Me.rtb3.Text.Length > 0 Then
            My.Computer.Clipboard.SetText(Me.rtb3.Text, TextDataFormat.Text)
        End If
    End Sub

    Private Sub cmdFileClipboard_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdFileClipboard.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.rtb3.Rtf.Length > 0 Then
                My.Computer.Clipboard.SetText(Me.rtb3.Rtf, TextDataFormat.Rtf)
            End If
        End If
    End Sub

    Private Sub cmdSetFileDates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSetFileDates.Click
        ' Set new dates
        Try
            cSelFile.CreationTime = Me.DTcreation.Value
            cSelFile.LastAccessTime = Me.DTlastAccess.Value
            cSelFile.LastWriteTime = Me.DTlastModification.Value
            Misc.ShowMsg("Set file dates", "New dates have been set successfully.", MessageBoxButtons.OK)
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to change dates")
        End Try
    End Sub

    Private Sub dtMonitorL_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtMonitorL.ValueChanged
        If Me.chkMonitorLeftAuto.Checked = False Then
            Call tvMonitor_AfterSelect(Nothing, Nothing)
        End If
    End Sub

    Private Sub dtMonitorR_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtMonitorR.ValueChanged
        If Me.chkMonitorRightAuto.Checked = False Then
            Call tvMonitor_AfterSelect(Nothing, Nothing)
        End If
    End Sub

    Private Sub txtSearchResults_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtSearchResults.MouseDown
        Dim it As ListViewItem
        Dim comp As String = Me.lvSearchResults.Text.ToLowerInvariant
        For Each it In Me.lvSearchResults.Items
            Dim add As Boolean = False
            For Each subit As ListViewItem.ListViewSubItem In it.SubItems
                Dim ss As String = subit.Text
                If subit IsNot Nothing Then
                    If InStr(ss.ToLowerInvariant, comp, CompareMethod.Binary) > 0 Then
                        add = True
                        Exit For
                    End If
                End If
            Next
            If add = False Then
                it.Group = lvSearchResults.Groups(0)
            Else
                it.Group = lvSearchResults.Groups(1)
            End If
        Next
        Me.lblResultsCount.Text = CStr(lvSearchResults.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub txtSearchResults_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchResults.TextChanged
        Dim it As ListViewItem
        For Each it In Me.lvSearchResults.Items
            If InStr(LCase(it.SubItems(1).Text), LCase(Me.txtSearchResults.Text)) = 0 Then
                it.Group = lvSearchResults.Groups(0)
            Else
                it.Group = lvSearchResults.Groups(1)
            End If
        Next
        Me.lblResultsCount.Text = CStr(lvSearchResults.Groups(1).Items.Count) & " result(s)"
    End Sub

    Private Sub lblResultsCount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblResultsCount.Click
        If Me.lvSearchResults.Groups(1).Items.Count > 0 Then
            Me.lvSearchResults.Focus()
            Me.lvSearchResults.EnsureVisible(Me.lvSearchResults.Groups(1).Items(0).Index)
            Me.lvSearchResults.SelectedItems.Clear()
            Me.lvSearchResults.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub _tab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tab.SelectedIndexChanged
        Static bHelpShown As Boolean = False

        ' Hide specific menus
        If My.Settings.UseRibbonStyle = False Then
            Me.MenuItemProcesses.Visible = False
            Me.MenuItemMonitor.Visible = False
            Me.MenuItemServices.Visible = False
            Me.MenuItemFiles.Visible = False
            Me.MenuItemSearch.Visible = False
            Me.MenuItemJobs.Visible = False
        End If

        ' Change current tab of ribbon
        Dim theTab As RibbonTab = Me.TaskTab
        My.Settings.SelectedTab = _tab.TabPages(_tab.SelectedIndex).Text
        Select Case My.Settings.SelectedTab
            Case "Tasks"
                theTab = Me.TaskTab
            Case "Processes"
                theTab = Me.ProcessTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemProcesses.Visible = True
                End If
            Case "Jobs"
                theTab = Me.JobTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemJobs.Visible = True
                End If
            Case "Monitor"
                theTab = Me.MonitorTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemMonitor.Visible = True
                End If
            Case "Services"
                theTab = Me.ServiceTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemServices.Visible = True
                End If
            Case "Network"
                theTab = Me.NetworkTab
            Case "File"
                theTab = Me.FileTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemFiles.Visible = True
                End If
            Case "Search"
                theTab = Me.SearchTab
                If My.Settings.UseRibbonStyle = False Then
                    Me.MenuItemSearch.Visible = True
                End If
                'Case "Help"
                '    theTab = Me.HelpTab
                '    Me.Text = "Lite Process Monitor -- " & CStr(Me.lvProcess.Items.Count) & " processes running"
                '    If Not (bHelpShown) Then
                '        bHelpShown = True
                '        ' Load help file
                '        If IO.File.Exists(My.Application.Info.DirectoryPath & HELP_PATH_DD) Then
                '            'WBHelp.Document.Write("<body link=blue vlink=purple><span>Help file cannot be found. <p></span><span>Please download help file at <a href=" & Chr(34) & "https://" & Chr(34) & ">https://</a> and save it in the Help directory.</span></body>")
                '            WBHelp.Navigate(My.Application.Info.DirectoryPath & HELP_PATH_DD)
                '        Else
                '            WBHelp.Navigate(HELP_PATH_INTERNET)
                '        End If
                '    End If
                '    _tab.SelectedTab = Me.pageHelp
        End Select
        Me.Ribbon.ActiveTab = theTab
    End Sub

    Private Sub goSearch(ByVal ssearch As String)
        If ssearch IsNot Nothing AndAlso ssearch.Length > 0 Then
            With Me.lvSearchResults
                .CaseSensitive = Me.chkSearchCase.Checked
                .SearchString = ssearch
                Dim t As Native.Api.Enums.GeneralObjectType
                If Me.chkSearchEnvVar.Checked And Me.chkSearchEnvVar.Enabled Then
                    t = t Or Native.Api.Enums.GeneralObjectType.EnvironmentVariable
                End If
                If Me.chkSearchHandles.Checked Then
                    t = t Or Native.Api.Enums.GeneralObjectType.Handle
                End If
                If Me.chkSearchModules.Checked And Me.chkSearchModules.Enabled Then
                    t = t Or Native.Api.Enums.GeneralObjectType.Module
                End If
                If Me.chkSearchProcess.Checked Then
                    t = t Or Native.Api.Enums.GeneralObjectType.Process
                End If
                If Me.chkSearchServices.Checked Then
                    t = t Or Native.Api.Enums.GeneralObjectType.Service
                End If
                If Me.chkSearchWindows.Checked Then
                    t = t Or Native.Api.Enums.GeneralObjectType.Window
                End If
                .Includes = t
                Me.butSearchGo.Enabled = False
                Me.MenuItemSearchNew.Enabled = False
                Me.butSearchSaveReport.Enabled = False
                .UpdateItems()
            End With
        End If
    End Sub

    Private Sub timerNetwork_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerNetwork.Tick
        Call refreshNetworkList()
    End Sub

    Private Sub timerStateBasedActions_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerStateBasedActions.Tick
        'TODO_ (sba)
        'Me.emStateBasedActions.ProcessActions(lvProcess.GetAllItems)
    End Sub

    Private Sub butNewProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNewProcess.Click
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            cFile.ShowRunBox(Me.Handle, "Start a new process", "Enter the path of the process you want to start.")
        Else
            Dim sres As String = CInputBox("Enter the path of the process you want to start.", "Start a new process", "")
            If sres Is Nothing OrElse sres.Equals(String.Empty) Then Exit Sub
            cProcess.SharedRLStartNewProcess(sres)
        End If
    End Sub

    Private Sub butLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butLog.Click
        Call Me.MenuItemMainLog_Click(Nothing, Nothing)
    End Sub

    Private Sub butWindows_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butWindows.Click
        Call Me.MenuItemSystemOpenedWindows_Click(Nothing, Nothing)
    End Sub

    Private Sub butSystemInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSystemInfo.Click
        Call Me.MenuItemMainSysInfo_Click(Nothing, Nothing)
    End Sub

    Private Sub butFindWindow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFindWindow.Click
        Call Me.MenuItemMainFindWindow_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuAbout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuAbout.Click
        Call Me.MenuItemMainAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuEmergency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuEmergency.Click
        Call Me.MenuItemMainEmergencyH_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuSaveReport.Click
        Call Me.MenuItemMainReport_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuSBA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuSBA.Click
        Call Me.MenuItemMainSBA_Click(Nothing, Nothing)
    End Sub

    Private Sub butNetwork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNetwork.Click
        Call orbMenuNetwork_Click(Nothing, Nothing)
    End Sub

    Private Sub orbMenuNetwork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbMenuNetwork.Click
        If ConnectionForm.Visible Then
            ConnectionForm.Hide()
        Else
            ConnectionForm.TopMost = _frmMain.TopMost
            ConnectionForm.Show()
        End If
    End Sub

    Public Sub ConnectToMachine()

        _local = (Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection)
        _notWMI = (Program.Connection.Type <> cConnection.TypeOfConnection.RemoteConnectionViaWMI)
        _notSnapshotMode = (Program.Connection.Type <> cConnection.TypeOfConnection.SnapshotFile)

        ' Disable all refreshments
        Me.timerProcess.Enabled = False
        Me.timerServices.Enabled = False
        Me.timerMonitoring.Enabled = False
        Me.timerTask.Enabled = False
        Me.timerNetwork.Enabled = False
        Me.timerJobs.Enabled = False

        ' Clear all lvItems
        Me.lvProcess.ClearItems()
        Me.lvSearchResults.ClearItems()
        Me.tv.ClearItems()
        Me.tv2.ClearItems()
        Me.lvTask.ClearItems()
        Me.lvServices.ClearItems()
        Me.lvNetwork.ClearItems()
        Me.lvJob.ClearItems()

        ' Connect all lvItems
        Me.tv.ConnectionObj = Program.Connection
        Me.tv2.ConnectionObj = Program.Connection
        Me.lvSearchResults.ConnectionObj = Program.Connection
        _shutdownConnection.ConnectionObj = Program.Connection
        Try
            Program.Connection.Connect()
            _shutdownConnection.Connect()
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to connect")
            Exit Sub
        End Try

        Me.butServiceFileDetails.Enabled = _local
        Me.butServiceFileProp.Enabled = _local
        Me.butServiceOpenDir.Enabled = _local
        Me.butDeleteService.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.butResumeProcess.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.butStopProcess.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.butProcessAffinity.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.butProcessAffinity.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.butStopProcess.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.butResumeProcess.Enabled = Me._notWMI AndAlso _notSnapshotMode
        Me.pageJobs.Enabled = (_local And (cEnvironment.HasLitePMDebugPrivilege)) OrElse Not (_local)
        Me.RBJobActions.Enabled = Me.pageJobs.Enabled AndAlso _notSnapshotMode
        Me.RBJobDisplay.Enabled = Me.pageJobs.Enabled
        Me.RBJobPrivileges.Enabled = _local AndAlso Not (cEnvironment.HasLitePMDebugPrivilege)
        Me.pageNetwork.Enabled = _notWMI
        Me.pageTasks.Enabled = _notWMI
        Me.pageSearch.Enabled = _notWMI
        Me.RBNetworkRefresh.Enabled = _notWMI
        Me.RBSearchMain.Enabled = _notWMI
        Me.RBTaskActions.Enabled = _notWMI AndAlso _notSnapshotMode
        Me.RBTaskDisplay.Enabled = _notWMI
        Me.RBServiceFile.Enabled = _notWMI
        Me.butDeleteService.Enabled = _notWMI
        Me.butProcessOtherActions.Enabled = _notWMI AndAlso _notSnapshotMode
        Me.RBProcessActions.Enabled = _notSnapshotMode
        Me.RBProcessPriority.Enabled = _notSnapshotMode
        Me.RBServiceAction.Enabled = _notSnapshotMode
        Me.RBServiceStartType.Enabled = _notSnapshotMode
        Me.RBServiceFile.Enabled = _notSnapshotMode
        Me.butCheckSignatures.Enabled = _local

        Me.lvProcess.CatchErrors = Not (_local)
        Me.lvServices.CatchErrors = Not (_local)
        Me.lvSearchResults.CatchErrors = Not (_local)
        Me.lvTask.CatchErrors = Not (_local)
        Me.lvNetwork.CatchErrors = Not (_local)
        Me.lvJob.CatchErrors = Not (_local)

        ' Set new refreshment intervals
        Me.timerProcess.Interval = CInt(My.Settings.ProcessInterval * Program.Connection.RefreshmentCoefficient)
        Me.timerServices.Interval = CInt(My.Settings.ServiceInterval * Program.Connection.RefreshmentCoefficient)
        Me.timerNetwork.Interval = CInt(My.Settings.NetworkInterval * Program.Connection.RefreshmentCoefficient)
        Me.timerTask.Interval = CInt(My.Settings.TaskInterval * Program.Connection.RefreshmentCoefficient)
        Me.timerTrayIcon.Interval = CInt(My.Settings.TrayInterval * Program.Connection.RefreshmentCoefficient)
        Me.timerJobs.Interval = CInt(My.Settings.JobInterval * Program.Connection.RefreshmentCoefficient)

        ' Enable all refreshments
        Me.timerProcess.Enabled = True ' _local
        Me.timerServices.Enabled = True ' _local
        Me.timerMonitoring.Enabled = True ' _local
        Me.timerNetwork.Enabled = True ' _local
        Me.timerTask.Enabled = True ' _local
        Me.timerJobs.Enabled = True ' _local
    End Sub

    Public Sub DisconnectFromMachine()
        ' Close all frmInfo forms

        ' Disable all refreshments
        Me.timerProcess.Enabled = False
        Me.timerServices.Enabled = False
        Me.timerMonitoring.Enabled = False
        Me.timerTask.Enabled = False
        Me.timerNetwork.Enabled = False
        Me.timerJobs.Enabled = False

        ' Clear all lvItems
        Me.lvProcess.ClearItems()
        Me.lvSearchResults.ClearItems()
        Me.lvTask.ClearItems()
        Me.lvServices.ClearItems()
        Me.lvNetwork.ClearItems()
        Me.lvJob.ClearItems()

        For x As Integer = Application.OpenForms.Count - 1 To 0 Step -1
            Dim frm As Form = Application.OpenForms(x)
            If TypeOf frm Is frmProcessInfo OrElse TypeOf frm Is frmServiceInfo OrElse TypeOf frm Is frmJobInfo Then
                Try
                    frm.Close()
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
            End If
        Next
        Try
            Program.Connection.Disconnect()
        Catch ex As Exception
            Misc.ShowError(ex, "Unable to disconnect")
            Program.Connection.DisconnectForce()
            Exit Sub
        End Try
    End Sub


    Private Sub butHiddenProcesses_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butHiddenProcesses.Click
        frmHiddenProcesses.TopMost = _frmMain.TopMost
        frmHiddenProcesses.Show()
    End Sub

    Private Sub butServiceDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butServiceDetails.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            Dim frm As New frmServiceInfo
            frm.SetService(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub butShowPreferences_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowPreferences.Click
        Dim frm As New frmPreferences
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

    Private Sub butExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butExit.Click
        Call Me.MenuItemMainExit_Click(Nothing, Nothing)
    End Sub

    Private Sub rtb3_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtb3.DragDrop
        Dim strFile As String = Nothing
        For Each file As String In CType(e.Data.GetData(DataFormats.FileDrop), String())
            If IO.File.Exists(file) Then
                strFile = file
            End If
        Next
        If strFile IsNot Nothing Then
            Call DisplayDetailsFile(strFile)
        End If
    End Sub

    Private Sub rtb3_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles rtb3.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub rtb3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtb3.TextChanged
        Me.cmdFileClipboard.Enabled = (rtb3.Rtf.Length > 0)
    End Sub

    Private Sub butShowDepViewer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowDepViewer.Click
        Dim _depFrm As New frmDepViewerMain
        _depFrm.TopMost = _frmMain.TopMost
        _depFrm.Show()
    End Sub

    Private Sub MenuItemTaskShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskShow.Click
        Call butTaskShow_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemTaskMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskMax.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.Maximize()
        Next
    End Sub

    Private Sub MenuItemTaskMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskMin.Click
        For Each it As cTask In Me.lvTask.GetSelectedItems
            it.Minimize()
        Next
    End Sub

    Private Sub MenuItemTaskEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskEnd.Click
        Call butTaskEndTask_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemTaskSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskSelProc.Click
        ' Select processes associated to selected windows
        If Me.lvTask.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it As cTask In Me.lvTask.GetSelectedItems
            Dim pid As Integer = it.Infos.ProcessId
            Dim it2 As ListViewItem
            For Each it2 In Me.lvProcess.Items
                Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                If cp IsNot Nothing AndAlso cp.Infos.ProcessId = pid Then
                    it2.Selected = True
                    it2.EnsureVisible()
                End If
            Next
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
        Me.lvProcess.Focus()
    End Sub

    Private Sub MenuItemTaskColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskColumns.Click
        Me.lvTask.ChooseColumns()
    End Sub

    Private Sub MenuItemMonitorAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorAdd.Click
        Call butMonitoringAdd_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMonitorRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorRemove.Click
        Call butMonitoringRemove_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMonitorStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorStart.Click
        Call butMonitorStart_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMonitorStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMonitorStop.Click
        Call butMonitorStop_Click(Nothing, Nothing)
    End Sub

    Private Sub lvTask_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvTask.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvTask.SelectedItems IsNot Nothing _
                                                    AndAlso Me.lvTask.SelectedItems.Count > 0)
            Me.MenuItemTaskEnd.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemTaskSelProc.Enabled = selectionIsNotNothing AndAlso Me.lvProcess.Items.Count > 0
            Me.MenuItemTaskShow.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemTaskMax.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemTaskMin.Enabled = selectionIsNotNothing AndAlso _notSnapshotMode
            Me.MenuItemTaskSelectWindow.Enabled = selectionIsNotNothing

            Me.MenuItemCopyTask.Enabled = selectionIsNotNothing
            Me.mnuTask.Show(Me.lvTask, e.Location)
        End If
    End Sub

    Private Sub tvMonitor_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tvMonitor.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuMonitor.Show(Me.tvMonitor, e.Location)
        End If
    End Sub

    Private Sub MenuItemCopySmall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopySmall.Click
        My.Computer.Clipboard.SetImage(Me.pctFileSmall.Image)
    End Sub

    Private Sub MenuItemCopyBig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCopyBig.Click
        My.Computer.Clipboard.SetImage(Me.pctFileBig.Image)
    End Sub

    Private Sub pctFileBig_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctFileBig.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuFileCpPctBig.Show(Me.pctFileBig, e.Location)
        End If
    End Sub

    Private Sub pctFileSmall_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pctFileSmall.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Me.mnuFileCpPctSmall.Show(Me.pctFileSmall, e.Location)
        End If
    End Sub

    Private Sub MenuItemMainShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainShow.Click
        ' If ribbon is used, then the main form is to the left of the screen and
        ' not shown in taskbar
        If My.Settings.UseRibbonStyle Then
            If Me.Left = Pref.LEFT_POSITION_HIDDEN Then
                Me.CenterToScreen()
            End If
            Me.ShowInTaskbar = True
        End If
        Me.Visible = True
        Me.WindowState = FormWindowState.Normal
        Me.Show()
    End Sub

    Private Sub MenuItemMainToTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainToTray.Click
        Me.Hide()
        Me.Visible = False
    End Sub

    Private Sub MenuItemMainAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainAbout.Click
        Me.butAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMainAlwaysVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainAlwaysVisible.Click
        Call changeTopMost()
    End Sub

    Private Sub MenuItemMainRestart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainRestart.Click
        Call cSystem.Restart()
    End Sub

    Private Sub MenuItemMainShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainShutdown.Click
        Call cSystem.Shutdown()
    End Sub

    Private Sub MenuItemMainPowerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainPowerOff.Click
        Call cSystem.Poweroff()
    End Sub

    Private Sub MenuItemMainSleep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainSleep.Click
        Call cSystem.Sleep()
    End Sub

    Private Sub MenuItemMainHibernate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainHibernate.Click
        Call cSystem.Hibernate()
    End Sub

    Private Sub MenuItemMainLogOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainLogOff.Click
        Call cSystem.Logoff()
    End Sub

    Private Sub MenuItemMainLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainLock.Click
        Call cSystem.Lock()
    End Sub

    Private Sub MenuItemMainLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainLog.Click
        Program.Log.ShowForm = True
    End Sub

    Private Sub MenuItemMainReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainReport.Click
        frmGlobalReport.TopMost = _frmMain.TopMost
        frmGlobalReport.ShowDialog()
    End Sub

    Private Sub MenuItemMainSysInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainSysInfo.Click
        Program._frmSystemInfo.Show()
    End Sub

    Private Sub MenuItemMainOpenedW_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainOpenedW.Click
        frmWindowsList.BringToFront()
        frmWindowsList.WindowState = FormWindowState.Normal
        frmWindowsList.TopMost = _frmMain.TopMost
        frmWindowsList.Show()
    End Sub

    Private Sub MenuItemMainEmergencyH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainEmergencyH.Click
        frmHotkeys.BringToFront()
        frmHotkeys.WindowState = FormWindowState.Normal
        frmHotkeys.TopMost = _frmMain.TopMost
        frmHotkeys.Show()
    End Sub

    Private Sub MenuItemMainFindWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainFindWindow.Click
        frmFindWindow.TopMost = _frmMain.TopMost
        frmFindWindow.Show()
    End Sub

    Private Sub MenuItemMainSBA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainSBA.Click
        frmBasedStateAction.BringToFront()
        frmBasedStateAction.WindowState = FormWindowState.Normal
        frmBasedStateAction.TopMost = _frmMain.TopMost
        frmBasedStateAction.Show()
    End Sub

    Private Sub MenuItemRefProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRefProc.Click
        Me.MenuItemRefProc.Checked = Not (Me.MenuItemRefProc.Checked)
        Me.MenuItemSystemRefProc.Checked = Me.MenuItemRefProc.Checked
        Me.timerProcess.Enabled = Me.MenuItemRefProc.Checked
    End Sub

    Private Sub MenuItemMainRefServ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainRefServ.Click
        Me.MenuItemMainRefServ.Checked = Not (Me.MenuItemMainRefServ.Checked)
        Me.MenuItemSystemRefServ.Checked = Me.MenuItemMainRefServ.Checked
        Me.timerServices.Enabled = Me.MenuItemMainRefServ.Checked
    End Sub

    Private Sub MenuItemMainExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainExit.Click
        Call ExitLitePM()
    End Sub

    Private Sub MenuItemServSelService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSelService.Click
        ' Select processes associated to selected services results
        Dim it As ListViewItem
        Dim bOne As Boolean = False
        If Me.lvServices.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvServices.SelectedItems
            Dim tmp As cService = Me.lvServices.GetItemByKey(it.Name)
            If tmp IsNot Nothing Then
                Dim pid As Integer = tmp.Infos.ProcessId
                Dim it2 As ListViewItem
                For Each it2 In Me.lvProcess.Items
                    Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                    If cp IsNot Nothing AndAlso cp.Infos.ProcessId = pid And pid > 0 Then
                        it2.Selected = True
                        bOne = True
                        it2.EnsureVisible()
                    End If
                Next
            End If
        Next
        If bOne Then
            Me.Ribbon.ActiveTab = Me.ProcessTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemServFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServFileProp.Click
        Dim s As String = vbNullString
        For Each it As cService In Me.lvServices.GetSelectedItems
            Dim sP As String = it.GetInformation("ImagePath")
            If sP <> NO_INFO_RETRIEVED Then
                'TODO_
                s = sP  'cService.GetFileNameFromSpecial(sP)
                If IO.File.Exists(s) Then
                    cFile.ShowFileProperty(s, Me.Handle)
                Else
                    ' Cannot retrieve a good path
                    Dim box As New frmBox
                    With box
                        .txtMsg1.Text = "The file path cannot be extracted. Please edit it and then click 'OK' to open file properties box, or click 'Cancel' to cancel."
                        .txtMsg1.Height = 35
                        .txtMsg2.Top = 50
                        .txtMsg2.Height = 25
                        .txtMsg2.Text = s
                        .txtMsg2.ReadOnly = False
                        .txtMsg2.BackColor = Drawing.Color.White
                        .Text = "Show file properties box"
                        .Height = 150
                        .ShowDialog()
                        .TopMost = _frmMain.TopMost
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            If IO.File.Exists(.MsgResult2) Then _
                                cFile.ShowFileProperty(.MsgResult2, Me.Handle)
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub MenuItemServOpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServOpenDir.Click
        Dim s As String = vbNullString
        For Each it As cService In Me.lvServices.GetSelectedItems
            Dim sP As String = it.GetInformation("ImagePath")
            If sP <> NO_INFO_RETRIEVED Then
                s = cFile.GetParentDir(sP)
                If IO.Directory.Exists(s) Then
                    cFile.OpenADirectory(s)
                Else
                    ' Cannot retrieve a good path
                    Dim box As New frmBox
                    With box
                        .txtMsg1.Text = "The file directory cannot be extracted. Please edit it and then click 'OK' to open directory, or click 'Cancel' to cancel."
                        .txtMsg1.Height = 35
                        .txtMsg2.Top = 50
                        .txtMsg2.Height = 25
                        .txtMsg2.Text = s
                        .txtMsg2.ReadOnly = False
                        .txtMsg2.BackColor = Drawing.Color.White
                        .Text = "Open directory"
                        .Height = 150
                        .ShowDialog()
                        .TopMost = _frmMain.TopMost
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            If IO.Directory.Exists(.MsgResult2) Then
                                cFile.OpenADirectory(.MsgResult2)
                            End If
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Private Sub MenuItemServFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServFileDetails.Click
        Call Me.butServiceFileDetails_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSearch.Click
        Call Me.butServiceGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServDepe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDepe.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            If System.IO.File.Exists(it.GetInformation("ImagePath")) Then
                Dim frm As New frmDepViewerMain
                frm.HideOpenMenu()
                frm.OpenReferences(it.GetInformation("ImagePath"))
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemServPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServPause.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            If it.Infos.State = Native.Api.NativeEnums.ServiceState.Running Then
                it.PauseService()
            Else
                it.ResumeService()
            End If
        Next

        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStop.Click
        If WarnDangerousAction("Are you sure you want to stop these services ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StopService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.StartService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServAutoStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServAutoStart.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.AutoStart)
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServOnDemand_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServOnDemand.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.DemandStart)
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServDisabled_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDisabled.Click
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.SetServiceStartType(Native.Api.NativeEnums.ServiceStartType.StartDisabled)
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServReanalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServReanalize.Click
        Me.lvServices.ReAnalizeServices()
    End Sub

    Private Sub MenuItemServColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServColumns.Click
        Me.lvServices.ChooseColumns()
    End Sub

    Private Sub MenuItemServSelProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServSelProc.Click
        ' Select processes associated to selected connections
        Dim it As ListViewItem
        If Me.lvNetwork.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it In Me.lvNetwork.SelectedItems
            Dim tmp As cNetwork = lvNetwork.GetItemByKey(it.Name)
            If tmp IsNot Nothing Then
                Dim pid As Integer = tmp.Infos.ProcessId
                Dim it2 As ListViewItem
                For Each it2 In Me.lvProcess.Items
                    Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                    If cp IsNot Nothing AndAlso cp.Infos.ProcessId = pid Then
                        it2.Selected = True
                        it2.EnsureVisible()
                    End If
                Next
            End If
        Next
        Me.Ribbon.ActiveTab = Me.ProcessTab
        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub menuCloseTCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkClose.Click
        If WarnDangerousAction("Are you sure you want to close these connections ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            If it.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                it.CloseTCP()
            End If
        Next
    End Sub

    Private Sub MenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkColumns.Click
        Me.lvNetwork.ChooseColumns()
    End Sub

    Private Sub lvNetwork_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvNetwork.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then

            Dim selectionIsNotNothing As Boolean = (Me.lvNetwork.SelectedItems IsNot Nothing _
                AndAlso Me.lvNetwork.SelectedItems.Count > 0)

            Dim enable As Boolean = False
            For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
                If it.Infos.Protocol = Native.Api.Enums.NetworkProtocol.Tcp Then
                    If it.Infos.State <> Native.Api.Enums.MibTcpState.Listening AndAlso it.Infos.State <> Native.Api.Enums.MibTcpState.TimeWait AndAlso it.Infos.State <> Native.Api.Enums.MibTcpState.CloseWait Then
                        enable = True
                        Exit For
                    End If
                End If
            Next
            Me.MenuItemNetworkClose.Enabled = enable AndAlso _notSnapshotMode AndAlso _notWMI
            Me.MenuItemServSelProc.Enabled = selectionIsNotNothing
            Me.MenuItemCopyNetwork.Enabled = selectionIsNotNothing

            Dim bTools As Boolean = True
            If Me.lvNetwork.SelectedItems.Count = 1 Then
                bTools = (Me.lvNetwork.GetSelectedItem.Infos.Remote IsNot Nothing)
            End If
            Me.MenuItemNetworkTools.Enabled = selectionIsNotNothing AndAlso bTools

            Me.mnuNetwork.Show(Me.lvNetwork, e.Location)
        End If
    End Sub

    Private Sub MenuItemSearchNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSearchNew.Click
        Dim r As String = CInputBox("Enter the string you want to search", "String search")
        If r IsNot Nothing AndAlso Not (r.Equals(String.Empty)) Then
            Call goSearch(r)
        End If
    End Sub

    Private Sub MenuItemSearchSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSearchSel.Click
        ' Select processes associated to selected search results
        If Me.lvSearchResults.SelectedItems.Count > 0 Then Me.lvProcess.SelectedItems.Clear()
        For Each it As cSearchItem In Me.lvSearchResults.GetSelectedItems
            Try
                If it.Infos.Type = Native.Api.Enums.GeneralObjectType.Service Then
                    ' Select service
                    Dim sp As String = it.Infos.Owner
                    Dim it2 As ListViewItem
                    For Each it2 In Me.lvServices.Items
                        Dim cp As cService = Me.lvServices.GetItemByKey(it2.Name)
                        If cp IsNot Nothing AndAlso cp.Infos.Name = sp Then
                            it2.Selected = True
                            it2.EnsureVisible()
                        End If
                    Next
                    Me.Ribbon.ActiveTab = Me.ServiceTab
                Else
                    ' Select process
                    Dim i As Integer = it.Infos.OwnedProcessId
                    If i > 0 Then
                        Dim it2 As ListViewItem
                        For Each it2 In Me.lvProcess.Items
                            Dim cp As cProcess = Me.lvProcess.GetItemByKey(it2.Name)
                            If cp IsNot Nothing AndAlso cp.Infos.ProcessId = i Then
                                it2.Selected = True
                                it2.EnsureVisible()
                            End If
                        Next
                    End If
                    Me.Ribbon.ActiveTab = Me.ProcessTab
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        Next

        Call Me.Ribbon_MouseMove(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSearchClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSearchClose.Click
        ' Close selected items
        If WarnDangerousAction("This will close handles, unload module, stop service, kill process or close window depending on the selected object.", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cSearchItem In Me.lvSearchResults.GetSelectedItems
            it.CloseTerminate()
        Next
    End Sub

    Private Sub lvSearchResults_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSearchResults.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvSearchResults.SelectedItems IsNot Nothing _
                    AndAlso Me.lvSearchResults.SelectedItems.Count > 0)

            Me.MenuItemSearchClose.Enabled = False ' selectionIsNotNothing
            Me.MenuItemSearchSel.Enabled = selectionIsNotNothing
            Me.MenuItemCopySearch.Enabled = selectionIsNotNothing

            Me.mnuSearch.Show(Me.lvSearchResults, e.Location)
        End If
    End Sub

    Private Sub MenuItemProcKill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKill.Click
        If WarnDangerousAction("Are you sure you want to kill these processes ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.Kill()
        Next
    End Sub

    Private Sub MenuItemProcKillT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKillT.Click
        If WarnDangerousAction("Are you sure you want to kill these processes ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            it.KillProcessTree()
        Next
    End Sub

    Private Sub MenuItemProcStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcStop.Click
        If WarnDangerousAction("Are you sure you want to suspend these processes ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SuspendProcess()
        Next
    End Sub

    Private Sub MenuItemProcResume_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcResume.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.ResumeProcess()
        Next
    End Sub

    Private Sub MenuItemProcPIdle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPIdle.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.Idle)
        Next
    End Sub

    Private Sub MenuItemProcPBN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPBN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub MenuItemProcPN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.Normal)
        Next
    End Sub

    Private Sub MenuItemProcPAN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPAN.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.AboveNormal)
        Next
    End Sub

    Private Sub MenuItemProcPH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPH.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.High)
        Next
    End Sub

    Private Sub MenuItemProcPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcPRT.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            cp.SetPriority(ProcessPriorityClass.RealTime)
        Next
    End Sub

    Private Sub MenuItemProcWorkingSS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcWSS.Click
        For Each _p As cProcess In Me.lvProcess.GetSelectedItems
            _p.EmptyWorkingSetSize()
        Next
    End Sub

    Private Sub MenuItemProcDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcDump.Click
        Dim _frm As New frmDumpFile
        _frm.TopMost = _frmMain.TopMost
        If _frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            For Each cp As cProcess In Me.lvProcess.GetSelectedItems
                Dim _file As String = _frm.TargetDir & "\" & Date.Now.Ticks.ToString & "_" & cp.Infos.Name & ".dmp"
                Call cp.CreateDumpFile(_file, _frm.DumpOption)
            Next
        End If
    End Sub

    Private Sub MenuItemProcReanalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcReanalize.Click
        Me.lvProcess.ReAnalizeProcesses()
    End Sub

    Private Sub MenuItemProcSServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' Refresh service list if necessary
        If Me.lvServices.Items.Count = 0 Then
            Call Me.refreshServiceList()
        End If

        ' Get selected processes pids
        Dim pid() As Integer
        ReDim pid(0)
        Dim x As Integer = -1
        For Each lvi As cProcess In Me.lvProcess.GetSelectedItems
            x += 1
            ReDim Preserve pid(x)
            pid(x) = lvi.Infos.ProcessId
        Next

        ' Get services names of all associated services
        Dim bAddedOneService As Boolean = False
        Dim bServRef As Boolean = Me.timerServices.Enabled
        Me.timerServices.Enabled = False            ' Lock service timer

        For Each lvi As ListViewItem In Me.lvServices.Items
            Dim cServ As cService = Me.lvServices.GetItemByKey(lvi.Name)
            Dim bToAdd As Boolean = False
            If cServ IsNot Nothing Then
                For Each _pid As Integer In pid
                    If cServ.Infos.ProcessId = _pid And _pid > 0 Then
                        bToAdd = True
                        Exit For
                    End If
                Next
            End If

            ' Then we select service
            If bToAdd Then
                If bAddedOneService = False Then
                    Me.lvServices.SelectedItems.Clear()
                    bAddedOneService = True
                End If
                lvi.Selected = True
                lvi.EnsureVisible()
            End If
        Next

        ' Unlock timer
        Me.timerServices.Enabled = bServRef

        If bAddedOneService Then
            Me.Ribbon.ActiveTab = Me.ServiceTab
            Call Me.Ribbon_MouseMove(Nothing, Nothing)
        End If
    End Sub

    Private Sub MenuItemProcSFileProp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSFileProp.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If IO.File.Exists(cp.Infos.Path) Then
                cFile.ShowFileProperty(cp.Infos.Path, Me.Handle)
            End If
        Next
    End Sub

    Private Sub MenuItemProcSOpenDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSOpenDir.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            If cp.Infos.Path <> NO_INFO_RETRIEVED Then
                cFile.OpenDirectory(cp.Infos.Path)
            End If
        Next
    End Sub

    Private Sub MenuItemProcSFileDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSFileDetails.Click
        If Me.lvProcess.SelectedItems.Count > 0 Then
            Dim cp As cProcess = Me.lvProcess.GetSelectedItem
            Dim s As String = cp.Infos.Path
            If IO.File.Exists(s) Then
                DisplayDetailsFile(s)
            End If
        End If
    End Sub

    Private Sub MenuItemProcSSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSSearch.Click
        Call Me.butProcessGoogle_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcSDep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcSDep.Click
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            If System.IO.File.Exists(it.Infos.Path) Then
                Dim frm As New frmDepViewerMain
                frm.HideOpenMenu()
                frm.OpenReferences(it.Infos.Path)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemProcColumns_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcColumns.Click
        Me.lvProcess.ChooseColumns()
    End Sub

    Private Sub MenuItemSystemRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRefresh.Click
        Select Case _tab.TabPages(_tab.SelectedIndex).Text
            Case "Tasks"
                Call Me.butTaskRefresh_Click(Nothing, Nothing)
            Case "Processes"
                Call Me.butProcessRerfresh_Click(Nothing, Nothing)
            Case "Services"
                Call Me.butServiceRefresh_Click(Nothing, Nothing)
            Case "Network"
                Call Me.butNetworkRefresh_Click(Nothing, Nothing)
            Case "File"
                Call Me.butFileRefresh_Click(Nothing, Nothing)
            Case "Jobs"
                Call Me.butJobRefresh_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub MenuItemSystemConnection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemConnection.Click
        Call orbMenuNetwork_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemLog.Click
        Call Me.MenuItemMainLog_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemReport.Click
        Call Me.MenuItemMainReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemInfos.Click
        Call Me.MenuItemMainSysInfo_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemOpenedWindows_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemOpenedWindows.Click
        Call Me.MenuItemMainOpenedW_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemFindWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemFindWindow.Click
        Call Me.MenuItemMainFindWindow_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemEmergency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemEmergency.Click
        Call Me.MenuItemMainEmergencyH_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemSBA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemSBA.Click
        Call Me.MenuItemMainSBA_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemToTray_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemToTray.Click
        Call Me.MenuItemMainToTray_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemExit.Click
        Call Me.MenuItemMainExit_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemAlwaysVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemAlwaysVisible.Click
        Me.MenuItemMainAlwaysVisible_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemRefProc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRefProc.Click
        Me.MenuItemRefProc_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemRefServ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRefServ.Click
        Call Me.MenuItemMainRefServ_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemOptions.Click
        Call Me.butPreferences_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemShowHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemShowHidden.Click
        Call butHiddenProcesses_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemDependency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemDependency.Click
        Call butShowDepViewer_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemRestart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemRestart.Click
        Call cSystem.Restart()
    End Sub

    Private Sub MenuItemSystemShutdown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemShutdown.Click
        Call cSystem.Shutdown()
    End Sub

    Private Sub MenuItemSystemPowerOff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemPowerOff.Click
        Call cSystem.Poweroff()
    End Sub

    Private Sub MenuItemSystemSleep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemSleep.Click
        Call cSystem.Sleep()
    End Sub

    Private Sub MenuItemSystemHIbernate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemHIbernate.Click
        Call cSystem.Hibernate()
    End Sub

    Private Sub MenuItemSystemLogoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemLogoff.Click
        Call cSystem.Logoff()
    End Sub

    Private Sub MenuItemSystemLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemLock.Click
        Call cSystem.Lock()
    End Sub

    Private Sub MenuItemSystemUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemUpdate.Click
        Call Me.butUpdate_Click(Nothing, Nothing)
    End Sub


    Private Sub MenuItemSystemDownloads_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemDownloads.Click
        Call Me.butDownload_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemSystemAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemAbout.Click
        Call Me.butAbout_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemProcAff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcAff.Click
        If Me.lvProcess.SelectedItems.Count = 0 Then Exit Sub

        Dim c() As cProcess
        ReDim c(Me.lvProcess.SelectedItems.Count - 1)
        Dim x As Integer = 0
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            c(x) = it
            x += 1
        Next

        Dim frm As New frmProcessAffinity(c)
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub

#Region "Copy to clipboard menus"

    Private Sub MenuItemCopyService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cService In Me.lvServices.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cJob In Me.lvJob.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cProcess In Me.lvProcess.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyNetwork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopySearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cSearchItem In Me.lvSearchResults.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

    Private Sub MenuItemCopyTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim info As String = CType(sender, System.Windows.Forms.MenuItem).Text
        Dim toCopy As String = ""
        For Each it As cTask In Me.lvTask.GetSelectedItems
            toCopy &= it.GetInformation(info) & vbNewLine
        Next
        If toCopy.Length > 2 Then
            ' Remove last vbNewline
            toCopy = toCopy.Substring(0, toCopy.Length - 2)
        End If
        My.Computer.Clipboard.SetText(toCopy)
    End Sub

#End Region

    Private Sub orbStartElevated_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles orbStartElevated.Click
        ' Restart elevated
        Call cEnvironment.RestartElevated()
    End Sub

    Private Sub MenuItemRunAsAdmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemRunAsAdmin.Click
        ' Restart elevated
        Call cEnvironment.RestartElevated()
    End Sub

    Private Sub timerStatus_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerStatus.Tick
        ' Update panels of status bar
        Try
            ' /!\ Here we refresh the informations about system
            ' RefreshInfo should not be called elsewhere
            Call Program.SystemInfo.RefreshInfo()

            Me.sbPanelConnection.Text = Program.Connection.ToString
            Me.sbPanelCpu.Text = "CPU : " & Common.Misc.GetFormatedPercentage(Program.SystemInfo.CpuUsage, 3, True) & " %"
            Me.sbPanelMemory.Text = "Phys. Memory : " & Common.Misc.GetFormatedPercentage(Program.SystemInfo.PhysicalMemoryPercentageUsage, 3, True) & " %"
            Me.sbPanelProcesses.Text = Me.lvProcess.Items.Count & " processes"
            Me.sbPanelServices.Text = Me.lvServices.Items.Count & " services"

            ' We disable some buttons on the main form
            Me.butSaveSystemSnaphotFile.Enabled = Program.Connection.Type <> cConnection.TypeOfConnection.SnapshotFile
            Me.MenuItemSystemSaveSSFile.Enabled = Program.Connection.Type <> cConnection.TypeOfConnection.SnapshotFile

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub

    Private Sub butShowAllPendingTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butShowAllPendingTasks.Click
        Dim frm As New frmPendingTasks
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub changeTopMost()
        Me.butAlwaysDisplay.Checked = Not (Me.butAlwaysDisplay.Checked)
        Me.MenuItemMainAlwaysVisible.Checked = Me.butAlwaysDisplay.Checked
        Me.TopMost = Me.butAlwaysDisplay.Checked

        For Each frm As Form In Application.OpenForms
            frm.TopMost = Me.TopMost
        Next
    End Sub

    Private Sub MenuItemShowPendingTasks_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemShowPendingTasks.Click
        Call butShowAllPendingTasks_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportProcesses_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportProcesses.Click
        Call Me.butSaveProcessReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportMonitor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportMonitor.Click
        Call Me.butMonitorSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportServices.Click
        Call Me.butServiceReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemNewSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNewSearch.Click
        Call Me.MenuItemSearchNew_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemReportSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemReportSearch.Click
        Call Me.butSearchSaveReport_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileOpen.Click
        Call Me.butOpenFile_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileRelease.Click
        Call Me.butFileRelease_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileDelete.Click
        Call Me.butDeleteFile_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileTrash_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileTrash.Click
        Call Me.butMoveFileToTrash_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileRename.Click
        Call Me.butFileRename_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileShellOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileShellOpen.Click
        Call Me.butFileOpen_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileMove.Click
        Call Me.butFileMove_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileCopy.Click
        Call Me.butFileCopy_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileEncrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileEncrypt.Click
        Call Me.butFileEncrypt_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileDecrypt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileDecrypt.Click
        Call Me.butFileDecrypt_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemFileStrings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemFileStrings.Click
        Call Me.butFileSeeStrings_Click(Nothing, Nothing)
    End Sub

    Private Sub butMonitorSaveReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butMonitorSaveReport.Click
        Call Me.saveMonitorReport()
    End Sub

    Private Sub butProcessReduceWS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessReduceWS.Click
        Call Me.MenuItemProcWorkingSS_Click(Nothing, Nothing)
    End Sub

    Private Sub butProcessDumpF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butProcessDumpF.Click
        Call Me.MenuItemProcDump_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemNotifAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifAll.Click
        Me.MenuItemNotifDS.Checked = True
        Me.MenuItemNotifNS.Checked = True
        Me.MenuItemNotifNP.Checked = True
        Me.MenuItemNotifTP.Checked = True
        My.Settings.NotifyNewProcesses = True
        My.Settings.NotifyNewServices = True
        My.Settings.NotifyDeletedServices = True
        My.Settings.NotifyTerminatedProcesses = True
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifNone.Click
        Me.MenuItemNotifDS.Checked = False
        Me.MenuItemNotifNS.Checked = False
        Me.MenuItemNotifNP.Checked = False
        Me.MenuItemNotifTP.Checked = False
        My.Settings.NotifyNewProcesses = False
        My.Settings.NotifyNewServices = False
        My.Settings.NotifyDeletedServices = False
        My.Settings.NotifyTerminatedProcesses = False
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifDS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifDS.Click
        Me.MenuItemNotifDS.Checked = Not (Me.MenuItemNotifDS.Checked)
        My.Settings.NotifyDeletedServices = Me.MenuItemNotifDS.Checked
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifNP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifNP.Click
        Me.MenuItemNotifNP.Checked = Not (Me.MenuItemNotifNP.Checked)
        My.Settings.NotifyNewProcesses = Me.MenuItemNotifNP.Checked
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifNS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifNS.Click
        Me.MenuItemNotifNS.Checked = Not (Me.MenuItemNotifNS.Checked)
        My.Settings.NotifyNewServices = Me.MenuItemNotifNS.Checked
        My.Settings.Save()
    End Sub

    Private Sub MenuItemNotifTP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemNotifTP.Click
        Me.MenuItemNotifTP.Checked = Not (Me.MenuItemNotifTP.Checked)
        My.Settings.NotifyTerminatedProcesses = Me.MenuItemNotifTP.Checked
        My.Settings.Save()
    End Sub

    Private Sub timerJobs_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timerJobs.Tick
        Call refreshJobList()
    End Sub

    Private Sub lvJob_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvJob.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call butJobDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvJob_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvJob.MouseDoubleClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Call butJobDetails_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lvJob_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvJob.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim selectionIsNotNothing As Boolean = (Me.lvJob.SelectedItems IsNot Nothing _
                                        AndAlso Me.lvJob.SelectedItems.Count > 0)
            Me.MenuItemJobTerminate.Enabled = selectionIsNotNothing AndAlso _notWMI AndAlso _notSnapshotMode
            Me.MenuItemCopyJob.Enabled = selectionIsNotNothing
            Me.mnuJob.Show(Me.lvJob, e.Location)
        End If
    End Sub

    Private Sub MenuItemJobTerminate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemJobTerminate.Click
        If WarnDangerousAction("Are you sure you want to terminate these jobs ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each cJ As cJob In Me.lvJob.GetSelectedItems
            cJ.TerminateJob()
        Next
    End Sub

    Private Sub butJobRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobRefresh.Click
        Me.refreshJobList()
    End Sub

    Private Sub butJobTerminate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobTerminate.Click
        Call MenuItemJobTerminate_Click(Nothing, Nothing)
    End Sub

    Private Sub butJobDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobDetails.Click
        For Each it As cJob In Me.lvJob.GetSelectedItems
            Dim frm As New frmJobInfo
            frm.SetJob(it)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemJobMng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItemJobMng.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Dim theJob As cJob = cJob.GetProcessJobById(cp.Infos.ProcessId)
            If theJob IsNot Nothing Then
                Dim frm As New frmJobInfo
                frm.SetJob(theJob)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            End If
        Next
    End Sub

    Private Sub MenuItemProcAddToJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcAddToJob.Click
        ' Add to job

        ' Get list of PIDs
        Dim pid As New List(Of Integer)
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            pid.Add(cp.Infos.ProcessId)
        Next

        Dim frm As New frmAddToJob(pid)
        frm.ShowDialog()

    End Sub

    Private Sub MenuItemProcKillByMethod_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemProcKillByMethod.Click
        For Each cp As cProcess In Me.lvProcess.GetSelectedItems
            Dim frm As New frmKillProcessByMethod
            frm.ProcessToKill = cp
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub butJobElevate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butJobElevate.Click
        ' Restart elevated
        Call cEnvironment.RestartElevated()
    End Sub

    Private Sub lblTaskCountResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTaskCountResult.Click
        If Me.lvTask.Groups(1).Items.Count > 0 Then
            Me.lvTask.Focus()
            Me.lvTask.EnsureVisible(Me.lvTask.Groups(1).Items(0).Index)
            Me.lvTask.SelectedItems.Clear()
            Me.lvTask.Groups(1).Items(0).Selected = True
        End If
    End Sub

    Private Sub MenuItemCreateService_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemCreateService.Click
        Dim frm As New frmCreateService
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub butCreateService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCreateService.Click
        Call MenuItemCreateService_Click(Nothing, Nothing)
    End Sub

    Private Sub butDeleteService_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butDeleteService.Click
        If WarnDangerousAction("Are you sure you want to delete these services ?", Me.Handle) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If
        For Each it As cService In Me.lvServices.GetSelectedItems
            it.DeleteService()
        Next
        Call Me.lvServices_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub MenuItemServDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemServDelete.Click
        Call butDeleteService_Click(Nothing, Nothing)
    End Sub

    Public Delegate Sub NewUpdateAvailableNotification(ByVal release As cUpdate.NewReleaseInfos)
    Public Delegate Sub NoNewUpdateAvailableNotification()
    Public Delegate Sub FailedToCheckUpDateNotification(ByVal msg As String)
    Public Delegate Sub GotErrorFromServer(ByVal err As Exception)

#Region "Select window in window tab feature"

    Private Sub MenuItemTaskSelectWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemTaskSelectWindow.Click
        ' Select the task's window in the "window tab" of the associated process'
        ' detailed form
        ' This is a bit tricky, but here is it :
        For Each it As cTask In Me.lvTask.GetSelectedItems

            ' Retrieve the associated process
            Dim _proc As cProcess = ProcessProvider.GetProcessById(it.Infos.ProcessId)

            If _proc IsNot Nothing Then

                ' Open the process' detailed form
                Dim frm As New frmProcessInfo
                frm.SetProcess(_proc)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
                ' Display 'Windows' tab
                frm.tabProcess.SelectedTab = frm.TabPageWindows

                ' Create a thread which wait for threads to be added in the lvThread
                ' and then select the good thread
                Threading.ThreadPool.QueueUserWorkItem(AddressOf selectWindowImp, New contextObjSelectWindow(it.Infos.Handle.ToString, frm))
            End If
        Next
    End Sub

    Private Structure contextObjSelectWindow
        Public handle As String
        Public frmProcInfo As frmProcessInfo
        Public Sub New(ByVal hWnd As String, ByVal form As frmProcessInfo)
            handle = hWnd
            frmProcInfo = form
        End Sub
    End Structure
    Private Sub selectWindowImp(ByVal context As Object)
        Dim pObj As contextObjSelectWindow = DirectCast(context, contextObjSelectWindow)

        ' Wait for windows to be added in the listview
        While pObj.frmProcInfo.lvWindows.Items.Count = 0
            Threading.Thread.Sleep(50)
        End While

        ' Select the good window
        Async.ListView.EnsureItemVisible(pObj.frmProcInfo.lvWindows, pObj.handle)
    End Sub

#End Region

    Private Sub MenuItemNetworkPing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkPing.Click
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            Dim frm As New frmNetworkTool(it, Native.Api.Enums.ToolType.Ping)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemNetworkRoute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkRoute.Click
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            Dim frm As New frmNetworkTool(it, Native.Api.Enums.ToolType.TraceRoute)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemNetworkWhoIs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemNetworkWhoIs.Click
        For Each it As cNetwork In Me.lvNetwork.GetSelectedItems
            Dim frm As New frmNetworkTool(it, Native.Api.Enums.ToolType.WhoIs)
            frm.TopMost = _frmMain.TopMost
            frm.Show()
        Next
    End Sub

    Private Sub MenuItemSystemNetworkInfos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemNetworkInfos.Click
        Call Me.butNetworkInfos_Click(Nothing, Nothing)
    End Sub

    Private Sub MenuItemMainNetworkInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemMainNetworkInfo.Click
        Call Me.butNetworkInfos_Click(Nothing, Nothing)
    End Sub

    Private Sub butNetworkInfos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butNetworkInfos.Click
        Program._frmNetworkInfo.Show()
    End Sub

    Private Sub MenuItemSystemScripting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemScripting.Click
        Call Me.butScripting_Click(Nothing, Nothing)
    End Sub

    Private Sub butScripting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butScripting.Click
        Dim frm As New frmScripting
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub MenuItemSystemSaveSSFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemSaveSSFile.Click
        Call Me.butSaveSystemSnaphotFile_Click(Nothing, Nothing)
    End Sub

    Private Sub butSaveSystemSnaphotFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butSaveSystemSnaphotFile.Click
        ' Save System Snapshot File
        Dim frm As New frmSaveSystemSnapshot
        frm.ShowDialog()
    End Sub

    Private Sub MenuItemSystemExploreSSFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemExploreSSFile.Click
        Call Me.butExploreSSFile_Click(Nothing, Nothing)
    End Sub

    Private Sub butExploreSSFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butExploreSSFile.Click
        Dim frm As New frmSnapshotInfos
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub MenuItemSystemCheckSignatures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItemSystemCheckSignatures.Click
        Call Me.butCheckSignatures_Click(Nothing, Nothing)
    End Sub

    Private Sub butCheckSignatures_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butCheckSignatures.Click
        Dim frm As New frmCheckSignatures
        frm.TopMost = _frmMain.TopMost
        frm.Show()
    End Sub

    Private Sub butFreeMemory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butFreeMemory.Click
        ' Calls the garbage collector
        Program.CollectGarbage()
    End Sub

    Private Sub lvMonitorReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvMonitorReport.Click
        ' Select associated item in tvMonitor
        If Me.lvMonitorReport.SelectedItems.Count > 0 Then
            Dim it As ListViewItem = Me.lvMonitorReport.SelectedItems(0)
            Misc.ShowTvNodeByText(Me.tvMonitor, it.Text)
        End If
    End Sub

    Private Sub saveMonitorReport()
        ' Save a report of which is monitored using perfmon counters
        Dim sFile As String
        With Me.saveDial
            .AddExtension = True
            .CheckPathExists = True
            .Filter = "HTML (*.html)|*.html"
            .Title = "Save report"
            If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                sFile = .FileName
            Else
                Exit Sub
            End If
        End With

        Me.panelMainMonitoring.Enabled = False
        Me.timerMonitoring.Enabled = False

        ' Save to an html file
        ' Defines columns
        Dim totalWidth As Integer = 0
        For Each c As ColumnHeader In Me.lvMonitorReport.Columns
            totalWidth += c.Width
        Next
        Dim col(Me.lvMonitorReport.Columns.Count) As cHTML.HtmlColumnStructure
        Dim colCount As Integer = 0
        For Each c As ColumnHeader In Me.lvMonitorReport.Columns
            With col(colCount)
                .sizePercent = CInt(100 * c.Width / totalWidth)
                .title = c.Text
            End With
            colCount += 1
        Next

        ' Defines title
        Dim title As String = Me.lvMonitorReport.Items.Count.ToString & " perfmon counter(s)"
        Dim _html As New cHTML(col, sFile, title)

        ' Write items
        Dim it As ListViewItem
        Dim x As Integer = 0
        For Each it In Me.lvMonitorReport.Items
            Dim _lin(Me.lvMonitorReport.Columns.Count) As String
            colCount = 0
            For Each c As ColumnHeader In Me.lvMonitorReport.Columns
                _lin(colCount) = it.SubItems(colCount).Text
                colCount += 1
            Next
            ' Write line to HTML
            _html.AppendLine(_lin)
            ' Select node associated to lvItem'
            ' so it well update image in me.graphMonitor
            Misc.ShowTvNodeByText(Me.tvMonitor, it.Text)
            ' Save image in html
            _html.AppendImage(Me.graphMonitor.GetImage, it.Text)
            x += 1
        Next

        If _html.ExportHTML = False Then
            Misc.ShowMsg("Save report", "Could not save report !", MessageBoxButtons.OK)
        End If

        Me.panelMainMonitoring.Enabled = True
        Me.timerMonitoring.Enabled = True
    End Sub

End Class