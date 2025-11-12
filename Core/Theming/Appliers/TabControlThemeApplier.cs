using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class TabControlThemeApplier : ThemeApplierBase
{
    public TabControlThemeApplier(ILogger<TabControlThemeApplier> logger) : base(logger) { }

    public override bool CanApply(Control control) => control is TabControl;

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not TabControl tabControl) return;
        try
        {
            tabControl.BackColor = GetColorOrDefault(theme.TabControlBackColor, Color.White);
            tabControl.ForeColor = GetColorOrDefault(theme.TabControlForeColor, Color.Black);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
