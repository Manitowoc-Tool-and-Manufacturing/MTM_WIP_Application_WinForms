using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Enhanced database helper for stored procedures with comprehensive status reporting.
/// REFACTORED: 2025-10-13 - Phase 2: Comprehensive Database Layer Refactor
/// - Automatic parameter prefix detection via INFORMATION_SCHEMA cache
/// - Model_Dao_Result pattern for consistent error handling
/// - Async-only execution (removed useAsync parameter)
/// - Transient error retry logic with exponential backoff
/// - Performance monitoring with configurable thresholds
/// </summary>
public static class Helper_Database_StoredProcedure
{
    #region Constants

    private const int MAX_RETRY_ATTEMPTS = 3;
    private const int BASE_RETRY_DELAY_MS = 500;

    // Transient MySQL error codes that warrant retry
    private static readonly int[] TransientErrorCodes = { 1205, 1213, 2006, 2013 };

    #endregion

    #region Public Methods - Core Execution

    /// <summary>
    /// Execute stored procedure that returns multiple result sets as a DataSet with status output parameters
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="progressHelper">Progress helper for visual feedback (optional)</param>
    /// <param name="connection">Optional external connection (for test transaction support)</param>
    /// <param name="transaction">Optional external transaction (for test transaction support)</param>
    /// <returns>Model_Dao_Result containing DataSet with multiple tables and status information</returns>
    /// <remarks>
    /// <para>
    /// Use this method when a stored procedure returns multiple result sets.
    /// Each result set will be a separate DataTable in the returned DataSet.
    /// </para>
    /// <para>
    /// <strong>Transaction Support</strong>: When <paramref name="connection"/> and <paramref name="transaction"/>
    /// are provided, the method uses the external connection/transaction instead of creating a new one.
    /// </para>
    /// </remarks>
    public static async Task<Model_Dao_Result<DataSet>> ExecuteDataSetWithStatusAsync(
        string connectionString,
        string procedureName,
        Dictionary<string, object>? parameters = null,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        var stopwatch = Stopwatch.StartNew();
        var performanceKey = Service_DebugTracer.StartPerformanceTrace($"SP_{procedureName}");

        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["procedureName"] = procedureName,
            ["parameterCount"] = parameters?.Count ?? 0
        }, nameof(ExecuteDataSetWithStatusAsync), "Helper_Database_StoredProcedure");

        Service_DebugTracer.TraceDatabaseStart("PROCEDURE", procedureName, parameters, connectionString);

        try
        {
            return await ExecuteWithRetryAsync(async () =>
            {
                progressHelper?.UpdateProgress(10, $"Connecting to database for {procedureName}...");

                // Use provided connection or create new one
                bool externalConnection = connection != null;
                MySqlConnection activeConnection = connection ?? new MySqlConnection(connectionString);

                try
                {
                    if (!externalConnection)
                    {
                        await activeConnection.OpenAsync();
                    }

                    progressHelper?.UpdateProgress(30, $"Executing stored procedure {procedureName}...");

                    using var command = new MySqlCommand(procedureName, activeConnection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
                    };

                    // Associate with external transaction if provided
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }

                    // Add input parameters with automatic prefix detection
                    var finalParameters = AddParametersWithPrefixDetection(command, procedureName, parameters);

                    // Add standard output parameters
                    var (statusParam, errorMsgParam) = AddStandardOutputParameters(command, procedureName);

                    progressHelper?.UpdateProgress(50, $"Retrieving data sets from {procedureName}...");

                    // Execute and fill DataSet with all result sets
                    var dataSet = new DataSet();
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        await Task.Run(() => adapter.Fill(dataSet));
                    }

                    progressHelper?.UpdateProgress(80, $"Processing {dataSet.Tables.Count} result sets from {procedureName}...");

                    int status = Convert.ToInt32(statusParam.Value ?? 0);
                    string errorMessage = errorMsgParam.Value?.ToString() ?? string.Empty;

                    progressHelper?.UpdateProgress(100, $"Completed {procedureName}");

                    stopwatch.Stop();

                    // Log performance if threshold exceeded
                    LogPerformanceIfNeeded(procedureName, stopwatch.ElapsedMilliseconds, "Query");

                    Service_DebugTracer.TraceStoredProcedure(
                        procedureName,
                        finalParameters,
                        new Dictionary<string, object>
                        {
                            ["Status"] = status,
                            ["ErrorMsg"] = errorMessage
                        },
                        $"DataSet[{dataSet.Tables.Count} tables]",
                        status,
                        errorMessage,
                        stopwatch.ElapsedMilliseconds);

                    Service_DebugTracer.TraceDatabaseComplete("PROCEDURE", procedureName,
                        $"DataSet with {dataSet.Tables.Count} tables", dataSet.Tables.Count, stopwatch.ElapsedMilliseconds);

                    // Status codes: 1=success with data, 0=success without data, negative=error
                    if (status >= 0)
                    {
                        // Both status 0 and 1 are success (with or without data)
                        int totalRows = 0;
                        foreach (DataTable table in dataSet.Tables)
                        {
                            totalRows += table.Rows.Count;
                        }

                        progressHelper?.ProcessStoredProcedureResult(status, errorMessage,
                            $"Successfully retrieved {dataSet.Tables.Count} result sets with {totalRows} total records");

                        return Model_Dao_Result<DataSet>.Success(dataSet,
                            errorMessage != string.Empty ? errorMessage : "Data sets retrieved successfully");
                    }
                    else
                    {
                        // Negative status = error
                        progressHelper?.ProcessStoredProcedureResult(status, errorMessage,
                            $"Stored procedure {procedureName} returned error status");

                        return Model_Dao_Result<DataSet>.Failure(
                            errorMessage != string.Empty ? errorMessage : $"Stored procedure {procedureName} returned error status: {status}");
                    }
                }
                finally
                {
                    if (!externalConnection)
                    {
                        await activeConnection.DisposeAsync();
                    }
                }
            });
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            string userFriendlyMessage = GetUserFriendlyConnectionError(ex, procedureName);
            LoggingUtility.LogDatabaseError(ex);
            progressHelper?.ProcessStoredProcedureResult(-1, ex.Message);

            Service_DebugTracer.TraceDatabaseComplete("PROCEDURE", procedureName,
                $"EXCEPTION: {ex.Message}", 0, stopwatch.ElapsedMilliseconds);

            var result = Model_Dao_Result<DataSet>.Failure(userFriendlyMessage, ex);

            Service_DebugTracer.TraceMethodExit(result, nameof(ExecuteDataSetWithStatusAsync), "Helper_Database_StoredProcedure");
            Service_DebugTracer.StopPerformanceTrace(performanceKey, new Dictionary<string, object>
            {
                ["Status"] = "EXCEPTION",
                ["ErrorMessage"] = ex.Message
            });

            return result;
        }
    }

    /// <summary>
    /// Execute stored procedure that returns a DataTable with status output parameters
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="progressHelper">Progress helper for visual feedback (optional)</param>
    /// <param name="connection">Optional external connection (for test transaction support)</param>
    /// <param name="transaction">Optional external transaction (for test transaction support)</param>
    /// <returns>Model_Dao_Result containing DataTable and status information</returns>
    /// <remarks>
    /// <para>
    /// Async-only execution. Automatically detects and applies correct parameter prefixes (p_, in_, o_)
    /// using INFORMATION_SCHEMA cache. Includes retry logic for transient errors and performance monitoring.
    /// </para>
    /// <para>
    /// <strong>Transaction Support</strong>: When <paramref name="connection"/> and <paramref name="transaction"/>
    /// are provided, the method uses the external connection/transaction instead of creating a new one.
    /// This enables proper test isolation with automatic rollback. The external connection is NOT disposed
    /// by this method - caller remains responsible for connection lifecycle.
    /// </para>
    /// </remarks>
    public static async Task<Model_Dao_Result<DataTable>> ExecuteDataTableWithStatusAsync(
        string connectionString,
        string procedureName,
        Dictionary<string, object>? parameters = null,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        var stopwatch = Stopwatch.StartNew();
        var performanceKey = Service_DebugTracer.StartPerformanceTrace($"SP_{procedureName}");

        Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
        {
            ["procedureName"] = procedureName,
            ["parameterCount"] = parameters?.Count ?? 0
        }, nameof(ExecuteDataTableWithStatusAsync), "Helper_Database_StoredProcedure");

        Service_DebugTracer.TraceDatabaseStart("PROCEDURE", procedureName, parameters, connectionString);

        try
        {
            return await ExecuteWithRetryAsync(async () =>
            {
                progressHelper?.UpdateProgress(10, $"Connecting to database for {procedureName}...");

                // Use provided connection or create new one
                bool externalConnection = connection != null;
                MySqlConnection activeConnection = connection ?? new MySqlConnection(connectionString);

                try
                {
                    if (!externalConnection)
                    {
                        await activeConnection.OpenAsync();
                    }

                    progressHelper?.UpdateProgress(30, $"Executing stored procedure {procedureName}...");

                    using var command = new MySqlCommand(procedureName, activeConnection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
                    };

                    // Associate with external transaction if provided
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }

                    // Add input parameters with automatic prefix detection
                    var finalParameters = AddParametersWithPrefixDetection(command, procedureName, parameters);

                    // Add standard output parameters
                    var (statusParam, errorMsgParam) = AddStandardOutputParameters(command, procedureName);

                    progressHelper?.UpdateProgress(50, $"Retrieving data from {procedureName}...");

                    var dataTable = new DataTable();
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        await Task.Run(() => adapter.Fill(dataTable));
                    }

                    progressHelper?.UpdateProgress(80, $"Processing results from {procedureName}...");

                    int status = Convert.ToInt32(statusParam.Value ?? 0);
                    string errorMessage = errorMsgParam.Value?.ToString() ?? string.Empty;

                progressHelper?.UpdateProgress(100, $"Completed {procedureName}");

                stopwatch.Stop();

                // Log performance if threshold exceeded
                LogPerformanceIfNeeded(procedureName, stopwatch.ElapsedMilliseconds, "Query");

                Service_DebugTracer.TraceStoredProcedure(
                    procedureName,
                    finalParameters,
                    new Dictionary<string, object>
                    {
                        ["Status"] = status,
                        ["ErrorMsg"] = errorMessage
                    },
                    $"DataTable[{dataTable.Rows.Count} rows]",
                    status,
                    errorMessage,
                    stopwatch.ElapsedMilliseconds);

                Service_DebugTracer.TraceDatabaseComplete("PROCEDURE", procedureName,
                    $"DataTable with {dataTable.Rows.Count} rows", dataTable.Rows.Count, stopwatch.ElapsedMilliseconds);

                // Status codes: 1=success with data, 0=success without data, negative=error
                if (status >= 0)
                {
                    // Both status 0 and 1 are success (with or without data)
                    progressHelper?.ProcessStoredProcedureResult(status, errorMessage,
                        $"Successfully retrieved {dataTable.Rows.Count} records");

                    var result = Model_Dao_Result<DataTable>.Success(dataTable, errorMessage, dataTable.Rows.Count);

                    Service_DebugTracer.TraceMethodExit(result, nameof(ExecuteDataTableWithStatusAsync), "Helper_Database_StoredProcedure");
                    Service_DebugTracer.StopPerformanceTrace(performanceKey, new Dictionary<string, object>
                    {
                        ["Status"] = "SUCCESS",
                        ["RowCount"] = dataTable.Rows.Count
                    });

                    return result;
                }
                else
                {
                    // Negative status = error
                    progressHelper?.ProcessStoredProcedureResult(status, errorMessage);

                    var result = Model_Dao_Result<DataTable>.Failure(errorMessage, null);

                    Service_DebugTracer.TraceMethodExit(result, nameof(ExecuteDataTableWithStatusAsync), "Helper_Database_StoredProcedure");
                    Service_DebugTracer.StopPerformanceTrace(performanceKey, new Dictionary<string, object>
                    {
                        ["Status"] = "ERROR",
                        ["ErrorMessage"] = errorMessage
                    });

                    return result;
                }
                }
                finally
                {
                    // Only dispose if we created the connection (not provided externally)
                    if (!externalConnection)
                    {
                        activeConnection?.Dispose();
                    }
                }
            });
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            string userFriendlyMessage = GetUserFriendlyConnectionError(ex, procedureName);
            LoggingUtility.LogDatabaseError(ex);
            progressHelper?.ShowError(userFriendlyMessage);

            Service_DebugTracer.TraceDatabaseComplete("PROCEDURE", procedureName,
                $"EXCEPTION: {ex.Message}", 0, stopwatch.ElapsedMilliseconds);

            var result = Model_Dao_Result<DataTable>.Failure(userFriendlyMessage, ex);

            Service_DebugTracer.TraceMethodExit(result, nameof(ExecuteDataTableWithStatusAsync), "Helper_Database_StoredProcedure");
            Service_DebugTracer.StopPerformanceTrace(performanceKey, new Dictionary<string, object>
            {
                ["Status"] = "EXCEPTION",
                ["ExceptionMessage"] = ex.Message
            });

            return result;
        }
    }

    /// <summary>
    /// Execute stored procedure that returns a scalar value with status output parameters
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="progressHelper">Progress helper for visual feedback (optional)</param>
    /// <param name="connection">Optional external connection (for test transaction support)</param>
    /// <param name="transaction">Optional external transaction (for test transaction support)</param>
    /// <returns>Model_Dao_Result containing scalar value and status information</returns>
    /// <remarks>
    /// When connection and transaction are provided, uses external connection for test isolation support.
    /// </remarks>
    public static async Task<Model_Dao_Result<object>> ExecuteScalarWithStatusAsync(
        string connectionString,
        string procedureName,
        Dictionary<string, object>? parameters = null,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var result = await ExecuteWithRetryAsync(async () =>
            {
                progressHelper?.UpdateProgress(10, $"Connecting to database for {procedureName}...");

                // Use provided connection or create new one
                bool externalConnection = connection != null;
                MySqlConnection activeConnection = connection ?? new MySqlConnection(connectionString);

                try
                {
                    if (!externalConnection)
                    {
                        await activeConnection.OpenAsync();
                    }

                    progressHelper?.UpdateProgress(30, $"Executing stored procedure {procedureName}...");

                    using var command = new MySqlCommand(procedureName, activeConnection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
                    };

                    // Associate with external transaction if provided
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }

                    // Add input parameters with automatic prefix detection
                    AddParametersWithPrefixDetection(command, procedureName, parameters);

                    // Add standard output parameters
                    var (statusParam, errorMsgParam) = AddStandardOutputParameters(command, procedureName);

                    progressHelper?.UpdateProgress(50, $"Retrieving data from {procedureName}...");

                    object? result = await command.ExecuteScalarAsync();

                    progressHelper?.UpdateProgress(80, $"Processing results from {procedureName}...");

                    int status = Convert.ToInt32(statusParam.Value ?? 0);
                    string errorMessage = errorMsgParam.Value?.ToString() ?? string.Empty;

                    progressHelper?.UpdateProgress(100, $"Completed {procedureName}");
                    progressHelper?.ProcessStoredProcedureResult(status, errorMessage);

                    stopwatch.Stop();
                    LogPerformanceIfNeeded(procedureName, stopwatch.ElapsedMilliseconds, "Query");

                    // Handle null result properly
                    object safeResult = result ?? DBNull.Value;

                    // Status codes: 1=success with data, 0=success without data, negative=error
                    if (status >= 0)
                        return Model_Dao_Result<object>.Success(safeResult, errorMessage);
                    else
                        return Model_Dao_Result<object>.Failure(errorMessage, null);
                }
                finally
                {
                    // Only dispose if we created the connection (not provided externally)
                    if (!externalConnection)
                    {
                        activeConnection?.Dispose();
                    }
                }
            });

            // ---------------------------------------------------------
            // DUAL-WRITE LOGIC (Production -> Test)
            // ---------------------------------------------------------
            // If the operation was successful AND we are on Production,
            // replicate the change to the Test database.
            if (result.IsSuccess)
            {
                try
                {
                    var builder = new MySqlConnectionStringBuilder(connectionString);
                    // Check if we are targeting the Production database
                    if (builder.Database.Equals("mtm_wip_application_winforms", StringComparison.OrdinalIgnoreCase))
                    {
                        // Switch to Test database
                        builder.Database = "mtm_wip_application_winforms_test";
                        string testConnectionString = builder.ConnectionString;

                        // Execute recursively against Test DB
                        // - No progress helper (background task)
                        // - No external connection/transaction (must be independent)
                        await ExecuteScalarWithStatusAsync(
                            testConnectionString,
                            procedureName,
                            parameters,
                            null,
                            null,
                            null
                        );
                    }
                }
                catch (Exception ex)
                {
                    // Log replication failure but DO NOT fail the primary operation
                    // This ensures Production stability even if Test DB is down
                    LoggingUtility.LogApplicationError(new Exception($"Dual-write to Test Database failed for {procedureName}", ex));
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            string userFriendlyMessage = GetUserFriendlyConnectionError(ex, procedureName);
            LoggingUtility.LogDatabaseError(ex);
            progressHelper?.ShowError(userFriendlyMessage);
            return Model_Dao_Result<object>.Failure(userFriendlyMessage, ex);
        }
    }

    /// <summary>
    /// Execute stored procedure that doesn't return data (INSERT, UPDATE, DELETE) with status output parameters
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="progressHelper">Progress helper for visual feedback (optional)</param>
    /// <returns>Model_Dao_Result containing operation status and rows affected</returns>
    /// <summary>
    /// Execute stored procedure that doesn't return data (INSERT, UPDATE, DELETE) with status output parameters
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="progressHelper">Progress helper for visual feedback (optional)</param>
    /// <param name="connection">Optional external connection (for test transaction support)</param>
    /// <param name="transaction">Optional external transaction (for test transaction support)</param>
    /// <returns>Model_Dao_Result containing status and rows affected</returns>
    /// <remarks>
    /// When connection and transaction are provided, uses external connection for test isolation support.
    /// </remarks>
    public static async Task<Model_Dao_Result> ExecuteNonQueryWithStatusAsync(
        string connectionString,
        string procedureName,
        Dictionary<string, object>? parameters = null,
        Helper_StoredProcedureProgress? progressHelper = null,
        MySqlConnection? connection = null,
        MySqlTransaction? transaction = null)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var result = await ExecuteWithRetryAsync(async () =>
            {
                progressHelper?.UpdateProgress(10, $"Connecting to database for {procedureName}...");

                // Use provided connection or create new one
                bool externalConnection = connection != null;
                MySqlConnection activeConnection = connection ?? new MySqlConnection(connectionString);

                try
                {
                    if (!externalConnection)
                    {
                        await activeConnection.OpenAsync();
                    }

                    progressHelper?.UpdateProgress(30, $"Executing stored procedure {procedureName}...");

                    using var command = new MySqlCommand(procedureName, activeConnection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
                    };

                    // Associate with external transaction if provided
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }

                    // Add input parameters with automatic prefix detection
                    AddParametersWithPrefixDetection(command, procedureName, parameters);

                    // Add standard output parameters
                    var (statusParam, errorMsgParam) = AddStandardOutputParameters(command, procedureName);

                    progressHelper?.UpdateProgress(50, $"Executing {procedureName}...");

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    progressHelper?.UpdateProgress(80, $"Processing results from {procedureName}...");

                    int status = Convert.ToInt32(statusParam.Value ?? 0);
                string errorMessage = errorMsgParam.Value?.ToString() ?? string.Empty;

                progressHelper?.UpdateProgress(100, $"Completed {procedureName}");
                progressHelper?.ProcessStoredProcedureResult(status, errorMessage,
                    $"Operation completed successfully ({rowsAffected} rows affected)");

                stopwatch.Stop();
                LogPerformanceIfNeeded(procedureName, stopwatch.ElapsedMilliseconds, "Modification");

                // Status codes: 1=success with data, 0=success without data, negative=error
                if (status >= 0)
                    return Model_Dao_Result.Success(errorMessage, rowsAffected);
                else
                    return Model_Dao_Result.Failure(errorMessage, null);
                }
                finally
                {
                    // Only dispose if we created the connection (not provided externally)
                    if (!externalConnection)
                    {
                        activeConnection?.Dispose();
                    }
                }
            });

            // ---------------------------------------------------------
            // DUAL-WRITE LOGIC (Production -> Test)
            // ---------------------------------------------------------
            // If the operation was successful AND we are on Production,
            // replicate the change to the Test database.
            if (result.IsSuccess)
            {
                try
                {
                    var builder = new MySqlConnectionStringBuilder(connectionString);
                    // Check if we are targeting the Production database
                    if (builder.Database.Equals("mtm_wip_application_winforms", StringComparison.OrdinalIgnoreCase))
                    {
                        // Switch to Test database
                        builder.Database = "mtm_wip_application_winforms_test";
                        string testConnectionString = builder.ConnectionString;

                        // Execute recursively against Test DB
                        // - No progress helper (background task)
                        // - No external connection/transaction (must be independent)
                        await ExecuteNonQueryWithStatusAsync(
                            testConnectionString,
                            procedureName,
                            parameters,
                            null,
                            null,
                            null
                        );
                    }
                }
                catch (Exception ex)
                {
                    // Log replication failure but DO NOT fail the primary operation
                    // This ensures Production stability even if Test DB is down
                    LoggingUtility.LogApplicationError(new Exception($"Dual-write to Test Database failed for {procedureName}", ex));
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            string userFriendlyMessage = GetUserFriendlyConnectionError(ex, procedureName);
            LoggingUtility.LogDatabaseError(ex);
            progressHelper?.ShowError(userFriendlyMessage);
            return Model_Dao_Result.Failure(userFriendlyMessage, ex);
        }
    }

    /// <summary>
    /// Execute stored procedure with custom output parameters
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="outputParameters">Output parameter names to retrieve</param>
    /// <param name="progressHelper">Progress helper for visual feedback (optional)</param>
    /// <returns>Model_Dao_Result containing dictionary of output parameter values</returns>
    public static async Task<Model_Dao_Result<Dictionary<string, object>>> ExecuteWithCustomOutputAsync(
        string connectionString,
        string procedureName,
        Dictionary<string, object>? parameters = null,
        List<string>? outputParameters = null,
        Helper_StoredProcedureProgress? progressHelper = null)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await ExecuteWithRetryAsync(async () =>
            {
                progressHelper?.UpdateProgress(10, $"Connecting to database for {procedureName}...");

                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                progressHelper?.UpdateProgress(30, $"Executing stored procedure {procedureName}...");

                using var command = new MySqlCommand(procedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
                };

                // Add input parameters with automatic prefix detection
                AddParametersWithPrefixDetection(command, procedureName, parameters);

                // Add standard output parameters
                var (statusParam, errorMsgParam) = AddStandardOutputParameters(command, procedureName);

                // Add custom output parameters with prefix detection
                var customOutputParams = new Dictionary<string, MySqlParameter>();
                if (outputParameters != null)
                {
                    foreach (var paramName in outputParameters)
                    {
                        string prefix = Model_ParameterPrefix_Cache.GetParameterPrefix(procedureName, paramName);
                        string fullParamName = prefix + paramName;

                        var param = new MySqlParameter(fullParamName, MySqlDbType.VarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(param);
                        customOutputParams[paramName] = param;
                    }
                }

                progressHelper?.UpdateProgress(50, $"Executing {procedureName}...");

                await command.ExecuteNonQueryAsync();

                progressHelper?.UpdateProgress(80, $"Processing results from {procedureName}...");

                int status = Convert.ToInt32(statusParam.Value ?? 0);
                string errorMessage = errorMsgParam.Value?.ToString() ?? string.Empty;

                // Collect output parameter values
                var outputValues = new Dictionary<string, object>();
                foreach (var kvp in customOutputParams)
                {
                    outputValues[kvp.Key] = kvp.Value.Value ?? DBNull.Value;
                }

                progressHelper?.UpdateProgress(100, $"Completed {procedureName}");
                progressHelper?.ProcessStoredProcedureResult(status, errorMessage);

                stopwatch.Stop();
                LogPerformanceIfNeeded(procedureName, stopwatch.ElapsedMilliseconds, "Modification");

                // Status codes: 1=success with data, 0=success without data, negative=error
                if (status >= 0)
                    return Model_Dao_Result<Dictionary<string, object>>.Success(outputValues, errorMessage);
                else
                    return Model_Dao_Result<Dictionary<string, object>>.Failure(errorMessage, null);
            });
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            string userFriendlyMessage = GetUserFriendlyConnectionError(ex, procedureName);
            LoggingUtility.LogDatabaseError(ex);
            progressHelper?.ShowError(userFriendlyMessage);
            return Model_Dao_Result<Dictionary<string, object>>.Failure(userFriendlyMessage, ex);
        }
    }

    /// <summary>
    /// Execute stored procedure and return MySqlDataReader for streaming large result sets (no Model_Dao_Result wrapper)
    /// </summary>
    /// <param name="connectionString">Database connection string</param>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="parameters">Input parameters (WITHOUT prefix - prefixes added automatically)</param>
    /// <param name="commandType">Command type (usually StoredProcedure)</param>
    /// <returns>MySqlDataReader for reading results</returns>
    /// <remarks>
    /// IMPORTANT: Caller is responsible for disposing the reader and its underlying connection.
    /// This method intentionally does NOT wrap the result in Model_Dao_Result to allow streaming.
    /// </remarks>
    public static async Task<MySqlDataReader> ExecuteReaderAsync(
        string connectionString,
        string procedureName,
        Dictionary<string, object>? parameters = null,
        CommandType commandType = CommandType.StoredProcedure)
    {
        var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            using var command = new MySqlCommand(procedureName, connection)
            {
                CommandType = commandType,
                CommandTimeout = Model_Application_Variables.CommandTimeoutSeconds
            };

            // Add input parameters with automatic prefix detection
            AddParametersWithPrefixDetection(command, procedureName, parameters);

            return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            connection.Dispose();

            // Re-throw with user-friendly message for connection errors
            if (IsConnectionRelatedError(ex))
            {
                string userFriendlyMessage = GetUserFriendlyConnectionError(ex, procedureName);
                LoggingUtility.LogDatabaseError(ex);
                throw new InvalidOperationException(userFriendlyMessage, ex);
            }

            throw;
        }
    }

    #endregion

    #region Private Helper Methods - Parameter Management

    /// <summary>
    /// Adds parameters to MySqlCommand with automatic prefix detection
    /// </summary>
    /// <param name="command">MySqlCommand to add parameters to</param>
    /// <param name="procedureName">Stored procedure name for prefix cache lookup</param>
    /// <param name="parameters">Parameters dictionary (WITHOUT prefixes)</param>
    /// <returns>Dictionary of final parameter names WITH prefixes for logging</returns>
    private static Dictionary<string, object> AddParametersWithPrefixDetection(
        MySqlCommand command,
        string procedureName,
        Dictionary<string, object>? parameters)
    {
        var finalParameters = new Dictionary<string, object>();

        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                string paramName = param.Key;

                // Check if parameter already has a prefix
                bool hasPrefix = paramName.StartsWith("p_") ||
                                paramName.StartsWith("in_") ||
                                paramName.StartsWith("o_");

                string fullParamName;
                if (hasPrefix)
                {
                    // Use as-is if already prefixed
                    fullParamName = paramName;
                }
                else
                {
                    // Detect prefix from cache
                    string prefix = Model_ParameterPrefix_Cache.GetParameterPrefix(procedureName, paramName);
                    fullParamName = prefix + paramName;
                }

                command.Parameters.AddWithValue(fullParamName, param.Value ?? DBNull.Value);
                finalParameters[fullParamName] = param.Value ?? DBNull.Value;
            }
        }

        return finalParameters;
    }

    /// <summary>
    /// Adds standard output parameters (p_Status, p_ErrorMsg) to MySqlCommand
    /// </summary>
    /// <param name="command">MySqlCommand to add parameters to</param>
    /// <param name="procedureName">Stored procedure name for prefix cache lookup</param>
    /// <returns>Tuple of status and error message parameters</returns>
    private static (MySqlParameter statusParam, MySqlParameter errorMsgParam) AddStandardOutputParameters(
        MySqlCommand command,
        string procedureName)
    {
        // Detect prefixes for standard output parameters
        string statusPrefix = Model_ParameterPrefix_Cache.GetParameterPrefix(procedureName, "Status");
        string errorMsgPrefix = Model_ParameterPrefix_Cache.GetParameterPrefix(procedureName, "ErrorMsg");

        var statusParam = new MySqlParameter(statusPrefix + "Status", MySqlDbType.Int32)
        {
            Direction = ParameterDirection.Output
        };
        var errorMsgParam = new MySqlParameter(errorMsgPrefix + "ErrorMsg", MySqlDbType.VarChar, 500)
        {
            Direction = ParameterDirection.Output
        };

        command.Parameters.Add(statusParam);
        command.Parameters.Add(errorMsgParam);

        return (statusParam, errorMsgParam);
    }

    #endregion

    #region Private Helper Methods - Retry Logic

    /// <summary>
    /// Executes operation with retry logic for transient MySQL errors
    /// </summary>
    /// <typeparam name="T">Return type of the operation</typeparam>
    /// <param name="operation">Operation to execute</param>
    /// <returns>Result of the operation</returns>
    private static async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> operation)
    {
        int attempt = 0;
        Exception? lastException = null;

        while (attempt < MAX_RETRY_ATTEMPTS)
        {
            try
            {
                return await operation();
            }
            catch (MySqlException ex) when (IsTransientError(ex) && attempt < MAX_RETRY_ATTEMPTS - 1)
            {
                attempt++;
                lastException = ex;

                int delayMs = BASE_RETRY_DELAY_MS * (int)Math.Pow(2, attempt - 1); // Exponential backoff



                await Task.Delay(delayMs);
            }
        }

        // If we exhausted retries, throw the last exception
        throw lastException ?? new InvalidOperationException("Operation failed after retries but no exception was captured");
    }

    /// <summary>
    /// Checks if a MySqlException represents a transient error that should be retried
    /// </summary>
    private static bool IsTransientError(MySqlException ex)
    {
        foreach (var code in TransientErrorCodes)
        {
            if (ex.Number == code)
                return true;
        }
        return false;
    }

    #endregion

    #region Private Helper Methods - Performance Monitoring

    /// <summary>
    /// Logs warning if stored procedure execution exceeds configured threshold
    /// </summary>
    /// <param name="procedureName">Stored procedure name</param>
    /// <param name="elapsedMs">Elapsed milliseconds</param>
    /// <param name="operationType">Operation type: Query, Modification, Batch, Report</param>
    private static void LogPerformanceIfNeeded(string procedureName, long elapsedMs, string operationType)
    {
        long thresholdMs = operationType switch
        {
            "Query" => Model_Application_Variables.QueryThresholdMs ?? 500,
            "Modification" => Model_Application_Variables.ModificationThresholdMs ?? 1000,
            "Batch" => Model_Application_Variables.BatchThresholdMs ?? 5000,
            "Report" => Model_Application_Variables.ReportThresholdMs ?? 2000,
            _ => 1000
        };

        if (elapsedMs > thresholdMs)
        {

        }
    }

    #endregion

    #region Private Helper Methods - Error Handling

    /// <summary>
    /// Checks if exception is connection-related
    /// </summary>
    private static bool IsConnectionRelatedError(Exception ex)
    {
        return ex is MySqlException ||
               ex is System.Net.Sockets.SocketException ||
               ex is TimeoutException ||
               ex is InvalidOperationException && ex.Message.Contains("connection");
    }

    /// <summary>
    /// Generates user-friendly error message from database exception
    /// </summary>
    private static string GetUserFriendlyConnectionError(Exception ex, string procedureName)
    {
        if (ex is MySqlException mysqlEx)
        {
            return mysqlEx.Number switch
            {
                0 => $"Unable to connect to database while executing '{procedureName}'. Please check network connectivity.",
                1042 => $"Cannot reach database server while executing '{procedureName}'. The server may be offline.",
                1045 => $"Access denied when executing '{procedureName}'. Please check database credentials.",
                1205 => $"Database deadlock detected while executing '{procedureName}'. The operation will be retried automatically.",
                1213 => $"Database lock timeout while executing '{procedureName}'. The operation will be retried automatically.",
                2002 => $"Database server connection timed out while executing '{procedureName}'.",
                2006 => $"Database connection lost while executing '{procedureName}'. The operation will be retried automatically.",
                2013 => $"Lost connection to database during '{procedureName}'. The operation will be retried automatically.",
                _ => $"Database error while executing '{procedureName}': {mysqlEx.Message}"
            };
        }
        else if (ex is TimeoutException)
        {
            return $"Database operation '{procedureName}' timed out. The database may be overloaded.";
        }
        else if (ex is System.Net.Sockets.SocketException)
        {
            return $"Network error while executing '{procedureName}'. Please check network connectivity.";
        }
        else
        {
            return $"Error executing '{procedureName}': {ex.Message}";
        }
    }

    #endregion
}
