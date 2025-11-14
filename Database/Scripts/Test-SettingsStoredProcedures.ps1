<#
.SYNOPSIS
    Tests all settings-related stored procedures for correct functionality.

.DESCRIPTION
    Systematically tests user and UI settings stored procedures by:
    1. Running each procedure with test data
    2. Verifying expected results
    3. Reporting any failures

.EXAMPLE
    .\Test-SettingsStoredProcedures.ps1
#>

param(
    [string]$MySqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe",
    [string]$ServerHost = "localhost",
    [int]$Port = 3306,
    [string]$User = "root",
    [string]$DbPassword = "root",
    [string]$Database = "MTM_WIP_Application_Winforms"
)

$ErrorActionPreference = "Continue"

function Invoke-MySqlQuery {
    param([string]$Query)
    
    $output = & $MySqlPath -h $ServerHost -P $Port -u $User -p"$DbPassword" -D $Database -e $Query 2>&1 | 
        Where-Object { $_ -notmatch "Warning.*password" }
    
    return $output
}

Write-Host "=== Settings Stored Procedures Test Suite ===" -ForegroundColor Cyan
Write-Host "Database: $Database" -ForegroundColor Gray
Write-Host ""

$testResults = @()
$testsPassed = 0
$testsFailed = 0

# Test 1: usr_users_Exists with non-existent user
Write-Host "[1] Testing usr_users_Exists (non-existent user)..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery "CALL usr_users_Exists('NONEXISTENTUSER9999', @status, @msg); SELECT @status as status, @msg as message;"
$status = ($result | Select-String -Pattern "^\d+\s+" | Select-Object -First 1) -replace '\s+.*$', ''

if ($status -eq "0") {
    Write-Host "  ✅ PASS: Correctly returns status 0 for non-existent user" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Exists (not exists)"; Result = "PASS"; Details = "Status: $status" }
} else {
    Write-Host "  ❌ FAIL: Expected status 0, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Exists (not exists)"; Result = "FAIL"; Details = "Expected 0, got $status" }
}

# Test 2: usr_users_Exists with existing user
Write-Host "[2] Testing usr_users_Exists (existing user)..." -ForegroundColor Yellow
$existingUser = Invoke-MySqlQuery "SELECT User FROM usr_users LIMIT 1;" | Select-Object -Skip 1 | Select-Object -First 1
$existingUser = $existingUser.Trim()

$result = Invoke-MySqlQuery "CALL usr_users_Exists('$existingUser', @status, @msg); SELECT @status as status, @msg as message;"
$status = ($result | Select-String -Pattern "^\d+\s+" | Select-Object -First 1) -replace '\s+.*$', ''

if ($status -eq "1") {
    Write-Host "  ✅ PASS: Correctly returns status 1 for existing user '$existingUser'" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Exists (exists)"; Result = "PASS"; Details = "User: $existingUser, Status: $status" }
} else {
    Write-Host "  ❌ FAIL: Expected status 1, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Exists (exists)"; Result = "FAIL"; Details = "Expected 1, got $status" }
}

# Test 3: usr_users_Get_ByUser
Write-Host "[3] Testing usr_users_Get_ByUser..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery "CALL usr_users_Get_ByUser('$existingUser', @status, @msg); SELECT @status as status;"
$status = ($result | Select-String -Pattern "^\d+$" | Select-Object -First 1)

if ($status -eq "1") {
    Write-Host "  ✅ PASS: Successfully retrieved user '$existingUser'" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Get_ByUser"; Result = "PASS"; Details = "User: $existingUser" }
} else {
    Write-Host "  ❌ FAIL: Expected status 1, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Get_ByUser"; Result = "FAIL"; Details = "Expected 1, got $status" }
}

# Test 4: usr_users_Get_All
Write-Host "[4] Testing usr_users_Get_All..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery "CALL usr_users_Get_All(@status, @msg); SELECT @status as status;"
$status = ($result | Select-String -Pattern "^\d+$" | Select-Object -First 1)

if ($status -eq "1") {
    Write-Host "  ✅ PASS: Successfully retrieved all users" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Get_All"; Result = "PASS"; Details = "Status: $status" }
} else {
    Write-Host "  ❌ FAIL: Expected status 1, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Get_All"; Result = "FAIL"; Details = "Expected 1, got $status" }
}

# Test 5: usr_ui_settings_Get
Write-Host "[5] Testing usr_ui_settings_Get..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery "CALL usr_ui_settings_Get('$existingUser', @status, @msg); SELECT @status as status;"
$status = ($result | Select-String -Pattern "^[0-1]$" | Select-Object -First 1)

if ($status -eq "1" -or $status -eq "0") {
    Write-Host "  ✅ PASS: Retrieved settings for user '$existingUser' (status: $status)" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_ui_settings_Get"; Result = "PASS"; Details = "User: $existingUser, Status: $status" }
} else {
    Write-Host "  ❌ FAIL: Unexpected status $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_ui_settings_Get"; Result = "FAIL"; Details = "Unexpected status: $status" }
}

# Test 6: usr_ui_settings_GetJsonSetting
Write-Host "[6] Testing usr_ui_settings_GetJsonSetting..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery "CALL usr_ui_settings_GetJsonSetting('$existingUser', @status, @msg); SELECT @status as status;"
$status = ($result | Select-String -Pattern "^[0-1]$" | Select-Object -First 1)

