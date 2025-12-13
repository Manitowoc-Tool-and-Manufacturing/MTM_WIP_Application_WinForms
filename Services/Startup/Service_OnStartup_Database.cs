using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    /// <summary>
    /// Service responsible for database-related startup tasks, including connectivity validation
    /// and parameter cache initialization.
    /// </summary>
    /// <remarks>
    /// <para><strong>ARCHITECTURAL EXCEPTION (Constitution Principle I)</strong></para>
    /// <para>This service contains direct MySqlConnection usage in InitializeParameterPrefixCacheInternal().
    /// This is an APPROVED exception because:</para>
    /// <list type="number">
    /// <item><description>Parameter cache initialization requires querying INFORMATION_SCHEMA.PARAMETERS at startup</description></item>
    /// <item><description>This is a one-time operation that cannot use stored procedures (it's building the stored procedure metadata cache)</description></item>
    /// <item><description>Direct connection is properly disposed using 'using var' pattern</description></item>
    /// <item><description>This exception is documented in .specify/memory/constitution.md</description></item>
    /// </list>
    /// <para>ALL other components MUST use Helper_Database_StoredProcedure for database access.</para>
    /// </remarks>
    public static class Service_OnStartup_Database
    {
        #region Methods

        /// <summary>
        /// Validates connectivity to the database during application startup.
        /// </summary>
        /// <returns>
        /// A <see cref="Model_StartupResult"/> indicating success or failure.
        /// </returns>
        public static async Task<Model_StartupResult> ValidateConnectivityAsync()
        {
            var connectivityResult = await Dao_System.CheckConnectivityAsync();
            if (!connectivityResult.IsSuccess)
            {
                LoggingUtility.Log($"[Startup] Database connectivity validation failed: {connectivityResult.StatusMessage}");

                Console.WriteLine($"[DEBUG] connectivityResult.StatusMessage: '{connectivityResult.StatusMessage}'");
                Console.WriteLine($"[DEBUG] connectivityResult.ErrorMessage: '{connectivityResult.ErrorMessage}'");

                string errorMessage = !string.IsNullOrEmpty(connectivityResult.StatusMessage) &&
                                    connectivityResult.StatusMessage != "Database connectivity validation failed"
                                    ? connectivityResult.StatusMessage
                                    : connectivityResult.ErrorMessage;

                return Model_StartupResult.Failure(errorMessage, connectivityResult.Exception ?? new Exception(errorMessage), new Dictionary<string, object>
                {
                    ["DatabaseName"] = Model_Shared_Users.Database ?? "mtm_wip_application_winforms",
                    ["ServerAddress"] = Model_Shared_Users.WipServerAddress,
                    ["MethodName"] = "ValidateConnectivity",
                    ["ErrorType"] = "DatabaseConnectivityValidation"
                });
            }

            LoggingUtility.Log("[Startup] Database connectivity validated successfully");
            return Model_StartupResult.Success("Database connectivity validated successfully");
        }

        /// <summary>
        /// Initializes the stored procedure parameter cache by querying INFORMATION_SCHEMA.
        /// </summary>
        /// <returns>
        /// A <see cref="Model_StartupResult"/> indicating success or failure.
        /// </returns>
        public static Model_StartupResult InitializeParameterCache()
        {
            try
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                LoggingUtility.Log("[Startup] Initializing INFORMATION_SCHEMA parameter cache...");

                var cacheResult = InitializeParameterPrefixCacheInternal();
                stopwatch.Stop();

                if (cacheResult.IsSuccess)
                {
                    string msg = $"[Startup] Parameter prefix cache initialized successfully in {stopwatch.ElapsedMilliseconds}ms. Cached {Model_ParameterPrefix_Cache.ProcedureCount} stored procedures.";
                    LoggingUtility.Log(msg);
                    Console.WriteLine($"[Startup] Parameter cache: {Model_ParameterPrefix_Cache.ProcedureCount} procedures cached in {stopwatch.ElapsedMilliseconds}ms");
                    return Model_StartupResult.Success(msg);
                }
                else
                {
                    string errorMsg = $"Parameter prefix cache initialization failed: {cacheResult.ErrorMessage}. Using fallback convention-based detection.";
                    LoggingUtility.Log($"[Startup] Warning: {errorMsg}");
                    Console.WriteLine($"[Startup Warning] {errorMsg}");

                    return Model_StartupResult.Failure(errorMsg, null, new Dictionary<string, object>
                    {
                        ["IsCritical"] = false
                    });
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_StartupResult.Failure("Unexpected error initializing parameter cache", ex);
            }
        }

        /// <summary>
        /// Generates a user-friendly error message based on specific MySQL connection exceptions.
        /// </summary>
        /// <param name="ex">The <see cref="MySqlException"/> that occurred during connection.</param>
        /// <returns>
        /// A formatted, user-friendly error message string describing the specific connection issue
        /// (e.g., unknown database, access denied, timeout).
        /// </returns>
        public static string GetDatabaseConnectionErrorMessage(MySqlException ex)
        {
            if (ex.Message.Contains("Unknown database"))
            {
                string dbName = Model_Shared_Users.Database ?? "mtm_wip_application_winforms";
                string serverAddress = Model_Shared_Users.WipServerAddress;

#if DEBUG
                return $"The test database '{dbName}' does not exist on server '{serverAddress}'.\n\n" +
                       "This is a DEBUG build that requires the test database.\n\n" +
                       "Please:\n" +
                       "• Create the test database '{dbName}' on MySQL server\n" +
                       "• Or run the application in RELEASE mode to use the production database\n" +
                       "• Contact your system administrator for database setup assistance";
#else
                return $"The database '{dbName}' does not exist on server '{serverAddress}'.\n\n" +
                       "Please contact your system administrator to:\n" +
                       "• Verify the database server is running\n" +
                       "• Ensure the database '{dbName}' exists and is accessible\n" +
                       "• Check database permissions for your user account";
#endif
            }
            else if (ex.Message.Contains("Unable to connect to any of the specified MySQL hosts"))
            {
                return "Cannot connect to the database server.\n\n" +
                       "This usually means:\n" +
                       "• The database server is not running\n" +
                       "• The server address or port is incorrect\n" +
                       "• A firewall is blocking the connection\n\n" +
                       "Please check with your system administrator or verify the server is running.";
            }
            else if (ex.Message.Contains("Access denied"))
            {
                return "Access denied when connecting to the database.\n\n" +
                       "This usually means:\n" +
                       "• Your username or password is incorrect\n" +
                       "• Your account doesn't have permission to access the database\n\n" +
                       "Please check your credentials with your system administrator.";
            }
            else if (ex.Message.Contains("timeout") || ex.Message.Contains("Connection timeout"))
            {
                return "Connection to the database timed out.\n\n" +
                       "This usually means:\n" +
                       "• The database server is responding slowly\n" +
                       "• Network connectivity issues\n" +
                       "• The server is overloaded\n\n" +
                       "Please try starting the application again in a few moments.\n" +
                       "If the problem persists, contact your system administrator.";
            }
            else
            {
                return $"Database connection failed with the following error:\n\n{ex.Message}\n\n" +
                       "Please contact your system administrator for assistance.";
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Internal helper to query INFORMATION_SCHEMA.PARAMETERS and populate the <see cref="Model_ParameterPrefix_Cache"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Model_Dao_Result"/> indicating success or failure of the cache initialization.
        /// </returns>
        private static Model_Dao_Result InitializeParameterPrefixCacheInternal()
        {
            try
            {
                LoggingUtility.Log("[Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata");

                const string query = @"
                    SELECT
                        SPECIFIC_NAME AS ROUTINE_NAME,
                        PARAMETER_NAME,
                        PARAMETER_MODE
                    FROM INFORMATION_SCHEMA.PARAMETERS
                    WHERE SPECIFIC_SCHEMA = DATABASE()
                    AND ROUTINE_TYPE = 'PROCEDURE'
                    ORDER BY SPECIFIC_NAME, ORDINAL_POSITION";

                var cacheData = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

                using var connection = new MySqlConnection(Model_Application_Variables.ConnectionString);
                connection.Open();

                using var command = new MySqlCommand(query, connection);
                command.CommandTimeout = 10;

                using var reader = command.ExecuteReader();

                int parameterCount = 0;
                while (reader.Read())
                {
                    string routineName = reader.GetString("ROUTINE_NAME");
                    string parameterName = reader.GetString("PARAMETER_NAME");
                    string parameterMode = reader.GetString("PARAMETER_MODE");

                    if (!cacheData.ContainsKey(routineName))
                    {
                        cacheData[routineName] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    }

                    cacheData[routineName][parameterName] = parameterMode;
                    parameterCount++;
                }

                Model_ParameterPrefix_Cache.Initialize(cacheData);

                LoggingUtility.Log($"[Startup] Parameter cache populated: {cacheData.Count} procedures, {parameterCount} total parameters");

                return Model_Dao_Result.Success($"Parameter cache initialized: {cacheData.Count} procedures, {parameterCount} parameters");
            }
            catch (MySqlException ex)
            {
                string errorMsg = $"MySQL error querying INFORMATION_SCHEMA.PARAMETERS: {ex.Message}";
                LoggingUtility.LogDatabaseError(ex);
                return Model_Dao_Result.Failure(errorMsg, ex);
            }
            catch (Exception ex)
            {
                string errorMsg = $"Unexpected error initializing parameter cache: {ex.Message}";
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result.Failure(errorMsg, ex);
            }
        }

        #endregion
    }
}
