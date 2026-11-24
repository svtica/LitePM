
' Lite Process Monitor

' User interface language. English texts live in the code and the designer
' files; lang\<language>.xml next to LitePM.exe holds the translations:
'   <translation key="frmMain.MenuItemFileOpen.Text" source="&amp;Open...">&amp;Ouvrir...</translation>
' A text of a window is replaced only when it is still the English text of the
' "source" attribute: a text set by the code is never overwritten, and a
' translation left behind by a change of the designer is ignored. A missing key
' keeps the English text. See docs/localization.md.

Option Strict On

Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports System.Xml

Public NotInheritable Class Localization

    Public Const Automatic As String = "auto"
    Public Const English As String = "en"
    Public Const French As String = "fr"

    ' Key prefixes of the texts looked up by their English text
    Private Const MessagePrefix As String = "Message."
    Private Const ColumnPrefix As String = "Column."

    Private Shared ReadOnly _translations As New Dictionary(Of String, String)(StringComparer.Ordinal)
    Private Shared ReadOnly _sources As New Dictionary(Of String, String)(StringComparer.Ordinal)
    Private Shared ReadOnly _messages As New Dictionary(Of String, String)(StringComparer.Ordinal)
    Private Shared ReadOnly _messagePrefixes As New List(Of String)
    Private Shared ReadOnly _columns As New Dictionary(Of String, String)(StringComparer.Ordinal)
    ' English text of what Apply translated (tab pages, list items, combo boxes...)
    Private Shared ReadOnly _originals As New ConditionalWeakTable(Of Object, String)
    Private Shared ReadOnly _comboOriginals As New ConditionalWeakTable(Of ComboBox, String())
    Private Shared _language As String = English

    Private Sub New()
    End Sub

    ' Language in use: "en" or "fr"
    Public Shared ReadOnly Property Language() As String
        Get
            Return _language
        End Get
    End Property

    ' Chooses the language: the LITEPM_LANG environment variable (tests), else the
    ' user's choice ("auto", "en", "fr"); "auto" follows the Windows display language.
    Public Shared Sub Initialize(ByVal preference As String)
        Dim env As String = Environment.GetEnvironmentVariable("LITEPM_LANG")
        Dim choice As String = If(String.IsNullOrEmpty(env), preference, env)

        If String.IsNullOrEmpty(choice) OrElse choice = Automatic Then
            choice = If(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName = French, French, English)
        End If

        _language = If(choice = French, French, English)
        _translations.Clear()
        _sources.Clear()
        _messages.Clear()
        _messagePrefixes.Clear()
        _columns.Clear()

        If _language <> English Then
            Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lang", _language & ".xml"))
        End If
    End Sub

    Private Shared Sub Load(ByVal file As String)
        If Not IO.File.Exists(file) Then Exit Sub
        Try
            Dim doc As New XmlDocument()
            doc.XmlResolver = Nothing
            doc.Load(file)
            For Each node As XmlNode In doc.SelectNodes("/translations/translation")
                Dim key As XmlAttribute = node.Attributes("key")
                If key Is Nothing Then Continue For
                Dim text As String = node.InnerText
                _translations(key.Value) = text
                Dim source As XmlAttribute = node.Attributes("source")
                If source Is Nothing Then Continue For
                _sources(key.Value) = source.Value
                If key.Value.StartsWith(MessagePrefix, StringComparison.Ordinal) Then
                    _messages(source.Value) = text
                    ' "Unable to enumerate jobs : " & detail -> the translated start, then the detail
                    If source.Value.TrimEnd().EndsWith(":", StringComparison.Ordinal) Then
                        _messagePrefixes.Add(source.Value)
                    End If
                ElseIf key.Value.StartsWith(ColumnPrefix, StringComparison.Ordinal) Then
                    _columns(source.Value) = text
                End If
            Next
            ' Longest first: the most specific start wins
            _messagePrefixes.Sort(Function(a As String, b As String) b.Length.CompareTo(a.Length))
        Catch ex As Exception
            ' A damaged translation file leaves the interface in English
            _translations.Clear()
            _sources.Clear()
            _messages.Clear()
            _messagePrefixes.Clear()
            _columns.Clear()
        End Try
    End Sub

    ' True when a translation file is loaded
    Public Shared ReadOnly Property IsTranslated() As Boolean
        Get
            Return _translations.Count > 0
        End Get
    End Property

    ' Translation of key, or the English text (also when the translation was made for another English text)
    Public Shared Function T(ByVal key As String, ByVal english As String) As String
        Dim text As String = Nothing
        If Not _translations.TryGetValue(key, text) Then Return english
        Dim source As String = Nothing
        If _sources.TryGetValue(key, source) AndAlso source <> english Then Return english
        Return text
    End Function

    ' String.Format of the translation of key (or of the English format)
    Public Shared Function F(ByVal key As String, ByVal englishFormat As String, ByVal ParamArray args() As Object) As String
        Return String.Format(T(key, englishFormat), args)
    End Function

    ' String.Format of the translation of an English format looked up by its text ("Message.*" keys,
    ' like Text): for a text of the code made of fixed parts and values
    Public Shared Function Format(ByVal englishFormat As String, ByVal ParamArray args() As Object) As String
        Return String.Format(Text(englishFormat), args)
    End Function

    ' A message shown by Misc.ShowMsg, ShowError... looked up by its English text
    ' ("Message.*" keys): the whole text, else its start up to a colon followed by
    ' the detail added by the code, else each line. Unknown: unchanged.
    Public Shared Function Text(ByVal english As String) As String
        If String.IsNullOrEmpty(english) OrElse _messages.Count = 0 Then Return english
        Dim result As String = Nothing
        If _messages.TryGetValue(english, result) Then Return result
        For Each start As String In _messagePrefixes
            If english.Length > start.Length AndAlso english.StartsWith(start, StringComparison.Ordinal) Then
                Return _messages(start) & english.Substring(start.Length)
            End If
        Next
        If english.Contains(vbLf) Then
            Dim lines() As String = english.Replace(vbCrLf, vbLf).Split(ControlChars.Lf)
            Dim changed As Boolean = False
            For i As Integer = 0 To lines.Length - 1
                Dim line As String = Nothing
                If lines(i).Length > 0 AndAlso _messages.TryGetValue(lines(i), line) Then
                    lines(i) = line
                    changed = True
                End If
            Next
            If changed Then Return String.Join(vbCrLf, lines)
        End If
        Return english
    End Function

    ' Displayed text of a list column whose English name is also its identity
    ' (customLV lists save and look up their columns by name): "Column.*" keys.
    ' The sort mark "> " or "< " added by DoubleBufferedLV is kept.
    Public Shared Function ColumnText(ByVal name As String) As String
        If String.IsNullOrEmpty(name) OrElse _columns.Count = 0 Then Return name
        Dim mark As String = ""
        Dim bare As String = name
        If name.StartsWith("> ", StringComparison.Ordinal) OrElse name.StartsWith("< ", StringComparison.Ordinal) Then
            mark = name.Substring(0, 2)
            bare = name.Substring(2)
        End If
        Dim result As String = Nothing
        If _columns.TryGetValue(bare, result) Then Return mark & result
        Return name
    End Function

    ' True when the display of the list columns is translated
    Public Shared ReadOnly Property TranslatesColumns() As Boolean
        Get
            Return _columns.Count > 0
        End Get
    End Property

    ' English text of a tab page, list item, menu item... translated by Apply (its text otherwise):
    ' the code compares and saves this one, never the displayed text
    Public Shared Function Original(ByVal page As TabPage) As String
        If page Is Nothing Then Return Nothing
        Return OriginalOf(page, page.Text)
    End Function

    Public Shared Function Original(ByVal item As ListViewItem) As String
        If item Is Nothing Then Return Nothing
        Return OriginalOf(item, item.Text)
    End Function

    ' English text of the selected item of a combo box translated by Apply (its text otherwise)
    Public Shared Function Original(ByVal combo As ComboBox) As String
        If combo Is Nothing Then Return Nothing
        Dim items() As String = Nothing
        If _comboOriginals.TryGetValue(combo, items) AndAlso combo.SelectedIndex >= 0 AndAlso combo.SelectedIndex < items.Length Then
            Return items(combo.SelectedIndex)
        End If
        Return combo.Text
    End Function

    Private Shared Function OriginalOf(ByVal obj As Object, ByVal current As String) As String
        Dim english As String = Nothing
        If _originals.TryGetValue(obj, english) Then Return english
        Return current
    End Function

    ' Translates the text of an item built by the code and remembers its English text (Original)
    Public Shared Sub Translate(ByVal item As ListViewItem, ByVal key As String)
        If item Is Nothing Then Exit Sub
        item.Text = Swap(key, item.Text, item)
    End Sub

    ' The translation of key when current is still its English source; current otherwise
    Private Shared Function Swap(ByVal key As String, ByVal current As String, ByVal owner As Object) As String
        Dim text As String = Nothing
        If current Is Nothing OrElse Not _translations.TryGetValue(key, text) Then Return current
        Dim source As String = Nothing
        If _sources.TryGetValue(key, source) AndAlso source <> current Then Return current
        If owner IsNot Nothing Then
            _originals.Remove(owner)
            _originals.Add(owner, current)
        End If
        Return text
    End Function

    ' Translates a window: its title ("<Form>.Text") and, for every control, menu item,
    ' column header, dialog and tool tip held in a field of the form, "<Form>.<field>.Text"
    ' (".ToolTipText", ".ToolTip", ".Title", ".Description", ".HeaderText", ".Items.N",
    ' ".Groups.N"). User controls held in fields are translated with their own type name.
    Public Shared Sub Apply(ByVal form As Form)
        If form Is Nothing OrElse _translations.Count = 0 Then Exit Sub
        Dim name As String = form.GetType().Name
        form.Text = Swap(name & ".Text", form.Text, Nothing)
        ApplyFields(form, New HashSet(Of Object)())
    End Sub

    ' Same as a window, for a user control created by the code (no title)
    Public Shared Sub Apply(ByVal control As UserControl)
        If control Is Nothing OrElse _translations.Count = 0 Then Exit Sub
        ApplyFields(control, New HashSet(Of Object)())
    End Sub

    ' Field name of a designer item: VB keeps a WithEvents field as "_Name"
    Private Shared Function FieldName(ByVal field As FieldInfo) As String
        If field.Name.StartsWith("_", StringComparison.Ordinal) AndAlso field.Name.Length > 1 Then
            Return field.Name.Substring(1)
        End If
        Return field.Name
    End Function

    Private Shared Sub ApplyFields(ByVal owner As Object, ByVal done As HashSet(Of Object))
        If Not done.Add(owner) Then Exit Sub
        Dim prefix As String = owner.GetType().Name & "."
        Dim fields() As FieldInfo = owner.GetType().GetFields(BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.DeclaredOnly)
        Dim tips As New List(Of ToolTip)
        Dim controls As New List(Of KeyValuePair(Of String, Control))

        For Each field As FieldInfo In fields
            Dim value As Object = Nothing
            Try
                value = field.GetValue(owner)
            Catch ex As Exception
                Continue For
            End Try
            If value Is Nothing Then Continue For
            Dim key As String = prefix & FieldName(field)

            If TypeOf value Is ToolTip Then
                tips.Add(DirectCast(value, ToolTip))

            ElseIf TypeOf value Is ComboBox Then
                Dim combo As ComboBox = DirectCast(value, ComboBox)
                controls.Add(New KeyValuePair(Of String, Control)(key, combo))
                TranslateItems(combo, key)

            ElseIf TypeOf value Is UserControl Then
                Dim uc As UserControl = DirectCast(value, UserControl)
                controls.Add(New KeyValuePair(Of String, Control)(key, uc))
                ApplyFields(uc, done)

            ElseIf TypeOf value Is ListView Then
                Dim list As ListView = DirectCast(value, ListView)
                controls.Add(New KeyValuePair(Of String, Control)(key, list))
                For i As Integer = 0 To list.Groups.Count - 1
                    list.Groups(i).Header = Swap(key & ".Groups." & i.ToString(CultureInfo.InvariantCulture), list.Groups(i).Header, Nothing)
                Next

            ElseIf TypeOf value Is Control AndAlso Not TypeOf value Is ListControl _
                    AndAlso Not TypeOf value Is NumericUpDown AndAlso Not TypeOf value Is DateTimePicker Then
                Dim c As Control = DirectCast(value, Control)
                controls.Add(New KeyValuePair(Of String, Control)(key, c))
                c.Text = Swap(key & ".Text", c.Text, c)

            ElseIf TypeOf value Is ToolStripItem Then
                Dim item As ToolStripItem = DirectCast(value, ToolStripItem)
                item.Text = Swap(key & ".Text", item.Text, item)
                If Not String.IsNullOrEmpty(item.ToolTipText) Then
                    item.ToolTipText = Swap(key & ".ToolTipText", item.ToolTipText, Nothing)
                End If

            ElseIf TypeOf value Is MenuItem Then
                Dim item As MenuItem = DirectCast(value, MenuItem)
                item.Text = Swap(key & ".Text", item.Text, item)

            ElseIf TypeOf value Is ColumnHeader Then
                Dim column As ColumnHeader = DirectCast(value, ColumnHeader)
                ' The columns of DoubleBufferedLV lists (customLV...) are named by their
                ' English text: only their display is translated (ColumnText)
                If Not TypeOf column.ListView Is DoubleBufferedLV Then
                    column.Text = Swap(key & ".Text", column.Text, column)
                End If

            ElseIf TypeOf value Is StatusBarPanel Then
                Dim panel As StatusBarPanel = DirectCast(value, StatusBarPanel)
                panel.Text = Swap(key & ".Text", panel.Text, Nothing)
                If Not String.IsNullOrEmpty(panel.ToolTipText) Then
                    panel.ToolTipText = Swap(key & ".ToolTipText", panel.ToolTipText, Nothing)
                End If

            ElseIf TypeOf value Is DataGridViewColumn Then
                Dim column As DataGridViewColumn = DirectCast(value, DataGridViewColumn)
                column.HeaderText = Swap(key & ".HeaderText", column.HeaderText, Nothing)

            ElseIf TypeOf value Is FileDialog Then
                Dim dialog As FileDialog = DirectCast(value, FileDialog)
                dialog.Title = Swap(key & ".Title", dialog.Title, Nothing)

            ElseIf TypeOf value Is FolderBrowserDialog Then
                Dim dialog As FolderBrowserDialog = DirectCast(value, FolderBrowserDialog)
                dialog.Description = Swap(key & ".Description", dialog.Description, Nothing)
            End If
        Next

        ' Tool tips: "<Form>.<control field>.ToolTip"
        For Each tip As ToolTip In tips
            For Each pair As KeyValuePair(Of String, Control) In controls
                Dim current As String = tip.GetToolTip(pair.Value)
                If String.IsNullOrEmpty(current) Then Continue For
                Dim translated As String = Swap(pair.Key & ".ToolTip", current, Nothing)
                If translated <> current Then tip.SetToolTip(pair.Value, translated)
            Next
        Next
    End Sub

    ' Items of a combo box: "<Form>.<field>.Items.N"
    Private Shared Sub TranslateItems(ByVal combo As ComboBox, ByVal key As String)
        Dim english(combo.Items.Count - 1) As String
        Dim changed As Boolean = False
        For i As Integer = 0 To combo.Items.Count - 1
            Dim item As String = TryCast(combo.Items(i), String)
            english(i) = If(item, Convert.ToString(combo.Items(i), CultureInfo.CurrentCulture))
            If item Is Nothing Then Continue For
            Dim translated As String = Swap(key & ".Items." & i.ToString(CultureInfo.InvariantCulture), item, Nothing)
            If translated <> item Then
                combo.Items(i) = translated
                changed = True
            End If
        Next
        If changed Then
            _comboOriginals.Remove(combo)
            _comboOriginals.Add(combo, english)
        End If
    End Sub

End Class
