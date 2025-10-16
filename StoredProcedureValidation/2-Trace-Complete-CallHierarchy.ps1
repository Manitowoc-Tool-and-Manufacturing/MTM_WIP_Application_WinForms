<#
.SYNOPSIS
    Complete call hierarchy tracer - full chain from UI to database
.DESCRIPTION
    Traces the complete execution path:
    Event Handler → Method Chain → DAO → Stored Procedure → Tables → Return Type → UI Element Updated
    
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
$sourceRoot = Join-Path $PSScriptRoot $settings.Paths.CSharpCodeFolder
$sqlPath = Join-Path $PSScriptRoot $settings.Paths.StoredProceduresFolder
$outputFile = Join-Path $PSScriptRoot $settings.Paths.CallHierarchyJSON

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   COMPLETE CALL HIERARCHY TRACING                    ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

if (-not (Test-Path $sourceRoot)) {
    Write-Host "✗ C# code folder not found: $sourceRoot" -ForegroundColor Red
    Write-Host "  Check CSharpCodeFolder path in SPSettings.json" -ForegroundColor Yellow
    exit 1
}

if (-not (Test-Path $sqlPath)) {
    Write-Host "✗ Stored procedures folder not found: $sqlPath" -ForegroundColor Red
    Write-Host "  Check StoredProceduresFolder path in SPSettings.json" -ForegroundColor Yellow
    exit 1
}

if (-not (Test-Path $sqlPath)) {
    Write-Host "✗ Stored procedures folder not found: $sqlPath" -ForegroundColor Red
    Write-Host "  Check StoredProceduresFolder path in SPSettings.json" -ForegroundColor Yellow
    exit 1
}

$totalStartTime = Get-Date

# === PHASE 1: Index C# code ===
Write-Host "Phase 1: Indexing C# code structure..." -ForegroundColor Yellow
$phase1Start = Get-Date

$csFiles = Get-ChildItem -Path $sourceRoot -Recurse -Filter "*.cs" -Exclude "*.Designer.cs","*.g.cs" | 
    Where-Object { 
        $_.FullName -notmatch "\\obj\\|\\bin\\|\\Tests\\|_Tests\\.cs|Test\\.cs" -and
        $_.Name -notlike "*Test*.cs"
    }

$methodIndex = @{}
$eventHandlers = @{}
$daoMethods = @{}

$totalFiles = $csFiles.Count
$currentFile = 0

foreach ($file in $csFiles) {
    $currentFile++
    $percentComplete = [math]::Round(($currentFile / $totalFiles) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r  [{0}{1}] {2}% Indexing C# files..." -f 
        $progressBar, $progressEmpty, $percentComplete) -NoNewline -ForegroundColor Cyan
    
    $content = Get-Content $file.FullName -Raw
    $fileName = [System.IO.Path]::GetFileName($file.Name)
    
    # Extract all methods with their full signatures
    $methodPattern = '(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?([^>]+)>?|void|([\w<>]+))\s+(\w+)\s*\(([^)]*)\)'
    $methodMatches = [regex]::Matches($content, $methodPattern)
    
    foreach ($match in $methodMatches) {
        $returnType = if ($match.Groups[1].Value) { "Task<$($match.Groups[1].Value)>" }
                      elseif ($match.Groups[2].Value) { $match.Groups[2].Value }
                      else { "void" }
        $methodName = $match.Groups[3].Value
        $parameters = $match.Groups[4].Value
        
        if (-not $methodIndex.ContainsKey($methodName)) {
            $methodIndex[$methodName] = @()
        }
        
        $methodIndex[$methodName] += [PSCustomObject]@{
            File = $fileName
            MethodName = $methodName
            ReturnType = $returnType
            Parameters = $parameters
            Content = $content
            Position = $match.Index
        }
        
        # Track DAO methods separately
        if ($fileName -match '^Dao_') {
            if (-not $daoMethods.ContainsKey($methodName)) {
                $daoMethods[$methodName] = @()
            }
            $daoMethods[$methodName] += $methodIndex[$methodName][-1]
        }
    }
    
    # Extract event handlers
    $eventPattern = '(?:private|protected|public)\s+(?:async\s+)?void\s+(\w+)\s*\([^)]*(?:object\s+sender|EventArgs|RoutedEventArgs)[^)]*\)'
    $eventMatches = [regex]::Matches($content, $eventPattern)
    
    foreach ($match in $eventMatches) {
        $handlerName = $match.Groups[1].Value
        
        if (-not $eventHandlers.ContainsKey($handlerName)) {
            $eventHandlers[$handlerName] = @()
        }
        
        # Determine event type and extract UI element name
        $eventType = if ($handlerName -match '(\w+)_(Click|SelectionChanged|TextChanged|Load|Closing)$') {
            $uiElement = $matches[1]
            $eventName = $matches[2]
            @{
                Type = $eventName
                UIElement = $uiElement
                FullName = $handlerName
            }
        } else {
            @{
                Type = "Event Handler"
                UIElement = "Unknown"
                FullName = $handlerName
            }
        }
        
        $eventHandlers[$handlerName] += [PSCustomObject]@{
            File = $fileName
            HandlerName = $handlerName
            EventType = $eventType.Type
            UIElement = $eventType.UIElement
            Content = $content
            Position = $match.Index
        }
    }
}

