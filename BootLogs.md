------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[20:02:50.495] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[20:02:50.529] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[20:02:50.530] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[20:02:50.531] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
[20:02:50.576] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
[20:02:50.632] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:50.633] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[20:02:50.806] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (229ms) - Status: 1
[20:02:50.818] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (229ms) - 9 rows
[20:02:50.820] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (187ms)
[20:02:50.821] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (244ms)
[Startup] Parameter cache: 107 procedures cached in 10ms
[20:02:50.840] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
[20:02:50.841] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:50.842] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
[20:02:50.872] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[20:02:50.874] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[20:02:50.887] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (46ms) - Status: 1
[20:02:50.888] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (46ms) - 88 rows
[20:02:50.889] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
[20:02:50.890] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (49ms)
[20:02:50.900] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[20:02:50.930] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[20:02:50.931] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
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
[20:02:50.953] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[20:02:50.954] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (81ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-01-2025 @ 8-02 PM_normal.log
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[20:02:51.393] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
[20:02:51.395] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.397] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[20:02:51.456] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (62ms) - Status: 1
[20:02:51.457] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (62ms) - 3746 rows
[20:02:51.458] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (62ms)
[20:02:51.459] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (65ms)
[20:02:51.486] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
[20:02:51.488] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.489] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[20:02:51.519] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (32ms) - Status: 1
[20:02:51.522] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (32ms) - 72 rows
[20:02:51.523] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (35ms)
[20:02:51.524] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (37ms)
[20:02:51.530] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
[20:02:51.531] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.532] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[20:02:51.612] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (82ms) - Status: 1
[20:02:51.616] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (82ms) - 10371 rows
[20:02:51.617] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (85ms)
[20:02:51.618] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (87ms)
[20:02:51.630] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
[20:02:51.631] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.632] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[20:02:51.660] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (30ms) - Status: 1
[20:02:51.661] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (30ms) - 88 rows
[20:02:51.663] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (31ms)
[20:02:51.664] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (34ms)
[20:02:51.667] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
[20:02:51.668] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.669] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[20:02:51.699] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (31ms) - Status: 1
[20:02:51.700] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (31ms) - 4 rows
[20:02:51.701] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (32ms)
[20:02:51.702] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (34ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
Running VersionChecker...
[20:02:51.766] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
[20:02:51.767] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.768] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
[20:02:51.799] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (32ms) - Status: 1
[20:02:51.800] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (32ms) - 1 rows
[20:02:51.801] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (33ms)
[20:02:51.802] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (36ms)
Database version retrieved: 5.2.0.0
Version labels updated - App: 5.2.0.0, DB: 5.2.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
[20:02:51.842] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
[20:02:51.844] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.845] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[20:02:51.850] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (7ms) - Status: 1
[20:02:51.851] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (7ms) - 9 rows
[20:02:51.852] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (8ms)
[20:02:51.854] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (11ms)
[20:02:51.886] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[20:02:51.888] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:02:51.889] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[20:02:51.890] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:51.891] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:02:51.919] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (30ms) - Status: 1
[20:02:51.921] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (30ms) - 1 rows
[20:02:51.922] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (32ms)
[20:02:51.924] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (34ms)
[20:02:51.931] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (43ms)
[20:02:51.932] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (46ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
[20:02:52.061] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[20:02:52.062] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:02:52.064] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[20:02:52.065] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:52.067] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:02:52.072] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (7ms) - Status: 1
[20:02:52.073] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (7ms) - 1 rows
[20:02:52.074] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (8ms)
[20:02:52.075] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (10ms)
[20:02:52.077] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (15ms)
[20:02:52.078] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (17ms)
[20:02:52.081] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[20:02:52.082] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:02:52.083] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[20:02:52.085] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:52.086] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:02:52.089] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (6ms) - Status: 1
[20:02:52.090] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (6ms) - 1 rows
[20:02:52.092] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (6ms)
[20:02:52.093] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (9ms)
[20:02:52.095] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (13ms)
[20:02:52.096] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (15ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
[20:02:52.471] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[20:02:52.473] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[20:02:52.496] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[20:02:52.497] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[20:02:52.508] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[20:02:52.515] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[20:02:52.516] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[20:02:52.518] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[20:02:52.520] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[20:02:52.530] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
[20:02:52.531] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[20:02:52.540] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[20:02:52.543] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[20:02:52.545] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[20:02:52.546] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (49ms)
[20:02:52.549] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[20:02:52.549] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
[20:02:52.562] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[20:02:52.585] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
[20:02:52.587] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[20:02:52.588] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[20:02:52.598] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[20:02:52.612] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[20:02:52.613] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[20:02:52.614] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[20:02:52.623] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[20:02:52.624] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[20:02:52.625] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[20:02:52.626] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[20:02:52.627] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (39ms)
[20:02:52.629] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[20:02:52.630] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[20:02:52.639] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[20:02:52.658] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[20:02:52.660] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[20:02:52.664] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[20:02:52.665] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[20:02:52.681] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[20:02:52.683] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[20:02:52.684] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[20:02:52.687] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[20:02:52.698] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[20:02:52.719] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[20:02:52.749] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[20:02:52.750] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[20:02:52.753] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[20:02:52.755] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (4ms)
[20:02:52.757] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[20:02:52.758] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[20:02:52.759] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (9ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[20:02:52.764] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[20:02:52.766] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[20:02:52.768] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[20:02:52.769] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[20:02:52.773] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[20:02:52.775] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] DPI change events wired up.
[20:02:52.777] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[20:02:52.779] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (10ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[20:02:52.781] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[20:02:52.783] [MEDIUM] ⬅️ EXITING MainForm.MainForm (312ms)
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
[20:02:52.789] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:02:52.790] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[20:02:52.792] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:52.793] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[20:02:52.797] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
[20:02:52.799] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (1ms)
[20:02:52.814] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
[20:02:52.815] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (1ms)
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
[20:02:52.884] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[THEME] MainForm_UserControl_InventoryTab (Control_InventoryTab) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] MainForm_UserControl_InventoryTab (Control_InventoryTab) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[20:02:52.886] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[THEME] Control_InventoryTab_GroupBox_Main (GroupBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_GroupBox_Main (GroupBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[20:02:52.891] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:52.892] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
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
[20:02:52.906] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (19ms) - Status: 1
[THEME] Control_InventoryTab_TextBox_Quantity (TextBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_TextBox_Quantity (TextBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Operation (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_ComboBox_Operation (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[20:02:52.913] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (19ms) - 1 rows
[THEME] Control_InventoryTab_ComboBox_Part (ComboBox) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[20:02:52.915] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (123ms)
[THEME] Control_InventoryTab_ComboBox_Part (ComboBox) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[20:02:52.917] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (31ms)
[THEME] Control_InventoryTab_Label_Loc (Label) - BackColor: FormBackColor = Color [A=255, R=240, G=248, B=240] | Theme: Forest
[THEME] Control_InventoryTab_Label_Loc (Label) - ForeColor: FormForeColor = Color [A=255, R=26, G=26, B=26] | Theme: Forest
[20:02:52.920] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (131ms)
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
[20:02:53.366] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (576ms) - Status: 1
[20:02:53.367] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (576ms) - 1 rows
[20:02:53.370] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (477ms)
[20:02:53.371] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (580ms)
[20:02:53.372] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (488ms)
[20:02:53.374] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (854ms)
[20:02:54.404] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[20:02:54.434] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:02:54.435] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[20:02:54.437] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:54.438] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[20:02:54.442] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (7ms) - Status: 1
[20:02:54.444] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (7ms) - 1 rows
[20:02:54.445] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (7ms)
[20:02:54.446] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (11ms)
[20:02:54.448] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (13ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[20:02:54.452] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[20:02:54.455] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[20:02:54.456] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
[20:02:54.457] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (5ms)
[20:02:54.511] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
[20:02:54.514] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
[20:02:54.515] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
[20:02:54.517] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:54.519] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[20:02:54.543] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (27ms) - Status: 1
[20:02:54.544] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (27ms) - 10 rows
[20:02:54.545] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (27ms)
[20:02:54.547] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (31ms)
[20:02:54.551] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
[20:02:54.587] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (71ms)
[20:02:54.588] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
[20:02:57.515] [MEDIUM] ➡️ ENTERING Dao_Part.GetAllPartsAsync
[20:02:57.517] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
[20:02:57.518] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:57.519] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[20:02:57.524] [MEDIUM] ➡️ ENTERING Dao_User.GetAllUsersAsync
[20:02:57.526] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
[20:02:57.527] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:57.528] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[20:02:57.534] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
[20:02:57.535] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:02:57.536] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[20:02:57.587] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (61ms) - Status: 1
[20:02:57.589] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (61ms) - 88 rows
[20:02:57.590] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
[20:02:57.591] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (65ms)
[20:02:57.593] [MEDIUM] ⬅️ EXITING Dao_User.GetAllUsersAsync (68ms)
[20:02:57.619] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (101ms) - Status: 1
[20:02:57.622] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (101ms) - 3746 rows
[20:02:57.623] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (96ms)
[20:02:57.625] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (108ms)
[20:02:57.627] [MEDIUM] ⬅️ EXITING Dao_Part.GetAllPartsAsync (111ms)
[20:02:57.649] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (114ms) - Status: 1
[20:02:57.652] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (114ms) - 10371 rows
[20:02:57.653] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (117ms)
[20:02:57.654] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (120ms)
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[20:03:03.017] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
[20:03:03.018] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:03:03.019] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
[20:03:03.077] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (60ms) - Status: 1
[20:03:03.078] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (60ms) - 50 rows
[20:03:03.079] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (61ms)
[20:03:03.081] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (64ms)
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
The program '[9772] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
