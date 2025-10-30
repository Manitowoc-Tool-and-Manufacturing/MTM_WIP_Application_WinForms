<#
.SYNOPSIS
    Extract complete database schema using mysql CLI tool
.DESCRIPTION
    T101 Deliverable: Extract INFORMATION_SCHEMA metadata for stored procedures, parameters, tables, and columns
    Uses mysql CLI tool for maximum compatibility and reliability
.PARAMETER Server
    MySQL server hostname (default: localhost)
.PARAMETER Port
    MySQL server port (default: 3306)
.PARAMETER Database
    Target database name (default: MTM_WIP_Application_Winforms)
.PARAMETER User
    MySQL username (default: root)
.PARAMETER Password
    MySQL password as SecureString (default: root)
.OUTPUTS
    database-schema-snapshot.json - Complete schema metadata
#>

[CmdletBinding()]
param(
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$Database = "MTM_WIP_Application_Winforms",
    [string]$User = "root",
    [SecureString]$Password = (ConvertTo-SecureString "root" -AsPlainText -Force)
)

# Convert SecureString to plain text for mysql CLI
$BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password)
$PlainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)
[System.Runtime.InteropServices.Marshal]::ZeroFreeBSTR($BSTR)

Write-Host "=== T101: Database Schema Extraction (mysql CLI) ===" -ForegroundColor Cyan
Write-Host "Target: ${Server}:${Port}/${Database}" -ForegroundColor Gray

# Locate mysql CLI (try PATH first, then common locations)
$mysqlPath = $null
try {
    $mysqlPath = (Get-Command mysql -ErrorAction Stop).Source
    Write-Host "✓ mysql CLI found in PATH: $mysqlPath" -ForegroundColor Green
} catch {
    # Try common MAMP location
    $mampPath = "C:\MAMP\bin\mysql\bin\mysql.exe"
    if (Test-Path $mampPath) {
        $mysqlPath = $mampPath
        Write-Host "✓ mysql CLI found: $mysqlPath" -ForegroundColor Green
    } else {
        Write-Host "✗ mysql CLI not found in PATH or C:\MAMP\bin\mysql\bin" -ForegroundColor Red
        Write-Host "  Install MySQL client or add mysql.exe to PATH" -ForegroundColor Yellow
        exit 1
    }
}

# Helper function to execute mysql query and parse JSON output
function Invoke-MySqlCliQuery {
    param(
        [string]$Query,
        [string]$Description
    )

    Write-Host "Executing: $Description..." -ForegroundColor Gray

    try {
        # Execute query with JSON output format
        $mysqlArgs = @(
            "-h", $Server,
            "-P", $Port,
            "-u", $User,
            "-p$PlainPassword",
            $Database,
            "--batch",
            "--skip-column-names",
            "-e", $Query
        )

        $output = & $mysqlPath @mysqlArgs 2>&1

        if ($LASTEXITCODE -ne 0) {
            Write-Host "  ✗ Query failed: $output" -ForegroundColor Red
            return @()
        }

        # Parse tab-delimited output into objects
        $lines = $output -split "`n" | Where-Object { $_.Trim() -ne "" }

        if ($lines.Count -eq 0) {
            Write-Host "  ⚠ No results" -ForegroundColor Yellow
            return @()
        }

        Write-Host "  ✓ Retrieved $($lines.Count) rows" -ForegroundColor Green
        return $lines

    } catch {
        Write-Host "  ✗ Error: $_" -ForegroundColor Red
        return @()
    }
}

# Query 1: INFORMATION_SCHEMA.ROUTINES (Stored Procedures)
Write-Host "`n[1/4] Extracting stored procedures..." -ForegroundColor Cyan
$routinesQuery = @"
SELECT
    ROUTINE_SCHEMA,
    ROUTINE_NAME,
    ROUTINE_TYPE,
    DATA_TYPE,
    DTD_IDENTIFIER,
    CREATED,
    LAST_ALTERED,
    SQL_MODE,
    ROUTINE_COMMENT,
    DEFINER,
    CHARACTER_SET_CLIENT,
    COLLATION_CONNECTION,
    DATABASE_COLLATION
FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_SCHEMA = '$Database'
ORDER BY ROUTINE_NAME;
"@

$routinesRaw = Invoke-MySqlCliQuery -Query $routinesQuery -Description "ROUTINES query"

