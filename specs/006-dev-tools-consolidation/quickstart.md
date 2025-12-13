# Quickstart Guide: Developer Tools Consolidation

**Feature**: 006-dev-tools-consolidation  
**Date**: 2025-12-13  
**Status**: Draft

---

## Overview

This guide provides code patterns and implementation examples for the Developer Tools Consolidation feature. Follow these patterns to ensure consistency with existing codebase conventions.

---

## 1. Service Registration Pattern

### DI Container Setup

```csharp
// In Service_OnStartup_DependencyInjection.ConfigureServices()
public static IServiceProvider ConfigureServices()
{
    try
    {
        LoggingUtility.Log("[Startup] Initializing dependency injection container");

        ServiceCollection services = new ServiceCollection();

        // === EXISTING REGISTRATIONS ===
        services.AddThemeServices();
        services.AddSingleton<IDao_Shortcuts, Dao_Shortcuts>();
        services.AddSingleton<IShortcutService, Service_Shortcut>();
        // ... other existing registrations ...
        
        // === NEW: Core Services (Singletons) ===
        services.AddSingleton<ILoggingService, Service_Logging>();
        services.AddSingleton<IService_ErrorHandler, Service_ErrorHandler>();
        
        // === NEW: Developer Tools Services ===
        services.AddTransient<IDao_DeveloperTools, Dao_DeveloperTools>();
        services.AddTransient<IService_DeveloperTools, Service_DeveloperTools>();

        // Build the service provider
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        
        // === NEW: Initialize static accessors for backward compatibility ===
        Service_Logging.Instance = serviceProvider.GetRequiredService<ILoggingService>();
        Service_ErrorHandler.Instance = serviceProvider.GetRequiredService<IService_ErrorHandler>();

        LoggingUtility.Log("[Startup] Dependency injection container initialized successfully");
        return serviceProvider;
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        throw;
    }
}
```

---

## 2. ILoggingService Implementation Pattern

### Interface Implementation

