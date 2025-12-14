# Interface Contract: ILoggingService

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Draft

---

## Overview

`ILoggingService` is the DI-enabled interface extracted from `LoggingUtility`. It provides the same logging functionality but supports constructor injection while maintaining backward compatibility through a static `Instance` property.

## Interface Definition

```csharp
namespace MTM_WIP_Application_Winforms.Services.Logging;

/// <summary>
/// Interface for application logging services.
/// Provides structured logging to CSV files with support for different severity levels.
/// </summary>
public interface ILoggingService
{
    #region Core Logging Methods

    /// <summary>
    /// Logs a message with default Information level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Log(string message);

    /// <summary>
    /// Logs a message with specified level and optional context.
    /// </summary>
    /// <param name="level">The log level (Information, Warning, Error, Critical).</param>
    /// <param name="source">The source component (module, class, method).</param>
    /// <param name="message">The message to log.</param>
    /// <param name="user">Optional user identifier.</param>
    /// <param name="exception">Optional exception for error logs.</param>
    void Log(Enum_LogLevel level, string source, string message, string? user = null, Exception? exception = null);

    #endregion

    #region Specialized Logging Methods

    /// <summary>
    /// Logs an application error with full exception details.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    /// <param name="additionalMessage">Optional additional context message.</param>
    void LogApplicationError(Exception ex, string? additionalMessage = null);

    /// <summary>
    /// Logs a database error with severity classification.
    /// </summary>
    /// <param name="ex">The database exception.</param>
    /// <param name="severity">The database error severity level.</param>
    void LogDatabaseError(Exception ex, Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error);

    /// <summary>
    /// Logs an informational application event.
    /// </summary>
    /// <param name="message">The information message.</param>
    void LogApplicationInfo(string message);

    #endregion

    #region Initialization and Cleanup

    /// <summary>
    /// Initializes the logging system (creates directories, sets up file paths).
    /// Should be called once at application startup.
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Cleans up old log files based on retention policy.
    /// </summary>
    /// <param name="retentionDays">Number of days to retain logs (default: 30).</param>
    Task CleanUpOldLogsAsync(int retentionDays = 30);

    #endregion
}
```

## Implementation Notes

### Service Registration

```csharp
// In Service_OnStartup_DependencyInjection.ConfigureServices()
services.AddSingleton<ILoggingService, Service_Logging>();
```

### Backward Compatibility

The existing `LoggingUtility` class will be modified to:
1. Implement `ILoggingService`
2. Add a static `Instance` property that returns the DI-resolved singleton
3. Keep static method wrappers that delegate to `Instance`

```csharp
public class Service_Logging : ILoggingService
{
    // Static accessor for backward compatibility
    private static ILoggingService? _instance;
    
    /// <summary>
    /// Gets the singleton instance of the logging service.
    /// Set by DI container during startup.
    /// </summary>
    public static ILoggingService Instance 
    {
        get => _instance ?? throw new InvalidOperationException(
            "Logging service not initialized. Ensure DI container is configured.");
        internal set => _instance = value;
    }
    
    // ... implementation
}

// Static wrapper class for backward compatibility
public static class LoggingUtility
{
    public static void Log(string message) => Service_Logging.Instance.Log(message);
    
    public static void LogApplicationError(Exception ex, string? additionalMessage = null) 
        => Service_Logging.Instance.LogApplicationError(ex, additionalMessage);
    
    // ... other static wrappers
}
```

### Migration Strategy

1. **Phase 1**: Create `ILoggingService` and `Service_Logging` with backward-compatible static wrappers
2. **Phase 2**: Update new code to inject `ILoggingService`
3. **Phase 3**: Gradually migrate existing code from `LoggingUtility.Log()` to injected service
4. **Future**: Mark static wrappers as `[Obsolete]` and eventually remove

## Usage Examples

### New Code (Recommended)

```csharp
public class MyService
{
    private readonly ILoggingService _logger;
    
    public MyService(ILoggingService logger)
    {
        _logger = logger;
    }
    
    public async Task DoSomethingAsync()
    {
        _logger.Log("Starting operation");
        try
        {
            // ... operation
            _logger.LogApplicationInfo("Operation completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogApplicationError(ex, "Operation failed");
            throw;
        }
    }
}
```

### Legacy Code (Backward Compatible)

```csharp
// Existing code continues to work unchanged
LoggingUtility.Log("Starting operation");
LoggingUtility.LogApplicationError(ex);
```
