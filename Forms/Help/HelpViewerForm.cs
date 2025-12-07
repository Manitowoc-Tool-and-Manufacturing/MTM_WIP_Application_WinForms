using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Help;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Services.Logging;
using System.Text.Json;
using MTM_WIP_Application_Winforms.Models.Entities;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Data;

namespace MTM_WIP_Application_Winforms.Forms.Help
{
    /// <summary>
    /// Form for displaying help content using WebView2.
    /// </summary>
    public partial class HelpViewerForm : ThemedForm
    {
        private readonly IHelpSystem _helpSystem;
        private readonly Service_HelpTemplateEngine _templateEngine;
        private bool _isWebViewInitialized = false;
        private string? _pendingCategoryId;
        private string? _pendingTopicId;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpViewerForm"/> class.
        /// </summary>
        public HelpViewerForm()
        {
            InitializeComponent();
            _helpSystem = new Service_HelpSystem();
            _templateEngine = new Service_HelpTemplateEngine();

            this.Load += async (s, e) => await InitializeWebViewAsync();
        }

        /// <summary>
        /// Navigates the help viewer to a specific category and optional topic.
        /// </summary>
        /// <param name="categoryId">The ID of the category to open.</param>
        /// <param name="topicId">The ID of the topic to open (optional).</param>
        public void ShowHelp(string categoryId, string? topicId = null)
        {
            if (!_isWebViewInitialized)
            {
                _pendingCategoryId = categoryId;
                _pendingTopicId = topicId;
                return;
            }

            if (string.IsNullOrEmpty(categoryId))
            {
                LoadIndex();
                return;
            }

            Model_HelpCategory? category = _helpSystem.GetCategory(categoryId);
            if (category != null)
            {
                Model_HelpTopic? topic = null;
                if (!string.IsNullOrEmpty(topicId))
                {
                    topic = category.Topics.Find(t => t.TopicId == topicId);
                }

                if (topic == null && category.Topics.Count > 0)
                {
                    topic = category.Topics[0];
                }

                if (topic != null)
                {
                    string html = _templateEngine.GenerateTopicHtml(category, topic);
                    webView.NavigateToString(html);
                }
                else
                {
                    LoadIndex();
                }
            }
            else
            {
                LoadIndex();
            }
        }

        /// <summary>
        /// Initializes the WebView2 control asynchronously and loads initial content.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task InitializeWebViewAsync()
        {
            try
            {
                await webView.EnsureCoreWebView2Async();
                
                // T012.4 Enforce local-only template loading
                webView.CoreWebView2.Settings.IsScriptEnabled = true;
                webView.CoreWebView2.Settings.IsWebMessageEnabled = true;

                await _helpSystem.InitializeAsync();

                _isWebViewInitialized = true;

                webView.NavigationStarting += WebView_NavigationStarting;
                webView.WebMessageReceived += WebView_WebMessageReceived;

                if (!string.IsNullOrEmpty(_pendingCategoryId))
                {
                    ShowHelp(_pendingCategoryId, _pendingTopicId);
                    _pendingCategoryId = null;
                    _pendingTopicId = null;
                }
                else
                {
                    LoadIndex();
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium, callerName: nameof(InitializeWebViewAsync));
            }
        }

        /// <summary>
        /// Handles web messages received from JavaScript in the WebView2 control.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments containing the web message.</param>
        private void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                string json = e.TryGetWebMessageAsString();
                if (string.IsNullOrEmpty(json)) return;

                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("type", out JsonElement typeElement))
                {
                    string type = typeElement.GetString() ?? string.Empty;
                    LoggingUtility.Log($"WebView message received: {type}");

                    switch (type)
                    {
                        case "search":
                            if (root.TryGetProperty("query", out JsonElement queryElement))
                            {
                                PerformSearch(queryElement.GetString() ?? string.Empty);
                            }
                            break;

                        case "submitFeedback":
                            if (root.TryGetProperty("data", out JsonElement dataElement))
                            {
                                HandleFeedbackSubmission(dataElement);
                            }
                            break;

                        case "viewSubmissions":
                            HandleViewSubmissions();
                            break;

                        case "addComment":
                            if (root.TryGetProperty("data", out JsonElement commentData))
                            {
                                HandleAddComment(commentData);
                            }
                            break;

                        case "getWindowMappings":
                            HandleGetWindowMappings();
                            break;

                        case "getControlMappings":
                            if (root.TryGetProperty("windowId", out JsonElement windowIdElement))
                            {
                                HandleGetControlMappings(windowIdElement.GetString());
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Low, callerName: nameof(WebView_WebMessageReceived));
            }
        }

        private void HandleViewSubmissions()
        {
            // Scaffolding for Phase 6
            LoggingUtility.Log("View submissions requested (scaffolding)");
        }

