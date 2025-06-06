﻿'
'' Lite Process Monitor
'


'Option Strict On

'Public Class cBasedStateActionState

'    <System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError:=True)> _
'    Private Shared Function Beep(ByVal dwFreq As UInteger, ByVal dwDuration As UInteger) As Boolean
'    End Function
'    
'    ' Public Enums
'    
'    Public Enum STATE_OPERATOR As Integer
'        [less_than] = 1
'        [less_or_equal_than] = 2
'        [equal] = 3
'        [greater_or_equal_than] = 4
'        [greater_than] = 5
'        [different_from] = 6
'    End Enum
'    Public Enum ThresoldType As Integer
'        [INTEGER]           ' as an integer
'        [STRING]            ' as a string
'        [DECIMAL]           ' as a double
'        [SIZE_MB]           ' as a string
'        [ENUM_PRIORITY]     ' as a string I-BN-N-AN-H-RT
'        [TIME]              ' as a string 00:00:00
'    End Enum


'    ' StateThreshold type
'    Public Structure StateThreshold
'        Dim _type As ThresoldType
'        Dim _sval As String
'        Dim _dval As Double
'        Dim _ival As Integer

'        Public Sub New(ByVal theType As ThresoldType, ByVal value As Double)
'            _type = theType
'            _dval = value
'        End Sub
'        Public Sub New(ByVal theType As ThresoldType, ByVal value As String)
'            _type = theType
'            _sval = value
'        End Sub
'        Public Sub New(ByVal theType As ThresoldType, ByVal value As Integer)
'            _type = theType
'            _ival = value
'        End Sub

'        ' _type should be the same for h1 and h2 !!!
'        Public Shared Operator <(ByVal h1 As StateThreshold, ByVal h2 As StateThreshold) As Boolean
'            Select Case h1._type
'                Case ThresoldType.DECIMAL
'                    Return h1._dval < h2._dval
'                Case ThresoldType.ENUM_PRIORITY
'                    Return getEnumPriorityCorIntValue(h1._sval) < getEnumPriorityCorIntValue(h2._sval)
'                Case ThresoldType.INTEGER
'                    Return h1._ival < h2._ival
'                Case ThresoldType.SIZE_MB
'                    Return h1._dval < GetSizeFromFormatedSize(h2._sval)
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return h1._sval < h2._sval
'                Case Else
'                    Return False
'            End Select
'        End Operator
'        Public Shared Operator >(ByVal h1 As StateThreshold, ByVal h2 As StateThreshold) As Boolean
'            Select Case h1._type
'                Case ThresoldType.DECIMAL
'                    Return h1._dval > h2._dval
'                Case ThresoldType.ENUM_PRIORITY
'                    Return getEnumPriorityCorIntValue(h1._sval) > getEnumPriorityCorIntValue(h2._sval)
'                Case ThresoldType.INTEGER
'                    Return h1._ival > h2._ival
'                Case ThresoldType.SIZE_MB
'                    Return h1._dval > GetSizeFromFormatedSize(h2._sval)
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return h1._sval > h2._sval
'                Case Else
'                    Return False
'            End Select
'        End Operator
'        Public Shared Operator <>(ByVal h1 As StateThreshold, ByVal h2 As StateThreshold) As Boolean
'            Select Case h1._type
'                Case ThresoldType.DECIMAL
'                    Return h1._dval <> h2._dval
'                Case ThresoldType.ENUM_PRIORITY
'                    Return getEnumPriorityCorIntValue(h1._sval) <> getEnumPriorityCorIntValue(h2._sval)
'                Case ThresoldType.INTEGER
'                    Return h1._ival <> h2._ival
'                Case ThresoldType.SIZE_MB
'                    Return h1._dval <> GetSizeFromFormatedSize(h2._sval)
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return h1._sval <> h2._sval
'                Case Else
'                    Return False
'            End Select
'        End Operator
'        Public Shared Operator =(ByVal h1 As StateThreshold, ByVal h2 As StateThreshold) As Boolean
'            Select Case h1._type
'                Case ThresoldType.DECIMAL
'                    Return h1._dval = h2._dval
'                Case ThresoldType.ENUM_PRIORITY
'                    Return getEnumPriorityCorIntValue(h1._sval) = getEnumPriorityCorIntValue(h2._sval)
'                Case ThresoldType.INTEGER
'                    Return h1._ival = h2._ival
'                Case ThresoldType.SIZE_MB
'                    Return h1._dval = GetSizeFromFormatedSize(h2._sval)
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return h1._sval = h2._sval
'                Case Else
'                    Return False
'            End Select
'        End Operator
'        Public Shared Operator <=(ByVal h1 As StateThreshold, ByVal h2 As StateThreshold) As Boolean
'            Select Case h1._type
'                Case ThresoldType.DECIMAL
'                    Return h1._dval <= h2._dval
'                Case ThresoldType.ENUM_PRIORITY
'                    Return getEnumPriorityCorIntValue(h1._sval) <= getEnumPriorityCorIntValue(h2._sval)
'                Case ThresoldType.INTEGER
'                    Return h1._ival <= h2._ival
'                Case ThresoldType.SIZE_MB
'                    Return h1._dval <= GetSizeFromFormatedSize(h2._sval)
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return h1._sval <= h2._sval
'                Case Else
'                    Return False
'            End Select
'        End Operator
'        Public Shared Operator >=(ByVal h1 As StateThreshold, ByVal h2 As StateThreshold) As Boolean
'            Select Case h1._type
'                Case ThresoldType.DECIMAL
'                    Return h1._dval >= h2._dval
'                Case ThresoldType.ENUM_PRIORITY
'                    Return getEnumPriorityCorIntValue(h1._sval) >= getEnumPriorityCorIntValue(h2._sval)
'                Case ThresoldType.INTEGER
'                    Return h1._ival >= h2._ival
'                Case ThresoldType.SIZE_MB
'                    Return h1._dval >= GetSizeFromFormatedSize(h2._sval)
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return h1._sval >= h2._sval
'                Case Else
'                    Return False
'            End Select
'        End Operator

