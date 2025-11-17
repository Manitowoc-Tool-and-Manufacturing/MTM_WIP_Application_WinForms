------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[07:42:57.072] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-17 07:42:57 - [07:42:57.072] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[07:42:57.106] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-17 07:42:57 - [07:42:57.106] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[07:42:57.108] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-17 07:42:57 - [07:42:57.108] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[07:42:57.110] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-17 07:42:57 - [07:42:57.110] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-17 07:42:57 - [Startup] Application initialization started
2025-11-17 07:42:57 - [Startup] User identified: JOHNK
2025-11-17 07:42:57 - [Dao_System] Checking database connectivity
[07:42:57.142] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-17 07:42:57 - [07:42:57.142] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-17 07:42:57 - [07:42:57.142] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638989621771419213"}
[07:42:57.200] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:57 - [07:42:57.200] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:57.202] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-17 07:42:57 - [07:42:57.202] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[07:42:57.403] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (261ms) - Status: 1
2025-11-17 07:42:57 - [07:42:57.403] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (261ms) - Status: 1
2025-11-17 07:42:57 - [07:42:57.403] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":261,"Thread":14,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[07:42:57.416] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (261ms) - 9 rows
2025-11-17 07:42:57 - [07:42:57.416] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (261ms) - 9 rows
[07:42:57.418] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (217ms)
2025-11-17 07:42:57 - [07:42:57.418] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (217ms)
[07:42:57.420] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (278ms)
2025-11-17 07:42:57 - [07:42:57.420] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (278ms)
2025-11-17 07:42:57 - [07:42:57.420] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":278,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638989621771419213","Status":"SUCCESS","RowCount":9}
2025-11-17 07:42:57 - [Dao_System] Database connectivity check passed
2025-11-17 07:42:57 - [Startup] Database connectivity validated successfully
2025-11-17 07:42:57 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-17 07:42:57 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-17 07:42:57 - [Startup] Parameter cache populated: 120 procedures, 536 total parameters
2025-11-17 07:42:57 - [Startup] Parameter prefix cache initialized successfully in 20ms. Cached 120 stored procedures.
[Startup] Parameter cache: 120 procedures cached in 20ms
[07:42:57.452] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-17 07:42:57 - [07:42:57.452] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-17 07:42:57 - [07:42:57.452] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638989621774522257"}
[07:42:57.454] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:57 - [07:42:57.454] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:57.456] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-17 07:42:57 - [07:42:57.456] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-17 07:42:57 - [Startup] Initializing dependency injection container
2025-11-17 07:42:57 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
2025-11-17 07:42:57 - [Startup] Dependency injection container configured successfully
2025-11-17 07:42:57 - [Startup] Dependency injection container initialized successfully
2025-11-17 07:42:57 - [Splash] Initializing splash screen
[07:42:57.499] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (46ms) - Status: 1
2025-11-17 07:42:57 - [07:42:57.499] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (46ms) - Status: 1
2025-11-17 07:42:57 - [07:42:57.499] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":46,"Thread":8,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user access type(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user access type(s)"}
[07:42:57.501] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (46ms) - 88 rows
2025-11-17 07:42:57 - [07:42:57.501] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (46ms) - 88 rows
[07:42:57.503] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (48ms)
2025-11-17 07:42:57 - [07:42:57.503] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (48ms)
[07:42:57.504] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (52ms)
2025-11-17 07:42:57 - [07:42:57.504] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (52ms)
2025-11-17 07:42:57 - [07:42:57.504] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_GetUserAccessType","ElapsedMs":52,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638989621774522257","Status":"SUCCESS","RowCount":88}
2025-11-17 07:42:57 - System_UserAccessType executed successfully for user: JOHNK
[07:42:57.531] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-17 07:42:57 - [07:42:57.531] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[07:42:57.534] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-17 07:42:57 - [07:42:57.534] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-17 07:42:57 - [ThemedUserControl] Control_ProgressBarUserControl initialized with automatic theme support
[07:42:57.593] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-17 07:42:57 - [07:42:57.593] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[07:42:57.622] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-17 07:42:57 - [07:42:57.622] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[07:42:57.624] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-17 07:42:57 - [07:42:57.624] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[07:42:57.626] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-17 07:42:57 - [07:42:57.626] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[07:42:57.627] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (96ms)
2025-11-17 07:42:57 - [07:42:57.627] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (96ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-17 07:42:57 - [ThemedUserControl] Using applier for _progressControl
2025-11-17 07:42:57 - [FormThemeApplier] Applying to '_progressControl' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-17 07:42:57 - [FormThemeApplier] Applying to '_progressControl' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-17 07:42:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:42:57 - [FocusUtils] CanControlReceiveFocus:  (PictureBox) - FALSE (control type cannot receive focus)
2025-11-17 07:42:57 - [FocusUtils] ApplyFocusEventHandling:  (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:57 - [FocusUtils] CanControlReceiveFocus:  (ProgressBar) - FALSE (control type cannot receive focus)
2025-11-17 07:42:57 - [FocusUtils] ApplyFocusEventHandling:  (ProgressBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:57 - [FocusUtils] CanControlReceiveFocus:  (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:42:57 - [FocusUtils] ApplyFocusEventHandling:  (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:57 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-17-2025 @ 7-42 AM_normal.csv
2025-11-17 07:42:57 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-17 07:42:57 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-17 07:42:57 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-17 07:42:57 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-17 07:42:57 - [Splash] Starting async database connectivity verification
2025-11-17 07:42:58 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-17 07:42:58 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[07:42:58.090] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-17 07:42:58 - [07:42:58.090] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-17 07:42:58 - [07:42:58.090] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638989621780907522"}
[07:42:58.093] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.093] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.095] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-17 07:42:58 - [07:42:58.095] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[07:42:58.160] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (69ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.160] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (69ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.160] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":69,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3747 part(s)"},"ResultData":"DataTable[3747 rows]","ErrorMessage":"Retrieved 3747 part(s)"}
[07:42:58.163] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (69ms) - 3747 rows
2025-11-17 07:42:58 - [07:42:58.163] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (69ms) - 3747 rows
[07:42:58.165] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
2025-11-17 07:42:58 - [07:42:58.165] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
[07:42:58.172] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (81ms)
2025-11-17 07:42:58 - [07:42:58.172] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (81ms)
2025-11-17 07:42:58 - [07:42:58.172] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":81,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638989621780907522","Status":"SUCCESS","RowCount":3747}
2025-11-17 07:42:58 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-17 07:42:58 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), RequiresColorCode(Boolean), ItemType(String), Operations(String)
2025-11-17 07:42:58 - [DataTable] ComboBoxPart: Target schema:
2025-11-17 07:42:58 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[07:42:58.189] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-17 07:42:58 - [07:42:58.189] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-17 07:42:58 - [07:42:58.189] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638989621781891717"}
[07:42:58.192] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.192] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.194] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-17 07:42:58 - [07:42:58.194] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[07:42:58.224] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (35ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.224] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (35ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.224] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[07:42:58.227] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (35ms) - 72 rows
2025-11-17 07:42:58 - [07:42:58.227] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (35ms) - 72 rows
[07:42:58.229] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-17 07:42:58 - [07:42:58.229] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[07:42:58.231] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (42ms)
2025-11-17 07:42:58 - [07:42:58.231] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (42ms)
2025-11-17 07:42:58 - [07:42:58.231] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638989621781891717","Status":"SUCCESS","RowCount":72}
2025-11-17 07:42:58 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-17 07:42:58 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-17 07:42:58 - [DataTable] ComboBoxOperation: Target schema:
2025-11-17 07:42:58 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[07:42:58.243] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-17 07:42:58 - [07:42:58.243] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-17 07:42:58 - [07:42:58.243] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638989621782436629"}
[07:42:58.246] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.246] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.248] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-17 07:42:58 - [07:42:58.248] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[07:42:58.328] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (84ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.328] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (84ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.328] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":84,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[07:42:58.333] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (84ms) - 10371 rows
2025-11-17 07:42:58 - [07:42:58.333] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (84ms) - 10371 rows
[07:42:58.335] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (88ms)
2025-11-17 07:42:58 - [07:42:58.335] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (88ms)
[07:42:58.338] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (94ms)
2025-11-17 07:42:58 - [07:42:58.338] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (94ms)
2025-11-17 07:42:58 - [07:42:58.338] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":94,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638989621782436629","Status":"SUCCESS","RowCount":10371}
2025-11-17 07:42:58 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-17 07:42:58 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-17 07:42:58 - [DataTable] ComboBoxLocation: Target schema:
2025-11-17 07:42:58 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[07:42:58.356] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-17 07:42:58 - [07:42:58.356] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-17 07:42:58 - [07:42:58.356] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638989621783564517"}
[07:42:58.359] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.359] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.361] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-17 07:42:58 - [07:42:58.361] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[07:42:58.390] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (34ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.390] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (34ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.390] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":34,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[07:42:58.393] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (34ms) - 88 rows
2025-11-17 07:42:58 - [07:42:58.393] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (34ms) - 88 rows
[07:42:58.396] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (36ms)
2025-11-17 07:42:58 - [07:42:58.396] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (36ms)
[07:42:58.398] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (41ms)
2025-11-17 07:42:58 - [07:42:58.398] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (41ms)
2025-11-17 07:42:58 - [07:42:58.398] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":41,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638989621783564517","Status":"SUCCESS","RowCount":88}
2025-11-17 07:42:58 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-17 07:42:58 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-17 07:42:58 - [DataTable] ComboBoxUser: Target schema:
2025-11-17 07:42:58 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[07:42:58.407] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-17 07:42:58 - [07:42:58.407] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-17 07:42:58 - [07:42:58.407] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638989621784076385"}
[07:42:58.410] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.410] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.412] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-17 07:42:58 - [07:42:58.412] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[07:42:58.443] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (36ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.443] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (36ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.443] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 4 item type(s)"},"ResultData":"DataTable[4 rows]","ErrorMessage":"Retrieved 4 item type(s)"}
[07:42:58.447] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (36ms) - 4 rows
2025-11-17 07:42:58 - [07:42:58.447] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (36ms) - 4 rows
[07:42:58.449] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-17 07:42:58 - [07:42:58.449] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[07:42:58.452] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-17 07:42:58 - [07:42:58.452] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-17 07:42:58 - [07:42:58.452] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_item_types_Get_All","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638989621784076385","Status":"SUCCESS","RowCount":4}
2025-11-17 07:42:58 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-17 07:42:58 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-17 07:42:58 - [DataTable] ComboBoxItemType: Target schema:
2025-11-17 07:42:58 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-17 07:42:58 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 55, Status: Loading color code cache...
[07:42:58.526] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
2025-11-17 07:42:58 - [07:42:58.526] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[07:42:58.528] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-17 07:42:58 - [07:42:58.528] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-17 07:42:58 - [07:42:58.528] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638989621785284860"}
[07:42:58.531] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.531] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.533] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
2025-11-17 07:42:58 - [07:42:58.533] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[07:42:58.563] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (35ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.563] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (35ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.563] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[6 rows]"}
[07:42:58.566] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (35ms) - 6 rows
2025-11-17 07:42:58 - [07:42:58.566] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (35ms) - 6 rows
[07:42:58.568] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-17 07:42:58 - [07:42:58.568] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[07:42:58.570] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (42ms)
2025-11-17 07:42:58 - [07:42:58.570] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (42ms)
2025-11-17 07:42:58 - [07:42:58.570] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638989621785284860","Status":"SUCCESS","RowCount":6}
[07:42:58.574] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (47ms)
2025-11-17 07:42:58 - [07:42:58.574] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (47ms)
2025-11-17 07:42:58 - [Model_Application_Variables] ColorCodeParts cache loaded: 6 parts
[07:42:58.578] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-17 07:42:58 - [07:42:58.578] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-17 07:42:58 - [07:42:58.578] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638989621785784773"}
[07:42:58.581] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.581] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.584] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-17 07:42:58 - [07:42:58.584] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[07:42:58.613] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (35ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.613] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (35ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.613] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[07:42:58.617] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (35ms) - 10 rows
2025-11-17 07:42:58 - [07:42:58.617] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (35ms) - 10 rows
[07:42:58.618] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-17 07:42:58 - [07:42:58.618] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[07:42:58.620] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (42ms)
2025-11-17 07:42:58 - [07:42:58.620] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (42ms)
2025-11-17 07:42:58 - [07:42:58.620] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638989621785784773","Status":"SUCCESS","RowCount":10}
2025-11-17 07:42:58 - [Model_Application_Variables] ValidColorCodes cache loaded: 10 colors
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 58, Status: Color code cache loaded.
2025-11-17 07:42:58 - [Splash] ColorCodeParts cache loaded: 6 parts flagged
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-17 07:42:58 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-17 07:42:58 - Running VersionChecker - checking database version information.
[07:42:58.686] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:42:58 - [07:42:58.686] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:42:58 - [07:42:58.686] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989621786864582"}
[07:42:58.689] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.689] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.691] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-17 07:42:58 - [07:42:58.691] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-17 07:42:58 - [Splash] Version checker initialized
[07:42:58.722] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (36ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.722] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (36ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.722] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[07:42:58.725] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (36ms) - 1 rows
2025-11-17 07:42:58 - [07:42:58.725] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (36ms) - 1 rows
[07:42:58.727] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-17 07:42:58 - [07:42:58.727] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[07:42:58.729] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (42ms)
2025-11-17 07:42:58 - [07:42:58.729] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (42ms)
2025-11-17 07:42:58 - [07:42:58.729] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989621786864582","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.3.0
2025-11-17 07:42:58 - Version check successful - Database version: 6.2.3.0
Version labels updated - App: 6.2.1.0, DB: 6.2.3.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-17 07:42:58 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[07:42:58.762] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-17 07:42:58 - [07:42:58.762] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-17 07:42:58 - [07:42:58.762] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638989621787624611"}
[07:42:58.765] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.765] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.767] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-17 07:42:58 - [07:42:58.767] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[07:42:58.775] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.775] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.775] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[07:42:58.778] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
2025-11-17 07:42:58 - [07:42:58.778] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
[07:42:58.780] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-17 07:42:58 - [07:42:58.780] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[07:42:58.782] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (19ms)
2025-11-17 07:42:58 - [07:42:58.782] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (19ms)
2025-11-17 07:42:58 - [07:42:58.782] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638989621787624611","Status":"SUCCESS","RowCount":9}
2025-11-17 07:42:58 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-17 07:42:58 - Successfully loaded 9 themes from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Default' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Forest' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-17 07:42:58 - [DEBUG] Urban Bloom JSON preview: {"InfoColor": "#8E44AD", "ErrorColor": "#F44336", "AccentColor": "#8E44AD", "SuccessColor": "#BA68C8", "WarningColor": "#FF9800", "FormBackColor": "#F6F0FA", "FormForeColor": "#1A1A1A", "LabelBackColor": "#F6F0FA", "LabelForeColor": "#1A1A1A", "PanelBackColor": "#F6F0FA", "PanelForeColor": "#1A1A1A", "ButtonBackColor": "#EDE3F7", "ButtonForeColor": "#1A1A1A", "ControlBackColor": "#F6F0FA", "ControlForeColor": "#1A1A1A", "ListBoxBackColor": "#FFFFFF", "ListBoxForeColor": "#1A1A1A", "PanelBorderCo
2025-11-17 07:42:58 - [DEBUG] Urban Bloom deserialized - FormBackColor: Color [A=255, R=246, G=240, B=250], FormForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:42:58 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-17 07:42:58 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-17 07:42:58 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[07:42:58.829] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-17 07:42:58 - [07:42:58.829] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[07:42:58.832] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-17 07:42:58 - [07:42:58.832] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[07:42:58.834] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:58 - [07:42:58.834] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:58 - [07:42:58.834] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621788345194"}
[07:42:58.837] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.837] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.839] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-17 07:42:58 - [07:42:58.839] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[07:42:58.873] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (38ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.873] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (38ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.873] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":38,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[07:42:58.876] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (38ms) - 1 rows
2025-11-17 07:42:58 - [07:42:58.876] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (38ms) - 1 rows
[07:42:58.878] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
2025-11-17 07:42:58 - [07:42:58.878] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
[07:42:58.880] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (46ms)
2025-11-17 07:42:58 - [07:42:58.880] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (46ms)
2025-11-17 07:42:58 - [07:42:58.880] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":46,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621788345194","Status":"SUCCESS","RowCount":1}
[07:42:58.888] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (56ms)
2025-11-17 07:42:58 - [07:42:58.888] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (56ms)
[07:42:58.891] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (61ms)
2025-11-17 07:42:58 - [07:42:58.891] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (61ms)
2025-11-17 07:42:58 - Theme system enabled status for user JOHNK: True
[07:42:58.896] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-17 07:42:58 - [07:42:58.896] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[07:42:58.897] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-17 07:42:58 - [07:42:58.897] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[07:42:58.899] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:58 - [07:42:58.899] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:58 - [07:42:58.899] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621788998141"}
[07:42:58.903] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:58 - [07:42:58.903] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:58.905] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-17 07:42:58 - [07:42:58.905] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[07:42:58.909] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.909] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-17 07:42:58 - [07:42:58.909] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[07:42:58.912] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-17 07:42:58 - [07:42:58.912] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[07:42:58.914] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-17 07:42:58 - [07:42:58.914] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[07:42:58.916] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-17 07:42:58 - [07:42:58.916] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-17 07:42:58 - [07:42:58.916] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621788998141","Status":"SUCCESS","RowCount":1}
[07:42:58.920] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-17 07:42:58 - [07:42:58.920] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[07:42:58.922] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (26ms)
2025-11-17 07:42:58 - [07:42:58.922] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (26ms)
2025-11-17 07:42:58 - Loaded theme preference for user JOHNK: Arctic
2025-11-17 07:42:58 - Set Model_Application_Variables.ThemeName to: Arctic
2025-11-17 07:42:58 - Theme system initialized for user JOHNK. Final theme: Arctic, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
2025-11-17 07:42:58 - ThemeStore loaded 9 themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-17 07:42:58 - [Splash] ThemeStore loaded from database
2025-11-17 07:42:58 - [Splash] ThemeManager initialized with 'Arctic' theme
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-17 07:42:58 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-17 07:42:58 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-17 07:42:59 - [Splash] Loading theme settings
[07:42:59.056] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-17 07:42:59 - [07:42:59.056] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[07:42:59.058] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-17 07:42:59 - [07:42:59.058] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[07:42:59.061] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.061] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.061] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621790610252"}
[07:42:59.064] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:59 - [07:42:59.064] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:59.066] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.066] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[07:42:59.070] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-17 07:42:59 - [07:42:59.070] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-17 07:42:59 - [07:42:59.070] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[07:42:59.073] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-17 07:42:59 - [07:42:59.073] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[07:42:59.075] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-17 07:42:59 - [07:42:59.075] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[07:42:59.077] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-17 07:42:59 - [07:42:59.077] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-17 07:42:59 - [07:42:59.077] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621790610252","Status":"SUCCESS","RowCount":1}
[07:42:59.081] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-17 07:42:59 - [07:42:59.081] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[07:42:59.083] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (26ms)
2025-11-17 07:42:59 - [07:42:59.083] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (26ms)
[07:42:59.085] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-17 07:42:59 - [07:42:59.085] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[07:42:59.087] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-17 07:42:59 - [07:42:59.087] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[07:42:59.089] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.089] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.089] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621790897730"}
[07:42:59.092] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:59 - [07:42:59.092] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:59.094] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.094] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[07:42:59.099] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-17 07:42:59 - [07:42:59.099] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-17 07:42:59 - [07:42:59.099] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[07:42:59.102] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-17 07:42:59 - [07:42:59.102] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[07:42:59.104] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-17 07:42:59 - [07:42:59.104] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[07:42:59.106] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-17 07:42:59 - [07:42:59.106] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-17 07:42:59 - [07:42:59.106] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621790897730","Status":"SUCCESS","RowCount":1}
[07:42:59.110] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-17 07:42:59 - [07:42:59.110] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[07:42:59.112] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (26ms)
2025-11-17 07:42:59 - [07:42:59.112] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (26ms)
[07:42:59.115] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-17 07:42:59 - [07:42:59.115] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[07:42:59.117] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-17 07:42:59 - [07:42:59.117] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[07:42:59.119] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.119] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.119] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621791195472"}
[07:42:59.122] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:59 - [07:42:59.122] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:59.124] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-17 07:42:59 - [07:42:59.124] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[07:42:59.129] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-17 07:42:59 - [07:42:59.129] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-17 07:42:59 - [07:42:59.129] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[07:42:59.132] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-17 07:42:59 - [07:42:59.132] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[07:42:59.135] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-17 07:42:59 - [07:42:59.135] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[07:42:59.137] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-17 07:42:59 - [07:42:59.137] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-17 07:42:59 - [07:42:59.137] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638989621791195472","Status":"SUCCESS","RowCount":1}
[07:42:59.140] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
2025-11-17 07:42:59 - [07:42:59.140] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
[07:42:59.142] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (27ms)
2025-11-17 07:42:59 - [07:42:59.142] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (27ms)
2025-11-17 07:42:59 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-17 07:42:59 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-17 07:42:59 - [Splash] Core startup sequence completed
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeDebouncer[0]
      Applying debounced theme change: Arctic (Reason: Login)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Theme changed to 'Arctic' (Reason: Login, User: JOHNK)
