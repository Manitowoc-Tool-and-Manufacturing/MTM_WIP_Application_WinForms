namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Filter criteria for querying Visual ERP inventory transactions.
    /// </summary>
    public class Model_VisualTransactionFilter
    {
        /// <summary>
        /// Part ID to filter by.
        /// </summary>
        public string? PartId { get; set; }

        /// <summary>
        /// User ID to filter by.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Work Order ID to filter by.
        /// </summary>
        public string? WorkOrder { get; set; }

        /// <summary>
        /// Customer Order ID to filter by.
        /// </summary>
        public string? CustomerOrder { get; set; }

        /// <summary>
        /// Purchase Order ID to filter by.
        /// </summary>
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// Start date for the transaction query.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// End date for the transaction query.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Maximum number of records to return. Default is 1000.
        /// </summary>
        public int MaxRecords { get; set; } = 1000;
    }
}
