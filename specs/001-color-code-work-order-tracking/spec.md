# Feature Specification: Color Code & Work Order Tracking

**Feature Branch**: `001-color-code-work-order-tracking`  
**Created**: 2025-11-13  
**Status**: Draft  
**Input**: User description: "Add color code and work order tracking to inventory management for parts that require material handler segregation by work order. Enable users to flag specific part numbers as requiring color codes and work orders, capture this data during inventory entry, and display it in search/remove/transfer operations for better tracking and organization."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Inventory Entry with Color Codes (Priority: P1)

Material handlers need to inventory parts that require work order segregation by entering color codes and work order numbers during the inventory process. When inventorying a flagged part number, the system prompts for both color code and work order number, validates the work order format, and saves the data for future tracking.

**Why this priority**: This is the core data capture functionality. Without this, the entire feature has no value. Material handlers must be able to record color codes at the point of inventory entry to maintain work order segregation.

**Independent Test**: Can be fully tested by flagging a part number as requiring color codes, attempting to inventory it, entering color and work order data, and verifying successful save with data persistence in both inventory and transaction tables.

**Acceptance Scenarios**:

1. **Given** a part number flagged as requiring color codes, **When** user enters the part in Inventory Tab, **Then** color code and work order input fields appear dynamically
2. **Given** color code and work order fields are visible, **When** user enters a 6-digit number "054321" in work order field, **Then** system auto-formats it to "WO-054321"
3. **Given** user enters work order "WO-1234" (less than 6 digits), **When** field loses focus, **Then** system formats it to "WO-001234"
4. **Given** user enters invalid work order "ABC123" with letters, **When** field loses focus, **Then** system shows error "Invalid work order format. Enter 5-6 digit number or WO-######", clears field, and returns focus
5. **Given** all required fields filled correctly, **When** user clicks Save, **Then** inventory record saves with color code and work order data to both inv_inventory and inv_transaction tables
6. **Given** user attempts to save without entering required color code, **When** Save button clicked, **Then** system blocks save with validation error message
7. **Given** user selects "OTHER" from color dropdown, **When** selection made, **Then** dialog appears prompting for custom color name (max 15 characters)
8. **Given** user enters custom color "blueberry", **When** save confirmed, **Then** system prompts "Save this color to database for future use?" with Yes/No buttons
9. **Given** user clicks Yes to save custom color, **When** confirmed, **Then** system adds "Blueberry" (title case) to md_color_codes table

---

### User Story 2 - Search & Remove with Color Filtering (Priority: P2)

Material handlers need to view and remove inventory grouped by color code to efficiently process work orders. When searching for a part that requires color codes, the system displays color and work order columns, automatically sorts results by color then location, enabling quick identification and removal of specific work order batches.

**Why this priority**: Provides immediate value for material handlers to organize and remove inventory by work order. Builds on P1 data capture functionality.

**Independent Test**: Can be tested independently by searching for a pre-existing flagged part with color code data, verifying column display, sort order, and successful removal operations.

**Acceptance Scenarios**:

1. **Given** user searches for a part flagged as requiring color codes in Remove Tab, **When** search results load, **Then** Color and Work Order columns appear after PartID column
2. **Given** search results contain multiple colors, **When** results display, **Then** rows are automatically sorted alphabetically by Color (Black, Blue, Green, etc.) then by Location within each color
3. **Given** search results include "Unknown" color codes (legacy data), **When** sorted, **Then** "Unknown" entries appear at the end of the list
4. **Given** same part exists in same location with different colors (Red WO-012345 Qty:100, Blue WO-012346 Qty:50), **When** displayed, **Then** they appear as separate selectable rows
5. **Given** user clicks "Show All" button, **When** results load, **Then** Color and Work Order columns are hidden to reduce visual clutter
6. **Given** "Show All" returns more than 1000 records, **When** query executes, **Then** system displays warning "Query returned [X] results. This may impact performance. Continue?" with Yes/No options
7. **Given** user clicks "No" on performance warning, **When** selected, **Then** system cancels the load operation
8. **Given** user selects a row with color code data, **When** Delete button clicked, **Then** removal proceeds normally (all quantity removed, no partial removal)

---

### User Story 3 - Transfer with Color Code Display (Priority: P3)

Material handlers need to see color code and work order information when transferring parts to maintain work order integrity during location changes. When transferring a flagged part, the system displays color and work order data as read-only information, ensuring material handlers know which work order batch they're moving.

**Why this priority**: Supports operational visibility during transfers but doesn't change core transfer logic. Lower priority as transfers don't modify color/work order data.

