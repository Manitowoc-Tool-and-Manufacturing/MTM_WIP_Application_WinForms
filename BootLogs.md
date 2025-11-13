------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[11:32:10.922] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-12 11:32:10 - [11:32:10.922] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[11:32:10.958] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-12 11:32:10 - [11:32:10.958] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[11:32:10.960] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-12 11:32:10 - [11:32:10.960] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[11:32:10.962] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-12 11:32:10 - [11:32:10.962] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-12 11:32:10 - [Startup] Application initialization started
2025-11-12 11:32:10 - [Startup] User identified: JOHNK
2025-11-12 11:32:10 - [Dao_System] Checking database connectivity
[11:32:10.997] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 11:32:10 - [11:32:10.997] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 11:32:11 - [11:32:10.997] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985439309975267"}
[11:32:11.059] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:11 - [11:32:11.059] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:11.061] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-12 11:32:11 - [11:32:11.061] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[11:32:11.257] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (259ms) - Status: 1
2025-11-12 11:32:11 - [11:32:11.257] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (259ms) - Status: 1
2025-11-12 11:32:11 - [11:32:11.257] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":259,"Thread":10,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[11:32:11.271] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (259ms) - 9 rows
2025-11-12 11:32:11 - [11:32:11.271] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (259ms) - 9 rows
[11:32:11.274] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (214ms)
2025-11-12 11:32:11 - [11:32:11.274] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (214ms)
[11:32:11.276] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (278ms)
2025-11-12 11:32:11 - [11:32:11.276] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (278ms)
2025-11-12 11:32:11 - [11:32:11.276] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":278,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985439309975267","Status":"SUCCESS","RowCount":9}
2025-11-12 11:32:11 - [Dao_System] Database connectivity check passed
2025-11-12 11:32:11 - [Startup] Database connectivity validated successfully
2025-11-12 11:32:11 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-12 11:32:11 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-12 11:32:11 - [Startup] Parameter cache populated: 116 procedures, 519 total parameters
2025-11-12 11:32:11 - [Startup] Parameter prefix cache initialized successfully in 14ms. Cached 116 stored procedures.
[Startup] Parameter cache: 116 procedures cached in 14ms
[11:32:11.302] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-12 11:32:11 - [11:32:11.302] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-12 11:32:11 - [11:32:11.302] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638985439313025760"}
[11:32:11.304] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:11 - [11:32:11.304] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:11.306] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-12 11:32:11 - [11:32:11.306] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-12 11:32:11 - [Startup] Initializing dependency injection container
2025-11-12 11:32:11 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
2025-11-12 11:32:11 - [Startup] Dependency injection container configured successfully
2025-11-12 11:32:11 - [Startup] Dependency injection container initialized successfully
2025-11-12 11:32:11 - [Splash] Initializing splash screen
[11:32:11.352] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (49ms) - Status: 1
2025-11-12 11:32:11 - [11:32:11.352] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (49ms) - Status: 1
2025-11-12 11:32:11 - [11:32:11.352] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_GetUserAccessType","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":49,"Thread":15,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user access type(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user access type(s)"}
[11:32:11.354] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (49ms) - 88 rows
2025-11-12 11:32:11 - [11:32:11.354] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (49ms) - 88 rows
[11:32:11.356] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
2025-11-12 11:32:11 - [11:32:11.356] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
[11:32:11.358] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (55ms)
2025-11-12 11:32:11 - [11:32:11.358] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (55ms)
2025-11-12 11:32:11 - [11:32:11.358] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_GetUserAccessType","ElapsedMs":55,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638985439313025760","Status":"SUCCESS","RowCount":88}
2025-11-12 11:32:11 - System_UserAccessType executed successfully for user: JOHNK
[11:32:11.373] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-12 11:32:11 - [11:32:11.373] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[11:32:11.375] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-12 11:32:11 - [11:32:11.375] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-12 11:32:11 - [ThemedUserControl] Control_ProgressBarUserControl initialized with automatic theme support
[11:32:11.435] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-12 11:32:11 - [11:32:11.435] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[11:32:11.466] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-12 11:32:11 - [11:32:11.466] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[11:32:11.467] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-12 11:32:11 - [11:32:11.467] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[11:32:11.469] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-12 11:32:11 - [11:32:11.469] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[11:32:11.470] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (97ms)
2025-11-12 11:32:11 - [11:32:11.470] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (97ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-12 11:32:11 - [ThemedUserControl] Using applier for _progressControl
2025-11-12 11:32:11 - [FormThemeApplier] Applying to '_progressControl' - FormBackColor: Color [A=255, R=30, G=30, B=30], ControlBackColor: Color [A=255, R=30, G=30, B=30], Final BackColor: Color [A=255, R=30, G=30, B=30]
2025-11-12 11:32:11 - [FormThemeApplier] Applying to '_progressControl' - FormForeColor: Color [A=255, R=255, G=255, B=255], ControlForeColor: Color [A=255, R=255, G=255, B=255], Final ForeColor: Color [A=255, R=255, G=255, B=255]
2025-11-12 11:32:11 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:11 - [FocusUtils] CanControlReceiveFocus:  (PictureBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:11 - [FocusUtils] ApplyFocusEventHandling:  (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:11 - [FocusUtils] CanControlReceiveFocus:  (ProgressBar) - FALSE (control type cannot receive focus)
2025-11-12 11:32:11 - [FocusUtils] ApplyFocusEventHandling:  (ProgressBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:11 - [FocusUtils] CanControlReceiveFocus:  (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:11 - [FocusUtils] ApplyFocusEventHandling:  (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:11 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-12-2025 @ 11-32 AM_normal.csv
2025-11-12 11:32:11 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-12 11:32:11 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-12 11:32:11 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-12 11:32:11 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-12 11:32:11 - [Splash] Starting async database connectivity verification
2025-11-12 11:32:11 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-12 11:32:11 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[11:32:11.913] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-12 11:32:11 - [11:32:11.913] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-12 11:32:11 - [11:32:11.913] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638985439319130111"}
[11:32:11.916] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:11 - [11:32:11.916] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:11.918] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-12 11:32:11 - [11:32:11.918] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[11:32:11.977] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (64ms) - Status: 1
2025-11-12 11:32:11 - [11:32:11.977] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (64ms) - Status: 1
2025-11-12 11:32:11 - [11:32:11.977] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_part_ids_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":64,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 3745 part(s)"},"ResultData":"DataTable[3745 rows]","ErrorMessage":"Retrieved 3745 part(s)"}
[11:32:11.980] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (64ms) - 3745 rows
2025-11-12 11:32:11 - [11:32:11.980] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (64ms) - 3745 rows
[11:32:11.982] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (66ms)
2025-11-12 11:32:11 - [11:32:11.982] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (66ms)
[11:32:11.984] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (71ms)
2025-11-12 11:32:11 - [11:32:11.984] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (71ms)
2025-11-12 11:32:11 - [11:32:11.984] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_part_ids_Get_All","ElapsedMs":71,"Key":"ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638985439319130111","Status":"SUCCESS","RowCount":3745}
2025-11-12 11:32:11 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-12 11:32:11 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), ItemType(String), Operations(String)
2025-11-12 11:32:11 - [DataTable] ComboBoxPart: Target schema:
2025-11-12 11:32:11 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[11:32:12.009] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-12 11:32:12 - [11:32:12.009] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-12 11:32:12 - [11:32:12.009] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638985439320091527"}
[11:32:12.012] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.012] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.014] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-12 11:32:12 - [11:32:12.014] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[11:32:12.043] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (34ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.043] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (34ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.043] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_operation_numbers_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":34,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 72 operation(s)"},"ResultData":"DataTable[72 rows]","ErrorMessage":"Retrieved 72 operation(s)"}
[11:32:12.048] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (34ms) - 72 rows
2025-11-12 11:32:12 - [11:32:12.048] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (34ms) - 72 rows
[11:32:12.050] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-12 11:32:12 - [11:32:12.050] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[11:32:12.052] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (43ms)
2025-11-12 11:32:12 - [11:32:12.052] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (43ms)
2025-11-12 11:32:12 - [11:32:12.052] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_operation_numbers_Get_All","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638985439320091527","Status":"SUCCESS","RowCount":72}
2025-11-12 11:32:12 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-12 11:32:12 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-12 11:32:12 - [DataTable] ComboBoxOperation: Target schema:
2025-11-12 11:32:12 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[11:32:12.061] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 11:32:12 - [11:32:12.061] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-12 11:32:12 - [11:32:12.061] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985439320613626"}
[11:32:12.064] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.064] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.066] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-12 11:32:12 - [11:32:12.066] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[11:32:12.144] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (83ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.144] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (83ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.144] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_locations_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":83,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 10371 location(s)"},"ResultData":"DataTable[10371 rows]","ErrorMessage":"Retrieved 10371 location(s)"}
[11:32:12.149] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (83ms) - 10371 rows
2025-11-12 11:32:12 - [11:32:12.149] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (83ms) - 10371 rows
[11:32:12.151] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (87ms)
2025-11-12 11:32:12 - [11:32:12.151] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (87ms)
[11:32:12.154] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (92ms)
2025-11-12 11:32:12 - [11:32:12.154] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (92ms)
2025-11-12 11:32:12 - [11:32:12.154] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_locations_Get_All","ElapsedMs":92,"Key":"ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638985439320613626","Status":"SUCCESS","RowCount":10371}
2025-11-12 11:32:12 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-12 11:32:12 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-12 11:32:12 - [DataTable] ComboBoxLocation: Target schema:
2025-11-12 11:32:12 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[11:32:12.172] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-12 11:32:12 - [11:32:12.172] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-12 11:32:12 - [11:32:12.172] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638985439321724919"}
[11:32:12.175] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.175] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.177] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-12 11:32:12 - [11:32:12.177] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[11:32:12.209] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (36ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.209] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (36ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.209] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":36,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 88 user(s)"},"ResultData":"DataTable[88 rows]","ErrorMessage":"Retrieved 88 user(s)"}
[11:32:12.212] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (36ms) - 88 rows
2025-11-12 11:32:12 - [11:32:12.212] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (36ms) - 88 rows
[11:32:12.219] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-12 11:32:12 - [11:32:12.219] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[11:32:12.221] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (48ms)
2025-11-12 11:32:12 - [11:32:12.221] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (48ms)
2025-11-12 11:32:12 - [11:32:12.221] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_All","ElapsedMs":48,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638985439321724919","Status":"SUCCESS","RowCount":88}
2025-11-12 11:32:12 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-12 11:32:12 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-12 11:32:12 - [DataTable] ComboBoxUser: Target schema:
2025-11-12 11:32:12 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[11:32:12.230] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-12 11:32:12 - [11:32:12.230] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-12 11:32:12 - [11:32:12.230] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638985439322306987"}
[11:32:12.233] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.233] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.235] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-12 11:32:12 - [11:32:12.235] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[11:32:12.266] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (35ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.266] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (35ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.266] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"md_item_types_Get_All","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 4 item type(s)"},"ResultData":"DataTable[4 rows]","ErrorMessage":"Retrieved 4 item type(s)"}
[11:32:12.269] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (35ms) - 4 rows
2025-11-12 11:32:12 - [11:32:12.269] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (35ms) - 4 rows
[11:32:12.271] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-12 11:32:12 - [11:32:12.271] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[11:32:12.273] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (42ms)
2025-11-12 11:32:12 - [11:32:12.273] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (42ms)
2025-11-12 11:32:12 - [11:32:12.273] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_md_item_types_Get_All","ElapsedMs":42,"Key":"ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638985439322306987","Status":"SUCCESS","RowCount":4}
2025-11-12 11:32:12 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-12 11:32:12 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-12 11:32:12 - [DataTable] ComboBoxItemType: Target schema:
2025-11-12 11:32:12 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-12 11:32:12 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-12 11:32:12 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-12 11:32:12 - Running VersionChecker - checking database version information.
[11:32:12.343] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-12 11:32:12 - [11:32:12.343] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-12 11:32:12 - [11:32:12.343] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638985439323436055"}
[11:32:12.346] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.346] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.349] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-12 11:32:12 - [11:32:12.349] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-12 11:32:12 - [Splash] Version checker initialized
[11:32:12.379] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (35ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.379] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (35ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.379] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"log_changelog_Get_Current","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved current changelog version"},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved current changelog version"}
[11:32:12.382] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (35ms) - 1 rows
2025-11-12 11:32:12 - [11:32:12.382] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (35ms) - 1 rows
[11:32:12.384] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-12 11:32:12 - [11:32:12.384] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[11:32:12.386] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (43ms)
2025-11-12 11:32:12 - [11:32:12.386] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (43ms)
2025-11-12 11:32:12 - [11:32:12.386] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_log_changelog_Get_Current","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638985439323436055","Status":"SUCCESS","RowCount":1}
Database version retrieved: 6.0.0.0
2025-11-12 11:32:12 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.1.0, DB: 6.0.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-12 11:32:12 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[11:32:12.418] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 11:32:12 - [11:32:12.418] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-12 11:32:12 - [11:32:12.418] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985439324188433"}
[11:32:12.421] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.421] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.423] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-12 11:32:12 - [11:32:12.423] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[11:32:12.430] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (11ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.430] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (11ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.430] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_theme_GetAll","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 9 theme(s)"},"ResultData":"DataTable[9 rows]","ErrorMessage":"Retrieved 9 theme(s)"}
[11:32:12.433] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (11ms) - 9 rows
2025-11-12 11:32:12 - [11:32:12.433] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (11ms) - 9 rows
[11:32:12.434] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-12 11:32:12 - [11:32:12.434] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[11:32:12.437] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-12 11:32:12 - [11:32:12.437] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-12 11:32:12 - [11:32:12.437] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_theme_GetAll","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638985439324188433","Status":"SUCCESS","RowCount":9}
2025-11-12 11:32:12 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-12 11:32:12 - Successfully loaded 9 themes from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Default' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Forest' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-12 11:32:12 - [DEBUG] Urban Bloom JSON preview: {"InfoColor": "#8E44AD", "ErrorColor": "#F44336", "AccentColor": "#8E44AD", "SuccessColor": "#BA68C8", "WarningColor": "#FF9800", "FormBackColor": "#F6F0FA", "FormForeColor": "#1A1A1A", "LabelBackColor": "#F6F0FA", "LabelForeColor": "#1A1A1A", "PanelBackColor": "#F6F0FA", "PanelForeColor": "#1A1A1A", "ButtonBackColor": "#EDE3F7", "ButtonForeColor": "#1A1A1A", "ControlBackColor": "#F6F0FA", "ControlForeColor": "#1A1A1A", "ListBoxBackColor": "#FFFFFF", "ListBoxForeColor": "#1A1A1A", "PanelBorderCo
2025-11-12 11:32:12 - [DEBUG] Urban Bloom deserialized - FormBackColor: Color [A=255, R=246, G=240, B=250], FormForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:12 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-12 11:32:12 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-12 11:32:12 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[11:32:12.485] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-12 11:32:12 - [11:32:12.485] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[11:32:12.488] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 11:32:12 - [11:32:12.488] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:32:12.490] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.490] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.490] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439324905149"}
[11:32:12.493] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.493] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.495] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.495] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:32:12.526] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (35ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.526] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (35ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.526] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":35,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:32:12.530] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (35ms) - 1 rows
2025-11-12 11:32:12 - [11:32:12.530] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (35ms) - 1 rows
[11:32:12.532] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-12 11:32:12 - [11:32:12.532] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[11:32:12.534] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (43ms)
2025-11-12 11:32:12 - [11:32:12.534] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (43ms)
2025-11-12 11:32:12 - [11:32:12.534] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":43,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439324905149","Status":"SUCCESS","RowCount":1}
[11:32:12.543] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (54ms)
2025-11-12 11:32:12 - [11:32:12.543] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (54ms)
[11:32:12.545] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (59ms)
2025-11-12 11:32:12 - [11:32:12.545] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (59ms)
2025-11-12 11:32:12 - Theme system enabled status for user JOHNK: True
[11:32:12.550] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-12 11:32:12 - [11:32:12.550] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[11:32:12.551] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 11:32:12 - [11:32:12.551] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:32:12.553] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.553] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.553] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439325539427"}
[11:32:12.556] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.556] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.558] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.558] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:32:12.563] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.563] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.563] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:32:12.566] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-12 11:32:12 - [11:32:12.566] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[11:32:12.568] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-12 11:32:12 - [11:32:12.568] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[11:32:12.570] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-12 11:32:12 - [11:32:12.570] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-12 11:32:12 - [11:32:12.570] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439325539427","Status":"SUCCESS","RowCount":1}
[11:32:12.573] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-12 11:32:12 - [11:32:12.573] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[11:32:12.576] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-12 11:32:12 - [11:32:12.576] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-12 11:32:12 - Loaded theme preference for user JOHNK: Urban Bloom
2025-11-12 11:32:12 - Set Model_Application_Variables.ThemeName to: Urban Bloom
2025-11-12 11:32:12 - Theme system initialized for user JOHNK. Final theme: Urban Bloom, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loading themes from database via Core_AppThemes
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeStore[0]
      Loaded 9 themes into ThemeStore cache
