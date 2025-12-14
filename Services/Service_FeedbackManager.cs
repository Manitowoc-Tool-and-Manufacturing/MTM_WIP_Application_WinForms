using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Entities;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
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
        /// Retrieves all feedback submissions with optional filters.
        /// </summary>
        /// <param name="filters">Filter parameters.</param>
        /// <returns>DataTable of feedback.</returns>
        Task<Model_Dao_Result<DataTable>> GetAllAsync(Dictionary<string, object> filters);

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
        /// <param name="newStatus">The new status.</param>
        /// <param name="assignedDeveloperId">Optional developer ID.</param>
        /// <param name="notes">Optional developer notes.</param>
        /// <param name="modifiedByUserId">ID of the user making the change.</param>
        /// <returns>True if successful.</returns>
        Task<Model_Dao_Result<bool>> UpdateStatusAsync(int feedbackId, string newStatus, int? assignedDeveloperId, string? notes, string modifiedByUser);

        /// <summary>
        /// Updates the details of a feedback record.
        /// </summary>
        /// <param name="model">The feedback model containing updated details.</param>
        /// <returns>Success or failure.</returns>
        Task<Model_Dao_Result> UpdateDetailsAsync(Model_UserFeedback model);

        /// <summary>
        /// Marks a feedback submission as a duplicate.
        /// </summary>
        /// <param name="feedbackId">The feedback ID.</param>
        /// <param name="duplicateOfId">The original feedback ID.</param>
        /// <param name="modifiedByUserId">ID of the user making the change.</param>
        /// <returns>True if successful.</returns>
        Task<Model_Dao_Result<bool>> MarkDuplicateAsync(int feedbackId, int duplicateOfId, int modifiedByUserId);

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

        /// <summary>
        /// Updates the details of a feedback submission.
        /// </summary>
        /// <param name="feedback">The feedback model with updated details.</param>
        /// <returns>True if successful.</returns>
        Task<Model_Dao_Result<bool>> UpdateFeedbackDetailsAsync(Model_UserFeedback feedback);
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
        private readonly IService_EmailNotification _emailService;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor for legacy support.
        /// </summary>
        public Service_FeedbackManager() : this(
            new Dao_UserFeedback(),
            new Dao_UserFeedbackComments(),
            new Dao_WindowFormMapping(),
            new Dao_UserControlMapping(),
            new Service_EmailNotification())
        {
        }

        /// <summary>
        /// Constructor with dependency injection.
        /// </summary>
        /// <param name="feedbackDao">The feedback DAO.</param>
        /// <param name="commentsDao">The comments DAO.</param>
        /// <param name="windowMappingDao">The window mapping DAO.</param>
        /// <param name="controlMappingDao">The control mapping DAO.</param>
        /// <param name="emailService">The email service.</param>
        public Service_FeedbackManager(
            IDao_UserFeedback feedbackDao,
            IDao_UserFeedbackComments commentsDao,
            IDao_WindowFormMapping windowMappingDao,
            IDao_UserControlMapping controlMappingDao,
            IService_EmailNotification emailService)
        {
            _feedbackDao = feedbackDao;
            _commentsDao = commentsDao;
            _windowMappingDao = windowMappingDao;
            _controlMappingDao = controlMappingDao;
            _emailService = emailService;
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<string>> SubmitFeedbackAsync(Model_UserFeedback feedback)
        {
            try
            {
                // Validate inputs
                var validationResult = await ValidateFeedbackAsync(feedback);
                if (!validationResult.IsSuccess)
                {
                    return Model_Dao_Result<string>.Failure(validationResult.ErrorMessage);
                }
                
                // Sanitize HTML
                feedback.Description = Helper_HtmlSanitizer.Sanitize(feedback.Description ?? string.Empty);
                if (!string.IsNullOrEmpty(feedback.StepsToReproduce)) feedback.StepsToReproduce = Helper_HtmlSanitizer.Sanitize(feedback.StepsToReproduce);
                if (!string.IsNullOrEmpty(feedback.ExpectedBehavior)) feedback.ExpectedBehavior = Helper_HtmlSanitizer.Sanitize(feedback.ExpectedBehavior);
                if (!string.IsNullOrEmpty(feedback.ActualBehavior)) feedback.ActualBehavior = Helper_HtmlSanitizer.Sanitize(feedback.ActualBehavior);
                if (!string.IsNullOrEmpty(feedback.BusinessJustification)) feedback.BusinessJustification = Helper_HtmlSanitizer.Sanitize(feedback.BusinessJustification);
                if (!string.IsNullOrEmpty(feedback.ExpectedConsistency)) feedback.ExpectedConsistency = Helper_HtmlSanitizer.Sanitize(feedback.ExpectedConsistency);

                // Insert
                var result = await _feedbackDao.InsertAsync(feedback);
                
                if (result.IsSuccess && result.Data != null)
                {
                    LoggingUtility.Log(Enum_LogLevel.Information, "Feedback", $"Feedback submitted: {result.Data.TrackingNumber} (Type: {feedback.FeedbackType})", feedback.UserID.ToString());
                    
                    // Trigger email notification if Critical/High bug (Phase 7)
                    if (feedback.FeedbackType == "Bug Report" && (feedback.Severity == "Critical" || feedback.Severity == "High"))
                    {
                        string subject = $"[{feedback.Severity}] Bug Report: {feedback.TrackingNumber}";
                        string body = $"A new {feedback.Severity} bug has been reported.\n\n" +
                                      $"Tracking Number: {feedback.TrackingNumber}\n" +
                                      $"Title: {feedback.Title}\n" +
                                      $"Submitted By: {feedback.UserID}\n" +
                                      $"Window: {feedback.WindowForm}\n\n" +
                                      $"Description:\n{feedback.Description}";
                                      
                        _emailService.SendNotification(subject, body, feedback.Category ?? "All");
                    }

                    return Model_Dao_Result<string>.Success(data: result.Data.TrackingNumber);
                }
                
                return Model_Dao_Result<string>.Failure(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                LoggingUtility.Log(Enum_LogLevel.Error, "Feedback", "Error submitting feedback", feedback.UserID.ToString(), ex);
                return Model_Dao_Result<string>.Failure($"Error submitting feedback: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetAllAsync(Dictionary<string, object> filters)
        {
            return await _feedbackDao.GetAllAsync(filters);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetUserSubmissionsAsync(int userId)
        {
            return await _feedbackDao.GetByUserIdAsync(userId);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<Model_UserFeedback>> GetSubmissionAsync(int feedbackId)
        {
            var result = await _feedbackDao.GetByIdAsync(feedbackId);
            if (!result.IsSuccess || result.Data == null) return Model_Dao_Result<Model_UserFeedback>.Failure(result.ErrorMessage ?? "Feedback not found");

            try
            {
                var row = result.Data;
                var model = new Model_UserFeedback
                {
                    FeedbackID = Convert.ToInt32(row["FeedbackID"]),
                    FeedbackType = row["FeedbackType"].ToString() ?? string.Empty,
                    TrackingNumber = row["TrackingNumber"].ToString() ?? string.Empty,
                    UserID = Convert.ToInt32(row["UserID"]),
                    SubmissionDateTime = Convert.ToDateTime(row["SubmissionDateTime"]),
                    Title = row["Title"].ToString() ?? string.Empty,
                    Description = row["Description"].ToString() ?? string.Empty,
                    Status = row["Status"].ToString() ?? string.Empty,
                    Category = row["Category"] != DBNull.Value ? row["Category"].ToString() : null,
                    Severity = row["Severity"] != DBNull.Value ? row["Severity"].ToString() : null,
                    Priority = row["Priority"] != DBNull.Value ? row["Priority"].ToString() : null,
                    WindowForm = row["WindowForm"] != DBNull.Value ? row["WindowForm"].ToString() : null,
                    ActiveSection = row["ActiveSection"] != DBNull.Value ? row["ActiveSection"].ToString() : null,
                    StepsToReproduce = row["StepsToReproduce"] != DBNull.Value ? row["StepsToReproduce"].ToString() : null,
                    ExpectedBehavior = row["ExpectedBehavior"] != DBNull.Value ? row["ExpectedBehavior"].ToString() : null,
                    ActualBehavior = row["ActualBehavior"] != DBNull.Value ? row["ActualBehavior"].ToString() : null,
                    BusinessJustification = row["BusinessJustification"] != DBNull.Value ? row["BusinessJustification"].ToString() : null,
                    AffectedUsers = row["AffectedUsers"] != DBNull.Value ? row["AffectedUsers"].ToString() : null,
                    Location1 = row["Location1"] != DBNull.Value ? row["Location1"].ToString() : null,
                    Location2 = row["Location2"] != DBNull.Value ? row["Location2"].ToString() : null,
                    ExpectedConsistency = row["ExpectedConsistency"] != DBNull.Value ? row["ExpectedConsistency"].ToString() : null,
                    LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"]),
                    IsDuplicate = Convert.ToBoolean(row["IsDuplicate"]),
                    DuplicateOfFeedbackID = row["DuplicateOfFeedbackID"] != DBNull.Value ? Convert.ToInt32(row["DuplicateOfFeedbackID"]) : null
                };

                // Get comments
                var commentsResult = await _commentsDao.GetByFeedbackIdAsync(feedbackId);
                if (commentsResult.IsSuccess && commentsResult.Data != null)
                {
                    foreach (DataRow commentRow in commentsResult.Data.Rows)
                    {
                        model.Comments.Add(new Model_UserFeedbackComments
                        {
                            CommentID = Convert.ToInt32(commentRow["CommentID"]),
                            FeedbackID = Convert.ToInt32(commentRow["FeedbackID"]),
                            UserID = Convert.ToInt32(commentRow["UserID"]),
                            CommentText = commentRow["CommentText"].ToString() ?? string.Empty,
                            CommentDateTime = Convert.ToDateTime(commentRow["CommentDateTime"]),
                            IsInternalNote = Convert.ToBoolean(commentRow["IsInternalNote"]),
                            UserFullName = commentRow.Table.Columns.Contains("UserFullName") ? commentRow["UserFullName"].ToString() : null
                        });
                    }
                }
                
                return Model_Dao_Result<Model_UserFeedback>.Success(model);
            }
            catch (Exception ex)
            {
                 return Model_Dao_Result<Model_UserFeedback>.Failure($"Error mapping feedback: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<int>> AddCommentAsync(int feedbackId, int userId, string commentText, bool isInternalNote)
        {
            if (string.IsNullOrWhiteSpace(commentText)) return Model_Dao_Result<int>.Failure("Comment text is required.");
            
            commentText = Helper_HtmlSanitizer.Sanitize(commentText);
            
            return await _commentsDao.InsertAsync(feedbackId, userId, commentText, isInternalNote);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result> UpdateDetailsAsync(Model_UserFeedback feedback)
        {
            try
            {
                // Sanitize HTML
                feedback.Description = Helper_HtmlSanitizer.Sanitize(feedback.Description ?? string.Empty);
                if (!string.IsNullOrEmpty(feedback.StepsToReproduce)) feedback.StepsToReproduce = Helper_HtmlSanitizer.Sanitize(feedback.StepsToReproduce);
                if (!string.IsNullOrEmpty(feedback.ExpectedBehavior)) feedback.ExpectedBehavior = Helper_HtmlSanitizer.Sanitize(feedback.ExpectedBehavior);
                if (!string.IsNullOrEmpty(feedback.ActualBehavior)) feedback.ActualBehavior = Helper_HtmlSanitizer.Sanitize(feedback.ActualBehavior);
                if (!string.IsNullOrEmpty(feedback.BusinessJustification)) feedback.BusinessJustification = Helper_HtmlSanitizer.Sanitize(feedback.BusinessJustification);
                if (!string.IsNullOrEmpty(feedback.ExpectedConsistency)) feedback.ExpectedConsistency = Helper_HtmlSanitizer.Sanitize(feedback.ExpectedConsistency);

                var result = await _feedbackDao.UpdateDetailsAsync(feedback);
                
                if (result.IsSuccess)
                {
                    LoggingUtility.Log(Enum_LogLevel.Information, "Feedback", $"Feedback details updated: {feedback.TrackingNumber}", feedback.UserID.ToString());
                    return Model_Dao_Result.Success();
                }
                
                return Model_Dao_Result.Failure(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                LoggingUtility.Log(Enum_LogLevel.Error, "Feedback", "Error updating feedback details", feedback.UserID.ToString(), ex);
                return Model_Dao_Result.Failure($"Error updating feedback details: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        /// <inheritdoc/>
        public async Task<Model_Dao_Result<bool>> UpdateStatusAsync(int feedbackId, string newStatus, int? assignedDeveloperId, string? notes, string modifiedByUser)
        {
            // Get User ID
            int modifiedByUserId = 0;
            var userResult = await Dao_User.GetUserByUsernameAsync(modifiedByUser);
            if (userResult.IsSuccess && userResult.Data != null)
            {
                modifiedByUserId = Convert.ToInt32(userResult.Data["ID"]);
            }
            else
            {
                LoggingUtility.Log(Enum_LogLevel.Warning, "Feedback", $"Could not find UserID for {modifiedByUser}. Using 0.");
            }

            // Get old status for logging (FR-037)
            string oldStatus = "Unknown";
            int? oldDevId = null;

            var current = await GetSubmissionAsync(feedbackId);
            if (current.IsSuccess && current.Data != null) 
            {
                oldStatus = current.Data.Status;
                oldDevId = current.Data.AssignedToDeveloperID;
            }

            var result = await _feedbackDao.UpdateStatusAsync(feedbackId, newStatus, assignedDeveloperId, notes, modifiedByUserId);
            if (result.IsSuccess)
            {
                // T032.1 & T032.2: Log assignment change
                if (assignedDeveloperId.HasValue && assignedDeveloperId != oldDevId)
                {
                    LoggingUtility.Log(Enum_LogLevel.Information, "Feedback", $"Feedback {feedbackId} assigned to DevID: {assignedDeveloperId}", modifiedByUserId.ToString());
                }

                // Log status change
                if (newStatus != oldStatus)
                {
                    LoggingUtility.Log(Enum_LogLevel.Information, "Feedback", $"Feedback {feedbackId} status changed: {oldStatus} -> {newStatus}", modifiedByUserId.ToString());
                }

                return Model_Dao_Result<bool>.Success(true);
            }
            
            return Model_Dao_Result<bool>.Failure(result.ErrorMessage);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<bool>> MarkDuplicateAsync(int feedbackId, int duplicateOfId, int modifiedByUserId)
        {
            var result = await _feedbackDao.MarkAsDuplicateAsync(feedbackId, duplicateOfId, modifiedByUserId);
            if (result.IsSuccess)
            {
                LoggingUtility.Log(Enum_LogLevel.Information, "Feedback", $"Feedback {feedbackId} marked as duplicate of {duplicateOfId}", modifiedByUserId.ToString());
                return Model_Dao_Result<bool>.Success(true);
            }
            return Model_Dao_Result<bool>.Failure(result.ErrorMessage);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> ExportToCsvAsync(Dictionary<string, object> filters)
        {
            return await _feedbackDao.ExportToCsvAsync(filters);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<string>> GetTrackingNumberAsync(string feedbackType)
        {
            return await _feedbackDao.GetTrackingNumberAsync(feedbackType);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetWindowMappingsAsync()
        {
            return await _windowMappingDao.GetAllAsync(false);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetControlMappingsAsync(int windowFormMappingId)
        {
            return await _controlMappingDao.GetByWindowAsync(windowFormMappingId, false);
        }

        #endregion

        #region Helpers
        
        private async Task<Model_Dao_Result<bool>> ValidateFeedbackAsync(Model_UserFeedback feedback)
        {
            // T027.1 Description max length
            if (!string.IsNullOrEmpty(feedback.Description) && feedback.Description.Length > 50000)
            {
                string msg = "Description exceeds 50,000 characters.";
                Service_ErrorHandler.HandleValidationError(msg, "Description", nameof(SubmitFeedbackAsync));
                return Model_Dao_Result<bool>.Failure(msg);
            }
            
            // T027.2 Required fields
            if (string.IsNullOrWhiteSpace(feedback.Description))
            {
                 string msg = "Description is required.";
                 Service_ErrorHandler.HandleValidationError(msg, "Description", nameof(SubmitFeedbackAsync));
                 return Model_Dao_Result<bool>.Failure(msg);
            }
            
            if (feedback.FeedbackType == "Bug Report" && string.IsNullOrWhiteSpace(feedback.Severity))
            {
                 string msg = "Severity is required for Bug Reports.";
                 Service_ErrorHandler.HandleValidationError(msg, "Severity", nameof(SubmitFeedbackAsync));
                 return Model_Dao_Result<bool>.Failure(msg);
            }

            if (feedback.FeedbackType == "Suggestion" && string.IsNullOrWhiteSpace(feedback.Title))
            {
                 string msg = "Title is required for Suggestions.";
                 Service_ErrorHandler.HandleValidationError(msg, "Title", nameof(SubmitFeedbackAsync));
                 return Model_Dao_Result<bool>.Failure(msg);
            }
            
            // T027.3 Dropdown validity (WindowForm)
            if (!string.IsNullOrEmpty(feedback.WindowForm) && feedback.WindowForm != "Unknown")
            {
                var mappingsResult = await _windowMappingDao.GetAllAsync(true);
                if (mappingsResult.IsSuccess && mappingsResult.Data != null)
                {
                    bool exists = false;
                    foreach (DataRow row in mappingsResult.Data.Rows)
                    {
                        if (row["CodebaseName"].ToString() == feedback.WindowForm || 
                            row["UserFriendlyName"].ToString() == feedback.WindowForm)
                        {
                            exists = true;
                            break;
                        }
                    }
                    
                    if (!exists)
                    {
                        string msg = $"Invalid Window/Form: {feedback.WindowForm}";
                        Service_ErrorHandler.HandleValidationError(msg, "WindowForm", nameof(SubmitFeedbackAsync));
                        return Model_Dao_Result<bool>.Failure(msg);
                    }
                }
            }

            return Model_Dao_Result<bool>.Success(true);
        }

        public Task<Model_Dao_Result<bool>> UpdateFeedbackDetailsAsync(Model_UserFeedback feedback) => throw new NotImplementedException();

        #endregion
    }
}
