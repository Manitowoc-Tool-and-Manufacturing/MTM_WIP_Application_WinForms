# Setup Test Database Script
# This script exports schema and stored procedures from production database
# and imports them into the test database

$ErrorActionPreference = "Stop"
$mysqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe"
$mysqldumpPath = "C:\MAMP\bin\mysql\bin\mysqldump.exe"
$prodDatabase = "mtm_wip_application"
$testDatabase = "mtm_wip_application_winforms_test"
$username = "root"
$password = "root"

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "MTM Test Database Setup" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Step 1: Export schema (structure only, no data)
Write-Host "[1/4] Exporting schema from production database..." -ForegroundColor Yellow
$schemaFile = "test-db-schema.sql"
& $mysqldumpPath -u $username -p$password --no-data --routines --triggers $prodDatabase > $schemaFile
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to export schema" -ForegroundColor Red
    exit 1
}
Write-Host "✓ Schema exported to $schemaFile" -ForegroundColor Green
Write-Host ""

# Step 2: Import schema into test database
Write-Host "[2/4] Importing schema into test database..." -ForegroundColor Yellow
Get-Content $schemaFile | & $mysqlPath -u $username -p$password $testDatabase 2>&1 | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to import schema" -ForegroundColor Red
    exit 1
}
Write-Host "✓ Schema imported successfully" -ForegroundColor Green
Write-Host ""

# Step 3: Export seed data from master tables
Write-Host "[3/4] Exporting seed data from master tables..." -ForegroundColor Yellow
$seedFile = "test-db-seed-data.sql"
$masterTables = @("location", "operation", "itemtype", "role", "user")
$tableArgs = $masterTables -join " "
& $mysqldumpPath -u $username -p$password --no-create-info --skip-triggers $prodDatabase $tableArgs > $seedFile
if ($LASTEXITCODE -ne 0) {
    Write-Host "⚠ Warning: Some seed data may not have been exported" -ForegroundColor Yellow
} else {
    Write-Host "✓ Seed data exported to $seedFile" -ForegroundColor Green
}
Write-Host ""

# Step 4: Import seed data into test database
Write-Host "[4/4] Importing seed data into test database..." -ForegroundColor Yellow
if (Test-Path $seedFile) {
    Get-Content $seedFile | & $mysqlPath -u $username -p$password $testDatabase 2>&1 | Out-Null
    if ($LASTEXITCODE -ne 0) {
        Write-Host "⚠ Warning: Some seed data may not have been imported" -ForegroundColor Yellow
    } else {
        Write-Host "✓ Seed data imported successfully" -ForegroundColor Green
    }
} else {
    Write-Host "⚠ Warning: No seed data file found" -ForegroundColor Yellow
}
Write-Host ""

Write-Host "================================================" -ForegroundColor Cyan
Write-Host "✓ Test Database Setup Complete!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Database: $testDatabase" -ForegroundColor White
Write-Host "Schema file: $schemaFile" -ForegroundColor White
Write-Host "Seed data file: $seedFile" -ForegroundColor White
Write-Host ""
Write-Host "Next step: Run tests with:" -ForegroundColor Yellow
Write-Host "  dotnet test Tests/MTM_Inventory_Application.Tests.csproj" -ForegroundColor Cyan
Write-Host ""