'        Private Shared Function getEnumPriorityCorIntValue(ByVal s As String) As Integer
'            Select Case s   'I-BN-N-AN-H-RT
'                Case "I"
'                    Return 1
'                Case "BN"
'                    Return 2
'                Case "N"
'                    Return 3
'                Case "AN"
'                    Return 4
'                Case "H"
'                    Return 5
'                Case "RT"
'                    Return 6
'                Case Else
'                    Return 0
'            End Select
'        End Function

'        Public Overrides Function ToString() As String
'            Select Case _type
'                Case ThresoldType.DECIMAL
'                    Return _dval.ToString
'                Case ThresoldType.ENUM_PRIORITY
'                    Return _sval
'                Case ThresoldType.INTEGER
'                    Return _ival.ToString
'                Case ThresoldType.SIZE_MB
'                    Return _sval
'                Case ThresoldType.STRING, ThresoldType.TIME
'                    Return _sval
'                Case Else
'                    Return "0"
'            End Select
'        End Function
'    End Structure



'    
'    ' Private variables
'    
'    Private _checkProcName As Boolean
'    Private _checkProcNameS As String = ""
'    Private _checkProcID As Boolean
'    Private _checkProcIDS As String = ""
'    Private _checkProcPath As Boolean
'    Private _checkProcPathS As String = ""
'    Private _stateCounter As String
'    Private _stateOperator As STATE_OPERATOR
'    Private _threshold As StateThreshold
'    Private _action As String = ""
'    Private _param1 As String = ""
'    Private _param2 As String = ""
'    Private _theCounter As Integer
'    Private _theCounterInitial As Integer
'    Private _enabled As Boolean
'    Private _notify As Boolean
'    Private _key As String


