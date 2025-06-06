﻿
' Lite Process Monitor









'




'




Option Strict On

Imports System.Runtime.InteropServices
Imports Native.Api

Public Class cEnvironment

    Public Enum PrivilegeToRequest
        ShutdownPrivilege
        DebugPrivilege
    End Enum

    ' Public properties

    ' Return true if the application is already running
    Public Shared ReadOnly Property IsAlreadyRunning() As Boolean
        Get
            Dim hMap As IntPtr
            Dim pMem As IntPtr
            Dim hPid As Integer

            Const FILE_NAME As String = "LitePM-instanceCheck"

            '# Nous tentons ici d'acceder au mappage (précedemment créé ?)
            hMap = NativeFunctions.OpenFileMapping(NativeConstants.FILE_MAP_READ, False, FILE_NAME)
            If hMap.IsNotNull Then
                '# L'application est déjà lancée.
                pMem = NativeFunctions.MapViewOfFile(hMap, NativeEnums.FileMapAccess.FileMapRead, 0, 0, 0)
                If pMem.IsNotNull Then
                    '# On récupère le handle vers la précédente fenêtre
                    hPid = Marshal.ReadInt32(pMem, 0)
                    If hPid <> 0 Then
                        '# On active l'instance précedente
                        Try
                            AppActivate(hPid)
                        Catch ex As Exception
                            Misc.ShowDebugError(ex)
                        End Try
                    End If
                    NativeFunctions.UnmapViewOfFile(pMem)
                End If
                '# On libère le handle hmap
                NativeFunctions.CloseHandle(hMap)
                '# et on prévient l'appelant que l'application avait dejà été lancée.
                Return True
            Else
                '# Nous sommes dans la première instance de l'application.
                '# Nous allons laisser une marque en mémoire, pour l'indiquer
                hMap = NativeFunctions.CreateFileMapping(NativeConstants.InvalidHandleValue, IntPtr.Zero, NativeEnums.FileMapProtection.PageReadWrite, 0, 4, FILE_NAME)
                If hMap.IsNotNull Then
                    '# On ouvre le 'fichier' en écriture
                    pMem = NativeFunctions.MapViewOfFile(hMap, NativeEnums.FileMapAccess.FileMapWrite, 0, 0, 0)
                    If pMem.IsNotNull Then
                        '# On y écrit l'ID du process courant
                        Marshal.WriteInt32(pMem, 0, NativeFunctions.GetCurrentProcessId)
                        NativeFunctions.UnmapViewOfFile(pMem)
                    End If
                    '# Pas de CloseHandle hMap ici, sous peine de détruire le mappage lui-même...
                End If
            End If

            Return False
        End Get
    End Property

    Public Shared ReadOnly Property IsAdmin() As Boolean
        Get
            Return My.User.IsAuthenticated AndAlso My.User.IsInRole(ApplicationServices.BuiltInRole.Administrator)
        End Get
    End Property

    Public Shared ReadOnly Property Is32Bits() As Boolean
        Get
            Return System.Runtime.InteropServices.Marshal.SizeOf(IntPtr.Zero) = 4
        End Get
    End Property

    Public Shared ReadOnly Property IsFramework2OrAbove() As Boolean
        Get
            Return Environment.Version.Major >= 2
        End Get
    End Property

    Public Shared ReadOnly Property IsWindowsVistaOrAbove() As Boolean
        Get
            Return (Environment.OSVersion.Platform = PlatformID.Win32NT) And (Environment.OSVersion.Version.Major >= 6)
        End Get
    End Property

    Public Shared ReadOnly Property IsWindows7() As Boolean
        Get
            Return (Environment.OSVersion.Platform = PlatformID.Win32NT) And (Environment.OSVersion.Version.Major = 6 And Environment.OSVersion.Version.Minor = 1)
        End Get
    End Property


