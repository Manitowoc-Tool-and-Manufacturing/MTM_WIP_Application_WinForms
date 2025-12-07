# Tasks: Help System Enhancements / Contact Support System

## Phases

### Phase 1 – Setup
- [X] T001 Ensure MySQL 5.7.24 reachable (localhost:3306, root/root) and WebView2 runtime installed (manual check)
  - [X] T001.1 Verify MySQL connection: `& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot -e "SELECT VERSION();"`
  - [X] T001.2 Verify WebView2 runtime installed via registry or test app launch
  - [X] T001.3 Document any missing prerequisites in quickstart.md
- [X] T002 Restore/build solution: `dotnet build MTM_WIP_Application_Winforms.csproj -c Debug`
  - [X] T002.1 Run `dotnet restore MTM_WIP_Application_Winforms.csproj`
  - [X] T002.2 Run `dotnet build MTM_WIP_Application_Winforms.csproj -c Debug`
  - [X] T002.3 Verify zero errors before proceeding
- [X] T003 Create feature DB scripts staging area under Database/UpdatedStoredProcedures/feedback and Database/UpdatedStoredProcedures/system (no code changes yet)
  - [X] T003.1 Create folder `Database/UpdatedStoredProcedures/feedback/`
  - [X] T003.2 Create folder `Database/UpdatedStoredProcedures/system/` (if not exists)
  - [X] T003.3 Add placeholder README.md in each folder documenting purpose

### Phase 2 – Foundational (blocking)
- [X] T004 Add DDL scripts for new tables (UserFeedback, UserFeedbackComments, WindowFormMapping, UserControlMapping, TrackingNumberSequence, EmailNotificationConfig) in Database/UpdatedDatabase/ with MySQL 5.7.24 syntax
  - [X] T004.1 Create `schema_user_feedback.sql` with UserFeedback table (all columns from spec: FeedbackID, FeedbackType, TrackingNumber, UserID, timestamps, metadata, content fields, status fields, duplicate tracking); include UNIQUE constraint on TrackingNumber, FK to usr_users
  - [X] T004.2 Create `schema_user_feedback_comments.sql` with UserFeedbackComments table (CommentID, FeedbackID FK, UserID FK, CommentDateTime, CommentText, IsInternalNote); include CASCADE DELETE on FeedbackID
  - [X] T004.3 Create `schema_window_form_mapping.sql` with WindowFormMapping table (MappingID, CodebaseName UNIQUE, UserFriendlyName, IsActive, timestamps)
  - [X] T004.4 Create `schema_user_control_mapping.sql` with UserControlMapping table (MappingID, WindowFormMappingID FK, CodebaseName, UserFriendlyName, IsActive, timestamps); include CASCADE DELETE
  - [X] T004.5 Create `schema_tracking_number_sequence.sql` with TrackingNumberSequence table (SequenceID, FeedbackType, Year, NextNumber, LastGeneratedDateTime); include UNIQUE constraint on (FeedbackType, Year)
  - [X] T004.6 Create `schema_email_notification_config.sql` with EmailNotificationConfig table (ConfigID, FeedbackCategory UNIQUE, RecipientEmails, IsActive, timestamps)
  - [X] T004.7 Create indexes per spec: IX_UserFeedback_UserID, IX_UserFeedback_Status, IX_UserFeedback_FeedbackType, IX_UserFeedback_SubmissionDateTime, IX_UserFeedbackComments_FeedbackID, etc.
  - [X] T004.8 Run all DDL scripts against mtm_wip_application_winforms database and verify tables created
- [X] T005 [P] Add stored procedure scripts for UserFeedback* and UserFeedbackComments* under Database/UpdatedStoredProcedures/feedback/
  - [X] T005.1 Create `md_feedback_Insert.sql` with all input params (FeedbackType, UserID, WindowForm, ActiveSection, Category, CustomCategory, Severity, Priority, Title, Description, StepsToReproduce, ExpectedBehavior, ActualBehavior, BusinessJustification, AffectedUsers, Location1, Location2, ExpectedConsistency, ApplicationVersion, OSVersion, MachineIdentifier) and output params (p_Status, p_ErrorMsg, p_FeedbackID, p_TrackingNumber); call sys_tracking_number_GetNext internally
  - [X] T005.2 Create `md_feedback_GetAll.sql` with optional filter params (FilterStatus, FilterFeedbackType, FilterUserID, FilterDateFrom, FilterDateTo, FilterAssignedDeveloperID, FilterCategory) and output params; return DataTable with user info joined
  - [X] T005.3 Create `md_feedback_GetByUser.sql` with UserID input and output params; return all feedback for user
  - [X] T005.4 Create `md_feedback_GetById.sql` with FeedbackID input and output params; return single record with user info
  - [X] T005.5 Create `md_feedback_UpdateStatus.sql` with FeedbackID, NewStatus, AssignedToDeveloperID (optional), DeveloperNotes (optional), ModifiedByUserID inputs and output params; update LastUpdatedDateTime; validate developer role if assigned
  - [X] T005.6 Create `md_feedback_MarkDuplicate.sql` with FeedbackID, DuplicateOfFeedbackID, ModifiedByUserID inputs and output params; set IsDuplicate=1 and link
  - [X] T005.7 Create `md_feedback_ExportToCsv.sql` with filter params and output params; return all fields for CSV export
  - [X] T005.8 Create `md_feedback_comment_Insert.sql` with FeedbackID, UserID, CommentText, IsInternalNote inputs and output params (p_CommentID); update parent LastUpdatedDateTime
  - [X] T005.9 Create `md_feedback_comment_GetByFeedbackId.sql` with FeedbackID input and output params; return comments ordered by CommentDateTime with user info
  - [X] T005.10 Run all stored procedure scripts and verify creation via `SHOW PROCEDURE STATUS WHERE Db = 'mtm_wip_application_winforms'`
