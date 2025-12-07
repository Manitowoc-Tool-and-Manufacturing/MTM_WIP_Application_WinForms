using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Help;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Services.Logging;

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
                if (json.Contains("\"type\":\"search\""))
                {
                    int queryStartIndex = json.IndexOf("\"query\":\"") + 9;
                    int queryEndIndex = json.LastIndexOf('\"');
                    if (queryStartIndex > 8 && queryEndIndex > queryStartIndex)
                    {
                        string query = json.Substring(queryStartIndex, queryEndIndex - queryStartIndex);
                        PerformSearch(query);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
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
