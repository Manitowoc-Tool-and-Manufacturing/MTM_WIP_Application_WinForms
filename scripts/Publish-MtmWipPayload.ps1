#Requires -Version 5.1
<#
.SYNOPSIS
    Publishes the MTM WIP Application as a payload for the MTM Loader (WinUI 3).

.DESCRIPTION
    Builds the shape the MTM Loader expects in the shared application store's
    "MTM WIP Application" folder:

      MTM_Inventory_Application.exe   the executable named in the loader catalogue
      build-manifest.json             version, file list, size and SHA-256 per file

    The manifest is written last, so a half-copied payload is never advertised to
    the loader. Nothing is written to the live share unless -Destination is given.

.PARAMETER Configuration
    Build configuration. Release by default; a shared payload should always be Release.

.PARAMETER Runtime
    Runtime identifier. The loader expects win-x64.

.PARAMETER Output
    Local folder the payload is staged into. Relative paths resolve against the repo root.

.PARAMETER Destination
    Optional application master folder to publish into, for example
    "\\mtmanu-fs01\Expo Drive\MH_RESOURCE\Material_Handler\MTM Application Store\MTM WIP Application".
    The payload is staged beside the folder, verified file by file, swapped into place, and only
    then given its manifest, so the published folder is either the old payload or the new one.

.PARAMETER FrameworkDependent
    Publishes framework-dependent instead of self-contained. Only use this when every
    floor machine is known to have the .NET 8 Desktop Runtime installed.

.EXAMPLE
    ./scripts/Publish-MtmWipPayload.ps1

    Stages the payload under artifacts/payload/win-x64 and writes build-manifest.json.

.EXAMPLE
    ./scripts/Publish-MtmWipPayload.ps1 -Destination '\\mtmanu-fs01\Expo Drive\MH_RESOURCE\Material_Handler\MTM Application Store\MTM WIP Application'

    Stages the payload, then copies it to the shared application store with the manifest last.

.NOTES
    Rules taken from the MTM_Loader_WinUI3 specification:
      * 03-Platform/02-deployment-and-release.md (DP-7, DP-14, DP-17)
      * 01-Architecture/02-data-formats.md section 3 (build-manifest.json)
    The catalogue entry for this application is id "wip", source folder
    "MTM WIP Application", executable "MTM_Inventory_Application.exe".
#>
[CmdletBinding()]
param(
    [string] $Configuration = 'Release',
    [string] $Runtime = 'win-x64',
    [string] $Output = 'artifacts/payload/win-x64',
    [string] $Destination,
    [switch] $FrameworkDependent
)

$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$ApplicationId = 'wip'
$ExecutableName = 'MTM_Inventory_Application.exe'
$ManifestName = 'build-manifest.json'
$SchemaVersion = 1

function Resolve-RepoPath {
    param([string] $Path)

    if ([System.IO.Path]::IsPathRooted($Path)) {
        return $Path
    }

    return (Join-Path $script:RepoRoot $Path)
}

function Get-ProjectVersion {
    param([string] $ProjectPath)

    $text = Get-Content -LiteralPath $ProjectPath -Raw
    $match = [regex]::Match($text, '<Version>([^<]+)</Version>')
    if (-not $match.Success) {
        throw "No <Version> element found in '$ProjectPath'."
    }

    return $match.Groups[1].Value.Trim()
}

function Get-SemanticVersion {
    param([string] $Version)

    $parts = $Version.Split('.')
    while ($parts.Count -lt 3) {
        $parts += '0'
    }

    return ($parts[0..2] -join '.')
}

