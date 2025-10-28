using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace MTM_Inventory_Application.Services;

/// <summary>
/// Provides services for extracting method information from stack traces
/// and generating Copilot prompts for error fixes.
/// </summary>
public static class Service_PromptGenerator
{
    #region Constants

    /// <summary>
    /// Regex timeout to prevent ReDoS attacks.
    /// </summary>
    private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Namespaces to exclude when extracting application methods from stack traces.
    /// These are framework/library namespaces, not application code.
    /// </summary>
    private static readonly HashSet<string> ExcludedNamespaces = new(StringComparer.OrdinalIgnoreCase)
    {
        "System",
        "Microsoft",
        "MySql",
        "DocumentFormat",
        "Avalonia"
    };

    #endregion

    #region Compiled Regex Patterns

    /// <summary>
    /// Pattern to match stack trace lines in the format:
    /// "at Namespace.Class.Method(Parameters) in FileName:line LineNumber"
    /// Also handles async methods and lambda expressions.
    /// </summary>
    private static readonly Regex StackTracePattern = new(
        @"at\s+(?<namespace>[\w\.]+)\.(?<class>[\w<>]+)\.(?<method>[\w<>]+)\s*\(",
        RegexOptions.Compiled | RegexOptions.Multiline,
        RegexTimeout);

    #endregion

    #region Method Name Extraction

    /// <summary>
    /// Extracts the first application method name from a stack trace,
    /// excluding system and framework namespaces.
    /// </summary>
    /// <param name="stackTrace">Stack trace string to parse.</param>
    /// <returns>Sanitized method name suitable for filename, or null if no valid method found.</returns>
    public static string? ExtractMethodName(string? stackTrace)
    {
        if (string.IsNullOrWhiteSpace(stackTrace))
        {
            LoggingUtility.Log("[Service_PromptGenerator] ExtractMethodName: Stack trace is null or empty");
            return null;
        }

        try
        {
            var matches = StackTracePattern.Matches(stackTrace);

            foreach (Match match in matches)
            {
                if (!match.Success)
                {
                    continue;
                }

                string fullNamespace = match.Groups["namespace"].Value;
                string className = match.Groups["class"].Value;
                string methodName = match.Groups["method"].Value;

                // Check if this is a framework/library namespace
                bool isExcluded = ExcludedNamespaces.Any(excluded => 
                    fullNamespace.StartsWith(excluded, StringComparison.OrdinalIgnoreCase));

                if (isExcluded)
                {
                    continue; // Skip framework methods
                }

                // Found an application method - construct full method name
                string fullMethodName = $"{className}.{methodName}";

                // Handle async methods (remove <> wrapper and MoveNext)
                fullMethodName = CleanAsyncMethodName(fullMethodName);

                // Handle lambda expressions (replace <> with readable name)
                fullMethodName = CleanLambdaMethodName(fullMethodName);

                // Sanitize for filesystem
                string sanitized = SanitizeMethodNameForFilename(fullMethodName);

                if (!string.IsNullOrWhiteSpace(sanitized))
                {
                    LoggingUtility.Log($"[Service_PromptGenerator] Extracted method name: {sanitized}");
                    return sanitized;
                }
            }

            LoggingUtility.Log("[Service_PromptGenerator] No application method found in stack trace");
            return null;
        }
        catch (RegexMatchTimeoutException ex)
        {
            LoggingUtility.Log("[Service_PromptGenerator] Regex timeout during method extraction");
            LoggingUtility.LogApplicationError(ex);
            return null;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[Service_PromptGenerator] Error extracting method name from stack trace");
            LoggingUtility.LogApplicationError(ex);
            return null;
        }
    }

    #endregion

    #region Name Cleaning Helpers

    /// <summary>
    /// Cleans async method names by removing compiler-generated wrappers.
    /// Example: MyClass.<MyMethodAsync>d__15.MoveNext -> MyClass.MyMethodAsync
    /// </summary>
    private static string CleanAsyncMethodName(string methodName)
    {
        // Match pattern: <MethodName>d__## or <MethodName>c__DisplayClass##
        var asyncPattern = new Regex(@"<(\w+)>[dc]__\w+", RegexOptions.None, RegexTimeout);
        var match = asyncPattern.Match(methodName);

        if (match.Success)
        {
            string cleanName = match.Groups[1].Value;
            // Replace the entire async wrapper with just the method name
            methodName = asyncPattern.Replace(methodName, cleanName);
            
            // Remove .MoveNext if present
            methodName = methodName.Replace(".MoveNext", string.Empty);
        }

        return methodName;
    }

