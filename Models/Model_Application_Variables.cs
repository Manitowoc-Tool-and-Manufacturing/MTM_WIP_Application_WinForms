using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Models
{
    #region Model_Application_Variables

    internal static class Model_Application_Variables
    {
        #region User Info

    public static string EnteredUser { get; set; } = "Default User";
    public static string User { get; set; } = (Environment.UserName ?? "UNKNOWN").ToUpperInvariant();
        public static string? UserPin { get; set; }
        public static string? UserShift { get; set; }
        public static bool UserTypeDeveloper { get; set; } = false;
        public static bool UserTypeAdmin { get; set; } = false;
        public static bool UserTypeReadOnly { get; set; } = false;
        public static bool UserTypeNormal { get; set; } = true;

        public static string UserVersion { get; set; } =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Error: Version not found";

        public static string? UserFullName { get; set; }
        public static string? VisualUserName { get; set; }
        public static string? VisualPassword { get; set; }
        public static Model_Shared_UserUiColors UserUiColors { get; set; } = new();

        #endregion

        #region Inventory State

        public static int InventoryQuantity { get; set; }
        public static string? PartId { get; set; }
        public static string? PartType { get; set; }
        public static string? Location { get; set; }
        public static string? Operation { get; set; }
        public static string? Notes { get; set; }

        #endregion

        #region Color Code Tracking

        /// <summary>
        /// Cache of PartIDs that require color code tracking during inventory entry.
        /// Populated at application startup from md_part_ids.RequiresColorCode flag.
        /// Used for fast lookup during inventory save to determine if color/work order fields should be shown.
        /// </summary>
        public static HashSet<string> ColorCodeParts { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Cache of valid color codes from md_color_codes table.
        /// Populated at application startup and used for fast validation during inventory entry.
        /// Case-sensitive HashSet for exact matching after title-case formatting.
        /// </summary>
        public static HashSet<string> ValidColorCodes { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Theme & Version

        public static string? LastUpdated { get; set; } = "08/05/2025";
        public static string? ThemeName { get; set; } = "Default";
        public static bool ThemeEnabled { get; set; } = true;
        public static float ThemeFontSize { get; set; } = 9f;
        public static string? WipDataGridTheme { get; set; } = "Default";
        public static string? WipServerAddress
        {
            get
            {
                return Model_Shared_Users.WipServerAddress;
            }
            set
            {
                // For compatibility, but the logic is now in Model_Shared_Users
            }
        }

        public static string? WipServerPort { get; set; } = "3306";
        public static string? Version { get; set; } = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        /// <summary>
        /// Optional override for database name. When set, all connection strings will use this value.
        /// Defaults to Model_Shared_Users.Database when not explicitly overridden.
        /// </summary>
        private static string? _databaseNameOverride;

        /// <summary>
        /// Gets or sets the database name used for connections.
        /// When not set, falls back to Model_Shared_Users.Database (legacy location).
        /// </summary>
        public static string DatabaseName
        {
            get => _databaseNameOverride ?? (Model_Shared_Users.Database ?? "MTM_WIP_Application_Winforms");
            set => _databaseNameOverride = value;
        }

        /// <summary>
        /// Database username used in connection strings. Default: root (MAMP default)
        /// </summary>
        public static string DatabaseUser { get; set; } = "root";

        /// <summary>
        /// Database password used in connection strings. Default: root (MAMP default)
        /// </summary>
        public static string DatabasePassword { get; set; } = "root";

        /// <summary>
        /// Bootstrap connection string cached at application startup.
        /// Used for fetching user settings to avoid circular dependency.
        /// This uses the initial default values and never changes.
        /// </summary>
        private static string? _bootstrapConnectionString;

        /// <summary>
        /// Gets the bootstrap connection string, initializing it on first access.
        /// This connection string uses default values and is immune to changes in Model_Shared_Users properties.
        /// </summary>
        public static string BootstrapConnectionString
        {
            get
            {
                if (_bootstrapConnectionString == null)
                {
                    // Capture initial values - these should never change during settings fetch
                    string server = Model_Shared_Users.WipServerAddress ?? "localhost";
                    string database = Model_Shared_Users.Database ?? "MTM_WIP_Application_Winforms";
                    // Bootstrap uses initial defaults intentionally
                    _bootstrapConnectionString = Helper_Database_Variables.GetConnectionString(server, database, DatabaseUser, DatabasePassword);
                }
                return _bootstrapConnectionString;
            }
        }

        /// <summary>
        /// Dynamic connection string that uses current Model_Shared_Users values.
        /// This will reflect any changes made to server/database/port settings.
        /// </summary>
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

        /// <summary>
        /// Page size for transaction viewer pagination. Default: 50 records per page
        /// </summary>
        public static int TransactionPageSize { get; set; } = 50;

        #endregion

        #region About Variables

        public static string ApplicationName { get; } = "Manitowoc Tool and Manufacturing WIP Application";

        public static string ApplicationAuthor { get; } = @"John Koll";

        public static string ApplicationCopyright { get; } = @"Manitowoc Tool and Manufacturing";

        public static string ApplicationVersion { get; } =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";

        #endregion

        #region Error Reporting Configuration

        /// <summary>
        /// Configuration for error reporting and offline queue functionality.
        /// </summary>
        public static ErrorReportingConfig ErrorReporting { get; } = new();

        /// <summary>
        /// Configuration class for error reporting system.
        /// </summary>
        public class ErrorReportingConfig
        {
            /// <summary>
            /// Directory path for pending (not yet submitted) error reports.
            /// Default: %APPDATA%\MTM_Application\ErrorReports\Pending
            /// </summary>
            public string QueueDirectory { get; set; } =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MTM_Application", "ErrorReports", "Pending");

            /// <summary>
            /// Directory path for successfully submitted error reports archive.
            /// Default: %APPDATA%\MTM_Application\ErrorReports\Sent
            /// </summary>
            public string ArchiveDirectory { get; set; } =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "MTM_Application", "ErrorReports", "Sent");

            /// <summary>
            /// Maximum age in days for pending reports before flagged as stale.
            /// Default: 30 days
            /// </summary>
            public int MaxPendingAgeDays { get; set; } = 30;

            /// <summary>
            /// Maximum age in days for archived sent reports before deletion during cleanup.
            /// Default: 30 days
            /// </summary>
            public int MaxSentArchiveAgeDays { get; set; } = 30;

            /// <summary>
            /// Whether to automatically sync pending reports on application startup.
            /// Default: true
            /// </summary>
            public bool EnableAutoSyncOnStartup { get; set; } = true;

            /// <summary>
            /// Minimum number of pending reports to trigger progress indicator during sync.
            /// If pending count exceeds this threshold, show progress UI.
            /// Default: 5
            /// </summary>
            public int SyncProgressThreshold { get; set; } = 5;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reloads the ColorCodeParts and ValidColorCodes caches from the database.
        /// </summary>
        /// <returns>Task representing the async operation.</returns>
        /// <remarks>
        /// - Queries md_part_ids for PartIDs where RequiresColorCode = TRUE (ColorCodeParts)
        /// - Queries md_color_codes for all valid ColorCode values (ValidColorCodes)
        /// Call this at startup and after any change to part flags or color codes.
        /// </remarks>
        public static async Task ReloadColorCodePartsAsync()
        {
            try
            {
                // Load parts that require color code tracking
                var result = await Dao_Part.GetColorCodeFlaggedPartsAsync();

                if (result.IsSuccess && result.Data != null)
                {
                    ColorCodeParts.Clear();
                    foreach (System.Data.DataRow row in result.Data.Rows)
                    {
                        var partId = row["PartID"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(partId))
                        {
                            ColorCodeParts.Add(partId);
                        }
                    }
                }

                // Load valid color codes for fast validation (case-insensitive set)
                var colorDao = new Data.Dao_ColorCode();
                var colorResult = await colorDao.GetAllAsync();
                if (colorResult.IsSuccess && colorResult.Data != null)
                {
                    ValidColorCodes.Clear();
                    foreach (System.Data.DataRow row in colorResult.Data.Rows)
                    {
                        var code = row["ColorCode"]?.ToString();
                        if (!string.IsNullOrWhiteSpace(code))
                        {
                            ValidColorCodes.Add(code);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw - cache remains in previous state
                Logging.LoggingUtility.LogDatabaseError(ex);
            }
        }

        #endregion
    }

    #endregion
}
