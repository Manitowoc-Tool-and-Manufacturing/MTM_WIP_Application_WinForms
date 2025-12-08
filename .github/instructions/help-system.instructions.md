# Help System Instructions

## Overview
The MTM WIP Application features a comprehensive, JSON-driven help system that renders HTML content via `WebView2`. This system is designed to be decoupled from the application binary, allowing for content updates without recompilation. It supports rich text, interactive components, and a fully integrated feedback/support system.

## Architecture

### Core Components
- **Data Source**: JSON files located in `Documentation/Help/JSON/`.
- **Models**: 
  - `Model_HelpCategory`: Represents a major section (file-level).
  - `Model_HelpTopic`: Represents a single article within a category.
  - `Model_HelpSearchResult`: Represents a search hit with relevance scoring.
- **Services**:
  - `Service_HelpContentLoader`: Deserializes JSON content asynchronously.
  - `Service_HelpTemplateEngine`: Generates HTML from templates, processes components, and injects dynamic data (watermarks, theme hooks).
  - `Service_HelpSystem`: Orchestrates initialization, caching, and in-memory search.
  - `Service_FeedbackManager`: Handles backend logic for user feedback, bug reports, and support tickets.
- **UI**: `HelpViewerForm` hosts the `WebView2` control and manages the JavaScript bridge.

### Data Flow
1. **Initialization**: `Service_HelpSystem` uses `Service_HelpContentLoader` to read all `*.json` files from `Documentation/Help/JSON/`.
2. **Rendering**: When a topic is requested, `Service_HelpTemplateEngine` loads the appropriate HTML template, injects the content, and applies the base layout.
3. **Interaction**: The `WebView2` control communicates with the C# backend via `chrome.webview.postMessage` for actions like submitting feedback or navigating.

## Content Management

### Adding New Content
To add a new help category, create a new `.json` file in `Documentation/Help/JSON/`. The filename does not matter, but the content must follow this schema:

```json
{
  "CategoryId": "unique-category-id",
  "Title": "Category Display Title",
  "Icon": "📝", 
  "Description": "Short description for the index card.",
  "Order": 10,
  "Topics": [
    {
      "TopicId": "topic-unique-id",
      "Title": "Article Title",
      "Summary": "Brief summary used for search results.",
      "Content": "<p>HTML content goes here.</p>",
      "Keywords": ["search", "terms", "tags"],
      "LastUpdated": "2023-10-27T00:00:00"
    }
  ]
}
```

### Content Guidelines
- **IDs**: Must be URL-safe strings (kebab-case recommended).
- **HTML**: Use standard HTML5 tags.
- **Images**: Can be referenced relatively or embedded (though external images are blocked by CSP).
- **Components**: Use the custom component syntax for rich elements.

## Template System

Templates are stored in `Documentation/Help/Templates/`.

### Key Templates
- `help-base-template.html`: The master layout containing `<head>`, CSS, and the sidebar structure.
- `topic-card-template.html`: The HTML fragment for a category card on the home page.
- `search-box-template.html`: The search bar UI.
- **Forms**: `help-bug-report-form.html`, `help-contact-support-page.html`, etc.

### Component System
The template engine supports a custom syntax to inject reusable UI components:

**Syntax:**
```html
{{component:name attribute="value"}}
Inner Content
{{/component:name}}
```

**Available Components:**
1. **Alert**:
   ```html
   {{component:alert type="warning"}}
   This is a warning message.
   {{/component:alert}}
   ```
   - Attributes: `type` ("info", "warning", "error", "success")

2. **Code Block**:
   ```html
   {{component:code language="csharp"}}
   var x = 1;
   {{/component:code}}
   ```

## Interactive Features & JS Bridge

The `HelpViewerForm` exposes a JavaScript bridge to enable two-way communication between the HTML content and the WinForms application.

### JavaScript -> C# Messages
The HTML content sends messages using `window.chrome.webview.postMessage(payload)`.

**Supported Message Types:**
- `submitFeedback`: Submits a bug report or suggestion.
- `viewSubmissions`: Requests the user's feedback history.
- `addComment`: Adds a comment to an existing ticket.
- `getWindowMappings`: Retrieves list of windows for bug reporting.
- `getControlMappings`: Retrieves controls for a specific window.

### C# -> JavaScript Events
The application executes JavaScript in the WebView context to return data:
- `onSubmissionsLoaded(data)`
- `onWindowMappingsLoaded(data)`
- `onControlMappingsLoaded(data)`
- `onFeedbackSubmitted(trackingNumber)`

## Search Logic

Search is performed in-memory by `Service_HelpSystem.Search()`. It calculates a relevance score for each topic:

| Match Location | Points |
|----------------|--------|
| Title          | 10     |
| Keywords       | 8      |
| Summary        | 5      |
| Content        | 1      |

Results are sorted by score (descending).

## Theming & Watermark

- **Theming**: The system automatically detects the OS color scheme (Light/Dark) using CSS media queries (`@media (prefers-color-scheme: dark)`).
- **Watermark**: A watermark is injected into the `help-base-template.html` via the `{{WATERMARK_URL}}` placeholder. It uses a local resource (`Resources/MTM.png`) if available, falling back to an SVG data URI.

## Troubleshooting

- **Missing Content**: Check `Service_HelpContentLoader` logs for JSON parsing errors.
- **Template Errors**: Missing templates will render an error div in place of the content.
- **WebView2**: Ensure the WebView2 Runtime is installed on the target machine.
