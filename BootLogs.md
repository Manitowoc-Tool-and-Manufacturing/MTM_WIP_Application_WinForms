------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[20:20:03.432] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-15 20:20:03 - [20:20:03.432] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[20:20:03.468] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-15 20:20:03 - [20:20:03.468] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[20:20:03.471] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-15 20:20:03 - [20:20:03.471] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[20:20:03.473] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-15 20:20:03 - [20:20:03.473] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-15 20:20:03 - [Startup] Application initialization started
2025-11-15 20:20:03 - [Startup] User identified: JOHNK
2025-11-15 20:20:03 - [Dao_System] Checking database connectivity
[20:20:03.507] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-15 20:20:03 - [20:20:03.507] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-15 20:20:03 - [20:20:03.507] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638988348035068275"}
[20:20:03.567] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:03 - [20:20:03.567] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:03.568] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-15 20:20:03 - [20:20:03.568] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[20:20:03.747] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (240ms) - Status: 1
2025-11-15 20:20:03 - [20:20:03.747] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (240ms) - Status: 1
2025-11-15 20:20:03 - [20:20:03.747] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":240,"Thread":15,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[20:20:03.761] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (240ms) - 9 rows
2025-11-15 20:20:03 - [20:20:03.761] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (240ms) - 9 rows
[20:20:03.764] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (196ms)
2025-11-15 20:20:03 - [20:20:03.764] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (196ms)
[20:20:03.766] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (259ms)
2025-11-15 20:20:03 - [20:20:03.766] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (259ms)
2025-11-15 20:20:03 - [20:20:03.766] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":259,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638988348035068275","Status":"SUCCESS","RowCount":9}
2025-11-15 20:20:03 - [Dao_System] Database connectivity check passed
2025-11-15 20:20:03 - [Startup] Database connectivity validated successfully
2025-11-15 20:20:03 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-15 20:20:03 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-15 20:20:03 - [Startup] Parameter cache populated: 120 procedures, 536 total parameters
2025-11-15 20:20:03 - [Startup] Parameter prefix cache initialized successfully in 13ms. Cached 120 stored procedures.
[Startup] Parameter cache: 120 procedures cached in 13ms
[20:20:03.790] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-15 20:20:03 - [20:20:03.790] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-15 20:20:03 - [20:20:03.790] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638988348037906076"}
[20:20:03.793] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:03 - [20:20:03.793] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:03.794] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-15 20:20:03 - [20:20:03.794] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-15 20:20:03 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
2025-11-15 20:20:03 - [Startup] Initializing dependency injection container
2025-11-15 20:20:03 - [Startup] Dependency injection container configured successfully
2025-11-15 20:20:03 - [Startup] Dependency injection container initialized successfully
2025-11-15 20:20:03 - [Splash] Initializing splash screen
[20:20:03.836] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (45ms) - Status: 1
2025-11-15 20:20:03 - [20:20:03.836] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (45ms) - Status: 1
2025-11-15 20:20:03 - [20:20:03.836] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":45,"Thread":8,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user access type(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user access type(s)"}
[20:20:03.838] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (45ms) - 88 rows
2025-11-15 20:20:03 - [20:20:03.838] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (45ms) - 88 rows
[20:20:03.840] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
2025-11-15 20:20:03 - [20:20:03.840] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (47ms)
[20:20:03.842] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (51ms)
2025-11-15 20:20:03 - [20:20:03.842] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (51ms)
2025-11-15 20:20:03 - [20:20:03.842] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_GetUserAccessType","ElapsedMs":51,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638988348037906076","Status":"SUCCESS","RowCount":88}
2025-11-15 20:20:03 - System_UserAccessType executed successfully for user: JOHNK
[20:20:03.864] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-15 20:20:03 - [20:20:03.864] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[20:20:03.866] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-15 20:20:03 - [20:20:03.866] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-15 20:20:03 - [ThemedUserControl] Control_ProgressBarUserControl initialized with automatic theme support
[20:20:03.923] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-15 20:20:03 - [20:20:03.923] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[20:20:03.953] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-15 20:20:03 - [20:20:03.953] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[20:20:03.955] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-15 20:20:03 - [20:20:03.955] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[20:20:03.956] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-15 20:20:03 - [20:20:03.956] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[20:20:03.958] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (94ms)
2025-11-15 20:20:03 - [20:20:03.958] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (94ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-15 20:20:03 - [ThemedUserControl] Using applier for _progressControl
2025-11-15 20:20:03 - [FormThemeApplier] Applying to '_progressControl' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-15 20:20:03 - [FormThemeApplier] Applying to '_progressControl' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-15 20:20:03 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:03 - [FocusUtils] CanControlReceiveFocus:  (PictureBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:03 - [FocusUtils] ApplyFocusEventHandling:  (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:03 - [FocusUtils] CanControlReceiveFocus:  (ProgressBar) - FALSE (control type cannot receive focus)
2025-11-15 20:20:03 - [FocusUtils] ApplyFocusEventHandling:  (ProgressBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:03 - [FocusUtils] CanControlReceiveFocus:  (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:03 - [FocusUtils] ApplyFocusEventHandling:  (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:04 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-15-2025 @ 8-20 PM_normal.csv
2025-11-15 20:20:04 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-15 20:20:04 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-15 20:20:04 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-15 20:20:04 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-15 20:20:04 - [Splash] Starting async database connectivity verification
2025-11-15 20:20:04 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-15 20:20:04 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[20:20:04.408] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-15 20:20:04 - [20:20:04.408] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-15 20:20:04 - [20:20:04.408] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638988348044086676"}
[20:20:04.412] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.412] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.414] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-15 20:20:04 - [20:20:04.414] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[20:20:04.473] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (65ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.473] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (65ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.473] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":65,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3747 part(s)"},"ResultData":"DataTable[3747 rows]","ErrorMessage":"Retrieved 3747 part(s)"}
[20:20:04.476] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (65ms) - 3747 rows
2025-11-15 20:20:04 - [20:20:04.476] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (65ms) - 3747 rows
[20:20:04.478] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (65ms)
2025-11-15 20:20:04 - [20:20:04.478] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (65ms)
[20:20:04.480] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (71ms)
2025-11-15 20:20:04 - [20:20:04.480] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (71ms)
2025-11-15 20:20:04 - [20:20:04.480] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":71,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638988348044086676","Status":"SUCCESS","RowCount":3747}
2025-11-15 20:20:04 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-15 20:20:04 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), RequiresColorCode(Boolean), ItemType(String), Operations(String)
2025-11-15 20:20:04 - [DataTable] ComboBoxPart: Target schema:
2025-11-15 20:20:04 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[20:20:04.506] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-15 20:20:04 - [20:20:04.506] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-15 20:20:04 - [20:20:04.506] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638988348045060375"}
[20:20:04.508] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.508] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.510] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-15 20:20:04 - [20:20:04.510] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[20:20:04.542] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (36ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.542] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (36ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.542] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[20:20:04.545] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (36ms) - 72 rows
2025-11-15 20:20:04 - [20:20:04.545] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (36ms) - 72 rows
[20:20:04.548] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-15 20:20:04 - [20:20:04.548] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[20:20:04.550] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (44ms)
2025-11-15 20:20:04 - [20:20:04.550] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (44ms)
2025-11-15 20:20:04 - [20:20:04.550] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":44,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638988348045060375","Status":"SUCCESS","RowCount":72}
2025-11-15 20:20:04 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-15 20:20:04 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-15 20:20:04 - [DataTable] ComboBoxOperation: Target schema:
2025-11-15 20:20:04 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[20:20:04.561] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-15 20:20:04 - [20:20:04.561] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-15 20:20:04 - [20:20:04.561] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638988348045619883"}
[20:20:04.564] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.564] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.566] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-15 20:20:04 - [20:20:04.566] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[20:20:04.642] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (80ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.642] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (80ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.642] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":80,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[20:20:04.647] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (80ms) - 10371 rows
2025-11-15 20:20:04 - [20:20:04.647] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (80ms) - 10371 rows
[20:20:04.649] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (84ms)
2025-11-15 20:20:04 - [20:20:04.649] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (84ms)
[20:20:04.651] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (89ms)
2025-11-15 20:20:04 - [20:20:04.651] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (89ms)
2025-11-15 20:20:04 - [20:20:04.651] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":89,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638988348045619883","Status":"SUCCESS","RowCount":10371}
2025-11-15 20:20:04 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-15 20:20:04 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-15 20:20:04 - [DataTable] ComboBoxLocation: Target schema:
2025-11-15 20:20:04 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[20:20:04.668] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-15 20:20:04 - [20:20:04.668] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-15 20:20:04 - [20:20:04.668] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638988348046686860"}
[20:20:04.671] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.671] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.673] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-15 20:20:04 - [20:20:04.673] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[20:20:04.704] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.704] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.704] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[20:20:04.707] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
2025-11-15 20:20:04 - [20:20:04.707] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
[20:20:04.709] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-15 20:20:04 - [20:20:04.709] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[20:20:04.711] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-15 20:20:04 - [20:20:04.711] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-15 20:20:04 - [20:20:04.711] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638988348046686860","Status":"SUCCESS","RowCount":88}
2025-11-15 20:20:04 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-15 20:20:04 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-15 20:20:04 - [DataTable] ComboBoxUser: Target schema:
2025-11-15 20:20:04 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[20:20:04.720] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-15 20:20:04 - [20:20:04.720] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-15 20:20:04 - [20:20:04.720] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638988348047200668"}
[20:20:04.722] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.722] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.724] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-15 20:20:04 - [20:20:04.724] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[20:20:04.755] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (35ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.755] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (35ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.755] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 4 item type(s)"},"ResultData":"DataTable[4 rows]","ErrorMessage":"Retrieved 4 item type(s)"}
[20:20:04.758] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (35ms) - 4 rows
2025-11-15 20:20:04 - [20:20:04.758] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (35ms) - 4 rows
[20:20:04.760] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-15 20:20:04 - [20:20:04.760] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[20:20:04.762] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (42ms)
2025-11-15 20:20:04 - [20:20:04.762] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (42ms)
2025-11-15 20:20:04 - [20:20:04.762] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_item_types_Get_All","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638988348047200668","Status":"SUCCESS","RowCount":4}
2025-11-15 20:20:04 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-15 20:20:04 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-15 20:20:04 - [DataTable] ComboBoxItemType: Target schema:
2025-11-15 20:20:04 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-15 20:20:04 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 55, Status: Loading color code cache...
[20:20:04.831] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
2025-11-15 20:20:04 - [20:20:04.831] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[20:20:04.833] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-15 20:20:04 - [20:20:04.833] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-15 20:20:04 - [20:20:04.833] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638988348048337345"}
[20:20:04.836] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.836] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.838] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
2025-11-15 20:20:04 - [20:20:04.838] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[20:20:04.867] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (34ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.867] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (34ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.867] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":34,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[6 rows]"}
[20:20:04.871] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (34ms) - 6 rows
2025-11-15 20:20:04 - [20:20:04.871] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (34ms) - 6 rows
[20:20:04.873] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-15 20:20:04 - [20:20:04.873] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[20:20:04.875] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (42ms)
2025-11-15 20:20:04 - [20:20:04.875] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (42ms)
2025-11-15 20:20:04 - [20:20:04.875] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638988348048337345","Status":"SUCCESS","RowCount":6}
[20:20:04.879] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (47ms)
2025-11-15 20:20:04 - [20:20:04.879] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (47ms)
2025-11-15 20:20:04 - [Model_Application_Variables] ColorCodeParts cache loaded: 6 parts
[20:20:04.883] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-15 20:20:04 - [20:20:04.883] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-15 20:20:04 - [20:20:04.883] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638988348048836951"}
[20:20:04.886] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:04 - [20:20:04.886] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:04.888] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-15 20:20:04 - [20:20:04.888] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[20:20:04.920] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (36ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.920] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (36ms) - Status: 1
2025-11-15 20:20:04 - [20:20:04.920] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[20:20:04.923] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (36ms) - 10 rows
2025-11-15 20:20:04 - [20:20:04.923] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (36ms) - 10 rows
[20:20:04.925] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-15 20:20:04 - [20:20:04.925] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[20:20:04.927] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (43ms)
2025-11-15 20:20:04 - [20:20:04.927] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (43ms)
2025-11-15 20:20:04 - [20:20:04.927] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638988348048836951","Status":"SUCCESS","RowCount":10}
2025-11-15 20:20:04 - [Model_Application_Variables] ValidColorCodes cache loaded: 10 colors
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 58, Status: Color code cache loaded.
2025-11-15 20:20:04 - [Splash] ColorCodeParts cache loaded: 6 parts flagged
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-15 20:20:05 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-15 20:20:05 - Running VersionChecker - checking database version information.
[20:20:05.005] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-15 20:20:05 - [20:20:05.005] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-15 20:20:05 - [20:20:05.005] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638988348050057231"}
[20:20:05.008] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.008] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.010] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-15 20:20:05 - [20:20:05.010] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-15 20:20:05 - [Splash] Version checker initialized
[20:20:05.046] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (40ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.046] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (40ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.046] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":40,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[20:20:05.048] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (40ms) - 1 rows
2025-11-15 20:20:05 - [20:20:05.048] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (40ms) - 1 rows
[20:20:05.050] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (42ms)
2025-11-15 20:20:05 - [20:20:05.050] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (42ms)
[20:20:05.052] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (46ms)
2025-11-15 20:20:05 - [20:20:05.052] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (46ms)
2025-11-15 20:20:05 - [20:20:05.052] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":46,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638988348050057231","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-15 20:20:05 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-15 20:20:05 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[20:20:05.083] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-15 20:20:05 - [20:20:05.083] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-15 20:20:05 - [20:20:05.083] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638988348050831248"}
[20:20:05.085] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.085] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.087] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-15 20:20:05 - [20:20:05.087] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[20:20:05.095] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.095] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.095] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[20:20:05.097] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
2025-11-15 20:20:05 - [20:20:05.097] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
[20:20:05.100] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-15 20:20:05 - [20:20:05.100] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[20:20:05.101] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-15 20:20:05 - [20:20:05.101] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-15 20:20:05 - [20:20:05.101] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638988348050831248","Status":"SUCCESS","RowCount":9}
2025-11-15 20:20:05 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-15 20:20:05 - Successfully loaded 9 themes from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Default' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Forest' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-15 20:20:05 - [DEBUG] Urban Bloom JSON preview: {"InfoColor": "#8E44AD", "ErrorColor": "#F44336", "AccentColor": "#8E44AD", "SuccessColor": "#BA68C8", "WarningColor": "#FF9800", "FormBackColor": "#F6F0FA", "FormForeColor": "#1A1A1A", "LabelBackColor": "#F6F0FA", "LabelForeColor": "#1A1A1A", "PanelBackColor": "#F6F0FA", "PanelForeColor": "#1A1A1A", "ButtonBackColor": "#EDE3F7", "ButtonForeColor": "#1A1A1A", "ControlBackColor": "#F6F0FA", "ControlForeColor": "#1A1A1A", "ListBoxBackColor": "#FFFFFF", "ListBoxForeColor": "#1A1A1A", "PanelBorderCo
2025-11-15 20:20:05 - [DEBUG] Urban Bloom deserialized - FormBackColor: Color [A=255, R=246, G=240, B=250], FormForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:05 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-15 20:20:05 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-15 20:20:05 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[20:20:05.146] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-15 20:20:05 - [20:20:05.146] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[20:20:05.148] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-15 20:20:05 - [20:20:05.148] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:20:05.150] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.150] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.150] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348051507082"}
[20:20:05.153] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.153] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.155] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.155] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:20:05.189] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (38ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.189] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (38ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.189] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":38,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[20:20:05.193] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (38ms) - 1 rows
2025-11-15 20:20:05 - [20:20:05.193] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (38ms) - 1 rows
[20:20:05.194] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
2025-11-15 20:20:05 - [20:20:05.194] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
[20:20:05.196] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (46ms)
2025-11-15 20:20:05 - [20:20:05.196] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (46ms)
2025-11-15 20:20:05 - [20:20:05.196] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":46,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348051507082","Status":"SUCCESS","RowCount":1}
[20:20:05.205] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (56ms)
2025-11-15 20:20:05 - [20:20:05.205] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (56ms)
[20:20:05.207] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (61ms)
2025-11-15 20:20:05 - [20:20:05.207] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (61ms)
2025-11-15 20:20:05 - Theme system enabled status for user JOHNK: True
[20:20:05.211] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-15 20:20:05 - [20:20:05.211] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[20:20:05.213] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-15 20:20:05 - [20:20:05.213] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:20:05.215] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.215] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.215] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348052152127"}
[20:20:05.218] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.218] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.220] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.220] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:20:05.224] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.224] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.224] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[20:20:05.227] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-15 20:20:05 - [20:20:05.227] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[20:20:05.229] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-15 20:20:05 - [20:20:05.229] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[20:20:05.231] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-15 20:20:05 - [20:20:05.231] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-15 20:20:05 - [20:20:05.231] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348052152127","Status":"SUCCESS","RowCount":1}
[20:20:05.235] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
2025-11-15 20:20:05 - [20:20:05.235] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
[20:20:05.237] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-15 20:20:05 - [20:20:05.237] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-15 20:20:05 - Loaded theme preference for user JOHNK: Arctic
2025-11-15 20:20:05 - Set Model_Application_Variables.ThemeName to: Arctic
2025-11-15 20:20:05 - Theme system initialized for user JOHNK. Final theme: Arctic, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
2025-11-15 20:20:05 - ThemeStore loaded 9 themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-15 20:20:05 - [Splash] ThemeStore loaded from database
2025-11-15 20:20:05 - [Splash] ThemeManager initialized with 'Arctic' theme
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-15 20:20:05 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-15 20:20:05 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-15 20:20:05 - [Splash] Loading theme settings
[20:20:05.379] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-15 20:20:05 - [20:20:05.379] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[20:20:05.382] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-15 20:20:05 - [20:20:05.382] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:20:05.384] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.384] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.384] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348053842352"}
[20:20:05.387] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.387] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.390] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.390] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:20:05.395] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.395] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.395] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[20:20:05.398] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
2025-11-15 20:20:05 - [20:20:05.398] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
[20:20:05.400] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-15 20:20:05 - [20:20:05.400] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[20:20:05.402] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-15 20:20:05 - [20:20:05.402] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-15 20:20:05 - [20:20:05.402] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":17,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348053842352","Status":"SUCCESS","RowCount":1}
[20:20:05.405] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
2025-11-15 20:20:05 - [20:20:05.405] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (23ms)
[20:20:05.407] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (27ms)
2025-11-15 20:20:05 - [20:20:05.407] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (27ms)
[20:20:05.410] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-15 20:20:05 - [20:20:05.410] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[20:20:05.412] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-15 20:20:05 - [20:20:05.412] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:20:05.413] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.413] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.413] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348054138526"}
[20:20:05.418] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.418] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.420] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.420] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:20:05.425] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.425] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.425] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[20:20:05.428] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
2025-11-15 20:20:05 - [20:20:05.428] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
[20:20:05.430] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-15 20:20:05 - [20:20:05.430] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[20:20:05.432] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-15 20:20:05 - [20:20:05.432] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-15 20:20:05 - [20:20:05.432] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348054138526","Status":"SUCCESS","RowCount":1}
[20:20:05.436] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
2025-11-15 20:20:05 - [20:20:05.436] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
[20:20:05.438] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (28ms)
2025-11-15 20:20:05 - [20:20:05.438] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (28ms)
[20:20:05.441] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-15 20:20:05 - [20:20:05.441] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[20:20:05.443] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-15 20:20:05 - [20:20:05.443] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:20:05.445] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.445] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.445] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348054451478"}
[20:20:05.448] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.448] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.450] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-15 20:20:05 - [20:20:05.450] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:20:05.454] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.454] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-15 20:20:05 - [20:20:05.454] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[20:20:05.457] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-15 20:20:05 - [20:20:05.457] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[20:20:05.459] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-15 20:20:05 - [20:20:05.459] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[20:20:05.461] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-15 20:20:05 - [20:20:05.461] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-15 20:20:05 - [20:20:05.461] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638988348054451478","Status":"SUCCESS","RowCount":1}
[20:20:05.464] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
2025-11-15 20:20:05 - [20:20:05.464] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
[20:20:05.466] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-15 20:20:05 - [20:20:05.466] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-15 20:20:05 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-15 20:20:05 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-15 20:20:05 - [Splash] Core startup sequence completed
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeDebouncer[0]
      Applying debounced theme change: Arctic (Reason: Login)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Theme changed to 'Arctic' (Reason: Login, User: JOHNK)
