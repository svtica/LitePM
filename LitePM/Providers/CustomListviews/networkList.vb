﻿

Option Strict On

Imports System.Runtime.InteropServices

Public Class networkList
    Inherits customLV

    Public Event ItemAdded(ByRef item As cNetwork)
    Public Event ItemDeleted(ByRef item As cNetwork)
    Public Event HasRefreshed()
    Public Event GotAnError(ByVal origin As String, ByVal msg As String)



    ' Private

    Private _first As Boolean
    Private _dicoNew As New Dictionary(Of String, cNetwork)
    Private _dicoDel As New Dictionary(Of String, cNetwork)
    Private _buffDico As New Dictionary(Of String, Native.Api.Structs.LightConnection)
    Private _dico As New Dictionary(Of String, cNetwork)
    Private WithEvents _connectionObject As New cConnection
    Private WithEvents _networkConnection As New cNetworkConnection(Me, _connectionObject, New cNetworkConnection.HasEnumeratedEventHandler(AddressOf HasEnumeratedEventHandler))
    Private _all As Boolean = False
    Private _showGroups As Boolean = False
    Private _pid As Integer


#Region "Properties"


    ' Properties

    Public Property ConnectionObj() As cConnection
        Get
            Return _connectionObject
        End Get
        Set(ByVal value As cConnection)
            _connectionObject = value
        End Set
    End Property
    Public Property ProcessId() As Integer
        Get
            Return _pid
        End Get
        Set(ByVal value As Integer)
            _pid = value
        End Set
    End Property
    Public Property ShowAllPid() As Boolean
        Get
            Return _all
        End Get
        Set(ByVal value As Boolean)
            _all = value
        End Set
    End Property
    Public Property ShowConnectionsByProcessesGroup() As Boolean
        Get
            Return _showGroups
        End Get
        Set(ByVal value As Boolean)
            _showGroups = value
        End Set
    End Property

