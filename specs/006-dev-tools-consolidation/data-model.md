# Data Model: Developer Tools Consolidation & Core Services Refactoring

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Draft

---

## Entity Overview

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           Developer Tools Domain                             │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                              │
│  ┌──────────────────┐   ┌──────────────────┐   ┌──────────────────┐        │
│  │   Model_LogEntry  │   │Model_LogStatistics│   │  Model_LogFilter  │        │
│  │   (CSV + DB)      │   │   (Aggregated)    │   │   (Filter DTO)    │        │
│  └──────────────────┘   └──────────────────┘   └──────────────────┘        │
│                                                                              │
│  ┌──────────────────┐   ┌──────────────────┐   ┌──────────────────┐        │
│  │Model_ErrorGrouping│   │Model_SystemHealth │   │Model_DatabaseHealth│       │
│  │   (Grouped Data)  │   │   Status          │   │   (DB Status)      │       │
│  └──────────────────┘   └──────────────────┘   └──────────────────┘        │
│                                                                              │
│  ┌──────────────────┐   ┌──────────────────┐                                │
│  │Model_Performance │   │Model_FeedbackSummary│                               │
│  │   Metrics        │   │   (Stats DTO)      │                               │
│  └──────────────────┘   └──────────────────┘                                │
│                                                                              │
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## Entity Definitions

### 1. Model_LogEntry

Represents a single log record from either CSV files or database.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Represents a single log entry from CSV files or database.
/// </summary>
public class Model_LogEntry
{
    #region Properties

    /// <summary>
    /// Unique identifier (database ID or file line number).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Timestamp when the log entry was created.
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Log level: Information, Warning, Error, Critical.
    /// </summary>
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// Source component (module, class, or method name).
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Log message content.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Additional details (stack trace, context data).
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// User who triggered the log entry (if applicable).
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Error type for exceptions (e.g., MySqlException, NullReferenceException).
    /// </summary>
    public string? ErrorType { get; set; }

    /// <summary>
    /// Stack trace for error entries.
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// Machine name where the log originated.
    /// </summary>
    public string? MachineName { get; set; }

    /// <summary>
    /// Application version at time of logging.
    /// </summary>
    public string? AppVersion { get; set; }

    /// <summary>
    /// Source of the log entry (File or Database).
    /// </summary>
    public Enum_LogSource LogSource { get; set; } = Enum_LogSource.File;

