using System.Data;
using System.Diagnostics;
using MTM_Inventory_Application.Forms.MainForm;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MySql.Data.MySqlClient;

namespace MTM_Inventory_Application.Data;

#region Dao_ErrorLog

internal static class Dao_ErrorLog
{
    #region Test Mode Detection

    /// <summary>
    /// Set to true when running in test mode to suppress MessageBox dialogs.
    /// Tests should set this flag in their setup to prevent blocking UI dialogs.
    /// </summary>
    public static bool IsTestMode { get; set; } = false;

    #endregion

    #region Query Methods

    internal static async Task<List<(string MethodName, string ErrorMessage)>> GetUniqueErrorsAsync(MySqlConnection? connection = null, MySqlTransaction? transaction = null)
    {
        List<(string MethodName, string ErrorMessage)> uniqueErrors = new();
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "log_error_Get_Unique",
                null, // No parameters needed
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (dataResult.IsSuccess && dataResult.Data != null)
            {
                foreach (DataRow row in dataResult.Data.Rows)
                {
                    uniqueErrors.Add((
                        row["MethodName"]?.ToString() ?? "", 
                        row["ErrorMessage"]?.ToString() ?? ""
                    ));
                }
            }
            else
            {
                LoggingUtility.Log($"GetUniqueErrors failed: {dataResult.ErrorMessage}");
            }

            LoggingUtility.Log("GetUniqueErrors executed successfully.");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // Don't call HandleException_GeneralError_CloseApp here to avoid recursion during startup
            LoggingUtility.Log($"GetUniqueErrors failed with exception: {ex.Message}");
        }

