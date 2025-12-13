namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Represents a snapshot of database connection health statistics.
    /// </summary>
    public class ConnectionStats
    {
        #region Properties

        /// <summary>
        /// Gets or sets the database server address.
        /// </summary>
        public string ServerAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of open connections from this application.
        /// </summary>
        public int OpenConnections { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when these statistics were captured.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the connection state is considered healthy.
        /// (e.g., OpenConnections == 0 when idle).
        /// </summary>
        public bool IsHealthy { get; set; }

        /// <summary>
        /// Gets or sets a warning message if the state is unhealthy.
        /// </summary>
        public string? WarningMessage { get; set; }

        #endregion
    }
}