'    
'    ' Public properties
'    
'#Region "Properties"
'    Public Property Counter() As Integer
'        Get
'            Return _theCounter
'        End Get
'        Set(ByVal value As Integer)
'            _theCounter = value
'        End Set
'    End Property
'    Public Property InitialCounter() As Integer
'        Get
'            Return _theCounterInitial
'        End Get
'        Set(ByVal value As Integer)
'            _theCounterInitial = value
'        End Set
'    End Property
'    Public Property CheckProcName() As Boolean
'        Get
'            Return _checkProcName
'        End Get
'        Set(ByVal value As Boolean)
'            _checkProcName = value
'        End Set
'    End Property
'    Public Property CheckProcNameS() As String
'        Get
'            Return _checkProcNameS
'        End Get
'        Set(ByVal value As String)
'            _checkProcNameS = value
'        End Set
'    End Property
'    Public Property CheckProcID() As Boolean
'        Get
'            Return _checkProcID
'        End Get
'        Set(ByVal value As Boolean)
'            _checkProcID = value
'        End Set
'    End Property
'    Public Property Notify() As Boolean
'        Get
'            Return _notify
'        End Get
'        Set(ByVal value As Boolean)
'            _notify = value
'        End Set
'    End Property
'    Public Property CheckProcIDS() As String
'        Get
'            Return _checkProcIDS
'        End Get
'        Set(ByVal value As String)
'            _checkProcIDS = value
'        End Set
'    End Property
'    Public Property CheckProcPath() As Boolean
'        Get
'            Return _checkProcPath
'        End Get
'        Set(ByVal value As Boolean)
'            _checkProcPath = value
'        End Set
'    End Property
'    Public Property CheckProcPathS() As String
'        Get
'            Return _checkProcPathS
'        End Get
'        Set(ByVal value As String)
'            _checkProcPathS = value
'        End Set
'    End Property
'    Public Property StateCounter() As String
'        Get
'            Return _stateCounter
'        End Get
'        Set(ByVal value As String)
'            _stateCounter = value
'        End Set
'    End Property
'    Public Property StateOperator() As STATE_OPERATOR
'        Get
'            Return _stateOperator
'        End Get
'        Set(ByVal value As STATE_OPERATOR)
'            _stateOperator = value
'        End Set
'    End Property
'    Public Property Threshold() As StateThreshold
'        Get
'            Return _threshold
'        End Get
'        Set(ByVal value As StateThreshold)
'            _threshold = value
'        End Set
'    End Property
'    Public Property Action() As String
'        Get
'            Return _action
'        End Get
'        Set(ByVal value As String)
'            _action = value
'        End Set
'    End Property
'    Public Property Param1() As String
'        Get
'            Return _param1
'        End Get
'        Set(ByVal value As String)
'            _param1 = value
'        End Set
'    End Property
'    Public Property Param2() As String
'        Get
'            Return _param2
'        End Get
'        Set(ByVal value As String)
'            _param2 = value
'        End Set
'    End Property
'    Public Property Enabled() As Boolean
'        Get
'            Return _enabled
'        End Get
'        Set(ByVal value As Boolean)
'            _enabled = value
'        End Set
'    End Property
'    Public Property Key() As String
'        Get
'            Return _key
'        End Get
'        Set(ByVal value As String)
'            _key = value
'        End Set
'    End Property
'    Public ReadOnly Property ProcessText() As String
'        Get
'            Dim _s As String = ""
'            If _checkProcID Then
'                _s &= "ID (" & _checkProcIDS & ")    "
'            End If
'            If _checkProcName Then
'                _s &= "Name (" & _checkProcNameS & ")    "
'            End If
'            If _checkProcPath Then
'                _s &= "Path (" & _checkProcPathS & ")"
'            End If
'            Return _s
'        End Get
'    End Property
'    Public ReadOnly Property StateText() As String
'        Get
'            Return _stateCounter & " " & getOperator(_stateOperator) & " " & _
'                    _threshold.ToString
'        End Get
'    End Property
'    Public ReadOnly Property ActionText() As String
'        Get
'            Return _action
'        End Get
'    End Property
'    Public ReadOnly Property RuleText() As String
'        Get
'            Return ProcessText & " " & StateText
'        End Get
'    End Property
'#End Region


'    
'    ' Public functions
'    
'    Public Sub New(ByVal checkPN As Boolean, ByVal checkPI As Boolean, ByVal _
'                   checkPP As Boolean, ByVal stateCounter As String, ByVal _
'                   stateOperator As STATE_OPERATOR, ByVal threshold As String, ByVal _
'                   action As String, ByVal param1 As String, ByVal param2 As String, _
'                   Optional ByVal checkPNS As String = "", Optional ByVal checkPIS _
'                   As String = "", Optional ByVal checkPPS As String = "", Optional ByVal _
'                   counter As String = "0", Optional ByVal notif As Boolean = False)

