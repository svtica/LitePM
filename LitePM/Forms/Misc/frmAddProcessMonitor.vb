
' Lite Process Monitor


Option Strict On
Imports Common.Misc

Public Class frmAddProcessMonitor

    ' Process to select by default
    Public _selProcess As Integer

    Private _con As cConnection

    Public Sub New(ByRef connection As cConnection)
        InitializeComponent()
        _con = connection
    End Sub

    Private Sub frmAddProcessMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CloseWithEchapKey(Me)

        SetToolTip(Me.butAdd, "Monitor the selected process")
        SetToolTip(Me.txtInterval, "Set the refresh interval (milliseconds)")
        SetToolTip(Me.cmdAddToList, "Add counters from list")
        SetToolTip(Me.cmdRemoveFromList, "Remove counters from list")
        SetToolTip(Me.cmdSearch, "Launch search")
        SetToolTip(Me.lstToAdd, "Counters to monitor")
        SetToolTip(Me.lstCounterType, "Available counters")
        SetToolTip(Me.lstInstance, "Available instances")
        SetToolTip(Me.lstCategory, "Available categories")

        Native.Functions.Misc.SetTheme(Me.lstToAdd.Handle)
        Native.Functions.Misc.SetTheme(Me.lstInstance.Handle)
        Native.Functions.Misc.SetTheme(Me.lstCounterType.Handle)
        Native.Functions.Misc.SetTheme(Me.lstCategory.Handle)

        'Call Me.cmdRefresh_Click(Nothing, Nothing)

        '' Select desired process (_selProcess)
        'Dim s As String
        'For Each s In Me.cbProcess.Items
        '    Dim i As Integer = InStr(s, " -- ", CompareMethod.Binary)
        '    Dim _name As String = s.Substring(0, i - 1)
        '    Dim _pid As Integer = CInt(Val(s.Substring(i + 3, s.Length - i - 3)))
        '    If _pid = _selProcess Then
        '        Me.cbProcess.Text = s
        '        Exit For
        '    End If
        'Next
        Try
            Dim myCat2 As PerformanceCounterCategory()
            Dim i As Integer
            Me.lstCategory.Items.Clear()
            If _con.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat2 = PerformanceCounterCategory.GetCategories(_con.WmiParameters.serverName)
            Else    ' Local
                myCat2 = PerformanceCounterCategory.GetCategories
            End If
            For i = 0 To myCat2.Length - 1
                Me.lstCategory.Items.Add(myCat2(i).CategoryName)
            Next
        Catch ex As Exception
            ' Cannot connect to network or access denied ??
            Misc.ShowDebugError(ex)
            Misc.ShowError(ex, "Unable to access to performance counters")
        End Try

    End Sub

    Private Sub butAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butAdd.Click
        ' Monitor our process
        Dim lstIt As ListViewItem

        _frmMain.tvMonitor.BeginUpdate()
        _frmMain.lvMonitorReport.BeginUpdate()

        For Each lstIt In Me.lstToAdd.Items

            Dim obj As Native.Api.Structs.PerfCounter = _
                        DirectCast(lstIt.Tag, Native.Api.Structs.PerfCounter)

            With obj
                Dim _name As String = .instanceName
                Dim _cat As String = .categoryName
                Dim _count As String = .counterTypeName

                Dim it As cMonitor
                If _con.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                    it = New cMonitor(_cat, _count, _name, _con.WmiParameters.serverName)
                Else
                    it = New cMonitor(_cat, _count, _name)
                End If
                it.Interval = CInt(Val(Me.txtInterval.Text))
                _frmMain.AddMonitoringItem(it)
            End With

        Next

        _frmMain.UpdateMonitoringLog()
        _frmMain.tvMonitor.EndUpdate()
        _frmMain.lvMonitorReport.EndUpdate()

        Me.Close()

    End Sub

    Private Sub cmdAddToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddToList.Click
        ' Add selected counters to wish list
        Dim listIt As ListViewItem

        Dim _name As String = vbNullString
        Dim _cat As String = vbNullString
        Dim _count As String = vbNullString
        If Me.lstCategory.SelectedItems Is Nothing Then Exit Sub

        For Each listIt In Me.lstCounterType.SelectedItems

            _count = listIt.Text
            If Me.lstInstance.SelectedItems IsNot Nothing AndAlso Me.lstInstance.SelectedItems.Count > 0 Then _name = Me.lstInstance.SelectedItems(0).Text
            If _count = vbNullString And _name = vbNullString Then Exit Sub
            _cat = Me.lstCategory.SelectedItems(0).Text

            Dim it As New Native.Api.Structs.PerfCounter(_cat, _count, _name)

            Dim sName As String = _cat & " -- " & CStr(IIf(_name = vbNullString, vbNullString, _name & " -- ")) & _count
            Dim bPresent As Boolean = False

            ' Check if item is already added or not
            Dim lvIt As ListViewItem
            For Each lvIt In Me.lstToAdd.Items
                If lvIt.Text = sName Then
                    bPresent = True
                    Exit For
                End If
            Next

            If bPresent = False Then
                Dim itList As New ListViewItem
                itList.Text = sName
                itList.Tag = it
                Me.lstToAdd.Items.Add(itList)
            End If
        Next

        Me.butAdd.Enabled = (Me.lstCounterType.Items.Count > 0)
    End Sub

    Private Sub cmdRemoveFromList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveFromList.Click
        Dim it As ListViewItem
        For Each it In Me.lstToAdd.SelectedItems
            it.Remove()
        Next
        Me.butAdd.Enabled = (Me.lstCounterType.Items.Count > 0)
    End Sub

    Private Sub lstCategory_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCategory.MouseDown
        Common.Misc.CopyLvToClip(e, lstCategory)
    End Sub

    Private Sub lstCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCategory.SelectedIndexChanged
        Dim mypc() As String
        Dim i As Integer
        If lstCategory.SelectedItems IsNot Nothing AndAlso lstCategory.SelectedItems.Count > 0 Then
            Dim myCat As PerformanceCounterCategory
            If _con.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text)
            End If
            txtHelp.Text = myCat.CategoryHelp
            Me.lstInstance.Items.Clear()
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetInstanceNames
                For i = 0 To mypc.Length - 1
                    Me.lstInstance.Items.Add(mypc(i))
                Next
                ' Add a comment if there is no instance available
                Me.lstInstance.Enabled = (Me.lstInstance.Items.Count > 0)
                If Me.lstInstance.Items.Count = 0 Then
                    Me.lstInstance.Items.Add("No instance available")
                End If
            Catch ex As Exception
                '
            End Try
            Call lstInstance_SelectedIndexChanged(Nothing, Nothing)
        End If

    End Sub

    Private Sub lstInstance_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstInstance.MouseDown
        Common.Misc.CopyLvToClip(e, lstInstance)
    End Sub

    Private Sub lstInstance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstInstance.SelectedIndexChanged
        Dim mypc() As PerformanceCounter
        Dim i As Integer
        Me.lstCounterType.Items.Clear()
        If lstInstance.SelectedItems.Count = 0 Then
            Dim myCat As PerformanceCounterCategory
            If _con.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text)
            End If
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetCounters()
                For i = 0 To mypc.Length - 1
                    Me.lstCounterType.Items.Add(mypc(i).CounterName)
                Next
            Catch ex As Exception
                '
            End Try
        Else
            Dim myCat As PerformanceCounterCategory
            If _con.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text, _con.WmiParameters.serverName)
            Else
                myCat = New PerformanceCounterCategory(lstCategory.SelectedItems(0).Text)
            End If
            Me.lstCounterType.Items.Clear()
            Try
                mypc = myCat.GetCounters(lstInstance.SelectedItems(0).Text)
                For i = 0 To mypc.Length - 1
                    Me.lstCounterType.Items.Add(mypc(i).CounterName)
                Next
            Catch ex As Exception
                '
            End Try
        End If
        Me.butAdd.Enabled = (Me.lstCounterType.SelectedItems.Count > 0)
    End Sub

    Private Sub lstCounterType_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstCounterType.MouseDown
        Common.Misc.CopyLvToClip(e, lstCounterType)
    End Sub

    Private Sub lstCounterType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCounterType.SelectedIndexChanged
        If lstCounterType.SelectedItems IsNot Nothing AndAlso lstCounterType.SelectedItems.Count > 0 Then
            Dim myCat As PerformanceCounter
            Try
                If _con.Type = cConnection.TypeOfConnection.RemoteConnectionViaWMI Then
                    myCat = New PerformanceCounter(Me.lstCategory.SelectedItems(0).Text, lstCounterType.SelectedItems(0).Text, Nothing, _con.WmiParameters.serverName)
                Else
                    myCat = New PerformanceCounter(Me.lstCategory.SelectedItems(0).Text, lstCounterType.SelectedItems(0).Text)
                End If
                txtHelp.Text = myCat.CounterHelp
                Me.cmdAddToList.Enabled = True
            Catch ex As Exception
                ' Multi instance counter
                Me.cmdAddToList.Enabled = False
            End Try
        End If
    End Sub

    Private Sub lstToAdd_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstToAdd.MouseDown
        Common.Misc.CopyLvToClip(e, lstToAdd)
    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Dim frm As New frmSearchMonitor
        frm.TopMost = _frmMain.TopMost
        frm.ShowDialog()
    End Sub
End Class