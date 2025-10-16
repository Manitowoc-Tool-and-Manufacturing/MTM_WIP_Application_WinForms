<#
.SYNOPSIS
    Enhanced call hierarchy tracer with progress reporting
.DESCRIPTION
    Traces call paths from UI → DAO → Stored Procedure
    Shows detailed progress for each phase of analysis
#>

[CmdletBinding()]
param(
    [string]$SourceRoot = "..",
    [string]$OutputFile = ".\call-hierarchy-complete.json"
)

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   CALL HIERARCHY TRACING                              ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

$totalStartTime = Get-Date

# === PHASE 1: Get all C# files ===
Write-Host "Phase 1: Scanning for C# files..." -ForegroundColor Yellow
$phase1Start = Get-Date

$csFiles = Get-ChildItem -Path $SourceRoot -Recurse -Filter "*.cs" -Exclude "*.Designer.cs","*.g.cs" | 
    Where-Object { 
        $_.FullName -notmatch "\\obj\\|\\bin\\|\\Tests\\|_Tests\\.cs|Test\\.cs" -and
        $_.Name -notlike "*Test*.cs" -and
        $_.Name -notlike "*_Tests.cs"
    }

$phase1Time = ((Get-Date) - $phase1Start).TotalSeconds
Write-Host "  ✓ Found $($csFiles.Count) production C# files (${phase1Time}s)`n" -ForegroundColor Green

# === PHASE 2: Build method and event handler indexes ===
Write-Host "Phase 2: Building code index..." -ForegroundColor Yellow
$phase2Start = Get-Date

$methodIndex = @{}
$eventHandlers = @{}
$totalFiles = $csFiles.Count
$currentFile = 0

foreach ($file in $csFiles) {
    $currentFile++
    $percentComplete = [math]::Round(($currentFile / $totalFiles) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    $fileName = [System.IO.Path]::GetFileName($file.Name)
    Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) Indexing: {5,-35}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentFile, $totalFiles,
        $fileName.Substring(0, [Math]::Min(35, $fileName.Length))) -NoNewline -ForegroundColor Cyan
    
    $content = Get-Content $file.FullName -Raw
    $relativePath = $file.FullName.Replace($SourceRoot, "").TrimStart('\')
    
    # Extract methods
    $methodPattern = '(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+(\w+)\s*\([^)]*\)'
    $methods = [regex]::Matches($content, $methodPattern)
    
    foreach ($match in $methods) {
        $methodName = $match.Groups[1].Value
        
        if (-not $methodIndex.ContainsKey($methodName)) {
            $methodIndex[$methodName] = @()
        }
        
        $methodIndex[$methodName] += [PSCustomObject]@{
            File = [System.IO.Path]::GetFileName($relativePath)
            FullPath = $relativePath
            MethodName = $methodName
        }
    }
    
    # Extract event handlers
    $eventPattern = '(?:private|protected|public)\s+(?:async\s+)?void\s+(\w+)\s*\([^)]*(?:object\s+sender|EventArgs|RoutedEventArgs)[^)]*\)'
    $events = [regex]::Matches($content, $eventPattern)
    
    foreach ($match in $events) {
        $handlerName = $match.Groups[1].Value
        
        if (-not $eventHandlers.ContainsKey($handlerName)) {
            $eventHandlers[$handlerName] = @()
        }
        
        # Determine event type
        $eventType = if ($handlerName -match '_Click$') { "Button.Click" }
                     elseif ($handlerName -match '_SelectionChanged$') { "SelectionChanged" }
                     elseif ($handlerName -match '_TextChanged$') { "TextChanged" }
                     elseif ($handlerName -match '_Load$') { "Form.Load" }
                     else { "Event Handler" }
        
        $eventHandlers[$handlerName] += [PSCustomObject]@{
            File = [System.IO.Path]::GetFileName($relativePath)
            FullPath = $relativePath
            HandlerName = $handlerName
            EventType = $eventType
        }
    }
}

$phase2Time = ((Get-Date) - $phase2Start).TotalSeconds
Write-Host "`n  ✓ Indexed $($methodIndex.Count) methods, $($eventHandlers.Count) event handlers (${phase2Time}s)`n" -ForegroundColor Green

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
    
    $fileName = [System.IO.Path]::GetFileName($file.Name)
    Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) Scanning: {5,-35}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentFile, $totalFiles,
        $fileName.Substring(0, [Math]::Min(35, $fileName.Length))) -NoNewline -ForegroundColor Cyan
    
    $content = Get-Content $file.FullName -Raw
    
    # Find stored procedure calls
    $spPattern = '"(inv_\w+|sp_\w+)"'
    $matches = [regex]::Matches($content, $spPattern)
    
    foreach ($match in $matches) {
        $spName = $match.Groups[1].Value
        
        if (-not $spCallSites.ContainsKey($spName)) {
            $spCallSites[$spName] = @()
        }
        
        # Find calling method
        $position = $match.Index
        $beforeCall = $content.Substring(0, $position)
        $methodMatch = [regex]::Match($beforeCall, '(?s)(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+(\w+)\s*\([^)]*\)[^{]*{(?!.*(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+\w+\s*\()')
        
        $callingMethod = if ($methodMatch.Success) { $methodMatch.Groups[1].Value } else { "Unknown" }
        
        $spCallSites[$spName] += [PSCustomObject]@{
            File = [System.IO.Path]::GetFileName($file.Name)
            Method = $callingMethod
        }
    }
}