# Parse routines into objects
$routines = @()
foreach ($line in $routinesRaw) {
    $fields = $line -split "`t"
    if ($fields.Count -ge 13) {
        $routines += [PSCustomObject]@{
            ROUTINE_SCHEMA = $fields[0]
            ROUTINE_NAME = $fields[1]
            ROUTINE_TYPE = $fields[2]
            DATA_TYPE = $fields[3]
            DTD_IDENTIFIER = $fields[4]
            CREATED = $fields[5]
            LAST_ALTERED = $fields[6]
            SQL_MODE = $fields[7]
            ROUTINE_COMMENT = $fields[8]
            DEFINER = $fields[9]
            CHARACTER_SET_CLIENT = $fields[10]
            COLLATION_CONNECTION = $fields[11]
            DATABASE_COLLATION = $fields[12]
        }
    }
}

# Query 2: INFORMATION_SCHEMA.PARAMETERS (Procedure Parameters)
Write-Host "`n[2/4] Extracting procedure parameters..." -ForegroundColor Cyan
$parametersQuery = @"
SELECT
    SPECIFIC_SCHEMA,
    SPECIFIC_NAME,
    ORDINAL_POSITION,
    PARAMETER_MODE,
    PARAMETER_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    NUMERIC_PRECISION,
    NUMERIC_SCALE,
    DTD_IDENTIFIER
FROM INFORMATION_SCHEMA.PARAMETERS
WHERE SPECIFIC_SCHEMA = '$Database'
  AND PARAMETER_NAME IS NOT NULL
ORDER BY SPECIFIC_NAME, ORDINAL_POSITION;
"@

$parametersRaw = Invoke-MySqlCliQuery -Query $parametersQuery -Description "PARAMETERS query"

# Parse parameters into objects
$parameters = @()
foreach ($line in $parametersRaw) {
    $fields = $line -split "`t"
    if ($fields.Count -ge 10) {
        $parameters += [PSCustomObject]@{
            SPECIFIC_SCHEMA = $fields[0]
            SPECIFIC_NAME = $fields[1]
            ORDINAL_POSITION = $fields[2]
            PARAMETER_MODE = $fields[3]
            PARAMETER_NAME = $fields[4]
            DATA_TYPE = $fields[5]
            CHARACTER_MAXIMUM_LENGTH = if ($fields[6] -eq "NULL") { $null } else { $fields[6] }
            NUMERIC_PRECISION = if ($fields[7] -eq "NULL") { $null } else { $fields[7] }
            NUMERIC_SCALE = if ($fields[8] -eq "NULL") { $null } else { $fields[8] }
            DTD_IDENTIFIER = $fields[9]
        }
    }
}

# Query 3: INFORMATION_SCHEMA.TABLES (All Tables)
Write-Host "`n[3/4] Extracting table metadata..." -ForegroundColor Cyan
$tablesQuery = @"
SELECT
    TABLE_SCHEMA,
    TABLE_NAME,
    TABLE_TYPE,
    ENGINE,
    VERSION,
    ROW_FORMAT,
    TABLE_ROWS,
    AVG_ROW_LENGTH,
    DATA_LENGTH,
    MAX_DATA_LENGTH,
    INDEX_LENGTH,
    DATA_FREE,
    AUTO_INCREMENT,
    CREATE_TIME,
    UPDATE_TIME,
    CHECK_TIME,
    TABLE_COLLATION,
    CHECKSUM,
    CREATE_OPTIONS,
    TABLE_COMMENT
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = '$Database'
ORDER BY TABLE_NAME;
"@

$tablesRaw = Invoke-MySqlCliQuery -Query $tablesQuery -Description "TABLES query"

