<#
.SYNOPSIS
    Complete stored procedure analysis - determines patterns, transaction strategies, DML counts
.DESCRIPTION
    Analyzes each stored procedure to determine:
    - Domain (inventory, transactions, logging, master-data, users, system)
    - Pattern (CRUD, SEQUENTIAL, BATCH, QUERY)
    - Transaction strategy (IMPLICIT, EXPLICIT, BATCH)
    - DML operation counts (INSERT, UPDATE, DELETE)
    - Transaction handling (START TRANSACTION, COMMIT, ROLLBACK)
    - Validation presence (IF checks, parameter validation)
    - Rationale (human-readable explanation)
    
    Reads configuration from SPSettings.json for portability
#>

[CmdletBinding()]
param(
    [string]$SettingsPath = $env:SP_SETTINGS_PATH
)

# Load settings
if (-not $SettingsPath -or -not (Test-Path $SettingsPath)) {
    $SettingsPath = Join-Path $PSScriptRoot "SPSettings.json"
}

if (-not (Test-Path $SettingsPath)) {
    Write-Host "✗ SPSettings.json not found" -ForegroundColor Red
    exit 1
}

$settings = Get-Content $SettingsPath -Raw | ConvertFrom-Json
$storedProceduresPath = Join-Path $PSScriptRoot $settings.Paths.StoredProceduresFolder
$outputFile = Join-Path $PSScriptRoot "procedure-base-analysis.csv"

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   COMPLETE STORED PROCEDURE ANALYSIS                  ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

if (-not (Test-Path $storedProceduresPath)) {
    Write-Host "✗ Stored procedures folder not found: $storedProceduresPath" -ForegroundColor Red
    Write-Host "  Check StoredProceduresFolder path in SPSettings.json" -ForegroundColor Yellow
    exit 1
}

$sqlFiles = Get-ChildItem -Path $storedProceduresPath -Recurse -Filter "*.sql"
Write-Host "Found $($sqlFiles.Count) stored procedures to analyze`n" -ForegroundColor Green

$results = @()
$totalFiles = $sqlFiles.Count
$currentFile = 0
$startTime = Get-Date

