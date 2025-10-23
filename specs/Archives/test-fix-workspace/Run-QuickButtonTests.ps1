# Run Quick Button Tests and Capture Results
# Usage: .\Run-QuickButtonTests.ps1

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Category 1: Quick Button Tests" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan

# Run tests and capture output
$testOutput = dotnet test MTM_Inventory_Application.csproj `
    --filter "FullyQualifiedName~Integration.Dao_QuickButtons_Tests" `
    --logger:"console;verbosity=normal" `
    2>&1

# Display output
$testOutput | ForEach-Object { Write-Host $_ }

# Save to file
$testOutput | Out-File -FilePath "test-results-quickbuttons-$(Get-Date -Format 'yyyy-MM-dd-HHmmss').txt"

# Parse results
$passedLine = $testOutput | Select-String -Pattern "Passed!" 
$failedLine = $testOutput | Select-String -Pattern "Failed!"
$totalLine = $testOutput | Select-String -Pattern "Total tests:"

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Test Summary" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if ($passedLine) {
    Write-Host "✅ ALL TESTS PASSED!" -ForegroundColor Green
    Write-Host $passedLine -ForegroundColor Green
} elseif ($failedLine) {
    Write-Host "⚠️ SOME TESTS FAILED" -ForegroundColor Yellow
    Write-Host $failedLine -ForegroundColor Yellow
} else {
    Write-Host "📊 Check output above for details" -ForegroundColor White
}

if ($totalLine) {
    Write-Host $totalLine -ForegroundColor White
}

Write-Host "`nResults saved to: test-results-quickbuttons-*.txt" -ForegroundColor Cyan
Write-Host "========================================`n" -ForegroundColor Cyan