- [X] T006 [P] Add stored procedure scripts for mappings/tracking/email under Database/UpdatedStoredProcedures/system/
  - [X] T006.1 Create `sys_windowform_mapping_GetAll.sql` with IncludeInactive optional param and output params; return active or all mappings
  - [X] T006.2 Create `sys_windowform_mapping_Upsert.sql` with CodebaseName, UserFriendlyName, IsActive inputs and output params (p_MappingID); insert if not exists, update if exists
  - [X] T006.3 Create `sys_usercontrol_mapping_GetByWindow.sql` with WindowFormMappingID, IncludeInactive inputs and output params; return controls for window
  - [X] T006.4 Create `sys_usercontrol_mapping_Upsert.sql` with WindowFormMappingID, CodebaseName, UserFriendlyName, IsActive inputs and output params (p_MappingID)
  - [X] T006.5 Create `sys_tracking_number_GetNext.sql` with FeedbackType, Year inputs and output params (p_TrackingNumber); implement atomic increment with transaction + row lock; format as {PREFIX}-{YEAR}-{SEQUENCE}; handle collision with retry up to 3 times
  - [X] T006.6 Create `sys_email_notification_GetRecipients.sql` with FeedbackCategory input and output params (p_RecipientEmails); fall back to 'All' category if specific not found
  - [X] T006.7 Create `sys_email_notification_Upsert.sql` with FeedbackCategory, RecipientEmails, IsActive inputs and output params
  - [X] T006.8 Run all stored procedure scripts and verify creation
- [X] T007 Seed initial WindowFormMapping/UserControlMapping and EmailNotificationConfig data via migration script in Database/UpdatedDatabase/
  - [X] T007.1 Create `seed_window_form_mapping.sql` with INSERT statements for all forms from FR-001 table: MainForm, SettingsForm, PrintForm, Transactions, TransactionLifecycleForm, Form_InforVisualDashboard, Form_WIPUserAnalytics, Form_AnalyticsViewer, Form_ViewLogsForm, Form_ViewErrorReports, SettingsForm_ViewReleaseNotesHTML, Form_QuickButtonEdit, Form_ShortcutEdit, Form_PODetails, HelpViewerForm
  - [X] T007.2 Create `seed_user_control_mapping.sql` with INSERT statements for all controls from FR-001 table mapped to their parent forms (Control_InventoryTab, Control_AdvancedInventory, Control_RemoveTab, Control_AdvancedRemove, Control_TransferTab, Control_About, Control_Database, Control_PartIDManagement, Control_OperationManagement, Control_LocationManagement, Control_ItemTypeManagement, Control_User_Management, Control_Shortcuts, Control_Theme)
  - [X] T007.3 Create `seed_email_notification_config.sql` with fallback category 'All' and any team-specific categories (e.g., 'Integration Issue (Infor Visual)')
  - [X] T007.4 Run seed scripts and verify data via SELECT queries
- [ ] T008 Add Dao_UserFeedback and Dao_UserFeedbackComments (interface + implementation) in Data/
  - [ ] T008.1 Create `IDao_UserFeedback.cs` interface with methods: GetAllAsync(filters), GetByUserIdAsync(userId), GetByIdAsync(feedbackId), InsertAsync(model), UpdateStatusAsync(feedbackId, newStatus, assignedDeveloperId, notes, modifiedByUserId), MarkAsDuplicateAsync(feedbackId, duplicateOfId, modifiedByUserId), ExportToCsvAsync(filters)
  - [ ] T008.2 Create `Dao_UserFeedback.cs` implementation using Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync and ExecuteNonQueryWithStatusAsync; return Model_Dao_Result<T> for all methods; implement #region structure per coding standards
  - [ ] T008.3 Create `IDao_UserFeedbackComments.cs` interface with methods: InsertAsync(feedbackId, userId, commentText, isInternalNote), GetByFeedbackIdAsync(feedbackId)
  - [ ] T008.4 Create `Dao_UserFeedbackComments.cs` implementation using Helper_Database_StoredProcedure; return Model_Dao_Result<T>
  - [ ] T008.5 Add XML documentation to all public methods per FR-045
  - [ ] T008.6 Add nullable annotations per FR-046
- [ ] T009 [P] Add Dao_WindowFormMapping and Dao_UserControlMapping (interface + impl) in Data/
  - [ ] T009.1 Create `IDao_WindowFormMapping.cs` interface with methods: GetAllAsync(includeInactive), UpsertAsync(codebaseName, userFriendlyName, isActive)
  - [ ] T009.2 Create `Dao_WindowFormMapping.cs` implementation using Helper_Database_StoredProcedure
  - [ ] T009.3 Create `IDao_UserControlMapping.cs` interface with methods: GetByWindowAsync(windowFormMappingId, includeInactive), UpsertAsync(windowFormMappingId, codebaseName, userFriendlyName, isActive)
  - [ ] T009.4 Create `Dao_UserControlMapping.cs` implementation using Helper_Database_StoredProcedure
  - [ ] T009.5 Add XML documentation and nullable annotations
