' Lite Process Monitor


Option Strict On

Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports LiteSuite

' Self-update of LitePM from its GitHub releases (svtica/LitePM, asset LitePM.zip),
' with the SelfUpdater of the LiteSuite.Common package:
'   1. CheckAsync:   latest release, newer than this LitePM or not;
'   2. PrepareAsync: download, extraction, Authenticode signature checked
'                    (LiteSuite publisher, or the signer of this LitePM.exe);
'   3. Apply:        replacement script started; LitePM must then exit. The
'                    script stops the services that run this LitePM.exe (the
'                    remote service "LitePM.exe -SERVERSERVICE", whatever its
'                    name), copies the files, starts again the services that
'                    were running and restarts LitePM. Only the copy is
'                    elevated (UAC), and only when needed.
' Nothing of the installation changes before step 3.
'
' Files of the user: LitePM keeps its settings (user.config), hotkeys.xml and
' log.txt in the user profile (%LOCALAPPDATA%), not next to LitePM.exe, so the
' update cannot touch them. Next to LitePM.exe it only reads tcp.txt and
' udp.txt (optional port name lists, not shipped in the package): they are
' kept in case a user provides or edits them. LitePM.exe.config is shipped by
' each release and is not edited by LitePM (the settings are per user): it is
' replaced.
Public NotInheritable Class AppUpdater
    Implements IDisposable

    Public Const DIALOG_TITLE As String = "LitePM update"

    ' Release notes longer than this are cut in the confirmation dialog
    Private Const NOTES_MAX_LENGTH As Integer = 1200

    ' Files of the user next to LitePM.exe, never overwritten by an update
    Private Shared ReadOnly PRESERVED_FILES As String() = {"tcp.txt", "udp.txt"}

    Private ReadOnly _selfUpdater As SelfUpdater

    Public Sub New()
        Dim options As New SelfUpdaterOptions()
        options.ApplicationName = "LitePM"
        options.Repository = "LitePM"                   ' Owner: svtica (default)
        options.PackagePattern = "^LitePM.*\.zip$"
        options.PackageName = "LitePM.zip"
        options.ExecutableName = "LitePM.exe"
        options.CurrentVersion = GetType(AppUpdater).Assembly.GetName().Version.ToString()
        options.CurrentExecutablePath = Application.ExecutablePath
        For Each pattern As String In PRESERVED_FILES
            options.PreservePatterns.Add(pattern)
        Next
        _selfUpdater = New SelfUpdater(options)
    End Sub

    ' Checks for a new version and, when the user accepts, prepares it and
    ' starts the replacement script. Returns True when the script is started:
    ' the caller must then exit LitePM (Program.ExitLitePM). Every message is
    ' shown here; failures do not throw.
    Public Shared Async Function CheckAndInstallAsync(ByVal owner As Form) As Task(Of Boolean)
        Using updater As New AppUpdater()
            Return Await updater.RunAsync(owner)
        End Using
    End Function

    Private Async Function RunAsync(ByVal owner As Form) As Task(Of Boolean)

        ' 1. Latest release
        Dim check As UpdateCheckResult = Nothing
        Dim failure As Exception = Nothing
        owner.UseWaitCursor = True
        Try
            check = Await _selfUpdater.CheckAsync()
        Catch ex As Exception
            failure = ex
        Finally
            owner.UseWaitCursor = False
        End Try
        If failure IsNot Nothing Then
            ShowFailure(owner, failure)
            Return False
        End If

        If Not check.IsUpdateAvailable Then
            If check.Package Is Nothing AndAlso ReleaseVersion.IsNewer(check.CurrentVersion, check.LatestVersion) Then
                MessageBox.Show(owner, "LitePM " & check.LatestVersion & " is available, but its release has no LitePM package yet." &
                                vbCrLf & vbCrLf & "Please try again later.",
                                DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show(owner, "LitePM is up to date (version " & check.CurrentVersion & ").",
                                DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return False
        End If

        ' 2. Confirmation
        If MessageBox.Show(owner, BuildConfirmation(check), DIALOG_TITLE, MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) <> DialogResult.Yes Then
            Return False
        End If

        ' 3. Download, extraction and signature (nothing replaced yet)
        Dim update As PreparedUpdate = Nothing
        Dim progressForm As New UpdateProgressForm()
        owner.UseWaitCursor = True
        owner.Enabled = False
        progressForm.Show(owner)
        Try
            Dim progress As New Progress(Of UpdateStage)(AddressOf progressForm.ShowStage)
            update = Await _selfUpdater.PrepareAsync(check, progress)
        Catch ex As Exception
            failure = ex
        Finally
            progressForm.Close()
            progressForm.Dispose()
            owner.Enabled = True
            owner.UseWaitCursor = False
        End Try
        If failure IsNot Nothing Then
            ShowFailure(owner, failure)
            Return False
        End If

        ' Unsigned package: accepted by the policy only because this LitePM.exe
        ' is an unsigned (development) build too. Ask before installing it.
        If update.IsUnsigned Then
            If MessageBox.Show(owner, "The package of LitePM " & update.Version & " is not signed." & vbCrLf &
                               "It is accepted only because this LitePM is an unsigned build too." &
                               vbCrLf & vbCrLf & "Install it anyway?",
                               DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                               MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                update.Discard()
                Return False
            End If
        End If

        ' 4. Replacement script, which waits for this process to exit
        Try
            Using current As Process = Process.GetCurrentProcess()
                _selfUpdater.Apply(update, current.Id)
            End Using
        Catch ex As Exception
            update.Discard()
            ShowFailure(owner, ex)
            Return False
        End Try
        Return True

    End Function

    ' Text of the "install now?" dialog: versions, release notes, services
    Private Function BuildConfirmation(ByVal check As UpdateCheckResult) As String
        Dim text As New StringBuilder()
        text.AppendLine("A new version of LitePM is available.")
        text.AppendLine()
        text.AppendLine("Installed version: " & check.CurrentVersion)
        text.AppendLine("New version: " & check.LatestVersion)

        Dim notes As String = ReleaseNotes(check.Release)
        If notes.Length > 0 Then
            text.AppendLine()
            text.AppendLine("Release notes:")
            text.AppendLine(notes)
        End If

        ' Services that run this LitePM.exe (remote service): the script stops
        ' them and starts again those that are running now
        Dim services As List(Of String) = _selfUpdater.FindServices()
        Dim activeServices As New List(Of String)
        For Each name As String In services
            If ServiceLocator.IsActive(name) Then activeServices.Add(name)
        Next
        If activeServices.Count > 0 Then
            text.AppendLine()
            text.AppendLine("The LitePM remote service will be stopped and restarted (" & String.Join(", ", activeServices.ToArray()) & ").")
        End If
        If UpdateScriptLauncher.NeedsElevation(_selfUpdater.Options.ApplicationDirectory, services) Then
            text.AppendLine()
            text.AppendLine("Windows will ask for administrator rights to replace the files.")
        End If

        text.AppendLine()
        text.Append("Install now? LitePM will close and restart.")
        Return text.ToString()
    End Function

    ' Body of the release, line breaks normalized, cut to NOTES_MAX_LENGTH
    Private Shared Function ReleaseNotes(ByVal release As GitHubRelease) As String
        If release Is Nothing OrElse String.IsNullOrWhiteSpace(release.Body) Then Return ""
        Dim body As String = release.Body.Replace(vbCrLf, vbLf).Replace(vbCr, vbLf).Replace(vbLf, vbCrLf).Trim()
        If body.Length > NOTES_MAX_LENGTH Then
            body = body.Substring(0, NOTES_MAX_LENGTH).TrimEnd() & "..."
        End If
        Return body
    End Function

    Private Shared Sub ShowFailure(ByVal owner As IWin32Window, ByVal ex As Exception)
        Dim message As String
        Dim updateError As UpdateException = TryCast(ex, UpdateException)
        If updateError IsNot Nothing Then
            message = FailureText(updateError)
        Else
            message = "The update failed: an unexpected error occurred."
        End If
        message &= vbCrLf & vbCrLf & "Nothing was changed in the installation." &
                   vbCrLf & vbCrLf & "Details: " & ex.Message
        MessageBox.Show(owner, message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Shared Function FailureText(ByVal ex As UpdateException) As String
        Select Case ex.Failure
            Case UpdateFailure.CheckFailed
                Return "Could not check for updates: the latest LitePM release could not be read from GitHub."
            Case UpdateFailure.NoPackage
                Return "The latest LitePM release has no LitePM package."
            Case UpdateFailure.UnsafeUrl
                Return "The download link of the update was refused (not a LitePM release on github.com)."
            Case UpdateFailure.DownloadFailed
                Return "The update could not be downloaded."
            Case UpdateFailure.InvalidPackage
                Return "The update package is invalid."
            Case UpdateFailure.ExecutableNotFound
                Return "LitePM.exe is not in the update package."
            Case UpdateFailure.SignatureRejected
                Return SignatureText(ex)
            Case UpdateFailure.LaunchFailed
                Return "The update could not be started."
            Case Else
                Return "The update failed."
        End Select
    End Function

    Private Shared Function SignatureText(ByVal ex As UpdateException) As String
        If ex.Verdict.HasValue Then
            Select Case ex.Verdict.Value
                Case UpdateSignatureVerdict.RejectedUnsigned
                    Return "The update was refused: its LitePM.exe is not signed, while this LitePM is."
                Case UpdateSignatureVerdict.RejectedTampered
                    Return "The update was refused: the signature of its LitePM.exe does not match its content (the file was modified)."
                Case UpdateSignatureVerdict.RejectedPublisherMismatch
                    Return "The update was refused: its LitePM.exe is signed by another publisher."
            End Select
        End If
        Return "The update was refused: the signature of its LitePM.exe could not be validated."
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        _selfUpdater.Dispose()
    End Sub

End Class

' Small window shown while the update is downloaded and verified
Friend NotInheritable Class UpdateProgressForm
    Inherits Form

    Private ReadOnly _label As Label
    Private ReadOnly _bar As ProgressBar

    Public Sub New()
        Me.Text = AppUpdater.DIALOG_TITLE
        Me.Font = SystemFonts.MessageBoxFont
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.ControlBox = False
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.ClientSize = New Size(380, 84)

        _label = New Label()
        _label.AutoSize = False
        _label.Location = New Point(12, 14)
        _label.Size = New Size(356, 22)
        _label.Text = "Checking for updates..."

        _bar = New ProgressBar()
        _bar.Location = New Point(12, 44)
        _bar.Size = New Size(356, 20)
        _bar.Style = ProgressBarStyle.Marquee
        _bar.MarqueeAnimationSpeed = 30

        Me.Controls.Add(_label)
        Me.Controls.Add(_bar)
    End Sub

    ' Centered on its owner (StartPosition.CenterParent applies to ShowDialog only)
    Protected Overrides Sub OnLoad(ByVal e As EventArgs)
        MyBase.OnLoad(e)
        If Me.Owner IsNot Nothing AndAlso Me.Owner.Visible Then
            Dim area As Rectangle = Me.Owner.Bounds
            Me.Location = New Point(area.Left + (area.Width - Me.Width) \ 2, area.Top + (area.Height - Me.Height) \ 2)
        Else
            Dim screenArea As Rectangle = Screen.PrimaryScreen.WorkingArea
            Me.Location = New Point(screenArea.Left + (screenArea.Width - Me.Width) \ 2, screenArea.Top + (screenArea.Height - Me.Height) \ 2)
        End If
    End Sub

    ' Called on the UI thread by Progress(Of UpdateStage)
    Public Sub ShowStage(ByVal stage As UpdateStage)
        If Me.IsDisposed Then Return
        Select Case stage
            Case UpdateStage.Checking
                _label.Text = "Checking for updates..."
            Case UpdateStage.Downloading
                _label.Text = "Downloading the update..."
            Case UpdateStage.Extracting
                _label.Text = "Extracting the update..."
            Case UpdateStage.VerifyingSignature
                _label.Text = "Verifying the signature..."
            Case UpdateStage.SignatureAccepted
                _label.Text = "Signature verified."
            Case UpdateStage.UnsignedAccepted
                _label.Text = "The update is not signed."
            Case UpdateStage.LaunchingScript
                _label.Text = "Starting the update..."
        End Select
    End Sub

End Class
