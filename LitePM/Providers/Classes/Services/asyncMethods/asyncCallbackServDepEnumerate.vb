﻿
' Lite Process Monitor









'




'




Option Strict On

Public Class asyncCallbackServDepEnumerate

    Private ctrl As Control
    Private deg As [Delegate]
    Private con As cServDepConnection
    Private _instanceId As Integer

    Public Sub New(ByRef ctr As Control, ByVal de As [Delegate], ByRef co As cServDepConnection, ByVal iId As Integer)
        ctrl = ctr
        deg = de
        _instanceId = iId
        con = co
    End Sub

    Public Structure poolObj
        Public name As String
        Public type As cServDepConnection.DependenciesToget
        Public forInstanceId As Integer
        Public Sub New(ByVal nam As String, ByVal typ As cServDepConnection.DependenciesToget, ByVal forII As Integer)
            name = nam
            type = typ
            forInstanceId = forII
        End Sub
    End Structure


    ' When socket got a list !
    Private _poolObj As poolObj
    Friend Sub GotListFromSocket(ByRef lst() As generalInfos, ByRef keys() As String, ByVal type As cServDepConnection.DependenciesToget)
        Dim dico As New Dictionary(Of String, serviceInfos)
        If lst IsNot Nothing AndAlso keys IsNot Nothing AndAlso lst.Length = keys.Length Then
            For x As Integer = 0 To lst.Length - 1
                dico.Add(keys(x), DirectCast(lst(x), serviceInfos))
            Next
        End If
        Try
            If deg IsNot Nothing AndAlso ctrl.Created Then _
                ctrl.Invoke(deg, True, dico, Nothing, _instanceId, type)
        Catch ex As Exception

        End Try
    End Sub
    Friend Shared sem As New System.Threading.Semaphore(1, 1)
    Public Sub Process(ByVal thePoolObj As Object)

        Try
            sem.WaitOne()

            Dim pObj As poolObj = DirectCast(thePoolObj, poolObj)
            If Program.Connection.IsConnected = False Then
                Exit Sub
            End If

            Select Case Program.Connection.Type

                Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                    _poolObj = pObj
                    Try
                        Dim cDat As New cSocketData(cSocketData.DataType.Order, cSocketData.OrderType.RequestServDepList, pObj.name, pObj.type)
                        cDat.InstanceId = _instanceId   ' Instance which request the list
                        Program.Connection.Socket.Send(cDat)
                    Catch ex As Exception
                        Misc.ShowError(ex, "Unable to send request to server")
                    End Try

                Case cConnection.TypeOfConnection.RemoteConnectionViaWMI


                Case cConnection.TypeOfConnection.SnapshotFile
                    '' Snapshot file
                    'Dim _dico As New Dictionary(Of String, serviceInfos)
                    'Dim snap As cSnapshot = con.ConnectionObj.Snapshot
                    'If snap IsNot Nothing Then
                    '    _dico = snap.Services
                    'End If
                    'Try
                    '    'If deg IsNot Nothing AndAlso ctrl.Created Then _
                    '    ctrl.Invoke(deg, True, _dico, Nothing, pObj.forInstanceId, pObj.type)
                    'Catch ex As Exception

                    'End Try

                Case Else
                    ' Local
                    Dim _dico As New Dictionary(Of String, serviceInfos)
                    If pObj.type = cServDepConnection.DependenciesToget.ServiceWhichDependsFromMe Then
                        recursiveAddDep(pObj.name, pObj.name, _dico)
                    Else
                        recursiveAddDep2(pObj.name, pObj.name, _dico)
                    End If
                    Try
                        If deg IsNot Nothing AndAlso ctrl.Created Then _
                            ctrl.Invoke(deg, True, _dico, Native.Api.Win32.GetLastError, pObj.forInstanceId, pObj.type)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try

            End Select

        Catch ex As Exception
            Misc.ShowDebugError(ex)
        Finally
            sem.Release()
        End Try

    End Sub

    Private Sub recursiveAddDep(ByVal parent As String, ByVal chain As String, ByRef _dico As Dictionary(Of String, serviceInfos))
        For Each ii As serviceInfos In ServiceProvider.GetServiceDependencies(parent).Values
            ii.Tag = False
            _dico.Add(chain & "->" & ii.Name, ii)
            recursiveAddDep(ii.Name, chain & "->" & ii.Name, _dico)
        Next
    End Sub
    Private Sub recursiveAddDep2(ByVal parent As String, ByVal chain As String, ByRef _dico As Dictionary(Of String, serviceInfos))
        For Each ii As serviceInfos In ServiceProvider.GetServiceWhichDependFromByServiceName(parent).Values
            ii.Tag = False
            _dico.Add(chain & "->" & ii.Name, ii)
            recursiveAddDep2(ii.Name, chain & "->" & ii.Name, _dico)
        Next
    End Sub

End Class