```csharp
namespace MTM_WIP_Application_Winforms.Services.Logging;

/// <summary>
/// DI-enabled logging service implementation.
/// </summary>
internal class Service_Logging : ILoggingService
{
    #region Fields

    private static ILoggingService? _instance;
    private readonly object _logLock = new();
    private string _logDirectory = string.Empty;
    private string _normalLogFile = string.Empty;
    private string _dbErrorLogFile = string.Empty;
    private string _appErrorLogFile = string.Empty;

    #endregion

    #region Properties

    /// <summary>
    /// Static accessor for backward compatibility.
    /// Set by DI container during startup.
    /// </summary>
    public static ILoggingService Instance
    {
        get => _instance ?? throw new InvalidOperationException(
            "Logging service not initialized. Ensure DI container is configured.");
        internal set => _instance = value;
    }

    #endregion

    #region ILoggingService Implementation

    /// <inheritdoc/>
    public void Log(string message)
    {
        Log(Enum_LogLevel.Information, "Application", message);
    }

    /// <inheritdoc/>
    public void Log(Enum_LogLevel level, string source, string message, 
        string? user = null, Exception? exception = null)
    {
        try
        {
            var entry = FormatCsvEntry(DateTime.Now, level.ToString(), source, message, 
                exception?.ToString());
            FlushLogEntryToDisk(entry, _normalLogFile);
        }
        catch
        {
            // Silent failure - don't let logging break the application
        }
    }

    /// <inheritdoc/>
    public void LogApplicationError(Exception ex, string? additionalMessage = null)
    {
        var message = additionalMessage != null 
            ? $"{additionalMessage}: {ex.Message}" 
            : ex.Message;
        var entry = FormatCsvEntry(DateTime.Now, "Error", ex.TargetSite?.Name ?? "Unknown",
            message, ex.ToString());
        FlushLogEntryToDisk(entry, _appErrorLogFile);
    }

    /// <inheritdoc/>
    public void LogDatabaseError(Exception ex, 
        Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error)
    {
        var entry = FormatCsvEntry(DateTime.Now, severity.ToString(), "Database",
            ex.Message, ex.ToString());
        FlushLogEntryToDisk(entry, _dbErrorLogFile);
    }

    /// <inheritdoc/>
    public void LogApplicationInfo(string message)
    {
        Log(Enum_LogLevel.Information, "Application", message);
    }

    /// <inheritdoc/>
    public async Task InitializeAsync()
    {
        // Initialize log directories and file paths
        _logDirectory = Helper_LogPath.GetLogDirectory();
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var username = Model_Application_Variables.User ?? "Unknown";
        
        _normalLogFile = Path.Combine(_logDirectory, $"{username}_{timestamp}_normal.csv");
        _dbErrorLogFile = Path.Combine(_logDirectory, $"{username}_{timestamp}_db_error.csv");
        _appErrorLogFile = Path.Combine(_logDirectory, $"{username}_{timestamp}_app_error.csv");
        
        Directory.CreateDirectory(_logDirectory);
        await Task.CompletedTask;
    }

    /// <inheritdoc/>
    public async Task CleanUpOldLogsAsync(int retentionDays = 30)
    {
        await Task.Run(() =>
        {
            var cutoff = DateTime.Now.AddDays(-retentionDays);
            var files = Directory.GetFiles(_logDirectory, "*.csv");
            foreach (var file in files)
            {
                if (File.GetCreationTime(file) < cutoff)
                {
                    try { File.Delete(file); } catch { /* Ignore */ }
                }
            }
        });
    }

    #endregion

    #region Helpers

    private static string FormatCsvEntry(DateTime timestamp, string level, 
        string source, string message, string? details)
    {
        return $"{timestamp:yyyy-MM-dd HH:mm:ss}," +
               $"{EscapeCsvField(level)}," +
               $"{EscapeCsvField(source)}," +
               $"{EscapeCsvField(message)}," +
               $"{EscapeCsvField(details ?? string.Empty)}";
    }

    private static string EscapeCsvField(string field)
    {
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        return field;
    }

    private void FlushLogEntryToDisk(string entry, string filePath)
    {
        lock (_logLock)
        {
            File.AppendAllText(filePath, entry + Environment.NewLine);
        }
    }

    #endregion
}
```

### Backward Compatibility Wrapper

```csharp
namespace MTM_WIP_Application_Winforms.Services.Logging;

/// <summary>
/// Static wrapper class for backward compatibility.
/// Delegates all calls to the DI-resolved ILoggingService instance.
/// </summary>
public static class LoggingUtility
{
    /// <summary>
    /// Logs a message with default Information level.
    /// </summary>
    public static void Log(string message) 
        => Service_Logging.Instance.Log(message);

    /// <summary>
    /// Logs a message with specified level and context.
    /// </summary>
    public static void Log(Enum_LogLevel level, string source, string message, 
        string? user = null, Exception? exception = null)
        => Service_Logging.Instance.Log(level, source, message, user, exception);

    /// <summary>
    /// Logs an application error.
    /// </summary>
    public static void LogApplicationError(Exception ex, string? additionalMessage = null)
        => Service_Logging.Instance.LogApplicationError(ex, additionalMessage);

    /// <summary>
    /// Logs a database error.
    /// </summary>
    public static void LogDatabaseError(Exception ex, 
        Enum_DatabaseEnum_ErrorSeverity severity = Enum_DatabaseEnum_ErrorSeverity.Error)
        => Service_Logging.Instance.LogDatabaseError(ex, severity);

    /// <summary>
    /// Logs an informational message.
    /// </summary>
    public static void LogApplicationInfo(string message)
        => Service_Logging.Instance.LogApplicationInfo(message);

    /// <summary>
    /// Initializes the logging system.
    /// </summary>
    public static Task InitializeLoggingAsync()
        => Service_Logging.Instance.InitializeAsync();

    /// <summary>
    /// Cleans up old log files.
    /// </summary>
    public static Task CleanUpOldLogsIfNeededAsync()
        => Service_Logging.Instance.CleanUpOldLogsAsync();
}
```

