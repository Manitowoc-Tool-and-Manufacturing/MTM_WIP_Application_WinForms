<#
.SYNOPSIS
    Finds Forms and Controls not included in tasks.md async migration

.DESCRIPTION
    Scans the Forms/ and Controls/ directories for .cs files and checks if they
    are referenced in tasks.md. Reports files that may need async migration but
    are not currently included in the task list.

.PARAMETER Json
    Output results as JSON instead of formatted text

.PARAMETER IncludeDesigner
    Include .Designer.cs files in the scan (normally excluded)

.EXAMPLE
    .\find-unmapped-forms-controls.ps1
    Finds all Forms/Controls not in tasks.md

.EXAMPLE
    .\find-unmapped-forms-controls.ps1 -Json
    Outputs results as JSON for automation
#>

param(
    [switch]$Json,
    [switch]$IncludeDesigner
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
$tasksFile = Join-Path $PSScriptRoot "tasks.md"

Write-Host "INFO: Scanning Forms/ and Controls/ directories..." -ForegroundColor Cyan

# Read tasks.md
if (-not (Test-Path $tasksFile)) {
    Write-Error "tasks.md not found at: $tasksFile"
    exit 1
}

$tasksContent = Get-Content $tasksFile -Raw

# Find all Forms and Controls .cs files
$formsDir = Join-Path $repoRoot "Forms"
$controlsDir = Join-Path $repoRoot "Controls"

$allFiles = @()

if (Test-Path $formsDir) {
    $allFiles += Get-ChildItem -Path $formsDir -Filter "*.cs" -Recurse | Where-Object {
        $IncludeDesigner -or $_.Name -notmatch '\.Designer\.cs$'
    }
}

if (Test-Path $controlsDir) {
    $allFiles += Get-ChildItem -Path $controlsDir -Filter "*.cs" -Recurse | Where-Object {
        $IncludeDesigner -or $_.Name -notmatch '\.Designer\.cs$'
    }
}

# Categorize files
$mappedFiles = @()
$unmappedFiles = @()

foreach ($file in $allFiles) {
    $relativePath = $file.FullName.Replace("$repoRoot\", "")
    
    # Check if file is mentioned in tasks.md
    $isMapped = $tasksContent -match [regex]::Escape($relativePath)
    
    # Also check for just the filename (some tasks may not include full path)
    if (-not $isMapped) {
        $isMapped = $tasksContent -match [regex]::Escape($file.Name)
    }
    
    # Check if parent directory/file base name is mentioned
    if (-not $isMapped) {
        $baseName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
        $parentDir = $file.Directory.Name
        $isMapped = $tasksContent -match "$parentDir[/\\]$baseName"
    }
    
    $fileInfo = @{
        Path = $relativePath
        Name = $file.Name
        Directory = $file.Directory.FullName.Replace("$repoRoot\", "")
        Size = $file.Length
        LastModified = $file.LastWriteTime
        IsMapped = $isMapped
    }
    
    if ($isMapped) {
        $mappedFiles += $fileInfo
    }
    else {
        $unmappedFiles += $fileInfo
    }
}

# Analyze unmapped files for async migration candidates
$asyncCandidates = @()
foreach ($file in $unmappedFiles) {
    $content = Get-Content (Join-Path $repoRoot $file.Path) -Raw -ErrorAction SilentlyContinue
    
    if ($content) {
        # Check for potential async migration indicators
        $hasEventHandlers = $content -match '_Click\s*\(|_Load\s*\(|_Changed\s*\('
        $hasDatabaseCalls = $content -match 'Dao_|MySql|Database|StoredProcedure'
        $hasLongRunningOps = $content -match 'Thread\.|Task\.|BackgroundWorker'
        
        if ($hasEventHandlers -or $hasDatabaseCalls -or $hasLongRunningOps) {
            $asyncCandidates += @{
                Path = $file.Path
                Name = $file.Name
                Directory = $file.Directory
                HasEventHandlers = $hasEventHandlers
                HasDatabaseCalls = $hasDatabaseCalls
                HasLongRunningOps = $hasLongRunningOps
                Size = $file.Size
                LastModified = $file.LastModified
            }
        }
    }
}

# Output results
if ($Json) {
    $result = @{
        TotalFiles = $allFiles.Count
        MappedFiles = $mappedFiles.Count
        UnmappedFiles = $unmappedFiles.Count
        AsyncCandidates = $asyncCandidates.Count
        Mapped = @($mappedFiles)
        Unmapped = @($unmappedFiles)
        Candidates = @($asyncCandidates)
    }
    
    $result | ConvertTo-Json -Depth 10
}
else {
    Write-Host "`n========================================" -ForegroundColor Cyan
    Write-Host "  Forms/Controls Coverage Report" -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    
    Write-Host "`nTotal Forms/Controls files: $($allFiles.Count)" -ForegroundColor White
    Write-Host "  ✓ Mapped in tasks.md: $($mappedFiles.Count) ($(($mappedFiles.Count / $allFiles.Count * 100).ToString('F1'))%)" -ForegroundColor Green
    Write-Host "  ✗ Not mapped in tasks.md: $($unmappedFiles.Count) ($(($unmappedFiles.Count / $allFiles.Count * 100).ToString('F1'))%)" -ForegroundColor Yellow
    Write-Host "  ⚠ Async migration candidates: $($asyncCandidates.Count)" -ForegroundColor Cyan
    
    if ($mappedFiles.Count -gt 0) {
        Write-Host "`n✓ MAPPED FILES (Included in tasks.md)" -ForegroundColor Green
        Write-Host "=" * 50 -ForegroundColor Green
        foreach ($file in ($mappedFiles | Sort-Object Path)) {
            Write-Host "  $($file.Path)" -ForegroundColor White
        }
    }
    
    if ($unmappedFiles.Count -gt 0) {
        Write-Host "`n✗ UNMAPPED FILES (Not in tasks.md)" -ForegroundColor Yellow
        Write-Host "=" * 50 -ForegroundColor Yellow
        foreach ($file in ($unmappedFiles | Sort-Object Path)) {
            Write-Host "  $($file.Path)" -ForegroundColor White
            Write-Host "    Last modified: $($file.LastModified)" -ForegroundColor DarkGray
        }
    }
    
    if ($asyncCandidates.Count -gt 0) {
        Write-Host "`n⚠ ASYNC MIGRATION CANDIDATES" -ForegroundColor Cyan
        Write-Host "=" * 50 -ForegroundColor Cyan
        Write-Host "These files contain patterns suggesting async migration may be needed:" -ForegroundColor Cyan
        Write-Host ""
        
        foreach ($candidate in ($asyncCandidates | Sort-Object Path)) {
            Write-Host "  $($candidate.Path)" -ForegroundColor White
            
            $indicators = @()
            if ($candidate.HasEventHandlers) { $indicators += "Event Handlers" }
            if ($candidate.HasDatabaseCalls) { $indicators += "Database Calls" }
            if ($candidate.HasLongRunningOps) { $indicators += "Long-Running Operations" }
            
            Write-Host "    Indicators: $($indicators -join ', ')" -ForegroundColor DarkGray
            Write-Host "    Last modified: $($candidate.LastModified)" -ForegroundColor DarkGray
        }
        
        Write-Host "`nRecommended Actions:" -ForegroundColor Cyan
        Write-Host "  1. Review each candidate file for actual async migration needs" -ForegroundColor White
        Write-Host "  2. Add necessary tasks to tasks.md (Phase 5 or Phase 7)" -ForegroundColor White
        Write-Host "  3. Or document why async migration is not needed" -ForegroundColor White
    }
    
    Write-Host "`n========================================" -ForegroundColor Cyan
    if ($asyncCandidates.Count -eq 0 -and $unmappedFiles.Count -eq 0) {
        Write-Host "✓ All Forms/Controls files are mapped!" -ForegroundColor Green
    }
    elseif ($asyncCandidates.Count -eq 0) {
        Write-Host "✓ No async migration candidates found in unmapped files" -ForegroundColor Green
        Write-Host "Unmapped files appear to be static/non-async code" -ForegroundColor Green
    }
    else {
        Write-Host "⚠ Review $($asyncCandidates.Count) async migration candidate(s)" -ForegroundColor Yellow
        Write-Host "Consider adding tasks or documenting non-async rationale" -ForegroundColor Yellow
    }
    Write-Host "========================================`n" -ForegroundColor Cyan
}