- [ ] T010 [P] Add Service_FeedbackManager skeleton in Services/
  - [ ] T010.1 Create `Service_FeedbackManager.cs` with constructor accepting IDao_UserFeedback, IDao_UserFeedbackComments, IDao_WindowFormMapping, IDao_UserControlMapping (DI-ready)
  - [ ] T010.2 Add method stubs: SubmitFeedbackAsync, GetUserSubmissionsAsync, GetSubmissionAsync, AddCommentAsync, UpdateStatusAsync, MarkDuplicateAsync, ExportToCsvAsync, GetTrackingNumberAsync, GetWindowMappingsAsync, GetControlMappingsAsync
  - [ ] T010.3 Add #region structure (Fields, Properties, Constructors, Methods, Helpers)
  - [ ] T010.4 Add XML documentation for all public methods
  - [ ] T010.5 Register in DI container (Program.cs or Startup.cs if exists)
- [ ] T011 Add HtmlSanitizer dependency and wire validation utility method
  - [ ] T011.1 Add `HtmlSanitizer` NuGet package to MTM_WIP_Application_Winforms.csproj
  - [ ] T011.2 Create `Helper_HtmlSanitizer.cs` in Helpers/ with static method SanitizeHtml(string input) returning sanitized string
  - [ ] T011.3 Integrate with Service_Validation or create new validation method ValidateAndSanitizeHtmlAsync
  - [ ] T011.4 Add XML documentation
- [ ] T012 Add WebView2 JS bridge handler scaffolding in Forms/Help/HelpViewerForm.cs
  - [ ] T012.1 Add WebMessageReceived event handler in HelpViewerForm constructor or InitializeComponent
  - [ ] T012.2 Implement secure message parsing (JSON deserialization with validation)
  - [ ] T012.3 Add handler for message types: 'submitFeedback', 'viewSubmissions', 'addComment', 'getWindowMappings', 'getControlMappings'
  - [ ] T012.4 Enforce local-only template loading: set CoreWebView2.Settings.IsScriptEnabled = true, CoreWebView2.Settings.AreRemoteJavaScriptObjectsAllowed = false
  - [ ] T012.5 Add CSP headers via NavigateToString or local HTML meta tags: default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'
  - [ ] T012.6 Add error handling via Service_ErrorHandler for malformed messages
  - [ ] T012.7 Add logging via LoggingUtility for all bridge interactions

### Phase 3 – User Story 1 (Context-Sensitive Help Buttons, P1)
- [ ] T013 [US1] Wire missing help buttons for MainForm tabs
  - [ ] T013.1 In `Controls/MainForm/Control_AdvancedInventory.cs`: Add help button with ID `MainForm_Button_Help_AdvancedInventory`; wire Click event to open HelpViewer with category='advanced-inventory-operations', topic='advanced-inventory-overview'
  - [ ] T013.2 In `Controls/MainForm/Control_RemoveTab.cs`: Add help button with ID `MainForm_Button_Help_Remove`; wire to category='remove-operations', topic='remove-overview'
  - [ ] T013.3 In `Controls/MainForm/Control_AdvancedRemove.cs`: Add help button with ID `MainForm_Button_Help_AdvancedRemove`; wire to category='advanced-remove-operations', topic='advanced-remove-overview'
  - [ ] T013.4 In `Controls/MainForm/Control_TransferTab.cs`: Add help button with ID `MainForm_Button_Help_Transfer`; wire to category='transfer-operations', topic='transfer-overview'
  - [ ] T013.5 Ensure all buttons inherit ThemedButton styling and position in top-right per FR-001 table
  - [ ] T013.6 Test each button opens correct help topic
- [ ] T014 [US1] Wire SettingsForm help buttons for panels
  - [ ] T014.1 In `Controls/SettingsForm/Control_SettingsHome.cs`: Add help button `SettingsForm_Button_Help_Home`; wire to category='settings-management', topic='home'
  - [ ] T014.2 In `Controls/SettingsForm/Control_About.cs`: Add help button `SettingsForm_Button_Help_About`; wire to category='settings-management', topic='about'
  - [ ] T014.3 In `Controls/SettingsForm/Control_Database.cs`: Add help button `SettingsForm_Button_Help_Database`; wire to category='settings-management', topic='database-config'
  - [ ] T014.4 In `Controls/SettingsForm/Control_PartIDManagement.cs`: Add help button `SettingsForm_Button_Help_PartNumbers`; wire to category='settings-management', topic='part-number-overview'
  - [ ] T014.5 In `Controls/SettingsForm/Control_OperationManagement.cs`: Add help button `SettingsForm_Button_Help_Operations`; wire to category='settings-management', topic='operation-overview'
  - [ ] T014.6 In `Controls/SettingsForm/Control_LocationManagement.cs`: Add help button `SettingsForm_Button_Help_Locations`; wire to category='settings-management', topic='location-overview'
  - [ ] T014.7 In `Controls/SettingsForm/Control_ItemTypeManagement.cs`: Add help button `SettingsForm_Button_Help_ItemTypes`; wire to category='settings-management', topic='inventory-type-overview'
  - [ ] T014.8 In `Controls/SettingsForm/Control_User_Management.cs`: Add help button `SettingsForm_Button_Help_Users`; wire to category='settings-management', topic='user-overview'
  - [ ] T014.9 In `Controls/SettingsForm/Control_Shortcuts.cs`: Add help button `SettingsForm_Button_Help_Shortcuts`; wire to category='settings-management', topic='shortcuts'
  - [ ] T014.10 In `Controls/SettingsForm/Control_Theme.cs`: Add help button `SettingsForm_Button_Help_Theme`; wire to category='settings-management', topic='themes'
  - [ ] T014.11 Position all buttons consistently in top-right panel area per FR-001 table
