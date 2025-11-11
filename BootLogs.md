------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[20:21:43.099] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-09 20:21:43 - [20:21:43.099] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[20:21:43.134] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-09 20:21:43 - [20:21:43.134] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[20:21:43.136] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-09 20:21:43 - [20:21:43.136] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[20:21:43.138] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-09 20:21:43 - [20:21:43.138] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-09 20:21:43 - [Startup] Application initialization started
2025-11-09 20:21:43 - [Startup] User identified: JOHNK
2025-11-09 20:21:43 - [Dao_System] Checking database connectivity
[20:21:43.173] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-09 20:21:43 - [20:21:43.173] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-09 20:21:43 - [20:21:43.173] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638983165031727587"
}
[20:21:43.247] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:43 - [20:21:43.247] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:43.249] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-09 20:21:43 - [20:21:43.249] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[20:21:43.438] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (265ms) - Status: 1
2025-11-09 20:21:43 - [20:21:43.438] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (265ms) - Status: 1
2025-11-09 20:21:43 - [20:21:43.438] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 265,
  "Thread": 15,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 9 theme(s)"
  },
  "ResultData": "DataTable[9 rows]",
  "ErrorMessage": "Retrieved 9 theme(s)"
}
[20:21:43.453] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (265ms) - 9 rows
2025-11-09 20:21:43 - [20:21:43.453] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (265ms) - 9 rows
[20:21:43.455] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (207ms)
2025-11-09 20:21:43 - [20:21:43.455] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (207ms)
[20:21:43.457] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (284ms)
2025-11-09 20:21:43 - [20:21:43.457] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (284ms)
2025-11-09 20:21:43 - [20:21:43.457] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_theme_GetAll",
  "ElapsedMs": 284,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638983165031727587",
  "Status": "SUCCESS",
  "RowCount": 9
}
2025-11-09 20:21:43 - [Dao_System] Database connectivity check passed
2025-11-09 20:21:43 - [Startup] Database connectivity validated successfully
2025-11-09 20:21:43 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-09 20:21:43 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-09 20:21:43 - [Startup] Parameter cache populated: 116 procedures, 519 total parameters
2025-11-09 20:21:43 - [Startup] Parameter prefix cache initialized successfully in 14ms. Cached 116 stored procedures.
[Startup] Parameter cache: 116 procedures cached in 14ms
[20:21:43.484] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-09 20:21:43 - [20:21:43.484] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-09 20:21:43 - [20:21:43.484] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_GetUserAccessType",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638983165034841936"
}
[20:21:43.486] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:43 - [20:21:43.486] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:43.488] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-09 20:21:43 - [20:21:43.488] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-09 20:21:43 - [Splash] Initializing splash screen
2025-11-09 20:21:43 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
[20:21:43.509] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (25ms) - Status: 1
2025-11-09 20:21:43 - [20:21:43.509] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (25ms) - Status: 1
2025-11-09 20:21:43 - [20:21:43.509] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_GetUserAccessType",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 25,
  "Thread": 8,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user access type(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user access type(s)"
}
[20:21:43.512] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (25ms) - 88 rows
2025-11-09 20:21:43 - [20:21:43.512] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (25ms) - 88 rows
[20:21:43.514] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (26ms)
2025-11-09 20:21:43 - [20:21:43.514] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (26ms)
[20:21:43.515] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (31ms)
2025-11-09 20:21:43 - [20:21:43.515] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (31ms)
2025-11-09 20:21:43 - [20:21:43.515] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_GetUserAccessType",
  "ElapsedMs": 31,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638983165034841936",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-09 20:21:43 - System_UserAccessType executed successfully for user: JOHNK
