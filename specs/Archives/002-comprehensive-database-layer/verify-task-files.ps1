<#
.SYNOPSIS
    Verifies existence of all files referenced in tasks.md for 002-comprehensive-database-layer

.DESCRIPTION
    This script checks every file path mentioned in tasks T001-T067 and reports:
    - Files that exist (ready for refactoring)
    - Files that don't exist (need creation or task removal)
    - Conditional files marked "(if exists)" that need decision

.EXAMPLE
    .\verify-task-files.ps1
    .\verify-task-files.ps1 -Json
#>

[CmdletBinding()]
param(
    [switch]$Json
)

$ErrorActionPreference = "Stop"
$repoRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)

# Files to be CREATED (should NOT exist yet)
$filesToCreate = @(
    @{ Path = "Models/Model_Dao_Result.cs"; Task = "T001"; Phase = "Phase 1: Setup" }
    @{ Path = "Models/Model_Dao_Result_Generic.cs"; Task = "T002"; Phase = "Phase 1: Setup" }
    @{ Path = "Models/Model_ParameterPrefix_Cache.cs"; Task = "T003"; Phase = "Phase 1: Setup" }
    @{ Path = "Tests/Integration/BaseIntegrationTest.cs"; Task = "T013"; Phase = "Phase 2: Foundational" }
    @{ Path = "Tests/Integration/Dao_System_Tests.cs"; Task = "T014"; Phase = "Phase 3: US1" }
    @{ Path = "Tests/Integration/Dao_ErrorLog_Tests.cs"; Task = "T015"; Phase = "Phase 3: US1" }
    @{ Path = "Tests/Integration/Helper_Database_StoredProcedure_Tests.cs"; Task = "T016"; Phase = "Phase 3: US1" }
    @{ Path = "Tests/Integration/Dao_Inventory_Tests.cs"; Task = "T021"; Phase = "Phase 4: US2" }
    @{ Path = "Tests/Integration/ConnectionPooling_Tests.cs"; Task = "T022"; Phase = "Phase 4: US2" }
    @{ Path = "Tests/Integration/TransactionManagement_Tests.cs"; Task = "T023"; Phase = "Phase 4: US2" }
    @{ Path = "Tests/Integration/ErrorLogging_Tests.cs"; Task = "T030"; Phase = "Phase 5: US3" }
    @{ Path = "Tests/Integration/ValidationErrors_Tests.cs"; Task = "T031"; Phase = "Phase 5: US3" }
    @{ Path = "Tests/Integration/ErrorCooldown_Tests.cs"; Task = "T032"; Phase = "Phase 5: US3" }
    @{ Path = "Tests/Integration/StoredProcedureValidation_Tests.cs"; Task = "T039"; Phase = "Phase 6: US4" }
    @{ Path = "Tests/Integration/ParameterNaming_Tests.cs"; Task = "T041"; Phase = "Phase 6: US4" }
    @{ Path = "Tests/Integration/PerformanceMonitoring_Tests.cs"; Task = "T050"; Phase = "Phase 7: US5" }
    @{ Path = "Tests/Integration/ConcurrentOperations_Tests.cs"; Task = "T051"; Phase = "Phase 7: US5" }
    @{ Path = "Tests/Integration/TransactionRollback_Tests.cs"; Task = "T052"; Phase = "Phase 7: US5" }
    @{ Path = "Scripts/Validate-Parameter-Prefixes.ps1"; Task = "T040"; Phase = "Phase 6: US4" }
    @{ Path = "Documentation/Database-Layer-Migration-Guide.md"; Task = "T062"; Phase = "Phase 8: Polish" }
)

