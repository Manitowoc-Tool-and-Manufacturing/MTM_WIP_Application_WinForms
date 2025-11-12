using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming;

/// <summary>
/// Specifies the reason for a theme change.
/// </summary>
public enum ThemeChangeReason
{
    /// <summary>
    /// Theme changed due to user selection in settings.
    /// </summary>
    UserSelection,

    /// <summary>
    /// Theme applied during user login based on saved preference.
    /// </summary>
    Login,

    /// <summary>
    /// Theme change triggered by DPI scaling change.
    /// </summary>
    DpiChange,

    /// <summary>
    /// Default theme applied (fallback scenario).
    /// </summary>
    SystemDefault,

    /// <summary>
    /// Theme preview in a separate window (not global change).
    /// </summary>
    Preview
}

/// <summary>
/// Provides data for theme change events.
/// </summary>
public class ThemeChangedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the theme that was active before the change.
    /// </summary>
    public Model_Shared_UserUiColors? OldTheme { get; }

    /// <summary>
    /// Gets the newly activated theme.
    /// </summary>
    public Model_Shared_UserUiColors NewTheme { get; }

    /// <summary>
    /// Gets the user ID associated with this theme change, if applicable.
    /// </summary>
    public string? UserId { get; }

    /// <summary>
    /// Gets the timestamp when the theme change occurred.
    /// </summary>
    public DateTime ChangedAt { get; }

    /// <summary>
    /// Gets the reason for the theme change.
    /// </summary>
    public ThemeChangeReason Reason { get; }

    /// <summary>
    /// Initializes a new instance of the ThemeChangedEventArgs class.
    /// </summary>
    /// <param name="oldTheme">The previous theme.</param>
    /// <param name="newTheme">The new theme.</param>
    /// <param name="userId">The user ID, if applicable.</param>
    /// <param name="reason">The reason for the change.</param>
    public ThemeChangedEventArgs(
        Model_Shared_UserUiColors? oldTheme,
        Model_Shared_UserUiColors newTheme,
        string? userId = null,
        ThemeChangeReason reason = ThemeChangeReason.UserSelection)
    {
        OldTheme = oldTheme;
        NewTheme = newTheme ?? throw new ArgumentNullException(nameof(newTheme));
        UserId = userId;
        ChangedAt = DateTime.Now;
        Reason = reason;
    }
}
