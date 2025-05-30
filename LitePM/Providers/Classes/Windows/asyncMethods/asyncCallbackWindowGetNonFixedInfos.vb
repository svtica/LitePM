
Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Objects

Public Class asyncCallbackWindowGetNonFixedInfos

    Public Structure TheseInfos
        Public enabled As Boolean
        Public height As Integer
        Public isTask As Boolean
        Public left As Integer
        Public opacity As Byte
        Public top As Integer
        Public visible As Boolean
        Public width As Integer
        Public theRect As Native.Api.NativeStructs.Rect
        Public caption As String
        Public Sub New(ByVal enab As Boolean, ByVal isTas As Boolean, _
                       ByVal opac As Byte, ByRef r As Native.Api.NativeStructs.Rect, ByVal scap As String, ByVal isV As Boolean)
            enabled = enab
            isTask = isTas
            opacity = opac
            height = r.Bottom - r.Top
            width = r.Right - r.Left
            top = r.Top
            left = r.Left
            theRect = r
            caption = scap
            visible = isV
        End Sub
    End Structure

    ' Totally sync retrieving of informations
    Public Shared Function ProcessAndReturnLocal(ByVal handle As IntPtr) As TheseInfos
        Dim enabled As Boolean = Window.IsWindowEnabled(handle)
        Dim visible As Boolean = Window.IsWindowVisible(handle)
        Dim opacity As Byte = Window.GetWindowOpacityByHandle(handle)
        Dim isTask As Boolean = Window.IsWindowATask(handle)
        Dim r As Native.Api.NativeStructs.Rect = Window.GetWindowPositionAndSizeByHandle(handle)
        Dim s As String = Window.GetWindowCaption(handle)
        Return New TheseInfos(enabled, isTask, opacity, r, s, visible)
    End Function

End Class
