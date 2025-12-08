using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Models.Entities;
using MTM_WIP_Application_Winforms.Services.Logging;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Data Access Object for UserFeedback operations.
    /// </summary>
    public class Dao_UserFeedback : IDao_UserFeedback
    {
        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetAllAsync(Dictionary<string, object>? filters = null)
        {
            // Initialize all parameters with DBNull.Value to ensure they exist in the collection
            // This prevents "Parameter not found" errors since the SP requires all parameters to be present
            var parameters = new Dictionary<string, object>
            {
                { "FilterStatus", DBNull.Value },
                { "FilterFeedbackType", DBNull.Value },
                { "FilterUserID", DBNull.Value },
                { "FilterDateFrom", DBNull.Value },
                { "FilterDateTo", DBNull.Value },
                { "FilterAssignedDeveloperID", DBNull.Value },
                { "FilterCategory", DBNull.Value }
            };

            // Override with provided filters
            if (filters != null)
            {
                foreach (var kvp in filters)
                {
                    parameters[kvp.Key] = kvp.Value;
                }
            }

            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_GetAll",
                parameters);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> GetByUserIdAsync(int userId)
        {
            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_GetByUser",
                new Dictionary<string, object> { { "UserID", userId } });
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataRow>> GetByIdAsync(int feedbackId)
        {
            var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_GetById",
                new Dictionary<string, object> { { "FeedbackID", feedbackId } });

            if (result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0)
            {
                return Model_Dao_Result<DataRow>.Success(result.Data.Rows[0]);
            }
            return Model_Dao_Result<DataRow>.Failure("Feedback not found");
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<Model_UserFeedback>> InsertAsync(Model_UserFeedback model)
        {
            var parameters = new Dictionary<string, object>
            {
                { "FeedbackType", model.FeedbackType },
                { "UserID", model.UserID },
                { "WindowForm", model.WindowForm ?? (object)DBNull.Value },
                { "ActiveSection", model.ActiveSection ?? (object)DBNull.Value },
                { "Category", model.Category ?? (object)DBNull.Value },
                { "CustomCategory", model.CustomCategory ?? (object)DBNull.Value },
                { "Severity", model.Severity ?? (object)DBNull.Value },
                { "Priority", model.Priority ?? (object)DBNull.Value },
                { "Title", model.Title ?? (object)DBNull.Value },
                { "Description", model.Description ?? (object)DBNull.Value },
                { "StepsToReproduce", model.StepsToReproduce ?? (object)DBNull.Value },
                { "ExpectedBehavior", model.ExpectedBehavior ?? (object)DBNull.Value },
                { "ActualBehavior", model.ActualBehavior ?? (object)DBNull.Value },
                { "BusinessJustification", model.BusinessJustification ?? (object)DBNull.Value },
                { "AffectedUsers", model.AffectedUsers ?? (object)DBNull.Value },
                { "Location1", model.Location1 ?? (object)DBNull.Value },
                { "Location2", model.Location2 ?? (object)DBNull.Value },
                { "ExpectedConsistency", model.ExpectedConsistency ?? (object)DBNull.Value },
                { "ApplicationVersion", model.ApplicationVersion ?? (object)DBNull.Value },
                { "OSVersion", model.OSVersion ?? (object)DBNull.Value },
                { "MachineIdentifier", model.MachineIdentifier ?? (object)DBNull.Value }
            };

            var outputParams = new List<string> { "FeedbackID", "TrackingNumber" };

            var result = await Helper_Database_StoredProcedure.ExecuteWithCustomOutputAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_Insert",
                parameters,
                outputParams);

            if (result.IsSuccess && result.Data != null)
            {
                if (result.Data.TryGetValue("FeedbackID", out var idObj) && int.TryParse(idObj?.ToString(), out int id))
                {
                    model.FeedbackID = id;
                    if (result.Data.TryGetValue("TrackingNumber", out var trackObj))
                    {
                        model.TrackingNumber = trackObj?.ToString() ?? string.Empty;
                    }
                    return Model_Dao_Result<Model_UserFeedback>.Success(model);
                }
            }
            return Model_Dao_Result<Model_UserFeedback>.Failure(result.ErrorMessage ?? "Failed to insert feedback");
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result> UpdateStatusAsync(int feedbackId, string newStatus, int? assignedDeveloperId, string? notes, int modifiedByUserId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "FeedbackID", feedbackId },
                { "NewStatus", newStatus },
                { "AssignedToDeveloperID", assignedDeveloperId ?? (object)DBNull.Value },
                { "DeveloperNotes", notes ?? (object)DBNull.Value },
                { "ModifiedByUserID", modifiedByUserId }
            };

            return await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_UpdateStatus",
                parameters);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result> MarkAsDuplicateAsync(int feedbackId, int duplicateOfId, int modifiedByUserId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "FeedbackID", feedbackId },
                { "DuplicateOfFeedbackID", duplicateOfId },
                { "ModifiedByUserID", modifiedByUserId }
            };

            return await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_MarkDuplicate",
                parameters);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<DataTable>> ExportToCsvAsync(Dictionary<string, object>? filters = null)
        {
            var parameters = new Dictionary<string, object>();
            if (filters != null)
            {
                foreach (var kvp in filters)
                {
                    parameters[kvp.Key] = kvp.Value;
                }
            }

            return await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                Model_Application_Variables.ConnectionString,
                "md_feedback_ExportToCsv",
                parameters);
        }

        /// <inheritdoc/>
        public async Task<Model_Dao_Result<string>> GetTrackingNumberAsync(string feedbackType)
        {
            int maxRetries = 3;
            int currentRetry = 0;
            string lastError = string.Empty;

            while (currentRetry < maxRetries)
            {
                var parameters = new Dictionary<string, object>
                {
                    { "FeedbackType", feedbackType },
                    { "Year", DateTime.Now.Year }
                };

                var outputParams = new List<string> { "TrackingNumber" };

                var result = await Helper_Database_StoredProcedure.ExecuteWithCustomOutputAsync(
                    Model_Application_Variables.ConnectionString,
                    "sys_tracking_number_GetNext",
                    parameters,
                    outputParams);

                if (result.IsSuccess && result.Data != null && result.Data.TryGetValue("TrackingNumber", out var trackingNumber))
                {
                    return Model_Dao_Result<string>.Success(data: trackingNumber?.ToString() ?? string.Empty);
                }

                lastError = result.ErrorMessage;
                currentRetry++;
                await Task.Delay(100); // Small delay before retry
            }

            return Model_Dao_Result<string>.Failure($"Failed to generate tracking number after {maxRetries} attempts. Last error: {lastError}");
        }
    }
}
