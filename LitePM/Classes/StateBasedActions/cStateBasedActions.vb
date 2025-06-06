﻿'
'' Lite Process Monitor


'Option Strict On

'Imports System.Runtime.InteropServices
'Imports System.Windows.Forms

'Public Class cStateBasedActions

'    
'    ' Private attributes
'    
'    Private _col As New Collection
'    Private _simulationMode As Boolean = False
'    Private frmConsole As New frmSBASimulationConsole


'    
'    ' Public events & associated functions
'    
'    Public Shared Event ExitRequested()
'    Protected Friend Shared Sub ExitLitePM()
'        RaiseEvent ExitRequested()
'    End Sub
'    Public Shared Event LogRequested(ByRef process As cLocalProcess)
'    Protected Friend Shared Sub StartLog(ByRef process As cLocalProcess)
'        RaiseEvent LogRequested(process)
'    End Sub
'    Public Shared Event SaveServiceListRequested(ByVal path As String)
'    Protected Friend Shared Sub SaveServiceList(ByVal path As String)
'        RaiseEvent SaveServiceListRequested(path)
'    End Sub
'    Public Shared Event SaveProcessListRequested(ByVal path As String)
'    Protected Friend Shared Sub SaveProcessList(ByVal path As String)
'        RaiseEvent SaveProcessListRequested(path)
'    End Sub
'    Public Shared Event NotifyAction(ByRef action As cBasedStateActionState, ByRef process As cLocalProcess)
'    Protected Friend Shared Sub Notify(ByRef action As cBasedStateActionState, ByRef process As cLocalProcess)
'        RaiseEvent NotifyAction(action, process)
'    End Sub


