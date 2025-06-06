﻿
' Lite Process Monitor









'




'




Option Strict On

<Serializable()> Public Class windowInfos
    Inherits generalInfos

#Region "Private attributes"

    Protected _procName As String
    Protected _processId As Integer
    Protected _handle As IntPtr
    Protected _isTask As Boolean
    Protected _positions As Native.Api.NativeStructs.Rect
    Protected _enabled As Boolean
    Protected _visible As Boolean
    Protected _threadId As Integer
    Protected _height As Integer
    Protected _width As Integer
    Protected _top As Integer
    Protected _left As Integer
    Protected _opacity As Byte
    Protected _caption As String

#End Region

#Region "Properties"

    Friend ReadOnly Property Caption() As String
        Get
            Return _caption
        End Get
    End Property
    Public ReadOnly Property ProcessId() As Integer
        Get
            Return _processId
        End Get
    End Property
    Public Property IsTask() As Boolean
        Get
            Return _isTask
        End Get
        Friend Set(ByVal value As Boolean)
            _isTask = value
        End Set
    End Property
    Public ReadOnly Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
    End Property
    Public ReadOnly Property Visible() As Boolean
        Get
            Return _visible
        End Get
    End Property
    Public ReadOnly Property ThreadId() As Integer
        Get
            Return _threadId
        End Get
    End Property
    Public ReadOnly Property Height() As Integer
        Get
            Return _height
        End Get
    End Property
    Public ReadOnly Property Width() As Integer
        Get
            Return _width
        End Get
    End Property
    Public ReadOnly Property Top() As Integer
        Get
            Return _top
        End Get
    End Property
    Public ReadOnly Property Left() As Integer
        Get
            Return _left
        End Get
    End Property
    Public ReadOnly Property Opacity() As Byte
        Get
            Return _opacity
        End Get
    End Property
    Public ReadOnly Property Handle() As IntPtr
        Get
            Return _handle
        End Get
    End Property
    Public ReadOnly Property ProcessName() As String
        Get
            If _procName = vbNullString Then
                _procName = cProcess.GetProcessName(Me.ProcessId)
                If _procName = vbNullString Then
                    _procName = NO_INFO_RETRIEVED
                End If
            End If
            Return _procName
        End Get
    End Property
    Public ReadOnly Property Positions() As Native.Api.NativeStructs.Rect
        Get
            Return _positions
        End Get
    End Property
    Public Overrides ReadOnly Property Key() As String
        Get
            Static _key As String
            If _key Is Nothing Then
                _key = Me.ProcessId.ToString & "-" & Me.ThreadId.ToString & "-" & Me.Handle.ToString
            End If
            Return _key
        End Get
    End Property
#End Region



    ' Public

    Public Sub New()
        '
    End Sub
    Public Sub New(ByRef window As windowInfos)
        With window
            _processId = .ProcessId
            _threadId = .ThreadId
            _handle = .Handle
            _caption = .Caption
            _procName = .ProcessName
            _isTask = .IsTask
            _positions = .Positions
            _enabled = .Enabled
            _visible = .Visible
            _height = .Height
            _width = .Width
            _top = .Top
            _left = .Left
            _opacity = .Opacity
        End With
    End Sub
    Public Sub New(ByVal pid As Integer, ByVal tid As Integer, ByVal handle As IntPtr, ByVal caption As String)
        _processId = pid
        _threadId = tid
        _handle = handle
        _caption = caption
    End Sub

    Friend Sub SetNonFixedInfos(ByRef infos As Native.Api.Structs.WindowNonFixedInfo)
        With infos
            _enabled = .enabled
            _height = .height
            _isTask = .isTask
            _left = .left
            _opacity = .opacity
            _positions = .theRect
            _top = .top
            _visible = .visible
            _width = .width
            If _positions.IsNull = False Then
                ' If it's Null, we assume the window has been closed (and as
                ' the new caption is "", we do not update it)
                _caption = .caption
            End If
        End With
    End Sub


    ' Merge an old and a new instance
    Public Sub Merge(ByRef infos As windowInfos)

        With infos
            _enabled = .enabled
            _height = .height
            _isTask = .isTask
            _left = .left
            _opacity = .opacity
            _positions = .Positions
            _top = .top
            _visible = .visible
            _width = .width
            _caption = .caption
        End With

    End Sub

    ' Retrieve all information's names availables
    Public Shared Function GetAvailableProperties(Optional ByVal includeFirstProp As Boolean = False, Optional ByVal sorted As Boolean = False) As String()
        Dim s(10) As String

        s(0) = "Caption"
        s(1) = "Process"
        s(2) = "IsTask"
        s(3) = "Enabled"
        s(4) = "Visible"
        s(5) = "ThreadId"
        s(6) = "Height"
        s(7) = "Width"
        s(8) = "Top"
        s(9) = "Left"
        s(10) = "Opacity"

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
