using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Services
{
    internal static class Service_OnStartup_AppDataCleaner
    {
        #region Public Methods

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

            }
        }

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

            }
        }

        #endregion

        #region Private Methods

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

            }
        }

        #endregion
    }
}
