<#
.SYNOPSIS
    Applies transaction support fixes based on analysis JSON files.

.DESCRIPTION
    Reads the JSON files created by 1-Analyze-TransactionSupport.ps1 and applies
    the necessary changes to add MySqlConnection/MySqlTransaction parameters to
    DAO methods and update test calls.

.PARAMETER AnalysisPath
    Path where JSON analysis files are located. Defaults to Database\TransactionSupportAnalysis\

.PARAMETER WhatIf
    Shows what changes would be made without actually making them.

.EXAMPLE
    .\2-Apply-TransactionSupport.ps1 -WhatIf
    Preview changes without applying them

.EXAMPLE
    .\2-Apply-TransactionSupport.ps1
    Apply all fixes

.EXAMPLE
    .\2-Apply-TransactionSupport.ps1 -Verbose
    Apply fixes with detailed output
#>

[CmdletBinding(SupportsShouldProcess)]
param(
    [string]$AnalysisPath = "TransactionSupportAnalysis"
)

$ErrorActionPreference = "Stop"

# Determine repo root
$scriptRoot = $PSScriptRoot
if ([string]::IsNullOrEmpty($scriptRoot)) {
    $scriptRoot = Get-Location
}
$repoRoot = Split-Path $scriptRoot -Parent

$analysisDir = Join-Path $scriptRoot $AnalysisPath

# Check for analysis files
$daoAnalysisPath = Join-Path $analysisDir "dao-analysis.json"
$testAnalysisPath = Join-Path $analysisDir "test-analysis.json"

if (-not (Test-Path $daoAnalysisPath)) {
    Write-Error "DAO analysis file not found: $daoAnalysisPath`nRun 1-Analyze-TransactionSupport.ps1 first."
}

if (-not (Test-Path $testAnalysisPath)) {
    Write-Error "Test analysis file not found: $testAnalysisPath`nRun 1-Analyze-TransactionSupport.ps1 first."
}

Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  Applying Transaction Support Fixes" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

if ($WhatIf) {
    Write-Host "⚠️  WHAT-IF MODE: No changes will be made" -ForegroundColor Yellow
    Write-Host ""
}

# Load analysis data
$daoAnalysis = Get-Content $daoAnalysisPath -Raw | ConvertFrom-Json -AsHashtable
$testAnalysis = Get-Content $testAnalysisPath -Raw | ConvertFrom-Json -AsHashtable

# ============================================================================
# Helper Functions
# ============================================================================

function Add-TransactionParametersToSignature {
    param(
        [string]$Signature
    )
    
    # Remove trailing ) and any whitespace
    $sig = $Signature.TrimEnd()
    if ($sig.EndsWith(')')) {
        $sig = $sig.Substring(0, $sig.Length - 1).TrimEnd()
    }
    
    # Add comma if there are already parameters
    if (-not $sig.EndsWith('(')) {
        $sig += ","
    }
    
    # Add the new parameters
    $sig += "`n        MySqlConnection? connection = null,`n        MySqlTransaction? transaction = null)"
    
    return $sig
}

function Add-TransactionArgumentsToHelperCall {
    param(
        [string]$CallText
    )
    
    # Find the closing parenthesis for the helper call
    $lines = $CallText -split "`n"
    $result = ""
    $foundProgressHelper = $false
    
    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]
        
        # Check if this line has the closing ) or progressHelper: null
        if ($line -match '\bprogressHelper:\s*null\b' -or $line -match '\bnull\s*//.*progress') {
            $foundProgressHelper = $true
            # Replace null with null, and add connection/transaction
            $line = $line -replace '(progressHelper:\s*)null', '$1null,'
            if ($line -notmatch 'connection:') {
                $line = $line -replace '(progressHelper:\s*null),?', "`$1,`n                connection: connection,`n                transaction: transaction"
            }
        }
        elseif ($line -match '\);?\s*$' -and -not $foundProgressHelper) {
            # No progressHelper found, add all parameters before closing )
            $line = $line -replace '\);?', ",`n                progressHelper: null,`n                connection: connection,`n                transaction: transaction`n            );"
        }
        
        $result += $line
        if ($i -lt $lines.Count - 1) {
            $result += "`n"
        }
    }
    
    return $result
}

