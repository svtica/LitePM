
' Lite Process Monitor









'




'




Option Strict On

Public Class cLog

    Private frm As New frmLog

    'Private _lineCount As Integer
    'Private _spaces As Integer = 5
    'Private _addTime As Boolean = True
    'Private _s() As String

    ' Constructors
    Public Sub New(ByVal stringValue As String)
        '_lineCount = 0
        'ReDim _s(250)
    End Sub
    Public Sub New()
        '_lineCount = 0
        'ReDim _s(250)
    End Sub


    ' Propeties
    'Public Property SpacesBetweenTimeAndText() As Integer
    '    Get
    '        Return _spaces
    '    End Get
    '    Set(ByVal value As Integer)
    '        If value >= 0 Then
    '            _spaces = value
    '        End If
    '    End Set
    'End Property
    'Public Property AddDateTime() As Boolean
    '    Get
    '        Return _addTime
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _addTime = value
    '    End Set
    'End Property
    Public ReadOnly Property LineCount() As Integer
        Get
            Return Me.frm.lv.Items.Count
        End Get
    End Property
    Public ReadOnly Property Items() As ListView.ListViewItemCollection
        Get
            Return Me.frm.lv.Items
        End Get
    End Property
    'Public ReadOnly Property Line(ByVal index As Integer) As String
    '    Get
    '        If index > 0 And index <= Me.LineCount Then
    '            Return _s(index)
    '        Else
    '            Return vbNullString
    '        End If
    '    End Get
    'End Property
    Public WriteOnly Property ShowForm() As Boolean
        Set(ByVal value As Boolean)
            If value Then
                frm.TopMost = _frmMain.TopMost
                frm.Show()
            Else
                frm.Hide()
            End If
        End Set
    End Property
    Public ReadOnly Property Form() As frmLog
        Get
            Return frm
        End Get
    End Property



    ' Public functions
    Public Sub Clear()
        'ReDim _s(0)
        '_lineCount = 0
        frm.lv.Items.Clear()
    End Sub

    Public Sub AppendLine(ByVal line As String)
        'Dim s As String = ""
        'If _lineCount > 0 Then
        '    s &= vbNewLine
        'End If
        'If _addTime Then
        '    s &= Date.Now.ToLongDateString & " -- " & Date.Now.ToLongTimeString & Space(_spaces)
        'End If
        's &= line
        '_lineCount += 1

        '' Redim array if necessary
        '' Size *2 -> size each time (reduces number of redim calls)
        'If _lineCount >= _s.Length Then
        '    ReDim Preserve _s(_s.Length * 2)
        'End If
        '_s(_lineCount) = s
        Dim it As New ListViewItem(Date.Now.ToLongDateString & " -- " & Date.Now.ToLongTimeString)
        it.SubItems.Add(line)
        Async.ListView.AddItem(Me.frm.lv, it)
    End Sub

    Public Function GetLog() As String
        'Dim logSize As Integer = System.Runtime.InteropServices.Marshal.SizeOf(_s)
        'Dim sb As New StringBuilder(logSize)
        'For Each ss As String In _s
        '    sb.AppendLine(ss)
        'Next
        'Return sb.ToString
        Dim s As String = ""
        For Each it As ListViewItem In frm.lv.Items
            s &= it.Text & vbTab & it.SubItems(1).Text
        Next
        Return s
    End Function

    'Public Sub AddEmptyLine()
    '    ' Redim array if necessary
    '    ' Size *2 -> size each time (reduces number of redim calls)
    '    _lineCount += 1
    '    If _lineCount > _s.Length Then
    '        ReDim Preserve _s(_s.Length * 2)
    '    End If
    '    _s(_lineCount) = vbNewLine
    'End Sub

End Class
