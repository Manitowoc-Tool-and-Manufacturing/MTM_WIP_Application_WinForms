using System.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Interface for UserFeedbackComments Data Access Object.
    /// </summary>
    public interface IDao_UserFeedbackComments
    {
        /// <summary>
        /// Inserts a new comment for a feedback record.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <param name="userId">The user ID submitting the comment.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="isInternalNote">Whether this is an internal note.</param>
        /// <returns>The new CommentID.</returns>
        Task<Model_Dao_Result<int>> InsertAsync(int feedbackId, int userId, string commentText, bool isInternalNote);

        /// <summary>
        /// Retrieves all comments for a specific feedback record.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <returns>DataTable containing comments.</returns>
        Task<Model_Dao_Result<DataTable>> GetByFeedbackIdAsync(int feedbackId);
    }
}
