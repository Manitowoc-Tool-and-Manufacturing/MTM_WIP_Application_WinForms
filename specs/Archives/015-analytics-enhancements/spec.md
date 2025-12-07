# Feature Specification: Analytics & Inventory Management Enhancements

**Feature Branch**: `015-analytics-enhancements`  
**Created**: December 4, 2025  
**Status**: Draft  
**Input**: Comprehensive analytics, inventory audit, and search enhancements including Material Handler scoring, user shift tracking, PO Details refactor, and Die Tool Discovery improvements

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Manager Reviews Material Handler Performance with Fair Scoring (Priority: P1)

A warehouse manager wants to evaluate material handler performance across different shifts, accounting for the fact that some shifts are naturally busier than others. They need to see weighted scores that normalize performance across varying workload volumes.

**Why this priority**: Critical for fair employee evaluation and operational decision-making. Provides immediate value by enabling managers to identify top performers and areas needing improvement.

**Independent Test**: Can be fully tested by loading Material Handler Analytics, selecting a date range, and verifying that scores are displayed with shift-weighted normalization and explanatory documentation.

**Acceptance Scenarios**:

1. **Given** the manager is viewing Material Handler Analytics, **When** they select a date range and view results, **Then** they see a bar chart showing points per user with shift-weighted scores and a "Fair Grading Policy" explanation section
2. **Given** a user worked only 3rd shift (typically quieter), **When** their score is calculated, **Then** their score is weighted up to normalize against busier shifts
3. **Given** the manager wants to understand scoring, **When** they view the analytics page, **Then** they see clear documentation that 1pt = Add/Remove, 2pt = Transfer, and explanation of shift weighting formula

---

### User Story 2 - Admin Updates User Metadata from Infor Visual (Priority: P1)

A system administrator needs to synchronize user shift assignments and full names from the Infor Visual database so that analytics and reports display accurate, human-readable information.

**Why this priority**: Foundation for all user-facing analytics features. Without accurate user metadata, all reporting becomes unreliable.

**Independent Test**: Can be tested by navigating to Development → Database Maintenance → InforVisual Related, clicking "Update User Shifts" and "Update User Names", then verifying the sys_visual table contains correct JSON data.

**Acceptance Scenarios**:

1. **Given** the admin is in Database Maintenance, **When** they click "Update User Shifts", **Then** the system analyzes the last 50 transactions per user from Infor Visual and calculates shift assignments (1st/2nd/3rd/Weekend/Unknown) stored in JSON format
2. **Given** a user has transactions averaging between 06:00-14:00, **When** shift calculation runs, **Then** the user is assigned to 1st shift
3. **Given** the admin clicks "Update User Names", **When** the process completes, **Then** Visual usernames are mapped to full names and stored in sys_visual.json_user_fullnames

---

### User Story 3 - Manager Filters Inventory Audit by Date Presets (Priority: P2)

A manager reviewing inventory audit logs wants to quickly filter by common date ranges (Today, This Week, This Month, etc.) without manually adjusting calendar controls.

**Why this priority**: Significantly improves workflow efficiency for daily audit reviews. High-frequency use case that saves time.

**Independent Test**: Can be tested by opening Inventory Audit, selecting each radio button preset, and verifying the correct date range is applied and calendar controls are disabled unless "Custom" is selected.

**Acceptance Scenarios**:

1. **Given** the manager is viewing Inventory Audit, **When** they select "This Week" radio button, **Then** the system displays data from Monday-Friday of the current week and disables manual calendar controls
2. **Given** "This Month" is selected, **When** the page loads, **Then** data from the 1st to the last day of the current month is displayed
3. **Given** the manager selects "Custom", **When** the selection is made, **Then** calendar controls become enabled for manual date entry

---

### User Story 4 - User Searches Die Tools by Part or FGT Number (Priority: P2)

A production floor user needs to locate die tools using either a part number or FGT number, as they may only have one identifier available at the time of search.

**Why this priority**: Directly impacts production floor efficiency. Users frequently only have one type of identifier when searching.

**Independent Test**: Can be tested by entering a Part Number in Die Tool Discovery search box, verifying results appear, then repeating with an FGT number.

**Acceptance Scenarios**:

1. **Given** the user is in Die Tool Discovery, **When** they enter a valid Part Number, **Then** the system displays matching die tool records
2. **Given** the user enters an FGT number, **When** they submit the search, **Then** the system displays matching die tool records
3. **Given** an invalid identifier is entered, **When** search is performed, **Then** the system displays a clear "No results found" message

---

### User Story 5 - Procurement Reviews PO Details in Form View (Priority: P2)