# Files to be REFACTORED (MUST exist)
$filesToRefactor = @(
    @{ Path = "Program.cs"; Task = "T004, T019"; Phase = "Phase 1 & 3"; Purpose = "INFORMATION_SCHEMA init + startup validation" }
    @{ Path = "Helpers/Helper_Database_StoredProcedure.cs"; Task = "T005-T010"; Phase = "Phase 2: Foundational"; Purpose = "Refactor all 4 execution methods" }
    @{ Path = "Helpers/Helper_Database_Variables.cs"; Task = "T011"; Phase = "Phase 2: Foundational"; Purpose = "Add TestDatabaseName constant" }
    @{ Path = "Logging/LoggingUtility.cs"; Task = "T012, T037"; Phase = "Phase 2 & 5"; Purpose = "Recursive prevention + severity classification" }
    @{ Path = "Data/Dao_System.cs"; Task = "T017"; Phase = "Phase 3: US1"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_ErrorLog.cs"; Task = "T018"; Phase = "Phase 3: US1"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_Inventory.cs"; Task = "T024"; Phase = "Phase 4: US2"; Purpose = "Async methods with transaction management" }
    @{ Path = "Data/Dao_Transactions.cs"; Task = "T025"; Phase = "Phase 4: US2"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_History.cs"; Task = "T026"; Phase = "Phase 4: US2"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Forms/MainForm/MainForm.cs"; Task = "T027"; Phase = "Phase 4: US2"; Purpose = "Async event handlers" }
    @{ Path = "Forms/Transactions/TransactionForm.cs"; Task = "T028"; Phase = "Phase 4: US2"; Purpose = "Async event handlers" }
    @{ Path = "Data/Dao_User.cs"; Task = "T033"; Phase = "Phase 5: US3"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_Part.cs"; Task = "T034"; Phase = "Phase 5: US3"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Forms/Settings/UserManagementForm.cs"; Task = "T038"; Phase = "Phase 5: US3"; Purpose = "Async event handlers" }
    @{ Path = "Data/Dao_Location.cs"; Task = "T042"; Phase = "Phase 6: US4"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_Operation.cs"; Task = "T043"; Phase = "Phase 6: US4"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_ItemType.cs"; Task = "T044"; Phase = "Phase 6: US4"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Data/Dao_QuickButtons.cs"; Task = "T045"; Phase = "Phase 6: US4"; Purpose = "Async methods returning Model_Dao_Result" }
    @{ Path = "Forms/Settings/LocationManagementForm.cs"; Task = "T046"; Phase = "Phase 6: US4"; Purpose = "Async event handlers" }
    @{ Path = "Forms/Settings/OperationManagementForm.cs"; Task = "T047"; Phase = "Phase 6: US4"; Purpose = "Async event handlers" }
    @{ Path = "Controls/MainForm/QuickButtonsControl.cs"; Task = "T048"; Phase = "Phase 6: US4"; Purpose = "Async event handlers" }
    @{ Path = "Documentation/Copilot Files/04-patterns-and-templates.md"; Task = "T060"; Phase = "Phase 8: Polish"; Purpose = "Update with Model_Dao_Result patterns" }
    @{ Path = "README.md"; Task = "T061"; Phase = "Phase 8: Polish"; Purpose = "Update Database Access Patterns section" }
)

# Files marked with "(if exists)" - need verification and decision
$conditionalFiles = @(
    @{ Path = "Forms/MainForm/InventorySearchForm.cs"; Task = "T055"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T055 or create skeleton" }
    @{ Path = "Forms/MainForm/ReportsForm.cs"; Task = "T055"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T055 or create skeleton" }
    @{ Path = "Forms/Transactions/BatchOperationsForm.cs"; Task = "T055"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T055 (marked optional in task)" }
    @{ Path = "Controls/MainForm/InventoryGridControl.cs"; Task = "T056"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T056 (marked conditional)" }
    @{ Path = "Controls/Shared/DataGridViewHelper.cs"; Task = "T056"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T056 (marked conditional on async methods)" }
    @{ Path = "Services/Service_InventoryMonitoring.cs"; Task = "T057"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T057 (marked optional)" }
    @{ Path = "Services/Service_BackupScheduler.cs"; Task = "T057"; Phase = "Phase 7: US5"; Decision = "If missing, remove from T057 (marked optional)" }
    @{ Path = "Services/Service_Startup.cs"; Task = "T057"; Phase = "Phase 7: US5"; Decision = "If missing, create basic startup service wrapper" }
)

# Check files
$results = @{
    FilesToCreate = @{
        Total = $filesToCreate.Count
        Exists = @()
        Missing = @()
    }
    FilesToRefactor = @{
        Total = $filesToRefactor.Count
        Exists = @()
        Missing = @()
    }
    ConditionalFiles = @{
        Total = $conditionalFiles.Count
        Exists = @()
        Missing = @()
    }
}

Write-Host "`n=== File Verification for 002-comprehensive-database-layer ===" -ForegroundColor Cyan

# Check files to create
Write-Host "`n[1] Files to be CREATED (should NOT exist yet):" -ForegroundColor Yellow
foreach ($file in $filesToCreate) {
    $fullPath = Join-Path $repoRoot $file.Path
    $exists = Test-Path $fullPath
    
    if ($exists) {
        Write-Host "  ⚠ ALREADY EXISTS: $($file.Path) ($($file.Task))" -ForegroundColor Yellow
        $results.FilesToCreate.Exists += $file
    } else {
        Write-Host "  ✓ Ready to create: $($file.Path) ($($file.Task))" -ForegroundColor Green
        $results.FilesToCreate.Missing += $file
    }
}

# Check files to refactor
Write-Host "`n[2] Files to be REFACTORED (MUST exist):" -ForegroundColor Yellow
foreach ($file in $filesToRefactor) {
    $fullPath = Join-Path $repoRoot $file.Path
    $exists = Test-Path $fullPath
    
    if ($exists) {
        Write-Host "  ✓ EXISTS: $($file.Path) ($($file.Task))" -ForegroundColor Green
        $results.FilesToRefactor.Exists += $file
    } else {
        Write-Host "  ✗ MISSING: $($file.Path) ($($file.Task)) - BLOCKING!" -ForegroundColor Red
        $results.FilesToRefactor.Missing += $file
    }
}

# Check conditional files
Write-Host "`n[3] Conditional Files (need decision):" -ForegroundColor Yellow
foreach ($file in $conditionalFiles) {
    $fullPath = Join-Path $repoRoot $file.Path
    $exists = Test-Path $fullPath
    
    if ($exists) {
        Write-Host "  ✓ EXISTS: $($file.Path) ($($file.Task))" -ForegroundColor Green
        $results.ConditionalFiles.Exists += $file
    } else {
        Write-Host "  ? MISSING: $($file.Path) ($($file.Task))" -ForegroundColor Magenta
        Write-Host "    Decision: $($file.Decision)" -ForegroundColor DarkGray
        $results.ConditionalFiles.Missing += $file
    }
}

# Summary
Write-Host "`n=== SUMMARY ===" -ForegroundColor Cyan
Write-Host "Files to Create: $($results.FilesToCreate.Missing.Count)/$($results.FilesToCreate.Total) ready ($($results.FilesToCreate.Exists.Count) already exist)" -ForegroundColor $(if ($results.FilesToCreate.Exists.Count -eq 0) { "GREEN" } else { "YELLOW" })
Write-Host "Files to Refactor: $($results.FilesToRefactor.Exists.Count)/$($results.FilesToRefactor.Total) ready" -ForegroundColor $(if ($results.FilesToRefactor.Missing.Count -eq 0) { "GREEN" } else { "RED" })
Write-Host "Conditional Files: $($results.ConditionalFiles.Exists.Count) exist, $($results.ConditionalFiles.Missing.Count) need decision" -ForegroundColor $(if ($results.ConditionalFiles.Missing.Count -eq 0) { "GREEN" } else { "Magenta" })

# Blocking issues?
if ($results.FilesToRefactor.Missing.Count -gt 0) {
    Write-Host "`n⚠ BLOCKING ISSUES DETECTED!" -ForegroundColor Red
    Write-Host "The following files are required for refactoring but are missing:" -ForegroundColor Red
    foreach ($file in $results.FilesToRefactor.Missing) {
        Write-Host "  - $($file.Path) (Task $($file.Task): $($file.Purpose))" -ForegroundColor Red
    }
    Write-Host "`nAction: Verify file paths are correct or create placeholder files before proceeding." -ForegroundColor Yellow
} else {
    Write-Host "`n✓ All required refactor files exist - ready to proceed!" -ForegroundColor Green
}

# Conditional decisions needed?
if ($results.ConditionalFiles.Missing.Count -gt 0) {
    Write-Host "`n⚠ DECISIONS NEEDED for conditional files:" -ForegroundColor Magenta
    foreach ($file in $results.ConditionalFiles.Missing) {
        Write-Host "  - $($file.Path) (Task $($file.Task))" -ForegroundColor Magenta
        Write-Host "    → $($file.Decision)" -ForegroundColor DarkGray
    }
    Write-Host "`nAction: Review tasks.md T055-T057 and remove references to missing files or create them." -ForegroundColor Yellow
}

# JSON output if requested
if ($Json) {
    $results | ConvertTo-Json -Depth 10
}

Write-Host ""