#End Region


    ' Public functions


    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        _first = True

        ' Set handlers
        _networkConnection.Disconnected = New cnetworkConnection.DisconnectedEventHandler(AddressOf HasDisconnected)
        _networkConnection.Connected = New cnetworkConnection.ConnectedEventHandler(AddressOf HasConnected)
    End Sub

    ' Delete all items
    Public Sub ClearItems()
        _first = True
        _buffDico.Clear()
        _dico.Clear()
        _dicoDel.Clear()
        _dicoNew.Clear()
        Me.Items.Clear()
    End Sub

    ' Call this to update items in listview
    Public Overrides Sub UpdateItems()

        ' Create a buffer of subitems if necessary
        If _columnsName Is Nothing Then
            Call CreateSubItemsBuffer()
        End If

        If _networkConnection.IsConnected Then

            ' Now enumerate items
            _networkConnection.Enumerate(_first, _pid, _all)

        End If

    End Sub

    ' Get all items (associated to listviewitems)
    Public Function GetAllItems() As Dictionary(Of String, cNetwork).ValueCollection
        Return _dico.Values
    End Function

    ' Get the selected item
    Public Function GetSelectedItem() As cNetwork
        If Me.SelectedItems.Count > 0 Then
            Return _dico.Item(Me.SelectedItems.Item(0).Name)
        Else
            Return Nothing
        End If
    End Function

    ' Get a specified item
    Public Function GetItemByKey(ByVal key As String) As cNetwork
        If _dico.ContainsKey(key) Then
            Return _dico.Item(key)
        Else
            Return Nothing
        End If
    End Function

    ' Get selected items
    Public Shadows Function GetSelectedItems() As Dictionary(Of String, cNetwork).ValueCollection
        Dim res As New Dictionary(Of String, cNetwork)

        generalLvSemaphore.WaitOne()
        For Each it As ListViewItem In Me.SelectedItems
            res.Add(it.Name, _dico.Item(it.Name))
        Next
        generalLvSemaphore.Release()

        Return res.Values
    End Function



    ' Private properties


    ' Executed when enumeration is done
    Private Sub HasEnumeratedEventHandler(ByVal Success As Boolean, ByVal Dico As Dictionary(Of String, networkInfos), ByVal errorMessage As String, ByVal instanceId As Integer)

        generalLvSemaphore.WaitOne()

        If Success = False Then
            Trace.WriteLine("Cannot enumerate, an error was raised...")
            RaiseEvent GotAnError("Network connection enumeration", errorMessage)
            generalLvSemaphore.Release()
            Exit Sub
        End If

        ' We won't enumerate next time with all informations (included fixed infos)
        _first = False


        ' Now add all items with isKilled = true to _dicoDel dictionnary
        For Each z As cNetwork In _dico.Values
            If z.IsKilledItem Then
                _dicoDel.Add(z.Infos.ProcessId.ToString & "-" & z.Infos.Protocol.ToString & "-" & z.Infos.Local.ToString, Nothing)
            End If
        Next


        ' Now add new items to dictionnary
        For Each pair As System.Collections.Generic.KeyValuePair(Of String, networkInfos) In Dico
            If Not (_dico.ContainsKey(pair.Key)) Then
                ' Add to dico
                _dicoNew.Add(pair.Key, New cNetwork(pair.Value))
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
        For Each z As String In _dicoDel.Keys
            If Me.Items.ContainsKey(z) Then
                Me.Items.RemoveByKey(z)
                RaiseEvent ItemDeleted(_dico.Item(z))
                _dico.Item(z).Dispose()
                _dico.Remove(z)
            End If
        Next
        _dicoDel.Clear()


        ' Merge _dico and _dicoNew
        For Each z As String In _dicoNew.Keys
            Dim _it As cNetwork = _dicoNew.Item(z)
            RaiseEvent ItemAdded(_it)
            _it.IsNewItem = Not (_firstItemUpdate)        ' If first refresh, don't highlight item
            _dico.Add(z.ToString, _it)
        Next


        ' Now add all new items to listview
        ' If first time, lock listview
        If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.BeginUpdate()
        For Each z As String In _dicoNew.Keys
            ' Add to listview
            Dim _subItems() As String
            ReDim _subItems(Me.Columns.Count - 1)
            For x As Integer = 1 To _subItems.Length - 1
                _subItems(x) = ""
            Next
            Dim _tmp As cNetwork = _dico.Item(z)
            AddItemWithStyle(z, _tmp).SubItems.AddRange(_subItems)
        Next
        If _firstItemUpdate OrElse _dicoNew.Count > EMPIRIC_MINIMAL_NUMBER_OF_NEW_ITEMS_TO_BEGIN_UPDATE OrElse _dicoDel.Count > EMPIRIC_MINIMAL_NUMBER_OF_DELETED_ITEMS_TO_BEGIN_UPDATE Then Me.EndUpdate()
        _dicoNew.Clear()


        ' Now refresh all subitems of the listview
        Dim isub As ListViewItem.ListViewSubItem
        Dim it As ListViewItem
        For Each it In Me.Items
            Dim x As Integer = 0
            If _dico.ContainsKey(it.Name) Then
                Dim _item As cNetwork = _dico.Item(it.Name)
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
                    it.BackColor = NEW_ITEM_COLOR
                ElseIf _item.IsKilledItem Then
                    it.BackColor = DELETED_ITEM_COLOR
                Else
                    it.BackColor = Color.White
                End If
            End If
        Next

        ' Sort items
        Me.Sort()

        _firstItemUpdate = False

        'Trace.WriteLine("It tooks " & _test.ToString & " ms to refresh thread list.")

        MyBase.UpdateItems()

        generalLvSemaphore.Release()

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
    Private Shadows Function AddItemWithStyle(ByVal key As String, ByRef net As cNetwork) As ListViewItem

        Dim item As ListViewItem = Me.Items.Add(key)
        item.Name = key
        item.ForeColor = _foreColor
        item.Tag = key

        ' Add a group if necessary
        If ShowConnectionsByProcessesGroup AndAlso _all Then
            If Me.Groups(net.Infos.ProcessId.ToString) Is Nothing Then
                Dim sText As String
                If net.Infos.ProcessId > 0 Then
                    sText = net.Infos.ProcessName & " (" & net.Infos.ProcessId.ToString & ")"
                Else
                    sText = "Unknow process"
                End If
                Me.Groups.Add(net.Infos.ProcessId.ToString, sText)
            End If
            item.Group = Me.Groups(net.Infos.ProcessId.ToString)
        End If

        Return item

    End Function

#Region "Connection stuffs"

    Private Sub _connectionObject_Connected() Handles _connectionObject.Connected
        Call Connect()
    End Sub

    Private Sub _connectionObject_Disconnected() Handles _connectionObject.Disconnected
        Call Disconnect()
    End Sub

    Protected Overrides Function Connect() As Boolean
        If MyBase.Connect Then
            Me.IsConnected = True
            _first = True
            _networkConnection.ConnectionObj = _connectionObject
            _networkConnection.Connect()
            cNetwork.Connection = _networkConnection
        End If
    End Function

    Protected Overrides Function Disconnect() As Boolean
        If MyBase.Disconnect Then
            Me.IsConnected = False
            _networkConnection.Disconnect()
        End If
    End Function

    Private Sub HasDisconnected(ByVal Success As Boolean)
        ' We HAVE TO disconnect, because this event is raised when we got an error
        '_networkConnection.Disconnect()
        '     _networkConnection.Con()
    End Sub

    Private Sub HasConnected(ByVal Success As Boolean)
        '
    End Sub

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
