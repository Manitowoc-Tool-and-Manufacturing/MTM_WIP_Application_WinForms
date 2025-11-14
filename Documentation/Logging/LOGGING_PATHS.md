# Logging Path System Documentation

## Overview

The MTM WIP Application uses a hierarchical directory structure for log files with automatic fallback mechanisms to ensure logging always works, even when the primary directory is unavailable.

## Directory Structure

### Primary Location

**For johnk (developer):**
```
C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\
├── {username}\
│   ├── {username} {timestamp}_normal.csv
│   ├── {username} {timestamp}_db_error.csv
│   └── {username} {timestamp}_app_error.csv
└── Prompt Fixes\
    └── {MethodName}.md
```

**For all other users:**
```
X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs\
├── {username}\
│   ├── {username} {timestamp}_normal.csv
│   ├── {username} {timestamp}_db_error.csv
│   └── {username} {timestamp}_app_error.csv
└── Prompt Fixes\
    └── {MethodName}.md
```

### Fallback Location (CommonApplicationData)

**When primary location is unavailable:**
```
%ProgramData%\MTM_WIP_Application_Winforms\Logs\
├── {username}\
│   ├── {username} {timestamp}_normal.csv
│   ├── {username} {timestamp}_db_error.csv
│   └── {username} {timestamp}_app_error.csv
└── fallback_{timestamp}.csv (emergency fallback only)
```

**Typical path on Windows:**
```
C:\ProgramData\MTM_WIP_Application_Winforms\Logs\
```

## Log File Types

### Normal Logs (`_normal.csv`)

**Format:**
```
[TIMESTAMP] - [SEVERITY] - MESSAGE
```

**Example:**
```
[2025-11-11 14:30:00] - [LOW] - Application started successfully
[2025-11-11 14:30:05] - [DATA] - Loaded 150 inventory items
```

**Severity Levels:** LOW, MEDIUM, HIGH, DATA

### Database Error Logs (`_db_error.csv`)

**Format:**
```
[TIMESTAMP] - [SEVERITY] - DATABASE ERROR - PROCEDURE_NAME - MESSAGE
Details: {JSON with connection info, parameters, error codes}
```

**Example:**
```
[2025-11-11 14:40:23] - [CRITICAL] - DATABASE ERROR - sp_inventory_GetList - Connection timeout
Details: {"Server":"localhost","Database":"mtm_wip","CommandTimeout":"30","ErrorCode":"1205"}
```

**Severity Levels:** WARNING, ERROR, CRITICAL

### Application Error Logs (`_app_error.csv`)

**Format:**
```
[TIMESTAMP] - [ERROR] - SOURCE - MESSAGE
Details: {JSON with exception details, stack trace}
```

**Example:**
```
[2025-11-11 14:35:12] - [ERROR] - MainForm.LoadInventoryAsync - Failed to load inventory data
Details: {"ExceptionType":"InvalidOperationException","StackTrace":"at MTM_WIP_Application..."}
```

## Fallback Mechanisms

### Scenario 1: Timeout Fallback

**Trigger:** Directory creation times out (>5 seconds for Helper_Database_Variables, >10 seconds for LoggingUtility)

**Behavior:**
```csharp
// Falls back to CommonApplicationData with username subdirectory
var fallbackDir = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
    "MTM_WIP_Application_Winforms",
    "Logs",
    userName);
```

**Result:** Logs created in `%ProgramData%\MTM_WIP_Application_Winforms\Logs\{username}\`

### Scenario 2: Exception Fallback

**Trigger:** Critical exception during logging initialization (e.g., invalid connection string, missing user)

**Behavior:**
```csharp
// Falls back to CommonApplicationData root (no username)
var fallbackDir = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
    "MTM_WIP_Application_Winforms",
    "Logs");

