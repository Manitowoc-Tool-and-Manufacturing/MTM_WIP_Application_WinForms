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
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Helpers;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_VisualInventoryAudit : ThemedUserControl
    {
        private readonly IService_VisualDatabase? _visualService;
        private DataTable? _cachedDataTable;

        public Control_VisualInventoryAudit()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            InitializeControls();
            WireUpEvents();
        }

        private void InitializeControls()
        {
            // Date Pickers
            Control_VisualInventoryAudit_DateTimePicker_StartDate.Value = DateTime.Today.AddDays(-30);
            Control_VisualInventoryAudit_DateTimePicker_EndDate.Value = DateTime.Today;

            // User ID
            Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_User.LabelText = "User ID";
            Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_User.EnableSuggestions = true;
            // TODO: Add user provider if available

            // Transaction Type
            Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_Type.LabelText = "Trans Type";
            Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_Type.EnableSuggestions = true;
            // TODO: Add transaction type provider if available
        }

        private void WireUpEvents()
        {
            Control_VisualInventoryAudit_Button_Search.Click += async (s, e) => await PerformSearchAsync();
            Control_VisualInventoryAudit_Button_Export.Click += Control_VisualInventoryAudit_Button_Export_Click;
            
            // Enter key support
            Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_User.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_Type.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
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
                Control_VisualInventoryAudit_Button_Search.Enabled = false;
                Control_VisualInventoryAudit_Button_Search.Text = "Searching...";

                DateTime startDate = Control_VisualInventoryAudit_DateTimePicker_StartDate.Value;
                DateTime endDate = Control_VisualInventoryAudit_DateTimePicker_EndDate.Value;
                string userId = Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_User.Text?.Trim() ?? "";
                string transType = Control_VisualInventoryAudit_SuggestionTextBoxWithLabel_Type.Text?.Trim() ?? "";

                var result = await _visualService.GetInventoryAuditAsync(startDate, endDate, userId, transType);

                if (result.IsSuccess && result.Data != null)
                {
                    _cachedDataTable = result.Data;
                    Control_VisualInventoryAudit_DataGridView_Results.DataSource = _cachedDataTable;
                    await Service_DataGridView.ApplyStandardSettingsAsync(Control_VisualInventoryAudit_DataGridView_Results, Model_Application_Variables.User);
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    Control_VisualInventoryAudit_DataGridView_Results.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                Control_VisualInventoryAudit_Button_Search.Enabled = true;
                Control_VisualInventoryAudit_Button_Search.Text = "Search";
            }
        }

        private async void Control_VisualInventoryAudit_Button_Export_Click(object? sender, EventArgs e)
        {
            if (Control_VisualInventoryAudit_DataGridView_Results.DataSource is not DataTable dt || dt.Rows.Count == 0)
            {
                Service_ErrorHandler.ShowError("No data to export.");
                return;
            }

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Export to Excel",
                FileName = $"VisualInventoryAudit_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Control_VisualInventoryAudit_Button_Export.Enabled = false;
                    Control_VisualInventoryAudit_Button_Export.Text = "Exporting...";

                    var columnOrder = new List<string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        columnOrder.Add(col.ColumnName);
                    }

                    var printJob = new Model_Print_Job(dt, columnOrder, columnOrder, "Visual Inventory Audit Export");
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
                    Control_VisualInventoryAudit_Button_Export.Enabled = true;
                    Control_VisualInventoryAudit_Button_Export.Text = "Export to Excel";
                }
            }
        }
    }
}
