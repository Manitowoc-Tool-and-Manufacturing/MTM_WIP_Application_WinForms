using System;
using System.Collections.Generic;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Centralized validation service for user input validation.
/// Provides extensible validator registration and standard validation methods.
/// </summary>
public static class Service_Validation
{
    #region Fields

    private static readonly Dictionary<string, IValidator> _validators = new();
    private static readonly object _validatorLock = new();

    #endregion

    #region Constructor

    /// <summary>
    /// Static constructor to initialize default validators.
    /// </summary>
    static Service_Validation()
    {
        InitializeDefaultValidators();
    }

    #endregion

    #region Public Methods - Validator Registration

    /// <summary>
    /// Registers a validator for a specific field type.
    /// </summary>
    /// <param name="fieldType">The field type identifier (e.g., "PartNumber", "WorkOrder")</param>
    /// <param name="validator">The validator implementation</param>
    /// <exception cref="ArgumentNullException">Thrown when fieldType or validator is null</exception>
    public static void RegisterValidator(string fieldType, IValidator validator)
    {
        ArgumentNullException.ThrowIfNull(fieldType);
        ArgumentNullException.ThrowIfNull(validator);

        lock (_validatorLock)
        {
            _validators[fieldType] = validator;
        }
    }

    /// <summary>
    /// Unregisters a validator for a specific field type.
    /// </summary>
    /// <param name="fieldType">The field type identifier to unregister</param>
    /// <returns>True if validator was removed, false if it didn't exist</returns>
    public static bool UnregisterValidator(string fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        lock (_validatorLock)
        {
            return _validators.Remove(fieldType);
        }
    }

    /// <summary>
    /// Gets a validator for a specific field type.
    /// </summary>
    /// <param name="fieldType">The field type identifier</param>
    /// <returns>The validator if registered, null otherwise</returns>
    public static IValidator? GetValidator(string fieldType)
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        lock (_validatorLock)
        {
            return _validators.TryGetValue(fieldType, out var validator) ? validator : null;
        }
    }

    #endregion

    #region Public Methods - Validation

    /// <summary>
    /// Validates input using a registered validator.
    /// </summary>
    /// <param name="fieldType">The field type identifier</param>
    /// <param name="input">The input to validate</param>
    /// <param name="fieldName">Optional friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result Validate(string fieldType, string input, string fieldName = "")
    {
        ArgumentNullException.ThrowIfNull(fieldType);

        var validator = GetValidator(fieldType);
        if (validator == null)
        {
            return Model_Validation_Result.Failure(
                $"No validator registered for field type '{fieldType}'",
                fieldName);
        }

        var result = validator.Validate(input);
        if (!string.IsNullOrEmpty(fieldName) && string.IsNullOrEmpty(result.FieldName))
        {
            result.FieldName = fieldName;
        }

        return result;
    }

    /// <summary>
    /// Validates that a string is not null or whitespace.
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <param name="fieldName">Friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result ValidateRequired(string? input, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure($"{fieldName} is required.", fieldName);
        }

        return Model_Validation_Result.Success(fieldName);
    }

    /// <summary>
    /// Validates that a string meets minimum length requirements.
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <param name="minLength">Minimum required length</param>
    /// <param name="fieldName">Friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result ValidateMinLength(string? input, int minLength, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (string.IsNullOrEmpty(input) || input.Length < minLength)
        {
            return Model_Validation_Result.Failure(
                $"{fieldName} must be at least {minLength} characters.",
                fieldName);
        }

        return Model_Validation_Result.Success(fieldName);
    }

    /// <summary>
    /// Validates that a string does not exceed maximum length.
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <param name="maxLength">Maximum allowed length</param>
    /// <param name="fieldName">Friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result ValidateMaxLength(string? input, int maxLength, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (!string.IsNullOrEmpty(input) && input.Length > maxLength)
        {
            return Model_Validation_Result.Failure(
                $"{fieldName} cannot exceed {maxLength} characters.",
                fieldName);
        }

        return Model_Validation_Result.Success(fieldName);
    }

    /// <summary>
    /// Validates that a string contains only numeric characters.
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <param name="fieldName">Friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result ValidateNumeric(string? input, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure($"{fieldName} is required.", fieldName);
        }

        if (!int.TryParse(input, out _))
        {
            return Model_Validation_Result.Failure($"{fieldName} must be a valid number.", fieldName);
        }

        return Model_Validation_Result.Success(fieldName);
    }

    /// <summary>
    /// Validates that a string can be parsed as a decimal within a range.
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <param name="min">Minimum allowed value (inclusive)</param>
    /// <param name="max">Maximum allowed value (inclusive)</param>
    /// <param name="fieldName">Friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result ValidateDecimalRange(string? input, decimal min, decimal max, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure($"{fieldName} is required.", fieldName);
        }

        if (!decimal.TryParse(input, out var value))
        {
            return Model_Validation_Result.Failure($"{fieldName} must be a valid decimal number.", fieldName);
        }

        if (value < min || value > max)
        {
            return Model_Validation_Result.Failure(
                $"{fieldName} must be between {min} and {max}.",
                fieldName);
        }

        return Model_Validation_Result.Success(fieldName);
    }

    /// <summary>
    /// Validates that a string can be parsed as an integer within a range.
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <param name="min">Minimum allowed value (inclusive)</param>
    /// <param name="max">Maximum allowed value (inclusive)</param>
    /// <param name="fieldName">Friendly field name for error messages</param>
    /// <returns>Validation result indicating success or failure</returns>
    public static Model_Validation_Result ValidateIntegerRange(string? input, int min, int max, string fieldName)
    {
        ArgumentNullException.ThrowIfNull(fieldName);

        if (string.IsNullOrWhiteSpace(input))
        {
            return Model_Validation_Result.Failure($"{fieldName} is required.", fieldName);
        }

        if (!int.TryParse(input, out var value))
        {
            return Model_Validation_Result.Failure($"{fieldName} must be a valid integer.", fieldName);
        }

        if (value < min || value > max)
        {
            return Model_Validation_Result.Failure(
                $"{fieldName} must be between {min} and {max}.",
                fieldName);
        }

        return Model_Validation_Result.Success(fieldName);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Initializes default validators for common field types.
    /// </summary>
    private static void InitializeDefaultValidators()
    {
        // Register standard validators
        RegisterValidator("PartNumber", new Validators.PartNumberValidator());
        RegisterValidator("WorkOrder", new Validators.WorkOrderValidator());
        RegisterValidator("LocationCode", new Validators.LocationCodeValidator());
        
        // Register common numeric validators
        RegisterValidator("Quantity", new Validators.NumericValidator(0, 999999, allowDecimals: false, fieldName: "Quantity"));
        RegisterValidator("Weight", new Validators.NumericValidator(0, 99999.99m, allowDecimals: true, fieldName: "Weight"));
        RegisterValidator("Price", new Validators.NumericValidator(0, 9999999.99m, allowDecimals: true, fieldName: "Price"));
    }

    #endregion
}
