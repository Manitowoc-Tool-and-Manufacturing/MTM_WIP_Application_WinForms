<#
.SYNOPSIS
    Generate individual .sql files for each stored procedure
.DESCRIPTION
    T102 Deliverable: Extract SHOW CREATE PROCEDURE for each procedure and save to organized folder structure
    Categorizes procedures by domain prefix: inv_*, sys_*, md_*, log_*, qb_*, ub_*
.PARAMETER SchemaFile
    Path to database-schema-snapshot.json from T101 (default: ./database-schema-snapshot.json)
.PARAMETER OutputDir
    Base directory for .sql files (default: ./UpdatedStoredProcedures/ReadyForVerification)
.PARAMETER Server
    MySQL server hostname (default: localhost)
.PARAMETER Port
    MySQL server port (default: 3306)
.PARAMETER Database
    Target database name (default: mtm_wip_application)
.PARAMETER User
    MySQL username (default: root)
.PARAMETER Password
    MySQL password as SecureString (default: root)
.OUTPUTS
    UpdatedStoredProcedures/ReadyForVerification/<domain>/<procedure>.sql files
#>

[CmdletBinding()]
param(
    [string]$SchemaFile = ".\database-schema-snapshot.json",
    [string]$OutputDir = ".\UpdatedStoredProcedures\ReadyForVerification",
    [string]$Server = "localhost",
    [int]$Port = 3306,
    [string]$Database = "mtm_wip_application",
    [string]$User = "root",
    [SecureString]$Password = (ConvertTo-SecureString "root" -AsPlainText -Force)
)

# Convert SecureString to plain text for mysql CLI
$BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($Password)
$PlainPassword = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)
[System.Runtime.InteropServices.Marshal]::ZeroFreeBSTR($BSTR)

Write-Host "=== T102: Generate Stored Procedure SQL Files ===" -ForegroundColor Cyan

# Validate schema file exists
if (-not (Test-Path $SchemaFile)) {
    Write-Host "✗ Schema file not found: $SchemaFile" -ForegroundColor Red
    Write-Host "  Run Extract-DatabaseSchema-CLI.ps1 first (T101)" -ForegroundColor Yellow
    exit 1
}

# Load schema
Write-Host "Loading schema from: $SchemaFile" -ForegroundColor Gray
$schema = Get-Content $SchemaFile | ConvertFrom-Json
$routines = $schema.routines

Write-Host "Found $($routines.Count) stored procedures" -ForegroundColor Green

# Locate mysql CLI
$mysqlPath = $null
try {
    $mysqlPath = (Get-Command mysql -ErrorAction Stop).Source
} catch {
    $mampPath = "C:\MAMP\bin\mysql\bin\mysql.exe"
    if (Test-Path $mampPath) {
        $mysqlPath = $mampPath
    } else {
        Write-Host "✗ mysql CLI not found" -ForegroundColor Red
        exit 1
    }
}
Write-Host "Using mysql: $mysqlPath" -ForegroundColor Gray

# Define domain categorization
$domainMapping = @{
    "inv_"  = "inventory"
    "sys_"  = "system"
    "md_"   = "master-data"
    "log_"  = "logging"
    "qb_"   = "quick-buttons"
    "ub_"   = "user-based"
    "usr_"  = "users"
    "role_" = "roles"
    "trn_"  = "transactions"
}

function Get-ProcedureDomain {
    param([string]$ProcedureName)
    
    foreach ($prefix in $domainMapping.Keys) {
        if ($ProcedureName.StartsWith($prefix)) {
            return $domainMapping[$prefix]
        }
    }
    
    # Default to 'uncategorized' for unknown prefixes
    return "uncategorized"
}

function Get-ProcedureDefinition {
    param(
        [string]$ProcedureName,
        [string]$MysqlPath,
        [string]$Server,
        [int]$Port,
        [string]$Database,
        [string]$User,
        [string]$Password
    )
    
    try {
        $query = "SHOW CREATE PROCEDURE ``$ProcedureName``;"
        
        $mysqlArgs = @(
            "-h", $Server,
            "-P", $Port,
            "-u", $User,
            "-p$Password",
            $Database,
            "--batch",
            "--skip-column-names",
            "-e", $query
        )
        
        # Redirect stderr to null to suppress password warning
        $output = & $MysqlPath @mysqlArgs 2>$null
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "  ✗ Failed to extract $ProcedureName" -ForegroundColor Red
            return $null
        }
        
        # Parse output: SHOW CREATE PROCEDURE returns 3 tab-separated columns:
        # Column 1: Procedure name
        # Column 2: sql_mode (e.g., NO_AUTO_VALUE_ON_ZERO)
        # Column 3: CREATE PROCEDURE statement
        
        $outputText = $output -join "`n"
        $columns = $outputText -split "`t"
        
        if ($columns.Count -lt 3) {
            Write-Host "  ✗ Unexpected output format for $ProcedureName" -ForegroundColor Red
            return $null
        }
        
        # Extract the CREATE PROCEDURE statement (3rd column)
        $definition = $columns[2]
        
        # Replace literal \n with actual newlines for readability
        $definition = $definition -replace '\\n', "`n"
        $definition = $definition -replace '\\t', "    "  # Replace \t with spaces
        
        return $definition
        
    } catch {
        Write-Host "  ✗ Error extracting $ProcedureName`: $_" -ForegroundColor Red
        return $null
    }
}

