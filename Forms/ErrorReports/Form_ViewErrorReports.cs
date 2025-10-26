using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_Inventory_Application.Controls.ErrorReports;
using MTM_Inventory_Application.Core;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;

namespace MTM_Inventory_Application.Forms.ErrorReports
{
    public partial class Form_ViewErrorReports : Form
    {
        #region Fields

        private bool _isInitialized;

        #endregion

        #region Properties

        internal Control_ErrorReportsGrid GridControl => controlErrorReportsGrid;

        #endregion

        #region Progress Control Methods

        // Reserved for future progress integration

        #endregion

        #region Constructors

        public Form_ViewErrorReports()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);
            WireUpEvents();
        }

        #endregion

        #region Key Processing

        private void WireUpEvents()
        {
            controlErrorReportsGrid.ReportSelected += ControlErrorReportsGrid_ReportSelected;
            controlErrorReportsGrid.SelectionChanged += ControlErrorReportsGrid_SelectionChanged;
            Shown += Form_ViewErrorReports_Shown;
            btnExportCsv.Click += BtnExportCsv_Click;
            btnExportExcel.Click += BtnExportExcel_Click;
            btnExportSelected.Click += BtnExportSelected_Click;
        }

        #endregion

        #region ComboBox & UI Events

        private void Form_ViewErrorReports_Shown(object? sender, EventArgs e)
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
        }

        private async void ControlErrorReportsGrid_ReportSelected(object? sender, int reportId)
        {
            await ShowErrorReportDetailsDialogAsync(reportId);
        }

        private void ControlErrorReportsGrid_SelectionChanged(object? sender, EventArgs e)
        {
            // Enable Export Selected button only when rows are selected
            btnExportSelected.Enabled = controlErrorReportsGrid.HasSelectedRows;
        }

        private async void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            await ExportReportsAsync(ExportFormat.Csv, selectedOnly: false);
        }

        private async void BtnExportExcel_Click(object? sender, EventArgs e)
        {
            await ExportReportsAsync(ExportFormat.Excel, selectedOnly: false);
        }

        private async void BtnExportSelected_Click(object? sender, EventArgs e)
        {
            await ExportSelectedReportsAsync();
        }

        #endregion

        #region Helpers

        private enum ExportFormat
        {
            Csv,
            Excel
        }

        /// <summary>
        /// Exports error reports to CSV or Excel format with user file selection dialog.
        /// </summary>
        /// <param name="format">The export format (CSV or Excel).</param>
        /// <param name="selectedOnly">If true, exports only selected rows; otherwise exports all filtered rows.</param>
        private async Task ExportReportsAsync(ExportFormat format, bool selectedOnly)
        {
            try
            {
                DataTable? dataTable = controlErrorReportsGrid.GetCurrentDataTable();
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "No data available to export. Please load error reports first.",
                        "Export Error");
                    return;
                }

                // Show SaveFileDialog
                using SaveFileDialog saveDialog = new()
                {
                    Filter = format == ExportFormat.Csv
                        ? "CSV Files (*.csv)|*.csv"
                        : "Excel Files (*.xlsx)|*.xlsx",
                    DefaultExt = format == ExportFormat.Csv ? "csv" : "xlsx",
                    FileName = $"ErrorReports_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Title = "Export Error Reports"
                };

                if (saveDialog.ShowDialog(this) != DialogResult.OK)
                {
                    return; // User cancelled
                }

                // Perform export
                bool success = format == ExportFormat.Csv
                    ? await Helper_ErrorReportExport.ExportToCsvAsync(dataTable, saveDialog.FileName)
                    : await Helper_ErrorReportExport.ExportToExcelAsync(dataTable, saveDialog.FileName);

                if (success)
                {
                    MessageBox.Show(
                        this,
                        $"Successfully exported {dataTable.Rows.Count} error report(s) to:\n{saveDialog.FileName}",
                        "Export Complete",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    Service_ErrorHandler.HandleValidationError(
                        "Failed to export error reports. Please check the log for details.",
                        "Export Failed");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    controlName: Name,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = $"Export to {format}",
                        ["SelectedOnly"] = selectedOnly
                    });
            }
        }

        /// <summary>
        /// Exports only the selected rows from the grid.
        /// Shows format selection dialog (CSV or Excel).
        /// </summary>
        private async Task ExportSelectedReportsAsync()
        {
            try
            {
                int[] selectedIndices = controlErrorReportsGrid.GetSelectedRowIndices();
                if (selectedIndices.Length == 0)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "No rows selected. Please select one or more error reports to export.",
                        "Export Selected");
                    return;
                }

                DataTable? sourceTable = controlErrorReportsGrid.GetCurrentDataTable();
                if (sourceTable == null)
                {
                    Service_ErrorHandler.HandleValidationError(
                        "No data available to export.",
                        "Export Error");
                    return;
                }

                // Ask user for format
                using Form promptForm = new()
                {
                    Text = "Export Format",
                    Size = new Size(300, 150),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                Label lblPrompt = new()
                {
                    Text = $"Export {selectedIndices.Length} selected report(s) as:",
                    AutoSize = true,
                    Location = new Point(20, 20)
                };

                Button btnCsv = new()
                {
                    Text = "CSV",
                    DialogResult = DialogResult.OK,
                    Location = new Point(50, 60),
                    Size = new Size(80, 30)
                };

                Button btnExcel = new()
                {
                    Text = "Excel",
                    DialogResult = DialogResult.Yes,
                    Location = new Point(150, 60),
                    Size = new Size(80, 30)
                };

                promptForm.Controls.AddRange(new Control[] { lblPrompt, btnCsv, btnExcel });
                DialogResult result = promptForm.ShowDialog(this);

                if (result == DialogResult.OK || result == DialogResult.Yes)
                {
                    ExportFormat format = result == DialogResult.OK ? ExportFormat.Csv : ExportFormat.Excel;

                    // Create filtered DataTable with selected rows
                    DataTable filteredTable = Helper_ErrorReportExport.CreateFilteredDataTable(
                        sourceTable, selectedIndices);

                    // Show SaveFileDialog
                    using SaveFileDialog saveDialog = new()
                    {
                        Filter = format == ExportFormat.Csv
                            ? "CSV Files (*.csv)|*.csv"
                            : "Excel Files (*.xlsx)|*.xlsx",
                        DefaultExt = format == ExportFormat.Csv ? "csv" : "xlsx",
                        FileName = $"ErrorReports_Selected_{DateTime.Now:yyyyMMdd_HHmmss}",
                        Title = "Export Selected Error Reports"
                    };

                    if (saveDialog.ShowDialog(this) != DialogResult.OK)
                    {
                        return; // User cancelled
                    }

                    // Perform export
                    bool success = format == ExportFormat.Csv
                        ? await Helper_ErrorReportExport.ExportToCsvAsync(filteredTable, saveDialog.FileName)
                        : await Helper_ErrorReportExport.ExportToExcelAsync(filteredTable, saveDialog.FileName);

                    if (success)
                    {
                        MessageBox.Show(
                            this,
                            $"Successfully exported {filteredTable.Rows.Count} selected error report(s) to:\n{saveDialog.FileName}",
                            "Export Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        Service_ErrorHandler.HandleValidationError(
                            "Failed to export selected error reports. Please check the log for details.",
                            "Export Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    controlName: Name,
                    contextData: new Dictionary<string, object>
                    {
                        ["Operation"] = "Export Selected",
                        ["SelectedCount"] = controlErrorReportsGrid.GetSelectedRowIndices().Length
                    });
            }
        }

        private async Task ShowErrorReportDetailsDialogAsync(int reportId)
        {
            try
            {
                using Form_ErrorReportDetailsDialog dialog = new(reportId);
                dialog.StatusChanged += async (s, e) =>
                {
                    await controlErrorReportsGrid.LoadReportsAsync(controlErrorReportsGrid.LastFilter);
                    controlErrorReportsGrid.SelectReportById(e.ReportId);
                };

                dialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: Name);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            controlErrorReportsGrid.ReportSelected -= ControlErrorReportsGrid_ReportSelected;
            controlErrorReportsGrid.SelectionChanged -= ControlErrorReportsGrid_SelectionChanged;
            Shown -= Form_ViewErrorReports_Shown;
            btnExportCsv.Click -= BtnExportCsv_Click;
            btnExportExcel.Click -= BtnExportExcel_Click;
            btnExportSelected.Click -= BtnExportSelected_Click;
            base.OnFormClosed(e);
        }

        #endregion

        #region Cleanup

        // Additional cleanup not required at this time

        #endregion
    }
}
