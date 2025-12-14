namespace MTM_WIP_Application_Winforms.Models.DeveloperTools;

/// <summary>
/// Aggregated feedback statistics for dashboard display.
/// </summary>
public class Model_FeedbackSummary
{
    #region Properties

    /// <summary>
    /// Total feedback count.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Count with Status = 'New'.
    /// </summary>
    public int NewCount { get; set; }

    /// <summary>
    /// Count with Status = 'In Progress'.
    /// </summary>
    public int InProgressCount { get; set; }

    /// <summary>
    /// Count with Status = 'Resolved'.
    /// </summary>
    public int ResolvedCount { get; set; }

    /// <summary>
    /// Count of bugs.
    /// </summary>
    public int BugCount { get; set; }

    /// <summary>
    /// Count of feature requests.
    /// </summary>
    public int FeatureCount { get; set; }

    /// <summary>
    /// Count of questions.
    /// </summary>
    public int QuestionCount { get; set; }

    /// <summary>
    /// Most recent feedback submission time.
    /// </summary>
    public DateTime? LastSubmissionTime { get; set; }

    #endregion

    #region Computed Properties

    /// <summary>
    /// Count of open items (New + In Progress).
    /// </summary>
    public int OpenCount => NewCount + InProgressCount;

    /// <summary>
    /// Resolution rate as percentage.
    /// </summary>
    public double ResolutionRate => TotalCount > 0 
        ? Math.Round((double)ResolvedCount / TotalCount * 100, 1) 
        : 0;

    #endregion
}
