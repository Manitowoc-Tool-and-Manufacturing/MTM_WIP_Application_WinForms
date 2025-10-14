using System.Threading.Tasks;
using System.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;

namespace MTM_Inventory_Application.Data
{
    public static class Dao_QuickButtons
    {
        #region Quick Button Methods

        public static async Task UpdateQuickButtonAsync(string user, int position, string partId, string operation, int quantity)
        {
            try
            {
                // Use the existing stored procedure: sys_last_10_transactions_Update_ByUserAndPosition_1
                // Note: position is already 1-based from the UI (1-10)
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["Position"] = position,       // Use position as-is (1-based)
                    ["p_PartID"] = partId,
                    ["p_Operation"] = operation,
                    ["Quantity"] = quantity
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Update_ByUserAndPosition_1",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"UpdateQuickButtonAsync failed: {result.ErrorMessage}");
                }
                else
                {
                    LoggingUtility.Log($"UpdateQuickButtonAsync succeeded: Updated position {position} with {partId} Op:{operation} Qty:{quantity} for user {user}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "UpdateQuickButtonAsync");
            }
        }

        public static async Task RemoveQuickButtonAndShiftAsync(string user, int position)
        {
            try
            {
                // Ensure position is always 1-10 (never 0)
                int safePosition = Math.Max(1, Math.Min(10, position + 1));
                
                // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["Position"] = safePosition
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_RemoveAndShift_ByUser",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"RemoveQuickButtonAndShiftAsync failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "RemoveQuickButtonAndShiftAsync");
            }
        }

        public static async Task AddQuickButtonAsync(string user, string partId, string operation, int quantity, int position)
        {
            try
            {
                // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["p_PartID"] = partId,
                    ["p_Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = position
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Add_AtPosition",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"AddQuickButtonAsync failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "AddQuickButtonAsync");
            }
        }

        public static async Task MoveQuickButtonAsync(string user, int fromPosition, int toPosition)
        {
            try
            {
                // Ensure positions are always 1-10 (never 0)
                int safeFrom = Math.Max(1, Math.Min(10, fromPosition + 1));
                int safeTo = Math.Max(1, Math.Min(10, toPosition + 1));
                
                // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["FromPosition"] = safeFrom,
                    ["ToPosition"] = safeTo
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Move",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"MoveQuickButtonAsync failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "MoveQuickButtonAsync");
            }
        }

        public static async Task DeleteAllQuickButtonsForUserAsync(string user)
        {
            try
            {
                // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user                // p_ prefix added automatically
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_DeleteAll_ByUser",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"DeleteAllQuickButtonsForUserAsync failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "DeleteAllQuickButtonsForUserAsync");
            }
        }

        public static async Task AddOrShiftQuickButtonAsync(string user, string partId, string operation, int quantity)
        {
            try
            {
                // Use the existing stored procedure: sys_last_10_transactions_AddQuickButton_1
                // This adds a new QuickButton at position 1 (top of the list)
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["p_PartID"] = partId,
                    ["p_Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = 1               // Always add to position 1 (most recent)
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_AddQuickButton_1",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"AddOrShiftQuickButtonAsync failed: {result.ErrorMessage}");
                }
                else
                {
                    LoggingUtility.Log($"AddOrShiftQuickButtonAsync succeeded: Added {partId} Op:{operation} Qty:{quantity} for user {user}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "AddOrShiftQuickButtonAsync");
            }
        }

        public static async Task RemoveAndShiftQuickButtonAsync(string user, int position)
        {
            try
            {
                int safePosition = Math.Max(1, Math.Min(10, position + 1));
                
                // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["Position"] = safePosition
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Delete_ByUserAndPosition_1",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"RemoveAndShiftQuickButtonAsync failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "RemoveAndShiftQuickButtonAsync");
            }
        }

        public static async Task AddQuickButtonAtPositionAsync(string user, string partId, string operation, int quantity, int position)
        {
            try
            {
                // MIGRATED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                Dictionary<string, object> parameters = new()
                {
                    ["p_User"] = user,               // p_ prefix added automatically
                    ["p_PartID"] = partId,
                    ["p_Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = position
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                    Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_AddQuickButton_1",
                    parameters,
                    null, // No progress helper for this method
                    true  // Use async
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"AddQuickButtonAtPositionAsync failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "AddQuickButtonAtPositionAsync");
            }
        }

        #endregion
    }
}
