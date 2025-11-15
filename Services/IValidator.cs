using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Interface for input validators.
/// Implement this interface to create custom validators that can be registered with Service_Validation.
/// </summary>
public interface IValidator
{
    /// <summary>
    /// Validates the input string.
    /// </summary>
    /// <param name="input">The input string to validate</param>
    /// <returns>Validation result indicating success or failure with error message</returns>
    Model_Validation_Result Validate(string input);
}
