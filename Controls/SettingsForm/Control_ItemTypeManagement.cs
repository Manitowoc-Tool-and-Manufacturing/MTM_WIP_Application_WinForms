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
    /// Unified control that manages adding, editing, and removing item types using the new Settings card layout.
    /// </summary>
    public partial class Control_ItemTypeManagement : ThemedUserControl
    {
        #region Fields

        private DataRow? _selectedEditItemType;
        private DataRow? _selectedRemoveItemType;
        private Helper_StoredProcedureProgress? _progressHelper;
        private ToolStripProgressBar? _progressBar;
        private ToolStripStatusLabel? _statusLabel;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the item type management control.
        /// </summary>
        public Control_ItemTypeManagement()
        {
            InitializeComponent();
            InitializeControlText();
            ConfigureInputs();
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
            // Suggestion controls
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.LabelText = "Select Item Type";
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.PlaceholderText = "Search item types (F4)";
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.LabelText = "Select Item Type";
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.PlaceholderText = "Search item types (F4)";
            
            // Buttons
            Control_ItemTypeManagement_Button_AddSave.Text = "Save";
            Control_ItemTypeManagement_Button_AddClear.Text = "Clear";
            Control_ItemTypeManagement_Button_EditSave.Text = "Save Changes";
            Control_ItemTypeManagement_Button_EditReset.Text = "Reset";
            Control_ItemTypeManagement_Button_RemoveConfirm.Text = "Remove";
            Control_ItemTypeManagement_Button_RemoveCancel.Text = "Cancel";
            Control_ItemTypeManagement_Button_Back.Text = "← Back to Selection";
            
            // Labels
            Control_ItemTypeManagement_Label_RemoveWarning.Text = "⚠️ Warning: Removal is permanent and cannot be undone.";
        }

        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            // Store for lazy initialization when control is added to form
            _progressBar = progressBar;
            _statusLabel = statusLabel;
        }

        private Helper_StoredProcedureProgress? GetProgressHelper()
        {
            if (_progressHelper == null && _progressBar != null && _statusLabel != null)
            {
                var form = this.FindForm();
                if (form != null)
                {
                    _progressHelper = Helper_StoredProcedureProgress.Create(_progressBar, _statusLabel, form);
                }
            }
            return _progressHelper;
        }

        private void ConfigureInputs()
        {
            Helper_SuggestionTextBox.ConfigureForItemTypes(
                Control_ItemTypeManagement_Suggestion_EditSelectItemType,
                Helper_SuggestionTextBox.GetCachedItemTypesAsync);

            Helper_SuggestionTextBox.ConfigureForItemTypes(
                Control_ItemTypeManagement_Suggestion_RemoveSelectItemType,
                Helper_SuggestionTextBox.GetCachedItemTypesAsync);
        }

        private void WireUpEventHandlers()
        {
            if (Control_ItemTypeManagement_Suggestion_EditSelectItemType != null)
            {
                Control_ItemTypeManagement_Suggestion_EditSelectItemType.SuggestionSelected += async (_, args) =>
                    await LoadEditItemTypeAsync(args.SelectedValue);
            }

            if (Control_ItemTypeManagement_Suggestion_RemoveSelectItemType != null)
            {
                Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.SuggestionSelected += async (_, args) =>
                    await LoadRemoveItemTypeAsync(args.SelectedValue);
            }

            Control_ItemTypeManagement_Button_AddSave.Click += async (_, _) => await HandleAddItemTypeAsync();
            Control_ItemTypeManagement_Button_AddClear.Click += (_, _) => ClearAddSection();

            Control_ItemTypeManagement_Button_EditSave.Click += async (_, _) => await HandleEditItemTypeSaveAsync();
            Control_ItemTypeManagement_Button_EditReset.Click += (_, _) => ClearEditSection();

            Control_ItemTypeManagement_Button_RemoveConfirm.Click += async (_, _) => await HandleRemoveItemTypeAsync();
            Control_ItemTypeManagement_Button_RemoveCancel.Click += (_, _) => ClearRemoveSection();
        }

        private void WireUpNavigationHandlers()
        {
            // Home tile clicks
            Control_ItemTypeManagement_Panel_HomeTile_Add.Click += (_, _) => ShowCard(0);
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Click += (_, _) => ShowCard(1);
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Click += (_, _) => ShowCard(2);
            
            // Make all child controls of home tiles also clickable
            foreach (Control control in Control_ItemTypeManagement_Panel_HomeTile_Add.Controls)
            {
                WireUpTileControlClick(control, 0);
            }
            foreach (Control control in Control_ItemTypeManagement_Panel_HomeTile_Edit.Controls)
            {
                WireUpTileControlClick(control, 1);
            }
            foreach (Control control in Control_ItemTypeManagement_Panel_HomeTile_Remove.Controls)
            {
                WireUpTileControlClick(control, 2);
            }
            
            // Back button
            Control_ItemTypeManagement_Button_Back.Click += (_, _) => ShowHome();
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
            Control_ItemTypeManagement_Panel_Home.Visible = false;
            Control_ItemTypeManagement_TableLayout_Cards.Visible = true;
            Control_ItemTypeManagement_TableLayout_BackButton.Visible = true;
            
            // Hide all cards first
            Control_ItemTypeManagement_Panel_AddCard.Visible = false;
            Control_ItemTypeManagement_Panel_EditCard.Visible = false;
            Control_ItemTypeManagement_Panel_RemoveCard.Visible = false;
            
            // Show selected card
            switch (cardIndex)
            {
                case 0:
                    Control_ItemTypeManagement_Panel_AddCard.Visible = true;
                    Control_ItemTypeManagement_TextBox_AddItemType.Focus();
                    break;
                case 1:
                    Control_ItemTypeManagement_Panel_EditCard.Visible = true;
                    Control_ItemTypeManagement_Suggestion_EditSelectItemType.Focus();
                    break;
                case 2:
                    Control_ItemTypeManagement_Panel_RemoveCard.Visible = true;
                    Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Focus();
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
            Control_ItemTypeManagement_Panel_Home.Visible = true;
            Control_ItemTypeManagement_TableLayout_Cards.Visible = false;
            Control_ItemTypeManagement_TableLayout_BackButton.Visible = false;
        }

        private void UpdateIssuedByLabels()
        {
            string issuedBy = Model_Application_Variables.User ?? "Current User";
            Control_ItemTypeManagement_Label_AddIssuedByValue.Text = issuedBy;
            Control_ItemTypeManagement_Label_EditIssuedByValue.Text = issuedBy;
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Text = issuedBy;
        }

        private async Task HandleAddItemTypeAsync()
        {
            string itemType = Control_ItemTypeManagement_TextBox_AddItemType.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(itemType))
            {
                ShowWarning("Item type is required before saving.");
                Control_ItemTypeManagement_TextBox_AddItemType.Focus();
                return;
            }

            GetProgressHelper()?.ShowProgress("Checking for existing item type...");

            // Check if item type already exists
            var existsResult = await Dao_ItemType.ItemTypeExists(itemType);

            if (!existsResult.IsSuccess)
            {
                GetProgressHelper()?.ShowError($"Error checking item type existence: {existsResult.ErrorMessage}");
                return;
            }

            if (existsResult.Data)
            {
                GetProgressHelper()?.ShowError($"Item type '{itemType}' already exists.");
                ShowWarning($"Item type '{itemType}' already exists.");
                Control_ItemTypeManagement_TextBox_AddItemType.Focus();
                Control_ItemTypeManagement_TextBox_AddItemType.TextBox.SelectAll();
                return;
            }

            GetProgressHelper()?.UpdateProgress(60, "Creating item type...");

            // Insert new item type
            var insertResult = await Dao_ItemType.InsertItemType(
                itemType,
                Model_Application_Variables.User ?? "Current User");

            if (!insertResult.IsSuccess)
            {
                GetProgressHelper()?.ShowError($"Error creating item type: {insertResult.ErrorMessage}");
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Item type added successfully: ItemType={itemType}");
            GetProgressHelper()?.ShowSuccess($"Item type '{itemType}' created successfully!");
            ShowSuccess($"Item type '{itemType}' added successfully.");
            
            // Refresh caches
            await RefreshItemTypeCachesAsync();
            NotifyItemTypeListChanged();
            RaiseStatusMessage($"Item type '{itemType}' created successfully");
            
            // Clear form
            ClearAddSection();
            Control_ItemTypeManagement_TextBox_AddItemType.Focus();
            
            await Task.Delay(500);
            GetProgressHelper()?.HideProgress();
        }

        private async Task HandleEditItemTypeSaveAsync()
        {
            if (_selectedEditItemType == null)
            {
                ShowWarning("Please select an item type to edit first.");
                return;
            }

            string originalItemType = _selectedEditItemType["ItemType"]?.ToString() ?? string.Empty;
            string newItemType = Control_ItemTypeManagement_TextBox_EditNewItemType.Text?.Trim() ?? string.Empty;
            int itemTypeId = Convert.ToInt32(_selectedEditItemType["ID"]);

            if (string.IsNullOrWhiteSpace(newItemType))
            {
                ShowWarning("Item type is required.");
                Control_ItemTypeManagement_TextBox_EditNewItemType.Focus();
                return;
            }

            // Check for duplicates if name changed
            if (!string.Equals(newItemType, originalItemType, StringComparison.OrdinalIgnoreCase))
            {
                var existsResult = await Dao_ItemType.ItemTypeExists(newItemType);
                if (!existsResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception(existsResult.ErrorMessage ?? "Failed to check if item type exists"),
                        contextData: new Dictionary<string, object>
                        {
                            ["OriginalItemType"] = originalItemType,
                            ["NewItemType"] = newItemType,
                            ["User"] = Model_Application_Variables.User ?? "Unknown"
                        },
                        callerName: nameof(HandleEditItemTypeSaveAsync),
                        controlName: this.Name);
                    return;
                }

                if (existsResult.Data)
                {
                    ShowWarning($"Item type '{newItemType}' already exists.");
                    Control_ItemTypeManagement_TextBox_EditNewItemType.Focus();
                    Control_ItemTypeManagement_TextBox_EditNewItemType.TextBox.SelectAll();
                    return;
                }
            }

            // Update item type
            var updateResult = await Dao_ItemType.UpdateItemType(
                itemTypeId, 
                newItemType,
                Model_Application_Variables.User ?? "Unknown");

            if (!updateResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(updateResult.ErrorMessage ?? "Failed to update item type"),
                    contextData: new Dictionary<string, object>
                    {
                        ["OriginalItemType"] = originalItemType,
                        ["NewItemType"] = newItemType,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleEditItemTypeSaveAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Item type updated successfully: Original={originalItemType}, New={newItemType}");
            ShowSuccess($"Item type '{newItemType}' updated successfully.");
            
            // Refresh caches
            await RefreshItemTypeCachesAsync();
            NotifyItemTypeListChanged();
            RaiseStatusMessage($"Item type updated successfully");
            
            // Clear form
            ClearEditSection();
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Focus();
        }

        private async Task HandleRemoveItemTypeAsync()
        {
            if (_selectedRemoveItemType == null)
            {
                ShowWarning("Please select an item type to remove first.");
                return;
            }

            string itemType = _selectedRemoveItemType["ItemType"]?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(itemType))
            {
                ShowWarning("Invalid item type selection.");
                return;
            }

            // Confirm deletion
            DialogResult confirmation = Service_ErrorHandler.ShowConfirmation(
                $"Are you sure you want to remove item type '{itemType}'?\n\nThis action cannot be undone.",
                "Confirm Item Type Removal");

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            // Delete item type
            var deleteResult = await Dao_ItemType.DeleteItemType(itemType);
            if (!deleteResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(deleteResult.ErrorMessage ?? "Failed to delete item type"),
                    contextData: new Dictionary<string, object>
                    {
                        ["ItemType"] = itemType,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleRemoveItemTypeAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Item type removed successfully: ItemType={itemType}");
            ShowSuccess($"Item type '{itemType}' removed successfully.");
            
            // Refresh caches
            await RefreshItemTypeCachesAsync();
            NotifyItemTypeListChanged();
            RaiseStatusMessage($"Item type removed successfully");
            
            // Clear form
            ClearRemoveSection();
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Focus();
        }

        private async Task LoadEditItemTypeAsync(string itemTypeName)
        {
            if (string.IsNullOrWhiteSpace(itemTypeName))
            {
                SetEditSectionEnabled(false);
                return;
            }

            var result = await Dao_ItemType.GetItemTypeByName(itemTypeName);
            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(result.ErrorMessage ?? "Failed to load item type details"),
                    contextData: new Dictionary<string, object>
                    {
                        ["ItemType"] = itemTypeName,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(LoadEditItemTypeAsync),
                    controlName: this.Name);
                SetEditSectionEnabled(false);
                return;
            }

            _selectedEditItemType = result.Data;
            
            // Populate fields
            string itemType = _selectedEditItemType["ItemType"]?.ToString() ?? string.Empty;
            string issuedBy = _selectedEditItemType["IssuedBy"]?.ToString() ?? "Unknown";

            Control_ItemTypeManagement_TextBox_EditNewItemType.Text = itemType;
            Control_ItemTypeManagement_Label_EditIssuedByValue.Text = issuedBy;

            SetEditSectionEnabled(true);
        }

        private async Task LoadRemoveItemTypeAsync(string itemTypeName)
        {
            if (string.IsNullOrWhiteSpace(itemTypeName))
            {
                SetRemoveSectionEnabled(false);
                return;
            }

            var result = await Dao_ItemType.GetItemTypeByName(itemTypeName);
            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(result.ErrorMessage ?? "Failed to load item type details"),
                    contextData: new Dictionary<string, object>
                    {
                        ["ItemType"] = itemTypeName,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(LoadRemoveItemTypeAsync),
                    controlName: this.Name);
                SetRemoveSectionEnabled(false);
                return;
            }

            _selectedRemoveItemType = result.Data;
            
            // Display details
            string itemType = _selectedRemoveItemType["ItemType"]?.ToString() ?? "N/A";
            string issuedBy = _selectedRemoveItemType["IssuedBy"]?.ToString() ?? "Unknown";

            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Text = itemType;
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Text = issuedBy;

            SetRemoveSectionEnabled(true);
        }

        private void SetEditSectionEnabled(bool enabled)
        {
            Control_ItemTypeManagement_TextBox_EditNewItemType.Enabled = enabled;
            Control_ItemTypeManagement_TextBox_EditNewItemType.TextBox.Enabled = enabled;
            Control_ItemTypeManagement_Button_EditSave.Enabled = enabled;
            Control_ItemTypeManagement_Button_EditReset.Enabled = enabled;
        }

        private void SetRemoveSectionEnabled(bool enabled)
        {
            Control_ItemTypeManagement_Button_RemoveConfirm.Enabled = enabled;
            Control_ItemTypeManagement_Button_RemoveCancel.Enabled = enabled;
        }

        private void ClearAddSection()
        {
            Helper_SuggestionTextBox.Clear(Control_ItemTypeManagement_TextBox_AddItemType.TextBox);
        }

        private void ClearEditSection()
        {
            _selectedEditItemType = null;
            Helper_SuggestionTextBox.Clear(Control_ItemTypeManagement_Suggestion_EditSelectItemType.TextBox);
            Helper_SuggestionTextBox.Clear(Control_ItemTypeManagement_TextBox_EditNewItemType.TextBox);
            Control_ItemTypeManagement_Label_EditIssuedByValue.Text = Model_Application_Variables.User ?? "Current User";
            SetEditSectionEnabled(false);
        }

        private void ClearRemoveSection()
        {
            _selectedRemoveItemType = null;
            Helper_SuggestionTextBox.Clear(Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.TextBox);
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Text = string.Empty;
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Text = Model_Application_Variables.User ?? "Current User";
            SetRemoveSectionEnabled(false);
        }

        private void NotifyItemTypeListChanged()
        {
            ItemTypeListChanged?.Invoke(this, EventArgs.Empty);
        }

        private void RaiseStatusMessage(string message)
        {
            StatusMessageChanged?.Invoke(this, message);
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
            Control_ItemTypeManagement_Panel_HomeTile_Add.Visible = canAdd;
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Visible = canEdit;
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Visible = canRemove;
            
            // Add card
            Control_ItemTypeManagement_Panel_AddCard.Visible = canAdd;
            Control_ItemTypeManagement_TextBox_AddItemType.Enabled = canAdd;
            Control_ItemTypeManagement_Button_AddSave.Enabled = canAdd;
            Control_ItemTypeManagement_Button_AddClear.Enabled = canAdd;

            // Edit card
            Control_ItemTypeManagement_Panel_EditCard.Visible = canEdit;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Enabled = canEdit;
            Control_ItemTypeManagement_TextBox_EditNewItemType.Enabled = canEdit;
            Control_ItemTypeManagement_Button_EditSave.Enabled = canEdit;
            Control_ItemTypeManagement_Button_EditReset.Enabled = canEdit;

            // Remove card
            Control_ItemTypeManagement_Panel_RemoveCard.Visible = canRemove;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Enabled = canRemove;
            Control_ItemTypeManagement_Button_RemoveConfirm.Enabled = canRemove && _selectedRemoveItemType != null;
            Control_ItemTypeManagement_Button_RemoveCancel.Enabled = canRemove;

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
                Control_ItemTypeManagement_Label_Subtitle.Text = "You do not have permissions to manage item types.";
                Control_ItemTypeManagement_Panel_Home.Visible = false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when item type data has changed (add, edit, remove) so parent views can refresh caches.
        /// </summary>
        public event EventHandler? ItemTypeListChanged;

        /// <summary>
        /// Raised to update status message in parent form.
        /// </summary>
        public event EventHandler<string>? StatusMessageChanged;

        #endregion

        #region Helpers

        private static async Task RefreshItemTypeCachesAsync()
        {
            try
            {
                await Helper_SuggestionTextBox.GetCachedItemTypesAsync();
                await Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync(new ComboBox());
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
    }
}
