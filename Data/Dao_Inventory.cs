using System.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Services;

namespace MTM_Inventory_Application.Data;

#region Dao_Inventory

public static class Dao_Inventory
{

    #region Search Methods

    /// <summary>
    /// Retrieves all inventory records from the database.
    /// </summary>
    /// <param name="useAsync">Whether to execute asynchronously</param>
    /// <returns>DaoResult containing DataTable with all inventory records</returns>
    public static async Task<DaoResult<DataTable>> GetAllInventoryAsync(bool useAsync = true)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["useAsync"] = useAsync
        }, nameof(GetAllInventoryAsync), "Dao_Inventory");

        try
        {
            // Use inv_inventory_Get_ByUser with empty user to get all records
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Get_ByUser",
                new Dictionary<string, object> { ["p_User"] = "" },
                null
            );

            if (result.IsSuccess && result.Data != null)
            {
                var successResult = DaoResult<DataTable>.Success(result.Data, 
                    $"Retrieved {result.Data.Rows.Count} inventory records");

                Service_DebugTracer.TraceBusinessLogic("INVENTORY_GET_ALL_COMPLETE",
                    outputData: new Dictionary<string, object>
                    {
                        ["recordCount"] = result.Data.Rows.Count,
                        ["hasData"] = result.Data.Rows.Count > 0
                    });

                Service_DebugTracer.TraceMethodExit(successResult, nameof(GetAllInventoryAsync), "Dao_Inventory");
                return successResult;
            }
            else
            {
                var failureResult = DaoResult<DataTable>.Failure($"Failed to retrieve all inventory: {result.ErrorMessage}");
                Service_DebugTracer.TraceMethodExit(failureResult, nameof(GetAllInventoryAsync), "Dao_Inventory");
                return failureResult;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetAllInventoryAsync");

            var errorResult = DaoResult<DataTable>.Failure("Failed to retrieve all inventory", ex);
            Service_DebugTracer.TraceMethodExit(errorResult, nameof(GetAllInventoryAsync), "Dao_Inventory");
            return errorResult;
        }
    }

    /// <summary>
    /// Searches inventory records by partial PartID match.
    /// </summary>
    /// <param name="searchTerm">Partial PartID to search for</param>
    /// <param name="useAsync">Whether to execute asynchronously</param>
    /// <returns>DaoResult containing DataTable with matching inventory records</returns>
    public static async Task<DaoResult<DataTable>> SearchInventoryAsync(string searchTerm, bool useAsync = true)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["searchTerm"] = searchTerm ?? "NULL",
            ["useAsync"] = useAsync
        }, nameof(SearchInventoryAsync), "Dao_Inventory");

        try
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Return all inventory if search term is empty
                return await GetAllInventoryAsync(useAsync);
            }

            // Use GetInventoryByPartIdAsync which will match partial PartIDs
            var result = await GetInventoryByPartIdAsync(searchTerm, useAsync);

            if (result.IsSuccess)
            {
                Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_COMPLETE",
                    inputData: new Dictionary<string, object> { ["searchTerm"] = searchTerm },
                    outputData: new Dictionary<string, object>
                    {
                        ["recordCount"] = result.Data?.Rows.Count ?? 0
                    });

                Service_DebugTracer.TraceMethodExit(result, nameof(SearchInventoryAsync), "Dao_Inventory");
                return result;
            }
            else
            {
                var failureResult = DaoResult<DataTable>.Failure($"Failed to search inventory: {result.ErrorMessage}");
                Service_DebugTracer.TraceMethodExit(failureResult, nameof(SearchInventoryAsync), "Dao_Inventory");
                return failureResult;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "SearchInventoryAsync");

            var errorResult = DaoResult<DataTable>.Failure($"Failed to search inventory for term '{searchTerm}'", ex);
            Service_DebugTracer.TraceMethodExit(errorResult, nameof(SearchInventoryAsync), "Dao_Inventory");
            return errorResult;
        }
    }

    public static async Task<DaoResult<DataTable>> GetInventoryByPartIdAsync(string partId, bool useAsync = false)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["partId"] = partId,
            ["useAsync"] = useAsync
        }, nameof(GetInventoryByPartIdAsync), "Dao_Inventory");

        Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_BY_PARTID", new Dictionary<string, object>
        {
            ["SearchCriteria"] = partId,
            ["AsyncMode"] = useAsync
        });

        try
        {
            // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core for procedures with output parameters
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Get_ByPartID",
                new Dictionary<string, object> { ["p_PartID"] = partId }, // p_ prefix added automatically
                null // No progress helper for this method
            );
                
            if (result.IsSuccess && result.Data != null)
            {
                var successResult = DaoResult<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} inventory items for part {partId}");
                
                Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_COMPLETE", 
                    inputData: new Dictionary<string, object> { ["partId"] = partId },
                    outputData: new Dictionary<string, object> 
                    { 
                        ["recordCount"] = result.Data.Rows.Count,
                        ["hasData"] = result.Data.Rows.Count > 0 
                    },
                    businessRules: new Dictionary<string, object>
                    {
                        ["rule"] = "Return all inventory records matching PartID",
                        ["validation"] = "PartID must not be null or empty"
                    });

                Service_DebugTracer.TraceMethodExit(successResult, nameof(GetInventoryByPartIdAsync), "Dao_Inventory");
                return successResult;
            }
            else
            {
                var failureResult = DaoResult<DataTable>.Failure($"Failed to retrieve inventory for part {partId}: {result.ErrorMessage}");
                
                Service_DebugTracer.TraceMethodExit(failureResult, nameof(GetInventoryByPartIdAsync), "Dao_Inventory");
                return failureResult;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetInventoryByPartIdAsync");
            
            var errorResult = DaoResult<DataTable>.Failure($"Failed to retrieve inventory for part {partId}", ex);
            
            Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_ERROR",
                inputData: new Dictionary<string, object> { ["partId"] = partId },
                validationResults: new Dictionary<string, object>
                {
                    ["exception"] = ex.GetType().Name,
                    ["message"] = ex.Message,
                    ["stackTrace"] = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace.Length, 200)) + "..."
                });

            Service_DebugTracer.TraceMethodExit(errorResult, nameof(GetInventoryByPartIdAsync), "Dao_Inventory");
            return errorResult;
        }
    }

    public static async Task<DaoResult<DataTable>> GetInventoryByPartIdAndOperationAsync(string partId, string operation,
        bool useAsync = false)
    {
        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["partId"] = partId ?? "NULL",
            ["operation"] = operation ?? "NULL",
            ["useAsync"] = useAsync
        }, nameof(GetInventoryByPartIdAndOperationAsync), "Dao_Inventory");

        try
        {
            // Validate input parameters to prevent ArgumentException
            if (string.IsNullOrWhiteSpace(partId))
            {
                var validationResult = DaoResult<DataTable>.Failure("PartID cannot be null or empty");
                Service_DebugTracer.TraceMethodExit(validationResult, nameof(GetInventoryByPartIdAndOperationAsync), "Dao_Inventory");
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(operation))
            {
                var validationResult = DaoResult<DataTable>.Failure("Operation cannot be null or empty");
                Service_DebugTracer.TraceMethodExit(validationResult, nameof(GetInventoryByPartIdAndOperationAsync), "Dao_Inventory");
                return validationResult;
            }

            // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core for procedures with output parameters
            // IMPORTANT: Procedure name in database is "inv_inventory_Get_ByPartIDandOperation" with lowercase "and"
            // IMPORTANT: Stored procedure parameter is p_Operation (not o_Operation)
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Get_ByPartIDandOperation",  // Fixed: lowercase "and" to match database
                new Dictionary<string, object> 
                { 
                    ["p_PartID"] = partId.Trim(),
                    ["p_Operation"] = operation.Trim()  // Fixed: Changed from o_Operation to p_Operation
                },
                null // No progress helper for this method
            );
                
            if (result.IsSuccess && result.Data != null)
            {
                var successResult = DaoResult<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} inventory items for part {partId}, operation {operation}");
                
                Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_BY_PART_AND_OP_COMPLETE", 
                    inputData: new Dictionary<string, object> { ["partId"] = partId, ["operation"] = operation },
                    outputData: new Dictionary<string, object> 
                    { 
                        ["recordCount"] = result.Data.Rows.Count,
                        ["hasData"] = result.Data.Rows.Count > 0 
                    });

                Service_DebugTracer.TraceMethodExit(successResult, nameof(GetInventoryByPartIdAndOperationAsync), "Dao_Inventory");
                return successResult;
            }
            else
            {
                var failureResult = DaoResult<DataTable>.Failure($"Failed to retrieve inventory for part {partId}, operation {operation}: {result.ErrorMessage}");
                Service_DebugTracer.TraceMethodExit(failureResult, nameof(GetInventoryByPartIdAndOperationAsync), "Dao_Inventory");
                return failureResult;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetInventoryByPartIdAndOperationAsync");
            
            var errorResult = DaoResult<DataTable>.Failure($"Failed to retrieve inventory for part {partId}, operation {operation}", ex);
            
            Service_DebugTracer.TraceBusinessLogic("INVENTORY_SEARCH_BY_PART_AND_OP_ERROR",
                inputData: new Dictionary<string, object> { ["partId"] = partId ?? "NULL", ["operation"] = operation ?? "NULL" },
                validationResults: new Dictionary<string, object>
                {
                    ["exception"] = ex.GetType().Name,
                    ["message"] = ex.Message
                });

            Service_DebugTracer.TraceMethodExit(errorResult, nameof(GetInventoryByPartIdAndOperationAsync), "Dao_Inventory");
            return errorResult;
        }
    }

    #endregion

    #region Modification Methods

    public static async Task<DaoResult<int>> AddInventoryItemAsync(
        string partId,
        string location,
        string operation,
        int quantity,
        string? itemType,
        string user,
        string? batchNumber,
        string notes,
        bool useAsync = false)
    {
        try
        {
            // Get item type if not provided
            if (string.IsNullOrWhiteSpace(itemType))
            {
                var itemTypeResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_AppVariables.ConnectionString,
                    "md_part_ids_Get_ByItemNumber",
                    new Dictionary<string, object> { ["ItemNumber"] = partId },
                    null // No progress helper for this method
                );

                if (itemTypeResult.IsSuccess && itemTypeResult.Data != null && itemTypeResult.Data.Rows.Count > 0)
                {
                    itemType = itemTypeResult.Data.Rows[0]["ItemType"]?.ToString() ?? "None";
                }
                else
                {
                    itemType = "None";
                }
            }

            // Generate batch number if not provided
            if (string.IsNullOrWhiteSpace(batchNumber))
            {
                var batchNumberResult = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                    Model_AppVariables.ConnectionString,
                    "inv_inventory_GetNextBatchNumber",
                    null, // No parameters needed
                    null // No progress helper for this method
                );

                if (batchNumberResult.IsSuccess && batchNumberResult.Data != null && 
                    int.TryParse(batchNumberResult.Data.ToString(), out int bn))
                {
                    batchNumber = bn.ToString("D10");
                }
                else
                {
                    batchNumber = "0000000001";
                }
            }

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Add_Item",
                new Dictionary<string, object>
                {
                    ["p_PartID"] = partId,         // p_ prefix added automatically
                    ["Location"] = location,
                    ["p_Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["ItemType"] = itemType ?? "None",
                    ["p_User"] = user,
                    ["BatchNumber"] = batchNumber,
                    ["Notes"] = notes
                },
                null // No progress helper for this method
            );

            // FIXED: Do NOT call FixBatchNumbersAsync() after inventory additions
            // This was causing unwanted consolidation of separate transactions into single rows
            // FixBatchNumbers should only be called for maintenance operations, not regular transactions
            // await FixBatchNumbersAsync();

            if (result.IsSuccess)
            {
                return DaoResult<int>.Success(1, $"Added inventory item: {partId} at {location}, quantity {quantity}", 1);
            }
            else
            {
                return DaoResult<int>.Failure($"Failed to add inventory item for part {partId}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "AddInventoryItemAsync");
            return DaoResult<int>.Failure($"Failed to add inventory item for part {partId}", ex);
        }
    }

    public static async Task<DaoResult<(int RemovedCount, List<string> ErrorMessages)>> RemoveInventoryItemsFromDataGridViewAsync(System.Windows.Forms.DataGridView dgv, bool useAsync = false)
    {
        int removedCount = 0;
        List<string> errorMessages = new();

        if (dgv == null || dgv.SelectedRows.Count == 0)
        {
            return DaoResult<(int, List<string>)>.Success((0, errorMessages), "No rows selected for removal");
        }

        try
        {
            foreach (System.Windows.Forms.DataGridViewRow row in dgv.SelectedRows)
            {
                string partId = row.Cells["p_PartID"].Value?.ToString() ?? "";
                string location = row.Cells["Location"].Value?.ToString() ?? "";
                string operation = row.Cells["p_Operation"].Value?.ToString() ?? "";
                int quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int qty) ? qty : 0;
                string batchNumber = row.Cells["BatchNumber"].Value?.ToString() ?? "";
                string itemType = row.Cells["ItemType"].Value?.ToString() ?? "";
                string user = row.Cells["p_User"].Value?.ToString() ?? "";
                string notes = row.Cells["Notes"].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(partId) || string.IsNullOrWhiteSpace(location) ||
                    string.IsNullOrWhiteSpace(operation))
                {
                    continue;
                }

                var removeResult = await RemoveInventoryItemAsync(
                    partId, location, operation, quantity, itemType, user, batchNumber, notes, useAsync);

                // FIXED: Check if removal was actually successful (Data.Status > 0 means items were removed)
                if (removeResult.IsSuccess && removeResult.Data.Status > 0)
                {
                    removedCount += removeResult.Data.Status;
                }
                else if (removeResult.IsSuccess && removeResult.Data.Status == 0)
                {
                    // No matching item found, add to error messages
                    errorMessages.Add($"PartID: {partId}, Location: {location}, Operation: {operation}, Error: {removeResult.Data.ErrorMsg}");
                }
                else
                {
                    // Actual failure occurred
                    errorMessages.Add($"PartID: {partId}, Location: {location}, Operation: {operation}, Error: {removeResult.ErrorMessage}");
                }
            }

            return DaoResult<(int, List<string>)>.Success((removedCount, errorMessages), $"Processed {dgv.SelectedRows.Count} items, removed {removedCount}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "RemoveInventoryItemsFromDataGridViewAsync");
            return DaoResult<(int, List<string>)>.Failure("Failed to remove inventory items from DataGridView", ex);
        }
    }

    public static async Task<DaoResult<(int Status, string ErrorMsg)>> RemoveInventoryItemAsync(
        string partId,
        string location,
        string operation,
        int quantity,
        string itemType,
        string user,
        string batchNumber,
        string notes,
        bool useAsync = false)
    {
        try
        {
            // MIGRATED: Use Helper_Database_StoredProcedure for proper status handling
            Dictionary<string, object> parameters = new()
            {
                ["p_PartID"] = partId,             // p_ prefix added automatically
                ["Location"] = location,
                ["p_Operation"] = operation,
                ["Quantity"] = quantity,
                ["ItemType"] = itemType,
                ["p_User"] = user,
                ["BatchNumber"] = batchNumber,
                ["Notes"] = notes
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Remove_Item",
                parameters,
                null // No progress helper for this method
            );

            // Check result success - ErrorMessage may contain "Item not found" if item doesn't exist
            if (result.IsSuccess)
            {
                // Check if ErrorMessage indicates "not found" scenario
                bool notFound = !string.IsNullOrEmpty(result.ErrorMessage) && 
                                result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase);
                
                if (notFound)
                {
                    // No matching item found for removal
                    return DaoResult<(int, string)>.Success((0, result.ErrorMessage), 
                        $"No inventory item found for removal: {result.ErrorMessage}");
                }
                else
                {
                    // Actual removal occurred
                    return DaoResult<(int, string)>.Success((1, result.ErrorMessage ?? ""), 
                        $"Successfully removed inventory item: {partId}");
                }
            }
            else
            {
                // Database error or exception
                return DaoResult<(int, string)>.Failure(
                    $"Failed to remove inventory item for part {partId}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "RemoveInventoryItemAsync");
            return DaoResult<(int, string)>.Failure($"Failed to remove inventory item for part {partId}", ex);
        }
    }

    public static async Task<DaoResult> TransferPartSimpleAsync(string batchNumber, string partId, string operation, string newLocation)
    {
        try
        {
            // Use explicit p_ prefix for transfer procedures (parameter cache may not be populated yet)
            Dictionary<string, object> parameters = new()
            {
                ["p_BatchNumber"] = batchNumber,
                ["p_PartID"] = partId,
                ["p_Operation"] = operation,
                ["p_NewLocation"] = newLocation
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Transfer_Part",
                parameters,
                null // No progress helper for this method
            );

            // FIXED: Do NOT call FixBatchNumbersAsync() after transfer operations
            // This was causing unwanted consolidation of separate transactions into single rows
            // FixBatchNumbers should only be called for maintenance operations, not regular transactions
            // await FixBatchNumbersAsync();
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"Transferred part {partId} from {operation} to {newLocation}");
            }
            else
            {
                return DaoResult.Failure($"Failed to transfer part {partId}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "TransferPartSimpleAsync");
            return DaoResult.Failure($"Failed to transfer part {partId}", ex);
        }
    }

    public static async Task<DaoResult> TransferInventoryQuantityAsync(string batchNumber, string partId, string operation,
        int transferQuantity, int originalQuantity, string newLocation, string user)
    {
        try
        {
            // Use explicit p_ prefix for transfer procedures (parameter cache may not be populated yet)
            Dictionary<string, object> parameters = new()
            {
                ["p_BatchNumber"] = batchNumber,
                ["p_PartID"] = partId,
                ["p_Operation"] = operation,
                ["p_TransferQuantity"] = transferQuantity,
                ["p_OriginalQuantity"] = originalQuantity,
                ["p_NewLocation"] = newLocation,
                ["p_User"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_transfer_quantity",
                parameters,
                null // No progress helper for this method
            );

            // FIXED: Do NOT call FixBatchNumbersAsync() after quantity transfer operations
            // This was causing unwanted consolidation of separate transactions into single rows
            // FixBatchNumbers should only be called for maintenance operations, not regular transactions
            // await FixBatchNumbersAsync();
            
            if (result.IsSuccess)
            {
                return DaoResult.Success($"Transferred {transferQuantity} of part {partId} to {newLocation}");
            }
            else
            {
                return DaoResult.Failure($"Failed to transfer quantity for part {partId}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "TransferInventoryQuantityAsync");
            return DaoResult.Failure($"Failed to transfer quantity for part {partId}", ex);
        }
    }

    public static async Task<DaoResult> FixBatchNumbersAsync()
    {
        try
        {
            // MIGRATED: Use Helper_Database_StoredProcedure for proper status handling
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_inventory_Fix_BatchNumbers",
                new Dictionary<string, object>(), // No parameters needed
                null // No progress helper for this method
            );

            if (result.IsSuccess)
            {
                return DaoResult.Success("Batch numbers fixed successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to fix batch numbers: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "FixBatchNumbersAsync");
            return DaoResult.Failure("Failed to fix batch numbers", ex);
        }
    }

    #endregion

}

#endregion
