﻿
' Lite Process Monitor


' This file uses third-party pieces of code under GNU 
' GPL 3.0 license. See below for details







'




'


'
'
' This file uses some work under GNU GPL 3.0 license
' The following definitions are from Process Hacker by Wj32,
' which is under GNU GPL 3.0 :
' - RtlUserProcessFlags
' - LdrpDataTableEntryFlags
' - ProcessInformationClass
' - MemoryState
' - ThreadInformationClass 
' - SePrivilegeAttributes
' - TokenInformationClass
' - SystemInformationClass
' - ExitWindowsFlags
' - WindowMessage
' - SmtoFlags
' - RemoteThreadCreationFlags
' - KwaitReason
' - SidNameUse
' - SecurityImpersonationLevel
' - GdiPenStyle
' - GdiBlendMode
' - GdiStockObject
' - ServiceFlags
' - PoolType
' - ObjectFlags

Option Strict On

Namespace Native.Api

    Public Class NativeEnums

#Region "Declarations used for jobs"

        ' http://msdn.microsoft.com/en-us/library/ms684155(VS.85).aspx
        <Flags()> _
        Public Enum EndOfJobTimeActionFlag As UInteger
            TerminateAtEndOfJob = 0
            PostAtEndOfJob = 1
        End Enum

        ' http://msdn.microsoft.com/en-us/library/ms684152(VS.85).aspx
        <Flags()> _
        Public Enum JobObjectBasicUiRestrictions As UInteger
            [Handles] = &H1
            ReadClipboard = &H2
            WriteClipboard = &H4
            SystemParameters = &H8
            DisplaySettings = &H10
            GlobalAtoms = &H20
            Desktop = &H40
            ExitWindows = &H80
        End Enum

        Public Enum JobObjectInformationClass As Integer
            JobObjectBasicAccountingInformation = 1
            JobObjectBasicLimitInformation = 2
            JobObjectBasicProcessIdList = 3
            JobObjectBasicUIRestrictions = 4
            JobObjectSecurityLimitInformation = 5
            JobObjectEndOfJobTimeInformation = 6
            JobObjectAssociateCompletionPortInformation = 7
            JobObjectBasicAndIoAccountingInformation = 8
            JobObjectExtendedLimitInformation = 9
            JobObjectJobSetInformation = 10
            JobObjectGroupInformation = 11      ' Not supported on XP/Vista
        End Enum

        ' http://msdn.microsoft.com/en-us/library/ms684147(VS.85).aspx
        <Flags()> _
        Public Enum JobObjectLimitFlags As UInteger
            WorkingSet = &H1
            ProcessTime = &H2
            JobTime = &H4
            ActiveProcess = &H8
            Affinity = &H10
            PriorityClass = &H20
            PreserveJobTime = &H40
            SchedulingClass = &H80
            ProcessMemory = &H100
            JobMemory = &H200
            DieOnUnhandledException = &H400
            BreakawayOk = &H800
            SilentBreakawayOk = &H1000
            KillOnJobClose = &H2000
        End Enum

#End Region

#Region "Declarations used for processes"

        Public Enum DepFlags As UInteger
            Disable = &H0
            Enable = &H1
            DisableAtlThunkEmulation = &H2
        End Enum

        Public Enum GuiResourceType As Integer
            GdiObjects = &H0
            UserObjects = &H1
        End Enum

        <Flags()> _
        Public Enum LdrpDataTableEntryFlags As UInteger
            StaticLink = &H2
            ImageDll = &H4
            Flag0x8 = &H8
            Flag0x10 = &H10
            LoadInProgress = &H1000
            UnloadInProgress = &H2000
            EntryProcessed = &H4000
            EntryInserted = &H8000
            CurrentLoad = &H10000
            FailedBuiltInLoad = &H20000
            DontCallForThreads = &H40000
            ProcessAttachCalled = &H80000
            DebugSymbolsLoaded = &H100000
            ImageNotAtBase = &H200000
            CorImage = &H400000
            CorOwnsUnmap = &H800000
            SystemMapped = &H1000000
            ImageVerifying = &H2000000
            DriverDependentDll = &H4000000
            EntryNative = &H8000000
            Redirected = &H10000000
            NonPagedDebugInfo = &H20000000
            MmLoaded = &H40000000
            CompatDatabaseProcessed = &H80000000
        End Enum

        Public Enum MiniDumpType As Integer
            MiniDumpNormal = &H0
            MiniDumpWithDataSegs = &H1
            MiniDumpWithFullMemory = &H2
            MiniDumpWithHandleData = &H4
            MiniDumpFilterMemory = &H8
            MiniDumpScanMemory = &H10
            MiniDumpWithUnloadedModules = &H20
            MiniDumpWithIndirectlyReferencedMemory = &H40
            MiniDumpFilterModulePaths = &H80
            MiniDumpWithProcessThreadData = &H100
            MiniDumpWithPrivateReadWriteMemory = &H200
            MiniDumpWithoutOptionalData = &H400
            MiniDumpWithFullMemoryInfo = &H800
            MiniDumpWithThreadInfo = &H1000
            MiniDumpWithCodeSegs = &H2000
        End Enum

        Public Enum ProcessInformationClass As Integer
            ProcessBasicInformation
            ' 0
            ProcessQuotaLimits
            ProcessIoCounters
            ProcessVmCounters
            ProcessTimes
            ProcessBasePriority
            ProcessRaisePriority
            ProcessDebugPort
            ProcessExceptionPort
            ProcessAccessToken
            ProcessLdtInformation
            ' 10
            ProcessLdtSize
            ProcessDefaultHardErrorMode
            ProcessIoPortHandlers
            ProcessPooledUsageAndLimits
            ProcessWorkingSetWatch
            ProcessUserModeIOPL
            ProcessEnableAlignmentFaultFixup
            ProcessPriorityClass
            ProcessWx86Information
            ProcessHandleCount
            ' 20
            ProcessAffinityMask
            ProcessPriorityBoost
            ProcessDeviceMap
            ProcessSessionInformation
            ProcessForegroundInformation
            ProcessWow64Information
            ProcessImageFileName
            ProcessLUIDDeviceMapsEnabled
            ProcessBreakOnTermination
            ProcessDebugObjectHandle
            ' 30
            ProcessDebugFlags
            ProcessHandleTracing
            ProcessIoPriority
            ProcessExecuteFlags
            ProcessResourceManagement
            ProcessCookie
            ProcessImageInformation
            ProcessCycleTime
            ProcessPagePriority
            ProcessInstrumentationCallback
            ' 40
            ProcessThreadStackAllocation
            ProcessWorkingSetWatchEx
            ProcessImageFileNameWin32
            ProcessImageFileMapping
            ProcessAffinityUpdateMode
            ProcessMemoryAllocationMode
            MaxProcessInfoClass
        End Enum

        <Flags()> _
        Public Enum RtlUserProcessFlags As UInteger
            ParamsNormalized = &H1
            ProfileUser = &H2
            ProfileKernel = &H4
            ProfileServer = &H8
            Reserve1Mb = &H20
            Reserve16Mb = &H40
            CaseSensitive = &H80
            DisableHeapDecommit = &H100
            DllRedirectionLocal = &H1000
            AppManifestPresent = &H2000
            ImageKeyMissing = &H4000
            OptInProcess = &H20000
        End Enum

        <Flags()> _
        Public Enum Toolhelp32SnapshotFlags As UInteger
            HeapList = &H1
            Process = &H2
            Thread = &H4
            [Module] = &H8
            Module32 = &H10
            Inherit = &H80000000
            All = HeapList Or [Module] Or Process Or Thread
        End Enum

