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

### Added
- `tools/soak/Measure-LitePM.ps1` and the **Memory soak** workflow: a
  multi-hour run under process churn that compares Private Bytes, handles,
  GDI and USER objects between `main` and a pull request.

### Removed
- The memory leak warning and the "restart every few hours" workaround from
  the README.
