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
using System.Data;

namespace MTM_WIP_Application_Winforms.Forms.Help
{
    /// <summary>
    /// Form for displaying help content using WebView2.
    /// </summary>
    public partial class HelpViewerForm : ThemedForm
    {
        private readonly IHelpSystem _helpSystem;
        private readonly Service_HelpTemplateEngine _templateEngine;
        private readonly IService_FeedbackManager _feedbackManager;
        private static HelpViewerForm? _instance;
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
            _feedbackManager = new Service_FeedbackManager();

            this.Load += async (s, e) => await InitializeWebViewAsync();
        }

        /// <summary>
        /// Gets the singleton instance of the HelpViewerForm.
        /// Creates a new instance if one does not exist or is disposed.
        /// </summary>
        /// <returns>The singleton HelpViewerForm instance.</returns>
        public static HelpViewerForm GetInstance()
        {
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new HelpViewerForm();
            }
            return _instance;
        }

        /// <summary>
        /// Brings the form to the front and navigates to the specified category and topic.
        /// </summary>
        /// <param name="category">The category ID.</param>
        /// <param name="topic">The topic ID (optional).</param>
        public void BringToFrontAndNavigate(string category, string? topic = null)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Show();
            this.BringToFront();
            this.Activate();
            this.ShowHelp(category, topic);
        }

        /// <inheritdoc/>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _instance = null;
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
                // Robustly handle both string and object messages
                string json = e.WebMessageAsJson;
                if (string.IsNullOrEmpty(json)) return;

                using JsonDocument doc = JsonDocument.Parse(json);
                JsonElement root = doc.RootElement;

                // If the message was sent as a string (JSON.stringify), it comes as a JSON string
                if (root.ValueKind == JsonValueKind.String)
                {
                    string innerJson = root.GetString() ?? string.Empty;
                    if (string.IsNullOrEmpty(innerJson)) return;
                    
                    using JsonDocument innerDoc = JsonDocument.Parse(innerJson);
                    ProcessWebMessage(innerDoc.RootElement);
                }
                else if (root.ValueKind == JsonValueKind.Object)
                {
                    ProcessWebMessage(root);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Low, callerName: nameof(WebView_WebMessageReceived));
            }
        }

        private void ProcessWebMessage(JsonElement root)
        {
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

                    case "getSubmissionDetails":
                        if (root.TryGetProperty("feedbackId", out JsonElement feedbackIdElement))
                        {
                            int feedbackId = 0;
                            if (feedbackIdElement.ValueKind == JsonValueKind.Number)
                            {
                                feedbackId = feedbackIdElement.GetInt32();
                            }
                            else if (feedbackIdElement.ValueKind == JsonValueKind.String)
                            {
                                int.TryParse(feedbackIdElement.GetString(), out feedbackId);
                            }
                            
                            if (feedbackId > 0)
                            {
                                HandleGetSubmissionDetails(feedbackId);
                            }
                        }
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
                        // Handle both 'windowId' (legacy/spec) and 'windowFormMappingId' (actual JS)
                        if (root.TryGetProperty("windowId", out JsonElement windowIdElement))
                        {
                            HandleGetControlMappings(windowIdElement.GetString());
                        }
                        else if (root.TryGetProperty("windowFormMappingId", out JsonElement mappingIdElement))
                        {
                            HandleGetControlMappings(mappingIdElement.GetString());
                        }
                        break;
                }
            }
        }

        private async void HandleViewSubmissions()
        {
            try
            {
                int userId = 0;
                var userResult = await Dao_System.GetUserIdByNameAsync(Model_Application_Variables.User);
                if (userResult.IsSuccess) userId = userResult.Data;

                var result = await _feedbackManager.GetUserSubmissionsAsync(userId);
                if (result.IsSuccess && result.Data != null)
                {
                    var submissions = new List<Dictionary<string, object>>();
                    foreach (DataRow row in result.Data.Rows)
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (DataColumn col in result.Data.Columns)
                        {
                            dict[col.ColumnName] = row[col];
                        }
                        submissions.Add(dict);
                    }

                    string json = JsonSerializer.Serialize(submissions);
                    await webView.CoreWebView2.ExecuteScriptAsync($"if(typeof onSubmissionsLoaded === 'function') {{ onSubmissionsLoaded({json}); }}");
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to load submissions: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(HandleViewSubmissions), controlName: this.Name);
            }
        }

        private async void HandleGetSubmissionDetails(int feedbackId)
        {
            try
            {
                var result = await _feedbackManager.GetSubmissionAsync(feedbackId);
                if (result.IsSuccess)
                {
                    string json = JsonSerializer.Serialize(result.Data);
                    await webView.CoreWebView2.ExecuteScriptAsync($"if(typeof onSubmissionDetailsLoaded === 'function') {{ onSubmissionDetailsLoaded({json}); }}");
                }
                else
                {
                    Service_ErrorHandler.ShowUserError($"Failed to load submission details: {result.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, callerName: nameof(HandleGetSubmissionDetails), controlName: this.Name);
            }
        }

        private async void HandleAddComment(JsonElement data)
        {
            try
            {
                int feedbackId = 0;
                if (data.TryGetProperty("feedbackId", out var f))
                {
                    if (f.ValueKind == JsonValueKind.Number)
                    {
                        feedbackId = f.GetInt32();
                    }
                    else if (f.ValueKind == JsonValueKind.String)
                    {
                        int.TryParse(f.GetString(), out feedbackId);
                    }
                }
                
                string? comment = data.TryGetProperty("comment", out var c) ? c.GetString() : null;

                if (feedbackId == 0 || string.IsNullOrEmpty(comment)) return;

                int userId = 0;
                var userResult = await Dao_System.GetUserIdByNameAsync(Model_Application_Variables.User);
                if (userResult.IsSuccess) userId = userResult.Data;

                var result = await _feedbackManager.AddCommentAsync(feedbackId, userId, comment!, false);
                if (result.IsSuccess)
                {
                    await webView.CoreWebView2.ExecuteScriptAsync("alert('Comment added!'); if(typeof refreshSubmissions === 'function') { refreshSubmissions(); }");
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

        private async void HandleGetWindowMappings()
        {
            try
            {
                var result = await _feedbackManager.GetWindowMappingsAsync();
                if (result.IsSuccess && result.Data != null)
                {
                    var mappings = new List<Dictionary<string, object>>();
                    foreach (DataRow row in result.Data.Rows)
                    {
                        var dict = new Dictionary<string, object>();
                        foreach (DataColumn col in result.Data.Columns)
                        {
                            dict[col.ColumnName] = row[col];
                        }
                        mappings.Add(dict);
                    }
                    string json = JsonSerializer.Serialize(mappings);
                    await webView.CoreWebView2.ExecuteScriptAsync($"if(typeof onWindowMappingsLoaded === 'function') {{ onWindowMappingsLoaded({json}); }}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, callerName: nameof(HandleGetWindowMappings));
            }
        }

        private async void HandleGetControlMappings(string? windowIdStr)
        {
            try
            {
                if (int.TryParse(windowIdStr, out int windowId))
                {
                    var result = await _feedbackManager.GetControlMappingsAsync(windowId);
                    if (result.IsSuccess && result.Data != null)
                    {
                        var mappings = new List<Dictionary<string, object>>();
                        foreach (DataRow row in result.Data.Rows)
                        {
                            var dict = new Dictionary<string, object>();
                            foreach (DataColumn col in result.Data.Columns)
                            {
                                dict[col.ColumnName] = row[col];
                            }
                            mappings.Add(dict);
                        }
                        string json = JsonSerializer.Serialize(mappings);
                        await webView.CoreWebView2.ExecuteScriptAsync($"if(typeof onControlMappingsLoaded === 'function') {{ onControlMappingsLoaded({json}); }}");
                    }
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Low, callerName: nameof(HandleGetControlMappings));
            }
        }

        /// <summary>
        /// Handles feedback submission from the help viewer.
        /// </summary>
        /// <param name="data">The feedback data.</param>
        private async void HandleFeedbackSubmission(JsonElement data)
        {
            // Clone the data to ensure it persists across async calls (JsonDocument disposal)
            JsonElement safeData = data.Clone();

            try
            {
                // Check if this is a legacy "Help Feedback" (isHelpful) or new Contact Support
                bool isLegacy = safeData.TryGetProperty("isHelpful", out _);

                int userId = 0;
                var userResult = await Dao_System.GetUserIdByNameAsync(Model_Application_Variables.User);
                if (userResult.IsSuccess) userId = userResult.Data;

                Model_UserFeedback feedback;

                if (isLegacy)
                {
                    string? topicId = safeData.TryGetProperty("topicId", out var t) ? t.GetString() : string.Empty;
                    bool isHelpful = safeData.TryGetProperty("isHelpful", out var h) && h.GetBoolean();
                    string? comment = safeData.TryGetProperty("comment", out var c) ? c.GetString() : null;
                    string? categoryId = safeData.TryGetProperty("categoryId", out var cat) ? cat.GetString() : null;

                    feedback = new Model_UserFeedback
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
                    
                    // Append comment to description if present for legacy
                    if (!string.IsNullOrEmpty(comment))
                    {
                         feedback.Description += $"\nComment: {comment}";
                    }
                }
                else
                {
                    // New Contact Support forms
                    // Map fields based on what's available
                    feedback = new Model_UserFeedback
                    {
                        UserID = userId,
                        SubmissionDateTime = DateTime.Now,
                        Status = "New"
                    };

                    if (safeData.TryGetProperty("bugCategory", out _))
                    {
                        feedback.FeedbackType = "Bug Report";
                        feedback.Category = safeData.GetProperty("bugCategory").GetString();
                        feedback.Severity = safeData.GetProperty("severity").GetString();
                        feedback.Priority = feedback.Severity; // Default priority to severity
                        feedback.ActiveSection = safeData.TryGetProperty("activeSection", out var s) ? s.GetString() : null;
                        
                        string windowForm = safeData.TryGetProperty("windowForm", out var w) ? w.GetString() ?? "Unknown" : "Unknown";
                        feedback.Title = $"Bug: {feedback.Category} in {windowForm}";
                        
                        feedback.Description = safeData.GetProperty("description").GetString() ?? "";
                        feedback.StepsToReproduce = safeData.TryGetProperty("stepsToReproduce", out var st) ? st.GetString() : null;
                        feedback.ExpectedBehavior = safeData.TryGetProperty("expectedBehavior", out var ex) ? ex.GetString() : null;
                        feedback.ActualBehavior = safeData.TryGetProperty("actualBehavior", out var ac) ? ac.GetString() : null;
                    }
                    else if (safeData.TryGetProperty("suggestionCategory", out _))
                    {
                        feedback.FeedbackType = "Suggestion";
                        feedback.Category = safeData.GetProperty("suggestionCategory").GetString();
                        feedback.Priority = safeData.GetProperty("priority").GetString();
                        feedback.Title = safeData.GetProperty("title").GetString();
                        
                        feedback.Description = safeData.GetProperty("description").GetString() ?? "";
                        feedback.BusinessJustification = safeData.TryGetProperty("businessJustification", out var bj) ? bj.GetString() : null;
                        feedback.AffectedUsers = safeData.TryGetProperty("affectedUsers", out var au) ? au.GetString() : null;
                    }
                    else if (safeData.TryGetProperty("inconsistencyType", out _))
                    {
                        feedback.FeedbackType = "Inconsistency";
                        feedback.Category = safeData.GetProperty("inconsistencyType").GetString();
                        feedback.ActiveSection = safeData.TryGetProperty("activeSection", out var s) ? s.GetString() : null;
                        feedback.Title = $"Inconsistency: {feedback.Category}";
                        feedback.Severity = "Low";
                        feedback.Priority = "Low";

                        feedback.Description = safeData.GetProperty("description").GetString() ?? "";
                        feedback.Location1 = safeData.TryGetProperty("location1", out var l1) ? l1.GetString() : null;
                        feedback.Location2 = safeData.TryGetProperty("location2", out var l2) ? l2.GetString() : null;
                        feedback.ExpectedConsistency = safeData.TryGetProperty("expectedConsistency", out var ec) ? ec.GetString() : null;
                    }
                    else if (safeData.TryGetProperty("questionCategory", out _))
                    {
                        feedback.FeedbackType = "Question";
                        feedback.Category = safeData.GetProperty("questionCategory").GetString();
                        feedback.Priority = safeData.GetProperty("priority").GetString();
                        feedback.Title = $"Question: {feedback.Category}";
                        feedback.Severity = "Low";
                        
                        feedback.Description = safeData.GetProperty("question").GetString();
                    }
                    else
                    {
                        // Fallback
                        feedback.FeedbackType = "General";
                        feedback.Title = "General Feedback";
                        feedback.Description = "No details provided.";
                    }
                }

                var result = await _feedbackManager.SubmitFeedbackAsync(feedback);

                if (result.IsSuccess)
                {
                    string trackingNumber = result.Data ?? string.Empty;
                    await webView.CoreWebView2.ExecuteScriptAsync($"if(typeof onFeedbackSubmitted === 'function') {{ onFeedbackSubmitted('{trackingNumber}'); }} else {{ alert('Feedback submitted! Tracking #: {trackingNumber}'); }}");
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
                else if (type == "support")
                {
                    if (parts.Length == 1)
                    {
                        string html = _templateEngine.GenerateContactSupportHtml();
                        webView.NavigateToString(html);
                    }
                    else if (parts.Length > 1)
                    {
                        string subType = parts[1];
                        string html = "";
                        switch (subType)
                        {
                            case "bug":
                                html = _templateEngine.GenerateBugReportFormHtml();
                                break;
                            case "suggestion":
                                html = _templateEngine.GenerateSuggestionFormHtml();
                                break;
                            case "inconsistency":
                                html = _templateEngine.GenerateInconsistencyFormHtml();
                                break;
                            case "question":
                                html = _templateEngine.GenerateQuestionFormHtml();
                                break;
                            case "submissions":
                                html = _templateEngine.GenerateViewSubmissionsHtml();
                                break;
                        }
                        if (!string.IsNullOrEmpty(html))
                        {
                            webView.NavigateToString(html);
                        }
                    }
                }
            }
        }
    }
}
