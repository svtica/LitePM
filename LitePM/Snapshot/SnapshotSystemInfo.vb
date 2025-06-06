﻿
' Lite Process Monitor









'




'



Option Strict On

<Serializable()> _
Public Class SnapshotSystemInfo

#Region "Private attributes"

    Private _ComputerName As String
    Private _UserName As String
    Private _Culture As System.Globalization.CultureInfo
    Private _IntPtrSize As Integer
    Private _OSFullName As String
    Private _OSPlatform As String
    Private _OSVersion As String
    Private _AvailablePhysicalMemory As ULong
    Private _AvailableVirtualMemory As ULong
    Private _TotalPhysicalMemory As ULong
    Private _TotalVirtualMemory As ULong

#End Region


#Region "Public properties"

    Public ReadOnly Property ComputerName() As String
        Get
            Return _ComputerName
        End Get
    End Property

    Public ReadOnly Property UserName() As String
        Get
            Return _UserName
        End Get
    End Property

    Public ReadOnly Property Culture() As System.Globalization.CultureInfo
        Get
            Return _Culture
        End Get
    End Property

    Public ReadOnly Property IntPtrSize() As Integer
        Get
            Return _IntPtrSize
        End Get
    End Property

    Public ReadOnly Property OSFullName() As String
        Get
            Return _OSFullName
        End Get
    End Property

    Public ReadOnly Property OSPlatform() As String
        Get
            Return _OSPlatform
        End Get
    End Property

    Public ReadOnly Property OSVersion() As String
        Get
            Return _OSVersion
        End Get
    End Property

    Public ReadOnly Property AvailablePhysicalMemory() As ULong
        Get
            Return _AvailablePhysicalMemory
        End Get
    End Property

    Public ReadOnly Property AvailableVirtualMemory() As ULong
        Get
            Return _AvailableVirtualMemory
        End Get
    End Property

    Public ReadOnly Property TotalPhysicalMemory() As ULong
        Get
            Return _TotalPhysicalMemory
        End Get
    End Property

    Public ReadOnly Property TotalVirtualMemory() As ULong
        Get
            Return _TotalVirtualMemory
        End Get
    End Property

#End Region


#Region "Constructor & desctructor"

    Public Sub New(ByVal con As cConnection)

        ' Automatically fill in properties
        Select Case con.Type

            Case cConnection.TypeOfConnection.LocalConnection
                ' Local connection
                _ComputerName = My.Computer.Name
                _UserName = Common.Objects.Token.CurrentUserName
                With My.Computer.Info
                    _Culture = .InstalledUICulture
                    _OSFullName = .OSFullName
                    _OSPlatform = .OSPlatform
                    _OSVersion = .OSVersion
                    _AvailablePhysicalMemory = .AvailablePhysicalMemory
                    _AvailableVirtualMemory = .AvailableVirtualMemory
                    _TotalPhysicalMemory = .TotalPhysicalMemory
                    _TotalVirtualMemory = .TotalVirtualMemory
                End With
                _IntPtrSize = IntPtr.Size

            Case cConnection.TypeOfConnection.RemoteConnectionViaSocket
                ' TODO
            Case cConnection.TypeOfConnection.RemoteConnectionViaWMI
                ' TODO
            Case cConnection.TypeOfConnection.SnapshotFile
                '
            Case Else
                '
        End Select
    End Sub

#End Region

End Class
