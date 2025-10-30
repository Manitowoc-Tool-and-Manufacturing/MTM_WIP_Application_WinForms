#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Database Compatibility Check Script for MTM WIP Application

.DESCRIPTION
    This script checks the current MAMP MySQL database to identify:
    1. What stored procedures actually exist
    2. What the application is trying to call (from code analysis)
    3. Compatibility issues between code expectations and database reality

    The application should adapt to work with the existing database structure,
    not require database schema changes.

.PARAMETER DatabaseName
    Name of the database to check (default: MTM_WIP_Application_Winforms)

.PARAMETER OutputFormat
    Output format: Console, Json, or Report (default: Report)

.EXAMPLE
    .\Database-Compatibility-Check.ps1

.EXAMPLE
    .\Database-Compatibility-Check.ps1 -OutputFormat Json
#>

param(
    [string]$DatabaseName = "MTM_WIP_Application_Winforms",
    [ValidateSet("Console", "Json", "Report")]
    [string]$OutputFormat = "Report"
)

# MySQL connection settings for MAMP
$MySqlPath = "C:\MAMP\bin\mysql\bin\mysql.exe"
$Server = "localhost"
$Port = "3306"
$Username = "JOHNK"  # Using Windows username like the app does
$Password = ""       # App doesn't use password in connection string

# Application stored procedure calls (extracted from code analysis)
$ExpectedStoredProcedures = @(
    "app_themes_Get_All",
    "usr_ui_settings_GetSettingsJson_ByUserId",
    "usr_users_GetUserSetting_ByUserAndField",
    "usr_users_GetFullName_ByUser",
    "sys_GetUserAccessType",
    "md_part_ids_Get_All",
    "md_operation_numbers_Get_All",
    "md_locations_Get_All",
    "usr_users_Get_All",
    "md_item_types_Get_All",
    "log_changelog_Get_Current",
    "sys_last_10_transactions_Get_ByUser"
)

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

function Test-MySqlConnection {
    Write-Log "Testing MySQL connection to MAMP server..."

    if (-not (Test-Path $MySqlPath)) {
        Write-Log "MySQL executable not found at: $MySqlPath" "ERROR"
        return $false
    }

    try {
        $testQuery = "SELECT 1 as test;"

        # Try different connection methods based on password
        if ([string]::IsNullOrEmpty($Password)) {
            Write-Log "Attempting connection without password (Windows Auth)..."
            $result = & $MySqlPath -h $Server -P $Port -u $Username -D $DatabaseName -e $testQuery 2>&1
        } else {
            Write-Log "Attempting connection with password..."
            $result = & $MySqlPath -h $Server -P $Port -u $Username -p$Password -D $DatabaseName -e $testQuery 2>&1
        }

        if ($LASTEXITCODE -eq 0) {
            Write-Log "Database connection successful" "SUCCESS"
            return $true
        } else {
            Write-Log "Database connection failed: $result" "ERROR"

            # Try with root/root as fallback
            Write-Log "Trying fallback connection with root/root..."
            $fallbackResult = & $MySqlPath -h $Server -P $Port -u "root" -proot -D $DatabaseName -e $testQuery 2>&1

            if ($LASTEXITCODE -eq 0) {
                Write-Log "Fallback connection successful" "SUCCESS"
                # Update variables for subsequent calls
                $script:Username = "root"
                $script:Password = "root"
                return $true
            } else {
                Write-Log "Fallback connection also failed: $fallbackResult" "ERROR"
                return $false
            }
        }
    }
    catch {
        Write-Log "Database connection error: $($_.Exception.Message)" "ERROR"
        return $false
    }
}

