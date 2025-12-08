using System;

namespace MTM_WIP_Application_Winforms.Models.Entities
{
    /// <summary>
    /// Represents a user feedback submission (Bug, Suggestion, etc.).
    /// Maps to UserFeedback table.
    /// </summary>
    public class Model_UserFeedback
    {
        public int FeedbackID { get; set; }
        public string FeedbackType { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? UserFullName { get; set; }
        public DateTime SubmissionDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string? ApplicationVersion { get; set; }
        public string? OSVersion { get; set; }
        public string? MachineIdentifier { get; set; }
        public string? WindowForm { get; set; }
        public string? ActiveSection { get; set; }
        public string? Category { get; set; }
        public string? CustomCategory { get; set; }
        public string? Severity { get; set; }
        public string? Priority { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? StepsToReproduce { get; set; }
        public string? ExpectedBehavior { get; set; }
        public string? ActualBehavior { get; set; }
        public string? BusinessJustification { get; set; }
        public string? AffectedUsers { get; set; }
        public string? Location1 { get; set; }
        public string? Location2 { get; set; }
        public string? ExpectedConsistency { get; set; }
        public string Status { get; set; } = "New";
        public int? AssignedToDeveloperID { get; set; }
        public string? AssignedDeveloperName { get; set; }
        public string? DeveloperNotes { get; set; }
        public DateTime? ResolutionDateTime { get; set; }
        public bool IsDuplicate { get; set; }
        public int? DuplicateOfFeedbackID { get; set; }
        
        /// <summary>
        /// List of comments associated with this feedback.
        /// </summary>
        public List<Model_UserFeedbackComments> Comments { get; set; } = new();
    }
}
