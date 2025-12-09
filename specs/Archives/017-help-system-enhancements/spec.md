# Feature Specification: Help System Enhancements

**Feature Branch**: `017-help-system-enhancements`  
**Created**: 2025-12-07  
**Status**: Draft  
**Parent Feature**: `016-new-help-system`  
**Input**: Analysis of NEW_HELP_SYSTEM_IMPLEMENTATION_PROMPT.md against current implementation

## Clarifications

### Session 2025-12-07

- Q: Window/Form mapping management - Should there be an admin UI in Developer Tools, SQL migration scripts, or hybrid approach? → A: Extend existing Form_DatabaseMaintenance with Window/Form mapping management tab. Initial seed data via SQL migration script (version controlled), admin UI allows adding/editing/deactivating mappings as forms evolve. Validation ensures mapped forms/controls exist in codebase.

- Q: Email notification configuration - Where are recipient email addresses stored and should there be per-category recipient configuration? → A: Developer Tools admin UI with per-category recipient configuration stored in database Settings table. Runtime configurability without app restarts, supports category-specific routing (e.g., Visual Integration bugs → Visual team).

- Q: Tracking number sequence management - Should sequence reset annually or globally increment forever? → A: Annual reset using TrackingNumberSequence table (FeedbackType, Year, NextNumber columns). Clean yearly numbering (BUG-2025-000001 → BUG-2026-000001), MySQL 5.7.24 compatible, prevents sequence exhaustion.

- Q: Developer assignment workflow - Single or multiple assignments, auto-assignment, handling inactive developers? → A: Single assignment only, manual assignment, no auto-assignment. Clear ownership (one developer responsible per issue), simple status tracking, manual reassignment if developer unavailable.

- Q: Duplicate detection strategy - Auto-suggest duplicates or manual only? → A: Manual only with no auto-detection. Developers mark duplicates after review in Developer Tools. Prioritizes simplicity and accuracy, avoids false positives from automated similarity matching.

---

## User Scenarios & Testing *(mandatory)*

<!--
  IMPORTANT: User stories are PRIORITIZED as user journeys ordered by importance.
  Each user story/journey is INDEPENDENTLY TESTABLE - implementing just ONE story
  delivers a viable increment of value. 
  
  Priorities assigned: P1 (critical), P2 (important), P3 (nice-to-have)
-->

### User Story 1 - Context-Sensitive Help Buttons on All Forms (Priority: P1)

As a user, I want to click a help button on any screen (Inventory, Remove, Transfer, Settings, etc.) and be taken directly to the relevant help topic so that I don't have to browse or search for context-specific information.

**Why this priority**: Context-sensitive help is the most impactful feature for user productivity. Currently only one tab has a help button.  Users waste time manually searching for relevant topics.  This eliminates that friction and makes help instantly accessible from every major workflow.

**Independent Test**: Can be fully tested by clicking help buttons on each major form/tab and verifying the correct category opens with the first topic displayed.  No dependencies on other stories.

**Acceptance Scenarios**:

1. **Given** I am on the "Inventory" tab in MainForm, **When** I click the Help button, **Then** the help viewer opens to the "inventory-operations" category with "inventory-overview" topic displayed. 
2. **Given** I am on the "Advanced Inventory" tab in MainForm, **When** I click the Help button, **Then** the help viewer opens to the "advanced-inventory-operations" category with "advanced-inventory-overview" topic displayed.
3. **Given** I am on the "Remove" tab in MainForm, **When** I click the Help button, **Then** the help viewer opens to the "remove-operations" category with "remove-overview" topic displayed.
4. **Given** I am on the "Transfer" tab in MainForm, **When** I click the Help button, **Then** the help viewer opens to the "transfer-operations" category with "transfer-overview" topic displayed.
5. **Given** I am in the SettingsForm viewing any settings panel (About, Database, Part Numbers, Operations, Locations, Inventory Types, Users, Shortcuts, Theme), **When** I click the Help button, **Then** the help viewer opens to the "settings-management" category with the corresponding topic displayed. 
6. **Given** I am in the PrintForm, **When** I click the Help button, **Then** the help viewer opens to the "print-operations" category with "print-overview" topic displayed. 
7. **Given** I am in the Transactions viewer, **When** I click the Help button, **Then** the help viewer opens to the "transaction-history" category with "transaction-overview" topic displayed.
8. **Given** I am in the Visual Dashboard, **When** I click the Help button, **Then** the help viewer opens to the "infor-visual-integration" category with "visual-dashboard-overview" topic displayed.
9.  **Given** I am in the Analytics form, **When** I click the Help button, **Then** the help viewer opens to the "analytics-reporting" category with "analytics-overview" topic displayed.
10. **Given** the help viewer is already open from a previous help button click, **When** I click a Help button on a different form/tab, **Then** the existing help viewer window comes to front and navigates to the newly requested category/topic without opening a duplicate window.

---

### User Story 2 - Help Menu Integration (Priority: P1)

As a user, I want to access the help system from the main application menu bar so that I can easily find help documentation without needing to know keyboard shortcuts. 

**Why this priority**: Standard UX for Windows applications requires menu bar access.  This provides discoverability for users unfamiliar with keyboard shortcuts and establishes help as a first-class feature.

**Independent Test**: Can be tested by opening the Help menu and selecting options to verify they navigate to the correct content. No dependencies on other features.

**Acceptance Scenarios**:

1. **Given** the application is running, **When** I click the "Help" menu in the menu bar, **Then** I see menu items: "Help Index" (F1), "Getting Started" (Ctrl+F1), "Keyboard Shortcuts" (Ctrl+Shift+K), separator, and "About". 
2. **Given** the Help menu is open, **When** I click "Help Index", **Then** the help viewer opens to the index page showing all categories. 
3. **Given** the Help menu is open, **When** I click "Getting Started", **Then** the help viewer opens directly to the "Getting Started" category.
4. **Given** the Help menu is open, **When** I click "Keyboard Shortcuts", **Then** the help viewer opens directly to the "Keyboard Shortcuts" category. 