#End Region

        ' OK
#Region "Declarations used for memory management"

        <Flags()> _
        Public Enum MemoryProtectionType As UInteger
            AccessDenied = 0
            Execute = &H10
            ExecuteRead = &H20
            ExecuteReadWrite = &H40
            ExecuteWriteCopy = &H80
            NoAccess = &H1
            [ReadOnly] = &H2
            ReadWrite = &H4
            WriteCopy = &H8
            Guard = &H100
            NoCache = &H200
            WriteCombine = &H400
        End Enum

        <Flags()> _
        Public Enum MemoryState As UInteger
            Free = &H10000
            Commit = &H1000
            Reserve = &H2000
            Decommit = &H4000
            Release = &H8000
            Reset = &H80000
            TopDown = &H100000
            Physical = &H400000
            LargePages = &H20000000
        End Enum

        Public Enum MemoryType As Integer
            Image = &H1000000
            [Private] = &H20000
            Mapped = &H40000
        End Enum

#End Region

        ' OK
#Region "Declarations used for threads"

        Public Enum KwaitReason As Integer
            Executive = 0
            FreePage = 1
            PageIn = 2
            PoolAllocation = 3
            DelayExecution = 4
            Suspended = 5
            UserRequest = 6
            WrExecutive = 7
            WrFreePage = 8
            WrPageIn = 9
            WrPoolAllocation = 10
            WrDelayExecution = 11
            WrSuspended = 12
            WrUserRequest = 13
            WrEventPair = 14
            WrQueue = 15
            WrLpcReceive = 16
            WrLpcReply = 17
            WrVirtualMemory = 18
            WrPageOut = 19
            WrRendezvous = 20
            Spare2 = 21
            Spare3 = 22
            Spare4 = 23
            Spare5 = 24
            WrCalloutStack = 25
            WrKernel = 26
            WrResource = 27
            WrPushLock = 28
            WrMutex = 29
            WrQuantumEnd = 30
            WrDispatchInt = 31
            WrPreempted = 32
            WrYieldExecution = 33
            WrFastMutex = 34
            WrGuardedMutex = 35
            WrRundown = 36
            MaximumWaitReason = 37
        End Enum

        <Flags()> _
        Public Enum RemoteThreadCreationFlags As UInteger
            DebugProcess = &H1
            DebugOnlyThisProcess = &H2
            CreateSuspended = &H4
            DetachedProcess = &H8
            CreateNewConsole = &H10
            NormalPriorityClass = &H20
            IdlePriorityClass = &H40
            HighPriorityClass = &H80
            RealtimePriorityClass = &H100
            CreateNewProcessGroup = &H200
            CreateUnicodeEnvironment = &H400
            CreateSeparateWowVdm = &H800
            CreateSharedWowVdm = &H1000
            CreateForceDos = &H2000
            BelowNormalPriorityClass = &H4000
            AboveNormalPriorityClass = &H8000
            StackSizeParamIsAReservation = &H10000
            InheritCallerPriority = &H20000
            CreateProtectedProcess = &H40000
            ExtendedStartupInfoPresent = &H80000
            ProcessModeBackgroundBegin = &H100000
            ProcessModeBackgroundEnd = &H200000
            CreateBreakawayFromJob = &H1000000
            CreatePreserveCodeAuthzLevel = &H2000000
            CreateDefaultErrorMode = &H4000000
            CreateNoWindow = &H8000000
            ProfileUser = &H10000000
            ProfileKernel = &H20000000
            ProfileServer = &H40000000
            CreateIgnoreSystemDefault = &H80000000
        End Enum

        Public Enum ThreadInformationClass
            ThreadBasicInformation
            ThreadTimes
            ThreadPriority
            ThreadBasePriority
            ThreadAffinityMask
            ThreadImpersonationToken
            ThreadDescriptorTableEntry
            ThreadEnableAlignmentFaultFixup
            ThreadEventPair
            ThreadQuerySetWin32StartAddress
            ThreadZeroTlsCell
            ThreadPerformanceCount
            ThreadAmILastThread
            ThreadIdealProcessor
            ThreadPriorityBoost
            ThreadSetTlsArrayAddress
            ThreadIsIoPending
            ThreadHideFromDebugger
            ThreadBreakOnTermination
            ThreadSwitchLegacyState
            ThreadIsTerminated
            ThreadLastSystemCall
            ThreadIoPriority
            ThreadCycleTime
            ThreadPagePriority
            ThreadActualBasePriority
            ThreadTebInformation
            ThreadCSwitchMon
            MaxThreadInfoClass
        End Enum

