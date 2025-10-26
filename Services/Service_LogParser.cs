using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using System.Text.RegularExpressions;

namespace MTM_Inventory_Application.Services;

/// <summary>
/// Provides log entry parsing services with format detection and type-specific parsers.
/// Supports Normal, ApplicationError, and DatabaseError log formats with regex-based parsing
/// and ReDoS protection via timeout.
/// </summary>
public static class Service_LogParser
{
    #region Constants

    /// <summary>
    /// Regex timeout in milliseconds to prevent ReDoS attacks.
    /// </summary>
    private static readonly TimeSpan RegexTimeout = TimeSpan.FromSeconds(1);

    #endregion

    #region Compiled Regex Patterns

    /// <summary>
    /// Pattern for Normal log format: [yyyy-MM-dd HH:mm:ss] LEVEL emoji [SOURCE] Message
    /// </summary>
    private static readonly Regex NormalLogPattern = new(
        @"^\[(\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}:\d{2})\]\s+(\w+)\s+([\p{So}\u200D]+)?\s*\[([^\]]+)\]\s+(.+)$",
        RegexOptions.Compiled,
        RegexTimeout);

    /// <summary>
    /// Pattern for detecting Application Error format: ERROR TYPE: ...
    /// </summary>
    private static readonly Regex AppErrorDetectionPattern = new(
        @"^\[(\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}:\d{2})\]\s+ERROR\s+TYPE:",
        RegexOptions.Compiled,
        RegexTimeout);

    /// <summary>
    /// Pattern for detecting Database Error format: [timestamp] SEVERITY Database Error
    /// </summary>
    private static readonly Regex DbErrorDetectionPattern = new(
        @"^\[(\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}:\d{2})\]\s+(WARNING|ERROR|CRITICAL)\s+Database\s+Error",
        RegexOptions.Compiled | RegexOptions.IgnoreCase,
        RegexTimeout);

    #endregion

    #region Format Detection

    /// <summary>
    /// Detects the log format type by analyzing the first line of a log entry.
    /// </summary>
    /// <param name="firstLine">First line of the log entry to analyze.</param>
    /// <returns>Detected LogFormat or Unknown if pattern doesn't match any known format.</returns>
    public static LogFormat DetectFormat(string firstLine)
    {
        if (string.IsNullOrWhiteSpace(firstLine))
        {
            return LogFormat.Unknown;
        }

        try
        {
            // Check for Application Error format first (most specific)
            if (AppErrorDetectionPattern.IsMatch(firstLine))
            {
                return LogFormat.ApplicationError;
            }

            // Check for Database Error format
            if (DbErrorDetectionPattern.IsMatch(firstLine))
            {
                return LogFormat.DatabaseError;
            }

            // Check for Normal log format
            if (NormalLogPattern.IsMatch(firstLine))
            {
                return LogFormat.Normal;
            }

            // No match found
            return LogFormat.Unknown;
        }
        catch (RegexMatchTimeoutException ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Regex timeout during format detection");
            LoggingUtility.LogApplicationError(ex);
            return LogFormat.Unknown;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Error detecting log format");
            LoggingUtility.LogApplicationError(ex);
            return LogFormat.Unknown;
        }
    }

    #endregion

    #region Parsing Methods

