# Updates Since 2025-12-07

This document lists the updates made to the project since December 7th, 2025.
Each update has been verified by analyzing the code changes.

## Summary of Changes

| Category | Description |
|----------|-------------|
| **Server Update** | Changes related to server address, connection strings, and database connectivity. |
| **Stability Update** | Fixes for crashes, race conditions, and resource management. |
| **Feature Update** | Enhancements to existing features. |
| **New Feature** | Completely new functionality added. |
| **Refactoring** | Code structure improvements without changing external behavior. |

---

## Detailed Updates

### Helpers/Helper_Database_Variables.cs
- **Server Update**: Updated connection string to include connection pooling (Pooling=true, MinPoolSize=5, MaxPoolSize=100) and ConnectionReset=true.
- **Server Update**: Changed SslMode from none to Disabled for compatibility with newer MySQL drivers.
- **Refactoring**: Removed GetLogFilePathAsync method (functionality moved to Helper_LogPath.cs).

### Models/Core/Model_Application_Variables.cs
- **Feature Update**: Added ConnectionIdleTimeoutMs property to support configurable database connection idle timeouts.

### Models/Shared/Model_Shared_Users.cs
- **Server Update**: Implemented conditional compilation to automatically select the test database in Debug mode and production database in Release mode.

### Services/Startup/Service_OnStartup_Database.cs
- **Refactoring**: Converted ValidateConnectivity to ValidateConnectivityAsync for non-blocking execution.
- **Refactoring**: Updated methods to return Model_StartupResult for better error propagation to the startup orchestrator.
- **Stability Update**: Removed direct error handling and application restart logic, delegating control to the startup orchestrator.

### Program.cs
- **Stability Update**: Implemented single-instance check using Mutex to prevent multiple application instances.
- **Stability Update**: Added global exception handling around Application.Run to catch and log startup crashes.

### Services/Logging/Service_LoggingUtility.cs
- **Stability Update**: Implemented thread-safe log queue using BlockingCollection to prevent race conditions during concurrent logging.
- **Stability Update**: Added background log processing task to handle file I/O off the main thread.
- **Refactoring**: Removed GetLogFilePathAsync and app data cleanup logic (moved to Helper_LogPath and Startup Orchestrator).

### Services/Startup/Service_OnStartup_AppLifecycle.cs
- **Refactoring**: Implemented ExecuteStartupSequenceAsync as a central orchestrator for the application startup process.
- **Refactoring**: Centralized initialization logic for logging, database, user settings, and themes.
- **Stability Update**: Improved error handling and progress reporting during startup.

### Services/Startup/Service_OnStartup_DependencyInjection.cs
- **Feature Update**: Registered new services for the User Feedback System (IDao_UserFeedback, IService_FeedbackManager).

### Forms/Help/HelpViewerForm.cs
- **New Feature**: Implemented two-way communication between WebView2 and WinForms to support interactive help features.
- **New Feature**: Added handlers for feedback submission, viewing past submissions, and adding comments directly from the help viewer.
- **Refactoring**: Implemented Singleton pattern to ensure only one help viewer instance exists.

### Services/Help/Service_HelpSystem.cs
- **Feature Update**: Integrated with User Feedback DAOs to support submitting feedback and comments.

### Forms/Settings/SettingsForm.cs
- **Feature Update**: Added context-sensitive help buttons to all settings panels.
- **Feature Update**: Renamed 'Home' to ' Back to Home' for better navigation clarity.

### Forms/ViewLogs/Form_ViewLogsForm.cs
- **Feature Update**: Added a help button linking directly to the log viewer documentation.

### Forms/MainForm/MainForm.cs
- **Feature Update**: Implemented IConnectionRecoveryView to support real-time connection status updates (Signal Strength, Disconnected Banner).
- **Feature Update**: Added global keyboard shortcuts for Help (F1), Getting Started (Ctrl+F1), and Keyboard Shortcuts (Ctrl+Shift+K).
- **Feature Update**: Added 'Developer Tools' menu for accessing advanced debugging features.

### Services/Service_FeedbackManager.cs
- **New Feature**: Created Service_FeedbackManager to centralize all user feedback operations (Submission, Retrieval, Comments, Status Updates).
- **Feature Update**: Implemented input validation and HTML sanitization for feedback content.
- **Feature Update**: Integrated automatic email notifications for high-severity bug reports.

### Forms/DeveloperTools/Form_DeveloperTools.cs
- **New Feature**: Added a dedicated Developer Tools interface for managing user feedback and system mappings.
- **Security**: Implemented strict role-based access control for developer features.

### Services/Database/Service_ConnectionRecoveryManager.cs
- **Refactoring**: Decoupled connection recovery logic from the main form using the IConnectionRecoveryView interface.
- **Stability Update**: Improved thread safety for UI updates during connection loss/recovery.

### Services/Service_EmailNotification.cs
- **New Feature**: Implemented an email notification service with retry logic and recipient management.
- **Note**: Currently configured for simulation/logging mode until SMTP details are finalized.

### Database Updates
- **New Feature**: Added schema and stored procedures for the User Feedback System (Feedback, Comments, Attachments).
- **New Feature**: Added schema and stored procedures for Email Notification Configuration.
- **Refactoring**: Updated system stored procedures for better performance and parameter handling.


## TODO

### Services/Service_EmailNotification.cs
- **Implementation Required**: The email sending logic is currently a simulation. A real SMTP client needs to be configured and implemented.
  - **Action**: Replace the simulation block in SendEmailWithRetryAsync with actual SmtpClient logic.
  - **Configuration**: Add SMTP settings (Host, Port, Credentials) to Model_Application_Variables or a secure configuration store.
  - **Security**: Ensure email credentials are not hardcoded.

### Database/UpdatedStoredProcedures
- **Verification Required**: Ensure that the stored procedures md_feedback_GetAll, md_feedback_GetByUser, sys_email_notification_GetRecipients, etc., are deployed to the target database.
  - **Action**: Run the SQL scripts located in Database/UpdatedStoredProcedures/ against the development and production databases.