A procurement specialist reviewing purchase orders wants to view detailed line specifications in a readable form layout rather than scrolling through a dense grid, especially for POs with many lines.

**Why this priority**: Improves readability and reduces errors when reviewing complex purchase orders with detailed specifications.

**Independent Test**: Can be tested by opening a PO with multiple lines, navigating between lines using Next/Back buttons, and verifying all details including specs are displayed in textboxes and RichTextBox.

**Acceptance Scenarios**:

1. **Given** a PO with multiple lines is selected, **When** the user views PO Details, **Then** the first line's details are displayed in labeled textboxes with Next/Back navigation enabled
2. **Given** the user clicks "Next", **When** the navigation button is pressed, **Then** the form updates to show the next line's details
3. **Given** a PO has only one line, **When** the details form loads, **Then** Next/Back buttons are disabled
4. **Given** a line has specifications text, **When** the line is displayed, **Then** the RichTextBox shows the full multi-line specification details from InforVisual

---

### User Story 6 - User Views Auto-issue Location for Coil/Flatstock (Priority: P3)

A material handler needs to identify where coil or flatstock materials are automatically issued from to streamline pick operations.

**Why this priority**: Operational efficiency improvement for specific material types. Lower priority as it affects a subset of materials.

**Independent Test**: Can be tested by searching for an MMC or MMF part in the Coil/Flatstock tab and verifying the Auto-issue Location ID is displayed.

**Acceptance Scenarios**:

1. **Given** the user searches for an MMC part in Coil/Flatstock Search, **When** results are displayed, **Then** the Auto-issue Location ID from InforVisual PART_LOCATION.AUTO_ISSUE_LOC is shown
2. **Given** no Auto-issue Location is defined for a part, **When** the part is displayed, **Then** the field shows "Not Set" or similar indication

---

### User Story 7 - User Checks Where a Part is Used (Priority: P3)

A production planner wants to see all Work Orders, assemblies, and FGTs that use a specific part to understand impact before making changes.

**Why this priority**: Valuable for planning and change impact analysis but not required for daily operations.

**Independent Test**: Can be tested by selecting a non-MMC/MMF part in Coil/Flatstock Search, clicking "Where Used", and verifying a DataGridView displays all related Work Orders, Parts, and FGTs.

**Acceptance Scenarios**:

1. **Given** a part is selected in Coil/Flatstock Search, **When** the user clicks "Where Used", **Then** a DataGridView displays all Work Orders, Parts, and FGTs using this part from the Bill of Materials
2. **Given** an MMC or MMF part is selected, **When** the form loads, **Then** the "Where Used" button is disabled
3. **Given** a part is not used anywhere, **When** "Where Used" is clicked, **Then** the DataGridView shows a message "This part is not currently used in any assemblies or work orders"

---

### Edge Cases

- What happens when a user has fewer than 50 transactions in Infor Visual Transaction History?
  - System uses whatever transactions are available (minimum 1) to calculate shift
  - If 0 transactions, user is marked as "Unknown" shift
  
- How does the system handle users who no longer exist in Infor Visual?
  - They are retained in sys_visual table to preserve historical analytics data
  - They appear in analytics with historical data but are marked as inactive
  
- What if a PO has 100+ lines and the user wants to jump to a specific line?
  - Navigation includes a dropdown/input field to jump directly to any line number
  
- How does shift weighting handle shifts with no activity?
  - ShiftVolumeFactor calculation excludes shifts with 0 transactions to prevent division by zero
  - If all shifts have 0 transactions, no weighting is applied (factor = 1.0)
  
- What if InforVisual CSVs are missing or corrupted?
  - System displays clear error message via Service_ErrorHandler
  - Features gracefully degrade (e.g., Auto-issue Location shows "Data Unavailable")
  
- How are concurrent updates to sys_visual handled?
  - Last-write-wins for JSON columns
  - Admin receives warning if another update is in progress

## Requirements *(mandatory)*

### Functional Requirements

#### Database & Infrastructure

- **FR-001**: System MUST create a new table `sys_visual` in both `mtm_wip_application_winforms` and `mtm_wip_application_winforms_test` databases
- **FR-002**: Table MUST include columns: `id` (INT, Primary Key, Auto-Increment), `json_shift_data` (JSON), `json_user_fullnames` (JSON)
- **FR-003**: `json_shift_data` MUST store user-to-shift mappings as `{"USERNAME": shift_number}` where shift_number is 1 (1st), 2 (2nd), 3 (3rd), 4 (Weekend), or 0 (Unknown)
- **FR-004**: `json_user_fullnames` MUST store username-to-full-name mappings as `{"USERNAME": "Full Name"}`
- **FR-005**: Enum `Enum_SuggestionDataSource` MUST include values: MTM_ItemType, MTM_Building, MTM_Warehouse, Infor_WorkOrder, Infor_CustomerOrder, Infor_PurchaseOrder

