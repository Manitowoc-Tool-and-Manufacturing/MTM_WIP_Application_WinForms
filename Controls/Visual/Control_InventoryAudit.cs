using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Analytics;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models.Analytics;
using MTM_WIP_Application_Winforms.Models.Enums;
using Newtonsoft.Json;
using MTM_WIP_Application_Winforms.Services.Logging;

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
        private readonly IDao_VisualAnalytics? _daoVisualAnalytics;
        private readonly IService_UserShiftLogic? _userShiftLogicService;
        private DataTable? _cachedDataTable;
        private string _lastSearchSelection = string.Empty;
        private Dictionary<string, int> _userShifts = new Dictionary<string, int>();
        private Dictionary<string, string> _userNames = new Dictionary<string, string>();
        #endregion

        #region Constructors
        public Control_InventoryAudit()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            _daoVisualAnalytics = Program.ServiceProvider?.GetService<IDao_VisualAnalytics>();
            _userShiftLogicService = Program.ServiceProvider?.GetService<IService_UserShiftLogic>();
            
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
            _txtLifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PartNumber;
        }

        private void InitializeDefaultValues()
        {
            // Lifecycle Defaults
            _rbLifecycleMonth.Checked = true;
            SetDateRange(_dtpLifecycleStart, _dtpLifecycleEnd, "Month");
            
            // Analytics Defaults
            _rbAnalyticsMonth.Checked = true;
            SetDateRange(_dtpAnalyticsStart, _dtpAnalyticsEnd, "Month");
            
            UpdateAnalyticsWorkflow();
        }

        private void WireUpEvents()
        {
            _btnSearch.Click += async (s, e) => await PerformSearchAsync();
            _btnExport.Click += BtnExport_Click;
            
            // Search By Selection Change
            _txtSearchBy.SuggestionSelected += (s, e) => OnSearchBySelected();
            _txtSearchBy.TextBox.Leave += (s, e) => OnSearchBySelected(); // Ensure update on leave if typed

            // Lifecycle Date Range Events
            _rbLifecycleToday.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();
            _rbLifecycleWeek.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();
            _rbLifecycleMonth.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();
            _rbLifecycleCustom.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();

            // Analytics Date Range Events
            _rbAnalyticsToday.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();
            _rbAnalyticsWeek.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();
            _rbAnalyticsMonth.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();
            _rbAnalyticsCustom.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();

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
        
        public void SelectUserAnalyticsTab()
        {
            _tabControl.SelectedTab = _tabUserAnalytics;
        }

        public void SelectLifecycleTab()
        {
            _tabControl.SelectedTab = _tabLifecycle;
        }

        private void OnSearchBySelected()
        {
            string selection = _txtSearchBy.Text ?? "";
            
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
                    _txtLifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_User;
                    break;
                case "Work Order":
                    _txtLifecyclePart.LabelText = "Enter Work Order";
                    _txtLifecyclePart.PlaceholderText = "Enter Work Order Number";
                    _txtLifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_WONumber;
                    break;
                case "Customer Order":
                    _txtLifecyclePart.LabelText = "Enter Customer Order";
                    _txtLifecyclePart.PlaceholderText = "Enter Customer Order Number";
                    _txtLifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_CONumber;
                    break;
                case "Purchase Order":
                    _txtLifecyclePart.LabelText = "Enter Purchase Order";
                    _txtLifecyclePart.PlaceholderText = "Enter Purchase Order Number";
                    _txtLifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PONumber;
                    break;
                case "Part Number":
                default:
                    _txtLifecyclePart.LabelText = "Enter Part ID";
                    _txtLifecyclePart.PlaceholderText = "Enter Part Number";
                    _txtLifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PartNumber;
                    break;
            }
        }

        private void OnLifecycleDateRangeChanged()
        {
            if (_rbLifecycleToday.Checked) SetDateRange(_dtpLifecycleStart, _dtpLifecycleEnd, "Today");
            else if (_rbLifecycleWeek.Checked) SetDateRange(_dtpLifecycleStart, _dtpLifecycleEnd, "Week");
            else if (_rbLifecycleMonth.Checked) SetDateRange(_dtpLifecycleStart, _dtpLifecycleEnd, "Month");
            else if (_rbLifecycleCustom.Checked) SetDateRange(_dtpLifecycleStart, _dtpLifecycleEnd, "Custom");
        }

        private void OnAnalyticsDateRangeChanged()
        {
            if (_rbAnalyticsToday.Checked) SetDateRange(_dtpAnalyticsStart, _dtpAnalyticsEnd, "Today");
            else if (_rbAnalyticsWeek.Checked) SetDateRange(_dtpAnalyticsStart, _dtpAnalyticsEnd, "Week");
            else if (_rbAnalyticsMonth.Checked) SetDateRange(_dtpAnalyticsStart, _dtpAnalyticsEnd, "Month");
            else if (_rbAnalyticsCustom.Checked) SetDateRange(_dtpAnalyticsStart, _dtpAnalyticsEnd, "Custom");
        }

        private void SetDateRange(DateTimePicker dtpStart, DateTimePicker dtpEnd, string rangeType)
        {
            bool isCustom = rangeType == "Custom";
            dtpStart.Enabled = isCustom;
            dtpEnd.Enabled = isCustom;

            if (isCustom) return;

            DateTime end = DateTime.Today.AddDays(1).AddSeconds(-1);
            DateTime start = DateTime.Today;

            switch (rangeType)
            {
                case "Today":
                    start = DateTime.Today;
                    break;
                case "Week":
                    start = DateTime.Today.AddDays(-7);
                    break;
                case "Month":
                    start = DateTime.Today.AddDays(-30);
                    break;
            }

            dtpStart.Value = start;
            dtpEnd.Value = end;
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
                LoggingUtility.Log("[Control_InventoryAudit] Search started");
                _btnSearch.Enabled = false;
                _btnSearch.Text = "Searching...";

                var filter = new Model_VisualTransactionFilter();
                bool isLifecycleView = false;

                // Populate filter based on active tab and search selection
                if (_tabControl.SelectedTab == _tabLifecycle)
                {
                    string searchBy = _txtSearchBy.Text ?? "Part Number";
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

            LoggingUtility.Log("[Control_InventoryAudit] Export clicked");

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

                // 1. Get Active Users from Visual
                var result = await _visualService.GetDistinctUsersForAnalyticsAsync(start, end);
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    return;
                }

                // 2. Get WIP Users for Shift Mapping
                var wipUsersResult = await Dao_User.GetAllUsersAsync();
                var wipUsers = new List<DataRow>();
                if (wipUsersResult.IsSuccess && wipUsersResult.Data != null)
                {
                    foreach (DataRow row in wipUsersResult.Data.Rows)
                    {
                        wipUsers.Add(row);
                    }
                }

                // 3. Get Shift/Name Metadata from sys_visual (Fallback)
                if (_daoVisualAnalytics != null)
                {
                    var metaResult = await _daoVisualAnalytics.GetSysVisualDataAsync();
                    if (metaResult.IsSuccess && metaResult.Data != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(metaResult.Data.JsonShiftData))
                                _userShifts = JsonConvert.DeserializeObject<Dictionary<string, int>>(metaResult.Data.JsonShiftData) ?? new Dictionary<string, int>();
                            
                            if (!string.IsNullOrEmpty(metaResult.Data.JsonUserFullNames))
                                _userNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(metaResult.Data.JsonUserFullNames) ?? new Dictionary<string, string>();
                        }
                        catch { /* Ignore JSON errors */ }
                    }
                }

                // 4. Filter and Populate
                var activeUsers = result.Data ?? new List<string>();
                var selectedShifts = new HashSet<int>();
                if (_cbShift1.Checked) selectedShifts.Add(1);
                if (_cbShift2.Checked) selectedShifts.Add(2);
                if (_cbShift3.Checked) selectedShifts.Add(3);
                if (_cbShiftWeekend.Checked) selectedShifts.Add(4);
                
                foreach (var userId in activeUsers)
                {
                    int shift = 0;

                    // Try to find shift from WIP Users first
                    var matchingWipUser = wipUsers.FirstOrDefault(r => IsUserMatch(userId, r["User"]?.ToString() ?? ""));
                    if (matchingWipUser != null)
                    {
                        string shiftStr = matchingWipUser["Shift"]?.ToString() ?? "";
                        if (int.TryParse(shiftStr, out int s)) shift = s;
                        else if (shiftStr.StartsWith("First")) shift = 1;
                        else if (shiftStr.StartsWith("Second")) shift = 2;
                        else if (shiftStr.StartsWith("Third")) shift = 3;
                        else if (shiftStr.StartsWith("Weekend")) shift = 4;
                    }

                    // Fallback to Visual Metadata if not found in WIP
                    if (shift == 0 && _userShifts.ContainsKey(userId))
                    {
                        shift = _userShifts[userId];
                    }
                    
                    // Filter by shift
                    // If shift is 0 (unknown), include it if Shift 1 is checked (fallback)
                    if (shift != 0 && !selectedShifts.Contains(shift)) continue;
                    if (shift == 0 && !_cbShift1.Checked) continue;

                    string displayName = userId;
                    if (_userNames.ContainsKey(userId))
                    {
                        displayName = $"{_userNames[userId]} ({userId})";
                    }
                    else if (matchingWipUser != null)
                    {
                        displayName = $"{matchingWipUser["Full Name"]} ({userId})";
                    }

                    _clbUsers.Items.Add(new UserDisplayItem { UserId = userId, DisplayName = displayName });
                }

                UpdateUserSelectionState();
                UpdateAnalyticsWorkflow();
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

        private bool IsUserMatch(string visualUser, string wipUser)
        {
            if (string.IsNullOrEmpty(visualUser) || visualUser.Length < 5) return false;
            
            char firstInitial = visualUser[0];
            string lastPart = visualUser.Substring(1); // First 4 of last name (assuming 5 chars total)
            
            // Check if WIP User starts with First Initial AND contains Last Part
            // Example: MSAMZ (Visual) vs MIKESAMZ (WIP)
            // M matches M
            // SAMZ is in MIKESAMZ
            
            return wipUser.StartsWith(firstInitial.ToString(), StringComparison.OrdinalIgnoreCase) &&
                   wipUser.IndexOf(lastPart, StringComparison.OrdinalIgnoreCase) >= 0;
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
            if (_visualService == null || _userShiftLogicService == null) return;

            try
            {
                _btnGenerateReport.Enabled = false;
                _btnGenerateReport.Text = "Generating...";

                var selectedUsers = new HashSet<string>();
                foreach (var item in _clbUsers.CheckedItems)
                {
                    if (item is UserDisplayItem userItem)
                    {
                        selectedUsers.Add(userItem.UserId);
                    }
                    else
                    {
                        selectedUsers.Add(item?.ToString() ?? "");
                    }
                }

                var start = _dtpAnalyticsStart.Value;
                var end = _dtpAnalyticsEnd.Value;

                // Use the new scoring logic service
                var result = await _userShiftLogicService.CalculateMaterialHandlerScoresAsync(start, end);

                if (result.IsSuccess && result.Data != null)
                {
                    SetWorkflowStep(4, true); // Step 5 complete
                    
                    // Filter by selected users
                    var filteredData = result.Data
                        .Where(x => selectedUsers.Contains(x.UserName))
                        .ToList();

                    if (filteredData.Count == 0)
                    {
                        Service_ErrorHandler.ShowInformation("No data found for the selected users and date range.");
                    }
                    else
                    {
                        // Open the viewer form with the Enhanced HTML template
                        var viewer = new MTM_WIP_Application_Winforms.Forms.Visual.Form_AnalyticsViewer(
                            filteredData, 
                            "VisualUserAnalytics_Enhanced.html");
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

    public class UserDisplayItem
    {
        public string UserId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public override string ToString() => DisplayName;
    }
}
