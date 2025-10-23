<#
.SYNOPSIS
    Validates documentation completeness and quality for MTM database layer standardization.

.DESCRIPTION
    Scans specification directory for required documentation files, checks freshness,
    validates markdown syntax, and verifies cross-references.

.PARAMETER SpecDir
    Path to specification directory (default: specs/002-003-database-layer-complete)

.PARAMETER MinCoverage
    Minimum required documentation coverage percentage (default: 80)

.PARAMETER Json
    Output results as JSON

.EXAMPLE
    .\Validate-Documentation.ps1 -SpecDir "specs\002-003-database-layer-complete" -Verbose

.NOTES
    Author: MTM Development Team
    Date: 2025-10-19
    Task: T129 - Generate Documentation Update Matrix with validation script
#>

[CmdletBinding()]
param(
    [string]$SpecDir = "specs\002-003-database-layer-complete",
    [int]$MinCoverage = 80,
    [switch]$Json
)

$ErrorActionPreference = "Stop"

#region Helper Functions

function Test-FileExists {
    param([string]$Path, [string]$Category)
    
    if (Test-Path $Path) {
        return @{
            Exists = $true
            Path = $Path
            Category = $Category
            LastModified = (Get-Item $Path).LastWriteTime
            SizeKB = [math]::Round((Get-Item $Path).Length / 1KB, 2)
        }
    } else {
        return @{
            Exists = $false
            Path = $Path
            Category = $Category
            Missing = $true
        }
    }
}

function Test-MarkdownSyntax {
    param([string]$FilePath)
    
    $issues = @()
    $content = Get-Content $FilePath -Raw
    
    # Check for broken internal links
    $linkPattern = '\[([^\]]+)\]\(([^\)]+)\)'
    $linkMatches = [regex]::Matches($content, $linkPattern)
    
    foreach ($match in $linkMatches) {
        $linkTarget = $match.Groups[2].Value
        if ($linkTarget -match '^\.\.?/') {
            $targetPath = Join-Path (Split-Path $FilePath -Parent) $linkTarget
            if (-not (Test-Path $targetPath)) {
                $issues += @{
                    Type = "BrokenLink"
                    Line = $match.Index
                    Text = $match.Value
                    Target = $linkTarget
                }
            }
        }
    }
    
    # Check for unclosed code blocks
    $codeBlockCount = ([regex]::Matches($content, '```')).Count
    if ($codeBlockCount % 2 -ne 0) {
        $issues += @{
            Type = "UnclosedCodeBlock"
            Count = $codeBlockCount
        }
    }
    
    return $issues
}

function Get-DocumentationCoverage {
    param([hashtable]$FileChecks)
    
    $total = $FileChecks.Count
    $existing = ($FileChecks.Values | Where-Object { $_.Exists }).Count
    
    return @{
        Total = $total
        Existing = $existing
        Missing = $total - $existing
        Percentage = [math]::Round(($existing / $total) * 100, 2)
    }
}

#endregion

#region Main Execution

