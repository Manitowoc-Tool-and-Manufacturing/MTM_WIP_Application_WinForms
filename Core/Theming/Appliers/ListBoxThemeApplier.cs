using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class ListBoxThemeApplier : ThemeApplierBase
{
    public ListBoxThemeApplier(ILogger<ListBoxThemeApplier> logger) : base(logger) { }

    public override bool CanApply(Control control) => control is ListBox;

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not ListBox listBox) return;
        try
        {
            listBox.BackColor = GetColorOrDefault(theme.ListBoxBackColor, Color.White);
            listBox.ForeColor = GetColorOrDefault(theme.ListBoxForeColor, Color.Black);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
