using MTM_WIP_Application_Winforms.Controls.SettingsForm;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Data;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using System.Security;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    public static class Service_OnStartup_AppLifecycle
    {
        /// <summary>
        /// Configures global exception handlers for the application domain and UI thread.
        /// </summary>
        /// <remarks>
        /// Sets up listeners for <see cref="Application.ThreadException"/> and <see cref="AppDomain.UnhandledException"/>
        /// to ensure all uncaught exceptions are logged and handled gracefully before termination.
        /// </remarks>
        public static void SetupGlobalExceptionHandling()
        {
            Application.ThreadException += (sender, args) =>
            {
                Console.WriteLine($"[Global Exception] ThreadException: {args.Exception}");
                Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                {
                    ["ExceptionType"] = args.Exception.GetType().Name,
                    ["ExceptionMessage"] = args.Exception.Message
                }, "ThreadExceptionHandler", "Program");

                try
                {
                    LoggingUtility.LogApplicationError(args.Exception);
                }
                catch (Exception loggingEx)
                {
                    Console.WriteLine($"[Critical] Failed to log thread exception: {loggingEx.Message}");
                }
                HandleGlobalException(args.Exception);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (args.ExceptionObject is Exception ex)
                {
                    Console.WriteLine($"[Global Exception] UnhandledException: {ex}");
                    Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                    {
                        ["ExceptionType"] = ex.GetType().Name,
                        ["ExceptionMessage"] = ex.Message,
                        ["IsTerminating"] = args.IsTerminating
                    }, "UnhandledExceptionHandler", "Program");

                    try
                    {
                        LoggingUtility.LogApplicationError(ex);
                    }
                    catch (Exception loggingEx)
                    {
                        Console.WriteLine($"[Critical] Failed to log unhandled exception: {loggingEx.Message}");
                    }
                    HandleGlobalException(ex);
                }
            };
        }

        /// <summary>
        /// Registers event handlers for application shutdown cleanup.
        /// </summary>
        /// <remarks>
        /// Hooks into <see cref="AppDomain.ProcessExit"/> and <see cref="Application.ApplicationExit"/>
        /// to trigger cleanup routines when the application closes.
        /// </remarks>
        public static void SetupCleanupHandlers()
        {
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => PerformAppCleanup();
            Application.ApplicationExit += (sender, args) => PerformAppCleanup();
        }

        /// <summary>
        /// Executes cleanup operations including temporary file deletion and memory management.
        /// </summary>
        /// <remarks>
        /// Attempts to clean up temp files from <see cref="Control_About"/> and forces garbage collection.
        /// Failures during cleanup are logged but do not stop the shutdown process.
        /// </remarks>
        public static void PerformAppCleanup()
        {
            try
            {
                LoggingUtility.Log("[Cleanup] Starting application cleanup");

                try
                {
                    Control_About.CleanupAllTempFiles();
                    LoggingUtility.Log("[Cleanup] Control_About temp files cleaned up successfully");
                }
                catch (UnauthorizedAccessException ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Console.WriteLine($"[Cleanup Warning] Access denied cleaning temp files: {ex.Message}");
                }
                catch (IOException ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Console.WriteLine($"[Cleanup Warning] IO error cleaning temp files: {ex.Message}");
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Console.WriteLine($"[Cleanup Warning] Error cleaning Control_About temp files: {ex.Message}");
                }

                try
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    LoggingUtility.Log("[Cleanup] Memory cleanup completed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Cleanup Warning] Error during memory cleanup: {ex.Message}");
                }

                LoggingUtility.Log("[Cleanup] Application cleanup completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Cleanup Warning] Error during application cleanup: {ex.Message}");
                try
                {
                    LoggingUtility.LogApplicationError(ex);
                }
                catch
                {
                    Console.WriteLine("[Cleanup Warning] Failed to log cleanup error");
                }
            }
        }

        /// <summary>
        /// Orchestrates the application startup sequence.
        /// </summary>
        /// <param name="progress">Progress reporter.</param>
        /// <param name="sharedLoginCallback">Callback for shared workstation login.</param>
        /// <returns>A <see cref="Model_StartupResult"/> indicating overall success or failure.</returns>
        public static async Task<Model_StartupResult> ExecuteStartupSequenceAsync(
            IProgress<(int percent, string message)> progress,
            Func<Task<bool>> sharedLoginCallback)
        {
            try
            {
                // 1. Initialize logging
                progress.Report((5, "Initializing logging..."));
                await LoggingUtility.InitializeLoggingAsync();
                progress.Report((10, "Logging initialized."));

                // 2. Clean up old logs
                progress.Report((15, "Cleaning up old logs..."));
                await LoggingUtility.CleanUpOldLogsIfNeededAsync();
                progress.Report((20, "Old logs cleaned up."));

                // 3. Clean app data folders
                progress.Report((25, "Wiping app data folders..."));
                await Task.Run(() => Service_OnStartup_AppDataCleaner.WipeAppDataFolders());
                progress.Report((30, "App data folders wiped."));

                // 3.5. Shared Workstation Login
                if (Model_Application_Variables.User.Equals("SHOP2", StringComparison.OrdinalIgnoreCase) ||
                    Model_Application_Variables.User.Equals("MTMDC", StringComparison.OrdinalIgnoreCase))
                {
                    progress.Report((31, "Shared workstation detected. Please login..."));
                    bool loginSuccess = await sharedLoginCallback();
                    if (!loginSuccess)
                    {
                        return Model_StartupResult.Failure("Shared workstation login cancelled or failed.");
                    }
                }

                // 4. Load User Settings
                progress.Report((32, "Loading user settings..."));
                await Service_OnStartup_User.LoadUserSettingsAsync();
                progress.Report((35, "User settings loaded."));

                // 5. Verify database connectivity
                progress.Report((38, "Verifying database connectivity..."));
                var connectivityResult = await Service_OnStartup_Database.ValidateConnectivityAsync();
                if (!connectivityResult.IsSuccess)
                {
                    return connectivityResult;
                }
                progress.Report((40, "Database connectivity verified."));

                // 6. Initialize Parameter Cache
                progress.Report((45, "Initializing parameter cache..."));
                var cacheResult = await Task.Run(() => Service_OnStartup_Database.InitializeParameterCache());
                if (!cacheResult.IsSuccess)
                {
                    // Non-critical, just log/warn (already handled in service, but we can check context)
                    if (cacheResult.Context != null && cacheResult.Context.ContainsKey("IsCritical") && (bool)cacheResult.Context["IsCritical"] == false)
                    {
                        // Continue
                    }
                    else
                    {
                        return cacheResult;
                    }
                }
                progress.Report((48, "Parameter cache initialized."));

                // 7. Load User Access
                progress.Report((50, "Verifying permissions..."));
                var accessResult = await Service_OnStartup_User.LoadUserAccessAsync();
                if (!accessResult.IsSuccess)
                {
                    return accessResult;
                }

                // User Not Found check
                bool hasAccess = Model_Application_Variables.UserTypeNormal || 
                                 Model_Application_Variables.UserTypeReadOnly || 
                                 Model_Application_Variables.UserTypeAdmin || 
                                 Model_Application_Variables.UserTypeDeveloper;

                if (!hasAccess)
                {
                    LoggingUtility.Log($"[Startup] User '{Model_Application_Variables.User}' not found in database. Showing Quick User Creation form.");
                    
                    // User not found - show Quick User Creation form
                    progress.Report((52, "User not found. Please create user account..."));
                    
                    using var createUserForm = new Forms.Shared.Form_QuickUserCreation();
                    var createUserResult = createUserForm.ShowDialog();
                    
                    if (createUserResult == DialogResult.Cancel)
                    {
                        LoggingUtility.Log("[Startup] User chose to exit application instead of creating account.");
                        return Model_StartupResult.Failure("Application startup cancelled by user.");
                    }
                    
                    if (createUserResult != DialogResult.OK)
                    {
                        LoggingUtility.Log("[Startup] User creation form closed without successful user creation.");
                        return Model_StartupResult.Failure("User creation was not completed. Application cannot continue.");
                    }
                    
                    // User was created - reload user access
                    LoggingUtility.Log($"[Startup] User created successfully. Reloading user access for: {Model_Application_Variables.User}");
                    progress.Report((54, "User created. Loading permissions..."));
                    
                    var reloadAccessResult = await Service_OnStartup_User.LoadUserAccessAsync();
                    if (!reloadAccessResult.IsSuccess)
                    {
                        return reloadAccessResult;
                    }
                    
                    // Verify user now has access
                    hasAccess = Model_Application_Variables.UserTypeNormal || 
                                Model_Application_Variables.UserTypeReadOnly || 
                                Model_Application_Variables.UserTypeAdmin || 
                                Model_Application_Variables.UserTypeDeveloper;
                    
                    if (!hasAccess)
                    {
                        LoggingUtility.Log($"[Startup] User '{Model_Application_Variables.User}' still does not have access after creation.");
                        return Model_StartupResult.Failure("User was created but permissions could not be verified. Please contact your system administrator.");
                    }
                }
                progress.Report((55, "Permissions verified."));

                // 8. Setup Data Tables
                progress.Report((60, "Setting up Data Tables..."));
                await Helper_UI_ComboBoxes.SetupDataTables();
                progress.Report((65, "Data Tables set up."));

                // 9. Load Color Code Cache
                progress.Report((66, "Loading color code cache..."));
                try 
                {
                    await Model_Application_Variables.ReloadColorCodePartsAsync();
                } 
                catch (Exception ex) 
                {
                    LoggingUtility.LogApplicationError(ex);
                }
                progress.Report((68, "Color code cache loaded."));

                // 10. Initialize Version Checker
                progress.Report((70, "Initializing version checker..."));
                try 
                {
                    Service_Timer_VersionChecker.Initialize();
                } 
                catch (Exception ex) 
                {
                    LoggingUtility.LogApplicationError(ex);
                }
                progress.Report((75, "Version checker initialized."));

                // 11. Initialize Theme System
                progress.Report((80, "Initializing theme system..."));
                try 
                {
                    await Core_AppThemes.InitializeThemeSystemAsync(Model_Application_Variables.User);

                    // Initialize ThemeStore cache
                    var themeStore = Program.ServiceProvider?.GetService<IThemeStore>();
                    if (themeStore != null)
                    {
                        await themeStore.LoadFromDatabaseAsync();
                    }

                    // Set ThemeManager's current theme
                    var themeProvider = Program.ServiceProvider?.GetService<IThemeProvider>();
                    if (themeProvider != null && Model_Application_Variables.ThemeEnabled && !string.IsNullOrEmpty(Model_Application_Variables.ThemeName))
                    {
                        await themeProvider.SetThemeAsync(
                            Model_Application_Variables.ThemeName,
                            Core.Theming.ThemeChangeReason.Login,
                            Model_Application_Variables.User);
                    }
                } 
                catch (Exception ex) 
                {
                    LoggingUtility.LogApplicationError(ex);
                }
                progress.Report((85, "Theme system initialized."));

                // 12. Load Theme Settings
                progress.Report((90, "Loading theme settings..."));
                try
                {
                    // Load theme enabled/disabled setting
                    var themeEnabledResult = await Dao_User.GetThemeEnabledAsync(Model_Application_Variables.User);
                    Model_Application_Variables.ThemeEnabled = themeEnabledResult.Data;

                    // Load animations enabled setting
                    var animationsResult = await Dao_User.GetAnimationsEnabledAsync(Model_Application_Variables.User);
                    Model_Application_Variables.AnimationsEnabled = animationsResult.IsSuccess ? animationsResult.Data : true;

                    int? fontSize = await Dao_User.GetThemeFontSizeAsync(Model_Application_Variables.User);
                    Model_Application_Variables.ThemeFontSize = fontSize ?? 9;

                    Model_Application_Variables.UserUiColors = await Core_Themes.GetUserThemeColorsAsync(Model_Application_Variables.User);
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    // Set defaults
                    Model_Application_Variables.ThemeEnabled = true;
                    Model_Application_Variables.ThemeFontSize = 9;
                }
                progress.Report((93, "Theme settings loaded."));

                return Model_StartupResult.Success("Startup sequence completed successfully.");
            }
            catch (Exception ex)
            {
                return Model_StartupResult.Failure("Unexpected error during startup sequence.", ex);
            }
        }

        /// <summary>
        /// Centralized handler for processing unhandled exceptions based on their type.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <remarks>
        /// Analyzes the exception type (MySQL, IO, Security, etc.) and displays appropriate
        /// user-friendly error messages. For critical database errors, it may attempt to restart the application.
        /// </remarks>
        public static void HandleGlobalException(Exception ex)
        {
            try
            {
                if (ex is MySqlException mysqlEx)
                {
                    string userMessage = Service_OnStartup_Database.GetDatabaseConnectionErrorMessage(mysqlEx);

                    _ = Service_ErrorHandler.HandleException(
                        mysqlEx,
                        Enum_ErrorSeverity.Fatal,
                        retryAction: () =>
                        {
                            // Restart application
                            Application.Restart();
                            Environment.Exit(0);
                            return true;
                        },
                        contextData: new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["DatabaseName"] = Model_Shared_Users.Database ?? "mtm_wip_application_winforms",
                            ["ServerAddress"] = Model_Shared_Users.WipServerAddress,
                            ["MethodName"] = "HandleGlobalException",
                            ["ErrorType"] = "GlobalMySqlException"
                        },
                        controlName: "Program_GlobalExceptionHandler_MySQL");
                }
                else if (ex is InvalidOperationException && ex.Message.Contains("database"))
                {
                    ShowDatabaseError("Database Configuration Error",
                        $"Database configuration error:\n\n{ex.Message}\n\n" +
                        "Please contact your system administrator for assistance.");
                }
                else if (ex is FileNotFoundException fileEx)
                {
                    ShowFileSystemError("Missing File Error",
                        $"A required file could not be found:\n\n{fileEx.FileName ?? "Unknown file"}\n\n" +
                        $"Error: {fileEx.Message}\n\n" +
                        "Please reinstall the application or contact your system administrator.");
                }
                else if (ex is DirectoryNotFoundException dirEx)
                {
                    ShowFileSystemError("Missing Directory Error",
                        $"A required directory could not be found:\n\n{dirEx.Message}\n\n" +
                        "Please reinstall the application or contact your system administrator.");
                }
                else if (ex is UnauthorizedAccessException accessEx)
                {
                    ShowSecurityError("Access Denied",
                        $"Access was denied:\n\n{accessEx.Message}\n\n" +
                        "This usually means:\n" +
                        "• Insufficient file system permissions\n" +
                        "• Security software is blocking the application\n" +
                        "• User account restrictions\n\n" +
                        "Please contact your system administrator.");
                }
                else if (ex is SecurityException secEx)
                {
                    ShowSecurityError("Security Error",
                        $"A security error occurred:\n\n{secEx.Message}\n\n" +
                        "This usually means:\n" +
                        "• Application lacks required permissions\n" +
                        "• Security policy restrictions\n" +
                        "• Certificate or signature issues\n\n" +
                        "Please contact your system administrator.");
                }
                else if (ex is COMException comEx)
                {
                    ShowSystemError("System Component Error",
                        $"A system component error occurred:\n\n{comEx.Message}\n\n" +
                        $"Error Code: 0x{comEx.HResult:X8}\n\n" +
                        "This usually means:\n" +
                        "• Missing system components\n" +
                        "• Corrupted system files\n" +
                        "• Compatibility issues\n\n" +
                        "Please contact your system administrator.");
                }
                else if (ex is ExternalException extEx)
                {
                    ShowSystemError("External System Error",
                        $"An external system error occurred:\n\n{extEx.Message}\n\n" +
                        $"Error Code: 0x{extEx.ErrorCode:X8}\n\n" +
                        "Please contact your system administrator.");
                }
                else if (ex is TimeoutException)
                {
                    ShowTimeoutError("Operation Timeout",
                        $"An operation timed out:\n\n{ex.Message}\n\n" +
                        "This usually means:\n" +
                        "• Network connectivity issues\n" +
                        "• Server is overloaded or unresponsive\n" +
                        "• System performance issues\n\n" +
                        "Please try again or contact your system administrator.");
                }
                else
                {
                    ShowFatalError("Application Error",
                        $"An unexpected error occurred:\n\n{ex.Message}\n\n" +
                        $"Error Type: {ex.GetType().Name}\n\n" +
                        "Please check the log files and contact your system administrator if needed.\n\n" +
                        "The application will now exit.");
                }
            }
            catch (Exception innerEx)
            {
                Console.WriteLine($"Error in global exception handler: {innerEx.Message}");
                try
                {
                    Service_ErrorHandler.ShowError($"A critical error occurred and could not be handled properly.\n\n" +
                                   $"Original error: {ex.Message}\n" +
                                   $"Handler error: {innerEx.Message}\n\n" +
                                   "The application will now exit.",
                        "Critical Error");
                }
                catch
                {
                    Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// Displays a database-related error message to the user.
        /// </summary>
        /// <param name="title">The title of the error dialog.</param>
        /// <param name="message">The detailed error message.</param>
        /// <remarks>
        /// Uses a fail-safe try-catch block around the message box to ensure error reporting
        /// doesn't cause a secondary crash.
        /// </remarks>
        public static void ShowDatabaseError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowError(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show database error dialog: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a fatal error message to the user.
        /// </summary>
        /// <param name="title">The title of the error dialog.</param>
        /// <param name="message">The detailed error message.</param>
        /// <remarks>
        /// Used for critical errors that typically require application termination.
        /// </remarks>
        public static void ShowFatalError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowError(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show fatal error dialog: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a non-critical warning message to the user.
        /// </summary>
        /// <param name="title">The title of the warning dialog.</param>
        /// <param name="message">The warning message.</param>
        public static void ShowNonCriticalError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowWarning(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show non-critical error dialog: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a file system related error message to the user.
        /// </summary>
        /// <param name="title">The title of the error dialog.</param>
        /// <param name="message">The detailed error message.</param>
        public static void ShowFileSystemError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowError(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show file system error dialog: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a security or permission related error message to the user.
        /// </summary>
        /// <param name="title">The title of the error dialog.</param>
        /// <param name="message">The detailed error message.</param>
        public static void ShowSecurityError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowWarning(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show security error dialog: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a system or component related error message to the user.
        /// </summary>
        /// <param name="title">The title of the error dialog.</param>
        /// <param name="message">The detailed error message.</param>
        public static void ShowSystemError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowError(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show system error dialog: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays a timeout related error message to the user.
        /// </summary>
        /// <param name="title">The title of the error dialog.</param>
        /// <param name="message">The detailed error message.</param>
        public static void ShowTimeoutError(string title, string message)
        {
            try
            {
                Service_ErrorHandler.ShowWarning(message, title);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show timeout error dialog: {ex.Message}");
            }
        }
    }
}