function Get-PayloadFiles {
    param([string] $Path)

    return Get-ChildItem -LiteralPath $Path -Recurse -File -Force |
        ForEach-Object {
            $relative = $_.FullName.Substring($Path.Length).TrimStart('\', '/').Replace('\', '/')
            [pscustomobject]@{
                path   = $relative
                size   = $_.Length
                sha256 = (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash.ToLowerInvariant()
            }
        } |
        Sort-Object -Property path
}

function New-ManifestJson {
    param(
        [string] $Version,
        [object[]] $Entries,
        [long] $TotalBytes
    )

    $builder = New-Object System.Text.StringBuilder
    [void]$builder.AppendLine('{')
    [void]$builder.AppendLine("  `"schemaVersion`": $SchemaVersion,")
    [void]$builder.AppendLine("  `"applicationId`": `"$ApplicationId`",")
    [void]$builder.AppendLine("  `"version`": `"$Version`",")
    [void]$builder.AppendLine("  `"publishedUtc`": `"$script:PublishedUtc`",")
    [void]$builder.AppendLine("  `"executable`": `"$ExecutableName`",")
    [void]$builder.AppendLine("  `"fileCount`": $($Entries.Count),")
    [void]$builder.AppendLine("  `"totalBytes`": $TotalBytes,")
    [void]$builder.AppendLine('  "files": [')

    for ($i = 0; $i -lt $Entries.Count; $i++) {
        $entry = $Entries[$i]
        $comma = if ($i -lt $Entries.Count - 1) { ',' } else { '' }
        [void]$builder.AppendLine("    { `"path`": `"$($entry.path)`", `"size`": $($entry.size), `"sha256`": `"$($entry.sha256)`" }$comma")
    }

    [void]$builder.AppendLine('  ]')
    [void]$builder.Append('}')

    return $builder.ToString()
}

$RepoRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $RepoRoot 'MTM_WIP_Application_Winforms.csproj'
if (-not (Test-Path -LiteralPath $projectPath -PathType Leaf)) {
    throw "The project file '$projectPath' was not found."
}

$projectVersion = Get-ProjectVersion -ProjectPath $projectPath
$manifestVersion = Get-SemanticVersion -Version $projectVersion
$outputPath = Resolve-RepoPath -Path $Output
$PublishedUtc = (Get-Date).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss'Z'")

Write-Host "Publishing $projectVersion as $manifestVersion to $outputPath" -ForegroundColor Cyan

if (Test-Path -LiteralPath $outputPath) {
    Remove-Item -LiteralPath $outputPath -Recurse -Force
}
New-Item -ItemType Directory -Path $outputPath -Force | Out-Null

$selfContained = if ($FrameworkDependent) { 'false' } else { 'true' }

dotnet publish $projectPath `
    -c $Configuration `
    -r $Runtime `
    --self-contained $selfContained `
    -p:AssemblyName=MTM_Inventory_Application `
    -p:PublishSingleFile=false `
    -p:PublishTrimmed=false `
    -o $outputPath
if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish failed with exit code $LASTEXITCODE."
}

$executablePath = Join-Path $outputPath $ExecutableName
if (-not (Test-Path -LiteralPath $executablePath -PathType Leaf)) {
    throw "The publish output does not contain '$ExecutableName'. The loader catalogue expects that file name."
}

$entries = @(Get-PayloadFiles -Path $outputPath)
$totalBytes = ($entries | Measure-Object -Property size -Sum).Sum
$manifestJson = New-ManifestJson -Version $manifestVersion -Entries $entries -TotalBytes $totalBytes

$manifestPath = Join-Path $outputPath $ManifestName
[System.IO.File]::WriteAllText($manifestPath, $manifestJson + "`n", (New-Object System.Text.UTF8Encoding($false)))

Write-Host "Wrote $ManifestName ($($entries.Count) files, $totalBytes bytes, version $manifestVersion)" -ForegroundColor Green

if (-not $Destination) {
    Write-Host "Payload staged. Nothing was copied to the share because -Destination was not supplied." -ForegroundColor Yellow
    Write-Output $manifestPath
    return
}

if (-not (Test-Path -LiteralPath $Destination -PathType Container)) {
    New-Item -ItemType Directory -Path $Destination -Force | Out-Null
    Write-Host "Created destination '$Destination'" -ForegroundColor Yellow
}

$existingManifestPath = Join-Path $Destination $ManifestName
if (Test-Path -LiteralPath $existingManifestPath -PathType Leaf) {
    $existing = Get-Content -LiteralPath $existingManifestPath -Raw | ConvertFrom-Json
    if ($existing.version -eq $manifestVersion) {
        $existingExe = @($existing.files | Where-Object { $_.path -eq $ExecutableName })[0]
        $newExe = @($entries | Where-Object { $_.path -eq $ExecutableName })[0]
        if ($existingExe -and $existingExe.sha256 -ne $newExe.sha256) {
            throw "Version $manifestVersion is already published with a different payload. Bump the version instead of reusing it."
        }
    }
}

$parentPath = Split-Path -Parent $Destination
if (-not (Test-Path -LiteralPath $parentPath -PathType Container)) {
    throw "The parent folder '$parentPath' does not exist. Check the share path before publishing."
}

$stagingPath = "$Destination.staging"
$previousPath = "$Destination.previous"

if (Test-Path -LiteralPath $stagingPath) {
    Remove-Item -LiteralPath $stagingPath -Recurse -Force
}
New-Item -ItemType Directory -Path $stagingPath -Force | Out-Null

Write-Host "Staging the payload in '$stagingPath'" -ForegroundColor Cyan
Get-ChildItem -LiteralPath $outputPath -Force |
    Where-Object { $_.Name -ne $ManifestName } |
    ForEach-Object {
        Copy-Item -LiteralPath $_.FullName -Destination $stagingPath -Recurse -Force
    }

$mismatch = @()
foreach ($entry in $entries) {
    $stagedPath = Join-Path $stagingPath ($entry.path -replace '/', '\')
    if (-not (Test-Path -LiteralPath $stagedPath -PathType Leaf)) {
        $mismatch += "$($entry.path) (missing)"
        continue
    }

    $stagedHash = (Get-FileHash -LiteralPath $stagedPath -Algorithm SHA256).Hash.ToLowerInvariant()
    if ($stagedHash -ne $entry.sha256) {
        $mismatch += "$($entry.path) (hash differs)"
    }
}

if ($mismatch.Count -gt 0) {
    Remove-Item -LiteralPath $stagingPath -Recurse -Force
    throw "The staged copy did not match the payload. Nothing was replaced. First problem: $($mismatch[0]). Total problems: $($mismatch.Count)."
}

Write-Host "Verified $($entries.Count) staged files against the payload hashes." -ForegroundColor Green

if (Test-Path -LiteralPath $previousPath) {
    Remove-Item -LiteralPath $previousPath -Recurse -Force
}

if (Test-Path -LiteralPath $Destination) {
    Rename-Item -LiteralPath $Destination -NewName (Split-Path -Leaf $previousPath)
}

Rename-Item -LiteralPath $stagingPath -NewName (Split-Path -Leaf $Destination)

Copy-Item -LiteralPath $manifestPath -Destination (Join-Path $Destination $ManifestName) -Force
Write-Host "Wrote $ManifestName last into '$Destination'" -ForegroundColor Green

if (Test-Path -LiteralPath $previousPath) {
    Write-Host "Removing the previous payload folder '$previousPath'" -ForegroundColor Yellow
    Remove-Item -LiteralPath $previousPath -Recurse -Force
}

Write-Host "Published version $manifestVersion to '$Destination'." -ForegroundColor Green
Write-Output (Join-Path $Destination $ManifestName)