    /// <summary>
    /// Cleans lambda expression method names.
    /// Example: <MethodName>b__1_0 -> MethodName_Lambda
    /// </summary>
    private static string CleanLambdaMethodName(string methodName)
    {
        // Match pattern: <MethodName>b__##_##
        var lambdaPattern = new Regex(@"<(\w+)>b__\d+_\d+", RegexOptions.None, RegexTimeout);
        var match = lambdaPattern.Match(methodName);

        if (match.Success)
        {
            string cleanName = match.Groups[1].Value;
            methodName = lambdaPattern.Replace(methodName, $"{cleanName}_Lambda");
        }

        return methodName;
    }

    /// <summary>
    /// Sanitizes a method name for use as a filename by removing invalid characters.
    /// </summary>
    private static string SanitizeMethodNameForFilename(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return string.Empty;
        }

        // Get invalid filename characters
        char[] invalidChars = Path.GetInvalidFileNameChars();

        // Also block additional characters that might cause issues
        char[] additionalInvalidChars = new[] { '<', '>', ':', '"', '/', '\\', '|', '?', '*', ' ' };

        // Combine both sets
        var allInvalidChars = invalidChars.Concat(additionalInvalidChars).Distinct().ToArray();

        // Replace invalid characters with underscores
        foreach (char c in allInvalidChars)
        {
            methodName = methodName.Replace(c, '_');
        }

        // Remove consecutive underscores
        while (methodName.Contains("__"))
        {
            methodName = methodName.Replace("__", "_");
        }

        // Trim underscores from start and end
        methodName = methodName.Trim('_');

        // Limit length to reasonable filename size (200 chars)
        if (methodName.Length > 200)
        {
            methodName = methodName.Substring(0, 200);
        }

