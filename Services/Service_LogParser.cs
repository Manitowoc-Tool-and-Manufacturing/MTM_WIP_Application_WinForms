using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Provides simplified CSV-based log entry parsing services.
/// Parses CSV format: Timestamp,Level,Source,Message,Details
/// </summary>
public static class Service_LogParser
{
    #region CSV Parsing Helper

    /// <summary>
    /// Splits a CSV line into fields, handling quoted values with embedded commas and escaped quotes.
    /// </summary>
    /// <param name="csvLine">CSV line to parse.</param>
    /// <returns>Array of field values.</returns>
    private static string[] SplitCsvLine(string csvLine)
    {
        var fields = new List<string>();
        var currentField = new System.Text.StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < csvLine.Length; i++)
        {
            char c = csvLine[i];

            if (c == '"')
            {
                // Check for escaped quote ("")
                if (inQuotes && i + 1 < csvLine.Length && csvLine[i + 1] == '"')
                {
                    currentField.Append('"'); // Add single quote
                    i++; // Skip next quote
                }
                else
                {
                    inQuotes = !inQuotes; // Toggle quote state
                }
            }
            else if (c == ',' && !inQuotes)
            {
                // Field separator outside quotes
                fields.Add(currentField.ToString());
                currentField.Clear();
            }
            else
            {
                currentField.Append(c);
            }
        }

        // Add final field
        fields.Add(currentField.ToString());

        return fields.ToArray();
    }

    #endregion

    #region Parsing Methods

    /// <summary>
    /// Parses a CSV log entry into Model_LogEntry.
    /// CSV format: Timestamp,Level,Source,Message,Details
    /// </summary>
    /// <param name="csvLine">CSV line to parse.</param>
    /// <param name="format">Log format type for proper model creation.</param>
    /// <returns>Parsed Model_LogEntry or raw entry if parsing fails.</returns>
    public static Model_LogEntry ParseEntry(string csvLine, LogFormat format = LogFormat.Normal)
    {
        if (string.IsNullOrWhiteSpace(csvLine))
        {
            return Model_LogEntry.CreateRawEntry(DateTime.Now, csvLine ?? string.Empty);
        }

        try
        {
            // Skip header row if present
            if (csvLine.StartsWith("Timestamp,", StringComparison.OrdinalIgnoreCase))
            {
                LoggingUtility.Log("[Service_LogParser] Skipping CSV header row");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, csvLine);
            }

            string[] fields = SplitCsvLine(csvLine);

            // Validate field count (should be 5: Timestamp, Level, Source, Message, Details)
            if (fields.Length < 4)
            {
                LoggingUtility.Log($"[Service_LogParser] Invalid CSV format - expected 5 fields, got {fields.Length}");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, csvLine);
            }

            // Extract CSV fields
            string timestampStr = fields[0];
            string level = fields[1];
            string source = fields[2];
            string message = fields[3];
            string? details = fields.Length > 4 && !string.IsNullOrWhiteSpace(fields[4]) ? fields[4] : null;

            // Parse timestamp
            if (!DateTime.TryParse(timestampStr, out DateTime timestamp))
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse timestamp: {timestampStr}");
                timestamp = DateTime.Now;
            }

            // Route to appropriate factory method based on format
            return format switch
            {
                LogFormat.Normal => Model_LogEntry.CreateNormalEntry(
                    timestamp,
                    level,
                    string.Empty, // No emoji in CSV format
                    source,
                    message,
                    details,
                    csvLine),

                LogFormat.ApplicationError => Model_LogEntry.CreateApplicationErrorEntry(
                    timestamp,
                    "ApplicationException", // Error type from message if needed
                    message,
                    details, // Stack trace
                    csvLine),

                LogFormat.DatabaseError => Model_LogEntry.CreateDatabaseErrorEntry(
                    timestamp,
                    level, // Severity (ERROR, WARNING, CRITICAL)
                    message,
                    details,
                    csvLine),

                _ => Model_LogEntry.CreateRawEntry(timestamp, csvLine)
            };
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Error parsing CSV entry: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, csvLine);
        }
    }

    /// <summary>
    /// Parses a Normal log CSV entry.
    /// </summary>
    public static Model_LogEntry ParseNormalLog(string csvLine)
    {
        return ParseEntry(csvLine, LogFormat.Normal);
    }

    /// <summary>
    /// Parses an Application Error log CSV entry.
    /// </summary>
    public static Model_LogEntry ParseApplicationError(string csvLine)
    {
        return ParseEntry(csvLine, LogFormat.ApplicationError);
    }

    /// <summary>
    /// Parses a Database Error log CSV entry.
    /// </summary>
    public static Model_LogEntry ParseDatabaseError(string csvLine)
    {
        return ParseEntry(csvLine, LogFormat.DatabaseError);
    }

    #endregion
}
