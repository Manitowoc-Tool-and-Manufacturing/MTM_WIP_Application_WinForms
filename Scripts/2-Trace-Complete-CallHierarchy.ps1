<#
.SYNOPSIS
    Enhanced call hierarchy tracer - ensures ALL procedures show hierarchy status
.DESCRIPTION
    Traces call paths from UI → DAO → Stored Procedure
    For procedures NOT called from UI, shows:
    - "Not called from UI code"
    - "Legacy/unused procedure"
    - "Called from other stored procedures"
    - "Background service only"
#>

[CmdletBinding()]
param(
    [string]$SourceRoot = "..",
    [string]$OutputFile = ".\call-hierarchy-complete.json"
)

Write-Host "=== Complete Call Hierarchy Tracer ===" -ForegroundColor Cyan
Write-Host "Tracing ALL stored procedure call paths..." -ForegroundColor Gray

# Get all C# files (excluding tests)
$csFiles = Get-ChildItem -Path $SourceRoot -Recurse -Filter "*.cs" -Exclude "*.Designer.cs","*.g.cs" | 
    Where-Object { 
        $_.FullName -notmatch "\\obj\\|\\bin\\|\\Tests\\|_Tests\\.cs|Test\\.cs" -and
        $_.Name -notlike "*Test*.cs" -and
        $_.Name -notlike "*_Tests.cs"
    }

Write-Host "Found $($csFiles.Count) production C# files`n" -ForegroundColor Green

# Build method and event handler indexes
$methodIndex = @{}
$eventHandlers = @{}

Write-Host "Building code index..." -ForegroundColor Gray

foreach ($file in $csFiles) {
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
        
        $methodIndex[$methodName] += @{
            File = [System.IO.Path]::GetFileName($relativePath)
            FullPath = $file.FullName
            MethodName = $methodName
        }
        
        # Detect event handlers
        if ($methodName -match '_Click$|_SelectedIndexChanged$|_TextChanged$|_KeyPress$|_CheckedChanged$|_Load$|_Enter$|_Leave$|_Shown$|_CellClick$') {
            $eventType = switch -Regex ($methodName) {
                '_Click$' { 'Button.Click' }
                '_SelectedIndexChanged$' { 'ComboBox.SelectedIndexChanged' }
                '_TextChanged$' { 'TextBox.TextChanged' }
                '_KeyPress$' { 'Control.KeyPress' }
                '_CheckedChanged$' { 'CheckBox.CheckedChanged' }
                '_Load$' { 'Form.Load' }
                '_Shown$' { 'Form.Shown' }
                '_Enter$' { 'Control.Enter' }
                '_Leave$' { 'Control.Leave' }
                '_CellClick$' { 'DataGridView.CellClick' }
                default { 'EventHandler' }
            }
            
            if (-not $eventHandlers.ContainsKey($methodName)) {
                $eventHandlers[$methodName] = @{
                    File = [System.IO.Path]::GetFileName($relativePath)
                    EventType = $eventType
                    MethodName = $methodName
                }
            }
        }
    }
}

Write-Host "  Methods indexed: $($methodIndex.Keys.Count)" -ForegroundColor White
Write-Host "  Event handlers found: $($eventHandlers.Keys.Count)`n" -ForegroundColor White

# Find stored procedure calls
Write-Host "Finding stored procedure call sites..." -ForegroundColor Gray

$storedProcedureCallSites = @{}

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    $relativePath = $file.FullName.Replace($SourceRoot, "").TrimStart('\')
    
    # Only search in production code
    $isProductionCode = $relativePath -match "\\Data\\|\\Controls\\|\\Forms\\|\\Helpers\\"
    
    if (-not $isProductionCode) {
        continue
    }
    
    # Find stored procedure calls
    $spCallPattern = 'Helper_Database_StoredProcedure\.Execute\w+\s*\([^"]*"([^"]+)"'
    $spMatches = [regex]::Matches($content, $spCallPattern)
    
    foreach ($spMatch in $spMatches) {
        $procedureName = $spMatch.Groups[1].Value
        $lineNumber = ($content.Substring(0, $spMatch.Index) -split "`n").Count
        
        # Find containing method
        $methodDeclPattern = '(?:^|\n)\s*(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+(\w+)\s*\([^)]*\)\s*(?:\{|=>)'
        $allMethods = [regex]::Matches($content, $methodDeclPattern, [System.Text.RegularExpressions.RegexOptions]::Multiline)
        
        $containingMethod = $null
        foreach ($methodMatch in $allMethods) {
            if ($methodMatch.Index -lt $spMatch.Index) {
                $containingMethod = $methodMatch.Groups[1].Value
            }
        }
        
        if ($containingMethod) {
            $isDaoFile = $relativePath -match "\\Data\\"
            
            if ($isDaoFile) {
                if (-not $storedProcedureCallSites.ContainsKey($procedureName)) {
                    $storedProcedureCallSites[$procedureName] = @()
                }
                
                $storedProcedureCallSites[$procedureName] += @{
                    File = [System.IO.Path]::GetFileName($relativePath)
                    Method = $containingMethod
                    LineNumber = $lineNumber
                    FullPath = $file.FullName
                }
            }
        }
    }
}

Write-Host "  Stored procedures with call sites: $(($storedProcedureCallSites.Keys | Measure-Object).Count)`n" -ForegroundColor White

