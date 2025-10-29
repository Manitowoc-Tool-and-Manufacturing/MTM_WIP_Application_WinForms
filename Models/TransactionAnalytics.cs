namespace MTM_Inventory_Application.Models;

/// <summary>
/// Analytics summary for transaction data with calculated percentages.
/// </summary>
internal sealed class TransactionAnalytics
{
    #region Properties

    /// <summary>
    /// Gets or sets the total number of transactions.
    /// </summary>
    public int TotalTransactions { get; set; }

    /// <summary>
    /// Gets or sets the total number of IN transactions.
    /// </summary>
    public int TotalIN { get; set; }

    /// <summary>
    /// Gets or sets the total number of OUT transactions.
    /// </summary>
    public int TotalOUT { get; set; }

    /// <summary>
    /// Gets or sets the total number of TRANSFER transactions.
    /// </summary>
    public int TotalTRANSFER { get; set; }

    /// <summary>
    /// Gets or sets the date range for the analytics data.
    /// </summary>
    public (DateTime? From, DateTime? To) DateRange { get; set; }

    #endregion

    #region Calculated Properties

    /// <summary>
    /// Gets the percentage of IN transactions relative to total.
    /// Handles division by zero gracefully.
    /// </summary>
    public double PercentageIN => TotalTransactions > 0 ? (double)TotalIN / TotalTransactions * 100.0 : 0.0;

    /// <summary>
    /// Gets the percentage of OUT transactions relative to total.
    /// Handles division by zero gracefully.
    /// </summary>
    public double PercentageOUT => TotalTransactions > 0 ? (double)TotalOUT / TotalTransactions * 100.0 : 0.0;

    /// <summary>
    /// Gets the percentage of TRANSFER transactions relative to total.
    /// Handles division by zero gracefully.
    /// </summary>
    public double PercentageTRANSFER => TotalTransactions > 0 ? (double)TotalTRANSFER / TotalTransactions * 100.0 : 0.0;

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns a summary string describing the analytics.
    /// </summary>
    /// <returns>Human-readable analytics summary.</returns>
    public override string ToString()
    {
        var dateRangeStr = DateRange.From.HasValue && DateRange.To.HasValue
            ? $" ({DateRange.From.Value:MM/dd/yyyy} - {DateRange.To.Value:MM/dd/yyyy})"
            : "";

        return $"Total: {TotalTransactions}, IN: {TotalIN} ({PercentageIN:F1}%), " +
               $"OUT: {TotalOUT} ({PercentageOUT:F1}%), " +
               $"TRANSFER: {TotalTRANSFER} ({PercentageTRANSFER:F1}%){dateRangeStr}";
    }

    #endregion
}
