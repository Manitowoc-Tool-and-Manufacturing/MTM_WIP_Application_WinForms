using System;
using System.Collections.Generic;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Examples;

/// <summary>
/// Example demonstrating proper usage of Service_Validation in forms.
/// This class shows best practices for input validation before database operations.
/// </summary>
/// <remarks>
/// IMPORTANT: Always validate input at the UI layer BEFORE calling DAOs.
/// Use Service_ErrorHandler.HandleValidationError() to display validation errors to users.
/// </remarks>
public static class Service_Validation_Usage_Example
{
    #region Example 1: Basic Required Field Validation

    /// <summary>
    /// Example: Validate required fields in a form.
    /// </summary>
    public static void Example_ValidateRequiredFields()
    {
        // Simulated form input
        string partNumberInput = "";  // User left field empty
        string quantityInput = "50";

        // Validate part number (required field)
        var partNumberResult = Service_Validation.ValidateRequired(partNumberInput, "Part Number");
        if (partNumberResult.IsFailure)
        {
            // Display error to user via Service_ErrorHandler
            Service_ErrorHandler.HandleValidationError(
                partNumberResult.ErrorMessage,
                field: partNumberResult.FieldName);
            return; // Stop processing
        }

        // Validate quantity (required field)
        var quantityResult = Service_Validation.ValidateRequired(quantityInput, "Quantity");
        if (quantityResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(quantityResult.ErrorMessage);
            return;
        }

        // All validations passed - proceed with DAO call
        // await Dao_Part.InsertAsync(...);
    }

    #endregion

    #region Example 2: Using Registered Validators

    /// <summary>
    /// Example: Validate using registered validators for specific field types.
    /// </summary>
    public static void Example_UseRegisteredValidators()
    {
        // Simulated form input
        string partNumber = "R-ABC123-01";
        string workOrder = "WO-1234";
        string location = "A-12";

        // Validate part number using registered PartNumberValidator
        var partResult = Service_Validation.Validate("PartNumber", partNumber, "Part Number");
        if (partResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(partResult.ErrorMessage);
            return;
        }

        // Validate work order using registered WorkOrderValidator
        var workOrderResult = Service_Validation.Validate("WorkOrder", workOrder, "Work Order");
        if (workOrderResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(workOrderResult.ErrorMessage);
            return;
        }

        // Validate location using registered LocationCodeValidator
        var locationResult = Service_Validation.Validate("LocationCode", location, "Location");
        if (locationResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(locationResult.ErrorMessage);
            return;
        }

        // All validations passed - proceed with DAO call
        // await Dao_Inventory.TransferAsync(...);
    }

    #endregion

    #region Example 3: Numeric Range Validation

    /// <summary>
    /// Example: Validate numeric input with range constraints.
    /// </summary>
    public static void Example_ValidateNumericRanges()
    {
        // Simulated form input
        string quantityInput = "150";
        string weightInput = "45.75";
        string priceInput = "1250.50";

        // Validate quantity (integer, 0-999999)
        var quantityResult = Service_Validation.Validate("Quantity", quantityInput, "Quantity");
        if (quantityResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(quantityResult.ErrorMessage);
            return;
        }

        // Validate weight (decimal, 0-99999.99)
        var weightResult = Service_Validation.Validate("Weight", weightInput, "Weight");
        if (weightResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(weightResult.ErrorMessage);
            return;
        }

        // Validate price (decimal, 0-9999999.99)
        var priceResult = Service_Validation.Validate("Price", priceInput, "Price");
        if (priceResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(priceResult.ErrorMessage);
            return;
        }

        // All validations passed - proceed with DAO call
        // await Dao_Part.UpdatePricingAsync(...);
    }

    #endregion

    #region Example 4: Standard Validation Methods

    /// <summary>
    /// Example: Use standard validation methods for common scenarios.
    /// </summary>
    public static void Example_StandardValidationMethods()
    {
        // Simulated form input
        string description = "High-precision bearing assembly";
        string notes = "Special handling required";
        string code = "12345";

        // Validate minimum length
        var descResult = Service_Validation.ValidateMinLength(description, 10, "Description");
        if (descResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(descResult.ErrorMessage);
            return;
        }

        // Validate maximum length
        var notesResult = Service_Validation.ValidateMaxLength(notes, 500, "Notes");
        if (notesResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(notesResult.ErrorMessage);
            return;
        }

        // Validate numeric only
        var codeResult = Service_Validation.ValidateNumeric(code, "Code");
        if (codeResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(codeResult.ErrorMessage);
            return;
        }

        // All validations passed - proceed with DAO call
        // await Dao_Part.UpdateAsync(...);
    }

