using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    /// <summary>
    /// Control for tracking part transaction lifecycles and auditing inventory history.
    /// Supports multiple filter modes via tabs.
    /// </summary>
    public partial class Control_VisualPartLifecycle : ThemedUserControl
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private DataTable? _cachedDataTable;
        private TabControl _tabControl = null!;
        private DataGridView _dataGridView = null!;
        private Button _btnSearch = null!;
        private Button _btnExport = null!;
        
        // Tab Pages
        private TabPage _tabLifecycle = null!;
        private TabPage _tabByPart = null!;
        private TabPage _tabByUser = null!;
        private TabPage _tabByWO = null!;
        private TabPage _tabByCO = null!;
        private TabPage _tabByPO = null!;

        // Inputs
        private SuggestionTextBoxWithLabel _txtLifecyclePart = null!;
        private DateTimePicker _dtpLifecycleStart = null!;
        private DateTimePicker _dtpLifecycleEnd = null!;

        private SuggestionTextBoxWithLabel _txtByPartPart = null!;
        
        private SuggestionTextBoxWithLabel _txtByUserUser = null!;
        private DateTimePicker _dtpByUserStart = null!;
        private DateTimePicker _dtpByUserEnd = null!;

        private SuggestionTextBoxWithLabel _txtByWOWO = null!;
        private SuggestionTextBoxWithLabel _txtByCOCO = null!;
        private SuggestionTextBoxWithLabel _txtByPOPO = null!;

        #endregion

        #region Constructors
        public Control_VisualPartLifecycle()
        {
            // InitializeComponent(); // Not using designer
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            InitializeControls();
            WireUpEvents();
        }
        #endregion

        #region Initialization
        private void InitializeControls()
        {
            // Main Layout
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F)); // Tabs/Inputs area
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));  // Buttons area
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // Grid area

            // Tab Control
            _tabControl = new TabControl { Dock = DockStyle.Fill };
            
            // Initialize Tabs
            _tabLifecycle = CreateTab("Lifecycle View", out var pnlLifecycle);
            _txtLifecyclePart = CreateInput(pnlLifecycle, "Part ID", 0, 0);
            CreateDateRange(pnlLifecycle, out _dtpLifecycleStart, out _dtpLifecycleEnd, 0, 1);

            _tabByPart = CreateTab("By Part Number", out var pnlByPart);
            _txtByPartPart = CreateInput(pnlByPart, "Part ID", 0, 0);

            _tabByUser = CreateTab("By User", out var pnlByUser);
            _txtByUserUser = CreateInput(pnlByUser, "User ID", 0, 0);
            CreateDateRange(pnlByUser, out _dtpByUserStart, out _dtpByUserEnd, 0, 1);

            _tabByWO = CreateTab("By Work Order", out var pnlByWO);
            _txtByWOWO = CreateInput(pnlByWO, "Work Order", 0, 0);

            _tabByCO = CreateTab("By Customer Order", out var pnlByCO);
            _txtByCOCO = CreateInput(pnlByCO, "Customer Order", 0, 0);

            _tabByPO = CreateTab("By PO Number", out var pnlByPO);
            _txtByPOPO = CreateInput(pnlByPO, "PO Number", 0, 0);

            _tabControl.TabPages.AddRange(new[] { _tabLifecycle, _tabByPart, _tabByUser, _tabByWO, _tabByCO, _tabByPO });

            // Buttons Panel
            var pnlButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(5)
            };
            _btnSearch = new Button { Text = "Search", AutoSize = true, Height = 30 };
            _btnExport = new Button { Text = "Export to Excel", AutoSize = true, Height = 30 };
            pnlButtons.Controls.Add(_btnSearch);
            pnlButtons.Controls.Add(_btnExport);

            // DataGridView
            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Add to Main Layout
            mainLayout.Controls.Add(_tabControl, 0, 0);
            mainLayout.Controls.Add(pnlButtons, 0, 1);
            mainLayout.Controls.Add(_dataGridView, 0, 2);

            this.Controls.Add(mainLayout);

            // Configure Suggestions
            Helper_SuggestionTextBox.ConfigureForPartNumbers(_txtLifecyclePart, Helper_SuggestionTextBox.GetCachedPartNumbersAsync);
            Helper_SuggestionTextBox.ConfigureForPartNumbers(_txtByPartPart, Helper_SuggestionTextBox.GetCachedPartNumbersAsync);
            Helper_SuggestionTextBox.ConfigureForUsers(_txtByUserUser, GetUsersAsync);
            Helper_SuggestionTextBox.ConfigureForWorkOrders(_txtByWOWO, GetWorkOrdersAsync);
            Helper_SuggestionTextBox.ConfigureForCustomerOrders(_txtByCOCO, GetCustomerOrdersAsync);
            Helper_SuggestionTextBox.ConfigureForPurchaseOrders(_txtByPOPO, GetPurchaseOrdersAsync);
        }

        private TabPage CreateTab(string title, out TableLayoutPanel panel)
        {
            var tab = new TabPage(title);
            panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2,
                Padding = new Padding(10)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tab.Controls.Add(panel);
            return tab;
        }

        private SuggestionTextBoxWithLabel CreateInput(TableLayoutPanel panel, string label, int col, int row)
        {
            var input = new SuggestionTextBoxWithLabel
            {
                LabelText = label,
                Dock = DockStyle.Top,
                EnableSuggestions = true
            };
            panel.Controls.Add(input, col, row);
            return input;
        }

        private void CreateDateRange(TableLayoutPanel panel, out DateTimePicker start, out DateTimePicker end, int col, int row)
        {
            var pnlDates = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
            
            var lblStart = new Label { Text = "From:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(0, 5, 5, 0) };
            start = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 100 };
            start.Value = DateTime.Today.AddDays(-30);

            var lblEnd = new Label { Text = "To:", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Margin = new Padding(10, 5, 5, 0) };
            end = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 100 };
            end.Value = DateTime.Today;

            pnlDates.Controls.Add(lblStart);
            pnlDates.Controls.Add(start);
            pnlDates.Controls.Add(lblEnd);
            pnlDates.Controls.Add(end);

            panel.Controls.Add(pnlDates, col, row);
        }

        private void WireUpEvents()
        {
            _btnSearch.Click += async (s, e) => await PerformSearchAsync();
            _btnExport.Click += BtnExport_Click;
            
            // Enter key support on inputs
            _txtLifecyclePart.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            _txtByPartPart.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            _txtByUserUser.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            _txtByWOWO.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            _txtByCOCO.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            _txtByPOPO.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            
            _dataGridView.CellDoubleClick += DataGridView_CellDoubleClick;
        }
        #endregion

        #region Methods
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
                    // Try to find Part ID column (could be PART_ID or Part Number depending on view)
                    string partId = "";
                    if (drv.Row.Table.Columns.Contains("PART_ID"))
                        partId = drv["PART_ID"]?.ToString() ?? "";
                    else if (drv.Row.Table.Columns.Contains("Part Number"))
                        partId = drv["Part Number"]?.ToString() ?? "";
                    
                    if (!string.IsNullOrEmpty(partId))
                    {
                        // Switch to Lifecycle tab
                        _tabControl.SelectedTab = _tabLifecycle;
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

                // Populate filter based on active tab
                if (_tabControl.SelectedTab == _tabLifecycle)
                {
                    filter.PartId = _txtLifecyclePart.Text?.Trim();
                    filter.StartDate = _dtpLifecycleStart.Value;
                    filter.EndDate = _dtpLifecycleEnd.Value;
                    isLifecycleView = true;

                    if (string.IsNullOrEmpty(filter.PartId))
                    {
                        Service_ErrorHandler.ShowUserError("Please enter a Part ID for Lifecycle View.");
                        return;
                    }
                }
                else if (_tabControl.SelectedTab == _tabByPart)
                {
                    filter.PartId = _txtByPartPart.Text?.Trim();
                    if (string.IsNullOrEmpty(filter.PartId))
                    {
                        Service_ErrorHandler.ShowUserError("Please enter a Part ID.");
                        return;
                    }
                }
                else if (_tabControl.SelectedTab == _tabByUser)
                {
                    filter.UserId = _txtByUserUser.Text?.Trim();
                    filter.StartDate = _dtpByUserStart.Value;
                    filter.EndDate = _dtpByUserEnd.Value;
                    if (string.IsNullOrEmpty(filter.UserId))
                    {
                        Service_ErrorHandler.ShowUserError("Please enter a User ID.");
                        return;
                    }
                }
                else if (_tabControl.SelectedTab == _tabByWO)
                {
                    filter.WorkOrder = _txtByWOWO.Text?.Trim();
                    if (string.IsNullOrEmpty(filter.WorkOrder))
                    {
                        Service_ErrorHandler.ShowUserError("Please enter a Work Order.");
                        return;
                    }
                }
                else if (_tabControl.SelectedTab == _tabByCO)
                {
                    filter.CustomerOrder = _txtByCOCO.Text?.Trim();
                    if (string.IsNullOrEmpty(filter.CustomerOrder))
                    {
                        Service_ErrorHandler.ShowUserError("Please enter a Customer Order.");
                        return;
                    }
                }
                else if (_tabControl.SelectedTab == _tabByPO)
                {
                    filter.PurchaseOrder = _txtByPOPO.Text?.Trim();
                    if (string.IsNullOrEmpty(filter.PurchaseOrder))
                    {
                        Service_ErrorHandler.ShowUserError("Please enter a PO Number.");
                        return;
                    }
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
        #endregion
    }
}
