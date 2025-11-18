
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core;
public static class Core_AppThemes
{
    #region Theme Definition

    public class AppTheme
    {
        public Model_Shared_UserUiColors Colors { get; set; } = new();
        public Font? FormFont { get; set; }
    }

    #endregion

    #region Theme Registry

    private static Dictionary<string, AppTheme> Themes = new();

    private static async Task<string?> LoadAndSetUserThemeNameAsync(string userId)
    {
        try
        {
            // Get the user's saved theme preference
            var themeNameResult = await Dao_User.GetThemeNameAsync(userId);
            string? themeName = themeNameResult.IsSuccess ? themeNameResult.Data : null;

            // If no theme preference is saved, or it's null, set to "Default"
            if (string.IsNullOrWhiteSpace(themeName))
            {
                themeName = "Default";
            }

            // Set the theme name in Model_Application_Variables
            Model_Application_Variables.ThemeName = themeName;

            return themeName;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // On error, default to "Default" theme
            Model_Application_Variables.ThemeName = "Default";

            return "Default";
        }
    }

    public static async Task LoadThemesFromDatabaseAsync()
    {
        try
        {
            Dictionary<string, AppTheme> themes = new();

            try
            {

                // UPDATED: Use Dao_System.GetAllThemesAsync instead of non-existent stored procedure
                var dataResult = await Dao_System.GetAllThemesAsync();

                if (dataResult.IsSuccess && dataResult.Data != null)
                {
                    DataTable dt = dataResult.Data;

                    foreach (DataRow row in dt.Rows)
                    {
                        string? themeName = row["ThemeName"]?.ToString();
                        string? settingsJson = row["SettingsJson"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(themeName) && !string.IsNullOrWhiteSpace(settingsJson))
                        {
                            try
                            {

                                JsonSerializerOptions options = new()
                                {
                                    AllowTrailingCommas = true,
                                    ReadCommentHandling = System.Text.Json.JsonCommentHandling.Skip,
                                    PropertyNameCaseInsensitive = false
                                };
                                options.Converters.Add(new JsonColorConverter());

                                // Directly deserialize the complete Model_Shared_UserUiColors from database
                                // The database should contain the full JSON with all color properties
                                Model_Shared_UserUiColors? colors = JsonSerializer.Deserialize<Model_Shared_UserUiColors>(settingsJson, options);

                                if (colors != null)
                                {
                                    themes[themeName] = new AppTheme { Colors = colors, FormFont = null };
                                }
                                else
                                {

                                }
                            }
                            catch (JsonException jsonEx)
                            {
                                LoggingUtility.LogApplicationError(jsonEx);


                            }
                            catch (Exception ex)
                            {
                                LoggingUtility.LogApplicationError(ex);

                            }
                        }
                    }
                }
                else
                {

                    themes = CreateDefaultThemes();
                }
            }
            catch (Exception dbEx)
            {

                LoggingUtility.LogApplicationError(dbEx);
                themes = CreateDefaultThemes();
            }

            // Ensure we always have at least default themes
            if (themes.Count == 0)
            {

                themes = CreateDefaultThemes();
            }

            // Log which themes were loaded
            string themeList = string.Join(", ", themes.Keys);


            Themes = themes;

        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Themes = CreateDefaultThemes();
        }
    }

    #endregion

    #region Theme Accessors

    public static IEnumerable<string> GetThemeNames()
    {
        try
        {
            Debug.Assert(Themes != null, "Themes dictionary is not initialized.");
            return Themes.Keys;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            throw;
        }
    }

    public static AppTheme GetCurrentTheme()
    {
        try
        {
            string themeName = Model_Application_Variables.ThemeName ?? "Default";
            if (Themes.TryGetValue(themeName, out AppTheme? theme))
            {
                return theme;
            }

            Debug.Assert(Themes != null, "Themes dictionary is not initialized.");
            return Themes.ContainsKey("Default") ? Themes["Default"] : new AppTheme();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            throw;
        }
    }

    public static AppTheme GetTheme(string themeName)
    {
        try
        {
            if (Themes.TryGetValue(themeName, out AppTheme? theme))
            {
                return theme;
            }

            Debug.Assert(Themes != null, "Themes dictionary is not initialized.");
            return Themes.ContainsKey("Default") ? Themes["Default"] : new AppTheme();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            throw;
        }
    }


    #endregion

    #region Theme Startup Sequence

    public static async Task InitializeThemeSystemAsync(string userId)
    {
        try
        {
            // First load all available themes from database
            await LoadThemesFromDatabaseAsync();

            // Load theme enabled/disabled setting
            var themeEnabledResult = await Dao_User.GetThemeEnabledAsync(userId);
            if (themeEnabledResult.IsSuccess)
            {
                Model_Application_Variables.ThemeEnabled = themeEnabledResult.Data;
            }
            else
            {

                Model_Application_Variables.ThemeEnabled = true; // Default to enabled
            }

            // Then try to get the user's theme preference
            string? userThemeName = await LoadAndSetUserThemeNameAsync(userId);

            // Check if the user's preferred theme actually exists in our loaded themes
            if (!string.IsNullOrWhiteSpace(userThemeName) && !Themes.ContainsKey(userThemeName))
            {


                // Try to find a suitable fallback theme from available database themes
                string fallbackTheme = "Default";
                if (Themes.ContainsKey("Default"))
                {
                    fallbackTheme = "Default";
                }
                else if (Themes.ContainsKey("Dark"))
                {
                    fallbackTheme = "Dark";
                }
                else if (Themes.ContainsKey("BLUE"))
                {
                    fallbackTheme = "BLUE";
                }
                else if (Themes.Count > 0)
                {
                    fallbackTheme = Themes.Keys.First();
                }


                Model_Application_Variables.ThemeName = fallbackTheme;
            }

            // Apply font settings to all themes
            foreach (AppTheme theme in Themes.Values)
            {
                if (theme.FormFont == null)
                {
                    theme.FormFont = new Font("Segoe UI Emoji", Model_Application_Variables.ThemeFontSize);
                }
                else if (Math.Abs(theme.FormFont.Size - Model_Application_Variables.ThemeFontSize) > 0.01f)
                {
                    theme.FormFont = new Font(theme.FormFont.FontFamily, Model_Application_Variables.ThemeFontSize,
                        theme.FormFont.Style);
                }
            }

            string finalTheme = Model_Application_Variables.ThemeName ?? "Default";

        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // Ensure we have fallback themes even on error
            if (Themes.Count == 0)
            {
                Themes = CreateDefaultThemes();
            }
            Model_Application_Variables.ThemeName = Themes.Keys.FirstOrDefault() ?? "Default";

            throw;
        }
    }

