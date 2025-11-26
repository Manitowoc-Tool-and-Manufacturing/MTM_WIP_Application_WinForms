using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;

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
                    chkShowService.Checked
                );

                if (result.IsSuccess && result.Data != null)
                {
                    dataGridViewResults.DataSource = result.Data;
                    
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

        private void dataGridViewResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dataGridViewResults.Rows.Count) return;

            var row = dataGridViewResults.Rows[e.RowIndex];
            
            // Get values safely
            string poStatus = row.Cells["PO Status"].Value?.ToString() ?? "";
            string lineStatus = row.Cells["Line Status"].Value?.ToString() ?? "";
            
            decimal orderQty = 0;
            decimal receivedQty = 0;
            decimal.TryParse(row.Cells["Order Qty"].Value?.ToString(), out orderQty);
            decimal.TryParse(row.Cells["Received Qty"].Value?.ToString(), out receivedQty);

            DateTime? desiredDate = null;
            if (DateTime.TryParse(row.Cells["Line Desired Date"].Value?.ToString(), out DateTime dt))
            {
                desiredDate = dt;
            }
            else if (DateTime.TryParse(row.Cells["PO Desired Date"].Value?.ToString(), out DateTime dt2))
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
}
