﻿
' Lite Process Monitor



Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Win32
Imports System.Windows.Forms


Namespace Common

    Public Class Misc




        ' Private constants

        Private Shared sizeUnits() As String = {"Bytes", "KB", "MB", "GB", "TB", "PB", "EB"}



        ' Private attributes


        ' Contains devices (logical drives) and their corresponding path
        ' e.g. :        /Device/Harddisk1/...       , C:
        Private Shared _DicoLogicalDrivesNames As New Dictionary(Of String, String)



        ' Public properties

        Public Shared ReadOnly Property NON_BLACK_COLOR() As System.Drawing.Color
            Get
                Return System.Drawing.Color.FromArgb(30, 30, 30)
            End Get
        End Property



        ' Other public

        Public Enum SecurityRisk As Integer
            Unknow = -1
            Safe = 0
            Caution1 = 1
            Caution2 = 2
            Alert1 = 3
            Alert2 = 4
            Alert3 = 4
        End Enum

        Public Structure InternetProcessInfo
            Dim _Description As String
            Dim _Risk As SecurityRisk
        End Structure




        ' Public functions


        ' Return a path without double-slashs
        Public Shared Function FormatPathWithoutDoubleSlashs(ByVal path As String) As String
            If path IsNot Nothing Then
                Return path.Replace("\\", "\")
            Else
                Return Nothing
            End If
        End Function

        ' Return a path with double-slashs
        Public Shared Function FormatPathWithDoubleSlashs(ByVal path As String) As String
            If path IsNot Nothing Then
                Return path.Replace("\", "\\")
            Else
                Return Nothing
            End If
        End Function


        ' Get a formated value as a string (in Bytes, KB, MB or GB) from an Integer
        Public Shared Function GetFormatedSize(ByVal size As Double, Optional ByVal digits As Integer = 3, Optional ByVal forceZeo As Boolean = False) As String
            Return GetFormatedSize(New Decimal(size), digits)
        End Function
        Public Shared Function GetFormatedSize(ByVal size As Integer, Optional ByVal digits As Integer = 3, Optional ByVal forceZeo As Boolean = False) As String
            Return GetFormatedSize(New Decimal(size), digits)
        End Function
        Public Shared Function GetFormatedSize(ByVal size As ULong, Optional ByVal digits As Integer = 3, Optional ByVal forceZeo As Boolean = False) As String
            Return GetFormatedSize(New Decimal(size), digits)
        End Function
        Public Shared Function GetFormatedSize(ByVal size As IntPtr, Optional ByVal digits As Integer = 3, Optional ByVal forceZeo As Boolean = False) As String
            Return GetFormatedSize(New Decimal(size.ToInt64), digits)
        End Function
        Public Shared Function GetFormatedSize(ByVal size As UInteger, Optional ByVal digits As Integer = 3, Optional ByVal forceZeo As Boolean = False) As String
            Return GetFormatedSize(New Decimal(size), digits)
        End Function
        Public Shared Function GetFormatedSize(ByVal size As Decimal, Optional ByVal digits As Integer = 3, Optional ByVal forceZeo As Boolean = False) As String
            Dim t As Decimal = size
            Dim dep As Integer = 0

            While t >= 1024
                t /= 1024
                dep += 1
            End While

            Dim d As Double = Math.Round(t, digits)

            If d > 0 Then
                Return d.ToString & " " & sizeUnits(dep)
            Else
                If forceZeo Then
                    Return "0 " & sizeUnits(0)
                Else
                    Return ""
                End If
            End If

        End Function
        Public Shared Function GetSizeFromFormatedSize(ByVal _frmtSize As String) As Long
            ' Get position of space
            If _frmtSize Is Nothing OrElse _frmtSize.Length < 4 Then
                Return 0
            End If

            Dim x As Integer = -1
            For Each _unit As String In sizeUnits
                x += 1
                Dim i As Integer = InStrRev(_frmtSize, " " & _unit)
                If i > 0 Then
                    Dim z As String = _frmtSize.Substring(0, i - 1)
                    Return CLng(Double.Parse(z, Globalization.NumberStyles.Float) * 1024 ^ x)
                End If
            Next

        End Function

        ' Get formated size per second
        Public Shared Function GetFormatedSizePerSecond(ByVal size As Double, Optional ByVal digits As Integer = 3, Optional ByVal forceZeroDisplay As Boolean = False) As String
            Return GetFormatedSizePerSecond(New Decimal(size), digits, forceZeroDisplay)
        End Function
        Public Shared Function GetFormatedSizePerSecond(ByVal size As Decimal, Optional ByVal digits As Integer = 3, Optional ByVal forceZeroDisplay As Boolean = False) As String
            Dim t As Decimal = size
            Dim dep As Integer = 0

            While t >= 1024
                t /= 1024
                dep += 1
            End While

            If forceZeroDisplay OrElse size > 0 Then
                Return CStr(Math.Round(t, digits)) & " " & sizeUnits(dep) & "/s"
            Else
                Return ""
            End If

        End Function

        ' Return true if a string is (or seems to be) a formated size
        Public Shared Function IsFormatedSize(ByVal _str As String) As Boolean
            If _str Is Nothing OrElse _str.Length < 4 Then
                Return False
            End If
            ' Try to find " UNIT" in _str
            ' Return true if first char is a numeric value
            For Each _unit As String In sizeUnits
                Dim i As Integer = InStrRev(_str, " " & _unit)
                If i > 0 AndAlso (i + _unit.Length) = _str.Length Then
                    Return IsNumeric(_str.Substring(0, 1))
                End If
            Next
            Return False
        End Function

        ' Return true if a string is (or seems to be) a hexadecimal expression
        Public Shared Function IsHex(ByVal _str As String) As Boolean
            If _str Is Nothing OrElse _str.Length < 4 Then
                Return False
            End If
            Return (_str.Substring(0, 2) = "0x")
        End Function

        ' Return long value from hex value
        Public Shared Function HexToLong(ByVal _str As String) As Long
            If _str Is Nothing OrElse _str.Length < 4 Then
                Return 0
            End If
            Return Long.Parse(_str.Substring(2, _str.Length - 2),
                              Globalization.NumberStyles.AllowHexSpecifier)
        End Function

        ' Get a formated percentage
        Public Shared Function GetFormatedPercentage(ByVal p As Double, Optional ByVal digits As Integer = 3, Optional ByVal forceZeroDisplay As Boolean = False) As String
            If forceZeroDisplay OrElse p > 0 Then
                Dim d100 As Double = p * 100
                Dim d As Double = Math.Round(d100, digits)
                Dim tr As Double = Math.Truncate(d)
                Dim lp As Integer = CInt(tr)
                Dim rp As Integer = CInt((d100 - tr) * 10 ^ digits)

                Return CStr(IIf(lp < 10, "0", "")) & CStr(lp) & "." & CStr(IIf(rp < 10, "00", "")) & CStr(IIf(rp < 100 And rp >= 10, "0", "")) & CStr(rp)
            Else
                Return ""
            End If
        End Function

        ' Convert a DMTF datetime to a valid Date
        Public Shared Function DMTFDateToDateTime(ByVal dmtfDate As String) As Date
            Try
                Dim initializer As Date = Date.MinValue
                Dim year As Integer = initializer.Year
                Dim month As Integer = initializer.Month
                Dim day As Integer = initializer.Day
                Dim hour As Integer = initializer.Hour
                Dim minute As Integer = initializer.Minute
                Dim second As Integer = initializer.Second
                Dim ticks As Long = 0
                Dim dmtf As String = dmtfDate
                Dim datetime As Date = Date.MinValue
                Dim tempString As String = String.Empty
                tempString = dmtf.Substring(0, 4)
                If ("****" <> tempString) Then
                    year = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(4, 2)
                If ("**" <> tempString) Then
                    month = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(6, 2)
                If ("**" <> tempString) Then
                    day = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(8, 2)
                If ("**" <> tempString) Then
                    hour = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(10, 2)
                If ("**" <> tempString) Then
                    minute = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(12, 2)
                If ("**" <> tempString) Then
                    second = Integer.Parse(tempString)
                End If
                tempString = dmtf.Substring(15, 6)
                If ("******" <> tempString) Then
                    ticks = (Long.Parse(tempString) * CType((System.TimeSpan.TicksPerMillisecond / 1000), Long))
                End If

                datetime = New Date(year, month, day, hour, minute, second, 0)
                datetime = datetime.AddTicks(ticks)
                Dim tickOffset As System.TimeSpan = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime)
                Dim UTCOffset As Integer = 0
                Dim OffsetToBeAdjusted As Integer = 0
                Dim OffsetMins As Long = CType((tickOffset.Ticks / System.TimeSpan.TicksPerMinute), Long)
                tempString = dmtf.Substring(22, 3)
                If (tempString <> "******") Then
                    tempString = dmtf.Substring(21, 4)
                    UTCOffset = Integer.Parse(tempString)
                    OffsetToBeAdjusted = CType((OffsetMins - UTCOffset), Integer)
                    datetime = datetime.AddMinutes(CType(OffsetToBeAdjusted, Double))
                End If
                Return datetime
            Catch ex As Exception
                Return New Date(0)
            End Try
        End Function

        Public Shared Function ReadUnicodeString(ByVal str As Native.Api.NativeStructs.UnicodeString) As String
            If str.Length = 0 Then
                Return Nothing
            End If
            Try
                Return System.Runtime.InteropServices.Marshal.PtrToStringUni(str.Buffer, str.Length \ 2)
            Catch ex As Exception
                Return ""
            End Try
        End Function

        ' Expand the first node of a tv which has a specific text
        Public Shared Sub ShowTvNodeByText(ByVal tv As TreeView, ByVal text As String)
            For Each n As TreeNode In tv.Nodes
                If pvtExpandTvNodeByText(n, text, tv) Then
                    Exit For
                End If
            Next
        End Sub
        Private Shared Function pvtExpandTvNodeByText(ByVal node As TreeNode, ByVal text As String, ByVal tv As TreeView) As Boolean
            If node.Nodes.Count > 0 Then
                For Each n As TreeNode In node.Nodes
                    If pvtExpandTvNodeByText(n, text, tv) Then
                        Exit For
                    End If
                Next
            Else
                Dim ret As Boolean = (node.Text = text)
                If ret Then
                    node.EnsureVisible()
                    tv.SelectedNode = node
                    tv.Focus()
                End If
                Return ret
            End If
        End Function



        ' Get a good path
        Public Shared Function GetRealPath(ByVal path As String) As String
            If Len(path) > 0 Then
                If path.ToLowerInvariant.StartsWith("\systemroot\") OrElse path.ToLowerInvariant.StartsWith("system32\") Then
                    path = path.Substring(12, path.Length - 12)
                    Dim ii As Integer = InStr(path, "\", CompareMethod.Binary)
                    If ii > 0 Then
                        path = path.Substring(ii, path.Length - ii)
                        path = Environment.SystemDirectory & "\" & path
                    End If
                ElseIf path.ToLowerInvariant.StartsWith("\windows\") OrElse path.ToLowerInvariant.StartsWith("system32\") Then
                    path = path.Substring(9, path.Length - 9)
                    Dim ii As Integer = InStr(path, "\", CompareMethod.Binary)
                    If ii > 0 Then
                        path = path.Substring(ii, path.Length - ii)
                        path = Environment.SystemDirectory & "\" & path
                    End If
                ElseIf path.StartsWith("\??\") Then
                    path = path.Substring(4)
                ElseIf path.StartsWith(Char.ConvertFromUtf32(34)) Then
                    If path.Length > 2 Then
                        path = path.Substring(1, path.Length - 2)
                    End If
                End If
            End If
            Return path
        End Function

        ' Get path from a command
        Public Shared Function GetPathFromCommand(ByVal path As String) As String
            If IO.File.Exists(path) Then
                Return path
            Else
                Return cFile.IntelligentPathRetrieving2(path)
            End If
        End Function

        ' Get a file name from a path
        Public Shared Function GetFileName(ByVal _path As String) As String
            Dim i As Integer = InStrRev(_path, "\", , CompareMethod.Binary)
            If i > 0 Then
                Return Right(_path, _path.Length - i)
            Else
                Return vbNullString
            End If
        End Function

        ' Permute bytes
        Public Shared Function PermuteBytes(ByVal v As Integer) As UShort
            Dim b1 As Byte = CByte(v)
            Dim b2 As Byte = CByte((v >> 8))

            Return CType((b2 + (CUShort(b1) << 8)), UShort)
        End Function

        ' Return a Uinteger from a IPEndPoint
        Public Shared Function GetAddressAsInteger(ByVal ip As System.Net.IPEndPoint) As UInt32
            Dim i As Integer = 0
            Dim addressInteger As UInt32 = 0

            For Each b As Byte In ip.Address.GetAddressBytes()
                addressInteger += CType(CInt(b) << (8 * i), UInt32)
                i += 1
            Next

            Return addressInteger

        End Function

        ' Escape will close the form frm
        Public Shared Sub CloseWithEchapKey(ByRef frm As System.Windows.Forms.Form)
            frm.KeyPreview = True
            Dim oo As New System.Windows.Forms.KeyEventHandler(AddressOf handlerCloseForm_)
            AddHandler frm.KeyDown, oo
        End Sub

        ' Escape will hide the form frm
        Public Shared Sub HideWithEchapKey(ByRef frm As System.Windows.Forms.Form)
            frm.KeyPreview = True
            Dim oo As New System.Windows.Forms.KeyEventHandler(AddressOf handlerHideForm_)
            AddHandler frm.KeyDown, oo
        End Sub

        ' Copy content of a listview (selected items) into clipboard
        Public Shared Sub CopyLvToClip(ByVal e As MouseEventArgs, ByVal lv As ListView)
            If e.Button = Windows.Forms.MouseButtons.Middle Then
                Dim s As String = vbNullString
                Dim it As ListViewItem
                Dim x As Integer = 0
                For Each it In lv.SelectedItems
                    s &= it.Text
                    Dim it2 As ListViewItem.ListViewSubItem
                    For Each it2 In it.SubItems
                        s &= vbTab & vbTab & it2.Text
                    Next
                    x += 1
                    If Not (x = lv.SelectedItems.Count) Then s &= vbNewLine
                Next
                If String.IsNullOrEmpty(s) = False Then
                    My.Computer.Clipboard.SetText(s, TextDataFormat.Text)
                End If
            End If
        End Sub

        ' Start (or not) with windows startup
        Public Shared Sub StartWithWindows(ByVal value As Boolean)
            Try
                Dim regKey As RegistryKey
                regKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)

                If value Then
                    regKey.SetValue(Application.ProductName, Application.ExecutablePath)
                Else
                    If regKey.GetValue(Application.ProductName) IsNot Nothing Then
                        regKey.DeleteValue(Application.ProductName)
                    End If
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End Sub

        ' Replace (or not) taskmgr
        Public Shared Sub ReplaceTaskmgr(ByVal value As Boolean)
            Try
                Dim regKey As RegistryKey
                regKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options", True)

                If value Then
                    Try
                        regKey.CreateSubKey("taskmgr.exe").SetValue("debugger", Application.ExecutablePath)
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try
                Else
                    Try
                        If regKey.OpenSubKey("taskmgr.exe", False) IsNot Nothing Then
                            regKey.DeleteSubKey("taskmgr.exe")
                        End If
                    Catch ex As Exception
                        Misc.ShowDebugError(ex)
                    End Try
                End If
            Catch ex As Exception
                Misc.ShowDebugError(ex)
            End Try
        End Sub

        ' Set tool tips
        Public Shared Sub SetToolTip(ByVal ctrl As Control, ByVal text As String)
            Dim tToolTip As ToolTip = New System.Windows.Forms.ToolTip
            With tToolTip
                .SetToolTip(ctrl, text)
                .IsBalloon = False
                .Active = True
            End With
        End Sub

        ' Custom input box
        Public Shared Function CInputBox(ByVal text As String, ByVal title As String, Optional ByVal defaultValue As String = Nothing) As String
            Dim frm As New frmInput
            With frm
                .Text = title
                .lblMessage.Text = text
                .txtRes.Text = defaultValue
                .TopMost = _frmMain.TopMost
                .ShowDialog()
                Return .Result
            End With

        End Function

        ' Search on internet
        Public Shared Sub SearchInternet(ByVal item As String, ByVal handle As IntPtr)
            cFile.ShellOpenFile(My.Settings.SearchEngine.Replace("ITEM", item), handle)
        End Sub


        ' Get available IPV-4 IP
        Public Shared Function GetIpv4Ips() As String()
            Dim res() As String
            Dim x As Integer = -1
            ReDim res(x)
            Dim s() As System.Net.IPAddress = System.Net.Dns.GetHostAddresses(My.Computer.Name)
            For Each t As System.Net.IPAddress In s
                If t.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                    x += 1
                    ReDim Preserve res(x)
                    res(x) = t.ToString
                End If
            Next
            Return res
        End Function

        ' Return Assembly GUID
        ' Could be used ??
        Public Shared Function GetAppGuid() As String
            Dim assemblyGuid As Guid = Nothing
            Dim assemblyObjects As Object() = System.Reflection.Assembly.GetEntryAssembly().GetCustomAttributes(GetType(System.Runtime.InteropServices.GuidAttribute), True)
            If assemblyObjects.Length > 0 Then
                assemblyGuid = New Guid(DirectCast(assemblyObjects(0),
                                        System.Runtime.InteropServices.GuidAttribute).Value)
            End If
            Return assemblyGuid.ToString
        End Function

        ' Return informations about installed netword card interfaces
        Public Shared Function GetNics() As List(Of Native.Api.Structs.NicDescription)
            Dim ret As New List(Of Native.Api.Structs.NicDescription)
            For Each nic As System.Net.NetworkInformation.NetworkInterface In System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                If cEnvironment.IsWindowsVistaOrAbove Then
                    If nic.GetIPProperties.UnicastAddresses.Count > 1 Then
                        If nic.GetIPProperties.UnicastAddresses(1).Address.ToString <> "127.0.0.1" Then
                            ret.Add(New Native.Api.Structs.NicDescription(nic.Name, nic.Description, nic.GetIPProperties.UnicastAddresses(1).Address.ToString))
                        End If
                    End If
                Else
                    If nic.GetIPProperties.UnicastAddresses.Count > 0 Then
                        If nic.GetIPProperties.UnicastAddresses(0).Address.ToString <> "127.0.0.1" Then
                            ret.Add(New Native.Api.Structs.NicDescription(nic.Name, nic.Description, nic.GetIPProperties.UnicastAddresses(0).Address.ToString))
                        End If
                    End If
                End If
            Next
            Return ret
        End Function

        ' Navigate to regedit
        Public Shared Sub NavigateToRegedit(ByVal key As String)
            ' Write the path of the key into the registry, so regedit
            ' will use this value to show the key when it open
            Dim regKey As RegistryKey
            Dim myComputerLocalized As String = Nothing
            regKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Applets\Regedit\", True)

            ' Create key if it does not exists (happens when regedit has never been opened)
            If regKey Is Nothing Then
                regKey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Windows\CurrentVersion\Applets\Regedit\",
                                                  RegistryKeyPermissionCheck.ReadWriteSubTree)
                ' Have to find a way to set "My Computer" to this key value ("My Computer" should be localized)
                regKey.SetValue("LastKey", "")
            End If

            ' Format key name
            If key.ToLower.StartsWith("hklm\") Then
                key = "HKEY_LOCAL_MACHINE\" & key.Substring(5, key.Length - 5)
            End If
            If key.ToLower.StartsWith("hku\") Then
                key = "HKEY_USERS\" & key.Substring(4, key.Length - 4)
            End If
            If key.ToLower.StartsWith("hkcu\") Then
                key = "HKEY_CURRENT_USER\" & key.Substring(5, key.Length - 5)
            End If
            If key.ToLower.StartsWith("hkcr\") Then
                key = "HKEY_CLASSES_ROOT\" & key.Substring(5, key.Length - 5)
            End If
            If key.ToLower.StartsWith("hkcc\") Then
                key = "HKEY_CURRENT_CONFIG\" & key.Substring(5, key.Length - 5)
            End If
            If key.ToLower.StartsWith("hkpd\") Then
                key = "HKEY_PERFORMANCE_DATA\" & key.Substring(5, key.Length - 5)
            End If

            If regKey IsNot Nothing Then

                ' Retrieve 'My Computer' translated into computer culture
                ' We simply read current value of LastKey key
                myComputerLocalized = CStr(regKey.GetValue("LastKey"))

                Dim i As Integer = InStr(myComputerLocalized, "\")
                If i > 0 Then
                    myComputerLocalized = myComputerLocalized.Substring(0, i - 1)
                End If

                Try
                    regKey.SetValue("Lastkey", myComputerLocalized & "\" & key)
                    regKey.Close()
                    Shell("regedit.exe", AppWinStyle.NormalFocus)
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
            End If
        End Sub

        ' Move a listview item (up or down)
        ' Come from here : http://www.knowdotnet.com/articles/listviewmoveitem.html
        Public Shared Sub MoveListViewItem(ByVal lv As ListView, ByVal moveUp As Boolean)
            Dim i As Integer
            Dim cache As String
            Dim cacheColor As System.Drawing.Color
            Dim cacheSel As Boolean
            Dim selIdx As Integer

            With lv
                selIdx = .SelectedItems.Item(0).Index
                If moveUp Then
                    ' ignore moveup of row(0)
                    If selIdx = 0 Then
                        Exit Sub
                    End If
                    ' move the subitems for the previous row
                    ' to cache so we can move the selected row up
                    cacheSel = .Items(selIdx - 1).Checked
                    cacheColor = .Items(selIdx - 1).BackColor
                    For i = 0 To .Items(selIdx).SubItems.Count - 1
                        cache = .Items(selIdx - 1).SubItems(i).Text
                        .Items(selIdx - 1).SubItems(i).Text = .Items(selIdx).SubItems(i).Text
                        .Items(selIdx).SubItems(i).Text = cache
                    Next
                    .Items(selIdx - 1).Checked = .Items(selIdx).Checked
                    .Items(selIdx - 1).BackColor = .Items(selIdx).BackColor
                    .Items(selIdx).Checked = cacheSel
                    .Items(selIdx).BackColor = cacheColor
                    .Items(selIdx - 1).Selected = True
                    .Refresh()
                    .Focus()
                Else
                    ' ignore move down of last row
                    If selIdx = .Items.Count - 1 Then
                        Exit Sub
                    End If
                    ' move the subitems for the next row
                    ' to cache so we can move the selected row down
                    cacheSel = .Items(selIdx + 1).Checked
                    cacheColor = .Items(selIdx + 1).BackColor
                    For i = 0 To .Items(selIdx).SubItems.Count - 1
                        cache = .Items(selIdx + 1).SubItems(i).Text
                        .Items(selIdx + 1).SubItems(i).Text = .Items(selIdx).SubItems(i).Text
                        .Items(selIdx).SubItems(i).Text = cache
                    Next
                    .Items(selIdx + 1).Checked = .Items(selIdx).Checked
                    .Items(selIdx + 1).BackColor = .Items(selIdx).BackColor
                    .Items(selIdx).Checked = cacheSel
                    .Items(selIdx).BackColor = cacheColor
                    .Items(selIdx + 1).Selected = True
                    .Refresh()
                    .Focus()
                End If
            End With
        End Sub

        Public Shared Function GetIcon(ByVal fName As String, Optional ByVal small As Boolean = True) _
                As System.Drawing.Icon

            Dim hImgSmall As Integer
            Dim hImgLarge As Integer
            Dim shinfo As Native.Api.NativeStructs.SHFileInfo
            shinfo = New Native.Api.NativeStructs.SHFileInfo()

            If System.IO.File.Exists(fName) = False Then Return Nothing

            If small Then
                hImgSmall = Native.Api.NativeFunctions.SHGetFileInfo(fName, 0, shinfo,
                    Marshal.SizeOf(shinfo),
                     Native.Api.NativeConstants.SHGFI_ICON Or Native.Api.NativeConstants.SHGFI_SMALLICON)
            Else
                hImgLarge = Native.Api.NativeFunctions.SHGetFileInfo(fName, 0,
                    shinfo, Marshal.SizeOf(shinfo),
                    Native.Api.NativeConstants.SHGFI_ICON Or Native.Api.NativeConstants.SHGFI_LARGEICON)
            End If

            Dim img As System.Drawing.Icon = Nothing
            Try
                If shinfo.hIcon.IsNotNull Then
                    img = System.Drawing.Icon.FromHandle(shinfo.hIcon)
                End If
            Catch ex As Exception
                ' Can't retrieve icon
            End Try

            Return img

        End Function

        Public Shared Function GetIcon2(ByVal fName As String, Optional ByVal small As Boolean = True) _
            As System.Drawing.Icon

            Dim hImgSmall As Integer
            Dim hImgLarge As Integer
            Dim shinfo As Native.Api.NativeStructs.SHFileInfo
            shinfo = New Native.Api.NativeStructs.SHFileInfo()

            If small Then
                hImgSmall = Native.Api.NativeFunctions.SHGetFileInfo(fName, 0, shinfo,
                    Marshal.SizeOf(shinfo),
                   Native.Api.NativeConstants.SHGFI_ICON Or Native.Api.NativeConstants.SHGFI_SMALLICON)
            Else
                hImgLarge = Native.Api.NativeFunctions.SHGetFileInfo(fName, 0,
                    shinfo, Marshal.SizeOf(shinfo),
                    Native.Api.NativeConstants.SHGFI_ICON Or Native.Api.NativeConstants.SHGFI_LARGEICON)
            End If

            Dim img As System.Drawing.Icon = Nothing
            Try
                If shinfo.hIcon.IsNotNull Then
                    img = System.Drawing.Icon.FromHandle(shinfo.hIcon)
                End If
            Catch ex As Exception
                ' Can't retrieve icon
            End Try

            Return img

        End Function

        ' Get security risk from internet
        Public Shared Function GetSecurityRisk(ByVal process As String) As SecurityRisk
            Dim ret As Integer = 0

            ' Download source page of
            ' http://www.processlibrary.com/directory/files/PROCESSS/
            ' and retrieve security risk from source :
            '<h4 class="red-heading">Security risk (0-5):</h4><p>0</p>

            Dim s As String
            s = DownloadPage("http://www.processlibrary.com/directory/files/" & LCase(process) & "/")

            Dim i As Integer = InStr(s, "Security risk (0-5)", CompareMethod.Binary)

            If i > 0 Then
                Dim z As String = s.Substring(i + 27, 1)
                ret = CInt(Val(z))
            Else
                ret = -1
            End If

            Return CType(ret, SecurityRisk)
        End Function

        ' Get security rick and description from internet
        Public Shared Function GetInternetInfos(ByVal process As String) As InternetProcessInfo
            Dim ret As InternetProcessInfo = Nothing

            ' Download source page of
            ' http://www.processlibrary.com/directory/files/PROCESSS/
            ' and retrieve security risk from source :
            '<h4 class="red-heading">Security risk (0-5):</h4><p>0</p>

            Dim s As String
            s = DownloadPage("http://www.processlibrary.com/directory/files/" & LCase(process) & "/")

            Dim i As Integer = InStr(s, "Security risk (0-5)", CompareMethod.Binary)
            Dim d1 As Integer = InStr(s, ">Description</h4>", CompareMethod.Binary)
            Dim d2 As Integer = InStr(d1 + 1, s, "</p>", CompareMethod.Binary)

            If i > 0 Then
                Dim z As String = s.Substring(i + 27, 1)
                ret._Risk = CType(CInt(Val(z)), SecurityRisk)
                If d1 > 0 And d2 > 0 Then
                    Dim z2 As String = s.Substring(d1 + 23, d2 - d1 - 24)
                    ret._Description = Replace(z2, "<BR><BR>", vbNewLine)
                Else
                    ret._Description = NO_INFO_RETRIEVED
                End If
            Else
                ret._Risk = SecurityRisk.Unknow
            End If

            Return ret
        End Function

        ' Download web page
        Public Shared Function DownloadPage(ByVal sURL As String) As String

            Try
                Dim objWebRequest As System.Net.WebRequest = System.Net.HttpWebRequest.Create(sURL)
                Dim objWebResponse As System.Net.WebResponse = objWebRequest.GetResponse()
                Dim objStreamReader As System.IO.StreamReader = Nothing
                Dim ret As String = vbNullString

                Try
                    objStreamReader = New System.IO.StreamReader(objWebResponse.GetResponseStream())
                    ret = objStreamReader.ReadToEnd()
                Finally
                    If Not objWebResponse Is Nothing Then
                        objWebResponse.Close()
                    End If
                End Try

                Return ret

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

        ' Display details of a file
        Public Shared Sub DisplayDetailsFile(ByVal file As String)
            _frmMain.txtFile.Text = file
            Call _frmMain.refreshFileInfos(file)
            _frmMain.Ribbon.ActiveTab = _frmMain.FileTab
            Call _frmMain.Ribbon_MouseMove(Nothing, Nothing)
        End Sub

        ' Return the column header which have DisplayIndex == nDispIndex
        Public Shared Function GetColumnHeaderByDisplayIndex(ByRef lv As customLV, ByVal nDispIndex As Integer) As ColumnHeader
            For i As Integer = lv.Columns.Count - 1 To 0 Step -1
                If lv.Columns(i).DisplayIndex = nDispIndex Then
                    Return lv.Columns(i)
                End If
            Next
            Return Nothing
        End Function

        ' Parse port text files
        Public Shared Function ParsePortTextFiles(ByVal tcpFile As String, ByVal udpFile As String,
                                           ByRef dicoTcp As Dictionary(Of Integer, String),
                                           ByRef dicoUdp As Dictionary(Of Integer, String)) As Boolean

            Try

                Dim sTcp() As String =
                    My.Resources.tcp.Split(CChar(vbNewLine))
                Dim sUdp() As String =
                    My.Resources.udp.Split(CChar(vbNewLine))

                ' TCP
                For Each s As String In sTcp

                    Dim p As Integer = s.IndexOf(vbTab)
                    Dim ports As String = s.Substring(0, p)
                    Dim desc As String = s.Substring(p + 1, s.Length - p - 1)

                    Dim i As Integer = ports.IndexOf("-")
                    If i > 0 Then
                        ' Then these are multiple ports
                        Dim s1 As Integer = Integer.Parse(ports.Substring(0, i))
                        Dim s2 As Integer = Integer.Parse(ports.Substring(i + 1, ports.Length - i - 1))
                        For u As Integer = s1 To s2
                            If dicoTcp.ContainsKey(u) = False Then
                                dicoTcp.Add(u, desc)
                            Else
                                dicoTcp.Item(u) = dicoTcp.Item(u) & " OR " & desc
                            End If
                        Next
                    Else
                        Dim port As Integer = Integer.Parse(ports)
                        If dicoTcp.ContainsKey(port) = False Then
                            dicoTcp.Add(port, desc)
                        Else
                            dicoTcp.Item(port) = dicoTcp.Item(port) & " OR " & desc
                        End If
                    End If
                Next


                ' UDP

                For Each s As String In sUdp

                    Dim p As Integer = s.IndexOf(vbTab)
                    Dim ports As String = s.Substring(0, p)
                    Dim desc As String = s.Substring(p + 1, s.Length - p - 1)

                    Dim i As Integer = ports.IndexOf("-")
                    If i > 0 Then
                        ' Then these are multiple ports
                        Dim s1 As Integer = Integer.Parse(ports.Substring(0, i))
                        Dim s2 As Integer = Integer.Parse(ports.Substring(i + 1, ports.Length - i - 1))
                        For u As Integer = s1 To s2
                            If dicoUdp.ContainsKey(u) = False Then
                                dicoUdp.Add(u, desc)
                            Else
                                dicoUdp.Item(u) = dicoUdp.Item(u) & " OR " & desc
                            End If
                        Next
                    Else
                        Dim port As Integer = Integer.Parse(ports)
                        If dicoUdp.ContainsKey(port) = False Then
                            dicoUdp.Add(port, desc)
                        Else
                            dicoUdp.Item(port) = dicoUdp.Item(port) & " OR " & desc
                        End If
                    End If
                Next

                Return True

            Catch ex As Exception
                Return False
            End Try

        End Function

        ' Return a well formated path
        Public Shared Function GetWellFormatedPath(ByVal path As String) As String
            Try
                If path IsNot Nothing Then
                    If path.ToUpperInvariant.StartsWith("\SYSTEMROOT\") Then
                        path = path.Substring(12, path.Length - 12)
                        Dim ii As Integer = InStr(path, "\", CompareMethod.Binary)
                        If ii > 0 Then
                            path = path.Substring(ii, path.Length - ii)
                            path = Environment.SystemDirectory & "\" & path
                        End If
                    ElseIf path.StartsWith("\??\") Then
                        path = path.Substring(4)
                    End If
                End If
                Return path
            Catch ex As Exception
                Return path
            End Try
        End Function

        ' Return dos drive name
        Public Shared Function DeviceDriveNameToDosDriveName(ByVal path As String) As String

            If path IsNot Nothing Then

                Dim res As String = Nothing
                Dim found As Boolean = False

                Dim _tempDico As Dictionary(Of String, String) = _DicoLogicalDrivesNames
                For Each pair As System.Collections.Generic.KeyValuePair(Of String, String) In _tempDico
                    If path.StartsWith(pair.Key) Then
                        res = pair.Value & path.Substring(pair.Key.Length)
                        found = True
                        Exit For
                    End If
                Next

                If found Then
                    Return res
                Else
                    Return path
                End If

            Else
                Return Nothing
            End If

        End Function

        ' Refresh the dictionnary of logical drives
        Public Shared Sub RefreshLogicalDrives()

            Dim _tempDico As New Dictionary(Of String, String)

            ' From 'A' to 'Z'
            ' It also possible to use GetLogicalDriveStringsA
            For c As Byte = 65 To 90
                Dim _badPath As New System.Text.StringBuilder(1024)

                If Native.Api.NativeFunctions.QueryDosDevice(Char.ConvertFromUtf32(c) & ":", _badPath, 1024) <> 0 Then
                    _tempDico.Add(_badPath.ToString(), Char.ConvertFromUtf32(c).ToString() & ":")
                End If
            Next

            _DicoLogicalDrivesNames = _tempDico
        End Sub

        ' Return a priority as an int from a string (System.Diagnostics.ProcessPriorityClass)
        Public Shared Function GetPriorityFromString(ByVal p As String) As System.Diagnostics.ProcessPriorityClass
            Return DirectCast([Enum].Parse(GetType(System.Diagnostics.ProcessPriorityClass), p), System.Diagnostics.ProcessPriorityClass)
        End Function

        ' Return byte array from securestring
        Public Shared Function SecureStringToCharArray(ByRef str As System.Security.SecureString) As Char()

            Dim bytes As Char() = New Char(str.Length - 1) {}
            Dim ptr As IntPtr = IntPtr.Zero

            Try
                ptr = Marshal.SecureStringToBSTR(str)
                bytes = New Char(str.Length - 1) {}
                Marshal.Copy(ptr, bytes, 0, str.Length)
            Finally
                If ptr <> IntPtr.Zero Then
                    Marshal.ZeroFreeBSTR(ptr)
                End If
            End Try

            Return bytes

        End Function

        ' Return True if a byte array is null or filled with empty values
        Public Shared Function IsByteArrayNullOrEmpty(ByRef bArray() As Byte) As Boolean
            If bArray Is Nothing Then
                Return True
            Else
                For Each b As Byte In bArray
                    If b <> 0 Then
                        Return False
                    End If
                Next
                Return True
            End If
        End Function

        ' General ShowMessage function
        Public Shared Function ShowMsg(message As String, Optional title As String = "Message", Optional buttons As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
            Return MessageBox.Show(message, title, buttons)
        End Function

        ' Dangerous action message
        Public Shared Function WarnDangerousAction(ByVal text As String, ByVal Owner As IntPtr) As DialogResult
            If My.Settings.WarnDangerousActions Then
                Return ShowMsg(text, "Warning", MessageBoxButtons.YesNo)
            Else
                Return DialogResult.Yes
            End If
        End Function

        ' Show Vista Message
        Public Shared Function ShowVistaMessage(ByVal text As String, ByVal title As String, ByVal buttons As MessageBoxButtons, ByVal icon As MessageBoxIcon) As DialogResult
            Return MessageBox.Show(text, title, buttons, icon)
        End Function

        ' Show an error message
        Public Shared Sub ShowError(ByVal ex As Exception, ByVal msg As String, Optional ByVal forceClassical As Boolean = False)
            Dim t0 As New cError(msg, ex)
            t0.ShowMessage(forceClassical)
        End Sub
        Public Shared Sub ShowError(ByVal msg As String, Optional ByVal forceClassical As Boolean = False)
            Dim t0 As New cError(msg)
            t0.ShowMessage(forceClassical)
        End Sub

        ' Show an error message as a debug message (now messagebox)
        Public Shared Sub ShowDebugError(ByVal ex As Exception)
            Debug.WriteLine("=")
            Debug.WriteLine("GOT AN ERROR")
            Debug.WriteLine("       Source : " & ex.Source)
            Debug.WriteLine("       Message : " & ex.Message)
            Debug.WriteLine("       Trace : " & ex.StackTrace)
            Debug.WriteLine("=")

            ' Write error to log (if enabled)
            If My.Settings.SaveErrorLog Then
                Dim stream As StreamWriter = Nothing
                Try
                    stream = New StreamWriter(Program.LogPath, True)
                    stream.WriteLine("=====")
                    stream.WriteLine("Got an error, date = " & Date.Now.ToLongDateString & " - " & Date.Now.ToLongTimeString)
                    stream.Write(Program.ErrorLog(ex) & vbNewLine)
                    stream.WriteLine("=====")
                    stream.WriteLine()
                    stream.WriteLine()
                Catch ex2 As Exception
                    ' Won't catch this...
                Finally
                    If stream IsNot Nothing Then
                        stream.Close()
                    End If
                End Try
            End If

            ' Beep for debug mode :-)
#If RELEASE_MODE = 0 Then
            Beep()
#End If
        End Sub




        ' Private functions


        Private Shared Sub handlerCloseForm_(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.KeyCode = System.Windows.Forms.Keys.Escape Then
                Try
                    Dim _tmp As System.Windows.Forms.Form = DirectCast(sender, System.Windows.Forms.Form)
                    _tmp.DialogResult = Windows.Forms.DialogResult.Cancel
                    _tmp.Close()
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
                e.Handled = True
            End If
        End Sub

        Private Shared Sub handlerHideForm_(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
            If e.KeyCode = System.Windows.Forms.Keys.Escape Then
                Try
                    Dim _tmp As System.Windows.Forms.Form = DirectCast(sender, System.Windows.Forms.Form)
                    _tmp.Hide()
                Catch ex As Exception
                    Misc.ShowDebugError(ex)
                End Try
                e.Handled = True
            End If
        End Sub

    End Class

End Namespace
