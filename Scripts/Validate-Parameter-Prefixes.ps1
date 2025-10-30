#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Validates MySQL stored procedure parameter naming conventions.

.DESCRIPTION
    Queries INFORMATION_SCHEMA.PARAMETERS to verify all stored procedures follow
    standard parameter prefix conventions (p_, in_, o_). Reports inconsistencies
    for database administrator review.

.PARAMETER ConnectionString
    MySQL connection string. Defaults to test database connection.

.PARAMETER Json
    Output results in JSON format instead of text.

.PARAMETER Verbose
    Show detailed parameter information for all procedures.

.EXAMPLE
    .\Validate-Parameter-Prefixes.ps1

.EXAMPLE
    .\Validate-Parameter-Prefixes.ps1 -ConnectionString "Server=localhost;Database=MTM_WIP_Application_Winforms;User=root;Password=root" -Json

.EXAMPLE
    .\Validate-Parameter-Prefixes.ps1 -Verbose
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory=$false)]
    [string]$ConnectionString = "Server=localhost;Port=3306;Database=mtm_wip_application_winform_test;User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true",

    [Parameter(Mandatory=$false)]
    [switch]$Json,

    [Parameter(Mandatory=$false)]
    [switch]$Verbose
)

$ErrorActionPreference = 'Stop'

# Load MySQL connector assembly
Add-Type -Path "C:\Program Files\MySQL\MySQL Connector NET 8.0.33\MySql.Data.dll" -ErrorAction SilentlyContinue

# Query to get all stored procedure parameters
$parameterQuery = @"
SELECT
    SPECIFIC_NAME as ProcedureName,
    PARAMETER_NAME as ParameterName,
    PARAMETER_MODE as ParameterMode,
    DATA_TYPE as DataType,
    ORDINAL_POSITION as Position
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_SCHEMA = DATABASE()
    AND PARAMETER_NAME IS NOT NULL
    AND PARAMETER_NAME != ''
ORDER BY SPECIFIC_NAME, ORDINAL_POSITION
"@

# Query to get stored procedure counts
$procedureCountQuery = @"
SELECT COUNT(*) as ProcCount
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_SCHEMA = DATABASE()
    AND ROUTINE_TYPE = 'PROCEDURE'
"@

$results = @{
    TotalProcedures = 0
    TotalParameters = 0
    ValidParameters = 0
    InvalidParameters = 0
    MissingStatusParameter = @()
    MissingErrorMsgParameter = @()
    InvalidPrefixParameters = @()
    AllParameters = @()
}

