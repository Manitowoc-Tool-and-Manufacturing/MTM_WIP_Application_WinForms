------------------------------------------------------------------------------
You may only use the Microsoft Visual Studio .NET/C/C++ Debugger (vsdbg) with
Visual Studio Code, Visual Studio or Visual Studio for Mac software to help you
develop and test your applications.
------------------------------------------------------------------------------
[15:01:02.786] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
2025-11-08 15:01:02 - [15:01:02.786] [LOW   ] 🚀 DEBUG TRACER INITIALIZED
[15:01:02.820] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
2025-11-08 15:01:02 - [15:01:02.820] [LOW   ] 🖱️ UI ACTION: DEBUG_CONFIGURATION_INITIALIZED on Service_DebugConfiguration
[15:01:02.822] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
2025-11-08 15:01:02 - [15:01:02.822] [LOW   ] 🖱️ UI ACTION: DEBUG_MODE_SET on Service_DebugConfiguration
[15:01:02.824] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-08 15:01:02 - [15:01:02.824] [LOW   ] 🖱️ UI ACTION: APPLICATION_STARTUP on Program
2025-11-08 15:01:02 - [Startup] Application initialization started
2025-11-08 15:01:02 - [Startup] User identified: JOHNK
2025-11-08 15:01:02 - [Dao_System] Checking database connectivity
[15:01:02.856] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 15:01:02 - [15:01:02.856] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 15:01:02 - [15:01:02.856] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982108628565115"
}
[15:01:02.920] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:02 - [15:01:02.920] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:02.922] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-08 15:01:02 - [15:01:02.922] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[15:01:03.113] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (256ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.113] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (256ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.113] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 256,
  "Thread": 15,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 9 theme(s)"
  },
  "ResultData": "DataTable[9 rows]",
  "ErrorMessage": "Retrieved 9 theme(s)"
}
[15:01:03.127] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (256ms) - 9 rows
2025-11-08 15:01:03 - [15:01:03.127] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (256ms) - 9 rows
[15:01:03.130] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (208ms)
2025-11-08 15:01:03 - [15:01:03.130] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (208ms)
[15:01:03.131] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (275ms)
2025-11-08 15:01:03 - [15:01:03.131] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (275ms)
2025-11-08 15:01:03 - [15:01:03.131] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_theme_GetAll",
  "ElapsedMs": 275,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982108628565115",
  "Status": "SUCCESS",
  "RowCount": 9
}
2025-11-08 15:01:03 - [Dao_System] Database connectivity check passed
2025-11-08 15:01:03 - [Startup] Database connectivity validated successfully
2025-11-08 15:01:03 - [Startup] Initializing INFORMATION_SCHEMA parameter cache...
2025-11-08 15:01:03 - [Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata
2025-11-08 15:01:03 - [Startup] Parameter cache populated: 116 procedures, 519 total parameters
2025-11-08 15:01:03 - [Startup] Parameter prefix cache initialized successfully in 22ms. Cached 116 stored procedures.
[Startup] Parameter cache: 116 procedures cached in 22ms
[15:01:03.165] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-08 15:01:03 - [15:01:03.165] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_GetUserAccessType
2025-11-08 15:01:03 - [15:01:03.165] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_GetUserAccessType",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638982108631659018"
}
[15:01:03.168] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:03 - [15:01:03.168] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:03.170] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
2025-11-08 15:01:03 - [15:01:03.170] [MEDIUM] 🗄️ DB PROCEDURE START: sys_GetUserAccessType
[Trace] [Main] Application starting...
[Trace] [Main] Application starting...
2025-11-08 15:01:03 - [Splash] Initializing splash screen
2025-11-08 15:01:03 - [Service_ErrorReportSync] Startup sync completed: 0 reports submitted
[15:01:03.199] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
2025-11-08 15:01:03 - [15:01:03.199] [MEDIUM] ➡️ ENTERING SplashScreenForm.SplashScreenForm
[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...
[15:01:03.201] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-08 15:01:03 - [15:01:03.201] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-08 15:01:03 - DPI scaling applied to user control 'Control_ProgressBarUserControl' and all its controls.
2025-11-08 15:01:03 - Runtime layout adjustments applied to user control 'Control_ProgressBarUserControl'.
[15:01:03.236] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (70ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.236] [HIGH  ] ✅ PROCEDURE sys_GetUserAccessType (70ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.236] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_GetUserAccessType",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 70,
  "Thread": 15,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user access type(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user access type(s)"
}
[15:01:03.239] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (70ms) - 88 rows
2025-11-08 15:01:03 - [15:01:03.239] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_GetUserAccessType (70ms) - 88 rows
[15:01:03.242] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (74ms)
2025-11-08 15:01:03 - [15:01:03.242] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (74ms)
[15:01:03.244] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (78ms)
2025-11-08 15:01:03 - [15:01:03.244] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_GetUserAccessType (78ms)
2025-11-08 15:01:03 - [15:01:03.244] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_GetUserAccessType",
  "ElapsedMs": 78,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_GetUserAccessType:638982108631659018",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-08 15:01:03 - System_UserAccessType executed successfully for user: JOHNK
[15:01:03.257] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-08 15:01:03 - [15:01:03.257] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SplashScreenForm
2025-11-08 15:01:03 - DPI scaling applied to form 'SplashScreenForm' and all its controls.
2025-11-08 15:01:03 - Runtime layout adjustments applied to form 'SplashScreenForm'.
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
Exception thrown: 'System.Text.Json.JsonException' in System.Text.Json.dll
[15:01:03.290] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
2025-11-08 15:01:03 - [15:01:03.290] [MEDIUM] 📊 BUSINESS LOGIC: UI_COLORS_APPLICATION
[15:01:03.292] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
2025-11-08 15:01:03 - [15:01:03.292] [LOW   ] 🖱️ UI ACTION: THEME_APPLIED on SplashScreenForm
[DEBUG] [SplashScreenForm.ApplyTheme] Applying theme...
2025-11-08 15:01:03 - DPI scaling applied to form 'SplashScreenForm' and all its controls.
2025-11-08 15:01:03 - Runtime layout adjustments applied to form 'SplashScreenForm'.
2025-11-08 15:01:03 - Global theme 'Default' with DPI scaling applied to form 'SplashScreenForm'.
[DEBUG] [SplashScreenForm.ApplyTheme] Theme applied.
[15:01:03.305] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
2025-11-08 15:01:03 - [15:01:03.305] [LOW   ] 🖱️ UI ACTION: SPLASH_FORM_INITIALIZATION on SplashScreenForm
[15:01:03.306] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (107ms)
2025-11-08 15:01:03 - [15:01:03.306] [MEDIUM] ⬅️ EXITING SplashScreenForm.SplashScreenForm (107ms)
[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.
2025-11-08 15:01:03 - [Splash] Starting startup sequence
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 0, Status: Starting startup sequence...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 5, Status: Initializing logging...
[DEBUG] Starting logging initialization...
[DEBUG] Server: localhost, User: JOHNK
[DEBUG] Log directory: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK
[DEBUG] Normal log file: C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs\JOHNK\JOHNK 11-08-2025 @ 3-01 PM_normal.log
2025-11-08 15:01:03 - Initializing logging...
[DEBUG] Logging initialization completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 10, Status: Logging initialized.
2025-11-08 15:01:03 - [Splash] Logging system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 15, Status: Cleaning up old logs...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 20, Status: Old logs cleaned up.
2025-11-08 15:01:03 - [Splash] Log cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 25, Status: Wiping app data folders...
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 30, Status: App data folders wiped.
2025-11-08 15:01:03 - [Splash] App data cleanup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 35, Status: Verifying database connectivity...
2025-11-08 15:01:03 - [Splash] Starting async database connectivity verification
2025-11-08 15:01:03 - [Splash] Database connectivity verified. MySQL version: 5.7.24
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 40, Status: Database connectivity verified.
2025-11-08 15:01:03 - [Splash] Database connectivity verified during startup
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 45, Status: Setting up Data Tables...
[15:01:03.755] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-08 15:01:03 - [15:01:03.755] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_All
2025-11-08 15:01:03 - [15:01:03.755] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_part_ids_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638982108637557376"
}
[15:01:03.758] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:03 - [15:01:03.758] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:03.760] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
2025-11-08 15:01:03 - [15:01:03.760] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_All
[15:01:03.823] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (67ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.823] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_All (67ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.823] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_part_ids_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 67,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3745 part(s)"
  },
  "ResultData": "DataTable[3745 rows]",
  "ErrorMessage": "Retrieved 3745 part(s)"
}
[15:01:03.827] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (67ms) - 3745 rows
2025-11-08 15:01:03 - [15:01:03.827] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_All (67ms) - 3745 rows
[15:01:03.831] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (72ms)
2025-11-08 15:01:03 - [15:01:03.831] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (72ms)
[15:01:03.833] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (78ms)
2025-11-08 15:01:03 - [15:01:03.833] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_All (78ms)
2025-11-08 15:01:03 - [15:01:03.833] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_part_ids_Get_All",
  "ElapsedMs": 78,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_All:638982108637557376",
  "Status": "SUCCESS",
  "RowCount": 3745
}
2025-11-08 15:01:03 - [DataTable] ComboBoxPart: Schema mismatch detected
2025-11-08 15:01:03 - [DataTable] ComboBoxPart: Source schema: ID(Int32), PartID(String), Customer(String), Description(String), IssuedBy(String), ItemType(String), Operations(String)
2025-11-08 15:01:03 - [DataTable] ComboBoxPart: Target schema:
2025-11-08 15:01:03 - [DataTable] ComboBoxPart: Replacing target table with source copy instead of merging
[15:01:03.863] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-08 15:01:03 - [15:01:03.863] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_operation_numbers_Get_All
2025-11-08 15:01:03 - [15:01:03.863] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_operation_numbers_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638982108638632201"
}
[15:01:03.867] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:03 - [15:01:03.867] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:03.870] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
2025-11-08 15:01:03 - [15:01:03.870] [MEDIUM] 🗄️ DB PROCEDURE START: md_operation_numbers_Get_All
[15:01:03.904] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (40ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.904] [HIGH  ] ✅ PROCEDURE md_operation_numbers_Get_All (40ms) - Status: 1
2025-11-08 15:01:03 - [15:01:03.904] [DATA  ] {
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
[15:01:03.909] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (40ms) - 72 rows
2025-11-08 15:01:03 - [15:01:03.909] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_operation_numbers_Get_All (40ms) - 72 rows
[15:01:03.912] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (44ms)
2025-11-08 15:01:03 - [15:01:03.912] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (44ms)
[15:01:03.914] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (51ms)
2025-11-08 15:01:03 - [15:01:03.914] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_operation_numbers_Get_All (51ms)
2025-11-08 15:01:03 - [15:01:03.914] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_operation_numbers_Get_All",
  "ElapsedMs": 51,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_operation_numbers_Get_All:638982108638632201",
  "Status": "SUCCESS",
  "RowCount": 72
}
2025-11-08 15:01:03 - [DataTable] ComboBoxOperation: Schema mismatch detected
2025-11-08 15:01:03 - [DataTable] ComboBoxOperation: Source schema: ID(Int32), Operation(String), IssuedBy(String)
2025-11-08 15:01:03 - [DataTable] ComboBoxOperation: Target schema:
2025-11-08 15:01:03 - [DataTable] ComboBoxOperation: Replacing target table with source copy instead of merging
[15:01:03.927] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-08 15:01:03 - [15:01:03.927] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_locations_Get_All
2025-11-08 15:01:03 - [15:01:03.927] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_locations_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638982108639270114"
}
[15:01:03.930] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:03 - [15:01:03.930] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:03.932] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
2025-11-08 15:01:03 - [15:01:03.932] [MEDIUM] 🗄️ DB PROCEDURE START: md_locations_Get_All
[15:01:04.013] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (86ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.013] [HIGH  ] ✅ PROCEDURE md_locations_Get_All (86ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.013] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_locations_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 86,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 10371 location(s)"
  },
  "ResultData": "DataTable[10371 rows]",
  "ErrorMessage": "Retrieved 10371 location(s)"
}
[15:01:04.018] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (86ms) - 10371 rows
2025-11-08 15:01:04 - [15:01:04.018] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_locations_Get_All (86ms) - 10371 rows
[15:01:04.020] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (89ms)
2025-11-08 15:01:04 - [15:01:04.020] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (89ms)
[15:01:04.022] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (95ms)
2025-11-08 15:01:04 - [15:01:04.022] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_locations_Get_All (95ms)
2025-11-08 15:01:04 - [15:01:04.022] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_locations_Get_All",
  "ElapsedMs": 95,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_locations_Get_All:638982108639270114",
  "Status": "SUCCESS",
  "RowCount": 10371
}
2025-11-08 15:01:04 - [DataTable] ComboBoxLocation: Schema mismatch detected
2025-11-08 15:01:04 - [DataTable] ComboBoxLocation: Source schema: ID(Int32), Location(String), Building(String), IssuedBy(String)
2025-11-08 15:01:04 - [DataTable] ComboBoxLocation: Target schema:
2025-11-08 15:01:04 - [DataTable] ComboBoxLocation: Replacing target table with source copy instead of merging
[15:01:04.040] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-08 15:01:04 - [15:01:04.040] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
2025-11-08 15:01:04 - [15:01:04.040] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638982108640401403"
}
[15:01:04.043] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.043] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.045] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
2025-11-08 15:01:04 - [15:01:04.045] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[15:01:04.075] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.075] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.075] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 35,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 88 user(s)"
  },
  "ResultData": "DataTable[88 rows]",
  "ErrorMessage": "Retrieved 88 user(s)"
}
[15:01:04.078] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
2025-11-08 15:01:04 - [15:01:04.078] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
[15:01:04.081] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-08 15:01:04 - [15:01:04.081] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[15:01:04.083] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-08 15:01:04 - [15:01:04.083] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (42ms)
2025-11-08 15:01:04 - [15:01:04.083] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_All",
  "ElapsedMs": 42,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_All:638982108640401403",
  "Status": "SUCCESS",
  "RowCount": 88
}
2025-11-08 15:01:04 - [DataTable] ComboBoxUser: Schema mismatch detected
2025-11-08 15:01:04 - [DataTable] ComboBoxUser: Source schema: ID(Int32), User(String), Full Name(String), Shift(String), VitsUser(Boolean), Pin(String), LastShownVersion(String), HideChangeLog(String), Theme_Name(String), Theme_FontSize(Int32), VisualUserName(String), VisualPassword(String), WipServerAddress(String), WIPDatabase(String), WipServerPort(String)
2025-11-08 15:01:04 - [DataTable] ComboBoxUser: Target schema:
2025-11-08 15:01:04 - [DataTable] ComboBoxUser: Replacing target table with source copy instead of merging
[15:01:04.092] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-08 15:01:04 - [15:01:04.092] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_item_types_Get_All
2025-11-08 15:01:04 - [15:01:04.092] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_item_types_Get_All",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638982108640923153"
}
[15:01:04.095] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.095] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.097] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
2025-11-08 15:01:04 - [15:01:04.097] [MEDIUM] 🗄️ DB PROCEDURE START: md_item_types_Get_All
[15:01:04.129] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.129] [HIGH  ] ✅ PROCEDURE md_item_types_Get_All (37ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.129] [DATA  ] {
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
[15:01:04.132] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
2025-11-08 15:01:04 - [15:01:04.132] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_item_types_Get_All (37ms) - 4 rows
[15:01:04.134] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-08 15:01:04 - [15:01:04.134] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[15:01:04.136] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-08 15:01:04 - [15:01:04.136] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_item_types_Get_All (44ms)
2025-11-08 15:01:04 - [15:01:04.136] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_item_types_Get_All",
  "ElapsedMs": 44,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_item_types_Get_All:638982108640923153",
  "Status": "SUCCESS",
  "RowCount": 4
}
2025-11-08 15:01:04 - [DataTable] ComboBoxItemType: Schema mismatch detected
2025-11-08 15:01:04 - [DataTable] ComboBoxItemType: Source schema: ID(Int32), ItemType(String), IssuedBy(String)
2025-11-08 15:01:04 - [DataTable] ComboBoxItemType: Target schema:
2025-11-08 15:01:04 - [DataTable] ComboBoxItemType: Replacing target table with source copy instead of merging
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 50, Status: Data Tables set up.
2025-11-08 15:01:04 - [Splash] Data tables setup completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 60, Status: Initializing version checker...
VersionTimer initialized and started.
2025-11-08 15:01:04 - VersionTimer initialized and started successfully.
Running VersionChecker...
2025-11-08 15:01:04 - Running VersionChecker - checking database version information.
[15:01:04.205] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-08 15:01:04 - [15:01:04.205] [HIGH  ] ⏱️ PERFORMANCE START: SP_log_changelog_Get_Current
2025-11-08 15:01:04 - [15:01:04.205] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_log_changelog_Get_Current",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638982108642056099"
}
[15:01:04.209] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.209] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.211] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
2025-11-08 15:01:04 - [15:01:04.211] [MEDIUM] 🗄️ DB PROCEDURE START: log_changelog_Get_Current
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 65, Status: Version checker initialized.
2025-11-08 15:01:04 - [Splash] Version checker initialized
[15:01:04.244] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (38ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.244] [HIGH  ] ✅ PROCEDURE log_changelog_Get_Current (38ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.244] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "log_changelog_Get_Current",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 38,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved current changelog version"
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved current changelog version"
}
[15:01:04.246] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (38ms) - 1 rows
2025-11-08 15:01:04 - [15:01:04.246] [MEDIUM] ✅ DB PROCEDURE COMPLETE: log_changelog_Get_Current (38ms) - 1 rows
[15:01:04.248] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-08 15:01:04 - [15:01:04.248] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[15:01:04.250] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (45ms)
2025-11-08 15:01:04 - [15:01:04.250] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_log_changelog_Get_Current (45ms)
2025-11-08 15:01:04 - [15:01:04.250] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_log_changelog_Get_Current",
  "ElapsedMs": 45,
  "Key": "ExecuteDataTableWithStatusAsync:SP_log_changelog_Get_Current:638982108642056099",
  "Status": "SUCCESS",
  "RowCount": 1
}
Database version retrieved: 6.0.0.0
2025-11-08 15:01:04 - Version check successful - Database version: 6.0.0.0
Version labels updated - App: 6.0.0.0, DB: 6.0.0.0
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 70, Status: Initializing theme system...
2025-11-08 15:01:04 - Attempting to load themes from database using Dao_System.GetAllThemesAsync...
[15:01:04.279] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 15:01:04 - [15:01:04.279] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_theme_GetAll
2025-11-08 15:01:04 - [15:01:04.279] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982108642797393"
}
[15:01:04.282] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.282] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.284] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
2025-11-08 15:01:04 - [15:01:04.284] [MEDIUM] 🗄️ DB PROCEDURE START: sys_theme_GetAll
[15:01:04.292] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.292] [HIGH  ] ✅ PROCEDURE sys_theme_GetAll (12ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.292] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_theme_GetAll",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 12,
  "Thread": 1,
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 9 theme(s)"
  },
  "ResultData": "DataTable[9 rows]",
  "ErrorMessage": "Retrieved 9 theme(s)"
}
[15:01:04.295] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
2025-11-08 15:01:04 - [15:01:04.295] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_theme_GetAll (12ms) - 9 rows
[15:01:04.297] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-08 15:01:04 - [15:01:04.297] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[15:01:04.299] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (19ms)
2025-11-08 15:01:04 - [15:01:04.299] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_theme_GetAll (19ms)
2025-11-08 15:01:04 - [15:01:04.299] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_theme_GetAll",
  "ElapsedMs": 19,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_theme_GetAll:638982108642797393",
  "Status": "SUCCESS",
  "RowCount": 9
}
2025-11-08 15:01:04 - [Dao_System] Retrieved 9 themes using stored procedure
2025-11-08 15:01:04 - Successfully loaded 9 themes from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Arctic' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Default' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Fire Storm' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Forest' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Lavender' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Midnight' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Ocean' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Sunset' from database
2025-11-08 15:01:04 - ✓ Successfully loaded theme 'Urban Bloom' from database
2025-11-08 15:01:04 - Final theme collection contains: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
2025-11-08 15:01:04 - Theme system initialized with 9 themes available: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom
[15:01:04.344] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-08 15:01:04 - [15:01:04.344] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[15:01:04.346] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 15:01:04 - [15:01:04.346] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[15:01:04.348] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.348] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.348] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982108643481954"
}
[15:01:04.351] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.351] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.353] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.353] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[15:01:04.383] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (35ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.383] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (35ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.383] [DATA  ] {
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
[15:01:04.386] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (35ms) - 1 rows
2025-11-08 15:01:04 - [15:01:04.386] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (35ms) - 1 rows
[15:01:04.389] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
2025-11-08 15:01:04 - [15:01:04.389] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (37ms)
[15:01:04.390] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (42ms)
2025-11-08 15:01:04 - [15:01:04.390] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (42ms)
2025-11-08 15:01:04 - [15:01:04.390] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 42,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982108643481954",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:04.399] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (52ms)
2025-11-08 15:01:04 - [15:01:04.399] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (52ms)
[15:01:04.401] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (57ms)
2025-11-08 15:01:04 - [15:01:04.401] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (57ms)
2025-11-08 15:01:04 - Loaded theme preference for user JOHNK: Forest
2025-11-08 15:01:04 - Set Model_Application_Variables.ThemeName to: Forest
2025-11-08 15:01:04 - Theme system initialized for user JOHNK. Final theme: Forest, Available themes: Arctic, Default, Fire Storm, Forest, Lavender, Midnight, Ocean, Sunset, Urban Bloom, Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 75, Status: Theme system initialized.
2025-11-08 15:01:04 - [Splash] Theme system initialized
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 80, Status: User Full Name loaded: JOHNK
2025-11-08 15:01:04 - [Splash] User context loaded: JOHNK
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 85, Status: Loading theme settings...
2025-11-08 15:01:04 - [Splash] Loading theme settings
[15:01:04.531] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
2025-11-08 15:01:04 - [15:01:04.531] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeFontSizeAsync
[15:01:04.534] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 15:01:04 - [15:01:04.534] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[15:01:04.538] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.538] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.538] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982108645388299"
}
[15:01:04.542] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.542] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.544] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.544] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[15:01:04.548] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.548] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (9ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.548] [DATA  ] {
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
[15:01:04.551] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
2025-11-08 15:01:04 - [15:01:04.551] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (9ms) - 1 rows
[15:01:04.553] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-08 15:01:04 - [15:01:04.553] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[15:01:04.555] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-08 15:01:04 - [15:01:04.555] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (16ms)
2025-11-08 15:01:04 - [15:01:04.555] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 16,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982108645388299",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:04.559] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
2025-11-08 15:01:04 - [15:01:04.559] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (24ms)
[15:01:04.560] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (29ms)
2025-11-08 15:01:04 - [15:01:04.560] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeFontSizeAsync (29ms)
[15:01:04.563] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
2025-11-08 15:01:04 - [15:01:04.563] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[15:01:04.565] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
2025-11-08 15:01:04 - [15:01:04.565] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[15:01:04.567] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.567] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.567] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982108645672373"
}
[15:01:04.569] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:04 - [15:01:04.569] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:04.571] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
2025-11-08 15:01:04 - [15:01:04.571] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[15:01:04.575] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (8ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.575] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (8ms) - Status: 1
2025-11-08 15:01:04 - [15:01:04.575] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_ui_settings_Get",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 8,
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
[15:01:04.578] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (8ms) - 1 rows
2025-11-08 15:01:04 - [15:01:04.578] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (8ms) - 1 rows
[15:01:04.580] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
2025-11-08 15:01:04 - [15:01:04.580] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (10ms)
[15:01:04.582] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (15ms)
2025-11-08 15:01:04 - [15:01:04.582] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (15ms)
2025-11-08 15:01:04 - [15:01:04.582] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_ui_settings_Get",
  "ElapsedMs": 15,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_ui_settings_Get:638982108645672373",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:04.585] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (20ms)
