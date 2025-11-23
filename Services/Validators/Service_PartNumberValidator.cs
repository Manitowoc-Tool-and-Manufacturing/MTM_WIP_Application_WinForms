using System.Text.RegularExpressions;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Validators;

/// <summary>
/// Validates part number format and business rules.
/// Expected format: R-{ALPHANUMERIC}-{2 DIGITS}
/// Example: R-ABC123-01, R-XYZ-99
/// </summary>
public class Service_PartNumberValidator : IValidator
{
    #region Fields

    private static readonly Regex Pattern = new(@"^R-[A-Z0-9]+-\d{2}$", RegexOptions.Compiled);
    private const int MinLength = 6;  // Minimum: R-X-00
    private const int MaxLength = 50; // Reasonable maximum for part numbers

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates part number format and business rules.
    /// </summary>
    /// <param name="input">The part number to validate</param>
    /// <returns>Validation result indicating success or failure with specific error message</returns>
    public Model_Validation_Result Validate(string input)
    {
        // Check for null or empty
        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure(
                "Part number is required.",
                "PartNumber");
        }

        // Trim whitespace for validation
        var trimmed = input.Trim();

        // Check minimum length
        if (trimmed.Length < MinLength)
        {
            return Model_Validation_Result.Failure(
                $"Part number must be at least {MinLength} characters.",
                "PartNumber");
        }

        // Check maximum length
        if (trimmed.Length > MaxLength)
        {
            return Model_Validation_Result.Failure(
                $"Part number cannot exceed {MaxLength} characters.",
                "PartNumber");
        }

        // Validate format with regex
        if (!Pattern.IsMatch(trimmed))
        {
            return Model_Validation_Result.Failure(
                "Part number must follow format: R-{ALPHANUMERIC}-{2 DIGITS} (e.g., R-ABC123-01)",
                "PartNumber");
        }

        // All validations passed
        return Model_Validation_Result.Success("PartNumber");
    }

    #endregion
}
