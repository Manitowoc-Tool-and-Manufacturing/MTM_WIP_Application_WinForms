# Feature Specification: Modern JSON-Driven Help System

**Feature Branch**: `016-new-help-system`  
**Created**: 2025-12-06  
**Status**: Draft  
**Input**: User description: "Implement a modern, HTML-based help system for the MTM WIP Application that replaces the current static HTML help files with a dynamic, JSON-driven system modeled after the existing Release Notes viewer."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Browse and Read Help Topics (Priority: P1)

As a user, I want to browse help categories and read specific topics so that I can learn how to use the application features.

**Why this priority**: This is the core functionality of a help system. Without this, the feature provides no value.

**Independent Test**: Can be fully tested by launching the help viewer and navigating through different categories and topics.

**Acceptance Scenarios**:

1. **Given** the application is running, **When** I press F1 or click "Help", **Then** the help viewer opens to the index page showing all categories.
2. **Given** the help viewer is open, **When** I click a category card, **Then** the sidebar populates with topics for that category and the first topic is displayed.
3. **Given** a topic is displayed, **When** I scroll through the content, **Then** I can read the full article including formatted text and lists.

---

### User Story 2 - Search Help Content (Priority: P1)

As a user, I want to search for specific keywords so that I can quickly find relevant help topics without browsing.

**Why this priority**: Users often have specific questions and browsing is too slow. Search is critical for usability.

**Independent Test**: Can be tested by typing various keywords into the search box and verifying the filtered results.

**Acceptance Scenarios**:

1. **Given** the help viewer is open, **When** I type "inventory" into the search box, **Then** the list of topics is filtered to show only those containing "inventory" in the title, summary, or keywords.
2. **Given** search results are displayed, **When** I click a result, **Then** the viewer navigates to that specific topic.
3. **Given** no topics match the search term, **When** I type a nonsense string, **Then** the interface indicates no results found (or hides all topics).

---

### User Story 3 - Context-Sensitive Access (Priority: P2)

As a user, I want to click a help button on a specific screen and be taken directly to the relevant help topic so that I get immediate assistance for my current task.

**Why this priority**: Improves user experience by reducing friction in finding relevant information.

**Independent Test**: Can be tested by clicking help buttons on different forms (e.g., Inventory, Settings) and verifying the correct topic opens.

**Acceptance Scenarios**:

1. **Given** I am on the "Inventory" tab, **When** I click the help icon/button, **Then** the help viewer opens directly to the "Inventory Operations" category.
2. **Given** I am on the "Settings" form, **When** I click the help icon, **Then** the help viewer opens directly to the "Settings Management" category.

---

### User Story 4 - Maintain Help Content (Priority: P3)

As a content maintainer, I want to update help text in JSON files so that I can correct errors or add new information without needing a developer to recompile the application.

**Why this priority**: Reduces maintenance overhead and allows non-developers to update documentation.

**Independent Test**: Can be tested by modifying a JSON file in the deployment directory and restarting the help viewer to see changes.

**Acceptance Scenarios**:

1. **Given** the application is installed, **When** I edit `getting-started.json` to change a topic title, **Then** the new title appears in the help viewer immediately upon next launch.
2. **Given** a new feature is added, **When** I create a new JSON file `new-feature.json` and add it to the folder, **Then** the new category appears in the help index automatically.

### Edge Cases

- What happens when a JSON file is malformed? (System should log error and skip that file/category, not crash).
- What happens when a referenced image/asset is missing? (System should show a placeholder or broken image icon, not crash).
- What happens when the search term matches nothing? (List becomes empty, user sees visual feedback).
- What happens when the help viewer is opened while already open? (Bring existing window to front or allow multiple instances - preference for single instance brought to front).

### Assumptions

- The application has write access to the log directory for error reporting.
- The target environment supports the necessary rendering components (e.g., WebView2 runtime is available).
- Help content authors have access to a text editor to modify JSON files.
- **Content Constraint**: New help content must be authored from scratch based on current application behavior. Existing static HTML help files are outdated and must not be used as a source of truth.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST load help content from external JSON files. The initial release MUST include the following categories:
  - Getting Started
  - Inventory Operations
  - Remove Operations
  - Transfer Operations
  - Advanced Features
  - Transaction History
  - Infor Visual Integration
  - Analytics Reporting
  - Settings Management
  - Admin Tools
  - Keyboard Shortcuts
  - Troubleshooting
  - System Configuration
- **FR-002**: System MUST render help content using a unified, responsive HTML template displayed within a WebView2 control.
- **FR-003**: System MUST provide a sidebar navigation menu that lists topics within the current category.
- **FR-004**: System MUST provide a global index page that displays all available help categories as clickable cards.
- **FR-005**: System MUST include a search function that filters and ranks the visible topic list, prioritizing matches in Title, Summary, and Keywords over Content.
- **FR-006**: System MUST support "deep linking" to open the help viewer directly to a specific Category and Topic ID.
- **FR-007**: System MUST gracefully handle missing or corrupt JSON files by logging the error and continuing to load other valid content.
- **FR-008**: System MUST support rich HTML formatting in content, including headers, lists, bold/italic text, paragraphs, alert boxes (info/warning), and code blocks.
- **FR-009**: System MUST integrate with the application's visual theme (e.g., window borders, title bar).
- **FR-010**: System MUST display a background watermark (e.g., company logo) on help pages, consistent with the Release Notes viewer.
- **FR-011**: System MUST support standard keyboard shortcuts: `F1` to open help, and `Ctrl+F` (when viewer is open) to focus the search box.
- **FR-012**: System MUST display breadcrumb navigation (e.g., "Home > Category > Topic") to help users understand their location within the documentation.
- **FR-013**: System MUST provide access to the help system via the main application menu (e.g., a top-level "Help" menu item).
- **FR-014**: System MUST ensure HTML templates include standard ARIA labels and support keyboard navigation within the WebView2 control for accessibility.

### Key Entities

- **HelpCategory**: Represents a major section of documentation (e.g., "Getting Started"). Attributes: `CategoryId`, `Category` (Display Name), `Icon`, `Description`, `Topics` (List).
- **HelpTopic**: Represents a single article. Attributes: `TopicId`, `Title`, `Summary`, `Content` (HTML), `Keywords` (List), `LastUpdated`.
- **HelpSearchResult**: Represents a match found during search. Attributes: `TopicId`, `Title`, `RelevanceScore`.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Help viewer opens and renders the index page in under 1 second on standard hardware.
- **SC-002**: Search results update in real-time (under 200ms) as the user types.
- **SC-003**: 100% of help content is decoupled from the application executable (contained entirely in external assets).
- **SC-004**: Adding a new help category requires 0 lines of application code changes (only adding a data file).
- **SC-005**: Help viewer correctly renders text and background colors that contrast appropriately in both Light and Dark application themes.

### Validation Service Integration

- **Search Input**: No strict validation required, but input should be sanitized to prevent HTML injection if reflected in the UI (though strictly local context reduces risk).
- **JSON Parsing**: Use `Service_ErrorHandler` to catch and log `JsonException` during content loading.
- **File Paths**: Validate file existence before attempting read; log warnings for missing expected files.