$phase1Time = ((Get-Date) - $phase1Start).TotalSeconds
Write-Host "`n  ✓ Indexed $($methodIndex.Count) methods, $($eventHandlers.Count) event handlers, $($daoMethods.Count) DAO methods (${phase1Time}s)`n" -ForegroundColor Green

# === PHASE 2: Analyze stored procedures for tables ===
Write-Host "Phase 2: Analyzing stored procedure tables..." -ForegroundColor Yellow
$phase2Start = Get-Date

$sqlFiles = Get-ChildItem -Path $sqlPath -Recurse -Filter "*.sql"
$procTableMap = @{}

$currentFile = 0
foreach ($file in $sqlFiles) {
    $currentFile++
    $percentComplete = [math]::Round(($currentFile / $sqlFiles.Count) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r  [{0}{1}] {2}% Analyzing SQL files..." -f 
        $progressBar, $progressEmpty, $percentComplete) -NoNewline -ForegroundColor Cyan
    
    $procName = $file.BaseName
    $content = Get-Content $file.FullName -Raw
    
    # Extract all table references (INSERT, UPDATE, DELETE, SELECT FROM)
    $tables = @()
    $tablePattern = '(?i)(?:INSERT\s+INTO|UPDATE|DELETE\s+FROM|FROM)\s+`?(\w+)`?'
    $tableMatches = [regex]::Matches($content, $tablePattern)
    
    foreach ($match in $tableMatches) {
        $tableName = $match.Groups[1].Value
        if ($tables -notcontains $tableName) {
            $tables += $tableName
        }
    }
    
    $procTableMap[$procName] = $tables
}

$phase2Time = ((Get-Date) - $phase2Start).TotalSeconds
Write-Host "`n  ✓ Analyzed $($sqlFiles.Count) procedures (${phase2Time}s)`n" -ForegroundColor Green

# === PHASE 3: Find stored procedure call sites ===
Write-Host "Phase 3: Finding stored procedure calls..." -ForegroundColor Yellow
$phase3Start = Get-Date

$spCallSites = @{}
$currentFile = 0

