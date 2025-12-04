using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    public static class Service_OnStartup_User
    {
        public static void IdentifyUser()
        {
            try
            {
                Model_Application_Variables.User = Dao_System.System_GetUserName();
                LoggingUtility.Log($"[Startup] User identified: {Model_Application_Variables.User}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Startup Error] User identification failed: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);

                // Use fallback user identification
                try
                {
                    Model_Application_Variables.User = Environment.UserName ?? "Unknown";
                    LoggingUtility.Log($"[Startup] Using fallback user identification: {Model_Application_Variables.User}");

                    Service_OnStartup_AppLifecycle.ShowNonCriticalError("User Identification Warning",
                        $"Could not identify user through normal methods:\n\n{ex.Message}\n\n" +
                        $"Using system username '{Model_Application_Variables.User}' as fallback.\n" +
                        "Some user-specific features may not work correctly.");
                }
                catch (Exception fallbackEx)
                {
                    Model_Application_Variables.User = "SHOP2";
                    LoggingUtility.LogApplicationError(fallbackEx);
                    Service_OnStartup_AppLifecycle.ShowNonCriticalError("User Identification Error",
                        "Failed to identify current user. Using 'Unknown' as username.\n" +
                        "This may affect user-specific settings and permissions.");
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
                    Model_Shared_Users.Database = dbName;
                    LoggingUtility.Log($"[Startup] Loaded user database name setting: {dbName}");
                }
            }
            catch (Exception ex)
            {
                // If we can't load user settings, use defaults (already set in Model_Shared_Users)
                LoggingUtility.Log($"[Startup] Could not load user database settings, using defaults: {ex.Message}");
            }
        }

        public static async Task LoadUserAccessAsync()
        {
            try
            {
                await Dao_System.System_UserAccessTypeAsync();
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                string userMessage = Service_OnStartup_Database.GetDatabaseConnectionErrorMessage(ex);

                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Fatal,
                    retryAction: () => { 
                        Application.Restart();
                        Environment.Exit(0);
                        return true; 
                    },
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["DatabaseName"] = Model_Shared_Users.Database ?? "mtm_wip_application_winforms",
                        ["ServerAddress"] = Model_Shared_Users.WipServerAddress,
                        ["MethodName"] = "System_UserAccessTypeAsync",
                        ["ErrorType"] = "UserAccessLoading_MySqlException"
                    },
                    controlName: "Program_Main_UserAccessLoading");

                LoggingUtility.Log("[Startup] Exiting after user access loading failure");
                Environment.Exit(1);
            }
            catch (TimeoutException ex)
            {
                LoggingUtility.LogApplicationError(ex);

                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.High,
                    retryAction: () => { 
                        Application.Restart();
                        Environment.Exit(0);
                        return true; 
                    },
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["MethodName"] = "System_UserAccessTypeAsync",
                        ["ErrorType"] = "UserAccessLoading_Timeout"
                    },
                    controlName: "Program_Main_UserAccessTimeout");
            }
            catch (UnauthorizedAccessException ex)
            {
                LoggingUtility.LogApplicationError(ex);

                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Fatal,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["MethodName"] = "System_UserAccessTypeAsync",
                        ["ErrorType"] = "UserAccessLoading_UnauthorizedAccess"
                    },
                    controlName: "Program_Main_UnauthorizedAccess");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);

                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Fatal,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["MethodName"] = "System_UserAccessTypeAsync",
                        ["ErrorType"] = "UserAccessLoading_GeneralException",
                        ["ExceptionType"] = ex.GetType().Name
                    },
                    controlName: "Program_Main_UserAccessError");
            }
        }
    }
}
