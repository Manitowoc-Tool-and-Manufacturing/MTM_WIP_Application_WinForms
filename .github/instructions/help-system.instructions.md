# Help System Instructions

## Overview
The MTM WIP Application uses a modern, JSON-driven help system. Content is decoupled from the application code, allowing updates without recompilation. The system uses `WebView2` to render HTML content generated from JSON data.

## Architecture
- **Data Source**: JSON files in `Documentation/Help/JSON/`.
- **Models**: `Model_HelpCategory`, `Model_HelpTopic`.
- **Services**: 
  - `Service_HelpContentLoader`: Deserializes JSON.
  - `Service_HelpTemplateEngine`: Generates HTML.
  - `Service_HelpSystem`: Orchestrates loading and search.
- **UI**: `HelpViewerForm` (WebView2).

## Adding New Content
1. Create a new JSON file in `Documentation/Help/JSON/`.
2. Use the following structure:
   ```json
   {
     "CategoryId": "unique-id",
     "Title": "Display Title",
     "Icon": "emoji or text icon",
     "Description": "Short description.",
     "Order": 10,
     "Topics": [
       {
         "TopicId": "topic-id",
         "Title": "Topic Title",
         "Summary": "Search summary.",
         "Content": "HTML content...",
         "Keywords": ["keyword1", "keyword2"]
       }
     ]
   }
   ```

## Editing Templates
HTML templates are located in `Documentation/Help/Templates/`.
- `help-base-template.html`: Main layout.
- `topic-card-template.html`: Index card.
- `search-box-template.html`: Search UI.

## Search Logic
Search is performed in-memory by `Service_HelpSystem.Search()`. It ranks results based on matches in Title (10 pts), Keywords (8 pts), Summary (5 pts), and Content (1 pt).

## Theming
The system automatically detects the OS color scheme (light/dark) via CSS media queries in `help-base-template.html`.
