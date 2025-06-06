﻿
' Lite Process Monitor









'




'




Option Strict On

Public Class serviceList
    Inherits customLV


    ' Private

    Private _pid As Integer
    Private _first As Boolean
    Private _dico As New Dictionary(Of String, cService)
    Private WithEvents _connectionObject As New cConnection

#Region "Properties"


    ' Properties

    Public Property ProcessId() As Integer
        Get
            Return _pid
        End Get
        Set(ByVal value As Integer)
            _pid = value
        End Set
    End Property

#End Region


    ' Public functions


    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        _IMG = New ImageList
        _IMG.ImageSize = New Size(16, 16)
        _IMG.ColorDepth = ColorDepth.Depth32Bit

        Me.SmallImageList = _IMG
        _IMG.Images.Add("service", My.Resources.gear)   ' Icon is specific

        _first = True

        ' Create buffer
        Me.CreateSubItemsBuffer()
        AddHandler Program.Connection.Connected, AddressOf impConnected
        AddHandler Program.Connection.Disconnected, AddressOf impDisConnected
    End Sub

    ' Get an item from listview
    Public Function GetImageFromImageList(ByVal key As String) As System.Drawing.Image
        Return _IMG.Images.Item(key)
    End Function

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _dico.Clear()
        _IMG.Images.Clear()
        _IMG.Images.Add("service", My.Resources.gear)   ' Icon is specific
        Me.Items.Clear()
    End Sub

    ' Reanalize a process
    Public Sub ReAnalizeServices()

        ' In local mode, we simply refresh the service
        ' In WMI and Socket mode, there IS NO NEED to reanalize because all informations
        ' are retrieved each time we refresh
        ' In fact, "Reanalize" is only available for Local mode
        If Program.Connection.Type = cConnection.TypeOfConnection.LocalConnection Then
            For Each cs As cService In Me.GetSelectedItems
                cs.Refresh()
            Next
        End If
    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cService).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cService
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cService
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cService).ValueCollection
        Dim res As New Dictionary(Of String, cService)

        Try
            generalLvSemaphore.WaitOne()
            For Each it As ListViewItem In Me.SelectedItems
                res.Add(it.Name, _dico.Item(it.Name))
            Next
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            generalLvSemaphore.Release()
        End Try

        Return res.Values
    End Function

    ' Dispose
    Public Overloads Sub Dispose()
        MyBase.Dispose()
        Me.ClearItems()
        RemoveHandler Program.Connection.Connected, AddressOf impConnected
        RemoveHandler Program.Connection.Disconnected, AddressOf impDisConnected
    End Sub



    ' Private properties


