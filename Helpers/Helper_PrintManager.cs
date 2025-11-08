using System.Drawing.Printing;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Manages print preview and printing using Core_TablePrinter
/// </summary>
public class Helper_PrintManager : IDisposable
{
    private readonly Model_PrintJob _printJob;
    private Core_TablePrinter? _tablePrinter;
    private bool _disposed;
    
    public Helper_PrintManager(Model_PrintJob printJob)
    {
        _printJob = printJob ?? throw new ArgumentNullException(nameof(printJob));
    }
    
    public PrintDocument? PreparePrintDocument()
    {
        try
        {
            _tablePrinter = new Core_TablePrinter();
            
            // DO NOT pre-filter data - pass ALL data to the printer
            // The PrintDocument.PrinterSettings (FromPage/ToPage) tells the printer which pages to output
            // Core_TablePrinter will render all pages, but only the requested pages get printed
            _tablePrinter.SetData(
                _printJob.Data,
                _printJob.ColumnOrder,
                _printJob.VisibleColumns,
                _printJob.Title ?? "MTM WIP Application");

            var printDoc = _tablePrinter.PrintDocument;
            if (printDoc == null)
            {
                LoggingUtility.Log("[PrintManager] Failed to create print document");
                return null;
            }

            // Apply print settings including page range
            _printJob.ApplyToPrintDocument(printDoc);
            
            LoggingUtility.Log($"[PrintManager] Print document prepared: {_printJob.Data.Rows.Count} rows, Page range: {_printJob.PageRangeType}");
            return printDoc;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return null;
        }
    }
    
    public bool Print()
    {
        try
        {
            var printDoc = PreparePrintDocument();
            if (printDoc == null) return false;

            using var printDialog = new PrintDialog 
            { 
                Document = printDoc,
                AllowSomePages = true,  // Enable page range selection
                AllowSelection = false   // Disable selection option (we don't support it)
            };
            
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                LoggingUtility.Log("[PrintManager] Print job sent to printer");
                return true;
            }
            
            return false;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        
        _tablePrinter?.PrintDocument?.Dispose();
        _tablePrinter = null;
        
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
