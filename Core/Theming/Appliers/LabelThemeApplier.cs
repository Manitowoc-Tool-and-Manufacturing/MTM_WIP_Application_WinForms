using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Controls.SettingsForm;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Applies themes to Label controls.
/// </summary>
public class LabelThemeApplier : ThemeApplierBase
{
    public LabelThemeApplier(ILogger<LabelThemeApplier> logger) : base(logger)
    {
    }

    public override bool CanApply(Control control)
    {
        return control is Label;
    }

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not Label label)
        {
            return;
        }

        // Skip labels inside Control_QuickButton_Single or Control_SettingsCategoryCard - they manage their own colors
        Control? parent = label.Parent;
        while (parent != null)
        {
            if (parent is Control_QuickButton_Single)
            {
                Logger.LogDebug("Skipping Label '{ControlName}' inside Control_QuickButton_Single", label.Name);
                return;
            }
            if (parent is Control_SettingsCategoryCard)
            {
                Logger.LogDebug("Skipping Label '{ControlName}' inside Control_SettingsCategoryCard", label.Name);
                return;
            }
            parent = parent.Parent;
        }

        try
        {
            label.BackColor = GetColorOrDefault(theme.LabelBackColor, Color.Transparent);
            label.ForeColor = GetColorOrDefault(theme.LabelForeColor, Color.Black);
            
            Logger.LogDebug("Applied theme to Label '{ControlName}'", label.Name);
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }
}
