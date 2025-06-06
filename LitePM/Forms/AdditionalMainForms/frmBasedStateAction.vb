﻿'
'' Lite Process Monitor



'Option Strict On

'Imports System.Runtime.InteropServices

'Public Class frmBasedStateAction

'    Public Const BASED_STATE_ACTION As String = "statebasedaction.xml"

'    Private Const MSG_FIRST_TIME_SBA As String = "This is the first time you use SBA (State Based Actions) feature." & vbNewLine & "Remember this feature is NOT SAFE TO USE. Be careful when using it." & vbNewLine & "Please read help before starting with SBA, and to do not forget to systematically use the simulation console." & vbNewLine & "This console is available with F2 shortcut."
'    Private _desc1() As String
'    Private _desc2() As String
'    Private _modify As Boolean = False
'    Private _selectedAction As cBasedStateActionState
'    Private _selectedItem As ListViewItem

'    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode, ExactSpelling:=True)> _
'    Private Shared Function SetWindowTheme(ByVal hWnd As IntPtr, ByVal appName As String, ByVal partList As String) As Integer
'    End Function

'    Private Sub frmBasedStateAction_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

'        If MsgBox("Are you sure you want to apply and save these actions ?", MsgBoxStyle.Information _
'                    Or MsgBoxStyle.YesNo, "State based actions") = MsgBoxResult.Yes Then

'            ' Save to XML
'            Call writeXML()
'            _frmMain.emStateBasedActions.ShowConsole = False
'            _frmMain.emStateBasedActions.SimulationMode = False
'        Else
'            e.Cancel = True
'        End If

'    End Sub

'    Private Sub frmBasedStateAction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        _frmMain.emStateBasedActions.ClearConsole()
'        _frmMain.emStateBasedActions.SimulationMode = True

'        SetWindowTheme(lv.Handle)

'        If Program.Preferences.firstTimeSBA Then
'            MsgBox(MSG_FIRST_TIME_SBA, MsgBoxStyle.Information, "State based actions")
'            Program.Preferences.firstTimeSBA = False
'            Program.Preferences.Save()
'        End If

'        Me.cbAction.Items.Clear()
'        For Each i As String In _frmMain.emStateBasedActions.ActionsAvailable
'            If i IsNot Nothing Then
'                Me.cbAction.Items.Add(i)
'            End If
'        Next

'        ' Read collection and add items
'        For Each _it As cBasedStateActionState In _frmMain.emStateBasedActions.StateBasedActionCollection
'            ' Add hotkey
'            Dim it As New ListViewItem(_it.ProcessText)
'            it.Tag = _it
'            If _it.Enabled = False Then
'                it.ForeColor = Color.Gray
'            End If
'            it.SubItems.Add(_it.StateText)
'            it.SubItems.Add(_it.ActionText)
'            it.ImageKey = "default"
'            Me.lv.Items.Add(it)
'        Next

'        ' Fill comboboxes
'        cbAction.Items.Clear()
'        For Each s As String In _frmMain.emStateBasedActions.ActionsAvailable
'            cbAction.Items.Add(s)
'        Next
'        cbCounter.Items.Clear()
'        For Each s As String In _frmMain.emStateBasedActions.CounterAvailables
'            cbCounter.Items.Add(s)
'        Next
'        _desc1 = _frmMain.emStateBasedActions.Param1Description
'        _desc2 = _frmMain.emStateBasedActions.Param2Description

'    End Sub

'    Private Sub ShowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowToolStripMenuItem.Click
'        gp.Visible = True
'    End Sub

'    Private Sub cmdKO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdKO.Click
'        gp.Visible = False
'        _modify = False
'    End Sub

'    Private Sub CloseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseToolStripMenuItem.Click
'        For Each it As ListViewItem In Me.lv.SelectedItems
'            If it.Selected Then
'                ' Remove or ?
'                Dim sKey As String = CType(it.Tag, cBasedStateActionState).Key
'                If _frmMain.emStateBasedActions.RemoveStateBasedAction(sKey) Then
'                    it.Remove()
'                End If
'            End If
'        Next
'    End Sub

