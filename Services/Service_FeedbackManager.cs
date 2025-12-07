using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Entities;
using System.Data;

namespace MTM_WIP_Application_Winforms.Services
{
    /// <summary>
    /// Interface for managing user feedback operations.
    /// </summary>
    public interface IService_FeedbackManager
    {
        /// <summary>
        /// Submits a new feedback entry.
        /// </summary>
        /// <param name="feedback">The feedback model.</param>
        /// <returns>The generated Tracking Number.</returns>
        Task<Model_Dao_Result<string>> SubmitFeedbackAsync(Model_UserFeedback feedback);

        /// <summary>
        /// Retrieves feedback submissions for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>DataTable containing feedback records.</returns>
        Task<Model_Dao_Result<DataTable>> GetUserSubmissionsAsync(int userId);

        /// <summary>
        /// Retrieves a single feedback submission with details.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <returns>The feedback model.</returns>
        Task<Model_Dao_Result<Model_UserFeedback>> GetSubmissionAsync(int feedbackId);

        /// <summary>
        /// Adds a comment to a feedback submission.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="commentText">The comment text.</param>
        /// <param name="isInternalNote">Whether it is an internal note.</param>
        /// <returns>The new CommentID.</returns>
        Task<Model_Dao_Result<int>> AddCommentAsync(int feedbackId, int userId, string commentText, bool isInternalNote);

        /// <summary>
        /// Updates the status of a feedback submission.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <param name="newStatus">The new status ID.</param>
        /// <param name="assignedDeveloperId">Optional developer ID.</param>
        /// <param name="notes">Optional developer notes.</param>
        /// <param name="modifiedByUserId">ID of the user making the change.</param>
        /// <returns>True if successful.</returns>
        Task<Model_Dao_Result<bool>> UpdateStatusAsync(int feedbackId, int newStatus, string? assignedDeveloperId, string? notes, string modifiedByUserId);

        /// <summary>
        /// Marks a feedback submission as a duplicate.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <param name="duplicateOfId">The original feedback ID.</param>
        /// <param name="modifiedByUserId">ID of the user making the change.</param>
        /// <returns>True if successful.</returns>
        Task<Model_Dao_Result<bool>> MarkDuplicateAsync(int feedbackId, int duplicateOfId, string modifiedByUserId);

        /// <summary>
        /// Exports feedback data to CSV.
        /// </summary>
        /// <param name="filters">Filter parameters.</param>
        /// <returns>DataTable for export.</returns>
        Task<Model_Dao_Result<DataTable>> ExportToCsvAsync(Dictionary<string, object> filters);

        /// <summary>
        /// Generates the next tracking number.
        /// </summary>
        /// <param name="feedbackType">The feedback type.</param>
        /// <returns>The new tracking number.</returns>
        Task<Model_Dao_Result<string>> GetTrackingNumberAsync(string feedbackType);

        /// <summary>
        /// Retrieves all active window form mappings.
        /// </summary>
        /// <returns>DataTable of mappings.</returns>
        Task<Model_Dao_Result<DataTable>> GetWindowMappingsAsync();

        /// <summary>
        /// Retrieves active control mappings for a window.
        /// </summary>
        /// <param name="windowFormMappingId">The window mapping ID.</param>
        /// <returns>DataTable of mappings.</returns>
        Task<Model_Dao_Result<DataTable>> GetControlMappingsAsync(int windowFormMappingId);
    }

    /// <summary>
    /// Service for managing user feedback operations.
    /// </summary>
    public class Service_FeedbackManager : IService_FeedbackManager
    {
        #region Fields

        private readonly IDao_UserFeedback _feedbackDao;
        private readonly IDao_UserFeedbackComments _commentsDao;
        private readonly IDao_WindowFormMapping _windowMappingDao;
        private readonly IDao_UserControlMapping _controlMappingDao;

        #endregion

        #region Constructors

        public Service_FeedbackManager(
            IDao_UserFeedback feedbackDao,
            IDao_UserFeedbackComments commentsDao,
            IDao_WindowFormMapping windowMappingDao,
            IDao_UserControlMapping controlMappingDao)
        {
            _feedbackDao = feedbackDao;
            _commentsDao = commentsDao;
            _windowMappingDao = windowMappingDao;
            _controlMappingDao = controlMappingDao;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<string>> SubmitFeedbackAsync(Model_UserFeedback feedback)
        {
            // TODO: Implement T023.1
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetUserSubmissionsAsync(int userId)
        {
            // TODO: Implement T023.2
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<Model_UserFeedback>> GetSubmissionAsync(int feedbackId)
        {
            // TODO: Implement T023.3
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<int>> AddCommentAsync(int feedbackId, int userId, string commentText, bool isInternalNote)
        {
            // TODO: Implement T023.4
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<bool>> UpdateStatusAsync(int feedbackId, int newStatus, string? assignedDeveloperId, string? notes, string modifiedByUserId)
        {
            // TODO: Implement T023.5
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<bool>> MarkDuplicateAsync(int feedbackId, int duplicateOfId, string modifiedByUserId)
        {
            // TODO: Implement T023.6
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> ExportToCsvAsync(Dictionary<string, object> filters)
        {
            // TODO: Implement T023.7
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<string>> GetTrackingNumberAsync(string feedbackType)
        {
            // TODO: Implement T023.8
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetWindowMappingsAsync()
        {
            // TODO: Implement T023.9
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetControlMappingsAsync(int windowFormMappingId)
        {
            // TODO: Implement T023.10
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers
        // Helper methods
        #endregion
    }
}