foreach ($file in $csFiles) {
    $currentFile++
    $percentComplete = [math]::Round(($currentFile / $totalFiles) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r  [{0}{1}] {2}% Finding SP calls..." -f 
        $progressBar, $progressEmpty, $percentComplete) -NoNewline -ForegroundColor Cyan
    
    $content = Get-Content $file.FullName -Raw
    $fileName = [System.IO.Path]::GetFileName($file.Name)
    
    # Find stored procedure calls
    $spPattern = '"(inv_\w+|sp_\w+|md_\w+|log_\w+|usr_\w+|sys_\w+)"'
    $spMatches = [regex]::Matches($content, $spPattern)
    
    foreach ($match in $spMatches) {
        $spName = $match.Groups[1].Value
        
        # Find the calling method
        $position = $match.Index
        $beforeCall = $content.Substring(0, $position)
        $methodMatch = [regex]::Match($beforeCall, '(?s)(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+(\w+)\s*\([^)]*\)[^{]*{(?!.*(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+\w+\s*\()')
        
        $callingMethod = if ($methodMatch.Success) { $methodMatch.Groups[1].Value } else { "Unknown" }
        
        # Get return type
        $returnType = if ($methodIndex.ContainsKey($callingMethod)) {
            $methodIndex[$callingMethod][0].ReturnType
        } else { "Unknown" }
        
        if (-not $spCallSites.ContainsKey($spName)) {
            $spCallSites[$spName] = @()
        }
        
        $spCallSites[$spName] += [PSCustomObject]@{
            File = $fileName
            Method = $callingMethod
            ReturnType = $returnType
        }
    }
}

$phase3Time = ((Get-Date) - $phase3Start).TotalSeconds
Write-Host "`n  ✓ Found $($spCallSites.Count) stored procedures called from code (${phase3Time}s)`n" -ForegroundColor Green

# === PHASE 4: Build complete hierarchies ===
Write-Host "Phase 4: Building complete call hierarchies..." -ForegroundColor Yellow
Write-Host "  ℹ️  This phase can be time-intensive for large datasets" -ForegroundColor Gray
$phase4Start = Get-Date

$allStoredProcs = $sqlFiles | ForEach-Object { $_.BaseName }
$results = @{}

