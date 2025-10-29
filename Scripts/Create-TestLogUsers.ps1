<#
.SYNOPSIS
    Creates test user log directories with sample CSV log files for testing the View Application Logs feature.

.DESCRIPTION
    Generates multiple test users (TESTUSER1, TESTUSER2, TESTUSER3) with various CSV log files using the enhanced logging format.
    
    Enhanced CSV format: Timestamp,Level,Source,Message,Details
    - Source: FileName.MethodName:LineNumber (e.g., ViewApplicationLogsForm.LoadLogFileAsync:448)
    - Level: INFO, SUCCESS, PERFORMANCE, WARNING, ERROR, DEBUG, CRITICAL
    - Message: Includes inner exceptions for errors
    - Details: Full stack traces with file paths and line numbers
    
    Deletes any existing .log files and creates new .csv files in both locations:
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

# Function to escape CSV value (enclose in quotes if contains comma, quote, or newline)
function ConvertTo-CsvValue {
    param([string]$Value)
    
    if ([string]::IsNullOrEmpty($Value)) {
        return ""
    }
    
    # If value contains comma, quote, or newline, enclose in quotes and escape internal quotes
    if ($Value.Contains(',') -or $Value.Contains('"') -or $Value.Contains("`n") -or $Value.Contains("`r")) {
        return "`"$($Value.Replace('"', '""'))`""
    }
    
    return $Value
}

# Function to create a CSV log file with proper format
function New-CsvLogFile {
    param(
        [string]$FilePath,
        [string]$LogType,  # normal, app_error, db_error
        [int]$EntryCount = 10
    )

    $entries = @()
    
    # Add CSV header
    $entries += "Timestamp,Level,Source,Message,Details"

    switch ($LogType) {
        "normal" {
            # Generate realistic log entries with new enhanced format
            $files = @("ViewApplicationLogsForm", "MainForm", "SettingsForm", "Control_QuickButtons")
            $methods = @("LoadLogFileAsync", "OnFormLoad", "SaveSettingsAsync", "RefreshButtons")
            
            for ($i = 1; $i -le $EntryCount; $i++) {
                $time = (Get-Date).AddMinutes(-$i).ToString("yyyy-MM-dd HH:mm:ss")
                $file = $files[$i % $files.Count]
                $method = $methods[$i % $methods.Count]
                $line = (Get-Random -Minimum 100 -Maximum 999)
                $source = "$file.$method`:$line"
                
                # Mix of different log levels with realistic messages
                $levelType = $i % 7
                switch ($levelType) {
                    0 {
                        $level = "INFO"
                        $message = "[$file] User session started successfully"
                    }
                    1 {
                        $level = "SUCCESS"
                        $message = "[$file] Loaded $($i * 10) entries from database in $(Get-Random -Minimum 50 -Maximum 300)ms"
                    }
                    2 {
                        $level = "PERFORMANCE"
                        $message = "[Performance] LoadUserListAsync took $($i * 15)ms for $(Get-Random -Minimum 10 -Maximum 50) users (target <500ms)"
                    }
                    3 {
                        $level = "WARNING"
                        $message = "[$file] Slow query detected - operation took $($i * 100)ms (threshold: 1000ms)"
                    }
                    4 {
                        $level = "DEBUG"
                        $message = "[$file] Cache hit for key: user_preferences_$($i)"
                    }
                    5 {
                        $level = "INFO"
                        $message = "[$file] Auto-refresh completed successfully"
                    }
                    6 {
                        $level = "SUCCESS"
                        $message = "[$file] Transaction processed - InventoryID: $(Get-Random -Minimum 1000 -Maximum 9999)"
                    }
                }
                
                $entries += "$(ConvertTo-CsvValue $time),$(ConvertTo-CsvValue $level),$(ConvertTo-CsvValue $source),$(ConvertTo-CsvValue $message),"
            }
        }
        "app_error" {
            # Generate realistic application error entries with enhanced format
            $files = @("ViewApplicationLogsForm", "Dao_Inventory", "Service_ErrorHandler", "Control_TransactionEntry")
            $methods = @("LoadLogFileAsync", "GetInventoryAsync", "HandleException", "SaveTransactionAsync")
            
            for ($i = 1; $i -le $EntryCount; $i++) {
                $time = (Get-Date).AddMinutes(-$i * 2).ToString("yyyy-MM-dd HH:mm:ss")
                $file = $files[$i % $files.Count]
                $method = $methods[$i % $methods.Count]
                $line = (Get-Random -Minimum 100 -Maximum 999)
                $source = "$file.$method`:$line"
                
                $exceptions = @(
                    "NullReferenceException: Object reference not set to an instance of an object | Inner[1]: ArgumentNullException: Value cannot be null. (Parameter 'inventoryItem')",
                    "ArgumentException: Invalid parameter value provided | Inner[1]: FormatException: Input string was not in a correct format.",
                    "InvalidOperationException: Operation is not valid due to the current state",
                    "FileNotFoundException: Could not find file 'C:\temp\export_$(Get-Random -Minimum 1000 -Maximum 9999).xlsx'",
                    "UnauthorizedAccessException: Access to the path is denied | Inner[1]: IOException: The process cannot access the file because it is being used by another process."
                )
                $ex = $exceptions[$i % $exceptions.Count]
                $stackTrace = "   at MTM_Inventory_Application.$file.$method() in $file.cs:line $line`n   at System.Windows.Forms.Control.InvokeMarshaledCallbacks()`n   at MTM_Inventory_Application.Program.Main() in Program.cs:line $(Get-Random -Minimum 20 -Maximum 50)"
                
                $entries += "$(ConvertTo-CsvValue $time),$(ConvertTo-CsvValue 'ERROR'),$(ConvertTo-CsvValue $source),$(ConvertTo-CsvValue $ex),$(ConvertTo-CsvValue $stackTrace)"
            }
        }
        "db_error" {
            # Generate realistic database error entries with enhanced format
            $files = @("Dao_Inventory", "Dao_Transactions", "Dao_QuickButtons", "Helper_Database_StoredProcedure")
            $methods = @("GetInventoryAsync", "SearchTransactionsAsync", "SaveQuickButtonAsync", "ExecuteDataTableWithStatusAsync")
            
            for ($i = 1; $i -le $EntryCount; $i++) {
                $time = (Get-Date).AddMinutes(-$i * 3).ToString("yyyy-MM-dd HH:mm:ss")
                $file = $files[$i % $files.Count]
                $method = $methods[$i % $methods.Count]
                $line = (Get-Random -Minimum 100 -Maximum 999)
                $source = "$file.$method`:$line"
                
                $severities = @("WARNING", "ERROR", "CRITICAL")
                $severity = $severities[$i % $severities.Count]
                
                $dbErrors = @(
                    "MySqlException: Unable to connect to any of the specified MySQL hosts | Inner[1]: SocketException: No connection could be made because the target machine actively refused it",
                    "TimeoutException: Connection timeout expired. The timeout period elapsed while attempting to consume the pre-login handshake acknowledgement",
                    "MySqlException: Deadlock found when trying to get lock; try restarting transaction",
                    "MySqlException: Table 'mtm_wip_application.inventory_temp' doesn't exist",
                    "MySqlException: Duplicate entry 'INV-$(Get-Random -Minimum 1000 -Maximum 9999)' for key 'PRIMARY'"
                )
                $err = $dbErrors[$i % $dbErrors.Count]
                
                $details = ""
                if ($severity -ne "WARNING") {
                    $details = "   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader() in MySqlCommand.cs:line 425`n   at MTM_Inventory_Application.Helpers.Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync() in Helper_Database_StoredProcedure.cs:line $(Get-Random -Minimum 200 -Maximum 400)`n   at MTM_Inventory_Application.Data.$file.$method() in $file.cs:line $line"
                }
                
                $entries += "$(ConvertTo-CsvValue $time),$(ConvertTo-CsvValue $severity),$(ConvertTo-CsvValue $source),$(ConvertTo-CsvValue $err),$(ConvertTo-CsvValue $details)"
            }
        }
    }

    # Write all entries to file
    $entries | Out-File -FilePath $FilePath -Encoding UTF8
    Write-Host "  Created: $FilePath ($($entries.Count - 1) entries)" -ForegroundColor Green
}