#End Region

#Region "Declarations used for tokens & privileges"

        Public Enum SecurityImpersonationLevel As Integer
            SecurityAnonymous
            SecurityIdentification
            SecurityImpersonation
            SecurityDelegation
        End Enum

        <Flags()> _
        Public Enum SePrivilegeAttributes As UInteger
            Disabled = &H0
            EnabledByDefault = &H1
            Enabled = &H2
            DisabledByDefault = &H3
            Removed = &H4
            UsedForAccess = &H80000000
        End Enum

        Public Enum SidNameUse As Integer
            User = 1
            Group
            Domain
            [Alias]
            WellKnownGroup
            DeletedAccount
            Invalid
            Unknown
            Computer
            Label
        End Enum

        Public Enum TokenInformationClass
            TokenUser = 1
            TokenGroups
            TokenPrivileges
            TokenOwner
            TokenPrimaryGroup
            TokenDefaultDacl
            TokenSource
            TokenType
            TokenImpersonationLevel
            TokenStatistics
            ' 10
            TokenRestrictedSids
            TokenSessionId
            TokenGroupsAndPrivileges
            TokenSessionReference
            TokenSandBoxInert
            TokenAuditPolicy
            TokenOrigin
            TokenElevationType
            TokenLinkedToken
            TokenElevation
            ' 20
            TokenHasRestrictions
            TokenAccessInformation
            TokenVirtualizationAllowed
            TokenVirtualizationEnabled
            TokenIntegrityLevel
            TokenUIAccess
            TokenMandatoryPolicy
            TokenLogonSid
            MaxTokenInfoClass
        End Enum

        Public Enum TokenType As Integer
            Primary = 1
            Impersonation
        End Enum

#End Region

#Region "Declarations used for files"

        Public Enum ECreationDisposition
            [New] = 1
            CreateAlways = 2
            OpenExisting = 3
            OpenAlways = 4
            TruncateExisting = 5
        End Enum

        Public Enum EFileAccess
            GenericRead = &H80000000
            GenericWrite = &H40000000
            GenericExecute = &H20000000
            GenericAll = &H10000000
        End Enum

        Public Enum EFileShare
            None = &H0
            Read = &H1
            Write = &H2
            Delete = &H4
        End Enum

        Public Enum EFileAttributes
            [Readonly] = &H1
            Hidden = &H2
            System = &H4
            Directory = &H10
            Archive = &H20
            Device = &H40
            Normal = &H80
            Temporary = &H100
            SparseFile = &H200
            ReparsePoint = &H400
            Compressed = &H800
            Offline = &H1000
            NotContentIndexed = &H2000
            Encrypted = &H4000
            Write_Through = &H80000000
            Overlapped = &H40000000
            NoBuffering = &H20000000
            RandomAccess = &H10000000
            SequentialScan = &H8000000
            DeleteOnClose = &H4000000
            BackupSemantics = &H2000000
            PosixSemantics = &H1000000
            OpenReparsePoint = &H200000
            OpenNoRecall = &H100000
            FirstPipeInstance = &H80000
        End Enum

        <Flags()> _
        Public Enum FileMapAccess As UInteger
            FileMapCopy = &H1
            FileMapWrite = &H2
            FileMapRead = &H4
            FileMapAllAccess = &H1F
            fileMapExecute = &H20
        End Enum

        <Flags()> _
        Public Enum FileMapProtection As UInteger
            PageReadonly = &H2
            PageReadWrite = &H4
            PageWriteCopy = &H8
            PageExecuteRead = &H20
            PageExecuteReadWrite = &H40
            SectionCommit = &H8000000
            SectionImage = &H1000000
            SectionNoCache = &H10000000
            SectionReserve = &H4000000
        End Enum

        Public Enum FO_Func As UInteger
            Move = &H1
            Copy = &H2
            Delete = &H3
            Rename = &H4
        End Enum

        <Flags()> _
        Public Enum RunFileDialogFlags As UInteger
            None = &H0
            NoBrowse = &H1
            NoDefault = &H2
            CalcDirectory = &H4
            NoLabel = &H8
            NoSeparateMemory = &H20
        End Enum

        <Flags()> _
        Public Enum ShellExecuteInfoMask As Integer
            InvokeIdList = &HC
            NoCloseProcess = &H40
            NoUI = &H400
        End Enum