        return methodName;
    }

    #endregion

    #region Quick Fix Templates

    /// <summary>
    /// Dictionary of common error types with suggested fix approaches.
    /// </summary>
    private static readonly Dictionary<string, string> QuickFixTemplates = new()
    {
        ["NullReferenceException"] = @"
**Suggested Fix Approach:**
1. Add null checks before accessing the object
2. Use null-conditional operator (?.) or null-coalescing operator (??)
3. Ensure object is initialized before use
4. Check if the object is properly retrieved from the data source",

        ["ArgumentNullException"] = @"
**Suggested Fix Approach:**
1. Add parameter validation at method entry
2. Use `ArgumentNullException.ThrowIfNull(parameter)` in .NET 6+
3. Consider using nullable reference types
4. Document which parameters cannot be null",

        ["TimeoutException"] = @"
**Suggested Fix Approach:**
1. Increase timeout value if operation is legitimately slow
2. Optimize slow database queries or operations
3. Add retry logic with exponential backoff
4. Check network connectivity and server health",

        ["FileNotFoundException"] = @"
**Suggested Fix Approach:**
1. Verify file path is correct
2. Check file exists before accessing: `File.Exists(path)`
3. Ensure proper file permissions
4. Handle file not found scenario gracefully",

        ["MySqlException"] = @"
**Suggested Fix Approach:**
1. Check connection string is valid
2. Verify database server is accessible
3. Review stored procedure parameters
4. Add retry logic for transient database errors
5. Check for deadlocks or connection pool exhaustion",

        ["InvalidOperationException"] = @"
**Suggested Fix Approach:**
1. Review operation prerequisites
2. Check object state before operation
3. Ensure UI operations run on UI thread
4. Verify collection is not modified during enumeration",

        ["IndexOutOfRangeException"] = @"
**Suggested Fix Approach:**
1. Validate array/list indices before access
2. Use LINQ methods like `.ElementAtOrDefault()`
3. Check collection Count/Length before indexing
4. Use foreach instead of index-based loops when possible",

        ["ObjectDisposedException"] = @"
**Suggested Fix Approach:**
1. Check if object is disposed before use
2. Don't use object after calling Dispose()
3. Review object lifetime management
4. Consider using `using` statements for IDisposable objects",

        ["UnauthorizedAccessException"] = @"
**Suggested Fix Approach:**
1. Check file/directory permissions
2. Run application with appropriate privileges
3. Verify user has access to the resource
4. Handle permission errors gracefully",

        ["FormatException"] = @"
**Suggested Fix Approach:**
1. Validate input format before parsing
2. Use `TryParse` instead of `Parse`
3. Provide clear format requirements to users
4. Sanitize input data before processing"
    };

    #endregion

    #region Prompt Generation

    /// <summary>
    /// Generates a Copilot-ready markdown prompt from a log entry.
    /// Extracts error details and applies quick fix template if available.
    /// </summary>
    /// <param name="logEntry">Log entry to generate prompt from.</param>
    /// <returns>Formatted markdown prompt string, or null if generation fails.</returns>
    public static string? GeneratePrompt(Model_LogEntry logEntry)
    {
        if (logEntry == null)
        {
            LoggingUtility.Log("[Service_PromptGenerator] GeneratePrompt: Log entry is null");
            return null;
        }

        try
        {
            // Extract method name from stack trace
            string? methodName = ExtractMethodName(logEntry.StackTrace);
            if (string.IsNullOrWhiteSpace(methodName))
            {
                LoggingUtility.Log("[Service_PromptGenerator] Could not extract method name from stack trace");
                return null;
            }

            // Build prompt using StringBuilder for efficiency
            var prompt = new StringBuilder();

            // Add header
            prompt.AppendLine("# Fix Error in Method");
            prompt.AppendLine();

            // Add error summary section
            prompt.AppendLine("## Error Summary");
            prompt.AppendLine();
            prompt.AppendLine($"**Timestamp:** {logEntry.Timestamp:yyyy-MM-dd HH:mm:ss}");
            
            string errorType = logEntry.ErrorType ?? logEntry.Severity ?? "Unknown Error";
            prompt.AppendLine($"**Error Type:** {errorType}");
            
            prompt.AppendLine($"**Method:** `{methodName}`");
            prompt.AppendLine();

            // Add message section
            if (!string.IsNullOrWhiteSpace(logEntry.Message))
            {
                prompt.AppendLine("## Error Message");
                prompt.AppendLine();
                prompt.AppendLine($"```");
                prompt.AppendLine(logEntry.Message);
                prompt.AppendLine($"```");
                prompt.AppendLine();
            }

            // Add stack trace section
            if (!string.IsNullOrWhiteSpace(logEntry.StackTrace))
            {
                prompt.AppendLine("## Stack Trace");
                prompt.AppendLine();
                prompt.AppendLine($"```");
                prompt.AppendLine(logEntry.StackTrace);
                prompt.AppendLine($"```");
                prompt.AppendLine();
            }

            // Add quick fix template if available
            string? quickFix = GetQuickFixTemplate(errorType);
            if (!string.IsNullOrWhiteSpace(quickFix))
            {
                prompt.AppendLine(quickFix);
                prompt.AppendLine();
            }

            // Add instructions for Copilot
            prompt.AppendLine("## Fix Instructions");
            prompt.AppendLine();
            prompt.AppendLine("Please analyze the error above and:");
            prompt.AppendLine("1. Identify the root cause of the error");
            prompt.AppendLine("2. Suggest specific code changes to fix the issue");
            prompt.AppendLine("3. Explain why the error occurred");
            prompt.AppendLine("4. Provide the corrected code with inline comments");
            prompt.AppendLine();

            // Add footer
            prompt.AppendLine("---");
            prompt.AppendLine($"*Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}*");

            string result = prompt.ToString();
            LoggingUtility.Log($"[Service_PromptGenerator] Generated prompt for method: {methodName}");
            
            return result;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[Service_PromptGenerator] Error generating prompt");
            LoggingUtility.LogApplicationError(ex);
            return null;
        }
    }

    /// <summary>
    /// Gets the quick fix template for a specific error type.
    /// Performs case-insensitive matching and handles common variations.
    /// </summary>
    /// <param name="errorType">Error type to lookup (e.g., "NullReferenceException").</param>
    /// <returns>Quick fix template text, or null if no template found.</returns>
    private static string? GetQuickFixTemplate(string errorType)
    {
        if (string.IsNullOrWhiteSpace(errorType))
        {
            return null;
        }

        // Try exact match first
        if (QuickFixTemplates.TryGetValue(errorType, out string? template))
        {
            return template;
        }

        // Try partial match (case-insensitive)
        foreach (var kvp in QuickFixTemplates)
        {
            if (errorType.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
            {
                return kvp.Value;
            }
        }

        return null;
    }

    /// <summary>
    /// Writes a generated prompt to a file in the Prompt Fixes directory.
    /// Creates the directory if it doesn't exist.
    /// </summary>
    /// <param name="methodName">Method name to use for filename.</param>
    /// <param name="promptContent">Prompt content to write.</param>
    /// <returns>True if file was written successfully; false otherwise.</returns>
    public static bool WritePromptToFile(string methodName, string promptContent)
    {
        if (string.IsNullOrWhiteSpace(methodName) || string.IsNullOrWhiteSpace(promptContent))
        {
            LoggingUtility.Log("[Service_PromptGenerator] WritePromptToFile: Invalid parameters");
            return false;
        }

        try
        {
            // Ensure Prompt Fixes directory exists
            if (!Helper_LogPath.CreatePromptFixesDirectory())
            {
                LoggingUtility.Log("[Service_PromptGenerator] Failed to create Prompt Fixes directory");
                return false;
            }

            // Get safe file path
            string? filePath = Helper_LogPath.GetPromptFilePath(methodName);
            if (filePath == null)
            {
                LoggingUtility.Log($"[Service_PromptGenerator] Failed to get prompt file path for method: {methodName}");
                return false;
            }

            // Write prompt to file
            File.WriteAllText(filePath, promptContent, Encoding.UTF8);
            LoggingUtility.Log($"[Service_PromptGenerator] Wrote prompt file: {filePath}");
            
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_PromptGenerator] Error writing prompt file for method: {methodName}");
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }

    #endregion
}
