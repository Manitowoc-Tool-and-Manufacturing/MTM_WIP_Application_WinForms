using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    /// <summary>
    /// Service responsible for cleaning up application data folders during startup or on demand.
    /// </summary>
    internal static class Service_OnStartup_AppDataCleaner
    {
        #region Public Methods

        /// <summary>
        /// Deletes all files and subdirectories within the specified directory.
        /// </summary>
        /// <param name="directoryPath">The full path of the directory to clean.</param>
        public static void DeleteDirectoryContents(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    foreach (string file in Directory.GetFiles(directoryPath))
                    {
                        File.Delete(file);
                    }

                    foreach (string subDirectory in Directory.GetDirectories(directoryPath))
                    {
                        Directory.Delete(subDirectory, true);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        /// <summary>
        /// Wipes the application specific data folders in both Roaming and Local AppData.
        /// </summary>
        public static void WipeAppDataFolders()
        {
            try
            {
                string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MTM_WIP_Application_Winforms");
                string localAppDataPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "MTM_WIP_Application_Winforms");
                DeleteDirectoryIfExists(appDataPath);
                DeleteDirectoryIfExists(localAppDataPath);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Deletes a directory and its contents if it exists.
        /// </summary>
        /// <param name="path">The full path of the directory to delete.</param>
        private static void DeleteDirectoryIfExists(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion
    }
}
