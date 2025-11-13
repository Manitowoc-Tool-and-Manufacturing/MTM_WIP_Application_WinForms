# Feature Specification: Universal Suggestion System for TextBox Inputs

**Feature Branch**: `001-suggestion-textbox`  
**Created**: November 12, 2025  
**Status**: Draft  
**Input**: User description: "Universal Suggestion System for TextBox Inputs"

## Executive Summary

Implement a universal suggestion/autocomplete system for text input fields that provides intelligent filtering and selection of master data values. This system will work with any database table or data source (parts, operations, locations, customers, users, etc.) and provide consistent user experience across all forms.

**Business Value**: Reduces data entry errors by 50%, accelerates data entry by 30%, improves data quality through validated master data selection, and provides universal reusability across all forms.

**Problem Statement**: Users currently must manually type exact values for part numbers, operations, locations, and other master data, leading to typos, validation errors, and slow data entry. The lack of autocomplete and wildcard search capabilities results in inconsistent user experience and frequent data entry mistakes.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Part Number Entry with Autocomplete (Priority: P1)

As a shop floor user, I want part number suggestions as I type so that I can quickly select the correct part without typing the full number and avoid typos.

**Why this priority**: Part number entry is the most frequent operation in the application, used dozens of times daily by every shop floor user. Errors here directly impact inventory accuracy and production tracking.

**Independent Test**: Can be fully tested by typing partial part numbers in the Inventory tab, observing filtered suggestions, and verifying correct part selection. Delivers immediate value by reducing part entry time and errors.

**Acceptance Scenarios**:

1. **Given** user is on Inventory tab with part field focused, **When** user types "R-" and tabs away, **Then** overlay displays all part numbers starting with "R-" (filtered, sorted, maximum 100 results)
2. **Given** suggestion overlay is displayed with 23 matches, **When** user presses Down arrow twice and presses Enter, **Then** selected part number fills the field and focus moves to next field (Operation)
3. **Given** user types exact part number "R-ABC-01" that exists, **When** user tabs away, **Then** no overlay displays (exact match), field keeps value, focus moves to next field
4. **Given** user types invalid part number "INVALID-99", **When** user tabs away and no matches found, **Then** field is cleared and user-friendly message shown
5. **Given** user types wildcard pattern "R-%-01", **When** user tabs away, **Then** overlay shows all R-series parts ending in -01 (e.g., "R-ABC-01", "R-XYZ-01")

---

### User Story 2 - Keyboard Navigation for Power Users (Priority: P1)

As a power user who prefers keyboard-only workflows, I want full keyboard navigation of suggestions so that I can maintain data entry speed without switching to mouse.

**Why this priority**: Shop floor users often enter data while standing or with limited mouse access. Keyboard-only operation is essential for productivity and ergonomics.

**Independent Test**: Can be fully tested by entering data using only keyboard (Tab, Arrow keys, Enter, Escape) across all input fields. Delivers value by enabling efficient keyboard-only workflows.

**Acceptance Scenarios**:

1. **Given** suggestion overlay is displayed, **When** user presses Down arrow, **Then** next item is highlighted
2. **Given** suggestion overlay is displayed, **When** user presses Up arrow, **Then** previous item is highlighted
3. **Given** suggestion overlay is displayed, **When** user presses Home, **Then** first item is highlighted
4. **Given** suggestion overlay is displayed, **When** user presses End, **Then** last item is highlighted
5. **Given** suggestion overlay is displayed with item highlighted, **When** user presses Enter, **Then** item is selected, field is filled, overlay closes, focus moves to next field
6. **Given** suggestion overlay is displayed, **When** user presses Escape, **Then** overlay closes, original input is preserved, focus stays on current field

---

### User Story 3 - Location Entry with Wildcard Search (Priority: P2)

As a warehouse operator, I want to search locations using wildcard patterns so that I can find locations without knowing exact codes (e.g., "SHOP-%-A" finds all shop locations ending with A).

**Why this priority**: Warehouse has hundreds of location codes with complex naming patterns. Wildcard search enables users to find locations by pattern rather than memorizing exact codes.

