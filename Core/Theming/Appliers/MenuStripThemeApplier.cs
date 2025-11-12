using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class MenuStripThemeApplier : ThemeApplierBase
{
    public MenuStripThemeApplier(ILogger<MenuStripThemeApplier> logger) : base(logger) { }
    public override bool CanApply(Control control) => control is MenuStrip;
    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not MenuStrip menuStrip) return;
        try
        {
            menuStrip.BackColor = GetColorOrDefault(theme.MenuStripBackColor, Color.FromArgb(45, 45, 48));
            menuStrip.ForeColor = GetColorOrDefault(theme.MenuStripForeColor, Color.White);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
