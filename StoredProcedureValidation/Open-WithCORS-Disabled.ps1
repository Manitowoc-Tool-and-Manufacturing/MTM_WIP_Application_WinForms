# Launch browser with CORS disabled for local file development
# WARNING: Only use for local development - DO NOT browse the internet with these flags!

$htmlFile = Join-Path $PSScriptRoot "procedure-review-tool.html"

Write-Host "`n╔════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║   Opening Browser with CORS Disabled                 ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════╝`n" -ForegroundColor Cyan

Write-Host "  ⚠️  WARNING: Do NOT browse the internet in this window!" -ForegroundColor Yellow
Write-Host "  This browser instance has security features disabled." -ForegroundColor Yellow
Write-Host "  Only use it for viewing the local HTML file.`n" -ForegroundColor Yellow

Write-Host "  Opening: $htmlFile`n" -ForegroundColor Gray

# Try Chrome first (most common)
$chromePaths = @(
    "${env:ProgramFiles}\Google\Chrome\Application\chrome.exe",
    "${env:ProgramFiles(x86)}\Google\Chrome\Application\chrome.exe",
    "${env:LocalAppData}\Google\Chrome\Application\chrome.exe"
)

$chromeExe = $chromePaths | Where-Object { Test-Path $_ } | Select-Object -First 1

if ($chromeExe) {
    Write-Host "  Using Chrome with CORS disabled..." -ForegroundColor Green
    $tempDataDir = Join-Path $env:TEMP "chrome_dev_$(Get-Random)"
    
    & $chromeExe --disable-web-security --user-data-dir="$tempDataDir" --disable-features=CrossOriginResourcePolicy "file:///$($htmlFile -replace '\\', '/')"
    
    Write-Host "`n  Chrome closed. Cleaning up temporary data..." -ForegroundColor Gray
    if (Test-Path $tempDataDir) {
        Remove-Item -Recurse -Force $tempDataDir -ErrorAction SilentlyContinue
    }
    Write-Host "  Done!`n" -ForegroundColor Green
}
# Try Edge as fallback
else {
    $edgePaths = @(
        "${env:ProgramFiles(x86)}\Microsoft\Edge\Application\msedge.exe",
        "${env:ProgramFiles}\Microsoft\Edge\Application\msedge.exe"
    )
    
    $edgeExe = $edgePaths | Where-Object { Test-Path $_ } | Select-Object -First 1
    
    if ($edgeExe) {
        Write-Host "  Using Edge with CORS disabled..." -ForegroundColor Green
        $tempDataDir = Join-Path $env:TEMP "edge_dev_$(Get-Random)"
        
        & $edgeExe --disable-web-security --user-data-dir="$tempDataDir" --disable-features=CrossOriginResourcePolicy "file:///$($htmlFile -replace '\\', '/')"
        
        Write-Host "`n  Edge closed. Cleaning up temporary data..." -ForegroundColor Gray
        if (Test-Path $tempDataDir) {
            Remove-Item -Recurse -Force $tempDataDir -ErrorAction SilentlyContinue
        }
        Write-Host "  Done!`n" -ForegroundColor Green
    }
    else {
        Write-Host "  ❌ Neither Chrome nor Edge found." -ForegroundColor Red
        Write-Host "  Please use Start-WebServer.ps1 instead.`n" -ForegroundColor Yellow
    }
}
