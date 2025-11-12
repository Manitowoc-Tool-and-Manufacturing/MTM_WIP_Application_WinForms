using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

public class ComboBoxThemeApplier : ThemeApplierBase
{
    public ComboBoxThemeApplier(ILogger<ComboBoxThemeApplier> logger) : base(logger) { }

    public override bool CanApply(Control control) => control is ComboBox;

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not ComboBox comboBox) return;
        try
        {
            comboBox.BackColor = GetColorOrDefault(theme.ComboBoxBackColor, Color.White);
            comboBox.ForeColor = GetColorOrDefault(theme.ComboBoxForeColor, Color.Black);
            comboBox.FlatStyle = FlatStyle.Standard; // Use Standard for visible borders like TextBox FixedSingle
        }
        catch (Exception ex) { HandleApplyError(ex, control, theme); }
    }
}
