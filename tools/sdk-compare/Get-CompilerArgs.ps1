# Temporary (SDK-style migration check): extracts the vbc/csc command line of
# each project from a detailed msbuild log, one sorted argument per line.
param([Parameter(Mandatory)] [string] $Log, [Parameter(Mandatory)] [string] $Root, [Parameter(Mandatory)] [string] $Out)
$result = New-Object System.Collections.Generic.List[string]
foreach ($line in Get-Content $Log) {
    if ($line -notmatch '(vbc|csc)(\.exe|\.dll)"?\s+/noconfig') { continue }
    $cmd = $line.Substring($line.IndexOf('/noconfig'))
    $out = if ($cmd -match '/out:("[^"]+"|\S+)') { Split-Path ($Matches[1].Trim('"')) -Leaf } else { 'unknown' }
    $tokens = [regex]::Matches($cmd, '("[^"]*"|\S)+') | ForEach-Object { $_.Value.Trim('"') }
    foreach ($t in $tokens) {
        $t = $t.Replace($Root, '<ROOT>')
        # framework reference paths differ between the legacy and SDK resolution
        $t = $t -replace '^/reference:.*\\([^\\]+)$', '/reference:$1'
        $result.Add("$out $t")
    }
}
$result | Sort-Object -Unique | Set-Content -Path $Out -Encoding UTF8
Write-Host "$($result.Count) arguments -> $Out"
