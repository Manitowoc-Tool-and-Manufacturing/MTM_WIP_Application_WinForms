using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Models; // For Model_Application_Variables

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

        /// <summary>
        /// Generates the HTML for the Index page (category cards).
        /// </summary>
        /// <param name="categories">List of categories to display.</param>
        /// <returns>HTML string for the index page.</returns>
        public string GenerateIndexHtml(IEnumerable<Model_HelpCategory> categories)
        {
            var sb = new StringBuilder();
            sb.Append("<div class='category-grid'>");

            foreach (var category in categories)
            {
                sb.Append($@"
                    <div class='category-card' onclick='window.location.href=""help://category/{category.CategoryId}""'>
                        <div class='category-icon'>{category.Icon}</div>
                        <div class='category-title'>{category.Title}</div>
                        <div class='category-desc'>{category.Description}</div>
                    </div>");
            }

            sb.Append("</div>");

            return WrapInBaseTemplate("Help Index", sb.ToString(), string.Empty);
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

        private string WrapInBaseTemplate(string title, string content, string sidebar)
        {
            // Load base template
            string template = "";
            try 
            {
                // In a real app, we'd read from file. For now, we use the embedded string from T013
                // But since we can't easily read the file here without IO, we'll reconstruct it with theme support
                template = GetBaseTemplate();
            }
            catch
            {
                // Fallback
                template = "<html><body>{{CONTENT}}</body></html>";
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

            return template;
        }

        private string GetBaseTemplate()
        {
            // This should match T013 content
            return @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{{TITLE}} - MTM Help</title>
    <style>
        :root {
            --bg-color: #ffffff;
            --text-color: #333333;
            --sidebar-bg: #f5f5f5;
            --sidebar-border: #e0e0e0;
            --link-color: #0066cc;
            --card-bg: #ffffff;
            --card-border: #dddddd;
            --card-hover: #f9f9f9;
        }

        @media (prefers-color-scheme: dark) {
            :root {
                --bg-color: #1e1e1e;
                --text-color: #e0e0e0;
                --sidebar-bg: #252526;
                --sidebar-border: #3e3e42;
                --link-color: #3794ff;
                --card-bg: #2d2d30;
                --card-border: #3e3e42;
                --card-hover: #3e3e42;
            }
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            display: flex;
            height: 100vh;
            background-color: var(--bg-color);
            color: var(--text-color);
            overflow: hidden;
        }

        .sidebar {
            width: 280px;
            background-color: var(--sidebar-bg);
            border-right: 1px solid var(--sidebar-border);
            display: flex;
            flex-direction: column;
            flex-shrink: 0;
        }

        .search-container {
            padding: 15px;
            border-bottom: 1px solid var(--sidebar-border);
        }

        .nav-list {
            flex: 1;
            overflow-y: auto;
            padding: 10px 0;
            list-style: none;
            margin: 0;
        }

        .nav-item {
            padding: 0;
        }

        .nav-link {
            display: block;
            padding: 8px 20px;
            color: var(--text-color);
            text-decoration: none;
            font-size: 14px;
            border-left: 3px solid transparent;
        }

        .nav-link:hover {
            background-color: rgba(128, 128, 128, 0.1);
        }

        .nav-link.active {
            background-color: rgba(128, 128, 128, 0.15);
            border-left-color: var(--link-color);
            font-weight: 600;
        }

        .content-area {
            flex: 1;
            padding: 40px;
            overflow-y: auto;
            position: relative;
        }

        /* Watermark */
        .content-area::after {
            content: '';
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 300px;
            height: 300px;
            background-image: url('data:image/svg+xml;utf8,<svg xmlns=""http://www.w3.org/2000/svg"" viewBox=""0 0 100 100""><text y=""50%"" x=""50%"" dy="".3em"" text-anchor=""middle"" font-size=""20"" fill=""rgba(128,128,128,0.05)"" font-family=""Arial"">MTM WIP</text></svg>');
            background-repeat: no-repeat;
            background-position: center;
            pointer-events: none;
            z-index: 0;
        }

        h1, h2, h3 { margin-top: 0; }
        h1 { font-size: 28px; margin-bottom: 10px; }
        .topic-meta { font-size: 12px; color: #888; margin-bottom: 20px; }
        
        p { line-height: 1.6; margin-bottom: 15px; }
        
        code {
            background-color: rgba(128, 128, 128, 0.1);
            padding: 2px 5px;
            border-radius: 3px;
            font-family: Consolas, monospace;
        }

        /* Index Grid */
        .category-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
            gap: 20px;
            max-width: 1200px;
            margin: 0 auto;
        }

        .category-card {
            background-color: var(--card-bg);
            border: 1px solid var(--card-border);
            border-radius: 8px;
            padding: 20px;
            cursor: pointer;
            transition: transform 0.2s, box-shadow 0.2s, background-color 0.2s;
            text-decoration: none;
            color: var(--text-color);
            display: block;
        }

        .category-card:hover {
            transform: translateY(-2px);
            box-shadow: 0 4px 12px rgba(0,0,0,0.1);
            background-color: var(--card-hover);
        }

        .category-icon {
            font-size: 24px;
            margin-bottom: 10px;
            color: var(--link-color);
        }

        .category-title {
            font-size: 18px;
            font-weight: 600;
            margin-bottom: 8px;
        }

        .category-desc {
            font-size: 14px;
            color: #888;
            line-height: 1.4;
        }

        /* Breadcrumbs */
        .breadcrumbs {
            font-size: 12px;
            color: #888;
            margin-bottom: 20px;
        }
        .breadcrumbs a { color: #888; text-decoration: none; }
        .breadcrumbs a:hover { text-decoration: underline; }

    </style>
</head>
<body>
    <div class=""sidebar"">
        <div class=""search-container"">
            <input type=""text"" id=""searchInput"" placeholder=""Search help..."" 
                   style=""width: 100%; padding: 8px; border: 1px solid var(--sidebar-border); border-radius: 4px; background: var(--bg-color); color: var(--text-color); box-sizing: border-box;"">
        </div>
        {{SIDEBAR}}
    </div>
    <div class=""content-area"">
        {{CONTENT}}
    </div>

    <script>
        // Handle internal navigation
        document.addEventListener('click', function(e) {
            if (e.target.tagName === 'A' && e.target.href.startsWith('help://')) {
                // Let WebView2 handle it via NavigationStarting
            }
        });

        // Search Logic
        const searchInput = document.getElementById('searchInput');
        let debounceTimer;
        
        if (searchInput) {
            searchInput.addEventListener('input', (e) => {
                clearTimeout(debounceTimer);
                debounceTimer = setTimeout(() => {
                    if (window.chrome && window.chrome.webview) {
                        window.chrome.webview.postMessage(JSON.stringify({ type: 'search', query: e.target.value }));
                    }
                }, 300);
            });
            
            // Focus search on Ctrl+F
            document.addEventListener('keydown', function(e) {
                if ((e.ctrlKey || e.metaKey) && e.key === 'f') {
                    e.preventDefault();
                    searchInput.focus();
                }
            });
        }
    </script>
</body>
</html>";
        }
    }
}
