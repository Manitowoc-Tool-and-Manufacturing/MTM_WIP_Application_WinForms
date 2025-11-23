using System.Text.RegularExpressions;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Validators;

/// <summary>
/// Validates work order format and business rules.
/// Work orders are typically numeric with optional prefix.
/// </summary>
public class Service_WorkOrderValidator : IValidator
{
    #region Fields

    private static readonly Regex Pattern = new(@"^WO-?\d{4,10}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private const long MinWorkOrderNumber = 0;
    private const long MaxWorkOrderNumber = 999999; // 6 digits max

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates work order format and business rules.
    /// </summary>
    /// <param name="input">The work order to validate</param>
    /// <returns>Validation result indicating success or failure with specific error message</returns>
    public Model_Validation_Result Validate(string input)
    {
        // Check for null or empty
        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure(
                "Work order is required.",
                "WorkOrder");
        }

        // Trim and convert to uppercase for validation
        var trimmed = input.Trim().ToUpperInvariant();

        // Validate format with regex (WO-1234 or WO1234 or 1234)
        if (!Pattern.IsMatch(trimmed) && !int.TryParse(trimmed, out _))
        {
            return Model_Validation_Result.Failure(
                "Work order must be in format WO-1234 or a numeric value.",
                "WorkOrder");
        }

        // Extract numeric portion
        var numericPart = trimmed.Replace("WO-", "").Replace("WO", "");
        
        if (!long.TryParse(numericPart, out var workOrderNumber))
        {
            return Model_Validation_Result.Failure(
                "Work order must contain a valid numeric value.",
                "WorkOrder");
        }

        // Validate range
        if (workOrderNumber < MinWorkOrderNumber)
        {
            return Model_Validation_Result.Failure(
                $"Work order number must be at least {MinWorkOrderNumber}.",
                "WorkOrder");
        }

        if (workOrderNumber > MaxWorkOrderNumber)
        {
            return Model_Validation_Result.Failure(
                $"Work order number cannot exceed {MaxWorkOrderNumber}.",
                "WorkOrder");
        }

        // All validations passed
        return Model_Validation_Result.Success("WorkOrder");
    }

    #endregion
}
