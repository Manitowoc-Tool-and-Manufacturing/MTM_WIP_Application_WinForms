using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Interfaces;

/// <summary>
/// Defines the strategy for applying themes to specific control types.
/// Implementations provide control-type-specific theme application logic.
/// </summary>
public interface IThemeApplier
{
    /// <summary>
    /// Determines whether this applier can handle the specified control type.
    /// </summary>
    /// <param name="control">The control to check.</param>
    /// <returns>True if this applier can apply themes to the control; otherwise, false.</returns>
    bool CanApply(Control control);

    /// <summary>
    /// Applies the theme to the specified control.
    /// </summary>
    /// <param name="control">The control to theme.</param>
    /// <param name="theme">The theme to apply.</param>
    void Apply(Control control, Model_Shared_UserUiColors theme);
}
