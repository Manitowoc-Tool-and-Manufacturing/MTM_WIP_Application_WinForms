using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming;

/// <summary>
/// Provides theme storage and retrieval with two-level caching.
/// Wraps Core_AppThemes functionality for dependency injection.
/// </summary>
public class ThemeStore : IThemeStore
{
    private readonly ILogger<ThemeStore> _logger;

    // Level 1 cache: Theme objects by name
    private readonly Dictionary<string, Model_Shared_UserUiColors> _themeCache = new();

    // Level 2 cache: Last applied theme per control (for performance optimization)
    private readonly Dictionary<Control, Model_Shared_UserUiColors> _appliedCache = new();

    private readonly object _cacheLock = new();

    /// <summary>
    /// Initializes a new instance of the ThemeStore class.
    /// </summary>
    public ThemeStore(ILogger<ThemeStore> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets a theme by name asynchronously.
    /// </summary>
    public async Task<Model_Shared_UserUiColors?> GetThemeAsync(string themeName)
    {
        if (string.IsNullOrWhiteSpace(themeName))
        {
            _logger.LogWarning("GetThemeAsync called with null or empty theme name");
            return GetDefaultTheme();
        }

        // Check cache first
        lock (_cacheLock)
        {
            if (_themeCache.TryGetValue(themeName, out var cachedTheme))
            {
                _logger.LogDebug("Theme '{ThemeName}' retrieved from cache", themeName);
                return cachedTheme;
            }
        }

        // Load from database if not cached
        await LoadFromDatabaseAsync();

        // Try cache again after load
        lock (_cacheLock)
        {
            if (_themeCache.TryGetValue(themeName, out var theme))
            {
                return theme;
            }
        }

        _logger.LogWarning("Theme '{ThemeName}' not found, returning default", themeName);
        return GetDefaultTheme();
    }

    /// <summary>
    /// Gets all available themes asynchronously.
    /// </summary>
    public async Task<IEnumerable<Model_Shared_UserUiColors>> GetAllThemesAsync()
    {
        await LoadFromDatabaseAsync();

        lock (_cacheLock)
        {
            return _themeCache.Values.ToList(); // Return copy to prevent external modification
        }
    }

    /// <summary>
    /// Loads themes from the database asynchronously, populating the cache.
    /// </summary>
    public async Task LoadFromDatabaseAsync()
    {
        try
        {
            _logger.LogInformation("Loading themes from database via Core_AppThemes");

            // Use Core_AppThemes to load themes from database
            // Core_AppThemes.LoadThemesFromDatabaseAsync() has already been called during startup
            // So we just need to access Core_AppThemes.Themes dictionary

            lock (_cacheLock)
            {
                _themeCache.Clear();

                // Copy all themes from Core_AppThemes into our cache
                var themeNames = Core_AppThemes.GetThemeNames();

                foreach (var themeName in themeNames)
                {
                    var appTheme = Core_AppThemes.GetTheme(themeName);

                    if (appTheme?.Colors != null)
                    {
                        _themeCache[themeName] = appTheme.Colors;
                        _logger.LogDebug("Cached theme '{ThemeName}' from Core_AppThemes", themeName);
                    }
                }

                // Ensure default theme exists
                if (!_themeCache.ContainsKey("Default"))
                {
                    _themeCache["Default"] = GetDefaultTheme();
                    _logger.LogWarning("No 'Default' theme found in database, using hardcoded default");
                }
            }

            _logger.LogInformation("Loaded {ThemeCount} themes into ThemeStore cache", _themeCache.Count);


            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load themes from database");
            LoggingUtility.LogApplicationError(ex);

            // Ensure at least default theme is available
            lock (_cacheLock)
            {
                if (!_themeCache.ContainsKey("Default"))
                {
                    _themeCache["Default"] = GetDefaultTheme();
                }
            }
        }
    }

    /// <summary>
    /// Gets the default theme to use when no theme is specified or theme loading fails.
    /// </summary>
    public Model_Shared_UserUiColors GetDefaultTheme()
    {
        // Return the dark theme as default (matches existing behavior)
        return new Model_Shared_UserUiColors
        {
            FormBackColor = Color.FromArgb(30, 30, 30),
            FormForeColor = Color.FromArgb(255, 255, 255),
            ControlBackColor = Color.FromArgb(30, 30, 30),
            ControlForeColor = Color.FromArgb(255, 255, 255),
            LabelBackColor = Color.FromArgb(30, 30, 30),
            LabelForeColor = Color.FromArgb(204, 204, 204),
            TextBoxBackColor = Color.FromArgb(30, 30, 30),
            TextBoxForeColor = Color.FromArgb(255, 255, 255),
            ButtonBackColor = Color.FromArgb(45, 45, 48),
            ButtonForeColor = Color.FromArgb(255, 255, 255),
            DataGridBackColor = Color.FromArgb(30, 30, 30),
            DataGridForeColor = Color.FromArgb(255, 255, 255),
            DataGridGridColor = Color.FromArgb(60, 60, 60),
            DataGridHeaderBackColor = Color.FromArgb(45, 45, 48),
            DataGridHeaderForeColor = Color.FromArgb(255, 255, 255)
        };
    }

    /// <summary>
    /// Creates a light theme variant for demonstration purposes.
    /// </summary>
    private Model_Shared_UserUiColors CreateLightTheme()
    {
        return new Model_Shared_UserUiColors
        {
            FormBackColor = Color.FromArgb(240, 240, 240),
            FormForeColor = Color.FromArgb(0, 0, 0),
            ControlBackColor = Color.FromArgb(255, 255, 255),
            ControlForeColor = Color.FromArgb(0, 0, 0),
            LabelBackColor = Color.FromArgb(240, 240, 240),
            LabelForeColor = Color.FromArgb(60, 60, 60),
            TextBoxBackColor = Color.FromArgb(255, 255, 255),
            TextBoxForeColor = Color.FromArgb(0, 0, 0),
            ButtonBackColor = Color.FromArgb(225, 225, 225),
            ButtonForeColor = Color.FromArgb(0, 0, 0),
            DataGridBackColor = Color.FromArgb(255, 255, 255),
            DataGridForeColor = Color.FromArgb(0, 0, 0),
            DataGridGridColor = Color.FromArgb(200, 200, 200),
            DataGridHeaderBackColor = Color.FromArgb(225, 225, 225),
            DataGridHeaderForeColor = Color.FromArgb(0, 0, 0)
        };
    }

    /// <summary>
    /// Checks if a theme should be applied to a control (cache hit check).
    /// Returns false if the same theme is already applied (performance optimization).
    /// </summary>
    public bool ShouldApplyTheme(Control control, Model_Shared_UserUiColors theme)
    {
        lock (_cacheLock)
        {
            if (_appliedCache.TryGetValue(control, out var lastTheme))
            {
                // Skip if same theme already applied
                return !ReferenceEquals(lastTheme, theme);
            }
            return true; // No cache entry, should apply
        }
    }

    /// <summary>
    /// Records that a theme has been applied to a control.
    /// </summary>
    public void RecordApplied(Control control, Model_Shared_UserUiColors theme)
    {
        lock (_cacheLock)
        {
            _appliedCache[control] = theme;
        }
    }

    /// <summary>
    /// Clears the applied cache (called on theme change).
    /// This invalidates all cached theme applications, forcing full re-application.
    /// </summary>
    public void ClearAppliedCache()
    {
        lock (_cacheLock)
        {
            _appliedCache.Clear();
            _logger.LogDebug("Applied theme cache cleared ({Count} entries removed)", _appliedCache.Count);
        }
    }
}
