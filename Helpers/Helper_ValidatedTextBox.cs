using MTM_WIP_Application_Winforms.Components.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Helper class for configuring SuggestionTextBoxWithLabel in validated mode (non-suggestion).
/// Simplifies setup for common validation scenarios like quantity, weight, price, etc.
/// </summary>
public static class Helper_ValidatedTextBox
{
    #region Configuration Methods

    /// <summary>
    /// Configures SuggestionTextBoxWithLabel for positive integer quantity input.
    /// Validation: Must be integer > 0, max 999,999.
    /// </summary>
    /// <param name="control">The SuggestionTextBoxWithLabel control to configure</param>
    /// <param name="showValidationColor">Whether to show validation colors (default: true)</param>
    public static void ConfigureForQuantity(Component_SuggestionTextBoxWithLabel control, bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.ConfigureForValidation("Quantity", showValidationColor);
    }

    /// <summary>
    /// Configures SuggestionTextBoxWithLabel for work order input.
    /// Applies WO formatting validator (registered as "WorkOrder").
    /// </summary>
    /// <param name="control">The control to configure.</param>
    /// <param name="showValidationColor">Whether to show validation colors (default: true).</param>
    public static void ConfigureForWorkOrder(Component_SuggestionTextBoxWithLabel control, bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.ConfigureForValidation("WorkOrder", showValidationColor);
    }

    #endregion

}
