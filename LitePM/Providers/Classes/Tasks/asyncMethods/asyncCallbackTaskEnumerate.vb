﻿
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Management
Imports Native.Objects

Public Class asyncCallbackTaskEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cTaskConnection
    Private _instanceId As Integer
    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cTaskConnection, ByVal iId As Integer)
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

    ' When socket got a list of processes !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String)
        Dim dico As New Dictionary(Of String, windowInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), windowInfos))
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
                    Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestTaskList)
                    cDat.InstanceId = _instanceId
                    con.ConnectionObj.Socket.Send(cDat)
                Catch ex As Exception
                    Misc.ShowError(ex, "Unable to send request to server")
                End Try

            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI

            Case cConnection.TypeOfConnection.SnapshotFile
                ' Snapshot file

                Dim _dico As New Dictionary(Of String, windowInfos)
                Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                If snap IsNot Nothing Then
                    _dico = snap.Tasks
                End If
                Try
                    If deg IsNot Nothing AndAlso ctrl.Created Then _
                        ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try

            Case Else
                ' Local
                Dim _dico As Dictionary(Of String, windowInfos) = SharedLocalSyncEnumerate()

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
    Public Shared Function SharedLocalSyncEnumerate() As Dictionary(Of String, windowInfos)
        Dim currWnd As IntPtr
        Dim cpt As Integer

        Dim _dico As New Dictionary(Of String, windowInfos)

        currWnd = Window.GetWindow(Window.GetDesktopWindow, _
                                    Native.Api.NativeEnums.GetWindowCmd.Child)
        cpt = 0
        Do While currWnd.IsNotNull

            If Window.IsWindowATask(currWnd) Then
                Dim pid As Integer = Window.GetProcessIdFromWindowHandle(currWnd)
                Dim tid As Integer = Window.GetThreadIdFromWindowHandle(currWnd)
                Dim key As String = pid.ToString & "-" & tid.ToString & "-" & currWnd.ToString

                If _dico.ContainsKey(key) = False Then
                    If Program.Parameters.ModeServer Then
                        ' Then we need to retrieve all informations
                        ' (this is server mode)
                        Dim wInfo As windowInfos
                        wInfo = New windowInfos(pid, tid, currWnd, Window.GetWindowCaption(currWnd))
                        wInfo.SetNonFixedInfos(asyncCallbackWindowGetNonFixedInfos.ProcessAndReturnLocal(currWnd))
                        _dico.Add(key, wInfo)
                    Else
                        _dico.Add(key, New windowInfos(pid, tid, currWnd, Window.GetWindowCaption(currWnd)))
                    End If
                End If

            End If

            currWnd = Window.GetWindow(currWnd, _
                                    Native.Api.NativeEnums.GetWindowCmd.[Next])
        Loop

        Return _dico
    End Function

End Class