- [ ] T015 [US1] Wire specialized forms/dialogs help buttons
  - [ ] T015.1 In `Forms/Shared/PrintForm.cs`: Add help button `PrintForm_Button_Help`; wire to category='print-operations', topic='print-overview'
  - [ ] T015.2 In `Forms/Transactions/Transactions.cs`: Add help button `Transactions_Button_Help`; wire to category='transaction-history', topic='transaction-overview'
  - [ ] T015.3 In `Forms/Transactions/TransactionLifecycleForm.cs`: Add help button `TransactionLifecycleForm_Button_Help`; wire to category='transaction-history', topic='lifecycle'
  - [ ] T015.4 In `Forms/Visual/Form_InforVisualDashboard.cs`: Add help button `Form_InforVisualDashboard_Button_Help`; wire to category='infor-visual-integration', topic='visual-dashboard-overview'
  - [ ] T015.5 In `Forms/WIPAppAnalytics/Form_WIPUserAnalytics.cs`: Add help button `Form_WIPUserAnalytics_Button_Help`; wire to category='analytics-reporting', topic='analytics-overview'
  - [ ] T015.6 In `Forms/Visual/Form_AnalyticsViewer.cs`: Add help button `Form_AnalyticsViewer_Button_Help`; wire to category='analytics-reporting', topic='analytics-data'
  - [ ] T015.7 In `Forms/ViewLogs/Form_ViewLogsForm.cs`: Add help button `Form_ViewLogsForm_Button_Help`; wire to category='admin-tools', topic='logs'
  - [ ] T015.8 In `Forms/ErrorReports/Form_ViewErrorReports.cs`: Add help button `Form_ViewErrorReports_Button_Help`; wire to category='admin-tools', topic='error-reports'
  - [ ] T015.9 In `Forms/Settings/SettingsForm_ViewReleaseNotesHTML.cs`: Add help button `Form_ReleaseNotes_Button_Help`; wire to category='getting-started', topic='release-notes'
  - [ ] T015.10 In `Forms/Shared/Form_QuickButtonEdit.cs`: Add help button `Form_QuickButtonEdit_Button_Help`; wire to category='settings-management', topic='quick-buttons'
  - [ ] T015.11 In `Forms/Shared/Form_ShortcutEdit.cs`: Add help button `Form_ShortcutEdit_Button_Help`; wire to category='settings-management', topic='shortcuts'
  - [ ] T015.12 In `Forms/Visual/Form_PODetails.cs`: Add help button `Form_PODetails_Button_Help`; wire to category='infor-visual-integration', topic='po-details'
- [ ] T016 [US1] Update HelpViewer routing map to include all categories/topics referenced by FR-001 table
  - [ ] T016.1 Verify JSON help content files exist for all categories: inventory-operations, advanced-inventory-operations, remove-operations, advanced-remove-operations, transfer-operations, settings-management, print-operations, transaction-history, infor-visual-integration, analytics-reporting, admin-tools, getting-started
  - [ ] T016.2 Verify each category has required topics per FR-001 table
  - [ ] T016.3 Update HelpViewerForm.NavigateToCategory() method to handle all category/topic combinations
  - [ ] T016.4 Test singleton reuse: open help from one form, click help from another, verify same window navigates

### Phase 4 – User Story 2 (Help Menu Integration, P1)
- [ ] T017 [US2] Update MainForm menu to include Help menu items
  - [ ] T017.1 Add "Help" top-level menu item in MainForm.Designer.cs or MainForm menu strip
  - [ ] T017.2 Add "Help Index" menu item with shortcut F1; wire Click to HelpViewer.NavigateToIndex()
  - [ ] T017.3 Add "Getting Started" menu item with shortcut Ctrl+F1; wire Click to HelpViewer.NavigateToCategory('getting-started')
  - [ ] T017.4 Add "Keyboard Shortcuts" menu item with shortcut Ctrl+Shift+K; wire Click to HelpViewer.NavigateToCategory('keyboard-shortcuts')
  - [ ] T017.5 Add ToolStripSeparator
  - [ ] T017.6 Add "About" menu item; wire Click to show About dialog or navigate to About help topic
  - [ ] T017.7 Ensure menu items use ThemedMenuStrip styling if available
- [ ] T018 [US2] Ensure keyboard accelerators registered and do not conflict
  - [ ] T018.1 Review existing shortcuts in sys_shortcuts table and Control_Shortcuts.cs
  - [ ] T018.2 Verify F1 not already assigned to conflicting action
  - [ ] T018.3 Verify Ctrl+F1 not already assigned
  - [ ] T018.4 Verify Ctrl+Shift+K not already assigned
  - [ ] T018.5 Register accelerators in MainForm.KeyPreview and ProcessCmdKey override if needed
  - [ ] T018.6 Test all three shortcuts open correct help content

### Phase 5 – User Story 3 (Singleton Help Viewer, P1)
- [ ] T019 [US3] Enforce single-instance HelpViewer
  - [ ] T019.1 Add static field `private static HelpViewerForm? _instance` in HelpViewerForm.cs
  - [ ] T019.2 Add static method `public static HelpViewerForm GetInstance()` returning existing or new instance
  - [ ] T019.3 Override OnFormClosed to set `_instance = null`
  - [ ] T019.4 Add method `public void BringToFrontAndNavigate(string category, string? topic = null)` that: restores if minimized (WindowState = FormWindowState.Normal), brings to front (BringToFront(), Activate()), navigates to category/topic
  - [ ] T019.5 Update all help button Click handlers to use `HelpViewerForm.GetInstance().BringToFrontAndNavigate(category, topic)` instead of `new HelpViewerForm()`
  - [ ] T019.6 Handle case where form is disposing/disposed in GetInstance()
