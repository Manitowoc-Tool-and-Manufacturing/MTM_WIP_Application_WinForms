# Fix-TestFiles-DaoResult.ps1
# Updates integration test files to handle DaoResult<DataTable> return types from Dao_ErrorLog methods

$testFiles = @(
    "ErrorLogging_Tests.cs",
    "ErrorCooldown_Tests.cs"
)

$testsPath = Join-Path $PSScriptRoot "..\Tests\Integration"

foreach ($file in $testFiles) {
    $filePath = Join-Path $testsPath $file
    if (-not (Test-Path $filePath)) {
        Write-Warning "File not found: $filePath"
        continue
    }

    Write-Host "Processing $file..." -ForegroundColor Cyan
    $content = Get-Content $filePath -Raw

    # Pattern 1: Fix GetAllErrorsAsync assignments with immediate .Data access
    $content = $content -replace '(\s+)var (errors|allErrors|errorsBefore|errorsAfter) = await Dao_ErrorLog\.GetAllErrorsAsync\(\);',
        ('$1var $2Result = await Dao_ErrorLog.GetAllErrorsAsync();' + [Environment]::NewLine + '$1Assert.IsTrue($2Result.IsSuccess, "GetAllErrorsAsync should succeed");' + [Environment]::NewLine + '$1var $2 = $2Result.Data!;')

    # Pattern 2: Fix GetErrorsByUserAsync
    $content = $content -replace '(\s+)var (userErrors) = await Dao_ErrorLog\.GetErrorsByUserAsync\(([^;]+)\);',
        ('$1var $2Result = await Dao_ErrorLog.GetErrorsByUserAsync($3);' + [Environment]::NewLine + '$1Assert.IsTrue($2Result.IsSuccess, "GetErrorsByUserAsync should succeed");' + [Environment]::NewLine + '$1var $2 = $2Result.Data!;')

    # Pattern 3: Fix GetErrorsByDateRangeAsync  
    $content = $content -replace '(\s+)var (dateRangeErrors) = await Dao_ErrorLog\.GetErrorsByDateRangeAsync\(([^;]+)\);',
        ('$1var $2Result = await Dao_ErrorLog.GetErrorsByDateRangeAsync($3);' + [Environment]::NewLine + '$1Assert.IsTrue($2Result.IsSuccess, "GetErrorsByDateRangeAsync should succeed");' + [Environment]::NewLine + '$1var $2 = $2Result.Data!;')

    # Pattern 4: Fix inline GetAllErrorsAsync().Rows.Count pattern
    $content = $content -replace '\(await Dao_ErrorLog\.GetAllErrorsAsync\(\)\)\.Rows\.Count',
        '(await Dao_ErrorLog.GetAllErrorsAsync()).Data!.Rows.Count'

    # Write back
    Set-Content -Path $filePath -Value $content -NoNewline
    Write-Host "  ✓ Fixed $file" -ForegroundColor Green
}

Write-Host "`nTest file fixes complete!" -ForegroundColor Green
Write-Host "Run 'dotnet build' to verify compilation." -ForegroundColor Yellow
