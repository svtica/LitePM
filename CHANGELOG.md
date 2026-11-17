# Changelog

All notable changes to LitePM are documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Removed
- Bug report sent to the SourceForge tracker (no longer answers): the
  **Send the bug report** button and the custom message field of the error
  window (`BugReporter`). The error window now has a **Copy the report** button
  and points to the GitHub issues of LitePM.
- Unused `frmTracker` window and unused `Pref.MessageFirstStartOfLitePM`
  message (both pointed to SourceForge).

## [1.1.5] - 2026-11-17

### Changed
- The **Check now** button of Preferences > Update and the
  **Check for updates at startup** option (setting `UpdateAuto`, still off by
  default) now use the GitHub releases of LitePM, like
  **Help > Check for updates...**. The startup check runs in the background
  when the main window is first shown (not in server mode, nor hidden or
  minimized) and stays silent when the check fails or LitePM is up to date;
  when a new version exists, it offers the same installation dialog.

### Removed
- The old update checker (`cUpdate`, update file on SourceForge, which no
  longer answers), its "new version available" window
  (`frmNewVersionAvailable`) and its preferences: update server, alpha and
  beta releases (settings `UpdateServer`, `UpdateAlpha`, `UpdateBeta`).

## [1.1.4] - 2026-11-17

### Fixed
- Memory and handle leak when LitePM stays open for a long time (the reason
  the README asked to restart it every few hours):
  - `Misc.GetIcon` / `GetIcon2` leaked the HICON returned by `SHGetFileInfo`
    (one USER object per call, i.e. per process start shown in the process
    list). The icon is now copied and the original destroyed.
  - The process list added an image to its image list for every process start
    and never removed any. Images are now added once per executable path and
    unused ones are dropped (same for the task list).
  - `cProcess` never closed its token handle, and `Dispose` did nothing. Both
    handles are now closed on `Dispose` (and by the finalizer as a fallback);
    the process list disposes the entries of terminated processes.
  - `cProcess.GetProcessName` (called on every refresh for each network
    connection, service and window), the job list refresh, the search owner
    lookup and the new-process notification created a throw-away `cProcess`,
    opening process and token handles each time. They now read the cached
    process list instead.
  - A process handle leaked when the token of a process could not be opened,
    and `NtGetNextProcess` handles leaked in the fallback handle lookup.

### Added
- **Help > Check for updates...**: LitePM checks its latest GitHub release
  and, when a newer one exists, shows the versions and release notes, then
  (if accepted) downloads `LitePM.zip`, closes LitePM, replaces the files and
  restarts it. Built on `SelfUpdater` of LiteSuite.Common (`AppUpdater`):
  - the Authenticode signature of the new `LitePM.exe` is verified (pinned
    LiteSuite publisher or signer of the running `LitePM.exe`); a modified,
    foreign or missing signature is refused, and an unsigned package is
    offered, with a confirmation, only to an unsigned development build;
  - the LitePM remote service (`LitePM.exe -SERVERSERVICE`, found from its
    command line whatever its name) is stopped during the copy and restarted
    if it was running;
  - only the copy is elevated (UAC), and only when the folder is not writable
    or a service must be stopped; LitePM restarts without elevation;
  - files of the user next to `LitePM.exe` (`tcp.txt`, `udp.txt`) are kept;
    the settings, hotkeys and log are in the user profile and untouched;
  - nothing is replaced before the package is downloaded and verified; the
    replaced files are restored if the copy fails
    (log: `%TEMP%\LitePM-update.log`).
- `-REMOTE <computer>` command-line option: LitePM starts connected to that
  computer through WMI, with the current Windows user (no password asked or
  stored). LiteDeploy uses it to open a computer in LitePM. Only a computer
  name or address is accepted (letters, digits, `.`, `-`, `_`).
- WMI connections with an empty user name now use the current user instead of
  sending an empty user name and password.