'    Private Sub EnableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnableToolStripMenuItem.Click
'        For Each it As ListViewItem In Me.lv.SelectedItems
'            If it.Selected Then
'                CType(it.Tag, cBasedStateActionState).Enabled = True
'                it.ForeColor = Color.Black
'            End If
'        Next
'    End Sub

'    Private Sub DisableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisableToolStripMenuItem.Click
'        For Each it As ListViewItem In Me.lv.SelectedItems
'            If it.Selected Then
'                CType(it.Tag, cBasedStateActionState).Enabled = False
'                it.ForeColor = Color.Gray
'            End If
'        Next
'    End Sub

'    ' Read from XML file
'    Public Sub readStateBasedActionFromXML()
'        Dim XmlDoc As XmlDocument = New XmlDocument
'        Dim element As XmlNodeList
'        Dim noeud, noeudEnf As XmlNode

'        '<statebasedactions>
'        '	<sbaction>
'        '		<enabled>true</enabled>
'        '		<checkprocname>true</checkprocname>
'        '		<checkprocid>true</checkprocid>
'        '		<checkprocpath>true</checkprocpath>
'        '		<procname>LitePM.exe</procname>
'        '		<procid>654</procid>
'        '		<procpath>c:\*</procpath>
'        '		<counter>CpuUsage</counter>
'        '		<operator>4</operator>
'        '		<threshold>12.5</threshold>
'        '		<action>Kill process</action>
'        '		<param1></param1>
'        '		<param2></param2>
'        '       <thecounter>0</thecounter>
'        '	</sbaction>
'        '</statebasedactions>

'        Try
'            Call XmlDoc.Load(My.Application.Info.DirectoryPath & "\" & BASED_STATE_ACTION)
'            element = XmlDoc.DocumentElement.GetElementsByTagName("sbaction")

'            For Each noeud In element

'                Dim _checkProcName As Boolean
'                Dim _checkProcNameS As String = ""
'                Dim _checkProcID As Boolean
'                Dim _checkProcIDS As String = ""
'                Dim _checkProcPath As Boolean
'                Dim _checkProcPathS As String = ""
'                Dim _stateCounter As String = ""
'                Dim _stateOperator As cBasedStateActionState.STATE_OPERATOR = _
'                    cBasedStateActionState.STATE_OPERATOR.equal
'                Dim _threshold As String = ""
'                Dim _action As String = ""
'                Dim _param1 As String = ""
'                Dim _param2 As String = ""
'                Dim _enabled As Boolean
'                Dim _notify As Boolean
'                Dim _counter As String = ""

'                For Each noeudEnf In noeud.ChildNodes

'                    If noeudEnf.LocalName = "enabled" Then
'                        _enabled = CBool(noeudEnf.InnerText)
'                    ElseIf noeudEnf.LocalName = "checkprocname" Then
'                        _checkProcName = CBool(noeudEnf.InnerText)
'                    ElseIf noeudEnf.LocalName = "checkprocid" Then
'                        _checkProcID = CBool(noeudEnf.InnerText)
'                    ElseIf noeudEnf.LocalName = "checkprocpath" Then
'                        _checkProcPath = CBool(noeudEnf.InnerText)
'                    ElseIf noeudEnf.LocalName = "procname" Then
'                        _checkProcNameS = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "procid" Then
'                        _checkProcIDS = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "procpath" Then
'                        _checkProcPathS = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "counter" Then
'                        _stateCounter = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "operator" Then
'                        _stateOperator = CType(CInt(Val(noeudEnf.InnerText)), cBasedStateActionState.STATE_OPERATOR)
'                    ElseIf noeudEnf.LocalName = "action" Then
'                        _action = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "param1" Then
'                        _param1 = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "param2" Then
'                        _param2 = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "threshold" Then
'                        _threshold = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "thecounter" Then
'                        _counter = noeudEnf.InnerText
'                    ElseIf noeudEnf.LocalName = "notify" Then
'                        _notify = CBool(noeudEnf.InnerText)
'                    End If
'                Next