---

### User Story 3 - Singleton Help Viewer (Priority: P1)

As a user, I want the help viewer to behave as a single instance so that clicking multiple help buttons doesn't open multiple windows, making navigation cleaner. 

**Why this priority**: Critical for user experience. Multiple windows confuse users, consume system resources unnecessarily, and create cognitive overhead tracking which window has which content.

**Independent Test**: Can be tested by opening help, then clicking another help button, and verifying only one window exists and it navigates to the new topic. 

**Acceptance Scenarios**:

1. **Given** the help viewer is already open, **When** I click a Help button on any form, **Then** the existing window comes to front and navigates to the requested category without opening a duplicate window.
2.  **Given** the help viewer is minimized, **When** I click a Help button, **Then** the window is restored and brought to front with the new topic displayed. 

---

### User Story 4 - Contact Support System (Priority: P1)

As a user, I want to submit bug reports, improvement suggestions, report inconsistencies, and ask general questions directly from the Help Viewer so that I can communicate issues and ideas to the development team without leaving the application.

**Why this priority**: Direct user feedback is critical for continuous improvement and issue resolution. This feature eliminates barriers to reporting problems (no email, external forms, or separate tools required) and provides transparency into the development process.  Users can see their submissions are acknowledged and tracked, building trust and engagement.

**Independent Test**: Can be fully tested by accessing the Help Viewer, clicking "Contact Support", submitting each type of feedback (bug/suggestion/inconsistency/question), verifying database storage, and confirming submissions appear in both user status view and developer management form.

**Acceptance Scenarios**:

#### Bug Report Submission

1. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Report a Bug", **Then** I see a bug report form with required fields: Window/Form (dropdown), Active Section (dropdown), Bug Category (dropdown), Bug Severity (dropdown), Description (multi-line), Steps to Reproduce (multi-line), Expected Behavior (textbox), Actual Behavior (textbox). 
2. **Given** I am on the bug report form, **When** I select a window from the "Window/Form" dropdown, **Then** the dropdown displays user-friendly names (e.g., "Main Inventory Window" not "MainForm").
3. **Given** I have selected a window in the bug report form, **When** the "Active Section" dropdown populates, **Then** it shows only the user controls/sections relevant to that window.
4. **Given** I have filled out all required fields in the bug report form, **When** I click "Submit", **Then** the system stores the bug report in the database and displays a confirmation message with a unique tracking reference number (e.g., "BUG-2025-001234").
5. **Given** I submit a bug report with severity "Critical" or "High", **When** the submission is saved to the database, **Then** the system queues an email notification to designated developers. 
6. **Given** I attempt to submit a bug report with missing required fields, **When** I click "Submit", **Then** the form displays clear validation error messages and prevents submission.

#### Improvement Suggestion Submission

7. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Suggest an Improvement", **Then** I see a suggestion form with fields: Suggestion Category (dropdown), Related Window (optional dropdown), Priority (dropdown), Title (textbox), Description (multi-line), Business Justification (multi-line), Affected Users (dropdown). 
8. **Given** I have filled out all required fields in the suggestion form, **When** I click "Submit", **Then** the system stores the suggestion with auto-captured metadata and displays confirmation with tracking reference number (e.g., "SUG-2025-001234"). 

#### Report Inconsistency Submission

9. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Report an Inconsistency", **Then** I see an inconsistency form with fields: Inconsistency Type (dropdown), Window/Form (dropdown), Active Section (dropdown), Description (multi-line), Location 1 (textbox), Location 2 (textbox), Expected Consistency (textbox). 
10. **Given** I have filled out all required fields in the inconsistency form, **When** I click "Submit", **Then** the system stores the inconsistency report and displays confirmation with tracking number (e.g., "INC-2025-001234"). 

#### Ask General Question

11. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Ask a Question", **Then** I see a question form with fields: Question Category (dropdown), Related Window (optional dropdown), Priority (dropdown), Question (multi-line). 
12. **Given** I have filled out all required fields in the question form, **When** I click "Submit", **Then** the system stores the question and displays confirmation with tracking number (e.g., "QUE-2025-001234").

#### View Submission Status

13. **Given** I have previously submitted feedback, **When** I click "View My Submissions" from the Contact Support page, **Then** I see a filterable/sortable list showing all my submissions with columns: Type, Title/Summary, Status, Priority/Severity, Submitted Date, Last Updated Date.
14. **Given** I am viewing my submissions list, **When** I click a column header (Type, Status, Date), **Then** the list sorts by that column. 
15. **Given** I am viewing a submission with status "In Progress", **When** I click on the submission, **Then** I can add follow-up comments to provide additional context. 
16. **Given** I am viewing a submission, **When** the submission has developer notes, **Then** I can see the developer's comments/updates.

#### Developer Tools Form Access

17. **Given** I am logged in as a user with Admin or Developer role, **When** I navigate to the "Development" menu in MainForm, **Then** I see a "Developer Tools" menu item. 
18. **Given** I am logged in as a standard user (non-admin), **When** I access the "Development" menu, **Then** the "Developer Tools" option is hidden or disabled.
19. **Given** I am in the Developer Tools form, **When** the form loads, **Then** I see all user feedback submissions in a grid with filters for Status, Priority, User, Date Range, Category. 
20. **Given** I am viewing a submission in Developer Tools, **When** I click "Update Status", **Then** I can change the status from New → In Review → In Progress → Resolved → Closed. 
21. **Given** I am viewing a submission in Developer Tools, **When** I add developer notes, **Then** the notes are saved and visible to the original submitter in their "View My Submissions" page. 
22. **Given** I am viewing submissions in Developer Tools, **When** I click "Export to CSV", **Then** the system exports all filtered submissions to a CSV file.

---

### User Story 5 - External Template File Loading (Priority: P2)

As a help content maintainer, I want the HTML template to be loaded from an external file (not hard-coded) so that I can update the template design, styling, and structure without recompiling the application.

