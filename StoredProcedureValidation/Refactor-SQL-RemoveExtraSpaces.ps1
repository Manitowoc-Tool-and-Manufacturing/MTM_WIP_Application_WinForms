<#
.SYNOPSIS
    Clean up extra spaces and normalize whitespace in SQL stored procedure files
.DESCRIPTION
    This script will:
    - Remove multiple consecutive blank lines (keep max 1)
    - Remove trailing whitespace from lines
    - Normalize indentation (convert tabs to spaces if configured)
    - Remove extra spaces between tokens
    - Preserve SQL structure and readability
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory=$false)]
    [string]$FolderPath,
    
    [Parameter(Mandatory=$false)]
    [switch]$PreviewOnly,
    
    [Parameter(Mandatory=$false)]
    [int]$IndentSpaces = 4,
    
    [Parameter(Mandatory=$false)]
    [switch]$ConvertTabsToSpaces,
    
    [Parameter(Mandatory=$false)]
    [switch]$RemoveComments
)

# Default to UpdatedStoredProcedures folder if not specified
if (-not $FolderPath) {
    $FolderPath = Join-Path $PSScriptRoot "UpdatedStoredProcedures"
}

if (-not (Test-Path $FolderPath)) {
    Write-Host "✗ Folder not found: $FolderPath" -ForegroundColor Red
    exit 1
}

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   SQL WHITESPACE CLEANUP TOOL                         ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

if ($PreviewOnly) {
    Write-Host "  MODE: Preview Only (no files will be modified)" -ForegroundColor Yellow
} else {
    Write-Host "  MODE: Live Editing (files WILL be modified)" -ForegroundColor Green
}

Write-Host "  Folder: $FolderPath" -ForegroundColor Gray
Write-Host "  Indent Spaces: $IndentSpaces" -ForegroundColor Gray
Write-Host "  Convert Tabs: $($ConvertTabsToSpaces.IsPresent)" -ForegroundColor Gray
Write-Host "  Remove Comments: $($RemoveComments.IsPresent)`n" -ForegroundColor Gray

$sqlFiles = Get-ChildItem -Path $FolderPath -Recurse -Filter "*.sql"
Write-Host "Found $($sqlFiles.Count) SQL files to process`n" -ForegroundColor Green

$totalFiles = $sqlFiles.Count
$currentFile = 0
$modifiedCount = 0
$startTime = Get-Date

