using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Provides network diagnostics and signal strength measurement for MySQL server connectivity.
/// </summary>
/// <remarks>
/// <para><strong>ARCHITECTURAL EXCEPTION (Constitution Principle I)</strong></para>
/// <para>This helper contains direct MySqlConnection usage in GetStrengthAsync().
/// This is an APPROVED exception because:</para>
/// <list type="number">
/// <item><description>Network diagnostic tool that measures raw connection performance (ping time)</description></item>
/// <item><description>Using Helper_Database_StoredProcedure would add overhead and defeat the purpose of measuring connection speed</description></item>
/// <item><description>Direct connection is properly disposed using 'using var' pattern</description></item>
/// <item><description>This exception is documented in .specify/memory/constitution.md</description></item>
/// </list>
/// <para>ALL other components MUST use Helper_Database_StoredProcedure for database access.</para>
/// </remarks>
public class Helper_Control_MySqlSignal
{
    #region Public Methods

    /// <summary>
    /// Get network strength and ping time to MySQL server
    /// </summary>
    /// <returns>Tuple containing strength (0-5) and ping time in milliseconds</returns>
    public static async Task<(int strength, int pingMs)> GetStrengthAsync()
    {
        string host;
        try
        {
            var builder = new MySqlConnectionStringBuilder(DatabaseConfig.ConnectionString);
            host = builder.Server ??
                   throw new InvalidOperationException("Connection string does not specify a server.");
        }
        catch
        {
            return (0, -1);
        }

        // First, try to actually connect to MySQL (this is the real test)
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            using var connection = new MySqlConnection(DatabaseConfig.ConnectionString);
            await connection.OpenAsync();
            sw.Stop();
            var pingMs = (int)sw.ElapsedMilliseconds;
            
            // Calculate strength based on connection time
            var strength = pingMs switch
            {
                < 50 => 5,
                < 100 => 4,
                < 200 => 3,
                < 400 => 2,
                < 800 => 1,
                _ => 0
            };
            
            return (strength, pingMs);
        }
        catch
        {
            // MySQL connection failed, return 0 strength
            return (0, -1);
        }
    }

    #endregion
}

/* The `DatabaseConfig` class in C# contains a static property for the database connection string. */
public static class DatabaseConfig
{
    #region Properties

    /// <summary>
    /// Gets or sets the database connection string
    /// </summary>
    public static string ConnectionString { get; set; } =
        Helper_Database_Variables.GetConnectionString(null, null, null, null);

    #endregion
}
