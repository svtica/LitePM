# LitePM

[![Part of LiteSuite](https://img.shields.io/badge/part%20of-LiteSuite-blue)](https://github.com/svtica/LiteSuite)
[![License: Unlicense](https://img.shields.io/badge/license-Unlicense-green.svg)](LICENSE)

**Advanced Windows process manager with real-time monitoring, process control, and system optimization tools.**

LitePM is a comprehensive process management application that provides detailed system insights, process control capabilities, and performance monitoring tools for Windows systems.

## Features

### Process Management
- **Real-time Process Monitoring**: Live view of all running processes with detailed information
- **Process Control**: Terminate, suspend, resume, and manage process priorities
- **Process Tree View**: Hierarchical display showing parent-child relationships
- **Service Management**: Control Windows services and view service dependencies

### System Information
- **Hardware Details**: CPU, memory, storage, and network adapter information
- **Performance Metrics**: Real-time CPU usage, memory consumption, and system statistics
- **Network Monitoring**: Active connections, listening ports, and network statistics
- **System Diagnostics**: Event log viewing and system health monitoring

### Advanced Features
- **Process Filtering**: Search and filter processes by name, PID, or resource usage
- **Memory Analysis**: Detailed memory usage breakdown and working set information
- **Module Information**: View loaded modules and DLLs for processes
- **Handle Monitoring**: Track file handles, registry keys, and other system resources

### System Tools
- **Cleanup Utilities**: Temporary file cleanup and system optimization
- **Registry Tools**: Safe registry viewing and basic management
- **Performance Analysis**: CPU and memory usage trending
- **Export Capabilities**: Export process lists and system information

## Installation

1. Download the latest release
2. Extract files to desired location
3. Run `LitePM.exe`
4. **Note**: Some features require administrative privileges for full functionality

## Usage

### Main Interface
- **Processes Tab**: View and manage running processes
- **Services Tab**: Control Windows services
- **Performance Tab**: Monitor system performance metrics
- **System Info Tab**: View hardware and system details

### Process Operations
- Right-click processes for context menu with control options
- Use toolbar buttons for common operations
- Filter processes using the search functionality
- Monitor resource usage with real-time updates

### Service Management
- Start, stop, pause, and resume Windows services
- View service dependencies and startup types
- Monitor service status changes in real-time

## Technology Stack

- **Platform**: .NET Framework 4.8 (SDK-style projects; see [docs/dotnet-migration.md](docs/dotnet-migration.md) for .NET 10)
- **Language**: VB.NET
- **UI**: Windows Forms
- **System APIs**: WMI, Windows APIs for process management

## Development Status

This project is **Production Ready** but **not actively developed**. Moderate contributions are accepted for bug fixes and minor improvements.

## System Requirements

- Windows 7 or later
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
- **[LiteRun](https://github.com/svtica/LiteRun)** - Remote Command Execution Utility
- **[LiteSrv](https://github.com/svtica/LiteSrv)** - Windows Service Wrapper

### 📦 Download the Complete Suite
Get all tools in one package: **[LiteSuite Releases](https://github.com/svtica/LiteSuite/releases/latest)**

---

*LiteSuite - Professional Windows administration tools for modern IT environments.*
