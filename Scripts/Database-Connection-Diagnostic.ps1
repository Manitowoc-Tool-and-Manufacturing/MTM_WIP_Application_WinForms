#!/usr/bin/env pwsh
<#
.SYNOPSIS
    MTM Application Database Connection Diagnostic
    
.DESCRIPTION
    This script tests the exact connection method and parameters that the MTM application uses
    to identify why the stored procedure calls are failing with MySqlException.
    
    Based on the code analysis, the app uses:
    - Server: localhost (or 172.16.1.104 in production)
    - Database: mtm_wip_application_winforms_test (Debug) or mtm_wip_application (Release)
    - Username: Environment.UserName (JOHNK)
    - No password in connection string
    - Allow User Variables=True
#>

# MySQL connection settings matching the application
$MySqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe"
$Server = "localhost"
$Port = "3306"
$DatabaseDebug = "mtm_wip_application_winforms_test"
$DatabaseRelease = "mtm_wip_application"
$Username = $env:USERNAME.ToUpper()

function Write-Log {
    param([string]$Message, [string]$Level = "INFO")
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Host "[$timestamp] [$Level] $Message" -ForegroundColor $(
        switch ($Level) {
            "ERROR" { "Red" }
            "WARN" { "Yellow" }
            "SUCCESS" { "Green" }
            default { "White" }
        }
    )
}

function Test-StoredProcedureCall {
    param(
        [string]$Database,
        [string]$ProcedureName,
        [string]$Parameters = ""
    )
    
    Write-Log "Testing stored procedure: $ProcedureName in $Database"
    
    $query = if ([string]::IsNullOrEmpty($Parameters)) {
        "CALL $ProcedureName();"
    } else {
        "CALL $ProcedureName($Parameters);"
    }
    
    try {
        $result = & $MySqlPath -h $Server -P $Port -u $Username -D $Database -e $query 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Log "✅ $ProcedureName executed successfully" "SUCCESS"
            
            # Show first few lines of output if any
            if ($result -and $result.Count -gt 0) {
                $outputLines = ($result | Select-Object -First 3) -join "`n"
                Write-Log "   Output preview: $outputLines" "INFO"
            }
            return $true
        } else {
            Write-Log "❌ $ProcedureName failed: $result" "ERROR"
            return $false
        }
    }
    catch {
        Write-Log "❌ Exception calling $ProcedureName : $($_.Exception.Message)" "ERROR"
        return $false
    }
}

function Test-ApplicationConnection {
    Write-Log "Testing MTM Application Database Connection Pattern" "SUCCESS"
    Write-Log "=" * 60
    
    # Test basic connection info
    Write-Log "Connection Parameters:"
    Write-Log "  Server: $Server"
    Write-Log "  Port: $Port"
    Write-Log "  Username: $Username"
    Write-Log "  No Password (matching app behavior)"
    Write-Log ""
    
    # Test both databases
    $databases = @($DatabaseDebug, $DatabaseRelease)
    $failingProcedures = @(
        "app_themes_Get_All",
        "usr_ui_settings_GetSettingsJson_ByUserId", 
        "usr_users_GetUserSetting_ByUserAndField",
        "usr_users_GetFullName_ByUser",
        "sys_GetUserAccessType"
    )
    
    foreach ($db in $databases) {
        Write-Log "Testing database: $db" "INFO"
        Write-Log "-" * 40
        
        # Test basic connection
        $testQuery = "SELECT 1 as connection_test;"
        $result = & $MySqlPath -h $Server -P $Port -u $Username -D $db -e $testQuery 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Log "✅ Basic connection to $db successful" "SUCCESS"
            
            # Test the specific stored procedures that are failing
            $successCount = 0
            foreach ($proc in $failingProcedures) {
                switch ($proc) {
                    "usr_users_GetFullName_ByUser" {
                        if (Test-StoredProcedureCall -Database $db -ProcedureName $proc -Parameters "'$Username'") {
                            $successCount++
                        }
                    }
                    "usr_ui_settings_GetSettingsJson_ByUserId" {
                        if (Test-StoredProcedureCall -Database $db -ProcedureName $proc -Parameters "'Theme_Name', '$Username'") {
                            $successCount++
                        }
                    }
                    "usr_users_GetUserSetting_ByUserAndField" {
                        if (Test-StoredProcedureCall -Database $db -ProcedureName $proc -Parameters "'$Username', 'Theme_Name'") {
                            $successCount++
                        }
                    }
                    default {
                        if (Test-StoredProcedureCall -Database $db -ProcedureName $proc) {
                            $successCount++
                        }
                    }
                }
            }
            
            Write-Log ""
            Write-Log "Summary for $db :"
            Write-Log "  Successful procedures: $successCount / $($failingProcedures.Count)"
            
            if ($successCount -eq $failingProcedures.Count) {
                Write-Log "  🎉 All procedures work correctly!" "SUCCESS"
            } else {
                Write-Log "  ⚠️  Some procedures failed - this matches your app's behavior" "WARN"
            }
            
        } else {
            Write-Log "❌ Cannot connect to database $db : $result" "ERROR"
        }
        
        Write-Log ""
    }
    
    # Additional diagnostics
    Write-Log "Additional Diagnostics:" "INFO"
    Write-Log "-" * 40
    
    # Check if MAMP is running
    $mampProcesses = Get-Process | Where-Object { $_.ProcessName -like "*mysql*" -or $_.ProcessName -like "*mamp*" }
    if ($mampProcesses) {
        Write-Log "✅ MAMP/MySQL processes are running:" "SUCCESS"
        foreach ($proc in $mampProcesses) {
            Write-Log "   - $($proc.ProcessName) (PID: $($proc.Id))"
        }
    } else {
        Write-Log "❌ No MAMP/MySQL processes found running" "ERROR"
    }
    
    # Test the exact connection string pattern the app uses
    Write-Log ""
    Write-Log "Testing connection string pattern used by application:" "INFO"
    Write-Log "  Pattern: SERVER=$Server;DATABASE=<db>;UID=$Username;Allow User Variables=True;" "INFO"
    
    # MySql.Data.dll connection test simulation
    Write-Log ""
    Write-Log "NOTE: The application uses MySql.Data.dll connector, not command-line mysql" "WARN"
    Write-Log "Command-line success doesn't guarantee MySql.Data.dll success" "WARN"
}

# Main execution
Write-Log "MTM WIP Application - Database Connection Diagnostics" "SUCCESS"
Write-Log "Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
Write-Log ""

# Check if MySQL executable exists
if (-not (Test-Path $MySqlPath)) {
    Write-Log "❌ MySQL executable not found at: $MySqlPath" "ERROR"
    Write-Log "Please verify MAMP installation" "ERROR"
    exit 1
}

Test-ApplicationConnection

Write-Log ""
Write-Log "Diagnostic completed. Compare these results with your application's behavior." "SUCCESS"