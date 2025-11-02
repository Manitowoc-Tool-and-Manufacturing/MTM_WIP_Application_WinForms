using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.MainForm.Classes;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    #region InventoryTab

    public partial class Control_InventoryTab : UserControl
    {
        #region Fields

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }

        private Helper_StoredProcedureProgress? _progressHelper;

        #endregion

        #region Progress Control Methods

        /// <summary>
        /// Sets progress controls for visual feedback during long-running database operations.
        /// </summary>
        /// <param name="progressBar">The progress bar control to display operation progress (0-100%)</param>
        /// <param name="statusLabel">The status label control to display operation status messages</param>
        /// <exception cref="InvalidOperationException">Thrown when control is not added to a form</exception>
        /// <remarks>
        /// Must be called during initialization before any async operations that require progress feedback.
        /// Progress helper is used by LoadDataComboBoxesAsync, Save, and Reset operations.
        /// </remarks>
        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel, 
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Constructors

        #region Initialization

        public Control_InventoryTab()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_InventoryTab),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab));

            Service_DebugTracer.TraceUIAction("INVENTORY_TAB_INITIALIZATION", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();

            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });
            // Apply comprehensive DPI scaling and runtime layout adjustments
            // THEME POLICY: Only update theme on startup, in settings menu, or on DPI change.
            // Do NOT call theme update methods from arbitrary event handlers or business logic.
            Core_Themes.ApplyDpiScaling(this); // Allowed: UserControl initialization
            Core_Themes.ApplyRuntimeLayoutAdjustments(this); // Allowed: UserControl initialization

            Service_DebugTracer.TraceUIAction("TOOLTIPS_SETUP", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["TooltipCount"] = 4,
                    ["ButtonsConfigured"] = new[] { "Save", "AdvancedEntry", "Reset", "ToggleRightPanel" }
                });
            Control_InventoryTab_Tooltip.SetToolTip(Control_InventoryTab_Button_Save,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Inventory_Save)}");
            Control_InventoryTab_Tooltip.SetToolTip(Control_InventoryTab_Button_AdvancedEntry,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Inventory_Advanced)}");
            Control_InventoryTab_Tooltip.SetToolTip(Control_InventoryTab_Button_Reset,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Inventory_Reset)}");
            Control_InventoryTab_Tooltip.SetToolTip(Control_InventoryTab_Button_Toggle_RightPanel,
                $"Shortcut: {Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Inventory_ToggleRightPanel_Left)}/{Helper_UI_Shortcuts.ToShortcutString(Core_WipAppVariables.Shortcut_Inventory_ToggleRightPanel_Right)}");

            Service_DebugTracer.TraceUIAction("VERSION_TIMER_SETUP", nameof(Control_InventoryTab),
                new Dictionary<string, object> { ["TimerInstance"] = "Service_Timer_VersionChecker" });
            Service_Timer_VersionChecker.ControlInventoryInstance = this;

            Service_DebugTracer.TraceUIAction("DATA_LOADING_START", nameof(Control_InventoryTab),
                new Dictionary<string, object> { ["DataType"] = "ComboBoxes" });
            _ = Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync();

            Service_DebugTracer.TraceUIAction("EVENTS_WIREUP", nameof(Control_InventoryTab));
            Control_InventoryTab_OnStartup_WireUpEvents();

            Service_DebugTracer.TraceUIAction("VERSION_LABEL_SET", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["UserVersion"] = Model_AppVariables.UserVersion,
                    ["DatabaseVersion"] = Service_Timer_VersionChecker.LastCheckedDatabaseVersion ?? "unknown"
                });
            SetVersionLabel(Model_AppVariables.UserVersion,
                Service_Timer_VersionChecker.LastCheckedDatabaseVersion ?? "unknown");

            Service_DebugTracer.TraceUIAction("UI_STYLING_APPLIED", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["FocusHighlighting"] = true,
                    ["ComboBoxColors"] = "Applied",
                    ["InitialFocus"] = "Control_InventoryTab_ComboBox_Part",
                    ["QuantityTextBoxState"] = "Placeholder"
                });
            Core_Themes.ApplyFocusHighlighting(this);
            Control_InventoryTab_ComboBox_Part.ForeColor = Control_InventoryTab_ComboBox_Operation.ForeColor =
                Control_InventoryTab_ComboBox_Location.ForeColor =
                    Model_AppVariables.UserUiColors.ComboBoxForeColor ?? Color.Red;
            Control_InventoryTab_ComboBox_Part.Focus();
            Control_InventoryTab_TextBox_Quantity.Text = "[ Enter Valid Quantity ]";
            Control_InventoryTab_TextBox_Quantity.ForeColor =
                Model_AppVariables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;

            Service_DebugTracer.TraceUIAction("PRIVILEGES_APPLIED", nameof(Control_InventoryTab));
            ApplyPrivileges();

            Service_DebugTracer.TraceUIAction("INVENTORY_TAB_INITIALIZATION", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["Phase"] = "COMPLETE",
                    ["Success"] = true
                });

            Service_DebugTracer.TraceMethodExit(null, nameof(Control_InventoryTab), nameof(Control_InventoryTab));
        }

        #endregion

        #region Privlages

        private void ApplyPrivileges()
        {
            bool isDeveloper = Model_AppVariables.UserTypeDeveloper;
            bool isAdmin = Model_AppVariables.UserTypeAdmin;
            bool isNormal = Model_AppVariables.UserTypeNormal;
            bool isReadOnly = Model_AppVariables.UserTypeReadOnly;

            // Developers have all Admin privileges
            bool hasAdminAccess = isDeveloper || isAdmin;

            // Admin/Developer and Normal: all controls visible/enabled
            // Read-Only: only specific controls visible/enabled
            Control_InventoryTab_GroupBox_Main.Visible = hasAdminAccess || isNormal || isReadOnly;
            Control_InventoryTab_Button_Reset.Visible = hasAdminAccess || isNormal;
            Control_InventoryTab_Button_Save.Visible = hasAdminAccess || isNormal;
            Control_InventoryTab_Label_Version.Visible = true;
            Control_InventoryTab_Button_AdvancedEntry.Visible = hasAdminAccess || isNormal;
            Control_InventoryTab_Label_Part.Visible = true;
            Control_InventoryTab_Label_Op.Visible = true;
            Control_InventoryTab_Label_Loc.Visible = true;
            Control_InventoryTab_Label_Qty.Visible = true;
            Control_InventoryTab_TextBox_Quantity.Visible = hasAdminAccess || isNormal || isReadOnly;
            Control_InventoryTab_RichTextBox_Notes.Visible = hasAdminAccess || isNormal || isReadOnly;
            Control_InventoryTab_TableLayout_Main.Visible = true;
            Control_InventoryTab_TableLayout_TopGroup.Visible = true;
            Control_InventoryTab_Button_Toggle_RightPanel.Visible = isAdmin || isNormal;
            // ToolTip is always available
            // All other input controls (if any):
            // If you add more, follow the same pattern

            // For Read-Only, set controls to ReadOnly/Disabled if applicable
            if (isReadOnly)
            {
                Control_InventoryTab_ComboBox_Part.Enabled = false;
                Control_InventoryTab_ComboBox_Operation.Enabled = false;
                Control_InventoryTab_ComboBox_Location.Enabled = false;
                Control_InventoryTab_TextBox_Quantity.Enabled = false;
                Control_InventoryTab_RichTextBox_Notes.ReadOnly = true;
                Control_InventoryTab_Button_Save.Visible = false;
                Control_InventoryTab_Button_Reset.Visible = false;
                Control_InventoryTab_Button_AdvancedEntry.Visible = false;
                Control_InventoryTab_Button_Toggle_RightPanel.Visible = false;
            }
            else
            {
                Control_InventoryTab_ComboBox_Part.Enabled = true;
                Control_InventoryTab_ComboBox_Operation.Enabled = true;
                Control_InventoryTab_ComboBox_Location.Enabled = true;
                Control_InventoryTab_TextBox_Quantity.Enabled = true;
                Control_InventoryTab_RichTextBox_Notes.ReadOnly = false;
                Control_InventoryTab_Button_Save.Visible = true;
                Control_InventoryTab_Button_Reset.Visible = true;
                Control_InventoryTab_Button_AdvancedEntry.Visible = true;
                Control_InventoryTab_Button_Toggle_RightPanel.Visible = true;
            }
            // TODO: If there are TreeView branches, set their .Visible property here as well.
        }

        #endregion

        #region Startup / ComboBox Loading

        /// <summary>
        /// Asynchronously loads all combo box data (Parts, Operations, Locations) during tab initialization.
        /// </summary>
        /// <returns>A task that completes when all combo boxes are populated</returns>
        /// <remarks>
        /// This method is called automatically during control construction and should not be called directly.
        /// Uses Helper_UI_ComboBoxes to populate combo boxes from master data tables.
        /// Displays progress updates during loading and handles errors with Service_ErrorHandler.
        /// </remarks>
        public async Task Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["MethodName"] = nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync),
                ["ControlType"] = nameof(Control_InventoryTab)
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync));

            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Loading part data...");
                
                await Helper_UI_ComboBoxes.FillPartComboBoxesAsync(Control_InventoryTab_ComboBox_Part);
                
                _progressHelper?.UpdateProgress(40, "Loading operation data...");
                await Helper_UI_ComboBoxes.FillOperationComboBoxesAsync(Control_InventoryTab_ComboBox_Operation);
                
                _progressHelper?.UpdateProgress(70, "Loading location data...");
                await Helper_UI_ComboBoxes.FillLocationComboBoxesAsync(Control_InventoryTab_ComboBox_Location);

                _progressHelper?.UpdateProgress(100, "Combo boxes loaded");
                await Task.Delay(100);
                
                LoggingUtility.Log("Inventory tab ComboBoxes loaded.");
                Service_DebugTracer.TraceMethodExit("Success", nameof(Control_InventoryTab), nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync));
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.High,
                    retryAction: () => { _ = Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync(); return true; },
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync),
                        ["ControlType"] = nameof(Control_InventoryTab)
                    },
                    controlName: nameof(Control_InventoryTab));
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        #endregion

        #region Key Processing

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["KeyData"] = keyData.ToString(),
                ["Visible"] = Visible
            }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));

            try
            {
                if (Visible)
                {
                    if (keyData == Core_WipAppVariables.Shortcut_Inventory_Save)
                    {
                        if (Control_InventoryTab_Button_Save.Visible && Control_InventoryTab_Button_Save.Enabled)
                        {
                            Control_InventoryTab_Button_Save.PerformClick();
                            Service_DebugTracer.TraceMethodExit(new { KeyHandled = true, Action = "Save" }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_Inventory_Advanced)
                    {
                        if (Control_InventoryTab_Button_AdvancedEntry.Visible &&
                            Control_InventoryTab_Button_AdvancedEntry.Enabled)
                        {
                            Control_InventoryTab_Button_AdvancedEntry.PerformClick();
                            Service_DebugTracer.TraceMethodExit(new { KeyHandled = true, Action = "Advanced" }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                            return true;
                        }
                    }

                    if (keyData == Core_WipAppVariables.Shortcut_Inventory_Reset)
                    {
                        if (Control_InventoryTab_Button_Reset.Visible && Control_InventoryTab_Button_Reset.Enabled)
                        {
                            Control_InventoryTab_Button_Reset.PerformClick();
                            Service_DebugTracer.TraceMethodExit(new { KeyHandled = true, Action = "Reset" }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                            return true;
                        }
                    }
                }

                if (keyData == Keys.Enter)
                {
                    SelectNextControl(
                        ActiveControl,
                        true,
                        true,
                        true,
                        true
                    );
                    Service_DebugTracer.TraceMethodExit(new { KeyHandled = true, Action = "NextControl" }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                    return true;
                }

                if (MainFormInstance != null && !MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed &&
                    keyData == Core_WipAppVariables.Shortcut_Inventory_ToggleRightPanel_Right)
                {
                    Control_InventoryTab_Button_Toggle_RightPanel.PerformClick();
                    Service_DebugTracer.TraceMethodExit(new { KeyHandled = true, Action = "TogglePanelRight" }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                    return true;
                }

                if (MainFormInstance != null && MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed &&
                    keyData == Core_WipAppVariables.Shortcut_Inventory_ToggleRightPanel_Left)
                {
                    Control_InventoryTab_Button_Toggle_RightPanel.PerformClick();
                    Service_DebugTracer.TraceMethodExit(new { KeyHandled = true, Action = "TogglePanelLeft" }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                    return true;
                }

                Service_DebugTracer.TraceMethodExit(new { KeyHandled = false, PassedToBase = true }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(ProcessCmdKey));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(ProcessCmdKey),
                        ["KeyData"] = keyData.ToString()
                    },
                    controlName: nameof(Control_InventoryTab));
                
                return false;
            }
        }

        #endregion

        #region Button Clicks

        private static void Control_InventoryTab_Button_AdvancedEntry_Click()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["MainFormInstance"] = Service_Timer_VersionChecker.MainFormInstance != null
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_AdvancedEntry_Click));

            try
            {
                if (Service_Timer_VersionChecker.MainFormInstance is null)
                {
                    LoggingUtility.Log("MainForm instance is null, cannot open Advanced Inventory Removal.");
                    Service_DebugTracer.TraceMethodExit("MainForm instance null", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_AdvancedEntry_Click));
                    return;
                }

                if (MainFormInstance is not null)
                {
                    MainFormInstance.MainForm_UserControl_InventoryTab.Visible = false;
                }

                if (MainFormInstance is not null)
                {
                    MainFormInstance.MainForm_UserControl_AdvancedInventory.Visible = true;
                }

                if (MainFormInstance?.MainForm_UserControl_AdvancedInventory is null)
                {
                    Service_DebugTracer.TraceMethodExit("AdvancedInventory control null", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_AdvancedEntry_Click));
                    return;
                }

                Control_AdvancedInventory? adv = MainFormInstance.MainForm_UserControl_AdvancedInventory;

                if (adv.GetType().GetField("AdvancedInventory_Single_ComboBox_Part",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is ComboBox combo && combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;
                    combo.ForeColor = Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
                    combo.Focus();
                    combo.SelectAll();
                }

                if (adv.GetType().GetField("AdvancedInventory_Single_ComboBox_Op",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is ComboBox op && op.Items.Count > 0)
                {
                    op.SelectedIndex = 0;
                    op.ForeColor = Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
                }

                if (adv.GetType().GetField("AdvancedInventory_Single_ComboBox_Loc",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is ComboBox loc && loc.Items.Count > 0)
                {
                    loc.SelectedIndex = 0;
                    loc.ForeColor = Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
                }

                if (adv.GetType().GetField("AdvancedInventory_MultiLoc_ComboBox_Part",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is ComboBox multiPart && multiPart.Items.Count > 0)
                {
                    multiPart.SelectedIndex = 0;
                    multiPart.ForeColor = Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
                }

                if (adv.GetType().GetField("AdvancedInventory_MultiLoc_ComboBox_Op",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is ComboBox multiOp && multiOp.Items.Count > 0)
                {
                    multiOp.SelectedIndex = 0;
                    multiOp.ForeColor = Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
                }

                if (adv.GetType().GetField("AdvancedInventory_MultiLoc_ComboBox_Loc",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is ComboBox multiLoc && multiLoc.Items.Count > 0)
                {
                    multiLoc.SelectedIndex = 0;
                    multiLoc.ForeColor = Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
                }

                if (adv.GetType().GetField("AdvancedInventory_TabControl",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is TabControl tab)
                {
                    tab.SelectedIndex = 0;
                }

                Service_DebugTracer.TraceMethodExit("Success", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_AdvancedEntry_Click));
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_AdvancedEntry_Click));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_Button_AdvancedEntry_Click)
                    },
                    controlName: nameof(Control_InventoryTab_Button_AdvancedEntry));
            }
        }

        private void Control_InventoryTab_Button_Reset_Click()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["MethodName"] = nameof(Control_InventoryTab_Button_Reset_Click),
                ["ShiftKeyPressed"] = (ModifierKeys & Keys.Shift) == Keys.Shift
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Reset_Click));

            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Inventory tab...");
                if ((ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    Control_InventoryTab_HardReset();
                    Service_DebugTracer.TraceMethodExit(new { ResetType = "Hard" }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Reset_Click));
                }
                else
                {
                    Control_InventoryTab_SoftReset();
                    Service_DebugTracer.TraceMethodExit(new { ResetType = "Soft" }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Reset_Click));
                }

                _progressHelper?.UpdateProgress(100, "Reset complete");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Reset_Click));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Medium,
                    retryAction: () => { Control_InventoryTab_Button_Reset_Click(); return true; },
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_Button_Reset_Click),
                        ["ShiftKeyPressed"] = (ModifierKeys & Keys.Shift) == Keys.Shift
                    },
                    controlName: nameof(Control_InventoryTab_Button_Reset));
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        /// <summary>
        /// Performs a hard reset of the Inventory tab by refreshing all master data and resetting UI fields.
        /// </summary>
        /// <remarks>
        /// Hard reset (Shift + Reset button):
        /// - Refreshes all DataTables from the database (Parts, Operations, Locations)
        /// - Refills all combo boxes with fresh data
        /// - Resets all input fields to default placeholder values
        /// - Resets focus to Part combo box
        /// 
        /// Use when master data may have changed or combo boxes are out of sync.
        /// Triggered by: Shift + Click on Reset button or Shift + Keyboard shortcut.
        /// </remarks>
        public async void Control_InventoryTab_HardReset()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["MethodName"] = nameof(Control_InventoryTab_HardReset),
                ["ControlType"] = nameof(Control_InventoryTab)
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_HardReset));

            Control_InventoryTab_Button_Reset.Enabled = false;
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Inventory tab...");
                Debug.WriteLine("[DEBUG] InventoryTab Reset button clicked - start");
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                _progressHelper?.UpdateProgress(30, "Resetting data tables...");
                Debug.WriteLine("[DEBUG] Hiding ComboBoxes");

                Debug.WriteLine("[DEBUG] Resetting and refreshing all ComboBox DataTables");
                await Helper_UI_ComboBoxes.ResetAndRefreshAllDataTablesAsync();
                Debug.WriteLine("[DEBUG] DataTables reset complete");

                _progressHelper?.UpdateProgress(60, "Refilling combo boxes...");
                Debug.WriteLine("[DEBUG] Refilling Part ComboBox");
                await Helper_UI_ComboBoxes.FillPartComboBoxesAsync(Control_InventoryTab_ComboBox_Part);
                Debug.WriteLine("[DEBUG] Refilling Operation ComboBox");
                await Helper_UI_ComboBoxes.FillOperationComboBoxesAsync(Control_InventoryTab_ComboBox_Operation);
                Debug.WriteLine("[DEBUG] Refilling Location ComboBox");
                await Helper_UI_ComboBoxes.FillLocationComboBoxesAsync(Control_InventoryTab_ComboBox_Location);

                Debug.WriteLine("[DEBUG] Resetting UI fields");
                MainFormControlHelper.ResetComboBox(Control_InventoryTab_ComboBox_Part,
                    Model_AppVariables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red, 0);
                MainFormControlHelper.ResetComboBox(Control_InventoryTab_ComboBox_Operation,
                    Model_AppVariables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red, 0);
                MainFormControlHelper.ResetComboBox(Control_InventoryTab_ComboBox_Location,
                    Model_AppVariables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red, 0);
                MainFormControlHelper.ResetTextBox(Control_InventoryTab_TextBox_Quantity,
                    Model_AppVariables.UserUiColors.TextBoxErrorForeColor ?? Color.Red, "[ Enter Valid Quantity ]");
                MainFormControlHelper.ResetRichTextBox(Control_InventoryTab_RichTextBox_Notes,
                    Model_AppVariables.UserUiColors.RichTextBoxErrorForeColor ?? Color.Red, "");

                Control_InventoryTab_ComboBox_Part.Focus();

                Control_InventoryTab_Update_SaveButtonState();

                Debug.WriteLine("[DEBUG] InventoryTab Reset button clicked - end");
                _progressHelper?.UpdateProgress(100, "Reset complete");
                Service_DebugTracer.TraceMethodExit("Success", nameof(Control_InventoryTab), nameof(Control_InventoryTab_HardReset));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in InventoryTab Reset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_HardReset));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.High,
                    retryAction: () => { Control_InventoryTab_HardReset(); return true; },
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_HardReset),
                        ["ControlType"] = nameof(Control_InventoryTab)
                    },
                    controlName: nameof(Control_InventoryTab_Button_Reset));
            }
            finally
            {
                Debug.WriteLine("[DEBUG] InventoryTab Reset button re-enabled");
                Control_InventoryTab_Button_Reset.Enabled = true;
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

        private void Control_InventoryTab_SoftReset()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["MethodName"] = nameof(Control_InventoryTab_SoftReset),
                ["ControlType"] = nameof(Control_InventoryTab)
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_SoftReset));

            Control_InventoryTab_Button_Reset.Enabled = false;
            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Resetting Inventory tab...");
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Updating status strip for Soft Reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text = @"Please wait while resetting...";
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = false;
                }

                Debug.WriteLine("[DEBUG] Resetting UI fields");
                MainFormControlHelper.ResetComboBox(Control_InventoryTab_ComboBox_Part,
                    Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red, 0);
                MainFormControlHelper.ResetComboBox(Control_InventoryTab_ComboBox_Operation,
                    Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red, 0);
                MainFormControlHelper.ResetComboBox(Control_InventoryTab_ComboBox_Location,
                    Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red, 0);
                MainFormControlHelper.ResetTextBox(Control_InventoryTab_TextBox_Quantity,
                    Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red, "[ Enter Valid Quantity ]");
                MainFormControlHelper.ResetRichTextBox(Control_InventoryTab_RichTextBox_Notes,
                    Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red, "");
                Control_InventoryTab_Button_Save.Enabled = false;
                _progressHelper?.UpdateProgress(100, "Reset complete");
                Service_DebugTracer.TraceMethodExit("Success", nameof(Control_InventoryTab), nameof(Control_InventoryTab_SoftReset));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] Exception in InventoryTab SoftReset: {ex}");
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_SoftReset));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Medium,
                    retryAction: () => { Control_InventoryTab_SoftReset(); return true; },
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_SoftReset),
                        ["ControlType"] = nameof(Control_InventoryTab)
                    },
                    controlName: nameof(Control_InventoryTab_Button_Reset));
            }
            finally
            {
                Debug.WriteLine("[DEBUG] InventoryTab SoftReset button re-enabled");
                Control_InventoryTab_Button_Reset.Enabled = true;
                Control_InventoryTab_ComboBox_Part.Focus();
                if (MainFormInstance != null)
                {
                    Debug.WriteLine("[DEBUG] Restoring status strip after reset");
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Visible = false;
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Visible = true;
                    MainFormInstance.MainForm_StatusStrip_Disconnected.Text =
                        @"Disconnected from Server, please standby...";
                }

                _progressHelper?.HideProgress();
            }
        }

        private async Task Control_InventoryTab_Button_Save_Click_Async()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["MethodName"] = nameof(Control_InventoryTab_Button_Save_Click_Async),
                ["ControlType"] = nameof(Control_InventoryTab)
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));

            try
            {
                _progressHelper?.ShowProgress();
                _progressHelper?.UpdateProgress(10, "Saving inventory transaction...");
                LoggingUtility.Log("Inventory Save button clicked.");

                string partId = Control_InventoryTab_ComboBox_Part.Text;
                string op = Control_InventoryTab_ComboBox_Operation.Text;
                string loc = Control_InventoryTab_ComboBox_Location.Text;
                string qtyText = Control_InventoryTab_TextBox_Quantity.Text.Trim();
                string notes = Control_InventoryTab_RichTextBox_Notes.Text.Trim();

                if (string.IsNullOrWhiteSpace(partId) || Control_InventoryTab_ComboBox_Part.SelectedIndex <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please select a valid Part.",
                        nameof(Control_InventoryTab_ComboBox_Part));
                    Control_InventoryTab_ComboBox_Part.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Part invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                if (string.IsNullOrWhiteSpace(op) || Control_InventoryTab_ComboBox_Operation.SelectedIndex <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please select a valid Operation.",
                        nameof(Control_InventoryTab_ComboBox_Operation));
                    Control_InventoryTab_ComboBox_Operation.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Operation invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                if (string.IsNullOrWhiteSpace(loc) || Control_InventoryTab_ComboBox_Location.SelectedIndex <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please select a valid Location.",
                        nameof(Control_InventoryTab_ComboBox_Location));
                    Control_InventoryTab_ComboBox_Location.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Location invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please enter a valid quantity.",
                        nameof(Control_InventoryTab_TextBox_Quantity));
                    Control_InventoryTab_TextBox_Quantity.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Quantity invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                Model_AppVariables.PartId = partId;
                Model_AppVariables.Operation = op;
                Model_AppVariables.Location = loc;
                Model_AppVariables.Notes = notes;
                Model_AppVariables.InventoryQuantity = qty;
                Model_AppVariables.User ??= Environment.UserName;

                _progressHelper?.UpdateProgress(40, "Adding inventory item...");
                
                Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                {
                    ["PartId"] = partId,
                    ["Location"] = loc,
                    ["Operation"] = op,
                    ["Quantity"] = qty,
                    ["User"] = Model_AppVariables.User
                }, nameof(Control_InventoryTab), "AddInventoryItemAsync");
                
                // Verify the transaction succeeded before proceeding
                var inventoryResult = await Dao_Inventory.AddInventoryItemAsync(
                    partId,
                    loc,
                    op,
                    qty,
                    "",
                    Model_AppVariables.User,
                    "",
                    notes,
                    true);

                Service_DebugTracer.TraceMethodExit(new { IsSuccess = inventoryResult.IsSuccess, Message = inventoryResult.StatusMessage }, nameof(Control_InventoryTab), "AddInventoryItemAsync");

                // Check if the inventory transaction was successful
                if (!inventoryResult.IsSuccess)
                {
                    LoggingUtility.LogApplicationError(new Exception($"Inventory transaction failed: {inventoryResult.ErrorMessage}"));
                    
                    Service_ErrorHandler.HandleException(
                        inventoryResult.Exception ?? new Exception(inventoryResult.ErrorMessage),
                        ErrorSeverity.Medium,
                        retryAction: () => { Control_InventoryTab_Button_Save.PerformClick(); return true; },
                        contextData: new Dictionary<string, object>
                        {
                            ["PartId"] = partId,
                            ["Operation"] = op,
                            ["Location"] = loc,
                            ["Quantity"] = qty,
                            ["User"] = Model_AppVariables.User
                        },
                        controlName: nameof(Control_InventoryTab_Button_Save));
                    
                    // Update status to show failure
                    if (MainFormInstance != null)
                    {
                        MainFormInstance.MainForm_StatusStrip_SavedStatus.Text = 
                            $@"Failed to save inventory transaction @ {DateTime.Now:hh:mm tt}";
                    }
                    
                    Service_DebugTracer.TraceMethodExit("Save failed: DAO returned error", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                LoggingUtility.Log($"Inventory transaction verified successful: {inventoryResult.StatusMessage}");
                
                _progressHelper?.UpdateProgress(70, "Updating recent transactions...");
                await AddToLast10TransactionsIfUniqueAsync(Model_AppVariables.User, partId, op, qty);

                // Only update status after verifying transaction success
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text =
                        $@"Last Inventoried Part: {partId} (Op: {op}), Location: {(string.IsNullOrWhiteSpace(loc) ? "" : loc)}, Quantity: {qty} @ {DateTime.Now:hh:mm tt}";
                }

                _progressHelper?.UpdateProgress(90, "Resetting form...");
                Control_InventoryTab_Button_Reset_Click();
                if (MainFormInstance != null && MainFormInstance.MainForm_UserControl_QuickButtons != null)
                {
                    await MainFormInstance.MainForm_UserControl_QuickButtons.LoadLast10Transactions(Model_AppVariables.User);
                }

                _progressHelper?.UpdateProgress(100, "Save complete");
                LoggingUtility.Log("Inventory Save operation completed successfully.");
                Service_DebugTracer.TraceMethodExit("Success", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                
                // Update status to show error occurred
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_StatusStrip_SavedStatus.Text = 
                        $@"Error occurred during save operation @ {DateTime.Now:hh:mm tt}";
                }
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.High,
                    retryAction: () => { Control_InventoryTab_Button_Save.PerformClick(); return true; },
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_Button_Save_Click_Async),
                        ["ControlType"] = nameof(Control_InventoryTab)
                    },
                    controlName: nameof(Control_InventoryTab));
                
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
            }
            finally
            {
                _progressHelper?.HideProgress();
            }
        }

        private static async Task AddToLast10TransactionsIfUniqueAsync(string user, string partId, string operation,
            int quantity)
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["User"] = user,
                ["PartId"] = partId,
                ["Operation"] = operation,
                ["Quantity"] = quantity
            }, nameof(Control_InventoryTab), nameof(AddToLast10TransactionsIfUniqueAsync));

            try
            {
                // Use the proper Dao_QuickButtons method that handles positions correctly
                var result = await Dao_QuickButtons.AddOrShiftQuickButtonAsync(user, partId, operation, quantity);
                if (!result.IsSuccess)
                {
                    LoggingUtility.LogDatabaseError(
                        result.Exception ?? new Exception(result.ErrorMessage),
                        DatabaseErrorSeverity.Warning); // Non-critical operation, log as warning
                    Service_DebugTracer.TraceMethodExit(new { Success = false, Error = result.ErrorMessage }, nameof(Control_InventoryTab), nameof(AddToLast10TransactionsIfUniqueAsync));
                }
                else
                {
                    Service_DebugTracer.TraceMethodExit(new { Success = true }, nameof(Control_InventoryTab), nameof(AddToLast10TransactionsIfUniqueAsync));
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(AddToLast10TransactionsIfUniqueAsync));
            }
        }

        private void Control_InventoryTab_Button_Toggle_RightPanel_Click(object sender, EventArgs e)
        {
            if (MainFormInstance != null && !MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed)
            {
                MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed = true;

                Control_InventoryTab_Button_Toggle_RightPanel.Text = "⬅️";
                Control_InventoryTab_Button_Toggle_RightPanel.ForeColor =
                    Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red;
            }
            else
            {
                if (MainFormInstance != null)
                {
                    MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed = false;
                    Control_InventoryTab_Button_Toggle_RightPanel.Text = "➡️";
                    Control_InventoryTab_Button_Toggle_RightPanel.ForeColor =
                        Model_AppVariables.UserUiColors.SuccessColor ?? Color.Green;
                }
            }

            Helper_UI_ComboBoxes.DeselectAllComboBoxText(this);
        }

        #endregion

        #region ComboBox & UI Events

        private void Control_InventoryTab_ComboBox_Location_SelectedIndexChanged()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["SelectedIndex"] = Control_InventoryTab_ComboBox_Location.SelectedIndex,
                ["SelectedText"] = Control_InventoryTab_ComboBox_Location.Text
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Location_SelectedIndexChanged));

            try
            {
                LoggingUtility.Log("Inventory Location ComboBox selection changed.");

                if (Control_InventoryTab_ComboBox_Location.SelectedIndex > 0)
                {
                    Control_InventoryTab_ComboBox_Location.ForeColor =
                        Model_AppVariables.UserUiColors.ComboBoxForeColor ?? Color.Black;
                    Model_AppVariables.Location = Control_InventoryTab_ComboBox_Location.Text;
                    Service_DebugTracer.TraceMethodExit(new { Valid = true, Location = Model_AppVariables.Location }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Location_SelectedIndexChanged));
                }
                else
                {
                    Control_InventoryTab_ComboBox_Location.ForeColor =
                        Model_AppVariables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                    if (Control_InventoryTab_ComboBox_Location.SelectedIndex != 0 &&
                        Control_InventoryTab_ComboBox_Location.Items.Count > 0)
                    {
                        Control_InventoryTab_ComboBox_Location.SelectedIndex = 0;
                    }

                    Model_AppVariables.Location = null;
                    Service_DebugTracer.TraceMethodExit(new { Valid = false }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Location_SelectedIndexChanged));
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Location_SelectedIndexChanged));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_ComboBox_Location_SelectedIndexChanged),
                        ["SelectedIndex"] = Control_InventoryTab_ComboBox_Location.SelectedIndex
                    },
                    controlName: nameof(Control_InventoryTab_ComboBox_Location));
            }
        }

        private void Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["SelectedIndex"] = Control_InventoryTab_ComboBox_Operation.SelectedIndex,
                ["SelectedText"] = Control_InventoryTab_ComboBox_Operation.Text
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged));

            try
            {
                LoggingUtility.Log("Inventory Op ComboBox selection changed.");

                if (Control_InventoryTab_ComboBox_Operation.SelectedIndex > 0)
                {
                    Control_InventoryTab_ComboBox_Operation.ForeColor =
                        Model_AppVariables.UserUiColors.ComboBoxForeColor ?? Color.Black;
                    Model_AppVariables.Operation = Control_InventoryTab_ComboBox_Operation.Text;
                    Service_DebugTracer.TraceMethodExit(new { Valid = true, Operation = Model_AppVariables.Operation }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged));
                }
                else
                {
                    Control_InventoryTab_ComboBox_Operation.ForeColor =
                        Model_AppVariables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                    if (Control_InventoryTab_ComboBox_Operation.SelectedIndex != 0 &&
                        Control_InventoryTab_ComboBox_Operation.Items.Count > 0)
                    {
                        Control_InventoryTab_ComboBox_Operation.SelectedIndex = 0;
                    }

                    Model_AppVariables.Operation = null;
                    Service_DebugTracer.TraceMethodExit(new { Valid = false }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged));
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged),
                        ["SelectedIndex"] = Control_InventoryTab_ComboBox_Operation.SelectedIndex
                    },
                    controlName: nameof(Control_InventoryTab_ComboBox_Operation));
            }
        }

        private void Control_InventoryTab_ComboBox_Part_SelectedIndexChanged()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["SelectedIndex"] = Control_InventoryTab_ComboBox_Part.SelectedIndex,
                ["SelectedText"] = Control_InventoryTab_ComboBox_Part.Text
            }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Part_SelectedIndexChanged));

            try
            {
                LoggingUtility.Log("Inventory Part ComboBox selection changed.");

                if (Control_InventoryTab_ComboBox_Part.SelectedIndex > 0)
                {
                    Control_InventoryTab_ComboBox_Part.ForeColor =
                        Model_AppVariables.UserUiColors.ComboBoxForeColor ?? Color.Black;
                    Model_AppVariables.PartId = Control_InventoryTab_ComboBox_Part.Text;
                    Service_DebugTracer.TraceMethodExit(new { Valid = true, PartId = Model_AppVariables.PartId }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Part_SelectedIndexChanged));
                }
                else
                {
                    Control_InventoryTab_ComboBox_Part.ForeColor =
                        Model_AppVariables.UserUiColors.ComboBoxErrorForeColor ?? Color.Red;
                    if (Control_InventoryTab_ComboBox_Part.SelectedIndex != 0 &&
                        Control_InventoryTab_ComboBox_Part.Items.Count > 0)
                    {
                        Control_InventoryTab_ComboBox_Part.SelectedIndex = 0;
                    }

                    Model_AppVariables.PartId = null;
                    Service_DebugTracer.TraceMethodExit(new { Valid = false }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Part_SelectedIndexChanged));
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_ComboBox_Part_SelectedIndexChanged));
                
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_ComboBox_Part_SelectedIndexChanged),
                        ["SelectedIndex"] = Control_InventoryTab_ComboBox_Part.SelectedIndex
                    },
                    controlName: nameof(Control_InventoryTab_ComboBox_Part));
            }
        }

        private void Control_InventoryTab_TextBox_Quantity_TextChanged()
        {
            try
            {
                LoggingUtility.Log("Inventory Quantity TextBox changed.");

                string text = Control_InventoryTab_TextBox_Quantity.Text.Trim();
                const string placeholder = "[ Enter Valid Quantity ]";
                bool isValid = int.TryParse(text, out int qty) && qty > 0;

                if (isValid)
                {
                    Control_InventoryTab_TextBox_Quantity.ForeColor =
                        Model_AppVariables.UserUiColors.TextBoxForeColor ?? Color.Black;
                }
                else
                {
                    Control_InventoryTab_TextBox_Quantity.ForeColor =
                        Model_AppVariables.UserUiColors.TextBoxErrorForeColor ?? Color.Red;
                    if (text != placeholder)
                    {
                        Control_InventoryTab_TextBox_Quantity.Text = placeholder;
                        Control_InventoryTab_TextBox_Quantity.SelectionStart =
                            Control_InventoryTab_TextBox_Quantity.Text.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_TextBox_Quantity_TextChanged),
                        ["ControlName"] = nameof(Control_InventoryTab_TextBox_Quantity)
                    },
                    controlName: nameof(Control_InventoryTab_TextBox_Quantity));
            }
        }

        private void Control_InventoryTab_Update_SaveButtonState()
        {
            try
            {
                bool partValid = Control_InventoryTab_ComboBox_Part.SelectedIndex > 0 &&
                                 !string.IsNullOrWhiteSpace(Control_InventoryTab_ComboBox_Part.Text);
                bool opValid = Control_InventoryTab_ComboBox_Operation.SelectedIndex > 0 &&
                               !string.IsNullOrWhiteSpace(Control_InventoryTab_ComboBox_Operation.Text);
                bool locValid = Control_InventoryTab_ComboBox_Location.SelectedIndex > 0 &&
                                !string.IsNullOrWhiteSpace(Control_InventoryTab_ComboBox_Location.Text);
                bool qtyValid = int.TryParse(Control_InventoryTab_TextBox_Quantity.Text.Trim(), out int qty) && qty > 0;
                Control_InventoryTab_Button_Save.Enabled = partValid && opValid && locValid && qtyValid;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_Update_SaveButtonState)
                    },
                    controlName: nameof(Control_InventoryTab));
            }
        }

        private void Control_InventoryTab_OnStartup_WireUpEvents()
        {
            try
            {
                Control_InventoryTab_Button_Save.Click +=
                    async (s, e) => await Control_InventoryTab_Button_Save_Click_Async();
                Control_InventoryTab_Button_Reset.Click += (s, e) => Control_InventoryTab_Button_Reset_Click();

                Control_InventoryTab_ComboBox_Part.SelectedIndexChanged += (s, e) =>
                {
                    Control_InventoryTab_ComboBox_Part_SelectedIndexChanged();
                    Helper_UI_ComboBoxes.ValidateComboBoxItem(Control_InventoryTab_ComboBox_Part,
                        "[ Enter Part Number ]");
                    Control_InventoryTab_Update_SaveButtonState();
                };
                Control_InventoryTab_ComboBox_Part.Leave += (s, e) =>
                {
                    Helper_UI_ComboBoxes.ValidateComboBoxItem(Control_InventoryTab_ComboBox_Part,
                        "[ Enter Part Number ]");
                };

                Control_InventoryTab_ComboBox_Operation.SelectedIndexChanged += (s, e) =>
                {
                    Control_InventoryTab_ComboBox_Operation_SelectedIndexChanged();
                    Helper_UI_ComboBoxes.ValidateComboBoxItem(Control_InventoryTab_ComboBox_Operation,
                        "[ Enter Operation ]");
                    Control_InventoryTab_Update_SaveButtonState();
                };
                Control_InventoryTab_ComboBox_Operation.Leave += (s, e) =>
                {
                    Helper_UI_ComboBoxes.ValidateComboBoxItem(Control_InventoryTab_ComboBox_Operation,
                        "[ Enter Operation ]");
                };

                Control_InventoryTab_ComboBox_Location.SelectedIndexChanged += (s, e) =>
                {
                    Control_InventoryTab_ComboBox_Location_SelectedIndexChanged();
                    Helper_UI_ComboBoxes.ValidateComboBoxItem(Control_InventoryTab_ComboBox_Location,
                        "[ Enter Location ]");
                    Control_InventoryTab_Update_SaveButtonState();
                };
                Control_InventoryTab_ComboBox_Location.Leave += (s, e) =>
                {
                    Helper_UI_ComboBoxes.ValidateComboBoxItem(Control_InventoryTab_ComboBox_Location,
                        "[ Enter Location ]");
                };

                Control_InventoryTab_TextBox_Quantity.TextChanged += (s, e) =>
                {
                    Control_InventoryTab_TextBox_Quantity_TextChanged();
                    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity,
                        "[ Enter Valid Quantity ]");
                    Control_InventoryTab_Update_SaveButtonState();
                };
                Control_InventoryTab_TextBox_Quantity.Leave += (s, e) =>
                {
                    Control_AdvancedInventory.ValidateQtyTextBox(Control_InventoryTab_TextBox_Quantity,
                        "[ Enter Valid Quantity ]");
                };

                Control_InventoryTab_Button_AdvancedEntry.Click +=
                    (s, e) => Control_InventoryTab_Button_AdvancedEntry_Click();

                Control_InventoryTab_TextBox_Quantity.KeyDown += (sender, e) =>
                    MainFormControlHelper.AdjustQuantityByKey_Quantity(sender, e, "[ Enter Valid Quantity ]",
                        Model_AppVariables.UserUiColors.TextBoxForeColor ?? Color.Black,
                        Model_AppVariables.UserUiColors.TextBoxErrorForeColor ?? Color.Red);

                LoggingUtility.Log("Inventory tab events wired up.");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    ErrorSeverity.High,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_OnStartup_WireUpEvents)
                    },
                    controlName: nameof(Control_InventoryTab));
            }
        }

        /// <summary>
        /// Updates the version label to display client and server version comparison.
        /// </summary>
        /// <param name="currentVersion">The current client application version</param>
        /// <param name="serverVersion">The server database version retrieved from version check</param>
        /// <remarks>
        /// Thread-safe method that marshals to UI thread if called from background thread.
        /// Updates label color to red if versions mismatch (out of date), otherwise uses default label color.
        /// Called by Service_Timer_VersionChecker after periodic version checks.
        /// </remarks>
        public void SetVersionLabel(string currentVersion, string serverVersion)
        {
            if (Control_InventoryTab_Label_Version.InvokeRequired)
            {
                Control_InventoryTab_Label_Version.Invoke(new Action(() =>
                    SetVersionLabel(currentVersion, serverVersion)));
                return;
            }

            bool isOutOfDate = currentVersion != serverVersion;
            Control_InventoryTab_Label_Version.Text =
                $@"Client Version: {currentVersion} | Server Version: {serverVersion}";
            Control_InventoryTab_Label_Version.ForeColor = isOutOfDate
                ? Model_AppVariables.UserUiColors.ErrorColor ?? Color.Red
                : Model_AppVariables.UserUiColors.LabelForeColor ?? SystemColors.ControlText;
        }

        #endregion

        #endregion
    }

    #endregion
}
