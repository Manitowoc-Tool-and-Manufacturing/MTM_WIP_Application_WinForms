#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Generates a structured suggestion file from audit results.

.DESCRIPTION
    Creates markdown suggestion files following the MTM WIP Application suggestion template.
    Handles categorization, deduplication, and proper file organization.

.PARAMETER TargetFile
    The file that was audited (e.g., "Control_Edit_PartID.cs")

.PARAMETER Category
    Suggestion category: "file_only", "system", or "multi_file"

.PARAMETER SystemName
    For system/multi_file categories, the system being affected (e.g., "Service_ErrorHandler")

.PARAMETER Suggestions
    JSON array of suggestion objects with properties: id, title, priority, scope, description, etc.

.PARAMETER Force
    Overwrite existing suggestion file without prompting

.EXAMPLE
    .\Generate-SuggestionFile.ps1 -TargetFile "Control_Edit_PartID.cs" -Category "file_only" -Suggestions $suggestionsJson
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$TargetFile,

    [Parameter(Mandatory = $true)]
    [ValidateSet("file_only", "system", "multi_file")]
    [string]$Category,

    [Parameter(Mandatory = $false)]
    [string]$SystemName,

    [Parameter(Mandatory = $true)]
    [string]$Suggestions,

    [Parameter(Mandatory = $false)]
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Load configuration
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$templatePath = Join-Path $scriptDir "suggestion-template.yml"
$suggestionsBaseDir = Join-Path (Split-Path -Parent $scriptDir) "suggestions"

# Ensure suggestions directory exists
if (-not (Test-Path $suggestionsBaseDir)) {
    New-Item -Path $suggestionsBaseDir -ItemType Directory -Force | Out-Null
}

# Determine output path based on category
$fileName = [System.IO.Path]::GetFileNameWithoutExtension($TargetFile)
switch ($Category) {
    "file_only" {
        $outputPath = Join-Path $suggestionsBaseDir "$fileName-suggestions.md"
    }
    "system" {
        if (-not $SystemName) {
            throw "SystemName parameter required for 'system' category"
        }
        $outputPath = Join-Path $suggestionsBaseDir "$SystemName-suggestions.md"
    }
    "multi_file" {
        $speckitDir = Join-Path $suggestionsBaseDir "speckit"
        if (-not (Test-Path $speckitDir)) {
            New-Item -Path $speckitDir -ItemType Directory -Force | Out-Null
        }
        if (-not $SystemName) {
            $SystemName = $fileName
        }
        $outputPath = Join-Path $speckitDir "$SystemName-suggestion.md"
    }
}

# Parse suggestions JSON
try {
    $suggestionsArray = $Suggestions | ConvertFrom-Json
} catch {
    throw "Failed to parse suggestions JSON: $_"
}

# Check for existing file and handle deduplication
$existingSuggestions = @()
if (Test-Path $outputPath) {
    Write-Host "Existing suggestion file found: $outputPath" -ForegroundColor Yellow
    
    if (-not $Force) {
        $response = Read-Host "File exists. [M]erge new suggestions, [O]verwrite, or [C]ancel? (M/O/C)"
        if ($response -eq 'C') {
            Write-Host "Operation cancelled." -ForegroundColor Yellow
            return
        } elseif ($response -eq 'M') {
            # TODO: Implement merging logic (parse existing markdown, deduplicate)
            Write-Host "Merging not yet implemented. Overwriting instead." -ForegroundColor Yellow
        }
    }
}

# Generate markdown content
$content = @"
# Suggestions: $fileName
**Generated**: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")  
**Category**: $Category  
**Target File**: ``$TargetFile``
$( if ($SystemName) { "**System**: ``$SystemName``" } )

---

## Table of Contents
- [Summary](#summary)
- [Suggestions](#suggestions)
$( foreach ($suggestion in $suggestionsArray) {
    "  - [$($suggestion.id): $($suggestion.title)](#$($suggestion.id.ToLower()))"
})
- [Implementation Notes](#implementation-notes)

---

## Summary

**Total Suggestions**: $($suggestionsArray.Count)  
**Priority Breakdown**:
$( 
    $priorityGroups = $suggestionsArray | Group-Object -Property priority | Sort-Object Name -Descending
    foreach ($group in $priorityGroups) {
        "- Priority $($group.Name): $($group.Count) suggestion(s)"
    }
)

---

## Suggestions

$( 
    foreach ($suggestion in $suggestionsArray) {
        @"

### $($suggestion.id): $($suggestion.title)
**Priority**: $($suggestion.priority)/10 ($($suggestion.priority_label))  
**Scope**: $($suggestion.scope)/10 ($($suggestion.scope_label))
$( if ($suggestion.requires_speckit) { "**Requires Speckit**: ✅ Yes" } )

#### User Benefit
$($suggestion.user_benefit)

#### Why Needed
$($suggestion.why_needed)

#### Implementation
``````$($suggestion.implementation_language -or 'csharp')
$($suggestion.implementation_code)
``````

$( if ($suggestion.diagram) {
@"
#### Diagram
``````mermaid
$($suggestion.diagram)
``````
"@
})

$( if ($suggestion.related_files) {
@"
#### Related Files
$( foreach ($file in $suggestion.related_files) { "- ``$file``" })
"@
})

---
"@
    }
)

## Implementation Notes

### General Guidelines
- Review all suggestions before implementing
- Test each change independently
- Update tests as needed
- Follow constitution compliance requirements

### Dependencies
Check for dependencies between suggestions - some may need to be implemented in sequence.

### Testing Checklist
- [ ] Unit tests pass
- [ ] Integration tests pass  
- [ ] Manual smoke testing complete
- [ ] Constitution compliance verified

---

**Auto-generated by**: ``Generate-SuggestionFile.ps1``  
**Template Version**: 1.0.0
"@

# Write to file
$content | Out-File -FilePath $outputPath -Encoding UTF8 -Force

Write-Host "✅ Suggestion file generated: $outputPath" -ForegroundColor Green
Write-Host "   Total suggestions: $($suggestionsArray.Count)" -ForegroundColor Cyan

return @{
    success = $true
    output_path = $outputPath
    suggestion_count = $suggestionsArray.Count
}
