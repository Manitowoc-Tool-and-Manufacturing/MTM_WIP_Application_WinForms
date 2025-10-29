namespace MTM_Inventory_Application.Models;

/// <summary>
/// Pagination wrapper for transaction search results.
/// </summary>
internal sealed class TransactionSearchResult
{
    #region Properties

    /// <summary>
    /// Gets or sets the list of transactions for the current page.
    /// </summary>
    public List<Model_Transactions> Transactions { get; set; } = new List<Model_Transactions>();

    /// <summary>
    /// Gets or sets the total number of records matching the search criteria.
    /// </summary>
    public int TotalRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the current page number (1-based).
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the number of records per page.
    /// </summary>
    public int PageSize { get; set; }

    #endregion

    #region Calculated Properties

    /// <summary>
    /// Gets the total number of pages based on record count and page size.
    /// </summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalRecordCount / PageSize) : 0;

    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;

    /// <summary>
    /// Gets a summary string describing the current pagination state.
    /// </summary>
    /// <example>"Page 1 of 5 (245 records)"</example>
    public string PaginationSummary => $"Page {CurrentPage} of {TotalPages} ({TotalRecordCount} records)";

    #endregion
}
