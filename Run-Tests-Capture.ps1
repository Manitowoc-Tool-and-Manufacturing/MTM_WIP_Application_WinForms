# Run Tests and Capture Full Output
# This script runs the tests and ensures all output is captured to a file

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Running Category 1: Quick Button Tests" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

# Define output file
$outputFile = "test-results-$(Get-Date -Format 'yyyy-MM-dd-HHmmss').txt"
$tempFile = "test-temp-output.txt"

Write-Host "Output will be saved to: $outputFile" -ForegroundColor Gray
Write-Host "Running tests (this may take 10-30 seconds)...`n" -ForegroundColor Gray

# Run tests and capture output using Out-File with explicit flushing
# The trick is to NOT redirect during Start-Process but capture after
$null = @"
========================================
Test Execution Started: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
========================================

"@ | Out-File -FilePath $outputFile -Encoding UTF8

# Run dotnet test and capture ALL output (both stdout and stderr)
$process = Start-Process -FilePath "dotnet" `
    -ArgumentList "test", "MTM_Inventory_Application.csproj", 
                  "--filter", "FullyQualifiedName~Integration.Dao_QuickButtons_Tests",
                  "--logger:console;verbosity=detailed",
                  "--nologo" `
    -NoNewWindow `
    -Wait `
    -PassThru `
    -RedirectStandardOutput $tempFile `
    -RedirectStandardError "${tempFile}.err"

# Append captured output to our output file
if (Test-Path $tempFile) {
    Get-Content $tempFile | Out-File -FilePath $outputFile -Append -Encoding UTF8
}
if (Test-Path "${tempFile}.err") {
    "`n========================================" | Out-File -FilePath $outputFile -Append -Encoding UTF8
    "STDERR Output:" | Out-File -FilePath $outputFile -Append -Encoding UTF8
    "========================================`n" | Out-File -FilePath $outputFile -Append -Encoding UTF8
    Get-Content "${tempFile}.err" | Out-File -FilePath $outputFile -Append -Encoding UTF8
}

# Add completion timestamp
@"

========================================
Test Execution Completed: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
Exit Code: $($process.ExitCode)
========================================
"@ | Out-File -FilePath $outputFile -Append -Encoding UTF8

# Display the captured output to console
Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Test Output:" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor White

$content = Get-Content $outputFile
$content | ForEach-Object { Write-Host $_ }

# Parse results from the output
$contentString = $content -join "`n"

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Summary" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Try multiple patterns for test results
$passed = 0
$failed = 0
$skipped = 0
$total = 0
$foundResults = $false

# Pattern 1: "Passed! - Failed: 0, Passed: 10, Skipped: 0, Total: 10"
if ($contentString -match "(?:Passed!|Failed!)\s*-\s*Failed:\s*(\d+),\s*Passed:\s*(\d+),\s*Skipped:\s*(\d+)(?:,\s*Total:\s*(\d+))?") {
    $failed = [int]$matches[1]
    $passed = [int]$matches[2]
    $skipped = [int]$matches[3]
    if ($matches[4]) { $total = [int]$matches[4] }
    $foundResults = $true
}
# Pattern 2: Individual counts
elseif ($contentString -match "Passed:\s*(\d+)") {
    $passed = [int]$matches[1]
    if ($contentString -match "Failed:\s*(\d+)") { $failed = [int]$matches[1] }
    if ($contentString -match "Skipped:\s*(\d+)") { $skipped = [int]$matches[1] }
    if ($contentString -match "Total:\s*(\d+)") { $total = [int]$matches[1] }
    $foundResults = $true
}

if ($foundResults) {
    if ($total -eq 0) { $total = $passed + $failed + $skipped }
    
    if ($failed -eq 0 -and $passed -gt 0) {
        Write-Host "✅ ALL TESTS PASSED!" -ForegroundColor Green
    } elseif ($failed -gt 0) {
        Write-Host "⚠️ SOME TESTS FAILED" -ForegroundColor Yellow
    } else {
        Write-Host "📊 Tests Completed" -ForegroundColor White
    }
    
    Write-Host "   Total:   $total" -ForegroundColor White
    Write-Host "   Passed:  $passed" -ForegroundColor $(if ($passed -gt 0) { "Green" } else { "Gray" })
    Write-Host "   Failed:  $failed" -ForegroundColor $(if ($failed -gt 0) { "Red" } else { "Gray" })
    Write-Host "   Skipped: $skipped" -ForegroundColor $(if ($skipped -gt 0) { "Yellow" } else { "Gray" })
} else {
    Write-Host "⚠️ Unable to parse test results" -ForegroundColor Yellow
    Write-Host "   This may indicate tests didn't run or output format changed" -ForegroundColor Yellow
    Write-Host "   Please check the output file for details" -ForegroundColor Yellow
}

Write-Host "`nExit Code: $($process.ExitCode)" -ForegroundColor Cyan
Write-Host "Full output saved to: $outputFile" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

# Clean up temp files
Remove-Item $tempFile -ErrorAction SilentlyContinue
Remove-Item "${tempFile}.err" -ErrorAction SilentlyContinue

# Return exit code
exit $process.ExitCode
