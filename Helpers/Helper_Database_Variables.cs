using System.Diagnostics;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers
{
    #region Helper_Database_Variables

    public static class Helper_Database_Variables
    {
        #region Connection String

        /// <summary>
        /// Builds MySQL connection string with immediate disposal pattern (Pooling=false).
        /// </summary>
        /// <param name="server">MySQL server address (defaults to Model_Shared_Users.WipServerAddress)</param>
        /// <param name="database">Database name (defaults to Model_Shared_Users.Database)</param>
        /// <param name="uid">User ID (defaults to "root")</param>
        /// <param name="password">Password (defaults to "root")</param>
        /// <returns>Complete MySQL connection string</returns>
        /// <remarks>
        /// Connection pooling is disabled (Pooling=false) per Constitution Principle V: Immediate Connection Disposal.
        /// Every database operation opens a fresh connection, performs its action, and closes immediately.
        /// This prevents connection leaks and simplifies debugging at the cost of ~10-20ms overhead per operation.
        /// </remarks>
        public static string GetConnectionString(string? server, string? database, string? uid, string? password)
        {
            try
            {
                server ??= Model_Shared_Users.WipServerAddress;
                database ??= Model_Shared_Users.Database;
                uid ??= "root";
                password ??= "root";
                
                return $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};" +
                       $"Allow User Variables=True;SslMode=Disabled;AllowPublicKeyRetrieval=true;" +
                       $"Pooling=false;";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Helper_Database_Variables] Error in GetConnectionString: {ex.Message}");
                return string.Empty;
            }
        }

        #endregion Connection String
    }

    #endregion
}
