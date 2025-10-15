<#
.SYNOPSIS
    Deploys stored procedures to MAMP MySQL 5.7 database.

.DESCRIPTION
    This script:
    1. Connects to the MAMP MySQL 5.7 database
    2. Reads all .sql files from CurrentStoredProcedures folder
    3. Executes each stored procedure creation script
    4. Reports success/failure for each procedure
    5. Generates a deployment report

.PARAMETER Server
    MySQL server address (default: localhost)

.PARAMETER Port
    MySQL port (default: 3306)

.PARAMETER Database
    Database name (default: mtm_wip_application)

.PARAMETER User
    MySQL username (default: root)

.PARAMETER Password
    MySQL password (default: root)

.PARAMETER WhatIf
    Shows what would be deployed without actually executing

.EXAMPLE
    .\Deploy-StoredProcedures.ps1
    
.EXAMPLE
    .\Deploy-StoredProcedures.ps1 -Database mtm_wip_application_winforms_test

.NOTES
    Created: 2025-10-14
    Requires: MySQL command-line client (mysql.exe) in PATH or MAMP installation
#>

[CmdletBinding()]
param(
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$Database = "mtm_wip_application",
    [string]$User = "root",
    [string]$Password = "root",
    [switch]$WhatIf
)

$ErrorActionPreference = "Continue"

# Paths
$repoRoot = Split-Path $PSScriptRoot -Parent
$proceduresFolder = Join-Path $repoRoot "Database\CurrentStoredProcedures"
$reportPath = Join-Path $repoRoot "Database\CurrentStoredProcedures\DEPLOYMENT_REPORT.txt"

# Common MAMP MySQL paths
$mampPaths = @(
    "C:\MAMP\bin\mysql\bin\mysql.exe",
    "C:\MAMP\bin\mysql\mysql5.7.24\bin\mysql.exe",
    "C:\Program Files\MySQL\MySQL Server 5.7\bin\mysql.exe",
    "C:\Program Files (x86)\MySQL\MySQL Server 5.7\bin\mysql.exe"
)

# Find mysql.exe
$mysqlExe = $null
foreach ($path in $mampPaths) {
    if (Test-Path $path) {
        $mysqlExe = $path
        break
    }
}

# Try PATH if not found in common locations
if (-not $mysqlExe) {
    $mysqlExe = (Get-Command mysql.exe -ErrorAction SilentlyContinue).Source
}

if (-not $mysqlExe) {
    Write-Host "`n❌ ERROR: mysql.exe not found!" -ForegroundColor Red
    Write-Host "Please ensure MySQL client is installed or MAMP is installed at C:\MAMP" -ForegroundColor Yellow
    Write-Host "Searched locations:" -ForegroundColor Gray
    $mampPaths | ForEach-Object { Write-Host "  - $_" -ForegroundColor Gray }
    exit 1
}

Write-Host "`n=== MySQL Stored Procedure Deployment ===" -ForegroundColor Cyan
Write-Host "Using MySQL client: $mysqlExe" -ForegroundColor Gray
Write-Host "Target: $Database on ${Server}:${Port}`n" -ForegroundColor Gray

# Get all SQL files (excluding standards doc and reports)
$sqlFiles = Get-ChildItem -Path $proceduresFolder -Filter "*.sql" | 
    Where-Object { 
        $_.Name -ne "CurrentStoredProcedures.sql" -and 
        $_.Name -notlike "*REPORT*.sql" -and
        $_.Name -notlike "*UNUSED*.sql"
    } |
    Sort-Object Name

if ($sqlFiles.Count -eq 0) {
    Write-Host "❌ No stored procedure files found in: $proceduresFolder" -ForegroundColor Red
    exit 1
}

Write-Host "Found $($sqlFiles.Count) stored procedure files to deploy`n" -ForegroundColor Yellow

# Test connection first
Write-Host "Testing database connection..." -ForegroundColor Cyan
$testQuery = "SELECT VERSION();"
$testArgs = @(
    "-h", $Server,
    "-P", $Port,
    "-u", $User,
    "-p$Password",
    "-D", $Database,
    "-e", $testQuery
)

try {
    $versionOutput = & $mysqlExe $testArgs 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host "❌ Connection failed!" -ForegroundColor Red
        Write-Host $versionOutput -ForegroundColor Red
        exit 1
    }
    Write-Host "✅ Connected successfully" -ForegroundColor Green
    Write-Host "$versionOutput`n" -ForegroundColor Gray
} catch {
    Write-Host "❌ Connection error: $_" -ForegroundColor Red
    exit 1
}

# Deployment tracking
$results = @()
$successCount = 0
$failCount = 0
$skippedCount = 0

# Deploy each procedure
Write-Host "=== Deploying Stored Procedures ===" -ForegroundColor Cyan