    #endregion

    /// <summary>
    /// Creates a comprehensive set of default themes when database access fails
    /// </summary>
    private static Dictionary<string, AppTheme> CreateDefaultThemes()
    {
        var themes = new Dictionary<string, AppTheme>();

        // Default Light Theme
        var defaultColors = new Model_Shared_UserUiColors
        {
            FormBackColor = Color.White,
            FormForeColor = Color.Black,
            ControlBackColor = Color.White,
            ControlForeColor = Color.Black,
            ButtonBackColor = SystemColors.Control,
            ButtonForeColor = SystemColors.ControlText,
            ButtonHoverBackColor = SystemColors.ControlLight,
            ButtonPressedBackColor = SystemColors.ControlDark,
            TextBoxBackColor = Color.White,
            TextBoxForeColor = Color.Black,
            ComboBoxBackColor = Color.White,
            ComboBoxForeColor = Color.Black,
            ComboBoxErrorForeColor = Color.Red,
            DataGridBackColor = Color.White,
            DataGridForeColor = Color.Black,
            DataGridHeaderBackColor = SystemColors.Control,
            DataGridHeaderForeColor = SystemColors.ControlText,
            DataGridRowBackColor = Color.White,
            DataGridAltRowBackColor = Color.AliceBlue,
            DataGridSelectionBackColor = SystemColors.Highlight,
            DataGridSelectionForeColor = SystemColors.HighlightText
        };

        themes["Default"] = new AppTheme { Colors = defaultColors, FormFont = null };

        // Dark Theme
        var darkColors = new Model_Shared_UserUiColors
        {
            FormBackColor = Color.FromArgb(45, 45, 48),
            FormForeColor = Color.White,
            ControlBackColor = Color.FromArgb(45, 45, 48),
            ControlForeColor = Color.White,
            ButtonBackColor = Color.FromArgb(60, 60, 60),
            ButtonForeColor = Color.White,
            ButtonHoverBackColor = Color.FromArgb(80, 80, 80),
            ButtonPressedBackColor = Color.FromArgb(40, 40, 40),
            TextBoxBackColor = Color.FromArgb(30, 30, 30),
            TextBoxForeColor = Color.White,
            ComboBoxBackColor = Color.FromArgb(30, 30, 30),
            ComboBoxForeColor = Color.White,
            ComboBoxErrorForeColor = Color.FromArgb(255, 100, 100),
            DataGridBackColor = Color.FromArgb(45, 45, 48),
            DataGridForeColor = Color.White,
            DataGridHeaderBackColor = Color.FromArgb(60, 60, 60),
            DataGridHeaderForeColor = Color.White,
            DataGridRowBackColor = Color.FromArgb(45, 45, 48),
            DataGridAltRowBackColor = Color.FromArgb(55, 55, 58),
            DataGridSelectionBackColor = Color.FromArgb(51, 153, 255),
            DataGridSelectionForeColor = Color.White
        };

        themes["Dark"] = new AppTheme { Colors = darkColors, FormFont = null };

        // Blue Theme
        var blueColors = new Model_Shared_UserUiColors
        {
            FormBackColor = Color.FromArgb(240, 248, 255),
            FormForeColor = Color.FromArgb(25, 25, 25),
            ControlBackColor = Color.FromArgb(240, 248, 255),
            ControlForeColor = Color.FromArgb(25, 25, 25),
            ButtonBackColor = Color.FromArgb(70, 130, 180),
            ButtonForeColor = Color.White,
            ButtonHoverBackColor = Color.FromArgb(100, 149, 237),
            ButtonPressedBackColor = Color.FromArgb(30, 90, 140),
            TextBoxBackColor = Color.White,
            TextBoxForeColor = Color.Black,
            ComboBoxBackColor = Color.White,
            ComboBoxForeColor = Color.Black,
            ComboBoxErrorForeColor = Color.Red,
            DataGridBackColor = Color.White,
            DataGridForeColor = Color.Black,
            DataGridHeaderBackColor = Color.FromArgb(70, 130, 180),
            DataGridHeaderForeColor = Color.White,
            DataGridRowBackColor = Color.White,
            DataGridAltRowBackColor = Color.FromArgb(230, 240, 255),
            DataGridSelectionBackColor = Color.FromArgb(70, 130, 180),
            DataGridSelectionForeColor = Color.White
        };

        themes["BLUE"] = new AppTheme { Colors = blueColors, FormFont = null };


        return themes;
    }

}
