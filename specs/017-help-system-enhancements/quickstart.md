# Quickstart (Phase 1)

## Prerequisites
- .NET 8.0 SDK (windowsdesktop)
- MySQL 5.7.24 running (localhost:3306, root/root per project defaults)
- WebView2 Runtime installed
- HtmlSanitizer NuGet (planned) available for validation layer

## Setup
1. Restore/build: `dotnet build MTM_WIP_Application_Winforms.csproj -c Debug`
2. Apply DB schema updates (tables + procs) from [specs/017-help-system-enhancements/spec.md](specs/017-help-system-enhancements/spec.md):
   - Create tables: UserFeedback, UserFeedbackComments, WindowFormMapping, UserControlMapping, TrackingNumberSequence, EmailNotificationConfig
   - Create stored procedures: md_feedback_*, md_feedback_comment_*, sys_windowform_mapping_*, sys_usercontrol_mapping_*, sys_tracking_number_GetNext, sys_email_notification_*
3. Seed WindowFormMapping/UserControlMapping and EmailNotificationConfig using SQL migration scripts (to be added under Database/UpdatedStoredProcedures/). Default email config should include a fallback category 'All'.
4. Run app: `dotnet run --project MTM_WIP_Application_Winforms.csproj`

## Development Notes
- All new forms inherit ThemedForm; all user controls inherit ThemedUserControl.
- All DAOs use Helper_Database_StoredProcedure and return Model_Dao_Result<T>.
- All validation goes through Service_Validation; errors surfaced via Service_ErrorHandler.HandleValidationError.
- Logging via LoggingUtility for submissions, status changes, DB errors, email attempts.
- WebView2: load only local templates from Documentation/Help/Templates; use window.chrome.webview.postMessage bridge; sanitize any HTML rendered from user input.

## Verification Targets (from spec)
- Submit + confirmation ≤ 2s; filter/sort ≤ 1s; CSV export (10k rows) ≤ 3s; email queue trigger ≤ 1 minute.
- User-friendly names only in dropdowns; tracking numbers unique per type/year; description up to 50,000 chars.
