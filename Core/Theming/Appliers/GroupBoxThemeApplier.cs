using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class GroupBoxThemeApplier : ThemeApplierBase
{
    public GroupBoxThemeApplier(ILogger<GroupBoxThemeApplier> logger) : base(logger) { }

    public override bool CanApply(Control control) => control is GroupBox;

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not GroupBox groupBox) return;
        try
        {
            groupBox.BackColor = GetColorOrDefault(theme.GroupBoxBackColor, Color.Transparent);
            groupBox.ForeColor = GetColorOrDefault(theme.GroupBoxForeColor, Color.Black);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
