using System.Data;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Logging;

namespace MTM_Inventory_Application.Data;

#region Dao_History

internal class Dao_History
{
    #region History Methods

    public static async Task AddTransactionHistoryAsync(Model_TransactionHistory history)
    {
        try
        {
            // MIGRATED: Use Helper_Database_StoredProcedure with explicit in_ prefix
            Dictionary<string, object> parameters = new()
            {
                ["in_TransactionType"] = history.TransactionType,      // Explicit in_ prefix for transaction procedures
                ["in_PartID"] = history.PartId,
                ["in_FromLocation"] = history.FromLocation ?? (object)DBNull.Value,
                ["in_ToLocation"] = history.ToLocation ?? (object)DBNull.Value,
                ["in_Operation"] = history.Operation ?? (object)DBNull.Value,
                ["in_Quantity"] = history.Quantity,
                ["in_Notes"] = history.Notes ?? (object)DBNull.Value,
                ["in_User"] = history.User,
                ["in_ItemType"] = history.ItemType ?? (object)DBNull.Value,
                ["in_BatchNumber"] = history.BatchNumber ?? (object)DBNull.Value,
                ["in_ReceiveDate"] = history.DateTime
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatus(
                Model_AppVariables.ConnectionString,
                "inv_transaction_Add",
                parameters,
                null, // No progress helper for this method
                true  // Use async
            );

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"AddTransactionHistoryAsync failed: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, true, "AddTransactionHistoryAsync");
        }
    }

    #endregion
}

#endregion
