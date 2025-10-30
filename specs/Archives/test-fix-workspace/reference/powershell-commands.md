# PowerShell Command Reference

**Purpose**: Quick reference for build, test, and database commands  
**Last Updated**: 2025-10-19  
**Shell**: PowerShell 7+

---

## Quick Navigation

- [Build Commands](#build-commands)
- [Test Commands](#test-commands)
- [Database Commands](#database-commands)
- [Workspace Commands](#workspace-commands)

---

## Build Commands

### Clean and Restore

```powershell
# Clean solution
dotnet clean

# Restore NuGet packages
dotnet restore

# Clean and restore together
dotnet clean && dotnet restore
```

### Build Project

```powershell
# Build Debug configuration (uses test database)
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug

# Build Release configuration (uses production database)
dotnet build MTM_WIP_Application_Winforms.csproj -c Release

# Build with specific framework
dotnet build -p:TargetFramework=net8.0-windows -p:Configuration=Debug

# Build and suppress specific warnings
dotnet build -c Debug /p:NoWarn=CS1591,CS8618
```

### Check for Errors

```powershell
# Build and output to file for review
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug > build-output.txt

# Check for warnings only
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug 2>&1 | Select-String "warning"

# Check for errors only
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug 2>&1 | Select-String "error"
```

---

## Test Commands

### Run All Tests

```powershell
# Run all integration tests
dotnet test --filter "FullyQualifiedName~Integration"

# Run all tests (integration + unit)
dotnet test
```

### Run Category Tests

```powershell
# Category 1: Quick Button tests
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Category 2: System DAO tests
dotnet test --filter "FullyQualifiedName~Dao_System_Tests"

# Category 3: Helper & Validation tests
dotnet test --filter "FullyQualifiedName~Helper_Tests|FullyQualifiedName~Validation_Tests"

# Category 4: Specific failing tests
dotnet test --filter "FullyQualifiedName~Dao_Inventory_Tests.GetInventoryByLocation_ValidLocation_ReturnsInventory"
```

### Run Single Test

```powershell
# Run specific test by full name
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests.AddQuickButton_ValidData_InsertsButton"

# Run all tests in a test class
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Run tests matching name pattern
dotnet test --filter "DisplayName~QuickButton"
```

### Test Output Options

```powershell
# Detailed output
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests" --logger "console;verbosity=detailed"

# Minimal output
dotnet test --filter "FullyQualifiedName~Integration" --logger "console;verbosity=minimal"

# Output to file
dotnet test --filter "FullyQualifiedName~Integration" > test-results.txt 2>&1
```

### Test with Coverage (if configured)

```powershell
# Collect code coverage
dotnet test --filter "FullyQualifiedName~Integration" --collect:"XPlat Code Coverage"

# Generate coverage report (requires ReportGenerator tool)
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```

---

## Database Commands

### MySQL Connection

```powershell
# Connect to test database
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test

# Connect to production database (be careful!)
mysql -h 172.16.1.104 -P 3306 -u root -proot MTM_WIP_Application_Winforms

# Connect and run specific query
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT COUNT(*) FROM usr_users WHERE UserID LIKE 'TEST-%';"
```

### Execute SQL File

```powershell
# Run SQL script
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test < seed-test-data.sql

# Run with verbose output
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -v < seed-test-data.sql

# Run and capture output
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test < script.sql > output.txt 2>&1
```

### Query Test Data

```powershell
# Count test users
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT COUNT(*) AS TestUsers FROM usr_users WHERE UserID LIKE 'TEST-%';"

# List test quick buttons
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT * FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';"

# Export query to CSV
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT * FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%'" > quick-buttons.csv
```

### Clean Up Test Data

```powershell
# Delete test quick buttons
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';"

# Delete test users
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "DELETE FROM usr_users WHERE UserID LIKE 'TEST-%';"

# Clean up all test data (order matters!)
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "
DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';
DELETE FROM sys_transaction_history WHERE UserID LIKE 'TEST-%';
DELETE FROM usr_users WHERE UserID LIKE 'TEST-%';
"
```

### Database Backup

```powershell
# Backup test database
mysqldump -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test > backup-test-db-$(Get-Date -Format 'yyyyMMdd-HHmmss').sql

# Backup specific tables
mysqldump -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test usr_users sys_quick_buttons > backup-test-tables.sql

# Restore from backup
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test < backup-test-db.sql
```

---

## Workspace Commands

### Navigation

```powershell
# Navigate to workspace root
cd c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms

# Navigate to test fix workspace
cd c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\specs\test-fix-workspace

# Navigate to tests
cd c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Tests\Integration
```

### File Operations

```powershell
# List workspace structure
Get-ChildItem -Recurse -Directory | Select-Object FullName

# Find files by pattern
Get-ChildItem -Recurse -Filter "*.md" | Select-Object FullName

# Search file content
Get-ChildItem -Recurse -Filter "*.md" | Select-String "test data"

# Count markdown files
(Get-ChildItem -Recurse -Filter "*.md").Count
```

### Workspace Scripts (Future)

```powershell
# Update progress after fixing tests
.\tools\Update-Progress.ps1 -TestName "AddQuickButton_ValidData_InsertsButton" -Category 1

# Regenerate dashboard
.\tools\New-Dashboard.ps1

# Run category tests
.\tools\Invoke-CategoryTests.ps1 -Category 1

# Validate workspace structure
.\tools\Test-WorkspaceStructure.ps1

# Dry-run mode (preview changes without applying)
.\tools\Update-Progress.ps1 -TestName "TestName" -Category 1 -WhatIf
```

---

## Useful Combinations

### Complete Build and Test Cycle

```powershell
# Clean, restore, build, and test
dotnet clean && dotnet restore && dotnet build -c Debug && dotnet test --filter "FullyQualifiedName~Integration"
```

### Test Specific Category with Detailed Output

```powershell
# Run Category 1 tests with detailed output
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests" --logger "console;verbosity=detailed" 2>&1 | Tee-Object -FilePath category1-results.txt
```

### Database Setup for Testing

```powershell
# Complete test data setup
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test < seed-test-users.sql
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test < seed-quick-buttons.sql

# Verify setup
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "
SELECT 'Users' AS Type, COUNT(*) AS Count FROM usr_users WHERE UserID LIKE 'TEST-%'
UNION ALL
SELECT 'Quick Buttons', COUNT(*) FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';
"
```

### Test, Review Results, Clean Up

```powershell
# Run tests and save output
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests" > test-results.txt 2>&1

# Review results
Get-Content test-results.txt | Select-String "Passed|Failed"

# Clean up test data
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "
DELETE FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%';
DELETE FROM usr_users WHERE UserID LIKE 'TEST-%';
"
```

---

## Environment Variables

### Useful for Scripts

```powershell
# Set workspace root
$env:MTM_WORKSPACE = "c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"

# Set test database connection
$env:MTM_TEST_DB = "Server=localhost;Port=3306;Database=mtm_wip_application_winforms_test;User=root;Password=root;SslMode=none;"

# Use in commands
cd $env:MTM_WORKSPACE
```

---

## Troubleshooting Commands

### Check .NET Version

```powershell
# Check installed .NET SDK versions
dotnet --list-sdks

# Check .NET runtime versions
dotnet --list-runtimes

# Check current .NET version
dotnet --version
```

### Check MySQL Service

```powershell
# Check if MySQL is running (Windows)
Get-Service -Name "*mysql*"

# Start MySQL service (if stopped)
Start-Service -Name "MySQL80"  # Adjust name as needed

# Test MySQL connection
mysql -h localhost -P 3306 -u root -proot -e "SELECT VERSION();"
```

### Check Build Issues

```powershell
# Detailed build diagnostics
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug -v detailed > build-detailed.log 2>&1

# Check for specific error codes
Get-Content build-detailed.log | Select-String "CS[0-9]{4}"

# List all warnings
Get-Content build-detailed.log | Select-String "warning"
```

---

## Quick Reference Card

**Most Common Commands**:

```powershell
# Build
dotnet build -c Debug

# Run all integration tests
dotnet test --filter "FullyQualifiedName~Integration"

# Run Category 1 tests
dotnet test --filter "FullyQualifiedName~Dao_QuickButtons_Tests"

# Connect to test database
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test

# Count test users
mysql -h localhost -P 3306 -u root -proot mtm_wip_application_winforms_test -e "SELECT COUNT(*) FROM usr_users WHERE UserID LIKE 'TEST-%';"
```

---

**Command Count**: 50+ ready-to-use commands  
**Last Updated**: 2025-10-19  
**Maintained by**: Development Team
