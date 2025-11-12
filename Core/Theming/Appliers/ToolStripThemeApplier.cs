using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class ToolStripThemeApplier : ThemeApplierBase
{
    public ToolStripThemeApplier(ILogger<ToolStripThemeApplier> logger) : base(logger) { }
    public override bool CanApply(Control control) => control is ToolStrip;
    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not ToolStrip toolStrip) return;
        try
        {
            toolStrip.BackColor = GetColorOrDefault(theme.ToolStripBackColor, Color.FromArgb(45, 45, 48));
            toolStrip.ForeColor = GetColorOrDefault(theme.ToolStripForeColor, Color.White);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
