using System.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data
{
    /// <summary>
    /// Interface for Email Notification Data Access Object.
    /// </summary>
    public interface IDao_EmailNotification
    {
        /// <summary>
        /// Retrieves recipient emails for a specific feedback category.
        /// </summary>
        /// <param name="category">The feedback category.</param>
        /// <returns>Semicolon-separated list of email addresses.</returns>
        Task<Model_Dao_Result<string>> GetRecipientsAsync(string category);
    }

    /// <summary>
    /// Data Access Object for Email Notification operations.
    /// </summary>
    public class Dao_EmailNotification : IDao_EmailNotification
    {
        /// <inheritdoc/>
        public async Task<Model_Dao_Result<string>> GetRecipientsAsync(string category)
        {
            var parameters = new Dictionary<string, object>
            {
                { "FeedbackCategory", category }
            };
            
            var outputParams = new List<string> { "RecipientEmails" };

            var result = await Helper_Database_StoredProcedure.ExecuteWithCustomOutputAsync(
                
                Model_Application_Variables.ConnectionString,
                "sys_email_notification_GetRecipients",
                parameters,
                outputParams);

            if (result.IsSuccess && result.Data != null)
            {
                if (result.Data.TryGetValue("RecipientEmails", out var emailsObj))
                {
                    return Model_Dao_Result<string>.Success(data: emailsObj?.ToString() ?? string.Empty);
                }
            }
            return Model_Dao_Result<string>.Failure(result.ErrorMessage ?? "Failed to get recipients");
        }
    }
}
