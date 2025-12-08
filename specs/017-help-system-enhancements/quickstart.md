# Quickstart (Phase 1)

## Prerequisites
- .NET 8.0 SDK (windowsdesktop)
- MySQL 5.7.24 running (localhost:3306, root/root per project defaults)
- WebView2 Runtime installed
- HtmlSanitizer NuGet (installed)

## Setup

### 1. Build Application
```powershell
dotnet restore MTM_WIP_Application_Winforms.csproj
dotnet build MTM_WIP_Application_Winforms.csproj -c Debug
```

### 2. Database Migration (Run in Order)

**Step 2.1: Create Tables**
Run the following scripts from `Database/UpdatedDatabase/` against your database (`mtm_wip_application_winforms`):
1. `schema_user_feedback.sql`
2. `schema_user_feedback_comments.sql`
3. `schema_window_form_mapping.sql`
4. `schema_user_control_mapping.sql`
5. `schema_tracking_number_sequence.sql`
6. `schema_email_notification_config.sql`

**Step 2.2: Create Stored Procedures (Feedback)**
Run the following scripts from `Database/UpdatedStoredProcedures/feedback/`:
1. `md_feedback_Insert.sql`
2. `md_feedback_GetAll.sql`
3. `md_feedback_GetByUser.sql`
4. `md_feedback_GetById.sql`
5. `md_feedback_UpdateStatus.sql`
6. `md_feedback_MarkDuplicate.sql`
7. `md_feedback_ExportToCsv.sql`
8. `md_feedback_comment_Insert.sql`
9. `md_feedback_comment_GetByFeedbackId.sql`

**Step 2.3: Create Stored Procedures (System)**
Run the following scripts from `Database/UpdatedStoredProcedures/system/`:
1. `sys_windowform_mapping_GetAll.sql`
2. `sys_windowform_mapping_Upsert.sql`
3. `sys_usercontrol_mapping_GetByWindow.sql`
4. `sys_usercontrol_mapping_Upsert.sql`
5. `sys_tracking_number_GetNext.sql`
6. `sys_email_notification_GetRecipients.sql`
7. `sys_email_notification_Upsert.sql`

### 3. Seed Data
Run the following scripts from `Database/UpdatedDatabase/`:
1. `seed_window_form_mapping.sql`
2. `seed_user_control_mapping.sql`
3. `seed_email_notification_config.sql`

### 4. Verification
Run these queries to confirm successful setup:

```sql
-- Verify tables exist
SHOW TABLES LIKE 'UserFeedback';
SHOW TABLES LIKE 'WindowFormMapping';

-- Verify stored procedures exist
SHOW PROCEDURE STATUS WHERE Db = 'mtm_wip_application_winforms' AND Name LIKE 'md_feedback_%';

-- Verify seed data
SELECT COUNT(*) FROM WindowFormMapping; -- Should be > 0
SELECT COUNT(*) FROM UserControlMapping; -- Should be > 0
SELECT * FROM EmailNotificationConfig; -- Should show 'All' category
```

### 5. Run Application
```powershell
dotnet run --project MTM_WIP_Application_Winforms.csproj
```

## Development Notes
- All new forms inherit ThemedForm; all user controls inherit ThemedUserControl.
- All DAOs use Helper_Database_StoredProcedure and return Model_Dao_Result<T>.
- All validation goes through Service_Validation; errors surfaced via Service_ErrorHandler.HandleValidationError.
- Logging via LoggingUtility for submissions, status changes, DB errors, email attempts.
- WebView2: load only local templates from Documentation/Help/Templates; use window.chrome.webview.postMessage bridge; sanitize any HTML rendered from user input.

## Verification Targets (from spec)
- Submit + confirmation ≤ 2s; filter/sort ≤ 1s; CSV export (10k rows) ≤ 3s; email queue trigger ≤ 1 minute.
- User-friendly names only in dropdowns; tracking numbers unique per type/year; description up to 50,000 chars.
