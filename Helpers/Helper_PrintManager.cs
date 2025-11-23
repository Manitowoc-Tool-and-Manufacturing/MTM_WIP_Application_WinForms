using System.Data;
using System.Drawing.Printing;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Manages print preview and printing using Core_TablePrinter
/// </summary>
public class Helper_PrintManager : IDisposable
{
    #region Fields

    private readonly Model_Print_Job _printJob;
    private Core_TablePrinter? _tablePrinter;
    private bool _disposed;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets whether the printer is rendering for a preview.
    /// </summary>
    public bool IsPreview { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates new print manager for specified print job
    /// </summary>
    /// <param name="printJob">Print job configuration</param>
    public Helper_PrintManager(Model_Print_Job printJob)
    {
        _printJob = printJob ?? throw new ArgumentNullException(nameof(printJob));
        
    }

    #endregion

    #region Print Operations

    /// <summary>
    /// Prepares PrintDocument for preview or printing
    /// </summary>
    /// <returns>Configured PrintDocument, or null if preparation failed</returns>
    public PrintDocument? PreparePrintDocument()
    {
        try
        {
            

            // Dispose existing printer if any
            _tablePrinter?.Dispose();

            // Create new table printer
            _tablePrinter = new Core_TablePrinter();
            _tablePrinter.IsPreview = IsPreview;
            
            // Set data (pass ALL data - PrintDocument.PrinterSettings will control pages)
            _tablePrinter.SetData(_printJob);

            // Apply print job settings to PrintDocument
            _printJob.ApplyToPrintDocument(_tablePrinter.PrintDocument);

            

            return _tablePrinter.PrintDocument;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["PrintJob.Title"] = _printJob.Title,
                    ["PrintJob.RowCount"] = _printJob.Data?.Rows.Count ?? 0
                },
                controlName: nameof(Helper_PrintManager));
            return null;
        }
    }

    /// <summary>
    /// Shows print dialog and executes print operation
    /// </summary>
    /// <returns>True if print was initiated, false if cancelled or failed</returns>
    public bool Print()
    {
        try
        {
            

            var printDocument = PreparePrintDocument();
            if (printDocument == null)
            {
                
                return false;
            }

            // Show print dialog
            using var printDialog = new PrintDialog
            {
                // UseEXDialog = true can cause issues with settings transfer on some systems.
                // Reverting to standard dialog to ensure settings (Landscape, Copies) are respected.
                UseEXDialog = false,
                AllowSomePages = true,
                AllowSelection = false
            };

            // CRITICAL: Explicitly assign Document AND PrinterSettings to ensure 
            // the dialog respects the pre-configured printer and orientation.
            printDialog.Document = printDocument;
            printDialog.PrinterSettings = printDocument.PrinterSettings;

            DialogResult result = printDialog.ShowDialog();
            LoggingUtility.Log($"[Helper_PrintManager] PrintDialog result: {result}");

            if (result == DialogResult.OK)
            {
                // Capture settings back to job so they can be persisted
                _printJob.PrinterName = printDocument.PrinterSettings.PrinterName;
                _printJob.Copies = printDocument.PrinterSettings.Copies;
                _printJob.Landscape = printDocument.DefaultPageSettings.Landscape;
                
                printDocument.Print();
                SyncPageBoundariesFromPrinter();
                
                return true;
            }
            else
            {
                
                return false;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["PrintJob.Title"] = _printJob.Title,
                    ["PrintJob.Printer"] = _printJob.PrinterName ?? "Default"
                },
                controlName: nameof(Helper_PrintManager));
            return false;
        }
    }

    #endregion

    #region Cleanup

    /// <summary>
    /// Disposes managed resources
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        _tablePrinter?.Dispose();
        _tablePrinter = null;

        _disposed = true;
        GC.SuppressFinalize(this);

        
    }

    /// <summary>
    /// Copies page boundary information from the active table printer into the print job.
    /// </summary>
    public void SyncPageBoundariesFromPrinter()
    {
        if (_tablePrinter == null)
        {
            _printJob.SetPageBoundaries(Array.Empty<Model_Print_PageBoundary>());
            return;
        }

        IReadOnlyList<Model_Print_PageBoundary> boundaries = _tablePrinter.GetPageBoundariesSnapshot();
        _printJob.SetPageBoundaries(boundaries);
        _printJob.TotalPages = Math.Max(boundaries.Count, 0);
    }

    #endregion

    #region Static Helpers (Create Print Job / Dialog)

    /// <summary>
    /// Attempts to extract a DataTable from a DataGridView handling three common scenarios:
    /// - DataSource is a DataTable
    /// - DataSource is a BindingSource wrapping a DataTable
    /// - Unbound grid (reads displayed cell values)
    /// </summary>
    public static DataTable GetDataTableFromGrid(DataGridView dgv)
    {
        if (dgv is null) throw new ArgumentNullException(nameof(dgv));

        // If bound to DataTable directly
        if (dgv.DataSource is DataTable dt)
        {
            return dt.Copy();
        }

        // If bound via BindingSource
        if (dgv.DataSource is BindingSource bs)
        {
            if (bs.DataSource is DataTable bdt)
            {
                return bdt.Copy();
            }
        }

        // Unbound grid - create DataTable from visible columns and rows
        var table = new DataTable(dgv.Name ?? "GridData");
        foreach (DataGridViewColumn col in dgv.Columns)
        {
            var colName = !string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.DataPropertyName : col.Name;
            if (!table.Columns.Contains(colName))
            {
                table.Columns.Add(colName);
            }
        }

        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow) continue;
            var newRow = table.NewRow();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                var colName = !string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.DataPropertyName : col.Name;
                try
                {
                    newRow[colName] = row.Cells[col.Index].Value ?? DBNull.Value;
                }
                catch
                {
                    newRow[colName] = DBNull.Value;
                }
            }

            table.Rows.Add(newRow);
        }

        return table;
    }

    /// <summary>
    /// Creates a <see cref="Model_Print_Job"/> from the DataGridView contents and column metadata.
    /// </summary>
    public static Model_Print_Job CreatePrintJob(DataGridView dgv, string title = "Print Job")
    {
        if (dgv is null) throw new ArgumentNullException(nameof(dgv));

        var data = GetDataTableFromGrid(dgv);
        var columnOrder = dgv.Columns.Cast<DataGridViewColumn>()
            .Select(c => !string.IsNullOrWhiteSpace(c.DataPropertyName) ? c.DataPropertyName : c.Name)
            .ToList();

        var visibleColumns = dgv.Columns.Cast<DataGridViewColumn>()
            .Where(c => c.Visible)
            .Select(c => !string.IsNullOrWhiteSpace(c.DataPropertyName) ? c.DataPropertyName : c.Name)
            .ToList();

        var job = new Model_Print_Job(data, columnOrder, visibleColumns, title);

        // Capture user-friendly headers
        foreach (DataGridViewColumn col in dgv.Columns)
        {
            string colKey = !string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.DataPropertyName : col.Name;
            if (!string.IsNullOrWhiteSpace(col.HeaderText))
            {
                job.ColumnHeaders[colKey] = col.HeaderText;
            }
        }

        // Capture row colors
        int rowIndex = 0;
        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow) continue;

            if (row.DefaultCellStyle.BackColor != Color.Empty &&
                row.DefaultCellStyle.BackColor != Color.Transparent &&
                row.DefaultCellStyle.BackColor != SystemColors.Window)
            {
                job.RowColors[rowIndex] = row.DefaultCellStyle.BackColor;
            }
            rowIndex++;
        }

        return job;
    }

    /// <summary>
    /// Shows the PrintForm dialog for the provided DataGridView. Loads persisted settings by gridName.
    /// </summary>
    public static Task<DialogResult> ShowPrintDialogAsync(Control parent, DataGridView dgv, string gridName)
    {
        if (parent is null) throw new ArgumentNullException(nameof(parent));
        if (dgv is null) throw new ArgumentNullException(nameof(dgv));
        if (string.IsNullOrWhiteSpace(gridName)) throw new ArgumentNullException(nameof(gridName));

        var printJob = CreatePrintJob(dgv, title: gridName);
        var settings = Model_Print_Settings.Load(gridName);

        // Apply persisted selections to print job where applicable
        if (settings.ColumnOrder?.Count > 0)
        {
            var merged = settings.ColumnOrder.Where(printJob.ColumnOrder.Contains).ToList();
            foreach (var col in printJob.ColumnOrder)
            {
                if (!merged.Contains(col)) merged.Add(col);
            }

            if (merged.Count == printJob.ColumnOrder.Count)
            {
                printJob.ColumnOrder = new List<string>(merged);
            }
        }

        if (settings.VisibleColumns?.Count > 0)
        {
            var visible = settings.VisibleColumns.Where(c => printJob.ColumnOrder.Contains(c)).ToList();
            if (visible.Count > 0) printJob.VisibleColumns = new List<string>(visible);
        }

        if (!string.IsNullOrWhiteSpace(settings.PrinterName))
        {
            printJob.PrinterName = settings.PrinterName;
        }

        var tcs = new TaskCompletionSource<DialogResult>();

        parent.BeginInvoke(new Action(() =>
        {
            using var form = new Forms.Shared.PrintForm(printJob, settings);
            var result = form.ShowDialog(parent);
            tcs.SetResult(result);
        }));

        return tcs.Task;
    }

    #endregion
}
