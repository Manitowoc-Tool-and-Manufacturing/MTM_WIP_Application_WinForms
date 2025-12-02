using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Data;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    /// <summary>
    /// Control for tracking part transaction lifecycles and auditing inventory history.
    /// Supports multiple filter modes via dynamic search selection.
    /// </summary>
    public partial class Control_InventoryAudit : ThemedUserControl
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private DataTable? _cachedDataTable;
        private string _lastSearchSelection = string.Empty;
        #endregion

        #region Constructors
        public Control_InventoryAudit()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            
            // Initialize Search By options
            _txtSearchBy.TextBox.DataProvider = () => Task.FromResult(new List<string> 
            { 
                "Part Number", 
                "User", 
                "Work Order", 
                "Customer Order", 
                "Purchase Order" 
            });
            _txtSearchBy.EnableSuggestions = true;
            _txtSearchBy.ShowF4Button = true;
            _txtSearchBy.Text = "Part Number"; // Default

            // Initialize Analytics Workflow List
            _ProcessUserAnalytics.SelectionMode = SelectionMode.None;

            WireUpEvents();
            ConfigureSuggestions();
            InitializeDefaultValues();
            
            // Trigger initial setup for default selection
            OnSearchBySelected();
        }
        #endregion

        #region Initialization
        private void ConfigureSuggestions()
        {
            // Initial configuration for Part Number (default)
            Helper_SuggestionTextBox.ConfigureForPartNumbers(_txtLifecyclePart, GetPartIdsAsync);
        }

        private void InitializeDefaultValues()
        {
            _dtpLifecycleStart.Value = DateTime.Today.AddDays(-30);
            _dtpLifecycleEnd.Value = DateTime.Today;
            
            // Analytics Defaults
            _dtpAnalyticsStart.Value = DateTime.Today;
            _dtpAnalyticsEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1); // End of today
            
            UpdateAnalyticsWorkflow();
        }

        private void WireUpEvents()
        {
            _btnSearch.Click += async (s, e) => await PerformSearchAsync();
            _btnExport.Click += BtnExport_Click;
            
            // Search By Selection Change
            _txtSearchBy.SuggestionSelected += (s, e) => OnSearchBySelected();
            _txtSearchBy.TextBox.Leave += (s, e) => OnSearchBySelected(); // Ensure update on leave if typed

            // User Analytics Events
            _btnLoadUsers.Click += async (s, e) => await LoadUsersForAnalyticsAsync();
            _btnSelectAllUsers.Click += (s, e) => SelectAllUsers();
            _btnGenerateReport.Click += async (s, e) => await GenerateAnalyticsReportAsync();
            _clbUsers.ItemCheck += (s, e) => 
            {
                // Delay check to allow ItemCheck to complete
                this.BeginInvoke(new Action(() => 
                {
                    UpdateUserSelectionState();
                    UpdateAnalyticsWorkflow();
                }));
            };
            
            _dtpAnalyticsStart.ValueChanged += (s, e) => UpdateAnalyticsWorkflow();
            _dtpAnalyticsEnd.ValueChanged += (s, e) => UpdateAnalyticsWorkflow();

            // Enter key support on inputs
            _txtLifecyclePart.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            
            _dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
        }
        #endregion

        #region Methods
        
        private void OnSearchBySelected()
        {
            string selection = _txtSearchBy.Text;
            
            // Prevent unnecessary updates if selection hasn't changed
            if (string.Equals(selection, _lastSearchSelection, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _lastSearchSelection = selection;
            _txtLifecyclePart.Text = string.Empty;

            switch (selection)
            {
                case "User":
                    _txtLifecyclePart.LabelText = "Enter User ID";
                    _txtLifecyclePart.PlaceholderText = "Enter User Name";
                    Helper_SuggestionTextBox.ConfigureForUsers(_txtLifecyclePart, GetUsersAsync);
                    break;
                case "Work Order":
                    _txtLifecyclePart.LabelText = "Enter Work Order";
                    _txtLifecyclePart.PlaceholderText = "Enter Work Order Number";
                    Helper_SuggestionTextBox.ConfigureForWorkOrders(_txtLifecyclePart, GetWorkOrdersAsync);
                    break;
                case "Customer Order":
                    _txtLifecyclePart.LabelText = "Enter Customer Order";
                    _txtLifecyclePart.PlaceholderText = "Enter Customer Order Number";
                    Helper_SuggestionTextBox.ConfigureForCustomerOrders(_txtLifecyclePart, GetCustomerOrdersAsync);
                    break;
                case "Purchase Order":
                    _txtLifecyclePart.LabelText = "Enter Purchase Order";
                    _txtLifecyclePart.PlaceholderText = "Enter Purchase Order Number";
                    Helper_SuggestionTextBox.ConfigureForPurchaseOrders(_txtLifecyclePart, GetPurchaseOrdersAsync);
                    break;
                case "Part Number":
                default:
                    _txtLifecyclePart.LabelText = "Enter Part ID";
                    _txtLifecyclePart.PlaceholderText = "Enter Part Number";
                    Helper_SuggestionTextBox.ConfigureForPartNumbers(_txtLifecyclePart, GetPartIdsAsync);
                    break;
            }
        }

        private void UpdateAnalyticsWorkflow()
        {
            // Step 1: Enter Desired Date Range (Always true if dates valid)
            SetWorkflowStep(0, true);

            // Step 2: Click Load Users (True if users loaded)
            bool usersLoaded = _clbUsers.Items.Count > 0;
            SetWorkflowStep(1, usersLoaded);

            // Step 3: Click Select All Users (Optional - True if all selected)
            bool allSelected = usersLoaded && _clbUsers.CheckedItems.Count == _clbUsers.Items.Count;
            SetWorkflowStep(2, allSelected);

            // Step 4: Select Users Below (True if any selected)
            bool anySelected = _clbUsers.CheckedItems.Count > 0;
            SetWorkflowStep(3, anySelected);

            // Step 5: Click Generate Report (Handled in GenerateAnalyticsReportAsync)
        }

        private void SetWorkflowStep(int index, bool isChecked)
        {
            if (index >= 0 && index < _ProcessUserAnalytics.Items.Count)
            {
                _ProcessUserAnalytics.SetItemChecked(index, isChecked);
            }
        }

        private async Task<List<string>> GetPartIdsAsync()
        {
            if (_visualService == null) return new List<string>();
            var result = await _visualService.GetPartIdsAsync();
            return result.IsSuccess ? (result.Data ?? new List<string>()) : new List<string>();
        }

        private async Task<List<string>> GetUsersAsync()
        {
            if (_visualService == null) return new List<string>();
            var result = await _visualService.GetUserIdsAsync();
            return result.IsSuccess ? (result.Data ?? new List<string>()) : new List<string>();
        }

        private async Task<List<string>> GetWorkOrdersAsync()
        {
            if (_visualService == null) return new List<string>();
            var result = await _visualService.GetWorkOrdersAsync();
            return result.IsSuccess ? (result.Data ?? new List<string>()) : new List<string>();
        }

        private async Task<List<string>> GetPurchaseOrdersAsync()
        {
            if (_visualService == null) return new List<string>();
            var result = await _visualService.GetPurchaseOrdersAsync();
            return result.IsSuccess ? (result.Data ?? new List<string>()) : new List<string>();
        }

        private async Task<List<string>> GetCustomerOrdersAsync()
        {
            if (_visualService == null) return new List<string>();
            var result = await _visualService.GetCustomerOrdersAsync();
            return result.IsSuccess ? (result.Data ?? new List<string>()) : new List<string>();
        }

        private async void DataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = _dataGridView.Rows[e.RowIndex];
                if (row.DataBoundItem is DataRowView drv)
                {
                    // Check if we are already in Lifecycle View and have a Trans ID
                    if (_tabControl.SelectedTab == _tabLifecycle && drv.Row.Table.Columns.Contains("Trans ID"))
                    {
                        // This is a trace request on a specific transaction
                        string transIdStr = drv["Trans ID"]?.ToString() ?? "";
                        
                        // Handle combined IDs (e.g. "123/124")
                        int transId = 0;
                        if (transIdStr.Contains("/"))
                        {
                            // Pick the first one (usually the OUT part of a transfer)
                            int.TryParse(transIdStr.Split('/')[0], out transId);
                        }
                        else
                        {
                            int.TryParse(transIdStr, out transId);
                        }

                        if (transId > 0 && _cachedDataTable != null)
                        {
                            await PerformTraceSearchAsync(transId);
                            return;
                        }
                    }

                    // Otherwise, standard Part ID switch logic
                    string partId = "";
                    if (drv.Row.Table.Columns.Contains("PART_ID"))
                        partId = drv["PART_ID"]?.ToString() ?? "";
                    else if (drv.Row.Table.Columns.Contains("Part Number"))
                        partId = drv["Part Number"]?.ToString() ?? "";
                    else if (drv.Row.Table.Columns.Contains("Part ID"))
                        partId = drv["Part ID"]?.ToString() ?? "";
                    
                    if (!string.IsNullOrEmpty(partId))
                    {
                        // Switch to Lifecycle tab and Part Number mode
                        _tabControl.SelectedTab = _tabLifecycle;
                        _txtSearchBy.Text = "Part Number";
                        OnSearchBySelected();
                        _txtLifecyclePart.Text = partId;
                        
                        // Set date range to cover a broad history
                        _dtpLifecycleStart.Value = DateTime.Today.AddYears(-2); 
                        _dtpLifecycleEnd.Value = DateTime.Today;

                        await PerformSearchAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: this.Name);
            }
        }

        private async Task PerformTraceSearchAsync(int transId)
        {
            if (_visualService == null) return;

            try
            {
                _btnSearch.Enabled = false;
                _btnSearch.Text = "Tracing...";

                // We need to fetch the raw transactions for the current part first
                // We can reuse the current filter settings since we are already on the Lifecycle tab
                var filter = new Model_VisualTransactionFilter
                {
                    PartId = _txtLifecyclePart.Text?.Trim(),
                    StartDate = _dtpLifecycleStart.Value,
                    EndDate = _dtpLifecycleEnd.Value
                };

                var result = await _visualService.GetTransactionsAsync(filter);

                if (result.IsSuccess && result.Data != null)
                {
                    // Apply Trace Logic
                    _cachedDataTable = Helper_VisualLifecycle.TraceTransactionFlow(result.Data, transId);
                    
                    _dataGridView.DataSource = _cachedDataTable;
                    await Service_DataGridView.ApplyStandardSettingsAsync(_dataGridView, Model_Application_Variables.User);
                    Service_DataGridView.ApplySmartNumericFormatting(_dataGridView);
                    ApplyLifecycleColoring();
                    
                    Service_ErrorHandler.ShowInformation("Showing transaction flow trace. Search again to reset.");
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                _btnSearch.Enabled = true;
                _btnSearch.Text = "Search";
            }
        }

        private async Task PerformSearchAsync()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                return;
            }

            try
            {
                _btnSearch.Enabled = false;
                _btnSearch.Text = "Searching...";

                var filter = new Model_VisualTransactionFilter();
                bool isLifecycleView = false;

                // Populate filter based on active tab and search selection
                if (_tabControl.SelectedTab == _tabLifecycle)
                {
                    string searchBy = _txtSearchBy.Text;
                    string searchTerm = _txtLifecyclePart.Text?.Trim() ?? "";
                    
                    filter.StartDate = _dtpLifecycleStart.Value;
                    filter.EndDate = _dtpLifecycleEnd.Value;

                    if (string.IsNullOrEmpty(searchTerm))
                    {
                        Service_ErrorHandler.ShowUserError($"Please enter a {searchBy}.");
                        return;
                    }

                    switch (searchBy)
                    {
                        case "User":
                            filter.UserId = searchTerm;
                            break;
                        case "Work Order":
                            filter.WorkOrder = searchTerm;
                            break;
                        case "Customer Order":
                            filter.CustomerOrder = searchTerm;
                            break;
                        case "Purchase Order":
                            filter.PurchaseOrder = searchTerm;
                            break;
                        case "Part Number":
                        default:
                            filter.PartId = searchTerm;
                            isLifecycleView = true; // Only Part Number search supports lifecycle view logic
                            break;
                    }
                }
                else if (_tabControl.SelectedTab == _tabUserAnalytics)
                {
                    // Should not happen as search button is on Lifecycle tab, but just in case
                    return;
                }

                var result = await _visualService.GetTransactionsAsync(filter);

                if (result.IsSuccess && result.Data != null)
                {
                    if (isLifecycleView)
                    {
                        _cachedDataTable = Helper_VisualLifecycle.ProcessTransactions(result.Data);
                    }
                    else
                    {
                        _cachedDataTable = result.Data;
                    }

                    _dataGridView.DataSource = _cachedDataTable;
                    await Service_DataGridView.ApplyStandardSettingsAsync(_dataGridView, Model_Application_Variables.User);
                    Service_DataGridView.ApplySmartNumericFormatting(_dataGridView);
                    
                    // Apply row coloring if Lifecycle view
                    if (isLifecycleView)
                    {
                        ApplyLifecycleColoring();
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    _dataGridView.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                _btnSearch.Enabled = true;
                _btnSearch.Text = "Search";
            }
        }

        private void ApplyLifecycleColoring()
        {
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                if (row.DataBoundItem is DataRowView drv)
                {
                    string type = drv["RowType"]?.ToString() ?? "";
                    switch (type)
                    {
                        case "receipt": row.DefaultCellStyle.BackColor = Color.FromArgb(26, 76, 46); break; // Dark Green
                        case "transfer-out": row.DefaultCellStyle.BackColor = Color.FromArgb(76, 58, 26); break; // Dark Brown/Orange
                        case "transfer-in": row.DefaultCellStyle.BackColor = Color.FromArgb(26, 46, 76); break; // Dark Blue
                        case "shipment": row.DefaultCellStyle.BackColor = Color.FromArgb(76, 26, 26); break; // Dark Red
                    }
                    
                    // Ensure text is readable on dark backgrounds
                    if (!string.IsNullOrEmpty(type))
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
        }

        private async void BtnExport_Click(object? sender, EventArgs e)
        {
            if (_dataGridView.DataSource is not DataTable dt || dt.Rows.Count == 0)
            {
                Service_ErrorHandler.ShowError("No data to export.");
                return;
            }

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Export to Excel",
                FileName = $"VisualAudit_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _btnExport.Enabled = false;
                    _btnExport.Text = "Exporting...";

                    var columnOrder = new List<string>();
                    foreach (DataGridViewColumn col in _dataGridView.Columns)
                    {
                        if (col.Visible) columnOrder.Add(col.Name);
                    }

                    var printJob = new Model_Print_Job(dt, columnOrder, columnOrder, "Visual Audit Export");
                    var result = await Helper_ExportManager.ExportToExcelAsync(printJob, saveFileDialog.FileName);

                    if (result.IsSuccess)
                    {
                        Service_ErrorHandler.ShowInformation($"Export successful to {saveFileDialog.FileName}");
                    }
                    else
                    {
                        Service_ErrorHandler.ShowError($"Export failed: {result.ErrorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
                }
                finally
                {
                    _btnExport.Enabled = true;
                    _btnExport.Text = "Export to Excel";
                }
            }
        }

        private async Task LoadUsersForAnalyticsAsync()
        {
            if (_visualService == null) return;

            try
            {
                _btnLoadUsers.Enabled = false;
                _btnLoadUsers.Text = "Loading...";
                _clbUsers.Items.Clear();

                var start = _dtpAnalyticsStart.Value;
                var end = _dtpAnalyticsEnd.Value;

                var result = await _visualService.GetDistinctUsersForAnalyticsAsync(start, end);

                if (result.IsSuccess && result.Data != null)
                {
                    foreach (var user in result.Data)
                    {
                        _clbUsers.Items.Add(user);
                    }
                    UpdateUserSelectionState();
                    UpdateAnalyticsWorkflow();
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                _btnLoadUsers.Enabled = true;
                _btnLoadUsers.Text = "Load Users";
            }
        }

        private void UpdateUserSelectionState()
        {
            int count = _clbUsers.CheckedItems.Count;
            _lblUserCount.Text = $"Selected: {count}";
            
            if (count > 10)
            {
                var userUiColors = Model_Application_Variables.UserUiColors;
                var colorWarning = userUiColors?.WarningColor;
                _lblUserCount.ForeColor = colorWarning ?? Color.OrangeRed;
                _lblUserCount.Text += " (Slow)";
                _btnGenerateReport.Enabled = true;
            }
            else
            {
                // _lblUserCount.ForeColor = Color.Black; // Removed to allow theme system to manage color
                _btnGenerateReport.Enabled = count > 0;
            }
        }

        private void SelectAllUsers()
        {
            bool allChecked = _clbUsers.CheckedItems.Count == _clbUsers.Items.Count;
            for (int i = 0; i < _clbUsers.Items.Count; i++)
            {
                _clbUsers.SetItemChecked(i, !allChecked);
            }
            UpdateUserSelectionState();
            UpdateAnalyticsWorkflow();
        }

        private async Task GenerateAnalyticsReportAsync()
        {
            if (_visualService == null) return;

            try
            {
                _btnGenerateReport.Enabled = false;
                _btnGenerateReport.Text = "Generating...";

                var selectedUsers = new List<string>();
                foreach (var item in _clbUsers.CheckedItems)
                {
                    selectedUsers.Add(item.ToString() ?? "");
                }

                var start = _dtpAnalyticsStart.Value;
                var end = _dtpAnalyticsEnd.Value;

                var result = await _visualService.GetUserAnalyticsDataAsync(start, end, selectedUsers);

                if (result.IsSuccess && result.Data != null)
                {
                    SetWorkflowStep(4, true); // Step 5 complete
                    
                    if (result.Data.Rows.Count == 0)
                    {
                        Service_ErrorHandler.ShowInformation("No data found for the selected users and date range.");
                    }
                    else
                    {
                        // Open the viewer form
                        var viewer = new MTM_WIP_Application_Winforms.Forms.Visual.Form_AnalyticsViewer(result.Data);
                        viewer.Show();
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                _btnGenerateReport.Enabled = true;
                _btnGenerateReport.Text = "Generate Report";
            }
        }
        #endregion
    }
}
