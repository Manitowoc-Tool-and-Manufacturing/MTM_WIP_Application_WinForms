# Extract-DatabaseSchema.ps1
# Phase 2.5 Task T101: Extract complete database schema snapshot
# 
# Queries INFORMATION_SCHEMA for:
# - ROUTINES (stored procedures)
# - PARAMETERS (procedure parameters with prefixes)
# - TABLES (all tables)
# - COLUMNS (table columns)
#
# Output: database-schema-snapshot.json

param(
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$Database = "MTM_WIP_Application_Winforms",
    [string]$User = "root",
    [SecureString]$Password
)

# Convert SecureString to plain text for MySQL connection (local dev environment only)
if (-not $Password) {
    $Password = ConvertTo-SecureString "root" -AsPlainText -Force
}
$BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password)
$PlainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)

Write-Host "=== Database Schema Extraction (T101) ===" -ForegroundColor Cyan
Write-Host "Target: $Database on ${Server}:${Port}`n" -ForegroundColor Yellow

# Connection string
$connectionString = "Server=$Server;Port=$Port;Database=$Database;Uid=$User;Pwd=$PlainPassword;SslMode=none;AllowPublicKeyRetrieval=true;"

# Load MySQL .NET connector
Add-Type -Path "C:\Program Files (x86)\MySQL\MySQL Connector NET 8.4.0\MySql.Data.dll"

# Helper function to execute query and return DataTable
function Invoke-MySqlQuery {
    param([string]$Query)
    
    $connection = New-Object MySql.Data.MySqlClient.MySqlConnection($connectionString)
    $command = New-Object MySql.Data.MySqlClient.MySqlCommand($Query, $connection)
    $adapter = New-Object MySql.Data.MySqlClient.MySqlDataAdapter($command)
    $dataTable = New-Object System.Data.DataTable
    
    try {
        $connection.Open()
        [void]$adapter.Fill($dataTable)
        return $dataTable
    }
    finally {
        $connection.Close()
    }
}

# 1. Extract ROUTINES (stored procedures)
Write-Host "[1/4] Querying INFORMATION_SCHEMA.ROUTINES..." -ForegroundColor Green
$routinesQuery = @"
SELECT 
    ROUTINE_SCHEMA,
    ROUTINE_NAME,
    ROUTINE_TYPE,
    DATA_TYPE,
    DTD_IDENTIFIER,
    ROUTINE_DEFINITION,
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

$routines = Invoke-MySqlQuery -Query $routinesQuery
Write-Host "  Found $($routines.Rows.Count) stored procedures" -ForegroundColor White

# 2. Extract PARAMETERS
Write-Host "[2/4] Querying INFORMATION_SCHEMA.PARAMETERS..." -ForegroundColor Green
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

$parameters = Invoke-MySqlQuery -Query $parametersQuery
Write-Host "  Found $($parameters.Rows.Count) procedure parameters" -ForegroundColor White

# 3. Extract TABLES
Write-Host "[3/4] Querying INFORMATION_SCHEMA.TABLES..." -ForegroundColor Green
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

$tables = Invoke-MySqlQuery -Query $tablesQuery
Write-Host "  Found $($tables.Rows.Count) tables" -ForegroundColor White

# 4. Extract COLUMNS
Write-Host "[4/4] Querying INFORMATION_SCHEMA.COLUMNS..." -ForegroundColor Green
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
    DATETIME_PRECISION,
    CHARACTER_SET_NAME,
    COLLATION_NAME,
    COLUMN_TYPE,
    COLUMN_KEY,
    EXTRA,
    PRIVILEGES,
    COLUMN_COMMENT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = '$Database'
ORDER BY TABLE_NAME, ORDINAL_POSITION;
"@

$columns = Invoke-MySqlQuery -Query $columnsQuery
Write-Host "  Found $($columns.Rows.Count) table columns`n" -ForegroundColor White

# Convert DataTables to JSON-friendly objects
function ConvertTo-JsonObject {
    param($DataTable)
    
    $rows = @()
    foreach ($row in $DataTable.Rows) {
        $obj = @{}
        foreach ($col in $DataTable.Columns) {
            $value = $row[$col.ColumnName]
            if ($value -is [DBNull]) {
                $obj[$col.ColumnName] = $null
            }
            else {
                $obj[$col.ColumnName] = $value
            }
        }
        $rows += $obj
    }
    return $rows
}

Write-Host "Converting to JSON format..." -ForegroundColor Green

$schemaSnapshot = @{
    ExtractedAt = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Database = $Database
    Server = $Server
    Routines = ConvertTo-JsonObject -DataTable $routines
    Parameters = ConvertTo-JsonObject -DataTable $parameters
    Tables = ConvertTo-JsonObject -DataTable $tables
    Columns = ConvertTo-JsonObject -DataTable $columns
    Summary = @{
        StoredProcedureCount = $routines.Rows.Count
        ParameterCount = $parameters.Rows.Count
        TableCount = $tables.Rows.Count
        ColumnCount = $columns.Rows.Count
    }
}

# Save to JSON file
$outputPath = "Database\database-schema-snapshot.json"
$schemaSnapshot | ConvertTo-Json -Depth 10 | Out-File -FilePath $outputPath -Encoding UTF8

Write-Host "`n=== Extraction Complete ===" -ForegroundColor Cyan
Write-Host "Output saved to: $outputPath`n" -ForegroundColor Yellow
Write-Host "Summary:" -ForegroundColor White
Write-Host "  - Stored Procedures: $($routines.Rows.Count)" -ForegroundColor White
Write-Host "  - Parameters: $($parameters.Rows.Count)" -ForegroundColor White
Write-Host "  - Tables: $($tables.Rows.Count)" -ForegroundColor White
Write-Host "  - Columns: $($columns.Rows.Count)" -ForegroundColor White
Write-Host "`nNext Step: T102 - Generate individual .sql files for each stored procedure" -ForegroundColor Cyan