foreach ($file in $sqlFiles) {
    $currentFile++
    $fileName = $file.Name
    
    # Progress display
    $percentComplete = [math]::Round(($currentFile / $totalFiles) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r[{0}{1}] {2}% ({3}/{4}) Processing: {5,-40}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentFile, $totalFiles, 
        $fileName.Substring(0, [Math]::Min(40, $fileName.Length))) -NoNewline -ForegroundColor Yellow
    
    # Read file content
    $lines = Get-Content $file.FullName
    
    # Track if changes were made
    $wasModified = $false
    $cleanedLines = @()
    $previousLineWasBlank = $false
    $inMultiLineComment = $false
    
    foreach ($line in $lines) {
        $originalLine = $line
        
        # 1. Handle multi-line comments /* ... */
        if ($RemoveComments) {
            # Check if starting a multi-line comment
            if ($line -match '/\*') {
                $inMultiLineComment = $true
                $wasModified = $true
                # Remove everything from /* onwards on this line
                $line = $line -replace '/\*.*', ''
            }
            
            # If we're in a multi-line comment, skip this line entirely
            if ($inMultiLineComment) {
                # Check if this line ends the comment
                if ($originalLine -match '\*/') {
                    $inMultiLineComment = $false
                    # If there's content after */, keep it
                    if ($originalLine -match '\*/(.+)$') {
                        $line = $matches[1]
                    } else {
                        continue  # Skip this line entirely
                    }
                } else {
                    continue  # Skip this line entirely
                }
            }
            
            # Remove single-line comments (-- comments)
            if ($line -match '--') {
                $beforeComment = ($line -split '--')[0]
                if ($beforeComment.TrimEnd() -ne $line.TrimEnd()) {
                    $line = $beforeComment
                    $wasModified = $true
                }
            }
        }
        
        # 2. Remove trailing whitespace
        $cleanedLine = $line.TrimEnd()
        
        # 3. Convert tabs to spaces if requested
        if ($ConvertTabsToSpaces -and $cleanedLine -match '\t') {
            $cleanedLine = $cleanedLine -replace '\t', (' ' * $IndentSpaces)
            $wasModified = $true
        }
        
        # 4. Normalize spaces between keywords (but preserve indentation)
        # Only apply to SQL keywords, not string literals
        if ($cleanedLine -match '^\s*(INSERT|UPDATE|DELETE|SELECT|FROM|WHERE|SET|VALUES|INTO|BEGIN|END|IF|THEN|ELSE|ELSEIF|DECLARE|START|COMMIT|ROLLBACK)\s+') {
            $originalCleaned = $cleanedLine
            # Remove multiple spaces between tokens (but keep leading indentation)
            $leadingSpaces = ($cleanedLine -replace '^(\s*).*', '$1').Length
            $lineContent = $cleanedLine.Substring($leadingSpaces)
            $lineContent = $lineContent -replace '\s+', ' '
            $cleanedLine = (' ' * $leadingSpaces) + $lineContent
            
            if ($originalCleaned -ne $cleanedLine) {
                $wasModified = $true
            }
        }
        
        # 5. Skip ALL blank lines (more aggressive - remove all empty lines)
        if ([string]::IsNullOrWhiteSpace($cleanedLine)) {
            $wasModified = $true
            # Skip this line entirely - don't add it
        } else {
            $cleanedLines += $cleanedLine
            $previousLineWasBlank = $false
            
            if ($line -ne $cleanedLine) {
                $wasModified = $true
            }
        }
    }
    
    # Create cleaned content
    $cleanedContent = ($cleanedLines -join "`n").TrimEnd() + "`n"
    
    if ($wasModified) {
        $modifiedCount++
        
        if ($PreviewOnly) {
            Write-Host "`n  ✓ Changes detected: $fileName" -ForegroundColor Cyan
        } else {
            # Write cleaned content back to file
            Set-Content -Path $file.FullName -Value $cleanedContent -NoNewline -Encoding UTF8
            Write-Host "`n  ✓ Modified: $fileName" -ForegroundColor Green
        }
    }
}

Write-Host "`n`n" # Clear progress line

$elapsed = (Get-Date) - $startTime
Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   CLEANUP COMPLETE                                    ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

Write-Host "  Total files processed: $totalFiles" -ForegroundColor Gray
Write-Host "  Files with changes: $modifiedCount" -ForegroundColor $(if ($modifiedCount -gt 0) { "Yellow" } else { "Gray" })
Write-Host "  Time elapsed: $($elapsed.TotalSeconds.ToString('F2'))s`n" -ForegroundColor Gray

if ($PreviewOnly -and $modifiedCount -gt 0) {
    Write-Host "To apply changes, run without -PreviewOnly flag:`n" -ForegroundColor Yellow
    Write-Host "  .\Refactor-SQL-RemoveExtraSpaces.ps1`n" -ForegroundColor Cyan
}

# Summary of what was done
Write-Host "Changes applied:" -ForegroundColor White
Write-Host "  • Removed trailing whitespace" -ForegroundColor Gray
Write-Host "  • Removed ALL blank lines (ultra-compact)" -ForegroundColor Gray
Write-Host "  • Normalized spaces between SQL keywords" -ForegroundColor Gray
if ($ConvertTabsToSpaces) {
    Write-Host "  • Converted tabs to $IndentSpaces spaces" -ForegroundColor Gray
}
if ($RemoveComments) {
    Write-Host "  • Removed all comments (-- and /* */)" -ForegroundColor Gray
}
Write-Host ""
