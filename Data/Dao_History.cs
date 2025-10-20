using System.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Logging;
using MySql.Data.MySqlClient;

namespace MTM_Inventory_Application.Data;

#region Dao_History

internal class Dao_History
{
    #region History Methods

    public static async Task<DaoResult> AddTransactionHistoryAsync(Model_TransactionHistory history,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            // MIGRATED: Use Helper_Database_StoredProcedure with explicit p_ prefix
            Dictionary<string, object> parameters = new()
            {
                ["p_TransactionType"] = history.TransactionType,      // Explicit p_ prefix for transaction procedures
                ["p_PartID"] = history.PartId,
                ["p_FromLocation"] = history.FromLocation ?? (object)DBNull.Value,
                ["p_ToLocation"] = history.ToLocation ?? (object)DBNull.Value,
                ["p_Operation"] = history.Operation ?? (object)DBNull.Value,
                ["p_Quantity"] = history.Quantity,
                ["p_Notes"] = history.Notes ?? (object)DBNull.Value,
                ["p_User"] = history.User,
                ["p_ItemType"] = history.ItemType ?? (object)DBNull.Value,
                ["p_BatchNumber"] = history.BatchNumber ?? (object)DBNull.Value,
                ["p_ReceiveDate"] = history.DateTime
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "inv_transaction_Add",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"AddTransactionHistoryAsync failed: {result.ErrorMessage}");
                return DaoResult.Failure(result.ErrorMessage ?? "Failed to add transaction history", result.Exception);
            }

            return DaoResult.Success();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            var errorResult = await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "AddTransactionHistoryAsync");
            return DaoResult.Failure("Failed to add transaction history", ex);
        }
    }

    #endregion
}

#endregion
