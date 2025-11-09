using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Persisted print preferences per data grid instance.
/// </summary>
public class Model_Print_Settings
{
    #region Properties

    /// <summary>
    /// Unique identifier for the grid this setting belongs to.
    /// </summary>
    public string GridName { get; set; } = string.Empty;

    /// <summary>
    /// Last selected printer name; null falls back to system default.
    /// </summary>
    public string? PrinterName { get; set; }
        = null;

    /// <summary>
    /// Last selected visible columns in display order.
    /// </summary>
    public List<string> VisibleColumns { get; set; } = new();

    /// <summary>
    /// Full column ordering for the grid.
    /// </summary>
    public List<string> ColumnOrder { get; set; } = new();

    /// <summary>
    /// Timestamp of last persisted change (UTC).
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    #endregion

    #region Persistence

    /// <summary>
    /// Load persisted settings for the specified grid name from AppData.
    /// Returns a default instance when no persisted file exists or on error.
    /// </summary>
    /// <param name="gridName">Unique grid identifier used as the filename (without extension).</param>
    /// <returns>Loaded <see cref="Model_Print_Settings"/> instance.</returns>
    public static Model_Print_Settings Load(string gridName)
    {
        try
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dir = Path.Combine(appData, "MTM", "PrintSettings");
            var file = Path.Combine(dir, gridName + ".json");

            if (!File.Exists(file))
            {
                return new Model_Print_Settings { GridName = gridName };
            }

            var json = File.ReadAllText(file);
            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            var settings = JsonSerializer.Deserialize<Model_Print_Settings>(json, opts);
            if (settings is null)
                return new Model_Print_Settings { GridName = gridName };

            settings.GridName = gridName; // ensure GridName preserved
            return settings;
        }
        catch
        {
            // On any error, return a default settings instance to avoid blocking the UI flow.
            return new Model_Print_Settings { GridName = gridName };
        }
    }

    /// <summary>
    /// Persist current settings to AppData\MTM\PrintSettings\{GridName}.json.
    /// Updates <see cref="LastModified"/> on success.
    /// </summary>
    public void Save()
    {
        if (string.IsNullOrWhiteSpace(GridName))
            throw new InvalidOperationException("GridName must be set before saving PrintSettings.");

        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dir = Path.Combine(appData, "MTM", "PrintSettings");
        Directory.CreateDirectory(dir);
        var file = Path.Combine(dir, GridName + ".json");

        var opts = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(this, opts);
        File.WriteAllText(file, json);
        LastModified = DateTime.UtcNow;
    }

    #endregion
}
