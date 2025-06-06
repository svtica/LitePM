﻿
' Lite Process Monitor









'




'




Option Strict On
Imports Native.Api.Enums

<Serializable()> Public Class searchInfos
    Inherits generalInfos

    Private _typeOfItem As GeneralObjectType
    Private _field As String
    Private _res As String
    Private _owner As String
    Private _pid As Integer

#Region "Constructors & destructor"

    Public Sub New(ByVal Item As cGeneralObject, ByVal field As String, _
                ByVal result As String)
        _res = result
        _field = field
        _typeOfItem = Item.TypeOfObject
        _pid = GetProcessId(Item)
        _owner = GetOwner(Item, _pid)
    End Sub

#End Region

#Region "Read only properties"

    Public ReadOnly Property Field() As String
        Get
            Return _field
        End Get
    End Property
    Public ReadOnly Property Result() As String
        Get
            Return _res
        End Get
    End Property
    Public ReadOnly Property Type() As GeneralObjectType
        Get
            Return _typeOfItem
        End Get
    End Property
    Public ReadOnly Property Owner() As String
        Get
            Return _owner
        End Get
    End Property
    Public ReadOnly Property OwnedProcessId() As Integer
        Get
            Return _pid
        End Get
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Return ""     ' Not used
        End Get
    End Property

#End Region

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(3) As String

        s(0) = "Type"
        s(1) = "Result"
        s(2) = "Field"
        s(3) = "Owner"

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function


    ' Return owner of item
    Private Shared Function GetOwner(ByVal obj As cGeneralObject, ByVal pid As Integer) As String
        ' Just say a big thanks to polymorphism...
        Try
            Dim res As String = ""
            Dim _pid As Integer = pid

            If obj.TypeOfObject <> GeneralObjectType.Service Then
                ' Try to get the owner process
                Dim _curProc As cProcess = ProcessProvider.GetProcessById(_pid)
                If _curProc IsNot Nothing Then
                    Dim nn As String = _curProc.Infos.Name
                    If String.IsNullOrEmpty(nn) = False Then
                        res = "Process " & nn & " (" & _pid.ToString & ")"
                    Else
                        res = "Process " & _pid.ToString
                    End If
                Else
                    res = "Process " & _pid.ToString
                End If
            Else
                res = DirectCast(obj, cService).Infos.Name
            End If

            Return res
        Catch ex As Exception
            Return "Unknown"
        End Try
    End Function

    ' Return associated process ID
    Private Shared Function GetProcessId(ByVal obj As cGeneralObject) As Integer
        Select Case obj.TypeOfObject
            Case GeneralObjectType.EnvironmentVariable
                Return DirectCast(obj, cEnvVariable).Infos.ProcessId
            Case GeneralObjectType.Handle
                Return DirectCast(obj, cHandle).Infos.ProcessId
            Case GeneralObjectType.Module
                Return DirectCast(obj, cModule).Infos.ProcessId
            Case GeneralObjectType.Process
                Return DirectCast(obj, cProcess).Infos.ProcessId
            Case GeneralObjectType.Service
                Return (DirectCast(obj, cService).Infos.ProcessId)
            Case GeneralObjectType.Window
                Return DirectCast(obj, cWindow).Infos.ProcessId
        End Select
    End Function

End Class
