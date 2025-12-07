# Feature Specification: Help System Missing Features

**Feature Branch**: `016-new-help-system-enhancements`  
**Created**: 2025-12-07  
**Status**: Draft  
**Parent Feature**: `016-new-help-system`  
**Input**: Analysis of NEW_HELP_SYSTEM_IMPLEMENTATION_PROMPT.md against current implementation

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Context Menu Help Buttons on All Forms (Priority: P1)

As a user, I want to click a help button on any screen (Inventory, Remove, Transfer, Settings, etc.) and be taken directly to the relevant help topic so that I don't have to browse or search for context-specific information.

**Why this priority**: The original spec emphasizes context-sensitive help as a core feature. Currently only one tab has a help button, and it wasn't using the correct category ID until recently fixed.

**Independent Test**: Can be fully tested by clicking help buttons on each major form/tab and verifying the correct category opens with the first topic displayed.

**Acceptance Scenarios**:

1. **Given** I am on the "Inventory" tab, **When** I click the Help button, **Then** the help viewer opens to the "Inventory Operations" category.
2. **Given** I am on the "Remove" tab, **When** I click the Help button, **Then** the help viewer opens to the "Remove Operations" category.
3. **Given** I am on the "Transfer" tab, **When** I click the Help button, **Then** the help viewer opens to the "Transfer Operations" category.
4. **Given** I am in the "Settings" form, **When** I click the Help button, **Then** the help viewer opens to the "Settings Management" category.
5. **Given** I am in the "Transactions" viewer, **When** I click the Help button, **Then** the help viewer opens to the "Transaction History" category.
6. **Given** I am in the "Visual Dashboard", **When** I click the Help button, **Then** the help viewer opens to the "Infor Visual Integration" category.
7. **Given** I am in the "Analytics" form, **When** I click the Help button, **Then** the help viewer opens to the "Analytics & Reporting" category.

---

### User Story 2 - Help Menu Integration (Priority: P1)

As a user, I want to access the help system from the main application menu bar so that I can easily find help documentation without needing to know keyboard shortcuts.

**Why this priority**: The original spec (FR-013) explicitly requires menu bar access. This is standard UX for Windows applications and provides discoverability.

**Independent Test**: Can be tested by opening the Help menu and selecting options to verify they navigate to the correct content.

**Acceptance Scenarios**:

1. **Given** the application is running, **When** I click the "Help" menu in the menu bar, **Then** I see menu items: "Help Index", "Getting Started", "Keyboard Shortcuts", and "About".
2. **Given** the Help menu is open, **When** I click "Help Index", **Then** the help viewer opens to the index page showing all categories.
3. **Given** the Help menu is open, **When** I click "Getting Started", **Then** the help viewer opens directly to the "Getting Started" category.
4. **Given** the Help menu is open, **When** I click "Keyboard Shortcuts", **Then** the help viewer opens directly to the "Keyboard Shortcuts" category.

---

### User Story 3 - External Template File Loading (Priority: P2)

