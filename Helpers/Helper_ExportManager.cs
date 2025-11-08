using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using ClosedXML.Excel;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Handles exporting DataGridView data to various formats (PDF, Excel, Image)
/// </summary>
public static class Helper_ExportManager
{
    #region PDF Export
    
    /// <summary>
    /// Exports data to PDF using Microsoft Print to PDF printer
    /// </summary>
    public static bool ExportToPdf(Model_PrintJob printJob, string filePath)
    {
        try
        {
            // Find PDF printer
            string? pdfPrinter = FindPdfPrinter();
            if (string.IsNullOrEmpty(pdfPrinter))
            {
                MessageBox.Show(
                    "Microsoft Print to PDF printer not found. Please install it from Windows Features.",
                    "PDF Export",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            
            // Set PDF printer and file output
            printJob.PrinterName = pdfPrinter;
            
            // Create print manager (it will handle page range filtering)
            using (var printManager = new Helper_PrintManager(printJob))
            {
                var printDoc = printManager.PreparePrintDocument();
                if (printDoc == null)
                {
                    LoggingUtility.Log("[ExportManager] PDF export failed - could not prepare print document");
                    return false;
                }
                
                // Configure for file output
                printDoc.PrinterSettings.PrintToFile = true;
                printDoc.PrinterSettings.PrintFileName = filePath;
                
                // Print to file
                printDoc.Print();
                
                LoggingUtility.Log($"[ExportManager] PDF export completed: {filePath}");
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["FilePath"] = filePath });
            return false;
        }
    }
    
    private static string? FindPdfPrinter()
    {
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            if (printer.Contains("PDF", StringComparison.OrdinalIgnoreCase) ||
                printer.Contains("Microsoft Print To PDF", StringComparison.OrdinalIgnoreCase))
            {
                return printer;
            }
        }
        return null;
    }
    
    #endregion
    
    #region Excel Export
    
    /// <summary>
    /// Exports data to Excel file using ClosedXML
    /// Note: Page range is approximate (~31 rows/page) since Excel doesn't use the print system
    /// </summary>
    public static bool ExportToExcel(Model_PrintJob printJob, string filePath)
    {
        try
        {
            // Show warning if using page range (not exact like printing)
            if (printJob.PageRangeType != PrintRangeType.AllPages)
            {
                var result = MessageBox.Show(
                    "Excel export uses an approximate page range (~31 rows per page).\n\n" +
                    "For exact page ranges, use PDF export instead.\n\n" +
                    "Continue with Excel export?",
                    "Page Range Approximation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                    
                if (result != DialogResult.Yes)
                    return false;
            }
            
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");
                
                // Get visible columns in order
                var visibleCols = printJob.ColumnOrder.Where(c => printJob.VisibleColumns.Contains(c)).ToList();
                
                // Get filtered data based on page range
                var exportData = printJob.GetFilteredData();
                
                // Add headers
                for (int i = 0; i < visibleCols.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = visibleCols[i];
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                }
                
                // Add data rows
                for (int row = 0; row < exportData.Rows.Count; row++)
                {
                    var dataRow = exportData.Rows[row];
                    for (int col = 0; col < visibleCols.Count; col++)
                    {
                        string columnName = visibleCols[col];
                        if (exportData.Columns.Contains(columnName))
                        {
                            var value = dataRow[columnName];
                            worksheet.Cell(row + 2, col + 1).Value = value?.ToString() ?? string.Empty;
                        }
                    }
                }
                
                // Auto-fit columns
                worksheet.Columns().AdjustToContents();
                
                workbook.SaveAs(filePath);
                LoggingUtility.Log($"[ExportManager] Excel export completed: {filePath}");
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["FilePath"] = filePath });
            return false;
        }
    }
    
    #endregion
    
    #region Image Export
    
    /// <summary>
    /// Exports data to image (PNG/JPG) by rendering to bitmap
    /// Note: Page range is approximate (~31 rows/page) since image export doesn't use the print system
    /// </summary>
    public static bool ExportToImage(Model_PrintJob printJob, string filePath, ImageFormat format)
    {
        try
        {
            // Show warning if using page range (not exact like printing)
            if (printJob.PageRangeType != PrintRangeType.AllPages)
            {
                var result = MessageBox.Show(
                    "Image export uses an approximate page range (~31 rows per page).\n\n" +
                    "For exact page ranges, use PDF export instead.\n\n" +
                    "Continue with image export?",
                    "Page Range Approximation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                    
                if (result != DialogResult.Yes)
                    return false;
            }
            
            var visibleCols = printJob.ColumnOrder.Where(c => printJob.VisibleColumns.Contains(c)).ToList();
            
            // Get filtered data based on page range
            var exportData = printJob.GetFilteredData();
            
            // Calculate image size
            int cellWidth = 120;
            int cellHeight = 25;
            int imageWidth = visibleCols.Count * cellWidth;
            int imageHeight = (exportData.Rows.Count + 1) * cellHeight; // +1 for header
            
            using (var bitmap = new System.Drawing.Bitmap(imageWidth, imageHeight))
            using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.White);
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                
                var font = new System.Drawing.Font("Segoe UI", 9);
                var brush = System.Drawing.Brushes.Black;
                var headerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(240, 240, 240));
                
                // Draw header
                for (int col = 0; col < visibleCols.Count; col++)
                {
                    var rect = new System.Drawing.Rectangle(col * cellWidth, 0, cellWidth, cellHeight);
                    graphics.FillRectangle(headerBrush, rect);
                    graphics.DrawRectangle(System.Drawing.Pens.Gray, rect);
                    graphics.DrawString(visibleCols[col], font, brush, rect.X + 5, rect.Y + 5);
                }
                
                // Draw data rows
                for (int row = 0; row < exportData.Rows.Count; row++)
                {
                    var dataRow = exportData.Rows[row];
                    for (int col = 0; col < visibleCols.Count; col++)
                    {
                        string columnName = visibleCols[col];
                        var value = dataRow[columnName]?.ToString() ?? string.Empty;
                        var rect = new System.Drawing.Rectangle(
                            col * cellWidth,
                            (row + 1) * cellHeight,
                            cellWidth,
                            cellHeight);
                        graphics.DrawRectangle(System.Drawing.Pens.LightGray, rect);
                        graphics.DrawString(value, font, brush, rect.X + 5, rect.Y + 5);
                    }
                }
                
                bitmap.Save(filePath, format);
                LoggingUtility.Log($"[ExportManager] Image export completed: {filePath}");
                return true;
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["FilePath"] = filePath });
            return false;
        }
    }
    
    #endregion
    
    #region File Dialogs
    
    public static string? ShowPdfSaveDialog(string defaultFileName)
    {
        using (var dialog = new SaveFileDialog())
        {
            dialog.Filter = "PDF Files (*.pdf)|*.pdf";
            dialog.DefaultExt = "pdf";
            dialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
    }
    
    public static string? ShowExcelSaveDialog(string defaultFileName)
    {
        using (var dialog = new SaveFileDialog())
        {
            dialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            dialog.DefaultExt = "xlsx";
            dialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
    }
    
    public static string? ShowImageSaveDialog(string defaultFileName)
    {
        using (var dialog = new SaveFileDialog())
        {
            dialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg";
            dialog.DefaultExt = "png";
            dialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }
    }
    
    #endregion
}