# Function to delete old .log files from a directory
function Remove-OldLogFiles {
    param([string]$UserDir)
    
    if (Test-Path $UserDir) {
        $oldLogs = Get-ChildItem -Path $UserDir -Filter "*.log" -File
        if ($oldLogs.Count -gt 0) {
            Write-Host "  Deleting $($oldLogs.Count) old .log files..." -ForegroundColor Yellow
            $oldLogs | Remove-Item -Force
        }
    }
}

# Function to create test user directories and CSV log files
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
    
    # Delete old .log files
    Remove-OldLogFiles -UserDir $userDir

    $now = Get-Date
    
    # Create normal CSV log files (3 files with varying dates)
    for ($days = 0; $days -lt 3; $days++) {
        $date = $now.AddDays(-$days)
        $filename = "$Username {0:MM-dd-yyyy} @ {0:h-mm} {0:tt}_normal.csv" -f $date
        $filepath = Join-Path $userDir $filename
        New-CsvLogFile -FilePath $filepath -LogType "normal" -EntryCount (15 - $days * 3)
    }

    # Create app error CSV log files (2 files)
    for ($days = 0; $days -lt 2; $days++) {
        $date = $now.AddDays(-$days)
        $filename = "$Username {0:MM-dd-yyyy} @ {0:h-mm} {0:tt}_app_error.csv" -f $date
        $filepath = Join-Path $userDir $filename
        New-CsvLogFile -FilePath $filepath -LogType "app_error" -EntryCount (8 - $days * 2)
    }

    # Create db error CSV log files (2 files)
    for ($days = 0; $days -lt 2; $days++) {
        $date = $now.AddDays(-$days * 2)
        $filename = "$Username {0:MM-dd-yyyy} @ {0:h-mm} {0:tt}_db_error.csv" -f $date
        $filepath = Join-Path $userDir $filename
        New-CsvLogFile -FilePath $filepath -LogType "db_error" -EntryCount (6 - $days)
    }

    # Create an empty file (should be skipped by viewer)
    $emptyFile = Join-Path $userDir "$Username empty_file_normal.csv"
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