        return uniqueErrors;
    }

    internal static async Task<DaoResult<DataTable>> GetAllErrorsAsync(MySqlConnection? connection = null, MySqlTransaction? transaction = null) =>
        await GetErrorsByStoredProcedureAsync("log_error_Get_All", null, connection, transaction);

    internal static async Task<DaoResult<DataTable>> GetErrorsByUserAsync(string user, MySqlConnection? connection = null, MySqlTransaction? transaction = null) =>
        await GetErrorsByStoredProcedureAsync(
            "log_error_Get_ByUser",
            new Dictionary<string, object> { ["User"] = user },
            connection,
            transaction);

    internal static async Task<DaoResult<DataTable>>
        GetErrorsByDateRangeAsync(DateTime start, DateTime end, MySqlConnection? connection = null, MySqlTransaction? transaction = null) =>
        await GetErrorsByStoredProcedureAsync(
            "log_error_Get_ByDateRange",
            new Dictionary<string, object> { ["StartDate"] = start, ["EndDate"] = end },
            connection,
            transaction);

    private static async Task<DaoResult<DataTable>> GetErrorsByStoredProcedureAsync(string procedureName, 
        Dictionary<string, object>? parameters,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_AppVariables.ConnectionString,
                procedureName,
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (dataResult.IsSuccess && dataResult.Data != null)
            {
                return DaoResult<DataTable>.Success(dataResult.Data);
            }
            else
            {
                LoggingUtility.Log($"{procedureName} failed: {dataResult.ErrorMessage}");
                return DaoResult<DataTable>.Failure(dataResult.ErrorMessage ?? "Unknown error", dataResult.Exception);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            // Don't call HandleException_GeneralError_CloseApp here to avoid recursion during startup
            LoggingUtility.Log($"{procedureName} failed with exception: {ex.Message}");
            return DaoResult<DataTable>.Failure($"{procedureName} failed", ex);
        }
    }

    #endregion

    #region Delete Methods

    internal static async Task<DaoResult> DeleteErrorByIdAsync(int id, MySqlConnection? connection = null, MySqlTransaction? transaction = null) =>
        await ExecuteStoredProcedureNonQueryAsync("log_error_Delete_ById",
            new Dictionary<string, object> { ["Id"] = id }, connection, transaction);

    internal static async Task<DaoResult> DeleteAllErrorsAsync(MySqlConnection? connection = null, MySqlTransaction? transaction = null) =>
        await ExecuteStoredProcedureNonQueryAsync("log_error_Delete_All", null, connection, transaction);

    private static async Task<DaoResult> ExecuteStoredProcedureNonQueryAsync(string procedureName, 
        Dictionary<string, object>? parameters,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                procedureName,
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                LoggingUtility.Log($"{procedureName} failed: {result.ErrorMessage}");
                return DaoResult.Failure(result.ErrorMessage ?? "Unknown error", result.Exception);
            }

            return DaoResult.Success();
        }
        catch (Exception ex)
        {
            if (ex is MySqlException)
            {
                LoggingUtility.LogDatabaseError(ex);
            }
            else
            {
                LoggingUtility.LogApplicationError(ex);
            }

            // Don't call HandleException_GeneralError_CloseApp here to avoid recursion during startup
            LoggingUtility.Log($"{procedureName} failed with exception: {ex.Message}");
            return DaoResult.Failure($"{procedureName} failed", ex);
        }
    }

    #endregion

    #region Error Handling Methods

    private static string? _lastErrorMessage;
    private static DateTime _lastErrorTime = DateTime.MinValue;
    private static readonly TimeSpan ErrorMessageCooldown = TimeSpan.FromSeconds(5);

    private static string? _lastSqlErrorMessage;
    private static DateTime _lastSqlErrorTime = DateTime.MinValue;
    private static readonly TimeSpan SqlErrorMessageCooldown = TimeSpan.FromSeconds(5);

    #region Error Message Helpers

    private static bool ShouldShowErrorMessage(string message)
    {
        // Suppress message boxes during test execution
        if (IsTestMode)
        {
            return false;
        }

        DateTime now = DateTime.Now;
        lock (typeof(Dao_ErrorLog))
        {
            if (_lastErrorMessage == message && now - _lastErrorTime < ErrorMessageCooldown)
            {
                return false;
            }

            _lastErrorMessage = message;
            _lastErrorTime = now;
            return true;
        }
    }

    private static bool ShouldShowSqlErrorMessage(string message)
    {
        // Suppress message boxes during test execution
        if (IsTestMode)
        {
            return false;
        }

        DateTime now = DateTime.Now;
        lock (typeof(Dao_ErrorLog))
        {
            if (_lastSqlErrorMessage == message && now - _lastSqlErrorTime < SqlErrorMessageCooldown)
            {
                return false;
            }

            _lastSqlErrorMessage = message;
            _lastSqlErrorTime = now;
            return true;
        }
    }

    #endregion

    internal static async Task<DaoResult> HandleException_SQLError_CloseApp(
        Exception ex,
        [System.Runtime.CompilerServices.CallerMemberName]
        string callerName = "",
        string controlName = "")
    {
        try
        {
            LoggingUtility.LogDatabaseError(ex);
            LoggingUtility.Log($"SQL Error in method: {callerName}, Control: {controlName}");

            if (ex is MySqlException mysqlEx)
            {
                LoggingUtility.Log($"MySQL Error Code: {mysqlEx.Number}");
                LoggingUtility.Log($"MySQL Error Details: {mysqlEx.Message}");
            }

            bool isConnectionError = ex.Message.Contains("Unable to connect to any of the specified MySQL hosts.")
                                     || ex.Message.Contains("Access denied for user")
                                     || ex.Message.Contains("Can't connect to MySQL server on")
                                     || ex.Message.Contains("Unknown MySQL server host")
                                     || ex.Message.Contains("Lost connection to MySQL server")
                                     || ex.Message.Contains("MySQL server has gone away");

            string message = $"SQL Error in method: {callerName}, Control: {controlName}\n{ex.Message}";

            if (isConnectionError)
            {
                if (ShouldShowSqlErrorMessage(message))
                {
                    MessageBox.Show(
                        @"Database connection error. The application will now close.",
                        @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                Process.GetCurrentProcess().Kill();
                return DaoResult.Failure("Database connection error - application terminated", ex);
            }
            else
            {
                await LogErrorToDatabaseAsync(
                    "Critical",
                    ex.GetType().ToString(),
                    ex.Message,
                    ex.StackTrace,
                    "",
                    callerName,
                    controlName
                );

                if (ShouldShowSqlErrorMessage(message))
                {
                    MessageBox.Show(message, @"SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return DaoResult.Failure(message, ex);
            }
        }
        catch (Exception innerEx)
        {
            LoggingUtility.LogApplicationError(innerEx);
            return DaoResult.Failure($"Error handling SQL exception: {innerEx.Message}", innerEx);
        }
    }

    internal static async Task<DaoResult> HandleException_GeneralError_CloseApp(
        Exception ex,
        [System.Runtime.CompilerServices.CallerMemberName]
        string callerName = "",
        string controlName = "",
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            string errorType = ex switch
            {
                ArgumentNullException => "A required argument was null.",
                ArgumentOutOfRangeException => "An argument was out of range.",
                InvalidOperationException => "An invalid operation occurred.",
                FormatException => "A format error occurred.",
                NullReferenceException => "A null reference occurred.",
                OutOfMemoryException => "The application ran out of memory.",
                StackOverflowException => "A stack overflow occurred.",
                AccessViolationException => "An access violation occurred.",
                _ => "An unexpected error occurred."
            };

            string message = $"{errorType}\nMethod: {callerName}\nControl: {controlName}\nException:\n{ex.Message}";

            bool isCritical = ex is OutOfMemoryException || ex is StackOverflowException ||
                              ex is AccessViolationException;

            MainForm? mainForm = Application.OpenForms.OfType<MainForm>().FirstOrDefault();
            if (mainForm != null)
            {
                mainForm.ConnectionRecoveryManager.HandleConnectionLost();
            }

            LoggingUtility.LogApplicationError(ex);

            await LogErrorToDatabaseAsync(
                isCritical ? "Critical" : "Error",
                ex.GetType().ToString(),
                ex.Message,
                ex.StackTrace,
                "",
                callerName,
                controlName,
                connection,
                transaction
            );

            if (ShouldShowErrorMessage(message))
            {
                if (isCritical)
                {
                    MessageBox.Show(message + "\n\nThe application will now close due to a critical error.",
                        @"Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Process.GetCurrentProcess().Kill();
                    return DaoResult.Failure("Critical error - application terminated", ex);
                }
                else
                {
                    MessageBox.Show(message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            LoggingUtility.Log("HandleException_GeneralError_CloseApp executed successfully.");
            return DaoResult.Success();
        }
        catch (Exception innerEx)
        {
            LoggingUtility.LogApplicationError(innerEx);
            return await HandleException_GeneralError_CloseApp(innerEx, controlName: controlName);
        }
    }

    private static async Task LogErrorToDatabaseAsync(
        string severity,
        string errorType,
        string errorMessage,
        string? stackTrace,
        string moduleName,
        string methodName,
        string? additionalInfo,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        try
        {
            Dictionary<string, object> parameters = new()
            {
                ["p_User"] = Model_AppVariables.User ?? "Unknown", // FIXED: Remove p_ prefix
                ["Severity"] = severity,
                ["ErrorType"] = errorType,
                ["ErrorMessage"] = errorMessage,
                ["StackTrace"] = stackTrace ?? "",
                ["ModuleName"] = moduleName ?? "",
                ["MethodName"] = methodName ?? "",
                ["AdditionalInfo"] = additionalInfo ?? "",
                ["MachineName"] = Environment.MachineName,
                ["OSVersion"] = Environment.OSVersion.ToString(),
                ["AppVersion"] = Application.ProductVersion,
                ["ErrorTime"] = DateTime.Now
            };

            var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_AppVariables.ConnectionString,
                "log_error_Add_Error",
                parameters,
                progressHelper: null,
                connection: connection,
                transaction: transaction
            );

            if (!result.IsSuccess)
            {
                // If database logging fails, just log to file system
                LoggingUtility.Log($"Failed to log error to database: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            // If logging to database fails, we don't want to create a recursive error loop
            // Just log to the file system via LoggingUtility
            LoggingUtility.LogApplicationError(new Exception($"Failed to log error to database: {ex.Message}", ex));
        }
    }

    #endregion

    #region Synchronous Helpers

    internal static List<(string MethodName, string ErrorMessage)> GetUniqueErrors() =>
        GetUniqueErrorsAsync().GetAwaiter().GetResult();

    internal static void LogErrorWithMethod(Exception ex,
        [System.Runtime.CompilerServices.CallerMemberName]
        string methodName = "")
    {
        LoggingUtility.LogApplicationError(ex);
        LoggingUtility.Log($"Error in {methodName}: {ex.Message}");
    }

    #endregion
}

#endregion
