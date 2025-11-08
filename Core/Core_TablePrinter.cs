using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core;

/// <summary>
/// Professional DataTable printer with watermark, theme colors, and headers
/// </summary>
public class Core_TablePrinter : IDisposable
{
    #region Fields

    private DataTable? _data;
    private List<string> _columnOrder = new();
    private List<string> _visibleColumns = new();
    private int _currentRow;
    private int _pageNumber;
    private string _title = string.Empty;

    private readonly Font _titleFont;
    private readonly Font _headerFont;
    private readonly Font _cellFont;
    private readonly Font _watermarkFont;
    private readonly Font _pageFont;
    private readonly Brush _titleBrush;
    private readonly Brush _headerBrush;
    private readonly Brush _cellBrush;
    private readonly Brush _watermarkBrush;
    private readonly Pen _gridPen;
    private readonly Color _headerBackColor;

    private readonly PrintDocument _printDocument;

    #endregion

    #region Properties

    /// <summary>
    /// Configured PrintDocument ready for printing
    /// </summary>
    public PrintDocument PrintDocument => _printDocument;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates new table printer with theme integration
    /// </summary>
    public Core_TablePrinter()
    {
        // Initialize fonts
        _titleFont = new Font("Segoe UI", 16, FontStyle.Bold);
        _headerFont = new Font("Segoe UI", 11, FontStyle.Bold);
        _cellFont = new Font("Segoe UI", 10, FontStyle.Regular);
        _watermarkFont = new Font("Segoe UI", 48, FontStyle.Bold);
        _pageFont = new Font("Segoe UI", 9, FontStyle.Regular);

        // Get theme colors from Model_Application_Variables
        var colors = Model_Application_Variables.UserUiColors;

        _titleBrush = new SolidBrush(colors?.DataGridHeaderForeColor ?? SystemColors.ControlText);
        _headerBrush = new SolidBrush(colors?.DataGridHeaderForeColor ?? SystemColors.ControlText);
        _cellBrush = new SolidBrush(colors?.DataGridForeColor ?? SystemColors.WindowText);
        _watermarkBrush = new SolidBrush(Color.FromArgb(30, SystemColors.ControlText));
        _gridPen = new Pen(colors?.DataGridGridColor ?? SystemColors.ControlDark);
        _headerBackColor = colors?.DataGridHeaderBackColor ?? SystemColors.Control;

        // Initialize PrintDocument
        _printDocument = new PrintDocument();
        _printDocument.PrintPage += PrintDocument_PrintPage;
    }

    #endregion

    #region Configuration

    /// <summary>
    /// Sets data and configuration for printing
    /// </summary>
    /// <param name="data">Data to print</param>
    /// <param name="columnOrder">Column display order</param>
    /// <param name="visibleColumns">Visible columns list</param>
    /// <param name="title">Print title</param>
    public void SetData(DataTable data, List<string> columnOrder, List<string> visibleColumns, string title)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(columnOrder);
        ArgumentNullException.ThrowIfNull(visibleColumns);

        _data = data;
        _columnOrder = columnOrder;
        _visibleColumns = visibleColumns;
        _title = title ?? "Data Print";
        _currentRow = 0;
        _pageNumber = 0;

