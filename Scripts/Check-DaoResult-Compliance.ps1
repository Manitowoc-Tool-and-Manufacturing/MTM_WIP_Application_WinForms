<#
.SYNOPSIS
    Checks all DAO files for methods that don't return Model_Dao_Result types
.DESCRIPTION
    Scans Data/*.cs files for async methods that return primitive types instead of Model_Dao_Result<T> or Model_Dao_Result
.EXAMPLE
    .\Check-Model_Dao_Result-Compliance.ps1
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"
$dataFolder = Join-Path $PSScriptRoot "..\Data"

Write-Host "=== DAO Model_Dao_Result Compliance Check ===" -ForegroundColor Cyan
Write-Host "Scanning: $dataFolder" -ForegroundColor Gray
Write-Host ""

# Find all DAO files
$daoFiles = Get-ChildItem -Path $dataFolder -Filter "Dao_*.cs" -File

$findings = @()

foreach ($file in $daoFiles) {
    Write-Host "Checking: $($file.Name)" -ForegroundColor Yellow
    
    $content = Get-Content $file.FullName -Raw
    
    # Pattern to find async methods that don't return Model_Dao_Result
    # Matches: internal/public static async Task<string|int|bool|void|DataTable|DataRow|etc>
    # Excludes: internal/public static async Task<Model_Dao_Result
    
    $lines = Get-Content $file.FullName
    $lineNumber = 0
    
    foreach ($line in $lines) {
        $lineNumber++
        
        # Skip comments and empty lines
        if ($line -match '^\s*//') { continue }
        if ($line -match '^\s*$') { continue }
        
        # Check for async methods that don't return Model_Dao_Result
        if ($line -match '^\s*(internal|public)\s+static\s+async\s+Task<(?!Model_Dao_Result)([^>]+)>\s+(\w+)') {
            $visibility = $matches[1]
            $returnType = $matches[2]
            $methodName = $matches[3]
            
            $findings += [PSCustomObject]@{
                File = $file.Name
                Line = $lineNumber
                Method = $methodName
                ReturnType = "Task<$returnType>"
                Visibility = $visibility
            }
            
            Write-Host "  ⚠️  Line $lineNumber`: $methodName returns Task<$returnType>" -ForegroundColor Red
        }
        
        # Check for async methods that return void (Task without generic)
        if ($line -match '^\s*(internal|public)\s+static\s+async\s+Task\s+(\w+)\s*\(') {
            $visibility = $matches[1]
            $methodName = $matches[2]
            
            $findings += [PSCustomObject]@{
                File = $file.Name
                Line = $lineNumber
                Method = $methodName
                ReturnType = "Task"
                Visibility = $visibility
            }
            
            Write-Host "  ⚠️  Line $lineNumber`: $methodName returns Task (void-like)" -ForegroundColor Red
        }
    }
    
    if (-not ($findings | Where-Object { $_.File -eq $file.Name })) {
        Write-Host "  ✅ All methods return Model_Dao_Result" -ForegroundColor Green
    }
    
    Write-Host ""
}

Write-Host "=== Summary ===" -ForegroundColor Cyan
Write-Host ""

if ($findings.Count -eq 0) {
    Write-Host "✅ All DAO files are compliant - all async methods return Model_Dao_Result types" -ForegroundColor Green
    exit 0
} else {
    Write-Host "⚠️  Found $($findings.Count) non-compliant methods across $($findings | Select-Object -ExpandProperty File -Unique | Measure-Object).Count files" -ForegroundColor Yellow
    Write-Host ""
    
    # Group by file
    $byFile = $findings | Group-Object -Property File
    
    foreach ($fileGroup in $byFile) {
        Write-Host "📄 $($fileGroup.Name):" -ForegroundColor Cyan
        foreach ($finding in $fileGroup.Group) {
            Write-Host "   Line $($finding.Line): $($finding.Method) → $($finding.ReturnType)" -ForegroundColor Gray
        }
        Write-Host ""
    }
    
    Write-Host "💡 These methods need to be refactored to return:" -ForegroundColor Yellow
    Write-Host "   - Model_Dao_Result<T> for methods returning data" -ForegroundColor Gray
    Write-Host "   - Model_Dao_Result for void-like operations" -ForegroundColor Gray
    Write-Host ""
    
    # Export to CSV for tracking
    $csvPath = Join-Path $PSScriptRoot "dao-compliance-issues.csv"
    $findings | Export-Csv -Path $csvPath -NoTypeInformation
    Write-Host "📊 Detailed report exported to: $csvPath" -ForegroundColor Cyan
    
    exit 1
}
