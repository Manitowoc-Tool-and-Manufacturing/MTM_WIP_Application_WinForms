<#
.SYNOPSIS
    Master script to run complete stored procedure analysis
.DESCRIPTION
    Executes all analysis scripts in sequence:
    1. Analyze SQL operations (detailed breakdown)
    2. Trace complete call hierarchy (all procedures)
    3. Merge all data into single CSV
    
    Reads configuration from SPSettings.json to make this tool portable
#>

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   COMPLETE STORED PROCEDURE ANALYSIS PIPELINE         ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

$ErrorActionPreference = "Stop"

# Load settings from SPSettings.json
$settingsPath = Join-Path $PSScriptRoot "SPSettings.json"
if (-not (Test-Path $settingsPath)) {
    Write-Host "✗ SPSettings.json not found at: $settingsPath" -ForegroundColor Red
    Write-Host "  Please create SPSettings.json with project configuration" -ForegroundColor Yellow
    exit 1
}

try {
    $settings = Get-Content $settingsPath -Raw | ConvertFrom-Json
    Write-Host "✓ Loaded settings from SPSettings.json" -ForegroundColor Green
    Write-Host "  Project: $($settings.ProjectSettings.ProjectName)" -ForegroundColor Gray
    Write-Host "  Database: $($settings.DatabaseConnection.Database)@$($settings.DatabaseConnection.Server)" -ForegroundColor Gray
} catch {
    Write-Host "✗ Failed to parse SPSettings.json: $_" -ForegroundColor Red
    exit 1
}

# Pass settings to child scripts via environment variables
$env:SP_SETTINGS_PATH = $settingsPath

# Get script directory for running child scripts
$scriptDir = $PSScriptRoot
if (-not $scriptDir) {
    $scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
}

# Step 0: Complete procedure analysis (patterns, DML counts, transaction handling)
try {
    & (Join-Path $scriptDir "0-Analyze-Procedures-Complete.ps1")
} catch {
    Write-Host "`n✗ Step 0 failed: $_`n" -ForegroundColor Red
    exit 1
}

# Step 1: Analyze SQL Operations
try {
    & (Join-Path $scriptDir "1-Analyze-SQL-Operations.ps1")
} catch {
    Write-Host "`n✗ Step 1 failed: $_`n" -ForegroundColor Red
    exit 1
}

# Step 2: Trace Complete Call Hierarchy
try {
    & (Join-Path $scriptDir "2-Trace-Complete-CallHierarchy.ps1")
} catch {
    Write-Host "`n✗ Step 2 failed: $_`n" -ForegroundColor Red
    exit 1
}

# Step 3: Merge All Analysis
try {
    & (Join-Path $scriptDir "3-Merge-All-Analysis.ps1")
} catch {
    Write-Host "`n✗ Step 3 failed: $_`n" -ForegroundColor Red
    exit 1
}

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║   ✓ ANALYSIS PIPELINE COMPLETED SUCCESSFULLY          ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Green

Write-Host "Generated Files:" -ForegroundColor Cyan
Write-Host "  • $($settings.Paths.OutputCSV) - Master CSV with all analysis data" -ForegroundColor White
Write-Host "  • $($settings.Paths.SQLOperationsJSON) - Detailed SQL operation breakdown" -ForegroundColor White
Write-Host "  • $($settings.Paths.CallHierarchyJSON) - Complete call hierarchy for all procedures" -ForegroundColor White
Write-Host "  • procedure-base-analysis.csv - Pattern and DML analysis" -ForegroundColor White

Write-Host "`nNext Steps:" -ForegroundColor Yellow
Write-Host "  1. Open procedure-review-tool.html in your browser" -ForegroundColor Gray
Write-Host "  2. Load $($settings.Paths.OutputCSV)" -ForegroundColor Gray
Write-Host "  3. Review detailed SQL operations and complete call hierarchies`n" -ForegroundColor Gray

# Open the HTML review tool automatically
$htmlPath = Join-Path $scriptDir "procedure-review-tool.html"
if (Test-Path $htmlPath) {
    Write-Host "Opening procedure review tool..." -ForegroundColor Cyan
    Start-Process $htmlPath
} else {
    Write-Host "⚠ Could not find procedure-review-tool.html at: $htmlPath" -ForegroundColor Yellow
}

# Clean up environment variables
Remove-Item Env:\SP_SETTINGS_PATH -ErrorAction SilentlyContinue
