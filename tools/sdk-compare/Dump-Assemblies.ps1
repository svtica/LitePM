# Temporary (SDK-style migration check): dumps what matters in every built
# assembly of a folder so that two builds can be diffed.
param([Parameter(Mandatory)] [string] $Dir, [Parameter(Mandatory)] [string] $Out)
$ErrorActionPreference = 'Continue'
$lines = New-Object System.Collections.Generic.List[string]
$files = Get-ChildItem $Dir -File | Sort-Object Name
foreach ($f in $files) { $lines.Add("FILE $($f.Name)") }
foreach ($f in $files | Where-Object { $_.Extension -in '.exe', '.dll' }) {
    try { $asm = [Reflection.Assembly]::LoadFrom($f.FullName) } catch { $lines.Add("NOTNET $($f.Name)"); continue }
    $n = $f.Name
    $lines.Add("$n NAME $($asm.GetName().FullName)")
    $pek = [Reflection.PortableExecutableKinds]0; $mach = [Reflection.ImageFileMachine]::I386
    $asm.ManifestModule.GetPEKind([ref]$pek, [ref]$mach)
    $lines.Add("$n PE $pek $mach")
    foreach ($r in $asm.GetReferencedAssemblies() | Sort-Object FullName) { $lines.Add("$n REF $($r.FullName)") }
    foreach ($r in $asm.GetManifestResourceNames() | Sort-Object) { $lines.Add("$n RES $r") }
    foreach ($a in $asm.GetCustomAttributesData() | ForEach-Object { $_.ToString() } | Sort-Object) { $lines.Add("$n ATTR $a") }
    try { $types = $asm.GetTypes() } catch [Reflection.ReflectionTypeLoadException] { $types = $_.Exception.Types | Where-Object { $_ } }
    foreach ($t in $types | ForEach-Object { $_.FullName } | Sort-Object) { $lines.Add("$n TYPE $t") }
}
$lines | Set-Content -Path $Out -Encoding UTF8
Write-Host "$($lines.Count) lines -> $Out"
