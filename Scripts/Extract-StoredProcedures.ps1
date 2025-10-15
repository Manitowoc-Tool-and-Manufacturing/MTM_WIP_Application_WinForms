<#
.SYNOPSIS
    Extracts individual stored procedures from CurrentStoredProcedures.md and creates separate SQL files.

.DESCRIPTION
    Reads the CurrentStoredProcedures.md file, extracts each stored procedure,
    and creates individual SQL files in the Database/CurrentStoredProcedures/ folder
    with proper formatting and header comments.

.NOTES
    Created: 2025-10-14
    Part of: 002-comprehensive-database-layer refactor
#>

param(
    [string]$InputFile = "$PSScriptRoot\..\CurrentStoredProcedures.md",
    [string]$OutputFolder = "$PSScriptRoot\..\Database\CurrentStoredProcedures"
)

# Ensure output folder exists
if (-not (Test-Path $OutputFolder)) {
    New-Item -Path $OutputFolder -ItemType Directory -Force | Out-Null
}

Write-Host "Reading stored procedures from: $InputFile" -ForegroundColor Cyan
Write-Host "Output folder: $OutputFolder" -ForegroundColor Cyan

# Read the entire file
$content = Get-Content $InputFile -Raw

# Split by DELIMITER patterns to find procedure boundaries
$procedures = @()
$lines = $content -split "`n"
$currentProc = @()
$inProcedure = $false
$procName = ""

for ($i = 0; $i -lt $lines.Count; $i++) {
    $line = $lines[$i]
    
    # Detect start of procedure
    if ($line -match 'CREATE\s+(DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`([^`]+)`') {
        $procName = $Matches[2]
        $inProcedure = $true
        $currentProc = @()
        $currentProc += "DELIMITER `$`$"
        $currentProc += ""
    }
    
    if ($inProcedure) {
        $currentProc += $line
        
        # Detect end of procedure
        if ($line -match '^\s*END\$\$\s*$') {
            $currentProc += "DELIMITER ;"
            $currentProc += ""
            
            # Save procedure
            if ($procName) {
                $procedures += @{
                    Name = $procName
                    Content = $currentProc -join "`n"
                }
                Write-Host "  Found procedure: $procName" -ForegroundColor Gray
            }
            
            $inProcedure = $false
            $procName = ""
            $currentProc = @()
        }
    }
}

Write-Host "`nExtracted $($procedures.Count) procedures" -ForegroundColor Green
Write-Host "`nCreating individual SQL files..." -ForegroundColor Cyan

$created = 0
foreach ($proc in $procedures) {
    $fileName = "$($proc.Name).sql"
    $filePath = Join-Path $OutputFolder $fileName
    
    # Create header
    $header = @"
-- =============================================
-- Stored Procedure: $($proc.Name)
-- Description: [AUTO-EXTRACTED - ADD DESCRIPTION]
-- Created: 2025-10-14
-- Part of: Current database procedures (reference)
-- Status: NEEDS REVIEW - Fix status codes per 00_STATUS_CODE_STANDARDS.md
-- =============================================

"@
    
    # Write file
    $fullContent = $header + $proc.Content
    Set-Content -Path $filePath -Value $fullContent -Encoding UTF8
    $created++
}

Write-Host "`nCreated $created SQL files in: $OutputFolder" -ForegroundColor Green
Write-Host "`nNOTE: These procedures need review and status code corrections!" -ForegroundColor Yellow
Write-Host "See Database/UpdatedStoredProcedures/00_STATUS_CODE_STANDARDS.md for proper patterns" -ForegroundColor Yellow