2025-11-08 15:01:04 - [15:01:04.585] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (20ms)
[15:01:04.587] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (23ms)
2025-11-08 15:01:04 - [15:01:04.587] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (23ms)
2025-11-08 15:01:04 - [Splash] Theme settings loaded - Font size: 9
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 90, Status: Theme settings loaded.
2025-11-08 15:01:04 - [Splash] Theme settings loaded
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 93, Status: Startup sequence completed.
2025-11-08 15:01:04 - [Splash] Core startup sequence completed
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 95, Status: Creating main form...
[15:01:04.965] [MEDIUM] ➡️ ENTERING MainForm.MainForm
2025-11-08 15:01:04 - [15:01:04.965] [MEDIUM] ➡️ ENTERING MainForm.MainForm
[DEBUG] [MainForm.ctor] Constructing MainForm...
[15:01:04.968] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-08 15:01:04 - [15:01:04.968] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
2025-11-08 15:01:04 - DPI scaling applied to user control 'Control_ConnectionStrengthControl' and all its controls.
2025-11-08 15:01:04 - Runtime layout adjustments applied to user control 'Control_ConnectionStrengthControl'.
[15:01:04.995] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
2025-11-08 15:01:04 - [15:01:04.995] [MEDIUM] ➡️ ENTERING Control_InventoryTab.Control_InventoryTab
[15:01:04.997] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-08 15:01:04 - [15:01:04.997] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[15:01:05.006] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.006] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_InventoryTab
2025-11-08 15:01:05 - DPI scaling applied to user control 'Control_InventoryTab' and all its controls.
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 15:01:05 - Runtime layout adjustments applied to user control 'Control_InventoryTab'.
[15:01:05.020] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.020] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_InventoryTab
[15:01:05.022] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.022] [LOW   ] 🖱️ UI ACTION: VERSION_TIMER_SETUP on Control_InventoryTab
[15:01:05.024] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.024] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_InventoryTab
[15:01:05.027] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.027] [MEDIUM] ➡️ ENTERING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab
[15:01:05.039] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.039] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on Control_InventoryTab
2025-11-08 15:01:05 - Inventory tab events wired up.
[15:01:05.043] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.043] [LOW   ] 🖱️ UI ACTION: VERSION_LABEL_SET on Control_InventoryTab
[15:01:05.046] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.046] [LOW   ] 🖱️ UI ACTION: UI_STYLING_APPLIED on Control_InventoryTab
2025-11-08 15:01:05 - Inventory Quantity TextBox changed.
[15:01:05.050] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.050] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_InventoryTab
[15:01:05.052] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.052] [LOW   ] 🖱️ UI ACTION: INVENTORY_TAB_INITIALIZATION on Control_InventoryTab
[15:01:05.054] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (58ms)
2025-11-08 15:01:05 - [15:01:05.054] [MEDIUM] ⬅️ EXITING Control_InventoryTab.Control_InventoryTab (58ms)
[15:01:05.056] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
2025-11-08 15:01:05 - [15:01:05.056] [MEDIUM] ➡️ ENTERING Control_AdvancedInventory.Control_AdvancedInventory
[15:01:05.058] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-08 15:01:05 - [15:01:05.058] [LOW   ] 🖱️ UI ACTION: ADVANCED_INVENTORY_INITIALIZATION on Control_AdvancedInventory
2025-11-08 15:01:05 - Control_AdvancedInventory constructor entered.
[15:01:05.070] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-08 15:01:05 - [15:01:05.070] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedInventory
2025-11-08 15:01:05 - DPI scaling applied to user control 'Control_AdvancedInventory' and all its controls.
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel4'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel1'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel2'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel3'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 15:01:05 - Runtime layout adjustments applied to user control 'Control_AdvancedInventory'.
[15:01:05.120] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-08 15:01:05 - [15:01:05.120] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_AdvancedInventory
2025-11-08 15:01:05 - Control_AdvancedInventory constructor exited.
[15:01:05.125] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.125] [MEDIUM] ➡️ ENTERING Control_RemoveTab.Control_RemoveTab
[15:01:05.127] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.127] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[15:01:05.136] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.136] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_RemoveTab
2025-11-08 15:01:05 - DPI scaling applied to user control 'Control_RemoveTab' and all its controls.
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 15:01:05 - Runtime layout adjustments applied to user control 'Control_RemoveTab'.
[15:01:05.156] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.156] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_RemoveTab
[15:01:05.158] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.158] [LOW   ] 🖱️ UI ACTION: COMBOBOX_PROPERTIES_APPLIED on Control_RemoveTab
[15:01:05.159] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.159] [LOW   ] 🖱️ UI ACTION: DATA_LOADING_START on Control_RemoveTab
[15:01:05.169] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.169] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_RemoveTab
[15:01:05.171] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.171] [LOW   ] 🖱️ UI ACTION: TOOLTIPS_SETUP on Control_RemoveTab
[15:01:05.173] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.173] [LOW   ] 🖱️ UI ACTION: PRIVILEGES_APPLIED on Control_RemoveTab
[15:01:05.175] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
2025-11-08 15:01:05 - [15:01:05.175] [LOW   ] 🖱️ UI ACTION: REMOVE_TAB_INITIALIZATION on Control_RemoveTab
[15:01:05.177] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (52ms)
2025-11-08 15:01:05 - [15:01:05.177] [MEDIUM] ⬅️ EXITING Control_RemoveTab.Control_RemoveTab (52ms)
[15:01:05.180] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
2025-11-08 15:01:05 - [15:01:05.180] [MEDIUM] ➡️ ENTERING Control_AdvancedRemove.Control_AdvancedRemove
[15:01:05.182] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
2025-11-08 15:01:05 - [15:01:05.182] [LOW   ] 🖱️ UI ACTION: ADVANCED_REMOVE_INITIALIZATION on Control_AdvancedRemove
[15:01:05.193] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-08 15:01:05 - [15:01:05.193] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_AdvancedRemove
2025-11-08 15:01:05 - DPI scaling applied to user control 'Control_AdvancedRemove' and all its controls.
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 15:01:05 - Runtime layout adjustments applied to user control 'Control_AdvancedRemove'.
[15:01:05.225] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
2025-11-08 15:01:05 - [15:01:05.225] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_AdvancedRemove
[15:01:05.228] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
2025-11-08 15:01:05 - [15:01:05.228] [LOW   ] 🖱️ UI ACTION: BUTTON_EVENTS_SETUP on Control_AdvancedRemove
[15:01:05.233] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
2025-11-08 15:01:05 - [15:01:05.233] [MEDIUM] ➡️ ENTERING Control_TransferTab.Control_TransferTab
[15:01:05.234] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-08 15:01:05 - [15:01:05.234] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-08 15:01:05 - DPI scaling applied to user control 'Control_TransferTab' and all its controls.
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel1'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 15:01:05 - Runtime layout adjustments applied to user control 'Control_TransferTab'.
2025-11-08 15:01:05 - Transfer tab events wired up.
[15:01:05.262] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
2025-11-08 15:01:05 - [15:01:05.262] [LOW   ] 🖱️ UI ACTION: TRANSFER_TAB_INITIALIZATION on Control_TransferTab
[15:01:05.265] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
2025-11-08 15:01:05 - [15:01:05.265] [MEDIUM] ➡️ ENTERING Control_QuickButtons.Control_QuickButtons
[15:01:05.267] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
2025-11-08 15:01:05 - [15:01:05.267] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_INITIALIZATION on Control_QuickButtons
[15:01:05.271] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
2025-11-08 15:01:05 - [15:01:05.271] [LOW   ] 🖱️ UI ACTION: TABLE_LAYOUT_SETUP on Control_QuickButtons
[15:01:05.274] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-08 15:01:05 - [15:01:05.274] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_POST_CONSTRUCTOR on Control_QuickButtons
2025-11-08 15:01:05 - DPI scaling applied to user control 'Control_QuickButtons' and all its controls.
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 15:01:05 - Runtime layout adjustments applied to user control 'Control_QuickButtons'.
2025-11-08 15:01:05 - Inventory Part ComboBox selection changed.
2025-11-08 15:01:05 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[15:01:05.340] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-08 15:01:05 - [15:01:05.340] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on MainForm
2025-11-08 15:01:05 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel4'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel1'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel2'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel3'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel1'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 15:01:05 - Inventory Op ComboBox selection changed.
2025-11-08 15:01:05 - Runtime layout adjustments applied to form 'MainForm'.
[DEBUG] [MainForm.ctor] InitializeComponent complete.
2025-11-08 15:01:05 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
[15:01:05.429] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
2025-11-08 15:01:05 - [15:01:05.429] [MEDIUM] ➡️ ENTERING MainForm.InitializeFormTitle
[15:01:05.432] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
2025-11-08 15:01:05 - [15:01:05.432] [MEDIUM] ➡️ ENTERING MainForm.GetUserPrivilegeDisplayText
[15:01:05.436] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
2025-11-08 15:01:05 - [15:01:05.436] [MEDIUM] 📊 BUSINESS LOGIC: USER_PRIVILEGE_DETERMINATION
[15:01:05.438] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
2025-11-08 15:01:05 - [15:01:05.438] [MEDIUM] ⬅️ EXITING MainForm.GetUserPrivilegeDisplayText (6ms)
[15:01:05.442] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
2025-11-08 15:01:05 - [15:01:05.442] [MEDIUM] 📊 BUSINESS LOGIC: FORM_TITLE_GENERATION
[15:01:05.478] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
2025-11-08 15:01:05 - [15:01:05.478] [LOW   ] 🖱️ UI ACTION: FORM_TITLE_SET on MainForm
[15:01:05.510] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (80ms)
2025-11-08 15:01:05 - [15:01:05.510] [MEDIUM] ⬅️ EXITING MainForm.InitializeFormTitle (80ms)
[DEBUG] [MainForm] UserControl progress helpers initialized.
[DEBUG] [MainForm.ctor] Progress control initialized.
[15:01:05.540] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
2025-11-08 15:01:05 - [15:01:05.540] [LOW   ] 🖱️ UI ACTION: CONNECTION_CHECKER_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthChecker initialized.
[15:01:05.543] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
2025-11-08 15:01:05 - [15:01:05.543] [LOW   ] 🖱️ UI ACTION: CONNECTION_RECOVERY_INIT on MainForm
[DEBUG] [MainForm.ctor] ConnectionRecoveryManager initialized.
[15:01:05.547] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
2025-11-08 15:01:05 - [15:01:05.547] [MEDIUM] ➡️ ENTERING MainForm.InitializeStartupComponents
[15:01:05.549] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
2025-11-08 15:01:05 - [15:01:05.549] [LOW   ] 🖱️ UI ACTION: CONNECTION_STRENGTH_SETUP on MainForm
[DEBUG] [MainForm.ctor] ConnectionStrengthControl setup complete.
[15:01:05.553] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
2025-11-08 15:01:05 - [15:01:05.553] [LOW   ] 🖱️ UI ACTION: EVENTS_WIREUP on MainForm
[DEBUG] [MainForm.ctor] Events wired up.
[15:01:05.585] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-08 15:01:05 - [15:01:05.585] [LOW   ] 🖱️ UI ACTION: DPI_EVENTS_WIREUP on MainForm
2025-11-08 15:01:05 - [PERF] Update_ButtonStates called from: <Control_TransferTab_OnStartup_WireUpEvents>g__ValidateAndUpdate|24_1
2025-11-08 15:01:05 - DPI change event handlers wired up successfully
[DEBUG] [MainForm.ctor] DPI change events wired up.
[15:01:05.679] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-08 15:01:05 - [15:01:05.679] [LOW   ] 🖱️ UI ACTION: STARTUP_COMPONENTS on MainForm
2025-11-08 15:01:05 - Transfer tab ComboBoxes loaded.
[15:01:05.712] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (165ms)
2025-11-08 15:01:05 - [15:01:05.712] [MEDIUM] ⬅️ EXITING MainForm.InitializeStartupComponents (165ms)
[DEBUG] [MainForm.ctor] Startup components initialized.
[15:01:05.716] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[15:01:05.717] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 15:01:05 - [15:01:05.717] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 15:01:05 - [15:01:05.716] [LOW   ] 🖱️ UI ACTION: FORM_INITIALIZATION on MainForm
[15:01:05.719] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[DEBUG] [MainForm.ctor] MainForm constructed.
2025-11-08 15:01:05 - [15:01:05.719] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[15:01:05.722] [MEDIUM] ⬅️ EXITING MainForm.MainForm (757ms)
2025-11-08 15:01:05 - [15:01:05.719] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982108657198356"
}
2025-11-08 15:01:05 - [15:01:05.722] [MEDIUM] ⬅️ EXITING MainForm.MainForm (757ms)
2025-11-08 15:01:05 - [Splash] MainForm created
[15:01:05.727] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 97, Status: Configuring form instances...
2025-11-08 15:01:05 - [15:01:05.727] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:05.731] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 15:01:05 - [15:01:05.731] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 15:01:05 - Remove tab ComboBoxes loaded.
2025-11-08 15:01:05 - Removal tab events wired up.
2025-11-08 15:01:05 - Initial setup of ComboBoxes in the Remove Tab.
[15:01:05.739] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 15:01:05 - [15:01:05.739] [MEDIUM]   ➡️ ENTERING Dao_User.GetUserFullNameAsync
[15:01:05.740] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 15:01:05 - [15:01:05.740] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 15:01:05 - [15:01:05.740] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982108657408287"
}
[15:01:05.743] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:05 - [15:01:05.743] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:05.745] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 15:01:05 - [15:01:05.745] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[15:01:05.752] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.752] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:05 - Inventory Op ComboBox selection changed.
[15:01:05.756] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:05 - [15:01:05.756] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:05.779] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (59ms) - Status: 1
2025-11-08 15:01:05 - [15:01:05.779] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (59ms) - Status: 1
2025-11-08 15:01:05 - [15:01:05.779] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 59,
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
[15:01:05.784] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (59ms) - 1 rows
2025-11-08 15:01:05 - [15:01:05.784] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (59ms) - 1 rows
[15:01:05.786] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (59ms)
2025-11-08 15:01:05 - [15:01:05.786] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (59ms)
[15:01:05.788] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (69ms)
2025-11-08 15:01:05 - [15:01:05.788] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (69ms)
2025-11-08 15:01:05 - [15:01:05.788] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 69,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982108657198356",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:05.793] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (76ms)
2025-11-08 15:01:05 - [15:01:05.793] [MEDIUM]   ⬅️ EXITING Dao_User.GetUserFullNameAsync (76ms)
2025-11-08 15:01:05 - User full name loaded: John Koll
[15:01:05.822] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:05 - [15:01:05.822] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:05 - Inventory Location ComboBox selection changed.
[15:01:05.825] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:05 - [15:01:05.825] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:05 - [Splash] All form instances configured successfully
2025-11-08 15:01:05 - [Splash] Form instances configured
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 99, Status: Applying theme...
[15:01:05.833] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (92ms) - Status: 1
2025-11-08 15:01:05 - [15:01:05.833] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (92ms) - Status: 1
2025-11-08 15:01:05 - [15:01:05.833] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 92,
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
[15:01:05.836] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (92ms) - 1 rows
2025-11-08 15:01:05 - [15:01:05.836] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (92ms) - 1 rows
[15:01:05.839] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (95ms)
2025-11-08 15:01:05 - [15:01:05.839] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (95ms)
[15:01:05.841] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (100ms)
2025-11-08 15:01:05 - [15:01:05.841] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (100ms)
2025-11-08 15:01:05 - [15:01:05.841] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 100,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982108657408287",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:05.844] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (105ms)
2025-11-08 15:01:05 - [15:01:05.844] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (105ms)
2025-11-08 15:01:05 - User full name loaded: John Koll
2025-11-08 15:01:05 - DPI scaling applied to form 'MainForm' and all its controls.
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'MainForm_TableLayout'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'MainForm_TabPage_Inventory'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_InventoryTab_GroupBox_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_MiddleGroup'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_TopGroup'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_InventoryTab_TableLayout_BottomGroup'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_GroupBox_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Single'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayout_Single'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_LowerRight'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_Single_GroupBox_Left'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Single_TableLayout_Left'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel4'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_MultiLoc'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_TableLayoutPanel_Multi'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Preview'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Right'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_BottomRight'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel1'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'AdvancedInventory_MultiLoc_GroupBox_Item'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel2'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Multi_TableLayout_Left'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel3'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_TabControl_Import'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'AdvancedInventory_Import_Panel_Middle'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'AdvancedInventory_Import_TableLayout_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'MainForm_TabPage_Remove'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_RemoveTab_GroupBox_MainControl'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_Panel_Main'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_DataGridView'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_RemoveTab_Panel_Header'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Top'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_RemoveTab_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_AdvancedRemove_GroupBox_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Row4'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomRight'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_BottomLeft'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_TopLeft'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_DateRange'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_AdvancedRemove_TableLayout_Quantity'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_AdvancedRemove_Panel_Row4_Center'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'MainForm_TabPage_Transfer'
2025-11-08 15:01:05 - Applied GroupBox layout adjustments to 'Control_TransferTab_GroupBox_MainControl'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_TransferTab_Panel_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Database_TableLayout_Top'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_Shortcuts_TableLayout_Bottom'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'panel1'
2025-11-08 15:01:05 - Applied Panel layout adjustments to 'Control_TransferTab_Panel_DataGridView'
2025-11-08 15:01:05 - Applied Panel layout adjustments to ''
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'Control_QuickButtons_TableLayoutPanel_Main'
2025-11-08 15:01:05 - Applied TableLayoutPanel layout adjustments to 'tableLayoutPanel1'
2025-11-08 15:01:05 - Runtime layout adjustments applied to form 'MainForm'.
2025-11-08 15:01:05 - Global theme 'Forest' with DPI scaling applied to form 'MainForm'.
2025-11-08 15:01:05 - [Splash] Theme applied to MainForm
2025-11-08 15:01:05 - [Splash] Theme applied to MainForm
[DEBUG] [SplashScreenForm.UpdateProgress] Progress: 100, Status: Ready to start!
2025-11-08 15:01:05 - Inventory tab ComboBoxes loaded.
[15:01:05.972] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (944ms)
2025-11-08 15:01:05 - [15:01:05.972] [MEDIUM] ⬅️ EXITING Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync.Control_InventoryTab (944ms)
[15:01:07.032] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-08 15:01:07 - [15:01:07.032] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_EVENT on Control_QuickButtons
2025-11-08 15:01:07 - [Splash] MainForm displayed successfully
2025-11-08 15:01:07 - [Splash] MainForm displayed - startup complete
2025-11-08 15:01:07 - [Splash] Splash screen closed
[DEBUG] [MainForm.ctor] MainForm Shown event triggered.
[15:01:07.070] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
2025-11-08 15:01:07 - [15:01:07.070] [MEDIUM] ➡️ ENTERING Dao_User.GetUserFullNameAsync
[15:01:07.071] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.071] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.071] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982108670719515"
}
[15:01:07.075] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:07 - [15:01:07.075] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:07.076] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.076] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
[15:01:07.082] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:07 - [15:01:07.082] [HIGH  ] ✅ PROCEDURE usr_users_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:07 - [15:01:07.082] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "usr_users_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 10,
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
[15:01:07.085] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (10ms) - 1 rows
2025-11-08 15:01:07 - [15:01:07.085] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (10ms) - 1 rows
[15:01:07.087] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-08 15:01:07 - [15:01:07.087] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[15:01:07.089] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (17ms)
2025-11-08 15:01:07 - [15:01:07.089] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (17ms)
2025-11-08 15:01:07 - [15:01:07.089] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_usr_users_Get_ByUser",
  "ElapsedMs": 17,
  "Key": "ExecuteDataTableWithStatusAsync:SP_usr_users_Get_ByUser:638982108670719515",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:07.092] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (22ms)
