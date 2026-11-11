# Temporary (SDK-style migration check): extracts the vbc/csc command line of
# each project from a detailed msbuild log, one sorted argument per line.
param([Parameter(Mandatory)] [string] $Log, [Parameter(Mandatory)] [string] $Root, [Parameter(Mandatory)] [string] $Out)
$result = New-Object System.Collections.Generic.List[string]
$hits = 0
foreach ($line in Get-Content $Log) {
    if ($line -notmatch '(vbc|csc)(\.exe|\.dll)"?\s+/noconfig') { continue }
    $hits++
    $cmd = $line.Substring($line.IndexOf('/noconfig'))
    $target = if ($cmd -match '/out:("[^"]+"|\S+)') { Split-Path ($Matches[1].Trim('"')) -Leaf } else { 'unknown' }
    $tokens = [regex]::Matches($cmd, '("[^"]*"|\S)+') | ForEach-Object { $_.Value.Trim('"') }
    foreach ($t in $tokens) {
        $t = $t.Replace($Root, '<ROOT>')
        # framework reference paths differ between the legacy and SDK resolution
        $t = $t -replace '^/reference:.*\\([^\\]+)$', '/reference:$1'
        $result.Add("$target $t")
    }
}
Set-Content -Path $Out -Encoding UTF8 -Value (($result | Sort-Object -Unique) -join "`r`n")
Write-Host "$hits compiler command lines, $($result.Count) arguments -> $Out"
