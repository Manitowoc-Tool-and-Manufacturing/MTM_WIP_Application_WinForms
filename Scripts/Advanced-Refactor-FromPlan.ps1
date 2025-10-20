# Advanced refactoring script that uses the MANUAL_FIX_PLAN.csv to fix all remaining issues
# This handles procedures that need OUT parameter additions and complex fixes

$fixPlan = Import-Csv "Database\CurrentStoredProcedures\MANUAL_FIX_PLAN.csv"
$sqlDir = "Database\CurrentStoredProcedures"
$backupDir = Join-Path $sqlDir "Backup_Before_Manual_Refactor"

# Create backup directory
if (-not (Test-Path $backupDir)) {
    New-Item -ItemType Directory -Path $backupDir | Out-Null
}

Write-Host "`n=== Advanced Systematic Refactoring ===" -ForegroundColor Cyan

# Process CRITICAL procedures first
$critical = $fixPlan | Where-Object { $_.Priority -eq "CRITICAL" }
Write-Host "`n🔥 Fixing $($critical.Count) CRITICAL procedures..." -ForegroundColor Red

$fixedCount = 0
$errorCount = 0

foreach ($item in $critical) {
    $procName = $item.Procedure
    $sqlFile = Join-Path $sqlDir "$procName.sql"
    
    if (-not (Test-Path $sqlFile)) {
        Write-Host "[SKIP] $procName - File not found" -ForegroundColor Gray
        continue
    }
    
    Write-Host "Processing: $procName" -ForegroundColor White
    
    try {
        # Backup
        Copy-Item $sqlFile (Join-Path $backupDir "$procName.sql.bak") -Force
        
        $content = Get-Content $sqlFile -Raw
        $originalContent = $content
        $changes = @()
        
        # Fix 1: Add OUT parameters if missing (must modify signature)
        if ($item.Issues -match "Missing OUT p_Status parameter") {
            # Find CREATE PROCEDURE line and add OUT parameters
            if ($content -match "CREATE PROCEDURE ``$procName``\s*\(([^)]*)\)") {
                $oldSignature = $matches[0]
                $params = $matches[1].Trim()
                
                # Build new parameter list
                if ($params -eq "") {
                    $newParams = "`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)`n"
                } else {
                    # Add comma if needed
                    if (-not $params.EndsWith(",")) {
                        $params += ","
                    }
                    $newParams = "$params`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)"
                }
                
                $newSignature = "CREATE PROCEDURE ``$procName``(`n    $newParams`n)"
                $content = $content.Replace($oldSignature, $newSignature)
                $changes += "Added OUT parameters"
            } elseif ($content -match "CREATE PROCEDURE ``$procName``\(\)") {
                # No parameters at all
                $content = $content -replace "(CREATE PROCEDURE ``$procName``)\(\)", "`$1(`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)`n)"
                $changes += "Added OUT parameters (was empty)"
            }
        }
        
        # Fix 2: Remove unconditional SET p_Status = 0 at end
        if ($item.Issues -match "Unconditional SET p_Status = 0 at end") {
            # Remove the bad pattern
            $content = $content -replace "\s*SET p_Status = 0;\s*(\r?\n\s*SET p_ErrorMsg[^;]+;)?(\r?\n)+END\$\$", "`nEND`$`$"
            $content = $content -replace "\s*SET p_Status = 0;(\r?\n)+END\$\$", "`nEND`$`$"
            $changes += "CRITICAL: Removed unconditional p_Status = 0"
        }
        
        # Fix 3: Add FOUND_ROWS check if SELECT query exists
        if ($content -match "SELECT .+ FROM" -and $content -notmatch "FOUND_ROWS\(\)") {
            # Add after the last SELECT before END
            if ($content -match "(SELECT[^;]+FROM[^;]+;)(\s*)END\$\$") {
                $checkCode = @"
`n
    SELECT FOUND_ROWS() INTO @rowCount;
    
    IF @rowCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No records found';
    END IF;
"@
                $content = $content -replace "(SELECT[^;]+FROM[^;]+;)(\s*)END\$\$", "`$1$checkCode`$2END`$`$"
                
                # Add variable declaration
                if ($content -notmatch "DECLARE @rowCount") {
                    $content = $content -replace "(BEGIN\s*\r?\n)", "`$1    DECLARE @rowCount INT DEFAULT 0;`n`n"
                }
                $changes += "Added FOUND_ROWS() check"
            }
        }
        
        # Write if changed
        if ($changes.Count -gt 0 -and $content -ne $originalContent) {
            Set-Content -Path $sqlFile -Value $content -NoNewline
            Write-Host "  ✓ Fixed: $($changes -join ', ')" -ForegroundColor Green
            $fixedCount++
        } else {
            Write-Host "  - No changes applied" -ForegroundColor Gray
        }
        
    } catch {
        Write-Host "  ✗ ERROR: $_" -ForegroundColor Red
        $errorCount++
        
        $backupFile = Join-Path $backupDir "$procName.sql.bak"
        if (Test-Path $backupFile) {
            Copy-Item $backupFile $sqlFile -Force
            Write-Host "  ↶ Restored from backup" -ForegroundColor Yellow
        }
    }
}

# Process Normal priority procedures
$normal = $fixPlan | Where-Object { $_.Priority -eq "Normal" }
Write-Host "`n⚠️  Fixing $($normal.Count) Normal priority procedures..." -ForegroundColor Yellow

