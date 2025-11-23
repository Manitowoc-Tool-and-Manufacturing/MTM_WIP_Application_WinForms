using MTM_WIP_Application_Winforms.Controls.SettingsForm;
using MTM_WIP_Application_Winforms.Logging;
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
        /// Starts the main application message loop using the startup context.
        /// </summary>
        /// <remarks>
        /// Initializes the <see cref="Service_Onstartup_StartupSplashApplicationContext"/> which manages
        /// the splash screen and transition to the main form. Catches startup-specific exceptions.
        /// </remarks>
        public static void RunApplication()
        {
            try
            {
                Application.Run(new Service_Onstartup_StartupSplashApplicationContext());
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Application.Run"))
            {
                LoggingUtility.LogApplicationError(ex);
                ShowFatalError("Application Startup Error",
                    "Failed to start the main application interface.\n\n" +
                    "This usually means:\n" +
                    "• Another instance may already be running\n" +
                    "• System resources are insufficient\n" +
                    "• Display configuration issues\n\n" +
                    "Please restart your computer and try again.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                HandleGlobalException(ex);
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
                    _ = MessageBox.Show($"A critical error occurred and could not be handled properly.\n\n" +
                                   $"Original error: {ex.Message}\n" +
                                   $"Handler error: {innerEx.Message}\n\n" +
                                   "The application will now exit.",
                        "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Critical] Failed to show timeout error dialog: {ex.Message}");
            }
        }
    }
}