- **Release** workflow (Actions > Release > Run workflow): bumps the version
  (patch / minor / major or an explicit version), commits and tags it on
  `main`, then builds and publishes the release. `bump-version.ps1` gains
  `-Bump`, refuses a dirty tree, a branch other than `main`, a version that is
  not greater than the current one or an existing tag.
- `docs/dotnet-migration.md`: assessment of a migration to .NET 10
  (blockers, effort, recommendation to stay on .NET Framework 4.8).
- `tools/soak/Measure-LitePM.ps1` and the **Memory soak** workflow: a
  multi-hour run under process churn that compares Private Bytes, handles,
  GDI and USER objects between `main` and a pull request.
- Authenticode signing in CI: `LitePM.exe`, `LitePMService.exe` and the DLLs
  built by the solution are signed (SHA-256, DigiCert timestamp) before the
  release zip is created, when the `SIGNING_CERTIFICATE` and
  `CERTIFICATE_PASSWORD` secrets exist. Each signature is verified
  (`tools/ci/Sign-Binaries.ps1`). Without the secrets, CI still exercises the
  signing script on a copy of the binaries with a throw-away self-signed
  certificate; the package then stays unsigned.
- CI check of the release package against the LiteTask contract (asset name,
  `LitePM.exe` and its dependencies at the zip root).

### Changed
- New dependency: the NuGet package `LiteSuite.Common` 1.1.0 (components
  shared with LiteDeploy and LiteTask), from GitHub Packages through
  `nuget.config`; `LiteSuite.Common.dll` ships next to `LitePM.exe`, is signed
  with the other binaries and is required by the CI package check. Restoring
  needs a token even for a public package: the CI uses its `GITHUB_TOKEN`
  (`packages: read`); on a development computer, set the user environment
  variable `LITESUITE_PACKAGES_TOKEN` to a personal access token with
  `read:packages` only.
- The release asset is now always `LitePM.zip` (was
  `LitePM-<version>-win-x64.zip`), so the latest release has a fixed link:
  `https://github.com/svtica/LitePM/releases/latest/download/LitePM.zip`.
  LiteTask 1.2.2.5 and later accept it; 1.2.2.3 and 1.2.2.4 need to update.
- All projects of the solution use the SDK-style project format, still
  targeting .NET Framework 4.8 (built assemblies unchanged). CI builds with
  `msbuild -restore`. ClickOnce/bootstrapper leftovers removed from
  `LitePM.vbproj`.
- Lighter user interface: the ribbon (and `System.Windows.Forms.Ribbon.dll`)
  is removed; LitePM always uses the menus and tabs. The ribbon control was
  built at every start even in menu mode, where the window title and some
  refreshes followed the hidden ribbon tab instead of the displayed one.
  Ribbon-only commands now in the menus: *Run a new process...* (File and
  Processes), *Help > About LitePM*, and in Files: file properties, open file
  directory, directory properties, Internet search.
- Only the displayed list is refreshed: tasks, jobs, network connections and
  the monitor graph are no longer refreshed every second in the background;
  services only when displayed or when service notifications are on. The
  process list (filter used by LiteTask, notifications) is unchanged.
- The system and network information windows are created when first opened
  and only refresh while visible (their timers ran from startup before).
- System information is sampled once per second instead of twice (the tray
  icon reused to refresh it, which also halved the CPU usage interval).
- Pushing a `v*` tag now builds and publishes the release (as
  `bump-version.ps1` already assumed); publishing a release still works.
- Branch build packages no longer break on branch names containing `/`.
- `softprops/action-gh-release` v1 -> v2.

### Removed
- `LitePM/LitePM_TemporaryKey.pfx` (unused ClickOnce temporary key); `*.pfx`
  files are now ignored by git.
- Ribbon option in Preferences (`UseRibbonStyle` setting).
- Entries for features that were not implemented: State based actions (and
  its 1-second timer that did nothing), TraceRoute and WhoIs network tools.
  The "Free memory" ribbon button (a forced garbage collection of LitePM
  itself) is not carried over.
- The memory leak warning and the "restart every few hours" workaround from
  the README.
