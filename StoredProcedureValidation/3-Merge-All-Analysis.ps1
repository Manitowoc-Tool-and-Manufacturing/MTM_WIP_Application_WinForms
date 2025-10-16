<#
.SYNOPSIS
    Merge all analysis data with progress reporting
.DESCRIPTION
    Combines original analysis + SQL operations + call hierarchy
    Shows progress for loading and merging steps
    
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
$baseAnalysis = Join-Path $PSScriptRoot "procedure-base-analysis.csv"
$sqlOperationsFile = Join-Path $PSScriptRoot $settings.Paths.SQLOperationsJSON
$callHierarchyFile = Join-Path $PSScriptRoot $settings.Paths.CallHierarchyJSON
$outputFile = Join-Path $PSScriptRoot $settings.Paths.OutputCSV

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   MERGING ALL ANALYSIS DATA                           ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

$totalStartTime = Get-Date

# === STEP 1: Load base analysis ===
Write-Host "Step 1: Loading base procedure analysis..." -ForegroundColor Yellow
$step1Start = Get-Date

if (-not (Test-Path $baseAnalysis)) {
    Write-Host "✗ Base analysis file not found: $baseAnalysis" -ForegroundColor Red
    exit 1
}

$baseData = Import-Csv $baseAnalysis

$step1Time = ((Get-Date) - $step1Start).TotalSeconds
Write-Host "  ✓ Loaded $($baseData.Count) procedures (${step1Time}s)`n" -ForegroundColor Green

# === STEP 2: Load SQL operations ===
Write-Host "Step 2: Loading SQL operations..." -ForegroundColor Yellow
$step2Start = Get-Date

if (-not (Test-Path $sqlOperationsFile)) {
    Write-Host "✗ SQL operations file not found: $sqlOperationsFile" -ForegroundColor Red
    exit 1
}

$sqlOpsJson = Get-Content $sqlOperationsFile -Raw | ConvertFrom-Json
$sqlOps = @{}
foreach ($prop in $sqlOpsJson.PSObject.Properties) {
    $sqlOps[$prop.Name] = $prop.Value
}

$step2Time = ((Get-Date) - $step2Start).TotalSeconds
Write-Host "  ✓ Loaded SQL operations for $($sqlOps.Count) procedures (${step2Time}s)`n" -ForegroundColor Green

# === STEP 3: Load call hierarchy ===
Write-Host "Step 3: Loading call hierarchy..." -ForegroundColor Yellow
$step3Start = Get-Date

if (-not (Test-Path $callHierarchyFile)) {
    Write-Host "✗ Call hierarchy file not found: $callHierarchyFile" -ForegroundColor Red
    exit 1
}

$callHierJson = Get-Content $callHierarchyFile -Raw | ConvertFrom-Json
$callHier = @{}
foreach ($prop in $callHierJson.PSObject.Properties) {
    $callHier[$prop.Name] = $prop.Value
}

$step3Time = ((Get-Date) - $step3Start).TotalSeconds
Write-Host "  ✓ Loaded call hierarchy for $($callHier.Count) procedures (${step3Time}s)`n" -ForegroundColor Green

# === STEP 3.5: Find missing procedures (called from code but no SQL file) ===
Write-Host "Step 3.5: Checking for missing stored procedures..." -ForegroundColor Yellow

$allCalledProcs = $callHier.Keys | Where-Object { 
    $status = $callHier[$_][0].Status
    $status -like "*Called*"
}

$existingProcs = $baseData.ProcedureName
$missingProcs = $allCalledProcs | Where-Object { $_ -notin $existingProcs }

if ($missingProcs) {
    Write-Host "  ⚠ Found $($missingProcs.Count) procedures called from code but missing SQL files:" -ForegroundColor Yellow
    $missingProcs | ForEach-Object { Write-Host "    • $_" -ForegroundColor Red }
    Write-Host ""
} else {
    Write-Host "  ✓ All called procedures have SQL files`n" -ForegroundColor Green
}

# === STEP 4: Merge data ===
Write-Host "Step 4: Merging all data..." -ForegroundColor Yellow
$step4Start = Get-Date

$enhancedData = @()
$totalProcs = $baseData.Count + $missingProcs.Count
$currentProc = 0

