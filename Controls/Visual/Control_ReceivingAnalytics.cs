using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_ReceivingAnalytics : ThemedUserControl
    {
        private readonly IService_VisualDatabase? _visualService;
        private DataTable? _originalData;
        private bool _isPopulatingFilters;

        public Control_ReceivingAnalytics()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            // Set default dates to current week (Monday - Friday)
            SetCurrentWeek();

            // Set default combo selection
            cmbDateType.SelectedIndex = 4; // "Any of the Above"

            // Set default checkboxes
            chkIncludeClosed.Checked = false;
            chkShowConsignment.Checked = true;
            chkShowInternal.Checked = true;
            chkShowService.Checked = true;
            chkShowLate.Checked = true;
            chkShowPartial.Checked = true;
            chkShowOnTime.Checked = true;
            chkShowWithPartNumber.Checked = false;

            // Add context menu for column ordering
            var contextMenu = new ContextMenuStrip();
            var itemColumnOrder = new ToolStripMenuItem("Column Order...");
            itemColumnOrder.Click += ContextMenuItem_ColumnOrder_Click;
            contextMenu.Items.Add(itemColumnOrder);
            dataGridViewResults.ContextMenuStrip = contextMenu;

            // Performance: Handle coloring in DataBindingComplete instead of CellFormatting
            dataGridViewResults.CellFormatting -= dataGridViewResults_CellFormatting;
            dataGridViewResults.DataBindingComplete += (s, e) => ApplyRowColors();
            dataGridViewResults.Sorted += (s, e) => ApplyRowColors();

            // Double click to show PO details
            dataGridViewResults.CellDoubleClick += DataGridViewResults_CellDoubleClick;

            // Filter events
            cmbVendorFilter.SelectedIndexChanged += (s, e) => ApplyClientSideFilters();
            cmbPOFilter.SelectedIndexChanged += (s, e) => ApplyClientSideFilters();
            chkShowLate.CheckedChanged += (s, e) => ApplyClientSideFilters();
            chkShowPartial.CheckedChanged += (s, e) => ApplyClientSideFilters();
            chkShowOnTime.CheckedChanged += (s, e) => ApplyClientSideFilters();
            chkShowWithPartNumber.CheckedChanged += (s, e) => ApplyClientSideFilters();
        }

        private void SetCurrentWeek()
        {
            DateTime today = DateTime.Today;
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime monday = today.AddDays(-1 * diff).Date;
            DateTime friday = monday.AddDays(4).Date;

            dtpStartDate.Value = monday;
            dtpEndDate.Value = friday;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                return;
            }

            try
            {
                btnSearch.Enabled = false;
                btnSearch.Text = "Loading...";

                var result = await _visualService.GetReceivingScheduleAsync(
                    dtpStartDate.Value,
                    dtpEndDate.Value,
                    cmbDateType.SelectedItem?.ToString() ?? "Any of the Above",
                    chkIncludeClosed.Checked,
                    chkShowConsignment.Checked,
                    chkShowInternal.Checked,
                    chkShowService.Checked,
                    "", // Vendor filter handled client-side
                    "", // PO filter handled client-side
                    chkShowWithPartNumber.Checked
                );

                if (result.IsSuccess && result.Data != null)
                {
                    _originalData = result.Data;
                    PopulateFilters();
                    ApplyClientSideFilters();
                    
                    // Format columns if needed
                    if (dataGridViewResults.Columns["Order Date"] != null)
                        dataGridViewResults.Columns["Order Date"].DefaultCellStyle.Format = "d";
                    if (dataGridViewResults.Columns["Order Date"] != null)
                        dataGridViewResults.Columns["Order Date"].DefaultCellStyle.Format = "d";
                    if (dataGridViewResults.Columns["PO Desired Date"] != null)
                        dataGridViewResults.Columns["PO Desired Date"].DefaultCellStyle.Format = "d";
                    if (dataGridViewResults.Columns["PO Promise Date"] != null)
                        dataGridViewResults.Columns["PO Promise Date"].DefaultCellStyle.Format = "d";
                    if (dataGridViewResults.Columns["Line Desired Date"] != null)
                        dataGridViewResults.Columns["Line Desired Date"].DefaultCellStyle.Format = "d";
                    if (dataGridViewResults.Columns["Line Promise Date"] != null)
                        dataGridViewResults.Columns["Line Promise Date"].DefaultCellStyle.Format = "d";

                    // Apply standard settings (load user column preferences)
                    await Service_DataGridView.ApplyStandardSettingsAsync(dataGridViewResults, Model_Application_Variables.User);
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
            finally
            {
                btnSearch.Enabled = true;
                btnSearch.Text = "Search";
            }
        }

        private void ApplyRowColors()
        {
            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                if (row.DataBoundItem is not DataRowView drv) continue;

                // Get values safely from DataRowView (faster than Cells access)
                string poStatus = drv["PO Status"]?.ToString() ?? "";
                string lineStatus = drv["Line Status"]?.ToString() ?? "";
                
                decimal orderQty = 0;
                decimal receivedQty = 0;
                decimal.TryParse(drv["Order Qty"]?.ToString(), out orderQty);
                decimal.TryParse(drv["Received Qty"]?.ToString(), out receivedQty);

                DateTime? desiredDate = null;
                if (DateTime.TryParse(drv["Line Desired Date"]?.ToString(), out DateTime dt))
                {
                    desiredDate = dt;
                }
                else if (DateTime.TryParse(drv["PO Desired Date"]?.ToString(), out DateTime dt2))
                {
                    desiredDate = dt2;
                }

                // Logic for coloring
                Color backColor = Color.White;

                if (poStatus == "C" || lineStatus == "C")
                {
                    // Closed - Green
                    backColor = Color.FromArgb(200, 255, 200);
                }
                else if (desiredDate.HasValue && desiredDate.Value.Date < DateTime.Today && receivedQty < orderQty)
                {
                    // Late - Red
                    backColor = Color.FromArgb(255, 200, 200);
                }
                else if (receivedQty > 0 && receivedQty < orderQty)
                {
                    // Partial - Yellow
                    backColor = Color.FromArgb(255, 255, 200);
                }
                else
                {
                    // On Time / Open - Blue (Light)
                    backColor = Color.FromArgb(200, 240, 255);
                }

                row.DefaultCellStyle.BackColor = backColor;
            }
        }

        private void dataGridViewResults_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            // Logic moved to ApplyRowColors for performance
            // This method remains to satisfy the designer-generated code
        }

        private void PopulateFilters()
        {
            _isPopulatingFilters = true;

            try
            {
                PopulateCombo(cmbVendorFilter, "Vendor", "[ Enter Vendor ]");
                PopulateCombo(cmbPOFilter, "PO Number", "[ Enter PO # ]");
            }
            finally
            {
                _isPopulatingFilters = false;
            }
        }

        private void PopulateCombo(ComboBox comboBox, string columnName, string placeholder)
        {
            if (comboBox == null)
            {
                return;
            }

            comboBox.BeginUpdate();
            comboBox.Items.Clear();
            comboBox.Items.Add(placeholder);

            if (_originalData != null)
            {
                var values = _originalData.AsEnumerable()
                    .Select(row => row[columnName]?.ToString())
                    .Where(value => !string.IsNullOrWhiteSpace(value))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(value => value);

                foreach (var value in values)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        comboBox.Items.Add(value);
                    }
                }
            }

            comboBox.SelectedIndex = 0;
            comboBox.EndUpdate();
        }

        private void ApplyClientSideFilters()
        {
            if (_originalData == null || _isPopulatingFilters)
            {
                return;
            }

            var filteredTable = _originalData.Clone();

            foreach (DataRow row in _originalData.Rows)
            {
                if (ShouldIncludeRow(row))
                {
                    filteredTable.ImportRow(row);
                }
            }

            dataGridViewResults.DataSource = filteredTable;
        }

        private bool ShouldIncludeRow(DataRow row)
        {
            if (cmbVendorFilter != null && cmbVendorFilter.SelectedIndex > 0)
            {
                var selectedVendor = cmbVendorFilter.SelectedItem?.ToString();
                if (!string.Equals(row["Vendor"]?.ToString(), selectedVendor, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            if (cmbPOFilter != null && cmbPOFilter.SelectedIndex > 0)
            {
                var selectedPo = cmbPOFilter.SelectedItem?.ToString();
                if (!string.Equals(row["PO Number"]?.ToString(), selectedPo, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            if (chkShowWithPartNumber.Checked)
            {
                var partNumber = row["Part Number"]?.ToString();
                if (string.IsNullOrWhiteSpace(partNumber))
                {
                    return false;
                }
            }

            string poStatus = row["PO Status"]?.ToString() ?? string.Empty;
            string lineStatus = row["Line Status"]?.ToString() ?? string.Empty;

            decimal orderQty = 0;
            decimal receivedQty = 0;
            decimal.TryParse(row["Order Qty"]?.ToString(), out orderQty);
            decimal.TryParse(row["Received Qty"]?.ToString(), out receivedQty);

            var desiredDate = GetDesiredDate(row);

            bool isClosed = poStatus == "C" || lineStatus == "C";
            bool isLate = !isClosed && desiredDate.HasValue && desiredDate.Value.Date < DateTime.Today && receivedQty < orderQty;
            bool isPartial = !isClosed && !isLate && receivedQty > 0 && receivedQty < orderQty;
            bool isOnTime = !isClosed && !isLate && !isPartial;

            if (isLate && !chkShowLate.Checked)
            {
                return false;
            }

            if (isPartial && !chkShowPartial.Checked)
            {
                return false;
            }

            if (isOnTime && !chkShowOnTime.Checked)
            {
                return false;
            }

            return true;
        }

        private static DateTime? GetDesiredDate(DataRow row)
        {
            if (DateTime.TryParse(row["Line Desired Date"]?.ToString(), out DateTime lineDate))
            {
                return lineDate;
            }

            if (DateTime.TryParse(row["PO Desired Date"]?.ToString(), out DateTime poDate))
            {
                return poDate;
            }

            return null;
        }

        /// <summary>
        /// Applies the theme to the control and restores legend colors.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        protected override void ApplyTheme(Model_Shared_UserUiColors theme)
        {
            base.ApplyTheme(theme);

            // Restore legend colors that might have been overwritten by the theme
            if (panelLegendClosed != null) panelLegendClosed.BackColor = Color.FromArgb(200, 255, 200);
            if (panelLegendLate != null) panelLegendLate.BackColor = Color.FromArgb(255, 200, 200);
            if (panelLegendPartial != null) panelLegendPartial.BackColor = Color.FromArgb(255, 255, 200);
            if (panelLegendOnTime != null) panelLegendOnTime.BackColor = Color.FromArgb(200, 240, 255);
        }

        private void ContextMenuItem_ColumnOrder_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var dlg = new ColumnOrderDialog(dataGridViewResults))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        // Settings are saved within the dialog logic via Core_Themes.SaveGridSettingsAsync
                        // We just need to refresh if needed, but the dialog updates the grid directly
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: this.Name);
            }
        }

        private void DataGridViewResults_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dataGridViewResults.Rows[e.RowIndex];
                if (row.DataBoundItem is DataRowView drv)
                {
                    string poNumber = drv["PO Number"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(poNumber))
                    {
                        using (var detailsForm = new MTM_WIP_Application_Winforms.Forms.Visual.Form_PODetails(poNumber))
                        {
                            detailsForm.ShowDialog(this);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, controlName: this.Name);
            }
        }
    }
}