**Independent Test**: Can be tested by transferring a flagged part with color code data between locations and verifying color/work order columns display correctly without allowing edits.

**Acceptance Scenarios**:

1. **Given** user searches for a part flagged as requiring color codes in Transfer Tab, **When** search results load, **Then** Color and Work Order columns appear after PartID column
2. **Given** color and work order columns are visible, **When** user views the data, **Then** both fields display as read-only (no editing allowed)
3. **Given** user selects a row and enters transfer details, **When** Transfer button clicked, **Then** color code and work order transfer unchanged to new location
4. **Given** multiple colors of same part at same location, **When** displayed, **Then** sorted alphabetically by Color then Location (same as Remove Tab)

---

### User Story 4 - Part Configuration Management (Priority: P2)

Administrators and users need to configure which part numbers require color code tracking by adding or editing part numbers in the Settings form. When adding a new part or editing an existing part, users can check the "Requires Color Code & Work Order" checkbox to flag the part for enhanced tracking.

**Why this priority**: Essential for system flexibility and ongoing maintenance. Allows users to adapt to new work order tracking requirements without database changes.

**Independent Test**: Can be tested by adding a new part with color code flag enabled, editing an existing part to enable/disable the flag, and verifying the flag persists and affects inventory entry behavior.

**Acceptance Scenarios**:

1. **Given** user opens Add Part ID form in Settings, **When** form loads, **Then** "Requires Color Code & Work Order" checkbox appears near Part ID input field
2. **Given** user checks "Requires Color Code & Work Order" checkbox, **When** new part is saved, **Then** RequiresColorCode flag sets to TRUE in md_part_ids table
3. **Given** user opens Edit Part ID form for existing part, **When** form loads, **Then** checkbox reflects current RequiresColorCode flag value
4. **Given** user changes RequiresColorCode flag for existing part, **When** changes saved, **Then** system flags Settings form to prompt for app restart on close
5. **Given** Settings form flagged for restart, **When** user closes Settings, **Then** prompt appears: "Changes require application restart. Restart now?"

---

### User Story 5 - Advanced Inventory Redirect (Priority: P3)

Users attempting to inventory color-coded parts via Advanced Inventory tab are redirected to the standard Inventory Tab where color code entry is supported. This ensures data integrity by preventing multi-location entry of parts requiring work order segregation.

**Why this priority**: Prevents data quality issues but lower priority as Advanced Inventory is less commonly used. Primarily a validation/guard rail feature.

**Independent Test**: Can be tested by entering a flagged part number in Advanced Inventory tab and verifying redirect prompt and navigation behavior.

**Acceptance Scenarios**:

1. **Given** user enters a part flagged as requiring color codes in Advanced Inventory tab, **When** Part ID field loses focus, **Then** validation check executes immediately
2. **Given** validation detects flagged part, **When** triggered, **Then** message box displays: "Part [PartID] requires color code entry. Use of the Inventory Tab is required for this transaction. Switch to Inventory Tab now?"
3. **Given** user clicks "Yes" on redirect prompt, **When** clicked, **Then** system navigates to Inventory Tab and pre-populates Part ID field
4. **Given** user clicks "No" on redirect prompt, **When** clicked, **Then** message box closes and Part ID field clears
5. **Given** user clicks "No", **When** Part ID cleared, **Then** focus returns to Part ID field for next entry

---

### User Story 6 - Advanced Remove with Show All (Priority: P3)

Users need consistent "Show All" functionality across all tabs. Advanced Remove tab gains a "Show All" button that loads entire inventory dataset but hides color code columns (matching standard Remove tab behavior) to reduce visual complexity.

**Why this priority**: Feature parity and user experience consistency. Lower priority as it's an enhancement to an existing workflow.

**Independent Test**: Can be tested by clicking new "Show All" button in Advanced Remove tab and verifying complete inventory loads without color code columns.

**Acceptance Scenarios**:

1. **Given** user opens Advanced Remove tab, **When** UI loads, **Then** "Show All" button is visible and enabled
2. **Given** user clicks "Show All" button, **When** clicked, **Then** system loads all inventory records regardless of filter criteria
3. **Given** "Show All" query returns more than 1000 records, **When** executed, **Then** warning displays: "Query returned [X] results. This may impact performance. Continue?"
4. **Given** "Show All" results displayed, **When** loaded, **Then** Color and Work Order columns are hidden (not displayed)
5. **Given** user searches for specific part after "Show All", **When** search executes, **Then** Color/Work Order columns appear if part is flagged

---

### Edge Cases

