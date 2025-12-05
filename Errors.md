--- Raw Text - Entry 1 of 6 ---

2025-12-05 09:19:46,ERROR,Core Microsoft SqlClient Data Provider,Invalid column name 'AUTO_ISSUE_LOC_ID'.,"Type: SqlException
Stack Trace:    at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, SqlCommand command, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.CompleteAsyncExecuteReader(Boolean isInternal, Boolean forDescribeParameterEncryption)
   at Microsoft.Data.SqlClient.SqlCommand.InternalEndExecuteReader(IAsyncResult asyncResult, Boolean isInternal, String endMethod)
   at Microsoft.Data.SqlClient.SqlCommand.EndExecuteReaderInternal(IAsyncResult asyncResult)
   at Microsoft.Data.SqlClient.SqlCommand.EndExecuteReaderAsync(IAsyncResult asyncResult)
   at Microsoft.Data.SqlClient.SqlCommand.<>c.<InternalExecuteReaderAsync>b__201_1(IAsyncResult asyncResult)
   at System.Threading.Tasks.TaskFactory`1.FromAsyncCoreLogic(IAsyncResult iar, Func`2 endFunction, Action`1 endAction, Task`1 promise, Boolean requiresSynchronization)
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Services.Visual.Service_VisualDatabase.GetCoilFlatstockInfoAsync(String partNumber) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Services\Visual\Service_VisualDatabase.cs:line 314"

   ═══ Raw Text - Entry 2 of 6 ═══

2025-12-05 09:24:02,ERROR,Microsoft.Extensions.DependencyInjection.Abstractions,No service for type 'MTM_WIP_Application_Winforms.Services.Analytics.IService_UserShiftLogic' has been registered.,"Type: InvalidOperationException
Stack Trace:    at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
   at MTM_WIP_Application_Winforms.Forms.Maintenance.Form_DatabaseMaintenance.btnUpdateUserNames_Click(Object sender, EventArgs e) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Forms\Maintenance\Form_DatabaseMaintenance.cs:line 430"

   ═══ Raw Text - Entry 3 of 6 ═══

2025-12-05 09:24:03,ERROR,MySql.Data,Parameter 'p_ReportID' not found in the collection.,"Type: ArgumentException
Stack Trace:    at MySql.Data.MySqlClient.MySqlParameterCollection.GetParameterFlexible(String parameterName, Boolean throwOnNotFound)
   at MySql.Data.MySqlClient.StoredProcedure.GetAndFixParameter(String spName, MySqlSchemaRow param, Boolean realAsFloat, MySqlParameter returnParameter)
   at MySql.Data.MySqlClient.StoredProcedure.CheckParametersAsync(String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.Resolve(Boolean preparing)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.FillInternal(DataSet dataset, DataTable[] datatables, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable[] dataTables, Int32 startRecord, Int32 maxRecords, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable dataTable)
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_1.<ExecuteDataTableWithStatusAsync>b__1() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
   at System.Threading.Tasks.Task`1.InnerInvoke()
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_0.<<ExecuteDataTableWithStatusAsync>b__0>d.MoveNext() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteWithRetryAsync[T](Func`1 operation) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 927
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(String connectionString, String procedureName, Dictionary`2 parameters, Helper_StoredProcedureProgress progressHelper, MySqlConnection connection, MySqlTransaction transaction) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 247"

   ═══ Raw Text - Entry 4 of 6 ═══

2025-12-05 09:24:20,ERROR,Microsoft.Extensions.DependencyInjection.Abstractions,No service for type 'MTM_WIP_Application_Winforms.Services.Analytics.IService_UserShiftLogic' has been registered.,"Type: InvalidOperationException
Stack Trace:    at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
   at MTM_WIP_Application_Winforms.Forms.Maintenance.Form_DatabaseMaintenance.btnUpdateUserShifts_Click(Object sender, EventArgs e) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Forms\Maintenance\Form_DatabaseMaintenance.cs:line 374"

   ═══ Raw Text - Entry 5 of 6 ═══

2025-12-05 09:24:20,ERROR,MySql.Data,Parameter 'p_ReportID' not found in the collection.,"Type: ArgumentException
Stack Trace:    at MySql.Data.MySqlClient.StoredProcedure.GetAndFixParameter(String spName, MySqlSchemaRow param, Boolean realAsFloat, MySqlParameter returnParameter)
   at MySql.Data.MySqlClient.StoredProcedure.CheckParametersAsync(String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.Resolve(Boolean preparing)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.FillInternal(DataSet dataset, DataTable[] datatables, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable[] dataTables, Int32 startRecord, Int32 maxRecords, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable dataTable)
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_1.<ExecuteDataTableWithStatusAsync>b__1() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
   at System.Threading.Tasks.Task`1.InnerInvoke()
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_0.<<ExecuteDataTableWithStatusAsync>b__0>d.MoveNext() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteWithRetryAsync[T](Func`1 operation) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 927
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(String connectionString, String procedureName, Dictionary`2 parameters, Helper_StoredProcedureProgress progressHelper, MySqlConnection connection, MySqlTransaction transaction) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 247"

   ═══ Parse Failed - Showing Raw Text ═══

2025-12-05 09:28:05 - Application exiting.


═══ Raw Text - Entry 1 of 6 ═══

2025-12-05 09:19:46,ERROR,Core Microsoft SqlClient Data Provider,Invalid column name 'AUTO_ISSUE_LOC_ID'.,"Type: SqlException
Stack Trace:    at Microsoft.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, SqlCommand command, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady)
   at Microsoft.Data.SqlClient.SqlDataReader.TryConsumeMetaData()
   at Microsoft.Data.SqlClient.SqlDataReader.get_MetaData()
   at Microsoft.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted)
   at Microsoft.Data.SqlClient.SqlCommand.CompleteAsyncExecuteReader(Boolean isInternal, Boolean forDescribeParameterEncryption)
   at Microsoft.Data.SqlClient.SqlCommand.InternalEndExecuteReader(IAsyncResult asyncResult, Boolean isInternal, String endMethod)
   at Microsoft.Data.SqlClient.SqlCommand.EndExecuteReaderInternal(IAsyncResult asyncResult)
   at Microsoft.Data.SqlClient.SqlCommand.EndExecuteReaderAsync(IAsyncResult asyncResult)
   at Microsoft.Data.SqlClient.SqlCommand.<>c.<InternalExecuteReaderAsync>b__201_1(IAsyncResult asyncResult)
   at System.Threading.Tasks.TaskFactory`1.FromAsyncCoreLogic(IAsyncResult iar, Func`2 endFunction, Action`1 endAction, Task`1 promise, Boolean requiresSynchronization)
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Services.Visual.Service_VisualDatabase.GetCoilFlatstockInfoAsync(String partNumber) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Services\Visual\Service_VisualDatabase.cs:line 314"

   ═══ Raw Text - Entry 2 of 6 ═══

