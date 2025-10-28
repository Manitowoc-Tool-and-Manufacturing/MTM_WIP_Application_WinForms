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
    /// Pattern for structured log format: [TIMESTAMP]|LEVEL|SOURCE|MESSAGE
    /// This is the new standardized format that all logs use.
    /// Example: [2025-10-28 14:30:45]|INFO|Application|Starting application
    /// </summary>
    private static readonly Regex StructuredLogPattern = new(
        @"^\[(\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}:\d{2})\]\|([A-Z]+)\|([^\|]+)\|(.*)$",
        RegexOptions.Compiled,
        RegexTimeout);

    #endregion

    #region Format Detection

    /// <summary>
    /// Detects the log format type by analyzing the first line of a log entry.
    /// All logs now use structured format: [TIMESTAMP]|LEVEL|SOURCE|MESSAGE
    /// </summary>
    /// <param name="firstLine">First line of the log entry to analyze.</param>
    /// <returns>Detected LogFormat based on SOURCE field.</returns>
    public static LogFormat DetectFormat(string firstLine)
    {
        if (string.IsNullOrWhiteSpace(firstLine))
        {
            return LogFormat.Unknown;
        }

        try
        {
            // Try to match structured format
            var match = StructuredLogPattern.Match(firstLine);
            if (!match.Success)
            {
                return LogFormat.Unknown;
            }

            // Determine format based on SOURCE field (Group 3)
            string source = match.Groups[3].Value.ToLowerInvariant();
            
            if (source.Contains("database"))
            {
                return LogFormat.DatabaseError;
            }
            
            string level = match.Groups[2].Value.ToUpperInvariant();
            if (level == "ERROR" || level == "CRITICAL" || level == "WARNING")
            {
                // Check if it's from Application source
                if (source == "application")
                {
                    return LogFormat.ApplicationError;
                }
                return LogFormat.DatabaseError;
            }

            return LogFormat.Normal;
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
    /// Parses a log entry with structured format: [TIMESTAMP]|LEVEL|SOURCE|MESSAGE
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
            var match = StructuredLogPattern.Match(firstLine);

            if (!match.Success)
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse structured log format: {firstLine.Substring(0, Math.Min(50, firstLine.Length))}...");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            // Extract structured components
            DateTime timestamp = DateTime.Parse(match.Groups[1].Value);
            string level = match.Groups[2].Value;
            string source = match.Groups[3].Value;
            string message = match.Groups[4].Value;

            // Check for detail lines (DETAIL level indicates stack traces, etc.)
            string? details = null;
            if (lines.Length > 1)
            {
                var detailLines = lines.Skip(1)
                    .Where(line => line.Contains("|DETAIL|"))
                    .Select(line =>
                    {
                        var detailMatch = StructuredLogPattern.Match(line);
                        return detailMatch.Success ? detailMatch.Groups[4].Value : line;
                    })
                    .ToList();

                if (detailLines.Any())
                {
                    details = string.Join(Environment.NewLine, detailLines);
                }
            }

            return Model_LogEntry.CreateNormalEntry(
                timestamp,
                level,
                string.Empty, // No emoji in new format
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
            LoggingUtility.Log($"[Service_LogParser] Error parsing Normal log: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
    }

    /// <summary>
    /// Parses an Application Error log entry with structured format: [TIMESTAMP]|ERROR|Application|MESSAGE
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
            if (lines.Length == 0)
            {
                LoggingUtility.Log($"[Service_LogParser] Application Error log has no lines");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            var firstLine = lines[0];
            var match = StructuredLogPattern.Match(firstLine);

            if (!match.Success)
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse Application Error format: {firstLine.Substring(0, Math.Min(50, firstLine.Length))}...");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            DateTime timestamp = DateTime.Parse(match.Groups[1].Value);
            string message = match.Groups[4].Value;

            // Extract error type from message (format: ExceptionType: Message)
            string errorType = "ApplicationException";
            if (message.Contains(":"))
            {
                int colonIndex = message.IndexOf(':');
                errorType = message.Substring(0, colonIndex).Trim();
                message = message.Substring(colonIndex + 1).Trim();
            }

            // Look for stack trace in detail lines
            string? stackTrace = null;
            if (lines.Length > 1)
            {
                var stackLines = lines.Skip(1)
                    .Where(line => line.Contains("|DETAIL|StackTrace|"))
                    .Select(line =>
                    {
                        var detailMatch = StructuredLogPattern.Match(line);
                        return detailMatch.Success ? detailMatch.Groups[4].Value : line;
                    })
                    .ToList();

                if (stackLines.Any())
                {
                    stackTrace = string.Join(Environment.NewLine, stackLines);
                }
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
            LoggingUtility.Log($"[Service_LogParser] Error parsing Application Error: {ex.Message}");
            LoggingUtility.LogApplicationError(ex);
            return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
        }
    }

    /// <summary>
    /// Parses a Database Error log entry with structured format: [TIMESTAMP]|ERROR|Database|MESSAGE
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
            var match = StructuredLogPattern.Match(firstLine);

            if (!match.Success)
            {
                LoggingUtility.Log($"[Service_LogParser] Failed to parse Database Error format: {firstLine.Substring(0, Math.Min(50, firstLine.Length))}...");
                return Model_LogEntry.CreateRawEntry(DateTime.Now, rawText);
            }

            DateTime timestamp = DateTime.Parse(match.Groups[1].Value);
            string severity = match.Groups[2].Value.ToUpperInvariant();
            string message = match.Groups[4].Value;

            // Extract error type from message (format: ExceptionType: Message)
            if (message.Contains(":"))
            {
                int colonIndex = message.IndexOf(':');
                message = message.Substring(colonIndex + 1).Trim();
            }

            // Look for detail lines
            string? details = null;
            if (lines.Length > 1)
            {
                var detailLines = lines.Skip(1)
                    .Where(line => line.Contains("|DETAIL|"))
                    .Select(line =>
                    {
                        var detailMatch = StructuredLogPattern.Match(line);
                        return detailMatch.Success ? detailMatch.Groups[4].Value : line;
                    })
                    .ToList();

                if (detailLines.Any())
                {
                    details = string.Join(Environment.NewLine, detailLines);
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
            LoggingUtility.Log($"[Service_LogParser] Error parsing Database Error: {ex.Message}");
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
