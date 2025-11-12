using System.Timers;
using Microsoft.Extensions.Logging;

namespace MTM_WIP_Application_Winforms.Core.Theming;

/// <summary>
/// Debounces rapid theme changes to prevent excessive processing.
/// Uses a 300ms threshold - only the final theme change is applied.
/// </summary>
public class ThemeDebouncer : IDisposable
{
    private readonly ILogger<ThemeDebouncer> _logger;
    private readonly System.Timers.Timer _timer;
    private readonly object _lock = new();
    private string? _pendingThemeName;
    private ThemeChangeReason _pendingReason;
    private string? _pendingUserId;
    private Func<string, ThemeChangeReason, string?, Task>? _callback;
    private bool _disposed;

    /// <summary>
    /// Gets the debounce threshold in milliseconds.
    /// </summary>
    public int ThresholdMs { get; }

    /// <summary>
    /// Initializes a new instance of the ThemeDebouncer class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <param name="thresholdMs">Debounce threshold in milliseconds (default: 300ms).</param>
    public ThemeDebouncer(ILogger<ThemeDebouncer> logger, int thresholdMs = 300)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        if (thresholdMs <= 0)
        {
            throw new ArgumentException("Threshold must be positive", nameof(thresholdMs));
        }

        ThresholdMs = thresholdMs;
        
        _timer = new System.Timers.Timer(thresholdMs)
        {
            AutoReset = false
        };
        
        _timer.Elapsed += OnTimerElapsed;
    }

    /// <summary>
    /// Debounces a theme change request. Only the last request within the threshold period is executed.
    /// </summary>
    /// <param name="themeName">The theme name to apply.</param>
    /// <param name="reason">The reason for the change.</param>
    /// <param name="userId">Optional user ID.</param>
    /// <param name="callback">The callback to execute after the debounce period.</param>
    public void Debounce(
        string themeName,
        ThemeChangeReason reason,
        string? userId,
        Func<string, ThemeChangeReason, string?, Task> callback)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(ThemeDebouncer));
        }

        lock (_lock)
        {
            // Store the latest values
            _pendingThemeName = themeName;
            _pendingReason = reason;
            _pendingUserId = userId;
            _callback = callback;

            // Reset the timer
            _timer.Stop();
            _timer.Start();

            _logger.LogDebug(
                "Theme change debounced: {ThemeName} (will apply if no changes within {ThresholdMs}ms)",
                themeName,
                ThresholdMs);
        }
    }

    private async void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        string? themeName;
        ThemeChangeReason reason;
        string? userId;
        Func<string, ThemeChangeReason, string?, Task>? callback;

        lock (_lock)
        {
            themeName = _pendingThemeName;
            reason = _pendingReason;
            userId = _pendingUserId;
            callback = _callback;

            // Clear pending state
            _pendingThemeName = null;
            _callback = null;
        }

        if (callback != null && themeName != null)
        {
            try
            {
                _logger.LogInformation(
                    "Applying debounced theme change: {ThemeName} (Reason: {Reason})",
                    themeName,
                    reason);

                await callback(themeName, reason, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying debounced theme change");
            }
        }
    }

    /// <summary>
    /// Flushes any pending theme change immediately without waiting for the debounce period.
    /// </summary>
    public async Task FlushAsync()
    {
        string? themeName;
        ThemeChangeReason reason;
        string? userId;
        Func<string, ThemeChangeReason, string?, Task>? callback;

        lock (_lock)
        {
            _timer.Stop();
            
            themeName = _pendingThemeName;
            reason = _pendingReason;
            userId = _pendingUserId;
            callback = _callback;

            _pendingThemeName = null;
            _callback = null;
        }

        if (callback != null && themeName != null)
        {
            await callback(themeName, reason, userId);
        }
    }

    /// <summary>
    /// Disposes the debouncer and releases resources.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _timer.Stop();
        _timer.Elapsed -= OnTimerElapsed;
        _timer.Dispose();
        
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
