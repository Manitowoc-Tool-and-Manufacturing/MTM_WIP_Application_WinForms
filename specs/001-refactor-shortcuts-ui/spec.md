# Feature Specification: Refactor Shortcuts UI

**Feature Branch**: `001-refactor-shortcuts-ui`
**Created**: 2025-11-23
**Status**: Draft
**Input**: User description: "Refactor Control_Shortcuts into a modern card-based UI, ensure all shortcuts flow through Service_Shortcut, and align default shortcuts."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - View and Manage Shortcuts (Priority: P1)

As a user, I want to view all application shortcuts grouped by category and customize them so that I can optimize my workflow.

**Why this priority**: This is the core functionality of the refactor, replacing the legacy interface with a usable, modern design.

**Independent Test**: Can be tested by opening the Shortcuts settings, expanding categories, and verifying all expected shortcuts are listed with correct current keys.

**Acceptance Scenarios**:

1. **Given** the Shortcuts settings page is open, **When** the user views the list, **Then** shortcuts are displayed in collapsible cards grouped by feature (Inventory, Remove, etc.).
2. **Given** a shortcut card, **When** the user clicks "Change", **Then** a modal dialog appears to capture the new key combination.
3. **Given** the modal dialog, **When** the user presses a valid key combination and confirms, **Then** the shortcut is updated and saved.
4. **Given** a conflict (duplicate key), **When** the user tries to save, **Then** an error message is displayed and the change is rejected.

---

### User Story 2 - QuickButton Exclusivity (Priority: P1)

As a system administrator, I want to ensure QuickButton shortcuts (Alt+0 through Alt+9) are reserved so that they don't conflict with other application functions.

**Why this priority**: Prevents conflicts that could trigger unintended actions or block QuickButton usage.

**Independent Test**: Try to assign `Alt+1` to a non-QuickButton shortcut and verify it is rejected.

**Acceptance Scenarios**:

1. **Given** a non-QuickButton shortcut, **When** the user attempts to assign `Alt+1` (or any reserved QuickButton key), **Then** the system rejects the assignment with a specific error message.
2. **Given** the QuickButtons configuration, **When** the user assigns a valid key (e.g., `Alt+1` to QuickButton 1), **Then** it is accepted.

---

### User Story 3 - UI Modernization and Consistency (Priority: P2)

As a user, I want the Shortcuts settings to look and behave consistently with other settings pages (collapsible cards, headers) for a seamless experience.

**Why this priority**: Ensures UI consistency and usability across the application.

**Independent Test**: Visually inspect the Shortcuts page and compare headers/cards with other settings pages.

**Acceptance Scenarios**:

1. **Given** the Shortcuts page, **When** the user interacts with a category header, **Then** the card expands or collapses with a smooth animation.
2. **Given** the page content, **When** the content exceeds the view height, **Then** a vertical scrollbar appears.
3. **Given** the bottom of the page, **When** scrolling, **Then** the "Back to Home" button remains accessible.

### Edge Cases

- **Configuration Failure**: If the shortcut configuration cannot be loaded, the system should fall back to default values and notify the user.
- **OS Reserved Keys**: If a user attempts to assign a key combination reserved by the OS (e.g., Ctrl+Alt+Del), the system should handle the input gracefully (likely not capturing it or showing a warning).
- **Small Screens**: On screens with limited vertical height, the scrollbar must allow access to all categories and the "Back" button.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST display shortcuts in a single-column, vertically scrollable stack of collapsible cards.
- **FR-002**: Each card MUST represent a logical category of shortcuts (e.g., Inventory, General).
- **FR-003**: The system MUST use the centralized shortcut service for all shortcut retrieval, updates, and persistence.
- **FR-004**: The system MUST provide a modal dialog to capture and validate new shortcut key combinations.
- **FR-005**: The system MUST prevent assignment of duplicate shortcuts within the same scope (or globally if applicable).
- **FR-006**: The system MUST strictly reserve `Alt+0` through `Alt+9` for QuickButtons and reject their assignment to other functions.
- **FR-007**: The UI MUST update immediately to reflect changed shortcuts without requiring an application restart.
- **FR-008**: The system MUST implement a reusable collapsible card component for the UI.
- **FR-009**: The system MUST align default shortcuts in the configuration with the codebase audit results.

### Key Entities *(include if feature involves data)*

- **Shortcut**: Represents a key binding. Attributes: Name (ID), Key Code, Category, Display Text.
- **Shortcut Service**: Manages the loading, saving, and validation of shortcuts.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Legacy grid interface is completely removed and replaced by the new card-based UI.
- **SC-002**: All application shortcuts are managed through the centralized service, with no hardcoded key bindings remaining in the codebase.
- **SC-003**: User can successfully rebind a shortcut and use it immediately.
- **SC-004**: Attempting to bind a duplicate key triggers a validation error 100% of the time.
- **SC-005**: Reserved keys (e.g., Alt+0-9) cannot be assigned to non-QuickButton features.