'    
'    ' Public properties
'    
'    Public ReadOnly Property StateBasedActionCollection() As Collection
'        Get
'            Return _col
'        End Get
'    End Property
'    Public ReadOnly Property ActionsAvailable() As String()
'        Get
'            Dim s(24) As String
'            s(0) = "Kill process"
'            s(1) = "Pause process"
'            s(2) = "Resume process"
'            s(3) = "Change priority"
'            s(4) = "Reduce priority"
'            s(5) = "Increase priority"
'            s(6) = "Activate process log"
'            s(7) = "Change affinity"
'            s(8) = "Launch a command"
'            s(9) = "Restart computer"
'            s(10) = "Shutdown computer"
'            s(11) = "Poweroff computer"
'            s(12) = "Sleep computer"
'            s(13) = "Hibernate computer"
'            s(14) = "Logoff computer"
'            s(15) = "Lock computer"
'            s(16) = "Exit LitePM"
'            s(17) = "Show process task windows"
'            s(18) = "Hide process task windows"
'            s(19) = "Maximize process task windows"
'            s(20) = "Minimize process task windows"
'            s(21) = "Beep"
'            s(22) = "Save process list"
'            s(23) = "Save service list"
'            s(24) = "Do nothing"
'            Return s
'        End Get
'    End Property
'    Public ReadOnly Property ThresholdDescription() As String()
'        Get
'            Dim s(30) As String
'            s(0) = "Integer"
'            s(1) = "String"
'            s(2) = "Integer"
'            s(3) = "String"
'            s(4) = "Decimal (%)"
'            s(5) = "Decimal (%)"
'            s(6) = "Decimal (sec)"
'            s(7) = "Decimal (sec)"
'            s(8) = "Decimal (sec)"
'            s(9) = "Date (hh:mm:ss)"
'            s(10) = "Date (hh:mm:ss)"
'            s(11) = "Integer"
'            s(12) = "Integer"
'            s(13) = "Integer (mask : Sum(processor_number^2), processor_number start at 0)"
'            s(14) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(15) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(16) = "Integer"
'            s(17) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(18) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(19) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(20) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(21) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(22) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(23) = "Integer"
'            s(24) = "Integer"
'            s(25) = "Integer"
'            s(26) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(27) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(28) = "Decimal (+ space char + unit : Bytes, KB, MB, GB)"
'            s(29) = "String enum (I-BN-N-AN-H-RT)"
'            s(30) = "String"
'            Return s
'        End Get
'    End Property
'    Public ReadOnly Property Param1Description() As String()
'        Get
'            Dim s(24) As String
'            s(0) = "None"
'            s(1) = "None"
'            s(2) = "None"
'            s(3) = "New priority (I-BN-N-AN-H-RT)"
'            s(4) = "None"
'            s(5) = "None"
'            s(6) = "None"
'            s(7) = "New affinity (mask : Sum(processor_number^2), processor_number start at 0)"
'            s(8) = "Command to launch"
'            s(9) = "None"
'            s(10) = "None"
'            s(11) = "None"
'            s(12) = "None"
'            s(13) = "None"
'            s(14) = "None"
'            s(15) = "None"
'            s(16) = "None"
'            s(17) = "None"
'            s(18) = "None"
'            s(19) = "None"
'            s(20) = "None"
'            s(21) = "None"
'            s(22) = "Report path (path without file name)"
'            s(23) = "Report path (path without file name)"
'            s(24) = "You can add a description here (no effect on rule)"
'            Return s
'        End Get
'    End Property
'    Public ReadOnly Property Param2Description() As String()
'        Get
'            Dim s(24) As String
'            s(0) = "None"
'            s(1) = "None"
'            s(2) = "None"
'            s(3) = "None"
'            s(4) = "None"
'            s(5) = "None"
'            s(6) = "None"
'            s(7) = "None"
'            s(8) = "None"
'            s(9) = "None"
'            s(10) = "None"
'            s(11) = "None"
'            s(12) = "None"
'            s(13) = "None"
'            s(14) = "None"
'            s(15) = "None"
'            s(16) = "None"
'            s(17) = "None"
'            s(18) = "None"
'            s(19) = "None"
'            s(20) = "None"
'            s(21) = "None"
'            s(22) = "Report file name"
'            s(23) = "Report file name"
'            s(24) = "You can add a description here (no effect on rule)"
'            Return s
'        End Get
'    End Property
'    Public ReadOnly Property CounterAvailables() As String()
'        Get
'            Dim s(30) As String
'            s(0) = "PID"
'            s(1) = "UserName"
'            s(2) = "ParentPID"
'            s(3) = "ParentName"
'            s(4) = "CpuUsage"
'            s(5) = "AverageCpuUsage"
'            s(6) = "KernelCpuTime"
'            s(7) = "UserCpuTime"
'            s(8) = "TotalCpuTime"
'            s(9) = "StartTime"
'            s(10) = "RunTime"
'            s(11) = "GdiObjects"
'            s(12) = "UserObjects"
'            s(13) = "AffinityMask"
'            s(14) = "WorkingSet"
'            s(15) = "PeakWorkingSet"
'            s(16) = "PageFaultCount"
'            s(17) = "PagefileUsage"
'            s(18) = "PeakPagefileUsage"
'            s(19) = "QuotaPeakPagedPoolUsage"
'            s(20) = "QuotaPagedPoolUsage"
'            s(21) = "QuotaPeakNonPagedPoolUsage"
'            s(22) = "QuotaNonPagedPoolUsage"
'            s(23) = "ReadOperationCount"
'            s(24) = "WriteOperationCount"
'            s(25) = "OtherOperationCount"
'            s(26) = "ReadTransferCount"
'            s(27) = "WriteTransferCount"
'            s(28) = "OtherTransferCount"
'            s(29) = "Priority"
'            s(30) = "Path"
'            Return s
'        End Get
'    End Property
'    Public Property SimulationMode() As Boolean
'        Get
'            Return _simulationMode
'        End Get
'        Set(ByVal value As Boolean)
'            _simulationMode = value
'        End Set
'    End Property
'    Public WriteOnly Property ShowConsole() As Boolean
'        Set(ByVal value As Boolean)
'            If value Then
'                frmConsole.Show()
'            Else
'                frmConsole.Hide()
'            End If
'        End Set
'    End Property


'    
'    ' Public functions
'    

'    ' Clear console
'    Public Sub ClearConsole()
'        Me.frmConsole.lv.Items.Clear()
'    End Sub