2025-11-12 11:32:12 - ThemeStore loaded 9 themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-12 11:32:12 - [Splash] ThemeStore loaded from database
2025-11-12 11:32:12 - [Splash] ThemeManager initialized with 'Urban Bloom' theme
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-12 11:32:12 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-12 11:32:12 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-12 11:32:12 - [Splash] Loading theme settings
[11:32:12.713] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-12 11:32:12 - [11:32:12.713] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[11:32:12.715] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 11:32:12 - [11:32:12.715] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:32:12.718] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.718] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.718] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439327182412"}
[11:32:12.722] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.722] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.724] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.724] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:32:12.729] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.729] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.729] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:32:12.732] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
2025-11-12 11:32:12 - [11:32:12.732] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
[11:32:12.734] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-12 11:32:12 - [11:32:12.734] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[11:32:12.736] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-12 11:32:12 - [11:32:12.736] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-12 11:32:12 - [11:32:12.736] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":18,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439327182412","Status":"SUCCESS","RowCount":1}
[11:32:12.740] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
2025-11-12 11:32:12 - [11:32:12.740] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
[11:32:12.742] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (29ms)
2025-11-12 11:32:12 - [11:32:12.742] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (29ms)
[11:32:12.744] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-12 11:32:12 - [11:32:12.744] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[11:32:12.746] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 11:32:12 - [11:32:12.746] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:32:12.748] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.748] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.748] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439327486446"}
[11:32:12.752] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.752] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.754] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.754] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:32:12.758] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.758] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.758] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:32:12.760] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-12 11:32:12 - [11:32:12.760] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[11:32:12.762] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
2025-11-12 11:32:12 - [11:32:12.762] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
[11:32:12.764] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-12 11:32:12 - [11:32:12.764] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-12 11:32:12 - [11:32:12.764] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439327486446","Status":"SUCCESS","RowCount":1}
[11:32:12.768] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
2025-11-12 11:32:12 - [11:32:12.768] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
[11:32:12.770] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (25ms)
2025-11-12 11:32:12 - [11:32:12.770] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (25ms)
[11:32:12.773] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-12 11:32:12 - [11:32:12.773] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[11:32:12.775] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-12 11:32:12 - [11:32:12.775] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[11:32:12.777] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.777] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.777] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439327776090"}
[11:32:12.780] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:12 - [11:32:12.780] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:12.782] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-12 11:32:12 - [11:32:12.782] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[11:32:12.787] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.787] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-12 11:32:12 - [11:32:12.787] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_ui_settings_Get","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":9,"Thread":1,"InputParameters":{"p_UserId":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved settings for user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved settings for user \"JOHNK\""}
[11:32:12.790] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-12 11:32:12 - [11:32:12.790] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[11:32:12.792] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-12 11:32:12 - [11:32:12.792] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[11:32:12.794] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-12 11:32:12 - [11:32:12.794] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-12 11:32:12 - [11:32:12.794] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_ui_settings_Get","ElapsedMs":16,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638985439327776090","Status":"SUCCESS","RowCount":1}
[11:32:12.797] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-12 11:32:12 - [11:32:12.797] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[11:32:12.799] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (26ms)
2025-11-12 11:32:12 - [11:32:12.799] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (26ms)
2025-11-12 11:32:12 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-12 11:32:12 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-12 11:32:12 - [Splash] Core startup sequence completed
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeDebouncer[0]
      Applying debounced theme change: Urban Bloom (Reason: Login)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Theme changed to 'Urban Bloom' (Reason: Login, User: JOHNK)