**Independent Test**: Can be fully tested by typing wildcard patterns in location fields on Transfer tab and verifying pattern matching works correctly. Delivers value by enabling flexible location lookup.

**Acceptance Scenarios**:

1. **Given** user is on Transfer tab with location field focused, **When** user types "SHOP-%" and tabs away, **Then** overlay shows all locations starting with "SHOP-"
2. **Given** user types pattern "%-A", **When** user tabs away, **Then** overlay shows all locations ending with "A"
3. **Given** user types complex pattern "SHOP-%-A", **When** user tabs away, **Then** overlay shows locations matching pattern (e.g., "SHOP-123-A", "SHOP-456-A")
4. **Given** wildcard pattern returns no matches, **When** user tabs away, **Then** field is cleared and "No locations matching pattern" message shown

---

### User Story 4 - Operation Selection Across All Tabs (Priority: P2)

As an inventory manager, I want consistent operation selection on all tabs (Inventory, Transfer, Remove) so that data entry is uniform and efficient regardless of which tab I'm using.

**Why this priority**: Operations are entered on multiple tabs with different contexts. Consistent behavior reduces cognitive load and training time.

**Independent Test**: Can be fully tested by entering operations on Inventory, Transfer, and Remove tabs and verifying identical suggestion behavior. Delivers value through consistent user experience.

**Acceptance Scenarios**:

1. **Given** user is on any tab with operation field, **When** user enters partial operation code, **Then** suggestions are filtered and displayed consistently across all tabs
2. **Given** user is on Inventory tab, **When** user selects operation from suggestions, **Then** behavior matches Transfer and Remove tabs (field filled, focus moved)
3. **Given** operation suggestions are displayed on any tab, **When** user navigates with keyboard, **Then** keyboard shortcuts work identically across all tabs

---

### User Story 5 - Mouse Interaction for Casual Users (Priority: P3)

As an occasional user, I want to use mouse clicks to select suggestions so that I can use familiar point-and-click interaction without learning keyboard shortcuts.

**Why this priority**: Administrators and office staff who use the system less frequently prefer mouse interaction. This broadens accessibility without impacting keyboard users.

**Independent Test**: Can be fully tested using only mouse (clicking, double-clicking) to interact with suggestions. Delivers value by supporting alternative interaction style.

**Acceptance Scenarios**:

1. **Given** suggestion overlay is displayed, **When** user single-clicks an item, **Then** item is highlighted (not selected)
2. **Given** suggestion overlay is displayed, **When** user double-clicks an item, **Then** item is selected, field is filled, overlay closes, focus moves to next field
3. **Given** suggestion overlay is displayed, **When** user clicks outside overlay, **Then** overlay closes (light dismiss), original input preserved, focus stays on field

---

### User Story 6 - Customer Name Entry in Part Management (Priority: P3)

As an administrator, I want customer name suggestions when adding parts so that customer data is consistent and validated across all parts.

**Why this priority**: Customer names are entered only during part setup, not daily operations. However, consistency is important for reporting and data integrity.

**Independent Test**: Can be fully tested by adding new parts in Settings → Part Management and verifying customer suggestions work. Delivers value through improved data consistency.

**Acceptance Scenarios**:

1. **Given** admin is adding new part in Settings, **When** admin types partial customer name (e.g., "Acme"), **Then** overlay shows matching customers from master data (e.g., "Acme Corp", "Acme Industries")
2. **Given** customer suggestions are displayed, **When** admin selects customer, **Then** customer field is filled with validated name from master data
3. **Given** admin types customer name not in master data, **When** admin tabs away, **Then** field is cleared and message indicates customer must be from master list

---

### Edge Cases