var fallbackFile = Path.Combine(fallbackDir, $"fallback_{timestamp}.csv");
```

**Result:** Single file at `%ProgramData%\MTM_WIP_Application_Winforms\Logs\fallback_{timestamp}.csv`

**Note:** This fallback doesn't use a username subdirectory because:
- Username may not be available when exception occurs
- Meant for system-level errors affecting any user
- Clear indication of critical system failure

### Scenario 3: Complete Failure

**Trigger:** Even fallback directory creation fails

**Behavior:** Logging is disabled to prevent application crash
```csharp
_logDirectory = "";
_normalLogFile = "";
_dbErrorLogFile = "";
_appErrorLogFile = "";
```

## File Naming Convention

### Standard Log Files

**Pattern:** `{username} {timestamp}_{type}.csv`

**Timestamp Format:** `MM-dd-yyyy @ h-mm tt`

**Examples:**
- `john.doe 11-14-2025 @ 2-30 PM_normal.csv`
- `jane.smith 11-14-2025 @ 3-45 PM_db_error.csv`
- `admin.user 11-14-2025 @ 9-15 AM_app_error.csv`

### Emergency Fallback Files

**Pattern:** `fallback_{timestamp}.csv`

**Example:**
- `fallback_11-14-2025 @ 2-30 PM.csv`

## ViewApplicationLogsForm Integration

### User Discovery

The form searches **all base log directories** for user subdirectories:

```csharp
string[] baseLogDirs = Helper_LogPath.GetAllBaseLogDirectories();
// Returns: ["X:\MH_RESOURCE\...", "%ProgramData%\MTM_WIP_Application_Winforms\Logs"]
```

**Search process:**
1. Check if primary directory exists → add to search list
2. Check if fallback directory exists → add to search list
3. If neither exist → return preferred location for creation
4. Enumerate subdirectories in all found locations
5. Merge user lists (case-insensitive)

### File Discovery

For a selected user, the form searches their directory in **both locations**:

```csharp
string? userDirectory = Helper_LogPath.GetUserLogDirectory(username);
// Checks: Primary first, then fallback, returns first found
```

### Security Validation

All paths are validated using `Helper_LogPath.IsPathSafe()`:
- Must be within primary OR fallback directory
- No directory traversal attempts
- Sanitized filenames (invalid characters removed)

## Code References

### LoggingUtility.cs

**Line 166-242:** `InitializeLoggingAsync()`
- **Line 183:** Get log path from `Helper_Database_Variables`
- **Line 189-193:** Timeout fallback (with username)
- **Line 219-222:** Exception fallback (without username)

### Helper_LogPath.cs

**Line 16-27:** Directory constants
- **Line 16-18:** Primary log directory
- **Line 24-27:** Fallback log directory (CommonApplicationData)

**Line 117-140:** `GetAllBaseLogDirectories()`
- Returns array of all searchable log directories

**Line 148-190:** `GetUserLogDirectory(username)`
- Checks primary → fallback → returns first found

### Helper_Database_Variables.cs

**Line 45-81:** `GetLogFilePathAsync(server, userName)`
- Creates log file path in primary directory
- Throws on timeout (caught by LoggingUtility)

## Best Practices

### For Developers

1. **Always use LoggingUtility** for logging operations
2. **Never hardcode paths** - use Helper_LogPath methods
3. **Validate paths** with `Helper_LogPath.IsPathSafe()` before file operations
4. **Handle exceptions** - logging system has fallbacks, but always wrap in try-catch

### For System Administrators

1. **Ensure permissions** on X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs
2. **Monitor fallback directory** - if logs appear in %ProgramData%, investigate primary directory issues
3. **Check emergency fallbacks** - `fallback_{timestamp}.csv` indicates critical system errors
4. **Regular cleanup** - implement log rotation/archival as needed

### For Troubleshooting

1. **Check primary location first:** `X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs\{username}\`
2. **Check fallback location:** `%ProgramData%\MTM_WIP_Application_Winforms\Logs\{username}\`
3. **Check emergency fallbacks:** `%ProgramData%\MTM_WIP_Application_Winforms\Logs\fallback_*.csv`
4. **Review timestamps:** Identify when issue started
5. **Check permissions:** Verify write access to both directories

## Testing Scenarios

### Test 1: Primary Directory Unavailable

**Setup:**
1. Rename X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs (or johnk's OneDrive path)
2. Launch application
3. Generate logs (errors, database operations, etc.)

**Expected:**
- Logs created in `%ProgramData%\MTM_WIP_Application_Winforms\Logs\{username}\`
- ViewApplicationLogsForm finds and displays logs
- No application crashes

### Test 2: Timeout During Directory Creation

**Setup:**
1. Place network share (X:\) in slow/unresponsive state
2. Launch application

**Expected:**
- Timeout after 10 seconds
- Fallback to CommonApplicationData
- Logs created successfully

### Test 3: Critical Initialization Error

**Setup:**
1. Corrupt Model_Application_Variables.ConnectionString
2. Launch application

**Expected:**
- Exception fallback triggered
- `fallback_{timestamp}.csv` created in `%ProgramData%\MTM_WIP_Application_Winforms\Logs\`
- Error details logged

### Test 4: Log Viewer Discovery

**Setup:**
1. Create logs in primary location for User1
2. Create logs in fallback location for User2
3. Open ViewApplicationLogsForm

**Expected:**
- Both User1 and User2 appear in dropdown
- Selecting User1 shows logs from primary location
- Selecting User2 shows logs from fallback location

## Change History

| Date | Version | Change | Author |
|------|---------|--------|--------|
| 2025-11-14 | 1.0 | Initial documentation | Copilot Agent |
| 2025-11-14 | 1.0 | Verified fallback paths use CommonApplicationData | Copilot Agent |

## See Also

- [Service_ErrorHandler Instructions](.github/instructions/service-error-handler.instructions.md)
- [LoggingUtility Instructions](.github/instructions/service-logging.instructions.md)
- [Helper_LogPath.cs](Helpers/Helper_LogPath.cs)
- [LoggingUtility.cs](Logging/LoggingUtility.cs)
