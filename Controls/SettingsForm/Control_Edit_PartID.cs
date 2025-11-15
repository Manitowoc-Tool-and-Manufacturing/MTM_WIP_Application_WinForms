using System.Data;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
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
    public partial class Control_Edit_PartID : UserControl
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

                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Control initialized successfully");
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
        /// Configures SuggestionTextBox controls with appropriate data providers and F4 key support.
        /// </summary>
        private void ConfigureSuggestionTextBoxes()
        {
            // Configure Part Number field
            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                Control_Edit_PartID_TextBox_Part,
                GetPartNumberSuggestionsAsync,
                enableF4: true);

            // Configure Item Type field
            Helper_SuggestionTextBox.ConfigureForItemTypes(
                Control_Edit_PartID_TextBox_ItemType,
                GetItemTypeSuggestionsAsync,
                enableF4: true);
        }

        /// <summary>
        /// Wires up event handlers for controls.
        /// </summary>
        private void WireUpEventHandlers()
        {
            Control_Edit_PartID_TextBox_Part.SuggestionSelected += Control_Edit_PartID_TextBox_Part_SuggestionSelected;
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
                        ["Operation"] = "SetIssuedByLabel"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(OnLoad));
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles part selection from SuggestionTextBox.
        /// Loads selected part data and enables form controls for editing.
        /// </summary>
        private async void Control_Edit_PartID_TextBox_Part_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
        {
            string selectedPart = e.SelectedValue;

            try
            {
                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Part selected: {selectedPart}");

                // Clear current part before loading new one
                _currentPart = null;
                SetFormEnabled(false);

                // Load part from database
                var result = await Dao_Part.GetPartByNumberAsync(selectedPart);

                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowWarning($"Part '{selectedPart}' could not be loaded: {result.ErrorMessage}");
                    ClearForm();
                    return;
                }

                if (result.Data == null)
                {
                    Service_ErrorHandler.ShowWarning($"Part '{selectedPart}' was not found.");
                    ClearForm();
                    return;
                }

                // Load part data into form
                _currentPart = result.Data;
                LoadPartData();
                SetFormEnabled(true);

                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Part '{selectedPart}' loaded successfully");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["SelectedPart"] = selectedPart,
                        ["Operation"] = "LoadPartData"
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(Control_Edit_PartID_TextBox_Part_SuggestionSelected));

                ClearForm();
            }
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
                    LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Save attempted with no part selected");
                    return;
                }

                // Validate item type is selected
                if (string.IsNullOrWhiteSpace(Control_Edit_PartID_TextBox_ItemType.Text))
                {
                    Service_ErrorHandler.ShowWarning("Please select a valid item type.");
                    Control_Edit_PartID_TextBox_ItemType.Focus();
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

                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Part updated successfully");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "SavePart",
                        ["PartID"] = _currentPart?["PartID"]?.ToString() ?? "Unknown"
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
                Control_Edit_PartID_TextBox_Part.Clear();
                SetFormEnabled(false);
                Control_Edit_PartID_TextBox_Part.Focus();

                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Control reset to initial state");
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

        #region Data Providers

        /// <summary>
        /// Data provider for part number SuggestionTextBox.
        /// Returns list of all part IDs from database.
        /// </summary>
        private async Task<List<string>> GetPartNumberSuggestionsAsync()
        {
            return await GetSuggestionsFromDatabaseAsync(
                "md_part_ids_Get_All",
                "PartID",
                "part numbers");
        }

        /// <summary>
        /// Data provider for item type SuggestionTextBox.
        /// Returns list of all item types from database.
        /// </summary>
        private async Task<List<string>> GetItemTypeSuggestionsAsync()
        {
            return await GetSuggestionsFromDatabaseAsync(
                "md_item_types_Get_All",
                "ItemType",
                "item types");
        }

        /// <summary>
        /// Generic method to load suggestions from database stored procedure.
        /// </summary>
        /// <param name="procedureName">Stored procedure name</param>
        /// <param name="columnName">Column name to extract</param>
        /// <param name="dataTypeName">Friendly name for error messages</param>
        /// <returns>List of suggestion strings</returns>
        private async Task<List<string>> GetSuggestionsFromDatabaseAsync(string procedureName, string columnName, string dataTypeName)
        {
            try
            {
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    procedureName,
                    null);

                if (!result.IsSuccess || result.Data == null)
                {
                    Service_ErrorHandler.ShowWarning(result.ErrorMessage ?? $"Failed to load {dataTypeName}");
                    return new List<string>();
                }

                var suggestions = new List<string>(result.Data.Rows.Count);
                foreach (DataRow row in result.Data.Rows)
                {
                    var value = row[columnName];
                    if (value != null && value != DBNull.Value)
                    {
                        suggestions.Add(value.ToString() ?? string.Empty);
                    }
                }

                return suggestions;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Procedure"] = procedureName,
                        ["DataType"] = dataTypeName
                    },
                    controlName: nameof(Control_Edit_PartID),
                    callerName: nameof(GetSuggestionsFromDatabaseAsync));
                return new List<string>();
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
                Control_Edit_PartID_TextBox_ItemType.Text = _currentPart["ItemType"]?.ToString() ?? string.Empty;
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

        #endregion

        #region Helpers

        /// <summary>
        /// Enables or disables form controls for editing.
        /// </summary>
        /// <param name="enabled">True to enable controls, false to disable</param>
        private void SetFormEnabled(bool enabled)
        {
            itemNumberTextBox.Enabled = enabled;
            Control_Edit_PartID_TextBox_ItemType.Enabled = enabled;
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
            string type = Control_Edit_PartID_TextBox_ItemType.Text.Trim();
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
                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] RequiresColorCode changed for part {itemNumber}: {_originalRequiresColorCode} -> {requiresColorCode}");
                await Model_Application_Variables.ReloadColorCodePartsAsync();
                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] ColorCodeParts cache reloaded: {Model_Application_Variables.ColorCodeParts.Count} parts flagged");
            }
        }

        /// <summary>
        /// Clears all form fields and resets state.
        /// </summary>
        private void ClearForm()
        {
            itemNumberTextBox.Clear();
            Control_Edit_PartID_TextBox_ItemType.Clear();
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
                Control_Edit_PartID_TextBox_Part.Clear();
                SetFormEnabled(false);
                Control_Edit_PartID_TextBox_Part.Focus();

                LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Controls reset after save");
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
                    if (Control_Edit_PartID_TextBox_Part != null)
                    {
                        Control_Edit_PartID_TextBox_Part.SuggestionSelected -= Control_Edit_PartID_TextBox_Part_SuggestionSelected;
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
    }
}
