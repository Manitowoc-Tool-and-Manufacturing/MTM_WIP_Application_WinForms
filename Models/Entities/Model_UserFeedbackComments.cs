namespace MTM_WIP_Application_Winforms.Models.Entities
{
    /// <summary>
    /// Represents a comment on a user feedback submission.
    /// Maps to UserFeedbackComments table.
    /// </summary>
    public class Model_UserFeedbackComments
    {
        /// <summary>
        /// Unique identifier for the comment.
        /// </summary>
        public int CommentID { get; set; }

        /// <summary>
        /// ID of the associated feedback.
        /// </summary>
        public int FeedbackID { get; set; }

        /// <summary>
        /// ID of the user who made the comment.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Full name of the user who made the comment.
        /// </summary>
        public string? UserFullName { get; set; }

        /// <summary>
        /// Date and time when the comment was made.
        /// </summary>
        public DateTime CommentDateTime { get; set; }

        /// <summary>
        /// The content of the comment.
        /// </summary>
        public string CommentText { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if the comment is an internal note (visible only to developers).
        /// </summary>
        public bool IsInternalNote { get; set; }
    }
}
