
' Lite Process Monitor









'




'




Option Strict On

Imports System.IO
Imports System.Text

Public Class cLog

    Private frm As New frmLog
    Private Const MAX_LOG_ITEMS As Integer = 5000
    Private _logFilePath As String = Nothing
    Private _logToFile As Boolean = False

    ' Constructors
    Public Sub New(ByVal stringValue As String)
    End Sub
    Public Sub New()
    End Sub


    ' Properties
    Public Property LogToFile() As Boolean
        Get
            Return _logToFile
        End Get
        Set(ByVal value As Boolean)
            _logToFile = value
        End Set
    End Property
    Public Property LogFilePath() As String
        Get
            Return _logFilePath
        End Get
        Set(ByVal value As String)
            _logFilePath = value
        End Set
    End Property
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
        frm.lv.Items.Clear()
    End Sub

    Public Sub AppendLine(ByVal line As String)
        Dim timestamp As String = Date.Now.ToLongDateString & " -- " & Date.Now.ToLongTimeString

        ' Evict oldest entries when the log exceeds the max size
        If Me.frm.lv.Items.Count >= MAX_LOG_ITEMS Then
            Dim toRemove As Integer = Me.frm.lv.Items.Count - MAX_LOG_ITEMS + 1
            For i As Integer = 0 To toRemove - 1
                Me.frm.lv.Items.RemoveAt(0)
            Next
        End If
        Dim it As New ListViewItem(timestamp)
        it.SubItems.Add(line)
        Async.ListView.AddItem(Me.frm.lv, it)

        ' Append to log file if enabled
        If _logToFile AndAlso _logFilePath IsNot Nothing Then
            Try
                Using stream As New StreamWriter(_logFilePath, True)
                    stream.WriteLine(timestamp & vbTab & line)
                End Using
            Catch ex As Exception
                ' Silently ignore file write errors to avoid recursive logging
            End Try
        End If
    End Sub

    Public Function GetLog() As String
        Dim sb As New StringBuilder(frm.lv.Items.Count * 80)
        For Each it As ListViewItem In frm.lv.Items
            sb.Append(it.Text).Append(vbTab).AppendLine(it.SubItems(1).Text)
        Next
        Return sb.ToString()
    End Function

End Class
