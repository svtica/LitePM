﻿
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management

Public Class asyncCallbackPrivilegesEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cPrivilegeConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cPrivilegeConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public pid As Integer
        Public forInstanceId As Integer
        Public Sub New(ByVal pi As Integer, ByVal ii As Integer)
            forInstanceId = ii
            pid = pi
        End Sub
    End Structure

    ' When socket got a list  !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, privilegeInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), privilegeInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestPrivilegesList, pObj.pid)
                    cDat.InstanceId = _instanceId   ' Instance which request the list
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot

                Dim _dico As New Dictionary(Of String, privilegeInfos)
                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then
                    ' For some processes only
                    _dico = snap.PrivilegesByProcessId(pObj.pid)
                End If
                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case Else
                ' Local
                Dim _dico As Dictionary(Of String, privilegeInfos) = _
                        SharedLocalSyncEnumerate(pObj)

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
    Public Shared Function SharedLocalSyncEnumerate(ByVal pObj As poolObj) As Dictionary(Of String, privilegeInfos)
        Dim _dico As New Dictionary(Of String, privilegeInfos)

        Dim ret As Native.Api.NativeStructs.PrivilegeInfo() = _
            Native.Objects.Token.GetPrivilegesListByProcessId(pObj.pid)

        For Each tmp As Native.Api.NativeStructs.PrivilegeInfo In ret
            _dico.Add(tmp.Name, New privilegeInfos(tmp.Name, pObj.pid, tmp.Status))
        Next
        Return _dico
    End Function

End Class
