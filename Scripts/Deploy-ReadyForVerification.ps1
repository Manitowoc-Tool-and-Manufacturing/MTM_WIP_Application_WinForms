<#
.SYNOPSIS
    Deploys stored procedures from ReadyForVerification folder to MySQL server

.DESCRIPTION
    This script reads all .sql files from the ReadyForVerification folder and
    executes them against the specified MySQL database server.

.PARAMETER Server
    MySQL server hostname or IP address (default: localhost)

.PARAMETER Port
    MySQL server port (default: 3306)

.PARAMETER Database
    Target database name (default: MTM_WIP_Application_Winforms)

.PARAMETER User
    MySQL username (default: root)

.PARAMETER Password
    MySQL password (default: root)

.PARAMETER SourcePath
    Path to stored procedures folder (default: Database\CurrentStoredProcedures\ReadyForVerification)

.PARAMETER DryRun
    Shows what would be deployed without actually executing

.EXAMPLE
    .\Scripts\Deploy-ReadyForVerification.ps1
    Deploy to localhost with default settings

.EXAMPLE
    .\Scripts\Deploy-ReadyForVerification.ps1 -Server 172.16.1.104 -Database mtm_wip_application_winforms_test
    Deploy to remote test server

.EXAMPLE
    .\Scripts\Deploy-ReadyForVerification.ps1 -DryRun
    Preview what would be deployed
#>

[CmdletBinding()]
param(
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$Database = "MTM_WIP_Application_Winforms",
    [string]$User = "root",
    [string]$Password = "root",
    [string]$SourcePath = "Database\CurrentStoredProcedures\ReadyForVerification",
    [string]$MysqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe",
    [switch]$DryRun
)

# Color output functions
function Write-Success { param([string]$Message) Write-Host $Message -ForegroundColor Green }
function Write-Info { param([string]$Message) Write-Host $Message -ForegroundColor Cyan }
function Write-Warn { param([string]$Message) Write-Host $Message -ForegroundColor Yellow }
function Write-Err { param([string]$Message) Write-Host $Message -ForegroundColor Red }

# Main deployment logic
function Deploy-StoredProcedures {
    Write-Info "`n=== ReadyForVerification Deployment ==="
    Write-Host "Server: $Server`:$Port" -ForegroundColor White
    Write-Host "Database: $Database" -ForegroundColor White
    Write-Host "Source: $SourcePath" -ForegroundColor White

    if ($DryRun) {
        Write-Warn "`n⚠️  DRY RUN MODE - No changes will be made`n"
    }

    # Verify source path exists
    if (-not (Test-Path $SourcePath)) {
        Write-Err "❌ Source path not found: $SourcePath"
        return $false
    }

    # Get all SQL files
    $sqlFiles = Get-ChildItem $SourcePath -Filter "*.sql" | Sort-Object Name

    if ($sqlFiles.Count -eq 0) {
        Write-Warn "⚠️  No SQL files found in $SourcePath"
        return $false
    }

    Write-Info "`nFound $($sqlFiles.Count) stored procedure(s) to deploy`n"

    # Deployment statistics
    $deployed = 0
    $failed = 0
    $skipped = 0
    $deployErrors = @()

    foreach ($file in $sqlFiles) {
        $procName = $file.BaseName

        if ($DryRun) {
            Write-Host "  [DRYRUN] Would deploy: $procName" -ForegroundColor Gray
            $skipped++
            continue
        }

        Write-Host "  Deploying: " -NoNewline
        Write-Host $procName -ForegroundColor White -NoNewline
        Write-Host "..." -NoNewline

        try {
            # Read the SQL file content
            $sqlContent = Get-Content $file.FullName -Raw

            # Handle DELIMITER $$ syntax
            # Remove DELIMITER statements and replace $$ with semicolons
            $sqlContent = $sqlContent -replace "DELIMITER \$\$", ""
            $sqlContent = $sqlContent -replace "DELIMITER ;", ""
            $sqlContent = $sqlContent -replace "\$\$", ";"

            # Build mysql command - execute SQL content directly
            $mysqlCmd = $MysqlPath
            $mysqlArgs = @(
                "--host=$Server",
                "--port=$Port",
                "--user=$User",
                "--password=$Password",
                "--database=$Database"
            )

            # Execute via mysql command-line (pipe SQL content to stdin)
            $output = $sqlContent | & $mysqlCmd @mysqlArgs 2>&1

            # Check for actual errors (ignore password warning)
            $hasError = $false
            if ($output) {
                $errorLines = $output | Where-Object { $_ -is [System.Management.Automation.ErrorRecord] -or ($_ -match "ERROR") }
                if ($errorLines) {
                    $hasError = $true
                }
            }

            if (-not $hasError -and $LASTEXITCODE -eq 0) {
                Write-Success " ✓"
                $deployed++
            }
            else {
                Write-Err " ✗"
                $failed++
                $errorMsg = if ($output) { ($output | Out-String).Trim() } else { "MySQL exit code: $LASTEXITCODE" }
                $deployErrors += @{
                    Procedure = $procName
                    ErrorMsg = $errorMsg
                }
            }
        }
        catch {
            Write-Err " ✗"
            $failed++
            $deployErrors += @{
                Procedure = $procName
                ErrorMsg = $_.Exception.Message
            }
        }
    }

    # Summary
    Write-Info "`n=== Deployment Summary ==="
    Write-Host "Total Procedures: " -NoNewline; Write-Host $sqlFiles.Count -ForegroundColor White

    if ($DryRun) {
        Write-Host "Would Deploy: " -NoNewline; Write-Host $skipped -ForegroundColor Yellow
    }
    else {
        Write-Host "Deployed: " -NoNewline; Write-Host $deployed -ForegroundColor Green

        if ($failed -gt 0) {
            Write-Host "Failed: " -NoNewline; Write-Host $failed -ForegroundColor Red

            Write-Info "`n=== Deployment Errors ==="
            foreach ($deployError in $deployErrors) {
                Write-Err "❌ $($deployError.Procedure)"
                Write-Host "   $($deployError.ErrorMsg)" -ForegroundColor DarkRed
            }
        }
    }

    if (-not $DryRun -and $failed -eq 0) {
        Write-Success "`n✅ All stored procedures deployed successfully!"
        return $true
    }
    elseif (-not $DryRun -and $failed -gt 0) {
        Write-Warn "`n⚠️  Deployment completed with errors"
        return $false
    }
    else {
        Write-Info "`n✅ Dry run preview completed"
        return $true
    }
}

# Execute deployment
$success = Deploy-StoredProcedures

if (-not $success) {
    exit 1
}

exit 0
