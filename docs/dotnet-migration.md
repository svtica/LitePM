# .NET 10 migration assessment

Status: **not migrated** — LitePM stays on .NET Framework 4.8 (supported as a
Windows component). The projects were converted to the SDK-style format
(still `net48`) so that a later move is a target-framework change plus the
work below, not a project-system rewrite.

## Blockers for `net10.0-windows`

| Blocker | Where | Why it blocks | Way out |
|---|---|---|---|
| `ContextMenu`, `MainMenu`, `MenuItem`, `StatusBar` | ~460 declarations in 37 files, mostly `*.Designer.vb` (every context menu and the main menus) | Removed from Windows Forms in .NET Core 3.1 | Rewrite with `ContextMenuStrip` / `ToolStripMenuItem` / `StatusStrip`, form by form (UI rework, needs manual testing of every menu) |
| .NET Remoting (`System.Runtime.Remoting`, TCP channels, `SecureChannel` sinks, `MarshalByRefObject` server) | `Classes/Socket/*`, `SecureChannel` project | Not available on .NET Core / .NET 5+ | Rewrite the remote / server mode on another transport (named pipes, gRPC) or drop the feature |
| `BinaryFormatter` | `Classes/Socket/cSerialization.vb` (remote protocol and snapshot files `cSnapshot*`) | Removed in .NET 9 (always throws) | New serializer; existing snapshot files would no longer open without a converter |
| `Thread.Abort` | `frmProcessInfo`, `frmServiceInfo`, `Aga.Controls/Threading/AbortableThreadPool` | Throws `PlatformNotSupportedException` | Cooperative cancellation (small) |
| `System.Windows.Forms.Ribbon.dll` (precompiled, `res/`) | main window | .NET Framework 1.0.0.0 build, untested on .NET | Probably loads through the compatibility shims; otherwise the RibbonWinForms package for `net8.0-windows`+ (API to check) |

Not blocking: P/Invoke declarations (~410, plain Win32/NT calls), WMI
(`System.Management` package), `ServiceBase` in `InteractiveProcessFromService`
(`System.ServiceProcess.ServiceController` package), the VB `My` namespace,
`Aga.Controls` and `MemoryHexEditor` (source code in the repository).

## Estimate

- Menus: 3–5 days plus a full manual UI pass.
- Remote mode and snapshots: 3–5 days to rewrite, or a product decision to
  remove them.
- Everything else: 1–2 days.

Roughly 2–3 weeks with significant regression risk on the UI, for no
functional gain for LiteTask (which only needs the local process view).

## Recommendation

Stay on `net48`. Revisit only if one of these changes: .NET Framework 4.8
support, a need for a feature only available on modern .NET, or a decision
to drop the remote mode (which removes two of the four blockers).

## SDK-style conversion (done)

All projects of `LitePM.sln` use `Microsoft.NET.Sdk` with `net48`, kept
equivalent to the former project files: explicit item lists, existing
`AssemblyInfo` files, `bin\<Configuration>\` outputs, Option Infer Off and
no implicit imports for VB, empty root namespace for LitePM. Equivalence was
checked in CI by building `main` and the converted solution side by side:
the built assemblies are identical in types, embedded resources, referenced
assemblies, attributes, PE kind and output file list. Compiler arguments only
differ by defaults made explicit, unused framework references and the
`RELEASE` symbol, which no code uses.

`LitePM/UserControls/MemoryHexEditor/*.vbproj` (and its `Test` project) are
not part of the solution and were left as they are.
