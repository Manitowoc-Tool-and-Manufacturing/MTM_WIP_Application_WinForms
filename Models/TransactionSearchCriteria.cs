namespace MTM_Inventory_Application.Models;

/// <summary>
/// Encapsulates search criteria for transaction queries with validation methods.
/// </summary>
internal sealed class TransactionSearchCriteria
{
    #region Properties

    /// <summary>
    /// Gets or sets the part identifier to filter transactions.
    /// </summary>
    public string? PartID { get; set; }

    /// <summary>
    /// Gets or sets the user name to filter transactions.
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Gets or sets the source location to filter transactions.
    /// </summary>
    public string? FromLocation { get; set; }

    /// <summary>
    /// Gets or sets the destination location to filter transactions.
    /// </summary>
    public string? ToLocation { get; set; }

    /// <summary>
    /// Gets or sets the operation number to filter transactions.
    /// </summary>
    public string? Operation { get; set; }

    /// <summary>
    /// Gets or sets the transaction type to filter (IN, OUT, TRANSFER).
    /// </summary>
    public string? TransactionType { get; set; }

    /// <summary>
    /// Gets or sets the start date for the date range filter.
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Gets or sets the end date for the date range filter.
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Gets or sets the notes text for partial matching.
    /// </summary>
    public string? Notes { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Validates that the search criteria has at least one filter specified.
    /// </summary>
    /// <returns>True if at least one filter is specified; otherwise false.</returns>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(PartID) ||
               !string.IsNullOrWhiteSpace(User) ||
               !string.IsNullOrWhiteSpace(FromLocation) ||
               !string.IsNullOrWhiteSpace(ToLocation) ||
               !string.IsNullOrWhiteSpace(Operation) ||
               !string.IsNullOrWhiteSpace(TransactionType) ||
               DateFrom.HasValue ||
               DateTo.HasValue ||
               !string.IsNullOrWhiteSpace(Notes);
    }

    /// <summary>
    /// Validates that the date range is logically correct (DateFrom &lt;= DateTo).
    /// </summary>
    /// <returns>True if date range is valid or no dates specified; otherwise false.</returns>
    public bool IsDateRangeValid()
    {
        if (!DateFrom.HasValue || !DateTo.HasValue)
            return true;

        return DateFrom.Value <= DateTo.Value;
    }

    /// <summary>
    /// Returns a summary string describing the active filters.
    /// </summary>
    /// <returns>Human-readable summary of search criteria.</returns>
    public override string ToString()
    {
        var filters = new List<string>();

        if (!string.IsNullOrWhiteSpace(PartID))
            filters.Add($"Part: {PartID}");
        if (!string.IsNullOrWhiteSpace(User))
            filters.Add($"User: {User}");
        if (!string.IsNullOrWhiteSpace(FromLocation))
            filters.Add($"From: {FromLocation}");
        if (!string.IsNullOrWhiteSpace(ToLocation))
            filters.Add($"To: {ToLocation}");
        if (!string.IsNullOrWhiteSpace(Operation))
            filters.Add($"Op: {Operation}");
        if (!string.IsNullOrWhiteSpace(TransactionType))
            filters.Add($"Type: {TransactionType}");
        if (DateFrom.HasValue)
            filters.Add($"From: {DateFrom.Value:MM/dd/yyyy}");
        if (DateTo.HasValue)
            filters.Add($"To: {DateTo.Value:MM/dd/yyyy}");
        if (!string.IsNullOrWhiteSpace(Notes))
            filters.Add($"Notes: {Notes}");

        return filters.Count > 0 ? string.Join(", ", filters) : "No filters";
    }

    #endregion
}