'                Dim ht As New cBasedStateActionState(_checkProcName, _checkProcID, _
'                                                     _checkProcPath, _stateCounter, _
'                                                     _stateOperator, _threshold, _
'                                                     _action, _param1, _param2, _
'                                                     _checkProcNameS, _checkProcIDS, _
'                                                     _checkProcPathS, _counter, _notify)
'                ht.Enabled = _enabled

'                _frmMain.emStateBasedActions.AddStateBasedAction(ht)
'            Next
'        Catch ex As Exception
'            Trace.WriteLine("XML loading failed : " & ex.Message)
'        End Try
'    End Sub

'    ' Write to XML file
'    Private Sub writeXML()

'        '<statebasedactions>
'        '	<sbaction>
'        '		<enabled>true</enabled>
'        '		<checkprocname>true</checkprocname>
'        '		<checkprocid>true</checkprocid>
'        '		<checkprocpath>true</checkprocpath>
'        '		<procname>LitePM.exe</procname>
'        '		<procid>654</procid>
'        '		<procpath>c:\*</procpath>
'        '		<counter>CpuUsage</counter>
'        '		<operator>4</operator>
'        '		<threshold>12.5</threshold>
'        '		<action>Kill process</action>
'        '		<param1></param1>
'        '		<param2></param2>
'        '       <thecounter>0</thecounter>
'        '	</sbaction>
'        '</statebasedactions>

'        Dim XmlDoc As XmlDocument = New XmlDocument()
'        XmlDoc.LoadXml("<statebasedactions></statebasedactions>")

'        For Each cs As cBasedStateActionState In _frmMain.emStateBasedActions.StateBasedActionCollection

'            Dim elemStateBasedAction As XmlElement = XmlDoc.CreateElement("sbaction")

'            Dim elemEnabled As XmlElement
'            elemEnabled = XmlDoc.CreateElement("enabled")
'            elemEnabled.InnerText = cs.Enabled.ToString
'            elemStateBasedAction.AppendChild(elemEnabled)
'            Dim elemCheckProcName As XmlElement
'            elemCheckProcName = XmlDoc.CreateElement("checkprocname")
'            elemCheckProcName.InnerText = cs.CheckProcName.ToString
'            elemStateBasedAction.AppendChild(elemCheckProcName)
'            Dim elemCheckProcId As XmlElement
'            elemCheckProcId = XmlDoc.CreateElement("checkprocid")
'            elemCheckProcId.InnerText = cs.CheckProcID.ToString
'            elemStateBasedAction.AppendChild(elemCheckProcId)
'            Dim elemCheckProcPath As XmlElement
'            elemCheckProcPath = XmlDoc.CreateElement("checkprocpath")
'            elemCheckProcPath.InnerText = cs.CheckProcPath.ToString
'            elemStateBasedAction.AppendChild(elemCheckProcPath)
'            Dim elemProcName As XmlElement
'            elemProcName = XmlDoc.CreateElement("procname")
'            elemProcName.InnerText = cs.CheckProcNameS.ToString
'            elemStateBasedAction.AppendChild(elemProcName)
'            Dim elemProcID As XmlElement
'            elemProcID = XmlDoc.CreateElement("procid")
'            elemProcID.InnerText = cs.CheckProcIDS.ToString
'            elemStateBasedAction.AppendChild(elemProcID)
'            Dim elemProcPath As XmlElement
'            elemProcPath = XmlDoc.CreateElement("procpath")
'            elemProcPath.InnerText = cs.CheckProcPathS.ToString
'            elemStateBasedAction.AppendChild(elemProcPath)
'            Dim elemCounter As XmlElement
'            elemCounter = XmlDoc.CreateElement("counter")
'            elemCounter.InnerText = cs.StateCounter.ToString
'            elemStateBasedAction.AppendChild(elemCounter)
'            Dim elemOperator As XmlElement
'            elemOperator = XmlDoc.CreateElement("operator")
'            elemOperator.InnerText = CStr(cs.StateOperator)
'            elemStateBasedAction.AppendChild(elemOperator)
'            Dim elemThreshold As XmlElement
'            elemThreshold = XmlDoc.CreateElement("threshold")
'            elemThreshold.InnerText = cs.Threshold.ToString
'            elemStateBasedAction.AppendChild(elemThreshold)
'            Dim elemAction As XmlElement
'            elemAction = XmlDoc.CreateElement("action")
'            elemAction.InnerText = cs.Action.ToString
'            elemStateBasedAction.AppendChild(elemAction)
'            Dim elemParam1 As XmlElement
'            elemParam1 = XmlDoc.CreateElement("param1")
'            elemParam1.InnerText = cs.Param1.ToString
'            elemStateBasedAction.AppendChild(elemParam1)
'            Dim elemParam2 As XmlElement
'            elemParam2 = XmlDoc.CreateElement("param2")
'            elemParam2.InnerText = cs.Param2.ToString
'            elemStateBasedAction.AppendChild(elemParam2)
'            Dim elemTheCounter As XmlElement
'            elemTheCounter = XmlDoc.CreateElement("thecounter")
'            elemTheCounter.InnerText = cs.InitialCounter.ToString
'            elemStateBasedAction.AppendChild(elemTheCounter)

