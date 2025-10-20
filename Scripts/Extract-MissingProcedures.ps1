#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Extracts missing stored procedures from CurrentStoredProcedures.md and creates individual SQL files.

.DESCRIPTION
    Reads the missing procedures report, searches for them in CurrentStoredProcedures.md,
    and creates properly formatted SQL files with correct status codes (p_Status and p_ErrorMsg).
#>

param()

$ErrorActionPreference = "Stop"

# Paths
$repoRoot = Split-Path $PSScriptRoot -Parent
$mdFile = Join-Path $repoRoot "CurrentStoredProcedures.md"
$outputFolder = Join-Path $repoRoot "Database\CurrentStoredProcedures"
$reportPath = Join-Path $outputFolder "MISSING_PROCEDURES_REPORT.txt"

# Missing procedures from the report
$missingProcedures = @(
    'inv_inventory_Get_All',
    'inv_inventory_GetNextBatchNumber',
    'inv_inventory_Search_Advanced',
    'inv_transaction_Add',
    'inv_transaction_log',
    'inv_transactions_GetAnalytics',
    'inv_transactions_Search',
    'inv_transactions_SmartSearch',
    'md_item_types_Exists_ByType',
    'md_item_types_GetDistinct',
    'md_locations_Exists_ByLocation',
    'md_operation_numbers_Exists_ByOperation',
    'md_part_ids_GetItemType_ByPartID',
    'sys_GetRoleIdByName',
    'sys_last_10_transactions_Add_AtPosition',
    'sys_last_10_transactions_Delete_ByUserAndPosition_1',
    'sys_last_10_transactions_DeleteAll_ByUser',
    'sys_last_10_transactions_Move',
    'usr_ui_settings_Delete_ByUserId',
    'usr_ui_settings_GetJsonSetting',
    'usr_user_roles_GetRoleId_ByUserId',
    'usr_users_SetUserSetting_ByUserAndField'
)

Write-Host "`n=== Extracting Missing Stored Procedures ===" -ForegroundColor Cyan
Write-Host "Reading from: $mdFile" -ForegroundColor Gray
Write-Host "Output to: $outputFolder`n" -ForegroundColor Gray

# Read the entire markdown file
$mdContent = Get-Content -Path $mdFile -Raw

$foundCount = 0
$notFoundCount = 0
$notFoundList = @()

foreach ($procName in $missingProcedures) {
    # Pattern to find the procedure definition - more flexible to handle incomplete procedures
    $pattern = "(?s)DELIMITER \`$\`$\s*[\r\n]+CREATE DEFINER=\``root\``@\``localhost\`` PROCEDURE \``$procName\``.*?(?=DELIMITER ;|DELIMITER \`$\`$|$)"
    
    if ($mdContent -match $pattern) {
        $procedureText = $Matches[0]
        
        # Check if procedure already has p_Status and p_ErrorMsg
        $hasStatusParams = $procedureText -match "OUT\s+p_Status\s+INT" -and $procedureText -match "OUT\s+p_ErrorMsg\s+VARCHAR"
        
        if (-not $hasStatusParams) {
            Write-Host "  [NEEDS FIX] $procName - Missing status parameters" -ForegroundColor Yellow
            
            # Extract parameter list
            if ($procedureText -match "PROCEDURE ``$procName``\(([^\)]+)\)") {
                $params = $Matches[1].Trim()
                
                # Add status parameters if not empty params
                if ($params -ne "") {
                    $params = "$params,`n    OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)"
                } else {
                    $params = "OUT p_Status INT,`n    OUT p_ErrorMsg VARCHAR(500)"
                }
                
                # Rebuild procedure with proper format
                $bodyStart = $procedureText.IndexOf("BEGIN")
                $bodyEnd = $procedureText.LastIndexOf("END`$`$")
                
                if ($bodyStart -gt 0 -and $bodyEnd -gt $bodyStart) {
                    $body = $procedureText.Substring($bodyStart, $bodyEnd - $bodyStart + 5)
                    
                    # Add error handler and status setting if not present
                    if ($body -notmatch "DECLARE EXIT HANDLER FOR SQLEXCEPTION") {
                        $body = $body -replace "BEGIN", @"
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred in $procName';
    END;
"@
                    }
                    
                    # Add success status at end if not present
                    if ($body -notmatch "SET p_Status\s*=\s*[01]") {
                        $body = $body -replace "END\$\$", @"
    
    SET p_Status = 1;
    SET p_ErrorMsg = NULL;
END`$`$
"@
                    }
                    
                    $newProcedure = @"
DROP PROCEDURE IF EXISTS ``$procName``;
DELIMITER `$`$
CREATE DEFINER=``root``@``localhost`` PROCEDURE ``$procName``(
    $params
)
$body
DELIMITER ;
"@
                    
                    # Write to file
                    $outputPath = Join-Path $outputFolder "$procName.sql"
                    [System.IO.File]::WriteAllText($outputPath, $newProcedure, [System.Text.Encoding]::UTF8)
                    Write-Host "  [CREATED] $outputPath" -ForegroundColor Green
                    $foundCount++
                }
            }
        } else {
            # Already has proper format, just add DROP IF EXISTS
            $newProcedure = "DROP PROCEDURE IF EXISTS ``$procName``;`n" + $procedureText
            
            $outputPath = Join-Path $outputFolder "$procName.sql"
            [System.IO.File]::WriteAllText($outputPath, $newProcedure, [System.Text.Encoding]::UTF8)
            Write-Host "  [CREATED] $outputPath" -ForegroundColor Green
            $foundCount++
        }
    } else {
        Write-Host "  [NOT FOUND] $procName" -ForegroundColor Red
        $notFoundCount++
        $notFoundList += $procName
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Found and extracted: $foundCount" -ForegroundColor Green
Write-Host "Not found in markdown: $notFoundCount" -ForegroundColor $(if ($notFoundCount -gt 0) { "Yellow" } else { "Green" })

if ($notFoundList.Count -gt 0) {
    Write-Host "`n=== Procedures NOT FOUND (need to be created from scratch) ===" -ForegroundColor Yellow
    foreach ($proc in $notFoundList) {
        Write-Host "  - $proc"
    }
}

Write-Host "`nExtraction complete!" -ForegroundColor Cyan
