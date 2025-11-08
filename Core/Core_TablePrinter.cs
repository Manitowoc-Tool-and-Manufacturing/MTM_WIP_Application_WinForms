using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core;

/// <summary>
/// Professional DataTable printer with watermark, theme colors, and headers
/// </summary>
public class Core_TablePrinter
{
    private DataTable _data;
    private List<string> _columnOrder;
    private List<string> _visibleColumns;
    private int _currentRow;
    private int _pageNumber;
    private string _title;
    
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
    
    public PrintDocument? PrintDocument { get; private set; }
    
    public Core_TablePrinter()
    {
        _data = new DataTable();
        _columnOrder = new List<string>();
        _visibleColumns = new List<string>();
        _currentRow = 0;
        _pageNumber = 1;
        _title = "MTM WIP Application";
        
        // Use theme colors if available
        var themeColors = Model_AppVariables.UserUiColors;
        
        _titleFont = new Font("Arial", 14, FontStyle.Bold);
        _headerFont = new Font("Arial", 10, FontStyle.Bold);
        _cellFont = new Font("Arial", 9, FontStyle.Regular);
        _watermarkFont = new Font("Arial", 60, FontStyle.Bold);
        _pageFont = new Font("Arial", 8, FontStyle.Regular);
        
        _titleBrush = new SolidBrush(themeColors.DataGridHeaderForeColor ?? Color.Black);
        _headerBrush = new SolidBrush(themeColors.DataGridHeaderForeColor ?? Color.Black);
        _cellBrush = new SolidBrush(themeColors.DataGridForeColor ?? Color.Black);
        _watermarkBrush = new SolidBrush(Color.FromArgb(40, Color.Gray)); // Semi-transparent
        
        _headerBackColor = themeColors.DataGridHeaderBackColor ?? Color.LightGray;
        _gridPen = new Pen(themeColors.DataGridGridColor ?? Color.Gray, 1);
    }
    
    public void SetData(DataTable data, List<string> columnOrder, List<string> visibleColumns, string title = "MTM WIP Application")
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(columnOrder);
        ArgumentNullException.ThrowIfNull(visibleColumns);
        
        _data = data;
        _columnOrder = columnOrder;
        _visibleColumns = visibleColumns;
        _currentRow = 0;
        _pageNumber = 1;
        _title = title;
        
        PrintDocument?.Dispose();
        PrintDocument = new PrintDocument();
        PrintDocument.PrintPage += OnPrintPage;
        
        LoggingUtility.Log($"[TablePrinter] Data set: {_data.Rows.Count} rows, {_visibleColumns.Count} visible columns");
    }
    
    private void OnPrintPage(object? sender, PrintPageEventArgs e)
    {
        try
        {
            if (e.Graphics == null) return;
            
            var visibleCols = _columnOrder.Where(c => _visibleColumns.Contains(c)).ToList();
            
            if (visibleCols.Count == 0)
            {
                LoggingUtility.Log("[TablePrinter] No visible columns to print");
                e.HasMorePages = false;
                return;
            }
            
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;
            
            // Draw watermark (faint "MTM" in center of page)
            DrawWatermark(e.Graphics, e.PageBounds);
            
            // Draw header (title, date, page number)
            y = DrawHeader(e.Graphics, e.MarginBounds, ref _pageNumber) + 20;
            
            float pageWidth = e.MarginBounds.Width;
            float columnWidth = pageWidth / visibleCols.Count;
            float rowHeight = 25;
            
            // Draw column header row
            for (int i = 0; i < visibleCols.Count; i++)
            {
                var rect = new RectangleF(x + (i * columnWidth), y, columnWidth, rowHeight);
                e.Graphics.FillRectangle(new SolidBrush(_headerBackColor), rect);
                e.Graphics.DrawRectangle(_gridPen, rect.X, rect.Y, rect.Width, rect.Height);
                e.Graphics.DrawString(visibleCols[i], _headerFont, _headerBrush, rect, new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                });
            }
            
            y += rowHeight;
            
            // Draw data rows
            while (_currentRow < _data.Rows.Count)
            {
                if (y + rowHeight > e.MarginBounds.Bottom - 30) // Reserve space for footer
                {
                    e.HasMorePages = true;
                    _pageNumber++;
                    LoggingUtility.Log($"[TablePrinter] Page {_pageNumber - 1} complete, row {_currentRow}/{_data.Rows.Count}");
                    return;
                }
                
                var dataRow = _data.Rows[_currentRow];
                
                for (int i = 0; i < visibleCols.Count; i++)
                {
                    string columnName = visibleCols[i];
                    string value = string.Empty;
                    
                    if (_data.Columns.Contains(columnName))
                    {
                        value = dataRow[columnName]?.ToString() ?? string.Empty;
                    }
                    
                    var rect = new RectangleF(x + (i * columnWidth), y, columnWidth, rowHeight);
                    e.Graphics.DrawRectangle(_gridPen, rect.X, rect.Y, rect.Width, rect.Height);
                    e.Graphics.DrawString(value, _cellFont, _cellBrush, rect, new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.EllipsisCharacter
                    });
                }
                
                y += rowHeight;
                _currentRow++;
            }
            
            // Draw footer
            DrawFooter(e.Graphics, e.MarginBounds, _pageNumber);
            
            e.HasMorePages = false;
            LoggingUtility.Log($"[TablePrinter] Final page {_pageNumber} complete, total rows: {_currentRow}");
            
            _currentRow = 0;
            _pageNumber = 1;
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            e.HasMorePages = false;
        }
    }
    
    private void DrawWatermark(Graphics g, Rectangle pageBounds)
    {
        // Draw faint "MTM" watermark in center of page
        string watermarkText = "MTM";
        SizeF textSize = g.MeasureString(watermarkText, _watermarkFont);
        
        float x = (pageBounds.Width - textSize.Width) / 2;
        float y = (pageBounds.Height - textSize.Height) / 2;
        
        g.DrawString(watermarkText, _watermarkFont, _watermarkBrush, x, y);
    }
    
    private float DrawHeader(Graphics g, Rectangle marginBounds, ref int pageNumber)
    {
        float y = marginBounds.Top;
        
        // Draw title
        g.DrawString(_title, _titleFont, _titleBrush, marginBounds.Left, y);
        y += _titleFont.Height + 5;
        
        // Draw date and page number
        string dateStr = $"Printed: {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        string pageStr = $"Page {pageNumber}";
        string userName = Model_AppVariables.User ?? Environment.UserName;
        string userStr = $"User: {userName}";
        
        g.DrawString(dateStr, _pageFont, _cellBrush, marginBounds.Left, y);
        
        SizeF userSize = g.MeasureString(userStr, _pageFont);
        g.DrawString(userStr, _pageFont, _cellBrush, marginBounds.Right - userSize.Width, y);
        y += _pageFont.Height + 5;
        
        // Draw separator line
        g.DrawLine(_gridPen, marginBounds.Left, y, marginBounds.Right, y);
        
        return y + 5;
    }
    
    private void DrawFooter(Graphics g, Rectangle marginBounds, int pageNumber)
    {
        float y = marginBounds.Bottom + 5;
        
        // Draw separator line
        g.DrawLine(_gridPen, marginBounds.Left, y, marginBounds.Right, y);
        y += 5;
        
        // Draw page number centered
        string pageStr = $"Page {pageNumber}";
        SizeF pageSize = g.MeasureString(pageStr, _pageFont);
        float x = marginBounds.Left + (marginBounds.Width - pageSize.Width) / 2;
        g.DrawString(pageStr, _pageFont, _cellBrush, x, y);
    }
}
