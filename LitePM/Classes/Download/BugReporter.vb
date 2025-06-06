﻿
' Lite Process Monitor



Option Strict On

Imports System.Net

Public Class BugReporter
    Inherits WebClient

    ' Private variables
    Private _postValues As New Specialized.NameValueCollection()

    Private _summary As String
    Private _details As String

    ' Public properties
    Public Property ValueSummary() As String
        Get
            Return _summary
        End Get
        Set(ByVal value As String)
            _summary = value
        End Set
    End Property
    Public Property ValueDetails() As String
        Get
            Return _details
        End Get
        Set(ByVal value As String)
            _details = value
        End Set
    End Property


    ' Constructor
    Public Sub New()
        MyBase.New()

        ' Add some values to the list of values to send

        ' See http://sourceforge.net/tracker/?func=add&group_id=244697&atid=1126635

        ' Fixed values for yaprocmon project
        _postValues.Add("group_id", "244697")
        _postValues.Add("atid", "1126635")
        _postValues.Add("func", "postadd")
        _postValues.Add("submit", "Add Artifact")
        _postValues.Add("category_id", "1252941")   ' Auto_report category

        ' Private ?
        _postValues.Add("is_private", "0")

        ' Cateogry, 100 = None
        _postValues.Add("category_id", "100")

        ' Group, 100 = None
        _postValues.Add("artifact_group_id", "100")

        ' Assigned to me ( ID) :-)
        _postValues.Add("assigned_to", "1590933")

    End Sub


    ' Go async
    Public Function GoAsync() As Boolean

        If _summary Is Nothing OrElse _details Is Nothing Then
            Return False
        End If

        ' Add summary and description informations
        _postValues.Add("summary", Uri.EscapeDataString(_summary))
        _postValues.Add("details", Uri.EscapeDataString(_details))

        Me.QueryString = _postValues

        ' Send request
        Me.DownloadStringAsync(New Uri("http://sourceforge.net/tracker/index.php"))
        Return True
    End Function


    ' Return the bug report URL from the HTML code of the bug page
    Public Shared Function GetUrlOfBugReportFromHtml(ByVal content As String) As String
        ' Search (Artifact <a href=
        Dim i As Integer = InStr(content, "(Artifact <a href=", CompareMethod.Binary)
        Dim i2 As Integer = InStr(i, content, ">", CompareMethod.Binary)
        If i > 0 AndAlso i2 > i Then
            Return "http://sourceforge.net/" & content.Substring(i + 19, i2 - i - 21)
        Else
            Return ""
        End If
    End Function

End Class