'        _checkProcID = checkPI
'        _checkProcIDS = checkPIS
'        _checkProcName = checkPN
'        _checkProcNameS = checkPNS
'        _checkProcPath = checkPP
'        _checkProcPathS = checkPPS
'        _stateCounter = stateCounter
'        _notify = notif
'        _theCounterInitial = CInt(Val(counter))
'        _theCounter = _theCounterInitial
'        ' Create _threshold as StateThreshold from a threshold (string)
'        ' and from action (have to make a select case)
'        Select Case stateCounter
'            Case "PID"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "UserName"
'                _threshold = New StateThreshold(ThresoldType.STRING, threshold)
'            Case "ParentPID"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "ParentName"
'                _threshold = New StateThreshold(ThresoldType.STRING, threshold)
'            Case "CpuUsage"
'                _threshold = New StateThreshold(ThresoldType.DECIMAL, Val(threshold))
'            Case "AverageCpuUsage"
'                _threshold = New StateThreshold(ThresoldType.DECIMAL, Val(threshold))
'            Case "KernelCpuTime"
'                _threshold = New StateThreshold(ThresoldType.DECIMAL, Val(threshold))
'            Case "UserCpuTime"
'                _threshold = New StateThreshold(ThresoldType.DECIMAL, Val(threshold))
'            Case "TotalCpuTime"
'                _threshold = New StateThreshold(ThresoldType.DECIMAL, Val(threshold))
'            Case "StartTime"
'                _threshold = New StateThreshold(ThresoldType.TIME, threshold)
'            Case "RunTime"
'                _threshold = New StateThreshold(ThresoldType.TIME, threshold)
'            Case "GdiObjects"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "UserObjects"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "AffinityMask"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "WorkingSet"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "PeakWorkingSet"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "PageFaultCount"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "PagefileUsage"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "PeakPagefileUsage"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "QuotaPeakPagedPoolUsage"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "QuotaPagedPoolUsage"     '
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "QuotaPeakNonPagedPoolUsage"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "QuotaNonPagedPoolUsage"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "ReadOperationCount"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "WriteOperationCount"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "OtherOperationCount"
'                _threshold = New StateThreshold(ThresoldType.INTEGER, CInt(Val(threshold)))
'            Case "ReadTransferCount"     '
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "WriteTransferCount"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "OtherTransferCount"
'                _threshold = New StateThreshold(ThresoldType.SIZE_MB, threshold)
'            Case "Priority"
'                _threshold = New StateThreshold(ThresoldType.ENUM_PRIORITY, threshold)
'            Case "Path"
'                _threshold = New StateThreshold(ThresoldType.STRING, threshold)
'        End Select

'        _stateOperator = stateOperator
'        _action = action
'        _param1 = param1
'        _param2 = param2
'        _key = _checkProcName.ToString & "|" & _checkProcNameS & "|" & _
'                _checkProcID.ToString & "|" & _checkProcIDS & "|" & _
'                _checkProcPath.ToString & "|" & _checkProcPathS & "|" & _
'                _stateCounter & "|" & _stateOperator.ToString & "|" & _threshold.ToString & _
'                "|" & _action & "|" & _param1 & "|" & _param2
'        _enabled = True
'    End Sub

'    ' Proceed to action !
'    Public Sub RaiseAction(ByRef _proc As cLocalProcess)

'        Trace.WriteLine("Have to raise action : " & Action)
'        cStateBasedActions.Notify(Me, _proc)