if ($status -eq "1" -or $status -eq "0") {
    Write-Host "  ✅ PASS: Retrieved JSON settings for user '$existingUser' (status: $status)" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_ui_settings_GetJsonSetting"; Result = "PASS"; Details = "User: $existingUser, Status: $status" }
} else {
    Write-Host "  ❌ FAIL: Unexpected status $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_ui_settings_GetJsonSetting"; Result = "FAIL"; Details = "Unexpected status: $status" }
}

# Test 7: usr_ui_settings_GetShortcutsJson
Write-Host "[7] Testing usr_ui_settings_GetShortcutsJson..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery "CALL usr_ui_settings_GetShortcutsJson('$existingUser', @status, @msg); SELECT @status as status;"
$status = ($result | Select-String -Pattern "^[0-1]$" | Select-Object -First 1)

if ($status -eq "1" -or $status -eq "0") {
    Write-Host "  ✅ PASS: Retrieved shortcuts for user '$existingUser' (status: $status)" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_ui_settings_GetShortcutsJson"; Result = "PASS"; Details = "User: $existingUser, Status: $status" }
} else {
    Write-Host "  ❌ FAIL: Unexpected status $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_ui_settings_GetShortcutsJson"; Result = "FAIL"; Details = "Unexpected status: $status" }
}

# Test 8: usr_users_Add_User (with rollback)
Write-Host "[8] Testing usr_users_Add_User (with rollback)..." -ForegroundColor Yellow
$testUser = "TEST_USER_$(Get-Date -Format 'yyyyMMddHHmmss')"
$result = Invoke-MySqlQuery @"
START TRANSACTION;
CALL usr_users_Add_User(
    '$testUser', 'Test User', 'Day', 0, '1234', '1.0.0', 'false',
    'Default', 12, 'visualuser', 'visualpass',
    '172.16.1.104', '3306', 'mtm_wip_application',
    @status, @msg
);
SELECT @status as status, @msg as message;
ROLLBACK;
"@

$status = ($result | Select-String -Pattern "^[0-1]$" | Select-Object -First 1)

if ($status -eq "1") {
    Write-Host "  ✅ PASS: Successfully added user (rolled back)" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Add_User"; Result = "PASS"; Details = "User: $testUser (rolled back)" }
} else {
    Write-Host "  ❌ FAIL: Expected status 1, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Add_User"; Result = "FAIL"; Details = "Expected 1, got $status" }
}

# Test 9: usr_users_Update_User
Write-Host "[9] Testing usr_users_Update_User..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery @"
START TRANSACTION;
CALL usr_users_Update_User(
    '$existingUser', 'Updated Full Name', 'Night', '9999',
    'updated_visual', 'updated_pass',
    @status, @msg
);
SELECT @status as status;
ROLLBACK;
"@

$status = ($result | Select-String -Pattern "^1$" | Select-Object -First 1)

if ($status -eq "1") {
    Write-Host "  ✅ PASS: Successfully updated user (rolled back)" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Update_User"; Result = "PASS"; Details = "User: $existingUser (rolled back)" }
} else {
    Write-Host "  ❌ FAIL: Expected status 1, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_Update_User"; Result = "FAIL"; Details = "Expected 1, got $status" }
}

# Test 10: usr_users_SetUserSetting_ByUserAndField
Write-Host "[10] Testing usr_users_SetUserSetting_ByUserAndField..." -ForegroundColor Yellow
$result = Invoke-MySqlQuery @"
START TRANSACTION;
CALL usr_users_SetUserSetting_ByUserAndField(
    '$existingUser', 'Shift', 'Test Shift',
    @status, @msg
);
SELECT @status as status;
ROLLBACK;
"@

$status = ($result | Select-String -Pattern "^1$" | Select-Object -First 1)

if ($status -eq "1") {
    Write-Host "  ✅ PASS: Successfully set user setting (rolled back)" -ForegroundColor Green
    $testsPassed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_SetUserSetting_ByUserAndField"; Result = "PASS"; Details = "Field: Shift (rolled back)" }
} else {
    Write-Host "  ❌ FAIL: Expected status 1, got $status" -ForegroundColor Red
    $testsFailed++
    $testResults += [PSCustomObject]@{ Test = "usr_users_SetUserSetting_ByUserAndField"; Result = "FAIL"; Details = "Expected 1, got $status" }
}

Write-Host ""
Write-Host "=== TEST SUMMARY ===" -ForegroundColor Cyan
Write-Host "Total Tests: $($testsPassed + $testsFailed)" -ForegroundColor White
Write-Host "Passed: $testsPassed" -ForegroundColor Green
Write-Host "Failed: $testsFailed" -ForegroundColor $(if ($testsFailed -eq 0) { "GREEN" } else { "RED" })
Write-Host ""

# Export results
$resultsFile = "Database\ValidationRuns\settings-sp-test-$(Get-Date -Format 'yyyyMMdd-HHmmss').csv"
$testResults | Export-Csv -Path $resultsFile -NoTypeInformation
Write-Host "Results exported to: $resultsFile" -ForegroundColor Cyan
Write-Host ""

if ($testsFailed -gt 0) {
    Write-Host "❌ Some tests failed" -ForegroundColor Red
    $testResults | Where-Object { $_.Result -eq "FAIL" } | Format-Table -AutoSize
    exit 1
} else {
    Write-Host "✅ All tests passed!" -ForegroundColor Green
    exit 0
}
