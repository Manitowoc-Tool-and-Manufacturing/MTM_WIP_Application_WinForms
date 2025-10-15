# Verify all stored procedures follow status code standards
# Checks for proper FOUND_ROWS()/ROW_COUNT() usage and no unconditional SET p_Status = 0

$sqlFiles = Get-ChildItem "Database\CurrentStoredProcedures" -Filter "*.sql" | Where-Object { $_.Name -ne "00_STATUS_CODE_STANDARDS.md" }
$totalFiles = $sqlFiles.Count

Write-Host "`n=== Verifying Status Code Standards ===" -ForegroundColor Cyan
Write-Host "Total procedures to check: $totalFiles`n" -ForegroundColor Cyan

$violations = @()
$compliant = @()

foreach ($file in $sqlFiles) {
    $content = Get-Content $file.FullName -Raw
    $procName = $file.BaseName
    $issues = @()
    
    # Check 1: Has OUT p_Status parameter
    if ($content -notmatch "OUT p_Status") {
        $issues += "Missing OUT p_Status parameter"
    }
    
    # Check 2: Has OUT p_ErrorMsg parameter
    if ($content -notmatch "OUT p_ErrorMsg") {
        $issues += "Missing OUT p_ErrorMsg parameter"
    }
    
    # Check 3: Has EXIT HANDLER
    if ($content -notmatch "DECLARE EXIT HANDLER FOR SQLEXCEPTION") {
        $issues += "Missing EXIT HANDLER FOR SQLEXCEPTION"
    }
    
    # Check 4: Check for unconditional SET p_Status = 0 at end (BAD PATTERN)
    # This should ONLY match truly unconditional status settings, not those inside IF blocks
    if ($content -match "SET p_Status = 0;\s*\r?\n\s*END\$\$") {
        # Make sure it's not part of an IF/ELSE block
        if ($content -notmatch "ELSE\s+SET p_Status = 0;\s*\r?\n\s*(SET p_ErrorMsg[^;]+;)?\s*\r?\n\s*END IF") {
            $issues += "⚠️ CRITICAL: Unconditional SET p_Status = 0 at end"
        }
    }
    
    # Check 5: For SELECT queries, should use FOUND_ROWS()
    if ($content -match "\bSELECT\b" -and $content -notmatch "INTO" -and $content -notmatch "FOUND_ROWS()") {
        # Exclude procedures that only have SELECT INTO
        if ($content -match "SELECT [^;]+ FROM" -and $content -notmatch "SELECT [^;]+ INTO") {
            $issues += "SELECT query without FOUND_ROWS() check"
        }
    }
    
    # Check 6: For INSERT/UPDATE/DELETE, should use ROW_COUNT()
    if ($content -match "\b(INSERT|UPDATE|DELETE)\b" -and $content -notmatch "ROW_COUNT()") {
        $issues += "INSERT/UPDATE/DELETE without ROW_COUNT() check"
    }
    
    # Check 7: Should have status = 1 for success with data
    if ($content -notmatch "SET p_Status = 1") {
        $issues += "Missing SET p_Status = 1 for success"
    }
    
    # Check 8: Should set error status in EXIT HANDLER
    if ($content -match "DECLARE EXIT HANDLER" -and $content -notmatch "SET p_Status = -1") {
        $issues += "EXIT HANDLER doesn't set p_Status = -1"
    }
    
    if ($issues.Count -gt 0) {
        $violations += [PSCustomObject]@{
            Procedure = $procName
            Issues = $issues -join "; "
            Critical = ($issues | Where-Object { $_ -like "*CRITICAL*" }).Count -gt 0
        }
        
        if ($issues | Where-Object { $_ -like "*CRITICAL*" }) {
            Write-Host "[CRITICAL] $procName" -ForegroundColor Red
        } else {
            Write-Host "[ISSUES] $procName" -ForegroundColor Yellow
        }
        foreach ($issue in $issues) {
            Write-Host "  - $issue" -ForegroundColor Gray
        }
    } else {
        $compliant += $procName
        Write-Host "[OK] $procName" -ForegroundColor Green
    }
}

# Summary
Write-Host "`n=== SUMMARY ===" -ForegroundColor Cyan
Write-Host "Total procedures: $totalFiles" -ForegroundColor White
Write-Host "Compliant: $($compliant.Count)" -ForegroundColor Green
Write-Host "Issues found: $($violations.Count)" -ForegroundColor Yellow

if ($violations.Count -gt 0) {
    $critical = $violations | Where-Object { $_.Critical -eq $true }
    Write-Host "Critical violations: $($critical.Count)" -ForegroundColor Red
    
    Write-Host "`n=== VIOLATIONS BY SEVERITY ===" -ForegroundColor Cyan
    
    if ($critical.Count -gt 0) {
        Write-Host "`nCRITICAL (Unconditional p_Status = 0):" -ForegroundColor Red
        $critical | ForEach-Object {
            Write-Host "  - $($_.Procedure)" -ForegroundColor Red
        }
    }
    
    $nonCritical = $violations | Where-Object { $_.Critical -eq $false }
    if ($nonCritical.Count -gt 0) {
        Write-Host "`nNon-Critical Issues:" -ForegroundColor Yellow
        $nonCritical | Select-Object -First 10 | ForEach-Object {
            Write-Host "  - $($_.Procedure): $($_.Issues)" -ForegroundColor Gray
        }
        
        if ($nonCritical.Count -gt 10) {
            Write-Host "  ... and $($nonCritical.Count - 10) more" -ForegroundColor Gray
        }
    }
    
    # Export detailed report
    $violations | Export-Csv "Database\CurrentStoredProcedures\VERIFICATION_REPORT.csv" -NoTypeInformation
    Write-Host "`nDetailed report saved to: VERIFICATION_REPORT.csv" -ForegroundColor Cyan
}

Write-Host "`n✅ Verification complete!" -ForegroundColor Green
