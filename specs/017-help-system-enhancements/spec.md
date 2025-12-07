# Feature Specification: Help System Enhancements

**Feature Branch**: `017-help-system-enhancements`  
**Created**: 2025-12-07  
**Status**: Draft  
**Parent Feature**: `016-new-help-system`  
**Input**: Analysis of NEW_HELP_SYSTEM_IMPLEMENTATION_PROMPT.md against current implementation

## Overview

This specification details enhancements to the help system including context-sensitive help buttons on all major forms, help menu integration, singleton help viewer pattern, external template loading, accessibility support, and a comprehensive user feedback/contact support system with developer tools for issue tracking and management.

## Goals

1. **Improve User Productivity**: Enable context-sensitive help access from every major application screen without manual navigation.
2. **Ensure Accessibility**: Make help system usable via keyboard and screen readers.
3. **Enable Template Customization**: Allow non-developers to update help styling without code recompilation.
4. **Gather User Feedback**: Implement structured feedback collection for bugs, suggestions, inconsistencies, and questions with developer management tools.
5. **Support User Engagement**: Provide status tracking for submissions and threaded communication between users and developers.


## User Scenarios & Testing *(mandatory)*

### User Story 1 - Context-Sensitive Help Buttons on All Forms (Priority: P1)

As a user, I want to click a help button on any screen (Inventory, Remove, Transfer, Settings, etc.) and be taken directly to the relevant help topic so that I don't have to browse or search for context-specific information.

**Why this priority**: The original spec emphasizes context-sensitive help as a core feature. Currently only one tab has a help button. This is the most impactful feature for user productivity as it eliminates manual searching.

**Independent Test**: Can be fully tested by clicking help buttons on each major form/tab and verifying the correct category opens with the first topic displayed.


**Acceptance Scenarios**:

1. **Given** I am on the "Inventory" tab in MainForm, **When** I click the Help button (displaying "Inventory"), **Then** the help viewer opens to the "inventory-operations" category with "inventory-overview" topic displayed.
2. **Given** I am on the "Advanced Inventory" tab in MainForm, **When** I click the Help button (displaying "Advanced Inventory"), **Then** the help viewer opens to the "advanced-inventory-operations" category with "advanced-inventory-overview" topic displayed.
3. **Given** I am on the "Remove" tab in MainForm, **When** I click the Help button (displaying "Remove"), **Then** the help viewer opens to the "remove-operations" category with "remove-overview" topic displayed.
4. **Given** I am on the "Advanced Removal" tab in MainForm, **When** I click the Help button (displaying "Advanced Removal"), **Then** the help viewer opens to the "advanced-remove-operations" category with "advanced-remove-overview" topic displayed.
5. **Given** I am on the "Transfer" tab in MainForm, **When** I click the Help button (displaying "Transfer"), **Then** the help viewer opens to the "transfer-operations" category with "transfer-overview" topic displayed.
6. **Given** I am in the SettingsForm viewing the Home panel, **When** I click the Help button (displaying "Settings Home"), **Then** the help viewer opens to the "settings-management" category with "home" topic displayed.
7. **Given** I am in the SettingsForm viewing any settings panel (About, Database, Part Numbers, Operations, Locations, Inventory Types, Users, Shortcuts, or Theme), **When** I click the Help button, **Then** the help viewer opens to the "settings-management" category with the corresponding topic (e.g., "about", "database-config", "part-number-overview") displayed.
8. **Given** I am in the PrintForm, **When** I click the Help button, **Then** the help viewer opens to the "print-operations" category with "print-overview" topic displayed.
9. **Given** I am in the Transactions viewer, **When** I click the Help button, **Then** the help viewer opens to the "transaction-history" category with "transaction-overview" topic displayed.
10. **Given** I am in the TransactionLifecycleForm, **When** I click the Help button, **Then** the help viewer opens to the "transaction-history" category with "lifecycle" topic displayed.
11. **Given** I am in the Visual Dashboard, **When** I click the Help button, **Then** the help viewer opens to the "infor-visual-integration" category with "visual-dashboard-overview" topic displayed.
12. **Given** I am in the Analytics form, **When** I click the Help button, **Then** the help viewer opens to the "analytics-reporting" category with "analytics-overview" topic displayed.
13. **Given** I am in the Analytics Viewer form, **When** I click the Help button, **Then** the help viewer opens to the "analytics-reporting" category with "analytics-data" topic displayed.
14. **Given** I am in the Logs Viewer form, **When** I click the Help button, **Then** the help viewer opens to the "admin-tools" category with "logs" topic displayed.
15. **Given** I am in the Error Reports form, **When** I click the Help button, **Then** the help viewer opens to the "admin-tools" category with "error-reports" topic displayed.
16. **Given** I am in the Release Notes form, **When** I click the Help button, **Then** the help viewer opens to the "getting-started" category with "release-notes" topic displayed.
17. **Given** I am in the Quick Button Edit dialog, **When** I click the Help button, **Then** the help viewer opens to the "settings-management" category with "quick-buttons" topic displayed.
18. **Given** I am in the Shortcut Edit dialog, **When** I click the Help button, **Then** the help viewer opens to the "settings-management" category with "shortcuts" topic displayed.
19. **Given** I am in the PO Details dialog, **When** I click the Help button, **Then** the help viewer opens to the "infor-visual-integration" category with "po-details" topic displayed.
20. **Given** the help viewer is already open from a previous help button click, **When** I click a Help button on a different form/tab, **Then** the existing help viewer window comes to front and navigates to the newly requested category/topic without opening a duplicate window.


---

### User Story 2 - Help Menu Integration (Priority: P1)

As a user, I want to access the help system from the main application menu bar so that I can easily find help documentation without needing to know keyboard shortcuts.

**Why this priority**: Standard UX for Windows applications requires menu bar access. This provides discoverability for users unfamiliar with keyboard shortcuts.

**Independent Test**: Can be tested by opening the Help menu and selecting options to verify they navigate to the correct content.

**Acceptance Scenarios**:

