<#
.SYNOPSIS
    Audit stored procedures for compliance with standards and detect transaction patterns
.DESCRIPTION
    T103 Deliverable: Analyze all .sql files for:
    1. Compliance with 00_STATUS_CODE_STANDARDS.md (OUT params, status codes, error handling)
    2. Transaction pattern detection (single-step vs multi-step operations)
    Generates TWO CSV files:
    - compliance-report.csv: Compliance scores per procedure
    - procedure-transaction-analysis.csv: Pattern detection with recommendations
.PARAMETER SqlFilesDir
    Directory containing .sql files from T102 (default: ./UpdatedStoredProcedures/ReadyForVerification)
.PARAMETER StandardsFile
    Path to standards document (default: ./CurrentStoredProcedures/00_STATUS_CODE_STANDARDS.md)
.PARAMETER SchemaFile
    Path to database schema from T101 (default: ./database-schema-snapshot.json)
.OUTPUTS
    compliance-report.csv
    procedure-transaction-analysis.csv
#>

[CmdletBinding()]
param(
    [string]$SqlFilesDir = ".\UpdatedStoredProcedures\ReadyForVerification",
    [string]$StandardsFile = ".\CurrentStoredProcedures\00_STATUS_CODE_STANDARDS.md",
    [string]$SchemaFile = ".\database-schema-snapshot.json",
    [string]$ComplianceOutput = ".\compliance-report.csv",
    [string]$TransactionOutput = ".\procedure-transaction-analysis.csv"
)

Write-Host "=== T103: Stored Procedure Compliance Audit ===" -ForegroundColor Cyan

# Validate inputs
if (-not (Test-Path $SqlFilesDir)) {
    Write-Host "✗ SQL files directory not found: $SqlFilesDir" -ForegroundColor Red
    exit 1
}

if (-not (Test-Path $SchemaFile)) {
    Write-Host "✗ Schema file not found: $SchemaFile" -ForegroundColor Red
    exit 1
}

Write-Host "Loading schema metadata..." -ForegroundColor Gray
$schema = Get-Content $SchemaFile | ConvertFrom-Json

# Build parameter lookup for fast access
$parameterLookup = @{}
foreach ($param in $schema.parameters) {
    $procName = $param.SPECIFIC_NAME
    if (-not $parameterLookup.ContainsKey($procName)) {
        $parameterLookup[$procName] = @()
    }
    $parameterLookup[$procName] += $param
}

Write-Host "Found $($schema.routines.Count) procedures to audit" -ForegroundColor Green

# Get all .sql files
$sqlFiles = Get-ChildItem -Path $SqlFilesDir -Recurse -Filter "*.sql"
Write-Host "Found $($sqlFiles.Count) SQL files" -ForegroundColor Green

# Compliance audit results
$complianceResults = @()

# Transaction analysis results
$transactionResults = @()

function Test-HasOutParameter {
    param([string]$Content, [string]$ParamName)
    return $Content -match "OUT\s+$ParamName\s+"
}

function Test-HasExitHandler {
    param([string]$Content)
    return $Content -match "DECLARE\s+EXIT\s+HANDLER\s+FOR\s+SQLEXCEPTION"
}

function Test-HasTransactionHandling {
    param([string]$Content)
    $hasStart = $Content -match "START\s+TRANSACTION"
    $hasCommit = $Content -match "COMMIT"
    $hasRollback = $Content -match "ROLLBACK"
    return @{
        HasStart = $hasStart
        HasCommit = $hasCommit
        HasRollback = $hasRollback
        IsComplete = ($hasStart -and $hasCommit -and $hasRollback)
    }
}

function Test-HasStatusCodeChecks {
    param([string]$Content)
    
    $checks = @{
        Status1 = $Content -match "SET\s+p_Status\s*=\s*1"  # Success with data
        Status0 = $Content -match "SET\s+p_Status\s*=\s*0"  # Success no data
        StatusNeg1 = $Content -match "SET\s+p_Status\s*=\s*-1"  # Database error
        StatusNeg2 = $Content -match "SET\s+p_Status\s*=\s*-2"  # Validation error
        StatusNeg3 = $Content -match "SET\s+p_Status\s*=\s*-3"  # Business logic error
    }
    
    return $checks
}

