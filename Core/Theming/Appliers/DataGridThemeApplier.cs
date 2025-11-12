using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Applies themes to DataGridView controls.
/// </summary>
public class DataGridThemeApplier : ThemeApplierBase
{
    public DataGridThemeApplier(ILogger<DataGridThemeApplier> logger) : base(logger)
    {
    }

    public override bool CanApply(Control control)
    {
        return control is DataGridView;
    }

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        if (control is not DataGridView grid)
        {
            return;
        }

        try
        {
            grid.BackgroundColor = GetColorOrDefault(theme.DataGridBackColor, Color.White);
            grid.ForeColor = GetColorOrDefault(theme.DataGridForeColor, Color.Black);
            grid.GridColor = GetColorOrDefault(theme.DataGridGridColor, Color.LightGray);
            
            if (theme.DataGridHeaderBackColor.HasValue)
            {
                grid.ColumnHeadersDefaultCellStyle.BackColor = theme.DataGridHeaderBackColor.Value;
            }
            
            if (theme.DataGridHeaderForeColor.HasValue)
            {
                grid.ColumnHeadersDefaultCellStyle.ForeColor = theme.DataGridHeaderForeColor.Value;
            }
            
            if (theme.DataGridSelectionBackColor.HasValue)
            {
                grid.DefaultCellStyle.SelectionBackColor = theme.DataGridSelectionBackColor.Value;
            }
            
            if (theme.DataGridSelectionForeColor.HasValue)
            {
                grid.DefaultCellStyle.SelectionForeColor = theme.DataGridSelectionForeColor.Value;
            }

            Logger.LogDebug("Applied theme to DataGridView '{ControlName}'", grid.Name);
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }
}