2025-11-12 11:32:12 - Theme changed to 'Urban Bloom' - Reason: Login
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form '' subscribed to theme changes
2025-11-12 11:32:13 - [ThemedForm] MainForm initialized with automatic theme support
[11:32:13.180] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-12 11:32:13 - [11:32:13.180] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[11:32:13.183] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-12 11:32:13 - [11:32:13.183] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-12 11:32:13 - [ThemedUserControl] Control_ConnectionStrengthControl initialized with automatic theme support
2025-11-12 11:32:13 - [ThemedUserControl] Control_InventoryTab initialized with automatic theme support
[11:32:13.214] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.214] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[11:32:13.216] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.216] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[11:32:13.228] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.228] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
[11:32:13.232] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.232] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[11:32:13.235] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.235] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[11:32:13.236] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.236] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[11:32:13.239] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.239] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[11:32:13.241] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.241] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-12 11:32:13 - Inventory tab events wired up.
[11:32:13.245] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.245] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[11:32:13.248] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.248] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-12 11:32:13 - Inventory Quantity TextBox changed.
[11:32:13.253] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.253] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[11:32:13.255] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-12 11:32:13 - [11:32:13.255] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[11:32:13.258] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (43ms)
2025-11-12 11:32:13 - [11:32:13.258] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (43ms)
[11:32:13.261] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-12 11:32:13 - [11:32:13.261] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[11:32:13.263] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-12 11:32:13 - [11:32:13.263] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-12 11:32:13 - Control_AdvancedInventory constructor entered.
[11:32:13.275] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-12 11:32:13 - [11:32:13.275] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
[11:32:13.277] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-12 11:32:13 - [11:32:13.277] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - Control_AdvancedInventory constructor exited.
[11:32:13.492] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.492] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[11:32:13.494] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.494] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[11:32:13.504] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.504] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
[11:32:13.507] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.507] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[11:32:13.510] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.510] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[11:32:13.512] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.512] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[11:32:13.525] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.525] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[11:32:13.527] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.527] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[11:32:13.530] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.530] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[11:32:13.532] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-12 11:32:13 - [11:32:13.532] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[11:32:13.534] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (41ms)
2025-11-12 11:32:13 - [11:32:13.534] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (41ms)
[11:32:13.537] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-12 11:32:13 - [11:32:13.537] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[11:32:13.539] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-12 11:32:13 - [11:32:13.539] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[11:32:13.549] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-12 11:32:13 - [11:32:13.549] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
[11:32:13.551] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-12 11:32:13 - [11:32:13.551] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top (Panel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_To (DateTimePicker)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_DateTimePicker_To
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_To
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_DateTimePicker_From (DateTimePicker)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_DateTimePicker_From
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_DateTimePicker_From
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Location (TextBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_TextBox_Location
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_TextBox_Location
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Location
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Location
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Part (TextBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_TextBox_Part
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_TextBox_Part
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Part
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Part
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date (CheckBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_ComboBox_User (ComboBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_ComboBox_User
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_ComboBox_User
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached DropDown handler for ComboBox Control_AdvancedRemove_ComboBox_User
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_ComboBox_User
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Operation (TextBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_TextBox_Operation
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Operation
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Operation
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_Notes (TextBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_TextBox_Notes
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_Notes
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_Notes
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMin (TextBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMin
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_AdvancedRemove_TextBox_QtyMax (TextBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Click handler for TextBox Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_AdvancedRemove_TextBox_QtyMax
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center (Panel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results (DataGridView) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
[11:32:13.728] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-12 11:32:13 - [11:32:13.728] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[11:32:13.737] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-12 11:32:13 - [11:32:13.737] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[11:32:13.739] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 11:32:13 - [11:32:13.739] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_ComboBox_Operation (ComboBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_TransferTab_ComboBox_Operation (ComboBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_TransferTab_ComboBox_Operation
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_TransferTab_ComboBox_Operation
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached DropDown handler for ComboBox Control_TransferTab_ComboBox_Operation
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_TransferTab_ComboBox_Operation
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_ComboBox_Part (ComboBox) - APPLYING
2025-11-12 11:32:13 - [FocusUtils] Apply: Starting for Control_TransferTab_ComboBox_Part (ComboBox)
2025-11-12 11:32:13 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_TransferTab_ComboBox_Part
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_TransferTab_ComboBox_Part
2025-11-12 11:32:13 - [FocusUtils] Apply: Attached DropDown handler for ComboBox Control_TransferTab_ComboBox_Part
2025-11-12 11:32:13 - [FocusUtils] Apply: Completed for Control_TransferTab_ComboBox_Part
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_ComboBox_ToLocation - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_ComboBox_ToLocation (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: panel1 (Panel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView (Panel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main (DataGridView) - FALSE (control type cannot receive focus)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:13 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:13 - Transfer tab events wired up.
[11:32:13.856] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 11:32:13 - [11:32:13.856] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-12 11:32:13 - [ThemedUserControl] Control_QuickButtons initialized with automatic theme support
[11:32:13.860] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-12 11:32:13 - [11:32:13.860] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[11:32:13.862] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-12 11:32:13 - [11:32:13.862] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[11:32:13.866] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-12 11:32:13 - [11:32:13.866] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[11:32:13.930] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-12 11:32:13 - [11:32:13.930] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-12 11:32:13 - Inventory Part ComboBox selection changed.
2025-11-12 11:32:13 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|25_1
[11:32:13.988] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-12 11:32:13 - [11:32:13.988] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[11:32:13.991] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-12 11:32:13 - [11:32:13.991] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[11:32:13.994] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-12 11:32:13 - [11:32:13.994] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[11:32:13.997] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-12 11:32:13 - [11:32:13.997] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[11:32:13.999] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (5ms)
2025-11-12 11:32:14 - [11:32:13.999] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (5ms)
[11:32:14.003] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-12 11:32:14 - [11:32:14.003] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[11:32:14.005] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-12 11:32:14 - [11:32:14.005] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[11:32:14.007] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (16ms)
2025-11-12 11:32:14 - [11:32:14.007] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (16ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[11:32:14.013] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-12 11:32:14 - [11:32:14.013] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[11:32:14.016] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-12 11:32:14 - [11:32:14.016] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[11:32:14.019] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-12 11:32:14 - [11:32:14.019] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[11:32:14.021] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-12 11:32:14 - [11:32:14.021] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[11:32:14.026] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-12 11:32:14 - [11:32:14.026] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[11:32:14.029] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-12 11:32:14 - [11:32:14.029] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-12 11:32:14 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[11:32:14.034] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-12 11:32:14 - [11:32:14.034] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[11:32:14.036] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
2025-11-12 11:32:14 - [11:32:14.036] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[11:32:14.040] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-12 11:32:14 - [11:32:14.040] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[11:32:14.043] [MEDIUM] ⬅️ EXITING MainForm.MainForm (862ms)
2025-11-12 11:32:14 - [11:32:14.043] [MEDIUM] ⬅️ EXITING MainForm.MainForm (862ms)
2025-11-12 11:32:14 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-12 11:32:14 - Inventory tab suggestion controls configured.
[11:32:14.050] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (810ms)
2025-11-12 11:32:14 - [11:32:14.050] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (810ms)
2025-11-12 11:32:14 - Remove tab ComboBoxes loaded.
2025-11-12 11:32:14 - Removal tab events wired up.
2025-11-12 11:32:14 - Initial setup of ComboBoxes in the Remove Tab.
[11:32:14.058] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-12 11:32:14 - [11:32:14.058] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[11:32:14.060] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.060] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.060] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985439340600917"}
[11:32:14.063] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:14 - [11:32:14.063] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:14.065] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.065] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 11:32:14 - [Splash] All form instances configured successfully
2025-11-12 11:32:14 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-12 11:32:14 - Inventory Op ComboBox selection changed.
2025-11-12 11:32:14 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|25_1
2025-11-12 11:32:14 - [Splash] MainForm uses ThemedForm - automatic theme application
2025-11-12 11:32:14 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
2025-11-12 11:32:14 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|25_1
2025-11-12 11:32:14 - Transfer tab ComboBoxes loaded.
[11:32:14.098] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[11:32:14.098] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (38ms) - Status: 1
2025-11-12 11:32:14 - [11:32:14.098] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-12 11:32:14 - [11:32:14.098] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (38ms) - Status: 1
[11:32:14.101] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.101] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.101] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985439341018826"}
[11:32:14.106] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:14 - [11:32:14.106] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:14.108] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.108] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 11:32:14 - [11:32:14.098] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":38,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[11:32:14.116] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (38ms) - 1 rows
2025-11-12 11:32:14 - [11:32:14.116] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (38ms) - 1 rows
[11:32:14.118] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (54ms)
2025-11-12 11:32:14 - [11:32:14.118] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (54ms)
[11:32:14.120] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (60ms)
2025-11-12 11:32:14 - [11:32:14.120] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (60ms)
2025-11-12 11:32:14 - [11:32:14.120] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":60,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985439340600917","Status":"SUCCESS","RowCount":1}
[11:32:14.124] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (66ms)
2025-11-12 11:32:14 - [11:32:14.124] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (66ms)
2025-11-12 11:32:14 - User full name loaded: John Koll
[11:32:14.135] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (33ms) - Status: 1
2025-11-12 11:32:14 - [11:32:14.135] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (33ms) - Status: 1
2025-11-12 11:32:14 - [11:32:14.135] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":33,"Thread":14,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[11:32:14.138] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (33ms) - 1 rows
2025-11-12 11:32:14 - [11:32:14.138] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (33ms) - 1 rows
[11:32:14.141] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (34ms)
2025-11-12 11:32:14 - [11:32:14.141] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (34ms)
[11:32:14.143] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (41ms)
2025-11-12 11:32:14 - [11:32:14.143] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (41ms)
2025-11-12 11:32:14 - [11:32:14.143] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":41,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985439341018826","Status":"SUCCESS","RowCount":1}
[11:32:14.146] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (48ms)
2025-11-12 11:32:14 - [11:32:14.146] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (48ms)
2025-11-12 11:32:14 - User full name loaded: John Koll
2025-11-12 11:32:14 - [ThemedUserControl] Using applier for MainForm_UserControl_InventoryTab
2025-11-12 11:32:14 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:14 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [ThemedUserControl] Using applier for MainForm_UserControl_QuickButtons
2025-11-12 11:32:14 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:14 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[11:32:14.726] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-12 11:32:14 - [11:32:14.726] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 0 controls
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_TableLayout (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_MenuStrip (MenuStrip) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_MenuStrip (MenuStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_SplitContainer_Middle (SplitContainer) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_SplitContainer_Middle (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_TabControl (TabControl) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Inventory (TabPage) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Inventory (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_InventoryTab (Control_InventoryTab) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_InventoryTab (Control_InventoryTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_GroupBox_Main (GroupBox) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_MiddleGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Notes (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_RichTextBox_Notes (RichTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_RichTextBox_Notes (RichTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_RichTextBox_Notes
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_RichTextBox_Notes
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_TopGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Location (SuggestionTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Location (SuggestionTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Location
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Quantity (TextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Quantity (TextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Quantity
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Operation (SuggestionTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Operation (SuggestionTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Operation
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TextBox_Part (SuggestionTextBox) - APPLYING
2025-11-12 11:32:14 - [FocusUtils] Apply: Starting for Control_InventoryTab_TextBox_Part (SuggestionTextBox)
2025-11-12 11:32:14 - [FocusUtils] Apply: Removed old Enter/Leave handlers for Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Enter/Leave handlers for Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] Apply: Attached Click handler for TextBox Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] Apply: Completed for Control_InventoryTab_TextBox_Part
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Loc (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Qty (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Op (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Part (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_TableLayout_BottomGroup (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 5 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Toggle_RightPanel (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Label_Version (Label) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Label_Version (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Save - FALSE (Enabled=False, Visible=True)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_AdvancedEntry (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_AdvancedEntry (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: Control_InventoryTab_Button_Reset (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: Control_InventoryTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedInventory - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedInventory (Control_AdvancedInventory) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl (TabControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Single (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayout_Single - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayout_Single (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Right (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ListView - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ListView (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_LowerRight - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_LowerRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_GroupBox_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_GroupBox_Left (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 13 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Button_Send - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Button_Send (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Count (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_TextBox_Count - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_TextBox_Count (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: panel4 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: panel4 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Single_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Single_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_MultiLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_MultiLoc (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TableLayoutPanel_Multi - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TableLayoutPanel_Multi (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Preview (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Right - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Right (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_SaveAll - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:14 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_SaveAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:14 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ListView_Preview - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ListView_Preview (ListView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_GroupBox_Item - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_GroupBox_Item (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: panel2 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: panel2 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Button_AddLoc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Button_AddLoc (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Multi_TableLayout_Left - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Multi_TableLayout_Left (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Op (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_TextBox_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_TextBox_Qty (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_ComboBox_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_ComboBox_Loc (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: panel3 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: panel3 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_MultiLoc_RichTextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_MultiLoc_RichTextBox_Notes (RichTextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_TabControl_Import - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_TabControl_Import (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Panel_Middle - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Panel_Middle (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_DataGridView (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_Save - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_Save (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_OpenExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_OpenExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: AdvancedInventory_Import_Button_ImportExcel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: AdvancedInventory_Import_Button_ImportExcel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Remove - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Remove (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_RemoveTab - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_RemoveTab (Control_RemoveTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Panel_Header - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Panel_Header (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_ComboBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_ComboBox_Operation (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 8 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_ShowAll - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_ShowAll (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_AdvancedItemRemoval - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_AdvancedItemRemoval (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Delete - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_RemoveTab_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_RemoveTab_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_AdvancedRemove - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_AdvancedRemove (Control_AdvancedRemove) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_GroupBox_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_GroupBox_Main (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Row4 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Row4 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomRight - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomRight (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Normal - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Normal (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_BottomLeft - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_BottomLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Search - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Undo - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Undo (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_SidePanel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_SidePanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Button_Delete - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Button_Delete (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Top (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_TopLeft - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_TopLeft (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 14 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_DateRange - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_DateRange (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_To - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_To (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DateTimePicker_From - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DateTimePicker_From (DateTimePicker) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_DateDash - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_DateDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Location - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Location (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Part (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Loc - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Loc (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Op - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Op (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_User - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_User (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Notes (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_CheckBox_Date - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_CheckBox_Date (CheckBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Qty - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Qty (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_ComboBox_User - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_ComboBox_User (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Operation (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_Notes - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_Notes (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TableLayout_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TableLayout_Quantity (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 3 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMin - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMin (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_TextBox_QtyMax - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_TextBox_QtyMax (TextBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Label_QtyDash - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Label_QtyDash (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Panel_Row4_Center - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Panel_Row4_Center (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_AdvancedRemove_DataGridView_Results - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_AdvancedRemove_DataGridView_Results (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_TabPage_Transfer - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_TabPage_Transfer (TabPage) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_TransferTab - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_TransferTab (Control_TransferTab) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_GroupBox_MainControl - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_GroupBox_MainControl (GroupBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_Database_TableLayout_Top - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_Database_TableLayout_Top (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_Split - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_Split (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Toggle_RightPanel - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Toggle_RightPanel (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_SplitContainer_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_SplitContainer_Main (SplitContainer) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 9 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_ComboBox_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_ComboBox_Operation (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_ComboBox_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_ComboBox_Part (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_NumericUpDown_Quantity - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_NumericUpDown_Quantity (NumericUpDown) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (UpDownButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (UpDownEdit) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_ComboBox_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_ComboBox_ToLocation (ComboBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Part - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Part (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Operation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Operation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_ToLocation - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_ToLocation (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Label_Quantity - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Label_Quantity (Label) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_Shortcuts_TableLayout_Bottom - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_Shortcuts_TableLayout_Bottom (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 4 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Transfer - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Transfer (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Search - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Search (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Print - FALSE (Enabled=False, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Print (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Button_Reset - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Button_Reset (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: panel1 - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: panel1 (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Panel_DataGridView - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Panel_DataGridView (Panel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_Image_NothingFound - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_Image_NothingFound (PictureBox) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_TransferTab_DataGridView_Main - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_TransferTab_DataGridView_Main (DataGridView) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (HScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  - FALSE (Enabled=True, Visible=False)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (VScrollBar) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  (SplitterPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (SplitterPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_QuickButtons (Control_QuickButtons) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_QuickButtons (Control_QuickButtons) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 1 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_TableLayoutPanel_Main (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 10 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button1 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button1 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button2 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button2 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button3 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button3 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button4 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button4 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button5 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button5 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button6 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button6 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button7 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button7 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button8 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button8 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button9 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button9 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: Control_QuickButtons_Button_Button10 (Button) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: Control_QuickButtons_Button_Button10 (Button) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: tableLayoutPanel1 (TableLayoutPanel) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: tableLayoutPanel1 (TableLayoutPanel) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandlingToControls: Processing 2 controls
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_StatusStrip (StatusStrip) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_StatusStrip (StatusStrip) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling: MainForm_UserControl_SignalStrength (Control_ConnectionStrengthControl) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [FocusUtils] CanControlReceiveFocus:  (MdiClient) - FALSE (control type cannot receive focus)
2025-11-12 11:32:15 - [FocusUtils] ApplyFocusEventHandling:  (MdiClient) - SKIPPED (CanControlReceiveFocus=false)
2025-11-12 11:32:15 - [Splash] MainForm displayed successfully
2025-11-12 11:32:15 - [Splash] MainForm displayed - startup complete
2025-11-12 11:32:15 - [Splash] Splash screen closed
[11:32:15.518] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-12 11:32:15 - [11:32:15.518] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-12 11:32:15 -
2025-11-12 11:32:15 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-12 11:32:15 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-12 11:32:15 - [QuickButtons]   User: JOHNK
2025-11-12 11:32:15 - [QuickButtons] ════════════════════════════════════════════════════════════
[11:32:15.529] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-12 11:32:15 - [11:32:15.529] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-12 11:32:15 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-12 11:32:15 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[11:32:15.536] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.536] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.536] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985439355363665"}
[11:32:15.540] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:15 - [11:32:15.540] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:15.543] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.543] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [ThemedForm] Using FormThemeApplier for MainForm
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_InventoryTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedInventory' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_RemoveTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_AdvancedRemove' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_TransferTab' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_QuickButtons' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormBackColor: Color [A=255, R=246, G=240, B=250], ControlBackColor: Color [A=255, R=246, G=240, B=250], Final BackColor: Color [A=255, R=246, G=240, B=250]
2025-11-12 11:32:15 - [FormThemeApplier] Applying to 'MainForm_UserControl_SignalStrength' - FormForeColor: Color [A=255, R=26, G=26, B=26], ControlForeColor: Color [A=255, R=26, G=26, B=26], Final ForeColor: Color [A=255, R=26, G=26, B=26]
[Theme] Applied theme to form 'MainForm' in 41ms
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[11:32:15.597] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-12 11:32:15 - [11:32:15.597] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[11:32:15.599] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.599] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.599] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985439355994013"}
[11:32:15.602] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:15 - [11:32:15.602] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:15.605] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.605] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[11:32:15.611] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (11ms) - Status: 1
2025-11-12 11:32:15 - [11:32:15.611] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (11ms) - Status: 1
2025-11-12 11:32:15 - [11:32:15.611] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"usr_users_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved user \"JOHNK\""},"ResultData":"DataTable[1 rows]","ErrorMessage":"Retrieved user \"JOHNK\""}
[11:32:15.617] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (11ms) - 1 rows
2025-11-12 11:32:15 - [11:32:15.617] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (11ms) - 1 rows
[11:32:15.619] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-12 11:32:15 - [11:32:15.619] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[11:32:15.622] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (23ms)
2025-11-12 11:32:15 - [11:32:15.622] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (23ms)
2025-11-12 11:32:15 - [11:32:15.622] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_usr_users_Get_ByUser","ElapsedMs":23,"Key":"ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638985439355994013","Status":"SUCCESS","RowCount":1}
[11:32:15.626] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (28ms)
2025-11-12 11:32:15 - [11:32:15.626] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (28ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[11:32:15.630] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-12 11:32:15 - [11:32:15.630] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[11:32:15.635] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-12 11:32:15 - [11:32:15.635] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[11:32:15.637] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-12 11:32:15 - [11:32:15.637] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-12 11:32:15 - Application Info - Development Menu configured for user 'JOHNK': Visible
[11:32:15.641] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (10ms)
2025-11-12 11:32:15 - [11:32:15.641] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (10ms)
[11:32:15.645] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (109ms) - Status: 1
2025-11-12 11:32:15 - [11:32:15.645] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (109ms) - Status: 1
2025-11-12 11:32:15 - [11:32:15.645] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":109,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 8 transaction(s) for user: JOHNK"},"ResultData":"DataTable[8 rows]","ErrorMessage":"Retrieved 8 transaction(s) for user: JOHNK"}
[11:32:15.649] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (109ms) - 8 rows
2025-11-12 11:32:15 - [11:32:15.649] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (109ms) - 8 rows
[11:32:15.652] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (110ms)
2025-11-12 11:32:15 - [11:32:15.652] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (110ms)
[11:32:15.654] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (118ms)
2025-11-12 11:32:15 - [11:32:15.654] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (118ms)
2025-11-12 11:32:15 - [11:32:15.654] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":118,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985439355363665","Status":"SUCCESS","RowCount":8}
2025-11-12 11:32:15 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 21-28841-006 + 19 (Qty: 500)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 21179 + 15 (Qty: 500)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 06-96408-001 + N/A (Qty: 40)
2025-11-12 11:32:15 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 10)
2025-11-12 11:32:15 - [Dao_QuickButtons] Array restructured: 8 unique buttons, 0 duplicates removed
2025-11-12 11:32:15 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-12 11:32:15 - [Dao_QuickButtons] All buttons deleted from database
2025-11-12 11:32:15 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 1: 21-28841-006 + 19 (Qty: 500)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 2: 21179 + 15 (Qty: 500)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 3: 01-31976-000 + 10 (Qty: 1)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 4: 04-27693-000 + 90 (Qty: 10)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 5: 01-34578-000 + 880 (Qty: 20)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 6: 03-29236-030 + 959 (Qty: 30)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 7: 06-96408-001 + N/A (Qty: 40)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created button at position 8: 01-33016-000 + 109 (Qty: 10)
2025-11-12 11:32:15 - [Dao_QuickButtons] Created 8 buttons in database
2025-11-12 11:32:15 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 8 buttons remain
2025-11-12 11:32:15 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-12 11:32:15 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 8 buttons remain
2025-11-12 11:32:15 - [QuickButtons] STEP 2: Loading data from database
[11:32:15.783] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.783] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.783] [DATA  ] {"Action":"PERFORMANCE_START","Operation":"SP_sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985439357839263"}
[11:32:15.788] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-12 11:32:15 - [11:32:15.788] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[11:32:15.790] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-12 11:32:15 - [11:32:15.790] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[11:32:15.795] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-12 11:32:15 - [11:32:15.795] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-12 11:32:15 - [11:32:15.795] [DATA  ] {"Action":"STORED_PROCEDURE_EXECUTION","Procedure":"sys_last_10_transactions_Get_ByUser","Caller":"ExecuteDataTableWithStatusAsync","Status":1,"ElapsedMs":11,"Thread":1,"InputParameters":{"p_User":"JOHNK"},"OutputParameters":{"Status":"1","ErrorMsg":"Retrieved 8 transaction(s) for user: JOHNK"},"ResultData":"DataTable[8 rows]","ErrorMessage":"Retrieved 8 transaction(s) for user: JOHNK"}
[11:32:15.798] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 8 rows
2025-11-12 11:32:15 - [11:32:15.798] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 8 rows
[11:32:15.800] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-12 11:32:15 - [11:32:15.800] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[11:32:15.802] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-12 11:32:15 - [11:32:15.802] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-12 11:32:15 - [11:32:15.802] [DATA  ] {"Action":"PERFORMANCE_COMPLETE","Operation":"SP_sys_last_10_transactions_Get_ByUser","ElapsedMs":19,"Key":"ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638985439357839263","Status":"SUCCESS","RowCount":8}
[11:32:15.809] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-12 11:32:15 - [11:32:15.809] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-12 11:32:15 - [QuickButtons] STEP 2: ✓ Retrieved 8 button(s) from database
2025-11-12 11:32:15 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 1: 21-28841-006 + Op:19 (Qty: 500)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 2: 21179 + Op:15 (Qty: 500)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 3: 01-31976-000 + Op:10 (Qty: 1)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 4: 04-27693-000 + Op:90 (Qty: 10)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 5: 01-34578-000 + Op:880 (Qty: 20)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 6: 03-29236-030 + Op:959 (Qty: 30)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 7: 06-96408-001 + Op:N/A (Qty: 40)
2025-11-12 11:32:15 - [QuickButtons] STEP 3:   Button 8: 01-33016-000 + Op:109 (Qty: 10)
2025-11-12 11:32:15 - [QuickButtons] STEP 3: Filled 8 button(s) with data
2025-11-12 11:32:15 - [QuickButtons] STEP 3: Clearing remaining 2 button(s)
2025-11-12 11:32:15 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-12 11:32:15 - [QuickButtons] STEP 4: Layout refreshed - 8 visible button(s)
2025-11-12 11:32:15 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-12 11:32:15 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-12 11:32:15 - [QuickButtons] ║ User: JOHNK
2025-11-12 11:32:15 - [QuickButtons] ║ Visible Buttons: 8
2025-11-12 11:32:15 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-12 11:32:15 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-12 11:32:15 -
[11:32:15.875] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (344ms)
2025-11-12 11:32:15 - [11:32:15.875] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (344ms)
[11:32:15.877] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-12 11:32:15 - [11:32:15.877] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
[11:32:17.701] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-12 11:32:17 - [11:32:17.701] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:32:17.705] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:17 - [11:32:17.705] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:17 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Part - Checking if should restore normal BackColor
2025-11-12 11:32:17 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Part - Control not focused, restoring normal BackColor
2025-11-12 11:32:17 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Part
[11:32:18.076] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-12 11:32:18 - [11:32:18.076] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:32:18.078] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:18 - [11:32:18.078] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:18 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Operation - Checking if should restore normal BackColor
2025-11-12 11:32:18 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Operation - Control not focused, restoring normal BackColor
2025-11-12 11:32:18 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Operation
[11:32:18.465] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-12 11:32:18 - [11:32:18.465] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:32:18.468] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:18 - [11:32:18.468] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:18 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Quantity - Checking if should restore normal BackColor
2025-11-12 11:32:18 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Quantity - Control not focused, restoring normal BackColor
2025-11-12 11:32:18 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Quantity
[11:32:18.836] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-12 11:32:18 - [11:32:18.836] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:32:18.838] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:18 - [11:32:18.838] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:18 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Location - Checking if should restore normal BackColor
2025-11-12 11:32:18 - [FocusUtils] Control_Leave_Handler: Control_InventoryTab_TextBox_Location - Control not focused, restoring normal BackColor
2025-11-12 11:32:18 - [FocusUtils] BackColor set to: Color [A=255, R=255, G=255, B=255] for Control_InventoryTab_TextBox_Location
[ConnectionRecovery] UpdateConnectionStrengthAsync called
[ConnectionRecovery] Strength: 5, Ping: 2, TimerActive: False
[11:32:19.229] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-12 11:32:19 - [11:32:19.229] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:32:19.231] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:19 - [11:32:19.231] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[11:32:19.605] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-12 11:32:19 - [11:32:19.605] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[11:32:19.607] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-12 11:32:19 - [11:32:19.607] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
info: MTM_WIP_Application_Winforms.Core.Theming.ThemeManager[0]
      Form 'MainForm' unsubscribed from theme changes
2025-11-12 11:32:21 - [Cleanup] Starting application cleanup
2025-11-12 11:32:21 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-12 11:32:21 - [Cleanup] Memory cleanup completed
2025-11-12 11:32:21 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-12 11:32:21 - [Startup] Application shutdown completed
2025-11-12 11:32:21 - [Cleanup] Starting application cleanup
2025-11-12 11:32:21 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-12 11:32:21 - [Cleanup] Memory cleanup completed
2025-11-12 11:32:21 - [Cleanup] Application cleanup completed successfully
2025-11-12 11:32:21 - [Cleanup] Starting application cleanup
2025-11-12 11:32:21 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-12 11:32:21 - [Cleanup] Memory cleanup completed
2025-11-12 11:32:21 - [Cleanup] Application cleanup completed successfully
The program '[25512] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