function Test-HasRowCountCheck {
    param([string]$Content)
    
    $foundRows = $Content -match "FOUND_ROWS\(\)"
    $rowCount = $Content -match "ROW_COUNT\(\)"
    
    return @{
        UsesFOUND_ROWS = $foundRows
        UsesROW_COUNT = $rowCount
        HasCheck = ($foundRows -or $rowCount)
    }
}

function Test-HasValidation {
    param([string]$Content)
    
    # Check for common validation patterns
    $hasNullCheck = $Content -match "IS NULL"
    $hasEmptyCheck = $Content -match "=\s*''"
    $hasIfValidation = $Content -match "IF\s+.*\s+THEN"
    
    return ($hasNullCheck -or $hasEmptyCheck) -and $hasIfValidation
}

function Get-DMLOperationCount {
    param([string]$Content)
    
    # Count INSERT, UPDATE, DELETE statements (ignore comments)
    $lines = $Content -split "`n" | Where-Object { $_ -notmatch "^\s*--" }
    $contentNoComments = $lines -join "`n"
    
    $insertCount = ([regex]::Matches($contentNoComments, "\bINSERT\s+INTO\b", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)).Count
    $updateCount = ([regex]::Matches($contentNoComments, "\bUPDATE\b", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)).Count
    $deleteCount = ([regex]::Matches($contentNoComments, "\bDELETE\s+FROM\b", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)).Count
    
    return @{
        INSERT = $insertCount
        UPDATE = $updateCount
        DELETE = $deleteCount
        Total = $insertCount + $updateCount + $deleteCount
    }
}

