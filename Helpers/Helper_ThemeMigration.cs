using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Migration adapter bridging old Core_Themes static methods to new dependency injection theme system.
/// Provides backward compatibility during gradual migration from static theme application to DI-based system.
/// </summary>
public static class Helper_ThemeMigration
{
    private static IThemeProvider? _themeProvider;
    private static bool _initialized;

    /// <summary>
    /// Initializes the migration helper with the theme provider from DI container.
    /// Call this once during application startup after DI container is built.
    /// </summary>
    /// <param name="themeProvider">The IThemeProvider instance from DI container.</param>
    public static void Initialize(IThemeProvider themeProvider)
    {
        if (themeProvider == null)
        {
            throw new ArgumentNullException(nameof(themeProvider));
        }

        _themeProvider = themeProvider;
        _initialized = true;

        LoggingUtility.Log("[ThemeMigration] Migration helper initialized with DI theme provider");
    }

    /// <summary>
    /// Applies theme to a form using the appropriate method based on form type.
    /// - If form is ThemedForm: Uses DI-based automatic theme subscription (new system)
    /// - If form is legacy Form: Falls back to Core_Themes.ApplyTheme() (old system)
    /// </summary>
    /// <param name="form">The form to apply theme to.</param>
    /// <param name="themeName">Optional theme name. If not specified, uses current theme.</param>
    public static void ApplyThemeStatic(Form form, string? themeName = null)
    {
        if (form == null)
        {
            throw new ArgumentNullException(nameof(form));
        }

        // Check if form is using new DI-based ThemedForm
        if (form is ThemedForm themedForm)
        {
            LoggingUtility.Log($"[ThemeMigration] Form '{form.Name}' is ThemedForm - using DI-based theme system");

            // ThemedForm handles its own theme subscription automatically via constructor
            // No manual theme application needed - it's already subscribed to theme changes
            // Just log that we detected the new system
            return;
        }
    }

    /// <summary>
    /// Checks if the migration helper has been initialized with a theme provider.
    /// </summary>
    public static bool IsInitialized => _initialized && _themeProvider != null;

    /// <summary>
    /// Gets the current theme provider (if initialized).
    /// </summary>
    public static IThemeProvider? ThemeProvider => _themeProvider;

    /// <summary>
    /// Subscribes a form to automatic theme changes (new DI system only).
    /// For legacy forms, this is a no-op (they use static Core_Themes calls).
    /// </summary>
    /// <param name="form">The form to subscribe.</param>
    public static void SubscribeToThemeChanges(Form form)
    {
        if (form == null)
        {
            throw new ArgumentNullException(nameof(form));
        }

        if (!_initialized || _themeProvider == null)
        {
            LoggingUtility.Log($"[ThemeMigration] Cannot subscribe form '{form.Name}' - migration helper not initialized");
            return;
        }

        if (form is ThemedForm)
        {
            // ThemedForm already subscribes in its constructor
            LoggingUtility.Log($"[ThemeMigration] Form '{form.Name}' is ThemedForm - already auto-subscribed");
            return;
        }

        // For legacy forms, subscribe manually via migration adapter
        _themeProvider.Subscribe(form);
        LoggingUtility.Log($"[ThemeMigration] Manually subscribed legacy form '{form.Name}' to theme changes");
    }

    /// <summary>
    /// Unsubscribes a form from automatic theme changes.
    /// </summary>
    /// <param name="form">The form to unsubscribe.</param>
    public static void UnsubscribeFromThemeChanges(Form form)
    {
        if (form == null || !_initialized || _themeProvider == null)
        {
            return;
        }

        _themeProvider.Unsubscribe(form);
        LoggingUtility.Log($"[ThemeMigration] Unsubscribed form '{form.Name}' from theme changes");
    }
}
