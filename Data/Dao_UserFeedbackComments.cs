using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Data Access Object for UserFeedbackComments operations.
    /// </summary>
    public class Dao_UserFeedbackComments : IDao_UserFeedbackComments
    {
        /// <inheritdoc/>
        public async Task<Model_Dao_Result<int>> InsertAsync(int feedbackId, int userId, string commentText, bool isInternalNote)
        {
            var parameters = new Dictionary<string, object>
            {
                { "FeedbackID", feedbackId },
                { "UserID", userId },
                { "CommentText", commentText },
                { "IsInternalNote", isInternalNote ? 1 : 0 }
            };

            var outputParams = new List<string> { "CommentID" };

            var result = await Helper_Database_StoredProcedure.ExecuteWithCustomOutputAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_comment_Insert",
                parameters,
                outputParams);

            if (result.IsSuccess && result.Data != null)
            {
                if (result.Data.TryGetValue("CommentID", out var idObj) && int.TryParse(idObj?.ToString(), out int id))
                {
                    return Model_Dao_Result<int>.Success(id);
                }
            }
            return Model_Dao_Result<int>.Failure(result.ErrorMessage ?? "Failed to insert comment");
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetByFeedbackIdAsync(int feedbackId)
        {
            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_comment_GetByFeedbackId",
                new Dictionary<string, object> { { "FeedbackID", feedbackId } });
        }
    }
}
