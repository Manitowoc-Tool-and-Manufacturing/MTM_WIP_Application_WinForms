using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Applies themes to Panel controls.
/// </summary>
public class PanelThemeApplier : ThemeApplierBase
{
    public PanelThemeApplier(ILogger<PanelThemeApplier> logger) : base(logger)
    {
    }

    public override bool CanApply(Control control)
    {
        return control is Panel;
    }

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not Panel panel)
        {
            return;
        }

        try
        {
            panel.BackColor = GetColorOrDefault(theme.PanelBackColor, Color.White);
            panel.BorderStyle = BorderStyle.None;
            
            Logger.LogDebug("Applied theme to Panel '{ControlName}'", panel.Name);
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }
}
