<#
.SYNOPSIS
    Analyzes DAO and test files to identify methods needing transaction support.

.DESCRIPTION
    Scans DAO files to find methods that call Helper_Database_StoredProcedure
    but don't have MySqlConnection/MySqlTransaction parameters.
    
    Scans test files to find DAO method calls that don't pass connection/transaction.
    
    Outputs detailed JSON files for each DAO/test that can be used by the fix script.

.PARAMETER OutputPath
    Path where JSON analysis files will be saved. Defaults to Database\TransactionSupportAnalysis\

.EXAMPLE
    .\1-Analyze-TransactionSupport.ps1
    Analyze all files and create JSON reports

.EXAMPLE
    .\1-Analyze-TransactionSupport.ps1 -Verbose
    Show detailed analysis information
#>

[CmdletBinding()]
param(
    [string]$OutputPath = "TransactionSupportAnalysis"
)

$ErrorActionPreference = "Stop"

# Determine repo root
$scriptRoot = $PSScriptRoot
if ([string]::IsNullOrEmpty($scriptRoot)) {
    $scriptRoot = Get-Location
}
$repoRoot = Split-Path $scriptRoot -Parent

# Create output directory
$analysisDir = Join-Path $scriptRoot $OutputPath
if (-not (Test-Path $analysisDir)) {
    New-Item -ItemType Directory -Path $analysisDir -Force | Out-Null
}

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  Transaction Support Analysis" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Phase 1: Analyze DAO Files
# ============================================================================

Write-Host "Phase 1: Analyzing DAO Files..." -ForegroundColor Yellow
Write-Host ""

$dataDir = Join-Path $repoRoot "Data"
$daoFiles = Get-ChildItem -Path $dataDir -Filter "Dao_*.cs" | Where-Object { $_.Name -notlike "*_Tests.cs" }

$daoAnalysis = @{}

