using System.Diagnostics;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers
{
    #region Helper_Database_Variables

    public static class Helper_Database_Variables
    {
        #region Database Configuration Constants

        /// <summary>
        /// Test database name for integration testing. Matches the database created during test setup.
        /// </summary>
        public const string TestDatabaseName = "mtm_wip_application_winforms_test";

        #endregion

        #region Connection String

        public static string GetConnectionString(string? server, string? database, string? uid, string? password)
        {
            try
            {
                server ??= Model_Shared_Users.WipServerAddress;
                database ??= Model_Shared_Users.Database;
                uid ??= "root";  // Use root user for MAMP MySQL
                password ??= "root";  // Use root password for MAMP MySQL
                return $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};Allow User Variables=True;SslMode=none;AllowPublicKeyRetrieval=true;";
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return string.Empty;
            }
        }

        #endregion

        #region Log File Path

        public static async Task<string> GetLogFilePathAsync(string server, string userName)
        {
            try
            {
                string logDirectory = Environment.UserName.Equals("johnk", StringComparison.OrdinalIgnoreCase)
                    ? @"C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs"
                    : @"X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs";

                string userDirectory = Path.Combine(logDirectory, userName);

                using CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));

                try
                {
                    await Task.Run(() =>
                    {
                        if (!Directory.Exists(userDirectory))
                        {
                            Directory.CreateDirectory(userDirectory);
                        }
                    }, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    throw new TimeoutException($"Directory creation timed out for: {userDirectory}");
                }

                string timestamp = DateTime.Now.ToString("MM-dd-yyyy @ h-mm tt");
                string logFileName = $"{userName} {timestamp}.csv";
                return Path.Combine(userDirectory, logFileName);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[DEBUG] Error in GetLogFilePathAsync: {ex.Message}");
                throw;
            }
        }

        // Keep the synchronous version for backward compatibility, but mark as obsolete
        [Obsolete("Use GetLogFilePathAsync for better async performance")]
        public static string GetLogFilePath(string server, string userName) =>
            GetLogFilePathAsync(server, userName).GetAwaiter().GetResult();

        #endregion
    }

    #endregion
}
