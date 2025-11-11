using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_ItemType

internal static class Dao_ItemType
{
    #region Delete

    internal static async Task<Model_Dao_Result> DeleteItemType(string itemType,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new() { ["ItemType"] = itemType }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_item_types_Delete_ByType",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Item type {itemType} deleted successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to delete item type {itemType}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "DeleteItemType");
            return Model_Dao_Result.Failure($"Error deleting item type {itemType}", ex);
        }
    }

    internal static async Task<Model_Dao_Result> InsertItemType(string itemType, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["ItemType"] = itemType,  // p_ prefix added automatically
                ["IssuedBy"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_item_types_Add_ItemType",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Item type {itemType} created successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to create item type {itemType}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "InsertItemType");
            return Model_Dao_Result.Failure($"Error creating item type {itemType}", ex);
        }
    }

    #endregion

    #region Update

    internal static async Task<Model_Dao_Result> UpdateItemType(int id, string newItemType, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["ID"] = id,                    // p_ prefix added automatically
                ["ItemType"] = newItemType,
                ["IssuedBy"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_item_types_Update_ItemType",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Item type updated to {newItemType}");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to update item type: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "UpdateItemType");
            return Model_Dao_Result.Failure("Error updating item type", ex);
        }
    }

    internal static async Task<Model_Dao_Result<DataTable>> GetAllItemTypes(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_item_types_Get_All",
                null, // No parameters needed
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                return Model_Dao_Result<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} item types");
            }
            else
            {
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve item types: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetAllItemTypes");
            return Model_Dao_Result<DataTable>.Failure("Error retrieving item types", ex);
        }
    }

    internal static async Task<Model_Dao_Result<DataRow>> GetItemTypeByName(string itemType, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var allItemTypesResult = await GetAllItemTypes(connection, transaction);
            if (!allItemTypesResult.IsSuccess)
            {
                return Model_Dao_Result<DataRow>.Failure(allItemTypesResult.ErrorMessage, allItemTypesResult.Exception);
            }

            var table = allItemTypesResult.Data!;
            var rows = table.Select($"ItemType = '{itemType.Replace("'", "''")}'");

            if (rows.Length > 0)
            {
                return Model_Dao_Result<DataRow>.Success(rows[0], $"Found item type {itemType}");
            }

            return Model_Dao_Result<DataRow>.Failure($"Item type {itemType} not found");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<DataRow>.Failure($"Error retrieving item type {itemType}", ex);
        }
    }

    #endregion

    #region Existence Check

    internal static async Task<Model_Dao_Result<bool>> ItemTypeExists(string itemType, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new() { ["ItemType"] = itemType }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_item_types_Exists_ByType",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                bool exists = Convert.ToInt32(result.Data) > 0;
                return Model_Dao_Result<bool>.Success(exists, exists ? $"Item type {itemType} exists" : $"Item type {itemType} does not exist");
            }
            else
            {
                return Model_Dao_Result<bool>.Failure($"Failed to check item type {itemType}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "ItemTypeExists");
            return Model_Dao_Result<bool>.Failure($"Error checking item type {itemType}", ex);
        }
    }

    #endregion
}

#endregion
