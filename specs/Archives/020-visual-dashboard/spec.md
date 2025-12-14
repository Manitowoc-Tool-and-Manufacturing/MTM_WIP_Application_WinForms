# Feature Specification: Infor Visual Dashboard

**Feature Branch**: `014-visual-dashboard`
**Created**: 2025-11-26
**Status**: Draft
**Input**: User description: "Architect a solution that allows this WinForms application to safely query the Infor Visual database for specific metrics. The implementation must be Read-Only, secure, and presented in a stunning, highly functional interface."

## 1. User Scenarios & Testing *(mandatory)*

### User Story 1 - Secure Access to Dashboard (Priority: P1)
As a user with Infor Visual credentials, I want to access the dashboard so that I can view ERP data without logging into the full ERP client.
*   **Why this priority**: Security is paramount. Only authorized users should access this sensitive data.
*   **Independent Test**: Can be tested by attempting access with and without configured Visual credentials.
*   **Acceptance Scenarios**:
    1.  **Given** a user has `VisualUsername` and `VisualPassword` configured, **When** they open the dashboard, **Then** the dashboard loads and connects successfully.
    2.  **Given** a user has NO `VisualUsername` or `VisualPassword` configured, **When** they attempt to open the dashboard, **Then** access is denied with a clear message or the option is disabled.

### User Story 2 - View Inventory & Operations Data (Priority: P1)
As a material handler or manager, I want to view real-time Inventory, Receiving, and Shipping data so that I can make informed decisions.
*   **Why this priority**: This is the core value proposition of the dashboard.
*   **Independent Test**: Can be tested by selecting categories and verifying data matches the ERP (read-only).
*   **Acceptance Scenarios**:
    1.  **Given** the dashboard is open, **When** I select "Inventory" from the sidebar, **Then** the main view updates to show current inventory metrics in a styled grid.
    2.  **Given** the dashboard is open, **When** I select "Receiving" or "Shipping", **Then** the view updates to show the respective data.
    3.  **Given** data is loading, **When** I wait, **Then** a clear loading state is shown.

### User Story 3 - Modern & Responsive Interface (Priority: P2)
As a user, I want a modern, clean interface that is easy to navigate and read, so that I can work efficiently.
*   **Why this priority**: The legacy forms are "clunky"; a modern UI improves user satisfaction and adoption.
*   **Independent Test**: Can be tested by resizing the window and verifying layout responsiveness and visual consistency.
*   **Acceptance Scenarios**:
    1.  **Given** the dashboard is open, **When** I resize the window, **Then** the sidebar and data grids resize gracefully without content clipping.
    2.  **Given** the dashboard is displayed, **When** I view the controls, **Then** they use a flat design, consistent color palette, and proper spacing (not default WinForms gray).

### Edge Cases
*   **Database Offline**: System should show a friendly error message, not crash.
*   **Expired Password**: System should prompt the user to update credentials in `Settings -> User Management`.
*   **No Data Returned**: System **MUST** show an "Empty State" message/graphic, using a new shared `Control_EmptyState` (refactored from `Control_RemoveTab`) for consistency.

---

## 2. Requirements *(mandatory)*

### Functional Requirements
*   **FR-001**: System MUST provide a new `InforVisualDashboard` form accessible from the main menu via a new `Visual` Option in `MainForm.cs` Menu Bar. Place it to the right of `Edit`, pushing other content to the right.
*   **FR-002**: System MUST restrict access to users with valid `VisualUsername` and `VisualPassword` stored in `Settings -> User Management`.
*   **FR-003**: System MUST connect to the Infor Visual SQL Server database using the user's personal credentials (no shared service account). Server and Database connection details MUST be stored in `App.config`.
*   **FR-004**: System MUST provide read-only access to the following data categories:
    1.  Inventory
    2.  Receiving
    3.  Shipping
    4.  Inventory Auditing
    5.  Die & Tool Discovery
    6.  Material Handler Analytics (General)
    7.  Material Handler Analytics (Team Performance)
*   **FR-005**: System MUST use pre-defined, embedded SQL queries (no dynamic SQL generation) to prevent injection and ensure performance.
*   **FR-006**: UI MUST use a sidebar navigation layout for switching between data categories.
*   **FR-007**: UI MUST implement a modern design language (flat styles, custom colors, padding) distinct from standard legacy forms, but MUST inherit from `ThemedForm` to maintain application consistency.
*   **FR-008**: System MUST NOT allow any write operations (INSERT, UPDATE, DELETE) to the Infor Visual database.
*   **FR-009**: System MUST handle database connection timeouts and errors gracefully with user-friendly messages. Data refresh MUST be triggered manually by the user (no auto-refresh).
*   **FR-010**: System MUST allow users to **Export to CSV** the data currently displayed in the active grid.
*   **FR-011**: Data Grids MUST support **Sorting** (by clicking headers) and basic **Filtering** to allow users to slice the data.

