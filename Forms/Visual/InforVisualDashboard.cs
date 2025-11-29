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
using System.Collections.Generic;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Dashboard form for viewing Infor Visual ERP data.
    /// </summary>
    public partial class InforVisualDashboard : ThemedForm
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private Control_DieToolDiscovery? _controlDieToolDiscovery;
        private Control_ReceivingAnalytics? _controlReceivingAnalytics;
        private Control_VisualInventory? _controlVisualInventory;
        #endregion

        #region Properties
        // Intentionally left blank.
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InforVisualDashboard"/> class.
        /// </summary>
        public InforVisualDashboard()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            Load += InforVisualDashboard_Load;
        }
        #endregion

        #region Methods
        private async Task LoadCategoryDataAsync(Enum_VisualDashboardCategory category)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                return;
            }

            if (category == Enum_VisualDashboardCategory.DieToolDiscovery)
            {
                ShowDieToolDiscoveryControl();
                return;
            }

            if (category == Enum_VisualDashboardCategory.Receiving)
            {
                ShowReceivingAnalyticsControl();
                return;
            }

            if (category == Enum_VisualDashboardCategory.Inventory)
            {
                ShowVisualInventoryControl();
                return;
            }

            HideCustomControls();

            try
            {
                SetLoadingState(true);
                controlEmptyState.Visible = false;
                dataGridViewResults.Visible = false;
                btnExport.Visible = false;
                labelTitle.Text = GetCategoryTitle(category);

                var result = await _visualService.GetDashboardDataAsync(category);
                if (result.IsSuccess && result.Data != null)
                {
                    dataGridViewResults.DataSource = result.Data;
                    controlEmptyState.Message = string.Empty;

                    if (result.Data.Rows.Count > 0)
                    {
                        dataGridViewResults.Visible = true;
                        btnExport.Visible = true;
                        dataGridViewResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                    }
                    else
                    {
                        controlEmptyState.Visible = true;
                        controlEmptyState.Message = "No records found for this category.";
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to load data: {result.ErrorMessage}");
                    controlEmptyState.Visible = true;
                    controlEmptyState.Message = "Error loading data.";
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Category"] = category.ToString()
                    },
                    callerName: nameof(LoadCategoryDataAsync),
                    controlName: Name);

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

            if (btnExport != null)
            {
                btnExport.Enabled = !isLoading;
                if (isLoading)
                {
                    btnExport.Visible = false;
                }
            }

            if (isLoading)
            {
                dataGridViewResults.Visible = false;
                controlEmptyState.Visible = false;
            }
        }
        #endregion

        #region Events
        private async void InforVisualDashboard_Load(object? sender, EventArgs e)
        {
            if (_visualService == null)
            {
                Service_ErrorHandler.ShowUserError("Visual Database Service is not available.");
                return;
            }

            try
            {
                var result = await _visualService.TestConnectionAsync();
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowUserError($"Connection to Visual ERP failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Stage"] = "TestConnection"
                    },
                    callerName: nameof(InforVisualDashboard_Load),
                    controlName: Name);
            }
        }

        private async void CategoryButton_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is Enum_VisualDashboardCategory category)
            {
                await LoadCategoryDataAsync(category);
            }
        }

        private async void btnExport_Click(object? sender, EventArgs e)
        {
            if (dataGridViewResults.DataSource is not DataTable dt || dt.Rows.Count == 0)
            {
                Service_ErrorHandler.ShowUserError("No data to export.");
                return;
            }

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Export to Excel",
                FileName = $"VisualDashboard_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

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
                    Service_ErrorHandler.ShowUserError($"Export failed: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["ExportPath"] = saveFileDialog.FileName
                    },
                    callerName: nameof(btnExport_Click),
                    controlName: Name);
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void textBoxFilter_TextChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dataGridViewResults.DataSource is DataTable dt)
                {
                    var filterText = textBoxFilter.Text.Trim();
                    if (string.IsNullOrEmpty(filterText))
                    {
                        dt.DefaultView.RowFilter = string.Empty;
                        return;
                    }

                    var columns = dt.Columns.Cast<DataColumn>()
                        .Where(c => c.DataType == typeof(string))
                        .Select(c => $"[{c.ColumnName}] LIKE '%{filterText.Replace("'", "''")}%'");

                    dt.DefaultView.RowFilter = string.Join(" OR ", columns);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }
        #endregion

        #region Helpers
        private void ShowDieToolDiscoveryControl()
        {
            HideGenericControls();
            EnsureDieToolDiscoveryControl();
            HideAllCustomControls(_controlDieToolDiscovery);
            _controlDieToolDiscovery!.Visible = true;
            labelTitle.Text = "Die Tool Discovery";
        }

        private void ShowReceivingAnalyticsControl()
        {
            HideGenericControls();
            EnsureReceivingAnalyticsControl();
            HideAllCustomControls(_controlReceivingAnalytics);
            _controlReceivingAnalytics!.Visible = true;
            labelTitle.Text = "Receiving Analytics";
        }

        private void ShowVisualInventoryControl()
        {
            HideGenericControls();
            EnsureVisualInventoryControl();
            HideAllCustomControls(_controlVisualInventory);
            _controlVisualInventory!.Visible = true;
            labelTitle.Text = "Inventory Search";
        }

        private void HideGenericControls()
        {
            dataGridViewResults.Visible = false;
            controlEmptyState.Visible = false;
            btnExport.Visible = false;
            textBoxFilter.Visible = false;
            labelFilter.Visible = false;
        }

        private void HideCustomControls()
        {
            HideAllCustomControls();
            textBoxFilter.Visible = true;
            labelFilter.Visible = true;
        }

        private void HideAllCustomControls(Control? exception = null)
        {
            if (_controlDieToolDiscovery != null)
            {
                _controlDieToolDiscovery.Visible = _controlDieToolDiscovery == exception;
            }

            if (_controlReceivingAnalytics != null)
            {
                _controlReceivingAnalytics.Visible = _controlReceivingAnalytics == exception;
            }

            if (_controlVisualInventory != null)
            {
                _controlVisualInventory.Visible = _controlVisualInventory == exception;
            }
        }

        private void EnsureDieToolDiscoveryControl()
        {
            if (_controlDieToolDiscovery != null)
            {
                return;
            }

            _controlDieToolDiscovery = new Control_DieToolDiscovery { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlDieToolDiscovery);
        }

        private void EnsureReceivingAnalyticsControl()
        {
            if (_controlReceivingAnalytics != null)
            {
                return;
            }

            _controlReceivingAnalytics = new Control_ReceivingAnalytics { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlReceivingAnalytics);
        }

        private void EnsureVisualInventoryControl()
        {
            if (_controlVisualInventory != null)
            {
                return;
            }

            _controlVisualInventory = new Control_VisualInventory { Dock = DockStyle.Fill };
            panelContent.Controls.Add(_controlVisualInventory);
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
        #endregion

        #region Cleanup / Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Load -= InforVisualDashboard_Load;
                _controlDieToolDiscovery?.Dispose();
                _controlReceivingAnalytics?.Dispose();
                _controlVisualInventory?.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}