2025-11-17 07:42:59 - Theme changed to 'Arctic' - Reason: Login
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:42:59 - [ThemedForm] MainForm initialized with automatic theme support
[07:42:59.524] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-17 07:42:59 - [07:42:59.524] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[07:42:59.527] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-17 07:42:59 - [07:42:59.527] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-17 07:42:59 - [ThemedUserControl] Control_ConnectionStrengthControl initialized with automatic theme support
2025-11-17 07:42:59 - [ThemedUserControl] Control_InventoryTab initialized with automatic theme support
[07:42:59.558] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.558] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[07:42:59.560] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.560] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[07:42:59.573] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.573] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[07:42:59.577] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.577] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[07:42:59.581] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.581] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[07:42:59.584] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.584] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[07:42:59.587] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.587] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[07:42:59.589] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-17 07:42:59 - [07:42:59.589] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-17 07:42:59 - [07:42:59.589] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638989621795899057"}
[07:42:59.593] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:59 - [07:42:59.593] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:59.595] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-17 07:42:59 - [07:42:59.595] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[07:42:59.599] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-17 07:42:59 - [07:42:59.599] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-17 07:42:59 - [07:42:59.599] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638989621795993269"}
[07:42:59.602] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:59 - [07:42:59.602] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:59.604] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-17 07:42:59 - [07:42:59.604] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[07:42:59.608] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-17 07:42:59 - [07:42:59.608] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-17 07:42:59 - [07:42:59.608] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638989621796080333"}
[07:42:59.610] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:42:59 - [07:42:59.610] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:42:59.612] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-17 07:42:59 - [07:42:59.612] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[07:42:59.617] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.617] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-17 07:42:59 - Inventory tab events wired up.
[07:42:59.622] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.622] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[07:42:59.630] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.630] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[07:42:59.632] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.632] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[07:42:59.635] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-17 07:42:59 - [07:42:59.635] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[07:42:59.637] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (79ms)
2025-11-17 07:42:59 - [07:42:59.637] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (79ms)
[07:42:59.696] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-17 07:42:59 - [07:42:59.696] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[07:42:59.698] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-17 07:42:59 - [07:42:59.698] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-17 07:42:59 - Control_AdvancedInventory constructor entered.
[07:42:59.713] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-17 07:42:59 - [07:42:59.713] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[07:42:59.715] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-17 07:42:59 - [07:42:59.715] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Part
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Part for Part Numbers
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Op
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Op for Operations
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Loc
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Loc for Locations
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Part
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Part for Part Numbers
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Op
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Op for Operations
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Loc
2025-11-17 07:42:59 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Loc for Locations
2025-11-17 07:42:59 - [Control_AdvancedInventory] SuggestionTextBox controls configured for Single Entry and Multi-Location tabs
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_InputToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_InputToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Panel_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Panel_Preview (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 16 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_LocationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_LocationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_OperationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_OperationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_PartF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_PartF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_InputToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_InputToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_LocationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_LocationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_OperationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_OperationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_PartF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_PartF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_CleanSheet - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_CleanSheet (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:42:59 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:42:59 - Control_AdvancedInventory constructor exited.
[07:42:59.995] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-17 07:42:59 - [07:42:59.995] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[07:42:59.997] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-17 07:42:59 - [07:42:59.997] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[07:43:00.010] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.010] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[07:43:00.012] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.012] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Part (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Part (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Operation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_SearchToggle (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_SearchToggle (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_InputPanel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_InputPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
[07:43:00.116] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.116] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[07:43:00.120] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.120] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] F4 handler registered for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Part Numbers
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] F4 handler registered for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Operations
[07:43:00.128] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.128] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[07:43:00.130] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.130] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[07:43:00.133] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.133] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[07:43:00.136] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-17 07:43:00 - [07:43:00.136] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[07:43:00.139] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (143ms)
2025-11-17 07:43:00 - [07:43:00.139] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (143ms)
[07:43:00.142] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-17 07:43:00 - [07:43:00.142] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[07:43:00.144] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-17 07:43:00 - [07:43:00.144] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[07:43:00.223] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-17 07:43:00 - [07:43:00.223] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[07:43:00.225] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-17 07:43:00 - [07:43:00.225] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Bottom_Buttons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Bottom_Buttons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_QuickButtonToggle (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopRow (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopRow (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_DGV (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_DGV (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results (DataGridView) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Inputs (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Inputs (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Inputs (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Inputs (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 15 controls
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Location (TextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Location
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Location
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Part (TextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Part
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Part
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Operation (TextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Operation
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Operation
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Buttons_Left (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Buttons_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Notes (TextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Notes
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Notes
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMin (TextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMin
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMin
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMax (TextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMax
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMax
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateRange (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateRange (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_To (DateTimePicker)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_To
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_From (DateTimePicker)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_From
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Month (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Month (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Today (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Today (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Week (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Week (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Everything (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Everything (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Custom (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Custom (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SuggestionBox_User (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SuggestionBox_User (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
[07:43:00.413] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-17 07:43:00 - [07:43:00.413] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-17 07:43:00 - [Control_AdvancedRemove] Quick filter applied: Month
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Users
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured Control_AdvancedRemove_SuggestionBox_User (composite) for Users
[07:43:00.437] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-17 07:43:00 - [07:43:00.437] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[07:43:00.439] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-17 07:43:00 - [07:43:00.439] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Inputs (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Inputs (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_Inputs (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_Inputs (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 6 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Part (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Operation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel2 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel2 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_SaveSearch (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_SaveSearch (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_Print (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_Print (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel3 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel3 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:00 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:00 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:00 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - Transfer tab events wired up.
[07:43:00.655] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-17 07:43:00 - [07:43:00.655] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-17 07:43:00 - [ThemedUserControl] Control_QuickButtons initialized with automatic theme support
[07:43:00.659] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-17 07:43:00 - [07:43:00.659] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] F4 handler registered for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Part Numbers
[07:43:00.661] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-17 07:43:00 - [07:43:00.661] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] F4 handler registered for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Operations
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] F4 handler registered for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:00 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Locations
[07:43:00.672] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-17 07:43:00 - [07:43:00.672] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[07:43:00.675] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-17 07:43:00 - [07:43:00.675] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[07:43:00.713] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-17 07:43:00 - [07:43:00.713] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[07:43:00.717] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-17 07:43:00 - [07:43:00.717] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[07:43:00.719] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-17 07:43:00 - [07:43:00.719] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[07:43:00.724] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-17 07:43:00 - [07:43:00.724] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[07:43:00.726] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
2025-11-17 07:43:00 - [07:43:00.726] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
[07:43:00.729] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-17 07:43:00 - [07:43:00.729] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[07:43:00.732] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-17 07:43:00 - [07:43:00.732] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[07:43:00.734] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (16ms)
2025-11-17 07:43:00 - [07:43:00.734] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (16ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[07:43:00.739] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-17 07:43:00 - [07:43:00.739] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[07:43:00.742] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-17 07:43:00 - [07:43:00.742] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[07:43:00.745] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-17 07:43:00 - [07:43:00.745] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[07:43:00.747] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-17 07:43:00 - [07:43:00.747] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[07:43:00.752] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-17 07:43:00 - [07:43:00.752] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[07:43:00.755] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-17 07:43:00 - [07:43:00.755] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-17 07:43:00 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[07:43:00.759] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-17 07:43:00 - [07:43:00.759] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[07:43:00.761] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (15ms)
2025-11-17 07:43:00 - [07:43:00.761] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (15ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[07:43:00.765] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-17 07:43:00 - [07:43:00.765] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[07:43:00.768] [MEDIUM] ⬅️ EXITING MainForm.MainForm (1243ms)
2025-11-17 07:43:00 - [07:43:00.768] [MEDIUM] ⬅️ EXITING MainForm.MainForm (1243ms)
2025-11-17 07:43:00 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-17 07:43:00 - Transfer tab suggestion controls configured.
2025-11-17 07:43:00 - Remove tab suggestion controls configured.
2025-11-17 07:43:00 - Removal tab events wired up.
2025-11-17 07:43:00 - Initial setup of ComboBoxes in the Remove Tab.
[07:43:00.779] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-17 07:43:00 - [07:43:00.779] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[07:43:00.782] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[07:43:00.782] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-17 07:43:00 - [07:43:00.782] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-17 07:43:00 - [07:43:00.782] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[07:43:00.785] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-17 07:43:00 - [07:43:00.782] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638989621807820201"}
2025-11-17 07:43:00 - [07:43:00.785] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[07:43:00.789] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:00 - [07:43:00.785] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638989621807856578"}
2025-11-17 07:43:00 - [07:43:00.789] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:00.794] [MEDIUM]         ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:00.794] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-17 07:43:00 - [07:43:00.794] [MEDIUM]         ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:00 - [07:43:00.794] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[07:43:00.797] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-17 07:43:00 - [07:43:00.797] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-17 07:43:00 - [Performance Warning] Stored procedure 'md_operation_numbers_Get_All' (Query) took 1218ms (threshold: 500ms)
[07:43:00.819] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (1218ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.819] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (1218ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.819] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1218,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[07:43:00.825] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (1218ms) - 72 rows
2025-11-17 07:43:00 - [07:43:00.825] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (1218ms) - 72 rows
[07:43:00.827] [MEDIUM]         ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1233ms)
2025-11-17 07:43:00 - [07:43:00.827] [MEDIUM]         ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1233ms)
[07:43:00.830] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (1231ms)
2025-11-17 07:43:00 - [07:43:00.830] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (1231ms)
2025-11-17 07:43:00 - [07:43:00.830] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":1231,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638989621795993269","Status":"SUCCESS","RowCount":72}
2025-11-17 07:43:00 - [Splash] All form instances configured successfully
2025-11-17 07:43:00 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-17 07:43:00 - [Splash] MainForm uses ThemedForm - automatic theme application
2025-11-17 07:43:00 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
2025-11-17 07:43:00 - [Performance Warning] Stored procedure 'md_part_ids_Get_All' (Query) took 1283ms (threshold: 500ms)
[07:43:00.874] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (1283ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.874] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (1283ms) - Status: 1
[07:43:00.878] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (96ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.878] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (96ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.874] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1283,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3747 part(s)"},"ResultData":"DataTable[3747 rows]","ErrorMessage":"Retrieved 3747 part(s)"}
2025-11-17 07:43:00 - [07:43:00.878] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":96,"Thread":23,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[07:43:00.882] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (1283ms) - 3747 rows
[07:43:00.884] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (96ms) - 1 rows
2025-11-17 07:43:00 - [07:43:00.884] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (96ms) - 1 rows
2025-11-17 07:43:00 - [07:43:00.882] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (1283ms) - 3747 rows
[07:43:00.887] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1284ms)
2025-11-17 07:43:00 - [07:43:00.887] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1284ms)
[07:43:00.891] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1279ms)
2025-11-17 07:43:00 - [07:43:00.891] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1279ms)
[07:43:00.893] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1303ms)
[07:43:00.893] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (111ms)
2025-11-17 07:43:00 - [07:43:00.893] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1303ms)
2025-11-17 07:43:00 - [07:43:00.893] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (111ms)
2025-11-17 07:43:00 - [07:43:00.893] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":1303,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638989621795899057","Status":"SUCCESS","RowCount":3747}
2025-11-17 07:43:00 - [07:43:00.893] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":111,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638989621807820201","Status":"SUCCESS","RowCount":1}
[07:43:00.901] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (122ms)
2025-11-17 07:43:00 - [07:43:00.901] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (122ms)
2025-11-17 07:43:00 - User full name loaded: John Koll
[07:43:00.908] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (122ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.908] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (122ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.908] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":122,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[07:43:00.912] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (122ms) - 1 rows
2025-11-17 07:43:00 - [07:43:00.912] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (122ms) - 1 rows
[07:43:00.914] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (125ms)
2025-11-17 07:43:00 - [07:43:00.914] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (125ms)
[07:43:00.916] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (131ms)
2025-11-17 07:43:00 - [07:43:00.916] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (131ms)
2025-11-17 07:43:00 - [07:43:00.916] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":131,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638989621807856578","Status":"SUCCESS","RowCount":1}
[07:43:00.920] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (138ms)
2025-11-17 07:43:00 - [07:43:00.920] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (138ms)
2025-11-17 07:43:00 - User full name loaded: John Koll
2025-11-17 07:43:00 - [Performance Warning] Stored procedure 'md_locations_Get_All' (Query) took 1324ms (threshold: 500ms)
[07:43:00.933] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1324ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.933] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1324ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.933] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1324,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[07:43:00.937] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1324ms) - 10371 rows
2025-11-17 07:43:00 - [07:43:00.937] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1324ms) - 10371 rows
[07:43:00.939] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (145ms)
2025-11-17 07:43:00 - [07:43:00.939] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (145ms)
[07:43:00.943] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1335ms)
2025-11-17 07:43:00 - [07:43:00.943] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1335ms)
2025-11-17 07:43:00 - [07:43:00.943] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":1335,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638989621796080333","Status":"SUCCESS","RowCount":10371}
[07:43:00.965] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
2025-11-17 07:43:00 - [07:43:00.965] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[07:43:00.967] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-17 07:43:00 - [07:43:00.967] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-17 07:43:00 - [07:43:00.967] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638989621809674036"}
[07:43:00.971] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:00 - [07:43:00.971] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:00.973] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
2025-11-17 07:43:00 - [07:43:00.973] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[07:43:00.978] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (11ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.978] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (11ms) - Status: 1
2025-11-17 07:43:00 - [07:43:00.978] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[6 rows]"}
[07:43:00.983] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (11ms) - 6 rows
2025-11-17 07:43:00 - [07:43:00.983] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (11ms) - 6 rows
[07:43:00.985] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-17 07:43:00 - [07:43:00.985] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[07:43:00.988] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (20ms)
2025-11-17 07:43:00 - [07:43:00.988] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (20ms)
2025-11-17 07:43:00 - [07:43:00.988] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638989621809674036","Status":"SUCCESS","RowCount":6}
[07:43:00.992] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (26ms)
2025-11-17 07:43:00 - [07:43:00.992] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (26ms)
2025-11-17 07:43:00 - [Model_Application_Variables] ColorCodeParts cache loaded: 6 parts
[07:43:00.995] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-17 07:43:00 - [07:43:00.995] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-17 07:43:00 - [07:43:00.995] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638989621809956997"}
[07:43:00.999] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:01 - [07:43:00.999] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:01.001] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-17 07:43:01 - [07:43:01.001] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[07:43:01.007] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (11ms) - Status: 1
2025-11-17 07:43:01 - [07:43:01.007] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (11ms) - Status: 1
2025-11-17 07:43:01 - [07:43:01.007] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[07:43:01.010] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (11ms) - 10 rows
2025-11-17 07:43:01 - [07:43:01.010] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (11ms) - 10 rows
[07:43:01.012] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-17 07:43:01 - [07:43:01.012] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[07:43:01.015] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (19ms)
2025-11-17 07:43:01 - [07:43:01.015] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (19ms)
2025-11-17 07:43:01 - [07:43:01.015] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638989621809956997","Status":"SUCCESS","RowCount":10}
2025-11-17 07:43:01 - [Model_Application_Variables] ValidColorCodes cache loaded: 10 colors
2025-11-17 07:43:01 - [InventoryTab Startup] Color code caches loaded: Parts=6, Colors=10
2025-11-17 07:43:01 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Part Numbers
2025-11-17 07:43:01 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Operations
2025-11-17 07:43:01 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Locations
2025-11-17 07:43:01 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Color Codes
2025-11-17 07:43:01 - Inventory tab suggestion controls configured.
[07:43:01.145] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (1558ms)
2025-11-17 07:43:01 - [07:43:01.145] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (1558ms)
2025-11-17 07:43:01 - [ThemedUserControl] Using applier for MainForm_UserControl_InventoryTab
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Location' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Location' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Quantity' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Quantity' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Operation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Operation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Part' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Part' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_ColorCode' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_ColorCode' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_WorkOrder' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_WorkOrder' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: NotesPanel (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: NotesPanel (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 6 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Location (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Location (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Quantity (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Quantity (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Operation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Part (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Part (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_ColorCode (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_WorkOrder (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [ThemedUserControl] Using applier for MainForm_UserControl_QuickButtons
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:01 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[07:43:01.640] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-17 07:43:01 - [07:43:01.640] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 0 controls
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_TableLayout (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_MenuStrip (MenuStrip) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_MenuStrip (MenuStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_SplitContainer_Middle (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_SplitContainer_Middle (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Inventory (TabPage) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Inventory (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_InventoryTab (Control_InventoryTab) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_InventoryTab (Control_InventoryTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: NotesPanel (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: NotesPanel (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 6 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Location (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Location (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Quantity (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Quantity (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Operation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_Part (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_Part (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:01 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:01 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_ColorCode (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_SuggestionBox_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_SuggestionBox_WorkOrder (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedInventory - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_InputToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_InputToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Panel_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Panel_Preview (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 16 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_LocationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_LocationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_OperationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_OperationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_PartF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_PartF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:01 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:01 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_InputToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_InputToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_LocationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_LocationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_OperationF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_OperationF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_PartF4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_PartF4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_CleanSheet - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_CleanSheet (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Remove - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Remove (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_RemoveTab - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_RemoveTab (Control_RemoveTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Part (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_SearchToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_SearchToggle (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_InputPanel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_InputPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedRemove - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Bottom_Buttons - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Bottom_Buttons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_QuickButtonToggle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_QuickButtonToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopRow - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopRow (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_DGV - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_DGV (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Inputs - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Inputs (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Inputs - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Inputs (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 15 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Location - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Buttons_Left - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Buttons_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMin - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMax - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateRange - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateRange (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_To - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_From - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_QuickFilters - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Month - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Month (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Today - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Today (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Week - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Week (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Everything - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Everything (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Custom - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Custom (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SuggestionBox_User - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SuggestionBox_User (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Transfer (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_TransferTab - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_TransferTab (Control_TransferTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Inputs - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Inputs (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_Inputs - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_Inputs (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 6 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel2 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel2 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_SaveSearch - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_SaveSearch (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TableLayout_Print - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TableLayout_Print (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel3 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel3 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_QuickButtons (Control_QuickButtons) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_QuickButtons (Control_QuickButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel1 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel1 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_StatusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_StatusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] CanControlReceiveFocus:  (MdiClient) - FALSE (control type cannot receive focus)
2025-11-17 07:43:02 - [FocusUtils] ApplyFocusEventHandling:  (MdiClient) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:02 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - ENTERING
2025-11-17 07:43:02 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:02 - [FocusUtils] Calling SelectAll on TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:02 - [FocusUtils] Queueing BeginInvoke to attach handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:02 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - EXITING
2025-11-17 07:43:02 - [Splash] MainForm displayed successfully
2025-11-17 07:43:02 - [Splash] MainForm displayed - startup complete
2025-11-17 07:43:02 - [Splash] Splash screen closed
[07:43:02.671] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-17 07:43:02 - [07:43:02.671] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-17 07:43:02 -
2025-11-17 07:43:02 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-17 07:43:02 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-17 07:43:02 - [QuickButtons]   User: JOHNK
2025-11-17 07:43:02 - [QuickButtons] ════════════════════════════════════════════════════════════
[07:43:02.681] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-17 07:43:02 - [07:43:02.681] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-17 07:43:02 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-17 07:43:02 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[07:43:02.688] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:02 - [07:43:02.688] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:02 - [07:43:02.688] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638989621826888201"}
[07:43:02.692] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:02 - [07:43:02.692] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:02.695] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:02 - [07:43:02.695] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:02 - [ThemedForm] Using FormThemeApplier for MainForm
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'MainForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'MainForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Location' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Location' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Quantity' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Quantity' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Operation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Operation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Part' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_Part' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_ColorCode' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_ColorCode' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_WorkOrder' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'Control_InventoryTab_SuggestionBox_WorkOrder' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:02 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_RemoveTab_TextBox_Part' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_RemoveTab_TextBox_Part' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_RemoveTab_TextBox_Operation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_RemoveTab_TextBox_Operation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_AdvancedRemove_SuggestionBox_User' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_AdvancedRemove_SuggestionBox_User' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_TransferTab_TextBox_Part' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_TransferTab_TextBox_Part' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_TransferTab_TextBox_Operation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_TransferTab_TextBox_Operation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_TransferTab_TextBox_ToLocation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'Control_TransferTab_TextBox_ToLocation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:03 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:03 - [Performance Warning] Theme application to form 'MainForm' took 327ms (>100ms threshold)
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[07:43:03.036] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-17 07:43:03 - [07:43:03.036] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[07:43:03.038] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-17 07:43:03 - [07:43:03.038] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-17 07:43:03 - [07:43:03.038] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638989621830389499"}
[07:43:03.043] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:03 - [07:43:03.043] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:03.045] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-17 07:43:03 - [07:43:03.045] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-17 07:43:03 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:03 - [FocusUtils] AttachTextChangedHandlers: Attaching to SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:03 - [FocusUtils] Attached TextChanged to TextBox: SuggestionTextBoxWithLabel_TextBox_Main
[07:43:03.054] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (15ms) - Status: 1
2025-11-17 07:43:03 - [07:43:03.054] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (15ms) - Status: 1
2025-11-17 07:43:03 - [07:43:03.054] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":15,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[07:43:03.060] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (15ms) - 1 rows
2025-11-17 07:43:03 - [07:43:03.060] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (15ms) - 1 rows
[07:43:03.062] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (19ms)
2025-11-17 07:43:03 - [07:43:03.062] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (19ms)
[07:43:03.065] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (26ms)
2025-11-17 07:43:03 - [07:43:03.065] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (26ms)
2025-11-17 07:43:03 - [07:43:03.065] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":26,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638989621830389499","Status":"SUCCESS","RowCount":1}
[07:43:03.069] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (32ms)
2025-11-17 07:43:03 - [07:43:03.069] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (32ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[07:43:03.073] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-17 07:43:03 - [07:43:03.073] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[07:43:03.078] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-17 07:43:03 - [07:43:03.078] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[07:43:03.080] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-17 07:43:03 - [07:43:03.080] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-17 07:43:03 - Application Info - Development Menu configured for user 'JOHNK': Visible
[07:43:03.084] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (10ms)
2025-11-17 07:43:03 - [07:43:03.084] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (10ms)
[07:43:03.086] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (397ms) - Status: 1
2025-11-17 07:43:03 - [07:43:03.086] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (397ms) - Status: 1
2025-11-17 07:43:03 - [07:43:03.086] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":397,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10 transaction(s) for user: JOHNK"},"ResultData":"DataTable[10 rows]","ErrorMessage":"Retrieved 10 transaction(s) for user: JOHNK"}
[07:43:03.090] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (397ms) - 10 rows
2025-11-17 07:43:03 - [07:43:03.090] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (397ms) - 10 rows
[07:43:03.094] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (401ms)
2025-11-17 07:43:03 - [07:43:03.094] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (401ms)
[07:43:03.097] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (408ms)
2025-11-17 07:43:03 - [07:43:03.097] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (408ms)
2025-11-17 07:43:03 - [07:43:03.097] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":408,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638989621826888201","Status":"SUCCESS","RowCount":10}
2025-11-17 07:43:03 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: A110147 + 12 (Qty: 5454)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: A110146 + 900 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: A110147 + 90 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: A110146 + 90 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: 0K2142 + 19 (Qty: 16)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: 21-28841-006 + 19 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: 21179 + 15 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-17 07:43:03 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-17 07:43:03 - [Dao_QuickButtons] Array restructured: 10 unique buttons, 0 duplicates removed
2025-11-17 07:43:03 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-17 07:43:03 - [Dao_QuickButtons] All buttons deleted from database
2025-11-17 07:43:03 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 1: A110147 + 12 (Qty: 5454)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 2: A110146 + 900 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 3: A110147 + 90 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 4: A110146 + 90 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 5: 0K2142 + 19 (Qty: 16)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 6: 21-28841-006 + 19 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 7: 21179 + 15 (Qty: 500)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 8: 01-31976-000 + 10 (Qty: 1)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 9: 04-27693-000 + 90 (Qty: 10)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created button at position 10: 01-34578-000 + 880 (Qty: 20)
2025-11-17 07:43:03 - [Dao_QuickButtons] Created 10 buttons in database
2025-11-17 07:43:03 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 10 buttons remain
2025-11-17 07:43:03 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-17 07:43:03 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 10 buttons remain
2025-11-17 07:43:03 - [QuickButtons] STEP 2: Loading data from database
[07:43:03.249] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:03 - [07:43:03.249] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:03 - [07:43:03.249] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638989621832498951"}
[07:43:03.253] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:03 - [07:43:03.253] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:03.255] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-17 07:43:03 - [07:43:03.255] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[07:43:03.261] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (12ms) - Status: 1
2025-11-17 07:43:03 - [07:43:03.261] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (12ms) - Status: 1
2025-11-17 07:43:03 - [07:43:03.261] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10 transaction(s) for user: JOHNK"},"ResultData":"DataTable[10 rows]","ErrorMessage":"Retrieved 10 transaction(s) for user: JOHNK"}
[07:43:03.265] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (12ms) - 10 rows
2025-11-17 07:43:03 - [07:43:03.265] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (12ms) - 10 rows
[07:43:03.267] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-17 07:43:03 - [07:43:03.267] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[07:43:03.270] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (20ms)
2025-11-17 07:43:03 - [07:43:03.270] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (20ms)
2025-11-17 07:43:03 - [07:43:03.270] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638989621832498951","Status":"SUCCESS","RowCount":10}
[07:43:03.278] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-17 07:43:03 - [07:43:03.278] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-17 07:43:03 - [QuickButtons] STEP 2: ✓ Retrieved 10 button(s) from database
2025-11-17 07:43:03 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 1: A110147 + Op:12 (Qty: 5454)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 2: A110146 + Op:900 (Qty: 500)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 3: A110147 + Op:90 (Qty: 500)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 4: A110146 + Op:90 (Qty: 500)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 5: 0K2142 + Op:19 (Qty: 16)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 6: 21-28841-006 + Op:19 (Qty: 500)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 7: 21179 + Op:15 (Qty: 500)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 8: 01-31976-000 + Op:10 (Qty: 1)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 9: 04-27693-000 + Op:90 (Qty: 10)
2025-11-17 07:43:03 - [QuickButtons] STEP 3:   Button 10: 01-34578-000 + Op:880 (Qty: 20)
2025-11-17 07:43:03 - [QuickButtons] STEP 3: Filled 10 button(s) with data
2025-11-17 07:43:03 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-17 07:43:03 - [QuickButtons] STEP 4: Layout refreshed - 10 visible button(s)
2025-11-17 07:43:03 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-17 07:43:03 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-17 07:43:03 - [QuickButtons] ║ User: JOHNK
2025-11-17 07:43:03 - [QuickButtons] ║ Visible Buttons: 10
2025-11-17 07:43:03 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-17 07:43:03 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-17 07:43:03 -
[07:43:03.381] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (698ms)
2025-11-17 07:43:03 - [07:43:03.381] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (698ms)
[07:43:03.383] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-17 07:43:03 - [07:43:03.383] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:43:05 - [ThemedForm] Transactions initialized with automatic theme support
2025-11-17 07:43:05 - [ThemedUserControl] TransactionGridControl initialized with automatic theme support
2025-11-17 07:43:05 - [TransactionDetailPanel] Initializing...
2025-11-17 07:43:05 - [TransactionDetailPanel] Initialization complete.
2025-11-17 07:43:05 - [Model_Transactions_Core_AnalyticsControl] Initializing...
2025-11-17 07:43:05 - [Model_Transactions_Core_AnalyticsControl] Initialization complete.
2025-11-17 07:43:05 - [TransactionGridControl] Initializing...
2025-11-17 07:43:05 - [TransactionGridControl] Export/Print buttons disabled
2025-11-17 07:43:05 - [TransactionGridControl] Initialization complete.
2025-11-17 07:43:05 - [ThemedUserControl] TransactionSearchControl initialized with automatic theme support
2025-11-17 07:43:05 - [TransactionSearchControl] Initializing...
2025-11-17 07:43:05 - [TransactionSearchControl] Quick filter applied: Month
2025-11-17 07:43:05 - [TransactionSearchControl] Initialization complete.
2025-11-17 07:43:05 - [Transactions] Form initializing...
2025-11-17 07:43:05 - [Transactions] User: JOHNK, IsAdmin: True
2025-11-17 07:43:05 - [Transactions] Starting async initialization...
2025-11-17 07:43:05 - [Transactions] Loading dropdown data (parts, users, locations, operations)...
[07:43:05.683] [MEDIUM] ➡️ ENTERING Dao_Part.GetAllPartsAsync
2025-11-17 07:43:05 - [07:43:05.683] [MEDIUM] ➡️ ENTERING Dao_Part.GetAllPartsAsync
[07:43:05.685] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-17 07:43:05 - [07:43:05.685] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-17 07:43:05 - [07:43:05.685] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638989621856853813"}
[07:43:05.688] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:05 - [07:43:05.688] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:05.691] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-17 07:43:05 - [07:43:05.691] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[07:43:05.697] [MEDIUM] ➡️ ENTERING Dao_User.GetAllUsersAsync
2025-11-17 07:43:05 - [07:43:05.697] [MEDIUM] ➡️ ENTERING Dao_User.GetAllUsersAsync
[07:43:05.699] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-17 07:43:05 - [07:43:05.699] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-17 07:43:05 - [07:43:05.699] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638989621856997798"}
[07:43:05.703] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:05 - [07:43:05.703] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:05.706] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-17 07:43:05 - [07:43:05.706] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-17 07:43:05 - [Model_Transactions_ViewModel] LoadLocationsAsync starting...
2025-11-17 07:43:05 - [Model_Transactions_ViewModel] Calling Dao_Location.GetAllLocations...
[07:43:05.715] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-17 07:43:05 - [07:43:05.715] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-17 07:43:05 - [07:43:05.715] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638989621857157262"}
[07:43:05.719] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:05 - [07:43:05.719] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:05.721] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-17 07:43:05 - [07:43:05.721] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-17 07:43:05 - [Model_Transactions_ViewModel] LoadOperationsAsync starting...
[07:43:05.728] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-17 07:43:05 - [07:43:05.728] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-17 07:43:05 - [07:43:05.728] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638989621857286762"}
[07:43:05.732] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:05 - [07:43:05.732] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:05.734] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-17 07:43:05 - [07:43:05.734] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-17 07:43:05 - [ThemedUserControl] Using applier for Transactions_UserControl_Grid
2025-11-17 07:43:05 - [FormThemeApplier] Applying to 'Transactions_UserControl_Grid' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:05 - [FormThemeApplier] Applying to 'Transactions_UserControl_Grid' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:05 - [FormThemeApplier] Applying to 'TransactionGridControl_Model_Transactions_Core_AnalyticsControl' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:05 - [FormThemeApplier] Applying to 'TransactionGridControl_Model_Transactions_Core_AnalyticsControl' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:05 - [FormThemeApplier] Applying to 'TransactionGridControl_TransactionDetailPanel' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:05 - [FormThemeApplier] Applying to 'TransactionGridControl_TransactionDetailPanel' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_StatusStrip_Main (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_StatusStrip_Main (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling:  (ToolStripTextBoxControl) - APPLYING
2025-11-17 07:43:05 - [FocusUtils] Apply: Starting for  (ToolStripTextBoxControl)
2025-11-17 07:43:05 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for
2025-11-17 07:43:05 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for
2025-11-17 07:43:05 - [FocusUtils] Apply: Attached Click handler for TextBox
2025-11-17 07:43:05 - [FocusUtils] Apply: Completed for
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_DataGridView_Transactions (DataGridView) - FALSE (control type cannot receive focus)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_DataGridView_Transactions (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_Model_Transactions_Core_AnalyticsControl - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_Model_Transactions_Core_AnalyticsControl (Model_Transactions_Core_AnalyticsControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 12 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_Total - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_Total (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Total - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Total (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TotalCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TotalCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TotalValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TotalValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_In - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_In (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_In - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_In (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_InCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_InCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_InValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_InValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_InPercentage - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_InPercentage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_Out - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_Out (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Out - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Out (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_OutCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_OutCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_OutValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_OutValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_OutPercentage - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_OutPercentage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_Transfer (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Transfer (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransferCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransferCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransferValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransferValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransferPercentage - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransferPercentage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_DatabaseLifespan - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_DatabaseLifespan (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_DatabaseLifespan - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_DatabaseLifespan (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_AvgDaily - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_AvgDaily (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_TopUser - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_TopUser (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_TopUser - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_TopUser (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopUserCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopUserCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopUserName - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopUserName (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopUserCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopUserCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_TopPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_TopPart (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_TopPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_TopPart (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopPartCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopPartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopPartID - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopPartID (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopPartCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopPartCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_TransactionRate - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_TransactionRate (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_TransactionRate - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_TransactionRate (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_BusiestDay - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_BusiestDay (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestDay - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestDay (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_PeakHour - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_PeakHour (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_PeakHour - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_PeakHour (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_BusiestLocation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_BusiestLocation (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestLocation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestLocation (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:05 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:05 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_MostTransferredPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_MostTransferredPart (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_MostTransferredPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_MostTransferredPart (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_TransactionDetailPanel (TransactionDetailPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_TransactionDetailPanel (TransactionDetailPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Inner (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Inner (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_RelatedHeader (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_RelatedHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_RelatedTitle (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_RelatedTitle (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Button_ViewBatchHistory (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Button_ViewBatchHistory (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TextBox_Notes (TextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for TransactionDetailPanel_TextBox_Notes (TextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_RelatedStatus (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_RelatedStatus (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Details (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Details (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 22 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_IdCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_IdCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_IdValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_IdValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_TypeCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_TypeCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_TypeValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_TypeValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ItemTypeCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ItemTypeCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ItemTypeValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ItemTypeValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_PartCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_PartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_PartValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_PartValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_BatchCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_BatchCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_BatchValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_BatchValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_QuantityCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_QuantityCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_QuantityValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_QuantityValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_FromCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_FromCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_FromValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_FromValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ToCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ToCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ToValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ToValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_OperationCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_OperationCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_OperationValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_OperationValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_UserCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_UserCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_UserValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_UserValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_DateCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_DateCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_DateValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_DateValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_NotesCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_NotesCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [ThemedUserControl] Using applier for Transactions_UserControl_Search
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'Transactions_UserControl_Search' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'Transactions_UserControl_Search' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_PartNumber' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_PartNumber' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_FromLocation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_FromLocation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_User' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_User' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_ToLocation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_ToLocation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Operation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Operation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Notes' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:06 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Notes' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Filters (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Filters (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Controls (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Controls (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_DateRange (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_DateRange (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_DateTimePicker (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_DateTimePicker (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_DateTimePicker_DateFrom - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_DateTimePicker_DateFrom (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_DateTimePicker_DateTo - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_DateTimePicker_DateTo (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Label_DateFrom (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Label_DateFrom (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Label_DateTo (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Label_DateTo (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Panel_Buttons (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Panel_Buttons (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Buttons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Buttons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_Search (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_Search (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Search (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Search (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_PartNumber (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_PartNumber (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_FromLocation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_FromLocation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_RadioButtons (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_RadioButtons (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Custom (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Custom (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Month (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Month (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Week (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Week (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Today (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Today (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Everything (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Everything (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_User (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_User (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_ToLocation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_ToLocation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_TransactionTypes (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_TransactionTypes (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_TransactionTypes (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_TransactionTypes (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_CheckBox_TRANSFER (CheckBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_CheckBox_TRANSFER (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_CheckBox_OUT (CheckBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_CheckBox_OUT (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_CheckBox_IN (CheckBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_CheckBox_IN (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_Operation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_Notes (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_Notes (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [Transactions] Form loaded. Title: Transaction Viewer - JOHNK (Admin)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_Panel_Main (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_Panel_Main (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_Label_Title (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_Label_Title (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_Panel_Grid (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_Panel_Grid (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_UserControl_Grid (TransactionGridControl) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_UserControl_Grid (TransactionGridControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_StatusStrip_Main (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_StatusStrip_Main (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling:  (ToolStripTextBoxControl) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for  (ToolStripTextBoxControl)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_DataGridView_Transactions (DataGridView) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_DataGridView_Transactions (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_Model_Transactions_Core_AnalyticsControl - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_Model_Transactions_Core_AnalyticsControl (Model_Transactions_Core_AnalyticsControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 12 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_Total - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_Total (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Total - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Total (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TotalCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TotalCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TotalValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TotalValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_In - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_In (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_In - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_In (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_InCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_InCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_InValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_InValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_InPercentage - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_InPercentage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_Out - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_Out (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Out - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Out (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_OutCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_OutCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_OutValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_OutValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_OutPercentage - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_OutPercentage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_Transfer (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_Transfer (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransferCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransferCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransferValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransferValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransferPercentage - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransferPercentage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_DatabaseLifespan - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_DatabaseLifespan (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_DatabaseLifespan - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_DatabaseLifespan (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_DatabaseLifespanValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_AvgDaily - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_AvgDaily (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_TopUser - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_TopUser (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_TopUser - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_TopUser (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopUserCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopUserCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopUserName - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopUserName (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopUserCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopUserCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_TopPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_TopPart (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_TopPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_TopPart (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopPartCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopPartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopPartID - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopPartID (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TopPartCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TopPartCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_TransactionRate - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_TransactionRate (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_TransactionRate - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_TransactionRate (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_TransactionRateTrend (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_BusiestDay - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_BusiestDay (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestDay - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestDay (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestDayCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_PeakHour - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_PeakHour (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_PeakHour - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_PeakHour (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_PeakHourValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_PeakHourCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_BusiestLocation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_BusiestLocation (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestLocation - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_BusiestLocation (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationName (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_BusiestLocationCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Panel_MostTransferredPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Panel_MostTransferredPart (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_TableLayout_MostTransferredPart - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_TableLayout_MostTransferredPart (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCaption - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartID (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Model_Transactions_Core_AnalyticsControl_Label_MostTransferredPartCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionGridControl_TransactionDetailPanel (TransactionDetailPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionGridControl_TransactionDetailPanel (TransactionDetailPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Inner (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Inner (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_RelatedHeader (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_RelatedHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_RelatedTitle (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_RelatedTitle (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Button_ViewBatchHistory (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Button_ViewBatchHistory (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TextBox_Notes (TextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for TransactionDetailPanel_TextBox_Notes (TextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_RelatedStatus (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_RelatedStatus (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Details (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Details (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 22 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_IdCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_IdCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_IdValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_IdValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_TypeCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_TypeCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_TypeValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_TypeValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ItemTypeCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ItemTypeCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ItemTypeValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ItemTypeValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_PartCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_PartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_PartValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_PartValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_BatchCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_BatchCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_BatchValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_BatchValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_QuantityCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_QuantityCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_QuantityValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_QuantityValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_FromCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_FromCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_FromValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_FromValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ToCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ToCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ToValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ToValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_OperationCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_OperationCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_OperationValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_OperationValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_UserCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_UserCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_UserValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_UserValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_DateCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_DateCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_DateValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_DateValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_NotesCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_NotesCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_Panel_Search (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_Panel_Search (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: Transactions_UserControl_Search (TransactionSearchControl) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: Transactions_UserControl_Search (TransactionSearchControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Filters (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Filters (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Controls (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Controls (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_DateRange (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_DateRange (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_DateTimePicker (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_DateTimePicker (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_DateTimePicker_DateFrom - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_DateTimePicker_DateFrom (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_DateTimePicker_DateTo - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_DateTimePicker_DateTo (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Label_DateFrom (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Label_DateFrom (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Label_DateTo (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Label_DateTo (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Panel_Buttons (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Panel_Buttons (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Buttons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Buttons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_Search (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_Search (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_Search (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_Search (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_PartNumber (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_PartNumber (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_FromLocation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_FromLocation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_RadioButtons (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_RadioButtons (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_QuickFilters (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Custom (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Custom (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Month (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Month (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Week (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Week (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Today (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Today (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_RadioButton_Everything (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_RadioButton_Everything (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_User (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_User (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_ToLocation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_ToLocation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:06 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:06 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:06 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:06 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_GroupBox_TransactionTypes (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_GroupBox_TransactionTypes (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_TableLayout_TransactionTypes (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_TableLayout_TransactionTypes (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_CheckBox_TRANSFER (CheckBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_CheckBox_TRANSFER (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_CheckBox_OUT (CheckBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_CheckBox_OUT (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_CheckBox_IN (CheckBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_CheckBox_IN (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_Operation (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_Operation (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - APPLYING
2025-11-17 07:43:07 - [FocusUtils] Apply: Starting for SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [FocusUtils] Apply: Attached Click handler for TextBox SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [FocusUtils] Apply: Completed for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: TransactionSearchControl_Suggestion_Notes (SuggestionTextBoxWithLabel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: TransactionSearchControl_Suggestion_Notes (SuggestionTextBoxWithLabel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Label_Main (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Label_Main (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_TextBox_Main - FALSE (Enabled=False, Visible=True)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] CanControlReceiveFocus: SuggestionTextBoxWithLabel_Button_F4 - FALSE (Enabled=True, Visible=False)
2025-11-17 07:43:07 - [FocusUtils] ApplyFocusEventHandling: SuggestionTextBoxWithLabel_Button_F4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:07 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Checking if should restore normal BackColor
2025-11-17 07:43:07 - [ThemedForm] Using FormThemeApplier for Transactions
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'Transactions' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'Transactions' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'Transactions_UserControl_Grid' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'Transactions_UserControl_Grid' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionGridControl_Model_Transactions_Core_AnalyticsControl' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionGridControl_Model_Transactions_Core_AnalyticsControl' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionGridControl_TransactionDetailPanel' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionGridControl_TransactionDetailPanel' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'Transactions_UserControl_Search' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'Transactions_UserControl_Search' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_PartNumber' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_PartNumber' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_FromLocation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_FromLocation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_User' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_User' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_ToLocation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_ToLocation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Operation' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Operation' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Notes' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:07 - [FormThemeApplier] Applying to 'TransactionSearchControl_Suggestion_Notes' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'Transactions' in 32ms
2025-11-17 07:43:07 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Control not focused, restoring normal BackColor
2025-11-17 07:43:07 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:07 - [Performance Warning] Stored procedure 'md_operation_numbers_Get_All' (Query) took 1421ms (threshold: 500ms)
[07:43:07.151] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (1421ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.151] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (1421ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.151] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1421,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[07:43:07.158] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (1421ms) - 72 rows
2025-11-17 07:43:07 - [07:43:07.158] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (1421ms) - 72 rows
[07:43:07.160] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1428ms)
2025-11-17 07:43:07 - [07:43:07.160] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1428ms)
[07:43:07.163] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (1434ms)
2025-11-17 07:43:07 - [07:43:07.163] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (1434ms)
2025-11-17 07:43:07 - [07:43:07.163] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":1434,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638989621857286762","Status":"SUCCESS","RowCount":72}
2025-11-17 07:43:07 - [Performance Warning] Stored procedure 'usr_users_Get_All' (Query) took 1468ms (threshold: 500ms)
2025-11-17 07:43:07 - [Model_Transactions_ViewModel] LoadOperationsAsync complete: 72 operations cached
[07:43:07.169] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (1468ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.169] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (1468ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.169] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1468,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[07:43:07.176] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (1468ms) - 88 rows
2025-11-17 07:43:07 - [07:43:07.176] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (1468ms) - 88 rows
[07:43:07.180] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1460ms)
2025-11-17 07:43:07 - [07:43:07.180] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1460ms)
[07:43:07.182] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (1482ms)
2025-11-17 07:43:07 - [07:43:07.182] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (1482ms)
2025-11-17 07:43:07 - [07:43:07.182] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":1482,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638989621856997798","Status":"SUCCESS","RowCount":88}
[07:43:07.186] [MEDIUM] ⬅️ EXITING Dao_User.GetAllUsersAsync (1489ms)
2025-11-17 07:43:07 - [07:43:07.186] [MEDIUM] ⬅️ EXITING Dao_User.GetAllUsersAsync (1489ms)
2025-11-17 07:43:07 - [Performance Warning] Stored procedure 'md_part_ids_Get_All' (Query) took 1543ms (threshold: 500ms)
[07:43:07.230] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (1543ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.230] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (1543ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.230] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1543,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3747 part(s)"},"ResultData":"DataTable[3747 rows]","ErrorMessage":"Retrieved 3747 part(s)"}
[07:43:07.235] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (1543ms) - 3747 rows
2025-11-17 07:43:07 - [07:43:07.235] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (1543ms) - 3747 rows
[07:43:07.238] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1534ms)
2025-11-17 07:43:07 - [07:43:07.238] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1534ms)
[07:43:07.240] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1555ms)
2025-11-17 07:43:07 - [07:43:07.240] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1555ms)
2025-11-17 07:43:07 - [07:43:07.240] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":1555,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638989621856853813","Status":"SUCCESS","RowCount":3747}
[07:43:07.244] [MEDIUM] ⬅️ EXITING Dao_Part.GetAllPartsAsync (1561ms)
2025-11-17 07:43:07 - [07:43:07.244] [MEDIUM] ⬅️ EXITING Dao_Part.GetAllPartsAsync (1561ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
2025-11-17 07:43:07 - [Performance Warning] Stored procedure 'md_locations_Get_All' (Query) took 1561ms (threshold: 500ms)
[07:43:07.278] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1561ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.278] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1561ms) - Status: 1
2025-11-17 07:43:07 - [07:43:07.278] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1561,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[07:43:07.283] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1561ms) - 10371 rows
2025-11-17 07:43:07 - [07:43:07.283] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1561ms) - 10371 rows
[07:43:07.285] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1596ms)
2025-11-17 07:43:07 - [07:43:07.285] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (1596ms)
[07:43:07.287] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1572ms)
2025-11-17 07:43:07 - [07:43:07.287] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1572ms)
2025-11-17 07:43:07 - [07:43:07.287] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":1572,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638989621857157262","Status":"SUCCESS","RowCount":10371}
2025-11-17 07:43:07 - [Model_Transactions_ViewModel] DAO returned. Success: True, HasData: True
2025-11-17 07:43:07 - [Model_Transactions_ViewModel] DataTable has 10371 rows, 4 columns
2025-11-17 07:43:07 - [Model_Transactions_ViewModel] DataTable columns: ID, Location, Building, IssuedBy
2025-11-17 07:43:07 - [Model_Transactions_ViewModel] Extracted 10371 non-empty locations from DataTable
2025-11-17 07:43:07 - [Model_Transactions_ViewModel] LoadLocationsAsync complete: 10371 locations cached
2025-11-17 07:43:07 - [Transactions] Data loaded - Parts: 3747, Users: 88, Locations: 10371, Operations: 72
2025-11-17 07:43:07 - [TransactionSearchControl] LoadParts called with 3747 parts
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Part Numbers
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured TransactionSearchControl_Suggestion_PartNumber (composite) for Part Numbers
2025-11-17 07:43:07 - [TransactionSearchControl] Parts loaded: 3747 unique values
2025-11-17 07:43:07 - [TransactionSearchControl] LoadUsers called with 88 users
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Users
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured TransactionSearchControl_Suggestion_User (composite) for Users
2025-11-17 07:43:07 - [TransactionSearchControl] Users loaded: 88 values, Enabled: True
2025-11-17 07:43:07 - [TransactionSearchControl] LoadLocations called with 10371 locations
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Locations
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured TransactionSearchControl_Suggestion_FromLocation (composite) for Locations
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Locations
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured TransactionSearchControl_Suggestion_ToLocation (composite) for Locations
2025-11-17 07:43:07 - [TransactionSearchControl] Locations loaded: 10371 unique codes
2025-11-17 07:43:07 - [TransactionSearchControl] LoadOperations called with 72 operations
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured SuggestionTextBoxWithLabel_TextBox_Main for Operations
2025-11-17 07:43:07 - [Helper_SuggestionTextBox] Configured TransactionSearchControl_Suggestion_Operation (composite) for Operations
2025-11-17 07:43:07 - [TransactionSearchControl] Operations loaded: 72 unique values
2025-11-17 07:43:07 - [Transactions] Dropdown data loaded into search control.
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:43:14 - [TransactionSearchControl] Quick filter changed: Custom
2025-11-17 07:43:14 - [TransactionSearchControl] Quick filter applied: Custom (user-defined dates)
2025-11-17 07:43:15 - [TransactionSearchControl] Quick filter changed: Everything
2025-11-17 07:43:15 - [TransactionSearchControl] Quick filter applied: Everything (all dates)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
2025-11-17 07:43:18 - [TransactionSearchControl] Quick filter changed: Today
2025-11-17 07:43:18 - [TransactionSearchControl] Quick filter applied: Today
2025-11-17 07:43:19 - [TransactionSearchControl] Quick filter changed: Everything
2025-11-17 07:43:19 - [TransactionSearchControl] Quick filter applied: Everything (all dates)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:43:21 - [TransactionSearchControl] Search button clicked.
2025-11-17 07:43:21 - [TransactionSearchControl] Search criteria validated (Everything mode). Types: IN,OUT,TRANSFER
2025-11-17 07:43:21 - [Transactions] Search requested with criteria: Type: IN,OUT,TRANSFER, From: 11/17/2015, To: 11/17/2026
2025-11-17 07:43:21 - [TransactionDetailPanel] Clearing transaction details.
2025-11-17 07:43:21 - [TransactionDetailPanel] Transaction details cleared successfully.
2025-11-17 07:43:21 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:43:21 - [TransactionGridControl.UpdatePaginationControls] _currentResults is NULL
2025-11-17 07:43:21 - [TransactionGridControl] Export/Print buttons disabled
2025-11-17 07:43:21 - [Model_Transactions_ViewModel] SearchTransactionsAsync starting. User: JOHNK, IsAdmin: True, Page: 1
2025-11-17 07:43:21 - [Model_Transactions_ViewModel] Criteria: Type: IN,OUT,TRANSFER, From: 11/17/2015, To: 11/17/2026
2025-11-17 07:43:21 - [Model_Transactions_ViewModel] Calling DAO SearchAsync...
2025-11-17 07:43:21 - [Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
2025-11-17 07:43:21 - [Dao_Transactions] SearchAsync called. User: JOHNK, IsAdmin: True, Page: 1, PageSize: 50
2025-11-17 07:43:21 - [Dao_Transactions] Criteria: Type: IN,OUT,TRANSFER, From: 11/17/2015, To: 11/17/2026
2025-11-17 07:43:21 - [Dao_Transactions] Criteria.TransactionType raw value: 'IN,OUT,TRANSFER'
2025-11-17 07:43:21 - [Dao_Transactions] Multiple transaction types specified: 'IN,OUT,TRANSFER'. Searching all types.
2025-11-17 07:43:21 - [Dao_Transactions] Calling SearchTransactionsAsync with:
2025-11-17 07:43:21 -   - PartID: ''
2025-11-17 07:43:21 -   - User: ''
2025-11-17 07:43:21 -   - FromLocation: ''
2025-11-17 07:43:21 -   - ToLocation: ''
2025-11-17 07:43:21 -   - Operation: ''
2025-11-17 07:43:21 -   - TransactionType: '' (parsed: )
2025-11-17 07:43:21 -   - Notes: ''
2025-11-17 07:43:21 -   - DateFrom: 11/17/2015 12:00:00 AM
2025-11-17 07:43:21 -   - DateTo: 11/17/2026 11:59:59 PM
[07:43:21.095] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
2025-11-17 07:43:21 - [07:43:21.095] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
2025-11-17 07:43:21 - [07:43:21.095] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_inv_transactions_Search","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_Search:638989622010950752"}
[07:43:21.098] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:21 - [07:43:21.098] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:21.101] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
2025-11-17 07:43:21 - [07:43:21.101] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
[07:43:21.149] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (54ms) - Status: 1
2025-11-17 07:43:21 - [07:43:21.149] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (54ms) - Status: 1
2025-11-17 07:43:21 - [07:43:21.149] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"inv_transactions_Search","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":54,"Thread":1,"InputParameters":{"p_UserName":"","p_IsAdmin":"True","p_PartID":"","p_BatchNumber":"","p_FromLocation":"","p_ToLocation":"","p_Operation":"","p_TransactionType":"","p_Quantity":"{}","p_Notes":"","p_ItemType":"","p_FromDate":"2015-11-17 00:00:00","p_ToDate":"2026-11-17 23:59:59","p_SortColumn":"ReceiveDate","p_SortDescending":"True","p_Page":"1","p_PageSize":"50"},"OutputParameters":{"Status":"1","ErrorMsg":"Found 8468 transaction(s) matching criteria"},"ResultData":"DataTable[50 rows]","ErrorMessage":"Found 8468 transaction(s) matching criteria"}
[07:43:21.152] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (54ms) - 50 rows
2025-11-17 07:43:21 - [07:43:21.152] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (54ms) - 50 rows
[07:43:21.155] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (56ms)
2025-11-17 07:43:21 - [07:43:21.155] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (56ms)
[07:43:21.157] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (62ms)
2025-11-17 07:43:21 - [07:43:21.157] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (62ms)
2025-11-17 07:43:21 - [07:43:21.157] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_inv_transactions_Search","ElapsedMs":62,"Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_Search:638989622010950752","Status":"SUCCESS","RowCount":50}
2025-11-17 07:43:21 - [Dao_Transactions.SearchTransactionsAsync] Retrieved 50 transactions from DB
2025-11-17 07:43:21 - [Dao_Transactions.SearchTransactionsAsync] result.StatusMessage: 'Found 8468 transaction(s) matching criteria'
2025-11-17 07:43:21 - [Dao_Transactions.SearchTransactionsAsync] result.RowsAffected: 50
2025-11-17 07:43:21 - [Dao_Transactions.SearchTransactionsAsync] Parsed totalCount from StatusMessage: 8468
2025-11-17 07:43:21 - [Dao_Transactions.SearchTransactionsAsync] Final totalCount: 8468, page: 1, pageSize: 50
2025-11-17 07:43:21 - [Dao_Transactions] SearchTransactionsAsync returned. Success: True, Count: 50
2025-11-17 07:43:21 - [Model_Transactions_ViewModel] DAO returned. Success: True, DataCount: 50
2025-11-17 07:43:21 - [Model_Transactions_ViewModel] SearchResult created:
2025-11-17 07:43:21 -   - TotalRecordCount: 8468
2025-11-17 07:43:21 -   - CurrentPage: 1
2025-11-17 07:43:21 -   - PageSize: 50
2025-11-17 07:43:21 -   - TotalPages: 170
2025-11-17 07:43:21 -   - HasPreviousPage: False
2025-11-17 07:43:21 -   - HasNextPage: True
2025-11-17 07:43:21 -   - Transactions.Count: 50
2025-11-17 07:43:21 - [Transactions] Search completed. Success: True, HasData: True
2025-11-17 07:43:21 - [Transactions] Displaying 50 transactions (Page 1 of 170)
2025-11-17 07:43:21 - [TransactionGridControl] Displaying 50 transactions (Page 1 of 170).
2025-11-17 07:43:21 - [TransactionGridControl] Row selected: Transaction ID 40472
2025-11-17 07:43:21 - [TransactionDetailPanel] Loading transaction details for ID: 40472
2025-11-17 07:43:21 - [TransactionDetailPanel] Transaction details loaded successfully.
2025-11-17 07:43:21 - [Transactions] Row selected: Transaction #40472 (IN)
2025-11-17 07:43:21 - [TransactionGridControl] Row selected: Transaction ID 40472
2025-11-17 07:43:21 - [TransactionDetailPanel] Loading transaction details for ID: 40472
2025-11-17 07:43:21 - [TransactionDetailPanel] Transaction details loaded successfully.
2025-11-17 07:43:21 - [Transactions] Row selected: Transaction #40472 (IN)
2025-11-17 07:43:21 - [TransactionGridControl] Row colors applied to 50 rows.
2025-11-17 07:43:21 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:43:21 - [TransactionGridControl.UpdatePaginationControls] _currentResults:
2025-11-17 07:43:21 -   - TotalRecordCount: 8468
2025-11-17 07:43:21 -   - CurrentPage: 1
2025-11-17 07:43:21 -   - TotalPages: 170
2025-11-17 07:43:21 -   - HasPreviousPage: False
2025-11-17 07:43:21 -   - HasNextPage: True
2025-11-17 07:43:21 - [TransactionGridControl.UpdatePaginationControls] Button states:
2025-11-17 07:43:21 -   - Previous.Enabled: False
2025-11-17 07:43:21 -   - Next.Enabled: True
2025-11-17 07:43:21 - [TransactionGridControl] Export/Print buttons enabled
2025-11-17 07:43:21 - [TransactionGridControl] Results displayed successfully.
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:43:28 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
Running VersionChecker...
2025-11-17 07:43:28 - Running VersionChecker - checking database version information.
[07:43:28.695] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:43:28 - [07:43:28.695] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:43:28 - [07:43:28.695] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989622086956384"}
[07:43:28.699] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:28 - [07:43:28.699] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:28.702] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-17 07:43:28 - [07:43:28.702] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[07:43:28.707] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (12ms) - Status: 1
2025-11-17 07:43:28 - [07:43:28.707] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (12ms) - Status: 1
2025-11-17 07:43:28 - [07:43:28.707] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":33,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[07:43:28.711] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (12ms) - 1 rows
2025-11-17 07:43:28 - [07:43:28.711] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (12ms) - 1 rows
[07:43:28.714] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-17 07:43:28 - [07:43:28.714] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[07:43:28.716] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-17 07:43:28 - [07:43:28.716] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-17 07:43:28 - [07:43:28.716] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989622086956384","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.3.0
2025-11-17 07:43:28 - Version check successful - Database version: 6.2.3.0
2025-11-17 07:43:29 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:29 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:29 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:29 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:29 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
Version labels updated - App: 6.2.1.0, DB: 6.2.3.0
2025-11-17 07:43:29 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-17 07:43:29 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:29 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 7ms
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
2025-11-17 07:43:33 - [SuggestionOverlay] AcceptSelection called: SelectedItem='01-27991-006'
2025-11-17 07:43:33 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - ENTERING
2025-11-17 07:43:33 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:33 - [FocusUtils] Calling SelectAll on TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:33 - [FocusUtils] Queueing BeginInvoke to attach handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:33 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - EXITING
2025-11-17 07:43:33 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:33 - [FocusUtils] AttachTextChangedHandlers: Attaching to SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:33 - [FocusUtils] Attached TextChanged to TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:34 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Checking if should restore normal BackColor
2025-11-17 07:43:34 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Control not focused, restoring normal BackColor
2025-11-17 07:43:34 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:43:34 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-17 07:43:34 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:34 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:34 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:34 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:34 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:34 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-17 07:43:34 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:34 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 4ms
2025-11-17 07:43:35 - [SuggestionOverlay] AcceptSelection called: SelectedItem='ADMININT'
2025-11-17 07:43:35 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:35 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:35 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - ENTERING
2025-11-17 07:43:35 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:35 - [FocusUtils] Calling SelectAll on TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:35 - [FocusUtils] Queueing BeginInvoke to attach handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:35 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - EXITING
2025-11-17 07:43:35 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:35 - [FocusUtils] AttachTextChangedHandlers: Attaching to SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:35 - [FocusUtils] Attached TextChanged to TextBox: SuggestionTextBoxWithLabel_TextBox_Main
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:43:36 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Checking if should restore normal BackColor
2025-11-17 07:43:36 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Control not focused, restoring normal BackColor
2025-11-17 07:43:36 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:43:36 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-17 07:43:36 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:36 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:36 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:36 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:36 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:36 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-17 07:43:36 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:36 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-17 07:43:37 - [SuggestionOverlay] AcceptSelection called: SelectedItem='100'
2025-11-17 07:43:37 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:37 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:37 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - ENTERING
2025-11-17 07:43:37 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:37 - [FocusUtils] Calling SelectAll on TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:37 - [FocusUtils] Queueing BeginInvoke to attach handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:37 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - EXITING
2025-11-17 07:43:37 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:37 - [FocusUtils] AttachTextChangedHandlers: Attaching to SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:37 - [FocusUtils] Attached TextChanged to TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:38 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Checking if should restore normal BackColor
2025-11-17 07:43:38 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Control not focused, restoring normal BackColor
2025-11-17 07:43:38 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:43:38 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-17 07:43:40 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:40 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:40 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:40 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:40 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:40 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-17 07:43:40 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:40 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 5ms
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:43:41 - [SuggestionOverlay] AcceptSelection called: SelectedItem='CS-01'
2025-11-17 07:43:41 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:41 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - ENTERING
2025-11-17 07:43:41 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:41 - [FocusUtils] Calling SelectAll on TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:41 - [FocusUtils] Queueing BeginInvoke to attach handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:41 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - EXITING
2025-11-17 07:43:41 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:41 - [FocusUtils] AttachTextChangedHandlers: Attaching to SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox)
2025-11-17 07:43:41 - [FocusUtils] Attached TextChanged to TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:42 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Checking if should restore normal BackColor
2025-11-17 07:43:42 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Control not focused, restoring normal BackColor
2025-11-17 07:43:42 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:43:42 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-17 07:43:44 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:43:44 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:43:44 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:44 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-17 07:43:44 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:43:44 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-17 07:43:44 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:43:44 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-17 07:43:45 - [SuggestionOverlay] AcceptSelection called: SelectedItem='CS-01'
2025-11-17 07:43:45 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:45 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:43:56 - [TransactionSearchControl] Search button clicked.
2025-11-17 07:43:56 - [TransactionSearchControl] Search criteria validated (Everything mode). Types: IN,OUT,TRANSFER
2025-11-17 07:43:56 - [Transactions] Search requested with criteria: Part: 01-27991-006, User: ADMININT, From: CS-01, To: CS-01, Op: 100, Type: IN,OUT,TRANSFER, From: 11/17/2015, To: 11/17/2026
2025-11-17 07:43:56 - [TransactionDetailPanel] Clearing transaction details.
2025-11-17 07:43:56 - [TransactionDetailPanel] Transaction details cleared successfully.
2025-11-17 07:43:56 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:43:56 - [TransactionGridControl.UpdatePaginationControls] _currentResults is NULL
2025-11-17 07:43:56 - [TransactionGridControl] Export/Print buttons disabled
2025-11-17 07:43:56 - [Model_Transactions_ViewModel] SearchTransactionsAsync starting. User: JOHNK, IsAdmin: True, Page: 1
2025-11-17 07:43:56 - [Model_Transactions_ViewModel] Criteria: Part: 01-27991-006, User: ADMININT, From: CS-01, To: CS-01, Op: 100, Type: IN,OUT,TRANSFER, From: 11/17/2015, To: 11/17/2026
2025-11-17 07:43:56 - [Model_Transactions_ViewModel] Calling DAO SearchAsync...
2025-11-17 07:43:56 - [Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
2025-11-17 07:43:56 - [Dao_Transactions] SearchAsync called. User: JOHNK, IsAdmin: True, Page: 1, PageSize: 50
2025-11-17 07:43:56 - [Dao_Transactions] Criteria: Part: 01-27991-006, User: ADMININT, From: CS-01, To: CS-01, Op: 100, Type: IN,OUT,TRANSFER, From: 11/17/2015, To: 11/17/2026
2025-11-17 07:43:56 - [Dao_Transactions] Criteria.TransactionType raw value: 'IN,OUT,TRANSFER'
2025-11-17 07:43:56 - [Dao_Transactions] Multiple transaction types specified: 'IN,OUT,TRANSFER'. Searching all types.
2025-11-17 07:43:56 - [Dao_Transactions] Calling SearchTransactionsAsync with:
2025-11-17 07:43:56 -   - PartID: '01-27991-006'
2025-11-17 07:43:56 -   - User: 'ADMININT'
2025-11-17 07:43:56 -   - FromLocation: 'CS-01'
2025-11-17 07:43:56 -   - ToLocation: 'CS-01'
2025-11-17 07:43:56 -   - Operation: '100'
2025-11-17 07:43:56 -   - TransactionType: '' (parsed: )
2025-11-17 07:43:56 -   - Notes: ''
2025-11-17 07:43:56 -   - DateFrom: 11/17/2015 12:00:00 AM
2025-11-17 07:43:56 -   - DateTo: 11/17/2026 11:59:59 PM
[07:43:56.608] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
2025-11-17 07:43:56 - [07:43:56.608] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
2025-11-17 07:43:56 - [07:43:56.608] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_inv_transactions_Search","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_Search:638989622366085499"}
[07:43:56.612] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:56 - [07:43:56.612] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:56.615] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
2025-11-17 07:43:56 - [07:43:56.615] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
[07:43:56.641] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (32ms) - Status: 0
2025-11-17 07:43:56 - [07:43:56.641] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (32ms) - Status: 0
2025-11-17 07:43:56 - [07:43:56.641] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"inv_transactions_Search","Caller":"ExecuteDataTableWithStatusAsync","Status":0,"ElapsedMs":32,"Thread":1,"InputParameters":{"p_UserName":"ADMININT","p_IsAdmin":"True","p_PartID":"01-27991-006","p_BatchNumber":"","p_FromLocation":"CS-01","p_ToLocation":"CS-01","p_Operation":"100","p_TransactionType":"","p_Quantity":"{}","p_Notes":"","p_ItemType":"","p_FromDate":"2015-11-17 00:00:00","p_ToDate":"2026-11-17 23:59:59","p_SortColumn":"ReceiveDate","p_SortDescending":"True","p_Page":"1","p_PageSize":"50"},"OutputParameters":{"Status":"0","ErrorMsg":"No transactions found matching search criteria"},"ResultData":"DataTable[0 rows]","ErrorMessage":"No transactions found matching search criteria"}
[07:43:56.644] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (32ms) - 0 rows
2025-11-17 07:43:56 - [07:43:56.644] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (32ms) - 0 rows
[07:43:56.647] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (34ms)
2025-11-17 07:43:56 - [07:43:56.647] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (34ms)
[07:43:56.649] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (40ms)
2025-11-17 07:43:56 - [07:43:56.649] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (40ms)
2025-11-17 07:43:56 - [07:43:56.649] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_inv_transactions_Search","ElapsedMs":40,"Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_Search:638989622366085499","Status":"SUCCESS","RowCount":0}
2025-11-17 07:43:56 - [Dao_Transactions.SearchTransactionsAsync] Retrieved 0 transactions from DB
2025-11-17 07:43:56 - [Dao_Transactions.SearchTransactionsAsync] result.StatusMessage: 'No transactions found matching search criteria'
2025-11-17 07:43:56 - [Dao_Transactions.SearchTransactionsAsync] result.RowsAffected: 0
2025-11-17 07:43:56 - [Dao_Transactions.SearchTransactionsAsync] Using fallback totalCount: 0
2025-11-17 07:43:56 - [Dao_Transactions.SearchTransactionsAsync] Final totalCount: 0, page: 1, pageSize: 50
2025-11-17 07:43:56 - [Dao_Transactions] SearchTransactionsAsync returned. Success: True, Count: 0
2025-11-17 07:43:56 - [Model_Transactions_ViewModel] DAO returned. Success: True, DataCount: 0
2025-11-17 07:43:56 - [Model_Transactions_ViewModel] SearchResult created:
2025-11-17 07:43:56 -   - TotalRecordCount: 0
2025-11-17 07:43:56 -   - CurrentPage: 1
2025-11-17 07:43:56 -   - PageSize: 50
2025-11-17 07:43:56 -   - TotalPages: 0
2025-11-17 07:43:56 -   - HasPreviousPage: False
2025-11-17 07:43:56 -   - HasNextPage: False
2025-11-17 07:43:56 -   - Transactions.Count: 0
2025-11-17 07:43:56 - [Transactions] Search completed. Success: True, HasData: True
2025-11-17 07:43:56 - [Transactions] Displaying 0 transactions (Page 1 of 0)
2025-11-17 07:43:56 - [TransactionGridControl] Displaying 0 transactions (Page 1 of 0).
2025-11-17 07:43:56 - [TransactionGridControl] Row colors applied to 0 rows.
2025-11-17 07:43:56 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:43:56 - [TransactionGridControl.UpdatePaginationControls] _currentResults:
2025-11-17 07:43:56 -   - TotalRecordCount: 0
2025-11-17 07:43:56 -   - CurrentPage: 1
2025-11-17 07:43:56 -   - TotalPages: 0
2025-11-17 07:43:56 -   - HasPreviousPage: False
2025-11-17 07:43:56 -   - HasNextPage: False
2025-11-17 07:43:56 - [TransactionGridControl.UpdatePaginationControls] Button states:
2025-11-17 07:43:56 -   - Previous.Enabled: False
2025-11-17 07:43:56 -   - Next.Enabled: False
2025-11-17 07:43:56 - [TransactionGridControl] Export/Print buttons disabled
2025-11-17 07:43:56 - [TransactionGridControl] Results displayed successfully.
2025-11-17 07:43:58 - [TransactionSearchControl] Reset button clicked.
2025-11-17 07:43:58 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:58 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:58 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:58 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:58 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:58 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:58 - [FocusUtils] TextBox_TextChanged_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Clearing highlight
2025-11-17 07:43:58 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:43:58 - [TransactionSearchControl] Quick filter changed: Month
2025-11-17 07:43:58 - [TransactionSearchControl] Quick filter applied: Month
2025-11-17 07:43:58 - [Transactions] Reset requested, clearing grid results.
2025-11-17 07:43:58 - [TransactionDetailPanel] Clearing transaction details.
2025-11-17 07:43:58 - [TransactionDetailPanel] Transaction details cleared successfully.
2025-11-17 07:43:58 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:43:58 - [TransactionGridControl.UpdatePaginationControls] _currentResults is NULL
2025-11-17 07:43:58 - [TransactionGridControl] Export/Print buttons disabled
2025-11-17 07:43:58 - [TransactionSearchControl] Search criteria reset successfully.
Running VersionChecker...
2025-11-17 07:43:58 - Running VersionChecker - checking database version information.
[07:43:58.694] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:43:58 - [07:43:58.694] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:43:58 - [07:43:58.694] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989622386947774"}
[07:43:58.699] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:43:58 - [07:43:58.699] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:43:58.702] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-17 07:43:58 - [07:43:58.702] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[07:43:58.708] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (13ms) - Status: 1
2025-11-17 07:43:58 - [07:43:58.708] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (13ms) - Status: 1
2025-11-17 07:43:58 - [07:43:58.708] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":13,"Thread":31,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[07:43:58.712] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (13ms) - 1 rows
2025-11-17 07:43:58 - [07:43:58.712] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (13ms) - 1 rows
[07:43:58.714] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
2025-11-17 07:43:58 - [07:43:58.714] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
[07:43:58.717] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (22ms)
2025-11-17 07:43:58 - [07:43:58.717] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (22ms)
2025-11-17 07:43:58 - [07:43:58.717] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":22,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989622386947774","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.3.0
2025-11-17 07:43:58 - Version check successful - Database version: 6.2.3.0
Version labels updated - App: 6.2.1.0, DB: 6.2.3.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 4, TimerActive: False
2025-11-17 07:44:21 - [TransactionSearchControl] Search button clicked.
2025-11-17 07:44:21 - [TransactionSearchControl] Search criteria validated. DateRange: 11/01/25 - 11/30/25, Types: IN,OUT,TRANSFER
2025-11-17 07:44:21 - [Transactions] Search requested with criteria: Type: IN,OUT,TRANSFER, From: 11/01/2025, To: 11/30/2025
2025-11-17 07:44:21 - [TransactionDetailPanel] Clearing transaction details.
2025-11-17 07:44:21 - [TransactionDetailPanel] Transaction details cleared successfully.
2025-11-17 07:44:21 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:44:21 - [TransactionGridControl.UpdatePaginationControls] _currentResults is NULL
2025-11-17 07:44:21 - [TransactionGridControl] Export/Print buttons disabled
2025-11-17 07:44:21 - [Model_Transactions_ViewModel] SearchTransactionsAsync starting. User: JOHNK, IsAdmin: True, Page: 1
2025-11-17 07:44:21 - [Model_Transactions_ViewModel] Criteria: Type: IN,OUT,TRANSFER, From: 11/01/2025, To: 11/30/2025
2025-11-17 07:44:21 - [Model_Transactions_ViewModel] Calling DAO SearchAsync...
2025-11-17 07:44:21 - [Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
[Dao_Transactions.SearchAsync] ===== METHOD ENTRY ===== User: JOHNK, IsAdmin: True, Page: 1
2025-11-17 07:44:21 - [Dao_Transactions] SearchAsync called. User: JOHNK, IsAdmin: True, Page: 1, PageSize: 50
2025-11-17 07:44:21 - [Dao_Transactions] Criteria: Type: IN,OUT,TRANSFER, From: 11/01/2025, To: 11/30/2025
2025-11-17 07:44:21 - [Dao_Transactions] Criteria.TransactionType raw value: 'IN,OUT,TRANSFER'
2025-11-17 07:44:21 - [Dao_Transactions] Multiple transaction types specified: 'IN,OUT,TRANSFER'. Searching all types.
2025-11-17 07:44:21 - [Dao_Transactions] Calling SearchTransactionsAsync with:
2025-11-17 07:44:21 -   - PartID: ''
2025-11-17 07:44:21 -   - User: ''
2025-11-17 07:44:21 -   - FromLocation: ''
2025-11-17 07:44:21 -   - ToLocation: ''
2025-11-17 07:44:21 -   - Operation: ''
2025-11-17 07:44:21 -   - TransactionType: '' (parsed: )
2025-11-17 07:44:21 -   - Notes: ''
2025-11-17 07:44:21 -   - DateFrom: 11/1/2025 12:00:00 AM
2025-11-17 07:44:21 -   - DateTo: 11/30/2025 11:59:59 PM
[07:44:21.976] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
2025-11-17 07:44:21 - [07:44:21.976] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_Search
2025-11-17 07:44:21 - [07:44:21.976] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_inv_transactions_Search","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_Search:638989622619766679"}
[07:44:21.981] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:44:21 - [07:44:21.981] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:44:21.983] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
2025-11-17 07:44:21 - [07:44:21.983] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_Search
[07:44:21.990] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (13ms) - Status: 1
2025-11-17 07:44:21 - [07:44:21.990] [HIGH  ] ✅ PROCEDURE inv_transactions_Search (13ms) - Status: 1
2025-11-17 07:44:21 - [07:44:21.990] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"inv_transactions_Search","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":13,"Thread":1,"InputParameters":{"p_UserName":"","p_IsAdmin":"True","p_PartID":"","p_BatchNumber":"","p_FromLocation":"","p_ToLocation":"","p_Operation":"","p_TransactionType":"","p_Quantity":"{}","p_Notes":"","p_ItemType":"","p_FromDate":"2025-11-01 00:00:00","p_ToDate":"2025-11-30 23:59:59","p_SortColumn":"ReceiveDate","p_SortDescending":"True","p_Page":"1","p_PageSize":"50"},"OutputParameters":{"Status":"1","ErrorMsg":"Found 111 transaction(s) matching criteria"},"ResultData":"DataTable[50 rows]","ErrorMessage":"Found 111 transaction(s) matching criteria"}
[07:44:21.994] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (13ms) - 50 rows
2025-11-17 07:44:21 - [07:44:21.994] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_Search (13ms) - 50 rows
[07:44:21.997] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
2025-11-17 07:44:21 - [07:44:21.997] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
[07:44:21.999] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (22ms)
2025-11-17 07:44:22 - [07:44:21.999] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_Search (22ms)
2025-11-17 07:44:22 - [07:44:21.999] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_inv_transactions_Search","ElapsedMs":22,"Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_Search:638989622619766679","Status":"SUCCESS","RowCount":50}
2025-11-17 07:44:22 - [Dao_Transactions.SearchTransactionsAsync] Retrieved 50 transactions from DB
2025-11-17 07:44:22 - [Dao_Transactions.SearchTransactionsAsync] result.StatusMessage: 'Found 111 transaction(s) matching criteria'
2025-11-17 07:44:22 - [Dao_Transactions.SearchTransactionsAsync] result.RowsAffected: 50
2025-11-17 07:44:22 - [Dao_Transactions.SearchTransactionsAsync] Parsed totalCount from StatusMessage: 111
2025-11-17 07:44:22 - [Dao_Transactions.SearchTransactionsAsync] Final totalCount: 111, page: 1, pageSize: 50
2025-11-17 07:44:22 - [Dao_Transactions] SearchTransactionsAsync returned. Success: True, Count: 50
2025-11-17 07:44:22 - [Model_Transactions_ViewModel] DAO returned. Success: True, DataCount: 50
2025-11-17 07:44:22 - [Model_Transactions_ViewModel] SearchResult created:
2025-11-17 07:44:22 -   - TotalRecordCount: 111
2025-11-17 07:44:22 -   - CurrentPage: 1
2025-11-17 07:44:22 -   - PageSize: 50
2025-11-17 07:44:22 -   - TotalPages: 3
2025-11-17 07:44:22 -   - HasPreviousPage: False
2025-11-17 07:44:22 -   - HasNextPage: True
2025-11-17 07:44:22 -   - Transactions.Count: 50
2025-11-17 07:44:22 - [Transactions] Search completed. Success: True, HasData: True
2025-11-17 07:44:22 - [Transactions] Displaying 50 transactions (Page 1 of 3)
2025-11-17 07:44:22 - [TransactionGridControl] Displaying 50 transactions (Page 1 of 3).
2025-11-17 07:44:22 - [TransactionGridControl] Row selected: Transaction ID 40472
2025-11-17 07:44:22 - [TransactionDetailPanel] Loading transaction details for ID: 40472
2025-11-17 07:44:22 - [TransactionDetailPanel] Transaction details loaded successfully.
2025-11-17 07:44:22 - [Transactions] Row selected: Transaction #40472 (IN)
2025-11-17 07:44:22 - [TransactionGridControl] Row colors applied to 50 rows.
2025-11-17 07:44:22 - [TransactionGridControl.UpdatePaginationControls] Called
2025-11-17 07:44:22 - [TransactionGridControl.UpdatePaginationControls] _currentResults:
2025-11-17 07:44:22 -   - TotalRecordCount: 111
2025-11-17 07:44:22 -   - CurrentPage: 1
2025-11-17 07:44:22 -   - TotalPages: 3
2025-11-17 07:44:22 -   - HasPreviousPage: False
2025-11-17 07:44:22 -   - HasNextPage: True
2025-11-17 07:44:22 - [TransactionGridControl.UpdatePaginationControls] Button states:
2025-11-17 07:44:22 -   - Previous.Enabled: False
2025-11-17 07:44:22 -   - Next.Enabled: True
2025-11-17 07:44:22 - [TransactionGridControl] Export/Print buttons enabled
2025-11-17 07:44:22 - [TransactionGridControl] Results displayed successfully.
2025-11-17 07:44:22 - [TransactionGridControl] Row selected: Transaction ID 40469
2025-11-17 07:44:22 - [TransactionDetailPanel] Loading transaction details for ID: 40469
2025-11-17 07:44:22 - [TransactionDetailPanel] Transaction details loaded successfully.
2025-11-17 07:44:22 - [Transactions] Row selected: Transaction #40469 (IN)
2025-11-17 07:44:23 - [TransactionDetailPanel] Opening lifecycle form for Part: A110146, Batch: 0000021449
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:44:23 - [ThemedForm] TransactionLifecycleForm initialized with automatic theme support
2025-11-17 07:44:23 - [TransactionDetailPanel] Initializing...
2025-11-17 07:44:23 - [TransactionDetailPanel] Initialization complete.
2025-11-17 07:44:23 - [TransactionLifecycleForm] Initializing...
2025-11-17 07:44:23 - [TransactionDetailPanel] Configured for embedded mode - related transactions section hidden.
2025-11-17 07:44:23 - [TransactionLifecycleForm] Initialized for Part: A110146, Batch: 0000021449
2025-11-17 07:44:23 - [TransactionLifecycleForm] Loading lifecycle for batch: 0000021449
2025-11-17 07:44:23 - [Dao_Transactions.GetBatchLifecycleAsync] Retrieving lifecycle for batch: 0000021449
[07:44:23.949] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_GetBatchLifecycle
2025-11-17 07:44:23 - [07:44:23.949] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_transactions_GetBatchLifecycle
2025-11-17 07:44:23 - [07:44:23.949] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_inv_transactions_GetBatchLifecycle","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_GetBatchLifecycle:638989622639498210"}
[07:44:23.953] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:44:23 - [07:44:23.953] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:44:23.955] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_GetBatchLifecycle
2025-11-17 07:44:23 - [07:44:23.955] [MEDIUM] 🗄️ DB PROCEDURE START: inv_transactions_GetBatchLifecycle
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_Panel_TreeView (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_Panel_TreeView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_TreeView_Lifecycle (TreeView) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_TreeView_Lifecycle (TreeView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_Panel_DetailView (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_Panel_DetailView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_DetailPanel (TransactionDetailPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_DetailPanel (TransactionDetailPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Inner (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Inner (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_RelatedHeader - FALSE (Enabled=True, Visible=False)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_RelatedHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_RelatedTitle - FALSE (Enabled=True, Visible=False)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_RelatedTitle (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Button_ViewBatchHistory - FALSE (Enabled=True, Visible=False)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Button_ViewBatchHistory (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:23 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TextBox_Notes (TextBox) - APPLYING
2025-11-17 07:44:24 - [FocusUtils] Apply: Starting for TransactionDetailPanel_TextBox_Notes (TextBox)
2025-11-17 07:44:24 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:44:24 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:44:24 - [FocusUtils] Apply: Attached Click handler for TextBox TransactionDetailPanel_TextBox_Notes
2025-11-17 07:44:24 - [FocusUtils] Apply: Completed for TransactionDetailPanel_TextBox_Notes
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_RelatedStatus - FALSE (Enabled=True, Visible=False)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_RelatedStatus (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_TableLayout_Details (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_TableLayout_Details (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 22 controls
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_IdCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_IdCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_IdValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_IdValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_TypeCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_TypeCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_TypeValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_TypeValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ItemTypeCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ItemTypeCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ItemTypeValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ItemTypeValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_PartCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_PartCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_PartValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_PartValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_BatchCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_BatchCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_BatchValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_BatchValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_QuantityCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_QuantityCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_QuantityValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_QuantityValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_FromCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_FromCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_FromValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_FromValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ToCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ToCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_ToValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_ToValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_OperationCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_OperationCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_OperationValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_OperationValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_UserCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_UserCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_UserValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_UserValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_DateCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_DateCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_DateValue (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_DateValue (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionDetailPanel_Label_NotesCaption (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionDetailPanel_Label_NotesCaption (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_Panel_Buttons (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_Panel_Buttons (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_TableLayout_Buttons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_TableLayout_Buttons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_Button_Export (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_Button_Export (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_Button_Print (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_Button_Close (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_Button_Close (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [FocusUtils] CanControlReceiveFocus: TransactionLifecycleForm_StatusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-17 07:44:24 - [FocusUtils] ApplyFocusEventHandling: TransactionLifecycleForm_StatusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:24 - [ThemedForm] Using FormThemeApplier for TransactionLifecycleForm
2025-11-17 07:44:24 - [FormThemeApplier] Applying to 'TransactionLifecycleForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:44:24 - [FormThemeApplier] Applying to 'TransactionLifecycleForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-17 07:44:24 - [FormThemeApplier] Applying to 'TransactionLifecycleForm_DetailPanel' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:44:24 - [FormThemeApplier] Applying to 'TransactionLifecycleForm_DetailPanel' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'TransactionLifecycleForm' in 7ms
[07:44:24.145] [HIGH  ] ✅ PROCEDURE inv_transactions_GetBatchLifecycle (195ms) - Status: 1
2025-11-17 07:44:24 - [07:44:24.145] [HIGH  ] ✅ PROCEDURE inv_transactions_GetBatchLifecycle (195ms) - Status: 1
2025-11-17 07:44:24 - [07:44:24.145] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"inv_transactions_GetBatchLifecycle","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":195,"Thread":1,"InputParameters":{"p_BatchNumber":"0000021449"},"OutputParameters":{"Status":"1","ErrorMsg":"Found 1 transaction(s) for batch 0000021449"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Found 1 transaction(s) for batch 0000021449"}
[07:44:24.148] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_GetBatchLifecycle (195ms) - 1 rows
2025-11-17 07:44:24 - [07:44:24.148] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_transactions_GetBatchLifecycle (195ms) - 1 rows
[07:44:24.150] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (197ms)
2025-11-17 07:44:24 - [07:44:24.150] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (197ms)
[07:44:24.152] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_GetBatchLifecycle (202ms)
2025-11-17 07:44:24 - [07:44:24.152] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_transactions_GetBatchLifecycle (202ms)
2025-11-17 07:44:24 - [07:44:24.152] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_inv_transactions_GetBatchLifecycle","ElapsedMs":202,"Key":"ExecuteDataTableWithStatusAsync:SP_inv_transactions_GetBatchLifecycle:638989622639498210","Status":"SUCCESS","RowCount":1}
2025-11-17 07:44:24 - [Dao_Transactions.GetBatchLifecycleAsync] Retrieved 1 transactions for batch 0000021449
2025-11-17 07:44:24 - [TransactionLifecycleForm] Retrieved 1 transactions
2025-11-17 07:44:24 - [TransactionLifecycleForm] Node selected: Transaction ID 40469
2025-11-17 07:44:24 - [TransactionDetailPanel] Loading transaction details for ID: 40469
2025-11-17 07:44:24 - [TransactionDetailPanel] Transaction details loaded successfully.
2025-11-17 07:44:24 - [TransactionLifecycleForm] Lifecycle loaded successfully
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:44:26 - [TransactionLifecycleForm] Close button clicked.
2025-11-17 07:44:26 - [TransactionDetailPanel] Lifecycle form closed.
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'TransactionLifecycleForm' unsubscribed from theme changes
Running VersionChecker...
2025-11-17 07:44:28 - Running VersionChecker - checking database version information.
[07:44:28.690] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:44:28 - [07:44:28.690] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-17 07:44:28 - [07:44:28.690] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989622686904672"}
[07:44:28.694] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-17 07:44:28 - [07:44:28.694] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[07:44:28.697] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-17 07:44:28 - [07:44:28.697] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[07:44:28.702] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (11ms) - Status: 1
2025-11-17 07:44:28 - [07:44:28.702] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (11ms) - Status: 1
2025-11-17 07:44:28 - [07:44:28.702] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":26,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[07:44:28.706] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (11ms) - 1 rows
2025-11-17 07:44:28 - [07:44:28.706] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (11ms) - 1 rows
[07:44:28.708] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-17 07:44:28 - [07:44:28.708] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[07:44:28.710] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-17 07:44:28 - [07:44:28.710] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-17 07:44:28 - [07:44:28.710] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638989622686904672","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.3.0
2025-11-17 07:44:28 - Version check successful - Database version: 6.2.3.0
Version labels updated - App: 6.2.1.0, DB: 6.2.3.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:44:31 - [TransactionGridControl] Export button clicked.
2025-11-17 07:44:31 - [Transactions] Export requested.
2025-11-17 07:44:33 - [Transactions] Export cancelled by user.
2025-11-17 07:44:35 - [TransactionGridControl] Print button clicked.
2025-11-17 07:44:35 - [Transactions] Print requested.
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-17 07:44:35 - [ThemedForm] PrintForm initialized with automatic theme support
2025-11-17 07:44:35 - [PrintForm] Constructor: Incoming printJob.CurrentPage=1, TotalPages=1
2025-11-17 07:44:35 - [PrintForm] Constructor: After InitializeComponent, Control.StartPage=0
2025-11-17 07:44:35 - [PrintForm] InitializePreviewSection: Before reset StartPage=0
2025-11-17 07:44:35 - [PrintForm] InitializePreviewSection: After reset StartPage=0
2025-11-17 07:44:35 - [PrintForm] Constructor complete: CurrentPage=1, Control.StartPage=0
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_Main (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_Main (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_Master (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_Master (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_Sidebar (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_Sidebar (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_Sidebar (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_Sidebar (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 6 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_PageSettingsSection (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_PageSettingsSection (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayoutPanel_PageSettingsHeader (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayoutPanel_PageSettingsHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_PageSettingsHeader (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_PageSettingsHeader (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_PageSettingsToggle (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_PageSettingsToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_PageSettingsContent (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_PageSettingsContent (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_PageSettingsContent (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_PageSettingsContent (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_PageRange (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_PageRange (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_AllPages (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_AllPages (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_CurrentPage (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_CurrentPage (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_CustomRange (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_CustomRange (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_CustomPageRange (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_CustomPageRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_FromPage (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_FromPage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TextBox_FromPage - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TextBox_FromPage (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_ToPage (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_ToPage (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TextBox_ToPage - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TextBox_ToPage (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_ActionButtons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_ActionButtons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_Export - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_Export (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_Cancel (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_Cancel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_OptionsSection (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_OptionsSection (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayoutPanel_OptionsHeader (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayoutPanel_OptionsHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_OptionsToggle (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_OptionsToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_OptionsHeader (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_OptionsHeader (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_OptionsContent (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_OptionsContent (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_OptionsContent (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_OptionsContent (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayoutPanel_ColorMode (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayoutPanel_ColorMode (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_Color (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_Color (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_Grayscale (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_Grayscale (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_ColorMode (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_ColorMode (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_Zoom (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_Zoom (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_ComboBox_Zoom (ComboBox) - APPLYING
2025-11-17 07:44:35 - [FocusUtils] Apply: Starting for PrintForm_ComboBox_Zoom (ComboBox)
2025-11-17 07:44:35 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for PrintForm_ComboBox_Zoom
2025-11-17 07:44:35 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for PrintForm_ComboBox_Zoom
2025-11-17 07:44:35 - [FocusUtils] Apply: Attached DropDown handler for ComboBox PrintForm_ComboBox_Zoom
2025-11-17 07:44:35 - [FocusUtils] Apply: Completed for PrintForm_ComboBox_Zoom
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_ColumnSettingsSection (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_ColumnSettingsSection (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayoutPanel_ColumnSettingsHeader (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayoutPanel_ColumnSettingsHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_ColumnSettingsToggle (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_ColumnSettingsToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_ColumnSettingsHeader (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_ColumnSettingsHeader (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_ColumnSettingsContent (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_ColumnSettingsContent (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_ColumnSettingsContent (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_ColumnSettingsContent (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayoutPanel_ColumnButtons (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayoutPanel_ColumnButtons (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_ColumnUp - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_ColumnUp (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_ColumnDown (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_ColumnDown (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_Columns (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_Columns (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_CheckedListBox_Columns (CheckedListBox) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_CheckedListBox_Columns (CheckedListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_PrinterSettingsSection (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_PrinterSettingsSection (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayoutPanel_PrinterSettingsHeader (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayoutPanel_PrinterSettingsHeader (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_PrinterSettingsHeader (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_PrinterSettingsHeader (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_PrinterSettingsToggle (Button) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_PrinterSettingsToggle (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_PrinterSettingsContent (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_PrinterSettingsContent (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_PrinterSettingsContent (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_PrinterSettingsContent (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_PrinterName (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_PrinterName (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_ComboBox_Printer (ComboBox) - APPLYING
2025-11-17 07:44:35 - [FocusUtils] Apply: Starting for PrintForm_ComboBox_Printer (ComboBox)
2025-11-17 07:44:35 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] Apply: Attached DropDown handler for ComboBox PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] Apply: Completed for PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_PrinterOrientation (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_PrinterOrientation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel1 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel1 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_Portrait (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_Portrait (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_RadioButton_Landscape (RadioButton) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_RadioButton_Landscape (RadioButton) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_SidebarSpacer (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_SidebarSpacer (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Panel_PreviewViewport (Panel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Panel_PreviewViewport (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_PreviewArea (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_PreviewArea (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_PrintPreviewControl (PrintPreviewControl) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_PrintPreviewControl (PrintPreviewControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling:  (HorizontalScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling:  (VerticalScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_TableLayout_PreviewNavigation (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_TableLayout_PreviewNavigation (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_FirstPage - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_FirstPage (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_PreviousPage - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_PreviousPage (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Label_PageCounter (Label) - FALSE (control type cannot receive focus)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Label_PageCounter (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_NextPage - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_NextPage (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] CanControlReceiveFocus: PrintForm_Button_LastPage - FALSE (Enabled=False, Visible=True)
2025-11-17 07:44:35 - [FocusUtils] ApplyFocusEventHandling: PrintForm_Button_LastPage (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-17 07:44:35 - [FocusUtils] Control_GotFocus_Handler: PrintForm_ComboBox_Printer (ComboBox) - ENTERING
2025-11-17 07:44:35 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] Queueing BeginInvoke to attach handlers for PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] Control_GotFocus_Handler: PrintForm_ComboBox_Printer - EXITING
2025-11-17 07:44:35 - [ThemedForm] Using FormThemeApplier for PrintForm
2025-11-17 07:44:35 - [FormThemeApplier] Applying to 'PrintForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-17 07:44:35 - [FormThemeApplier] Applying to 'PrintForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'PrintForm' in 98ms
2025-11-17 07:44:35 - [PrintForm] GeneratePreviewAsync ENTRY: CurrentPage=1, TotalPages=1, PageRangeType=AllPages
2025-11-17 07:44:35 - [PrintForm] After reset: CurrentPage=1
2025-11-17 07:44:35 - [PrintForm] Captured original values: Range=AllPages, From=1, To=1, Current=1
2025-11-17 07:44:35 - [Helper_PrintManager] Print manager initialized
2025-11-17 07:44:35 - [Helper_PrintManager] Preparing print document...
2025-11-17 07:44:35 - [Core_TablePrinter] Data prepared: RangeType=AllPages, FirstPage=1, StartRow=0, EndRowExclusive=50, VisibleColumns=8, TotalRows=50, ExistingTotalPages=1
2025-11-17 07:44:35 - [Helper_PrintManager] Print document prepared: 50 rows, Printer: Microsoft Print to PDF
2025-11-17 07:44:35 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [FocusUtils] AttachTextChangedHandlers: Attaching to PrintForm_ComboBox_Printer (ComboBox)
2025-11-17 07:44:35 - [FocusUtils] Attached TextChanged to ComboBox: PrintForm_ComboBox_Printer
2025-11-17 07:44:35 - [Core_TablePrinter] BeginPrint: Reset to StartRow=0, PageNumber will start at 1
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-17 07:44:36 - [Core_TablePrinter] PrintPage first page rendered: Page=1, StartRow=0, EndRow=32, HasMore=True
2025-11-17 07:44:36 - [Core_TablePrinter] Printing complete: 2 page(s), 50 rows printed
2025-11-17 07:44:36 - [PrintForm] Preview setup: TotalPages=2, TargetIndex=0, OriginalCurrentPage=1
2025-11-17 07:44:36 - [PrintForm] Before setting Document: Control.StartPage=0
2025-11-17 07:44:36 - [PrintForm] After setting Document and StartPage=0: Control.StartPage=0
2025-11-17 07:44:36 - [PrintForm] Final CurrentPage set to: 1, StartPage=0
2025-11-17 07:44:36 - [PrintForm] UpdatePreviewNavigationState: StartPage=0, CurrentIndex=0, DisplayPage=1, TotalPages=2
2025-11-17 07:44:36 - [PrintForm] UpdatePreviewNavigationState: Set CurrentPage to 1
2025-11-17 07:44:36 - [Core_TablePrinter] BeginPrint: Reset to StartRow=0, PageNumber will start at 1
2025-11-17 07:44:36 - [Core_TablePrinter] PrintPage first page rendered: Page=1, StartRow=0, EndRow=32, HasMore=True
2025-11-17 07:44:36 - [Core_TablePrinter] Printing complete: 2 page(s), 50 rows printed
2025-11-17 07:44:36 - [FocusUtils] Control_LostFocus_Handler: PrintForm_ComboBox_Printer - Checking if should restore normal BackColor
2025-11-17 07:44:36 - [FocusUtils] Control_LostFocus_Handler: PrintForm_ComboBox_Printer - Control not focused, restoring normal BackColor
2025-11-17 07:44:36 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for PrintForm_ComboBox_Printer
2025-11-17 07:44:36 - [FocusUtils] Control_GotFocus_Handler: PrintForm_ComboBox_Printer (ComboBox) - ENTERING
2025-11-17 07:44:36 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for PrintForm_ComboBox_Printer
2025-11-17 07:44:36 - [FocusUtils] Queueing BeginInvoke to attach handlers for PrintForm_ComboBox_Printer
2025-11-17 07:44:36 - [FocusUtils] Control_GotFocus_Handler: PrintForm_ComboBox_Printer - EXITING
2025-11-17 07:44:36 - [FocusUtils] BeginInvoke executing: Handlers already attached for PrintForm_ComboBox_Printer
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
2025-11-17 07:44:52 - [Helper_PrintManager] Print manager disposed
2025-11-17 07:44:52 - [FocusUtils] Control_LostFocus_Handler: PrintForm_ComboBox_Printer - Checking if should restore normal BackColor
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'PrintForm' unsubscribed from theme changes
2025-11-17 07:44:52 - [Transactions] Print dialog closed with result: Cancel
2025-11-17 07:44:55 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main (SuggestionTextBox) - ENTERING
2025-11-17 07:44:55 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:44:55 - [FocusUtils] Calling SelectAll on TextBox: SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:44:55 - [FocusUtils] Queueing BeginInvoke to attach handlers for SuggestionTextBoxWithLabel_TextBox_Main
2025-11-17 07:44:55 - [FocusUtils] Control_GotFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - EXITING
2025-11-17 07:44:55 - [FocusUtils] BeginInvoke executing: Handlers already attached for SuggestionTextBoxWithLabel_TextBox_Main
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'MainForm' unsubscribed from theme changes
2025-11-17 07:44:57 - [FocusUtils] Control_LostFocus_Handler: SuggestionTextBoxWithLabel_TextBox_Main - Checking if should restore normal BackColor
2025-11-17 07:44:57 - [Cleanup] Starting application cleanup
2025-11-17 07:44:57 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-17 07:44:57 - [Cleanup] Memory cleanup completed
2025-11-17 07:44:57 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-17 07:44:57 - [Startup] Application shutdown completed
2025-11-17 07:44:57 - [Cleanup] Starting application cleanup
2025-11-17 07:44:57 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-17 07:44:57 - [Cleanup] Memory cleanup completed
2025-11-17 07:44:57 - [Cleanup] Application cleanup completed successfully
2025-11-17 07:44:57 - [Cleanup] Starting application cleanup
2025-11-17 07:44:57 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-17 07:44:57 - [Cleanup] Memory cleanup completed
2025-11-17 07:44:57 - [Cleanup] Application cleanup completed successfully
The program '[25128] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
