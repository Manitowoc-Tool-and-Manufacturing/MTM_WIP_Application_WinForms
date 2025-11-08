using System.Data;
using System.Drawing.Printing;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Complete configuration for a print/export job
/// </summary>
public class Model_PrintJob
{
    #region Data
    
    /// <summary>
    /// The data to print (already filtered)
    /// </summary>
    public DataTable Data { get; set; } = new DataTable();
    
    /// <summary>
    /// Title to display in print header
    /// </summary>
    public string Title { get; set; } = "MTM WIP Application";
    
    #endregion
    
    #region Printer Settings
    
    public string PrinterName { get; set; } = string.Empty;
    public bool IsLandscape { get; set; } = false;
    public bool IsColor { get; set; } = true;
    
    #endregion
    
    #region Page Range Settings
    
    public PrintRangeType PageRangeType { get; set; } = PrintRangeType.AllPages;
    public int FromPage { get; set; } = 1;
    public int ToPage { get; set; } = 999;
    public int CurrentPreviewPage { get; set; } = 1;
    
    #endregion
    
    #region Column Settings
    
    public List<string> ColumnOrder { get; set; } = new();
    public List<string> VisibleColumns { get; set; } = new();
    
    #endregion
    
    #region Helper Methods
    
    private const int RowsPerPage = 31; // Actual rows per printed page (measured from output)
    
    /// <summary>
    /// Gets the data to export/print based on page range settings
    /// </summary>
    public DataTable GetFilteredData()
    {
        // If all pages or no specific range, return all data
        if (PageRangeType == PrintRangeType.AllPages)
        {
            return Data;
        }
        
        // Calculate row range based on page numbers
        int startRow = 0;
        int endRow = Data.Rows.Count;
        
        if (PageRangeType == PrintRangeType.CurrentPage)
        {
            startRow = (CurrentPreviewPage - 1) * RowsPerPage;
            endRow = Math.Min(startRow + RowsPerPage, Data.Rows.Count);
        }
        else if (PageRangeType == PrintRangeType.PageRange)
        {
            startRow = (FromPage - 1) * RowsPerPage;
            endRow = Math.Min(ToPage * RowsPerPage, Data.Rows.Count);
        }
        
        // Create filtered data table
        var filteredData = Data.Clone();
        for (int i = startRow; i < endRow && i < Data.Rows.Count; i++)
        {
            filteredData.ImportRow(Data.Rows[i]);
        }
        
        return filteredData;
    }
    
    /// <summary>
    /// Applies settings to a PrintDocument
    /// </summary>
    public void ApplyToPrintDocument(PrintDocument document)
    {
        if (!string.IsNullOrEmpty(PrinterName))
        {
            document.PrinterSettings.PrinterName = PrinterName;
        }
        
        document.DefaultPageSettings.Landscape = IsLandscape;
        document.DefaultPageSettings.Color = IsColor;
        
        int from = FromPage;
        int to = ToPage;
        
        switch (PageRangeType)
        {
            case PrintRangeType.AllPages:
                document.PrinterSettings.PrintRange = PrintRange.AllPages;
                break;
            case PrintRangeType.CurrentPage:
                from = CurrentPreviewPage;
                to = CurrentPreviewPage;
                document.PrinterSettings.PrintRange = PrintRange.SomePages;
                document.PrinterSettings.FromPage = from;
                document.PrinterSettings.ToPage = to;
                break;
            case PrintRangeType.PageRange:
                document.PrinterSettings.PrintRange = PrintRange.SomePages;
                document.PrinterSettings.FromPage = from;
                document.PrinterSettings.ToPage = to;
                break;
        }
    }
    
    #endregion
}

/// <summary>
/// Type of page range for printing
/// </summary>
public enum PrintRangeType
{
    AllPages,
    CurrentPage,
    PageRange
}
