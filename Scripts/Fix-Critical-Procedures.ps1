# Final targeted fix for critical procedures
# Fixes OUT parameters and removes unconditional SET p_Status = 0

$criticals = @('md_item_types_Get_All','md_locations_Get_All','md_operation_numbers_Get_All','md_part_ids_Get_All','sys_user_roles_Update','usr_users_Delete_User','usr_users_Exists','usr_users_Get_All','usr_users_Get_ByUser')

foreach ($procName in $criticals) {
    $file = "Database\CurrentStoredProcedures\$procName.sql"
    
    Write-Host "Processing: $procName" -ForegroundColor Cyan
    
    $lines = Get-Content $file
    $newLines = @()
    $inProcSignature = $false
    $signatureFixed = $false
    $foundProcStart = $false
    
    for ($i = 0; $i < $lines.Count; $i++) {
        $line = $lines[$i]
        
        # Detect CREATE PROCEDURE line
        if ($line -match "CREATE.*PROCEDURE\s+``$procName``") {
            $foundProcStart = $true
            $newLines += $line
            
            # Check next line for parameters or empty ()
            if ($i + 1 -lt $lines.Count) {
                $nextLine = $lines[$i + 1]
                if ($nextLine.Trim() -eq "()") {
                    # Empty parameters - replace with OUT params
                    $newLines += "("
                    $newLines += "    OUT p_Status INT,"
                    $newLines += "    OUT p_ErrorMsg VARCHAR(500)"
                    $newLines += ")"
                    $i++ # Skip the () line
                    $signatureFixed = $true
                    continue
                } elseif ($nextLine.Trim() -eq "(") {
                    # Parameters start - need to find end and add OUT params
                    $newLines += "("
                    $i++
                    $paramLines = @()
                    
                    # Collect all parameter lines until we find )
                    while ($i < $lines.Count) {
                        $paramLine = $lines[$i]
                        if ($paramLine.Trim() -eq ")") {
                            # End of parameters - add OUT params before closing
                            if ($paramLines.Count -gt 0) {
                                # Add existing params with comma
                                $newLines += $paramLines
                                $newLines += "    OUT p_Status INT,"
                                $newLines += "    OUT p_ErrorMsg VARCHAR(500)"
                            }
                            $newLines += ")"
                            $signatureFixed = $true
                            break
                        } else {
                            $paramLines += $paramLine
                        }
                        $i++
                    }
                    continue
                }
            }
        }
        
        # Remove unconditional SET p_Status = 0 at end
        if ($line.Trim() -eq "SET p_Status = 0;" -and $i + 1 -lt $lines.Count -and $lines[$i + 1] -match "END\$\$") {
            Write-Host "  Removing unconditional SET p_Status = 0" -ForegroundColor Yellow
            # Skip this line and check if next line also has SET p_ErrorMsg
            if ($i + 1 -lt $lines.Count -and $lines[$i + 1].Trim().StartsWith("SET p_ErrorMsg")) {
                $i++ # Skip error msg line too
            }
            continue
        }
        
        $newLines += $line
    }
    
    if ($signatureFixed) {
        Set-Content $file -Value $newLines
        Write-Host "  ✓ Fixed procedure signature" -ForegroundColor Green
    } else {
        Write-Host "  ⚠ Could not fix signature automatically" -ForegroundColor Yellow
    }
}

Write-Host "`n✅ Processing complete!" -ForegroundColor Cyan
