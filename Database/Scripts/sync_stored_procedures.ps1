# ============================================================================
# Stored Procedure Synchronization Script
# ============================================================================
# Purpose: Synchronize ReadyForVerification folder with code calls and database
# Date: 2025-11-04
# ============================================================================

param(
    [switch]$WhatIf = $false,
    [switch]$Force = $false
)

$ErrorActionPreference = 'Stop'
$repoRoot = "C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"
$spFolder = Join-Path $repoRoot "Database\UpdatedStoredProcedures\ReadyForVerification"
$archiveFolder = Join-Path $repoRoot "Database\UpdatedStoredProcedures\Archive"
$mysqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe"
$dbName = "mtm_wip_application_winforms"
$dbUser = "root"
$dbPass = "root"

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  STORED PROCEDURE SYNCHRONIZATION ANALYSIS" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# STEP 1: Extract SP names called by C# code
# ============================================================================
Write-Host "[1/8] Scanning C# code for stored procedure calls..." -ForegroundColor Yellow

$codeFiles = Get-ChildItem -Path (Join-Path $repoRoot "Data"),
                                  (Join-Path $repoRoot "Forms"),
                                  (Join-Path $repoRoot "Controls"),
                                  (Join-Path $repoRoot "Helpers"),
                                  (Join-Path $repoRoot "Services") `
             -Filter "*.cs" -Recurse -File -ErrorAction SilentlyContinue

$calledProcs = @{}
$spPattern = 'ExecuteDataTableWithStatusAsync|ExecuteNonQueryWithStatusAsync|ExecuteScalarWithStatusAsync'

foreach ($file in $codeFiles) {
    try {
        $content = Get-Content $file.FullName -Raw
        $matches = [regex]::Matches($content, '(?:' + $spPattern + ')\s*\(\s*[^,]+,\s*"([^"]+)"')
        foreach ($match in $matches) {
            $spName = $match.Groups[1].Value
            if ($spName -and $spName -notmatch '^\$' -and $spName -notmatch '^@') {
                $calledProcs[$spName] = @{
                    Name = $spName
                    CalledInFiles = @()
                }
                if (-not $calledProcs[$spName].CalledInFiles.Contains($file.Name)) {
                    $calledProcs[$spName].CalledInFiles += $file.Name
                }
            }
        }
    }
    catch {
        Write-Warning "Error scanning $($file.Name): $_"
    }
}

Write-Host "   Found $($calledProcs.Count) unique stored procedures called by code" -ForegroundColor Green

# ============================================================================
# STEP 2: Get SPs from database
# ============================================================================
Write-Host "[2/8] Extracting stored procedures from database..." -ForegroundColor Yellow

$dbProcsQuery = "SELECT ROUTINE_NAME FROM information_schema.ROUTINES WHERE ROUTINE_SCHEMA='$dbName' AND ROUTINE_TYPE='PROCEDURE' ORDER BY ROUTINE_NAME;"

# Create temp file for MySQL output to avoid PowerShell object conversion issues
$tempFile = [System.IO.Path]::GetTempFileName()
try {
    # Execute MySQL and save to temp file
    $mysqlCmd = "& `"$mysqlPath`" -h localhost -P 3306 -u $dbUser -p$dbPass $dbName -sN -e `"$dbProcsQuery`" 2>&1 | Out-File -FilePath `"$tempFile`" -Encoding UTF8"
    Invoke-Expression $mysqlCmd | Out-Null
    
    # Read from temp file
    $dbProcsResult = Get-Content $tempFile | Where-Object { 
        $_ -notmatch 'Warning' -and 
        $_ -notmatch '^mysql:' -and 
        $_.Trim() -ne '' 
    }
    
    $dbProcs = @{}
    foreach ($proc in $dbProcsResult) {
        $procName = $proc.Trim()
        if ($procName -and $procName -ne '') {
            $dbProcs[$procName] = $true
        }
    }
}
finally {
    if (Test-Path $tempFile) {
        Remove-Item $tempFile -Force
    }
}

Write-Host "   Found $($dbProcs.Count) stored procedures in database" -ForegroundColor Green

# ============================================================================
# STEP 3: Get existing SQL files
# ============================================================================
Write-Host "[3/8] Scanning existing SQL files..." -ForegroundColor Yellow

$existingFiles = Get-ChildItem $spFolder -Recurse -Filter "*.sql" -File
$fileProcs = @{}

foreach ($file in $existingFiles) {
    $procName = $file.BaseName
    $fileProcs[$procName] = @{
        Name = $procName
        Path = $file.FullName
        RelativePath = $file.FullName.Replace("$spFolder\", "")
        Folder = $file.Directory.Name
    }
}

Write-Host "   Found $($fileProcs.Count) SQL files in ReadyForVerification" -ForegroundColor Green

# ============================================================================
# STEP 4: Cross-reference analysis
# ============================================================================
Write-Host "[4/8] Cross-referencing code, files, and database..." -ForegroundColor Yellow

$analysis = @{
    CalledMissingFile = @()        # Called by code, no file exists
    CalledMissingDB = @()          # Called by code, not in DB
    FileNotCalled = @()            # File exists but not called by code
    FileNotInDB = @()              # File exists but not in DB
    DBNotCalled = @()              # In DB but not called by code
    AllGood = @()                  # Called, has file, in DB
}

# Analyze called procedures
foreach ($procName in $calledProcs.Keys) {
    $hasFile = $fileProcs.ContainsKey($procName)
    $inDB = $dbProcs.ContainsKey($procName)
    
    if ($hasFile -and $inDB) {
        $analysis.AllGood += $procName
    }
    elseif (-not $hasFile -and $inDB) {
        $analysis.CalledMissingFile += $procName
    }
    elseif (-not $inDB) {
        $analysis.CalledMissingDB += $procName
    }
}

# Analyze file procedures not called
foreach ($procName in $fileProcs.Keys) {
    if (-not $calledProcs.ContainsKey($procName)) {
        $analysis.FileNotCalled += $procName
        if (-not $dbProcs.ContainsKey($procName)) {
            $analysis.FileNotInDB += $procName
        }
    }
}

# Analyze DB procedures not called
foreach ($procName in $dbProcs.Keys) {
    if (-not $calledProcs.ContainsKey($procName) -and -not $fileProcs.ContainsKey($procName)) {
        $analysis.DBNotCalled += $procName
    }
}

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  ANALYSIS RESULTS" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
Write-Host "✅ Procedures in sync (called + file + DB): " -NoNewline; Write-Host "$($analysis.AllGood.Count)" -ForegroundColor Green
Write-Host "⚠️  Called but missing file: " -NoNewline; Write-Host "$($analysis.CalledMissingFile.Count)" -ForegroundColor Yellow
Write-Host "❌ Called but not in DB: " -NoNewline; Write-Host "$($analysis.CalledMissingDB.Count)" -ForegroundColor Red
Write-Host "📦 File exists but not called by code: " -NoNewline; Write-Host "$($analysis.FileNotCalled.Count)" -ForegroundColor Magenta
Write-Host "🗄️  In DB but not called by code: " -NoNewline; Write-Host "$($analysis.DBNotCalled.Count)" -ForegroundColor DarkGray
Write-Host ""

# Output details for action items
if ($analysis.CalledMissingFile.Count -gt 0) {
    Write-Host "Called procedures missing SQL files:" -ForegroundColor Yellow
    $analysis.CalledMissingFile | Sort-Object | ForEach-Object { Write-Host "  - $_" -ForegroundColor Yellow }
    Write-Host ""
}

if ($analysis.CalledMissingDB.Count -gt 0) {
    Write-Host "Called procedures NOT in database (ERROR - fix code or create SP):" -ForegroundColor Red
    $analysis.CalledMissingDB | Sort-Object | ForEach-Object { Write-Host "  - $_" -ForegroundColor Red }
    Write-Host ""
}

if ($analysis.FileNotCalled.Count -gt 0) {
    Write-Host "SQL files not called by code (candidates for archiving):" -ForegroundColor Magenta
    $analysis.FileNotCalled | Sort-Object | ForEach-Object { Write-Host "  - $_" -ForegroundColor Magenta }
    Write-Host ""
}

# ============================================================================
# STEP 5: Export analysis to JSON for further processing
# ============================================================================
Write-Host "[5/8] Exporting analysis results..." -ForegroundColor Yellow

$analysisFile = Join-Path $repoRoot "Database\sp_sync_analysis.json"
$analysisData = @{
    Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Statistics = @{
        TotalCalledByCode = $calledProcs.Count
        TotalInDatabase = $dbProcs.Count
        TotalSQLFiles = $fileProcs.Count
        InSync = $analysis.AllGood.Count
        CalledMissingFile = $analysis.CalledMissingFile.Count
        CalledMissingDB = $analysis.CalledMissingDB.Count
        FileNotCalled = $analysis.FileNotCalled.Count
    }
    Analysis = $analysis
    CalledProcedures = $calledProcs
    FileProcedures = $fileProcs
}

$analysisData | ConvertTo-Json -Depth 10 | Out-File $analysisFile -Encoding UTF8
Write-Host "   Analysis exported to: $analysisFile" -ForegroundColor Green

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  NEXT STEPS" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""
Write-Host "This script has completed the analysis phase." -ForegroundColor White
Write-Host ""
Write-Host "To continue synchronization, the following tasks remain:" -ForegroundColor White
Write-Host "  [6/8] Create missing SP files from database" -ForegroundColor Gray
Write-Host "  [7/8] Add DROP IF EXISTS to files missing it" -ForegroundColor Gray
Write-Host "  [8/8] Archive unused SP files" -ForegroundColor Gray
Write-Host ""
Write-Host "Review the analysis above and run the appropriate actions." -ForegroundColor White
Write-Host ""