#End Region

        ' OK
#Region "Declarations used for system"

        Public Enum ExitWindowsFlags As Integer
            Logoff = &H0
            Shutdown = &H1
            Reboot = &H2
            Force = &H4
            Poweroff = &H8
            ForceIfHung = &H10
            RestartApps = &H40
        End Enum

        <Flags()> _
        Public Enum KBDLLHookStructFlags As Integer
            Extended = &H1
            Injected = &H10
            AltDown = &H20
            Up = &H80
        End Enum

        Public Enum SystemInformationClass As Integer
            SystemBasicInformation
            SystemProcessorInformation
            SystemPerformanceInformation
            SystemTimeOfDayInformation
            SystemPathInformation
            SystemProcessInformation
            SystemCallCountInformation
            SystemDeviceInformation
            SystemProcessorPerformanceInformation
            SystemFlagsInformation
            SystemCallTimeInformation
            ' 10
            SystemModuleInformation
            SystemLocksInformation
            SystemStackTraceInformation
            SystemPagedPoolInformation
            SystemNonPagedPoolInformation
            SystemHandleInformation
            SystemObjectInformation
            SystemPageFileInformation
            SystemVdmInstemulInformation
            SystemVdmBopInformation
            ' 20
            SystemFileCacheInformation
            SystemPoolTagInformation
            SystemInterruptInformation
            SystemDpcBehaviorInformation
            SystemFullMemoryInformation
            SystemLoadGdiDriverInformation
            SystemUnloadGdiDriverInformation
            SystemTimeAdjustmentInformation
            SystemSummaryMemoryInformation
            SystemMirrorMemoryInformation
            ' 30
            SystemPerformanceTraceInformation
            SystemCrashDumpInformation
            SystemExceptionInformation
            SystemCrashDumpStateInformation
            SystemKernelDebuggerInformation
            SystemContextSwitchInformation
            SystemRegistryQuotaInformation
            SystemExtendServiceTableInformation
            ' used to be SystemLoadAndCallImage
            SystemPrioritySeparation
            SystemVerifierAddDriverInformation
            ' 40
            SystemVerifierRemoveDriverInformation
            SystemProcessorIdleInformation
            SystemLegacyDriverInformation
            SystemCurrentTimeZoneInformation
            SystemLookasideInformation
            SystemTimeSlipNotification
            SystemSessionCreate
            SystemSessionDetach
            SystemSessionInformation
            SystemRangeStartInformation
            ' 50
            SystemVerifierInformation
            SystemVerifierThunkExtend
            SystemSessionProcessInformation
            SystemLoadGdiDriverInSystemSpace
            SystemNumaProcessorMap
            SystemPrefetcherInformation
            SystemExtendedProcessInformation
            SystemRecommendedSharedDataAlignment
            SystemComPlusPackage
            SystemNumaAvailableMemory
            ' 60
            SystemProcessorPowerInformation
            SystemEmulationBasicInformation
            SystemEmulationProcessorInformation
            SystemExtendedHandleInformation
            SystemLostDelayedWriteInformation
            SystemBigPoolInformation
            SystemSessionPoolTagInformation
            SystemSessionMappedViewInformation
            SystemHotpatchInformation
            SystemObjectSecurityMode
            ' 70
            SystemWatchdogTimerHandler
            ' doesn't seem to be implemented
            SystemWatchdogTimerInformation
            SystemLogicalProcessorInformation
            SystemWow64SharedInformation
            SystemRegisterFirmwareTableInformationHandler
            SystemFirmwareTableInformation
            SystemModuleInformationEx
            SystemVerifierTriageInformation
            SystemSuperfetchInformation
            SystemMemoryListInformation
            ' 80
            SystemFileCacheInformationEx
            SystemNotImplemented19
            SystemProcessorDebugInformation
            SystemVerifierInformation2
            SystemNotImplemented20
            SystemRefTraceInformation
            SystemSpecialPoolTag
            ' MmSpecialPoolTag, then MmSpecialPoolCatchOverruns != 0
            SystemProcessImageName
            SystemNotImplemented21
            SystemBootEnvironmentInformation
            ' 90
            SystemEnlightenmentInformation
            SystemVerifierInformationEx
            SystemNotImplemented22
            SystemNotImplemented23
            SystemCovInformation
            SystemNotImplemented24
            SystemNotImplemented25
            SystemPartitionInformation
            SystemSystemDiskInformation
            ' this and SystemPartitionInformation both call IoQuerySystemDeviceName
            SystemPerformanceDistributionInformation
            ' 100
            SystemNumaProximityNodeInformation
            SystemTimeZoneInformation2
            SystemCodeIntegrityInformation
            SystemNotImplemented26
            SystemUnknownInformation
            ' No symbols for this case, very strange...
            SystemVaInformation
            ' 106, calls MmQuerySystemVaInformation
        End Enum

#End Region

