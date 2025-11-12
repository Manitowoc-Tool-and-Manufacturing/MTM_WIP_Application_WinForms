using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class RadioButtonThemeApplier : ThemeApplierBase
{
    public RadioButtonThemeApplier(ILogger<RadioButtonThemeApplier> logger) : base(logger) { }

    public override bool CanApply(Control control) => control is RadioButton;

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not RadioButton radioButton) return;
        try
        {
            radioButton.BackColor = GetColorOrDefault(theme.RadioButtonBackColor, Color.Transparent);
            radioButton.ForeColor = GetColorOrDefault(theme.RadioButtonForeColor, Color.Black);
            radioButton.FlatStyle = FlatStyle.Flat;
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
