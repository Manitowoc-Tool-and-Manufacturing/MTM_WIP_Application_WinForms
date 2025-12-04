using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    /// <summary>
    /// Handles startup reporting and logging initialization tasks.
    /// </summary>
    public static class Service_OnStartup_Reporting
    {
        #region Methods

        /// <summary>
        /// Initializes the application logging system and records the startup event.
        /// </summary>
        /// <remarks>
        /// If logging fails, a console warning is written and a non-critical error dialog is shown to the user.
        /// This ensures the user is aware of potential logging issues without crashing the application.
        /// </remarks>
        public static void InitializeLogging()
        {
            try
            {
                LoggingUtility.Log("[Startup] Application initialization started");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Warning] Initial logging failed: {ex.Message}");
                Service_OnStartup_AppLifecycle.ShowNonCriticalError("Logging System Warning",
                    $"The logging system encountered an issue during startup:\n\n{ex.Message}\n\n" +
                    "The application will continue, but some log entries may be missing.\n" +
                    "Please check file system permissions and disk space.");
            }
        }

        /// <summary>
        /// Initiates the background synchronization of error reports to the database.
        /// </summary>
        /// <remarks>
        /// This method triggers an asynchronous operation in a fire-and-forget manner.
        /// Any exceptions during the initialization of the sync process are caught and logged locally.
        /// </remarks>
        public static void SyncErrorReports()
        {
            try
            {
                _ = Service_ErrorReportSync.SyncOnStartupAsync();
            }
            catch (Exception ex)
            {
                LoggingUtility.Log($"[Startup] Error report sync initialization failed: {ex.Message}");
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion
    }
}
