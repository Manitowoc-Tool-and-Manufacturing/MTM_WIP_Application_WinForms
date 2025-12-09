using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models.Enums;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    /// <summary>
    /// Provides inventory lookup functionality with filtering, export, and suggestion support.
    /// </summary>
    public partial class Control_VisualInventory : ThemedUserControl
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private DataTable? _cachedDataTable;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Control_VisualInventory"/> class.
        /// </summary>
        public Control_VisualInventory()
        {
            InitializeComponent();
            this.MinimumSize = new Size(600, 400);
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();

            InitializeSuggestionBoxes();
            WireUpEvents();

            // Initialize Nothing Found image state
            // if (Control_VisualInventory_Image_NothingFound != null)
            // {
            //     Control_VisualInventory_Image_NothingFound.Visible = false;
            // }
        }
        #endregion

        #region Methods
        private void InitializeSuggestionBoxes()
        {
            // Part Number
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.LabelText = "Part Number";
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.SuggestionDataSource = Enum_SuggestionDataSource.Infor_PartNumber;

            // Warehouse
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.LabelText = "Warehouse";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.SuggestionDataSource = Enum_SuggestionDataSource.Infor_Warehouse;

            // Location
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.LabelText = "Location";
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.SuggestionDataSource = Enum_SuggestionDataSource.Infor_Location;
        }

        private void WireUpEvents()
        {
            Control_VisualInventory_Button_Search.Click += async (s, e) => await PerformSearchAsync();
            Control_VisualInventory_Button_Export.Click += Control_VisualInventory_Button_Export_Click;
            
            // Enter key support
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.TextBox.KeyDown += async (s, e) => { if (e.KeyCode == Keys.Enter) await PerformSearchAsync(); };
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
                LoggingUtility.Log("[Control_VisualInventory] Search started");
                Control_VisualInventory_Button_Search.Enabled = false;
                Control_VisualInventory_Button_Search.Text = "Searching...";

                string partNumber = Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.Text?.Trim() ?? "";
                string warehouse = Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Text?.Trim() ?? "";
                string location = Control_VisualInventory_SuggestionTextBoxWithLabel_Location.Text?.Trim() ?? "";
                bool nonZeroOnly = Control_VisualInventory_CheckBox_NonZeroOnly.Checked;

                var result = await _visualService.GetInventoryAsync(partNumber, warehouse, location, nonZeroOnly);

                if (result.IsSuccess && result.Data != null)
                {
                    _cachedDataTable = result.Data;
                    Control_VisualInventory_DataGridView_Results.DataSource = _cachedDataTable;
                    await Service_DataGridView.ApplyStandardSettingsAsync(Control_VisualInventory_DataGridView_Results, Model_Application_Variables.User);
                    Service_DataGridView.ApplySmartNumericFormatting(Control_VisualInventory_DataGridView_Results);

                    // if (Control_VisualInventory_Image_NothingFound != null)
                    // {
                    //     Control_VisualInventory_Image_NothingFound.Visible = _cachedDataTable.Rows.Count == 0;
                    // }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    Control_VisualInventory_DataGridView_Results.DataSource = null;

                    // if (Control_VisualInventory_Image_NothingFound != null)
                    // {
                    //     Control_VisualInventory_Image_NothingFound.Visible = false;
                    // }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                Control_VisualInventory_Button_Search.Enabled = true;
                Control_VisualInventory_Button_Search.Text = "Search";
            }
        }

        /// <summary>
        /// Performs a search initiated from an external control.
        /// </summary>
        /// <param name="partNumber">The part number to search for.</param>
        public async Task PerformExternalSearchAsync(string partNumber)
        {
            Control_VisualInventory_SuggestionTextBoxWithLabel_PartNumber.Text = partNumber;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Warehouse.Text = string.Empty;
            Control_VisualInventory_SuggestionTextBoxWithLabel_Location.Text = string.Empty;
            Control_VisualInventory_CheckBox_NonZeroOnly.Checked = false;

            await PerformSearchAsync();
        }
        #endregion

        #region Events
        private async void Control_VisualInventory_Button_Export_Click(object? sender, EventArgs e)
        {
            if (Control_VisualInventory_DataGridView_Results.DataSource is not DataTable dt || dt.Rows.Count == 0)
            {
                Service_ErrorHandler.ShowError("No data to export.");
                return;
            }

            LoggingUtility.Log("[Control_VisualInventory] Export clicked");

            using var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Workbook|*.xlsx",
                Title = "Export to Excel",
                FileName = $"VisualInventory_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Control_VisualInventory_Button_Export.Enabled = false;
                    Control_VisualInventory_Button_Export.Text = "Exporting...";

                    var columnOrder = new List<string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        columnOrder.Add(col.ColumnName);
                    }

                    var printJob = new Model_Print_Job(dt, columnOrder, columnOrder, "Visual Inventory Export");
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
                    Control_VisualInventory_Button_Export.Enabled = true;
                    Control_VisualInventory_Button_Export.Text = "Export to Excel";
                }
            }
        }
        #endregion

    }
}