        LoggingUtility.Log($"[Core_TablePrinter] Data set: {_data.Rows.Count} rows, {_visibleColumns.Count} visible columns, Title: {_title}");
    }

    #endregion

    #region Print Event Handlers

    private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (_data == null || e.Graphics == null)
        {
            e.HasMorePages = false;
            return;
        }

        try
        {
            _pageNumber++;
            
            // Calculate margins
            int leftMargin = e.MarginBounds.Left;
            int topMargin = e.MarginBounds.Top;
            int rightMargin = e.MarginBounds.Right;
            int bottomMargin = e.MarginBounds.Bottom;
            int printableWidth = rightMargin - leftMargin;
            int printableHeight = bottomMargin - topMargin;

            int yPosition = topMargin;

            // Draw title
            var titleSize = e.Graphics.MeasureString(_title, _titleFont);
            e.Graphics.DrawString(_title, _titleFont, _titleBrush, 
                leftMargin + (printableWidth - titleSize.Width) / 2, yPosition);
            yPosition += (int)titleSize.Height + 20;

            // Draw watermark
            DrawWatermark(e.Graphics, e.MarginBounds);

            // Calculate column widths
            int columnCount = _visibleColumns.Count;
            int columnWidth = printableWidth / Math.Max(columnCount, 1);

            // Draw header row with background
            var headerRect = new Rectangle(leftMargin, yPosition, printableWidth, 30);
            using (var headerBrush = new SolidBrush(_headerBackColor))
            {
                e.Graphics.FillRectangle(headerBrush, headerRect);
            }

            int xPosition = leftMargin;
            foreach (var columnName in _visibleColumns)
            {
                e.Graphics.DrawRectangle(_gridPen, xPosition, yPosition, columnWidth, 30);
                e.Graphics.DrawString(columnName, _headerFont, _headerBrush, 
                    new RectangleF(xPosition + 5, yPosition + 7, columnWidth - 10, 20));
                xPosition += columnWidth;
            }
            yPosition += 30;

            // Draw data rows
            int rowHeight = 25;
            while (_currentRow < _data.Rows.Count && (yPosition + rowHeight) < bottomMargin)
            {
                var row = _data.Rows[_currentRow];
                xPosition = leftMargin;

                foreach (var columnName in _visibleColumns)
                {
                    var cellValue = row[columnName]?.ToString() ?? string.Empty;
                    e.Graphics.DrawRectangle(_gridPen, xPosition, yPosition, columnWidth, rowHeight);
                    e.Graphics.DrawString(cellValue, _cellFont, _cellBrush,
                        new RectangleF(xPosition + 5, yPosition + 5, columnWidth - 10, rowHeight - 10));
                    xPosition += columnWidth;
                }

                yPosition += rowHeight;
                _currentRow++;
            }

            // Draw page number
            string pageText = $"Page {_pageNumber}";
            var pageSize = e.Graphics.MeasureString(pageText, _pageFont);
            e.Graphics.DrawString(pageText, _pageFont, _cellBrush,
                rightMargin - pageSize.Width, bottomMargin + 10);

            // Check if more pages needed
            e.HasMorePages = _currentRow < _data.Rows.Count;
            
            if (!e.HasMorePages)
            {
                LoggingUtility.Log($"[Core_TablePrinter] Printing complete: {_pageNumber} page(s), {_currentRow} rows printed");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            e.HasMorePages = false;
        }
    }

    #endregion

    #region Watermark

    /// <summary>
    /// Draws diagonal watermark on page
    /// </summary>
    private void DrawWatermark(Graphics graphics, Rectangle bounds)
    {
        string watermark = "MTM WIP Application";
        var size = graphics.MeasureString(watermark, _watermarkFont);

        // Save graphics state
        var state = graphics.Save();

        // Move to center and rotate
        graphics.TranslateTransform(
            bounds.Left + bounds.Width / 2,
            bounds.Top + bounds.Height / 2);
        graphics.RotateTransform(-45);

        // Draw watermark
        graphics.DrawString(watermark, _watermarkFont, _watermarkBrush,
            -size.Width / 2, -size.Height / 2);

        // Restore graphics state
        graphics.Restore(state);
    }

    #endregion

    #region Cleanup

    /// <summary>
    /// Disposes resources
    /// </summary>
    public void Dispose()
    {
        _printDocument?.Dispose();
        _titleFont?.Dispose();
        _headerFont?.Dispose();
        _cellFont?.Dispose();
        _watermarkFont?.Dispose();
        _pageFont?.Dispose();
        _titleBrush?.Dispose();
        _headerBrush?.Dispose();
        _cellBrush?.Dispose();
        _watermarkBrush?.Dispose();
        _gridPen?.Dispose();
    }

    #endregion
}
