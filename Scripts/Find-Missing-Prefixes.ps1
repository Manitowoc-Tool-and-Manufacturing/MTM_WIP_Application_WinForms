# Find parameters being passed WITHOUT prefixes that should have them
# This looks for dictionary entries like ["User"] instead of ["p_User"]

$workspaceRoot = "c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms"
$findings = @()

# Get all C# files
$csFiles = Get-ChildItem -Path $workspaceRoot -Filter "*.cs" -Recurse -Exclude "bin","obj" | 
    Where-Object { $_.FullName -notmatch "\\bin\\|\\obj\\" }

Write-Host "=== Searching for parameters without prefixes ===" -ForegroundColor Cyan
Write-Host "Files to scan: $($csFiles.Count)" -ForegroundColor Yellow
Write-Host ""

foreach ($file in $csFiles) {
    $content = Get-Content $file.FullName -Raw
    
    # Look for dictionary parameter patterns like ["SomeParameter"]
    # that appear after ExecuteDataTableWithStatus, ExecuteNonQueryWithStatus, or ExecuteScalarWithStatus
    $matches = [regex]::Matches($content, 'Execute(?:DataTable|NonQuery|Scalar)(?:WithStatus|Async)[\s\S]{0,500}?new Dictionary<string, object>[\s\S]{0,1000}?\["([^"]+)"\]')
    
    foreach ($match in $matches) {
        $paramName = $match.Groups[1].Value
        
        # Skip if it already has a prefix
        if ($paramName -match '^(p_|o_|in_)') {
            continue
        }
        
        # Skip common non-parameter names
        if ($paramName -in @('Status', 'ErrorMsg', 'ErrorMessage', 'OutputParameters')) {
            continue
        }
        
        $findings += [PSCustomObject]@{
            File = $file.Name
            FullPath = $file.FullName
            Parameter = $paramName
            Context = $match.Value.Substring(0, [Math]::Min(200, $match.Value.Length))
        }
    }
}

Write-Host "=== Results ===" -ForegroundColor Cyan
Write-Host ""

if ($findings.Count -eq 0) {
    Write-Host "✅ No parameters found without prefixes!" -ForegroundColor Green
} else {
    Write-Host "❌ Found $($findings.Count) parameters potentially missing prefixes:" -ForegroundColor Red
    Write-Host ""
    
    $groupedByFile = $findings | Group-Object File
    foreach ($group in $groupedByFile) {
        Write-Host "$($group.Name):" -ForegroundColor Yellow
        foreach ($finding in $group.Group) {
            Write-Host "  - Parameter: $($finding.Parameter)" -ForegroundColor White
        }
        Write-Host ""
    }
}

Write-Host ""
Write-Host "=== Summary ===" -ForegroundColor Cyan
Write-Host "Total parameters without prefixes: $($findings.Count)" -ForegroundColor Yellow