'    ' Add a key to collection
'    Public Function AddStateBasedAction(ByVal action As cBasedStateActionState) As Boolean
'        Dim sKey As String = action.Key
'        Try
'            _col.Add(Key:=sKey, Item:=action)
'            Return True
'        Catch ex As Exception
'            Return False
'        End Try
'    End Function

'    ' Remove key from collection
'    Public Function RemoveStateBasedAction(ByVal action As cBasedStateActionState) As Boolean
'        Dim sKey As String = action.Key
'        Try
'            _col.Remove(sKey)
'            Return True
'        Catch ex As Exception
'            Return False
'        End Try
'    End Function
'    Public Function RemoveStateBasedAction(ByVal action As String) As Boolean
'        Try
'            _col.Remove(action)
'            Return True
'        Catch ex As Exception
'            Return False
'        End Try
'    End Function

'    ' Process actions
'    Public Sub ProcessActions(ByRef _dico As Dictionary(Of String, cLocalProcess).ValueCollection)

'        For Each action As cBasedStateActionState In _col

'            If action.Enabled Then

'                ' Check if there is a process concerned
'                Dim b As Boolean = False
'                For Each _p As cLocalProcess In _dico

'                    If (action.CheckProcID And action.CheckProcIDS = _p.Pid.ToString) OrElse _
'                        (action.CheckProcName And action.CheckProcNameS.ToLower = _p.Name.ToLower) Then
'                        b = True
'                    ElseIf action.CheckProcPath Then
'                        ' Test process path
'                        Dim _path As String = _p.Path
'                        If action.CheckProcPathS.Length = 0 Then
'                            b = True
'                        Else
'                            If action.CheckProcPathS.Substring(action.CheckProcPathS.Length - 1, 1) = "*" Then
'                                b = (InStr(_path.ToLower, action.CheckProcPathS.ToLower.Replace("*", "")) > 0)
'                            Else
'                                b = (action.CheckProcPathS.ToLower = _path.ToLower)
'                            End If
'                        End If
'                    End If

'                    If b Then
'                        ' Ok we found a process
'                        ' Check state
'                        If isStateOk(action, _p) Then
'                            If _simulationMode = False Then
'                                If action.Counter > 0 Then
'                                    action.Counter -= 1
'                                End If
'                                action.RaiseAction(_p)
'                            Else
'                                ' Just display an information
'                                Dim _it As New ListViewItem(Date.Now.ToLongTimeString)
'                                _it.SubItems.Add(_p.Name & " (" & _p.Pid.ToString & ")")
'                                _it.SubItems.Add(action.ActionText)
'                                _it.SubItems.Add(action.RuleText)
'                                Me.frmConsole.lv.Items.Add(_it)
'                            End If
'                        End If
'                        b = False
'                    End If
'                Next

'            End If

'        Next

'    End Sub

'    ' Check if process state is reached
'    Private Function isStateOk(ByRef action As cBasedStateActionState, ByRef _p As cLocalProcess) As Boolean

'        Dim _currentValue As cBasedStateActionState.StateThreshold = _p.GetInformationAsStateThreshold(action.StateCounter)

'        If _simulationMode OrElse action.Counter > 0 OrElse action.Counter = -1 Then

'            Select Case action.StateOperator
'                Case cBasedStateActionState.STATE_OPERATOR.different_from
'                    Return (_currentValue <> action.Threshold)
'                Case cBasedStateActionState.STATE_OPERATOR.equal
'                    Return (_currentValue = action.Threshold)
'                Case cBasedStateActionState.STATE_OPERATOR.greater_or_equal_than
'                    Return (_currentValue >= action.Threshold)
'                Case cBasedStateActionState.STATE_OPERATOR.greater_than
'                    Return (_currentValue > action.Threshold)
'                Case cBasedStateActionState.STATE_OPERATOR.less_or_equal_than
'                    Return (_currentValue <= action.Threshold)
'                Case cBasedStateActionState.STATE_OPERATOR.less_than
'                    Return (_currentValue < action.Threshold)
'                Case Else
'                    Return False
'            End Select

'        Else
'            Return False
'        End If

'    End Function

'End Class
