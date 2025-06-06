
' Lite Process Monitor


Option Strict On
Imports Common.Misc

Public Class frmChooseColumns

    Private theListview As customLV

    Public Property ConcernedListView() As customLV
        Get
            Return theListview
        End Get
        Set(ByVal value As customLV)
            theListview = value
        End Set
    End Property


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK

        Try
            theListview.generalLvSemaphore.WaitOne()
            theListview.ReorganizeColumns = True
            theListview.BeginUpdate()

            ' Remove all columns
            For x As Integer = theListview.Columns.Count - 1 To 1 Step -1
                theListview.Columns.Remove(theListview.Columns(x))
            Next

            ' Add new columns
            For Each it As ListViewItem In Me.lv.CheckedItems
                Dim width As Integer = CInt(Val(it.SubItems(1).Text))
                If width <= 0 Then
                    width = 90        ' Default size
                End If
                theListview.Columns.Add(it.Text, width).TextAlign = CType([Enum].Parse(GetType(HorizontalAlignment), it.SubItems(2).Text), HorizontalAlignment)
            Next

            ' Add items which are selected
            For Each it As ListViewItem In theListview.Items

                ' Can not use .Clear because it also remove the Item
                For i As Integer = it.SubItems.Count - 1 To 1 Step -1
                    it.SubItems.RemoveAt(i)
                Next

                Dim subit() As String
                ReDim subit(Me.lv.CheckedItems.Count)

                For z As Integer = 0 To UBound(subit) - 1
                    subit(z) = ""
                Next

                it.SubItems.AddRange(subit)
            Next

            ' Refresh all items & subitems
            ConcernedListView.CreateSubItemsBuffer()
            ConcernedListView.ForceRefreshingOfAllItems()

            theListview.ReorganizeColumns = False
            theListview.EndUpdate()
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            theListview.generalLvSemaphore.Release()
        End Try
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = True
        Next
    End Sub

    Private Sub btnUnSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnSelAll.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = False
        Next
    End Sub

    Private Sub frmChooseProcessColumns_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call CloseWithEchapKey(Me)

        Native.Functions.Misc.SetTheme(Me.lv.Handle)

        Dim ss() As String
        ReDim ss(-1)

        ' This is some kind of shit.
        ' But as I can't write a MustOverride Shared Function...
        If TypeOf (ConcernedListView) Is handleList Then
            ss = handleInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is memRegionList Then
            ss = memRegionInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is moduleList Then
            ss = moduleInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is mainNetworkConnectionsList Then
            ss = networkInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is mainProcessList Then
            ss = processInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is mainServiceList Then
            ss = serviceInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is mainTaskList Then
            ss = taskInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is threadList Then
            ss = threadInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is windowList Then
            ss = windowInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is privilegeList Then
            ss = privilegeInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is envVariableList Then
            ss = envVariableInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is jobLimitList Then
            ss = jobLimitInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is mainJobList Then
            ss = jobInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is processesInJobList Then
            ss = processInfos.GetAvailableProperties
        ElseIf TypeOf (ConcernedListView) Is heapList Then
            ss = heapInfos.GetAvailableProperties
        End If

        ReDim Preserve ss(ss.Length + 1)
        ss(ss.Length - 2) = "ObjectCreationDate"
        ss(ss.Length - 1) = "PendingTaskCount"

        ' Now add displayed columns names to list
        ' Add this columns by DisplayIndex order

        ' Start from 1 because item 0 is fixed and not added in our list
        For x As Integer = 1 To ConcernedListView.Columns.Count - 1
            Dim col As ColumnHeader = Common.Misc.GetColumnHeaderByDisplayIndex(ConcernedListView, x)
            Dim sss As String = col.Text.Replace("< ", "").Replace("> ", "")
            Dim it As New ListViewItem(sss)
            it.Checked = True
            it.Name = sss
            it.SubItems.Add(col.Width.ToString)
            it.SubItems.Add(col.TextAlign.ToString)
            Me.lv.Items.Add(it)
        Next

        ' Add other columns (which are not displayed)
        For Each s As String In ss
            If Me.lv.Items.ContainsKey(s) = False Then
                Dim it As New ListViewItem(s)
                it.SubItems.Add("")
                it.SubItems.Add(HorizontalAlignment.Left.ToString)
                Me.lv.Items.Add(it)
            End If
        Next

    End Sub

    Private Sub cmdInvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInvert.Click
        Dim it As ListViewItem
        For Each it In Me.lv.Items
            it.Checked = Not (it.Checked)
        Next
    End Sub

    Private Sub cmdMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveUp.Click
        If Me.lv.SelectedItems Is Nothing OrElse Me.lv.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        If Me.lv.SelectedItems(0).Index = 0 Then
            Exit Sub
        End If

        Me.lv.BeginUpdate()
        MoveListViewItem(Me.lv, True)
        Me.lv.EndUpdate()
    End Sub

    Private Sub cmdMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoveDown.Click
        If Me.lv.SelectedItems Is Nothing OrElse Me.lv.SelectedItems.Count <> 1 Then
            Exit Sub
        End If
        If Me.lv.SelectedItems(0).Index = Me.lv.Items.Count - 1 Then
            Exit Sub
        End If

        Me.lv.BeginUpdate()
        MoveListViewItem(Me.lv, False)
        Me.lv.EndUpdate()
    End Sub

    Private Sub cmdDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDefault.Click
        ' Set default columns

    End Sub

    Private Sub lv_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseDown
        If Me.lv.SelectedItems.Count = 1 AndAlso e.Button = Windows.Forms.MouseButtons.Right Then
            Dim sAlign As String = Me.lv.SelectedItems(0).SubItems(2).Text
            Select Case sAlign
                Case HorizontalAlignment.Left.ToString
                    sAlign = HorizontalAlignment.Center.ToString
                Case HorizontalAlignment.Right.ToString
                    sAlign = HorizontalAlignment.Left.ToString
                Case Else '"Center"
                    sAlign = HorizontalAlignment.Right.ToString
            End Select
            Me.lv.SelectedItems(0).SubItems(2).Text = sAlign
        End If
    End Sub

End Class