#Region "Update methods & callbacks"

    ' Called when connected
    Public Sub impConnected()
        ' Nothing special here
    End Sub

    ' Called when disconnected
    Public Sub impDisConnected()
        _timeToDisplayNewItemsGreen = False
        _firstItemUpdate = True
    End Sub

    Public Overrides Sub UpdateTheItems()

        Try
            generalLvSemaphore.WaitOne()

            ' This should not be used elsewhere...
            ServiceProvider._semServices.WaitOne()

            ' Get current services
            Dim Dico As Dictionary(Of String, serviceInfos) = ServiceProvider.CurrentServices

            ' We won't enumerate next time with all informations (included fixed infos)
            _first = False


            ' Now add all items with isKilled = true to _dicoDel dictionnary
            Dim _dicoDel As New List(Of String)
            Dim _dicoNew As New List(Of String)
            For Each z As cService In _dico.Values
                If z.IsKilledItem Then
                    _dicoDel.Add(z.Infos.Name)
                End If
            Next


            ' Now add new items to dictionnary
            For Each pair As System.Collections.Generic.KeyValuePair(Of String, serviceInfos) In Dico
                ' Services for ONLY one process
                If Not (_dico.ContainsKey(pair.Key)) AndAlso pair.Value.ProcessId = Me.ProcessId Then
                    ' Add to dico
                    _dicoNew.Add(pair.Key)
                End If
            Next


            ' Now remove deleted items from dictionnary
            For Each z As String In _dico.Keys
                If Dico.ContainsKey(z) = False Then
                    ' Remove from dico
                    _dico.Item(z).IsKilledItem = True  ' Will be deleted next time
                End If
            Next


            ' Now remove all deleted items from listview and _dico
            If _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then
                Me.BeginUpdate()
            End If
            For Each z As String In _dicoDel
                Me.Items.RemoveByKey(z)
                _dico.Remove(z)
            Next
            If _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then
                Me.EndUpdate()
            End If


            ' Merge _dico and _dicoNew
            For Each z As String In _dicoNew
                Dim _it As New cService(Dico(z))
                _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
                _dico.Add(z, _it)
            Next


            ' Create buffer if necessary
            If _columnsName.Length = 0 Then
                Me.CreateSubItemsBuffer()
            End If


            ' Now add all new items to listview
            ' If first time, lock listview
            If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.BeginUpdate()
            For Each z As String In _dicoNew

                ' Add to listview
                Dim _subItems() As String
                ReDim _subItems(Me.Columns.Count - 1)
                For x As Integer = 1 To _subItems.Length - 1
                    _subItems(x) = ""
                Next
                AddItemWithStyle(z).SubItems.AddRange(_subItems)
            Next
            If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.EndUpdate()


            ' Now refresh all subitems of the listview
            Dim isub As ListViewItem.ListViewSubItem
            Dim it As ListViewItem
            For Each it In Me.Items
                Dim x As Integer = 0
                Dim _item As cService = _dico.Item(it.Name)
                If Dico.ContainsKey(it.Name) Then
                    _item.Merge(Dico.Item(it.Name))
                End If
                Dim __info As String = Nothing
                For Each isub In it.SubItems
                    If _item.GetInformation(_columnsName(x), __info) Then
                        isub.Text = __info
                    End If
                    x += 1
                Next
                If _item.IsNewItem Then
                    _item.IsNewItem = False
                    If _timeToDisplayNewItemsGreen Then
                        it.BackColor = NEW_ITEM_COLOR
                    Else
                        it.BackColor = _item.GetBackColor
                    End If
                ElseIf _item.IsKilledItem Then
                    it.BackColor = DELETED_ITEM_COLOR
                Else
                    _timeToDisplayNewItemsGreen = True
                    it.BackColor = _item.GetBackColor
                End If
            Next

            ' Sort items
            Me.Sort()

            _firstItemUpdate = False

            'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh service list.")

            MyBase.UpdateItems()

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            ServiceProvider._semServices.Release()
            generalLvSemaphore.Release()
        End Try

    End Sub

    ' Force item refreshing
    Public Overrides Sub ForceRefreshingOfAllItems()    ' Always called in a safe protected context
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            If _dico.ContainsKey(it.Name) Then
                Dim _item As cGeneralObject = _dico.Item(it.Name)
                For Each isub In it.SubItems
                    _item.GetInformation(_columnsName(x), isub.Text)
                    x += 1
                Next
                If _item.IsNewItem Then
                    _item.IsNewItem = False
                    it.BackColor = NEW_ITEM_COLOR
                ElseIf _item.IsKilledItem Then
                    it.BackColor = DELETED_ITEM_COLOR
                Else
                    it.BackColor = Color.White
                End If
            End If
        Next
    End Sub

    ' Add an item (specific to type of list)
    Friend Overrides Function AddItemWithStyle(ByVal key As String) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        With item
            .Name = key
            .ImageKey = "service"
            .ForeColor = _foreColor
            .Tag = key
            '.Group = Me.Groups(0)
        End With

        Return item

    End Function

#End Region


    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        MyBase.OnKeyDown(e)
        If e.Shift AndAlso e.Control Then
            ' OK, show thread management
            For Each obj As cGeneralObject In Me.GetSelectedItems
                Dim frm As New frmPendingTasks(obj)
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Next
        ElseIf e.KeyCode = Keys.F7 Then
            Me.showObjectProperties()
        End If
    End Sub

    ' Display properties form
    Protected Overrides Sub OnMouseDoubleClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDoubleClick(e)
        If Me.ShowObjectDetailsOnDoubleClick Then
            Me.showObjectProperties()
        End If
    End Sub

    Private Sub showObjectProperties()
        For Each obj As cGeneralObject In Me.GetSelectedItems
            Dim frm As New frmObjDetails
            frm.TopMost = _frmMain.TopMost
            frm.TheObject = obj
            frm.Show()
        Next
    End Sub

End Class