1. **Given** the application is running, **When** I click the "Help" menu in the menu bar, **Then** I see menu items: "Help Index", "Getting Started", "Keyboard Shortcuts", and "About".
2. **Given** the Help menu is open, **When** I click "Help Index", **Then** the help viewer opens to the index page showing all categories.
3. **Given** the Help menu is open, **When** I click "Getting Started", **Then** the help viewer opens directly to the "Getting Started" category.
4. **Given** the Help menu is open, **When** I click "Keyboard Shortcuts", **Then** the help viewer opens directly to the "Keyboard Shortcuts" category.

---

### User Story 3 - Singleton Help Viewer (Priority: P1)

As a user, I want the help viewer to behave as a single instance so that clicking multiple help buttons doesn't open multiple windows, making navigation cleaner.

**Why this priority**: Critical for user experience—multiple windows confuse users and consume system resources unnecessarily.

**Independent Test**: Can be tested by opening help, then clicking another help button, and verifying only one window exists and it navigates to the new topic.

**Acceptance Scenarios**:

1. **Given** the help viewer is already open, **When** I click a Help button on any form, **Then** the existing window comes to front and navigates to the requested category.
2. **Given** the help viewer is minimized, **When** I click a Help button, **Then** the window is restored and brought to front with the new topic displayed.

---

### User Story 4 - External Template File Loading (Priority: P2)

As a help content maintainer, I want the HTML template to be loaded from an external file (not hard-coded) so that I can update the template design, styling, and structure without recompiling the application.

**Why this priority**: Enables rapid iteration on help content styling by non-developers. Current hard-coded templates require developer involvement for any visual changes.

**Independent Test**: Can be tested by modifying the template file to change a CSS style (e.g., background color) and verifying the change appears in the help viewer without recompiling.

**Acceptance Scenarios**:

1. **Given** the application is deployed, **When** I edit the external template file to change the header background color, **Then** the next time I open the help viewer, the new color is displayed.
2. **Given** the template file is missing or corrupt, **When** the help viewer tries to load, **Then** the system falls back to a minimal built-in template and logs an error.
3. **Given** I want to add a new CSS class, **When** I add it to the external template file, **Then** topics can use that class in their Content without code changes.

---

### User Story 5 - ARIA Labels and Accessibility (Priority: P2)

As a user with accessibility needs, I want the help viewer to support keyboard navigation and screen readers so that I can access help documentation effectively.

**Why this priority**: Accessibility compliance is important for inclusive design and may be required for certain deployments.

**Independent Test**: Can be tested using screen reader software and keyboard-only navigation to verify all interactive elements are accessible.

**Acceptance Scenarios**:

1. **Given** I am using a screen reader, **When** the help viewer opens, **Then** the sidebar navigation items are announced correctly with role and state information.
2. **Given** I am navigating via keyboard only, **When** I press Tab repeatedly, **Then** I can navigate through all clickable elements (category cards, topic links, search box) in a logical order.
3. **Given** I am on a topic page, **When** I use arrow keys, **Then** I can navigate through the content smoothly.

---

### User Story 6 - Component Template Support (Priority: P3)

As a help content author, I want to use reusable HTML components (alert boxes, code blocks) defined in separate template files so that the help content has consistent formatting and I can update component designs globally.

**Why this priority**: Nice-to-have feature that improves content consistency. Lower priority than core functionality.

**Independent Test**: Can be tested by using component placeholders in help content and verifying they render correctly with the component template styling.

**Acceptance Scenarios**:

1. **Given** a topic contains an alert placeholder, **When** the topic is rendered, **Then** it displays as a styled alert box using the component template.
2. **Given** a topic contains a code block placeholder, **When** the topic is rendered, **Then** it displays as a formatted code block.
3. **Given** I update a component template to change the styling, **When** I reopen the help viewer, **Then** all topics using that component show the new styling.

---

### User Story 7 - Watermark/Logo Integration (Priority: P3)

As a system administrator, I want the help viewer to display the company logo as a background watermark so that the help documentation is visually consistent with the Release Notes viewer and reflects company branding.

**Why this priority**: Polish feature that enhances brand consistency. Not critical for functionality.

**Independent Test**: Can be tested by opening the help viewer and verifying a faint company logo appears in the background of help pages.

**Acceptance Scenarios**:

1. **Given** the help viewer is open, **When** I view any topic page, **Then** I see a faint watermark of the company logo in the background.
2. **Given** the watermark image file is missing, **When** the help viewer loads, **Then** the page still renders correctly without the watermark (graceful degradation).

---

### User Story 8 - Contact Support System (Priority: P1)

As a user, I want to submit bug reports, improvement suggestions, report inconsistencies, and ask general questions directly from the Help Viewer so that I can communicate issues and ideas to the development team without leaving the application.

**Why this priority**: Direct user feedback is critical for continuous improvement and issue resolution. This feature eliminates barriers to reporting problems and provides transparency into the development process. Users can see their submissions are acknowledged and tracked, building trust and engagement.

**Independent Test**: Can be fully tested by accessing the Help Viewer, clicking "Contact Support", submitting each type of feedback (bug/suggestion/inconsistency/question), verifying database storage, and confirming submissions appear in both user status view and developer management form.

**Acceptance Scenarios**:

#### Bug Report Submission (10 scenarios)

1. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Report a Bug", **Then** I see a bug report form with required fields: Window/Form (dropdown), Active Section (dropdown), Bug Category (dropdown), Bug Severity (dropdown), Description (multi-line), Steps to Reproduce (multi-line), Expected Behavior (textbox), and Actual Behavior (textbox).

2. **Given** I am on the bug report form, **When** I select a window from the "Window/Form" dropdown, **Then** the dropdown displays user-friendly names (e.g., "Main Inventory Window" not "MainForm", "Settings Window" not "SettingsForm", "Transfer Operations" not "Control_TransferTab").

3. **Given** I have selected a window in the bug report form, **When** the "Active Section" dropdown populates, **Then** it shows only the user controls/sections relevant to that window (e.g., selecting "Main Inventory Window" shows "Inventory Tab", "Advanced Inventory", "Remove Tab", "Advanced Removal", "Transfer Tab").

