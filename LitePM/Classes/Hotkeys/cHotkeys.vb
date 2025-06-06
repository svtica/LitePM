﻿
' Lite Process Monitor









'




'




Option Strict On

Public Class cHotkeys


    ' API declarations


    Private Const vbKeyShift As Integer = 16
    Private Const vbKeyControl As Integer = 17
    Private Const vbKeyAlt As Integer = 18
    Private Const vbShiftMask As Integer = vbKeyShift
    Private Const vbCtrlMask As Integer = vbKeyControl
    Private Const vbAltMask As Integer = vbKeyAlt



    ' Private attributes

    Private hKeyHook As IntPtr
    Private boolStopHooking As Boolean              ' Private !
    Private _col As New Collection

    ' Delegate function (function which replace the 'normal' one)
    Private myCallbackDelegate As Native.Api.NativeFunctions.HookProcKbd = Nothing



    ' Public properties

    Public ReadOnly Property HotKeysCollection() As Collection
        Get
            Return _col
        End Get
    End Property
    Public ReadOnly Property ActionsAvailable() As String()
        Get
            Dim s() As String
            ReDim s(1)
            s(0) = "Kill foreground application"
            s(1) = "Exit LitePM"
            Return s
        End Get
    End Property


    ' Public functions


    ' Add a key to collection
    Public Function AddHotkey(ByVal hotkey As cShortcut) As Boolean
        Dim sKey As String = "|" & CStr(CInt(hotkey.Key1)) & "|" & CStr(CInt(hotkey.Key2)) & "|" & CStr(CInt(hotkey.Key3))
        Try
            _col.Add(Key:=sKey, Item:=hotkey)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' Remove key from collection
    Public Function RemoveHotKey(ByVal hotkey As cShortcut) As Boolean
        Dim sKey As String = "|" & CStr(CInt(hotkey.Key1)) & "|" & CStr(CInt(hotkey.Key2)) & "|" & CStr(CInt(hotkey.Key3))
        Try
            _col.Remove(sKey)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function RemoveHotKey(ByVal hotkey As String) As Boolean
        Try
            _col.Remove(hotkey)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub New()
        Call AttachKeyboardHook()
    End Sub
    Protected Overrides Sub Finalize()
        Call DetachKeyboardHook()
    End Sub



    ' Private functions



    ' Start hooking of keyboard

    Private Sub AttachKeyboardHook()
        If hKeyHook.IsNull Then

            ' Initialize our delegate
            Me.myCallbackDelegate = New Native.Api.NativeFunctions.HookProcKbd(AddressOf Me.KeyboardFilter)

            hKeyHook = Native.Api.NativeFunctions.SetWindowsHookEx(Native.Api.NativeEnums.HookType.KeyboardLowLevel, _
                        Me.myCallbackDelegate, IntPtr.Zero, 0) ' 0 -> all threads
            GC.KeepAlive(Me.myCallbackDelegate)
        End If

    End Sub


    ' Stop hooking of keyboard

    Private Sub DetachKeyboardHook()

        If (hKeyHook.IsNotNull) Then
            Call Native.Api.NativeFunctions.UnhookWindowsHookEx(hKeyHook)
            hKeyHook = IntPtr.Zero
        End If

    End Sub



    ' This function is called each time a key is pressed

    Private Function KeyboardFilter(ByVal nCode As Integer, ByVal wParam As IntPtr, ByRef lParam As Native.Api.NativeStructs.KBDLLHookStruct) As Integer
        Dim bAlt As Boolean
        Dim bCtrl As Boolean
        Dim bShift As Boolean
        Dim cSs As Object


        If nCode = Native.Api.NativeConstants.HC_ACTION And Not boolStopHooking Then

            bShift = (Native.Api.NativeFunctions.GetAsyncKeyState(vbKeyShift) <> 0)
            bAlt = (Native.Api.NativeFunctions.GetAsyncKeyState(vbKeyAlt) <> 0)
            bCtrl = (Native.Api.NativeFunctions.GetAsyncKeyState(vbKeyControl) <> 0)

            ' Check for each of our cShortCut if the shortcut is activated
            For Each cSs In _col

                Dim cs As cShortcut = CType(cSs, cShortcut)

                If cs.Enabled Then

                    If (cs.Key1 = cShortcut.ShorcutKeys.VK_NO_BUTTON) Or (cs.Key1 = vbShiftMask And bShift) Or (cs.Key1 = vbAltMask And bAlt) Or _
                        (cs.Key1 = vbCtrlMask And bCtrl) Then

                        ' Then the first of the 3 keys is pressed
                        ' Check the second one
                        If (cs.Key2 = cShortcut.ShorcutKeys.VK_NO_BUTTON) Or (cs.Key2 = vbShiftMask And bShift) Or (cs.Key2 = vbAltMask And bAlt) Or _
                            (cs.Key2 = vbCtrlMask And bCtrl) Then

                            ' The the second of the 3 keys is pressed
                            ' Check this last one
                            If (lParam.vkCode = cs.Key3) Then

                                ' The third of the 3 keys is pressed
                                boolStopHooking = True                      ' We stop hooking shortcuts

                                ' Process emergency action
                                cs.RaiseAction()

                                boolStopHooking = False                     ' Now we can hook another shortcuts
                                Exit For

                            End If

                        End If

                    End If

                End If

            Next cSs

        End If

        ' Next hook
        KeyboardFilter = Native.Api.NativeFunctions.CallNextHookEx(hKeyHook, nCode, wParam, lParam)

    End Function
End Class