- **Empty input**: When user tabs away from empty field, no overlay is shown (no suggestions needed)
- **Large datasets**: When master data contains 10,000+ items, system limits suggestions to first 100 matches and shows "Showing 100 of [total] matches" indicator
- **Slow data sources**: When data loading takes >200ms, loading indicator is displayed to provide feedback
- **Special characters in wildcards**: When user types invalid regex characters (e.g., "[", "*"), system treats them as literal characters in search
- **Multi-monitor setup**: When overlay would appear off-screen on secondary monitor, system positions it on-screen automatically
- **High DPI scaling**: When display scaling is 125%, 150%, or 200%, overlay renders correctly with proper sizing
- **Theme switching**: When user switches from light to dark theme while overlay is visible, overlay adapts to new theme immediately
- **No matches with exact input**: When user types value that doesn't match but is exact length of valid values, field is still cleared (follows MTM pattern of strict validation)
- **Focus conflicts**: When another control programmatically requests focus while overlay is open, overlay closes gracefully
- **Rapid tab navigation**: When user tabs through fields quickly, overlays don't stack or interfere with each other

## Requirements *(mandatory)*

### Scope & Objectives

**Primary Objective**: Replace ALL existing ComboBox controls that currently use master data tables generated at startup with the new suggestion TextBox system. This is a complete UI paradigm shift, not a phased enhancement.

**Scope**: All 24 form/control fields identified in the Integration Points table will be converted simultaneously in a single release. This ensures consistent user experience across the entire application and eliminates the hybrid ComboBox/SuggestionTextBox state.

**Explicitly In Scope**:
- Master data ComboBoxes: Parts, Operations, Locations, Customers, Users, Roles, Item Types, Themes
- Any ComboBox populated from database tables (md_*, usr_*, sys_* tables)
- Any ComboBox with >20 items requiring scrolling

**Explicitly Out of Scope**:
- Fixed enum ComboBoxes with 3-10 options (e.g., Shift: Day/Night/Swing, Status: Active/Inactive, Priority: Low/Medium/High)
- Hard-coded option lists that don't query database tables
- Boolean toggles or Yes/No selections

**Out of Scope for v1**:
- Multi-column suggestions (showing part number + description in overlay)
- Recent selections history or favorites
- Voice input integration
- Touch-optimized mode for tablets
- Localization/internationalization of suggestion text
- Advanced accessibility features (screen readers, WCAG compliance) - standard WinForms behavior only, defer to future

### Functional Requirements

#### Core Functionality

- **FR-001**: System MUST support any master data table as suggestion source (parts, operations, locations, customers, item types, users, roles, etc.)
- **FR-002**: System MUST allow custom data providers via delegate/function pattern (both database queries and in-memory lists)
- **FR-003**: System MUST filter suggestions based on user input using case-insensitive substring matching
- **FR-004**: System MUST support wildcard patterns using `%` symbol (e.g., "R-%" matches "R-ABC", "R-XYZ")
- **FR-005**: System MUST sort filtered results by relevance (shortest match first, then alphabetically)
- **FR-006**: System MUST limit results to configurable maximum (default 100 items)
- **FR-007**: System MUST show "No matches found" message when filter returns empty results
- **FR-008**: System MUST clear field when user enters invalid value with no matches (MTM validation pattern)

#### User Interaction

- **FR-009**: System MUST trigger suggestion display when user exits field (LostFocus event), NOT on every keystroke
- **FR-010**: System MUST NOT show overlay for exact matches (field value exactly matches one suggestion)
- **FR-011**: System MUST NOT show overlay for empty input
- **FR-012**: System MUST show overlay positioned near input field (below if space available, above if not)
- **FR-013**: System MUST support keyboard navigation with Arrow keys (Up/Down), Enter (select), Escape (cancel), Home/End (first/last)
- **FR-014**: System MUST support mouse interaction with single click (highlight), double click (select), click outside (cancel)
- **FR-015**: System MUST move focus to next field in tab order after selection
- **FR-016**: System MUST keep focus on current field if user cancels (Escape or click outside)

#### Visual Feedback

- **FR-017**: System MUST display count of filtered suggestions (e.g., "23 matches found")
- **FR-018**: System MUST highlight currently selected item in list with distinct visual style
- **FR-019**: System MUST show scrollbar when suggestions exceed visible area
- **FR-020**: System MUST apply current application theme (light/dark) to overlay
- **FR-021**: System MUST show loading indicator when data loading exceeds 100ms

#### Performance

