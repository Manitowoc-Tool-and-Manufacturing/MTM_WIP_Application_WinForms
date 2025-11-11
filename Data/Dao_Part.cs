using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_Part

/// <summary>
/// Data Access Object for part management operations.
/// Implements Model_Dao_Result pattern with async/await and Service_DebugTracer integration.
/// </summary>
internal static class Dao_Part
{
    #region Read Operations

    /// <summary>
    /// Retrieves all parts from the system.
    /// </summary>
    /// <returns>A Model_Dao_Result containing a DataTable with all parts.</returns>
    internal static async Task<Model_Dao_Result<DataTable>> GetAllPartsAsync(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(controlName: "Dao_Part");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_part_ids_Get_All",
                null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} parts");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve parts: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetAllPartsAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result<DataTable>.Failure("Error retrieving parts", ex);
        }
    }

    /// <summary>
    /// Retrieves a specific part by its part number.
    /// </summary>
    /// <param name="partNumber">The part number to search for.</param>
    /// <returns>A Model_Dao_Result containing the part data row.</returns>
    internal static async Task<Model_Dao_Result<DataRow>> GetPartByNumberAsync(string partNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["partNumber"] = partNumber }, controlName: "Dao_Part");

        try
        {
            Dictionary<string, object> parameters = new() { ["ItemNumber"] = partNumber };

            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_part_ids_Get_ByItemNumber",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0)
            {
                var row = result.Data.Rows[0];

                Service_DebugTracer.TraceMethodExit(row, controlName: "Dao_Part");
                return Model_Dao_Result<DataRow>.Success(row, $"Found part {partNumber}");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
                return Model_Dao_Result<DataRow>.Failure($"Part {partNumber} not found");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetPartByNumberAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result<DataRow>.Failure($"Error retrieving part {partNumber}", ex);
        }
    }

    /// <summary>
    /// Checks if a part exists in the system.
    /// </summary>
    /// <param name="partNumber">The part number to check.</param>
    /// <returns>A Model_Dao_Result containing true if the part exists, false otherwise.</returns>
    internal static async Task<Model_Dao_Result<bool>> PartExistsAsync(string partNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["partNumber"] = partNumber }, controlName: "Dao_Part");

        try
        {
            var result = await GetPartByNumberAsync(partNumber, connection, transaction);
            bool exists = result.IsSuccess;

            Service_DebugTracer.TraceMethodExit(exists, controlName: "Dao_Part");
            return Model_Dao_Result<bool>.Success(exists, exists ? $"Part {partNumber} exists" : $"Part {partNumber} does not exist");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);

            Service_DebugTracer.TraceMethodExit(false, controlName: "Dao_Part");
            return Model_Dao_Result<bool>.Failure($"Error checking if part {partNumber} exists", ex);
        }
    }

    /// <summary>
    /// Retrieves all distinct part types from the system.
    /// </summary>
    /// <returns>A Model_Dao_Result containing a DataTable with part types.</returns>
    internal static async Task<Model_Dao_Result<DataTable>> GetPartTypesAsync(
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(controlName: "Dao_Part");

        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_item_types_GetDistinct",
                null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} part types");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve part types: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetPartTypesAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result<DataTable>.Failure("Error retrieving part types", ex);
        }
    }

    /// <summary>
    /// Searches for parts matching the specified criteria.
    /// </summary>
    /// <param name="searchTerm">The search term to match against part numbers.</param>
    /// <returns>A Model_Dao_Result containing a DataTable with matching parts.</returns>
    internal static async Task<Model_Dao_Result<DataTable>> SearchPartsAsync(string searchTerm, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["searchTerm"] = searchTerm }, controlName: "Dao_Part");

        try
        {
            // Get all parts and filter in memory (unless there's a specific search stored procedure)
            var allPartsResult = await GetAllPartsAsync(connection, transaction);

            if (!allPartsResult.IsSuccess || allPartsResult.Data == null)
            {
                Service_DebugTracer.TraceMethodExit(allPartsResult, controlName: "Dao_Part");
                return allPartsResult;
            }

            // Filter by search term
            var filteredRows = allPartsResult.Data.AsEnumerable()
                .Where(row => row["ItemNumber"]?.ToString()?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true);

            var filteredTable = allPartsResult.Data.Clone();
            foreach (var row in filteredRows)
            {
                filteredTable.ImportRow(row);
            }

            Service_DebugTracer.TraceMethodExit(filteredTable, controlName: "Dao_Part");
            return Model_Dao_Result<DataTable>.Success(filteredTable, $"Found {filteredTable.Rows.Count} parts matching '{searchTerm}'");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "SearchPartsAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result<DataTable>.Failure($"Error searching for parts with term '{searchTerm}'", ex);
        }
    }

    #endregion

    #region Create Operations

    /// <summary>
    /// Creates a new part in the system.
    /// </summary>
    /// <param name="itemNumber">The part/item number.</param>
    /// <param name="customer">The customer name.</param>
    /// <param name="description">The part description.</param>
    /// <param name="issuedBy">The user creating the part.</param>
    /// <param name="type">The part type/category.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> CreatePartAsync(string itemNumber, string customer, string description,
        string issuedBy, string type,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["itemNumber"] = itemNumber,
            ["customer"] = customer,
            ["type"] = type
        }, controlName: "Dao_Part");

        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["ItemNumber"] = itemNumber,
                ["Customer"] = customer,
                ["Description"] = description,
                ["IssuedBy"] = issuedBy,
                ["ItemType"] = type
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_part_ids_Add_Part",
                parameters,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result.Success($"Part {itemNumber} added successfully");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result.Failure($"Failed to add part {itemNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "CreatePartAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result.Failure($"Error adding part {itemNumber}", ex);
        }
    }

    #endregion

    #region Update Operations

    /// <summary>
    /// Updates an existing part's information.
    /// </summary>
    /// <param name="id">The internal part ID.</param>
    /// <param name="itemNumber">The part/item number.</param>
    /// <param name="customer">The customer name.</param>
    /// <param name="description">The part description.</param>
    /// <param name="issuedBy">The user updating the part.</param>
    /// <param name="type">The part type/category.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> UpdatePartAsync(int id, string itemNumber, string customer,
        string description, string issuedBy, string type,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["id"] = id,
            ["itemNumber"] = itemNumber,
            ["customer"] = customer,
            ["type"] = type
        }, controlName: "Dao_Part");

        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["ID"] = id,
                ["ItemNumber"] = itemNumber,
                ["Customer"] = customer,
                ["Description"] = description,
                ["IssuedBy"] = issuedBy,
                ["ItemType"] = type
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_part_ids_Update_Part",
                parameters,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result.Success($"Part {itemNumber} updated successfully");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result.Failure($"Failed to update part {itemNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "UpdatePartAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result.Failure($"Error updating part {itemNumber}", ex);
        }
    }

    /// <summary>
    /// Updates a part by part number (looks up ID automatically).
    /// </summary>
    /// <param name="partNumber">The part number to update.</param>
    /// <param name="partType">The new part type.</param>
    /// <param name="user">The user making the update.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> UpdatePartByNumberAsync(string partNumber, string partType, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["partNumber"] = partNumber,
            ["partType"] = partType,
            ["user"] = user
        }, controlName: "Dao_Part");

        try
        {
            // First get the part to update
            var existingPart = await GetPartByNumberAsync(partNumber, connection, transaction);
            if (!existingPart.IsSuccess || existingPart.Data == null)
            {
                Service_DebugTracer.TraceMethodExit(existingPart, controlName: "Dao_Part");
                return Model_Dao_Result.Failure($"Part {partNumber} not found");
            }

            int id = Convert.ToInt32(existingPart.Data["ID"]);
            string customer = existingPart.Data["Customer"]?.ToString() ?? "";
            string description = existingPart.Data["Description"]?.ToString() ?? "";

            var result = await UpdatePartAsync(id, partNumber, customer, description, user, partType);

            Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
            return result;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "UpdatePartByNumberAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result.Failure($"Error updating part {partNumber}", ex);
        }
    }

    #endregion

    #region Delete Operations

    /// <summary>
    /// Deletes a part from the system by part number.
    /// </summary>
    /// <param name="partNumber">The part number to delete.</param>
    /// <returns>A Model_Dao_Result indicating success or failure.</returns>
    internal static async Task<Model_Dao_Result> DeletePartAsync(string partNumber,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object> { ["partNumber"] = partNumber }, controlName: "Dao_Part");

        try
        {
            Dictionary<string, object> parameters = new() { ["ItemNumber"] = partNumber };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_part_ids_Delete_ByItemNumber",
                parameters,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result.Success($"Part {partNumber} deleted successfully");
            }
            else
            {
                Service_DebugTracer.TraceMethodExit(result, controlName: "Dao_Part");
                return Model_Dao_Result.Failure($"Failed to delete part {partNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "DeletePartAsync");

            Service_DebugTracer.TraceMethodExit(null, controlName: "Dao_Part");
            return Model_Dao_Result.Failure($"Error deleting part {partNumber}", ex);
        }
    }

    #endregion

    #region Legacy Methods (Backward Compatibility)

    /// <summary>
    /// Legacy method for inserting parts. Use CreatePartAsync instead.
    /// </summary>
    [Obsolete("Use CreatePartAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result> InsertPart(string partNumber, string user, string partType, bool useAsync = false)
    {
        return await CreatePartAsync(partNumber, "", "", user, partType);
    }

    /// <summary>
    /// Legacy method for updating parts. Use UpdatePartByNumberAsync instead.
    /// </summary>
    [Obsolete("Use UpdatePartByNumberAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result> UpdatePart(string partNumber, string partType, string user, bool useAsync = false)
    {
        return await UpdatePartByNumberAsync(partNumber, partType, user);
    }

    /// <summary>
    /// Legacy method for deleting parts. Use DeletePartAsync instead.
    /// </summary>
    [Obsolete("Use DeletePartAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result> DeletePart(string partNumber, bool useAsync = false)
    {
        return await DeletePartAsync(partNumber);
    }

    /// <summary>
    /// Legacy method for getting all parts. Use GetAllPartsAsync instead.
    /// </summary>
    [Obsolete("Use GetAllPartsAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result<DataTable>> GetAllParts(bool useAsync = false, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        return await GetAllPartsAsync(connection, transaction);
    }

    /// <summary>
    /// Legacy method for getting part by number. Use GetPartByNumberAsync instead.
    /// </summary>
    [Obsolete("Use GetPartByNumberAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result<DataRow>> GetPartByNumber(string partNumber, bool useAsync = false, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        return await GetPartByNumberAsync(partNumber, connection, transaction);
    }

    /// <summary>
    /// Legacy method for checking part existence. Use PartExistsAsync instead.
    /// </summary>
    [Obsolete("Use PartExistsAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result<bool>> PartExists(string partNumber, bool useAsync = false)
    {
        return await PartExistsAsync(partNumber);
    }

    /// <summary>
    /// Legacy method for getting part types. Use GetPartTypesAsync instead.
    /// </summary>
    [Obsolete("Use GetPartTypesAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result<DataTable>> GetPartTypes(bool useAsync = false)
    {
        return await GetPartTypesAsync();
    }

    /// <summary>
    /// Legacy method wrapper. Use CreatePartAsync instead.
    /// </summary>
    [Obsolete("Use CreatePartAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result> AddPartWithStoredProcedure(string itemNumber, string customer, string description,
        string issuedBy, string type, bool useAsync = false)
    {
        return await CreatePartAsync(itemNumber, customer, description, issuedBy, type);
    }

    /// <summary>
    /// Legacy method wrapper. Use UpdatePartAsync instead.
    /// </summary>
    [Obsolete("Use UpdatePartAsync instead. This method will be removed in a future version.")]
    internal static async Task<Model_Dao_Result> UpdatePartWithStoredProcedure(int id, string itemNumber, string customer,
        string description, string issuedBy, string type, bool useAsync = false)
    {
        return await UpdatePartAsync(id, itemNumber, customer, description, issuedBy, type);
    }

    #endregion
}

#endregion