foreach ($item in $normal) {
    $procName = $item.Procedure
    $sqlFile = Join-Path $sqlDir "$procName.sql"
    
    if (-not (Test-Path $sqlFile)) { continue }
    
    Write-Host "Processing: $procName" -ForegroundColor White
    
    try {
        Copy-Item $sqlFile (Join-Path $backupDir "$procName.sql.bak") -Force
        
        $content = Get-Content $sqlFile -Raw
        $originalContent = $content
        $changes = @()
        
        # Fix OUT parameters
        if ($item.Action -match "Add OUT parameters to signature") {
            if ($content -match "CREATE PROCEDURE ``$procName``\s*\(([^)]*)\)") {
                $oldSig = $matches[0]
                $params = $matches[1].Trim()
                
                if ($params -eq "") {
                    $newParams = "`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)`n"
                } else {
                    if (-not $params.EndsWith(",")) { $params += "," }
                    $newParams = "$params`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)"
                }
                
                $newSig = "CREATE PROCEDURE ``$procName``(`n    $newParams`n)"
                $content = $content.Replace($oldSig, $newSig)
                $changes += "Added OUT parameters"
            } elseif ($content -match "CREATE PROCEDURE ``$procName``\(\)") {
                $content = $content -replace "(CREATE PROCEDURE ``$procName``)\(\)", "`$1(`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)`n)"
                $changes += "Added OUT parameters (empty)"
            }
        }
        
        # Fix FOUND_ROWS
        if ($item.Action -match "Add FOUND_ROWS\(\) check" -and $content -notmatch "FOUND_ROWS\(\)") {
            if ($content -match "SELECT .+ FROM" -and $content -match "(SELECT[^;]+;)(\s*)END\$\$") {
                $checkCode = "`n`n    SELECT FOUND_ROWS() INTO @rowCount;`n    IF @rowCount > 0 THEN`n        SET p_Status = 1;`n        SET p_ErrorMsg = NULL;`n    ELSE`n        SET p_Status = 0;`n        SET p_ErrorMsg = 'No records found';`n    END IF;"
                $content = $content -replace "(SELECT[^;]+;)(\s*)END\$\$", "`$1$checkCode`$2END`$`$"
                
                if ($content -notmatch "DECLARE @rowCount") {
                    $content = $content -replace "(BEGIN\s*\r?\n)", "`$1    DECLARE @rowCount INT DEFAULT 0;`n`n"
                }
                $changes += "Added FOUND_ROWS()"
            }
        }
        
        # Fix ROW_COUNT
        if ($item.Action -match "Add ROW_COUNT\(\) check" -and $content -notmatch "ROW_COUNT\(\)") {
            if ($content -match "(INSERT|UPDATE|DELETE)" -and $content -match "((INSERT|UPDATE|DELETE)[^;]+;)(\s*)END\$\$") {
                $checkCode = "`n`n    SET @rowCount = ROW_COUNT();`n    IF @rowCount > 0 THEN`n        SET p_Status = 1;`n        SET p_ErrorMsg = CONCAT('Successfully affected ', @rowCount, ' row(s)');`n    ELSE`n        SET p_Status = 0;`n        SET p_ErrorMsg = 'No rows were affected';`n    END IF;"
                $content = $content -replace "((INSERT|UPDATE|DELETE)[^;]+;)(\s*)END\$\$", "`$1$checkCode`$3END`$`$"
                
                if ($content -notmatch "DECLARE @rowCount") {
                    $content = $content -replace "(BEGIN\s*\r?\n)", "`$1    DECLARE @rowCount INT DEFAULT 0;`n`n"
                }
                $changes += "Added ROW_COUNT()"
            }
        }
        
        # Fix success status
        if ($item.Action -match "Add SET p_Status = 1 for success" -and $content -notmatch "SET p_Status = 1") {
            # Add before END if no other status setting exists
            if ($content -notmatch "SET p_Status =") {
                $content = $content -replace "(\s*)END\$\$", "`n    SET p_Status = 1;`n    SET p_ErrorMsg = NULL;`$1END`$`$"
                $changes += "Added success status"
            }
        }
        
        if ($changes.Count -gt 0 -and $content -ne $originalContent) {
            Set-Content -Path $sqlFile -Value $content -NoNewline
            Write-Host "  ✓ Fixed: $($changes -join ', ')" -ForegroundColor Green
            $fixedCount++
        } else {
            Write-Host "  - No changes" -ForegroundColor Gray
        }
        
    } catch {
        Write-Host "  ✗ ERROR: $_" -ForegroundColor Red
        $errorCount++
        
        $backupFile = Join-Path $backupDir "$procName.sql.bak"
        if (Test-Path $backupFile) {
            Copy-Item $backupFile $sqlFile -Force
        }
    }
}

Write-Host "`n=== Advanced Refactoring Complete ===" -ForegroundColor Cyan
Write-Host "Successfully fixed: $fixedCount" -ForegroundColor Green
Write-Host "Errors: $errorCount" -ForegroundColor $(if ($errorCount -gt 0) { "Red" } else { "Green" })
Write-Host "Backups: $backupDir" -ForegroundColor Gray

Write-Host "`n⚠️  Run Verify-StatusCodeStandards.ps1 to check results!" -ForegroundColor Yellow
