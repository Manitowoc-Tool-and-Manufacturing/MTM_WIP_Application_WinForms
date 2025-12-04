using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.MainForm;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Forms.Splash;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    
    public class Service_Onstartup_StartupSplashApplicationContext : ApplicationContext
    {
        #region Fields

        private SplashScreenForm? _splashScreen;
        private MainForm? _mainForm;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Service_Onstartup_StartupSplashApplicationContext"/> class.
        /// </summary>
        public Service_Onstartup_StartupSplashApplicationContext()
        {
            try
            {

                _splashScreen = new SplashScreenForm();
                _splashScreen.Shown += async (s, e) => await RunStartupAsync();
                _splashScreen.FormClosed += SplashScreen_FormClosed;
                _splashScreen.Show();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                    contextData: new Dictionary<string, object> { ["MethodName"] = "ApplicationContext" },
                    callerName: "ApplicationContext_Constructor",
                    controlName: "StartupSplash_ApplicationContext");
                HandleStartupException(ex);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the FormClosed event of the splash screen.  Exits the application if the main form is not initialized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashScreen_FormClosed(object? sender, EventArgs e)
        {
            try
            {
                if (_mainForm == null || _mainForm.IsDisposed)
                {

                    ExitThread();
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["MethodName"] = "SplashScreen_FormClosed" },
                    callerName: "SplashScreen_FormClosed",
                    controlName: "StartupSplash_FormClosed");
                HandleStartupException(ex);
            }
        }

        #endregion

        #region Startup Sequence

        /// <summary>
        /// Runs the startup sequence asynchronously, updating the splash screen progress.
        /// </summary>
        private async Task RunStartupAsync()
        {
            try
            {

                int progress = 0;
                _splashScreen?.UpdateProgress(progress, "Starting startup sequence...");
                await Task.Delay(100);

                // 1. Initialize logging with error handling
                progress = 5;
                _splashScreen?.UpdateProgress(progress, "Initializing logging...");
                try
                {
                    await LoggingUtility.InitializeLoggingAsync();
                    progress = 10;
                    _splashScreen?.UpdateProgress(progress, "Logging initialized.");

                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.High,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Logging Initialization" },
                        callerName: "RunStartupAsync_LoggingInit",
                        controlName: "StartupSplash_LoggingInit");
                    ShowStartupStepError("Logging Initialization Failed", ex);
                    return;
                }
                await Task.Delay(50);

                // 2. Clean up old logs with error handling
                progress = 15;
                _splashScreen?.UpdateProgress(progress, "Cleaning up old logs...");
                try
                {
                    await LoggingUtility.CleanUpOldLogsIfNeededAsync();
                    progress = 20;
                    _splashScreen?.UpdateProgress(progress, "Old logs cleaned up.");

                }
                catch (Exception ex)
                {
                    // Log cleanup failure is not critical - continue startup
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Log Cleanup", ["IsCritical"] = false },
                        callerName: "RunStartupAsync_LogCleanup",
                        controlName: "StartupSplash_LogCleanup");

                }
                await Task.Delay(50);

                // 3. Clean app data folders with error handling
                progress = 25;
                _splashScreen?.UpdateProgress(progress, "Wiping app data folders...");
                try
                {
                    await Task.Run(() => Service_OnStartup_AppDataCleaner.WipeAppDataFolders());
                    progress = 30;
                    _splashScreen?.UpdateProgress(progress, "App data folders wiped.");

                }
                catch (Exception ex)
                {
                    // App data cleanup failure is not critical - continue startup
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "App Data Cleanup", ["IsCritical"] = false },
                        callerName: "RunStartupAsync_AppDataCleanup",
                        controlName: "StartupSplash_AppDataCleanup");

                }
                await Task.Delay(50);

                // 3.5. Shared Workstation Login
                if (Model_Application_Variables.User.Equals("SHOP2", StringComparison.OrdinalIgnoreCase) ||
                    Model_Application_Variables.User.Equals("MTMDC", StringComparison.OrdinalIgnoreCase))
                {
                    progress = 31;
                    _splashScreen?.UpdateProgress(progress, "Shared workstation detected. Please login...");
                    
                    using var loginForm = new Form_SharedLogin();
                    // Show dialog on top of splash screen
                    var result = loginForm.ShowDialog(_splashScreen);
                    
                    if (result == DialogResult.OK)
                    {
                        Model_Application_Variables.User = loginForm.ValidatedUsername;
                        LoggingUtility.Log($"[Startup] Shared workstation login successful. User: {Model_Application_Variables.User}");
                    }
                    else
                    {
                        // User cancelled or failed
                        LoggingUtility.Log("[Startup] Shared workstation login cancelled/failed. Exiting.");
                        _splashScreen?.Close();
                        return;
                    }
                }

                // 4. Load User Settings (Async)
                progress = 32;
                _splashScreen?.UpdateProgress(progress, "Loading user settings...");
                try
                {
                    await Service_OnStartup_User.LoadUserSettingsAsync();
                    progress = 35;
                    _splashScreen?.UpdateProgress(progress, "User settings loaded.");
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Load User Settings" },
                        callerName: "RunStartupAsync_LoadUserSettings",
                        controlName: "StartupSplash_LoadUserSettings");
                }
                await Task.Delay(50);

                // 5. Verify database connectivity using helper patterns - CRITICAL STEP
                progress = 38;
                _splashScreen?.UpdateProgress(progress, "Verifying database connectivity...");
                try
                {
                    var connectivityResult = await VerifyDatabaseConnectivityWithHelperAsync();
                    if (!connectivityResult.IsSuccess)
                    {

                        // Error already shown in VerifyDatabaseConnectivityWithHelperAsync
                        _splashScreen?.Close();
                        return;
                    }
                    progress = 40;
                    _splashScreen?.UpdateProgress(progress, "Database connectivity verified.");

                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object>
                        {
                            ["StartupStep"] = "Database Connectivity",
                            ["DatabaseName"] = Model_Shared_Users.Database ?? "mtm_wip_application_winforms",
                            ["ServerAddress"] = Model_Shared_Users.WipServerAddress
                        },
                        callerName: "RunStartupAsync_DatabaseConnectivity",
                        controlName: "StartupSplash_DatabaseConnectivity");
                    ShowStartupStepError("Database Connectivity Check Failed", ex);
                    return;
                }
                await Task.Delay(50);

                // 6. Initialize Parameter Cache (Async wrapper)
                progress = 45;
                _splashScreen?.UpdateProgress(progress, "Initializing parameter cache...");
                try
                {
                    await Task.Run(() => Service_OnStartup_Database.InitializeParameterCache());
                    progress = 48;
                    _splashScreen?.UpdateProgress(progress, "Parameter cache initialized.");
                }
                catch (Exception ex)
                {
                     Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Parameter Cache" },
                        callerName: "RunStartupAsync_ParameterCache",
                        controlName: "StartupSplash_ParameterCache");
                }
                await Task.Delay(50);

                // 7. Load User Access (Async)
                progress = 50;
                _splashScreen?.UpdateProgress(progress, "Verifying permissions...");
                try
                {
                    await Service_OnStartup_User.LoadUserAccessAsync();
                    
                    // User Not Found check
                    bool hasAccess = Model_Application_Variables.UserTypeNormal || 
                                     Model_Application_Variables.UserTypeReadOnly || 
                                     Model_Application_Variables.UserTypeAdmin || 
                                     Model_Application_Variables.UserTypeDeveloper;

                    if (!hasAccess)
                    {
                        ShowErrorDialog("User Not Found", 
                            "You do not currently have a username set. Please contact your supervisor.");
                        _splashScreen?.Close();
                        return;
                    }

                    progress = 55;
                    _splashScreen?.UpdateProgress(progress, "Permissions verified.");
                }
                catch (Exception ex)
                {
                     Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Load User Access" },
                        callerName: "RunStartupAsync_LoadUserAccess",
                        controlName: "StartupSplash_LoadUserAccess");
                }
                await Task.Delay(50);

                // 8. Setup data tables with error handling - CRITICAL STEP
                progress = 60;
                _splashScreen?.UpdateProgress(progress, "Setting up Data Tables...");
                try
                {
                    await Helper_UI_ComboBoxes.SetupDataTables();
                    progress = 50;
                    _splashScreen?.UpdateProgress(progress, "Data Tables set up.");

                }
                catch (MySqlException ex)
                {
                    Service_ErrorHandler.HandleDatabaseError(ex,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Data Tables Setup" },
                        callerName: "RunStartupAsync_DataTablesSetup_MySql",
                        controlName: "StartupSplash_DataTablesSetup");
                    string userMessage = GetUserFriendlyConnectionError(ex);
                    ShowErrorDialog("Data Tables Setup Failed", userMessage);
                    return;
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Data Tables Setup" },
                        callerName: "RunStartupAsync_DataTablesSetup_General",
                        controlName: "StartupSplash_DataTablesSetup");
                    ShowStartupStepError("Data Tables Setup Failed", ex);
                    return;
                }
                await Task.Delay(50);

                // 5.5. Load ColorCodeParts cache with error handling
                progress = 55;
                _splashScreen?.UpdateProgress(progress, "Loading color code cache...");
                try
                {
                    await Model_Application_Variables.ReloadColorCodePartsAsync();
                    progress = 58;
                    _splashScreen?.UpdateProgress(progress, "Color code cache loaded.");

                }
                catch (Exception ex)
                {
                    // Cache loading failure is not critical - continue startup with empty cache
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Color Code Cache", ["IsCritical"] = false },
                        callerName: "RunStartupAsync_ColorCodeCache",
                        controlName: "StartupSplash_ColorCodeCache");

                }
                await Task.Delay(50);

                // 6. Initialize version checker with error handling
                progress = 60;
                _splashScreen?.UpdateProgress(progress, "Initializing version checker...");
                try
                {
                    Service_Timer_VersionChecker.Initialize();
                    progress = 65;
                    _splashScreen?.UpdateProgress(progress, "Version checker initialized.");

                }
                catch (Exception ex)
                {
                    // Version checker failure is not critical - continue startup
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Version Checker", ["IsCritical"] = false },
                        callerName: "RunStartupAsync_VersionChecker",
                        controlName: "StartupSplash_VersionChecker");

                }
                await Task.Delay(50);

                // 7. Initialize theme system with error handling
                progress = 70;
                _splashScreen?.UpdateProgress(progress, "Initializing theme system...");
                try
                {
                    await Core_AppThemes.InitializeThemeSystemAsync(Model_Application_Variables.User);

                    // Initialize ThemeStore cache from Core_AppThemes
                    var themeStore = Program.ServiceProvider?.GetService<IThemeStore>();
                    if (themeStore != null)
                    {
                        await themeStore.LoadFromDatabaseAsync();

                    }
                    else
                    {

                    }

                    // Set ThemeManager's current theme to the user's preference (only if enabled)
                    var themeProvider = Program.ServiceProvider?.GetService<IThemeProvider>();
                    if (themeProvider != null && Model_Application_Variables.ThemeEnabled && !string.IsNullOrEmpty(Model_Application_Variables.ThemeName))
                    {
                        await themeProvider.SetThemeAsync(
                            Model_Application_Variables.ThemeName,
                            Core.Theming.ThemeChangeReason.Login,
                            Model_Application_Variables.User);

                    }
                    else if (!Model_Application_Variables.ThemeEnabled)
                    {

                    }
                    else
                    {

                    }

                    progress = 75;
                    _splashScreen?.UpdateProgress(progress, "Theme system initialized.");

                }
                catch (MySqlException ex)
                {
                    // Theme system might use database - show specific error
                    Service_ErrorHandler.HandleDatabaseError(ex,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Theme System", ["User"] = Model_Application_Variables.User },
                        callerName: "RunStartupAsync_ThemeSystem_MySql",
                        controlName: "StartupSplash_ThemeSystem");
                    string userMessage = GetUserFriendlyConnectionError(ex);
                    ShowErrorDialog("Theme System Initialization Failed", userMessage);
                    return;
                }
                catch (Exception ex)
                {
                    // Theme initialization failure is not critical - continue with defaults
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Theme System", ["IsCritical"] = false, ["User"] = Model_Application_Variables.User },
                        callerName: "RunStartupAsync_ThemeSystem_General",
                        controlName: "StartupSplash_ThemeSystem");

                }
                await Task.Delay(50);

                // 8. Load user context (no database dependency)
                progress = 80;
                _splashScreen?.UpdateProgress(progress, $"User Full Name loaded: {Model_Application_Variables.User}");

                await Task.Delay(50);

                // 9. Load theme settings with error handling
                progress = 85;
                _splashScreen?.UpdateProgress(progress, "Loading theme settings...");
                try
                {
                    await LoadThemeSettingsAsync();
                    progress = 90;
                    _splashScreen?.UpdateProgress(progress, "Theme settings loaded.");

                }
                catch (MySqlException ex)
                {
                    // Theme settings use database - show specific error
                    Service_ErrorHandler.HandleDatabaseError(ex,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Theme Settings", ["User"] = Model_Application_Variables.User },
                        callerName: "RunStartupAsync_ThemeSettings_MySql",
                        controlName: "StartupSplash_ThemeSettings");
                    string userMessage = GetUserFriendlyConnectionError(ex);
                    ShowErrorDialog("Theme Settings Loading Failed", userMessage);
                    return;
                }
                catch (Exception ex)
                {
                    // Theme settings failure is not critical - continue with defaults
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Theme Settings", ["IsCritical"] = false, ["User"] = Model_Application_Variables.User },
                        callerName: "RunStartupAsync_ThemeSettings_General",
                        controlName: "StartupSplash_ThemeSettings");

                }
                await Task.Delay(50);

                // 10. Complete core startup
                progress = 93;
                _splashScreen?.UpdateProgress(progress, "Startup sequence completed.");

                await Task.Delay(100);

                // 11. Create main form with error handling - CRITICAL STEP
                progress = 95;
                _splashScreen?.UpdateProgress(progress, "Creating main form...");
                try
                {
                    await Task.Delay(200);
                    _mainForm = new MainForm();
                    _mainForm.FormClosed += (s, e) => ExitThread();

                }
                catch (MySqlException ex)
                {
                    Service_ErrorHandler.HandleDatabaseError(ex,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Main Form Creation" },
                        callerName: "RunStartupAsync_MainFormCreation_MySql",
                        controlName: "StartupSplash_MainFormCreation");
                    string userMessage = GetUserFriendlyConnectionError(ex);
                    ShowErrorDialog("Main Form Creation Failed", userMessage);
                    return;
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Main Form Creation" },
                        callerName: "RunStartupAsync_MainFormCreation_General",
                        controlName: "StartupSplash_MainFormCreation");
                    ShowStartupStepError("Main Form Creation Failed", ex);
                    return;
                }

                // 12. Configure form instances with error handling
                progress = 97;
                _splashScreen?.UpdateProgress(progress, "Configuring form instances...");
                try
                {
                    ConfigureFormInstances();

                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Form Instance Configuration" },
                        callerName: "RunStartupAsync_FormConfiguration",
                        controlName: "StartupSplash_FormConfiguration");
                    ShowStartupStepError("Form Instance Configuration Failed", ex);
                    return;
                }

                // 13. Apply theme with error handling
                progress = 99;
                _splashScreen?.UpdateProgress(progress, "Applying theme...");
                try
                {
                    ApplyThemeToMainForm();

                }
                catch (Exception ex)
                {
                    // Theme application failure is not critical
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Theme Application", ["IsCritical"] = false },
                        callerName: "RunStartupAsync_ThemeApplication",
                        controlName: "StartupSplash_ThemeApplication");

                }

                // 14. Show application with error handling - CRITICAL STEP
                progress = 100;
                _splashScreen?.UpdateProgress(progress, "Ready to start!");
                await Task.Delay(500);

                try
                {
                    ShowMainForm();


                    if (_splashScreen != null)
                    {
                        _splashScreen.FormClosed -= SplashScreen_FormClosed;
                        _splashScreen.Close();

                    }
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object> { ["StartupStep"] = "Main Form Display" },
                        callerName: "RunStartupAsync_MainFormDisplay",
                        controlName: "StartupSplash_MainFormDisplay");
                    ShowStartupStepError("Main Form Display Failed", ex);
                    return;
                }
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);
                string userMessage = GetUserFriendlyConnectionError(ex);
                ShowErrorDialog("Database Error During Startup", userMessage);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                HandleStartupException(ex);
            }
        }



        #endregion

        #region Database Connectivity (Async)

        /// <summary>
        /// Verify database connectivity using helper patterns with async support
        /// </summary>
        /// <returns>Model_Dao_Result indicating connectivity status</returns>
        private async Task<Model_Dao_Result> VerifyDatabaseConnectivityWithHelperAsync()
        {
            try
            {


                // Use consistent timeout settings
                var connectionStringBuilder = new MySqlConnectionStringBuilder(Model_Application_Variables.ConnectionString)
                {
                    ConnectionTimeout = 30,
                    DefaultCommandTimeout = 30
                };

                using var connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
                await connection.OpenAsync();

                // Test database functionality with version query
                using var command = new MySqlCommand("SELECT VERSION() as mysql_version", connection);
                var version = await command.ExecuteScalarAsync();

                if (version != null)
                {

                    return Model_Dao_Result.Success($"Database connectivity verified. MySQL version: {version}");
                }
                else
                {
                    const string errorMsg = "Database version query returned null";

                    return Model_Dao_Result.Failure(errorMsg);
                }
            }
            catch (MySqlException ex)
            {
                LoggingUtility.LogDatabaseError(ex);

                string userMessage = GetUserFriendlyConnectionError(ex);

                // Use Service_ErrorHandler for consistent error UX with proper UI thread handling
                try
                {
                    Service_ErrorHandler.HandleException(
                        ex,
                        Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object>
                        {
                            ["DatabaseName"] = Model_Shared_Users.Database ?? "mtm_wip_application_winforms",
                            ["ServerAddress"] = Model_Shared_Users.WipServerAddress,
                            ["MethodName"] = "ValidateConnectivityAsync",
                            ["ErrorType"] = "MySqlException_StartupConnectivity"
                        },
                        controlName: "StartupSplash_DatabaseConnectivity");
                }
                catch (Exception msgBoxEx)
                {
                    // Fallback if Service_ErrorHandler fails
                    LoggingUtility.LogApplicationError(msgBoxEx);
                    Console.WriteLine($"[CRITICAL] Failed to show error dialog: {msgBoxEx.Message}");
                    Console.WriteLine($"[CRITICAL] Original database error: {userMessage}");
                }

                return Model_Dao_Result.Failure(userMessage, ex);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);

                string userMessage = "Unable to verify database connectivity during startup:\n\n" +
                                   $"{ex.Message}\n\n" +
                                   "Please check your network connection and database server status.\n" +
                                   "The application cannot start without database access.";

                // Use Service_ErrorHandler for consistent error UX
                try
                {
                    Service_ErrorHandler.HandleException(
                        ex,
                        Enum_ErrorSeverity.Fatal,
                        contextData: new Dictionary<string, object>
                        {
                            ["DatabaseName"] = Model_Shared_Users.Database ?? "mtm_wip_application_winforms",
                            ["ServerAddress"] = Model_Shared_Users.WipServerAddress,
                            ["MethodName"] = "ValidateConnectivityAsync",
                            ["ErrorType"] = "GeneralException_StartupConnectivity"
                        },
                        controlName: "StartupSplash_ConnectivityValidation");
                }
                catch (Exception handlerEx)
                {
                    // Fallback if Service_ErrorHandler fails
                    LoggingUtility.LogApplicationError(handlerEx);
                    Console.WriteLine($"[CRITICAL] Failed to show error dialog: {handlerEx.Message}");
                    Console.WriteLine($"[CRITICAL] Original error: {userMessage}");
                }

                return Model_Dao_Result.Failure(userMessage, ex);
            }
        }

        /// <summary>
        /// Get user-friendly error message for MySQL connection errors
        /// </summary>
        /// <param name="ex">MySQL exception</param>
        /// <returns>User-friendly error message</returns>
        private static string GetUserFriendlyConnectionError(MySqlException ex)
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
                       "• Or build and run the application in RELEASE mode\n" +
                       "• Contact your system administrator for database setup assistance\n\n" +
                       "The application cannot start without a valid database connection.";
