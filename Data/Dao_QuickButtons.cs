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

        public static async Task<DaoResult> UpdateQuickButtonAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Update_ByUserAndPosition",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    string detailedError = $"Status: {result.ErrorMessage}, Exception: {result.Exception?.Message ?? "none"}";
                    LoggingUtility.Log($"UpdateQuickButtonAsync failed: {detailedError}");
                    Console.WriteLine($"[DEBUG] UpdateQuickButtonAsync failure details: {detailedError}");
                    return DaoResult.Failure($"Failed to update quick button: {result.ErrorMessage}");
                }
                
                LoggingUtility.Log($"UpdateQuickButtonAsync succeeded: Updated position {position} with {partId} Op:{operation} Qty:{quantity} for user {user}");
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "UpdateQuickButtonAsync");
                return DaoResult.Failure($"Exception updating quick button: {ex.Message}");
            }
        }

        public static async Task<DaoResult> RemoveQuickButtonAndShiftAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_RemoveAndShift_ByUser",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"RemoveQuickButtonAndShiftAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to remove quick button: {result.StatusMessage}");
                }
                
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "RemoveQuickButtonAndShiftAsync");
                return DaoResult.Failure($"Exception removing quick button: {ex.Message}");
            }
        }

        public static async Task<DaoResult> AddQuickButtonAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Add_AtPosition",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"AddQuickButtonAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to add quick button: {result.StatusMessage}");
                }
                
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "AddQuickButtonAsync");
                return DaoResult.Failure($"Exception adding quick button: {ex.Message}");
            }
        }

        public static async Task<DaoResult> MoveQuickButtonAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Move",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"MoveQuickButtonAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to move quick button: {result.StatusMessage}");
                }
                
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "MoveQuickButtonAsync");
                return DaoResult.Failure($"Exception moving quick button: {ex.Message}");
            }
        }

        public static async Task<DaoResult> DeleteAllQuickButtonsForUserAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_DeleteAll_ByUser",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"DeleteAllQuickButtonsForUserAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to delete all quick buttons: {result.StatusMessage}");
                }
                
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "DeleteAllQuickButtonsForUserAsync");
                return DaoResult.Failure($"Exception deleting all quick buttons: {ex.Message}");
            }
        }

        public static async Task<DaoResult> AddOrShiftQuickButtonAsync(
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
                // Use the existing stored procedure: sys_last_10_transactions_AddQuickButton
                // This adds a new QuickButton at position 1 (top of the list)
                Dictionary<string, object> parameters = new()
                {
                    ["User"] = user,
                    ["PartID"] = partId,
                    ["Operation"] = operation,
                    ["Quantity"] = quantity,
                    ["Position"] = 1  // Always add to position 1 (most recent)
                };

                var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_AddQuickButton",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"AddOrShiftQuickButtonAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to add/shift quick button: {result.StatusMessage}");
                }
                
                LoggingUtility.Log($"AddOrShiftQuickButtonAsync succeeded: Added {partId} Op:{operation} Qty:{quantity} for user {user}");
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "AddOrShiftQuickButtonAsync");
                return DaoResult.Failure($"Exception adding/shifting quick button: {ex.Message}");
            }
        }

        public static async Task<DaoResult> RemoveAndShiftQuickButtonAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_Delete_ByUserAndPosition",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"RemoveAndShiftQuickButtonAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to remove/shift quick button: {result.StatusMessage}");
                }
                
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "RemoveAndShiftQuickButtonAsync");
                return DaoResult.Failure($"Exception removing/shifting quick button: {ex.Message}");
            }
        }

        public static async Task<DaoResult> AddQuickButtonAtPositionAsync(
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
                    connectionString ?? Model_AppVariables.ConnectionString,
                    "sys_last_10_transactions_AddQuickButton",
                    parameters,
                    progressHelper: null,
                    connection: connection,
                    transaction: transaction
                );

                if (!result.IsSuccess)
                {
                    LoggingUtility.Log($"AddQuickButtonAtPositionAsync failed: {result.StatusMessage}");
                    return DaoResult.Failure($"Failed to add quick button at position: {result.StatusMessage}");
                }
                
                return DaoResult.Success();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "AddQuickButtonAtPositionAsync");
                return DaoResult.Failure($"Exception adding quick button at position: {ex.Message}");
            }
        }

        #endregion
    }
}