# First, merge existing procedures
foreach ($proc in $baseData) {
    $currentProc++
    $percentComplete = [math]::Round(($currentProc / $totalProcs) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    $procName = $proc.ProcedureName
    Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) Merging: {5,-35}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentProc, $totalProcs,
        $procName.Substring(0, [Math]::Min(35, $procName.Length))) -NoNewline -ForegroundColor Cyan
    
    # Get SQL operations detail
    $sqlDetail = if ($sqlOps.ContainsKey($procName)) {
        $sqlOps[$procName] | ConvertTo-Json -Compress -Depth 10
    } else {
        '{"Operations": []}'
    }
    
    # Get call hierarchy
    $hierarchy = if ($callHier.ContainsKey($procName)) {
        $callHier[$procName] | ConvertTo-Json -Compress -Depth 10
    } else {
        '[{"Status": "Unknown"}]'
    }
    
    # Create enhanced row
    $enhanced = [PSCustomObject]@{
        ProcedureName = $proc.ProcedureName
        Domain = $proc.Domain
        DetectedPattern = $proc.DetectedPattern
        RecommendedStrategy = $proc.RecommendedStrategy
        Confidence = $proc.Confidence
        ProcedureType = $proc.ProcedureType
        InsertCount = $proc.InsertCount
        InsertTables = if ($proc.PSObject.Properties['InsertTables']) { $proc.InsertTables } else { "" }
        UpdateCount = $proc.UpdateCount
        UpdateTables = if ($proc.PSObject.Properties['UpdateTables']) { $proc.UpdateTables } else { "" }
        DeleteCount = $proc.DeleteCount
        DeleteTables = if ($proc.PSObject.Properties['DeleteTables']) { $proc.DeleteTables } else { "" }
        TotalDMLOps = $proc.TotalDMLOps
        HasTransactionHandling = $proc.HasTransactionHandling
        HasValidation = $proc.HasValidation
        Rationale = $proc.Rationale
        SQLOperationsDetail = $sqlDetail
        CallHierarchy = $hierarchy
        DeveloperCorrection = if ($proc.PSObject.Properties['DeveloperCorrection']) { $proc.DeveloperCorrection } else { "" }
        Notes = if ($proc.PSObject.Properties['Notes']) { $proc.Notes } else { "" }
        NeedsAttention = if ($proc.PSObject.Properties['NeedsAttention']) { $proc.NeedsAttention } else { "False" }
    }
    
    $enhancedData += $enhanced
}

# Second, add rows for missing procedures
foreach ($procName in $missingProcs) {
    $currentProc++
    $percentComplete = [math]::Round(($currentProc / $totalProcs) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) Adding missing: {5,-35}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentProc, $totalProcs,
        $procName.Substring(0, [Math]::Min(35, $procName.Length))) -NoNewline -ForegroundColor Red
    
    # Get call hierarchy (we know it exists because we found it in the missing list)
    $hierarchy = $callHier[$procName] | ConvertTo-Json -Compress -Depth 10
    
    # Determine domain from procedure name prefix
    $domain = if ($procName -match '^inv_') { "Inventory" }
              elseif ($procName -match '^(trans_|transaction_)') { "Transactions" }
              elseif ($procName -match '^(log_|err_)') { "Logging" }
              elseif ($procName -match '^md_') { "Master Data" }
              elseif ($procName -match '^sys_') { "System" }
              elseif ($procName -match '^usr_|^user_') { "Users" }
              else { "Unknown" }
    
    # Create row for missing procedure
    $enhanced = [PSCustomObject]@{
        ProcedureName = $procName
        Domain = $domain
        DetectedPattern = "MISSING_SQL_FILE"
        RecommendedStrategy = "CREATE_STORED_PROCEDURE"
        Confidence = "N/A"
        ProcedureType = "MISSING"
        InsertCount = "?"
        InsertTables = ""
        UpdateCount = "?"
        UpdateTables = ""
        DeleteCount = "?"
        DeleteTables = ""
        TotalDMLOps = "?"
        HasTransactionHandling = "Unknown"
        HasValidation = "Unknown"
        Rationale = "[MISSING SQL FILE] This stored procedure is called from C# code but no .sql file exists. User must provide details in Notes field to generate this procedure."
        SQLOperationsDetail = '{"Error": "SQL file not found"}'
        CallHierarchy = $hierarchy
        DeveloperCorrection = ""
        Notes = "⚠️ MISSING STORED PROCEDURE: Please describe what this procedure should do, what parameters it needs, what tables it should interact with, and what it should return."
        NeedsAttention = "True"
    }
    
    $enhancedData += $enhanced
}

$step4Time = ((Get-Date) - $step4Start).TotalSeconds
Write-Host "`n`n✓ Merging complete!`n" -ForegroundColor Green

# === Export ===
Write-Host "Exporting merged analysis..." -ForegroundColor Cyan
$enhancedData | Export-Csv -Path $outputFile -NoTypeInformation -Encoding UTF8

# === STEP 5: Export ===
Write-Host "Step 5: Writing enhanced CSV..." -ForegroundColor Yellow
$step5Start = Get-Date

$enhancedData | Export-Csv $outputFile -NoTypeInformation -Encoding UTF8

$step5Time = ((Get-Date) - $step5Start).TotalSeconds
$totalTime = ((Get-Date) - $totalStartTime).TotalSeconds

Write-Host "  ✓ Output written to: $outputFile (${step5Time}s)" -ForegroundColor Green
Write-Host ""

Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║   ✓ MERGE COMPLETE                                    ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host "  Total procedures: $($enhancedData.Count)" -ForegroundColor Gray
Write-Host "  Existing procedures: $($baseData.Count)" -ForegroundColor Gray
Write-Host "  Missing procedures: $($missingProcs.Count)" -ForegroundColor Yellow
Write-Host "  Total time: ${totalTime}s" -ForegroundColor Gray
Write-Host ""