- **FR-022**: System MUST filter 1,000+ items without UI lag (target <50ms)
- **FR-023**: System MUST display overlay within 100ms of trigger event
- **FR-024**: System MUST use async operations for data loading to prevent UI blocking
- **FR-025**: System MUST limit memory usage per control to under 10MB

#### Integration

- **FR-026**: System MUST work correctly on multi-monitor setups (overlay positioned on correct screen)
- **FR-027**: System MUST work correctly at all DPI scaling levels (100%, 125%, 150%, 200%)
- **FR-028**: System MUST integrate with existing ThemedForm system for visual consistency
- **FR-029**: System MUST use Service_ErrorHandler for all exceptions (NO MessageBox.Show)
- **FR-030**: System MUST use LoggingUtility for event tracking (structured CSV logging)
- **FR-031**: System MUST implement IDisposable pattern for proper resource cleanup
- **FR-032**: System MUST validate all user input before database queries (parameterized queries, no SQL injection)

### Key Entities

- **SuggestionTextBox**: Enhanced text input control that monitors user interaction, calls suggestion provider, displays overlay, handles selection/cancellation, manages focus flow
- **SuggestionProvider**: Abstract data source interface that returns list of suggestion strings, supports both sync and async operations, allows caching
- **SuggestionOverlay**: Popup form that displays filtered suggestions, handles keyboard/mouse navigation, applies theme styling, positions relative to parent control
- **FilterService**: Service that applies case-insensitive substring matching, converts wildcard patterns to regex, sorts results by relevance, limits to maximum count
- **SuggestionConfig**: Configuration containing data source function, maximum results (default 100), wildcard enable flag (default true), exact match behavior (default false), cache duration (optional)

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Users can enter part numbers in under 5 seconds (down from average 12 seconds with manual typing)
- **SC-002**: Data entry error rate for master data fields reduces by 50% (from current ~8% to ~4%)
- **SC-003**: System displays suggestion overlay in under 100ms for 95% of queries
- **SC-004**: System filters 1,000 items in under 50ms without UI stutter
- **SC-005**: 80% of users report preferring suggestion system over manual typing (post-deployment survey)
- **SC-006**: New users can complete data entry training 30% faster with suggestion system enabled
- **SC-007**: Support tickets related to "invalid data entered" reduce by 40% within first month
- **SC-008**: 90% of eligible text input fields across all forms use suggestion system within 3 months of deployment
- **SC-009**: System maintains under 10MB memory per control instance during normal operation
- **SC-010**: Zero crashes or exceptions related to suggestion system in production (proper error handling)

## Clarifications

### Session 2025-11-12

Pre-answered design decisions from detailed specification review:

- Q: Should suggestions be cached at the component level? → A: No caching at component level; master data is managed at application startup and caching happens in DAO layer or application memory
- Q: Should overlay be modal (blocks parent) or modeless (allows interaction)? → A: Modal - blocks parent form for simpler focus management and prevents data corruption
- Q: Should Tab key accept first suggestion or cancel overlay? → A: Tab cancels (normal behavior), Enter accepts - prevents accidental selection and matches user expectations
- Q: Should data provider support async operations? → A: Async for all data provider operations - prevents UI blocking for large datasets, future-proof design, consistent with existing DAO patterns
- Q: How to detect when master data changes for cache invalidation? → A: Cache loaded on application startup; master data tables (md_*) populated at app startup, refresh occurs on app restart
- Q: Should suggestion system be deployed all at once or phased by priority? → A: Deploy to all 24 fields simultaneously - Goal is to completely replace ALL ComboBoxes that currently use master data tables generated at startup
- Q: Should ALL ComboBoxes be replaced or only master data ComboBoxes? → A: Replace only master data ComboBoxes (parts, operations, locations, customers, users, roles); preserve ComboBoxes for fixed enums/status values (3-10 options like Shift: Day/Night)
- Q: What level of accessibility compliance is required? → A: No specific accessibility requirements - standard WinForms behavior only, defer advanced accessibility to future enhancement
- Q: Should design tradeoff rationale be documented in spec? → A: No documentation needed in spec - implementation details and rationale can be in code comments only
- Q: Should suggestion text support multiple languages? → A: English-only for v1 - simplest approach, defer localization to future if international expansion occurs

