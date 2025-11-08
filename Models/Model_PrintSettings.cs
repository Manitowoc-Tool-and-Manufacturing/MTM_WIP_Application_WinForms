using System.Text.Json.Serialization;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// User's last-used print settings (persisted locally)
/// </summary>
public class Model_PrintSettings
{
    [JsonPropertyName("lastPrinterName")]
    public string? LastPrinterName { get; set; }

    [JsonPropertyName("lastOrientation")]
    public string LastOrientation { get; set; } = "Portrait";

    [JsonPropertyName("lastIsColor")]
    public bool LastIsColor { get; set; } = true;

    [JsonPropertyName("lastColumnSelections")]
    public Dictionary<string, List<string>> LastColumnSelections { get; set; } = new();

    [JsonPropertyName("lastColumnOrders")]
    public Dictionary<string, List<string>> LastColumnOrders { get; set; } = new();

    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; } = DateTime.Now;
}
