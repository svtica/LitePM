<#
.SYNOPSIS
    Bumps LitePM version, commits, tags and pushes to publish a GitHub release.

.DESCRIPTION
    Updates every version location (AssemblyInfo.vb, LitePM.vbproj,
    app.manifest), commits "Bump version to X"
    on main, tags v<Major.Minor.Build> and pushes both.

    Locally, pushing the tag triggers the "Build and Release" workflow. The
    "Release" workflow (Actions tab > Release > Run workflow) runs this script
    with -Bump or -Version and then builds the release itself, because a tag
    pushed from a workflow does not trigger other workflows.

.PARAMETER Version
    The new version, 4 parts (e.g. "1.0.2.0").

.PARAMETER Bump
    Instead of -Version: increment the current version (patch = Build part,
    e.g. 1.0.1.0 -> 1.0.2.0; minor = 1.0.1.0 -> 1.1.0.0; major -> 2.0.0.0).

.EXAMPLE
    .\bump-version.ps1 -Version "1.0.2.0"
.EXAMPLE
    .\bump-version.ps1 -Bump patch
#>
[CmdletBinding(DefaultParameterSetName = 'Explicit')]
param(
    [Parameter(Mandatory = $true, ParameterSetName = 'Explicit')]
    [ValidatePattern('^\d+\.\d+\.\d+\.\d+$')]
    [string]$Version,

    [Parameter(Mandatory = $true, ParameterSetName = 'Bump')]
    [ValidateSet('patch', 'minor', 'major')]
    [string]$Bump
)

$ErrorActionPreference = 'Stop'

$assemblyInfoFile = Join-Path $PSScriptRoot 'LitePM\My Project\AssemblyInfo.vb'
$projectFile      = Join-Path $PSScriptRoot 'LitePM\LitePM.vbproj'
$manifestFile     = Join-Path $PSScriptRoot 'LitePM\My Project\app.manifest'

# Refuse to tag a dirty tree or a branch other than main: the tag would publish
# a release that does not match what is on GitHub.
if (git status --porcelain) { throw 'Working tree has uncommitted changes; commit or stash them first.' }
$branch = (git rev-parse --abbrev-ref HEAD).Trim()
if ($branch -ne 'main') { throw "Releases are tagged from main (current branch: $branch)." }
git pull --ff-only origin main
if ($LASTEXITCODE -ne 0) { throw 'git pull --ff-only failed; resolve it before bumping the version.' }

# Current version (AssemblyVersion, the line that is not commented out)
$asm = Get-Content $assemblyInfoFile -Raw
$m = [regex]::Match($asm, '(?m)^<Assembly:\s*AssemblyVersion\("(\d+)\.(\d+)\.(\d+)\.(\d+)"\)>')
if (-not $m.Success) { throw "AssemblyVersion not found in $assemblyInfoFile" }
$current = "$($m.Groups[1].Value).$($m.Groups[2].Value).$($m.Groups[3].Value).$($m.Groups[4].Value)"

if ($PSCmdlet.ParameterSetName -eq 'Bump') {
    $major = [int]$m.Groups[1].Value; $minor = [int]$m.Groups[2].Value; $build = [int]$m.Groups[3].Value
    switch ($Bump) {
        'major' { $major++; $minor = 0; $build = 0 }
        'minor' { $minor++; $build = 0 }
        'patch' { $build++ }
    }
    $Version = "$major.$minor.$build.0"
}

# Derive 3-part version (Major.Minor.Build) for the tag, workflow and vbproj
$parts = $Version.Split('.')
$version3 = "$($parts[0]).$($parts[1]).$($parts[2])"
$tag = "v$version3"

if ([version]$Version -le [version]$current) { throw "New version $Version must be greater than the current $current." }
git fetch --tags --quiet origin
if (git tag --list $tag) { throw "Tag $tag already exists (release tags use Major.Minor.Build)." }

Write-Host "Bumping $current -> $Version (tag $tag)"

# Update AssemblyInfo.vb
$asm = $asm -replace '(?m)^(<Assembly:\s*AssemblyVersion\(")[^"]+("\)>)', "`${1}$Version`${2}"
$asm = $asm -replace '(?m)^(<Assembly:\s*AssemblyFileVersion\(")[^"]+("\)>)', "`${1}$Version`${2}"
Set-Content $assemblyInfoFile $asm -NoNewline

# Update LitePM.vbproj (ApplicationVersion uses 3-part + wildcard)
$proj = Get-Content $projectFile -Raw
$proj = $proj -replace '<ApplicationVersion>[^<]+</ApplicationVersion>', "<ApplicationVersion>$version3.%2a</ApplicationVersion>"
Set-Content $projectFile $proj -NoNewline

# Update app.manifest
$manifest = Get-Content $manifestFile -Raw
$manifest = $manifest -replace '(assemblyIdentity\s+version=")[^"]+(")', "`${1}$Version`${2}"
Set-Content $manifestFile $manifest -NoNewline

# (build-and-release.yml reads the version from AssemblyInfo.vb: a workflow
# token cannot push changes to workflow files.)

Write-Host "Version updated to $Version in:"
Write-Host "  - AssemblyInfo.vb (AssemblyVersion, AssemblyFileVersion)"
Write-Host "  - LitePM.vbproj (ApplicationVersion -> $version3.%2a)"
Write-Host "  - app.manifest (assemblyIdentity)"

# Git operations
git add $assemblyInfoFile $projectFile $manifestFile
git commit -m "Bump version to $Version"
if ($LASTEXITCODE -ne 0) { throw 'git commit failed.' }
git tag $tag
git push origin HEAD
if ($LASTEXITCODE -ne 0) { throw 'git push of main failed (branch protection?).' }
git push origin $tag
if ($LASTEXITCODE -ne 0) { throw "git push of $tag failed." }

# Expose the result to a calling workflow
if ($env:GITHUB_OUTPUT) {
    Add-Content -Path $env:GITHUB_OUTPUT -Value "version=$Version"
    Add-Content -Path $env:GITHUB_OUTPUT -Value "tag=$tag"
}

Write-Host "`nDone! $tag pushed."