function Add-TransactionArgumentsToDaoCall {
    param(
        [string]$CallText
    )
    
    # Find the closing parenthesis
    $result = $CallText.TrimEnd()
    
    # Remove trailing semicolon if present
    $hasSemicolon = $result.EndsWith(';')
    if ($hasSemicolon) {
        $result = $result.Substring(0, $result.Length - 1).TrimEnd()
    }
    
    # Remove trailing )
    if ($result.EndsWith(')')) {
        $result = $result.Substring(0, $result.Length - 1).TrimEnd()
    }
    
    # Add comma if there are already parameters
    if (-not $result.EndsWith('(')) {
        $result += ", "
    }
    
    # Add connection and transaction arguments
    $result += "connection: GetTestConnection(), transaction: GetTestTransaction())"
    
    # Re-add semicolon if it was there
    if ($hasSemicolon) {
        $result += ";"
    }
    
    return $result
}

# ============================================================================
# Phase 1: Update DAO Files
# ============================================================================

Write-Host "Phase 1: Updating DAO Files..." -ForegroundColor Yellow
Write-Host ""

$daoUpdates = 0
$daoFailures = 0

foreach ($daoName in $daoAnalysis.Keys) {
    $daoInfo = $daoAnalysis[$daoName]
    
    if (-not $daoInfo.NeedsUpdate) {
        Write-Verbose "  Skipping $daoName (no updates needed)"
        continue
    }
    
    $filePath = Join-Path $repoRoot $daoInfo.FilePath.TrimStart('\', '/')
    
    if (-not (Test-Path $filePath)) {
        Write-Warning "  ✗ File not found: $filePath"
        $daoFailures++
        continue
    }
    
    Write-Host "  Processing $daoName..." -ForegroundColor Cyan
    
    $content = Get-Content $filePath -Raw
    $originalContent = $content
    $changesMade = 0
    
    # Update each method that needs it
    foreach ($method in $daoInfo.MethodsNeedingUpdate) {
        Write-Verbose "    Updating method: $($method.Name)"
        
        # Update method signature if it doesn't have the parameters
        if (-not $method.HasConnectionParam -or -not $method.HasTransactionParam) {
            $oldSig = $method.Signature
            $newSig = Add-TransactionParametersToSignature -Signature $oldSig
            
            if ($oldSig -ne $newSig) {
                $content = $content -replace [regex]::Escape($oldSig), $newSig
                $changesMade++
                Write-Verbose "      ✓ Updated method signature"
            }
        }
        
        # Update helper calls within this method
        foreach ($helperCall in $method.HelperCalls) {
            if ($helperCall.NeedsConnectionArg -or $helperCall.NeedsTransactionArg) {
                $oldCall = $helperCall.CallText
                $newCall = Add-TransactionArgumentsToHelperCall -CallText $oldCall
                
                if ($oldCall -ne $newCall) {
                    # Use literal string replacement to avoid regex issues
                    $content = $content.Replace($oldCall, $newCall)
                    $changesMade++
                    Write-Verbose "      ✓ Updated helper call at line $($helperCall.LineNumber)"
                }
            }
        }
    }
    
    if ($changesMade -eq 0) {
        Write-Host "    No changes needed" -ForegroundColor Gray
        continue
    }
    
    if ($PSCmdlet.ShouldProcess($filePath, "Update $changesMade location(s) in $daoName")) {
        Set-Content -Path $filePath -Value $content -NoNewline
        Write-Host "    ✓ Updated $changesMade location(s)" -ForegroundColor Green
        $daoUpdates++
    } else {
        Write-Host "    [WHAT-IF] Would update $changesMade location(s)" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "  DAO Updates: $daoUpdates completed, $daoFailures failed" -ForegroundColor $(if ($daoFailures -eq 0) { "Green" } else { "Yellow" })

# ============================================================================
# Phase 2: Update Test Files
# ============================================================================

Write-Host ""
Write-Host "Phase 2: Updating Test Files..." -ForegroundColor Yellow
Write-Host ""

$testUpdates = 0
$testFailures = 0

foreach ($testName in $testAnalysis.Keys) {
    $testInfo = $testAnalysis[$testName]
    
    if (-not $testInfo.NeedsUpdate) {
        Write-Verbose "  Skipping $testName (no updates needed)"
        continue
    }
    
    $filePath = Join-Path $repoRoot $testInfo.FilePath.TrimStart('\', '/')
    
    if (-not (Test-Path $filePath)) {
        Write-Warning "  ✗ File not found: $filePath"
        $testFailures++
        continue
    }
    
    Write-Host "  Processing $testName..." -ForegroundColor Cyan
    
    $content = Get-Content $filePath -Raw
    $originalContent = $content
    $changesMade = 0
    
    # Update each DAO call that needs it
    foreach ($call in $testInfo.CallsNeedingUpdate) {
        Write-Verbose "    Updating call to $($call.DaoClass).$($call.MethodName) at line $($call.LineNumber)"
        
        $oldCall = $call.CallText
        $newCall = Add-TransactionArgumentsToDaoCall -CallText $oldCall
        
        if ($oldCall -ne $newCall) {
            # Use literal string replacement to avoid regex issues
            $content = $content.Replace($oldCall, $newCall)
            $changesMade++
            Write-Verbose "      ✓ Updated DAO call"
        }
    }
    
    if ($changesMade -eq 0) {
        Write-Host "    No changes needed" -ForegroundColor Gray
        continue
    }
    
    if ($PSCmdlet.ShouldProcess($filePath, "Update $changesMade DAO call(s) in $testName")) {
        Set-Content -Path $filePath -Value $content -NoNewline
        Write-Host "    ✓ Updated $changesMade DAO call(s)" -ForegroundColor Green
        $testUpdates++
    } else {
        Write-Host "    [WHAT-IF] Would update $changesMade DAO call(s)" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "  Test Updates: $testUpdates completed, $testFailures failed" -ForegroundColor $(if ($testFailures -eq 0) { "Green" } else { "Yellow" })

# ============================================================================
# Summary
# ============================================================================

Write-Host ""
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host "  Summary" -ForegroundColor Cyan
Write-Host "═══════════════════════════════════════════════════════════════" -ForegroundColor Cyan
Write-Host ""

if ($WhatIf) {
    Write-Host "What-If Mode Summary:" -ForegroundColor Yellow
    Write-Host "  Would update $daoUpdates DAO files" -ForegroundColor White
    Write-Host "  Would update $testUpdates test files" -ForegroundColor White
} else {
    Write-Host "Updates Applied:" -ForegroundColor Green
    Write-Host "  ✓ Updated $daoUpdates DAO files" -ForegroundColor White
    Write-Host "  ✓ Updated $testUpdates test files" -ForegroundColor White
    
    if ($daoFailures -gt 0 -or $testFailures -gt 0) {
        Write-Host ""
        Write-Host "Failures:" -ForegroundColor Red
        if ($daoFailures -gt 0) {
            Write-Host "  ✗ $daoFailures DAO files failed" -ForegroundColor White
        }
        if ($testFailures -gt 0) {
            Write-Host "  ✗ $testFailures test files failed" -ForegroundColor White
        }
    }
}

Write-Host ""
Write-Host "Next Steps:" -ForegroundColor Yellow
Write-Host "  1. Run: dotnet build" -ForegroundColor White
Write-Host "  2. Fix any compilation errors" -ForegroundColor White
Write-Host "  3. Run: dotnet test" -ForegroundColor White
Write-Host "  4. Verify all 136 tests pass" -ForegroundColor White
Write-Host ""

if (-not $WhatIf) {
    Write-Host "✓ All fixes applied! Run 'dotnet build' to verify." -ForegroundColor Green
    Write-Host ""
}
