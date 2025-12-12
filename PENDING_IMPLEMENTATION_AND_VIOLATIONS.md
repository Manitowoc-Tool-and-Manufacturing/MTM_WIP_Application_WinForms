# Pending Implementation and Codebase Violations

This document outlines features that are not fully implemented and code that violates the project's architectural guidelines (`constitution.md`, `copilot-instructions.md`, `AGENTS.md`).

## Summary of Violations

| Category | Violation | Files Affected |
|----------|-----------|----------------|
| **Architecture** | Direct Database Access (`MySqlConnection`) | `Service_OnStartup_Database.cs`, `Service_Migration.cs`, `Service_Analytics.cs`, `Service_ErrorReportSync.cs` |
| **Architecture** | UI in Data/Core Layers (`MessageBox.Show`) | `Dao_ErrorLog.cs`, `Core_Themes.cs` |
| **Documentation** | Missing `#region` Blocks | `Form_DeveloperTools.cs`, `HelpViewerForm.cs` |
| **Documentation** | Missing XML Documentation | `Form_DeveloperTools.cs` |
| **Implementation** | Simulation / Mock Logic | `Service_EmailNotification.cs` |

---

## Detailed Analysis

### Data/Dao_ErrorLog.cs
- **Violation**: Uses `MessageBox.Show` directly in `HandleException_SqlError_CloseApp` and `HandleException_GeneralError_CloseApp`.
- **Requirement**: All error display must be routed through `Service_ErrorHandler`.
- **Action**: Refactor to use `Service_ErrorHandler.ShowFatalError` or similar methods.

### Core/Core_Themes.cs
- **Violation**: Uses `MessageBox.Show` in the "Save Grid Settings" context menu handler.
- **Requirement**: Core logic should not directly invoke UI elements.
- **Action**: Use `Service_ErrorHandler.ShowInformation` for success messages.

### Services/Startup/Service_OnStartup_Database.cs
- **Violation**: Instantiates `new MySqlConnection` in `InitializeParameterCache`.
- **Requirement**: All database access must go through `Helper_Database_StoredProcedure`.
- **Action**: Refactor to use `Helper_Database_StoredProcedure.ExecuteReaderAsync` or similar.

### Services/Maintenance/Service_Migration.cs
- **Violation**: Multiple instances of `new MySqlConnection`.
- **Requirement**: All database access must go through `Helper_Database_StoredProcedure`.
- **Action**: Refactor migration logic to use the centralized helper.

### Services/Analytics/Service_Analytics.cs
- **Violation**: Uses `new MySqlConnection` for analytics queries.
- **Requirement**: All database access must go through `Helper_Database_StoredProcedure`.
- **Action**: Refactor to use stored procedures via the helper.

### Services/ErrorHandling/Service_ErrorReportSync.cs
- **Violation**: Uses `new MySqlConnection` for syncing error reports.
- **Requirement**: All database access must go through `Helper_Database_StoredProcedure`.
- **Action**: Refactor to use stored procedures via the helper.

### Services/Service_EmailNotification.cs
- **Status**: **Incomplete / Simulation**
- **Details**: The `SendEmailWithRetryAsync` method contains a simulation block and commented-out `SmtpClient` code.
- **Action**: Implement actual SMTP logic using configuration from `Model_Application_Variables`.

### Forms/DeveloperTools/Form_DeveloperTools.cs
- **Status**: **Functional but Non-Compliant**
- **Violations**:
    -   Missing standard `#region` blocks (Fields, Properties, Constructors, Methods, etc.).
    -   Missing XML documentation for class and methods.
- **Action**: Reorganize code into regions and add XML comments.

### Forms/Help/HelpViewerForm.cs
- **Status**: **Functional but Non-Compliant**
- **Violations**:
    -   Missing standard `#region` blocks.
- **Action**: Reorganize code into regions.