function Get-ExistingStoredProcedures {
    Write-Log "Querying existing stored procedures..."

    $query = @"
SELECT
    ROUTINE_NAME as ProcedureName,
    ROUTINE_TYPE as Type,
    CREATED as CreatedDate,
    LAST_ALTERED as LastModified
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_SCHEMA = '$DatabaseName'
    AND ROUTINE_TYPE = 'PROCEDURE'
ORDER BY ROUTINE_NAME;
"@

    try {
        if ([string]::IsNullOrEmpty($Password)) {
            $result = & $MySqlPath -h $Server -P $Port -u $Username -D $DatabaseName -e $query 2>&1
        } else {
            $result = & $MySqlPath -h $Server -P $Port -u $Username -p$Password -D $DatabaseName -e $query 2>&1
        }

        if ($LASTEXITCODE -eq 0) {
            # Parse the output (skip header line)
            $procedures = @()
            $lines = ($result -split "`n") | Where-Object { $_ -and $_ -notmatch "^ProcedureName" }

            foreach ($line in $lines) {
                if ($line.Trim()) {
                    $columns = $line -split "`t"
                    if ($columns.Count -ge 4) {
                        $procedures += [PSCustomObject]@{
                            Name = $columns[0].Trim()
                            Type = $columns[1].Trim()
                            Created = $columns[2].Trim()
                            LastModified = $columns[3].Trim()
                        }
                    }
                }
            }

            Write-Log "Found $($procedures.Count) stored procedures in database" "SUCCESS"
            return $procedures
        } else {
            Write-Log "Failed to query stored procedures: $result" "ERROR"
            return @()
        }
    }
    catch {
        Write-Log "Error querying stored procedures: $($_.Exception.Message)" "ERROR"
        return @()
    }
}

function Compare-DatabaseCompatibility {
    param(
        [array]$ExistingProcedures,
        [array]$ExpectedProcedures
    )

    Write-Log "Analyzing database compatibility..."

    $existingNames = $ExistingProcedures | ForEach-Object { $_.Name }

    $compatibility = [PSCustomObject]@{
        ExistingCount = $ExistingProcedures.Count
        ExpectedCount = $ExpectedProcedures.Count
        MissingProcedures = @()
        ExtraProcedures = @()
        MatchingProcedures = @()
        CompatibilityScore = 0
    }

    # Find missing procedures (expected but not in database)
    foreach ($expected in $ExpectedProcedures) {
        if ($expected -in $existingNames) {
            $compatibility.MatchingProcedures += $expected
        } else {
            $compatibility.MissingProcedures += $expected
        }
    }

    # Find extra procedures (in database but not expected)
    foreach ($existing in $existingNames) {
        if ($existing -notin $ExpectedProcedures) {
            $compatibility.ExtraProcedures += $existing
        }
    }

    # Calculate compatibility score
    if ($ExpectedProcedures.Count -gt 0) {
        $compatibility.CompatibilityScore = [math]::Round(
            ($compatibility.MatchingProcedures.Count / $ExpectedProcedures.Count) * 100, 2
        )
    }

    return $compatibility
}

