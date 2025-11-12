using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class TreeViewThemeApplier : ThemeApplierBase
{
    public TreeViewThemeApplier(ILogger<TreeViewThemeApplier> logger) : base(logger) { }
    public override bool CanApply(Control control) => control is TreeView;
    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not TreeView treeView) return;
        try
        {
            treeView.BackColor = GetColorOrDefault(theme.TreeViewBackColor, Color.White);
            treeView.ForeColor = GetColorOrDefault(theme.TreeViewForeColor, Color.Black);
            treeView.LineColor = GetColorOrDefault(theme.TreeViewLineColor, Color.Gray);
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