# Create base output directory
if (-not (Test-Path $OutputDir)) {
    New-Item -Path $OutputDir -ItemType Directory -Force | Out-Null
    Write-Host "Created output directory: $OutputDir" -ForegroundColor Green
}

# Create domain folders
$domains = $domainMapping.Values | Select-Object -Unique
foreach ($domain in $domains) {
    $domainPath = Join-Path $OutputDir $domain
    if (-not (Test-Path $domainPath)) {
        New-Item -Path $domainPath -ItemType Directory -Force | Out-Null
    }
}

# Create uncategorized folder
$uncategorizedPath = Join-Path $OutputDir "uncategorized"
if (-not (Test-Path $uncategorizedPath)) {
    New-Item -Path $uncategorizedPath -ItemType Directory -Force | Out-Null
}

Write-Host "`n=== Extracting Procedure Definitions ===" -ForegroundColor Cyan

$successCount = 0
$failureCount = 0
$totalCount = $routines.Count

$progress = 0
foreach ($routine in $routines) {
    $progress++
    $procedureName = $routine.ROUTINE_NAME
    
    Write-Host "[$progress/$totalCount] $procedureName..." -NoNewline
    
    # Get domain
    $domain = Get-ProcedureDomain -ProcedureName $procedureName
    $domainPath = Join-Path $OutputDir $domain
    
    # Get procedure definition
    $definition = Get-ProcedureDefinition `
        -ProcedureName $procedureName `
        -MysqlPath $mysqlPath `
        -Server $Server `
        -Port $Port `
        -Database $Database `
        -User $User `
        -Password $PlainPassword
    
    if ($null -eq $definition -or $definition.Trim() -eq "") {
        Write-Host " FAILED" -ForegroundColor Red
        $failureCount++
        continue
    }
    
    # Build SQL file content with header
    $fileHeader = @"
-- =============================================
-- Procedure: $procedureName
-- Domain: $domain
-- Extracted: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")
-- Source: $Database on ${Server}:${Port}
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS ``$procedureName``//

"@
    
    $fileFooter = @"

//

DELIMITER ;

-- =============================================
-- End of $procedureName
-- =============================================
"@
    
    $fullContent = $fileHeader + "`n" + $definition + $fileFooter
    
    # Save to file
    $outputFile = Join-Path $domainPath "$procedureName.sql"
    $fullContent | Out-File -FilePath $outputFile -Encoding UTF8 -Force
    
    Write-Host " ✓ $domain/$procedureName.sql" -ForegroundColor Green
    $successCount++
}

# Generate summary report
Write-Host "`n=== Extraction Summary ===" -ForegroundColor Cyan
Write-Host "✓ Success: $successCount procedures" -ForegroundColor Green
if ($failureCount -gt 0) {
    Write-Host "✗ Failed: $failureCount procedures" -ForegroundColor Red
}
Write-Host "Total: $totalCount procedures" -ForegroundColor White

# Domain statistics
Write-Host "`n=== Domain Distribution ===" -ForegroundColor Cyan
$domainStats = @{}
foreach ($routine in $routines) {
    $domain = Get-ProcedureDomain -ProcedureName $routine.ROUTINE_NAME
    if (-not $domainStats.ContainsKey($domain)) {
        $domainStats[$domain] = 0
    }
    $domainStats[$domain]++
}

foreach ($domain in $domainStats.Keys | Sort-Object) {
    $count = $domainStats[$domain]
    Write-Host "  $domain`: $count procedures" -ForegroundColor White
}

Write-Host "`nOutput Directory: $OutputDir" -ForegroundColor Cyan
Write-Host "`nNext Step: T103 - Audit compliance and generate CSV analysis" -ForegroundColor Yellow

# Clear password from memory
$PlainPassword = $null