### Key Entities
*   **Visual Credentials**: The username/password pair used to authenticate against the ERP database.
*   **Dashboard Category**: A logical grouping of metrics (e.g., "Inventory", "Shipping").
*   **Visual Metric**: A specific data point or list retrieved from the ERP (e.g., "Items received today").

---

## 3. AI Workflow & Prompt Assets

To ensure standardized and automated SQL generation, the following assets **MUST** be created and maintained as the "Source of Truth" for AI operations.

### Mandatory File Pairs
The developer MUST create a specific pair of Markdown files for *each* of the 7 categories (Inventory, Receiving, Shipping, Auditing, Die/Tool, Material Handler General, Material Handler Team).
*   **Format**: `[Category].instruction.md` and `[Category].prompt.md`.

### Purpose & Usage
These files are designed to be consumed by AI agents. They serve as the strict definition for how SQL is generated for this application.
*   **Scenario**: "If a developer runs a command like `/visual.create-inventory-query`, the AI must be able to load `Inventory.instruction.md` (to understand the schema/joins) and `Inventory.prompt.md` (to understand the rules/security) to generate the final SQL result."

### Content Definition
*   **`.instruction.md`**: Contains the static knowledge base (Table maps, join logic, `_BINARY` table handling).
*   **`.prompt.md`**: Contains the strict "System Prompt" (Read-Only enforcement, Security constraints, formatting rules).

### Integration Requirement
Whenever a new pair of prompt/instruction files is created for a category, the `InforVisualDashboard` form MUST be updated to include a corresponding navigation item and data view for that category.

---

## 4. Success Criteria *(mandatory)*

### Measurable Outcomes
*   **SC-001**: Dashboard loads and displays initial data within **3 seconds** on a standard network connection.
*   **SC-002**: **100%** of queries executed by the dashboard are Read-Only (verified by code review).
*   **SC-003**: UI adapts to window resizing from 1024x768 to 1920x1080 without horizontal scrollbars on the main layout (grids may scroll internally).
*   **SC-004**: Users report a "modern" look and feel compared to existing forms (qualitative feedback).

---
## 5. Clarifications

### Session 2025-11-26

- Q: Does the project currently allow `System.Data.SqlClient` references, or must we add this NuGet package? → A: Add it. We will need to add `System.Data.SqlClient` specifically for this feature.
- Q: Do we need to create a database migration for the *App's* MySQL database to store `VisualUsername` and `VisualPassword`? → A: No, these fields already exist. The Settings form already allows users to add Infor Visual Credentials when saving/editing a user.
- Q: Should the `InforVisualDashboard` be a standalone floating window or a new Tab in `MainForm`? → A: Standalone Form.
- Q: What is the exact Resource Name for the "Empty State" image? → A: `Control_AdvancedRemove_Image_NothingFound`.
- Q: Should the new form inherit from `ThemedForm` or use custom styling? → A: Inherit `ThemedForm` to maintain consistency with the application's theme system, while applying custom modern styles to specific controls.
- Q: Where should Infor Visual connection details (Server/DB) be stored? → A: Store in `App.config` to allow environment switching without recompilation.
- Q: Should the dashboard support auto-refresh or manual refresh only? → A: Manual Refresh only to prevent accidental load on the ERP database.
- Q: How should the "Empty State" asset be handled? → A: Refactor the existing image/logic into a shared `Control_EmptyState` to be used by both the new dashboard and existing forms.
- Q: **Edge Case**: What if a query returns excessive rows (e.g., 50k+)? → A: Implement a hard limit (e.g., `TOP 5000`) in the SQL generation to prevent memory issues. Pagination is not required for V1.
- Q: **Edge Case**: The Constitution forbids inline SQL. How do we reconcile this with the external database? → A: This is a strictly defined exception. SQL must be loaded from embedded resources or const strings, NEVER concatenated dynamically. It must be isolated in a specific `Service_VisualDatabase`.
- Q: **Edge Case**: What happens if the user tries to open the dashboard without credentials? → A: The menu option should be visible but clicking it should prompt a `Service_ErrorHandler` user-friendly message directing them to Settings, rather than opening a broken form.
- Q: **Edge Case**: What if the Infor Visual server is unreachable (VPN down/Network issue)? → A: Catch the `SqlException`, log it via `LoggingUtility`, and display a user-friendly "Connection Failed" state in the dashboard, rather than crashing.
