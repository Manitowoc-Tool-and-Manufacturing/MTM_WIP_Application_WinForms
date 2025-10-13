# Update all prompt files to be compatible with Visual Studio
# Removes VS Code-specific frontmatter (mode and tools) wherever present

$promptsDir = Join-Path $PSScriptRoot "..\prompts"

if (-not (Test-Path $promptsDir)) {
    Write-Error "Prompts directory not found: $promptsDir"
    exit 1
}

$promptFiles = Get-ChildItem -Path $promptsDir -Filter '*.prompt.md' -File | Sort-Object Name

if (-not $promptFiles) {
    Write-Host "No prompt files discovered under $promptsDir" -ForegroundColor Yellow
    exit 0
}

$updated = 0
$unchanged = 0

foreach ($file in $promptFiles) {
    Write-Host "Processing: $($file.Name)" -ForegroundColor Cyan

    $content = Get-Content $file.FullName -Raw

    if ($content -notmatch '^---\s*\r?\n') {
        Write-Host "⊘ Skipped (no frontmatter): $($file.Name)" -ForegroundColor Yellow
        $unchanged++
        continue
    }

    if ($content -notmatch 'mode:' -and $content -notmatch 'tools:') {
        Write-Host "⊘ No mode/tools entries detected: $($file.Name)" -ForegroundColor Yellow
        $unchanged++
        continue
    }

    if ($content -match '^(?s)(---\s*\r?\n)(.*?)(\r?\n---\s*\r?\n)(.*)$') {
        $headerStart = $matches[1]
        $headerBody = $matches[2]
        $headerEnd = $matches[3]
        $restOfFile = $matches[4]

        $cleanedHeader = $headerBody -split '\r?\n' |
            Where-Object { $_ -and ($_ -notmatch '^\s*(mode|tools)\b') } |
            ForEach-Object { $_.TrimEnd() }

        $newHeader = if ($cleanedHeader.Count -gt 0) {
            ($headerStart + ($cleanedHeader -join "`r`n") + "`r`n" + $headerEnd)
        } else {
            "---`r`n---`r`n"
        }

        $newContent = $newHeader + $restOfFile

        if ($newContent -ne $content) {
            Set-Content -Path $file.FullName -Value $newContent -NoNewline
            Write-Host "✓ Updated: $($file.Name)" -ForegroundColor Green
            $updated++
        } else {
            Write-Host "⊘ No changes needed: $($file.Name)" -ForegroundColor Yellow
            $unchanged++
        }
    } else {
        Write-Host "⊘ Frontmatter block not found: $($file.Name)" -ForegroundColor Yellow
        $unchanged++
    }
}

Write-Host "`nSummary:" -ForegroundColor Cyan
Write-Host "  Updated files  : $updated" -ForegroundColor Green
Write-Host "  Unchanged files: $unchanged" -ForegroundColor Yellow
Write-Host "`n✓ All prompt files processed for Visual Studio compatibility" -ForegroundColor Green
