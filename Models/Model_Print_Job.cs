using System.Data;
using System.Drawing.Printing;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Complete print/export job configuration
/// </summary>
public class Model_Print_Job
{
    #region Fields

    private DataTable _data;
    private List<string> _columnOrder;
    private List<string> _visibleColumns;

    #endregion

    #region Properties

    /// <summary>
    /// Source data to print/export
    /// </summary>
    public DataTable Data
    {
        get => _data;
        set => _data = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Print job title
    /// </summary>
    public string Title { get; set; } = "Print Job";

    /// <summary>
    /// Column display order
    /// </summary>
    public List<string> ColumnOrder
    {
        get => _columnOrder;
        set => _columnOrder = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Visible columns list
    /// </summary>
    public List<string> VisibleColumns
    {
        get => _visibleColumns;
        set => _visibleColumns = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// Selected printer name
    /// </summary>
    public string? PrinterName { get; set; }

    /// <summary>
    /// Number of copies to print
    /// </summary>
    public int Copies { get; set; } = 1;

    /// <summary>
    /// Page orientation
    /// </summary>
    public bool Landscape { get; set; }

    /// <summary>
    /// Page range type
    /// </summary>
    public Enum_PrintRangeType PageRangeType { get; set; } = Enum_PrintRangeType.AllPages;

    /// <summary>
    /// Starting page number (1-based)
    /// </summary>
    public int FromPage { get; set; } = 1;

    /// <summary>
    /// Ending page number (1-based)
    /// </summary>
    public int ToPage { get; set; } = 1;

    /// <summary>
    /// Current page for CurrentPage mode
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates new print job with required data
    /// </summary>
    /// <param name="data">Source data table</param>
    /// <param name="columnOrder">Column display order</param>
    /// <param name="visibleColumns">Visible columns list</param>
    /// <param name="title">Print job title</param>
    public Model_Print_Job(DataTable data, List<string> columnOrder, List<string> visibleColumns, string title = "Print Job")
    {
        _data = data ?? throw new ArgumentNullException(nameof(data));
        _columnOrder = columnOrder ?? throw new ArgumentNullException(nameof(columnOrder));
        _visibleColumns = visibleColumns ?? throw new ArgumentNullException(nameof(visibleColumns));
        Title = title;
    }

    #endregion

    #region Data Filtering

    /// <summary>
    /// Gets filtered data based on page range type.
    /// Used for exports (Excel, Image) where we need to slice the DataTable.
    /// For printing, pass ALL data and let PrintDocument.PrinterSettings control pages.
    /// </summary>
    /// <param name="rowsPerPage">Estimated rows per page (default 31 for 11pt font)</param>
    /// <returns>Filtered DataTable</returns>
    public DataTable GetFilteredData(int rowsPerPage = 31)
    {
        if (PageRangeType == Enum_PrintRangeType.AllPages)
        {
            return Data.Copy();
        }

        if (PageRangeType == Enum_PrintRangeType.CurrentPage)
        {
            int startRow = (CurrentPage - 1) * rowsPerPage;
            int count = Math.Min(rowsPerPage, Data.Rows.Count - startRow);
            
            if (startRow >= Data.Rows.Count || count <= 0)
            {
                return Data.Clone(); // Return empty table with same schema
            }

            return SliceDataTable(Data, startRow, count);
        }

        // PageRange
        int fromRow = (FromPage - 1) * rowsPerPage;
        int toRow = ToPage * rowsPerPage;
        int totalCount = Math.Min(toRow - fromRow, Data.Rows.Count - fromRow);

        if (fromRow >= Data.Rows.Count || totalCount <= 0)
        {
            return Data.Clone();
        }

        return SliceDataTable(Data, fromRow, totalCount);
    }

    #endregion

    #region Print Configuration

    /// <summary>
    /// Applies print job settings to PrintDocument
    /// </summary>
    /// <param name="printDocument">PrintDocument to configure</param>
    public void ApplyToPrintDocument(PrintDocument printDocument)
    {
        ArgumentNullException.ThrowIfNull(printDocument);

        // Apply printer name if specified
        if (!string.IsNullOrWhiteSpace(PrinterName))
        {
            printDocument.PrinterSettings.PrinterName = PrinterName;
        }

        // Apply page orientation
        printDocument.DefaultPageSettings.Landscape = Landscape;

        // Apply copies
        printDocument.PrinterSettings.Copies = (short)Copies;

        // Apply page range
        switch (PageRangeType)
        {
            case Enum_PrintRangeType.AllPages:
                printDocument.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
                break;

            case Enum_PrintRangeType.CurrentPage:
                printDocument.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                printDocument.PrinterSettings.FromPage = CurrentPage;
                printDocument.PrinterSettings.ToPage = CurrentPage;
                break;

            case Enum_PrintRangeType.PageRange:
                printDocument.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.SomePages;
                printDocument.PrinterSettings.FromPage = FromPage;
                printDocument.PrinterSettings.ToPage = ToPage;
                break;
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Slices DataTable to specified row range
    /// </summary>
    private static DataTable SliceDataTable(DataTable source, int startRow, int count)
    {
        DataTable result = source.Clone();
        
        for (int i = 0; i < count && (startRow + i) < source.Rows.Count; i++)
        {
            result.ImportRow(source.Rows[startRow + i]);
        }

        return result;
    }

    #endregion
}
