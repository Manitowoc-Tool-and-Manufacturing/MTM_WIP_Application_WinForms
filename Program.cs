using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services.Startup;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MTM_WIP_Application_Winforms
{
    internal static class Program
    {
        #region Win32 API Imports

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        private const int SW_RESTORE = 9;

        #endregion

        #region Dependency Injection

        /// <summary>
        /// Global service provider for dependency injection.
        /// </summary>
        public static IServiceProvider? ServiceProvider { get; private set; }

        /// <summary>
        /// Application mutex for single instance check.
        /// </summary>
        public static Mutex? AppMutex;

        #endregion

        #region Entry Point

        [STAThread]
        private static void Main(string[] args)
        {
            // Single instance check - prevents multiple app instances
            // See: Documentation/Single_Instance_Check.md
            AppMutex = new Mutex(true, "Global\\MTM_WIP_Application_Winforms_SingleInstance", out bool isNewInstance);
            
            if (!isNewInstance)
            {
                // Another instance is already running
                MessageBox.Show(
                    "MTM WIP Application is already running.\n\nThe existing window will be brought to the front.",
                    "Application Already Running",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                BringExistingInstanceToFront();
                return; // Exit this instance
            }
            
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
                // Release mutex if it exists
                if (AppMutex != null)
                {
                    try
                    {
                        AppMutex.ReleaseMutex();
                    }
                    catch (Exception)
                    {
                        // Ignore errors (e.g. not owned, already released, abandoned)
                    }

                    AppMutex.Dispose();
                    AppMutex = null;
                }

                // Ensure cleanup runs even if exceptions occur
                Service_OnStartup_AppLifecycle.PerformAppCleanup();
            }

            // Force process termination to prevent "zombie" processes keeping the Mutex alive
            // This addresses the concern of the app appearing closed but still holding the Mutex
            Environment.Exit(0);
        }

        private static void BringExistingInstanceToFront()
        {
            Process current = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    IntPtr hWnd = process.MainWindowHandle;
                    if (hWnd != IntPtr.Zero)
                    {
                        if (IsIconic(hWnd))
                        {
                            ShowWindow(hWnd, SW_RESTORE);
                        }
                        SetForegroundWindow(hWnd);
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
