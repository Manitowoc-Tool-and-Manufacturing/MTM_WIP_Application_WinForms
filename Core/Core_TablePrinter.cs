using System.Data;
using System.Drawing.Printing;
using MTM_WIP_Application_Winforms.Services.Logging;
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
    private readonly Brush _pageBackgroundBrush;
    private readonly Brush _rowBackgroundBrush;
    private readonly Pen _gridPen;
    private readonly Color _headerBackColor;

    private readonly PrintDocument _printDocument;
    private int _startRowIndex;
    private int _endRowIndexExclusive;
    private int _firstPageNumber;
    private bool _completionLogged;
    private bool _disposed;

    #endregion

    #region Properties

    /// <summary>
    /// Configured PrintDocument ready for printing
    /// </summary>
    public PrintDocument PrintDocument => _printDocument;

    /// <summary>
    /// Gets or sets whether the printer is rendering for a preview.
    /// If true, a white background is drawn to simulate paper.
    /// If false, the background is transparent (useful for pre-printed stationery).
    /// </summary>
    public bool IsPreview { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Creates new table printer with theme integration
    /// </summary>
    public Core_TablePrinter()
    {
        // Initialize fonts
        _titleFont = new Font("Segoe UI Emoji", 16, FontStyle.Bold);
        _headerFont = new Font("Segoe UI Emoji", 11, FontStyle.Bold);
        _cellFont = new Font("Segoe UI Emoji", 10, FontStyle.Regular);
        _watermarkFont = new Font("Segoe UI Emoji", 48, FontStyle.Bold);
        _pageFont = new Font("Segoe UI Emoji", 9, FontStyle.Regular);

        // Get theme colors from Model_Application_Variables
        var colors = Model_Application_Variables.UserUiColors;

        Color headerForeColor = EnsurePrintableColor(colors?.DataGridHeaderForeColor ?? SystemColors.ControlText);
        Color cellForeColor = EnsurePrintableColor(colors?.DataGridForeColor ?? SystemColors.WindowText);
        Color gridColor = EnsurePrintableColor(colors?.DataGridGridColor ?? SystemColors.ControlDark);
        Color rowBackColor = EnsureReadableBackgroundColor(colors?.DataGridBackColor ?? SystemColors.Window);
        Color headerBackColor = EnsureReadableBackgroundColor(colors?.DataGridHeaderBackColor ?? SystemColors.Control);

        _titleBrush = new SolidBrush(headerForeColor);
        _headerBrush = new SolidBrush(headerForeColor);
        _cellBrush = new SolidBrush(cellForeColor);
        _watermarkBrush = new SolidBrush(Color.FromArgb(30, SystemColors.ControlText));
        _pageBackgroundBrush = new SolidBrush(Color.White);
        _rowBackgroundBrush = new SolidBrush(rowBackColor);
        _gridPen = new Pen(gridColor);
        _headerBackColor = headerBackColor;

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


    }

    private void PrintDocument_PrintPage(object? sender, PrintPageEventArgs e)
    {
        if (_disposed || _data == null || e.Graphics == null)
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

            // Paint page background to ensure dark application themes do not influence printouts
            if (IsPreview)
            {
                e.Graphics.FillRectangle(_pageBackgroundBrush, e.PageBounds);
            }

            // Draw header logo (small watermark)
            Bitmap? logoImage = Properties.Resources.MTM;
            if (logoImage != null)
            {
                int logoHeight = 40;
                float scale = (float)logoHeight / logoImage.Height;
                int logoWidth = (int)(logoImage.Width * scale);
                
                // Draw logo at top left
                e.Graphics.DrawImage(logoImage, leftMargin, topMargin, logoWidth, logoHeight);
                
                // Adjust title position to be centered but respect the logo
                // Or just center the title on the page as before, assuming logo is small enough
            }

            // Draw title
            var titleSize = e.Graphics.MeasureString(_title, _titleFont);
            e.Graphics.DrawString(_title, _titleFont, _titleBrush,
                leftMargin + (printableWidth - titleSize.Width) / 2, yPosition);
            
            // Draw date/time on right
            string dateText = DateTime.Now.ToString("g");
            var dateSize = e.Graphics.MeasureString(dateText, _pageFont);
            e.Graphics.DrawString(dateText, _pageFont, _cellBrush, 
                rightMargin - dateSize.Width, yPosition + (titleSize.Height - dateSize.Height) / 2);

            yPosition += (int)titleSize.Height + 20;

            // Draw watermark
            DrawWatermark(e.Graphics, e.MarginBounds);

            // Calculate column widths
            int columnCount = _visibleColumns.Count;
            int notesWidth = 0;
            if (_printJob?.AddNotesColumn == true)
            {
                double percentage = Math.Clamp(_printJob.NotesColumnWidthPercentage, 5, 75) / 100.0;
                notesWidth = (int)(printableWidth * percentage);
            }
            int dataWidth = printableWidth - notesWidth;
            int columnWidth = dataWidth / Math.Max(columnCount, 1);

            // Draw header row with background
            var headerRect = new Rectangle(leftMargin, yPosition, printableWidth, 30);
            using (var headerBrush = new SolidBrush(_headerBackColor))
            {
                e.Graphics.FillRectangle(headerBrush, headerRect);
            }

            int xPosition = leftMargin;
            foreach (var columnName in _visibleColumns)
            {
                // Use user-friendly header if available, otherwise fallback to column name
                string headerText = columnName;
                if (_printJob?.ColumnHeaders.TryGetValue(columnName, out string? friendlyHeader) == true)
                {
                    headerText = friendlyHeader;
                }

                e.Graphics.DrawRectangle(_gridPen, xPosition, yPosition, columnWidth, 30);
                e.Graphics.DrawString(headerText, _headerFont, _headerBrush,
                    new RectangleF(xPosition + 5, yPosition + 7, columnWidth - 10, 20));
                xPosition += columnWidth;
            }

            if (notesWidth > 0)
            {
                e.Graphics.DrawRectangle(_gridPen, xPosition, yPosition, notesWidth, 30);
                e.Graphics.DrawString("Corrections", _headerFont, _headerBrush,
                    new RectangleF(xPosition + 5, yPosition + 7, notesWidth - 10, 20));
                xPosition += notesWidth;
            }
            yPosition += 30;

            // Draw data rows
            int rowHeight = 25;
            int pageStartRow = _currentRow;
            while (_currentRow < _endRowIndexExclusive && (yPosition + rowHeight) <= bottomMargin)
            {
                var row = _data.Rows[_currentRow];
                xPosition = leftMargin;

                // Determine row background color
                Color rowColor = Color.Empty;
                bool hasCustomColor = _printJob?.RowColors.TryGetValue(_currentRow, out rowColor) == true;

                foreach (var columnName in _visibleColumns)
                {
                    string cellValue = string.Empty;
                    if (row.Table?.Columns.Contains(columnName) == true)
                    {
                        cellValue = row[columnName]?.ToString() ?? string.Empty;
                    }
                    else
                    {
                        string trimmedName = columnName.Trim();
                        if (!string.Equals(trimmedName, columnName, StringComparison.Ordinal) && row.Table?.Columns.Contains(trimmedName) == true)
                        {
                            cellValue = row[trimmedName]?.ToString() ?? string.Empty;
                        }
                    }

                    Rectangle cellBounds = new Rectangle(xPosition, yPosition, columnWidth, rowHeight);
                    
                    if (hasCustomColor)
                    {
                        using var customBrush = new SolidBrush(EnsureReadableBackgroundColor(rowColor));
                        e.Graphics.FillRectangle(customBrush, cellBounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(_rowBackgroundBrush, cellBounds);
                    }

                    e.Graphics.DrawRectangle(_gridPen, cellBounds);
                    e.Graphics.DrawString(cellValue, _cellFont, _cellBrush,
                        new RectangleF(xPosition + 5, yPosition + 5, columnWidth - 10, rowHeight - 10));
                    xPosition += columnWidth;
                }

                if (notesWidth > 0)
                {
                    Rectangle cellBounds = new Rectangle(xPosition, yPosition, notesWidth, rowHeight);

                    if (hasCustomColor)
                    {
                        using var customBrush = new SolidBrush(EnsureReadableBackgroundColor(rowColor));
                        e.Graphics.FillRectangle(customBrush, cellBounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(_rowBackgroundBrush, cellBounds);
                    }

                    e.Graphics.DrawRectangle(_gridPen, cellBounds);
                    xPosition += notesWidth;
                }

                yPosition += rowHeight;
                _currentRow++;
            }

            // Draw page number
            int totalPages = _printJob?.TotalPages ?? 0;
            string pageText = totalPages > 0 
                ? $"Page {_pageNumber} of {totalPages}" 
                : $"Page {_pageNumber}";
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

    private static Color EnsureReadableBackgroundColor(Color color)
    {
        Color opaqueColor = Color.FromArgb(255, color);

        if (opaqueColor.GetBrightness() <= 0.35f)
        {
            // Blend with white to avoid near-black backgrounds that hide text ink.
            return BlendWithWhite(opaqueColor, 0.65f);
        }

        return opaqueColor;
    }

    private static Color BlendWithWhite(Color color, float whiteRatio)
    {
        whiteRatio = Math.Clamp(whiteRatio, 0f, 1f);
        int r = (int)Math.Round(color.R * (1 - whiteRatio) + 255 * whiteRatio);
        int g = (int)Math.Round(color.G * (1 - whiteRatio) + 255 * whiteRatio);
        int b = (int)Math.Round(color.B * (1 - whiteRatio) + 255 * whiteRatio);
        return Color.FromArgb(255, r, g, b);
    }

    #endregion

    #region Cleanup

    /// <summary>
    /// Disposes resources
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

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
        _pageBackgroundBrush?.Dispose();
        _rowBackgroundBrush?.Dispose();
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