4. **Given** I am filling out the bug report form, **When** I click the "Bug Category" dropdown, **Then** I see options: Data Entry Error, Display/UI Issue, Calculation Error, Performance Issue, Crash/Freeze, Feature Not Working, Unexpected Behavior, Integration Issue (Infor Visual), Database Error, Printing Issue, Search/Filter Issue, Permission/Access Issue, Localization/Translation Issue, Export/Import Issue, Keyboard Navigation Issue, Theming/Visual Glitch, Other.

5. **Given** I have selected "Other" in the Bug Category dropdown, **When** the form updates, **Then** a text input field appears requiring me to specify the custom bug category.

6. **Given** I have filled out all required fields in the bug report form, **When** I click "Submit", **Then** the system captures auto-metadata (UserID, SubmissionDateTime, ApplicationVersion, WindowsVersion, MachineHostname) and stores the complete bug report in the database table `UserFeedback`.

7. **Given** I submit a bug report with severity "Critical" or "High", **When** the submission is saved to the database, **Then** the system sends an email notification to designated developers within 1 minute.

8. **Given** I successfully submit a bug report, **When** the submission completes, **Then** I see a confirmation message displaying a unique tracking reference number (e.g., "BUG-2025-001234").

9. **Given** I attempt to submit a bug report with missing required fields, **When** I click "Submit", **Then** the form displays clear validation error messages indicating which fields are incomplete and prevents submission.

10. **Given** the database connection fails during bug report submission, **When** the error occurs, **Then** the system displays a user-friendly error message and logs the failed submission attempt for later retry.

#### Improvement Suggestion Submission (6 scenarios)

11. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Suggest an Improvement", **Then** I see a suggestion form with fields: Suggestion Category (dropdown), Related Window (dropdown - optional), Priority (dropdown), Title (textbox), Description (multi-line), Business Justification (multi-line), Affected Users (dropdown).

12. **Given** I am filling out the suggestion form, **When** I click the "Suggestion Category" dropdown, **Then** I see options: New Feature Request, UI/UX Enhancement, Performance Improvement, Workflow Optimization, Reporting Enhancement, Integration Enhancement, Accessibility Improvement, Documentation Improvement, Keyboard Shortcut Request, Mobile/Tablet Support, Batch Operation Enhancement, Audit Trail Enhancement, Other.

13. **Given** I have selected "Other" in Suggestion Category, **When** the form updates, **Then** a text input field appears requiring me to specify the custom suggestion category.

14. **Given** I am filling out the suggestion form, **When** I click the "Affected Users" dropdown, **Then** I see options: Just Me, My Team, All Users, Specific Role.

15. **Given** I have filled out all required fields in the suggestion form, **When** I click "Submit", **Then** the system stores the suggestion with auto-captured metadata in the database and displays a confirmation with tracking reference number (e.g., "SUG-2025-001234").

16. **Given** I leave the "Related Window" field empty in a suggestion, **When** I submit the form, **Then** the submission succeeds (field is optional) and stores NULL for the Related Window.

#### Report Inconsistency Submission (5 scenarios)

17. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Report an Inconsistency", **Then** I see an inconsistency form with fields: Inconsistency Type (dropdown), Window/Form (dropdown), Active Section (dropdown), Description (multi-line), Location 1 (textbox), Location 2 (textbox), Expected Consistency (textbox).

18. **Given** I am filling out the inconsistency form, **When** I click the "Inconsistency Type" dropdown, **Then** I see options: Data Mismatch (between screens), Data Mismatch (with Infor Visual), Incorrect Labeling, Help Documentation Error, Conflicting Validation Rules, UI Inconsistency, Calculation Discrepancy, Permission Inconsistency, Theme/Styling Issue, Terminology Inconsistency, Date/Time Format Inconsistency, Measurement Unit Inconsistency, Other.

19. **Given** I have selected "Other" in Inconsistency Type, **When** the form updates, **Then** a text input field appears requiring me to specify the custom inconsistency type.

20. **Given** I have filled out all required fields in the inconsistency form, **When** I click "Submit", **Then** the system stores the inconsistency report with auto-captured metadata and displays confirmation with tracking reference number (e.g., "INC-2025-001234").

21. **Given** I report an inconsistency with very long description text (>5000 characters), **When** I submit the form, **Then** the system handles the full text without truncation and stores it in the database nvarchar(max) field.

#### Ask General Question (5 scenarios)

22. **Given** I am viewing the Help Viewer, **When** I click "Contact Support" → "Ask a Question", **Then** I see a question form with fields: Question Category (dropdown), Related Window (dropdown - optional), Priority (dropdown), Question (multi-line).

23. **Given** I am filling out the question form, **When** I click the "Question Category" dropdown, **Then** I see options: How-To Question, Feature Clarification, Best Practice Inquiry, Training Request, Configuration Help, Workflow Guidance, Integration Question, Permissions Question, Troubleshooting Assistance, Data Migration Question, Compliance/Audit Question, Other.

24. **Given** I have selected "Other" in Question Category, **When** the form updates, **Then** a text input field appears for custom question category specification.

25. **Given** I have filled out all required fields in the question form, **When** I click "Submit", **Then** the system stores the question with metadata and displays confirmation with tracking number (e.g., "QUE-2025-001234").

26. **Given** I select priority "Urgent" for a question, **When** I submit the form, **Then** the system flags the question for expedited developer review.

#### View Submission Status (8 scenarios)

27. **Given** I have previously submitted bug reports, suggestions, inconsistencies, or questions, **When** I click "View My Submissions" from the Contact Support page, **Then** I see a filterable/sortable list showing all my submissions with columns: Type, Title/Summary, Status, Priority/Severity, Submitted Date, Last Updated Date.

28. **Given** I am viewing my submissions list, **When** I click a column header (Type, Status, Date), **Then** the list sorts by that column in ascending/descending order.

