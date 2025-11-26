using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using System.Linq;
using System.Data;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Dashboard form for viewing Infor Visual ERP data.
    /// </summary>
    public partial class InforVisualDashboard : ThemedForm
    {
        private readonly IService_VisualDatabase? _visualService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InforVisualDashboard"/> class.
        /// </summary>
        public InforVisualDashboard()
        {
            InitializeComponent();
            // Resolve service
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            this.Load += InforVisualDashboard_Load;
        }

        private async void InforVisualDashboard_Load(object? sender, EventArgs e)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowError("Visual Database Service not available.");
                return;
            }

            try
            {
                var result = await _visualService.TestConnectionAsync();
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowError($"Connection to Visual ERP failed: {result.ErrorMessage}");
                    // Optionally close form or disable controls
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
        }

        private async void CategoryButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Enum_VisualDashboardCategory category)
            {
                await LoadCategoryDataAsync(category);
            }
        }

        private async Task LoadCategoryDataAsync(Enum_VisualDashboardCategory category)
        {
            if (_visualService == null) return;

            try
            {
                // UI State: Loading
                SetLoadingState(true);
                controlEmptyState.Visible = false;
                dataGridViewResults.Visible = false;
                btnExport.Visible = false;

                // Fetch Data
                var result = await _visualService.GetDashboardDataAsync(category);

                if (result.IsSuccess && result.Data != null)
                {
                    var dt = result.Data;

                    // Bind Data
                    dataGridViewResults.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewResults.Visible = true;
                        controlEmptyState.Visible = false;
                        btnExport.Visible = true;
                        // Auto-size columns
                        dataGridViewResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    }
                    else
                    {
                        dataGridViewResults.Visible = false;
                        controlEmptyState.Visible = true;
                        btnExport.Visible = false;
                        controlEmptyState.Message = "No records found for this category.";
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError($"Failed to load data: {result.ErrorMessage}");
                    controlEmptyState.Visible = true;
                    controlEmptyState.Message = "Error loading data.";
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
                controlEmptyState.Visible = true;
                controlEmptyState.Message = "An unexpected error occurred.";
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool isLoading)
        {
            labelLoading.Visible = isLoading;
            panelSidebar.Enabled = !isLoading;
            btnExport.Enabled = !isLoading;

            if (isLoading)
            {
                controlEmptyState.Visible = false;
                dataGridViewResults.Visible = false;
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewResults.DataSource is not DataTable dt || dt.Rows.Count == 0)
            {
                Service_ErrorHandler.ShowError("No data to export.");
                return;
            }

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Export to Excel",
                FileName = $"VisualDashboard_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SetLoadingState(true);
                    
                    var columnOrder = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
                    var printJob = new Model_Print_Job(dt, columnOrder, columnOrder, "Visual Dashboard Export");
                    
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
                    SetLoadingState(false);
                }
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewResults.DataSource is DataTable dt)
                {
                    string filterText = textBoxFilter.Text.Trim();
                    if (string.IsNullOrEmpty(filterText))
                    {
                        dt.DefaultView.RowFilter = string.Empty;
                    }
                    else
                    {
                        // Build a filter string that checks all string columns
                        var columns = dt.Columns.Cast<DataColumn>()
                            .Where(c => c.DataType == typeof(string))
                            .Select(c => $"[{c.ColumnName}] LIKE '%{filterText.Replace("'", "''")}%'");
                        
                        string rowFilter = string.Join(" OR ", columns);
                        dt.DefaultView.RowFilter = rowFilter;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log but don't show error dialog for every keystroke
                LoggingUtility.LogApplicationError(ex);
            }
        }
    }
}
