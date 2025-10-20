# =============================================
# Deploy-Tables.ps1
# Purpose: Deploy table creation scripts to specified database
# Usage: .\Deploy-Tables.ps1 -Database "mtm_wip_application"
# =============================================

param(
    [Parameter(Mandatory=$false)]
    [string]$Database = "mtm_wip_application",
    
    [Parameter(Mandatory=$false)]
    [string]$Server = "localhost",
    
    [Parameter(Mandatory=$false)]
    [int]$Port = 3306,
    
    [Parameter(Mandatory=$false)]
    [string]$User = "root",
    
    [Parameter(Mandatory=$false)]
    [string]$Password = "root",
    
    [Parameter(Mandatory=$false)]
    [string]$TablePattern = "*.sql"
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Table Deployment Script" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Database: $Database" -ForegroundColor Yellow
Write-Host "Server: $Server" -ForegroundColor Yellow
Write-Host "Port: $Port" -ForegroundColor Yellow
Write-Host ""

# Check if MySqlConnector module is available
$mysqlPath = "C:\Program Files\MySQL\MySQL Server 5.7\bin\mysql.exe"
if (-not (Test-Path $mysqlPath)) {
    $mysqlPath = "C:\xampp\mysql\bin\mysql.exe"
}
if (-not (Test-Path $mysqlPath)) {
    $mysqlPath = "C:\mamp\bin\mysql\bin\mysql.exe"
}

if (-not (Test-Path $mysqlPath)) {
    Write-Host "ERROR: mysql.exe not found. Please install MySQL or update path." -ForegroundColor Red
    Write-Host "Falling back to .NET method..." -ForegroundColor Yellow
    $useDotNet = $true
} else {
    Write-Host "Using MySQL client: $mysqlPath" -ForegroundColor Green
    $useDotNet = $false
}

# Get all SQL files from UpdatedTables folder
$tablesPath = Join-Path $PSScriptRoot "..\Database\UpdatedTables"
$sqlFiles = Get-ChildItem -Path $tablesPath -Filter $TablePattern -File

Write-Host "Found $($sqlFiles.Count) SQL files to deploy" -ForegroundColor Cyan
Write-Host ""

$successCount = 0
$failedCount = 0
$failedFiles = @()

foreach ($file in $sqlFiles) {
    Write-Host "Deploying: $($file.Name)..." -NoNewline
    
    try {
        if ($useDotNet) {
            # Use .NET MySql.Data connector
            Add-Type -Path "C:\Program Files (x86)\MySQL\MySQL Connector Net 8.0.30\Assemblies\v4.5.2\MySql.Data.dll" -ErrorAction SilentlyContinue
            
            $connectionString = "Server=$Server;Port=$Port;Database=$Database;Uid=$User;Pwd=$Password;SslMode=none;AllowPublicKeyRetrieval=true;"
            $connection = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)
            $connection.Open()
            
            $sql = Get-Content $file.FullName -Raw
            $command = $connection.CreateCommand()
            $command.CommandText = $sql
            $command.ExecuteNonQuery() | Out-Null
            
            $connection.Close()
            Write-Host " SUCCESS" -ForegroundColor Green
            $successCount++
        } else {
            # Use mysql.exe command line
            $sql = Get-Content $file.FullName -Raw
            $tempFile = [System.IO.Path]::GetTempFileName()
            Set-Content -Path $tempFile -Value $sql -Encoding UTF8
            
            $arguments = @(
                "-h", $Server,
                "-P", $Port,
                "-u", $User,
                "-p$Password",
                $Database,
                "-e", "source $tempFile"
            )
            
            $process = Start-Process -FilePath $mysqlPath -ArgumentList $arguments -NoNewWindow -Wait -PassThru
            Remove-Item $tempFile -Force
            
            if ($process.ExitCode -eq 0) {
                Write-Host " SUCCESS" -ForegroundColor Green
                $successCount++
            } else {
                Write-Host " ERROR" -ForegroundColor Red
                $failedCount++
                $failedFiles += $file.Name
            }
        }
    }
    catch {
        Write-Host " ERROR" -ForegroundColor Red
        Write-Host "  $($_.Exception.Message)" -ForegroundColor Red
        $failedCount++
        $failedFiles += $file.Name
    }
}

Write-Host ""
Write-Host "=== Deployment Summary ===" -ForegroundColor Cyan
Write-Host "Total: $($sqlFiles.Count)" -ForegroundColor White
Write-Host "Success: $successCount" -ForegroundColor Green
Write-Host "Failed: $failedCount" -ForegroundColor $(if ($failedCount -eq 0) { "Green" } else { "Red" })

if ($failedFiles.Count -gt 0) {
    Write-Host ""
    Write-Host "Failed files:" -ForegroundColor Red
    $failedFiles | ForEach-Object { Write-Host "  - $_" -ForegroundColor Red }
}

Write-Host ""