---

## 3. Form Constructor Injection Pattern

### Developer Tools Form

```csharp
namespace MTM_WIP_Application_Winforms.Forms.DeveloperTools;

/// <summary>
/// Multi-tab developer diagnostic tools form.
/// </summary>
public partial class Form_DeveloperTools : ThemedForm
{
    #region Fields

    private readonly IService_DeveloperTools _devToolsService;
    private readonly IService_ErrorHandler _errorHandler;
    private readonly IService_FeedbackManager _feedbackManager;
    private readonly int _currentUserId;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new Developer Tools form.
    /// </summary>
    /// <param name="devToolsService">Developer tools analytics service.</param>
    /// <param name="errorHandler">Error handling service.</param>
    /// <param name="feedbackManager">Feedback management service.</param>
    public Form_DeveloperTools(
        IService_DeveloperTools devToolsService,
        IService_ErrorHandler errorHandler,
        IService_FeedbackManager feedbackManager)
    {
        _devToolsService = devToolsService ?? throw new ArgumentNullException(nameof(devToolsService));
        _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
        _feedbackManager = feedbackManager ?? throw new ArgumentNullException(nameof(feedbackManager));
        _currentUserId = Model_Application_Variables.CurrentUserID;
        
        InitializeComponent();
    }

    #endregion

    #region Methods

    private async void Form_DeveloperTools_Load(object sender, EventArgs e)
    {
        // Check role access
        if (!CheckRoleAccess())
        {
            _errorHandler.ShowUserError("Access denied. Developer or Admin role required.");
            Close();
            return;
        }
        
        await LoadDashboardDataAsync();
    }

    private async Task LoadDashboardDataAsync()
    {
        try
        {
            // Load statistics for last 24 hours
            var end = DateTime.Now;
            var start = end.AddHours(-24);
            
            var statsResult = await _devToolsService.GetLogStatisticsAsync(start, end);
            if (!statsResult.IsSuccess)
            {
                _errorHandler.ShowUserError(statsResult.ErrorMessage);
                return;
            }
            
            // Update dashboard cards
            UpdateDashboardCards(statsResult.Data);
            
            // Load recent errors
            var errorsResult = await _devToolsService.GetRecentErrorsAsync(10);
            if (errorsResult.IsSuccess)
            {
                dgvRecentErrors.DataSource = errorsResult.Data;
            }
            
            // Load feedback summary
            var feedbackResult = await _devToolsService.GetFeedbackSummaryAsync();
            if (feedbackResult.IsSuccess)
            {
                UpdateFeedbackSummary(feedbackResult.Data);
            }
        }
        catch (Exception ex)
        {
            _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                callerName: nameof(LoadDashboardDataAsync),
                controlName: Name);
        }
    }

    private bool CheckRoleAccess()
    {
        var role = Model_Application_Variables.CurrentUserRole;
        return role == "Admin" || role == "Developer";
    }

    #endregion
}
```

### Form Factory for DI Resolution

```csharp
// Opening Developer Tools from MainForm
private void tsmiDeveloperTools_Click(object sender, EventArgs e)
{
    var devToolsForm = new Form_DeveloperTools(
        Model_Application_Variables.ServiceProvider.GetRequiredService<IService_DeveloperTools>(),
        Model_Application_Variables.ServiceProvider.GetRequiredService<IService_ErrorHandler>(),
        Model_Application_Variables.ServiceProvider.GetRequiredService<IService_FeedbackManager>());
    
    devToolsForm.Show();
}
```

---

## 4. DAO Pattern for Developer Tools

### DAO Interface