- [ ] T020 [US3] Add verification: clicking help from different forms reuses instance
  - [ ] T020.1 Manual test: Open help from Inventory tab, note window handle; open help from Settings, verify same window handle
  - [ ] T020.2 Manual test: Minimize help viewer, click help button, verify window restores and comes to front
  - [ ] T020.3 Document test results in manual verification checklist (T044)

### Phase 6 – User Story 4 (Contact Support System, P1)
- [ ] T021 [US4] Implement Contact Support templates in Documentation/Help/Templates/
  - [ ] T021.1 Create `help-contact-support-page.html` with navigation cards for: Report a Bug, Suggest an Improvement, Report an Inconsistency, Ask a Question, View My Submissions; include ARIA labels (role="navigation", aria-label)
  - [ ] T021.2 Create `help-bug-report-form.html` with fields: Window/Form dropdown (id="windowForm"), Active Section dropdown (id="activeSection"), Bug Category dropdown (id="bugCategory"), Severity dropdown (id="severity" with Critical/High/Medium/Low), Description textarea (id="description", maxlength="50000"), Steps to Reproduce textarea, Expected Behavior input, Actual Behavior input; add client-side validation hints (required attributes, aria-required="true"); add character counter for description
  - [ ] T021.3 Create `help-suggestion-form.html` with fields: Suggestion Category dropdown, Related Window dropdown (optional), Priority dropdown, Title input (required), Description textarea (maxlength="50000"), Business Justification textarea, Affected Users dropdown (Just Me/My Team/All Users/Specific Role); add ARIA labels and validation
  - [ ] T021.4 Create `help-inconsistency-form.html` with fields: Inconsistency Type dropdown, Window/Form dropdown, Active Section dropdown, Description textarea, Location 1 input, Location 2 input, Expected Consistency textarea; add ARIA labels
  - [ ] T021.5 Create `help-question-form.html` with fields: Question Category dropdown, Related Window dropdown (optional), Priority dropdown, Question textarea (required); add ARIA labels
  - [ ] T021.6 Create `help-view-submissions.html` with sortable/filterable table: Type, Title/Summary, Status, Priority/Severity, Submitted Date, Last Updated Date; include column header click-to-sort, filter dropdowns; add "Add Comment" button per row for In Progress items; display threaded comments with developer notes styled distinctly
  - [ ] T021.7 Add JavaScript postMessage handlers in each form template: `window.chrome.webview.postMessage(JSON.stringify({type: 'submitFeedback', data: {...}}))`
  - [ ] T021.8 Add CSS styling consistent with existing help templates; respect theme colors if applicable
- [ ] T022 [P] [US4] Implement WebView2 JS bridge handlers in HelpViewerForm.cs
  - [ ] T022.1 Parse incoming messages by type: 'submitFeedback' → call Service_FeedbackManager.SubmitFeedbackAsync
  - [ ] T022.2 Handle 'viewSubmissions' → call Service_FeedbackManager.GetUserSubmissionsAsync, return JSON to WebView2
  - [ ] T022.3 Handle 'addComment' → call Service_FeedbackManager.AddCommentAsync
  - [ ] T022.4 Handle 'getWindowMappings' → call Service_FeedbackManager.GetWindowMappingsAsync, return JSON for dropdown population
  - [ ] T022.5 Handle 'getControlMappings' → call Service_FeedbackManager.GetControlMappingsAsync with windowFormMappingId, return JSON
  - [ ] T022.6 Add async/await throughout; marshal results back to WebView2 via ExecuteScriptAsync or PostWebMessageAsJson
  - [ ] T022.7 Wrap all handlers in try-catch; log errors via LoggingUtility; show errors via Service_ErrorHandler
  - [ ] T022.8 Sanitize all incoming data via Helper_HtmlSanitizer before processing
- [ ] T023 [P] [US4] Implement Service_FeedbackManager methods
  - [ ] T023.1 Implement SubmitFeedbackAsync: validate all inputs via Service_Validation; sanitize HTML; call Dao_UserFeedback.InsertAsync; log submission via LoggingUtility (FR-036); return tracking number; trigger email notification if Critical/High bug
  - [ ] T023.2 Implement GetUserSubmissionsAsync: get current UserID from Model_Application_Variables.User; call Dao_UserFeedback.GetByUserIdAsync; return list
  - [ ] T023.3 Implement GetSubmissionAsync: call Dao_UserFeedback.GetByIdAsync; include comments via Dao_UserFeedbackComments.GetByFeedbackIdAsync
  - [ ] T023.4 Implement AddCommentAsync: validate CommentText; call Dao_UserFeedbackComments.InsertAsync; update parent LastUpdatedDateTime
  - [ ] T023.5 Implement UpdateStatusAsync: validate status transition; validate developer role if assigning; call Dao_UserFeedback.UpdateStatusAsync; log via LoggingUtility (FR-037)
  - [ ] T023.6 Implement MarkDuplicateAsync: validate both IDs exist; call Dao_UserFeedback.MarkAsDuplicateAsync
  - [ ] T023.7 Implement ExportToCsvAsync: call Dao_UserFeedback.ExportToCsvAsync; use ClosedXML or manual CSV generation; return file path or byte array
  - [ ] T023.8 Implement GetTrackingNumberAsync: call sys_tracking_number_GetNext SP via DAO; implement retry on collision (up to 3 attempts)
  - [ ] T023.9 Implement GetWindowMappingsAsync: call Dao_WindowFormMapping.GetAllAsync; filter active only
  - [ ] T023.10 Implement GetControlMappingsAsync: call Dao_UserControlMapping.GetByWindowAsync; filter active only