'            XmlDoc.DocumentElement.AppendChild(elemStateBasedAction)

'        Next

'        XmlDoc.Save(My.Application.Info.DirectoryPath & "\" & BASED_STATE_ACTION)
'    End Sub

'    ' Check if we can add the action
'    Private Sub checkAddState()
'        Me.cmdAdd.Enabled = False

'        If (Me.chkCheckProcessID.Checked Or Me.chkCheckProcessName.Checked Or _
'            Me.chkCheckProcessPath.Checked) = False Then Exit Sub
'        If (cbAction.Text = "" Or cbCounter.Text = "" Or cbOperator.Text = "") Then Exit Sub
'        If txtThreshold.Text = "" Then Exit Sub

'        Me.cmdAdd.Enabled = True
'    End Sub

'    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
'        gp.Visible = False

'        If _modify = False Then
'            Dim _operator As cBasedStateActionState.STATE_OPERATOR
'            Select Case cbOperator.Text
'                Case "<"
'                    _operator = cBasedStateActionState.STATE_OPERATOR.less_than
'                Case "<="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.less_or_equal_than
'                Case "="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.equal
'                Case ">"
'                    _operator = cBasedStateActionState.STATE_OPERATOR.greater_than
'                Case ">="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.greater_or_equal_than
'                Case "!="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.different_from
'            End Select
'            Dim _it As New cBasedStateActionState(Me.chkCheckProcessName.Checked, _
'                                                  Me.chkCheckProcessID.Checked, _
'                                                  Me.chkCheckProcessPath.Checked, _
'                                                  cbCounter.Text, _operator, txtThreshold.Text, _
'                                                  cbAction.Text, txtParam1Val.Text, _
'                                                  txtParam2Val.Text, txtProcessName.Text, _
'                                                  txtProcessID.Text, txtProcessPath.Text, _
'                                                  Me.updownCounter.Value.ToString, _
'                                                  Me.chkNotify.Checked)
'            If _frmMain.emStateBasedActions.AddStateBasedAction(_it) Then
'                ' Add hotkey
'                Dim it As New ListViewItem(_it.ProcessText)
'                it.Tag = _it
'                it.SubItems.Add(_it.StateText)
'                it.SubItems.Add(_it.ActionText)
'                it.ImageKey = "default"
'                Me.lv.Items.Add(it)
'            End If

'        Else
'            _modify = False

'            ' Delete old action
'            _frmMain.emStateBasedActions.RemoveStateBasedAction(_selectedAction.Key)

