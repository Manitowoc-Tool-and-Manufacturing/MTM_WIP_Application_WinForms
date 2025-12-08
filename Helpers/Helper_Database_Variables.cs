using System.Diagnostics;
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
                Debug.WriteLine($"[Helper_Database_Variables] Error in GetConnectionString: {ex.Message}");
                return string.Empty;
            }
        }

        #endregion Connection String
    }

    #endregion
}
