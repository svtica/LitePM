<#
.SYNOPSIS
    Authenticode-signs the LitePM binaries of a folder and verifies them.

.DESCRIPTION
    Signs, in -Dir, the executables and DLLs built by the solution
    (System.Windows.Forms.Ribbon.dll is third-party and left as is) with
    signtool from the latest Windows 10/11 SDK, SHA-256 file digest and an
    RFC 3161 timestamp, then checks each signature with "signtool verify /pa".
    Used by .github/workflows/build-and-release.yml. The certificate password
    is read from the environment (never passed on the command line of the
    caller or written to the log).
#>
param(
    [Parameter(Mandatory = $true)] [string] $Dir,
    [Parameter(Mandatory = $true)] [string] $CertPath,
    [string] $PasswordEnvVar = 'CERTIFICATE_PASSWORD',
    [string] $TimestampUrl = 'http://timestamp.digicert.com'
)
$ErrorActionPreference = 'Stop'

$signtool = Get-ChildItem -Path "C:\Program Files (x86)\Windows Kits\10\bin\*\x64\signtool.exe" -ErrorAction SilentlyContinue |
            Sort-Object FullName -Descending |
            Select-Object -First 1
if (-not $signtool) { throw "signtool.exe not found in Windows SDK" }
Write-Host "Using signtool: $($signtool.FullName)"

$password = [Environment]::GetEnvironmentVariable($PasswordEnvVar)

$names = @(
    'LitePM.exe',
    'LitePMService.exe',
    'Aga.Controls.dll',
    'TaskDialog.dll',
    'SecurePasswordTextBox.dll',
    'SecureChannel.dll'
)
$files = $names | ForEach-Object { Join-Path $Dir $_ } | Where-Object { Test-Path $_ }
if (-not ($files | Where-Object { $_ -like '*LitePM.exe' })) { throw "LitePM.exe not found in $Dir" }

foreach ($file in $files) {
    Write-Host "Signing: $file"
    $signArgs = @('sign', '/f', $CertPath)
    if ($password) { $signArgs += @('/p', $password) }
    $signArgs += @('/tr', $TimestampUrl, '/td', 'sha256', '/fd', 'sha256', $file)
    & $signtool.FullName @signArgs
    if ($LASTEXITCODE -ne 0) { throw "Failed to sign $file" }

    & $signtool.FullName verify /pa $file
    if ($LASTEXITCODE -ne 0) { throw "Signature verification failed for $file" }
}
Write-Host "Signed and verified $(@($files).Count) file(s)."
