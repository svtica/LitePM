﻿
' Lite Process Monitor









'




'




Option Strict On

<Serializable()> Public Class threadInfos
    Inherits generalInfos

#Region "Private attributes"

    Private _AffinityMask As IntPtr
    Private _KernelTime As Long
    Private _UserTime As Long
    Private _CreateTime As Long
    Private _WaitTime As Integer
    Private _StartAddress As IntPtr
    Private _Id As Integer
    Private _ProcessId As Integer
    Private _Priority As Integer
    Private _BasePriority As Integer
    Private _ContextSwitchCount As Integer
    Private _State As ThreadState
    Private _WaitReason As Native.Api.NativeEnums.KwaitReason

#End Region

#Region "Read only properties"

    Public ReadOnly Property TotalTime() As Long
        Get
            Return _KernelTime + _UserTime
        End Get
    End Property
    Public ReadOnly Property KernelTime() As Long
        Get
            Return _KernelTime
        End Get
    End Property
    Public ReadOnly Property UserTime() As Long
        Get
            Return _UserTime
        End Get
    End Property
    Public ReadOnly Property CreateTime() As Long
        Get
            Return _CreateTime
        End Get
    End Property
    Public ReadOnly Property WaitTime() As Integer
        Get
            Return _WaitTime
        End Get
    End Property
    Public ReadOnly Property StartAddress() As IntPtr
        Get
            Return _StartAddress
        End Get
    End Property
    Public ReadOnly Property Id() As Integer
        Get
            Return _Id
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _ProcessId
        End Get
    End Property
    Public ReadOnly Property Priority() As System.Diagnostics.ThreadPriorityLevel
        Get
            Return Native.Functions.Thread.GetThreadPriorityClassFromInt(_Priority)
        End Get
    End Property
    Public ReadOnly Property BasePriority() As System.Diagnostics.ThreadPriorityLevel
        Get
            Return Native.Functions.Thread.GetThreadPriorityClassFromInt(_BasePriority)
        End Get
    End Property
    Public ReadOnly Property ContextSwitchCount() As Integer
        Get
            Return _ContextSwitchCount
        End Get
    End Property
    Public ReadOnly Property State() As ThreadState
        Get
            Return _State
        End Get
    End Property
    Public ReadOnly Property WaitReason() As Native.Api.NativeEnums.KwaitReason
        Get
            Return _WaitReason
        End Get
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Return _Id.ToString & "-" & _ProcessId.ToString
        End Get
    End Property

    Public ReadOnly Property ContextSwitchDelta() As Integer
        Get
            Static oldCount As Integer = Me.ContextSwitchCount
            Dim res As Integer = Me.ContextSwitchCount - oldCount
            oldCount = Me.ContextSwitchCount
            Return res
        End Get
    End Property

    Friend Sub SetPriority(ByVal i As Integer)
        _Priority = i
    End Sub

#End Region

#Region "Other Non-fixed informations"

    Public Property AffinityMask() As IntPtr
        Get
            Return _AffinityMask
        End Get
        Set(ByVal value As IntPtr)
            _AffinityMask = value
        End Set
    End Property

#End Region



    ' Public


    ' Constructor of this class
    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef Thr As Native.Api.NativeStructs.SystemThreadInformation)

        With Thr
            _AffinityMask = IntPtr.Zero
            _Id = .ClientId.UniqueThread.ToInt32
            _ProcessId = .ClientId.UniqueProcess.ToInt32
            _BasePriority = .BasePriority
            _ContextSwitchCount = .ContextSwitchCount
            _CreateTime = .CreateTime
            _KernelTime = .KernelTime
            _Priority = .Priority
            _StartAddress = .StartAddress
            _State = .State
            _UserTime = .UserTime
            _WaitReason = .WaitReason
            _WaitTime = .WaitTime
        End With

    End Sub

    ' Merge an old and a new instance
    Public Sub Merge(ByRef newI As threadInfos)

        With newI
            _AffinityMask = .AffinityMask
            _BasePriority = .BasePriority
            _ContextSwitchCount = .ContextSwitchCount
            _CreateTime = .CreateTime
            _KernelTime = .KernelTime
            _Priority = .Priority
            _State = .State
            _UserTime = .UserTime
            _WaitReason = .WaitReason
            _WaitTime = .WaitTime
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(13) As String

        s(0) = "Priority"
        s(1) = "State"
        s(2) = "WaitReason"
        s(3) = "CreateTime"
        s(4) = "KernelTime"
        s(5) = "UserTime"
        s(6) = "WaitTime"
        s(7) = "TotalTime"
        s(8) = "StartAddress"
        s(9) = "BasePriority"
        s(10) = "AffinityMask"
        s(11) = "ContextSwitchCount"
        s(12) = "ContextSwitchDelta"
        s(13) = "ProcessId"

        If includeFirstProp Then
            Dim s2(s.Length) As String
            Array.Copy(s, 0, s2, 1, s.Length)
            s2(0) = "Id"
            s = s2
        End If

        If sorted Then
            Array.Sort(s)
        End If

        Return s
    End Function

End Class