#### User Shift Calculation

- **FR-006**: System MUST analyze last 50 transactions per user from Infor Visual Transaction History to calculate shift assignment
- **FR-007**: Shift assignment MUST follow rules: 06:00-14:00 = 1st, 14:00-22:00 = 2nd, 22:00-06:00 = 3rd, Fri-Mon 06:00-18:00 = Weekend
- **FR-008**: Users with fewer than 50 transactions MUST be assigned based on available transaction times
- **FR-009**: Users with 0 transactions MUST be assigned to "Unknown" shift
- **FR-010**: System MUST calculate ShiftVolumeFactor as (Average Global Transactions / Average Shift Transactions)
- **FR-011**: User performance scores MUST be multiplied by ShiftVolumeFactor to normalize across shifts

#### User Name Mapping

- **FR-012**: System MUST map Visual UserNames to Full Names by matching records in Visual database
- **FR-013**: Mapping MUST be stored in `sys_visual.json_user_fullnames` in JSON format
- **FR-014**: Unmapped users MUST retain their username as the display value

#### Material Handler Analytics

- **FR-015**: System MUST remove tabs: Quality & Anomalies, User Detail, Glossary & Metrics
- **FR-016**: System MUST create new HTML template with graphs: Points per User (weighted), Transaction Type distribution (pie), Hot Parts (top 10), Location Heatmap (top 10), Shift Performance
- **FR-017**: Scoring MUST assign 1 point for Add/Remove transactions, 2 points for Transfer transactions
- **FR-018**: HTML template MUST include "Fair Grading Policy" section explaining shift-weighted scoring methodology
- **FR-019**: MainForm menu MUST move "MaterialHandlerAnalytics" from Development to View menu

#### Inventory Audit Enhancements

- **FR-020**: Inventory Audit (General) MUST provide Date Range Radio Buttons: Today, This Week (Default), This Month, This Quarter, This Year, Custom
- **FR-021**: Calendar controls MUST be disabled when any preset except "Custom" is selected
- **FR-022**: "This Week" MUST default to Monday-Friday of current week
- **FR-023**: User Analytics tab MUST display users as "USERNAME (Full Name)" format using data from sys_visual
- **FR-024**: User Analytics MUST include Shift Filter Checkboxes: First, Second, Third, Weekend
- **FR-025**: User Analytics MUST include Date Range Radio Buttons with same logic as General tab

#### Receiving Analytics

- **FR-026**: Toggle buttons MUST respect animation settings from Control_Theme
- **FR-027**: Groupboxes (Filters, PO State, Receiving Scope) MUST extend to bottom of Row 1 in TableLayoutPanel
- **FR-028**: System MUST display Vendor Scorecard showing Top 10 Vendors by Receipt Count (source: INVENTORY_TRANS joined with PURCHASE_ORDER)
- **FR-029**: System MUST display Receipts by Hour heatmap using TRANSACTION_DATE time component from INVENTORY_TRANS

#### Advanced Inventory

- **FR-030**: Advanced Inventory (Multiple Tab) MUST include F4 buttons for Part, Operation, and Location fields

#### Die Tool Discovery

- **FR-031**: "Enter Part Number or Die Number" SuggestionTextBox MUST accept both Part Numbers and FGT numbers
- **FR-032**: Coil/Flatstock Search MUST display Auto-issue Location ID from InforVisual PART_LOCATION.AUTO_ISSUE_LOC column
- **FR-033**: Coil/Flatstock Search MUST include "Where Used" button displaying Work Orders, Parts, FGTs in DataGridView
- **FR-034**: "Where Used" button MUST be disabled when MMC or MMF part is selected
- **FR-035**: "Where Used" data MUST be sourced from Bill of Materials (BOM) definitions

#### PO Details Form

- **FR-036**: Form MUST use TextBoxes with Labels instead of DataGridView for displaying PO line data
- **FR-037**: Layout MUST include sections: Top (Vendor, PO Date, Status), Center (Part ID, Qty, Unit Price, Due Date), Bottom (RichTextBox for Line Specs)
- **FR-038**: Form MUST include Next/Back buttons for navigating between PO lines
- **FR-039**: Next/Back buttons MUST be disabled when PO has only one line
- **FR-040**: RichTextBox MUST display multi-line specifications from InforVisual CSVs
- **FR-041**: Form MUST include dropdown/input to jump directly to specific line number for POs with many lines

