using System.Reflection;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Models
{
    #region Model_Application_Variables

    internal static class Model_Application_Variables
    {
        #region User Info

        /* The line `public static string EnteredUser { get; set; } = "Default User";` is declaring a public
        static property named `EnteredUser` in the `Model_Application_Variables` class. This property is of
        type `string` and has both a getter and a setter. The property is initialized with a default value
        of "Default User". This property can be used to store and retrieve the entered user information
        within the application. */
        public static string EnteredUser { get; set; } = "Default User";
        /* The line `public static string User { get; set; } = (Environment.UserName ??
        "UNKNOWN").ToUpperInvariant();` is declaring a public static property named `User` in the
        `Model_Application_Variables` class. */
        public static string User { get; set; } = (Environment.UserName ?? "UNKNOWN").ToUpperInvariant();

        /* The above C# code snippet defines a public static property called `UserPin` of type `string`. The
        property has both a getter and a setter, allowing other parts of the code to read and modify the
        value of `UserPin`. The `?` after `string` indicates that the property can hold a null value. */
        public static string? UserPin { get; set; }
        /* The above C# code snippet defines a public static property called `UserShift` of type `string`. The
        property has both a getter and a setter, allowing other parts of the code to get and set the value
        of `UserShift`. The `?` after `string` indicates that the property can hold a null value. */
        public static string? UserShift { get; set; }
        /* The above C# code snippet is defining a public static property called UserTypeDeveloper of type
        bool. It is initializing the property with a default value of false. */
        public static bool UserTypeDeveloper { get; set; } = false;
        /* The above C# code snippet is defining a public static property called UserTypeAdmin of type bool. It
        is initializing the property with a default value of false. This property can be accessed and
        modified by other parts of the code. */
        public static bool UserTypeAdmin { get; set; } = false;
        /* The above C# code is defining a public static property called `UserTypeReadOnly` with a getter and
        setter. The property is initialized with a default value of `false`. This property can be used to
        determine whether a user type is read-only or not. */
        public static bool UserTypeReadOnly { get; set; } = false;
        /* The above C# code snippet defines a public static property called UserTypeNormal of type bool. It
        has a getter and setter, and it is initialized with the value true. */
        public static bool UserTypeNormal { get; set; } = true;

        public static string UserVersion { get; set; } =
            Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Error: Version not found";

        /* The above C# code is defining a public static property called `UserFullName` of type `string`. The
        property has both a getter and a setter, allowing other parts of the code to get and set the value
        of `UserFullName`. The `?` after `string` indicates that the property can be nullable, meaning it
        can hold a null value in addition to a string value. */
        public static string? UserFullName { get; set; }
        /* The above C# code snippet defines a public static property called `VisualUserName` of type
        `string?`. The `string?` type indicates that the property can hold a string value or a null value.
        The property has both a getter and a setter, allowing other parts of the code to read and modify the
        value of `VisualUserName`. */
        public static string? VisualUserName { get; set; }
        /* The above C# code snippet defines a public static property called `VisualPassword` of type
        `string?`. The `string?` type indicates that the property can hold a string value or a null value.
        This property can be accessed and modified from other parts of the code. */
        public static string? VisualPassword { get; set; }
        /* The above C# code is defining a public static property `UserUiColors` of type
        `Model_Shared_UserUiColors` with a getter and setter. It is initializing this property with a new
        instance of `Model_Shared_UserUiColors` using the default constructor. */
        public static Model_Shared_UserUiColors UserUiColors { get; set; } = new();

        #endregion

        #region Inventory State

        /* The above C# code is defining a public static property called InventoryQuantity with a getter and
        setter. This property can be accessed and modified from other parts of the code. */
        public static int InventoryQuantity { get; set; }
        /* The above C# code snippet defines a public static property named `PartId` of type `string`. The
        property has both a getter and a setter, allowing other parts of the code to get and set the value
        of `PartId`. The `?` after `string` indicates that the property can be nullable, meaning it can hold
        a null value in addition to string values. */
        public static string? PartId { get; set; }
        /* The above C# code snippet defines a public static property `PartType` of type `string`. The property
        has both a getter and a setter, allowing other parts of the code to get and set the value of
        `PartType`. The `?` after `string` indicates that the property can be nullable, meaning it can hold
        a null value in addition to string values. */
        public static string? PartType { get; set; }
        /* The above C# code defines a public static property called Location with a nullable string type. The
        property has both a getter and a setter, allowing you to get and set the value of the Location
        property. */
        public static string? Location { get; set; }
        /* The above C# code defines a public static property called Operation with a nullable string type. The
        property has both a getter and a setter, allowing you to get and set the value of the Operation
        property. */
        public static string? Operation { get; set; }
        /* The above C# code defines a public static property called Notes with a nullable string type. The
        property has both a getter and a setter, allowing you to get and set the value of the Notes
        property. */
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

        /* The above C# code snippet defines a public static property `LastUpdated` of type `string` with a
        nullable reference type (`string?`). The property has both a getter and a setter, and it is
        initialized with the value "08/05/2025". This property can be accessed and modified from other parts of the code. */
        public static string? LastUpdated { get; set; } = "08/05/2025";
        /* The above C# code snippet is defining a public static property called `ThemeName` with a nullable
        string type (`string?`). The property has both a getter and a setter, and it is initialized with a
        default value of "Default". This means that if no other value is explicitly set for `ThemeName`, it
        will default to "Default". */
        public static string? ThemeName { get; set; } = "Default";
        /* The above C# code is defining a public static property called `ThemeEnabled` with a getter and
        setter. The property is of type `bool` and is initialized with a value of `true`. This property can
        be used to determine whether a theme is enabled in the application. */
        public static bool ThemeEnabled { get; set; } = true;
        /* The above C# code snippet is defining a public static property called `AnimationsEnabled` with a
        getter and setter. The property is initialized with a default value of `false`. This property can be
        used to control whether animations are enabled in the application. */
        public static bool AnimationsEnabled { get; set; } = false;/* The above code is defining a
        public static property named
        AutoExpandPanels of type bool in
        C#. The property has a getter and
        setter and is initialized with a
        default value of true. */

        public static bool AutoExpandPanels { get; set; } = true;
        /* The above C# code snippet is defining a public static property called `ThemeFontSize` of type
        `float`. The property has both a getter and a setter, allowing other parts of the code to read and
        modify its value. The property is initialized with a default value of 9.0f. */
        public static float ThemeFontSize { get; set; } = 9f;
        /* The above C# code snippet defines a public static property `WipDataGridTheme` of type `string` with
        a nullable reference type (`string?`). The property has both a getter and a setter, and it is
        initialized with the value "Default". This property can be used to store and retrieve the theme
        information for a data grid in a C# application. */
        public static string? WipDataGridTheme { get; set; } = "Default";
        /* The above code is written in C# and declares a public static nullable string variable named
        WipServerAddress. The question mark (?) after the string type indicates that the variable can hold a
        null value. The code seems to be a placeholder for storing the address of a work-in-progress (WIP)
        server. */
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

        /* The above C# code snippet is defining a public static property `WipServerPort` of type `string` with
        a nullable reference type (`string?`). The property has both a getter and a setter and is
        initialized with the value of `Model_Shared_Users.WipServerPort`. */
        public static string? WipServerPort { get; set; } = Model_Shared_Users.WipServerPort;

        /* The above C# code snippet defines a static property `Version` that can be accessed and set. The
        property is initialized with the version number of the entry assembly, if available. The
        `Assembly.GetEntryAssembly()` method returns the entry assembly of the application domain, and the
        `?.` operator is used for null-conditional access to avoid null reference exceptions. If the entry
        assembly or its version is null, the property will be set to null. */
        public static string? Version { get; set; } = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
        /* The above C# code snippet is defining a public static property called `DatabaseName` with a nullable
        string type. The property has both a getter and a setter, and it is initialized with the value of
        `Model_Shared_Users.Database`. */
        public static string? DatabaseName { get; set; } = Model_Shared_Users.Database;

        /// <summary>
        /// Database username used in connection strings. Default: root (MAMP default)
        /// </summary>
        public static string DatabaseUser { get; set; } = "root";

        /// <summary>
        /// Database password used in connection strings. Default: root (MAMP default)
        /// </summary>
        public static string DatabasePassword { get; set; } = "root";

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
        /// Maximum idle time before connection is closed (milliseconds).
        /// Default: 1800000 ms (30 minutes)
        /// See: Documentation/Connection_Timeout_30Min.md
        /// </summary>
        public static int ConnectionIdleTimeoutMs { get; set; } = 1800000; // 30 minutes

        /// <summary>
        /// Page size for transaction viewer pagination. Default: 50 records per page
        /// </summary>
        public static int TransactionPageSize { get; set; } = 50;

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
                Services.Logging.LoggingUtility.LogDatabaseError(ex);
            }
        }

        #endregion

        /* The above code is defining a public static property named `ShowTotalSummaryPanel` of type `bool` in
        C#. The property has a getter and setter and is initialized with a default value of `false`. */
        public static bool ShowTotalSummaryPanel { get; set; } = false;
    }

    #endregion
}
