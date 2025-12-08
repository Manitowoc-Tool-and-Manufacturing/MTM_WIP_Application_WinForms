# Speckit: Help System Enhancements

## Feature Overview
**Feature**: Help System Enhancements
**Description**: A comprehensive upgrade to the application's help system, moving from static forms to a dynamic, WebView2-based solution with search, feedback, and interactive templates.

## Core Components
*   **UI**: `HelpViewerForm` (WebView2 host)
*   **Service**: `Service_HelpSystem` (Logic), `Service_HelpTemplateEngine` (Rendering), `Service_FeedbackManager` (Feedback)
*   **Data**: `Dao_UserFeedback` (Database interaction)
*   **Content**: JSON-based help topics and HTML templates.

## Key Workflows
1.  **Viewing Help**: User opens help -> `HelpViewerForm` loads -> `Service_HelpSystem` retrieves content -> `Service_HelpTemplateEngine` renders HTML -> WebView displays it.
2.  **Searching**: User types query -> WebView sends message -> `Service_HelpSystem` performs search -> Results rendered and displayed.
3.  **Submitting Feedback**: User clicks feedback button -> WebView shows form -> User submits -> `Service_FeedbackManager` validates and saves to DB via `Dao_UserFeedback`.

## Database Schema
*   **Tables**: `sys_help_feedback`, `sys_help_feedback_comments`, `sys_window_form_mapping`, `sys_user_control_mapping`.
*   **Stored Procedures**: `md_feedback_Insert`, `md_feedback_GetAll`, `md_feedback_UpdateStatus`, etc.

## Configuration
*   **Templates**: Located in `Documentation/Help/Templates/`.
*   **Content**: Located in `Documentation/Help/JSON/`.
*   **Settings**: `Model_Application_Variables.ThemeEnabled` controls dark/light mode.

## Future Considerations
*   **Remote Content**: The system is designed to potentially load content from a remote server in the future.
*   **Admin Interface**: A future "Help Editor" could allow admins to modify JSON content directly within the app.