foreach ($file in $sqlFiles) {
    $procName = $file.BaseName
    $filePath = $file.FullName
    
    Write-Host "`nDeploying: $procName" -ForegroundColor White
    
    if ($WhatIf) {
        Write-Host "  [WHATIF] Would deploy from: $($file.Name)" -ForegroundColor Magenta
        $skippedCount++
        continue
    }
    
    # Read the SQL file
    try {
        $sqlContent = Get-Content -Path $filePath -Raw -ErrorAction Stop
        
        # Check if file has actual content
        if ([string]::IsNullOrWhiteSpace($sqlContent)) {
            Write-Host "  ⚠️  SKIPPED: Empty file" -ForegroundColor Yellow
            $results += [PSCustomObject]@{
                Procedure = $procName
                Status = "SKIPPED"
                Message = "Empty file"
                File = $file.Name
            }
            $skippedCount++
            continue
        }
        
        # Keep DELIMITER statements - they're needed for mysql command-line source command
        # Just add DROP statement before the procedure
        $dropStmt = "DROP PROCEDURE IF EXISTS ``$procName``;"
        $sqlContent = $dropStmt + "`r`n" + $sqlContent
        
        # Save to temp file
        $tempFile = [System.IO.Path]::GetTempFileName()
        $tempSqlFile = [System.IO.Path]::ChangeExtension($tempFile, '.sql')
        Move-Item $tempFile $tempSqlFile -Force
        
        # Write SQL content with UTF8 encoding (no BOM)
        $utf8NoBom = New-Object System.Text.UTF8Encoding $false
        [System.IO.File]::WriteAllText($tempSqlFile, $sqlContent, $utf8NoBom)
        
        # Execute via mysql command line using source command
        $tempPathForward = $tempSqlFile.Replace('\', '/')
        $deployArgs = @(
            "--host=$Server",
            "--port=$Port",
            "--user=$User",
            "--password=$Password",
            "--database=$Database",
            "--execute=source $tempPathForward"
        )
        
        $output = & $mysqlExe $deployArgs 2>&1
        $exitCode = $LASTEXITCODE
        
        # Cleanup temp file
        Remove-Item $tempSqlFile -Force -ErrorAction SilentlyContinue
        
        if ($exitCode -eq 0) {
            Write-Host "  ✅ SUCCESS" -ForegroundColor Green
            $results += [PSCustomObject]@{
                Procedure = $procName
                Status = "SUCCESS"
                Message = "Deployed successfully"
                File = $file.Name
            }
            $successCount++
        } else {
            Write-Host "  ❌ FAILED" -ForegroundColor Red
            Write-Host "     Error: $output" -ForegroundColor Red
            $results += [PSCustomObject]@{
                Procedure = $procName
                Status = "FAILED"
                Message = $output
                File = $file.Name
            }
            $failCount++
        }
        
    } catch {
        Write-Host "  ❌ ERROR: $_" -ForegroundColor Red
        $results += [PSCustomObject]@{
            Procedure = $procName
            Status = "ERROR"
            Message = $_.Exception.Message
            File = $file.Name
        }
        $failCount++
    }
}

# Generate report
Write-Host "`n=== Deployment Summary ===" -ForegroundColor Cyan
Write-Host "Total procedures: $($sqlFiles.Count)" -ForegroundColor White
Write-Host "Successful: $successCount" -ForegroundColor Green
Write-Host "Failed: $failCount" -ForegroundColor Red
Write-Host "Skipped: $skippedCount" -ForegroundColor Yellow

# Create detailed report
$reportContent = @"
MySQL Stored Procedure Deployment Report
Generated: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")

Target Database: $Database
Server: ${Server}:${Port}
MySQL Client: $mysqlExe

=== Summary ===
Total procedures: $($sqlFiles.Count)
Successful deployments: $successCount
Failed deployments: $failCount
Skipped: $skippedCount

=== Deployment Details ===
"@

foreach ($result in $results) {
    $reportContent += "`n[$($result.Status)] $($result.Procedure)"
    if ($result.Status -ne "SUCCESS") {
        $reportContent += "`n    File: $($result.File)"
        $reportContent += "`n    Message: $($result.Message)"
    }
}

# Add list of successful procedures
if ($successCount -gt 0) {
    $reportContent += "`n`n=== Successfully Deployed Procedures ==="
    $results | Where-Object { $_.Status -eq "SUCCESS" } | ForEach-Object {
        $reportContent += "`n- $($_.Procedure)"
    }
}

# Add list of failed procedures
if ($failCount -gt 0) {
    $reportContent += "`n`n=== Failed Procedures ==="
    $results | Where-Object { $_.Status -eq "FAILED" -or $_.Status -eq "ERROR" } | ForEach-Object {
        $reportContent += "`n- $($_.Procedure)"
        $reportContent += "`n  Error: $($_.Message)"
    }
}

$reportContent | Out-File -FilePath $reportPath -Encoding UTF8

Write-Host "`nDeployment report saved to: $reportPath" -ForegroundColor Cyan

# Exit with error code if any failures
if ($failCount -gt 0) {
    Write-Host "`n⚠️  Deployment completed with errors!" -ForegroundColor Yellow
    exit 1
} else {
    Write-Host "`n✅ Deployment completed successfully!" -ForegroundColor Green
    exit 0
}