$currentProc = 0
foreach ($procName in $allStoredProcs) {
    $currentProc++
    $percentComplete = [math]::Round(($currentProc / $allStoredProcs.Count) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) Processing: {5,-40}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentProc, $allStoredProcs.Count,
        $procName.Substring(0, [Math]::Min(40, $procName.Length))) -NoNewline -ForegroundColor Cyan
    
    $hierarchies = @()
    
    if ($spCallSites.ContainsKey($procName)) {
        foreach ($callSite in $spCallSites[$procName]) {
            $daoMethod = $callSite.Method
            $daoFile = $callSite.File
            $returnType = $callSite.ReturnType
            
            # Find method chain (who calls this DAO method)
            $methodChain = @()
            $eventHandler = $null
            $uiElementUpdated = @{
                OnSuccess = @()
                OnFailure = @()
            }
            
            # Search for callers
            $currentFileInScan = 0
            foreach ($file in $csFiles) {
                $currentFileInScan++
                $fileName = [System.IO.Path]::GetFileName($file.Name)
                
                # Update progress with current file
                Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) {5,-25} → Reading: {6,-30}" -f 
                    $progressBar, $progressEmpty, $percentComplete, $currentProc, $allStoredProcs.Count,
                    $procName.Substring(0, [Math]::Min(25, $procName.Length)),
                    $fileName.Substring(0, [Math]::Min(30, $fileName.Length))) -NoNewline -ForegroundColor Cyan
                
                $content = Get-Content $file.FullName -Raw
                
                # Check if this file calls the DAO method
                if ($content -match "\b$daoMethod\b") {
                    # Look for event handlers that call this method (directly or through chain)
                    foreach ($handler in $eventHandlers.Keys) {
                        if ($eventHandlers[$handler].Count -gt 0) {
                            $handlerInfo = $eventHandlers[$handler][0]
                            $handlerContent = $handlerInfo.Content
                            
                            # Check if handler calls this DAO method directly
                            $daoCallMatch = [regex]::Match($handlerContent, "(\bawait\s+)?$daoMethod\s*\([^)]*\)")
                            if ($daoCallMatch.Success) {
                                $eventHandler = $handlerInfo
                                
                                # Extract code AFTER the DAO call to find UI updates
                                $afterDaoCall = $handlerContent.Substring($daoCallMatch.Index + $daoCallMatch.Length)
                                
                                # Look for success path (if result.IsSuccess, if status == 0, etc.)
                                $successPattern = '(?s)if\s*\([^)]*(?:IsSuccess|status\s*==\s*0|Success)[^)]*\)\s*{([^}]+)}'
                                $successMatch = [regex]::Match($afterDaoCall, $successPattern)
                                if ($successMatch.Success) {
                                    $successBlock = $successMatch.Groups[1].Value
                                    
                                    # Find method calls in success block
                                    $successMethods = [regex]::Matches($successBlock, '\b([A-Z]\w+)\s*\(') | 
                                        ForEach-Object { $_.Groups[1].Value } | 
                                        Where-Object { $_ -match '^(Show|Update|Refresh|Load|Display|Set|Clear|Enable|Disable|Hide|Close)' }
                                    
                                    $uiElementUpdated.OnSuccess = $successMethods | Select-Object -Unique
                                }
                                
                                # Look for failure path (else, if !IsSuccess, if status != 0, etc.)
                                $failurePattern = '(?s)(?:else\s*{([^}]+)}|if\s*\([^)]*(?:!IsSuccess|status\s*!=\s*0|Failed)[^)]*\)\s*{([^}]+)})'
                                $failureMatch = [regex]::Match($afterDaoCall, $failurePattern)
                                if ($failureMatch.Success) {
                                    $failureBlock = if ($failureMatch.Groups[1].Success) { $failureMatch.Groups[1].Value } else { $failureMatch.Groups[2].Value }
                                    
                                    # Find method calls in failure block
                                    $failureMethods = [regex]::Matches($failureBlock, '\b([A-Z]\w+)\s*\(') | 
                                        ForEach-Object { $_.Groups[1].Value } | 
                                        Where-Object { $_ -match '^(Show|Update|Refresh|Load|Display|Set|Clear|Enable|Disable|Hide|MessageBox)' }
                                    
                                    $uiElementUpdated.OnFailure = $failureMethods | Select-Object -Unique
                                }
                                
                                break
                            }
                            
                            # Check for intermediate methods
                            $afterHandler = $handlerContent.Substring($handlerInfo.Position)
                            $handlerBody = if ($afterHandler -match '(?s){([^}]*)}') { $matches[1] } else { "" }
                            
                            # Find intermediate method calls
                            $intermediateCalls = [regex]::Matches($handlerBody, '\b(\w+)\s*\(')
                            foreach ($call in $intermediateCalls) {
                                $intermediateMethod = $call.Groups[1].Value
                                
                                if ($methodIndex.ContainsKey($intermediateMethod)) {
                                    $intermediateInfo = $methodIndex[$intermediateMethod][0]
                                    $intermediateContent = $intermediateInfo.Content
                                    
                                    # Check if intermediate method calls DAO
                                    $intermediateDaoMatch = [regex]::Match($intermediateContent, "(\bawait\s+)?$daoMethod\s*\([^)]*\)")
                                    if ($intermediateDaoMatch.Success) {
                                        $eventHandler = $handlerInfo
                                        $methodChain += $intermediateMethod
                                        
                                        # Extract UI updates from intermediate method
                                        $afterIntermediateDao = $intermediateContent.Substring($intermediateDaoMatch.Index + $intermediateDaoMatch.Length)
                                        
                                        # Success path in intermediate
                                        $successPattern = '(?s)if\s*\([^)]*(?:IsSuccess|status\s*==\s*0|Success)[^)]*\)\s*{([^}]+)}'
                                        $successMatch = [regex]::Match($afterIntermediateDao, $successPattern)
                                        if ($successMatch.Success) {
                                            $successBlock = $successMatch.Groups[1].Value
                                            $successMethods = [regex]::Matches($successBlock, '\b([A-Z]\w+)\s*\(') | 
                                                ForEach-Object { $_.Groups[1].Value } | 
                                                Where-Object { $_ -match '^(Show|Update|Refresh|Load|Display|Set|Clear|Enable|Disable|Hide|Close)' }
                                            $uiElementUpdated.OnSuccess = $successMethods | Select-Object -Unique
                                        }
                                        
                                        # Failure path in intermediate
                                        $failurePattern = '(?s)(?:else\s*{([^}]+)}|if\s*\([^)]*(?:!IsSuccess|status\s*!=\s*0|Failed)[^)]*\)\s*{([^}]+)})'
                                        $failureMatch = [regex]::Match($afterIntermediateDao, $failurePattern)
                                        if ($failureMatch.Success) {
                                            $failureBlock = if ($failureMatch.Groups[1].Success) { $failureMatch.Groups[1].Value } else { $failureMatch.Groups[2].Value }
                                            $failureMethods = [regex]::Matches($failureBlock, '\b([A-Z]\w+)\s*\(') | 
                                                ForEach-Object { $_.Groups[1].Value } | 
                                                Where-Object { $_ -match '^(Show|Update|Refresh|Load|Display|Set|Clear|Enable|Disable|Hide|MessageBox)' }
                                            $uiElementUpdated.OnFailure = $failureMethods | Select-Object -Unique
                                        }
                                        
                                        break
                                    }
                                }
                            }
                            
                            if ($eventHandler) { break }
                        }
                    }
                    
                    if ($eventHandler) { break }
                }
            }
            
            # Get tables from proc
            $tables = if ($procTableMap.ContainsKey($procName)) { $procTableMap[$procName] } else { @() }
            
            if ($eventHandler) {
                # Build full event handler name with form
                $formName = $eventHandler.File
                $fullEventHandler = "$formName.$($eventHandler.HandlerName)"
                
                $hierarchies += [PSCustomObject]@{
                    Status = "Called from UI"
                    EventHandler = $fullEventHandler
                    MethodChain = $methodChain
                    DAO = @{
                        File = $daoFile
                        Method = $daoMethod
                    }
                    StoredProcedure = $procName
                    Tables = $tables
                    ReturnType = $returnType
                    UIElementUpdated = @{
                        OnSuccess = if ($uiElementUpdated.OnSuccess.Count -gt 0) { $uiElementUpdated.OnSuccess } else { @("None detected") }
                        OnFailure = if ($uiElementUpdated.OnFailure.Count -gt 0) { $uiElementUpdated.OnFailure } else { @("None detected") }
                    }
                }
            } else {
                $hierarchies += [PSCustomObject]@{
                    Status = "Called from DAO (no UI event handler traced)"
                    DAO = @{
                        File = $daoFile
                        Method = $daoMethod
                    }
                    StoredProcedure = $procName
                    Tables = $tables
                    ReturnType = $returnType
                }
            }
        }
    } else {
        # No C# calls found
        $tables = if ($procTableMap.ContainsKey($procName)) { $procTableMap[$procName] } else { @() }
        
        $hierarchies += [PSCustomObject]@{
            Status = "Not called from C# code"
            StoredProcedure = $procName
            Tables = $tables
            Note = "May be legacy, unused, called from other stored procedures, or background services"
        }
    }
    
    $results[$procName] = $hierarchies
}

$phase4Time = ((Get-Date) - $phase4Start).TotalSeconds
Write-Host "`n  ✓ Built complete hierarchies (${phase4Time}s)`n" -ForegroundColor Green

# === PHASE 5: Export ===
Write-Host "Phase 5: Writing results..." -ForegroundColor Yellow
$results | ConvertTo-Json -Depth 10 | Set-Content $OutputFile -Encoding UTF8

$totalTime = ((Get-Date) - $totalStartTime).TotalSeconds

Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║   ✓ COMPLETE HIERARCHY TRACING DONE                  ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host "  Output: $OutputFile" -ForegroundColor Gray
Write-Host "  Total time: ${totalTime}s`n" -ForegroundColor Gray
