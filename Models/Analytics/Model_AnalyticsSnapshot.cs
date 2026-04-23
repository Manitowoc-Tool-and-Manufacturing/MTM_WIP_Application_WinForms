namespace MTM_WIP_Application_Winforms.Models.Analytics
{
    /// <summary>
    /// Serializable analytics snapshot consumed by the HTML analytics view.
    /// </summary>
    public class Model_AnalyticsSnapshot
    {
        #region Fields
        // Intentionally left blank.
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current user identifier.
        /// </summary>
        public string CurrentUserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current user display name.
        /// </summary>
        public string CurrentUserDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether team scope is available.
        /// </summary>
        public bool CanViewTeam { get; set; }

        /// <summary>
        /// Gets or sets the time the snapshot was generated.
        /// </summary>
        public DateTime GeneratedAt { get; set; }

        /// <summary>
        /// Gets or sets the first date included in the data window.
        /// </summary>
        public DateTime DataWindowStart { get; set; }

        /// <summary>
        /// Gets or sets the last date included in the data window.
        /// </summary>
        public DateTime DataWindowEnd { get; set; }

        /// <summary>
        /// Gets or sets the default range start date.
        /// </summary>
        public DateTime DefaultStartDate { get; set; }

        /// <summary>
        /// Gets or sets the default range end date.
        /// </summary>
        public DateTime DefaultEndDate { get; set; }

        /// <summary>
        /// Gets or sets the known users represented in the snapshot.
        /// </summary>
        public List<Model_AnalyticsUser> Users { get; set; } = new();

        /// <summary>
        /// Gets or sets the normalized transaction feed.
        /// </summary>
        public List<Model_AnalyticsTransaction> Transactions { get; set; } = new();

        /// <summary>
        /// Gets or sets glossary definitions shown in the analytics UI.
        /// </summary>
        public List<Model_AnalyticsGlossaryItem> Glossary { get; set; } = new();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Model_AnalyticsSnapshot"/> class.
        /// </summary>
        public Model_AnalyticsSnapshot()
        {
        }
        #endregion

        #region Methods
        // Intentionally left blank.
        #endregion

        #region Events
        // Intentionally left blank.
        #endregion

        #region Helpers
        // Intentionally left blank.
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }

    /// <summary>
    /// Lightweight user reference for analytics selectors.
    /// </summary>
    public class Model_AnalyticsUser
    {
        #region Fields
        // Intentionally left blank.
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Model_AnalyticsUser"/> class.
        /// </summary>
        public Model_AnalyticsUser()
        {
        }
        #endregion

        #region Methods
        // Intentionally left blank.
        #endregion

        #region Events
        // Intentionally left blank.
        #endregion

        #region Helpers
        // Intentionally left blank.
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }

    /// <summary>
    /// Normalized transaction row used by the analytics page.
    /// </summary>
    public class Model_AnalyticsTransaction
    {
        #region Fields
        // Intentionally left blank.
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user display name.
        /// </summary>
        public string UserDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the friendly activity label.
        /// </summary>
        public string ActivityType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the raw transaction type.
        /// </summary>
        public string TransactionType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the part identifier.
        /// </summary>
        public string PartId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity moved.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the origin location.
        /// </summary>
        public string? FromLocation { get; set; }

        /// <summary>
        /// Gets or sets the destination location.
        /// </summary>
        public string? ToLocation { get; set; }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        public string? Operation { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public string? ItemType { get; set; }

        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        public string? BatchNumber { get; set; }

        /// <summary>
        /// Gets or sets when the transaction occurred.
        /// </summary>
        public DateTime OccurredAt { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Model_AnalyticsTransaction"/> class.
        /// </summary>
        public Model_AnalyticsTransaction()
        {
        }
        #endregion

        #region Methods
        // Intentionally left blank.
        #endregion

        #region Events
        // Intentionally left blank.
        #endregion

        #region Helpers
        // Intentionally left blank.
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }

    /// <summary>
    /// Glossary definition shown in the analytics page.
    /// </summary>
    public class Model_AnalyticsGlossaryItem
    {
        #region Fields
        // Intentionally left blank.
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the metric label.
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        public string Definition { get; set; } = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Model_AnalyticsGlossaryItem"/> class.
        /// </summary>
        public Model_AnalyticsGlossaryItem()
        {
        }
        #endregion

        #region Methods
        // Intentionally left blank.
        #endregion

        #region Events
        // Intentionally left blank.
        #endregion

        #region Helpers
        // Intentionally left blank.
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }
}