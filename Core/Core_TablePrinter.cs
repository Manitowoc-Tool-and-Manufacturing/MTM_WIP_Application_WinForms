using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
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
    private Model_Print_Job? _printJob;
    private readonly List<Model_Print_PageBoundary> _pageBoundaries = new();

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
    private int _startRowIndex;
    private int _endRowIndexExclusive;
    private int _firstPageNumber;
    private bool _completionLogged;

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

    Color headerForeColor = EnsurePrintableColor(colors?.DataGridHeaderForeColor ?? SystemColors.ControlText);
    Color cellForeColor = EnsurePrintableColor(colors?.DataGridForeColor ?? SystemColors.WindowText);
    Color gridColor = EnsurePrintableColor(colors?.DataGridGridColor ?? SystemColors.ControlDark);

    _titleBrush = new SolidBrush(headerForeColor);
    _headerBrush = new SolidBrush(headerForeColor);
    _cellBrush = new SolidBrush(cellForeColor);
    _watermarkBrush = new SolidBrush(Color.FromArgb(30, SystemColors.ControlText));
    _gridPen = new Pen(gridColor);
    _headerBackColor = colors?.DataGridHeaderBackColor ?? SystemColors.Control;

        // Initialize PrintDocument
        _printDocument = new PrintDocument();
        _printDocument.BeginPrint += PrintDocument_BeginPrint;
        _printDocument.PrintPage += PrintDocument_PrintPage;
    }

    #endregion

    #region Configuration

    /// <summary>
    /// Configures the printer with a full print job description.
    /// </summary>
    /// <param name="printJob">The print job to render.</param>
    public void SetData(Model_Print_Job printJob)
    {
        ArgumentNullException.ThrowIfNull(printJob);
        ArgumentNullException.ThrowIfNull(printJob.Data);
        ArgumentNullException.ThrowIfNull(printJob.ColumnOrder);
        ArgumentNullException.ThrowIfNull(printJob.VisibleColumns);

        _printJob = printJob;

        _data = printJob.Data ?? throw new ArgumentNullException(nameof(printJob.Data));
        _columnOrder = new List<string>(printJob.ColumnOrder);
        _visibleColumns = new List<string>(printJob.VisibleColumns);
        _title = printJob.Title ?? "Data Print";

        var boundarySnapshot = CapturePageBoundariesSnapshot(printJob);
    (_startRowIndex, _endRowIndexExclusive, _firstPageNumber, _) = CalculateRowWindow(printJob, boundarySnapshot);

        _currentRow = _startRowIndex;
        _pageNumber = _firstPageNumber - 1;
    _pageBoundaries.Clear();
    _completionLogged = false;

        LoggingUtility.Log(string.Format(
            "[Core_TablePrinter] Data prepared: RangeType={0}, FirstPage={1}, StartRow={2}, EndRowExclusive={3}, VisibleColumns={4}, TotalRows={5}, ExistingTotalPages={6}",
            printJob.PageRangeType,
            _firstPageNumber,
            _startRowIndex,
            _endRowIndexExclusive,
            _visibleColumns.Count,
            _data.Rows.Count,
            printJob.TotalPages));
    }

    private static IReadOnlyList<Model_Print_PageBoundary> CapturePageBoundariesSnapshot(Model_Print_Job printJob)
    {
        if (printJob.PageBoundaries is null || printJob.PageBoundaries.Count == 0)
        {
            return Array.Empty<Model_Print_PageBoundary>();
        }

        return printJob.PageBoundaries
            .Where(boundary => boundary is not null)
            .Select(boundary => boundary!.Clone())
            .OrderBy(boundary => boundary.PageNumber)
            .ToList();
    }

    private (int startRow, int endRowExclusive, int firstPage, int lastPage) CalculateRowWindow(
        Model_Print_Job printJob,
        IReadOnlyList<Model_Print_PageBoundary> boundaries)
    {
        int totalRows = _data?.Rows.Count ?? 0;
        if (totalRows <= 0)
        {
            return (0, 0, 1, 1);
        }

        int defaultTotalPages = Math.Max(printJob.TotalPages, 1);
        int defaultRowsPerPage = Math.Max(1, totalRows / defaultTotalPages);
        if (defaultRowsPerPage <= 0)
        {
            defaultRowsPerPage = Math.Max(1, totalRows);
        }

        int highestRecordedPage = boundaries.Count > 0
            ? Math.Max(1, boundaries.Max(boundary => boundary.PageNumber))
            : defaultTotalPages;

        int firstPage;
        int lastPage;

        if (printJob.PageRangeType == Enum_PrintRangeType.AllPages)
        {
            firstPage = 1;
            lastPage = Math.Max(firstPage, highestRecordedPage);
        }
        else
        {
            int firstRecordedPage = boundaries.Count > 0 ? boundaries[0].PageNumber : 1;
            int lastRecordedPage = boundaries.Count > 0 ? boundaries[boundaries.Count - 1].PageNumber : highestRecordedPage;
            firstPage = Math.Max(firstRecordedPage, 1);
            lastPage = Math.Max(lastRecordedPage, firstPage);
        }

        (int start, int end) ResolveRange(int requestedPage)
        {
            if (boundaries.Count > 0)
            {
                foreach (Model_Print_PageBoundary boundary in boundaries)
                {
                    if (boundary.PageNumber == requestedPage)
                    {
                        int start = Math.Clamp(boundary.StartRow, 0, totalRows);
                        int end = Math.Clamp(boundary.EndRow, start, totalRows);
                        return (start, end);
                    }
                }

                if (requestedPage < boundaries[0].PageNumber)
                {
                    return ComputeHeuristicRange(requestedPage);
                }

                Model_Print_PageBoundary lastBoundary = boundaries[boundaries.Count - 1];
                if (requestedPage > lastBoundary.PageNumber)
                {
                    int alignedStart = Math.Clamp(lastBoundary.EndRow, 0, totalRows);
                    return (alignedStart, totalRows);
                }
            }

            return ComputeHeuristicRange(requestedPage);
        }

        (int start, int end) ComputeHeuristicRange(int requestedPage)
        {
            int rowsPerPage = defaultRowsPerPage;
            int tentativeStart = Math.Clamp((requestedPage - 1) * rowsPerPage, 0, totalRows);
            int tentativeEnd = Math.Clamp(tentativeStart + rowsPerPage, tentativeStart, totalRows);
            return (tentativeStart, tentativeEnd);
        }

        switch (printJob.PageRangeType)
        {
            case Enum_PrintRangeType.CurrentPage:
            {
                int currentPage = ClampPageNumber(printJob.CurrentPage, firstPage, lastPage);
                (int start, int end) = ResolveRange(currentPage);
                return (start, end, currentPage, currentPage);
            }

            case Enum_PrintRangeType.PageRange:
            {
                int fromPage = ClampPageNumber(printJob.FromPage, firstPage, lastPage);
                int toPage = ClampPageNumber(printJob.ToPage, fromPage, lastPage);
                (int start, _) = ResolveRange(fromPage);
                (_, int end) = ResolveRange(toPage);
                start = Math.Clamp(start, 0, totalRows);
                end = Math.Clamp(end, start, totalRows);
                return (start, end, fromPage, toPage);
            }

            default:
                return (0, totalRows, firstPage, lastPage);
        }
    }

    private static int ClampPageNumber(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value;
    }

    #endregion

    #region Print Event Handlers

    private void PrintDocument_BeginPrint(object? sender, PrintEventArgs e)
    {
        // Reset state for new print job
        _currentRow = _startRowIndex;
        _pageNumber = _firstPageNumber - 1;
        _completionLogged = false;
        
        LoggingUtility.Log($"[Core_TablePrinter] BeginPrint: Reset to StartRow={_currentRow}, PageNumber will start at {_pageNumber + 1}");
    }

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
            int pageStartRow = _currentRow;
            while (_currentRow < _endRowIndexExclusive && (yPosition + rowHeight) <= bottomMargin)
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

            int pageEndRow = _currentRow;
            _pageBoundaries.Add(new Model_Print_PageBoundary
            {
                PageNumber = _pageNumber,
                StartRow = pageStartRow,
                EndRow = pageEndRow
            });

            if (_pageNumber == _firstPageNumber)
            {
                LoggingUtility.Log(string.Format(
                    "[Core_TablePrinter] PrintPage first page rendered: Page={0}, StartRow={1}, EndRow={2}, HasMore={3}",
                    _pageNumber,
                    pageStartRow,
                    pageEndRow,
                    _currentRow < _endRowIndexExclusive));
            }

            // Check if more pages needed
            e.HasMorePages = _currentRow < _endRowIndexExclusive;
            
            if (!e.HasMorePages && !_completionLogged)
            {
                LoggingUtility.Log($"[Core_TablePrinter] Printing complete: {_pageNumber} page(s), {_currentRow} rows printed");
                _printJob?.SetPageBoundaries(_pageBoundaries);
                _completionLogged = true;
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
        // Load MTM logo watermark
        Bitmap? watermarkImage = Properties.Resources.MTM;
        if (watermarkImage is null)
        {
            return;
        }

        // Save graphics state
        var state = graphics.Save();

        // Move to center and rotate -45 degrees (same angle as text was)
        graphics.TranslateTransform(
            bounds.Left + bounds.Width / 2,
            bounds.Top + bounds.Height / 2);
        graphics.RotateTransform(-45);

        // Scale image to reasonable size (30% of page width)
        float targetWidth = bounds.Width * 0.3f;
        float scale = targetWidth / watermarkImage.Width;
        int scaledWidth = (int)(watermarkImage.Width * scale);
        int scaledHeight = (int)(watermarkImage.Height * scale);

        // Draw watermark image centered and semi-transparent
        using var imageAttr = new System.Drawing.Imaging.ImageAttributes();
        var colorMatrix = new System.Drawing.Imaging.ColorMatrix
        {
            Matrix33 = 0.15f // 15% opacity for subtle watermark
        };
        imageAttr.SetColorMatrix(colorMatrix);

        graphics.DrawImage(watermarkImage,
            new Rectangle(-scaledWidth / 2, -scaledHeight / 2, scaledWidth, scaledHeight),
            0, 0, watermarkImage.Width, watermarkImage.Height,
            GraphicsUnit.Pixel, imageAttr);

        // Restore graphics state
        graphics.Restore(state);
    }

    #endregion

    #region Helpers

    private static Color EnsurePrintableColor(Color color)
    {
        // Force fully opaque color so printers render consistently.
        Color opaqueColor = Color.FromArgb(255, color);

        // If the color is very light (high brightness), switch to black for contrast on white paper.
        if (opaqueColor.GetBrightness() >= 0.8f)
        {
            return Color.Black;
        }

        return opaqueColor;
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
        _pageBoundaries.Clear();
    }

    /// <summary>
    /// Returns a snapshot of the page boundary data captured during the last render.
    /// </summary>
    public IReadOnlyList<Model_Print_PageBoundary> GetPageBoundariesSnapshot()
    {
        return _pageBoundaries
            .Select(boundary => boundary.Clone())
            .ToList();
    }

    #endregion
}
