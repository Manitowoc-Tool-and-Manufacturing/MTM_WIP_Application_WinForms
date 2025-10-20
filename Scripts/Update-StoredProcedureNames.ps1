<#
.SYNOPSIS
    Updates old stored procedure names in C# code to match current naming conventions.

.DESCRIPTION
    This script identifies and fixes old procedure names that have been renamed
    to follow current naming standards (e.g., sys_GetUserIdByName → sys_user_GetIdByName).
    
    After running this, the Find-MissingStoredProcedures.ps1 script will show
    only truly missing procedures that need to be created.

.NOTES
    Created: 2025-10-14
    Part of database layer cleanup
#>

[CmdletBinding()]
param(
    [switch]$WhatIf
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path $PSScriptRoot -Parent

Write-Host "`n=== Stored Procedure Name Standardization ===" -ForegroundColor Cyan

# Define old → new name mappings
$nameMappings = @{
    # Test file using old name
    'inv_inventory_Add' = 'inv_inventory_Add_Item'
    
    # System procedures with naming convention updates  
    'sys_SetUserAccessType' = 'sys_user_access_SetType'
    'sys_GetUserIdByName' = 'sys_user_GetIdByName'
}

$filesModified = 0
$replacementsMade = 0

foreach ($oldName in $nameMappings.Keys) {
    $newName = $nameMappings[$oldName]
    
    Write-Host "`nSearching for: $oldName → $newName" -ForegroundColor Yellow
    
    # Find files containing the old name
    $files = Get-ChildItem -Path $repoRoot -Filter "*.cs" -Recurse |
        Where-Object { $_.FullName -notmatch "\\obj\\|\\bin\\" } |
        Where-Object { (Get-Content $_.FullName -Raw) -match [regex]::Escape($oldName) }
    
    foreach ($file in $files) {
        $content = Get-Content -Path $file.FullName -Raw
        $newContent = $content -replace [regex]::Escape("""$oldName"""), """$newName"""
        
        if ($content -ne $newContent) {
            $relativePath = $file.FullName.Replace($repoRoot, "").TrimStart('\')
            
            if ($WhatIf) {
                Write-Host "  [WHATIF] Would update: $relativePath" -ForegroundColor Magenta
            } else {
                [System.IO.File]::WriteAllText($file.FullName, $newContent, [System.Text.Encoding]::UTF8)
                Write-Host "  ✅ Updated: $relativePath" -ForegroundColor Green
                $filesModified++
            }
            $replacementsMade++
        }
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan

if ($WhatIf) {
    Write-Host "Would update $filesModified files with $replacementsMade replacements" -ForegroundColor Magenta
} else {
    Write-Host "Updated $filesModified files with $replacementsMade replacements" -ForegroundColor Green
    
    if ($filesModified -gt 0) {
        Write-Host "`n✅ Procedure names standardized!" -ForegroundColor Green
        Write-Host "Run Find-MissingStoredProcedures.ps1 again to see updated missing list" -ForegroundColor Cyan
    } else {
        Write-Host "`nℹ️  No files needed updating" -ForegroundColor Gray
    }
}