'            ' Add new one
'            Dim _operator As cBasedStateActionState.STATE_OPERATOR
'            Select Case cbOperator.Text
'                Case "<"
'                    _operator = cBasedStateActionState.STATE_OPERATOR.less_than
'                Case "<="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.less_or_equal_than
'                Case "="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.equal
'                Case ">"
'                    _operator = cBasedStateActionState.STATE_OPERATOR.greater_than
'                Case ">="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.greater_or_equal_than
'                Case "!="
'                    _operator = cBasedStateActionState.STATE_OPERATOR.different_from
'            End Select
'            Dim _it As New cBasedStateActionState(Me.chkCheckProcessName.Checked, _
'                                                  Me.chkCheckProcessID.Checked, _
'                                                  Me.chkCheckProcessPath.Checked, _
'                                                  cbCounter.Text, _operator, txtThreshold.Text, _
'                                                  cbAction.Text, txtParam1Val.Text, _
'                                                  txtParam2Val.Text, txtProcessName.Text, _
'                                                  txtProcessID.Text, txtProcessPath.Text, _
'                                                  Me.updownCounter.Value.ToString, _
'                                                  Me.chkNotify.Checked)
'            If _frmMain.emStateBasedActions.AddStateBasedAction(_it) Then
'                ' Add hotkey
'                _selectedItem.Tag = _it
'                _selectedItem.SubItems(1).Text = _it.StateText
'                _selectedItem.SubItems(2).Text = _it.ActionText
'                _selectedItem.ImageKey = "default"
'            End If

'        End If
'    End Sub

'    Private Sub chkCheckProcessName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckProcessName.CheckedChanged
'        Call checkAddState()
'    End Sub

'    Private Sub chkCheckProcessID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckProcessID.CheckedChanged
'        Call checkAddState()
'    End Sub

'    Private Sub chkCheckProcessPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCheckProcessPath.CheckedChanged
'        Call checkAddState()
'    End Sub

'    Private Sub cbOperator_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOperator.SelectedIndexChanged
'        Call checkAddState()
'    End Sub

'    Private Sub txtThreshold_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtThreshold.TextChanged
'        Call checkAddState()
'    End Sub

'    Private Sub cbCounter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCounter.SelectedIndexChanged
'        Call checkAddState()
'        Dim i As Integer = cbCounter.SelectedIndex
'        If i >= 0 Then
'            Me.lblThresholdDesc.Text = _frmMain.emStateBasedActions.ThresholdDescription(i)
'        Else
'            Me.lblThresholdDesc.Text = ""
'        End If
'    End Sub

'    Private Sub cbAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAction.SelectedIndexChanged
'        Call checkAddState()
'        Dim i As Integer = cbAction.SelectedIndex
'        If i >= 0 Then
'            Me.txtParam1Desc.Text = _frmMain.emStateBasedActions.Param1Description(i)
'            Me.txtParam2Desc.Text = _frmMain.emStateBasedActions.Param2Description(i)
'        Else
'            Me.txtParam1Desc.Text = ""
'            Me.txtParam2Desc.Text = ""
'        End If
'        Me.txtParam1Val.Enabled = Not (Me.txtParam1Desc.Text = "None")
'        Me.txtParam2Val.Enabled = Not (Me.txtParam2Desc.Text = "None")
'    End Sub

'    Private Sub txtParam1Val_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtParam1Val.TextChanged
'        Call checkAddState()
'    End Sub

'    Private Sub txtParam2Val_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtParam2Val.TextChanged
'        Call checkAddState()
'    End Sub

'    Private Sub lv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv.DoubleClick
'        If lv.SelectedItems.Count > 0 Then
'            _modify = True
'            _selectedItem = lv.SelectedItems(0)
'            _selectedAction = CType(_selectedItem.Tag, cBasedStateActionState)
'            Me.gp.Visible = True
'        End If
'    End Sub

