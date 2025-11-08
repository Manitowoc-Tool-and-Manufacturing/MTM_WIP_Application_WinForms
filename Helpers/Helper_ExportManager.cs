using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using ClosedXML.Excel;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Static export methods for PDF, Excel, Image formats
/// </summary>
public static class Helper_ExportManager
{
    #region Constants

    private const int EstimatedRowsPerPage = 31; // Approximate for 11pt font

    #endregion

    #region PDF Export

    /// <summary>
    /// Exports data to PDF using Microsoft Print to PDF printer
    /// </summary>
    /// <param name="printJob">Print job configuration</param>
    /// <param name="filePath">Output PDF file path</param>
    /// <returns>True if export succeeded, false otherwise</returns>
    public static bool ExportToPdf(Model_Print_Job printJob, string filePath)
    {
        ArgumentNullException.ThrowIfNull(printJob);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        try
        {
            LoggingUtility.Log($"[Helper_ExportManager] Starting PDF export to: {filePath}");

            // Create table printer
            using var tablePrinter = new Core_TablePrinter();
            
            // Set data (pass ALL data - PrintDocument handles page ranges)
            tablePrinter.SetData(
                printJob.Data,
                printJob.ColumnOrder,
                printJob.VisibleColumns,
                printJob.Title);

            // Apply print job settings
            printJob.ApplyToPrintDocument(tablePrinter.PrintDocument);

            // Set printer to Microsoft Print to PDF
            tablePrinter.PrintDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            tablePrinter.PrintDocument.PrinterSettings.PrintToFile = true;
            tablePrinter.PrintDocument.PrinterSettings.PrintFileName = filePath;

            // Print to PDF
            tablePrinter.PrintDocument.Print();

            LoggingUtility.Log($"[Helper_ExportManager] PDF export completed: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["FilePath"] = filePath,
                    ["PrintJob.Title"] = printJob.Title
                },
                controlName: nameof(Helper_ExportManager));
            return false;
        }
    }

    #endregion

    #region Excel Export

    /// <summary>
    /// Exports data to Excel using ClosedXML
    /// </summary>
    /// <param name="printJob">Print job configuration</param>
    /// <param name="filePath">Output Excel file path</param>
    /// <returns>True if export succeeded, false otherwise</returns>
    public static bool ExportToExcel(Model_Print_Job printJob, string filePath)
    {
        ArgumentNullException.ThrowIfNull(printJob);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

        try
        {
            LoggingUtility.Log($"[Helper_ExportManager] Starting Excel export to: {filePath}");

            // Get filtered data (Excel needs sliced DataTable for page ranges)
            var dataToExport = printJob.GetFilteredData(EstimatedRowsPerPage);

            if (dataToExport.Rows.Count == 0)
            {
                Service_ErrorHandler.HandleValidationError("No data to export.", "Excel Export");
                return false;
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(printJob.Title);

            // Add headers (only visible columns in specified order)
            int col = 1;
            foreach (var columnName in printJob.VisibleColumns)
            {
                var headerCell = worksheet.Cell(1, col);
                headerCell.Value = columnName;
                headerCell.Style.Font.Bold = true;
                headerCell.Style.Fill.BackgroundColor = XLColor.LightGray;
                col++;
            }

            // Add data rows
            int row = 2;
            foreach (DataRow dataRow in dataToExport.Rows)
            {
                col = 1;
                foreach (var columnName in printJob.VisibleColumns)
                {
                    var cellValue = dataRow[columnName]?.ToString() ?? string.Empty;
                    worksheet.Cell(row, col).Value = cellValue;
                    col++;
                }
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Save workbook
            workbook.SaveAs(filePath);

            LoggingUtility.Log($"[Helper_ExportManager] Excel export completed: {filePath}, Rows: {dataToExport.Rows.Count}");
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["FilePath"] = filePath,
                    ["PrintJob.Title"] = printJob.Title,
                    ["RowCount"] = printJob.Data?.Rows.Count ?? 0
                },
                controlName: nameof(Helper_ExportManager));
            return false;
        }
    }

    #endregion

    #region Image Export

    /// <summary>
    /// Exports data to image using GDI+ rendering
    /// </summary>
    /// <param name="printJob">Print job configuration</param>
    /// <param name="filePath">Output image file path</param>
    /// <param name="format">Image format (PNG, JPEG, BMP)</param>
    /// <returns>True if export succeeded, false otherwise</returns>
    public static bool ExportToImage(Model_Print_Job printJob, string filePath, ImageFormat format)
    {
        ArgumentNullException.ThrowIfNull(printJob);
        ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
        ArgumentNullException.ThrowIfNull(format);

        try
        {
            LoggingUtility.Log($"[Helper_ExportManager] Starting image export to: {filePath}, Format: {format}");

            // Get filtered data (Image needs sliced DataTable for page ranges)
            var dataToExport = printJob.GetFilteredData(EstimatedRowsPerPage);

            if (dataToExport.Rows.Count == 0)
            {
                Service_ErrorHandler.HandleValidationError("No data to export.", "Image Export");
                return false;
            }

            // Create table printer with filtered data
            using var tablePrinter = new Core_TablePrinter();
            tablePrinter.SetData(
                dataToExport,
                printJob.ColumnOrder,
                printJob.VisibleColumns,
                printJob.Title);

            // Set orientation
            tablePrinter.PrintDocument.DefaultPageSettings.Landscape = printJob.Landscape;

            // Calculate image size based on page settings
            var pageSettings = tablePrinter.PrintDocument.DefaultPageSettings;
            int width = printJob.Landscape ? pageSettings.PaperSize.Height : pageSettings.PaperSize.Width;
            int height = printJob.Landscape ? pageSettings.PaperSize.Width : pageSettings.PaperSize.Height;

            // Create bitmap and graphics
            using var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            
            // High quality rendering
            graphics.Clear(Color.White);
            graphics.PageUnit = GraphicsUnit.Display;

            // Render print page
            var args = new PrintPageEventArgs(
                graphics,
                pageSettings.Bounds,
                pageSettings.Bounds,
                pageSettings);

            tablePrinter.PrintDocument.GetType()
                .GetMethod("OnPrintPage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?
                .Invoke(tablePrinter.PrintDocument, new object[] { args });

            // Save bitmap
            bitmap.Save(filePath, format);

            LoggingUtility.Log($"[Helper_ExportManager] Image export completed: {filePath}");
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                    ["FilePath"] = filePath,
                    ["ImageFormat"] = format.ToString(),
                    ["PrintJob.Title"] = printJob.Title
                },
                controlName: nameof(Helper_ExportManager));
            return false;
        }
    }

    #endregion
}