- [ ] T024 [P] [US4] Implement Dao_UserFeedback methods
  - [ ] T024.1 Implement GetAllAsync with filter parameter dictionary; call md_feedback_GetAll SP
  - [ ] T024.2 Implement GetByUserIdAsync; call md_feedback_GetByUser SP
  - [ ] T024.3 Implement GetByIdAsync; call md_feedback_GetById SP
  - [ ] T024.4 Implement InsertAsync with Model_UserFeedback parameter; build parameter dictionary; call md_feedback_Insert SP; extract p_FeedbackID and p_TrackingNumber from output
  - [ ] T024.5 Implement UpdateStatusAsync; call md_feedback_UpdateStatus SP
  - [ ] T024.6 Implement MarkAsDuplicateAsync; call md_feedback_MarkDuplicate SP
  - [ ] T024.7 Implement ExportToCsvAsync; call md_feedback_ExportToCsv SP
  - [ ] T024.8 Ensure all methods return Model_Dao_Result<T>; handle p_Status non-zero as IsSuccess=false
- [ ] T025 [P] [US4] Implement Dao_UserFeedbackComments methods
  - [ ] T025.1 Implement InsertAsync; call md_feedback_comment_Insert SP; extract p_CommentID
  - [ ] T025.2 Implement GetByFeedbackIdAsync; call md_feedback_comment_GetByFeedbackId SP
  - [ ] T025.3 Ensure all methods return Model_Dao_Result<T>
- [ ] T026 [P] [US4] Implement Dao_WindowFormMapping/Dao_UserControlMapping
  - [ ] T026.1 Implement Dao_WindowFormMapping.GetAllAsync; call sys_windowform_mapping_GetAll SP
  - [ ] T026.2 Implement Dao_WindowFormMapping.UpsertAsync; call sys_windowform_mapping_Upsert SP
  - [ ] T026.3 Implement Dao_UserControlMapping.GetByWindowAsync; call sys_usercontrol_mapping_GetByWindow SP
  - [ ] T026.4 Implement Dao_UserControlMapping.UpsertAsync; call sys_usercontrol_mapping_Upsert SP
- [ ] T027 [US4] Implement Service_Validation usage for Contact Support inputs
  - [ ] T027.1 Add validation rule for Description max length (50,000 chars) with warning at 45,000
  - [ ] T027.2 Add required field validation for: Description (all forms), Severity (bugs), Title (suggestions), Question (questions)
  - [ ] T027.3 Add dropdown validity check: ensure selected WindowForm/ActiveSection exists in mapping tables
  - [ ] T027.4 Add email format validation for EmailNotificationConfig recipient emails (semicolon-separated)
  - [ ] T027.5 Pipe all validation errors through Service_ErrorHandler.HandleValidationError with field name, caller name, control name
  - [ ] T027.6 Return aggregated validation result to WebView2 for client-side display
- [ ] T028 [US4] Implement LoggingUtility calls per FR-036..FR-039
  - [ ] T028.1 In SubmitFeedbackAsync: LoggingUtility.Log() with FeedbackType, UserID, TrackingNumber, timestamp (FR-036)
  - [ ] T028.2 In UpdateStatusAsync: LoggingUtility.Log() with FeedbackID, OldStatus, NewStatus, AssignedDeveloperID, timestamp (FR-037)
  - [ ] T028.3 In DAO catch blocks: LoggingUtility.LogDatabaseError() with SP name and error details (FR-038)
  - [ ] T028.4 In email send method: LoggingUtility.Log() with recipient list, FeedbackID, delivery status (FR-039)
- [ ] T029 [US4] Implement email notification trigger for Critical/High bugs
  - [ ] T029.1 After successful bug submission with Severity='Critical' or 'High', call Service_EmailNotification.SendNotificationAsync (or create if not exists)
  - [ ] T029.2 Lookup recipients via sys_email_notification_GetRecipients SP; use category from bug or fallback to 'All'
  - [ ] T029.3 Build email body with bug details: TrackingNumber, Description, Severity, Submitter name, Window/Section
  - [ ] T029.4 Use existing SMTP infrastructure (check for existing email helper or System.Net.Mail)
  - [ ] T029.5 Implement retry with exponential backoff: 5 min, 10 min, 15 min; log failures
  - [ ] T029.6 Log permanent failure for admin review after 3 retries
- [ ] T030 [US4] Implement Developer Tools form
  - [ ] T030.1 Create `Forms/DeveloperTools/Form_DeveloperTools.cs` inheriting ThemedForm
  - [ ] T030.2 Add DataGridView for feedback display with columns: TrackingNumber, Type, Title, Status, Priority/Severity, Submitter, SubmittedDate, LastUpdatedDate, AssignedDeveloper
  - [ ] T030.3 Add filter panel: Status dropdown, FeedbackType dropdown, User dropdown, Category dropdown, DateFrom/DateTo date pickers
  - [ ] T030.4 Add "Apply Filter" button; wire to Service_FeedbackManager.GetAllAsync with filter params
  - [ ] T030.5 Add context menu or buttons: Update Status, Add Notes, Assign Developer, Mark Duplicate, View Details
  - [ ] T030.6 Implement Update Status: show dropdown with New/In Review/In Progress/Resolved/Closed/Won't Fix; call Service_FeedbackManager.UpdateStatusAsync
  - [ ] T030.7 Implement Add Notes: show text dialog; call Service_FeedbackManager.UpdateStatusAsync with notes param
  - [ ] T030.8 Implement Assign Developer: show dropdown of users with Developer role; validate selection; call UpdateStatusAsync
  - [ ] T030.9 Implement Mark Duplicate: show dialog to enter DuplicateOfFeedbackID; call Service_FeedbackManager.MarkDuplicateAsync
  - [ ] T030.10 Add "Export to CSV" button; call Service_FeedbackManager.ExportToCsvAsync; prompt SaveFileDialog
  - [ ] T030.11 Implement role guard: in Form_Load, check Model_Application_Variables.User role; if not Admin/Developer, call Service_ErrorHandler.ShowUserError("Access denied"), then Close()
  - [ ] T030.12 Add to MainForm Development menu: "Developer Tools" menu item; wire to Form_DeveloperTools
  - [ ] T030.13 Hide/disable Developer Tools menu item for non-Admin/Developer users