- **What happens when user enters "WO-WO-123456" (double prefix)?** System should strip to numeric portion only, format as "WO-123456"
- **What happens when work order exceeds 6 digits (e.g., "12345678")?** Accept as-is: "WO-12345678" (no truncation, support longer work orders)
- **What happens when duplicate custom color exists (user enters "Blueberry" but it already exists)?** System silently uses existing color code from md_color_codes, no duplicate created, no error shown
- **What happens when legacy data with "Unknown" color is edited?** User must select valid color code before saving changes (cannot re-save with "Unknown")
- **What happens when user Shift+Clicks Reset button?** System refreshes all master data caches including color codes and color-flagged parts list
- **What happens when part is un-flagged after having color code data?** Existing inventory records retain color/work order data (historical preservation), new inventory entries don't require color code
- **What happens when searching for non-flagged part in Remove/Transfer tabs?** Color and Work Order columns remain hidden (dynamic column display)
- **What happens during app startup with network/database errors?** Color code cache load failure logs error but allows app to continue (graceful degradation)

## Requirements *(mandatory)*

### Functional Requirements

#### Data Capture & Validation

- **FR-001**: System MUST add `RequiresColorCode BOOLEAN DEFAULT FALSE` column to `md_part_ids` table to flag parts requiring color code tracking
- **FR-002**: System MUST create `md_color_codes` table with predefined colors: Red, Blue, Green, Yellow, Orange, Purple, Pink, White, Black, Other, Unknown
- **FR-003**: System MUST add `ColorCode VARCHAR(50) DEFAULT 'Unknown'` and `WorkOrder VARCHAR(10) DEFAULT 'Unknown'` columns to `inv_inventory` table
- **FR-004**: System MUST add `ColorCode VARCHAR(50) DEFAULT 'Unknown'` and `WorkOrder VARCHAR(10) DEFAULT 'Unknown'` columns to `inv_transaction` table
- **FR-005**: System MUST validate work order format as 5-6 digit numbers with optional "WO-" prefix
- **FR-006**: System MUST auto-format work order input by padding numbers to 6 digits with leading zeros and adding "WO-" prefix
- **FR-007**: System MUST reject work order input containing non-numeric characters (letters, special symbols) with validation error
- **FR-008**: System MUST enforce mandatory color code and work order entry for parts flagged with RequiresColorCode=TRUE
- **FR-009**: System MUST allow "OTHER" as color code option with custom text entry (max 15 characters)
- **FR-010**: System MUST prompt user to save custom colors to database with title case formatting (first letter capitalized)

#### UI Display & Interaction

- **FR-011**: Inventory Tab MUST dynamically show SuggestionTextBox controls for Color Code and Work Order when flagged part number entered
- **FR-012**: Inventory Tab MUST hide Color Code and Work Order fields when non-flagged part number entered
- **FR-013**: Remove Tab MUST display Color and Work Order columns after PartID column when searching flagged parts
- **FR-014**: Transfer Tab MUST display Color and Work Order columns as read-only after PartID column when searching flagged parts
- **FR-015**: Advanced Remove Tab MUST include "Show All" button with same behavior as standard Remove Tab
- **FR-016**: Remove and Advanced Remove tabs MUST hide Color and Work Order columns when "Show All" is active
- **FR-017**: System MUST display performance warning "Query returned [X] results. This may impact performance. Continue?" when Show All returns >1000 records
- **FR-018**: Settings > Add Part ID form MUST include "Requires Color Code & Work Order" checkbox near Part ID input
- **FR-019**: Settings > Edit Part ID form MUST include "Requires Color Code & Work Order" checkbox reflecting current flag state
- **FR-020**: Advanced Inventory Tab MUST validate part number on field lose focus and block entry of flagged parts

#### Data Processing & Business Logic

- **FR-021**: System MUST auto-sort search results by Color (alphabetical) then Location (alphabetical) when displaying flagged parts
- **FR-022**: System MUST place "Unknown" color code entries at end of sorted results
- **FR-023**: System MUST cache color-flagged parts list in Model_Application_Variables at application startup
- **FR-024**: System MUST cache md_color_codes data in Helper_UI_ComboBoxes cache at application startup
- **FR-025**: System MUST flag Settings form for app restart when RequiresColorCode flag changed for any part
- **FR-026**: System MUST refresh color code cache when Shift+Click executed on Reset buttons in main form tabs
- **FR-027**: Advanced Inventory Tab MUST prompt redirect to Inventory Tab with message: "Part [PartID] requires color code entry. Use of the Inventory Tab is required for this transaction. Switch to Inventory Tab now?"
- **FR-028**: System MUST navigate to Inventory Tab and pre-populate Part ID when user confirms redirect from Advanced Inventory
- **FR-029**: System MUST clear Part ID field in Advanced Inventory when user declines redirect
- **FR-030**: System MUST preserve color code and work order data unchanged during transfer operations

