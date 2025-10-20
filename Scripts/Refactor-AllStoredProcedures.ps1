# Systematic refactoring of all stored procedures to comply with status code standards
# This script will fix all 64 non-compliant procedures automatically

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$sqlDir = Join-Path (Split-Path -Parent $scriptDir) "Database\CurrentStoredProcedures"

# Import the verification report to know which procedures need fixing
$report = Import-Csv (Join-Path $sqlDir "VERIFICATION_REPORT.csv")
$proceduresToFix = $report | Where-Object { $_.Critical -eq $true -or $_.Issues -ne "" }

Write-Host "`n=== Starting Systematic Refactoring ===" -ForegroundColor Cyan
Write-Host "Total procedures to refactor: $($proceduresToFix.Count)`n" -ForegroundColor Yellow

$fixed = 0
$errors = 0
$backupDir = Join-Path $sqlDir "Backup_Before_Refactor"

# Create backup directory
if (-not (Test-Path $backupDir)) {
    New-Item -ItemType Directory -Path $backupDir | Out-Null
    Write-Host "Created backup directory: $backupDir`n" -ForegroundColor Cyan
}

foreach ($proc in $proceduresToFix) {
    $procName = $proc.Procedure
    $sqlFile = Join-Path $sqlDir "$procName.sql"
    
    if (-not (Test-Path $sqlFile)) {
        Write-Host "[SKIP] $procName - File not found" -ForegroundColor Gray
        continue
    }
    
    Write-Host "Processing: $procName" -ForegroundColor White
    
    try {
        # Backup original file
        $backupFile = Join-Path $backupDir "$procName.sql.bak"
        Copy-Item $sqlFile $backupFile -Force
        
        # Read current content
        $content = Get-Content $sqlFile -Raw
        $originalContent = $content
        
        # Track what changes we make
        $changes = @()
        
        # Fix 1: Add missing OUT parameters if needed
        if ($proc.Issues -match "Missing OUT p_Status parameter" -or $proc.Issues -match "Missing OUT p_ErrorMsg parameter") {
            # Find the parameter list
            if ($content -match "CREATE PROCEDURE ``$procName``\s*\(((?:[^)]|\n)*?)\)") {
                $paramBlock = $matches[1]
                
                # Check if we need to add parameters
                $needsStatus = $paramBlock -notmatch "OUT p_Status"
                $needsError = $paramBlock -notmatch "OUT p_ErrorMsg"
                
                if ($needsStatus -or $needsError) {
                    $newParams = $paramBlock.TrimEnd()
                    
                    # Add comma if there are existing parameters
                    if ($newParams.Trim() -ne "" -and -not $newParams.EndsWith(",")) {
                        $newParams += ","
                    }
                    
                    if ($needsStatus) {
                        $newParams += "`n    OUT p_Status INT"
                        if ($needsError) {
                            $newParams += ","
                        }
                    }
                    
                    if ($needsError) {
                        $newParams += "`n    OUT p_ErrorMsg VARCHAR(500)"
                    }
                    
                    $content = $content -replace [regex]::Escape($paramBlock), $newParams
                    $changes += "Added OUT parameters"
                }
            }
        }
        
        # Fix 2: Add EXIT HANDLER if missing
        if ($proc.Issues -match "Missing EXIT HANDLER FOR SQLEXCEPTION") {
            # Add EXIT HANDLER after BEGIN
            if ($content -match "BEGIN\s*\n") {
                $exitHandler = @"

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while executing $procName';
    END;
"@
                $content = $content -replace "(BEGIN\s*\n)", "`$1$exitHandler`n"
                $changes += "Added EXIT HANDLER"
            }
        }
        
        # Fix 3: Fix EXIT HANDLER that doesn't set p_Status = -1
        if ($proc.Issues -match "EXIT HANDLER doesn't set p_Status = -1") {
            # Update existing EXIT HANDLER to set p_Status = -1
            if ($content -match "DECLARE EXIT HANDLER FOR SQLEXCEPTION\s*BEGIN([^E]+?)END;") {
                $handlerBody = $matches[1]
                if ($handlerBody -notmatch "SET p_Status = -1") {
                    $newHandlerBody = "`n        SET p_Status = -1;`n        SET p_ErrorMsg = 'Database error occurred while executing $procName';$handlerBody"
                    $content = $content -replace "(DECLARE EXIT HANDLER FOR SQLEXCEPTION\s*BEGIN)[^E]+?(END;)", "`${1}$newHandlerBody`n    `$2"
                    $changes += "Fixed EXIT HANDLER status"
                }
            }
        }
        
        # Fix 4: Add row count variables if needed
        $needsCountVar = $false
        if ($proc.Issues -match "SELECT query without FOUND_ROWS\(\) check" -or 
            $proc.Issues -match "INSERT/UPDATE/DELETE without ROW_COUNT\(\) check") {
            
            # Add v_Count variable after EXIT HANDLER or after BEGIN
            if ($content -notmatch "DECLARE v_Count INT") {
                if ($content -match "END;\s*\n\s*\n") {
                    $content = $content -replace "(END;\s*\n\s*\n)", "`$1    DECLARE v_Count INT DEFAULT 0;`n`n"
                    $changes += "Added v_Count variable"
                    $needsCountVar = $true
                }
            }
        }
        
        # Fix 5: Add FOUND_ROWS() check for SELECT queries
        if ($proc.Issues -match "SELECT query without FOUND_ROWS\(\) check") {
            # Find SELECT statements that return data (not INTO)
            if ($content -match "SELECT .+ FROM .+;" -and $content -notmatch "SELECT .+ INTO") {
                # Add FOUND_ROWS check after the SELECT
                if ($content -match "(SELECT[^;]+;)(\s*END)" -and $content -notmatch "FOUND_ROWS\(\)") {
                    $checkCode = @"
`n
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No records found';
        END IF;
"@
                    $content = $content -replace "(SELECT[^;]+;)(\s*END)", "`$1$checkCode`$2"
                    $changes += "Added FOUND_ROWS() check"
                }
            }
        }
        
        # Fix 6: Add ROW_COUNT() check for INSERT/UPDATE/DELETE
        if ($proc.Issues -match "INSERT/UPDATE/DELETE without ROW_COUNT\(\) check") {
            # Find INSERT/UPDATE/DELETE statements
            if ($content -match "(INSERT|UPDATE|DELETE) .+;" -and $content -notmatch "ROW_COUNT\(\)") {
                # Add ROW_COUNT check after the statement
                if ($content -match "((INSERT|UPDATE|DELETE)[^;]+;)(\s*END)" -and $content -notmatch "ROW_COUNT\(\)") {
                    $checkCode = @"
`n
        SET v_Count = ROW_COUNT();
        
        IF v_Count > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Successfully affected ', v_Count, ' row(s)');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = 'No rows were affected';
        END IF;
"@
                    $content = $content -replace "((INSERT|UPDATE|DELETE)[^;]+;)(\s*END)", "`$1$checkCode`$3"
                    $changes += "Added ROW_COUNT() check"
                }
            }
        }
        
        # Fix 7: Remove unconditional SET p_Status = 0 at end (CRITICAL)
        if ($proc.Issues -match "CRITICAL: Unconditional SET p_Status = 0 at end") {
            # Remove the unconditional status setting
            $content = $content -replace "SET p_Status = 0;\s*(SET p_ErrorMsg[^;]+;)?\s*END IF;\s*END\$\$", "END IF;`nEND`$`$"
            $changes += "CRITICAL: Removed unconditional p_Status = 0"
        }
        
        # Fix 8: Add SET p_Status = 1 for success if completely missing
        if ($proc.Issues -match "Missing SET p_Status = 1 for success" -and $content -notmatch "SET p_Status = 1") {
            # This should have been handled by FOUND_ROWS/ROW_COUNT checks above
            # But if still missing, add a basic success status before END
            if ($content -notmatch "SET p_Status =") {
                $content = $content -replace "(\s*)END\$\$", "`n    SET p_Status = 1;`n    SET p_ErrorMsg = NULL;`$1END`$`$"
                $changes += "Added basic success status"
            }
        }
        
        # Only write if changes were made
        if ($changes.Count -gt 0 -and $content -ne $originalContent) {
            Set-Content -Path $sqlFile -Value $content -NoNewline
            Write-Host "  ✓ Fixed: $($changes -join ', ')" -ForegroundColor Green
            $fixed++
        } else {
            Write-Host "  - No changes needed or failed to apply fixes" -ForegroundColor Gray
        }
        
    } catch {
        Write-Host "  ✗ ERROR: $_" -ForegroundColor Red
        $errors++
        
        # Restore from backup on error
        if (Test-Path $backupFile) {
            Copy-Item $backupFile $sqlFile -Force
            Write-Host "  ↶ Restored from backup" -ForegroundColor Yellow
        }
    }
}

Write-Host "`n=== Refactoring Complete ===" -ForegroundColor Cyan
Write-Host "Successfully fixed: $fixed" -ForegroundColor Green
Write-Host "Errors: $errors" -ForegroundColor $(if ($errors -gt 0) { "Red" } else { "Green" })
Write-Host "Backups saved to: $backupDir" -ForegroundColor Gray

Write-Host "`n⚠️  IMPORTANT: Run Verify-StatusCodeStandards.ps1 again to check results!" -ForegroundColor Yellow
