using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Applies themes to TextBox controls.
/// </summary>
public class TextBoxThemeApplier : ThemeApplierBase
{
    public TextBoxThemeApplier(ILogger<TextBoxThemeApplier> logger) : base(logger)
    {
    }

    public override bool CanApply(Control control)
    {
        return control is TextBox;
    }

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not TextBox textBox)
        {
            return;
        }

        try
        {
            textBox.BackColor = GetColorOrDefault(theme.TextBoxBackColor, Color.White);
            textBox.ForeColor = GetColorOrDefault(theme.TextBoxForeColor, Color.Black);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            
            Logger.LogDebug("Applied theme to TextBox '{ControlName}'", textBox.Name);
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }
}
