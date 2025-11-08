using System.Text.Json.Serialization;

namespace MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Represents a saved print configuration preset
/// </summary>
public class Model_PrintPreset
{
    [JsonPropertyName("presetName")]
    public string PresetName { get; set; } = string.Empty;

    [JsonPropertyName("printerName")]
    public string? PrinterName { get; set; }

    [JsonPropertyName("isColor")]
    public bool IsColor { get; set; } = true;

    [JsonPropertyName("orientation")]
    public string Orientation { get; set; } = "Portrait";

    [JsonPropertyName("pageRange")]
    public string PageRange { get; set; } = "All";

    [JsonPropertyName("pageFrom")]
    public int PageFrom { get; set; } = 1;

    [JsonPropertyName("pageTo")]
    public int PageTo { get; set; } = 1;

    [JsonPropertyName("selectedColumns")]
    public List<string> SelectedColumns { get; set; } = new();

    [JsonPropertyName("columnOrder")]
    public List<string> ColumnOrder { get; set; } = new();

    [JsonPropertyName("filters")]
    public List<PrintFilter> Filters { get; set; } = new();

    [JsonPropertyName("exportToPdf")]
    public bool ExportToPdf { get; set; }

    [JsonPropertyName("exportToExcel")]
    public bool ExportToExcel { get; set; }

    [JsonPropertyName("exportToImage")]
    public bool ExportToImage { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [JsonPropertyName("lastUsedDate")]
    public DateTime? LastUsedDate { get; set; }
}

/// <summary>
/// Represents a filter applied to print data
/// </summary>
public class PrintFilter
{
    [JsonPropertyName("columnName")]
    public string ColumnName { get; set; } = string.Empty;

    [JsonPropertyName("filterType")]
    public string FilterType { get; set; } = "Contains";

    [JsonPropertyName("filterValue")]
    public string FilterValue { get; set; } = string.Empty;

    [JsonPropertyName("dateFrom")]
    public DateTime? DateFrom { get; set; }

    [JsonPropertyName("dateTo")]
    public DateTime? DateTo { get; set; }

    public override string ToString()
    {
        return FilterType switch
        {
            "DateRange" => $"{ColumnName}: {DateFrom:yyyy-MM-dd} to {DateTo:yyyy-MM-dd}",
            "ShowSelected" => $"{ColumnName}: Selected rows only",
            _ => $"{ColumnName} {FilterType} '{FilterValue}'"
        };
    }
}
