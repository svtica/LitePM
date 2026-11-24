<#
.SYNOPSIS
    Checks the translation files of LitePM (LitePM\lang\*.xml).

.DESCRIPTION
    For each file: well-formed XML, a key and a source on every translation,
    unique keys, the {0}-style placeholders and [NAME]-style markers of the
    source kept in the translation, and for French the Quebec typography (a
    non-breaking space, never a plain space, before : ; ? !). Exits with 1 and
    lists the problems when one is found. Used by
    .github/workflows/build-and-release.yml; see docs/localization.md.
#>
param(
    [string] $Dir = (Join-Path $PSScriptRoot '..\..\LitePM\lang')
)
$ErrorActionPreference = 'Stop'
$problems = New-Object System.Collections.Generic.List[string]
$files = @(Get-ChildItem -Path $Dir -Filter '*.xml' -File)
if ($files.Count -eq 0) { Write-Host "No translation file in $Dir"; exit 1 }

foreach ($file in $files) {
    $doc = New-Object System.Xml.XmlDocument
    try { $doc.Load($file.FullName) } catch { $problems.Add("$($file.Name): not well formed: $($_.Exception.Message)"); continue }
    $keys = @{}
    $french = $file.BaseName -eq 'fr'
    foreach ($node in $doc.SelectNodes('/translations/translation')) {
        $key = $node.GetAttribute('key')
        $source = $node.GetAttribute('source')
        $text = $node.InnerText
        if (-not $key) { $problems.Add("$($file.Name): translation without key"); continue }
        if ($keys.ContainsKey($key)) { $problems.Add("$($file.Name): duplicate key $key") }
        $keys[$key] = $true
        if (-not $source) { $problems.Add("$($file.Name): $key has no source") }
        foreach ($m in [regex]::Matches($source, '\{\d+\}|\[[A-Z]+\]')) {
            if (-not $text.Contains($m.Value)) { $problems.Add("$($file.Name): $key lost $($m.Value)") }
        }
        if ($french -and [regex]::IsMatch($text, ' [:;?!]')) {
            $problems.Add("$($file.Name): $key has a plain space before : ; ? or ! (use U+00A0)")
        }
    }
    Write-Host "$($file.Name): $($keys.Count) translations"
}

if ($problems.Count -gt 0) {
    $problems | ForEach-Object { Write-Host "::error::$_" }
    exit 1
}
Write-Host 'Translations OK'