#Region "Declarations used for windows (not Windows :-p)"

        <Flags()> _
        Public Enum FlashWInfoFlags As UInteger
            [Stop] = 0
            Caption = &H1
            Tray = &H2
            All = &H3
            Timer = &H4
            TimerNoFG = &HC
        End Enum

        Public Enum GdiBlendMode As Integer
            Black = 1
            NotMergePen
            MaskNotPen
            NotCopyPen
            MaskPenNot
            [Not]
            XorPen
            NotMaskPen
            MaskPen
            NotXorPen
            Nop
            MergeNotPen
            CopyPen
            MergePenNot
            MergePen
            White
            Last
        End Enum

        Public Enum GdiPenStyle As Integer
            Solid = 0
            Dash
            Dot
            DashDot
            DashDotDot
            Null
            InsideFrame
            UserStyle
            Alternate
        End Enum

        Public Enum GdiStockObject As Integer
            WhiteBrush = 0
            LightGrayBrush
            GrayBrush
            DarkGrayBrush
            BlackBrush
            NullBrush
            WhitePen
            BlackPen
            NullPen
            OemFixedFont
            AnsiFixedFont
            AnsiVarFont
            SystemFont
            DeviceDefaultFont
            DefaultPalette
            SystemFixedFont
            DefaultGuiFont
            DcBrush
            DcPen
        End Enum

        Public Enum GetWindowCmd As UInteger
            First = 0
            Last = 1
            [Next] = 2
            Previous = 3
            Owner = 4
            Child = 5
            EnabledPopup = 6
        End Enum

        Public Enum GetWindowLongOffset As Integer
            WndProc = -4
            HInstance = -6
            HwndParent = -8
            Id = -12
            Style = -16
            ExStyle = -20
            UserData = -21
        End Enum

        Public Enum LvsEx
            GridLines = &H1
            SubitemImages = &H2
            Checkboxes = &H4
            TrackSelect = &H8
            HeaderDragDrop = &H10
            FullRowSelect = &H20
            OneClickActivate = &H40
            TwoClickActivate = &H80
            FlatSB = &H100
            Regional = &H200
            InfoTip = &H400
            UnderlineHot = &H800
            UnderlineCold = &H1000
            MultiWorkAreas = &H2000
            LabelTip = &H4000
            BorderSelect = &H8000
            DoubleBuffer = &H10000
            HideLabels = &H20000
            SingleRow = &H40000
            SnapToGrid = &H80000
            SimpleSelect = &H100000
        End Enum

        Public Enum SendMessageTimeoutFlags As Integer
            Normal = &H0
            Block = &H1
            AbortIfHung = &H2
            NoTimeoutIfHung = &H8
        End Enum

        Public Enum ShowWindowType As UInteger
            Hide = 0
            ShowNormal = 1
            Normal = 1
            ShowMinimized = 2
            ShowMaximized = 3
            Maximize = 3
            ShowNoActivate = 4
            Show = 5
            Minimize = 6
            ShowMinNoActive = 7
            ShowNa = 8
            Restore = 9
            ShowDefault = 10
            ForceMinimize = 11
            Max = 11
        End Enum

        <Flags()> _
        Public Enum SmtoFlags As Integer
            Normal = &H0
            Block = &H1
            AbortIfHung = &H2
            NoTimeoutIfNotHung = &H8
            ErrorOnExit = &H20
        End Enum

        Public Enum WindowMessage As UInteger
            Null = &H0
            Create = &H1
            Destroy = &H2
            Move = &H3
            Size = &H5
            Activate = &H6
            SetFocus = &H7
            KillFocus = &H8
            Enable = &HA
            SetRedraw = &HB
            SetText = &HC
            GetText = &HD
            GetTextLength = &HE
            Paint = &HF
            Close = &H10
            QueryEndSession = &H11
            Quit = &H12
            QueryOpen = &H13
            EraseBkgnd = &H14
            SysColorChange = &H15
            EndSession = &H16
            SystemError = &H17
            ShowWindow = &H18
            CtlColor = &H19
            WinIniChange = &H1A
            SettingChange = &H1A
            DevModeChange = &H1B
            ActivateApp = &H1C
            FontChange = &H1D
            TimeChange = &H1E
            CancelMode = &H1F
            SetCursor = &H20
            MouseActivate = &H21
            ChildActivate = &H22
            QueueSync = &H23
            GetMinMaxInfo = &H24
            PaintIcon = &H26
            IconEraseBkgnd = &H27
            NextDlgCtl = &H28
            SpoolerStatus = &H2A
            DrawIcon = &H2B
            MeasureItem = &H2C
            DeleteItem = &H2D
            VKeyToItem = &H2E
            CharToItem = &H2F

            SetFont = &H30
            GetFont = &H31
            SetHotkey = &H32
            GetHotkey = &H33
            QueryDragIcon = &H37
            CompareItem = &H39
            Compacting = &H41
            WindowPosChanging = &H46
            WindowPosChanged = &H47
            Power = &H48
            CopyData = &H4A
            CancelJournal = &H4B
            Notify = &H4E
            InputLangChangeRequest = &H50
            InputLangChange = &H51
            TCard = &H52
            Help = &H53
            UserChanged = &H54
            NotifyFormat = &H55
            ContextMenu = &H7B
            StyleChanging = &H7C
            StyleChanged = &H7D
            DisplayChange = &H7E
            GetIcon = &H7F
            SetIcon = &H80

            NcCreate = &H81
            NcDestroy = &H82
            NcCalcSize = &H83
            NcHitTest = &H84
            NcPaint = &H85
            NcActivate = &H86
            GetDlgCode = &H87
            NcMouseMove = &HA0
            NcLButtonDown = &HA1
            NcLButtonUp = &HA2
            NcLButtonDblClk = &HA3
            NcRButtonDown = &HA4
            NcRButtonUp = &HA5
            NcRButtonDblClk = &HA6
            NcMButtonDown = &HA7
            NcMButtonUp = &HA8
            NcMButtonDblClk = &HA9

            KeyDown = &H100
            KeyUp = &H101
            [Char] = &H102
            DeadChar = &H103
            SysKeyDown = &H104
            SysKeyUp = &H105
            SysChar = &H106
            SysDeadChar = &H107

            ImeStartComposition = &H10D
            ImeEndComposition = &H10E
            ImeComposition = &H10F
            ImeKeyLast = &H10F

            InitDialog = &H110
            Command = &H111
            SysCommand = &H112
            Timer = &H113
            HScroll = &H114
            VScroll = &H115
            InitMenu = &H116
            InitMenuPopup = &H117
            MenuSelect = &H11F
            MenuChar = &H120
            EnterIdle = &H121

            CtlColorMsgBox = &H132
            CtlColorEdit = &H133
            CtlColorListBox = &H134
            CtlColorBtn = &H135
            CtlColorDlg = &H136
            CtlColorScrollbar = &H137
            CtlColorStatic = &H138

            MouseMove = &H200
            LButtonDown = &H201
            LButtonUp = &H202
            LButtonDblClk = &H203
            RButtonDown = &H204
            RButtonUp = &H205
            RButtonDblClk = &H206
            MButtonDown = &H207
            MButtonUp = &H208
            MButtonDblClk = &H209
            MouseWheel = &H20A

            ParentNotify = &H210
            EnterMenuLoop = &H211
            ExitMenuLoop = &H212
            NextMenu = &H213
            Sizing = &H214
            CaptureChanged = &H215
            Moving = &H216
            PowerBroadcast = &H218
            DeviceChange = &H219

            MdiCreate = &H220
            MdiDestroy = &H221
            MdiActivate = &H222
            MdiRestore = &H223
            MdiNext = &H224
            MdiMaximize = &H225
            MdiTile = &H226
            MdiCascade = &H227
            MdiIconArrange = &H228
            MdiGetActive = &H229
            MdiSetMenu = &H230
            EnterSizeMove = &H231
            ExitSizeMove = &H232
            DropFiles = &H233
            MdiRefreshMenu = &H234

            ImeSetContext = &H281
            ImeNotify = &H282
            ImeControl = &H283
            ImeCompositionFull = &H284
            ImeSelect = &H285
            ImeChar = &H286
            ImeKeyDown = &H290
            ImeKeyUp = &H291

            NcMouseHover = &H2A0
            MouseHover = &H2A1
            NcMouseLeave = &H2A2
            MouseLeave = &H2A3

            WtsSessionChange = &H2B1

            TabletFirst = &H2C0
            TabletLast = &H2DF

            Cut = &H300
            Copy = &H301
            Paste = &H302
            Clear = &H303
            Undo = &H304

            RenderFormat = &H305
            RenderAllFormats = &H306
            DestroyClipboard = &H307
            DrawClipboard = &H308
            PaintClipboard = &H309
            VScrollClipboard = &H30A
            SizeClipboard = &H30B
            AskCbFormatName = &H30C
            ChangeCbChain = &H30D
            HScrollClipboard = &H30E
            QueryNewPalette = &H30F
            PaletteIsChanging = &H310
            PaletteChanged = &H311

            Hotkey = &H312
            Print = &H317
            PrintClient = &H318

            HandheldFirst = &H358
            HandheldLast = &H35F
            PenWinFirst = &H380
            PenWinLast = &H38F
            CoalesceFirst = &H390
            CoalesceLast = &H39F
            DdeInitiate = &H3E0
            DdeTerminate = &H3E1
            DdeAdvise = &H3E2
            DdeUnadvise = &H3E3
            DdeAck = &H3E4
            DdeData = &H3E5
            DdeRequest = &H3E6
            DdePoke = &H3E7
            DdeExecute = &H3E8

            User = &H400

            BcmSetShield = &H160C

            App = &H8000
        End Enum

        <Flags()> _
        Public Enum WindowPlacementFlags As Integer
            SetMinPosition = &H1
            RestoreToMaximized = &H2
            AsyncWindowPlacement = &H4
        End Enum