**Why this priority**: Enables rapid iteration on help content styling by non-developers. Current hard-coded templates require developer involvement for any visual changes, slowing down documentation improvements.

**Independent Test**: Can be tested by modifying the template file to change a CSS style (e.g., background color) and verifying the change appears in the help viewer without recompiling. 

**Acceptance Scenarios**:

1. **Given** the application is deployed, **When** I edit the external template file to change styling, **Then** the next time I open the help viewer, the new style is displayed.
2. **Given** the template file is missing or corrupt, **When** the help viewer tries to load, **Then** the system falls back to a minimal built-in template and logs an error.
3. **Given** I want to add a new CSS class, **When** I add it to the external template file, **Then** topics can use that class in their Content without code changes.

---


### User Story 6 - Component Template Support (Priority: P3)

As a help content author, I want to use reusable HTML components (alert boxes, code blocks) defined in separate template files so that the help content has consistent formatting and I can update component designs globally.

**Why this priority**: Nice-to-have feature that improves content consistency.  Lower priority than core functionality, but enables better documentation maintenance long-term.

**Independent Test**: Can be tested by using component placeholders in help content and verifying they render correctly with the component template styling.

**Acceptance Scenarios**:

1. **Given** a topic contains an alert placeholder, **When** the topic is rendered, **Then** it displays as a styled alert box using the component template.
2. **Given** a topic contains a code block placeholder, **When** the topic is rendered, **Then** it displays as a formatted code block. 
3. **Given** I update a component template to change the styling, **When** I reopen the help viewer, **Then** all topics using that component show the new styling.

---

### User Story 7 - Watermark/Logo Integration (Priority: P3)

As a system administrator, I want the help viewer to display the company logo as a background watermark so that the help documentation is visually consistent with the Release Notes viewer and reflects company branding.

**Why this priority**: Polish feature that enhances brand consistency. Not critical for functionality, but improves professional appearance and user confidence.

**Independent Test**: Can be tested by opening the help viewer and verifying a faint company logo appears in the background of help pages.

**Acceptance Scenarios**:

1. **Given** the help viewer is open, **When** I view any topic page, **Then** I see a faint watermark of the company logo in the background. Watermark Logo located here: `Resources\MTM.png`
2. **Given** the watermark image file is missing, **When** the help viewer loads, **Then** the page still renders correctly without the watermark (graceful degradation). 

---

### Edge Cases

- **Help viewer already open**: When a help button is clicked while the help viewer is already open, the system brings the existing window to front and navigates to the new topic, not open a second instance.
- **External template file invalid**: When an external template file contains invalid HTML or CSS, the system logs an error and falls back to a built-in minimal template.
- **Theme changes**: When the user's theme changes while the help viewer is open, the help viewer detects the theme change and re-renders with new colors. 
- **WebView2 runtime missing**: When the WebView2 runtime is not installed, the system displays a clear error message with download instructions instead of crashing. 
- **User submits duplicate bug reports**: System allows submission but Developer Tools provides duplicate detection/linking functionality to merge related issues. 
- **Required metadata cannot be captured**: System logs warning, stores NULL for failed metadata field, and allows submission to proceed with partial metadata. 
- **Database connection fails during submission**: System displays user-friendly error message, logs the failed attempt, and allows user to retry.
- **User attempts to comment on closed/resolved submission**: System allows the comment but displays notice that the issue is closed (developer can still review). 
- **Developer form accessed by non-admin user**: System performs role validation on form load and immediately closes form with access denied message, logging the unauthorized access attempt.
- **Very long description text** (>50,000 characters): Form validation displays character count warning at 45,000 chars and prevents submission beyond 50,000 chars.
- **User submits while offline**: HTML form in WebView2 detects offline state and displays message "You are currently offline. Please connect to the network to submit feedback."
- **Email notification fails for high-priority bug**: System logs email failure, queues for retry with exponential backoff (5 min, 10 min, 15 min), then logs permanent failure for manual administrator review. 
- **Tracking number generation collision**: System uses combination of FeedbackType prefix, year, and auto-incrementing sequence with uniqueness constraint.  On collision, retry with next sequence number. 

---

## Requirements *(mandatory)*

### Functional Requirements

**Context-Sensitive Help**:
- **FR-001**: System MUST provide context-sensitive help buttons on all major forms (Inventory, Remove, Transfer, Settings, Print, Transactions, Analytics, Logs, Error Reports, Release Notes, Quick Button Edit, Shortcut Edit, PO Details). Table with full context of this FR can be found here: `specs\FR-001-Table.md`
- **FR-002**: System MUST include a top-level "Help" menu in the main application menu bar with items: "Help Index" (F1), "Getting Started" (Ctrl+F1), "Keyboard Shortcuts" (Ctrl+Shift+K), and "About".
- **FR-003**: System MUST handle the case where the help viewer is already open when a help button is clicked by bringing the existing window to front and navigating to the requested category/topic (singleton pattern). 

**Template Management**:
- **FR-004**: System MUST load the base HTML template from an external file at runtime, not compile it into the executable.
- **FR-005**: System MUST fall back to a minimal built-in template if external template files are missing or corrupt, and log an error appropriately.
- **FR-006**: System SHOULD load component templates (alert box, code block) from external files and support placeholder replacement in help content. 

**Accessibility**:
- **FR-007**: System MUST include ARIA labels on all interactive HTML elements in the template for screen reader support.
- **FR-008**: System MUST ensure keyboard focus order is logical (sidebar → search → category cards → topic links) and Tab navigation works correctly within the viewer. 

**Visual Enhancements**:
- **FR-009**: System SHOULD support embedding a company logo watermark in the help viewer background. 
- **FR-010**: System MUST display a user-friendly error message with a download link if WebView2 runtime is not installed, instead of crashing or showing a generic exception. 
- **FR-011**: System MUST provide a "Recent Articles" section on the help home page showing the 5 most recently updated topics across all categories, ordered by LastUpdated date. 
- **FR-012**: System MUST support instant search filtering as users type in the search box, showing live suggestions before Enter is pressed (autocomplete/instant search). 
- **FR-013**: System MUST display category cards on the home page with: category icon, title, brief description, article count indicator, and click-to-navigate functionality. 

