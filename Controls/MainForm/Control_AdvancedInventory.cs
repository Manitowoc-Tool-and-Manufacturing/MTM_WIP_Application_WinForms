using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Windows.Forms;
using ClosedXML.Excel;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.ErrorDialog;
using MTM_WIP_Application_Winforms.Forms.MainForm.Classes;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;
using Color = System.Drawing.Color;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    public partial class Control_AdvancedInventory : UserControl
    {
        #region Fields

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }

        private Helper_StoredProcedureProgress? _progressHelper;

        #endregion

        #region Progress Control Methods

        /// <summary>
        /// Sets progress controls for visual feedback during long-running advanced inventory operations.
        /// </summary>
        /// <param name="progressBar">The progress bar control to display operation progress (0-100%)</param>
        /// <param name="statusLabel">The status label control to display operation status messages</param>
        /// <exception cref="InvalidOperationException">Thrown when control is not added to a form</exception>
        /// <remarks>
        /// Must be called during initialization before any async operations that require progress feedback.
        /// Progress helper is used by Excel import/export, multi-location saves, and batch operations.
        /// Provides visual feedback for database-intensive operations and file I/O.
        /// </remarks>
        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel,
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Constructors

        public Control_AdvancedInventory()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_AdvancedInventory),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_AdvancedInventory), nameof(Control_AdvancedInventory));

            try
            {
                Service_DebugTracer.TraceUIAction("ADVANCED_INVENTORY_INITIALIZATION", nameof(Control_AdvancedInventory),
                    new Dictionary<string, object>
                    {
                        ["Phase"] = "START",
                        ["ComponentType"] = "UserControl"
                    });

                LoggingUtility.Log("Control_AdvancedInventory constructor entered.");
                InitializeComponent();
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_AdvancedInventory),
                    new Dictionary<string, object>
                    {
                        ["DpiScaling"] = "APPLIED",
                        ["LayoutAdjustments"] = "APPLIED"
                    });
                // Apply comprehensive DPI scaling and runtime layout adjustments
                // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
                // Do NOT call theme update methods from arbitrary event handlers or business logic.

                Service_DebugTracer.TraceUIAction("TOOLTIPS_SETUP", nameof(Control_AdvancedInventory),
                    new Dictionary<string, object>
                    {
                        ["TooltipCount"] = 8,
                        ["ButtonTypes"] = new[] { "Single", "MultiLocation", "Import" },
                        ["ButtonsConfigured"] = new[] { "Send", "Save", "Reset", "Normal", "AddLoc", "SaveAll", "OpenExcel" }
                    });
                ToolTip toolTip = new();
                toolTip.SetToolTip(AdvancedInventory_Single_Button_Send,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Send)}");
                toolTip.SetToolTip(AdvancedInventory_Single_Button_Save,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Save)}");
                toolTip.SetToolTip(AdvancedInventory_Single_Button_Reset,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Reset)}");
                toolTip.SetToolTip(AdvancedInventory_Single_Button_Normal,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Normal)}");
                toolTip.SetToolTip(AdvancedInventory_MultiLoc_Button_AddLoc,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Multi_AddLoc)}");
                toolTip.SetToolTip(AdvancedInventory_MultiLoc_Button_SaveAll,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Multi_SaveAll)}");
                toolTip.SetToolTip(AdvancedInventory_MultiLoc_Button_Reset,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Multi_Reset)}");
                toolTip.SetToolTip(AdvancedInventory_Multi_Button_Normal,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Multi_Normal)}");
                toolTip.SetToolTip(AdvancedInventory_Import_Button_OpenExcel,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Import_OpenExcel)}");
                toolTip.SetToolTip(AdvancedInventory_Import_Button_ImportExcel,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Import_ImportExcel)}");
                toolTip.SetToolTip(AdvancedInventory_Import_Button_Save,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Import_Save)}");
                toolTip.SetToolTip(AdvancedInventory_Import_Button_Normal,
                    $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_AdvInv_Import_Normal)}");

                AdvancedInventory_Single_Button_Reset.TabStop = false;
                AdvancedInventory_MultiLoc_Button_Reset.TabStop = false;

                // Configure SuggestionTextBox controls for Single and Multi-Location tabs
                ConfigureSuggestionTextBoxes();

                WireUpEvents();

                AdvancedInventory_MultiLoc_ListView_Preview.View = View.Details;
                if (AdvancedInventory_MultiLoc_ListView_Preview.Columns.Count == 0)
                {
                    AdvancedInventory_MultiLoc_ListView_Preview.Columns.Add("Location", 150);
                    AdvancedInventory_MultiLoc_ListView_Preview.Columns.Add("Part ID", 150);
                    AdvancedInventory_MultiLoc_ListView_Preview.Columns.Add("Quantity", 75);
                }
                AdvancedInventory_MultiLoc_ListView_Preview.SizeChanged += (s, e) =>
                {
                    int width = AdvancedInventory_MultiLoc_ListView_Preview.ClientSize.Width;
                    AdvancedInventory_MultiLoc_ListView_Preview.Columns[0].Width = (int)(width * 0.30); // Location 40%
                    AdvancedInventory_MultiLoc_ListView_Preview.Columns[1].Width = (int)(width * 0.40); // Part ID 40%
                    AdvancedInventory_MultiLoc_ListView_Preview.Columns[2].Width = (int)(width * 0.30); // Quantity 20%
                };

                AdvancedInventory_Single_ListView_Preview.View = View.Details;
                if (AdvancedInventory_Single_ListView_Preview.Columns.Count == 0)
                {
                    AdvancedInventory_Single_ListView_Preview.Columns.Add("Location", 150);
                    AdvancedInventory_Single_ListView_Preview.Columns.Add("Part ID", 150);
                    AdvancedInventory_Single_ListView_Preview.Columns.Add("Quantity", 75);
                }
                AdvancedInventory_Single_ListView_Preview.SizeChanged += (s, e) =>
                {
                    int width = AdvancedInventory_Single_ListView_Preview.ClientSize.Width;
                    AdvancedInventory_Single_ListView_Preview.Columns[0].Width = (int)(width * 0.30); // Location 40%
                    AdvancedInventory_Single_ListView_Preview.Columns[1].Width = (int)(width * 0.40); // Part ID 40%
                    AdvancedInventory_Single_ListView_Preview.Columns[2].Width = (int)(width * 0.30); // Quantity 20%
                };

                // ListView KeyDown handlers for Delete key
                AdvancedInventory_Single_ListView_Preview.KeyDown += AdvancedInventory_Single_ListView_KeyDown;
                AdvancedInventory_MultiLoc_ListView_Preview.KeyDown += AdvancedInventory_MultiLoc_ListView_Preview_KeyDown;

                if (AdvancedInventory_TabControl == null)
                {
                    LoggingUtility.LogApplicationError(
                        new InvalidOperationException("TabControl 'AdvancedInventory_TabControl' not found."));
                    throw new InvalidOperationException("TabControl 'AdvancedInventory_TabControl' not found.");
                }

                if (AdvancedInventory_TabControl_Import == null)
                {
                    LoggingUtility.LogApplicationError(
                        new InvalidOperationException("Tab 'AdvancedInventory_TabControl_Import' not found."));
                    throw new InvalidOperationException("Tab 'AdvancedInventory_TabControl_Import' not found.");
                }

                ValidateQtyTextBox(AdvancedInventory_Single_TextBox_Qty);
                ValidateQtyTextBox(AdvancedInventory_Single_TextBox_Count);
                ValidateQtyTextBox(AdvancedInventory_MultiLoc_TextBox_Qty);

                Enter += (s, e) =>
                {
                    if (AdvancedInventory_TabControl.SelectedIndex == 0 &&
                        AdvancedInventory_Single_TextBox_Part.Visible &&
                        AdvancedInventory_Single_TextBox_Part.Enabled)
                    {
                        AdvancedInventory_Single_TextBox_Part.Focus();
                        AdvancedInventory_Single_TextBox_Part.SelectAll();
                    }
                    else if (AdvancedInventory_TabControl.SelectedIndex == 1 &&
                             AdvancedInventory_MultiLoc_TextBox_Part.Visible &&
                             AdvancedInventory_MultiLoc_TextBox_Part.Enabled)
                    {
                        AdvancedInventory_MultiLoc_TextBox_Part.Focus();
                        AdvancedInventory_MultiLoc_TextBox_Part.SelectAll();
                    }
                };
                Core_Themes.ApplyFocusHighlighting(this);
                LoggingUtility.Log("Control_AdvancedInventory constructor exited.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "Control_AdvancedInventory_Ctor");
            }
        }

        #endregion

        #region SuggestionTextBox Configuration

        /// <summary>
        /// Configures SuggestionTextBox controls for Single Entry and Multi-Location tabs with appropriate data providers and F4 support.
        /// </summary>
        private void ConfigureSuggestionTextBoxes()
        {
            // Single Entry tab
            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                AdvancedInventory_Single_TextBox_Part,
                Helper_SuggestionTextBox.GetCachedPartNumbersAsync,
                enableF4: true);

            Helper_SuggestionTextBox.ConfigureForOperations(
                AdvancedInventory_Single_TextBox_Op,
                Helper_SuggestionTextBox.GetCachedOperationsAsync,
                enableF4: true);

            Helper_SuggestionTextBox.ConfigureForLocations(
                AdvancedInventory_Single_TextBox_Loc,
                Helper_SuggestionTextBox.GetCachedLocationsAsync,
                enableF4: true);

            // Wire up SuggestionSelected event handlers for Single tab
            AdvancedInventory_Single_TextBox_Part.SuggestionSelected += AdvancedInventory_Single_TextBox_Part_SuggestionSelected;
            AdvancedInventory_Single_TextBox_Op.SuggestionSelected += AdvancedInventory_Single_TextBox_Op_SuggestionSelected;
            AdvancedInventory_Single_TextBox_Loc.SuggestionSelected += AdvancedInventory_Single_TextBox_Loc_SuggestionSelected;

            // Multi-Location tab
            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                AdvancedInventory_MultiLoc_TextBox_Part,
                Helper_SuggestionTextBox.GetCachedPartNumbersAsync,
                enableF4: true);

            Helper_SuggestionTextBox.ConfigureForOperations(
                AdvancedInventory_MultiLoc_TextBox_Op,
                Helper_SuggestionTextBox.GetCachedOperationsAsync,
                enableF4: true);

            Helper_SuggestionTextBox.ConfigureForLocations(
                AdvancedInventory_MultiLoc_TextBox_Loc,
                Helper_SuggestionTextBox.GetCachedLocationsAsync,
                enableF4: true);

            // Wire up SuggestionSelected event handlers for Multi-Location tab
            AdvancedInventory_MultiLoc_TextBox_Part.SuggestionSelected += AdvancedInventory_MultiLoc_TextBox_Part_SuggestionSelected;
            AdvancedInventory_MultiLoc_TextBox_Op.SuggestionSelected += AdvancedInventory_MultiLoc_TextBox_Op_SuggestionSelected;
            AdvancedInventory_MultiLoc_TextBox_Loc.SuggestionSelected += AdvancedInventory_MultiLoc_TextBox_Loc_SuggestionSelected;

            LoggingUtility.Log("[Control_AdvancedInventory] SuggestionTextBox controls configured for Single Entry and Multi-Location tabs");
        }

        #endregion

        #region Event Wiring

        private void WireUpEvents()
        {
            try
            {
                // Single tab and Multi-Location tab SuggestionTextBox events are wired in ConfigureSuggestionTextBoxes()
                // Part, Op, Loc now use SuggestionSelected events instead of SelectedIndexChanged/Leave

                AdvancedInventory_Single_TextBox_Qty.TextChanged += (s, e) =>
                {
                    InventoryTextBoxQty_TextChanged(AdvancedInventory_Single_TextBox_Qty);
                    ValidateQtyTextBox(AdvancedInventory_Single_TextBox_Qty);
                    UpdateSingleSaveButtonState();
                    LoggingUtility.Log("Single Qty TextBox changed.");
                };
                AdvancedInventory_Single_TextBox_Qty.Leave += (s, e) =>
                {
                    ValidateQtyTextBox(AdvancedInventory_Single_TextBox_Qty);
                };

                AdvancedInventory_Single_TextBox_Qty.Enter +=
                    (s, e) => AdvancedInventory_Single_TextBox_Qty.SelectAll();
                AdvancedInventory_Single_TextBox_Qty.Click +=
                    (s, e) => AdvancedInventory_Single_TextBox_Qty.SelectAll();
                AdvancedInventory_Single_TextBox_Qty.KeyDown += (sender, e) =>
                {
                    MainFormControlHelper.AdjustQuantityByKey_Quantity(sender, e,
                        Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black,
                        Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red);
                };

                AdvancedInventory_Single_TextBox_Count.Text = "Enter How Many Transactions";
                AdvancedInventory_Single_TextBox_Count.TextChanged += (s, e) =>
                {
                    ValidateQtyTextBox(AdvancedInventory_Single_TextBox_Count);
                    UpdateSingleSaveButtonState();
                    LoggingUtility.Log("Single Count TextBox changed.");
                };
                AdvancedInventory_Single_TextBox_Count.Leave += (s, e) =>
                {
                    ValidateQtyTextBox(AdvancedInventory_Single_TextBox_Count);
                };

                AdvancedInventory_Single_TextBox_Count.Enter +=
                    (s, e) => AdvancedInventory_Single_TextBox_Count.SelectAll();
                AdvancedInventory_Single_TextBox_Count.Click +=
                    (s, e) => AdvancedInventory_Single_TextBox_Count.SelectAll();
                AdvancedInventory_Single_TextBox_Count.KeyDown += (sender, e) =>
                {
                    MainFormControlHelper.AdjustQuantityByKey_Transfers(sender, e,
                        Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black,
                        Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red);
                };

                // Multi-Location tab TextChanged events for button state updates
                AdvancedInventory_MultiLoc_TextBox_Part.TextChanged += (s, e) =>
                {
                    UpdateMultiSaveButtonState();
                    LoggingUtility.Log("MultiLoc Part TextBox changed.");
                };

                AdvancedInventory_MultiLoc_TextBox_Op.TextChanged += (s, e) =>
                {
                    UpdateMultiSaveButtonState();
                    LoggingUtility.Log("MultiLoc Op TextBox changed.");
                };

                AdvancedInventory_MultiLoc_TextBox_Loc.TextChanged += (s, e) =>
                {
                    UpdateMultiSaveButtonState();
                    LoggingUtility.Log("MultiLoc Loc TextBox changed.");
                };

                AdvancedInventory_MultiLoc_TextBox_Qty.Text = "";
                AdvancedInventory_MultiLoc_TextBox_Qty.TextChanged += (s, e) =>
                {
                    ValidateQtyTextBox(AdvancedInventory_MultiLoc_TextBox_Qty);
                    UpdateMultiSaveButtonState();
                    LoggingUtility.Log("MultiLoc Qty TextBox changed.");
                };
                AdvancedInventory_MultiLoc_TextBox_Qty.Leave += (s, e) =>
                {
                    ValidateQtyTextBox(AdvancedInventory_MultiLoc_TextBox_Qty);
                };

                // F4 Button Click Handlers - Single Tab
                AdvancedInventory_Single_Button_PartF4.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(AdvancedInventory_Single_TextBox_Part.Text))
                    {
                        // Empty: Trigger F4 dropdown
                        AdvancedInventory_Single_TextBox_Part.Focus();
                        SendKeys.Send("{F4}");
                    }
                    else
                    {
                        // Has text: Trigger Enter (move to next field)
                        AdvancedInventory_Single_TextBox_Op.Focus();
                    }
                };

                AdvancedInventory_Single_Button_OperationF4.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(AdvancedInventory_Single_TextBox_Op.Text))
                    {
                        // Empty: Trigger F4 dropdown
                        AdvancedInventory_Single_TextBox_Op.Focus();
                        SendKeys.Send("{F4}");
                    }
                    else
                    {
                        // Has text: Trigger Enter (move to next field)
                        AdvancedInventory_Single_TextBox_Loc.Focus();
                    }
                };

                AdvancedInventory_Single_Button_LocationF4.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(AdvancedInventory_Single_TextBox_Loc.Text))
                    {
                        // Empty: Trigger F4 dropdown
                        AdvancedInventory_Single_TextBox_Loc.Focus();
                        SendKeys.Send("{F4}");
                    }
                    else
                    {
                        // Has text: Trigger Enter (move to next field)
                        AdvancedInventory_Single_TextBox_Qty.Focus();
                    }
                };

                // F4 Button Click Handlers - Multi-Location Tab
                AdvancedInventory_MultiLoc_Button_PartF4.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(AdvancedInventory_MultiLoc_TextBox_Part.Text))
                    {
                        // Empty: Trigger F4 dropdown
                        AdvancedInventory_MultiLoc_TextBox_Part.Focus();
                        SendKeys.Send("{F4}");
                    }
                    else
                    {
                        // Has text: Trigger Enter (move to next field)
                        AdvancedInventory_MultiLoc_TextBox_Op.Focus();
                    }
                };

                AdvancedInventory_MultiLoc_Button_OperationF4.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(AdvancedInventory_MultiLoc_TextBox_Op.Text))
                    {
                        // Empty: Trigger F4 dropdown
                        AdvancedInventory_MultiLoc_TextBox_Op.Focus();
                        SendKeys.Send("{F4}");
                    }
                    else
                    {
                        // Has text: Trigger Enter (move to next field)
                        AdvancedInventory_MultiLoc_TextBox_Loc.Focus();
                    }
                };

                AdvancedInventory_MultiLoc_Button_LocationF4.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(AdvancedInventory_MultiLoc_TextBox_Loc.Text))
                    {
                        // Empty: Trigger F4 dropdown
                        AdvancedInventory_MultiLoc_TextBox_Loc.Focus();
                        SendKeys.Send("{F4}");
                    }
                    else
                    {
                        // Has text: Trigger Enter (move to next field)
                        AdvancedInventory_MultiLoc_TextBox_Qty.Focus();
                    }
                };
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "Control_AdvancedInventory_WireUpEvents");
            }
        }

        #endregion

        #region Single Tab SuggestionTextBox Events

        /// <summary>
        /// Handles part selection from SuggestionTextBox for Single Entry tab.
        /// Checks if part requires color code and redirects to Inventory Tab if needed.
        /// Updates save button state.
        /// </summary>
        private void AdvancedInventory_Single_TextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedPart = e.SelectedValue;
            LoggingUtility.Log($"[Control_AdvancedInventory] Single Part selected: {selectedPart}");

            // Check if part requires color code and redirect if necessary
            HandleAdvancedInventoryRedirect(selectedPart);
            
            UpdateSingleSaveButtonState();
        }

        /// <summary>
        /// Handles operation selection from SuggestionTextBox for Single Entry tab.
        /// Updates save button state.
        /// </summary>
        private void AdvancedInventory_Single_TextBox_Op_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedOp = e.SelectedValue;
            LoggingUtility.Log($"[Control_AdvancedInventory] Single Op selected: {selectedOp}");
            UpdateSingleSaveButtonState();
        }

        /// <summary>
        /// Handles location selection from SuggestionTextBox for Single Entry tab.
        /// Updates save button state.
        /// </summary>
        private void AdvancedInventory_Single_TextBox_Loc_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedLoc = e.SelectedValue;
            LoggingUtility.Log($"[Control_AdvancedInventory] Single Loc selected: {selectedLoc}");
            UpdateSingleSaveButtonState();
        }

        /// <summary>
        /// Checks if the entered part requires color code and redirects to Inventory Tab if needed.
        /// Overload for SuggestionTextBox (string partId).
        /// </summary>
        /// <param name="partId">The part ID to check</param>
        private void HandleAdvancedInventoryRedirect(string partId)
        {
            if (string.IsNullOrWhiteSpace(partId))
                return;
            if (!Model_Application_Variables.ColorCodeParts.Contains(partId))
                return;

            var result = Service_ErrorHandler.ShowConfirmation(
                "This part requires color code entry. Redirect to Inventory Tab?",
                "Redirect Required",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Switch to Inventory Tab and pre-populate PartID
                if (MainFormInstance != null && MainFormInstance.MainForm_UserControl_InventoryTab != null)
                {
                    MainFormInstance.MainForm_UserControl_AdvancedInventory.Visible = false;
                    MainFormInstance.MainForm_UserControl_InventoryTab.Visible = true;
                    MainFormInstance.MainForm_TabControl.SelectedIndex = 0;
                    var invTab = MainFormInstance.MainForm_UserControl_InventoryTab;
                    if (invTab.Control_InventoryTab_TextBox_Part != null)
                    {
                        invTab.Control_InventoryTab_TextBox_Part.Text = partId;
                        invTab.Control_InventoryTab_TextBox_Part.Focus();
                        invTab.Control_InventoryTab_TextBox_Part.SelectAll();
                    }
                }
            }
            else
            {
                // Clear PartID and return focus
                AdvancedInventory_Single_TextBox_Part.Text = string.Empty;
                AdvancedInventory_Single_TextBox_Part.Focus();
            }
        }

        #endregion

        #region Multi-Location Tab SuggestionTextBox Events

        /// <summary>
        /// Handles part selection from SuggestionTextBox for Multi-Location tab.
        /// Checks if part requires color code and redirects to Inventory Tab if needed.
        /// Updates save button state and locks Part/Op fields after first location is added.
        /// </summary>
        private void AdvancedInventory_MultiLoc_TextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedPart = e.SelectedValue;
            LoggingUtility.Log($"[Control_AdvancedInventory] Multi-Loc Part selected: {selectedPart}");

            // Check if part requires color code and redirect if necessary
            HandleAdvancedInventoryRedirect(selectedPart);
            
            UpdateMultiSaveButtonState();
        }

        /// <summary>
        /// Handles operation selection from SuggestionTextBox for Multi-Location tab.
        /// Updates save button state.
        /// </summary>
        private void AdvancedInventory_MultiLoc_TextBox_Op_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedOp = e.SelectedValue;
            LoggingUtility.Log($"[Control_AdvancedInventory] Multi-Loc Op selected: {selectedOp}");
            UpdateMultiSaveButtonState();
        }

        /// <summary>
        /// Handles location selection from SuggestionTextBox for Multi-Location tab.
        /// Updates save button state.
        /// </summary>
        private void AdvancedInventory_MultiLoc_TextBox_Loc_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedLoc = e.SelectedValue;
            LoggingUtility.Log($"[Control_AdvancedInventory] Multi-Loc Loc selected: {selectedLoc}");
            UpdateMultiSaveButtonState();
        }

        #endregion

        #region ListView Delete Key Handlers

        /// <summary>
        /// Handles Delete key press on Single Entry ListView to remove selected items.
        /// Prompts user for confirmation before deletion.
        /// </summary>
        private void AdvancedInventory_Single_ListView_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && AdvancedInventory_Single_ListView_Preview.SelectedItems.Count > 0)
            {
                var result = Service_ErrorHandler.ShowConfirmation(
                    $"Are you sure you want to delete the selected row(s)?",
                    "Delete Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Remove selected items (iterate backwards to avoid index issues)
                    for (int i = AdvancedInventory_Single_ListView_Preview.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        AdvancedInventory_Single_ListView_Preview.Items.Remove(AdvancedInventory_Single_ListView_Preview.SelectedItems[i]);
                    }
                    
                    UpdateSingleSaveButtonState();
                    LoggingUtility.Log("[Control_AdvancedInventory] Deleted selected row(s) from Single ListView");
                }
                
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles Delete key press on Multi-Location ListView to remove selected items.
        /// Prompts user for confirmation before deletion.
        /// </summary>
        private void AdvancedInventory_MultiLoc_ListView_Preview_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && AdvancedInventory_MultiLoc_ListView_Preview.SelectedItems.Count > 0)
            {
                var result = Service_ErrorHandler.ShowConfirmation(
                    $"Are you sure you want to delete the selected row(s)?",
                    "Delete Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Remove selected items (iterate backwards to avoid index issues)
                    for (int i = AdvancedInventory_MultiLoc_ListView_Preview.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        AdvancedInventory_MultiLoc_ListView_Preview.Items.Remove(AdvancedInventory_MultiLoc_ListView_Preview.SelectedItems[i]);
                    }
                    
                    // Re-enable Part field if all items removed
                    if (AdvancedInventory_MultiLoc_ListView_Preview.Items.Count == 0)
                    {
                        AdvancedInventory_MultiLoc_TextBox_Part.Enabled = true;
                    }
                    
                    UpdateMultiSaveButtonState();
                    LoggingUtility.Log("[Control_AdvancedInventory] Deleted selected row(s) from Multi-Location ListView");
                }
                
                e.Handled = true;
            }
        }

        #endregion

        #region Validation and Utility Methods

        private static void InventoryTextBoxQty_TextChanged(TextBox textBox)
        {
            try
            {
                string text = textBox.Text.Trim();
                bool isValid = int.TryParse(text, out int qty) && qty > 0;
                if (isValid)
                {
                    textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                }
                else
                {
                    textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        textBox.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private void UpdateSingleSaveButtonState()
        {
            AdvancedInventory_Single_Button_Save.Enabled = AdvancedInventory_Single_ListView_Preview.Items.Count > 0;
            bool partValid = !string.IsNullOrWhiteSpace(AdvancedInventory_Single_TextBox_Part.Text);
            bool opValid = !string.IsNullOrWhiteSpace(AdvancedInventory_Single_TextBox_Op.Text);
            bool locValid = !string.IsNullOrWhiteSpace(AdvancedInventory_Single_TextBox_Loc.Text);
            bool qtyValid = int.TryParse(AdvancedInventory_Single_TextBox_Qty.Text.Trim(), out int qty) && qty > 0;
            bool countValid = int.TryParse(AdvancedInventory_Single_TextBox_Count.Text.Trim(), out int count) &&
                              count > 0;

            AdvancedInventory_Single_Button_Send.Enabled = partValid && opValid && locValid && qtyValid && countValid;
        }

        private void UpdateMultiSaveButtonState()
        {
            bool partValid = !string.IsNullOrWhiteSpace(AdvancedInventory_MultiLoc_TextBox_Part.Text);
            bool opValid = !string.IsNullOrWhiteSpace(AdvancedInventory_MultiLoc_TextBox_Op.Text);
            bool locValid = !string.IsNullOrWhiteSpace(AdvancedInventory_MultiLoc_TextBox_Loc.Text);
            bool qtyValid = int.TryParse(AdvancedInventory_MultiLoc_TextBox_Qty.Text.Trim(), out int qty) && qty > 0;
            AdvancedInventory_MultiLoc_Button_AddLoc.Enabled = partValid && opValid && locValid && qtyValid;
            AdvancedInventory_MultiLoc_Button_SaveAll.Enabled =
                AdvancedInventory_MultiLoc_ListView_Preview.Items.Count > 0 && partValid && opValid;
        }

        /// <summary>
        /// Validates quantity input in TextBox and applies appropriate foreground color based on validation result.
        /// </summary>
        /// <param name="textBox">The TextBox control containing quantity input to validate</param>
        /// <param name="placeholder">Placeholder text to display if input is invalid or empty</param>
        /// <remarks>
        /// Validation rules: Quantity must be a positive integer (> 0).
        /// Valid input: Sets foreground to UserUiColors.TextBoxForeColor (black).
        /// Invalid input: Sets foreground to UserUiColors.TextBoxErrorForeColor (red) and resets to placeholder.
        /// Used for quantity validation in both single and multi-location inventory entry forms.
        /// </remarks>
        public static void ValidateQtyTextBox(TextBox textBox)
        {
            string text = textBox.Text.Trim();
            bool isValid = int.TryParse(text, out int value) && value > 0;
            if (isValid)
            {
                textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
            }
            else
            {
                textBox.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = "";
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (AdvancedInventory_TabControl.SelectedTab == AdvancedInventory_TabControl_Single)
                {
                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Send)
                    {
                        if (AdvancedInventory_Single_Button_Send.Visible &&
                            AdvancedInventory_Single_Button_Send.Enabled)
                        {
                            AdvancedInventory_Single_Button_Send.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Save)
                    {
                        if (AdvancedInventory_Single_Button_Save.Visible &&
                            AdvancedInventory_Single_Button_Save.Enabled)
                        {
                            AdvancedInventory_Single_Button_Save.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Reset)
                    {
                        if (AdvancedInventory_Single_Button_Reset.Visible &&
                            AdvancedInventory_Single_Button_Reset.Enabled)
                        {
                            AdvancedInventory_Single_Button_Reset.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Normal)
                    {
                        if (AdvancedInventory_Single_Button_Normal.Visible &&
                            AdvancedInventory_Single_Button_Normal.Enabled)
                        {
                            AdvancedInventory_Single_Button_Normal.PerformClick();
                            return true;
                        }
                    }
                }

                if (AdvancedInventory_TabControl.SelectedTab == AdvancedInventory_TabControl_MultiLoc)
                {
                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Multi_AddLoc)
                    {
                        if (AdvancedInventory_MultiLoc_Button_AddLoc.Visible &&
                            AdvancedInventory_MultiLoc_Button_AddLoc.Enabled)
                        {
                            AdvancedInventory_MultiLoc_Button_AddLoc.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Multi_SaveAll)
                    {
                        if (AdvancedInventory_MultiLoc_Button_SaveAll.Visible &&
                            AdvancedInventory_MultiLoc_Button_SaveAll.Enabled)
                        {
                            AdvancedInventory_MultiLoc_Button_SaveAll.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Multi_Reset)
                    {
                        if (AdvancedInventory_MultiLoc_Button_Reset.Visible &&
                            AdvancedInventory_MultiLoc_Button_Reset.Enabled)
                        {
                            AdvancedInventory_MultiLoc_Button_Reset.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Multi_Normal)
                    {
                        if (AdvancedInventory_Multi_Button_Normal.Visible &&
                            AdvancedInventory_Multi_Button_Normal.Enabled)
                        {
                            AdvancedInventory_Multi_Button_Normal.PerformClick();
                            return true;
                        }
                    }
                }

                if (AdvancedInventory_TabControl.SelectedTab == AdvancedInventory_TabControl_Import)
                {
                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Import_OpenExcel)
                    {
                        if (AdvancedInventory_Import_Button_OpenExcel.Visible &&
                            AdvancedInventory_Import_Button_OpenExcel.Enabled)
                        {
                            AdvancedInventory_Import_Button_OpenExcel.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Import_ImportExcel)
                    {
                        if (AdvancedInventory_Import_Button_ImportExcel.Visible &&
                            AdvancedInventory_Import_Button_ImportExcel.Enabled)
                        {
                            AdvancedInventory_Import_Button_ImportExcel.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Import_Save)
                    {
                        if (AdvancedInventory_Import_Button_Save.Visible &&
                            AdvancedInventory_Import_Button_Save.Enabled)
                        {
                            AdvancedInventory_Import_Button_Save.PerformClick();
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_AdvInv_Import_Normal)
                    {
                        if (AdvancedInventory_Import_Button_Normal.Visible &&
                            AdvancedInventory_Import_Button_Normal.Enabled)
                        {
                            AdvancedInventory_Import_Button_Normal.PerformClick();
                            return true;
                        }
                    }
                }

                if (keyData == Core_WipAppVariables.Shortcut_Remove_Advanced)
                {
                    if (MainFormInstance != null && MainFormInstance.MainForm_UserControl_AdvancedRemove != null)
                    {
                        MainFormInstance.MainForm_UserControl_AdvancedInventory.Visible = false;
                        MainFormInstance.MainForm_UserControl_AdvancedRemove.Visible = true;
                        MainFormInstance.MainForm_TabControl.SelectedIndex = 2;
                        return true;
                    }
                }

                if (keyData == Keys.Enter)
                {
                    SelectNextControl(ActiveControl, true, true, true, true);
                    return true;
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "Control_AdvancedInventory_ProcessCmdKey");
                return false;
            }
        }

        #endregion

        #region Single Entry Actions

        private async Task AdvancedInventory_Single_HardResetAsync()
        {
            AdvancedInventory_Single_Button_Reset.Enabled = false;
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Advanced Inventory (Single)...");
                Debug.WriteLine("[DEBUG] AdvancedInventory Single HardReset - start");

                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for hard reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                _progressHelper?.UpdateProgress(30, "Resetting data tables...");

                Debug.WriteLine("[DEBUG] Resetting and refreshing all TextBox DataTables");
                await Helper_UI_ComboBoxes.ResetAndRefreshAllDataTablesAsync();
                Debug.WriteLine("[DEBUG] DataTables reset complete");

                _progressHelper?.UpdateProgress(60, "Resetting UI fields...");
                Debug.WriteLine("[DEBUG] Resetting UI fields");
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_MultiLoc_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Quantity");
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Quantity");
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Count,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Enter How Many Times");
                AdvancedInventory_Single_RichTextBox_Notes.Text = string.Empty;
                AdvancedInventory_Single_ListView_Preview.Items.Clear();

                UpdateSingleSaveButtonState();

                Debug.WriteLine("[DEBUG] AdvancedInventory Single HardReset - end");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedInventory Single HardReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_Single_HardResetAsync");
            }
            finally
            {
                Debug.WriteLine("[DEBUG] AdvancedInventory Single HardReset button re-enabled");
                AdvancedInventory_Single_Button_Reset.Enabled = true;
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Restoring status strip after reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                    _progressHelper?.HideProgress();
                }
            }
        }

        private void AdvancedInventory_Single_SoftReset()
        {
            AdvancedInventory_Single_Button_Reset.Enabled = false;
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
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_MultiLoc_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red,
                    "Enter Quantity");
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red,
                    "Enter Quantity");
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Count,
                    Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red,
                    "Enter Enter How Many Times");
                AdvancedInventory_Single_RichTextBox_Notes.Text = string.Empty;
                AdvancedInventory_Single_ListView_Preview.Items.Clear();

                UpdateSingleSaveButtonState();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedInventory Single SoftReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_Single_SoftReset");
            }
            finally
            {
                Debug.WriteLine("[DEBUG] AdvancedInventory Single SoftReset button re-enabled");
                AdvancedInventory_Single_Button_Reset.Enabled = true;
                AdvancedInventory_Single_TextBox_Part.Focus();
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                }
            }
        }

        private async void AdvancedInventory_Single_Button_Reset_Click(object? sender, EventArgs e)
        {
            AdvancedInventory_Single_Button_Reset.Enabled = false;
            try
            {
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    await AdvancedInventory_Single_HardResetAsync();
                }
                else
                {
                    AdvancedInventory_Single_SoftReset();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedInventory Single Reset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_Single_Button_Reset_Click");
            }
        }

        private async void AdvancedInventory_Single_Button_Save_Click(object? sender, EventArgs e)
        {
            try
            {
                LoggingUtility.Log("AdvancedInventory_Single_Button_Save_Click entered.");

                if (AdvancedInventory_Single_ListView_Preview.Items.Count == 0)
                {
                    Service_ErrorHandler.ShowWarning(
                        "No items to inventory. Please add at least one item to the list.",
                        "No Items",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                HashSet<string> partIds = new(StringComparer.OrdinalIgnoreCase);
                HashSet<string> operations = new(StringComparer.OrdinalIgnoreCase);
                HashSet<string> locations = new(StringComparer.OrdinalIgnoreCase);
                List<ListViewItem> itemsToRemove = new();
                int totalQty = 0;
                int savedCount = 0;
                bool anyFailure = false;

                foreach (ListViewItem item in AdvancedInventory_Single_ListView_Preview.Items)
                {
                    string partId = item.SubItems.Count > 0 ? item.SubItems[0].Text : string.Empty;
                    string op = item.SubItems.Count > 1 ? item.SubItems[1].Text : string.Empty;
                    string loc = item.SubItems.Count > 2 ? item.SubItems[2].Text : string.Empty;
                    string qtyText = item.SubItems.Count > 3 ? item.SubItems[3].Text : string.Empty;
                    string notes = item.SubItems.Count > 4 ? item.SubItems[4].Text : string.Empty;

                    if (string.IsNullOrWhiteSpace(partId) || string.IsNullOrWhiteSpace(op) ||
                        string.IsNullOrWhiteSpace(loc) || !int.TryParse(qtyText, out int qty) || qty <= 0)
                    {
                        anyFailure = true;
                        item.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                        Service_ErrorHandler.HandleValidationError(
                            "Inventory list contains invalid data. Please review entries highlighted in red.",
                            "Inventory List",
                            controlName: nameof(Control_AdvancedInventory));
                        continue;
                    }

                    Model_Application_Variables.PartId = partId;
                    Model_Application_Variables.Operation = op;
                    Model_Application_Variables.Location = loc;
                    Model_Application_Variables.Notes = notes;
                    Model_Application_Variables.InventoryQuantity = qty;
                    Model_Application_Variables.User ??= Environment.UserName;
                    Model_Application_Variables.PartType ??= string.Empty;

                    Model_Dao_Result<int> addResult = await Dao_Inventory.AddInventoryItemAsync(
                        partId,
                        loc,
                        op,
                        qty,
                        Model_Application_Variables.PartType ?? string.Empty,
                        Model_Application_Variables.User,
                        null,
                        notes,
                        null,  // colorCode
                        null,  // workOrder
                        true);

                    if (!addResult.IsSuccess)
                    {
                        anyFailure = true;
                        HandleDaoFailure(
                            addResult,
                            $"Failed to save inventory for part {partId} at {loc} ({op}).",
                            CreateInventoryContext(partId, op, loc, qty, notes),
                            nameof(AdvancedInventory_Single_Button_Save_Click));
                        item.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                        continue;
                    }

                    partIds.Add(partId);
                    operations.Add(op);
                    locations.Add(loc);
                    totalQty += qty;
                    savedCount++;
                    itemsToRemove.Add(item);
                }

                foreach (ListViewItem item in itemsToRemove)
                {
                    AdvancedInventory_Single_ListView_Preview.Items.Remove(item);
                }

                UpdateSingleSaveButtonState();

                if (savedCount > 0)
                {
                    Service_ErrorHandler.ShowInformation(
                        $"{savedCount} inventory transaction(s) saved successfully.",
                        "Inventory Saved",
                        controlName: nameof(Control_AdvancedInventory));

                    LoggingUtility.Log($"Saved {savedCount} inventory transaction(s) from ListView.");

                    if (MainFormInstance != null)
                    {
                        string time = DateTime.Now.ToString("hh:mm tt");
                        string locDisplay = locations.Count > 1 ? "Multiple Locations" : locations.FirstOrDefault() ?? string.Empty;

                        if (partIds.Count == 1 && operations.Count == 1)
                        {
                            MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                                $"Last Inventoried: {partIds.First()} (Op: {operations.First()}), Location: {locDisplay}, Quantity: {totalQty} @ {time}";
                        }
                        else if (partIds.Count == 1 && operations.Count > 1)
                        {
                            MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                                $"Last Inventoried: {partIds.First()} (Multiple Ops), Location: {locDisplay}, Quantity: {totalQty} @ {time}";
                        }
                        else
                        {
                            MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                                $"Last Inventoried: Multiple Part IDs, Location: {locDisplay}, Quantity: Multiple @ {time}";
                        }
                    }

                    if (AdvancedInventory_Single_ListView_Preview.Items.Count == 0)
                    {
                        AdvancedInventory_Single_SoftReset();
                    }
                }

                if (anyFailure && savedCount == 0)
                {
                    Service_ErrorHandler.ShowWarning(
                        "No inventory transactions were saved. Resolve highlighted issues and try again.",
                        "Inventory Save Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else if (anyFailure)
                {
                    Service_ErrorHandler.ShowWarning(
                        "Some inventory transactions could not be saved. Review highlighted entries and logs before retrying.",
                        "Partial Inventory Save",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_Single_Button_Save_Click");
            }
        }

        private void AdvancedInventory_Single_Button_Send_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("Send button clicked");
                LoggingUtility.Log("Send button clicked");

                string partId = AdvancedInventory_Single_TextBox_Part.Text?.Trim() ?? string.Empty;
                string op = AdvancedInventory_Single_TextBox_Op.Text?.Trim() ?? string.Empty;
                string loc = AdvancedInventory_Single_TextBox_Loc.Text?.Trim() ?? string.Empty;
                string qtyText = AdvancedInventory_Single_TextBox_Qty.Text.Trim();
                string countText = AdvancedInventory_Single_TextBox_Count.Text.Trim();
                string notes = AdvancedInventory_Single_RichTextBox_Notes.Text.Trim();

                Debug.WriteLine($"partId: {partId}, op: {op}, loc: {loc}, qtyText: {qtyText}, countText: {countText}");

                if (string.IsNullOrWhiteSpace(partId))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a valid Part.", "Part");
                    AdvancedInventory_Single_TextBox_Part.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(op))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a valid Operation.", "Operation");
                    AdvancedInventory_Single_TextBox_Op.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(loc))
                {
                    Service_ErrorHandler.HandleValidationError("Please select a valid Location.", "Location");
                    AdvancedInventory_Single_TextBox_Loc.Focus();
                    return;
                }

                if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                {
                    Service_ErrorHandler.HandleValidationError("Please enter a valid quantity.", "Quantity");
                    AdvancedInventory_Single_TextBox_Qty.Focus();
                    return;
                }

                if (!int.TryParse(countText, out int count) || count <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please enter a valid transaction count.",
                        "Transaction Count");
                    AdvancedInventory_Single_TextBox_Count.Focus();
                    return;
                }

                for (int i = 0; i < count; i++)
                {
                    // Column order: Location, Part ID (with Op), Quantity
                    ListViewItem listViewItem = new([
                        loc,
                        $"{partId} (Op: {op})",
                        qty.ToString()
                    ]);
                    AdvancedInventory_Single_ListView_Preview.Items.Add(listViewItem);
                    Debug.WriteLine(
                        $"Added item to ListView: Part={partId}, Op={op}, Loc={loc}, Qty={qty}, Notes={notes}");
                }

                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Quantity");
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Count,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Enter How Many Times");
                AdvancedInventory_Single_RichTextBox_Notes.Text = string.Empty;

                AdvancedInventory_Single_TextBox_Part.Focus();

                UpdateSingleSaveButtonState();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_Single_Button_Send_Click");
            }
        }

        private void AdvancedInventory_Button_Normal_Click(object? sender, EventArgs e)
        {
            try
            {
                if (Service_Timer_VersionChecker.MainFormInstance is null)
                {
                    LoggingUtility.Log("MainForm instance is null, cannot open Advanced Inventory Entry.");
                    return;
                }

                if (MainFormInstance is not null)
                {
                    MainFormInstance.MainForm_UserControl_InventoryTab.Visible = true;
                    MainFormInstance.MainForm_UserControl_AdvancedInventory.Visible = false;
                    MainFormInstance.MainForm_TabControl.SelectedIndex = 0;
                    Control_InventoryTab? invTab = MainFormInstance.MainForm_UserControl_InventoryTab;
                    if (invTab is not null)
                    {
                        // Part, Operation, and Location now use SuggestionTextBox (TextBox-based)
                        var part = invTab.Control_InventoryTab_TextBox_Part;
                        var op = invTab.GetType().GetField("Control_InventoryTab_TextBox_Operation",
                                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.GetValue(invTab) as Controls.Shared.SuggestionTextBox;
                        var loc = invTab.GetType().GetField("Control_InventoryTab_TextBox_Location",
                                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.GetValue(invTab) as Controls.Shared.SuggestionTextBox;
                        if (part is not null)
                        {
                            part.Text = string.Empty;
                            part.Focus();
                            part.SelectAll();
                            part.BackColor = Model_Application_Variables.UserUiColors.ControlFocusedBackColor ?? Color.LightBlue;
                        }

                        if (op is not null)
                        {
                            op.Text = string.Empty;
                        }

                        if (loc is not null)
                        {
                            loc.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "Control_InventoryTab_Button_AdvancedInventory_Click");
            }
        }

        #endregion

        #region Multi-Location Actions

        private async Task AdvancedInventory_MultiLoc_HardResetAsync()
        {
            AdvancedInventory_MultiLoc_Button_Reset.Enabled = false;
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Advanced Inventory (MultiLoc)...");
                Debug.WriteLine("[DEBUG] AdvancedInventory MultiLoc HardReset - start");
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for hard reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                _progressHelper?.UpdateProgress(30, "Resetting data tables...");
                Debug.WriteLine("[DEBUG] Resetting UI fields");

                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_MultiLoc_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Quantity");
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Quantity");
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Count,
                    Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red,
                    "Enter Enter How Many Times");
                AdvancedInventory_MultiLoc_RichTextBox_Notes.Text = string.Empty;
                AdvancedInventory_MultiLoc_ListView_Preview.Items.Clear();
                AdvancedInventory_MultiLoc_TextBox_Part.Enabled = true;
                AdvancedInventory_MultiLoc_TextBox_Op.Enabled = true;

                UpdateMultiSaveButtonState();

                Debug.WriteLine("[DEBUG] AdvancedInventory MultiLoc HardReset - end");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedInventory MultiLoc HardReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_MultiLoc_HardResetAsync");
            }
            finally
            {
                Debug.WriteLine("[DEBUG] AdvancedInventory MultiLoc HardReset button re-enabled");
                AdvancedInventory_MultiLoc_Button_Reset.Enabled = true;
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Restoring status strip after reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                    _progressHelper?.HideProgress();
                }
            }
        }

        private void AdvancedInventory_MultiLoc_SoftReset()
        {
            AdvancedInventory_MultiLoc_Button_Reset.Enabled = false;
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
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_MultiLoc_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red,
                    "Enter Quantity");
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Part);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Op);
                Helper_SuggestionTextBox.Clear(AdvancedInventory_Single_TextBox_Loc);
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Qty,
                    Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red,
                    "Enter Quantity");
                MainFormControlHelper.ResetTextBox(AdvancedInventory_Single_TextBox_Count,
                    Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red,
                    "Enter Enter How Many Times");
                AdvancedInventory_MultiLoc_RichTextBox_Notes.Text = string.Empty;
                AdvancedInventory_MultiLoc_ListView_Preview.Items.Clear();
                AdvancedInventory_MultiLoc_TextBox_Part.Enabled = true;
                AdvancedInventory_MultiLoc_TextBox_Op.Enabled = true;

                UpdateMultiSaveButtonState();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedInventory MultiLoc SoftReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_MultiLoc_SoftReset");
            }
            finally
            {
                Debug.WriteLine("[DEBUG] AdvancedInventory MultiLoc SoftReset button re-enabled");
                AdvancedInventory_MultiLoc_Button_Reset.Enabled = true;
                AdvancedInventory_MultiLoc_TextBox_Part.Focus();
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                }
            }
        }

        private async void AdvancedInventory_MultiLoc_Button_Reset_Click(object? sender, EventArgs e)
        {
            AdvancedInventory_MultiLoc_Button_Reset.Enabled = false;
            try
            {
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    await AdvancedInventory_MultiLoc_HardResetAsync();
                }
                else
                {
                    AdvancedInventory_MultiLoc_SoftReset();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in AdvancedInventory MultiLoc Reset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_MultiLoc_Button_Reset_Click");
            }
        }

        private void AdvancedInventory_MultiLoc_Button_AddLoc_Click(object? sender, EventArgs e)
        {
            try
            {
                LoggingUtility.Log("AdvancedInventory_MultiLoc_Button_AddLoc_Click entered.");

                string partId = AdvancedInventory_MultiLoc_TextBox_Part.Text?.Trim() ?? string.Empty;
                string op = AdvancedInventory_MultiLoc_TextBox_Op.Text?.Trim() ?? string.Empty;
                string loc = AdvancedInventory_MultiLoc_TextBox_Loc.Text?.Trim() ?? string.Empty;
                string qtyText = AdvancedInventory_MultiLoc_TextBox_Qty.Text.Trim();
                string notes = AdvancedInventory_MultiLoc_RichTextBox_Notes.Text.Trim();

                if (string.IsNullOrWhiteSpace(partId))
                {
                    Service_ErrorHandler.HandleValidationError(@"Please select a valid Part.", nameof(AdvancedInventory_MultiLoc_TextBox_Part));
                    AdvancedInventory_MultiLoc_TextBox_Part.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(op))
                {
                    Service_ErrorHandler.HandleValidationError(@"Please select a valid Operation.", nameof(AdvancedInventory_MultiLoc_TextBox_Op));
                    AdvancedInventory_MultiLoc_TextBox_Op.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(loc))
                {
                    Service_ErrorHandler.HandleValidationError(@"Please select a valid Location.", nameof(AdvancedInventory_MultiLoc_TextBox_Loc));
                    AdvancedInventory_MultiLoc_TextBox_Loc.Focus();
                    return;
                }

                if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(@"Please enter a valid quantity.", nameof(AdvancedInventory_MultiLoc_TextBox_Qty));
                    AdvancedInventory_MultiLoc_TextBox_Qty.Focus();
                    return;
                }

                foreach (ListViewItem item in AdvancedInventory_MultiLoc_ListView_Preview.Items)
                {
                    // Check if location already exists in preview list (Location is column 0)
                    if (string.Equals(item.SubItems[0].Text, loc, StringComparison.OrdinalIgnoreCase))
                    {
                        Service_ErrorHandler.HandleValidationError(@"This location has already been added.", nameof(AdvancedInventory_MultiLoc_TextBox_Loc));
                        AdvancedInventory_MultiLoc_TextBox_Loc.Focus();
                        return;
                    }
                }

                // Column order: Location, Part ID (with Op), Quantity
                ListViewItem listViewItem = new([
                    loc,
                    $"{partId} (Op: {op})",
                    qty.ToString()
                ]);
                AdvancedInventory_MultiLoc_ListView_Preview.Items.Add(listViewItem);

                LoggingUtility.Log(
                    $"Added MultiLoc entry: PartId = {partId}, Op = {op}, Loc={loc}, Qty={qty}, Notes={notes}");

                if (AdvancedInventory_MultiLoc_ListView_Preview.Items.Count == 1)
                {
                    AdvancedInventory_MultiLoc_TextBox_Part.Enabled = false;
                }

                Helper_SuggestionTextBox.Clear(AdvancedInventory_MultiLoc_TextBox_Loc);
                AdvancedInventory_MultiLoc_TextBox_Loc.Focus();

                UpdateMultiSaveButtonState();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                _ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_MultiLoc_Button_AddLoc_Click");
            }
        }

        private void AdvancedInventory_MultiLoc_Label_Notes_Click(object? sender, EventArgs e)
        {
            // Event handler for Notes label click - no action needed
        }

        private async void AdvancedInventory_MultiLoc_Button_SaveAll_Click(object? sender, EventArgs e)
        {
            try
            {
                LoggingUtility.Log("AdvancedInventory_MultiLoc_Button_SaveAll_Click entered.");

                if (AdvancedInventory_MultiLoc_ListView_Preview.Items.Count == 0)
                {
                    Service_ErrorHandler.HandleValidationError(@"Please add at least one location entry before saving.", nameof(AdvancedInventory_MultiLoc_ListView_Preview));
                    return;
                }

                string partId = AdvancedInventory_MultiLoc_TextBox_Part.Text?.Trim() ?? string.Empty;
                string op = AdvancedInventory_MultiLoc_TextBox_Op.Text?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(partId))
                {
                    Service_ErrorHandler.HandleValidationError(@"Please select a valid Part.", nameof(AdvancedInventory_MultiLoc_TextBox_Part));
                    AdvancedInventory_MultiLoc_TextBox_Part.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(op))
                {
                    Service_ErrorHandler.HandleValidationError(@"Please select a valid Operation.", nameof(AdvancedInventory_MultiLoc_TextBox_Op));
                    AdvancedInventory_MultiLoc_TextBox_Op.Focus();
                    return;
                }

                HashSet<string> locations = new();
                int totalQty = 0;
                int savedCount = 0;
                foreach (ListViewItem item in AdvancedInventory_MultiLoc_ListView_Preview.Items)
                {
                    // ListView columns: Location (0), Part ID with Op (1), Quantity (2)
                    string loc = item.SubItems[0].Text;
                    string qtyText = item.SubItems[2].Text;
                    string notes = AdvancedInventory_MultiLoc_RichTextBox_Notes.Text.Trim();

                    if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                    {
                        LoggingUtility.LogApplicationError(
                            new Exception($"Invalid quantity for location '{loc}': '{qtyText}'"));
                        continue;
                    }

                    Model_Application_Variables.PartId = partId;
                    Model_Application_Variables.Operation = op;
                    Model_Application_Variables.Location = loc;
                    Model_Application_Variables.Notes = notes;
                    Model_Application_Variables.InventoryQuantity = qty;
                    Model_Application_Variables.User ??= Environment.UserName;
                    Model_Application_Variables.PartType ??= "";

                    // Pass null/empty for batchNumber for unique batch per transaction
                    await Dao_Inventory.AddInventoryItemAsync(
                        partId,
                        loc,
                        op,
                        qty,
                        Model_Application_Variables.PartType ?? "",
                        Model_Application_Variables.User,
                        null, // <-- ensure unique batch number
                        notes,
                        null,  // colorCode
                        null,  // workOrder
                        true);

                    locations.Add(loc);
                    totalQty += qty;
                    savedCount++;
                }

                Service_ErrorHandler.ShowInformation(
                    $@"{savedCount} inventory transaction(s) saved successfully.",
                    "Multi-Location Save Success");

                LoggingUtility.Log(
                    $"Saved {savedCount} multi-location inventory transaction(s) for Part: {partId}, Op: {op}");

                if (MainFormInstance != null && savedCount > 0)
                {
                    string time = DateTime.Now.ToString("hh:mm tt");
                    string locDisplay = locations.Count > 1 ? "Multiple Locations" : locations.FirstOrDefault() ?? "";
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                        $"Last Inventoried: {partId} (Op: {op}), Location: {locDisplay}, Quantity: {totalQty} @ {time}";
                }

                AdvancedInventory_MultiLoc_Button_Reset_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "AdvancedInventory_MultiLoc_Button_SaveAll_Click");
            }
        }

        #endregion

        #region Excel Export/Import Helpers

        private static async Task<string> GetWipAppExcelUserFolderAsync()
        {
            string? server = new MySqlConnectionStringBuilder(Model_Application_Variables.ConnectionString).Server;
            string userName = Model_Application_Variables.User ?? Environment.UserName;
            
            // Sanitize username to prevent path traversal
            userName = Path.GetInvalidFileNameChars()
                .Aggregate(userName, (current, c) => current.Replace(c.ToString(), "_"));
            
            string logFilePath = await Helper_Database_Variables.GetLogFilePathAsync(server, userName);
            string logDir = Directory.GetParent(logFilePath)?.Parent?.FullName ?? "";
            string excelRoot = Path.Combine(logDir, "WIP App Excel Files");
            string userFolder = Path.Combine(excelRoot, userName);
            
            // Validate the path is within the expected directory (prevent directory traversal)
            string fullExcelRoot = Path.GetFullPath(excelRoot);
            string fullUserFolder = Path.GetFullPath(userFolder);
            if (!fullUserFolder.StartsWith(fullExcelRoot, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityException($"Path traversal detected: {userFolder}");
            }
            
            if (!Directory.Exists(userFolder))
            {
                Directory.CreateDirectory(userFolder);
            }

            return userFolder;
        }

        private static async Task<string> GetUserExcelFilePathAsync()
        {
            string userFolder = await GetWipAppExcelUserFolderAsync();
            string userName = Model_Application_Variables.User ?? Environment.UserName;
            
            // Sanitize username for filename
            userName = Path.GetInvalidFileNameChars()
                .Aggregate(userName, (current, c) => current.Replace(c.ToString(), "_"));
            
            string fileName = $"{userName}_import.xlsx";
            string filePath = Path.Combine(userFolder, fileName);
            
            // Validate the file path is within the user folder (prevent directory traversal)
            string fullUserFolder = Path.GetFullPath(userFolder);
            string fullFilePath = Path.GetFullPath(filePath);
            if (!fullFilePath.StartsWith(fullUserFolder, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityException($"Path traversal detected: {filePath}");
            }
            
            return filePath;
        }

        /// <summary>
        /// Ensures the user's Excel import file exists. Creates it with template structure if missing.
        /// Called during control initialization.
        /// </summary>
        private static async Task EnsureExcelFileExistsAsync()
        {
            try
            {
                string excelPath = await GetUserExcelFilePathAsync();
                
                if (!File.Exists(excelPath))
                {
                    LoggingUtility.Log($"[Control_AdvancedInventory] Excel file not found at {excelPath}, creating template...");
                    
                    // Create the directory if it doesn't exist
                    string? directory = Path.GetDirectoryName(excelPath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    
                    // Create Excel file with template structure
                    using (XLWorkbook workbook = new())
                    {
                        IXLWorksheet worksheet = workbook.Worksheets.Add("Tab 1");
                        
                        // Add header row
                        worksheet.Cell(1, 1).Value = "Part";
                        worksheet.Cell(1, 2).Value = "Operation";
                        worksheet.Cell(1, 3).Value = "Location";
                        worksheet.Cell(1, 4).Value = "Quantity";
                        worksheet.Cell(1, 5).Value = "Notes";
                        
                        // Format header row
                        var headerRange = worksheet.Range(1, 1, 1, 5);
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                        
                        // Auto-fit columns
                        worksheet.Columns().AdjustToContents();
                        
                        workbook.SaveAs(excelPath);
                    }
                    
                    LoggingUtility.Log($"[Control_AdvancedInventory] Excel template created successfully at {excelPath}");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                LoggingUtility.Log($"[Control_AdvancedInventory] Error ensuring Excel file exists: {ex.Message}");
            }
        }

        // Fix for CS8600: Converting null literal or possible null value to non-nullable type.
        // The problematic line is:
        // string excelPath = GetUserExcelFilePathAsync().ToString();
        // GetUserExcelFilePathAsync() returns a Task<string>, so .ToString() does not return the path, but the type name.
        // The correct way is to await the Task and ensure the result is not null.

        private async void AdvancedInventory_Import_Button_OpenExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                string excelPath = await GetUserExcelFilePathAsync();
                if (!File.Exists(excelPath))
                {
                    string? userFolder = Path.GetDirectoryName(excelPath);
                    if (!string.IsNullOrEmpty(userFolder) && !Directory.Exists(userFolder))
                    {
                        Directory.CreateDirectory(userFolder!);
                    }

                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Controls",
                        "MainForm", "WIPAppTemplate.xlsx");
                    
                    // Validate template path is within application directory (prevent directory traversal)
                    string fullBaseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
                    string fullTemplatePath = Path.GetFullPath(templatePath);
                    if (!fullTemplatePath.StartsWith(fullBaseDir, StringComparison.OrdinalIgnoreCase))
                    {
                        Service_ErrorHandler.HandleException(
                            new SecurityException($"Template path validation failed: {templatePath}"),
                            Enum_ErrorSeverity.High,
                            controlName: nameof(AdvancedInventory_Import_Button_OpenExcel));
                        return;
                    }
                    
                    if (File.Exists(templatePath))
                    {
                        File.Copy(templatePath, excelPath, false);
                    }
                    else
                    {
                        Service_ErrorHandler.HandleException(
                            new FileNotFoundException($"Excel template not found: {templatePath}"),
                            Enum_ErrorSeverity.Medium,
                            controlName: nameof(AdvancedInventory_Import_Button_OpenExcel));
                        return;
                    }
                }

                Process.Start(new ProcessStartInfo(excelPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["ExcelPath"] = await GetUserExcelFilePathAsync() },
                    controlName: nameof(AdvancedInventory_Import_Button_OpenExcel));
            }
        }

        // Fix for CS8600 in AdvancedInventory_Import_Button_ImportExcel_Click and other usages
        private async void AdvancedInventory_Import_Button_ImportExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                string excelPath = await GetUserExcelFilePathAsync();
                if (!File.Exists(excelPath))
                {
                    Service_ErrorHandler.HandleValidationError(@"Excel file not found. Please create or open the Excel file first.", nameof(AdvancedInventory_Import_Button_ImportExcel));
                    return;
                }

                DataTable dt = new();
                using (XLWorkbook workbook = new(excelPath))
                {
                    IXLWorksheet? worksheet = workbook.Worksheet("Tab 1");
                    if (worksheet == null)
                    {
                        Service_ErrorHandler.HandleValidationError(@"Worksheet 'Tab 1' not found in the Excel file.", nameof(AdvancedInventory_Import_Button_ImportExcel));
                        return;
                    }

                    IXLRange? usedRange = worksheet.RangeUsed();
                    if (usedRange == null)
                    {
                        Service_ErrorHandler.HandleValidationError(@"No data found in 'Tab 1'.", nameof(AdvancedInventory_Import_Button_ImportExcel));
                        return;
                    }

                    int colCount = usedRange.ColumnCount();
                    int rowCount = usedRange.RowCount();

                    IXLRangeRow? headerRow = usedRange.Row(1);
                    for (int col = 1; col <= colCount; col++)
                    {
                        string? colName = headerRow.Cell(col).GetValue<string>();
                        if (string.IsNullOrWhiteSpace(colName))
                        {
                            colName = $"Column{col}";
                        }

                        dt.Columns.Add(colName);
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dataRow = dt.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            dataRow[col - 1] = usedRange.Row(row).Cell(col).GetValue<string>();
                        }

                        dt.Rows.Add(dataRow);
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No data found in the Excel file to import.",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                AdvancedInventory_Import_DataGridView.DataSource = dt;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["ExcelPath"] = await GetUserExcelFilePathAsync() },
                    controlName: nameof(AdvancedInventory_Import_Button_ImportExcel));
            }
        }

        // Fix for CS8600 in AdvancedInventory_Import_Button_Save_Click and RefreshImportDataGridView
        private async void AdvancedInventory_Import_Button_Save_Click(object? sender, EventArgs e)
        {
            if (AdvancedInventory_Import_DataGridView.DataSource == null)
            {
                return;
            }

            DataGridView? dgv = AdvancedInventory_Import_DataGridView;
            List<DataGridViewRow> rowsToRemove = new();
            bool anyError = false;

            // Get validation data from cached sources (used by SuggestionTextBox controls)
            var partNumbers = await Helper_SuggestionTextBox.GetCachedPartNumbersAsync();
            var operations = await Helper_SuggestionTextBox.GetCachedOperationsAsync();
            var locations = await Helper_SuggestionTextBox.GetCachedLocationsAsync();

            HashSet<string> validParts = partNumbers.ToHashSet(StringComparer.OrdinalIgnoreCase);
            HashSet<string> validOps = operations.ToHashSet(StringComparer.OrdinalIgnoreCase);
            HashSet<string> validLocs = locations.ToHashSet(StringComparer.OrdinalIgnoreCase);

            string excelPath = await GetUserExcelFilePathAsync();
            XLWorkbook? workbook = null;
            IXLWorksheet? worksheet = null;
            if (File.Exists(excelPath))
            {
                workbook = new XLWorkbook(excelPath);
                worksheet = workbook.Worksheet("Tab 1");
            }

            List<int> excelRowsToDelete = new();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                // Always pass null/empty for batchNumber to ensure each transaction gets a unique batch number
                bool rowValid = true;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black;
                }

                string part = row.Cells["Part"].Value?.ToString() ?? "";
                string op = row.Cells["Operation"].Value?.ToString() ?? "";
                string loc = row.Cells["Location"].Value?.ToString() ?? "";
                string qtyText = row.Cells["Quantity"].Value?.ToString() ?? "";
                string notesOriginal = row.Cells["Notes"].Value?.ToString() ?? "";
                string notes = "Excel Import: " + notesOriginal;

                if (!validParts.Contains(part))
                {
                    row.Cells["Part"].Style.ForeColor =
                        Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    rowValid = false;
                }

                if (!validOps.Contains(op))
                {
                    row.Cells["Operation"].Style.ForeColor =
                        Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    rowValid = false;
                }

                if (!validLocs.Contains(loc))
                {
                    row.Cells["Location"].Style.ForeColor =
                        Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    rowValid = false;
                }

                if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                {
                    row.Cells["Quantity"].Style.ForeColor =
                        Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    rowValid = false;
                }

                if (rowValid)
                {
                    try
                    {
                        // Pass null/empty for batchNumber for unique batch per transaction
                        await Dao_Inventory.AddInventoryItemAsync(
                            part, loc, op, qty, "", Model_Application_Variables.User ?? Environment.UserName, null, notes, null, null, true);
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.LogApplicationError(ex);
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            cell.Style.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                        }

                        rowValid = false;
                        anyError = true;
                    }
                }
                else
                {
                    anyError = true;
                }
            }

            if (worksheet != null && excelRowsToDelete.Count > 0)
            {
                excelRowsToDelete.Sort((a, b) => b.CompareTo(a));
                foreach (int rowNum in excelRowsToDelete)
                {
                    worksheet.Row(rowNum).Delete();
                }

                IXLRange? usedRange = worksheet.RangeUsed();
                if (usedRange != null)
                {
                    int headerRow = usedRange.FirstRow().RowNumber();
                    int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? headerRow;
                }

                workbook?.Save();
            }

            foreach (DataGridViewRow row in rowsToRemove)
            {
                if (!row.IsNewRow)
                {
                    dgv.Rows.Remove(row);
                }
            }

            RefreshImportDataGridView();

            if (!anyError)
            {
                Service_ErrorHandler.ShowInformation(@"All transactions saved successfully.", "Import Success");
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                        $"Last Import: {DateTime.Now:hh:mm tt} ({dgv.Rows.Count} rows imported)";
                }
            }
            else
            {
                Service_ErrorHandler.HandleValidationError(@"Some rows could not be saved. Please correct highlighted errors.", nameof(AdvancedInventory_Import_DataGridView));
            }
        }

        private async void RefreshImportDataGridView()
        {
            string excelPath = await GetUserExcelFilePathAsync();
            if (!File.Exists(excelPath))
            {
                return;
            }

            Dictionary<int, HashSet<string>> highlightMap = new();
            if (AdvancedInventory_Import_DataGridView.DataSource is DataTable)
            {
                foreach (DataGridViewRow row in AdvancedInventory_Import_DataGridView.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    HashSet<string> cols = new();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Style.ForeColor ==
                            (Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red))
                        {
                            if (cell.OwningColumn != null)
                            {
                                cols.Add(cell.OwningColumn.Name);
                            }
                        }
                    }

                    if (cols.Count > 0)
                    {
                        highlightMap[row.Index] = cols;
                    }
                }
            }

            DataTable dt = new();
            using (XLWorkbook workbook = new(excelPath))
            {
                IXLWorksheet? worksheet = workbook.Worksheet("Tab 1");
                if (worksheet == null)
                {
                    return;
                }

                IXLRange? usedRange = worksheet.RangeUsed();
                if (usedRange == null)
                {
                    return;
                }

                int colCount = usedRange.ColumnCount();
                int rowCount = usedRange.RowCount();

                IXLRangeRow? headerRow = usedRange.Row(1);
                for (int col = 1; col <= colCount; col++)
                {
                    string? colName = headerRow.Cell(col).GetValue<string>();
                    if (string.IsNullOrWhiteSpace(colName))
                    {
                        colName = $"Column{col}";
                    }

                    dt.Columns.Add(colName);
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        dataRow[col - 1] = usedRange.Row(row).Cell(col).GetValue<string>();
                    }

                    dt.Rows.Add(dataRow);
                }
            }

            AdvancedInventory_Import_DataGridView.DataSource = dt;

            foreach (DataGridViewRow row in AdvancedInventory_Import_DataGridView.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                if (highlightMap.TryGetValue(row.Index, out HashSet<string>? cols))
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.OwningColumn != null && cols.Contains(cell.OwningColumn.Name))
                        {
                            cell.Style.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                        }
                    }
                }

                if (row.DataGridView != null && row.DataGridView.Columns.Contains("Quantity"))
                {
                    DataGridViewCell? qtyCell = row.Cells["Quantity"];
                    string qtyText = qtyCell.Value?.ToString() ?? "";
                    if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                    {
                        qtyCell.Style.ForeColor = Model_Application_Variables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    }
                }
            }
        }

        #endregion

        #region Helpers

        private static void HandleDaoFailure<T>(Model_Dao_Result<T> result, string fallbackMessage,
            Dictionary<string, object> contextData, string callerName)
        {
            string message = string.IsNullOrWhiteSpace(result.ErrorMessage)
                ? fallbackMessage
                : result.ErrorMessage;

            Service_ErrorHandler.HandleException(
                new InvalidOperationException(message),
                Enum_ErrorSeverity.Medium,
                contextData: contextData,
                callerName: callerName,
                controlName: nameof(Control_AdvancedInventory));
        }

        private static Dictionary<string, object> CreateInventoryContext(string partId, string operation,
            string location, int quantity, string notes)
        {
            return new Dictionary<string, object>
            {
                ["PartID"] = partId,
                ["Operation"] = operation,
                ["Location"] = location,
                ["Quantity"] = quantity,
                ["Notes"] = notes
            };
        }

        #endregion
    }
}