#End Region

        ' OK
#Region "Declarations used for services"

        Public Enum ServiceAccept As UInteger
            [NetBindChange] = &H10
            [ParamChange] = &H8
            [PauseContinue] = &H2
            [PreShutdown] = &H100
            [Shutdown] = &H4
            [Stop] = &H1
            [HardwareProfileChange] = &H20
            [PowerEvent] = &H40
            [SessionChange] = &H80
        End Enum

        Public Enum ServiceControl
            [Stop] = 1
            [Pause] = 2
            [Continue] = 3
            Interrogate = 4
            Shutdown = 5
            ParamChange = 6
            NetBindAdd = 7
            NetBindRemove = 8
            NetBindEnable = 9
            NetBindDisable = 10
            DeviceEvent = 11
            HardwareProfileChange = 12
            PowerEvent = 13
            SessionChange = 14
        End Enum

        Public Enum ServiceErrorControl As Integer
            Critical = &H3
            Ignore = &H0
            Normal = &H1
            Severe = &H2
            Unknown = &HF
            NoChange = &HFFFFFFFF
        End Enum

        Public Enum ServiceFlags As UInteger
            None = 0
            RunsInSystemProcess = &H1
        End Enum

        Public Enum ServiceQueryState As UInteger
            Active = 1
            Inactive = 2
            All = 3
        End Enum

        <Flags()> _
        Public Enum ServiceQueryType As UInteger
            Driver = &HB
            Win32 = &H30
            All = Driver Or Win32
        End Enum

        Public Enum ServiceStartType As Integer
            BootStart = &H0
            SystemStart = &H1
            AutoStart = &H2
            DemandStart = &H3
            StartDisabled = &H4
            NoChange = &HFFFFFFFF
        End Enum

        Public Enum ServiceState As UInteger
            ContinuePending = &H5
            PausePending = &H6
            Paused = &H7
            Running = &H4
            StartPending = &H2
            StopPending = &H3
            Stopped = &H1
            Unknown = &HF
        End Enum

        <Flags()> _
        Public Enum ServiceType As UInteger
            FileSystemDriver = &H2
            KernelDriver = &H1
            Adapter = &H4
            RecognizerDriver = &H8
            Win32OwnProcess = &H10
            Win32ShareProcess = &H20
            InteractiveProcess = &H100
            NoChange = &HFFFFFFFF
        End Enum

