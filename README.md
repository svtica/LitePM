# LitePM

[![Part of LiteSuite](https://img.shields.io/badge/part%20of-LiteSuite-blue)](https://github.com/svtica/LiteSuite)
[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-green.svg)](LICENSE)

**Advanced Windows process manager with real-time monitoring, process control, and system optimization tools.**

LitePM is a comprehensive process management application that provides detailed system insights, process control capabilities, and performance monitoring tools for Windows systems.

## Features

### Process Management
- **Processes**: live list with CPU, memory, handles, threads, modules and environment of each process; hidden
  process detection
- **Process Control**: kill (also by method or the whole tree), suspend and resume, priority and affinity,
  reduce the working set, dump file, add to a job
- **Jobs**: create jobs, set their limits, terminate them

### Services
- Start, stop, pause and resume services; change the start type; create a service; dependency viewer;
  new and deleted services

### Monitoring and Search
- **Monitor**: performance counters of selected processes over time
- **Notifications and log**: new and terminated processes, new and deleted services
- **Search**: find a string in processes, modules, services, handles, windows and environment variables
- **Emergency hotkeys** and state-based actions

### Network, Files and System
- **Network**: TCP/UDP connections with their process, close a TCP connection, ping and network information
- **Files**: properties, attributes and dates, strings, release a locked file, encrypt or decrypt
- **System**: system information, system report and snapshot files, signature check of files, opened windows,
  log off, lock, sleep, hibernate, restart, shutdown
- **Remote**: manage another computer through WMI or the LitePM server

## Installation

1. Download the latest release
2. Extract files to desired location
3. Run `LitePM.exe`
4. **Note**: Some features require administrative privileges for full functionality

## Usage

### Main Interface
- Tabs: Tasks, Processes, Jobs, Monitor, Services, Network, File and Search
- System information from **System > System infos**

### Process Operations
- Right-click processes for context menu with control options
- Use toolbar buttons for common operations
- Filter processes using the search functionality
- Monitor resource usage with real-time updates

### Service Management
- Start, stop, pause, and resume Windows services
- View service dependencies and startup types
- Monitor service status changes in real-time

### Command line
- `LitePM.exe -REMOTE <computer>`: open LitePM connected to a remote computer
  through WMI, as the current Windows user (used by LiteDeploy). Needs
  administrator rights and WMI access on that computer.

## Technology Stack

- **Platform**: .NET Framework 4.8 (SDK-style projects; see [docs/dotnet-migration.md](docs/dotnet-migration.md) for .NET 10)
- **Language**: VB.NET
- **UI**: Windows Forms
- **System APIs**: WMI, Windows APIs for process management

## Development Status

This project is **Production Ready** but **not actively developed**. Moderate contributions are accepted for bug fixes and minor improvements.

## System Requirements

- Windows 7 SP1 or later (required by .NET Framework 4.8)
- .NET Framework 4.8
- Administrative privileges recommended for full functionality

## Releases and code signing

To publish a release, run **Actions > Release > Run workflow** on GitHub and
choose the increment (patch, minor, major) or type a version. The workflow
updates the version, commits it on `main`, tags `v<Major.Minor.Build>` and builds
and publishes the release. From a local clone on `main`, the same is done with
`.\bump-version.ps1 -Bump patch` (or `-Version 1.2.3.0`): pushing the tag
triggers `.github/workflows/build-and-release.yml`. The release asset is always named `LitePM.zip`
(latest: https://github.com/svtica/LitePM/releases/latest/download/LitePM.zip), with `LitePM.exe` and
all its DLLs and `.config` at the root of the zip: LiteTask downloads this asset
(Tools > Process manager) and keeps that layout.

`LitePM.exe`, `LitePMService.exe` and the DLLs built by the solution are signed
with Authenticode (SHA-256, RFC 3161 timestamp) when the repository has these
Actions secrets:

| Secret | Content |
|---|---|
| `SIGNING_CERTIFICATE` | the code-signing `.pfx`, base64-encoded |
| `CERTIFICATE_PASSWORD` | the `.pfx` password |

Without them the build still succeeds and the package is unsigned (a warning is
shown on tag builds). Never commit a certificate or password to the repository.

## License

This software is released under [The Unlicense](LICENSE) - public domain.

---

*LitePM provides essential process management capabilities for Windows administrators and power users.*

## 🌟 Part of LiteSuite

This tool is part of **[LiteSuite](https://github.com/svtica/LiteSuite)** - a comprehensive collection of lightweight Windows administration tools.

### Other Tools in the Suite:
- **[LiteTask](https://github.com/svtica/LiteTask)** - Advanced Task Scheduler Alternative
- **[LitePM](https://github.com/svtica/LitePM)** - Process Manager with System Monitoring
- **[LiteDeploy](https://github.com/svtica/LiteDeploy)** - Network Deployment and Management
- **[LiteRun](https://github.com/svtica/LiteRun)** - Remote Command Execution Utility (archived)
- **[LiteSrv](https://github.com/svtica/LiteSrv)** - Windows Service Wrapper (archived)

### 📦 Download the Complete Suite
Get all tools in one package: **[LiteSuite Releases](https://github.com/svtica/LiteSuite/releases/latest)**

---

*LiteSuite - Professional Windows administration tools for modern IT environments.*
