
' Lite Process Monitor

' Light or dark display. "auto" follows the Windows mode of the applications
' (Settings > Personalization > Colors), "light" and "dark" force it; the
' LITEPM_THEME environment variable overrides the choice (tests). Theme.Apply is
' called for every window by LocalizedForm, after Localization.Apply: dark
' colors for the window and its controls, dark title bar, scroll bars and
' popup menus, lists and tabs drawn here, accent color for the selection in
' tool bars. In light mode the system colors are kept.

Option Strict On

Imports System.Drawing
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Microsoft.Win32

Public NotInheritable Class Theme

    Public Const Automatic As String = "auto"
    Public Const Light As String = "light"
    Public Const Dark As String = "dark"

    Private Shared _dark As Boolean?
    Private Shared _accent As Color?
    Private Shared _preference As String = Automatic

    Private Sub New()
    End Sub

    ' "auto", "light" or "dark" (My.Settings.Theme); call before the first window is shown
    Public Shared Sub Initialize(ByVal preference As String)
        _preference = If(String.IsNullOrEmpty(preference), Automatic, preference)
        _dark = Nothing
        _accent = Nothing
        If IsDark Then DarkPopupMenus()
    End Sub

    Public Shared ReadOnly Property IsDark() As Boolean
        Get
            If Not _dark.HasValue Then
                Dim forced As String = Environment.GetEnvironmentVariable("LITEPM_THEME")
                If String.IsNullOrEmpty(forced) Then forced = _preference
                If forced = Dark Then
                    _dark = True
                ElseIf forced = Light Then
                    _dark = False
                Else
                    Dim light As Integer? = ReadDword("Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme")
                    _dark = light.HasValue AndAlso light.Value = 0
                End If
            End If
            Return _dark.Value
        End Get
    End Property

    ' Accent color of Windows (DWM), else the selection color of the system
    Public Shared ReadOnly Property Accent() As Color
        Get
            If Not _accent.HasValue Then
                Dim abgr As Integer? = ReadDword("Software\Microsoft\Windows\DWM", "AccentColor")
                If abgr.HasValue Then
                    _accent = Color.FromArgb(255, abgr.Value And &HFF, (abgr.Value >> 8) And &HFF, (abgr.Value >> 16) And &HFF)
                Else
                    _accent = SystemColors.Highlight
                End If
            End If
            Return _accent.Value
        End Get
    End Property

    ' Dark palette (close to the Windows 11 dark applications)
    Public Shared ReadOnly Property Window() As Color
        Get
            Return If(IsDark, Color.FromArgb(32, 32, 32), SystemColors.Control)
        End Get
    End Property
    Public Shared ReadOnly Property Surface() As Color
        Get
            Return If(IsDark, Color.FromArgb(43, 43, 43), SystemColors.Window)
        End Get
    End Property
    Public Shared ReadOnly Property Text() As Color
        Get
            Return If(IsDark, Color.FromArgb(240, 240, 240), SystemColors.ControlText)
        End Get
    End Property
    Public Shared ReadOnly Property Border() As Color
        Get
            Return If(IsDark, Color.FromArgb(70, 70, 70), SystemColors.ControlDark)
        End Get
    End Property
    Public Shared ReadOnly Property ButtonFace() As Color
        Get
            Return If(IsDark, Color.FromArgb(55, 55, 55), SystemColors.Control)
        End Get
    End Property
    Public Shared ReadOnly Property Link() As Color
        Get
            Return If(IsDark, Color.FromArgb(120, 180, 255), Color.Blue)
        End Get
    End Property

    ' Visual style of the native lists, trees and scroll bars (Native.Functions.Misc.SetTheme)
    Public Shared ReadOnly Property ListThemeName() As String
        Get
            Return If(IsDark, "DarkMode_Explorer", "explorer")
        End Get
    End Property

    ' Visual style of a native list view: in dark mode the "items view" theme, which draws the group
    ' headers light (with DarkMode_Explorer they stay dark blue on the dark background)
    Public Shared ReadOnly Property ListViewThemeName() As String
        Get
            Return If(IsDark, "DarkMode_ItemsView", "explorer")
        End Get
    End Property

    Public Shared Sub Apply(ByVal form As Form)
        If form Is Nothing Then Exit Sub
        If IsDark Then
            form.BackColor = Window
            form.ForeColor = Text
            If form.IsHandleCreated Then
                DarkTitleBar(form)
            Else
                AddHandler form.HandleCreated, AddressOf OnFormHandleCreated
            End If
            DarkMenuBar(form)
            For Each c As Control In form.Controls
                ApplyTo(c)
            Next
        End If
        ApplyStrips(form)
    End Sub

    ' A user control created by the code
    Public Shared Sub Apply(ByVal control As UserControl)
        If control Is Nothing Then Exit Sub
        If IsDark Then ApplyTo(control)
        ApplyStrips(control)
    End Sub

    ' A menu or tool bar built after the window was themed
    Public Shared Sub Apply(ByVal strip As ToolStrip)
        If strip IsNot Nothing Then ApplyTo(strip)
    End Sub

    ' Tool bars and menus held in fields, including context menus that are not in Controls
    Private Shared Sub ApplyStrips(ByVal owner As Control)
        For Each field As FieldInfo In owner.GetType().GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.DeclaredOnly)
            Dim strip As ToolStrip = Nothing
            Try
                strip = TryCast(field.GetValue(owner), ToolStrip)
            Catch ex As Exception
                strip = Nothing
            End Try
            If strip IsNot Nothing Then ApplyTo(strip)
        Next
    End Sub

    Private Shared Sub OnFormHandleCreated(ByVal sender As Object, ByVal e As EventArgs)
        DarkTitleBar(DirectCast(sender, Form))
    End Sub

    ' The bar of a MainMenu (Win32 menu) is painted by Windows: in dark mode its text is light but its
    ' background stays black; give it the color of the window
    Private Shared Sub DarkMenuBar(ByVal form As Form)
        Try
            If form.Menu Is Nothing Then Exit Sub
            If _menuBrush = IntPtr.Zero Then _menuBrush = CreateSolidBrush(ColorTranslator.ToWin32(Window))
            Dim info As New MENUINFO()
            info.cbSize = Marshal.SizeOf(GetType(MENUINFO))
            info.fMask = MIM_BACKGROUND
            info.hbrBack = _menuBrush
            SetMenuInfo(form.Menu.Handle, info)
            If form.IsHandleCreated Then DrawMenuBar(form.Handle)
        Catch ex As Exception
            ' Older Windows: default menu bar
        End Try
    End Sub

    Private Shared Sub OnControlHandleCreated(ByVal sender As Object, ByVal e As EventArgs)
        DarkScrollBars(DirectCast(sender, Control))
    End Sub

    Private Shared Sub ApplyTo(ByVal c As Control)
        Dim strip As ToolStrip = TryCast(c, ToolStrip)
        If strip IsNot Nothing Then
            strip.Renderer = New ToolStripProfessionalRenderer(New Colors()) With {.RoundedEdges = False}
            If IsDark Then
                strip.BackColor = Window
                strip.ForeColor = Text
                For Each item As ToolStripItem In strip.Items
                    ApplyTo(item)
                Next
            End If
            Exit Sub
        End If
        If Not IsDark Then Exit Sub

        Dim list As ListView = TryCast(c, ListView)
        If list IsNot Nothing AndAlso list.View = View.Details Then DarkDetailsView(list)

        Dim tabs As TabControl = TryCast(c, TabControl)
        If tabs IsNot Nothing Then DarkTabs(tabs)

        If TypeOf c Is TextBoxBase OrElse TypeOf c Is ListBox OrElse TypeOf c Is ComboBox OrElse TypeOf c Is ListView _
                OrElse TypeOf c Is TreeView OrElse TypeOf c Is NumericUpDown OrElse TypeOf c Is DateTimePicker Then
            ' A read-only text box keeps a darker background, like a label area
            c.BackColor = Surface
            c.ForeColor = Text
            Dim combo As ComboBox = TryCast(c, ComboBox)
            If combo IsNot Nothing Then combo.FlatStyle = FlatStyle.Flat
            If TypeOf c Is ListView OrElse TypeOf c Is TreeView OrElse TypeOf c Is TextBoxBase OrElse TypeOf c Is ListBox Then
                If c.IsHandleCreated Then
                    DarkScrollBars(c)
                Else
                    AddHandler c.HandleCreated, AddressOf OnControlHandleCreated
                End If
            End If
        ElseIf TypeOf c Is DataGridView Then
            Dim grid As DataGridView = DirectCast(c, DataGridView)
            grid.BackgroundColor = Window
            grid.GridColor = Border
            grid.DefaultCellStyle.BackColor = Surface
            grid.DefaultCellStyle.ForeColor = Text
            grid.DefaultCellStyle.SelectionBackColor = Accent
            grid.ColumnHeadersDefaultCellStyle.BackColor = Window
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Text
            grid.RowHeadersDefaultCellStyle.BackColor = Window
            grid.EnableHeadersVisualStyles = False
        ElseIf TypeOf c Is ButtonBase AndAlso Not TypeOf c Is CheckBox AndAlso Not TypeOf c Is RadioButton Then
            Dim b As ButtonBase = DirectCast(c, ButtonBase)
            b.FlatStyle = FlatStyle.Flat
            b.FlatAppearance.BorderColor = Border
            b.BackColor = ButtonFace
            b.ForeColor = Text
        ElseIf TypeOf c Is ProgressBar OrElse TypeOf c Is PictureBox Then
            ' Drawn by Windows or showing a color or an image chosen elsewhere: unchanged
        ElseIf TypeOf c Is LinkLabel Then
            Dim link As LinkLabel = DirectCast(c, LinkLabel)
            link.BackColor = If(IsSystemBack(link.BackColor), Window, link.BackColor)
            link.ForeColor = Text
            link.LinkColor = Theme.Link
            link.ActiveLinkColor = Theme.Link
            link.VisitedLinkColor = Theme.Link
        Else
            If TypeOf c Is GroupBox OrElse TypeOf c Is Panel OrElse TypeOf c Is SplitContainer OrElse TypeOf c Is TabPage _
                    OrElse TypeOf c Is UserControl OrElse IsSystemBack(c.BackColor) Then
                c.BackColor = Window
            End If
            ' A text drawn on a color of its own (status labels, graphs...) keeps its color
            If IsSystemText(c.ForeColor) Then c.ForeColor = Text
            Dim page As TabPage = TryCast(c, TabPage)
            If page IsNot Nothing Then page.UseVisualStyleBackColor = False
        End If

        For Each child As Control In c.Controls
            ApplyTo(child)
        Next
    End Sub

    Private Shared Function IsSystemBack(ByVal color As Color) As Boolean
        Return color = SystemColors.Control OrElse color = SystemColors.Window OrElse color = SystemColors.ButtonFace _
            OrElse color = Drawing.Color.White OrElse color = Drawing.Color.Transparent
    End Function

    Private Shared Function IsSystemText(ByVal color As Color) As Boolean
        Return color = SystemColors.ControlText OrElse color = SystemColors.WindowText _
            OrElse color.ToArgb() = Drawing.Color.Black.ToArgb() OrElse color.IsEmpty
    End Function

    Private Shared Sub ApplyTo(ByVal item As ToolStripItem)
        item.ForeColor = Text
        Dim drop As ToolStripDropDownItem = TryCast(item, ToolStripDropDownItem)
        If drop IsNot Nothing Then
            drop.DropDown.BackColor = Window
            drop.DropDown.ForeColor = Text
            For Each subItem As ToolStripItem In drop.DropDownItems
                ApplyTo(subItem)
            Next
        End If
    End Sub

    ' Colors of a row in a dark list: a row colored by LitePM (new or deleted item,
    ' highlighting) keeps its background with a dark text; any other row is dark.
    Public Shared Sub RowColors(ByVal back As Color, ByVal fore As Color, ByRef rowBack As Color, ByRef rowFore As Color)
        If back.IsEmpty OrElse IsSystemBack(back) OrElse back = Surface Then
            rowBack = Surface
            rowFore = If(IsSystemText(fore), Text, fore)
        Else
            rowBack = back
            rowFore = If(IsSystemText(fore) OrElse fore = Text, Drawing.Color.Black, fore)
        End If
    End Sub

    ' A details list drawn by Windows keeps white grid lines and light headers in dark mode:
    ' it is drawn here (headers, rows, selection, check boxes, icons, grid lines). The text of
    ' a header goes through Localization.ColumnText (columns named by their English text).
    Private Shared Sub DarkDetailsView(ByVal list As ListView)
        If list.OwnerDraw Then Exit Sub
        Dim drawer As New DetailsDrawer(list)
        drawer.Attach()
    End Sub

    Private NotInheritable Class DetailsDrawer
        Private ReadOnly _list As ListView
        Private ReadOnly _gridLines As Boolean

        Public Sub New(ByVal list As ListView)
            _list = list
            _gridLines = list.GridLines
        End Sub

        Public Sub Attach()
            _list.GridLines = False
            _list.OwnerDraw = True
            AddHandler _list.DrawColumnHeader, AddressOf OnDrawColumnHeader
            AddHandler _list.DrawItem, AddressOf OnDrawItem
            AddHandler _list.DrawSubItem, AddressOf OnDrawSubItem
            ' Windows still handles the click on the check box: only the row is redrawn
            If _list.CheckBoxes Then AddHandler _list.ItemChecked, AddressOf OnItemChecked
        End Sub

        Private Sub OnItemChecked(ByVal sender As Object, ByVal e As ItemCheckedEventArgs)
            _list.Invalidate(e.Item.Bounds)
        End Sub

        Private Sub OnDrawColumnHeader(ByVal sender As Object, ByVal e As DrawListViewColumnHeaderEventArgs)
            Using back As New SolidBrush(Theme.Window)
                e.Graphics.FillRectangle(back, e.Bounds)
            End Using
            Using line As New Pen(Theme.Border)
                e.Graphics.DrawLine(line, e.Bounds.Right - 1, e.Bounds.Top + 3, e.Bounds.Right - 1, e.Bounds.Bottom - 4)
                e.Graphics.DrawLine(line, e.Bounds.Left, e.Bounds.Bottom - 1, e.Bounds.Right, e.Bounds.Bottom - 1)
            End Using
            Dim r As New Rectangle(e.Bounds.X + 6, e.Bounds.Y, e.Bounds.Width - 8, e.Bounds.Height)
            Dim caption As String = If(e.Header Is Nothing, "", Localization.ColumnText(e.Header.Text))
            TextRenderer.DrawText(e.Graphics, caption, _list.Font, r, Theme.Text,
                TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)
        End Sub

        Private Sub OnDrawItem(ByVal sender As Object, ByVal e As DrawListViewItemEventArgs)
            ' Everything is drawn per sub-item
        End Sub

        Private Sub OnDrawSubItem(ByVal sender As Object, ByVal e As DrawListViewSubItemEventArgs)
            Dim item As ListViewItem = e.Item
            If item Is Nothing Then Exit Sub
            Dim bounds As Rectangle = e.Bounds

            Dim back As Color = item.BackColor
            Dim fore As Color = item.ForeColor
            If e.SubItem IsNot Nothing AndAlso e.ColumnIndex > 0 AndAlso Not item.UseItemStyleForSubItems Then
                back = e.SubItem.BackColor
                fore = e.SubItem.ForeColor
            End If
            Dim rowBack As Color = Color.Empty
            Dim rowFore As Color = Color.Empty
            Theme.RowColors(back, fore, rowBack, rowFore)

            Using brush As New SolidBrush(rowBack)
                e.Graphics.FillRectangle(brush, bounds)
            End Using
            If item.Selected Then
                Using brush As New SolidBrush(Color.FromArgb(110, Theme.Accent))
                    e.Graphics.FillRectangle(brush, bounds)
                End Using
                rowFore = Theme.Text
            End If

            Dim x As Integer = bounds.X + 4
            If e.ColumnIndex = 0 Then
                If _list.CheckBoxes Then
                    Dim box As New Point(x, bounds.Y + (bounds.Height - 13) \ 2)
                    Dim state As System.Windows.Forms.VisualStyles.CheckBoxState = If(item.Checked, System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal)
                    CheckBoxRenderer.DrawCheckBox(e.Graphics, box, state)
                    x += 17
                End If
                Dim images As ImageList = _list.StateImageList
                Dim index As Integer = item.StateImageIndex
                If (images Is Nothing OrElse index < 0) AndAlso _list.SmallImageList IsNot Nothing Then
                    images = _list.SmallImageList
                    index = item.ImageIndex
                    If index < 0 AndAlso Not String.IsNullOrEmpty(item.ImageKey) Then index = images.Images.IndexOfKey(item.ImageKey)
                End If
                If images IsNot Nothing AndAlso index >= 0 AndAlso index < images.Images.Count Then
                    e.Graphics.DrawImage(images.Images(index), x, bounds.Y + (bounds.Height - images.ImageSize.Height) \ 2,
                        images.ImageSize.Width, images.ImageSize.Height)
                    x += images.ImageSize.Width + 3
                End If
            End If

            Dim caption As String = If(e.SubItem Is Nothing, item.Text, e.SubItem.Text)
            Dim r As New Rectangle(x, bounds.Y, Math.Max(0, bounds.Right - x - 2), bounds.Height)
            Dim rowFont As Font = If(item.Font, _list.Font)
            ' SingleLine: a text with a line break stays on its row (cut with an ellipsis)
            TextRenderer.DrawText(e.Graphics, caption, rowFont, r, rowFore,
                TextFormatFlags.SingleLine Or TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis Or TextFormatFlags.NoPrefix)

            If _gridLines Then
                Using line As New Pen(Theme.Border)
                    e.Graphics.DrawLine(line, bounds.Left, bounds.Bottom - 1, bounds.Right, bounds.Bottom - 1)
                    e.Graphics.DrawLine(line, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom)
                End Using
            End If
        End Sub
    End Class

    ' Tabs drawn by Windows stay light in dark mode
    Private Shared ReadOnly _tabPainters As New System.Runtime.CompilerServices.ConditionalWeakTable(Of TabControl, DarkTabStrip)()

    Private Shared Sub DarkTabs(ByVal tabs As TabControl)
        If tabs.DrawMode = TabDrawMode.OwnerDrawFixed Then Exit Sub
        tabs.DrawMode = TabDrawMode.OwnerDrawFixed
        AddHandler tabs.DrawItem, AddressOf OnDrawTab
        ' Windows still paints the strip behind the tabs, their borders and the frame of the page light;
        ' the painter lives as long as its TabControl
        _tabPainters.Add(tabs, New DarkTabStrip(tabs))
    End Sub

    Private Shared Sub OnDrawTab(ByVal sender As Object, ByVal e As DrawItemEventArgs)
        Dim tabs As TabControl = DirectCast(sender, TabControl)
        If e.Index < 0 OrElse e.Index >= tabs.TabPages.Count Then Exit Sub
        DrawTab(e.Graphics, tabs, e.Index, e.Bounds)
    End Sub

    Private Shared Sub DrawTab(ByVal g As Graphics, ByVal tabs As TabControl, ByVal index As Integer, ByVal bounds As Rectangle)
        Dim selected As Boolean = (index = tabs.SelectedIndex)
        Using back As New SolidBrush(If(selected, Surface, Window))
            g.FillRectangle(back, bounds)
        End Using
        If selected Then
            Using pen As New Pen(Accent, 2)
                g.DrawLine(pen, bounds.Left + 2, bounds.Bottom - 1, bounds.Right - 2, bounds.Bottom - 1)
            End Using
        End If
        TextRenderer.DrawText(g, tabs.TabPages(index).Text, tabs.Font, bounds, Text,
            TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPrefix)
    End Sub

    ' After Windows paints a dark owner-drawn TabControl: everything but the page (strip, borders
    ' between the tabs, frame) in the window color, then the tabs again on top
    Private NotInheritable Class DarkTabStrip
        Inherits NativeWindow

        Private Const WM_PAINT As Integer = &HF
        Private ReadOnly _tabs As TabControl

        Public Sub New(ByVal tabs As TabControl)
            _tabs = tabs
            If tabs.IsHandleCreated Then AssignHandle(tabs.Handle)
            AddHandler tabs.HandleCreated, AddressOf OnHandleCreated
            AddHandler tabs.HandleDestroyed, AddressOf OnHandleDestroyed
        End Sub

        Private Sub OnHandleCreated(ByVal sender As Object, ByVal e As EventArgs)
            AssignHandle(_tabs.Handle)
        End Sub

        Private Sub OnHandleDestroyed(ByVal sender As Object, ByVal e As EventArgs)
            ReleaseHandle()
        End Sub

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)
            If m.Msg <> WM_PAINT Then Exit Sub
            Try
                Using g As Graphics = Graphics.FromHwnd(_tabs.Handle)
                    Using area As New Region(_tabs.ClientRectangle)
                        area.Exclude(_tabs.DisplayRectangle)
                        Using back As New SolidBrush(Window)
                            g.FillRegion(back, area)
                        End Using
                    End Using
                    For i As Integer = 0 To _tabs.TabPages.Count - 1
                        DrawTab(g, _tabs, i, _tabs.GetTabRect(i))
                    Next
                End Using
            Catch ex As Exception
                ' Painting only: never stop the window for it
            End Try
        End Sub
    End Class

    ' Selection in menus and tool bars: the accent color (both modes); dark menus in dark mode
    Private NotInheritable Class Colors
        Inherits ProfessionalColorTable

        Private ReadOnly Property Selected() As Color
            Get
                Return Color.FromArgb(If(IsDark, 110, 60), Accent)
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemSelectedGradientEnd() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemPressedGradientBegin() As Color
            Get
                Return If(IsDark, Theme.Surface, MyBase.MenuItemPressedGradientBegin)
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemPressedGradientEnd() As Color
            Get
                Return If(IsDark, Theme.Surface, MyBase.MenuItemPressedGradientEnd)
            End Get
        End Property
        Public Overrides ReadOnly Property MenuItemBorder() As Color
            Get
                Return Accent
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonSelectedHighlight() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonSelectedGradientBegin() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonSelectedGradientEnd() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonPressedGradientBegin() As Color
            Get
                Return Accent
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonPressedGradientEnd() As Color
            Get
                Return Accent
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonSelectedBorder() As Color
            Get
                Return Accent
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonCheckedHighlight() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonCheckedGradientBegin() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonCheckedGradientMiddle() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ButtonCheckedGradientEnd() As Color
            Get
                Return Selected
            End Get
        End Property
        Public Overrides ReadOnly Property ToolStripDropDownBackground() As Color
            Get
                Return If(IsDark, Theme.Surface, MyBase.ToolStripDropDownBackground)
            End Get
        End Property
        Public Overrides ReadOnly Property ImageMarginGradientBegin() As Color
            Get
                Return If(IsDark, Theme.Surface, MyBase.ImageMarginGradientBegin)
            End Get
        End Property
        Public Overrides ReadOnly Property ImageMarginGradientMiddle() As Color
            Get
                Return If(IsDark, Theme.Surface, MyBase.ImageMarginGradientMiddle)
            End Get
        End Property
        Public Overrides ReadOnly Property ImageMarginGradientEnd() As Color
            Get
                Return If(IsDark, Theme.Surface, MyBase.ImageMarginGradientEnd)
            End Get
        End Property
        Public Overrides ReadOnly Property MenuBorder() As Color
            Get
                Return If(IsDark, Theme.Border, MyBase.MenuBorder)
            End Get
        End Property
        Public Overrides ReadOnly Property MenuStripGradientBegin() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.MenuStripGradientBegin)
            End Get
        End Property
        Public Overrides ReadOnly Property MenuStripGradientEnd() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.MenuStripGradientEnd)
            End Get
        End Property
        Public Overrides ReadOnly Property ToolStripGradientBegin() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.ToolStripGradientBegin)
            End Get
        End Property
        Public Overrides ReadOnly Property ToolStripGradientMiddle() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.ToolStripGradientMiddle)
            End Get
        End Property
        Public Overrides ReadOnly Property ToolStripGradientEnd() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.ToolStripGradientEnd)
            End Get
        End Property
        Public Overrides ReadOnly Property ToolStripBorder() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.ToolStripBorder)
            End Get
        End Property
        Public Overrides ReadOnly Property StatusStripGradientBegin() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.StatusStripGradientBegin)
            End Get
        End Property
        Public Overrides ReadOnly Property StatusStripGradientEnd() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.StatusStripGradientEnd)
            End Get
        End Property
        Public Overrides ReadOnly Property SeparatorDark() As Color
            Get
                Return If(IsDark, Theme.Border, MyBase.SeparatorDark)
            End Get
        End Property
        Public Overrides ReadOnly Property SeparatorLight() As Color
            Get
                Return If(IsDark, Theme.Window, MyBase.SeparatorLight)
            End Get
        End Property
    End Class

    ' ---- Win32 ------------------------------------------------------------------

    <DllImport("dwmapi.dll")>
    Private Shared Function DwmSetWindowAttribute(ByVal hwnd As IntPtr, ByVal attribute As Integer, ByRef value As Integer, ByVal size As Integer) As Integer
    End Function

    <DllImport("uxtheme.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal appName As String, ByVal idList As String) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    ' uxtheme ordinals 135 (SetPreferredAppMode) and 136 (FlushMenuThemes), Windows 10 1903 and later
    <DllImport("uxtheme.dll", EntryPoint:="#135")>
    Private Shared Function SetPreferredAppMode(ByVal mode As Integer) As Integer
    End Function

    <DllImport("uxtheme.dll", EntryPoint:="#136")>
    Private Shared Sub FlushMenuThemes()
    End Sub

    <StructLayout(LayoutKind.Sequential)>
    Private Structure MENUINFO
        Public cbSize As Integer
        Public fMask As Integer
        Public dwStyle As Integer
        Public cyMax As Integer
        Public hbrBack As IntPtr
        Public dwContextHelpID As Integer
        Public dwMenuData As IntPtr
    End Structure

    <DllImport("user32.dll")>
    Private Shared Function SetMenuInfo(ByVal hmenu As IntPtr, ByRef info As MENUINFO) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function DrawMenuBar(ByVal hwnd As IntPtr) As Boolean
    End Function

    <DllImport("gdi32.dll")>
    Private Shared Function CreateSolidBrush(ByVal color As Integer) As IntPtr
    End Function

    Private Const MIM_BACKGROUND As Integer = &H2
    ' One brush for every menu bar, kept for the life of LitePM
    Private Shared _menuBrush As IntPtr = IntPtr.Zero

    Private Const DWMWA_USE_IMMERSIVE_DARK_MODE As Integer = 20
    Private Const DWMWA_USE_IMMERSIVE_DARK_MODE_OLD As Integer = 19   ' Windows 10 before 20H1
    Private Const LVM_GETHEADER As Integer = &H101F
    Private Const APPMODE_FORCE_DARK As Integer = 2

    ' Popup menus (ContextMenu, menus of the main menu) drawn dark by Windows; the menu
    ' bar itself stays light
    Private Shared Sub DarkPopupMenus()
        Try
            Dim v As Version = Environment.OSVersion.Version
            If v.Major < 10 OrElse (v.Major = 10 AndAlso v.Build < 18362) Then Exit Sub
            SetPreferredAppMode(APPMODE_FORCE_DARK)
            FlushMenuThemes()
        Catch ex As Exception
            ' Older Windows: light menus
        End Try
    End Sub

    Private Shared Sub DarkTitleBar(ByVal form As Form)
        Try
            Dim isOn As Integer = 1
            If DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, isOn, 4) <> 0 Then
                DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE_OLD, isOn, 4)
            End If
        Catch ex As Exception
            ' Older Windows: light title bar
        End Try
    End Sub

    Private Shared Sub DarkScrollBars(ByVal c As Control)
        Try
            ' A list takes the dark "items view" theme of Windows: its group headers ("Default"...)
            ' are then drawn light instead of dark blue
            SetWindowTheme(c.Handle, If(TypeOf c Is ListView, "DarkMode_ItemsView", "DarkMode_Explorer"), Nothing)
            If TypeOf c Is ListView Then
                Dim header As IntPtr = SendMessage(c.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero)
                If header <> IntPtr.Zero Then SetWindowTheme(header, "DarkMode_ItemsView", Nothing)
            End If
        Catch ex As Exception
            ' Older Windows: light scroll bars
        End Try
    End Sub

    Private Shared Function ReadDword(ByVal key As String, ByVal name As String) As Integer?
        Try
            Using k As RegistryKey = Registry.CurrentUser.OpenSubKey(key)
                If k IsNot Nothing Then
                    Dim v As Object = k.GetValue(name)
                    If TypeOf v Is Integer Then Return DirectCast(v, Integer)
                End If
            End Using
        Catch ex As Exception
            ' No value: light mode, system accent
        End Try
        Return Nothing
    End Function

End Class
