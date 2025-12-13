<#
.SYNOPSIS
    Validates codebase compliance with MTM WIP Application Constitution

.DESCRIPTION
    Scans C# files for constitution violations:
    - MessageBox.Show usage (FORBIDDEN)
    - Direct MySqlConnection/SqlConnection instantiation (FORBIDDEN except documented exceptions)
    - Missing using statements on connections
    - Blocking async calls (.Result, .Wait(), .GetAwaiter().GetResult())
    
.PARAMETER Path
    Path to scan (defaults to repository root)

.PARAMETER FailOnViolations
    Exit with non-zero code if violations found (useful for CI/CD)

.PARAMETER ExcludeTests
    Exclude test files from validation

.EXAMPLE
    .\validate-constitution-compliance.ps1
    
.EXAMPLE
    .\validate-constitution-compliance.ps1 -Path "Services" -FailOnViolations
#>

param(
    [string]$Path = $PSScriptRoot + "\..\..\..\..",
    [switch]$FailOnViolations,
    [switch]$ExcludeTests
)

$ErrorActionPreference = "Stop"

Write-Host "==================================================================" -ForegroundColor Cyan
Write-Host "MTM WIP Application - Constitution Compliance Validation" -ForegroundColor Cyan
Write-Host "==================================================================" -ForegroundColor Cyan
Write-Host ""

# Resolve absolute path
$ScanPath = Resolve-Path $Path -ErrorAction Stop
Write-Host "Scanning: $ScanPath" -ForegroundColor Yellow
Write-Host ""

# Approved architectural exceptions
$ApprovedExceptions = @(
    "Service_OnStartup_Database.cs",  # Parameter cache initialization
    "Helper_Control_MySqlSignal.cs"   # Network diagnostics
)

$Violations = @()

# Get all C# files
$CsFiles = Get-ChildItem -Path $ScanPath -Filter "*.cs" -Recurse | Where-Object {
    $_.FullName -notmatch "\\obj\\" -and 
    $_.FullName -notmatch "\\bin\\" -and
    (-not $ExcludeTests -or $_.FullName -notmatch "\\Tests?\\")
}

Write-Host "Found $($CsFiles.Count) C# files to scan" -ForegroundColor Gray
Write-Host ""

# Check 1: MessageBox.Show usage (ABSOLUTELY FORBIDDEN)
Write-Host "[1/4] Checking for MessageBox.Show usage..." -ForegroundColor Cyan
$MessageBoxViolations = @()

