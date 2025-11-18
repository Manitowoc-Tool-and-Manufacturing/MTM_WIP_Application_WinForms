using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using DocumentFormat.OpenXml.Office.CustomUI;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.MainForm.Classes;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    #region InventoryTab

    public partial class Control_InventoryTab : ThemedUserControl
    {
        #region Fields

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static Forms.MainForm.MainForm? MainFormInstance { get; set; }

        private Helper_StoredProcedureProgress? _progressHelper;
        private Control_TextAnimationSequence? _quickButtonsPanelAnimator;

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

        /// <summary>
        /// Handles the "OTHER" color code selection by prompting user for custom color.
        /// </summary>
        /// <remarks>
        /// When user selects "OTHER" from color code dropdown:
        /// 1. Shows input dialog for custom color entry
        /// 2. Formats color to title case
        /// 3. Adds to database via Dao_ColorCode.AddCustomColorAsync
        /// 4. Replaces "OTHER" text with the custom color
        /// </remarks>
        private async void HandleColorCodeOtherSelection()
        {
            if (Control_InventoryTab_SuggestionBox_ColorCode.Text?.Trim().Equals("OTHER", StringComparison.OrdinalIgnoreCase) == true)
            {
                try
                {
                    using var dialog = new Form
                    {
                        Text = "Enter Custom Color",
                        Width = 400,
                        Height = 150,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        StartPosition = FormStartPosition.CenterParent,
                        MaximizeBox = false,
                        MinimizeBox = false
                    };

                    var label = new Label { Left = 20, Top = 20, Text = "Color Name:", Width = 100 };
                    var textBox = new TextBox { Left = 130, Top = 20, Width = 230 };
                    var okButton = new System.Windows.Forms.Button { Text = "OK", Left = 130, Width = 100, Top = 60, DialogResult = DialogResult.OK };
                    var cancelButton = new System.Windows.Forms.Button { Text = "Cancel", Left = 240, Width = 100, Top = 60, DialogResult = DialogResult.Cancel };

                    dialog.Controls.Add(label);
                    dialog.Controls.Add(textBox);
                    dialog.Controls.Add(okButton);
                    dialog.Controls.Add(cancelButton);
                    dialog.AcceptButton = okButton;
                    dialog.CancelButton = cancelButton;

                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Capture user-entered value; store to DB in ALL CAPS
                        var userEnteredColor = textBox.Text.Trim();
                        var colorForDatabase = userEnteredColor.ToUpperInvariant();

                        // Add to database (stored as ALL CAPS)
                        var dao = new Dao_ColorCode();
                        var result = await dao.AddCustomColorAsync(colorForDatabase);

                        if (result.IsSuccess)
                        {
                            // Reload cache to include new custom color
                            await Model_Application_Variables.ReloadColorCodePartsAsync();

                            // Do NOT change user's casing in the textbox
                            Control_InventoryTab_SuggestionBox_ColorCode.Text = userEnteredColor;

                        }
                        else
                        {
                            Service_ErrorHandler.ShowWarning($"Failed to add custom color: {result.ErrorMessage}");
                            Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                        }
                    }
                    else
                    {
                        // User cancelled - clear the "OTHER" text
                        Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(
                        ex,
                        Enum_ErrorSeverity.Medium,
                        contextData: new Dictionary<string, object>
                        {
                            ["Control"] = nameof(Control_InventoryTab),
                            ["Method"] = nameof(HandleColorCodeOtherSelection)
                        },
                        callerName: nameof(HandleColorCodeOtherSelection),
                        controlName: this.Name);

                    Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Validates and formats the work order number.
        /// </summary>
        /// <remarks>
        /// Applies WO-###### formatting with zero-padding.
        /// Accepts 5-6 digit numbers with or without WO- prefix.
        /// Shows error message for invalid formats.
        /// </remarks>
        private void ValidateAndFormatWorkOrder()
        {
            var input = Control_InventoryTab_SuggestionBox_WorkOrder.Text?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                // Empty is valid - will default to "Unknown" on save
                return;
            }

            if (Service_ColorCodeValidator.ValidateAndFormatWorkOrder(input, out string formatted, out string errorMessage))
            {
                Control_InventoryTab_SuggestionBox_WorkOrder.Text = formatted;
            }
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
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // DPI scaling, layout adjustments, and focus highlighting applied automatically by ThemedUserControl base class
            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "AUTO_APPLIED_BY_BASE",
                    ["LayoutAdjustments"] = "AUTO_APPLIED_BY_BASE",
                    ["FocusHighlighting"] = "AUTO_APPLIED_BY_BASE"
                });


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

            InitializeQuickButtonsPanelAnimator();

            Service_DebugTracer.TraceUIAction("VERSION_TIMER_SETUP", nameof(Control_InventoryTab),
                new Dictionary<string, object> { ["TimerInstance"] = "Service_Timer_VersionChecker" });
            Service_Timer_VersionChecker.ControlInventoryInstance = this;

            Service_DebugTracer.TraceUIAction("DATA_LOADING_START", nameof(Control_InventoryTab),
                new Dictionary<string, object> { ["DataType"] = "ComboBoxes" });
            _ = Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync();

            // Quantity uses validated textbox mode (no suggestions) – configure validator so control stays enabled
            Helper_ValidatedTextBox.ConfigureForQuantity(Control_InventoryTab_SuggestionBox_Quantity);
            Helper_ValidatedTextBox.ConfigureForWorkOrder(Control_InventoryTab_SuggestionBox_WorkOrder);

            Service_DebugTracer.TraceUIAction("EVENTS_WIREUP", nameof(Control_InventoryTab));
            Control_InventoryTab_OnStartup_WireUpEvents();

            Service_DebugTracer.TraceUIAction("VERSION_LABEL_SET", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["UserVersion"] = Model_Application_Variables.UserVersion,
                    ["DatabaseVersion"] = Service_Timer_VersionChecker.LastCheckedDatabaseVersion ?? "unknown"
                });
            SetVersionLabel(Model_Application_Variables.UserVersion,
                Service_Timer_VersionChecker.LastCheckedDatabaseVersion ?? "unknown");

            Service_DebugTracer.TraceUIAction("UI_STYLING_APPLIED", nameof(Control_InventoryTab),
                new Dictionary<string, object>
                {
                    ["FocusHighlighting"] = "AUTO_APPLIED_BY_BASE",
                    ["ComboBoxColors"] = "Applied",
                    ["InitialFocus"] = "Control_InventoryTab_SuggestionBox_Part",
                    ["QuantityTextBoxState"] = "Placeholder"
                });
            // ForeColor is now managed automatically by SuggestionTextBox and validation logic
            Control_InventoryTab_SuggestionBox_Part.TextBox.Focus();

            // PlaceholderText is now set in Designer instead of programmatically setting Text

            // Disable tab stop on F4 buttons - they should not be in tab order
            Control_InventoryTab_SuggestionBox_Part.SetF4ButtonTabStop(false);
            Control_InventoryTab_SuggestionBox_Operation.SetF4ButtonTabStop(false);
            Control_InventoryTab_SuggestionBox_Location.SetF4ButtonTabStop(false);
            Control_InventoryTab_SuggestionBox_ColorCode.SetF4ButtonTabStop(false);

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
            bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;
            bool isAdmin = Model_Application_Variables.UserTypeAdmin;
            bool isNormal = Model_Application_Variables.UserTypeNormal;
            bool isReadOnly = Model_Application_Variables.UserTypeReadOnly;

            // Developers have all Admin privileges
            bool hasAdminAccess = isDeveloper || isAdmin;

            // Admin/Developer and Normal: all controls visible/enabled
            // Read-Only: only specific controls visible/enabled
            Control_InventoryTab_GroupBox_Main.Visible = hasAdminAccess || isNormal || isReadOnly;
            Control_InventoryTab_Button_Reset.Visible = hasAdminAccess || isNormal;
            Control_InventoryTab_Button_Save.Visible = hasAdminAccess || isNormal;
            Control_InventoryTab_Label_Version.Visible = true;
            Control_InventoryTab_Button_AdvancedEntry.Visible = hasAdminAccess || isNormal;
            Control_InventoryTab_SuggestionBox_Part.Visible = true;
            Control_InventoryTab_SuggestionBox_Operation.Visible = true;
            Control_InventoryTab_SuggestionBox_Location.Visible = true;
            Control_InventoryTab_SuggestionBox_Quantity.Visible = hasAdminAccess || isNormal || isReadOnly;
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
                Control_InventoryTab_SuggestionBox_Part.TextBox.Enabled = false;
                Control_InventoryTab_SuggestionBox_Operation.TextBox.Enabled = false;
                Control_InventoryTab_SuggestionBox_Location.TextBox.Enabled = false;
                Control_InventoryTab_SuggestionBox_Quantity.Enabled = false;
                Control_InventoryTab_RichTextBox_Notes.ReadOnly = true;
                Control_InventoryTab_Button_Save.Visible = false;
                Control_InventoryTab_Button_Reset.Visible = false;
                Control_InventoryTab_Button_AdvancedEntry.Visible = false;
                Control_InventoryTab_Button_Toggle_RightPanel.Visible = false;
            }
            else
            {
                Control_InventoryTab_SuggestionBox_Part.TextBox.Enabled = true;
                Control_InventoryTab_SuggestionBox_Operation.TextBox.Enabled = true;
                Control_InventoryTab_SuggestionBox_Location.TextBox.Enabled = true;
                Control_InventoryTab_SuggestionBox_Quantity.Enabled = true;
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
                _progressHelper?.UpdateProgress(5, "Loading validation data...");

                // Load validation data for exact match checking
                await Helper_UI_SuggestionBoxes.LoadAllDataAsync();

                _progressHelper?.UpdateProgress(8, "Loading color code caches...");
                await Model_Application_Variables.ReloadColorCodePartsAsync();


                Control_InventoryTab_Button_Save.Enabled = false;
                _progressHelper?.UpdateProgress(10, "Configuring part suggestions...");

                // Configure SuggestionTextBox controls using helper methods with F4 support
                Helper_SuggestionTextBox.ConfigureForPartNumbers(
                    Control_InventoryTab_SuggestionBox_Part.TextBox,
                    GetPartNumberSuggestionsAsync,
                    enableF4: false); // F4 handled by composite control button

                _progressHelper?.UpdateProgress(40, "Configuring operation suggestions...");

                Helper_SuggestionTextBox.ConfigureForOperations(
                    Control_InventoryTab_SuggestionBox_Operation.TextBox,
                    GetOperationSuggestionsAsync,
                    enableF4: false);

                _progressHelper?.UpdateProgress(70, "Configuring location suggestions...");

                Helper_SuggestionTextBox.ConfigureForLocations(
                    Control_InventoryTab_SuggestionBox_Location.TextBox,
                    GetLocationSuggestionsAsync,
                    enableF4: false);

                _progressHelper?.UpdateProgress(85, "Configuring color code suggestions...");

                Helper_SuggestionTextBox.ConfigureForColorCodes(
                    Control_InventoryTab_SuggestionBox_ColorCode.TextBox,
                    GetColorCodeSuggestionsAsync,
                    enableF4: false);

                _progressHelper?.UpdateProgress(100, "Combo boxes loaded");
                await Task.Delay(100);


                Service_DebugTracer.TraceMethodExit("Success", nameof(Control_InventoryTab), nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync));
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceMethodExit(new { Exception = ex.Message }, nameof(Control_InventoryTab), nameof(Control_InventoryTab_OnStartup_LoadDataComboBoxesAsync));

                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.High,
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
                        ["Control"] = nameof(Control_InventoryTab),
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
                        ["Control"] = nameof(Control_InventoryTab),
                        ["Method"] = nameof(GetOperationSuggestionsAsync)
                    },
                    callerName: nameof(GetOperationSuggestionsAsync),
                    controlName: this.Name);

                return new List<string>();
            }
        }

        /// <summary>
        /// Data provider for location SuggestionTextBox.
        /// Returns list of all location values from database.
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
                        ["Control"] = nameof(Control_InventoryTab),
                        ["Method"] = nameof(GetLocationSuggestionsAsync)
                    },
                    callerName: nameof(GetLocationSuggestionsAsync),
                    controlName: this.Name);

                return new List<string>();
            }
        }

        /// <summary>
        /// Data provider for color code SuggestionTextBox.
        /// Returns list of all color codes from database plus "OTHER" option.
        /// </summary>
        /// <returns>List of color code strings.</returns>
        private async Task<List<string>> GetColorCodeSuggestionsAsync()
        {
            try
            {
                var dao = new Dao_ColorCode();
                var dataResult = await dao.GetAllAsync();

                if (!dataResult.IsSuccess || dataResult.Data == null)
                {
                    Service_ErrorHandler.ShowWarning(dataResult.ErrorMessage ?? "Failed to load color codes");
                    return new List<string> { "OTHER" };
                }

                var suggestions = new List<string>();
                foreach (System.Data.DataRow row in dataResult.Data.Rows)
                {
                    if (row["ColorCode"] != null && row["ColorCode"] != DBNull.Value)
                    {
                        suggestions.Add(row["ColorCode"].ToString() ?? string.Empty);
                    }
                }

                // Add "OTHER" option at the end for custom colors
                suggestions.Add("OTHER");

                return suggestions;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_InventoryTab),
                        ["Method"] = nameof(GetColorCodeSuggestionsAsync)
                    },
                    callerName: nameof(GetColorCodeSuggestionsAsync),
                    controlName: this.Name);

                return new List<string> { "OTHER" };
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
                    Enum_ErrorSeverity.Medium,
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
                        ?.GetValue(adv) is System.Windows.Forms.ComboBox combo && combo.Items.Count > 0)
                {
                    combo.SelectedIndex = 0;
                    combo.Focus();
                    combo.SelectAll();
                }

                if (adv.GetType().GetField("AdvancedInventory_Single_ComboBox_Op",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is System.Windows.Forms.ComboBox op && op.Items.Count > 0)
                {
                    op.SelectedIndex = 0;
                }

                if (adv.GetType().GetField("AdvancedInventory_Single_ComboBox_Loc",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is System.Windows.Forms.ComboBox loc && loc.Items.Count > 0)
                {
                    loc.SelectedIndex = 0;
                }

                if (adv.GetType().GetField("AdvancedInventory_MultiLoc_ComboBox_Part",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is System.Windows.Forms.ComboBox multiPart && multiPart.Items.Count > 0)
                {
                    multiPart.SelectedIndex = 0;
                }

                if (adv.GetType().GetField("AdvancedInventory_MultiLoc_ComboBox_Op",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is System.Windows.Forms.ComboBox multiOp && multiOp.Items.Count > 0)
                {
                    multiOp.SelectedIndex = 0;
                }

                if (adv.GetType().GetField("AdvancedInventory_MultiLoc_ComboBox_Loc",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.GetValue(adv) is System.Windows.Forms.ComboBox multiLoc && multiLoc.Items.Count > 0)
                {
                    multiLoc.SelectedIndex = 0;
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
                    Enum_ErrorSeverity.Medium,
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
                    Enum_ErrorSeverity.Medium,
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

                Debug.WriteLine("[DEBUG] Resetting UI fields");
                // Reset SuggestionTextBox fields (Part, Operation, and Location)
                Control_InventoryTab_SuggestionBox_Part.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_Operation.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_Location.Text = string.Empty;

                Control_InventoryTab_SuggestionBox_Quantity.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_WorkOrder.Text = string.Empty;
                MainFormControlHelper.ResetRichTextBox(Control_InventoryTab_RichTextBox_Notes,
                    Model_Application_Variables.UserUiColors.RichTextBoxForeColor ?? Color.Black, "");

                UpdateColorCodeFieldsVisibility();
                Control_InventoryTab_SuggestionBox_Part.TextBox.Focus();

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
                    Enum_ErrorSeverity.High,
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
                // Reset SuggestionTextBox fields (Part and Operation)
                Control_InventoryTab_SuggestionBox_Part.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_Operation.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_Location.Text = string.Empty;

                Control_InventoryTab_SuggestionBox_Quantity.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                Control_InventoryTab_SuggestionBox_WorkOrder.Text = string.Empty;
                MainFormControlHelper.ResetRichTextBox(Control_InventoryTab_RichTextBox_Notes,
                    Model_Application_Variables.UserUiColors.TextBoxForeColor ?? Color.Black, "");
                Control_InventoryTab_Button_Save.Enabled = false;
                UpdateColorCodeFieldsVisibility();
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
                    Enum_ErrorSeverity.Medium,
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
                Control_InventoryTab_SuggestionBox_Part.TextBox.Focus();
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


                string partId = Control_InventoryTab_SuggestionBox_Part.Text ?? string.Empty;
                string op = Control_InventoryTab_SuggestionBox_Operation.Text ?? string.Empty;
                string loc = Control_InventoryTab_SuggestionBox_Location.Text ?? string.Empty;
                string qtyText = Control_InventoryTab_SuggestionBox_Quantity.Text?.Trim() ?? string.Empty;
                string notes = Control_InventoryTab_RichTextBox_Notes.Text.Trim();
                string? colorCode = Control_InventoryTab_SuggestionBox_ColorCode.TextBox.Visible ? Control_InventoryTab_SuggestionBox_ColorCode.Text?.Trim() : null;
                string? workOrder = Control_InventoryTab_SuggestionBox_WorkOrder.Visible ? Control_InventoryTab_SuggestionBox_WorkOrder.Text?.Trim() : null;

                if (string.IsNullOrWhiteSpace(partId))
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please enter a valid Part.",
                        nameof(Control_InventoryTab_SuggestionBox_Part));
                    Control_InventoryTab_SuggestionBox_Part.TextBox.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Part invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                if (string.IsNullOrWhiteSpace(op))
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please enter a valid Operation.",
                        nameof(Control_InventoryTab_SuggestionBox_Operation));
                    Control_InventoryTab_SuggestionBox_Operation.TextBox.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Operation invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                if (string.IsNullOrWhiteSpace(loc))
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please enter a valid Location.",
                        nameof(Control_InventoryTab_SuggestionBox_Location));
                    Control_InventoryTab_SuggestionBox_Location.TextBox.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Location invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                if (!int.TryParse(qtyText, out int qty) || qty <= 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Please enter a valid quantity.",
                        nameof(Control_InventoryTab_SuggestionBox_Quantity));
                    Control_InventoryTab_SuggestionBox_Quantity.Focus();
                    Service_DebugTracer.TraceMethodExit("Validation failed: Quantity invalid", nameof(Control_InventoryTab), nameof(Control_InventoryTab_Button_Save_Click_Async));
                    return;
                }

                Model_Application_Variables.PartId = partId;
                Model_Application_Variables.Operation = op;
                Model_Application_Variables.Location = loc;
                Model_Application_Variables.Notes = notes;
                Model_Application_Variables.InventoryQuantity = qty;
                Model_Application_Variables.User ??= Environment.UserName;

                _progressHelper?.UpdateProgress(40, "Adding inventory item...");

                Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                {
                    ["PartId"] = partId,
                    ["Location"] = loc,
                    ["Operation"] = op,
                    ["Quantity"] = qty,
                    ["User"] = Model_Application_Variables.User
                }, nameof(Control_InventoryTab), "AddInventoryItemAsync");

                // Verify the transaction succeeded before proceeding
                var inventoryResult = await Dao_Inventory.AddInventoryItemAsync(
                    partId,
                    loc,
                    op,
                    qty,
                    "",
                    Model_Application_Variables.User,
                    "",
                    notes,
                    colorCode,
                    workOrder,
                    true);

                Service_DebugTracer.TraceMethodExit(new { IsSuccess = inventoryResult.IsSuccess, Message = inventoryResult.StatusMessage }, nameof(Control_InventoryTab), "AddInventoryItemAsync");

                // Check if the inventory transaction was successful
                if (!inventoryResult.IsSuccess)
                {
                    LoggingUtility.LogApplicationError(new Exception($"Inventory transaction failed: {inventoryResult.ErrorMessage}"));

                    Service_ErrorHandler.HandleException(
                        inventoryResult.Exception ?? new Exception(inventoryResult.ErrorMessage),
                        Enum_ErrorSeverity.Medium,
                        retryAction: () => { Control_InventoryTab_Button_Save.PerformClick(); return true; },
                        contextData: new Dictionary<string, object>
                        {
                            ["PartId"] = partId,
                            ["Operation"] = op,
                            ["Location"] = loc,
                            ["Quantity"] = qty,
                            ["User"] = Model_Application_Variables.User
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



                _progressHelper?.UpdateProgress(70, "Updating recent transactions...");
                await AddToLast10TransactionsIfUniqueAsync(Model_Application_Variables.User, partId, op, qty);

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
                    await MainFormInstance.MainForm_UserControl_QuickButtons.LoadLast10Transactions(Model_Application_Variables.User);
                }

                _progressHelper?.UpdateProgress(100, "Save complete");

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
                    Enum_ErrorSeverity.High,
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
                        Enum_DatabaseEnum_ErrorSeverity.Warning); // Non-critical operation, log as warning
                    Service_DebugTracer.TraceMethodExit(new { Success = false, Error = result.ErrorMessage }, nameof(Control_InventoryTab), nameof(AddToLast10TransactionsIfUniqueAsync));
                }
                else
                {
                    // CRITICAL: Refresh QuickButtons UI to match database state
                    if (MainFormInstance?.MainForm_UserControl_QuickButtons != null)
                    {
                        await MainFormInstance.MainForm_UserControl_QuickButtons.LoadLast10Transactions(user);
                    }
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
            if (MainFormInstance != null)
            {
                bool panelCollapsed = MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed;
                MainFormInstance.MainForm_SplitContainer_Middle.Panel2Collapsed = !panelCollapsed;
                UpdateQuickButtonsPanelArrow(!panelCollapsed);
            }

            Helper_UI_ComboBoxes.DeselectAllComboBoxText(this);
        }

        #endregion

        #region ComboBox & UI Events

        private void Control_InventoryTab_TextBox_Quantity_TextChanged()
        {
            try
            {
                // Intentionally left blank for future implementation
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_TextBox_Quantity_TextChanged),
                        ["ControlName"] = nameof(Control_InventoryTab_SuggestionBox_Quantity)
                    },
                    controlName: nameof(Control_InventoryTab_SuggestionBox_Quantity));
            }
        }

        private void Control_InventoryTab_Update_SaveButtonState()
        {
            try
            {
                Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
                {
                    ["PartText"] = Control_InventoryTab_SuggestionBox_Part.Text?.Trim() ?? "",
                    ["OpText"] = Control_InventoryTab_SuggestionBox_Operation.Text?.Trim() ?? "",
                    ["LocText"] = Control_InventoryTab_SuggestionBox_Location.Text?.Trim() ?? "",
                    ["QtyText"] = Control_InventoryTab_SuggestionBox_Quantity.Text?.Trim() ?? "",
                    ["ColorText"] = Control_InventoryTab_SuggestionBox_ColorCode.TextBox.Visible ? (Control_InventoryTab_SuggestionBox_ColorCode.Text?.Trim() ?? "") : "<hidden>",
                    ["WoText"] = Control_InventoryTab_SuggestionBox_WorkOrder.Visible ? (Control_InventoryTab_SuggestionBox_WorkOrder.Text?.Trim() ?? "") : "<hidden>",
                    ["ColorFieldsVisible"] = Control_InventoryTab_SuggestionBox_ColorCode.TextBox.Visible
                }, nameof(Control_InventoryTab_Update_SaveButtonState), nameof(Control_InventoryTab));
                // Check validity: Either Model_Application_Variables is set (from overlay selection)
                // OR the text matches a valid value in the master data (exact match typed)
                string partText = Control_InventoryTab_SuggestionBox_Part.Text?.Trim() ?? string.Empty;
                string opText = Control_InventoryTab_SuggestionBox_Operation.Text?.Trim() ?? string.Empty;
                string locText = Control_InventoryTab_SuggestionBox_Location.Text?.Trim() ?? string.Empty;

                bool partValid = !string.IsNullOrWhiteSpace(Model_Application_Variables.PartId)
                    || Helper_UI_SuggestionBoxes.IsValidPartId(partText);
                bool opValid = !string.IsNullOrWhiteSpace(Model_Application_Variables.Operation)
                    || Helper_UI_SuggestionBoxes.IsValidOperation(opText);
                bool locValid = !string.IsNullOrWhiteSpace(Model_Application_Variables.Location)
                    || Helper_UI_SuggestionBoxes.IsValidLocation(locText);
                bool qtyValid = int.TryParse(Control_InventoryTab_SuggestionBox_Quantity.Text?.Trim() ?? "", out int qty) && qty > 0;

                // Check if color code is required and valid
                bool colorCodeValid = true;
                if (Control_InventoryTab_SuggestionBox_ColorCode.TextBox.Visible)
                {
                    string colorCodeText = Control_InventoryTab_SuggestionBox_ColorCode.Text?.Trim() ?? string.Empty;

                    // Color code is required when visible
                    if (string.IsNullOrWhiteSpace(colorCodeText))
                    {
                        colorCodeValid = false;

                    }
                    else
                    {
                        // Fallback: if cache not yet loaded treat as tentatively valid and will revalidate once cache populated
                        if (Model_Application_Variables.ValidColorCodes.Count == 0)
                        {
                            colorCodeValid = true; // optimistic until cache loads

                        }
                        else
                        {
                            colorCodeValid = IsValidColorCode(colorCodeText);

                        }
                    }
                }

                // Check if work order is required and valid
                bool workOrderValid = true;
                if (Control_InventoryTab_SuggestionBox_WorkOrder.Visible)
                {
                    string workOrderText = Control_InventoryTab_SuggestionBox_WorkOrder.Text?.Trim() ?? string.Empty;

                    // Work order is required when visible and must pass validator (without altering textbox)
                    if (string.IsNullOrWhiteSpace(workOrderText))
                    {
                        workOrderValid = false;

                    }
                    else
                    {
                        workOrderValid = Service_ColorCodeValidator.ValidateAndFormatWorkOrder(
                            workOrderText,
                            out _,
                            out _);

                    }
                }

                // Update Model_Application_Variables from exact matches
                if (partValid && !string.IsNullOrWhiteSpace(partText))
                {
                    if (string.IsNullOrWhiteSpace(Model_Application_Variables.PartId))
                    {
                        Model_Application_Variables.PartId = partText; // Set from exact match
                    }
                }

                if (opValid && !string.IsNullOrWhiteSpace(opText))
                {
                    if (string.IsNullOrWhiteSpace(Model_Application_Variables.Operation))
                    {
                        Model_Application_Variables.Operation = opText; // Set from exact match
                    }
                }

                if (locValid && !string.IsNullOrWhiteSpace(locText))
                {
                    if (string.IsNullOrWhiteSpace(Model_Application_Variables.Location))
                    {
                        Model_Application_Variables.Location = locText; // Set from exact match
                    }
                }

                bool saveEnabled = partValid && opValid && locValid && qtyValid && colorCodeValid && workOrderValid;
                Service_DebugTracer.TraceUIAction("SAVE_BUTTON_VALIDATION", nameof(Control_InventoryTab), new Dictionary<string, object>
                {
                    ["PartValid"] = partValid,
                    ["OperationValid"] = opValid,
                    ["LocationValid"] = locValid,
                    ["QuantityValid"] = qtyValid,
                    ["ColorRequired"] = Control_InventoryTab_SuggestionBox_ColorCode.TextBox.Visible,
                    ["ColorValid"] = colorCodeValid,
                    ["WorkOrderRequired"] = Control_InventoryTab_SuggestionBox_WorkOrder.Visible,
                    ["WorkOrderValid"] = workOrderValid,
                    ["ValidColorCodesCount"] = Model_Application_Variables.ValidColorCodes.Count,
                    ["SaveEnabled"] = saveEnabled
                });
                Control_InventoryTab_Button_Save.Enabled = saveEnabled;

                Service_DebugTracer.TraceMethodExit(null, nameof(Control_InventoryTab_Update_SaveButtonState), nameof(Control_InventoryTab));
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["MethodName"] = nameof(Control_InventoryTab_Update_SaveButtonState)
                    },
                    controlName: nameof(Control_InventoryTab));
            }
        }

        /// <summary>
        /// Validates if a color code exists in the cached color codes (case-insensitive match).
        /// </summary>
        /// <param name="colorCode">The color code to validate.</param>
        /// <returns>True if the color code exists in the cache, false otherwise.</returns>
        /// <remarks>
        /// Uses Model_Application_Variables.ValidColorCodes cache for fast lookup (case-insensitive).
        /// No database call is made - cache is loaded at startup.
        /// Does not modify the textbox - only validates the input.
        /// </remarks>
        private bool IsValidColorCode(string colorCode)
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["Input"] = colorCode,
                ["CacheCount"] = Model_Application_Variables.ValidColorCodes.Count
            }, nameof(IsValidColorCode), nameof(Control_InventoryTab));

            if (string.IsNullOrWhiteSpace(colorCode))
            {
                Service_DebugTracer.TraceMethodExit(false, nameof(IsValidColorCode), nameof(Control_InventoryTab));
                return false;
            }

            var trimmed = colorCode.Trim();
            bool contains = Model_Application_Variables.ValidColorCodes.Contains(trimmed);

            Service_DebugTracer.TraceUIAction("COLOR_VALIDATION", nameof(Control_InventoryTab), new Dictionary<string, object>
            {
                ["Color"] = trimmed,
                ["Result"] = contains,
                ["CacheCount"] = Model_Application_Variables.ValidColorCodes.Count
            });

            Service_DebugTracer.TraceMethodExit(contains, nameof(IsValidColorCode), nameof(Control_InventoryTab));
            return contains;
        }
    private void Control_InventoryTab_OnStartup_WireUpEvents()

        {
            try
            {
                Control_InventoryTab_Button_Save.Click +=
                    async (s, e) => await Control_InventoryTab_Button_Save_Click_Async();
                Control_InventoryTab_Button_Reset.Click += (s, e) => Control_InventoryTab_Button_Reset_Click();

                // NOTE: Part and Operation now use SuggestionTextBox - wire up SuggestionSelected events instead
                Control_InventoryTab_SuggestionBox_Part.TextBox.SuggestionSelected += (s, e) =>
                {
                    Model_Application_Variables.PartId = e.SelectedValue;

                    Control_InventoryTab_Update_SaveButtonState();

                    // Show/hide color code and work order fields based on part requirements
                    UpdateColorCodeFieldsVisibility();
                };

                // Clear PartId when user manually types (not selecting from overlay)
                Control_InventoryTab_SuggestionBox_Part.TextChanged += (s, e) =>
                {
                    // Only clear if text doesn't match the stored valid value
                    if (Control_InventoryTab_SuggestionBox_Part.Text != Model_Application_Variables.PartId)
                    {
                        Model_Application_Variables.PartId = null;
                        Control_InventoryTab_Update_SaveButtonState();
                    }

                    // Show/hide color code and work order fields based on part requirements
                    UpdateColorCodeFieldsVisibility();
                };

                Control_InventoryTab_SuggestionBox_Operation.TextBox.SuggestionSelected += (s, e) =>
                {
                    Model_Application_Variables.Operation = e.SelectedValue;

                    Control_InventoryTab_Update_SaveButtonState();
                };

                // Clear Operation when user manually types
                Control_InventoryTab_SuggestionBox_Operation.TextChanged += (s, e) =>
                {
                    if (Control_InventoryTab_SuggestionBox_Operation.Text != Model_Application_Variables.Operation)
                    {
                        Model_Application_Variables.Operation = null;
                        Control_InventoryTab_Update_SaveButtonState();
                    }
                };

                Control_InventoryTab_SuggestionBox_Location.TextBox.SuggestionSelected += (s, e) =>
                {
                    Model_Application_Variables.Location = e.SelectedValue;

                    Control_InventoryTab_Update_SaveButtonState();
                };

                // Clear Location when user manually types
                Control_InventoryTab_SuggestionBox_Location.TextChanged += (s, e) =>
                {
                    if (Control_InventoryTab_SuggestionBox_Location.Text != Model_Application_Variables.Location)
                    {
                        Model_Application_Variables.Location = null;
                        Control_InventoryTab_Update_SaveButtonState();
                    }
                };

                // Quantity validation handler (using built-in SuggestionTextBoxWithLabel validator)
                Control_InventoryTab_SuggestionBox_Quantity.ValidationChanged += (s, e) =>
                {
                    Control_InventoryTab_Update_SaveButtonState();
                };

                Control_InventoryTab_SuggestionBox_Quantity.TextBox.TextChanged += (s, e) =>
                {
                    Control_InventoryTab_Update_SaveButtonState();
                };

                // Color code "OTHER" selection handler and standard validation
                Control_InventoryTab_SuggestionBox_ColorCode.TextBox.SuggestionSelected += (s, e) =>
                {

                    Control_InventoryTab_Update_SaveButtonState();
                };

                Control_InventoryTab_SuggestionBox_ColorCode.TextChanged += (s, e) =>
                {
                    HandleColorCodeOtherSelection();
                    Control_InventoryTab_Update_SaveButtonState();
                };

                Control_InventoryTab_SuggestionBox_ColorCode.TextBox.SuggestionCancelled += (s, e) =>
                {
                    // Clear textbox on cancel (no matches found)
                    if (e.Reason == SuggestionCancelReason.NoMatches)
                    {
                        Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                    }
                };

                // Work order validation and auto-formatting
                Control_InventoryTab_SuggestionBox_WorkOrder.TextBox.Leave += (s, e) =>
                {
                    ValidateAndFormatWorkOrder();
                };

                // Work order text changed - update save button state
                Control_InventoryTab_SuggestionBox_WorkOrder.TextBox.TextChanged += (s, e) =>
                {
                    Control_InventoryTab_Update_SaveButtonState();
                };

                Control_InventoryTab_Button_AdvancedEntry.Click +=
                    (s, e) => Control_InventoryTab_Button_AdvancedEntry_Click();

                // F4 buttons now handled automatically by SuggestionTextBoxWithLabel composite control
                // The F4 button in each composite control automatically triggers Helper_SuggestionTextBox.ShowFullSuggestionListAsync

                // Make non-essential buttons not tabbable on Inventory tab
                try
                {
                    Control_InventoryTab_Button_Reset.TabStop = false;
                    Control_InventoryTab_Button_AdvancedEntry.TabStop = false;
                    Control_InventoryTab_Button_Toggle_RightPanel.TabStop = false;
                    // Keep Save button tabbable (default TabStop = true)
                }
                catch { /* Controls may not be available at design-time */ }


            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.High,
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
                ? Model_Application_Variables.UserUiColors.ErrorColor ?? Color.Red
                : Model_Application_Variables.UserUiColors.LabelForeColor ?? SystemColors.ControlText;
        }

        #endregion

        #region Color Code Field Management

        /// <summary>
        /// Validates the color code input and shows the suggestion overlay if invalid.
        /// </summary>
        /// <remarks>
        /// When user enters an invalid color (not in cache):
        /// 1. Clears the color code textbox
        /// 2. Opens the suggestion overlay showing all available colors
        /// </remarks>
        private async Task ValidateColorCodeAndShowOverlayAsync(bool allowblank = false)
        {
            try
            {
                var colorInput = Control_InventoryTab_SuggestionBox_ColorCode.Text?.Trim();

                // Skip validation if empty or "OTHER" (handled separately)
                if (!allowblank && (string.IsNullOrWhiteSpace(colorInput) || colorInput.Equals("OTHER", StringComparison.OrdinalIgnoreCase)))
                {
                    return;
                }

                // Check if color exists in cache (only if not empty)
                if (!string.IsNullOrWhiteSpace(colorInput) && !IsValidColorCode(colorInput))
                {
                    // Invalid color - clear and show overlay


                    Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;

                    // Show suggestion overlay with all colors
                    await Control_InventoryTab_SuggestionBox_ColorCode.TextBox.ShowSuggestionsAsync();
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_InventoryTab),
                        ["Method"] = nameof(ValidateColorCodeAndShowOverlayAsync)
                    },
                    callerName: nameof(ValidateColorCodeAndShowOverlayAsync),
                    controlName: this.Name);
            }
        }

        /// <summary>
        /// Shows or hides the color code and work order fields based on the selected part.
        /// </summary>
        /// <remarks>
        /// Color code fields are shown if the part is in the ColorCodeParts cache.
        /// This method is called when the Part TextBox value changes.
        /// </remarks>
        internal void UpdateColorCodeFieldsVisibility()
        {
            try
            {
                Service_DebugTracer.TraceMethodEntry(null, nameof(UpdateColorCodeFieldsVisibility), nameof(Control_InventoryTab));

                var partText = Control_InventoryTab_SuggestionBox_Part.Text?.Trim();
                bool inCache = !string.IsNullOrWhiteSpace(partText) && Model_Application_Variables.ColorCodeParts.Contains(partText);

                Service_DebugTracer.TraceUIAction("COLOR_FIELDS_VISIBILITY", nameof(Control_InventoryTab), new Dictionary<string, object>
                {
                    ["PartText"] = partText ?? "",
                    ["ColorCodePartsCount"] = Model_Application_Variables.ColorCodeParts.Count,
                    ["RequiresColorCode"] = inCache
                });

                // Show/hide color code and work order fields
                Control_InventoryTab_SuggestionBox_ColorCode.Visible = inCache;
                Control_InventoryTab_SuggestionBox_WorkOrder.Visible = inCache;

                // Collapse/expand rows in the top group when fields are hidden/shown
                if (Control_InventoryTab_TableLayout_TopGroup != null && Control_InventoryTab_TableLayout_TopGroup.RowStyles.Count >= 6)
                {
                    var tlp = Control_InventoryTab_TableLayout_TopGroup;
                    tlp.SuspendLayout();
                    try
                    {
                        if (inCache)
                        {
                            // Let rows auto-size when visible
                            tlp.RowStyles[4].SizeType = SizeType.AutoSize;
                            tlp.RowStyles[4].Height = 0; // Height ignored for AutoSize
                            tlp.RowStyles[5].SizeType = SizeType.AutoSize;
                            tlp.RowStyles[5].Height = 0;
                        }
                        else
                        {
                            // Collapse rows completely when hidden
                            tlp.RowStyles[4].SizeType = SizeType.Absolute;
                            tlp.RowStyles[4].Height = 0;
                            tlp.RowStyles[5].SizeType = SizeType.Absolute;
                            tlp.RowStyles[5].Height = 0;
                        }
                    }
                    finally
                    {
                        tlp.ResumeLayout(true);
                        tlp.PerformLayout();
                    }
                }

                // Clear fields when hiding
                if (!inCache)
                {
                    Control_InventoryTab_SuggestionBox_ColorCode.Text = string.Empty;
                    Control_InventoryTab_SuggestionBox_WorkOrder.Text = string.Empty;
                }

                Service_DebugTracer.TraceMethodExit(new Dictionary<string, object>
                {
                    ["Visible"] = inCache
                }, nameof(UpdateColorCodeFieldsVisibility), nameof(Control_InventoryTab));
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_InventoryTab),
                        ["Method"] = nameof(UpdateColorCodeFieldsVisibility),
                        ["PartText"] = Control_InventoryTab_SuggestionBox_Part.Text ?? "NULL"
                    },
                    callerName: nameof(UpdateColorCodeFieldsVisibility),
                    controlName: this.Name);
            }
        }

        #endregion

        #region Helpers

        private void InitializeQuickButtonsPanelAnimator()
        {
            try
            {
                Helper_ButtonToggleAnimations.ValidateIconButton(
                    Control_InventoryTab_Button_Toggle_RightPanel,
                    nameof(Control_InventoryTab));

                bool collapsed = MainFormInstance?.MainForm_SplitContainer_Middle.Panel2Collapsed ?? false;
                UpdateQuickButtonsPanelArrow(collapsed);

            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_DebugTracer.TraceBusinessLogic("INVENTORY_TAB_ANIMATION_INIT_FAILED",
                    inputData: new Dictionary<string, object>
                    {
                        ["Control"] = nameof(Control_InventoryTab_Button_Toggle_RightPanel)
                    },
                    validationResults: new Dictionary<string, object>
                    {
                        ["Exception"] = ex.Message
                    });
            }
        }

        private void UpdateQuickButtonsPanelArrow(bool panelCollapsed)
        {
            Helper_ButtonToggleAnimations.ApplyHorizontalArrow(
                ref _quickButtonsPanelAnimator,
                components,
                Control_InventoryTab_Button_Toggle_RightPanel,
                panelCollapsed);

        }

        internal void SyncQuickButtonsPanelState(bool panelCollapsed)
        {
            UpdateQuickButtonsPanelArrow(panelCollapsed);
        }

        #endregion

        #endregion
    }

    #endregion
}
