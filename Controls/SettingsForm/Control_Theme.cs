using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
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
            
            // Initialize new checkbox programmatically since we can't edit designer
            Control_Themes_CheckBox_ShowTotalSummaryPanel = new CheckBox();
            Control_Themes_CheckBox_ShowTotalSummaryPanel.AutoSize = true;
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Location = new Point(Control_Themes_CheckBox_AutoExpandPanels.Location.X, Control_Themes_CheckBox_AutoExpandPanels.Location.Y + 30);
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Name = "Control_Themes_CheckBox_ShowTotalSummaryPanel";
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Size = new Size(180, 24);
            Control_Themes_CheckBox_ShowTotalSummaryPanel.TabIndex = 10;
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Text = "Show Total Summary Panel";
            Control_Themes_CheckBox_ShowTotalSummaryPanel.UseVisualStyleBackColor = true;
            
            // Add to the same parent as other checkboxes
            if (Control_Themes_CheckBox_AutoExpandPanels.Parent != null)
            {
                Control_Themes_CheckBox_AutoExpandPanels.Parent.Controls.Add(Control_Themes_CheckBox_ShowTotalSummaryPanel);
            }

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
                var animationsResult = await Dao_User.GetAnimationsEnabledAsync(user);
                var autoExpandResult = await Dao_User.GetAutoExpandPanelsAsync(user);
                var showTotalSummaryResult = await Dao_User.GetShowTotalSummaryPanelAsync(user);

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

                    Control_Themes_CheckBox_EnableAnimations.Checked = animationsResult.IsSuccess ? animationsResult.Data : true;
                    Control_Themes_CheckBox_AutoExpandPanels.Checked = autoExpandResult.IsSuccess ? autoExpandResult.Data : true;
                    if (Control_Themes_CheckBox_ShowTotalSummaryPanel != null)
                    {
                        Control_Themes_CheckBox_ShowTotalSummaryPanel.Checked = showTotalSummaryResult.IsSuccess ? showTotalSummaryResult.Data : false;
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

                // Save settings
                var saveResult = await Dao_User.SetThemeNameAsync(user, selectedTheme);
                var animResult = await Dao_User.SetAnimationsEnabledAsync(user, Control_Themes_CheckBox_EnableAnimations.Checked);
                var autoExpandResult = await Dao_User.SetAutoExpandPanelsAsync(user, Control_Themes_CheckBox_AutoExpandPanels.Checked);
                var showTotalSummaryResult = await Dao_User.SetShowTotalSummaryPanelAsync(user, Control_Themes_CheckBox_ShowTotalSummaryPanel?.Checked ?? false);

                if (!saveResult.IsSuccess || !animResult.IsSuccess || !autoExpandResult.IsSuccess || !showTotalSummaryResult.IsSuccess)
                {
                    string errorMsg = "Failed to save some settings:\n";
                    if (!saveResult.IsSuccess) errorMsg += $"- Theme: {saveResult.ErrorMessage}\n";
                    if (!animResult.IsSuccess) errorMsg += $"- Animations: {animResult.ErrorMessage}\n";
                    if (!autoExpandResult.IsSuccess) errorMsg += $"- Auto Expand: {autoExpandResult.ErrorMessage}\n";
                    if (!showTotalSummaryResult.IsSuccess) errorMsg += $"- Show Total Summary: {showTotalSummaryResult.ErrorMessage}\n";
                    
                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception(errorMsg),
                        contextData: new Dictionary<string, object> { ["User"] = user },
                        callerName: nameof(SaveButton_Click),
                        controlName: nameof(Control_Theme));
                        
                    StatusMessageChanged?.Invoke(this, "Error saving settings.");
                }
                else
                {
                    // Update global variables
                    Model_Application_Variables.ThemeName = selectedTheme;
                    Model_Application_Variables.AnimationsEnabled = Control_Themes_CheckBox_EnableAnimations.Checked;
                    Model_Application_Variables.AutoExpandPanels = Control_Themes_CheckBox_AutoExpandPanels.Checked;
                    Model_Application_Variables.ShowTotalSummaryPanel = Control_Themes_CheckBox_ShowTotalSummaryPanel?.Checked ?? false;

                    StatusMessageChanged?.Invoke(this, "Settings saved successfully.");
                }
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

        private CheckBox? Control_Themes_CheckBox_ShowTotalSummaryPanel;
    }
}
