using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services.Startup;

namespace MTM_WIP_Application_Winforms
{
    internal static class Program
    {
        #region Dependency Injection

        /// <summary>
        /// Global service provider for dependency injection.
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; private set; }

        #endregion

        #region Entry Point

        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                // Initialize debugging system first
                Service_OnStartup_System.InitializeDebugging();

                // Global exception handling setup
                Service_OnStartup_AppLifecycle.SetupGlobalExceptionHandling();

                // Register cleanup handler
                Service_OnStartup_AppLifecycle.SetupCleanupHandlers();

                // Windows Forms initialization
                if (!Service_OnStartup_System.InitializeWinForms())
                {
                    return;
                }

                // Logging initialization
                Service_OnStartup_Reporting.InitializeLogging();

                // User identification
                Service_OnStartup_User.IdentifyUser();

                // Initialize parameter cache (Moved to Splash Context)
                // Service_OnStartup_Database.InitializeParameterCache();

                // Synchronize error reports
                Service_OnStartup_Reporting.SyncErrorReports();

                // Trace setup
                Service_OnStartup_System.SetupTrace();

                // Configure dependency injection
                ServiceProvider = Service_OnStartup_DependencyInjection.ConfigureServices();

                // Start application
                try
                {
                    Application.Run(new Service_Onstartup_StartupSplashApplicationContext());
                }
                catch (InvalidOperationException ex) when (ex.Message.Contains("Application.Run"))
                {
                    LoggingUtility.LogApplicationError(ex);
                    Service_OnStartup_AppLifecycle.ShowFatalError("Application Startup Error",
                        "Failed to start the main application interface.\n\n" +
                        "This usually means:\n" +
                        "• Another instance may already be running\n" +
                        "• System resources are insufficient\n" +
                        "• Display configuration issues\n\n" +
                        "Please restart your computer and try again.");
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine($"[Critical] Out of memory: {ex.Message}");
                // Try to show error if possible, otherwise exit
                try { Service_OnStartup_AppLifecycle.ShowFatalError("Out of Memory", "The application has run out of memory and must close."); } catch { }
                Environment.Exit(1);
            }
            catch (StackOverflowException)
            {
                Console.WriteLine("[Critical] Stack overflow occurred");
                Environment.Exit(1);
            }
            catch (AccessViolationException ex)
            {
                Console.WriteLine($"[Critical] Access violation: {ex.Message}");
                try { Service_OnStartup_AppLifecycle.ShowFatalError("Access Violation", "A critical system error occurred."); } catch { }
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Global Exception] Main catch: {ex}");
                try
                {
                    LoggingUtility.LogApplicationError(ex);
                }
                catch
                {
                    Console.WriteLine("[Critical] Failed to log main exception");
                }
                Service_OnStartup_AppLifecycle.HandleGlobalException(ex);
            }
            finally
            {
                // Ensure cleanup runs even if exceptions occur
                Service_OnStartup_AppLifecycle.PerformAppCleanup();
            }
        }

        #endregion
    }
}
