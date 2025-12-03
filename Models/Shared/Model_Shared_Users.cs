namespace MTM_WIP_Application_Winforms.Models
{
    internal class Model_Shared_Users
    {
        #region Properties

        public static string FullName { get; set; } = string.Empty;
        public string HideChangeLog { get; set; } = "false";
        public int Id { get; set; } = 0;
        public string LastShownVersion { get; set; } = "0.0.0.0";
        public string Pin { get; set; } = "0000";
        public string Shift { get; set; } = string.Empty;
        public int ThemeFontSize { get; set; } = 9;
        public string ThemeName { get; set; } = "Default";
        public string User { get; set; } = string.Empty;
        public static string VisualPassword { get; set; } = "Password";
        public static string VisualUserName { get; set; } = "User Name";
        public bool VitsUser { get; set; } = false;
        public static string? Database { get; set ; } = "mtm_wip_application_winforms";

        public static string WipServerAddress { get; set; } = "localhost";


        public static string WipServerPort { get; set; } = "3306";

        /// <summary>
        /// Centralized check for whether 32x32 icon animations should run.
        /// Provides a single place to expand validation logic (e.g., future performance guards).
        /// </summary>
        public static bool AreAnimationsEnabled()
        {
            return Model_Application_Variables.AnimationsEnabled;
        }

        #endregion
    }
}
