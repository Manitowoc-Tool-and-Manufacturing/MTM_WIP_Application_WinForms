using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class StatusStripThemeApplier : ThemeApplierBase
{
    public StatusStripThemeApplier(ILogger<StatusStripThemeApplier> logger) : base(logger) { }
    public override bool CanApply(Control control) => control is StatusStrip;
    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not StatusStrip statusStrip) return;
        try
        {
            statusStrip.BackColor = GetColorOrDefault(theme.StatusStripBackColor, Color.FromArgb(0, 122, 204));
            statusStrip.ForeColor = GetColorOrDefault(theme.StatusStripForeColor, Color.White);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
