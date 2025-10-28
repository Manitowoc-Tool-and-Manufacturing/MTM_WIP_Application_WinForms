using MTM_Inventory_Application.Logging;

namespace MTM_Inventory_Application.Helpers;

/// <summary>
/// Provides security validation for log file paths to prevent directory traversal attacks
/// and ensure access is restricted to the application's log directory structure.
/// </summary>
public static class Helper_LogPath
{
    #region Constants

    /// <summary>
    /// Primary log directory path (production location).
    /// </summary>
    private static readonly string PrimaryLogDirectory = Environment.UserName.Equals("johnk", StringComparison.OrdinalIgnoreCase)
        ? @"C:\Users\johnk\OneDrive\Documents\Work Folder\WIP App Logs"
        : @"X:\MH_RESOURCE\Material_Handler\MTM WIP App\Logs";

    /// <summary>
    /// Fallback log directory path (CommonApplicationData).
    /// Used when primary location is unavailable.
    /// </summary>
    private static readonly string FallbackLogDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
        "MTM_Inventory_Application",
        "Logs");

    /// <summary>
    /// Gets the base log directory, preferring primary location if it exists.
    /// </summary>
    private static string BaseLogDirectory
    {
        get
        {
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
            LoggingUtility.Log("[Helper_LogPath] Validation failed: Path is null or empty");
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

            if (!isWithinPrimary && !isWithinFallback)
            {
                LoggingUtility.Log($"[Helper_LogPath] Validation failed: Path outside allowed directories. Path: {fullPath}, Primary: {primaryFullPath}, Fallback: {fallbackFullPath}");
            }

            return isWithinPrimary || isWithinFallback;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Helper_LogPath] Path validation exception for: {filePath}");
            LoggingUtility.LogApplicationError(ex);
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
            LoggingUtility.Log("[Helper_LogPath] GetUserLogDirectory failed: Username is null or empty");
            return null;
        }

        // Remove any path-unsafe characters from username
        string safeUsername = SanitizeFilename(username);

        if (string.IsNullOrWhiteSpace(safeUsername))
        {
            LoggingUtility.Log($"[Helper_LogPath] GetUserLogDirectory failed: Username contains only invalid characters: {username}");
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
            LoggingUtility.Log($"[Helper_LogPath] GetUserLogDirectory failed security check: {userDirectory}");
            return null;
        }

        return userDirectory;
    }

    /// <summary>
    /// Constructs a safe full file path within a user's log directory.
    /// </summary>
    /// <param name="username">Username who owns the log file.</param>
    /// <param name="filename">Log filename.</param>
    /// <returns>Full path to the log file, or null if validation fails.</returns>
    public static string? GetUserLogFilePath(string username, string filename)
    {
        string? userDirectory = GetUserLogDirectory(username);
        if (userDirectory == null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(filename))
        {
            LoggingUtility.Log("[Helper_LogPath] GetUserLogFilePath failed: Filename is null or empty");
            return null;
        }

        // Remove any path-unsafe characters from filename
        string safeFilename = SanitizeFilename(filename);

        if (string.IsNullOrWhiteSpace(safeFilename))
        {
            LoggingUtility.Log($"[Helper_LogPath] GetUserLogFilePath failed: Filename contains only invalid characters: {filename}");
            return null;
        }

        string filePath = Path.Combine(userDirectory, safeFilename);

        // Validate the constructed path
        if (!IsPathSafe(filePath))
        {
            LoggingUtility.Log($"[Helper_LogPath] GetUserLogFilePath failed security check: {filePath}");
            return null;
        }

        return filePath;
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

    #region Directory Operations

    /// <summary>
    /// Ensures the base log directory exists, creating it if necessary.
    /// </summary>
    /// <returns>True if directory exists or was created successfully; false otherwise.</returns>
    public static bool EnsureBaseDirectoryExists()
    {
        try
        {
            if (!Directory.Exists(BaseLogDirectory))
            {
                Directory.CreateDirectory(BaseLogDirectory);
                LoggingUtility.Log($"[Helper_LogPath] Created base log directory: {BaseLogDirectory}");
            }
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Helper_LogPath] Failed to create base log directory: {BaseLogDirectory}");
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }

    /// <summary>
    /// Ensures a user's log directory exists, creating it if necessary.
    /// </summary>
    /// <param name="username">Username to create directory for.</param>
    /// <returns>True if directory exists or was created successfully; false otherwise.</returns>
    public static bool EnsureUserDirectoryExists(string username)
    {
        string? userDirectory = GetUserLogDirectory(username);
        if (userDirectory == null)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(userDirectory))
            {
                Directory.CreateDirectory(userDirectory);
                LoggingUtility.Log($"[Helper_LogPath] Created user log directory: {userDirectory}");
            }
            return true;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Helper_LogPath] Failed to create user log directory: {userDirectory}");
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }

    #endregion
}