'        Try
'            Select Case Action
'                Case "Kill process"
'                    _proc.Kill()
'                Case "Pause process"
'                    _proc.SuspendProcess()
'                Case "Resume process"
'                    _proc.ResumeProcess()
'                Case "Change priority"
'                    Select Case _param1
'                        Case "RT"
'                            _proc.SetProcessPriority(ProcessPriorityClass.RealTime)
'                        Case "H"
'                            _proc.SetProcessPriority(ProcessPriorityClass.High)
'                        Case "AN"
'                            _proc.SetProcessPriority(ProcessPriorityClass.AboveNormal)
'                        Case "N"
'                            _proc.SetProcessPriority(ProcessPriorityClass.Normal)
'                        Case "BN"
'                            _proc.SetProcessPriority(ProcessPriorityClass.BelowNormal)
'                        Case "I"
'                            _proc.SetProcessPriority(ProcessPriorityClass.Idle)
'                    End Select
'                Case "Reduce priority"
'                    _proc.DecreasePriority()
'                Case "Increase priority"
'                    _proc.IncreasePriority()
'                Case "Activate process log"
'                    cStateBasedActions.StartLog(_proc)
'                Case "Change affinity"
'                    _proc.AffinityMask = Integer.Parse(_param1)
'                Case "Launch a command"
'                    Shell("cmd.exe /C " & Chr(34) & _param1 & Chr(34), AppWinStyle.Hide)
'                Case "Restart computer"
'                    cSystem.Restart()
'                Case "Shutdown computer"
'                    cSystem.Shutdown()
'                Case "Poweroff computer"
'                    cSystem.Poweroff()
'                Case "Sleep computer"
'                    cSystem.Sleep()
'                Case "Hibernate computer"
'                    cSystem.Hibernate()
'                Case "Logoff computer"
'                    cSystem.Logoff()
'                Case "Lock computer"
'                    cSystem.Lock()
'                Case "Exit LitePM"
'                    cStateBasedActions.ExitLitePM()
'                Case "Show process task windows"
'                    ' Get all windows owned by _proc
'                    Dim pid(0) As Integer
'                    Dim keys() As Integer = Nothing
'                    Dim _dico As New Dictionary(Of String, cWindow.LightWindow)
'                    pid(0) = _proc.Pid
'                    cWindow.Enumerate(False, pid, keys, _dico)
'                    For Each win As cWindow.LightWindow In _dico.Values
'                        ' Only if is a task
'                        If cWindow.IsWindowTask(win.handle) Then
'                            cWindow.ShowWindow(win.handle.ToInt32)
'                        End If
'                    Next
'                Case "Hide process task windows"
'                    ' Get all windows owned by _proc
'                    Dim pid(0) As Integer
'                    Dim keys() As Integer = Nothing
'                    Dim _dico As New Dictionary(Of String, cWindow.LightWindow)
'                    pid(0) = _proc.Pid
'                    cWindow.Enumerate(False, pid, keys, _dico)
'                    For Each win As cWindow.LightWindow In _dico.Values
'                        ' Only if is a task
'                        If cWindow.IsWindowTask(win.handle) Then
'                            cWindow.HideWindow(win.handle)
'                        End If
'                    Next
'                Case "Maximize process task windows"
'                    ' Get all windows owned by _proc
'                    Dim pid(0) As Integer
'                    Dim keys() As Integer = Nothing
'                    Dim _dico As New Dictionary(Of String, cWindow.LightWindow)
'                    pid(0) = _proc.Pid
'                    cWindow.Enumerate(False, pid, keys, _dico)
'                    For Each win As cWindow.LightWindow In _dico.Values
'                        ' Only if is a task
'                        If cWindow.IsWindowTask(win.handle) Then
'                            cWindow.MaximizeWindow(win.handle)
'                        End If
'                    Next
'                Case "Minimize process task windows"
'                    ' Get all windows owned by _proc
'                    Dim pid(0) As Integer
'                    Dim keys() As Integer = Nothing
'                    Dim _dico As New Dictionary(Of String, cWindow.LightWindow)
'                    pid(0) = _proc.Pid
'                    cWindow.Enumerate(False, pid, keys, _dico)
'                    For Each win As cWindow.LightWindow In _dico.Values
'                        ' Only if is a task
'                        If cWindow.IsWindowTask(win.handle) Then
'                            cWindow.MinimizeWindow(win.handle)
'                        End If
'                    Next
'                Case "Beep"
'                    Interaction.Beep()
'                Case "Save process list"
'                    cStateBasedActions.SaveProcessList(_param1 & "\" & Date.Now.Ticks.ToString & "_" & _param2)
'                Case "Save service list"
'                    cStateBasedActions.SaveServiceList(_param1 & "\" & Date.Now.Ticks.ToString & "_" & _param2)
'            End Select
'        Catch

'        End Try


'    End Sub

'    Private Function getOperator(ByVal _op As STATE_OPERATOR) As String
'        Select Case _op
'            Case STATE_OPERATOR.different_from
'                Return "!="
'            Case STATE_OPERATOR.equal
'                Return "="
'            Case STATE_OPERATOR.greater_or_equal_than
'                Return ">="
'            Case STATE_OPERATOR.greater_than
'                Return ">"
'            Case STATE_OPERATOR.less_or_equal_than
'                Return "<="
'            Case Else
'                Return "<"
'        End Select
'    End Function

'    Private Function getTimeFromS(ByVal s As String) As Integer
'        Try
'            Dim s1 As String = s.Substring(0, 2)
'            Dim s2 As String = s.Substring(3, 2)
'            Dim s3 As String = s.Substring(6, 2)
'            Return 10000 * CInt(Val(s1)) + 100 * CInt(Val(s2)) + CInt(Val(s3))
'        Catch ex As Exception
'            Return 0
'        End Try
'    End Function

'End Class
