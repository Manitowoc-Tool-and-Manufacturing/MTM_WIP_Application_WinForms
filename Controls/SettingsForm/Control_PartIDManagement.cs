using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Unified control that manages adding, editing, and removing part numbers using the new Settings card layout.
    /// </summary>
    public partial class Control_PartIDManagement : ThemedUserControl
    {
        #region Fields

        private DataRow? _selectedEditPart;
        private DataRow? _selectedRemovePart;
        private bool _originalEditRequiresColorCode;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the part management control.
        /// </summary>
        public Control_PartIDManagement()
        {
            InitializeComponent();
            InitializeControlText();
            ConfigureSuggestionInputs();
            WireUpEventHandlers();
            WireUpNavigationHandlers();
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateIssuedByLabels();
            SetEditSectionEnabled(false);
            SetRemoveSectionEnabled(false);
        }

        private void InitializeControlText()
        {
            // Add Card
            Control_PartIDManagement_Suggestion_AddPartNumber.LabelText = "Part Number";
            Control_PartIDManagement_Suggestion_AddPartNumber.PlaceholderText = "Enter part number";
            Control_PartIDManagement_Suggestion_AddItemType.LabelText = "Item Type";
            Control_PartIDManagement_Suggestion_AddItemType.PlaceholderText = "Search item types (F4)";
            Control_PartIDManagement_Label_AddIssuedBy.Text = "Issued By:";
            Control_PartIDManagement_Button_AddSave.Text = "Save";
            Control_PartIDManagement_Button_AddClear.Text = "Clear";
            
            // Edit Card
            Control_PartIDManagement_Suggestion_EditSelectPart.LabelText = "Select Part";
            Control_PartIDManagement_Suggestion_EditSelectPart.PlaceholderText = "Search parts (F4)";
            Control_PartIDManagement_Suggestion_EditNewPartNumber.LabelText = "New Part Number";
            Control_PartIDManagement_Suggestion_EditNewPartNumber.PlaceholderText = "Enter new part number";
            Control_PartIDManagement_Suggestion_EditItemType.LabelText = "Item Type";
            Control_PartIDManagement_Suggestion_EditItemType.PlaceholderText = "Search item types (F4)";
            Control_PartIDManagement_Label_EditIssuedBy.Text = "Issued By:";
            Control_PartIDManagement_Button_EditSave.Text = "Save Changes";
            Control_PartIDManagement_Button_EditReset.Text = "Reset";
            
            // Remove Card
            Control_PartIDManagement_Suggestion_RemoveSelectPart.LabelText = "Select Part";
            Control_PartIDManagement_Suggestion_RemoveSelectPart.PlaceholderText = "Search parts (F4)";
            Control_PartIDManagement_Label_RemoveWarning.Text = "⚠️ Warning: Removal is permanent and cannot be undone.";
            Control_PartIDManagement_Button_RemoveConfirm.Text = "Remove";
            Control_PartIDManagement_Button_RemoveCancel.Text = "Cancel";
            
            // Back Button
            Control_PartIDManagement_Button_Back.Text = "← Back to Selection";
        }

        private void ConfigureSuggestionInputs()
        {
            if (Control_PartIDManagement_Suggestion_AddPartNumber != null)
            {
                Control_PartIDManagement_Suggestion_AddPartNumber.EnableSuggestions = false;
                Control_PartIDManagement_Suggestion_AddPartNumber.ShowF4Button = false;
                Control_PartIDManagement_Suggestion_AddPartNumber.ShowValidationColor = false;
                // Use TextBox.Enabled directly since UpdateTextBoxEnabledState requires validator
                Control_PartIDManagement_Suggestion_AddPartNumber.TextBox.Enabled = true;
                Control_PartIDManagement_Suggestion_AddPartNumber.TextBox.BackColor = System.Drawing.SystemColors.Window;
            }

            Helper_SuggestionTextBox.ConfigureForItemTypes(
                Control_PartIDManagement_Suggestion_AddItemType,
                Helper_SuggestionTextBox.GetCachedItemTypesAsync);

            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                Control_PartIDManagement_Suggestion_EditSelectPart,
                Helper_SuggestionTextBox.GetCachedPartNumbersAsync);

            if (Control_PartIDManagement_Suggestion_EditNewPartNumber != null)
            {
                Control_PartIDManagement_Suggestion_EditNewPartNumber.EnableSuggestions = false;
                Control_PartIDManagement_Suggestion_EditNewPartNumber.ShowF4Button = false;
                Control_PartIDManagement_Suggestion_EditNewPartNumber.ShowValidationColor = false;
                // Use TextBox.Enabled directly since UpdateTextBoxEnabledState requires validator
                Control_PartIDManagement_Suggestion_EditNewPartNumber.TextBox.Enabled = true;
                Control_PartIDManagement_Suggestion_EditNewPartNumber.TextBox.BackColor = System.Drawing.SystemColors.Window;
            }

            Helper_SuggestionTextBox.ConfigureForItemTypes(
                Control_PartIDManagement_Suggestion_EditItemType,
                Helper_SuggestionTextBox.GetCachedItemTypesAsync);

            Helper_SuggestionTextBox.ConfigureForPartNumbers(
                Control_PartIDManagement_Suggestion_RemoveSelectPart,
                Helper_SuggestionTextBox.GetCachedPartNumbersAsync);
        }

        private void WireUpEventHandlers()
        {
            if (Control_PartIDManagement_Suggestion_EditSelectPart != null)
            {
                Control_PartIDManagement_Suggestion_EditSelectPart.SuggestionSelected += async (_, args) =>
                    await LoadEditPartAsync(args.SelectedValue);
            }

            if (Control_PartIDManagement_Suggestion_RemoveSelectPart != null)
            {
                Control_PartIDManagement_Suggestion_RemoveSelectPart.SuggestionSelected += async (_, args) =>
                    await LoadRemovePartAsync(args.SelectedValue);
            }

            Control_PartIDManagement_Button_AddSave.Click += async (_, _) => await HandleAddPartAsync();
            Control_PartIDManagement_Button_AddClear.Click += (_, _) => ClearAddSection();

            Control_PartIDManagement_Button_EditSave.Click += async (_, _) => await HandleEditPartSaveAsync();
            Control_PartIDManagement_Button_EditReset.Click += (_, _) => ClearEditSection();

            Control_PartIDManagement_Button_RemoveConfirm.Click += async (_, _) => await HandleRemovePartAsync();
            Control_PartIDManagement_Button_RemoveCancel.Click += (_, _) => ClearRemoveSection();
        }

        private void WireUpNavigationHandlers()
        {
            // Home tile clicks
            Control_PartIDManagement_Panel_HomeTile_Add.Click += (_, _) => ShowCard(0);
            Control_PartIDManagement_Panel_HomeTile_Edit.Click += (_, _) => ShowCard(1);
            Control_PartIDManagement_Panel_HomeTile_Remove.Click += (_, _) => ShowCard(2);

            // Make all child controls of home tiles also clickable
            foreach (Control control in Control_PartIDManagement_Panel_HomeTile_Add.Controls)
            {
                WireUpTileControlClick(control, 0);
            }
            foreach (Control control in Control_PartIDManagement_Panel_HomeTile_Edit.Controls)
            {
                WireUpTileControlClick(control, 1);
            }
            foreach (Control control in Control_PartIDManagement_Panel_HomeTile_Remove.Controls)
            {
                WireUpTileControlClick(control, 2);
            }

            // Back button
            Control_PartIDManagement_Button_Back.Click += (_, _) => ShowHome();
        }

        private void WireUpTileControlClick(Control control, int cardIndex)
        {
            control.Click += (_, _) => ShowCard(cardIndex);
            control.Cursor = Cursors.Hand;

            // Recursively wire up child controls
            foreach (Control child in control.Controls)
            {
                WireUpTileControlClick(child, cardIndex);
            }
        }

        private void ShowCard(int cardIndex)
        {
            // Hide home, show cards container and back button
            Control_PartIDManagement_Panel_Home.Visible = false;
            Control_PartIDManagement_TableLayout_Cards.Visible = true;
            Control_PartIDManagement_TableLayoutPanel_BackButton.Visible = true;

            // Hide all cards first
            Control_PartIDManagement_Panel_AddCard.Visible = false;
            Control_PartIDManagement_Panel_EditCard.Visible = false;
            Control_PartIDManagement_Panel_RemoveCard.Visible = false;

            // Show selected card
            switch (cardIndex)
            {
                case 0:
                    Control_PartIDManagement_Panel_AddCard.Visible = true;
                    Control_PartIDManagement_Suggestion_AddPartNumber.Focus();
                    break;
                case 1:
                    Control_PartIDManagement_Panel_EditCard.Visible = true;
                    Control_PartIDManagement_Suggestion_EditSelectPart.Focus();
                    break;
                case 2:
                    Control_PartIDManagement_Panel_RemoveCard.Visible = true;
                    Control_PartIDManagement_Suggestion_RemoveSelectPart.Focus();
                    break;
            }
        }

        private void ShowHome()
        {
            // Clear all sections
            ClearAddSection();
            ClearEditSection();
            ClearRemoveSection();

            // Show home, hide cards and back button
            Control_PartIDManagement_Panel_Home.Visible = true;
            Control_PartIDManagement_TableLayout_Cards.Visible = false;
            Control_PartIDManagement_TableLayoutPanel_BackButton.Visible = false;
        }

        private void UpdateIssuedByLabels()
        {
            string issuedBy = Model_Application_Variables.User ?? "Current User";
            Control_PartIDManagement_Label_AddIssuedByValue.Text = issuedBy;
            Control_PartIDManagement_Label_EditIssuedByValue.Text = issuedBy;
            Control_PartIDManagement_Label_RemoveIssuedByValue.Text = issuedBy;
        }

        private async Task HandleAddPartAsync()
        {
            string partNumber = Control_PartIDManagement_Suggestion_AddPartNumber.Text?.Trim() ?? string.Empty;
            string itemType = Control_PartIDManagement_Suggestion_AddItemType.Text?.Trim() ?? string.Empty;
            bool requiresColor = Control_PartIDManagement_CheckBox_AddRequiresColorCode.Checked;

            if (string.IsNullOrWhiteSpace(partNumber))
            {
                ShowWarning("Part number is required before saving.");
                Control_PartIDManagement_Suggestion_AddPartNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(itemType))
            {
                ShowWarning("Item type is required before saving.");
                Control_PartIDManagement_Suggestion_AddItemType.Focus();
                return;
            }

            try
            {
                var existsResult = await Dao_Part.PartExistsAsync(partNumber);
                if (!existsResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        existsResult.Exception ?? new Exception(existsResult.ErrorMessage),
                        controlName: nameof(Control_PartIDManagement),
                        callerName: nameof(HandleAddPartAsync));
                    return;
                }

                if (existsResult.Data)
                {
                    ShowWarning($"Part '{partNumber}' already exists. Use Edit instead.");
                    Control_PartIDManagement_Suggestion_AddPartNumber.Focus();
                    Control_PartIDManagement_Suggestion_AddPartNumber.Text = partNumber;
                    return;
                }

                var addResult = await Dao_Part.CreatePartAsync(
                    partNumber,
                    string.Empty,
                    string.Empty,
                    Model_Application_Variables.User ?? "System",
                    itemType,
                    requiresColor);

                if (!addResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        addResult.Exception ?? new Exception(addResult.ErrorMessage),
                        controlName: nameof(Control_PartIDManagement),
                        callerName: nameof(HandleAddPartAsync));
                    return;
                }

                ShowSuccess("Part added successfully.");
                if (requiresColor)
                {
                    await Model_Application_Variables.ReloadColorCodePartsAsync();
                }

                await RefreshPartCachesAsync();
                ClearAddSection();
                NotifyPartListChanged();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    controlName: nameof(Control_PartIDManagement),
                    callerName: nameof(HandleAddPartAsync));
            }
        }

        private async Task HandleEditPartSaveAsync()
        {
            if (_selectedEditPart == null)
            {
                ShowWarning("Select a part to edit before saving changes.");
                return;
            }

            string newPartNumber = Control_PartIDManagement_Suggestion_EditNewPartNumber.Text?.Trim() ?? string.Empty;
            string newItemType = Control_PartIDManagement_Suggestion_EditItemType.Text?.Trim() ?? string.Empty;
            bool requiresColor = Control_PartIDManagement_CheckBox_EditRequiresColorCode.Checked;

            if (string.IsNullOrWhiteSpace(newPartNumber))
            {
                ShowWarning("New part number is required.");
                Control_PartIDManagement_Suggestion_EditNewPartNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newItemType))
            {
                ShowWarning("Item type is required.");
                Control_PartIDManagement_Suggestion_EditItemType.Focus();
                return;
            }

            try
            {
                string currentPartNumber = _selectedEditPart["PartID"]?.ToString() ?? string.Empty;
                if (!string.Equals(currentPartNumber, newPartNumber, StringComparison.OrdinalIgnoreCase))
                {
                    var existsResult = await Dao_Part.PartExistsAsync(newPartNumber);
                    if (!existsResult.IsSuccess)
                    {
                        Service_ErrorHandler.HandleDatabaseError(
                            existsResult.Exception ?? new Exception(existsResult.ErrorMessage),
                            controlName: nameof(Control_PartIDManagement),
                            callerName: nameof(HandleEditPartSaveAsync));
                        return;
                    }

                    if (existsResult.Data)
                    {
                        ShowWarning($"Part '{newPartNumber}' already exists. Choose another number.");
                        Control_PartIDManagement_Suggestion_EditNewPartNumber.Focus();
                        Control_PartIDManagement_Suggestion_EditNewPartNumber.Text = newPartNumber;
                        return;
                    }
                }

                if (!_selectedEditPart.Table.Columns.Contains("ID"))
                {
                    ShowWarning("Unable to update part because the selected item is missing an identifier.");
                    return;
                }

                int partId = Convert.ToInt32(_selectedEditPart["ID"]);
                var updateResult = await Dao_Part.UpdatePartAsync(
                    partId,
                    newPartNumber,
                    string.Empty,
                    string.Empty,
                    Model_Application_Variables.User ?? "System",
                    newItemType,
                    requiresColor);

                if (!updateResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        updateResult.Exception ?? new Exception(updateResult.ErrorMessage),
                        controlName: nameof(Control_PartIDManagement),
                        callerName: nameof(HandleEditPartSaveAsync));
                    return;
                }

                if (requiresColor != _originalEditRequiresColorCode)
                {
                    await Model_Application_Variables.ReloadColorCodePartsAsync();
                }

                ShowSuccess("Part updated successfully.");
                await RefreshPartCachesAsync();
                ClearEditSection();
                NotifyPartListChanged();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    controlName: nameof(Control_PartIDManagement),
                    callerName: nameof(HandleEditPartSaveAsync));
            }
        }

        private async Task HandleRemovePartAsync()
        {
            if (_selectedRemovePart == null)
            {
                ShowWarning("Select a part to remove before continuing.");
                return;
            }

            string partNumber = _selectedRemovePart["PartID"]?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(partNumber))
            {
                ShowWarning("The selected part is missing a part number.");
                return;
            }

            DialogResult confirmation = Service_ErrorHandler.ShowConfirmation(
                $"Are you sure you want to remove part '{partNumber}'?\n\nThis action cannot be undone.",
                "Confirm Part Removal");

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            try
            {
                var deleteResult = await Dao_Part.DeletePartAsync(partNumber);
                if (!deleteResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        deleteResult.Exception ?? new Exception(deleteResult.ErrorMessage),
                        controlName: nameof(Control_PartIDManagement),
                        callerName: nameof(HandleRemovePartAsync));
                    return;
                }

                ShowSuccess("Part removed successfully.");
                await RefreshPartCachesAsync();
                ClearRemoveSection();
                NotifyPartListChanged();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.High,
                    controlName: nameof(Control_PartIDManagement),
                    callerName: nameof(HandleRemovePartAsync));
            }
        }

        private async Task LoadEditPartAsync(string partNumber)
        {
            if (string.IsNullOrWhiteSpace(partNumber))
            {
                ShowWarning("Enter or select a part number to edit.");
                return;
            }

            // Optimization: Don't reload if already selected
            if (_selectedEditPart != null)
            {
                string currentPartNumber = _selectedEditPart["PartID"]?.ToString() ?? string.Empty;
                if (string.Equals(currentPartNumber, partNumber, StringComparison.OrdinalIgnoreCase))
                {
                    // Ensure section is enabled
                    SetEditSectionEnabled(true);
                    // Move focus to next control manually since we skipped the load which usually handles state
                    Control_PartIDManagement_Suggestion_EditNewPartNumber.Focus();
                    return;
                }
            }

            try
            {
                var result = await Dao_Part.GetPartByNumberAsync(partNumber);
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        result.Exception ?? new Exception(result.ErrorMessage),
                        controlName: nameof(Control_PartIDManagement),
                        callerName: nameof(LoadEditPartAsync));
                    return;
                }

                _selectedEditPart = result.Data;
                if (_selectedEditPart == null)
                {
                    ShowWarning($"Part '{partNumber}' could not be found.");
                    ClearEditSection();
                    return;
                }

                Control_PartIDManagement_Suggestion_EditNewPartNumber.Text = _selectedEditPart["PartID"]?.ToString() ?? string.Empty;
                Control_PartIDManagement_Suggestion_EditItemType.Text = _selectedEditPart["ItemType"]?.ToString() ?? string.Empty;
                Control_PartIDManagement_Label_EditIssuedByValue.Text = _selectedEditPart["IssuedBy"]?.ToString() ?? string.Empty;

                _originalEditRequiresColorCode = _selectedEditPart.Table.Columns.Contains("RequiresColorCode") &&
                                                   _selectedEditPart["RequiresColorCode"] != DBNull.Value &&
                                                   Convert.ToBoolean(_selectedEditPart["RequiresColorCode"]);
                Control_PartIDManagement_CheckBox_EditRequiresColorCode.Checked = _originalEditRequiresColorCode;

                SetEditSectionEnabled(true);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    controlName: nameof(Control_PartIDManagement),
                    callerName: nameof(LoadEditPartAsync));
                ClearEditSection();
            }
        }

        private async Task LoadRemovePartAsync(string partNumber)
        {
            if (string.IsNullOrWhiteSpace(partNumber))
            {
                ShowWarning("Enter or select a part number to remove.");
                return;
            }

            try
            {
                SetRemoveSectionEnabled(false);
                var result = await Dao_Part.GetPartByNumberAsync(partNumber);
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        result.Exception ?? new Exception(result.ErrorMessage),
                        controlName: nameof(Control_PartIDManagement),
                        callerName: nameof(LoadRemovePartAsync));
                    return;
                }

                _selectedRemovePart = result.Data;
                if (_selectedRemovePart == null)
                {
                    ShowWarning($"Part '{partNumber}' could not be found.");
                    ClearRemoveSection();
                    return;
                }

                Control_PartIDManagement_Label_RemoveItemNumberValue.Text = _selectedRemovePart["PartID"]?.ToString() ?? string.Empty;
                Control_PartIDManagement_Label_RemoveCustomerValue.Text = _selectedRemovePart["Customer"]?.ToString() ?? string.Empty;
                Control_PartIDManagement_Label_RemoveDescriptionValue.Text = _selectedRemovePart["Description"]?.ToString() ?? string.Empty;
                Control_PartIDManagement_Label_RemoveTypeValue.Text = _selectedRemovePart["ItemType"]?.ToString() ?? string.Empty;
                Control_PartIDManagement_Label_RemoveIssuedByValue.Text = _selectedRemovePart["IssuedBy"]?.ToString() ?? string.Empty;

                SetRemoveSectionEnabled(true);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    controlName: nameof(Control_PartIDManagement),
                    callerName: nameof(LoadRemovePartAsync));
                ClearRemoveSection();
            }
        }

        private void SetEditSectionEnabled(bool enabled)
        {
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Enabled = enabled;
            Control_PartIDManagement_Suggestion_EditItemType.Enabled = enabled;
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Enabled = enabled;
            Control_PartIDManagement_Button_EditSave.Enabled = enabled;
            Control_PartIDManagement_Button_EditReset.Enabled = enabled;
        }

        private void SetRemoveSectionEnabled(bool enabled)
        {
            Control_PartIDManagement_TableLayout_RemoveDetails.Visible = enabled;
            Control_PartIDManagement_Label_RemoveWarning.Visible = enabled;
            Control_PartIDManagement_Button_RemoveConfirm.Enabled = enabled;
            Control_PartIDManagement_Button_RemoveCancel.Enabled = true;
        }

        private void ClearAddSection()
        {
            Control_PartIDManagement_Suggestion_AddPartNumber.Text = string.Empty;
            Helper_SuggestionTextBox.Clear(Control_PartIDManagement_Suggestion_AddItemType.TextBox);
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Checked = false;
            Control_PartIDManagement_Suggestion_AddPartNumber.Focus();
        }

        private void ClearEditSection()
        {
            Helper_SuggestionTextBox.Clear(Control_PartIDManagement_Suggestion_EditSelectPart.TextBox);
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Text = string.Empty;
            Helper_SuggestionTextBox.Clear(Control_PartIDManagement_Suggestion_EditItemType.TextBox);
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Checked = false;
            Control_PartIDManagement_Label_EditIssuedByValue.Text = Model_Application_Variables.User ?? string.Empty;
            _selectedEditPart = null;
            _originalEditRequiresColorCode = false;
            SetEditSectionEnabled(false);
        }

        private void ClearRemoveSection()
        {
            Helper_SuggestionTextBox.Clear(Control_PartIDManagement_Suggestion_RemoveSelectPart.TextBox);
            Control_PartIDManagement_Label_RemoveItemNumberValue.Text = string.Empty;
            Control_PartIDManagement_Label_RemoveCustomerValue.Text = string.Empty;
            Control_PartIDManagement_Label_RemoveDescriptionValue.Text = string.Empty;
            Control_PartIDManagement_Label_RemoveTypeValue.Text = string.Empty;
            Control_PartIDManagement_Label_RemoveIssuedByValue.Text = Model_Application_Variables.User ?? string.Empty;
            _selectedRemovePart = null;
            SetRemoveSectionEnabled(false);
        }

        private void NotifyPartListChanged()
        {
            PartListChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Applies privilege-based visibility and enabled states for each card.
        /// </summary>
        /// <param name="canAdd">If true, add card remains visible and interactive.</param>
        /// <param name="canEdit">If true, edit card remains visible and interactive.</param>
        /// <param name="canRemove">If true, remove card remains visible and interactive.</param>
        public void ApplyPrivileges(bool canAdd, bool canEdit, bool canRemove)
        {
            // Home tiles
            Control_PartIDManagement_Panel_HomeTile_Add.Visible = canAdd;
            Control_PartIDManagement_Panel_HomeTile_Edit.Visible = canEdit;
            Control_PartIDManagement_Panel_HomeTile_Remove.Visible = canRemove;

            // Add card
            Control_PartIDManagement_Panel_AddCard.Visible = canAdd;
            Control_PartIDManagement_Suggestion_AddPartNumber.Enabled = canAdd;
            Control_PartIDManagement_Suggestion_AddItemType.Enabled = canAdd;
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Enabled = canAdd;
            Control_PartIDManagement_Button_AddSave.Enabled = canAdd;
            Control_PartIDManagement_Button_AddClear.Enabled = canAdd;

            // Edit card
            Control_PartIDManagement_Panel_EditCard.Visible = canEdit;
            Control_PartIDManagement_Suggestion_EditSelectPart.Enabled = canEdit;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Enabled = canEdit;
            Control_PartIDManagement_Suggestion_EditItemType.Enabled = canEdit;
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Enabled = canEdit;
            Control_PartIDManagement_Button_EditSave.Enabled = canEdit;
            Control_PartIDManagement_Button_EditReset.Enabled = canEdit;

            // Remove card
            Control_PartIDManagement_Panel_RemoveCard.Visible = canRemove;
            Control_PartIDManagement_Suggestion_RemoveSelectPart.Enabled = canRemove;
            Control_PartIDManagement_Button_RemoveConfirm.Enabled = canRemove && _selectedRemovePart != null;
            Control_PartIDManagement_Button_RemoveCancel.Enabled = canRemove;

            if (!canEdit)
            {
                ClearEditSection();
            }

            if (!canRemove)
            {
                ClearRemoveSection();
            }

            // If user has no privileges at all, show message
            if (!canAdd && !canEdit && !canRemove)
            {
                Control_PartIDManagement_Label_Subtitle.Text = "You do not have permissions to manage part numbers.";
                Control_PartIDManagement_Panel_Home.Visible = false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when part data has changed (add, edit, remove) so parent views can refresh caches.
        /// </summary>
        public event EventHandler? PartListChanged;

        #endregion

        #region Helpers

        private static async Task RefreshPartCachesAsync()
        {
            try
            {
                await Task.WhenAll(
                    Helper_UI_ComboBoxes.SetupPartDataTable(),
                    Helper_UI_SuggestionBoxes.LoadPartIdsAsync());
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static void ShowWarning(string message)
        {
            Service_ErrorHandler.ShowWarning(
                message,
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private static void ShowSuccess(string message)
        {
            Service_ErrorHandler.ShowInformation(
                message,
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Cleanup / Dispose

        #endregion


    }
}
