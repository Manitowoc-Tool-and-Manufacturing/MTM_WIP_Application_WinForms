using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Applies themes to Button controls.
/// </summary>
public class ButtonThemeApplier : ThemeApplierBase
{
    public ButtonThemeApplier(ILogger<ButtonThemeApplier> logger) : base(logger)
    {
    }

    public override bool CanApply(Control control)
    {
        return control is Button;
    }

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not Button button)
        {
            return;
        }

        try
        {
            button.BackColor = GetColorOrDefault(theme.ButtonBackColor, Color.FromArgb(225, 225, 225));
            button.ForeColor = GetColorOrDefault(theme.ButtonForeColor, Color.Black);
            button.FlatStyle = FlatStyle.Flat;
            
            Logger.LogDebug("Applied theme to Button '{ControlName}'", button.Name);
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }
}
