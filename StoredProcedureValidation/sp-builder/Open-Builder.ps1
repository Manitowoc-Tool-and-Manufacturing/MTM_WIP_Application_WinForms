# Open-Builder.ps1
# Opens the MySQL 5.7 Stored Procedure Builder in the default browser
# Ensures MAMP is running and opens the correct URL

param(
    [switch]$SkipMampCheck = $false,
    [switch]$OpenInChrome = $false
)

# Script configuration
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$IndexFile = Join-Path $ScriptDir "index.html"
$MampPorts = @(80, 8888, 8080) # Common MAMP ports to check
$MampPort = $null
$MampUrl = $null

# Colors for console output
$ErrorColor = "Red"
$SuccessColor = "Green"
$WarningColor = "Yellow"
$InfoColor = "Cyan"

# Function to detect which port MAMP is running on
function Get-MampPort {
    foreach ($port in $MampPorts) {
        try {
            $null = Invoke-WebRequest -Uri "http://localhost:$port" -Method Head -TimeoutSec 1 -ErrorAction SilentlyContinue
            return $port
        }
        catch {
            # Port not responding, try next
        }
    }
    return $null
}

# Function to check if MAMP is running
function Test-MampRunning {
    param([int]$Port)
    
    if ($null -eq $Port) {
        return $false
    }
    
    try {
        $null = Invoke-WebRequest -Uri "http://localhost:$Port" -Method Head -TimeoutSec 2 -ErrorAction SilentlyContinue
        return $true
    }
    catch {
        return $false
    }
}

# Function to check if MySQL is accessible
function Test-MySqlRunning {
    try {
        $connection = New-Object System.Data.Odbc.OdbcConnection
        $connection.ConnectionString = "DRIVER={MySQL ODBC 8.0 Driver};SERVER=localhost;PORT=3306;UID=root;PWD=root;"
        $connection.Open()
        $connection.Close()
        return $true
    }
    catch {
        # ODBC driver may not be installed, try TCP port check
        try {
            $tcpClient = New-Object System.Net.Sockets.TcpClient
            $tcpClient.Connect("localhost", 3306)
            $tcpClient.Close()
            return $true
        }
        catch {
            return $false
        }
    }
}

# Function to open URL in specific browser
function Open-InBrowser {
    param(
        [string]$Url,
        [switch]$UseChrome
    )
    
    # Always try to find Chrome first (for incognito mode)
    $chromePaths = @(
        "$env:ProgramFiles\Google\Chrome\Application\chrome.exe",
        "$env:ProgramFiles(x86)\Google\Chrome\Application\chrome.exe",
        "$env:LocalAppData\Google\Chrome\Application\chrome.exe"
    )
    
    $chromePath = $chromePaths | Where-Object { Test-Path $_ } | Select-Object -First 1
    
    if ($chromePath) {
        Write-Host "Opening in Chrome (incognito mode to bypass cache)..." -ForegroundColor $InfoColor
        # Always use incognito mode to avoid cache issues with JavaScript modules
        Start-Process $chromePath -ArgumentList "--incognito", $Url
        return $true
    }
    else {
        Write-Host "Chrome not found. Opening in default browser..." -ForegroundColor $WarningColor
        Write-Host "Note: Cache issues may occur. Press Ctrl+Shift+R to hard refresh." -ForegroundColor $WarningColor
        Start-Process $Url
        return $false
    }
}

# Main script execution
Write-Host "`n=== MySQL 5.7 Stored Procedure Builder ===" -ForegroundColor $InfoColor
Write-Host "Starting application...`n" -ForegroundColor $InfoColor

# Check if index.html exists
if (-not (Test-Path $IndexFile)) {
    Write-Host "ERROR: index.html not found at: $IndexFile" -ForegroundColor $ErrorColor
    Write-Host "Please run this script from the sp-builder directory." -ForegroundColor $ErrorColor
    exit 1
}

# Check MAMP unless skipped
if (-not $SkipMampCheck) {
    Write-Host "Checking MAMP status..." -ForegroundColor $InfoColor
    
    # Auto-detect MAMP port
    $MampPort = Get-MampPort
    
    if ($null -ne $MampPort) {
        $MampUrl = "http://localhost:$MampPort/StoredProcedureValidation/sp-builder/index.html"
        Write-Host "✓ MAMP web server detected on port $MampPort" -ForegroundColor $SuccessColor
    }
    
    $mampRunning = Test-MampRunning -Port $MampPort
    $mysqlRunning = Test-MySqlRunning
    
    if (-not $mampRunning) {
        Write-Host "WARNING: MAMP web server is not responding" -ForegroundColor $WarningColor
        Write-Host "Checked ports: $($MampPorts -join ', ')" -ForegroundColor $WarningColor
        Write-Host "The application requires MAMP to be running for database connectivity." -ForegroundColor $WarningColor
        Write-Host "`nPlease:" -ForegroundColor $WarningColor
        Write-Host "  1. Start MAMP" -ForegroundColor $WarningColor
        Write-Host "  2. Ensure Apache is running" -ForegroundColor $WarningColor
        Write-Host "  3. Run this script again`n" -ForegroundColor $WarningColor
        
        $response = Read-Host "Continue anyway? (y/n)"
        if ($response -notmatch '^[Yy]') {
            Write-Host "Exiting." -ForegroundColor $InfoColor
            exit 0
        }
    }
    
    if (-not $mysqlRunning) {
        Write-Host "WARNING: MySQL is not accessible on port 3306" -ForegroundColor $WarningColor
        Write-Host "Database metadata features will not work." -ForegroundColor $WarningColor
        Write-Host "`nPlease ensure MySQL is running in MAMP.`n" -ForegroundColor $WarningColor
    }
    else {
        Write-Host "✓ MySQL is accessible on port 3306" -ForegroundColor $SuccessColor
    }
}

# Determine URL to open
if ($mampRunning -and $null -ne $MampPort) {
    # Use MAMP URL (avoids CORS issues with ES6 modules)
    $urlToOpen = "http://localhost:$MampPort/sp-builder/index.html"
    Write-Host "`nOpening: $urlToOpen" -ForegroundColor $InfoColor
    Write-Host "Note: Using MAMP web server (symlink at C:\MAMP\htdocs\sp-builder)" -ForegroundColor $InfoColor
} else {
    # Fallback to file:// (will have CORS issues with modules)
    $fileUrl = "file:///$($IndexFile -replace '\\', '/')"
    $urlToOpen = $fileUrl
    Write-Host "`nOpening: $fileUrl" -ForegroundColor $InfoColor
    Write-Host "WARNING: file:// protocol has CORS restrictions. Start MAMP for full functionality." -ForegroundColor $WarningColor
}

# Open in browser
$opened = Open-InBrowser -Url $urlToOpen -UseChrome:$OpenInChrome

if ($opened) {
    Write-Host "`n✓ Application opened successfully!" -ForegroundColor $SuccessColor
    Write-Host "`nQuick Start Guide:" -ForegroundColor $InfoColor
    Write-Host "  - Click 'Start Wizard' to create a new procedure" -ForegroundColor "White"
    Write-Host "  - Click 'Browse Templates' to use a pre-built template" -ForegroundColor "White"
    Write-Host "  - Click 'Import SQL' to edit an existing procedure" -ForegroundColor "White"
    Write-Host "`nFor help, press F1 or visit the Help page.`n" -ForegroundColor $InfoColor
}
else {
    Write-Host "`nERROR: Failed to open application" -ForegroundColor $ErrorColor
    exit 1
}

exit 0