$phase3Time = ((Get-Date) - $phase3Start).TotalSeconds
Write-Host "`n  ✓ Found $($spCallSites.Count) stored procedures called from code (${phase3Time}s)`n" -ForegroundColor Green

# === PHASE 4: Get all stored procedures ===
Write-Host "Phase 4: Loading stored procedure list..." -ForegroundColor Yellow
$phase4Start = Get-Date

$sqlDir = Join-Path $PSScriptRoot "UpdatedStoredProcedures\ReadyForVerification"
$allStoredProcs = Get-ChildItem -Path $sqlDir -Filter "*.sql" -File -Recurse | ForEach-Object { $_.BaseName }

$phase4Time = ((Get-Date) - $phase4Start).TotalSeconds
Write-Host "  ✓ Found $($allStoredProcs.Count) total stored procedures (${phase4Time}s)`n" -ForegroundColor Green

# === PHASE 5: Build complete hierarchy ===
Write-Host "Phase 5: Building call hierarchies..." -ForegroundColor Yellow
$phase5Start = Get-Date

$results = @{}
$totalProcs = $allStoredProcs.Count
$currentProc = 0

foreach ($procName in $allStoredProcs) {
    $currentProc++
    $percentComplete = [math]::Round(($currentProc / $totalProcs) * 100)
    $progressBar = "█" * [math]::Floor($percentComplete / 2)
    $progressEmpty = "░" * (50 - [math]::Floor($percentComplete / 2))
    
    Write-Host ("`r  [{0}{1}] {2}% ({3}/{4}) Tracing: {5,-35}" -f 
        $progressBar, $progressEmpty, $percentComplete, $currentProc, $totalProcs,
        $procName.Substring(0, [Math]::Min(35, $procName.Length))) -NoNewline -ForegroundColor Cyan
    
    $hierarchies = @()
    
    if ($spCallSites.ContainsKey($procName)) {
        foreach ($callSite in $spCallSites[$procName]) {
            $daoMethod = $callSite.Method
            $daoFile = $callSite.File
            
            # Try to find event handler that calls this DAO method
            $eventHandler = $null
            $intermediateMethods = @()
            
            foreach ($file in $csFiles) {
                $content = Get-Content $file.FullName -Raw
                
                if ($content -match "(?s)(?:private|protected|public)\s+(?:async\s+)?void\s+(\w+)\s*\([^)]*(?:EventArgs|sender)[^)]*\)[^{]*{[^}]*\b$daoMethod\b") {
                    $handlerName = $matches[1]
                    
                    if ($eventHandlers.ContainsKey($handlerName)) {
                        $eventHandler = $eventHandlers[$handlerName][0]
                        break
                    }
                }
            }
            
            if ($eventHandler) {
                $hierarchies += [PSCustomObject]@{
                    Status = "Called from UI"
                    EventHandler = [PSCustomObject]@{
                        Type = $eventHandler.EventType
                        Method = $eventHandler.HandlerName
                        File = $eventHandler.File
                    }
                    IntermediateMethods = $intermediateMethods
                    DAO = [PSCustomObject]@{
                        File = $daoFile
                        Method = $daoMethod
                    }
                }
            } else {
                $hierarchies += [PSCustomObject]@{
                    Status = "Called from DAO (no UI event handler traced)"
                    DAO = [PSCustomObject]@{
                        File = $daoFile
                        Method = $daoMethod
                    }
                    Note = "Called directly from DAO or service layer, no UI event handler found"
                }
            }
        }
    } else {
        $hierarchies += [PSCustomObject]@{
            Status = "Not called from C# code"
            Note = "May be legacy, unused, called from other stored procedures, or used by background services"
        }
    }
    
    $results[$procName] = $hierarchies
}

$phase5Time = ((Get-Date) - $phase5Start).TotalSeconds
Write-Host "`n  ✓ Built hierarchies for all procedures (${phase5Time}s)`n" -ForegroundColor Green

# === PHASE 6: Export ===
Write-Host "Phase 6: Writing results..." -ForegroundColor Yellow
$phase6Start = Get-Date

$results | ConvertTo-Json -Depth 10 | Set-Content $OutputFile -Encoding UTF8

$phase6Time = ((Get-Date) - $phase6Start).TotalSeconds
$totalTime = ((Get-Date) - $totalStartTime).TotalSeconds

Write-Host "  ✓ Output written to: $OutputFile (${phase6Time}s)`n" -ForegroundColor Green

Write-Host "╔════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║   ✓ CALL HIERARCHY COMPLETE                           ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host "  Total procedures: $totalProcs" -ForegroundColor Gray
Write-Host "  Total time: ${totalTime}s`n" -ForegroundColor Gray
