using System.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Entities;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Interface for UserFeedback Data Access Object.
    /// </summary>
    public interface IDao_UserFeedback
    {
        /// <summary>
        /// Retrieves all feedback records matching the optional filters.
        /// </summary>
        /// <param name="filters">Dictionary of filter parameters (Status, FeedbackType, UserID, DateFrom, DateTo, AssignedDeveloperID, Category).</param>
        /// <returns>DataTable containing feedback records.</returns>
        Task<Model_Dao_Result<DataTable>> GetAllAsync(Dictionary<string, object>? filters = null);

        /// <summary>
        /// Retrieves all feedback records submitted by a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>DataTable containing feedback records.</returns>
        Task<Model_Dao_Result<DataTable>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Retrieves a single feedback record by ID.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <returns>DataRow containing the feedback record.</returns>
        Task<Model_Dao_Result<DataRow>> GetByIdAsync(int feedbackId);

        /// <summary>
        /// Inserts a new feedback record.
        /// </summary>
        /// <param name="model">The feedback model containing data to insert.</param>
        /// <returns>The inserted model with generated ID and TrackingNumber.</returns>
        Task<Model_Dao_Result<Model_UserFeedback>> InsertAsync(Model_UserFeedback model);

        /// <summary>
        /// Updates the status of a feedback record.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <param name="newStatus">The new status.</param>
        /// <param name="assignedDeveloperId">Optional developer ID to assign.</param>
        /// <param name="notes">Optional developer notes.</param>
        /// <param name="modifiedByUserId">The user ID performing the update.</param>
        /// <returns>Success or failure.</returns>
        Task<Model_Dao_Result> UpdateStatusAsync(int feedbackId, string newStatus, int? assignedDeveloperId, string? notes, int modifiedByUserId);

        /// <summary>
        /// Marks a feedback record as a duplicate of another.
        /// </summary>
        /// <param name="feedbackId">The feedback ID to mark as duplicate.</param>
        /// <param name="duplicateOfId">The original feedback ID.</param>
        /// <param name="modifiedByUserId">The user ID performing the update.</param>
        /// <returns>Success or failure.</returns>
        Task<Model_Dao_Result> MarkAsDuplicateAsync(int feedbackId, int duplicateOfId, int modifiedByUserId);

        /// <summary>
        /// Retrieves data for CSV export based on filters.
        /// </summary>
        /// <param name="filters">Dictionary of filter parameters.</param>
        /// <returns>DataTable containing export data.</returns>
        Task<Model_Dao_Result<DataTable>> ExportToCsvAsync(Dictionary<string, object>? filters = null);

        /// <summary>
        /// Generates the next tracking number for a feedback type.
        /// </summary>
        /// <param name="feedbackType">The feedback type.</param>
        /// <returns>The generated tracking number.</returns>
        Task<Model_Dao_Result<string>> GetTrackingNumberAsync(string feedbackType);
    }
}