#else
                return $"The database '{dbName}' does not exist on server '{serverAddress}'.\n\n" +
                       "Please contact your system administrator to:\n" +
                       "• Verify the database server is running\n" +
                       "• Ensure the database '{dbName}' exists and is accessible\n" +
                       "• Check database permissions for your user account\n\n" +
                       "The application cannot start without a valid database connection.";
#endif
            }

            if (ex.Message.Contains("Unable to connect to any of the specified MySQL hosts"))
            {
                return "Cannot connect to the database server during application startup.\n\n" +
                       "This usually means:\n" +
                       "• The database server is not running\n" +
                       "• The server address or port is incorrect\n" +
                       "• A firewall is blocking the connection\n\n" +
                       "Please verify the MySQL server is running and accessible.\n" +
                       "The application cannot start without a database connection.";
            }

            if (ex.Message.Contains("Access denied"))
            {
                return "Access denied when connecting to the database during startup.\n\n" +
                       "This usually means:\n" +
                       "• Your username or password is incorrect\n" +
                       "• Your account doesn't have permission to access the database\n\n" +
                       "Please check your database credentials with your system administrator.\n" +
                       "The application cannot start without proper database access.";
            }

            if (ex.Message.Contains("timeout"))
            {
                return "Database connection timed out during application startup.\n\n" +
                       "This usually means:\n" +
                       "• The database server is responding slowly\n" +
                       "• Network connectivity issues\n" +
                       "• The server is overloaded\n\n" +
                       "Please try starting the application again in a few moments.";
            }

            return $"Database connection failed during startup:\n\n{ex.Message}\n\n" +
                   "Please contact your system administrator for assistance.\n" +
                   "The application cannot start without a database connection.";
        }

        #endregion

        #region Exception Handling

        /// <summary>
        /// Handle startup exceptions with user-friendly messaging
        /// </summary>
        /// <param name="ex">Exception to handle</param>
        private void HandleStartupException(Exception ex)
        {
            try
            {
                string userMessage;

                if (ex is MySqlException mysqlEx)
                {
                    userMessage = GetUserFriendlyConnectionError(mysqlEx);
                }
                else
                {
                    userMessage = "A critical error occurred during application startup:\n\n" +
                                 $"{ex.Message}\n\n" +
                                 "The application will now close. Please check the log files for more details " +
                                 "and contact your system administrator if the problem persists.";
                }

                ShowErrorDialog("Startup Error", userMessage);
                _splashScreen?.Close();
            }
            catch (Exception innerEx)
            {
                // Fallback error handling for critical scenarios
                Console.WriteLine($"Critical error in startup exception handler: {innerEx.Message}");
                Console.WriteLine($"Original startup error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Show error for specific startup step failures
        /// </summary>
        /// <param name="stepName">Name of the failed startup step</param>
        /// <param name="ex">Exception that occurred</param>
        private void ShowStartupStepError(string stepName, Exception ex)
        {
            try
            {
                string userMessage;

                if (ex is MySqlException mysqlEx)
                {
                    userMessage = GetUserFriendlyConnectionError(mysqlEx);
                }
                else
                {
                    userMessage = $"Failed to complete startup step: {stepName}\n\n" +
                                 $"Error: {ex.Message}\n\n" +
                                 "The application cannot continue. Please check the log files for more details " +
                                 "and contact your system administrator if the problem persists.";
                }

                ShowErrorDialog(stepName, userMessage);
                _splashScreen?.Close();
            }
            catch (Exception innerEx)
            {
                // Fallback error handling
                Console.WriteLine($"Error showing startup step error: {innerEx.Message}");
                Console.WriteLine($"Original error in {stepName}: {ex.Message}");
            }
        }

        /// <summary>
        /// Show error dialog with proper thread handling using Service_ErrorHandler
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="message">Error message</param>
        private void ShowErrorDialog(string title, string message)
        {
            try
            {
                Service_ErrorHandler.HandleException(
                    new Exception(message),
                    Enum_ErrorSeverity.High,
                    contextData: new Dictionary<string, object>
                    {
                        ["Title"] = title,
                        ["MethodName"] = "ShowErrorDialog",
                        ["ErrorType"] = "StartupError"
                    },
                    controlName: "StartupSplash_ErrorDialog");
            }
            catch (Exception ex)
            {
                // Final fallback
                Console.WriteLine($"Failed to show error dialog: {ex.Message}");
                Console.WriteLine($"Original error message: {message}");
            }
        }

        #endregion

        #region Form Configuration

        /// <summary>
        /// Load theme settings using established DAO patterns
        /// </summary>
        private async Task LoadThemeSettingsAsync()
        {
            try
            {


                // Load theme enabled/disabled setting
                var themeEnabledResult = await Dao_User.GetThemeEnabledAsync(Model_Application_Variables.User);
                Model_Application_Variables.ThemeEnabled = themeEnabledResult.Data; // Defaults to true

                // Load animations enabled setting
                var animationsResult = await Dao_User.GetAnimationsEnabledAsync(Model_Application_Variables.User);
                Model_Application_Variables.AnimationsEnabled = animationsResult.IsSuccess ? animationsResult.Data : true;

                int? fontSize = await Dao_User.GetThemeFontSizeAsync(Model_Application_Variables.User);
                Model_Application_Variables.ThemeFontSize = fontSize ?? 9;

                Model_Application_Variables.UserUiColors = await Core_Themes.GetUserThemeColorsAsync(Model_Application_Variables.User);


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = "LoadThemeSettingsAsync",
                        ["User"] = Model_Application_Variables.User,
                        ["IsCritical"] = false
                    },
                    callerName: "LoadThemeSettingsAsync",
                    controlName: "StartupSplash_LoadThemeSettings");
                // Set defaults if theme loading fails
                Model_Application_Variables.ThemeEnabled = true;
                Model_Application_Variables.ThemeFontSize = 9;

            }
        }

        /// <summary>
        /// Configure form instances with proper error handling
        /// </summary>
        private void ConfigureFormInstances()
        {
            try
            {
                if (_mainForm == null) return;

                Control_RemoveTab.MainFormInstance = _mainForm;
                Control_InventoryTab.MainFormInstance = _mainForm;
                Control_TransferTab.MainFormInstance = _mainForm;
                Control_AdvancedInventory.MainFormInstance = _mainForm;
                Control_AdvancedRemove.MainFormInstance = _mainForm;
                Control_QuickButtons.MainFormInstance = _mainForm;
                Helper_UI_ComboBoxes.MainFormInstance = _mainForm;
                Service_Timer_VersionChecker.MainFormInstance = _mainForm;


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                    contextData: new Dictionary<string, object> { ["MethodName"] = "ConfigureFormInstances" },
                    callerName: "ConfigureFormInstances",
                    controlName: "StartupSplash_ConfigureFormInstances");
                throw new InvalidOperationException("Failed to configure form instances", ex);
            }
        }

        /// <summary>
        /// Apply theme to MainForm with thread safety
        /// </summary>
        private void ApplyThemeToMainForm()
        {
            try
            {
                if (_mainForm == null) return;

                // MIGRATION NOTE: MainForm now inherits from ThemedForm which automatically
                // applies themes via the new dependency injection system. Calling Core_Themes.ApplyTheme()
                // here would OVERRIDE the ThemedForm colors with fallback values, causing the
                // black and white appearance instead of the actual theme colors.
                // The theme is already applied by ThemedForm.Shown event handler.

                // OLD CODE - DO NOT RE-ENABLE:
                // if (_mainForm.InvokeRequired)
                // {
                //     _mainForm.Invoke(new Action(() => Core_Themes.ApplyTheme(_mainForm)));
                // }
                // else
                // {
                //     Core_Themes.ApplyTheme(_mainForm);
                // }


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = "ApplyThemeToMainForm",
                        ["IsCritical"] = false
                    },
                    callerName: "ApplyThemeToMainForm",
                    controlName: "StartupSplash_ApplyThemeToMainForm");
                // Theme application failure is not critical for startup

            }
        }

        /// <summary>
        /// Show MainForm with thread safety
        /// </summary>
        private void ShowMainForm()
        {
            try
            {
                if (_mainForm == null) return;

                if (_mainForm.InvokeRequired)
                {
                    _mainForm.Invoke(new Action(() => _mainForm.Show()));
                }
                else
                {
                    _mainForm.Show();
                }


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Fatal,
                    contextData: new Dictionary<string, object> { ["MethodName"] = "ShowMainForm" },
                    callerName: "ShowMainForm",
                    controlName: "StartupSplash_ShowMainForm");
                throw new InvalidOperationException("Failed to show MainForm", ex);
            }
        }

        #endregion
    }
}
