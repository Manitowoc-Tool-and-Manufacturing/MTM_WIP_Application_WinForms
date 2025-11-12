------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[23:15:55.834] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-11 23:15:55 - [23:15:55.834] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[23:15:55.877] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-11 23:15:55 - [23:15:55.877] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[23:15:55.879] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-11 23:15:55 - [23:15:55.879] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[23:15:55.882] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-11 23:15:55 - [23:15:55.882] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-11 23:15:55 - [Startup] Application initialization started
2025-11-11 23:15:55 - [Startup] User identified: JOHNK
2025-11-11 23:15:55 - [Dao_System] Checking database connectivity
[23:15:55.921] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-11 23:15:55 - [23:15:55.921] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-11 23:15:55 - [23:15:55.921] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638984997559211012"}
[23:15:55.990] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:55 - [23:15:55.990] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:55.992] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-11 23:15:55 - [23:15:55.992] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[23:15:56.209] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (287ms) - Status: 1
2025-11-11 23:15:56 - [23:15:56.209] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (287ms) - Status: 1
2025-11-11 23:15:56 - [23:15:56.209] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":287,"Thread":11,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[23:15:56.223] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (287ms) - 9 rows
2025-11-11 23:15:56 - [23:15:56.223] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (287ms) - 9 rows
[23:15:56.227] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (236ms)
2025-11-11 23:15:56 - [23:15:56.227] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (236ms)
[23:15:56.230] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (308ms)
2025-11-11 23:15:56 - [23:15:56.230] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (308ms)
2025-11-11 23:15:56 - [23:15:56.230] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":308,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638984997559211012","Status":"SUCCESS","RowCount":9}
2025-11-11 23:15:56 - [Dao_System] Database connectivity check passed
2025-11-11 23:15:56 - [Startup] Database connectivity validated successfully
2025-11-11 23:15:56 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-11 23:15:56 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-11 23:15:56 - [Startup] Parameter cache populated: 116 procedures, 519 total parameters
2025-11-11 23:15:56 - [Startup] Parameter prefix cache initialized successfully in 16ms. Cached 116 stored procedures.
[Startup] Parameter cache: 116 procedures cached in 16ms
[23:15:56.261] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-11 23:15:56 - [23:15:56.261] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-11 23:15:56 - [23:15:56.261] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638984997562611581"}
[23:15:56.264] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:56 - [23:15:56.264] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:56.266] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-11 23:15:56 - [23:15:56.266] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-11 23:15:56 - [Startup] Initializing dependency injection container
2025-11-11 23:15:56 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
2025-11-11 23:15:56 - [Startup] Dependency injection container configured successfully
2025-11-11 23:15:56 - [Startup] Dependency injection container initialized successfully
2025-11-11 23:15:56 - [Splash] Initializing splash screen
[23:15:56.312] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (51ms) - Status: 1
2025-11-11 23:15:56 - [23:15:56.312] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (51ms) - Status: 1
2025-11-11 23:15:56 - [23:15:56.312] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":51,"Thread":10,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user access type(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user access type(s)"}
[23:15:56.316] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (51ms) - 88 rows
2025-11-11 23:15:56 - [23:15:56.316] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (51ms) - 88 rows
[23:15:56.318] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (53ms)
2025-11-11 23:15:56 - [23:15:56.318] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (53ms)
[23:15:56.320] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (59ms)
2025-11-11 23:15:56 - [23:15:56.320] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (59ms)
2025-11-11 23:15:56 - [23:15:56.320] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_GetUserAccessType","ElapsedMs":59,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638984997562611581","Status":"SUCCESS","RowCount":88}
2025-11-11 23:15:56 - System_UserAccessType executed successfully for user: JOHNK
[23:15:56.345] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-11 23:15:56 - [23:15:56.345] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[23:15:56.349] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-11 23:15:56 - [23:15:56.349] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-11 23:15:56 - [ThemedUserControl] Control_ProgressBarUserControl initialized with automatic theme support
[23:15:56.420] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-11 23:15:56 - [23:15:56.420] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[23:15:56.453] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-11 23:15:56 - [23:15:56.453] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[23:15:56.455] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-11 23:15:56 - [23:15:56.455] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[23:15:56.457] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-11 23:15:56 - [23:15:56.457] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[23:15:56.459] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (113ms)
2025-11-11 23:15:56 - [23:15:56.459] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (113ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-11 23:15:56 - [ThemedUserControl] Using applier for _progressControl
2025-11-11 23:15:56 - [FormThemeApplier] Applying to '_progressControl' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:15:56 - [FormThemeApplier] Applying to '_progressControl' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:15:56 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-11-2025 @ 11-15 PM_normal.csv
2025-11-11 23:15:56 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-11 23:15:56 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-11 23:15:56 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-11 23:15:56 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-11 23:15:56 - [Splash] Starting async database connectivity verification
2025-11-11 23:15:56 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-11 23:15:56 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[23:15:56.906] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-11 23:15:56 - [23:15:56.906] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-11 23:15:56 - [23:15:56.906] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638984997569063134"}
[23:15:56.909] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:56 - [23:15:56.909] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:56.911] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-11 23:15:56 - [23:15:56.911] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[23:15:56.972] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (66ms) - Status: 1
2025-11-11 23:15:56 - [23:15:56.972] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (66ms) - Status: 1
2025-11-11 23:15:56 - [23:15:56.972] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":66,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[23:15:56.975] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (66ms) - 3745 rows
2025-11-11 23:15:56 - [23:15:56.975] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (66ms) - 3745 rows
[23:15:56.977] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (68ms)
2025-11-11 23:15:56 - [23:15:56.977] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (68ms)
[23:15:56.979] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (73ms)
2025-11-11 23:15:56 - [23:15:56.979] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (73ms)
2025-11-11 23:15:56 - [23:15:56.979] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":73,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638984997569063134","Status":"SUCCESS","RowCount":3745}
2025-11-11 23:15:56 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-11 23:15:56 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), ItemType(String), Operations(String)
2025-11-11 23:15:56 - [DataTable] ComboBoxPart: Target schema:
2025-11-11 23:15:56 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[23:15:57.006] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-11 23:15:57 - [23:15:57.006] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-11 23:15:57 - [23:15:57.006] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638984997570069320"}
[23:15:57.010] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.010] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.012] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-11 23:15:57 - [23:15:57.012] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[23:15:57.044] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (37ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.044] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (37ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.044] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[23:15:57.048] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (37ms) - 72 rows
2025-11-11 23:15:57 - [23:15:57.048] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (37ms) - 72 rows
[23:15:57.050] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-11 23:15:57 - [23:15:57.050] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[23:15:57.052] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (46ms)
2025-11-11 23:15:57 - [23:15:57.052] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (46ms)
2025-11-11 23:15:57 - [23:15:57.052] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":46,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638984997570069320","Status":"SUCCESS","RowCount":72}
2025-11-11 23:15:57 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-11 23:15:57 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-11 23:15:57 - [DataTable] ComboBoxOperation: Target schema:
2025-11-11 23:15:57 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[23:15:57.062] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-11 23:15:57 - [23:15:57.062] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-11 23:15:57 - [23:15:57.062] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638984997570620321"}
[23:15:57.065] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.065] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.067] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-11 23:15:57 - [23:15:57.067] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[23:15:57.148] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (86ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.148] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (86ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.148] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":86,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[23:15:57.153] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (86ms) - 10371 rows
2025-11-11 23:15:57 - [23:15:57.153] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (86ms) - 10371 rows
[23:15:57.155] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (90ms)
2025-11-11 23:15:57 - [23:15:57.155] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (90ms)
[23:15:57.157] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (95ms)
2025-11-11 23:15:57 - [23:15:57.157] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (95ms)
2025-11-11 23:15:57 - [23:15:57.157] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":95,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638984997570620321","Status":"SUCCESS","RowCount":10371}
2025-11-11 23:15:57 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-11 23:15:57 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-11 23:15:57 - [DataTable] ComboBoxLocation: Target schema:
2025-11-11 23:15:57 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[23:15:57.178] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-11 23:15:57 - [23:15:57.178] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-11 23:15:57 - [23:15:57.178] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638984997571785804"}
[23:15:57.181] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.181] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.183] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-11 23:15:57 - [23:15:57.183] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[23:15:57.215] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (36ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.215] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (36ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.215] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[23:15:57.218] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (36ms) - 88 rows
2025-11-11 23:15:57 - [23:15:57.218] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (36ms) - 88 rows
[23:15:57.221] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-11 23:15:57 - [23:15:57.221] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[23:15:57.222] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (44ms)
2025-11-11 23:15:57 - [23:15:57.222] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (44ms)
2025-11-11 23:15:57 - [23:15:57.222] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638984997571785804","Status":"SUCCESS","RowCount":88}
2025-11-11 23:15:57 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-11 23:15:57 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-11 23:15:57 - [DataTable] ComboBoxUser: Target schema:
2025-11-11 23:15:57 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[23:15:57.232] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-11 23:15:57 - [23:15:57.232] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-11 23:15:57 - [23:15:57.232] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638984997572320658"}
[23:15:57.235] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.235] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.237] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-11 23:15:57 - [23:15:57.237] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[23:15:57.269] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.269] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.269] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 4 item type(s)"},"ResultData":"DataTable[4 rows]","ErrorMessage":"Retrieved 4 item type(s)"}
[23:15:57.272] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
2025-11-11 23:15:57 - [23:15:57.272] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
[23:15:57.274] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-11 23:15:57 - [23:15:57.274] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[23:15:57.276] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-11 23:15:57 - [23:15:57.276] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-11 23:15:57 - [23:15:57.276] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_item_types_Get_All","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638984997572320658","Status":"SUCCESS","RowCount":4}
2025-11-11 23:15:57 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-11 23:15:57 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-11 23:15:57 - [DataTable] ComboBoxItemType: Target schema:
2025-11-11 23:15:57 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-11 23:15:57 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-11 23:15:57 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-11 23:15:57 - Running VersionChecker - checking database version information.
[23:15:57.349] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-11 23:15:57 - [23:15:57.349] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-11 23:15:57 - [23:15:57.349] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638984997573496986"}
[23:15:57.353] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.353] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.355] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-11 23:15:57 - [23:15:57.355] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-11 23:15:57 - [Splash] Version checker initialized
[23:15:57.389] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (39ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.389] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (39ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.389] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":39,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[23:15:57.392] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (39ms) - 1 rows
2025-11-11 23:15:57 - [23:15:57.392] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (39ms) - 1 rows
[23:15:57.394] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
2025-11-11 23:15:57 - [23:15:57.394] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
[23:15:57.396] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (46ms)
2025-11-11 23:15:57 - [23:15:57.396] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (46ms)
2025-11-11 23:15:57 - [23:15:57.396] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":46,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638984997573496986","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.0.0.0
2025-11-11 23:15:57 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.1.0, DB: 6.0.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-11 23:15:57 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[23:15:57.422] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-11 23:15:57 - [23:15:57.422] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-11 23:15:57 - [23:15:57.422] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638984997574224759"}
[23:15:57.425] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.425] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.427] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-11 23:15:57 - [23:15:57.427] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[23:15:57.433] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (11ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.433] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (11ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.433] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[23:15:57.436] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (11ms) - 9 rows
2025-11-11 23:15:57 - [23:15:57.436] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (11ms) - 9 rows
[23:15:57.438] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-11 23:15:57 - [23:15:57.438] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[23:15:57.440] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-11 23:15:57 - [23:15:57.440] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-11 23:15:57 - [23:15:57.440] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638984997574224759","Status":"SUCCESS","RowCount":9}
2025-11-11 23:15:57 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-11 23:15:57 - Successfully loaded 9 themes from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Default' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Forest' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-11 23:15:57 - [DEBUG] Urban Bloom JSON preview: {"InfoColor": "#8E44AD", "ErrorColor": "#F44336", "AccentColor": "#8E44AD", "SuccessColor": "#BA68C8", "WarningColor": "#FF9800", "FormBackColor": "#F6F0FA", "FormForeColor": "#1A1A1A", "LabelBackColor": "#F6F0FA", "LabelForeColor": "#1A1A1A", "PanelBackColor": "#F6F0FA", "PanelForeColor": "#1A1A1A", "ButtonBackColor": "#EDE3F7", "ButtonForeColor": "#1A1A1A", "ControlBackColor": "#F6F0FA", "ControlForeColor": "#1A1A1A", "ListBoxBackColor": "#FFFFFF", "ListBoxForeColor": "#1A1A1A", "PanelBorderCo
2025-11-11 23:15:57 - [DEBUG] Urban Bloom deserialized - FormBackColor: Color [A=255, R=246, G=240, B=250], FormForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-11 23:15:57 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-11 23:15:57 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-11 23:15:57 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[23:15:57.490] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-11 23:15:57 - [23:15:57.490] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[23:15:57.493] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-11 23:15:57 - [23:15:57.493] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[23:15:57.495] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.495] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.495] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997574954050"}
[23:15:57.498] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.498] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.500] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.500] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[23:15:57.532] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (37ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.532] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (37ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.532] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[23:15:57.536] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (37ms) - 1 rows
2025-11-11 23:15:57 - [23:15:57.536] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (37ms) - 1 rows
[23:15:57.538] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (40ms)
2025-11-11 23:15:57 - [23:15:57.538] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (40ms)
[23:15:57.540] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (45ms)
2025-11-11 23:15:57 - [23:15:57.540] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (45ms)
2025-11-11 23:15:57 - [23:15:57.540] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":45,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997574954050","Status":"SUCCESS","RowCount":1}
[23:15:57.550] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (56ms)
2025-11-11 23:15:57 - [23:15:57.550] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (56ms)
[23:15:57.552] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (61ms)
2025-11-11 23:15:57 - [23:15:57.552] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (61ms)
2025-11-11 23:15:57 - Loaded theme preference for user JOHNK: Urban Bloom
2025-11-11 23:15:57 - Set Model_Application_Variables.ThemeName to: Urban Bloom
2025-11-11 23:15:57 - Theme system initialized for user JOHNK. Final theme: Urban Bloom, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
2025-11-11 23:15:57 - ThemeStore loaded 9 themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-11 23:15:57 - [Splash] ThemeStore loaded from database
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-11 23:15:57 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-11 23:15:57 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-11 23:15:57 - [Splash] Loading theme settings
[23:15:57.686] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-11 23:15:57 - [23:15:57.686] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[23:15:57.688] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-11 23:15:57 - [23:15:57.688] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[23:15:57.691] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.691] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.691] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997576911933"}
[23:15:57.694] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.694] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.696] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.696] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[23:15:57.700] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.700] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.700] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[23:15:57.703] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-11 23:15:57 - [23:15:57.703] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[23:15:57.705] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-11 23:15:57 - [23:15:57.705] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[23:15:57.707] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-11 23:15:57 - [23:15:57.707] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-11 23:15:57 - [23:15:57.707] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997576911933","Status":"SUCCESS","RowCount":1}
[23:15:57.711] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-11 23:15:57 - [23:15:57.711] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[23:15:57.713] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (27ms)
2025-11-11 23:15:57 - [23:15:57.713] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (27ms)
[23:15:57.716] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-11 23:15:57 - [23:15:57.716] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[23:15:57.718] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-11 23:15:57 - [23:15:57.718] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[23:15:57.720] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.720] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.720] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997577204300"}
[23:15:57.723] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.723] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.725] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.725] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[23:15:57.729] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (8ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.729] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (8ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.729] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":8,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[23:15:57.732] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (8ms) - 1 rows
2025-11-11 23:15:57 - [23:15:57.732] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (8ms) - 1 rows
[23:15:57.734] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-11 23:15:57 - [23:15:57.734] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[23:15:57.736] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-11 23:15:57 - [23:15:57.736] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-11 23:15:57 - [23:15:57.736] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997577204300","Status":"SUCCESS","RowCount":1}
[23:15:57.742] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
2025-11-11 23:15:57 - [23:15:57.742] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
[23:15:57.744] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (27ms)
2025-11-11 23:15:57 - [23:15:57.744] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (27ms)
[23:15:57.747] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-11 23:15:57 - [23:15:57.747] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[23:15:57.748] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-11 23:15:57 - [23:15:57.748] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[23:15:57.750] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.750] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.750] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997577508422"}
[23:15:57.754] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:57 - [23:15:57.754] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:57.756] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-11 23:15:57 - [23:15:57.756] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[23:15:57.761] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.761] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-11 23:15:57 - [23:15:57.761] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[23:15:57.763] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-11 23:15:57 - [23:15:57.763] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[23:15:57.765] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-11 23:15:57 - [23:15:57.765] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[23:15:57.768] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-11 23:15:57 - [23:15:57.768] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-11 23:15:57 - [23:15:57.768] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638984997577508422","Status":"SUCCESS","RowCount":1}
[23:15:57.771] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-11 23:15:57 - [23:15:57.771] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[23:15:57.773] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (26ms)
2025-11-11 23:15:57 - [23:15:57.773] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (26ms)
2025-11-11 23:15:57 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-11 23:15:57 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-11 23:15:57 - [Splash] Core startup sequence completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-11 23:15:58 - [ThemedForm] MainForm initialized with automatic theme support
[23:15:58.166] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-11 23:15:58 - [23:15:58.166] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[23:15:58.169] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-11 23:15:58 - [23:15:58.169] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-11 23:15:58 - [ThemedUserControl] Control_ConnectionStrengthControl initialized with automatic theme support
2025-11-11 23:15:58 - [ThemedUserControl] Control_InventoryTab initialized with automatic theme support
[23:15:58.197] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.197] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[23:15:58.198] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.198] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[23:15:58.209] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.209] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[23:15:58.213] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.213] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[23:15:58.215] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.215] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[23:15:58.217] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.217] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[23:15:58.220] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.220] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[23:15:58.232] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.232] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-11 23:15:58 - Inventory tab events wired up.
[23:15:58.235] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.235] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[23:15:58.238] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.238] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-11 23:15:58 - Inventory Quantity TextBox changed.
[23:15:58.248] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.248] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[23:15:58.250] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.250] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[23:15:58.252] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (55ms)
2025-11-11 23:15:58 - [23:15:58.252] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (55ms)
[23:15:58.255] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-11 23:15:58 - [23:15:58.255] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[23:15:58.257] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-11 23:15:58 - [23:15:58.257] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-11 23:15:58 - Control_AdvancedInventory constructor entered.
[23:15:58.269] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-11 23:15:58 - [23:15:58.269] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[23:15:58.272] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-11 23:15:58 - [23:15:58.272] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-11 23:15:58 - Control_AdvancedInventory constructor exited.
[23:15:58.276] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.276] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[23:15:58.278] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.278] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[23:15:58.287] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.287] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[23:15:58.289] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.289] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[23:15:58.291] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.291] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[23:15:58.293] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.293] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[23:15:58.303] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.303] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[23:15:58.305] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.305] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[23:15:58.307] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.307] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[23:15:58.309] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-11 23:15:58 - [23:15:58.309] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[23:15:58.311] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (35ms)
2025-11-11 23:15:58 - [23:15:58.311] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (35ms)
[23:15:58.314] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-11 23:15:58 - [23:15:58.314] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[23:15:58.316] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-11 23:15:58 - [23:15:58.316] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[23:15:58.326] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-11 23:15:58 - [23:15:58.326] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[23:15:58.328] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-11 23:15:58 - [23:15:58.328] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[23:15:58.331] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-11 23:15:58 - [23:15:58.331] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[23:15:58.335] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-11 23:15:58 - [23:15:58.335] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[23:15:58.337] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-11 23:15:58 - [23:15:58.337] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-11 23:15:58 - Transfer tab events wired up.
[23:15:58.346] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-11 23:15:58 - [23:15:58.346] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-11 23:15:58 - [ThemedUserControl] Control_QuickButtons initialized with automatic theme support
[23:15:58.350] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-11 23:15:58 - [23:15:58.350] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[23:15:58.352] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-11 23:15:58 - [23:15:58.352] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[23:15:58.356] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-11 23:15:58 - [23:15:58.356] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[23:15:58.386] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-11 23:15:58 - [23:15:58.386] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-11 23:15:58 - Inventory Part ComboBox selection changed.
2025-11-11 23:15:58 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|25_1
[23:15:58.471] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-11 23:15:58 - [23:15:58.471] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[23:15:58.475] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-11 23:15:58 - [23:15:58.475] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[23:15:58.477] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-11 23:15:58 - [23:15:58.477] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[23:15:58.481] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-11 23:15:58 - [23:15:58.481] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[23:15:58.484] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (7ms)
2025-11-11 23:15:58 - [23:15:58.484] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (7ms)
[23:15:58.488] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-11 23:15:58 - [23:15:58.488] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[23:15:58.490] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-11 23:15:58 - [23:15:58.490] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[23:15:58.492] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (17ms)
2025-11-11 23:15:58 - [23:15:58.492] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (17ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[23:15:58.497] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-11 23:15:58 - [23:15:58.497] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[23:15:58.500] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-11 23:15:58 - [23:15:58.500] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[23:15:58.503] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-11 23:15:58 - [23:15:58.503] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[23:15:58.505] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-11 23:15:58 - [23:15:58.505] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[23:15:58.509] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-11 23:15:58 - [23:15:58.509] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[23:15:58.512] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-11 23:15:58 - [23:15:58.512] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-11 23:15:58 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[23:15:58.516] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-11 23:15:58 - [23:15:58.516] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[23:15:58.518] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (14ms)
2025-11-11 23:15:58 - [23:15:58.518] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (14ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[23:15:58.521] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-11 23:15:58 - [23:15:58.521] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[23:15:58.524] [MEDIUM] ⬅️ EXITING MainForm.MainForm (357ms)
2025-11-11 23:15:58 - [23:15:58.524] [MEDIUM] ⬅️ EXITING MainForm.MainForm (357ms)
2025-11-11 23:15:58 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-11 23:15:58 - Remove tab ComboBoxes loaded.
2025-11-11 23:15:58 - Removal tab events wired up.
2025-11-11 23:15:58 - Initial setup of ComboBoxes in the Remove Tab.
[23:15:58.534] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-11 23:15:58 - [23:15:58.534] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[23:15:58.536] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-11 23:15:58 - [23:15:58.536] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-11 23:15:58 - [23:15:58.536] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638984997585363123"}
[23:15:58.540] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:58 - [23:15:58.540] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:58.542] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-11 23:15:58 - [23:15:58.542] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[23:15:58.547] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.547] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-11 23:15:58 - Inventory Op ComboBox selection changed.
[23:15:58.551] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-11 23:15:58 - [23:15:58.551] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[23:15:58.567] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-11 23:15:58 - [23:15:58.567] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-11 23:15:58 - Inventory Location ComboBox selection changed.
[23:15:58.570] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-11 23:15:58 - [23:15:58.570] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-11 23:15:58 - Inventory Op ComboBox selection changed.
2025-11-11 23:15:58 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|25_1
[23:15:58.604] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (67ms) - Status: 1
2025-11-11 23:15:58 - [23:15:58.604] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (67ms) - Status: 1
2025-11-11 23:15:58 - [23:15:58.604] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":67,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[23:15:58.609] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (67ms) - 1 rows
2025-11-11 23:15:58 - [23:15:58.609] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (67ms) - 1 rows
[23:15:58.611] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
2025-11-11 23:15:58 - [23:15:58.611] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
[23:15:58.634] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (98ms)
2025-11-11 23:15:58 - [23:15:58.634] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (98ms)
2025-11-11 23:15:58 - [23:15:58.634] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":98,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638984997585363123","Status":"SUCCESS","RowCount":1}
[23:15:58.664] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (130ms)
2025-11-11 23:15:58 - [23:15:58.664] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (130ms)
2025-11-11 23:15:58 - User full name loaded: John Koll
2025-11-11 23:15:58 - [Splash] All form instances configured successfully
2025-11-11 23:15:58 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-11 23:15:58 - [Splash] MainForm uses ThemedForm - automatic theme application
2025-11-11 23:15:58 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
2025-11-11 23:15:58 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|25_1
2025-11-11 23:15:58 - Transfer tab ComboBoxes loaded.
[23:15:58.727] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-11 23:15:58 - [23:15:58.727] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[23:15:58.729] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-11 23:15:58 - [23:15:58.729] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-11 23:15:58 - [23:15:58.729] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638984997587293851"}
[23:15:58.733] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:15:58 - [23:15:58.733] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:15:58.735] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-11 23:15:58 - [23:15:58.735] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[23:15:58.738] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (9ms) - Status: 1
2025-11-11 23:15:58 - [23:15:58.738] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (9ms) - Status: 1
2025-11-11 23:15:58 - [23:15:58.738] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":6,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[23:15:58.744] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (9ms) - 1 rows
2025-11-11 23:15:58 - [23:15:58.744] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (9ms) - 1 rows
[23:15:58.746] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-11 23:15:58 - [23:15:58.746] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[23:15:58.748] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (18ms)
2025-11-11 23:15:58 - [23:15:58.748] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (18ms)
2025-11-11 23:15:58 - [23:15:58.748] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638984997587293851","Status":"SUCCESS","RowCount":1}
[23:15:58.751] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (24ms)
2025-11-11 23:15:58 - [23:15:58.751] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (24ms)
2025-11-11 23:15:58 - User full name loaded: John Koll
2025-11-11 23:15:58 - Inventory tab ComboBoxes loaded.
[23:15:58.821] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (601ms)
2025-11-11 23:15:58 - [23:15:58.821] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (601ms)
2025-11-11 23:16:00 - [ThemedUserControl] Using applier for MainForm_UserControl_InventoryTab
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [ThemedUserControl] Using applier for MainForm_UserControl_QuickButtons
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
[23:16:00.524] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-11 23:16:00 - [23:16:00.524] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-11 23:16:00 - [ThemedUserControl] Using applier for MainForm_UserControl_SignalStrength
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [Splash] MainForm displayed successfully
2025-11-11 23:16:00 - [Splash] MainForm displayed - startup complete
2025-11-11 23:16:00 - [Splash] Splash screen closed
2025-11-11 23:16:00 - [ThemedForm] Using FormThemeApplier for MainForm
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-11 23:16:00 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
[Theme] Applied theme to form 'MainForm' in 35ms
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[23:16:00.601] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-11 23:16:00 - [23:16:00.601] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[23:16:00.603] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.603] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.603] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638984997606032476"}
[23:16:00.606] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:16:00 - [23:16:00.606] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:16:00.608] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.608] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[23:16:00.613] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (9ms) - Status: 1
2025-11-11 23:16:00 - [23:16:00.613] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (9ms) - Status: 1
2025-11-11 23:16:00 - [23:16:00.613] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[23:16:00.616] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (9ms) - 1 rows
2025-11-11 23:16:00 - [23:16:00.616] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (9ms) - 1 rows
[23:16:00.619] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-11 23:16:00 - [23:16:00.619] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[23:16:00.620] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (17ms)
2025-11-11 23:16:00 - [23:16:00.620] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (17ms)
2025-11-11 23:16:00 - [23:16:00.620] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638984997606032476","Status":"SUCCESS","RowCount":1}
[23:16:00.624] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (22ms)
2025-11-11 23:16:00 - [23:16:00.624] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (22ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[23:16:00.628] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-11 23:16:00 - [23:16:00.628] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[23:16:00.632] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-11 23:16:00 - [23:16:00.632] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[23:16:00.634] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-11 23:16:00 - [23:16:00.634] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-11 23:16:00 - Application Info - Development Menu configured for user 'JOHNK': Visible
[23:16:00.637] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
2025-11-11 23:16:00 - [23:16:00.637] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
[23:16:00.639] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-11 23:16:00 - [23:16:00.639] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-11 23:16:00 -
2025-11-11 23:16:00 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-11 23:16:00 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-11 23:16:00 - [QuickButtons]   User: JOHNK
2025-11-11 23:16:00 - [QuickButtons] ════════════════════════════════════════════════════════════
[23:16:00.648] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-11 23:16:00 - [23:16:00.648] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-11 23:16:00 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-11 23:16:00 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[23:16:00.658] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.658] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.658] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638984997606583202"}
[23:16:00.661] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:16:00 - [23:16:00.661] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:16:00.663] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.663] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[23:16:00.694] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (36ms) - Status: 1
2025-11-11 23:16:00 - [23:16:00.694] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (36ms) - Status: 1
2025-11-11 23:16:00 - [23:16:00.694] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 6 transaction(s) for user: JOHNK"},"ResultData":"DataTable[6 rows]","ErrorMessage":"Retrieved 6 transaction(s) for user: JOHNK"}
[23:16:00.697] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (36ms) - 6 rows
2025-11-11 23:16:00 - [23:16:00.697] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (36ms) - 6 rows
[23:16:00.700] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-11 23:16:00 - [23:16:00.700] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[23:16:00.702] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (43ms)
2025-11-11 23:16:00 - [23:16:00.702] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (43ms)
2025-11-11 23:16:00 - [23:16:00.702] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638984997606583202","Status":"SUCCESS","RowCount":6}
2025-11-11 23:16:00 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-11 23:16:00 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-11 23:16:00 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-11 23:16:00 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-11 23:16:00 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-11 23:16:00 - [Dao_QuickButtons] Added to array: 06-96408-001 + N/A (Qty: 40)
2025-11-11 23:16:00 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 10)
2025-11-11 23:16:00 - [Dao_QuickButtons] Array restructured: 6 unique buttons, 0 duplicates removed
2025-11-11 23:16:00 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-11 23:16:00 - [Dao_QuickButtons] All buttons deleted from database
2025-11-11 23:16:00 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-11 23:16:00 - [Dao_QuickButtons] Created button at position 1: 01-31976-000 + 10 (Qty: 1)
2025-11-11 23:16:00 - [Dao_QuickButtons] Created button at position 2: 04-27693-000 + 90 (Qty: 10)
2025-11-11 23:16:00 - [Dao_QuickButtons] Created button at position 3: 01-34578-000 + 880 (Qty: 20)
2025-11-11 23:16:00 - [Dao_QuickButtons] Created button at position 4: 03-29236-030 + 959 (Qty: 30)
2025-11-11 23:16:00 - [Dao_QuickButtons] Created button at position 5: 06-96408-001 + N/A (Qty: 40)
2025-11-11 23:16:00 - [Dao_QuickButtons] Created button at position 6: 01-33016-000 + 109 (Qty: 10)
2025-11-11 23:16:00 - [Dao_QuickButtons] Created 6 buttons in database
2025-11-11 23:16:00 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 6 buttons remain
2025-11-11 23:16:00 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-11 23:16:00 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 6 buttons remain
2025-11-11 23:16:00 - [QuickButtons] STEP 2: Loading data from database
[23:16:00.813] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.813] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.813] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638984997608139192"}
[23:16:00.817] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-11 23:16:00 - [23:16:00.817] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[23:16:00.819] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-11 23:16:00 - [23:16:00.819] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[23:16:00.824] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-11 23:16:00 - [23:16:00.824] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-11 23:16:00 - [23:16:00.824] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 6 transaction(s) for user: JOHNK"},"ResultData":"DataTable[6 rows]","ErrorMessage":"Retrieved 6 transaction(s) for user: JOHNK"}
[23:16:00.827] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 6 rows
2025-11-11 23:16:00 - [23:16:00.827] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 6 rows
[23:16:00.829] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-11 23:16:00 - [23:16:00.829] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[23:16:00.831] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (17ms)
2025-11-11 23:16:00 - [23:16:00.831] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (17ms)
2025-11-11 23:16:00 - [23:16:00.831] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638984997608139192","Status":"SUCCESS","RowCount":6}
[23:16:00.837] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-11 23:16:00 - [23:16:00.837] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-11 23:16:00 - [QuickButtons] STEP 2: ✓ Retrieved 6 button(s) from database
2025-11-11 23:16:00 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-11 23:16:00 - [QuickButtons] STEP 3:   Button 1: 01-31976-000 + Op:10 (Qty: 1)
2025-11-11 23:16:00 - [QuickButtons] STEP 3:   Button 2: 04-27693-000 + Op:90 (Qty: 10)
2025-11-11 23:16:00 - [QuickButtons] STEP 3:   Button 3: 01-34578-000 + Op:880 (Qty: 20)
2025-11-11 23:16:00 - [QuickButtons] STEP 3:   Button 4: 03-29236-030 + Op:959 (Qty: 30)
2025-11-11 23:16:00 - [QuickButtons] STEP 3:   Button 5: 06-96408-001 + Op:N/A (Qty: 40)
2025-11-11 23:16:00 - [QuickButtons] STEP 3:   Button 6: 01-33016-000 + Op:109 (Qty: 10)
2025-11-11 23:16:00 - [QuickButtons] STEP 3: Filled 6 button(s) with data
2025-11-11 23:16:00 - [QuickButtons] STEP 3: Clearing remaining 4 button(s)
2025-11-11 23:16:00 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-11 23:16:00 - [QuickButtons] STEP 4: Layout refreshed - 6 visible button(s)
2025-11-11 23:16:00 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-11 23:16:00 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-11 23:16:00 - [QuickButtons] ║ User: JOHNK
2025-11-11 23:16:00 - [QuickButtons] ║ Visible Buttons: 6
2025-11-11 23:16:00 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-11 23:16:00 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-11 23:16:00 -
[23:16:00.900] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (250ms)
2025-11-11 23:16:00 - [23:16:00.900] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (250ms)
[23:16:00.902] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-11 23:16:00 - [23:16:00.902] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'MainForm' unsubscribed from theme changes
2025-11-11 23:16:02 - [Cleanup] Starting application cleanup
2025-11-11 23:16:02 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-11 23:16:02 - [Cleanup] Memory cleanup completed
2025-11-11 23:16:02 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-11 23:16:02 - [Startup] Application shutdown completed
2025-11-11 23:16:02 - [Cleanup] Starting application cleanup
2025-11-11 23:16:02 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-11 23:16:02 - [Cleanup] Memory cleanup completed
2025-11-11 23:16:02 - [Cleanup] Application cleanup completed successfully
2025-11-11 23:16:02 - [Cleanup] Starting application cleanup
2025-11-11 23:16:02 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-11 23:16:02 - [Cleanup] Memory cleanup completed
2025-11-11 23:16:02 - [Cleanup] Application cleanup completed successfully
The program '[36196] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
