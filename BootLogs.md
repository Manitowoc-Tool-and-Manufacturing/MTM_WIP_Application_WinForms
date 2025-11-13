------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[21:14:30.563] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-12 21:14:30 - [21:14:30.563] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[21:14:30.597] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-12 21:14:30 - [21:14:30.597] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[21:14:30.599] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-12 21:14:30 - [21:14:30.599] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[21:14:30.601] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-12 21:14:30 - [21:14:30.601] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-12 21:14:30 - [Startup] Application initialization started
2025-11-12 21:14:30 - [Startup] User identified: JOHNK
2025-11-12 21:14:30 - [Dao_System] Checking database connectivity
[21:14:30.633] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 21:14:30 - [21:14:30.633] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 21:14:30 - [21:14:30.633] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985788706333960"}
[21:14:30.701] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:30 - [21:14:30.701] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:30.703] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-12 21:14:30 - [21:14:30.703] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[21:14:30.906] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (273ms) - Status: 1
2025-11-12 21:14:30 - [21:14:30.906] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (273ms) - Status: 1
2025-11-12 21:14:30 - [21:14:30.906] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":273,"Thread":15,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[21:14:30.920] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (273ms) - 9 rows
2025-11-12 21:14:30 - [21:14:30.920] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (273ms) - 9 rows
[21:14:30.923] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (221ms)
2025-11-12 21:14:30 - [21:14:30.923] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (221ms)
[21:14:30.926] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (292ms)
2025-11-12 21:14:30 - [21:14:30.926] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (292ms)
2025-11-12 21:14:30 - [21:14:30.926] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":292,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985788706333960","Status":"SUCCESS","RowCount":9}
2025-11-12 21:14:30 - [Dao_System] Database connectivity check passed
2025-11-12 21:14:30 - [Startup] Database connectivity validated successfully
2025-11-12 21:14:30 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-12 21:14:30 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-12 21:14:30 - [Startup] Parameter cache populated: 116 procedures, 519 total parameters
2025-11-12 21:14:30 - [Startup] Parameter prefix cache initialized successfully in 15ms. Cached 116 stored procedures.
[Startup] Parameter cache: 116 procedures cached in 15ms
[21:14:30.953] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-12 21:14:30 - [21:14:30.953] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-12 21:14:30 - [21:14:30.953] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638985788709536597"}
[21:14:30.956] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:30 - [21:14:30.956] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:30.957] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-12 21:14:30 - [21:14:30.957] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-12 21:14:30 - [Startup] Initializing dependency injection container
2025-11-12 21:14:30 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
2025-11-12 21:14:30 - [Startup] Dependency injection container configured successfully
2025-11-12 21:14:30 - [Startup] Dependency injection container initialized successfully
2025-11-12 21:14:30 - [Splash] Initializing splash screen
[21:14:30.999] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (45ms) - Status: 1
2025-11-12 21:14:31 - [21:14:30.999] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (45ms) - Status: 1
2025-11-12 21:14:31 - [21:14:30.999] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":45,"Thread":15,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user access type(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user access type(s)"}
[21:14:31.002] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (45ms) - 88 rows
2025-11-12 21:14:31 - [21:14:31.002] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (45ms) - 88 rows
[21:14:31.004] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
2025-11-12 21:14:31 - [21:14:31.004] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
[21:14:31.005] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (52ms)
2025-11-12 21:14:31 - [21:14:31.005] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (52ms)
2025-11-12 21:14:31 - [21:14:31.005] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_GetUserAccessType","ElapsedMs":52,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638985788709536597","Status":"SUCCESS","RowCount":88}
2025-11-12 21:14:31 - System_UserAccessType executed successfully for user: JOHNK
[21:14:31.030] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-12 21:14:31 - [21:14:31.030] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[21:14:31.033] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-12 21:14:31 - [21:14:31.033] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-12 21:14:31 - [ThemedUserControl] Control_ProgressBarUserControl initialized with automatic theme support
[21:14:31.096] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-12 21:14:31 - [21:14:31.096] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[21:14:31.127] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-12 21:14:31 - [21:14:31.127] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[21:14:31.129] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-12 21:14:31 - [21:14:31.129] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[21:14:31.130] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-12 21:14:31 - [21:14:31.130] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[21:14:31.132] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (101ms)
2025-11-12 21:14:31 - [21:14:31.132] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (101ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-12 21:14:31 - [ThemedUserControl] Using applier for _progressControl
2025-11-12 21:14:31 - [FormThemeApplier] Applying to '_progressControl' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-12 21:14:31 - [FormThemeApplier] Applying to '_progressControl' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-12 21:14:31 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:31 - [FocusUtils] CanControlReceiveFocus:  (PictureBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:31 - [FocusUtils] ApplyFocusEventHandling:  (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:31 - [FocusUtils] CanControlReceiveFocus:  (ProgressBar) - FALSE (control type cannot receive focus)
2025-11-12 21:14:31 - [FocusUtils] ApplyFocusEventHandling:  (ProgressBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:31 - [FocusUtils] CanControlReceiveFocus:  (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:31 - [FocusUtils] ApplyFocusEventHandling:  (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:31 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-12-2025 @ 9-14 PM_normal.csv
2025-11-12 21:14:31 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-12 21:14:31 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-12 21:14:31 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-12 21:14:31 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-12 21:14:31 - [Splash] Starting async database connectivity verification
2025-11-12 21:14:31 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-12 21:14:31 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[21:14:31.576] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-12 21:14:31 - [21:14:31.576] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-12 21:14:31 - [21:14:31.576] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638985788715762776"}
[21:14:31.578] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:31 - [21:14:31.578] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:31.581] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-12 21:14:31 - [21:14:31.581] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[21:14:31.648] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (72ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.648] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (72ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.648] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":72,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[21:14:31.653] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (72ms) - 3745 rows
2025-11-12 21:14:31 - [21:14:31.653] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (72ms) - 3745 rows
[21:14:31.656] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (77ms)
2025-11-12 21:14:31 - [21:14:31.656] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (77ms)
[21:14:31.659] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (82ms)
2025-11-12 21:14:31 - [21:14:31.659] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (82ms)
2025-11-12 21:14:31 - [21:14:31.659] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":82,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638985788715762776","Status":"SUCCESS","RowCount":3745}
2025-11-12 21:14:31 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-12 21:14:31 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), ItemType(String), Operations(String)
2025-11-12 21:14:31 - [DataTable] ComboBoxPart: Target schema:
2025-11-12 21:14:31 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[21:14:31.688] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-12 21:14:31 - [21:14:31.688] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-12 21:14:31 - [21:14:31.688] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638985788716886397"}
[21:14:31.693] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:31 - [21:14:31.693] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:31.696] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-12 21:14:31 - [21:14:31.696] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[21:14:31.728] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (40ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.728] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (40ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.728] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":40,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[21:14:31.736] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (40ms) - 72 rows
2025-11-12 21:14:31 - [21:14:31.736] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (40ms) - 72 rows
[21:14:31.739] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (46ms)
2025-11-12 21:14:31 - [21:14:31.739] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (46ms)
[21:14:31.742] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (53ms)
2025-11-12 21:14:31 - [21:14:31.742] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (53ms)
2025-11-12 21:14:31 - [21:14:31.742] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":53,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638985788716886397","Status":"SUCCESS","RowCount":72}
2025-11-12 21:14:31 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-12 21:14:31 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-12 21:14:31 - [DataTable] ComboBoxOperation: Target schema:
2025-11-12 21:14:31 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[21:14:31.753] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 21:14:31 - [21:14:31.753] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 21:14:31 - [21:14:31.753] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985788717533412"}
[21:14:31.756] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:31 - [21:14:31.756] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:31.758] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-12 21:14:31 - [21:14:31.758] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[21:14:31.841] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (88ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.841] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (88ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.841] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":88,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[21:14:31.847] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (88ms) - 10371 rows
2025-11-12 21:14:31 - [21:14:31.847] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (88ms) - 10371 rows
[21:14:31.849] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (92ms)
2025-11-12 21:14:31 - [21:14:31.849] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (92ms)
[21:14:31.851] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (98ms)
2025-11-12 21:14:31 - [21:14:31.851] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (98ms)
2025-11-12 21:14:31 - [21:14:31.851] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":98,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985788717533412","Status":"SUCCESS","RowCount":10371}
2025-11-12 21:14:31 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-12 21:14:31 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-12 21:14:31 - [DataTable] ComboBoxLocation: Target schema:
2025-11-12 21:14:31 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[21:14:31.869] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-12 21:14:31 - [21:14:31.869] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-12 21:14:31 - [21:14:31.869] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638985788718693537"}
[21:14:31.872] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:31 - [21:14:31.872] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:31.874] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-12 21:14:31 - [21:14:31.874] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[21:14:31.904] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.904] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.904] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[21:14:31.907] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
2025-11-12 21:14:31 - [21:14:31.907] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
[21:14:31.910] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-12 21:14:31 - [21:14:31.910] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[21:14:31.911] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-12 21:14:31 - [21:14:31.911] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-12 21:14:31 - [21:14:31.911] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638985788718693537","Status":"SUCCESS","RowCount":88}
2025-11-12 21:14:31 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-12 21:14:31 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-12 21:14:31 - [DataTable] ComboBoxUser: Target schema:
2025-11-12 21:14:31 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[21:14:31.921] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-12 21:14:31 - [21:14:31.921] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-12 21:14:31 - [21:14:31.921] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638985788719211361"}
[21:14:31.924] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:31 - [21:14:31.924] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:31.926] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-12 21:14:31 - [21:14:31.926] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[21:14:31.958] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.958] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-12 21:14:31 - [21:14:31.958] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 4 item type(s)"},"ResultData":"DataTable[4 rows]","ErrorMessage":"Retrieved 4 item type(s)"}
[21:14:31.961] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
2025-11-12 21:14:31 - [21:14:31.961] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
[21:14:31.963] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-12 21:14:31 - [21:14:31.963] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[21:14:31.966] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-12 21:14:31 - [21:14:31.966] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-12 21:14:31 - [21:14:31.966] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_item_types_Get_All","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638985788719211361","Status":"SUCCESS","RowCount":4}
2025-11-12 21:14:31 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-12 21:14:31 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-12 21:14:31 - [DataTable] ComboBoxItemType: Target schema:
2025-11-12 21:14:31 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-12 21:14:31 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-12 21:14:32 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-12 21:14:32 - Running VersionChecker - checking database version information.
[21:14:32.050] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-12 21:14:32 - [21:14:32.050] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-12 21:14:32 - [21:14:32.050] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638985788720506315"}
[21:14:32.053] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.053] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.056] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-12 21:14:32 - [21:14:32.056] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-12 21:14:32 - [Splash] Version checker initialized
[21:14:32.088] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (37ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.088] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (37ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.088] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[21:14:32.091] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (37ms) - 1 rows
2025-11-12 21:14:32 - [21:14:32.091] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (37ms) - 1 rows
[21:14:32.093] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-12 21:14:32 - [21:14:32.093] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[21:14:32.095] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (44ms)
2025-11-12 21:14:32 - [21:14:32.095] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (44ms)
2025-11-12 21:14:32 - [21:14:32.095] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638985788720506315","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.0.0.0
2025-11-12 21:14:32 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.1.0, DB: 6.0.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-12 21:14:32 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[21:14:32.119] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 21:14:32 - [21:14:32.119] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 21:14:32 - [21:14:32.119] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985788721190860"}
[21:14:32.122] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.122] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.124] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-12 21:14:32 - [21:14:32.124] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[21:14:32.133] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (14ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.133] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (14ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.133] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":14,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[21:14:32.136] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (14ms) - 9 rows
2025-11-12 21:14:32 - [21:14:32.136] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (14ms) - 9 rows
[21:14:32.138] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-12 21:14:32 - [21:14:32.138] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[21:14:32.141] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (21ms)
2025-11-12 21:14:32 - [21:14:32.141] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (21ms)
2025-11-12 21:14:32 - [21:14:32.141] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":21,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985788721190860","Status":"SUCCESS","RowCount":9}
2025-11-12 21:14:32 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-12 21:14:32 - Successfully loaded 9 themes from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Default' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Forest' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-12 21:14:32 - [DEBUG] Urban Bloom JSON preview: {"InfoColor": "#8E44AD", "ErrorColor": "#F44336", "AccentColor": "#8E44AD", "SuccessColor": "#BA68C8", "WarningColor": "#FF9800", "FormBackColor": "#F6F0FA", "FormForeColor": "#1A1A1A", "LabelBackColor": "#F6F0FA", "LabelForeColor": "#1A1A1A", "PanelBackColor": "#F6F0FA", "PanelForeColor": "#1A1A1A", "ButtonBackColor": "#EDE3F7", "ButtonForeColor": "#1A1A1A", "ControlBackColor": "#F6F0FA", "ControlForeColor": "#1A1A1A", "ListBoxBackColor": "#FFFFFF", "ListBoxForeColor": "#1A1A1A", "PanelBorderCo
2025-11-12 21:14:32 - [DEBUG] Urban Bloom deserialized - FormBackColor: Color [A=255, R=246, G=240, B=250], FormForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:32 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-12 21:14:32 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-12 21:14:32 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[21:14:32.193] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-12 21:14:32 - [21:14:32.193] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[21:14:32.196] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 21:14:32 - [21:14:32.196] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:14:32.198] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.198] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.198] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788721980620"}
[21:14:32.201] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.201] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.203] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.203] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:14:32.234] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (36ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.234] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (36ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.234] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[21:14:32.239] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (36ms) - 1 rows
2025-11-12 21:14:32 - [21:14:32.239] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (36ms) - 1 rows
[21:14:32.241] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-12 21:14:32 - [21:14:32.241] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[21:14:32.243] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (45ms)
2025-11-12 21:14:32 - [21:14:32.243] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (45ms)
2025-11-12 21:14:32 - [21:14:32.243] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":45,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788721980620","Status":"SUCCESS","RowCount":1}
[21:14:32.253] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (57ms)
2025-11-12 21:14:32 - [21:14:32.253] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (57ms)
[21:14:32.256] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (63ms)
2025-11-12 21:14:32 - [21:14:32.256] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (63ms)
2025-11-12 21:14:32 - Theme system enabled status for user JOHNK: True
[21:14:32.260] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-12 21:14:32 - [21:14:32.260] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[21:14:32.262] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 21:14:32 - [21:14:32.262] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:14:32.264] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.264] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.264] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788722649754"}
[21:14:32.268] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.268] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.270] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.270] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:14:32.275] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.275] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.275] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[21:14:32.278] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-12 21:14:32 - [21:14:32.278] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[21:14:32.280] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-12 21:14:32 - [21:14:32.280] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[21:14:32.282] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-12 21:14:32 - [21:14:32.282] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-12 21:14:32 - [21:14:32.282] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788722649754","Status":"SUCCESS","RowCount":1}
[21:14:32.286] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
2025-11-12 21:14:32 - [21:14:32.286] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
[21:14:32.289] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (28ms)
2025-11-12 21:14:32 - [21:14:32.289] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (28ms)
2025-11-12 21:14:32 - Loaded theme preference for user JOHNK: Urban Bloom
2025-11-12 21:14:32 - Set Model_Application_Variables.ThemeName to: Urban Bloom
2025-11-12 21:14:32 - Theme system initialized for user JOHNK. Final theme: Urban Bloom, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
2025-11-12 21:14:32 - ThemeStore loaded 9 themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-12 21:14:32 - [Splash] ThemeStore loaded from database
2025-11-12 21:14:32 - [Splash] ThemeManager initialized with 'Urban Bloom' theme
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-12 21:14:32 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-12 21:14:32 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-12 21:14:32 - [Splash] Loading theme settings
[21:14:32.424] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-12 21:14:32 - [21:14:32.424] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[21:14:32.427] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 21:14:32 - [21:14:32.427] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:14:32.429] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.429] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.429] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788724296766"}
[21:14:32.432] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.432] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.435] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.435] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:14:32.440] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.440] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.440] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[21:14:32.443] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-12 21:14:32 - [21:14:32.443] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[21:14:32.446] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-12 21:14:32 - [21:14:32.446] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[21:14:32.448] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-12 21:14:32 - [21:14:32.448] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-12 21:14:32 - [21:14:32.448] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788724296766","Status":"SUCCESS","RowCount":1}
[21:14:32.452] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (25ms)
2025-11-12 21:14:32 - [21:14:32.452] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (25ms)
[21:14:32.454] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (29ms)
2025-11-12 21:14:32 - [21:14:32.454] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (29ms)
[21:14:32.457] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-12 21:14:32 - [21:14:32.457] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[21:14:32.460] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 21:14:32 - [21:14:32.460] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:14:32.462] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.462] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.462] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788724623180"}
[21:14:32.466] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.466] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.469] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.469] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:14:32.473] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.473] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.473] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[21:14:32.477] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
2025-11-12 21:14:32 - [21:14:32.477] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
[21:14:32.479] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-12 21:14:32 - [21:14:32.479] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[21:14:32.480] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-12 21:14:32 - [21:14:32.480] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-12 21:14:32 - [21:14:32.480] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788724623180","Status":"SUCCESS","RowCount":1}
[21:14:32.486] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (25ms)
2025-11-12 21:14:32 - [21:14:32.486] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (25ms)
[21:14:32.488] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (30ms)
2025-11-12 21:14:32 - [21:14:32.488] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (30ms)
[21:14:32.491] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-12 21:14:32 - [21:14:32.491] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[21:14:32.493] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 21:14:32 - [21:14:32.493] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[21:14:32.495] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.495] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.495] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788724952058"}
[21:14:32.498] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:32 - [21:14:32.498] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:32.500] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 21:14:32 - [21:14:32.500] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[21:14:32.505] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.505] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-12 21:14:32 - [21:14:32.505] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[21:14:32.508] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-12 21:14:32 - [21:14:32.508] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[21:14:32.510] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-12 21:14:32 - [21:14:32.510] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[21:14:32.513] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-12 21:14:32 - [21:14:32.513] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-12 21:14:32 - [21:14:32.513] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985788724952058","Status":"SUCCESS","RowCount":1}
[21:14:32.516] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
2025-11-12 21:14:32 - [21:14:32.516] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
[21:14:32.519] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (27ms)
2025-11-12 21:14:32 - [21:14:32.519] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (27ms)
2025-11-12 21:14:32 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-12 21:14:32 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-12 21:14:32 - [Splash] Core startup sequence completed
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeDebouncer[0]
      Applying debounced theme change: Urban Bloom (Reason: Login)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Theme changed to 'Urban Bloom' (Reason: Login, User: JOHNK)