'    Private Sub gp_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gp.VisibleChanged
'        lv.Enabled = Not (gp.Visible)
'        If gp.Visible Then
'            If _modify Then
'                Me.cmdAdd.Text = "Modify"
'                Me.chkCheckProcessName.Checked = CBool(_selectedAction.CheckProcName)
'                Me.chkCheckProcessID.Checked = CBool(_selectedAction.CheckProcID)
'                Me.chkCheckProcessPath.Checked = CBool(_selectedAction.CheckProcPath)
'                cbCounter.Text = _selectedAction.StateCounter
'                Select Case _selectedAction.StateOperator
'                    Case cBasedStateActionState.STATE_OPERATOR.less_than
'                        cbOperator.Text = "<"
'                    Case cBasedStateActionState.STATE_OPERATOR.less_or_equal_than
'                        cbOperator.Text = "<="
'                    Case cBasedStateActionState.STATE_OPERATOR.equal
'                        cbOperator.Text = "="
'                    Case cBasedStateActionState.STATE_OPERATOR.greater_than
'                        cbOperator.Text = ">"
'                    Case cBasedStateActionState.STATE_OPERATOR.greater_or_equal_than
'                        cbOperator.Text = ">="
'                    Case cBasedStateActionState.STATE_OPERATOR.different_from
'                        cbOperator.Text = "!="
'                End Select
'                txtThreshold.Text = _selectedAction.Threshold.ToString
'                cbAction.Text = _selectedAction.Action
'                txtParam1Val.Text = _selectedAction.Param1
'                txtParam2Val.Text = _selectedAction.Param2
'                txtProcessName.Text = _selectedAction.CheckProcNameS
'                txtProcessID.Text = _selectedAction.CheckProcIDS
'                txtProcessPath.Text = _selectedAction.CheckProcPathS
'                txtParam1Desc.Text = ""
'                txtParam2Desc.Text = ""
'                updownCounter.Value = _selectedAction.InitialCounter
'                chkNotify.Checked = _selectedAction.Notify
'            Else
'                Me.cmdAdd.Text = "Add"
'                Me.chkCheckProcessName.Checked = False
'                Me.chkCheckProcessID.Checked = False
'                Me.chkCheckProcessPath.Checked = False
'                cbCounter.SelectedIndex = -1
'                chkNotify.Checked = True
'                cbOperator.SelectedIndex = -1
'                txtThreshold.Text = ""
'                cbAction.SelectedIndex = -1
'                txtParam1Val.Text = ""
'                txtParam2Val.Text = ""
'                txtProcessName.Text = ""
'                txtProcessID.Text = ""
'                txtProcessPath.Text = ""
'                txtParam1Desc.Text = ""
'                txtParam2Desc.Text = ""
'                updownCounter.Value = -1
'            End If
'            Call cbAction_SelectedIndexChanged(Nothing, Nothing)
'            Call cbCounter_SelectedIndexChanged(Nothing, Nothing)
'        End If
'    End Sub

'    Private Sub cmdBrowseProcessName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseProcessName.Click
'        Dim frm As New frmChooseProcess
'        frm.ShowDialog()
'        If frm.SelectedProcess IsNot Nothing Then
'            Me.txtProcessName.Text = frm.SelectedProcess.Name
'        End If
'    End Sub

'    Private Sub cmdBrowseProcessId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseProcessId.Click
'        Dim frm As New frmChooseProcess
'        frm.ShowDialog()
'        If frm.SelectedProcess IsNot Nothing Then
'            Me.txtProcessID.Text = frm.SelectedProcess.Pid.ToString
'        End If
'    End Sub

'    Private Sub cmdBrowseProcessPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseProcessPath.Click
'        Dim frm As New frmChooseProcess
'        frm.ShowDialog()
'        If frm.SelectedProcess IsNot Nothing Then
'            Me.txtProcessPath.Text = frm.SelectedProcess.Path
'        End If
'    End Sub

'    Private Sub SimulationConsoleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SimulationConsoleToolStripMenuItem.Click
'        SimulationConsoleToolStripMenuItem.Checked = Not (SimulationConsoleToolStripMenuItem.Checked)
'        _frmMain.emStateBasedActions.ShowConsole = SimulationConsoleToolStripMenuItem.Checked
'    End Sub

'End Class