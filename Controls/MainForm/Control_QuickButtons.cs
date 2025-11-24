using System.Reflection;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;
using MTM_WIP_Application_Winforms.Controls.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    public partial class Control_QuickButtons : ThemedUserControl
    {
        #region Fields

        internal List<Control_QuickButton_Single>? quickButtons; // Changed to use custom control
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }
        private Helper_StoredProcedureProgress? _progressHelper;
        private readonly IShortcutService? _shortcutService;

        #endregion

        #region Progress Control Methods

        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel,
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Constructors

        public Control_QuickButtons()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_QuickButtons),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_QuickButtons), nameof(Control_QuickButtons));

            Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_INITIALIZATION", nameof(Control_QuickButtons),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();
            
            // Resolve shortcut service
            _shortcutService = Program.ServiceProvider?.GetService<IShortcutService>();
            if (_shortcutService != null && !string.IsNullOrEmpty(Model_Application_Variables.User))
            {
                // Initialize asynchronously
                _ = _shortcutService.InitializeAsync(Model_Application_Variables.User);
            }

            // Add padding to the main control as requested
            this.Padding = new Padding(3);

            Service_DebugTracer.TraceUIAction("TABLE_LAYOUT_SETUP", nameof(Control_QuickButtons),
                new Dictionary<string, object>
                {
                    ["RowCount"] = 11,
                    ["LayoutType"] = "TableLayoutPanel"
                });

            Control_QuickButtons_TableLayoutPanel_Main.RowCount = 11;
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Clear();

            for (int i = 0; i < 10; i++)
            {
                Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            }
            
            // Add 11th row to fill remaining space and equalize button heights
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 0F));

            quickButtons = new List<Control_QuickButton_Single>();
            for (int i = 0; i < 10; i++)
            {
                string shortcutId = $"Shortcut_QuickButton_{(i + 1):D2}";
                string hotkeyText = GetShortcutKey(i);


                var btn = new Control_QuickButton_Single
                {
                    Name = $"Control_QuickButtons_Button_{i + 1}",
                    Dock = DockStyle.Fill,
                    Margin = new Padding(1),
                    MinimumSize = new Size(0, 0),
                    TabIndex = i + 1,
                    TabStop = false,
                    ContextMenuStrip = Control_QuickButtons_ContextMenu,
                    HotkeyText = hotkeyText
                };

                btn.Click += QuickButton_Click;
                quickButtons.Add(btn);

                // Add button to its proper row in the TableLayoutPanel
                Control_QuickButtons_TableLayoutPanel_Main.Controls.Add(btn, 0, i);
            }

            this.Resize += Control_QuickButtons_Resize;

            this.Load += async (s, e) =>
            {
                try
                {
                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_EVENT", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Phase"] = "BEFORE_DELAY"
                        });

                    await Task.Delay(100); // Small delay to ensure UI is fully ready

                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_START", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Phase"] = "AFTER_DELAY"
                        });

                    await LoadLast10Transactions(Model_Application_Variables.User);

                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_COMPLETE", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Success"] = true
                        });
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_LOAD_ERROR", nameof(Control_QuickButtons),
                        new Dictionary<string, object>
                        {
                            ["User"] = Model_Application_Variables.User ?? "Unknown",
                            ["Error"] = ex.Message
                        });
                }
            };

            Service_DebugTracer.TraceUIAction("QUICK_BUTTONS_POST_CONSTRUCTOR", nameof(Control_QuickButtons),
                new Dictionary<string, object>
                {
                    ["Phase"] = "BEFORE_DPI_SCALING"
                });

            menuItemEdit.Click += MenuItemEdit_Click;
            menuItemRemove.Click += MenuItemRemove_Click;
            menuItemReorder.Click += MenuItemReorder_Click;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles control resize to update button font sizes adaptively
        /// </summary>
        private void Control_QuickButtons_Resize(object? sender, EventArgs e)
        {
            if (quickButtons == null)
            {
                return;
            }
            // Refresh all button layouts with new adaptive font sizes
            foreach (var btn in quickButtons)
            {
                if (btn.Tag != null)
                {
                    dynamic tag = btn.Tag!;
                    string partId = tag.partId?.ToString() ?? "";
                    string operation = tag.operation?.ToString() ?? "";
                    int quantity = tag.quantity != null ? Convert.ToInt32(tag.quantity) : 0;
                    string hotkeyText = tag.shortcutDisplay?.ToString() ?? "";
                    
                    // Safely access new properties (handle case where Tag might be from older code version if not fully reloaded)
                    string workOrder = "";
                    string colorCode = "";
                    try { workOrder = tag.workOrder?.ToString() ?? ""; } catch { }
                    try { colorCode = tag.colorCode?.ToString() ?? ""; } catch { }

                    // Repopulate with adaptive font sizes based on new button height
                    PopulateQuickButtonLayout(btn, partId, operation, quantity, hotkeyText, workOrder, colorCode);
                }
            }
        }

        public async Task LoadLast10Transactions(string? currentUser)
        {
            try
            {

                Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                {
                    ["CurrentUser"] = currentUser ?? "NULL",
                    ["ConnectionString"] = Model_Application_Variables.ConnectionString?.Substring(0, Math.Min(50, Model_Application_Variables.ConnectionString?.Length ?? 0)) + "..."
                }, nameof(Control_QuickButtons), nameof(LoadLast10Transactions));

                // FIXED: Use Helper_Database_StoredProcedure instead of direct MySqlConnection
                // because the stored procedure has p_Status and p_ErrorMsg parameters
                if (string.IsNullOrEmpty(currentUser)) { currentUser = string.Empty; }

                // STEP 1: Cleanup duplicates and gaps BEFORE loading

                var cleanupResult = await Dao_QuickButtons.CleanupGapsAndDuplicatesAsync(currentUser);
                if (!cleanupResult.IsSuccess)
                {
                    Service_DebugTracer.TraceBusinessLogic("QUICK_BUTTONS_CLEANUP_FAILED",
                        inputData: new { User = currentUser },
                        outputData: new { ErrorMessage = cleanupResult.ErrorMessage });
                }
                else
                {
                    Service_DebugTracer.TraceBusinessLogic("QUICK_BUTTONS_CLEANUP_SUCCESS",
                        inputData: new { User = currentUser },
                        outputData: new { });
                }


                // STEP 2: Load data from database
                if (string.IsNullOrEmpty(Model_Application_Variables.ConnectionString))
                {
                    throw new InvalidOperationException("Database connection string is not set.");
                }

                var dataResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString!,
                    "sys_last_10_transactions_Get_ByUser",
                    new Dictionary<string, object> { ["User"] = currentUser },
                    null // No progress helper for this method
                );

                Service_DebugTracer.TraceBusinessLogic("QUICK_BUTTONS_DATA_RESULT",
                    inputData: new { User = currentUser },
                    outputData: new
                    {
                        IsSuccess = dataResult.IsSuccess,
                        RowCount = dataResult.Data?.Rows.Count ?? 0,
                        ErrorMessage = dataResult.ErrorMessage
                    });

                if (!dataResult.IsSuccess || dataResult.Data == null)
                {


                    Service_DebugTracer.TraceBusinessLogic("QUICK_BUTTONS_LOAD_FAILED",
                        inputData: new { User = currentUser },
                        outputData: new { ErrorMessage = dataResult.ErrorMessage });

                    // Clear all buttons if no data can be loaded
                    if (quickButtons != null)
                    {

                        for (int j = 0; j < quickButtons.Count; j++)
                        {
                            quickButtons[j].Text = string.Empty;
                            Control_QuickButtons_Tooltip.SetToolTip(quickButtons[j], $"Position: {j + 1}");
                            quickButtons[j].Tag = new
                            {
                                partId = (string?)null,
                                operation = (string?)null,
                                quantity = (int?)null,
                                position = j + 1
                            };
                            quickButtons[j].Visible = false;
                        }
                        RefreshButtonLayout();
                    }

                    Service_DebugTracer.TraceMethodExit(null, nameof(Control_QuickButtons), nameof(LoadLast10Transactions));
                    return;
                }

                var dataTable = dataResult.Data;


                // STEP 3: Populate UI buttons with database data

                int i = 0;

                // Fill buttons with data from DB
                foreach (System.Data.DataRow row in dataTable.Rows)
                {
                    if (i >= quickButtons?.Count) break;

                    string? partId = row["PartID"] == DBNull.Value ? null : row["PartID"].ToString();
                    string? operation = row["Operation"] == DBNull.Value ? null : row["Operation"].ToString();
                    int? quantity = row["Quantity"] == DBNull.Value ? null :
                        row["Quantity"] is int q ? q : Convert.ToInt32(row["Quantity"]);
                    
                    // Check for optional columns (WorkOrder, ColorCode) that might be added in future DB updates
                    string? workOrder = dataTable.Columns.Contains("WorkOrder") && row["WorkOrder"] != DBNull.Value 
                        ? row["WorkOrder"].ToString() 
                        : null;
                    string? colorCode = dataTable.Columns.Contains("ColorCode") && row["ColorCode"] != DBNull.Value 
                        ? row["ColorCode"].ToString() 
                        : null;

                    // Always set position 1-10, no duplicates
                    int displayPosition = i + 1;

                    if (quickButtons != null)
                    {
                        string shortcutId = $"Shortcut_QuickButton_{displayPosition:D2}";
                        string shortcutDisplay = GetShortcutKey(displayPosition - 1);

                        // Set all data at once using SetData (includes hotkey, part, operation, quantity, workOrder, colorCode)
                        if (quickButtons[i] is Control_QuickButton_Single singleButton)
                        {
                            singleButton.SetData(shortcutDisplay, partId ?? "", operation ?? "", quantity ?? 0, workOrder ?? "", colorCode ?? "");
                            singleButton.UpdateFontSizes();
                        }

                        string tooltipText = partId != null && operation != null && quantity != null
                            ? $"Part ID: {partId}, Operation: {operation}, Quantity: {quantity}"
                            : $"Position: {displayPosition}";

                        if (!string.IsNullOrEmpty(workOrder))
                        {
                            tooltipText += $"\nWork Order: {workOrder}";
                        }
                        if (!string.IsNullOrEmpty(colorCode))
                        {
                            tooltipText += $"\nColor: {colorCode}";
                        }

                        tooltipText += $"\nPosition: {displayPosition}\nShortcut: {shortcutDisplay}";
                        
                        // Recursively set tooltip on button and all its children to ensure it shows up
                        SetTooltipRecursive(quickButtons[i], tooltipText);

                        quickButtons[i].Tag = new { shortcutDisplay, partId, operation, quantity, position = displayPosition, workOrder, colorCode };
                        quickButtons[i].Visible = !string.IsNullOrWhiteSpace(shortcutDisplay) && 
                                                  !string.IsNullOrWhiteSpace(partId) && 
                                                  !string.IsNullOrWhiteSpace(operation) && 
                                                  quantity != null;
                    }
                    i++;
                }

                // Clear remaining buttons
                if (quickButtons != null && i < quickButtons.Count)
                {

                    for (; i < quickButtons.Count; i++)
                    {
                        int displayPosition = i + 1;

                        if (quickButtons[i] is Control_QuickButton_Single singleButton)
                        {
                            singleButton.SetData(GetShortcutKey(displayPosition - 1), "", "", 0);
                        }

                        // quickButtons[i].Controls.Clear(); // REMOVED: This destroys the UserControl structure!
                        quickButtons[i].Text = string.Empty;
                        
                        string tooltipText = $"Position: {displayPosition}\nShortcut: {GetShortcutKey(displayPosition - 1)}";
                        SetTooltipRecursive(quickButtons[i], tooltipText);
                        
                        quickButtons[i].Tag = new
                        {
                            partId = (string?)null,
                            operation = (string?)null,
                            quantity = (int?)null,
                            position = displayPosition,
                            workOrder = (string?)null,
                            colorCode = (string?)null
                        };
                        quickButtons[i].Visible = false;
                    }
                }

                // STEP 4: Refresh UI layout

                if (quickButtons != null)
                {
                    RefreshButtonLayout();
                    int visibleCount = quickButtons.Count(b => b.Visible);

                }

                Service_DebugTracer.TraceMethodExit(new { VisibleButtons = quickButtons?.Count(b => b.Visible) ?? 0 },
                    nameof(Control_QuickButtons), nameof(LoadLast10Transactions));
            }
            catch (Exception ex)
            {




                LoggingUtility.LogApplicationError(ex);

                // Clear all buttons on error to prevent showing control names
                if (quickButtons != null)
                {
                    for (int j = 0; j < quickButtons.Count; j++)
                    {
                        quickButtons[j].Text = string.Empty;
                        quickButtons[j].Visible = false;
                    }
                    RefreshButtonLayout();
                }
            }
        }

        /// <summary>
        /// Recursively sets the tooltip for a control and all its children.
        /// This ensures the tooltip shows up even when hovering over child controls (labels, panels, etc.).
        /// </summary>
        private void SetTooltipRecursive(Control control, string text)
        {
            if (control == null) return;
            
            Control_QuickButtons_Tooltip.SetToolTip(control, text);
            
            foreach (Control child in control.Controls)
            {
                SetTooltipRecursive(child, text);
            }
        }

        private void RefreshButtonLayout()
        {
            // Force UI refresh: clear and re-add only visible buttons
            Control_QuickButtons_TableLayoutPanel_Main.SuspendLayout();
            Control_QuickButtons_TableLayoutPanel_Main.Controls.Clear();

            if (quickButtons != null)
            {
                for (int j = 0; j < quickButtons.Count; j++)
                {
                    if (quickButtons[j].Visible)
                    {
                        Control_QuickButtons_TableLayoutPanel_Main.Controls.Add(quickButtons[j], 0, j);
                    }
                }
            }

            Control_QuickButtons_TableLayoutPanel_Main.ResumeLayout();
            Control_QuickButtons_TableLayoutPanel_Main.PerformLayout();
        }

        private void PopulateQuickButtonLayout(Control_QuickButton_Single button, string partId, string operation, int quantity, string hotkeyText, string workOrder = "", string colorCode = "")
        {
            // Update the custom control's data properties
            button.PartId = partId;
            button.Operation = operation;
            button.Quantity = quantity;
            button.HotkeyText = hotkeyText;
            button.WorkOrder = workOrder;
            button.ColorCode = colorCode;

            // Update font sizes for current height
            button.UpdateFontSizes();
        }

        /// <summary>
        /// Wires mouse events from child controls to propagate to parent button for hover/click effects
        /// </summary>
        private void WireMouseEventsToChildren(Control parentButton, Control childControl)
        {
            // No longer needed - UserControl handles clicks directly
        }

        private static void SetComboBoxText(object control, string fieldName, string value)
        {
            FieldInfo? field = control.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            var fieldValue = field?.GetValue(control);

            // Handle TextBox/SuggestionTextBox controls
            if (fieldValue is TextBox tb)
            {
                tb.Text = value;

                // Set proper ForeColor for SuggestionTextBox to prevent error coloring
                tb.ForeColor = Model_Application_Variables.UserUiColors.ComboBoxForeColor ?? Color.Black;

                // Update Model_Application_Variables based on field name to sync state
                if (fieldName.Contains("Part"))
                {
                    Model_Application_Variables.PartId = value;
                }
                else if (fieldName.Contains("Operation"))
                {
                    Model_Application_Variables.Operation = value;
                }
                else if (fieldName.Contains("Location"))
                {
                    Model_Application_Variables.Location = value;
                }

                return;
            }

            // Handle ComboBox controls (legacy)
            if (fieldValue is not ComboBox cb)
            {
                return;
            }

            // Search for the item by display text (not SelectedValue which expects integer ID)
            // This handles multi-column ComboBoxes where DisplayMember is the text we want to match
            if (cb.DataSource != null && !string.IsNullOrEmpty(cb.DisplayMember))
            {
                // Search through items by display text
                for (int i = 0; i < cb.Items.Count; i++)
                {
                    string? displayText = cb.GetItemText(cb.Items[i]);
                    if (string.IsNullOrEmpty(displayText)) continue;

                    // Match if display text starts with the value (handles "PartID | Customer | Description" format)
                    if (displayText.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                    {
                        cb.SelectedIndex = i;
                        return;
                    }
                }

                // If no match found, set text directly (allows typing)
                cb.Text = value;
            }
            else
            {
                // Fallback to text search for simple ComboBoxes
                cb.SelectedIndex = cb.FindStringExact(value);
                if (cb.SelectedIndex < 0)
                {
                    cb.Text = value;
                }
            }
        }

        private static void SetTextBoxText(object control, string fieldName, string value)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            FieldInfo? field = control.GetType().GetField(fieldName, bindingFlags);
            if (field?.GetValue(control) is SuggestionTextBoxWithLabel tb)
            {
                tb.Text = value;
                return;
            }

            field = control.GetType().GetField(fieldName, bindingFlags);
            if (field?.GetValue(control) is SuggestionTextBox tbLegacy)
            {
                tbLegacy.Text = value;
            }
        }

        private void QuickButton_Click(object? sender, EventArgs? e) // Changed from static to instance method
        {
            if (sender is not Control_QuickButton_Single btn)
            {
                return;
            }

            object? tagObj = btn.Tag;
            if (tagObj == null)
            {
                return;
            }

            dynamic tag = tagObj;
            string partId = tag.partId;
            string operation = tag.operation;
            int quantity = tag.quantity;
            
            string workOrder = "";
            string colorCode = "";
            try { workOrder = tag.workOrder?.ToString() ?? ""; } catch { }
            try { colorCode = tag.colorCode?.ToString() ?? ""; } catch { }

            Forms.MainForm.MainForm? mainForm = MainFormInstance;
            if (mainForm == null)
            {
                return;
            }

            void SetComboBoxes(object control, string partField, string opField, string part, string op)
            {
                SetComboBoxText(control, partField, part);
                SetComboBoxText(control, opField, op);
            }

            void TriggerEnterEvent(Control control)
            {
                EventArgs enterEventArgs = EventArgs.Empty;
                MethodInfo? onEnterMethod =
                    control.GetType().GetMethod("OnEnter", BindingFlags.NonPublic | BindingFlags.Instance);
                onEnterMethod?.Invoke(control, new object[] { enterEventArgs });
            }

            void SetFocusOnControl(object parentControl, string fieldName)
            {
                FieldInfo? field = parentControl.GetType().GetField(fieldName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field?.GetValue(parentControl) is Control targetControl && targetControl.CanFocus)
                {
                    targetControl.Focus();
                    TriggerEnterEvent(targetControl);
                }
            }

            void ClickSearchButtonIfAvailable(object control, string fieldName, bool allowHidden = false, string? handlerName = null)
            {
                FieldInfo? field =
                    control.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (field?.GetValue(control) is Button searchButton && searchButton.Enabled)
                {
                    if (searchButton.Visible)
                    {
                        searchButton.PerformClick();
                    }
                    else if (allowHidden && !string.IsNullOrWhiteSpace(handlerName))
                    {
                        MethodInfo? handler = control.GetType().GetMethod(handlerName,
                            BindingFlags.NonPublic | BindingFlags.Instance);
                        handler?.Invoke(control, new object?[] { searchButton, EventArgs.Empty });
                    }
                }
            }

            if (mainForm.MainForm_UserControl_InventoryTab?.Visible == true)
            {
                Control_InventoryTab? inv = mainForm.MainForm_UserControl_InventoryTab;
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_Part", partId);
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_Operation", operation);
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_Quantity", quantity.ToString());
                
                // Set optional fields
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_WorkOrder", workOrder);
                SetTextBoxText(inv, "Control_InventoryTab_SuggestionBox_ColorCode", colorCode);

                SetFocusOnControl(inv, "Control_InventoryTab_SuggestionBox_Location");
                mainForm.MainForm_UserControl_InventoryTab.UpdateColorCodeFieldsVisibility();
                return;
            }

            if (mainForm.MainForm_UserControl_RemoveTab?.Visible == true)
            {
                Control_RemoveTab? rem = mainForm.MainForm_UserControl_RemoveTab;
                SetTextBoxText(rem, "Control_RemoveTab_TextBox_Part", partId);
                SetTextBoxText(rem, "Control_RemoveTab_TextBox_Operation", operation);
                SetFocusOnControl(rem, "Control_RemoveTab_TextBox_Operation");
                rem.Focus();
                TriggerEnterEvent(rem);
                ClickSearchButtonIfAvailable(rem, "Control_RemoveTab_Button_Search");
                return;
            }

            if (mainForm.MainForm_UserControl_TransferTab?.Visible == true)
            {
                Control_TransferTab? trn = mainForm.MainForm_UserControl_TransferTab;
                SetTextBoxText(trn, "Control_TransferTab_TextBox_Part", partId);
                SetTextBoxText(trn, "Control_TransferTab_TextBox_Operation", operation);
                trn.Focus();
                TriggerEnterEvent(trn);
                SetFocusOnControl(trn, "Control_TransferTab_TextBox_ToLocation");
                ClickSearchButtonIfAvailable(trn, "Control_TransferTab_Button_Search",
                    allowHidden: true,
                    handlerName: "Control_TransferTab_Button_Search_Click");
                return;
            }

            if (mainForm.MainForm_UserControl_AdvancedInventory?.Visible == true)
            {
                Control_AdvancedInventory? advInv = mainForm.MainForm_UserControl_AdvancedInventory;
                FieldInfo? tabControlField = advInv.GetType().GetField("AdvancedInventory_TabControl",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                TabControl? tabControl = tabControlField?.GetValue(advInv) as TabControl;
                if (tabControl != null)
                {
                    TabPage? selectedTab = tabControl.SelectedTab;
                    if (selectedTab != null && selectedTab.Name == "AdvancedInventory_TabControl_MultiLoc")
                    {
                        // Multi-Location tab - use SuggestionTextBox controls
                        SetTextBoxText(advInv, "AdvancedInventory_MultiLoc_TextBox_Part", partId);
                        SetTextBoxText(advInv, "AdvancedInventory_MultiLoc_TextBox_Op", operation);
                        SetTextBoxText(advInv, "AdvancedInventory_MultiLoc_TextBox_Qty", quantity.ToString());
                        SetFocusOnControl(advInv, "AdvancedInventory_MultiLoc_TextBox_Loc");
                    }
                    else
                    {
                        // Single Entry tab - use SuggestionTextBox controls
                        SetTextBoxText(advInv, "AdvancedInventory_Single_TextBox_Part", partId);
                        SetTextBoxText(advInv, "AdvancedInventory_Single_TextBox_Op", operation);
                        SetTextBoxText(advInv, "AdvancedInventory_Single_TextBox_Qty", quantity.ToString());
                        SetFocusOnControl(advInv, "AdvancedInventory_Single_TextBox_Loc");
                    }
                }

                advInv.Focus();
                TriggerEnterEvent(advInv);
                return;
            }

            if (mainForm.MainForm_UserControl_AdvancedRemove?.Visible == true)
            {
                Control_AdvancedRemove? advRem = mainForm.MainForm_UserControl_AdvancedRemove;
                SetComboBoxes(advRem, "Control_AdvancedRemove_TextBox_Part", "Control_AdvancedRemove_TextBox_Operation",
                    partId, operation);
                advRem.Focus();
                TriggerEnterEvent(advRem);
                ClickSearchButtonIfAvailable(advRem, "Control_AdvancedRemove_Button_Search");
                return;
            }
        }

        private async void MenuItemEdit_Click(object? sender, EventArgs? e)
        {
                if (Control_QuickButtons_ContextMenu.SourceControl is Control_QuickButton_Single btn && btn.Tag != null && quickButtons != null)
            {
                int idx = quickButtons.IndexOf(btn);
                int position = idx + 1; // Convert 0-based index to 1-based position
                object tagObj = btn.Tag;
                    if (tagObj == null)
                    {
                        return;
                    }
                    dynamic tag = tagObj;
                    string oldPartId = tag.partId;
                    string oldOperation = tag.operation;
                    int oldQuantity = tag.quantity;
                string user = Model_Application_Variables.User;

                using Form_QuickButtonEdit dlg = new(oldPartId, oldOperation, oldQuantity);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Check if the edited values create a duplicate (same PartID + Operation already exists)
                    bool isDuplicate = false;
                    foreach (var otherBtn in quickButtons)
                    {
                        if (otherBtn == btn) continue; // Skip the current button being edited

                        if (otherBtn.Tag != null)
                        {
                            object otherTagObj = otherBtn.Tag;
                            if (otherTagObj == null) continue;
                            dynamic otherTag = otherTagObj;
                            string otherPartId = otherTag.partId?.ToString() ?? "";
                            string otherOperation = otherTag.operation?.ToString() ?? "";

                            if (string.Equals(otherPartId, dlg.PartId, StringComparison.OrdinalIgnoreCase) &&
                                string.Equals(otherOperation, dlg.Operation, StringComparison.OrdinalIgnoreCase))
                            {
                                isDuplicate = true;
                                break;
                            }
                        }
                    }

                    if (isDuplicate)
                    {

                        MessageBox.Show(
                            $"A quick button for Part '{dlg.PartId}' with Operation '{dlg.Operation}' already exists.\n\nNo changes were made.",
                            "Duplicate Quick Button",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    try
                    {
                        // Update the button in the database
                        var result = await Dao_QuickButtons.UpdateQuickButtonAsync(user, position, dlg.PartId, dlg.Operation,
                            dlg.Quantity);
                        if (!result.IsSuccess)
                        {
                            Service_ErrorHandler.HandleDatabaseError(
                                result.Exception ?? new Exception(result.ErrorMessage),
                                contextData: new Dictionary<string, object>
                                {
                                    ["User"] = user,
                                    ["ButtonIndex"] = idx,
                                    ["Position"] = position,
                                    ["PartId"] = dlg.PartId,
                                    ["Operation"] = dlg.Operation,
                                    ["Quantity"] = dlg.Quantity
                                },
                                methodName: nameof(MenuItemEdit_Click),
                                dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                            return;
                        }

                        // CRITICAL: Reload from database to ensure UI matches database state
                        await LoadLast10Transactions(user);

                    }
                    catch (MySqlException ex)
                    {
                        LoggingUtility.LogDatabaseError(ex);
                        Service_ErrorHandler.HandleDatabaseError(ex,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            methodName: nameof(MenuItemEdit_Click),
                            dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            controlName: nameof(Control_QuickButtons));
                    }
                }
            }
        }

        private async void MenuItemRemove_Click(object? sender, EventArgs? e)
        {
            if (Control_QuickButtons_ContextMenu.SourceControl is Control_QuickButton_Single btn && quickButtons != null)
            {
                int idx = quickButtons.IndexOf(btn);
                if (idx >= 0 && btn.Tag is not null)
                {
                    string user = Model_Application_Variables.User;
                    int position = idx + 1; // Convert 0-based index to 1-based position

                    try
                    {
                        var result = await Dao_QuickButtons.RemoveQuickButtonAndShiftAsync(user, position);
                        if (!result.IsSuccess)
                        {
                            Service_ErrorHandler.HandleDatabaseError(
                                result.Exception ?? new Exception(result.ErrorMessage),
                                contextData: new Dictionary<string, object>
                                {
                                    ["User"] = user,
                                    ["ButtonIndex"] = idx,
                                    ["Position"] = position
                                },
                                methodName: nameof(MenuItemRemove_Click),
                                dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                            return;
                        }

                        // CRITICAL: Reload from database to ensure UI matches database state
                        await LoadLast10Transactions(user);

                    }
                    catch (MySqlException ex)
                    {
                        LoggingUtility.LogDatabaseError(ex);
                        Service_ErrorHandler.HandleDatabaseError(ex,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            methodName: nameof(MenuItemRemove_Click),
                            dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                            contextData: new Dictionary<string, object>
                            {
                                ["User"] = user,
                                ["ButtonIndex"] = idx,
                                ["Position"] = position
                            },
                            controlName: nameof(Control_QuickButtons));
                    }
                }
                else
                {

                }
            }
        }

        private string GetShortcutKey(int index)
        {
            if (_shortcutService == null)
            {
            return "Error";
            }

            string shortcutId = $"Shortcut_QuickButton_{(index + 1):D2}";
            Keys shortcutKey = _shortcutService.GetShortcutKey(shortcutId);
            
            return shortcutKey == Keys.None ? string.Empty : shortcutKey.ToString();
        }

        private async void MenuItemReorder_Click(object? sender, EventArgs? e)
        {
            if (quickButtons == null)
            {
                return;
            }

            using Form_QuickButtonOrder dlg = new(quickButtons);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                string user = Model_Application_Variables.User;
                List<Control_QuickButton_Single> newOrder = dlg.GetButtonOrder();

                try
                {
                    // Extract data to avoid UI thread issues during async operations
                    var buttonsData = newOrder
                        .Where(b => !string.IsNullOrEmpty(b.PartId) && !string.IsNullOrEmpty(b.Operation))
                        .Select(b => new { b.PartId, b.Operation, b.Quantity })
                        .ToList();

                    // Delete all existing buttons
                    var deleteResult = await Dao_QuickButtons.DeleteAllQuickButtonsForUserAsync(user);
                    if (!deleteResult.IsSuccess)
                    {
                        Service_ErrorHandler.HandleDatabaseError(
                            deleteResult.Exception ?? new Exception(deleteResult.ErrorMessage),
                            contextData: new Dictionary<string, object> { ["User"] = user },
                            methodName: nameof(MenuItemReorder_Click),
                            dbSeverity: Enum_DatabaseEnum_ErrorSeverity.Error);
                        return;
                    }

                    // Insert buttons in new order
                    for (int i = 0; i < buttonsData.Count; i++)
                    {
                        var data = buttonsData[i];
                        int position = i + 1;

                        // Use AddQuickButtonAsync which maps to sys_last_10_transactions_Add_AtPosition
                        var addResult = await Dao_QuickButtons.AddQuickButtonAsync(
                            user, data.PartId, data.Operation, data.Quantity, position);

                        if (!addResult.IsSuccess)
                        {
                            LoggingUtility.LogDatabaseError(new Exception($"Failed to add button at position {position}: {addResult.ErrorMessage}"));
                        }
                    }

                    // Reload UI
                    await LoadLast10Transactions(user);
                }
                catch (Exception ex)
                {
                    LoggingUtility.LogApplicationError(ex);
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                        contextData: new Dictionary<string, object> { ["User"] = user },
                        controlName: nameof(Control_QuickButtons));
                }
            }
        }

        #endregion Methods
    }
}

