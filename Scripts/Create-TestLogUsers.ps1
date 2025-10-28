<#
.SYNOPSIS
    Creates test user log directories with sample log files for testing the View Application Logs feature.

.DESCRIPTION
    Generates multiple test users (TESTUSER1, TESTUSER2, TESTUSER3) with various log files
    in the structured format: [TIMESTAMP]|LEVEL|SOURCE|MESSAGE
    
    Creates logs in both locations:
    - C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\{username}\
    - X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs\{username}\ (if accessible)

.EXAMPLE
    .\Create-TestLogUsers.ps1
#>

[CmdletBinding()]
param()

# Base log directory (OneDrive location for johnk)
$baseLogDir = "C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs"

# Network location (if accessible)
$networkLogDir = "X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs"

# Test users to create
$testUsers = @("TESTUSER1", "TESTUSER2", "TESTUSER3")

# Function to create a log file with structured entries
function New-StructuredLogFile {
    param(
        [string]$FilePath,
        [string]$LogType,  # normal, app_error, db_error
        [int]$EntryCount = 10
    )

    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $entries = @()

    switch ($LogType) {
        "normal" {
            for ($i = 1; $i -le $EntryCount; $i++) {
                $time = (Get-Date).AddMinutes(-$i).ToString("yyyy-MM-dd HH:mm:ss")
                $entries += "[$time]|INFO|Application|Test log message #$i from normal log"
                
                # Add some variety
                if ($i % 3 -eq 0) {
                    $entries += "[$time]|DEBUG|Database|Query executed successfully in $($i * 10)ms"
                }
                if ($i % 5 -eq 0) {
                    $entries += "[$time]|WARN|Application|Low disk space warning - 15% remaining"
                }
            }
        }
        "app_error" {
            for ($i = 1; $i -le $EntryCount; $i++) {
                $time = (Get-Date).AddMinutes(-$i * 2).ToString("yyyy-MM-dd HH:mm:ss")
                $exceptions = @(
                    "NullReferenceException: Object reference not set to an instance of an object",
                    "ArgumentException: Invalid parameter value provided",
                    "InvalidOperationException: Operation is not valid due to the current state",
                    "FileNotFoundException: Could not find file 'test.dat'"
                )
                $ex = $exceptions[$i % $exceptions.Count]
                
                $entries += "[$time]|ERROR|Application|$ex"
                $entries += "[$time]|DETAIL|StackTrace|   at MTM.Application.TestMethod() in TestFile.cs:line $($i * 10)"
                $entries += "[$time]|DETAIL|StackTrace|   at MTM.Application.Main() in Program.cs:line $($i * 5)"
            }
        }
        "db_error" {
            for ($i = 1; $i -le $EntryCount; $i++) {
                $time = (Get-Date).AddMinutes(-$i * 3).ToString("yyyy-MM-dd HH:mm:ss")
                $severities = @("WARNING", "ERROR", "CRITICAL")
                $severity = $severities[$i % $severities.Count]
                
                $dbErrors = @(
                    "MySqlException: Unable to connect to database server",
                    "TimeoutException: Command timeout expired",
                    "MySqlException: Deadlock found when trying to get lock",
                    "MySqlException: Table 'test_table' doesn't exist"
                )
                $err = $dbErrors[$i % $dbErrors.Count]
                
                $entries += "[$time]|$severity|Database|$err"
                
                if ($severity -ne "WARNING") {
                    $entries += "[$time]|DETAIL|StackTrace|   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader()"
                    $entries += "[$time]|DETAIL|StackTrace|   at MTM.Data.Dao_Inventory.GetInventoryAsync()"
                }
            }
        }
    }

    # Write all entries to file
    $entries | Out-File -FilePath $FilePath -Encoding UTF8
    Write-Host "  Created: $FilePath ($($entries.Count) entries)" -ForegroundColor Green
}

# Function to create test user directories and log files
function New-TestUserLogs {
    param(
        [string]$BaseDir,
        [string]$Username
    )

    $userDir = Join-Path $BaseDir $Username
    
    # Create user directory if it doesn't exist
    if (-not (Test-Path $userDir)) {
        New-Item -ItemType Directory -Path $userDir -Force | Out-Null
        Write-Host "Created directory: $userDir" -ForegroundColor Cyan
    }

    $now = Get-Date
    
    # Create normal log files (3 files with varying dates)
    for ($days = 0; $days -lt 3; $days++) {
        $date = $now.AddDays(-$days)
        $filename = "$Username {0:MM-dd-yyyy} @ {0:h-mm} {0:tt}_normal.log" -f $date
        $filepath = Join-Path $userDir $filename
        New-StructuredLogFile -FilePath $filepath -LogType "normal" -EntryCount (15 - $days * 3)
    }

    # Create app error log files (2 files)
    for ($days = 0; $days -lt 2; $days++) {
        $date = $now.AddDays(-$days)
        $filename = "$Username {0:MM-dd-yyyy} @ {0:h-mm} {0:tt}_app_error.log" -f $date
        $filepath = Join-Path $userDir $filename
        New-StructuredLogFile -FilePath $filepath -LogType "app_error" -EntryCount (8 - $days * 2)
    }

    # Create db error log files (2 files)
    for ($days = 0; $days -lt 2; $days++) {
        $date = $now.AddDays(-$days * 2)
        $filename = "$Username {0:MM-dd-yyyy} @ {0:h-mm} {0:tt}_db_error.log" -f $date
        $filepath = Join-Path $userDir $filename
        New-StructuredLogFile -FilePath $filepath -LogType "db_error" -EntryCount (6 - $days)
    }

    # Create an empty file (should be skipped by viewer)
    $emptyFile = Join-Path $userDir "$Username empty_file_normal.log"
    New-Item -ItemType File -Path $emptyFile -Force | Out-Null
    Write-Host "  Created empty file (should be hidden): $emptyFile" -ForegroundColor Yellow
}

# Main execution
Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "Creating Test Log Users" -ForegroundColor Magenta
Write-Host "========================================`n" -ForegroundColor Magenta

# Create test users in OneDrive location
Write-Host "Creating test users in: $baseLogDir" -ForegroundColor Cyan
foreach ($user in $testUsers) {
    Write-Host "`nProcessing user: $user" -ForegroundColor Yellow
    New-TestUserLogs -BaseDir $baseLogDir -Username $user
}

# Try to create in network location if accessible
if (Test-Path $networkLogDir) {
    Write-Host "`n`nCreating test users in: $networkLogDir" -ForegroundColor Cyan
    foreach ($user in $testUsers) {
        Write-Host "`nProcessing user: $user" -ForegroundColor Yellow
        New-TestUserLogs -BaseDir $networkLogDir -Username $user
    }
} else {
    Write-Host "`n`nNetwork location not accessible: $networkLogDir" -ForegroundColor Yellow
    Write-Host "Test users only created in OneDrive location." -ForegroundColor Yellow
}

Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "Test User Creation Complete!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Magenta
Write-Host "`nTest users created:" -ForegroundColor Cyan
foreach ($user in $testUsers) {
    Write-Host "  - $user" -ForegroundColor White
}
Write-Host "`nYou can now test the View Application Logs feature with these users." -ForegroundColor Green
Write-Host "Empty files have been created but should NOT appear in the log viewer.`n" -ForegroundColor Yellow