# Function to trace callers
function Find-MethodCallers {
    param(
        [string]$MethodName,
        [int]$MaxDepth = 3,
        [int]$CurrentDepth = 0
    )
    
    if ($CurrentDepth -ge $MaxDepth) {
        return @()
    }
    
    $callers = @()
    
    foreach ($file in $csFiles) {
        $content = Get-Content $file.FullName -Raw
        $relativePath = $file.FullName.Replace($SourceRoot, "").TrimStart('\')
        
        if (-not ($relativePath -match "\\Controls\\|\\Forms\\|\\Data\\")) {
            continue
        }
        
        $callPattern = "(?:await\s+)?(?:\w+\.)?$MethodName\s*\("
        $callMatches = [regex]::Matches($content, $callPattern, [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
        
        if ($callMatches.Count -gt 0) {
            foreach ($match in $callMatches) {
                $beforeCall = $content.Substring(0, $match.Index)
                $methodDeclPattern = '(?:^|\n)\s*(?:public|private|protected|internal)\s+(?:static\s+)?(?:async\s+)?(?:Task<?[\w<>]+>?|void|[\w<>]+)\s+(\w+)\s*\([^)]*\)\s*(?:\{|=>)'
                $methodMatches = [regex]::Matches($beforeCall, $methodDeclPattern, [System.Text.RegularExpressions.RegexOptions]::Multiline)
                
                if ($methodMatches.Count -gt 0) {
                    $lastMethod = $methodMatches[$methodMatches.Count - 1]
                    $callingMethod = $lastMethod.Groups[1].Value
                    
                    $callers += @{
                        File = [System.IO.Path]::GetFileName($relativePath)
                        Method = $callingMethod
                        IsEventHandler = $eventHandlers.ContainsKey($callingMethod)
                        EventType = if ($eventHandlers.ContainsKey($callingMethod)) { $eventHandlers[$callingMethod].EventType } else { $null }
                    }
                    
                    if (-not $eventHandlers.ContainsKey($callingMethod)) {
                        $parentCallers = Find-MethodCallers -MethodName $callingMethod -MaxDepth $MaxDepth -CurrentDepth ($CurrentDepth + 1)
                        foreach ($parentCaller in $parentCallers) {
                            $callers += $parentCaller
                        }
                    }
                }
            }
        }
    }
    
    return $callers
}

# Get list of all stored procedures from SQL files
Write-Host "Getting all stored procedures from SQL files..." -ForegroundColor Gray
$allSqlFiles = Get-ChildItem -Path ".\UpdatedStoredProcedures\ReadyForVerification" -Recurse -Filter "*.sql"
$allProcedureNames = $allSqlFiles | ForEach-Object { [System.IO.Path]::GetFileNameWithoutExtension($_.Name) }

Write-Host "  Total stored procedures: $($allProcedureNames.Count)`n" -ForegroundColor White

# Build complete hierarchy for ALL procedures
Write-Host "Building complete call hierarchy..." -ForegroundColor Cyan

$completeHierarchy = @{}

foreach ($procName in $allProcedureNames) {
    $hierarchies = @()
    
    if ($storedProcedureCallSites.ContainsKey($procName)) {
        # Procedure IS called from code
        foreach ($callSite in $storedProcedureCallSites[$procName]) {
            $daoFile = [System.IO.Path]::GetFileNameWithoutExtension($callSite.File)
            $daoMethod = $callSite.Method
            
            $callers = Find-MethodCallers -MethodName $daoMethod -MaxDepth 3
            $eventHandlerChain = $callers | Where-Object { $_.IsEventHandler } | Select-Object -First 1
            
            if ($eventHandlerChain) {
                $intermediateMethodsChain = $callers | 
                    Where-Object { -not $_.IsEventHandler -and $_.Method -ne $daoMethod } | 
                    Select-Object -ExpandProperty Method -Unique
                
                $hierarchies += @{
                    Status = "Called from UI"
                    EventHandler = @{
                        Type = $eventHandlerChain.EventType
                        Method = $eventHandlerChain.Method
                        File = $eventHandlerChain.File
                    }
                    IntermediateMethods = @($intermediateMethodsChain)
                    DAO = @{
                        File = $daoFile
                        Method = $daoMethod
                    }
                }
            } else {
                # Called from DAO but no UI event handler found
                $hierarchies += @{
                    Status = "Called from DAO (no UI event handler traced)"
                    EventHandler = $null
                    IntermediateMethods = @()
                    DAO = @{
                        File = $daoFile
                        Method = $daoMethod
                    }
                }
            }
        }
    } else {
        # Procedure NOT called from code
        $hierarchies += @{
            Status = "Not called from C# code"
            EventHandler = $null
            IntermediateMethods = @()
            DAO = $null
            Note = "May be legacy, unused, called from other stored procedures, or used by background services"
        }
    }
    
    $completeHierarchy[$procName] = $hierarchies
}

Write-Host "`nSaving complete call hierarchy..." -ForegroundColor Cyan
$completeHierarchy | ConvertTo-Json -Depth 10 | Out-File -FilePath $OutputFile -Encoding UTF8
Write-Host "✓ Saved: $OutputFile" -ForegroundColor Green

$calledFromUI = ($completeHierarchy.Values | Where-Object { $_[0].Status -eq "Called from UI" }).Count
$calledFromDAO = ($completeHierarchy.Values | Where-Object { $_[0].Status -eq "Called from DAO (no UI event handler traced)" }).Count
$notCalled = ($completeHierarchy.Values | Where-Object { $_[0].Status -eq "Not called from C# code" }).Count

Write-Host "`n=== Call Hierarchy Summary ===" -ForegroundColor Cyan
Write-Host "  Called from UI: $calledFromUI" -ForegroundColor Green
Write-Host "  Called from DAO only: $calledFromDAO" -ForegroundColor Yellow
Write-Host "  Not called from code: $notCalled" -ForegroundColor Red
Write-Host "  Total procedures: $($completeHierarchy.Keys.Count)" -ForegroundColor White
