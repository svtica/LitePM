﻿
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management
Imports System.Net

Public Class asyncCallbackJobEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cJobConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cJobConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public forInstanceId As Integer
        Public Sub New(ByVal ii As Integer)
            forInstanceId = ii
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, jobInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), jobInfos))
            Next
        End If
        If deg IsNot Nothing AndAlso ctrl.Created Then _
            ctrl.Invoke(deg, True, dico, Nothing, _instanceId)
    End Sub
    Private Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        sem.WaitOne()

        Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
        If con.ConnectionObj.IsConnected = False Then
            sem.Release()
            Exit Sub
        End If

        Select Case con.ConnectionObj.ConnectionType

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                _poolObj = pObj
                Try
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestJobList)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

                ' Save current collection
                Dim _dico As New Dictionary(Of String, jobInfos)
                Dim res As Boolean
                Dim msg As String = ""

                res = Wmi.Objects.Job.EnumerateJobs(con.wmiSearcher, _dico, msg)
                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, res, _dico, msg, 0)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot

                Dim _dico As New Dictionary(Of String, jobInfos)
                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then
                    _dico = snap.Jobs
                End If
                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case Else
                ' Local

                Dim _dico As Dictionary(Of String, jobInfos) = _
                        Native.Objects.Job.EnumerateJobs

                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

        End Select

        sem.Release()

    End Sub


    ' Shared, local and sync enumeration
    Public Shared Function SharedLocalSyncEnumerate() As Dictionary(Of String, jobInfos)
        Dim _dico As Dictionary(Of String, jobInfos) = Native.Objects.Job.EnumerateJobs
        Return _dico
    End Function

End Class
