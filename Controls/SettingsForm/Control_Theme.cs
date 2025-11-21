using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Theme : ThemedUserControl
    {
        public event EventHandler? ThemeChanged;
        public event EventHandler<string>? StatusMessageChanged;

        public Control_Theme()
        {
            InitializeComponent();
            Control_Themes_Button_Save.Click += SaveButton_Click;
            Control_Themes_Button_Preview.Click += PreviewButton_Click;
            LoadThemeSettingsAsync();
        }

        public async void LoadThemeSettingsAsync()
        {
            try
            {
                Control_Themes_ComboBox_Theme.Items.Clear();
                string[] themeNames = [.. Core_AppThemes.GetThemeNames()];
                Control_Themes_ComboBox_Theme.Items.AddRange(themeNames);

                string user = Model_Application_Variables.User;

                // DEFENSIVE: Validate user is not corrupted (should never be a type name)
                if (string.IsNullOrWhiteSpace(user) || user.Contains("System.") || user.Contains("DataRow"))
                {

                    user = Environment.UserName?.ToUpperInvariant() ?? "UNKNOWN";
                }

                var themeResult = await Dao_User.GetThemeNameAsync(user);

                if (themeResult.IsSuccess)
                {
                    string? themeName = themeResult.Data;
                    if (!string.IsNullOrEmpty(themeName) && Control_Themes_ComboBox_Theme.Items.Contains(themeName))
                    {
                        Control_Themes_ComboBox_Theme.SelectedItem = themeName;
                    }
                    else if (Control_Themes_ComboBox_Theme.Items.Count > 0)
                    {
                        Control_Themes_ComboBox_Theme.SelectedIndex = 0;
                    }

                    StatusMessageChanged?.Invoke(this, "Theme settings loaded successfully.");
                }
                else
                {
                    // Handle database error gracefully
                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception($"Failed to load theme settings: {themeResult.ErrorMessage}"),
                        contextData: new Dictionary<string, object> { ["User"] = user },
                        callerName: nameof(LoadThemeSettingsAsync),
                        controlName: nameof(Control_Theme)
                    );

                    // Fallback to first theme if available
                    if (Control_Themes_ComboBox_Theme.Items.Count > 0)
                    {
                        Control_Themes_ComboBox_Theme.SelectedIndex = 0;
                    }

                    StatusMessageChanged?.Invoke(this, $"Error loading theme settings: {themeResult.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["User"] = Model_Application_Variables.User },
                    callerName: nameof(LoadThemeSettingsAsync),
                    controlName: nameof(Control_Theme));

                // Fallback to first theme if available
                if (Control_Themes_ComboBox_Theme.Items.Count > 0)
                {
                    Control_Themes_ComboBox_Theme.SelectedIndex = 0;
                }
            }
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            try
            {
                Control_Themes_Button_Save.Enabled = false;

                string user = Model_Application_Variables.User;

                // DEFENSIVE: Validate user is not corrupted before saving
                if (string.IsNullOrWhiteSpace(user) || user.Contains("System.") || user.Contains("DataRow"))
                {

                    Service_ErrorHandler.HandleValidationError(
                        $"Cannot save theme: User identity is corrupted ('{user}'). Please restart the application.",
                        "User Identity",
                        callerName: nameof(SaveButton_Click),
                        controlName: nameof(Control_Theme));
                    return;
                }

                // Save theme name
                string? selectedTheme = Control_Themes_ComboBox_Theme.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(selectedTheme))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a theme.", "Theme",
                        callerName: nameof(SaveButton_Click),
                        controlName: nameof(Control_Theme));
                    return;
                }

                // FIXED: Use the proper theme setter that works with existing database structure
                var saveResult = await Dao_User.SetThemeNameAsync(user, selectedTheme);

                if (!saveResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception($"Failed to save theme: {saveResult.ErrorMessage}"),
                        contextData: new Dictionary<string, object>
                        {
                            ["User"] = user,
                            ["SelectedTheme"] = selectedTheme
                        },
                        callerName: nameof(SaveButton_Click),
                        controlName: nameof(Control_Theme)
                    );

                    StatusMessageChanged?.Invoke(this, $"Error saving theme: {saveResult.ErrorMessage}");
                    return;
                }

                // Update the application variables and apply changes
                Model_Application_Variables.ThemeEnabled = true; // Always enabled
                if (!string.IsNullOrEmpty(selectedTheme))
                {
                    Model_Application_Variables.ThemeName = selectedTheme;
                }

                // Use new theme system: ThemeManager will notify all ThemedForm subscribers
                var themeProvider = Program.ServiceProvider?.GetService<IThemeProvider>();
                if (themeProvider != null)
                {
                    if (!string.IsNullOrEmpty(selectedTheme))
                    {
                        // Apply theme
                        await themeProvider.SetThemeAsync(selectedTheme, ThemeChangeReason.UserSelection, user);

                    }
                }
                else if (themeProvider == null)
                {

                }

                ThemeChanged?.Invoke(this, EventArgs.Empty);
                StatusMessageChanged?.Invoke(this, "Theme saved and applied successfully!");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["SelectedTheme"] = Control_Themes_ComboBox_Theme.SelectedItem?.ToString() ?? "null"
                    },
                    callerName: nameof(SaveButton_Click),
                    controlName: nameof(Control_Theme));
            }
            finally
            {
                Control_Themes_Button_Save.Enabled = true;
            }
        }

        private async void PreviewButton_Click(object? sender, EventArgs e)
        {
            try
            {
                string? selectedTheme = Control_Themes_ComboBox_Theme.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(selectedTheme))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a theme to preview.", "Theme",
                        callerName: nameof(PreviewButton_Click),
                        controlName: nameof(Control_Theme));
                    return;
                }

                string? originalTheme = Model_Application_Variables.ThemeName;
                Model_Application_Variables.ThemeName = selectedTheme;

                // Use new theme system: ThemeManager will notify all ThemedForm subscribers
                var themeProvider = Program.ServiceProvider?.GetService<IThemeProvider>();
                if (themeProvider != null)
                {
                    await themeProvider.SetThemeAsync(selectedTheme, ThemeChangeReason.Preview, Model_Application_Variables.User);

                }
                else
                {

                }

                Model_Application_Variables.ThemeName = originalTheme;
                StatusMessageChanged?.Invoke(this, $"Theme preview applied: {selectedTheme}");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["SelectedTheme"] = Control_Themes_ComboBox_Theme.SelectedItem?.ToString() ?? "null"
                    },
                    callerName: nameof(PreviewButton_Click),
                    controlName: nameof(Control_Theme));
            }
        }


    }
}
