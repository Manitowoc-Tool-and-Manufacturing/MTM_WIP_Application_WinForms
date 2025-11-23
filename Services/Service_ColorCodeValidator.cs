using System.Globalization;
using System.Text.RegularExpressions;

namespace MTM_WIP_Application_Winforms.Services
{
    #region Service_ColorCodeValidator

    /// <summary>
    /// Provides validation and formatting services for color codes and work orders.
    /// </summary>
    /// <remarks>
    /// This service handles:
    /// - Work order format validation (5-6 digits)
    /// - Work order auto-formatting (WO-###### with zero-padding)
    /// - Color code title-case formatting
    /// Used by Inventory Tab for data entry validation.
    /// </remarks>
    public static class Service_ColorCodeValidator
    {
        #region Fields

        private const string WorkOrderPrefix = "WO-";
        private const int WorkOrderDigits = 6;

        #endregion

        #region Methods

        /// <summary>
        /// Validates and formats a work order number.
        /// </summary>
        /// <param name="input">Raw work order input from user.</param>
        /// <param name="formattedWorkOrder">Formatted work order (e.g., "WO-064153").</param>
        /// <param name="errorMessage">Error message if validation fails.</param>
        /// <returns>True if validation succeeds, false otherwise.</returns>
        /// <remarks>
        /// Validation rules:
        /// - Accepts 5-6 digit numbers (with or without "WO-" prefix)
        /// - Strips any existing "WO-" prefix before processing
        /// - Pads numbers to 6 digits with leading zeros
        /// - Rejects input containing non-numeric characters
        /// 
        /// Examples:
        /// - "64153" → "WO-064153"
        /// - "WO-1234" → "WO-001234"
        /// - "ABC123" → Validation error
        /// - "12345678" → "WO-12345678" (accepts longer numbers)
        /// </remarks>
        public static bool ValidateAndFormatWorkOrder(string input, out string formattedWorkOrder, out string errorMessage)
        {
            formattedWorkOrder = string.Empty;
            errorMessage = string.Empty;

            // Handle null or empty
            if (string.IsNullOrWhiteSpace(input))
            {
                errorMessage = "Work order is required";
                return false;
            }

            // Strip any existing WO- prefix (including multiple prefixes like "WO-WO-123")
            string cleaned = input.Trim();
            while (cleaned.StartsWith(WorkOrderPrefix, StringComparison.OrdinalIgnoreCase))
            {
                cleaned = cleaned.Substring(WorkOrderPrefix.Length);
            }

            // Remove any whitespace
            cleaned = cleaned.Replace(" ", "").Replace("-", "");

            // Validate that remaining characters are all digits
            if (!Regex.IsMatch(cleaned, @"^\d+$"))
            {
                errorMessage = "Invalid work order format. Enter 5-6 digit number or WO-######";
                return false;
            }

            // Pad to 6 digits if less than 6 (but accept longer numbers)
            if (cleaned.Length < WorkOrderDigits)
            {
                cleaned = cleaned.PadLeft(WorkOrderDigits, '0');
            }

            // Format with WO- prefix
            formattedWorkOrder = WorkOrderPrefix + cleaned;
            return true;
        }

        /// <summary>
        /// Formats a color code to title case.
        /// </summary>
        /// <param name="colorCode">Raw color code input from user.</param>
        /// <returns>Title-cased color code (e.g., "blueberry" → "Blueberry").</returns>
        /// <remarks>
        /// Used when saving custom colors via "OTHER" option.
        /// Ensures consistent capitalization in database.
        /// Examples:
        /// - "blueberry" → "Blueberry"
        /// - "LIGHT BLUE" → "Light Blue"
        /// - "teal" → "Teal"
        /// </remarks>
        public static string FormatColorToTitleCase(string colorCode)
        {
            if (string.IsNullOrWhiteSpace(colorCode))
            {
                return string.Empty;
            }

            // Convert to title case using TextInfo
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(colorCode.Trim().ToLower());
        }

        #endregion
    }

    #endregion
}
