﻿'
'' Lite Process Monitor
'
'
'


'
'
'
'
''
'
'
'
'
''
'
'


'Option Strict On

'Imports System.Runtime.InteropServices
'Imports System.Text
'Imports System.Windows.Forms
'Imports System.Management

'Public Class asyncCallbackMemRegionEnumerate

'    Private ctrl As Control
'    Private deg As [Delegate]
'    Private con As cMemRegionConnection
'    Private _instanceId As Integer
'    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cMemRegionConnection, ByVal iId As Integer)
'        ctrl = ctr
'        deg = de
'        _instanceId = iId
'        con = co
'    End Sub

'    Public Structure poolObj
'        Public pid As Integer
'        Public forInstanceId As Integer
'        Public Sub New(ByVal pi As Integer, ByVal ii As Integer)
'            forInstanceId = ii
'            pid = pi
'        End Sub
'    End Structure

'    ' When socket got a list  !
'    Private _poolObj As poolObj
'    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
'        Dim dico As New Dictionary(Of String, memRegionInfos)
'        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
'            For x As Integer = 0 To lst.Length - 1
'                dico.Add(keys(x), DirectCast(lst(x), memRegionInfos))
'            Next
'        End If
'        If deg IsNot Nothing AndAlso ctrl.Created Then _
'            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
'    End Sub
'    Private Shared sem As New System.Threading.Semaphore(1, 1)
'    Public Sub Process(ByVal thePoolObj As Object)

'        Try
'            sem.WaitOne()

'            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
'            If Program.Connection.IsConnected = False Then
'                Exit Sub
'            End If

'            Select Case Program.Connection.Type

'                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
'                    _poolObj = pObj
'                    Try
'                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestMemoryRegionList, pObj.pid)
'                        cDat.InstanceId = _instanceId   ' Instance which request the list
'                        Program.Connection.Socket.Send(cDat)
'                    Catch ex As Exception
'                        Misc.ShowError(ex, "Unable to send request to server")
'                    End Try

'                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI


'                Case cConnection.TypeOfConnection.SnapshotFile
'                    ' Snapshot

'                    Dim _dico As New Dictionary(Of String, memRegionInfos)
'                    Dim snap As cSnapshot = con.ConnectionObj.Snapshot
'                    If snap IsNot Nothing Then
'                        ' For some processes only
'                        _dico = snap.MemoryRegionsByProcessId(pObj.pid)
'                    End If
'                    Try
'                        If deg IsNot Nothing AndAlso ctrl.Created Then _
'                            ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
'                    Catch ex As Exception
'                        Misc.ShowDebugError(ex)
'                    End Try

'                Case Else
'                    ' Local

'                    Dim _dico As New Dictionary(Of String, memRegionInfos)

'                    Native.Objects.MemRegion.EnumerateMemoryRegionsByProcessId(pObj.pid, _dico)

'                    Try
'                        If deg IsNot Nothing AndAlso ctrl.Created Then _
'                            ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
'                    Catch ex As Exception
'                        Misc.ShowDebugError(ex)
'                    End Try

'            End Select

'        Catch ex As Exception
'            Misc.ShowDebugError(ex)
'        Finally
'            sem.Release()
'        End Try

'    End Sub


'    ' Shared, local and sync enumeration
'    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj) As Dictionary(Of String, memRegionInfos)
'        Dim _dico As New Dictionary(Of String, memRegionInfos)
'        Native.Objects.MemRegion.EnumerateMemoryRegionsByProcessId(pObj.pid, _dico)
'        Return _dico
'    End Function

'End Class
