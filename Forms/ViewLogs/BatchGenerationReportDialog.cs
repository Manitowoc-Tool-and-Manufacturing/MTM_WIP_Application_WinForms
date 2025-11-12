using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using System.Diagnostics;

namespace MTM_WIP_Application_Winforms.Forms.ViewLogs;

/// <summary>
/// T067: Dialog showing detailed batch prompt generation results.
/// Displays summary statistics and per-prompt status breakdown.
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
public partial class BatchGenerationReportDialog : ThemedForm
{
    #region Fields

    private readonly List<BatchPromptResult> _results;
    private readonly int _createdCount;
    private readonly int _skippedCount;
    private readonly int _failedCount;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new batch generation report dialog.
    /// </summary>
    /// <param name="results">List of batch generation results for each prompt.</param>
    /// <param name="createdCount">Number of prompts created.</param>
    /// <param name="skippedCount">Number of prompts skipped (already exist).</param>
    /// <param name="failedCount">Number of prompts that failed to generate.</param>
    public BatchGenerationReportDialog(
        List<BatchPromptResult> results,
        int createdCount,
        int skippedCount,
        int failedCount)
    {
        _results = results ?? new List<BatchPromptResult>();
        _createdCount = createdCount;
        _skippedCount = skippedCount;
        _failedCount = failedCount;

        InitializeComponent();
    }

    #endregion

    #region Form Events

    /// <summary>
    /// Handles form load event. Populates summary and details grid.
    /// </summary>
    private void BatchGenerationReportDialog_Load(object? sender, EventArgs e)
    {
        try
        {
            
            PopulateSummary();
            PopulateDetailsGrid();
            
            LoggingUtility.Log("[BatchGenerationReportDialog] Report dialog loaded");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    #endregion

    #region Summary Population

    /// <summary>
    /// Populates the summary textbox with batch generation statistics.
    /// </summary>
    private void PopulateSummary()
    {
        var summary = new System.Text.StringBuilder();
        summary.AppendLine("Batch Prompt Generation Results");
        summary.AppendLine("================================");
        summary.AppendLine();
        summary.AppendLine($"✅ Created: {_createdCount} new prompts");
        summary.AppendLine($"⚠️ Skipped: {_skippedCount} (already exist)");
        summary.AppendLine($"❌ Failed: {_failedCount} (couldn't generate)");
        summary.AppendLine();
        summary.AppendLine($"Total processed: {_results.Count} unique errors");
        summary.AppendLine($"Success rate: {CalculateSuccessRate():F1}%");

        txtSummary.Text = summary.ToString();
    }

    /// <summary>
    /// Calculates success rate percentage.
    /// </summary>
    private double CalculateSuccessRate()
    {
        if (_results.Count == 0) return 0.0;
        return (_createdCount + _skippedCount) * 100.0 / _results.Count;
    }

    #endregion

    #region Details Grid Population

    /// <summary>
    /// Populates the DataGridView with per-prompt status breakdown.
    /// </summary>
    private void PopulateDetailsGrid()
    {
        dgvResults.Rows.Clear();
        
        foreach (var result in _results)
        {
            int rowIndex = dgvResults.Rows.Add(
                result.MethodName,
                result.Action,
                result.Reason
            );

            // Color-code rows based on action
            DataGridViewRow row = dgvResults.Rows[rowIndex];
            switch (result.Action)
            {
                case "Created":
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                    break;
                case "Skipped":
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    break;
                case "Failed":
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    break;
            }
        }

        // Auto-size columns to fit content
        dgvResults.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
    }

    #endregion

    #region Button Handlers

    /// <summary>
    /// Handles View Created Prompts button click. Opens Prompt Fixes folder in Explorer.
    /// </summary>
    private void btnViewPrompts_Click(object? sender, EventArgs e)
    {
        try
        {
            string? promptDirectory = Helper_LogPath.GetPromptFixesDirectory();
            
            if (string.IsNullOrWhiteSpace(promptDirectory))
            {
                MessageBox.Show(
                    "The Prompt Fixes directory path could not be determined.",
                    "Directory Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Ensure directory exists
            if (!Directory.Exists(promptDirectory))
            {
                bool created = Helper_LogPath.CreatePromptFixesDirectory();
                if (!created)
                {
                    MessageBox.Show(
                        "The Prompt Fixes directory does not exist and could not be created.",
                        "Directory Creation Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            // Open in File Explorer
            Process.Start(new ProcessStartInfo
            {
                FileName = promptDirectory,
                UseShellExecute = true,
                Verb = "open"
            });
            
            LoggingUtility.Log($"[BatchGenerationReportDialog] Opened Prompt Fixes directory: {promptDirectory}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            MessageBox.Show(
                $"Error opening Prompt Fixes folder:\n\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Handles Close button click. Closes the dialog.
    /// </summary>
    private void btnClose_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    #endregion
}

/// <summary>
/// T067: Represents the result of a single prompt generation in batch mode.
/// </summary>
public class BatchPromptResult
{
    /// <summary>
    /// Gets or sets the method name extracted from the error.
    /// </summary>
    public string MethodName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the action taken (Created, Skipped, Failed).
    /// </summary>
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the reason for the action.
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}
