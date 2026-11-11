<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Tasks", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("System")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("[System process]", New System.Windows.Forms.TreeNode() {TreeNode1})
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Processes", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Tasks", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Items", 1, 1)
        Dim ListViewGroup7 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Services", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup8 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search result", System.Windows.Forms.HorizontalAlignment.Left)
        Dim CConnection1 As cConnection = New cConnection()
        Dim CConnection2 As cConnection = New cConnection()
        Dim CConnection3 As cConnection = New cConnection()
        Dim ListViewGroup9 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Results", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup10 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Search results", System.Windows.Forms.HorizontalAlignment.Left)
        Me.timerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.imgServices = New System.Windows.Forms.ImageList(Me.components)
        Me.timerServices = New System.Windows.Forms.Timer(Me.components)
        Me.Tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.EnableServiceRefreshingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.saveDial = New System.Windows.Forms.SaveFileDialog()
        Me.openDial = New System.Windows.Forms.OpenFileDialog()
        Me.imgMonitor = New System.Windows.Forms.ImageList(Me.components)
        Me.timerMonitoring = New System.Windows.Forms.Timer(Me.components)
        Me.FolderChooser = New System.Windows.Forms.FolderBrowserDialog()
        Me.timerTask = New System.Windows.Forms.Timer(Me.components)
        Me.timerTrayIcon = New System.Windows.Forms.Timer(Me.components)
        Me._main = New System.Windows.Forms.SplitContainer()
        Me.containerSystemMenu = New System.Windows.Forms.SplitContainer()
        Me._tab = New System.Windows.Forms.TabControl()
        Me.pageTasks = New System.Windows.Forms.TabPage()
        Me.panelMain13 = New System.Windows.Forms.Panel()
        Me.SplitContainerTask = New System.Windows.Forms.SplitContainer()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblTaskCountResult = New System.Windows.Forms.Label()
        Me.txtSearchTask = New System.Windows.Forms.TextBox()
        Me.lvTask = New mainTaskList()
        Me.ColumnHeader62 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader63 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader64 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pageProcesses = New System.Windows.Forms.TabPage()
        Me.containerProcessPage = New System.Windows.Forms.SplitContainer()
        Me.panelMenu = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblResCount = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.panelMain = New System.Windows.Forms.Panel()
        Me.SplitContainerProcess = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerTvLv = New System.Windows.Forms.SplitContainer()
        Me.tvProc = New System.Windows.Forms.TreeView()
        Me.lvProcess = New mainProcessList()
        Me.c1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pageJobs = New System.Windows.Forms.TabPage()
        Me.panelMain12 = New System.Windows.Forms.Panel()
        Me.lvJob = New mainJobList()
        Me.ColumnHeader50 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pageMonitor = New System.Windows.Forms.TabPage()
        Me.panelMainMonitoring = New System.Windows.Forms.Panel()
        Me.splitMonitor = New System.Windows.Forms.SplitContainer()
        Me.tvMonitor = New System.Windows.Forms.TreeView()
        Me.splitMonitor2 = New System.Windows.Forms.SplitContainer()
        Me.txtMonitoringLog = New System.Windows.Forms.TextBox()
        Me.lvMonitorReport = New DoubleBufferedLV()
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader47 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader48 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader49 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.splitMonitor3 = New System.Windows.Forms.SplitContainer()
        Me.splitMonitor4 = New System.Windows.Forms.SplitContainer()
        Me.graphMonitor = New Graph()
        Me.txtMonitorNumber = New System.Windows.Forms.TextBox()
        Me.lblMonitorMaxNumber = New System.Windows.Forms.Label()
        Me.chkMonitorRightAuto = New System.Windows.Forms.CheckBox()
        Me.chkMonitorLeftAuto = New System.Windows.Forms.CheckBox()
        Me.dtMonitorR = New System.Windows.Forms.DateTimePicker()
        Me.dtMonitorL = New System.Windows.Forms.DateTimePicker()
        Me.pageServices = New System.Windows.Forms.TabPage()
        Me.containerServicesPage = New System.Windows.Forms.SplitContainer()
        Me.panelMenu2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblResCount2 = New System.Windows.Forms.Label()
        Me.txtServiceSearch = New System.Windows.Forms.TextBox()
        Me.panelMain2 = New System.Windows.Forms.Panel()
        Me.splitServices = New System.Windows.Forms.SplitContainer()
        Me.lvServices = New mainServiceList()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.splitServices2 = New System.Windows.Forms.SplitContainer()
        Me.cmdCopyServiceToCp = New System.Windows.Forms.Button()
        Me.lblServicePath = New System.Windows.Forms.TextBox()
        Me.lblServiceName = New System.Windows.Forms.Label()
        Me.splitServices3 = New System.Windows.Forms.SplitContainer()
        Me.rtb2 = New System.Windows.Forms.RichTextBox()
        Me.splitServices4 = New System.Windows.Forms.SplitContainer()
        Me.tv2 = New serviceDependenciesList()
        Me.tv = New serviceDependenciesList()
        Me.pageNetwork = New System.Windows.Forms.TabPage()
        Me.panelMain14 = New System.Windows.Forms.Panel()
        Me.lvNetwork = New mainNetworkConnectionsList()
        Me.ColumnHeader66 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader67 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader68 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader69 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pageFile = New System.Windows.Forms.TabPage()
        Me.panelMain5 = New System.Windows.Forms.Panel()
        Me.SplitContainerFilexx = New System.Windows.Forms.SplitContainer()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.cmdFileClipboard = New System.Windows.Forms.Button()
        Me.pctFileSmall = New System.Windows.Forms.PictureBox()
        Me.pctFileBig = New System.Windows.Forms.PictureBox()
        Me.SplitContainerFile = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerFile2 = New System.Windows.Forms.SplitContainer()
        Me.rtb3 = New System.Windows.Forms.RichTextBox()
        Me.gpFileAttributes = New System.Windows.Forms.GroupBox()
        Me.chkFileEncrypted = New System.Windows.Forms.CheckBox()
        Me.chkFileContentNotIndexed = New System.Windows.Forms.CheckBox()
        Me.chkFileNormal = New System.Windows.Forms.CheckBox()
        Me.chkFileSystem = New System.Windows.Forms.CheckBox()
        Me.chkFileReadOnly = New System.Windows.Forms.CheckBox()
        Me.chkFileHidden = New System.Windows.Forms.CheckBox()
        Me.chkFileCompressed = New System.Windows.Forms.CheckBox()
        Me.chkFileArchive = New System.Windows.Forms.CheckBox()
        Me.gpFileDates = New System.Windows.Forms.GroupBox()
        Me.cmdSetFileDates = New System.Windows.Forms.Button()
        Me.DTlastModification = New System.Windows.Forms.DateTimePicker()
        Me.DTlastAccess = New System.Windows.Forms.DateTimePicker()
        Me.DTcreation = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lvFileString = New DoubleBufferedLV()
        Me.headerString = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pageSearch = New System.Windows.Forms.TabPage()
        Me.panelMain6 = New System.Windows.Forms.Panel()
        Me.SplitContainerSearch = New System.Windows.Forms.SplitContainer()
        Me.chkSearchEnvVar = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblResultsCount = New System.Windows.Forms.Label()
        Me.txtSearchResults = New System.Windows.Forms.TextBox()
        Me.chkSearchWindows = New System.Windows.Forms.CheckBox()
        Me.chkSearchHandles = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkSearchModules = New System.Windows.Forms.CheckBox()
        Me.chkSearchServices = New System.Windows.Forms.CheckBox()
        Me.chkSearchProcess = New System.Windows.Forms.CheckBox()
        Me.chkSearchCase = New System.Windows.Forms.CheckBox()
        Me.lvSearchResults = New searchList()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pageHelp = New System.Windows.Forms.TabPage()
        Me.panelMain4 = New System.Windows.Forms.Panel()
        Me.timerNetwork = New System.Windows.Forms.Timer(Me.components)
        Me.MenuItemTaskShow = New System.Windows.Forms.MenuItem()
        Me.MenuItemTaskEnd = New System.Windows.Forms.MenuItem()
        Me.MenuItemTaskSelProc = New System.Windows.Forms.MenuItem()
        Me.MenuItemMonitorAdd = New System.Windows.Forms.MenuItem()
        Me.MenuItemMonitorRemove = New System.Windows.Forms.MenuItem()
        Me.MenuItemMonitorStart = New System.Windows.Forms.MenuItem()
        Me.MenuItemMonitorStop = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopyBig = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopySmall = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainShow = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainToTray = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainAbout = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainLog = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainOpenedW = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainExit = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainSysInfo = New System.Windows.Forms.MenuItem()
        Me.MenuItemServSelService = New System.Windows.Forms.MenuItem()
        Me.MenuItemServFileProp = New System.Windows.Forms.MenuItem()
        Me.MenuItemServOpenDir = New System.Windows.Forms.MenuItem()
        Me.MenuItemServFileDetails = New System.Windows.Forms.MenuItem()
        Me.MenuItemServSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItemServPause = New System.Windows.Forms.MenuItem()
        Me.MenuItemServStop = New System.Windows.Forms.MenuItem()
        Me.MenuItemServStart = New System.Windows.Forms.MenuItem()
        Me.MenuItemServAutoStart = New System.Windows.Forms.MenuItem()
        Me.MenuItemServOnDemand = New System.Windows.Forms.MenuItem()
        Me.MenuItemServDisabled = New System.Windows.Forms.MenuItem()
        Me.MenuItemServDepe = New System.Windows.Forms.MenuItem()
        Me.MenuItemNetworkClose = New System.Windows.Forms.MenuItem()
        Me.MenuItemServSelProc = New System.Windows.Forms.MenuItem()
        Me.MenuItemSearchSel = New System.Windows.Forms.MenuItem()
        Me.MenuItemSearchClose = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcSFileProp = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcSOpenDir = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcSFileDetails = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcSSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcSDep = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcKill = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcKillT = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcStop = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcResume = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPIdle = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPBN = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPN = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPAN = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPH = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPRT = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemRefresh = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemLog = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemInfos = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemOpenedWindows = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemToTray = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemExit = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemUpdate = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemWebsite = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemAbout = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemFindWindow = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemHelp = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemOptions = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainFindWindow = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopyTask = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopyService = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopyNetwork = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopySearch = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopyProcess = New System.Windows.Forms.MenuItem()
        Me.mnuTask = New System.Windows.Forms.ContextMenu()
        Me.MenuItemTaskMax = New System.Windows.Forms.MenuItem()
        Me.MenuItemTaskMin = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItemTaskSelectWindow = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MenuItemTaskColumns = New System.Windows.Forms.MenuItem()
        Me.mnuMonitor = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnuFileCpPctBig = New System.Windows.Forms.ContextMenu()
        Me.mnuFileCpPctSmall = New System.Windows.Forms.ContextMenu()
        Me.mnuMain = New System.Windows.Forms.ContextMenu()
        Me.MenuItemMainAlwaysVisible = New System.Windows.Forms.MenuItem()
        Me.MenuItem29 = New System.Windows.Forms.MenuItem()
        Me.MenuItem31 = New System.Windows.Forms.MenuItem()
        Me.MenuItemNotifNP = New System.Windows.Forms.MenuItem()
        Me.MenuItemNotifTP = New System.Windows.Forms.MenuItem()
        Me.MenuItemNotifNS = New System.Windows.Forms.MenuItem()
        Me.MenuItemNotifDS = New System.Windows.Forms.MenuItem()
        Me.MenuItem46 = New System.Windows.Forms.MenuItem()
        Me.MenuItemNotifAll = New System.Windows.Forms.MenuItem()
        Me.MenuItemNotifNone = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainRestart = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainShutdown = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainPowerOff = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainSleep = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainHibernate = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainLogOff = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainLock = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainReport = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainNetworkInfo = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainEmergencyH = New System.Windows.Forms.MenuItem()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.MenuItemRefProc = New System.Windows.Forms.MenuItem()
        Me.MenuItemMainRefServ = New System.Windows.Forms.MenuItem()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.mnuService = New System.Windows.Forms.ContextMenu()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.MenuItemServStartType = New System.Windows.Forms.MenuItem()
        Me.MenuItemServDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.MenuItemServReanalize = New System.Windows.Forms.MenuItem()
        Me.MenuItem24 = New System.Windows.Forms.MenuItem()
        Me.MenuItemServColumns = New System.Windows.Forms.MenuItem()
        Me.mnuNetwork = New System.Windows.Forms.ContextMenu()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.MenuItemNetworkTools = New System.Windows.Forms.MenuItem()
        Me.MenuItemNetworkPing = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MenuItemNetworkColumns = New System.Windows.Forms.MenuItem()
        Me.mnuSearch = New System.Windows.Forms.ContextMenu()
        Me.MenuItemSearchNew = New System.Windows.Forms.MenuItem()
        Me.MenuItem28 = New System.Windows.Forms.MenuItem()
        Me.MenuItem30 = New System.Windows.Forms.MenuItem()
        Me.mnuProcess = New System.Windows.Forms.ContextMenu()
        Me.MenuItemProcKillByMethod = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcPriority = New System.Windows.Forms.MenuItem()
        Me.MenuItem44 = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcReanalize = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcOther = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcWSS = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcAff = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcDump = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcJob = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcAddToJob = New System.Windows.Forms.MenuItem()
        Me.MenuItem33 = New System.Windows.Forms.MenuItem()
        Me.MenuItemJobMng = New System.Windows.Forms.MenuItem()
        Me.MenuItem51 = New System.Windows.Forms.MenuItem()
        Me.MenuItem38 = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcColumns = New System.Windows.Forms.MenuItem()
        Me.mnuSystem = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItemSYSTEMFILE = New System.Windows.Forms.MenuItem()
        Me.MenuItem54 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemConnection = New System.Windows.Forms.MenuItem()
        Me.MenuItemRunAsAdmin = New System.Windows.Forms.MenuItem()
        Me.MenuItem56 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemReport = New System.Windows.Forms.MenuItem()
        Me.MenuItem59 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemNetworkInfos = New System.Windows.Forms.MenuItem()
        Me.MenuItemShowPendingTasks = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemScripting = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemSaveSSFile = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemExploreSSFile = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemCheckSignatures = New System.Windows.Forms.MenuItem()
        Me.MenuItem62 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemEmergency = New System.Windows.Forms.MenuItem()
        Me.MenuItem66 = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcesses = New System.Windows.Forms.MenuItem()
        Me.MenuItemReportProcesses = New System.Windows.Forms.MenuItem()
        Me.MenuItemJobs = New System.Windows.Forms.MenuItem()
        Me.MenuItemMonitor = New System.Windows.Forms.MenuItem()
        Me.MenuItemReportMonitor = New System.Windows.Forms.MenuItem()
        Me.MenuItemServices = New System.Windows.Forms.MenuItem()
        Me.MenuItemReportServices = New System.Windows.Forms.MenuItem()
        Me.MenuItemSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItemNewSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItem61 = New System.Windows.Forms.MenuItem()
        Me.MenuItemReportSearch = New System.Windows.Forms.MenuItem()
        Me.MenuItemFiles = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileOpen = New System.Windows.Forms.MenuItem()
        Me.MenuItem68 = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileRelease = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileDelete = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileTrash = New System.Windows.Forms.MenuItem()
        Me.MenuItem72 = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileRename = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileShellOpen = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileMove = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileCopy = New System.Windows.Forms.MenuItem()
        Me.MenuItem77 = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileEncrypt = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileDecrypt = New System.Windows.Forms.MenuItem()
        Me.MenuItem80 = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileStrings = New System.Windows.Forms.MenuItem()
        Me.MenuItemSYSTEMOPT = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemAlwaysVisible = New System.Windows.Forms.MenuItem()
        Me.MenuItem37 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemRefProc = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemRefServ = New System.Windows.Forms.MenuItem()
        Me.MenuItem42 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSYSTEMTOOLS = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemShowHidden = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemDependency = New System.Windows.Forms.MenuItem()
        Me.MenuItemCreateService = New System.Windows.Forms.MenuItem()
        Me.MenuItemSYSTEMSYSTEM = New System.Windows.Forms.MenuItem()
        Me.MenuItem34 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemRestart = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemShutdown = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemPowerOff = New System.Windows.Forms.MenuItem()
        Me.MenuItem40 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemSleep = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemHIbernate = New System.Windows.Forms.MenuItem()
        Me.MenuItem43 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemLogoff = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemLock = New System.Windows.Forms.MenuItem()
        Me.MenuItemSYSTEMHEL = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemNewProcess = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcNewProcess = New System.Windows.Forms.MenuItem()
        Me.MenuItemProcSep1 = New System.Windows.Forms.MenuItem()
        Me.MenuItemHelpAbout = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileSep2 = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileProperties = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileOpenDir = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileDirProperties = New System.Windows.Forms.MenuItem()
        Me.MenuItemFileSearchInternet = New System.Windows.Forms.MenuItem()
        Me.MenuItem39 = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemDonation = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemFeedBack = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemSF = New System.Windows.Forms.MenuItem()
        Me.MenuItemSystemDownloads = New System.Windows.Forms.MenuItem()
        Me.MenuItem49 = New System.Windows.Forms.MenuItem()
        Me.MenuItemJobTerminate = New System.Windows.Forms.MenuItem()
        Me.MenuItemCopyJob = New System.Windows.Forms.MenuItem()
        Me.StatusBar = New System.Windows.Forms.StatusBar()
        Me.sbPanelConnection = New System.Windows.Forms.StatusBarPanel()
        Me.sbPanelProcesses = New System.Windows.Forms.StatusBarPanel()
        Me.sbPanelServices = New System.Windows.Forms.StatusBarPanel()
        Me.sbPanelCpu = New System.Windows.Forms.StatusBarPanel()
        Me.sbPanelMemory = New System.Windows.Forms.StatusBarPanel()
        Me.timerStatus = New System.Windows.Forms.Timer(Me.components)
        Me.timerJobs = New System.Windows.Forms.Timer(Me.components)
        Me.mnuJob = New System.Windows.Forms.ContextMenu()
        Me.MenuItem53 = New System.Windows.Forms.MenuItem()
        CType(Me._main, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._main.Panel1.SuspendLayout()
        Me._main.Panel2.SuspendLayout()
        Me._main.SuspendLayout()
        CType(Me.containerSystemMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.containerSystemMenu.Panel2.SuspendLayout()
        Me.containerSystemMenu.SuspendLayout()
        Me._tab.SuspendLayout()
        Me.pageTasks.SuspendLayout()
        Me.panelMain13.SuspendLayout()
        CType(Me.SplitContainerTask, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTask.Panel1.SuspendLayout()
        Me.SplitContainerTask.Panel2.SuspendLayout()
        Me.SplitContainerTask.SuspendLayout()
        Me.pageProcesses.SuspendLayout()
        CType(Me.containerProcessPage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.containerProcessPage.Panel1.SuspendLayout()
        Me.containerProcessPage.Panel2.SuspendLayout()
        Me.containerProcessPage.SuspendLayout()
        Me.panelMenu.SuspendLayout()
        Me.panelMain.SuspendLayout()
        CType(Me.SplitContainerProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerProcess.Panel1.SuspendLayout()
        Me.SplitContainerProcess.SuspendLayout()
        CType(Me.SplitContainerTvLv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTvLv.Panel1.SuspendLayout()
        Me.SplitContainerTvLv.Panel2.SuspendLayout()
        Me.SplitContainerTvLv.SuspendLayout()
        Me.pageJobs.SuspendLayout()
        Me.panelMain12.SuspendLayout()
        Me.pageMonitor.SuspendLayout()
        Me.panelMainMonitoring.SuspendLayout()
        CType(Me.splitMonitor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMonitor.Panel1.SuspendLayout()
        Me.splitMonitor.Panel2.SuspendLayout()
        Me.splitMonitor.SuspendLayout()
        CType(Me.splitMonitor2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMonitor2.Panel1.SuspendLayout()
        Me.splitMonitor2.Panel2.SuspendLayout()
        Me.splitMonitor2.SuspendLayout()
        CType(Me.splitMonitor3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMonitor3.Panel1.SuspendLayout()
        Me.splitMonitor3.Panel2.SuspendLayout()
        Me.splitMonitor3.SuspendLayout()
        CType(Me.splitMonitor4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMonitor4.Panel2.SuspendLayout()
        Me.splitMonitor4.SuspendLayout()
        CType(Me.graphMonitor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pageServices.SuspendLayout()
        CType(Me.containerServicesPage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.containerServicesPage.Panel1.SuspendLayout()
        Me.containerServicesPage.Panel2.SuspendLayout()
        Me.containerServicesPage.SuspendLayout()
        Me.panelMenu2.SuspendLayout()
        Me.panelMain2.SuspendLayout()
        CType(Me.splitServices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitServices.Panel1.SuspendLayout()
        Me.splitServices.Panel2.SuspendLayout()
        Me.splitServices.SuspendLayout()
        CType(Me.splitServices2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitServices2.Panel1.SuspendLayout()
        Me.splitServices2.Panel2.SuspendLayout()
        Me.splitServices2.SuspendLayout()
        CType(Me.splitServices3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitServices3.Panel1.SuspendLayout()
        Me.splitServices3.Panel2.SuspendLayout()
        Me.splitServices3.SuspendLayout()
        CType(Me.splitServices4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitServices4.Panel1.SuspendLayout()
        Me.splitServices4.Panel2.SuspendLayout()
        Me.splitServices4.SuspendLayout()
        Me.pageNetwork.SuspendLayout()
        Me.panelMain14.SuspendLayout()
        Me.pageFile.SuspendLayout()
        Me.panelMain5.SuspendLayout()
        CType(Me.SplitContainerFilexx, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerFilexx.Panel1.SuspendLayout()
        Me.SplitContainerFilexx.Panel2.SuspendLayout()
        Me.SplitContainerFilexx.SuspendLayout()
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerFile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerFile.Panel1.SuspendLayout()
        Me.SplitContainerFile.Panel2.SuspendLayout()
        Me.SplitContainerFile.SuspendLayout()
        CType(Me.SplitContainerFile2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerFile2.Panel1.SuspendLayout()
        Me.SplitContainerFile2.Panel2.SuspendLayout()
        Me.SplitContainerFile2.SuspendLayout()
        Me.gpFileAttributes.SuspendLayout()
        Me.gpFileDates.SuspendLayout()
        Me.pageSearch.SuspendLayout()
        Me.panelMain6.SuspendLayout()
        CType(Me.SplitContainerSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerSearch.Panel1.SuspendLayout()
        Me.SplitContainerSearch.Panel2.SuspendLayout()
        Me.SplitContainerSearch.SuspendLayout()
        Me.pageHelp.SuspendLayout()
        CType(Me.sbPanelConnection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelProcesses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelServices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelCpu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbPanelMemory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'timerProcess
        '
        Me.timerProcess.Interval = 1000
        '
        'imgServices
        '
        Me.imgServices.ImageStream = CType(resources.GetObject("imgServices.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgServices.TransparentColor = System.Drawing.Color.Transparent
        Me.imgServices.Images.SetKeyName(0, "ok")
        Me.imgServices.Images.SetKeyName(1, "ko")
        Me.imgServices.Images.SetKeyName(2, "key")
        Me.imgServices.Images.SetKeyName(3, "thread")
        Me.imgServices.Images.SetKeyName(4, "noicon")
        Me.imgServices.Images.SetKeyName(5, "service")
        '
        'timerServices
        '
        Me.timerServices.Interval = 6000
        '
        'Tray
        '
        Me.Tray.Icon = CType(resources.GetObject("Tray.Icon"), System.Drawing.Icon)
        Me.Tray.Text = "Lite Process Monitor"
        '
        'EnableServiceRefreshingToolStripMenuItem
        '
        Me.EnableServiceRefreshingToolStripMenuItem.Checked = True
        Me.EnableServiceRefreshingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EnableServiceRefreshingToolStripMenuItem.Name = "EnableServiceRefreshingToolStripMenuItem"
        Me.EnableServiceRefreshingToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.EnableServiceRefreshingToolStripMenuItem.Text = "Enable service refreshing"
        '
        'saveDial
        '
        Me.saveDial.Filter = "HTML File (*.html)|*.html|Text file (*.txt)|*.txt"
        Me.saveDial.SupportMultiDottedExtensions = True
        '
        'openDial
        '
        Me.openDial.CheckFileExists = False
        Me.openDial.CheckPathExists = False
        Me.openDial.SupportMultiDottedExtensions = True
        '
        'imgMonitor
        '
        Me.imgMonitor.ImageStream = CType(resources.GetObject("imgMonitor.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMonitor.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMonitor.Images.SetKeyName(0, "down")
        Me.imgMonitor.Images.SetKeyName(1, "sub")
        Me.imgMonitor.Images.SetKeyName(2, "exe")
        '
        'timerMonitoring
        '
        Me.timerMonitoring.Interval = 1000
        '
        'timerTask
        '
        Me.timerTask.Interval = 1000
        '
        'timerTrayIcon
        '
        Me.timerTrayIcon.Interval = 1000
        '
        '_main
        '
        Me._main.Dock = System.Windows.Forms.DockStyle.Fill
        Me._main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me._main.IsSplitterFixed = True
        Me._main.Location = New System.Drawing.Point(0, 0)
        Me._main.Name = "_main"
        Me._main.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        '_main.Panel1
        '
        '_main.Panel2
        '
        Me._main.Panel2.Controls.Add(Me.containerSystemMenu)
        Me._main.Panel1Collapsed = True
        Me._main.Size = New System.Drawing.Size(1708, 1089)
        Me._main.SplitterDistance = 138
        Me._main.TabIndex = 57
        '
        'containerSystemMenu
        '
        Me.containerSystemMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerSystemMenu.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerSystemMenu.IsSplitterFixed = True
        Me.containerSystemMenu.Location = New System.Drawing.Point(0, 0)
        Me.containerSystemMenu.Name = "containerSystemMenu"
        Me.containerSystemMenu.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.containerSystemMenu.Panel1Collapsed = True
        '
        'containerSystemMenu.Panel2
        '
        Me.containerSystemMenu.Panel2.Controls.Add(Me._tab)
        Me.containerSystemMenu.Size = New System.Drawing.Size(1708, 947)
        Me.containerSystemMenu.SplitterDistance = 25
        Me.containerSystemMenu.TabIndex = 0
        '
        '_tab
        '
        Me._tab.Controls.Add(Me.pageTasks)
        Me._tab.Controls.Add(Me.pageProcesses)
        Me._tab.Controls.Add(Me.pageJobs)
        Me._tab.Controls.Add(Me.pageMonitor)
        Me._tab.Controls.Add(Me.pageServices)
        Me._tab.Controls.Add(Me.pageNetwork)
        Me._tab.Controls.Add(Me.pageFile)
        Me._tab.Controls.Add(Me.pageSearch)
        Me._tab.Controls.Add(Me.pageHelp)
        Me._tab.Dock = System.Windows.Forms.DockStyle.Fill
        Me._tab.Location = New System.Drawing.Point(0, 0)
        Me._tab.Name = "_tab"
        Me._tab.SelectedIndex = 0
        Me._tab.Size = New System.Drawing.Size(1708, 947)
        Me._tab.TabIndex = 3
        '
        'pageTasks
        '
        Me.pageTasks.BackColor = System.Drawing.Color.Transparent
        Me.pageTasks.Controls.Add(Me.panelMain13)
        Me.pageTasks.Location = New System.Drawing.Point(4, 28)
        Me.pageTasks.Name = "pageTasks"
        Me.pageTasks.Padding = New System.Windows.Forms.Padding(3)
        Me.pageTasks.Size = New System.Drawing.Size(1700, 915)
        Me.pageTasks.TabIndex = 11
        Me.pageTasks.Text = "Tasks"
        Me.pageTasks.UseVisualStyleBackColor = True
        '
        'panelMain13
        '
        Me.panelMain13.Controls.Add(Me.SplitContainerTask)
        Me.panelMain13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain13.Location = New System.Drawing.Point(3, 3)
        Me.panelMain13.Name = "panelMain13"
        Me.panelMain13.Size = New System.Drawing.Size(1694, 909)
        Me.panelMain13.TabIndex = 56
        '
        'SplitContainerTask
        '
        Me.SplitContainerTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTask.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerTask.IsSplitterFixed = True
        Me.SplitContainerTask.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerTask.Name = "SplitContainerTask"
        Me.SplitContainerTask.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerTask.Panel1
        '
        Me.SplitContainerTask.Panel1.Controls.Add(Me.Label19)
        Me.SplitContainerTask.Panel1.Controls.Add(Me.lblTaskCountResult)
        Me.SplitContainerTask.Panel1.Controls.Add(Me.txtSearchTask)
        '
        'SplitContainerTask.Panel2
        '
        Me.SplitContainerTask.Panel2.Controls.Add(Me.lvTask)
        Me.SplitContainerTask.Size = New System.Drawing.Size(1694, 909)
        Me.SplitContainerTask.SplitterDistance = 25
        Me.SplitContainerTask.TabIndex = 0
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 6)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(78, 19)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "Search task"
        '
        'lblTaskCountResult
        '
        Me.lblTaskCountResult.AutoSize = True
        Me.lblTaskCountResult.Location = New System.Drawing.Point(393, 6)
        Me.lblTaskCountResult.Name = "lblTaskCountResult"
        Me.lblTaskCountResult.Size = New System.Drawing.Size(69, 19)
        Me.lblTaskCountResult.TabIndex = 12
        Me.lblTaskCountResult.Text = "0 result(s)"
        '
        'txtSearchTask
        '
        Me.txtSearchTask.Location = New System.Drawing.Point(72, 1)
        Me.txtSearchTask.Name = "txtSearchTask"
        Me.txtSearchTask.Size = New System.Drawing.Size(312, 26)
        Me.txtSearchTask.TabIndex = 11
        '
        'lvTask
        '
        Me.lvTask.AllowColumnReorder = True
        Me.lvTask.CatchErrors = False
        Me.lvTask.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader62, Me.ColumnHeader63, Me.ColumnHeader64})
        Me.lvTask.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvTask.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvTask.FullRowSelect = True
        ListViewGroup1.Header = "Tasks"
        ListViewGroup1.Name = "gpOther"
        ListViewGroup2.Header = "Search result"
        ListViewGroup2.Name = "gpSearch"
        Me.lvTask.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.lvTask.HideSelection = False
        Me.lvTask.InstanceId = 1
        Me.lvTask.IsConnected = False
        Me.lvTask.Location = New System.Drawing.Point(0, 0)
        Me.lvTask.Name = "lvTask"
        Me.lvTask.OverriddenDoubleBuffered = True
        Me.lvTask.ReorganizeColumns = True
        Me.lvTask.ShowItemToolTips = True
        Me.lvTask.ShowObjectDetailsOnDoubleClick = False
        Me.lvTask.Size = New System.Drawing.Size(1694, 880)
        Me.lvTask.TabIndex = 3
        Me.lvTask.UseCompatibleStateImageBehavior = False
        Me.lvTask.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader62
        '
        Me.ColumnHeader62.Text = "Name"
        Me.ColumnHeader62.Width = 400
        '
        'ColumnHeader63
        '
        Me.ColumnHeader63.Text = "CpuUsage"
        Me.ColumnHeader63.Width = 170
        '
        'ColumnHeader64
        '
        Me.ColumnHeader64.Text = "Process"
        Me.ColumnHeader64.Width = 170
        '
        'pageProcesses
        '
        Me.pageProcesses.BackColor = System.Drawing.Color.Transparent
        Me.pageProcesses.Controls.Add(Me.containerProcessPage)
        Me.pageProcesses.Location = New System.Drawing.Point(4, 28)
        Me.pageProcesses.Name = "pageProcesses"
        Me.pageProcesses.Padding = New System.Windows.Forms.Padding(3)
        Me.pageProcesses.Size = New System.Drawing.Size(1700, 915)
        Me.pageProcesses.TabIndex = 0
        Me.pageProcesses.Text = "Processes"
        Me.pageProcesses.UseVisualStyleBackColor = True
        '
        'containerProcessPage
        '
        Me.containerProcessPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerProcessPage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerProcessPage.IsSplitterFixed = True
        Me.containerProcessPage.Location = New System.Drawing.Point(3, 3)
        Me.containerProcessPage.Name = "containerProcessPage"
        Me.containerProcessPage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'containerProcessPage.Panel1
        '
        Me.containerProcessPage.Panel1.Controls.Add(Me.panelMenu)
        '
        'containerProcessPage.Panel2
        '
        Me.containerProcessPage.Panel2.Controls.Add(Me.panelMain)
        Me.containerProcessPage.Size = New System.Drawing.Size(1694, 909)
        Me.containerProcessPage.SplitterDistance = 25
        Me.containerProcessPage.TabIndex = 0
        '
        'panelMenu
        '
        Me.panelMenu.Controls.Add(Me.Label3)
        Me.panelMenu.Controls.Add(Me.lblResCount)
        Me.panelMenu.Controls.Add(Me.txtSearch)
        Me.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMenu.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu.Name = "panelMenu"
        Me.panelMenu.Size = New System.Drawing.Size(1694, 25)
        Me.panelMenu.TabIndex = 48
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Search process"
        '
        'lblResCount
        '
        Me.lblResCount.AutoSize = True
        Me.lblResCount.Location = New System.Drawing.Point(394, 6)
        Me.lblResCount.Name = "lblResCount"
        Me.lblResCount.Size = New System.Drawing.Size(69, 19)
        Me.lblResCount.TabIndex = 2
        Me.lblResCount.Text = "0 result(s)"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(90, 1)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(298, 26)
        Me.txtSearch.TabIndex = 1
        '
        'panelMain
        '
        Me.panelMain.Controls.Add(Me.SplitContainerProcess)
        Me.panelMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain.Location = New System.Drawing.Point(0, 0)
        Me.panelMain.Name = "panelMain"
        Me.panelMain.Size = New System.Drawing.Size(1694, 880)
        Me.panelMain.TabIndex = 4
        '
        'SplitContainerProcess
        '
        Me.SplitContainerProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerProcess.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerProcess.IsSplitterFixed = True
        Me.SplitContainerProcess.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerProcess.Name = "SplitContainerProcess"
        Me.SplitContainerProcess.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerProcess.Panel1
        '
        Me.SplitContainerProcess.Panel1.Controls.Add(Me.SplitContainerTvLv)
        Me.SplitContainerProcess.Panel2Collapsed = True
        Me.SplitContainerProcess.Size = New System.Drawing.Size(1694, 880)
        Me.SplitContainerProcess.SplitterDistance = 285
        Me.SplitContainerProcess.TabIndex = 0
        '
        'SplitContainerTvLv
        '
        Me.SplitContainerTvLv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTvLv.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerTvLv.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerTvLv.Name = "SplitContainerTvLv"
        '
        'SplitContainerTvLv.Panel1
        '
        Me.SplitContainerTvLv.Panel1.Controls.Add(Me.tvProc)
        Me.SplitContainerTvLv.Panel1Collapsed = True
        '
        'SplitContainerTvLv.Panel2
        '
        Me.SplitContainerTvLv.Panel2.Controls.Add(Me.lvProcess)
        Me.SplitContainerTvLv.Size = New System.Drawing.Size(1694, 880)
        Me.SplitContainerTvLv.SplitterDistance = 149
        Me.SplitContainerTvLv.TabIndex = 4
        '
        'tvProc
        '
        Me.tvProc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvProc.FullRowSelect = True
        Me.tvProc.Location = New System.Drawing.Point(0, 0)
        Me.tvProc.Name = "tvProc"
        TreeNode1.Name = "4"
        TreeNode1.Tag = "4"
        TreeNode1.Text = "System"
        TreeNode2.Name = "0"
        TreeNode2.Tag = "0"
        TreeNode2.Text = "[System process]"
        Me.tvProc.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2})
        Me.tvProc.Size = New System.Drawing.Size(149, 100)
        Me.tvProc.TabIndex = 4
        '
        'lvProcess
        '
        Me.lvProcess.AllowColumnReorder = True
        Me.lvProcess.CatchErrors = False
        Me.lvProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5, Me.c7, Me.c8, Me.c9, Me.c10, Me.ColumnHeader20})
        Me.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvProcess.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvProcess.FullRowSelect = True
        ListViewGroup3.Header = "Processes"
        ListViewGroup3.Name = "gpOther"
        ListViewGroup4.Header = "Search result"
        ListViewGroup4.Name = "gpSearch"
        Me.lvProcess.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup3, ListViewGroup4})
        Me.lvProcess.HideSelection = False
        Me.lvProcess.InstanceId = 2
        Me.lvProcess.IsConnected = False
        Me.lvProcess.Location = New System.Drawing.Point(0, 0)
        Me.lvProcess.Name = "lvProcess"
        Me.lvProcess.OverriddenDoubleBuffered = True
        Me.lvProcess.ReorganizeColumns = True
        Me.lvProcess.ShowItemToolTips = True
        Me.lvProcess.ShowObjectDetailsOnDoubleClick = False
        Me.lvProcess.Size = New System.Drawing.Size(1694, 880)
        Me.lvProcess.TabIndex = 3
        Me.lvProcess.UseCompatibleStateImageBehavior = False
        Me.lvProcess.View = System.Windows.Forms.View.Details
        '
        'c1
        '
        Me.c1.Text = "Name"
        Me.c1.Width = 100
        '
        'c2
        '
        Me.c2.Text = "PID"
        Me.c2.Width = 40
        '
        'c3
        '
        Me.c3.Text = "UserName"
        Me.c3.Width = 100
        '
        'c4
        '
        Me.c4.DisplayIndex = 5
        Me.c4.Text = "TotalCpuTime"
        Me.c4.Width = 80
        '
        'c5
        '
        Me.c5.DisplayIndex = 6
        Me.c5.Text = "WorkingSet"
        Me.c5.Width = 80
        '
        'c7
        '
        Me.c7.DisplayIndex = 7
        Me.c7.Text = "Priority"
        Me.c7.Width = 70
        '
        'c8
        '
        Me.c8.DisplayIndex = 8
        Me.c8.Text = "Path"
        Me.c8.Width = 350
        '
        'c9
        '
        Me.c9.DisplayIndex = 9
        Me.c9.Text = "StartTime"
        Me.c9.Width = 250
        '
        'c10
        '
        Me.c10.DisplayIndex = 3
        Me.c10.Text = "CpuUsage"
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.DisplayIndex = 4
        Me.ColumnHeader20.Text = "AverageCpuUsage"
        '
        'pageJobs
        '
        Me.pageJobs.BackColor = System.Drawing.Color.Transparent
        Me.pageJobs.Controls.Add(Me.panelMain12)
        Me.pageJobs.Location = New System.Drawing.Point(4, 25)
        Me.pageJobs.Name = "pageJobs"
        Me.pageJobs.Padding = New System.Windows.Forms.Padding(3)
        Me.pageJobs.Size = New System.Drawing.Size(1700, 928)
        Me.pageJobs.TabIndex = 13
        Me.pageJobs.Text = "Jobs"
        Me.pageJobs.UseVisualStyleBackColor = True
        '
        'panelMain12
        '
        Me.panelMain12.Controls.Add(Me.lvJob)
        Me.panelMain12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain12.Location = New System.Drawing.Point(3, 3)
        Me.panelMain12.Name = "panelMain12"
        Me.panelMain12.Size = New System.Drawing.Size(1694, 922)
        Me.panelMain12.TabIndex = 58
        '
        'lvJob
        '
        Me.lvJob.AllowColumnReorder = True
        Me.lvJob.CatchErrors = False
        Me.lvJob.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader50, Me.ColumnHeader5})
        Me.lvJob.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvJob.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvJob.FullRowSelect = True
        ListViewGroup5.Header = "Tasks"
        ListViewGroup5.Name = "gpOther"
        ListViewGroup6.Header = "Search result"
        ListViewGroup6.Name = "gpSearch"
        Me.lvJob.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup5, ListViewGroup6})
        Me.lvJob.HideSelection = False
        Me.lvJob.InstanceId = 3
        Me.lvJob.IsConnected = False
        Me.lvJob.Location = New System.Drawing.Point(0, 0)
        Me.lvJob.Name = "lvJob"
        Me.lvJob.OverriddenDoubleBuffered = True
        Me.lvJob.ReorganizeColumns = True
        Me.lvJob.ShowItemToolTips = True
        Me.lvJob.ShowObjectDetailsOnDoubleClick = False
        Me.lvJob.Size = New System.Drawing.Size(1694, 922)
        Me.lvJob.TabIndex = 10
        Me.lvJob.UseCompatibleStateImageBehavior = False
        Me.lvJob.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader50
        '
        Me.ColumnHeader50.Text = "Name"
        Me.ColumnHeader50.Width = 400
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "ProcessesCount"
        Me.ColumnHeader5.Width = 142
        '
        'pageMonitor
        '
        Me.pageMonitor.BackColor = System.Drawing.Color.Transparent
        Me.pageMonitor.Controls.Add(Me.panelMainMonitoring)
        Me.pageMonitor.Location = New System.Drawing.Point(4, 25)
        Me.pageMonitor.Name = "pageMonitor"
        Me.pageMonitor.Padding = New System.Windows.Forms.Padding(3)
        Me.pageMonitor.Size = New System.Drawing.Size(1700, 928)
        Me.pageMonitor.TabIndex = 7
        Me.pageMonitor.Text = "Monitor"
        Me.pageMonitor.UseVisualStyleBackColor = True
        '
        'panelMainMonitoring
        '
        Me.panelMainMonitoring.Controls.Add(Me.splitMonitor)
        Me.panelMainMonitoring.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMainMonitoring.Location = New System.Drawing.Point(3, 3)
        Me.panelMainMonitoring.Name = "panelMainMonitoring"
        Me.panelMainMonitoring.Size = New System.Drawing.Size(1694, 922)
        Me.panelMainMonitoring.TabIndex = 51
        '
        'splitMonitor
        '
        Me.splitMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor.Name = "splitMonitor"
        '
        'splitMonitor.Panel1
        '
        Me.splitMonitor.Panel1.Controls.Add(Me.tvMonitor)
        '
        'splitMonitor.Panel2
        '
        Me.splitMonitor.Panel2.Controls.Add(Me.splitMonitor2)
        Me.splitMonitor.Size = New System.Drawing.Size(1694, 922)
        Me.splitMonitor.SplitterDistance = 558
        Me.splitMonitor.TabIndex = 0
        '
        'tvMonitor
        '
        Me.tvMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvMonitor.FullRowSelect = True
        Me.tvMonitor.ImageIndex = 0
        Me.tvMonitor.ImageList = Me.imgMonitor
        Me.tvMonitor.Location = New System.Drawing.Point(0, 0)
        Me.tvMonitor.Name = "tvMonitor"
        TreeNode3.ImageIndex = 1
        TreeNode3.Name = "processes"
        TreeNode3.SelectedImageIndex = 1
        TreeNode3.Text = "Items"
        Me.tvMonitor.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode3})
        Me.tvMonitor.SelectedImageIndex = 0
        Me.tvMonitor.Size = New System.Drawing.Size(558, 922)
        Me.tvMonitor.TabIndex = 0
        '
        'splitMonitor2
        '
        Me.splitMonitor2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor2.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor2.Name = "splitMonitor2"
        Me.splitMonitor2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMonitor2.Panel1
        '
        Me.splitMonitor2.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.splitMonitor2.Panel1.Controls.Add(Me.txtMonitoringLog)
        Me.splitMonitor2.Panel1.Controls.Add(Me.lvMonitorReport)
        '
        'splitMonitor2.Panel2
        '
        Me.splitMonitor2.Panel2.Controls.Add(Me.splitMonitor3)
        Me.splitMonitor2.Size = New System.Drawing.Size(1132, 922)
        Me.splitMonitor2.SplitterDistance = 341
        Me.splitMonitor2.TabIndex = 0
        '
        'txtMonitoringLog
        '
        Me.txtMonitoringLog.BackColor = System.Drawing.Color.White
        Me.txtMonitoringLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMonitoringLog.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonitoringLog.Location = New System.Drawing.Point(0, 0)
        Me.txtMonitoringLog.Multiline = True
        Me.txtMonitoringLog.Name = "txtMonitoringLog"
        Me.txtMonitoringLog.ReadOnly = True
        Me.txtMonitoringLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMonitoringLog.Size = New System.Drawing.Size(1132, 341)
        Me.txtMonitoringLog.TabIndex = 0
        Me.txtMonitoringLog.Text = "No process monitored." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click on 'Add' button to monitor a process."
        '
        'lvMonitorReport
        '
        Me.lvMonitorReport.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader22, Me.ColumnHeader2, Me.ColumnHeader23, Me.ColumnHeader47, Me.ColumnHeader48, Me.ColumnHeader49})
        Me.lvMonitorReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvMonitorReport.FullRowSelect = True
        Me.lvMonitorReport.HideSelection = False
        Me.lvMonitorReport.Location = New System.Drawing.Point(0, 0)
        Me.lvMonitorReport.Name = "lvMonitorReport"
        Me.lvMonitorReport.OverriddenDoubleBuffered = False
        Me.lvMonitorReport.ShowItemToolTips = True
        Me.lvMonitorReport.Size = New System.Drawing.Size(1132, 341)
        Me.lvMonitorReport.TabIndex = 1
        Me.lvMonitorReport.UseCompatibleStateImageBehavior = False
        Me.lvMonitorReport.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Counter"
        Me.ColumnHeader22.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "MachineName"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "Creation date"
        Me.ColumnHeader23.Width = 100
        '
        'ColumnHeader47
        '
        Me.ColumnHeader47.Text = "Last start"
        Me.ColumnHeader47.Width = 100
        '
        'ColumnHeader48
        '
        Me.ColumnHeader48.Text = "State"
        '
        'ColumnHeader49
        '
        Me.ColumnHeader49.Text = "Interval"
        '
        'splitMonitor3
        '
        Me.splitMonitor3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.splitMonitor3.IsSplitterFixed = True
        Me.splitMonitor3.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor3.Name = "splitMonitor3"
        Me.splitMonitor3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitMonitor3.Panel1
        '
        Me.splitMonitor3.Panel1.Controls.Add(Me.splitMonitor4)
        '
        'splitMonitor3.Panel2
        '
        Me.splitMonitor3.Panel2.Controls.Add(Me.txtMonitorNumber)
        Me.splitMonitor3.Panel2.Controls.Add(Me.lblMonitorMaxNumber)
        Me.splitMonitor3.Panel2.Controls.Add(Me.chkMonitorRightAuto)
        Me.splitMonitor3.Panel2.Controls.Add(Me.chkMonitorLeftAuto)
        Me.splitMonitor3.Panel2.Controls.Add(Me.dtMonitorR)
        Me.splitMonitor3.Panel2.Controls.Add(Me.dtMonitorL)
        Me.splitMonitor3.Size = New System.Drawing.Size(1132, 577)
        Me.splitMonitor3.SplitterDistance = 548
        Me.splitMonitor3.TabIndex = 0
        '
        'splitMonitor4
        '
        Me.splitMonitor4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMonitor4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitMonitor4.IsSplitterFixed = True
        Me.splitMonitor4.Location = New System.Drawing.Point(0, 0)
        Me.splitMonitor4.Name = "splitMonitor4"
        Me.splitMonitor4.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.splitMonitor4.Panel1Collapsed = True
        '
        'splitMonitor4.Panel2
        '
        Me.splitMonitor4.Panel2.Controls.Add(Me.graphMonitor)
        Me.splitMonitor4.Size = New System.Drawing.Size(1132, 548)
        Me.splitMonitor4.SplitterDistance = 25
        Me.splitMonitor4.TabIndex = 4
        '
        'graphMonitor
        '
        Me.graphMonitor.BackColor = System.Drawing.Color.Black
        Me.graphMonitor.ColorMemory2 = System.Drawing.Color.Blue
        Me.graphMonitor.ColorMemory3 = System.Drawing.Color.Orange
        Me.graphMonitor.dDate = New Date(CType(0, Long))
        Me.graphMonitor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.graphMonitor.EnableGraph = False
        Me.graphMonitor.Location = New System.Drawing.Point(0, 0)
        Me.graphMonitor.Name = "graphMonitor"
        Me.graphMonitor.Size = New System.Drawing.Size(1132, 548)
        Me.graphMonitor.TabIndex = 3
        Me.graphMonitor.TabStop = False
        Me.graphMonitor.ViewMax = 0
        Me.graphMonitor.ViewMin = 0
        '
        'txtMonitorNumber
        '
        Me.txtMonitorNumber.Location = New System.Drawing.Point(241, 0)
        Me.txtMonitorNumber.Name = "txtMonitorNumber"
        Me.txtMonitorNumber.Size = New System.Drawing.Size(33, 26)
        Me.txtMonitorNumber.TabIndex = 11
        Me.txtMonitorNumber.Text = "200"
        '
        'lblMonitorMaxNumber
        '
        Me.lblMonitorMaxNumber.AutoSize = True
        Me.lblMonitorMaxNumber.Location = New System.Drawing.Point(171, 6)
        Me.lblMonitorMaxNumber.Name = "lblMonitorMaxNumber"
        Me.lblMonitorMaxNumber.Size = New System.Drawing.Size(80, 19)
        Me.lblMonitorMaxNumber.TabIndex = 10
        Me.lblMonitorMaxNumber.Text = "Max. values"
        '
        'chkMonitorRightAuto
        '
        Me.chkMonitorRightAuto.AutoSize = True
        Me.chkMonitorRightAuto.Checked = True
        Me.chkMonitorRightAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonitorRightAuto.Dock = System.Windows.Forms.DockStyle.Right
        Me.chkMonitorRightAuto.Location = New System.Drawing.Point(984, 0)
        Me.chkMonitorRightAuto.Name = "chkMonitorRightAuto"
        Me.chkMonitorRightAuto.Size = New System.Drawing.Size(59, 25)
        Me.chkMonitorRightAuto.TabIndex = 9
        Me.chkMonitorRightAuto.Text = "Now"
        Me.chkMonitorRightAuto.UseVisualStyleBackColor = True
        '
        'chkMonitorLeftAuto
        '
        Me.chkMonitorLeftAuto.AutoSize = True
        Me.chkMonitorLeftAuto.Checked = True
        Me.chkMonitorLeftAuto.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMonitorLeftAuto.Location = New System.Drawing.Point(95, 4)
        Me.chkMonitorLeftAuto.Name = "chkMonitorLeftAuto"
        Me.chkMonitorLeftAuto.Size = New System.Drawing.Size(94, 23)
        Me.chkMonitorLeftAuto.TabIndex = 8
        Me.chkMonitorLeftAuto.Text = "Automatic"
        Me.chkMonitorLeftAuto.UseVisualStyleBackColor = True
        '
        'dtMonitorR
        '
        Me.dtMonitorR.Dock = System.Windows.Forms.DockStyle.Right
        Me.dtMonitorR.Enabled = False
        Me.dtMonitorR.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonitorR.Location = New System.Drawing.Point(1043, 0)
        Me.dtMonitorR.Name = "dtMonitorR"
        Me.dtMonitorR.Size = New System.Drawing.Size(89, 26)
        Me.dtMonitorR.TabIndex = 7
        '
        'dtMonitorL
        '
        Me.dtMonitorL.Dock = System.Windows.Forms.DockStyle.Left
        Me.dtMonitorL.Enabled = False
        Me.dtMonitorL.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtMonitorL.Location = New System.Drawing.Point(0, 0)
        Me.dtMonitorL.Name = "dtMonitorL"
        Me.dtMonitorL.Size = New System.Drawing.Size(89, 26)
        Me.dtMonitorL.TabIndex = 6
        '
        'pageServices
        '
        Me.pageServices.BackColor = System.Drawing.Color.Transparent
        Me.pageServices.Controls.Add(Me.containerServicesPage)
        Me.pageServices.Location = New System.Drawing.Point(4, 25)
        Me.pageServices.Name = "pageServices"
        Me.pageServices.Padding = New System.Windows.Forms.Padding(3)
        Me.pageServices.Size = New System.Drawing.Size(1700, 928)
        Me.pageServices.TabIndex = 1
        Me.pageServices.Text = "Services"
        Me.pageServices.UseVisualStyleBackColor = True
        '
        'containerServicesPage
        '
        Me.containerServicesPage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.containerServicesPage.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.containerServicesPage.IsSplitterFixed = True
        Me.containerServicesPage.Location = New System.Drawing.Point(3, 3)
        Me.containerServicesPage.Name = "containerServicesPage"
        Me.containerServicesPage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'containerServicesPage.Panel1
        '
        Me.containerServicesPage.Panel1.Controls.Add(Me.panelMenu2)
        '
        'containerServicesPage.Panel2
        '
        Me.containerServicesPage.Panel2.Controls.Add(Me.panelMain2)
        Me.containerServicesPage.Size = New System.Drawing.Size(1694, 922)
        Me.containerServicesPage.SplitterDistance = 25
        Me.containerServicesPage.TabIndex = 0
        '
        'panelMenu2
        '
        Me.panelMenu2.Controls.Add(Me.Label2)
        Me.panelMenu2.Controls.Add(Me.lblResCount2)
        Me.panelMenu2.Controls.Add(Me.txtServiceSearch)
        Me.panelMenu2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMenu2.Location = New System.Drawing.Point(0, 0)
        Me.panelMenu2.Name = "panelMenu2"
        Me.panelMenu2.Size = New System.Drawing.Size(1694, 25)
        Me.panelMenu2.TabIndex = 49
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Search service"
        '
        'lblResCount2
        '
        Me.lblResCount2.AutoSize = True
        Me.lblResCount2.Location = New System.Drawing.Point(596, 6)
        Me.lblResCount2.Name = "lblResCount2"
        Me.lblResCount2.Size = New System.Drawing.Size(69, 19)
        Me.lblResCount2.TabIndex = 2
        Me.lblResCount2.Text = "0 result(s)"
        '
        'txtServiceSearch
        '
        Me.txtServiceSearch.Location = New System.Drawing.Point(89, 1)
        Me.txtServiceSearch.Name = "txtServiceSearch"
        Me.txtServiceSearch.Size = New System.Drawing.Size(501, 26)
        Me.txtServiceSearch.TabIndex = 1
        '
        'panelMain2
        '
        Me.panelMain2.Controls.Add(Me.splitServices)
        Me.panelMain2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain2.Location = New System.Drawing.Point(0, 0)
        Me.panelMain2.Name = "panelMain2"
        Me.panelMain2.Size = New System.Drawing.Size(1694, 893)
        Me.panelMain2.TabIndex = 17
        '
        'splitServices
        '
        Me.splitServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices.Location = New System.Drawing.Point(0, 0)
        Me.splitServices.Name = "splitServices"
        Me.splitServices.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitServices.Panel1
        '
        Me.splitServices.Panel1.Controls.Add(Me.lvServices)
        '
        'splitServices.Panel2
        '
        Me.splitServices.Panel2.Controls.Add(Me.splitServices2)
        Me.splitServices.Size = New System.Drawing.Size(1694, 893)
        Me.splitServices.SplitterDistance = 531
        Me.splitServices.TabIndex = 0
        '
        'lvServices
        '
        Me.lvServices.AllowColumnReorder = True
        Me.lvServices.CatchErrors = False
        Me.lvServices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader19})
        Me.lvServices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvServices.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvServices.FullRowSelect = True
        ListViewGroup7.Header = "Services"
        ListViewGroup7.Name = "gpOther"
        ListViewGroup8.Header = "Search result"
        ListViewGroup8.Name = "gpSearch"
        Me.lvServices.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup7, ListViewGroup8})
        Me.lvServices.HideSelection = False
        Me.lvServices.InstanceId = 4
        Me.lvServices.IsConnected = False
        Me.lvServices.Location = New System.Drawing.Point(0, 0)
        Me.lvServices.Name = "lvServices"
        Me.lvServices.OverriddenDoubleBuffered = True
        Me.lvServices.ReorganizeColumns = True
        Me.lvServices.ShowItemToolTips = True
        Me.lvServices.ShowObjectDetailsOnDoubleClick = False
        Me.lvServices.Size = New System.Drawing.Size(1694, 531)
        Me.lvServices.TabIndex = 1
        Me.lvServices.UseCompatibleStateImageBehavior = False
        Me.lvServices.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Name"
        Me.ColumnHeader3.Width = 121
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "DisplayName"
        Me.ColumnHeader7.Width = 243
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "State"
        Me.ColumnHeader8.Width = 79
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "StartType"
        Me.ColumnHeader9.Width = 70
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "ImagePath"
        Me.ColumnHeader10.Width = 250
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Process"
        Me.ColumnHeader11.Width = 150
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "ServiceType"
        Me.ColumnHeader19.Width = 100
        '
        'splitServices2
        '
        Me.splitServices2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitServices2.IsSplitterFixed = True
        Me.splitServices2.Location = New System.Drawing.Point(0, 0)
        Me.splitServices2.Name = "splitServices2"
        Me.splitServices2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitServices2.Panel1
        '
        Me.splitServices2.Panel1.Controls.Add(Me.cmdCopyServiceToCp)
        Me.splitServices2.Panel1.Controls.Add(Me.lblServicePath)
        Me.splitServices2.Panel1.Controls.Add(Me.lblServiceName)
        '
        'splitServices2.Panel2
        '
        Me.splitServices2.Panel2.Controls.Add(Me.splitServices3)
        Me.splitServices2.Size = New System.Drawing.Size(1694, 358)
        Me.splitServices2.SplitterDistance = 35
        Me.splitServices2.TabIndex = 15
        '
        'cmdCopyServiceToCp
        '
        Me.cmdCopyServiceToCp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCopyServiceToCp.Enabled = False
        Me.cmdCopyServiceToCp.Image = Global.My.Resources.Resources.copy16
        Me.cmdCopyServiceToCp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdCopyServiceToCp.Location = New System.Drawing.Point(1562, 8)
        Me.cmdCopyServiceToCp.Name = "cmdCopyServiceToCp"
        Me.cmdCopyServiceToCp.Size = New System.Drawing.Size(130, 24)
        Me.cmdCopyServiceToCp.TabIndex = 19
        Me.cmdCopyServiceToCp.Text = "Copy to clipboard"
        Me.cmdCopyServiceToCp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdCopyServiceToCp.UseVisualStyleBackColor = True
        '
        'lblServicePath
        '
        Me.lblServicePath.BackColor = System.Drawing.SystemColors.Control
        Me.lblServicePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblServicePath.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.lblServicePath.Location = New System.Drawing.Point(7, 19)
        Me.lblServicePath.Name = "lblServicePath"
        Me.lblServicePath.ReadOnly = True
        Me.lblServicePath.Size = New System.Drawing.Size(440, 19)
        Me.lblServicePath.TabIndex = 18
        '
        'lblServiceName
        '
        Me.lblServiceName.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblServiceName.Location = New System.Drawing.Point(3, 0)
        Me.lblServiceName.Name = "lblServiceName"
        Me.lblServiceName.Size = New System.Drawing.Size(554, 32)
        Me.lblServiceName.TabIndex = 17
        Me.lblServiceName.Text = "Service name :"
        '
        'splitServices3
        '
        Me.splitServices3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices3.Location = New System.Drawing.Point(0, 0)
        Me.splitServices3.Name = "splitServices3"
        '
        'splitServices3.Panel1
        '
        Me.splitServices3.Panel1.Controls.Add(Me.rtb2)
        '
        'splitServices3.Panel2
        '
        Me.splitServices3.Panel2.Controls.Add(Me.splitServices4)
        Me.splitServices3.Size = New System.Drawing.Size(1694, 319)
        Me.splitServices3.SplitterDistance = 1250
        Me.splitServices3.TabIndex = 0
        '
        'rtb2
        '
        Me.rtb2.AutoWordSelection = True
        Me.rtb2.BackColor = System.Drawing.Color.White
        Me.rtb2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb2.HideSelection = False
        Me.rtb2.Location = New System.Drawing.Point(0, 0)
        Me.rtb2.Name = "rtb2"
        Me.rtb2.ReadOnly = True
        Me.rtb2.Size = New System.Drawing.Size(1250, 319)
        Me.rtb2.TabIndex = 13
        Me.rtb2.Text = ""
        Me.rtb2.WordWrap = False
        '
        'splitServices4
        '
        Me.splitServices4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitServices4.Location = New System.Drawing.Point(0, 0)
        Me.splitServices4.Name = "splitServices4"
        Me.splitServices4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitServices4.Panel1
        '
        Me.splitServices4.Panel1.Controls.Add(Me.tv2)
        '
        'splitServices4.Panel2
        '
        Me.splitServices4.Panel2.Controls.Add(Me.tv)
        Me.splitServices4.Size = New System.Drawing.Size(440, 319)
        Me.splitServices4.SplitterDistance = 155
        Me.splitServices4.TabIndex = 0
        '
        'tv2
        '
        CConnection1.Snapshot = Nothing
        CConnection1.SnapshotFile = Nothing
        CConnection1.Type = cConnection.TypeOfConnection.LocalConnection
        Me.tv2.ConnectionObj = CConnection1
        Me.tv2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tv2.ImageIndex = 0
        Me.tv2.ImageList = Me.imgServices
        Me.tv2.InfosToGet = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
        Me.tv2.IsConnected = False
        Me.tv2.Location = New System.Drawing.Point(0, 0)
        Me.tv2.Name = "tv2"
        Me.tv2.RootService = Nothing
        Me.tv2.SelectedImageIndex = 2
        Me.tv2.Size = New System.Drawing.Size(440, 155)
        Me.tv2.TabIndex = 15
        '
        'tv
        '
        CConnection2.Snapshot = Nothing
        CConnection2.SnapshotFile = Nothing
        CConnection2.Type = cConnection.TypeOfConnection.LocalConnection
        Me.tv.ConnectionObj = CConnection2
        Me.tv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tv.ImageIndex = 0
        Me.tv.ImageList = Me.imgServices
        Me.tv.InfosToGet = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe
        Me.tv.IsConnected = False
        Me.tv.Location = New System.Drawing.Point(0, 0)
        Me.tv.Name = "tv"
        Me.tv.RootService = Nothing
        Me.tv.SelectedImageIndex = 0
        Me.tv.Size = New System.Drawing.Size(440, 160)
        Me.tv.TabIndex = 14
        '
        'pageNetwork
        '
        Me.pageNetwork.BackColor = System.Drawing.Color.Transparent
        Me.pageNetwork.Controls.Add(Me.panelMain14)
        Me.pageNetwork.Location = New System.Drawing.Point(4, 25)
        Me.pageNetwork.Name = "pageNetwork"
        Me.pageNetwork.Padding = New System.Windows.Forms.Padding(3)
        Me.pageNetwork.Size = New System.Drawing.Size(1700, 928)
        Me.pageNetwork.TabIndex = 12
        Me.pageNetwork.Text = "Network"
        Me.pageNetwork.UseVisualStyleBackColor = True
        '
        'panelMain14
        '
        Me.panelMain14.Controls.Add(Me.lvNetwork)
        Me.panelMain14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain14.Location = New System.Drawing.Point(3, 3)
        Me.panelMain14.Name = "panelMain14"
        Me.panelMain14.Size = New System.Drawing.Size(1694, 922)
        Me.panelMain14.TabIndex = 57
        '
        'lvNetwork
        '
        Me.lvNetwork.AllowColumnReorder = True
        Me.lvNetwork.CatchErrors = False
        Me.lvNetwork.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader66, Me.ColumnHeader67, Me.ColumnHeader68, Me.ColumnHeader69})
        Me.lvNetwork.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvNetwork.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvNetwork.FullRowSelect = True
        Me.lvNetwork.HideSelection = False
        Me.lvNetwork.InstanceId = 5
        Me.lvNetwork.IsConnected = False
        Me.lvNetwork.Location = New System.Drawing.Point(0, 0)
        Me.lvNetwork.Name = "lvNetwork"
        Me.lvNetwork.OverriddenDoubleBuffered = True
        Me.lvNetwork.ReorganizeColumns = True
        Me.lvNetwork.ShowByProcessGroup = False
        Me.lvNetwork.ShowItemToolTips = True
        Me.lvNetwork.ShowObjectDetailsOnDoubleClick = True
        Me.lvNetwork.Size = New System.Drawing.Size(1694, 922)
        Me.lvNetwork.TabIndex = 4
        Me.lvNetwork.UseCompatibleStateImageBehavior = False
        Me.lvNetwork.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader66
        '
        Me.ColumnHeader66.Text = "Local"
        Me.ColumnHeader66.Width = 200
        '
        'ColumnHeader67
        '
        Me.ColumnHeader67.Text = "Remote"
        Me.ColumnHeader67.Width = 250
        '
        'ColumnHeader68
        '
        Me.ColumnHeader68.Text = "Protocol"
        Me.ColumnHeader68.Width = 100
        '
        'ColumnHeader69
        '
        Me.ColumnHeader69.Text = "State"
        Me.ColumnHeader69.Width = 150
        '
        'pageFile
        '
        Me.pageFile.BackColor = System.Drawing.Color.Transparent
        Me.pageFile.Controls.Add(Me.panelMain5)
        Me.pageFile.Location = New System.Drawing.Point(4, 25)
        Me.pageFile.Name = "pageFile"
        Me.pageFile.Padding = New System.Windows.Forms.Padding(3)
        Me.pageFile.Size = New System.Drawing.Size(1700, 928)
        Me.pageFile.TabIndex = 4
        Me.pageFile.Text = "File"
        Me.pageFile.UseVisualStyleBackColor = True
        '
        'panelMain5
        '
        Me.panelMain5.Controls.Add(Me.SplitContainerFilexx)
        Me.panelMain5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain5.Location = New System.Drawing.Point(3, 3)
        Me.panelMain5.Name = "panelMain5"
        Me.panelMain5.Size = New System.Drawing.Size(1694, 922)
        Me.panelMain5.TabIndex = 48
        '
        'SplitContainerFilexx
        '
        Me.SplitContainerFilexx.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerFilexx.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerFilexx.IsSplitterFixed = True
        Me.SplitContainerFilexx.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerFilexx.Name = "SplitContainerFilexx"
        Me.SplitContainerFilexx.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerFilexx.Panel1
        '
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.txtFile)
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.cmdFileClipboard)
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.pctFileSmall)
        Me.SplitContainerFilexx.Panel1.Controls.Add(Me.pctFileBig)
        '
        'SplitContainerFilexx.Panel2
        '
        Me.SplitContainerFilexx.Panel2.Controls.Add(Me.SplitContainerFile)
        Me.SplitContainerFilexx.Size = New System.Drawing.Size(1694, 922)
        Me.SplitContainerFilexx.SplitterDistance = 35
        Me.SplitContainerFilexx.TabIndex = 0
        '
        'txtFile
        '
        Me.txtFile.BackColor = System.Drawing.SystemColors.Control
        Me.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtFile.Location = New System.Drawing.Point(5, 9)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(240, 20)
        Me.txtFile.TabIndex = 22
        Me.txtFile.Text = "No selected file"
        '
        'cmdFileClipboard
        '
        Me.cmdFileClipboard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFileClipboard.Enabled = False
        Me.cmdFileClipboard.Image = Global.My.Resources.Resources.copy16
        Me.cmdFileClipboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdFileClipboard.Location = New System.Drawing.Point(1493, 7)
        Me.cmdFileClipboard.Name = "cmdFileClipboard"
        Me.cmdFileClipboard.Size = New System.Drawing.Size(130, 24)
        Me.cmdFileClipboard.TabIndex = 21
        Me.cmdFileClipboard.Text = "Copy to clipboard"
        Me.cmdFileClipboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdFileClipboard.UseVisualStyleBackColor = True
        '
        'pctFileSmall
        '
        Me.pctFileSmall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctFileSmall.Location = New System.Drawing.Point(1636, 16)
        Me.pctFileSmall.Name = "pctFileSmall"
        Me.pctFileSmall.Size = New System.Drawing.Size(16, 16)
        Me.pctFileSmall.TabIndex = 20
        Me.pctFileSmall.TabStop = False
        '
        'pctFileBig
        '
        Me.pctFileBig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pctFileBig.Location = New System.Drawing.Point(1658, 0)
        Me.pctFileBig.Name = "pctFileBig"
        Me.pctFileBig.Size = New System.Drawing.Size(32, 32)
        Me.pctFileBig.TabIndex = 19
        Me.pctFileBig.TabStop = False
        '
        'SplitContainerFile
        '
        Me.SplitContainerFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerFile.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerFile.Name = "SplitContainerFile"
        '
        'SplitContainerFile.Panel1
        '
        Me.SplitContainerFile.Panel1.Controls.Add(Me.SplitContainerFile2)
        '
        'SplitContainerFile.Panel2
        '
        Me.SplitContainerFile.Panel2.Controls.Add(Me.lvFileString)
        Me.SplitContainerFile.Size = New System.Drawing.Size(1694, 883)
        Me.SplitContainerFile.SplitterDistance = 1154
        Me.SplitContainerFile.TabIndex = 15
        '
        'SplitContainerFile2
        '
        Me.SplitContainerFile2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerFile2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerFile2.IsSplitterFixed = True
        Me.SplitContainerFile2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerFile2.Name = "SplitContainerFile2"
        Me.SplitContainerFile2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerFile2.Panel1
        '
        Me.SplitContainerFile2.Panel1.Controls.Add(Me.rtb3)
        '
        'SplitContainerFile2.Panel2
        '
        Me.SplitContainerFile2.Panel2.Controls.Add(Me.gpFileAttributes)
        Me.SplitContainerFile2.Panel2.Controls.Add(Me.gpFileDates)
        Me.SplitContainerFile2.Size = New System.Drawing.Size(1154, 883)
        Me.SplitContainerFile2.SplitterDistance = 767
        Me.SplitContainerFile2.TabIndex = 3
        '
        'rtb3
        '
        Me.rtb3.AutoWordSelection = True
        Me.rtb3.BackColor = System.Drawing.Color.White
        Me.rtb3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb3.HideSelection = False
        Me.rtb3.Location = New System.Drawing.Point(0, 0)
        Me.rtb3.Name = "rtb3"
        Me.rtb3.ReadOnly = True
        Me.rtb3.Size = New System.Drawing.Size(1154, 767)
        Me.rtb3.TabIndex = 21
        Me.rtb3.Text = ""
        '
        'gpFileAttributes
        '
        Me.gpFileAttributes.Controls.Add(Me.chkFileEncrypted)
        Me.gpFileAttributes.Controls.Add(Me.chkFileContentNotIndexed)
        Me.gpFileAttributes.Controls.Add(Me.chkFileNormal)
        Me.gpFileAttributes.Controls.Add(Me.chkFileSystem)
        Me.gpFileAttributes.Controls.Add(Me.chkFileReadOnly)
        Me.gpFileAttributes.Controls.Add(Me.chkFileHidden)
        Me.gpFileAttributes.Controls.Add(Me.chkFileCompressed)
        Me.gpFileAttributes.Controls.Add(Me.chkFileArchive)
        Me.gpFileAttributes.Dock = System.Windows.Forms.DockStyle.Left
        Me.gpFileAttributes.Location = New System.Drawing.Point(203, 0)
        Me.gpFileAttributes.Name = "gpFileAttributes"
        Me.gpFileAttributes.Size = New System.Drawing.Size(173, 112)
        Me.gpFileAttributes.TabIndex = 19
        Me.gpFileAttributes.TabStop = False
        Me.gpFileAttributes.Text = "File attributes"
        '
        'chkFileEncrypted
        '
        Me.chkFileEncrypted.AutoSize = True
        Me.chkFileEncrypted.Enabled = False
        Me.chkFileEncrypted.Location = New System.Drawing.Point(91, 51)
        Me.chkFileEncrypted.Name = "chkFileEncrypted"
        Me.chkFileEncrypted.Size = New System.Drawing.Size(92, 23)
        Me.chkFileEncrypted.TabIndex = 7
        Me.chkFileEncrypted.Text = "Encrypted"
        Me.chkFileEncrypted.UseVisualStyleBackColor = True
        '
        'chkFileContentNotIndexed
        '
        Me.chkFileContentNotIndexed.AutoSize = True
        Me.chkFileContentNotIndexed.Location = New System.Drawing.Point(6, 88)
        Me.chkFileContentNotIndexed.Name = "chkFileContentNotIndexed"
        Me.chkFileContentNotIndexed.Size = New System.Drawing.Size(157, 23)
        Me.chkFileContentNotIndexed.TabIndex = 6
        Me.chkFileContentNotIndexed.Text = "Content not indexed"
        Me.chkFileContentNotIndexed.UseVisualStyleBackColor = True
        '
        'chkFileNormal
        '
        Me.chkFileNormal.AutoSize = True
        Me.chkFileNormal.Enabled = False
        Me.chkFileNormal.Location = New System.Drawing.Point(91, 15)
        Me.chkFileNormal.Name = "chkFileNormal"
        Me.chkFileNormal.Size = New System.Drawing.Size(76, 23)
        Me.chkFileNormal.TabIndex = 5
        Me.chkFileNormal.Text = "Normal"
        Me.chkFileNormal.UseVisualStyleBackColor = True
        '
        'chkFileSystem
        '
        Me.chkFileSystem.AutoSize = True
        Me.chkFileSystem.Location = New System.Drawing.Point(91, 33)
        Me.chkFileSystem.Name = "chkFileSystem"
        Me.chkFileSystem.Size = New System.Drawing.Size(75, 23)
        Me.chkFileSystem.TabIndex = 4
        Me.chkFileSystem.Text = "System"
        Me.chkFileSystem.UseVisualStyleBackColor = True
        '
        'chkFileReadOnly
        '
        Me.chkFileReadOnly.AutoSize = True
        Me.chkFileReadOnly.Location = New System.Drawing.Point(6, 70)
        Me.chkFileReadOnly.Name = "chkFileReadOnly"
        Me.chkFileReadOnly.Size = New System.Drawing.Size(91, 23)
        Me.chkFileReadOnly.TabIndex = 3
        Me.chkFileReadOnly.Text = "Read only"
        Me.chkFileReadOnly.UseVisualStyleBackColor = True
        '
        'chkFileHidden
        '
        Me.chkFileHidden.AutoSize = True
        Me.chkFileHidden.Location = New System.Drawing.Point(6, 52)
        Me.chkFileHidden.Name = "chkFileHidden"
        Me.chkFileHidden.Size = New System.Drawing.Size(75, 23)
        Me.chkFileHidden.TabIndex = 2
        Me.chkFileHidden.Text = "Hidden"
        Me.chkFileHidden.UseVisualStyleBackColor = True
        '
        'chkFileCompressed
        '
        Me.chkFileCompressed.AutoSize = True
        Me.chkFileCompressed.Enabled = False
        Me.chkFileCompressed.Location = New System.Drawing.Point(6, 34)
        Me.chkFileCompressed.Name = "chkFileCompressed"
        Me.chkFileCompressed.Size = New System.Drawing.Size(107, 23)
        Me.chkFileCompressed.TabIndex = 1
        Me.chkFileCompressed.Text = "Compressed"
        Me.chkFileCompressed.UseVisualStyleBackColor = True
        '
        'chkFileArchive
        '
        Me.chkFileArchive.AutoSize = True
        Me.chkFileArchive.Location = New System.Drawing.Point(6, 17)
        Me.chkFileArchive.Name = "chkFileArchive"
        Me.chkFileArchive.Size = New System.Drawing.Size(76, 23)
        Me.chkFileArchive.TabIndex = 0
        Me.chkFileArchive.Text = "Archive"
        Me.chkFileArchive.UseVisualStyleBackColor = True
        '
        'gpFileDates
        '
        Me.gpFileDates.Controls.Add(Me.cmdSetFileDates)
        Me.gpFileDates.Controls.Add(Me.DTlastModification)
        Me.gpFileDates.Controls.Add(Me.DTlastAccess)
        Me.gpFileDates.Controls.Add(Me.DTcreation)
        Me.gpFileDates.Controls.Add(Me.Label6)
        Me.gpFileDates.Controls.Add(Me.Label5)
        Me.gpFileDates.Controls.Add(Me.Label4)
        Me.gpFileDates.Dock = System.Windows.Forms.DockStyle.Left
        Me.gpFileDates.Location = New System.Drawing.Point(0, 0)
        Me.gpFileDates.Name = "gpFileDates"
        Me.gpFileDates.Size = New System.Drawing.Size(203, 112)
        Me.gpFileDates.TabIndex = 18
        Me.gpFileDates.TabStop = False
        Me.gpFileDates.Text = "File dates"
        '
        'cmdSetFileDates
        '
        Me.cmdSetFileDates.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cmdSetFileDates.Location = New System.Drawing.Point(3, 87)
        Me.cmdSetFileDates.Name = "cmdSetFileDates"
        Me.cmdSetFileDates.Size = New System.Drawing.Size(197, 22)
        Me.cmdSetFileDates.TabIndex = 6
        Me.cmdSetFileDates.Text = "Set dates"
        Me.cmdSetFileDates.UseVisualStyleBackColor = True
        '
        'DTlastModification
        '
        Me.DTlastModification.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTlastModification.Location = New System.Drawing.Point(102, 57)
        Me.DTlastModification.Name = "DTlastModification"
        Me.DTlastModification.Size = New System.Drawing.Size(84, 26)
        Me.DTlastModification.TabIndex = 5
        '
        'DTlastAccess
        '
        Me.DTlastAccess.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTlastAccess.Location = New System.Drawing.Point(102, 35)
        Me.DTlastAccess.Name = "DTlastAccess"
        Me.DTlastAccess.Size = New System.Drawing.Size(84, 26)
        Me.DTlastAccess.TabIndex = 4
        '
        'DTcreation
        '
        Me.DTcreation.CustomFormat = ""
        Me.DTcreation.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTcreation.Location = New System.Drawing.Point(102, 13)
        Me.DTcreation.Name = "DTcreation"
        Me.DTcreation.Size = New System.Drawing.Size(84, 26)
        Me.DTcreation.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 41)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(76, 19)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Last access"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 63)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 19)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Last modification"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 19)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Creation"
        '
        'lvFileString
        '
        Me.lvFileString.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.headerString})
        Me.lvFileString.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvFileString.FullRowSelect = True
        Me.lvFileString.HideSelection = False
        Me.lvFileString.Location = New System.Drawing.Point(0, 0)
        Me.lvFileString.Name = "lvFileString"
        Me.lvFileString.OverriddenDoubleBuffered = True
        Me.lvFileString.ShowItemToolTips = True
        Me.lvFileString.Size = New System.Drawing.Size(536, 883)
        Me.lvFileString.TabIndex = 22
        Me.lvFileString.UseCompatibleStateImageBehavior = False
        Me.lvFileString.View = System.Windows.Forms.View.Details
        '
        'headerString
        '
        Me.headerString.Text = "Strings"
        Me.headerString.Width = 250
        '
        'pageSearch
        '
        Me.pageSearch.BackColor = System.Drawing.Color.Transparent
        Me.pageSearch.Controls.Add(Me.panelMain6)
        Me.pageSearch.Location = New System.Drawing.Point(4, 25)
        Me.pageSearch.Name = "pageSearch"
        Me.pageSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.pageSearch.Size = New System.Drawing.Size(1700, 928)
        Me.pageSearch.TabIndex = 5
        Me.pageSearch.Text = "Search"
        Me.pageSearch.UseVisualStyleBackColor = True
        '
        'panelMain6
        '
        Me.panelMain6.Controls.Add(Me.SplitContainerSearch)
        Me.panelMain6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain6.Location = New System.Drawing.Point(3, 3)
        Me.panelMain6.Name = "panelMain6"
        Me.panelMain6.Size = New System.Drawing.Size(1694, 922)
        Me.panelMain6.TabIndex = 49
        '
        'SplitContainerSearch
        '
        Me.SplitContainerSearch.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerSearch.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerSearch.IsSplitterFixed = True
        Me.SplitContainerSearch.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerSearch.Name = "SplitContainerSearch"
        Me.SplitContainerSearch.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerSearch.Panel1
        '
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchEnvVar)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.Label11)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.lblResultsCount)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.txtSearchResults)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchWindows)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchHandles)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchModules)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchServices)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchProcess)
        Me.SplitContainerSearch.Panel1.Controls.Add(Me.chkSearchCase)
        '
        'SplitContainerSearch.Panel2
        '
        Me.SplitContainerSearch.Panel2.Controls.Add(Me.lvSearchResults)
        Me.SplitContainerSearch.Size = New System.Drawing.Size(1694, 922)
        Me.SplitContainerSearch.SplitterDistance = 55
        Me.SplitContainerSearch.TabIndex = 2
        '
        'chkSearchEnvVar
        '
        Me.chkSearchEnvVar.AutoSize = True
        Me.chkSearchEnvVar.Checked = True
        Me.chkSearchEnvVar.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchEnvVar.Location = New System.Drawing.Point(376, 7)
        Me.chkSearchEnvVar.Name = "chkSearchEnvVar"
        Me.chkSearchEnvVar.Size = New System.Drawing.Size(160, 23)
        Me.chkSearchEnvVar.TabIndex = 14
        Me.chkSearchEnvVar.Text = "environment variable"
        Me.chkSearchEnvVar.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 19)
        Me.Label11.TabIndex = 13
        Me.Label11.Text = "Search result"
        '
        'lblResultsCount
        '
        Me.lblResultsCount.AutoSize = True
        Me.lblResultsCount.Location = New System.Drawing.Point(396, 34)
        Me.lblResultsCount.Name = "lblResultsCount"
        Me.lblResultsCount.Size = New System.Drawing.Size(69, 19)
        Me.lblResultsCount.TabIndex = 12
        Me.lblResultsCount.Text = "0 result(s)"
        '
        'txtSearchResults
        '
        Me.txtSearchResults.Location = New System.Drawing.Point(89, 31)
        Me.txtSearchResults.Name = "txtSearchResults"
        Me.txtSearchResults.Size = New System.Drawing.Size(298, 26)
        Me.txtSearchResults.TabIndex = 11
        '
        'chkSearchWindows
        '
        Me.chkSearchWindows.AutoSize = True
        Me.chkSearchWindows.Checked = True
        Me.chkSearchWindows.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchWindows.Location = New System.Drawing.Point(656, 8)
        Me.chkSearchWindows.Name = "chkSearchWindows"
        Me.chkSearchWindows.Size = New System.Drawing.Size(84, 23)
        Me.chkSearchWindows.TabIndex = 6
        Me.chkSearchWindows.Text = "windows"
        Me.chkSearchWindows.UseVisualStyleBackColor = True
        '
        'chkSearchHandles
        '
        Me.chkSearchHandles.AutoSize = True
        Me.chkSearchHandles.Checked = True
        Me.chkSearchHandles.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchHandles.Location = New System.Drawing.Point(587, 8)
        Me.chkSearchHandles.Name = "chkSearchHandles"
        Me.chkSearchHandles.Size = New System.Drawing.Size(78, 23)
        Me.chkSearchHandles.TabIndex = 5
        Me.chkSearchHandles.Text = "handles"
        Me.chkSearchHandles.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(162, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "search in"
        '
        'chkSearchModules
        '
        Me.chkSearchModules.AutoSize = True
        Me.chkSearchModules.Checked = True
        Me.chkSearchModules.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchModules.Location = New System.Drawing.Point(300, 7)
        Me.chkSearchModules.Name = "chkSearchModules"
        Me.chkSearchModules.Size = New System.Drawing.Size(83, 23)
        Me.chkSearchModules.TabIndex = 3
        Me.chkSearchModules.Text = "modules"
        Me.chkSearchModules.UseVisualStyleBackColor = True
        '
        'chkSearchServices
        '
        Me.chkSearchServices.AutoSize = True
        Me.chkSearchServices.Checked = True
        Me.chkSearchServices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchServices.Location = New System.Drawing.Point(516, 8)
        Me.chkSearchServices.Name = "chkSearchServices"
        Me.chkSearchServices.Size = New System.Drawing.Size(78, 23)
        Me.chkSearchServices.TabIndex = 2
        Me.chkSearchServices.Text = "services"
        Me.chkSearchServices.UseVisualStyleBackColor = True
        '
        'chkSearchProcess
        '
        Me.chkSearchProcess.AutoSize = True
        Me.chkSearchProcess.Checked = True
        Me.chkSearchProcess.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSearchProcess.Location = New System.Drawing.Point(223, 7)
        Me.chkSearchProcess.Name = "chkSearchProcess"
        Me.chkSearchProcess.Size = New System.Drawing.Size(90, 23)
        Me.chkSearchProcess.TabIndex = 1
        Me.chkSearchProcess.Text = "processes"
        Me.chkSearchProcess.UseVisualStyleBackColor = True
        '
        'chkSearchCase
        '
        Me.chkSearchCase.AutoSize = True
        Me.chkSearchCase.Location = New System.Drawing.Point(8, 7)
        Me.chkSearchCase.Name = "chkSearchCase"
        Me.chkSearchCase.Size = New System.Drawing.Size(116, 23)
        Me.chkSearchCase.TabIndex = 0
        Me.chkSearchCase.Text = "Case sensitive"
        Me.chkSearchCase.UseVisualStyleBackColor = True
        '
        'lvSearchResults
        '
        Me.lvSearchResults.AllowColumnReorder = True
        Me.lvSearchResults.CaseSensitive = False
        Me.lvSearchResults.CatchErrors = False
        Me.lvSearchResults.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader17})
        CConnection3.Snapshot = Nothing
        CConnection3.SnapshotFile = Nothing
        CConnection3.Type = cConnection.TypeOfConnection.LocalConnection
        Me.lvSearchResults.ConnectionObj = CConnection3
        Me.lvSearchResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvSearchResults.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvSearchResults.FullRowSelect = True
        ListViewGroup9.Header = "Results"
        ListViewGroup9.Name = "gpResults"
        ListViewGroup10.Header = "Search results"
        ListViewGroup10.Name = "gpSearchResults"
        Me.lvSearchResults.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup9, ListViewGroup10})
        Me.lvSearchResults.HideSelection = False
        Me.lvSearchResults.Includes = Native.Api.Enums.GeneralObjectType.Process
        Me.lvSearchResults.InstanceId = 6
        Me.lvSearchResults.IsConnected = False
        Me.lvSearchResults.Location = New System.Drawing.Point(0, 0)
        Me.lvSearchResults.Name = "lvSearchResults"
        Me.lvSearchResults.OverriddenDoubleBuffered = True
        Me.lvSearchResults.ReorganizeColumns = True
        Me.lvSearchResults.SearchString = Nothing
        Me.lvSearchResults.ShowItemToolTips = True
        Me.lvSearchResults.ShowObjectDetailsOnDoubleClick = True
        Me.lvSearchResults.Size = New System.Drawing.Size(1694, 863)
        Me.lvSearchResults.TabIndex = 3
        Me.lvSearchResults.UseCompatibleStateImageBehavior = False
        Me.lvSearchResults.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Type"
        Me.ColumnHeader12.Width = 150
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Result"
        Me.ColumnHeader13.Width = 400
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Field"
        Me.ColumnHeader14.Width = 150
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "Owner"
        Me.ColumnHeader17.Width = 150
        '
        'pageHelp
        '
        Me.pageHelp.BackColor = System.Drawing.Color.Transparent
        Me.pageHelp.Controls.Add(Me.panelMain4)
        Me.pageHelp.Location = New System.Drawing.Point(4, 25)
        Me.pageHelp.Name = "pageHelp"
        Me.pageHelp.Padding = New System.Windows.Forms.Padding(3)
        Me.pageHelp.Size = New System.Drawing.Size(1700, 928)
        Me.pageHelp.TabIndex = 3
        Me.pageHelp.Text = "Help"
        Me.pageHelp.UseVisualStyleBackColor = True
        '
        'panelMain4
        '
        Me.panelMain4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelMain4.Location = New System.Drawing.Point(3, 3)
        Me.panelMain4.Name = "panelMain4"
        Me.panelMain4.Size = New System.Drawing.Size(1694, 922)
        Me.panelMain4.TabIndex = 17
        '
        'timerNetwork
        '
        Me.timerNetwork.Interval = 1000
        '
        '
        'MenuItemTaskShow
        '
        Me.MenuItemTaskShow.DefaultItem = True
        Me.MenuItemTaskShow.Index = 0
        Me.MenuItemTaskShow.Text = "Show window"
        '
        'MenuItemTaskEnd
        '
        Me.MenuItemTaskEnd.Index = 4
        Me.MenuItemTaskEnd.Text = "End task"
        '
        'MenuItemTaskSelProc
        '
        Me.MenuItemTaskSelProc.Enabled = False
        Me.MenuItemTaskSelProc.Index = 6
        Me.MenuItemTaskSelProc.Text = "Select associated process"
        '
        'MenuItemMonitorAdd
        '
        Me.MenuItemMonitorAdd.Index = 0
        Me.MenuItemMonitorAdd.Text = "Add..."
        '
        'MenuItemMonitorRemove
        '
        Me.MenuItemMonitorRemove.Index = 1
        Me.MenuItemMonitorRemove.Text = "Remove selection"
        '
        'MenuItemMonitorStart
        '
        Me.MenuItemMonitorStart.DefaultItem = True
        Me.MenuItemMonitorStart.Index = 3
        Me.MenuItemMonitorStart.Text = "Start"
        '
        'MenuItemMonitorStop
        '
        Me.MenuItemMonitorStop.Index = 4
        Me.MenuItemMonitorStop.Text = "Stop"
        '
        'MenuItemCopyBig
        '
        Me.MenuItemCopyBig.Index = 0
        Me.MenuItemCopyBig.Text = "Copy to clipboard"
        '
        'MenuItemCopySmall
        '
        Me.MenuItemCopySmall.Index = 0
        Me.MenuItemCopySmall.Text = "Copy to clipboard"
        '
        'MenuItemMainShow
        '
        Me.MenuItemMainShow.DefaultItem = True
        Me.MenuItemMainShow.Text = "Show LitePM"
        '
        'MenuItemMainToTray
        '
        Me.MenuItemMainToTray.Text = "Minimize to tray"
        '
        'MenuItemMainAbout
        '
        Me.MenuItemMainAbout.Text = "About LitePM"
        '
        'MenuItemMainLog
        '
        Me.MenuItemMainLog.Text = "Show log"
        '
        'MenuItemMainOpenedW
        '
        Me.MenuItemMainOpenedW.Text = "Opened windows"
        '
        'MenuItemMainExit
        '
        Me.MenuItemMainExit.Text = "Exit LitePM"
        '
        'MenuItemMainSysInfo
        '
        Me.MenuItemMainSysInfo.Text = "System information"
        '
        'MenuItemServSelService
        '
        Me.MenuItemServSelService.DefaultItem = True
        Me.MenuItemServSelService.Index = 0
        Me.MenuItemServSelService.Text = "Show selected process"
        '
        'MenuItemServFileProp
        '
        Me.MenuItemServFileProp.Index = 2
        Me.MenuItemServFileProp.Text = "File properties"
        '
        'MenuItemServOpenDir
        '
        Me.MenuItemServOpenDir.Index = 3
        Me.MenuItemServOpenDir.Text = "Open directory"
        '
        'MenuItemServFileDetails
        '
        Me.MenuItemServFileDetails.Index = 4
        Me.MenuItemServFileDetails.Text = "File details"
        '
        'MenuItemServSearch
        '
        Me.MenuItemServSearch.Index = 5
        Me.MenuItemServSearch.Text = "Internet search"
        '
        'MenuItemServPause
        '
        Me.MenuItemServPause.Index = 8
        Me.MenuItemServPause.Text = "Pause"
        '
        'MenuItemServStop
        '
        Me.MenuItemServStop.Index = 9
        Me.MenuItemServStop.Text = "Stop"
        '
        'MenuItemServStart
        '
        Me.MenuItemServStart.Index = 10
        Me.MenuItemServStart.Text = "Start"
        '
        'MenuItemServAutoStart
        '
        Me.MenuItemServAutoStart.Index = 0
        Me.MenuItemServAutoStart.Text = "Auto start"
        '
        'MenuItemServOnDemand
        '
        Me.MenuItemServOnDemand.Index = 1
        Me.MenuItemServOnDemand.Text = "On demand"
        '
        'MenuItemServDisabled
        '
        Me.MenuItemServDisabled.Index = 2
        Me.MenuItemServDisabled.Text = "Disabled"
        '
        'MenuItemServDepe
        '
        Me.MenuItemServDepe.Index = 6
        Me.MenuItemServDepe.Text = "Show dependencies..."
        '
        'MenuItemNetworkClose
        '
        Me.MenuItemNetworkClose.Index = 2
        Me.MenuItemNetworkClose.Text = "Close TCP connection"
        '
        'MenuItemServSelProc
        '
        Me.MenuItemServSelProc.DefaultItem = True
        Me.MenuItemServSelProc.Index = 0
        Me.MenuItemServSelProc.Text = "Select associated process"
        '
        'MenuItemSearchSel
        '
        Me.MenuItemSearchSel.Index = 2
        Me.MenuItemSearchSel.Text = "Select associated process/service"
        '
        'MenuItemSearchClose
        '
        Me.MenuItemSearchClose.Index = 3
        Me.MenuItemSearchClose.Text = "Close/terminate item"
        '
        'MenuItemProcSFileProp
        '
        Me.MenuItemProcSFileProp.Index = 11
        Me.MenuItemProcSFileProp.Text = "File properties"
        '
        'MenuItemProcSOpenDir
        '
        Me.MenuItemProcSOpenDir.Index = 12
        Me.MenuItemProcSOpenDir.Text = "Open directory"
        '
        'MenuItemProcSFileDetails
        '
        Me.MenuItemProcSFileDetails.Index = 13
        Me.MenuItemProcSFileDetails.Text = "File details"
        '
        'MenuItemProcSSearch
        '
        Me.MenuItemProcSSearch.Index = 14
        Me.MenuItemProcSSearch.Text = "Internet search"
        '
        'MenuItemProcSDep
        '
        Me.MenuItemProcSDep.Index = 15
        Me.MenuItemProcSDep.Text = "View dependencies..."
        '
        'MenuItemProcKill
        '
        Me.MenuItemProcKill.DefaultItem = True
        Me.MenuItemProcKill.Index = 0
        Me.MenuItemProcKill.Text = "Kill"
        '
        'MenuItemProcKillT
        '
        Me.MenuItemProcKillT.Index = 1
        Me.MenuItemProcKillT.Text = "Kill process tree"
        '
        'MenuItemProcStop
        '
        Me.MenuItemProcStop.Index = 3
        Me.MenuItemProcStop.Text = "Stop"
        '
        'MenuItemProcResume
        '
        Me.MenuItemProcResume.Index = 4
        Me.MenuItemProcResume.Text = "Resume"
        '
        'MenuItemProcPIdle
        '
        Me.MenuItemProcPIdle.Index = 0
        Me.MenuItemProcPIdle.Text = "Idle"
        '
        'MenuItemProcPBN
        '
        Me.MenuItemProcPBN.Index = 1
        Me.MenuItemProcPBN.Text = "Below normal"
        '
        'MenuItemProcPN
        '
        Me.MenuItemProcPN.Index = 2
        Me.MenuItemProcPN.Text = "Normal"
        '
        'MenuItemProcPAN
        '
        Me.MenuItemProcPAN.Index = 3
        Me.MenuItemProcPAN.Text = "Above normal"
        '
        'MenuItemProcPH
        '
        Me.MenuItemProcPH.Index = 4
        Me.MenuItemProcPH.Text = "High"
        '
        'MenuItemProcPRT
        '
        Me.MenuItemProcPRT.Index = 5
        Me.MenuItemProcPRT.Text = "Real time"
        '
        'MenuItemSystemRefresh
        '
        Me.MenuItemSystemRefresh.DefaultItem = True
        Me.MenuItemSystemRefresh.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.MenuItemSystemRefresh.Text = "&Refresh"
        '
        'MenuItemSystemLog
        '
        Me.MenuItemSystemLog.Text = "Show &log"
        '
        'MenuItemSystemInfos
        '
        Me.MenuItemSystemInfos.Text = "System &infos"
        '
        'MenuItemSystemOpenedWindows
        '
        Me.MenuItemSystemOpenedWindows.Text = "Opened &windows"
        '
        'MenuItemSystemToTray
        '
        Me.MenuItemSystemToTray.Text = "Minimize to &tray"
        '
        'MenuItemSystemExit
        '
        Me.MenuItemSystemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4
        Me.MenuItemSystemExit.Text = "E&xit"
        '
        'MenuItemSystemUpdate
        '
        Me.MenuItemSystemUpdate.Index = -1
        Me.MenuItemSystemUpdate.Text = "Check &updates"
        '
        'MenuItemSystemWebsite
        '
        Me.MenuItemSystemWebsite.Index = -1
        Me.MenuItemSystemWebsite.Text = ""
        '
        'MenuItemSystemAbout
        '
        Me.MenuItemSystemAbout.Index = -1
        Me.MenuItemSystemAbout.Text = ""
        '
        'MenuItemSystemFindWindow
        '
        Me.MenuItemSystemFindWindow.Text = "&Find a window"
        '
        'MenuItemSystemHelp
        '
        Me.MenuItemSystemHelp.Index = -1
        Me.MenuItemSystemHelp.Text = ""
        '
        'MenuItemSystemOptions
        '
        Me.MenuItemSystemOptions.DefaultItem = True
        Me.MenuItemSystemOptions.Index = 5
        Me.MenuItemSystemOptions.Text = "&Options..."
        '
        'MenuItemMainFindWindow
        '
        Me.MenuItemMainFindWindow.Text = "Find a window"
        '
        'MenuItemCopyTask
        '
        Me.MenuItemCopyTask.Index = 9
        Me.MenuItemCopyTask.Text = "Copy to clipboard"
        '
        'MenuItemCopyService
        '
        Me.MenuItemCopyService.Index = 16
        Me.MenuItemCopyService.Text = "Copy to clipboard"
        '
        'MenuItemCopyNetwork
        '
        Me.MenuItemCopyNetwork.Index = 5
        Me.MenuItemCopyNetwork.Text = "Copy to clipboard"
        '
        'MenuItemCopySearch
        '
        Me.MenuItemCopySearch.Index = 5
        Me.MenuItemCopySearch.Text = "Copy to clipboard"
        '
        'MenuItemCopyProcess
        '
        Me.MenuItemCopyProcess.Index = 17
        Me.MenuItemCopyProcess.Text = "Copy to clipboard"
        '
        'mnuTask
        '
        Me.mnuTask.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemTaskShow, Me.MenuItemTaskMax, Me.MenuItemTaskMin, Me.MenuItem4, Me.MenuItemTaskEnd, Me.MenuItem6, Me.MenuItemTaskSelProc, Me.MenuItemTaskSelectWindow, Me.MenuItem9, Me.MenuItemCopyTask, Me.MenuItemTaskColumns})
        '
        'MenuItemTaskMax
        '
        Me.MenuItemTaskMax.Index = 1
        Me.MenuItemTaskMax.Text = "Maximize"
        '
        'MenuItemTaskMin
        '
        Me.MenuItemTaskMin.Index = 2
        Me.MenuItemTaskMin.Text = "Minimize"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.Text = "-"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 5
        Me.MenuItem6.Text = "-"
        '
        'MenuItemTaskSelectWindow
        '
        Me.MenuItemTaskSelectWindow.Index = 7
        Me.MenuItemTaskSelectWindow.Text = "Select in ""window tab"""
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 8
        Me.MenuItem9.Text = "-"
        '
        'MenuItemTaskColumns
        '
        Me.MenuItemTaskColumns.Index = 10
        Me.MenuItemTaskColumns.Text = "Choose columns..."
        '
        'mnuMonitor
        '
        Me.mnuMonitor.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemMonitorAdd, Me.MenuItemMonitorRemove, Me.MenuItem1, Me.MenuItemMonitorStart, Me.MenuItemMonitorStop})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "-"
        '
        'mnuFileCpPctBig
        '
        Me.mnuFileCpPctBig.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopyBig})
        '
        'mnuFileCpPctSmall
        '
        Me.mnuFileCpPctSmall.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemCopySmall})
        '
        'mnuMain
        '
        Me.mnuMain.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemMainShow, Me.MenuItemMainToTray, Me.MenuItemMainAbout, Me.MenuItemMainAlwaysVisible, Me.MenuItem29, Me.MenuItem31, Me.MenuItem2, Me.MenuItem3, Me.MenuItem5, Me.MenuItemMainLog, Me.MenuItemMainReport, Me.MenuItemMainSysInfo, Me.MenuItemMainNetworkInfo, Me.MenuItemMainOpenedW, Me.MenuItemMainEmergencyH, Me.MenuItemMainFindWindow, Me.MenuItem15, Me.MenuItemRefProc, Me.MenuItemMainRefServ, Me.MenuItem18, Me.MenuItemMainExit})
        '
        'MenuItemMainAlwaysVisible
        '
        Me.MenuItemMainAlwaysVisible.Text = "Always visible"
        '
        'MenuItem29
        '
        Me.MenuItem29.Text = "-"
        '
        'MenuItem31
        '
        Me.MenuItem31.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemNotifNP, Me.MenuItemNotifTP, Me.MenuItemNotifNS, Me.MenuItemNotifDS, Me.MenuItem46, Me.MenuItemNotifAll, Me.MenuItemNotifNone})
        Me.MenuItem31.Text = "Notifications"
        '
        'MenuItemNotifNP
        '
        Me.MenuItemNotifNP.Index = 0
        Me.MenuItemNotifNP.Text = "&New processes"
        '
        'MenuItemNotifTP
        '
        Me.MenuItemNotifTP.Index = 1
        Me.MenuItemNotifTP.Text = "&Terminated processes"
        '
        'MenuItemNotifNS
        '
        Me.MenuItemNotifNS.Index = 2
        Me.MenuItemNotifNS.Text = "&New services"
        '
        'MenuItemNotifDS
        '
        Me.MenuItemNotifDS.Index = 3
        Me.MenuItemNotifDS.Text = "&Deleted services"
        '
        'MenuItem46
        '
        Me.MenuItem46.Index = 4
        Me.MenuItem46.Text = "-"
        '
        'MenuItemNotifAll
        '
        Me.MenuItemNotifAll.Index = 5
        Me.MenuItemNotifAll.Text = "&Enable all"
        '
        'MenuItemNotifNone
        '
        Me.MenuItemNotifNone.Index = 6
        Me.MenuItemNotifNone.Text = "&Disable all"
        '
        'MenuItem2
        '
        Me.MenuItem2.Text = "-"
        '
        'MenuItem3
        '
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemMainRestart, Me.MenuItemMainShutdown, Me.MenuItemMainPowerOff, Me.MenuItem11, Me.MenuItemMainSleep, Me.MenuItemMainHibernate, Me.MenuItem17, Me.MenuItemMainLogOff, Me.MenuItemMainLock})
        Me.MenuItem3.Text = "Shutdown"
        '
        'MenuItemMainRestart
        '
        Me.MenuItemMainRestart.Index = 0
        Me.MenuItemMainRestart.Text = "Restart"
        '
        'MenuItemMainShutdown
        '
        Me.MenuItemMainShutdown.Index = 1
        Me.MenuItemMainShutdown.Text = "Shutdown"
        '
        'MenuItemMainPowerOff
        '
        Me.MenuItemMainPowerOff.Index = 2
        Me.MenuItemMainPowerOff.Text = "Power off"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 3
        Me.MenuItem11.Text = "-"
        '
        'MenuItemMainSleep
        '
        Me.MenuItemMainSleep.Index = 4
        Me.MenuItemMainSleep.Text = "Sleep"
        '
        'MenuItemMainHibernate
        '
        Me.MenuItemMainHibernate.Index = 5
        Me.MenuItemMainHibernate.Text = "Hibernate"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 6
        Me.MenuItem17.Text = "-"
        '
        'MenuItemMainLogOff
        '
        Me.MenuItemMainLogOff.Index = 7
        Me.MenuItemMainLogOff.Text = "Log off"
        '
        'MenuItemMainLock
        '
        Me.MenuItemMainLock.Index = 8
        Me.MenuItemMainLock.Text = "Lock"
        '
        'MenuItem5
        '
        Me.MenuItem5.Text = "-"
        '
        'MenuItemMainReport
        '
        Me.MenuItemMainReport.Text = "Save system report..."
        '
        'MenuItemMainNetworkInfo
        '
        Me.MenuItemMainNetworkInfo.Text = "Network information"
        '
        'MenuItemMainEmergencyH
        '
        Me.MenuItemMainEmergencyH.Text = "Emergency hotkeys..."

        '
        'MenuItem15
        '
        Me.MenuItem15.Text = "-"
        '
        'MenuItemRefProc
        '
        Me.MenuItemRefProc.Checked = True
        Me.MenuItemRefProc.Text = "Refresh process list"
        '
        'MenuItemMainRefServ
        '
        Me.MenuItemMainRefServ.Checked = True
        Me.MenuItemMainRefServ.Text = "Refresh service list"
        '
        'MenuItem18
        '
        Me.MenuItem18.Text = "-"
        '
        'mnuService
        '
        Me.mnuService.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServSelService, Me.MenuItem7, Me.MenuItemServFileProp, Me.MenuItemServOpenDir, Me.MenuItemServFileDetails, Me.MenuItemServSearch, Me.MenuItemServDepe, Me.MenuItem20, Me.MenuItemServPause, Me.MenuItemServStop, Me.MenuItemServStart, Me.MenuItemServStartType, Me.MenuItemServDelete, Me.MenuItem25, Me.MenuItemServReanalize, Me.MenuItem24, Me.MenuItemCopyService, Me.MenuItemServColumns})
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 1
        Me.MenuItem7.Text = "-"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 7
        Me.MenuItem20.Text = "-"
        '
        'MenuItemServStartType
        '
        Me.MenuItemServStartType.Index = 11
        Me.MenuItemServStartType.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServAutoStart, Me.MenuItemServOnDemand, Me.MenuItemServDisabled})
        Me.MenuItemServStartType.Text = "Start type"
        '
        'MenuItemServDelete
        '
        Me.MenuItemServDelete.Index = 12
        Me.MenuItemServDelete.Text = "Delete"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 13
        Me.MenuItem25.Text = "-"
        '
        'MenuItemServReanalize
        '
        Me.MenuItemServReanalize.Index = 14
        Me.MenuItemServReanalize.Text = "Reanalyze"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 15
        Me.MenuItem24.Text = "-"
        '
        'MenuItemServColumns
        '
        Me.MenuItemServColumns.Index = 17
        Me.MenuItemServColumns.Text = "Choose columns..."
        '
        'mnuNetwork
        '
        Me.mnuNetwork.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemServSelProc, Me.MenuItem16, Me.MenuItemNetworkClose, Me.MenuItemNetworkTools, Me.MenuItem10, Me.MenuItemCopyNetwork, Me.MenuItemNetworkColumns})
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 1
        Me.MenuItem16.Text = "-"
        '
        'MenuItemNetworkTools
        '
        Me.MenuItemNetworkTools.Index = 3
        Me.MenuItemNetworkTools.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemNetworkPing})
        Me.MenuItemNetworkTools.Text = "Tools"
        '
        'MenuItemNetworkPing
        '
        Me.MenuItemNetworkPing.Text = "Ping"


        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 4
        Me.MenuItem10.Text = "-"
        '
        'MenuItemNetworkColumns
        '
        Me.MenuItemNetworkColumns.Index = 6
        Me.MenuItemNetworkColumns.Text = "Choose columns..."
        '
        'mnuSearch
        '
        Me.mnuSearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSearchNew, Me.MenuItem28, Me.MenuItemSearchSel, Me.MenuItemSearchClose, Me.MenuItem30, Me.MenuItemCopySearch})
        '
        'MenuItemSearchNew
        '
        Me.MenuItemSearchNew.DefaultItem = True
        Me.MenuItemSearchNew.Index = 0
        Me.MenuItemSearchNew.Text = "New search..."
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 1
        Me.MenuItem28.Text = "-"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 4
        Me.MenuItem30.Text = "-"
        '
        'mnuProcess
        '
        Me.mnuProcess.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcKill, Me.MenuItemProcKillT, Me.MenuItemProcKillByMethod, Me.MenuItemProcStop, Me.MenuItemProcResume, Me.MenuItemProcPriority, Me.MenuItem44, Me.MenuItemProcReanalize, Me.MenuItemProcOther, Me.MenuItemProcJob, Me.MenuItem51, Me.MenuItemProcSFileProp, Me.MenuItemProcSOpenDir, Me.MenuItemProcSFileDetails, Me.MenuItemProcSSearch, Me.MenuItemProcSDep, Me.MenuItem38, Me.MenuItemCopyProcess, Me.MenuItemProcColumns})
        '
        'MenuItemProcKillByMethod
        '
        Me.MenuItemProcKillByMethod.Index = 2
        Me.MenuItemProcKillByMethod.Text = "Kill process by method..."
        '
        'MenuItemProcPriority
        '
        Me.MenuItemProcPriority.Index = 5
        Me.MenuItemProcPriority.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcPIdle, Me.MenuItemProcPBN, Me.MenuItemProcPN, Me.MenuItemProcPAN, Me.MenuItemProcPH, Me.MenuItemProcPRT})
        Me.MenuItemProcPriority.Text = "Priority"
        '
        'MenuItem44
        '
        Me.MenuItem44.Index = 6
        Me.MenuItem44.Text = "-"
        '
        'MenuItemProcReanalize
        '
        Me.MenuItemProcReanalize.Index = 7
        Me.MenuItemProcReanalize.Text = "Reanalize"
        '
        'MenuItemProcOther
        '
        Me.MenuItemProcOther.Index = 8
        Me.MenuItemProcOther.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcWSS, Me.MenuItemProcAff, Me.MenuItemProcDump})
        Me.MenuItemProcOther.Text = "Other"
        '
        'MenuItemProcWSS
        '
        Me.MenuItemProcWSS.Index = 0
        Me.MenuItemProcWSS.Text = "Reduce working set size"
        '
        'MenuItemProcAff
        '
        Me.MenuItemProcAff.Index = 1
        Me.MenuItemProcAff.Text = "Set affinity..."
        '
        'MenuItemProcDump
        '
        Me.MenuItemProcDump.Index = 2
        Me.MenuItemProcDump.Text = "Create dump file..."
        '
        'MenuItemProcJob
        '
        Me.MenuItemProcJob.Index = 9
        Me.MenuItemProcJob.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcAddToJob, Me.MenuItem33, Me.MenuItemJobMng})
        Me.MenuItemProcJob.Text = "&Job"
        '
        'MenuItemProcAddToJob
        '
        Me.MenuItemProcAddToJob.Index = 0
        Me.MenuItemProcAddToJob.Text = "&Add to job..."
        '
        'MenuItem33
        '
        Me.MenuItem33.Index = 1
        Me.MenuItem33.Text = "-"
        '
        'MenuItemJobMng
        '
        Me.MenuItemJobMng.Index = 2
        Me.MenuItemJobMng.Text = "&Control process job..."
        '
        'MenuItem51
        '
        Me.MenuItem51.Index = 10
        Me.MenuItem51.Text = "-"
        '
        'MenuItem38
        '
        Me.MenuItem38.Index = 16
        Me.MenuItem38.Text = "-"
        '
        'MenuItemProcColumns
        '
        Me.MenuItemProcColumns.Index = 18
        Me.MenuItemProcColumns.Text = "Choose columns..."
        '
        'mnuSystem
        '
        Me.mnuSystem.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSYSTEMFILE, Me.MenuItemProcesses, Me.MenuItemJobs, Me.MenuItemMonitor, Me.MenuItemServices, Me.MenuItemSearch, Me.MenuItemFiles, Me.MenuItemSYSTEMOPT, Me.MenuItemSYSTEMTOOLS, Me.MenuItemSYSTEMSYSTEM, Me.MenuItemSYSTEMHEL})
        '
        'MenuItemSYSTEMFILE
        '
        Me.MenuItemSYSTEMFILE.Index = 0
        Me.MenuItemSYSTEMFILE.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemRefresh, Me.MenuItemSystemNewProcess, Me.MenuItem54, Me.MenuItemSystemConnection, Me.MenuItemRunAsAdmin, Me.MenuItem56, Me.MenuItemSystemLog, Me.MenuItemSystemReport, Me.MenuItem59, Me.MenuItemSystemInfos, Me.MenuItemSystemNetworkInfos, Me.MenuItemSystemOpenedWindows, Me.MenuItemShowPendingTasks, Me.MenuItemSystemScripting, Me.MenuItemSystemSaveSSFile, Me.MenuItemSystemExploreSSFile, Me.MenuItemSystemCheckSignatures, Me.MenuItem62, Me.MenuItemSystemFindWindow, Me.MenuItemSystemEmergency, Me.MenuItem66, Me.MenuItemSystemToTray, Me.MenuItemSystemExit})
        Me.MenuItemSYSTEMFILE.Text = "&File"
        '
        'MenuItem54
        '
        Me.MenuItem54.Text = "-"
        '
        'MenuItemSystemConnection
        '
        Me.MenuItemSystemConnection.Text = "&Connection..."
        '
        'MenuItemRunAsAdmin
        '
        Me.MenuItemRunAsAdmin.Text = "&Restart with privileges"
        '
        'MenuItem56
        '
        Me.MenuItem56.Text = "-"
        '
        'MenuItemSystemReport
        '
        Me.MenuItemSystemReport.Text = "Save &system report..."
        '
        'MenuItem59
        '
        Me.MenuItem59.Text = "-"
        '
        'MenuItemSystemNetworkInfos
        '
        Me.MenuItemSystemNetworkInfos.Text = "&Network infos"
        '
        'MenuItemShowPendingTasks
        '
        Me.MenuItemShowPendingTasks.Text = "&Pending tasks..."
        '
        'MenuItemSystemScripting
        '
        Me.MenuItemSystemScripting.Text = "&Scripting..."
        '
        'MenuItemSystemSaveSSFile
        '
        Me.MenuItemSystemSaveSSFile.Text = "&Save System Snapshot File..."
        '
        'MenuItemSystemExploreSSFile
        '
        Me.MenuItemSystemExploreSSFile.Text = "&Explore System Snapshot File..."
        '
        'MenuItemSystemCheckSignatures
        '
        Me.MenuItemSystemCheckSignatures.Text = "Check file signatures..."
        '
        'MenuItem62
        '
        Me.MenuItem62.Text = "-"
        '
        'MenuItemSystemEmergency
        '
        Me.MenuItemSystemEmergency.Text = "Emergency &hotkeys..."

        '
        'MenuItem66
        '
        Me.MenuItem66.Text = "-"
        '
        'MenuItemProcesses
        '
        Me.MenuItemProcesses.Index = 1
        Me.MenuItemProcesses.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemProcNewProcess, Me.MenuItemProcSep1, Me.MenuItemReportProcesses})
        Me.MenuItemProcesses.Text = "&Processes"
        Me.MenuItemProcesses.Visible = False
        '
        'MenuItemReportProcesses
        '
        Me.MenuItemReportProcesses.Text = "&Save report..."
        '
        'MenuItemJobs
        '
        Me.MenuItemJobs.Index = 2
        Me.MenuItemJobs.Text = "&Jobs"
        Me.MenuItemJobs.Visible = False
        '
        'MenuItemMonitor
        '
        Me.MenuItemMonitor.Index = 3
        Me.MenuItemMonitor.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportMonitor})
        Me.MenuItemMonitor.Text = "&Monitor"
        Me.MenuItemMonitor.Visible = False
        '
        'MenuItemReportMonitor
        '
        Me.MenuItemReportMonitor.Index = 0
        Me.MenuItemReportMonitor.Text = "&Save report..."
        '
        'MenuItemServices
        '
        Me.MenuItemServices.Index = 4
        Me.MenuItemServices.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemReportServices})
        Me.MenuItemServices.Text = "&Services"
        Me.MenuItemServices.Visible = False
        '
        'MenuItemReportServices
        '
        Me.MenuItemReportServices.Index = 0
        Me.MenuItemReportServices.Text = "&Save report..."
        '
        'MenuItemSearch
        '
        Me.MenuItemSearch.Index = 5
        Me.MenuItemSearch.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemNewSearch, Me.MenuItem61, Me.MenuItemReportSearch})
        Me.MenuItemSearch.Text = "&Search"
        Me.MenuItemSearch.Visible = False
        '
        'MenuItemNewSearch
        '
        Me.MenuItemNewSearch.DefaultItem = True
        Me.MenuItemNewSearch.Index = 0
        Me.MenuItemNewSearch.Text = "&New search..."
        '
        'MenuItem61
        '
        Me.MenuItem61.Index = 1
        Me.MenuItem61.Text = "-"
        '
        'MenuItemReportSearch
        '
        Me.MenuItemReportSearch.Index = 2
        Me.MenuItemReportSearch.Text = "&Save report..."
        '
        'MenuItemFiles
        '
        Me.MenuItemFiles.Index = 6
        Me.MenuItemFiles.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemFileOpen, Me.MenuItem68, Me.MenuItemFileRelease, Me.MenuItemFileDelete, Me.MenuItemFileTrash, Me.MenuItem72, Me.MenuItemFileRename, Me.MenuItemFileShellOpen, Me.MenuItemFileMove, Me.MenuItemFileCopy, Me.MenuItem77, Me.MenuItemFileEncrypt, Me.MenuItemFileDecrypt, Me.MenuItem80, Me.MenuItemFileStrings, Me.MenuItemFileSep2, Me.MenuItemFileProperties, Me.MenuItemFileOpenDir, Me.MenuItemFileDirProperties, Me.MenuItemFileSearchInternet})
        Me.MenuItemFiles.Text = "&Files"
        Me.MenuItemFiles.Visible = False
        '
        'MenuItemFileOpen
        '
        Me.MenuItemFileOpen.DefaultItem = True
        Me.MenuItemFileOpen.Text = "&Open file..."
        '
        'MenuItem68
        '
        Me.MenuItem68.Text = "-"
        '
        'MenuItemFileRelease
        '
        Me.MenuItemFileRelease.Enabled = False
        Me.MenuItemFileRelease.Text = "&Release file..."
        '
        'MenuItemFileDelete
        '
        Me.MenuItemFileDelete.Text = "&Delete file..."
        '
        'MenuItemFileTrash
        '
        Me.MenuItemFileTrash.Text = "&Move to trash..."
        '
        'MenuItem72
        '
        Me.MenuItem72.Text = "-"
        '
        'MenuItemFileRename
        '
        Me.MenuItemFileRename.Text = "&Rename..."
        '
        'MenuItemFileShellOpen
        '
        Me.MenuItemFileShellOpen.Text = "&Open"
        '
        'MenuItemFileMove
        '
        Me.MenuItemFileMove.Text = "&Move..."
        '
        'MenuItemFileCopy
        '
        Me.MenuItemFileCopy.Text = "&Copy..."
        '
        'MenuItem77
        '
        Me.MenuItem77.Text = "-"
        '
        'MenuItemFileEncrypt
        '
        Me.MenuItemFileEncrypt.Text = "&Encrypt"
        '
        'MenuItemFileDecrypt
        '
        Me.MenuItemFileDecrypt.Text = "&Decrypt"
        '
        'MenuItem80
        '
        Me.MenuItem80.Text = "-"
        '
        'MenuItemFileStrings
        '
        Me.MenuItemFileStrings.Text = "&Show file strings"
        '
        'MenuItemSYSTEMOPT
        '
        Me.MenuItemSYSTEMOPT.Index = 7
        Me.MenuItemSYSTEMOPT.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemAlwaysVisible, Me.MenuItem37, Me.MenuItemSystemRefProc, Me.MenuItemSystemRefServ, Me.MenuItem42, Me.MenuItemSystemOptions})
        Me.MenuItemSYSTEMOPT.Text = "&Options"
        '
        'MenuItemSystemAlwaysVisible
        '
        Me.MenuItemSystemAlwaysVisible.Index = 0
        Me.MenuItemSystemAlwaysVisible.Text = "&Always visible"
        '
        'MenuItem37
        '
        Me.MenuItem37.Index = 1
        Me.MenuItem37.Text = "-"
        '
        'MenuItemSystemRefProc
        '
        Me.MenuItemSystemRefProc.Checked = True
        Me.MenuItemSystemRefProc.Index = 2
        Me.MenuItemSystemRefProc.Text = "Refresh &process list"
        '
        'MenuItemSystemRefServ
        '
        Me.MenuItemSystemRefServ.Checked = True
        Me.MenuItemSystemRefServ.Index = 3
        Me.MenuItemSystemRefServ.Text = "Refresh &service list"
        '
        'MenuItem42
        '
        Me.MenuItem42.Index = 4
        Me.MenuItem42.Text = "-"
        '
        'MenuItemSYSTEMTOOLS
        '
        Me.MenuItemSYSTEMTOOLS.Index = 8
        Me.MenuItemSYSTEMTOOLS.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemShowHidden, Me.MenuItemSystemDependency, Me.MenuItemCreateService})
        Me.MenuItemSYSTEMTOOLS.Text = "&Tools"
        '
        'MenuItemSystemShowHidden
        '
        Me.MenuItemSystemShowHidden.Index = 0
        Me.MenuItemSystemShowHidden.Text = "Show &hidden processes..."
        '
        'MenuItemSystemDependency
        '
        Me.MenuItemSystemDependency.Index = 1
        Me.MenuItemSystemDependency.Text = "&Dependency viewer..."
        '
        'MenuItemCreateService
        '
        Me.MenuItemCreateService.Index = 2
        Me.MenuItemCreateService.Text = "&Create service..."
        '
        'MenuItemSYSTEMSYSTEM
        '
        Me.MenuItemSYSTEMSYSTEM.Index = 9
        Me.MenuItemSYSTEMSYSTEM.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem34})
        Me.MenuItemSYSTEMSYSTEM.Text = "&System"
        '
        'MenuItem34
        '
        Me.MenuItem34.Index = 0
        Me.MenuItem34.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemSystemRestart, Me.MenuItemSystemShutdown, Me.MenuItemSystemPowerOff, Me.MenuItem40, Me.MenuItemSystemSleep, Me.MenuItemSystemHIbernate, Me.MenuItem43, Me.MenuItemSystemLogoff, Me.MenuItemSystemLock})
        Me.MenuItem34.Text = "Shutdown"
        '
        'MenuItemSystemRestart
        '
        Me.MenuItemSystemRestart.Index = 0
        Me.MenuItemSystemRestart.Text = "Restart"
        '
        'MenuItemSystemShutdown
        '
        Me.MenuItemSystemShutdown.Index = 1
        Me.MenuItemSystemShutdown.Text = "Shutdown"
        '
        'MenuItemSystemPowerOff
        '
        Me.MenuItemSystemPowerOff.Index = 2
        Me.MenuItemSystemPowerOff.Text = "Power off"
        '
        'MenuItem40
        '
        Me.MenuItem40.Index = 3
        Me.MenuItem40.Text = "-"
        '
        'MenuItemSystemSleep
        '
        Me.MenuItemSystemSleep.Index = 4
        Me.MenuItemSystemSleep.Text = "Sleep"
        '
        'MenuItemSystemHIbernate
        '
        Me.MenuItemSystemHIbernate.Index = 5
        Me.MenuItemSystemHIbernate.Text = "Hibernate"
        '
        'MenuItem43
        '
        Me.MenuItem43.Index = 6
        Me.MenuItem43.Text = ""
        '
        'MenuItemSystemLogoff
        '
        Me.MenuItemSystemLogoff.Index = 7
        Me.MenuItemSystemLogoff.Text = "Log off"
        '
        'MenuItemSystemLock
        '
        Me.MenuItemSystemLock.Index = 8
        Me.MenuItemSystemLock.Text = "Lock"
        '
        'MenuItemSystemNewProcess
        '
        Me.MenuItemSystemNewProcess.Text = "Run a &new process..."
        '
        'MenuItemProcNewProcess
        '
        Me.MenuItemProcNewProcess.Text = "Run a &new process..."
        '
        'MenuItemProcSep1
        '
        Me.MenuItemProcSep1.Text = "-"
        '
        'MenuItemHelpAbout
        '
        Me.MenuItemHelpAbout.Text = "&About LitePM..."
        '
        'MenuItemFileSep2
        '
        Me.MenuItemFileSep2.Text = "-"
        '
        'MenuItemFileProperties
        '
        Me.MenuItemFileProperties.Text = "File &properties"
        '
        'MenuItemFileOpenDir
        '
        Me.MenuItemFileOpenDir.Text = "Open file &directory"
        '
        'MenuItemFileDirProperties
        '
        Me.MenuItemFileDirProperties.Text = "Directory propert&ies"
        '
        'MenuItemFileSearchInternet
        '
        Me.MenuItemFileSearchInternet.Text = "&Internet search"
        '
        'MenuItemSYSTEMHEL
        '
        Me.MenuItemSYSTEMHEL.Index = 10
        Me.MenuItemSYSTEMHEL.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemHelpAbout})
        Me.MenuItemSYSTEMHEL.Text = "&Help"
        '
        'MenuItem39
        '
        Me.MenuItem39.Index = -1
        Me.MenuItem39.Text = ""
        '
        'MenuItemSystemDonation
        '
        Me.MenuItemSystemDonation.Index = -1
        Me.MenuItemSystemDonation.Text = ""
        '
        'MenuItemSystemFeedBack
        '
        Me.MenuItemSystemFeedBack.Index = -1
        Me.MenuItemSystemFeedBack.Text = ""
        '
        'MenuItemSystemSF
        '
        Me.MenuItemSystemSF.Index = -1
        Me.MenuItemSystemSF.Text = ""
        '
        'MenuItemSystemDownloads
        '
        Me.MenuItemSystemDownloads.Index = -1
        Me.MenuItemSystemDownloads.Text = "&Downloads"
        '
        'MenuItem49
        '
        Me.MenuItem49.Index = -1
        Me.MenuItem49.Text = "-"
        '
        'MenuItemJobTerminate
        '
        Me.MenuItemJobTerminate.Index = 0
        Me.MenuItemJobTerminate.Text = "Terminate job"
        '
        'MenuItemCopyJob
        '
        Me.MenuItemCopyJob.Index = 2
        Me.MenuItemCopyJob.Text = "Copy to clipboard"
        '
        'StatusBar
        '
        Me.StatusBar.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.Location = New System.Drawing.Point(0, 1089)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.sbPanelConnection, Me.sbPanelProcesses, Me.sbPanelServices, Me.sbPanelCpu, Me.sbPanelMemory})
        Me.StatusBar.ShowPanels = True
        Me.StatusBar.Size = New System.Drawing.Size(1708, 49)
        Me.StatusBar.TabIndex = 62
        '
        'sbPanelConnection
        '
        Me.sbPanelConnection.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelConnection.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.Raised
        Me.sbPanelConnection.MinWidth = 100
        Me.sbPanelConnection.Name = "sbPanelConnection"
        Me.sbPanelConnection.Text = "Localhost"
        '
        'sbPanelProcesses
        '
        Me.sbPanelProcesses.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelProcesses.MinWidth = 80
        Me.sbPanelProcesses.Name = "sbPanelProcesses"
        Me.sbPanelProcesses.Text = "0 processes"
        Me.sbPanelProcesses.Width = 88
        '
        'sbPanelServices
        '
        Me.sbPanelServices.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelServices.MinWidth = 80
        Me.sbPanelServices.Name = "sbPanelServices"
        Me.sbPanelServices.Text = "0 services"
        Me.sbPanelServices.Width = 80
        '
        'sbPanelCpu
        '
        Me.sbPanelCpu.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelCpu.MinWidth = 80
        Me.sbPanelCpu.Name = "sbPanelCpu"
        Me.sbPanelCpu.Text = "CPU : 0%"
        Me.sbPanelCpu.Width = 80
        '
        'sbPanelMemory
        '
        Me.sbPanelMemory.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.sbPanelMemory.MinWidth = 120
        Me.sbPanelMemory.Name = "sbPanelMemory"
        Me.sbPanelMemory.Text = "Phys. Memory : 0%"
        Me.sbPanelMemory.Width = 133
        '
        'timerStatus
        '
        Me.timerStatus.Enabled = True
        Me.timerStatus.Interval = 1000
        '
        'timerJobs
        '
        Me.timerJobs.Enabled = True
        Me.timerJobs.Interval = 1000
        '
        'mnuJob
        '
        Me.mnuJob.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemJobTerminate, Me.MenuItem53, Me.MenuItemCopyJob})
        '
        'MenuItem53
        '
        Me.MenuItem53.Index = 1
        Me.MenuItem53.Text = "-"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1708, 1138)
        Me.Controls.Add(Me._main)
        Me.Controls.Add(Me.StatusBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mnuSystem
        Me.MinimumSize = New System.Drawing.Size(882, 589)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lite Process Monitor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me._main.Panel1.ResumeLayout(False)
        Me._main.Panel2.ResumeLayout(False)
        CType(Me._main, System.ComponentModel.ISupportInitialize).EndInit()
        Me._main.ResumeLayout(False)
        Me.containerSystemMenu.Panel2.ResumeLayout(False)
        CType(Me.containerSystemMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.containerSystemMenu.ResumeLayout(False)
        Me._tab.ResumeLayout(False)
        Me.pageTasks.ResumeLayout(False)
        Me.panelMain13.ResumeLayout(False)
        Me.SplitContainerTask.Panel1.ResumeLayout(False)
        Me.SplitContainerTask.Panel1.PerformLayout()
        Me.SplitContainerTask.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerTask, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTask.ResumeLayout(False)
        Me.pageProcesses.ResumeLayout(False)
        Me.containerProcessPage.Panel1.ResumeLayout(False)
        Me.containerProcessPage.Panel2.ResumeLayout(False)
        CType(Me.containerProcessPage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.containerProcessPage.ResumeLayout(False)
        Me.panelMenu.ResumeLayout(False)
        Me.panelMenu.PerformLayout()
        Me.panelMain.ResumeLayout(False)
        Me.SplitContainerProcess.Panel1.ResumeLayout(False)
        CType(Me.SplitContainerProcess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerProcess.ResumeLayout(False)
        Me.SplitContainerTvLv.Panel1.ResumeLayout(False)
        Me.SplitContainerTvLv.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerTvLv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTvLv.ResumeLayout(False)
        Me.pageJobs.ResumeLayout(False)
        Me.panelMain12.ResumeLayout(False)
        Me.pageMonitor.ResumeLayout(False)
        Me.panelMainMonitoring.ResumeLayout(False)
        Me.splitMonitor.Panel1.ResumeLayout(False)
        Me.splitMonitor.Panel2.ResumeLayout(False)
        CType(Me.splitMonitor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMonitor.ResumeLayout(False)
        Me.splitMonitor2.Panel1.ResumeLayout(False)
        Me.splitMonitor2.Panel1.PerformLayout()
        Me.splitMonitor2.Panel2.ResumeLayout(False)
        CType(Me.splitMonitor2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMonitor2.ResumeLayout(False)
        Me.splitMonitor3.Panel1.ResumeLayout(False)
        Me.splitMonitor3.Panel2.ResumeLayout(False)
        Me.splitMonitor3.Panel2.PerformLayout()
        CType(Me.splitMonitor3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMonitor3.ResumeLayout(False)
        Me.splitMonitor4.Panel2.ResumeLayout(False)
        CType(Me.splitMonitor4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMonitor4.ResumeLayout(False)
        CType(Me.graphMonitor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pageServices.ResumeLayout(False)
        Me.containerServicesPage.Panel1.ResumeLayout(False)
        Me.containerServicesPage.Panel2.ResumeLayout(False)
        CType(Me.containerServicesPage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.containerServicesPage.ResumeLayout(False)
        Me.panelMenu2.ResumeLayout(False)
        Me.panelMenu2.PerformLayout()
        Me.panelMain2.ResumeLayout(False)
        Me.splitServices.Panel1.ResumeLayout(False)
        Me.splitServices.Panel2.ResumeLayout(False)
        CType(Me.splitServices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitServices.ResumeLayout(False)
        Me.splitServices2.Panel1.ResumeLayout(False)
        Me.splitServices2.Panel1.PerformLayout()
        Me.splitServices2.Panel2.ResumeLayout(False)
        CType(Me.splitServices2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitServices2.ResumeLayout(False)
        Me.splitServices3.Panel1.ResumeLayout(False)
        Me.splitServices3.Panel2.ResumeLayout(False)
        CType(Me.splitServices3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitServices3.ResumeLayout(False)
        Me.splitServices4.Panel1.ResumeLayout(False)
        Me.splitServices4.Panel2.ResumeLayout(False)
        CType(Me.splitServices4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitServices4.ResumeLayout(False)
        Me.pageNetwork.ResumeLayout(False)
        Me.panelMain14.ResumeLayout(False)
        Me.pageFile.ResumeLayout(False)
        Me.panelMain5.ResumeLayout(False)
        Me.SplitContainerFilexx.Panel1.ResumeLayout(False)
        Me.SplitContainerFilexx.Panel1.PerformLayout()
        Me.SplitContainerFilexx.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerFilexx, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerFilexx.ResumeLayout(False)
        CType(Me.pctFileSmall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctFileBig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerFile.Panel1.ResumeLayout(False)
        Me.SplitContainerFile.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerFile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerFile.ResumeLayout(False)
        Me.SplitContainerFile2.Panel1.ResumeLayout(False)
        Me.SplitContainerFile2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerFile2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerFile2.ResumeLayout(False)
        Me.gpFileAttributes.ResumeLayout(False)
        Me.gpFileAttributes.PerformLayout()
        Me.gpFileDates.ResumeLayout(False)
        Me.gpFileDates.PerformLayout()
        Me.pageSearch.ResumeLayout(False)
        Me.panelMain6.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.ResumeLayout(False)
        Me.SplitContainerSearch.Panel1.PerformLayout()
        Me.SplitContainerSearch.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerSearch.ResumeLayout(False)
        Me.pageHelp.ResumeLayout(False)
        CType(Me.sbPanelConnection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelProcesses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelServices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelCpu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbPanelMemory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents timerProcess As System.Windows.Forms.Timer
    Friend WithEvents timerServices As System.Windows.Forms.Timer
    Friend WithEvents imgServices As System.Windows.Forms.ImageList
    Friend WithEvents Tray As System.Windows.Forms.NotifyIcon
    Friend WithEvents saveDial As System.Windows.Forms.SaveFileDialog
    Friend WithEvents openDial As System.Windows.Forms.OpenFileDialog
    Friend WithEvents imgMonitor As System.Windows.Forms.ImageList
    Friend WithEvents timerMonitoring As System.Windows.Forms.Timer
    Friend WithEvents FolderChooser As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents EnableServiceRefreshingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timerTask As System.Windows.Forms.Timer
    Friend WithEvents timerTrayIcon As System.Windows.Forms.Timer
    Friend WithEvents _main As System.Windows.Forms.SplitContainer
    Friend WithEvents containerSystemMenu As System.Windows.Forms.SplitContainer
    Friend WithEvents _tab As System.Windows.Forms.TabControl
    Friend WithEvents pageTasks As System.Windows.Forms.TabPage
    Friend WithEvents panelMain13 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerTask As System.Windows.Forms.SplitContainer
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblTaskCountResult As System.Windows.Forms.Label
    Friend WithEvents txtSearchTask As System.Windows.Forms.TextBox
    Friend WithEvents lvTask As mainTaskList
    Friend WithEvents ColumnHeader62 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader63 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader64 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageProcesses As System.Windows.Forms.TabPage
    Friend WithEvents containerProcessPage As System.Windows.Forms.SplitContainer
    Friend WithEvents panelMenu As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblResCount As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents panelMain As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerProcess As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerTvLv As System.Windows.Forms.SplitContainer
    Friend WithEvents tvProc As System.Windows.Forms.TreeView
    Friend WithEvents lvProcess As mainProcessList
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageMonitor As System.Windows.Forms.TabPage
    Friend WithEvents panelMainMonitoring As System.Windows.Forms.Panel
    Friend WithEvents splitMonitor As System.Windows.Forms.SplitContainer
    Friend WithEvents tvMonitor As System.Windows.Forms.TreeView
    Friend WithEvents splitMonitor2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtMonitoringLog As System.Windows.Forms.TextBox
    Friend WithEvents lvMonitorReport As DoubleBufferedLV
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader47 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader48 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader49 As System.Windows.Forms.ColumnHeader
    Friend WithEvents splitMonitor3 As System.Windows.Forms.SplitContainer
    Friend WithEvents splitMonitor4 As System.Windows.Forms.SplitContainer
    Friend WithEvents graphMonitor As Graph
    Friend WithEvents txtMonitorNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblMonitorMaxNumber As System.Windows.Forms.Label
    Friend WithEvents chkMonitorRightAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonitorLeftAuto As System.Windows.Forms.CheckBox
    Friend WithEvents dtMonitorR As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtMonitorL As System.Windows.Forms.DateTimePicker
    Friend WithEvents pageServices As System.Windows.Forms.TabPage
    Friend WithEvents containerServicesPage As System.Windows.Forms.SplitContainer
    Friend WithEvents panelMenu2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblResCount2 As System.Windows.Forms.Label
    Friend WithEvents txtServiceSearch As System.Windows.Forms.TextBox
    Friend WithEvents panelMain2 As System.Windows.Forms.Panel
    Friend WithEvents splitServices As System.Windows.Forms.SplitContainer
    Friend WithEvents lvServices As mainServiceList
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents splitServices2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cmdCopyServiceToCp As System.Windows.Forms.Button
    Friend WithEvents lblServicePath As System.Windows.Forms.TextBox
    Friend WithEvents lblServiceName As System.Windows.Forms.Label
    Friend WithEvents splitServices3 As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb2 As System.Windows.Forms.RichTextBox
    Friend WithEvents splitServices4 As System.Windows.Forms.SplitContainer
    Friend WithEvents tv2 As serviceDependenciesList
    Friend WithEvents tv As serviceDependenciesList
    Friend WithEvents pageNetwork As System.Windows.Forms.TabPage
    Friend WithEvents panelMain14 As System.Windows.Forms.Panel
    Friend WithEvents lvNetwork As mainNetworkConnectionsList
    Friend WithEvents ColumnHeader66 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader67 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader68 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader69 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageFile As System.Windows.Forms.TabPage
    Friend WithEvents panelMain5 As System.Windows.Forms.Panel
    Friend WithEvents pageSearch As System.Windows.Forms.TabPage
    Friend WithEvents panelMain6 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerSearch As System.Windows.Forms.SplitContainer
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblResultsCount As System.Windows.Forms.Label
    Friend WithEvents txtSearchResults As System.Windows.Forms.TextBox
    Friend WithEvents chkSearchWindows As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchHandles As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSearchModules As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchServices As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchProcess As System.Windows.Forms.CheckBox
    Friend WithEvents chkSearchCase As System.Windows.Forms.CheckBox
    Friend WithEvents lvSearchResults As searchList
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents pageHelp As System.Windows.Forms.TabPage
    Friend WithEvents panelMain4 As System.Windows.Forms.Panel
    'Friend WithEvents WBHelp As System.Windows.Forms.WebBrowser
    Friend WithEvents timerNetwork As System.Windows.Forms.Timer
    Friend WithEvents SplitContainerFilexx As System.Windows.Forms.SplitContainer
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdFileClipboard As System.Windows.Forms.Button
    Friend WithEvents pctFileSmall As System.Windows.Forms.PictureBox
    Friend WithEvents pctFileBig As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainerFile2 As System.Windows.Forms.SplitContainer
    Friend WithEvents rtb3 As System.Windows.Forms.RichTextBox
    Friend WithEvents gpFileAttributes As System.Windows.Forms.GroupBox
    Friend WithEvents chkFileEncrypted As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileContentNotIndexed As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileNormal As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileSystem As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileReadOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileHidden As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileCompressed As System.Windows.Forms.CheckBox
    Friend WithEvents chkFileArchive As System.Windows.Forms.CheckBox
    Friend WithEvents gpFileDates As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSetFileDates As System.Windows.Forms.Button
    Friend WithEvents DTlastModification As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTlastAccess As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTcreation As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SplitContainerFile As System.Windows.Forms.SplitContainer
    Friend WithEvents lvFileString As DoubleBufferedLV
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkSearchEnvVar As System.Windows.Forms.CheckBox
    'Friend WithEvents VistaMenu As wyDay.Controls.VistaMenu
    Private WithEvents mnuTask As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemTaskShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskMax As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskMin As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskEnd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuMonitor As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemMonitorAdd As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitorRemove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitorStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitorStop As System.Windows.Forms.MenuItem
    Private WithEvents mnuFileCpPctBig As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopyBig As System.Windows.Forms.MenuItem
    Private WithEvents mnuFileCpPctSmall As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemCopySmall As System.Windows.Forms.MenuItem
    Private WithEvents mnuMain As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemMainShow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainToTray As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainAbout As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainAlwaysVisible As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainLog As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainSysInfo As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainOpenedW As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainEmergencyH As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainFindWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemRefProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainRefServ As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainRestart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainShutdown As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainPowerOff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainSleep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainHibernate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainLogOff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainLock As System.Windows.Forms.MenuItem
    Private WithEvents mnuService As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemServSelService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDepe As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServPause As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStop As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServStartType As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServAutoStart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServOnDemand As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDisabled As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServReanalize As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuNetwork As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemServSelProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkColumns As System.Windows.Forms.MenuItem
    Private WithEvents mnuSearch As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemSearchNew As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSearchClose As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSearchSel As System.Windows.Forms.MenuItem
    Private WithEvents mnuProcess As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemProcSFileProp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSFileDetails As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSDep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem38 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcColumns As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcKill As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcKillT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcStop As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcResume As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPriority As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcReanalize As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem44 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem51 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPIdle As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPBN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPAN As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPH As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcPRT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcOther As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcWSS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcAff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcDump As System.Windows.Forms.MenuItem
    Private WithEvents mnuSystem As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItemSYSTEMFILE As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMOPT As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMTOOLS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMSYSTEM As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSYSTEMHEL As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemNewProcess As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcNewProcess As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcSep1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemHelpAbout As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileSep2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileProperties As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileOpenDir As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileDirProperties As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileSearchInternet As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem34 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRestart As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemShutdown As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemPowerOff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem40 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemSleep As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemHIbernate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem43 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemLogoff As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemLock As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemShowHidden As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemDependency As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemAlwaysVisible As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem37 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRefProc As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRefServ As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem42 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemOptions As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemRefresh As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem54 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemConnection As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem56 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemLog As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemReport As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem59 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemInfos As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemOpenedWindows As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem62 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemFindWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemEmergency As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem66 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemToTray As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemExit As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemUpdate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem39 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemDonation As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemFeedBack As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemSF As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemWebsite As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemDownloads As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem49 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemHelp As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemAbout As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopySearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyProcess As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyTask As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyNetwork As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemRunAsAdmin As System.Windows.Forms.MenuItem
    Friend WithEvents headerString As System.Windows.Forms.ColumnHeader
    Friend WithEvents StatusBar As System.Windows.Forms.StatusBar
    Friend WithEvents sbPanelConnection As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelProcesses As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelServices As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelCpu As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbPanelMemory As System.Windows.Forms.StatusBarPanel
    Friend WithEvents timerStatus As System.Windows.Forms.Timer
    Friend WithEvents MenuItemShowPendingTasks As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcesses As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportProcesses As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMonitor As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportMonitor As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServices As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportServices As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNewSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem61 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemReportSearch As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFiles As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem68 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileRelease As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileTrash As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem72 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileRename As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileShellOpen As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileMove As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileCopy As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem77 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileEncrypt As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileDecrypt As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem80 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemFileStrings As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem29 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem31 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNotifNP As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNotifTP As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNotifNS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNotifDS As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem46 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNotifAll As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNotifNone As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcJob As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemProcAddToJob As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem33 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemJobMng As System.Windows.Forms.MenuItem
    Friend WithEvents pageJobs As System.Windows.Forms.TabPage
    Friend WithEvents MenuItemJobs As System.Windows.Forms.MenuItem
    Friend WithEvents panelMain12 As System.Windows.Forms.Panel
    Friend WithEvents lvJob As mainJobList
    Friend WithEvents ColumnHeader50 As System.Windows.Forms.ColumnHeader
    Friend WithEvents timerJobs As System.Windows.Forms.Timer
    Private WithEvents mnuJob As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemJobTerminate As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem53 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCopyJob As System.Windows.Forms.MenuItem
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuItemProcKillByMethod As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemCreateService As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemServDelete As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTaskSelectWindow As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkTools As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemNetworkPing As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemNetworkInfos As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemMainNetworkInfo As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemScripting As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemSaveSSFile As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemExploreSSFile As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemSystemCheckSignatures As System.Windows.Forms.MenuItem

End Class