## Data Model *(mandatory)*

### Master Data Sources

The suggestion system works with any database table that returns string columns. The following master data tables are available in the `mtm_wip_application_winforms` database:

#### Core Master Data Tables

**md_part_ids** - Part master data
- **PartID** (VARCHAR(300)) - Primary suggestion field for part numbers
- **Customer** (VARCHAR(300)) - Suggestion field for customer names
- **Description** (VARCHAR(300)) - Suggestion field for descriptions
- **ItemType** (VARCHAR(100)) - Links to md_item_types, suggestion field
- **IssuedBy** (VARCHAR(100)) - Audit field (created by user)

**md_operation_numbers** - Operation master data
- **Operation** (VARCHAR(100)) - Primary suggestion field for operations
- **IssuedBy** (VARCHAR(100)) - Audit field (created by user)

**md_locations** - Location master data
- **Location** (VARCHAR(100)) - Primary suggestion field for locations
- **Building** (VARCHAR(100)) - Suggestion field for building names
- **IssuedBy** (VARCHAR(100)) - Audit field (created by user)

**md_item_types** - Item type master data
- **ItemType** (VARCHAR(100)) - Primary suggestion field for item types (WIP, FG, RM, etc.)
- **IssuedBy** (VARCHAR(100)) - Audit field (created by user)

#### System and User Tables

**usr_users** - User master data
- **User** (VARCHAR(100)) - Primary suggestion field for usernames
- **Full Name** (VARCHAR(200)) - Suggestion field for full names
- **Shift** (VARCHAR(50)) - Suggestion field for shift assignments
- **Theme_Name** (VARCHAR(50)) - Suggestion field for theme selection

**sys_roles** - Role master data
- **RoleName** (VARCHAR(50)) - Primary suggestion field for role assignments
- **Description** (VARCHAR(255)) - Role descriptions (informational)
- **Permissions** (VARCHAR(1000)) - Permission strings (not for suggestions)
- **IsSystem** (TINYINT) - System vs custom roles (filter criteria)

### Data Extensibility

The suggestion system is **data-agnostic** by design. Any master data table with VARCHAR columns can provide suggestions without modifying the suggestion component. This includes:
- Core master tables (parts, operations, locations, item types)
- User and system tables (users, roles)
- Custom business-specific tables (any new master data)
- In-memory lists (computed or derived values)
- External API responses (future enhancement)

## Technical Constraints *(mandatory)*

### Technology Stack
- **Platform**: .NET 8.0 Windows Forms
- **Database**: MySQL 5.7.24 (LEGACY - no 8.0+ features allowed)
- **UI Framework**: WinForms with custom theme system (ThemedForm/ThemedUserControl)
- **Patterns**: Observer (events), Strategy (data providers), Factory (overlay creation)

### Database Constraints (MySQL 5.7.24 Limitations)
- ❌ **NO JSON functions** - MySQL 5.7 does not support JSON_EXTRACT, JSON_CONTAINS, etc.
- ❌ **NO Common Table Expressions (CTEs)** - WITH clauses not available
- ❌ **NO Window functions** - ROW_NUMBER(), RANK(), LAG(), LEAD() not supported
- ❌ **NO CHECK constraints** - Use triggers or application-level validation
- ✅ **Simple SELECT queries only** - Straightforward WHERE, LIKE, ORDER BY patterns
- ✅ **Parameterized queries mandatory** - Always use MySqlParameter for user input

### Performance Constraints
- **Overlay display**: Must appear within 100ms of trigger event (95% of queries)
- **Filtering**: Must filter 1,000 items in under 50ms without UI stutter
- **Memory**: Maximum 10MB per control instance during normal operation
- **UI responsiveness**: No blocking - all data operations must be async

### Integration Constraints
- **MUST** inherit from ThemedForm for overlay (theme system integration)
- **MUST** use Service_ErrorHandler for all exceptions (NO MessageBox.Show)
- **MUST** use LoggingUtility for event tracking (structured CSV logging)
- **MUST** follow DAO → Service → Form layering (no direct database access in UI)
- **MUST** implement IDisposable pattern for proper resource cleanup

