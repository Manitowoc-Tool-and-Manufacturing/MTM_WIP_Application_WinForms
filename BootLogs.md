------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[01:22:26.692] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[01:22:26.725] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[01:22:26.727] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[01:22:26.728] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
[01:22:26.758] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
[01:22:26.820] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:26.821] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[01:22:27.012] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (253ms) - Status: 1
[01:22:27.025] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (253ms) - 9 rows
[01:22:27.027] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (207ms)
[01:22:27.028] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (269ms)
[Startup] Parameter cache: 114 procedures cached in 11ms
[01:22:27.048] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
[01:22:27.048] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.049] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
[01:22:27.071] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (23ms) - Status: 1
[01:22:27.074] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (23ms) - 88 rows
[01:22:27.075] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
[01:22:27.076] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[01:22:27.077] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (28ms)
[01:22:27.077] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[01:22:27.102] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[01:22:27.132] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[01:22:27.132] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[DEBUG] [SplashScreenForm.ApplyTheme] Applying theme...
[THEME]  (Label) - BackColor: FormBackColor = Color [A=255, R=30, G=30, B=30] | Theme: Default
[THEME]  (Label) - ForeColor: FormForeColor = Color [A=255, R=255, G=255, B=255] | Theme: Default
[THEME]  (Label) - BackColor: FormBackColor = Color [A=255, R=30, G=30, B=30] | Theme: Default
[THEME]  (Label) - ForeColor: FormForeColor = Color [A=255, R=255, G=255, B=255] | Theme: Default
[THEME] Control_ProgressBarUserControl (Control_ProgressBarUserControl) - BackColor: FormBackColor = Color [A=255, R=30, G=30, B=30] | Theme: Default
[THEME] Control_ProgressBarUserControl (Control_ProgressBarUserControl) - ForeColor: FormForeColor = Color [A=255, R=255, G=255, B=255] | Theme: Default
[THEME]  (PictureBox) - BackColor: FormBackColor = Color [A=255, R=30, G=30, B=30] | Theme: Default
[THEME]  (PictureBox) - ForeColor: FormForeColor = Color [A=255, R=255, G=255, B=255] | Theme: Default
[THEME]  (ProgressBar) - BackColor: FormBackColor = Color [A=255, R=30, G=30, B=30] | Theme: Default
[THEME]  (ProgressBar) - ForeColor: FormForeColor = Color [A=255, R=255, G=255, B=255] | Theme: Default
[THEME]  (Label) - BackColor: FormBackColor = Color [A=255, R=30, G=30, B=30] | Theme: Default
[THEME]  (Label) - ForeColor: FormForeColor = Color [A=255, R=255, G=255, B=255] | Theme: Default
[DEBUG] [SplashScreenForm.ApplyTheme] Theme applied.
[01:22:27.152] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[01:22:27.153] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (77ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-08-2025 @ 1-22 AM_normal.log
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[01:22:27.571] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
[01:22:27.572] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.573] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[01:22:27.619] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (48ms) - Status: 1
[01:22:27.620] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (48ms) - 3745 rows
[01:22:27.621] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (49ms)
[01:22:27.623] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (52ms)
[01:22:27.642] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
[01:22:27.644] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.645] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[01:22:27.662] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (19ms) - Status: 1
[01:22:27.665] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (19ms) - 72 rows
[01:22:27.666] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (21ms)
[01:22:27.667] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (24ms)
[01:22:27.675] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
[01:22:27.675] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.676] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[01:22:27.738] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (63ms) - Status: 1
[01:22:27.741] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (63ms) - 10371 rows
[01:22:27.742] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (66ms)
[01:22:27.743] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (68ms)
[01:22:27.758] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
[01:22:27.760] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.760] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[01:22:27.779] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (20ms) - Status: 1
[01:22:27.780] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (20ms) - 88 rows
[01:22:27.782] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (21ms)
[01:22:27.783] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (24ms)
[01:22:27.787] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
[01:22:27.788] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.789] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[01:22:27.806] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (19ms) - Status: 1
[01:22:27.807] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (19ms) - 4 rows
[01:22:27.808] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (19ms)
[01:22:27.810] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (22ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
Running VersionChecker...
[01:22:27.873] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
[01:22:27.874] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.875] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
[01:22:27.895] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (22ms) - Status: 1
[01:22:27.896] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (22ms) - 1 rows
[01:22:27.897] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (23ms)
[01:22:27.899] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (26ms)
Database version retrieved: 5.2.0.0
Version labels updated - App: 5.2.0.0, DB: 5.2.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
[01:22:27.948] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
[01:22:27.949] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.950] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[01:22:27.956] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (8ms) - Status: 1
[01:22:27.957] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (8ms) - 9 rows
[01:22:27.958] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (9ms)
[01:22:27.959] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (11ms)
[01:22:27.991] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[01:22:27.993] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[01:22:27.994] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[01:22:27.995] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:27.996] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[01:22:28.015] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (21ms) - Status: 1
[01:22:28.017] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (21ms) - 1 rows
[01:22:28.018] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (23ms)
[01:22:28.019] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (25ms)
[01:22:28.027] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (33ms)
[01:22:28.028] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (36ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
[01:22:28.152] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[01:22:28.153] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[01:22:28.154] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[01:22:28.155] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:28.157] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[01:22:28.160] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (6ms) - Status: 1
[01:22:28.161] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (6ms) - 1 rows
[01:22:28.162] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[01:22:28.164] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (10ms)
[01:22:28.166] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (13ms)
[01:22:28.167] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (15ms)
[01:22:28.169] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[01:22:28.170] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[01:22:28.171] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[01:22:28.172] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:28.174] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[01:22:28.177] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (5ms) - Status: 1
[01:22:28.178] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (5ms) - 1 rows
[01:22:28.179] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[01:22:28.181] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (9ms)
[01:22:28.182] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (11ms)
[01:22:28.183] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (13ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
[01:22:28.556] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[01:22:28.558] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[01:22:28.583] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[01:22:28.584] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[01:22:28.594] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[01:22:28.601] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[01:22:28.603] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[01:22:28.604] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[01:22:28.606] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[01:22:28.616] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
[01:22:28.618] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[01:22:28.620] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[01:22:28.622] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[01:22:28.624] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[01:22:28.625] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (41ms)
[01:22:28.627] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[01:22:28.629] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
[01:22:28.643] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[01:22:28.673] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
[01:22:28.676] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[01:22:28.677] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[01:22:28.685] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[01:22:28.699] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[01:22:28.700] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[01:22:28.701] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[01:22:28.710] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[01:22:28.711] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[01:22:28.712] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[01:22:28.713] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[01:22:28.714] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (38ms)
[01:22:28.716] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[01:22:28.717] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[01:22:28.726] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[01:22:28.744] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[01:22:28.746] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[01:22:28.750] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[01:22:28.751] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[01:22:28.767] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[01:22:28.768] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[01:22:28.769] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[01:22:28.773] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[01:22:28.783] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[01:22:28.804] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[01:22:28.837] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[01:22:28.839] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[01:22:28.842] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[01:22:28.843] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (4ms)
[01:22:28.846] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[01:22:28.847] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[01:22:28.848] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (10ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[01:22:28.853] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[01:22:28.855] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[01:22:28.858] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[01:22:28.859] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[01:22:28.862] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[01:22:28.865] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] DPI change events wired up.
[01:22:28.867] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[01:22:28.868] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (10ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[01:22:28.871] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[01:22:28.873] [MEDIUM] ⬅️ EXITING MainForm.MainForm (316ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
[01:22:28.879] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[01:22:28.880] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[01:22:28.882] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:28.883] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[01:22:28.889] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
[01:22:28.891] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (1ms)
[01:22:28.906] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[01:22:28.907] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[01:22:28.909] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:28.910] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[01:22:28.936] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (28ms) - Status: 1
[01:22:28.940] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (28ms) - 1 rows
[01:22:28.941] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (59ms)
[01:22:28.942] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (35ms)
[01:22:28.942] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
[01:22:28.944] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (65ms)
[01:22:28.947] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[01:22:28.951] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (70ms) - Status: 1
[01:22:28.952] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (70ms) - 1 rows
[01:22:28.954] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (44ms)
[01:22:28.955] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (74ms)
[01:22:28.956] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (50ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
[THEME] MainForm_TableLayout (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_TableLayout (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_MenuStrip (MenuStrip) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_MenuStrip (MenuStrip) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_SplitContainer_Middle (SplitContainer) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_SplitContainer_Middle (SplitContainer) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (SplitterPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (SplitterPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_TabControl (TabControl) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_TabControl (TabControl) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_TabPage_Inventory (TabPage) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_TabPage_Inventory (TabPage) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_InventoryTab (Control_InventoryTab) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_InventoryTab (Control_InventoryTab) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_GroupBox_Main (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_GroupBox_Main (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Label_Notes (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Notes (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_RichTextBox_Notes (RichTextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_RichTextBox_Notes (RichTextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Location (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Location (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_TextBox_Quantity (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_TextBox_Quantity (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Operation (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Operation (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Part (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Part (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Label_Loc (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Loc (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Label_Qty (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Qty (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Label_Op (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Op (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Label_Part (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Part (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Button_Toggle_RightPanel (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Button_Toggle_RightPanel (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Label_Version (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Version (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Button_Save (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Button_Save (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Button_AdvancedEntry (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Button_AdvancedEntry (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_Button_Reset (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Button_Reset (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_GroupBox_Main (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_GroupBox_Main (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_TabControl (TabControl) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_TabControl (TabControl) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_TabControl_Single (TabPage) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_TabControl_Single (TabPage) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_TableLayout_Single (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_TableLayout_Single (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_GroupBox_Right (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_GroupBox_Right (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_ListView (ListView) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_ListView (ListView) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Save (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Save (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Reset (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Reset (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Normal (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Normal (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_GroupBox_Left (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_GroupBox_Left (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Part (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Part (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Send (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Button_Send (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_ComboBox_Part (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_ComboBox_Part (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Op (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Op (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Qty (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Qty (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Loc (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Loc (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_ComboBox_Op (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_ComboBox_Op (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Count (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Count (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_TextBox_Qty (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_TextBox_Qty (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_ComboBox_Loc (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_ComboBox_Loc (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Notes (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_Label_Notes (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_TextBox_Count (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_TextBox_Count (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] panel4 (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] panel4 (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_TabControl_MultiLoc (TabPage) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_TabControl_MultiLoc (TabPage) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Button_SaveAll (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Button_SaveAll (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Button_Reset (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Button_Reset (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Multi_Button_Normal (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Multi_Button_Normal (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] panel1 (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] panel1 (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ListView_Preview (ListView) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ListView_Preview (ListView) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] panel2 (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] panel2 (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Button_AddLoc (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Button_AddLoc (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Part (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Part (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Op (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Op (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Qty (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Qty (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Notes (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Notes (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Loc (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_Label_Loc (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] panel3 (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] panel3 (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_TabControl_Import (TabPage) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_TabControl_Import (TabPage) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_TableLayout (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_TableLayout (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_Panel_Middle (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_Panel_Middle (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_DataGridView (DataGridView) - BackColor: DataGridBackColor = Color [A=255, R=255, G=255, B=255] | Theme: Forest
[THEME] AdvancedInventory_Import_DataGridView (DataGridView) - ForeColor: DataGridForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (HScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (HScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (VScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (VScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_Normal (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_Normal (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_Save (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_Save (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_OpenExcel (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_OpenExcel (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_ImportExcel (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] AdvancedInventory_Import_Button_ImportExcel (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_TabPage_Remove (TabPage) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_TabPage_Remove (TabPage) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_RemoveTab (Control_RemoveTab) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_RemoveTab (Control_RemoveTab) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_GroupBox_MainControl (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_GroupBox_MainControl (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Panel_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Panel_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Panel_DataGridView (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Panel_DataGridView (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Image_NothingFound (PictureBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Image_NothingFound (PictureBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_DataGridView_Main (DataGridView) - BackColor: DataGridBackColor = Color [A=255, R=255, G=255, B=255] | Theme: Forest
[THEME] Control_RemoveTab_DataGridView_Main (DataGridView) - ForeColor: DataGridForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (HScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (HScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (VScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (VScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Panel_Header (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Panel_Header (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Label_Part (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Label_Part (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_ComboBox_Part (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_ComboBox_Part (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Label_Operation (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Label_Operation (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_ComboBox_Operation (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_ComboBox_Operation (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_ShowAll (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_ShowAll (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_AdvancedItemRemoval (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_AdvancedItemRemoval (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_Delete (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_Delete (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_Search (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_Search (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_Toggle_RightPanel (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_Toggle_RightPanel (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_Reset (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_Reset (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_Print (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_Print (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_RemoveTab_Button_Undo (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_RemoveTab_Button_Undo (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_GroupBox_Main (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_GroupBox_Main (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Reset (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Reset (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Print (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Print (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Normal (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Normal (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Search (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Search (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Undo (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Undo (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_SidePanel (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_SidePanel (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Delete (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Button_Delete (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Panel_Top (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Panel_Top (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (SplitterPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (SplitterPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_DateDash (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_DateDash (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Location (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Location (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Part (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Part (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Loc (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Loc (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Op (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Op (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_User (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_User (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Notes (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Notes (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_CheckBox_Date (CheckBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_CheckBox_Date (CheckBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Qty (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Qty (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_ComboBox_User (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_ComboBox_User (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Operation (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Operation (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Part (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_Part (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Notes (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_Notes (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_QtyMin (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_QtyMin (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_QtyMax (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_TextBox_QtyMax (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_QtyDash (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Label_QtyDash (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (SplitterPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (SplitterPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Panel_Row4_Center (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Panel_Row4_Center (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_Image_NothingFound (PictureBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_AdvancedRemove_Image_NothingFound (PictureBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_AdvancedRemove_DataGridView_Results (DataGridView) - BackColor: DataGridBackColor = Color [A=255, R=255, G=255, B=255] | Theme: Forest
[THEME] Control_AdvancedRemove_DataGridView_Results (DataGridView) - ForeColor: DataGridForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (HScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (HScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (VScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (VScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_TabPage_Transfer (TabPage) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_TabPage_Transfer (TabPage) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_TransferTab (Control_TransferTab) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_TransferTab (Control_TransferTab) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_GroupBox_MainControl (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_GroupBox_MainControl (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Panel_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Panel_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_Database_TableLayout_Top (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_Database_TableLayout_Top (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Button_Toggle_Split (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Button_Toggle_Split (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Button_Toggle_RightPanel (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Button_Toggle_RightPanel (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_SplitContainer_Main (SplitContainer) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_SplitContainer_Main (SplitContainer) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (SplitterPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (SplitterPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_ComboBox_Operation (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_ComboBox_Operation (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_ComboBox_Part (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_ComboBox_Part (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (UpDownButtons) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (UpDownButtons) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (UpDownEdit) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (UpDownEdit) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_ComboBox_ToLocation (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_ComboBox_ToLocation (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Label_Part (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Label_Part (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Label_Operation (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Label_Operation (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Label_ToLocation (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Label_ToLocation (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Label_Quantity (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Label_Quantity (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Button_Transfer (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Button_Transfer (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Button_Search (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Button_Search (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Button_Print (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Button_Print (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Button_Reset (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Button_Reset (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (SplitterPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (SplitterPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] panel1 (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] panel1 (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Panel_DataGridView (Panel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Panel_DataGridView (Panel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_Image_NothingFound (PictureBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_TransferTab_Image_NothingFound (PictureBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_TransferTab_DataGridView_Main (DataGridView) - BackColor: DataGridBackColor = Color [A=255, R=255, G=255, B=255] | Theme: Forest
[THEME] Control_TransferTab_DataGridView_Main (DataGridView) - ForeColor: DataGridForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (HScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (HScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (VScrollBar) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (VScrollBar) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (SplitterPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (SplitterPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_QuickButtons (Control_QuickButtons) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_QuickButtons (Control_QuickButtons) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button10 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button10 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button9 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button9 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button8 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button8 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button7 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button7 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button6 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button6 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button5 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button5 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button4 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button4 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button3 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button3 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button2 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button2 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button1 (Button) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_QuickButtons_Button_Button1 (Button) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] tableLayoutPanel1 (TableLayoutPanel) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] tableLayoutPanel1 (TableLayoutPanel) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_StatusStrip (StatusStrip) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_StatusStrip (StatusStrip) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME]  (MdiClient) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME]  (MdiClient) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
[01:22:29.456] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (850ms)
[01:22:30.514] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[01:22:30.543] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[01:22:30.545] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[01:22:30.546] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:30.547] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[01:22:30.553] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (8ms) - Status: 1
[01:22:30.554] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (8ms) - 1 rows
[01:22:30.555] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (9ms)
[01:22:30.557] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (11ms)
[01:22:30.558] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (14ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[01:22:30.562] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[01:22:30.565] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[01:22:30.566] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
[01:22:30.568] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (6ms)
[01:22:30.629] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
[01:22:30.632] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
[01:22:30.633] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
[01:22:30.635] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:30.636] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[01:22:30.654] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (21ms) - Status: 1
[01:22:30.656] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (21ms) - 10 rows
[01:22:30.657] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (22ms)
[01:22:30.658] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (24ms)
[01:22:30.662] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
[01:22:30.696] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (63ms)
[01:22:30.697] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
Resetting user controls...
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
[01:22:37.234] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[01:22:37.244] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (10ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
[01:22:38.665] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_inventory_Get_All
[01:22:38.667] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[01:22:38.668] [MEDIUM] 🗄️ DB PROCEDURE START: inv_inventory_Get_All
[01:22:38.688] [HIGH  ] ✅ PROCEDURE inv_inventory_Get_All (22ms) - Status: 1
[01:22:38.689] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_inventory_Get_All (22ms) - 800 rows
[01:22:38.690] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (23ms)
[01:22:38.692] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_inventory_Get_All (26ms)
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
The program '[28224] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
