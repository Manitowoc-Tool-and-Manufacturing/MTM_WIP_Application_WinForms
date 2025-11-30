using System.Diagnostics;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers
{
    #region Helper_Database_Variables

    public static class Helper_Database_Variables
    {
        #region Connection String

        public static string GetConnectionString(string? server, string? database, string? uid, string? password)
        {
            try
            {
                server ??= Model_Shared_Users.WipServerAddress;
                database ??= Model_Shared_Users.Database;
                uid ??= "root";
                password ??= "root";
                return $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};Allow User Variables=True;SslMode=none;AllowPublicKeyRetrieval=true;";
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return string.Empty;
            }
        }

        #endregion Connection String

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

        #endregion Log File Path
    }

    #endregion
}
