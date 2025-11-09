------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[16:15:46.815] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-08 16:15:47 - [16:15:46.815] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[16:15:47.172] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-08 16:15:47 - [16:15:47.172] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[16:15:47.175] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-08 16:15:47 - [16:15:47.175] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[16:15:47.177] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-08 16:15:47 - [16:15:47.177] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-08 16:15:47 - [Startup] Application initialization started
2025-11-08 16:15:47 - [Startup] User identified: JOHNK
2025-11-08 16:15:47 - [Dao_System] Checking database connectivity
[16:15:47.211] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 16:15:47 - [16:15:47.211] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 16:15:47 - [16:15:47.211] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982153472111362"
}
[16:15:47.277] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:47 - [16:15:47.277] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:47.280] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-08 16:15:47 - [16:15:47.280] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[16:15:47.475] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (263ms) - Status: 1
2025-11-08 16:15:47 - [16:15:47.475] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (263ms) - Status: 1
2025-11-08 16:15:47 - [16:15:47.475] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 263,
  "Thread": 15,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 9 theme(s)"
  },
  "ResultData": "DataTable[9 rows]",
  "ErrorMessage": "Retrieved 9 theme(s)"
}
[16:15:47.490] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (263ms) - 9 rows
2025-11-08 16:15:47 - [16:15:47.490] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (263ms) - 9 rows
[16:15:47.493] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (215ms)
2025-11-08 16:15:47 - [16:15:47.493] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (215ms)
[16:15:47.496] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (284ms)
2025-11-08 16:15:47 - [16:15:47.496] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (284ms)
2025-11-08 16:15:47 - [16:15:47.496] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_theme_GetAll",
  "ElapsedMs": 284,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982153472111362",
  "Status": "SUCCESS",
  "RowCount": 9
}
2025-11-08 16:15:47 - [Dao_System] Database connectivity check passed
2025-11-08 16:15:47 - [Startup] Database connectivity validated successfully
2025-11-08 16:15:47 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-08 16:15:47 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-08 16:15:47 - [Startup] Parameter cache populated: 116 procedures, 519 total parameters
2025-11-08 16:15:47 - [Startup] Parameter prefix cache initialized successfully in 13ms. Cached 116 stored procedures.
[Startup] Parameter cache: 116 procedures cached in 13ms
[16:15:47.522] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-08 16:15:47 - [16:15:47.522] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-08 16:15:47 - [16:15:47.522] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_GetUserAccessType",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638982153475225941"
}
[16:15:47.525] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:47 - [16:15:47.525] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:47.526] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-08 16:15:47 - [16:15:47.526] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-08 16:15:47 - [Splash] Initializing splash screen
2025-11-08 16:15:47 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
[16:15:47.554] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-08 16:15:47 - [16:15:47.554] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[16:15:47.557] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-08 16:15:47 - [16:15:47.557] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[16:15:47.559] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (36ms) - Status: 1
2025-11-08 16:15:47 - [16:15:47.559] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (36ms) - Status: 1
2025-11-08 16:15:47 - [16:15:47.559] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_GetUserAccessType",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 36,
  "Thread": 15,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user access type(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user access type(s)"
}
[16:15:47.561] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (36ms) - 88 rows
2025-11-08 16:15:47 - [16:15:47.561] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (36ms) - 88 rows
[16:15:47.563] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-08 16:15:47 - [16:15:47.563] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[16:15:47.565] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (42ms)
2025-11-08 16:15:47 - [16:15:47.565] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (42ms)
2025-11-08 16:15:47 - [16:15:47.565] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_GetUserAccessType",
  "ElapsedMs": 42,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638982153475225941",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-08 16:15:47 - System_UserAccessType executed successfully for user: JOHNK
2025-11-08 16:15:47 - DPI scaling applied to user control 'Control_ProgressBarUserControl' and all its controls.
2025-11-08 16:15:47 - Runtime layout adjustments applied to user control 'Control_ProgressBarUserControl'.
[16:15:47.592] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-08 16:15:47 - [16:15:47.592] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-08 16:15:47 - DPI scaling applied to form 'SplashScreenForm' and all its controls.
2025-11-08 16:15:47 - Runtime layout adjustments applied to form 'SplashScreenForm'.
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[16:15:47.626] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-08 16:15:47 - [16:15:47.626] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[16:15:47.627] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-08 16:15:47 - [16:15:47.627] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[DEBUG] [SplashScreenForm.ApplyTheme] Applying theme...
2025-11-08 16:15:47 - DPI scaling applied to form 'SplashScreenForm' and all its controls.
2025-11-08 16:15:47 - Runtime layout adjustments applied to form 'SplashScreenForm'.
2025-11-08 16:15:47 - Global theme 'Default' with DPI scaling applied to form 'SplashScreenForm'.
[DEBUG] [SplashScreenForm.ApplyTheme] Theme applied.
[16:15:47.641] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-08 16:15:47 - [16:15:47.641] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[16:15:47.643] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (89ms)
2025-11-08 16:15:47 - [16:15:47.643] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (89ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-08 16:15:47 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-08-2025 @ 4-15 PM_normal.log
2025-11-08 16:15:47 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-08 16:15:47 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-08 16:15:47 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-08 16:15:47 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-08 16:15:48 - [Splash] Starting async database connectivity verification
2025-11-08 16:15:48 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-08 16:15:48 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[16:15:48.091] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-08 16:15:48 - [16:15:48.091] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-08 16:15:48 - [16:15:48.091] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_part_ids_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638982153480910093"
}
[16:15:48.094] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.094] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.096] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-08 16:15:48 - [16:15:48.096] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[16:15:48.154] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (62ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.154] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (62ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.154] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_part_ids_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 62,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3745 part(s)"
  },
  "ResultData": "DataTable[3745 rows]",
  "ErrorMessage": "Retrieved 3745 part(s)"
}
[16:15:48.157] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (62ms) - 3745 rows
2025-11-08 16:15:48 - [16:15:48.157] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (62ms) - 3745 rows
[16:15:48.159] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (65ms)
2025-11-08 16:15:48 - [16:15:48.159] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (65ms)
[16:15:48.161] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (70ms)
2025-11-08 16:15:48 - [16:15:48.161] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (70ms)
2025-11-08 16:15:48 - [16:15:48.161] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_part_ids_Get_All",
  "ElapsedMs": 70,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638982153480910093",
  "Status": "SUCCESS",
  "RowCount": 3745
}
2025-11-08 16:15:48 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-08 16:15:48 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), ItemType(String), Operations(String)
2025-11-08 16:15:48 - [DataTable] ComboBoxPart: Target schema:
2025-11-08 16:15:48 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[16:15:48.188] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-08 16:15:48 - [16:15:48.188] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-08 16:15:48 - [16:15:48.188] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_operation_numbers_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638982153481884651"
}
[16:15:48.192] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.192] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.194] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-08 16:15:48 - [16:15:48.194] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[16:15:48.228] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (40ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.228] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (40ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.228] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_operation_numbers_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 40,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 72 operation(s)"
  },
  "ResultData": "DataTable[72 rows]",
  "ErrorMessage": "Retrieved 72 operation(s)"
}
[16:15:48.233] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (40ms) - 72 rows
2025-11-08 16:15:48 - [16:15:48.233] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (40ms) - 72 rows
[16:15:48.235] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (43ms)
2025-11-08 16:15:48 - [16:15:48.235] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (43ms)
[16:15:48.237] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (48ms)
2025-11-08 16:15:48 - [16:15:48.237] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (48ms)
2025-11-08 16:15:48 - [16:15:48.237] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_operation_numbers_Get_All",
  "ElapsedMs": 48,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638982153481884651",
  "Status": "SUCCESS",
  "RowCount": 72
}
2025-11-08 16:15:48 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-08 16:15:48 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-08 16:15:48 - [DataTable] ComboBoxOperation: Target schema:
2025-11-08 16:15:48 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[16:15:48.249] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-08 16:15:48 - [16:15:48.249] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-08 16:15:48 - [16:15:48.249] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_locations_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638982153482490442"
}
[16:15:48.252] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.252] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.254] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-08 16:15:48 - [16:15:48.254] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[16:15:48.334] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (85ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.334] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (85ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.334] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_locations_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 85,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 10371 location(s)"
  },
  "ResultData": "DataTable[10371 rows]",
  "ErrorMessage": "Retrieved 10371 location(s)"
}
[16:15:48.339] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (85ms) - 10371 rows
2025-11-08 16:15:48 - [16:15:48.339] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (85ms) - 10371 rows
[16:15:48.342] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (90ms)
2025-11-08 16:15:48 - [16:15:48.342] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (90ms)
[16:15:48.344] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (95ms)
2025-11-08 16:15:48 - [16:15:48.344] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (95ms)
2025-11-08 16:15:48 - [16:15:48.344] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_locations_Get_All",
  "ElapsedMs": 95,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638982153482490442",
  "Status": "SUCCESS",
  "RowCount": 10371
}
2025-11-08 16:15:48 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-08 16:15:48 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-08 16:15:48 - [DataTable] ComboBoxLocation: Target schema:
2025-11-08 16:15:48 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[16:15:48.364] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-08 16:15:48 - [16:15:48.364] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-08 16:15:48 - [16:15:48.364] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638982153483642133"
}
[16:15:48.367] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.367] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.370] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-08 16:15:48 - [16:15:48.370] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[16:15:48.402] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (38ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.402] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (38ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.402] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 38,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user(s)"
}
[16:15:48.405] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (38ms) - 88 rows
2025-11-08 16:15:48 - [16:15:48.405] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (38ms) - 88 rows
[16:15:48.408] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (40ms)
2025-11-08 16:15:48 - [16:15:48.408] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (40ms)
[16:15:48.410] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (46ms)
2025-11-08 16:15:48 - [16:15:48.410] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (46ms)
2025-11-08 16:15:48 - [16:15:48.410] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_All",
  "ElapsedMs": 46,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638982153483642133",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-08 16:15:48 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-08 16:15:48 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-08 16:15:48 - [DataTable] ComboBoxUser: Target schema:
