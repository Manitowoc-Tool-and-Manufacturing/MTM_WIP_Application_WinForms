using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Data
{
    #region Dao_ColorCode

    /// <summary>
    /// Data access object for color code operations.
    /// </summary>
    /// <remarks>
    /// Provides access to md_color_codes table for:
    /// - Retrieving all color codes for dropdown/suggestion lists
    /// - Adding custom user-defined color codes
    /// All methods return Model_Dao_Result for consistent error handling.
    /// </remarks>
    public class Dao_ColorCode
    {
        #region Methods

        /// <summary>
        /// Retrieves all color codes from the database.
        /// </summary>
        /// <returns>
        /// Model_Dao_Result containing DataTable with columns: ColorCode, IsUserDefined, CreatedDate.
        /// Check IsSuccess before accessing Data.
        /// ErrorMessage contains user-friendly message on failure.
        /// </returns>
        /// <remarks>
        /// Returns all color codes except "OTHER" (which is UI-only trigger).
        /// Results are sorted alphabetically by ColorCode.
        /// Used to populate SuggestionTextBox in Inventory Tab.
        /// </remarks>
        public async Task<Model_Dao_Result<DataTable>> GetAllAsync()
        {
            return await Helper_Database_StoredProcedure
                .ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_color_codes_GetAll");
        }

        /// <summary>
        /// Adds a custom color code to the database.
        /// </summary>
        /// <param name="colorCode">Color code to add (should be title-cased).</param>
        /// <returns>
        /// Model_Dao_Result with success/failure status.
        /// Status codes:
        /// - 1: Successfully added new color
        /// - 0: Color already exists (silently reused)
        /// - -2: ColorCode is required
        /// - -3: Cannot add reserved color codes (Unknown, Other)
        /// Check IsSuccess before proceeding.
        /// </returns>
        /// <remarks>
        /// Duplicate prevention: If color already exists, returns success without error.
        /// Reserved colors (Unknown, Other) cannot be added.
        /// ColorCode should be formatted to title case before calling this method.
        /// Max length: 50 characters (enforced by database).
        /// </remarks>
        public async Task<Model_Dao_Result> AddCustomColorAsync(string colorCode)
        {
            var parameters = new Dictionary<string, object>
            {
                { "ColorCode", colorCode }  // NO p_ prefix in C# (only in SQL)
            };

            return await Helper_Database_StoredProcedure
                .ExecuteNonQueryWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_color_codes_Add",
                    parameters);
        }

        #endregion
    }

    #endregion
}
