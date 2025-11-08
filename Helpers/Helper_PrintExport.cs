using System.Data;
using System.Drawing;
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
/// Helper class for exporting DataGridView data to various formats (PDF, Excel, Image)
/// </summary>
public static class Helper_PrintExport
{
    #region Excel Export

    /// <summary>
    /// Exports DataTable to Excel file using ClosedXML
    /// </summary>
    public static bool ExportToExcel(DataTable data, string filePath, List<string> columnOrder, List<string> visibleColumns)
    {
        try
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Data");

            // Add headers (only visible columns in specified order)
            int col = 1;
            var orderedVisibleColumns = columnOrder.Where(c => visibleColumns.Contains(c)).ToList();
            
            foreach (var columnName in orderedVisibleColumns)
            {
                worksheet.Cell(1, col).Value = columnName;
                worksheet.Cell(1, col).Style.Font.Bold = true;
                worksheet.Cell(1, col).Style.Fill.BackgroundColor = XLColor.LightGray;
                col++;
            }

            // Add data rows
            int row = 2;
            foreach (DataRow dataRow in data.Rows)
            {
                col = 1;
                foreach (var columnName in orderedVisibleColumns)
                {
                    if (data.Columns.Contains(columnName))
                    {
                        var value = dataRow[columnName];
                        if (value != null && value != DBNull.Value)
                        {
                            worksheet.Cell(row, col).Value = value.ToString();
                        }
                    }
                    col++;
                }
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(filePath);
            LoggingUtility.Log($"[PrintExport] Excel export successful: {filePath}");
            return true;
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
    /// Exports DataTable to image (PNG/JPG) by rendering to bitmap
    /// </summary>
    public static bool ExportToImage(DataTable data, string filePath,
        List<string> columnOrder, List<string> visibleColumns,
        ImageFormat format)
    {
        try
        {
            // Calculate image size based on data
            var orderedVisibleColumns = columnOrder.Where(c => visibleColumns.Contains(c)).ToList();
            int columnCount = orderedVisibleColumns.Count;
            int rowCount = data.Rows.Count + 1; // +1 for header

            int cellWidth = 120;
            int cellHeight = 25;
            int imageWidth = columnCount * cellWidth;
            int imageHeight = rowCount * cellHeight;

            using var bitmap = new Bitmap(imageWidth, imageHeight);
            using var graphics = Graphics.FromImage(bitmap);
            
            graphics.Clear(Color.White);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            // Draw header
            using var headerBrush = new SolidBrush(Color.LightGray);
            using var textBrush = new SolidBrush(Color.Black);
            using var font = new Font("Segoe UI", 9);
            using var pen = new Pen(Color.Black, 1);

            int x = 0;
            int y = 0;

            // Draw header row
            foreach (var columnName in orderedVisibleColumns)
            {
                graphics.FillRectangle(headerBrush, x, y, cellWidth, cellHeight);
                graphics.DrawRectangle(pen, x, y, cellWidth, cellHeight);
                graphics.DrawString(columnName, font, textBrush, x + 5, y + 5);
                x += cellWidth;
            }

            // Draw data rows
            y += cellHeight;
            foreach (DataRow dataRow in data.Rows)
            {
                x = 0;
                foreach (var columnName in orderedVisibleColumns)
                {
                    graphics.DrawRectangle(pen, x, y, cellWidth, cellHeight);
                    if (data.Columns.Contains(columnName))
                    {
                        var value = dataRow[columnName];
                        if (value != null && value != DBNull.Value)
                        {
                            string text = value.ToString() ?? string.Empty;
                            if (text.Length > 15)
                                text = text.Substring(0, 12) + "...";
                            graphics.DrawString(text, font, textBrush, x + 5, y + 5);
                        }
                    }
                    x += cellWidth;
                }
                y += cellHeight;
            }

            bitmap.Save(filePath, format);
            LoggingUtility.Log($"[PrintExport] Image export successful: {filePath}");
            return true;
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

    /// <summary>
    /// Shows save file dialog for Excel export
    /// </summary>
    public static string? ShowExcelSaveDialog(string defaultFileName = "Export")
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*",
            DefaultExt = "xlsx",
            FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
            Title = "Export to Excel"
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
    }

    /// <summary>
    /// Shows save file dialog for PDF export
    /// </summary>
    public static string? ShowPdfSaveDialog(string defaultFileName = "Export")
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*",
            DefaultExt = "pdf",
            FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
            Title = "Export to PDF"
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
    }

    /// <summary>
    /// Shows save file dialog for image export
    /// </summary>
    public static string? ShowImageSaveDialog(string defaultFileName = "Export")
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg)|*.jpg|All Files (*.*)|*.*",
            DefaultExt = "png",
            FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png",
            Title = "Export to Image"
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
    }

    #endregion
}
