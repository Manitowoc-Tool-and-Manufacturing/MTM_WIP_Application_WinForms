# Tasks: Modern JSON-Driven Help System

**Feature**: `016-new-help-system`
**Spec**: [specs/016-new-help-system/spec.md](specs/016-new-help-system/spec.md)

## Phase 1: Setup & Infrastructure
*Goal: Establish the core data structures and service interfaces required for the help system.*

- [x] T001 Create `Models/Help/Model_HelpCategory.cs` with properties for ID, Title, Icon, Description, Order, and Topics list
- [x] T002 Create `Models/Help/Model_HelpTopic.cs` with properties for ID, Title, Summary, Content, Keywords, and LastUpdated
- [x] T003 Create `Models/Help/Model_HelpSearchResult.cs` with properties for Topic, Category, and RelevanceScore
- [x] T004 Create `Services/Help/IHelpSystem.cs` interface definition matching the contract
- [x] T005 Create `Services/Help/Service_HelpSystem.cs` implementing `IHelpSystem` (skeleton only)
- [x] T006 Create `Services/Help/Service_HelpContentLoader.cs` for JSON deserialization logic
- [x] T007 Create `Services/Help/Service_HelpTemplateEngine.cs` for HTML generation logic
- [x] T008 Create directory structure `Documentation/Help/JSON/` and `Documentation/Help/Templates/`

## Phase 2: Foundational Implementation
*Goal: Implement the core logic for loading content, generating HTML, and handling search.*

- [x] T009 Implement `Service_HelpContentLoader.LoadCategoriesAsync()` to read all JSON files from `Documentation/Help/JSON/`
- [x] T010 Implement `Service_HelpTemplateEngine.GenerateIndexHtml()` to render the category card grid
- [x] T011 Implement `Service_HelpTemplateEngine.GenerateTopicHtml()` to render the sidebar and topic content
- [x] T012 Implement `Service_HelpSystem.Search()` using LINQ to filter topics by Title, Summary, and Keywords
- [x] T013 Create `Documentation/Help/Templates/help-base-template.html` with WebView2-compatible HTML/CSS structure
- [x] T014 Create `Documentation/Help/Templates/topic-card-template.html` and `search-box-template.html` components
- [x] T015 Implement `Service_HelpSystem.InitializeAsync()` to orchestrate content loading and caching

## Phase 3: User Story 1 - Browse and Read Help Topics (P1)
*Goal: Enable users to view the help index, navigate categories, and read topics.*

- [x] T016 [US1] Create `Forms/Help/HelpViewerForm.cs` inheriting from `ThemedForm`
- [x] T017 [US1] Add `WebView2` control to `HelpViewerForm` and configure initialization
- [x] T018 [US1] Implement `HelpViewerForm` constructor to inject `IHelpSystem` (or use static service access)
- [x] T019 [US1] Implement `HelpViewerForm_Load` to call `GenerateIndexHtml()` and navigate WebView2
- [x] T020 [US1] Implement WebView2 `NavigationStarting` handler to intercept `help://` links for internal navigation
- [x] T021 [US1] Create `getting-started.json` with initial "Getting Started" content
- [x] T022 [US1] Create `inventory-operations.json` with "Inventory Operations" content
- [x] T023 [US1] Create `system-configuration.json` with "System Configuration" content

## Phase 4: User Story 2 - Search Help Content (P1)
*Goal: Enable users to find specific topics via keyword search.*

- [x] T024 [US2] Update `help-base-template.html` to include the search box UI in the sidebar
- [x] T025 [US2] Implement JavaScript in `help-base-template.html` to capture search input and send to C# via `window.chrome.webview.postMessage`
- [x] T026 [US2] Implement `WebMessageReceived` handler in `HelpViewerForm` to process search queries
- [x] T027 [US2] Update `Service_HelpTemplateEngine` to render search results HTML
- [x] T028 [US2] Implement logic to display search results in the sidebar or main content area dynamically

## Phase 5: User Story 3 - Context-Sensitive Access (P2)
*Goal: Allow users to open help directly to relevant topics from specific application screens.*

- [x] T029 [US3] Add `ShowHelp(string categoryId, string topicId)` method to `HelpViewerForm` (public API)
- [ ] T030 [US3] Add "Help" button/icon to `MainForm` (Inventory tab) that calls `ShowHelp("inventory-operations")`
- [ ] T031 [US3] Add "Help" button/icon to `SettingsForm` that calls `ShowHelp("settings-management")`
- [ ] T032 [US3] Implement `F1` key handler in `MainForm` to open Help Viewer (default index)
- [x] T033 [US3] Create `settings-management.json` with "Settings Management" content

## Phase 6: User Story 4 - Maintain Help Content (P3)
*Goal: Populate the remaining help content files.*

- [x] T034 [US4] Create `remove-operations.json`
- [x] T035 [US4] Create `transfer-operations.json`
- [x] T036 [US4] Create `advanced-features.json`
- [x] T037 [US4] Create `transaction-history.json`
- [x] T038 [US4] Create `infor-visual-integration.json`
- [x] T039 [US4] Create `analytics-reporting.json`
- [x] T040 [US4] Create `admin-tools.json`
- [x] T041 [US4] Create `keyboard-shortcuts.json`
- [x] T042 [US4] Create `troubleshooting.json`

## Phase 7: Polish & Cross-Cutting Concerns
*Goal: Ensure quality, theming, and accessibility.*

- [x] T043 Update `Service_HelpTemplateEngine` to inject current theme colors (Light/Dark) into CSS variables
- [x] T044 Add `Ctrl+F` shortcut handler to `HelpViewerForm` to focus the HTML search box
- [ ] T045 Add "Help" menu item to the main application `MenuStrip`
- [x] T046 Verify ARIA labels on all HTML templates for accessibility
- [x] T047 Add background watermark (company logo) to `help-base-template.html` CSS
- [x] T048 Implement error handling in `HelpViewerForm` for WebView2 initialization failures (fallback message)
- [x] T049 Update `AGENTS.md` with new help system architecture details
- [x] T050 Create `.github/instructions/help-system.instructions.md` guide for contributors

## Phase 8: Legacy Cleanup
*Goal: Remove the obsolete static HTML help system.*

- [x] T051 Remove legacy HTML files (*.html) from `Documentation/Help/` (ensure new Templates folder is preserved)
- [x] T052 Remove legacy CSS/JS files (help-styles.css, help-navigation.js) from `Documentation/Help/`
- [x] T053 Remove any legacy C# code or resources referencing the old help files (if applicable)

## Dependencies

- **Phase 1 & 2** must be completed before **Phase 3**.
- **Phase 3** (Viewer) is required for **Phase 4** (Search) and **Phase 5** (Context).
- **Phase 6** (Content) can be done in parallel with Phase 4/5 once the JSON structure is finalized in Phase 1.

## Implementation Strategy

1.  **Skeleton**: Build the Models and Service interfaces first (Phase 1).
2.  **Engine**: Implement the Loader and Template Engine (Phase 2).
3.  **Viewer**: Create the Form and get the "Getting Started" page rendering (Phase 3).
4.  **Interactivity**: Add Search and Context links (Phase 4 & 5).
5.  **Content**: Fill out the remaining JSON files (Phase 6).
6.  **Polish**: Finalize theming and shortcuts (Phase 7).
