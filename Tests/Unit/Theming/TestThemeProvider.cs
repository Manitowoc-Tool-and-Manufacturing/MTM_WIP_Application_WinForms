using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Tests.Unit.Theming;

/// <summary>
/// T088: Mock implementation of IThemeProvider for testing scenarios.
/// Allows tests to verify theme logic without running UI or database.
/// </summary>
public class TestThemeProvider : IThemeProvider
{
    private Model_Shared_UserUiColors? _currentTheme;
    private readonly List<EventHandler<ThemeChangedEventArgs>> _eventHandlers = new();

    /// <summary>
    /// Gets or sets the current theme for testing.
    /// </summary>
    public Model_Shared_UserUiColors? CurrentTheme
    {
        get => _currentTheme;
        set
        {
            var oldTheme = _currentTheme;
            _currentTheme = value;
            
            if (value != null)
            {
                RaiseThemeChanged(oldTheme, value, "TEST_USER", ThemeChangeReason.UserSelection);
            }
        }
    }

    /// <summary>
    /// Event raised when theme changes.
    /// </summary>
    public event EventHandler<ThemeChangedEventArgs>? ThemeChanged;

    /// <summary>
    /// Creates a test theme provider with an optional default theme.
    /// </summary>
    public TestThemeProvider(Model_Shared_UserUiColors? defaultTheme = null)
    {
        _currentTheme = defaultTheme ?? CreateDefaultTestTheme();
    }

    /// <summary>
    /// Sets the theme for testing (synchronous for easier testing).
    /// </summary>
    public Task SetThemeAsync(
        string themeName,
        ThemeChangeReason reason = ThemeChangeReason.UserSelection,
        string? userId = null)
    {
        var oldTheme = _currentTheme;
        _currentTheme = GetTestThemeByName(themeName);
        
        if (_currentTheme != null)
        {
            RaiseThemeChanged(oldTheme, _currentTheme, userId ?? "TEST_USER", reason);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Subscribes a form to theme change notifications (no-op for testing).
    /// </summary>
    public void Subscribe(Form form)
    {
        // For testing, we don't need to track subscriptions
        // Tests can verify theme changes directly
    }

    /// <summary>
    /// Unsubscribes a form from theme change notifications (no-op for testing).
    /// </summary>
    public void Unsubscribe(Form form)
    {
        // For testing, we don't need to track subscriptions
    }

    /// <summary>
    /// Raises the ThemeChanged event for testing.
    /// </summary>
    private void RaiseThemeChanged(
        Model_Shared_UserUiColors? oldTheme,
        Model_Shared_UserUiColors newTheme,
        string? userId,
        ThemeChangeReason reason)
    {
        var args = new ThemeChangedEventArgs(oldTheme, newTheme, userId, reason);
        ThemeChanged?.Invoke(this, args);
    }

    /// <summary>
    /// Gets a test theme by name for testing scenarios.
    /// </summary>
    private static Model_Shared_UserUiColors GetTestThemeByName(string themeName)
    {
        return themeName.ToLowerInvariant() switch
        {
            "dark" => new Model_Shared_UserUiColors
            {
                FormBackColor = Color.FromArgb(30, 30, 30),
                FormForeColor = Color.FromArgb(240, 240, 240),
                ControlBackColor = Color.FromArgb(45, 45, 45),
                ControlForeColor = Color.FromArgb(240, 240, 240),
                ButtonBackColor = Color.FromArgb(60, 60, 60),
                ButtonForeColor = Color.FromArgb(255, 255, 255),
                TextBoxBackColor = Color.FromArgb(50, 50, 50),
                TextBoxForeColor = Color.FromArgb(240, 240, 240),
                DataGridBackColor = Color.FromArgb(40, 40, 40),
                DataGridForeColor = Color.FromArgb(240, 240, 240)
            },
            "light" => new Model_Shared_UserUiColors
            {
                FormBackColor = Color.FromArgb(240, 240, 240),
                FormForeColor = Color.FromArgb(20, 20, 20),
                ControlBackColor = Color.FromArgb(255, 255, 255),
                ControlForeColor = Color.FromArgb(20, 20, 20),
                ButtonBackColor = Color.FromArgb(225, 225, 225),
                ButtonForeColor = Color.FromArgb(20, 20, 20),
                TextBoxBackColor = Color.FromArgb(255, 255, 255),
                TextBoxForeColor = Color.FromArgb(20, 20, 20),
                DataGridBackColor = Color.FromArgb(255, 255, 255),
                DataGridForeColor = Color.FromArgb(20, 20, 20)
            },
            _ => CreateDefaultTestTheme()
        };
    }

    /// <summary>
    /// Creates a default test theme.
    /// </summary>
    private static Model_Shared_UserUiColors CreateDefaultTestTheme()
    {
        return new Model_Shared_UserUiColors
        {
            FormBackColor = Color.FromArgb(240, 240, 240),
            FormForeColor = Color.FromArgb(0, 0, 0),
            ControlBackColor = Color.FromArgb(255, 255, 255),
            ControlForeColor = Color.FromArgb(0, 0, 0),
            ButtonBackColor = Color.FromArgb(225, 225, 225),
            ButtonForeColor = Color.FromArgb(0, 0, 0),
            TextBoxBackColor = Color.FromArgb(255, 255, 255),
            TextBoxForeColor = Color.FromArgb(0, 0, 0),
            DataGridBackColor = Color.FromArgb(255, 255, 255),
            DataGridForeColor = Color.FromArgb(0, 0, 0)
        };
    }
}
