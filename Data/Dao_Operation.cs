using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_Operation

internal static class Dao_Operation
{
    #region Delete

    internal static async Task<DaoResult> DeleteOperation(string operationNumber,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object> { ["p_Operation"] = operationNumber }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "md_operation_numbers_Delete_ByOperation",
                parameters,
                null // No progress helper for this method
            );

            if (result.IsSuccess)
            {
                return DaoResult.Success($"Operation {operationNumber} deleted successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to delete operation {operationNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "DeleteOperation");
            return DaoResult.Failure($"Error deleting operation {operationNumber}", ex);
        }
    }

    #endregion

    #region Insert

    internal static async Task<DaoResult> InsertOperation(string operationNumber, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["p_Operation"] = operationNumber,   // p_ prefix added automatically
                ["IssuedBy"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "md_operation_numbers_Add_Operation",
                parameters,
                null // No progress helper for this method
            );

            if (result.IsSuccess)
            {
                return DaoResult.Success($"Operation {operationNumber} created successfully");
            }
            else
            {
                return DaoResult.Failure($"Failed to create operation {operationNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "InsertOperation");
            return DaoResult.Failure($"Error creating operation {operationNumber}", ex);
        }
    }

    #endregion

    #region Update

    internal static async Task<DaoResult> UpdateOperation(string oldOperation, string newOperationNumber, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["p_Operation"] = oldOperation,        // p_ prefix added automatically
                ["NewOperation"] = newOperationNumber,
                ["IssuedBy"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "md_operation_numbers_Update_Operation",
                parameters,
                null // No progress helper for this method
            );

            if (result.IsSuccess)
            {
                return DaoResult.Success($"Operation updated from {oldOperation} to {newOperationNumber}");
            }
            else
            {
                return DaoResult.Failure($"Failed to update operation {oldOperation}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "UpdateOperation");
            return DaoResult.Failure($"Error updating operation {oldOperation}", ex);
        }
    }

    #endregion

    #region Read

    internal static async Task<DaoResult<DataTable>> GetAllOperations(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "md_operation_numbers_Get_All",
                null, // No parameters needed
                null // No progress helper for this method
            );

            if (result.IsSuccess && result.Data != null)
            {
                return DaoResult<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} operations");
            }
            else
            {
                return DaoResult<DataTable>.Failure($"Failed to retrieve operations: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "GetAllOperations");
            return DaoResult<DataTable>.Failure("Error retrieving operations", ex);
        }
    }

    internal static async Task<DaoResult<DataRow>> GetOperationByNumber(string operationNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var allOperationsResult = await GetAllOperations(connection, transaction);
            if (!allOperationsResult.IsSuccess)
            {
                return DaoResult<DataRow>.Failure(allOperationsResult.ErrorMessage, allOperationsResult.Exception);
            }

            var table = allOperationsResult.Data!;
            var rows = table.Select($"Operation = '{operationNumber.Replace("'", "''")}'");

            if (rows.Length > 0)
            {
                return DaoResult<DataRow>.Success(rows[0], $"Found operation {operationNumber}");
            }

            return DaoResult<DataRow>.Failure($"Operation {operationNumber} not found");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return DaoResult<DataRow>.Failure($"Error retrieving operation {operationNumber}", ex);
        }
    }

    internal static async Task<DaoResult<bool>> OperationExists(string operationNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object> { ["p_Operation"] = operationNumber }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "md_operation_numbers_Exists_ByOperation",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                bool exists = Convert.ToInt32(result.Data) > 0;
                return DaoResult<bool>.Success(exists, exists ? $"Operation {operationNumber} exists" : $"Operation {operationNumber} does not exist");
            }
            else
            {
                return DaoResult<bool>.Failure($"Failed to check operation {operationNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "OperationExists");
            return DaoResult<bool>.Failure($"Error checking operation {operationNumber}", ex);
        }
    }

    #endregion
}

#endregion

