using System.Data;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Control for editing existing part IDs.
    /// Provides part selection via SuggestionTextBox, item type assignment, and color code requirement configuration.
    /// </summary>
    public partial class Control_Edit_PartID : ThemedUserControl
    {
        #region Fields

        private DataRow? _currentPart;
        private bool _originalRequiresColorCode;

        #endregion

        #region Events

        /// <summary>
        /// Event raised when a part has been successfully updated.
        /// </summary>
        public event EventHandler? PartUpdated;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Control_Edit_PartID control.
        /// Configures SuggestionTextBox controls for Part and ItemType selection with F4 support.
        /// </summary>
        public Control_Edit_PartID()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_Edit_PartID),
                ["InitializationTime"] = DateTime.Now
            }, nameof(Control_Edit_PartID), nameof(Control_Edit_PartID));

            try
            {
                InitializeComponent();
                ConfigureSuggestionTextBoxes();
                WireUpEventHandlers();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.High,
                    contextData: new Dictionary<string, object>
                    {
                        ["InitializationPhase"] = "Constructor"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(Control_Edit_PartID));
            }
            finally
            {
                Service_DebugTracer.TraceMethodExit(new Dictionary<string, object>
                {
                    ["InitializationSuccess"] = true
                }, nameof(Control_Edit_PartID), nameof(Control_Edit_PartID));
            }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Configures SuggestionTextBoxWithLabel controls with appropriate data providers.
        /// </summary>
        private void ConfigureSuggestionTextBoxes()
        {
            // Configure Part Number field
            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                Control_Edit_PartID_SuggestionBox_Part.TextBox,
                Helper_SuggestionTextBox.GetCachedPartNumbersAsync,
                enableF4: true);

            // Configure Item Type field
            Helper_SuggestionTextBox.ConfigureForItemTypes(
                Control_Edit_PartID_SuggestionBox_ItemType.TextBox,
                Helper_SuggestionTextBox.GetCachedItemTypesAsync,
                enableF4: true);
        }

        /// <summary>
        /// Wires up event handlers for controls.
        /// </summary>
        private void WireUpEventHandlers()
        {
            Control_Edit_PartID_SuggestionBox_Part.SuggestionSelected += Control_Edit_PartID_Part_SuggestionSelected;
            Control_Edit_PartID_SuggestionBox_ItemType.SuggestionSelected += Control_Edit_PartID_ItemType_SuggestionSelected;
            saveButton.Click += SaveButton_Click;
            resetButton.Click += ResetButton_Click;
        }

        /// <summary>
        /// Handles control load event. Sets the issued by label to current user.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                if (issuedByValueLabel != null)
                {
                    issuedByValueLabel.Text = Model_Application_Variables.User ?? "Current User";
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "SetIssuedByLabel",
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(OnLoad));
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles part selection from SuggestionTextBoxWithLabel.
        /// Loads selected part data and enables form controls for editing.
        /// </summary>
        private async void Control_Edit_PartID_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            await LoadSelectedPartAsync(e.SelectedValue);
        }



        /// <summary>
        /// Handles item type selection updates for consistency with AdvancedInventory patterns.
        /// </summary>
        private void Control_Edit_PartID_ItemType_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            // No additional logic needed currently, but method retained to mirror AdvancedInventory pattern
            // and to simplify future enhancements (e.g., save button enablement, validation, logging).
        }

        /// <summary>
        /// Handles save button click. Validates input and updates part in database.
        /// </summary>
        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            var performanceKey = Service_DebugTracer.StartPerformanceTrace("SAVE_PART_OPERATION");

            try
            {
                // Validate that a part is selected
                if (_currentPart == null)
                {

                    return;
                }

                // Validate item type is selected
                if (string.IsNullOrWhiteSpace(Control_Edit_PartID_SuggestionBox_ItemType.Text))
                {
                    Service_ErrorHandler.ShowWarning("Please select a valid item type.");
                    Control_Edit_PartID_SuggestionBox_ItemType.Focus();
                    return;
                }

                // Check for duplicate part number if changed
                if (!await ValidatePartNumberNotDuplicateAsync())
                {
                    return;
                }

                // Update part in database
                await UpdatePartAsync();

                // Show success message
                Service_ErrorHandler.ShowInformation(
                    "Part has been updated successfully!",
                    "Update Complete",
                    controlName: nameof(Control_Edit_PartID));

                // Reset form and trigger refresh
                ResetControlsAndRefreshBindings();
                PartUpdated?.Invoke(this, EventArgs.Empty);


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "SavePart",
                        ["PartID"] = _currentPart?["PartID"]?.ToString() ?? "Unknown",
                        ["ItemType"] = Control_Edit_PartID_SuggestionBox_ItemType.Text ?? string.Empty,
                        ["RequiresColorCode"] = Control_Edit_PartID_CheckBox_RequiresColorCode.Checked,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(SaveButton_Click));
            }
            finally
            {
                Service_DebugTracer.StopPerformanceTrace(performanceKey, new Dictionary<string, object>
                {
                    ["Operation"] = "SavePart"
                });
            }
        }

        /// <summary>
        /// Handles reset button click. Resets control to initial load state (cleared and disabled).
        /// </summary>
        private void ResetButton_Click(object? sender, EventArgs e)
        {
            try
            {
                ClearForm();
                Helper_SuggestionTextBox.Clear(Control_Edit_PartID_SuggestionBox_Part.TextBox);
                SetFormEnabled(false);
                Control_Edit_PartID_SuggestionBox_Part.Focus();


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "ResetControl"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(ResetButton_Click));
            }
        }

        #endregion



        #region Validation

        /// <summary>
        /// Validates that the new part number doesn't already exist (if changed).
        /// </summary>
        /// <returns>True if valid (not duplicate), false if duplicate found</returns>
        private async Task<bool> ValidatePartNumberNotDuplicateAsync()
        {
            if (_currentPart == null)
            {
                return false;
            }

            string? originalPartNumber = _currentPart["PartID"]?.ToString();
            string newPartNumber = itemNumberTextBox.Text.Trim();

            // If part number unchanged, no need to check
            if (originalPartNumber == newPartNumber)
            {
                return true;
            }

            // Check if new part number already exists
            var existsResult = await Dao_Part.PartExistsAsync(newPartNumber);

            if (!existsResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    existsResult.Exception ?? new Exception(existsResult.ErrorMessage),
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "CheckDuplicatePart",
                        ["PartNumber"] = newPartNumber
                    },
                    controlName: nameof(Control_Edit_PartID));
                return false;
            }

            if (existsResult.Data)
            {
                Service_ErrorHandler.ShowWarning(
                    $"Part number '{newPartNumber}' already exists. Please use a different part number.");
                itemNumberTextBox.Focus();
                itemNumberTextBox.SelectAll();


                return false;
            }

            return true;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Loads the currently selected part's data into form controls.
        /// </summary>
        private void LoadPartData()
        {
            if (_currentPart == null)
            {
                return;
            }

            try
            {
                // Load basic part information
                itemNumberTextBox.Text = _currentPart["PartID"]?.ToString() ?? string.Empty;
                Control_Edit_PartID_SuggestionBox_ItemType.Text = _currentPart["ItemType"]?.ToString() ?? string.Empty;
                issuedByValueLabel.Text = _currentPart["IssuedBy"]?.ToString() ?? string.Empty;

                // Load RequiresColorCode checkbox with fallback
                if (_currentPart.Table.Columns.Contains("RequiresColorCode"))
                {
                    var colorCodeValue = _currentPart["RequiresColorCode"];
                    _originalRequiresColorCode = colorCodeValue != DBNull.Value && Convert.ToBoolean(colorCodeValue);
                }
                else
                {
                    _originalRequiresColorCode = false;
                }

                Control_Edit_PartID_CheckBox_RequiresColorCode.Checked = _originalRequiresColorCode;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "LoadPartData",
                        ["PartID"] = _currentPart["PartID"]?.ToString() ?? "Unknown"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(LoadPartData));
            }
        }

        /// <summary>
        /// Loads a part by number, updating form state and enabling editing.
        /// </summary>
        /// <param name="selectedPart">The part number to load.</param>
        /// <param name="showErrorsIfNotFound">Whether to show error messages if part is not found. Default true.</param>
        private async Task LoadSelectedPartAsync(string selectedPart, bool showErrorsIfNotFound = true)
        {
            try
            {


                _currentPart = null;
                SetFormEnabled(false);

                var result = await Dao_Part.GetPartByNumberAsync(selectedPart);

                if (!result.IsSuccess)
                {
                    if (showErrorsIfNotFound)
                    {
                        Service_ErrorHandler.ShowWarning($"Part '{selectedPart}' could not be loaded: {result.ErrorMessage}");
                    }
                    ClearForm();
                    return;
                }

                if (result.Data == null)
                {
                    if (showErrorsIfNotFound)
                    {
                        Service_ErrorHandler.ShowWarning($"Part '{selectedPart}' was not found.");
                    }
                    ClearForm();
                    return;
                }

                _currentPart = result.Data;
                LoadPartData();
                SetFormEnabled(true);


            }
            catch (Exception ex)
            {
                if (showErrorsIfNotFound)
                {
                    Service_ErrorHandler.HandleException(
                        ex,
                        Enum_ErrorSeverity.Medium,
                        contextData: new Dictionary<string, object>
                        {
                            ["SelectedPart"] = selectedPart,
                            ["Operation"] = "LoadPartData",
                            ["User"] = Model_Application_Variables.User ?? "Unknown"
                        },
                        controlName: nameof(Control_Edit_PartID),
                        callerName: nameof(LoadSelectedPartAsync));
                }

                ClearForm();
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Enables or disables form controls for editing.
        /// </summary>
        /// <param name="enabled">True to enable controls, false to disable</param>
        private void SetFormEnabled(bool enabled)
        {
            itemNumberTextBox.Enabled = enabled;
            Control_Edit_PartID_SuggestionBox_ItemType.Enabled = enabled;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Enabled = enabled;
            saveButton.Enabled = enabled;
        }

        /// <summary>
        /// Updates the part in the database with current form values.
        /// Handles RequiresColorCode changes with cache reload.
        /// </summary>
        private async Task UpdatePartAsync()
        {
            if (_currentPart == null)
            {
                throw new InvalidOperationException("Cannot update part: no part is currently selected");
            }

            int id = Convert.ToInt32(_currentPart["ID"]);

            string itemNumber = itemNumberTextBox.Text.Trim();
            string issuedBy = Model_Application_Variables.User ?? "System";
            string type = Control_Edit_PartID_SuggestionBox_ItemType?.Text?.Trim() ?? string.Empty;
            bool requiresColorCode = Control_Edit_PartID_CheckBox_RequiresColorCode.Checked;

            // Update part in database
            var result = await Dao_Part.UpdatePartAsync(id, itemNumber, "", "", issuedBy, type, requiresColorCode);

            if (!result.IsSuccess)
            {
                throw new Exception($"Failed to update part: {result.ErrorMessage}");
            }

            // If RequiresColorCode changed, reload cache
            if (requiresColorCode != _originalRequiresColorCode)
            {

                await Model_Application_Variables.ReloadColorCodePartsAsync();

            }
        }

        /// <summary>
        /// Clears all form fields and resets state.
        /// </summary>
        private void ClearForm()
        {
            itemNumberTextBox.Clear();
            Helper_SuggestionTextBox.Clear(Control_Edit_PartID_SuggestionBox_ItemType.TextBox);
            issuedByValueLabel.Text = string.Empty;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Checked = false;
            _originalRequiresColorCode = false;
            _currentPart = null;
        }

        /// <summary>
        /// Resets form controls and refreshes data after save operation.
        /// </summary>
        private void ResetControlsAndRefreshBindings()
        {
            try
            {
                ClearForm();
                Helper_SuggestionTextBox.Clear(Control_Edit_PartID_SuggestionBox_Part.TextBox);
                SetFormEnabled(false);
                Control_Edit_PartID_SuggestionBox_Part.Focus();


            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "ResetControls"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(ResetControlsAndRefreshBindings));
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Disposes resources used by this control.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    // Unsubscribe from events to prevent memory leaks
                    if (Control_Edit_PartID_SuggestionBox_Part != null)
                    {
                        Control_Edit_PartID_SuggestionBox_Part.SuggestionSelected -= Control_Edit_PartID_Part_SuggestionSelected;
                    }

                    if (Control_Edit_PartID_SuggestionBox_ItemType != null)
                    {
                        Control_Edit_PartID_SuggestionBox_ItemType.SuggestionSelected -= Control_Edit_PartID_ItemType_SuggestionSelected;
                    }

                    if (saveButton != null)
                    {
                        saveButton.Click -= SaveButton_Click;
                    }

                    if (resetButton != null)
                    {
                        resetButton.Click -= ResetButton_Click;
                    }
                }
                catch (Exception ex)
                {
                    // Log but don't throw during disposal
                    LoggingUtility.LogApplicationError(ex);
                }
            }

            base.Dispose(disposing);
        }

        #endregion

        #region PROPOSED_IMPROVEMENTS

        /*
         * ═══════════════════════════════════════════════════════════════════════════════
         * CONSTITUTION COMPLIANCE AUDIT RESULTS
         * ═══════════════════════════════════════════════════════════════════════════════
         * Audit Date: 2025-11-15
         * Compliance Score: 100%
         * Critical Issues Fixed: 0
         * Warnings Fixed: 0
         *
         * This file is FULLY COMPLIANT with MTM Constitution requirements.
         * No mandatory fixes required.
         *
         * ═══════════════════════════════════════════════════════════════════════════════
         * PROPOSED IMPROVEMENTS
         * ═══════════════════════════════════════════════════════════════════════════════
         *
         * Enhancement suggestions have been extracted to dedicated markdown files for
         * better organization and to avoid cluttering the codebase.
         *
         * SUGGESTION FILES:
         * - File-Only: .github/suggestions/Control_Edit_PartID-suggestions.md
         * - System (Performance): .github/suggestions/Helper_SuggestionTextBox-suggestions.md
         * - Speckit: .github/suggestions/speckit/ (multiple feature suggestions)
         *
         * SUGGESTION SUMMARY (Total: 38 suggestions):
         * - File-Only Suggestions: 22 (UI, UX, Validation, Accessibility)
         * - System Suggestions: 8 (Helper methods, Cache management, Error handling)
         * - Multi-File (Speckit) Suggestions: 8 (Architecture, Security, Batch Operations)
         *
         * TO IMPLEMENT:
         * 1. Review suggestion files linked above
         * 2. Prioritize based on Priority (1-10) and Scope (1-10) ratings
         * 3. For Speckit suggestions, run `/speckit.specify` to create feature branch
         * 4. Implement and test incrementally
         *
         * ═══════════════════════════════════════════════════════════════════════════════
         */

        #endregion

    }
}
