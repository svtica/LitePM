<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreferences
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
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Suspended thread")
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Relocated module")
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Process being debugged")
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Critical process")
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Elevated process")
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Process in job")
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Service process")
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Owned process")
        Dim ListViewItem9 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("System process")
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.chkSaveErrorLog = New System.Windows.Forms.CheckBox
        Me.chkWintrust = New System.Windows.Forms.CheckBox
        Me.chkAutoOnline = New System.Windows.Forms.CheckBox
        Me.cmdResetAll = New System.Windows.Forms.Button
        Me.chkWarn = New System.Windows.Forms.CheckBox
        Me.txtSearchEngine = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmdChangeTaskmgr = New System.Windows.Forms.Button
        Me.chkTopMost = New System.Windows.Forms.CheckBox
        Me.chkStartTray = New System.Windows.Forms.CheckBox
        Me.chkReplaceTaskmgr = New System.Windows.Forms.CheckBox
        Me.chkStart = New System.Windows.Forms.CheckBox
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.cmdMoveDownProcess = New System.Windows.Forms.Button
        Me.cmdMoveUpProcess = New System.Windows.Forms.Button
        Me.lvHighlightingOther = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.lvHighlightingProcess = New System.Windows.Forms.ListView
        Me.Header = New System.Windows.Forms.ColumnHeader
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.chkShowIconsList = New System.Windows.Forms.CheckBox
        Me.chkRemember = New System.Windows.Forms.CheckBox
        Me.chkClassicMsgbox = New System.Windows.Forms.CheckBox
        Me.cbShownTab = New System.Windows.Forms.ComboBox
        Me.chkFixedTab = New System.Windows.Forms.CheckBox
        Me.chkStatusBar = New System.Windows.Forms.CheckBox
        Me.chkUserGroup = New System.Windows.Forms.CheckBox
        Me.chkHideClosed = New System.Windows.Forms.CheckBox
        Me.chkHideMinimized = New System.Windows.Forms.CheckBox
        Me.chkRibbon = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.pctDeletedItems = New System.Windows.Forms.PictureBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.pctNewitems = New System.Windows.Forms.PictureBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.chkTrayIcon = New System.Windows.Forms.CheckBox
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.chkUnlimitedBuf = New System.Windows.Forms.CheckBox
        Me.bufferSize = New System.Windows.Forms.NumericUpDown
        Me.Label11 = New System.Windows.Forms.Label
        Me.cbPriority = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.valCoefRemote = New System.Windows.Forms.NumericUpDown
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtJobInterval = New System.Windows.Forms.NumericUpDown
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtSysInfoInterval = New System.Windows.Forms.NumericUpDown
        Me.txtTrayInterval = New System.Windows.Forms.NumericUpDown
        Me.txtNetworkInterval = New System.Windows.Forms.NumericUpDown
        Me.txtTaskInterval = New System.Windows.Forms.NumericUpDown
        Me.txtServiceIntervall = New System.Windows.Forms.NumericUpDown
        Me.txtProcessIntervall = New System.Windows.Forms.NumericUpDown
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.cmdUpdateCheckNow = New System.Windows.Forms.Button
        Me.txtUpdateServer = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.chkUpdateAuto = New System.Windows.Forms.CheckBox
        Me.chkUpdateAlpha = New System.Windows.Forms.CheckBox
        Me.chkUpdateBeta = New System.Windows.Forms.CheckBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdQuit = New System.Windows.Forms.Button
        Me.cmdDefaut = New System.Windows.Forms.Button
        Me.colDial = New System.Windows.Forms.ColorDialog
        Me.cmdClearLog = New System.Windows.Forms.Button
        Me.cmdOpenLog = New System.Windows.Forms.Button
        Me.TabControl.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.pctDeletedItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pctNewitems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.bufferSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.valCoefRemote, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtJobInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSysInfoInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTrayInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNetworkInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTaskInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServiceIntervall, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtProcessIntervall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Controls.Add(Me.TabPage1)
        Me.TabControl.Controls.Add(Me.TabPage5)
        Me.TabControl.Controls.Add(Me.TabPage3)
        Me.TabControl.Controls.Add(Me.TabPage4)
        Me.TabControl.Controls.Add(Me.TabPage2)
        Me.TabControl.Location = New System.Drawing.Point(9, 9)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(469, 306)
        Me.TabControl.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.cmdOpenLog)
        Me.TabPage1.Controls.Add(Me.cmdClearLog)
        Me.TabPage1.Controls.Add(Me.chkSaveErrorLog)
        Me.TabPage1.Controls.Add(Me.chkWintrust)
        Me.TabPage1.Controls.Add(Me.chkAutoOnline)
        Me.TabPage1.Controls.Add(Me.cmdResetAll)
        Me.TabPage1.Controls.Add(Me.chkWarn)
        Me.TabPage1.Controls.Add(Me.txtSearchEngine)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.ImageKey = "(aucun)"
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(461, 280)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkSaveErrorLog
        '
        Me.chkSaveErrorLog.AutoSize = True
        Me.chkSaveErrorLog.Location = New System.Drawing.Point(14, 241)
        Me.chkSaveErrorLog.Name = "chkSaveErrorLog"
        Me.chkSaveErrorLog.Size = New System.Drawing.Size(97, 17)
        Me.chkSaveErrorLog.TabIndex = 11
        Me.chkSaveErrorLog.Text = "Save error log"
        Me.chkSaveErrorLog.UseVisualStyleBackColor = True
        '
        'chkWintrust
        '
        Me.chkWintrust.AutoSize = True
        Me.chkWintrust.Location = New System.Drawing.Point(14, 218)
        Me.chkWintrust.Name = "chkWintrust"
        Me.chkWintrust.Size = New System.Drawing.Size(179, 17)
        Me.chkWintrust.TabIndex = 10
        Me.chkWintrust.Text = "Verify signatures of processes"
        Me.chkWintrust.UseVisualStyleBackColor = True
        '
        'chkAutoOnline
        '
        Me.chkAutoOnline.AutoSize = True
        Me.chkAutoOnline.Location = New System.Drawing.Point(14, 195)
        Me.chkAutoOnline.Name = "chkAutoOnline"
        Me.chkAutoOnline.Size = New System.Drawing.Size(180, 17)
        Me.chkAutoOnline.TabIndex = 9
        Me.chkAutoOnline.Text = "Get online infos automatically"
        Me.chkAutoOnline.UseVisualStyleBackColor = True
        '
        'cmdResetAll
        '
        Me.cmdResetAll.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetAll.Location = New System.Drawing.Point(325, 247)
        Me.cmdResetAll.Name = "cmdResetAll"
        Me.cmdResetAll.Size = New System.Drawing.Size(133, 26)
        Me.cmdResetAll.TabIndex = 8
        Me.cmdResetAll.Text = "Reset all settings"
        Me.cmdResetAll.UseVisualStyleBackColor = True
        '
        'chkWarn
        '
        Me.chkWarn.AutoSize = True
        Me.chkWarn.Location = New System.Drawing.Point(14, 172)
        Me.chkWarn.Name = "chkWarn"
        Me.chkWarn.Size = New System.Drawing.Size(187, 17)
        Me.chkWarn.TabIndex = 5
        Me.chkWarn.Text = "Warn about dangerous actions"
        Me.chkWarn.UseVisualStyleBackColor = True
        '
        'txtSearchEngine
        '
        Me.txtSearchEngine.Location = New System.Drawing.Point(97, 144)
        Me.txtSearchEngine.Name = "txtSearchEngine"
        Me.txtSearchEngine.Size = New System.Drawing.Size(346, 22)
        Me.txtSearchEngine.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 147)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Search engine"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmdChangeTaskmgr)
        Me.GroupBox1.Controls.Add(Me.chkTopMost)
        Me.GroupBox1.Controls.Add(Me.chkStartTray)
        Me.GroupBox1.Controls.Add(Me.chkReplaceTaskmgr)
        Me.GroupBox1.Controls.Add(Me.chkStart)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(429, 120)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Startup"
        '
        'cmdChangeTaskmgr
        '
        Me.cmdChangeTaskmgr.Location = New System.Drawing.Point(125, 39)
        Me.cmdChangeTaskmgr.Name = "cmdChangeTaskmgr"
        Me.cmdChangeTaskmgr.Size = New System.Drawing.Size(91, 26)
        Me.cmdChangeTaskmgr.TabIndex = 9
        Me.cmdChangeTaskmgr.Text = "Change..."
        Me.cmdChangeTaskmgr.UseVisualStyleBackColor = True
        Me.cmdChangeTaskmgr.Visible = False
        '
        'chkTopMost
        '
        Me.chkTopMost.AutoSize = True
        Me.chkTopMost.Location = New System.Drawing.Point(9, 91)
        Me.chkTopMost.Name = "chkTopMost"
        Me.chkTopMost.Size = New System.Drawing.Size(127, 17)
        Me.chkTopMost.TabIndex = 3
        Me.chkTopMost.Text = "Start LitePM topmost"
        Me.chkTopMost.UseVisualStyleBackColor = True
        '
        'chkStartTray
        '
        Me.chkStartTray.AutoSize = True
        Me.chkStartTray.Location = New System.Drawing.Point(9, 68)
        Me.chkStartTray.Name = "chkStartTray"
        Me.chkStartTray.Size = New System.Drawing.Size(121, 17)
        Me.chkStartTray.TabIndex = 2
        Me.chkStartTray.Text = "Start LitePM hidden"
        Me.chkStartTray.UseVisualStyleBackColor = True
        '
        'chkReplaceTaskmgr
        '
        Me.chkReplaceTaskmgr.AutoSize = True
        Me.chkReplaceTaskmgr.Location = New System.Drawing.Point(9, 45)
        Me.chkReplaceTaskmgr.Name = "chkReplaceTaskmgr"
        Me.chkReplaceTaskmgr.Size = New System.Drawing.Size(110, 17)
        Me.chkReplaceTaskmgr.TabIndex = 1
        Me.chkReplaceTaskmgr.Text = "Replace taskmgr"
        Me.chkReplaceTaskmgr.UseVisualStyleBackColor = True
        '
        'chkStart
        '
        Me.chkStart.AutoSize = True
        Me.chkStart.Location = New System.Drawing.Point(9, 22)
        Me.chkStart.Name = "chkStart"
        Me.chkStart.Size = New System.Drawing.Size(190, 17)
        Me.chkStart.TabIndex = 0
        Me.chkStart.Text = "Start LitePM on Windows startup"
        Me.chkStart.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.cmdMoveDownProcess)
        Me.TabPage5.Controls.Add(Me.cmdMoveUpProcess)
        Me.TabPage5.Controls.Add(Me.lvHighlightingOther)
        Me.TabPage5.Controls.Add(Me.lvHighlightingProcess)
        Me.TabPage5.ImageKey = "(aucun)"
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(461, 280)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Highlighting"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'cmdMoveDownProcess
        '
        Me.cmdMoveDownProcess.Enabled = False
        Me.cmdMoveDownProcess.Image = Global.My.Resources.Resources.down16
        Me.cmdMoveDownProcess.Location = New System.Drawing.Point(41, 237)
        Me.cmdMoveDownProcess.Name = "cmdMoveDownProcess"
        Me.cmdMoveDownProcess.Size = New System.Drawing.Size(28, 28)
        Me.cmdMoveDownProcess.TabIndex = 11
        Me.cmdMoveDownProcess.UseVisualStyleBackColor = True
        '
        'cmdMoveUpProcess
        '
        Me.cmdMoveUpProcess.Enabled = False
        Me.cmdMoveUpProcess.Image = Global.My.Resources.Resources.up16
        Me.cmdMoveUpProcess.Location = New System.Drawing.Point(7, 237)
        Me.cmdMoveUpProcess.Name = "cmdMoveUpProcess"
        Me.cmdMoveUpProcess.Size = New System.Drawing.Size(28, 28)
        Me.cmdMoveUpProcess.TabIndex = 10
        Me.cmdMoveUpProcess.UseVisualStyleBackColor = True
        '
        'lvHighlightingOther
        '
        Me.lvHighlightingOther.CheckBoxes = True
        Me.lvHighlightingOther.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvHighlightingOther.FullRowSelect = True
        Me.lvHighlightingOther.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem1.StateImageIndex = 0
        ListViewItem2.StateImageIndex = 0
        Me.lvHighlightingOther.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2})
        Me.lvHighlightingOther.Location = New System.Drawing.Point(229, 3)
        Me.lvHighlightingOther.MultiSelect = False
        Me.lvHighlightingOther.Name = "lvHighlightingOther"
        Me.lvHighlightingOther.Size = New System.Drawing.Size(220, 228)
        Me.lvHighlightingOther.TabIndex = 1
        Me.lvHighlightingOther.UseCompatibleStateImageBehavior = False
        Me.lvHighlightingOther.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 200
        '
        'lvHighlightingProcess
        '
        Me.lvHighlightingProcess.CheckBoxes = True
        Me.lvHighlightingProcess.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Header})
        Me.lvHighlightingProcess.FullRowSelect = True
        Me.lvHighlightingProcess.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
        ListViewItem3.StateImageIndex = 0
        ListViewItem4.StateImageIndex = 0
        ListViewItem5.StateImageIndex = 0
        ListViewItem6.StateImageIndex = 0
        ListViewItem7.StateImageIndex = 0
        ListViewItem8.StateImageIndex = 0
        ListViewItem9.StateImageIndex = 0
        Me.lvHighlightingProcess.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8, ListViewItem9})
        Me.lvHighlightingProcess.Location = New System.Drawing.Point(3, 3)
        Me.lvHighlightingProcess.MultiSelect = False
        Me.lvHighlightingProcess.Name = "lvHighlightingProcess"
        Me.lvHighlightingProcess.Size = New System.Drawing.Size(220, 228)
        Me.lvHighlightingProcess.TabIndex = 0
        Me.lvHighlightingProcess.UseCompatibleStateImageBehavior = False
        Me.lvHighlightingProcess.View = System.Windows.Forms.View.Details
        '
        'Header
        '
        Me.Header.Width = 200
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.chkShowIconsList)
        Me.TabPage3.Controls.Add(Me.chkRemember)
        Me.TabPage3.Controls.Add(Me.chkClassicMsgbox)
        Me.TabPage3.Controls.Add(Me.cbShownTab)
        Me.TabPage3.Controls.Add(Me.chkFixedTab)
        Me.TabPage3.Controls.Add(Me.chkStatusBar)
        Me.TabPage3.Controls.Add(Me.chkUserGroup)
        Me.TabPage3.Controls.Add(Me.chkHideClosed)
        Me.TabPage3.Controls.Add(Me.chkHideMinimized)
        Me.TabPage3.Controls.Add(Me.chkRibbon)
        Me.TabPage3.Controls.Add(Me.GroupBox3)
        Me.TabPage3.Controls.Add(Me.chkTrayIcon)
        Me.TabPage3.ImageKey = "(aucun)"
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(461, 280)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Display"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'chkShowIconsList
        '
        Me.chkShowIconsList.AutoSize = True
        Me.chkShowIconsList.Location = New System.Drawing.Point(181, 171)
        Me.chkShowIconsList.Name = "chkShowIconsList"
        Me.chkShowIconsList.Size = New System.Drawing.Size(166, 17)
        Me.chkShowIconsList.TabIndex = 16
        Me.chkShowIconsList.Text = "Show icons in detailed lists"
        Me.chkShowIconsList.UseVisualStyleBackColor = True
        '
        'chkRemember
        '
        Me.chkRemember.AutoSize = True
        Me.chkRemember.Location = New System.Drawing.Point(15, 240)
        Me.chkRemember.Name = "chkRemember"
        Me.chkRemember.Size = New System.Drawing.Size(181, 17)
        Me.chkRemember.TabIndex = 15
        Me.chkRemember.Text = "Remember positions and sizes"
        Me.chkRemember.UseVisualStyleBackColor = True
        '
        'chkClassicMsgbox
        '
        Me.chkClassicMsgbox.AutoSize = True
        Me.chkClassicMsgbox.Location = New System.Drawing.Point(181, 148)
        Me.chkClassicMsgbox.Name = "chkClassicMsgbox"
        Me.chkClassicMsgbox.Size = New System.Drawing.Size(167, 17)
        Me.chkClassicMsgbox.TabIndex = 14
        Me.chkClassicMsgbox.Text = "Show classic messageboxes"
        Me.chkClassicMsgbox.UseVisualStyleBackColor = True
        '
        'cbShownTab
        '
        Me.cbShownTab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShownTab.FormattingEnabled = True
        Me.cbShownTab.Location = New System.Drawing.Point(293, 123)
        Me.cbShownTab.Name = "cbShownTab"
        Me.cbShownTab.Size = New System.Drawing.Size(150, 21)
        Me.cbShownTab.TabIndex = 13
        '
        'chkFixedTab
        '
        Me.chkFixedTab.AutoSize = True
        Me.chkFixedTab.Location = New System.Drawing.Point(182, 125)
        Me.chkFixedTab.Name = "chkFixedTab"
        Me.chkFixedTab.Size = New System.Drawing.Size(105, 17)
        Me.chkFixedTab.TabIndex = 12
        Me.chkFixedTab.Text = "Tab shown first"
        Me.chkFixedTab.UseVisualStyleBackColor = True
        '
        'chkStatusBar
        '
        Me.chkStatusBar.AutoSize = True
        Me.chkStatusBar.Location = New System.Drawing.Point(15, 217)
        Me.chkStatusBar.Name = "chkStatusBar"
        Me.chkStatusBar.Size = New System.Drawing.Size(106, 17)
        Me.chkStatusBar.TabIndex = 11
        Me.chkStatusBar.Text = "Show statusbar"
        Me.chkStatusBar.UseVisualStyleBackColor = True
        '
        'chkUserGroup
        '
        Me.chkUserGroup.AutoSize = True
        Me.chkUserGroup.Location = New System.Drawing.Point(15, 194)
        Me.chkUserGroup.Name = "chkUserGroup"
        Me.chkUserGroup.Size = New System.Drawing.Size(158, 17)
        Me.chkUserGroup.TabIndex = 10
        Me.chkUserGroup.Text = "Show user group/domain"
        Me.chkUserGroup.UseVisualStyleBackColor = True
        '
        'chkHideClosed
        '
        Me.chkHideClosed.AutoSize = True
        Me.chkHideClosed.Location = New System.Drawing.Point(15, 171)
        Me.chkHideClosed.Name = "chkHideClosed"
        Me.chkHideClosed.Size = New System.Drawing.Size(118, 17)
        Me.chkHideClosed.TabIndex = 9
        Me.chkHideClosed.Text = "Hide when closed"
        Me.chkHideClosed.UseVisualStyleBackColor = True
        '
        'chkHideMinimized
        '
        Me.chkHideMinimized.AutoSize = True
        Me.chkHideMinimized.Location = New System.Drawing.Point(15, 148)
        Me.chkHideMinimized.Name = "chkHideMinimized"
        Me.chkHideMinimized.Size = New System.Drawing.Size(137, 17)
        Me.chkHideMinimized.TabIndex = 3
        Me.chkHideMinimized.Text = "Hide when minimized"
        Me.chkHideMinimized.UseVisualStyleBackColor = True
        '
        'chkRibbon
        '
        Me.chkRibbon.AutoSize = True
        Me.chkRibbon.Location = New System.Drawing.Point(15, 125)
        Me.chkRibbon.Name = "chkRibbon"
        Me.chkRibbon.Size = New System.Drawing.Size(127, 17)
        Me.chkRibbon.TabIndex = 2
        Me.chkRibbon.Text = "Ribbon style menus"
        Me.chkRibbon.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.pctDeletedItems)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.pctNewitems)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 37)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(428, 77)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Colors"
        '
        'pctDeletedItems
        '
        Me.pctDeletedItems.Location = New System.Drawing.Point(86, 46)
        Me.pctDeletedItems.Name = "pctDeletedItems"
        Me.pctDeletedItems.Size = New System.Drawing.Size(16, 16)
        Me.pctDeletedItems.TabIndex = 3
        Me.pctDeletedItems.TabStop = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Deleted items"
        '
        'pctNewitems
        '
        Me.pctNewitems.Location = New System.Drawing.Point(86, 21)
        Me.pctNewitems.Name = "pctNewitems"
        Me.pctNewitems.Size = New System.Drawing.Size(16, 16)
        Me.pctNewitems.TabIndex = 1
        Me.pctNewitems.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 23)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "New items"
        '
        'chkTrayIcon
        '
        Me.chkTrayIcon.AutoSize = True
        Me.chkTrayIcon.Location = New System.Drawing.Point(15, 14)
        Me.chkTrayIcon.Name = "chkTrayIcon"
        Me.chkTrayIcon.Size = New System.Drawing.Size(102, 17)
        Me.chkTrayIcon.TabIndex = 0
        Me.chkTrayIcon.Text = "Show tray icon"
        Me.chkTrayIcon.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.chkUnlimitedBuf)
        Me.TabPage4.Controls.Add(Me.bufferSize)
        Me.TabPage4.Controls.Add(Me.Label11)
        Me.TabPage4.Controls.Add(Me.cbPriority)
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.Controls.Add(Me.GroupBox2)
        Me.TabPage4.ImageKey = "(aucun)"
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(461, 280)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Performances"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'chkUnlimitedBuf
        '
        Me.chkUnlimitedBuf.AutoSize = True
        Me.chkUnlimitedBuf.Location = New System.Drawing.Point(201, 226)
        Me.chkUnlimitedBuf.Name = "chkUnlimitedBuf"
        Me.chkUnlimitedBuf.Size = New System.Drawing.Size(76, 17)
        Me.chkUnlimitedBuf.TabIndex = 9
        Me.chkUnlimitedBuf.Text = "Unlimited"
        Me.chkUnlimitedBuf.UseVisualStyleBackColor = True
        '
        'bufferSize
        '
        Me.bufferSize.Location = New System.Drawing.Point(120, 224)
        Me.bufferSize.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.bufferSize.Name = "bufferSize"
        Me.bufferSize.Size = New System.Drawing.Size(75, 22)
        Me.bufferSize.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(14, 226)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(100, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "History buffer (KB)"
        '
        'cbPriority
        '
        Me.cbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPriority.FormattingEnabled = True
        Me.cbPriority.Items.AddRange(New Object() {"Idle", "Below Normal", "Normal", "Above Normal", "High", "Real Time"})
        Me.cbPriority.Location = New System.Drawing.Point(74, 191)
        Me.cbPriority.Name = "cbPriority"
        Me.cbPriority.Size = New System.Drawing.Size(148, 21)
        Me.cbPriority.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 194)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Priority"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.valCoefRemote)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtJobInterval)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtSysInfoInterval)
        Me.GroupBox2.Controls.Add(Me.txtTrayInterval)
        Me.GroupBox2.Controls.Add(Me.txtNetworkInterval)
        Me.GroupBox2.Controls.Add(Me.txtTaskInterval)
        Me.GroupBox2.Controls.Add(Me.txtServiceIntervall)
        Me.GroupBox2.Controls.Add(Me.txtProcessIntervall)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(428, 163)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Update intervals"
        '
        'valCoefRemote
        '
        Me.valCoefRemote.Location = New System.Drawing.Point(367, 135)
        Me.valCoefRemote.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.valCoefRemote.Minimum = New Decimal(New Integer() {100, 0, 0, 0})
        Me.valCoefRemote.Name = "valCoefRemote"
        Me.valCoefRemote.Size = New System.Drawing.Size(55, 22)
        Me.valCoefRemote.TabIndex = 22
        Me.valCoefRemote.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(205, 138)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(156, 13)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Coeff. for remote monitoring"
        '
        'txtJobInterval
        '
        Me.txtJobInterval.Location = New System.Drawing.Point(282, 21)
        Me.txtJobInterval.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtJobInterval.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtJobInterval.Name = "txtJobInterval"
        Me.txtJobInterval.Size = New System.Drawing.Size(80, 22)
        Me.txtJobInterval.TabIndex = 20
        Me.txtJobInterval.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(205, 23)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Jobs interval"
        '
        'txtSysInfoInterval
        '
        Me.txtSysInfoInterval.Location = New System.Drawing.Point(109, 136)
        Me.txtSysInfoInterval.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtSysInfoInterval.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtSysInfoInterval.Name = "txtSysInfoInterval"
        Me.txtSysInfoInterval.Size = New System.Drawing.Size(80, 22)
        Me.txtSysInfoInterval.TabIndex = 18
        Me.txtSysInfoInterval.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'txtTrayInterval
        '
        Me.txtTrayInterval.Location = New System.Drawing.Point(109, 113)
        Me.txtTrayInterval.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtTrayInterval.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtTrayInterval.Name = "txtTrayInterval"
        Me.txtTrayInterval.Size = New System.Drawing.Size(80, 22)
        Me.txtTrayInterval.TabIndex = 17
        Me.txtTrayInterval.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'txtNetworkInterval
        '
        Me.txtNetworkInterval.Location = New System.Drawing.Point(109, 90)
        Me.txtNetworkInterval.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtNetworkInterval.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtNetworkInterval.Name = "txtNetworkInterval"
        Me.txtNetworkInterval.Size = New System.Drawing.Size(80, 22)
        Me.txtNetworkInterval.TabIndex = 16
        Me.txtNetworkInterval.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'txtTaskInterval
        '
        Me.txtTaskInterval.Location = New System.Drawing.Point(109, 67)
        Me.txtTaskInterval.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtTaskInterval.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtTaskInterval.Name = "txtTaskInterval"
        Me.txtTaskInterval.Size = New System.Drawing.Size(80, 22)
        Me.txtTaskInterval.TabIndex = 15
        Me.txtTaskInterval.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'txtServiceIntervall
        '
        Me.txtServiceIntervall.Location = New System.Drawing.Point(109, 44)
        Me.txtServiceIntervall.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtServiceIntervall.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtServiceIntervall.Name = "txtServiceIntervall"
        Me.txtServiceIntervall.Size = New System.Drawing.Size(80, 22)
        Me.txtServiceIntervall.TabIndex = 14
        Me.txtServiceIntervall.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'txtProcessIntervall
        '
        Me.txtProcessIntervall.Location = New System.Drawing.Point(109, 21)
        Me.txtProcessIntervall.Maximum = New Decimal(New Integer() {50000, 0, 0, 0})
        Me.txtProcessIntervall.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtProcessIntervall.Name = "txtProcessIntervall"
        Me.txtProcessIntervall.Size = New System.Drawing.Size(80, 22)
        Me.txtProcessIntervall.TabIndex = 13
        Me.txtProcessIntervall.Value = New Decimal(New Integer() {50, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Sys. info interval"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 115)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Tray interval"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Network interval"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Tasks interval"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Services interval"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Processes interval"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.cmdUpdateCheckNow)
        Me.TabPage2.Controls.Add(Me.txtUpdateServer)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.chkUpdateAuto)
        Me.TabPage2.Controls.Add(Me.chkUpdateAlpha)
        Me.TabPage2.Controls.Add(Me.chkUpdateBeta)
        Me.TabPage2.ImageKey = "(aucun)"
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(461, 280)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Update"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'cmdUpdateCheckNow
        '
        Me.cmdUpdateCheckNow.Location = New System.Drawing.Point(11, 131)
        Me.cmdUpdateCheckNow.Name = "cmdUpdateCheckNow"
        Me.cmdUpdateCheckNow.Size = New System.Drawing.Size(108, 23)
        Me.cmdUpdateCheckNow.TabIndex = 18
        Me.cmdUpdateCheckNow.Text = "Check now"
        Me.cmdUpdateCheckNow.UseVisualStyleBackColor = True
        '
        'txtUpdateServer
        '
        Me.txtUpdateServer.Location = New System.Drawing.Point(56, 90)
        Me.txtUpdateServer.Name = "txtUpdateServer"
        Me.txtUpdateServer.Size = New System.Drawing.Size(345, 22)
        Me.txtUpdateServer.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(11, 93)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(38, 13)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Server"
        '
        'chkUpdateAuto
        '
        Me.chkUpdateAuto.AutoSize = True
        Me.chkUpdateAuto.Location = New System.Drawing.Point(11, 67)
        Me.chkUpdateAuto.Name = "chkUpdateAuto"
        Me.chkUpdateAuto.Size = New System.Drawing.Size(219, 17)
        Me.chkUpdateAuto.TabIndex = 15
        Me.chkUpdateAuto.Text = "Check if LitePM is up to date at startup"
        Me.chkUpdateAuto.UseVisualStyleBackColor = True
        '
        'chkUpdateAlpha
        '
        Me.chkUpdateAlpha.AutoSize = True
        Me.chkUpdateAlpha.Location = New System.Drawing.Point(11, 44)
        Me.chkUpdateAlpha.Name = "chkUpdateAlpha"
        Me.chkUpdateAlpha.Size = New System.Drawing.Size(151, 17)
        Me.chkUpdateAlpha.TabIndex = 14
        Me.chkUpdateAlpha.Text = "Check for alpha releases"
        Me.chkUpdateAlpha.UseVisualStyleBackColor = True
        '
        'chkUpdateBeta
        '
        Me.chkUpdateBeta.AutoSize = True
        Me.chkUpdateBeta.Location = New System.Drawing.Point(11, 21)
        Me.chkUpdateBeta.Name = "chkUpdateBeta"
        Me.chkUpdateBeta.Size = New System.Drawing.Size(145, 17)
        Me.chkUpdateBeta.TabIndex = 13
        Me.chkUpdateBeta.Text = "Check for beta releases"
        Me.chkUpdateBeta.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.Location = New System.Drawing.Point(12, 323)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(100, 26)
        Me.cmdSave.TabIndex = 7
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'cmdQuit
        '
        Me.cmdQuit.Location = New System.Drawing.Point(374, 323)
        Me.cmdQuit.Name = "cmdQuit"
        Me.cmdQuit.Size = New System.Drawing.Size(100, 26)
        Me.cmdQuit.TabIndex = 9
        Me.cmdQuit.Text = "Close"
        Me.cmdQuit.UseVisualStyleBackColor = True
        '
        'cmdDefaut
        '
        Me.cmdDefaut.Location = New System.Drawing.Point(195, 323)
        Me.cmdDefaut.Name = "cmdDefaut"
        Me.cmdDefaut.Size = New System.Drawing.Size(100, 26)
        Me.cmdDefaut.TabIndex = 8
        Me.cmdDefaut.Text = "Default"
        Me.cmdDefaut.UseVisualStyleBackColor = True
        '
        'colDial
        '
        Me.colDial.AnyColor = True
        Me.colDial.FullOpen = True
        '
        'cmdClearLog
        '
        Me.cmdClearLog.Location = New System.Drawing.Point(117, 237)
        Me.cmdClearLog.Name = "cmdClearLog"
        Me.cmdClearLog.Size = New System.Drawing.Size(58, 23)
        Me.cmdClearLog.TabIndex = 12
        Me.cmdClearLog.Text = "Clear"
        Me.cmdClearLog.UseVisualStyleBackColor = True
        '
        'cmdOpenLog
        '
        Me.cmdOpenLog.Location = New System.Drawing.Point(182, 237)
        Me.cmdOpenLog.Name = "cmdOpenLog"
        Me.cmdOpenLog.Size = New System.Drawing.Size(58, 23)
        Me.cmdOpenLog.TabIndex = 13
        Me.cmdOpenLog.Text = "Open"
        Me.cmdOpenLog.UseVisualStyleBackColor = True
        '
        'frmPreferences
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 355)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdDefaut)
        Me.Controls.Add(Me.cmdQuit)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.TabControl)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPreferences"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preferences"
        Me.TabControl.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.pctDeletedItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pctNewitems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        CType(Me.bufferSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.valCoefRemote, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtJobInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSysInfoInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTrayInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNetworkInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTaskInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServiceIntervall, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtProcessIntervall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdQuit As System.Windows.Forms.Button
    Friend WithEvents cmdDefaut As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkReplaceTaskmgr As System.Windows.Forms.CheckBox
    Friend WithEvents chkStart As System.Windows.Forms.CheckBox
    Friend WithEvents chkStartTray As System.Windows.Forms.CheckBox
    Friend WithEvents chkTopMost As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbPriority As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearchEngine As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkWarn As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkTrayIcon As System.Windows.Forms.CheckBox
    Friend WithEvents pctDeletedItems As System.Windows.Forms.PictureBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents pctNewitems As System.Windows.Forms.PictureBox
    Friend WithEvents chkRibbon As System.Windows.Forms.CheckBox
    Friend WithEvents colDial As System.Windows.Forms.ColorDialog
    Friend WithEvents chkHideMinimized As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkHideClosed As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkUnlimitedBuf As System.Windows.Forms.CheckBox
    Friend WithEvents bufferSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmdResetAll As System.Windows.Forms.Button
    Friend WithEvents chkAutoOnline As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents lvHighlightingProcess As System.Windows.Forms.ListView
    Friend WithEvents Header As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvHighlightingOther As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdMoveDownProcess As System.Windows.Forms.Button
    Friend WithEvents cmdMoveUpProcess As System.Windows.Forms.Button
    Friend WithEvents chkUserGroup As System.Windows.Forms.CheckBox
    Friend WithEvents chkStatusBar As System.Windows.Forms.CheckBox
    Friend WithEvents cmdChangeTaskmgr As System.Windows.Forms.Button
    Friend WithEvents txtSysInfoInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTrayInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtNetworkInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTaskInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtServiceIntervall As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtProcessIntervall As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtJobInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkWintrust As System.Windows.Forms.CheckBox
    Friend WithEvents cbShownTab As System.Windows.Forms.ComboBox
    Friend WithEvents chkFixedTab As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateAuto As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateAlpha As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateBeta As System.Windows.Forms.CheckBox
    Friend WithEvents cmdUpdateCheckNow As System.Windows.Forms.Button
    Friend WithEvents txtUpdateServer As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkClassicMsgbox As System.Windows.Forms.CheckBox
    Friend WithEvents valCoefRemote As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkRemember As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowIconsList As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveErrorLog As System.Windows.Forms.CheckBox
    Friend WithEvents cmdOpenLog As System.Windows.Forms.Button
    Friend WithEvents cmdClearLog As System.Windows.Forms.Button
End Class
