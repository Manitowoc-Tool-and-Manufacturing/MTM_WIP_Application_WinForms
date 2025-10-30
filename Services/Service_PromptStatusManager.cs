using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using System.Text.Json;

namespace MTM_WIP_Application_Winforms.Services;

/// <summary>
/// Manages prompt status tracking via JSON file persistence.
/// Provides CRUD operations for prompt status records.
/// Implements T064 - Prompt Status JSON management.
/// </summary>
public static class Service_PromptStatusManager
{
    #region Constants

    /// <summary>
    /// JSON file name for storing prompt statuses.
    /// </summary>
    private const string StatusFileName = "prompt-status.json";

    /// <summary>
    /// Lock object for thread-safe file access.
    /// </summary>
    private static readonly object _fileLock = new();

    /// <summary>
    /// JSON serialization options for readable output.
    /// </summary>
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    #endregion

    #region Public Methods

    /// <summary>
    /// Loads all prompt statuses from JSON file.
    /// Creates empty file if it doesn't exist.
    /// </summary>
    /// <returns>List of prompt statuses, or empty list if file doesn't exist or read fails.</returns>
    public static List<Model_PromptStatus> LoadStatus()
    {
        try
        {
            string? filePath = GetStatusFilePath();
            
            if (filePath == null)
            {
                LoggingUtility.Log("[Service_PromptStatusManager] Failed to get status file path");
                return new List<Model_PromptStatus>();
            }

            lock (_fileLock)
            {
                // Create empty JSON file if it doesn't exist
                if (!File.Exists(filePath))
                {
                    LoggingUtility.Log($"[Service_PromptStatusManager] Creating new status file: {filePath}");
                    InitializeEmptyStatusFile(filePath);
                    return new List<Model_PromptStatus>();
                }

                // Read and deserialize JSON
                string json = File.ReadAllText(filePath);
                
                if (string.IsNullOrWhiteSpace(json))
                {
                    LoggingUtility.Log("[Service_PromptStatusManager] Status file is empty, returning empty list");
                    return new List<Model_PromptStatus>();
                }

                var statuses = JsonSerializer.Deserialize<List<Model_PromptStatus>>(json, _jsonOptions);
                
                LoggingUtility.Log($"[Service_PromptStatusManager] Loaded {statuses?.Count ?? 0} prompt statuses");
                
                return statuses ?? new List<Model_PromptStatus>();
            }
        }
        catch (JsonException ex)
        {
            LoggingUtility.Log("[Service_PromptStatusManager] JSON deserialization error loading statuses");
            LoggingUtility.LogApplicationError(ex);
            return new List<Model_PromptStatus>();
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[Service_PromptStatusManager] Error loading prompt statuses");
            LoggingUtility.LogApplicationError(ex);
            return new List<Model_PromptStatus>();
        }
    }