```csharp
namespace MTM_WIP_Application_Winforms.Data;

/// <summary>
/// Data access interface for Developer Tools analytics.
/// </summary>
public interface IDao_DeveloperTools
{
    /// <summary>
    /// Gets log statistics from the log_error table.
    /// </summary>
    Task<Model_Dao_Result<DataTable>> GetLogStatisticsAsync(DateTime start, DateTime end);
    
    /// <summary>
    /// Gets error entries with optional filtering.
    /// </summary>
    Task<Model_Dao_Result<DataTable>> GetErrorsAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? severity = null,
        string? user = null,
        int? maxResults = null);
    
    /// <summary>
    /// Gets grouped error data.
    /// </summary>
    Task<Model_Dao_Result<DataTable>> GetErrorGroupingsAsync(string groupBy);
}
```

### DAO Implementation

```csharp
namespace MTM_WIP_Application_Winforms.Data;

/// <summary>
/// Data access implementation for Developer Tools analytics.
/// </summary>
public class Dao_DeveloperTools : IDao_DeveloperTools
{
    #region IDao_DeveloperTools Implementation

    /// <inheritdoc/>
    public async Task<Model_Dao_Result<DataTable>> GetLogStatisticsAsync(DateTime start, DateTime end)
    {
        var parameters = new Dictionary<string, object>
        {
            { "StartDate", start },
            { "EndDate", end }
        };
        
        return await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync("md_devtools_GetLogStatistics", parameters);
    }

    /// <inheritdoc/>
    public async Task<Model_Dao_Result<DataTable>> GetErrorsAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? severity = null,
        string? user = null,
        int? maxResults = null)
    {
        var parameters = new Dictionary<string, object>
        {
            { "DateFrom", dateFrom ?? (object)DBNull.Value },
            { "DateTo", dateTo ?? (object)DBNull.Value },
            { "Severity", severity ?? (object)DBNull.Value },
            { "UserName", user ?? (object)DBNull.Value },
            { "MaxResults", maxResults ?? (object)DBNull.Value }
        };
        
        return await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync("log_error_Get_ByDateRange", parameters);
    }

    /// <inheritdoc/>
    public async Task<Model_Dao_Result<DataTable>> GetErrorGroupingsAsync(string groupBy)
    {
        var parameters = new Dictionary<string, object>
        {
            { "GroupBy", groupBy }
        };
        
        return await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync("md_devtools_GetErrorGroupings", parameters);
    }

    #endregion
}
```

---

## 5. Log Entry Parsing Pattern

### CSV Parser Service Method

```csharp
/// <summary>
/// Parses CSV log files matching the filter criteria.
/// </summary>
private async Task<List<Model_LogEntry>> ParseCsvLogsAsync(Model_LogFilter filter)
{
    var entries = new List<Model_LogEntry>();
    var logDirectory = Helper_LogPath.GetLogDirectory();
    
    if (!Directory.Exists(logDirectory))
    {
        return entries;
    }
    
    var files = Directory.GetFiles(logDirectory, "*.csv", SearchOption.AllDirectories);
    
    // Pre-filter files by date in filename if possible
    if (filter.DateFrom.HasValue || filter.DateTo.HasValue)
    {
        files = FilterFilesByDate(files, filter.DateFrom, filter.DateTo);
    }
    
    foreach (var file in files)
    {
        await ParseSingleCsvFileAsync(file, filter, entries);
        
        // Check if we've hit max results
        if (filter.MaxResults.HasValue && entries.Count >= filter.MaxResults.Value)
        {
            break;
        }
    }
    
    // Apply final filtering and sorting
    return entries
        .Where(e => MatchesFilter(e, filter))
        .OrderByDescending(e => e.Timestamp)
        .Take(filter.MaxResults ?? int.MaxValue)
        .ToList();
}

private async Task ParseSingleCsvFileAsync(string filePath, Model_LogFilter filter, 
    List<Model_LogEntry> entries)
{
    try
    {
        using var reader = new StreamReader(filePath);
        var lineNumber = 0;
        
        // Skip header if present
        var firstLine = await reader.ReadLineAsync();
        if (firstLine?.StartsWith("Timestamp") == true)
        {
            lineNumber++;
        }
        else if (firstLine != null)
        {
            // First line is data, parse it
            var entry = ParseCsvLine(firstLine, filePath, lineNumber);
            if (entry != null)
            {
                entries.Add(entry);
            }
        }
        
        while (!reader.EndOfStream)
        {
            lineNumber++;
            var line = await reader.ReadLineAsync();
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            var entry = ParseCsvLine(line, filePath, lineNumber);
            if (entry != null && MatchesFilter(entry, filter))
            {
                entries.Add(entry);
            }
        }
    }
    catch (IOException)
    {
        // File may be in use, skip it
    }
}

private Model_LogEntry? ParseCsvLine(string line, string filePath, int lineNumber)
{
    try
    {
        var fields = ParseCsvFields(line);
        if (fields.Length < 4) return null;
        
        return new Model_LogEntry
        {
            Id = lineNumber,
            Timestamp = DateTime.TryParse(fields[0], out var ts) ? ts : DateTime.Now,
            Level = fields[1],
            Source = fields[2],
            Message = fields[3],
            Details = fields.Length > 4 ? fields[4] : null,
            LogSource = Enum_LogSource.File,
            SourceFilePath = filePath
        };
    }
    catch
    {
        return null;
    }
}

private string[] ParseCsvFields(string line)
{
    var fields = new List<string>();
    var current = new StringBuilder();
    var inQuotes = false;
    
    foreach (var c in line)
    {
        if (c == '"')
        {
            inQuotes = !inQuotes;
        }
        else if (c == ',' && !inQuotes)
        {
            fields.Add(current.ToString());
            current.Clear();
        }
        else
        {
            current.Append(c);
        }
    }
    fields.Add(current.ToString());
    
    return fields.ToArray();
}
```

