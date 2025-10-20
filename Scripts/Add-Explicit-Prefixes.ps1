# Add explicit p_ prefixes to all parameters that are missing them
# This script adds p_ prefix to parameter names in Dictionary declarations

$workspaceRoot = "c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"

Write-Host "=== Adding Explicit p_ Prefixes to Parameters ===" -ForegroundColor Cyan
Write-Host ""

# Define the replacements - map unprefixed parameter names to their prefixed versions
# These are the 28 parameters found without prefixes
$replacements = @{
    '["Operation"]' = '["p_Operation"]'
    '["User"]' = '["p_User"]'
    '["UserId"]' = '["p_UserId"]'
    '["PartID"]' = '["p_PartID"]'
    '["ID"]' = '["p_ID"]'
}

$filesModified = 0
$totalReplacements = 0

# Get all C# files
$csFiles = Get-ChildItem -Path $workspaceRoot -Filter "*.cs" -Recurse -Exclude "bin","obj" |
    Where-Object { $_.FullName -notmatch "\\bin\\|\\obj\\" }

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    $modified = $false
    $fileReplacements = 0
    
    foreach ($key in $replacements.Keys) {
        $oldValue = $key
        $newValue = $replacements[$key]
        
        # Count how many replacements will be made
        $matchCount = ([regex]::Matches($content, [regex]::Escape($oldValue))).Count
        
        if ($matchCount -gt 0) {
            $content = $content.Replace($oldValue, $newValue)
            $modified = $true
            $fileReplacements += $matchCount
        }
    }
    
    if ($modified) {
        Set-Content $file.FullName $content -NoNewline
        $filesModified++
        $totalReplacements += $fileReplacements
        Write-Host "✅ Modified: $($file.Name) ($fileReplacements replacements)" -ForegroundColor Green
    }
}

Write-Host ""
Write-Host "=== Summary ===" -ForegroundColor Cyan
Write-Host "Files modified: $filesModified" -ForegroundColor Yellow
Write-Host "Total parameter prefixes added: $totalReplacements" -ForegroundColor Yellow
Write-Host ""
Write-Host "✅ All explicit prefixes added!" -ForegroundColor Green
