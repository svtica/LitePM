﻿
' Lite Process Monitor



Option Strict On

Public Class MemRegionProvider


    ' Private constants




    ' Private attributes


    ' Current processes running PID <-> (string <-> heaps)
    Private Shared _currentMemRegions As New Dictionary(Of Integer, Dictionary(Of String, memRegionInfos))
    Friend Shared _semMemRegions As New System.Threading.Semaphore(1, 1)

    ' First refresh done ?
    Private Shared _firstRefreshDone As Boolean = False

    ' Sempahore to protect async ProcessEnumeration
    Friend Shared _semProcessEnumeration As New System.Threading.Semaphore(1, 1)



    ' Public properties


    ' First refresh done ?
    Public Shared Property FirstRefreshDone() As Boolean
        Get
            Return _firstRefreshDone
        End Get
        Friend Set(ByVal value As Boolean)
            _firstRefreshDone = value
        End Set
    End Property

    ' Clear list of env variables
    Public Shared Sub ClearList()
        Try
            _semMemRegions.WaitOne()
            _currentMemRegions.Clear()
        Finally
            _semMemRegions.Release()
        End Try
    End Sub

    ' Clear list for a specific processID
    Public Shared Sub ClearListForAnId(ByVal pid As Integer)
        Try
            _semMemRegions.WaitOne()
            If _currentMemRegions.ContainsKey(pid) Then
                _currentMemRegions(pid).Clear()
            End If
        Finally
            _semMemRegions.Release()
        End Try
    End Sub

    ' List of current processes
    Public Shared ReadOnly Property CurrentMemRegions(ByVal pid As Integer) As Dictionary(Of String, memRegionInfos)
        Get
            Try
                _semMemRegions.WaitOne()
                If _currentMemRegions.ContainsKey(pid) Then
                    Return _currentMemRegions(pid)
                Else
                    Return New Dictionary(Of String, memRegionInfos)
                End If
            Finally
                _semMemRegions.Release()
            End Try
        End Get
    End Property

    Public Shared Sub SetCurrentMemRegions(ByVal pid As Integer, ByVal value As Dictionary(Of String, memRegionInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, memRegionInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semMemRegions.WaitOne()

            ' Add a new entry
            If _currentMemRegions.ContainsKey(pid) = False Then
                _currentMemRegions.Add(pid, New Dictionary(Of String, memRegionInfos))
            End If

            ' Get deleted items
            For Each vars As String In _currentMemRegions(pid).Keys
                If Not (value.ContainsKey(vars)) Then
                    _dicoDel.Add(vars, _currentMemRegions(pid)(vars))
                    _dicoDelSimp.Add(vars)
                End If
            Next

            ' Get new items
            For Each vars As String In value.Keys
                If Not (_currentMemRegions(pid).ContainsKey(vars)) Then
                    _dicoNew.Add(vars)
                End If
            Next

            ' Re-assign dico
            _currentMemRegions(pid) = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semMemRegions.Release()
        End Try

        ' Raise events
        RaiseEvent GotDeletedItems(_dicoDel, instanceId, res)
        RaiseEvent GotNewItems(_dicoNew, value, instanceId, res)
        RaiseEvent GotRefreshed(_dicoNew, _dicoDelSimp, value, instanceId, res)
        _firstRefreshDone = True

    End Sub



    ' Other public


    ' Shared events
    Public Shared Event GotNewItems(ByVal keys As List(Of String), ByVal newItems As Dictionary(Of String, memRegionInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal keys As Dictionary(Of String, memRegionInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal newItems As List(Of String), ByVal delItems As List(Of String), ByVal Dico As Dictionary(Of String, memRegionInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public pid As Integer
        Public instId As Integer
        Public Sub New(ByVal procId As Integer, ByVal instanceId As Integer)
            pid = procId
            instId = instanceId
        End Sub
    End Structure




    ' Public functions


    ' Constructor
    Public Sub New()
        ' Add handler for general connection/deconnection
        AddHandler Program.Connection.Connected, AddressOf eventConnected
        AddHandler Program.Connection.Disconnected, AddressOf eventDisConnected
        AddHandler Program.Connection.Socket.ReceivedData, AddressOf eventSockReceiveData
    End Sub

    ' Refresh list of env variables by processId depending on the connection NOW
    Public Shared Sub Update(ByVal pid As Integer, ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf MemRegionProvider.ProcessEnumeration), _
                New MemRegionProvider.asyncEnumPoolObj(pid, instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal pid As Integer, ByVal instanceId As Integer)
        ' This is of course sync
        MemRegionProvider.ProcessEnumeration(New MemRegionProvider.asyncEnumPoolObj(pid, instanceId))
    End Sub


    ' Private functions


    ' Called when connected
    Private Sub eventConnected()

        ' Connect
        Select Case Program.Connection.Type
            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' Nothing special here

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' Nothing special here

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Nothing special here

            Case cConnection.TypeOfConnection.LocalConnection
                ' Nothing special here
        End Select

    End Sub

    ' Called when disconnected
    Private Sub eventDisConnected()
        ' Nothing special here
    End Sub

    ' Called when socket receive data
    Private Sub eventSockReceiveData(ByRef data As cSocketData)

        ' Exit immediately if not connected
        If Program.Connection.IsConnected AndAlso _
            Program.Connection.Type = cConnection.TypeOfConnection.RemoteConnectionViaSocket Then

            If data Is Nothing Then
                Trace.WriteLine("Serialization error")
                Exit Sub
            End If

            If data.Type = cSocketData.DataType.RequestedList AndAlso _
                data.Order = cSocketData.OrderType.RequestMemoryRegionList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of mem regions !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, memRegionInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), memRegionInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary.
        ' Have to get the processId of the current list of processes, as there might
        ' be memregion enumeration for more than one process.
        ' So we retrieve the informations by enumerating the variables and getting
        ' the first PID
        Dim pid As Integer
        For Each it As memRegionInfos In _dico.Values
            pid = it.ProcessId
            Exit For
        Next
        MemRegionProvider.SetCurrentMemRegions(pid, _dico, instanceId)

    End Sub

    ' Enumeration of mem regions
    Private Shared Sub ProcessEnumeration(ByVal thePoolObj As Object)

        Try
            ' Synchronisation
            _semProcessEnumeration.WaitOne()

            If Program.Connection.IsConnected Then

                Dim pObj As asyncEnumPoolObj = DirectCast(thePoolObj, asyncEnumPoolObj)
                Select Case Program.Connection.Type

                    Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                        ' Send cDat
                        Try
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestMemoryRegionList, pObj.pid)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' Not available

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of String, memRegionInfos)
                        Dim snap As cSnapshot250 = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.MemoryRegionsByProcessId(pObj.pid)
                        End If

                        ' Save current processes into a dictionary
                        MemRegionProvider.SetCurrentMemRegions(pObj.pid, _dico, pObj.instId)

                    Case Else
                        ' Local
                        Dim _dico As Dictionary(Of String, memRegionInfos) = _
                                SharedLocalSyncEnumerate(pObj)

                        ' Save current processes into a dictionary
                        MemRegionProvider.SetCurrentMemRegions(pObj.pid, _dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

    ' Shared, local and sync enumeration
    Private Shared Function SharedLocalSyncEnumerate(ByVal pObj As asyncEnumPoolObj) As Dictionary(Of String, memRegionInfos)
        Dim _dico As New Dictionary(Of String, memRegionInfos)
        Native.Objects.MemRegion.EnumerateMemoryRegionsByProcessId(pObj.pid, _dico)
        Return _dico
    End Function

End Class