As a help content maintainer, I want the HTML template to be loaded from an external file (not hard-coded in C#) so that I can update the template design, styling, and structure without recompiling the application.

**Why this priority**: The original spec shows templates as separate HTML files in `Documentation/Help/Templates/`. The current implementation has templates hard-coded in `Service_HelpTemplateEngine.cs` as string literals, defeating the purpose of having a template system.

**Independent Test**: Can be tested by modifying `help-base-template.html` to change a CSS style (e.g., background color) and verifying the change appears in the help viewer without recompiling.

**Acceptance Scenarios**:

1. **Given** the application is deployed, **When** I edit `Documentation/Help/Templates/help-base-template.html` to change the header background color, **Then** the next time I open the help viewer, the new color is displayed.
2. **Given** the template file is missing or corrupt, **When** the help viewer tries to load, **Then** the system falls back to a minimal hard-coded template and logs an error.
3. **Given** I want to add a new CSS class, **When** I add it to the external template file, **Then** topics can use that class in their Content HTML without code changes.

---

### User Story 4 - Component Template Support (Priority: P3)

As a help content author, I want to use reusable HTML components (topic cards, search boxes, breadcrumbs, alert boxes) defined in separate template files so that the help content has consistent formatting and I can update component designs globally.

**Why this priority**: The original spec defines multiple component templates (`topic-card-template.html`, `search-box-template.html`, `breadcrumb-template.html`, `alert-box-template.html`, `code-block-template.html`). Currently these are either hard-coded or not implemented.

**Independent Test**: Can be tested by using component templates in help content and verifying they render correctly, then updating a component template and seeing the changes apply globally.

**Acceptance Scenarios**:

1. **Given** a topic contains `{{ALERT:INFO:message}}` placeholder, **When** the topic is rendered, **Then** it displays as a styled info alert box using the `alert-box-template.html` component.
2. **Given** a topic contains `{{CODE:language:snippet}}` placeholder, **When** the topic is rendered, **Then** it displays as a syntax-highlighted code block using the `code-block-template.html` component.
3. **Given** I update the `alert-box-template.html` to change the icon, **When** I reopen the help viewer, **Then** all topics with alerts show the new icon.

---

### User Story 5 - Watermark/Logo Integration (Priority: P3)

As a system administrator, I want the help viewer to display the company logo as a background watermark so that the help documentation is visually consistent with the Release Notes viewer and reflects company branding.

**Why this priority**: The original spec (FR-010) requires watermark support consistent with Release Notes. This is a polish feature that enhances brand consistency.

**Independent Test**: Can be tested by opening the help viewer and verifying a faint company logo appears in the background of help pages.

**Acceptance Scenarios**:

1. **Given** the help viewer is open, **When** I view any topic page, **Then** I see a faint watermark of the company logo in the background.
2. **Given** the watermark image file is missing, **When** the help viewer loads, **Then** the page still renders correctly without the watermark (graceful degradation).

---

### User Story 6 - ARIA Labels and Accessibility (Priority: P2)

As a user with accessibility needs, I want the help viewer to support keyboard navigation and screen readers so that I can access help documentation effectively.

**Why this priority**: The original spec (FR-014) mandates accessibility support. This is important for compliance and inclusive design.

**Independent Test**: Can be tested using screen reader software (e.g., NVDA) and keyboard-only navigation to verify all interactive elements are accessible.

**Acceptance Scenarios**:

1. **Given** I am using a screen reader, **When** the help viewer opens, **Then** the sidebar navigation items are announced correctly with role and state information.
2. **Given** I am navigating via keyboard only, **When** I press Tab repeatedly, **Then** I can navigate through all clickable elements (category cards, topic links, search box) in a logical order.
3. **Given** I am on a topic page, **When** I use arrow keys, **Then** I can navigate through the content smoothly.

### Edge Cases

- What happens when a help button is clicked while the help viewer is already open? (Should bring existing window to front and navigate to the new topic, not open a second instance)
- What happens when an external template file contains invalid HTML or CSS? (System should log error and fall back to hard-coded minimal template)
- What happens when a component template is missing? (System should render content without the component and log a warning)
- What happens when the user's theme changes while the help viewer is open? (Help viewer should detect theme change and re-render with new colors)
- What happens when the WebView2 runtime is not installed? (System should display a clear error message with download instructions)

## Requirements *(mandatory)*

### Functional Requirements

- **FR-015**: System MUST provide context-sensitive help buttons on all major forms and tabs:
  - Inventory Tab → "inventory-operations"
  - Remove Tab → "remove-operations"
  - Transfer Tab → "transfer-operations"
  - Settings Form → "settings-management"
  - Transactions Form → "transaction-history"
  - Visual Dashboard → "infor-visual-integration"
  - Analytics Form → "analytics-reporting"
  - Admin/Logs → "admin-tools"

- **FR-016**: System MUST include a top-level "Help" menu in the main application menu bar with the following items:
  - "Help Index" (F1) → Opens help viewer to index page
  - "Getting Started" (Ctrl+F1) → Opens "getting-started" category
  - "Keyboard Shortcuts" (Ctrl+Shift+K) → Opens "keyboard-shortcuts" category
  - Separator
  - "About" → Shows application version and credits

- **FR-017**: System MUST load the base HTML template from an external file (`Documentation/Help/Templates/help-base-template.html`) at runtime, not compile it into the executable.

- **FR-018**: System SHOULD load component templates (topic card, search box, breadcrumb, alert box, code block) from external files and support placeholder replacement in help content.

- **FR-019**: System MUST support embedding a company logo watermark in the help viewer background using CSS and an embedded image resource (Base64 or file path).

- **FR-020**: System MUST include ARIA labels (`aria-label`, `role`, `aria-current`) on all interactive HTML elements in the template for screen reader support.

- **FR-021**: System MUST ensure keyboard focus order is logical (sidebar → search → category cards → topic links) and Tab navigation works correctly within the WebView2 control.

- **FR-022**: System MUST handle the case where the help viewer is already open when a help button is clicked by bringing the existing window to front and navigating to the requested category/topic (singleton pattern).

- **FR-023**: System MUST fall back to a minimal hard-coded template if external template files are missing or corrupt, and log an error via `Service_ErrorHandler`.

- **FR-024**: System MUST display a user-friendly error message with a download link if WebView2 runtime is not installed, instead of crashing or showing a generic exception.

### Key Entities

No new entities required. Enhancements apply to existing `HelpViewerForm`, `Service_HelpTemplateEngine`, and template files.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-006**: 100% of major application forms/tabs have a visible Help button that navigates to the correct category.
- **SC-007**: Help Index is accessible via menu bar within 2 clicks from any screen.
- **SC-008**: Template changes (CSS/HTML) can be deployed without application recompilation (update file only, restart viewer).
- **SC-009**: Help viewer passes basic accessibility audit using Accessibility Insights or similar tool (no critical errors).
- **SC-010**: Help viewer supports single-instance pattern (clicking help button when viewer is open does not create a new window).

### Validation Service Integration

- **Template Loading**: Validate file existence before `File.ReadAllText()`. Catch and log `IOException` or `FileNotFoundException` via `Service_ErrorHandler`.
- **HTML Safety**: If rendering user-editable content in the future, sanitize HTML to prevent script injection. For admin-only JSON content, this is lower priority.
- **WebView2 Availability**: Check for WebView2 runtime availability before initializing. Provide clear error message if missing.
