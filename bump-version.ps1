<#
.SYNOPSIS
    Bumps LitePM version, commits, tags, and pushes to trigger a GitHub release.
.PARAMETER Version
    The new version number (e.g., "1.0.1.0")
.EXAMPLE
    .\bump-version.ps1 -Version "1.0.1.0"
#>
param(
    [Parameter(Mandatory = $true)]
    [ValidatePattern('^\d+\.\d+\.\d+\.\d+$')]
    [string]$Version
)

$ErrorActionPreference = 'Stop'

# Derive 3-part version (Major.Minor.Build) for workflow and vbproj
$parts = $Version.Split('.')
$version3 = "$($parts[0]).$($parts[1]).$($parts[2])"

$assemblyInfoFile = Join-Path $PSScriptRoot 'LitePM\My Project\AssemblyInfo.vb'
$projectFile      = Join-Path $PSScriptRoot 'LitePM\LitePM.vbproj'
$manifestFile     = Join-Path $PSScriptRoot 'LitePM\My Project\app.manifest'
$workflowFile     = Join-Path $PSScriptRoot '.github\workflows\build-and-release.yml'

# Update AssemblyInfo.vb
$asm = Get-Content $assemblyInfoFile -Raw
$asm = $asm -replace '(<Assembly:\s*AssemblyVersion\(")[^"]+("\)>)', "`${1}$Version`${2}"
$asm = $asm -replace '(<Assembly:\s*AssemblyFileVersion\(")[^"]+("\)>)', "`${1}$Version`${2}"
Set-Content $assemblyInfoFile $asm -NoNewline

# Update LitePM.vbproj (ApplicationVersion uses 3-part + wildcard)
$proj = Get-Content $projectFile -Raw
$proj = $proj -replace '<ApplicationVersion>[^<]+</ApplicationVersion>', "<ApplicationVersion>$version3.%2a</ApplicationVersion>"
Set-Content $projectFile $proj -NoNewline

# Update app.manifest
$manifest = Get-Content $manifestFile -Raw
$manifest = $manifest -replace '(assemblyIdentity\s+version=")[^"]+(")', "`${1}$Version`${2}"
Set-Content $manifestFile $manifest -NoNewline

# Update build-and-release.yml (fallback version strings)
$wf = Get-Content $workflowFile -Raw
$wf = $wf -replace '(\$version\s*=\s*")[^"]+(")', "`${1}$version3`${2}"
Set-Content $workflowFile $wf -NoNewline

Write-Host "Version updated to $Version in:"
Write-Host "  - AssemblyInfo.vb (AssemblyVersion, AssemblyFileVersion)"
Write-Host "  - LitePM.vbproj (ApplicationVersion -> $version3.%2a)"
Write-Host "  - app.manifest (assemblyIdentity)"
Write-Host "  - build-and-release.yml (fallback `$version -> $version3)"

# Git operations
git add $assemblyInfoFile $projectFile $manifestFile $workflowFile
git commit -m "Bump version to $Version"
git tag "v$version3"
git push origin HEAD
git push origin "v$version3"

Write-Host "`nDone! Tag v$version3 pushed - GitHub release workflow will trigger automatically."