[20:21:43.522] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-09 20:21:43 - [20:21:43.522] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[20:21:43.525] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-09 20:21:43 - [20:21:43.525] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-09 20:21:43 - DPI scaling applied to user control 'Control_ProgressBarUserControl' and all its controls.
2025-11-09 20:21:43 - Runtime layout adjustments applied to user control 'Control_ProgressBarUserControl'.
[20:21:43.549] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-09 20:21:43 - [20:21:43.549] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-09 20:21:43 - DPI scaling applied to form 'SplashScreenForm' and all its controls.
2025-11-09 20:21:43 - Runtime layout adjustments applied to form 'SplashScreenForm'.
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[20:21:43.583] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-09 20:21:43 - [20:21:43.583] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[20:21:43.585] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-09 20:21:43 - [20:21:43.585] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[DEBUG] [SplashScreenForm.ApplyTheme] Applying theme...
2025-11-09 20:21:43 - DPI scaling applied to form 'SplashScreenForm' and all its controls.
2025-11-09 20:21:43 - Runtime layout adjustments applied to form 'SplashScreenForm'.
2025-11-09 20:21:43 - Global theme 'Default' with DPI scaling applied to form 'SplashScreenForm'.
[DEBUG] [SplashScreenForm.ApplyTheme] Theme applied.
[20:21:43.598] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-09 20:21:43 - [20:21:43.598] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[20:21:43.600] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (77ms)
2025-11-09 20:21:43 - [20:21:43.600] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (77ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-09 20:21:43 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-09-2025 @ 8-21 PM_normal.log
2025-11-09 20:21:43 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-09 20:21:43 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-09 20:21:43 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-09 20:21:43 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-09 20:21:43 - [Splash] Starting async database connectivity verification
2025-11-09 20:21:43 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-09 20:21:43 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[20:21:44.032] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-09 20:21:44 - [20:21:44.032] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-09 20:21:44 - [20:21:44.032] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_part_ids_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638983165040320719"
}
[20:21:44.035] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.035] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.037] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-09 20:21:44 - [20:21:44.037] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[20:21:44.081] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (49ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.081] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (49ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.081] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_part_ids_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 49,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3745 part(s)"
  },
  "ResultData": "DataTable[3745 rows]",
  "ErrorMessage": "Retrieved 3745 part(s)"
}
[20:21:44.084] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (49ms) - 3745 rows
2025-11-09 20:21:44 - [20:21:44.084] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (49ms) - 3745 rows
[20:21:44.086] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
2025-11-09 20:21:44 - [20:21:44.086] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (51ms)
[20:21:44.088] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (56ms)
2025-11-09 20:21:44 - [20:21:44.088] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (56ms)
2025-11-09 20:21:44 - [20:21:44.088] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_part_ids_Get_All",
  "ElapsedMs": 56,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638983165040320719",
  "Status": "SUCCESS",
  "RowCount": 3745
}
2025-11-09 20:21:44 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-09 20:21:44 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), ItemType(String), Operations(String)
2025-11-09 20:21:44 - [DataTable] ComboBoxPart: Target schema:
2025-11-09 20:21:44 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[20:21:44.119] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-09 20:21:44 - [20:21:44.119] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-09 20:21:44 - [20:21:44.119] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_operation_numbers_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638983165041196315"
}
[20:21:44.122] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.122] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.124] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-09 20:21:44 - [20:21:44.124] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[20:21:44.143] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (24ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.143] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (24ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.143] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_operation_numbers_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 24,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 72 operation(s)"
  },
  "ResultData": "DataTable[72 rows]",
  "ErrorMessage": "Retrieved 72 operation(s)"
}
[20:21:44.148] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (24ms) - 72 rows
2025-11-09 20:21:44 - [20:21:44.148] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (24ms) - 72 rows
[20:21:44.150] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (27ms)
2025-11-09 20:21:44 - [20:21:44.150] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (27ms)
[20:21:44.152] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (32ms)
2025-11-09 20:21:44 - [20:21:44.152] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (32ms)
2025-11-09 20:21:44 - [20:21:44.152] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_operation_numbers_Get_All",
  "ElapsedMs": 32,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638983165041196315",
  "Status": "SUCCESS",
  "RowCount": 72
}
2025-11-09 20:21:44 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-09 20:21:44 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-09 20:21:44 - [DataTable] ComboBoxOperation: Target schema:
2025-11-09 20:21:44 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[20:21:44.161] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-09 20:21:44 - [20:21:44.161] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-09 20:21:44 - [20:21:44.161] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_locations_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638983165041610225"
}
[20:21:44.163] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.163] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.165] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-09 20:21:44 - [20:21:44.165] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[20:21:44.232] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (71ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.232] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (71ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.232] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_locations_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 71,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 10371 location(s)"
  },
  "ResultData": "DataTable[10371 rows]",
  "ErrorMessage": "Retrieved 10371 location(s)"
}
[20:21:44.237] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (71ms) - 10371 rows
2025-11-09 20:21:44 - [20:21:44.237] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (71ms) - 10371 rows
[20:21:44.239] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (75ms)
2025-11-09 20:21:44 - [20:21:44.239] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (75ms)
[20:21:44.241] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (80ms)
2025-11-09 20:21:44 - [20:21:44.241] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (80ms)
2025-11-09 20:21:44 - [20:21:44.241] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_locations_Get_All",
  "ElapsedMs": 80,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638983165041610225",
  "Status": "SUCCESS",
  "RowCount": 10371
}
2025-11-09 20:21:44 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-09 20:21:44 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-09 20:21:44 - [DataTable] ComboBoxLocation: Target schema:
2025-11-09 20:21:44 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[20:21:44.263] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-09 20:21:44 - [20:21:44.263] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-09 20:21:44 - [20:21:44.263] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638983165042634008"
}
[20:21:44.266] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.266] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.270] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-09 20:21:44 - [20:21:44.270] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[20:21:44.289] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (26ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.289] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (26ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.289] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 26,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user(s)"
}
[20:21:44.292] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (26ms) - 88 rows
2025-11-09 20:21:44 - [20:21:44.292] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (26ms) - 88 rows
[20:21:44.295] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (28ms)
2025-11-09 20:21:44 - [20:21:44.295] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (28ms)
[20:21:44.297] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (34ms)
2025-11-09 20:21:44 - [20:21:44.297] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (34ms)
2025-11-09 20:21:44 - [20:21:44.297] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_All",
  "ElapsedMs": 34,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638983165042634008",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-09 20:21:44 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-09 20:21:44 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-09 20:21:44 - [DataTable] ComboBoxUser: Target schema:
