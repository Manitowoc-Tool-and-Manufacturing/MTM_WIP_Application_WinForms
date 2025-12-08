using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Models.Entities;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Defines the contract for the help system service.
    /// </summary>
    public interface IHelpSystem
    {
        /// <summary>
        /// Loads all help content from JSON files asynchronously.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Gets all loaded help categories sorted by Order.
        /// </summary>
        IEnumerable<Model_HelpCategory> GetCategories();

        /// <summary>
        /// Retrieves a specific category by ID.
        /// </summary>
        Model_HelpCategory? GetCategory(string categoryId);

        /// <summary>
        /// Searches all topics for the given query string.
        /// </summary>
        /// <param name="query">Search terms</param>
        /// <returns>List of results sorted by relevance</returns>
        IEnumerable<Model_HelpSearchResult> Search(string query);

        /// <summary>
        /// Generates the HTML for the Index page (category cards).
        /// </summary>
        string GenerateIndexHtml();

        /// <summary>
        /// Generates the HTML for a specific category and topic.
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <param name="topicId">Topic ID (optional, defaults to first)</param>
        string GenerateTopicHtml(string categoryId, string? topicId = null);

        /// <summary>
        /// Submits user feedback for a help topic.
        /// </summary>
        /// <param name="feedback">The feedback model.</param>
        /// <returns>Result indicating success or failure.</returns>
        Task<Model_Dao_Result<Model_UserFeedback>> SubmitFeedbackAsync(Model_UserFeedback feedback);

        /// <summary>
        /// Adds a comment to a feedback submission.
        /// </summary>
        /// <param name="comment">The comment model.</param>
        /// <returns>Result indicating success or failure.</returns>
        Task<Model_Dao_Result<bool>> AddCommentAsync(Model_UserFeedbackComment comment);
    }
}