# Parse tables into objects
$tables = @()
foreach ($line in $tablesRaw) {
    $fields = $line -split "`t"
    if ($fields.Count -ge 20) {
        $tables += [PSCustomObject]@{
            TABLE_SCHEMA = $fields[0]
            TABLE_NAME = $fields[1]
            TABLE_TYPE = $fields[2]
            ENGINE = $fields[3]
            VERSION = $fields[4]
            ROW_FORMAT = $fields[5]
            TABLE_ROWS = if ($fields[6] -eq "NULL") { $null } else { $fields[6] }
            AVG_ROW_LENGTH = if ($fields[7] -eq "NULL") { $null } else { $fields[7] }
            DATA_LENGTH = if ($fields[8] -eq "NULL") { $null } else { $fields[8] }
            MAX_DATA_LENGTH = if ($fields[9] -eq "NULL") { $null } else { $fields[9] }
            INDEX_LENGTH = if ($fields[10] -eq "NULL") { $null } else { $fields[10] }
            DATA_FREE = if ($fields[11] -eq "NULL") { $null } else { $fields[11] }
            AUTO_INCREMENT = if ($fields[12] -eq "NULL") { $null } else { $fields[12] }
            CREATE_TIME = $fields[13]
            UPDATE_TIME = if ($fields[14] -eq "NULL") { $null } else { $fields[14] }
            CHECK_TIME = if ($fields[15] -eq "NULL") { $null } else { $fields[15] }
            TABLE_COLLATION = $fields[16]
            CHECKSUM = if ($fields[17] -eq "NULL") { $null } else { $fields[17] }
            CREATE_OPTIONS = $fields[18]
            TABLE_COMMENT = $fields[19]
        }
    }
}

# Query 4: INFORMATION_SCHEMA.COLUMNS (Table Columns)
Write-Host "`n[4/4] Extracting column metadata..." -ForegroundColor Cyan
$columnsQuery = @"
SELECT
    TABLE_SCHEMA,
    TABLE_NAME,
    COLUMN_NAME,
    ORDINAL_POSITION,
    COLUMN_DEFAULT,
    IS_NULLABLE,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    NUMERIC_PRECISION,
    NUMERIC_SCALE,
    COLUMN_TYPE,
    COLUMN_KEY,
    EXTRA,
    COLUMN_COMMENT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = '$Database'
ORDER BY TABLE_NAME, ORDINAL_POSITION;
"@

$columnsRaw = Invoke-MySqlCliQuery -Query $columnsQuery -Description "COLUMNS query"

# Parse columns into objects
$columns = @()
foreach ($line in $columnsRaw) {
    $fields = $line -split "`t"
    if ($fields.Count -ge 14) {
        $columns += [PSCustomObject]@{
            TABLE_SCHEMA = $fields[0]
            TABLE_NAME = $fields[1]
            COLUMN_NAME = $fields[2]
            ORDINAL_POSITION = $fields[3]
            COLUMN_DEFAULT = if ($fields[4] -eq "NULL") { $null } else { $fields[4] }
            IS_NULLABLE = $fields[5]
            DATA_TYPE = $fields[6]
            CHARACTER_MAXIMUM_LENGTH = if ($fields[7] -eq "NULL") { $null } else { $fields[7] }
            NUMERIC_PRECISION = if ($fields[8] -eq "NULL") { $null } else { $fields[8] }
            NUMERIC_SCALE = if ($fields[9] -eq "NULL") { $null } else { $fields[9] }
            COLUMN_TYPE = $fields[10]
            COLUMN_KEY = $fields[11]
            EXTRA = $fields[12]
            COLUMN_COMMENT = $fields[13]
        }
    }
}

# Build JSON structure
Write-Host "`n=== Building JSON Output ===" -ForegroundColor Cyan

$schemaSnapshot = [PSCustomObject]@{
    metadata = [PSCustomObject]@{
        extractionDate = (Get-Date -Format "yyyy-MM-dd HH:mm:ss")
        server = $Server
        port = $Port
        database = $Database
        tool = "mysql CLI"
        scriptVersion = "1.0.0"
    }
    routines = $routines
    parameters = $parameters
    tables = $tables
    columns = $columns
    summary = [PSCustomObject]@{
        totalRoutines = $routines.Count
        totalParameters = $parameters.Count
        totalTables = $tables.Count
        totalColumns = $columns.Count
    }
}

# Save to JSON file
$outputPath = Join-Path $PSScriptRoot "database-schema-snapshot.json"
$schemaSnapshot | ConvertTo-Json -Depth 10 | Out-File -FilePath $outputPath -Encoding UTF8

Write-Host "`n=== Extraction Summary ===" -ForegroundColor Green
Write-Host "✓ Stored Procedures: $($routines.Count)" -ForegroundColor White
Write-Host "✓ Parameters: $($parameters.Count)" -ForegroundColor White
Write-Host "✓ Tables: $($tables.Count)" -ForegroundColor White
Write-Host "✓ Columns: $($columns.Count)" -ForegroundColor White
Write-Host "`nOutput: $outputPath" -ForegroundColor Cyan
Write-Host "`nNext Step: T102 - Generate individual .sql files for each stored procedure" -ForegroundColor Yellow

# Clear password from memory
$PlainPassword = $null
