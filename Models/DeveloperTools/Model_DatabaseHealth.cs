using System;
using System.Data;

namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Database health and status information.
/// </summary>
public class Model_DatabaseHealth
{
    #region Properties

    /// <summary>
    /// Whether the database is currently connected.
    /// </summary>
    public bool IsConnected { get; set; }

    /// <summary>
    /// Connection status message.
    /// </summary>
    public string StatusMessage { get; set; } = "Unknown";

    /// <summary>
    /// MySQL server version.
    /// </summary>
    public string? ServerVersion { get; set; }

    /// <summary>
    /// Database name.
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// Server hostname/IP.
    /// </summary>
    public string? ServerHost { get; set; }

    /// <summary>
    /// Server uptime (if available).
    /// </summary>
    public TimeSpan? Uptime { get; set; }

    /// <summary>
    /// Last successful query timestamp.
    /// </summary>
    public DateTime? LastSuccessfulQuery { get; set; }

    /// <summary>
    /// Current connection latency in milliseconds.
    /// </summary>
    public int? LatencyMs { get; set; }

    /// <summary>
    /// Database uptime in seconds.
    /// </summary>
    public long UptimeSeconds { get; set; }

    /// <summary>
    /// Database version.
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Number of active connections.
    /// </summary>
    public int ConnectionCount { get; set; }

    /// <summary>
    /// Total bytes received.
    /// </summary>
    public long BytesReceived { get; set; }

    /// <summary>
    /// Total bytes sent.
    /// </summary>
    public long BytesSent { get; set; }

    /// <summary>
    /// Number of open tables.
    /// </summary>
    public int OpenTables { get; set; }

    /// <summary>
    /// Total queries executed.
    /// </summary>
    public int Queries { get; set; }
    /// <summary>
    /// Statistics about database tables (row counts, sizes).
    /// </summary>
    public DataTable? TableStatistics { get; set; }

    #endregion

    #region Factory Methods

    /// <summary>
    /// Creates a status for an offline database.
    /// </summary>
    public static Model_DatabaseHealth Offline(string message = "Database connection unavailable") => new()
    {
        IsConnected = false,
        StatusMessage = message
    };

    /// <summary>
    /// Creates a status for an online database.
    /// </summary>
    public static Model_DatabaseHealth Online(string serverVersion, string databaseName, string host) => new()
    {
        IsConnected = true,
        StatusMessage = "Connected",
        ServerVersion = serverVersion,
        DatabaseName = databaseName,
        ServerHost = host,
        LastSuccessfulQuery = DateTime.Now
    };

    #endregion
}