2025-11-08 16:15:48 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[16:15:48.423] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-08 16:15:48 - [16:15:48.423] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-08 16:15:48 - [16:15:48.423] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_item_types_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638982153484237767"
}
[16:15:48.427] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.427] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.429] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-08 16:15:48 - [16:15:48.429] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[16:15:48.461] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.461] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.461] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_item_types_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 37,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 4 item type(s)"
  },
  "ResultData": "DataTable[4 rows]",
  "ErrorMessage": "Retrieved 4 item type(s)"
}
[16:15:48.464] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
2025-11-08 16:15:48 - [16:15:48.464] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
[16:15:48.466] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-08 16:15:48 - [16:15:48.466] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[16:15:48.469] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (45ms)
2025-11-08 16:15:48 - [16:15:48.469] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (45ms)
2025-11-08 16:15:48 - [16:15:48.469] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_item_types_Get_All",
  "ElapsedMs": 45,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638982153484237767",
  "Status": "SUCCESS",
  "RowCount": 4
}
2025-11-08 16:15:48 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-08 16:15:48 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-08 16:15:48 - [DataTable] ComboBoxItemType: Target schema:
2025-11-08 16:15:48 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-08 16:15:48 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-08 16:15:48 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-08 16:15:48 - Running VersionChecker - checking database version information.
[16:15:48.549] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-08 16:15:48 - [16:15:48.549] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-08 16:15:48 - [16:15:48.549] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_log_changelog_Get_Current",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638982153485490756"
}
[16:15:48.552] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.552] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.554] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-08 16:15:48 - [16:15:48.554] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-08 16:15:48 - [Splash] Version checker initialized
[16:15:48.588] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (39ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.588] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (39ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.588] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "log_changelog_Get_Current",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 39,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved current changelog version"
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved current changelog version"
}
[16:15:48.592] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (39ms) - 1 rows
2025-11-08 16:15:48 - [16:15:48.592] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (39ms) - 1 rows
[16:15:48.594] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
2025-11-08 16:15:48 - [16:15:48.594] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (41ms)
[16:15:48.596] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (47ms)
2025-11-08 16:15:48 - [16:15:48.596] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (47ms)
2025-11-08 16:15:48 - [16:15:48.596] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_log_changelog_Get_Current",
  "ElapsedMs": 47,
  "Key": "ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638982153485490756",
  "Status": "SUCCESS",
  "RowCount": 1
}
Database version retrieved: 6.0.0.0
2025-11-08 16:15:48 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.1.0, DB: 6.0.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-08 16:15:48 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[16:15:48.625] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 16:15:48 - [16:15:48.625] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 16:15:48 - [16:15:48.625] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982153486259411"
}
[16:15:48.629] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.629] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.631] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-08 16:15:48 - [16:15:48.631] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[16:15:48.639] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (13ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.639] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (13ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.639] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 13,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 9 theme(s)"
  },
  "ResultData": "DataTable[9 rows]",
  "ErrorMessage": "Retrieved 9 theme(s)"
}
[16:15:48.643] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (13ms) - 9 rows
2025-11-08 16:15:48 - [16:15:48.643] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (13ms) - 9 rows
[16:15:48.645] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-08 16:15:48 - [16:15:48.645] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[16:15:48.647] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (21ms)
2025-11-08 16:15:48 - [16:15:48.647] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (21ms)
2025-11-08 16:15:48 - [16:15:48.647] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_theme_GetAll",
  "ElapsedMs": 21,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982153486259411",
  "Status": "SUCCESS",
  "RowCount": 9
}
2025-11-08 16:15:48 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-08 16:15:48 - Successfully loaded 9 themes from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Default' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Forest' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-08 16:15:48 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-08 16:15:48 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-08 16:15:48 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[16:15:48.698] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-08 16:15:48 - [16:15:48.698] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[16:15:48.701] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:48 - [16:15:48.701] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:48.704] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.704] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.704] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153487040782"
}
[16:15:48.707] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.707] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.709] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.709] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:48.739] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (35ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.739] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (35ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.739] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 35,
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
[16:15:48.743] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (35ms) - 1 rows
2025-11-08 16:15:48 - [16:15:48.743] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (35ms) - 1 rows
[16:15:48.746] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-08 16:15:48 - [16:15:48.746] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[16:15:48.748] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (43ms)
2025-11-08 16:15:48 - [16:15:48.748] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (43ms)
2025-11-08 16:15:48 - [16:15:48.748] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 43,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153487040782",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:48.757] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (55ms)
2025-11-08 16:15:48 - [16:15:48.757] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (55ms)
[16:15:48.760] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (61ms)
2025-11-08 16:15:48 - [16:15:48.760] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (61ms)
2025-11-08 16:15:48 - Loaded theme preference for user JOHNK: Forest
2025-11-08 16:15:48 - Set Model_Application_Variables.ThemeName to: Forest
2025-11-08 16:15:48 - Theme system initialized for user JOHNK. Final theme: Forest, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-08 16:15:48 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-08 16:15:48 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-08 16:15:48 - [Splash] Loading theme settings
[16:15:48.888] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-08 16:15:48 - [16:15:48.888] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[16:15:48.890] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:48 - [16:15:48.890] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:48.892] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.892] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.892] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153488922950"
}
[16:15:48.895] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.895] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.898] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.898] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:48.902] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.902] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (10ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.902] [DATA  ] {
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
[16:15:48.905] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
2025-11-08 16:15:48 - [16:15:48.905] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (10ms) - 1 rows
[16:15:48.908] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 16:15:48 - [16:15:48.908] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[16:15:48.910] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-08 16:15:48 - [16:15:48.910] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (18ms)
2025-11-08 16:15:48 - [16:15:48.910] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153488922950",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:48.915] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
2025-11-08 16:15:48 - [16:15:48.915] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
[16:15:48.917] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (29ms)
2025-11-08 16:15:48 - [16:15:48.917] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (29ms)
[16:15:48.921] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-08 16:15:48 - [16:15:48.921] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[16:15:48.923] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:48 - [16:15:48.923] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:48.925] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.925] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.925] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153489258549"
}
[16:15:48.929] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:48 - [16:15:48.929] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:48.931] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:48 - [16:15:48.931] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:48.937] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.937] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (11ms) - Status: 1
2025-11-08 16:15:48 - [16:15:48.937] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 11,
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
[16:15:48.940] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
2025-11-08 16:15:48 - [16:15:48.940] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (11ms) - 1 rows
[16:15:48.942] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-08 16:15:48 - [16:15:48.942] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[16:15:48.945] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (19ms)
2025-11-08 16:15:48 - [16:15:48.945] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (19ms)
2025-11-08 16:15:48 - [16:15:48.945] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 19,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153489258549",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:48.948] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (25ms)
2025-11-08 16:15:48 - [16:15:48.948] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (25ms)
[16:15:48.951] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (30ms)
2025-11-08 16:15:48 - [16:15:48.951] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (30ms)
2025-11-08 16:15:48 - [Splash] Theme settings loaded - Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-08 16:15:48 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-08 16:15:49 - [Splash] Core startup sequence completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
[16:15:49.321] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-08 16:15:49 - [16:15:49.321] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[16:15:49.324] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-08 16:15:49 - [16:15:49.324] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_ConnectionStrengthControl' and all its controls.
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_ConnectionStrengthControl'.
[16:15:49.353] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.353] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[16:15:49.355] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.355] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[16:15:49.365] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.365] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_InventoryTab' and all its controls.
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_InventoryTab'.
[16:15:49.380] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.380] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[16:15:49.383] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.383] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[16:15:49.385] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.385] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[16:15:49.388] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.388] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[16:15:49.400] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.400] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-08 16:15:49 - Inventory tab events wired up.
[16:15:49.404] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.404] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[16:15:49.406] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.406] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-08 16:15:49 - Inventory Quantity TextBox changed.
[16:15:49.411] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.411] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[16:15:49.414] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-08 16:15:49 - [16:15:49.414] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[16:15:49.416] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (62ms)
2025-11-08 16:15:49 - [16:15:49.416] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (62ms)
[16:15:49.419] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-08 16:15:49 - [16:15:49.419] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[16:15:49.421] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-08 16:15:49 - [16:15:49.421] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-08 16:15:49 - Control_AdvancedInventory constructor entered.
[16:15:49.434] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-08 16:15:49 - [16:15:49.434] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_AdvancedInventory' and all its controls.
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel4'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel2'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel3'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_AdvancedInventory'.
[16:15:49.487] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-08 16:15:49 - [16:15:49.487] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-08 16:15:49 - Control_AdvancedInventory constructor exited.
[16:15:49.492] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.492] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[16:15:49.494] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.494] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[16:15:49.503] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.503] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_RemoveTab' and all its controls.
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_RemoveTab'.
[16:15:49.525] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.525] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[16:15:49.528] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.528] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[16:15:49.530] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.530] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[16:15:49.540] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.540] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[16:15:49.542] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.542] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[16:15:49.544] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.544] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[16:15:49.546] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-08 16:15:49 - [16:15:49.546] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[16:15:49.548] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (56ms)
2025-11-08 16:15:49 - [16:15:49.548] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (56ms)
[16:15:49.552] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-08 16:15:49 - [16:15:49.552] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[16:15:49.554] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-08 16:15:49 - [16:15:49.554] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[16:15:49.563] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-08 16:15:49 - [16:15:49.563] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_AdvancedRemove' and all its controls.
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_AdvancedRemove'.
[16:15:49.599] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-08 16:15:49 - [16:15:49.599] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[16:15:49.603] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-08 16:15:49 - [16:15:49.603] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[16:15:49.609] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-08 16:15:49 - [16:15:49.609] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[16:15:49.611] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-08 16:15:49 - [16:15:49.611] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_TransferTab' and all its controls.
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_TransferTab'.
2025-11-08 16:15:49 - Transfer tab events wired up.
[16:15:49.646] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-08 16:15:49 - [16:15:49.646] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[16:15:49.649] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-08 16:15:49 - [16:15:49.649] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[16:15:49.652] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-08 16:15:49 - [16:15:49.652] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[16:15:49.712] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-08 16:15:49 - [16:15:49.712] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[16:15:49.743] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-08 16:15:49 - [16:15:49.743] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-08 16:15:49 - DPI scaling applied to user control 'Control_QuickButtons' and all its controls.
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 16:15:49 - Runtime layout adjustments applied to user control 'Control_QuickButtons'.
2025-11-08 16:15:49 - Inventory Part ComboBox selection changed.
2025-11-08 16:15:49 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[16:15:49.780] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-08 16:15:49 - [16:15:49.780] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-08 16:15:49 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel4'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel2'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel3'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-08 16:15:49 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 16:15:49 - Inventory Op ComboBox selection changed.
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:49 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 16:15:49 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
2025-11-08 16:15:49 - Applied Panel layout adjustments to ''
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 16:15:49 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:15:49 - Runtime layout adjustments applied to form 'MainForm'.
[DEBUG] [MainForm.ctor] InitializeComponent complete.
[16:15:49.881] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-08 16:15:49 - [16:15:49.881] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[16:15:49.945] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-08 16:15:49 - [16:15:49.945] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[16:15:49.950] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-08 16:15:49 - [16:15:49.950] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[16:15:49.953] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (7ms)
2025-11-08 16:15:49 - [16:15:49.953] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (7ms)
[16:15:49.979] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-08 16:15:49 - [16:15:49.979] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[16:15:49.981] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-08 16:15:49 - [16:15:49.981] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[16:15:49.983] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (101ms)
2025-11-08 16:15:49 - [16:15:49.983] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (101ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[16:15:49.989] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-08 16:15:49 - [16:15:49.989] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[16:15:50.026] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-08 16:15:50 - [16:15:50.026] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[16:15:50.030] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-08 16:15:50 - [16:15:50.030] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-08 16:15:50 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[16:15:50.032] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-08 16:15:50 - [16:15:50.032] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-08 16:15:50 - Transfer tab ComboBoxes loaded.
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[16:15:50.059] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[16:15:50.060] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 16:15:50 - [16:15:50.060] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 16:15:50 - [16:15:50.059] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[16:15:50.062] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:50 - [16:15:50.062] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[DEBUG] [MainForm.ctor] Events wired up.
[16:15:50.067] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-08 16:15:50 - [16:15:50.062] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153500628698"
}
2025-11-08 16:15:50 - [16:15:50.067] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
[16:15:50.069] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:50 - [16:15:50.069] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:50.073] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 16:15:50 - [16:15:50.073] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 16:15:50 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[16:15:50.078] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-08 16:15:50 - [16:15:50.078] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
[16:15:50.080] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (50ms)
2025-11-08 16:15:50 - [16:15:50.080] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (50ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[16:15:50.084] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-08 16:15:50 - [16:15:50.084] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[DEBUG] [MainForm.ctor] MainForm constructed.
[16:15:50.087] [MEDIUM] ⬅️ EXITING MainForm.MainForm (765ms)
2025-11-08 16:15:50 - [16:15:50.087] [MEDIUM] ⬅️ EXITING MainForm.MainForm (765ms)
2025-11-08 16:15:50 - [Splash] MainForm created
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-08 16:15:50 - Remove tab ComboBoxes loaded.
2025-11-08 16:15:50 - Removal tab events wired up.
2025-11-08 16:15:50 - Initial setup of ComboBoxes in the Remove Tab.
[16:15:50.098] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 16:15:50 - [16:15:50.098] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[16:15:50.100] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:50 - [16:15:50.100] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:50 - [16:15:50.100] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153501004938"
}
[16:15:50.104] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:50 - [16:15:50.104] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:50.106] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 16:15:50 - [16:15:50.106] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[16:15:50.113] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 16:15:50 - [16:15:50.113] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 16:15:50 - Inventory Op ComboBox selection changed.
[16:15:50.118] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 16:15:50 - [16:15:50.118] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[16:15:50.141] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (78ms) - Status: 1
2025-11-08 16:15:50 - [16:15:50.141] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (78ms) - Status: 1
2025-11-08 16:15:50 - [16:15:50.141] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 78,
  "Thread": 20,
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
[16:15:50.146] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (78ms) - 1 rows
2025-11-08 16:15:50 - [16:15:50.146] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (78ms) - 1 rows
[16:15:50.149] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (79ms)
2025-11-08 16:15:50 - [16:15:50.149] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (79ms)
[16:15:50.151] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (88ms)
2025-11-08 16:15:50 - [16:15:50.151] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (88ms)
2025-11-08 16:15:50 - [16:15:50.151] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 88,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153500628698",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:50.155] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (57ms)
2025-11-08 16:15:50 - [16:15:50.155] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (57ms)
2025-11-08 16:15:50 - User full name loaded: John Koll
[16:15:50.163] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 16:15:50 - [16:15:50.163] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 16:15:50 - Inventory Location ComboBox selection changed.
[16:15:50.167] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 16:15:50 - [16:15:50.167] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 16:15:50 - [Splash] All form instances configured successfully
2025-11-08 16:15:50 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
[16:15:50.176] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (75ms) - Status: 1
2025-11-08 16:15:50 - [16:15:50.176] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (75ms) - Status: 1
2025-11-08 16:15:50 - [16:15:50.176] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 75,
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
[16:15:50.179] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (75ms) - 1 rows
2025-11-08 16:15:50 - [16:15:50.179] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (75ms) - 1 rows
[16:15:50.182] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (78ms)
2025-11-08 16:15:50 - [16:15:50.182] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (78ms)
[16:15:50.185] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (84ms)
2025-11-08 16:15:50 - [16:15:50.185] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (84ms)
2025-11-08 16:15:50 - [16:15:50.185] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 84,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153501004938",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:50.188] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (128ms)
2025-11-08 16:15:50 - [16:15:50.188] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (128ms)
2025-11-08 16:15:50 - User full name loaded: John Koll
2025-11-08 16:15:50 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-08 16:15:50 - Applied Panel layout adjustments to ''
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'panel4'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'panel2'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'panel3'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 16:15:50 - Applied Panel layout adjustments to ''
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 16:15:50 - Applied Panel layout adjustments to ''
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-08 16:15:50 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 16:15:50 - Applied Panel layout adjustments to ''
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 16:15:50 - Applied Panel layout adjustments to ''
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:50 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 16:15:50 - Applied Panel layout adjustments to ''
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 16:15:50 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:15:50 - Runtime layout adjustments applied to form 'MainForm'.
2025-11-08 16:15:50 - Global theme 'Forest' with DPI scaling applied to form 'MainForm'.
2025-11-08 16:15:50 - [Splash] Theme applied to MainForm
2025-11-08 16:15:50 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
2025-11-08 16:15:50 - Inventory tab ComboBoxes loaded.
[16:15:50.336] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (948ms)
2025-11-08 16:15:50 - [16:15:50.336] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (948ms)
[16:15:51.650] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-08 16:15:51 - [16:15:51.650] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-08 16:15:51 - [Splash] MainForm displayed successfully
2025-11-08 16:15:51 - [Splash] MainForm displayed - startup complete
2025-11-08 16:15:51 - [Splash] Splash screen closed
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[16:15:51.687] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 16:15:51 - [16:15:51.687] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[16:15:51.689] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.689] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.689] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153516891730"
}
[16:15:51.692] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:51 - [16:15:51.692] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:51.695] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.695] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[16:15:51.700] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (11ms) - Status: 1
2025-11-08 16:15:51 - [16:15:51.700] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (11ms) - Status: 1
2025-11-08 16:15:51 - [16:15:51.700] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 11,
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
[16:15:51.704] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (11ms) - 1 rows
2025-11-08 16:15:51 - [16:15:51.704] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (11ms) - 1 rows
[16:15:51.706] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-08 16:15:51 - [16:15:51.706] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[16:15:51.709] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (20ms)
2025-11-08 16:15:51 - [16:15:51.709] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (20ms)
2025-11-08 16:15:51 - [16:15:51.709] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 20,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153516891730",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:51.713] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (26ms)
2025-11-08 16:15:51 - [16:15:51.713] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (26ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[16:15:51.717] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-08 16:15:51 - [16:15:51.717] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[16:15:51.722] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-08 16:15:51 - [16:15:51.722] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[16:15:51.724] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-08 16:15:51 - [16:15:51.724] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-08 16:15:51 - Application Info - Development Menu configured for user 'JOHNK': Visible
[16:15:51.728] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (10ms)
2025-11-08 16:15:51 - [16:15:51.728] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (10ms)
[16:15:51.757] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-08 16:15:51 - [16:15:51.757] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-08 16:15:51 -
2025-11-08 16:15:51 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 16:15:51 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-08 16:15:51 - [QuickButtons]   User: JOHNK
2025-11-08 16:15:51 - [QuickButtons] ════════════════════════════════════════════════════════════
[16:15:51.767] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 16:15:51 - [16:15:51.767] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 16:15:51 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-08 16:15:51 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[16:15:51.774] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.774] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.774] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982153517745696"
}
[16:15:51.778] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:51 - [16:15:51.778] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:51.780] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.780] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[16:15:51.811] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (37ms) - Status: 1
2025-11-08 16:15:51 - [16:15:51.811] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (37ms) - Status: 1
2025-11-08 16:15:51 - [16:15:51.811] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 37,
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
[16:15:51.815] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (37ms) - 6 rows
2025-11-08 16:15:51 - [16:15:51.815] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (37ms) - 6 rows
[16:15:51.817] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
2025-11-08 16:15:51 - [16:15:51.817] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (38ms)
[16:15:51.819] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (45ms)
2025-11-08 16:15:51 - [16:15:51.819] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (45ms)
2025-11-08 16:15:51 - [16:15:51.819] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 45,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982153517745696",
  "Status": "SUCCESS",
  "RowCount": 6
}
2025-11-08 16:15:51 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 16:15:51 - [Dao_QuickButtons] Added to array: 01-27991-006 + 10 (Qty: 40)
2025-11-08 16:15:51 - [Dao_QuickButtons] Added to array: 04-27693-000 + 90 (Qty: 10)
2025-11-08 16:15:51 - [Dao_QuickButtons] Added to array: 01-34578-000 + 880 (Qty: 20)
2025-11-08 16:15:51 - [Dao_QuickButtons] Added to array: 03-29236-030 + 959 (Qty: 30)
2025-11-08 16:15:51 - [Dao_QuickButtons] Added to array: 06-96408-001 + N/A (Qty: 40)
2025-11-08 16:15:51 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 10)
2025-11-08 16:15:51 - [Dao_QuickButtons] Array restructured: 6 unique buttons, 0 duplicates removed
2025-11-08 16:15:51 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 16:15:51 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 16:15:51 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 16:15:51 - [Dao_QuickButtons] Created button at position 1: 01-27991-006 + 10 (Qty: 40)
2025-11-08 16:15:51 - [Dao_QuickButtons] Created button at position 2: 04-27693-000 + 90 (Qty: 10)
2025-11-08 16:15:51 - [Dao_QuickButtons] Created button at position 3: 01-34578-000 + 880 (Qty: 20)
2025-11-08 16:15:51 - [Dao_QuickButtons] Created button at position 4: 03-29236-030 + 959 (Qty: 30)
2025-11-08 16:15:51 - [Dao_QuickButtons] Created button at position 5: 06-96408-001 + N/A (Qty: 40)
2025-11-08 16:15:51 - [Dao_QuickButtons] Created button at position 6: 01-33016-000 + 109 (Qty: 10)
2025-11-08 16:15:51 - [Dao_QuickButtons] Created 6 buttons in database
2025-11-08 16:15:51 - [Dao_QuickButtons] Cleanup complete: 0 duplicates removed, 6 buttons remain
2025-11-08 16:15:51 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 16:15:51 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 0 duplicates removed, 6 buttons remain
2025-11-08 16:15:51 - [QuickButtons] STEP 2: Loading data from database
[16:15:51.935] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.935] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.935] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982153519357327"
}
[16:15:51.939] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:51 - [16:15:51.939] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:51.942] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 16:15:51 - [16:15:51.942] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[16:15:51.947] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 16:15:51 - [16:15:51.947] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 16:15:51 - [16:15:51.947] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 11,
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
[16:15:51.950] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 6 rows
2025-11-08 16:15:51 - [16:15:51.950] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 6 rows
[16:15:51.952] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 16:15:51 - [16:15:51.952] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[16:15:51.954] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-08 16:15:51 - [16:15:51.954] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-08 16:15:51 - [16:15:51.954] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 19,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982153519357327",
  "Status": "SUCCESS",
  "RowCount": 6
}
[16:15:51.961] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 16:15:51 - [16:15:51.961] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 16:15:51 - [QuickButtons] STEP 2: ✓ Retrieved 6 button(s) from database
2025-11-08 16:15:51 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-08 16:15:51 - [QuickButtons] STEP 3:   Button 1: 01-27991-006 + Op:10 (Qty: 40)
2025-11-08 16:15:51 - [QuickButtons] STEP 3:   Button 2: 04-27693-000 + Op:90 (Qty: 10)
2025-11-08 16:15:51 - [QuickButtons] STEP 3:   Button 3: 01-34578-000 + Op:880 (Qty: 20)
2025-11-08 16:15:51 - [QuickButtons] STEP 3:   Button 4: 03-29236-030 + Op:959 (Qty: 30)
2025-11-08 16:15:51 - [QuickButtons] STEP 3:   Button 5: 06-96408-001 + Op:N/A (Qty: 40)
2025-11-08 16:15:52 - [QuickButtons] STEP 3:   Button 6: 01-33016-000 + Op:109 (Qty: 10)
2025-11-08 16:15:52 - [QuickButtons] STEP 3: Filled 6 button(s) with data
2025-11-08 16:15:52 - [QuickButtons] STEP 3: Clearing remaining 4 button(s)
2025-11-08 16:15:52 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-08 16:15:52 - [QuickButtons] STEP 4: Layout refreshed - 6 visible button(s)
2025-11-08 16:15:52 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-08 16:15:52 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-08 16:15:52 - [QuickButtons] ║ User: JOHNK
2025-11-08 16:15:52 - [QuickButtons] ║ Visible Buttons: 6
2025-11-08 16:15:52 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-08 16:15:52 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 16:15:52 -
[16:15:52.041] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (272ms)
2025-11-08 16:15:52 - [16:15:52.041] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (272ms)
[16:15:52.043] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-08 16:15:52 - [16:15:52.043] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
[16:15:53.996] [LOW   ] 🖱️ UI ACTION: SETTINGS_MENU_CLICK on MainForm
2025-11-08 16:15:53 - [16:15:53.996] [LOW   ] 🖱️ UI ACTION: SETTINGS_MENU_CLICK on MainForm
[16:15:53.999] [MEDIUM] ➡️ ENTERING MainForm.MainForm_MenuStrip_File_Settings_Click
2025-11-08 16:15:54 - [16:15:53.999] [MEDIUM] ➡️ ENTERING MainForm.MainForm_MenuStrip_File_Settings_Click
[16:15:54.001] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_OPEN on MainForm
2025-11-08 16:15:54 - [16:15:54.001] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_OPEN on MainForm
[16:15:54.004] [MEDIUM] ➡️ ENTERING SettingsForm.SettingsForm
2025-11-08 16:15:54 - [16:15:54.004] [MEDIUM] ➡️ ENTERING SettingsForm.SettingsForm
[16:15:54.006] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
2025-11-08 16:15:54 - [16:15:54.006] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
[16:15:54.011] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SettingsForm
2025-11-08 16:15:54 - [16:15:54.011] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SettingsForm
2025-11-08 16:15:54 - DPI scaling applied to form 'SettingsForm' and all its controls.
2025-11-08 16:15:54 - Applied Panel layout adjustments to ''
2025-11-08 16:15:54 - Applied Panel layout adjustments to ''
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'SettingsForm_TableLayout_Right'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_Right'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_Database'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_Shortcuts'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_About'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddPart'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditPart'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemovePart'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddOperation'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditOperation'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemoveOperation'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddLocation'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditLocation'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemoveLocation'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddItemType'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditItemType'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemoveItemType'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddUser'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditUser'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_DeleteUser'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_Theme'
2025-11-08 16:15:54 - Applied Panel layout adjustments to 'SettingsForm_Panel_Right_Main'
2025-11-08 16:15:54 - Runtime layout adjustments applied to form 'SettingsForm'.
[16:15:54.049] [LOW   ] 🖱️ UI ACTION: SETTINGS_PANELS_INITIALIZATION on SettingsForm
2025-11-08 16:15:54 - [16:15:54.049] [LOW   ] 🖱️ UI ACTION: SETTINGS_PANELS_INITIALIZATION on SettingsForm
[16:15:54.051] [LOW   ] 🖱️ UI ACTION: INITIALIZE_CONTROLS on SettingsForm
2025-11-08 16:15:54 - [16:15:54.051] [LOW   ] 🖱️ UI ACTION: INITIALIZE_CONTROLS on SettingsForm
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Shortcuts' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Shortcuts_GroupBox_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Shortcuts'.
[16:15:54.065] [MEDIUM] ➡️ ENTERING Dao_User.GetShortcutsJsonAsync
2025-11-08 16:15:54 - [16:15:54.065] [MEDIUM] ➡️ ENTERING Dao_User.GetShortcutsJsonAsync
[16:15:54.067] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_GetShortcutsJson
2025-11-08 16:15:54 - [16:15:54.067] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_GetShortcutsJson
2025-11-08 16:15:54 - [16:15:54.067] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_GetShortcutsJson",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_GetShortcutsJson:638982153540679843"
}
[16:15:54.071] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.071] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.074] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_GetShortcutsJson
2025-11-08 16:15:54 - [16:15:54.074] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_GetShortcutsJson
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Theme' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Themes_GroupBox_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Themes_TableLayout_Main'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Theme'.
[16:15:54.085] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-08 16:15:54 - [16:15:54.085] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[16:15:54.088] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:54 - [16:15:54.088] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:54.090] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.090] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.090] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153540905703"
}
[16:15:54.094] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.094] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.097] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.097] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Database' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Database_GroupBox_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Bottom'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Database'.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_About' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_About_GroupBox_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_1'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_2'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_Top'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_3'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_4'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_About'.
[16:15:54.165] [MEDIUM] ➡️ ENTERING Control_Add_User.Control_Add_User
2025-11-08 16:15:54 - [16:15:54.165] [MEDIUM] ➡️ ENTERING Control_Add_User.Control_Add_User
[16:15:54.167] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.167] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
[16:15:54.173] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.173] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Add_User
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Add_User' and all its controls.
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel7'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Add_User_GroupBox_UserInfo'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel5'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel6'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'groupBox1'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel2'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel4'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Add_User_GroupBox_UserPrivileges'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Add_User_GroupBox_VisualInfo'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel3'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Add_User'.
[16:15:54.206] [LOW   ] 🖱️ UI ACTION: DEFAULT_USER_TYPE_SET on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.206] [LOW   ] 🖱️ UI ACTION: DEFAULT_USER_TYPE_SET on Control_Add_User
[16:15:54.208] [LOW   ] 🖱️ UI ACTION: DEVELOPER_ROLE_WIRED on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.208] [LOW   ] 🖱️ UI ACTION: DEVELOPER_ROLE_WIRED on Control_Add_User
[16:15:54.211] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.211] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Add_User
[16:15:54.214] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.214] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Add_User
[16:15:54.216] [LOW   ] 🖱️ UI ACTION: VISUAL_ACCESS_EVENT_SETUP on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.216] [LOW   ] 🖱️ UI ACTION: VISUAL_ACCESS_EVENT_SETUP on Control_Add_User
[16:15:54.218] [LOW   ] 🖱️ UI ACTION: VIEW_PASSWORDS_EVENT_SETUP on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.218] [LOW   ] 🖱️ UI ACTION: VIEW_PASSWORDS_EVENT_SETUP on Control_Add_User
[16:15:54.221] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
2025-11-08 16:15:54 - [16:15:54.221] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
[16:15:54.224] [MEDIUM] ⬅️ EXITING Control_Add_User.Control_Add_User (59ms)
2025-11-08 16:15:54 - [16:15:54.224] [MEDIUM] ⬅️ EXITING Control_Add_User.Control_Add_User (59ms)
[16:15:54.228] [MEDIUM] ➡️ ENTERING Control_Edit_User.Control_Edit_User
2025-11-08 16:15:54 - [16:15:54.228] [MEDIUM] ➡️ ENTERING Control_Edit_User.Control_Edit_User
[16:15:54.231] [LOW   ] 🖱️ UI ACTION: EDIT_USER_INITIALIZATION on Control_Edit_User
2025-11-08 16:15:54 - [16:15:54.231] [LOW   ] 🖱️ UI ACTION: EDIT_USER_INITIALIZATION on Control_Edit_User
[16:15:54.239] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Edit_User
2025-11-08 16:15:54 - [16:15:54.239] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Edit_User
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Edit_User' and all its controls.
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel5'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel6'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_VisualInfo'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel8'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_UserInfo'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel4'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel2'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_Creds'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel3'
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_UserPrivileges'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel7'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Edit_User'.
[16:15:54.285] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Edit_User
2025-11-08 16:15:54 - [16:15:54.285] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Edit_User
[16:15:54.288] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Edit_User
2025-11-08 16:15:54 - [16:15:54.288] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Edit_User
[16:15:54.293] [MEDIUM] ➡️ ENTERING Control_Remove_User.Control_Remove_User
2025-11-08 16:15:54 - [16:15:54.293] [MEDIUM] ➡️ ENTERING Control_Remove_User.Control_Remove_User
[16:15:54.296] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
2025-11-08 16:15:54 - [16:15:54.296] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Remove_User' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'RemoveUserControl_GroupBox_UserInfo'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Remove_User'.
[16:15:54.305] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_Remove_User
2025-11-08 16:15:54 - [16:15:54.305] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_Remove_User
[16:15:54.307] [LOW   ] 🖱️ UI ACTION: USERS_DATA_LOADING on Control_Remove_User
2025-11-08 16:15:54 - [16:15:54.307] [LOW   ] 🖱️ UI ACTION: USERS_DATA_LOADING on Control_Remove_User
[16:15:54.312] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
2025-11-08 16:15:54 - [16:15:54.312] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
[16:15:54.314] [MEDIUM] ⬅️ EXITING Control_Remove_User.Control_Remove_User (21ms)
2025-11-08 16:15:54 - [16:15:54.314] [MEDIUM] ⬅️ EXITING Control_Remove_User.Control_Remove_User (21ms)
[16:15:54.317] [MEDIUM] ➡️ ENTERING Control_Add_PartID.Control_Add_PartID
2025-11-08 16:15:54 - [16:15:54.317] [MEDIUM] ➡️ ENTERING Control_Add_PartID.Control_Add_PartID
[16:15:54.320] [LOW   ] 🖱️ UI ACTION: ADD_PARTID_INITIALIZATION on Control_Add_PartID
2025-11-08 16:15:54 - [16:15:54.320] [LOW   ] 🖱️ UI ACTION: ADD_PARTID_INITIALIZATION on Control_Add_PartID
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Add_PartID' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Add_PartID_GroupBox_NewPartID'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Add_PartID_TableLayout_NewPartIDEntry'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Add_PartID'.
[16:15:54.330] [LOW   ] 🖱️ UI ACTION: PART_TYPES_LOADING on Control_Add_PartID
2025-11-08 16:15:54 - [16:15:54.330] [LOW   ] 🖱️ UI ACTION: PART_TYPES_LOADING on Control_Add_PartID
[16:15:54.334] [MEDIUM] ⬅️ EXITING Control_Add_PartID.Control_Add_PartID (16ms)
2025-11-08 16:15:54 - [16:15:54.334] [MEDIUM] ⬅️ EXITING Control_Add_PartID.Control_Add_PartID (16ms)
[16:15:54.337] [MEDIUM] ➡️ ENTERING Control_Edit_PartID.Control_Edit_PartID
2025-11-08 16:15:54 - [16:15:54.337] [MEDIUM] ➡️ ENTERING Control_Edit_PartID.Control_Edit_PartID
[16:15:54.340] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_Edit_PartID
2025-11-08 16:15:54 - [16:15:54.340] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_Edit_PartID
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Edit_PartID' and all its controls.
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Edit_PartID'.
[16:15:54.346] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_BINDING on Control_Edit_PartID
2025-11-08 16:15:54 - [16:15:54.346] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_BINDING on Control_Edit_PartID
[16:15:54.349] [LOW   ] 🖱️ UI ACTION: LOADING_PART_TYPES on Control_Edit_PartID
2025-11-08 16:15:54 - [16:15:54.349] [LOW   ] 🖱️ UI ACTION: LOADING_PART_TYPES on Control_Edit_PartID
[16:15:54.353] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_Edit_PartID
2025-11-08 16:15:54 - [16:15:54.353] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_Edit_PartID
[16:15:54.357] [MEDIUM] ⬅️ EXITING Control_Edit_PartID.Control_Edit_PartID (18ms)
2025-11-08 16:15:54 - [16:15:54.357] [MEDIUM] ⬅️ EXITING Control_Edit_PartID.Control_Edit_PartID (18ms)
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Remove_PartID' and all its controls.
[16:15:54.363] [MEDIUM] ➡️ ENTERING Control_Add_Operation.Control_Add_Operation
2025-11-08 16:15:54 - [16:15:54.363] [MEDIUM] ➡️ ENTERING Control_Add_Operation.Control_Add_Operation
[16:15:54.365] [LOW   ] 🖱️ UI ACTION: ADD_OPERATION_INITIALIZATION on Control_Add_Operation
2025-11-08 16:15:54 - [16:15:54.365] [LOW   ] 🖱️ UI ACTION: ADD_OPERATION_INITIALIZATION on Control_Add_Operation
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Add_Operation' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Shortcuts_GroupBox_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Add_Operation'.
[16:15:54.376] [MEDIUM] ⬅️ EXITING Control_Add_Operation.Control_Add_Operation (12ms)
2025-11-08 16:15:54 - [16:15:54.376] [MEDIUM] ⬅️ EXITING Control_Add_Operation.Control_Add_Operation (12ms)
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Edit_Operation' and all its controls.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Remove_Operation' and all its controls.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Add_Location' and all its controls.
2025-11-08 16:15:54 - Applied GroupBox layout adjustments to 'Control_Shortcuts_GroupBox_Main'
2025-11-08 16:15:54 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Add_Location'.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Edit_Location' and all its controls.
2025-11-08 16:15:54 - Runtime layout adjustments applied to user control 'Control_Edit_Location'.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Remove_Location' and all its controls.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Add_ItemType' and all its controls.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Edit_ItemType' and all its controls.
2025-11-08 16:15:54 - DPI scaling applied to user control 'Control_Remove_ItemType' and all its controls.
[16:15:54.407] [LOW   ] 🖱️ UI ACTION: INITIALIZE_FORM on SettingsForm
2025-11-08 16:15:54 - [16:15:54.407] [LOW   ] 🖱️ UI ACTION: INITIALIZE_FORM on SettingsForm
[16:15:54.411] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
2025-11-08 16:15:54 - [16:15:54.411] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
[16:15:54.413] [MEDIUM] ⬅️ EXITING SettingsForm.SettingsForm (409ms)
2025-11-08 16:15:54 - [16:15:54.413] [MEDIUM] ⬅️ EXITING SettingsForm.SettingsForm (409ms)
[16:15:54.439] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (348ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.439] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (348ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.439] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 348,
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
[16:15:54.442] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (348ms) - 1 rows
2025-11-08 16:15:54 - [16:15:54.442] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (348ms) - 1 rows
[16:15:54.445] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (373ms)
2025-11-08 16:15:54 - [16:15:54.445] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (373ms)
[16:15:54.447] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (356ms)
2025-11-08 16:15:54 - [16:15:54.447] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (356ms)
2025-11-08 16:15:54 - [16:15:54.447] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 356,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153540905703",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:54.452] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (363ms)
2025-11-08 16:15:54 - [16:15:54.452] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (363ms)
[16:15:54.454] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (368ms)
2025-11-08 16:15:54 - [16:15:54.454] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (368ms)
[16:15:54.457] [HIGH  ] ✅ PROCEDURE usr_ui_settings_GetShortcutsJson (389ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.457] [HIGH  ] ✅ PROCEDURE usr_ui_settings_GetShortcutsJson (389ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.457] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_GetShortcutsJson",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 389,
  "Thread": 1,
  "InputParameters": {
    "p_UserId": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved shortcuts for user \"JOHNK\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved shortcuts for user \"JOHNK\""
}
[16:15:54.461] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_GetShortcutsJson (389ms) - 1 rows
2025-11-08 16:15:54 - [16:15:54.461] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_GetShortcutsJson (389ms) - 1 rows
[16:15:54.463] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (369ms)
2025-11-08 16:15:54 - [16:15:54.463] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (369ms)
[16:15:54.466] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_GetShortcutsJson (398ms)
2025-11-08 16:15:54 - [16:15:54.466] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_GetShortcutsJson (398ms)
2025-11-08 16:15:54 - [16:15:54.466] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_GetShortcutsJson",
  "ElapsedMs": 398,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_GetShortcutsJson:638982153540679843",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:54.470] [MEDIUM] ⬅️ EXITING Dao_User.GetShortcutsJsonAsync (404ms)