## Integration Points *(mandatory)*

### Forms Requiring Suggestion Support

The following forms and controls require suggestion system integration, prioritized by usage frequency and impact:

| Form/Control | File Path | Field | Data Source | Priority |
| ------------ | --------- | ----- | ----------- | -------- |
| Control_InventoryTab | Controls/MainForm/Control_InventoryTab.cs | Part Number | md_part_ids.PartID | Critical |
| Control_InventoryTab | Controls/MainForm/Control_InventoryTab.cs | Operation | md_operation_numbers.Operation | Critical |
| Control_InventoryTab | Controls/MainForm/Control_InventoryTab.cs | Customer | md_part_ids.Customer | High |
| Control_InventoryTab | Controls/MainForm/Control_InventoryTab.cs | Description | md_part_ids.Description | Medium |
| Control_AdvancedInventory | Controls/MainForm/Control_AdvancedInventory.cs | Part Number | md_part_ids.PartID | Critical |
| Control_AdvancedInventory | Controls/MainForm/Control_AdvancedInventory.cs | Operation | md_operation_numbers.Operation | Critical |
| Control_TransferTab | Controls/MainForm/Control_TransferTab.cs | Part Number | md_part_ids.PartID | Critical |
| Control_TransferTab | Controls/MainForm/Control_TransferTab.cs | From Location | md_locations.Location | Critical |
| Control_TransferTab | Controls/MainForm/Control_TransferTab.cs | To Location | md_locations.Location | Critical |
| Control_TransferTab | Controls/MainForm/Control_TransferTab.cs | Operation | md_operation_numbers.Operation | Critical |
| Control_TransferTab | Controls/MainForm/Control_TransferTab.cs | Building | md_locations.Building | Medium |
| Control_RemoveTab | Controls/MainForm/Control_RemoveTab.cs | Part Number | md_part_ids.PartID | Critical |
| Control_RemoveTab | Controls/MainForm/Control_RemoveTab.cs | Operation | md_operation_numbers.Operation | Critical |
| Control_AdvancedRemove | Controls/MainForm/Control_AdvancedRemove.cs | Part Number | md_part_ids.PartID | Critical |
| Control_AdvancedRemove | Controls/MainForm/Control_AdvancedRemove.cs | Operation | md_operation_numbers.Operation | Critical |
| SettingsForm | Forms/Settings/SettingsForm.cs | Customer (Part Mgmt) | md_part_ids.Customer | High |
| SettingsForm | Forms/Settings/SettingsForm.cs | Item Type (Part Mgmt) | md_item_types.ItemType | High |
| SettingsForm | Forms/Settings/SettingsForm.cs | Username (User Mgmt) | usr_users.User | Medium |
| SettingsForm | Forms/Settings/SettingsForm.cs | Full Name (User Mgmt) | usr_users.`Full Name` | Medium |
| SettingsForm | Forms/Settings/SettingsForm.cs | Role Name (Role Mgmt) | sys_roles.RoleName | Medium |
| SettingsForm | Forms/Settings/SettingsForm.cs | Shift (User Mgmt) | usr_users.Shift | Low |
| SettingsForm | Forms/Settings/SettingsForm.cs | Theme Name (Theme Settings) | usr_users.Theme_Name | Low |
| Form_QuickButtonEdit | Forms/Shared/Form_QuickButtonEdit.cs | Part ID | md_part_ids.PartID | High |
| Form_QuickButtonEdit | Forms/Shared/Form_QuickButtonEdit.cs | Operation | md_operation_numbers.Operation | High |

**Note**: The MainForm (Forms/MainForm/MainForm.cs) contains tabs that host these UserControl components. The actual data entry occurs in the UserControls listed above.

### Service Dependencies

- **Data Access**: Dao_Part, Dao_Operation, Dao_Location, Dao_ItemType, Dao_User, Dao_System (for roles)
- **Error Handling**: Service_ErrorHandler for all exceptions
- **Logging**: LoggingUtility for event tracking
- **Theming**: Core_Themes for visual consistency
