namespace MTM_WIP_Application_Winforms.Models.Entities
{
    /// <summary>
    /// Represents a user feedback submission (Bug, Suggestion, etc.).
    /// Maps to UserFeedback table.
    /// </summary>
    public class Model_UserFeedback
    {
        /// <summary>
        /// Unique identifier for the feedback.
        /// </summary>
        public int FeedbackID { get; set; }

        /// <summary>
        /// Type of feedback (e.g., Bug, Suggestion).
        /// </summary>
        public string FeedbackType { get; set; } = string.Empty;

        /// <summary>
        /// Human-readable tracking number (e.g., BUG-2023-001).
        /// </summary>
        public string TrackingNumber { get; set; } = string.Empty;

        /// <summary>
        /// ID of the user who submitted the feedback.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Username of the submitter.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Full name of the submitter.
        /// </summary>
        public string? UserFullName { get; set; }

        /// <summary>
        /// Date and time of submission.
        /// </summary>
        public DateTime SubmissionDateTime { get; set; }

        /// <summary>
        /// Date and time of last update.
        /// </summary>
        public DateTime LastUpdatedDateTime { get; set; }

        /// <summary>
        /// Version of the application when feedback was submitted.
        /// </summary>
        public string? ApplicationVersion { get; set; }

        /// <summary>
        /// OS version of the client machine.
        /// </summary>
        public string? OSVersion { get; set; }

        /// <summary>
        /// Unique identifier of the client machine.
        /// </summary>
        public string? MachineIdentifier { get; set; }

        /// <summary>
        /// Name of the window/form where feedback originated.
        /// </summary>
        public string? WindowForm { get; set; }

        /// <summary>
        /// Active section or tab within the form.
        /// </summary>
        public string? ActiveSection { get; set; }

        /// <summary>
        /// Category of the feedback.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Custom category if "Other" is selected.
        /// </summary>
        public string? CustomCategory { get; set; }

        /// <summary>
        /// Severity level (e.g., Critical, High, Medium, Low).
        /// </summary>
        public string? Severity { get; set; }

        /// <summary>
        /// Priority level.
        /// </summary>
        public string? Priority { get; set; }

        /// <summary>
        /// Title or summary of the feedback.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Detailed description of the feedback.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Steps to reproduce the issue (for bugs).
        /// </summary>
        public string? StepsToReproduce { get; set; }

        /// <summary>
        /// Expected behavior (for bugs).
        /// </summary>
        public string? ExpectedBehavior { get; set; }

        /// <summary>
        /// Actual behavior (for bugs).
        /// </summary>
        public string? ActualBehavior { get; set; }

        /// <summary>
        /// Business justification (for suggestions).
        /// </summary>
        public string? BusinessJustification { get; set; }

        /// <summary>
        /// Users affected by the issue.
        /// </summary>
        public string? AffectedUsers { get; set; }

        /// <summary>
        /// Location 1 (e.g., Plant, Department).
        /// </summary>
        public string? Location1 { get; set; }

        /// <summary>
        /// Location 2 (e.g., Line, Station).
        /// </summary>
        public string? Location2 { get; set; }

        /// <summary>
        /// Expected consistency (e.g., Always, Intermittent).
        /// </summary>
        public string? ExpectedConsistency { get; set; }

        /// <summary>
        /// Current status of the feedback (e.g., New, In Progress).
        /// </summary>
        public string Status { get; set; } = "New";

        /// <summary>
        /// ID of the developer assigned to this feedback.
        /// </summary>
        public int? AssignedToDeveloperID { get; set; }

        /// <summary>
        /// Name of the assigned developer.
        /// </summary>
        public string? AssignedDeveloperName { get; set; }

        /// <summary>
        /// Notes from the developer.
        /// </summary>
        public string? DeveloperNotes { get; set; }

        /// <summary>
        /// Date and time when the feedback was resolved.
        /// </summary>
        public DateTime? ResolutionDateTime { get; set; }

        /// <summary>
        /// Indicates if this feedback is a duplicate.
        /// </summary>
        public bool IsDuplicate { get; set; }

        /// <summary>
        /// ID of the original feedback if this is a duplicate.
        /// </summary>
        public int? DuplicateOfFeedbackID { get; set; }
        
        /// <summary>
        /// List of comments associated with this feedback.
        /// </summary>
        public List<Model_UserFeedbackComments> Comments { get; set; } = new();
    }
}