#Region "SupportsXXX properties"

    Public Shared ReadOnly Property SupportsTaskDialog() As Boolean
        Get
            Return IsWindowsVistaOrAbove
        End Get
    End Property

    Public Shared ReadOnly Property SupportsUac() As Boolean
        Get
            Return IsWindowsVistaOrAbove
        End Get
    End Property

    Public Shared ReadOnly Property SupportsGetNextThreadProcessFunctions() As Boolean
        Get
            Return IsWindowsVistaOrAbove
        End Get
    End Property

    Public Shared ReadOnly Property SupportsGetThreadIdFunction() As Boolean
        Get
            Return IsWindowsVistaOrAbove
        End Get
    End Property

    Public Shared ReadOnly Property SupportsQueryFullProcessImageNameFunction() As Boolean
        Get
            Return IsWindowsVistaOrAbove
        End Get
    End Property

    Public Shared ReadOnly Property SupportsMinRights() As Boolean
        Get
            Return IsWindowsVistaOrAbove
        End Get
    End Property

#End Region

    ' Retrieve elevation type
    Public Shared ReadOnly Property GetElevationType() As Enums.ElevationType
        Get
            Static retrieved As Boolean = False
            Static valRetrieved As Enums.ElevationType

            If retrieved Then
                Return valRetrieved
            Else

                Dim hTok As IntPtr
                Dim hProc As IntPtr = Native.Objects.Process.GetProcessHandleById(Process.GetCurrentProcess.Id, _
                                                                                  Native.Security.ProcessAccess.QueryInformation)
                If hProc.IsNotNull Then
                    ' ?
                End If
                Call NativeFunctions.OpenProcessToken(hProc, Native.Security.TokenAccess.Query, hTok)
                NativeFunctions.CloseHandle(hProc)

                Dim value As Integer
                Dim ret As Integer

                ' Get tokeninfo length
                NativeFunctions.GetTokenInformation(hTok, _
                                                    NativeEnums.TokenInformationClass.TokenElevationType, _
                                                    IntPtr.Zero, _
                                                    0, _
                                                    ret)
                Using memAlloc As New Native.Memory.MemoryAlloc(ret)
                    ' Get token information
                    NativeFunctions.GetTokenInformation(hTok, _
                                                        NativeEnums.TokenInformationClass.TokenElevationType, _
                                                        memAlloc, _
                                                        memAlloc.Size, _
                                                        0)
                    ' Get a valid structure
                    value = memAlloc.ReadInt32(0)
                    valRetrieved = CType(value, Enums.ElevationType)

                    NativeFunctions.CloseHandle(hTok)

                    If valRetrieved = Enums.ElevationType.Default Then
                        If cEnvironment.IsAdmin = False Then
                            valRetrieved = Enums.ElevationType.Limited
                        Else
                            valRetrieved = Enums.ElevationType.Full
                        End If
                    End If

                    retrieved = True

                    Return valRetrieved

                End Using
            End If
        End Get
    End Property

    ' Has LitePM SeDebugPrivilege ?
    Public Shared Function HasLitePMDebugPrivilege() As Boolean
        Dim res As NativeEnums.SePrivilegeAttributes
        Native.Objects.Token.GetPrivilegeStatusByProcessId(Process.GetCurrentProcess.Id, "SeDebugPrivilege", res)
        Return (res = NativeEnums.SePrivilegeAttributes.Enabled)
    End Function

    ' Request a privilege
    Public Shared Sub RequestPrivilege(ByVal privilege As PrivilegeToRequest)
        'TOCHANGE : should be more generic
        Select Case privilege
            Case PrivilegeToRequest.DebugPrivilege
                Native.Objects.Token.SetPrivilegeStatusByProcessId(System.Diagnostics.Process.GetCurrentProcess.Id, "SeDebugPrivilege", NativeEnums.SePrivilegeAttributes.Enabled)
            Case PrivilegeToRequest.ShutdownPrivilege
                Native.Objects.Token.SetPrivilegeStatusByProcessId(System.Diagnostics.Process.GetCurrentProcess.Id, "SeShutdownPrivilege", NativeEnums.SePrivilegeAttributes.Enabled)
        End Select
    End Sub

    ' Restart LitePM elevated
    Public Shared Sub RestartElevated()

        Dim startInfo As New NativeStructs.ShellExecuteInfo
        With startInfo
            .cbSize = System.Runtime.InteropServices.Marshal.SizeOf(startInfo)
            .hwnd = _frmMain.Handle
            .lpFile = Application.ExecutablePath
            .lpParameters = PARAM_DO_NOT_CHECK_PREV_INSTANCE
            .lpVerb = "runas"
            .nShow = NativeEnums.ShowWindowType.ShowNormal
        End With

        Try
            If NativeFunctions.ShellExecuteEx(startInfo) Then
                ' Then the new process has started -> 
                '   - we hide tray icon
                '   - we brutaly terminate this instance
                '   - new instance will start
                _frmMain.Tray.Visible = False
                NativeFunctions.ExitProcess(0)
            End If
        Catch ex As Exception
            Misc.ShowDebugError(ex)
        End Try

    End Sub


    ' Return a bitmap containing the UAC shield.
    ' Got this code here : http://www.vb-helper.com/howto_2008_uac_shield.html
    ' But this is not the best way to do it...
    Public Shared Sub AddShieldToButton(ByVal btn As Button)
        Const BCM_SETSHIELD As Int32 = &H160C

        btn.FlatStyle = Windows.Forms.FlatStyle.System
        NativeFunctions.SendMessage(btn.Handle, BCM_SETSHIELD, IntPtr.Zero, NativeConstants.InvalidHandleValue)
    End Sub
    Public Shared Function GetUacShieldImage() As Bitmap
        Static shield_bm As Bitmap = Nothing
        If shield_bm Is Nothing Then
            Const WID As Integer = 50
            Const HGT As Integer = 50
            Const MARGIN As Integer = 4

            ' Make the button. For some reason, it must
            ' have text or the UAC shield won't appear.
            Dim btn As New Button
            btn.Text = " "
            btn.Size = New System.Drawing.Size(WID, HGT)
            AddShieldToButton(btn)

            ' Draw the button onto a bitmap.
            Dim bm As New Bitmap(WID, HGT)
            btn.Refresh()
            btn.DrawToBitmap(bm, New Rectangle(0, 0, WID, HGT))

            ' Find the part containing the shield.
            Dim min_x As Integer = WID
            Dim max_x As Integer = 0
            Dim min_y As Integer = WID
            Dim max_y As Integer = 0

            ' Fill on the left.
            For y As Integer = MARGIN To HGT - MARGIN - 1
                ' Get the leftmost pixel's color.
                Dim target_color As Color = bm.GetPixel(MARGIN, _
                    y)

                ' Fill in with this color as long as we see the
                ' target.
                For x As Integer = MARGIN To WID - MARGIN - 1
                    ' See if this pixel is part of the shield.
                    If bm.GetPixel(x, y).Equals(target_color) _
                        Then
                        ' It's not part of the shield.
                        ' Clear the pixel.
                        bm.SetPixel(x, y, Color.Transparent)
                    Else
                        ' It's part of the shield.
                        If min_y > y Then min_y = y
                        If min_x > x Then min_x = x
                        If max_y < y Then max_y = y
                        If max_x < x Then max_x = x
                    End If
                Next x
            Next y

            ' Clip out the shield part.
            Dim shield_wid As Integer = max_x - min_x + 1
            Dim shield_hgt As Integer = max_y - min_y + 1
            shield_bm = New Bitmap(shield_wid, shield_hgt)
            Dim shield_gr As Graphics = _
                Graphics.FromImage(shield_bm)
            shield_gr.DrawImage(bm, 0, 0, _
                New Rectangle(min_x, min_y, shield_wid, _
                    shield_hgt), _
                GraphicsUnit.Pixel)
        End If

        Return shield_bm
    End Function


End Class
