using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;

using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_ReceivingAnalytics : ThemedUserControl
    {
        private readonly IService_VisualDatabase? _visualService;

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
                    txtVendorFilter.Text.Trim(),
                    txtPOFilter.Text.Trim(),
                    chkShowWithPartNumber.Checked
                );

                if (result.IsSuccess && result.Data != null)
                {
                    // Apply client-side filters for status (Late, OnTime, Partial)
                    // We do this here to avoid complex SQL logic for derived statuses
                    // and to allow quick toggling if we wanted to implement that later (though currently it requires search)
                    DataTable dt = result.Data;
                    
                    // If any of the status checkboxes are unchecked, we need to filter
                    if (!chkShowLate.Checked || !chkShowPartial.Checked || !chkShowOnTime.Checked)
                    {
                        // We'll use a list of rows to remove to avoid modifying collection while iterating
                        var rowsToRemove = new System.Collections.Generic.List<DataRow>();
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            string poStatus = row["PO Status"]?.ToString() ?? "";
                            string lineStatus = row["Line Status"]?.ToString() ?? "";
                            
                            decimal orderQty = 0;
                            decimal receivedQty = 0;
                            decimal.TryParse(row["Order Qty"]?.ToString(), out orderQty);
                            decimal.TryParse(row["Received Qty"]?.ToString(), out receivedQty);

                            DateTime? desiredDate = null;
                            if (DateTime.TryParse(row["Line Desired Date"]?.ToString(), out DateTime d1)) desiredDate = d1;
                            else if (DateTime.TryParse(row["PO Desired Date"]?.ToString(), out DateTime d2)) desiredDate = d2;

                            bool isClosed = poStatus == "C" || lineStatus == "C";
                            bool isLate = !isClosed && desiredDate.HasValue && desiredDate.Value.Date < DateTime.Today && receivedQty < orderQty;
                            bool isPartial = !isClosed && !isLate && receivedQty > 0 && receivedQty < orderQty;
                            bool isOnTime = !isClosed && !isLate && !isPartial; // Everything else (On Time or Open)

                            if (isLate && !chkShowLate.Checked) rowsToRemove.Add(row);
                            else if (isPartial && !chkShowPartial.Checked) rowsToRemove.Add(row);
                            else if (isOnTime && !chkShowOnTime.Checked) rowsToRemove.Add(row);
                        }

                        foreach (var row in rowsToRemove)
                        {
                            dt.Rows.Remove(row);
                        }
                    }

                    dataGridViewResults.DataSource = dt;
                    
                    // Format columns if needed
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
