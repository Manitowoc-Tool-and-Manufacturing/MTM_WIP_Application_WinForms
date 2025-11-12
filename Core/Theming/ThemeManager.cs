using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming;

/// <summary>
/// Manages theme application and change notifications using the observer pattern.
/// Tracks subscribed forms using WeakReference to prevent memory leaks.
/// </summary>
public class ThemeManager : IThemeProvider, IDisposable
{
    private readonly IThemeStore _themeStore;
    private readonly ILogger<ThemeManager> _logger;
    private readonly ThemeDebouncer _debouncer;
    private readonly List<WeakReference<Form>> _subscribers = new();
    private readonly object _subscriberLock = new();
    private readonly Dictionary<Form, PendingThemeChange> _initializationQueue = new(); // T080: Queue for forms initializing
    private Model_Shared_UserUiColors? _currentTheme;
    private bool _disposed;

    /// <summary>
    /// T080: Represents a pending theme change for a form during initialization.
    /// </summary>
    private class PendingThemeChange
    {
        public string ThemeName { get; set; } = string.Empty;
        public ThemeChangeReason Reason { get; set; }
        public string? UserId { get; set; }
    }

    /// <summary>
    /// Gets the currently active theme.
    /// </summary>
    public Model_Shared_UserUiColors? CurrentTheme
    {
        get => _currentTheme;
        private set => _currentTheme = value;
    }

    /// <summary>
    /// Event raised when the theme changes.
    /// </summary>
    public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

    /// <summary>
    /// Initializes a new instance of the ThemeManager class.
    /// </summary>
    public ThemeManager(
        IThemeStore themeStore,
        ILogger<ThemeManager> logger,
        ThemeDebouncer debouncer)
    {
        _themeStore = themeStore ?? throw new ArgumentNullException(nameof(themeStore));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _debouncer = debouncer ?? throw new ArgumentNullException(nameof(debouncer));

        // Initialize with default theme
        _currentTheme = _themeStore.GetDefaultTheme();
    }

    /// <summary>
    /// Sets the active theme asynchronously with debouncing.
    /// </summary>
    public async Task SetThemeAsync(
        string themeName,
        ThemeChangeReason reason = ThemeChangeReason.UserSelection,
        string? userId = null)
    {
        if (string.IsNullOrWhiteSpace(themeName))
        {
            _logger.LogWarning("SetThemeAsync called with null or empty theme name");
            return;
        }

        // Debounce rapid changes
        _debouncer.Debounce(themeName, reason, userId, ApplyThemeInternalAsync);
    }

    /// <summary>
    /// Internal method to apply theme after debouncing.
    /// </summary>
    private async Task ApplyThemeInternalAsync(
        string themeName,
        ThemeChangeReason reason,
        string? userId)
    {
        try
        {
            var oldTheme = CurrentTheme;
            var newTheme = await _themeStore.GetThemeAsync(themeName);

            if (newTheme == null)
            {
                _logger.LogWarning("Theme '{ThemeName}' not found, using default", themeName);
                newTheme = _themeStore.GetDefaultTheme();
            }

            CurrentTheme = newTheme;

            _logger.LogInformation(
                "Theme changed to '{ThemeName}' (Reason: {Reason}, User: {UserId})",
                themeName,
                reason,
                userId ?? "<none>");

            LoggingUtility.Log($"Theme changed to '{themeName}' - Reason: {reason}");

            // Notify all subscribers
            NotifySubscribers(oldTheme, newTheme, userId, reason);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to set theme {ThemeName}, reason: {Reason}", themeName, reason);
            LoggingUtility.LogApplicationError(ex);

            // Don't rethrow - log and continue with current theme
        }
    }

    /// <summary>
    /// Subscribes a form to automatic theme change notifications.
    /// </summary>
    public void Subscribe(Form form)
    {
        if (form == null)
        {
            throw new ArgumentNullException(nameof(form));
        }

        lock (_subscriberLock)
        {
            // Clean up dead references
            CleanupDeadReferences();

            // Check if already subscribed
            foreach (var weakRef in _subscribers)
            {
                if (weakRef.TryGetTarget(out var existingForm) && ReferenceEquals(existingForm, form))
                {
                    _logger.LogDebug("Form '{FormName}' is already subscribed", form.Name);
                    return;
                }
            }

            // Add new subscription
            _subscribers.Add(new WeakReference<Form>(form));
            _logger.LogInformation("Form '{FormName}' subscribed to theme changes", form.Name);
        }
    }

