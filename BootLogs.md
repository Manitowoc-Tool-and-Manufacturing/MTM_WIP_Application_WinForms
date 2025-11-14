------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[11:26:53.877] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-14 11:26:53 - [11:26:53.877] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[11:26:53.910] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-14 11:26:53 - [11:26:53.910] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[11:26:53.912] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-14 11:26:53 - [11:26:53.912] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[11:26:53.914] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-14 11:26:53 - [11:26:53.914] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-14 11:26:53 - [Startup] Application initialization started
2025-11-14 11:26:53 - [Startup] User identified: JOHNK
2025-11-14 11:26:53 - [Dao_System] Checking database connectivity
[11:26:53.946] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-14 11:26:53 - [11:26:53.946] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-14 11:26:54 - [11:26:53.946] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638987164139466699"}
[11:26:54.010] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:54 - [11:26:54.010] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:54.012] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-14 11:26:54 - [11:26:54.012] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[11:26:54.193] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (246ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.193] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (246ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.193] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":246,"Thread":15,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[11:26:54.206] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (246ms) - 9 rows
2025-11-14 11:26:54 - [11:26:54.206] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (246ms) - 9 rows
[11:26:54.209] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (198ms)
2025-11-14 11:26:54 - [11:26:54.209] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (198ms)
[11:26:54.211] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (264ms)
2025-11-14 11:26:54 - [11:26:54.211] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (264ms)
2025-11-14 11:26:54 - [11:26:54.211] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":264,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638987164139466699","Status":"SUCCESS","RowCount":9}
2025-11-14 11:26:54 - [Dao_System] Database connectivity check passed
2025-11-14 11:26:54 - [Startup] Database connectivity validated successfully
2025-11-14 11:26:54 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-14 11:26:54 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-14 11:26:54 - [Startup] Parameter cache populated: 120 procedures, 536 total parameters
2025-11-14 11:26:54 - [Startup] Parameter prefix cache initialized successfully in 13ms. Cached 120 stored procedures.
[Startup] Parameter cache: 120 procedures cached in 13ms
[11:26:54.236] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-14 11:26:54 - [11:26:54.236] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-14 11:26:54 - [11:26:54.236] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638987164142366158"}
[11:26:54.239] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:54 - [11:26:54.239] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:54.240] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-14 11:26:54 - [11:26:54.240] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-14 11:26:54 - [Startup] Initializing dependency injection container
2025-11-14 11:26:54 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
2025-11-14 11:26:54 - [Startup] Dependency injection container configured successfully
2025-11-14 11:26:54 - [Startup] Dependency injection container initialized successfully
2025-11-14 11:26:54 - [Splash] Initializing splash screen
[11:26:54.282] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (45ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.282] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (45ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.282] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":45,"Thread":10,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user access type(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user access type(s)"}
[11:26:54.284] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (45ms) - 88 rows
2025-11-14 11:26:54 - [11:26:54.284] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (45ms) - 88 rows
[11:26:54.286] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
2025-11-14 11:26:54 - [11:26:54.286] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
[11:26:54.288] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (51ms)
2025-11-14 11:26:54 - [11:26:54.288] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (51ms)
2025-11-14 11:26:54 - [11:26:54.288] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_GetUserAccessType","ElapsedMs":51,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638987164142366158","Status":"SUCCESS","RowCount":88}
2025-11-14 11:26:54 - System_UserAccessType executed successfully for user: JOHNK
[11:26:54.309] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-14 11:26:54 - [11:26:54.309] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[11:26:54.311] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-14 11:26:54 - [11:26:54.311] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-14 11:26:54 - [ThemedUserControl] Control_ProgressBarUserControl initialized with automatic theme support
[11:26:54.371] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-14 11:26:54 - [11:26:54.371] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[11:26:54.402] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-14 11:26:54 - [11:26:54.402] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[11:26:54.404] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-14 11:26:54 - [11:26:54.404] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[11:26:54.405] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-14 11:26:54 - [11:26:54.405] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[11:26:54.407] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (98ms)
2025-11-14 11:26:54 - [11:26:54.407] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (98ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-14 11:26:54 - [ThemedUserControl] Using applier for _progressControl
2025-11-14 11:26:54 - [FormThemeApplier] Applying to '_progressControl' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-14 11:26:54 - [FormThemeApplier] Applying to '_progressControl' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-14 11:26:54 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:54 - [FocusUtils] CanControlReceiveFocus:  (PictureBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:54 - [FocusUtils] ApplyFocusEventHandling:  (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:54 - [FocusUtils] CanControlReceiveFocus:  (ProgressBar) - FALSE (control type cannot receive focus)
2025-11-14 11:26:54 - [FocusUtils] ApplyFocusEventHandling:  (ProgressBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:54 - [FocusUtils] CanControlReceiveFocus:  (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:54 - [FocusUtils] ApplyFocusEventHandling:  (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:54 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-14-2025 @ 11-26 AM_normal.csv
2025-11-14 11:26:54 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-14 11:26:54 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-14 11:26:54 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-14 11:26:54 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-14 11:26:54 - [Splash] Starting async database connectivity verification
2025-11-14 11:26:54 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-14 11:26:54 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[11:26:54.859] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:26:54 - [11:26:54.859] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:26:54 - [11:26:54.859] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164148591921"}
[11:26:54.862] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:54 - [11:26:54.862] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:54.863] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-14 11:26:54 - [11:26:54.863] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[11:26:54.931] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (72ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.931] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (72ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.931] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":72,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[11:26:54.934] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (72ms) - 3745 rows
2025-11-14 11:26:54 - [11:26:54.934] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (72ms) - 3745 rows
[11:26:54.936] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (74ms)
2025-11-14 11:26:54 - [11:26:54.936] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (74ms)
[11:26:54.938] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (79ms)
2025-11-14 11:26:54 - [11:26:54.938] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (79ms)
2025-11-14 11:26:54 - [11:26:54.938] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":79,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164148591921","Status":"SUCCESS","RowCount":3745}
2025-11-14 11:26:54 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-14 11:26:54 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), RequiresColorCode(Boolean), ItemType(String), Operations(String)
2025-11-14 11:26:54 - [DataTable] ComboBoxPart: Target schema:
2025-11-14 11:26:54 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[11:26:54.959] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:26:54 - [11:26:54.959] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:26:54 - [11:26:54.959] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164149599133"}
[11:26:54.963] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:54 - [11:26:54.963] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:54.965] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:26:54 - [11:26:54.965] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[11:26:54.994] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (34ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.994] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (34ms) - Status: 1
2025-11-14 11:26:54 - [11:26:54.994] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":34,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[11:26:54.998] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (34ms) - 72 rows
2025-11-14 11:26:54 - [11:26:54.998] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (34ms) - 72 rows
[11:26:55.000] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-14 11:26:55 - [11:26:55.000] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[11:26:55.002] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (43ms)
2025-11-14 11:26:55 - [11:26:55.002] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (43ms)
2025-11-14 11:26:55 - [11:26:55.002] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164149599133","Status":"SUCCESS","RowCount":72}
2025-11-14 11:26:55 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-14 11:26:55 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-14 11:26:55 - [DataTable] ComboBoxOperation: Target schema:
2025-11-14 11:26:55 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[11:26:55.014] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:26:55 - [11:26:55.014] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:26:55 - [11:26:55.014] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164150146585"}
[11:26:55.017] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.017] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.019] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-14 11:26:55 - [11:26:55.019] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[11:26:55.097] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (83ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.097] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (83ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.097] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":83,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[11:26:55.102] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (83ms) - 10371 rows
2025-11-14 11:26:55 - [11:26:55.102] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (83ms) - 10371 rows
[11:26:55.104] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (87ms)
2025-11-14 11:26:55 - [11:26:55.104] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (87ms)
[11:26:55.106] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (91ms)
2025-11-14 11:26:55 - [11:26:55.106] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (91ms)
2025-11-14 11:26:55 - [11:26:55.106] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":91,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164150146585","Status":"SUCCESS","RowCount":10371}
2025-11-14 11:26:55 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-14 11:26:55 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-14 11:26:55 - [DataTable] ComboBoxLocation: Target schema:
2025-11-14 11:26:55 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[11:26:55.124] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-14 11:26:55 - [11:26:55.124] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-14 11:26:55 - [11:26:55.124] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638987164151240100"}
[11:26:55.126] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.126] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.128] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-14 11:26:55 - [11:26:55.128] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[11:26:55.159] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.159] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.159] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[11:26:55.161] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
2025-11-14 11:26:55 - [11:26:55.161] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
[11:26:55.164] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-14 11:26:55 - [11:26:55.164] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[11:26:55.166] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-14 11:26:55 - [11:26:55.166] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-14 11:26:55 - [11:26:55.166] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638987164151240100","Status":"SUCCESS","RowCount":88}
2025-11-14 11:26:55 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-14 11:26:55 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-14 11:26:55 - [DataTable] ComboBoxUser: Target schema:
2025-11-14 11:26:55 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[11:26:55.174] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-14 11:26:55 - [11:26:55.174] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-14 11:26:55 - [11:26:55.174] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638987164151745799"}
[11:26:55.177] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.177] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.179] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-14 11:26:55 - [11:26:55.179] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[11:26:55.210] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (36ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.210] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (36ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.210] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 4 item type(s)"},"ResultData":"DataTable[4 rows]","ErrorMessage":"Retrieved 4 item type(s)"}
[11:26:55.213] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (36ms) - 4 rows
2025-11-14 11:26:55 - [11:26:55.213] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (36ms) - 4 rows
[11:26:55.215] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-14 11:26:55 - [11:26:55.215] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[11:26:55.217] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (43ms)
2025-11-14 11:26:55 - [11:26:55.217] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (43ms)
2025-11-14 11:26:55 - [11:26:55.217] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_item_types_Get_All","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638987164151745799","Status":"SUCCESS","RowCount":4}
2025-11-14 11:26:55 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-14 11:26:55 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-14 11:26:55 - [DataTable] ComboBoxItemType: Target schema:
2025-11-14 11:26:55 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-14 11:26:55 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 55, Status: Loading color code cache...
[11:26:55.283] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
2025-11-14 11:26:55 - [11:26:55.283] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[11:26:55.285] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-14 11:26:55 - [11:26:55.285] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-14 11:26:55 - [11:26:55.285] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638987164152856873"}
[11:26:55.288] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.288] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.290] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
2025-11-14 11:26:55 - [11:26:55.290] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[11:26:55.320] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (35ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.320] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (35ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.320] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[2 rows]"}
[11:26:55.323] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (35ms) - 2 rows
2025-11-14 11:26:55 - [11:26:55.323] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (35ms) - 2 rows
[11:26:55.325] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-14 11:26:55 - [11:26:55.325] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[11:26:55.327] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (42ms)
2025-11-14 11:26:55 - [11:26:55.327] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (42ms)
2025-11-14 11:26:55 - [11:26:55.327] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638987164152856873","Status":"SUCCESS","RowCount":2}
[11:26:55.331] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (47ms)
2025-11-14 11:26:55 - [11:26:55.331] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (47ms)
2025-11-14 11:26:55 - [Model_Application_Variables] ColorCodeParts cache loaded: 2 parts
[11:26:55.335] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-14 11:26:55 - [11:26:55.335] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-14 11:26:55 - [11:26:55.335] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638987164153350508"}
[11:26:55.337] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.337] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.339] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-14 11:26:55 - [11:26:55.339] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[11:26:55.368] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (33ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.368] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (33ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.368] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":33,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[11:26:55.371] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (33ms) - 10 rows
2025-11-14 11:26:55 - [11:26:55.371] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (33ms) - 10 rows
[11:26:55.373] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (35ms)
2025-11-14 11:26:55 - [11:26:55.373] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (35ms)
[11:26:55.375] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (40ms)
2025-11-14 11:26:55 - [11:26:55.375] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (40ms)
2025-11-14 11:26:55 - [11:26:55.375] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":40,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638987164153350508","Status":"SUCCESS","RowCount":10}
2025-11-14 11:26:55 - [Model_Application_Variables] ValidColorCodes cache loaded: 10 colors
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 58, Status: Color code cache loaded.
2025-11-14 11:26:55 - [Splash] ColorCodeParts cache loaded: 2 parts flagged
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-14 11:26:55 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-14 11:26:55 - Running VersionChecker - checking database version information.
[11:26:55.443] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:26:55 - [11:26:55.443] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:26:55 - [11:26:55.443] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987164154439817"}
[11:26:55.447] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.447] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.449] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-14 11:26:55 - [11:26:55.449] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-14 11:26:55 - [Splash] Version checker initialized
[11:26:55.481] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (37ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.481] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (37ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.481] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[11:26:55.484] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (37ms) - 1 rows
2025-11-14 11:26:55 - [11:26:55.484] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (37ms) - 1 rows
[11:26:55.486] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-14 11:26:55 - [11:26:55.486] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[11:26:55.488] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (44ms)
2025-11-14 11:26:55 - [11:26:55.488] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (44ms)
2025-11-14 11:26:55 - [11:26:55.488] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987164154439817","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-14 11:26:55 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-14 11:26:55 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[11:26:55.520] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-14 11:26:55 - [11:26:55.520] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-14 11:26:55 - [11:26:55.520] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638987164155202424"}
[11:26:55.522] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.522] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.524] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-14 11:26:55 - [11:26:55.524] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[11:26:55.532] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.532] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.532] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[11:26:55.535] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
2025-11-14 11:26:55 - [11:26:55.535] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
[11:26:55.537] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-14 11:26:55 - [11:26:55.537] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[11:26:55.539] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (19ms)
2025-11-14 11:26:55 - [11:26:55.539] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (19ms)
2025-11-14 11:26:55 - [11:26:55.539] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638987164155202424","Status":"SUCCESS","RowCount":9}
2025-11-14 11:26:55 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-14 11:26:55 - Successfully loaded 9 themes from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Default' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Forest' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-14 11:26:55 - [DEBUG] Urban Bloom JSON preview: {"InfoColor": "#8E44AD", "ErrorColor": "#F44336", "AccentColor": "#8E44AD", "SuccessColor": "#BA68C8", "WarningColor": "#FF9800", "FormBackColor": "#F6F0FA", "FormForeColor": "#1A1A1A", "LabelBackColor": "#F6F0FA", "LabelForeColor": "#1A1A1A", "PanelBackColor": "#F6F0FA", "PanelForeColor": "#1A1A1A", "ButtonBackColor": "#EDE3F7", "ButtonForeColor": "#1A1A1A", "ControlBackColor": "#F6F0FA", "ControlForeColor": "#1A1A1A", "ListBoxBackColor": "#FFFFFF", "ListBoxForeColor": "#1A1A1A", "PanelBorderCo
2025-11-14 11:26:55 - [DEBUG] Urban Bloom deserialized - FormBackColor: Color [A=255, R=246, G=240, B=250], FormForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:55 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-14 11:26:55 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-14 11:26:55 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[11:26:55.585] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-14 11:26:55 - [11:26:55.585] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[11:26:55.587] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-14 11:26:55 - [11:26:55.587] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:26:55.589] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.589] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.589] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164155895999"}
[11:26:55.592] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.592] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.594] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.594] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:26:55.627] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (37ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.627] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (37ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.627] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":37,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:26:55.631] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (37ms) - 1 rows
2025-11-14 11:26:55 - [11:26:55.631] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (37ms) - 1 rows
[11:26:55.632] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (40ms)
2025-11-14 11:26:55 - [11:26:55.632] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (40ms)
[11:26:55.634] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (45ms)
2025-11-14 11:26:55 - [11:26:55.634] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (45ms)
2025-11-14 11:26:55 - [11:26:55.634] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":45,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164155895999","Status":"SUCCESS","RowCount":1}
[11:26:55.643] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (55ms)
2025-11-14 11:26:55 - [11:26:55.643] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (55ms)
[11:26:55.645] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (60ms)
2025-11-14 11:26:55 - [11:26:55.645] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (60ms)
2025-11-14 11:26:55 - Theme system enabled status for user JOHNK: True
[11:26:55.649] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-14 11:26:55 - [11:26:55.649] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[11:26:55.651] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-14 11:26:55 - [11:26:55.651] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:26:55.653] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.653] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.653] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164156531145"}
[11:26:55.656] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.656] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.658] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.658] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:26:55.662] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.662] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.662] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:26:55.665] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-14 11:26:55 - [11:26:55.665] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[11:26:55.667] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
2025-11-14 11:26:55 - [11:26:55.667] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
[11:26:55.669] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-14 11:26:55 - [11:26:55.669] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-14 11:26:55 - [11:26:55.669] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164156531145","Status":"SUCCESS","RowCount":1}
[11:26:55.672] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (20ms)
2025-11-14 11:26:55 - [11:26:55.672] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (20ms)
[11:26:55.674] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (24ms)
2025-11-14 11:26:55 - [11:26:55.674] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (24ms)
2025-11-14 11:26:55 - Loaded theme preference for user JOHNK: Arctic
2025-11-14 11:26:55 - Set Model_Application_Variables.ThemeName to: Arctic
2025-11-14 11:26:55 - Theme system initialized for user JOHNK. Final theme: Arctic, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
2025-11-14 11:26:55 - ThemeStore loaded 9 themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-14 11:26:55 - [Splash] ThemeStore loaded from database
2025-11-14 11:26:55 - [Splash] ThemeManager initialized with 'Arctic' theme
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-14 11:26:55 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-14 11:26:55 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-14 11:26:55 - [Splash] Loading theme settings
[11:26:55.816] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-14 11:26:55 - [11:26:55.816] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[11:26:55.818] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-14 11:26:55 - [11:26:55.818] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:26:55.820] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.820] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.820] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164158209429"}
[11:26:55.824] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.824] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.826] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.826] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:26:55.831] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.831] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.831] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":10,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:26:55.833] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-14 11:26:55 - [11:26:55.833] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[11:26:55.835] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-14 11:26:55 - [11:26:55.835] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[11:26:55.837] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-14 11:26:55 - [11:26:55.837] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-14 11:26:55 - [11:26:55.837] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164158209429","Status":"SUCCESS","RowCount":1}
[11:26:55.840] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-14 11:26:55 - [11:26:55.840] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[11:26:55.842] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (25ms)
2025-11-14 11:26:55 - [11:26:55.842] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (25ms)
[11:26:55.845] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-14 11:26:55 - [11:26:55.845] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[11:26:55.847] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-14 11:26:55 - [11:26:55.847] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:26:55.848] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.848] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.848] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164158489102"}
[11:26:55.851] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.851] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.853] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.853] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:26:55.857] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (8ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.857] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (8ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.857] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":8,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:26:55.860] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (8ms) - 1 rows
2025-11-14 11:26:55 - [11:26:55.860] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (8ms) - 1 rows
[11:26:55.862] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
2025-11-14 11:26:55 - [11:26:55.862] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
[11:26:55.864] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (15ms)
2025-11-14 11:26:55 - [11:26:55.864] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (15ms)
2025-11-14 11:26:55 - [11:26:55.864] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":15,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164158489102","Status":"SUCCESS","RowCount":1}
[11:26:55.868] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (20ms)
2025-11-14 11:26:55 - [11:26:55.868] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (20ms)
[11:26:55.869] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (24ms)
2025-11-14 11:26:55 - [11:26:55.869] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (24ms)
[11:26:55.872] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-14 11:26:55 - [11:26:55.872] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[11:26:55.874] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-14 11:26:55 - [11:26:55.874] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:26:55.876] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.876] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.876] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164158767110"}
[11:26:55.879] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:55 - [11:26:55.879] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:55.881] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-14 11:26:55 - [11:26:55.881] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:26:55.885] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.885] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-14 11:26:55 - [11:26:55.885] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:26:55.888] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-14 11:26:55 - [11:26:55.888] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[11:26:55.890] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
2025-11-14 11:26:55 - [11:26:55.890] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
[11:26:55.892] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-14 11:26:55 - [11:26:55.892] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-14 11:26:55 - [11:26:55.892] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638987164158767110","Status":"SUCCESS","RowCount":1}
[11:26:55.896] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
2025-11-14 11:26:55 - [11:26:55.896] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
[11:26:55.898] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-14 11:26:55 - [11:26:55.898] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-14 11:26:55 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-14 11:26:55 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-14 11:26:55 - [Splash] Core startup sequence completed
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeDebouncer[0]
      Applying debounced theme change: Arctic (Reason: Login)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Theme changed to 'Arctic' (Reason: Login, User: JOHNK)