**Contact Support - Submission**:
- **FR-014**: System MUST provide a "Contact Support" section in the Help Viewer with four navigation options: "Report a Bug", "Suggest an Improvement", "Report an Inconsistency", "Ask a Question".
- **FR-015**: System MUST store all user submissions in database table `UserFeedback` with `FeedbackType` discriminator column ('Bug', 'Suggestion', 'Inconsistency', 'Question').
- **FR-016**: System MUST auto-capture and store metadata with every submission: `UserID`, `SubmissionDateTime`, `ApplicationVersion`, `OSVersion`, `MachineIdentifier`. 
- **FR-017**: System MUST use user-friendly display names for all windows/forms in dropdowns instead of code-based class names.
- **FR-018**: System MUST dynamically populate the "Active Section" dropdown based on the selected window, showing only relevant user controls/tabs.
- **FR-019**: System MUST validate all required fields before allowing submission and display clear, specific error messages for incomplete forms.
- **FR-020**: System MUST display a confirmation message immediately after successful submission, including a unique tracking reference number (format: `{PREFIX}-{YEAR}-{SEQUENCE}`, e.g., "BUG-2025-001234"). 

**Contact Support - User View**:
- **FR-021**: System MUST provide a "View My Submissions" page accessible from the Contact Support section, displaying all of the current user's previous submissions. 
- **FR-022**: System MUST allow users to filter submissions by Type, Status, Priority, and Date Range, and sort by any column header.
- **FR-023**: System MUST allow users to add follow-up comments to their existing submissions, creating a threaded conversation. 

**Contact Support - Developer Tools**:
- **FR-024**: System MUST create a new Developer Tools form accessible via the "Development" menu item in MainForm, allowing administrators to view all user feedback, filter/sort, update status, add notes, assign to developers, mark duplicates, and export to CSV. 
- **FR-025**: System MUST restrict access to the Developer Tools form to users with `Admin` or `Developer` role only, hiding or disabling the menu option for standard users.
- **FR-026**: System MUST send automated email notifications to designated developers when bug reports with Severity "Critical" or "High" are submitted. Email recipient configuration stored in database Settings table with per-category routing (e.g., Visual Integration bugs → Visual team). Configuration managed via Developer Tools admin UI for runtime changes without app restarts.
- **FR-027**: System MUST support single developer assignment per submission only. No auto-assignment rules. Clear ownership with one developer responsible per issue. Manual reassignment supported if assigned developer becomes unavailable. Assignment validation ensures AssignedDeveloperID has active Developer role.
- **FR-028**: System MUST support manual duplicate detection only (no auto-suggestion). Developers identify and mark duplicates during review in Developer Tools using `IsDuplicate` flag and `DuplicateOfFeedbackID` foreign key relationship. Both primary and duplicate submissions remain visible with clear relationship indication.

**Contact Support - Data Management**:
- **FR-029**: System MUST handle "Other" category selections by displaying an additional required text input field for users to specify their custom category name.
- **FR-030**: System MUST gracefully handle database connection failures during submission by displaying user-friendly error message and logging the failed attempt.
- **FR-031**: System MUST prevent injection attacks by validating and sanitizing all text inputs before storing in database.
- **FR-032**: System MUST store all window/form and user control mappings in configuration tables (`WindowFormMapping`, `UserControlMapping`) that can be updated without code changes. Initial seed data deployed via SQL migration scripts (version controlled). Ongoing management via new tab in existing `Form_DatabaseMaintenance` with add/edit/deactivate functionality and validation to ensure mapped forms/controls exist in codebase.
- **FR-033**: System MUST track Last Updated timestamp on submissions, updating it whenever status changes, developer notes are added, or follow-up comments are posted.
- **FR-034**: System MUST implement tracking number generation with annual reset using `TrackingNumberSequence` table (columns: FeedbackType, Year, NextNumber). Format: `{PREFIX}-{YEAR}-{SEQUENCE}` where PREFIX is BUG/SUG/INC/QUE, YEAR is 4-digit current year, SEQUENCE is 6-digit zero-padded auto-increment per type per year (e.g., BUG-2025-000001 → BUG-2026-000001). Uniqueness enforced by database UNIQUE constraint. Collision handling: retry with incremented sequence up to 3 attempts, then log fatal error.

**Contact Support - Architecture & Error Handling** (MTM Constitution Compliance):
- **FR-035**: System MUST use `Service_ErrorHandler` for all error handling and user notifications. `MessageBox.Show` is FORBIDDEN except for success confirmations. All exceptions must be handled via `Service_ErrorHandler.HandleException()` with appropriate severity levels (Low/Medium/High/Critical). Database errors must use `Service_ErrorHandler.HandleDatabaseError()`. Form validation errors must use `Service_ErrorHandler.HandleValidationError()` with field name, caller name, and control name context.

- **FR-036**: System MUST log all user feedback submissions using `LoggingUtility.Log()` with context including FeedbackType, UserID, TrackingNumber, and submission timestamp.

- **FR-037**: System MUST log all status changes in Developer Tools using `LoggingUtility.Log()` with context including FeedbackID, OldStatus, NewStatus, AssignedDeveloperID, and timestamp.

- **FR-038**: System MUST log database errors during feedback submission using `LoggingUtility.LogDatabaseError()` with stored procedure name and error details.

- **FR-039**: System MUST log all email notification attempts (success/failure) using `LoggingUtility.Log()` with recipient list, FeedbackID, and delivery status.

