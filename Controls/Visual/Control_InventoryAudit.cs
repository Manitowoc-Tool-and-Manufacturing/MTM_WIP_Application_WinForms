using System.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Analytics;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models.Enums;
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
        private DataTable? _cachedDataTable;
        private string _lastSearchSelection = string.Empty;
        #endregion

        #region Constructors
        public Control_InventoryAudit()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService(typeof(IService_VisualDatabase)) as IService_VisualDatabase;

            // Initialize Search By options
            Control_InventoryAudit_SuggestionTextBox_SearchBy.TextBox.DataProvider = () => Task.FromResult(new List<string>
            {
                "Part Number",
                "User",
                "Work Order",
                "Customer Order",
                "Purchase Order"
            });
            Control_InventoryAudit_SuggestionTextBox_SearchBy.EnableSuggestions = true;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.ShowF4Button = true;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.Text = "Part Number"; // Default

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
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PartNumber;
        }

        private void InitializeDefaultValues()
        {
            // Lifecycle Defaults
            Control_InventoryAudit_RadioButton_Month.Checked = true;
            SetDateRange(Control_InventoryAudit_DateTimePicker_StartDate, Control_InventoryAudit_DateTimePicker_EndDate, "Month");
        }

        private void WireUpEvents()
        {
            Control_InventoryAudit_Button_Search.Click += async (s, e) => await PerformSearchAsync();
            Control_InventoryAudit_Button_Export.Click += BtnExport_Click;

            // Search By Selection Change
            Control_InventoryAudit_SuggestionTextBox_SearchBy.SuggestionSelected += (s, e) => OnSearchBySelected();
            Control_InventoryAudit_SuggestionTextBox_SearchBy.TextBox.Leave += (s, e) => OnSearchBySelected(); // Ensure update on leave if typed

            // Lifecycle Date Range Events
            Control_InventoryAudit_RadioButton_Today.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();
            Control_InventoryAudit_RadioButton_Week.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();
            Control_InventoryAudit_RadioButton_Month.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();
            Control_InventoryAudit_RadioButton_Custom.CheckedChanged += (s, e) => OnLifecycleDateRangeChanged();

            // Enter key support on inputs
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };

            Control_InventoryAudit_DataGridView_Results.CellDoubleClick += DataGridView_CellDoubleClick;
        }
        #endregion

        #region Methods

        private void OnSearchBySelected()
        {
            string selection = Control_InventoryAudit_SuggestionTextBox_SearchBy.Text ?? "";

            // Prevent unnecessary updates if selection hasn't changed
            if (string.Equals(selection, _lastSearchSelection, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            _lastSearchSelection = selection;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Text = string.Empty;

            switch (selection)
            {
                case "User":
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelText = "Enter User ID";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.PlaceholderText = "Enter User Name";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_User;
                    break;
                case "Work Order":
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelText = "Enter Work Order";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.PlaceholderText = "Enter Work Order Number";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_WONumber;
                    break;
                case "Customer Order":
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelText = "Enter Customer Order";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.PlaceholderText = "Enter Customer Order Number";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_CONumber;
                    break;
                case "Purchase Order":
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelText = "Enter Purchase Order";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.PlaceholderText = "Enter Purchase Order Number";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PONumber;
                    break;
                case "Part Number":
                default:
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelText = "Enter Part ID";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.PlaceholderText = "Enter Part Number";
                    Control_InventoryAudit_SuggestionTextBox_LifecyclePart.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PartNumber;
                    break;
            }
        }

        private void OnLifecycleDateRangeChanged()
        {
            if (Control_InventoryAudit_RadioButton_Today.Checked) SetDateRange(Control_InventoryAudit_DateTimePicker_StartDate, Control_InventoryAudit_DateTimePicker_EndDate, "Today");
            else if (Control_InventoryAudit_RadioButton_Week.Checked) SetDateRange(Control_InventoryAudit_DateTimePicker_StartDate, Control_InventoryAudit_DateTimePicker_EndDate, "Week");
            else if (Control_InventoryAudit_RadioButton_Month.Checked) SetDateRange(Control_InventoryAudit_DateTimePicker_StartDate, Control_InventoryAudit_DateTimePicker_EndDate, "Month");
            else if (Control_InventoryAudit_RadioButton_Custom.Checked) SetDateRange(Control_InventoryAudit_DateTimePicker_StartDate, Control_InventoryAudit_DateTimePicker_EndDate, "Custom");
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

        private async void DataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = Control_InventoryAudit_DataGridView_Results.Rows[e.RowIndex];
                if (row.DataBoundItem is DataRowView drv)
                {
                    // Check if we have a Trans ID
                    if (drv.Row.Table.Columns.Contains("Trans ID"))
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
                        // Switch to Part Number mode
                        Control_InventoryAudit_SuggestionTextBox_SearchBy.Text = "Part Number";
                        OnSearchBySelected();
                        Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Text = partId;

                        // Set date range to cover a broad history
                        Control_InventoryAudit_DateTimePicker_StartDate.Value = DateTime.Today.AddYears(-2);
                        Control_InventoryAudit_DateTimePicker_EndDate.Value = DateTime.Today;

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
                Control_InventoryAudit_Button_Search.Enabled = false;
                Control_InventoryAudit_Button_Search.Text = "Tracing...";

                // We need to fetch the raw transactions for the current part first
                // We can reuse the current filter settings
                var filter = new Model_VisualTransactionFilter
                {
                    PartId = Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Text?.Trim(),
                    StartDate = Control_InventoryAudit_DateTimePicker_StartDate.Value,
                    EndDate = Control_InventoryAudit_DateTimePicker_EndDate.Value
                };

                var result = await _visualService.GetTransactionsAsync(filter);

                if (result.IsSuccess && result.Data != null)
                {
                    // Apply Trace Logic
                    _cachedDataTable = Helper_VisualLifecycle.TraceTransactionFlow(result.Data, transId);

                    Control_InventoryAudit_DataGridView_Results.DataSource = _cachedDataTable;
                    await Service_DataGridView.ApplyStandardSettingsAsync(Control_InventoryAudit_DataGridView_Results, Model_Application_Variables.User);
                    Service_DataGridView.ApplySmartNumericFormatting(Control_InventoryAudit_DataGridView_Results);
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
                Control_InventoryAudit_Button_Search.Enabled = true;
                Control_InventoryAudit_Button_Search.Text = "Search";
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
                Control_InventoryAudit_Button_Search.Enabled = false;
                Control_InventoryAudit_Button_Search.Text = "Searching...";

                var filter = new Model_VisualTransactionFilter();
                bool isLifecycleView = false;

                string searchBy = Control_InventoryAudit_SuggestionTextBox_SearchBy.Text ?? "Part Number";
                string searchTerm = Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Text?.Trim() ?? "";

                filter.StartDate = Control_InventoryAudit_DateTimePicker_StartDate.Value;
                filter.EndDate = Control_InventoryAudit_DateTimePicker_EndDate.Value;

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

                    Control_InventoryAudit_DataGridView_Results.DataSource = _cachedDataTable;
                    await Service_DataGridView.ApplyStandardSettingsAsync(Control_InventoryAudit_DataGridView_Results, Model_Application_Variables.User);
                    Service_DataGridView.ApplySmartNumericFormatting(Control_InventoryAudit_DataGridView_Results);

                    // Apply row coloring if Lifecycle view
                    if (isLifecycleView)
                    {
                        ApplyLifecycleColoring();
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    Control_InventoryAudit_DataGridView_Results.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                Control_InventoryAudit_Button_Search.Enabled = true;
                Control_InventoryAudit_Button_Search.Text = "Search";
            }
        }

        private void ApplyLifecycleColoring()
        {
            foreach (DataGridViewRow row in Control_InventoryAudit_DataGridView_Results.Rows)
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
            if (Control_InventoryAudit_DataGridView_Results.DataSource is not DataTable dt || dt.Rows.Count == 0)
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
                    Control_InventoryAudit_Button_Export.Enabled = false;
                    Control_InventoryAudit_Button_Export.Text = "Exporting...";

                    var columnOrder = new List<string>();
                    foreach (DataGridViewColumn col in Control_InventoryAudit_DataGridView_Results.Columns)
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
                    Control_InventoryAudit_Button_Export.Enabled = true;
                    Control_InventoryAudit_Button_Export.Text = "Export to Excel";
                }
            }
        }
        #endregion
    }
}
