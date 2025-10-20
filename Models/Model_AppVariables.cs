using System;
using System.Reflection;
using MTM_Inventory_Application.Data;
using MTM_Inventory_Application.Helpers;

namespace MTM_Inventory_Application.Models
{
    #region Model_AppVariables

    internal static class Model_AppVariables
    {
        #region User Info

    public static string EnteredUser { get; set; } = "Default User";
    public static string User { get; set; } = (Environment.UserName ?? "UNKNOWN").ToUpperInvariant();
        public static string? UserPin { get; set; }
        public static string? UserShift { get; set; }
        public static bool UserTypeAdmin { get; set; } = false;
        public static bool UserTypeReadOnly { get; set; } = false;
        public static bool UserTypeNormal { get; set; } = true;

        public static string UserVersion { get; set; } =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";

        public static string? UserFullName { get; set; }
        public static string? VisualUserName { get; set; }
        public static string? VisualPassword { get; set; }
        public static Model_UserUiColors UserUiColors { get; set; } = new();

        #endregion

        #region Inventory State

        public static int InventoryQuantity { get; set; }
        public static string? PartId { get; set; }
        public static string? PartType { get; set; }
        public static string? Location { get; set; }
        public static string? Operation { get; set; }
        public static string? Notes { get; set; }

        #endregion

        #region Theme & Version

        public static string? LastUpdated { get; set; } = "08/05/2025";
        public static string? ThemeName { get; set; } = "Default";
        public static float ThemeFontSize { get; set; } = 9f;
        public static string? WipDataGridTheme { get; set; } = "Default";
        public static string? WipServerAddress 
        { 
            get
            {
                return Model_Users.WipServerAddress;
            }
            set 
            {
                // For compatibility, but the logic is now in Model_Users
            }
        }

        public static string? WipServerPort { get; set; } = "3306";
        public static string? Version { get; set; } = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        public static string ConnectionString =>
            Helper_Database_Variables.GetConnectionString(null, null, null, null);

        #endregion

        #region Database Performance Thresholds

        /// <summary>
        /// Performance threshold for query operations (milliseconds). Default: 500ms
        /// </summary>
        public static long? QueryThresholdMs { get; set; } = 500;

        /// <summary>
        /// Performance threshold for modification operations (INSERT/UPDATE/DELETE) (milliseconds). Default: 1000ms
        /// </summary>
        public static long? ModificationThresholdMs { get; set; } = 1000;

        /// <summary>
        /// Performance threshold for batch operations (milliseconds). Default: 5000ms
        /// </summary>
        public static long? BatchThresholdMs { get; set; } = 5000;

        /// <summary>
        /// Performance threshold for report generation operations (milliseconds). Default: 2000ms
        /// </summary>
        public static long? ReportThresholdMs { get; set; } = 2000;

        /// <summary>
        /// Command timeout for stored procedure execution (seconds). Default: 30 seconds
        /// </summary>
        public static int CommandTimeoutSeconds { get; set; } = 30;

        #endregion

        #region About Variables

        public static string ApplicationName { get; } = "Manitowoc Tool and Manufacturing WIP Application";

        public static string ApplicationAuthor { get; } = @"John Koll";

        public static string ApplicationCopyright { get; } = @"Manitowoc Tool and Manufacturing";

        public static string ApplicationVersion { get; } =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";

        #endregion
    }

    #endregion
}
