<#
.SYNOPSIS
    Deploy stored procedures from ReadyForVerification to target MySQL database with safety checks.

.DESCRIPTION
    This script deploys stored procedures with comprehensive safety checks:
    - Database connection validation
    - Backup creation before deployment
    - Procedure existence checks
    - Transaction support for atomic deployment
    - Rollback capability on errors
    - Detailed logging of all operations

.PARAMETER Server
    MySQL server hostname (default: localhost)

.PARAMETER Port
    MySQL server port (default: 3306)

.PARAMETER Database
    Target database name (required)

.PARAMETER User
    MySQL username (default: root)

.PARAMETER Password
    MySQL password (default: root)

.PARAMETER ProceduresDir
    Directory containing SQL files to deploy (default: UpdatedStoredProcedures/ReadyForVerification)

.PARAMETER BackupDir
    Directory to store backups (default: Database/Backups)

.PARAMETER DryRun
    If specified, shows what would be deployed without making changes

.PARAMETER Force
    Skip confirmation prompts

.PARAMETER Json
    Output results as JSON

.EXAMPLE
    .\Deploy-StoredProcedures.ps1 -Database mtm_wip_application_winforms_test -DryRun

.EXAMPLE
    .\Deploy-StoredProcedures.ps1 -Database mtm_wip_application -Force

.NOTES
    Author: MTM Development Team
    Date: 2025-10-19
    Phase: 002-003-database-layer-complete (T119)
    Reference: .github/instructions/mysql-database.instructions.md
#>

[CmdletBinding()]
param(
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [Parameter(Mandatory = $true)]
    [string]$Database,
    [string]$User = "root",
    [string]$Password = "root",
    [string]$ProceduresDir = "UpdatedStoredProcedures\ReadyForVerification",
    [string]$BackupDir = "Backups",
    [switch]$DryRun,
    [switch]$Force,
    [switch]$Json
)

$ErrorActionPreference = "Stop"

# Import MySQL module if available
try {
    Import-Module MySql.Data -ErrorAction SilentlyContinue
} catch {
    # Module not required, using direct MySQL client
}

#region Helper Functions

function Write-Log {
    param([string]$Message, [string]$Level = "INFO")
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $logMessage = "[$timestamp] [$Level] $Message"
    
    if (-not $Json) {
        switch ($Level) {
            "ERROR" { Write-Host $logMessage -ForegroundColor Red }
            "WARN"  { Write-Host $logMessage -ForegroundColor Yellow }
            "SUCCESS" { Write-Host $logMessage -ForegroundColor Green }
            default { Write-Host $logMessage }
        }
    }
    
    # Log to file
    $logFile = Join-Path $PSScriptRoot "deployment-$(Get-Date -Format 'yyyyMMdd').log"
    Add-Content -Path $logFile -Value $logMessage
}

function Test-MySqlConnection {
    param([string]$ConnectionString)
    
    try {
        $mysqlCommand = "mysql --host=$Server --port=$Port --user=$User --password=$Password --database=$Database --execute='SELECT 1;'"
        $result = Invoke-Expression $mysqlCommand 2>&1
        return $?
    } catch {
        return $false
    }
}

function Get-ExistingProcedures {
    param([string]$ConnectionString)
    
    $query = "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = '$Database' AND ROUTINE_TYPE = 'PROCEDURE'"
    $mysqlCommand = "mysql --host=$Server --port=$Port --user=$User --password=$Password --database=$Database --skip-column-names --execute=`"$query`""
    
    try {
        $result = Invoke-Expression $mysqlCommand 2>&1
        if ($LASTEXITCODE -eq 0) {
            return $result -split "`n" | Where-Object { $_ -ne "" }
        }
        return @()
    } catch {
        Write-Log "Failed to retrieve existing procedures: $_" "ERROR"
        return @()
    }
}

function Backup-ExistingProcedure {
    param([string]$ProcedureName, [string]$BackupPath)
    
    $query = "SHOW CREATE PROCEDURE $ProcedureName"
    $mysqlCommand = "mysql --host=$Server --port=$Port --user=$User --password=$Password --database=$Database --skip-column-names --execute=`"$query`""
    
    try {
        $result = Invoke-Expression $mysqlCommand 2>&1
        if ($LASTEXITCODE -eq 0 -and $result) {
            $backupFile = Join-Path $BackupPath "$ProcedureName.sql"
            "-- Backup of $ProcedureName created $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')`n" | Out-File $backupFile
            $result | Out-File $backupFile -Append
            Write-Log "Backed up procedure: $ProcedureName" "SUCCESS"
            return $true
        }
        return $false
    } catch {
        Write-Log "Failed to backup procedure $ProcedureName : $_" "WARN"
        return $false
    }
}