2025-11-08 15:01:07 - [15:01:07.092] [MEDIUM] ⬅️ EXITING Dao_User.GetUserFullNameAsync (22ms)
[DEBUG] [MainForm.ctor] User full name loaded.
[15:01:07.096] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
2025-11-08 15:01:07 - [15:01:07.096] [MEDIUM] ➡️ ENTERING MainForm.ConfigureDevelopmentMenuVisibility
[15:01:07.100] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
2025-11-08 15:01:07 - [15:01:07.100] [MEDIUM] 📊 BUSINESS LOGIC: DEVELOPMENT_MENU_VISIBILITY
[15:01:07.102] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-08 15:01:07 - [15:01:07.102] [LOW   ] 🖱️ UI ACTION: DEVELOPMENT_MENU_CONFIGURED on MainForm
2025-11-08 15:01:07 - Application Info - Development Menu configured for user 'JOHNK': Visible
[15:01:07.106] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
2025-11-08 15:01:07 - [15:01:07.106] [MEDIUM] ⬅️ EXITING MainForm.ConfigureDevelopmentMenuVisibility (9ms)
[15:01:07.149] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-08 15:01:07 - [15:01:07.149] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_START on Control_QuickButtons
2025-11-08 15:01:07 -
2025-11-08 15:01:07 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:07 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-08 15:01:07 - [QuickButtons]   User: JOHNK
2025-11-08 15:01:07 - [QuickButtons] ════════════════════════════════════════════════════════════
[15:01:07.158] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:07 - [15:01:07.158] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:07 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-08 15:01:07 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[15:01:07.164] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.164] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.164] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108671645561"
}
[15:01:07.167] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:07 - [15:01:07.167] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:07.170] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.170] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:07.199] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (34ms) - Status: 1
2025-11-08 15:01:07 - [15:01:07.199] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (34ms) - Status: 1
2025-11-08 15:01:07 - [15:01:07.199] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 34,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:07.202] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (34ms) - 3 rows
2025-11-08 15:01:07 - [15:01:07.202] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (34ms) - 3 rows
[15:01:07.204] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (36ms)
2025-11-08 15:01:07 - [15:01:07.204] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (36ms)
[15:01:07.206] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (41ms)
2025-11-08 15:01:07 - [15:01:07.206] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (41ms)
2025-11-08 15:01:07 - [15:01:07.206] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 41,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108671645561",
  "Status": "SUCCESS",
  "RowCount": 3
}
2025-11-08 15:01:07 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 15:01:07 - [Dao_QuickButtons] Added to array: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:07 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:07 - [Dao_QuickButtons] Skipping duplicate: 029-22039-002 + 106
2025-11-08 15:01:07 - [Dao_QuickButtons] Array restructured: 2 unique buttons, 1 duplicates removed
2025-11-08 15:01:07 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 15:01:07 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 15:01:07 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 15:01:07 - [Dao_QuickButtons] Created button at position 1: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:07 - [Dao_QuickButtons] Created button at position 2: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:07 - [Dao_QuickButtons] Created 2 buttons in database
2025-11-08 15:01:07 - [Dao_QuickButtons] Cleanup complete: 1 duplicates removed, 2 buttons remain
2025-11-08 15:01:07 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 15:01:07 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 1 duplicates removed, 2 buttons remain
2025-11-08 15:01:07 - [QuickButtons] STEP 2: Loading data from database
[15:01:07.310] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.310] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.310] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108673101721"
}
[15:01:07.313] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:07 - [15:01:07.313] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:07.315] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:07 - [15:01:07.315] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:07.319] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (9ms) - Status: 1
2025-11-08 15:01:07 - [15:01:07.319] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (9ms) - Status: 1
2025-11-08 15:01:07 - [15:01:07.319] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 9,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:07.322] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (9ms) - 3 rows
2025-11-08 15:01:07 - [15:01:07.322] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (9ms) - 3 rows
[15:01:07.324] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
2025-11-08 15:01:07 - [15:01:07.324] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (11ms)
[15:01:07.327] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (17ms)
2025-11-08 15:01:07 - [15:01:07.327] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (17ms)
2025-11-08 15:01:07 - [15:01:07.327] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 17,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108673101721",
  "Status": "SUCCESS",
  "RowCount": 3
}
[15:01:07.333] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:07 - [15:01:07.333] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:07 - [QuickButtons] STEP 2: ✓ Retrieved 3 button(s) from database
2025-11-08 15:01:07 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-08 15:01:07 - [QuickButtons] STEP 3:   Button 1: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:07 - [QuickButtons] STEP 3:   Button 2: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:07 - [QuickButtons] STEP 3:   Button 3: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:07 - [QuickButtons] STEP 3: Filled 3 button(s) with data
2025-11-08 15:01:07 - [QuickButtons] STEP 3: Clearing remaining 7 button(s)
2025-11-08 15:01:07 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-08 15:01:07 - [QuickButtons] STEP 4: Layout refreshed - 3 visible button(s)
2025-11-08 15:01:07 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-08 15:01:07 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-08 15:01:07 - [QuickButtons] ║ User: JOHNK
2025-11-08 15:01:07 - [QuickButtons] ║ Visible Buttons: 3
2025-11-08 15:01:07 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-08 15:01:07 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:07 -
[15:01:07.376] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (216ms)
2025-11-08 15:01:07 - [15:01:07.376] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (216ms)
[15:01:07.378] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
2025-11-08 15:01:07 - [15:01:07.378] [LOW   ] 🖱️ UI ACTION: QUICK_BUTTONS_LOAD_COMPLETE on Control_QuickButtons
[DEBUG] [MainForm.ctor] MainForm is now idle and ready.
[15:01:09.089] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:09 - [15:01:09.089] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:09 - Inventory Part ComboBox selection changed.
[15:01:09.093] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:09 - [15:01:09.093] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:09.096] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:09 - [15:01:09.096] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:09 - Inventory Op ComboBox selection changed.
[15:01:09.100] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:09 - [15:01:09.100] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:09 - Inventory Quantity TextBox changed.
[15:01:11.162] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:11 - [15:01:11.162] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:11 - Inventory Part ComboBox selection changed.
[15:01:11.165] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:11 - [15:01:11.165] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:11.168] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:11 - [15:01:11.168] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:11 - Inventory Op ComboBox selection changed.
[15:01:11.171] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:11 - [15:01:11.171] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:11 - Inventory Quantity TextBox changed.
[15:01:15.063] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.063] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:15.066] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:15 - [15:01:15.066] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:15.070] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.070] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - Inventory Location ComboBox selection changed.
[15:01:15.075] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (4ms)
2025-11-08 15:01:15 - [15:01:15.075] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (4ms)
[15:01:15.173] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.173] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:15.175] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:15 - [15:01:15.175] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:15.178] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.178] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - Inventory Location ComboBox selection changed.
[15:01:15.182] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:15 - [15:01:15.182] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:15.207] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.207] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:15.209] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:15 - [15:01:15.209] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:15.212] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.212] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - Inventory Location ComboBox selection changed.
[15:01:15.215] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:15 - [15:01:15.215] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:15.226] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.226] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:15.228] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:15 - [15:01:15.228] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:15.231] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.231] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - Inventory Location ComboBox selection changed.
[15:01:15.234] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:15 - [15:01:15.234] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:15.254] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.254] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:15.256] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:15 - [15:01:15.256] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:15.259] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - [15:01:15.259] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:15 - Inventory Location ComboBox selection changed.
[15:01:15.262] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:15 - [15:01:15.262] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:16.383] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.383] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab
2025-11-08 15:01:16 - Inventory Save button clicked.
[15:01:16.391] [MEDIUM] ➡️ ENTERING AddInventoryItemAsync.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.391] [MEDIUM] ➡️ ENTERING AddInventoryItemAsync.Control_InventoryTab
[15:01:16.394] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_ByItemNumber
2025-11-08 15:01:16 - [15:01:16.394] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_ByItemNumber
2025-11-08 15:01:16 - [15:01:16.394] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_part_ids_Get_ByItemNumber",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_ByItemNumber:638982108763946005"
}
[15:01:16.397] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:16 - [15:01:16.397] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:16.399] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_ByItemNumber
2025-11-08 15:01:16 - [15:01:16.399] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_ByItemNumber
[15:01:16.431] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_ByItemNumber (36ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.431] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_ByItemNumber (36ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.431] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_part_ids_Get_ByItemNumber",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 36,
  "Thread": 1,
  "InputParameters": {
    "p_ItemNumber": "01-33016-000"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved part \"01-33016-000\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved part \"01-33016-000\""
}
[15:01:16.434] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_ByItemNumber (36ms) - 1 rows
2025-11-08 15:01:16 - [15:01:16.434] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_ByItemNumber (36ms) - 1 rows
[15:01:16.436] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
2025-11-08 15:01:16 - [15:01:16.436] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (39ms)
[15:01:16.439] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_ByItemNumber (44ms)
2025-11-08 15:01:16 - [15:01:16.439] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_ByItemNumber (44ms)
2025-11-08 15:01:16 - [15:01:16.439] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_part_ids_Get_ByItemNumber",
  "ElapsedMs": 44,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_ByItemNumber:638982108763946005",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:16.512] [MEDIUM] ⬅️ EXITING AddInventoryItemAsync.Control_InventoryTab (120ms)