---

## 6. Dashboard Card UI Pattern

### Summary Cards Layout

```csharp
// In Form_DeveloperTools.Designer.cs
private void InitializeDashboardTab()
{
    // Error count card
    var pnlErrors = CreateSummaryCard("Errors (24h)", "0", Color.FromArgb(231, 76, 60));
    pnlErrors.Location = new Point(20, 20);
    tabDashboard.Controls.Add(pnlErrors);
    
    // Warning count card
    var pnlWarnings = CreateSummaryCard("Warnings (24h)", "0", Color.FromArgb(241, 196, 15));
    pnlWarnings.Location = new Point(200, 20);
    tabDashboard.Controls.Add(pnlWarnings);
    
    // Feedback count card
    var pnlFeedback = CreateSummaryCard("Open Feedback", "0", Color.FromArgb(52, 152, 219));
    pnlFeedback.Location = new Point(380, 20);
    tabDashboard.Controls.Add(pnlFeedback);
}

private Panel CreateSummaryCard(string title, string value, Color accentColor)
{
    var panel = new Panel
    {
        Size = new Size(160, 100),
        BorderStyle = BorderStyle.FixedSingle
    };
    
    var lblTitle = new Label
    {
        Text = title,
        Location = new Point(10, 10),
        AutoSize = true,
        Font = new Font("Segoe UI", 9F)
    };
    
    var lblValue = new Label
    {
        Text = value,
        Location = new Point(10, 40),
        AutoSize = true,
        Font = new Font("Segoe UI", 24F, FontStyle.Bold),
        ForeColor = accentColor,
        Name = $"lbl{title.Replace(" ", "").Replace("(", "").Replace(")", "")}Value"
    };
    
    panel.Controls.AddRange([lblTitle, lblValue]);
    return panel;
}
```

### Updating Dashboard Values

```csharp
private void UpdateDashboardCards(Model_LogStatistics stats)
{
    if (InvokeRequired)
    {
        Invoke(new Action(() => UpdateDashboardCards(stats)));
        return;
    }
    
    var lblErrors = tabDashboard.Controls.Find("lblErrors24hValue", true).FirstOrDefault() as Label;
    var lblWarnings = tabDashboard.Controls.Find("lblWarnings24hValue", true).FirstOrDefault() as Label;
    
    if (lblErrors != null) lblErrors.Text = stats.ErrorsAndCritical.ToString();
    if (lblWarnings != null) lblWarnings.Text = stats.WarningCount.ToString();
}
```

