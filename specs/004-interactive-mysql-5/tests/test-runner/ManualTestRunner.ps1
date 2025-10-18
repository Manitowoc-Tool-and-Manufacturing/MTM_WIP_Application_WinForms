param(
    [string]$TestId,
    [string]$OutputDirectory = "./reports",
    [switch]$List
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$definitionPath = Join-Path $PSScriptRoot "manual-test-definitions.json"
if (-not (Test-Path $definitionPath)) {
    throw "Test definition file not found at $definitionPath"
}

$definitionJson = Get-Content -Raw -Path $definitionPath | ConvertFrom-Json
$tests = $definitionJson.tests
if (-not $tests) {
    throw "No tests defined in $definitionPath"
}

if ($List) {
    Write-Host "Available Tests:" -ForegroundColor Cyan
    foreach ($test in $tests) {
        Write-Host "  $($test.id) - $($test.title)" -ForegroundColor Gray
    }
    return
}

if (-not $TestId) {
    Write-Host "Select a test to run:" -ForegroundColor Cyan
    for ($i = 0; $i -lt $tests.Count; $i++) {
        $number = $i + 1
        $item = $tests[$i]
        Write-Host "  [$number] $($item.id) - $($item.title)" -ForegroundColor Gray
    }
    $selection = Read-Host "Enter number"
    if (-not [int]::TryParse($selection, [ref]$null)) {
        throw "Invalid selection"
    }
    $index = [int]$selection - 1
    if ($index -lt 0 -or $index -ge $tests.Count) {
        throw "Selection out of range"
    }
    $test = $tests[$index]
} else {
    $test = $tests | Where-Object { $_.id -eq $TestId }
    if (-not $test) {
        throw "Test with id '$TestId' not found. Use -List to view available tests."
    }
}

Write-Host "`nRunning $($test.id) - $($test.title)" -ForegroundColor Yellow
Write-Host "User Story: $($test.userStory)" -ForegroundColor Gray
Write-Host "Feature: $($test.feature)" -ForegroundColor Gray
Write-Host "Objective: $($test.objective)" -ForegroundColor Gray
Write-Host "Estimated Duration: $($test.estimatedDurationMinutes) minutes" -ForegroundColor Gray

if ($test.prerequisites) {
    Write-Host "`nPrerequisites:" -ForegroundColor Cyan
    $idx = 1
    foreach ($pre in $test.prerequisites) {
        Write-Host ("  [{0}] {1}" -f $idx, $pre) -ForegroundColor Gray
        $idx++
    }
    Write-Host "Confirm prerequisites satisfied? (Y/N)" -ForegroundColor Cyan
    $preCheck = Read-Host
    if ($preCheck -notin @("Y","y","Yes","YES")) {
        Write-Warning "Prerequisites not confirmed. Test may not be executable."
    }
}

$run = [ordered]@{
    testId      = $test.id
    title       = $test.title
    startedAt   = (Get-Date).ToString("s")
    prerequisitesConfirmed = $preCheck -in @("Y","y","Yes","YES")
    steps       = @()
}

foreach ($step in $test.steps) {
    Write-Host "`nStep $($step.number): $($step.title)" -ForegroundColor Yellow
    if ($step.actions) {
        Write-Host "Actions:" -ForegroundColor Cyan
        $aIdx = 1
        foreach ($action in $step.actions) {
            Write-Host ("  ({0}) {1}" -f $aIdx, $action) -ForegroundColor Gray
            $aIdx++
        }
    }
    if ($step.expectedResults) {
        Write-Host "Expected Results:" -ForegroundColor Cyan
        $eIdx = 1
        foreach ($expected in $step.expectedResults) {
            Write-Host ("  ({0}) {1}" -f $eIdx, $expected) -ForegroundColor DarkGray
            $eIdx++
        }
    }

    $actual = Read-Host "Enter actual result (leave blank to skip)"
    $statusInput = Read-Host "Mark step as PASS, FAIL, or SKIP"
    $status = $statusInput.ToUpperInvariant()
    if ($status -notin @("PASS","FAIL","SKIP")) {
        Write-Warning "Status '$statusInput' not recognized. Defaulting to SKIP."
        $status = "SKIP"
    }

    $run.steps += [ordered]@{
        number        = $step.number
        title         = $step.title
        status        = $status
        actualResult  = $actual
        timestamp     = (Get-Date).ToString("s")
    }

    if ($status -eq "FAIL") {
        Write-Host "Capture follow-up notes for failure (optional)" -ForegroundColor Cyan
        $notes = Read-Host
        if ($notes) {
            $run.steps[-1].notes = $notes
        }
    }
}

$run.completedAt = (Get-Date).ToString("s")
$run.overallStatus = if ($run.steps.status -contains "FAIL") { "FAIL" } elseif ($run.steps.status -contains "SKIP") { "PARTIAL" } else { "PASS" }

if (-not (Test-Path -Path (Join-Path $PSScriptRoot $OutputDirectory))) {
    New-Item -ItemType Directory -Path (Join-Path $PSScriptRoot $OutputDirectory) | Out-Null
}

$timestamp = Get-Date -Format "yyyyMMdd-HHmmss"
$outputPath = Join-Path $PSScriptRoot (Join-Path $OutputDirectory ("{0}-{1}.json" -f $timestamp, $test.id))
$run | ConvertTo-Json -Depth 5 | Set-Content -Path $outputPath -Encoding UTF8

Write-Host "`nTest run saved to $outputPath" -ForegroundColor Green
Write-Host "Overall Status: $($run.overallStatus)" -ForegroundColor Yellow