#End Region

        ' OK
#Region "Declarations used for registry"

        ' Type of monitoring to apply
        <Flags()> _
        Public Enum KeyMonitoringType
            ChangeName = &H1            ' Subkey added or deleted
            ChangeAttributes = &H2      ' Attributes changed
            ChangeLastSet = &H4         ' Value changed (changed, deleted, added)
            ChangeSecurity = &H8        ' Security descriptor changed
        End Enum

        ' http://msdn.microsoft.com/en-us/library/ms724892(VS.85).aspx
        ' Type of Key
        Public Enum KeyType
            ClassesRoot = &H80000000
            CurrentUser = &H80000001
            LocalMachine = &H80000002
            Users = &H80000003
            CurrentConfig = &H80000005
            PerformanceData = &H80000004
            DynData = &H80000006
        End Enum

        Public Enum WaitResult As UInteger
            Infinite = &HFFFFFFFF
            Abandoned = &H80
            Object_0 = &H0
            TimeOut = &H102
            Failed = &HFFFFFFFF
        End Enum

#End Region

        ' OK
#Region "Declarations used for graphical functions"

        Public Enum IconSize As Integer
            Small = &H0
            Big = &H1
        End Enum

        Public Enum LVM As UInteger
            First = &H1000
            SetExtendedListviewStyle = (First + 54)
            GetExtendedListviewStyle = (First + 55)
        End Enum

#End Region

        ' OK
#Region "Declarations used for keyboard management"

        Public Enum HookType As Byte
            JournalRecord = 0
            JournalPlayBack = 1
            Kerboard = 2
            GetMessage = 3
            CallWindowProc = 4
            Cbt = 5
            SysMsgFilter = 6
            Mouse = 7
            Hardware = 8
            Debug = 9
            Shell = 10
            ForegroundIdle = 11
            CallWindowProcRet = 12
            KeyboardLowLevel = 13
            MouseLowLevel = 14
        End Enum

#End Region

        ' OK
#Region "Declarations used for error management"

        <Flags()> _
        Public Enum FormatMessageFlags As Integer
            AllocateBuffer = &H100
            ArgumentArray = &H2000
            FromHModule = &H800
            FromString = &H400
            FromSystem = &H1000
            MessageIgnoreInserts = &H200
        End Enum

#End Region

        ' OK
#Region "Declarations used for handles"

        <Flags()> _
        Public Enum DuplicateOptions As Integer
            CloseSource = &H1
            SameAccess = &H2
            SameAttributes = &H4
        End Enum

        <Flags()> _
        Public Enum HandleFlags As Byte
            ProtectFromClose = &H1
            Inherit = &H2
            AuditObjectClose = &H4
        End Enum

        <Flags()> _
        Public Enum ObjectFlags As UInteger
            Inherit = &H2
            Permanent = &H10
            Exclusive = &H20
            CaseInsensitive = &H40
            OpenIf = &H80
            OpenLink = &H100
            KernelHandle = &H200
            ForceAccessCheck = &H400
            ValidAttributes = &H7F2
        End Enum

        Public Enum ObjectInformationClass As Integer
            ObjectAttributes = 0
            ObjectNameInformation = 1
            ObjectTypeInformation = 2
            ObjectTypesInformation = 3
            ObjectHandleFlagInformation = 4
            ObjectSessionInformation = 5
        End Enum

        Public Enum PoolType As UInteger
            NonPagedPool
            PagedPool
            NonPagedPoolMustSucceed
            DontUseThisType
            NonPagedPoolCacheAligned
            PagedPoolCacheAligned
            NonPagedPoolCacheAlignedMustS
        End Enum

#End Region

        ' OK
