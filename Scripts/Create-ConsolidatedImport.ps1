<#
.SYNOPSIS
    Creates a single consolidated SQL file from all ReadyForVerification stored procedures
    
.DESCRIPTION
    Combines all individual .sql files into one importable file for phpMyAdmin or MySQL Workbench
    
.PARAMETER SourcePath
    Path to stored procedures folder (default: Database\CurrentStoredProcedures\ReadyForVerification)
    
.PARAMETER OutputFile
    Output file path (default: Database\ALL_PROCEDURES_IMPORT.sql)
    
.EXAMPLE
    .\Scripts\Create-ConsolidatedImport.ps1
    Creates consolidated import file
#>

[CmdletBinding()]
param(
    [string]$SourcePath = "Database\CurrentStoredProcedures\ReadyForVerification",
    [string]$OutputFile = "Database\ALL_PROCEDURES_IMPORT.sql"
)

Write-Host "`n=== Creating Consolidated Import File ===" -ForegroundColor Cyan

# Verify source path exists
if (-not (Test-Path $SourcePath)) {
    Write-Host "❌ Source path not found: $SourcePath" -ForegroundColor Red
    exit 1
}

# Get all SQL files
$sqlFiles = Get-ChildItem $SourcePath -Filter "*.sql" | Sort-Object Name

if ($sqlFiles.Count -eq 0) {
    Write-Host "⚠️  No SQL files found in $SourcePath" -ForegroundColor Yellow
    exit 1
}

Write-Host "Found $($sqlFiles.Count) stored procedure(s)" -ForegroundColor White

# Create output content
$consolidatedContent = @"
-- ========================================
-- MTM WIP Application - Stored Procedures
-- Consolidated Import File
-- Generated: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")
-- Total Procedures: $($sqlFiles.Count)
-- ========================================

DELIMITER `$`$

"@

# Process each file
$processed = 0
foreach ($file in $sqlFiles) {
    Write-Host "  Adding: " -NoNewline
    Write-Host $file.Name -ForegroundColor White
    
    # Read file content
    $content = Get-Content $file.FullName -Raw
    
    # Remove ALL DELIMITER statements from individual files (we'll use one global delimiter)
    # Match with or without trailing newline/whitespace
    $content = $content -replace 'DELIMITER \$\$(\s|\r|\n)*', ''
    $content = $content -replace 'DELIMITER ;(\s|\r|\n)*', ''
    
    # Ensure there's a newline after DROP statements ending with $$
    # This helps MySQL recognize DROP and CREATE as separate statements
    $content = $content -replace '(DROP PROCEDURE[^\n]+\$\$)\s*(\r?\n)', "`$1`n`n"
    
    # The files already have $$ terminators, so no replacement needed
    # Just trim any trailing whitespace
    $content = $content.Trim()
    
    # Add separator comment and content
    $consolidatedContent += @"


-- ========================================
-- Procedure: $($file.BaseName)
-- ========================================

$content

"@
    
    $processed++
}

# Add final delimiter reset
$consolidatedContent += @"


DELIMITER ;

-- ========================================
-- Import Complete: $processed procedures
-- ========================================
"@

# Write consolidated file
$consolidatedContent | Out-File -FilePath $OutputFile -Encoding UTF8

Write-Host "`n✅ Consolidated import file created!" -ForegroundColor Green
Write-Host "   Location: " -NoNewline
Write-Host $OutputFile -ForegroundColor Cyan
Write-Host "   Procedures: " -NoNewline
Write-Host "$processed" -ForegroundColor White

Write-Host "`n📋 Next Steps:" -ForegroundColor Yellow
Write-Host "   1. Open phpMyAdmin (http://localhost/phpMyAdmin)" -ForegroundColor White
Write-Host "   2. Select database: mtm_wip_application_winforms_test" -ForegroundColor White
Write-Host "   3. Click 'Import' tab" -ForegroundColor White
Write-Host "   4. Choose file: $OutputFile" -ForegroundColor White
Write-Host "   5. Click 'Go' to import all $processed procedures`n" -ForegroundColor White

exit 0
