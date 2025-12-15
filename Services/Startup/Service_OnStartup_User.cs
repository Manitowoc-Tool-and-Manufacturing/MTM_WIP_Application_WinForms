using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    public static class Service_OnStartup_User
    {
        public static Model_StartupResult IdentifyUser()
        {
            try
            {
                Model_Application_Variables.User = Dao_System.System_GetUserName();
                LoggingUtility.Log($"[Startup] User identified: {Model_Application_Variables.User}");
                return Model_StartupResult.Success($"User identified: {Model_Application_Variables.User}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Startup Error] User identification failed: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);

                // Use fallback user identification
                try
                {
                    Model_Application_Variables.User = Environment.UserName ?? "Unknown";
                    string msg = $"Could not identify user through normal methods. Using system username '{Model_Application_Variables.User}' as fallback.";
                    LoggingUtility.Log($"[Startup] {msg}");

                    return Model_StartupResult.Failure(msg, ex, new Dictionary<string, object> { ["IsCritical"] = false });
                }
                catch (Exception fallbackEx)
                {
                    Model_Application_Variables.User = "SHOP2";
                    LoggingUtility.LogApplicationError(fallbackEx);
                    return Model_StartupResult.Failure("Failed to identify current user. Using 'SHOP2' as username.", fallbackEx, new Dictionary<string, object> { ["IsCritical"] = false });
                }
            }
        }

        public static async Task LoadUserSettingsAsync()
        {
            try
            {
                var serverResult = await Dao_User.GetWipServerAddressAsync(Model_Application_Variables.User);
                var portResult = await Dao_User.GetWipServerPortAsync(Model_Application_Variables.User);
                var databaseResult = await Dao_User.GetDatabaseAsync(Model_Application_Variables.User);

                if (serverResult.IsSuccess && !string.IsNullOrWhiteSpace(serverResult.Data))
                {
                    Model_Shared_Users.WipServerAddress = serverResult.Data;
                    LoggingUtility.Log($"[Startup] Loaded user database server setting: {serverResult.Data}");
                }

                if (portResult.IsSuccess && !string.IsNullOrWhiteSpace(portResult.Data))
                {
                    Model_Shared_Users.WipServerPort = portResult.Data;
                    LoggingUtility.Log($"[Startup] Loaded user database port setting: {portResult.Data}");
                }

                if (databaseResult.IsSuccess && !string.IsNullOrWhiteSpace(databaseResult.Data))
                {
                    // Validate database name
                    string dbName = databaseResult.Data;

                    // Allow any database name that the user has configured
                    // Connectivity validation happens in CheckConnectivityAsync below
#if !DEBUG
                    Model_Shared_Users.Database = dbName;
                    LoggingUtility.Log($"[Startup] Loaded user database name setting: {dbName}");
#else
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        Model_Shared_Users.Database = "mtm_wip_application_winforms";
                        LoggingUtility.Log($"[Startup] Debugger attached: Ignoring user database setting '{dbName}' and enforcing test database.");
                    }
                    else
                    {
                        Model_Shared_Users.Database = dbName;
                        LoggingUtility.Log($"[Startup] Debug configuration (No Debugger): Loaded user database name setting: {dbName}");
                    }
#endif
                }

                // Check and set default Infor Visual credentials if missing
                var visualUserResult = await Dao_User.GetVisualUserNameAsync(Model_Application_Variables.User);
                var visualPassResult = await Dao_User.GetVisualPasswordAsync(Model_Application_Variables.User);

                string? visualUser = visualUserResult.IsSuccess ? visualUserResult.Data : null;
                string? visualPass = visualPassResult.IsSuccess ? visualPassResult.Data : null;

                if (string.IsNullOrWhiteSpace(visualUser) || string.IsNullOrWhiteSpace(visualPass))
                {
                    LoggingUtility.Log($"[Startup] Missing Infor Visual credentials for user {Model_Application_Variables.User}. Setting defaults.");

                    string defaultUser = "SHOP2";
                    string defaultPass = "SHOP";

                    // Update Memory
                    Model_Application_Variables.VisualUserName = defaultUser;
                    Model_Application_Variables.VisualPassword = defaultPass;
                    Model_Shared_Users.VisualUserName = defaultUser;
                    Model_Shared_Users.VisualPassword = defaultPass;

                    // Update Database
                    await Dao_User.SetVisualUserNameAsync(Model_Application_Variables.User, defaultUser);
                    await Dao_User.SetVisualPasswordAsync(Model_Application_Variables.User, defaultPass);

                    LoggingUtility.Log($"[Startup] Assigned default Infor Visual credentials (SHOP2/SHOP) to user {Model_Application_Variables.User}.");
                }
                else
                {
                    // Populate memory if they exist
                    Model_Application_Variables.VisualUserName = visualUser;
                    Model_Application_Variables.VisualPassword = visualPass;
                    // Note: Dao_User.GetVisualUserNameAsync/GetVisualPasswordAsync already set Model_Shared_Users properties
                }
            }
            catch (Exception ex)
            {
                // If we can't load user settings, use defaults (already set in Model_Shared_Users)
                LoggingUtility.Log($"[Startup] Could not load user database settings, using defaults: {ex.Message}");
            }
        }

        public static async Task<Model_StartupResult> LoadUserAccessAsync()
        {
            try
            {
                await Dao_System.System_UserAccessTypeAsync();
                return Model_StartupResult.Success("User access loaded");
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                string userMessage = Service_OnStartup_Database.GetDatabaseConnectionErrorMessage(ex);
                return Model_StartupResult.Failure(userMessage, ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_StartupResult.Failure("Failed to load user access", ex);
            }
        }
    }
}