    /// <summary>
    /// Parses a Normal log entry with format: [timestamp] LEVEL emoji [SOURCE] Message.
    /// </summary>
    /// <param name="rawText">Raw log text to parse.</param>
    /// <returns>Parsed Model_LogEntry or raw entry if parsing fails.</returns>
    public static Model_LogEntry ParseNormalLog(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText))
        {
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText ?? string.Empty);
        }

        try
        {
            var lines = rawText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
            {
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            var firstLine = lines[0];
            var match = NormalLogPattern.Match(firstLine);

            if (!match.Success)
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse Normal log format");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            // Extract components
            DateTime timestamp = DateTime.Parse(match.Groups[1].Value);
            string level = match.Groups[2].Value;
            string emoji = match.Groups[3].Success ? match.Groups[3].Value : string.Empty;
            string source = match.Groups[4].Value;
            string message = match.Groups[5].Value;

            // Look for "Details:" section in remaining lines
            string? details = null;
            if (lines.Length > 1)
            {
                var detailsLine = Array.Find(lines, line => line.Trim().StartsWith("Details:", StringComparison.OrdinalIgnoreCase));
                if (detailsLine != null)
                {
                    int detailsIndex = Array.IndexOf(lines, detailsLine);
                    if (detailsIndex >= 0 && detailsIndex < lines.Length - 1)
                    {
                        details = string.Join(Environment.NewLine, lines.Skip(detailsIndex + 1));
                    }
                }
            }

            return Model_LogEntry.CreateNormalEntry(
                timestamp,
                level,
                emoji,
                source,
                message,
                details,
                rawText);
        }
        catch (RegexMatchTimeoutException ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Regex timeout parsing Normal log");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Error parsing Normal log");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
    }

    /// <summary>
    /// Parses an Application Error log entry with two-line paired format.
    /// Format: [timestamp] ERROR TYPE: ErrorType
    ///         Exception Message: Message
    ///         [optional] Stack Trace: ...
    /// </summary>
    /// <param name="rawText">Raw log text to parse.</param>
    /// <returns>Parsed Model_LogEntry or raw entry if parsing fails.</returns>
    public static Model_LogEntry ParseApplicationError(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText))
        {
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText ?? string.Empty);
        }

        try
        {
            var lines = rawText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
            {
                LoggingUtility.Log($"[Service_LogParser] Application Error log has insufficient lines");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            // Parse first line: [timestamp] ERROR TYPE: ErrorType
            var firstLineMatch = AppErrorDetectionPattern.Match(lines[0]);
            if (!firstLineMatch.Success)
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse Application Error first line");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            DateTime timestamp = DateTime.Parse(firstLineMatch.Groups[1].Value);
            string errorType = lines[0].Substring(lines[0].IndexOf("ERROR TYPE:") + "ERROR TYPE:".Length).Trim();

            // Parse second line: Exception Message: Message
            string message = string.Empty;
            if (lines.Length > 1 && lines[1].Contains("Exception Message:"))
            {
                message = lines[1].Substring(lines[1].IndexOf("Exception Message:") + "Exception Message:".Length).Trim();
            }

            // Look for Stack Trace section
            string? stackTrace = null;
            var stackTraceIndex = Array.FindIndex(lines, line => line.Trim().StartsWith("Stack Trace:", StringComparison.OrdinalIgnoreCase));
            if (stackTraceIndex >= 0 && stackTraceIndex < lines.Length - 1)
            {
                stackTrace = string.Join(Environment.NewLine, lines.Skip(stackTraceIndex + 1));
            }

            return Model_LogEntry.CreateApplicationErrorEntry(
                timestamp,
                errorType,
                message,
                stackTrace,
                rawText);
        }
        catch (RegexMatchTimeoutException ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Regex timeout parsing Application Error");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Error parsing Application Error");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
    }

    /// <summary>
    /// Parses a Database Error log entry with severity prefix.
    /// Format: [timestamp] SEVERITY Database Error: Message
    ///         [optional] Stack Trace: ...
    /// </summary>
    /// <param name="rawText">Raw log text to parse.</param>
    /// <returns>Parsed Model_LogEntry or raw entry if parsing fails.</returns>
    public static Model_LogEntry ParseDatabaseError(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText))
        {
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText ?? string.Empty);
        }

        try
        {
            var lines = rawText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
            {
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            var firstLine = lines[0];
            var match = DbErrorDetectionPattern.Match(firstLine);

            if (!match.Success)
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse Database Error format");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            DateTime timestamp = DateTime.Parse(match.Groups[1].Value);
            string severity = match.Groups[2].Value.ToUpperInvariant();

            // Extract message after "Database Error:"
            string message = firstLine.Contains("Database Error:")
                ? firstLine.Substring(firstLine.IndexOf("Database Error:") + "Database Error:".Length).Trim()
                : string.Empty;

            // Look for additional details in subsequent lines (before Stack Trace)
            string? details = null;
            var stackTraceIndex = Array.FindIndex(lines, line => line.Trim().StartsWith("Stack Trace:", StringComparison.OrdinalIgnoreCase));

            if (lines.Length > 1)
            {
                int endIndex = stackTraceIndex > 0 ? stackTraceIndex : lines.Length;
                if (endIndex > 1)
                {
                    details = string.Join(Environment.NewLine, lines.Skip(1).Take(endIndex - 1));
                }
            }

            return Model_LogEntry.CreateDatabaseErrorEntry(
                timestamp,
                severity,
                message,
                details,
                rawText);
        }
        catch (RegexMatchTimeoutException ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Regex timeout parsing Database Error");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Error parsing Database Error");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
    }

    /// <summary>
    /// Unified entry point for parsing any log entry. Detects format if unknown,
    /// routes to appropriate parser, and falls back to raw entry on failure.
    /// </summary>
    /// <param name="rawText">Raw log text to parse.</param>
    /// <param name="format">Known format type, or Unknown to auto-detect.</param>
    /// <returns>Parsed Model_LogEntry (structured or raw).</returns>
    public static Model_LogEntry ParseEntry(string rawText, LogFormat format = LogFormat.Unknown)
    {
        if (string.IsNullOrWhiteSpace(rawText))
        {
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText ?? string.Empty);
        }

        try
        {
            // Auto-detect format if unknown
            if (format == LogFormat.Unknown)
            {
                var lines = rawText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 0)
                {
                    format = DetectFormat(lines[0]);
                }
            }

            // Route to appropriate parser
            return format switch
            {
                LogFormat.Normal => ParseNormalLog(rawText),
                LogFormat.ApplicationError => ParseApplicationError(rawText),
                LogFormat.DatabaseError => ParseDatabaseError(rawText),
                _ => Model_LogEntry.CreateRawEntry(DateTime.Now, rawText)
            };
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_LogParser] Unexpected error in ParseEntry");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
    }

    #endregion
}