2025-11-08 15:01:16 - [15:01:16.512] [MEDIUM] ⬅️ EXITING AddInventoryItemAsync.Control_InventoryTab (120ms)
2025-11-08 15:01:16 - Inventory transaction verified successful: Added inventory item: 01-33016-000 at CS-04, quantity 20
[15:01:16.517] [MEDIUM] ➡️ ENTERING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.517] [MEDIUM] ➡️ ENTERING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab
2025-11-08 15:01:16 - [Dao_QuickButtons] ═══════════════════════════════════════════════════════
2025-11-08 15:01:16 - [Dao_QuickButtons] AddOrShiftQuickButtonAsync STARTED
2025-11-08 15:01:16 - [Dao_QuickButtons]   User: JOHNK
2025-11-08 15:01:16 - [Dao_QuickButtons]   PartID: 01-33016-000
2025-11-08 15:01:16 - [Dao_QuickButtons]   Operation: 109
2025-11-08 15:01:16 - [Dao_QuickButtons]   Quantity: 20
2025-11-08 15:01:16 - [Dao_QuickButtons] ═══════════════════════════════════════════════════════
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 1: Checking for existing duplicate
[15:01:16.528] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.528] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.528] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108765283567"
}
[15:01:16.531] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:16 - [15:01:16.531] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:16.534] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.534] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:16.542] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (14ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.542] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (14ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.542] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 14,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:16.545] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (14ms) - 3 rows
2025-11-08 15:01:16 - [15:01:16.545] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (14ms) - 3 rows
[15:01:16.547] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
2025-11-08 15:01:16 - [15:01:16.547] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (15ms)
[15:01:16.549] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (21ms)
2025-11-08 15:01:16 - [15:01:16.549] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (21ms)
2025-11-08 15:01:16 - [15:01:16.549] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 21,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108765283567",
  "Status": "SUCCESS",
  "RowCount": 3
}
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 1: Retrieved 3 existing buttons
2025-11-08 15:01:16 - [Dao_QuickButtons]   Position 1: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:16 - [Dao_QuickButtons]   Position 2: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:16 - [Dao_QuickButtons]   Position 3: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:16 - [Dao_QuickButtons] ╔════════════════════════════════════════════════════╗
2025-11-08 15:01:16 - [Dao_QuickButtons] ║ DUPLICATE FOUND at position 3
2025-11-08 15:01:16 - [Dao_QuickButtons] ║ PartID '01-33016-000' + Operation '109' already exists
2025-11-08 15:01:16 - [Dao_QuickButtons] ║ ACTION: DO NOTHING - Return success without changes
2025-11-08 15:01:16 - [Dao_QuickButtons] ╚════════════════════════════════════════════════════╝
2025-11-08 15:01:16 - [Dao_QuickButtons] AddOrShiftQuickButtonAsync COMPLETED (duplicate skipped)
2025-11-08 15:01:16 - [Dao_QuickButtons] ═══════════════════════════════════════════════════════
2025-11-08 15:01:16 -
2025-11-08 15:01:16 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:16 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-08 15:01:16 - [QuickButtons]   User: JOHNK
2025-11-08 15:01:16 - [QuickButtons] ════════════════════════════════════════════════════════════
[15:01:16.577] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:16 - [15:01:16.577] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:16 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[15:01:16.582] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.582] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.582] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108765820017"
}
[15:01:16.585] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:16 - [15:01:16.585] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:16.587] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.587] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:16.594] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (12ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.594] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (12ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.594] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 12,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:16.597] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (12ms) - 3 rows
2025-11-08 15:01:16 - [15:01:16.597] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (12ms) - 3 rows
[15:01:16.599] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-08 15:01:16 - [15:01:16.599] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[15:01:16.602] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (20ms)
2025-11-08 15:01:16 - [15:01:16.602] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (20ms)
2025-11-08 15:01:16 - [15:01:16.602] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 20,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108765820017",
  "Status": "SUCCESS",
  "RowCount": 3
}
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 15:01:16 - [Dao_QuickButtons] Added to array: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:16 - [Dao_QuickButtons] Skipping duplicate: 029-22039-002 + 106
2025-11-08 15:01:16 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:16 - [Dao_QuickButtons] Array restructured: 2 unique buttons, 1 duplicates removed
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 15:01:16 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 15:01:16 - [Dao_QuickButtons] Created button at position 1: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:16 - [Dao_QuickButtons] Created button at position 2: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:16 - [Dao_QuickButtons] Created 2 buttons in database
2025-11-08 15:01:16 - [Dao_QuickButtons] Cleanup complete: 1 duplicates removed, 2 buttons remain
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 15:01:16 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 1 duplicates removed, 2 buttons remain
2025-11-08 15:01:16 - [QuickButtons] STEP 2: Loading data from database
[15:01:16.662] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.662] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.662] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108766622549"
}
[15:01:16.665] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:16 - [15:01:16.665] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:16.668] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.668] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:16.674] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.674] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.674] [DATA  ] {
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
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:16.677] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 3 rows
2025-11-08 15:01:16 - [15:01:16.677] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 3 rows
[15:01:16.679] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-08 15:01:16 - [15:01:16.679] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[15:01:16.681] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:16 - [15:01:16.681] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:16 - [15:01:16.681] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108766622549",
  "Status": "SUCCESS",
  "RowCount": 3
}
[15:01:16.684] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:16 - [15:01:16.684] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:16 - [QuickButtons] STEP 2: ✓ Retrieved 3 button(s) from database
2025-11-08 15:01:16 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-08 15:01:16 - [QuickButtons] STEP 3:   Button 1: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:16 - [QuickButtons] STEP 3:   Button 2: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:16 - [QuickButtons] STEP 3:   Button 3: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:16 - [QuickButtons] STEP 3: Filled 3 button(s) with data
2025-11-08 15:01:16 - [QuickButtons] STEP 3: Clearing remaining 7 button(s)
2025-11-08 15:01:16 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-08 15:01:16 - [QuickButtons] STEP 4: Layout refreshed - 3 visible button(s)
2025-11-08 15:01:16 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-08 15:01:16 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-08 15:01:16 - [QuickButtons] ║ User: JOHNK
2025-11-08 15:01:16 - [QuickButtons] ║ Visible Buttons: 3
2025-11-08 15:01:16 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-08 15:01:16 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:16 -
[15:01:16.722] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (145ms)
2025-11-08 15:01:16 - [15:01:16.722] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (145ms)
[15:01:16.725] [MEDIUM] ⬅️ EXITING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab (207ms)
2025-11-08 15:01:16 - [15:01:16.725] [MEDIUM] ⬅️ EXITING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab (207ms)
[15:01:16.731] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.731] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab
[15:01:16.736] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.736] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[15:01:16.743] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.743] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:16 - Inventory Part ComboBox selection changed.
[15:01:16.747] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:16 - [15:01:16.747] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:16.752] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.752] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:16 - Inventory Op ComboBox selection changed.
[15:01:16.755] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:16 - [15:01:16.755] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:16.758] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:16 - [15:01:16.758] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:16 - Inventory Location ComboBox selection changed.
[15:01:16.761] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:16 - [15:01:16.761] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:16 - Inventory Quantity TextBox changed.
[15:01:16.772] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (36ms)
2025-11-08 15:01:16 - [15:01:16.772] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (36ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
[15:01:16.785] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab (53ms)
2025-11-08 15:01:16 - [15:01:16.785] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab (53ms)
2025-11-08 15:01:16 -
2025-11-08 15:01:16 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:16 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-08 15:01:16 - [QuickButtons]   User: JOHNK
2025-11-08 15:01:16 - [QuickButtons] ════════════════════════════════════════════════════════════
[15:01:16.794] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:16 - [15:01:16.794] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:16 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[15:01:16.799] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.799] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.799] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108767991946"
}
[15:01:16.802] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:16 - [15:01:16.802] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:16.805] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.805] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:16.810] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.810] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.810] [DATA  ] {
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
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:16.813] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 3 rows
2025-11-08 15:01:16 - [15:01:16.813] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 3 rows
[15:01:16.815] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 15:01:16 - [15:01:16.815] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[15:01:16.818] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:16 - [15:01:16.818] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:16 - [15:01:16.818] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108767991946",
  "Status": "SUCCESS",
  "RowCount": 3
}
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 15:01:16 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:16 - [Dao_QuickButtons] Added to array: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:16 - [Dao_QuickButtons] Skipping duplicate: 01-33016-000 + 109
2025-11-08 15:01:16 - [Dao_QuickButtons] Array restructured: 2 unique buttons, 1 duplicates removed
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 15:01:16 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 15:01:16 - [Dao_QuickButtons] Created button at position 1: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:16 - [Dao_QuickButtons] Created button at position 2: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:16 - [Dao_QuickButtons] Created 2 buttons in database
2025-11-08 15:01:16 - [Dao_QuickButtons] Cleanup complete: 1 duplicates removed, 2 buttons remain
2025-11-08 15:01:16 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 15:01:16 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 1 duplicates removed, 2 buttons remain
2025-11-08 15:01:16 - [QuickButtons] STEP 2: Loading data from database
[15:01:16.896] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.896] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.896] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108768965252"
}
[15:01:16.902] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:16 - [15:01:16.902] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:16.904] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:16 - [15:01:16.904] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:16.910] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (13ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.910] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (13ms) - Status: 1
2025-11-08 15:01:16 - [15:01:16.910] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 13,
  "Thread": 1,
  "InputParameters": {
    "p_User": "JOHNK"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:16.914] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (13ms) - 3 rows