2025-11-12 21:14:32 - Theme changed to 'Urban Bloom' - Reason: Login
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 21:14:32 - [ThemedForm] MainForm initialized with automatic theme support
[21:14:32.891] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-12 21:14:32 - [21:14:32.891] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[21:14:32.894] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-12 21:14:32 - [21:14:32.894] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-12 21:14:32 - [ThemedUserControl] Control_ConnectionStrengthControl initialized with automatic theme support
2025-11-12 21:14:32 - [ThemedUserControl] Control_InventoryTab initialized with automatic theme support
[21:14:32.921] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.921] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[21:14:32.923] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.923] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[21:14:32.934] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.934] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[21:14:32.937] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.937] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[21:14:32.940] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.940] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[21:14:32.942] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.942] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[21:14:32.945] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.945] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[21:14:32.948] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.948] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-12 21:14:32 - Inventory tab events wired up.
[21:14:32.952] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.952] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[21:14:32.955] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.955] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[21:14:32.957] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.957] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[21:14:32.960] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-12 21:14:32 - [21:14:32.960] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[21:14:32.962] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (40ms)
2025-11-12 21:14:32 - [21:14:32.962] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (40ms)
[21:14:32.965] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-12 21:14:32 - [21:14:32.965] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[21:14:32.967] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-12 21:14:32 - [21:14:32.967] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-12 21:14:32 - Control_AdvancedInventory constructor entered.
[21:14:32.980] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-12 21:14:32 - [21:14:32.980] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[21:14:32.982] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-12 21:14:32 - [21:14:32.982] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:32 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:32 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:32 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:32 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:32 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - Control_AdvancedInventory constructor exited.
[21:14:33.194] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.194] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[21:14:33.196] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.196] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[21:14:33.207] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.207] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[21:14:33.209] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.209] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[21:14:33.212] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.212] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[21:14:33.214] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.214] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[21:14:33.227] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.227] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[21:14:33.229] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.229] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[21:14:33.231] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.231] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[21:14:33.234] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-12 21:14:33 - [21:14:33.234] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[21:14:33.236] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (41ms)
2025-11-12 21:14:33 - [21:14:33.236] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (41ms)
[21:14:33.239] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-12 21:14:33 - [21:14:33.239] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[21:14:33.241] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-12 21:14:33 - [21:14:33.241] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[21:14:33.252] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-12 21:14:33 - [21:14:33.252] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[21:14:33.254] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-12 21:14:33 - [21:14:33.254] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top (Panel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_To (DateTimePicker)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_To
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_From (DateTimePicker)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_From
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Location (TextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Location
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Location
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Part (TextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date (CheckBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_ComboBox_User (ComboBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_ComboBox_User
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_ComboBox_User
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached DropDown handler for ComboBox Control_AdvancedRemove_ComboBox_User
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_ComboBox_User
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Operation (TextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Notes (TextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Notes
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Notes
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMin (TextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMax (TextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center (Panel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results (DataGridView) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
[21:14:33.426] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-12 21:14:33 - [21:14:33.426] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[21:14:33.432] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-12 21:14:33 - [21:14:33.432] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[21:14:33.434] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 21:14:33 - [21:14:33.434] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_Operation (SuggestionTextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-12 21:14:33 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_Part (SuggestionTextBox)
2025-11-12 21:14:33 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_Part
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: panel1 (Panel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:33 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:33 - Transfer tab events wired up.
[21:14:33.557] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 21:14:33 - [21:14:33.557] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 21:14:33 - [ThemedUserControl] Control_QuickButtons initialized with automatic theme support
[21:14:33.561] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-12 21:14:33 - [21:14:33.561] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[21:14:33.563] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-12 21:14:33 - [21:14:33.563] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[21:14:33.568] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-12 21:14:33 - [21:14:33.568] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[21:14:33.571] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-12 21:14:33 - [21:14:33.571] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[21:14:33.593] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-12 21:14:33 - [21:14:33.593] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[21:14:33.597] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-12 21:14:33 - [21:14:33.597] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[21:14:33.600] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-12 21:14:33 - [21:14:33.600] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[21:14:33.605] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-12 21:14:33 - [21:14:33.605] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[21:14:33.607] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (7ms)
2025-11-12 21:14:33 - [21:14:33.607] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (7ms)
[21:14:33.611] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-12 21:14:33 - [21:14:33.611] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[21:14:33.613] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-12 21:14:33 - [21:14:33.613] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[21:14:33.616] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (18ms)
2025-11-12 21:14:33 - [21:14:33.616] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (18ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[21:14:33.621] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-12 21:14:33 - [21:14:33.621] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[21:14:33.624] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-12 21:14:33 - [21:14:33.624] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[21:14:33.628] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-12 21:14:33 - [21:14:33.628] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[21:14:33.630] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-12 21:14:33 - [21:14:33.630] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[21:14:33.634] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-12 21:14:33 - [21:14:33.634] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[21:14:33.638] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-12 21:14:33 - [21:14:33.638] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-12 21:14:33 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[21:14:33.643] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-12 21:14:33 - [21:14:33.643] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[21:14:33.645] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
2025-11-12 21:14:33 - [21:14:33.645] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[21:14:33.649] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-12 21:14:33 - [21:14:33.649] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[21:14:33.652] [MEDIUM] ⬅️ EXITING MainForm.MainForm (761ms)
2025-11-12 21:14:33 - [21:14:33.652] [MEDIUM] ⬅️ EXITING MainForm.MainForm (761ms)
2025-11-12 21:14:33 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-12 21:14:33 - Inventory tab suggestion controls configured.
[21:14:33.659] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (713ms)
2025-11-12 21:14:33 - [21:14:33.659] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (713ms)
2025-11-12 21:14:33 - Remove tab ComboBoxes loaded.
2025-11-12 21:14:33 - Transfer tab suggestion controls configured.
2025-11-12 21:14:33 - Removal tab events wired up.
2025-11-12 21:14:33 - Initial setup of ComboBoxes in the Remove Tab.
[21:14:33.669] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-12 21:14:33 - [21:14:33.669] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[21:14:33.671] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-12 21:14:33 - [21:14:33.671] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[21:14:33.674] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 21:14:33 - [21:14:33.674] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[21:14:33.676] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 21:14:33 - [21:14:33.676] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 21:14:33 - [21:14:33.674] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985788736741540"}
2025-11-12 21:14:33 - [21:14:33.676] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985788736718516"}
[21:14:33.680] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:33 - [21:14:33.680] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:33.683] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[21:14:33.683] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:33 - [21:14:33.683] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 21:14:33 - [21:14:33.683] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:33.688] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 21:14:33 - [21:14:33.688] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 21:14:33 - [Splash] All form instances configured successfully
2025-11-12 21:14:33 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-12 21:14:33 - [Splash] MainForm uses ThemedForm - automatic theme application
2025-11-12 21:14:33 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
[21:14:33.724] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (50ms) - Status: 1
2025-11-12 21:14:33 - [21:14:33.724] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (50ms) - Status: 1
2025-11-12 21:14:33 - [21:14:33.724] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":50,"Thread":23,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[21:14:33.729] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (50ms) - 1 rows
2025-11-12 21:14:33 - [21:14:33.729] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (50ms) - 1 rows
[21:14:33.732] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
2025-11-12 21:14:33 - [21:14:33.732] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
[21:14:33.734] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (60ms)
2025-11-12 21:14:33 - [21:14:33.734] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (60ms)
2025-11-12 21:14:33 - [21:14:33.734] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":60,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985788736741540","Status":"SUCCESS","RowCount":1}
[21:14:33.738] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (66ms)
2025-11-12 21:14:33 - [21:14:33.738] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (66ms)
2025-11-12 21:14:33 - User full name loaded: John Koll
[21:14:33.742] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (70ms) - Status: 1
2025-11-12 21:14:33 - [21:14:33.742] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (70ms) - Status: 1
2025-11-12 21:14:33 - [21:14:33.742] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":70,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[21:14:33.745] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (70ms) - 1 rows
2025-11-12 21:14:33 - [21:14:33.745] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (70ms) - 1 rows
[21:14:33.748] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (64ms)
2025-11-12 21:14:33 - [21:14:33.748] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (64ms)
[21:14:33.750] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (73ms)
2025-11-12 21:14:33 - [21:14:33.750] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (73ms)
2025-11-12 21:14:33 - [21:14:33.750] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":73,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985788736718516","Status":"SUCCESS","RowCount":1}
[21:14:33.753] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (83ms)
2025-11-12 21:14:33 - [21:14:33.753] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (83ms)
2025-11-12 21:14:33 - User full name loaded: John Koll
2025-11-12 21:14:34 - [ThemedUserControl] Using applier for MainForm_UserControl_InventoryTab
2025-11-12 21:14:34 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:34 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [ThemedUserControl] Using applier for MainForm_UserControl_QuickButtons
2025-11-12 21:14:34 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:34 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[21:14:34.347] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-12 21:14:34 - [21:14:34.347] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 0 controls
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_TableLayout (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_MenuStrip (MenuStrip) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_MenuStrip (MenuStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_SplitContainer_Middle (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_SplitContainer_Middle (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Inventory (TabPage) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Inventory (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_InventoryTab (Control_InventoryTab) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_InventoryTab (Control_InventoryTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-12 21:14:34 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-12 21:14:34 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedInventory - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Remove - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Remove (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_RemoveTab - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_RemoveTab (Control_RemoveTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_ComboBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_ComboBox_Operation (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedRemove - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_To - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_From - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Location - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_ComboBox_User - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMin - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMax - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Transfer (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_TransferTab - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_TransferTab (Control_TransferTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:34 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:34 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_QuickButtons (Control_QuickButtons) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_QuickButtons (Control_QuickButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel1 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel1 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: MainForm_StatusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: MainForm_StatusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] CanControlReceiveFocus:  (MdiClient) - FALSE (control type cannot receive focus)
2025-11-12 21:14:35 - [FocusUtils] ApplyFocusEventHandling:  (MdiClient) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:35 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-12 21:14:35 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:35 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Part
2025-11-12 21:14:35 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:35 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part - EXITING
2025-11-12 21:14:35 - [Splash] MainForm displayed successfully
2025-11-12 21:14:35 - [Splash] MainForm displayed - startup complete
2025-11-12 21:14:35 - [Splash] Splash screen closed
[21:14:35.122] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-12 21:14:35 - [21:14:35.122] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-12 21:14:35 -
2025-11-12 21:14:35 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-12 21:14:35 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-12 21:14:35 - [QuickButtons]   User: JOHNK
2025-11-12 21:14:35 - [QuickButtons] ════════════════════════════════════════════════════════════
[21:14:35.132] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-12 21:14:35 - [21:14:35.132] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-12 21:14:35 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-12 21:14:35 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[21:14:35.139] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.139] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.139] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985788751390048"}
[21:14:35.143] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:35 - [21:14:35.143] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:35.145] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.145] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [ThemedForm] Using FormThemeApplier for MainForm
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:35 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'MainForm' in 46ms
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[21:14:35.205] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-12 21:14:35 - [21:14:35.205] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[21:14:35.208] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.208] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.208] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985788752082698"}
[21:14:35.212] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:35 - [21:14:35.212] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:35.214] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.214] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 21:14:35 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_InventoryTab_TextBox_Part
2025-11-12 21:14:35 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-12 21:14:35 - [FocusUtils] Attached TextChanged to TextBox: Control_InventoryTab_TextBox_Part
[21:14:35.225] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (17ms) - Status: 1
2025-11-12 21:14:35 - [21:14:35.225] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (17ms) - Status: 1
2025-11-12 21:14:35 - [21:14:35.225] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":17,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[21:14:35.232] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (17ms) - 1 rows
2025-11-12 21:14:35 - [21:14:35.232] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (17ms) - 1 rows
[21:14:35.235] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (22ms)
2025-11-12 21:14:35 - [21:14:35.235] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (22ms)
[21:14:35.237] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (29ms)
2025-11-12 21:14:35 - [21:14:35.237] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (29ms)
2025-11-12 21:14:35 - [21:14:35.237] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":29,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985788752082698","Status":"SUCCESS","RowCount":1}
[21:14:35.241] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (35ms)
2025-11-12 21:14:35 - [21:14:35.241] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (35ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[21:14:35.246] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-12 21:14:35 - [21:14:35.246] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[21:14:35.251] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-12 21:14:35 - [21:14:35.251] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[21:14:35.254] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-12 21:14:35 - [21:14:35.254] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-12 21:14:35 - Application Info - Development Menu configured for user 'JOHNK': Visible
[21:14:35.258] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (11ms)
2025-11-12 21:14:35 - [21:14:35.258] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (11ms)
[21:14:35.260] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (121ms) - Status: 1
2025-11-12 21:14:35 - [21:14:35.260] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (121ms) - Status: 1
2025-11-12 21:14:35 - [21:14:35.260] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":121,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 transaction(s) for user: JOHNK"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 transaction(s) for user: JOHNK"}
[21:14:35.265] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (121ms) - 9 rows
2025-11-12 21:14:35 - [21:14:35.265] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (121ms) - 9 rows
[21:14:35.268] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (124ms)
2025-11-12 21:14:35 - [21:14:35.268] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (124ms)
[21:14:35.270] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (131ms)
2025-11-12 21:14:35 - [21:14:35.270] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (131ms)
2025-11-12 21:14:35 - [21:14:35.270] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":131,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985788751390048","Status":"SUCCESS","RowCount":9}
2025-11-12 21:14:35 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 0K2142 + 19 (Qty: 16)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 21-28841-006 + 19 (Qty: 500)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 21179 + 15 (Qty: 500)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 06-96408-001 + N/A (Qty: 40)
2025-11-12 21:14:35 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 10)
2025-11-12 21:14:35 - [Dao_QuickButtons] Array restructured: 9 unique buttons, 0 duplicates removed
2025-11-12 21:14:35 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-12 21:14:35 - [Dao_QuickButtons] All buttons deleted from database
2025-11-12 21:14:35 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 1: 0K2142 + 19 (Qty: 16)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 2: 21-28841-006 + 19 (Qty: 500)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 3: 21179 + 15 (Qty: 500)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 4: 01-31976-000 + 10 (Qty: 1)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 5: 04-27693-000 + 90 (Qty: 10)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 6: 01-34578-000 + 880 (Qty: 20)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 7: 03-29236-030 + 959 (Qty: 30)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 8: 06-96408-001 + N/A (Qty: 40)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created button at position 9: 01-33016-000 + 109 (Qty: 10)
2025-11-12 21:14:35 - [Dao_QuickButtons] Created 9 buttons in database
2025-11-12 21:14:35 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 9 buttons remain
2025-11-12 21:14:35 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-12 21:14:35 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 9 buttons remain
2025-11-12 21:14:35 - [QuickButtons] STEP 2: Loading data from database
[21:14:35.414] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.414] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.414] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985788754147001"}
[21:14:35.419] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:35 - [21:14:35.419] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:35.421] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-12 21:14:35 - [21:14:35.421] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[21:14:35.427] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (12ms) - Status: 1
2025-11-12 21:14:35 - [21:14:35.427] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (12ms) - Status: 1
2025-11-12 21:14:35 - [21:14:35.427] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 transaction(s) for user: JOHNK"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 transaction(s) for user: JOHNK"}
[21:14:35.431] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (12ms) - 9 rows
2025-11-12 21:14:35 - [21:14:35.431] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (12ms) - 9 rows
[21:14:35.434] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-12 21:14:35 - [21:14:35.434] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[21:14:35.437] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (22ms)
2025-11-12 21:14:35 - [21:14:35.437] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (22ms)
2025-11-12 21:14:35 - [21:14:35.437] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":22,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985788754147001","Status":"SUCCESS","RowCount":9}
[21:14:35.444] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-12 21:14:35 - [21:14:35.444] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-12 21:14:35 - [QuickButtons] STEP 2: ✓ Retrieved 9 button(s) from database
2025-11-12 21:14:35 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 1: 0K2142 + Op:19 (Qty: 16)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 2: 21-28841-006 + Op:19 (Qty: 500)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 3: 21179 + Op:15 (Qty: 500)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 4: 01-31976-000 + Op:10 (Qty: 1)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 5: 04-27693-000 + Op:90 (Qty: 10)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 6: 01-34578-000 + Op:880 (Qty: 20)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 7: 03-29236-030 + Op:959 (Qty: 30)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 8: 06-96408-001 + Op:N/A (Qty: 40)
2025-11-12 21:14:35 - [QuickButtons] STEP 3:   Button 9: 01-33016-000 + Op:109 (Qty: 10)
2025-11-12 21:14:35 - [QuickButtons] STEP 3: Filled 9 button(s) with data
2025-11-12 21:14:35 - [QuickButtons] STEP 3: Clearing remaining 1 button(s)
2025-11-12 21:14:35 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-12 21:14:35 - [QuickButtons] STEP 4: Layout refreshed - 9 visible button(s)
2025-11-12 21:14:35 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-12 21:14:35 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-12 21:14:35 - [QuickButtons] ║ User: JOHNK
2025-11-12 21:14:35 - [QuickButtons] ║ Visible Buttons: 9
2025-11-12 21:14:35 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-12 21:14:35 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-12 21:14:35 -
[21:14:35.533] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (400ms)
2025-11-12 21:14:35 - [21:14:35.533] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (400ms)
[21:14:35.536] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-12 21:14:35 - [21:14:35.536] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
2025-11-12 21:14:36 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-12 21:14:36 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-12 21:14:36 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
2025-11-12 21:14:36 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-12 21:14:36 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:36 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Part
2025-11-12 21:14:36 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Part
2025-11-12 21:14:36 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part - EXITING
[21:14:36.984] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-12 21:14:36 - [21:14:36.984] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-12 21:14:36 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_TransferTab_TextBox_Part
2025-11-12 21:14:36 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_TransferTab_TextBox_Part (SuggestionTextBox)
2025-11-12 21:14:36 - [FocusUtils] Attached TextChanged to TextBox: Control_TransferTab_TextBox_Part
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[21:14:37.005] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (20ms)
2025-11-12 21:14:37 - [21:14:37.005] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (20ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
2025-11-12 21:14:37 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-12 21:14:37 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-12 21:14:37 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:37 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Operation
2025-11-12 21:14:37 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:37 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation - EXITING
2025-11-12 21:14:37 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-12 21:14:37 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:37 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:37 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_TransferTab_TextBox_Operation (SuggestionTextBox)
2025-11-12 21:14:37 - [FocusUtils] Attached TextChanged to TextBox: Control_TransferTab_TextBox_Operation
2025-11-12 21:14:37 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Checking if should restore normal BackColor
2025-11-12 21:14:37 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-12 21:14:37 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:38 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-12 21:14:38 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:38 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Operation
2025-11-12 21:14:38 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:38 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation - EXITING
2025-11-12 21:14:38 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Operation
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
2025-11-12 21:14:38 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Checking if should restore normal BackColor
2025-11-12 21:14:38 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-12 21:14:38 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:38 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Part
2025-11-12 21:14:38 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Part
2025-11-12 21:14:38 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part - EXITING
2025-11-12 21:14:38 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-12 21:14:38 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:38 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Part
2025-11-12 21:14:39 - [FocusUtils] TextBox_TextChanged_Handler: Control_TransferTab_TextBox_Part - Clearing highlight
2025-11-12 21:14:39 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:39 - [FocusUtils] TextBox_TextChanged_Handler: Control_TransferTab_TextBox_Part - Clearing highlight
2025-11-12 21:14:39 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:40 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Checking if should restore normal BackColor
[21:14:40.187] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-12 21:14:40 - [21:14:40.187] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-12 21:14:40 - [21:14:40.187] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638985788801876295"}
[21:14:40.193] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:40 - [21:14:40.193] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:40.196] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-12 21:14:40 - [21:14:40.196] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-12 21:14:40 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-12 21:14:40 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation - EXITING
2025-11-12 21:14:40 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-12 21:14:40 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:40 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Operation
[21:14:40.251] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (63ms) - Status: 1
2025-11-12 21:14:40 - [21:14:40.251] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (63ms) - Status: 1
2025-11-12 21:14:40 - [21:14:40.251] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":63,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[21:14:40.255] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (63ms) - 3745 rows
2025-11-12 21:14:40 - [21:14:40.255] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (63ms) - 3745 rows
[21:14:40.257] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (64ms)
2025-11-12 21:14:40 - [21:14:40.257] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (64ms)
[21:14:40.260] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (73ms)
2025-11-12 21:14:40 - [21:14:40.260] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (73ms)
2025-11-12 21:14:40 - [21:14:40.260] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":73,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638985788801876295","Status":"SUCCESS","RowCount":3745}
2025-11-12 21:14:40 - [SuggestionTextBox] Overlay opened: Field=Control_TransferTab_TextBox_Part, Matches=100, Input='21'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 21:14:40 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-12 21:14:40 - [SuggestionTextBox] About to show dialog
2025-11-12 21:14:40 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:40 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:40 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:40 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:40 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:40 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Checking if should restore normal BackColor
2025-11-12 21:14:40 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-12 21:14:40 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:40 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 4ms
2025-11-12 21:14:40 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-12 21:14:40 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [SuggestionOverlay] AcceptSelection called: SelectedItem='21179'
2025-11-12 21:14:40 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-12 21:14:40 - [FocusUtils] Setting BackColor to: Color [A=255, R=142, G=68, B=173] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:40 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Operation - EXITING
2025-11-12 21:14:41 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-12 21:14:41 - [SuggestionTextBox] Captured selectedValue: '21179'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-12 21:14:41 - [SuggestionTextBox] BEFORE text assignment: Field=Control_TransferTab_TextBox_Part, Current Text='21', Will set to='21179'
2025-11-12 21:14:41 - [FocusUtils] TextBox_TextChanged_Handler: Control_TransferTab_TextBox_Part - Clearing highlight
2025-11-12 21:14:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
2025-11-12 21:14:41 - [SuggestionTextBox] AFTER text assignment: Field=Control_TransferTab_TextBox_Part, Text is now='21179'
2025-11-12 21:14:41 - [SuggestionTextBox] Suggestion selected event raised: Field=Control_TransferTab_TextBox_Part, Value='21179', Original='21'
2025-11-12 21:14:41 - [SuggestionTextBox] Focus moved to next control: True
2025-11-12 21:14:41 - [SuggestionTextBox] Overlay closed finally block
2025-11-12 21:14:41 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:41 - [FocusUtils] TextBox_TextChanged_Handler: Control_TransferTab_TextBox_Operation - Clearing highlight
2025-11-12 21:14:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:41 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Checking if should restore normal BackColor
[21:14:41.790] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-12 21:14:41 - [21:14:41.790] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-12 21:14:41 - [21:14:41.790] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638985788817903953"}
[21:14:41.795] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:41 - [21:14:41.795] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:41.798] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-12 21:14:41 - [21:14:41.798] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-12 21:14:41 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-12 21:14:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Operation
[21:14:41.810] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (19ms) - Status: 1
2025-11-12 21:14:41 - [21:14:41.810] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (19ms) - Status: 1
2025-11-12 21:14:41 - [21:14:41.810] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":19,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[21:14:41.814] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (19ms) - 72 rows
2025-11-12 21:14:41 - [21:14:41.814] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (19ms) - 72 rows
[21:14:41.816] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (21ms)
2025-11-12 21:14:41 - [21:14:41.816] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (21ms)
[21:14:41.819] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (28ms)
2025-11-12 21:14:41 - [21:14:41.819] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (28ms)
2025-11-12 21:14:41 - [21:14:41.819] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":28,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638985788817903953","Status":"SUCCESS","RowCount":72}
2025-11-12 21:14:41 - [SuggestionTextBox] Overlay opened: Field=Control_TransferTab_TextBox_Operation, Matches=20, Input='1'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 21:14:41 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-12 21:14:41 - [SuggestionTextBox] About to show dialog
2025-11-12 21:14:41 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:41 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:41 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:41 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:41 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:41 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-12 21:14:41 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:41 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-12 21:14:43 - [SuggestionOverlay] AcceptSelection called: SelectedItem='18'
2025-11-12 21:14:43 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-12 21:14:43 - [SuggestionTextBox] Captured selectedValue: '18'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-12 21:14:43 - [SuggestionTextBox] BEFORE text assignment: Field=Control_TransferTab_TextBox_Operation, Current Text='1', Will set to='18'
2025-11-12 21:14:43 - [FocusUtils] TextBox_TextChanged_Handler: Control_TransferTab_TextBox_Operation - Clearing highlight
2025-11-12 21:14:43 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Operation
2025-11-12 21:14:43 - [SuggestionTextBox] AFTER text assignment: Field=Control_TransferTab_TextBox_Operation, Text is now='18'
2025-11-12 21:14:43 - [SuggestionTextBox] Suggestion selected event raised: Field=Control_TransferTab_TextBox_Operation, Value='18', Original='1'
2025-11-12 21:14:43 - [SuggestionTextBox] Focus moved to next control: True
2025-11-12 21:14:43 - [SuggestionTextBox] Overlay closed finally block
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[21:14:46.494] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 21:14:46 - [21:14:46.494] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 21:14:46 - [21:14:46.494] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985788864943128"}
[21:14:46.498] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:46 - [21:14:46.498] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:46.502] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-12 21:14:46 - [21:14:46.502] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[21:14:46.509] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 21:14:46 - [21:14:46.509] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 21:14:46 - [21:14:46.509] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985788865095047"}
[21:14:46.513] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:14:46 - [21:14:46.513] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:14:46.516] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-12 21:14:46 - [21:14:46.516] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[21:14:46.558] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (64ms) - Status: 1
2025-11-12 21:14:46 - [21:14:46.558] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (64ms) - Status: 1
2025-11-12 21:14:46 - [21:14:46.558] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":64,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[21:14:46.562] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (64ms) - 10371 rows
2025-11-12 21:14:46 - [21:14:46.562] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (64ms) - 10371 rows
[21:14:46.565] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
2025-11-12 21:14:46 - [21:14:46.565] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
[21:14:46.567] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (73ms)
2025-11-12 21:14:46 - [21:14:46.567] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (73ms)
2025-11-12 21:14:46 - [21:14:46.567] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":73,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985788864943128","Status":"SUCCESS","RowCount":10371}
2025-11-12 21:14:46 - [SuggestionTextBox] Overlay opened: Field=Control_TransferTab_TextBox_ToLocation, Matches=100, Input='dd-'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 21:14:46 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-12 21:14:46 - [SuggestionTextBox] About to show dialog
2025-11-12 21:14:46 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:46 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:46 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:46 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:46 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
[21:14:46.598] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (88ms) - Status: 1
2025-11-12 21:14:46 - [21:14:46.598] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (88ms) - Status: 1
2025-11-12 21:14:46 - [21:14:46.598] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":88,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[21:14:46.601] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (88ms) - 10371 rows
2025-11-12 21:14:46 - [21:14:46.601] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (88ms) - 10371 rows
[21:14:46.604] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (104ms)
2025-11-12 21:14:46 - [21:14:46.604] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (104ms)
[21:14:46.606] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (96ms)
2025-11-12 21:14:46 - [21:14:46.606] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (96ms)
2025-11-12 21:14:46 - [21:14:46.606] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":96,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985788865095047","Status":"SUCCESS","RowCount":10371}
2025-11-12 21:14:46 - [SuggestionTextBox] Overlay opened: Field=Control_TransferTab_TextBox_ToLocation, Matches=100, Input='dd-'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 21:14:46 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-12 21:14:46 - [SuggestionTextBox] About to show dialog
2025-11-12 21:14:46 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:46 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:46 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:46 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:46 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:46 - [SuggestionOverlay] CancelSelection called
2025-11-12 21:14:46 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-12 21:14:46 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:46 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-12 21:14:46 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-12 21:14:46 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:46 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-12 21:14:47 - [SuggestionOverlay] AcceptSelection called: SelectedItem='DD-A0-11'
2025-11-12 21:14:47 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-12 21:14:47 - [SuggestionTextBox] Captured selectedValue: 'DD-A0-11'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-12 21:14:47 - [SuggestionTextBox] BEFORE text assignment: Field=Control_TransferTab_TextBox_ToLocation, Current Text='dd-', Will set to='DD-A0-11'
2025-11-12 21:14:47 - [SuggestionTextBox] AFTER text assignment: Field=Control_TransferTab_TextBox_ToLocation, Text is now='DD-A0-11'
2025-11-12 21:14:47 - [SuggestionTextBox] Suggestion selected event raised: Field=Control_TransferTab_TextBox_ToLocation, Value='DD-A0-11', Original='dd-'
2025-11-12 21:14:47 - [SuggestionTextBox] Focus moved to next control: True
2025-11-12 21:14:47 - [SuggestionTextBox] Overlay closed finally block
2025-11-12 21:14:47 - [SuggestionOverlay] CancelSelection called
2025-11-12 21:14:47 - [SuggestionTextBox] Dialog closed with result: Cancel
Exception thrown: 'System.NullReferenceException' in MTM_WIP_Application_Winforms.dll
2025-11-12 21:14:47 - [SuggestionTextBox] Overlay closed finally block
2025-11-12 21:14:47 - Application Error - Object reference not set to an instance of an object.
2025-11-12 21:14:47 - Stack Trace -    at MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox.DisplayOverlay(List`1 suggestions) in C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Controls\Shared\SuggestionTextBox.cs:line 406
   at MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox.ShowSuggestionOverlayAsync() in C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Controls\Shared\SuggestionTextBox.cs:line 361
2025-11-12 21:14:47 - Error context - Caller: ShowSuggestionOverlayAsync, Control: , Context: Control=Control_TransferTab_TextBox_ToLocation, Input=dd-
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 21:14:47 - [ThemedForm] EnhancedErrorDialog initialized with automatic theme support
[21:14:47.748] [MEDIUM] ➡️ ENTERING EnhancedErrorDialog.EnhancedErrorDialog
2025-11-12 21:14:47 - [21:14:47.748] [MEDIUM] ➡️ ENTERING EnhancedErrorDialog.EnhancedErrorDialog
[21:14:47.750] [LOW   ] 🖱️ UI ACTION: ERROR_DIALOG_INITIALIZATION on EnhancedErrorDialog
2025-11-12 21:14:47 - [21:14:47.750] [LOW   ] 🖱️ UI ACTION: ERROR_DIALOG_INITIALIZATION on EnhancedErrorDialog
[21:14:47.762] [MEDIUM] 📊 BUSINESS LOGIC: ERROR_CONTEXT_SETUP
2025-11-12 21:14:47 - [21:14:47.762] [MEDIUM] 📊 BUSINESS LOGIC: ERROR_CONTEXT_SETUP
[21:14:47.764] [LOW   ] 🖱️ UI ACTION: ERROR_DIALOG_SETUP on EnhancedErrorDialog
2025-11-12 21:14:47 - [21:14:47.764] [LOW   ] 🖱️ UI ACTION: ERROR_DIALOG_SETUP on EnhancedErrorDialog
[21:14:47.770] [LOW   ] 🖱️ UI ACTION: ERROR_DIALOG_INITIALIZATION on EnhancedErrorDialog
2025-11-12 21:14:47 - [21:14:47.770] [LOW   ] 🖱️ UI ACTION: ERROR_DIALOG_INITIALIZATION on EnhancedErrorDialog
[21:14:47.772] [MEDIUM] ⬅️ EXITING EnhancedErrorDialog.EnhancedErrorDialog (24ms)
2025-11-12 21:14:47 - [21:14:47.772] [MEDIUM] ⬅️ EXITING EnhancedErrorDialog.EnhancedErrorDialog (24ms)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: tableLayoutMain (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: tableLayoutMain (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: tabControlDetails (TabControl) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: tabControlDetails (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: tabPageSummary (TabPage) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: tabPageSummary (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: labelPlainEnglish (Label) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: labelPlainEnglish (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: pictureBoxIcon (PictureBox) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: pictureBoxIcon (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: tabPageTechnical - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: tabPageTechnical (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: richTextBoxTechnical - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: richTextBoxTechnical (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: tabPageCallStack - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: tabPageCallStack (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: treeViewCallStack - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: treeViewCallStack (TreeView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: tableLayoutButtons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: tableLayoutButtons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: buttonRetry - FALSE (Enabled=True, Visible=False)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: buttonRetry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: buttonCopyDetails (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: buttonCopyDetails (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: buttonReportIssue (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: buttonReportIssue (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: buttonViewLogs (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: buttonViewLogs (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: buttonClose (Button) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: buttonClose (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [FocusUtils] CanControlReceiveFocus: statusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-12 21:14:47 - [FocusUtils] ApplyFocusEventHandling: statusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 21:14:47 - [ThemedForm] Using FormThemeApplier for EnhancedErrorDialog
2025-11-12 21:14:47 - [FormThemeApplier] Applying to 'EnhancedErrorDialog' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 21:14:47 - [FormThemeApplier] Applying to 'EnhancedErrorDialog' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'EnhancedErrorDialog' in 4ms
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
Running VersionChecker...
2025-11-12 21:15:02 - Running VersionChecker - checking database version information.
[21:15:02.060] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-12 21:15:02 - [21:15:02.060] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-12 21:15:02 - [21:15:02.060] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638985789020605746"}
[21:15:02.066] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 21:15:02 - [21:15:02.066] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[21:15:02.068] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-12 21:15:02 - [21:15:02.068] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[21:15:02.073] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (13ms) - Status: 1
2025-11-12 21:15:02 - [21:15:02.073] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (13ms) - Status: 1
2025-11-12 21:15:02 - [21:15:02.073] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":13,"Thread":15,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[21:15:02.076] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (13ms) - 1 rows
2025-11-12 21:15:02 - [21:15:02.076] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (13ms) - 1 rows
[21:15:02.079] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-12 21:15:02 - [21:15:02.079] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[21:15:02.080] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-12 21:15:02 - [21:15:02.080] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-12 21:15:02 - [21:15:02.080] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638985789020605746","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.0.0.0
2025-11-12 21:15:02 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.1.0, DB: 6.0.0.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'EnhancedErrorDialog' unsubscribed from theme changes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'MainForm' unsubscribed from theme changes
2025-11-12 21:15:06 - [Cleanup] Starting application cleanup
2025-11-12 21:15:06 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-12 21:15:06 - [Cleanup] Memory cleanup completed
2025-11-12 21:15:06 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-12 21:15:06 - [Startup] Application shutdown completed
2025-11-12 21:15:06 - [Cleanup] Starting application cleanup
2025-11-12 21:15:06 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-12 21:15:06 - [Cleanup] Memory cleanup completed
2025-11-12 21:15:06 - [Cleanup] Application cleanup completed successfully
2025-11-12 21:15:06 - [Cleanup] Starting application cleanup
2025-11-12 21:15:06 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-12 21:15:06 - [Cleanup] Memory cleanup completed
2025-11-12 21:15:06 - [Cleanup] Application cleanup completed successfully
The program '[29632] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
