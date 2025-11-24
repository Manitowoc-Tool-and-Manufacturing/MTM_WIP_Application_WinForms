$spFolder = "c:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Database\Dumps\StoredProcedures"
$codeBase = "c:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms"
$outputFile = Join-Path -Path $spFolder -ChildPath "procedure_usage_report.txt"

Write-Host "Analyzing stored procedure usage (v2)..."

# Get all stored procedure names from filenames
$spFiles = Get-ChildItem -Path $spFolder -Filter "*.sql"
$spNames = $spFiles | Select-Object -ExpandProperty BaseName

# Get all C# files, excluding the Database folder and obj/bin folders
$csFiles = Get-ChildItem -Path $codeBase -Recurse -Filter "*.cs" |
    Where-Object {
        $_.FullName -notmatch "\\Database\\" -and
        $_.FullName -notmatch "\\obj\\" -and
        $_.FullName -notmatch "\\bin\\"
    }

Write-Host "Found $($csFiles.Count) C# files to scan."

$results = @()
$totalSPs = $spNames.Count
$current = 0

foreach ($spName in $spNames) {
    $current++
    if ($current % 10 -eq 0) {
        Write-Progress -Activity "Searching for usages" -Status "Processing $spName ($current of $totalSPs)" -PercentComplete (($current / $totalSPs) * 100)
    }

    # Use Regex to match whole words only, but NOT SimpleMatch
    # Escape the SP name to be safe, though usually not needed for these names
    $escapedName = [Regex]::Escape($spName)
    $pattern = "\b$escapedName\b"

    $totalCount = 0

    foreach ($file in $csFiles) {
        # Select-String with -AllMatches to find multiple occurrences per line
        $found = Select-String -Path $file.FullName -Pattern $pattern -AllMatches

        if ($found) {
            # Sum up all matches from all lines found in this file
            $fileCount = ($found | Select-Object -ExpandProperty Matches).Count
            $totalCount += $fileCount
        }
    }

    $results += [PSCustomObject]@{
        ProcedureName = $spName
        UsageCount = $totalCount
    }
}

# Sort by usage count descending
$sortedResults = $results | Sort-Object UsageCount -Descending

# Format output
$outputContent = $sortedResults | Format-Table -AutoSize | Out-String

# Write to file
Set-Content -Path $outputFile -Value $outputContent

Write-Host "Report generated at: $outputFile"
