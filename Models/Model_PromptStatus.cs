using System.Text.Json.Serialization;

namespace MTM_Inventory_Application.Models;

/// <summary>
/// Represents the status of a Copilot prompt fix for a specific error method.
/// Used for tracking which errors have prompts, their completion status, and assignee.
/// Implements T064 - Prompt Status JSON management.
/// </summary>
public class Model_PromptStatus
{
    /// <summary>
    /// Gets or sets the prompt file path relative to Prompt Fixes directory.
    /// Example: "MethodName.md"
    /// </summary>
    [JsonPropertyName("promptFile")]
    public string PromptFile { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the method name extracted from error stack trace.
    /// Example: "LoadUserListAsync", "ExecuteStoredProcedure"
    /// </summary>
    [JsonPropertyName("methodName")]
    public string MethodName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current status of the prompt fix.
    /// </summary>
    [JsonPropertyName("status")]
    public PromptStatusEnum Status { get; set; } = PromptStatusEnum.New;

    /// <summary>
    /// Gets or sets the date the prompt was created.
    /// </summary>
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the date the status was last updated.
    /// </summary>
    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the developer assigned to fix this error.
    /// Optional field.
    /// </summary>
    [JsonPropertyName("assignee")]
    public string? Assignee { get; set; }

    /// <summary>
    /// Gets or sets additional notes about the fix status or implementation.
    /// Optional field.
    /// </summary>
    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}

/// <summary>
/// Enumeration of possible prompt fix statuses.
/// Used to track progress on error resolution.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PromptStatusEnum
{
    /// <summary>
    /// Prompt has been created but work has not started.
    /// </summary>
    New,

    /// <summary>
    /// Developer is actively working on the fix.
    /// </summary>
    InProgress,

    /// <summary>
    /// Fix has been implemented and error is resolved.
    /// </summary>
    Fixed,

    /// <summary>
    /// Error will not be fixed (by design, deprecated code, etc.).
    /// </summary>
    WontFix
}
