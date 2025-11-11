using System.Threading.Tasks;
using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data
{
    public static class Dao_QuickButtons
    {
        #region Quick Button Methods

        public static async Task<Model_Dao_Result> UpdateQuickButtonAsync(
            string user,
            int position,
            string partId,
            string operation,
            int quantity,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                // Use the existing stored procedure: sys_last_10_transactions_Update_ByUserAndPosition_1
                // Note: position is already 1-based from the UI (1-10)
                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["Position"] = position,
                    ["PartID"] = partId,
                    ["Operation"] = operation,
                    ["Quantity"] = quantity
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_Update_ByUserAndPosition",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] UpdateQuickButtonAsync failed: {result.ErrorMessage}");
                    return Model_Dao_Result.Failure($"Failed to update quick button: {result.ErrorMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] UpdateQuickButtonAsync succeeded: Position {position} → {partId}+Op:{operation} Qty:{quantity} for user {user}");
                
                // CRITICAL: Always cleanup after update to ensure no duplicates or gaps
                await CleanupGapsAndDuplicatesAsync(user, connectionString, connection, transaction);
                
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error updating quick button at position {position}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error updating quick button at position {position}: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> RemoveQuickButtonAndShiftAsync(
            string user,
            int position,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                // Note: position is already 1-based from the UI (1-10)
                int safePosition = Math.Max(1, Math.Min(10, position));

                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["Position"] = safePosition
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_RemoveAndShift_ByUser",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] RemoveQuickButtonAndShiftAsync failed: {result.StatusMessage}");
                    return Model_Dao_Result.Failure($"Failed to remove quick button: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] RemoveQuickButtonAndShiftAsync succeeded: Removed position {safePosition} for user {user}");
                
                // CRITICAL: Always cleanup after remove to ensure no gaps
                await CleanupGapsAndDuplicatesAsync(user, connectionString, connection, transaction);
                
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error removing quick button at position {position}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error removing quick button at position {position}: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> AddQuickButtonAsync(
            string user,
            string partId,
            string operation,
            int quantity,
            int position,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["PartID"] = partId,
                    ["Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = position
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_Add_AtPosition",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] AddQuickButtonAsync failed: {result.StatusMessage}");
                    return Model_Dao_Result.Failure($"Failed to add quick button: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] AddQuickButtonAsync succeeded: Added {partId}+Op:{operation} at position {position} for user {user}");
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error adding quick button at position {position}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error adding quick button at position {position}: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> MoveQuickButtonAsync(
            string user,
            int fromPosition,
            int toPosition,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                // Note: positions are already 1-based from the UI (1-10)
                // Clamp to valid range 1-10
                int safeFrom = Math.Max(1, Math.Min(10, fromPosition));
                int safeTo = Math.Max(1, Math.Min(10, toPosition));

                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["FromPosition"] = safeFrom,
                    ["ToPosition"] = safeTo
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_Move",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] MoveQuickButtonAsync failed: {result.StatusMessage}");
                    return Model_Dao_Result.Failure($"Failed to move quick button: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] MoveQuickButtonAsync succeeded: Moved from {safeFrom} to {safeTo} for user {user}");
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error moving quick button from {fromPosition} to {toPosition}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error moving quick button: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> DeleteAllQuickButtonsForUserAsync(
            string user,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_DeleteAll_ByUser",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] DeleteAllQuickButtonsForUserAsync failed: {result.StatusMessage}");
                    return Model_Dao_Result.Failure($"Failed to delete all quick buttons: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] DeleteAllQuickButtonsForUserAsync succeeded for user {user}");
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error deleting all quick buttons for user {user}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error deleting all quick buttons: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> AddOrShiftQuickButtonAsync(
            string user,
            string partId,
            string operation,
            int quantity,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                LoggingUtility.Log($"[Dao_QuickButtons] ═══════════════════════════════════════════════════════");
                LoggingUtility.Log($"[Dao_QuickButtons] AddOrShiftQuickButtonAsync STARTED");
                LoggingUtility.Log($"[Dao_QuickButtons]   User: {user}");
                LoggingUtility.Log($"[Dao_QuickButtons]   PartID: {partId}");
                LoggingUtility.Log($"[Dao_QuickButtons]   Operation: {operation}");
                LoggingUtility.Log($"[Dao_QuickButtons]   Quantity: {quantity}");
                LoggingUtility.Log($"[Dao_QuickButtons] ═══════════════════════════════════════════════════════");
                
                // STEP 1: Get current buttons to check for duplicates
                LoggingUtility.Log($"[Dao_QuickButtons] STEP 1: Checking for existing duplicate");
                var existingButtonsResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_Get_ByUser",
                    new Dictionary<string, object> { ["User"] = user },
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );
                
                if (!existingButtonsResult.IsSuccess || existingButtonsResult.Data == null)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] STEP 1: Failed to get existing buttons or no data: {existingButtonsResult.ErrorMessage}");
                }
                else
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] STEP 1: Retrieved {existingButtonsResult.Data.Rows.Count} existing buttons");
                    int currentPosition = 1;
                    foreach (DataRow row in existingButtonsResult.Data.Rows)
                    {
                        string existingPartId = row["PartID"]?.ToString() ?? "";
                        string existingOperation = row["Operation"]?.ToString() ?? "";
                        int existingQuantity = row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0;
                        
                        LoggingUtility.Log($"[Dao_QuickButtons]   Position {currentPosition}: {existingPartId} + Op:{existingOperation} (Qty: {existingQuantity})");
                        
                        // Check if this PartID AND Operation combination already exists
                        if (string.Equals(existingPartId, partId, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(existingOperation, operation, StringComparison.OrdinalIgnoreCase))
                        {
                            LoggingUtility.Log($"[Dao_QuickButtons] ╔════════════════════════════════════════════════════╗");
                            LoggingUtility.Log($"[Dao_QuickButtons] ║ DUPLICATE FOUND at position {currentPosition}");
                            LoggingUtility.Log($"[Dao_QuickButtons] ║ PartID '{partId}' + Operation '{operation}' already exists");
                            LoggingUtility.Log($"[Dao_QuickButtons] ║ ACTION: DO NOTHING - Return success without changes");
                            LoggingUtility.Log($"[Dao_QuickButtons] ╚════════════════════════════════════════════════════╝");
                            LoggingUtility.Log($"[Dao_QuickButtons] AddOrShiftQuickButtonAsync COMPLETED (duplicate skipped)");
                            LoggingUtility.Log($"[Dao_QuickButtons] ═══════════════════════════════════════════════════════");
                            return Model_Dao_Result.Success("Quick button already exists - no changes made");
                        }
                        currentPosition++;
                    }
                    LoggingUtility.Log($"[Dao_QuickButtons] STEP 1: No duplicate found - proceeding with add");
                }

                // STEP 2: Add at position 1 (shifts all others down)
                LoggingUtility.Log($"[Dao_QuickButtons] STEP 2: Adding new button at position 1");
                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["PartID"] = partId,
                    ["Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = 1
                };

                LoggingUtility.Log($"[Dao_QuickButtons] STEP 2: Calling sys_last_10_transactions_AddQuickButton");
                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_AddQuickButton",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] ╔════════════════════════════════════════════════════╗");
                    LoggingUtility.Log($"[Dao_QuickButtons] ║ STEP 2: ADD FAILED");
                    LoggingUtility.Log($"[Dao_QuickButtons] ║ Error: {result.StatusMessage}");
                    LoggingUtility.Log($"[Dao_QuickButtons] ╚════════════════════════════════════════════════════╝");
                    return Model_Dao_Result.Failure($"Failed to add quick button: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] STEP 2: Add succeeded - button inserted at position 1");
                
                LoggingUtility.Log($"[Dao_QuickButtons] ╔════════════════════════════════════════════════════╗");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ SUCCESS: New button added at position 1");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ {partId} + Op:{operation} (Qty: {quantity})");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ NOTE: Cleanup skipped - will run on next load");
                LoggingUtility.Log($"[Dao_QuickButtons] ╚════════════════════════════════════════════════════╝");
                LoggingUtility.Log($"[Dao_QuickButtons] AddOrShiftQuickButtonAsync COMPLETED (new button added)");
                LoggingUtility.Log($"[Dao_QuickButtons] ═══════════════════════════════════════════════════════");
                
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.Log($"[Dao_QuickButtons] ╔════════════════════════════════════════════════════╗");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ DATABASE ERROR");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ {ex.Message}");
                LoggingUtility.Log($"[Dao_QuickButtons] ╚════════════════════════════════════════════════════╝");
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error adding/shifting quick button: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.Log($"[Dao_QuickButtons] ╔════════════════════════════════════════════════════╗");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ UNEXPECTED ERROR");
                LoggingUtility.Log($"[Dao_QuickButtons] ║ {ex.Message}");
                LoggingUtility.Log($"[Dao_QuickButtons] ╚════════════════════════════════════════════════════╝");
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error adding/shifting quick button: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> RemoveAndShiftQuickButtonAsync(
            string user,
            int position,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                int safePosition = Math.Max(1, Math.Min(10, position + 1));

                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["Position"] = safePosition
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_Delete_ByUserAndPosition",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] RemoveAndShiftQuickButtonAsync failed: {result.StatusMessage}");
                    return Model_Dao_Result.Failure($"Failed to remove/shift quick button: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] RemoveAndShiftQuickButtonAsync succeeded: Removed position {position} for user {user}");
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error removing/shifting quick button at position {position}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error removing/shifting quick button: {ex.Message}", ex);
            }
        }

        public static async Task<Model_Dao_Result> AddQuickButtonAtPositionAsync(
            string user,
            string partId,
            string operation,
            int quantity,
            int position,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            try
            {
                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["PartID"] = partId,
                    ["Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = position
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_AddQuickButton",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] AddQuickButtonAtPositionAsync failed: {result.StatusMessage}");
                    return Model_Dao_Result.Failure($"Failed to add quick button at position: {result.StatusMessage}", result.Exception);
                }

                LoggingUtility.Log($"[Dao_QuickButtons] AddQuickButtonAtPositionAsync succeeded: Added {partId}+Op:{operation} at position {position} for user {user}");
                return Model_Dao_Result.Success();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error adding quick button at position {position}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error adding quick button at position {position}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cleans up quick buttons by removing duplicates and eliminating gaps.
        /// Workflow: 1) Pull data to array, 2) Remove duplicates/restructure, 3) Delete all, 4) Recreate from array, 5) Dispose
        /// </summary>
        public static async Task<Model_Dao_Result> CleanupGapsAndDuplicatesAsync(
            string user,
            string? connectionString = null,
            MySqlConnection? connection = null,
            MySqlTransaction? transaction = null)
        {
            List<(string PartId, string Operation, int Quantity)>? cleanButtonsArray = null;
            
            try
            {
                // STEP 1: Pull current button data from database to array
                LoggingUtility.Log($"[Dao_QuickButtons] STEP 1: Pulling current button data for user {user}");
                var buttonsResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_Get_ByUser",
                    new Dictionary<string, object> { ["User"] = user },
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!buttonsResult.IsSuccess || buttonsResult.Data == null || buttonsResult.Data.Rows.Count == 0)
                {
                    LoggingUtility.Log($"[Dao_QuickButtons] No buttons found for user {user} - cleanup complete");
                    return Model_Dao_Result.Success("No buttons to clean");
                }

                // STEP 2: Remove duplicates from array and restructure/reorder
                LoggingUtility.Log($"[Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array");
                cleanButtonsArray = new List<(string PartId, string Operation, int Quantity)>();
                var seenCombinations = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                int duplicatesRemoved = 0;

                foreach (DataRow row in buttonsResult.Data.Rows)
                {
                    string partId = row["PartID"]?.ToString() ?? "";
                    string operation = row["Operation"]?.ToString() ?? "";
                    int quantity = row["Quantity"] != DBNull.Value ? Convert.ToInt32(row["Quantity"]) : 0;

                    string combination = $"{partId}|{operation}";

                    if (seenCombinations.Add(combination))
                    {
                        // Not a duplicate - add to clean array
                        cleanButtonsArray.Add((partId, operation, quantity));
                        LoggingUtility.Log($"[Dao_QuickButtons] Added to array: {partId} + {operation} (Qty: {quantity})");
                    }
                    else
                    {
                        // Duplicate found - skip it
                        duplicatesRemoved++;
                        LoggingUtility.Log($"[Dao_QuickButtons] Skipping duplicate: {partId} + {operation}");
                    }
                }

                LoggingUtility.Log($"[Dao_QuickButtons] Array restructured: {cleanButtonsArray.Count} unique buttons, {duplicatesRemoved} duplicates removed");

                // STEP 3: Delete ALL buttons for this user at once (avoid position shifting issues)
                LoggingUtility.Log($"[Dao_QuickButtons] STEP 3: Deleting ALL buttons from database");
                await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_Application_Variables.ConnectionString,
                    "sys_last_10_transactions_DeleteAll_ByUser",
                    new Dictionary<string, object> { ["User"] = user },
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );
                LoggingUtility.Log($"[Dao_QuickButtons] All buttons deleted from database");

                // STEP 4: Use array data to create new buttons in database (consecutive positions 1,2,3...)
                LoggingUtility.Log($"[Dao_QuickButtons] STEP 4: Creating new buttons from array data");
                int newPosition = 1;
                foreach (var button in cleanButtonsArray)
                {
                    if (newPosition > 10) 
                    {
                        LoggingUtility.Log($"[Dao_QuickButtons] Max 10 buttons limit reached - stopping");
                        break;
                    }

                    var insertResult = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                        connectionString ?? Model_Application_Variables.ConnectionString,
                        "sys_last_10_transactions_Insert",
                        new Dictionary<string, object>
                        {
                            ["User"] = user,
                            ["PartID"] = button.PartId,
                            ["Operation"] = button.Operation,
                            ["Quantity"] = button.Quantity,
                            ["Position"] = newPosition
                        },
                        progressHelper: null,
                        connection: connection,
                        transaction: transaction
                    );

                    if (!insertResult.IsSuccess)
                    {
                        LoggingUtility.Log($"[Dao_QuickButtons] Failed to insert button at position {newPosition}: {insertResult.StatusMessage}");
                    }
                    else
                    {
                        LoggingUtility.Log($"[Dao_QuickButtons] Created button at position {newPosition}: {button.PartId} + {button.Operation} (Qty: {button.Quantity})");
                    }

                    newPosition++;
                }

                LoggingUtility.Log($"[Dao_QuickButtons] Created {newPosition - 1} buttons in database");

                // STEP 5: Dispose array (will happen in finally block)
                string summary = $"Cleanup complete: {duplicatesRemoved} duplicates removed, {cleanButtonsArray.Count} buttons remain";
                LoggingUtility.Log($"[Dao_QuickButtons] {summary}");
                return Model_Dao_Result.Success(summary);
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure($"Database error during cleanup for user {user}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure($"Unexpected error during cleanup: {ex.Message}", ex);
            }
            finally
            {
                // STEP 5: Dispose array
                if (cleanButtonsArray != null)
                {
                    cleanButtonsArray.Clear();
                    cleanButtonsArray = null;
                    LoggingUtility.Log($"[Dao_QuickButtons] STEP 5: Array disposed");
                }
            }
        }

        #endregion
    }
}
