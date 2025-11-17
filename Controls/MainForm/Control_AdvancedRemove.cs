using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.MainForm.Classes;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    public partial class Control_AdvancedRemove : UserControl
    {
        #region Fields

        private readonly List<Model_History_Remove> _lastRemovedItems = [];
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }
        private Helper_StoredProcedureProgress? _progressHelper;
        private Control_TextAnimationSequence? _sidePanelAnimator;
        private Control_TextAnimationSequence? _quickButtonsAnimator;
        private bool _isInputPanelCollapsed = false;

        #endregion

        #region Progress Control Methods

        /// <summary>
        /// Set progress controls for visual feedback during operations
        /// </summary>
        /// <param name="progressBar">Progress bar control</param>
        /// <param name="statusLabel">Status label control</param>
        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel,
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Constructors

        public Control_AdvancedRemove()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_AdvancedRemove),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_AdvancedRemove), nameof(Control_AdvancedRemove));

            Service_DebugTracer.TraceUIAction("ADVANCED_REMOVE_INITIALIZATION", nameof(Control_AdvancedRemove),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_AdvancedRemove),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });
            // Apply comprehensive DPI scaling and runtime layout adjustments
            // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
            // Do NOT call theme update methods from arbitrary event handlers or business logic.

            Service_DebugTracer.TraceUIAction("CONTROL_INITIALIZATION", nameof(Control_AdvancedRemove),
                new Dictionary<string, object>
                {
                    ["Components"] = new[] { "Initialize", "ComboBoxProperties", "ComboBoxEvents", "FocusHighlighting" }
                });
            // No longer need to initialize or wire up removed ComboBoxes or Like controls
            Control_AdvancedRemove_Initialize();
            ApplyStandardComboBoxProperties();
            WireUpComboBoxEvents();
            Core_Themes.ApplyFocusHighlighting(this);

            Service_DebugTracer.TraceUIAction("BUTTON_EVENTS_SETUP", nameof(Control_AdvancedRemove),
                new Dictionary<string, object>
                {
                    ["Buttons"] = new[] { "Normal", "Undo", "Search" },
                    ["EventRewiring"] = true
                });
            Control[] btn = Controls.Find("Control_AdvancedRemove_Button_Normal", true);
            if (btn.Length > 0 && btn[0] is Button normalBtn)
            {
                normalBtn.Click -= Control_AdvancedRemove_Button_Normal_Click;
                normalBtn.Click += Control_AdvancedRemove_Button_Normal_Click;
                ToolTip toolTip = new();
                toolTip.SetToolTip(normalBtn,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Normal)}");
            }

            Control[] undoBtn = Controls.Find("Control_AdvancedRemove_Button_Undo", true);
            if (undoBtn.Length > 0 && undoBtn[0] is Button undoButton)
            {
                undoButton.Click -= Control_AdvancedRemove_Button_Undo_Click;
                undoButton.Click += Control_AdvancedRemove_Button_Undo_Click;
                ToolTip toolTip = new();
                toolTip.SetToolTip(undoButton,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Undo)}");
            }

            Control[] searchBtn = Controls.Find("Control_AdvancedRemove_Button_Search", true);
            if (searchBtn.Length > 0 && searchBtn[0] is Button searchButton)
            {
                searchButton.Click -= Control_AdvancedRemove_Button_Search_Click;
                searchButton.Click += Control_AdvancedRemove_Button_Search_Click;
                ToolTip toolTip = new();
                toolTip.SetToolTip(searchButton,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Search)}");
            }

            Control[] resetBtn = Controls.Find("Control_AdvancedRemove_Button_Reset", true);
            if (resetBtn.Length > 0 && resetBtn[0] is Button resetButton)
            {
                resetButton.Click -= Control_AdvancedRemove_Button_Reset_Click;
                resetButton.Click += Control_AdvancedRemove_Button_Reset_Click;
                ToolTip toolTip = new();
                toolTip.SetToolTip(resetButton,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Reset)}");
            }

            Control[] deleteBtn = Controls.Find("Control_AdvancedRemove_Button_Delete", true);
            if (deleteBtn.Length > 0 && deleteBtn[0] is Button deleteButton)
            {
                deleteButton.Click -= Control_AdvancedRemove_Button_Delete_Click;
                deleteButton.Click += Control_AdvancedRemove_Button_Delete_Click;
                ToolTip toolTip = new();
                toolTip.SetToolTip(deleteButton,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Remove_Delete)}");
            }

            Control[] printBtn = Controls.Find("Control_AdvancedRemove_Button_Print", true);
            if (printBtn.Length > 0 && printBtn[0] is Button printButton)
            {
                printButton.Click -= Control_AdvancedRemove_Button_Print_Click;
                printButton.Click += Control_AdvancedRemove_Button_Print_Click;
                ToolTip toolTip = new();
                toolTip.SetToolTip(printButton, "Print the current results");
            }

            // Wire up date range radio button events
            WireUpDateRangeRadioButtons();

            // Initialize date range defaults (sets Month as default)
            InitializeDateRangeDefaults();

            InitializeSidePanelAnimator();
            InitializeQuickButtonsToggle();

            _ = LoadComboBoxesAsync();
        }

        #endregion

        #region Methods

        private static void Control_AdvancedRemove_Button_Normal_Click(object? sender, EventArgs? e)
        {
            try
            {
                if (Services.Service_Timer_VersionChecker.MainFormInstance == null)
                {
                    LoggingUtility.Log("MainForm instance is null, cannot return to normal Remove tab.");
                    return;
                }

                if (Control_RemoveTab.MainFormInstance != null)
                {
                    Control_RemoveTab.MainFormInstance.MainForm_UserControl_RemoveTab.Visible = true;
                }

                if (Control_RemoveTab.MainFormInstance != null)
                {
                    Control_RemoveTab.MainFormInstance.MainForm_UserControl_AdvancedRemove.Visible = false;
                }

                Control_RemoveTab? removeTab = Control_RemoveTab.MainFormInstance?.MainForm_UserControl_RemoveTab;
                if (removeTab != null)
                {
                    if (removeTab.Controls.Find("Control_RemoveTab_TextBox_Part", true)
                            .FirstOrDefault() is SuggestionTextBoxWithLabel part)
                    {
                        part.Text = string.Empty;
                        part.TextBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                        part.Focus();
                    }

                    if (removeTab.Controls.Find("Control_RemoveTab_TextBox_Operation", true)
                            .FirstOrDefault() is SuggestionTextBoxWithLabel op)
                    {
                        op.Text = string.Empty;
                        op.TextBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    new StringBuilder().Append("Control_AdvancedRemove_Button_Normal_Click").ToString());
            }
        }

        private void Control_AdvancedRemove_Initialize()
        {
            // No longer need to set ComboBox colors for removed controls
        }

        /// <summary>
        /// Wires up event handlers for date range radio buttons.
        /// </summary>
        private void WireUpDateRangeRadioButtons()
        {
            Control_AdvancedRemove_RadioButton_Today.CheckedChanged += QuickFilterChanged;
            Control_AdvancedRemove_RadioButton_Week.CheckedChanged += QuickFilterChanged;
            Control_AdvancedRemove_RadioButton_Month.CheckedChanged += QuickFilterChanged;
            Control_AdvancedRemove_RadioButton_Custom.CheckedChanged += QuickFilterChanged;
            Control_AdvancedRemove_RadioButton_Everything.CheckedChanged += QuickFilterChanged;
        }

        /// <summary>
        /// Sets default date range to current month.
        /// </summary>
        private void InitializeDateRangeDefaults()
        {
            Control_AdvancedRemove_RadioButton_Month.Checked = true;
            ApplyQuickFilter();
        }

        private void ApplyStandardComboBoxProperties()
        {
            // No standard ComboBoxes to configure - using SuggestionTextBoxWithLabel for Users
        }

        private async Task LoadComboBoxesAsync()
        {
            try
            {
                // Configure SuggestionTextBoxWithLabel for Users
                Helper_SuggestionTextBox.ConfigureForUsers(
                    Control_AdvancedRemove_SuggestionBox_User,
                    Helper_SuggestionTextBox.GetCachedUsersAsync);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void WireUpComboBoxEvents()
        {
            // User selection event for SuggestionTextBoxWithLabel
            Control_AdvancedRemove_SuggestionBox_User.TextBox.Enter += (s, e) =>
                Control_AdvancedRemove_SuggestionBox_User.BackColor =
                    Model_Application_Variables.UserUiColors.ControlFocusedBackColor ?? Color.LightBlue;

            Control_AdvancedRemove_SuggestionBox_User.TextBox.Leave += (s, e) =>
                Control_AdvancedRemove_SuggestionBox_User.BackColor =
                    Model_Application_Variables.UserUiColors.ControlBackColor ?? SystemColors.Window;

            Control_AdvancedRemove_DataGridView_Results.SelectionChanged +=
                (s, e) => Control_AdvancedRemove_Update_ButtonStates();
            Control_AdvancedRemove_DataGridView_Results.DataSourceChanged +=
                (s, e) => Control_AdvancedRemove_Update_ButtonStates();
        }

        #endregion

        #region Date Range Quick Filter Handlers

        /// <summary>
        /// Handles CheckedChanged event for date range radio buttons.
        /// </summary>
        private void QuickFilterChanged(object? sender, EventArgs e)
        {
            try
            {
                if (sender is RadioButton rdo && rdo.Checked)
                {
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Quick filter changed: {rdo.Text}");
                    ApplyQuickFilter();
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    controlName: nameof(Control_AdvancedRemove));
            }
        }

        /// <summary>
        /// Applies the selected quick filter to the date range pickers.
        /// Same logic as TransactionSearchControl for consistency.
        /// </summary>
        private void ApplyQuickFilter()
        {
            try
            {
                var now = DateTime.Now;

                if (Control_AdvancedRemove_RadioButton_Today.Checked)
                {
                    // Today: 00:00 to 23:59
                    Control_AdvancedRemove_DateTimePicker_From.Value = now.Date;
                    Control_AdvancedRemove_DateTimePicker_To.Value = now.Date.AddDays(1).AddSeconds(-1);
                    Control_AdvancedRemove_DateTimePicker_From.Enabled = false;
                    Control_AdvancedRemove_DateTimePicker_To.Enabled = false;
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Quick filter applied: Today");
                }
                else if (Control_AdvancedRemove_RadioButton_Week.Checked)
                {
                    // This Week: Monday to Sunday
                    int daysFromMonday = ((int)now.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                    DateTime monday = now.Date.AddDays(-daysFromMonday);
                    DateTime sunday = monday.AddDays(6);

                    Control_AdvancedRemove_DateTimePicker_From.Value = monday;
                    Control_AdvancedRemove_DateTimePicker_To.Value = sunday.AddDays(1).AddSeconds(-1);
                    Control_AdvancedRemove_DateTimePicker_From.Enabled = false;
                    Control_AdvancedRemove_DateTimePicker_To.Enabled = false;
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Quick filter applied: Week");
                }
                else if (Control_AdvancedRemove_RadioButton_Month.Checked)
                {
                    // This Month: 1st to last day
                    DateTime firstDay = new DateTime(now.Year, now.Month, 1);
                    DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

                    Control_AdvancedRemove_DateTimePicker_From.Value = firstDay;
                    Control_AdvancedRemove_DateTimePicker_To.Value = lastDay.AddDays(1).AddSeconds(-1);
                    Control_AdvancedRemove_DateTimePicker_From.Enabled = false;
                    Control_AdvancedRemove_DateTimePicker_To.Enabled = false;
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Quick filter applied: Month");
                }
                else if (Control_AdvancedRemove_RadioButton_Everything.Checked)
                {
                    // Everything: No date filtering - set to wide range (past 10 years to future 1 year)
                    Control_AdvancedRemove_DateTimePicker_From.Value = now.AddYears(-10);
                    Control_AdvancedRemove_DateTimePicker_To.Value = now.AddYears(1);
                    Control_AdvancedRemove_DateTimePicker_From.Enabled = false;
                    Control_AdvancedRemove_DateTimePicker_To.Enabled = false;
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Quick filter applied: Everything (all dates)");
                }
                else if (Control_AdvancedRemove_RadioButton_Custom.Checked)
                {
                    // Custom: user sets dates manually, enable date pickers
                    Control_AdvancedRemove_DateTimePicker_From.Enabled = true;
                    Control_AdvancedRemove_DateTimePicker_To.Enabled = true;
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Quick filter applied: Custom (user-defined dates)");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    controlName: nameof(Control_AdvancedRemove));
            }
        }

        private async void Control_AdvancedRemove_Button_Search_Click(object? sender, EventArgs? e)
        {
            try
            {
                // Gather search fields from textboxes and user combobox
                string part = Control_AdvancedRemove_TextBox_Part.Text.Trim();
                string op = Control_AdvancedRemove_TextBox_Operation.Text.Trim();
                string loc = Control_AdvancedRemove_TextBox_Location.Text.Trim();
                string qtyMinText = Control_AdvancedRemove_TextBox_QtyMin.Text.Trim();
                string qtyMaxText = Control_AdvancedRemove_TextBox_QtyMax.Text.Trim();
                string notes = Control_AdvancedRemove_TextBox_Notes.Text.Trim();
                string user = Control_AdvancedRemove_SuggestionBox_User?.Text?.Trim() ?? string.Empty;

                DateTime? dateFrom =
                    Control_AdvancedRemove_DateTimePicker_From.Value.Date;
                DateTime? dateTo = Control_AdvancedRemove_DateTimePicker_To.Value.Date.AddDays(1).AddTicks(-1);

                int? qtyMin = int.TryParse(qtyMinText, out int qmin) ? qmin : null;
                int? qtyMax = int.TryParse(qtyMaxText, out int qmax) ? qmax : null;

                bool userSelected = !string.IsNullOrWhiteSpace(user);

                bool anyFieldFilled =
                    !string.IsNullOrWhiteSpace(part) ||
                    !string.IsNullOrWhiteSpace(op) ||
                    !string.IsNullOrWhiteSpace(loc) ||
                    qtyMin.HasValue ||
                    qtyMax.HasValue ||
                    !string.IsNullOrWhiteSpace(notes) ||
                    userSelected ||
                    (dateFrom != null && dateTo != null);

                if (!anyFieldFilled)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please fill in at least one field to search.",
                        "Search Criteria",
                        controlName: nameof(Control_AdvancedRemove));
                    return;
                }

                // Skip date range validation when "Everything" is selected
                if (!Control_AdvancedRemove_RadioButton_Everything.Checked)
                {
                    // Validate date range for non-Everything searches
                    if (dateFrom > dateTo)
                    {
                        Service_ErrorHandler.HandleValidationError(
                            "The 'From' date cannot be after the 'To' date.",
                            "Date Range Validation",
                            controlName: nameof(Control_AdvancedRemove));
                        return;
                    }
                }

                // FIXED: Use Helper_Database_StoredProcedure instead of hardcoded MySQL query
                var searchParameters = new Dictionary<string, object>
                {
                    { "PartID", string.IsNullOrWhiteSpace(part) ? (object)DBNull.Value : part },
                    { "Operation", string.IsNullOrWhiteSpace(op) ? (object)DBNull.Value : op },
                    { "Location", string.IsNullOrWhiteSpace(loc) ? (object)DBNull.Value : loc },
                    { "QtyMin", qtyMin ?? (object)DBNull.Value },
                    { "QtyMax", qtyMax ?? (object)DBNull.Value },
                    { "Notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes },
                    { "User", userSelected ? user : (object)DBNull.Value },
                    { "FilterByDate", !Control_AdvancedRemove_RadioButton_Everything.Checked },
                    { "DateFrom", dateFrom ?? (object)DBNull.Value },
                    { "DateTo", dateTo ?? (object)DBNull.Value }
                };

                if (Control_AdvancedRemove_RadioButton_Everything.Checked)
                {
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Executing search (Everything mode). " +
                        $"PartID='{part}', Operation='{op}', Location='{loc}', " +
                        $"QtyMin={qtyMin}, QtyMax={qtyMax}, Notes='{notes}', User='{(userSelected ? user : "NULL")}'");
                }
                else
                {
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Executing search with date range. " +
                        $"DateRange={dateFrom:MM/dd/yy} - {dateTo:MM/dd/yy}, PartID='{part}', Operation='{op}', " +
                        $"Location='{loc}', QtyMin={qtyMin}, QtyMax={qtyMax}, Notes='{notes}', User='{(userSelected ? user : "NULL")}'");
                }

                var searchResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "inv_inventory_Search_Advanced",
                    searchParameters,
                    _progressHelper);

                if (!searchResult.IsSuccess)
                {
                    string errorMsg = !string.IsNullOrEmpty(searchResult.ErrorMessage)
                        ? searchResult.ErrorMessage
                        : "Unknown error occurred during search";
                    Service_ErrorHandler.ShowWarning($"Search failed: {errorMsg}");
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Search failed: {errorMsg}");
                    return;
                }

                DataTable dt = searchResult.Data ?? new DataTable();
                LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Search completed. IsSuccess: {searchResult.IsSuccess}, Rows: {dt.Rows.Count}");

                Control_AdvancedRemove_DataGridView_Results.DataSource = dt;
                Control_AdvancedRemove_DataGridView_Results.ClearSelection();
                // Only show columns in this order: Location, PartID, Operation, Quantity, Notes
                string[] columnsToShow = { "Location", "PartID", "Operation", "Quantity", "Notes" };
                foreach (DataGridViewColumn column in Control_AdvancedRemove_DataGridView_Results.Columns)
                {
                    column.Visible = columnsToShow.Contains(column.Name);
                }

                // Reorder columns
                for (int i = 0; i < columnsToShow.Length; i++)
                {
                    if (Control_AdvancedRemove_DataGridView_Results.Columns.Contains(columnsToShow[i]))
                    {
                        Control_AdvancedRemove_DataGridView_Results.Columns[columnsToShow[i]].DisplayIndex = i;
                    }
                }

                Core_Themes.ApplyThemeToDataGridView(Control_AdvancedRemove_DataGridView_Results);
                Core_Themes.SizeDataGrid(Control_AdvancedRemove_DataGridView_Results);
                Control_AdvancedRemove_Image_NothingFound.Visible = dt.Rows.Count == 0;
                Control_AdvancedRemove_Update_ButtonStates();

                // Collapse the search panel after search to maximize results area
                try
                {
                    if (Control_AdvancedRemove_SplitView != null)
                    {
                        Control_AdvancedRemove_SplitView.Panel1Collapsed = true;
                        UpdateSidePanelArrow(true);

                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["Operation"] = "AdvancedSearch"
                    },
                    controlName: nameof(Control_AdvancedRemove));
            }
        }

        private async void Control_AdvancedRemove_Button_Delete_Click(object? sender, EventArgs? e)
        {
            try
            {
                DataGridView? dgv = Control_AdvancedRemove_DataGridView_Results;
                int selectedCount = dgv.SelectedRows.Count;
                LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Delete clicked. Selected rows: {selectedCount}");
                if (selectedCount == 0)
                {
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] No rows selected for deletion.");
                    return;
                }

                // Gather deleted items for undo
                _lastRemovedItems.Clear();
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    string partId = row.Cells["PartID"].Value?.ToString() ?? "";
                    string location = row.Cells["Location"].Value?.ToString() ?? "";
                    string operation = row.Cells["Operation"].Value?.ToString() ?? "";
                    int quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int q) ? q : 0;
                    string itemType = row.Cells["ItemType"]?.Value?.ToString() ?? ""; // If you have this column
                    string user = row.Cells["User"]?.Value?.ToString() ?? "";
                    string batchNumber = row.Cells["BatchNumber"]?.Value?.ToString() ?? "";

                    _lastRemovedItems.Add(new Model_History_Remove
                    {
                        PartId = partId,
                        Location = location,
                        Operation = operation,
                        Quantity = quantity,
                        ItemType = itemType,
                        User = user,
                        BatchNumber = batchNumber
                    });
                }

                string confirmMessage;
                if (_lastRemovedItems.Count == 1)
                {
                    var item = _lastRemovedItems[0];
                    confirmMessage = $"Are you sure you want to delete this item?\n\nPart ID: {item.PartId}\nLocation: {item.Location}\nOperation: {item.Operation}\nQuantity: {item.Quantity}";
                }
                else
                {
                    // Group by Part ID for cleaner summary
                    var groupedItems = _lastRemovedItems
                        .GroupBy(x => x.PartId)
                        .Select(g => new { PartId = g.Key, Count = g.Count(), TotalQty = g.Sum(x => x.Quantity) })
                        .ToList();

                    StringBuilder sb = new();
                    foreach (var group in groupedItems)
                    {
                        sb.AppendLine($"  • {group.PartId}: {group.Count} location(s), {group.TotalQty} total quantity");
                    }

                    confirmMessage = $"Are you sure you want to delete {_lastRemovedItems.Count} items?\n\n{sb}";
                }

                DialogResult confirmResult = MessageBox.Show(
                    confirmMessage,
                    @"Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                {
                    LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] User cancelled deletion.");
                    _lastRemovedItems.Clear();
                    return;
                }

                var removeResult = await Dao_Inventory.RemoveInventoryItemsFromDataGridViewAsync(dgv);
                LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Remove operation result: Success={removeResult.IsSuccess}, Message={removeResult.StatusMessage}");

                if (removeResult.IsSuccess)
                {
                    int removedCount = removeResult.Data.RemovedCount;
                    List<string> errorMessages = removeResult.Data.ErrorMessages;

                    if (removedCount == 0)
                    {
                        string msg =
                            "No items were deleted. This may be because the selected items no longer exist in inventory, the data did not match exactly, or a database constraint prevented deletion.";
                        if (errorMessages.Count > 0)
                        {
                            msg += "\n\n" + string.Join("\n", errorMessages);
                        }

                        Service_ErrorHandler.ShowWarning(msg);
                        Control_AdvancedRemove_Button_Undo.Enabled = false;
                    }
                    else
                    {
                        // Enable Undo button if items were deleted
                        Control_AdvancedRemove_Button_Undo.Enabled = true;
                    }
                }
                else
                {
                    string errorMsg = !string.IsNullOrEmpty(removeResult.ErrorMessage)
                        ? removeResult.ErrorMessage
                        : "Unknown error occurred during removal";
                    Service_ErrorHandler.ShowWarning($"Error removing items: {errorMsg}");
                    Control_AdvancedRemove_Button_Undo.Enabled = false;
                }

                Control_AdvancedRemove_Button_Search_Click(null, null);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex);
            }
        }

        private async Task Control_AdvancedRemove_HardReset()
        {
            // Expand input panel when hard reset is triggered
            ExpandInputPanel();

            Control[] resetBtn = Controls.Find("Control_AdvancedRemove_Button_Reset", true);
            if (resetBtn.Length > 0 && resetBtn[0] is Button btn)
            {
                btn.Enabled = false;
            }

            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10,
                    "Resetting Advanced Remove tab...");
                Debug.WriteLine("[DEBUG] AdvancedRemove HardReset - start");
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for hard reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                _progressHelper?.UpdateProgress(30,
                    "Resetting data tables...");
                Debug.WriteLine("[DEBUG] Hiding ComboBoxes");

                Debug.WriteLine("[DEBUG] Resetting and refreshing all ComboBox DataTables");
                await Helper_UI_ComboBoxes.ResetAndRefreshAllDataTablesAsync();
                Debug.WriteLine("[DEBUG] DataTables reset complete");

                _progressHelper?.UpdateProgress(60,
                    "Refilling combo boxes...");
                Debug.WriteLine("[DEBUG] Reloading User data");
                await Helper_UI_ComboBoxes.SetupUserDataTable();
                Helper_SuggestionTextBox.Clear(Control_AdvancedRemove_SuggestionBox_User.TextBox);

                Control_AdvancedRemove_TextBox_QtyMin.Text = string.Empty;
                Control_AdvancedRemove_TextBox_QtyMax.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Notes.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Part.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Operation.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Location.Text = string.Empty;

                // Reset date range to Month (default)
                Control_AdvancedRemove_RadioButton_Month.Checked = true;
                // ApplyQuickFilter() is called automatically by CheckedChanged event

                Control_AdvancedRemove_DataGridView_Results.DataSource = null;
                Control_AdvancedRemove_DataGridView_Results.Rows.Clear();
                Control_AdvancedRemove_Image_NothingFound.Visible = false;

                if (Control_AdvancedRemove_SuggestionBox_User.FindForm() is { } form)
                {
                    MainFormControlHelper.SetActiveControl(form, Control_AdvancedRemove_SuggestionBox_User);
                }

                // Expand the search panel on reset
                try
                {
                    if (Control_AdvancedRemove_SplitView != null)
                    {
                        Control_AdvancedRemove_SplitView.Panel1Collapsed = false;
                        UpdateSidePanelArrow(false);
                        Control_AdvancedRemove_Button_SidePanel.ForeColor =
                            Model_Application_Variables.UserUiColors.SuccessColor ?? Color.Green;
                    }
                }
                catch { }

                Debug.WriteLine("[DEBUG] AdvancedRemove HardReset - end");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedRemove HardReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "Control_AdvancedRemove_HardReset");
            }
            finally
            {
                Debug.WriteLine("[DEBUG] AdvancedRemove HardReset button re-enabled");
                if (resetBtn.Length > 0 && resetBtn[0] is Button btn2)
                {
                    btn2.Enabled = true;
                }

                if (Control_RemoveTab.MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Restoring status strip after hard reset");
                    Control_RemoveTab.MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    Control_RemoveTab.MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    Control_RemoveTab.MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                    _progressHelper?.HideProgress();
                }
            }
        }

        private void Control_AdvancedRemove_SoftReset()
        {
            // Expand input panel when soft reset is triggered
            ExpandInputPanel();

            Control[] resetBtn = Controls.Find("Control_AdvancedRemove_Button_Reset", true);
            if (resetBtn.Length > 0 && resetBtn[0] is Button btn)
            {
                btn.Enabled = false;
            }

            try
            {
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for Soft Reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                Control_AdvancedRemove_SuggestionBox_User.Text = string.Empty;
                Control_AdvancedRemove_TextBox_QtyMin.Text = string.Empty;
                Control_AdvancedRemove_TextBox_QtyMin.ForeColor =
                    Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                Control_AdvancedRemove_TextBox_QtyMax.Text = string.Empty;
                Control_AdvancedRemove_TextBox_QtyMax.ForeColor =
                    Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                Control_AdvancedRemove_TextBox_Notes.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Notes.ForeColor =
                    Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                Control_AdvancedRemove_TextBox_Part.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Part.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                Control_AdvancedRemove_TextBox_Operation.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Operation.ForeColor =
                    Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                Control_AdvancedRemove_TextBox_Location.Text = string.Empty;
                Control_AdvancedRemove_TextBox_Location.ForeColor =
                    Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;

                // Reset date range to Month (default)
                Control_AdvancedRemove_RadioButton_Month.Checked = true;
                // ApplyQuickFilter() is called automatically by CheckedChanged event

                // Reset DataGridView as well
                Control_AdvancedRemove_DataGridView_Results.DataSource = null;
                Control_AdvancedRemove_DataGridView_Results.Rows.Clear();
                Control_AdvancedRemove_Image_NothingFound.Visible = false;

                // Expand the search panel on reset
                try
                {
                    if (Control_AdvancedRemove_SplitView != null)
                    {
                        Control_AdvancedRemove_SplitView.Panel1Collapsed = false;
                        UpdateSidePanelArrow(false);
                        Control_AdvancedRemove_Button_SidePanel.ForeColor =
                            Model_Application_Variables.UserUiColors.SuccessColor ?? Color.Green;
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedRemove SoftReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "Control_AdvancedRemove_SoftReset");
            }
            finally
            {
                Debug.WriteLine("[DEBUG] AdvancedRemove SoftReset button re-enabled");
                if (resetBtn.Length > 0 && resetBtn[0] is Button btn2)
                {
                    btn2.Enabled = true;
                }

                if (Control_RemoveTab.MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Restoring status strip after soft reset");
                    Control_RemoveTab.MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    Control_RemoveTab.MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    Control_RemoveTab.MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                }

                if (Control_AdvancedRemove_SuggestionBox_User.FindForm() is { } form)
                {
                    MainFormControlHelper.SetActiveControl(form, Control_AdvancedRemove_SuggestionBox_User);
                }
            }
        }

        private async void Control_AdvancedRemove_Button_Reset_Click(object? sender, EventArgs? e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                await Control_AdvancedRemove_HardReset();
            }
            else
            {
                Control_AdvancedRemove_SoftReset();
            }
        }

        private async void Control_AdvancedRemove_Button_Undo_Click(object? sender, EventArgs? e)
        {
            if (_lastRemovedItems.Count == 0)
            {
                return;
            }

            try
            {
                foreach (Model_History_Remove item in _lastRemovedItems)
                {
                    await Dao_Inventory.AddInventoryItemAsync(
                        item.PartId,
                        item.Location,
                        item.Operation,
                        item.Quantity,
                        item.ItemType,
                        item.User,
                        item.BatchNumber,
                        "Removal reversed via Undo Button.",
                        null,  // colorCode
                        null,  // workOrder
                        true
                    );
                }

                Service_ErrorHandler.ShowInformation("Undo successful. Removed items have been restored.");
                LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Undo: Removed items restored.");

                _lastRemovedItems.Clear();
                Control[] undoBtn = Controls.Find("Control_AdvancedRemove_Button_Undo", true);
                if (undoBtn.Length > 0 && undoBtn[0] is Button btn)
                {
                    btn.Enabled = false;
                }

                Control_AdvancedRemove_Button_Search_Click(null, null);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["ItemCount"] = _lastRemovedItems.Count
                    },
                    controlName: nameof(Control_AdvancedRemove));
            }
        }

        private void Control_AdvancedRemove_Button_Print_Click(object? sender, EventArgs? e)
        {
            // TEMPORARY: Print system being refactored (Phase 1 - Task T002)
            Service_ErrorHandler.ShowInformation(
                "Print functionality is being rebuilt. Coming soon!",
                "Feature Temporarily Unavailable");

            /* OLD IMPLEMENTATION - Kept for reference, will be restored in Phase 7
            try
            {
                MessageBox.Show(@"Not Implemented Yet");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                MessageBox.Show($@"Print failed: {ex.Message}", @"Print Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            */
        }

        private void Control_AdvancedRemove_Update_ButtonStates()
        {
            try
            {
                bool hasData = Control_AdvancedRemove_DataGridView_Results.Rows.Count > 0;
                if (Control_AdvancedRemove_Button_Print != null)
                {
                    Control_AdvancedRemove_Button_Print.Enabled = hasData;
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Core_WipAppVariables.Shortcut_Remove_Delete)
            {
                Control_AdvancedRemove_Button_Delete.PerformClick();
                return true;
            }

            if (keyData == Core_WipAppVariables.Shortcut_Remove_Undo)
            {
                Control_AdvancedRemove_Button_Undo.PerformClick();
                return true;
            }

            if (keyData == Core_WipAppVariables.Shortcut_Remove_Reset)
            {
                Control[] resetBtn = Controls.Find("Control_AdvancedRemove_Button_Reset", true);
                if (resetBtn.Length > 0 && resetBtn[0] is Button btn)
                {
                    btn.PerformClick();
                    return true;
                }
            }

            if (keyData == Core_WipAppVariables.Shortcut_Remove_Search)
            {
                Control[] searchBtn = Controls.Find("Control_AdvancedRemove_Button_Search", true);
                if (searchBtn.Length > 0 && searchBtn[0] is Button btn)
                {
                    btn.PerformClick();
                    return true;
                }
            }

            if (keyData == Core_WipAppVariables.Shortcut_Remove_Normal)
            {
                Control[] normalBtn = Controls.Find("Control_AdvancedRemove_Button_Normal", true);
                if (normalBtn.Length > 0 && normalBtn[0] is Button btn)
                {
                    btn.PerformClick();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Toggles the visibility of the input panel (collapse/expand).
        /// </summary>
        private void ToggleInputPanel()
        {
            if (Control_AdvancedRemove_Panel_Inputs == null)
                return;

            _isInputPanelCollapsed = !_isInputPanelCollapsed;

            // Toggle panel visibility
            Control_AdvancedRemove_Panel_Inputs.Visible = !_isInputPanelCollapsed;

            // Update button text to indicate current state
            Control[] sidePanelBtn = Controls.Find("Control_AdvancedRemove_Button_SidePanel", true);
            if (sidePanelBtn.Length > 0 && sidePanelBtn[0] is Button btn)
            {
                UpdateSidePanelArrow(_isInputPanelCollapsed);
                ToolTip toolTip = new();
                toolTip.SetToolTip(btn, _isInputPanelCollapsed ? "Show Search Panel" : "Hide Search Panel");
            }

            LoggingUtility.Log($"[{nameof(Control_AdvancedRemove)}] Input panel {(_isInputPanelCollapsed ? "collapsed" : "expanded")}");
        }

        /// <summary>
        /// Expands the input panel if it's currently collapsed.
        /// </summary>
        private void ExpandInputPanel()
        {
            if (_isInputPanelCollapsed)
            {
                ToggleInputPanel();
            }
        }

        private void Control_AdvancedRemove_Button_SidePanel_Click(object sender, EventArgs e)
        {
            ToggleInputPanel();
        }

        private void InitializeSidePanelAnimator()
        {
            try
            {
                Helper_ButtonToggleAnimations.ValidateIconButton(
                    Control_AdvancedRemove_Button_SidePanel,
                    nameof(Control_AdvancedRemove));

                UpdateSidePanelArrow(_isInputPanelCollapsed);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void UpdateSidePanelArrow(bool collapsed)
        {
            Helper_ButtonToggleAnimations.ApplyHorizontalArrow(
                ref _sidePanelAnimator,
                components,
                Control_AdvancedRemove_Button_SidePanel,
                collapsed);
        }


        private void InitializeQuickButtonsToggle()
        {
            try
            {
                Helper_ButtonToggleAnimations.ValidateIconButton(
                    Control_AdvancedRemove_Button_QuickButtonToggle,
                    nameof(Control_AdvancedRemove));

                if (Control_AdvancedRemove_Button_QuickButtonToggle != null)
                {
                    Control_AdvancedRemove_Button_QuickButtonToggle.Click -= Control_AdvancedRemove_Button_QuickButtonToggle_Click;
                    Control_AdvancedRemove_Button_QuickButtonToggle.Click += Control_AdvancedRemove_Button_QuickButtonToggle_Click;
                }

                bool collapsed = MainFormInstance?.MainForm_SplitContainer_Middle.Panel2Collapsed ?? false;
                UpdateQuickButtonsArrow(collapsed);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void Control_AdvancedRemove_Button_QuickButtonToggle_Click(object? sender, EventArgs e)
        {
            if (MainFormInstance == null)
            {
                return;
            }

            bool collapsed = MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed;
            MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed = !collapsed;
            UpdateQuickButtonsArrow(!collapsed);
        }

        internal void SyncQuickButtonsPanelState(bool panelCollapsed)
        {
            UpdateQuickButtonsArrow(panelCollapsed);
        }

        private void UpdateQuickButtonsArrow(bool collapsed)
        {
            Helper_ButtonToggleAnimations.ApplyHorizontalArrow(
                ref _quickButtonsAnimator,
                components,
                Control_AdvancedRemove_Button_QuickButtonToggle,
                collapsed);
        }
        #endregion

    }
}