function Write-CompatibilityReport {
    param(
        [PSCustomObject]$Compatibility,
        [array]$ExistingProcedures
    )

    Write-Host "`n" -NoNewline
    Write-Host "="*80 -ForegroundColor Cyan
    Write-Host "MTM WIP APPLICATION - DATABASE COMPATIBILITY REPORT" -ForegroundColor Cyan
    Write-Host "="*80 -ForegroundColor Cyan
    Write-Host "Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')" -ForegroundColor Gray
    Write-Host "Database: $DatabaseName on $Server`:$Port" -ForegroundColor Gray
    Write-Host ""

    # Summary
    Write-Host "COMPATIBILITY SUMMARY" -ForegroundColor Yellow
    Write-Host "-"*40 -ForegroundColor Yellow
    Write-Host "Compatibility Score: " -NoNewline

    $scoreColor = if ($Compatibility.CompatibilityScore -ge 90) { "Green" }
                  elseif ($Compatibility.CompatibilityScore -ge 70) { "Yellow" }
                  else { "Red" }
    Write-Host "$($Compatibility.CompatibilityScore)%" -ForegroundColor $scoreColor

    Write-Host "Procedures in Database: $($Compatibility.ExistingCount)"
    Write-Host "Procedures Expected by App: $($Compatibility.ExpectedCount)"
    Write-Host "Matching Procedures: $($Compatibility.MatchingProcedures.Count)"
    Write-Host "Missing Procedures: $($Compatibility.MissingProcedures.Count)"
    Write-Host ""

    # Missing procedures (critical)
    if ($Compatibility.MissingProcedures.Count -gt 0) {
        Write-Host "MISSING PROCEDURES (App will fail)" -ForegroundColor Red
        Write-Host "-"*40 -ForegroundColor Red
        foreach ($missing in $Compatibility.MissingProcedures) {
            Write-Host "  ❌ $missing" -ForegroundColor Red
        }
        Write-Host ""
    }

    # Matching procedures (good)
    if ($Compatibility.MatchingProcedures.Count -gt 0) {
        Write-Host "MATCHING PROCEDURES (App compatible)" -ForegroundColor Green
        Write-Host "-"*40 -ForegroundColor Green
        foreach ($matching in $Compatibility.MatchingProcedures) {
            Write-Host "  ✅ $matching" -ForegroundColor Green
        }
        Write-Host ""
    }

    # All existing procedures
    Write-Host "ALL PROCEDURES IN DATABASE" -ForegroundColor Cyan
    Write-Host "-"*40 -ForegroundColor Cyan
    if ($ExistingProcedures.Count -gt 0) {
        foreach ($proc in $ExistingProcedures | Sort-Object Name) {
            $status = if ($proc.Name -in $Compatibility.MatchingProcedures) { "✅" } else { "ℹ️" }
            Write-Host "  $status $($proc.Name)" -ForegroundColor $(if ($proc.Name -in $Compatibility.MatchingProcedures) { "Green" } else { "Gray" })
        }
    } else {
        Write-Host "  No procedures found in database" -ForegroundColor Red
    }
    Write-Host ""

    # Recommendations
    Write-Host "RECOMMENDATIONS" -ForegroundColor Yellow
    Write-Host "-"*40 -ForegroundColor Yellow

    if ($Compatibility.CompatibilityScore -lt 100) {
        Write-Host "1. The application code needs to be updated to work with the current database" -ForegroundColor Yellow
        Write-Host "2. Focus on these missing procedures:" -ForegroundColor Yellow
        foreach ($missing in $Compatibility.MissingProcedures) {
            Write-Host "   - Check if '$missing' can be replaced with an existing equivalent" -ForegroundColor Yellow
        }
        Write-Host "3. Consider implementing fallback logic for missing procedures" -ForegroundColor Yellow
    } else {
        Write-Host "Database is fully compatible with application requirements!" -ForegroundColor Green
    }

    Write-Host ""
    Write-Host "="*80 -ForegroundColor Cyan
}

# Main execution
function Main {
    Write-Log "Starting MTM Database Compatibility Check" "SUCCESS"
    Write-Log "Target Database: $DatabaseName"

    # Test connection
    if (-not (Test-MySqlConnection)) {
        Write-Log "Cannot proceed without database connection" "ERROR"
        exit 1
    }

    # Get existing procedures
    $existingProcedures = Get-ExistingStoredProcedures

    if ($existingProcedures.Count -eq 0) {
        Write-Log "No stored procedures found - this may indicate a problem" "WARN"
    }

    # Analyze compatibility
    $compatibility = Compare-DatabaseCompatibility -ExistingProcedures $existingProcedures -ExpectedProcedures $ExpectedStoredProcedures

    # Output results
    switch ($OutputFormat) {
        "Json" {
            $output = @{
                DatabaseName = $DatabaseName
                Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
                Compatibility = $compatibility
                ExistingProcedures = $existingProcedures
            }
            $output | ConvertTo-Json -Depth 10
        }
        "Console" {
            Write-Host "Compatibility Score: $($compatibility.CompatibilityScore)%"
            Write-Host "Missing: $($compatibility.MissingProcedures -join ', ')"
            Write-Host "Existing: $($existingProcedures.Name -join ', ')"
        }
        "Report" {
            Write-CompatibilityReport -Compatibility $compatibility -ExistingProcedures $existingProcedures
        }
    }

    # Return appropriate exit code
    if ($compatibility.CompatibilityScore -lt 70) {
        Write-Log "Compatibility check completed with issues" "WARN"
        return 2
    } elseif ($compatibility.CompatibilityScore -lt 100) {
        Write-Log "Compatibility check completed with minor issues" "WARN"
        return 1
    } else {
        Write-Log "Compatibility check completed successfully" "SUCCESS"
        return 0
    }
}

# Execute main function and capture return code
$exitCode = Main
exit $exitCode
