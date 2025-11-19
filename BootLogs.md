------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[21:02:39.258] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[21:02:39.281] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[21:02:39.283] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[21:02:39.284] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-18 21:02:39 - [Startup] Application initialization started
2025-11-18 21:02:39 - [Startup] User identified: JOHNK
[21:02:39.326] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerAddressAsync
[21:02:39.327] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:39.329] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:39.385] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:39.386] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:39.567] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (237ms) - Status: 1
[21:02:39.579] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (237ms) - 1 rows
[21:02:39.581] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (196ms)
[21:02:39.582] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (253ms)
[21:02:39.591] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (263ms)
[21:02:39.592] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerAddressAsync (265ms)
[21:02:39.593] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerPortAsync
[21:02:39.594] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:39.595] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:39.596] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:39.597] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:39.601] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (5ms) - Status: 1
[21:02:39.601] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (5ms) - 1 rows
[21:02:39.602] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:39.603] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (8ms)
[21:02:39.604] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (10ms)
[21:02:39.605] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerPortAsync (11ms)
[21:02:39.606] [MEDIUM] ➡️ ENTERING Dao_User.GetDatabaseAsync
[21:02:39.607] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:39.608] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:39.608] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:39.609] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:39.612] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (4ms) - Status: 1
[21:02:39.613] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (4ms) - 1 rows
[21:02:39.613] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (5ms)
[21:02:39.614] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (6ms)
[21:02:39.615] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (8ms)
[21:02:39.616] [MEDIUM] ⬅️ EXITING Dao_User.GetDatabaseAsync (9ms)
[21:02:39.617] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
[21:02:39.618] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:39.619] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[21:02:39.650] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (32ms) - Status: 1
[21:02:39.651] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (32ms) - 9 rows
[21:02:39.652] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (33ms)
[21:02:39.652] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (35ms)
2025-11-18 21:02:39 - [Startup] Database connectivity validated successfully
2025-11-18 21:02:39 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-18 21:02:39 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-18 21:02:39 - [Startup] Parameter cache populated: 120 procedures, 536 total parameters
2025-11-18 21:02:39 - [Startup] Parameter prefix cache initialized successfully in 13ms. Cached 120 stored procedures.
[Startup] Parameter cache: 120 procedures cached in 13ms
[21:02:39.673] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
[21:02:39.674] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:39.675] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-18 21:02:39 - [Startup] Initializing dependency injection container
2025-11-18 21:02:39 - [Startup] Dependency injection container initialized successfully
[21:02:39.715] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (42ms) - Status: 1
[21:02:39.716] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (42ms) - 88 rows
[21:02:39.717] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (43ms)
[21:02:39.718] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (44ms)
[21:02:39.733] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[21:02:39.735] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[21:02:39.784] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[21:02:39.813] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[21:02:39.814] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[21:02:39.815] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[21:02:39.816] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (82ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-18-2025 @ 9-02 PM_normal.csv
2025-11-18 21:02:39 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[21:02:40.242] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
[21:02:40.243] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.244] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[21:02:40.303] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (61ms) - Status: 1
[21:02:40.307] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (61ms) - 3747 rows
[21:02:40.308] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (64ms)
[21:02:40.309] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (66ms)
[21:02:40.317] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
[21:02:40.319] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.320] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[21:02:40.348] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (30ms) - Status: 1
[21:02:40.349] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (30ms) - 72 rows
[21:02:40.350] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (30ms)
[21:02:40.351] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (33ms)
[21:02:40.352] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
[21:02:40.353] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.354] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[21:02:40.433] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (80ms) - Status: 1
[21:02:40.435] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (80ms) - 10371 rows
[21:02:40.436] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (83ms)
[21:02:40.437] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (85ms)
[21:02:40.448] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
[21:02:40.451] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.454] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[21:02:40.485] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (37ms) - Status: 1
[21:02:40.486] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (37ms) - 88 rows
[21:02:40.488] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (36ms)
[21:02:40.489] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (40ms)
[21:02:40.490] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
[21:02:40.491] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.492] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[21:02:40.523] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (32ms) - Status: 1
[21:02:40.524] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (32ms) - 4 rows
[21:02:40.525] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (33ms)
[21:02:40.526] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (35ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 55, Status: Loading color code cache...
[21:02:40.587] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[21:02:40.588] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
[21:02:40.588] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.589] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[21:02:40.618] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (30ms) - Status: 1
[21:02:40.619] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (30ms) - 5 rows
[21:02:40.620] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (31ms)
[21:02:40.621] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (33ms)
[21:02:40.622] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (34ms)
[21:02:40.623] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
[21:02:40.624] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.625] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[21:02:40.649] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (26ms) - Status: 1
[21:02:40.650] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (26ms) - 10 rows
[21:02:40.651] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (27ms)
[21:02:40.652] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (29ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 58, Status: Color code cache loaded.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
Running VersionChecker...
[21:02:40.713] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
[21:02:40.714] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.715] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
[21:02:40.738] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (25ms) - Status: 1
[21:02:40.739] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (25ms) - 1 rows
[21:02:40.740] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
[21:02:40.741] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (27ms)
Database version retrieved: 6.2.3.0
Version labels updated - App: 6.2.1.0, DB: 6.2.3.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
[21:02:40.773] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
[21:02:40.775] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.777] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[21:02:40.783] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (10ms) - Status: 1
[21:02:40.784] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (10ms) - 9 rows
[21:02:40.785] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (9ms)
[21:02:40.786] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (12ms)
[21:02:40.809] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[21:02:40.810] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:40.811] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:40.812] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.813] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:40.816] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (5ms) - Status: 1
[21:02:40.817] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (5ms) - 1 rows
[21:02:40.818] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:40.819] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (8ms)
[21:02:40.821] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (10ms)
[21:02:40.821] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (11ms)
[21:02:40.823] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[21:02:40.824] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:40.824] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:40.825] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.826] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:40.829] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (4ms) - Status: 1
[21:02:40.830] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (4ms) - 1 rows
[21:02:40.830] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (5ms)
[21:02:40.831] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (6ms)
[21:02:40.832] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (8ms)
[21:02:40.833] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (10ms)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
[21:02:40.963] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[21:02:40.964] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:40.965] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:40.966] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.967] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:40.971] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (6ms) - Status: 1
[21:02:40.972] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (6ms) - 1 rows
[21:02:40.974] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (8ms)
[21:02:40.975] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (10ms)
[21:02:40.976] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (12ms)
[21:02:40.977] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (14ms)
[21:02:40.979] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[21:02:40.980] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:40.981] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:40.982] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.983] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:40.986] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (5ms) - Status: 1
[21:02:40.987] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (5ms) - 1 rows
[21:02:40.988] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:40.989] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (8ms)
[21:02:40.991] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (10ms)
[21:02:40.992] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (12ms)
[21:02:40.993] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[21:02:40.994] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:40.995] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:40.996] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:40.997] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:41.000] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (4ms) - Status: 1
[21:02:41.000] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (4ms) - 1 rows
[21:02:41.001] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (5ms)
[21:02:41.002] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (7ms)
[21:02:41.003] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (8ms)
[21:02:41.004] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (10ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeDebouncer[0]
      Applying debounced theme change: Forest (Reason: Login)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Theme changed to 'Forest' (Reason: Login, User: JOHNK)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
[21:02:41.385] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[21:02:41.387] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[21:02:41.409] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[21:02:41.410] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[21:02:41.425] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[21:02:41.427] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[21:02:41.431] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[21:02:41.432] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[21:02:41.434] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[21:02:41.435] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
[21:02:41.436] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.437] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[21:02:41.439] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
[21:02:41.441] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.442] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[21:02:41.444] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
[21:02:41.445] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.446] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[21:02:41.449] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
[21:02:41.450] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[21:02:41.457] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[21:02:41.458] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[21:02:41.460] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[21:02:41.461] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (51ms)
[21:02:41.466] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[21:02:41.467] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
[21:02:41.479] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[21:02:41.480] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
[21:02:41.490] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[21:02:41.491] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[21:02:41.502] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[21:02:41.503] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[21:02:41.504] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[21:02:41.505] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[21:02:41.507] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[21:02:41.507] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[21:02:41.508] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[21:02:41.509] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[21:02:41.510] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (20ms)
[21:02:41.512] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[21:02:41.512] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[21:02:41.591] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[21:02:41.592] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[21:02:41.593] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[21:02:41.609] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[21:02:41.610] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[21:02:41.681] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[21:02:41.682] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[21:02:41.683] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[21:02:41.685] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[21:02:41.686] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[21:02:41.723] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[21:02:41.725] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[21:02:41.726] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[21:02:41.728] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[21:02:41.729] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (3ms)
[21:02:41.731] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[21:02:41.732] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[21:02:41.733] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (8ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[21:02:41.737] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[21:02:41.738] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[21:02:41.741] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[21:02:41.742] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[21:02:41.744] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[21:02:41.746] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] DPI change events wired up.
[21:02:41.748] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[21:02:41.749] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (8ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[21:02:41.751] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[21:02:41.753] [MEDIUM] ⬅️ EXITING MainForm.MainForm (367ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
[21:02:41.756] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[21:02:41.757] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[21:02:41.779] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.780] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[21:02:41.782] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (342ms) - Status: 1
[21:02:41.786] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (342ms) - 72 rows
[21:02:41.787] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (350ms)
[21:02:41.788] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (348ms)
[21:02:41.790] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[21:02:41.791] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[21:02:41.792] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.793] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
[21:02:41.830] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (39ms) - Status: 1
[21:02:41.833] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (76ms) - Status: 1
[21:02:41.834] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (39ms) - 1 rows
[21:02:41.834] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (76ms) - 1 rows
[21:02:41.836] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (394ms)
[21:02:41.837] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (46ms)
[21:02:41.838] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (392ms)
[21:02:41.839] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (82ms)
[21:02:41.839] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (83ms)
[21:02:41.841] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (51ms)
[21:02:41.858] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (422ms) - Status: 1
[21:02:41.859] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (422ms) - 3747 rows
[21:02:41.860] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (81ms)
[21:02:41.861] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (425ms)
[21:02:41.891] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (446ms) - Status: 1
[21:02:41.894] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (446ms) - 10371 rows
[21:02:41.897] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (105ms)
[21:02:41.899] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (455ms)
[21:02:41.907] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[21:02:41.908] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
[21:02:41.909] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.910] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[21:02:41.914] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (5ms) - Status: 1
[21:02:41.915] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (5ms) - 5 rows
[21:02:41.916] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:41.917] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (8ms)
[21:02:41.918] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (10ms)
[21:02:41.920] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
[21:02:41.921] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:41.922] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[21:02:41.925] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (4ms) - Status: 1
[21:02:41.926] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (4ms) - 10 rows
[21:02:41.927] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (5ms)
[21:02:41.927] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (7ms)
[21:02:42.031] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (597ms)
[21:02:42.438] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-18 21:02:42 - [Performance Warning] Theme application to form 'MainForm' took 269ms (>100ms threshold)
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[21:02:42.780] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[21:02:42.781] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[21:02:42.781] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:42.782] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[21:02:42.785] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
[21:02:42.787] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
[21:02:42.789] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
[21:02:42.790] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:42.791] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[21:02:42.795] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (14ms) - Status: 1
[21:02:42.796] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (14ms) - 1 rows
[21:02:42.797] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:42.798] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (17ms)
[21:02:42.799] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (19ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[21:02:42.801] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[21:02:42.804] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[21:02:42.805] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-18 21:02:42 - Application Info - Development Menu configured for user 'JOHNK': Visible
[21:02:42.807] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (5ms)
[21:02:42.827] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (37ms) - Status: 1
[21:02:42.828] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (37ms) - 10 rows
[21:02:42.829] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
[21:02:42.829] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (40ms)
[21:02:42.915] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
[21:02:42.916] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:42.917] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[21:02:42.920] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (5ms) - Status: 1
[21:02:42.921] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (5ms) - 10 rows
[21:02:42.922] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:42.923] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (8ms)
[21:02:42.926] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
[21:02:42.966] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (178ms)
[21:02:42.967] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
[21:02:44.721] [LOW   ] 🖱️ UI ACTION: SETTINGS_MENU_CLICK on MainForm
[21:02:44.722] [MEDIUM] ➡️ ENTERING MainForm.MainForm_MenuStrip_File_Settings_Click
[21:02:44.723] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_OPEN on MainForm
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
[21:02:44.724] [MEDIUM] ➡️ ENTERING SettingsForm.SettingsForm
[21:02:44.725] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
[21:02:44.728] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SettingsForm
[21:02:44.729] [LOW   ] 🖱️ UI ACTION: SETTINGS_PANELS_INITIALIZATION on SettingsForm
[21:02:44.730] [LOW   ] 🖱️ UI ACTION: INITIALIZE_CONTROLS on SettingsForm
[21:02:44.763] [MEDIUM] ➡️ ENTERING Dao_User.GetShortcutsJsonAsync
[21:02:44.764] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_GetShortcutsJson
[21:02:44.765] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:44.766] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_GetShortcutsJson
[21:02:44.770] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[21:02:44.770] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:44.772] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:44.772] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:44.773] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:44.812] [MEDIUM] ➡️ ENTERING Control_Add_User.Control_Add_User
[21:02:44.813] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
[21:02:44.817] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Add_User
[21:02:44.818] [LOW   ] 🖱️ UI ACTION: DEFAULT_USER_TYPE_SET on Control_Add_User
[21:02:44.819] [LOW   ] 🖱️ UI ACTION: DEVELOPER_ROLE_WIRED on Control_Add_User
[21:02:44.820] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Add_User
[21:02:44.821] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Add_User
[21:02:44.822] [LOW   ] 🖱️ UI ACTION: VISUAL_ACCESS_EVENT_SETUP on Control_Add_User
[21:02:44.823] [LOW   ] 🖱️ UI ACTION: VIEW_PASSWORDS_EVENT_SETUP on Control_Add_User
[21:02:44.824] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
[21:02:44.825] [MEDIUM] ⬅️ EXITING Control_Add_User.Control_Add_User (13ms)
[21:02:44.827] [MEDIUM] ➡️ ENTERING Control_Edit_User.Control_Edit_User
[21:02:44.828] [LOW   ] 🖱️ UI ACTION: EDIT_USER_INITIALIZATION on Control_Edit_User
[21:02:44.835] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Edit_User
[21:02:44.836] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Edit_User
[21:02:44.837] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Edit_User
[21:02:44.839] [MEDIUM] ➡️ ENTERING Control_Remove_User.Control_Remove_User
[21:02:44.840] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
[21:02:44.842] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_Remove_User
[21:02:44.843] [LOW   ] 🖱️ UI ACTION: USERS_DATA_LOADING on Control_Remove_User
[21:02:44.844] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
[21:02:44.845] [MEDIUM] ⬅️ EXITING Control_Remove_User.Control_Remove_User (5ms)
[21:02:44.847] [MEDIUM] ➡️ ENTERING Control_Add_PartID.Control_Add_PartID
[21:02:44.848] [LOW   ] 🖱️ UI ACTION: ADD_PARTID_INITIALIZATION on Control_Add_PartID
[21:02:44.850] [LOW   ] 🖱️ UI ACTION: PART_TYPES_LOADING on Control_Add_PartID
[21:02:44.854] [MEDIUM] ⬅️ EXITING Control_Add_PartID.Control_Add_PartID (7ms)
[21:02:44.856] [MEDIUM] ➡️ ENTERING Control_Edit_PartID.Control_Edit_PartID
[21:02:44.860] [MEDIUM] ⬅️ EXITING Control_Edit_PartID.Control_Edit_PartID (2ms)
[21:02:44.862] [MEDIUM] ➡️ ENTERING Control_Add_Operation.Control_Add_Operation
[21:02:44.863] [LOW   ] 🖱️ UI ACTION: ADD_OPERATION_INITIALIZATION on Control_Add_Operation
[21:02:44.865] [MEDIUM] ⬅️ EXITING Control_Add_Operation.Control_Add_Operation (2ms)
[21:02:44.873] [LOW   ] 🖱️ UI ACTION: INITIALIZE_FORM on SettingsForm
[21:02:44.875] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
[21:02:44.877] [MEDIUM] ⬅️ EXITING SettingsForm.SettingsForm (152ms)
[Theme] Applied theme to form 'SettingsForm' in 12ms
[21:02:44.968] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (196ms) - Status: 1
[21:02:44.970] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (196ms) - 1 rows
[21:02:44.971] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (206ms)
[21:02:44.972] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (200ms)
[21:02:44.973] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (202ms)
[21:02:44.974] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (204ms)
[21:02:44.975] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[21:02:44.976] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:44.978] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:44.979] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:44.980] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:44.982] [HIGH  ] ✅ PROCEDURE usr_ui_settings_GetShortcutsJson (218ms) - Status: 1
[21:02:44.983] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_GetShortcutsJson (218ms) - 1 rows
[21:02:44.984] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (5ms)
[21:02:44.985] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_GetShortcutsJson (221ms)
[21:02:44.986] [MEDIUM] ⬅️ EXITING Dao_User.GetShortcutsJsonAsync (223ms)
[21:02:44.991] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (13ms) - Status: 1
[21:02:44.992] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (13ms) - 1 rows
[21:02:44.993] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (220ms)
[21:02:44.994] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
[21:02:44.995] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (18ms)
[21:02:44.996] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (20ms)
[21:02:45.028] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
[21:02:45.029] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:45.030] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[21:02:45.034] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (6ms) - Status: 1
[21:02:45.035] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (6ms) - 88 rows
[21:02:45.037] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (7ms)
[21:02:45.037] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (9ms)
[21:02:45.043] [MEDIUM] ➡️ ENTERING Dao_User.GetUserByUsernameAsync
[21:02:45.044] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[21:02:45.045] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:45.046] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[21:02:45.050] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (5ms) - Status: 0
[21:02:45.051] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (5ms) - 0 rows
[21:02:45.052] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[21:02:45.053] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (8ms)
[21:02:45.054] [MEDIUM] ⬅️ EXITING Dao_User.GetUserByUsernameAsync (10ms)
[21:02:46.444] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerAddressAsync
[21:02:46.445] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:46.446] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:46.447] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:46.448] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:46.450] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerPortAsync
[21:02:46.451] [MEDIUM]   ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:46.452] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:46.453] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:46.454] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:46.456] [MEDIUM] ➡️ ENTERING Dao_User.GetDatabaseAsync
[21:02:46.457] [MEDIUM]     ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:02:46.458] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[21:02:46.459] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:46.460] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:02:46.464] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
[21:02:46.465] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
[21:02:46.466] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (19ms)
[21:02:46.467] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (15ms)
[21:02:46.468] [MEDIUM]     ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
[21:02:46.469] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerPortAsync (18ms)
[21:02:46.470] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (23ms) - Status: 1
[21:02:46.471] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (23ms) - 1 rows
[21:02:46.472] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (18ms)
[21:02:46.473] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (26ms)
[21:02:46.474] [MEDIUM]   ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[21:02:46.474] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerAddressAsync (30ms)
[21:02:46.475] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (17ms) - Status: 1
[21:02:46.476] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (17ms) - 1 rows
[21:02:46.477] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (17ms)
[21:02:46.478] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (19ms)
[21:02:46.479] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
[21:02:46.480] [MEDIUM] ⬅️ EXITING Dao_User.GetDatabaseAsync (23ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[21:02:54.696] [MEDIUM] ➡️ ENTERING Dao_User.UserExistsAsync
[21:02:54.697] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Exists
[21:02:54.698] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:02:54.699] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Exists
[21:02:54.727] [HIGH  ] ✅ PROCEDURE usr_users_Exists (29ms) - Status: 1
[21:02:54.728] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Exists (29ms) - 1 rows
[21:02:54.729] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (30ms)
[21:02:54.729] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Exists (32ms)
[21:02:54.730] [MEDIUM] ⬅️ EXITING Dao_User.UserExistsAsync (34ms)
[21:02:56.582] [MEDIUM] ➡️ ENTERING Dao_User.SetWipServerAddressAsync
[21:02:56.584] [MEDIUM] ➡️ ENTERING Dao_User.SetUserSettingInternalAsync
[21:02:56.587] [MEDIUM] ➡️ ENTERING Dao_User.SetWipServerPortAsync
[21:02:56.587] [MEDIUM]   ➡️ ENTERING Dao_User.SetUserSettingInternalAsync
[21:02:56.590] [MEDIUM] ➡️ ENTERING Dao_User.SetDatabaseAsync
[21:02:56.591] [MEDIUM]     ➡️ ENTERING Dao_User.SetUserSettingInternalAsync
[21:02:56.622] [MEDIUM]     ⬅️ EXITING Dao_User.SetUserSettingInternalAsync (30ms)
[21:02:56.623] [MEDIUM] ⬅️ EXITING Dao_User.SetWipServerAddressAsync (40ms)
[21:02:56.623] [MEDIUM]   ⬅️ EXITING Dao_User.SetUserSettingInternalAsync (39ms)
[21:02:56.624] [MEDIUM] ⬅️ EXITING Dao_User.SetWipServerPortAsync (37ms)
[21:02:56.625] [MEDIUM] ⬅️ EXITING Dao_User.SetUserSettingInternalAsync (37ms)
[21:02:56.626] [MEDIUM] ⬅️ EXITING Dao_User.SetDatabaseAsync (36ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[21:03:01.094] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_CANCELED on MainForm
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SettingsForm' unsubscribed from theme changes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'MainForm' unsubscribed from theme changes
2025-11-18 21:03:01 - [Cleanup] Starting application cleanup
2025-11-18 21:03:01 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-18 21:03:01 - [Cleanup] Memory cleanup completed
2025-11-18 21:03:01 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-18 21:03:01 - [Startup] Application shutdown completed
2025-11-18 21:03:01 - [Cleanup] Starting application cleanup
2025-11-18 21:03:01 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-18 21:03:01 - [Cleanup] Memory cleanup completed
2025-11-18 21:03:01 - [Cleanup] Application cleanup completed successfully
2025-11-18 21:03:01 - [Cleanup] Starting application cleanup
2025-11-18 21:03:01 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-18 21:03:01 - [Cleanup] Memory cleanup completed
2025-11-18 21:03:01 - [Cleanup] Application cleanup completed successfully
The program '[9404] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