try {
    Write-Verbose "Validating documentation in: $SpecDir"
    
    $basePath = Join-Path $PSScriptRoot ".." $SpecDir
    
    if (-not (Test-Path $basePath)) {
        throw "Specification directory not found: $basePath"
    }
    
    # Define required documentation files
    $requiredDocs = @{
        # Core specification files
        "spec.md" = "Core"
        "plan.md" = "Core"
        "tasks.md" = "Core"
        "data-model.md" = "Core"
        "research.md" = "Core"
        "quickstart.md" = "Core"
        
        # Analysis and audit files
        "schema-drift-audit.md" = "Database"
        "documentation-update-matrix.md" = "Documentation"
        "test-isolation-validation.md" = "Testing"
        
        # Contract definitions
        "contracts/dao-result-schema.json" = "Contracts"
        "contracts/parameter-schema.json" = "Contracts"
        "contracts/stored-procedure-contract.json" = "Contracts"
        
        # Checklist files
        "checklists/requirements.md" = "Checklists"
    }
    
    # Check all required files
    $fileChecks = @{}
    foreach ($doc in $requiredDocs.Keys) {
        $fullPath = Join-Path $basePath $doc
        $fileChecks[$doc] = Test-FileExists -Path $fullPath -Category $requiredDocs[$doc]
    }
    
    # Calculate coverage
    $coverage = Get-DocumentationCoverage -FileChecks $fileChecks
    
    # Validate markdown syntax for existing files
    $markdownIssues = @{}
    foreach ($doc in $fileChecks.Keys) {
        if ($fileChecks[$doc].Exists -and $doc -match '\.md$') {
            $issues = Test-MarkdownSyntax -FilePath $fileChecks[$doc].Path
            if ($issues.Count -gt 0) {
                $markdownIssues[$doc] = $issues
            }
        }
    }
    
    # Check for outdated documents (>30 days old)
    $outdatedDocs = @()
    $thirtyDaysAgo = (Get-Date).AddDays(-30)
    foreach ($doc in $fileChecks.Keys) {
        if ($fileChecks[$doc].Exists) {
            if ($fileChecks[$doc].LastModified -lt $thirtyDaysAgo) {
                $outdatedDocs += @{
                    File = $doc
                    LastModified = $fileChecks[$doc].LastModified
                    DaysOld = ((Get-Date) - $fileChecks[$doc].LastModified).Days
                }
            }
        }
    }
    
    # Determine overall status
    $passStatus = $coverage.Percentage -ge $MinCoverage -and $markdownIssues.Count -eq 0
    $overallStatus = if ($passStatus) { "PASS" } else { "FAIL" }
    
    # Build result object
    $result = @{
        OverallStatus = $overallStatus
        Coverage = @{
            Percentage = $coverage.Percentage
            Total = $coverage.Total
            Existing = $coverage.Existing
            Missing = $coverage.Missing
            MeetsMinimum = ($coverage.Percentage -ge $MinCoverage)
        }
        Files = $fileChecks
        MarkdownIssues = $markdownIssues
        OutdatedDocs = $outdatedDocs
        Recommendations = @()
    }
    
    # Add recommendations
    if ($coverage.Percentage -lt $MinCoverage) {
        $result.Recommendations += "Documentation coverage ($($coverage.Percentage)%) is below minimum ($MinCoverage%)"
    }
    
    if ($markdownIssues.Count -gt 0) {
        $result.Recommendations += "$($markdownIssues.Count) document(s) have markdown syntax issues"
    }
    
    if ($outdatedDocs.Count -gt 0) {
        $result.Recommendations += "$($outdatedDocs.Count) document(s) are over 30 days old and may need review"
    }
    
    if ($passStatus) {
        $result.Recommendations += "All documentation validation checks passed"
    }
    
    # Output results
    if ($Json) {
        $result | ConvertTo-Json -Depth 4
    } else {
        Write-Host "`n=== Documentation Validation Results ===" -ForegroundColor Cyan
        Write-Host "Status: $overallStatus" -ForegroundColor $(if ($passStatus) { "Green" } else { "Red" })
        Write-Host "`nCoverage: $($coverage.Percentage)% ($($coverage.Existing)/$($coverage.Total))" -ForegroundColor $(if ($coverage.Percentage -ge $MinCoverage) { "Green" } else { "Yellow" })
        
        if ($coverage.Missing -gt 0) {
            Write-Host "`nMissing Documents:" -ForegroundColor Yellow
            foreach ($doc in $fileChecks.Keys) {
                if (-not $fileChecks[$doc].Exists) {
                    Write-Host "  - $doc" -ForegroundColor Yellow
                }
            }
        }
        
        if ($markdownIssues.Count -gt 0) {
            Write-Host "`nMarkdown Issues:" -ForegroundColor Yellow
            foreach ($doc in $markdownIssues.Keys) {
                Write-Host "  $doc`: $($markdownIssues[$doc].Count) issue(s)" -ForegroundColor Yellow
            }
        }
        
        if ($outdatedDocs.Count -gt 0) {
            Write-Host "`nOutdated Documents (>30 days):" -ForegroundColor Yellow
            foreach ($outdated in $outdatedDocs) {
                Write-Host "  - $($outdated.File): $($outdated.DaysOld) days old" -ForegroundColor Yellow
            }
        }
        
        Write-Host "`nRecommendations:" -ForegroundColor Cyan
        foreach ($rec in $result.Recommendations) {
            Write-Host "  - $rec" -ForegroundColor White
        }
        
        Write-Host ""
    }
    
    exit $(if ($passStatus) { 0 } else { 1 })
    
} catch {
    if ($Json) {
        @{
            OverallStatus = "ERROR"
            Error = $_.Exception.Message
            StackTrace = $_.ScriptStackTrace
        } | ConvertTo-Json
    } else {
        Write-Host "ERROR: $_" -ForegroundColor Red
        Write-Host $_.ScriptStackTrace -ForegroundColor Red
    }
    exit 1
}

#endregion
