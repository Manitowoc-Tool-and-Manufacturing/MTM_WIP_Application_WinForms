using System;
using System.Collections.Generic;
using System.Linq;
using MTM_WIP_Application_Winforms.Controls.Shared;
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
    public static void ConfigureForQuantity(SuggestionTextBoxWithLabel control, bool showValidationColor = true)
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
    public static void ConfigureForWorkOrder(SuggestionTextBoxWithLabel control, bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.ConfigureForValidation("WorkOrder", showValidationColor);
    }

    /// <summary>
    /// Configures SuggestionTextBoxWithLabel for weight input with decimals.
    /// Validation: Decimal 0-99,999.99.
    /// </summary>
    /// <param name="control">The SuggestionTextBoxWithLabel control to configure</param>
    /// <param name="showValidationColor">Whether to show validation colors (default: true)</param>
    public static void ConfigureForWeight(SuggestionTextBoxWithLabel control, bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.ConfigureForValidation("Weight", showValidationColor);
    }

    /// <summary>
    /// Configures SuggestionTextBoxWithLabel for price input with decimals.
    /// Validation: Decimal 0-9,999,999.99.
    /// </summary>
    /// <param name="control">The SuggestionTextBoxWithLabel control to configure</param>
    /// <param name="showValidationColor">Whether to show validation colors (default: true)</param>
    public static void ConfigureForPrice(SuggestionTextBoxWithLabel control, bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.ConfigureForValidation("Price", showValidationColor);
    }

    /// <summary>
    /// Configures SuggestionTextBoxWithLabel with a custom numeric validator.
    /// </summary>
    /// <param name="control">The SuggestionTextBoxWithLabel control to configure</param>
    /// <param name="min">Minimum allowed value (inclusive)</param>
    /// <param name="max">Maximum allowed value (inclusive)</param>
    /// <param name="allowDecimals">Whether decimal values are allowed (default: false)</param>
    /// <param name="fieldName">Field name for error messages (default: "Value")</param>
    /// <param name="showValidationColor">Whether to show validation colors (default: true)</param>
    public static void ConfigureForNumeric(
        SuggestionTextBoxWithLabel control,
        decimal min,
        decimal max,
        bool allowDecimals = false,
        string fieldName = "Value",
        bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        var validator = new Services.Validators.NumericValidator(min, max, allowDecimals, fieldName);
        control.ConfigureForValidation(validator, showValidationColor);
    }

    /// <summary>
    /// Configures SuggestionTextBoxWithLabel with a custom validator.
    /// </summary>
    /// <param name="control">The SuggestionTextBoxWithLabel control to configure</param>
    /// <param name="validator">Custom validator instance</param>
    /// <param name="showValidationColor">Whether to show validation colors (default: true)</param>
    public static void ConfigureForCustomValidation(
        SuggestionTextBoxWithLabel control,
        IValidator validator,
        bool showValidationColor = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        if (validator == null)
        {
            throw new ArgumentNullException(nameof(validator));
        }

        control.ConfigureForValidation(validator, showValidationColor);
    }

    #endregion

    #region Bulk Configuration

    /// <summary>
    /// Configures multiple SuggestionTextBoxWithLabel controls for quantity validation.
    /// </summary>
    /// <param name="showValidationColor">Whether to show validation colors</param>
    /// <param name="controls">Controls to configure</param>
    public static void ConfigureMultipleForQuantity(bool showValidationColor, params SuggestionTextBoxWithLabel[] controls)
    {
        foreach (var control in controls)
        {
            ConfigureForQuantity(control, showValidationColor);
        }
    }

    /// <summary>
    /// Configures multiple SuggestionTextBoxWithLabel controls with specific validator types.
    /// </summary>
    /// <param name="configurations">Tuples of (control, validatorType, showValidationColor)</param>
    public static void ConfigureMultiple(params (SuggestionTextBoxWithLabel control, string validatorType, bool showValidationColor)[] configurations)
    {
        foreach (var (control, validatorType, showValidationColor) in configurations)
        {
            control.ConfigureForValidation(validatorType, showValidationColor);
        }
    }

    #endregion

    #region Validation Methods

    /// <summary>
    /// Validates a SuggestionTextBoxWithLabel control and optionally shows error message.
    /// </summary>
    /// <param name="control">The control to validate</param>
    /// <param name="fieldName">Field name for error message</param>
    /// <param name="showError">Whether to show error dialog (default: true)</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool ValidateControl(SuggestionTextBoxWithLabel control, string fieldName = "", bool showError = true)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        var result = control.Validate();

        if (!result.IsValid && showError)
        {
            Service_ErrorHandler.HandleValidationError(
                result.ErrorMessage,
                string.IsNullOrEmpty(fieldName) ? result.FieldName : fieldName,
                controlName: control.Name);
        }

        return result.IsValid;
    }

    /// <summary>
    /// Validates multiple SuggestionTextBoxWithLabel controls.
    /// </summary>
    /// <param name="showError">Whether to show error dialog for first invalid control</param>
    /// <param name="controls">Controls to validate</param>
    /// <returns>True if all valid, false if any invalid</returns>
    public static bool ValidateMultiple(bool showError, params SuggestionTextBoxWithLabel[] controls)
    {
        foreach (var control in controls)
        {
            if (!ValidateControl(control, showError: showError))
            {
                control.Focus();
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Gets validation results for multiple controls without showing errors.
    /// </summary>
    /// <param name="controls">Controls to validate</param>
    /// <returns>Dictionary of control name to validation result</returns>
    public static Dictionary<string, Model_Validation_Result> GetValidationResults(params SuggestionTextBoxWithLabel[] controls)
    {
        var results = new Dictionary<string, Model_Validation_Result>();

        foreach (var control in controls)
        {
            results[control.Name] = control.Validate();
        }

        return results;
    }

    #endregion

    #region Clear Methods

    /// <summary>
    /// Clears a SuggestionTextBoxWithLabel control.
    /// </summary>
    /// <param name="control">The control to clear</param>
    public static void Clear(SuggestionTextBoxWithLabel control)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.ClearTextBox();
    }

    /// <summary>
    /// Clears multiple SuggestionTextBoxWithLabel controls.
    /// </summary>
    /// <param name="controls">Controls to clear</param>
    public static void ClearMultiple(params SuggestionTextBoxWithLabel[] controls)
    {
        foreach (var control in controls)
        {
            Clear(control);
        }
    }

    #endregion

    #region Enable/Disable Methods

    /// <summary>
    /// Enables or disables a SuggestionTextBoxWithLabel control.
    /// </summary>
    /// <param name="control">The control to enable/disable</param>
    /// <param name="enabled">Whether to enable the control</param>
    public static void SetEnabled(SuggestionTextBoxWithLabel control, bool enabled)
    {
        if (control == null)
        {
            throw new ArgumentNullException(nameof(control));
        }

        control.Enabled = enabled;
    }

    /// <summary>
    /// Enables or disables multiple SuggestionTextBoxWithLabel controls.
    /// </summary>
    /// <param name="enabled">Whether to enable the controls</param>
    /// <param name="controls">Controls to enable/disable</param>
    public static void SetEnabledMultiple(bool enabled, params SuggestionTextBoxWithLabel[] controls)
    {
        foreach (var control in controls)
        {
            SetEnabled(control, enabled);
        }
    }

    #endregion
}