- [ ] T031 [US4] Implement View My Submissions page
  - [ ] T031.1 Wire 'viewSubmissions' message type to Service_FeedbackManager.GetUserSubmissionsAsync
  - [ ] T031.2 Return JSON array of submissions to WebView2
  - [ ] T031.3 In help-view-submissions.html: populate table from received JSON; implement client-side sort on column header click
  - [ ] T031.4 Implement filter dropdowns client-side: Type, Status filter the displayed rows
  - [ ] T031.5 Add "View Details" link per row; clicking sends 'getSubmissionDetails' message; display full details + comments in modal or expanded section
  - [ ] T031.6 Add "Add Comment" button for items not Closed; sends 'addComment' message with FeedbackID and CommentText
  - [ ] T031.7 Style developer notes distinctly from user comments (e.g., different background color, "Developer Response" label)
- [ ] T032 [US4] Implement role-change detection (FR-047)
  - [ ] T032.1 In Form_DeveloperTools, add method CheckRoleStillValid() that queries current user role from database
  - [ ] T032.2 Call CheckRoleStillValid() before any action (Update Status, Add Notes, Assign, Export, etc.)
  - [ ] T032.3 If role changed to non-Developer/Admin: call Service_ErrorHandler.ShowUserError("Your permissions have changed. Please close this window."); disable all controls; set flag to prevent further actions
  - [ ] T032.4 Log unauthorized access attempt via LoggingUtility
- [ ] T033 [US4] Implement tracking number generation flow
  - [ ] T033.1 In Service_FeedbackManager.SubmitFeedbackAsync, call sys_tracking_number_GetNext SP via DAO
  - [ ] T033.2 Implement retry logic: if SP returns collision error (p_Status != 0), retry up to 3 times
  - [ ] T033.3 On 3rd failure, log fatal error via LoggingUtility; return error to user via Service_ErrorHandler
  - [ ] T033.4 Verify format: {PREFIX}-{YEAR}-{SEQUENCE} (e.g., BUG-2025-000001)
  - [ ] T033.5 Test annual reset: ensure 2026 submissions start at 000001
- [ ] T034 [US4] Seed EmailNotificationConfig defaults
  - [ ] T034.1 Ensure seed_email_notification_config.sql includes fallback category 'All' with at least one admin email
  - [ ] T034.2 Add category-specific configs if known (e.g., 'Integration Issue (Infor Visual)' → visual team emails)
  - [ ] T034.3 Verify via SELECT from EmailNotificationConfig after running seed script

### Phase 7 – User Story 5 (External Template File Loading, P2)
- [ ] T035 [US5] Ensure Service_HelpTemplateEngine loads base templates from external files
  - [ ] T035.1 In Service_HelpTemplateEngine, add method LoadTemplateFromFile(string templatePath) that reads from Documentation/Help/Templates/
  - [ ] T035.2 Add caching: load template once per viewer session; clear cache on viewer close or explicit refresh
  - [ ] T035.3 Verify template path uses relative path from application directory; handle deployed vs debug paths
  - [ ] T035.4 Test: modify template file, reopen viewer, verify changes reflected
- [ ] T036 [US5] Add error handling/logging for missing/corrupt templates
  - [ ] T036.1 In LoadTemplateFromFile, wrap File.ReadAllText in try-catch
  - [ ] T036.2 On FileNotFoundException: log via LoggingUtility.Log("Template file missing: {path}"); load fallback minimal template
  - [ ] T036.3 On parse/render error: log via LoggingUtility.Log("Template corrupt: {path}, error: {message}"); load fallback
  - [ ] T036.4 Define fallback minimal template as embedded resource or hardcoded string: basic HTML with message "Help content is temporarily unavailable"
  - [ ] T036.5 Show user-friendly message in viewer; don't crash or show exception

### Phase 8 – User Story 6 (Component Template Support, P3)
- [ ] T037 [US6] Add component templates and placeholder replacement
  - [ ] T037.1 Create `Documentation/Help/Templates/components/alert-component.html` with styled alert box (info, warning, error variants)
  - [ ] T037.2 Create `Documentation/Help/Templates/components/code-block-component.html` with syntax highlighting placeholder
  - [ ] T037.3 In Service_HelpTemplateEngine, add method ReplaceComponentPlaceholders(string html) that finds placeholders like `{{component:alert type="warning"}}content{{/component:alert}}` and replaces with rendered component
  - [ ] T037.4 Load component templates from external files; cache for performance
  - [ ] T037.5 Handle missing component template gracefully: log warning; render placeholder as plain text
- [ ] T038 [US6] Update topics to demonstrate component usage
  - [ ] T038.1 Update at least one help topic JSON file to include alert placeholder
  - [ ] T038.2 Update at least one help topic to include code block placeholder
  - [ ] T038.3 Test rendering: open topics in viewer; verify components display correctly
  - [ ] T038.4 Modify component template styling; verify topics reflect change on reload