- **FR-040**: System MUST validate all user input using `Service_Validation` before database submission:
  - Text length validation (max 50,000 chars for Description fields)
  - Required field validation with clear error messages via `Service_ErrorHandler.HandleValidationError()`
  - SQL injection prevention via parameterized queries (handled by `Helper_Database_StoredProcedure`)
  - XSS prevention for WebView2-rendered content using HtmlSanitizer library or equivalent
  - Dropdown selection validation (no invalid/outdated options)
  - Email format validation for notification recipient configuration

- **FR-041**: Contact Support forms in WebView2 MUST use secure JavaScript bridge (`window.chrome.webview.postMessage`) to submit data to WinForms backend. NO direct AJAX calls to external endpoints allowed.

- **FR-042**: WebView2 content MUST be loaded from local template files only. NO remote content loading allowed. Content Security Policy (CSP) headers must be enforced to prevent XSS attacks.

- **FR-043**: Form submission data MUST be validated on WinForms side before database storage, treating WebView2 as untrusted input source. All validation rules from FR-040 apply after data crosses JavaScript bridge.

- **FR-044**: System MUST properly dispose of all IDisposable resources (DataTables, database connections, file streams) using `using` statements or try-finally blocks to prevent resource leaks.

- **FR-045**: All public methods, properties, and classes MUST include XML documentation comments with `<summary>`, `<param>`, `<returns>`, and `<exception>` tags as applicable.

- **FR-046**: All reference types that can be null MUST be annotated with nullable reference type syntax (`string?`, `DataTable?`) to enable compile-time null safety checks.

- **FR-047**: System MUST handle role changes during active sessions. If a user's role changes from Developer to standard user while Developer Tools form is open, the form must detect the role change on next action, display "Your permissions have changed. Please close this window." via `Service_ErrorHandler.ShowUserError()`, and disable all controls.

### Database Schema

#### Table: UserFeedback

```sql
CREATE TABLE UserFeedback (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    FeedbackType NVARCHAR(50) NOT NULL, -- 'Bug', 'Suggestion', 'Inconsistency', 'Question'
    TrackingNumber NVARCHAR(50) NOT NULL,
    
    -- User & Timestamp Info
    UserID INT NOT NULL,
    SubmissionDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastUpdatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    -- Auto-Captured Metadata
    ApplicationVersion NVARCHAR(50),
    OSVersion NVARCHAR(100),
    MachineIdentifier NVARCHAR(255),
    
    -- Context Information
    WindowForm NVARCHAR(255), -- User-friendly window name
    ActiveSection NVARCHAR(255), -- User-friendly section name
    
    -- Categorization
    Category NVARCHAR(255) NOT NULL,
    CustomCategory NVARCHAR(500), -- Used when Category = 'Other'
    Severity NVARCHAR(50), -- For bugs: Critical, High, Medium, Low
    Priority NVARCHAR(50), -- For suggestions/questions: High, Medium, Low, Urgent
    
    -- Content Fields
    Title NVARCHAR(500),
    Description NVARCHAR(MAX) NOT NULL,
    
    -- Bug-Specific Fields
    StepsToReproduce NVARCHAR(MAX),
    ExpectedBehavior NVARCHAR(MAX),
    ActualBehavior NVARCHAR(MAX),
    
    -- Suggestion-Specific Fields
    BusinessJustification NVARCHAR(MAX),
    AffectedUsers NVARCHAR(100), -- 'Just Me', 'My Team', 'All Users', 'Specific Role'
    
    -- Inconsistency-Specific Fields
    Location1 NVARCHAR(500),
    Location2 NVARCHAR(500),
    ExpectedConsistency NVARCHAR(MAX),
    
    -- Status Management
    Status NVARCHAR(50) NOT NULL DEFAULT 'New', -- 'New', 'In Review', 'In Progress', 'Resolved', 'Closed', 'Won''t Fix'
    AssignedToDeveloperID INT NULL,
    DeveloperNotes NVARCHAR(MAX),
    ResolutionDateTime DATETIME2 NULL,
    
    -- Relationship Tracking
    IsDuplicate BIT DEFAULT 0,
    DuplicateOfFeedbackID INT NULL,
    
    CONSTRAINT UQ_UserFeedback_TrackingNumber UNIQUE (TrackingNumber),
    CONSTRAINT FK_UserFeedback_User FOREIGN KEY (UserID) REFERENCES Users(UserID),
    CONSTRAINT FK_UserFeedback_Developer FOREIGN KEY (AssignedToDeveloperID) REFERENCES Users(UserID),
    CONSTRAINT FK_UserFeedback_Duplicate FOREIGN KEY (DuplicateOfFeedbackID) REFERENCES UserFeedback(FeedbackID)
);

CREATE INDEX IX_UserFeedback_UserID ON UserFeedback(UserID);
CREATE INDEX IX_UserFeedback_Status ON UserFeedback(Status);
CREATE INDEX IX_UserFeedback_FeedbackType ON UserFeedback(FeedbackType);
CREATE INDEX IX_UserFeedback_SubmissionDateTime ON UserFeedback(SubmissionDateTime DESC);
CREATE INDEX IX_UserFeedback_AssignedToDeveloperID ON UserFeedback(AssignedToDeveloperID) WHERE AssignedToDeveloperID IS NOT NULL;
```

#### Table: UserFeedbackComments

```sql
CREATE TABLE UserFeedbackComments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    FeedbackID INT NOT NULL,
    UserID INT NOT NULL,
    CommentDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    CommentText NVARCHAR(MAX) NOT NULL,
    IsInternalNote BIT DEFAULT 0, -- TRUE if from developer, FALSE if from submitter
    
    CONSTRAINT FK_UserFeedbackComments_Feedback FOREIGN KEY (FeedbackID) REFERENCES UserFeedback(FeedbackID) ON DELETE CASCADE,
    CONSTRAINT FK_UserFeedbackComments_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE INDEX IX_UserFeedbackComments_FeedbackID ON UserFeedbackComments(FeedbackID);
CREATE INDEX IX_UserFeedbackComments_CommentDateTime ON UserFeedbackComments(CommentDateTime DESC);
```

