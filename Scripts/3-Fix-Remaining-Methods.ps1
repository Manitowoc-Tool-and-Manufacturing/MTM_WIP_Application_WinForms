# Script to fix remaining DAO methods that need transaction support
# This handles the methods that weren't in the original analysis

Write-Host "Fixing remaining DAO methods..." -ForegroundColor Cyan

# Fix Dao_Inventory methods
$daoInventory = "Data\Dao_Inventory.cs"
if (Test-Path $daoInventory) {
    $content = Get-Content $daoInventory -Raw

    # GetAllInventoryAsync
    $content = $content -replace 'GetAllInventoryAsync\(\s*\)', 'GetAllInventoryAsync(MySqlConnection? connection = null, MySqlTransaction? transaction = null)'

    # SearchInventoryAsync
    $content = $content -replace 'SearchInventoryAsync\(string searchTerm\)', 'SearchInventoryAsync(string searchTerm, MySqlConnection? connection = null, MySqlTransaction? transaction = null)'

    # Update their Execute calls
    $content = $content -replace '(ExecuteDataTableWithStatusAsync\([^)]+,\s+null,?\s*)(?:progressHelper:\s*)?null\s*\);', '$1progressHelper: null, connection: connection, transaction: transaction);'
    $content = $content -replace '(ExecuteDataTableWithStatusAsync\([^)]+,\s+parameters,?\s*)(?:progressHelper:\s*)?null\s*\);', '$1progressHelper: null, connection: connection, transaction: transaction);'

    $content | Set-Content $daoInventory
    Write-Host "  ✓ Fixed Dao_Inventory.cs" -ForegroundColor Green
}

# Fix Dao_Part methods
$daoPart = "Data\Dao_Part.cs"
if (Test-Path $daoPart) {
    $content = Get-Content $daoPart -Raw

    # GetPartByNumberAsync
    $content = $content -replace 'GetPartByNumberAsync\(string partNumber\)', 'GetPartByNumberAsync(string partNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)'

    # Update Execute calls that still have just `null`
    $content = $content -replace '(ExecuteDataTableWithStatusAsync\([^)]+,\s+parameters,?\s*)null\s*\);', '$1progressHelper: null, connection: connection, transaction: transaction);'

    $content | Set-Content $daoPart
    Write-Host "  ✓ Fixed Dao_Part.cs" -ForegroundColor Green
}

# Fix Dao_Operation methods
$daoOperation = "Data\Dao_Operation.cs"
if (Test-Path $daoOperation) {
    $content = Get-Content $daoOperation -Raw

    # OperationExists
    $content = $content -replace '(ExecuteScalarWithStatusAsync\([^)]+,\s+parameters,?\s*)null\s*\);', '$1progressHelper: null, connection: connection, transaction: transaction);'

    $content | Set-Content $daoOperation
    Write-Host "  ✓ Fixed Dao_Operation.cs" -ForegroundColor Green
}

# Fix Dao_System (GetAllThemesAsync uses its own connection, remove test calls)
Write-Host "  ℹ Dao_System.GetAllThemesAsync uses its own connection - tests need updating, not DAO" -ForegroundColor Yellow

# Fix test files - remove transaction parameters from methods that don't support them
$testFiles = @(
    "Tests\Integration\ConnectionPooling_Tests.cs",
    "Tests\Integration\TransactionManagement_Tests.cs",
    "Tests\Integration\Dao_System_Tests.cs"
)

foreach ($testFile in $testFiles) {
    if (Test-Path $testFile) {
        $content = Get-Content $testFile -Raw

        # Remove duplicate TestContext properties (already in BaseIntegrationTest)
        $content = $content -replace '(?m)^\s+public TestContext\? TestContext \{ get; set; \}\s*$', ''

        # Remove connection/transaction args from GetAllThemesAsync calls (it doesn't support them)
        $content = $content -replace 'GetAllThemesAsync\(connection:\s*GetTestConnection\(\),\s*transaction:\s*GetTestTransaction\(\)\)', 'GetAllThemesAsync()'

        # Remove connection/transaction args from GetTestConnection/GetTestTransaction that are undefined
        # These should be removed since GetTestConnection/GetTestTransaction don't exist in those test classes
        $content = $content -replace 'connection:\s*GetTestConnection\(\),\s*transaction:\s*GetTestTransaction\(\)', ''

        $content | Set-Content $testFile
        Write-Host "  ✓ Fixed $testFile" -ForegroundColor Green
    }
}

Write-Host "`nAttempting build..." -ForegroundColor Cyan
$buildResult = dotnet build MTM_WIP_Application_Winforms.csproj -c Debug --nologo 2>&1 | Out-String
$buildSuccess = $buildResult -match "Build succeeded"

if ($buildSuccess) {
    Write-Host "✓ Build succeeded!" -ForegroundColor Green
    $buildResult -split "`n" | Select-Object -Last 5 | ForEach-Object { Write-Host $_ }
} else {
    Write-Host "✗ Build failed. Remaining errors:" -ForegroundColor Red
    $buildResult -split "`n" | Where-Object { $_ -match "error CS" } | Select-Object -First 10 | ForEach-Object { Write-Host $_ -ForegroundColor Red }
}