---

## 7. Error Handling Pattern

### Standard Try-Catch with Service_ErrorHandler

```csharp
private async Task PerformDatabaseOperationAsync()
{
    try
    {
        var result = await _dao.GetDataAsync();
        if (!result.IsSuccess)
        {
            _errorHandler.ShowUserError(result.ErrorMessage);
            return;
        }
        
        // Process result.Data
        ProcessData(result.Data);
    }
    catch (MySql.Data.MySqlClient.MySqlException ex)
    {
        _errorHandler.HandleDatabaseError(ex,
            retryAction: () => PerformDatabaseOperationAsync().Wait(),
            callerName: nameof(PerformDatabaseOperationAsync),
            controlName: Name);
    }
    catch (Exception ex)
    {
        _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["User"] = Model_Application_Variables.User,
                ["Operation"] = "GetData"
            },
            callerName: nameof(PerformDatabaseOperationAsync),
            controlName: Name);
    }
}
```

---

## 8. Keyboard Shortcuts Pattern

### Form-Level Key Handling

```csharp
protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
{
    switch (keyData)
    {
        case Keys.Control | Keys.F:
            // Focus search textbox
            txtSearch.Focus();
            return true;
            
        case Keys.Control | Keys.R:
        case Keys.F5:
            // Refresh current tab
            _ = RefreshCurrentTabAsync();
            return true;
            
        case Keys.Escape:
            // Clear filters
            ClearAllFilters();
            return true;
    }
    
    return base.ProcessCmdKey(ref msg, keyData);
}
```

---

## 9. Common Pitfalls to Avoid

### ❌ DON'T: Use MessageBox.Show directly

```csharp
// WRONG
catch (Exception ex)
{
    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

### ✅ DO: Use Service_ErrorHandler

```csharp
// CORRECT
catch (Exception ex)
{
    _errorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
        callerName: nameof(MyMethod),
        controlName: Name);
}
```

### ❌ DON'T: Block async calls

```csharp
// WRONG - blocks UI thread
var result = _service.GetDataAsync().Result;
```

### ✅ DO: Use async/await

```csharp
// CORRECT
var result = await _service.GetDataAsync();
```

### ❌ DON'T: Forget Model_Dao_Result checks

```csharp
// WRONG - may access null Data
var result = await _dao.GetDataAsync();
var data = result.Data; // NullReferenceException if IsSuccess=false
```

### ✅ DO: Always check IsSuccess

```csharp
// CORRECT
var result = await _dao.GetDataAsync();
if (!result.IsSuccess)
{
    _errorHandler.ShowUserError(result.ErrorMessage);
    return;
}
var data = result.Data; // Safe to access
```

---

## 10. Testing Patterns

### Integration Test Example

```csharp
public class Service_DeveloperToolsTests : BaseIntegrationTest
{
    private IService_DeveloperTools _service = null!;
    
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _service = new Service_DeveloperTools(
            new Service_Logging(),
            new Dao_DeveloperTools(),
            new Service_FeedbackManager(...));
    }
    
    [Fact]
    public async Task GetLogStatisticsAsync_ValidDateRange_ReturnsStatistics()
    {
        // Arrange
        var start = DateTime.Today.AddDays(-7);
        var end = DateTime.Now;
        
        // Act
        var result = await _service.GetLogStatisticsAsync(start, end);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.TotalCount >= 0);
    }
    
    [Fact]
    public async Task GetLogEntriesAsync_ErrorsOnly_ReturnsOnlyErrors()
    {
        // Arrange
        var filter = Model_LogFilter.ErrorsOnly();
        
        // Act
        var result = await _service.GetLogEntriesAsync(filter);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.All(result.Data, entry => 
            Assert.True(entry.Level == "Error" || entry.Level == "Critical"));
    }
}
```
