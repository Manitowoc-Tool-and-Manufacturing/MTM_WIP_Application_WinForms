namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Analytics summary for transaction data with calculated percentages.
/// </summary>
internal sealed class Model_Transactions_Core_Analytics
{
    #region Properties - Transaction Counts

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

    #region Properties - Quantity Analytics

    /// <summary>
    /// Gets or sets the total quantity moved across all transactions.
    /// </summary>
    public int TotalQuantityMoved { get; set; }

    /// <summary>
    /// Gets or sets the total quantity from IN transactions.
    /// </summary>
    public int QuantityIN { get; set; }

    /// <summary>
    /// Gets or sets the total quantity from OUT transactions.
    /// </summary>
    public int QuantityOUT { get; set; }

    /// <summary>
    /// Gets or sets the total quantity from TRANSFER transactions.
    /// </summary>
    public int QuantityTRANSFER { get; set; }

    #endregion

    #region Properties - Top Items

    /// <summary>
    /// Gets or sets the most active user and their transaction count.
    /// </summary>
    public (string? UserName, int TransactionCount) MostActiveUser { get; set; }

    /// <summary>
    /// Gets or sets the most transacted part and its transaction count.
    /// </summary>
    public (string? PartID, int TransactionCount) MostTransactedPart { get; set; }

    /// <summary>
    /// Gets or sets the busiest location and its transaction count.
    /// </summary>
    public (string? LocationName, int TransactionCount) BusiestLocation { get; set; }

    /// <summary>
    /// Gets or sets the most transferred part (most TRANSFER transactions) and its count.
    /// </summary>
    public (string? PartID, int TransferCount) MostTransferredPart { get; set; }

    /// <summary>
    /// Gets or sets the busiest day of the week and its transaction count.
    /// </summary>
    public (string? DayName, int TransactionCount) BusiestDay { get; set; }

    /// <summary>
    /// Gets or sets the peak hour (0-23) and its transaction count.
    /// </summary>
    public (int Hour, int TransactionCount) PeakHour { get; set; }

    /// <summary>
    /// Gets or sets the transaction rate (transactions per day).
    /// </summary>
    public double TransactionRate { get; set; }

    /// <summary>
    /// Gets or sets the transaction rate trend (positive = increasing, negative = decreasing).
    /// </summary>
    public string? TransactionRateTrend { get; set; }

    #endregion

    #region Properties - Database Timeline

    /// <summary>
    /// Gets or sets the timestamp of the first transaction in the database.
    /// </summary>
    public DateTime? FirstTransactionDate { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the most recent transaction in the database.
    /// </summary>
    public DateTime? LastTransactionDate { get; set; }

    #endregion

    #region Calculated Properties - Percentages

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

    #region Calculated Properties - Derived Metrics

    /// <summary>
    /// Gets the net inventory change (IN quantity - OUT quantity).
    /// </summary>
    public int NetInventoryChange => QuantityIN - QuantityOUT;

    /// <summary>
    /// Gets the average daily transaction count.
    /// Returns 0 if database has no date range or only one day of data.
    /// </summary>
    public double AverageDailyTransactions
    {
        get
        {
            if (!FirstTransactionDate.HasValue || !LastTransactionDate.HasValue)
                return 0.0;

            var daySpan = (LastTransactionDate.Value - FirstTransactionDate.Value).Days;
            return daySpan > 0 ? (double)TotalTransactions / daySpan : TotalTransactions;
        }
    }

    /// <summary>
    /// Gets the number of days between first and last transaction.
    /// Returns 0 if dates are not available.
    /// </summary>
    public int DatabaseDaySpan
    {
        get
        {
            if (!FirstTransactionDate.HasValue || !LastTransactionDate.HasValue)
                return 0;

            return (LastTransactionDate.Value - FirstTransactionDate.Value).Days;
        }
    }

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