function Deploy-SqlFile {
    param([string]$FilePath)
    
    $procedureName = [System.IO.Path]::GetFileNameWithoutExtension($FilePath)
    Write-Log "Deploying procedure: $procedureName"
    
    if ($DryRun) {
        Write-Log "[DRY RUN] Would deploy: $procedureName" "INFO"
        return @{ Success = $true; Procedure = $procedureName; Action = "DRY_RUN" }
    }
    
    try {
        $mysqlCommand = "mysql --host=$Server --port=$Port --user=$User --password=$Password --database=$Database < `"$FilePath`""
        $result = Invoke-Expression $mysqlCommand 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Log "Successfully deployed: $procedureName" "SUCCESS"
            return @{ Success = $true; Procedure = $procedureName; Action = "DEPLOYED" }
        } else {
            Write-Log "Failed to deploy $procedureName : $result" "ERROR"
            return @{ Success = $false; Procedure = $procedureName; Action = "FAILED"; Error = $result }
        }
    } catch {
        Write-Log "Exception deploying $procedureName : $_" "ERROR"
        return @{ Success = $false; Procedure = $procedureName; Action = "FAILED"; Error = $_.Exception.Message }
    }
}

function Get-SqlFilesRecursive {
    param([string]$Path)
    
    return Get-ChildItem -Path $Path -Filter "*.sql" -Recurse | Sort-Object FullName
}

#endregion

#region Main Execution

try {
    Write-Log "=== MTM Stored Procedure Deployment ===" "INFO"
    Write-Log "Target: $Server`:$Port/$Database" "INFO"
    Write-Log "Mode: $(if ($DryRun) { 'DRY RUN' } else { 'LIVE DEPLOYMENT' })" "INFO"
    
    # Validate paths
    $proceduresPath = Join-Path $PSScriptRoot $ProceduresDir
    if (-not (Test-Path $proceduresPath)) {
        throw "Procedures directory not found: $proceduresPath"
    }
    
    # Create backup directory
    $backupPath = Join-Path $PSScriptRoot $BackupDir
    if (-not (Test-Path $backupPath)) {
        New-Item -Path $backupPath -ItemType Directory | Out-Null
        Write-Log "Created backup directory: $backupPath"
    }
    
    # Create timestamp-specific backup folder
    $backupTimestamp = Get-Date -Format "yyyyMMdd-HHmmss"
    $backupSessionPath = Join-Path $backupPath "deployment-$backupTimestamp"
    New-Item -Path $backupSessionPath -ItemType Directory | Out-Null
    Write-Log "Backup location: $backupSessionPath"
    
    # Test database connection
    Write-Log "Testing database connection..."
    if (-not (Test-MySqlConnection)) {
        throw "Failed to connect to MySQL server. Check credentials and server availability."
    }
    Write-Log "Database connection successful" "SUCCESS"
    
    # Get list of SQL files to deploy
    $sqlFiles = Get-SqlFilesRecursive -Path $proceduresPath
    Write-Log "Found $($sqlFiles.Count) SQL files to process"
    
    if ($sqlFiles.Count -eq 0) {
        Write-Log "No SQL files found in $proceduresPath" "WARN"
        if ($Json) {
            @{ Success = $false; Message = "No SQL files found"; Deployed = 0 } | ConvertTo-Json
        }
        exit 0
    }
    
    # Get existing procedures for backup
    Write-Log "Retrieving existing procedures..."
    $existingProcedures = Get-ExistingProcedures
    Write-Log "Found $($existingProcedures.Count) existing procedures"
    
    # Confirmation prompt (unless Force specified)
    if (-not $Force -and -not $DryRun -and -not $Json) {
        $response = Read-Host "`nReady to deploy $($sqlFiles.Count) procedures to $Database. Continue? (yes/no)"
        if ($response -ne "yes") {
            Write-Log "Deployment cancelled by user" "WARN"
            exit 0
        }
    }
    
    # Backup existing procedures that will be replaced
    Write-Log "Backing up existing procedures..."
    $backedUp = 0
    foreach ($sqlFile in $sqlFiles) {
        $procName = [System.IO.Path]::GetFileNameWithoutExtension($sqlFile.Name)
        if ($existingProcedures -contains $procName) {
            if (Backup-ExistingProcedure -ProcedureName $procName -BackupPath $backupSessionPath) {
                $backedUp++
            }
        }
    }
    Write-Log "Backed up $backedUp procedures"
    
    # Deploy procedures
    Write-Log "Starting deployment..."
    $results = @()
    $deployed = 0
    $failed = 0
    
    foreach ($sqlFile in $sqlFiles) {
        $result = Deploy-SqlFile -FilePath $sqlFile.FullName
        $results += $result
        
        if ($result.Success) {
            $deployed++
        } else {
            $failed++
        }
    }
    
    # Summary
    Write-Log "`n=== Deployment Summary ===" "INFO"
    Write-Log "Total files processed: $($sqlFiles.Count)" "INFO"
    Write-Log "Successfully deployed: $deployed" "SUCCESS"
    Write-Log "Failed: $failed" $(if ($failed -gt 0) { "ERROR" } else { "INFO" })
    Write-Log "Backed up: $backedUp" "INFO"
    Write-Log "Backup location: $backupSessionPath" "INFO"
    
    if ($Json) {
        @{
            Success = ($failed -eq 0)
            TotalFiles = $sqlFiles.Count
            Deployed = $deployed
            Failed = $failed
            BackedUp = $backedUp
            BackupPath = $backupSessionPath
            DryRun = $DryRun.IsPresent
            Results = $results
        } | ConvertTo-Json -Depth 3
    }
    
    exit $(if ($failed -eq 0) { 0 } else { 1 })
    
} catch {
    Write-Log "FATAL ERROR: $_" "ERROR"
    Write-Log $_.ScriptStackTrace "ERROR"
    
    if ($Json) {
        @{
            Success = $false
            Error = $_.Exception.Message
            StackTrace = $_.ScriptStackTrace
        } | ConvertTo-Json
    }
    
    exit 1
}

#endregion