#Region "Declarations used for network connections"

        <Flags()> _
        Public Enum AddConnectionFlag As Integer
            ConnectUpdateProfile = &H1
            ConnectUpdateRecent = &H2
            ConnectTemporary = &H4
            ConnectInteractive = &H8
            ConnectPrompt = &H10
            ConnectRedirect = &H80
            ConnectCurrentMedia = &H200
            ConnectCommandLine = &H800
            ConnectCmdSaveCred = &H1000
            ConnectCredReset = &H2000
        End Enum

        Public Enum IpVersion As UInteger
            AfInet = &H2
            AfInt6 = &H17
        End Enum

        Public Enum MibTcpRtoAlgorithm As UInteger
            [Other] = &H1
            [ConstantTimeOut] = &H2
            [MIL_STD_1778_Appendix_B] = &H3
            [VanJacobson] = &H4
        End Enum

        ''' <summary>
        ''' Display options for the network object in a network browsing user interface.
        ''' </summary>
        Public Enum NetResourceDisplayType As UInteger
            ''' <summary>
            ''' The object should be displayed as a domain.
            ''' </summary>
            Generic = &H0

            ''' <summary>
            ''' The object should be displayed as a server.
            ''' </summary>
            Domain = &H1

            ''' <summary>
            ''' The object should be displayed as a share.
            ''' </summary>
            Server = &H2

            ''' <summary>
            ''' The method used to display the object does not matter.
            ''' </summary>
            Share = &H3
        End Enum

        ''' <summary>
        ''' Scope of the enumeration.
        ''' </summary>
        Public Enum NetResourceScope As UInteger
            ''' <summary>
            ''' Enumerate currently connected resources. The dwUsage member cannot be specified.
            ''' </summary>
            Connected = &H1

            ''' <summary>
            ''' Enumerate all resources on the network. The dwUsage member is specified.
            ''' </summary>
            GlobalNet = &H2

            ''' <summary>
            ''' Enumerate remembered (persistent) connections. The dwUsage member cannot be specified.
            ''' </summary>
            Remembered = &H3
        End Enum

        ''' <summary>
        ''' Set of bit flags identifying the type of resource.
        ''' </summary>
        Public Enum NetResourceType As UInteger
            ''' <summary>
            ''' All resources
            ''' </summary>
            Any = &H0

            ''' <summary>
            ''' Disk resources
            ''' </summary>
            Disk = &H1

            ''' <summary>
            ''' Print resources
            ''' </summary>
            Print = &H2
        End Enum

        ''' <summary>
        ''' Set of bit flags describing how the resource can be used. 
        ''' Note that this member can be specified only if the dwScope member is equal to RESOURCE_GLOBALNET.
        ''' </summary>
        Public Enum NetResourceUsage As UInteger
            ''' <summary>
            ''' The resource is a connectable resource; the name pointed to by the lpRemoteName member can be passed to the WNetAddConnection function to make a network connection.
            ''' </summary>
            Connectable = &H1

            ''' <summary>
            ''' The resource is a container resource; the name pointed to by the lpRemoteName member can be passed to the WNetOpenEnum function to enumerate the resources in the container.
            ''' </summary>
            Container = &H2
        End Enum

#End Region

        ' OK
#Region "Declarations used for debugging"

        <Flags()> _
        Public Enum HeapBlockFlag As Integer
            Fixed = &H1
            Free = &H2
            Moveable = &H4
        End Enum

        <Flags()> _
        Public Enum HeapListEntryFlags As Integer
            [Default] = &H1
        End Enum

        ' http://www.cygwin.com/ml/cygwin-talk/2006-q2/txt00000.txt
        <Flags()> _
        Public Enum RtlQueryProcessDebugInformationFlags As UInteger
            Modules = &H1
            BackTrace = &H2
            Heaps = &H4
            HeapTags = &H8
            HeapBlocks = &H10
            Locks = &H20
            Modules32 = &H40
        End Enum

#End Region

        ' OK
#Region "Declarations used for wintrust verification"

        Public Enum WinTrustDataChoice As UInteger
            File = 1
            Catalog = 2
            Blob = 3
            Signer = 4
            Certificate = 5
        End Enum

        Public Enum WinTrustDataUIChoice As UInteger
            All = 1
            None = 2
            NoBad = 3
            NoGood = 4
        End Enum

        <FlagsAttribute()> _
        Public Enum WinTrustDataProvFlags As UInteger
            UseIe4TrustFlag = &H1
            NoIe4ChainFlag = &H2
            NoPolicyUsageFlag = &H4
            RevocationCheckNone = &H10
            RevocationCheckEndCert = &H20
            RevocationCheckChain = &H40
            RevocationCheckChainExcludeRoot = &H80
            SaferFlag = &H100
            HashOnlyFlag = &H200
            UseDefaultOsverCheck = &H400
            LifetimeSigningFlag = &H800
            CacheOnlyUrlRetrieval = &H1000
        End Enum

        Public Enum WinTrustDataRevocationChecks As UInteger
            None = &H0
            WholeChain = &H1
        End Enum

        Public Enum WinTrustDataStateAction As UInteger
            Ignore = &H0
            Verify = &H1
            Close = &H2
            AutoCache = &H3
            AutoCacheFlush = &H4
        End Enum

        Public Enum WinTrustDataUIContext As UInteger
            Execute = 0
            Install = 1
        End Enum

        Public Enum WinVerifyTrustResult As Integer
            Trusted = 0
            ProviderUnknown = &H800B0001
            ActionUnknown = &H800B0002
            SubjectFormUnknown = &H800B0003
            SubjectNotTrusted = &H800B0004
            NoSignature = &H800B0100
            Expired = &H800B0101
            ValidityPeriodNesting = &H800B0102
            Role = &H800B0103
            PathLenConst = &H800B0104
            Critical = &H800B0105
            Purpose = &H800B0106
            IssuerChaining = &H800B0107
            Malformed = &H800B0108
            UntrustedRoot = &H800B0109
            Chaining = &H800B010A
            Revoked = &H800B010C
            UntrustedTestRoot = &H800B010D
            RevocationFailure = &H800B010E
            CNNotMatch = &H800B010F
            WrongUsage = &H800B0110
            ExplicitDistrust = &H800B0111
            UntrustedCA = &H800B0112
            SecuritySettings = &H80092026
        End Enum

#End Region

    End Class

End Namespace
