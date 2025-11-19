using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.ErrorDialog;
using MTM_WIP_Application_Winforms.Forms.MainForm.Classes;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using static System.Int32;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    #region RemoveTab

    public partial class Control_RemoveTab : UserControl
    {
        #region Fields

        private readonly List<Model_History_Remove> _lastRemovedItems = [];
        private Helper_StoredProcedureProgress? _progressHelper;
        private Control_TextAnimationSequence? _quickButtonsPanelAnimator;
        private Control_TextAnimationSequence? _searchPanelAnimator;
        private bool _isSearchPanelCollapsed;
        private ToolTip? _searchPanelToggleToolTip;

        #endregion

        #region Progress Control Methods

        /// <summary>
        /// Sets progress controls for visual feedback during long-running inventory removal operations.
        /// </summary>
        /// <param name="progressBar">The progress bar control to display operation progress (0-100%)</param>
        /// <param name="statusLabel">The status label control to display operation status messages</param>
        /// <exception cref="InvalidOperationException">Thrown when control is not added to a form</exception>
        /// <remarks>
        /// Must be called during initialization before any async operations that require progress feedback.
        /// Progress helper is used by LoadDataComboBoxesAsync, Delete, Search, and LoadInventory operations.
        /// Provides visual feedback for database-intensive operations like bulk item searches and removals.
        /// </remarks>
        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel,
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }

        #endregion

        #region Constructors

        public Control_RemoveTab()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_RemoveTab),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_RemoveTab), nameof(Control_RemoveTab));

            Service_DebugTracer.TraceUIAction("REMOVE_TAB_INITIALIZATION", nameof(Control_RemoveTab),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();
SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_RemoveTab),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });
            // Apply comprehensive DPI scaling and runtime layout adjustments
            // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
            // Do NOT call theme update methods from arbitrary event handlers or business logic.

            Service_DebugTracer.TraceUIAction("CONTROL_INITIALIZATION", nameof(Control_RemoveTab),
                new Dictionary<string, object> { ["Phase"] = "START" });
            Control_RemoveTab_Initialize();

            Service_DebugTracer.TraceUIAction("COMBOBOX_PROPERTIES_APPLIED", nameof(Control_RemoveTab),
                new Dictionary<string, object>
                {
                    ["ComboBoxes"] = new[] { "Part", "Operation" },
                    ["StandardProperties"] = "Applied"
                });
            // NOTE: SuggestionTextBoxWithLabel handles combo-like behaviors and theming automatically
            Control_RemoveTab_Image_NothingFound.Visible = false;

            Service_DebugTracer.TraceUIAction("DATA_LOADING_START", nameof(Control_RemoveTab),
                new Dictionary<string, object> { ["DataType"] = "SuggestionTextBoxes" });
            _ = Control_RemoveTab_OnStartup_LoadComboBoxesAsync();

            Service_DebugTracer.TraceUIAction("EVENT_HANDLERS_SETUP", nameof(Control_RemoveTab),
                new Dictionary<string, object>
                {
                    ["ButtonEvents"] = new[] { "Print" },
                    ["EventRewiring"] = true
                });
            Control_RemoveTab_Button_Print.Click -= Control_RemoveTab_Button_Print_Click;
            Control_RemoveTab_Button_Print.Click += Control_RemoveTab_Button_Print_Click;

            Service_DebugTracer.TraceUIAction("TOOLTIPS_SETUP", nameof(Control_RemoveTab),
                new Dictionary<string, object>
                {
                    ["TooltipCount"] = 3,
                    ["ButtonsConfigured"] = new[] { "Search", "Delete", "Reset" }
                });
            ToolTip toolTip = new();
            toolTip.SetToolTip(Control_RemoveTab_Button_Search,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Search)}");
            toolTip.SetToolTip(Control_RemoveTab_Button_Delete,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Delete)}");
            toolTip.SetToolTip(Control_RemoveTab_Button_Reset,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Reset)}");
            toolTip.SetToolTip(Control_RemoveTab_Button_Toggle_InputPanel, "Collapse search panel");
            _searchPanelToggleToolTip = toolTip;

            Service_DebugTracer.TraceUIAction("PRIVILEGES_APPLIED", nameof(Control_RemoveTab));
            ApplyPrivileges();
            InitializeQuickButtonsPanelAnimator();
            InitializeSearchPanelToggleAnimator();

            Service_DebugTracer.TraceUIAction("REMOVE_TAB_INITIALIZATION", nameof(Control_RemoveTab),
                new Dictionary<string, object>
                {
                    ["Phase"] = "COMPLETE",
                    ["Success"] = true
                });

            Service_DebugTracer.TraceMethodExit(null, nameof(Control_RemoveTab), nameof(Control_RemoveTab));
        }

        #endregion

        #region Methods

        #region Privileges

        private void ApplyPrivileges()
        {
            bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;
            bool isAdmin = Model_Application_Variables.UserTypeAdmin;
            bool isNormal = Model_Application_Variables.UserTypeNormal;
            bool isReadOnly = Model_Application_Variables.UserTypeReadOnly;

            // Developers have all Admin privileges
            bool hasAdminAccess = isDeveloper || isAdmin;

            // Buttons
            Control_RemoveTab_Button_AdvancedItemRemoval.Visible = hasAdminAccess || isNormal;
            Control_RemoveTab_Button_Reset.Visible = true;
            Control_RemoveTab_Button_Delete.Visible = hasAdminAccess || isNormal;
            Control_RemoveTab_Button_Search.Visible = true;
            Control_RemoveTab_Button_Toggle_RightPanel.Visible = true;
            Control_RemoveTab_Button_Undo.Visible = hasAdminAccess || isNormal;


            // For Read-Only, hide buttons and disable ComboBoxes
            if (isReadOnly)
            {
                Control_RemoveTab_Button_AdvancedItemRemoval.Visible = false;
                Control_RemoveTab_Button_Delete.Visible = false;
                Control_RemoveTab_Button_Undo.Visible = false;
            }
        }

        #endregion

        #region Initialization

        private void Control_RemoveTab_Initialize()
        {
            Control_RemoveTab_Button_Reset.TabStop = false;
            Control_RemoveTab_TextBox_Part.SetF4ButtonTabStop(false);
            Control_RemoveTab_TextBox_Operation.SetF4ButtonTabStop(false);
            Core_Themes.ApplyFocusHighlighting(this);
        }

        #endregion

        #region Startup / ComboBox Loading

        private async Task Control_RemoveTab_OnStartup_LoadComboBoxesAsync()
        {
            try
            {
                await Control_RemoveTab_OnStartup_LoadDataComboBoxesAsync();
                Control_RemoveTab_OnStartup_WireUpEvents();

                Control_RemoveTab_Button_Search.Enabled = false;
                Control_RemoveTab_Button_Delete.Enabled = false;

                try
                {
                    Model_Application_Variables.UserFullName =
                        await Dao_User.GetUserFullNameAsync(Model_Application_Variables.User);

                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                        new StringBuilder().Append("Control_RemoveTab_OnStartup_GetUserFullName").ToString());
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_OnStartup").ToString());
            }
        }

        /// <summary>
        /// Asynchronously loads suggestion control data (Parts and Operations) during Remove tab initialization.
        /// </summary>
        /// <returns>A task that completes when suggestion controls are configured</returns>
        /// <remarks>
        /// This method is called automatically during control construction and should not be called directly.
        /// Configures SuggestionTextBoxWithLabel controls with data provider delegates.
        /// Handles errors with Dao_ErrorLog.HandleException_GeneralError_CloseApp for critical initialization failures.
        /// </remarks>
        public async Task Control_RemoveTab_OnStartup_LoadDataComboBoxesAsync()
        {
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Configuring part suggestions...");

                // Configure SuggestionTextBoxWithLabel controls by wiring their inner textboxes
                Helper_SuggestionTextBox.ConfigureForPartNumbers(
                    Control_RemoveTab_TextBox_Part.TextBox,
                    GetPartNumberSuggestionsAsync,
                    enableF4: true);

                _progressHelper?.UpdateProgress(70, "Configuring operation suggestions...");

                Helper_SuggestionTextBox.ConfigureForOperations(
                    Control_RemoveTab_TextBox_Operation.TextBox,
                    GetOperationSuggestionsAsync,
                    enableF4: true);

                _progressHelper?.UpdateProgress(100, "Suggestion controls configured");
                await Task.Delay(100);


            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_LoadRemoveTabComboBoxesAsync").ToString());
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
                        ["Control"] = nameof(Control_RemoveTab),
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
                        ["Control"] = nameof(Control_RemoveTab),
                        ["Method"] = nameof(GetOperationSuggestionsAsync)
                    },
                    callerName: nameof(GetOperationSuggestionsAsync),
                    controlName: this.Name);

                return new List<string>();
            }
        }

        #endregion

        #region Key Processing

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == Keys.Enter)
                {
                    SelectNextControl(
                        ActiveControl,
                        true,
                        true,
                        true,
                        true
                    );
                    return true;
                }

                if (keyData == Keys.Delete)
                {
                    Control_RemoveTab_Button_Delete.PerformClick();
                    return true;
                }

                if (MainFormInstance != null && !MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed &&
                    keyData == (Keys.Alt | Keys.Right))
                {
                    Control_RemoveTab_Button_Toggle_RightPanel.PerformClick();
                    return true;
                }

                if (MainFormInstance != null && MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed &&
                    keyData == (Keys.Alt | Keys.Left))
                {
                    Control_RemoveTab_Button_Toggle_RightPanel.PerformClick();
                    return true;
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

        #region Button Clicks

        private void Control_RemoveTab_Button_Toggle_InputPanel_Click(object? sender, EventArgs e)
        {
            ToggleSearchPanel();
        }

        private async void Control_RemoveTab_Button_Delete_Click(object? sender, EventArgs? e)
        {
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Preparing to delete...");

                DataGridView? dgv = Control_RemoveTab_DataGridView_Main;
                int selectedCount = dgv.SelectedRows.Count;


                if (selectedCount == 0)
                {

                    return;
                }

                // Build confirmation message showing what will be deleted
                var itemsToDelete = GetSelectedItemsToDelete(out string itemsSummary);

                string confirmMessage;
                if (itemsToDelete.Count == 1)
                {
                    var item = itemsToDelete[0];
                    confirmMessage = $"Are you sure you want to delete this item?\n\nPart ID: {item.PartID}\nLocation: {item.Location}\nQuantity: {item.Quantity}";
                }
                else
                {
                    confirmMessage = $"Are you sure you want to delete {itemsToDelete.Count} items?\n\n{itemsSummary}";
                }

                DialogResult confirmResult = Service_ErrorHandler.ShowConfirmation(
                    confirmMessage,
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                {

                    return;
                }

                // --- Only add to history and inv_transaction if actually removed ---
                _lastRemovedItems.Clear();
                StringBuilder sb = new();
                int attempted = 0;
                var removeResult = await Dao_Inventory.RemoveInventoryItemsFromDataGridViewAsync(dgv, true);


                int removedCount = 0;
                List<string> errorMessages = new();

                if (removeResult.IsSuccess)
                {
                    removedCount = removeResult.Data.RemovedCount;
                    errorMessages = removeResult.Data.ErrorMessages;
                }

                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (row.DataBoundItem is DataRowView drv)
                    {
                        attempted++;
                        Model_History_Remove item = new()
                        {
                            PartId = drv["PartID"]?.ToString() ?? "",
                            Location = drv["Location"]?.ToString() ?? "",
                            Operation = drv["Operation"]?.ToString() ?? "",
                            Quantity = TryParse(drv["Quantity"]?.ToString(), out int qty) ? qty : 0,
                            ItemType =
                                drv.Row.Table.Columns.Contains("ItemType") ? drv["ItemType"]?.ToString() ?? "" : "",
                            User = drv.Row.Table.Columns.Contains("User") ? drv["User"]?.ToString() ?? "" : "",
                            BatchNumber =
                                drv.Row.Table.Columns.Contains("BatchNumber")
                                    ? drv["BatchNumber"]?.ToString() ?? ""
                                    : "",
                            Notes = drv.Row.Table.Columns.Contains("Notes") ? drv["Notes"]?.ToString() ?? "" : "",
                            ReceiveDate =
                                drv.Row.Table.Columns.Contains("ReceiveDate") &&
                                DateTime.TryParse(drv["ReceiveDate"]?.ToString(), out DateTime dt)
                                    ? dt
                                    : DateTime.Now
                        };
                        if (removedCount > 0)
                        {
                            _lastRemovedItems.Add(item);
                            sb.AppendLine(
                                $"PartID: {item.PartId}, Location: {item.Location}, Operation: {item.Operation}, Quantity: {item.Quantity}");

                            // NOTE: Transaction history (OUT) is automatically created by stored procedure inv_inventory_Remove_Item
                            // DO NOT manually add transaction here - it would create duplicates
                        }
                    }
                }

                string summary = sb.ToString();

                if (_lastRemovedItems.Count == 0)
                {
                    string reason =
                        "No items were deleted. This may be because the selected items no longer exist in inventory, the data did not match exactly, or a database constraint prevented deletion.";
                    if (attempted == 0)
                    {
                        reason = "No items were deleted. No valid inventory rows were selected.";
                    }

                    if (errorMessages.Count > 0)
                    {
                        reason += "\n\n" + string.Join("\n", errorMessages);
                    }

                    Service_ErrorHandler.ShowConfirmation(reason, @"Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _progressHelper?.UpdateProgress(80, "Refreshing results...");
                Control_RemoveTab_Button_Search_Click(null, null);
                _progressHelper?.UpdateProgress(100, "Delete complete");

                // --- Enable Undo button if items were removed ---
                if (_lastRemovedItems.Count > 0)
                {
                    Control_RemoveTab_Button_Undo.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_Button_Delete_Click").ToString());
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private async void Control_RemoveTab_Button_Undo_Click(object? sender, EventArgs? e)
        {
            if (_lastRemovedItems.Count == 0)
            {
                return;
            }

            _progressHelper?.ShowProgress();
            _progressHelper?.UpdateProgress(10, "Restoring removed items...");

            try
            {
                foreach (Model_History_Remove item in _lastRemovedItems)
                {
                    // Pass colorCode and workOrder if available (for color-tracked parts)
                    string? colorCode = null;
                    string? workOrder = null;
                    if (item.GetType().GetProperty("ColorCode") != null)
                        colorCode = (string?)item.GetType().GetProperty("ColorCode")?.GetValue(item);
                    if (item.GetType().GetProperty("WorkOrder") != null)
                        workOrder = (string?)item.GetType().GetProperty("WorkOrder")?.GetValue(item);

                    await Dao_Inventory.AddInventoryItemAsync(
                        item.PartId,
                        item.Location,
                        item.Operation,
                        item.Quantity,
                        item.ItemType,
                        item.User,
                        item.BatchNumber,
                        "Removal reversed via Undo Button.",
                        colorCode,
                        workOrder,
                        true
                    );
                }

                _progressHelper?.UpdateProgress(80, "Refreshing results...");
                Service_ErrorHandler.ShowConfirmation(@"Undo successful. Removed items have been restored.", @"Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                _lastRemovedItems.Clear();

                Control_RemoveTab_Button_Undo.Enabled = false;

                Control_RemoveTab_Button_Search_Click(null, null);
                _progressHelper?.UpdateProgress(100, "Undo complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    controlName: nameof(Control_RemoveTab));
                    // Show warning dialog if >1000 records before loading results

            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

                    private bool ShowWarningIfTooManyRecords(DataTable results)
                    {
                        if (results.Rows.Count > 1000)
                        {
                            var confirmResult = Service_ErrorHandler.ShowConfirmation(
                                $"Query returned {results.Rows.Count} results. Continue?",
                                "Large Result Warning",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);
                            return confirmResult == DialogResult.Yes;
                        }
                        return true;
                    }

                    // Hide ColorCode/WorkOrder columns when Show All is active
                    private void HideColorWorkOrderColumnsIfShowAll(DataGridView dgv, bool showAllActive)
                    {
                        if (dgv.Columns.Contains("ColorCode"))
                            dgv.Columns["ColorCode"].Visible = !showAllActive;
                        if (dgv.Columns.Contains("WorkOrder"))
                            dgv.Columns["WorkOrder"].Visible = !showAllActive;
                    }

                    // Robust dynamic column visibility and sorting
                    private void UpdateColorWorkOrderColumnVisibilityAndSort(DataGridView dgv, DataTable results, string partId, bool showAllActive)
                    {
                        bool colorTrackedPart = Model_Application_Variables.ColorCodeParts.Contains(partId);
                        if (dgv.Columns.Contains("ColorCode"))
                            dgv.Columns["ColorCode"].Visible = colorTrackedPart && !showAllActive;
                        if (dgv.Columns.Contains("WorkOrder"))
                            dgv.Columns["WorkOrder"].Visible = colorTrackedPart && !showAllActive;

                        // Auto-sort: ColorCode ASC, Location ASC, Unknown at end
                        if (colorTrackedPart && results.Columns.Contains("ColorCode") && results.Columns.Contains("Location"))
                        {
                            try
                            {
                                DataTable sorted = SortInventoryByColorPriority(results, true);
                                if (!ReferenceEquals(sorted, results))
                                {
                                    dgv.DataSource = sorted;
                                }
                            }
                            catch (Exception ex)
                            {
                                LoggingUtility.LogApplicationError(ex);
                            }
                        }
                    }

        private void Control_RemoveTab_Button_Reset_Click()
        {
            _progressHelper?.ShowProgress();
            _progressHelper?.UpdateProgress(10, "Resetting Remove tab...");

            Control_RemoveTab_Button_Reset.Enabled = false;
            try
            {
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    _ = Control_RemoveTab_HardReset();
                }
                else
                {
                    Control_RemoveTab_SoftReset();
                }

                _progressHelper?.UpdateProgress(100, "Reset complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_Remove_Button_Reset_Click").ToString());
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private async Task Control_RemoveTab_HardReset()
        {
            Control_RemoveTab_Button_Reset.Enabled = false;
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Remove tab...");

                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                _progressHelper?.UpdateProgress(30, "Resetting data tables...");
                await Helper_UI_ComboBoxes.ResetAndRefreshAllDataTablesAsync();

                _progressHelper?.UpdateProgress(60, "Resetting suggestion fields...");
                Control_RemoveTab_TextBox_Part.Text = string.Empty;
                Control_RemoveTab_TextBox_Operation.Text = string.Empty;

                Control_RemoveTab_DataGridView_Main.DataSource = null;
                Control_RemoveTab_Image_NothingFound.Visible = false;

                Control_RemoveTab_TextBox_Part.Focus();

                Control_RemoveTab_Button_Search.Enabled = true;
                Control_RemoveTab_Button_Delete.Enabled = false;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_Remove_HardReset").ToString());
            }
            finally
            {
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                    _progressHelper?.HideProgress();
                }

                Control_RemoveTab_Button_Reset.Enabled = true;
                SetSearchPanelCollapsed(false);
            }
        }

        private void Control_RemoveTab_SoftReset()
        {
            Control_RemoveTab_Button_Reset.Enabled = false;
            try
            {
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                Control_RemoveTab_TextBox_Part.Text = string.Empty;
                Control_RemoveTab_TextBox_Operation.Text = string.Empty;

                Control_RemoveTab_DataGridView_Main.DataSource = null;
                Control_RemoveTab_Image_NothingFound.Visible = false;

                Control_RemoveTab_Button_Search.Enabled = true;
                Control_RemoveTab_Button_Delete.Enabled = false;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("MainForm_Remove_SoftReset").ToString());
            }
            finally
            {
                Control_RemoveTab_Button_Reset.Enabled = true;
                Control_RemoveTab_TextBox_Part.Focus();
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                }

                SetSearchPanelCollapsed(false);
            }
        }

        private static void Control_RemoveTab_Button_AdvancedItemRemoval_Click()
        {
            try
            {
                if (Service_Timer_VersionChecker.MainFormInstance == null)
                {

                    return;
                }

                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_UserControl_RemoveTab.Visible = false;
                }

                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_UserControl_AdvancedRemove.Visible = true;
                }

                Control_AdvancedRemove? adv = MainFormInstance?.MainForm_UserControl_AdvancedRemove;
                if (adv != null)
                {
                    if (adv.Controls.Find("Control_AdvancedRemove_ComboBox_Part", true).FirstOrDefault() is ComboBox
                        part)
                    {
                        part.SelectedIndex = 0;
                        part.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                        part.Focus();
                    }

                    if (adv.Controls.Find("Control_AdvancedRemove_ComboBox_Op", true).FirstOrDefault() is ComboBox op)
                    {
                        op.SelectedIndex = 0;
                        op.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                    }

                    if (adv.Controls.Find("Control_AdvancedRemove_ComboBox_Loc", true).FirstOrDefault() is ComboBox loc)
                    {
                        loc.SelectedIndex = 0;
                        loc.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                    }

                    if (adv.Controls.Find("Control_AdvancedRemove_SuggestionBox_User", true).FirstOrDefault() is SuggestionTextBoxWithLabel
                        user)
                    {
                        user.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_Button_AdvancedItemRemoval_Click").ToString());
            }
        }

        private static void Control_RemoveTab_Button_Normal_Click(object? sender, EventArgs? e)
        {
            try
            {
                if (Service_Timer_VersionChecker.MainFormInstance == null)
                {

                    return;
                }

                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_UserControl_RemoveTab.Visible = true;
                }

                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_UserControl_AdvancedRemove.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_Button_Normal_Click").ToString());
            }
        }

        private async void Control_RemoveTab_Button_Search_Click(object? sender, EventArgs? e)
        {
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Searching inventory...");



                string partId = Control_RemoveTab_TextBox_Part.Text?.Trim() ?? "";
                string op = Control_RemoveTab_TextBox_Operation.Text?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(partId))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a valid Part.", "Part Selection");
                    Control_RemoveTab_TextBox_Part.Focus();
                    return;
                }

                DataTable? results = null;

                if (!string.IsNullOrWhiteSpace(op))
                {
                    _progressHelper?.UpdateProgress(40,
                        "Querying by part and operation...");
                    var partOpResult = await Dao_Inventory.GetInventoryByPartIdAndOperationAsync(partId, op, true);
                    if (partOpResult.IsSuccess && partOpResult.Data != null)
                    {
                        results = partOpResult.Data;
                    }
                    else
                    {
                        LoggingUtility.LogDatabaseError(
                            partOpResult.Exception ?? new Exception(partOpResult.ErrorMessage),
                            Enum_DatabaseEnum_ErrorSeverity.Error);

                        Service_ErrorHandler.HandleException(
                            partOpResult.Exception ?? new Exception(partOpResult.ErrorMessage ?? "Failed to retrieve inventory by part and operation"),
                            Enum_ErrorSeverity.High,
                            retryAction: () => { Control_RemoveTab_Button_Search_Click(null, null); return true; },
                            contextData: new Dictionary<string, object>
                            {
                                ["PartId"] = partId,
                                ["Operation"] = op,
                                ["MethodName"] = nameof(Control_RemoveTab_Button_Search_Click)
                            },
                            controlName: nameof(Control_RemoveTab_Button_Search));
                        return;
                    }
                }
                else
                {
                    _progressHelper?.UpdateProgress(40, "Querying by part...");
                    var partResult = await Dao_Inventory.GetInventoryByPartIdAsync(partId, true);
                    if (partResult.IsSuccess && partResult.Data != null)
                    {
                        results = partResult.Data;
                    }
                    else
                    {
                        LoggingUtility.LogDatabaseError(
                            partResult.Exception ?? new Exception(partResult.ErrorMessage),
                            Enum_DatabaseEnum_ErrorSeverity.Error);

                        Service_ErrorHandler.HandleException(
                            partResult.Exception ?? new Exception(partResult.ErrorMessage ?? "Failed to retrieve inventory by part"),
                            Enum_ErrorSeverity.High,
                            retryAction: () => { Control_RemoveTab_Button_Search_Click(null, null); return true; },
                            contextData: new Dictionary<string, object>
                            {
                                ["PartId"] = partId,
                                ["MethodName"] = nameof(Control_RemoveTab_Button_Search_Click)
                            },
                            controlName: nameof(Control_RemoveTab_Button_Search));
                        return;
                    }
                }

                if (results == null)
                {
                    throw new Exception("No results returned from inventory query");
                }

                bool colorTrackedPart = Model_Application_Variables.ColorCodeParts.Contains(partId);

                // Show warning if too many records
                if (!ShowWarningIfTooManyRecords(results))
                {
                    _progressHelper?.HideProgress();
                    return;
                }

                // Optional sort by ColorCode then Location if color codes present
                if (results.Columns.Contains("ColorCode") && results.Columns.Contains("Location"))
                {
                    try
                    {
                        if (colorTrackedPart)
                        {
                            results = SortInventoryByColorPriority(results, true);
                        }
                        else
                        {
                            var dv = results.DefaultView;
                            dv.Sort = "ColorCode ASC, Location ASC"; // Multi-column sort fallback
                            results = dv.ToTable();
                        }
                    }
                    catch (Exception sortEx)
                    {
                        LoggingUtility.LogApplicationError(sortEx);
                    }
                }

                _progressHelper?.UpdateProgress(70, "Updating results...");
                Control_RemoveTab_DataGridView_Main.DataSource = results;
                Control_RemoveTab_DataGridView_Main.ClearSelection();

                // Only show columns in this order: Location, PartID, ColorCode, WorkOrder, Operation, Quantity, Notes
                string[] columnsToShow = { "Location", "PartID", "ColorCode", "WorkOrder", "Operation", "Quantity", "Notes" };
                foreach (DataGridViewColumn column in Control_RemoveTab_DataGridView_Main.Columns)
                {
                    column.Visible = columnsToShow.Contains(column.Name);
                }

                // Reorder columns
                for (int i = 0; i < columnsToShow.Length; i++)
                {
                    if (Control_RemoveTab_DataGridView_Main.Columns.Contains(columnsToShow[i]))
                    {
                        Control_RemoveTab_DataGridView_Main.Columns[columnsToShow[i]].DisplayIndex = i;
                        // Friendly headers
                        if (columnsToShow[i] == "ColorCode")
                            Control_RemoveTab_DataGridView_Main.Columns[columnsToShow[i]].HeaderText = "Color";
                        if (columnsToShow[i] == "WorkOrder")
                            Control_RemoveTab_DataGridView_Main.Columns[columnsToShow[i]].HeaderText = "Work Order";
                    }
                }

                // Hide Color/WorkOrder columns if not a color-tracked part
                if (Control_RemoveTab_DataGridView_Main.Columns.Contains("ColorCode"))
                    Control_RemoveTab_DataGridView_Main.Columns["ColorCode"].Visible = colorTrackedPart;
                if (Control_RemoveTab_DataGridView_Main.Columns.Contains("WorkOrder"))
                    Control_RemoveTab_DataGridView_Main.Columns["WorkOrder"].Visible = colorTrackedPart;

                if (!colorTrackedPart)
                {
                    // Normal theming only when not viewing color-tracked part
                    Core_Themes.ApplyThemeToDataGridView(Control_RemoveTab_DataGridView_Main);
                }
                else
                {
                    ApplyColorCodingToRows(Control_RemoveTab_DataGridView_Main);
                }
                Core_Themes.SizeDataGrid(Control_RemoveTab_DataGridView_Main);

                Control_RemoveTab_Image_NothingFound.Visible = results.Rows.Count == 0;
                _progressHelper?.UpdateProgress(100, "Search complete");
                SetSearchPanelCollapsed(true);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_Button_Search_Click").ToString());
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private void Control_RemoveTab_Button_Print_Click(object? sender, EventArgs? e)
        {
            try
            {


                if (Control_RemoveTab_DataGridView_Main is null || Control_RemoveTab_DataGridView_Main.Rows.Count == 0)
                {

                    Service_ErrorHandler.HandleValidationError(
                        "No records available to print. Run a search or perform an inventory removal first.",
                        "Print");
                    return;
                }

                Control parent = FindForm() is Control form ? form : this;
                string gridName = string.IsNullOrWhiteSpace(Control_RemoveTab_DataGridView_Main.Name)
                    ? "Remove Inventory"
                    : Control_RemoveTab_DataGridView_Main.Name;

                var dialogTask = Helper_PrintManager.ShowPrintDialogAsync(parent, Control_RemoveTab_DataGridView_Main, gridName);

                dialogTask.ContinueWith(t =>
                {
                    if (t.IsCompletedSuccessfully)
                    {

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
                                    controlName: nameof(Control_RemoveTab_Button_Print_Click))));
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
                    controlName: nameof(Control_RemoveTab_Button_Print_Click));
            }
        }

        private async void Control_RemoveTab_ContextMenuItem_Print_Click(object? sender, EventArgs e)
        {
            // Reuse existing print button logic
            Control_RemoveTab_Button_Print_Click(sender, e);
        }

        #endregion

        #region ComboBox & UI Events

        private void Control_RemoveTab_Update_ButtonStates()
        {
            try
            {
                Control_RemoveTab_Button_Search.Enabled = !string.IsNullOrWhiteSpace(Control_RemoveTab_TextBox_Part.Text);
                bool hasData = Control_RemoveTab_DataGridView_Main?.Rows.Count > 0;
                bool hasSelection = Control_RemoveTab_DataGridView_Main?.SelectedRows.Count > 0;
                Control_RemoveTab_Button_Delete.Enabled = hasData && hasSelection;
                // Print button enable/disable
                if (Control_RemoveTab_Button_Print != null)
                {
                    Control_RemoveTab_Button_Print.Enabled = hasData;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_Update_ButtonStates").ToString());
            }
        }

        private void Control_RemoveTab_OnStartup_WireUpEvents()
        {
            try
            {
                Control_RemoveTab_Button_Reset.Click += (s, e) => Control_RemoveTab_Button_Reset_Click();

                // SuggestionTextBox event handlers - simplified
                Control_RemoveTab_TextBox_Part.TextBox.TextChanged += (s, e) => Control_RemoveTab_Update_ButtonStates();
                Control_RemoveTab_TextBox_Operation.TextBox.TextChanged += (s, e) => Control_RemoveTab_Update_ButtonStates();

                Control_RemoveTab_Button_AdvancedItemRemoval.Click +=
                    (s, e) => Control_RemoveTab_Button_AdvancedItemRemoval_Click();

                if (MainFormInstance != null)
                {
                    Control_AdvancedRemove? adv = MainFormInstance.MainForm_UserControl_AdvancedRemove;
                    if (adv != null)
                    {
                        Control[] btn = adv.Controls.Find("Control_AdvancedRemove_Button_Normal", true);
                        if (btn.Length > 0 && btn[0] is Button normalBtn)
                        {
                            normalBtn.Click -= Control_RemoveTab_Button_Normal_Click;
                            normalBtn.Click += Control_RemoveTab_Button_Normal_Click;
                        }
                    }
                }

                // NOTE: SuggestionTextBoxWithLabel handles its own focus/visual states

                Control_RemoveTab_DataGridView_Main.SelectionChanged +=
                    (s, e) => Control_RemoveTab_Update_ButtonStates();

                // Also update print button state on data source change
                Control_RemoveTab_DataGridView_Main.DataSourceChanged +=
                    (s, e) => Control_RemoveTab_Update_ButtonStates();

                Control_RemoveTab_Button_Delete.Click += Control_RemoveTab_Button_Delete_Click;


            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_RemoveTab_WireUpEvents").ToString());
            }
        }

        private void Control_RemoveTab_Button_Toggle_RightPanel_Click(object sender, EventArgs e)
        {
            if (MainFormInstance != null)
            {
                bool panelCollapsed = MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed;
                MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed = !panelCollapsed;
                UpdateQuickButtonsPanelArrow(!panelCollapsed);
            }

            Helper_UI_ComboBoxes.DeselectAllComboBoxText(this);
        }

        private async void Control_RemoveTab_Button_ShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                _progressHelper?.ShowProgress("Loading all inventory...");

                // FIXED: Use inv_inventory_Get_All stored procedure instead of hardcoded SQL
                // NOTE: Don't pass progress helper to avoid displaying stored procedure warning messages
                var getAllResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "inv_inventory_Get_All",
                    null,
                    progressHelper: null);

                if (!getAllResult.IsSuccess)
                {
                    string errorMsg = !string.IsNullOrEmpty(getAllResult.ErrorMessage)
                        ? getAllResult.ErrorMessage
                        : "Unknown error occurred while loading inventory";
                    var dbException = new Exception($"Show All failed: {errorMsg}");
                    Service_ErrorHandler.HandleDatabaseError(dbException,
                        controlName: nameof(Control_RemoveTab));
                    return;
                }

                DataTable dt = getAllResult.Data ?? new DataTable();


                bool hasColorColumns = dt.Columns.Contains("ColorCode") && dt.Columns.Contains("Location");
                bool singlePartColorTracked = IsSingleColorTrackedPart(dt);

                if (hasColorColumns)
                {
                    try
                    {
                        if (singlePartColorTracked)
                        {
                            dt = SortInventoryByColorPriority(dt, true);
                        }
                        else
                        {
                            var dv = dt.DefaultView;
                            dv.Sort = "ColorCode ASC, Location ASC";
                            dt = dv.ToTable();
                        }
                    }
                    catch (Exception exSort)
                    {
                        LoggingUtility.LogApplicationError(exSort);
                    }
                }
                Control_RemoveTab_DataGridView_Main.DataSource = dt;
                Control_RemoveTab_DataGridView_Main.ClearSelection();

                // Only show columns in this order: Location, PartID, ColorCode, WorkOrder, Operation, Quantity, Notes
                string[] columnsToShow = { "Location", "PartID", "ColorCode", "WorkOrder", "Operation", "Quantity", "Notes" };
                foreach (DataGridViewColumn column in Control_RemoveTab_DataGridView_Main.Columns)
                {
                    column.Visible = columnsToShow.Contains(column.Name);
                }

                // Reorder columns
                for (int i = 0; i < columnsToShow.Length; i++)
                {
                    if (Control_RemoveTab_DataGridView_Main.Columns.Contains(columnsToShow[i]))
                    {
                        Control_RemoveTab_DataGridView_Main.Columns[columnsToShow[i]].DisplayIndex = i;
                        // Friendly headers
                        if (columnsToShow[i] == "ColorCode")
                            Control_RemoveTab_DataGridView_Main.Columns[columnsToShow[i]].HeaderText = "Color";
                        if (columnsToShow[i] == "WorkOrder")
                            Control_RemoveTab_DataGridView_Main.Columns[columnsToShow[i]].HeaderText = "Work Order";
                    }
                }

                // Show-all may contain multiple parts; only apply color theming if ALL rows are the same flagged part
                if (!singlePartColorTracked)
                {
                    Core_Themes.ApplyThemeToDataGridView(Control_RemoveTab_DataGridView_Main);
                }
                else
                {
                    ApplyColorCodingToRows(Control_RemoveTab_DataGridView_Main);
                }
                Core_Themes.SizeDataGrid(Control_RemoveTab_DataGridView_Main);
                Control_RemoveTab_Image_NothingFound.Visible = dt.Rows.Count == 0;
                SetSearchPanelCollapsed(true);

                // Show success message to replace any warning/error from stored procedure
                if (dt.Rows.Count == 0)
                {
                    _progressHelper?.ShowSuccess("No inventory items found");
                }
                else
                {
                    _progressHelper?.ShowSuccess($"Loaded {dt.Rows.Count} inventory items");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleDatabaseError(ex,
                    controlName: nameof(Control_RemoveTab));
                _progressHelper?.ShowError("Failed to load inventory");
            }
        }

        #endregion

        #region Helpers

        private static readonly HashSet<string> PredefinedColorCodes = new(StringComparer.OrdinalIgnoreCase)
        {
            "Red","Blue","Green","Yellow","Orange","Purple","Pink","White","Black"
        };

        private static int GetColorSortGroup(string? colorCode)
        {
            if (string.IsNullOrWhiteSpace(colorCode))
            {
                return 2; // Treat blanks as unknown
            }

            if (colorCode.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
            {
                return 2;
            }

            return PredefinedColorCodes.Contains(colorCode) ? 0 : 1;
        }

        private static DataTable SortInventoryByColorPriority(DataTable source, bool colorTrackedPart)
        {
            if (!colorTrackedPart)
            {
                return source;
            }

            if (!source.Columns.Contains("ColorCode") || !source.Columns.Contains("Location") || source.Rows.Count == 0)
            {
                return source;
            }

            DataTable sortedTable = source.Clone();
            foreach (DataRow row in source.AsEnumerable()
                         .OrderBy(r => GetColorSortGroup(r["ColorCode"]?.ToString()))
                         .ThenBy(r => r["ColorCode"]?.ToString(), StringComparer.OrdinalIgnoreCase)
                         .ThenBy(r => r["Location"]?.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                sortedTable.ImportRow(row);
            }

            return sortedTable;
        }

        private static bool IsSingleColorTrackedPart(DataTable table)
        {
            if (!table.Columns.Contains("PartID") || table.Rows.Count == 0)
            {
                return false;
            }

            string firstPart = table.Rows[0]["PartID"]?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(firstPart))
            {
                return false;
            }

            if (!Model_Application_Variables.ColorCodeParts.Contains(firstPart))
            {
                return false;
            }

            return table.AsEnumerable().All(row => string.Equals(
                row["PartID"]?.ToString() ?? string.Empty,
                firstPart,
                StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Applies background coloring to rows based on ColorCode column.
        /// Skips 'Unknown' and any non-predefined (user-defined) colors.
        /// Uses light variants for dark colors to keep text readable.
        /// </summary>
        /// <param name="dgv">Target DataGridView</param>
        private void ApplyColorCodingToRows(DataGridView dgv)
        {
            try
            {
                if (dgv.Columns.Contains("ColorCode") == false)
                    return;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    string colorCode = row.Cells["ColorCode"].Value?.ToString() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(colorCode)) continue;
                    if (colorCode.Equals("Unknown", StringComparison.OrdinalIgnoreCase)) continue;
                    if (!PredefinedColorCodes.Contains(colorCode)) continue; // user-defined -> skip

                    Color backColor;
                    switch (colorCode.ToLowerInvariant())
                    {
                        case "red": backColor = Color.MistyRose; break;
                        case "blue": backColor = Color.AliceBlue; break;
                        case "green": backColor = Color.Honeydew; break;
                        case "yellow": backColor = Color.LightYellow; break;
                        case "orange": backColor = Color.Moccasin; break;
                        case "purple": backColor = Color.Lavender; break;
                        case "pink": backColor = Color.LavenderBlush; break;
                        case "white": backColor = Color.WhiteSmoke; break;
                        case "black": backColor = Color.Gainsboro; break; // light gray for readability
                        default: continue;
                    }

                    row.DefaultCellStyle.BackColor = backColor;
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(Math.Max(0, backColor.R - 60), Math.Max(0, backColor.G - 60), Math.Max(0, backColor.B - 60)); // Darker shade for visible selection
                    row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                    row.DefaultCellStyle.SelectionForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private List<(string PartID, string Location, int Quantity)> GetSelectedItemsToDelete(out string summary)
        {
            StringBuilder sb = new();
            List<(string PartID, string Location, int Quantity)> itemsToDelete = new();
            foreach (DataGridViewRow row in Control_RemoveTab_DataGridView_Main.SelectedRows)
            {
                if (row.DataBoundItem is DataRowView drv)
                {
                    string partId = drv["PartID"]?.ToString() ?? "";
                    string location = drv["Location"]?.ToString() ?? "";
                    string quantityStr = drv["Quantity"]?.ToString() ?? "";
                    if (!TryParse(quantityStr, out int quantity))
                    {
                        LoggingUtility.LogApplicationError(new Exception(
                            $"Invalid quantity value: '{quantityStr}' for PartID={partId}, Location={location}"));
                        continue;
                    }

                    LoggingUtility.Log(
                        $"Selected for deletion: PartID={partId}, Location={location}, Quantity={quantity}");

                    itemsToDelete.Add((partId, location, quantity));
                }
            }

            // Create concise summary - group by Part ID for multiple items
            if (itemsToDelete.Count > 1)
            {
                var groupedByPart = itemsToDelete
                    .GroupBy(x => x.PartID)
                    .Select(g => new
                    {
                        PartID = g.Key,
                        LocationCount = g.Count(),
                        TotalQuantity = g.Sum(x => x.Quantity)
                    })
                    .OrderBy(x => x.PartID)
                    .ToList();

                foreach (var group in groupedByPart)
                {
                    sb.AppendLine($"PartID: {group.PartID}, Location(s): {group.LocationCount}, Quantity: {group.TotalQuantity}");
                }
            }
            else if (itemsToDelete.Count == 1)
            {
                var item = itemsToDelete[0];
                sb.AppendLine($"PartID: {item.PartID}, Location: {item.Location}, Quantity: {item.Quantity}");
            }

            summary = sb.ToString();
            return itemsToDelete;
        }

        private void InitializeQuickButtonsPanelAnimator()
        {
            try
            {
                Helper_ButtonToggleAnimations.ValidateIconButton(
                    Control_RemoveTab_Button_Toggle_RightPanel,
                    nameof(Control_RemoveTab));

                bool collapsed = MainFormInstance?.MainForm_SplitContainer_Middle.Panel2Collapsed ?? false;
                UpdateQuickButtonsPanelArrow(collapsed);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void UpdateQuickButtonsPanelArrow(bool panelCollapsed)
        {
            Helper_ButtonToggleAnimations.ApplyHorizontalArrow(
                ref _quickButtonsPanelAnimator,
                components,
                Control_RemoveTab_Button_Toggle_RightPanel,
                panelCollapsed);
        }

        internal void SyncQuickButtonsPanelState(bool panelCollapsed)
        {
            UpdateQuickButtonsPanelArrow(panelCollapsed);
        }

        private void InitializeSearchPanelToggleAnimator()
        {
            try
            {
                Helper_ButtonToggleAnimations.ValidateIconButton(
                    Control_RemoveTab_Button_Toggle_InputPanel,
                    nameof(Control_RemoveTab));

                UpdateSearchPanelToggleVisual(_isSearchPanelCollapsed);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void ToggleSearchPanel()
        {
            SetSearchPanelCollapsed(!_isSearchPanelCollapsed);
        }

        private void SetSearchPanelCollapsed(bool collapse)
        {
            _isSearchPanelCollapsed = collapse;

            if (Control_RemoveTab_Panel_Header != null)
            {
                Control_RemoveTab_Panel_Header.Visible = !collapse;
            }

            UpdateSearchPanelToggleVisual(collapse);

            if (!collapse)
            {
                Control_RemoveTab_TextBox_Part?.Focus();
            }
        }

        private void UpdateSearchPanelToggleVisual(bool collapsed)
        {
            Helper_ButtonToggleAnimations.ApplyAnimationState(
                ref _searchPanelAnimator,
                components,
                Control_RemoveTab_Button_Toggle_InputPanel,
                collapsed,
                TextAnimationPreset.Down,
                TextAnimationPreset.Up,
                Helper_ButtonToggleAnimations.ArrowDown,
                Helper_ButtonToggleAnimations.ArrowUp);
            _searchPanelToggleToolTip?.SetToolTip(
                Control_RemoveTab_Button_Toggle_InputPanel,
                collapsed ? "Expand search panel" : "Collapse search panel");
        }

        #endregion

        #endregion
    }

    #endregion
}
