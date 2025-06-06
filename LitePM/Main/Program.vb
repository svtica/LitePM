﻿
' Lite Process Monitor


Option Strict On

Imports System.Configuration
Imports System.Linq
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Module Program

    ' Exit code when leave LitePM in ModeRequestReplaceTaskMgr
    Public Enum RequestReplaceTaskMgrResult As Byte
        ReplaceSuccess
        ReplaceFail
        NotReplaceSuccess
        NotReplaceFail
    End Enum

    ' Constants for command line of LitePM
    Public Const PARAM_DO_NOT_CHECK_PREV_INSTANCE As String = " -donotcheckprevinstance"
    Public Const PARAM_CHANGE_TASKMGR As String = " -reptaskmgr"

    ' Represent options passed with command line
    Public Class ProgramParameters

        ' Available parameters
        Private isServerMode As Boolean = False
        Private remPort As Integer = 8081
        Private isHidden As Boolean = False
        Private requestReplaceTaskMgr As Boolean = False
        Private replaceTaskMgrValue As Boolean = False
        Private ssFileModeValue As String
        Private oneInstance As Boolean = True
        Private useDriver As Boolean = True
        Private serviceMode As Boolean = False
        Private ssFileMode As Boolean = False
        Private guiMode As Boolean = True
        Private filtermode As Boolean = False
        Private procfilter As String

        Public ReadOnly Property ModeServer() As Boolean
            Get
                Return isServerMode
            End Get
        End Property
        Public ReadOnly Property ModeServerService() As Boolean
            Get
                Return serviceMode
            End Get
        End Property
        Public ReadOnly Property ModeHidden() As Boolean
            Get
                Return isHidden
            End Get
        End Property
        Public ReadOnly Property RemotePort() As Integer
            Get
                Return remPort
            End Get
        End Property
        Public ReadOnly Property ModeRequestReplaceTaskMgr() As Boolean
            Get
                Return requestReplaceTaskMgr
            End Get
        End Property
        Public ReadOnly Property ValueReplaceTaskMgr() As Boolean
            Get
                Return replaceTaskMgrValue
            End Get
        End Property
        Public ReadOnly Property ValueCreateSSFile() As String
            Get
                Return ssFileModeValue
            End Get
        End Property
        Public ReadOnly Property OnlyOneInstance() As Boolean
            Get
                Return oneInstance
            End Get
        End Property
        Public ReadOnly Property UseKernelDriver() As Boolean
            Get
                Return useDriver
            End Get
        End Property
        Public ReadOnly Property ModeSnapshotFileCreation() As Boolean
            Get
                Return ssFileMode
            End Get
        End Property

        Public ReadOnly Property ModeGUI() As Boolean
            Get
                Return guiMode
            End Get
        End Property

        Public ReadOnly Property ModeFilter() As Boolean
            Get
                Return filtermode
            End Get
        End Property

        Public ReadOnly Property FilterValue As String
            Get
                Return procfilter
            End Get
        End Property

        Public Sub New(ByRef parameters As String())
            If parameters Is Nothing Then
                Exit Sub
            End If
            For i As Integer = 0 To parameters.Length - 1
                If parameters(i).ToUpperInvariant = "-SERVER" Then
                    isServerMode = True
                ElseIf parameters(i).ToUpperInvariant = "-HIDE" Then
                    isHidden = True
                ElseIf parameters(i).ToUpperInvariant = "-PORT" Then
                    If parameters.Length - 1 >= i + 1 Then
                        remPort = CInt(Val(parameters(i + 1)))
                    End If
                ElseIf parameters(i).ToUpperInvariant = "-REPTASKMGR" Then
                    If parameters.Length - 1 >= i + 1 Then
                        replaceTaskMgrValue = CBool(Val(parameters(i + 1)))
                        requestReplaceTaskMgr = True
                    End If
                ElseIf parameters(i).ToUpperInvariant = "-DONOTCHECKPREVINSTANCE" Then
                    oneInstance = False
                ElseIf parameters(i).ToUpperInvariant = "-NODRIVER" Then
                    useDriver = False
                ElseIf parameters(i).ToUpperInvariant = "-SSFILE" Then
                    If parameters.Length - 1 >= i + 1 Then
                        ssFileModeValue = parameters(i + 1)
                        ssFileMode = True
                    End If
                ElseIf parameters(i).ToUpperInvariant = "-SERVERSERVICE" Then
                    serviceMode = True
                ElseIf parameters(i).ToUpperInvariant = "-FILTER" Then
                    If parameters.Length - 1 >= i + 1 Then
                        procfilter = parameters(i + 1)
                        filtermode = True
                        isHidden = False
                    End If
                End If
            Next
        End Sub
    End Class



    Public _frmMain As frmMain
    Public _frmServer As frmServer
    Public _frmNetworkInfo As frmNetworkInfo
    Public _frmSystemInfo As frmSystemInfo

    Private _processProvider As ProcessProvider
    Private _handleProvider As HandleProvider
    Private _threadProvider As ThreadProvider
    Private _moduleProvider As ModuleProvider
    Private _windowProvider As WindowProvider
    Private _logProvider As LogProvider
    Private _memRegionProvider As MemRegionProvider
    Private _serviceProvider As ServiceProvider
    Private _jobLimitsProvider As JobLimitsProvider
    Private _privilegeProvider As PrivilegeProvider
    Private _heapProvider As HeapProvider
    Private _networkProvider As NetworkConnectionsProvider
    Private _envVariableProvider As EnvVariableProvider
    Private _jobProvider As JobProvider
    Private WithEvents _updater As cUpdate
    Private _progParameters As ProgramParameters
    Private WithEvents theConnection As New cConnection
    Private _systemInfo As cSystemInfo
    Private _hotkeys As cHotkeys
    Private _pref As Pref
    Private _log As cLog
    Private _isVistaOrAbove As Boolean
    Private _isAdmin As Boolean
    Private _trayIcon As cTrayIcon
    Private _ConnectionForm As frmConnection
    Private _time As Integer
    Private _isElevated As Boolean
    Private _mustCloseWithCloseButton As Boolean = False
    Private procfilter As String


    Public ReadOnly Property LogPath() As String
        Get
            Static path As String = Nothing
            If path Is Nothing Then
                path = cFile.GetParentDir(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath)
            End If
            Return path & "log.txt"
        End Get
    End Property
    Public ReadOnly Property HotkeysXmlPath() As String
        Get
            Static path As String = Nothing
            If path Is Nothing Then
                path = cFile.GetParentDir(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath)
            End If
            Return path & "hotkeys.xml"
        End Get
    End Property
    Public ReadOnly Property ErrorLog(ByVal e As Exception) As String
        Get
            Dim s As String = "System informations : "
            s &= vbNewLine & vbTab & "Name : " & My.Computer.Info.OSFullName
            s &= vbNewLine & vbTab & "Platform : " & My.Computer.Info.OSPlatform
            s &= vbNewLine & vbTab & "Version : " & My.Computer.Info.OSVersion.ToString
            s &= vbNewLine & vbTab & "UICulture : " & My.Computer.Info.InstalledUICulture.ToString
            s &= vbNewLine & vbTab & "Processor count : " & Program.PROCESSOR_COUNT.ToString
            s &= vbNewLine & vbTab & "Physical memory : " & Misc.GetFormatedSize(My.Computer.Info.AvailablePhysicalMemory) & "/" & Misc.GetFormatedSize(My.Computer.Info.TotalPhysicalMemory)
            s &= vbNewLine & vbTab & "Virtual memory : " & Misc.GetFormatedSize(My.Computer.Info.AvailableVirtualMemory) & "/" & Misc.GetFormatedSize(My.Computer.Info.TotalVirtualMemory)
            s &= vbNewLine & vbTab & "Screen : " & My.Computer.Screen.Bounds.ToString
            s &= vbNewLine & vbTab & "IntPtr.Size : " & IntPtr.Size.ToString
            s &= vbNewLine & vbNewLine
            s &= "User informations : "
            s &= vbNewLine & vbTab & "Admin : " & Program.IsAdministrator.ToString
            s &= vbNewLine & vbNewLine
            s &= "Application informations : "
            s &= vbNewLine & vbTab & "Path : " & My.Application.Info.DirectoryPath
            s &= vbNewLine & vbTab & "Version : " & My.Application.Info.Version.ToString
            s &= vbNewLine & vbTab & "WorkingSetSize : " & My.Application.Info.WorkingSet.ToString
            s &= vbNewLine & vbNewLine
            s &= "Error informations : "
            s &= vbNewLine & vbTab & "Message : " & e.Message
            s &= vbNewLine & vbTab & "Source : " & e.Source
            s &= vbNewLine & vbTab & "StackTrace : " & e.StackTrace
            s &= vbNewLine & vbTab & "Target : " & e.TargetSite.ToString
            s &= vbNewLine & vbNewLine
            s &= "Other informations : "
            s &= vbNewLine & vbTab & "Connection : " & Program.Connection.Type.ToString
            s &= vbNewLine & vbTab & "Connected : " & Program.Connection.IsConnected.ToString
            s &= vbNewLine & vbTab & "Elapsed time : " & Program.ElapsedTime.ToString
            Return s
        End Get
    End Property
    Public ReadOnly Property Parameters() As ProgramParameters
        Get
            Return _progParameters
        End Get
    End Property
    Public ReadOnly Property ElapsedTime() As Integer
        Get
            Return Native.Api.Win32.GetElapsedTime - _time
        End Get
    End Property
    Public ReadOnly Property Connection() As cConnection
        Get
            Return theConnection
        End Get
    End Property
    Public ReadOnly Property SystemInfo() As cSystemInfo
        Get
            Return _systemInfo
        End Get
    End Property
    Public ReadOnly Property Hotkeys() As cHotkeys
        Get
            Return _hotkeys
        End Get
    End Property
    Public ReadOnly Property Preferences() As Pref
        Get
            Return _pref
        End Get
    End Property
    Public ReadOnly Property Log() As cLog
        Get
            Return _log
        End Get
    End Property
    Public ReadOnly Property IsAdministrator() As Boolean
        Get
            Return _isAdmin
        End Get
    End Property

    Public ReadOnly Property TrayIcon() As cTrayIcon
        Get
            Return _trayIcon
        End Get
    End Property
    Public ReadOnly Property ConnectionForm() As frmConnection
        Get
            Return _ConnectionForm
        End Get
    End Property
    Public ReadOnly Property IsElevated() As Boolean
        Get
            Return _isElevated
        End Get
    End Property
    Public ReadOnly Property Updater() As cUpdate
        Get
            Return _updater
        End Get
    End Property
    Public Property MustCloseWithCloseButton() As Boolean
        Get
            Return _mustCloseWithCloseButton
        End Get
        Set(ByVal value As Boolean)
            _mustCloseWithCloseButton = value
        End Set
    End Property
    Public ReadOnly Property ProcessProvider() As ProcessProvider
        Get
            Return _processProvider
        End Get
    End Property
    Public ReadOnly Property ServiceProvider() As ServiceProvider
        Get
            Return _serviceProvider
        End Get
    End Property
    Public ReadOnly Property EnvVariableProvider() As EnvVariableProvider
        Get
            Return _envVariableProvider
        End Get
    End Property
    Public ReadOnly Property NetworkConnectionsProvider() As NetworkConnectionsProvider
        Get
            Return _networkProvider
        End Get
    End Property
    Public ReadOnly Property HeapProvider() As HeapProvider
        Get
            Return _heapProvider
        End Get
    End Property
    Public ReadOnly Property PrivilegeProvider() As PrivilegeProvider
        Get
            Return _privilegeProvider
        End Get
    End Property
    Public ReadOnly Property WindowProvider() As WindowProvider
        Get
            Return _windowProvider
        End Get
    End Property
    Public ReadOnly Property JobProvider() As JobProvider
        Get
            Return _jobProvider
        End Get
    End Property
    Public ReadOnly Property JobLimitsProvider() As JobLimitsProvider
        Get
            Return _jobLimitsProvider
        End Get
    End Property
    Public ReadOnly Property ModuleProvider() As ModuleProvider
        Get
            Return _moduleProvider
        End Get
    End Property
    Public ReadOnly Property ThreadProvider() As ThreadProvider
        Get
            Return _threadProvider
        End Get
    End Property
    Public ReadOnly Property HandleProvider() As HandleProvider
        Get
            Return _handleProvider
        End Get
    End Property
    Public ReadOnly Property MemRegionProvider() As MemRegionProvider
        Get
            Return _memRegionProvider
        End Get
    End Property


    Public Const HELP_PATH_INTERNET As String = "https://"
    Public Const HELP_PATH_DD As String = "help.md"
    Public Const NO_INFO_RETRIEVED As String = "N/A"

    Public NEW_ITEM_COLOR As Color = Color.FromArgb(128, 255, 0)
    Public DELETED_ITEM_COLOR As Color = Color.FromArgb(255, 64, 48)
    Public PROCESSOR_COUNT As Integer



    Public Sub Main()


        '  Some basic initialisations
        ' /!\ Looks like Comctl32 v6 could not be initialized before
        ' a form is loaded. So as Comctl32 v5 can not display VistaDialogBoxes,
        ' all error messages before instanciation of a form should use classical style.
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)    ' Use GDI, not GDI+



        '  Save time of start
        _time = Native.Api.Win32.GetElapsedTime



        '  Check if framework is 2.0 or above
        If cEnvironment.IsFramework2OrAbove = False Then
            Misc.ShowError(".Net Framework 2.0 must be installed.", True)
            Application.Exit()
        End If



        '  Other init
        _isVistaOrAbove = cEnvironment.IsWindowsVistaOrAbove
        _isAdmin = cEnvironment.IsAdmin
        _isElevated = (cEnvironment.GetElevationType = Native.Api.Enums.ElevationType.Full)


        '  Read parameters
        _progParameters = New ProgramParameters(Environment.GetCommandLineArgs)


        '  We replace Taskmgr if needed. This will end LitePM
        If _progParameters.ModeRequestReplaceTaskMgr Then
            Call safeReplaceTaskMgr(_progParameters.ValueReplaceTaskMgr)
        End If


        '  We create a snapshot file
        If _progParameters.ModeSnapshotFileCreation Then

            ' Request debug privilege (if possible)
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.DebugPrivilege)

            ' New connection
            theConnection = New cConnection

            ' Used for service enumeration. Snapshot enumeration of services
            ' need a cServiceConnection, retrieved as a property in
            ' _frmMain.lvServices
            _frmMain = New frmMain

            ' This initializes the Handle Enumeration Class (needed to enumerate
            ' handles)
            Native.Objects.Handle.HandleEnumerationClass =
                    New Native.Objects.HandleEnumeration(_progParameters.UseKernelDriver And
                                                 cEnvironment.Is32Bits)

            ' Providers
            _processProvider = New ProcessProvider  ' Process provider
            _serviceProvider = New ServiceProvider  ' Service provider
            _envVariableProvider = New EnvVariableProvider  ' Env variables provider
            _networkProvider = New NetworkConnectionsProvider   ' Network connections
            _heapProvider = New HeapProvider        ' Heap provider
            _privilegeProvider = New PrivilegeProvider  ' Privilege provider
            _windowProvider = New WindowProvider    ' Window provider
            _jobProvider = New JobProvider          ' Job provider
            _jobLimitsProvider = New JobLimitsProvider   ' Job limits provider
            _moduleProvider = New ModuleProvider    ' Module provider
            _threadProvider = New ThreadProvider    ' Thread provider
            _memRegionProvider = New MemRegionProvider  ' mem region provider
            _handleProvider = New HandleProvider    ' Handle provider
            _logProvider = New LogProvider          ' Log provider

            ' Connect to the local machine
            theConnection.SyncConnect()     ' Synchronous connection !!!

            Call createSSFile(_progParameters.ValueCreateSSFile)
            Exit Sub
        End If


        '  Close application if there is a previous instance of LitePM running
        If _progParameters.ModeServerService = False Then

            If _progParameters.OnlyOneInstance And cEnvironment.IsAlreadyRunning Then
                Exit Sub
            End If



            '  Set handler for exceptions
            AddHandler Application.ThreadException, AddressOf MYThreadHandler
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf MYExnHandler
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)



            '  Parse port files
            Call cNetwork.ParsePortTextFile()



            '  Enable some privileges
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.DebugPrivilege)
            cEnvironment.RequestPrivilege(cEnvironment.PrivilegeToRequest.ShutdownPrivilege)



            '  Instanciate all classes

            ' Common classes
            theConnection = New cConnection     ' The cConnection instance of the connection
            _systemInfo = New cSystemInfo       ' System informations
            _ConnectionForm = New frmConnection(theConnection)


            ' FOR NOW, we use kernel only on 32 bits systems...
            Native.Objects.Handle.HandleEnumerationClass =
                        New Native.Objects.HandleEnumeration(_progParameters.UseKernelDriver And
                                                             cEnvironment.Is32Bits)


            ' Classes for client only
            If _progParameters.ModeServer = False Then
                _pref = New Pref                        ' Preferences
                _hotkeys = New cHotkeys                 ' Hotkeys
                _log = New cLog                         ' Log instance
                _trayIcon = New cTrayIcon(2)            ' Tray icons
                _updater = New cUpdate                  ' Updater class
                _frmNetworkInfo = New frmNetworkInfo    ' Network info
                _frmSystemInfo = New frmSystemInfo      ' System info
            Else
                _frmServer = New frmServer          ' Server form (server mode)
            End If


            ' Common classes
            _frmMain = New frmMain                  ' Main form
            _processProvider = New ProcessProvider  ' Process provider
            _serviceProvider = New ServiceProvider  ' Service provider
            _envVariableProvider = New EnvVariableProvider  ' Env variables provider
            _networkProvider = New NetworkConnectionsProvider   ' Network connections
            _heapProvider = New HeapProvider        ' Heap provider
            _privilegeProvider = New PrivilegeProvider  ' Privilege provider
            _windowProvider = New WindowProvider    ' Window provider
            _jobProvider = New JobProvider          ' Job provider
            _jobLimitsProvider = New JobLimitsProvider   ' Job limits provider
            _moduleProvider = New ModuleProvider    ' Module provider
            _threadProvider = New ThreadProvider    ' Thread provider
            _memRegionProvider = New MemRegionProvider  ' mem region provider
            _handleProvider = New HandleProvider    ' Handle provider
            _logProvider = New LogProvider          ' Log provider


            '  Load preferences
            If My.Settings.ShouldUpgrade Then
                Try
                    ' Try to update settings from a previous version of LitePM
                    My.Settings.Upgrade()
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
                My.Settings.ShouldUpgrade = False
                My.Settings.Save()
            End If
            If _progParameters.ModeServer = False Then
                Try
                    If My.Settings.FirstTime Then
                        My.Settings.FirstTime = False
                        Program.Preferences.Save()
                    End If
                    Program.Preferences.Apply()
                    cProcess.BuffSize = My.Settings.HistorySize
                Catch ex As Exception
                    ' Preference file corrupted/missing
                    Misc.ShowMsg("Startup error", "Failed to load preferences." & " Preference file is missing or corrupted and will be now recreated.", MessageBoxButtons.OK)
                    Program.Preferences.SetDefault()
                End Try
            End If



            '  Read hotkeys & state based actions from XML files
            If _progParameters.ModeServer = False Then
                Call frmHotkeys.readHotkeysFromXML()
                'Call frmBasedStateAction.readStateBasedActionFromXML()
            End If

            ' Launch the main form with filter mode active
            If _progParameters.ModeFilter = True Then
                RunwithFilter(_progParameters.FilterValue)
            End If

            '  Show main form & start application
            If _progParameters.ModeServer Then
                Application.Run(_frmServer)
            Else
                Application.Run(_frmMain)
            End If

        Else
            ' Then LitePM is a service !

            '  Instanciate all classes

            ' Common classes
            theConnection = New cConnection     ' The cConnection instance of the connection
            _systemInfo = New cSystemInfo       ' System informations
            _ConnectionForm = New frmConnection(theConnection)

            ' FOR NOW, we use kernel only on 32 bits systems...
            Native.Objects.Handle.HandleEnumerationClass =
                        New Native.Objects.HandleEnumeration(False)

            Dim service As New LitePMLauncherService.InteractiveProcess
            ServiceProcess.ServiceBase.Run(service)
        End If

    End Sub

    ' Handler for exceptions
    Private Sub MYExnHandler(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Dim ex As Exception
        ex = CType(e.ExceptionObject, Exception)
        Console.WriteLine(ex.StackTrace)
        Dim t As New frmError(ex)
        t.TopMost = True
#If RELEASE_MODE = 1 Then
        t.ShowDialog()
#End If
    End Sub
    Private Sub MYThreadHandler(ByVal sender As Object, ByVal e As Threading.ThreadExceptionEventArgs)
        Console.WriteLine(e.Exception.StackTrace)
        Dim t As New frmError(e.Exception)
        t.TopMost = True
#If RELEASE_MODE = 1 Then
        t.ShowDialog()
#End If
    End Sub



    ' Free memory (GC)
    Public Sub CollectGarbage()
        ' Use GC to collect
        GC.Collect()
        GC.WaitForPendingFinalizers()
        GC.Collect()
    End Sub

    ' Generate an unhandled error
    Public Sub ThrowUnhandledError()
        Dim x As IntPtr = IntPtr.Zero
        Marshal.ReadInt64(x, 0)
    End Sub

    ' Exit application
    Public Sub ExitLitePM()

        ' Save settings
        Pref.SaveListViewColumns(_frmMain.lvTask, "COLmain_task")
        Pref.SaveListViewColumns(_frmMain.lvServices, "COLmain_service")
        Pref.SaveListViewColumns(_frmMain.lvProcess, "COLmain_process")
        Pref.SaveListViewColumns(_frmMain.lvNetwork, "COLmain_network")

        My.Settings.Save()

        ' Uninstall driver
        Try
            Native.Objects.Handle.HandleEnumerationClass.Close()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

        ' Close forms & exit
        Program.MustCloseWithCloseButton = True
        If _frmMain IsNot Nothing Then
            ' Close form
            _frmMain.Close()
        End If
        Application.Exit()

    End Sub

    Public Sub ClearDictionaries()
        ' Clear lists of processes/services
        ProcessProvider.ClearList()
        ProcessProvider.FirstRefreshDone = False

        ServiceProvider.ClearList()
        ServiceProvider.FirstRefreshDone = False

        EnvVariableProvider.ClearList()
        EnvVariableProvider.FirstRefreshDone = False

        HeapProvider.ClearList()
        HeapProvider.FirstRefreshDone = False

        NetworkConnectionsProvider.ClearList()
        NetworkConnectionsProvider.FirstRefreshDone = False

        PrivilegeProvider.ClearList()
        PrivilegeProvider.FirstRefreshDone = False

        WindowProvider.ClearList()
        WindowProvider.FirstRefreshDone = False

        JobProvider.ClearList()
        JobProvider.FirstRefreshDone = False

        JobLimitsProvider.ClearList()
        JobLimitsProvider.FirstRefreshDone = False

        ModuleProvider.ClearList()
        ModuleProvider.FirstRefreshDone = False

        ThreadProvider.ClearList()
        ThreadProvider.FirstRefreshDone = False

        MemRegionProvider.ClearList()
        MemRegionProvider.FirstRefreshDone = False

        HandleProvider.ClearList()
        HandleProvider.FirstRefreshDone = False

        LogProvider.ClearList()
        LogProvider.FirstRefreshDone = False

        ProcessProvider.ClearNewProcessesDico()
        ServiceProvider.ClearNewServicesList()
    End Sub

    Private Sub theConnection_Disconnected() Handles theConnection.Disconnected
        Program.ClearDictionaries()
    End Sub

    ' Create a snapshot file
    Private Function createSSFile(ByVal file As String) As Boolean

        Try
            Dim res As String = Nothing

            ' Create empty snapshot file
            Dim snap As New cSnapshot250

            ' Get options
            Dim options As Native.Api.Enums.SnapshotObject = Native.Api.Enums.SnapshotObject.All

            ' Build it
            If snap.CreateSnapshot(Program.Connection, options, res) = False Then
                ' Failed
                'Misc.ShowMsg("Snapshot creation", "Could not build snapshot.", res, MessageBoxButtons.OK, TaskDialogIcon.Error)
                Return False
            End If

            ' Save it
            If snap.SaveSnapshot(file, res) = False Then
                ' Failed
                'Misc.ShowMsg("Snapshot creation", "Could not save snapshot.", res, MessageBoxButtons.OK, TaskDialogIcon.Error)
                Return False
            End If

            Return True

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            Return False
        End Try

    End Function

    ' Replace taskmgr
    ' This function will end LitePM with a specific ExitCode (if fail or not)
    Private Sub safeReplaceTaskMgr(ByVal value As Boolean)
        Try
            Dim regKey As RegistryKey
            regKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", True)

            If value Then
                regKey.CreateSubKey("taskmgr.exe").SetValue("debugger", Application.ExecutablePath)
            Else
                regKey.DeleteSubKey("taskmgr.exe")
            End If

            ' Success
            Dim res As RequestReplaceTaskMgrResult
            If value Then
                res = RequestReplaceTaskMgrResult.ReplaceSuccess
            Else
                res = RequestReplaceTaskMgrResult.NotReplaceSuccess
            End If
            Call Native.Api.NativeFunctions.ExitProcess(res)

        Catch ex As Exception
            ' Could not set key -> failed
            Misc.ShowDebugError(ex)
            Dim res As RequestReplaceTaskMgrResult
            If value Then
                res = RequestReplaceTaskMgrResult.ReplaceFail
            Else
                res = RequestReplaceTaskMgrResult.NotReplaceFail
            End If
            Call Native.Api.NativeFunctions.ExitProcess(res)

        End Try
    End Sub

    Private Sub _updater_FailedToCheckVersion(ByVal silent As Boolean, ByVal msg As String) Handles _updater.FailedToCheckVersion
        ' Failed to check update
        If silent Then
            ' Silent mode -> only displays a tooltip
            If _frmMain IsNot Nothing AndAlso _frmMain.Tray IsNot Nothing Then
                With _frmMain.Tray
                    .BalloonTipText = msg
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "Could not check if LitePM us ip to date."
                    .ShowBalloonTip(3000)
                End With
            End If
        Else
            _frmMain.Invoke(New frmMain.FailedToCheckUpDateNotification(AddressOf impFailedToCheckUpDateNotification), msg)
        End If
    End Sub

    Private Sub _updater_NewVersionAvailable(ByVal silent As Boolean, ByVal release As cUpdate.NewReleaseInfos) Handles _updater.NewVersionAvailable
        ' A new version of LitePM is available
        If silent Then
            ' Silent mode -> only displays a tooltip
            If _frmMain IsNot Nothing AndAlso _frmMain.Tray IsNot Nothing Then
                With _frmMain.Tray
                    .BalloonTipText = release.Infos
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "A new version of LitePM is available !"
                    .ShowBalloonTip(3000)
                End With
            End If
        Else
            _frmMain.Invoke(New frmMain.NewUpdateAvailableNotification(AddressOf impNewUpdateAvailableNotification), release)
        End If
    End Sub

    Private Sub _updater_ProgramUpToDate(ByVal silent As Boolean) Handles _updater.ProgramUpToDate
        ' LitePM is up to date (no new version available)
        If silent Then
            ' Silent mode -> only displays a tooltip
            If _frmMain IsNot Nothing AndAlso _frmMain.Tray IsNot Nothing Then
                With _frmMain.Tray
                    .BalloonTipText = "LitePM is up to date !"
                    .BalloonTipIcon = ToolTipIcon.Info
                    .BalloonTipTitle = "No new version of LitePM is available."
                    .ShowBalloonTip(3000)
                End With
            End If
        Else
            _frmMain.Invoke(New frmMain.NoNewUpdateAvailableNotification(AddressOf impNoNewUpdateAvailableNotification))
        End If
    End Sub

    ' Called when a new update is available
    ' It's here cause of thread safety
    Public Sub impNewUpdateAvailableNotification(ByVal release As cUpdate.NewReleaseInfos)
        Dim frm As New frmNewVersionAvailable(release)
        frm.ShowDialog()
    End Sub

    ' Called when no new update is available
    ' It's here cause of thread safety
    Public Sub impNoNewUpdateAvailableNotification()
        Common.Misc.ShowMsg("LitePM update",
                          "LitePM is up to date !" & vbNewLine & vbNewLine &
                          "The current version (" & My.Application.Info.Version.ToString & ") is the latest available for download.",
                          MessageBoxButtons.OK)
    End Sub

    ' Called when failed to check is LitePM is up to date
    Public Sub impFailedToCheckUpDateNotification(ByVal msg As String)
        Common.Misc.ShowMsg("LitePM update",
                                "Could not check if LitePM is up to date." & vbNewLine & vbNewLine &
                                msg,
                                MessageBoxButtons.OK)
    End Sub

    Public Sub RunwithFilter(ByVal procfilter As String)
        'Application.Run(_frmMain)
        _frmMain.filterProcessList(procfilter)
    End Sub
End Module