function Get-DMLOperationDetails {
    param([string]$Content)
    
    # Extract detailed information about each DML operation
    $operations = @()
    
    # Remove comments
    $lines = $Content -split "`n" | Where-Object { $_ -notmatch "^\s*--" }
    $contentNoComments = $lines -join "`n"
    
    # Find INSERT operations with table names
    $insertMatches = [regex]::Matches($contentNoComments, "INSERT\s+INTO\s+(\w+)", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
    foreach ($match in $insertMatches) {
        $tableName = $match.Groups[1].Value
        $operations += "[INSERT] INTO [$tableName]"
    }
    
    # Find UPDATE operations with table names
    $updateMatches = [regex]::Matches($contentNoComments, "UPDATE\s+(\w+)\s+SET", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
    foreach ($match in $updateMatches) {
        $tableName = $match.Groups[1].Value
        $operations += "[UPDATE] TABLE [$tableName]"
    }
    
    # Find DELETE operations with table names
    $deleteMatches = [regex]::Matches($contentNoComments, "DELETE\s+FROM\s+(\w+)", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
    foreach ($match in $deleteMatches) {
        $tableName = $match.Groups[1].Value
        $operations += "[DELETE] FROM [$tableName]"
    }
    
    return $operations
}

function Format-ActionBasedRationale {
    param(
        [string]$Pattern,
        [int]$InsertCount,
        [int]$UpdateCount,
        [int]$DeleteCount,
        [string]$ProcedureType,
        [array]$Operations
    )
    
    $actions = @()
    
    # Build action description
    if ($InsertCount -gt 0) {
        $actions += "[INSERT] x$InsertCount"
    }
    if ($UpdateCount -gt 0) {
        $actions += "[UPDATE] x$UpdateCount"
    }
    if ($DeleteCount -gt 0) {
        $actions += "[DELETE] x$DeleteCount"
    }
    
    # Add pattern indicator
    $patternIndicator = switch ($Pattern) {
        "READ_ONLY" { "[READ-ONLY]" }
        "SINGLE_STEP" { "[1-STEP]" }
        "MULTI_STEP_SEQUENTIAL" { "[SEQUENTIAL]" }
        "MULTI_STEP_CONDITIONAL" { "[CONDITIONAL]" }
        default { "[UNKNOWN]" }
    }
    
    # Add procedure type
    $typeIndicator = switch ($ProcedureType) {
        "SELECT_ONLY" { "[QUERY]" }
        "DML_ONLY" { "[MODIFY]" }
        "MIXED" { "[QUERY+MODIFY]" }
        default { "[UNKNOWN]" }
    }
    
    # Build final rationale with operations detail
    if ($Operations.Count -gt 0) {
        $opsDetail = $Operations -join " → "
        return "$patternIndicator $typeIndicator | $opsDetail"
    } else {
        if ($actions.Count -gt 0) {
            $actionSummary = $actions -join " + "
            return "$patternIndicator $typeIndicator | $actionSummary"
        } else {
            return "$patternIndicator $typeIndicator | No data modifications"
        }
    }
}

function Get-ProcedureType {
    param([string]$Content, [object]$DMLCounts)
    
    # Determine if procedure is primarily SELECT, INSERT/UPDATE/DELETE, or mixed
    $hasSelect = $Content -match "\bSELECT\b"
    $hasDML = $DMLCounts.Total -gt 0
    
    if ($hasDML -and -not $hasSelect) {
        return "DML_ONLY"
    } elseif ($hasSelect -and -not $hasDML) {
        return "SELECT_ONLY"
    } elseif ($hasSelect -and $hasDML) {
        return "MIXED"
    } else {
        return "UNKNOWN"
    }
}

function Get-TransactionPattern {
    param(
        [string]$Content,
        [object]$DMLCounts,
        [string]$ProcedureType
    )
    
    # Detect transaction pattern based on DML operations and logic
    if ($DMLCounts.Total -eq 0) {
        return "READ_ONLY"
    } elseif ($DMLCounts.Total -eq 1) {
        return "SINGLE_STEP"
    } elseif ($DMLCounts.Total -gt 1) {
        # Check if operations are in conditional blocks (indicates complex logic)
        $hasConditionalDML = $Content -match "IF\s+.*\s+THEN\s+.*\b(INSERT|UPDATE|DELETE)\b"
        if ($hasConditionalDML) {
            return "MULTI_STEP_CONDITIONAL"
        } else {
            return "MULTI_STEP_SEQUENTIAL"
        }
    }
    
    return "UNKNOWN"
}

function Get-RecommendedStrategy {
    param(
        [string]$Pattern,
        [object]$TransactionHandling,
        [bool]$HasValidation
    )
    
    switch ($Pattern) {
        "READ_ONLY" {
            return "StandardQuery"
        }
        "SINGLE_STEP" {
            if ($TransactionHandling.IsComplete) {
                return "StandardTransaction"
            } else {
                return "AddTransactionHandling"
            }
        }
        "MULTI_STEP_SEQUENTIAL" {
            if ($TransactionHandling.IsComplete) {
                return "StandardTransaction"
            } else {
                return "AddTransactionHandling"
            }
        }
        "MULTI_STEP_CONDITIONAL" {
            if ($TransactionHandling.IsComplete -and $HasValidation) {
                return "ComplexTransaction"
            } else {
                return "RefactorToComplexTransaction"
            }
        }
        default {
            return "ManualReview"
        }
    }
}

function Get-ConfidenceScore {
    param(
        [string]$Pattern,
        [object]$DMLCounts,
        [bool]$HasTransactionHandling
    )
    
    # Calculate confidence (0-100) in pattern detection
    $confidence = 50  # Base confidence
    
    # Increase confidence if pattern is clear
    if ($Pattern -eq "READ_ONLY" -and $DMLCounts.Total -eq 0) {
        $confidence = 95
    } elseif ($Pattern -eq "SINGLE_STEP" -and $DMLCounts.Total -eq 1) {
        $confidence = 90
    } elseif ($Pattern -match "MULTI_STEP" -and $DMLCounts.Total -gt 1) {
        $confidence = 85
    }
    
    # Decrease confidence if transaction handling doesn't match expectation
    if ($DMLCounts.Total -gt 0 -and -not $HasTransactionHandling) {
        $confidence -= 15
    }
    
    return [Math]::Max(0, [Math]::Min(100, $confidence))
}

Write-Host "`n=== Analyzing Procedures ===" -ForegroundColor Cyan

$progress = 0
foreach ($file in $sqlFiles) {
    $progress++
    $procedureName = [System.IO.Path]::GetFileNameWithoutExtension($file.Name)
    
    Write-Host "[$progress/$($sqlFiles.Count)] $procedureName..." -NoNewline
    
    # Read file content
    $content = Get-Content $file.FullName -Raw
    
    # Get parameters from schema
    $params = $parameterLookup[$procedureName]
    if (-not $params) {
        $params = @()
    }
    
    # Compliance checks
    $hasStatusOut = Test-HasOutParameter -Content $content -ParamName "p_Status"
    $hasErrorMsgOut = Test-HasOutParameter -Content $content -ParamName "p_ErrorMsg"
    $hasExitHandler = Test-HasExitHandler -Content $content
    $transactionHandling = Test-HasTransactionHandling -Content $content
    $statusCodes = Test-HasStatusCodeChecks -Content $content
    $rowCountCheck = Test-HasRowCountCheck -Content $content
    $hasValidation = Test-HasValidation -Content $content
    
    # DML analysis
    $dmlCounts = Get-DMLOperationCount -Content $content
    $dmlDetails = Get-DMLOperationDetails -Content $content
    $procedureType = Get-ProcedureType -Content $content -DMLCounts $dmlCounts
    
    # Transaction pattern detection
    $pattern = Get-TransactionPattern -Content $content -DMLCounts $dmlCounts -ProcedureType $procedureType
    $recommendedStrategy = Get-RecommendedStrategy -Pattern $pattern -TransactionHandling $transactionHandling -HasValidation $hasValidation
    $confidence = Get-ConfidenceScore -Pattern $pattern -DMLCounts $dmlCounts -HasTransactionHandling $transactionHandling.IsComplete
    
    # Format action-based rationale
    $actionRationale = Format-ActionBasedRationale `
        -Pattern $pattern `
        -InsertCount $dmlCounts.INSERT `
        -UpdateCount $dmlCounts.UPDATE `
        -DeleteCount $dmlCounts.DELETE `
        -ProcedureType $procedureType `
        -Operations $dmlDetails
    
    # Calculate compliance score (0-100)
    $complianceScore = 0
    $maxPoints = 10
    
    if ($hasStatusOut) { $complianceScore += 2 }
    if ($hasErrorMsgOut) { $complianceScore += 2 }
    if ($hasExitHandler) { $complianceScore += 2 }
    
    # Status code usage (at least 2 different codes)
    $statusCodeCount = ($statusCodes.Values | Where-Object { $_ -eq $true }).Count
    if ($statusCodeCount -ge 2) { $complianceScore += 1 }
    if ($statusCodeCount -ge 3) { $complianceScore += 1 }
    
    # Row count checking (if SELECT procedure)
    if ($procedureType -eq "SELECT_ONLY" -or $procedureType -eq "MIXED") {
        if ($rowCountCheck.HasCheck) { $complianceScore += 1 }
    } else {
        $complianceScore += 1  # Not applicable, give credit
    }
    
    # Transaction handling (if DML procedure)
    if ($dmlCounts.Total -gt 0) {
        if ($transactionHandling.IsComplete) { $complianceScore += 1 }
    } else {
        $complianceScore += 1  # Not applicable, give credit
    }
    
    # Validation
    if ($hasValidation) { $complianceScore += 1 }
    
    $compliancePercent = [Math]::Round(($complianceScore / $maxPoints) * 100, 0)
    
    # Compliance status
    $complianceStatus = if ($compliancePercent -ge 80) { "COMPLIANT" } elseif ($compliancePercent -ge 50) { "PARTIAL" } else { "NON_COMPLIANT" }
    
    # Add to compliance results
    $complianceResults += [PSCustomObject]@{
        ProcedureName = $procedureName
        Domain = $file.Directory.Name
        ComplianceScore = $compliancePercent
        ComplianceStatus = $complianceStatus
        HasStatusOut = $hasStatusOut
        HasErrorMsgOut = $hasErrorMsgOut
        HasExitHandler = $hasExitHandler
        StatusCodeCount = $statusCodeCount
        HasRowCountCheck = $rowCountCheck.HasCheck
        HasTransactionHandling = $transactionHandling.IsComplete
        HasValidation = $hasValidation
        ProcedureType = $procedureType
        DMLOperations = $dmlCounts.Total
    }
    
    # Add to transaction analysis results
    $transactionResults += [PSCustomObject]@{
        ProcedureName = $procedureName
        Domain = $file.Directory.Name
        DetectedPattern = $pattern
        RecommendedStrategy = $recommendedStrategy
        Confidence = $confidence
        ProcedureType = $procedureType
        InsertCount = $dmlCounts.INSERT
        UpdateCount = $dmlCounts.UPDATE
        DeleteCount = $dmlCounts.DELETE
        TotalDMLOps = $dmlCounts.Total
        HasTransactionHandling = $transactionHandling.IsComplete
        HasValidation = $hasValidation
        Rationale = $actionRationale
        DeveloperCorrection = ""  # For T106a review workflow
        Notes = ""
    }
    
    Write-Host " $complianceStatus ($compliancePercent%)" -ForegroundColor $(
        if ($complianceStatus -eq "COMPLIANT") { "Green" }
        elseif ($complianceStatus -eq "PARTIAL") { "Yellow" }
        else { "Red" }
    )
}

# Generate compliance report CSV
Write-Host "`n=== Generating Compliance Report ===" -ForegroundColor Cyan
$complianceResults | Export-Csv -Path $ComplianceOutput -NoTypeInformation -Encoding UTF8
Write-Host "✓ Saved: $ComplianceOutput" -ForegroundColor Green

# Generate transaction analysis CSV
Write-Host "`n=== Generating Transaction Analysis ===" -ForegroundColor Cyan
$transactionResults | Export-Csv -Path $TransactionOutput -NoTypeInformation -Encoding UTF8
Write-Host "✓ Saved: $TransactionOutput" -ForegroundColor Green

# Generate summary statistics
Write-Host "`n=== Compliance Summary ===" -ForegroundColor Cyan

$compliantCount = ($complianceResults | Where-Object { $_.ComplianceStatus -eq "COMPLIANT" }).Count
$partialCount = ($complianceResults | Where-Object { $_.ComplianceStatus -eq "PARTIAL" }).Count
$nonCompliantCount = ($complianceResults | Where-Object { $_.ComplianceStatus -eq "NON_COMPLIANT" }).Count

Write-Host "  Compliant (≥80%): $compliantCount procedures" -ForegroundColor Green
Write-Host "  Partial (50-79%): $partialCount procedures" -ForegroundColor Yellow
Write-Host "  Non-Compliant (<50%): $nonCompliantCount procedures" -ForegroundColor Red

$avgCompliance = [Math]::Round(($complianceResults | Measure-Object -Property ComplianceScore -Average).Average, 1)
Write-Host "`n  Average Compliance: $avgCompliance%" -ForegroundColor White

# Pattern distribution
Write-Host "`n=== Transaction Pattern Distribution ===" -ForegroundColor Cyan
$patternGroups = $transactionResults | Group-Object -Property DetectedPattern | Sort-Object Count -Descending
foreach ($group in $patternGroups) {
    $percent = [Math]::Round(($group.Count / $transactionResults.Count) * 100, 1)
    Write-Host "  $($group.Name): $($group.Count) procedures ($percent%)" -ForegroundColor White
}

# Recommended actions
Write-Host "`n=== Recommended Actions ===" -ForegroundColor Cyan
$actionGroups = $transactionResults | Group-Object -Property RecommendedStrategy | Sort-Object Count -Descending
foreach ($group in $actionGroups) {
    Write-Host "  $($group.Name): $($group.Count) procedures" -ForegroundColor White
}

Write-Host "`n=== Next Steps ===" -ForegroundColor Yellow
Write-Host "1. Review procedure-transaction-analysis.csv" -ForegroundColor Gray
Write-Host "2. T104: Document parameter prefix conventions" -ForegroundColor Gray
Write-Host "3. T105: Create refactoring priority matrix" -ForegroundColor Gray
Write-Host "4. T106a: Developer CSV review workflow (GATES T113)" -ForegroundColor Gray