foreach ($file in $sqlFiles) {
    $currentFile++
    $procName = $file.BaseName
    
    # Progress bar
    $percentComplete = [math]::Round(($currentFile / $totalFiles) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r[{0}{1}] {2}% ({3}/{4}) Analyzing: {5,-35}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentFile, $totalFiles,
        $procName.Substring(0, [Math]::Min(35, $procName.Length))) -NoNewline -ForegroundColor Yellow
    
    $content = Get-Content $file.FullName -Raw
    
    # === DETERMINE DOMAIN ===
    $domain = if ($procName -match '^inv_inventory') { "Inventory" }
              elseif ($procName -match '^inv_transaction') { "Transactions" }
              elseif ($procName -match '^log_') { "Logging" }
              elseif ($procName -match '^md_') { "Master Data" }
              elseif ($procName -match '^usr_') { "Users" }
              elseif ($procName -match '^sys_') { "System" }
              else { "Unknown" }
    
    # Debug logging for specific procedure
    $isDebugProc = $procName -eq "inv_inventory_Add_Item"
    
    # === COUNT DML OPERATIONS AND EXTRACT TABLE NAMES ===
    # Extract INSERT operations with table names (handles backticks and underscores in names)
    $insertMatches = [regex]::Matches($content, '(?i)\bINSERT\s+(?:IGNORE\s+)?INTO\s+`?([a-zA-Z_][a-zA-Z0-9_]*)`?')
    $insertCount = $insertMatches.Count
    
    if ($isDebugProc) {
        Write-Host "`n`n=== DEBUG: $procName ===" -ForegroundColor Yellow
        Write-Host "INSERT matches found: $insertCount" -ForegroundColor Cyan
    }
    
    $insertTables = @{}
    foreach ($match in $insertMatches) {
        $tableName = $match.Groups[1].Value
        if ($isDebugProc) {
            Write-Host "  INSERT match: '$($match.Value)' -> table: '$tableName'" -ForegroundColor Gray
        }
        if ($insertTables.ContainsKey($tableName)) {
            $insertTables[$tableName]++
        } else {
            $insertTables[$tableName] = 1
        }
    }
    
    # Extract UPDATE operations with table names (handles backticks and underscores)
    $updateMatches = [regex]::Matches($content, '(?i)\bUPDATE\s+`?([a-zA-Z_][a-zA-Z0-9_]*)`?\s+(?:SET|INNER|LEFT|RIGHT|JOIN|WHERE)')
    $updateCount = $updateMatches.Count
    $updateTables = @{}
    foreach ($match in $updateMatches) {
        $tableName = $match.Groups[1].Value
        if ($updateTables.ContainsKey($tableName)) {
            $updateTables[$tableName]++
        } else {
            $updateTables[$tableName] = 1
        }
    }
    
    # Extract DELETE operations with table names (handles backticks and underscores)
    $deleteMatches = [regex]::Matches($content, '(?i)\bDELETE\s+FROM\s+`?([a-zA-Z_][a-zA-Z0-9_]*)`?')
    $deleteCount = $deleteMatches.Count
    $deleteTables = @{}
    foreach ($match in $deleteMatches) {
        $tableName = $match.Groups[1].Value
        if ($deleteTables.ContainsKey($tableName)) {
            $deleteTables[$tableName]++
        } else {
            $deleteTables[$tableName] = 1
        }
    }
    
    $totalDML = $insertCount + $updateCount + $deleteCount
    
    # Build detailed table operation strings
    $insertDetails = ($insertTables.GetEnumerator() | ForEach-Object { "$($_.Key)($($_.Value))" }) -join ", "
    $updateDetails = ($updateTables.GetEnumerator() | ForEach-Object { "$($_.Key)($($_.Value))" }) -join ", "
    $deleteDetails = ($deleteTables.GetEnumerator() | ForEach-Object { "$($_.Key)($($_.Value))" }) -join ", "
    
    # === CHECK TRANSACTION HANDLING ===
    $hasStartTransaction = $content -match '(?i)START\s+TRANSACTION'
    $hasCommit = $content -match '(?i)COMMIT'
    $hasRollback = $content -match '(?i)ROLLBACK'
    $hasSavepoint = $content -match '(?i)SAVEPOINT'
    
    $hasTransactionHandling = if ($hasStartTransaction -or $hasCommit -or $hasRollback -or $hasSavepoint) { "Yes" } else { "No" }
    
    # === CHECK VALIDATION ===
    $hasIfChecks = $content -match '(?i)\bIF\b'
    $hasParamValidation = $content -match '(?i)(IS NULL|IS NOT NULL|= NULL|<> NULL)'
    $hasValidation = if ($hasIfChecks -or $hasParamValidation) { "Yes" } else { "No" }
    
    # === DETERMINE PATTERN ===
    $pattern = if ($totalDML -eq 0) { "QUERY" }
               elseif ($insertCount -eq 1 -and $updateCount -eq 0 -and $deleteCount -eq 0) { "SIMPLE_INSERT" }
               elseif ($insertCount -eq 0 -and $updateCount -eq 1 -and $deleteCount -eq 0) { "SIMPLE_UPDATE" }
               elseif ($insertCount -eq 0 -and $updateCount -eq 0 -and $deleteCount -eq 1) { "SIMPLE_DELETE" }
               elseif ($totalDML -gt 1 -and $insertCount -gt 0 -and $updateCount -gt 0) { "SEQUENTIAL" }
               elseif ($insertCount -gt 1) { "BATCH_INSERT" }
               elseif ($updateCount -gt 1) { "BATCH_UPDATE" }
               else { "MIXED_DML" }
    
    # === DETERMINE PROCEDURE TYPE ===
    $procedureType = if ($procName -match '_Add_|_Create_|_Insert_') { "Create" }
                     elseif ($procName -match '_Get_|_Select_|_Retrieve_') { "Read" }
                     elseif ($procName -match '_Update_|_Modify_|_Edit_') { "Update" }
                     elseif ($procName -match '_Delete_|_Remove_') { "Delete" }
                     elseif ($procName -match '_Fix_|_Repair_') { "Maintenance" }
                     elseif ($procName -match '_Transfer_|_Move_') { "Transfer" }
                     else { "Complex" }
    
    # === RECOMMEND TRANSACTION STRATEGY ===
    $recommendedStrategy = if ($totalDML -eq 0) { "NONE (Read-only)" }
                           elseif ($totalDML -eq 1) { "IMPLICIT (Single DML)" }
                           elseif ($totalDML -le 3 -and $hasTransactionHandling -eq "Yes") { "EXPLICIT (Already implemented)" }
                           elseif ($totalDML -le 3) { "IMPLICIT (Few operations)" }
                           elseif ($totalDML -gt 3 -and $hasTransactionHandling -eq "Yes") { "EXPLICIT (Already implemented)" }
                           elseif ($totalDML -gt 3) { "EXPLICIT (Recommended)" }
                           else { "REVIEW REQUIRED" }
    
    # === DETERMINE CONFIDENCE ===
    $confidence = if ($totalDML -eq 0) { "High" }
                  elseif ($totalDML -eq 1) { "High" }
                  elseif ($hasTransactionHandling -eq "Yes") { "High" }
                  elseif ($totalDML -le 3) { "Medium" }
                  else { "Low" }
    
    # === BUILD RATIONALE ===
    $rationaleparts = @()
    
    # Pattern description
    if ($pattern -eq "QUERY") {
        $rationaleparts += "[READ-ONLY]"
    } elseif ($pattern -eq "SIMPLE_INSERT") {
        $rationaleparts += "[SIMPLE_INSERT]"
    } elseif ($pattern -eq "SIMPLE_UPDATE") {
        $rationaleparts += "[SIMPLE_UPDATE]"
    } elseif ($pattern -eq "SIMPLE_DELETE") {
        $rationaleparts += "[SIMPLE_DELETE]"
    } elseif ($pattern -eq "SEQUENTIAL") {
        $rationaleparts += "[SEQUENTIAL]"
    } elseif ($pattern -eq "BATCH_INSERT") {
        $rationaleparts += "[BATCH_INSERT]"
    } elseif ($pattern -eq "BATCH_UPDATE") {
        $rationaleparts += "[BATCH_UPDATE]"
    } elseif ($pattern -eq "MIXED_DML") {
        $rationaleparts += "[MIXED_DML]"
    }
    
    # DML breakdown with table details
    if ($totalDML -gt 0) {
        $dmlParts = @()
        if ($insertCount -gt 0) { 
            $dmlParts += "[$insertCount INSERT$(if($insertCount -gt 1){'s'}): $insertDetails]" 
        }
        if ($updateCount -gt 0) { 
            $dmlParts += "[$updateCount UPDATE$(if($updateCount -gt 1){'s'}): $updateDetails]" 
        }
        if ($deleteCount -gt 0) { 
            $dmlParts += "[$deleteCount DELETE$(if($deleteCount -gt 1){'s'}): $deleteDetails]" 
        }
        $rationaleparts += $dmlParts -join " + "
    }
    
    # Transaction handling
    if ($hasTransactionHandling -eq "Yes") {
        $rationaleparts += "[Has explicit transaction handling]"
    }
    
    # Validation
    if ($hasValidation -eq "Yes") {
        $rationaleparts += "[Has validation]"
    }
    
    $rationale = $rationaleparts -join " | "
    
    # === CREATE RESULT OBJECT ===
    $results += [PSCustomObject]@{
        ProcedureName = $procName
        Domain = $domain
        DetectedPattern = $pattern
        RecommendedStrategy = $recommendedStrategy
        Confidence = $confidence
        ProcedureType = $procedureType
        InsertCount = $insertCount
        InsertTables = $insertDetails
        UpdateCount = $updateCount
        UpdateTables = $updateDetails
        DeleteCount = $deleteCount
        DeleteTables = $deleteDetails
        TotalDMLOps = $totalDML
        HasTransactionHandling = $hasTransactionHandling
        HasValidation = $hasValidation
        Rationale = $rationale
    }
}

$elapsed = (Get-Date) - $startTime
Write-Host "`n`n✓ Analysis complete!`n" -ForegroundColor Green

# Export to CSV
Write-Host "Writing base analysis to CSV..." -ForegroundColor Cyan
$results | Export-Csv -Path $outputFile -NoTypeInformation -Encoding UTF8

Write-Host "✓ Output written to: $outputFile" -ForegroundColor Green
Write-Host "  Total procedures analyzed: $totalFiles" -ForegroundColor Gray
Write-Host "  Time elapsed: $($elapsed.ToString('mm\:ss'))`n" -ForegroundColor Gray