    /// <summary>
    /// Saves all prompt statuses to JSON file.
    /// Creates Prompt Fixes directory and file if they don't exist.
    /// </summary>
    /// <param name="statuses">List of prompt statuses to save.</param>
    /// <returns>True if save succeeded; false otherwise.</returns>
    public static bool SaveStatus(List<Model_PromptStatus> statuses)
    {
        if (statuses == null)
        {
            LoggingUtility.Log("[Service_PromptStatusManager] SaveStatus called with null statuses list");
            return false;
        }

        try
        {
            // Ensure Prompt Fixes directory exists
            if (!Helper_LogPath.CreatePromptFixesDirectory())
            {
                LoggingUtility.Log("[Service_PromptStatusManager] Failed to create Prompt Fixes directory");
                return false;
            }

            string? filePath = GetStatusFilePath();
            
            if (filePath == null)
            {
                LoggingUtility.Log("[Service_PromptStatusManager] Failed to get status file path");
                return false;
            }

            lock (_fileLock)
            {
                // Serialize to JSON with indentation
                string json = JsonSerializer.Serialize(statuses, _jsonOptions);
                
                // Write to file
                File.WriteAllText(filePath, json);
                
                LoggingUtility.Log($"[Service_PromptStatusManager] Saved {statuses.Count} prompt statuses to {filePath}");
                
                return true;
            }
        }
        catch (JsonException ex)
        {
            LoggingUtility.Log("[Service_PromptStatusManager] JSON serialization error saving statuses");
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[Service_PromptStatusManager] Error saving prompt statuses");
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }

    /// <summary>
    /// Gets the status for a specific method name.
    /// </summary>
    /// <param name="methodName">Method name to lookup.</param>
    /// <returns>Prompt status if found; null otherwise.</returns>
    public static Model_PromptStatus? GetStatus(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var statuses = LoadStatus();
        return statuses.FirstOrDefault(s => 
            s.MethodName.Equals(methodName, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Updates or creates a prompt status record.
    /// If status exists for method, updates it. Otherwise creates new record.
    /// </summary>
    /// <param name="methodName">Method name to update.</param>
    /// <param name="status">New status value.</param>
    /// <param name="assignee">Optional assignee name.</param>
    /// <param name="notes">Optional notes about the status.</param>
    /// <returns>True if update succeeded; false otherwise.</returns>
    public static bool UpdateStatus(string methodName, PromptStatusEnum status, string? assignee = null, string? notes = null)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            LoggingUtility.Log("[Service_PromptStatusManager] UpdateStatus called with empty method name");
            return false;
        }

        try
        {
            var statuses = LoadStatus();
            
            // Find existing status
            var existing = statuses.FirstOrDefault(s => 
                s.MethodName.Equals(methodName, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                // Update existing record
                existing.Status = status;
                existing.LastUpdated = DateTime.Now;
                
                if (assignee != null)
                {
                    existing.Assignee = assignee;
                }
                
                if (notes != null)
                {
                    existing.Notes = notes;
                }
                
                LoggingUtility.Log($"[Service_PromptStatusManager] Updated status for method: {methodName}");
            }
            else
            {
                // Create new record
                var newStatus = new Model_PromptStatus
                {
                    MethodName = methodName,
                    PromptFile = $"{methodName}.md",
                    Status = status,
                    CreatedDate = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Assignee = assignee,
                    Notes = notes
                };
                
                statuses.Add(newStatus);
                
                LoggingUtility.Log($"[Service_PromptStatusManager] Created new status for method: {methodName}");
            }

            return SaveStatus(statuses);
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_PromptStatusManager] Error updating status for method: {methodName}");
            LoggingUtility.LogApplicationError(ex);
            return false;
        }
    }

    /// <summary>
    /// Gets all prompt statuses.
    /// Convenience method that wraps LoadStatus() for consistency.
    /// </summary>
    /// <returns>List of all prompt statuses.</returns>
    public static List<Model_PromptStatus> GetAllStatuses()
    {
        return LoadStatus();
    }

    #endregion

    #region Private Helpers

    /// <summary>
    /// Gets the full file path for the status JSON file.
    /// </summary>
    /// <returns>Full file path, or null if path construction fails.</returns>
    private static string? GetStatusFilePath()
    {
        try
        {
            string? promptFixesDir = Helper_LogPath.GetPromptFixesDirectory();
            
            if (promptFixesDir == null)
            {
                return null;
            }

            return Path.Combine(promptFixesDir, StatusFileName);
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[Service_PromptStatusManager] Error getting status file path");
            LoggingUtility.LogApplicationError(ex);
            return null;
        }
    }

    /// <summary>
    /// Initializes an empty status file with an empty JSON array.
    /// </summary>
    /// <param name="filePath">Path to create the file at.</param>
    private static void InitializeEmptyStatusFile(string filePath)
    {
        try
        {
            var emptyList = new List<Model_PromptStatus>();
            string json = JsonSerializer.Serialize(emptyList, _jsonOptions);
            File.WriteAllText(filePath, json);
            
            LoggingUtility.Log($"[Service_PromptStatusManager] Initialized empty status file: {filePath}");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[Service_PromptStatusManager] Error initializing status file: {filePath}");
            LoggingUtility.LogApplicationError(ex);
        }
    }

    #endregion
}