#### Table: WindowFormMapping

```sql
CREATE TABLE WindowFormMapping (
    MappingID INT IDENTITY(1,1) PRIMARY KEY,
    CodebaseName NVARCHAR(255) NOT NULL, -- e.g., 'MainForm', 'SettingsForm', 'Form_InforVisualDashboard'
    UserFriendlyName NVARCHAR(255) NOT NULL, -- e.g., 'Main Inventory Window', 'Settings Window'
    IsActive BIT DEFAULT 1,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT UQ_WindowFormMapping_CodebaseName UNIQUE (CodebaseName)
);

CREATE INDEX IX_WindowFormMapping_IsActive ON WindowFormMapping(IsActive) WHERE IsActive = 1;
```

#### Table: UserControlMapping

```sql
CREATE TABLE UserControlMapping (
    MappingID INT IDENTITY(1,1) PRIMARY KEY,
    WindowFormMappingID INT NOT NULL,
    CodebaseName NVARCHAR(255) NOT NULL, -- e.g., 'Control_InventoryTab', 'Control_TransferTab'
    UserFriendlyName NVARCHAR(255) NOT NULL, -- e.g., 'Inventory Tab', 'Transfer Operations'
    IsActive BIT DEFAULT 1,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT FK_UserControlMapping_WindowForm FOREIGN KEY (WindowFormMappingID) REFERENCES WindowFormMapping(MappingID) ON DELETE CASCADE,
    CONSTRAINT UQ_UserControlMapping_WindowControl UNIQUE (WindowFormMappingID, CodebaseName)
);

CREATE INDEX IX_UserControlMapping_WindowFormMappingID ON UserControlMapping(WindowFormMappingID);
CREATE INDEX IX_UserControlMapping_IsActive ON UserControlMapping(IsActive) WHERE IsActive = 1;
```

#### Table: TrackingNumberSequence

```sql
CREATE TABLE TrackingNumberSequence (
    SequenceID INT IDENTITY(1,1) PRIMARY KEY,
    FeedbackType NVARCHAR(50) NOT NULL, -- 'Bug', 'Suggestion', 'Inconsistency', 'Question'
    Year INT NOT NULL,
    NextNumber INT NOT NULL DEFAULT 1,
    LastGeneratedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT UQ_TrackingNumberSequence_Type_Year UNIQUE (FeedbackType, Year)
);
```

#### Table: EmailNotificationConfig (New)

```sql
CREATE TABLE EmailNotificationConfig (
    ConfigID INT IDENTITY(1,1) PRIMARY KEY,
    FeedbackCategory NVARCHAR(255) NOT NULL, -- e.g., 'Integration Issue (Infor Visual)', 'Database Error', 'All'
    RecipientEmails NVARCHAR(MAX) NOT NULL, -- Semicolon-separated email list
    IsActive BIT DEFAULT 1,
    CreatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastModifiedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT UQ_EmailNotificationConfig_Category UNIQUE (FeedbackCategory)
);

CREATE INDEX IX_EmailNotificationConfig_IsActive ON EmailNotificationConfig(IsActive) WHERE IsActive = 1;
```

### Stored Procedures

All database access MUST use stored procedures via `Helper_Database_StoredProcedure`. Required stored procedures with signatures:

#### UserFeedback Operations

```sql
-- Insert new feedback submission
CREATE PROCEDURE md_feedback_Insert
    @FeedbackType NVARCHAR(50),
    @UserID INT,
    @WindowForm NVARCHAR(255),
    @ActiveSection NVARCHAR(255),
    @Category NVARCHAR(255),
    @CustomCategory NVARCHAR(500),
    @Severity NVARCHAR(50),
    @Priority NVARCHAR(50),
    @Title NVARCHAR(500),
    @Description NVARCHAR(MAX),
    @StepsToReproduce NVARCHAR(MAX),
    @ExpectedBehavior NVARCHAR(MAX),
    @ActualBehavior NVARCHAR(MAX),
    @BusinessJustification NVARCHAR(MAX),
    @AffectedUsers NVARCHAR(100),
    @Location1 NVARCHAR(500),
    @Location2 NVARCHAR(500),
    @ExpectedConsistency NVARCHAR(MAX),
    @ApplicationVersion NVARCHAR(50),
    @OSVersion NVARCHAR(100),
    @MachineIdentifier NVARCHAR(255),
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT,
    @p_FeedbackID INT OUTPUT,
    @p_TrackingNumber NVARCHAR(50) OUTPUT
AS BEGIN
    -- Implementation: Generate tracking number, insert record, return ID and tracking number
END;

-- Get all feedback with optional filtering
CREATE PROCEDURE md_feedback_GetAll
    @FilterStatus NVARCHAR(50) = NULL,
    @FilterFeedbackType NVARCHAR(50) = NULL,
    @FilterUserID INT = NULL,
    @FilterDateFrom DATETIME2 = NULL,
    @FilterDateTo DATETIME2 = NULL,
    @FilterAssignedDeveloperID INT = NULL,
    @FilterCategory NVARCHAR(255) = NULL,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return filtered feedback with user info joined
END;

-- Get feedback by UserID
CREATE PROCEDURE md_feedback_GetByUser
    @UserID INT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return all feedback for specific user
END;

-- Get single feedback by ID with full details
CREATE PROCEDURE md_feedback_GetById
    @FeedbackID INT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return single feedback record with user info
END;

-- Update feedback status and assignment
CREATE PROCEDURE md_feedback_UpdateStatus
    @FeedbackID INT,
    @NewStatus NVARCHAR(50),
    @AssignedToDeveloperID INT = NULL,
    @DeveloperNotes NVARCHAR(MAX) = NULL,
    @ModifiedByUserID INT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Update status, assignment, notes, LastUpdatedDateTime
    -- Validate AssignedToDeveloperID has Developer role if not NULL
END;

-- Mark feedback as duplicate
CREATE PROCEDURE md_feedback_MarkDuplicate
    @FeedbackID INT,
    @DuplicateOfFeedbackID INT,
    @ModifiedByUserID INT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Set IsDuplicate=1, link to primary, update LastUpdatedDateTime
END;

-- Export filtered feedback to CSV format
CREATE PROCEDURE md_feedback_ExportToCsv
    @FilterStatus NVARCHAR(50) = NULL,
    @FilterFeedbackType NVARCHAR(50) = NULL,
    @FilterUserID INT = NULL,
    @FilterDateFrom DATETIME2 = NULL,
    @FilterDateTo DATETIME2 = NULL,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return all fields formatted for CSV export
END;
```

