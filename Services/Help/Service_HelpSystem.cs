using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models.Entities;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Implementation of the help system service.
    /// </summary>
    public class Service_HelpSystem : IHelpSystem
    {
        private List<Model_HelpCategory> _categories = new List<Model_HelpCategory>();
        private bool _isInitialized = false;
        private readonly Service_HelpTemplateEngine _templateEngine = new Service_HelpTemplateEngine();
        private readonly IDao_UserFeedback _feedbackDao;
        private readonly IDao_UserFeedbackComments _commentsDao;

        public Service_HelpSystem()
        {
            _feedbackDao = new Dao_UserFeedback();
            _commentsDao = new Dao_UserFeedbackComments();
        }

        /// <summary>
        /// Loads all help content from JSON files asynchronously.
        /// </summary>
        public async Task InitializeAsync()
        {
            if (_isInitialized) return;

            try
            {
                var loader = new Service_HelpContentLoader();
                // Assuming the JSON files are in a standard location relative to the executable
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "Help", "JSON");
                
                _categories = await loader.LoadCategoriesAsync(jsonPath);
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(InitializeAsync));
            }
        }

        /// <summary>
        /// Gets all loaded help categories sorted by Order.
        /// </summary>
        public IEnumerable<Model_HelpCategory> GetCategories()
        {
            return _categories.OrderBy(c => c.Order);
        }

        /// <summary>
        /// Retrieves a specific category by ID.
        /// </summary>
        public Model_HelpCategory? GetCategory(string categoryId)
        {
            return _categories.FirstOrDefault(c => c.CategoryId.Equals(categoryId, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Searches all topics for the given query string.
        /// </summary>
        /// <param name="query">Search terms</param>
        /// <returns>List of results sorted by relevance</returns>
        public IEnumerable<Model_HelpSearchResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query) || !_isInitialized)
            {
                return new List<Model_HelpSearchResult>();
            }

            var terms = query.ToLowerInvariant().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var results = new List<Model_HelpSearchResult>();

            foreach (var category in _categories)
            {
                foreach (var topic in category.Topics)
                {
                    int score = 0;
                    string title = topic.Title.ToLowerInvariant();
                    string summary = topic.Summary.ToLowerInvariant();
                    string content = topic.Content.ToLowerInvariant();
                    var keywords = topic.Keywords.Select(k => k.ToLowerInvariant()).ToList();

                    foreach (var term in terms)
                    {
                        if (title.Contains(term)) score += 10;
                        if (keywords.Any(k => k.Contains(term))) score += 8;
                        if (summary.Contains(term)) score += 5;
                        if (content.Contains(term)) score += 1;
                    }

                    if (score > 0)
                    {
                        results.Add(new Model_HelpSearchResult
                        {
                            Topic = topic,
                            Category = category,
                            RelevanceScore = score
                        });
                    }
                }
            }

            return results.OrderByDescending(r => r.RelevanceScore);
        }

        /// <summary>
        /// Generates the HTML for the Index page (category cards).
        /// </summary>
        public string GenerateIndexHtml()
        {
            return _templateEngine.GenerateIndexHtml(GetCategories());
        }

        /// <summary>
        /// Generates the HTML for a specific category and topic.
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <param name="topicId">Topic ID (optional, defaults to first)</param>
        public string GenerateTopicHtml(string categoryId, string? topicId = null)
        {
            var category = GetCategory(categoryId);
            if (category == null) return "<html><body><h1>Category Not Found</h1></body></html>";

            Model_HelpTopic? topic = null;
            if (!string.IsNullOrEmpty(topicId))
            {
                topic = category.Topics.FirstOrDefault(t => t.TopicId == topicId);
            }

            if (topic == null && category.Topics.Any())
            {
                topic = category.Topics.First();
            }

            if (topic == null) return "<html><body><h1>Topic Not Found</h1></body></html>";

            return _templateEngine.GenerateTopicHtml(category, topic);
        }

        /// <summary>
        /// Submits user feedback for a help topic.
        /// </summary>
        public async Task<Model_Dao_Result<Model_UserFeedback>> SubmitFeedbackAsync(Model_UserFeedback feedback)
        {
            try
            {
                var result = await _feedbackDao.InsertAsync(feedback);
                if (result.IsSuccess && result.Data != null)
                {
                    return Model_Dao_Result<Model_UserFeedback>.Success(result.Data);
                }
                return Model_Dao_Result<Model_UserFeedback>.Failure(result.ErrorMessage ?? "Unknown error during feedback submission");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(SubmitFeedbackAsync));
                return Model_Dao_Result<Model_UserFeedback>.Failure(ex.Message);
            }
        }

        /// <summary>
        /// Adds a comment to a feedback submission.
        /// </summary>
        public async Task<Model_Dao_Result<bool>> AddCommentAsync(Model_UserFeedbackComment comment)
        {
            try
            {
                var result = await _commentsDao.InsertAsync(comment.FeedbackID, comment.UserID, comment.CommentText, comment.IsInternalNote);
                if (result.IsSuccess)
                {
                    return Model_Dao_Result<bool>.Success(true);
                }
                return Model_Dao_Result<bool>.Failure(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(AddCommentAsync));
                return Model_Dao_Result<bool>.Failure(ex.Message);
            }
        }
    }
}