2025-11-08 15:01:16 - [15:01:16.914] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (13ms) - 3 rows
[15:01:16.916] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-08 15:01:16 - [15:01:16.916] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[15:01:16.919] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (22ms)
2025-11-08 15:01:16 - [15:01:16.919] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (22ms)
2025-11-08 15:01:16 - [15:01:16.919] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 22,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108768965252",
  "Status": "SUCCESS",
  "RowCount": 3
}
[15:01:16.923] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:16 - [15:01:16.923] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:16 - [QuickButtons] STEP 2: ✓ Retrieved 3 button(s) from database
2025-11-08 15:01:16 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-08 15:01:16 - [QuickButtons] STEP 3:   Button 1: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:16 - [QuickButtons] STEP 3:   Button 2: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:16 - [QuickButtons] STEP 3:   Button 3: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:16 - [QuickButtons] STEP 3: Filled 3 button(s) with data
2025-11-08 15:01:16 - [QuickButtons] STEP 3: Clearing remaining 7 button(s)
2025-11-08 15:01:16 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-08 15:01:16 - [QuickButtons] STEP 4: Layout refreshed - 3 visible button(s)
2025-11-08 15:01:16 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-08 15:01:16 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-08 15:01:16 - [QuickButtons] ║ User: JOHNK
2025-11-08 15:01:16 - [QuickButtons] ║ Visible Buttons: 3
2025-11-08 15:01:16 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-08 15:01:16 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:16 -
[15:01:16.966] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (171ms)
2025-11-08 15:01:16 - [15:01:16.966] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (171ms)
2025-11-08 15:01:16 - Inventory Save operation completed successfully.
[15:01:16.975] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab (591ms)
2025-11-08 15:01:16 - [15:01:16.975] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab (591ms)
[15:01:20.113] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.113] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.115] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.115] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.118] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.118] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.122] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:20 - [15:01:20.122] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:20.137] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.137] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.139] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.139] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.142] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.142] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.145] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.145] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.157] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.157] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.159] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.159] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.162] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.162] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.165] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.165] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.480] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.480] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.482] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.482] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.486] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.486] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.491] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (5ms)
2025-11-08 15:01:20 - [15:01:20.491] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (5ms)
[15:01:20.499] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.499] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.501] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:20 - [15:01:20.501] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:20.504] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.504] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.507] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.507] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.511] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.511] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.513] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.513] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.517] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.517] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.520] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.520] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.523] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.523] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.525] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:20 - [15:01:20.525] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:20.528] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.528] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.531] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.531] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.535] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.535] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.537] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.537] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.541] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.541] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.544] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.544] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.837] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.837] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.839] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.839] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.842] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.842] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.845] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.845] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.853] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.853] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.855] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.855] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.858] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.858] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.861] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.861] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.865] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.865] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.867] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:20 - [15:01:20.867] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:20.871] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.871] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.875] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:20 - [15:01:20.875] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:20.879] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.879] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.881] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.881] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.884] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.884] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.887] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.887] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:20.891] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.891] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.894] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.894] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.898] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.898] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.901] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:20 - [15:01:20.901] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:20.905] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.905] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.907] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.907] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.910] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.910] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.914] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:20 - [15:01:20.914] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:20.918] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.918] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:20.920] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:20 - [15:01:20.920] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:20.924] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - [15:01:20.924] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:20 - Inventory Part ComboBox selection changed.
[15:01:20.928] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:20 - [15:01:20.928] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.205] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.205] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.207] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.207] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.210] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.210] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.214] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.214] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.220] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.220] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.222] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.222] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.224] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.224] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.227] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.227] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.232] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.232] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.234] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.234] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.245] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.245] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.249] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.249] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.253] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.253] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.255] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.255] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.258] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.258] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.262] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.262] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.266] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.266] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.268] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.268] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.272] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.272] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.275] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.275] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:21.278] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.278] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.280] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.280] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.284] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.284] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.287] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.287] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.297] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.297] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.299] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.299] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.302] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.302] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.305] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.305] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:21.572] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.572] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.574] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.574] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.578] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.578] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.581] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.581] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.587] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.587] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.589] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.589] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.593] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.593] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.596] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.596] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.600] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.600] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.602] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.602] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.606] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.606] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.609] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.609] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.612] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.612] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.614] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.614] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.617] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.617] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.621] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.621] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:21.624] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.624] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.626] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:21 - [15:01:21.626] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:21.630] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.630] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.634] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.634] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.638] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.638] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.640] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.640] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.644] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.644] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.647] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.647] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.651] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.651] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.653] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.653] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.657] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.657] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.661] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.661] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.928] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.928] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.930] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.930] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.934] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.934] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.937] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.937] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.941] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.941] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.943] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.943] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.947] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.947] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.951] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.951] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.955] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.955] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.957] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.957] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.969] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.969] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.973] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:21 - [15:01:21.973] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:21.978] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.978] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.980] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.980] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:21.984] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.984] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:21 - Inventory Part ComboBox selection changed.
[15:01:21.989] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (4ms)
2025-11-08 15:01:21 - [15:01:21.989] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (4ms)
[15:01:21.994] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:21 - [15:01:21.994] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:21.996] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:21 - [15:01:21.996] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.000] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.000] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Part ComboBox selection changed.
[15:01:22.003] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.003] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:22.007] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.007] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.009] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.009] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.013] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.013] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Part ComboBox selection changed.
[15:01:22.017] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.017] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:22.021] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.021] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.023] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.023] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.026] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.026] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Part ComboBox selection changed.
[15:01:22.029] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.029] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:22.881] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.881] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.883] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.883] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.886] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.886] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Op ComboBox selection changed.
[15:01:22.890] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.890] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:22.900] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.900] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.902] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.902] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.905] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.905] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Op ComboBox selection changed.
[15:01:22.908] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.908] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:22.913] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.913] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.915] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.915] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.918] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.918] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Op ComboBox selection changed.
[15:01:22.921] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.921] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:22.924] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.924] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.926] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:22 - [15:01:22.926] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:22.929] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.929] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Op ComboBox selection changed.
[15:01:22.933] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.933] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:22.938] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.938] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:22.940] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:22 - [15:01:22.940] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:22.943] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - [15:01:22.943] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:22 - Inventory Op ComboBox selection changed.
[15:01:22.946] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:22 - [15:01:22.946] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:23.278] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.278] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:23.280] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:23 - [15:01:23.280] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:23.283] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.283] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - Inventory Op ComboBox selection changed.
[15:01:23.287] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:23 - [15:01:23.287] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:23.292] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.292] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:23.295] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
2025-11-08 15:01:23 - [15:01:23.295] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
[15:01:23.299] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.299] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - Inventory Op ComboBox selection changed.
[15:01:23.302] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:23 - [15:01:23.302] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:23.304] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.304] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:23.306] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:23 - [15:01:23.306] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:23.310] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.310] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - Inventory Op ComboBox selection changed.
[15:01:23.313] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:23 - [15:01:23.313] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:23.316] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.316] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:23.318] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
2025-11-08 15:01:23 - [15:01:23.318] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (1ms)
[15:01:23.321] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.321] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - Inventory Op ComboBox selection changed.
[15:01:23.324] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:23 - [15:01:23.324] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:23.327] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.327] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:23.329] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:23 - [15:01:23.329] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:23.333] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.333] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - Inventory Op ComboBox selection changed.
[15:01:23.336] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:23 - [15:01:23.336] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:23.339] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.339] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:23.341] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:23 - [15:01:23.341] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:23.344] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - [15:01:23.344] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:23 - Inventory Op ComboBox selection changed.
[15:01:23.347] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:23 - [15:01:23.347] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:25.540] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:25 - [15:01:25.540] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:25.542] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:25 - [15:01:25.542] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:25 - Inventory Quantity TextBox changed.
[15:01:25.741] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:25 - [15:01:25.741] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:25.743] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:25 - [15:01:25.743] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:25 - Inventory Quantity TextBox changed.
[15:01:25.915] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:25 - [15:01:25.915] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:25.917] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:25 - [15:01:25.917] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:25 - Inventory Quantity TextBox changed.
[15:01:26.817] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.817] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:26.819] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:26 - [15:01:26.819] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:26.824] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.824] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - Inventory Location ComboBox selection changed.
[15:01:26.830] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (4ms)
2025-11-08 15:01:26 - [15:01:26.830] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (4ms)
[15:01:26.837] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.837] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:26.840] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
2025-11-08 15:01:26 - [15:01:26.840] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (3ms)
[15:01:26.843] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.843] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - Inventory Location ComboBox selection changed.
[15:01:26.846] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:26 - [15:01:26.846] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:26.851] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.851] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:26.853] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:26 - [15:01:26.853] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:26.856] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.856] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - Inventory Location ComboBox selection changed.
[15:01:26.860] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:26 - [15:01:26.860] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:26.864] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.864] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:26.866] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:26 - [15:01:26.866] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:26.869] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.869] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - Inventory Location ComboBox selection changed.
[15:01:26.873] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:26 - [15:01:26.873] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:26.880] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.880] [MEDIUM] ➡️ ENTERING ProcessCmdKey.Control_InventoryTab
[15:01:26.882] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
2025-11-08 15:01:26 - [15:01:26.882] [MEDIUM] ⬅️ EXITING ProcessCmdKey.Control_InventoryTab (2ms)
[15:01:26.885] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - [15:01:26.885] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:26 - Inventory Location ComboBox selection changed.
[15:01:26.889] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:26 - [15:01:26.889] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
[15:01:28.061] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.061] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab
2025-11-08 15:01:28 - Inventory Save button clicked.
[15:01:28.068] [MEDIUM] ➡️ ENTERING AddInventoryItemAsync.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.068] [MEDIUM] ➡️ ENTERING AddInventoryItemAsync.Control_InventoryTab
[15:01:28.070] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_ByItemNumber
2025-11-08 15:01:28 - [15:01:28.070] [HIGH  ] ⏱️ PERFORMANCE START: SP_md_part_ids_Get_ByItemNumber
2025-11-08 15:01:28 - [15:01:28.070] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_md_part_ids_Get_ByItemNumber",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_ByItemNumber:638982108880701117"
}
[15:01:28.074] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.074] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.076] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_ByItemNumber
2025-11-08 15:01:28 - [15:01:28.076] [MEDIUM] 🗄️ DB PROCEDURE START: md_part_ids_Get_ByItemNumber
[15:01:28.084] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_ByItemNumber (13ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.084] [HIGH  ] ✅ PROCEDURE md_part_ids_Get_ByItemNumber (13ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.084] [DATA  ] {
  "Action": "STORED_PROCEDURE_EXECUTION",
  "Procedure": "md_part_ids_Get_ByItemNumber",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Status": 1,
  "ElapsedMs": 13,
  "Thread": 1,
  "InputParameters": {
    "p_ItemNumber": "037352-22"
  },
  "OutputParameters": {
    "Status": "1",
    "ErrorMsg": "Retrieved part \"037352-22\""
  },
  "ResultData": "DataTable[1 rows]",
  "ErrorMessage": "Retrieved part \"037352-22\""
}
[15:01:28.088] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_ByItemNumber (13ms) - 1 rows
2025-11-08 15:01:28 - [15:01:28.088] [MEDIUM] ✅ DB PROCEDURE COMPLETE: md_part_ids_Get_ByItemNumber (13ms) - 1 rows
[15:01:28.090] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
2025-11-08 15:01:28 - [15:01:28.090] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (16ms)
[15:01:28.093] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_ByItemNumber (23ms)
2025-11-08 15:01:28 - [15:01:28.093] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_md_part_ids_Get_ByItemNumber (23ms)
2025-11-08 15:01:28 - [15:01:28.093] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_md_part_ids_Get_ByItemNumber",
  "ElapsedMs": 23,
  "Key": "ExecuteDataTableWithStatusAsync:SP_md_part_ids_Get_ByItemNumber:638982108880701117",
  "Status": "SUCCESS",
  "RowCount": 1
}
[15:01:28.137] [MEDIUM] ⬅️ EXITING AddInventoryItemAsync.Control_InventoryTab (69ms)
2025-11-08 15:01:28 - [15:01:28.137] [MEDIUM] ⬅️ EXITING AddInventoryItemAsync.Control_InventoryTab (69ms)
2025-11-08 15:01:28 - Inventory transaction verified successful: Added inventory item: 037352-22 at CS-04, quantity 30
[15:01:28.142] [MEDIUM] ➡️ ENTERING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.142] [MEDIUM] ➡️ ENTERING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab
2025-11-08 15:01:28 - [Dao_QuickButtons] ═══════════════════════════════════════════════════════
2025-11-08 15:01:28 - [Dao_QuickButtons] AddOrShiftQuickButtonAsync STARTED
2025-11-08 15:01:28 - [Dao_QuickButtons]   User: JOHNK
2025-11-08 15:01:28 - [Dao_QuickButtons]   PartID: 037352-22
2025-11-08 15:01:28 - [Dao_QuickButtons]   Operation: 19
2025-11-08 15:01:28 - [Dao_QuickButtons]   Quantity: 30
2025-11-08 15:01:28 - [Dao_QuickButtons] ═══════════════════════════════════════════════════════
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 1: Checking for existing duplicate
[15:01:28.154] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.154] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.154] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108881543913"
}
[15:01:28.157] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.157] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.160] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.160] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:28.165] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.165] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.165] [DATA  ] {
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
    "ErrorMsg": "Retrieved 3 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[3 rows]",
  "ErrorMessage": "Retrieved 3 transaction(s) for user: JOHNK"
}
[15:01:28.168] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 3 rows
2025-11-08 15:01:28 - [15:01:28.168] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 3 rows
[15:01:28.170] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 15:01:28 - [15:01:28.170] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[15:01:28.172] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:28 - [15:01:28.172] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:28 - [15:01:28.172] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108881543913",
  "Status": "SUCCESS",
  "RowCount": 3
}
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 1: Retrieved 3 existing buttons
2025-11-08 15:01:28 - [Dao_QuickButtons]   Position 1: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons]   Position 2: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons]   Position 3: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 1: No duplicate found - proceeding with add
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 2: Adding new button at position 1
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 2: Calling sys_last_10_transactions_AddQuickButton
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 2: Add succeeded - button inserted at position 1
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 3: Running cleanup to ensure integrity
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[15:01:28.221] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.221] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.221] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108882212923"
}
[15:01:28.224] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.224] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.227] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.227] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:28.231] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.231] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.231] [DATA  ] {
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
    "ErrorMsg": "Retrieved 4 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[4 rows]",
  "ErrorMessage": "Retrieved 4 transaction(s) for user: JOHNK"
}
[15:01:28.234] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 4 rows
2025-11-08 15:01:28 - [15:01:28.234] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 4 rows
[15:01:28.236] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 15:01:28 - [15:01:28.236] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[15:01:28.238] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (17ms)
2025-11-08 15:01:28 - [15:01:28.238] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (17ms)
2025-11-08 15:01:28 - [15:01:28.238] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 17,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108882212923",
  "Status": "SUCCESS",
  "RowCount": 4
}
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 037352-22 + 19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons] Skipping duplicate: 01-33016-000 + 109
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] Array restructured: 3 unique buttons, 1 duplicates removed
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 15:01:28 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 1: 037352-22 + 19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 2: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 3: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created 3 buttons in database
2025-11-08 15:01:28 - [Dao_QuickButtons] Cleanup complete: 1 duplicates removed, 3 buttons remain
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 3: Cleanup completed
2025-11-08 15:01:28 - [Dao_QuickButtons] ╔════════════════════════════════════════════════════╗
2025-11-08 15:01:28 - [Dao_QuickButtons] ║ SUCCESS: New button added at position 1
2025-11-08 15:01:28 - [Dao_QuickButtons] ║ 037352-22 + Op:19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] ╚════════════════════════════════════════════════════╝
2025-11-08 15:01:28 - [Dao_QuickButtons] AddOrShiftQuickButtonAsync COMPLETED (new button added)
2025-11-08 15:01:28 - [Dao_QuickButtons] ═══════════════════════════════════════════════════════
2025-11-08 15:01:28 -
2025-11-08 15:01:28 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:28 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-08 15:01:28 - [QuickButtons]   User: JOHNK
2025-11-08 15:01:28 - [QuickButtons] ════════════════════════════════════════════════════════════
[15:01:28.317] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:28 - [15:01:28.317] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:28 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[15:01:28.321] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.321] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.321] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108883215846"
}
[15:01:28.325] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.325] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.327] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.327] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:28.332] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.332] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.332] [DATA  ] {
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
    "ErrorMsg": "Retrieved 5 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[5 rows]",
  "ErrorMessage": "Retrieved 5 transaction(s) for user: JOHNK"
}
[15:01:28.335] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 5 rows
2025-11-08 15:01:28 - [15:01:28.335] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 5 rows
[15:01:28.338] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 15:01:28 - [15:01:28.338] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[15:01:28.340] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:28 - [15:01:28.340] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:28 - [15:01:28.340] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108883215846",
  "Status": "SUCCESS",
  "RowCount": 5
}
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons] Skipping duplicate: 01-33016-000 + 109
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 037352-22 + 19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] Skipping duplicate: 01-33016-000 + 109
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] Array restructured: 3 unique buttons, 2 duplicates removed
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 15:01:28 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 1: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 2: 037352-22 + 19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 3: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created 3 buttons in database
2025-11-08 15:01:28 - [Dao_QuickButtons] Cleanup complete: 2 duplicates removed, 3 buttons remain
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 15:01:28 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 2 duplicates removed, 3 buttons remain
2025-11-08 15:01:28 - [QuickButtons] STEP 2: Loading data from database
[15:01:28.406] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.406] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.406] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108884061128"
}
[15:01:28.409] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.409] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.411] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.411] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:28.418] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.418] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.418] [DATA  ] {
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
    "ErrorMsg": "Retrieved 4 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[4 rows]",
  "ErrorMessage": "Retrieved 4 transaction(s) for user: JOHNK"
}
[15:01:28.421] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 4 rows
2025-11-08 15:01:28 - [15:01:28.421] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 4 rows
[15:01:28.424] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
2025-11-08 15:01:28 - [15:01:28.424] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (14ms)
[15:01:28.425] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-08 15:01:28 - [15:01:28.425] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-08 15:01:28 - [15:01:28.425] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 19,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108884061128",
  "Status": "SUCCESS",
  "RowCount": 4
}
[15:01:28.430] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:28 - [15:01:28.430] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:28 - [QuickButtons] STEP 2: ✓ Retrieved 4 button(s) from database
2025-11-08 15:01:28 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 1: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 2: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 3: 037352-22 + Op:19 (Qty: 30)
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 4: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:28 - [QuickButtons] STEP 3: Filled 4 button(s) with data
2025-11-08 15:01:28 - [QuickButtons] STEP 3: Clearing remaining 6 button(s)
2025-11-08 15:01:28 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-08 15:01:28 - [QuickButtons] STEP 4: Layout refreshed - 4 visible button(s)
2025-11-08 15:01:28 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-08 15:01:28 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-08 15:01:28 - [QuickButtons] ║ User: JOHNK
2025-11-08 15:01:28 - [QuickButtons] ║ Visible Buttons: 4
2025-11-08 15:01:28 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-08 15:01:28 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:28 -
[15:01:28.472] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (154ms)
2025-11-08 15:01:28 - [15:01:28.472] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (154ms)
[15:01:28.474] [MEDIUM] ⬅️ EXITING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab (332ms)
2025-11-08 15:01:28 - [15:01:28.474] [MEDIUM] ⬅️ EXITING AddToLast10TransactionsIfUniqueAsync.Control_InventoryTab (332ms)
[15:01:28.480] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.480] [MEDIUM] ➡️ ENTERING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab
[15:01:28.484] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.484] [MEDIUM] ➡️ ENTERING Control_InventoryTab_SoftReset.Control_InventoryTab
[DEBUG] Updating status strip for Soft Reset
[DEBUG] Resetting UI fields
[15:01:28.492] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.492] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:28 - Inventory Part ComboBox selection changed.
[15:01:28.495] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:28 - [15:01:28.495] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Part_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:28.500] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.500] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:28 - Inventory Op ComboBox selection changed.
[15:01:28.503] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
2025-11-08 15:01:28 - [15:01:28.503] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged.Control_InventoryTab (2ms)
[15:01:28.506] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:28 - [15:01:28.506] [MEDIUM] ➡️ ENTERING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab
2025-11-08 15:01:28 - Inventory Location ComboBox selection changed.
[15:01:28.509] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:28 - [15:01:28.509] [MEDIUM] ⬅️ EXITING Control_InventoryTab_ComboBox_Location_SelectedIndexChanged.Control_InventoryTab (3ms)
2025-11-08 15:01:28 - Inventory Quantity TextBox changed.
[15:01:28.518] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (33ms)
2025-11-08 15:01:28 - [15:01:28.518] [MEDIUM] ⬅️ EXITING Control_InventoryTab_SoftReset.Control_InventoryTab (33ms)
[DEBUG] InventoryTab SoftReset button re-enabled
[DEBUG] Restoring status strip after reset
[15:01:28.531] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab (50ms)
2025-11-08 15:01:28 - [15:01:28.531] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Reset_Click.Control_InventoryTab (50ms)
2025-11-08 15:01:28 -
2025-11-08 15:01:28 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:28 - [QuickButtons] LoadLast10Transactions STARTED
2025-11-08 15:01:28 - [QuickButtons]   User: JOHNK
2025-11-08 15:01:28 - [QuickButtons] ════════════════════════════════════════════════════════════
[15:01:28.541] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:28 - [15:01:28.541] [MEDIUM] ➡️ ENTERING LoadLast10Transactions.Control_QuickButtons
2025-11-08 15:01:28 - [QuickButtons] STEP 1: Running cleanup before loading
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 1: Pulling current button data for user JOHNK
[15:01:28.545] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.545] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.545] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108885455417"
}
[15:01:28.548] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.548] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.551] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.551] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:28.556] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.556] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (10ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.556] [DATA  ] {
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
    "ErrorMsg": "Retrieved 4 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[4 rows]",
  "ErrorMessage": "Retrieved 4 transaction(s) for user: JOHNK"
}
[15:01:28.559] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 4 rows
2025-11-08 15:01:28 - [15:01:28.559] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (10ms) - 4 rows
[15:01:28.561] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
2025-11-08 15:01:28 - [15:01:28.561] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (12ms)
[15:01:28.563] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:28 - [15:01:28.563] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (18ms)
2025-11-08 15:01:28 - [15:01:28.563] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 18,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108885455417",
  "Status": "SUCCESS",
  "RowCount": 4
}
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 2: Removing duplicates and restructuring array
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons] Skipping duplicate: 01-33016-000 + 109
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 037352-22 + 19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] Added to array: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] Array restructured: 3 unique buttons, 1 duplicates removed
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 3: Deleting ALL buttons from database
2025-11-08 15:01:28 - [Dao_QuickButtons] All buttons deleted from database
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 4: Creating new buttons from array data
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 1: 01-33016-000 + 109 (Qty: 20)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 2: 037352-22 + 19 (Qty: 30)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created button at position 3: 029-22039-002 + 106 (Qty: 10)
2025-11-08 15:01:28 - [Dao_QuickButtons] Created 3 buttons in database
2025-11-08 15:01:28 - [Dao_QuickButtons] Cleanup complete: 1 duplicates removed, 3 buttons remain
2025-11-08 15:01:28 - [Dao_QuickButtons] STEP 5: Array disposed
2025-11-08 15:01:28 - [QuickButtons] STEP 1: ✓ Cleanup completed: Cleanup complete: 1 duplicates removed, 3 buttons remain
2025-11-08 15:01:28 - [QuickButtons] STEP 2: Loading data from database
[15:01:28.628] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.628] [HIGH  ] ⏱️ PERFORMANCE START: SP_sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.628] [DATA  ] {
  "Action": "PERFORMANCE_START",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "Caller": "ExecuteDataTableWithStatusAsync",
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108886288334"
}
[15:01:28.633] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
2025-11-08 15:01:28 - [15:01:28.633] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[15:01:28.635] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
2025-11-08 15:01:28 - [15:01:28.635] [MEDIUM] 🗄️ DB PROCEDURE START: sys_last_10_transactions_Get_ByUser
[15:01:28.640] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.640] [HIGH  ] ✅ PROCEDURE sys_last_10_transactions_Get_ByUser (11ms) - Status: 1
2025-11-08 15:01:28 - [15:01:28.640] [DATA  ] {
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
    "ErrorMsg": "Retrieved 4 transaction(s) for user: JOHNK"
  },
  "ResultData": "DataTable[4 rows]",
  "ErrorMessage": "Retrieved 4 transaction(s) for user: JOHNK"
}
[15:01:28.644] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 4 rows
2025-11-08 15:01:28 - [15:01:28.644] [MEDIUM] ✅ DB PROCEDURE COMPLETE: sys_last_10_transactions_Get_ByUser (11ms) - 4 rows
[15:01:28.646] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
2025-11-08 15:01:28 - [15:01:28.646] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (13ms)
[15:01:28.648] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-08 15:01:28 - [15:01:28.648] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_sys_last_10_transactions_Get_ByUser (19ms)
2025-11-08 15:01:28 - [15:01:28.648] [DATA  ] {
  "Action": "PERFORMANCE_COMPLETE",
  "Operation": "SP_sys_last_10_transactions_Get_ByUser",
  "ElapsedMs": 19,
  "Key": "ExecuteDataTableWithStatusAsync:SP_sys_last_10_transactions_Get_ByUser:638982108886288334",
  "Status": "SUCCESS",
  "RowCount": 4
}
[15:01:28.652] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:28 - [15:01:28.652] [MEDIUM] 📊 BUSINESS LOGIC: QUICK_BUTTONS_DATA_RESULT
2025-11-08 15:01:28 - [QuickButtons] STEP 2: ✓ Retrieved 4 button(s) from database
2025-11-08 15:01:28 - [QuickButtons] STEP 3: Populating UI buttons
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 1: 037352-22 + Op:19 (Qty: 30)
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 2: 01-33016-000 + Op:109 (Qty: 20)
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 3: 037352-22 + Op:19 (Qty: 30)
2025-11-08 15:01:28 - [QuickButtons] STEP 3:   Button 4: 029-22039-002 + Op:106 (Qty: 10)
2025-11-08 15:01:28 - [QuickButtons] STEP 3: Filled 4 button(s) with data
2025-11-08 15:01:28 - [QuickButtons] STEP 3: Clearing remaining 6 button(s)
2025-11-08 15:01:28 - [QuickButtons] STEP 4: Refreshing button layout
2025-11-08 15:01:28 - [QuickButtons] STEP 4: Layout refreshed - 4 visible button(s)
2025-11-08 15:01:28 - [QuickButtons] ╔══════════════════════════════════════════════════════════╗
2025-11-08 15:01:28 - [QuickButtons] ║ LoadLast10Transactions COMPLETED SUCCESSFULLY
2025-11-08 15:01:28 - [QuickButtons] ║ User: JOHNK
2025-11-08 15:01:28 - [QuickButtons] ║ Visible Buttons: 4
2025-11-08 15:01:28 - [QuickButtons] ╚══════════════════════════════════════════════════════════╝
2025-11-08 15:01:28 - [QuickButtons] ════════════════════════════════════════════════════════════
2025-11-08 15:01:28 -
[15:01:28.697] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (155ms)
2025-11-08 15:01:28 - [15:01:28.697] [MEDIUM] ⬅️ EXITING LoadLast10Transactions.Control_QuickButtons (155ms)
2025-11-08 15:01:28 - Inventory Save operation completed successfully.
[15:01:28.704] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab (643ms)
2025-11-08 15:01:28 - [15:01:28.704] [MEDIUM] ⬅️ EXITING Control_InventoryTab_Button_Save_Click_Async.Control_InventoryTab (643ms)
2025-11-08 15:01:32 - [Cleanup] Starting application cleanup
2025-11-08 15:01:32 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-08 15:01:32 - [Cleanup] Memory cleanup completed
2025-11-08 15:01:32 - [Cleanup] Application cleanup completed successfully
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
2025-11-08 15:01:32 - [Startup] Application shutdown completed
2025-11-08 15:01:32 - [Cleanup] Starting application cleanup
2025-11-08 15:01:32 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-08 15:01:32 - [Cleanup] Memory cleanup completed
2025-11-08 15:01:32 - [Cleanup] Application cleanup completed successfully
2025-11-08 15:01:32 - [Cleanup] Starting application cleanup
2025-11-08 15:01:32 - [Cleanup] Control_About temp files cleaned up successfully
2025-11-08 15:01:32 - [Cleanup] Memory cleanup completed
2025-11-08 15:01:32 - [Cleanup] Application cleanup completed successfully
The program '[12756] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
