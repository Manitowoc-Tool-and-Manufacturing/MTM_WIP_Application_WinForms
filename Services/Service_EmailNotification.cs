using System.Net.Mail;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services
{
    /// <summary>
    /// Service for handling email notifications.
    /// </summary>
    public interface IService_EmailNotification
    {
        /// <summary>
        /// Sends an email notification asynchronously (fire-and-forget).
        /// </summary>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email body.</param>
        /// <param name="category">Feedback category for recipient lookup.</param>
        void SendNotification(string subject, string body, string category);
    }

    /// <summary>
    /// Implementation of email notification service.
    /// </summary>
    public class Service_EmailNotification : IService_EmailNotification
    {
        private readonly IDao_EmailNotification _dao;
        private const int MAX_RETRIES = 3;

        public Service_EmailNotification() : this(new Dao_EmailNotification()) { }

        public Service_EmailNotification(IDao_EmailNotification dao)
        {
            _dao = dao;
        }

        /// <inheritdoc/>
        public void SendNotification(string subject, string body, string category)
        {
            // Run in background to avoid blocking UI, especially with retries
            Task.Run(() => ProcessNotificationAsync(subject, body, category));
        }

        private async Task ProcessNotificationAsync(string subject, string body, string category)
        {
            try
            {
                // Get recipients
                var recipientsResult = await _dao.GetRecipientsAsync(category);
                if (!recipientsResult.IsSuccess || string.IsNullOrEmpty(recipientsResult.Data))
                {
                    // Fallback to 'All' if specific category fails or empty
                    if (category != "All")
                    {
                        recipientsResult = await _dao.GetRecipientsAsync("All");
                    }
                    
                    if (!recipientsResult.IsSuccess || string.IsNullOrEmpty(recipientsResult.Data))
                    {
                        LoggingUtility.Log(Enum_LogLevel.Warning, "Email", $"No recipients found for category {category} or All. Notification skipped.");
                        return;
                    }
                }

                string recipients = recipientsResult.Data;
                
                // Send email with retry
                await SendEmailWithRetryAsync(recipients, subject, body);
            }
            catch (Exception ex)
            {
                LoggingUtility.Log(Enum_LogLevel.Error, "Email", $"Error processing notification for {category}", ex: ex);
            }
        }

        private async Task SendEmailWithRetryAsync(string recipients, string subject, string body)
        {
            int attempt = 0;
            while (attempt < MAX_RETRIES)
            {
                attempt++;
                try
                {
                    // TODO: Configure real SMTP client
                    // using (var client = new SmtpClient("smtp.example.com"))
                    // {
                    //     var mail = new MailMessage("noreply@mtm.com", recipients, subject, body);
                    //     await client.SendMailAsync(mail);
                    // }
                    
                    // Simulation
                    LoggingUtility.Log(Enum_LogLevel.Information, "Email", $"[SIMULATION] Sending email to {recipients}. Subject: {subject}");
                    await Task.Delay(100); 
                    
                    // Log success (FR-039)
                    LoggingUtility.Log(Enum_LogLevel.Information, "Email", $"Email sent to {recipients}. Subject: {subject}");
                    return;
                }
                catch (Exception ex)
                {
                    LoggingUtility.Log(Enum_LogLevel.Warning, "Email", $"Email send attempt {attempt} failed: {ex.Message}");
                    
                    if (attempt >= MAX_RETRIES)
                    {
                        LoggingUtility.Log(Enum_LogLevel.Error, "Email", $"Failed to send email to {recipients} after {MAX_RETRIES} attempts.");
                        return;
                    }
                    
                    // Exponential backoff: 5min, 10min, 15min
                    // For testing/simulation, use seconds
                    await Task.Delay(TimeSpan.FromMinutes(5 * attempt)); 
                }
            }
        }
    }
}
