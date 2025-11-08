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

    #region Constructors

    /// <summary>
    /// Creates new print manager for specified print job
    /// </summary>
    /// <param name="printJob">Print job configuration</param>
    public Helper_PrintManager(Model_Print_Job printJob)
    {
        _printJob = printJob ?? throw new ArgumentNullException(nameof(printJob));
        LoggingUtility.Log("[Helper_PrintManager] Print manager initialized");
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
            LoggingUtility.Log("[Helper_PrintManager] Preparing print document...");

            // Dispose existing printer if any
            _tablePrinter?.Dispose();

            // Create new table printer
            _tablePrinter = new Core_TablePrinter();
            
            // Set data (pass ALL data - PrintDocument.PrinterSettings will control pages)
            _tablePrinter.SetData(
                _printJob.Data,
                _printJob.ColumnOrder,
                _printJob.VisibleColumns,
                _printJob.Title);

            // Apply print job settings to PrintDocument
            _printJob.ApplyToPrintDocument(_tablePrinter.PrintDocument);

            LoggingUtility.Log($"[Helper_PrintManager] Print document prepared: {_printJob.Data.Rows.Count} rows, Printer: {_printJob.PrinterName ?? "Default"}");

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
            LoggingUtility.Log("[Helper_PrintManager] Starting print operation...");

            var printDocument = PreparePrintDocument();
            if (printDocument == null)
            {
                LoggingUtility.Log("[Helper_PrintManager] Print aborted - failed to prepare document");
                return false;
            }

            // Show print dialog
            using var printDialog = new PrintDialog
            {
                Document = printDocument,
                AllowSomePages = true,
                AllowSelection = false,
                UseEXDialog = true
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // User confirmed - start printing
                LoggingUtility.Log($"[Helper_PrintManager] User confirmed print: {printDialog.PrinterSettings.PrinterName}");
                printDocument.Print();
                LoggingUtility.Log("[Helper_PrintManager] Print operation initiated successfully");
                return true;
            }
            else
            {
                LoggingUtility.Log("[Helper_PrintManager] Print cancelled by user");
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

        LoggingUtility.Log("[Helper_PrintManager] Print manager disposed");
    }

    #endregion
}
