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
        public static string Database
        {
            get => _database ?? (
#if DEBUG
                "MTM_WIP_Application_Winforms"  // Changed to use main database in debug mode
#else
        "MTM_WIP_Application_Winforms"
#endif
            );
            set => _database = value;
        }

        private static string? _database;

        public static string WipServerAddress = "localhost";
      

        public static string WipServerPort { get; set; } = "3306";

        public static bool EnableAnimations { get; set; } = false;

        #endregion
    }
}
