using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Theme : UserControl
    {
        public event EventHandler? ThemeChanged;
        public event EventHandler<string>? StatusMessageChanged;

        public Control_Theme()
        {
            InitializeComponent();
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            Control_Themes_Button_Save.Click += SaveButton_Click;
            Control_Themes_Button_Preview.Click += PreviewButton_Click;
            LoadThemeSettingsAsync();
        }

        public async void LoadThemeSettingsAsync()
        {
            try
            {
                Control_Themes_ComboBox_Theme.Items.Clear();
                string[] themeNames = Core_Themes.Core_AppThemes.GetThemeNames().ToArray();
                Control_Themes_ComboBox_Theme.Items.AddRange(themeNames);

                string user = Model_AppVariables.User;
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
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["User"] = Model_AppVariables.User },
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
                string? selectedTheme = Control_Themes_ComboBox_Theme.SelectedItem?.ToString();
                if (string.IsNullOrWhiteSpace(selectedTheme))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a theme.", "Theme",
                        callerName: nameof(SaveButton_Click),
                        controlName: nameof(Control_Theme));
                    return;
                }

                string user = Model_AppVariables.User;

                // FIXED: Use the proper theme setter that works with existing database structure
                var saveResult = await Dao_User.SetThemeNameAsync(user, selectedTheme);

                if (saveResult.IsSuccess)
                {
                    // Update the current theme in the app variables and apply to all open forms
                    Model_AppVariables.ThemeName = selectedTheme;
                    foreach (Form openForm in Application.OpenForms)
                    {
                        Core_Themes.ApplyTheme(openForm);
                    }

                    ThemeChanged?.Invoke(this, EventArgs.Empty);
                    StatusMessageChanged?.Invoke(this, "Theme saved and applied successfully!");
                }
                else
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
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_AppVariables.User,
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

        private void PreviewButton_Click(object? sender, EventArgs e)
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

                string? originalTheme = Model_AppVariables.ThemeName;
                Model_AppVariables.ThemeName = selectedTheme;
                foreach (Form openForm in Application.OpenForms)
                {
                    Core_Themes.ApplyTheme(openForm);
                }
                Model_AppVariables.ThemeName = originalTheme;
                StatusMessageChanged?.Invoke(this, $"Theme preview applied: {selectedTheme}");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_AppVariables.User,
                        ["SelectedTheme"] = Control_Themes_ComboBox_Theme.SelectedItem?.ToString() ?? "null"
                    },
                    callerName: nameof(PreviewButton_Click),
                    controlName: nameof(Control_Theme));
            }
        }


    }
}