#### UserFeedbackComments Operations

```sql
-- Add comment to feedback
CREATE PROCEDURE md_feedback_comment_Insert
    @FeedbackID INT,
    @UserID INT,
    @CommentText NVARCHAR(MAX),
    @IsInternalNote BIT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT,
    @p_CommentID INT OUTPUT
AS BEGIN
    -- Implementation: Insert comment, update parent LastUpdatedDateTime
END;

-- Get all comments for feedback
CREATE PROCEDURE md_feedback_comment_GetByFeedbackId
    @FeedbackID INT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return comments ordered by CommentDateTime, join user info
END;
```

#### WindowFormMapping Operations

```sql
-- Get all active window/form mappings
CREATE PROCEDURE sys_windowform_mapping_GetAll
    @IncludeInactive BIT = 0,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return active mappings, optionally include inactive
END;

-- Get user control mappings for specific window
CREATE PROCEDURE sys_usercontrol_mapping_GetByWindow
    @WindowFormMappingID INT,
    @IncludeInactive BIT = 0,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Return user controls for window, optionally include inactive
END;

-- Insert or update window/form mapping
CREATE PROCEDURE sys_windowform_mapping_Upsert
    @CodebaseName NVARCHAR(255),
    @UserFriendlyName NVARCHAR(255),
    @IsActive BIT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT,
    @p_MappingID INT OUTPUT
AS BEGIN
    -- Implementation: Insert if not exists, update if exists
END;

-- Insert or update user control mapping
CREATE PROCEDURE sys_usercontrol_mapping_Upsert
    @WindowFormMappingID INT,
    @CodebaseName NVARCHAR(255),
    @UserFriendlyName NVARCHAR(255),
    @IsActive BIT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT,
    @p_MappingID INT OUTPUT
AS BEGIN
    -- Implementation: Insert if not exists, update if exists
END;
```

#### TrackingNumberSequence Operations

```sql
-- Get next tracking number for feedback type and year
CREATE PROCEDURE sys_tracking_number_GetNext
    @FeedbackType NVARCHAR(50),
    @Year INT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT,
    @p_TrackingNumber NVARCHAR(50) OUTPUT
AS BEGIN
    -- Implementation: 
    -- 1. Get or create sequence record for FeedbackType + Year
    -- 2. Increment NextNumber atomically (use transaction + row lock)
    -- 3. Format as {PREFIX}-{YEAR}-{SEQUENCE} (e.g., BUG-2025-001234)
    -- 4. Return formatted tracking number
    -- 5. Handle concurrency with retry logic
END;
```

#### EmailNotificationConfig Operations

```sql
-- Get email recipients for category
CREATE PROCEDURE sys_email_notification_GetRecipients
    @FeedbackCategory NVARCHAR(255),
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT,
    @p_RecipientEmails NVARCHAR(MAX) OUTPUT
AS BEGIN
    -- Implementation: Return recipient emails for category, fall back to 'All' if not found
END;

-- Upsert email notification config
CREATE PROCEDURE sys_email_notification_Upsert
    @FeedbackCategory NVARCHAR(255),
    @RecipientEmails NVARCHAR(MAX),
    @IsActive BIT,
    @p_Status INT OUTPUT,
    @p_ErrorMsg NVARCHAR(500) OUTPUT
AS BEGIN
    -- Implementation: Insert or update config for category
END;
```

### Key Entities

*Architecture Notes: Follow existing MTM WIP Application patterns.  All forms MUST inherit from `ThemedForm`.  All data access MUST return `Model_Dao_Result<T>`.  All database operations MUST be async.*

**Forms**:
- **HelpViewerForm**: Main help display form (enhanced with search, breadcrumbs, Contact Support integration).  Inherits `ThemedForm`.
- **Form_DeveloperTools**: Administrator form for managing user feedback.  Inherits `ThemedForm`.  Access restricted to Admin/Developer roles. 
- **Form_DatabaseMaintenance** (EXISTING - ENHANCED): Extends existing database maintenance form with new "Form Mappings" tab for managing WindowFormMapping and UserControlMapping tables.  Provides UI for add/edit/deactivate operations with validation to ensure mapped forms/controls exist in codebase.  Also hosts email notification configuration UI with per-category recipient management. 

**Services**:
- **Service_HelpTemplateEngine**: Generates HTML from templates and content (enhanced with component templates). 
- **Service_HelpSearchEngine**: Provides instant search, autocomplete, and relevance scoring.
- **Service_FeedbackManager**: Business logic for feedback operations (submission workflow, status transitions, email triggers, validation coordination).  Calls `Dao_UserFeedback` for all database access.

**Data Access**:
- **Dao_UserFeedback**: Data access object for UserFeedback and UserFeedbackComments tables.
  - All methods return `Model_Dao_Result<T>`
  - All database access via `Helper_Database_StoredProcedure`
  - All operations are async
  - Methods: `GetAllAsync()`, `GetByUserIdAsync()`, `GetByIdAsync()`, `InsertAsync()`, `UpdateStatusAsync()`, `MarkAsDuplicateAsync()`, `AddCommentAsync()`, `GetCommentsAsync()`, `ExportToCsvAsync()`