        private async void HandleAddComment(JsonElement data)
        {
            try
            {
                int feedbackId = data.TryGetProperty("feedbackId", out var f) ? f.GetInt32() : 0;
                string? comment = data.TryGetProperty("comment", out var c) ? c.GetString() : null;

                if (feedbackId == 0 || string.IsNullOrEmpty(comment)) return;

                int userId = 0;
                var userResult = await Dao_System.GetUserIdByNameAsync(Model_Application_Variables.User);
                if (userResult.IsSuccess) userId = userResult.Data;

                var commentModel = new Model_UserFeedbackComment
                {
                    FeedbackID = feedbackId,
                    UserID = userId,
                    CommentText = comment!,
                    IsInternalNote = false,
                    CommentDateTime = DateTime.Now
                };

                var result = await _helpSystem.AddCommentAsync(commentModel);
                if (result.IsSuccess)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("alert('Comment added!');");
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to add comment: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                   callerName: nameof(HandleAddComment),
                   controlName: this.Name);
            }
        }

        private void HandleGetWindowMappings()
        {
            // Scaffolding for Phase 6
            LoggingUtility.Log("Get window mappings requested (scaffolding)");
        }

        private void HandleGetControlMappings(string? windowId)
        {
            // Scaffolding for Phase 6
            LoggingUtility.Log($"Get control mappings requested for {windowId} (scaffolding)");
        }

        /// <summary>
        /// Handles feedback submission from the help viewer.
        /// </summary>
        /// <param name="data">The feedback data.</param>
        private async void HandleFeedbackSubmission(JsonElement data)
        {
            try
            {
                string? topicId = data.TryGetProperty("topicId", out var t) ? t.GetString() : string.Empty;
                bool isHelpful = data.TryGetProperty("isHelpful", out var h) && h.GetBoolean();
                string? comment = data.TryGetProperty("comment", out var c) ? c.GetString() : null;
                string? categoryId = data.TryGetProperty("categoryId", out var cat) ? cat.GetString() : null;

                // Get User ID
                int userId = 0;
                var userResult = await Dao_System.GetUserIdByNameAsync(Model_Application_Variables.User);
                if (userResult.IsSuccess)
                {
                    userId = userResult.Data;
                }
                else
                {
                    Service_ErrorHandler.ShowUserError("Could not identify current user for feedback submission.");
                    return;
                }

                var feedback = new Model_UserFeedback
                {
                    FeedbackType = "HelpSystem",
                    UserID = userId,
                    ActiveSection = topicId,
                    Category = categoryId,
                    Title = $"Help Feedback: {topicId}",
                    Description = isHelpful ? "User found this helpful." : "User did not find this helpful.",
                    Status = "New",
                    SubmissionDateTime = DateTime.Now,
                    Severity = "Low",
                    Priority = "Low"
                };

                var result = await _helpSystem.SubmitFeedbackAsync(feedback);

                if (result.IsSuccess && result.Data != null)
                {
                    // If there is a comment, add it
                    if (!string.IsNullOrEmpty(comment))
                    {
                        var commentModel = new Model_UserFeedbackComment
                        {
                            FeedbackID = result.Data.FeedbackID,
                            UserID = userId,
                            CommentText = comment!,
                            IsInternalNote = false,
                            CommentDateTime = DateTime.Now
                        };
                        await _helpSystem.AddCommentAsync(commentModel);
                    }

                    // Notify UI of success and pass back the FeedbackID
                    await webView.CoreWebView2.ExecuteScriptAsync($"if(typeof onFeedbackSubmitted === 'function') {{ onFeedbackSubmitted({result.Data.FeedbackID}); }} else {{ alert('Thank you for your feedback!'); }}");
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to submit feedback: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    callerName: nameof(HandleFeedbackSubmission),
                    controlName: this.Name);
            }
        }

        /// <summary>
        /// Performs a search across help content and displays results.
        /// </summary>
        /// <param name="query">The search query string.</param>
        private void PerformSearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                LoadIndex();
                return;
            }

            List<Model_HelpSearchResult> results = _helpSystem.Search(query).ToList();
            string html = _templateEngine.GenerateSearchResultsHtml(results, query);
            webView.NavigateToString(html);
        }

        /// <summary>
        /// Loads the help system index page showing all categories.
        /// </summary>
        private void LoadIndex()
        {
            List<Model_HelpCategory> categories = _helpSystem.GetCategories().ToList();
            string html = _templateEngine.GenerateIndexHtml(categories);
            webView.NavigateToString(html);
        }

        /// <summary>
        /// Handles navigation starting events to intercept internal help:// URIs.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments containing navigation information.</param>
        private void WebView_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            string uri = e.Uri;
            if (uri.StartsWith("help://"))
            {
                e.Cancel = true;
                HandleInternalNavigation(uri);
            }
        }

        /// <summary>
        /// Handles internal navigation for help:// protocol URIs.
        /// </summary>
        /// <param name="uri">The help:// URI to navigate to.</param>
        private void HandleInternalNavigation(string uri)
        {
            string[] parts = uri.Replace("help://", "").Split('/');

            if (parts.Length > 0)
            {
                string type = parts[0];
                if (type == "category" && parts.Length > 1)
                {
                    string categoryId = parts[1];
                    Model_HelpCategory? category = _helpSystem.GetCategory(categoryId);
                    if (category != null && category.Topics.Count > 0)
                    {
                        Model_HelpTopic topic = category.Topics[0];
                        string html = _templateEngine.GenerateTopicHtml(category, topic);
                        webView.NavigateToString(html);
                    }
                }
                else if (type == "topic" && parts.Length > 2)
                {
                    string categoryId = parts[1];
                    string topicId = parts[2];
                    Model_HelpCategory? category = _helpSystem.GetCategory(categoryId);
                    if (category != null)
                    {
                        Model_HelpTopic? topic = category.Topics.Find(t => t.TopicId == topicId);
                        if (topic != null)
                        {
                            string html = _templateEngine.GenerateTopicHtml(category, topic);
                            webView.NavigateToString(html);
                        }
                    }
                }
            }
        }
    }
}