2025-11-08 16:15:54 - [16:15:54.470] [MEDIUM] ⬅️ EXITING Dao_User.GetShortcutsJsonAsync (404ms)
[16:15:54.477] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-08 16:15:54 - [16:15:54.477] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-08 16:15:54 - [16:15:54.477] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638982153544772477"
}
[16:15:54.480] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.480] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.482] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-08 16:15:54 - [16:15:54.482] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[16:15:54.489] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (12ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.489] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (12ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.489] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 12,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user(s)"
}
[16:15:54.492] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (12ms) - 88 rows
2025-11-08 16:15:54 - [16:15:54.492] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (12ms) - 88 rows
[16:15:54.495] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-08 16:15:54 - [16:15:54.495] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[16:15:54.498] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (20ms)
2025-11-08 16:15:54 - [16:15:54.498] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (20ms)
2025-11-08 16:15:54 - [16:15:54.498] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_All",
  "ElapsedMs": 20,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638982153544772477",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-08 16:15:54 - [DataTable] ComboBoxUser: Successfully merged 88 rows
[16:15:54.507] [MEDIUM] ➡️ ENTERING Dao_User.GetUserByUsernameAsync
2025-11-08 16:15:54 - [16:15:54.507] [MEDIUM] ➡️ ENTERING Dao_User.GetUserByUsernameAsync
[16:15:54.510] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:54 - [16:15:54.510] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 16:15:54 - [16:15:54.510] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153545103042"
}
[16:15:54.514] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.514] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.517] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 16:15:54 - [16:15:54.517] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[16:15:54.523] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (13ms) - Status: 0
2025-11-08 16:15:54 - [16:15:54.523] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (13ms) - Status: 0
2025-11-08 16:15:54 - [16:15:54.523] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 0,
  "ElapsedMs": 13,
  "Thread": 1,
  "InputParameters": {
    "p_User": "System.Data.DataRowView"
  },
  "OutputParameters": {
    "Status": "0",
    "ErrorMsg": "User \"System.Data.DataRowView\" not found"
  },
  "ResultData": "DataTable[0 rows]",
  "ErrorMessage": "User \"System.Data.DataRowView\" not found"
}
[16:15:54.527] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (13ms) - 0 rows
2025-11-08 16:15:54 - [16:15:54.527] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (13ms) - 0 rows
[16:15:54.530] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
2025-11-08 16:15:54 - [16:15:54.530] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
[16:15:54.532] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (22ms)
2025-11-08 16:15:54 - [16:15:54.532] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (22ms)
2025-11-08 16:15:54 - [16:15:54.532] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 22,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982153545103042",
  "Status": "SUCCESS",
  "RowCount": 0
}
[16:15:54.537] [MEDIUM] ⬅️ EXITING Dao_User.GetUserByUsernameAsync (29ms)
2025-11-08 16:15:54 - [16:15:54.537] [MEDIUM] ⬅️ EXITING Dao_User.GetUserByUsernameAsync (29ms)
[16:15:54.545] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerAddressAsync
2025-11-08 16:15:54 - [16:15:54.545] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerAddressAsync
[16:15:54.547] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:54 - [16:15:54.547] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:54.550] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.550] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.550] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153545505010"
}
[16:15:54.554] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.554] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.557] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.557] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:54.581] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (31ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.581] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (31ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.581] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 31,
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
[16:15:54.585] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (31ms) - 1 rows
2025-11-08 16:15:54 - [16:15:54.585] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (31ms) - 1 rows
[16:15:54.588] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (33ms)
2025-11-08 16:15:54 - [16:15:54.588] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (33ms)
[16:15:54.591] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (40ms)
2025-11-08 16:15:54 - [16:15:54.591] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (40ms)
2025-11-08 16:15:54 - [16:15:54.591] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 40,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153545505010",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:54.595] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (47ms)
2025-11-08 16:15:54 - [16:15:54.595] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (47ms)
[16:15:54.598] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerAddressAsync (53ms)
2025-11-08 16:15:54 - [16:15:54.598] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerAddressAsync (53ms)
[16:15:54.602] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerPortAsync
2025-11-08 16:15:54 - [16:15:54.602] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerPortAsync
[16:15:54.604] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:54 - [16:15:54.604] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:54.607] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.607] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.607] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153546071837"
}
[16:15:54.611] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.611] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.614] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.614] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:54.620] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (13ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.620] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (13ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.620] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 13,
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
[16:15:54.624] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (13ms) - 1 rows
2025-11-08 16:15:54 - [16:15:54.624] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (13ms) - 1 rows
[16:15:54.627] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-08 16:15:54 - [16:15:54.627] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[16:15:54.630] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (23ms)
2025-11-08 16:15:54 - [16:15:54.630] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (23ms)
2025-11-08 16:15:54 - [16:15:54.630] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 23,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153546071837",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:54.634] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (30ms)
2025-11-08 16:15:54 - [16:15:54.634] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (30ms)
[16:15:54.637] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerPortAsync (35ms)
2025-11-08 16:15:54 - [16:15:54.637] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerPortAsync (35ms)
[16:15:54.641] [MEDIUM] ➡️ ENTERING Dao_User.GetDatabaseAsync
2025-11-08 16:15:54 - [16:15:54.641] [MEDIUM] ➡️ ENTERING Dao_User.GetDatabaseAsync
[16:15:54.643] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 16:15:54 - [16:15:54.643] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[16:15:54.646] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.646] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.646] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153546461957"
}
[16:15:54.650] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:54 - [16:15:54.650] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:54.653] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:54 - [16:15:54.653] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:54.660] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (13ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.660] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (13ms) - Status: 1
2025-11-08 16:15:54 - [16:15:54.660] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 13,
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
[16:15:54.663] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (13ms) - 1 rows
2025-11-08 16:15:54 - [16:15:54.663] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (13ms) - 1 rows
[16:15:54.666] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-08 16:15:54 - [16:15:54.666] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[16:15:54.669] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (23ms)
2025-11-08 16:15:54 - [16:15:54.669] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (23ms)
2025-11-08 16:15:54 - [16:15:54.669] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 23,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153546461957",
  "Status": "SUCCESS",
  "RowCount": 1
}
[16:15:54.673] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (30ms)
2025-11-08 16:15:54 - [16:15:54.673] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (30ms)
[16:15:54.676] [MEDIUM] ⬅️ EXITING Dao_User.GetDatabaseAsync (35ms)
2025-11-08 16:15:54 - [16:15:54.676] [MEDIUM] ⬅️ EXITING Dao_User.GetDatabaseAsync (35ms)
[16:15:58.913] [MEDIUM] ➡️ ENTERING Dao_User.SetThemeNameAsync
2025-11-08 16:15:58 - [16:15:58.913] [MEDIUM] ➡️ ENTERING Dao_User.SetThemeNameAsync
[16:15:58.916] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:58 - [16:15:58.916] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 16:15:58 - [16:15:58.916] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153589168218"
}
[16:15:58.920] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 16:15:58 - [16:15:58.920] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[16:15:58.922] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 16:15:58 - [16:15:58.922] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[16:15:58.928] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (12ms) - Status: 1
2025-11-08 16:15:58 - [16:15:58.928] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (12ms) - Status: 1
2025-11-08 16:15:58 - [16:15:58.928] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 12,
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
[16:15:58.932] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (12ms) - 1 rows
2025-11-08 16:15:58 - [16:15:58.932] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (12ms) - 1 rows
[16:15:58.934] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-08 16:15:58 - [16:15:58.934] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[16:15:58.937] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (20ms)
2025-11-08 16:15:58 - [16:15:58.937] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (20ms)
2025-11-08 16:15:58 - [16:15:58.937] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 20,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982153589168218",
  "Status": "SUCCESS",
  "RowCount": 1
}
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
2025-11-08 16:15:59 - Database Error [ERROR] - Procedure or function 'usr_ui_settings_Upsert' cannot be found in database '' Verify that user 'root'@'localhost' has enough privileges to execute
2025-11-08 16:15:59 - Stack Trace -    at MySql.Data.MySqlClient.ProcedureCache.GetProcDataAsync(MySqlConnection connection, String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.ProcedureCache.AddNewAsync(MySqlConnection connection, String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.ProcedureCache.GetProcedureAsync(MySqlConnection conn, String spName, String cacheKey, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.GetParametersAsync(String procName, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.CheckParametersAsync(String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.Resolve(Boolean preparing)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQueryAsync(Boolean execAsync, CancellationToken cancellationToken)
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass6_0.<<ExecuteNonQueryWithStatusAsync>b__0>d.MoveNext() in C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 563
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteWithRetryAsync[T](Func`1 operation) in C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 850
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(String connectionString, String procedureName, Dictionary`2 parameters, Helper_StoredProcedureProgress progressHelper, MySqlConnection connection, MySqlTransaction transaction) in C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 526
[16:15:59.029] [MEDIUM] ⬅️ EXITING Dao_User.SetThemeNameAsync (115ms)
2025-11-08 16:15:59 - [16:15:59.029] [MEDIUM] ⬅️ EXITING Dao_User.SetThemeNameAsync (115ms)
2025-11-08 16:15:59 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-08 16:15:59 - Applied Panel layout adjustments to ''
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'panel4'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'panel2'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'panel3'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 16:15:59 - Applied Panel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 16:15:59 - Applied Panel layout adjustments to ''
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-08 16:15:59 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 16:15:59 - Applied Panel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 16:15:59 - Applied Panel layout adjustments to ''
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'panel1'
2025-11-08 16:15:59 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 16:15:59 - Applied Panel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to ''
2025-11-08 16:15:59 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:15:59 - Runtime layout adjustments applied to form 'MainForm'.
2025-11-08 16:16:00 - Global theme 'Lavender' with DPI scaling applied to form 'MainForm'.
2025-11-08 16:16:00 - DPI scaling applied to form 'SettingsForm' and all its controls.
2025-11-08 16:16:00 - Applied Panel layout adjustments to ''
2025-11-08 16:16:00 - Applied Panel layout adjustments to ''
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'SettingsForm_TableLayout_Right'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_Right'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_Database'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Database_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Bottom'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_Shortcuts'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Shortcuts_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_About'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_About_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_1'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_2'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_Top'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_3'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_About_TableLayout_4'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddPart'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Add_PartID_GroupBox_NewPartID'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Add_PartID_TableLayout_NewPartIDEntry'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditPart'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemovePart'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'detailsGroupBox'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddOperation'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Shortcuts_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditOperation'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemoveOperation'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddLocation'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Shortcuts_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditLocation'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemoveLocation'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddItemType'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Add_ItemType_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Add_ItemType_TableLayout'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditItemType'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_RemoveItemType'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_AddUser'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel7'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Add_User_GroupBox_UserInfo'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel5'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel6'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'groupBox1'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel2'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel4'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Add_User_GroupBox_UserPrivileges'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Add_User_GroupBox_VisualInfo'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel3'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_EditUser'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel5'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel6'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_VisualInfo'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel8'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_UserInfo'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel4'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel2'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_Creds'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel3'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Edit_User_GroupBox_UserPrivileges'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel7'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_DeleteUser'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'RemoveUserControl_GroupBox_UserInfo'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_Theme'
2025-11-08 16:16:00 - Applied GroupBox layout adjustments to 'Control_Themes_GroupBox_Main'
2025-11-08 16:16:00 - Applied TableLayoutPanel layout adjustments to 'Control_Themes_TableLayout_Main'
2025-11-08 16:16:00 - Applied Panel layout adjustments to 'SettingsForm_Panel_Right_Main'
2025-11-08 16:16:00 - Runtime layout adjustments applied to form 'SettingsForm'.
2025-11-08 16:16:00 - Global theme 'Lavender' with DPI scaling applied to form 'SettingsForm'.
[16:16:03.214] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_CANCELED on MainForm
2025-11-08 16:16:03 - [16:16:03.214] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_CANCELED on MainForm
2025-11-08 16:16:03 - [Cleanup] Starting application cleanup
2025-11-08 16:16:03 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-08 16:16:03 - [Cleanup] Memory cleanup completed
2025-11-08 16:16:03 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-08 16:16:03 - [Startup] Application shutdown completed
2025-11-08 16:16:03 - [Cleanup] Starting application cleanup
2025-11-08 16:16:03 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-08 16:16:03 - [Cleanup] Memory cleanup completed
2025-11-08 16:16:03 - [Cleanup] Application cleanup completed successfully
2025-11-08 16:16:03 - [Cleanup] Starting application cleanup
2025-11-08 16:16:03 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-08 16:16:03 - [Cleanup] Memory cleanup completed
2025-11-08 16:16:03 - [Cleanup] Application cleanup completed successfully
The program '[33084] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
