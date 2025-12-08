# Data Model: Dependency Cycle Remediation

## Entities

### Model_StartupResult
Represents the outcome of a startup operation.

| Field | Type | Description |
|-------|------|-------------|
| `IsSuccess` | `bool` | Whether the operation completed successfully. |
| `Message` | `string` | User-friendly message describing the result or error. |
| `Exception` | `Exception?` | The underlying exception, if any. |
| `Context` | `Dictionary<string, object>?` | Additional context data for logging/debugging. |

### Helper_LogPath (Static)
Provides log file paths.

| Property/Method | Type | Description |
|-----------------|------|-------------|
| `GetLogFilePathAsync(string server, string user)` | `Task<string>` | Returns the full path to the log file. |
| `LogDirectory` | `string` | The base directory for logs. |
