using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class CheckBoxThemeApplier : ThemeApplierBase
{
    public CheckBoxThemeApplier(ILogger<CheckBoxThemeApplier> logger) : base(logger) { }

    public override bool CanApply(Control control) => control is CheckBox;

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not CheckBox checkBox) return;
        try
        {
            checkBox.BackColor = GetColorOrDefault(theme.CheckBoxBackColor, Color.Transparent);
            checkBox.ForeColor = GetColorOrDefault(theme.CheckBoxForeColor, Color.Black);
            checkBox.FlatStyle = FlatStyle.Flat;
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
