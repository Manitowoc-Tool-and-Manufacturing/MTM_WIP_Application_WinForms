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
    /// Unified control that manages adding, editing, and removing operations using the new Settings card layout.
    /// </summary>
    public partial class Control_OperationManagement : ThemedUserControl
    {
        #region Fields

        private DataRow? _selectedEditOperation;
        private DataRow? _selectedRemoveOperation;
        private Helper_StoredProcedureProgress? _progressHelper;
        private ToolStripProgressBar? _progressBar;
        private ToolStripStatusLabel? _statusLabel;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the operation management control.
        /// </summary>
        public Control_OperationManagement()
        {
            InitializeComponent();
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
            Helper_SuggestionTextBox.ConfigureForOperations(
                Control_OperationManagement_Suggestion_EditSelectOperation,
                Helper_SuggestionTextBox.GetCachedOperationsAsync);

            Helper_SuggestionTextBox.ConfigureForOperations(
                Control_OperationManagement_Suggestion_RemoveSelectOperation,
                Helper_SuggestionTextBox.GetCachedOperationsAsync);
        }

        private void WireUpEventHandlers()
        {
            if (Control_OperationManagement_Suggestion_EditSelectOperation != null)
            {
                Control_OperationManagement_Suggestion_EditSelectOperation.SuggestionSelected += async (_, args) =>
                    await LoadEditOperationAsync(args.SelectedValue);
            }

            if (Control_OperationManagement_Suggestion_RemoveSelectOperation != null)
            {
                Control_OperationManagement_Suggestion_RemoveSelectOperation.SuggestionSelected += async (_, args) =>
                    await LoadRemoveOperationAsync(args.SelectedValue);
            }

            Control_OperationManagement_Button_AddSave.Click += async (_, _) => await HandleAddOperationAsync();
            Control_OperationManagement_Button_AddClear.Click += (_, _) => ClearAddSection();

            Control_OperationManagement_Button_EditSave.Click += async (_, _) => await HandleEditOperationSaveAsync();
            Control_OperationManagement_Button_EditReset.Click += (_, _) => ClearEditSection();

            Control_OperationManagement_Button_RemoveConfirm.Click += async (_, _) => await HandleRemoveOperationAsync();
            Control_OperationManagement_Button_RemoveCancel.Click += (_, _) => ClearRemoveSection();
        }

        private void WireUpNavigationHandlers()
        {
            // Home tile clicks
            Control_OperationManagement_Panel_HomeTile_Add.Click += (_, _) => ShowCard(0);
            Control_OperationManagement_Panel_HomeTile_Edit.Click += (_, _) => ShowCard(1);
            Control_OperationManagement_Panel_HomeTile_Remove.Click += (_, _) => ShowCard(2);
            
            // Make all child controls of home tiles also clickable
            foreach (Control control in Control_OperationManagement_Panel_HomeTile_Add.Controls)
            {
                WireUpTileControlClick(control, 0);
            }
            foreach (Control control in Control_OperationManagement_Panel_HomeTile_Edit.Controls)
            {
                WireUpTileControlClick(control, 1);
            }
            foreach (Control control in Control_OperationManagement_Panel_HomeTile_Remove.Controls)
            {
                WireUpTileControlClick(control, 2);
            }
            
            // Back button
            Control_OperationManagement_Button_Back.Click += (_, _) => ShowHome();
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
            Control_OperationManagement_Panel_Home.Visible = false;
            Control_OperationManagement_TableLayout_Cards.Visible = true;
            Control_OperationManagement_FlowPanel_BackButton.Visible = true;
            
            // Hide all cards first
            Control_OperationManagement_Panel_AddCard.Visible = false;
            Control_OperationManagement_Panel_EditCard.Visible = false;
            Control_OperationManagement_Panel_RemoveCard.Visible = false;
            
            // Show selected card
            switch (cardIndex)
            {
                case 0:
                    Control_OperationManagement_Panel_AddCard.Visible = true;
                    Control_OperationManagement_TextBox_AddOperation.Focus();
                    break;
                case 1:
                    Control_OperationManagement_Panel_EditCard.Visible = true;
                    Control_OperationManagement_Suggestion_EditSelectOperation.Focus();
                    break;
                case 2:
                    Control_OperationManagement_Panel_RemoveCard.Visible = true;
                    Control_OperationManagement_Suggestion_RemoveSelectOperation.Focus();
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
            Control_OperationManagement_Panel_Home.Visible = true;
            Control_OperationManagement_TableLayout_Cards.Visible = false;
            Control_OperationManagement_FlowPanel_BackButton.Visible = false;
        }

        private void UpdateIssuedByLabels()
        {
            string issuedBy = Model_Application_Variables.User ?? "Current User";
            Control_OperationManagement_Label_AddIssuedByValue.Text = issuedBy;
            Control_OperationManagement_Label_EditIssuedByValue.Text = issuedBy;
            Control_OperationManagement_Label_RemoveIssuedByValue.Text = issuedBy;
        }

        private async Task HandleAddOperationAsync()
        {
            string operation = Control_OperationManagement_TextBox_AddOperation.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(operation))
            {
                ShowWarning("Operation number is required before saving.");
                Control_OperationManagement_TextBox_AddOperation.Focus();
                return;
            }

            GetProgressHelper()?.ShowProgress("Checking for existing operation...");

            // Check if operation already exists
            var existsResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Exists_ByOperation",
                new Dictionary<string, object> { ["Operation"] = operation },
                _progressHelper
            );

            if (!existsResult.IsSuccess)
            {
                GetProgressHelper()?.ShowError($"Error checking operation existence: {existsResult.ErrorMessage}");
                return;
            }

            if (existsResult.Data != null && existsResult.Data.Rows.Count > 0)
            {
                int operationExists = Convert.ToInt32(existsResult.Data.Rows[0]["OperationExists"]);
                if (operationExists > 0)
                {
                    GetProgressHelper()?.ShowError($"Operation '{operation}' already exists.");
                    ShowWarning($"Operation '{operation}' already exists.");
                    Control_OperationManagement_TextBox_AddOperation.Focus();
                    Control_OperationManagement_TextBox_AddOperation.TextBox.SelectAll();
                    return;
                }
            }

            GetProgressHelper()?.UpdateProgress(60, "Creating operation...");

            // Insert new operation
            var insertResult = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_operation_numbers_Add_Operation",
                new Dictionary<string, object>
                {
                    ["Operation"] = operation,
                    ["IssuedBy"] = Model_Application_Variables.User ?? "Current User"
                },
                _progressHelper
            );

            if (!insertResult.IsSuccess)
            {
                GetProgressHelper()?.ShowError($"Error creating operation: {insertResult.ErrorMessage}");
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Operation added successfully: Operation={operation}");
            GetProgressHelper()?.ShowSuccess($"Operation '{operation}' created successfully!");
            ShowSuccess($"Operation '{operation}' added successfully.");
            
            // Refresh caches
            await RefreshOperationCachesAsync();
            NotifyOperationListChanged();
            RaiseStatusMessage($"Operation '{operation}' created successfully");
            
            // Clear form
            ClearAddSection();
            Control_OperationManagement_TextBox_AddOperation.Focus();
            
            await Task.Delay(500);
            GetProgressHelper()?.HideProgress();
        }

        private async Task HandleEditOperationSaveAsync()
        {
            if (_selectedEditOperation == null)
            {
                ShowWarning("Please select an operation to edit first.");
                return;
            }

            string originalOperation = _selectedEditOperation["Operation"]?.ToString() ?? string.Empty;
            string newOperation = Control_OperationManagement_TextBox_EditNewOperation.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(newOperation))
            {
                ShowWarning("Operation number is required.");
                Control_OperationManagement_TextBox_EditNewOperation.Focus();
                return;
            }

            // Check for duplicates if name changed
            if (newOperation != originalOperation)
            {
                var existsResult = await Dao_Operation.OperationExists(newOperation);
                if (!existsResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception(existsResult.ErrorMessage ?? "Failed to check if operation exists"),
                        contextData: new Dictionary<string, object>
                        {
                            ["OriginalOperation"] = originalOperation,
                            ["NewOperation"] = newOperation,
                            ["User"] = Model_Application_Variables.User ?? "Unknown"
                        },
                        callerName: nameof(HandleEditOperationSaveAsync),
                        controlName: this.Name);
                    return;
                }

                if (existsResult.Data)
                {
                    ShowWarning($"Operation '{newOperation}' already exists.");
                    Control_OperationManagement_TextBox_EditNewOperation.Focus();
                    Control_OperationManagement_TextBox_EditNewOperation.TextBox.SelectAll();
                    return;
                }
            }

            // Update operation
            var updateResult = await Dao_Operation.UpdateOperation(
                originalOperation, 
                newOperation,
                Model_Application_Variables.User ?? "Unknown");

            if (!updateResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(updateResult.ErrorMessage ?? "Failed to update operation"),
                    contextData: new Dictionary<string, object>
                    {
                        ["OriginalOperation"] = originalOperation,
                        ["NewOperation"] = newOperation,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleEditOperationSaveAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Operation updated successfully: Original={originalOperation}, New={newOperation}");
            ShowSuccess($"Operation '{newOperation}' updated successfully.");
            
            // Refresh caches
            await RefreshOperationCachesAsync();
            NotifyOperationListChanged();
            RaiseStatusMessage($"Operation updated successfully");
            
            // Clear form
            ClearEditSection();
            Control_OperationManagement_Suggestion_EditSelectOperation.Focus();
        }

        private async Task HandleRemoveOperationAsync()
        {
            if (_selectedRemoveOperation == null)
            {
                ShowWarning("Please select an operation to remove first.");
                return;
            }

            string operation = _selectedRemoveOperation["Operation"]?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(operation))
            {
                ShowWarning("Invalid operation selection.");
                return;
            }

            // Confirm deletion
            var confirmation = MessageBox.Show(
                $"Are you sure you want to permanently remove operation '{operation}'?\n\nThis action cannot be undone.",
                "Confirm Removal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            // Delete operation
            var deleteResult = await Dao_Operation.DeleteOperation(operation);
            if (!deleteResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(deleteResult.ErrorMessage ?? "Failed to delete operation"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = operation,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleRemoveOperationAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Operation removed successfully: Operation={operation}");
            ShowSuccess($"Operation '{operation}' removed successfully.");
            
            // Refresh caches
            await RefreshOperationCachesAsync();
            NotifyOperationListChanged();
            RaiseStatusMessage($"Operation removed successfully");
            
            // Clear form
            ClearRemoveSection();
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Focus();
        }

        private async Task LoadEditOperationAsync(string operationNumber)
        {
            if (string.IsNullOrWhiteSpace(operationNumber))
            {
                SetEditSectionEnabled(false);
                return;
            }

            var result = await Dao_Operation.GetOperationByNumber(operationNumber);
            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(result.ErrorMessage ?? "Failed to load operation details"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = operationNumber,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(LoadEditOperationAsync),
                    controlName: this.Name);
                SetEditSectionEnabled(false);
                return;
            }

            _selectedEditOperation = result.Data;
            
            // Populate fields
            string operation = _selectedEditOperation["Operation"]?.ToString() ?? string.Empty;
            string issuedBy = _selectedEditOperation["IssuedBy"]?.ToString() ?? "Unknown";

            Control_OperationManagement_TextBox_EditNewOperation.Text = operation;
            Control_OperationManagement_Label_EditIssuedByValue.Text = issuedBy;

            SetEditSectionEnabled(true);
        }

        private async Task LoadRemoveOperationAsync(string operationNumber)
        {
            if (string.IsNullOrWhiteSpace(operationNumber))
            {
                SetRemoveSectionEnabled(false);
                return;
            }

            var result = await Dao_Operation.GetOperationByNumber(operationNumber);
            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(result.ErrorMessage ?? "Failed to load operation details"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = operationNumber,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(LoadRemoveOperationAsync),
                    controlName: this.Name);
                SetRemoveSectionEnabled(false);
                return;
            }

            _selectedRemoveOperation = result.Data;
            
            // Display details
            string operation = _selectedRemoveOperation["Operation"]?.ToString() ?? "N/A";
            string issuedBy = _selectedRemoveOperation["IssuedBy"]?.ToString() ?? "Unknown";

            Control_OperationManagement_Label_RemoveOperationValue.Text = operation;
            Control_OperationManagement_Label_RemoveIssuedByValue.Text = issuedBy;

            SetRemoveSectionEnabled(true);
        }

        private void SetEditSectionEnabled(bool enabled)
        {
            Control_OperationManagement_TextBox_EditNewOperation.Enabled = enabled;
            Control_OperationManagement_TextBox_EditNewOperation.TextBox.Enabled = enabled;
            Control_OperationManagement_Button_EditSave.Enabled = enabled;
            Control_OperationManagement_Button_EditReset.Enabled = enabled;
        }

        private void SetRemoveSectionEnabled(bool enabled)
        {
            Control_OperationManagement_Button_RemoveConfirm.Enabled = enabled;
            Control_OperationManagement_Button_RemoveCancel.Enabled = enabled;
        }

        private void ClearAddSection()
        {
            Control_OperationManagement_TextBox_AddOperation.ClearTextBox();
        }

        private void ClearEditSection()
        {
            _selectedEditOperation = null;
            Control_OperationManagement_Suggestion_EditSelectOperation.ClearTextBox();
            Control_OperationManagement_TextBox_EditNewOperation.ClearTextBox();
            Control_OperationManagement_Label_EditIssuedByValue.Text = Model_Application_Variables.User ?? "Current User";
            SetEditSectionEnabled(false);
        }

        private void ClearRemoveSection()
        {
            _selectedRemoveOperation = null;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.ClearTextBox();
            Control_OperationManagement_Label_RemoveOperationValue.Text = string.Empty;
            Control_OperationManagement_Label_RemoveIssuedByValue.Text = Model_Application_Variables.User ?? "Current User";
            SetRemoveSectionEnabled(false);
        }

        private void NotifyOperationListChanged()
        {
            OperationListChanged?.Invoke(this, EventArgs.Empty);
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
            Control_OperationManagement_Panel_HomeTile_Add.Visible = canAdd;
            Control_OperationManagement_Panel_HomeTile_Edit.Visible = canEdit;
            Control_OperationManagement_Panel_HomeTile_Remove.Visible = canRemove;
            
            // Add card
            Control_OperationManagement_Panel_AddCard.Visible = canAdd;
            Control_OperationManagement_TextBox_AddOperation.Enabled = canAdd;
            Control_OperationManagement_Button_AddSave.Enabled = canAdd;
            Control_OperationManagement_Button_AddClear.Enabled = canAdd;

            // Edit card
            Control_OperationManagement_Panel_EditCard.Visible = canEdit;
            Control_OperationManagement_Suggestion_EditSelectOperation.Enabled = canEdit;
            Control_OperationManagement_TextBox_EditNewOperation.Enabled = canEdit;
            Control_OperationManagement_Button_EditSave.Enabled = canEdit;
            Control_OperationManagement_Button_EditReset.Enabled = canEdit;

            // Remove card
            Control_OperationManagement_Panel_RemoveCard.Visible = canRemove;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Enabled = canRemove;
            Control_OperationManagement_Button_RemoveConfirm.Enabled = canRemove && _selectedRemoveOperation != null;
            Control_OperationManagement_Button_RemoveCancel.Enabled = canRemove;

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
                Control_OperationManagement_Label_Subtitle.Text = "You do not have permissions to manage operations.";
                Control_OperationManagement_Panel_Home.Visible = false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when operation data has changed (add, edit, remove) so parent views can refresh caches.
        /// </summary>
        public event EventHandler? OperationListChanged;

        /// <summary>
        /// Raised to update status message in parent form.
        /// </summary>
        public event EventHandler<string>? StatusMessageChanged;

        #endregion

        #region Helpers

        private static async Task RefreshOperationCachesAsync()
        {
            try
            {
                await Helper_SuggestionTextBox.GetCachedOperationsAsync();
                await Helper_UI_ComboBoxes.FillOperationComboBoxesAsync(new ComboBox());
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(message, "Validation Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private static void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Cleanup / Dispose

        #endregion
    }
}