29. **Given** I am viewing my submissions list, **When** I use the filter controls, **Then** I can filter by Submission Type (Bug/Suggestion/Inconsistency/Question), Status (New/In Review/In Progress/Resolved/Closed/Won't Fix), and Date Range.

30. **Given** I am viewing a submission in my list, **When** the submission has developer notes, **Then** I can see the developer's comments/updates.

31. **Given** I am viewing a submission with status "In Progress", **When** I click on the submission, **Then** I can add follow-up comments to provide additional context.

32. **Given** I am viewing a closed/resolved submission, **When** I attempt to add a comment, **Then** the system allows the comment but displays a notice that the issue is closed (developer can still review).

33. **Given** I am viewing my submissions, **When** there is a link between my submission and another related issue, **Then** I can see the relationship indicated (e.g., "Marked as duplicate of #123").

34. **Given** I navigate away from the submissions page while offline, **When** network connectivity is lost, **Then** the system gracefully handles the error and displays a clear offline message.

#### Developer Tools Form Access (7 scenarios)

35. **Given** I am logged in as a user with Admin or Developer role, **When** I navigate to the "Development" menu in MainForm, **Then** I see a "Developer Tools" menu item.

36. **Given** I am logged in as a standard user (non-admin), **When** I access the "Development" menu, **Then** the "Developer Tools" option is hidden or disabled.

37. **Given** I am in the Developer Tools form, **When** the form loads, **Then** I see all user feedback submissions (bugs, suggestions, inconsistencies, questions) in a grid with filters for Status, Priority, User, Date Range, Category.

38. **Given** I am viewing a submission in Developer Tools, **When** I click "Update Status", **Then** I can change the status from New → In Review → In Progress → Resolved → Closed or mark as "Won't Fix".

39. **Given** I am viewing a submission in Developer Tools, **When** I add developer notes, **Then** the notes are saved with timestamp and my UserID, and are visible to the original submitter in their "View My Submissions" page.

40. **Given** I am viewing multiple submissions in Developer Tools, **When** I identify duplicate submissions, **Then** I can mark one as duplicate and link it to the primary submission.

41. **Given** I am viewing submissions in Developer Tools, **When** I click "Export to CSV", **Then** the system exports all filtered submissions with all fields to a CSV file for external analysis.

---

### Edge Cases

- **Help viewer already open**: When a help button is clicked while the help viewer is already open, the system should bring the existing window to front and navigate to the new topic, not open a second instance.
- **External template file invalid**: When an external template file contains invalid HTML or CSS, the system should log an error and fall back to a built-in minimal template.
- **Component template missing**: When a component template is missing, the system should render content without the component and log a warning.
- **Theme changes**: When the user's theme changes while the help viewer is open, the help viewer should detect the theme change and re-render with new colors.
- **WebView2 runtime missing**: When the WebView2 runtime is not installed, the system should display a clear error message with download instructions instead of crashing.
- **User submits duplicate bug reports**: System allows submission but Developer Tools provides duplicate detection/linking functionality to merge related issues.
- **Required metadata cannot be captured** (e.g., hostname retrieval fails): System logs warning, stores NULL for failed metadata field, and allows submission to proceed with partial metadata.
- **Database connection fails during submission**: System displays user-friendly error message ("Unable to submit at this time. Please try again later."), logs the failed attempt with full submission content, and optionally allows user to save submission locally as JSON file for manual retry.
- **User attempts to comment on closed/resolved submission**: System allows the comment but displays notice ("This issue is marked as Resolved. Your comment will be reviewed by developers.") and flags the comment for developer attention.
- **Developer form accessed by non-admin user** (security bypass attempt): System performs role validation on form load and immediately closes form with access denied message, logging the unauthorized access attempt.
- **Very long description text** (>50,000 characters): Form validation displays character count warning at 45,000 chars and prevents submission beyond 50,000 chars with clear message.
- **User submits while offline**: HTML form in WebView2 detects offline state and displays message "You are currently offline. Please connect to the network to submit feedback." Submit button is disabled until connectivity is restored.
- **Window/section dropdown data becomes outdated after app update**: System loads dropdown data from database table (`WindowFormMapping`/`UserControlMapping`) at runtime, allowing administrators to update mappings via Developer Tools without code deployment.
- **Email notification fails for high-priority bug**: System logs email failure, increments retry counter, attempts resend every 5 minutes up to 3 times, then logs permanent failure for manual administrator review.
- **User's role changes from Developer to standard user while Developer Tools form is open**: Form detects role change on next action, displays "Your permissions have changed. Please close this window." and disables all controls.
- **Tracking number generation collision** (unlikely but possible): System uses combination of FeedbackType prefix, year, and auto-incrementing sequence with uniqueness constraint. On collision, retry with next sequence number.
- **Developer assigns bug to user who is not in Developer role**: Form validation checks assignee's role before saving and displays error "Selected user does not have Developer role. Please choose a valid developer."

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST provide context-sensitive help buttons on all major forms and tabs:

  | Component | Category | Topic | File Reference | Help Button ID | Button Location | Placement Strategy | Exists | Notes |
  |-----------|----------|-------|-----------------|-----------------|-----------------|-------------------|--------|-------|
  | **MainForm Help Button (Singleton - Context-Aware)** | | | | | | | | |
  | Inventory Tab | inventory-operations | inventory-overview | `Controls/MainForm/Control_InventoryTab.cs` | `MainForm_Button_Help_Inventory` | MainForm toolbar (top-right, next to menu) | Form-level button adapts content based on visible tab/control. Title: "Inventory" | YES | Primary inventory management interface |
  | Advanced Inventory | advanced-inventory-operations | advanced-inventory-overview | `Controls/MainForm/Control_AdvancedInventory.cs` | `MainForm_Button_Help_AdvancedInventory` | MainForm toolbar (top-right, next to menu) | Same form-level button updates when AdvancedInventory control becomes visible | NO | Extended inventory search and filtering |
  | Remove Tab | remove-operations | remove-overview | `Controls/MainForm/Control_RemoveTab.cs` | `MainForm_Button_Help_Remove` | MainForm toolbar (top-right, next to menu) | Same form-level button updates when RemoveTab control becomes visible | NO | Remove from inventory workflow |
  | Advanced Removal | advanced-remove-operations | advanced-remove-overview | `Controls/MainForm/Control_AdvancedRemove.cs` | `MainForm_Button_Help_AdvancedRemove` | MainForm toolbar (top-right, next to menu) | Same form-level button updates when AdvancedRemove control becomes visible | NO | Extended removal options and batch operations |
  | Transfer Tab | transfer-operations | transfer-overview | `Controls/MainForm/Control_TransferTab.cs` | `MainForm_Button_Help_Transfer` | MainForm toolbar (top-right, next to menu) | Same form-level button updates when TransferTab control becomes visible | NO | Transfer inventory between locations |
  | **SettingsForm Help Button (Singleton - Context-Aware)** | | | | | | | | |
  | Settings - Home | settings-management | home | `Controls/SettingsForm/Control_SettingsHome.cs` | `SettingsForm_Button_Help_Home` | SettingsForm toolbar (top-right panel, above right panel) | Form-level button adapts content based on visible settings panel. Title: "Settings Home" | NO | Settings management home page |
  | Settings - About | settings-management | about | `Controls/SettingsForm/Control_About.cs` | `SettingsForm_Button_Help_About` | SettingsForm toolbar (top-right panel) | Same form-level button updates when About panel becomes visible. Title: "About" | NO | Application information and version details |
  | Settings - Database | settings-management | database-config | `Controls/SettingsForm/Control_Database.cs` | `SettingsForm_Button_Help_Database` | SettingsForm toolbar (top-right panel) | Same form-level button updates when Database panel becomes visible. Title: "Database" | NO | Database connection configuration |
  | Settings - Part Numbers | settings-management | part-number-overview | `Controls/SettingsForm/Control_PartIDManagement.cs` | `SettingsForm_Button_Help_PartNumbers` | SettingsForm toolbar (top-right panel) | Same form-level button updates when PartNumbers panel becomes visible. Title: "Part Numbers" | NO | Part number management interface |
  | Settings - Operations | settings-management | operation-overview | `Controls/SettingsForm/Control_OperationManagement.cs` | `SettingsForm_Button_Help_Operations` | SettingsForm toolbar (top-right panel) | Same form-level button updates when Operations panel becomes visible. Title: "Operations" | NO | Operation management interface |
  | Settings - Locations | settings-management | location-overview | `Controls/SettingsForm/Control_LocationManagement.cs` | `SettingsForm_Button_Help_Locations` | SettingsForm toolbar (top-right panel) | Same form-level button updates when Locations panel becomes visible. Title: "Locations" | NO | Location management interface |
  | Settings - Inventory Types | settings-management | inventory-type-overview | `Controls/SettingsForm/Control_ItemTypeManagement.cs` | `SettingsForm_Button_Help_ItemTypes` | SettingsForm toolbar (top-right panel) | Same form-level button updates when ItemTypes panel becomes visible. Title: "Inventory Types" | NO | Inventory type management interface |
  | Settings - Users | settings-management | user-overview | `Controls/SettingsForm/Control_User_Management.cs` | `SettingsForm_Button_Help_Users` | SettingsForm toolbar (top-right panel) | Same form-level button updates when Users panel becomes visible. Title: "Users" | NO | User account management interface |
  | Settings - Shortcuts | settings-management | shortcuts | `Controls/SettingsForm/Control_Shortcuts.cs` | `SettingsForm_Button_Help_Shortcuts` | SettingsForm toolbar (top-right panel) | Same form-level button updates when Shortcuts panel becomes visible. Title: "Shortcuts" | NO | Manage keyboard shortcuts |
  | Settings - Theme | settings-management | themes | `Controls/SettingsForm/Control_Theme.cs` | `SettingsForm_Button_Help_Theme` | SettingsForm toolbar (top-right panel) | Same form-level button updates when Theme panel becomes visible. Title: "Theme" | NO | Theme and appearance customization |
  | **Specialized Forms** | | | | | | | | |
  | Print | print-operations | print-overview | `Forms/Shared/PrintForm.cs` | `PrintForm_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | Print operations and configuration |
  | Transactions | transaction-history | transaction-overview | `Forms/Transactions/Transactions.cs` | `Transactions_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | View transaction history and details |
  | Transaction Lifecycle | transaction-history | lifecycle | `Forms/Transactions/TransactionLifecycleForm.cs` | `TransactionLifecycleForm_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | View transaction batch lifecycle and relationships |
  | Visual Dashboard | infor-visual-integration | visual-dashboard-overview | `Forms/Visual/Form_InforVisualDashboard.cs` | `Form_InforVisualDashboard_Button_Help` | Form toolbar (top-right) | Individual form-level button adapts to active tab | NO | Main visual analytics interface |
  | Analytics | analytics-reporting | analytics-overview | `Forms/WIPAppAnalytics/Form_WIPUserAnalytics.cs` | `Form_WIPUserAnalytics_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | User-specific analytics and reporting |
  | Analytics Viewer | analytics-reporting | analytics-data | `Forms/Visual/Form_AnalyticsViewer.cs` | `Form_AnalyticsViewer_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | Detailed analytics data visualization |
  | Logs Viewer | admin-tools | logs | `Forms/ViewLogs/Form_ViewLogsForm.cs` | `Form_ViewLogsForm_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | Application logging and diagnostic information |
  | Error Reports | admin-tools | error-reports | `Forms/ErrorReports/Form_ViewErrorReports.cs` | `Form_ViewErrorReports_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | Error report review and analysis |
  | Release Notes | getting-started | release-notes | `Forms/Settings/SettingsForm_ViewReleaseNotesHTML.cs` | `Form_ReleaseNotes_Button_Help` | Form toolbar (top-right) | Individual form-level button | NO | Application version history and updates |
  | **Dialog Forms** | | | | | | | | |
  | Quick Button Edit | settings-management | quick-buttons | `Forms/Shared/Form_QuickButtonEdit.cs` | `Form_QuickButtonEdit_Button_Help` | Dialog toolbar (top-right) | Individual dialog-level button | NO | Configure custom quick action buttons |
  | Shortcut Edit | settings-management | shortcuts | `Forms/Shared/Form_ShortcutEdit.cs` | `Form_ShortcutEdit_Button_Help` | Dialog toolbar (top-right) | Individual dialog-level button | NO | Manage keyboard shortcuts |
  | PO Details | infor-visual-integration | po-details | `Forms/Visual/Form_PODetails.cs` | `Form_PODetails_Button_Help` | Dialog toolbar (top-right) | Individual dialog-level button | NO | Purchase order details and line items |
  | **Help & Documentation** | | | | | | | | |
  | Help Viewer | getting-started | help-index | `Forms/Help/HelpViewerForm.cs` | Not applicable | Built-in help system | Built-in help system (no additional button needed) | YES | Browse complete help documentation |

#### FR-001 Implementation Architecture Notes:

**MainForm Help Button Strategy:**
- Single help button positioned in top-right of MainForm (in a new toolbar panel next to menu)
- Button text/title dynamically updates based on currently visible control:
  - MainForm_TabControl.SelectedIndex determines which tab is active
  - If AdvancedInventory or AdvancedRemove is visible, display those titles instead
  - Button triggers ShowHelpAsync() with the appropriate category/topic
  - Quick Buttons control is always visible in the right panel (SplitContainer Panel2) - no separate help button needed for Quick Buttons

**SettingsForm Help Button Strategy:**
- Single help button positioned above SettingsForm_Panel_Right (in top-right panel toolbar)
- Button text/title dynamically updates based on currently visible settings panel:
  - Uses _settingsPanels dictionary key to determine which panel is active
  - CategoryTreeView.SelectedNode.Name provides the selected panel name
  - Button triggers ShowHelpAsync() with the appropriate category/topic
  - Monitors CategoryTreeView_AfterSelect event to update button state

**Implementation Sequence:**
1. Add `MainForm_Button_Help` to MainForm.Designer with toolbar positioning
2. Add event listener to MainForm_TabControl.SelectedIndexChanged to update button visibility/tooltip
3. Add ShowHelpAsync() click handler to MainForm_Button_Help
4. Add `SettingsForm_Button_Help` to SettingsForm.Designer above right panel
5. Add event listener to CategoryTreeView_AfterSelect to update button visibility/tooltip
6. Add ShowHelpAsync() click handler to SettingsForm_Button_Help
7. Individual forms (Print, Transactions, etc.) each get their own help button following the same pattern


- **FR-002**: System MUST include a top-level "Help" menu in the main application menu bar with the following items:
  - "Help Index" (F1) → Opens help viewer to index page
  - "Getting Started" (Ctrl+F1) → Opens "getting-started" category
  - "Keyboard Shortcuts" (Ctrl+Shift+K) → Opens "keyboard-shortcuts" category
  - Separator
  - "About" → Shows application version and credits

- **FR-003**: System MUST handle the case where the help viewer is already open when a help button is clicked by bringing the existing window to front and navigating to the requested category/topic (singleton pattern).

- **FR-004**: System MUST load the base HTML template from an external file at runtime, not compile it into the executable.

- **FR-005**: System MUST fall back to a minimal built-in template if external template files are missing or corrupt, and log an error appropriately.

- **FR-006**: System MUST include ARIA labels on all interactive HTML elements in the template for screen reader support.

- **FR-007**: System MUST ensure keyboard focus order is logical (sidebar → search → category cards → topic links) and Tab navigation works correctly within the viewer.

- **FR-008**: System SHOULD load component templates (alert box, code block) from external files and support placeholder replacement in help content.

- **FR-009**: System SHOULD support embedding a company logo watermark in the help viewer background.

- **FR-010**: System MUST display a user-friendly error message with a download link if WebView2 runtime is not installed, instead of crashing or showing a generic exception.

- **FR-011**: System MUST provide a "Recent Articles" section on the help home page showing the 5 most recently updated topics across all categories, ordered by LastUpdated date.

- **FR-012**: System MUST support instant search filtering as users type in the search box, showing live suggestions before Enter is pressed (autocomplete/instant search).

- **FR-013**: System MUST display category cards on the home page with the following elements:
  - Category icon (emoji or Unicode character)
  - Category title
  - Brief description (1-2 sentences)
  - Article count indicator
  - Click to navigate to category

- **FR-014**: System SHOULD provide a prominent search bar at the top of the help viewer with:
  - Placeholder text: "Ask question or search by keyword"
  - Search icon
  - Clear button (X) when text is entered
  - Minimum 3 characters before triggering search

- **FR-015**: System MUST support breadcrumb navigation showing the current location:
  - Format: "Home > Category > Topic"
  - Each segment is clickable to navigate back
  - Updates dynamically as user navigates

- **FR-016**: System SHOULD display article metadata on topic pages:
  - Last updated date
  - Estimated read time (based on word count)
  - Category badge
  - Related topics links (optional)

- **FR-017**: System MUST provide a mobile-responsive layout that adapts to different screen sizes:
  - Collapsible sidebar for narrow screens
  - Stacked card layout on mobile
  - Touch-friendly navigation elements
  - Readable font sizes across devices

- **FR-018**: System SHOULD support article categorization with visual indicators:
  - Policy articles: Document icon 📄
  - Process articles: Gear icon ⚙️
  - Emergency articles: Warning icon ⚠️
  - Tutorial articles: Book icon 📚
  - FAQ articles: Question icon ❓

- **FR-019**: System MUST provide a "Contact Support" or "Other questions?" link at the bottom of the help viewer that:
  - Opens email client with pre-filled support email
  - Or links to internal support form/ticket system
  - Or shows contact information dialog

- **FR-020**: System SHOULD support gradual content reveal for long articles:
  - Table of contents with anchor links
  - "Back to top" button
  - Section headers with permalink icons
  - Smooth scrolling to anchors

- **FR-021**: System MUST provide a "Contact Support" section in the Help Viewer with four navigation options: "Report a Bug", "Suggest an Improvement", "Report an Inconsistency", "Ask a Question".

- **FR-022**: System MUST store all user submissions in database table `UserFeedback` with `FeedbackType` discriminator column ('Bug', 'Suggestion', 'Inconsistency', 'Question') in the `mtm_wip_application_winforms` database.

- **FR-023**: System MUST auto-capture and store metadata with every submission: `UserID` (FK to Users table), `SubmissionDateTime` (datetime2), `ApplicationVersion` (nvarchar), `WindowsVersion` (nvarchar), `MachineHostname` (nvarchar).

- **FR-024**: System MUST use user-friendly display names for all windows/forms in dropdowns instead of code-based class names (e.g., "Main Inventory Window" instead of "MainForm.cs", "Settings Window" instead of "SettingsForm").

- **FR-025**: System MUST dynamically populate the "Active Section" dropdown based on the selected window, showing only relevant user controls/tabs for that specific window using a mapping table or configuration.

- **FR-026**: System MUST validate all required fields before allowing submission and display clear, specific error messages for incomplete forms (e.g., "Description is required", "Please select a Bug Category").

- **FR-027**: System MUST provide a "View My Submissions" page accessible from the Contact Support section, displaying all of the current user's previous submissions with real-time status updates.

- **FR-028**: System MUST allow users to filter submissions by Type (Bug/Suggestion/Inconsistency/Question), Status (New/In Review/In Progress/Resolved/Closed/Won't Fix), Priority, and Date Range, and sort by any column header.

- **FR-029**: System MUST create a new **Developer Tools** form accessible via the "Development" menu item in MainForm, allowing administrators to:
  - View all user feedback across all types in a unified grid
  - Filter by Status, Priority, Severity, User, Date Range, Category, Submission Type
  - Update submission status through defined workflow: New → In Review → In Progress → Resolved → Closed
  - Mark submissions as "Won't Fix" with required justification notes
  - Add internal developer notes visible to submitters
  - Assign submissions to specific developers (FK to Users table with Developer role)
  - Mark duplicate submissions and link to primary issue
  - Export filtered results to CSV for external analysis/reporting

- **FR-030**: System MUST restrict access to the Developer Tools form to users with `Admin` or `Developer` role only, hiding or disabling the menu option for standard users.

- **FR-031**: System MUST send automated email notifications to designated developers (configurable in settings) when bug reports with Severity "Critical" or "High" are submitted, including submission details and direct link to Developer Tools.

- **FR-032**: System MUST display a confirmation message immediately after successful submission, including a unique tracking reference number (e.g., "BUG-2025-001234") that users can reference for follow-up.

- **FR-033**: System MUST allow users to add follow-up comments to their existing submissions, creating a threaded conversation stored in a `UserFeedbackComments` table with timestamp and author tracking.

- **FR-034**: System MUST handle "Other" category selections by displaying an additional required text input field for users to specify their custom category name, stored in a separate field.

- **FR-035**: System MUST gracefully handle database connection failures during submission by displaying user-friendly error message, logging the failed attempt, and optionally allowing users to save submission locally for retry.

- **FR-036**: System MUST prevent injection attacks by validating and sanitizing all text inputs before storing in database, especially multi-line description fields.

- **FR-037**: System MUST store all window/form and user control mappings in a configuration table (`WindowFormMapping`/`UserControlMapping`) that can be updated without code changes when new forms are added to the application.

- **FR-038**: System MUST track Last Updated timestamp on submissions, updating it whenever status changes, developer notes are added, or follow-up comments are posted.

### Database Schema

#### Table: UserFeedback

```sql
CREATE TABLE UserFeedback (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    FeedbackType NVARCHAR(50) NOT NULL, -- 'Bug', 'Suggestion', 'Inconsistency', 'Question'
    TrackingNumber NVARCHAR(50) UNIQUE NOT NULL, -- e.g., 'BUG-2025-001234'
    
    -- User & Timestamp Info
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    SubmissionDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    LastUpdatedDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    -- Auto-Captured Metadata
    ApplicationVersion NVARCHAR(50),
    WindowsVersion NVARCHAR(100),
    MachineHostname NVARCHAR(255),
    
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
    AssignedToDeveloperID INT NULL FOREIGN KEY REFERENCES Users(UserID),
    DeveloperNotes NVARCHAR(MAX),
    ResolutionDateTime DATETIME2 NULL,
    
    -- Relationship Tracking
    IsDuplicate BIT DEFAULT 0,
    DuplicateOfFeedbackID INT NULL FOREIGN KEY REFERENCES UserFeedback(FeedbackID)
);

CREATE INDEX IX_UserFeedback_UserID ON UserFeedback(UserID);
CREATE INDEX IX_UserFeedback_Status ON UserFeedback(Status);
CREATE INDEX IX_UserFeedback_FeedbackType ON UserFeedback(FeedbackType);
CREATE INDEX IX_UserFeedback_SubmissionDateTime ON UserFeedback(SubmissionDateTime);
```

#### Table: UserFeedbackComments

```sql
CREATE TABLE UserFeedbackComments (
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    FeedbackID INT NOT NULL FOREIGN KEY REFERENCES UserFeedback(FeedbackID) ON DELETE CASCADE,
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
    CommentDateTime DATETIME2 NOT NULL DEFAULT GETDATE(),
    CommentText NVARCHAR(MAX) NOT NULL,
    IsInternalNote BIT DEFAULT 0 -- TRUE if from developer, FALSE if from submitter
);

CREATE INDEX IX_UserFeedbackComments_FeedbackID ON UserFeedbackComments(FeedbackID);
```

#### Table: WindowFormMapping (Configuration)

```sql
CREATE TABLE WindowFormMapping (
    MappingID INT IDENTITY(1,1) PRIMARY KEY,
    CodebaseName NVARCHAR(255) NOT NULL, -- e.g., 'MainForm', 'SettingsForm'
    UserFriendlyName NVARCHAR(255) NOT NULL, -- e.g., 'Main Inventory Window', 'Settings Window'
    IsActive BIT DEFAULT 1
);

CREATE TABLE UserControlMapping (
    MappingID INT IDENTITY(1,1) PRIMARY KEY,
    WindowFormMappingID INT NOT NULL FOREIGN KEY REFERENCES WindowFormMapping(MappingID),
    CodebaseName NVARCHAR(255) NOT NULL, -- e.g., 'Control_InventoryTab'
    UserFriendlyName NVARCHAR(255) NOT NULL, -- e.g., 'Inventory Tab'
    IsActive BIT DEFAULT 1
);
```

### Key Entities

Enhanced entities required:
- **HelpViewerForm**: The main form that displays help content (enhanced with search, breadcrumbs, recent articles, Contact Support integration)
- **Service_HelpTemplateEngine**: Service that generates HTML from templates and content (enhanced with component templates)
- **Service_HelpSearchEngine**: New service for instant search, autocomplete, and relevance scoring
- **Service_FeedbackManager**: Service class for CRUD operations on feedback data, including validation, submission, status updates, comment management, and export functionality
- **Service_EmailNotificationManager**: Service for sending email notifications to developers when high-priority bugs are submitted (may extend existing notification infrastructure)
- **Form_DeveloperTools**: New WinForms form accessible via Development menu for administrators to manage all user feedback submissions with filtering, status updates, assignment, and export capabilities
- **Model_UserFeedback**: Entity model representing a user feedback submission (bug/suggestion/inconsistency/question) with all metadata and status tracking properties
- **Model_UserFeedbackComment**: Entity model for follow-up comments on submissions, supporting threaded conversations
- **Model_WindowFormMapping**: Configuration entity mapping code-based form names to user-friendly display names
- **Model_UserControlMapping**: Configuration entity mapping user controls to their parent forms with user-friendly names
- **Template Files**: External HTML/CSS files in Documentation/Help/Templates/
  - `help-base-template.html` - Main page template
  - `help-home-template.html` - Home page with category cards and recent articles
  - `help-category-card-component.html` - Reusable category card
  - `help-search-component.html` - Search box with autocomplete
  - `help-breadcrumb-component.html` - Navigation breadcrumb
  - `help-recent-articles-component.html` - Recent articles sidebar widget
  - `help-contact-support-page.html` - Contact Support landing page with four submission options
  - `help-bug-report-form.html` - HTML form for bug report submission with dynamic dropdown population
  - `help-suggestion-form.html` - HTML form for improvement suggestions
  - `help-inconsistency-form.html` - HTML form for reporting inconsistencies
  - `help-question-form.html` - HTML form for asking questions
  - `help-view-submissions.html` - HTML page displaying user's submission history with filtering and sorting
- **Model_HelpTopic**: Enhanced with ArticleType, ReadTimeMinutes, RelatedTopicIds properties
- **Model_HelpSearchSuggestion**: New model for autocomplete suggestions with preview text

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of major application forms/tabs have a visible Help button that navigates to the correct category.
- **SC-002**: Help Index is accessible via menu bar within 2 clicks from any screen.
- **SC-003**: Template changes (CSS/HTML) can be deployed without application recompilation (update file only, restart viewer).
- **SC-004**: Help viewer passes basic accessibility audit (no critical errors for keyboard navigation and screen reader support).
- **SC-005**: Help viewer supports single-instance pattern (clicking help button when viewer is open does not create a new window).
- **SC-006**: WebView2 runtime errors display user-friendly message with download link in 100% of cases.
- **SC-007**: Search returns relevant results in < 500ms for typical queries (< 100 total topics).
- **SC-008**: Autocomplete suggestions appear in < 200ms as user types.
- **SC-009**: Recent Articles section updates correctly when topics are modified (based on LastUpdated field).
- **SC-010**: Help viewer is fully responsive and usable on screens from 320px to 4K resolution.
- **SC-011**: Category cards on home page show accurate article counts.
- **SC-012**: Breadcrumb navigation accurately reflects current location and all segments are clickable.
- **SC-013**: Contact Support link successfully opens email client or support form.
- **SC-014**: All interactive elements have proper focus states and keyboard navigation works throughout.
- **SC-015**: Users can submit bug reports with all required fields populated and receive confirmation with tracking number within 2 seconds of clicking Submit.
- **SC-016**: Developer Tools form displays all submissions with real-time filtering (by status, priority, user, category, date range) and sorting (by any column) with results appearing in < 1 second.
- **SC-017**: Users can view the current status of all their submissions, see developer notes/updates, and add follow-up comments, with changes reflected immediately.
- **SC-018**: High-priority (High/Critical severity) bug submissions trigger email notification to designated developers within 1 minute of submission, with 99% delivery success rate.
- **SC-019**: Contact Support forms in Help Viewer render correctly across all supported browsers (Edge, Chrome) and validate all required fields with clear error messages before allowing submission.
- **SC-020**: Window/Form and Active Section dropdowns display 100% user-friendly names with 0% code-based class names visible to users (e.g., no "MainForm.cs", only "Main Inventory Window").
- **SC-021**: Developer Tools form allows administrators to export filtered submissions to CSV format including all fields (metadata, content, status, comments) in < 3 seconds for datasets up to 10,000 records.
- **SC-022**: System successfully handles and stores bug descriptions with up to 50,000 characters without truncation or data loss.
- **SC-023**: Duplicate submission detection and linking functionality correctly maintains relationships, with linked submissions visible in both user and developer views.
- **SC-024**: Follow-up comment system supports threaded conversations with proper timestamp ordering and author attribution, displaying internal developer notes distinctly from user comments.

### Validation Service Integration

- **Template Loading**: Validate file existence before loading. Catch and log file access errors appropriately.
- **HTML Safety**: For admin-only JSON content, HTML sanitization is lower priority. If rendering user-editable content in the future, sanitize HTML to prevent script injection.
- **WebView2 Availability**: Check for WebView2 runtime availability before initializing. Provide clear error message if missing.

## Assumptions

- The existing `HelpViewerForm` and `Service_HelpTemplateEngine` architecture will be extended, not replaced.
- External template files will be deployed alongside the application in the Documentation/Help/Templates/ directory.
- The WebView2 runtime is expected to be pre-installed on most target machines (Windows 10/11), but graceful degradation is required for machines without it.
- JSON help content files are maintained by administrators, not end users, so HTML sanitization is lower priority.
- The existing theme system integration will be preserved and extended for accessibility compliance.
- Search functionality operates on client-side (in-memory) data for performance, not server-side indexing.
- Recent Articles are determined by the `LastUpdated` field in JSON, not by actual view/access statistics.
- Mobile responsiveness refers to the HTML rendering within WebView2, not native mobile app support.
- Article read time estimation uses a standard 200 words per minute reading speed.
- Contact Support functionality will use mailto: links initially, with potential for future integration with ticketing systems.

## Dependencies

- Depends on `016-new-help-system` feature being complete (base help viewer functionality).
- WebView2 runtime must be available or error handling must be in place.
- Help content JSON files must exist for each category referenced by help buttons.
