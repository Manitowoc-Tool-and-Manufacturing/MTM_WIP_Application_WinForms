namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents the result of a validation operation.
/// </summary>
public class Model_Validation_Result
{
    #region Properties

    /// <summary>
    /// Gets or sets whether the validation passed.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the error message if validation failed.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the field that was validated.
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Gets whether the validation failed.
    /// </summary>
    public bool IsFailure => !IsValid;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Model_Validation_Result"/> class.
    /// </summary>
    public Model_Validation_Result()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Model_Validation_Result"/> class.
    /// </summary>
    /// <param name="isValid">Whether the validation passed</param>
    /// <param name="errorMessage">Error message if validation failed</param>
    /// <param name="fieldName">Name of the validated field</param>
    public Model_Validation_Result(bool isValid, string errorMessage = "", string fieldName = "")
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
        FieldName = fieldName;
    }

    #endregion

    #region Static Factory Methods

    /// <summary>
    /// Creates a successful validation result.
    /// </summary>
    /// <param name="fieldName">Name of the validated field</param>
    /// <returns>Validation result indicating success</returns>
    public static Model_Validation_Result Success(string fieldName = "")
    {
        return new Model_Validation_Result(true, string.Empty, fieldName);
    }

    /// <summary>
    /// Creates a failed validation result.
    /// </summary>
    /// <param name="errorMessage">Error message describing the validation failure</param>
    /// <param name="fieldName">Name of the validated field</param>
    /// <returns>Validation result indicating failure</returns>
    public static Model_Validation_Result Failure(string errorMessage, string fieldName = "")
    {
        return new Model_Validation_Result(false, errorMessage, fieldName);
    }

    #endregion
}