    /// <summary>
    /// Unsubscribes a form from theme change notifications.
    /// </summary>
    public void Unsubscribe(Form form)
    {
        if (form == null)
        {
            return;
        }

        lock (_subscriberLock)
        {
            _subscribers.RemoveAll(weakRef =>
            {
                if (!weakRef.TryGetTarget(out var target))
                {
                    return true; // Remove dead reference
                }
                return ReferenceEquals(target, form); // Remove if matches
            });

            _logger.LogInformation("Form '{FormName}' unsubscribed from theme changes", form.Name);
        }
    }

    /// <summary>
    /// Notifies all subscribed forms of a theme change.
    /// </summary>
    private void NotifySubscribers(
        Model_Shared_UserUiColors? oldTheme,
        Model_Shared_UserUiColors newTheme,
        string? userId,
        ThemeChangeReason reason)
    {
        var eventArgs = new ThemeChangedEventArgs(oldTheme, newTheme, userId, reason);

        lock (_subscriberLock)
        {
            CleanupDeadReferences();

            foreach (var weakRef in _subscribers.ToList()) // ToList for safe iteration
            {
                if (weakRef.TryGetTarget(out var form))
                {
                    try
                    {
                        // Invoke on UI thread if required
                        if (form.InvokeRequired)
                        {
                            form.Invoke(() => RaiseThemeChanged(form, eventArgs));
                        }
                        else
                        {
                            RaiseThemeChanged(form, eventArgs);
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // Form was disposed, will be cleaned up in next cleanup
                        _logger.LogDebug("Form '{FormName}' was disposed during notification", form.Name);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(
                            ex,
                            "Error notifying form '{FormName}' of theme change",
                            form.Name);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Raises the ThemeChanged event for a specific form.
    /// </summary>
    private void RaiseThemeChanged(Form form, ThemeChangedEventArgs eventArgs)
    {
        try
        {
            ThemeChanged?.Invoke(this, eventArgs);
            _logger.LogDebug("Theme change notification sent to form '{FormName}'", form.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ThemeChanged event handler for form '{FormName}'", form.Name);
        }
    }

    /// <summary>
    /// Removes dead WeakReference entries (garbage collected forms).
    /// </summary>
    private void CleanupDeadReferences()
    {
        var beforeCount = _subscribers.Count;
        _subscribers.RemoveAll(weakRef => !weakRef.TryGetTarget(out _));
        var removed = beforeCount - _subscribers.Count;

        if (removed > 0)
        {
            _logger.LogDebug("Cleaned up {Count} dead form references", removed);
        }
    }

    /// <summary>
    /// T080: Marks a form as initializing, queuing theme changes until it completes loading.
    /// </summary>
    public void MarkFormInitializing(Form form)
    {
        if (form == null) return;

        lock (_subscriberLock)
        {
            if (!_initializationQueue.ContainsKey(form))
            {
                _initializationQueue[form] = new PendingThemeChange();
                _logger.LogDebug("Form '{FormName}' marked as initializing, theme changes will be queued", form.Name);

                // Subscribe to Load event to apply queued theme
                form.Load += OnFormLoadComplete;
            }
        }
    }

    /// <summary>
    /// T080: Applies queued theme changes when form finishes loading.
    /// </summary>
    private async void OnFormLoadComplete(object? sender, EventArgs e)
    {
        if (sender is not Form form) return;

        // Unsubscribe from Load event
        form.Load -= OnFormLoadComplete;

        PendingThemeChange? pending = null;
        lock (_subscriberLock)
        {
            if (_initializationQueue.TryGetValue(form, out pending))
            {
                _initializationQueue.Remove(form);
            }
        }

        // Apply the queued theme change if one exists
        if (pending != null && !string.IsNullOrEmpty(pending.ThemeName))
        {
            _logger.LogInformation("Applying queued theme '{ThemeName}' to form '{FormName}' after load complete",
                pending.ThemeName, form.Name);
            
            await SetThemeAsync(pending.ThemeName, pending.Reason, pending.UserId);
        }
    }

    /// <summary>
    /// T080: Checks if a form is currently initializing.
    /// </summary>
    public bool IsFormInitializing(Form form)
    {
        if (form == null) return false;

        lock (_subscriberLock)
        {
            return _initializationQueue.ContainsKey(form);
        }
    }

    /// <summary>
    /// Disposes the theme manager and releases resources.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _debouncer.Dispose();
        
        lock (_subscriberLock)
        {
            _subscribers.Clear();
        }

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
