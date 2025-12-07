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
        /// <returns>The new FeedbackID.</returns>
        Task<Model_Dao_Result<int>> SubmitFeedbackAsync(Model_UserFeedback feedback);

        /// <summary>
        /// Retrieves feedback for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>DataTable containing feedback records.</returns>
        Task<Model_Dao_Result<DataTable>> GetUserFeedbackAsync(int userId);

        /// <summary>
        /// Retrieves control mappings for a specific window form.
        /// </summary>
        /// <param name="windowCodebaseName">The codebase name of the window.</param>
        /// <returns>DataTable containing control mappings.</returns>
        Task<Model_Dao_Result<DataTable>> GetControlMappingsAsync(string windowCodebaseName);
    }

    /// <summary>
    /// Service for managing user feedback operations.
    /// </summary>
    public class Service_FeedbackManager : IService_FeedbackManager
    {
        private readonly IDao_UserFeedback _feedbackDao;
        private readonly IDao_UserFeedbackComments _commentsDao;
        private readonly IDao_WindowFormMapping _windowMappingDao;
        private readonly IDao_UserControlMapping _controlMappingDao;

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

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<int>> SubmitFeedbackAsync(Model_UserFeedback feedback)
        {
            // TODO: Add validation and sanitization (T023)
            return await _feedbackDao.InsertAsync(feedback);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetUserFeedbackAsync(int userId)
        {
            return await _feedbackDao.GetByUserIdAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetControlMappingsAsync(string windowCodebaseName)
        {
            // First get the window mapping ID
            // Since we don't have a direct "GetByCodebaseName" in DAO yet (only GetAll), 
            // we might need to add it or filter GetAll.
            // For efficiency, we should probably add GetByCodebaseName to IDao_WindowFormMapping later.
            // For now, let's use GetAll and filter in memory (list is small, ~15 items).
            
            var windowsResult = await _windowMappingDao.GetAllAsync(includeInactive: false);
            if (!windowsResult.IsSuccess || windowsResult.Data == null)
            {
                return Model_Dao_Result<DataTable>.Failure("Failed to retrieve window mappings");
            }

            DataRow? windowRow = windowsResult.Data.AsEnumerable()
                .FirstOrDefault(r => r.Field<string>("CodebaseName") == windowCodebaseName);

            if (windowRow == null)
            {
                return Model_Dao_Result<DataTable>.Failure($"Window mapping not found for {windowCodebaseName}");
            }

            int windowId = Convert.ToInt32(windowRow["MappingID"]);
            return await _controlMappingDao.GetByWindowAsync(windowId, includeInactive: false);
        }
    }
}