2025-11-14 11:26:55 - Theme changed to 'Arctic' - Reason: Login
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-14 11:26:56 - [ThemedForm] MainForm initialized with automatic theme support
[11:26:56.269] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-14 11:26:56 - [11:26:56.269] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[11:26:56.272] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-14 11:26:56 - [11:26:56.272] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-14 11:26:56 - [ThemedUserControl] Control_ConnectionStrengthControl initialized with automatic theme support
2025-11-14 11:26:56 - [ThemedUserControl] Control_InventoryTab initialized with automatic theme support
[11:26:56.304] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.304] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[11:26:56.306] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.306] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[11:26:56.316] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.316] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[11:26:56.320] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.320] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[11:26:56.322] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.322] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[11:26:56.324] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.324] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[11:26:56.327] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.327] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[11:26:56.330] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:26:56 - [11:26:56.330] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:26:56 - [11:26:56.330] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164163307426"}
[11:26:56.334] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:56 - [11:26:56.334] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:56.335] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-14 11:26:56 - [11:26:56.335] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[11:26:56.339] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:26:56 - [11:26:56.339] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:26:56 - [11:26:56.339] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164163398604"}
[11:26:56.342] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:56 - [11:26:56.342] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:56.344] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:26:56 - [11:26:56.344] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[11:26:56.350] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:26:56 - [11:26:56.350] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:26:56 - [11:26:56.350] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164163501267"}
[11:26:56.354] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:56 - [11:26:56.354] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:56.356] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-14 11:26:56 - [11:26:56.356] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[11:26:56.359] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.359] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-14 11:26:56 - Inventory tab events wired up.
[11:26:56.363] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.363] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[11:26:56.366] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.366] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[11:26:56.368] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.368] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[11:26:56.371] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-14 11:26:56 - [11:26:56.371] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[11:26:56.373] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (68ms)
2025-11-14 11:26:56 - [11:26:56.373] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (68ms)
[11:26:56.377] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-14 11:26:56 - [11:26:56.377] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[11:26:56.379] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-14 11:26:56 - [11:26:56.379] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-14 11:26:56 - Control_AdvancedInventory constructor entered.
[11:26:56.393] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-14 11:26:56 - [11:26:56.393] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[11:26:56.395] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-14 11:26:56 - [11:26:56.395] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - Control_AdvancedInventory constructor exited.
[11:26:56.602] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.602] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[11:26:56.604] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.604] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[11:26:56.615] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.615] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[11:26:56.617] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.617] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header (Panel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_RemoveTab_TextBox_Part (SuggestionTextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_RemoveTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_RemoveTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Operation (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_RemoveTab_TextBox_Operation (SuggestionTextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_RemoveTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_RemoveTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
[11:26:56.716] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.716] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[11:26:56.718] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.718] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[11:26:56.723] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.723] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[11:26:56.725] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.725] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[11:26:56.727] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.727] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[11:26:56.730] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-14 11:26:56 - [11:26:56.730] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[11:26:56.732] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (130ms)
2025-11-14 11:26:56 - [11:26:56.732] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (130ms)
[11:26:56.736] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-14 11:26:56 - [11:26:56.736] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[11:26:56.738] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-14 11:26:56 - [11:26:56.738] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[11:26:56.748] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-14 11:26:56 - [11:26:56.748] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[11:26:56.750] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-14 11:26:56 - [11:26:56.750] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top (Panel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_To (DateTimePicker)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_To
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_From (DateTimePicker)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_From
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Location (TextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Location
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Location
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Part (TextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date (CheckBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_ComboBox_User (ComboBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_ComboBox_User
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_ComboBox_User
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached DropDown handler for ComboBox Control_AdvancedRemove_ComboBox_User
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_ComboBox_User
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Operation (TextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Notes (TextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Notes
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Notes
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMin (TextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMin
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMin
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMax (TextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMax
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMax
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center (Panel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results (DataGridView) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
[11:26:56.932] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-14 11:26:56 - [11:26:56.932] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[11:26:56.941] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-14 11:26:56 - [11:26:56.941] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[11:26:56.943] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-14 11:26:56 - [11:26:56.943] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_Operation (SuggestionTextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_Operation
2025-11-14 11:26:56 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-14 11:26:56 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_Part (SuggestionTextBox)
2025-11-14 11:26:56 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_Part
2025-11-14 11:26:56 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: panel1 (Panel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - APPLYING
2025-11-14 11:26:57 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_ToLocation (SuggestionTextBox)
2025-11-14 11:26:57 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_ToLocation
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_ToLocation
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_ToLocation
2025-11-14 11:26:57 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_ToLocation
2025-11-14 11:26:57 - Transfer tab events wired up.
[11:26:57.073] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-14 11:26:57 - [11:26:57.073] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-14 11:26:57 - [ThemedUserControl] Control_QuickButtons initialized with automatic theme support
[11:26:57.077] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-14 11:26:57 - [11:26:57.077] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[11:26:57.079] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-14 11:26:57 - [11:26:57.079] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[11:26:57.083] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-14 11:26:57 - [11:26:57.083] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[11:26:57.086] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-14 11:26:57 - [11:26:57.086] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[11:26:57.107] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-14 11:26:57 - [11:26:57.107] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[11:26:57.111] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-14 11:26:57 - [11:26:57.111] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[11:26:57.113] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-14 11:26:57 - [11:26:57.113] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[11:26:57.118] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-14 11:26:57 - [11:26:57.118] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[11:26:57.120] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
2025-11-14 11:26:57 - [11:26:57.120] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
[11:26:57.124] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-14 11:26:57 - [11:26:57.124] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[11:26:57.126] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-14 11:26:57 - [11:26:57.126] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[11:26:57.128] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (17ms)
2025-11-14 11:26:57 - [11:26:57.128] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (17ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[11:26:57.133] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-14 11:26:57 - [11:26:57.133] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[11:26:57.136] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-14 11:26:57 - [11:26:57.136] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[11:26:57.140] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-14 11:26:57 - [11:26:57.140] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[11:26:57.142] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-14 11:26:57 - [11:26:57.142] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[11:26:57.147] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-14 11:26:57 - [11:26:57.147] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[11:26:57.150] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-14 11:26:57 - [11:26:57.150] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-14 11:26:57 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[11:26:57.155] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-14 11:26:57 - [11:26:57.155] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[11:26:57.157] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
2025-11-14 11:26:57 - [11:26:57.157] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[11:26:57.161] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-14 11:26:57 - [11:26:57.161] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[11:26:57.165] [MEDIUM] ⬅️ EXITING MainForm.MainForm (895ms)
2025-11-14 11:26:57 - [11:26:57.165] [MEDIUM] ⬅️ EXITING MainForm.MainForm (895ms)
2025-11-14 11:26:57 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-14 11:26:57 - Remove tab suggestion controls configured.
2025-11-14 11:26:57 - Removal tab events wired up.
2025-11-14 11:26:57 - Initial setup of ComboBoxes in the Remove Tab.
[11:26:57.176] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-14 11:26:57 - [11:26:57.176] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[11:26:57.178] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-14 11:26:57 - [11:26:57.178] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-14 11:26:57 - Transfer tab suggestion controls configured.
2025-11-14 11:26:57 - [11:26:57.178] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638987164171789759"}
[11:26:57.184] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:57 - [11:26:57.184] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:57.187] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[11:26:57.187] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-14 11:26:57 - [11:26:57.187] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-14 11:26:57 - [11:26:57.187] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[11:26:57.192] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-14 11:26:57 - [11:26:57.192] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-14 11:26:57 - [11:26:57.192] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638987164171925969"}
[11:26:57.197] [MEDIUM]         ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:57 - [11:26:57.197] [MEDIUM]         ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:57.199] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-14 11:26:57 - [11:26:57.199] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-14 11:26:57 - [Performance Warning] Stored procedure 'md_operation_numbers_Get_All' (Query) took 865ms (threshold: 500ms)
[11:26:57.206] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (865ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.206] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (865ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.206] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":865,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[11:26:57.214] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (865ms) - 72 rows
2025-11-14 11:26:57 - [11:26:57.214] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (865ms) - 72 rows
[11:26:57.217] [MEDIUM]         ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (883ms)
2025-11-14 11:26:57 - [11:26:57.217] [MEDIUM]         ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (883ms)
[11:26:57.220] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (880ms)
2025-11-14 11:26:57 - [11:26:57.220] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (880ms)
2025-11-14 11:26:57 - [11:26:57.220] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":880,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164163398604","Status":"SUCCESS","RowCount":72}
2025-11-14 11:26:57 - [Splash] All form instances configured successfully
2025-11-14 11:26:57 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-14 11:26:57 - [Splash] MainForm uses ThemedForm - automatic theme application
2025-11-14 11:26:57 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
[11:26:57.278] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (99ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.278] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (99ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.278] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":99,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[11:26:57.285] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (99ms) - 1 rows
2025-11-14 11:26:57 - [11:26:57.285] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (99ms) - 1 rows
[11:26:57.288] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (945ms)
2025-11-14 11:26:57 - [11:26:57.288] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (945ms)
[11:26:57.290] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (111ms)
2025-11-14 11:26:57 - [11:26:57.290] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (111ms)
2025-11-14 11:26:57 - [11:26:57.290] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":111,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638987164171789759","Status":"SUCCESS","RowCount":1}
[11:26:57.295] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (102ms) - Status: 1
[11:26:57.296] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (119ms)
2025-11-14 11:26:57 - [11:26:57.295] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (102ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.296] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (119ms)
2025-11-14 11:26:57 - [11:26:57.295] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":102,"Thread":31,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[11:26:57.302] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (102ms) - 1 rows
2025-11-14 11:26:57 - User full name loaded: John Koll
2025-11-14 11:26:57 - [11:26:57.302] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (102ms) - 1 rows
[11:26:57.308] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (952ms)
2025-11-14 11:26:57 - [11:26:57.308] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (952ms)
[11:26:57.310] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (117ms)
2025-11-14 11:26:57 - [11:26:57.310] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (117ms)
2025-11-14 11:26:57 - [11:26:57.310] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":117,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638987164171925969","Status":"SUCCESS","RowCount":1}
[11:26:57.314] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (127ms)
2025-11-14 11:26:57 - [11:26:57.314] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (127ms)
2025-11-14 11:26:57 - User full name loaded: John Koll
2025-11-14 11:26:57 - [Performance Warning] Stored procedure 'md_part_ids_Get_All' (Query) took 990ms (threshold: 500ms)
[11:26:57.322] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (990ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.322] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (990ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.322] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":990,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[11:26:57.330] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (990ms) - 3745 rows
2025-11-14 11:26:57 - [11:26:57.330] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (990ms) - 3745 rows
[11:26:57.332] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (148ms)
2025-11-14 11:26:57 - [11:26:57.332] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (148ms)
[11:26:57.335] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1004ms)
2025-11-14 11:26:57 - [11:26:57.335] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1004ms)
2025-11-14 11:26:57 - [11:26:57.335] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":1004,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164163307426","Status":"SUCCESS","RowCount":3745}
2025-11-14 11:26:57 - [Performance Warning] Stored procedure 'md_locations_Get_All' (Query) took 1041ms (threshold: 500ms)
[11:26:57.393] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1041ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.393] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1041ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.393] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1041,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[11:26:57.396] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1041ms) - 10371 rows
2025-11-14 11:26:57 - [11:26:57.396] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1041ms) - 10371 rows
[11:26:57.399] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (201ms)
2025-11-14 11:26:57 - [11:26:57.399] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (201ms)
[11:26:57.401] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1051ms)
2025-11-14 11:26:57 - [11:26:57.401] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1051ms)
2025-11-14 11:26:57 - [11:26:57.401] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":1051,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164163501267","Status":"SUCCESS","RowCount":10371}
[11:26:57.411] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
2025-11-14 11:26:57 - [11:26:57.411] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[11:26:57.413] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-14 11:26:57 - [11:26:57.413] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-14 11:26:57 - [11:26:57.413] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638987164174137299"}
[11:26:57.417] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:57 - [11:26:57.417] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:57.419] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
2025-11-14 11:26:57 - [11:26:57.419] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[11:26:57.425] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (11ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.425] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (11ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.425] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[2 rows]"}
[11:26:57.428] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (11ms) - 2 rows
2025-11-14 11:26:57 - [11:26:57.428] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (11ms) - 2 rows
[11:26:57.431] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-14 11:26:57 - [11:26:57.431] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[11:26:57.433] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (19ms)
2025-11-14 11:26:57 - [11:26:57.433] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (19ms)
2025-11-14 11:26:57 - [11:26:57.433] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638987164174137299","Status":"SUCCESS","RowCount":2}
[11:26:57.437] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (26ms)
2025-11-14 11:26:57 - [11:26:57.437] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (26ms)
2025-11-14 11:26:57 - [Model_Application_Variables] ColorCodeParts cache loaded: 2 parts
[11:26:57.441] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-14 11:26:57 - [11:26:57.441] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-14 11:26:57 - [11:26:57.441] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638987164174412221"}
[11:26:57.445] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:57 - [11:26:57.445] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:57.447] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-14 11:26:57 - [11:26:57.447] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[11:26:57.453] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (12ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.453] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (12ms) - Status: 1
2025-11-14 11:26:57 - [11:26:57.453] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[11:26:57.457] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (12ms) - 10 rows
2025-11-14 11:26:57 - [11:26:57.457] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (12ms) - 10 rows
[11:26:57.459] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-14 11:26:57 - [11:26:57.459] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[11:26:57.461] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (20ms)
2025-11-14 11:26:57 - [11:26:57.461] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (20ms)
2025-11-14 11:26:57 - [11:26:57.461] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638987164174412221","Status":"SUCCESS","RowCount":10}
2025-11-14 11:26:57 - [Model_Application_Variables] ValidColorCodes cache loaded: 10 colors
2025-11-14 11:26:57 - [InventoryTab Startup] Color code caches loaded: Parts=2, Colors=10
2025-11-14 11:26:57 - Inventory tab suggestion controls configured.
[11:26:57.575] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (1247ms)
2025-11-14 11:26:57 - [11:26:57.575] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (1247ms)
2025-11-14 11:26:57 - [ThemedUserControl] Using applier for MainForm_UserControl_InventoryTab
2025-11-14 11:26:57 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:57 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-14 11:26:57 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-14 11:26:57 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-14 11:26:57 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 12 controls
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-14 11:26:57 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-14 11:26:57 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-14 11:26:57 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-14 11:26:57 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-14 11:26:57 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:57 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-14 11:26:57 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-14 11:26:57 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:57 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-14 11:26:57 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-14 11:26:57 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:57 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-14 11:26:57 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_ColorCode (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_ColorCode (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_WorkOrder (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_WorkOrder (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [ThemedUserControl] Using applier for MainForm_UserControl_QuickButtons
2025-11-14 11:26:57 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:57 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[11:26:57.925] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-14 11:26:57 - [11:26:57.925] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 0 controls
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: MainForm_TableLayout (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: MainForm_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: MainForm_MenuStrip (MenuStrip) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: MainForm_MenuStrip (MenuStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: MainForm_SplitContainer_Middle (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: MainForm_SplitContainer_Middle (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: MainForm_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Inventory (TabPage) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Inventory (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_InventoryTab (Control_InventoryTab) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_InventoryTab (Control_InventoryTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:57 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:57 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-14 11:26:58 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-14 11:26:58 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-14 11:26:58 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 12 controls
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-14 11:26:58 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-14 11:26:58 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-14 11:26:58 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-14 11:26:58 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-14 11:26:58 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:58 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-14 11:26:58 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-14 11:26:58 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:58 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-14 11:26:58 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-14 11:26:58 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_ColorCode (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_ColorCode (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_WorkOrder (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_WorkOrder (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedInventory - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Remove - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Remove (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_RemoveTab - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_RemoveTab (Control_RemoveTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Operation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedRemove - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_To - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_From - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Location - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_ComboBox_User - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMin - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMax - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Transfer (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_TransferTab - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_TransferTab (Control_TransferTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_QuickButtons (Control_QuickButtons) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_QuickButtons (Control_QuickButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel1 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel1 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_StatusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_StatusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] CanControlReceiveFocus:  (MdiClient) - FALSE (control type cannot receive focus)
2025-11-14 11:26:58 - [FocusUtils] ApplyFocusEventHandling:  (MdiClient) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:26:58 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-14 11:26:58 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part - EXITING
2025-11-14 11:26:58 - [Splash] MainForm displayed successfully
2025-11-14 11:26:58 - [Splash] MainForm displayed - startup complete
2025-11-14 11:26:58 - [Splash] Splash screen closed
[11:26:58.727] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-14 11:26:58 - [11:26:58.727] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-14 11:26:58 -
2025-11-14 11:26:58 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-14 11:26:58 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-14 11:26:58 - [QuickButtons]   User: JOHNK
2025-11-14 11:26:58 - [QuickButtons] ════════════════════════════════════════════════════════════
[11:26:58.737] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-14 11:26:58 - [11:26:58.737] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-14 11:26:58 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-14 11:26:58 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[11:26:58.744] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:58 - [11:26:58.744] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:58 - [11:26:58.744] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638987164187442078"}
[11:26:58.748] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:58 - [11:26:58.748] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:58.751] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:58 - [11:26:58.751] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:58 - [ThemedForm] Using FormThemeApplier for MainForm
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:26:58 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'MainForm' in 45ms
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[11:26:58.810] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-14 11:26:58 - [11:26:58.810] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[11:26:58.812] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-14 11:26:58 - [11:26:58.812] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-14 11:26:58 - [11:26:58.812] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638987164188125126"}
[11:26:58.815] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:58 - [11:26:58.815] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:58.818] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-14 11:26:58 - [11:26:58.818] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-14 11:26:58 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:26:58 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-14 11:26:58 - [FocusUtils] Attached TextChanged to TextBox: Control_InventoryTab_TextBox_Part
[11:26:58.828] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (15ms) - Status: 1
2025-11-14 11:26:58 - [11:26:58.828] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (15ms) - Status: 1
2025-11-14 11:26:58 - [11:26:58.828] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":15,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[11:26:58.834] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (15ms) - 1 rows
2025-11-14 11:26:58 - [11:26:58.834] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (15ms) - 1 rows
[11:26:58.836] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (20ms)
2025-11-14 11:26:58 - [11:26:58.836] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (20ms)
[11:26:58.839] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (26ms)
2025-11-14 11:26:58 - [11:26:58.839] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (26ms)
2025-11-14 11:26:58 - [11:26:58.839] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":26,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638987164188125126","Status":"SUCCESS","RowCount":1}
[11:26:58.842] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (32ms)
2025-11-14 11:26:58 - [11:26:58.842] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (32ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[11:26:58.847] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-14 11:26:58 - [11:26:58.847] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[11:26:58.852] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-14 11:26:58 - [11:26:58.852] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[11:26:58.854] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-14 11:26:58 - [11:26:58.854] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-14 11:26:58 - Application Info - Development Menu configured for user 'JOHNK': Visible
[11:26:58.858] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (11ms)
2025-11-14 11:26:58 - [11:26:58.858] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (11ms)
[11:26:58.861] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (117ms) - Status: 1
2025-11-14 11:26:58 - [11:26:58.861] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (117ms) - Status: 1
2025-11-14 11:26:58 - [11:26:58.861] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":117,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 transaction(s) for user: JOHNK"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 transaction(s) for user: JOHNK"}
[11:26:58.865] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (117ms) - 9 rows
2025-11-14 11:26:58 - [11:26:58.865] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (117ms) - 9 rows
[11:26:58.868] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (118ms)
2025-11-14 11:26:58 - [11:26:58.868] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (118ms)
[11:26:58.870] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (126ms)
2025-11-14 11:26:58 - [11:26:58.870] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (126ms)
2025-11-14 11:26:58 - [11:26:58.870] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":126,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638987164187442078","Status":"SUCCESS","RowCount":9}
2025-11-14 11:26:58 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 0K2142 + 19 (Qty: 16)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 21-28841-006 + 19 (Qty: 500)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 21179 + 15 (Qty: 500)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 06-96408-001 + N/A (Qty: 40)
2025-11-14 11:26:58 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 10)
2025-11-14 11:26:58 - [Dao_QuickButtons] Array restructured: 9 unique buttons, 0 duplicates removed
2025-11-14 11:26:58 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-14 11:26:58 - [Dao_QuickButtons] All buttons deleted from database
2025-11-14 11:26:58 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 1: 0K2142 + 19 (Qty: 16)
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 2: 21-28841-006 + 19 (Qty: 500)
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 3: 21179 + 15 (Qty: 500)
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 4: 01-31976-000 + 10 (Qty: 1)
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 5: 04-27693-000 + 90 (Qty: 10)
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 6: 01-34578-000 + 880 (Qty: 20)
2025-11-14 11:26:58 - [Dao_QuickButtons] Created button at position 7: 03-29236-030 + 959 (Qty: 30)
2025-11-14 11:26:59 - [Dao_QuickButtons] Created button at position 8: 06-96408-001 + N/A (Qty: 40)
2025-11-14 11:26:59 - [Dao_QuickButtons] Created button at position 9: 01-33016-000 + 109 (Qty: 10)
2025-11-14 11:26:59 - [Dao_QuickButtons] Created 9 buttons in database
2025-11-14 11:26:59 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 9 buttons remain
2025-11-14 11:26:59 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-14 11:26:59 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 9 buttons remain
2025-11-14 11:26:59 - [QuickButtons] STEP 2: Loading data from database
[11:26:59.015] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:59 - [11:26:59.015] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:59 - [11:26:59.015] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638987164190156336"}
[11:26:59.019] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:26:59 - [11:26:59.019] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:26:59.021] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-14 11:26:59 - [11:26:59.021] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[11:26:59.027] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-14 11:26:59 - [11:26:59.027] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-14 11:26:59 - [11:26:59.027] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 transaction(s) for user: JOHNK"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 transaction(s) for user: JOHNK"}
[11:26:59.030] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 9 rows
2025-11-14 11:26:59 - [11:26:59.030] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 9 rows
[11:26:59.032] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-14 11:26:59 - [11:26:59.032] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[11:26:59.035] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-14 11:26:59 - [11:26:59.035] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-14 11:26:59 - [11:26:59.035] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638987164190156336","Status":"SUCCESS","RowCount":9}
[11:26:59.041] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-14 11:26:59 - [11:26:59.041] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-14 11:26:59 - [QuickButtons] STEP 2: ✓ Retrieved 9 button(s) from database
2025-11-14 11:26:59 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 1: 0K2142 + Op:19 (Qty: 16)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 2: 21-28841-006 + Op:19 (Qty: 500)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 3: 21179 + Op:15 (Qty: 500)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 4: 01-31976-000 + Op:10 (Qty: 1)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 5: 04-27693-000 + Op:90 (Qty: 10)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 6: 01-34578-000 + Op:880 (Qty: 20)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 7: 03-29236-030 + Op:959 (Qty: 30)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 8: 06-96408-001 + Op:N/A (Qty: 40)
2025-11-14 11:26:59 - [QuickButtons] STEP 3:   Button 9: 01-33016-000 + Op:109 (Qty: 10)
2025-11-14 11:26:59 - [QuickButtons] STEP 3: Filled 9 button(s) with data
2025-11-14 11:26:59 - [QuickButtons] STEP 3: Clearing remaining 1 button(s)
2025-11-14 11:26:59 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-14 11:26:59 - [QuickButtons] STEP 4: Layout refreshed - 9 visible button(s)
2025-11-14 11:26:59 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-14 11:26:59 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-14 11:26:59 - [QuickButtons] ║ User: JOHNK
2025-11-14 11:26:59 - [QuickButtons] ║ Visible Buttons: 9
2025-11-14 11:26:59 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-14 11:26:59 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-14 11:26:59 -
[11:26:59.124] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (386ms)
2025-11-14 11:26:59 - [11:26:59.124] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (386ms)
[11:26:59.126] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-14 11:26:59 - [11:26:59.126] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
[11:27:01.442] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:01 - [11:27:01.442] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:01.446] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:01 - [11:27:01.446] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:01 - [Save Button] Final: Part=False, Op=False, Loc=False, Qty=False, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:01 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:01 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[11:27:01.573] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:01 - [11:27:01.573] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:01.576] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:01 - [11:27:01.576] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:01 - [Save Button] Final: Part=False, Op=False, Loc=False, Qty=False, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:01 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:01 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 4, TimerActive: False
[11:27:02.446] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:02 - [11:27:02.446] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:02.448] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:02 - [11:27:02.448] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:02 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
[11:27:02.455] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:27:02 - [11:27:02.455] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:27:02 - [11:27:02.455] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164224551878"}
[11:27:02.458] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:02 - [11:27:02.458] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:02.461] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-14 11:27:02 - [11:27:02.461] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-14 11:27:02 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-14 11:27:02 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:02 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:02 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:02 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation - EXITING
2025-11-14 11:27:02 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-14 11:27:02 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
2025-11-14 11:27:02 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:02 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-14 11:27:02 - [FocusUtils] Attached TextChanged to TextBox: Control_InventoryTab_TextBox_Operation
[11:27:02.522] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (67ms) - Status: 1
2025-11-14 11:27:02 - [11:27:02.522] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (67ms) - Status: 1
2025-11-14 11:27:02 - [11:27:02.522] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":67,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[11:27:02.528] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (67ms) - 3745 rows
2025-11-14 11:27:02 - [11:27:02.528] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (67ms) - 3745 rows
[11:27:02.530] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
2025-11-14 11:27:02 - [11:27:02.530] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (71ms)
[11:27:02.532] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (77ms)
2025-11-14 11:27:02 - [11:27:02.532] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (77ms)
2025-11-14 11:27:02 - [11:27:02.532] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":77,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164224551878","Status":"SUCCESS","RowCount":3745}
2025-11-14 11:27:02 - [SuggestionTextBox] Overlay opened: Field=Control_InventoryTab_TextBox_Part, Matches=100, Input='21'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-14 11:27:02 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-14 11:27:02 - [SuggestionTextBox] About to show dialog
2025-11-14 11:27:02 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:27:02 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:27:02 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:27:02 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-14 11:27:02 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:27:02 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Checking if should restore normal BackColor
2025-11-14 11:27:02 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-14 11:27:02 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:27:02 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 4ms
2025-11-14 11:27:02 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-14 11:27:02 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:03 - [SuggestionOverlay] AcceptSelection called: SelectedItem='21179'
2025-11-14 11:27:03 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-14 11:27:03 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:03 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:03 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:03 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation - EXITING
2025-11-14 11:27:03 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-14 11:27:03 - [SuggestionTextBox] Captured selectedValue: '21179'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-14 11:27:03 - [SuggestionTextBox] BEFORE text assignment: Field=Control_InventoryTab_TextBox_Part, Current Text='21', Will set to='21179'
2025-11-14 11:27:03 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=False, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:03 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:03 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
2025-11-14 11:27:03 - [SuggestionTextBox] AFTER text assignment: Field=Control_InventoryTab_TextBox_Part, Text is now='21179'
2025-11-14 11:27:03 - Part suggestion selected: 21179
2025-11-14 11:27:03 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=False, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:03 - [SuggestionTextBox] Suggestion selected event raised: Field=Control_InventoryTab_TextBox_Part, Value='21179', Original='21'
2025-11-14 11:27:03 - [SuggestionTextBox] Focus moved to next control: True
2025-11-14 11:27:03 - [SuggestionTextBox] Overlay closed finally block
2025-11-14 11:27:03 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Operation
[11:27:04.546] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:04 - [11:27:04.546] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:04.549] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:04 - [11:27:04.549] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:04 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:04 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=False, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:04 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Operation - Clearing highlight
2025-11-14 11:27:04 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
[11:27:04.709] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:04 - [11:27:04.709] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:04.712] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:04 - [11:27:04.712] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:04 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:04 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=False, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:04 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Operation - Clearing highlight
2025-11-14 11:27:04 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
[11:27:04.987] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:04 - [11:27:04.987] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:04.989] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:04 - [11:27:04.989] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:04 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Checking if should restore normal BackColor
[11:27:04.998] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:27:05 - [11:27:04.998] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:27:05 - [11:27:04.998] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164249987494"}
[11:27:05.004] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:05 - [11:27:05.004] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:05.008] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:27:05 - [11:27:05.008] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:27:05 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Quantity (TextBox) - ENTERING
2025-11-14 11:27:05 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:05 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:05 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:05 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Quantity - EXITING
2025-11-14 11:27:05 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-14 11:27:05 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:05 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:05 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-14 11:27:05 - [FocusUtils] Attached TextChanged to TextBox: Control_InventoryTab_TextBox_Quantity
[11:27:05.030] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (31ms) - Status: 1
2025-11-14 11:27:05 - [11:27:05.030] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (31ms) - Status: 1
2025-11-14 11:27:05 - [11:27:05.030] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":31,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[11:27:05.033] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (31ms) - 72 rows
2025-11-14 11:27:05 - [11:27:05.033] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (31ms) - 72 rows
[11:27:05.036] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (32ms)
2025-11-14 11:27:05 - [11:27:05.036] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (32ms)
[11:27:05.039] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (40ms)
2025-11-14 11:27:05 - [11:27:05.039] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (40ms)
2025-11-14 11:27:05 - [11:27:05.039] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":40,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164249987494","Status":"SUCCESS","RowCount":72}
[11:27:05.536] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:05 - [11:27:05.536] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:05.539] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:05 - [11:27:05.539] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:05 - Inventory Quantity TextBox changed.
2025-11-14 11:27:05 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:05 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:05 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Quantity - Clearing highlight
2025-11-14 11:27:05 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Quantity
[11:27:05.704] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:05 - [11:27:05.704] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:05.706] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:05 - [11:27:05.706] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:05 - Inventory Quantity TextBox changed.
2025-11-14 11:27:05 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:05 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:05 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Quantity - Clearing highlight
2025-11-14 11:27:05 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Quantity
[11:27:05.895] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:05 - [11:27:05.895] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:05.897] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:05 - [11:27:05.897] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:05 - Inventory Quantity TextBox changed.
2025-11-14 11:27:05 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:05 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:05 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Quantity - Clearing highlight
2025-11-14 11:27:05 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Quantity
[11:27:06.017] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:06 - [11:27:06.017] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:06.019] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [11:27:06.019] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Quantity - Checking if should restore normal BackColor
2025-11-14 11:27:06 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - ENTERING
2025-11-14 11:27:06 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:06 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Location
2025-11-14 11:27:06 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:06 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Location - EXITING
2025-11-14 11:27:06 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Quantity - Control not focused, restoring normal BackColor
2025-11-14 11:27:06 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:06 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:06 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-14 11:27:06 - [FocusUtils] Attached TextChanged to TextBox: Control_InventoryTab_TextBox_Location
[11:27:06.495] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:06 - [11:27:06.495] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:06.497] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [11:27:06.497] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:06 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:06 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Location - Clearing highlight
2025-11-14 11:27:06 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
[11:27:06.670] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:06 - [11:27:06.670] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:06.672] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [11:27:06.672] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:06 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:06 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Location - Clearing highlight
2025-11-14 11:27:06 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
[11:27:06.838] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:06 - [11:27:06.838] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:06.841] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:06 - [11:27:06.841] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[11:27:08.048] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:08 - [11:27:08.048] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:08.051] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:08 - [11:27:08.051] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:08 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:08 - [Save Button] Final: Part=True, Op=False, Loc=False, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:08 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Location - Clearing highlight
2025-11-14 11:27:08 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:08 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Location - Checking if should restore normal BackColor
[11:27:08.063] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:27:08 - [11:27:08.063] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:27:08 - [11:27:08.063] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164280634624"}
[11:27:08.066] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:08 - [11:27:08.066] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:08.069] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-14 11:27:08 - [11:27:08.069] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-14 11:27:08 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Location - Control not focused, restoring normal BackColor
2025-11-14 11:27:08 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
[11:27:08.111] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (47ms) - Status: 1
2025-11-14 11:27:08 - [11:27:08.111] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (47ms) - Status: 1
2025-11-14 11:27:08 - [11:27:08.111] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":47,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[11:27:08.116] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (47ms) - 10371 rows
2025-11-14 11:27:08 - [11:27:08.116] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (47ms) - 10371 rows
[11:27:08.118] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
2025-11-14 11:27:08 - [11:27:08.118] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
[11:27:08.120] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (57ms)
2025-11-14 11:27:08 - [11:27:08.120] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (57ms)
2025-11-14 11:27:08 - [11:27:08.120] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":57,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164280634624","Status":"SUCCESS","RowCount":10371}
2025-11-14 11:27:08 - [SuggestionTextBox] Overlay opened: Field=Control_InventoryTab_TextBox_Location, Matches=30, Input='DD'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-14 11:27:08 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-14 11:27:08 - [SuggestionTextBox] About to show dialog
2025-11-14 11:27:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:27:08 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:27:08 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:27:08 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-14 11:27:08 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:27:08 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-14 11:27:08 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:27:08 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-14 11:27:08 - [SuggestionOverlay] AcceptSelection called: SelectedItem='DD-A0-02'
2025-11-14 11:27:09 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-14 11:27:09 - [SuggestionTextBox] Captured selectedValue: 'DD-A0-02'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-14 11:27:09 - [SuggestionTextBox] BEFORE text assignment: Field=Control_InventoryTab_TextBox_Location, Current Text='DD', Will set to='DD-A0-02'
2025-11-14 11:27:09 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:09 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:09 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Location - Clearing highlight
2025-11-14 11:27:09 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:09 - [SuggestionTextBox] AFTER text assignment: Field=Control_InventoryTab_TextBox_Location, Text is now='DD-A0-02'
2025-11-14 11:27:09 - Location suggestion selected: DD-A0-02
2025-11-14 11:27:09 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:09 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:09 - [SuggestionTextBox] Suggestion selected event raised: Field=Control_InventoryTab_TextBox_Location, Value='DD-A0-02', Original='DD'
2025-11-14 11:27:09 - [SuggestionTextBox] Focus moved to next control: True
2025-11-14 11:27:09 - [SuggestionTextBox] Overlay closed finally block
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
Running VersionChecker...
2025-11-14 11:27:25 - Running VersionChecker - checking database version information.
[11:27:25.449] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:27:25 - [11:27:25.449] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:27:25 - [11:27:25.449] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987164454498864"}
[11:27:25.453] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:25 - [11:27:25.453] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:25.456] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-14 11:27:25 - [11:27:25.456] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[11:27:25.459] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (9ms) - Status: 1
2025-11-14 11:27:25 - [11:27:25.459] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (9ms) - Status: 1
2025-11-14 11:27:25 - [11:27:25.459] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":33,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[11:27:25.474] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (9ms) - 1 rows
2025-11-14 11:27:25 - [11:27:25.474] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (9ms) - 1 rows
[11:27:25.477] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (23ms)
2025-11-14 11:27:25 - [11:27:25.477] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (23ms)
[11:27:25.483] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (33ms)
2025-11-14 11:27:25 - [11:27:25.483] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (33ms)
2025-11-14 11:27:25 - [11:27:25.483] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":33,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987164454498864","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-14 11:27:25 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 4, TimerActive: False
[11:27:28.838] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:28 - [11:27:28.838] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:28.841] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:28 - [11:27:28.841] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:28.853] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:28 - [11:27:28.853] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:28.855] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:28 - [11:27:28.855] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[11:27:32.348] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:32 - [11:27:32.348] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:32.350] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:32 - [11:27:32.350] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:32.430] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:32 - [11:27:32.430] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:32.432] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:32 - [11:27:32.432] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-14 11:27:39 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-14 11:27:39 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Part
2025-11-14 11:27:39 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Part
2025-11-14 11:27:39 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Part
2025-11-14 11:27:39 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part - EXITING
2025-11-14 11:27:39 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Part
[11:27:39.999] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:40 - [11:27:39.999] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:40.003] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (4ms)
2025-11-14 11:27:40 - [11:27:40.003] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (4ms)
[11:27:40.185] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:40 - [11:27:40.185] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:40.187] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:40 - [11:27:40.187] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:40 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:40 - [Save Button] Final: Part=False, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:40 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:40 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[11:27:41.378] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:41 - [11:27:41.378] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:41.381] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:41 - [11:27:41.381] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:41 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:41 - [Save Button] Final: Part=False, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:41 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[11:27:41.605] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:41 - [11:27:41.605] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:41.607] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:41 - [11:27:41.607] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:41 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:41 - [Save Button] Final: Part=False, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:41 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[11:27:41.723] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:41 - [11:27:41.723] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:41.725] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:41 - [11:27:41.725] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:41 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
[11:27:41.730] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:27:41 - [11:27:41.730] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-14 11:27:41 - [11:27:41.730] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164617302035"}
[11:27:41.733] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:41 - [11:27:41.733] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:41.735] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-14 11:27:41 - [11:27:41.735] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-14 11:27:41 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-14 11:27:41 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:41 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:41 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:41 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation - EXITING
2025-11-14 11:27:41 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-14 11:27:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
2025-11-14 11:27:41 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Operation
[11:27:41.773] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (42ms) - Status: 1
2025-11-14 11:27:41 - [11:27:41.773] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (42ms) - Status: 1
2025-11-14 11:27:41 - [11:27:41.773] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":42,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[11:27:41.777] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (42ms) - 3745 rows
2025-11-14 11:27:41 - [11:27:41.777] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (42ms) - 3745 rows
[11:27:41.779] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (46ms)
2025-11-14 11:27:41 - [11:27:41.779] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (46ms)
[11:27:41.782] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (52ms)
2025-11-14 11:27:41 - [11:27:41.782] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (52ms)
2025-11-14 11:27:41 - [11:27:41.782] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":52,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638987164617302035","Status":"SUCCESS","RowCount":3745}
2025-11-14 11:27:41 - [SuggestionTextBox] Overlay opened: Field=Control_InventoryTab_TextBox_Part, Matches=4, Input='A11'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-14 11:27:41 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-14 11:27:41 - [SuggestionTextBox] About to show dialog
2025-11-14 11:27:41 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-14 11:27:41 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-14 11:27:41 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:27:41 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-14 11:27:41 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-14 11:27:41 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Checking if should restore normal BackColor
[11:27:41.805] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:27:41 - [11:27:41.805] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:27:41 - [11:27:41.805] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164618052981"}
[11:27:41.808] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:41 - [11:27:41.808] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:41.810] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:27:41 - [11:27:41.810] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:27:41 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-14 11:27:41 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-14 11:27:41 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-14 11:27:41 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-14 11:27:41 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
[11:27:41.828] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (23ms) - Status: 1
2025-11-14 11:27:41 - [11:27:41.828] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (23ms) - Status: 1
2025-11-14 11:27:41 - [11:27:41.828] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":23,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[11:27:41.831] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (23ms) - 72 rows
2025-11-14 11:27:41 - [11:27:41.831] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (23ms) - 72 rows
[11:27:41.834] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
2025-11-14 11:27:41 - [11:27:41.834] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
[11:27:41.836] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (30ms)
2025-11-14 11:27:41 - [11:27:41.836] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (30ms)
2025-11-14 11:27:41 - [11:27:41.836] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":30,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164618052981","Status":"SUCCESS","RowCount":72}
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-14 11:27:42 - [SuggestionOverlay] AcceptSelection called: SelectedItem='A110146'
2025-11-14 11:27:42 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - ENTERING
2025-11-14 11:27:42 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:42 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:42 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:42 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Operation - EXITING
2025-11-14 11:27:42 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-14 11:27:42 - [SuggestionTextBox] Captured selectedValue: 'A110146'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-14 11:27:42 - [SuggestionTextBox] BEFORE text assignment: Field=Control_InventoryTab_TextBox_Part, Current Text='A11', Will set to='A110146'
2025-11-14 11:27:42 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:42 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
2025-11-14 11:27:42 - [FocusUtils] TextBox_TextChanged_Handler: Control_InventoryTab_TextBox_Part - Clearing highlight
2025-11-14 11:27:42 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
2025-11-14 11:27:42 - [SuggestionTextBox] AFTER text assignment: Field=Control_InventoryTab_TextBox_Part, Text is now='A110146'
2025-11-14 11:27:42 - Part suggestion selected: A110146
2025-11-14 11:27:42 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:42 - [Save Button] ColorCode validation FAILED: empty (required)
2025-11-14 11:27:42 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:27:42 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=False, WO=False => Save.Enabled=False
2025-11-14 11:27:42 - [SuggestionTextBox] Suggestion selected event raised: Field=Control_InventoryTab_TextBox_Part, Value='A110146', Original='A11'
2025-11-14 11:27:42 - [SuggestionTextBox] Focus moved to next control: True
2025-11-14 11:27:43 - [SuggestionTextBox] Overlay closed finally block
2025-11-14 11:27:43 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Operation
[11:27:43.582] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:43 - [11:27:43.582] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:43.585] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:43 - [11:27:43.585] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:43 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Checking if should restore normal BackColor
[11:27:43.590] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:27:43 - [11:27:43.590] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-14 11:27:43 - [11:27:43.590] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164635907792"}
[11:27:43.594] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:43 - [11:27:43.594] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:43.597] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:27:43 - [11:27:43.597] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-14 11:27:43 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Quantity (TextBox) - ENTERING
2025-11-14 11:27:43 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:43 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:43 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:43 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Quantity - EXITING
2025-11-14 11:27:43 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-14 11:27:43 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
2025-11-14 11:27:43 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Quantity
[11:27:43.616] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (26ms) - Status: 1
2025-11-14 11:27:43 - [11:27:43.616] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (26ms) - Status: 1
2025-11-14 11:27:43 - [11:27:43.616] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":26,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[11:27:43.620] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (26ms) - 72 rows
2025-11-14 11:27:43 - [11:27:43.620] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (26ms) - 72 rows
[11:27:43.622] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (28ms)
2025-11-14 11:27:43 - [11:27:43.622] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (28ms)
[11:27:43.624] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (34ms)
2025-11-14 11:27:43 - [11:27:43.624] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (34ms)
2025-11-14 11:27:43 - [11:27:43.624] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":34,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638987164635907792","Status":"SUCCESS","RowCount":72}
[11:27:43.789] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:43 - [11:27:43.789] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:43.791] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:43 - [11:27:43.791] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:43 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Quantity - Checking if should restore normal BackColor
2025-11-14 11:27:43 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - ENTERING
2025-11-14 11:27:43 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:43 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Location
2025-11-14 11:27:43 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Location
2025-11-14 11:27:43 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Location - EXITING
2025-11-14 11:27:43 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Quantity - Control not focused, restoring normal BackColor
2025-11-14 11:27:43 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Quantity
2025-11-14 11:27:43 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Location
[11:27:43.982] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:43 - [11:27:43.982] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:43.984] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:43 - [11:27:43.984] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:43 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Location - Checking if should restore normal BackColor
[11:27:43.988] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:27:43 - [11:27:43.988] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-14 11:27:43 - [11:27:43.988] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164639881995"}
[11:27:43.992] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:43 - [11:27:43.992] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:43.995] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-14 11:27:43 - [11:27:43.995] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-14 11:27:44 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Location - Control not focused, restoring normal BackColor
2025-11-14 11:27:44 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
[11:27:44.030] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (41ms) - Status: 1
2025-11-14 11:27:44 - [11:27:44.030] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (41ms) - Status: 1
2025-11-14 11:27:44 - [11:27:44.030] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":41,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[11:27:44.034] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (41ms) - 10371 rows
2025-11-14 11:27:44 - [11:27:44.034] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (41ms) - 10371 rows
[11:27:44.036] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (43ms)
2025-11-14 11:27:44 - [11:27:44.036] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (43ms)
[11:27:44.038] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (50ms)
2025-11-14 11:27:44 - [11:27:44.038] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (50ms)
2025-11-14 11:27:44 - [11:27:44.038] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":50,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638987164639881995","Status":"SUCCESS","RowCount":10371}
[11:27:44.484] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:44 - [11:27:44.484] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:44.487] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:44 - [11:27:44.487] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:44 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:44 - [Save Button] ColorCode validation 'r': FAILED (cache has 10 colors)
2025-11-14 11:27:44 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:27:44 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=False, WO=False => Save.Enabled=False
[11:27:44.805] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:44 - [11:27:44.805] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:44.807] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:44 - [11:27:44.807] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:44 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:44 - [Save Button] ColorCode validation 're': FAILED (cache has 10 colors)
2025-11-14 11:27:44 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:27:44 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=False, WO=False => Save.Enabled=False
[11:27:45.014] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:45 - [11:27:45.014] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:45.016] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:45 - [11:27:45.016] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:45 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:45 - [Save Button] ColorCode validation 'red': PASSED (cache has 10 colors)
2025-11-14 11:27:45 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:27:45 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:45.451] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:45 - [11:27:45.451] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:45.454] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:45 - [11:27:45.454] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:45 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:45 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:45 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:27:45 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:45.466] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-14 11:27:45 - [11:27:45.466] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-14 11:27:45 - [11:27:45.466] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638987164654666665"}
[11:27:45.469] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:45 - [11:27:45.469] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:45.472] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-14 11:27:45 - [11:27:45.472] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[11:27:45.480] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (14ms) - Status: 1
2025-11-14 11:27:45 - [11:27:45.480] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (14ms) - Status: 1
2025-11-14 11:27:45 - [11:27:45.480] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":14,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[11:27:45.484] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (14ms) - 10 rows
2025-11-14 11:27:45 - [11:27:45.484] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (14ms) - 10 rows
[11:27:45.486] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-14 11:27:45 - [11:27:45.486] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[11:27:45.488] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (21ms)
2025-11-14 11:27:45 - [11:27:45.488] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (21ms)
2025-11-14 11:27:45 - [11:27:45.488] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":21,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638987164654666665","Status":"SUCCESS","RowCount":10}
[11:27:47.035] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:47 - [11:27:47.035] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:47.037] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:47 - [11:27:47.037] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:47 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:47 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:47 - [Save Button] WorkOrder validation 'w': FAILED
2025-11-14 11:27:47 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[11:27:47.797] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:47 - [11:27:47.797] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:47.799] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:47 - [11:27:47.799] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:47 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:47 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:47 - [Save Button] WorkOrder validation 'w0': FAILED
2025-11-14 11:27:47 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:48.886] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:48 - [11:27:48.886] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:48.888] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:48 - [11:27:48.888] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:48 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:48 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:48 - [Save Button] WorkOrder validation 'w0-': FAILED
2025-11-14 11:27:48 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:49.095] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:49 - [11:27:49.095] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:49.098] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [11:27:49.098] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:49 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:49 - [Save Button] WorkOrder validation 'w0-0': FAILED
2025-11-14 11:27:49 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:49.374] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:49 - [11:27:49.374] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:49.377] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [11:27:49.377] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:49 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:49 - [Save Button] WorkOrder validation 'w0-06': FAILED
2025-11-14 11:27:49 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:49.568] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:49 - [11:27:49.568] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:49.571] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [11:27:49.571] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:49 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:49 - [Save Button] WorkOrder validation 'w0-065': FAILED
2025-11-14 11:27:49 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:49.861] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:49 - [11:27:49.861] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:49.864] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [11:27:49.864] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:49 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:49 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:49 - [Save Button] WorkOrder validation 'w0-0651': FAILED
2025-11-14 11:27:49 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:50.127] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:50 - [11:27:50.127] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:50.129] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:50 - [11:27:50.129] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:50 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:50 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:50 - [Save Button] WorkOrder validation 'w0-06515': FAILED
2025-11-14 11:27:50 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:50.348] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:50 - [11:27:50.348] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:50.351] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:50 - [11:27:50.351] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:50 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:50 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:50 - [Save Button] WorkOrder validation 'w0-065152': FAILED
2025-11-14 11:27:50 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:50.591] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:50 - [11:27:50.591] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:50.593] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:50 - [11:27:50.593] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:50 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:50 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:50 - [Save Button] WorkOrder validation 'w0-0651525': FAILED
2025-11-14 11:27:50 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:51.355] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:51 - [11:27:51.355] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:51.357] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:51 - [11:27:51.357] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:51 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:27:51 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:27:51 - [Save Button] WorkOrder validation 'w0-065152': FAILED
2025-11-14 11:27:51 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:27:51.593] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:51 - [11:27:51.593] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:51.595] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-14 11:27:51 - [11:27:51.595] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-14 11:27:51 - Warning dialog shown: Warning - Invalid work order format. Enter 5-6 digit number or WO-######
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[11:27:53.938] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:53 - [11:27:53.938] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:53.940] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:53 - [11:27:53.940] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
Running VersionChecker...
2025-11-14 11:27:55 - Running VersionChecker - checking database version information.
[11:27:55.443] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:27:55 - [11:27:55.443] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:27:55 - [11:27:55.443] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987164754430487"}
[11:27:55.447] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:27:55 - [11:27:55.447] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:27:55.450] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-14 11:27:55 - [11:27:55.450] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[11:27:55.457] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (13ms) - Status: 1
2025-11-14 11:27:55 - [11:27:55.457] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (13ms) - Status: 1
2025-11-14 11:27:55 - [11:27:55.457] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":13,"Thread":31,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[11:27:55.460] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (13ms) - 1 rows
2025-11-14 11:27:55 - [11:27:55.460] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (13ms) - 1 rows
[11:27:55.463] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
2025-11-14 11:27:55 - [11:27:55.463] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
[11:27:55.465] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (22ms)
2025-11-14 11:27:55 - [11:27:55.465] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (22ms)
2025-11-14 11:27:55 - [11:27:55.465] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":22,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987164754430487","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-14 11:27:55 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[11:27:57.316] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:57 - [11:27:57.316] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:57.318] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:57 - [11:27:57.318] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:57.325] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:57 - [11:27:57.325] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:57.327] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:57 - [11:27:57.327] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:57.334] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:57 - [11:27:57.334] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:57.336] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:57 - [11:27:57.336] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:57.431] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:57 - [11:27:57.431] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:57.433] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:57 - [11:27:57.433] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:57.439] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:57 - [11:27:57.439] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:57.442] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:57 - [11:27:57.442] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:27:57.448] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:27:57 - [11:27:57.448] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:27:57.451] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:27:57 - [11:27:57.451] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:28:00.138] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:00 - [11:28:00.138] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:00.141] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [11:28:00.141] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:00 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:00 - [Save Button] WorkOrder validation 'w0-': FAILED
2025-11-14 11:28:00 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:00.342] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:00 - [11:28:00.342] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:00.344] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [11:28:00.344] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:00 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:00 - [Save Button] WorkOrder validation 'w0': FAILED
2025-11-14 11:28:00 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:00.499] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:00 - [11:28:00.499] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:00.501] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [11:28:00.501] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:00 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:00 - [Save Button] WorkOrder validation 'w': FAILED
2025-11-14 11:28:00 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:00.664] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:00 - [11:28:00.664] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:00.667] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [11:28:00.667] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:00 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:00 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:28:00 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:00.819] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:00 - [11:28:00.819] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:00.821] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:00 - [11:28:00.821] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:28:01.100] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:01 - [11:28:01.100] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:01.103] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
2025-11-14 11:28:01 - [11:28:01.103] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
[11:28:01.109] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:01 - [11:28:01.109] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:01.112] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
2025-11-14 11:28:01 - [11:28:01.112] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
[11:28:01.117] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:01 - [11:28:01.117] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:01.119] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:01 - [11:28:01.119] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:01 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:01 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:01 - [Save Button] WorkOrder validation 'W': FAILED
2025-11-14 11:28:01 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:01.967] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:01 - [11:28:01.967] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:01.969] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:01 - [11:28:01.969] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:01 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:01 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:01 - [Save Button] WorkOrder validation FAILED: empty (required)
2025-11-14 11:28:01 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[11:28:02.337] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:02 - [11:28:02.337] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:02.340] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
2025-11-14 11:28:02 - [11:28:02.340] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
2025-11-14 11:28:02 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:02 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:02 - [Save Button] WorkOrder validation 'w': FAILED
2025-11-14 11:28:02 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:02.456] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:02 - [11:28:02.456] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:02.458] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:02 - [11:28:02.458] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:02 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:02 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:02 - [Save Button] WorkOrder validation 'wo': FAILED
2025-11-14 11:28:02 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:03.075] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:03 - [11:28:03.075] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:03.078] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [11:28:03.078] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:03 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:03 - [Save Button] WorkOrder validation 'wo-': FAILED
2025-11-14 11:28:03 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=False => Save.Enabled=False
[11:28:03.310] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:03 - [11:28:03.310] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:03.312] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [11:28:03.312] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:03 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:03 - [Save Button] WorkOrder validation 'wo-0': PASSED
2025-11-14 11:28:03 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:03.533] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:03 - [11:28:03.533] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:03.535] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [11:28:03.535] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:03 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:03 - [Save Button] WorkOrder validation 'wo-06': PASSED
2025-11-14 11:28:03 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:03.730] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:03 - [11:28:03.730] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:03.732] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [11:28:03.732] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:03 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:03 - [Save Button] WorkOrder validation 'wo-065': PASSED
2025-11-14 11:28:03 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:03.935] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:03 - [11:28:03.935] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:03.937] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [11:28:03.937] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:03 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:03 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:03 - [Save Button] WorkOrder validation 'wo-0651': PASSED
2025-11-14 11:28:03 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:04.109] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:04 - [11:28:04.109] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:04.111] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:04 - [11:28:04.111] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:04 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:04 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:04 - [Save Button] WorkOrder validation 'wo-06515': PASSED
2025-11-14 11:28:04 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:04.348] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:04 - [11:28:04.348] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:04.350] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:04 - [11:28:04.350] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:04 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:04 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:04 - [Save Button] WorkOrder validation 'wo-065152': PASSED
2025-11-14 11:28:04 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:04.634] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:04 - [11:28:04.634] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:04.636] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:04 - [11:28:04.636] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:04 - [Helper_UI_SuggestionBoxes] OperationNumber column not found in Operations_DataTable
2025-11-14 11:28:04 - [Save Button] ColorCode validation 'RED': PASSED (cache has 10 colors)
2025-11-14 11:28:04 - [Save Button] WorkOrder validation 'WO-065152': PASSED
2025-11-14 11:28:04 - [Save Button] Final: Part=True, Op=False, Loc=True, Qty=True, Color=True, WO=True => Save.Enabled=False
[11:28:07.008] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:07 - [11:28:07.008] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:07.010] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-14 11:28:07 - [11:28:07.010] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[11:28:07.022] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-14 11:28:07 - [11:28:07.022] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:28:07.024] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-14 11:28:07 - [11:28:07.024] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
Running VersionChecker...
2025-11-14 11:28:25 - Running VersionChecker - checking database version information.
[11:28:25.455] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:28:25 - [11:28:25.455] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:28:25 - [11:28:25.455] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987165054555695"}
[11:28:25.461] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:28:25 - [11:28:25.461] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:28:25.465] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-14 11:28:25 - [11:28:25.465] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[11:28:25.475] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (19ms) - Status: 1
2025-11-14 11:28:25 - [11:28:25.475] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (19ms) - Status: 1
2025-11-14 11:28:25 - [11:28:25.475] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":19,"Thread":24,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[11:28:25.481] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (19ms) - 1 rows
2025-11-14 11:28:25 - [11:28:25.481] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (19ms) - 1 rows
[11:28:25.490] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (29ms)
2025-11-14 11:28:25 - [11:28:25.490] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (29ms)
[11:28:25.494] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (39ms)
2025-11-14 11:28:25 - [11:28:25.494] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (39ms)
2025-11-14 11:28:25 - [11:28:25.494] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":39,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987165054555695","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-14 11:28:25 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 0, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 0, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 0, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 0, TimerActive: False
Running VersionChecker...
2025-11-14 11:28:55 - Running VersionChecker - checking database version information.
[11:28:55.442] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:28:55 - [11:28:55.442] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-14 11:28:55 - [11:28:55.442] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987165354422582"}
[11:28:55.448] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-14 11:28:55 - [11:28:55.448] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:28:55.451] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-14 11:28:55 - [11:28:55.451] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[11:28:55.463] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (21ms) - Status: 1
2025-11-14 11:28:55 - [11:28:55.463] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (21ms) - Status: 1
2025-11-14 11:28:55 - [11:28:55.463] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":21,"Thread":6,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[11:28:55.466] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (21ms) - 1 rows
2025-11-14 11:28:55 - [11:28:55.466] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (21ms) - 1 rows
[11:28:55.468] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (20ms)
2025-11-14 11:28:55 - [11:28:55.468] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (20ms)
[11:28:55.470] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (28ms)
2025-11-14 11:28:55 - [11:28:55.470] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (28ms)
2025-11-14 11:28:55 - [11:28:55.470] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":28,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638987165354422582","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-14 11:28:55 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
