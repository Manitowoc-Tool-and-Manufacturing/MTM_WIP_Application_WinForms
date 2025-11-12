using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Interfaces;

/// <summary>
/// Provides theme management capabilities including theme selection, change notifications, and form subscription.
/// </summary>
public interface IThemeProvider
{
    /// <summary>
    /// Gets the currently active theme.
    /// </summary>
    Model_Shared_UserUiColors? CurrentTheme { get; }

    /// <summary>
    /// Event raised when the theme changes. Subscribers receive the old and new theme information.
    /// </summary>
    event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

    /// <summary>
    /// Sets the active theme asynchronously.
    /// </summary>
    /// <param name="themeName">The name of the theme to activate.</param>
    /// <param name="reason">The reason for the theme change.</param>
    /// <param name="userId">Optional user ID for user-specific theme changes.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetThemeAsync(string themeName, ThemeChangeReason reason = ThemeChangeReason.UserSelection, string? userId = null);

    /// <summary>
    /// Subscribes a form to automatic theme change notifications.
    /// </summary>
    /// <param name="form">The form to subscribe.</param>
    void Subscribe(Form form);

    /// <summary>
    /// Unsubscribes a form from theme change notifications.
    /// </summary>
    /// <param name="form">The form to unsubscribe.</param>
    void Unsubscribe(Form form);
}
