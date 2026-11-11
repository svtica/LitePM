<#
.SYNOPSIS
    Soak test for LitePM: runs LitePM.exe for a long time under synthetic
    process churn and samples its resource counters.

.DESCRIPTION
    Launches LitePM the same way LiteTask does (-filter "Lite", working
    directory = LitePM folder), then, for DurationMinutes:
      - starts short-lived processes (several distinct image paths) so that
        LitePM keeps seeing processes appear and disappear;
      - samples Private Bytes, Working Set, kernel handles, GDI objects,
        USER objects, thread count and the .NET GC heap size.

    Writes one CSV row per sample and a Markdown summary (short-run value,
    end value, growth per hour after warm-up) to SummaryPath.

.EXAMPLE
    .\Measure-LitePM.ps1 -ExePath ..\..\LitePM\bin\Release\LitePM.exe -DurationMinutes 180
#>
param(
    [Parameter(Mandatory = $true)] [string] $ExePath,
    [int]    $DurationMinutes = 180,
    [int]    $SampleSeconds   = 30,
    [int]    $WarmupMinutes   = 10,
    # Short-lived processes started every ChurnSeconds (0 disables churn).
    [int]    $ChurnBatch      = 5,
    [int]    $ChurnSeconds    = 10,
    [string] $Label           = 'run',
    [string] $OutCsv          = "soak-$Label.csv",
    [string] $SummaryPath     = "soak-$Label.md"
)

$ErrorActionPreference = 'Stop'

Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class SoakNative {
    [DllImport("user32.dll")]
    public static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);
}
'@

$exe = (Resolve-Path $ExePath).Path
$exeDir = Split-Path $exe -Parent

# Distinct image paths for churn processes (copies of ping.exe), so that the
# per-path code (icons, version info) is exercised as well as the per-PID code.
$churnDir = Join-Path $env:RUNNER_TEMP "litepm-soak-churn"
if (-not $env:RUNNER_TEMP) { $churnDir = Join-Path $env:TEMP "litepm-soak-churn" }
New-Item -ItemType Directory -Force -Path $churnDir | Out-Null
$ping = Join-Path $env:SystemRoot 'System32\PING.EXE'
$churnExes = for ($i = 1; $i -le $ChurnBatch; $i++) {
    $dst = Join-Path $churnDir "churn$i.exe"
    Copy-Item $ping $dst -Force
    $dst
}

Write-Host "Starting $exe -filter Lite (cwd $exeDir)"
$proc = Start-Process -FilePath $exe -ArgumentList '-filter', 'Lite' -WorkingDirectory $exeDir -PassThru
# Startup time: until the main window exists
$startWatch = [Diagnostics.Stopwatch]::StartNew()
while ($startWatch.Elapsed.TotalSeconds -lt 60) {
    $proc.Refresh()
    if ($proc.HasExited -or $proc.MainWindowHandle -ne [IntPtr]::Zero) { break }
    Start-Sleep -Milliseconds 100
}
$startupSeconds = [math]::Round($startWatch.Elapsed.TotalSeconds, 1)
Write-Host "Main window after $startupSeconds s"
Start-Sleep -Seconds 15
if ($proc.HasExited) { throw "LitePM exited right after start (exit code $($proc.ExitCode))" }

$gcCounter = $null
try {
    $instance = [IO.Path]::GetFileNameWithoutExtension($exe)
    $gcCounter = "\.NET CLR Memory($instance)\# Bytes in all Heaps"
    Get-Counter -Counter $gcCounter -ErrorAction Stop | Out-Null
} catch {
    Write-Host "GC heap counter unavailable: $($_.Exception.Message)"
    $gcCounter = $null
}

$rows = New-Object System.Collections.Generic.List[object]
$sw = [Diagnostics.Stopwatch]::StartNew()
$nextSample = 0
$nextChurn = 0
$churnStarted = 0
$crashed = $false
$lastCpu = $proc.TotalProcessorTime.TotalSeconds
$lastCpuAt = $sw.Elapsed.TotalSeconds