#### Error Handling & Validation Refactoring

- **FR-042**: MainForm.cs, SettingsForm.cs, Dialog_EditParameterOverride.cs, Form_QuickButtonEdit.cs MUST replace all MessageBox.Show calls with Service_ErrorHandler methods
- **FR-043**: Service_OnStartup_AppLifecycle.cs MUST use Service_ErrorHandler where safe (preserve fallback for critical startup scenarios)

#### Suggestion Control Refactoring

- **FR-044**: Controls (ReceivingAnalytics, VisualInventory, InventoryAudit, PartIDManagement, OperationManagement, LocationManagement, ItemTypeManagement, TransactionSearchControl) MUST use SuggestionDataSource enum properties
- **FR-045**: Controls with manual focus handlers MUST set SelectionAction = None to avoid conflicts

#### Administrative Features

- **FR-046**: Database Maintenance view MUST include "InforVisual Related" Groupbox positioned under Cleanup Groupbox
- **FR-047**: Groupbox MUST include "Update User Shifts" button triggering shift calculation logic
- **FR-048**: Groupbox MUST include "Update User Names" button triggering name mapping logic
- **FR-049**: MainForm MUST hide "Help" menu item

### Key Entities

- **sys_visual Table**: Stores InforVisual-related metadata in JSON format
  - `id`: Unique identifier
  - `json_shift_data`: User-to-shift mappings
  - `json_user_fullnames`: Username-to-full-name mappings

- **User Shift**: Represents a user's primary work shift (1st/2nd/3rd/Weekend/Unknown) calculated from transaction patterns

- **Shift Volume Factor**: Normalization multiplier calculated as (Global Average Transactions / Shift Average Transactions)

- **Material Handler Score**: Weighted performance metric (1pt per Add/Remove, 2pt per Transfer, multiplied by ShiftVolumeFactor)

- **Auto-issue Location**: Default location from which a part is automatically issued in InforVisual

- **Where Used Data**: Bill of Materials information showing all assemblies, Work Orders, and FGTs using a specific part

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Managers can view Material Handler Analytics with shift-weighted scores and understand the Fair Grading Policy within 30 seconds of page load
- **SC-002**: System administrators can update user shift data and full names from Infor Visual in under 5 minutes per update operation
- **SC-003**: Users can filter Inventory Audit data by date presets (Today, Week, Month, etc.) with a single click, reducing average filter time from 30 seconds to 3 seconds
- **SC-004**: Procurement specialists can review PO line details and specifications without scrolling grids, reducing review time per line by 40%
- **SC-005**: Production floor users can search Die Tool Discovery using either Part Number or FGT number with 100% success rate for valid identifiers
- **SC-006**: Material handlers can identify Auto-issue Locations for Coil/Flatstock parts within 10 seconds of search
- **SC-007**: Production planners can view complete "Where Used" information for any non-MMC/MMF part in under 15 seconds
- **SC-008**: All error messages across the application use Service_ErrorHandler, achieving 100% compliance with centralized error handling policy
- **SC-009**: All suggestion controls use enum-based configuration, reducing code duplication by at least 60%
- **SC-010**: User Analytics displays full names (e.g., "JKOLL (John Koll)") for 95%+ of active users within the system

### Validation Service Integration

- **Date Range Inputs**: Map to existing `Service_Validation` date validators. Validation errors surfaced via `Service_ErrorHandler.HandleValidationError` with user-friendly messages.
- **User Selection Inputs**: Validate against active user list from sys_visual table. Display validation feedback inline on checkbox/selection controls.
- **Part Number / FGT Inputs**: Map to existing part number validators. No match action configurable via SuggestionTextBox.NoMatchAction property.
- **PO Line Navigation**: Validate line number is within bounds (1 to total lines). Display error if invalid line number entered.
- **Shift Filter Selections**: No validation required (checkboxes for predefined shifts).

**New Validators Required**:
- None - all inputs map to existing validators or require simple bounds checking

**Normalization**:
- Part Numbers: Uppercase, trim whitespace
- FGT Numbers: Uppercase, trim whitespace
- Usernames: Uppercase, trim whitespace
- Date presets: Convert to start/end DateTime pairs before querying

**Error Feedback**:
- All validation errors use `Service_ErrorHandler.HandleValidationError(message, field, callerName, controlName)`
- Inline validation feedback on SuggestionTextBox controls via red border + tooltip
- Date range errors display via MessageBox through Service_ErrorHandler with specific issue (e.g., "Start date must be before end date")
