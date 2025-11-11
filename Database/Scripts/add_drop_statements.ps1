# ============================================================================
# Add DROP IF EXISTS to Stored Procedure Files
# ============================================================================
# Purpose: Ensure all SP files have DROP PROCEDURE IF EXISTS at the top
# Date: 2025-11-04
# ============================================================================

param(
    [switch]$WhatIf = $false
)

$ErrorActionPreference = 'Stop'
$repoRoot = "C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"
$spFolder = Join-Path $repoRoot "Database\UpdatedStoredProcedures\ReadyForVerification"

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  ADD DROP IF EXISTS TO STORED PROCEDURE FILES" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

# Get all SQL files
$sqlFiles = Get-ChildItem $spFolder -Recurse -Filter "*.sql" -File
Write-Host "Found $($sqlFiles.Count) SQL files to check`n" -ForegroundColor Yellow

$stats = @{
    Total = $sqlFiles.Count
    AlreadyHas = 0
    Added = 0
    Errors = 0
}

$missingFiles = @()

foreach ($file in $sqlFiles) {
    try {
        $content = Get-Content $file.FullName -Raw
        $procName = $file.BaseName
        
        # Check if file already has DROP statement
        if ($content -match '(?i)DROP\s+PROCEDURE\s+IF\s+EXISTS') {
            $stats.AlreadyHas++
            Write-Host "✓ $procName" -ForegroundColor Green -NoNewline
            Write-Host " (already has DROP)" -ForegroundColor DarkGray
        }
        else {
            $stats.Added++
            $missingFiles += @{
                Name = $procName
                Path = $file.FullName
                RelativePath = $file.FullName.Replace("$spFolder\", "")
            }
            Write-Host "⚠ $procName" -ForegroundColor Yellow -NoNewline
            Write-Host " (missing DROP statement)" -ForegroundColor DarkYellow
            
            if (-not $WhatIf) {
                # Add DROP statement at the beginning
                $dropStatement = "-- =============================================`r`n"
                $dropStatement += "-- Drop procedure if it exists`r`n"
                $dropStatement += "-- =============================================`r`n"
                $dropStatement += "DROP PROCEDURE IF EXISTS ``$procName``;//`r`n`r`n"
                
                # Prepend to file
                $newContent = $dropStatement + $content
                Set-Content -Path $file.FullName -Value $newContent -Encoding UTF8 -NoNewline
                
                Write-Host "  → Added DROP statement" -ForegroundColor Green
            }
            else {
                Write-Host "  → Would add DROP statement (WhatIf mode)" -ForegroundColor Gray
            }
        }
    }
    catch {
        $stats.Errors++
        Write-Host "✗ $($file.BaseName)" -ForegroundColor Red -NoNewline
        Write-Host " (error: $_)" -ForegroundColor DarkRed
    }
}

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  SUMMARY" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
Write-Host "Total files checked: " -NoNewline; Write-Host $stats.Total -ForegroundColor White
Write-Host "Already had DROP:    " -NoNewline; Write-Host $stats.AlreadyHas -ForegroundColor Green
Write-Host "Added DROP:          " -NoNewline; Write-Host $stats.Added -ForegroundColor Yellow
Write-Host "Errors:              " -NoNewline; Write-Host $stats.Errors -ForegroundColor Red
Write-Host ""

if ($WhatIf) {
    Write-Host "⚠️  Running in WhatIf mode - no changes were made" -ForegroundColor Yellow
    Write-Host "   Run without -WhatIf to apply changes" -ForegroundColor Gray
}
elseif ($stats.Added -gt 0) {
    Write-Host "✅ Successfully added DROP statements to $($stats.Added) files" -ForegroundColor Green
}
else {
    Write-Host "✅ All files already have DROP statements" -ForegroundColor Green
}
Write-Host ""
