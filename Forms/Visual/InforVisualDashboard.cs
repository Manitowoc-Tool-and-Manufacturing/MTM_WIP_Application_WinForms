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
using MTM_WIP_Application_Winforms.Controls.Visual;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Dashboard form for viewing Infor Visual ERP data.
    /// </summary>
    public partial class InforVisualDashboard : ThemedForm
    {
        private readonly IService_VisualDatabase? _visualService;
        private Control_DieToolDiscovery? _controlDieToolDiscovery;
        private Control_ReceivingAnalytics? _controlReceivingAnalytics;
        private Control_VisualInventory? _controlVisualInventory;
        private Control_VisualInventoryAudit? _controlVisualInventoryAudit;

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

            // Handle Die Tool Discovery specifically
            if (category == Enum_VisualDashboardCategory.DieToolDiscovery)
            {
                ShowDieToolDiscoveryControl();
                return;
            }
            else if (category == Enum_VisualDashboardCategory.Receiving)
            {
                ShowReceivingAnalyticsControl();
                return;
            }
            else if (category == Enum_VisualDashboardCategory.Inventory)
            {
                ShowVisualInventoryControl();
                return;
            }
            else if (category == Enum_VisualDashboardCategory.InventoryAuditing)
            {
                ShowVisualInventoryAuditControl();
                return;
            }
            else
            {
                HideCustomControls();
            }

            try
            {
                // UI State: Loading
                SetLoadingState(true);
                controlEmptyState.Visible = false;
                dataGridViewResults.Visible = false;
                btnExport.Visible = false;

                // Update Title
                labelTitle.Text = GetCategoryTitle(category);

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

        private void ShowDieToolDiscoveryControl()
        {
            // Hide generic controls
            dataGridViewResults.Visible = false;
            controlEmptyState.Visible = false;
            btnExport.Visible = false;
            textBoxFilter.Visible = false;
            labelFilter.Visible = false;

            // Initialize control if needed
            if (_controlDieToolDiscovery == null)
            {
                _controlDieToolDiscovery = new Control_DieToolDiscovery();
                _controlDieToolDiscovery.Dock = DockStyle.Fill;
                panelContent.Controls.Add(_controlDieToolDiscovery);
                _controlDieToolDiscovery.BringToFront();
            }

            _controlDieToolDiscovery.Visible = true;
            labelTitle.Text = "Die Tool Discovery";
        }

        private void ShowReceivingAnalyticsControl()
        {
            // Hide generic controls
            dataGridViewResults.Visible = false;
            controlEmptyState.Visible = false;
            btnExport.Visible = false;
            textBoxFilter.Visible = false;
            labelFilter.Visible = false;

            // Initialize control if needed
            if (_controlReceivingAnalytics == null)
            {
                _controlReceivingAnalytics = new Control_ReceivingAnalytics();
                _controlReceivingAnalytics.Dock = DockStyle.Fill;
                panelContent.Controls.Add(_controlReceivingAnalytics);
                _controlReceivingAnalytics.BringToFront();
            }

            _controlReceivingAnalytics.Visible = true;
            labelTitle.Text = "Receiving Analytics";
            
            // Hide other custom controls
            if (_controlDieToolDiscovery != null) _controlDieToolDiscovery.Visible = false;
            if (_controlVisualInventory != null) _controlVisualInventory.Visible = false;
            if (_controlVisualInventoryAudit != null) _controlVisualInventoryAudit.Visible = false;
        }

        private void ShowVisualInventoryControl()
        {
            // Hide generic controls
            dataGridViewResults.Visible = false;
            controlEmptyState.Visible = false;
            btnExport.Visible = false;
            textBoxFilter.Visible = false;
            labelFilter.Visible = false;

            // Initialize control if needed
            if (_controlVisualInventory == null)
            {
                _controlVisualInventory = new Control_VisualInventory();
                _controlVisualInventory.Dock = DockStyle.Fill;
                panelContent.Controls.Add(_controlVisualInventory);
                _controlVisualInventory.BringToFront();
            }

            _controlVisualInventory.Visible = true;
            labelTitle.Text = "Inventory Search";

            // Hide other custom controls
            if (_controlDieToolDiscovery != null) _controlDieToolDiscovery.Visible = false;
            if (_controlReceivingAnalytics != null) _controlReceivingAnalytics.Visible = false;
            if (_controlVisualInventoryAudit != null) _controlVisualInventoryAudit.Visible = false;
        }

        private void ShowVisualInventoryAuditControl()
        {
            // Hide generic controls
            dataGridViewResults.Visible = false;
            controlEmptyState.Visible = false;
            btnExport.Visible = false;
            textBoxFilter.Visible = false;
            labelFilter.Visible = false;

            // Initialize control if needed
            if (_controlVisualInventoryAudit == null)
            {
                _controlVisualInventoryAudit = new Control_VisualInventoryAudit();
                _controlVisualInventoryAudit.Dock = DockStyle.Fill;
                panelContent.Controls.Add(_controlVisualInventoryAudit);
                _controlVisualInventoryAudit.BringToFront();
            }

            _controlVisualInventoryAudit.Visible = true;
            labelTitle.Text = "Inventory Audit";

            // Hide other custom controls
            if (_controlDieToolDiscovery != null) _controlDieToolDiscovery.Visible = false;
            if (_controlReceivingAnalytics != null) _controlReceivingAnalytics.Visible = false;
            if (_controlVisualInventory != null) _controlVisualInventory.Visible = false;
        }

        private void HideCustomControls()
        {
            if (_controlDieToolDiscovery != null)
            {
                _controlDieToolDiscovery.Visible = false;
            }
            if (_controlReceivingAnalytics != null)
            {
                _controlReceivingAnalytics.Visible = false;
            }
            if (_controlVisualInventory != null)
            {
                _controlVisualInventory.Visible = false;
            }
            if (_controlVisualInventoryAudit != null)
            {
                _controlVisualInventoryAudit.Visible = false;
            }
            
            // Restore generic controls visibility
            textBoxFilter.Visible = true;
            labelFilter.Visible = true;
        }

        private string GetCategoryTitle(Enum_VisualDashboardCategory category)
        {
            return category switch
            {
                Enum_VisualDashboardCategory.Inventory => "Inventory",
                Enum_VisualDashboardCategory.Receiving => "Receiving",
                Enum_VisualDashboardCategory.Shipping => "Shipping",
                Enum_VisualDashboardCategory.InventoryAuditing => "Inventory Auditing",
                Enum_VisualDashboardCategory.DieToolDiscovery => "Die Tool Discovery",
                Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General => "MH Analytics (General)",
                Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team => "MH Analytics (Team)",
                _ => "Dashboard"
            };
        }
    }
}
