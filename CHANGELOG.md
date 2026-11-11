# Changelog

All notable changes to LitePM are documented in this file.
The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

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

### Changed
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

### Removed
- Ribbon option in Preferences (`UseRibbonStyle` setting).
- Entries for features that were not implemented: State based actions (and
  its 1-second timer that did nothing), TraceRoute and WhoIs network tools.
  The "Free memory" ribbon button (a forced garbage collection of LitePM
  itself) is not carried over.

### Added
- `tools/soak/Measure-LitePM.ps1` and the **Memory soak** workflow: a
  multi-hour run under process churn that compares Private Bytes, handles,
  GDI and USER objects between `main` and a pull request.

### Removed
- The memory leak warning and the "restart every few hours" workaround from
  the README.