foreach ($File in $CsFiles) {
    $Content = Get-Content $File.FullName -Raw
    if ($Content -match "MessageBox\.Show") {
        $Lines = $Content -split "`n"
        for ($i = 0; $i -lt $Lines.Length; $i++) {
            if ($Lines[$i] -match "MessageBox\.Show") {
                $MessageBoxViolations += [PSCustomObject]@{
                    File = $File.FullName.Replace($ScanPath, "").TrimStart("\")
                    Line = $i + 1
                    Code = $Lines[$i].Trim()
                }
            }
        }
    }
}

if ($MessageBoxViolations.Count -gt 0) {
    Write-Host "  ❌ VIOLATION: Found $($MessageBoxViolations.Count) MessageBox.Show usage(s)" -ForegroundColor Red
    $Violations += $MessageBoxViolations
} else {
    Write-Host "  ✅ PASS: No MessageBox.Show usage found" -ForegroundColor Green
}
Write-Host ""

# Check 2: Direct MySqlConnection/SqlConnection usage (FORBIDDEN except approved)
Write-Host "[2/4] Checking for direct database connection instantiation..." -ForegroundColor Cyan
$ConnectionViolations = @()

foreach ($File in $CsFiles) {
    # Skip approved exceptions
    if ($ApprovedExceptions | Where-Object { $File.Name -eq $_ }) {
        Write-Host "  ⚠️  SKIP: $($File.Name) (approved architectural exception)" -ForegroundColor Yellow
        continue
    }
    
    $Content = Get-Content $File.FullName -Raw
    $Patterns = @("new MySqlConnection", "new SqlConnection")
    
    foreach ($Pattern in $Patterns) {
        if ($Content -match $Pattern) {
            $Lines = $Content -split "`n"
            for ($i = 0; $i -lt $Lines.Length; $i++) {
                if ($Lines[$i] -match $Pattern -and $Lines[$i] -notmatch "MySqlConnectionStringBuilder|SqlConnectionStringBuilder") {
                    $ConnectionViolations += [PSCustomObject]@{
                        File = $File.FullName.Replace($ScanPath, "").TrimStart("\")
                        Line = $i + 1
                        Code = $Lines[$i].Trim()
                        Type = $Pattern
                    }
                }
            }
        }
    }
}

if ($ConnectionViolations.Count -gt 0) {
    Write-Host "  ❌ VIOLATION: Found $($ConnectionViolations.Count) direct connection instantiation(s)" -ForegroundColor Red
    $Violations += $ConnectionViolations
} else {
    Write-Host "  ✅ PASS: No direct connection instantiation found (outside approved exceptions)" -ForegroundColor Green
}
Write-Host ""

# Check 3: Blocking async calls (FORBIDDEN)
Write-Host "[3/4] Checking for blocking async calls..." -ForegroundColor Cyan
$BlockingViolations = @()

foreach ($File in $CsFiles) {
    $Content = Get-Content $File.FullName -Raw
    $Patterns = @("\.Result\b", "\.Wait\(", "\.GetAwaiter\(\)\.GetResult\(")
    
    foreach ($Pattern in $Patterns) {
        if ($Content -match $Pattern) {
            $Lines = $Content -split "`n"
            for ($i = 0; $i -lt $Lines.Length; $i++) {
                if ($Lines[$i] -match $Pattern -and $Lines[$i] -notmatch "//.*$Pattern") {
                    $BlockingViolations += [PSCustomObject]@{
                        File = $File.FullName.Replace($ScanPath, "").TrimStart("\")
                        Line = $i + 1
                        Code = $Lines[$i].Trim()
                        Type = $Pattern
                    }
                }
            }
        }
    }
}

if ($BlockingViolations.Count -gt 0) {
    Write-Host "  ⚠️  WARNING: Found $($BlockingViolations.Count) potential blocking async call(s)" -ForegroundColor Yellow
    Write-Host "     (Review manually - some .Result usage may be legitimate in specific contexts)" -ForegroundColor Gray
    # Don't add to violations for now - these need manual review
} else {
    Write-Host "  ✅ PASS: No blocking async calls found" -ForegroundColor Green
}
Write-Host ""

# Check 4: Missing using statements on connections
Write-Host "[4/4] Checking for connections without using statements..." -ForegroundColor Cyan
$UsingViolations = @()

foreach ($File in $CsFiles) {
    $Content = Get-Content $File.FullName -Raw
    $Lines = $Content -split "`n"
    
    for ($i = 0; $i -lt $Lines.Length; $i++) {
        $Line = $Lines[$i]
        
        # Check for connection instantiation NOT preceded by "using" or "using var"
        if (($Line -match "new MySqlConnection|new SqlConnection") -and 
            ($Line -notmatch "MySqlConnectionStringBuilder|SqlConnectionStringBuilder") -and
            ($Line -notmatch "using\s+(var\s+)?")) {
            
            # Look back 1-2 lines to see if using is on previous line
            $HasUsing = $false
            if ($i -gt 0 -and $Lines[$i-1] -match "using\s*\(") { $HasUsing = $true }
            if ($i -gt 1 -and $Lines[$i-2] -match "using\s*\(") { $HasUsing = $true }
            
            if (-not $HasUsing) {
                $UsingViolations += [PSCustomObject]@{
                    File = $File.FullName.Replace($ScanPath, "").TrimStart("\")
                    Line = $i + 1
                    Code = $Line.Trim()
                }
            }
        }
    }
}

if ($UsingViolations.Count -gt 0) {
    Write-Host "  ⚠️  WARNING: Found $($UsingViolations.Count) connection(s) potentially missing using statement" -ForegroundColor Yellow
    Write-Host "     (Review manually - parser may miss some patterns)" -ForegroundColor Gray
} else {
    Write-Host "  ✅ PASS: All connections appear to use using statements" -ForegroundColor Green
}
Write-Host ""

# Summary Report
Write-Host "==================================================================" -ForegroundColor Cyan
Write-Host "COMPLIANCE SUMMARY" -ForegroundColor Cyan
Write-Host "==================================================================" -ForegroundColor Cyan
Write-Host ""

if ($Violations.Count -eq 0) {
    Write-Host "✅ ALL CHECKS PASSED - Constitution compliant!" -ForegroundColor Green
    Write-Host ""
    exit 0
} else {
    Write-Host "❌ VIOLATIONS FOUND: $($Violations.Count) total" -ForegroundColor Red
    Write-Host ""
    
    # Group violations by file
    $ViolationsByFile = $Violations | Group-Object -Property File
    
    foreach ($FileGroup in $ViolationsByFile) {
        Write-Host "$($FileGroup.Name) ($($FileGroup.Count) violation(s))" -ForegroundColor Red
        foreach ($Violation in $FileGroup.Group) {
            Write-Host "  Line $($Violation.Line): $($Violation.Code)" -ForegroundColor Gray
        }
        Write-Host ""
    }
    
    Write-Host "ACTION REQUIRED:" -ForegroundColor Yellow
    Write-Host "  1. MessageBox.Show → Use Service_ErrorHandler.ShowUserError()" -ForegroundColor Yellow
    Write-Host "  2. Direct connections → Use Helper_Database_StoredProcedure methods" -ForegroundColor Yellow
    Write-Host "  3. Document any legitimate exceptions with comments" -ForegroundColor Yellow
    Write-Host ""
    
    if ($FailOnViolations) {
        exit 1
    }
}
