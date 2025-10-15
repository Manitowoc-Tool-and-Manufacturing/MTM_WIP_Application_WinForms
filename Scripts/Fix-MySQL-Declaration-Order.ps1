<#
.SYNOPSIS
    Fixes MySQL declaration order issues in stored procedures

.DESCRIPTION
    MySQL requires declarations in this order:
    1. DECLARE variables
    2. DECLARE cursors  
    3. DECLARE handlers (EXIT HANDLER, CONTINUE HANDLER)
    
    This script reorders declarations to comply with MySQL syntax rules.
#>

$proceduresFolder = "Database\CurrentStoredProcedures"
$files = Get-ChildItem -Path $proceduresFolder -Filter "*.sql" | Where-Object { 
    $_.Name -notlike "*REPORT*" -and 
    $_.Name -notlike "*UNUSED*" 
}

Write-Host "`n=== Fixing MySQL Declaration Order ===" -ForegroundColor Cyan
Write-Host "Processing $($files.Count) files`n" -ForegroundColor Yellow

$fixedCount = 0
$errorCount = 0

foreach ($file in $files) {
    try {
        $content = Get-Content $file.FullName -Raw
        
        # Check if file needs fixing (has EXIT HANDLER before DECLARE v_Count)
        if ($content -match "DECLARE EXIT HANDLER.*?END;.*?DECLARE v_Count") {
            Write-Host "Fixing: $($file.BaseName)" -ForegroundColor Yellow
            
            # Extract the EXIT HANDLER block
            if ($content -match "(?s)(DECLARE EXIT HANDLER FOR SQLEXCEPTION\s*BEGIN.*?END;)") {
                $exitHandler = $Matches[1]
                
                # Remove the EXIT HANDLER from its current position
                $content = $content -replace [regex]::Escape($exitHandler), ""
                
                # Find where to insert it (after all variable declarations)
                # Insert after the last DECLARE variable line before BEGIN main logic
                $content = $content -replace "(DECLARE v_Count INT DEFAULT 0;)", "`$1`r`n`r`n    $exitHandler"
                
                # Save fixed content
                $content | Out-File -FilePath $file.FullName -Encoding UTF8 -NoNewline
                $fixedCount++
                Write-Host "  ✅ Fixed" -ForegroundColor Green
            }
        }
    }
    catch {
        Write-Host "  ❌ Error: $_" -ForegroundColor Red
        $errorCount++
    }
}

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Fixed: $fixedCount" -ForegroundColor Green
Write-Host "Errors: $errorCount" -ForegroundColor Red
