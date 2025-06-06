﻿
' Lite Process Monitor


Option Strict On

Public Class cUpdate


    ' Other public


    ' Structure for new versions
    Public Structure NewReleaseInfos
        Public Infos As String
        Public Url As String
        Public Hash As String
        Public Version As String
        Public Description As String
        Public Type As String
        Public Caption As String
        Public [Date] As String
        Public Sub New(ByVal aInfos As String, ByVal aUrl As String, ByVal aHash As String)
            Infos = aInfos
            Url = aUrl
            Hash = aHash
        End Sub
    End Structure

    ' Event raised when a new update is available
    Public Event NewVersionAvailable(ByVal silent As Boolean, _
                                     ByVal release As NewReleaseInfos)

    ' Event raised when no new update is available
    Public Event ProgramUpToDate(ByVal silent As Boolean)

    ' Event raised when failed to retrieve last version
    Public Event FailedToCheckVersion(ByVal silent As Boolean, ByVal msg As String)



    ' Public functions


    ' Check if LitePM is up to date
    ' This is async. and will raise events.
    Public Sub CheckUpdates(ByVal silentMode As Boolean)
        System.Threading.ThreadPool.QueueUserWorkItem(AddressOf pvtCheckUpdates, _
                                                      silentMode)
    End Sub


    ' Private functions

    ' Check updates (sync. version)
    Private Sub pvtCheckUpdates(ByVal context As Object)

        Dim silentMode As Boolean = CBool(context)

        Dim newRelease As NewReleaseInfos = Nothing
        Dim newReleaseL As NewReleaseInfos = Nothing
        Dim upToDate As Boolean = True
        Dim xmlContent As String
        Dim curVersion As Integer = getCurrentVersion()

        ' Download the XML file
        Dim cl As New System.Net.WebClient
        Try
            xmlContent = cl.DownloadString(My.Settings.UpdateServer)
            ' Debug line :             xmlContent = System.IO.File.ReadAllText("C:\Users\Admin\Desktop\LitePM\Website\update.xml")
        Catch ex As Exception
            RaiseEvent FailedToCheckVersion(silentMode, ex.Message)
            Exit Sub
        End Try

        ' Parse XML and extract version string
        Dim lastVersion As Integer = 0
        Dim updHash As String = Nothing
        Dim updDescription As String = Nothing
        Dim updCaption As String = Nothing
        Dim updUurl As String = Nothing
        Dim updDate As String = Nothing
        Dim updVersion As String = Nothing
        Dim updType As String = Nothing

        Try
            Dim xmlDoc As XmlDocument = New XmlDocument
            Dim updates As XmlNodeList
            xmlDoc.LoadXml(xmlContent)
            updates = xmlDoc.DocumentElement.GetElementsByTagName("update")
            For Each update As XmlNode In updates

                ' Retrieve type of update (stable, beta or alpha)
                Dim sType As String = update.Attributes("type").InnerText.ToLowerInvariant
                If sType = "stable" OrElse _
                    (sType = "alpha" AndAlso My.Settings.UpdateAlpha) OrElse _
                    (sType = "beta" AndAlso My.Settings.UpdateBeta) Then

                    Dim _updHash As String = Nothing
                    Dim _updDescription As String = Nothing
                    Dim _updCaption As String = Nothing
                    Dim _updUurl As String = Nothing
                    Dim _updDate As String = Nothing
                    Dim _updVersion As String = Nothing

                    ' Now retrieve informations about the update
                    For Each childNode As XmlNode In update.ChildNodes
                        If childNode.LocalName.ToLowerInvariant = "caption" Then
                            _updCaption = childNode.InnerText
                        ElseIf childNode.LocalName.ToLowerInvariant = "hash" Then
                            _updHash = childNode.InnerText
                        ElseIf childNode.LocalName.ToLowerInvariant = "description" Then
                            _updDescription = childNode.InnerText
                        ElseIf childNode.LocalName.ToLowerInvariant = "date" Then
                            _updDate = childNode.InnerText
                        ElseIf childNode.LocalName.ToLowerInvariant = "version" Then
                            _updVersion = childNode.InnerText
                        ElseIf childNode.LocalName.ToLowerInvariant = "downloadurl" Then
                            _updUurl = childNode.InnerText
                        End If
                    Next

                    ' We compare lastVersion and the version of this update
                    Dim i As Integer = getVersionNumberFromString(_updVersion)
                    If i > lastVersion Then
                        lastVersion = i
                        updHash = _updHash
                        updDescription = _updDescription
                        updCaption = _updCaption
                        updUurl = _updUurl
                        updDate = _updDate
                        updVersion = _updVersion
                        updType = sType
                    End If

                End If

                ' If we get a version, exit the loop (we only retrieve ONE version)
                If String.IsNullOrEmpty(updVersion) = False Then
                    Exit For
                End If

            Next

        Catch ex As Exception
            RaiseEvent FailedToCheckVersion(silentMode, "Failed to parse version file : " & ex.Message)
            Exit Sub
        End Try


        ' Compare more up-to-date version from XML with current version
        Dim sInfos As String = ""
        Dim sInfosL As String = ""
        If lastVersion > curVersion Then
            upToDate = False
            sInfosL = "New version : " & updVersion & vbNewLine & _
                "Release date : " & updDate & vbNewLine
            sInfos = sInfosL & vbNewLine & "Description : " & updDescription
            newRelease = New NewReleaseInfos(sInfos, updUurl, updHash)
            newReleaseL = New NewReleaseInfos(sInfosL, updUurl, updHash)
            With newRelease
                .Type = updType
                .Version = updVersion
                .Caption = updCaption
                .Date = updDate
                .Description = updDescription
            End With
        End If


        ' Raise event
        If silentMode Then
            If upToDate = False Then
                RaiseEvent NewVersionAvailable(silentMode, newReleaseL)
            End If
        Else
            If upToDate Then
                RaiseEvent ProgramUpToDate(silentMode)
            Else
                RaiseEvent NewVersionAvailable(silentMode, newRelease)
            End If
        End If
    End Sub

    ' Return a version number from a version string
    ' Ex : 2.2.1 returns 2210
    Private Function getVersionNumberFromString(ByVal version As String) As Integer
        Try
            Dim s() As String = version.Split(CChar("."))
            Dim l As Integer = s.Length
            Dim res As Integer = 0
            For x As Integer = 0 To l - 1
                res += CInt(Integer.Parse(s(x)) * 10 ^ (l - x + 1))
            Next
            Return res
        Catch ex As Exception
            Return 0
        End Try
    End Function

    ' Return current assembly version as a version number
    Private Function getCurrentVersion() As Integer
        Dim sVersion As String = My.Application.Info.Version.Major & "." & _
            My.Application.Info.Version.Minor & "." & _
            My.Application.Info.Version.Build & "." & _
            My.Application.Info.Version.Revision
        Return getVersionNumberFromString(sVersion)
    End Function

End Class
