using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.MainForm.Classes;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    #region ControlTransferTab

    public partial class Control_TransferTab : UserControl
    {
        #region Fields

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }

        // Cache ToolTip to avoid repeated instantiation
        private static readonly ToolTip SharedToolTip = new();
        private Helper_StoredProcedureProgress? _progressHelper;

        #endregion

        #region Progress Control Methods

        /// <summary>
        /// Sets progress controls for visual feedback during long-running inventory transfer operations.
        /// </summary>
        /// <param name="progressBar">The progress bar control to display operation progress (0-100%)</param>
        /// <param name="statusLabel">The status label control to display operation status messages</param>
        /// <exception cref="InvalidOperationException">Thrown when control is not added to a form</exception>
        /// <remarks>
        /// Must be called during initialization before any async operations that require progress feedback.
        /// Progress helper is used by LoadDataComboBoxesAsync, Search, Transfer, Reset, and Print operations.
        /// Provides visual feedback for database-intensive operations like inventory transfers and searches.
        /// </remarks>
        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel, 
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Initialization

        public Control_TransferTab()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_TransferTab),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_TransferTab), nameof(Control_TransferTab));

            Service_DebugTracer.TraceUIAction("TRANSFER_TAB_INITIALIZATION", nameof(Control_TransferTab),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Control_TransferTab_Initialize();
            ApplyPrivileges();
            
            // Initialize UI state immediately without heavy operations
            InitializeImmediateUI();
            
            // Move heavy initialization to background thread
            _ = Task.Run(async () =>
            {
                try
                {
                    await InitializeBackgroundOperationsAsync();
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    // If background initialization fails, still ensure basic functionality
                    this.BeginInvoke(() => Control_TransferTab_Update_ButtonStates());
                }
            });

            Service_DebugTracer.TraceUIAction("TRANSFER_TAB_INITIALIZATION", nameof(Control_TransferTab),
                new Dictionary<string, object>
                {
                    ["Phase"] = "COMPLETE",
                    ["ComponentType"] = "UserControl"
                });
        }

        private void InitializeImmediateUI()
        {
            Control_TransferTab_Image_NothingFound.Visible = false;

            // Set initial button states
            Control_TransferTab_Button_Transfer.Enabled = false;
            Control_TransferTab_Button_Search.Enabled = false;
            Control_TransferTab_NumericUpDown_Quantity.Enabled = false;

            // Setup event handlers early
            Control_TransferTab_OnStartup_WireUpEvents();

            // Use cached ToolTip
            SharedToolTip.SetToolTip(Control_TransferTab_Button_Search,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Transfer_Search)}");
            SharedToolTip.SetToolTip(Control_TransferTab_Button_Transfer,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Transfer_Transfer)}");
            SharedToolTip.SetToolTip(Control_TransferTab_Button_Reset,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Transfer_Reset)}");
            SharedToolTip.SetToolTip(Control_TransferTab_Button_Toggle_RightPanel,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Transfer_ToggleRightPanel_Left)}/{Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Transfer_ToggleRightPanel_Right)}");

            // Setup Print button event and tooltip directly
            Control_TransferTab_Button_Print.Click -= Control_TransferTab_Button_Print_Click;
            Control_TransferTab_Button_Print.Click += Control_TransferTab_Button_Print_Click;
            SharedToolTip.SetToolTip(Control_TransferTab_Button_Print, "Print the current results");
        }

        private async Task InitializeBackgroundOperationsAsync()
        {
            try
            {
                // Load ComboBoxes asynchronously in background
                await Control_TransferTab_OnStartup_LoadDataComboBoxesAsync();
                
                // Load user information asynchronously
                try
                {
                    Model_Application_Variables.UserFullName = await Dao_User.GetUserFullNameAsync(Model_Application_Variables.User);
                    LoggingUtility.Log($"User full name loaded: {Model_Application_Variables.UserFullName}");
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "Control_TransferTab_InitializeBackground_GetUserFullName");
                }

                // Update button states on UI thread after background operations complete
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(() =>
                    {
                        try
                        {
                            Control_TransferTab_Update_ButtonStates();
                            LoggingUtility.Log("Transfer tab background initialization completed successfully.");
                        }
                        catch (Exception ex)
                        {
                            LoggingUtility.LogApplicationError(ex);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "Control_TransferTab_InitializeBackground");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the Transfer tab UI components and applies focus highlighting theme.
        /// </summary>
        /// <remarks>
        /// Called automatically during control construction.
        /// Sets Reset button TabStop to false to improve tab navigation flow.
        /// Applies Core_Themes focus highlighting for better keyboard navigation visibility.
        /// </remarks>
        public void Control_TransferTab_Initialize()
        {
            Control_TransferTab_Button_Reset.TabStop = false;

            Core_Themes.ApplyFocusHighlighting(this);
        }

        #endregion

        #region Key Processing

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Core_WipAppVariables.Shortcut_Transfer_Search)
                {
                    if (Control_TransferTab_Button_Search.Visible && Control_TransferTab_Button_Search.Enabled)
                    {
                        Control_TransferTab_Button_Search.PerformClick();
                        return true;
                    }
                }

                if (keyData == Core_WipAppVariables.Shortcut_Transfer_Transfer)
                {
                    if (Control_TransferTab_Button_Transfer.Visible && Control_TransferTab_Button_Transfer.Enabled)
                    {
                        Control_TransferTab_Button_Transfer.PerformClick();
                        return true;
                    }
                }

                if (keyData == Core_WipAppVariables.Shortcut_Transfer_Reset)
                {
                    if (Control_TransferTab_Button_Reset.Visible && Control_TransferTab_Button_Reset.Enabled)
                    {
                        Control_TransferTab_Button_Reset.PerformClick();
                        return true;
                    }
                }

                if (keyData == Core_WipAppVariables.Shortcut_Transfer_ToggleRightPanel_Right)
                {
                    if (Control_TransferTab_Button_Toggle_RightPanel.Visible &&
                        Control_TransferTab_Button_Toggle_RightPanel.Enabled)
                    {
                        Control_TransferTab_Button_Toggle_RightPanel.PerformClick();
                        return true;
                    }
                }

                if (keyData == Core_WipAppVariables.Shortcut_Transfer_ToggleRightPanel_Left)
                {
                    if (Control_TransferTab_Button_Toggle_RightPanel.Visible &&
                        Control_TransferTab_Button_Toggle_RightPanel.Enabled)
                    {
                        Control_TransferTab_Button_Toggle_RightPanel.PerformClick();
                        return true;
                    }
                }

                if (keyData == Keys.Enter)
                {
                    SelectNextControl(ActiveControl, true, true, true, true);
                    return true;
                }

                if (MainFormInstance != null)
                {
                    bool panelCollapsed = MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed;
                    if ((!panelCollapsed && keyData == (Keys.Alt | Keys.Right)) ||
                        (panelCollapsed && keyData == (Keys.Alt | Keys.Left)))
                    {
                        Control_TransferTab_Button_Toggle_RightPanel.PerformClick();
                        return true;
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_ProcessCmdKey").ToString());
                return false;
            }
        }

        #endregion

        #region Startup / ComboBox Loading



        /// <summary>
        /// Asynchronously configures SuggestionTextBox controls (Parts, Operations, and ToLocation) during Transfer tab initialization.
        /// </summary>
        /// <returns>A task that completes when all suggestion controls are configured</returns>
        /// <remarks>
        /// This method is called automatically during control construction and should not be called directly.
        /// Configures data providers, max results, wildcard support, and clear-on-no-match behavior.
        /// Replaces old ComboBox population with SuggestionTextBox configuration for autocomplete functionality.
        /// Handles errors with Service_ErrorHandler for consistent error management across the application.
        /// </remarks>
        public async Task Control_TransferTab_OnStartup_LoadDataComboBoxesAsync()
        {
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Configuring part suggestions...");
                
                // Configure part number SuggestionTextBox
                Control_TransferTab_TextBox_Part.DataProvider = GetPartNumberSuggestionsAsync;
                Control_TransferTab_TextBox_Part.MaxResults = 100;
                Control_TransferTab_TextBox_Part.EnableWildcards = true;
                Control_TransferTab_TextBox_Part.ClearOnNoMatch = true;
                
                _progressHelper?.UpdateProgress(40, "Configuring operation suggestions...");
                
                // Configure operation SuggestionTextBox
                Control_TransferTab_TextBox_Operation.DataProvider = GetOperationSuggestionsAsync;
                Control_TransferTab_TextBox_Operation.MaxResults = 50;
                Control_TransferTab_TextBox_Operation.EnableWildcards = true;
                Control_TransferTab_TextBox_Operation.ClearOnNoMatch = true;
                
                _progressHelper?.UpdateProgress(70, "Configuring location suggestions...");
                
                // Configure location SuggestionTextBox
                Control_TransferTab_TextBox_ToLocation.DataProvider = GetLocationSuggestionsAsync;
                Control_TransferTab_TextBox_ToLocation.MaxResults = 100;
                Control_TransferTab_TextBox_ToLocation.EnableWildcards = true;
                Control_TransferTab_TextBox_ToLocation.ClearOnNoMatch = true;

                _progressHelper?.UpdateProgress(100, "Suggestion controls configured");
                await Task.Delay(100);
                
                LoggingUtility.Log("Transfer tab suggestion controls configured.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.High,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_TransferTab_OnStartup_LoadDataComboBoxesAsync),
                        ["ControlType"] = nameof(Control_TransferTab)
                    },
                    callerName: nameof(Control_TransferTab_OnStartup_LoadDataComboBoxesAsync),
                    controlName: this.Name);
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        #endregion

        #region Suggestion Data Providers

        /// <summary>
        /// Data provider for part number SuggestionTextBox.
        /// Returns list of all part IDs from database.
        /// </summary>
        /// <returns>List of part ID strings.</returns>
        private async Task<List<string>> GetPartNumberSuggestionsAsync()
        {
            try
            {
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_part_ids_Get_All",
                    null);

                if (!dataResult.IsSuccess || dataResult.Data == null)
                {
                    Service_ErrorHandler.ShowWarning(dataResult.ErrorMessage ?? "Failed to load part numbers");
                    return new List<string>();
                }

                var suggestions = new List<string>();
                foreach (System.Data.DataRow row in dataResult.Data.Rows)
                {
                    if (row["PartID"] != null && row["PartID"] != DBNull.Value)
                    {
                        suggestions.Add(row["PartID"].ToString() ?? string.Empty);
                    }
                }

                return suggestions;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_TransferTab),
                        ["Method"] = nameof(GetPartNumberSuggestionsAsync)
                    },
                    callerName: nameof(GetPartNumberSuggestionsAsync),
                    controlName: this.Name);

                return new List<string>();
            }
        }

        /// <summary>
        /// Data provider for operation SuggestionTextBox.
        /// Returns list of all operation numbers from database.
        /// </summary>
        /// <returns>List of operation strings.</returns>
        private async Task<List<string>> GetOperationSuggestionsAsync()
        {
            try
            {
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_operation_numbers_Get_All",
                    null);

                if (!dataResult.IsSuccess || dataResult.Data == null)
                {
                    Service_ErrorHandler.ShowWarning(dataResult.ErrorMessage ?? "Failed to load operations");
                    return new List<string>();
                }

                var suggestions = new List<string>();
                foreach (System.Data.DataRow row in dataResult.Data.Rows)
                {
                    if (row["Operation"] != null && row["Operation"] != DBNull.Value)
                    {
                        suggestions.Add(row["Operation"].ToString() ?? string.Empty);
                    }
                }

                return suggestions;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_TransferTab),
                        ["Method"] = nameof(GetOperationSuggestionsAsync)
                    },
                    callerName: nameof(GetOperationSuggestionsAsync),
                    controlName: this.Name);

                return new List<string>();
            }
        }

        /// <summary>
        /// Data provider for location SuggestionTextBox.
        /// Returns list of all location names from database.
        /// </summary>
        /// <returns>List of location strings.</returns>
        private async Task<List<string>> GetLocationSuggestionsAsync()
        {
            try
            {
                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_locations_Get_All",
                    null);

                if (!dataResult.IsSuccess || dataResult.Data == null)
                {
                    Service_ErrorHandler.ShowWarning(dataResult.ErrorMessage ?? "Failed to load locations");
                    return new List<string>();
                }

                var suggestions = new List<string>();
                foreach (System.Data.DataRow row in dataResult.Data.Rows)
                {
                    if (row["Location"] != null && row["Location"] != DBNull.Value)
                    {
                        suggestions.Add(row["Location"].ToString() ?? string.Empty);
                    }
                }

                return suggestions;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_TransferTab),
                        ["Method"] = nameof(GetLocationSuggestionsAsync)
                    },
                    callerName: nameof(GetLocationSuggestionsAsync),
                    controlName: this.Name);

                return new List<string>();
            }
        }

        #endregion

        #region Button Clicks

        private async void Control_TransferTab_Button_Reset_Click()
        {
            // Show progress bar at the start
            _progressHelper?.ShowProgress();
            _progressHelper?.UpdateProgress(10, "Resetting Transfer tab...");

            Control_TransferTab_Button_Reset.Enabled = false;
            try
            {
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    await Control_TransferTab_HardReset();
                }
                else
                {
                    Control_TransferTab_SoftReset();
                }

                _progressHelper?.UpdateProgress(100, "Reset complete");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in TransferTab Reset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_Transfer_Button_Reset_Click").ToString());
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private async Task Control_TransferTab_HardReset()
        {
            Control_TransferTab_Button_Reset.Enabled = false;
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Transfer tab...");
                Debug.WriteLine("[DEBUG] TransferTab HardReset - start");
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for hard reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                _progressHelper?.UpdateProgress(30, "Resetting data tables...");

                Debug.WriteLine("[DEBUG] Resetting and refreshing all ComboBox DataTables");
                await Helper_UI_ComboBoxes.ResetAndRefreshAllDataTablesAsync();
                Debug.WriteLine("[DEBUG] DataTables reset complete");

                _progressHelper?.UpdateProgress(60, "Resetting suggestion fields...");
                Debug.WriteLine("[DEBUG] Resetting Part TextBox");
                Control_TransferTab_TextBox_Part.Text = string.Empty;
                Debug.WriteLine("[DEBUG] Resetting Operation TextBox");
                Control_TransferTab_TextBox_Operation.Text = string.Empty;
                Debug.WriteLine("[DEBUG] Resetting ToLocation TextBox");
                Control_TransferTab_TextBox_ToLocation.Text = string.Empty;

                Debug.WriteLine("[DEBUG] Resetting UI fields");

                Control_TransferTab_DataGridView_Main.DataSource = null;
                Control_TransferTab_DataGridView_Main.Refresh();
                Control_TransferTab_Image_NothingFound.Visible = false;

                Control_TransferTab_TextBox_Part.Focus();

                Debug.WriteLine("[DEBUG] TransferTab HardReset - end");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in TransferTab HardReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_Transfer_HardReset").ToString());
            }
            finally
            {
                Debug.WriteLine("[DEBUG] TransferTab HardReset button re-enabled");
                Control_TransferTab_Button_Reset.Enabled = true;
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Resetting TransferTab buttons and status strip");
                    Control_TransferTab_Button_Search.Enabled = false;
                    Control_TransferTab_Button_Transfer.Enabled = false;
                    Control_TransferTab_NumericUpDown_Quantity.Value =
                        Control_TransferTab_NumericUpDown_Quantity.Minimum;
                    Control_TransferTab_NumericUpDown_Quantity.Enabled = false;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                    _progressHelper?.HideProgress();
                }
            }
        }

        private void Control_TransferTab_SoftReset()
        {
            Control_TransferTab_Button_Reset.Enabled = false;
            try
            {
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for Soft Reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                Debug.WriteLine("[DEBUG] Resetting UI fields");
                Control_TransferTab_TextBox_Part.Text = string.Empty;
                Control_TransferTab_TextBox_Operation.Text = string.Empty;
                Control_TransferTab_TextBox_ToLocation.Text = string.Empty;

                Control_TransferTab_DataGridView_Main.DataSource = null;
                Control_TransferTab_DataGridView_Main.Refresh();
                Control_TransferTab_Image_NothingFound.Visible = false;

                Control_TransferTab_Button_Search.Enabled = false;
                Control_TransferTab_Button_Transfer.Enabled = false;
                Control_TransferTab_NumericUpDown_Quantity.Value = Control_TransferTab_NumericUpDown_Quantity.Minimum;
                Control_TransferTab_NumericUpDown_Quantity.Enabled = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in TransferTab SoftReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_Transfer_SoftReset").ToString());
            }
            finally
            {
                Debug.WriteLine("[DEBUG] TransferTab SoftReset button re-enabled");
                Control_TransferTab_Button_Reset.Enabled = true;
                Control_TransferTab_TextBox_Part.Focus();
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                }
            }
        }

        private async void Control_TransferTab_Button_Search_Click(object? sender, EventArgs? e)
        {
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Searching inventory...");

                LoggingUtility.Log("TransferTab Search button clicked.");
                
                string partId = Control_TransferTab_TextBox_Part.Text?.Trim() ?? "";
                string op = Control_TransferTab_TextBox_Operation.Text?.Trim() ?? "";
                
                if (string.IsNullOrWhiteSpace(partId) || string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_Part.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a valid Part.", "Part Selection");
                    Control_TransferTab_TextBox_Part.Focus();
                    return;
                }

                DataTable? results = null;

                if (!string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_Operation.Text) &&
                    !string.IsNullOrWhiteSpace(op) && op != @"Enter or Select Operation")
                {
                    _progressHelper?.UpdateProgress(40, "Querying by part and operation...");
                    var partOpResult = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, op, true);
                    if (partOpResult.IsSuccess)
                    {
                        results = partOpResult.Data!;
                    }
                    else
                    {
                        throw new Exception(partOpResult.ErrorMessage ?? "Failed to retrieve inventory by part and operation");
                    }
                }
                else
                {
                    _progressHelper?.UpdateProgress(40, "Querying by part...");
                    var partResult = await Dao_Inventory.GetInventoryByPartIdAsync(partId, true);
                    if (partResult.IsSuccess)
                    {
                        results = partResult.Data!;
                    }
                    else
                    {
                        throw new Exception(partResult.ErrorMessage ?? "Failed to retrieve inventory by part");
                    }
                }
                
                if (results == null)
                {
                    throw new Exception("Failed to retrieve inventory data");
                }

                _progressHelper?.UpdateProgress(70, "Updating results...");
                Control_TransferTab_DataGridView_Main.DataSource = results;
                Control_TransferTab_DataGridView_Main.ClearSelection();

                // Only show columns in this order: Location, PartID, Operation, Quantity, Notes
                string[] columnsToShow = { "Location", "PartID", "Operation", "Quantity", "Notes" };
                foreach (DataGridViewColumn column in Control_TransferTab_DataGridView_Main.Columns)
                {
                    column.Visible = columnsToShow.Contains(column.Name);
                }

                // Reorder columns
                for (int i = 0; i < columnsToShow.Length; i++)
                {
                    if (Control_TransferTab_DataGridView_Main.Columns.Contains(columnsToShow[i]))
                    {
                        Control_TransferTab_DataGridView_Main.Columns[columnsToShow[i]].DisplayIndex = i;
                    }
                }

                Core_Themes.ApplyThemeToDataGridView(Control_TransferTab_DataGridView_Main);
                Core_Themes.SizeDataGrid(Control_TransferTab_DataGridView_Main);

                Control_TransferTab_Image_NothingFound.Visible = results.Rows.Count == 0;
                _progressHelper?.UpdateProgress(100, "Search complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_TransferTab_Button_Search_Click").ToString());
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private async Task Control_TransferTab_Button_Save_ClickAsync(object? sender, EventArgs? e)
        {
            try
            {
                // Show progress bar at the start
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Transferring inventory...");

                DataGridViewSelectedRowCollection selectedRows = Control_TransferTab_DataGridView_Main.SelectedRows;
                if (selectedRows.Count == 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please select a row to transfer from.",
                        "Row Selection");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_ToLocation.Text) ||
                    string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_ToLocation.Text))
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please select a valid destination location.",
                        "Location Selection");
                    Control_TransferTab_TextBox_ToLocation?.Focus();
                    return;
                }

                _progressHelper?.UpdateProgress(40, "Processing transfer...");

                if (selectedRows.Count == 1)
                {
                    await TransferSingleRowAsync(selectedRows[0]);
                }
                else
                {
                    await TransferMultipleRowsAsync(selectedRows);
                }

                _progressHelper?.UpdateProgress(80, "Refreshing results...");
                Control_TransferTab_Button_Search_Click(null, null);

                _progressHelper?.UpdateProgress(100, "Transfer complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_TransferTab_Button_Transfer_Click").ToString());
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private void Control_TransferTab_Button_Print_Click(object? sender, EventArgs? e)
        {
            try
            {
                LoggingUtility.Log("[TransferTab] Print requested.");

                if (Control_TransferTab_DataGridView_Main is null || Control_TransferTab_DataGridView_Main.Rows.Count == 0)
                {
                    LoggingUtility.Log("[TransferTab] Print aborted - grid is empty.");
                    Service_ErrorHandler.HandleValidationError(
                        "No records available to print. Run a search or perform an inventory transfer first.",
                        "Print");
                    return;
                }

                Control parent = FindForm() is Control form ? form : this;
                string gridName = "Transfer Inventory";

                var dialogTask = Helper_PrintManager.ShowPrintDialogAsync(parent, Control_TransferTab_DataGridView_Main, gridName);

                dialogTask.ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {
                        LoggingUtility.Log($"[TransferTab] Print dialog closed with result: {t.Result}.");
                    }
                    else if (t.IsFaulted)
                    {
                        Exception? baseException = t.Exception?.GetBaseException();
                        if (baseException != null)
                        {
                            LoggingUtility.LogApplicationError(baseException);
                            BeginInvoke(new Action(() =>
                                Service_ErrorHandler.HandleException(
                                    baseException,
                                    Enum_ErrorSeverity.Medium,
                                    controlName: nameof(Control_TransferTab_Button_Print_Click))));
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    controlName: nameof(Control_TransferTab_Button_Print_Click));
            }
        }

        private async void Control_TransferTab_ContextMenuItem_Print_Click(object? sender, EventArgs e)
        {
            // Reuse existing print button logic
            Control_TransferTab_Button_Print_Click(sender, e);
        }

        #endregion

        #region Transfer Logic

        private async Task TransferSingleRowAsync(DataGridViewRow row)
        {
            if (row.DataBoundItem is not DataRowView drv)
            {
                return;
            }

            string batchNumber = drv["BatchNumber"]?.ToString() ?? "";
            string partId = drv["PartID"]?.ToString() ?? "";
            string fromLocation = drv["Location"]?.ToString() ?? "";
            string itemType = drv.Row.Table.Columns.Contains("ItemType") ? drv["ItemType"]?.ToString() ?? "" : "";
            string notes = drv["Notes"]?.ToString() ?? "";
            string operation = drv["Operation"]?.ToString() ?? "";
            string quantityStr = drv["Quantity"]?.ToString() ?? "";
            if (!int.TryParse(quantityStr, out int originalQuantity))
            {
                LoggingUtility.LogApplicationError(
                    new Exception(
                        $"Invalid quantity value: '{quantityStr}' for PartID={partId}, Location={fromLocation}"));
                return;
            }

            int transferQuantity = Math.Min((int)Control_TransferTab_NumericUpDown_Quantity.Value, originalQuantity);
            string newLocation = Control_TransferTab_TextBox_ToLocation.Text;
            string user = Model_Application_Variables.User ?? Environment.UserName;
            if (transferQuantity < originalQuantity)
            {
                await Dao_Inventory.TransferInventoryQuantityAsync(
                    batchNumber, partId, operation, transferQuantity, originalQuantity, newLocation, user);
            }
            else
            {
                // TransferPartSimpleAsync transfers entire quantity - no quantity parameter needed
                await Dao_Inventory.TransferPartSimpleAsync(
                    batchNumber, partId, operation, newLocation);
            }

            var historyResult = await Dao_History.AddTransactionHistoryAsync(new Model_Transactions_History
            {
                TransactionType = "TRANSFER",
                PartId = partId,
                FromLocation = fromLocation,
                ToLocation = newLocation,
                Operation = operation,
                Quantity = transferQuantity,
                Notes = notes,
                User = user,
                ItemType = itemType,
                BatchNumber = batchNumber,
                DateTime = DateTime.Now
            });
            
            if (!historyResult.IsSuccess)
            {
                LoggingUtility.Log($"Failed to log transaction history: {historyResult.ErrorMessage}");
            }
            
            if (MainFormInstance != null)
            {
                MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                    $@"Last Transfer: {partId} (Op: {operation}), From: {fromLocation} To: {newLocation}, Qty: {transferQuantity} @ {DateTime.Now:hh:mm tt}";
            }
        }

        private async Task TransferMultipleRowsAsync(DataGridViewSelectedRowCollection selectedRows)
        {
            string newLocation = Control_TransferTab_TextBox_ToLocation.Text;
            string user = Model_Application_Variables.User ?? Environment.UserName;
            HashSet<string> partIds = new();
            HashSet<string> operations = new();
            HashSet<string> fromLocations = new();
            int totalQty = 0;
            foreach (DataGridViewRow row in selectedRows)
            {
                if (row.DataBoundItem is not DataRowView drv)
                {
                    continue;
                }

                string batchNumber = drv["BatchNumber"]?.ToString() ?? "";
                string partId = drv["PartID"]?.ToString() ?? "";
                string fromLocation = drv["Location"]?.ToString() ?? "";
                string itemType = drv.Row.Table.Columns.Contains("ItemType") ? drv["ItemType"]?.ToString() ?? "" : "";
                string operation = drv["Operation"]?.ToString() ?? "";
                string quantityStr = drv["Quantity"]?.ToString() ?? "";
                string notes = drv["Notes"]?.ToString() ?? "";
                if (!int.TryParse(quantityStr, out int originalQuantity))
                {
                    LoggingUtility.LogApplicationError(new Exception(
                        $"Invalid quantity value: '{quantityStr}' for PartID={partId}, Location={fromLocation}"));
                    continue;
                }

                int transferQuantity =
                    Math.Min((int)Control_TransferTab_NumericUpDown_Quantity.Value, originalQuantity);
                // TransferPartSimpleAsync transfers entire quantity - no quantity parameter needed
                await Dao_Inventory.TransferPartSimpleAsync(
                    batchNumber, partId, operation, newLocation);
                    
                var historyResult = await Dao_History.AddTransactionHistoryAsync(new Model_Transactions_History
                {
                    TransactionType = "TRANSFER",
                    PartId = partId,
                    FromLocation = fromLocation,
                    ToLocation = newLocation,
                    Operation = operation,
                    Quantity = transferQuantity,
                    Notes = notes,
                    User = user,
                    ItemType = itemType,
                    BatchNumber = batchNumber,
                    DateTime = DateTime.Now
                });
                
                if (!historyResult.IsSuccess)
                {
                    LoggingUtility.Log($"Failed to log transaction history: {historyResult.ErrorMessage}");
                }
                
                partIds.Add(partId);
                operations.Add(operation);
                fromLocations.Add(fromLocation);
                totalQty += transferQuantity;
            }

            if (MainFormInstance != null)
            {
                string time = DateTime.Now.ToString("hh:mm tt");
                string fromLocDisplay = fromLocations.Count > 1
                    ? "Multiple Locations"
                    : fromLocations.FirstOrDefault() ?? "";
                if (partIds.Count == 1 && operations.Count == 1)
                {
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                        $@"Last Transfer: {partIds.First()} (Op: {operations.First()}), From: {fromLocDisplay} To: {newLocation}, Qty: {totalQty} @ {time}";
                }
                else if (partIds.Count == 1 && operations.Count > 1)
                {
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                        $@"Last Transfer: {partIds.First()} (Multiple Ops), From: {fromLocDisplay} To: {newLocation}, Qty: {totalQty} @ {time}";
                }
                else
                {
                    string qtyDisplay = partIds.Count == 1 ? totalQty.ToString() : "Multiple";
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                        $@"Last Transfer: Multiple Part ID's, From: {fromLocDisplay} To: {newLocation}, Qty: {qtyDisplay} @ {time}";
                }
            }
        }

        #endregion

        #region TextBox & UI Events

        private void Control_TransferTab_Update_ButtonStates()
        {
            var callStack = new System.Diagnostics.StackTrace();
            var callerFrame = callStack.GetFrame(1);
            var callerMethod = callerFrame?.GetMethod()?.Name ?? "Unknown";
            LoggingUtility.Log($"[PERF] Update_ButtonStates called from: {callerMethod}");
            
            try
            {
                // Cache frequently accessed properties to avoid repeated lookups
                bool hasPart = !string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_Part.Text);
                bool hasData = Control_TransferTab_DataGridView_Main.Rows.Count > 0;
                bool hasSelection = hasData && Control_TransferTab_DataGridView_Main.SelectedRows.Count > 0;
                bool hasToLocation = !string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_ToLocation.Text) &&
                                     !string.IsNullOrWhiteSpace(Control_TransferTab_TextBox_ToLocation.Text);
                bool hasQuantity = Control_TransferTab_NumericUpDown_Quantity.Value > 0;

                // Update control states efficiently
                Control_TransferTab_Button_Search.Enabled = hasPart;
                // ToLocation TextBox stays enabled (respects user privileges from ApplyPrivileges)
                Control_TransferTab_NumericUpDown_Quantity.Enabled = hasData && 
                    Control_TransferTab_DataGridView_Main.SelectedRows.Count <= 1;

                // Check if destination location is same as source location (only if we have selection and destination)
                bool toLocationIsSameAsRow = false;
                if (hasSelection && hasToLocation)
                {
                    // Optimize by only checking first selected row for performance
                    var firstSelectedRow = Control_TransferTab_DataGridView_Main.SelectedRows[0];
                    if (firstSelectedRow?.DataBoundItem is DataRowView drv)
                    {
                        string rowLocation = drv["Location"]?.ToString() ?? string.Empty;
                        toLocationIsSameAsRow = string.Equals(rowLocation, Control_TransferTab_TextBox_ToLocation.Text,
                            StringComparison.OrdinalIgnoreCase);
                    }
                }

                // Update transfer button state
                Control_TransferTab_Button_Transfer.Enabled =
                    hasData && hasSelection && hasToLocation && hasPart && hasQuantity && !toLocationIsSameAsRow;
                
                // Update print button state if it exists
                if (Control_TransferTab_Button_Print != null)
                {
                    Control_TransferTab_Button_Print.Enabled = hasData;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_TransferTab_Update_ButtonStates").ToString());
            }
        }

        private void Control_TransferTab_OnStartup_WireUpEvents()
        {
            try
            {
                // Use lambda to match EventHandler signature for async void method
                Control_TransferTab_Button_Reset.Click += (s, e) => Control_TransferTab_Button_Reset_Click();
                                
                // Transfer button
                Control_TransferTab_Button_Transfer.Click +=
                    async (s, e) => await Control_TransferTab_Button_Save_ClickAsync(s, e);

                // NumericUpDown
                Control_TransferTab_NumericUpDown_Quantity.ValueChanged +=
                    (s, e) => Control_TransferTab_Update_ButtonStates();

                // DataGridView
                Control_TransferTab_DataGridView_Main.SelectionChanged +=
                    (s, e) => Control_TransferTab_Update_ButtonStates();
                Control_TransferTab_DataGridView_Main.SelectionChanged +=
                    Control_TransferTab_DataGridView_Main_SelectionChanged;
                Control_TransferTab_DataGridView_Main.DataSourceChanged +=
                    (s, e) => Control_TransferTab_Update_ButtonStates();


                LoggingUtility.Log("Transfer tab events wired up.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_WireUpTransferTabEvents").ToString());
            }
        }

        private void Control_TransferTab_DataGridView_Main_SelectionChanged(object? sender, EventArgs? e)
        {
            try
            {
                var selectedRowCount = Control_TransferTab_DataGridView_Main.SelectedRows.Count;
                
                if (selectedRowCount == 1)
                {
                    var row = Control_TransferTab_DataGridView_Main.SelectedRows[0];
                    if (row?.DataBoundItem is DataRowView drv && 
                        int.TryParse(drv["Quantity"]?.ToString(), out int qty) && qty > 0)
                    {
                        Control_TransferTab_NumericUpDown_Quantity.Maximum = qty;
                        Control_TransferTab_NumericUpDown_Quantity.Value = qty;
                        Control_TransferTab_NumericUpDown_Quantity.Enabled = true;
                    }
                    else
                    {
                        Control_TransferTab_NumericUpDown_Quantity.Enabled = false;
                    }
                }
                else if (selectedRowCount > 1)
                {
                    // Multiple rows selected - disable quantity editing
                    Control_TransferTab_NumericUpDown_Quantity.Enabled = false;
                }
                else
                {
                    // No rows selected
                    Control_TransferTab_NumericUpDown_Quantity.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void Control_TransferTab_Button_Toggle_RightPanel_Click(object sender, EventArgs e)
        {
            if (MainFormInstance != null)
            {
                bool panelCollapsed = MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed;
                MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed = !panelCollapsed;
                Control_TransferTab_Button_Toggle_RightPanel.Text = panelCollapsed ? "➡️" : "⬅️";
                Control_TransferTab_Button_Toggle_RightPanel.ForeColor = panelCollapsed
                    ? Model_Application_Variables.UserUiColors.SuccessColor ?? Color.Green
                    : Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red;
            }

            Helper_UI_ComboBoxes.DeselectAllComboBoxText(this);
        }

        #endregion

        #region Privileges

        private void ApplyPrivileges()
        {
            bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;
            bool isAdmin = Model_Application_Variables.UserTypeAdmin;
            bool isNormal = Model_Application_Variables.UserTypeNormal;
            bool isReadOnly = Model_Application_Variables.UserTypeReadOnly;

            // Developers have all Admin privileges
            bool hasAdminAccess = isDeveloper || isAdmin;

            // ComboBoxes
            Control_TransferTab_TextBox_Part.Enabled = hasAdminAccess || isNormal || isReadOnly;
            Control_TransferTab_TextBox_Operation.Enabled = hasAdminAccess || isNormal || isReadOnly;
            Control_TransferTab_TextBox_ToLocation.Enabled = hasAdminAccess || isNormal || isReadOnly;
            // NumericUpDown
            Control_TransferTab_NumericUpDown_Quantity.ReadOnly = isReadOnly;
            Control_TransferTab_NumericUpDown_Quantity.Enabled = hasAdminAccess || isNormal || isReadOnly;
            // DataGridView
            Control_TransferTab_DataGridView_Main.ReadOnly = isReadOnly;
            Control_TransferTab_DataGridView_Main.Enabled = hasAdminAccess || isNormal || isReadOnly;
            // Buttons
            Control_TransferTab_Button_Transfer.Visible = hasAdminAccess || isNormal;
            Control_TransferTab_Button_Transfer.Enabled = hasAdminAccess || isNormal;
            Control_TransferTab_Button_Reset.Visible = true;
            Control_TransferTab_Button_Reset.Enabled = true;
            Control_TransferTab_Button_Search.Visible = true;
            Control_TransferTab_Button_Search.Enabled = true;
            Control_TransferTab_Button_Toggle_RightPanel.Visible = true;
            Control_TransferTab_Button_Toggle_RightPanel.Enabled = true;
            // Panels, labels, images, etc. are always visible
            // If you add more, follow the same pattern

            // For Read-Only, hide Transfer button
            if (isReadOnly)
            {
                Control_TransferTab_Button_Transfer.Visible = false;
                Control_TransferTab_Button_Transfer.Enabled = false;
            }
            // TODO: If there are TreeView branches, set their .Visible property here as well.
        }

        #endregion

        #region Helper Methods


        #endregion

        private void Control_TransferTab_Button_Toggle_Split_Click(object sender, EventArgs e)
        {
            SplitContainer? splitContainer = Control_TransferTab_SplitContainer_Main;
            Button? button = sender as Button ?? Control_TransferTab_Button_Toggle_Split;

            if (splitContainer.Panel1Collapsed)
            {
                splitContainer.Panel1Collapsed = false;
                button.Text = "Collapse ⬅️";
            }
            else
            {
                splitContainer.Panel1Collapsed = true;
                button.Text = "Expand ➡️";
            }
        }
    }

    #endregion
}
