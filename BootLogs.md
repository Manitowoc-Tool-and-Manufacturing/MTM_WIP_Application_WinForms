[18:25:49.348] [LOW   ] 🖱️ UI ACTION: SETTINGS_MENU_CLICK on MainForm
[18:25:49.351] [MEDIUM] ➡️ ENTERING MainForm.MainForm_MenuStrip_File_Settings_Click
[18:25:49.352] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_OPEN on MainForm
[18:25:49.354] [MEDIUM] ➡️ ENTERING SettingsForm.SettingsForm
[18:25:49.355] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
[18:25:49.359] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on SettingsForm
[18:25:49.363] [LOW   ] 🖱️ UI ACTION: SETTINGS_PANELS_INITIALIZATION on SettingsForm
[18:25:49.364] [LOW   ] 🖱️ UI ACTION: INITIALIZE_CONTROLS on SettingsForm
[18:25:49.377] [MEDIUM] ➡️ ENTERING Dao_User.GetShortcutsJsonAsync
[18:25:49.378] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_GetShortcutsJson
[18:25:49.379] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.380] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_GetShortcutsJson
[18:25:49.387] [MEDIUM] ➡️ ENTERING Dao_User.GetThemeNameAsync
[18:25:49.388] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[18:25:49.389] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[18:25:49.390] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.391] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[18:25:49.436] [MEDIUM] ➡️ ENTERING Control_Add_User.Control_Add_User
[18:25:49.437] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
[18:25:49.442] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Add_User
[18:25:49.455] [LOW   ] 🖱️ UI ACTION: DEFAULT_USER_TYPE_SET on Control_Add_User
[18:25:49.457] [LOW   ] 🖱️ UI ACTION: DEVELOPER_ROLE_WIRED on Control_Add_User
[18:25:49.458] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Add_User
[18:25:49.459] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Add_User
[18:25:49.461] [LOW   ] 🖱️ UI ACTION: VISUAL_ACCESS_EVENT_SETUP on Control_Add_User
[18:25:49.462] [LOW   ] 🖱️ UI ACTION: VIEW_PASSWORDS_EVENT_SETUP on Control_Add_User
[18:25:49.463] [LOW   ] 🖱️ UI ACTION: ADD_USER_CONTROL_INITIALIZATION on Control_Add_User
[18:25:49.464] [MEDIUM] ⬅️ EXITING Control_Add_User.Control_Add_User (28ms)
[18:25:49.468] [MEDIUM] ➡️ ENTERING Control_Edit_User.Control_Edit_User
[18:25:49.469] [LOW   ] 🖱️ UI ACTION: EDIT_USER_INITIALIZATION on Control_Edit_User
[18:25:49.474] [LOW   ] 🖱️ UI ACTION: THEME_APPLICATION on Control_Edit_User
[18:25:49.489] [LOW   ] 🖱️ UI ACTION: KEYPRESS_EVENTS_SETUP on Control_Edit_User
[18:25:49.490] [LOW   ] 🖱️ UI ACTION: PASSWORD_FIELDS_SETUP on Control_Edit_User
[18:25:49.493] [MEDIUM] ➡️ ENTERING Control_Remove_User.Control_Remove_User
[18:25:49.494] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
[18:25:49.497] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_SETUP on Control_Remove_User
[18:25:49.498] [LOW   ] 🖱️ UI ACTION: USERS_DATA_LOADING on Control_Remove_User
[18:25:49.502] [LOW   ] 🖱️ UI ACTION: REMOVE_USER_INITIALIZATION on Control_Remove_User
[18:25:49.503] [MEDIUM] ⬅️ EXITING Control_Remove_User.Control_Remove_User (9ms)
[18:25:49.505] [MEDIUM] ➡️ ENTERING Control_Add_PartID.Control_Add_PartID
[18:25:49.506] [LOW   ] 🖱️ UI ACTION: ADD_PARTID_INITIALIZATION on Control_Add_PartID
[18:25:49.511] [LOW   ] 🖱️ UI ACTION: PART_TYPES_LOADING on Control_Add_PartID
[18:25:49.514] [MEDIUM] ⬅️ EXITING Control_Add_PartID.Control_Add_PartID (8ms)
[18:25:49.516] [MEDIUM] ➡️ ENTERING Control_Edit_PartID.Control_Edit_PartID
[18:25:49.518] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_Edit_PartID
[18:25:49.522] [LOW   ] 🖱️ UI ACTION: EVENT_HANDLERS_BINDING on Control_Edit_PartID
[18:25:49.523] [LOW   ] 🖱️ UI ACTION: LOADING_PART_TYPES on Control_Edit_PartID
[18:25:49.526] [LOW   ] 🖱️ UI ACTION: CONTROL_INITIALIZATION on Control_Edit_PartID
[18:25:49.530] [MEDIUM] ⬅️ EXITING Control_Edit_PartID.Control_Edit_PartID (11ms)
[18:25:49.535] [MEDIUM] ➡️ ENTERING Control_Add_Operation.Control_Add_Operation
[18:25:49.536] [LOW   ] 🖱️ UI ACTION: ADD_OPERATION_INITIALIZATION on Control_Add_Operation
[18:25:49.541] [MEDIUM] ⬅️ EXITING Control_Add_Operation.Control_Add_Operation (6ms)
[18:25:49.560] [LOW   ] 🖱️ UI ACTION: INITIALIZE_FORM on SettingsForm
[18:25:49.563] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_INITIALIZATION on SettingsForm
[18:25:49.564] [MEDIUM] ⬅️ EXITING SettingsForm.SettingsForm (210ms)
[18:25:49.585] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (196ms) - Status: 1
[18:25:49.586] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (196ms) - 1 rows
[18:25:49.587] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (208ms)
[18:25:49.588] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (199ms)
[18:25:49.590] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (202ms)
[18:25:49.591] [MEDIUM] ⬅️ EXITING Dao_User.GetThemeNameAsync (204ms)
[18:25:49.595] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_All
[18:25:49.597] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.598] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_All
[18:25:49.606] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerAddressAsync
[18:25:49.607] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[18:25:49.608] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[18:25:49.609] [MEDIUM]     ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.610] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
[18:25:49.631] [HIGH  ] ✅ PROCEDURE usr_users_Get_All (35ms) - Status: 1
[18:25:49.632] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_All (35ms) - 88 rows
[18:25:49.633] [MEDIUM]     ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (36ms)
[18:25:49.634] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_All (39ms)
[18:25:49.639] [HIGH  ] ✅ PROCEDURE usr_ui_settings_GetShortcutsJson (261ms) - Status: 1
[18:25:49.640] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_GetShortcutsJson (261ms) - 1 rows
[18:25:49.642] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (251ms)
[18:25:49.643] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_GetShortcutsJson (264ms)
[18:25:49.644] [MEDIUM] ⬅️ EXITING Dao_User.GetShortcutsJsonAsync (267ms)
[18:25:49.651] [HIGH  ] ✅ PROCEDURE usr_ui_settings_Get (42ms) - Status: 1
[18:25:49.652] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (42ms) - 1 rows
[18:25:49.654] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (44ms)
[18:25:49.655] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (46ms)
[18:25:49.656] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (49ms)
[18:25:49.658] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerAddressAsync (51ms)
[18:25:49.661] [MEDIUM] ➡️ ENTERING Dao_User.GetWipServerPortAsync
[18:25:49.662] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[18:25:49.663] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[18:25:49.664] [MEDIUM] ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.665] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
MTM_WIP_Application_Winforms Error: 0 : Unable to connect to any of the specified MySQL hosts
[18:25:49.688] [MEDIUM] ➡️ ENTERING Dao_User.GetUserByUsernameAsync
[18:25:49.690] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_users_Get_ByUser
[18:25:49.691] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.693] [MEDIUM] 🗄️ DB PROCEDURE START: usr_users_Get_ByUser
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
MTM_WIP_Application_Winforms Error: 0 : Unable to connect to any of the specified MySQL hosts
[18:25:49.835] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (165ms) - 0 rows
[18:25:49.837] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (146ms)
[18:25:49.839] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (176ms)
[18:25:49.841] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (179ms)
[18:25:49.843] [MEDIUM] ⬅️ EXITING Dao_User.GetWipServerPortAsync (182ms)
[18:25:49.846] [MEDIUM] ➡️ ENTERING Dao_User.GetDatabaseAsync
[18:25:49.847] [MEDIUM] ➡️ ENTERING Dao_User.GetSettingsJsonInternalAsync
[18:25:49.848] [HIGH  ] ⏱️ PERFORMANCE START: SP_usr_ui_settings_Get
[18:25:49.850] [MEDIUM]   ➡️ ENTERING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync
[18:25:49.851] [MEDIUM] 🗄️ DB PROCEDURE START: usr_ui_settings_Get
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
MTM_WIP_Application_Winforms Error: 0 : Unable to connect to any of the specified MySQL hosts
[18:25:49.946] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_users_Get_ByUser (252ms) - 0 rows
[18:25:49.947] [MEDIUM]   ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (97ms)
[18:25:49.948] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_users_Get_ByUser (258ms)
[18:25:49.950] [MEDIUM] ⬅️ EXITING Dao_User.GetUserByUsernameAsync (261ms)
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
Exception thrown: 'MySql.Data.MySqlClient.MySqlException' in System.Private.CoreLib.dll
[18:25:50.050] [MEDIUM] ✅ DB PROCEDURE COMPLETE: usr_ui_settings_Get (201ms) - 0 rows
[18:25:50.051] [MEDIUM] ⬅️ EXITING Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync (387ms)
[18:25:50.053] [HIGH  ] ⏱️ PERFORMANCE COMPLETE: SP_usr_ui_settings_Get (204ms)
[18:25:50.054] [MEDIUM] ⬅️ EXITING Dao_User.GetSettingsJsonInternalAsync (206ms)
[18:25:50.056] [MEDIUM] ⬅️ EXITING Dao_User.GetDatabaseAsync (210ms)
Exception thrown: 'System.ArgumentNullException' in System.Net.Ping.dll
Exception thrown: 'System.ArgumentNullException' in System.Net.Ping.dll
[18:25:56.418] [LOW   ] 🖱️ UI ACTION: SETTINGS_FORM_CANCELED on MainForm
[Trace] [Main] Application exiting Main().
[Trace] [Main] Application exiting Main().
The program '[16744] MTM_WIP_Application_Winforms.exe' has exited with code 0 (0x0).