2025-11-09 20:21:44 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[20:21:44.307] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-09 20:21:44 - [20:21:44.307] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-09 20:21:44 - [20:21:44.307] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_item_types_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638983165043077928"
}
[20:21:44.311] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.311] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.312] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-09 20:21:44 - [20:21:44.312] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[20:21:44.332] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (24ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.332] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (24ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.332] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_item_types_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 24,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 4 item type(s)"
  },
  "ResultData": "DataTable[4 rows]",
  "ErrorMessage": "Retrieved 4 item type(s)"
}
[20:21:44.334] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (24ms) - 4 rows
2025-11-09 20:21:44 - [20:21:44.334] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (24ms) - 4 rows
[20:21:44.336] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
2025-11-09 20:21:44 - [20:21:44.336] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
[20:21:44.339] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (31ms)
2025-11-09 20:21:44 - [20:21:44.339] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (31ms)
2025-11-09 20:21:44 - [20:21:44.339] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_item_types_Get_All",
  "ElapsedMs": 31,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638983165043077928",
  "Status": "SUCCESS",
  "RowCount": 4
}
2025-11-09 20:21:44 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-09 20:21:44 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-09 20:21:44 - [DataTable] ComboBoxItemType: Target schema:
2025-11-09 20:21:44 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-09 20:21:44 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-09 20:21:44 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-09 20:21:44 - Running VersionChecker - checking database version information.
[20:21:44.410] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-09 20:21:44 - [20:21:44.410] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-09 20:21:44 - [20:21:44.410] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_log_changelog_Get_Current",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638983165044107882"
}
[20:21:44.414] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.414] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.416] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-09 20:21:44 - [20:21:44.416] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-09 20:21:44 - [Splash] Version checker initialized
[20:21:44.434] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (23ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.434] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (23ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.434] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "log_changelog_Get_Current",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 23,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved current changelog version"
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved current changelog version"
}
[20:21:44.437] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (23ms) - 1 rows
2025-11-09 20:21:44 - [20:21:44.437] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (23ms) - 1 rows
[20:21:44.439] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
2025-11-09 20:21:44 - [20:21:44.439] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (25ms)
[20:21:44.441] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (30ms)
2025-11-09 20:21:44 - [20:21:44.441] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (30ms)
2025-11-09 20:21:44 - [20:21:44.441] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_log_changelog_Get_Current",
  "ElapsedMs": 30,
  "Key": "ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638983165044107882",
  "Status": "SUCCESS",
  "RowCount": 1
}
Database version retrieved: 6.0.0.0
2025-11-09 20:21:44 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.1.0, DB: 6.0.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-09 20:21:44 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[20:21:44.485] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-09 20:21:44 - [20:21:44.485] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-09 20:21:44 - [20:21:44.485] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638983165044854464"
}
[20:21:44.488] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.488] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.490] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-09 20:21:44 - [20:21:44.490] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[20:21:44.497] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (11ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.497] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (11ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.497] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 11,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 9 theme(s)"
  },
  "ResultData": "DataTable[9 rows]",
  "ErrorMessage": "Retrieved 9 theme(s)"
}
[20:21:44.499] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (11ms) - 9 rows
2025-11-09 20:21:44 - [20:21:44.499] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (11ms) - 9 rows
[20:21:44.502] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-09 20:21:44 - [20:21:44.502] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[20:21:44.503] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-09 20:21:44 - [20:21:44.503] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (18ms)
2025-11-09 20:21:44 - [20:21:44.503] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_theme_GetAll",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638983165044854464",
  "Status": "SUCCESS",
  "RowCount": 9
}
2025-11-09 20:21:44 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-09 20:21:44 - Successfully loaded 9 themes from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Default' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Forest' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-09 20:21:44 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-09 20:21:44 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-09 20:21:44 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[20:21:44.548] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-09 20:21:44 - [20:21:44.548] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[20:21:44.550] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-09 20:21:44 - [20:21:44.550] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:21:44.552] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.552] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.552] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165045528124"
}
[20:21:44.555] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.555] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.557] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.557] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:21:44.576] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (23ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.576] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (23ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.576] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 23,
  "Thread": 1,
  "InputParameters": {
    "p_UserId": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved settings for user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved settings for user \"JOHNK\""
}
[20:21:44.579] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (23ms) - 1 rows
2025-11-09 20:21:44 - [20:21:44.579] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (23ms) - 1 rows
[20:21:44.582] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (26ms)
2025-11-09 20:21:44 - [20:21:44.582] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (26ms)
[20:21:44.583] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (31ms)
2025-11-09 20:21:44 - [20:21:44.583] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (31ms)
2025-11-09 20:21:44 - [20:21:44.583] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 31,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165045528124",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:44.593] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (42ms)
2025-11-09 20:21:44 - [20:21:44.593] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (42ms)
[20:21:44.596] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (47ms)
2025-11-09 20:21:44 - [20:21:44.596] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (47ms)
2025-11-09 20:21:44 - Loaded theme preference for user JOHNK: Forest
2025-11-09 20:21:44 - Set Model_Application_Variables.ThemeName to: Forest
2025-11-09 20:21:44 - Theme system initialized for user JOHNK. Final theme: Forest, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-09 20:21:44 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-09 20:21:44 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-09 20:21:44 - [Splash] Loading theme settings
[20:21:44.738] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
2025-11-09 20:21:44 - [20:21:44.738] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeEnabledAsync
[20:21:44.740] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-09 20:21:44 - [20:21:44.740] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:21:44.742] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.742] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.742] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165047421655"
}
[20:21:44.745] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.745] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.747] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.747] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:21:44.752] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.752] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.752] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 10,
  "Thread": 1,
  "InputParameters": {
    "p_UserId": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved settings for user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved settings for user \"JOHNK\""
}
[20:21:44.755] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-09 20:21:44 - [20:21:44.755] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[20:21:44.757] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-09 20:21:44 - [20:21:44.757] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[20:21:44.759] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-09 20:21:44 - [20:21:44.759] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (17ms)
2025-11-09 20:21:44 - [20:21:44.759] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 17,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165047421655",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:44.762] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-09 20:21:44 - [20:21:44.762] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[20:21:44.765] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (26ms)
2025-11-09 20:21:44 - [20:21:44.765] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeEnabledAsync (26ms)
[20:21:44.767] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-09 20:21:44 - [20:21:44.767] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[20:21:44.769] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-09 20:21:44 - [20:21:44.769] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:21:44.771] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.771] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.771] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165047716379"
}
[20:21:44.774] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.774] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.776] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.776] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:21:44.781] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.781] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.781] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 9,
  "Thread": 1,
  "InputParameters": {
    "p_UserId": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved settings for user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved settings for user \"JOHNK\""
}
[20:21:44.783] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-09 20:21:44 - [20:21:44.783] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[20:21:44.786] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-09 20:21:44 - [20:21:44.786] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[20:21:44.788] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-09 20:21:44 - [20:21:44.788] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-09 20:21:44 - [20:21:44.788] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 16,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165047716379",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:44.792] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
2025-11-09 20:21:44 - [20:21:44.792] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (22ms)
[20:21:44.794] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (26ms)
2025-11-09 20:21:44 - [20:21:44.794] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (26ms)
[20:21:44.797] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-09 20:21:44 - [20:21:44.797] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[20:21:44.799] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-09 20:21:44 - [20:21:44.799] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[20:21:44.800] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.800] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.800] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165048007765"
}
[20:21:44.803] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:44 - [20:21:44.803] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:44.806] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-09 20:21:44 - [20:21:44.806] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[20:21:44.810] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.810] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-09 20:21:44 - [20:21:44.810] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 9,
  "Thread": 1,
  "InputParameters": {
    "p_UserId": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved settings for user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved settings for user \"JOHNK\""
}
[20:21:44.813] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-09 20:21:44 - [20:21:44.813] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[20:21:44.815] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-09 20:21:44 - [20:21:44.815] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[20:21:44.817] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-09 20:21:44 - [20:21:44.817] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-09 20:21:44 - [20:21:44.817] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 16,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638983165048007765",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:44.820] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
2025-11-09 20:21:44 - [20:21:44.820] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (21ms)
[20:21:44.822] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-09 20:21:44 - [20:21:44.822] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (25ms)
2025-11-09 20:21:44 - [Splash] Theme settings loaded - Theme Enabled: True, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-09 20:21:44 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-09 20:21:44 - [Splash] Core startup sequence completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
[20:21:45.203] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-09 20:21:45 - [20:21:45.203] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[20:21:45.206] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-09 20:21:45 - [20:21:45.206] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_ConnectionStrengthControl' and all its controls.
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_ConnectionStrengthControl'.
[20:21:45.233] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.233] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[20:21:45.235] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.235] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[20:21:45.245] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.245] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_InventoryTab' and all its controls.
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_InventoryTab'.
[20:21:45.259] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.259] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[20:21:45.261] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.261] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[20:21:45.263] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.263] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[20:21:45.266] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.266] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[20:21:45.278] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.278] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-09 20:21:45 - Inventory tab events wired up.
[20:21:45.281] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.281] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[20:21:45.284] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.284] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-09 20:21:45 - Inventory Quantity TextBox changed.
[20:21:45.288] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.288] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[20:21:45.290] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.290] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[20:21:45.292] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (58ms)
2025-11-09 20:21:45 - [20:21:45.292] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (58ms)
[20:21:45.295] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-09 20:21:45 - [20:21:45.295] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[20:21:45.296] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-09 20:21:45 - [20:21:45.296] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-09 20:21:45 - Control_AdvancedInventory constructor entered.
[20:21:45.308] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-09 20:21:45 - [20:21:45.308] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_AdvancedInventory' and all its controls.
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel4'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel1'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel2'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel3'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_AdvancedInventory'.
[20:21:45.362] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-09 20:21:45 - [20:21:45.362] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-09 20:21:45 - Control_AdvancedInventory constructor exited.
[20:21:45.367] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.367] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[20:21:45.368] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.368] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[20:21:45.378] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.378] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_RemoveTab' and all its controls.
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_RemoveTab'.
[20:21:45.400] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.400] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[20:21:45.402] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.402] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[20:21:45.404] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.404] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[20:21:45.413] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.413] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[20:21:45.415] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.415] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[20:21:45.417] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.417] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[20:21:45.419] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-09 20:21:45 - [20:21:45.419] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[20:21:45.421] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (54ms)
2025-11-09 20:21:45 - [20:21:45.421] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (54ms)
[20:21:45.424] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-09 20:21:45 - [20:21:45.424] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[20:21:45.425] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-09 20:21:45 - [20:21:45.425] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[20:21:45.435] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-09 20:21:45 - [20:21:45.435] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_AdvancedRemove' and all its controls.
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_AdvancedRemove'.
[20:21:45.467] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-09 20:21:45 - [20:21:45.467] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[20:21:45.470] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-09 20:21:45 - [20:21:45.470] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[20:21:45.475] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-09 20:21:45 - [20:21:45.475] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[20:21:45.476] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-09 20:21:45 - [20:21:45.476] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_TransferTab' and all its controls.
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel1'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_TransferTab'.
2025-11-09 20:21:45 - Transfer tab events wired up.
[20:21:45.505] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-09 20:21:45 - [20:21:45.505] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[20:21:45.507] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-09 20:21:45 - [20:21:45.507] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[20:21:45.509] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-09 20:21:45 - [20:21:45.509] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[20:21:45.532] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-09 20:21:45 - [20:21:45.532] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[20:21:45.564] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-09 20:21:45 - [20:21:45.564] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-09 20:21:45 - DPI scaling applied to user control 'Control_QuickButtons' and all its controls.
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-09 20:21:45 - Runtime layout adjustments applied to user control 'Control_QuickButtons'.
2025-11-09 20:21:45 - Inventory Part ComboBox selection changed.
2025-11-09 20:21:45 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[20:21:45.595] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-09 20:21:45 - [20:21:45.595] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-09 20:21:45 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel4'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel1'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel2'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel3'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'panel1'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-09 20:21:45 - Runtime layout adjustments applied to form 'MainForm'.
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[20:21:45.685] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-09 20:21:45 - [20:21:45.685] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[20:21:45.687] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-09 20:21:45 - [20:21:45.687] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-09 20:21:45 - Inventory Op ComboBox selection changed.
2025-11-09 20:21:45 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[20:21:45.694] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-09 20:21:45 - [20:21:45.694] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[20:21:45.720] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (32ms)
2025-11-09 20:21:45 - [20:21:45.720] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (32ms)
[20:21:45.752] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-09 20:21:45 - [20:21:45.752] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[20:21:45.754] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-09 20:21:45 - [20:21:45.754] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[20:21:45.756] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (71ms)
2025-11-09 20:21:45 - [20:21:45.756] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (71ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[20:21:45.761] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-09 20:21:45 - [20:21:45.761] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[20:21:45.764] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-09 20:21:45 - [20:21:45.764] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[20:21:45.797] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-09 20:21:45 - [20:21:45.797] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[20:21:45.799] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-09 20:21:45 - [20:21:45.799] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[20:21:45.803] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-09 20:21:45 - [20:21:45.803] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[20:21:45.807] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-09 20:21:45 - [20:21:45.807] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-09 20:21:45 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[20:21:45.812] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-09 20:21:45 - [20:21:45.812] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[20:21:45.814] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
2025-11-09 20:21:45 - [20:21:45.814] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (16ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[20:21:45.817] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-09 20:21:45 - [20:21:45.817] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[20:21:45.820] [MEDIUM] ⬅️ EXITING MainForm.MainForm (617ms)
2025-11-09 20:21:45 - [20:21:45.820] [MEDIUM] ⬅️ EXITING MainForm.MainForm (617ms)
2025-11-09 20:21:45 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-09 20:21:45 - Remove tab ComboBoxes loaded.
2025-11-09 20:21:45 - Removal tab events wired up.
2025-11-09 20:21:45 - Initial setup of ComboBoxes in the Remove Tab.
2025-11-09 20:21:45 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[20:21:45.831] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-09 20:21:45 - [20:21:45.831] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:21:45.860] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-09 20:21:45 - [20:21:45.860] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-09 20:21:45 - [20:21:45.860] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638983165058606228"
}
[20:21:45.864] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:45 - [20:21:45.864] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:45.866] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-09 20:21:45 - [20:21:45.866] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-09 20:21:45 - Transfer tab ComboBoxes loaded.
[20:21:45.870] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-09 20:21:45 - [20:21:45.870] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:21:45.873] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-09 20:21:45 - [20:21:45.873] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-09 20:21:45 - [20:21:45.873] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638983165058730858"
}
[20:21:45.876] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:45 - [20:21:45.876] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:45.879] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-09 20:21:45 - [20:21:45.879] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[20:21:45.884] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.884] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-09 20:21:45 - Inventory Op ComboBox selection changed.
[20:21:45.889] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-09 20:21:45 - [20:21:45.889] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[20:21:45.892] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (31ms) - Status: 1
2025-11-09 20:21:45 - [20:21:45.892] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (31ms) - Status: 1
2025-11-09 20:21:45 - [20:21:45.892] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 31,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved user \"JOHNK\""
}
[20:21:45.897] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (31ms) - 1 rows
2025-11-09 20:21:45 - [20:21:45.897] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (31ms) - 1 rows
[20:21:45.900] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (35ms)
2025-11-09 20:21:45 - [20:21:45.900] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (35ms)
[20:21:45.902] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (41ms)
2025-11-09 20:21:45 - [20:21:45.902] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (41ms)
2025-11-09 20:21:45 - [20:21:45.902] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 41,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638983165058606228",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:45.906] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (74ms)
2025-11-09 20:21:45 - [20:21:45.906] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (74ms)
2025-11-09 20:21:45 - User full name loaded: John Koll
[20:21:45.913] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (40ms) - Status: 1
2025-11-09 20:21:45 - [20:21:45.913] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (40ms) - Status: 1
2025-11-09 20:21:45 - [20:21:45.913] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 40,
  "Thread": 21,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved user \"JOHNK\""
}
[20:21:45.916] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (40ms) - 1 rows
2025-11-09 20:21:45 - [20:21:45.916] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (40ms) - 1 rows
[20:21:45.939] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (62ms)
2025-11-09 20:21:45 - [20:21:45.939] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (62ms)
[20:21:45.941] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (68ms)
2025-11-09 20:21:45 - [20:21:45.941] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (68ms)
2025-11-09 20:21:45 - [20:21:45.941] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 68,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638983165058730858",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:45.945] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (74ms)
2025-11-09 20:21:45 - [20:21:45.945] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (74ms)
2025-11-09 20:21:45 - User full name loaded: John Koll
[20:21:45.968] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-09 20:21:45 - [20:21:45.968] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-09 20:21:45 - Inventory Location ComboBox selection changed.
[20:21:45.971] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-09 20:21:45 - [20:21:45.971] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-09 20:21:45 - [Splash] All form instances configured successfully
2025-11-09 20:21:45 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
2025-11-09 20:21:45 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-09 20:21:45 - Applied Panel layout adjustments to ''
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-09 20:21:45 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-09 20:21:45 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-09 20:21:45 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'panel4'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-09 20:21:46 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'panel1'
2025-11-09 20:21:46 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'panel2'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'panel3'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-09 20:21:46 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-09 20:21:46 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-09 20:21:46 - Applied Panel layout adjustments to ''
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-09 20:21:46 - Applied Panel layout adjustments to ''
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-09 20:21:46 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-09 20:21:46 - Applied Panel layout adjustments to ''
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-09 20:21:46 - Applied Panel layout adjustments to ''
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'panel1'
2025-11-09 20:21:46 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-09 20:21:46 - Applied Panel layout adjustments to ''
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-09 20:21:46 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-09 20:21:46 - Runtime layout adjustments applied to form 'MainForm'.
2025-11-09 20:21:46 - Global theme 'Forest' with DPI scaling applied to form 'MainForm'.
2025-11-09 20:21:46 - [Splash] Theme applied to MainForm
2025-11-09 20:21:46 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
2025-11-09 20:21:46 - Inventory tab ComboBoxes loaded.
[20:21:46.110] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (844ms)
2025-11-09 20:21:46 - [20:21:46.110] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (844ms)
[20:21:47.165] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-09 20:21:47 - [20:21:47.165] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-09 20:21:47 - [Splash] MainForm displayed successfully
2025-11-09 20:21:47 - [Splash] MainForm displayed - startup complete
2025-11-09 20:21:47 - [Splash] Splash screen closed
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[20:21:47.203] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-09 20:21:47 - [20:21:47.203] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[20:21:47.205] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.205] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.205] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638983165072055394"
}
[20:21:47.209] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:47 - [20:21:47.209] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:47.211] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.211] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[20:21:47.217] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (12ms) - Status: 1
2025-11-09 20:21:47 - [20:21:47.217] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (12ms) - Status: 1
2025-11-09 20:21:47 - [20:21:47.217] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 12,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved user \"JOHNK\""
}
[20:21:47.222] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (12ms) - 1 rows
2025-11-09 20:21:47 - [20:21:47.222] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (12ms) - 1 rows
[20:21:47.224] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
2025-11-09 20:21:47 - [20:21:47.224] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
[20:21:47.226] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (21ms)
2025-11-09 20:21:47 - [20:21:47.226] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (21ms)
2025-11-09 20:21:47 - [20:21:47.226] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 21,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638983165072055394",
  "Status": "SUCCESS",
  "RowCount": 1
}
[20:21:47.230] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (27ms)
2025-11-09 20:21:47 - [20:21:47.230] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (27ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[20:21:47.234] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-09 20:21:47 - [20:21:47.234] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[20:21:47.238] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-09 20:21:47 - [20:21:47.238] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[20:21:47.240] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-09 20:21:47 - [20:21:47.240] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-09 20:21:47 - Application Info - Development Menu configured for user 'JOHNK': Visible
[20:21:47.244] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
2025-11-09 20:21:47 - [20:21:47.244] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
[20:21:47.277] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-09 20:21:47 - [20:21:47.277] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-09 20:21:47 -
2025-11-09 20:21:47 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-09 20:21:47 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-09 20:21:47 - [QuickButtons]   User: JOHNK
2025-11-09 20:21:47 - [QuickButtons] ════════════════════════════════════════════════════════════
[20:21:47.286] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-09 20:21:47 - [20:21:47.286] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-09 20:21:47 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-09 20:21:47 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[20:21:47.292] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.292] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.292] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638983165072922545"
}
[20:21:47.295] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:47 - [20:21:47.295] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:47.297] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.297] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[20:21:47.315] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (23ms) - Status: 1
2025-11-09 20:21:47 - [20:21:47.315] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (23ms) - Status: 1
2025-11-09 20:21:47 - [20:21:47.315] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 23,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 6 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[6 rows]",
  "ErrorMessage": "Retrieved 6 transaction(s) for user: JOHNK"
}
[20:21:47.318] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (23ms) - 6 rows
2025-11-09 20:21:47 - [20:21:47.318] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (23ms) - 6 rows
[20:21:47.321] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (24ms)
2025-11-09 20:21:47 - [20:21:47.321] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (24ms)
[20:21:47.323] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (30ms)
2025-11-09 20:21:47 - [20:21:47.323] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (30ms)
2025-11-09 20:21:47 - [20:21:47.323] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 30,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638983165072922545",
  "Status": "SUCCESS",
  "RowCount": 6
}
2025-11-09 20:21:47 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-09 20:21:47 - [Dao_QuickButtons] Added to array: 01-31976-000 + 10 (Qty: 1)
2025-11-09 20:21:47 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-09 20:21:47 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-09 20:21:47 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-09 20:21:47 - [Dao_QuickButtons] Added to array: 06-96408-001 + N/A (Qty: 40)
2025-11-09 20:21:47 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 10)
2025-11-09 20:21:47 - [Dao_QuickButtons] Array restructured: 6 unique buttons, 0 duplicates removed
2025-11-09 20:21:47 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-09 20:21:47 - [Dao_QuickButtons] All buttons deleted from database
2025-11-09 20:21:47 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-09 20:21:47 - [Dao_QuickButtons] Created button at position 1: 01-31976-000 + 10 (Qty: 1)
2025-11-09 20:21:47 - [Dao_QuickButtons] Created button at position 2: 04-27693-000 + 90 (Qty: 10)
2025-11-09 20:21:47 - [Dao_QuickButtons] Created button at position 3: 01-34578-000 + 880 (Qty: 20)
2025-11-09 20:21:47 - [Dao_QuickButtons] Created button at position 4: 03-29236-030 + 959 (Qty: 30)
2025-11-09 20:21:47 - [Dao_QuickButtons] Created button at position 5: 06-96408-001 + N/A (Qty: 40)
2025-11-09 20:21:47 - [Dao_QuickButtons] Created button at position 6: 01-33016-000 + 109 (Qty: 10)
2025-11-09 20:21:47 - [Dao_QuickButtons] Created 6 buttons in database
2025-11-09 20:21:47 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 6 buttons remain
2025-11-09 20:21:47 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-09 20:21:47 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 6 buttons remain
2025-11-09 20:21:47 - [QuickButtons] STEP 2: Loading data from database
[20:21:47.407] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.407] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.407] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638983165074075181"
}
[20:21:47.411] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:47 - [20:21:47.411] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:47.413] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-09 20:21:47 - [20:21:47.413] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[20:21:47.418] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-09 20:21:47 - [20:21:47.418] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-09 20:21:47 - [20:21:47.418] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 10,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 6 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[6 rows]",
  "ErrorMessage": "Retrieved 6 transaction(s) for user: JOHNK"
}
[20:21:47.421] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 6 rows
2025-11-09 20:21:47 - [20:21:47.421] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 6 rows
[20:21:47.423] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-09 20:21:47 - [20:21:47.423] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[20:21:47.425] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-09 20:21:47 - [20:21:47.425] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-09 20:21:47 - [20:21:47.425] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638983165074075181",
  "Status": "SUCCESS",
  "RowCount": 6
}
[20:21:47.431] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-09 20:21:47 - [20:21:47.431] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-09 20:21:47 - [QuickButtons] STEP 2: ✓ Retrieved 6 button(s) from database
2025-11-09 20:21:47 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-09 20:21:47 - [QuickButtons] STEP 3:   Button 1: 01-31976-000 + Op:10 (Qty: 1)
2025-11-09 20:21:47 - [QuickButtons] STEP 3:   Button 2: 04-27693-000 + Op:90 (Qty: 10)
2025-11-09 20:21:47 - [QuickButtons] STEP 3:   Button 3: 01-34578-000 + Op:880 (Qty: 20)
2025-11-09 20:21:47 - [QuickButtons] STEP 3:   Button 4: 03-29236-030 + Op:959 (Qty: 30)
2025-11-09 20:21:47 - [QuickButtons] STEP 3:   Button 5: 06-96408-001 + Op:N/A (Qty: 40)
2025-11-09 20:21:47 - [QuickButtons] STEP 3:   Button 6: 01-33016-000 + Op:109 (Qty: 10)
2025-11-09 20:21:47 - [QuickButtons] STEP 3: Filled 6 button(s) with data
2025-11-09 20:21:47 - [QuickButtons] STEP 3: Clearing remaining 4 button(s)
2025-11-09 20:21:47 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-09 20:21:47 - [QuickButtons] STEP 4: Layout refreshed - 6 visible button(s)
2025-11-09 20:21:47 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-09 20:21:47 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-09 20:21:47 - [QuickButtons] ║ User: JOHNK
2025-11-09 20:21:47 - [QuickButtons] ║ Visible Buttons: 6
2025-11-09 20:21:47 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-09 20:21:47 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-09 20:21:47 -
[20:21:47.485] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (198ms)
2025-11-09 20:21:47 - [20:21:47.485] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (198ms)
[20:21:47.487] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-09 20:21:47 - [20:21:47.487] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
Resetting user controls...
Attempting to invoke Control_InventoryTab_SoftReset on Control_InventoryTab
Attempting to invoke Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Invoking Control_InventoryTab_SoftReset on Control_InventoryTab
Invoking Control_AdvancedRemove_SoftReset on Control_AdvancedRemove
Attempting to invoke Control_TransferTab_SoftReset on Control_TransferTab
Attempting to invoke Control_AdvancedInventory_SoftReset on Control_AdvancedInventory
Invoking Control_TransferTab_SoftReset on Control_TransferTab
Method Control_AdvancedInventory_SoftReset not found on Control_AdvancedInventory
Attempting to invoke Control_RemoveTab_SoftReset on Control_RemoveTab
Invoking Control_RemoveTab_SoftReset on Control_RemoveTab
[20:21:48.922] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-09 20:21:48 - [20:21:48.922] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] AdvancedRemove SoftReset button re-enabled
[DEBUG] Restoring status strip after soft reset
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[DEBUG] TransferTab SoftReset button re-enabled
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[20:21:48.948] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (26ms)
2025-11-09 20:21:48 - [20:21:48.948] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (26ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
[20:21:49.404] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_inventory_Get_All
2025-11-09 20:21:49 - [20:21:49.404] [HIGH  ] ⏱️ PERFORMANCE START: SP_inv_inventory_Get_All
2025-11-09 20:21:49 - [20:21:49.404] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_inv_inventory_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_inv_inventory_Get_All:638983165094043396"
}
[20:21:49.407] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-09 20:21:49 - [20:21:49.407] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[20:21:49.409] [MEDIUM] 🗄️ DB PROCEDURE START: inv_inventory_Get_All
2025-11-09 20:21:49 - [20:21:49.409] [MEDIUM] 🗄️ DB PROCEDURE START: inv_inventory_Get_All
[20:21:49.431] [HIGH  ] ✅ PROCEDURE inv_inventory_Get_All (26ms) - Status: 1
2025-11-09 20:21:49 - [20:21:49.431] [HIGH  ] ✅ PROCEDURE inv_inventory_Get_All (26ms) - Status: 1
2025-11-09 20:21:49 - [20:21:49.431] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "inv_inventory_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 26,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 832 inventory record(s)"
  },
  "ResultData": "DataTable[832 rows]",
  "ErrorMessage": "Retrieved 832 inventory record(s)"
}
[20:21:49.434] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_inventory_Get_All (26ms) - 832 rows
2025-11-09 20:21:49 - [20:21:49.434] [MEDIUM] ✅ DB PROCEDURE COMPLETE: inv_inventory_Get_All (26ms) - 832 rows
[20:21:49.436] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (28ms)
2025-11-09 20:21:49 - [20:21:49.436] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (28ms)
[20:21:49.439] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_inventory_Get_All (34ms)
2025-11-09 20:21:49 - [20:21:49.439] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_inv_inventory_Get_All (34ms)
2025-11-09 20:21:49 - [20:21:49.439] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_inv_inventory_Get_All",
  "ElapsedMs": 34,
  "Key": "ExecuteDataTableWithStatusAsync:SP_inv_inventory_Get_All:638983165094043396",
  "Status": "SUCCESS",
  "RowCount": 832
}
2025-11-09 20:21:49 - [SHOW ALL DEBUG] Retrieved 832 inventory records. Success: True
2025-11-09 20:21:52 - [RemoveTab] Print requested.
2025-11-09 20:21:52 - [PrintForm] Constructor: Incoming printJob.CurrentPage=1, TotalPages=1
2025-11-09 20:21:52 - DPI scaling applied to form 'PrintForm' and all its controls.
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_Main'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_Master'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_Sidebar'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_Sidebar'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_PageSettingsSection'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayoutPanel_PageSettingsHeader'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_PageSettingsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_PageSettingsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_CustomPageRange'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_ActionButtons'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_OptionsSection'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayoutPanel_OptionsHeader'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_OptionsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_OptionsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayoutPanel_ColorMode'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_ColumnSettingsSection'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayoutPanel_ColumnSettingsHeader'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_ColumnSettingsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_ColumnSettingsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayoutPanel_ColumnButtons'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_PrinterSettingsSection'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_PrinterSettingsHeader'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayoutPanel_PrinterSettingsHeader'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_PrinterSettingsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_PrinterSettingsContent'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_SidebarSpacer'
2025-11-09 20:21:52 - Applied Panel layout adjustments to 'PrintForm_Panel_PreviewViewport'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_PreviewArea'
2025-11-09 20:21:52 - Applied TableLayoutPanel layout adjustments to 'PrintForm_TableLayout_PreviewNavigation'
2025-11-09 20:21:52 - Runtime layout adjustments applied to form 'PrintForm'.
2025-11-09 20:21:52 - [PrintForm] Constructor: After InitializeComponent, Control.StartPage=0
2025-11-09 20:21:52 - [PrintForm] InitializePreviewSection: Before reset StartPage=0
2025-11-09 20:21:52 - [PrintForm] InitializePreviewSection: After reset StartPage=0
2025-11-09 20:21:52 - [PrintForm] Constructor complete: CurrentPage=1, Control.StartPage=0
2025-11-09 20:21:52 - [PrintForm] GeneratePreviewAsync ENTRY: CurrentPage=1, TotalPages=1, PageRangeType=AllPages
2025-11-09 20:21:52 - [PrintForm] After reset: CurrentPage=1
2025-11-09 20:21:52 - [PrintForm] Captured original values: Range=AllPages, From=1, To=1, Current=1
2025-11-09 20:21:52 - [Helper_PrintManager] Print manager initialized
2025-11-09 20:21:52 - [Helper_PrintManager] Preparing print document...
2025-11-09 20:21:52 - [Core_TablePrinter] Data prepared: RangeType=AllPages, FirstPage=1, StartRow=0, EndRowExclusive=832, VisibleColumns=5, TotalRows=832, ExistingTotalPages=1
2025-11-09 20:21:52 - [Helper_PrintManager] Print document prepared: 832 rows, Printer: Microsoft Print to PDF
2025-11-09 20:21:52 - [Core_TablePrinter] PrintPage first page rendered: Page=1, StartRow=0, EndRow=32, HasMore=True
2025-11-09 20:21:54 - [Core_TablePrinter] Printing complete: 26 page(s), 832 rows printed
2025-11-09 20:21:54 - [PrintForm] Preview setup: TotalPages=26, TargetIndex=0, OriginalCurrentPage=1
2025-11-09 20:21:54 - [PrintForm] Before setting Document: Control.StartPage=0
2025-11-09 20:21:54 - [PrintForm] After setting Document, before StartPage: Control.StartPage=0
2025-11-09 20:21:54 - [PrintForm] After setting StartPage=0: Control.StartPage=0
2025-11-09 20:21:54 - [PrintForm] Final CurrentPage set to: 1, StartPage=0
2025-11-09 20:21:54 - [PrintForm] UpdatePreviewNavigationState: StartPage=0, CurrentIndex=0, DisplayPage=1, TotalPages=26
2025-11-09 20:21:54 - [PrintForm] UpdatePreviewNavigationState: Set CurrentPage to 1
2025-11-09 20:21:56 - [Helper_PrintManager] Print manager disposed
2025-11-09 20:21:56 - [RemoveTab] Print dialog closed with result: Cancel.
2025-11-09 20:21:58 - [Cleanup] Starting application cleanup
2025-11-09 20:21:58 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-09 20:21:58 - [Cleanup] Memory cleanup completed
2025-11-09 20:21:58 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-09 20:21:58 - [Startup] Application shutdown completed
2025-11-09 20:21:58 - [Cleanup] Starting application cleanup
2025-11-09 20:21:58 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-09 20:21:58 - [Cleanup] Memory cleanup completed
2025-11-09 20:21:58 - [Cleanup] Application cleanup completed successfully
2025-11-09 20:21:58 - [Cleanup] Starting application cleanup
2025-11-09 20:21:58 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-09 20:21:58 - [Cleanup] Memory cleanup completed
2025-11-09 20:21:58 - [Cleanup] Application cleanup completed successfully
The program '[19676] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