### Phase 9 – User Story 7 (Watermark/Logo Integration, P3)
- [ ] T039 [US7] Add watermark rendering to help templates
  - [ ] T039.1 In help-base-template.html, add background-image CSS referencing logo: `body { background-image: url('file:///{APP_PATH}/Resources/MTM.png'); background-repeat: no-repeat; background-position: center; background-size: 50%; opacity: 0.05; }`
  - [ ] T039.2 In Service_HelpTemplateEngine, replace `{APP_PATH}` placeholder with actual application directory path
  - [ ] T039.3 Handle missing logo file: check File.Exists before rendering; if missing, skip watermark CSS
  - [ ] T039.4 Test: view help page; verify faint watermark visible
  - [ ] T039.5 Test graceful degradation: rename/delete MTM.png; verify page still renders without watermark

### Phase 10 – Polish & Cross-Cutting
- [ ] T040 Add XML documentation for all new public members (FR-045)
  - [ ] T040.1 Add XML docs to all Dao_UserFeedback methods: summary, param, returns, exception tags
  - [ ] T040.2 Add XML docs to all Dao_UserFeedbackComments methods
  - [ ] T040.3 Add XML docs to all Dao_WindowFormMapping methods
  - [ ] T040.4 Add XML docs to all Dao_UserControlMapping methods
  - [ ] T040.5 Add XML docs to all Service_FeedbackManager methods
  - [ ] T040.6 Add XML docs to Form_DeveloperTools public methods and constructor
  - [ ] T040.7 Add XML docs to Helper_HtmlSanitizer
  - [ ] T040.8 Add XML docs to all new model classes
  - [ ] T040.9 Run build; verify zero documentation warnings (CS1591)
- [ ] T041 Add nullable annotations for new reference types (FR-046)
  - [ ] T041.1 Enable nullable in all new files: `#nullable enable` or project-wide already enabled
  - [ ] T041.2 Annotate nullable parameters: string? for optional text fields, int? for optional IDs
  - [ ] T041.3 Annotate nullable return types: Model_Dao_Result<DataTable?> where result can be null
  - [ ] T041.4 Fix all nullable warnings (CS8600, CS8602, CS8603, CS8604)
  - [ ] T041.5 Verify build compiles with zero nullable warnings
- [ ] T042 Add integration test coverage for DAOs/Service_FeedbackManager
  - [ ] T042.1 Create `Dao_UserFeedbackTests.cs` extending BaseIntegrationTest
  - [ ] T042.2 Add test: InsertAsync with valid data returns IsSuccess=true and TrackingNumber
  - [ ] T042.3 Add test: GetByIdAsync returns inserted record
  - [ ] T042.4 Add test: UpdateStatusAsync changes status and updates LastUpdatedDateTime
  - [ ] T042.5 Add test: AddCommentAsync inserts comment linked to feedback
  - [ ] T042.6 Add test: MarkAsDuplicateAsync sets IsDuplicate=1 and links to primary
  - [ ] T042.7 Add test: GetByUserIdAsync returns only current user's submissions
  - [ ] T042.8 Add test: ExportToCsvAsync returns DataTable with all fields
  - [ ] T042.9 Create `Service_FeedbackManagerTests.cs` with integration tests for business logic
  - [ ] T042.10 Run all tests; verify pass
- [ ] T043 Add migration/runbook steps to quickstart.md
  - [ ] T043.1 Update Prerequisites section if needed (HtmlSanitizer NuGet)
  - [ ] T043.2 Add step-by-step DB setup: list all DDL scripts in order
  - [ ] T043.3 Add step-by-step SP setup: list all stored procedure scripts
  - [ ] T043.4 Add seed data step: list seed scripts
  - [ ] T043.5 Add verification queries to confirm tables/SPs/data exist
- [ ] T044 Manual verification checklist
  - [ ] T044.1 Verify submit + confirmation ≤ 2 seconds (SC-011)
  - [ ] T044.2 Verify filter/sort in Developer Tools ≤ 1 second (SC-016)
  - [ ] T044.3 Verify CSV export for 10k rows ≤ 3 seconds (SC-017); may need test data generation
  - [ ] T044.4 Verify email notification queued within 1 minute for Critical/High bug (SC-015)
  - [ ] T044.5 Verify offline handling: disable network, submit feedback, verify "offline" message in WebView2
  - [ ] T044.6 Verify long description (45k chars): warning appears; 50k+ chars: submission blocked
  - [ ] T044.7 Verify duplicate handling: mark duplicate in Developer Tools, verify relationship visible in both views
  - [ ] T044.8 Verify role guard: log in as non-admin, attempt to open Developer Tools, verify access denied
  - [ ] T044.9 Verify singleton: open help from multiple forms, verify only one window exists
  - [ ] T044.10 Document all verification results and sign off

## Dependencies (Story Order)
1) US1 (Help buttons) → independent
2) US2 (Help menu) → independent
3) US3 (Singleton) → independent
4) US4 (Contact Support) → depends on foundational DB + DAOs + JS bridge (Phase 2)
5) US5 (External template) → independent but benefits from template work in US4
6) US6 (Component templates) → after US5
7) US7 (Watermark) → after template work (US5)

## Parallel Execution Examples
- Parallel: T005/T006 (SP scripts) and T008/T009 (DAOs) once DDL (T004) drafted.
- Parallel: T022/T023/T024/T025/T026 after foundational scaffolding (T010/T011/T012).
- Parallel: US1 button wiring tasks T013/T014/T015 can run concurrently on different files.
- Parallel: T040 subtasks (XML docs) can be done per-file independently.

## MVP Suggestion
- Minimum increment: US4 core submission + View My Submissions + Developer Tools minimal (T021–T034) once foundational Phase 2 complete. US1/US2/US3 can proceed in parallel as UX polish.
