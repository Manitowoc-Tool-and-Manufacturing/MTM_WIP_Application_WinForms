<#
.SYNOPSIS
    Simple PowerShell-compatible SQL deployment script

.DESCRIPTION
    Deploys SQL files using PowerShell pipeline instead of shell redirection
#>

param(
    [string]$Database = "mtm_wip_application_winforms_test",
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$User = "root",
    [string]$Password = "root"
)

# Add MySQL to PATH
$env:PATH = "C:\MAMP\bin\mysql\bin;$env:PATH"

# Get all SQL files recursively
$sqlFiles = Get-ChildItem -Path "UpdatedStoredProcedures\ReadyForVerification" -Filter "*.sql" -Recurse

Write-Host "Found $($sqlFiles.Count) SQL files to deploy" -ForegroundColor Cyan

$successCount = 0
$failCount = 0

foreach ($file in $sqlFiles) {
    Write-Host "Deploying: $($file.Name)..." -NoNewline
    
    try {
        # Read SQL file content
        $sqlContent = Get-Content -Path $file.FullName -Raw
        
        # Execute via pipeline
        $result = $sqlContent | & mysql --host=$Server --port=$Port --user=$User --password=$Password --database=$Database 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host " SUCCESS" -ForegroundColor Green
            $successCount++
        } else {
            Write-Host " FAILED" -ForegroundColor Red
            Write-Host "  Error: $result" -ForegroundColor Yellow
            $failCount++
        }
    }
    catch {
        Write-Host " EXCEPTION" -ForegroundColor Red
        Write-Host "  Error: $_" -ForegroundColor Yellow
        $failCount++
    }
}

Write-Host "`n=== Deployment Summary ===" -ForegroundColor Cyan
Write-Host "Total: $($sqlFiles.Count)" -ForegroundColor White
Write-Host "Success: $successCount" -ForegroundColor Green
Write-Host "Failed: $failCount" -ForegroundColor Red
