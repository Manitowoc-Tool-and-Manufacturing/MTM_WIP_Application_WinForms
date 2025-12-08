using System.Data;
using System.Diagnostics;
using System.Reflection;
using MTM_WIP_Application_Winforms.Controls.MainForm;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace MTM_WIP_Application_Winforms.Helpers
{
    #region Helper_UI_ComboBoxes

    public static class Helper_UI_ComboBoxes
    {
        #region Public Variables - MainForm Instance

        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }

        #endregion

        #region Private Variables - DataTables

        private static readonly DataTable ComboBoxPart_DataTable = new();
        private static readonly DataTable ComboBoxOperation_DataTable = new();
        private static readonly DataTable ComboBoxLocation_DataTable = new();
        private static readonly DataTable ComboBoxUser_DataTable = new();
        private static readonly DataTable ComboBoxItemType_DataTable = new();

        #endregion

        #region Private Variables - Locks

        private static readonly object PartDataLock = new();
        private static readonly object OperationDataLock = new();
        private static readonly object LocationDataLock = new();
        private static readonly object UserDataLock = new();
        private static readonly object ItemTypeDataLock = new();

        #endregion

        #region DataTableSetup

        public static async Task SetupPartDataTable()
        {
            try
            {
                // FIXED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                // because our stored procedures HAVE p_Status and p_ErrorMsg parameters
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_part_ids_Get_All",
                    null, // No parameters needed
                    null // No progress helper for this method
                );

                lock (PartDataLock)
                {
                    ComboBoxPart_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        SafeMergeDataTable(ComboBoxPart_DataTable, dataResult.Data, "ComboBoxPart");
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure fails, create an empty table to prevent crashes
                lock (PartDataLock)
                {
                    ComboBoxPart_DataTable.Clear();
                }
                Services.Logging.LoggingUtility.LogApplicationError(ex);
            }
        }

        public static async Task SetupOperationDataTable()
        {
            try
            {
                // FIXED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_operation_numbers_Get_All",
                    null, // No parameters needed
                    null // No progress helper for this method
                );

                lock (OperationDataLock)
                {
                    ComboBoxOperation_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        SafeMergeDataTable(ComboBoxOperation_DataTable, dataResult.Data, "ComboBoxOperation");
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure fails, create an empty table to prevent crashes
                lock (OperationDataLock)
                {
                    ComboBoxOperation_DataTable.Clear();
                }
                Services.Logging.LoggingUtility.LogApplicationError(ex);
            }
        }

        public static async Task SetupLocationDataTable()
        {
            try
            {
                // FIXED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_locations_Get_All",
                    null, // No parameters needed
                    null // No progress helper for this method
                );

                lock (LocationDataLock)
                {
                    ComboBoxLocation_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        SafeMergeDataTable(ComboBoxLocation_DataTable, dataResult.Data, "ComboBoxLocation");
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure fails, create an empty table to prevent crashes
                lock (LocationDataLock)
                {
                    ComboBoxLocation_DataTable.Clear();
                }
                Services.Logging.LoggingUtility.LogApplicationError(ex);
            }
        }

        public static async Task SetupUserDataTable()
        {
            try
            {
                // FIXED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "usr_users_Get_All",
                    null, // No parameters needed
                    null // No progress helper for this method
                );

                lock (UserDataLock)
                {
                    ComboBoxUser_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        // Use safe merge with schema validation
                        SafeMergeDataTable(ComboBoxUser_DataTable, dataResult.Data, "ComboBoxUser");

                        // Remove any row where the User column contains '[ All Users ]' or similar
                        List<DataRow> rowsToRemove = new();
                        foreach (DataRow row in ComboBoxUser_DataTable.Rows)
                        {
                            string userVal = row["User"]?.ToString() ?? string.Empty;
                            if (userVal.Contains("All Users"))
                            {
                                rowsToRemove.Add(row);
                            }
                        }

                        foreach (DataRow row in rowsToRemove)
                        {
                            ComboBoxUser_DataTable.Rows.Remove(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure fails, create an empty table to prevent crashes
                lock (UserDataLock)
                {
                    ComboBoxUser_DataTable.Clear();
                }
                Services.Logging.LoggingUtility.LogApplicationError(ex);
            }
        }

        public static async Task SetupItemTypeDataTable()
        {
            try
            {
                // FIXED: Use Helper_Database_StoredProcedure instead of Helper_Database_Core
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_item_types_Get_All",
                    null, // No parameters needed
                    null  // No progress helper for this method
                );

                lock (ItemTypeDataLock)
                {
                    ComboBoxItemType_DataTable.Clear();
                    if (dataResult.IsSuccess && dataResult.Data != null)
                    {
                        SafeMergeDataTable(ComboBoxItemType_DataTable, dataResult.Data, "ComboBoxItemType");
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure fails, create an empty table to prevent crashes
                lock (ItemTypeDataLock)
                {
                    ComboBoxItemType_DataTable.Clear();
                }
                Services.Logging.LoggingUtility.LogApplicationError(ex);
            }
        }

        #endregion

        #region FillComboBoxes

        public static async Task FillPartComboBoxesAsync(ComboBox comboBox)
        {
            try
            {
                await FillComboBoxAsync(
                    ComboBoxPart_DataTable,
                    comboBox,
                    "PartID",
                    "ID",
                    "[ Enter Part Number ]",
                    PartDataLock
                ).ConfigureAwait(false);
                comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while filling part combo boxes.", ex);
            }
        }

        public static async Task FillOperationComboBoxesAsync(ComboBox comboBox)
        {
            try
            {
                await FillComboBoxAsync(
                    ComboBoxOperation_DataTable,
                    comboBox,
                    "Operation",
                    "ID",
                    "[ Enter Operation ]",
                    OperationDataLock
                ).ConfigureAwait(false);
                comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while filling operation combo boxes.", ex);
            }
        }

        public static async Task FillLocationComboBoxesAsync(ComboBox comboBox)
        {
            try
            {
                await FillComboBoxAsync(
                    ComboBoxLocation_DataTable,
                    comboBox,
                    "Location",
                    "ID",
                    "[ Enter Location ]",
                    LocationDataLock
                ).ConfigureAwait(false);
                comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while filling location combo boxes.", ex);
            }
        }

        public static async Task FillUserComboBoxesAsync(ComboBox comboBox)
        {
            try
            {
                await FillComboBoxAsync(ComboBoxUser_DataTable, comboBox, "User", "ID", "[ Enter User ]", UserDataLock)
                    .ConfigureAwait(false);
                comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while filling user combo boxes.", ex);
            }
        }

        public static async Task FillItemTypeComboBoxesAsync(ComboBox comboBox)
        {
            try
            {
                await FillComboBoxAsync(
                    ComboBoxItemType_DataTable,
                    comboBox,
                    "ItemType",
                    "ID",
                    "[ Enter Item Type ]",
                    ItemTypeDataLock
                ).ConfigureAwait(false);
                comboBox.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while filling item type combo boxes.", ex);
            }
        }

        public static Task FillComboBoxAsync(
            DataTable dataTable,
            ComboBox comboBox,
            string displayMember,
            string valueMember,
            string placeholder,
            object? dataLock = null)
        {
            void SetComboBox()
            {
                if (dataLock != null)
                {
                    lock (dataLock)
                    {
                        SetComboBoxInternal();
                    }
                }
                else
                {
                    SetComboBoxInternal();
                }
            }

            void SetComboBoxInternal()
            {
                if (dataTable.Columns.Count == 0)
                {
                    Debug.WriteLine(
                        $"[WARNING] FillComboBoxAsync called with empty DataTable schema. Skipping ComboBox fill for '{displayMember}'/'{valueMember}'.");
                    return;
                }

                if (!dataTable.Columns.Contains(displayMember) || !dataTable.Columns.Contains(valueMember))
                {
                    throw new InvalidOperationException(
                        $"DataTable does not contain required columns: '{displayMember}' or '{valueMember}'. " +
                        $"Actual columns: {string.Join(", ", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");
                }

                // Create a copy of the DataTable to avoid shared reference issues
                DataTable comboDataTable = dataTable.Copy();

                // Check if placeholder already exists in the copy
                bool hasPlaceholder = comboDataTable.Rows.Count > 0 &&
                                      comboDataTable.Rows[0][displayMember]?.ToString() == placeholder;

                if (!hasPlaceholder)
                {
                    DataRow row = comboDataTable.NewRow();
                    row[displayMember] = placeholder;
                    if (comboDataTable.Columns[valueMember] != null &&
                        comboDataTable.Columns[valueMember]!.DataType == typeof(int))
                    {
                        row[valueMember] = -1;
                    }
                    else
                    {
                        row[valueMember] = placeholder;
                    }

                    comboDataTable.Rows.InsertAt(row, 0);
                }

                comboBox.DataSource = comboDataTable;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;
                comboBox.SelectedIndex = 0;
            }

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke(SetComboBox);
            }
            else
            {
                SetComboBox();
            }

            return Task.CompletedTask;
        }

        #endregion

        #region CachedDataAccess

        /// <summary>
        /// Gets cached part numbers from the pre-loaded ComboBoxPart_DataTable.
        /// Returns empty list if cache not populated.
        /// Thread-safe access to shared cache.
        /// </summary>
        /// <returns>List of all part IDs from cache</returns>
        public static List<string> GetCachedPartNumbers()
        {
            lock (PartDataLock)
            {
                var suggestions = new List<string>(ComboBoxPart_DataTable.Rows.Count);
                
                if (!ComboBoxPart_DataTable.Columns.Contains("PartID"))
                {
                    
                    return suggestions;
                }

                foreach (DataRow row in ComboBoxPart_DataTable.Rows)
                {
                    var value = row["PartID"];
                    if (value != null && value != DBNull.Value)
                    {
                        var partId = value.ToString() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(partId) && !partId.StartsWith("["))
                        {
                            suggestions.Add(partId);
                        }
                    }
                }

                return suggestions;
            }
        }

        /// <summary>
        /// Gets cached item types from the pre-loaded ComboBoxItemType_DataTable.
        /// Returns empty list if cache not populated.
        /// Thread-safe access to shared cache.
        /// </summary>
        /// <returns>List of all item types from cache</returns>
        public static List<string> GetCachedItemTypes()
        {
            lock (ItemTypeDataLock)
            {
                var suggestions = new List<string>(ComboBoxItemType_DataTable.Rows.Count);
                
                if (!ComboBoxItemType_DataTable.Columns.Contains("ItemType"))
                {
                    
                    return suggestions;
                }

                foreach (DataRow row in ComboBoxItemType_DataTable.Rows)
                {
                    var value = row["ItemType"];
                    if (value != null && value != DBNull.Value)
                    {
                        var itemType = value.ToString() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(itemType) && !itemType.StartsWith("["))
                        {
                            suggestions.Add(itemType);
                        }
                    }
                }

                return suggestions;
            }
        }

        /// <summary>
        /// Gets cached operations from the pre-loaded ComboBoxOperation_DataTable.
        /// Returns empty list if cache not populated.
        /// Thread-safe access to shared cache.
        /// </summary>
        /// <returns>List of all operations from cache</returns>
        public static List<string> GetCachedOperations()
        {
            lock (OperationDataLock)
            {
                var suggestions = new List<string>(ComboBoxOperation_DataTable.Rows.Count);
                
                if (!ComboBoxOperation_DataTable.Columns.Contains("Operation"))
                {
                    
                    return suggestions;
                }

                foreach (DataRow row in ComboBoxOperation_DataTable.Rows)
                {
                    var value = row["Operation"];
                    if (value != null && value != DBNull.Value)
                    {
                        var operation = value.ToString() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(operation) && !operation.StartsWith("["))
                        {
                            suggestions.Add(operation);
                        }
                    }
                }

                return suggestions;
            }
        }

        /// <summary>
        /// Gets cached locations from the pre-loaded ComboBoxLocation_DataTable.
        /// Returns empty list if cache not populated.
        /// Thread-safe access to shared cache.
        /// </summary>
        /// <returns>List of all locations from cache</returns>
        public static List<string> GetCachedLocations()
        {
            lock (LocationDataLock)
            {
                var suggestions = new List<string>(ComboBoxLocation_DataTable.Rows.Count);
                
                if (!ComboBoxLocation_DataTable.Columns.Contains("Location"))
                {
                    
                    return suggestions;
                }

                foreach (DataRow row in ComboBoxLocation_DataTable.Rows)
                {
                    var value = row["Location"];
                    if (value != null && value != DBNull.Value)
                    {
                        var location = value.ToString() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(location) && !location.StartsWith("["))
                        {
                            suggestions.Add(location);
                        }
                    }
                }

                return suggestions;
            }
        }

        /// <summary>
        /// Gets cached users from the pre-loaded ComboBoxUser_DataTable.
        /// Returns empty list if cache not populated.
        /// Thread-safe access to shared cache.
        /// </summary>
        /// <returns>List of all users from cache</returns>
        public static List<string> GetCachedUsers()
        {
            lock (UserDataLock)
            {
                var suggestions = new List<string>(ComboBoxUser_DataTable.Rows.Count);
                
                if (!ComboBoxUser_DataTable.Columns.Contains("User"))
                {
                    
                    return suggestions;
                }

                foreach (DataRow row in ComboBoxUser_DataTable.Rows)
                {
                    var value = row["User"];
                    if (value != null && value != DBNull.Value)
                    {
                        var user = value.ToString() ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(user) && !user.StartsWith("["))
                        {
                            suggestions.Add(user);
                        }
                    }
                }

                return suggestions;
            }
        }

        #endregion

        #region DataTableResetAndRefresh

        public static async Task SetupDataTables()
        {
            await SetupPartDataTable();
            await SetupOperationDataTable();
            await SetupLocationDataTable();
            await SetupUserDataTable();
            await SetupItemTypeDataTable();
        }

        public static async Task ResetAndRefreshAllDataTablesAsync()
        {
            if (MainFormInstance != null)
            {
                await UnbindAllComboBoxDataSourcesAsync(MainFormInstance);
            }

            await SetupDataTables();

            await ReloadAllTabComboBoxesAsync();
        }

        public static Task UnbindAllComboBoxDataSourcesAsync(Control root) =>
            Task.Run(() =>
            {
                void Unbind(Control parent)
                {
                    foreach (Control control in parent.Controls)
                    {
                        if (control is ComboBox combo)
                        {
                            if (combo.InvokeRequired)
                            {
                                combo.Invoke(new Action(() => combo.DataSource = null));
                            }
                            else
                            {
                                combo.DataSource = null;
                            }
                        }

                        if (control.HasChildren)
                        {
                            Unbind(control);
                        }
                    }
                }

                Unbind(root);
            });

        #endregion

        #region DataTable Merge Helpers

        /// <summary>
        /// Safely merges source DataTable into target with schema validation and error handling
        /// </summary>
        /// <param name="target">Target DataTable to merge into</param>
        /// <param name="source">Source DataTable to merge from</param>
        /// <param name="tableName">Name of the table for logging purposes</param>
        /// <returns>True if merge was successful, false if replaced due to schema mismatch</returns>
        private static bool SafeMergeDataTable(DataTable target, DataTable source, string tableName)
        {
            if (source == null || source.Rows.Count == 0)
            {
                
                return true; // Not an error, just nothing to merge
            }

            try
            {
                // Extract schema information
                var sourceSchema = source.Columns.Cast<DataColumn>()
                    .Select(c => new { c.ColumnName, c.DataType })
                    .ToList();
                var targetSchema = target.Columns.Cast<DataColumn>()
                    .Select(c => new { c.ColumnName, c.DataType })
                    .ToList();

                // Check if schemas match
                bool schemaMatches = sourceSchema.Count == targetSchema.Count &&
                    sourceSchema.All(sc => targetSchema.Any(tc =>
                        tc.ColumnName.Equals(sc.ColumnName, StringComparison.OrdinalIgnoreCase) &&
                        tc.DataType == sc.DataType));

                if (!schemaMatches)
                {
                    // Log schema mismatch details
                    var sourceColumns = string.Join(", ", sourceSchema.Select(c => $"{c.ColumnName}({c.DataType.Name})"));
                    var targetColumns = string.Join(", ", targetSchema.Select(c => $"{c.ColumnName}({c.DataType.Name})"));

                    
                    
                    
                    

                    // Clear and replace with a copy of the source
                    target.Clear();
                    target.Columns.Clear();
                    target.Merge(source.Copy());

                    return false; // Indicates replacement occurred
                }

                // Schemas match, safe to merge
                target.Merge(source);
                
                return true;
            }
            catch (ArgumentException ex)
            {
                // Handle constraint violations during merge
                
                LoggingUtility.LogApplicationError(ex);

                // Fallback: Replace instead of merge
                try
                {
                    target.Clear();
                    target.Columns.Clear();
                    target.Merge(source.Copy());
                    
                    return false;
                }
                catch (Exception replaceEx)
                {
                    
                    LoggingUtility.LogApplicationError(replaceEx);
                    return false;
                }
            }
            catch (Exception ex)
            {
                
                LoggingUtility.LogApplicationError(ex);
                return false;
            }
        }

        #endregion

        #region ComboBoxUIHelpers

        public static void DeselectAllComboBoxText(Control parent) => ComboBoxHelpers.DeselectAllComboBoxText(parent);

        #endregion

        #region ComboBoxHelpers (Nested static class)

        private static class ComboBoxHelpers
        {
            public static void DeselectAllComboBoxText(Control parent)
            {
                if (parent == null)
                {
                    return;
                }

                foreach (Control control in parent.Controls)
                {
                    if (control is ComboBox comboBox)
                    {
                        if (comboBox.DropDownStyle != ComboBoxStyle.DropDownList)
                        {
                            if (comboBox.InvokeRequired)
                            {
                                comboBox.Invoke(new MethodInvoker(() => comboBox.SelectionLength = 0));
                            }
                            else
                            {
                                comboBox.SelectionLength = 0;
                            }
                        }
                    }

                    if (control.HasChildren)
                    {
                        DeselectAllComboBoxText(control);
                    }
                }
            }
        }

        #endregion

        #region TabComboBoxReload

        public static async Task ReloadAllTabComboBoxesAsync()
        {
            if (MainFormInstance!.MainForm_UserControl_RemoveTab != null)
            {
                await MainFormInstance.MainForm_UserControl_RemoveTab
                    .Control_RemoveTab_OnStartup_LoadDataComboBoxesAsync();
            }

            if (MainFormInstance!.MainForm_UserControl_TransferTab != null)
            {
                await MainFormInstance!.MainForm_UserControl_TransferTab
                    .Control_TransferTab_OnStartup_LoadDataComboBoxesAsync();
            }

            if (MainFormInstance!.MainForm_UserControl_InventoryTab != null)
            {
                await MainFormInstance!.MainForm_UserControl_InventoryTab
                    .Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync();
            }

            if (MainFormInstance!.MainForm_UserControl_AdvancedRemove != null)
            {
                Control_AdvancedRemove? advRemove = MainFormInstance!.MainForm_UserControl_AdvancedRemove;
                MethodInfo? loadComboBoxesAsync = advRemove.GetType().GetMethod("LoadComboBoxesAsync",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (loadComboBoxesAsync != null)
                {
                    await ((Task)loadComboBoxesAsync.Invoke(advRemove, null)!)!;
                }
            }

            if (MainFormInstance!.MainForm_UserControl_AdvancedInventory != null)
            {
                Control_AdvancedInventory? advInv = MainFormInstance!.MainForm_UserControl_AdvancedInventory;
                MethodInfo? loadAllComboBoxesAsync = advInv.GetType().GetMethod("LoadAllComboBoxesAsync",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                if (loadAllComboBoxesAsync != null)
                {
                    await ((Task)loadAllComboBoxesAsync.Invoke(advInv, null)!)!;
                }
            }
        }


        #endregion
    }

    #endregion
}
