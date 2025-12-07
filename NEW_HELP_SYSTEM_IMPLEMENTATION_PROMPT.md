# NEW HELP SYSTEM IMPLEMENTATION PROMPT

## PROJECT OVERVIEW

Implement a modern, HTML-based help system for the MTM WIP Application that replaces the current static HTML help files with a dynamic, JSON-driven system modeled after the existing Release Notes viewer (`SettingsForm_ViewReleaseNotesHTML.cs`).

## CRITICAL CONSTRAINTS

**⛔ DO NOT USE OLD HELP FILES FOR CONTENT**
The existing HTML files in `Documentation/Help/` contain outdated information. When writing content for the new JSON files:
1. **Ignore** the text content of old HTML files.
2. **Analyze** the actual C# forms and controls to understand current features.
3. **Read** the code in `Forms/` and `Services/` to determine how features work.
4. **Use** the old files ONLY for understanding the *structure* (categories, navigation), not the *facts*.

---

## REFERENCE MATERIALS ANALYZED

### 1. Design Inspiration Sources

**ProProfs Operations Manual Template** (https://www.proprofskb.com/templates/operations-manuals/):
- Card-based navigation interface
- Categorized content sections (Policies & Procedures, Company's Processes, Emergency Procedures, Roles and Responsibilities)
- Search functionality with instant filtering
- Clean, modern UI with gradient backgrounds
- Mobile-responsive design
- Recent articles listing
- Prominent search bar

**Live Operations Manual Example** (https://operations-manual.helpdocsonline.com/home):
- Sidebar navigation with collapsible sections
- Homepage with category cards
- Search-first interface
- Clean typography and spacing
- Recent articles feature
- Professional branding integration

### 2. Existing Codebase Patterns

**Release Notes HTML System** (`SettingsForm_ViewReleaseNotesHTML.cs` + `RELEASE_NOTES.json`):
- JSON data structure with C# model mapping
- HTML template with placeholder injection (`{{PLACEHOLDER}}`)
- WebView2 rendering engine
- Base64 image embedding for watermarks
- Sidebar navigation with version cards
- Dynamic content generation via JavaScript
- Icon categorization based on content analysis

**Current Help System** (`Documentation/Help/`):
- Static HTML files per topic
- Shared CSS (`help-styles.css`) and JavaScript (`help-navigation.js`)
- Search index with keyword matching
- Breadcrumb navigation
- Card-based category layout on index page
- Keyboard shortcuts (F1, Ctrl+F)

---

## ARCHITECTURAL REQUIREMENTS

### 1. JSON-Driven Content Structure

Each help topic/category should have its own JSON file following this structure:

```json
{
  "Category": "Getting Started",
  "CategoryId": "getting-started",
  "Icon": "🚀",
  "Description": "New user guide and application overview",
  "Topics": [
    {
      "TopicId": "application-overview",
      "Title": "Application Overview",
      "Summary": "Comprehensive introduction to the MTM Inventory Application",
      "Content": "<h3>What is MTM WIP?</h3><p>The MTM Inventory Application is...</p>",
      "Keywords": ["overview", "introduction", "features"],
      "LastUpdated": "2025-12-06"
    },
    {
      "TopicId": "first-time-setup",
      "Title": "First Time Setup",
      "Summary": "Step-by-step guide for initial configuration",
      "Content": "<h3>System Requirements</h3><p>Before starting...</p>",
      "Keywords": ["setup", "installation", "requirements"],
      "LastUpdated": "2025-12-06"
    }
  ]
}
```

**Required JSON Files** (create one for each major help category):
1. `getting-started.json` - Overview, setup, first launch
2. `inventory-operations.json` - Add inventory, quantities, parts
3. `remove-operations.json` - Remove items, batch removal
4. `transfer-operations.json` - Transfer items between locations
5. `advanced-features.json` - Advanced inventory, QuickButtons
6. `transaction-history.json` - History viewer, search, analytics
7. `infor-visual-integration.json` - Visual Dashboard, Receiving, Die Tool Discovery
8. `analytics-reporting.json` - WIP User Analytics, Material Handler Performance
9. `settings-management.json` - Settings form, themes, preferences
10. `admin-tools.json` - Logs, Error Reports, Database Maintenance
11. `keyboard-shortcuts.json` - All keyboard shortcuts reference
12. `troubleshooting.json` - Common issues and solutions
13. `system-configuration.json` - System requirements, database setup

### 2. HTML Template System (Reusable Components)

Create a modular HTML template system similar to WinForms Form/UserControl relationship:

**Base Template** (`help-base-template.html`):
```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <style>
        /* SHARED STYLES - All pages use these */
        body { margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; }
        .sidebar { /* Consistent sidebar styling */ }
        .content { /* Consistent content area styling */ }
        /* ... more shared styles ... */
    </style>
</head>
<body>
    <!-- SIDEBAR NAVIGATION (Reusable) -->
    <div class="sidebar" id="sidebar">
        <div class="sidebar-header">
            <div class="sidebar-title">{{SECTION_TITLE}}</div>
        </div>
        <!-- Dynamic navigation items injected here -->
    </div>
    
    <!-- WATERMARK (Reusable) -->
    <img id="watermark-img" class="watermark" alt="MTM Watermark" />
    
    <!-- MAIN CONTENT AREA (Dynamic) -->
    <div class="content" id="content">
        <!-- Content injected here from JSON -->
    </div>
    
    <!-- SHARED JAVASCRIPT -->
    <script id="help-data" type="application/json">
        {{HELP_JSON}}
    </script>
    
    <script>
        {{SHARED_JAVASCRIPT}}
    </script>
</body>
</html>
```

**Component Templates** (Reusable HTML snippets):

- `topic-card-template.html` - Standard topic card layout
- `search-box-template.html` - Search interface component
- `breadcrumb-template.html` - Navigation breadcrumb
- `alert-box-template.html` - Info/warning/error alerts
- `code-block-template.html` - Code snippet formatting

### 3. Service Architecture (C# Implementation)

Create a new service following the existing pattern from Release Notes:

**File Structure:**
```
Services/Help/
├── Service_HelpSystem.cs           # Main help service (static or DI-based)
├── Service_HelpContentLoader.cs    # JSON loading and parsing
└── Service_HelpTemplateEngine.cs   # HTML template processing

Forms/Help/
├── HelpViewerForm.cs               # Main help window (inherits ThemedForm)
├── HelpViewerForm.Designer.cs
└── HelpViewerForm.resx

Models/Help/
├── Model_HelpCategory.cs           # Category data model
├── Model_HelpTopic.cs              # Topic data model
└── Model_HelpSearchResult.cs       # Search result model

Documentation/Help/JSON/
├── getting-started.json
├── inventory-operations.json
├── remove-operations.json
├── transfer-operations.json
├── advanced-features.json
├── transaction-history.json
├── infor-visual-integration.json
├── analytics-reporting.json
├── settings-management.json
├── admin-tools.json
├── keyboard-shortcuts.json
├── troubleshooting.json
└── system-configuration.json

Documentation/Help/Templates/
├── help-base-template.html         # Main template
├── topic-card-component.html       # Reusable card
├── search-component.html           # Search UI
└── navigation-component.html       # Nav elements
```

---

## IMPLEMENTATION SPECIFICATIONS

### PHASE 1: Data Models and JSON Structure

**1.1 Create C# Models** (`Models/Help/`)

```csharp
namespace MTM_WIP_Application_Winforms.Models.Help
{
    /// <summary>
    /// Represents a help category containing multiple related topics.
    /// </summary>
    public class Model_HelpCategory
    {
        /// <summary>
        /// Display name of the category (e.g., "Getting Started").
        /// </summary>
        public string Category { get; set; } = string.Empty;
        
        /// <summary>
        /// URL-safe identifier for the category (e.g., "getting-started").
        /// </summary>
        public string CategoryId { get; set; } = string.Empty;
        
        /// <summary>
        /// Emoji or icon character for visual identification.
        /// </summary>
        public string Icon { get; set; } = "📖";
        
        /// <summary>
        /// Brief description of category contents.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// List of help topics within this category.
        /// </summary>
        public List<Model_HelpTopic> Topics { get; set; } = new();
    }
    
    /// <summary>
    /// Represents an individual help topic with title, content, and metadata.
    /// </summary>
    public class Model_HelpTopic
    {
        /// <summary>
        /// URL-safe identifier for the topic (e.g., "application-overview").
        /// </summary>
        public string TopicId { get; set; } = string.Empty;
        
        /// <summary>
        /// Display title of the topic.
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Brief summary shown in search results and navigation.
        /// </summary>
        public string Summary { get; set; } = string.Empty;
        
        /// <summary>
        /// Full HTML content for the topic (supports rich formatting).
        /// </summary>
        public string Content { get; set; } = string.Empty;
        
        /// <summary>
        /// Search keywords for improved discoverability.
        /// </summary>
        public List<string> Keywords { get; set; } = new();
        
        /// <summary>
        /// Date of last content update (ISO 8601 format: "2025-12-06").
        /// </summary>
        public string LastUpdated { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// Represents a search result with relevance scoring.
    /// </summary>
    public class Model_HelpSearchResult
    {
        public string CategoryId { get; set; } = string.Empty;
        public string TopicId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public int RelevanceScore { get; set; }
        public string MatchedKeywords { get; set; } = string.Empty;
    }
}
```

**1.2 Create JSON Files** (`Documentation/Help/JSON/`)

Create new JSON content based on current application features. Do not convert old HTML files as they are outdated. Example:

```json
// getting-started.json
{
  "Category": "Getting Started",
  "CategoryId": "getting-started",
  "Icon": "🚀",
  "Description": "New user guide and application overview",
  "Topics": [
    {
      "TopicId": "application-overview",
      "Title": "Application Overview",
      "Summary": "Comprehensive introduction to the MTM Inventory Application",
      "Content": "<h3>Application Overview</h3><p>The MTM Inventory Application is a comprehensive Work-In-Progress (WIP) inventory management system built with Windows Forms (.NET 8) and MySQL database backend.</p><h4>🎯 Core Features</h4><ul><li><strong>Inventory Management</strong> - Add, track, and manage inventory items with parts, operations, and locations</li><li><strong>Remove Operations</strong> - Single and batch removal of inventory items with full audit trail</li><li><strong>Transfer Operations</strong> - Move items between locations with validation and tracking</li><li><strong>QuickButtons</strong> - One-click access to your most recent operations</li><li><strong>Advanced Features</strong> - Bulk operations, Excel import/export, and advanced filtering</li><li><strong>Transaction History</strong> - Complete audit trail with modern search and filtering</li></ul>",
      "Keywords": ["overview", "introduction", "features", "WIP", "inventory"],
      "LastUpdated": "2025-12-06"
    },
    {
      "TopicId": "system-requirements",
      "Title": "System Requirements",
      "Summary": "Hardware and software requirements for MTM WIP Application",
      "Content": "<h3>System Requirements</h3><p>Before starting, ensure your system meets the requirements:</p><ul><li><strong>Operating System:</strong> Windows 10/11 (64-bit recommended)</li><li><strong>Framework:</strong> .NET 8 Runtime (installed automatically)</li><li><strong>Database:</strong> MySQL 5.7.24+ or MAMP environment</li><li><strong>Memory:</strong> 4GB RAM minimum, 8GB recommended</li><li><strong>Storage:</strong> 100MB for application, additional space for database</li></ul>",
      "Keywords": ["requirements", "system", "windows", "mysql", "hardware", "software"],
      "LastUpdated": "2025-12-06"
    }
  ]
}
```

### PHASE 2: HTML Template Engine

**2.1 Base Template** (`Documentation/Help/Templates/help-base-template.html`)

Create a single template file similar to `release-notes.html` that can render ANY help category:

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <style>
        /* COPY STYLES FROM release-notes.html */
        body { margin: 0; padding: 0; font-family: 'Segoe UI', sans-serif; display: flex; height: 100vh; overflow: hidden; background-color: #ffffff; }
        
        .sidebar {
            width: 280px;
            background-color: #212121;
            color: #ffffff;
            overflow-y: auto;
            display: flex;
            flex-direction: column;
            border-right: 1px solid #444;
            flex-shrink: 0;
            z-index: 10;
        }
        
        .sidebar-header {
            padding: 20px;
            background-color: #000000;
            border-bottom: 1px solid #C5A059;
            position: sticky;
            top: 0;
            z-index: 20;
        }
        
        .sidebar-title { font-size: 18px; font-weight: bold; color: #C5A059; }
        
        .nav-item {
            padding: 20px;
            border-bottom: 1px solid #333;
            cursor: pointer;
            text-decoration: none;
            color: #ddd;
            transition: all 0.2s;
            display: flex;
            flex-direction: column;
            gap: 8px;
        }
        
        .nav-item:hover { background-color: #333; color: #fff; transform: translateY(-2px); }
        .nav-item.active { background-color: #333; border-left: 4px solid #C5A059; color: #fff; }
        
        .content {
            flex: 1;
            overflow-y: auto;
            padding: 40px;
            scroll-behavior: smooth;
            position: relative;
        }
        
        .watermark {
            position: fixed;
            top: 50%;
            left: calc(280px + (100% - 280px) / 2);
            transform: translate(-50%, -50%);
            opacity: 0.05;
            pointer-events: none;
            z-index: 0;
            max-width: 60%;
            max-height: 60%;
            filter: grayscale(100%);
        }
        
        .topic-section {
            margin-bottom: 60px;
            padding-bottom: 40px;
            border-bottom: 1px solid #eee;
            position: relative;
            z-index: 1;
        }
        
        .topic-header {
            display: flex;
            align-items: center;
            margin-bottom: 20px;
        }
        
        .topic-badge {
            background-color: #000000;
            color: #C5A059;
            padding: 5px 15px;
            border-radius: 4px;
            font-weight: bold;
            font-size: 24px;
            margin-right: 15px;
        }
        
        .topic-summary {
            font-size: 16px;
            color: #333;
            margin-bottom: 25px;
            font-style: italic;
            border-left: 4px solid #C5A059;
            padding-left: 15px;
            background-color: #f9f9f9;
            padding: 15px;
        }
        
        /* Search Box */
        .search-container {
            padding: 15px 20px;
            background-color: #1a1a1a;
            border-bottom: 1px solid #333;
        }
        
        .search-box {
            width: 100%;
            padding: 10px;
            border: 1px solid #444;
            border-radius: 4px;
            background-color: #2a2a2a;
            color: #fff;
            font-size: 14px;
        }
        
        .search-box:focus {
            outline: none;
            border-color: #C5A059;
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            body { flex-direction: column; }
            .sidebar { width: 100%; max-height: 300px; }
            .watermark { left: 50%; }
        }
    </style>
</head>
<body>
    <!-- SIDEBAR -->
    <div class="sidebar" id="sidebar">
        <div class="sidebar-header">
            <div class="sidebar-title">{{CATEGORY_TITLE}}</div>
        </div>
        <div class="search-container">
            <input type="text" class="search-box" placeholder="Search topics..." id="searchBox" />
        </div>
        <!-- Sidebar nav items injected here -->
    </div>
    
    <!-- WATERMARK -->
    <img id="watermark-img" class="watermark" src="../../Resources/MTM.png" alt="Watermark" />
    
    <!-- CONTENT -->
    <div class="content" id="content">
        <!-- Topic sections injected here -->
    </div>
    
    <!-- DATA INJECTION -->
    <script id="help-data" type="application/json">
        {{HELP_JSON}}
    </script>
    
    <!-- JAVASCRIPT -->
    <script>
        const watermarkBase64 = "{{WATERMARK_BASE64}}";
        
        function init() {
            try {
                // Set watermark
                if (watermarkBase64 && !watermarkBase64.startsWith("{{")) {
                    document.getElementById('watermark-img').src = "data:image/png;base64," + watermarkBase64;
                }
                
                // Parse JSON
                const dataElement = document.getElementById('help-data');
                let helpData = null;
                
                if (dataElement) {
                    const rawData = dataElement.textContent.trim();
                    if (rawData && !rawData.startsWith("{{")) {
                        helpData = JSON.parse(rawData);
                    }
                }
                
                if (!helpData || !helpData.Topics || helpData.Topics.length === 0) {
                    document.getElementById('content').innerHTML = "<h1>No help topics found.</h1>";
                    return;
                }
                
                const sidebar = document.getElementById('sidebar');
                const content = document.getElementById('content');
                
                // Render each topic
                helpData.Topics.forEach(topic => {
                    const id = "topic_" + topic.TopicId;
                    
                    // Create sidebar nav item
                    const navItem = document.createElement('a');
                    navItem.href = "#" + id;
                    navItem.className = 'nav-item';
                    navItem.onclick = function() { setActive(this); };
                    navItem.innerHTML = `
                        <strong>${topic.Title}</strong>
                        <small style="color: #aaa; font-size: 12px;">${topic.Summary}</small>
                    `;
                    sidebar.appendChild(navItem);
                    
                    // Create content section
                    const section = document.createElement('div');
                    section.id = id;
                    section.className = 'topic-section';
                    section.innerHTML = `
                        <div class="topic-header">
                            <span class="topic-badge">${topic.Title}</span>
                        </div>
                        <div class="topic-summary">${topic.Summary}</div>
                        <div class="topic-content">${topic.Content}</div>
                        <div style="margin-top: 20px; font-size: 12px; color: #999;">
                            Last Updated: ${topic.LastUpdated}
                        </div>
                    `;
                    content.appendChild(section);
                });
                
                // Search functionality
                setupSearch(helpData);
                
            } catch (err) {
                document.body.innerHTML += '<div style="color:red; padding:20px;">JS Error: ' + err.message + '</div>';
            }
        }
        
        function setActive(element) {
            document.querySelectorAll('.nav-item').forEach(el => el.classList.remove('active'));
            element.classList.add('active');
        }
        
        function setupSearch(helpData) {
            const searchBox = document.getElementById('searchBox');
            if (!searchBox) return;
            
            searchBox.addEventListener('input', function(e) {
                const term = e.target.value.toLowerCase().trim();
                
                if (term.length === 0) {
                    // Show all topics
                    document.querySelectorAll('.topic-section').forEach(section => {
                        section.style.display = 'block';
                    });
                    document.querySelectorAll('.nav-item').forEach(item => {
                        item.style.display = 'flex';
                    });
                    return;
                }
                
                // Filter topics
                helpData.Topics.forEach((topic, index) => {
                    const searchText = (topic.Title + " " + topic.Summary + " " + topic.Content + " " + topic.Keywords.join(" ")).toLowerCase();
                    const matches = searchText.includes(term);
                    
                    const sectionId = "topic_" + topic.TopicId;
                    const section = document.getElementById(sectionId);
                    const navItems = document.querySelectorAll('.nav-item');
                    
                    if (section && navItems[index]) {
                        section.style.display = matches ? 'block' : 'none';
                        navItems[index].style.display = matches ? 'flex' : 'none';
                    }
                });
            });
        }
        
        init();
    </script>
</body>
</html>
```

### PHASE 3: C# Service Implementation

**3.1 Content Loader Service** (`Services/Help/Service_HelpContentLoader.cs`)

```csharp
using System.Text.Json;
using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Loads and parses help content from JSON files.
    /// </summary>
    public static class Service_HelpContentLoader
    {
        #region Methods
        
        /// <summary>
        /// Loads a help category from a JSON file.
        /// </summary>
        /// <param name="categoryId">Category identifier (e.g., "getting-started")</param>
        /// <returns>Parsed help category with topics, or null if file not found</returns>
        public static Model_HelpCategory? LoadCategory(string categoryId)
        {
            try
            {
                string? jsonPath = GetHelpFilePath($"{categoryId}.json");
                if (string.IsNullOrEmpty(jsonPath) || !File.Exists(jsonPath))
                {
                    LoggingUtility.LogWarning($"Help JSON not found: {categoryId}.json");
                    return null;
                }
                
                string jsonContent = File.ReadAllText(jsonPath);
                var category = JsonSerializer.Deserialize<Model_HelpCategory>(jsonContent);
                
                return category;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Low);
                return null;
            }
        }
        
        /// <summary>
        /// Loads all available help categories.
        /// </summary>
        /// <returns>List of all help categories</returns>
        public static List<Model_HelpCategory> LoadAllCategories()
        {
            var categories = new List<Model_HelpCategory>();
            
            string[] categoryIds = {
                "getting-started",
                "inventory-operations",
                "remove-operations",
                "infor-visual-integration",
                "analytics-reporting",
                "settings-management",
                "admin-tools",
                "advanced-features",
                "transaction-history",
                "settings-management",
                "keyboard-shortcuts",
                "troubleshooting",
                "system-configuration"
            };
            
            foreach (string categoryId in categoryIds)
            {
                var category = LoadCategory(categoryId);
                if (category != null)
                {
                    categories.Add(category);
                }
            }
            
            return categories;
        }
        
        /// <summary>
        /// Searches all help topics for matching keywords.
        /// </summary>
        /// <param name="searchTerm">Search query</param>
        /// <returns>List of matching topics with relevance scores</returns>
        public static List<Model_HelpSearchResult> Search(string searchTerm)
        {
            var results = new List<Model_HelpSearchResult>();
            var categories = LoadAllCategories();
            
            string lowerTerm = searchTerm.ToLower();
            
            foreach (var category in categories)
            {
                foreach (var topic in category.Topics)
                {
                    int score = 0;
                    var matchedKeywords = new List<string>();
                    
                    // Search in title (highest priority)
                    if (topic.Title.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 50;
                        matchedKeywords.Add("Title");
                    }
                    
                    // Search in summary
                    if (topic.Summary.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 30;
                        matchedKeywords.Add("Summary");
                    }
                    
                    // Search in keywords
                    foreach (var keyword in topic.Keywords)
                    {
                        if (keyword.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase))
                        {
                            score += 20;
                            matchedKeywords.Add(keyword);
                        }
                    }
                    
                    // Search in content (lowest priority)
                    if (topic.Content.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 10;
                    }
                    
                    if (score > 0)
                    {
                        results.Add(new Model_HelpSearchResult
                        {
                            CategoryId = category.CategoryId,
                            TopicId = topic.TopicId,
                            Title = topic.Title,
                            Summary = topic.Summary,
                            RelevanceScore = score,
                            MatchedKeywords = string.Join(", ", matchedKeywords)
                        });
                    }
                }
            }
            
            return results.OrderByDescending(r => r.RelevanceScore).ToList();
        }
        
        #endregion
        
        #region Helpers
        
        /// <summary>
        /// Gets the full path to a help file.
        /// Checks bin directory and project root.
        /// </summary>
        /// <param name="relativePath">Relative path from Documentation/Help/JSON/</param>
        /// <returns>Full path if found, null otherwise</returns>
        private static string? GetHelpFilePath(string relativePath)
        {
            // Check bin directory
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "Help", "JSON", relativePath);
            if (File.Exists(path)) return path;
            
            // Check project root (for development)
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            path = Path.Combine(projectRoot, "Documentation", "Help", "JSON", relativePath);
            if (File.Exists(path)) return path;
            
            return null;
        }
        
        #endregion
    }
}
```

**3.2 Template Engine Service** (`Services/Help/Service_HelpTemplateEngine.cs`)

```csharp
using MTM_WIP_Application_Winforms.Models.Help;
using System.Text.Json;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Processes HTML templates and injects help content.
    /// </summary>
    public static class Service_HelpTemplateEngine
    {
        #region Methods
        
        /// <summary>
        /// Generates HTML for a help category using the base template.
        /// </summary>
        /// <param name="category">Help category to render</param>
        /// <param name="watermarkBase64">Optional Base64-encoded watermark image</param>
        /// <returns>Complete HTML document ready for WebView2</returns>
        public static string GenerateHtml(Model_HelpCategory category, string watermarkBase64 = "")
        {
            try
            {
                // Load base template
                string? templatePath = GetTemplatePath("help-base-template.html");
                if (string.IsNullOrEmpty(templatePath))
                {
                    return GenerateErrorHtml("Template not found");
                }
                
                string htmlTemplate = File.ReadAllText(templatePath);
                
                // Serialize category to JSON
                string categoryJson = JsonSerializer.Serialize(category, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                
                // Replace placeholders
                htmlTemplate = htmlTemplate.Replace("{{CATEGORY_TITLE}}", category.Category);
                htmlTemplate = htmlTemplate.Replace("{{HELP_JSON}}", categoryJson);
                htmlTemplate = htmlTemplate.Replace("{{WATERMARK_BASE64}}", watermarkBase64);
                
                return htmlTemplate;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return GenerateErrorHtml($"Error generating help: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Generates HTML for the help index/home page with all categories.
        /// </summary>
        /// <param name="categories">List of all help categories</param>
        /// <returns>HTML for help index page</returns>
        public static string GenerateIndexHtml(List<Model_HelpCategory> categories)
        {
            var html = new System.Text.StringBuilder();
            html.Append(@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8' />
    <title>MTM WIP Application - Help</title>
    <style>
        body { 
            margin: 0; 
            padding: 40px; 
            font-family: 'Segoe UI', sans-serif; 
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
        }
        .header {
            text-align: center;
            color: white;
            margin-bottom: 40px;
        }
        .categories {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 20px;
            max-width: 1200px;
            margin: 0 auto;
        }
        .category-card {
            background: white;
            border-radius: 8px;
            padding: 20px;
            cursor: pointer;
            transition: transform 0.2s, box-shadow 0.2s;
        }
        .category-card:hover {
            transform: translateY(-4px);
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
        }
        .category-icon {
            font-size: 48px;
            margin-bottom: 10px;
        }
        .category-title {
            font-size: 20px;
            font-weight: bold;
            color: #333;
            margin-bottom: 10px;
        }
        .category-desc {
            color: #666;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <div class='header'>
        <h1>MTM WIP Application Help</h1>
        <p>Select a category to view help topics</p>
    </div>
    <div class='categories'>
");
            
            foreach (var category in categories)
            {
                html.Append($@"
        <div class='category-card' onclick=""window.location.href='#category_{category.CategoryId}'"">
            <div class='category-icon'>{category.Icon}</div>
            <div class='category-title'>{category.Category}</div>
            <div class='category-desc'>{category.Description}</div>
        </div>
");
            }
            
            html.Append(@"
    </div>
</body>
</html>
");
            
            return html.ToString();
        }
        
        #endregion
        
        #region Helpers
        
        private static string? GetTemplatePath(string templateName)
        {
            // Check bin directory
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Documentation", "Help", "Templates", templateName);
            if (File.Exists(path)) return path;
            
            // Check project root
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
            path = Path.Combine(projectRoot, "Documentation", "Help", "Templates", templateName);
            if (File.Exists(path)) return path;
            
            return null;
        }
        
        private static string GenerateErrorHtml(string errorMessage)
        {
            return $@"
<!DOCTYPE html>
<html>
<head><meta charset='utf-8' /><title>Error</title></head>
<body style='font-family: sans-serif; padding: 40px;'>
    <h1 style='color: red;'>Error Loading Help</h1>
    <p>{errorMessage}</p>
</body>
</html>";
        }
        
        #endregion
    }
}
```

**3.3 Main Help System Service** (`Services/Help/Service_HelpSystem.cs`)

```csharp
using MTM_WIP_Application_Winforms.Models.Help;

namespace MTM_WIP_Application_Winforms.Services.Help
{
    /// <summary>
    /// Main entry point for the help system.
    /// Coordinates content loading, template rendering, and help display.
    /// </summary>
    public static class Service_HelpSystem
    {
        #region Methods
        
        /// <summary>
        /// Opens the help viewer with a specific category.
        /// </summary>
        /// <param name="categoryId">Category to display (e.g., "getting-started")</param>
        public static void ShowHelp(string categoryId = "getting-started")
        {
            try
            {
                var category = Service_HelpContentLoader.LoadCategory(categoryId);
                if (category == null)
                {
                    Service_ErrorHandler.ShowUserError($"Help category '{categoryId}' not found.");
                    return;
                }
                
                var helpForm = new Forms.Help.HelpViewerForm(category);
                helpForm.Show();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }
        
        /// <summary>
        /// Opens the help index page showing all categories.
        /// </summary>
        public static void ShowHelpIndex()
        {
            try
            {
                var categories = Service_HelpContentLoader.LoadAllCategories();
                var helpForm = new Forms.Help.HelpViewerForm(categories);
                helpForm.Show();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }
        
        /// <summary>
        /// Opens help for a specific topic within a category.
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="topicId">Topic identifier to scroll to</param>
        public static void ShowHelpTopic(string categoryId, string topicId)
        {
            try
            {
                var category = Service_HelpContentLoader.LoadCategory(categoryId);
                if (category == null)
                {
                    ShowHelpIndex();
                    return;
                }
                
                var helpForm = new Forms.Help.HelpViewerForm(category, topicId);
                helpForm.Show();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }
        
        /// <summary>
        /// Performs a search across all help content.
        /// </summary>
        /// <param name="searchTerm">Search query</param>
        /// <returns>List of matching topics</returns>
        public static List<Model_HelpSearchResult> Search(string searchTerm)
        {
            return Service_HelpContentLoader.Search(searchTerm);
        }
        
        #endregion
    }
}
```

### PHASE 4: WinForms Help Viewer

**4.1 Help Viewer Form** (`Forms/Help/HelpViewerForm.cs`)

```csharp
using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models.Help;
using MTM_WIP_Application_Winforms.Services.Help;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Help
{
    /// <summary>
    /// Help viewer form using WebView2 to display HTML-based help content.
    /// Similar to SettingsForm_ViewReleaseNotesHTML but for help system.
    /// </summary>
    public partial class HelpViewerForm : ThemedForm
    {
        #region Fields
        
        private readonly Model_HelpCategory? _category;
        private readonly List<Model_HelpCategory>? _allCategories;
        private readonly string? _initialTopicId;
        
        #endregion
        
        #region Constructors
        
        /// <summary>
        /// Creates a help viewer for a specific category.
        /// </summary>
        /// <param name="category">Help category to display</param>
        /// <param name="initialTopicId">Optional topic ID to scroll to on load</param>
        public HelpViewerForm(Model_HelpCategory category, string? initialTopicId = null)
        {
            InitializeComponent();
            _category = category;
            _initialTopicId = initialTopicId;
            
            this.Text = $"Help - {category.Category}";
            InitializeWebView();
        }
        
        /// <summary>
        /// Creates a help viewer showing all categories (index page).
        /// </summary>
        /// <param name="categories">All help categories</param>
        public HelpViewerForm(List<Model_HelpCategory> categories)
        {
            InitializeComponent();
            _allCategories = categories;
            
            this.Text = "Help - MTM WIP Application";
            InitializeWebView();
        }
        
        #endregion
        
        #region Methods
        
        private async void InitializeWebView()
        {
            try
            {
                await webView21.EnsureCoreWebView2Async();
                webView21.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
                DisplayHelp();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }
        
        private void CoreWebView2_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            // Handle navigation to other help categories
            if (e.Uri.StartsWith("#category_"))
            {
                e.Cancel = true;
                string categoryId = e.Uri.Replace("#category_", "");
                
                // Load new category
                var newCategory = Service_HelpContentLoader.LoadCategory(categoryId);
                if (newCategory != null)
                {
                    var newForm = new HelpViewerForm(newCategory);
                    newForm.Show();
                    this.Close();
                }
            }
        }
        
        private void DisplayHelp()
        {
            try
            {
                string htmlContent;
                
                if (_allCategories != null)
                {
                    // Display index page
                    htmlContent = Service_HelpTemplateEngine.GenerateIndexHtml(_allCategories);
                }
                else if (_category != null)
                {
                    // Display specific category
                    string watermarkBase64 = LoadWatermarkBase64();
                    htmlContent = Service_HelpTemplateEngine.GenerateHtml(_category, watermarkBase64);
                }
                else
                {
                    htmlContent = "<html><body><h1>No help content available</h1></body></html>";
                }
                
                webView21.NavigateToString(htmlContent);
                
                // Scroll to specific topic if specified
                if (!string.IsNullOrEmpty(_initialTopicId))
                {
                    _ = ScrollToTopicAsync(_initialTopicId);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                webView21.NavigateToString($"<html><body><h1>Error loading help</h1><p>{ex.Message}</p></body></html>");
            }
        }
        
        private async Task ScrollToTopicAsync(string topicId)
        {
            try
            {
                await Task.Delay(500); // Wait for page to load
                string script = $"document.getElementById('topic_{topicId}')?.scrollIntoView({{ behavior: 'smooth' }});";
                await webView21.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }
        
        private string LoadWatermarkBase64()
        {
            try
            {
                string? imagePath = GetFilePath(@"Resources\MTM.png");
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
            
            return string.Empty;
        }
        
        private string? GetFilePath(string relativePath)
        {
            // Check bin directory
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (File.Exists(path)) return path;
            
            // Check project root (for dev)
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            path = Path.Combine(projectRoot, relativePath);
            if (File.Exists(path)) return path;
            
            return null;
        }
        
        #endregion
    }
}
```

---

## IMPLEMENTATION CHECKLIST

### Phase 1: Data Models ✅
- [ ] Create `Models/Help/Model_HelpCategory.cs`
- [ ] Create `Models/Help/Model_HelpTopic.cs`
- [ ] Create `Models/Help/Model_HelpSearchResult.cs`
- [ ] Add XML documentation to all models

### Phase 2: JSON Content ✅
- [ ] Create `Documentation/Help/JSON/` directory
- [ ] Create `getting-started.json` (fresh content)
- [ ] Create `inventory-operations.json` (fresh content)
- [ ] Create `remove-operations.json` (fresh content)
- [ ] Create `infor-visual-integration.json` (New content for Visual Dashboard)
- [ ] Create `analytics-reporting.json` (New content for WIP Analytics)
- [ ] Create `settings-management.json` (fresh content)
- [ ] Create `admin-tools.json` (New content for Logs/Maintenance)
- [ ] Create `advanced-features.json` (fresh content)
- [ ] Create `transaction-history.json` (fresh content)
- [ ] Create `transfer-operations.json` (fresh content)
- [ ] Create `keyboard-shortcuts.json` (fresh content)
- [ ] Create `troubleshooting.json` (fresh content)
- [ ] Create `system-configuration.json` (combine system-requirements + database-configuration)

### Phase 3: HTML Templates ✅
- [ ] Create `Documentation/Help/Templates/` directory
- [ ] Create `help-base-template.html` (main template)
- [ ] Test template with sample JSON data

### Phase 4: Services ✅
- [ ] Create `Services/Help/Service_HelpContentLoader.cs`
- [ ] Create `Services/Help/Service_HelpTemplateEngine.cs`
- [ ] Create `Services/Help/Service_HelpSystem.cs`
- [ ] Add XML documentation to all services
- [ ] Add error handling with Service_ErrorHandler
- [ ] Add logging with LoggingUtility

### Phase 5: WinForms UI ✅
- [ ] Create `Forms/Help/HelpViewerForm.cs`
- [ ] Create `Forms/Help/HelpViewerForm.Designer.cs`
- [ ] Add WebView2 control to form
- [ ] Implement category navigation
- [ ] Implement topic scrolling
- [ ] Test with all JSON categories

### Phase 6: Integration ✅
- [ ] Add F1 keyboard shortcut handler in MainForm
- [ ] Add "Help" menu item to MainForm menu strip
- [ ] Update existing help references to use new system
- [ ] Remove/deprecate old static HTML help files (optional)

### Phase 7: Testing & Documentation ✅
- [ ] Test all help categories load correctly
- [ ] Test search functionality
- [ ] Test navigation between categories
- [ ] Test topic scrolling
- [ ] Test theming integration (light/dark themes)
- [ ] Add entry to RELEASE_NOTES.json
- [ ] Update AGENTS.md with help system information
- [ ] Create `.github/instructions/help-system.instructions.md` (explaining architecture and how to add content)

### Phase 8: Maintenance Tools ✅
- [ ] Create `.github/prompts/add-help-topic.prompt.md` (template for AI agents to generate new help JSON)

---

## KEY ARCHITECTURAL DECISIONS

### 1. **JSON-First Approach**
   - **Rationale**: Easier to maintain and update content without touching code
   - **Pattern**: Same as Release Notes system (proven to work)
   - **Benefit**: Content editors don't need HTML/CSS knowledge

### 2. **Single HTML Template**
   - **Rationale**: Reduces duplication, ensures consistency
   - **Pattern**: Similar to WinForms UserControl reuse
   - **Benefit**: One change updates all help pages

### 3. **WebView2 Rendering**
   - **Rationale**: Rich formatting, modern UI, already used in app
   - **Pattern**: Reuse existing Release Notes viewer approach
   - **Benefit**: No need to learn new rendering technology

### 4. **Static Services (No DI)**
   - **Rationale**: Help system is utility-like, doesn't need instance state
   - **Pattern**: Matches existing Helper_* pattern in codebase
   - **Benefit**: Simple to call from anywhere: `Service_HelpSystem.ShowHelp("getting-started")`

### 5. **Category-Based Organization**
   - **Rationale**: Matches ProProfs design, intuitive for users
   - **Pattern**: One JSON file per major feature area
   - **Benefit**: Easy to find and update related topics

---

## USAGE EXAMPLES

### Opening Help from MainForm

```csharp
// In MainForm.cs - F1 handler
protected override void OnKeyDown(KeyEventArgs e)
{
    if (e.KeyCode == Keys.F1)
    {
        e.Handled = true;
        Service_HelpSystem.ShowHelpIndex(); // Show help home
    }
    
    base.OnKeyDown(e);
}

// Help menu item click
private void HelpMenuItem_Click(object sender, EventArgs e)
{
    Service_HelpSystem.ShowHelpIndex();
}

// Context-sensitive help
private void InventoryTab_HelpButtonClicked(object sender, EventArgs e)
{
    Service_HelpSystem.ShowHelp("inventory-operations");
}

// Direct topic link
private void QuickButtonsHelp_Click(object sender, EventArgs e)
{
    Service_HelpSystem.ShowHelpTopic("advanced-features", "quickbuttons");
}
```

---

## MIGRATION STRATEGY

### Phased Rollout

**Phase 1 (Week 1)**: Infrastructure
- Set up models, services, and basic template
- Create 2-3 JSON files (getting-started, inventory-operations)
- Test rendering and navigation

**Phase 2 (Week 2)**: Content Creation
- Author remaining JSON files
- Review and update content for accuracy
- Add screenshots and examples

**Phase 3 (Week 3)**: Integration
- Add help access points throughout application
- Implement context-sensitive help
- Update menu items and keyboard shortcuts

**Phase 4 (Week 4)**: Polish & Testing
- User acceptance testing
- Fix bugs and improve search
- Final documentation and release notes

---

## SUCCESS CRITERIA

✅ **User Experience**
- Help loads in < 1 second
- Search returns relevant results instantly
- Navigation is intuitive (no training needed)
- Works across all themes (light/dark)

✅ **Maintainability**
- Content updates don't require code changes
- JSON format is easy to edit
- Adding new categories is straightforward
- Template changes apply to all pages

✅ **Consistency**
- Matches existing Release Notes viewer UX
- Follows MTM WIP Application design language
- Integrates with theme system
- Uses standard error handling and logging

✅ **Completeness**
- All application features documented
- Search covers all content
- Context-sensitive help works
- F1 keyboard shortcut functions

---

## NOTES

- **Theme Integration**: Help viewer automatically inherits from ThemedForm, so light/dark themes work out of the box
- **Error Handling**: ALL exceptions must use Service_ErrorHandler (never MessageBox.Show)
- **Logging**: Use LoggingUtility for tracking help system usage and errors
- **Future Enhancement**: Could add analytics to track which topics are most viewed
- **Mobile Consideration**: HTML is responsive, but WinForms app is desktop-only
- **Accessibility**: HTML templates should include ARIA labels and keyboard navigation

---

## REFERENCE FILES

**Study These Existing Files:**
1. `Forms/Settings/SettingsForm_ViewReleaseNotesHTML.cs` - WebView2 pattern
2. `RELEASE_NOTES.json` - JSON structure pattern
3. `Documentation/ReleaseNotes/release-notes.html` - HTML template pattern
4. `Documentation/Help/js/help-navigation.js` - Search implementation logic (ignore content)

**Do NOT Study:**
- `Documentation/Help/*.html` - Content is outdated. Use source code to understand features.

**Follow These Patterns:**
1. `.github/copilot-instructions.md` - Coding standards
2. `AGENTS.md` - Architecture guidelines
3. `.github/instructions/service-error-handler.instructions.md` - Error handling
4. `.github/instructions/service-logging.instructions.md` - Logging standards

---

END OF IMPLEMENTATION PROMPT