#### Database & Stored Procedures

- **FR-031**: System MUST create stored procedure `md_color_codes_GetAll` to retrieve all color codes
- **FR-032**: System MUST create stored procedure `md_part_ids_GetAllColorCodeFlagged` to retrieve parts where RequiresColorCode=TRUE
- **FR-033**: System MUST update `inv_inventory_Add` stored procedure to accept ColorCode and WorkOrder parameters (without p_ prefix in C# calls)
- **FR-034**: System MUST update `inv_inventory_Search` stored procedure to return ColorCode and WorkOrder columns
- **FR-035**: System MUST update `inv_transaction_Add` stored procedure to accept ColorCode and WorkOrder parameters
- **FR-036**: System MUST update `inv_transactions_Search` stored procedure to return ColorCode and WorkOrder columns
- **FR-037**: System MUST create stored procedure `md_color_codes_Add` to insert custom color codes with duplicate prevention
- **FR-038**: System MUST create stored procedure `md_part_ids_UpdateColorCodeFlag` to set RequiresColorCode flag for specific part

### Key Entities

- **Color Code**: Represents the physical color tag applied to parts for work order segregation. Predefined list stored in md_color_codes table, supports custom colors via "OTHER" option. Max length 50 characters, default "Unknown" for legacy data.

- **Work Order**: Represents the manufacturing work order number associated with inventory batches. Format: WO-###### (6 digits with leading zeros). User-entered during inventory, max length 10 characters, default "Unknown" for legacy data.

- **Flagged Part**: Part number configured to require color code and work order tracking. Identified by RequiresColorCode=TRUE in md_part_ids table. Cached at startup for performance. Determines UI behavior across all inventory operations.

- **Inventory Record**: Extended to include ColorCode and WorkOrder attributes. Both fields mandatory when part is flagged, default to "Unknown" otherwise. Persisted in both inv_inventory (current state) and inv_transaction (historical record).

- **Custom Color**: User-defined color code entered via "OTHER" option. Title-cased before storage, checked for duplicates, optionally saved to md_color_codes for future reuse. Max 15 characters.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Material handlers can inventory a flagged part with color code and work order in under 30 seconds (average time from part entry to save confirmation)
- **SC-002**: Work order format validation catches 100% of invalid entries (letters, symbols) before database save
- **SC-003**: Search results for flagged parts display with correct sort order (color then location) in under 2 seconds for datasets up to 1000 records
- **SC-004**: System successfully blocks Advanced Inventory entry of flagged parts with 100% accuracy (zero instances of color-required parts saved without color data)
- **SC-005**: Custom color entries save to database with correct title-case formatting 100% of the time
- **SC-006**: Application restart after Settings changes refreshes color-flagged parts cache within 5 seconds
- **SC-007**: Show All operations display performance warning for result sets exceeding 1000 records with 100% reliability
- **SC-008**: Legacy inventory records (pre-feature) display "Unknown" for color code and work order with zero data corruption
- **SC-009**: Transfer operations preserve color code and work order data with 100% accuracy (no data loss or modification)
- **SC-010**: Settings form checkbox for "Requires Color Code & Work Order" persists changes to database with 100% success rate
- **SC-011**: Reduce material handler errors in work order segregation by 80% (measured by incorrect inventory removals tracked in log_error table)
- **SC-012**: Enable material handlers to process work orders 40% faster by sorting inventory by color code (measured by average time between search and removal operations)

## Assumptions

1. **Database Version**: MySQL 5.7.24 constraints apply - no JSON functions, CTEs, or window functions allowed in stored procedures
2. **Concurrent Users**: Multiple material handlers may inventory same part with different color codes simultaneously (database handles concurrency)
3. **Color Code Permanence**: Once assigned, color codes rarely change for a specific inventory batch (transfers preserve original color)
4. **Work Order Format Stability**: WO-###### format is standard and won't change (6-digit padding is business requirement)
5. **Network Reliability**: Color code cache loads once at startup; network failures during startup are acceptable (graceful degradation)
6. **User Training**: Material handlers understand color coding system and when to use specific colors (no in-app training required)
7. **Legacy Data Volume**: Majority of existing inventory records will have "Unknown" color code until naturally cycled out through normal operations
8. **Settings Access**: All users have permission to add/edit parts in Settings (no administrator-only restriction for RequiresColorCode flag)
9. **Performance Threshold**: 1000 record threshold for "Show All" warning is acceptable based on current database size and performance testing
10. **Custom Color Frequency**: "OTHER" option used infrequently (less than 5% of entries), so duplicate checking on save is acceptable UX

## Implementation Notes

### Database Schema Changes

```sql
-- Add color code flag to parts table
ALTER TABLE md_part_ids 
ADD COLUMN RequiresColorCode BOOLEAN DEFAULT FALSE;

-- Create color codes master table
CREATE TABLE md_color_codes (
  ColorCode VARCHAR(50) PRIMARY KEY,
  IsUserDefined BOOLEAN DEFAULT FALSE,
  CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Prepopulate predefined colors
INSERT INTO md_color_codes (ColorCode, IsUserDefined) VALUES
('Red', FALSE), ('Blue', FALSE), ('Green', FALSE), ('Yellow', FALSE),
('Orange', FALSE), ('Purple', FALSE), ('Pink', FALSE), ('White', FALSE),
('Black', FALSE), ('Unknown', FALSE);

-- Add color code and work order to inventory
ALTER TABLE inv_inventory
ADD COLUMN ColorCode VARCHAR(50) DEFAULT 'Unknown',
ADD COLUMN WorkOrder VARCHAR(10) DEFAULT 'Unknown';

-- Add color code and work order to transactions
ALTER TABLE inv_transaction
ADD COLUMN ColorCode VARCHAR(50) DEFAULT 'Unknown',
ADD COLUMN WorkOrder VARCHAR(10) DEFAULT 'Unknown';

-- Add indexes for performance
CREATE INDEX idx_colorcode ON inv_inventory(ColorCode);
CREATE INDEX idx_workorder ON inv_inventory(WorkOrder);
CREATE INDEX idx_requires_colorcode ON md_part_ids(RequiresColorCode);
```

### Affected Files

**Data Layer (DAOs)**:
- `Data/Dao_Inventory.cs` - Update Add/Search methods for ColorCode/WorkOrder
- `Data/Dao_Transactions.cs` - Update Add/Search methods
- `Data/Dao_Part.cs` - Add methods for color code flag management

**Controls (UI)**:
- `Controls/MainForm/Control_InventoryTab.cs` - Add SuggestionTextBox for Color/WO, validation
- `Controls/MainForm/Control_RemoveTab.cs` - Add column display logic, Show All warning
- `Controls/MainForm/Control_TransferTab.cs` - Add read-only column display
- `Controls/MainForm/Control_AdvancedRemove.cs` - Add Show All button, column hiding
- `Controls/MainForm/Control_AdvancedInventory.cs` - Add validation and redirect logic
- `Controls/SettingsForm/Control_Add_PartID.cs` - Add RequiresColorCode checkbox
- `Controls/SettingsForm/Control_Add_PartID.Designer.cs` - Designer file for checkbox
- `Controls/SettingsForm/Control_Edit_PartID.cs` - Add RequiresColorCode checkbox
- `Controls/SettingsForm/Control_Edit_PartID.Designer.cs` - Designer file for checkbox

**Models**:
- `Models/Model_Application_Variables.cs` - Add ColorFlaggedParts cache

**Helpers**:
- `Helper_UI_ComboBoxes.cs` - Add color codes cache management

**Database**:
- `Database/UpdatedStoredProcedures/` - Create/update stored procedures for color code operations
- `Database/UpdatedTables/` - Schema migration scripts

### Validation Rules

**Work Order Format**:
- Accept: `123456`, `WO-123456`, `12345`, `WO-12345`
- Auto-format: `12345` → `WO-012345`, `WO-1234` → `WO-001234`
- Reject: `ABC123`, `WO-ABC`, `12.34`, `WO--123`
- Error message: "Invalid work order format. Enter 5-6 digit number or WO-######"

**Color Code**:
- Dropdown selection from predefined list + "OTHER"
- "OTHER" opens dialog for custom entry (max 15 chars)
- Custom colors title-cased before save: "blueberry" → "Blueberry"
- "Unknown" not selectable by user (database default only)

**Part Flagging**:
- RequiresColorCode boolean (TRUE/FALSE)
- Checkbox in Settings > Add/Edit Part ID forms
- Cache refresh on app restart or Shift+Click Reset

### Performance Considerations

- Cache color-flagged parts list at startup (avoid repeated DB queries)
- Cache md_color_codes in Helper_UI_ComboBoxes
- Index ColorCode and WorkOrder columns in inv_inventory
- Show All warning threshold: 1000 records
- Dynamic column display (hide when not needed)
- Sort in database via ORDER BY clause (not in-memory)
