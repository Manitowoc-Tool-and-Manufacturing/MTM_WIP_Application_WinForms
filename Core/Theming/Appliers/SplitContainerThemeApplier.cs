using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class SplitContainerThemeApplier : ThemeApplierBase
{
    public SplitContainerThemeApplier(ILogger<SplitContainerThemeApplier> logger) : base(logger) { }
    public override bool CanApply(Control control) => control is SplitContainer;
    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not SplitContainer splitContainer) return;
        try
        {
            splitContainer.BackColor = GetColorOrDefault(theme.SplitContainerBackColor, Color.Gray);
            splitContainer.Panel1.BackColor = GetColorOrDefault(theme.ControlBackColor, Color.White);
            splitContainer.Panel2.BackColor = GetColorOrDefault(theme.ControlBackColor, Color.White);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
