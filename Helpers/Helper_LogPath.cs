using System.Diagnostics;

namespace MTM_WIP_Application_Winforms.Helpers;

/// <summary>
/// Provides security validation for log file paths to prevent directory traversal attacks
/// and ensure access is restricted to the application's log directory structure.
/// Also provides log file path generation.
/// </summary>
public static class Helper_LogPath
{
    #region Constants

    /// <summary>
    /// Primary log directory path (production location).
    /// </summary>
    public static readonly string PrimaryLogDirectory = Environment.UserName.Equals("johnk", StringComparison.OrdinalIgnoreCase)
        ? @"C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs"
        : @"X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs";

    /// <summary>
    /// Fallback log directory path (CommonApplicationData).
    /// Used when primary location is unavailable.
    /// </summary>
    private static readonly string FallbackLogDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
        "MTM_WIP_Application_Winforms",
        "Logs");

    /// <summary>
    /// Custom log directory set by the user at runtime.
    /// If set, this overrides the default directory logic.
    /// </summary>
    private static string? _customLogDirectory;

    /// <summary>
    /// Gets the base log directory, preferring primary location if it exists.
    /// </summary>
    private static string BaseLogDirectory
    {
        get
        {
            // Check custom directory first
            if (!string.IsNullOrWhiteSpace(_customLogDirectory) && Directory.Exists(_customLogDirectory))
            {
                return _customLogDirectory;
            }

            // Check if primary directory exists or is accessible
            if (Directory.Exists(PrimaryLogDirectory))
            {
                return PrimaryLogDirectory;
            }

            // Fall back to CommonApplicationData if primary is unavailable
            return FallbackLogDirectory;
        }
    }

    #endregion

    #region Log File Path

    /// <summary>
    /// Gets the base directory for logs.
    /// </summary>
    public static string LogDirectory => BaseLogDirectory;

    /// <summary>
    /// Gets the full path to the log file for the specified user.
    /// Creates the directory if it does not exist.
    /// </summary>
    /// <param name="server">The server name (unused in current logic but kept for signature compatibility).</param>
    /// <param name="userName">The user name.</param>
    /// <returns>The full path to the log file.</returns>
    public static async Task<string> GetLogFilePathAsync(string server, string userName)
    {
        try
        {
            string userDirectory = Path.Combine(LogDirectory, userName);

            using CancellationTokenSource cts = new(TimeSpan.FromSeconds(5));

            try
            {
                await Task.Run(() =>
                {
                    if (!Directory.Exists(userDirectory))
                    {
                        Directory.CreateDirectory(userDirectory);
                    }
                }, cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException($"Directory creation timed out for: {userDirectory}");
            }

            string timestamp = DateTime.Now.ToString("MM-dd-yyyy @ h-mm tt");
            string logFileName = $"{userName} {timestamp}.csv";
            return Path.Combine(userDirectory, logFileName);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Helper_LogPath] Error in GetLogFilePathAsync: {ex.Message}");
            throw;
        }
    }

    #endregion

    #region Path Validation

    /// <summary>
    /// Validates that a file path is within the allowed log directory structure
    /// and does not contain directory traversal attempts.
    /// Checks both primary and fallback log directories.
    /// </summary>
    /// <param name="filePath">Path to validate.</param>
    /// <returns>True if path is safe and within allowed directory; false otherwise.</returns>
    public static bool IsPathSafe(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return false;
        }

        try
        {
            // Get fully qualified paths for comparison
            string fullPath = Path.GetFullPath(filePath);
            string primaryFullPath = Path.GetFullPath(PrimaryLogDirectory);
            string fallbackFullPath = Path.GetFullPath(FallbackLogDirectory);

            // Ensure the path starts with either primary or fallback log directory
            bool isWithinPrimary = fullPath.StartsWith(primaryFullPath, StringComparison.OrdinalIgnoreCase);
            bool isWithinFallback = fullPath.StartsWith(fallbackFullPath, StringComparison.OrdinalIgnoreCase);
            bool isWithinCustom = !string.IsNullOrWhiteSpace(_customLogDirectory) && 
                                  fullPath.StartsWith(Path.GetFullPath(_customLogDirectory), StringComparison.OrdinalIgnoreCase);

            return isWithinPrimary || isWithinFallback || isWithinCustom;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Helper_LogPath] Error in IsPathSafe: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Validates that a directory path is within the allowed log directory structure.
    /// </summary>
    /// <param name="directoryPath">Directory path to validate.</param>
    /// <returns>True if directory is safe and within allowed structure; false otherwise.</returns>
    public static bool IsDirectorySafe(string directoryPath)
    {
        return IsPathSafe(directoryPath);
    }

    /// <summary>
    /// Gets the base log directory path where all user logs are stored.
    /// Returns primary location if accessible, otherwise fallback location.
    /// </summary>
    /// <returns>Full path to the base log directory.</returns>
    public static string GetBaseLogDirectory()
    {
        return BaseLogDirectory;
    }

    /// <summary>
    /// Gets all possible base log directories (primary and fallback).
    /// Used for enumerating users across both locations.
    /// </summary>
    /// <returns>Array of base log directory paths.</returns>
    public static string[] GetAllBaseLogDirectories()
    {
        var directories = new List<string>();

        // If custom directory is set, return ONLY that one to scope the view
        if (!string.IsNullOrWhiteSpace(_customLogDirectory) && Directory.Exists(_customLogDirectory))
        {
            directories.Add(_customLogDirectory);
            return directories.ToArray();
        }

        // Add primary if it exists
        if (Directory.Exists(PrimaryLogDirectory))
        {
            directories.Add(PrimaryLogDirectory);
        }

        // Add fallback if it exists
        if (Directory.Exists(FallbackLogDirectory))
        {
            directories.Add(FallbackLogDirectory);
        }

        // If neither exist, return the preferred one
        if (directories.Count == 0)
        {
            directories.Add(BaseLogDirectory);
        }

        return directories.ToArray();
    }

    /// <summary>
    /// Constructs a safe log directory path for a specific username.
    /// Checks both primary and fallback locations, returning the one that exists.
    /// </summary>
    /// <param name="username">Username to create directory path for.</param>
    /// <returns>Full path to the user's log directory, or null if username is invalid.</returns>
    public static string? GetUserLogDirectory(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        // Remove any path-unsafe characters from username
        string safeUsername = SanitizeFilename(username);

        if (string.IsNullOrWhiteSpace(safeUsername))
        {
            return null;
        }

        // Check custom directory first
        if (!string.IsNullOrWhiteSpace(_customLogDirectory) && Directory.Exists(_customLogDirectory))
        {
            string customUserDirectory = Path.Combine(_customLogDirectory, safeUsername);
            if (Directory.Exists(customUserDirectory) && IsDirectorySafe(customUserDirectory))
            {
                return customUserDirectory;
            }
            // If custom is set but user dir doesn't exist there, return null (don't fall back to default)
            return null;
        }

        // Check primary location first
        string primaryUserDirectory = Path.Combine(PrimaryLogDirectory, safeUsername);
        if (Directory.Exists(primaryUserDirectory) && IsDirectorySafe(primaryUserDirectory))
        {
            return primaryUserDirectory;
        }

        // Check fallback location
        string fallbackUserDirectory = Path.Combine(FallbackLogDirectory, safeUsername);
        if (Directory.Exists(fallbackUserDirectory) && IsDirectorySafe(fallbackUserDirectory))
        {
            return fallbackUserDirectory;
        }

        // If neither exists, return the preferred location (for creation)
        string userDirectory = Path.Combine(BaseLogDirectory, safeUsername);

        // Validate the constructed path
        if (!IsDirectorySafe(userDirectory))
        {
            return null;
        }

        return userDirectory;
    }

    #endregion

    #region Custom Directory Management

    /// <summary>
    /// Sets a temporary custom log directory for the current session.
    /// </summary>
    /// <param name="path">The full path to the custom log directory.</param>
    public static void SetCustomLogDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            _customLogDirectory = path;
        }
    }

    /// <summary>
    /// Clears the custom log directory, reverting to default behavior.
    /// </summary>
    public static void ClearCustomLogDirectory()
    {
        _customLogDirectory = null;
    }

    /// <summary>
    /// Gets the current custom log directory, if set.
    /// </summary>
    public static string? GetCustomLogDirectory()
    {
        return _customLogDirectory;
    }

    #endregion

    #region Filename Sanitization

    /// <summary>
    /// Removes path-unsafe characters from a filename or username.
    /// </summary>
    /// <param name="filename">Filename to sanitize.</param>
    /// <returns>Sanitized filename with invalid characters removed.</returns>
    private static string SanitizeFilename(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            return string.Empty;
        }

        // Get invalid filename characters for the current platform
        char[] invalidChars = Path.GetInvalidFileNameChars();

        // Also block path separators explicitly
        char[] additionalInvalidChars = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, ':' };

        // Combine both sets
        var allInvalidChars = invalidChars.Concat(additionalInvalidChars).Distinct().ToArray();

        // Remove all invalid characters
        string sanitized = string.Concat(filename.Split(allInvalidChars, StringSplitOptions.RemoveEmptyEntries));

        return sanitized;
    }

    #endregion

    #region Prompt Fixes Directory Management

    /// <summary>
    /// Gets the central Prompt Fixes directory path where all generated prompts are stored.
    /// </summary>
    /// <returns>Full path to the Prompt Fixes directory.</returns>
    public static string GetPromptFixesDirectory()
    {
        return Path.Combine(GetBaseLogDirectory(), "Prompt Fixes");
    }

    #endregion
}