foreach ($daoFile in $daoFiles) {
    $daoName = $daoFile.BaseName
    Write-Verbose "Analyzing $daoName..."
    
    $content = Get-Content $daoFile.FullName -Raw
    $lines = Get-Content $daoFile.FullName
    
    # Find all public/internal methods
    $methodPattern = '^\s*(public|internal)\s+(static\s+)?async\s+Task(<[^>]+>)?\s+(\w+)\s*\('
    $methodMatches = [regex]::Matches($content, $methodPattern, [System.Text.RegularExpressions.RegexOptions]::Multiline)
    
    $methods = @()
    
    foreach ($match in $methodMatches) {
        $methodName = $match.Groups[4].Value
        $lineNumber = ($content.Substring(0, $match.Index) -split "`n").Count
        
        # Get the full method signature (may span multiple lines)
        $sigStartLine = $lineNumber - 1
        $sigEndLine = $sigStartLine
        $bracketCount = 0
        $foundOpenParen = $false
        
        for ($i = $sigStartLine; $i -lt $lines.Count; $i++) {
            $line = $lines[$i]
            foreach ($char in $line.ToCharArray()) {
                if ($char -eq '(') {
                    $foundOpenParen = $true
                    $bracketCount++
                } elseif ($char -eq ')') {
                    $bracketCount--
                }
            }
            
            if ($foundOpenParen -and $bracketCount -eq 0) {
                $sigEndLine = $i
                break
            }
        }
        
        $signature = ($lines[$sigStartLine..$sigEndLine] -join "`n").Trim()
        
        # Check if method already has connection/transaction parameters
        $hasConnectionParam = $signature -match 'MySqlConnection\??\s+connection\s*='
        $hasTransactionParam = $signature -match 'MySqlTransaction\??\s+transaction\s*='
        
        # Check if method calls Helper_Database_StoredProcedure
        $methodBodyStart = $sigEndLine + 1
        $methodBodyEnd = $methodBodyStart + 100  # Look ahead 100 lines (should be enough)
        if ($methodBodyEnd -gt $lines.Count) { $methodBodyEnd = $lines.Count - 1 }
        
        $methodBody = $lines[$methodBodyStart..$methodBodyEnd] -join "`n"
        $callsHelper = $methodBody -match 'Helper_Database_StoredProcedure\.(Execute\w+Async)'
        
        # Find specific helper calls
        $helperCalls = @()
        $helperCallPattern = 'await\s+Helper_Database_StoredProcedure\.(Execute\w+Async)\s*\('
        $helperMatches = [regex]::Matches($methodBody, $helperCallPattern)
        
        foreach ($helperMatch in $helperMatches) {
            $helperMethodName = $helperMatch.Groups[1].Value
            $callLineOffset = ($methodBody.Substring(0, $helperMatch.Index) -split "`n").Count
            $callLineNumber = $methodBodyStart + $callLineOffset
            
            # Extract the full helper call (may span multiple lines)
            $callText = ""
            $parenCount = 0
            $foundStart = $false
            
            for ($i = $callLineNumber; $i -lt $lines.Count -and $i -lt $callLineNumber + 20; $i++) {
                $line = $lines[$i]
                if (-not $foundStart) {
                    if ($line -match 'Helper_Database_StoredProcedure') {
                        $foundStart = $true
                        $callText = $line.Trim()
                    }
                } else {
                    $callText += " " + $line.Trim()
                }
                
                foreach ($char in $line.ToCharArray()) {
                    if ($char -eq '(') { $parenCount++ }
                    elseif ($char -eq ')') { $parenCount-- }
                }
                
                if ($foundStart -and $parenCount -eq 0) {
                    break
                }
            }
            
            # Check if this call already passes connection/transaction
            $hasConnectionArg = $callText -match 'connection\s*:\s*connection'
            $hasTransactionArg = $callText -match 'transaction\s*:\s*transaction'
            
            $helperCalls += @{
                MethodName = $helperMethodName
                LineNumber = $callLineNumber + 1  # 1-indexed for display
                CallText = $callText
                NeedsConnectionArg = -not $hasConnectionArg
                NeedsTransactionArg = -not $hasTransactionArg
            }
        }
        
        # Determine if method needs updating
        $needsUpdate = $callsHelper -and (-not $hasConnectionParam -or -not $hasTransactionParam)
        
        if ($needsUpdate -or $helperCalls.Count -gt 0) {
            $methods += @{
                Name = $methodName
                LineNumber = $lineNumber
                Signature = $signature
                HasConnectionParam = $hasConnectionParam
                HasTransactionParam = $hasTransactionParam
                CallsHelper = $callsHelper
                NeedsUpdate = $needsUpdate
                HelperCalls = $helperCalls
            }
        }
    }
    
    if ($methods.Count -gt 0) {
        $daoAnalysis[$daoName] = @{
            FilePath = $daoFile.FullName -replace [regex]::Escape($repoRoot), ""
            MethodsNeedingUpdate = $methods.Where({ $_.NeedsUpdate })
            MethodsWithHelperCalls = $methods
            TotalMethods = $methods.Count
            NeedsUpdate = ($methods.Where({ $_.NeedsUpdate }).Count -gt 0)
        }
        
        Write-Host "  ✓ $daoName`: Found $($methods.Count) methods, $($methods.Where({ $_.NeedsUpdate }).Count) need updating" -ForegroundColor Green
    } else {
        Write-Verbose "  - $daoName`: No methods need updating"
    }
}

# Save DAO analysis
$daoAnalysisPath = Join-Path $analysisDir "dao-analysis.json"
$daoAnalysis | ConvertTo-Json -Depth 10 | Set-Content $daoAnalysisPath
Write-Host ""
Write-Host "✓ DAO analysis saved to: $daoAnalysisPath" -ForegroundColor Cyan

# ============================================================================
# Phase 2: Analyze Test Files
# ============================================================================

Write-Host ""
Write-Host "Phase 2: Analyzing Test Files..." -ForegroundColor Yellow
Write-Host ""

$testsDir = Join-Path $repoRoot "Tests\Integration"
$testFiles = Get-ChildItem -Path $testsDir -Filter "*_Tests.cs" -ErrorAction SilentlyContinue

if (-not $testFiles) {
    Write-Warning "No test files found in $testsDir"
    $testFiles = @()
}

$testAnalysis = @{}

