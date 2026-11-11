# LitePM soak test

`Measure-LitePM.ps1` runs LitePM for a long time and records how its resource
usage evolves, to catch leaks that only show after hours of uptime.

What it does:

- starts `LitePM.exe -filter "Lite"` with the LitePM folder as working
  directory (the way LiteTask launches it);
- keeps starting short-lived processes (copies of `ping.exe` under several
  names, ~30 per minute plus their `conhost.exe`), so LitePM constantly sees
  processes appear and disappear;
- samples every 30 s: Private Bytes, Working Set, kernel handles, GDI objects,
  USER objects, threads and the .NET GC heap;
- writes a CSV and a Markdown summary: value after the warm-up (short run),
  value at the end, and the linear growth per hour after the warm-up.

A healthy build has a growth per hour close to zero for every counter.

## Running it

In CI: add the `soak` label (180 min) or `soak-quick` label (30 min) to a pull
request, or run the **Memory soak** workflow manually. The workflow builds
`main` and the pull request side by side and shows both summaries.

Locally (Windows, PowerShell 5.1+):

```powershell
.\tools\soak\Measure-LitePM.ps1 -ExePath .\LitePM\bin\Release\LitePM.exe -DurationMinutes 240
```
