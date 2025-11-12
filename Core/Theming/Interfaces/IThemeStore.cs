using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Interfaces;

/// <summary>
/// Provides access to theme data with caching capabilities.
/// Wraps the existing Core_AppThemes functionality.
/// </summary>
public interface IThemeStore
{
    /// <summary>
    /// Gets a theme by name asynchronously.
    /// </summary>
    /// <param name="themeName">The name of the theme to retrieve.</param>
    /// <returns>The theme if found; otherwise, null.</returns>
    Task<Model_Shared_UserUiColors?> GetThemeAsync(string themeName);

    /// <summary>
    /// Gets all available themes asynchronously.
    /// </summary>
    /// <returns>A collection of all available themes.</returns>
    Task<IEnumerable<Model_Shared_UserUiColors>> GetAllThemesAsync();

    /// <summary>
    /// Loads themes from the database asynchronously, populating the cache.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LoadFromDatabaseAsync();

    /// <summary>
    /// Gets the default theme to use when no theme is specified or theme loading fails.
    /// </summary>
    /// <returns>The default theme (never null).</returns>
    Model_Shared_UserUiColors GetDefaultTheme();
}
