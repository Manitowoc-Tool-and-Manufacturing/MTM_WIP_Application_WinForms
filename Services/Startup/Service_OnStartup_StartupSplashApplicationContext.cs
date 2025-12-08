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

                var progressReporter = new Progress<(int percent, string message)>(p => 
                {
                    _splashScreen?.UpdateProgress(p.percent, p.message);
                });

                var result = await Service_OnStartup_AppLifecycle.ExecuteStartupSequenceAsync(progressReporter, async () => 
                {
                    using var loginForm = new Form_SharedLogin();
                    // Show dialog on top of splash screen
                    var dialogResult = loginForm.ShowDialog(_splashScreen);
                    if (dialogResult == DialogResult.OK)
                    {
                        Model_Application_Variables.User = loginForm.ValidatedUsername;
                        LoggingUtility.Log($"[Startup] Shared workstation login successful. User: {Model_Application_Variables.User}");
                        return true;
                    }
                    else
                    {
                        LoggingUtility.Log("[Startup] Shared workstation login cancelled/failed. Exiting.");
                        return false;
                    }
                });

                if (!result.IsSuccess)
                {
                    if (result.Exception != null)
                    {
                        Service_ErrorHandler.HandleException(result.Exception, Enum_ErrorSeverity.Fatal,
                            contextData: result.Context,
                            callerName: "RunStartupAsync",
                            controlName: "StartupSplash");
                    }
                    else
                    {
                        Service_ErrorHandler.ShowError(result.Message, "Startup Failed");
                    }
                    _splashScreen?.Close();
                    return;
                }


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