try {
    $connection = New-Object MySql.Data.MySqlClient.MySqlConnection($ConnectionString)
    $connection.Open()

    # Get procedure count
    $countCmd = New-Object MySql.Data.MySqlClient.MySqlCommand($procedureCountQuery, $connection)
    $results.TotalProcedures = [int]$countCmd.ExecuteScalar()

    # Get all parameters
    $cmd = New-Object MySql.Data.MySqlClient.MySqlCommand($parameterQuery, $connection)
    $reader = $cmd.ExecuteReader()

    $procParameters = @{}

    while ($reader.Read()) {
        $procName = $reader["ProcedureName"]
        $paramName = $reader["ParameterName"]
        $paramMode = $reader["ParameterMode"]
        $dataType = $reader["DataType"]
        $position = $reader["Position"]

        $results.TotalParameters++

        # Track parameters per procedure
        if (-not $procParameters.ContainsKey($procName)) {
            $procParameters[$procName] = @{
                HasStatus = $false
                HasErrorMsg = $false
                Parameters = @()
            }
        }

        # Check for required output parameters
        if ($paramName -eq "p_Status" -and $paramMode -eq "OUT") {
            $procParameters[$procName].HasStatus = $true
        }

        if ($paramName -eq "p_ErrorMsg" -and $paramMode -eq "OUT") {
            $procParameters[$procName].HasErrorMsg = $true
        }

        # Validate parameter prefix
        $isValid = $paramName.StartsWith("p_") -or
                   $paramName.StartsWith("in_") -or
                   $paramName.StartsWith("o_")

        if ($isValid) {
            $results.ValidParameters++
        } else {
            $results.InvalidParameters++
            $results.InvalidPrefixParameters += [PSCustomObject]@{
                Procedure = $procName
                Parameter = $paramName
                Mode = $paramMode
                DataType = $dataType
            }
        }

        # Store parameter details
        $paramDetail = [PSCustomObject]@{
            Procedure = $procName
            Parameter = $paramName
            Mode = $paramMode
            DataType = $dataType
            Position = $position
            HasValidPrefix = $isValid
        }

        $procParameters[$procName].Parameters += $paramDetail
        $results.AllParameters += $paramDetail
    }

    $reader.Close()

    # Check for missing required parameters
    foreach ($proc in $procParameters.Keys) {
        if (-not $procParameters[$proc].HasStatus) {
            $results.MissingStatusParameter += $proc
        }
        if (-not $procParameters[$proc].HasErrorMsg) {
            $results.MissingErrorMsgParameter += $proc
        }
    }

    $connection.Close()

    # Output results
    if ($Json) {
        $results | ConvertTo-Json -Depth 10
    } else {
        Write-Host "`n=== MySQL Stored Procedure Parameter Validation ===" -ForegroundColor Cyan
        Write-Host "Database: $($ConnectionString -replace 'Password=[^;]+', 'Password=***')" -ForegroundColor Gray
        Write-Host ""

        Write-Host "Summary:" -ForegroundColor Yellow
        Write-Host "  Total Procedures: $($results.TotalProcedures)"
        Write-Host "  Total Parameters: $($results.TotalParameters)"
        Write-Host "  Valid Parameters: $($results.ValidParameters)" -ForegroundColor Green
        Write-Host "  Invalid Parameters: $($results.InvalidParameters)" -ForegroundColor $(if ($results.InvalidParameters -eq 0) { "Green" } else { "Red" })
        Write-Host ""

        # Report missing p_Status parameters
        if ($results.MissingStatusParameter.Count -gt 0) {
            Write-Host "❌ Missing p_Status OUT parameter ($($results.MissingStatusParameter.Count) procedures):" -ForegroundColor Red
            foreach ($proc in $results.MissingStatusParameter) {
                Write-Host "   - $proc" -ForegroundColor Red
            }
            Write-Host ""
        } else {
            Write-Host "✓ All procedures have p_Status OUT parameter" -ForegroundColor Green
            Write-Host ""
        }

        # Report missing p_ErrorMsg parameters
        if ($results.MissingErrorMsgParameter.Count -gt 0) {
            Write-Host "❌ Missing p_ErrorMsg OUT parameter ($($results.MissingErrorMsgParameter.Count) procedures):" -ForegroundColor Red
            foreach ($proc in $results.MissingErrorMsgParameter) {
                Write-Host "   - $proc" -ForegroundColor Red
            }
            Write-Host ""
        } else {
            Write-Host "✓ All procedures have p_ErrorMsg OUT parameter" -ForegroundColor Green
            Write-Host ""
        }

        # Report invalid parameter prefixes
        if ($results.InvalidPrefixParameters.Count -gt 0) {
            Write-Host "❌ Invalid parameter prefixes ($($results.InvalidPrefixParameters.Count) parameters):" -ForegroundColor Red
            foreach ($param in $results.InvalidPrefixParameters) {
                Write-Host "   - $($param.Procedure).$($param.Parameter) ($($param.Mode) $($param.DataType))" -ForegroundColor Red
            }
            Write-Host ""
        } else {
            Write-Host "✓ All parameters use standard prefixes (p_, in_, o_)" -ForegroundColor Green
            Write-Host ""
        }

        # Verbose output
        if ($Verbose -and $results.AllParameters.Count -gt 0) {
            Write-Host "All Parameters:" -ForegroundColor Yellow
            $results.AllParameters | Format-Table -Property Procedure, Parameter, Mode, DataType, HasValidPrefix -AutoSize
        }

        # Final status
        $inconsistencyCount = $results.MissingStatusParameter.Count +
                             $results.MissingErrorMsgParameter.Count +
                             $results.InvalidPrefixParameters.Count

        Write-Host "=== Validation " -NoNewline
        if ($inconsistencyCount -eq 0) {
            Write-Host "PASSED" -ForegroundColor Green -NoNewline
            Write-Host " ===" -ForegroundColor Cyan
            Write-Host "No inconsistencies found. All stored procedures follow standard conventions." -ForegroundColor Green
        } else {
            Write-Host "FAILED" -ForegroundColor Red -NoNewline
            Write-Host " ===" -ForegroundColor Cyan
            Write-Host "Found $inconsistencyCount inconsistencies requiring correction." -ForegroundColor Red
        }
        Write-Host ""

        # Exit with appropriate code
        exit $inconsistencyCount
    }

} catch {
    Write-Error "Failed to validate stored procedure parameters: $_"
    exit 1
}