foreach ($testFile in $testFiles) {
    $testName = $testFile.BaseName
    Write-Verbose "Analyzing $testName..."
    
    $content = Get-Content $testFile.FullName -Raw
    $lines = Get-Content $testFile.FullName
    
    # Find all DAO method calls
    $daoCallPattern = 'await\s+(Dao_\w+)\.(\w+)\s*\('
    $daoCallMatches = [regex]::Matches($content, $daoCallPattern)
    
    $calls = @()
    
    foreach ($match in $daoCallMatches) {
        $daoName = $match.Groups[1].Value
        $methodName = $match.Groups[2].Value
        $lineNumber = ($content.Substring(0, $match.Index) -split "`n").Count
        
        # Get the full call (may span multiple lines)
        $callStartLine = $lineNumber - 1
        $callText = ""
        $parenCount = 0
        $foundStart = $false
        
        for ($i = $callStartLine; $i -lt $lines.Count -and $i -lt $callStartLine + 30; $i++) {
            $line = $lines[$i]
            if (-not $foundStart) {
                if ($line -match $daoName) {
                    $foundStart = $true
                    $callText = $line.Trim()
                }
            } else {
                $callText += " " + $line.Trim()
            }
            
            foreach ($char in $line.ToCharArray()) {
                if ($char -eq '(') { $parenCount++ }
                elseif ($char -eq ')') { $parenCount-- }
            }
            
            if ($foundStart -and $parenCount -eq 0) {
                break
            }
        }
        
        # Check if call already passes connection/transaction
        $hasConnectionArg = $callText -match 'connection\s*:\s*GetTestConnection\(\)'
        $hasTransactionArg = $callText -match 'transaction\s*:\s*GetTestTransaction\(\)'
        
        $needsUpdate = -not ($hasConnectionArg -and $hasTransactionArg)
        
        if ($needsUpdate) {
            $calls += @{
                DaoClass = $daoName
                MethodName = $methodName
                LineNumber = $lineNumber
                CallText = $callText
                HasConnectionArg = $hasConnectionArg
                HasTransactionArg = $hasTransactionArg
                NeedsUpdate = $needsUpdate
            }
        }
    }
    
    if ($calls.Count -gt 0) {
        $testAnalysis[$testName] = @{
            FilePath = $testFile.FullName -replace [regex]::Escape($repoRoot), ""
            CallsNeedingUpdate = $calls
            TotalCalls = $calls.Count
            NeedsUpdate = $true
        }
        
        Write-Host "  ✓ $testName`: Found $($calls.Count) DAO calls needing update" -ForegroundColor Green
    } else {
        Write-Verbose "  - $testName`: No calls need updating"
    }
}

# Save test analysis
$testAnalysisPath = Join-Path $analysisDir "test-analysis.json"
$testAnalysis | ConvertTo-Json -Depth 10 | Set-Content $testAnalysisPath
Write-Host ""
Write-Host "✓ Test analysis saved to: $testAnalysisPath" -ForegroundColor Cyan

# ============================================================================
# Summary Report
# ============================================================================

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  Analysis Summary" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

$totalDaoMethods = ($daoAnalysis.Values | ForEach-Object { $_.MethodsNeedingUpdate.Count } | Measure-Object -Sum).Sum
$totalTestCalls = ($testAnalysis.Values | ForEach-Object { $_.CallsNeedingUpdate.Count } | Measure-Object -Sum).Sum

Write-Host "DAO Files:" -ForegroundColor Yellow
Write-Host "  Files analyzed: $($daoFiles.Count)" -ForegroundColor White
Write-Host "  Files needing updates: $($daoAnalysis.Count)" -ForegroundColor White
Write-Host "  Methods needing transaction params: $totalDaoMethods" -ForegroundColor White
Write-Host ""

Write-Host "Test Files:" -ForegroundColor Yellow
Write-Host "  Files analyzed: $($testFiles.Count)" -ForegroundColor White
Write-Host "  Files needing updates: $($testAnalysis.Count)" -ForegroundColor White
Write-Host "  DAO calls needing connection/transaction: $totalTestCalls" -ForegroundColor White
Write-Host ""

Write-Host "Output Files:" -ForegroundColor Yellow
Write-Host "  $daoAnalysisPath" -ForegroundColor White
Write-Host "  $testAnalysisPath" -ForegroundColor White
Write-Host ""

Write-Host "✓ Analysis complete! Run 2-Apply-TransactionSupport.ps1 to apply fixes." -ForegroundColor Green
Write-Host ""
