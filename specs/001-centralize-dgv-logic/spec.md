# Feature Specification: Centralize DataGridView Logic

**Feature Branch**: 001-centralize-dgv-logic  
**Created**: 2025-11-22  
**Status**: Draft  
**Input**: User description: "centralize dgv logic into a service"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Developer Maintenance (Priority: P1)

As a developer, I want to manage DataGridView logic (theming, configuration, printing) in a single service so that I can make global updates easily and reduce code duplication.

**Why this priority**: This is the primary goal of the refactoring effort—to improve code maintainability and reduce technical debt.

**Independent Test**: Verify that changes made to Service_DataGridView (e.g., changing a default color or print dialog title) are reflected across all refactored controls without modifying the controls themselves.

**Acceptance Scenarios**:

1. **Given** the new Service_DataGridView is implemented, **When** I inspect Control_TransferTab.cs, **Then** I should see calls to the service instead of duplicated logic for column setup and theming.
2. **Given** the refactoring is complete, **When** I run the application, **Then** all DataGridViews should look and behave exactly as they did before (no regressions).

---

### User Story 2 - Consistent User Experience (Priority: P2)

As a user, I want the inventory grids to behave consistently (sorting, color coding, printing) across different tabs so that I have a predictable experience.

**Why this priority**: Ensures that the refactoring doesn't negatively impact the user and enforces consistency.

**Independent Test**: Compare the behavior of the "Transfer" tab and "Remove" tab side-by-side.

**Acceptance Scenarios**:

1. **Given** I am on the Transfer tab, **When** I search for a color-coded part, **Then** the rows should be colored according to the standard priority rules.
2. **Given** I am on the Remove tab, **When** I click "Print", **Then** the print dialog should appear with the same validation logic (e.g., warning if no rows) as the Transfer tab.

### Edge Cases

- What happens when a grid does not have a "ColorCode" column? (Service should handle gracefully).
- What happens when user settings for a grid are missing or corrupted? (Service should fall back to defaults).
- What happens when printing is attempted on an empty grid? (Service should show the standard warning).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST provide a static Service_DataGridView class in the Services namespace.
- **FR-002**: Service_DataGridView MUST implement ConfigureColumns to handle column visibility, ordering, and header renaming based on a provided list.
- **FR-003**: Service_DataGridView MUST implement ApplyStandardSettingsAsync to apply standard theming and load user-specific grid settings (JSON) from the database.
- **FR-004**: Service_DataGridView MUST implement ApplyColorCoding to apply background colors to rows based on the "ColorCode" column, using the centralized color list.
- **FR-005**: Service_DataGridView MUST implement SortByColorPriority to sort DataTables by ColorCode (custom priority) and then Location.
- **FR-006**: Service_DataGridView MUST implement PrintGridAsync to handle printing, including validation for empty grids and error handling.
- **FR-007**: Control_TransferTab MUST be refactored to use Service_DataGridView for all grid operations.
- **FR-008**: Control_RemoveTab MUST be refactored to use Service_DataGridView for all grid operations.
- **FR-009**: Control_AdvancedRemove MUST be refactored to use Service_DataGridView for column setup and printing.
- **FR-010**: TransactionGridControl MUST be refactored to use Service_DataGridView for printing and potentially row coloring (if applicable).

### Key Entities *(include if feature involves data)*

- **Service_DataGridView**: The new service class containing the centralized logic.
- **GridSettings**: The JSON structure used for saving/loading user column preferences (existing entity, but managed by the service).

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Code duplication for DataGridView logic is reduced by at least 40% in target controls.
- **SC-002**: All 4 target controls (Transfer, Remove, AdvancedRemove, TransactionGrid) successfully compile and run using the new service.
- **SC-003**: Printing functionality works identical to the previous implementation across all 4 controls.
- **SC-004**: Color-coded parts display correct row colors in Transfer and Remove tabs.