**Models**:
- **Model_UserFeedback**: Entity model for feedback submissions.
- **Model_UserFeedbackComment**: Entity model for follow-up comments.
- **Model_WindowFormMapping**: Configuration entity mapping code-based form names to user-friendly display names.
- **Model_UserControlMapping**: Configuration entity mapping user controls to parent forms.
- **Model_HelpTopic**: Enhanced with ArticleType, ReadTimeMinutes, RelatedTopicIds properties.
- **Model_HelpSearchSuggestion**: Model for autocomplete suggestions with preview text. 

**Templates** (External HTML/CSS files in `Documentation/Help/Templates/`):
- `help-base-template.html` - Main page template
- `help-home-template.html` - Home page with category cards and recent articles
- `help-contact-support-page.html` - Contact Support landing page
- `help-bug-report-form.html` - Bug report submission form
- `help-suggestion-form.html` - Improvement suggestion form
- `help-inconsistency-form.html` - Inconsistency report form
- `help-question-form.html` - Question form
- `help-view-submissions. html` - User submission history page
- Component templates: `help-category-card-component.html`, `help-search-component.html`, `help-breadcrumb-component.html`, `help-recent-articles-component.html`

---

## Success Criteria *(mandatory)*

### Measurable Outcomes

**Context-Sensitive Help**:
- **SC-001**: 100% of major application forms/tabs have a visible Help button that navigates to the correct category. 
- **SC-002**: Help Index is accessible via menu bar within 2 clicks from any screen.
- **SC-003**: Help viewer supports single-instance pattern (clicking help button when viewer is open does not create a new window). 

**Template & Accessibility**:
- **SC-004**: Template changes (CSS/HTML) can be deployed without application recompilation (update file only, restart viewer).
- **SC-005**: Help viewer passes basic accessibility audit (no critical errors for keyboard navigation and screen reader support).
- **SC-006**: WebView2 runtime errors display user-friendly message with download link in 100% of cases. 

**Search & Navigation**:
- **SC-007**: Search returns relevant results in < 500ms for typical queries (< 100 total topics).
- **SC-008**: Autocomplete suggestions appear in < 200ms as user types. 
- **SC-009**: Recent Articles section updates correctly when topics are modified (based on LastUpdated field). 
- **SC-010**: Breadcrumb navigation accurately reflects current location and all segments are clickable.

**Contact Support - Submission**:
- **SC-011**: Users can submit feedback with all required fields populated and receive confirmation with tracking number within 2 seconds of clicking Submit.
- **SC-012**: Window/Form and Active Section dropdowns display 100% user-friendly names with 0% code-based class names visible to users.
- **SC-013**: Contact Support forms validate all required fields with clear error messages before allowing submission. 
- **SC-014**: System successfully handles and stores descriptions with up to 50,000 characters without truncation or data loss.

**Contact Support - Notifications & Developer Tools**:
- **SC-015**: High-priority (High/Critical severity) bug submissions trigger email notification queue within 1 minute of submission.
- **SC-016**: Developer Tools form displays all submissions with real-time filtering (by status, priority, user, category, date range) and sorting (by any column) with results appearing in < 1 second.
- **SC-017**: Developer Tools form allows administrators to export filtered submissions to CSV format in < 3 seconds for datasets up to 10,000 records. 

**Contact Support - User Experience**:
- **SC-018**: Users can view the current status of all their submissions, see developer notes/updates, and add follow-up comments. 
- **SC-019**: Duplicate submission detection and linking functionality correctly maintains relationships, with linked submissions visible in both user and developer views.
- **SC-020**: Follow-up comment system supports threaded conversations with proper timestamp ordering and author attribution, displaying internal developer notes distinctly from user comments.

---

## Assumptions

- The existing `HelpViewerForm` and `Service_HelpTemplateEngine` architecture will be extended, not replaced.
- External template files will be deployed alongside the application in the `Documentation/Help/Templates/` directory.
- The WebView2 runtime is expected to be pre-installed on most target machines (Windows 10/11), but graceful degradation is required for machines without it.
- JSON help content files are maintained by administrators, not end users.
- Search functionality operates on client-side (in-memory) data for performance, not server-side indexing. 
- Recent Articles are determined by the `LastUpdated` field in JSON, not by actual view/access statistics.
- Mobile responsiveness refers to the HTML rendering within WebView2, not native mobile app support.
- Article read time estimation uses a standard 200 words per minute reading speed.
- Email notifications use existing SMTP infrastructure and are queued asynchronously with retry logic.
- Database operations follow existing MTM WIP Application patterns: all async, all return `Model_Dao_Result<T>`, all use stored procedures via `Helper_Database_StoredProcedure`.
- **Window/Form mappings**: Initial seed data deployed via version-controlled SQL migration scripts alongside code changes. Ongoing updates managed via new "Form Mappings" tab in existing `Form_DatabaseMaintenance`.
- **Email notification recipients**: Configured per-category via database Settings table, managed through admin UI in Developer Tools/Database Maintenance for runtime changes without app restarts.
- **Tracking number sequences**: Reset annually (BUG-2025-000001 → BUG-2026-000001) using `TrackingNumberSequence` table. MySQL 5.7.24 compatible (no SEQUENCE objects).
- **Developer assignment**: Single assignment per submission only. No auto-assignment rules. Manual reassignment supported with validation of Developer role.
- **Duplicate detection**: Manual identification only by developers in Developer Tools. No automated similarity matching to avoid false positives. 

---

## Dependencies

- **Depends on** `016-new-help-system` feature being complete (base help viewer functionality). 
- **Depends on** WebView2 runtime being available or error handling being in place.
- **Depends on** Help content JSON files existing for each category referenced by help buttons. 
- **Depends on** Existing MTM WIP Application infrastructure:
  - `ThemedForm` base class for consistent UI
  - `Service_ErrorHandler` for all error handling
  - `LoggingUtility` for all logging operations
  - `Helper_Database_StoredProcedure` for all database access
  - `Model_Dao_Result<T>` pattern for all DAO return types
  - Existing User/Role tables in database for authentication/authorization
  - Existing SMTP infrastructure for email notifications