2025-11-15 20:20:05 - Theme changed to 'Arctic' - Reason: Login
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-15 20:20:05 - [ThemedForm] MainForm initialized with automatic theme support
[20:20:05.851] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-15 20:20:05 - [20:20:05.851] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[20:20:05.855] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-15 20:20:05 - [20:20:05.855] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-15 20:20:05 - [ThemedUserControl] Control_ConnectionStrengthControl initialized with automatic theme support
2025-11-15 20:20:05 - [ThemedUserControl] Control_InventoryTab initialized with automatic theme support
[20:20:05.881] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.881] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[20:20:05.883] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.883] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[20:20:05.899] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.899] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[20:20:05.902] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.902] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[20:20:05.905] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.905] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[20:20:05.907] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.907] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[20:20:05.910] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.910] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[20:20:05.913] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-15 20:20:05 - [20:20:05.913] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-15 20:20:05 - [20:20:05.913] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638988348059131512"}
[20:20:05.916] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.916] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.918] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-15 20:20:05 - [20:20:05.918] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[20:20:05.922] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-15 20:20:05 - [20:20:05.922] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-15 20:20:05 - [20:20:05.922] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638988348059221917"}
[20:20:05.924] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.924] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.926] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-15 20:20:05 - [20:20:05.926] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[20:20:05.930] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-15 20:20:05 - [20:20:05.930] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-15 20:20:05 - [20:20:05.930] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638988348059308141"}
[20:20:05.933] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:05 - [20:20:05.933] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:05.935] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-15 20:20:05 - [20:20:05.935] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[20:20:05.939] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.939] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-15 20:20:05 - Inventory tab events wired up.
[20:20:05.978] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.978] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[20:20:05.982] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.982] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
[20:20:05.983] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.983] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[20:20:05.986] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-15 20:20:05 - [20:20:05.986] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[20:20:05.988] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (106ms)
2025-11-15 20:20:05 - [20:20:05.988] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (106ms)
[20:20:05.992] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-15 20:20:05 - [20:20:05.992] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[20:20:05.994] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-15 20:20:05 - [20:20:05.994] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-15 20:20:05 - Control_AdvancedInventory constructor entered.
[20:20:06.007] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-15 20:20:06 - [20:20:06.007] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[20:20:06.009] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-15 20:20:06 - [20:20:06.009] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Part
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Part for Part Numbers
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Op
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Op for Operations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Loc
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Loc for Locations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Part
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Part for Part Numbers
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Op
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Op for Operations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Loc
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Loc for Locations
2025-11-15 20:20:06 - [Control_AdvancedInventory] SuggestionTextBox controls configured for Single Entry and Multi-Location tabs
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Part
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Part for Part Numbers
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Op
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Op for Operations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_Single_TextBox_Loc
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_Single_TextBox_Loc for Locations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Part
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Part for Part Numbers
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Op
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Op for Operations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for AdvancedInventory_MultiLoc_TextBox_Loc
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured AdvancedInventory_MultiLoc_TextBox_Loc for Locations
2025-11-15 20:20:06 - [Control_AdvancedInventory] SuggestionTextBox controls configured for Single Entry and Multi-Location tabs
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - Control_AdvancedInventory constructor exited.
[20:20:06.241] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.241] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[20:20:06.243] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.243] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[20:20:06.253] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.253] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[20:20:06.255] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.255] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_RemoveTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_RemoveTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Operation (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_RemoveTab_TextBox_Operation (SuggestionTextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_RemoveTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_RemoveTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_RemoveTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
[20:20:06.332] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.332] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[20:20:06.335] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.335] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured Control_RemoveTab_TextBox_Part for Part Numbers
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for Control_RemoveTab_TextBox_Operation
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured Control_RemoveTab_TextBox_Operation for Operations
[20:20:06.344] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.344] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[20:20:06.347] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.347] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[20:20:06.350] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.350] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[20:20:06.352] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-15 20:20:06 - [20:20:06.352] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[20:20:06.355] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (113ms)
2025-11-15 20:20:06 - [20:20:06.355] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (113ms)
[20:20:06.358] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-15 20:20:06 - [20:20:06.358] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[20:20:06.360] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-15 20:20:06 - [20:20:06.360] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[20:20:06.371] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-15 20:20:06 - [20:20:06.371] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[20:20:06.374] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-15 20:20:06 - [20:20:06.374] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_To (DateTimePicker)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_To
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_From (DateTimePicker)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_From
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Location (TextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Location
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Location
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Location
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Part (TextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date (CheckBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_ComboBox_User (ComboBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_ComboBox_User
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_ComboBox_User
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached DropDown handler for ComboBox Control_AdvancedRemove_ComboBox_User
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_ComboBox_User
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Operation (TextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Notes (TextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Notes
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Notes
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMin (TextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMin
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMin
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMax (TextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMax
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMax
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results (DataGridView) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
[20:20:06.545] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-15 20:20:06 - [20:20:06.545] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[20:20:06.553] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-15 20:20:06 - [20:20:06.553] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[20:20:06.555] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-15 20:20:06 - [20:20:06.555] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_Operation (SuggestionTextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_Operation
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_Part
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: panel1 (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:06 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - APPLYING
2025-11-15 20:20:06 - [FocusUtils] Apply: Starting for Control_TransferTab_TextBox_ToLocation (SuggestionTextBox)
2025-11-15 20:20:06 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_TransferTab_TextBox_ToLocation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_TransferTab_TextBox_ToLocation
2025-11-15 20:20:06 - [FocusUtils] Apply: Attached Click handler for TextBox Control_TransferTab_TextBox_ToLocation
2025-11-15 20:20:06 - [FocusUtils] Apply: Completed for Control_TransferTab_TextBox_ToLocation
2025-11-15 20:20:06 - Transfer tab events wired up.
[20:20:06.674] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-15 20:20:06 - [20:20:06.674] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-15 20:20:06 - [ThemedUserControl] Control_QuickButtons initialized with automatic theme support
[20:20:06.678] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-15 20:20:06 - [20:20:06.678] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[20:20:06.680] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for Control_TransferTab_TextBox_Part
2025-11-15 20:20:06 - [20:20:06.680] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured Control_TransferTab_TextBox_Part for Part Numbers
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for Control_TransferTab_TextBox_Operation
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured Control_TransferTab_TextBox_Operation for Operations
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] F4 handler registered for Control_TransferTab_TextBox_ToLocation
2025-11-15 20:20:06 - [Helper_SuggestionTextBox] Configured Control_TransferTab_TextBox_ToLocation for Locations
[20:20:06.691] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-15 20:20:06 - [20:20:06.691] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[20:20:06.694] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-15 20:20:06 - [20:20:06.694] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
[20:20:06.716] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-15 20:20:06 - [20:20:06.716] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[20:20:06.719] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-15 20:20:06 - [20:20:06.719] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[20:20:06.722] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-15 20:20:06 - [20:20:06.722] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[20:20:06.726] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-15 20:20:06 - [20:20:06.726] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[20:20:06.728] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
2025-11-15 20:20:06 - [20:20:06.728] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
[20:20:06.731] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-15 20:20:06 - [20:20:06.731] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[20:20:06.733] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-15 20:20:06 - [20:20:06.733] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[20:20:06.735] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (15ms)
2025-11-15 20:20:06 - [20:20:06.735] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (15ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[20:20:06.740] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-15 20:20:06 - [20:20:06.740] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[20:20:06.743] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-15 20:20:06 - [20:20:06.743] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[20:20:06.747] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-15 20:20:06 - [20:20:06.747] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[20:20:06.749] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-15 20:20:06 - [20:20:06.749] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[20:20:06.753] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-15 20:20:06 - [20:20:06.753] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[20:20:06.756] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-15 20:20:06 - [20:20:06.756] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-15 20:20:06 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[20:20:06.761] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-15 20:20:06 - [20:20:06.761] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[20:20:06.763] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
2025-11-15 20:20:06 - [20:20:06.763] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[20:20:06.767] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-15 20:20:06 - [20:20:06.767] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[20:20:06.771] [MEDIUM] ⬅️ EXITING MainForm.MainForm (919ms)
2025-11-15 20:20:06 - [20:20:06.771] [MEDIUM] ⬅️ EXITING MainForm.MainForm (919ms)
2025-11-15 20:20:06 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-15 20:20:06 - Remove tab suggestion controls configured.
2025-11-15 20:20:06 - Removal tab events wired up.
2025-11-15 20:20:06 - Initial setup of ComboBoxes in the Remove Tab.
[20:20:06.781] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-15 20:20:06 - [20:20:06.781] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:20:06.783] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.783] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.783] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638988348067832613"}
[20:20:06.786] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:06 - [20:20:06.786] [MEDIUM]       ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:06.789] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.789] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-15 20:20:06 - Transfer tab suggestion controls configured.
2025-11-15 20:20:06 - [Performance Warning] Stored procedure 'md_operation_numbers_Get_All' (Query) took 872ms (threshold: 500ms)
[20:20:06.795] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (872ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.795] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (872ms) - Status: 1
[20:20:06.797] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-15 20:20:06 - [20:20:06.797] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:20:06.801] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.801] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.801] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638988348068016945"}
[20:20:06.805] [MEDIUM]         ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:06 - [20:20:06.805] [MEDIUM]         ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:06.808] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.808] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-15 20:20:06 - [20:20:06.795] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":872,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[20:20:06.815] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (872ms) - 72 rows
2025-11-15 20:20:06 - [20:20:06.815] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (872ms) - 72 rows
[20:20:06.817] [MEDIUM]         ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (901ms)
2025-11-15 20:20:06 - [20:20:06.817] [MEDIUM]         ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (901ms)
[20:20:06.820] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (898ms)
2025-11-15 20:20:06 - [20:20:06.820] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (898ms)
2025-11-15 20:20:06 - [20:20:06.820] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":898,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638988348059221917","Status":"SUCCESS","RowCount":72}
2025-11-15 20:20:06 - [Splash] All form instances configured successfully
2025-11-15 20:20:06 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-15 20:20:06 - [Splash] MainForm uses ThemedForm - automatic theme application
2025-11-15 20:20:06 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
[20:20:06.863] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (80ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.863] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (80ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.863] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":80,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[20:20:06.868] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (80ms) - 1 rows
2025-11-15 20:20:06 - [20:20:06.868] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (80ms) - 1 rows
[20:20:06.871] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (946ms)
2025-11-15 20:20:06 - [20:20:06.871] [MEDIUM]       ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (946ms)
[20:20:06.873] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (90ms)
2025-11-15 20:20:06 - [20:20:06.873] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (90ms)
2025-11-15 20:20:06 - [20:20:06.873] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":90,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638988348067832613","Status":"SUCCESS","RowCount":1}
[20:20:06.877] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (96ms)
2025-11-15 20:20:06 - [20:20:06.877] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (96ms)
2025-11-15 20:20:06 - User full name loaded: John Koll
[20:20:06.880] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (79ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.880] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (79ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.880] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":79,"Thread":21,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[20:20:06.885] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (79ms) - 1 rows
2025-11-15 20:20:06 - [20:20:06.885] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (79ms) - 1 rows
[20:20:06.888] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (954ms)
2025-11-15 20:20:06 - [20:20:06.888] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (954ms)
[20:20:06.890] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (88ms)
2025-11-15 20:20:06 - [20:20:06.890] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (88ms)
2025-11-15 20:20:06 - [20:20:06.890] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":88,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638988348068016945","Status":"SUCCESS","RowCount":1}
[20:20:06.894] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (96ms)
2025-11-15 20:20:06 - [20:20:06.894] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (96ms)
2025-11-15 20:20:06 - User full name loaded: John Koll
2025-11-15 20:20:06 - [Performance Warning] Stored procedure 'md_part_ids_Get_All' (Query) took 996ms (threshold: 500ms)
[20:20:06.910] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (996ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.910] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (996ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.910] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":996,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3747 part(s)"},"ResultData":"DataTable[3747 rows]","ErrorMessage":"Retrieved 3747 part(s)"}
[20:20:06.916] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (996ms) - 3747 rows
2025-11-15 20:20:06 - [20:20:06.916] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (996ms) - 3747 rows
[20:20:06.918] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (131ms)
2025-11-15 20:20:06 - [20:20:06.918] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (131ms)
[20:20:06.921] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1007ms)
2025-11-15 20:20:06 - [20:20:06.921] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (1007ms)
2025-11-15 20:20:06 - [20:20:06.921] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":1007,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638988348059131512","Status":"SUCCESS","RowCount":3747}
2025-11-15 20:20:06 - [Performance Warning] Stored procedure 'md_locations_Get_All' (Query) took 1015ms (threshold: 500ms)
[20:20:06.947] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1015ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.947] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (1015ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.947] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":1015,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[20:20:06.950] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1015ms) - 10371 rows
2025-11-15 20:20:06 - [20:20:06.950] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (1015ms) - 10371 rows
[20:20:06.953] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (147ms)
2025-11-15 20:20:06 - [20:20:06.953] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (147ms)
[20:20:06.955] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1024ms)
2025-11-15 20:20:06 - [20:20:06.955] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (1024ms)
2025-11-15 20:20:06 - [20:20:06.955] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":1024,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638988348059308141","Status":"SUCCESS","RowCount":10371}
[20:20:06.965] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
2025-11-15 20:20:06 - [20:20:06.965] [MEDIUM] ➡️ ENTERING Dao_Part.GetColorCodeFlaggedPartsAsync
[20:20:06.967] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-15 20:20:06 - [20:20:06.967] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_GetAllColorCodeFlagged
2025-11-15 20:20:06 - [20:20:06.967] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638988348069679285"}
[20:20:06.971] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:06 - [20:20:06.971] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:06.973] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
2025-11-15 20:20:06 - [20:20:06.973] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_GetAllColorCodeFlagged
[20:20:06.979] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (11ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.979] [HIGH  ] ✅ PROCEDURE md_part_ids_GetAllColorCodeFlagged (11ms) - Status: 1
2025-11-15 20:20:06 - [20:20:06.979] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_GetAllColorCodeFlagged","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[6 rows]"}
[20:20:06.982] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (11ms) - 6 rows
2025-11-15 20:20:06 - [20:20:06.982] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_GetAllColorCodeFlagged (11ms) - 6 rows
[20:20:06.984] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-15 20:20:06 - [20:20:06.984] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[20:20:06.986] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (18ms)
2025-11-15 20:20:06 - [20:20:06.986] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_GetAllColorCodeFlagged (18ms)
2025-11-15 20:20:06 - [20:20:06.986] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_GetAllColorCodeFlagged","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_GetAllColorCodeFlagged:638988348069679285","Status":"SUCCESS","RowCount":6}
[20:20:06.990] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (24ms)
2025-11-15 20:20:06 - [20:20:06.990] [MEDIUM] ⬅️ EXITING Dao_Part.GetColorCodeFlaggedPartsAsync (24ms)
2025-11-15 20:20:06 - [Model_Application_Variables] ColorCodeParts cache loaded: 6 parts
[20:20:06.993] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-15 20:20:06 - [20:20:06.993] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_color_codes_GetAll
2025-11-15 20:20:06 - [20:20:06.993] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638988348069936593"}
[20:20:06.996] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:06 - [20:20:06.996] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:06.999] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
2025-11-15 20:20:07 - [20:20:06.999] [MEDIUM] 🗄️ DB PROCEDURE START: md_color_codes_GetAll
[20:20:07.004] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (11ms) - Status: 1
2025-11-15 20:20:07 - [20:20:07.004] [HIGH  ] ✅ PROCEDURE md_color_codes_GetAll (11ms) - Status: 1
2025-11-15 20:20:07 - [20:20:07.004] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_color_codes_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":""},"ResultData":"DataTable[10 rows]"}
[20:20:07.008] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (11ms) - 10 rows
2025-11-15 20:20:07 - [20:20:07.008] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_color_codes_GetAll (11ms) - 10 rows
[20:20:07.010] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-15 20:20:07 - [20:20:07.010] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[20:20:07.012] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (18ms)
2025-11-15 20:20:07 - [20:20:07.012] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_color_codes_GetAll (18ms)
2025-11-15 20:20:07 - [20:20:07.012] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_color_codes_GetAll","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_md_color_codes_GetAll:638988348069936593","Status":"SUCCESS","RowCount":10}
2025-11-15 20:20:07 - [Model_Application_Variables] ValidColorCodes cache loaded: 10 colors
2025-11-15 20:20:07 - [InventoryTab Startup] Color code caches loaded: Parts=6, Colors=10
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] F4 handler registered for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] Configured Control_InventoryTab_TextBox_Part for Part Numbers
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] F4 handler registered for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] Configured Control_InventoryTab_TextBox_Operation for Operations
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] F4 handler registered for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] Configured Control_InventoryTab_TextBox_Location for Locations
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] F4 handler registered for Control_InventoryTab_TextBox_ColorCode
2025-11-15 20:20:07 - [Helper_SuggestionTextBox] Configured Control_InventoryTab_TextBox_ColorCode for Color Codes
2025-11-15 20:20:07 - Inventory tab suggestion controls configured.
[20:20:07.145] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (1235ms)
2025-11-15 20:20:07 - [20:20:07.145] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (1235ms)
2025-11-15 20:20:07 - [ThemedUserControl] Using applier for MainForm_UserControl_InventoryTab
2025-11-15 20:20:07 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:07 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 12 controls
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_ColorCode (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_ColorCode (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_WorkOrder (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_WorkOrder (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [ThemedUserControl] Using applier for MainForm_UserControl_QuickButtons
2025-11-15 20:20:07 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:07 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[20:20:07.477] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-15 20:20:07 - [20:20:07.477] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 0 controls
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_TableLayout (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_MenuStrip (MenuStrip) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_MenuStrip (MenuStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_SplitContainer_Middle (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_SplitContainer_Middle (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Inventory (TabPage) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Inventory (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_InventoryTab (Control_InventoryTab) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_InventoryTab (Control_InventoryTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 12 controls
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-15 20:20:07 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:07 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_ColorCode (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_ColorCode - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_ColorCode (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_WorkOrder (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TextBox_WorkOrder - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_WorkOrder (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedInventory - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Remove - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Remove (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_RemoveTab - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_RemoveTab (Control_RemoveTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TextBox_Operation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedRemove - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_To - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_From - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Location - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_ComboBox_User - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:07 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:07 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMin - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMax - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Transfer (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_TransferTab - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_TransferTab (Control_TransferTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Operation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_TextBox_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_TextBox_ToLocation (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_QuickButtons (Control_QuickButtons) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_QuickButtons (Control_QuickButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel1 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel1 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: MainForm_StatusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: MainForm_StatusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] CanControlReceiveFocus:  (MdiClient) - FALSE (control type cannot receive focus)
2025-11-15 20:20:08 - [FocusUtils] ApplyFocusEventHandling:  (MdiClient) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:08 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:08 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:08 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Part
2025-11-15 20:20:08 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:08 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part - EXITING
2025-11-15 20:20:08 - [Splash] MainForm displayed successfully
2025-11-15 20:20:08 - [Splash] MainForm displayed - startup complete
2025-11-15 20:20:08 - [Splash] Splash screen closed
[20:20:08.209] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-15 20:20:08 - [20:20:08.209] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-15 20:20:08 -
2025-11-15 20:20:08 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-15 20:20:08 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-15 20:20:08 - [QuickButtons]   User: JOHNK
2025-11-15 20:20:08 - [QuickButtons] ════════════════════════════════════════════════════════════
[20:20:08.219] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-15 20:20:08 - [20:20:08.219] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-15 20:20:08 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-15 20:20:08 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[20:20:08.225] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.225] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.225] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638988348082256981"}
[20:20:08.229] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:08 - [20:20:08.229] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:08.231] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.231] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [ThemedForm] Using FormThemeApplier for MainForm
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:08 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'MainForm' in 40ms
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[20:20:08.284] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-15 20:20:08 - [20:20:08.284] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:20:08.286] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.286] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.286] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638988348082864048"}
[20:20:08.289] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:08 - [20:20:08.289] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:08.291] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.291] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-15 20:20:08 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:08 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:08 - [FocusUtils] Attached TextChanged to TextBox: Control_InventoryTab_TextBox_Part
[20:20:08.304] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (18ms) - Status: 1
2025-11-15 20:20:08 - [20:20:08.304] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (18ms) - Status: 1
2025-11-15 20:20:08 - [20:20:08.304] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":18,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[20:20:08.309] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (18ms) - 1 rows
2025-11-15 20:20:08 - [20:20:08.309] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (18ms) - 1 rows
[20:20:08.311] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (22ms)
2025-11-15 20:20:08 - [20:20:08.311] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (22ms)
[20:20:08.313] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (27ms)
2025-11-15 20:20:08 - [20:20:08.313] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (27ms)
2025-11-15 20:20:08 - [20:20:08.313] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":27,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638988348082864048","Status":"SUCCESS","RowCount":1}
[20:20:08.317] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (32ms)
2025-11-15 20:20:08 - [20:20:08.317] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (32ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[20:20:08.321] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-15 20:20:08 - [20:20:08.321] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[20:20:08.325] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-15 20:20:08 - [20:20:08.325] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[20:20:08.327] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-15 20:20:08 - [20:20:08.327] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-15 20:20:08 - Application Info - Development Menu configured for user 'JOHNK': Visible
[20:20:08.330] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
2025-11-15 20:20:08 - [20:20:08.330] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
[20:20:08.334] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (108ms) - Status: 1
2025-11-15 20:20:08 - [20:20:08.334] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (108ms) - Status: 1
2025-11-15 20:20:08 - [20:20:08.334] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":108,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10 transaction(s) for user: JOHNK"},"ResultData":"DataTable[10 rows]","ErrorMessage":"Retrieved 10 transaction(s) for user: JOHNK"}
[20:20:08.337] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (108ms) - 10 rows
2025-11-15 20:20:08 - [20:20:08.337] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (108ms) - 10 rows
[20:20:08.340] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (110ms)
2025-11-15 20:20:08 - [20:20:08.340] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (110ms)
[20:20:08.342] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (117ms)
2025-11-15 20:20:08 - [20:20:08.342] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (117ms)
2025-11-15 20:20:08 - [20:20:08.342] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":117,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638988348082256981","Status":"SUCCESS","RowCount":10}
2025-11-15 20:20:08 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: A110146 + 900 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: A110147 + 90 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: A110146 + 90 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 0K2142 + 19 (Qty: 16)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 21-28841-006 + 19 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 21179 + 15 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-15 20:20:08 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-15 20:20:08 - [Dao_QuickButtons] Array restructured: 10 unique buttons, 0 duplicates removed
2025-11-15 20:20:08 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-15 20:20:08 - [Dao_QuickButtons] All buttons deleted from database
2025-11-15 20:20:08 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 1: A110146 + 900 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 2: A110147 + 90 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 3: A110146 + 90 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 4: 0K2142 + 19 (Qty: 16)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 5: 21-28841-006 + 19 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 6: 21179 + 15 (Qty: 500)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 7: 01-31976-000 + 10 (Qty: 1)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 8: 04-27693-000 + 90 (Qty: 10)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 9: 01-34578-000 + 880 (Qty: 20)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created button at position 10: 03-29236-030 + 959 (Qty: 30)
2025-11-15 20:20:08 - [Dao_QuickButtons] Created 10 buttons in database
2025-11-15 20:20:08 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 10 buttons remain
2025-11-15 20:20:08 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-15 20:20:08 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 10 buttons remain
2025-11-15 20:20:08 - [QuickButtons] STEP 2: Loading data from database
[20:20:08.487] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.487] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.487] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638988348084870306"}
[20:20:08.490] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:08 - [20:20:08.490] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:08.493] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-15 20:20:08 - [20:20:08.493] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[20:20:08.498] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-15 20:20:08 - [20:20:08.498] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-15 20:20:08 - [20:20:08.498] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10 transaction(s) for user: JOHNK"},"ResultData":"DataTable[10 rows]","ErrorMessage":"Retrieved 10 transaction(s) for user: JOHNK"}
[20:20:08.501] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 10 rows
2025-11-15 20:20:08 - [20:20:08.501] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 10 rows
[20:20:08.503] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-15 20:20:08 - [20:20:08.503] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[20:20:08.505] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-15 20:20:08 - [20:20:08.505] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-15 20:20:08 - [20:20:08.505] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638988348084870306","Status":"SUCCESS","RowCount":10}
[20:20:08.511] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-15 20:20:08 - [20:20:08.511] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-15 20:20:08 - [QuickButtons] STEP 2: ✓ Retrieved 10 button(s) from database
2025-11-15 20:20:08 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 1: A110146 + Op:900 (Qty: 500)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 2: A110147 + Op:90 (Qty: 500)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 3: A110146 + Op:90 (Qty: 500)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 4: 0K2142 + Op:19 (Qty: 16)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 5: 21-28841-006 + Op:19 (Qty: 500)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 6: 21179 + Op:15 (Qty: 500)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 7: 01-31976-000 + Op:10 (Qty: 1)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 8: 04-27693-000 + Op:90 (Qty: 10)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 9: 01-34578-000 + Op:880 (Qty: 20)
2025-11-15 20:20:08 - [QuickButtons] STEP 3:   Button 10: 03-29236-030 + Op:959 (Qty: 30)
2025-11-15 20:20:08 - [QuickButtons] STEP 3: Filled 10 button(s) with data
2025-11-15 20:20:08 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-15 20:20:08 - [QuickButtons] STEP 4: Layout refreshed - 10 visible button(s)
2025-11-15 20:20:08 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-15 20:20:08 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-15 20:20:08 - [QuickButtons] ║ User: JOHNK
2025-11-15 20:20:08 - [QuickButtons] ║ Visible Buttons: 10
2025-11-15 20:20:08 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-15 20:20:08 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-15 20:20:08 -
[20:20:08.582] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (362ms)
2025-11-15 20:20:08 - [20:20:08.582] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (362ms)
[20:20:08.584] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-15 20:20:08 - [20:20:08.584] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
2025-11-15 20:20:10 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:10 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:10 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
2025-11-15 20:20:10 - [FocusUtils] Control_GotFocus_Handler: Control_RemoveTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:10 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:10 - [FocusUtils] Calling SelectAll on TextBox: Control_RemoveTab_TextBox_Part
2025-11-15 20:20:10 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:10 - [FocusUtils] Control_GotFocus_Handler: Control_RemoveTab_TextBox_Part - EXITING
[20:20:10.755] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:10 - [20:20:10.755] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:10 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:10 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_RemoveTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:10 - [FocusUtils] Attached TextChanged to TextBox: Control_RemoveTab_TextBox_Part
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:20:10.769] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (14ms)
2025-11-15 20:20:10 - [20:20:10.769] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (14ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
2025-11-15 20:20:11 - [FocusUtils] Control_LostFocus_Handler: Control_RemoveTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:11 - [FocusUtils] Control_LostFocus_Handler: Control_RemoveTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:11 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_RemoveTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
2025-11-15 20:20:11 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:11 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part - EXITING
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
2025-11-15 20:20:11 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Checking if should restore normal BackColor
[20:20:11.447] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:11 - [20:20:11.447] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:11 - [FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] AttachTextChangedHandlers: Attaching to Control_TransferTab_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:11 - [FocusUtils] Attached TextChanged to TextBox: Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:11 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:20:11.464] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (17ms)
2025-11-15 20:20:11 - [20:20:11.464] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (17ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
2025-11-15 20:20:11 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:11 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:11 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part - EXITING
2025-11-15 20:20:11 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Part
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
2025-11-15 20:20:11 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:11 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:11 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_TransferTab_SoftReset on Control_TransferTab
[20:20:12.156] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:12 - [20:20:12.156] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:20:12.183] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (26ms)
2025-11-15 20:20:12 - [20:20:12.183] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (26ms)
[DEBUG] InventoryTab SoftReset button re-enabled
2025-11-15 20:20:12 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:12 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part - EXITING
[DEBUG] Restoring status strip after reset
2025-11-15 20:20:12 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:12 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:12 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
[DEBUG] Updating status strip for Soft Reset
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
2025-11-15 20:20:12 - [FocusUtils] Control_GotFocus_Handler: Control_RemoveTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:12 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Calling SelectAll on TextBox: Control_RemoveTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_RemoveTab_TextBox_Part
2025-11-15 20:20:12 - [FocusUtils] Control_GotFocus_Handler: Control_RemoveTab_TextBox_Part - EXITING
[20:20:12.958] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:12 - [20:20:12.958] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:12 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_RemoveTab_TextBox_Part
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:20:12.969] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (11ms)
2025-11-15 20:20:12 - [20:20:12.969] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (11ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
2025-11-15 20:20:13 - [FocusUtils] Control_LostFocus_Handler: Control_RemoveTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:13 - [FocusUtils] Control_LostFocus_Handler: Control_RemoveTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:13 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_RemoveTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
2025-11-15 20:20:13 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:13 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part - EXITING
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
2025-11-15 20:20:13 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Checking if should restore normal BackColor
[20:20:13.632] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:13 - [20:20:13.632] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:13 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:13 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:20:13.645] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (12ms)
2025-11-15 20:20:13 - [20:20:13.645] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (12ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
2025-11-15 20:20:13 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:13 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Calling SelectAll on TextBox: Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Control_GotFocus_Handler: Control_TransferTab_TextBox_Part - EXITING
2025-11-15 20:20:13 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_TransferTab_TextBox_Part
2025-11-15 20:20:13 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:13 - [FocusUtils] Control_LostFocus_Handler: Control_TransferTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:13 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_TransferTab_TextBox_Part
Resetting user controls...
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
[20:20:14.344] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-15 20:20:14 - [20:20:14.344] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:20:14.354] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (9ms)
2025-11-15 20:20:14 - [20:20:14.354] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (9ms)
[DEBUG] InventoryTab SoftReset button re-enabled
2025-11-15 20:20:14 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - ENTERING
2025-11-15 20:20:14 - [FocusUtils] Setting BackColor to: Color [A=255, R=179, G=224, B=255] for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Calling SelectAll on TextBox: Control_InventoryTab_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Queueing BeginInvoke to attach handlers for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Control_GotFocus_Handler: Control_InventoryTab_TextBox_Part - EXITING
[DEBUG] Restoring status strip after reset
2025-11-15 20:20:14 - [FocusUtils] BeginInvoke executing: Handlers already attached for Control_InventoryTab_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:14 - [FocusUtils] Control_LostFocus_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:14 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[20:20:14.919] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_AdvancedEntry_Click.Control_InventoryTab
2025-11-15 20:20:14 - [20:20:14.919] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_AdvancedEntry_Click.Control_InventoryTab
2025-11-15 20:20:14 - Control_AdvancedInventory: All controls migrated to SuggestionTextBox.
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single (TabPage) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView (ListView) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left (GroupBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send (Button) - FALSE (control type cannot receive focus)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-15 20:20:14 - [FocusUtils] Apply: Starting for AdvancedInventory_Single_TextBox_Part (SuggestionTextBox)
2025-11-15 20:20:14 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Apply: Attached Click handler for TextBox AdvancedInventory_Single_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] Apply: Completed for AdvancedInventory_Single_TextBox_Part
2025-11-15 20:20:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Op (SuggestionTextBox) - APPLYING
2025-11-15 20:20:15 - [FocusUtils] Apply: Starting for AdvancedInventory_Single_TextBox_Op (SuggestionTextBox)
2025-11-15 20:20:15 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Op
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Op
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached Click handler for TextBox AdvancedInventory_Single_TextBox_Op
2025-11-15 20:20:15 - [FocusUtils] Apply: Completed for AdvancedInventory_Single_TextBox_Op
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - APPLYING
2025-11-15 20:20:15 - [FocusUtils] Apply: Starting for AdvancedInventory_Single_TextBox_Qty (TextBox)
2025-11-15 20:20:15 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Qty
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Qty
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached Click handler for TextBox AdvancedInventory_Single_TextBox_Qty
2025-11-15 20:20:15 - [FocusUtils] Apply: Completed for AdvancedInventory_Single_TextBox_Qty
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Loc (SuggestionTextBox) - APPLYING
2025-11-15 20:20:15 - [FocusUtils] Apply: Starting for AdvancedInventory_Single_TextBox_Loc (SuggestionTextBox)
2025-11-15 20:20:15 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Loc
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Loc
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached Click handler for TextBox AdvancedInventory_Single_TextBox_Loc
2025-11-15 20:20:15 - [FocusUtils] Apply: Completed for AdvancedInventory_Single_TextBox_Loc
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - APPLYING
2025-11-15 20:20:15 - [FocusUtils] Apply: Starting for AdvancedInventory_Single_TextBox_Count (TextBox)
2025-11-15 20:20:15 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Count
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for AdvancedInventory_Single_TextBox_Count
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached Click handler for TextBox AdvancedInventory_Single_TextBox_Count
2025-11-15 20:20:15 - [FocusUtils] Apply: Completed for AdvancedInventory_Single_TextBox_Count
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: panel4 (Panel) - FALSE (control type cannot receive focus)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-15 20:20:15 - [FocusUtils] Apply: Starting for AdvancedInventory_Single_RichTextBox_Notes (RichTextBox)
2025-11-15 20:20:15 - [FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for AdvancedInventory_Single_RichTextBox_Notes
2025-11-15 20:20:15 - [FocusUtils] Apply: Attached GotFocus/LostFocus handlers for AdvancedInventory_Single_RichTextBox_Notes
2025-11-15 20:20:15 - [FocusUtils] Apply: Completed for AdvancedInventory_Single_RichTextBox_Notes
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Part (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Op (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Loc (SuggestionTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-15 20:20:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
[20:20:15.175] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_AdvancedEntry_Click.Control_InventoryTab (256ms)
2025-11-15 20:20:15 - [20:20:15.175] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_AdvancedEntry_Click.Control_InventoryTab (256ms)
2025-11-15 20:20:16 - [FocusUtils] Control_LostFocus_Handler: AdvancedInventory_Single_TextBox_Part - Checking if should restore normal BackColor
2025-11-15 20:20:16 - [FocusUtils] Control_LostFocus_Handler: AdvancedInventory_Single_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-15 20:20:16 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for AdvancedInventory_Single_TextBox_Part
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-15 20:20:19 - [Helper_SuggestionTextBox] Retrieved 3747 part numbers from cache
2025-11-15 20:20:19 - [SuggestionTextBox] Overlay opened: Field=AdvancedInventory_MultiLoc_TextBox_Part, Matches=29, Input='21-'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-15 20:20:19 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-15 20:20:19 - [SuggestionTextBox] About to show dialog
2025-11-15 20:20:19 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:19 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:19 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:19 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:19 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:19 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-15 20:20:19 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:19 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
2025-11-15 20:20:20 - [SuggestionOverlay] AcceptSelection called: SelectedItem='21-25135-000'
2025-11-15 20:20:20 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-15 20:20:20 - [SuggestionTextBox] Captured selectedValue: '21-25135-000'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-15 20:20:20 - [SuggestionTextBox] BEFORE text assignment: Field=AdvancedInventory_MultiLoc_TextBox_Part, Current Text='21-', Will set to='21-25135-000'
2025-11-15 20:20:20 - [SuggestionTextBox] AFTER text assignment: Field=AdvancedInventory_MultiLoc_TextBox_Part, Text is now='21-25135-000'
2025-11-15 20:20:20 - [Control_AdvancedInventory] Multi-Loc Part selected: 21-25135-000
2025-11-15 20:20:20 - Confirmation dialog shown: Redirect Required - This part requires color code entry. Redirect to Inventory Tab?
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-15 20:20:24 - [Control_AdvancedInventory] Multi-Loc Part selected: 21-25135-000
2025-11-15 20:20:24 - Confirmation dialog shown: Redirect Required - This part requires color code entry. Redirect to Inventory Tab?
2025-11-15 20:20:25 - [SuggestionTextBox] Suggestion selected event raised: Field=AdvancedInventory_MultiLoc_TextBox_Part, Value='21-25135-000', Original='21-'
2025-11-15 20:20:25 - [SuggestionTextBox] Focus moved to next control: True
2025-11-15 20:20:25 - [SuggestionTextBox] Overlay closed finally block
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-15 20:20:30 - [Helper_SuggestionTextBox] Retrieved 3747 part numbers from cache
2025-11-15 20:20:30 - [SuggestionTextBox] Overlay opened: Field=AdvancedInventory_MultiLoc_TextBox_Part, Matches=29, Input='21-'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-15 20:20:30 - [ThemedForm] SuggestionOverlayForm initialized with automatic theme support
2025-11-15 20:20:30 - [SuggestionTextBox] About to show dialog
2025-11-15 20:20:30 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-15 20:20:30 - [FocusUtils] CanControlReceiveFocus: lblMatchCount (Label) - FALSE (control type cannot receive focus)
2025-11-15 20:20:30 - [FocusUtils] ApplyFocusEventHandling: lblMatchCount (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:30 - [FocusUtils] CanControlReceiveFocus: suggestionListBox (ListBox) - FALSE (control type cannot receive focus)
2025-11-15 20:20:30 - [FocusUtils] ApplyFocusEventHandling: suggestionListBox (ListBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-15 20:20:30 - [ThemedForm] Using FormThemeApplier for SuggestionOverlayForm
2025-11-15 20:20:30 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormBackColor: Color [A=255, R=248, G=249, B=250], ControlBackColor: Color [A=255, R=248, G=249, B=250], Final BackColor: Color [A=255, R=248, G=249, B=250]
2025-11-15 20:20:30 - [FormThemeApplier] Applying to 'SuggestionOverlayForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'SuggestionOverlayForm' in 3ms
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-15 20:20:32 - [SuggestionOverlay] AcceptSelection called: SelectedItem='68121-1L'
2025-11-15 20:20:32 - [SuggestionTextBox] Dialog closed with result: OK
2025-11-15 20:20:32 - [SuggestionTextBox] Captured selectedValue: '68121-1L'
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'SuggestionOverlayForm' unsubscribed from theme changes
2025-11-15 20:20:32 - [SuggestionTextBox] BEFORE text assignment: Field=AdvancedInventory_MultiLoc_TextBox_Part, Current Text='21-', Will set to='68121-1L'
2025-11-15 20:20:32 - [SuggestionTextBox] AFTER text assignment: Field=AdvancedInventory_MultiLoc_TextBox_Part, Text is now='68121-1L'
2025-11-15 20:20:32 - [Control_AdvancedInventory] Multi-Loc Part selected: 68121-1L
2025-11-15 20:20:32 - [Control_AdvancedInventory] Multi-Loc Part selected: 68121-1L
2025-11-15 20:20:32 - [SuggestionTextBox] Suggestion selected event raised: Field=AdvancedInventory_MultiLoc_TextBox_Part, Value='68121-1L', Original='21-'
2025-11-15 20:20:32 - [SuggestionTextBox] Focus moved to next control: True
2025-11-15 20:20:32 - [SuggestionTextBox] Overlay closed finally block
2025-11-15 20:20:34 - [Helper_SuggestionTextBox] Retrieved 72 operations from cache
2025-11-15 20:20:34 - MultiLoc Qty TextBox changed.
Running VersionChecker...
2025-11-15 20:20:35 - Running VersionChecker - checking database version information.
[20:20:35.015] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-15 20:20:35 - [20:20:35.015] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-15 20:20:35 - [20:20:35.015] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638988348350155561"}
[20:20:35.019] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:20:35 - [20:20:35.019] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:20:35.022] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-15 20:20:35 - [20:20:35.022] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[20:20:35.027] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (12ms) - Status: 1
2025-11-15 20:20:35 - [20:20:35.027] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (12ms) - Status: 1
2025-11-15 20:20:35 - [20:20:35.027] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":12,"Thread":22,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[20:20:35.031] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (12ms) - 1 rows
2025-11-15 20:20:35 - [20:20:35.031] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (12ms) - 1 rows
[20:20:35.034] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-15 20:20:35 - [20:20:35.034] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[20:20:35.036] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-15 20:20:35 - [20:20:35.036] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (20ms)
2025-11-15 20:20:35 - [20:20:35.036] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":20,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638988348350155561","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-15 20:20:35 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
2025-11-15 20:20:35 - MultiLoc Qty TextBox changed.
2025-11-15 20:20:35 - MultiLoc Qty TextBox changed.
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
2025-11-15 20:20:38 - [Helper_SuggestionTextBox] Retrieved 10371 locations from cache
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
Running VersionChecker...
2025-11-15 20:21:05 - Running VersionChecker - checking database version information.
[20:21:05.013] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-15 20:21:05 - [20:21:05.013] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-15 20:21:05 - [20:21:05.013] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638988348650133841"}
[20:21:05.016] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-15 20:21:05 - [20:21:05.016] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:05.019] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-15 20:21:05 - [20:21:05.019] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[20:21:05.025] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (11ms) - Status: 1
2025-11-15 20:21:05 - [20:21:05.025] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (11ms) - Status: 1
2025-11-15 20:21:05 - [20:21:05.025] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":31,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[20:21:05.027] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (11ms) - 1 rows
2025-11-15 20:21:05 - [20:21:05.027] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (11ms) - 1 rows
[20:21:05.030] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-15 20:21:05 - [20:21:05.030] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[20:21:05.032] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (19ms)
2025-11-15 20:21:05 - [20:21:05.032] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (19ms)
2025-11-15 20:21:05 - [20:21:05.032] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638988348650133841","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.2.1.0
2025-11-15 20:21:05 - Version check successful - Database version: 6.2.1.0
Version labels updated - App: 6.2.1.0, DB: 6.2.1.0
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 1, TimerActive: False
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 3, TimerActive: False
