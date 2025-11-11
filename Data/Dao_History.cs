using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_History

internal class Dao_History
{
    #region History Methods

    public static async Task<Model_Dao_Result> AddTransactionHistoryAsync(Model_Transactions_History history,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            // MIGRATED: Use Helper_Database_StoredProcedure with automatic parameter prefix detection
            Dictionary<string, object> parameters = new()
            {
                ["TransactionType"] = history.TransactionType,      // Automatic prefix detection
                ["PartID"] = history.PartId,
                ["FromLocation"] = history.FromLocation ?? (object)DBNull.Value,
                ["ToLocation"] = history.ToLocation ?? (object)DBNull.Value,
                ["Operation"] = history.Operation ?? (object)DBNull.Value,
                ["Quantity"] = history.Quantity,
                ["Notes"] = history.Notes ?? (object)DBNull.Value,
                ["User"] = history.User,
                ["ItemType"] = history.ItemType ?? (object)DBNull.Value,
                ["BatchNumber"] = history.BatchNumber ?? (object)DBNull.Value,
                ["ReceiveDate"] = history.DateTime
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "inv_transaction_Add",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"AddTransactionHistoryAsync failed: {result.ErrorMessage}");
                return Model_Dao_Result.Failure(result.ErrorMessage ?? "Failed to add transaction history", result.Exception);
            }

            return Model_Dao_Result.Success();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            var errorResult = await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "AddTransactionHistoryAsync");
            return Model_Dao_Result.Failure("Failed to add transaction history", ex);
        }
    }

    #endregion
}

#endregion
