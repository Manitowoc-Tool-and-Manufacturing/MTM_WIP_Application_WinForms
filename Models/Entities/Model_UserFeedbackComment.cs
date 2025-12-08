namespace MTM_WIP_Application_Winforms.Models.Entities
{
    /// <summary>
    /// Represents a comment on a user feedback submission.
    /// Maps to UserFeedbackComments table.
    /// </summary>
    public class Model_UserFeedbackComment
    {
        public int CommentID { get; set; }
        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? UserFullName { get; set; }
        public DateTime CommentDateTime { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public bool IsInternalNote { get; set; }
    }
}
