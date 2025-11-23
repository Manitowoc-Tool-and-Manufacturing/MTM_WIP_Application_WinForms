using System.Text.RegularExpressions;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Validators;

/// <summary>
/// Validates location code format and business rules.
/// Location codes are typically alphanumeric with optional hyphens.
/// </summary>
public class LocationCodeValidator : IValidator
{
    #region Fields

    private static readonly Regex Pattern = new(@"^[A-Z0-9]{1,3}-[A-Z0-9]{1,4}$", RegexOptions.Compiled);
    private const int MinLength = 3;  // Minimum: A-1
    private const int MaxLength = 8;  // Maximum: ABC-DEFG

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates location code format and business rules.
    /// </summary>
    /// <param name="input">The location code to validate</param>
    /// <returns>Validation result indicating success or failure with specific error message</returns>
    public Model_Validation_Result Validate(string input)
    {
        // Check for null or empty
        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure(
                "Location code is required.",
                "LocationCode");
        }

        // Trim and convert to uppercase for validation
        var trimmed = input.Trim().ToUpperInvariant();

        // Check minimum length
        if (trimmed.Length < MinLength)
        {
            return Model_Validation_Result.Failure(
                $"Location code must be at least {MinLength} characters.",
                "LocationCode");
        }

        // Check maximum length
        if (trimmed.Length > MaxLength)
        {
            return Model_Validation_Result.Failure(
                $"Location code cannot exceed {MaxLength} characters.",
                "LocationCode");
        }

        // Validate format with regex (e.g., A-1, AB-12, ABC-DEFG)
        if (!Pattern.IsMatch(trimmed))
        {
            return Model_Validation_Result.Failure(
                "Location code must follow format: {1-3 ALPHANUMERIC}-{1-4 ALPHANUMERIC} (e.g., A-1, AB-12)",
                "LocationCode");
        }

        // All validations passed
        return Model_Validation_Result.Success("LocationCode");
    }

    #endregion
}
