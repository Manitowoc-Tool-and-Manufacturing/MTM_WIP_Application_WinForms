using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data;

#region Dao_Operation

internal static class Dao_Operation
{
    #region Delete

    internal static async Task<Model_Dao_Result> DeleteOperation(string operationNumber,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object> { ["Operation"] = operationNumber }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Delete_ByOperation",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Operation {operationNumber} deleted successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to delete operation {operationNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "DeleteOperation");
            return Model_Dao_Result.Failure($"Error deleting operation {operationNumber}", ex);
        }
    }

    #endregion

    #region Insert

    internal static async Task<Model_Dao_Result> InsertOperation(string operationNumber, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["Operation"] = operationNumber,   // p_ prefix added automatically
                ["IssuedBy"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Add_Operation",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Operation {operationNumber} created successfully");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to create operation {operationNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "InsertOperation");
            return Model_Dao_Result.Failure($"Error creating operation {operationNumber}", ex);
        }
    }

    #endregion

    #region Update

    internal static async Task<Model_Dao_Result> UpdateOperation(string oldOperation, string newOperationNumber, string user,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object>
            {
                ["Operation"] = oldOperation,        // p_ prefix added automatically
                ["NewOperation"] = newOperationNumber,
                ["IssuedBy"] = user
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Update_Operation",
                parameters,
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess)
            {
                return Model_Dao_Result.Success($"Operation updated from {oldOperation} to {newOperationNumber}");
            }
            else
            {
                return Model_Dao_Result.Failure($"Failed to update operation {oldOperation}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "UpdateOperation");
            return Model_Dao_Result.Failure($"Error updating operation {oldOperation}", ex);
        }
    }

    #endregion

    #region Read

    internal static async Task<Model_Dao_Result<DataTable>> GetAllOperations(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Get_All",
                null, // No parameters needed
                null, // No progress helper for this method
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                return Model_Dao_Result<DataTable>.Success(result.Data, $"Retrieved {result.Data.Rows.Count} operations");
            }
            else
            {
                return Model_Dao_Result<DataTable>.Failure($"Failed to retrieve operations: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "GetAllOperations");
            return Model_Dao_Result<DataTable>.Failure("Error retrieving operations", ex);
        }
    }

    internal static async Task<Model_Dao_Result<DataRow>> GetOperationByNumber(string operationNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var allOperationsResult = await GetAllOperations(connection, transaction);
            if (!allOperationsResult.IsSuccess)
            {
                return Model_Dao_Result<DataRow>.Failure(allOperationsResult.ErrorMessage, allOperationsResult.Exception);
            }

            var table = allOperationsResult.Data!;
            var rows = table.Select($"Operation = '{operationNumber.Replace("'", "''")}'");

            if (rows.Length > 0)
            {
                return Model_Dao_Result<DataRow>.Success(rows[0], $"Found operation {operationNumber}");
            }

            return Model_Dao_Result<DataRow>.Failure($"Operation {operationNumber} not found");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return Model_Dao_Result<DataRow>.Failure($"Error retrieving operation {operationNumber}", ex);
        }
    }

    internal static async Task<Model_Dao_Result<bool>> OperationExists(string operationNumber, MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        try
        {
            var parameters = new Dictionary<string, object> { ["Operation"] = operationNumber }; // p_ prefix added automatically

            var result = await Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Exists_ByOperation",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (result.IsSuccess && result.Data != null)
            {
                bool exists = Convert.ToInt32(result.Data) > 0;
                return Model_Dao_Result<bool>.Success(exists, exists ? $"Operation {operationNumber} exists" : $"Operation {operationNumber} does not exist");
            }
            else
            {
                return Model_Dao_Result<bool>.Failure($"Failed to check operation {operationNumber}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogDatabaseError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: "OperationExists");
            return Model_Dao_Result<bool>.Failure($"Error checking operation {operationNumber}", ex);
        }
    }

    #endregion
}

#endregion

