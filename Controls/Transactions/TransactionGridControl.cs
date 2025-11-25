using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.Transactions;

/// <summary>
/// UserControl for displaying transaction search results in a paginated DataGridView.
/// Migrated to ThemedUserControl for automatic DPI scaling and theme support.
/// </summary>
/// <remarks>
/// This control handles the grid display, pagination controls, and row selection.
/// It raises events for page changes and row selection to allow parent forms to react.
/// </remarks>
internal partial class TransactionGridControl : ThemedUserControl
{
    #region Fields

    private Model_Transactions_SearchResult? _currentResults;
    private int _currentPage = 1;

    #endregion

    #region Events

    /// <summary>
    /// Raised when the user navigates to a different page.
    /// </summary>
    public event EventHandler<int>? PageChanged;

    /// <summary>
    /// Raised when the user selects a transaction row.
    /// </summary>
    public event EventHandler<Model_Transactions_Core>? RowSelected;

    /// <summary>
    /// Raised when the user clicks the Show/Hide Search button.
    /// </summary>
    public event EventHandler? ToggleSearchRequested;

    /// <summary>
    /// Raised when the user clicks the Export button.
    /// </summary>
    public event EventHandler? ExportRequested;

    /// <summary>
    /// Raised when the user clicks the Print button.
    /// </summary>
    public event EventHandler? PrintRequested;

    /// <summary>
    /// Raised when the user clicks the Analytics button.
    /// </summary>
    public event EventHandler? AnalyticsRequested;

    /// <summary>
    /// Raised whenever the transaction information panel visibility changes.
    /// </summary>
    public event EventHandler<bool>? InformationPanelToggled;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the currently selected transaction, or null if no selection.
    /// </summary>
    public Model_Transactions_Core? SelectedTransaction
    {
        get
        {
            if (TransactionGridControl_DataGridView_Transactions.SelectedRows.Count > 0)
            {
                return TransactionGridControl_DataGridView_Transactions.SelectedRows[0].DataBoundItem as Model_Transactions_Core;
            }
            return null;
        }
    }

    /// <summary>
    /// Gets the underlying DataGridView for advanced operations like printing.
    /// </summary>
    public DataGridView DataGridView => TransactionGridControl_DataGridView_Transactions;

    /// <summary>
    /// Gets the analytics control for external data binding.
    /// </summary>
    internal Model_Transactions_Core_AnalyticsControl AnalyticsControl => TransactionGridControl_Model_Transactions_Core_AnalyticsControl;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionGridControl"/> class.
    /// </summary>
    public TransactionGridControl()
    {
        InitializeComponent();



        InitializeColumns();
        WireUpEvents();
        ApplyAnalyticsVisibility(false);

        // Initially disable Export and Print buttons until grid has data
        SetExportPrintButtonsEnabled(false);


    }

    #endregion

    #region Initialization

