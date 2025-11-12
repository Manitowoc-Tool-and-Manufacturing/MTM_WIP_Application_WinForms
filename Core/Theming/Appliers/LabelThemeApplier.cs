using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

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
