# Manual refactoring guide - fixes the remaining issues that automated refactoring couldn't handle
# This script identifies the specific manual fixes needed for each procedure

$report = Import-Csv "Database\CurrentStoredProcedures\VERIFICATION_REPORT.csv"
$critical = $report | Where-Object { $_.Critical -eq $true }
$nonCritical = $report | Where-Object { $_.Critical -eq $false -and $_.Issues -ne "" }

Write-Host "`n=== MANUAL REFACTORING PLAN ===" -ForegroundColor Cyan
Write-Host "Total remaining issues: $($critical.Count + $nonCritical.Count)" -ForegroundColor Yellow

Write-Host "`n🔥 CRITICAL FIXES NEEDED (10 procedures):" -ForegroundColor Red
Write-Host "These have unconditional SET p_Status = 0 - MUST fix immediately`n" -ForegroundColor Red

foreach ($proc in $critical) {
    Write-Host "[$($proc.Procedure)]" -ForegroundColor Red
    Write-Host "  File: Database\CurrentStoredProcedures\$($proc.Procedure).sql" -ForegroundColor Gray
    Write-Host "  Issues: $($proc.Issues)" -ForegroundColor Yellow
    Write-Host ""
}

Write-Host "`n⚠️  NON-CRITICAL FIXES NEEDED (51 procedures):" -ForegroundColor Yellow
Write-Host "Grouped by fix type:`n" -ForegroundColor Yellow

# Group by common issue patterns
$missingParams = $nonCritical | Where-Object { $_.Issues -match "Missing OUT" }
$missingFoundRows = $nonCritical | Where-Object { $_.Issues -match "SELECT query without FOUND_ROWS" }
$missingRowCount = $nonCritical | Where-Object { $_.Issues -match "INSERT/UPDATE/DELETE without ROW_COUNT" }
$missingSuccess = $nonCritical | Where-Object { $_.Issues -match "Missing SET p_Status = 1" }

Write-Host "1. Missing OUT parameters ($($missingParams.Count) procedures):" -ForegroundColor Cyan
Write-Host "   These need OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500) added to signature" -ForegroundColor Gray
$missingParams | Select-Object -First 5 | ForEach-Object {
    Write-Host "   - $($_.Procedure)" -ForegroundColor White
}
if ($missingParams.Count -gt 5) {
    Write-Host "   ... and $($missingParams.Count - 5) more`n" -ForegroundColor Gray
}

Write-Host "2. Missing FOUND_ROWS() checks ($($missingFoundRows.Count) procedures):" -ForegroundColor Cyan
Write-Host "   These SELECT queries need FOUND_ROWS() check after query" -ForegroundColor Gray
$missingFoundRows | Select-Object -First 5 | ForEach-Object {
    Write-Host "   - $($_.Procedure)" -ForegroundColor White
}
if ($missingFoundRows.Count -gt 5) {
    Write-Host "   ... and $($missingFoundRows.Count - 5) more`n" -ForegroundColor Gray
}

Write-Host "3. Missing ROW_COUNT() checks ($($missingRowCount.Count) procedures):" -ForegroundColor Cyan
Write-Host "   These INSERT/UPDATE/DELETE need ROW_COUNT() check after operation" -ForegroundColor Gray
$missingRowCount | Select-Object -First 5 | ForEach-Object {
    Write-Host "   - $($_.Procedure)" -ForegroundColor White
}
if ($missingRowCount.Count -gt 5) {
    Write-Host "   ... and $($missingRowCount.Count - 5) more`n" -ForegroundColor Gray
}

Write-Host "4. Missing success status ($($missingSuccess.Count) procedures):" -ForegroundColor Cyan
Write-Host "   These need SET p_Status = 1 when successful" -ForegroundColor Gray
$missingSuccess | Select-Object -First 5 | ForEach-Object {
    Write-Host "   - $($_.Procedure)" -ForegroundColor White
}
if ($missingSuccess.Count -gt 5) {
    Write-Host "   ... and $($missingSuccess.Count - 5) more`n" -ForegroundColor Gray
}

Write-Host "`n=== RECOMMENDED APPROACH ===" -ForegroundColor Cyan
Write-Host "1. Fix 10 CRITICAL procedures first (remove unconditional p_Status = 0)" -ForegroundColor Yellow
Write-Host "2. Batch fix procedures missing OUT parameters (add to signatures)" -ForegroundColor Yellow
Write-Host "3. Add FOUND_ROWS() checks to SELECT queries" -ForegroundColor Yellow
Write-Host "4. Add ROW_COUNT() checks to INSERT/UPDATE/DELETE" -ForegroundColor Yellow
Write-Host "5. Verify all have SET p_Status = 1 for success" -ForegroundColor Yellow

Write-Host "`n📝 NOTE: Many procedures were created without OUT parameters originally." -ForegroundColor Gray
Write-Host "    This is a design decision - these procedures may need to remain as-is" -ForegroundColor Gray
Write-Host "    if changing signatures would break existing DAO code.`n" -ForegroundColor Gray

# Export detailed fix plan
$fixPlan = @()
foreach ($proc in $critical) {
    $fixPlan += [PSCustomObject]@{
        Procedure = $proc.Procedure
        Priority = "CRITICAL"
        File = "Database\CurrentStoredProcedures\$($proc.Procedure).sql"
        Issues = $proc.Issues
        Action = "Remove unconditional SET p_Status = 0; add proper status checks"
    }
}

foreach ($proc in $nonCritical) {
    $actions = @()
    if ($proc.Issues -match "Missing OUT") { $actions += "Add OUT parameters to signature" }
    if ($proc.Issues -match "SELECT query without FOUND_ROWS") { $actions += "Add FOUND_ROWS() check" }
    if ($proc.Issues -match "INSERT/UPDATE/DELETE without ROW_COUNT") { $actions += "Add ROW_COUNT() check" }
    if ($proc.Issues -match "Missing SET p_Status = 1") { $actions += "Add SET p_Status = 1 for success" }
    
    $fixPlan += [PSCustomObject]@{
        Procedure = $proc.Procedure
        Priority = "Normal"
        File = "Database\CurrentStoredProcedures\$($proc.Procedure).sql"
        Issues = $proc.Issues
        Action = $actions -join "; "
    }
}

$fixPlan | Export-Csv "Database\CurrentStoredProcedures\MANUAL_FIX_PLAN.csv" -NoTypeInformation
Write-Host "✅ Detailed fix plan exported to: MANUAL_FIX_PLAN.csv`n" -ForegroundColor Green