    #endregion

    #region Example 5: Custom Validator Registration (Extensibility)

    /// <summary>
    /// Example: Register custom validators for audit process or new field types.
    /// </summary>
    public static void Example_RegisterCustomValidators()
    {
        // During audit, if new validation type needed, easily register:
        // Service_Validation.RegisterValidator("SerialNumber", new SerialNumberValidator());
        // Service_Validation.RegisterValidator("BatchCode", new BatchCodeValidator());

        // Then use just like built-in validators:
        // var result = Service_Validation.Validate("SerialNumber", input, "Serial Number");

        // This extensibility allows audit process to add validators without modifying Service_Validation
    }

    #endregion

    #region Example 6: Complete Form Validation Pattern

    /// <summary>
    /// Example: Complete validation pattern for a form's Save button click handler.
    /// </summary>
    public static void Example_CompleteFormValidation()
    {
        // Simulated form input from TextBox controls
        string partNumber = "R-XYZ-01";
        string quantity = "100";
        string location = "B-5";
        string workOrder = "WO-5678";

        // Step 1: Validate all inputs BEFORE calling DAO
        if (!ValidateFormInputs(partNumber, quantity, location, workOrder))
        {
            return; // Validation failed, errors already shown to user
        }

        // Step 2: Apply standard transformations
        partNumber = partNumber.Trim().ToUpperInvariant();
        location = location.Trim().ToUpperInvariant();
        workOrder = workOrder.Trim().ToUpperInvariant();

        // Step 3: Proceed with DAO call (validation passed)
        // var result = await Dao_Inventory.AddAsync(partNumber, quantity, location, workOrder);
        // if (result.IsFailure)
        // {
        //     Service_ErrorHandler.HandleDatabaseError(...);
        //     return;
        // }
        
        // Success!
    }

    /// <summary>
    /// Validates all form inputs and displays errors to user.
    /// </summary>
    /// <returns>True if all validations pass, false otherwise</returns>
    private static bool ValidateFormInputs(string partNumber, string quantity, string location, string workOrder)
    {
        // Validate part number
        var partResult = Service_Validation.Validate("PartNumber", partNumber, "Part Number");
        if (partResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(
                partResult.ErrorMessage,
                field: partResult.FieldName);
            return false;
        }

        // Validate quantity
        var quantityResult = Service_Validation.Validate("Quantity", quantity, "Quantity");
        if (quantityResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(quantityResult.ErrorMessage);
            return false;
        }

        // Validate location
        var locationResult = Service_Validation.Validate("LocationCode", location, "Location");
        if (locationResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(locationResult.ErrorMessage);
            return false;
        }

        // Validate work order
        var workOrderResult = Service_Validation.Validate("WorkOrder", workOrder, "Work Order");
        if (workOrderResult.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(workOrderResult.ErrorMessage);
            return false;
        }

        // All validations passed
        return true;
    }

    #endregion

    #region Example 7: Creating Custom Validators

    /// <summary>
    /// Example: How to create a custom validator implementing IValidator.
    /// </summary>
    /// <remarks>
    /// To create a custom validator:
    /// 1. Create a class implementing IValidator interface
    /// 2. Implement Validate(string input) method
    /// 3. Return Model_Validation_Result.Success() or Model_Validation_Result.Failure()
    /// 4. Register with Service_Validation.RegisterValidator()
    /// </remarks>
    public static void Example_CustomValidatorPattern()
    {
        /*
        // Example custom validator implementation:
        
        public class SerialNumberValidator : IValidator
        {
            private static readonly Regex Pattern = new(@"^SN-\d{8}$", RegexOptions.Compiled);
            
            public Model_Validation_Result Validate(string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return Model_Validation_Result.Failure(
                        "Serial number is required.",
                        "SerialNumber");
                }
                
                var trimmed = input.Trim().ToUpperInvariant();
                
                if (!Pattern.IsMatch(trimmed))
                {
                    return Model_Validation_Result.Failure(
                        "Serial number must follow format: SN-12345678",
                        "SerialNumber");
                }
                
                return Model_Validation_Result.Success("SerialNumber");
            }
        }
        
        // Register the custom validator:
        Service_Validation.RegisterValidator("SerialNumber", new SerialNumberValidator());
        
        // Use it:
        var result = Service_Validation.Validate("SerialNumber", "SN-00012345", "Serial Number");
        if (result.IsFailure)
        {
            Service_ErrorHandler.HandleValidationError(result.ErrorMessage);
        }
        */
    }

    #endregion
}
