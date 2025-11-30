using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Controls.SettingsForm;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Base class for theme appliers providing common functionality.
/// </summary>
public abstract class ThemeApplierBase : IThemeApplier
{
    /// <summary>
    /// Logger for theme application operations.
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// Initializes a new instance of the ThemeApplierBase class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    protected ThemeApplierBase(ILogger logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Determines whether this applier can handle the specified control type.
    /// </summary>
    public abstract bool CanApply(Control control);

    /// <summary>
    /// Applies the theme to the specified control.
    /// </summary>
    public abstract void Apply(Control control, Model_Shared_UserUiColors theme);

    /// <summary>
    /// Applies common styles to any control (BackColor, ForeColor, Font).
    /// </summary>
    /// <param name="control">The control to style.</param>
    /// <param name="theme">The theme to apply.</param>
    protected virtual void ApplyCommonStyles(Control control, Model_Shared_UserUiColors theme)
    {
        // Skip Control_QuickButton_Single - it manages its own colors
        if (control is Control_QuickButton_Single)
        {
            Logger.LogDebug("Skipping ApplyCommonStyles for Control_QuickButton_Single '{ControlName}'", control.Name);
            return;
        }
        
        // Skip Control_SettingsCategoryCard - it manages its own colors
        if (control is Control_SettingsCategoryCard)
        {
            Logger.LogDebug("Skipping ApplyCommonStyles for Control_SettingsCategoryCard '{ControlName}'", control.Name);
            return;
        }
        
        try
        {
            if (theme.ControlBackColor.HasValue)
            {
                control.BackColor = theme.ControlBackColor.Value;
            }

            if (theme.ControlForeColor.HasValue)
            {
                control.ForeColor = theme.ControlForeColor.Value;
            }

            // Apply font if available (from theme or default)
            if (control.Font != null)
            {
                // Preserve existing font family and size unless theme specifies otherwise
                // This is a fallback - specific appliers can override
            }
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }

    /// <summary>
    /// Handles errors during theme application.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <param name="control">The control being themed.</param>
    /// <param name="theme">The theme being applied.</param>
    protected virtual void HandleApplyError(Exception ex, Control control, Model_Shared_UserUiColors theme)
    {
        Logger.LogError(ex, "Theme application failed for {ControlType} '{ControlName}' using {ApplierType}", 
            control.GetType().Name, 
            control.Name ?? "<unnamed>", 
            GetType().Name);

        LoggingUtility.LogApplicationError(ex);

        // Use Service_ErrorHandler for critical errors only
        // Recoverable errors are logged silently per FR-012
        if (ex is OutOfMemoryException or StackOverflowException)
        {
            Service_ErrorHandler.HandleException(
                ex,
                Enum_ErrorSeverity.High,
                null, // retryAction
                new Dictionary<string, object>
                {
                    ["ControlType"] = control.GetType().Name,
                    ["ControlName"] = control.Name ?? "<unnamed>",
                    ["ApplierType"] = GetType().Name
                },
                $"Critical error applying theme to {control.Name}");
        }
    }

    /// <summary>
    /// Safely gets a color value or returns a fallback.
    /// </summary>
    protected Color GetColorOrDefault(Color? themeColor, Color fallback)
    {
        return themeColor ?? fallback;
    }
}
