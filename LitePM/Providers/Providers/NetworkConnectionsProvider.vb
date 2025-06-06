﻿
' Lite Process Monitor



Option Strict On

Public Class NetworkConnectionsProvider


    ' Private constants




    ' Private attributes


    ' Current processes running
    Private Shared _currentConnections As New Dictionary(Of String, networkInfos)
    Friend Shared _semConnections As New System.Threading.Semaphore(1, 1)

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

    ' List of current processes
    Public Shared ReadOnly Property CurrentNetworkConnections() As Dictionary(Of String, networkInfos)
        Get
            Return _currentConnections
        End Get
    End Property
    Public Shared Sub SetCurrentConnections(ByVal value As Dictionary(Of String, networkInfos), ByVal instanceId As Integer)

        Dim _dicoDel As New Dictionary(Of String, networkInfos)
        Dim _dicoDelSimp As New List(Of String)
        Dim _dicoNew As New List(Of String)

        Dim res As Native.Api.Structs.QueryResult

        Try
            _semConnections.WaitOne()

            ' Get deleted items
            For Each key As String In _currentConnections.Keys
                If Not (value.ContainsKey(key)) Then
                    _dicoDel.Add(key, _currentConnections(key))
                    _dicoDelSimp.Add(key)
                End If
            Next

            ' Get new items
            For Each key As String In value.Keys
                If Not (_currentConnections.ContainsKey(key)) Then
                    _dicoNew.Add(key)
                End If
            Next

            ' Re-assign dico
            _currentConnections = value

            res = New Native.Api.Structs.QueryResult(True)

        Catch ex As Exception
            Misc.ShowDebugError(ex)
            res = New Native.Api.Structs.QueryResult(ex)
        Finally
            _semConnections.Release()
        End Try

        ' Raise events
        RaiseEvent GotDeletedItems(_dicoDel, instanceId, res)
        RaiseEvent GotNewItems(_dicoNew, value, instanceId, res)
        RaiseEvent GotRefreshed(_dicoNew, _dicoDelSimp, value, instanceId, res)
        _firstRefreshDone = True

    End Sub




    ' Other public


    ' Shared events
    Public Shared Event GotNewItems(ByVal news As List(Of String), ByVal newItems As Dictionary(Of String, networkInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotDeletedItems(ByVal dels As Dictionary(Of String, networkInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)
    Public Shared Event GotRefreshed(ByVal news As List(Of String), ByVal dels As List(Of String), ByVal Dico As Dictionary(Of String, networkInfos), ByVal instanceId As Integer, ByVal res As Native.Api.Structs.QueryResult)

    ' Structure used to store parameters of enumeration
    Public Structure asyncEnumPoolObj
        Public instId As Integer
        Public Sub New(ByVal instanceId As Integer)
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

    ' Clear list of current elements
    Public Shared Sub ClearList()
        Try
            _semConnections.WaitOne()
            _currentConnections.Clear()
        Finally
            _semConnections.Release()
        End Try
    End Sub

    ' Refresh list of processes depending on the connection NOW
    Public Shared Sub Update(ByVal instanceId As Integer)
        ' This is of course async
        Call Threading.ThreadPool.QueueUserWorkItem( _
                New System.Threading.WaitCallback(AddressOf NetworkConnectionsProvider.ProcessEnumeration), _
                New NetworkConnectionsProvider.asyncEnumPoolObj(instanceId))
    End Sub
    Public Shared Sub SyncUpdate(ByVal instanceId As Integer)
        ' This is of course sync
        NetworkConnectionsProvider.ProcessEnumeration(New NetworkConnectionsProvider.asyncEnumPoolObj(instanceId))
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
                data.Order = cSocketData.OrderType.RequestNetworkConnectionList Then
                ' We receive the list
                Me.GotListFromSocket(data.GetList, data.GetKeys, data.InstanceId)
            End If

        End If

    End Sub

    ' When socket got a list of processes !
    Private Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal instanceId As Integer)
        Dim _dico As New Dictionary(Of String, networkInfos)

        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                If _dico.ContainsKey(keys(x)) = False Then
                    _dico.Add(keys(x), DirectCast(lst(x), networkInfos))
                End If
            Next
        End If

        ' Save current processes into a dictionary
        NetworkConnectionsProvider.SetCurrentConnections(_dico, instanceId)

    End Sub

    ' Enumeration of processes
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
                            Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestNetworkConnectionList)
                            cDat.InstanceId = pObj.instId
                            Program.Connection.Socket.Send(cDat)
                        Catch ex As Exception
                            Misc.ShowError(ex, "Unable to send request to server")
                        End Try

                    Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                        ' Not available

                    Case cConnection.TypeOfConnection.SnapshotFile
                        ' Snapshot

                        Dim _dico As New Dictionary(Of String, networkInfos)
                        Dim snap As cSnapshot250 = Program.Connection.Snapshot
                        If snap IsNot Nothing Then
                            _dico = snap.NetworkConnections
                        End If

                        ' Save current processes into a dictionary
                        NetworkConnectionsProvider.SetCurrentConnections(_dico, pObj.instId)

                    Case Else
                        ' Local
                        Dim _dico As New Dictionary(Of String, networkInfos)

                        ' Enumeration
                        Native.Objects.Network.EnumerateTcpUdpConnections(_dico, True)

                        ' Save current processes into a dictionary
                        NetworkConnectionsProvider.SetCurrentConnections(_dico, pObj.instId)

                End Select

            End If

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            _semProcessEnumeration.Release()
        End Try

    End Sub

End Class
