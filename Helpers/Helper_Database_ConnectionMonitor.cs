using System.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Helpers
{
    /// <summary>
    /// Provides monitoring of database connection lifecycle and health.
    /// </summary>
    public static class Helper_Database_ConnectionMonitor
    {
        #region Methods

        /// <summary>
        /// Retrieves current database connection statistics.
        /// </summary>
        /// <returns>
        /// A <see cref="ConnectionStats"/> object containing current connection metrics.
        /// </returns>
        public static async Task<ConnectionStats> GetConnectionStatsAsync()
        {
            var stats = new ConnectionStats
            {
                ServerAddress = Model_Shared_Users.WipServerAddress,
                Timestamp = DateTime.UtcNow,
                IsHealthy = true
            };

            try
            {
                // Use the system stored procedure to get process list
                var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    Model_Application_Variables.ConnectionString,
                    "md_system_GetProcessList", 
                    null);

                if (result.IsSuccess && result.Data != null)
                {
                    string? targetDatabase = Model_Shared_Users.Database;
                    int openConnections = 0;

                    foreach (DataRow row in result.Data.Rows)
                    {
                        string? db = null;
                        if (row.Table.Columns.Contains("db") && row["db"] != DBNull.Value)
                        {
                            db = row["db"]?.ToString();
                        }
                        // Count connections to our database
                        if (string.Equals(db, targetDatabase, StringComparison.OrdinalIgnoreCase))
                        {
                            openConnections++;
                        }
                    }

                    stats.OpenConnections = openConnections;

                    // In a perfect immediate-disposal world, open connections should be 0 (or 1 for this monitor query).
                    // We allow 1 because this query itself takes a connection.
                    // If > 1, we might have a leak or concurrent operations.
                    if (openConnections > 1)
                    {
                        stats.IsHealthy = false;
                        stats.WarningMessage = $"Potential connection leak: {openConnections} connections active (expected <= 1).";
                    }
                }
                else
                {
                    stats.IsHealthy = false;
                    stats.WarningMessage = $"Failed to retrieve connection stats: {result.ErrorMessage}";
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                stats.IsHealthy = false;
                stats.WarningMessage = $"Exception during monitoring: {ex.Message}";
            }

            return stats;
        }

        #endregion
    }
}
