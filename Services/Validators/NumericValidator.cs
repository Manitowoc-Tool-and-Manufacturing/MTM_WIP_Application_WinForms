using System;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Validators;

/// <summary>
/// Validates numeric input with configurable range constraints.
/// Reusable validator that can be configured for different numeric fields.
/// </summary>
public class NumericValidator : IValidator
{
    #region Fields

    private readonly decimal _min;
    private readonly decimal _max;
    private readonly bool _allowDecimals;
    private readonly string _fieldName;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="NumericValidator"/> class.
    /// </summary>
    /// <param name="min">Minimum allowed value (inclusive)</param>
    /// <param name="max">Maximum allowed value (inclusive)</param>
    /// <param name="allowDecimals">Whether decimal values are allowed (default: false for integers only)</param>
    /// <param name="fieldName">Optional field name for error messages (default: "Value")</param>
    public NumericValidator(decimal min, decimal max, bool allowDecimals = false, string fieldName = "Value")
    {
        if (min > max)
        {
            throw new ArgumentException("Minimum value cannot be greater than maximum value.");
        }

        _min = min;
        _max = max;
        _allowDecimals = allowDecimals;
        _fieldName = fieldName;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates numeric input against configured constraints.
    /// </summary>
    /// <param name="input">The numeric string to validate</param>
    /// <returns>Validation result indicating success or failure with specific error message</returns>
    public Model_Validation_Result Validate(string input)
    {
        // Check for null or empty
        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure(
                $"{_fieldName} is required.",
                _fieldName);
        }

        // Trim whitespace
        var trimmed = input.Trim();

        // Try parsing as decimal
        if (!decimal.TryParse(trimmed, out var value))
        {
            return Model_Validation_Result.Failure(
                $"{_fieldName} must be a valid number.",
                _fieldName);
        }

        // Check if decimals are allowed
        if (!_allowDecimals && value != Math.Floor(value))
        {
            return Model_Validation_Result.Failure(
                $"{_fieldName} must be a whole number (no decimals).",
                _fieldName);
        }

        // Check minimum value
        if (value < _min)
        {
            return Model_Validation_Result.Failure(
                $"{_fieldName} must be at least {_min}.",
                _fieldName);
        }

        // Check maximum value
        if (value > _max)
        {
            return Model_Validation_Result.Failure(
                $"{_fieldName} cannot exceed {_max}.",
                _fieldName);
        }

        // All validations passed
        return Model_Validation_Result.Success(_fieldName);
    }

    #endregion
}
