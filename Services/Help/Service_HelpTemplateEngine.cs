using System.Text;
using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Models; // For Model_Application_Variables
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Service responsible for generating HTML content for the help system.
    /// </summary>
    public class Service_HelpTemplateEngine
    {
        private const string BaseTemplatePath = "Documentation/Help/Templates/help-base-template.html";
        private const string TopicCardTemplatePath = "Documentation/Help/Templates/topic-card-template.html";
        private const string SearchBoxTemplatePath = "Documentation/Help/Templates/search-box-template.html";
        private const string ContactSupportTemplatePath = "Documentation/Help/Templates/help-contact-support-page.html";
        private const string BugReportTemplatePath = "Documentation/Help/Templates/help-bug-report-form.html";
        private const string SuggestionTemplatePath = "Documentation/Help/Templates/help-suggestion-form.html";
        private const string InconsistencyTemplatePath = "Documentation/Help/Templates/help-inconsistency-form.html";
        private const string QuestionTemplatePath = "Documentation/Help/Templates/help-question-form.html";
        private const string ViewSubmissionsTemplatePath = "Documentation/Help/Templates/help-view-submissions.html";
        private const string AlertComponentPath = "Documentation/Help/Templates/components/alert-component.html";
        private const string CodeBlockComponentPath = "Documentation/Help/Templates/components/code-block-component.html";

        private readonly Dictionary<string, string> _templateCache = new();

        private string LoadTemplateFromFile(string templatePath)
        {
            if (_templateCache.TryGetValue(templatePath, out var cachedContent))
            {
                return cachedContent;
            }

            try
            {
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);
                if (!File.Exists(fullPath))
                {
                    LoggingUtility.Log(Enum_LogLevel.Error, "HelpSystem", $"Template file missing: {fullPath}");
                    // Fallback for base template if missing
                    if (templatePath == BaseTemplatePath)
                    {
                         return "<html><body><h1>Help System Error</h1><p>Base template missing.</p>{{CONTENT}}</body></html>";
                    }
                    return $"<div class='error'>Error: Template {templatePath} not found.</div>";
                }

                string content = File.ReadAllText(fullPath);
                _templateCache[templatePath] = content;
                return content;
            }
            catch (Exception ex)
            {
                LoggingUtility.Log(Enum_LogLevel.Error, "HelpSystem", $"Error loading template: {templatePath}", ex: ex);
                return $"<div class='error'>Error loading template: {ex.Message}</div>";
            }
        }

        /// <summary>
        /// Generates the HTML for the Index page (category cards).
        /// </summary>
        /// <param name="categories">List of categories to display.</param>
        /// <returns>HTML string for the index page.</returns>
        public string GenerateIndexHtml(IEnumerable<Model_HelpCategory> categories)
        {
            var sb = new StringBuilder();
            sb.Append("<div class='category-grid'>");

            string cardTemplate = LoadTemplateFromFile(TopicCardTemplatePath);

            foreach (var category in categories)
            {
                string card = cardTemplate
                    .Replace("{{CATEGORY_ID}}", category.CategoryId)
                    .Replace("{{ICON}}", category.Icon)
                    .Replace("{{TITLE}}", category.Title)
                    .Replace("{{DESCRIPTION}}", category.Description);
                sb.Append(card);
            }

            sb.Append("</div>");

            return WrapInBaseTemplate("Help Index", sb.ToString(), string.Empty);
        }

        public string GenerateContactSupportHtml()
        {
            string content = LoadTemplateFromFile(ContactSupportTemplatePath);
            return WrapInBaseTemplate("Contact Support", content, string.Empty);
        }

        public string GenerateBugReportFormHtml()
        {
            string content = LoadTemplateFromFile(BugReportTemplatePath);
            return WrapInBaseTemplate("Report a Bug", content, string.Empty);
        }

        public string GenerateSuggestionFormHtml()
        {
            string content = LoadTemplateFromFile(SuggestionTemplatePath);
            return WrapInBaseTemplate("Suggest Improvement", content, string.Empty);
        }

        public string GenerateInconsistencyFormHtml()
        {
            string content = LoadTemplateFromFile(InconsistencyTemplatePath);
            return WrapInBaseTemplate("Report Inconsistency", content, string.Empty);
        }

        public string GenerateQuestionFormHtml()
        {
            string content = LoadTemplateFromFile(QuestionTemplatePath);
            return WrapInBaseTemplate("Ask a Question", content, string.Empty);
        }

        public string GenerateViewSubmissionsHtml()
        {
            string content = LoadTemplateFromFile(ViewSubmissionsTemplatePath);
            return WrapInBaseTemplate("My Submissions", content, string.Empty);
        }

        /// <summary>
        /// Generates the HTML for a specific category and topic.
        /// </summary>
        /// <param name="category">The current category.</param>
        /// <param name="currentTopic">The current topic to display.</param>
        /// <returns>HTML string for the topic page.</returns>
        public string GenerateTopicHtml(Model_HelpCategory category, Model_HelpTopic currentTopic)
        {
            // Generate Sidebar
            var sidebarSb = new StringBuilder();
            sidebarSb.Append($"<h3>{category.Title}</h3><ul class='nav-list'>");
            foreach (var topic in category.Topics)
            {
                var activeClass = topic.TopicId == currentTopic.TopicId ? "active" : "";
                sidebarSb.Append($"<li class='nav-item'><a class='nav-link {activeClass}' href='help://topic/{category.CategoryId}/{topic.TopicId}'>{topic.Title}</a></li>");
            }
            sidebarSb.Append("</ul>");

            // Generate Content
            var contentSb = new StringBuilder();
            contentSb.Append($"<h1>{currentTopic.Title}</h1>");
            contentSb.Append($"<div class='topic-meta'>Last Updated: {currentTopic.LastUpdated?.ToShortDateString() ?? "N/A"}</div>");
            contentSb.Append(currentTopic.Content);
            contentSb.Append(GenerateFeedbackSection(currentTopic.TopicId, category.CategoryId));

            return WrapInBaseTemplate(currentTopic.Title, contentSb.ToString(), sidebarSb.ToString());
        }

        /// <summary>
        /// Generates the HTML for search results.
        /// </summary>
        /// <param name="results">List of search results.</param>
        /// <param name="query">The search query.</param>
        /// <returns>HTML string for the search results page.</returns>
        public string GenerateSearchResultsHtml(IEnumerable<Model_HelpSearchResult> results, string query)
        {
            var sb = new StringBuilder();
            sb.Append($"<h1>Search Results for \"{query}\"</h1>");
            
            if (!results.Any())
            {
                sb.Append("<p>No results found.</p>");
            }
            else
            {
                sb.Append("<div class='search-results'>");
                foreach (var result in results)
                {
                    sb.Append($@"
                        <div class='search-result-item' style='margin-bottom: 20px;'>
                            <h3><a href='help://topic/{result.Category.CategoryId}/{result.Topic.TopicId}'>{result.Topic.Title}</a></h3>
                            <div class='breadcrumbs'>{result.Category.Title} &gt; {result.Topic.Title}</div>
                            <p>{result.Topic.Summary}</p>
                        </div>");
                }
                sb.Append("</div>");
            }

            return WrapInBaseTemplate($"Search: {query}", sb.ToString(), string.Empty);
        }

        private string ReplaceComponentPlaceholders(string html)
        {
            // Regex to find component blocks: {{component:name attributes}}content{{/component:name}}
            var regex = new System.Text.RegularExpressions.Regex(@"\{\{component:(\w+)([^}]*)\}\}(.*?)\{\{/component:\1\}\}", System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return regex.Replace(html, match =>
            {
                string componentName = match.Groups[1].Value.ToLower();
                string attributes = match.Groups[2].Value;
                string content = match.Groups[3].Value;

                return RenderComponent(componentName, attributes, content);
            });
        }

        private string RenderComponent(string name, string attributes, string content)
        {
            string? templatePath = name switch
            {
                "alert" => AlertComponentPath,
                "code" => CodeBlockComponentPath,
                _ => null
            };

            if (templatePath == null) return $"<!-- Unknown component: {name} -->{content}";

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);
            if (!File.Exists(fullPath))
            {
                 LoggingUtility.Log(Enum_LogLevel.Warning, "HelpSystem", $"Component template missing: {templatePath}");
                 return content; // Render content as plain text
            }

            string template = LoadTemplateFromFile(templatePath);
            
            // Parse attributes: type="warning"
            var attrRegex = new System.Text.RegularExpressions.Regex(@"(\w+)=""([^""]*)""");
            var attrMatches = attrRegex.Matches(attributes);
            
            foreach (System.Text.RegularExpressions.Match attr in attrMatches)
            {
                string key = attr.Groups[1].Value.ToUpper();
                string value = attr.Groups[2].Value;
                template = template.Replace($"{{{{{key}}}}}", value);
            }

            // Replace content
            template = template.Replace("{{CONTENT}}", content);

            // Clean up unused placeholders with defaults
            template = template.Replace("{{ICON}}", ""); 
            template = template.Replace("{{TYPE}}", "info");
            template = template.Replace("{{LANGUAGE}}", "text");

            return template;
        }

        private string WrapInBaseTemplate(string title, string content, string sidebar)
        {
            // Load base template
            string template = LoadTemplateFromFile(BaseTemplatePath);

            // Process components in content
            content = ReplaceComponentPlaceholders(content);

            // Watermark Logic
            string watermarkUrl = "data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100'><text y='50%' x='50%' dy='.3em' text-anchor='middle' font-size='20' fill='rgba(128,128,128,0.05)' font-family='Arial'>MTM WIP</text></svg>";
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "MTM.png");
            
            if (File.Exists(logoPath))
            {
                watermarkUrl = new Uri(logoPath).AbsoluteUri;
            }

            // Inject Theme Colors
            // Note: Model_Application_Variables.ThemeEnabled is a boolean, but we might need more specific theme info
            // For now, we'll rely on CSS media queries in the template, or inject a class
            // string themeClass = Model_Application_Variables.ThemeEnabled ? "theme-dark" : "theme-light"; 
            // We'll stick to media queries for simplicity as per T013

            // Replace placeholders
            template = template.Replace("{{TITLE}}", title);
            template = template.Replace("{{CONTENT}}", content);
            template = template.Replace("{{SIDEBAR}}", sidebar);
            template = template.Replace("{{WATERMARK_URL}}", watermarkUrl);

            return template;
        }


        private string GenerateFeedbackSection(string topicId, string categoryId)
        {
            return $@"
                <div class='feedback-section' style='margin-top: 40px; padding-top: 20px; border-top: 1px solid #eee;'>
                    <h3 style='font-size: 1.1em; margin-bottom: 10px;'>Was this article helpful?</h3>
                    <div id='feedback-initial'>
                        <button class='btn-feedback' onclick='submitFeedback(""{topicId}"", ""{categoryId}"", true)'>Yes</button>
                        <button class='btn-feedback' onclick='submitFeedback(""{topicId}"", ""{categoryId}"", false)'>No</button>
                    </div>
                    <div id='feedback-followup' style='display:none; margin-top: 15px;'>
                        <p id='feedback-message' style='color: green; font-weight: bold; margin-bottom: 10px;'>Thank you for your feedback!</p>
                        <div id='comment-section'>
                            <p style='margin-bottom: 5px;'>Additional comments (optional):</p>
                            <textarea id='comment-text' rows='3' style='width: 100%; padding: 8px; border: 1px solid #ddd; border-radius: 4px; font-family: inherit;'></textarea>
                            <button class='btn-feedback' style='margin-top: 10px; background-color: #0078d4; color: white;' onclick='submitComment()'>Submit Comment</button>
                        </div>
                    </div>
                </div>
                <style>
                    .btn-feedback {{
                        padding: 6px 16px;
                        margin-right: 10px;
                        border: 1px solid #ccc;
                        background-color: #f8f9fa;
                        border-radius: 4px;
                        cursor: pointer;
                        font-size: 14px;
                    }}
                    .btn-feedback:hover {{
                        background-color: #e9ecef;
                    }}
                </style>
                <script>
                    let currentFeedbackId = 0;
                    
                    function submitFeedback(topicId, categoryId, isHelpful) {{
                        // Disable buttons
                        const buttons = document.querySelectorAll('#feedback-initial button');
                        buttons.forEach(b => b.disabled = true);
                        
                        window.chrome.webview.postMessage({{
                            type: 'submitFeedback',
                            data: {{ topicId: topicId, categoryId: categoryId, isHelpful: isHelpful }}
                        }});
                    }}

                    function onFeedbackSubmitted(feedbackId) {{
                        currentFeedbackId = feedbackId;
                        document.getElementById('feedback-initial').style.display = 'none';
                        document.getElementById('feedback-followup').style.display = 'block';
                    }}

                    function submitComment() {{
                        const comment = document.getElementById('comment-text').value;
                        if (!comment) return;
                        
                        window.chrome.webview.postMessage({{
                            type: 'addComment',
                            data: {{ feedbackId: currentFeedbackId, comment: comment }}
                        }});
                        
                        document.getElementById('comment-section').innerHTML = '<p>Comment submitted. Thanks again!</p>';
                    }}
                </script>";
        }
    }
}