    /// <summary>
    /// Original file path (for CSV-sourced entries).
    /// </summary>
    public string? SourceFilePath { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Returns true if this is an error or critical entry.
    /// </summary>
    public bool IsError => Level.Equals("Error", StringComparison.OrdinalIgnoreCase) ||
                           Level.Equals("Critical", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Returns the severity emoji for display.
    /// </summary>
    public string SeverityEmoji => Level.ToUpperInvariant() switch
    {
        "CRITICAL" => "🔴",
        "ERROR" => "🔴",
        "WARNING" => "⚠️",
        "INFORMATION" => "ℹ️",
        "INFO" => "ℹ️",
        _ => "📝"
    };

    #endregion
}

/// <summary>
/// Source of a log entry.
/// </summary>
public enum Enum_LogSource
{
    /// <summary>Log entry read from CSV file.</summary>
    File,
    /// <summary>Log entry read from database.</summary>
    Database
}
```

---

### 2. Model_LogStatistics

Aggregated statistics for dashboard display.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Aggregated log statistics for dashboard display.
/// </summary>
public class Model_LogStatistics
{
    #region Properties

    /// <summary>
    /// Start of the statistics period.
    /// </summary>
    public DateTime PeriodStart { get; set; }

    /// <summary>
    /// End of the statistics period.
    /// </summary>
    public DateTime PeriodEnd { get; set; }

    /// <summary>
    /// Total number of log entries in the period.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Number of Critical entries.
    /// </summary>
    public int CriticalCount { get; set; }

    /// <summary>
    /// Number of Error entries.
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// Number of Warning entries.
    /// </summary>
    public int WarningCount { get; set; }

    /// <summary>
    /// Number of Information entries.
    /// </summary>
    public int InfoCount { get; set; }

    /// <summary>
    /// Timestamp of the most recent error.
    /// </summary>
    public DateTime? LastErrorTime { get; set; }

    /// <summary>
    /// Message of the most recent error.
    /// </summary>
    public string? LastErrorMessage { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Combined error and critical count.
    /// </summary>
    public int ErrorsAndCritical => ErrorCount + CriticalCount;

    /// <summary>
    /// Percentage of entries that are errors/critical.
    /// </summary>
    public double ErrorPercentage => TotalCount > 0 
        ? Math.Round((double)ErrorsAndCritical / TotalCount * 100, 1) 
        : 0;

    #endregion
}
```

---

### 3. Model_LogFilter

Filter criteria for log queries.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Filter criteria for querying logs.
/// </summary>
public class Model_LogFilter
{
    #region Properties

    /// <summary>
    /// Start date for filtering (inclusive).
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// End date for filtering (inclusive).
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Filter by specific severity levels.
    /// </summary>
    public List<string> Severities { get; set; } = [];

    /// <summary>
    /// Filter by source/module name (supports partial match).
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Search text (applied to Message and Details).
    /// </summary>
    public string? SearchText { get; set; }

    /// <summary>
    /// Whether SearchText is a regular expression.
    /// </summary>
    public bool IsRegex { get; set; }

    /// <summary>
    /// Filter by specific user.
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Filter by error type (exception class name).
    /// </summary>
    public string? ErrorType { get; set; }

    /// <summary>
    /// Maximum number of entries to return.
    /// </summary>
    public int? MaxResults { get; set; }

    /// <summary>
    /// Offset for pagination (0-based).
    /// </summary>
    public int Skip { get; set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a filter for today's entries.
    /// </summary>
    public static Model_LogFilter Today() => new()
    {
        DateFrom = DateTime.Today,
        DateTo = DateTime.Today.AddDays(1).AddTicks(-1)
    };

    /// <summary>
    /// Creates a filter for the last 7 days.
    /// </summary>
    public static Model_LogFilter Last7Days() => new()
    {
        DateFrom = DateTime.Today.AddDays(-7),
        DateTo = DateTime.Now
    };

    /// <summary>
    /// Creates a filter for the last 30 days.
    /// </summary>
    public static Model_LogFilter Last30Days() => new()
    {
        DateFrom = DateTime.Today.AddDays(-30),
        DateTo = DateTime.Now
    };

    /// <summary>
    /// Creates a filter for errors only.
    /// </summary>
    public static Model_LogFilter ErrorsOnly() => new()
    {
        Severities = ["Error", "Critical"]
    };

    #endregion
}
```

---

### 4. Model_ErrorGrouping

Grouped error data for analysis.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Represents a group of similar errors.
/// </summary>
public class Model_ErrorGrouping
{
    #region Properties

    /// <summary>
    /// Unique key for the group (e.g., ErrorType_MethodName).
    /// </summary>
    public string GroupKey { get; set; } = string.Empty;

    /// <summary>
    /// Error type for the group.
    /// </summary>
    public string ErrorType { get; set; } = string.Empty;

    /// <summary>
    /// Method/source where the error occurs.
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Representative error message (from first occurrence).
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Number of occurrences in this group.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// First occurrence timestamp.
    /// </summary>
    public DateTime FirstOccurrence { get; set; }

    /// <summary>
    /// Last occurrence timestamp.
    /// </summary>
    public DateTime LastOccurrence { get; set; }

    /// <summary>
    /// List of unique users who encountered this error.
    /// </summary>
    public List<string> AffectedUsers { get; set; } = [];

    #endregion

    #region Computed Properties

    /// <summary>
    /// Duration between first and last occurrence.
    /// </summary>
    public TimeSpan Duration => LastOccurrence - FirstOccurrence;

    /// <summary>
    /// Average occurrences per day.
    /// </summary>
    public double OccurrencesPerDay => Duration.TotalDays > 0 
        ? Math.Round(Count / Duration.TotalDays, 2) 
        : Count;

    #endregion
}
```

---

### 5. Model_SystemHealthStatus

Health indicator for user-facing display.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// System health status for user display.
/// </summary>
public class Model_SystemHealthStatus
{
    #region Properties

    /// <summary>
    /// Overall health indicator.
    /// </summary>
    public Enum_HealthIndicator Status { get; set; } = Enum_HealthIndicator.Green;

    /// <summary>
    /// User-friendly status message.
    /// </summary>
    public string Message { get; set; } = "System Operating Normally";

    /// <summary>
    /// Number of errors in the evaluation period.
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// Evaluation period (e.g., "Last 24 hours").
    /// </summary>
    public string EvaluationPeriod { get; set; } = "Last 24 hours";

    /// <summary>
    /// Timestamp of last error (if any).
    /// </summary>
    public DateTime? LastErrorTime { get; set; }

    /// <summary>
    /// Timestamp when health was last evaluated.
    /// </summary>
    public DateTime LastChecked { get; set; } = DateTime.Now;

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a health status based on error count.
    /// </summary>
    /// <param name="errorCount">Number of errors in last 24 hours.</param>
    /// <param name="lastErrorTime">Time of last error.</param>
    public static Model_SystemHealthStatus FromErrorCount(int errorCount, DateTime? lastErrorTime = null)
    {
        return errorCount switch
        {
            0 => new Model_SystemHealthStatus
            {
                Status = Enum_HealthIndicator.Green,
                Message = "System Operating Normally",
                ErrorCount = 0
            },
            >= 1 and <= 5 => new Model_SystemHealthStatus
            {
                Status = Enum_HealthIndicator.Yellow,
                Message = $"Minor Issues Detected ({errorCount} errors in last 24 hours)",
                ErrorCount = errorCount,
                LastErrorTime = lastErrorTime
            },
            _ => new Model_SystemHealthStatus
            {
                Status = Enum_HealthIndicator.Red,
                Message = $"System Experiencing Issues ({errorCount} errors in last 24 hours)",
                ErrorCount = errorCount,
                LastErrorTime = lastErrorTime
            }
        };
    }

    #endregion
}

/// <summary>
/// Health indicator colors.
/// </summary>
public enum Enum_HealthIndicator
{
    /// <summary>No errors - system healthy.</summary>
    Green,
    /// <summary>Minor issues - 1-5 errors.</summary>
    Yellow,
    /// <summary>Significant issues - 6+ errors.</summary>
    Red
}
```

---

### 6. Model_DatabaseHealth

Database connection and status information.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Database health and status information.
/// </summary>
public class Model_DatabaseHealth
{
    #region Properties

    /// <summary>
    /// Whether the database is currently connected.
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Connection status message.
    /// </summary>
    public string StatusMessage { get; set; } = "Unknown";

    /// <summary>
    /// MySQL server version.
    /// </summary>
    public string? ServerVersion { get; set; }

    /// <summary>
    /// Database name.
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// Server hostname/IP.
    /// </summary>
    public string? ServerHost { get; set; }

    /// <summary>
    /// Server uptime (if available).
    /// </summary>
    public TimeSpan? Uptime { get; set; }

    /// <summary>
    /// Last successful query timestamp.
    /// </summary>
    public DateTime? LastSuccessfulQuery { get; set; }

    /// <summary>
    /// Current connection latency in milliseconds.
    /// </summary>
    public int? LatencyMs { get; set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a status for an offline database.
    /// </summary>
    public static Model_DatabaseHealth Offline(string message = "Database connection unavailable") => new()
    {
        IsConnected = false,
        StatusMessage = message
    };

    /// <summary>
    /// Creates a status for an online database.
    /// </summary>
    public static Model_DatabaseHealth Online(string serverVersion, string databaseName, string host) => new()
    {
        IsConnected = true,
        StatusMessage = "Connected",
        ServerVersion = serverVersion,
        DatabaseName = databaseName,
        ServerHost = host,
        LastSuccessfulQuery = DateTime.Now
    };

    #endregion
}
```

---

### 7. Model_PerformanceMetrics

Runtime performance metrics.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Runtime performance metrics.
/// </summary>
public class Model_PerformanceMetrics
{
    #region Properties

    /// <summary>
    /// Current memory usage in MB.
    /// </summary>
    public double MemoryUsageMB { get; set; }

    /// <summary>
    /// Peak memory usage in MB.
    /// </summary>
    public double PeakMemoryMB { get; set; }

    /// <summary>
    /// Number of active threads.
    /// </summary>
    public int ThreadCount { get; set; }

    /// <summary>
    /// Application start time.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Average response time for recent operations (ms).
    /// </summary>
    public double? AverageResponseTimeMs { get; set; }

    /// <summary>
    /// CPU usage percentage (if available).
    /// </summary>
    public double? CpuUsagePercent { get; set; }

    /// <summary>
    /// GC collection count (Gen 0).
    /// </summary>
    public int GCGen0Collections { get; set; }

    /// <summary>
    /// GC collection count (Gen 1).
    /// </summary>
    public int GCGen1Collections { get; set; }

    /// <summary>
    /// GC collection count (Gen 2).
    /// </summary>
    public int GCGen2Collections { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Application uptime.
    /// </summary>
    public TimeSpan Uptime => DateTime.Now - StartTime;

    /// <summary>
    /// Formatted uptime string.
    /// </summary>
    public string UptimeFormatted
    {
        get
        {
            var ts = Uptime;
            if (ts.TotalDays >= 1)
                return $"{ts.Days}d {ts.Hours}h {ts.Minutes}m";
            if (ts.TotalHours >= 1)
                return $"{ts.Hours}h {ts.Minutes}m {ts.Seconds}s";
            return $"{ts.Minutes}m {ts.Seconds}s";
        }
    }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Captures current performance metrics.
    /// </summary>
    public static Model_PerformanceMetrics Capture()
    {
        var process = System.Diagnostics.Process.GetCurrentProcess();
        return new Model_PerformanceMetrics
        {
            MemoryUsageMB = Math.Round(process.WorkingSet64 / (1024.0 * 1024.0), 1),
            PeakMemoryMB = Math.Round(process.PeakWorkingSet64 / (1024.0 * 1024.0), 1),
            ThreadCount = process.Threads.Count,
            StartTime = process.StartTime,
            GCGen0Collections = GC.CollectionCount(0),
            GCGen1Collections = GC.CollectionCount(1),
            GCGen2Collections = GC.CollectionCount(2)
        };
    }

    #endregion
}
```

---

### 8. Model_FeedbackSummary

Aggregated feedback statistics.

```csharp
namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Aggregated feedback statistics for dashboard display.
/// </summary>
public class Model_FeedbackSummary
{
    #region Properties

    /// <summary>
    /// Total feedback count.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Count with Status = 'New'.
    /// </summary>
    public int NewCount { get; set; }

    /// <summary>
    /// Count with Status = 'In Progress'.
    /// </summary>
    public int InProgressCount { get; set; }

    /// <summary>
    /// Count with Status = 'Resolved'.
    /// </summary>
    public int ResolvedCount { get; set; }

    /// <summary>
    /// Count of bugs.
    /// </summary>
    public int BugCount { get; set; }

    /// <summary>
    /// Count of feature requests.
    /// </summary>
    public int FeatureCount { get; set; }

    /// <summary>
    /// Count of questions.
    /// </summary>
    public int QuestionCount { get; set; }

    /// <summary>
    /// Most recent feedback submission time.
    /// </summary>
    public DateTime? LastSubmissionTime { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Count of open items (New + In Progress).
    /// </summary>
    public int OpenCount => NewCount + InProgressCount;

    /// <summary>
    /// Resolution rate as percentage.
    /// </summary>
    public double ResolutionRate => TotalCount > 0 
        ? Math.Round((double)ResolvedCount / TotalCount * 100, 1) 
        : 0;

    #endregion
}
```

---

## Enum Definitions

### Existing Enums to Reuse

| Enum | Location | Usage |
|------|----------|-------|
| `Enum_ErrorSeverity` | `Models/Enum_ErrorSeverity.cs` | Error severity for exception handling |
| `Enum_DatabaseEnum_ErrorSeverity` | `Models/Enum_DatabaseEnum_ErrorSeverity.cs` | Database error severity |
| `Enum_LogLevel` | `Models/Enum_LogLevel.cs` | Log levels for LoggingUtility |

### New Enums

| Enum | File | Values |
|------|------|--------|
| `Enum_LogSource` | `Model_LogEntry.cs` | File, Database |
| `Enum_HealthIndicator` | `Model_SystemHealthStatus.cs` | Green, Yellow, Red |
| `Enum_LogGroupBy` | `Model_LogFilter.cs` | None, ErrorType, Source, Hour, Day |

---

## Data Flow Diagrams

### Dashboard Statistics Flow

```
┌──────────────────┐     ┌────────────────────┐     ┌──────────────────┐
│ Form_DeveloperTools│────▶│ IService_DeveloperTools│────▶│  Dao_DeveloperTools │
│ (Dashboard Tab)   │     │ GetLogStatisticsAsync │     │  GetStatisticsAsync │
└──────────────────┘     └────────────────────┘     └──────────────────┘
                                                              │
                                   ┌──────────────────────────┼──────────────────────────┐
                                   ▼                          ▼                          ▼
                         ┌──────────────────┐     ┌────────────────────┐     ┌──────────────────┐
                         │  log_error table  │     │ UserFeedback table │     │   CSV Log Files   │
                         └──────────────────┘     └────────────────────┘     └──────────────────┘
```

### Log Search Flow

```
┌──────────────────┐     ┌────────────────────┐     ┌──────────────────┐
│ Form_DeveloperTools│────▶│ IService_DeveloperTools│────▶│  CSV Parser         │
│ (Logs Tab)        │     │ GetLogEntriesAsync     │     │  (File-based)       │
│ + Model_LogFilter │     └────────────────────┘     └──────────────────┘
└──────────────────┘                │                          │
                                    ▼                          ▼
                         ┌──────────────────┐     ┌──────────────────┐
                         │  List<Model_LogEntry>  │────▶│  DataGridView       │
                         └──────────────────┘     └──────────────────┘
```

---

## Database Entities (Existing)

### log_error Table

Already documented in research.md. Key columns for analytics:
- `ID`, `User`, `Severity`, `ErrorType`, `ErrorMessage`, `StackTrace`, `MethodName`, `ErrorTime`

### UserFeedback Table

Already documented in research.md. 25+ columns with comprehensive schema.

---

## File Organization

```
Models/
└── DeveloperTools/
    ├── Model_LogEntry.cs           # Single log record
    ├── Model_LogStatistics.cs      # Aggregated stats
    ├── Model_LogFilter.cs          # Filter criteria + Enum_LogGroupBy
    ├── Model_ErrorGrouping.cs      # Grouped errors
    ├── Model_SystemHealthStatus.cs # Health indicator + Enum_HealthIndicator
    ├── Model_DatabaseHealth.cs     # DB status
    ├── Model_PerformanceMetrics.cs # Runtime metrics
    └── Model_FeedbackSummary.cs    # Feedback stats
```

---

## Validation Rules

| Entity | Property | Validation |
|--------|----------|------------|
| `Model_LogFilter` | `DateFrom`/`DateTo` | DateFrom must be <= DateTo |
| `Model_LogFilter` | `MaxResults` | Must be > 0 if specified |
| `Model_LogFilter` | `Skip` | Must be >= 0 |
| `Model_LogFilter` | `SearchText` (IsRegex=true) | Must be valid regex pattern |

---

## Notes

1. **No database changes required** - All entities are DTOs for in-memory representation
2. **CSV parsing performance** - Use streaming for large files (10K+ lines)
3. **Memory management** - `Model_LogEntry` lists should be paged for large datasets
4. **Thread safety** - `Model_PerformanceMetrics.Capture()` is thread-safe