    /// <summary>
    /// Configures DataGridView columns programmatically.
    /// </summary>
    private void InitializeColumns()
    {
        TransactionGridControl_DataGridView_Transactions.AutoGenerateColumns = false;
        TransactionGridControl_DataGridView_Transactions.Columns.Clear();

        // ID Column - 80px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colID",
            HeaderText = "ID",
            DataPropertyName = "ID",
            Width = 80,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        // Type Column - 100px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colType",
            HeaderText = "Type",
            DataPropertyName = "TransactionType",
            Width = 100,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // Part Number Column - 150px, Fill
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colPartNumber",
            HeaderText = "Part Number",
            DataPropertyName = "PartID",
            Width = 150,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            MinimumWidth = 100
        });

        // Quantity Column - 80px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colQuantity",
            HeaderText = "Qty",
            DataPropertyName = "Quantity",
            Width = 80,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        // From Location Column - 120px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colFromLocation",
            HeaderText = "From",
            DataPropertyName = "FromLocation",
            Width = 120,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // To Location Column - 120px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colToLocation",
            HeaderText = "To",
            DataPropertyName = "ToLocation",
            Width = 120,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // User Column - 100px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colUser",
            HeaderText = "User",
            DataPropertyName = "User",
            Width = 100,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        });

        // Date/Time Column - 140px
        TransactionGridControl_DataGridView_Transactions.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colDateTime",
            HeaderText = "Date/Time",
            DataPropertyName = "DateTime",
            Width = 140,
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "MM/dd/yy HH:mm" }
        });

        // Enable sorting on all columns
        foreach (DataGridViewColumn column in TransactionGridControl_DataGridView_Transactions.Columns)
        {
            column.SortMode = DataGridViewColumnSortMode.Automatic;
        }

        // Set alternating row colors for better readability
        TransactionGridControl_DataGridView_Transactions.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
    }

    /// <summary>
    /// Wires up event handlers for pagination and selection.
    /// </summary>
    private void WireUpEvents()
    {
        TransactionGridControl_Button_Previous.Click += BtnPrevious_Click;
        TransactionGridControl_Button_Next.Click += BtnNext_Click;
        TransactionGridControl_Button_GoToPage.Click += BtnGoToPage_Click;
        TransactionGridControl_TextBox_GoToPage.KeyPress += TxtGoToPage_KeyPress;
        TransactionGridControl_DataGridView_Transactions.SelectionChanged += DgvTransactions_SelectionChanged;
        TransactionGridControl_Button_ShowHideSearch.Click += BtnShowHideSearch_Click;
        TransactionGridControl_Button_Export.Click += BtnExport_Click;
        TransactionGridControl_Button_Print.Click += BtnPrint_Click;
        TransactionGridControl_Button_Analytics.Click += BtnAnalytics_Click;
        TransactionGridControl_Button_ToggleDetails.Click += BtnToggleDetails_Click;

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Displays transaction search results and updates pagination controls.
    /// </summary>
    /// <param name="results">The search results with pagination metadata.</param>
    public void DisplayResults(Model_Transactions_SearchResult results)
    {
        if (results == null)
        {
            throw new ArgumentNullException(nameof(results));
        }

        try
        {


            _currentResults = results;
            _currentPage = results.CurrentPage;

            // Suspend layout to prevent flickering
            TransactionGridControl_DataGridView_Transactions.SuspendLayout();

            try
            {
                // Bind data
                TransactionGridControl_DataGridView_Transactions.DataSource = new BindingSource { DataSource = results.Transactions };

                // Apply row colors based on transaction type
                Service_DataGridView.ApplyTransactionRowColors(TransactionGridControl_DataGridView_Transactions);

                // Apply standard settings (Theme + User Settings)
                _ = Service_DataGridView.ApplyStandardSettingsAsync(TransactionGridControl_DataGridView_Transactions, Model_Application_Variables.User);

                // Update pagination controls
                UpdatePaginationControls();

                // Enable Export and Print buttons when grid has data
                bool hasData = results.Transactions != null && results.Transactions.Count > 0;
                SetExportPrintButtonsEnabled(hasData);


            }
            finally
            {
                TransactionGridControl_DataGridView_Transactions.ResumeLayout();
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                controlName: nameof(TransactionGridControl));
        }
    }

    /// <summary>
    /// Clears the grid and resets pagination controls.
    /// </summary>
    public void ClearResults()
    {
        _currentResults = null;
        _currentPage = 1;
        TransactionGridControl_DataGridView_Transactions.DataSource = null;
        TransactionGridControl_TransactionDetailPanel.ClearDetails();
        UpdatePaginationControls();

        // Disable Export and Print buttons when grid is cleared
        SetExportPrintButtonsEnabled(false);
    }

    /// <summary>
    /// Enables or disables the Export and Print buttons.
    /// </summary>
    /// <param name="enabled">True to enable buttons, false to disable.</param>
    public void SetExportPrintButtonsEnabled(bool enabled)
    {
        TransactionGridControl_Button_Export.Enabled = enabled;
        TransactionGridControl_Button_Print.Enabled = enabled;


    }

    /// <summary>
    /// Gets whether the information panel is currently visible.
    /// </summary>
    public bool InformationPanelVisible => TransactionGridControl_TransactionDetailPanel.Visible;

    /// <summary>
    /// Toggles the information panel visibility and returns the new state.
    /// </summary>
    public bool ToggleInformationPanel()
    {
        ApplyInformationPanelVisibility(!TransactionGridControl_TransactionDetailPanel.Visible);
        return TransactionGridControl_TransactionDetailPanel.Visible;
    }

    #endregion

    #region Button Clicks

    private void BtnPrevious_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_currentResults != null && _currentResults.HasPreviousPage)
            {

                PageChanged?.Invoke(this, _currentPage - 1);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private void BtnNext_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_currentResults != null && _currentResults.HasNextPage)
            {

                PageChanged?.Invoke(this, _currentPage + 1);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private void BtnGoToPage_Click(object? sender, EventArgs e)
    {
        try
        {

            GoToPageFromTextBox();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private void BtnShowHideSearch_Click(object? sender, EventArgs e)
    {
        try
        {

            ToggleSearchRequested?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private void BtnToggleDetails_Click(object? sender, EventArgs e)
    {
        try
        {

            ToggleInformationPanel();
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private void BtnExport_Click(object? sender, EventArgs e)
    {
        try
        {


            // Raise export event for parent form to handle
            ExportRequested?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private async void BtnPrint_Click(object? sender, EventArgs e)
    {
        try
        {
            // Use centralized print service
            string gridName = "Transaction History";
            await Service_DataGridView.PrintGridAsync(this, TransactionGridControl_DataGridView_Transactions, gridName);

            // Raise print event for parent form to handle (if needed for other logic)
            PrintRequested?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    private void BtnAnalytics_Click(object? sender, EventArgs e)
    {
        try
        {

            ApplyAnalyticsVisibility(!TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Visible);

            // Raise analytics event for parent form to handle (e.g., refresh analytics data)
            AnalyticsRequested?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

#if DEBUG
    /// <summary>
    /// Handles the Toggle Privileges button click (Debug mode only).
    /// Cycles through: ReadOnly → User → Admin → Developer → ReadOnly.
    /// </summary>
    private async void BtnTogglePrivileges_Click(object? sender, EventArgs e)
    {
        try
        {


            // Get current user ID from database
            var userResult = await Data.Dao_User.GetUserByUsernameAsync(Model_Application_Variables.User);
            if (!userResult.IsSuccess || userResult.Data == null)
            {
                Service_ErrorHandler.HandleValidationError(
                    $"Failed to get current user information: {userResult.ErrorMessage}",
                    nameof(TransactionGridControl));
                return;
            }

            int userId = Convert.ToInt32(userResult.Data["ID"]);

            // Determine current role
            string currentRole = "User";
            if (Model_Application_Variables.UserTypeDeveloper)
                currentRole = "Developer";
            else if (Model_Application_Variables.UserTypeAdmin)
                currentRole = "Admin";
            else if (Model_Application_Variables.UserTypeReadOnly)
                currentRole = "ReadOnly";

            // Determine next role in cycle: ReadOnly → User → Admin → Developer → ReadOnly
            string nextRole = currentRole switch
            {
                "Developer" => "ReadOnly",
                "ReadOnly" => "User",
                "User" => "Admin",
                "Admin" => "Developer",
                _ => "User"
            };



            // Get the next role ID
            var getRoleIdParams = new Dictionary<string, object>
            {
                ["RoleName"] = nextRole
            };

            var roleIdResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_GetRoleIdByName",
                getRoleIdParams
            );

            if (!roleIdResult.IsSuccess || roleIdResult.Data == null || roleIdResult.Data.Rows.Count == 0)
            {
                Service_ErrorHandler.HandleValidationError(
                    $"Failed to get role ID for {nextRole}: {roleIdResult.StatusMessage}",
                    nameof(TransactionGridControl));
                return;
            }

            int nextRoleId = Convert.ToInt32(roleIdResult.Data.Rows[0]["RoleId"]);

            if (nextRoleId == 0)
            {
                Service_ErrorHandler.HandleValidationError(
                    $"Role '{nextRole}' not found in database. Please ensure sys_roles table is populated.",
                    nameof(TransactionGridControl));
                return;
            }

            // Update the user's role
            var updateRoleParams = new Dictionary<string, object>
            {
                ["UserID"] = userId,
                ["NewRoleID"] = nextRoleId,
                ["AssignedBy"] = Model_Application_Variables.User + " (Debug Toggle)"
            };

            var updateResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "sys_user_roles_Update",
                updateRoleParams
            );

            if (!updateResult.IsSuccess)
            {
                Service_ErrorHandler.HandleValidationError(
                    $"Failed to update role: {updateResult.StatusMessage}",
                    nameof(TransactionGridControl));
                return;
            }

            // Update Model_Application_Variables to reflect new role
            Model_Application_Variables.UserTypeDeveloper = nextRole == "Developer";
            Model_Application_Variables.UserTypeAdmin = nextRole == "Admin";
            Model_Application_Variables.UserTypeReadOnly = nextRole == "ReadOnly";
            Model_Application_Variables.UserTypeNormal = nextRole == "User";



            // Show confirmation message using Service_ErrorHandler
            Service_ErrorHandler.ShowInformation(
                $"Debug Mode: User privilege changed from {currentRole} to {nextRole}.\n\n" +
                $"The application UI will reflect this change immediately.",
                "Privilege Changed (Debug)",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                controlName: nameof(TransactionGridControl));
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["CurrentUser"] = Model_Application_Variables.User
                },
                controlName: nameof(TransactionGridControl));
        }
    }
#endif

    private void TxtGoToPage_KeyPress(object? sender, KeyPressEventArgs e)
    {
        // Allow only digits, backspace, and Enter
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
        {
            e.Handled = true;
            return;
        }

        // Handle Enter key
        if (e.KeyChar == (char)Keys.Enter)
        {
            e.Handled = true;
            GoToPageFromTextBox();
        }
    }

    #endregion

    #region UI Events

    private void DgvTransactions_SelectionChanged(object? sender, EventArgs e)
    {
        try
        {
            var selectedTransaction = SelectedTransaction;
            if (selectedTransaction != null)
            {


                // Update the detail panel with the selected transaction
                TransactionGridControl_TransactionDetailPanel.Transaction = selectedTransaction;

                // Raise event for parent form
                RowSelected?.Invoke(this, selectedTransaction);
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low,
                controlName: nameof(TransactionGridControl));
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Updates the pagination controls based on current results.
    /// </summary>
    private void UpdatePaginationControls()
    {


        if (_currentResults == null)
        {

            TransactionGridControl_Button_Previous.Enabled = false;
            TransactionGridControl_Button_Next.Enabled = false;
            TransactionGridControl_Label_PageIndicator.Text = "Page 0 of 0";
            TransactionGridControl_Label_RecordCount.Text = "0 records";
            TransactionGridControl_TextBox_GoToPage.Enabled = false;
            TransactionGridControl_Button_GoToPage.Enabled = false;
            return;
        }








        // Enable/disable navigation buttons
        TransactionGridControl_Button_Previous.Enabled = _currentResults.HasPreviousPage;
        TransactionGridControl_Button_Next.Enabled = _currentResults.HasNextPage;





        // Update labels
        TransactionGridControl_Label_PageIndicator.Text = $"Page {_currentResults.CurrentPage} of {_currentResults.TotalPages}";
        TransactionGridControl_Label_RecordCount.Text = $"{_currentResults.TotalRecordCount} records found";

        // Enable page jump controls
        TransactionGridControl_TextBox_GoToPage.Enabled = _currentResults.TotalPages > 1;
        TransactionGridControl_Button_GoToPage.Enabled = _currentResults.TotalPages > 1;
    }

    /// <summary>
    /// Navigates to the page number entered in the text box.
    /// </summary>
    private void GoToPageFromTextBox()
    {
        if (_currentResults == null || string.IsNullOrWhiteSpace(TransactionGridControl_TextBox_GoToPage.Text))
        {
            return;
        }

        if (int.TryParse(TransactionGridControl_TextBox_GoToPage.Text, out int pageNumber))
        {
            if (pageNumber >= 1 && pageNumber <= _currentResults.TotalPages)
            {
                PageChanged?.Invoke(this, pageNumber);
                TransactionGridControl_TextBox_GoToPage.Text = string.Empty; // Clear after successful navigation
            }
            else
            {
                Service_ErrorHandler.HandleValidationError(
                    $"Page number must be between 1 and {_currentResults.TotalPages}.",
                    nameof(TransactionGridControl_TextBox_GoToPage));
            }
        }
    }

    private void ApplyInformationPanelVisibility(bool visible)
    {
        TransactionGridControl_TransactionDetailPanel.Visible = visible;

        if (visible)
        {
            TransactionGridControl_TableLayout_Main.SetColumnSpan(TransactionGridControl_Panel_DataGridView, 1);
            TransactionGridControl_Button_ToggleDetails.Text = "📋 ▶";
            TransactionGridControl_Button_ToggleDetails.ToolTipText = "Hide transaction details panel";
        }
        else
        {
            TransactionGridControl_TableLayout_Main.SetColumnSpan(TransactionGridControl_Panel_DataGridView, 2);
            TransactionGridControl_Button_ToggleDetails.Text = "◀ 📋";
            TransactionGridControl_Button_ToggleDetails.ToolTipText = "Show transaction details panel";
        }

        InformationPanelToggled?.Invoke(this, visible);
    }

    private void ApplyAnalyticsVisibility(bool showAnalytics)
    {
        TransactionGridControl_Model_Transactions_Core_AnalyticsControl.Visible = showAnalytics;
        TransactionGridControl_DataGridView_Transactions.Visible = !showAnalytics;

        if (showAnalytics)
        {
            TransactionGridControl_Model_Transactions_Core_AnalyticsControl.BringToFront();
            TransactionGridControl_Button_Analytics.Text = "Grid";
            TransactionGridControl_Button_Analytics.ToolTipText = "Show transaction grid";

        }
        else
        {
            TransactionGridControl_DataGridView_Transactions.BringToFront();
            TransactionGridControl_Button_Analytics.Text = "Analytics";
            TransactionGridControl_Button_Analytics.ToolTipText = "Show analytics summary";

        }
    }

    #endregion
}

