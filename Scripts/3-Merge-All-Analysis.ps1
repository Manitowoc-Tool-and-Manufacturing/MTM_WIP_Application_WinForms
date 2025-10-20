<#
.SYNOPSIS
    Merge all analysis data into single enhanced CSV
.DESCRIPTION
    Combines:
    - Original procedure analysis (procedure-transaction-analysis.csv)
    - Detailed SQL operations (sql-operations-detailed.json)
    - Complete call hierarchy (call-hierarchy-complete.json)
    
    Output: procedure-analysis-complete.csv
#>

[CmdletBinding()]
param(
    [string]$OriginalAnalysis = ".\procedure-transaction-analysis.csv",
    [string]$SQLOperationsFile = ".\sql-operations-detailed.json",
    [string]$CallHierarchyFile = ".\call-hierarchy-complete.json",
    [string]$OutputFile = ".\procedure-analysis-complete.csv"
)

Write-Host "=== Merging Analysis Data ===" -ForegroundColor Cyan

# Load original analysis
Write-Host "Loading original analysis..." -ForegroundColor Gray
$originalData = Import-Csv $OriginalAnalysis

# Load SQL operations
Write-Host "Loading SQL operations..." -ForegroundColor Gray
$sqlOpsJson = Get-Content $SQLOperationsFile -Raw | ConvertFrom-Json
$sqlOps = @{}
foreach ($prop in $sqlOpsJson.PSObject.Properties) {
    $sqlOps[$prop.Name] = $prop.Value
}

# Load call hierarchy
Write-Host "Loading call hierarchy..." -ForegroundColor Gray
$callHierJson = Get-Content $CallHierarchyFile -Raw | ConvertFrom-Json
$callHier = @{}
foreach ($prop in $callHierJson.PSObject.Properties) {
    $callHier[$prop.Name] = $prop.Value
}

Write-Host "`nMerging data..." -ForegroundColor Cyan

$enhancedData = @()

foreach ($proc in $originalData) {
    $procName = $proc.ProcedureName
    
    # Get SQL operations detail
    $sqlDetail = if ($sqlOps.ContainsKey($procName)) {
        ($sqlOps[$procName] | ConvertTo-Json -Compress -Depth 10)
    } else {
        ""
    }
    
    # Get call hierarchy
    $callHierDetail = if ($callHier.ContainsKey($procName)) {
        ($callHier[$procName] | ConvertTo-Json -Compress -Depth 10)
    } else {
        '[{"Status":"Not found","EventHandler":null,"IntermediateMethods":[],"DAO":null}]'
    }
    
    $enhancedData += [PSCustomObject]@{
        ProcedureName = $proc.ProcedureName
        Domain = $proc.Domain
        DetectedPattern = $proc.DetectedPattern
        RecommendedStrategy = $proc.RecommendedStrategy
        Confidence = $proc.Confidence
        ProcedureType = $proc.ProcedureType
        InsertCount = $proc.InsertCount
        UpdateCount = $proc.UpdateCount
        DeleteCount = $proc.DeleteCount
        TotalDMLOps = $proc.TotalDMLOps
        HasTransactionHandling = $proc.HasTransactionHandling
        HasValidation = $proc.HasValidation
        Rationale = $proc.Rationale
        SQLOperationsDetail = $sqlDetail
        CallHierarchy = $callHierDetail
        DeveloperCorrection = $proc.DeveloperCorrection
        Notes = $proc.Notes
    }
}

Write-Host "Exporting enhanced CSV..." -ForegroundColor Cyan
$enhancedData | Export-Csv -Path $OutputFile -NoTypeInformation -Encoding UTF8
Write-Host "✓ Saved: $OutputFile" -ForegroundColor Green

Write-Host "`n=== Merge Summary ===" -ForegroundColor Cyan
Write-Host "  Total procedures: $($enhancedData.Count)" -ForegroundColor White
Write-Host "  Procedures with SQL details: $(($enhancedData | Where-Object { $_.SQLOperationsDetail -ne '' }).Count)" -ForegroundColor White
Write-Host "  Procedures with call hierarchy: $(($enhancedData | Where-Object { $_.CallHierarchy -ne '' }).Count)" -ForegroundColor White