while ($sw.Elapsed.TotalMinutes -lt $DurationMinutes) {
    $t = $sw.Elapsed.TotalSeconds

    if ($ChurnBatch -gt 0 -and $t -ge $nextChurn) {
        foreach ($c in $churnExes) {
            # ~2 s lifetime: long enough for a 1 s refresh to see it.
            Start-Process -FilePath $c -ArgumentList '-n', '3', '127.0.0.1' -WindowStyle Hidden | Out-Null
            $churnStarted++
        }
        $nextChurn = $t + $ChurnSeconds
    }

    if ($t -ge $nextSample) {
        $proc.Refresh()
        if ($proc.HasExited) {
            Write-Host "::error::LitePM exited after $([math]::Round($sw.Elapsed.TotalMinutes,1)) min (exit code $($proc.ExitCode))"
            $crashed = $true
            break
        }
        # Average CPU of LitePM since the previous sample, in % of one core
        $cpuNow = $proc.TotalProcessorTime.TotalSeconds
        $cpuPercent = [math]::Round(100 * ($cpuNow - $lastCpu) / [math]::Max(0.001, $t - $lastCpuAt), 2)
        $lastCpu = $cpuNow; $lastCpuAt = $t
        $gc = $null
        if ($gcCounter) {
            try { $gc = (Get-Counter -Counter $gcCounter -ErrorAction Stop).CounterSamples[0].CookedValue } catch { $gc = $null }
        }
        $row = [pscustomobject]@{
            ElapsedMin   = [math]::Round($sw.Elapsed.TotalMinutes, 2)
            PrivateMB    = [math]::Round($proc.PrivateMemorySize64 / 1MB, 1)
            WorkingSetMB = [math]::Round($proc.WorkingSet64 / 1MB, 1)
            Handles      = $proc.HandleCount
            GdiObjects   = [SoakNative]::GetGuiResources($proc.Handle, 0)
            UserObjects  = [SoakNative]::GetGuiResources($proc.Handle, 1)
            Threads      = $proc.Threads.Count
            CpuPercent   = $cpuPercent
            GcHeapMB     = if ($null -ne $gc) { [math]::Round($gc / 1MB, 1) } else { '' }
            ChurnStarted = $churnStarted
        }
        $rows.Add($row)
        Write-Host ("{0,7:N1} min  priv {1,7:N1} MB  ws {2,7:N1} MB  handles {3,6}  gdi {4,5}  user {5,5}  thr {6,4}  cpu {8,6:N2} %  gc {7}" -f `
            $row.ElapsedMin, $row.PrivateMB, $row.WorkingSetMB, $row.Handles, $row.GdiObjects, $row.UserObjects, $row.Threads, $row.GcHeapMB, $row.CpuPercent)
        $nextSample = $t + $SampleSeconds
    }

    Start-Sleep -Milliseconds 500
}

if (-not $proc.HasExited) { Stop-Process -Id $proc.Id -Force }
$rows | Export-Csv -Path $OutCsv -NoTypeInformation -Encoding UTF8

# ---- Summary --------------------------------------------------------------
function Get-Slope($xs, $ys) {
    $n = $xs.Count
    if ($n -lt 2) { return 0 }
    $mx = ($xs | Measure-Object -Average).Average
    $my = ($ys | Measure-Object -Average).Average
    $num = 0.0; $den = 0.0
    for ($i = 0; $i -lt $n; $i++) {
        $num += ($xs[$i] - $mx) * ($ys[$i] - $my)
        $den += ($xs[$i] - $mx) * ($xs[$i] - $mx)
    }
    if ($den -eq 0) { return 0 }
    return $num / $den
}

$metrics = 'PrivateMB', 'WorkingSetMB', 'Handles', 'GdiObjects', 'UserObjects', 'Threads', 'GcHeapMB', 'CpuPercent'
$steady = @($rows | Where-Object { $_.ElapsedMin -ge $WarmupMinutes })
$short = $steady | Select-Object -First 1
$last = $rows | Select-Object -Last 1

$md = New-Object System.Text.StringBuilder
[void]$md.AppendLine("### LitePM soak - $Label")
[void]$md.AppendLine()
[void]$md.AppendLine("Duration: $([math]::Round($last.ElapsedMin,1)) min, churn processes started: $churnStarted" + $(if ($crashed) { ' - **LitePM exited before the end**' } else { '' }))
[void]$md.AppendLine("Main window shown after $startupSeconds s. Average CPU after warm-up: $([math]::Round((@($steady | Measure-Object CpuPercent -Average).Average), 2)) % of one core.")
[void]$md.AppendLine()
[void]$md.AppendLine("| Counter | At $WarmupMinutes min (short run) | At end | Growth per hour (after warm-up) |")
[void]$md.AppendLine("|---|---:|---:|---:|")
foreach ($m in $metrics) {
    $pts = @($steady | Where-Object { $_.$m -ne '' -and $null -ne $_.$m })
    if ($pts.Count -lt 2) { continue }
    $slope = Get-Slope ([double[]]($pts | ForEach-Object { $_.ElapsedMin })) ([double[]]($pts | ForEach-Object { [double]$_.$m }))
    [void]$md.AppendLine(("| {0} | {1} | {2} | {3:+0.0;-0.0;0} |" -f $m, $short.$m, $last.$m, ($slope * 60)))
}
$summary = $md.ToString()
Set-Content -Path $SummaryPath -Value $summary -Encoding UTF8
Write-Host $summary
if ($env:GITHUB_STEP_SUMMARY) { Add-Content -Path $env:GITHUB_STEP_SUMMARY -Value $summary }
if ($crashed) { exit 2 }