2025-12-05 09:24:02,ERROR,Microsoft.Extensions.DependencyInjection.Abstractions,No service for type 'MTM_WIP_Application_Winforms.Services.Analytics.IService_UserShiftLogic' has been registered.,"Type: InvalidOperationException
Stack Trace:    at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
   at MTM_WIP_Application_Winforms.Forms.Maintenance.Form_DatabaseMaintenance.btnUpdateUserNames_Click(Object sender, EventArgs e) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Forms\Maintenance\Form_DatabaseMaintenance.cs:line 430"

   ═══ Raw Text - Entry 3 of 6 ═══

2025-12-05 09:24:03,ERROR,MySql.Data,Parameter 'p_ReportID' not found in the collection.,"Type: ArgumentException
Stack Trace:    at MySql.Data.MySqlClient.MySqlParameterCollection.GetParameterFlexible(String parameterName, Boolean throwOnNotFound)
   at MySql.Data.MySqlClient.StoredProcedure.GetAndFixParameter(String spName, MySqlSchemaRow param, Boolean realAsFloat, MySqlParameter returnParameter)
   at MySql.Data.MySqlClient.StoredProcedure.CheckParametersAsync(String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.Resolve(Boolean preparing)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.FillInternal(DataSet dataset, DataTable[] datatables, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable[] dataTables, Int32 startRecord, Int32 maxRecords, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable dataTable)
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_1.<ExecuteDataTableWithStatusAsync>b__1() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
   at System.Threading.Tasks.Task`1.InnerInvoke()
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_0.<<ExecuteDataTableWithStatusAsync>b__0>d.MoveNext() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteWithRetryAsync[T](Func`1 operation) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 927
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(String connectionString, String procedureName, Dictionary`2 parameters, Helper_StoredProcedureProgress progressHelper, MySqlConnection connection, MySqlTransaction transaction) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 247"

   ═══ Raw Text - Entry 4 of 6 ═══

2025-12-05 09:24:20,ERROR,Microsoft.Extensions.DependencyInjection.Abstractions,No service for type 'MTM_WIP_Application_Winforms.Services.Analytics.IService_UserShiftLogic' has been registered.,"Type: InvalidOperationException
Stack Trace:    at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
   at MTM_WIP_Application_Winforms.Forms.Maintenance.Form_DatabaseMaintenance.btnUpdateUserShifts_Click(Object sender, EventArgs e) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Forms\Maintenance\Form_DatabaseMaintenance.cs:line 374"

   ═══ Raw Text - Entry 5 of 6 ═══

2025-12-05 09:24:20,ERROR,MySql.Data,Parameter 'p_ReportID' not found in the collection.,"Type: ArgumentException
Stack Trace:    at MySql.Data.MySqlClient.StoredProcedure.GetAndFixParameter(String spName, MySqlSchemaRow param, Boolean realAsFloat, MySqlParameter returnParameter)
   at MySql.Data.MySqlClient.StoredProcedure.CheckParametersAsync(String spName, Boolean execAsync)
   at MySql.Data.MySqlClient.StoredProcedure.Resolve(Boolean preparing)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReaderAsync(CommandBehavior behavior, Boolean execAsync, CancellationToken cancellationToken)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteReader(CommandBehavior behavior)
   at MySql.Data.MySqlClient.MySqlCommand.ExecuteDbDataReader(CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.FillInternal(DataSet dataset, DataTable[] datatables, Int32 startRecord, Int32 maxRecords, String srcTable, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable[] dataTables, Int32 startRecord, Int32 maxRecords, IDbCommand command, CommandBehavior behavior)
   at System.Data.Common.DbDataAdapter.Fill(DataTable dataTable)
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_1.<ExecuteDataTableWithStatusAsync>b__1() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
   at System.Threading.Tasks.Task`1.InnerInvoke()
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
--- End of stack trace from previous location ---
   at System.Threading.ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.<>c__DisplayClass4_0.<<ExecuteDataTableWithStatusAsync>b__0>d.MoveNext() in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 287
--- End of stack trace from previous location ---
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteWithRetryAsync[T](Func`1 operation) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 927
   at MTM_WIP_Application_Winforms.Helpers.Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(String connectionString, String procedureName, Dictionary`2 parameters, Helper_StoredProcedureProgress progressHelper, MySqlConnection connection, MySqlTransaction transaction) in C:\Users\jkoll\source\repos\MTM_WIP_Application_WinForms\Helpers\Helper_Database_StoredProcedure.cs:line 247"

   ═══ Parse Failed - Showing Raw Text ═══

2025-12-05 09:28:05 - Application exiting.

═══ Raw Text - Entry 1 of 14 ═══

2025-12-05 09:28:11,INFO,Application,Initializing logging...,
═══ Raw Text - Entry 2 of 14 ═══

2025-12-05 09:28:12,INFO,Application,[Startup] Loaded user database name setting: mtm_wip_application_winforms,
═══ Raw Text - Entry 3 of 14 ═══

2025-12-05 09:28:12,INFO,Application,[Startup] Querying INFORMATION_SCHEMA.PARAMETERS for stored procedure metadata,
═══ Raw Text - Entry 4 of 14 ═══

2025-12-05 09:28:12,INFO,Application,"[Startup] Parameter cache populated: 131 procedures, 570 total parameters",
═══ Parse Failed - Showing Raw Text ═══

stored procedures.,
═══ Raw Text - Entry 6 of 14 ═══

2025-12-05 09:28:14,INFO,Application,[Control_InventoryTab] Icon toggle 'Control_InventoryTab_Button_Toggle_RightPanel' expected 32x32 but was 64x32.,
═══ Raw Text - Entry 7 of 14 ═══

2025-12-05 09:28:14,INFO,Application,[Control_AdvancedInventory] Icon toggle 'AdvancedInventory_Import_Button_QuickButtonToggle' expected 32x32 but was 64x32.,
═══ Raw Text - Entry 8 of 14 ═══

2025-12-05 09:28:14,INFO,Application,[Control_RemoveTab] Icon toggle 'Control_RemoveTab_Button_Toggle_RightPanel' expected 32x32 but was 64x32.,
═══ Raw Text - Entry 9 of 14 ═══

2025-12-05 09:28:15,INFO,Application,[Control_AdvancedRemove] Icon toggle 'Control_AdvancedRemove_Button_SidePanel' expected 32x32 but was 64x32.,
═══ Raw Text - Entry 10 of 14 ═══

2025-12-05 09:28:15,INFO,Application,[Control_AdvancedRemove] Icon toggle 'Control_AdvancedRemove_Button_QuickButtonToggle' expected 32x32 but was 64x32.,
═══ Raw Text - Entry 11 of 14 ═══

2025-12-05 09:28:15,INFO,Application,[Control_TransferTab] Icon toggle 'Control_TransferTab_Button_Toggle_Split' expected 32x32 but was 64x32.,
═══ Parse Failed - Showing Raw Text ═══

2.,
═══ Raw Text - Entry 13 of 14 ═══

2025-12-05 09:28:17,INFO,Application,[Performance Warning] Theme application to form 'MainForm' took 1009ms (>100ms threshold),
═══ Raw Text - Entry 14 of 14 ═══

2025-12-05 09:28:17,INFO,Application,Development Menu configured for user 'RECEIVING': Visible,
