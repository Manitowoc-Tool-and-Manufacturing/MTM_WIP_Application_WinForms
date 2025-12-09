# Quick Button Management System - Edit Feature Specification

**Version**: 1.0.0  
**Created**: 2025-12-09  
**Feature Type**: User Interface Enhancement  
**Related Features**: Quick Button Management Add, Quick Button Management Remove, Quick Button Management Reorder  
**Implementation Order**: #2 (Requires #1 Add to be implemented first)

---

## Implementation Notes

**This specification requires #1 (Add) to be implemented FIRST** as it depends on:
- Quick Buttons existing in database (created via Add feature)
- Core DAO methods: `GetQuickButtonsByUserIdAsync`, `GetPartByIdAsync`, `GetAllActivePartsAsync`, `GetAllActiveOperationsAsync`, `GetAllActiveColorCodesAsync`, `GetActiveWorkOrdersAsync`
- Validation patterns established in Add feature
- Conditional field visibility logic from Add feature

**Dependent Features** (can be implemented in parallel after #1):
- #3: Quick Button Remove
- #4: Quick Button Reorder

**Integration Features** (implement after #2, #3, #4):
- #5: Quick Button Action Bar
- #6: Quick Button Management Hub

---

## Constitutional Alignment

This feature adheres to the MTM WIP Application Constitution principles:

- **I. User Trust Through Reliability**: All operations provide clear feedback and prevent data loss through validation
- **II. Operational Transparency**: All user actions are logged with timestamps and user identity
- **III. Data Quality Assurance**: Input validation prevents invalid Quick Button configurations
- **IV. Consistent User Experience**: Follows established patterns from Control_InventoryTab
- **V. Performance Expectations**: UI remains responsive during database operations
- **VII. Communication Clarity**: Clear visual feedback shows current vs new values
- **VIII.  Maintainability and Documentation**: Complete documentation required for all components
- **X.  Resilience and Graceful Degradation**:  Handles concurrent edits and data conflicts gracefully

---

## Overview

### Purpose
Provides an interface for users to edit existing Quick Button configurations.  Shows current values alongside editable fields, validates changes, and updates the database while maintaining data integrity.

### User Goals
- Quickly identify which Quick Button to edit
- See current configuration while making changes
- Update Part, Operation, Color Code, and Work Order as needed
- Validate changes before saving
- Reset changes if desired
- Understand what changed after saving

### Business Value
- Improves user efficiency by allowing in-place Quick Button updates
- Reduces need to delete and recreate Quick Buttons
- Maintains data integrity through validation
- Enhances user experience with clear before/after visibility
- Prevents data loss through reset functionality

---

## Technical Requirements

### Technology Stack Constraints
- **. NET**:  8.0-windows
- **C# Language**: 12.0
- **WinForms**:  Inherit from `ThemedUserControl`
- **MySQL**: 5.7.24 (NO CTEs, window functions, or JSON functions)
- **Database Access**: ALL operations MUST use stored procedures
- **Error Handling**: ALL errors MUST use centralized error handler
- **Logging**: ALL operations MUST use structured logging utility

### Designer File Compatibility
**CRITICAL REQUIREMENT**:  All Designer files MUST be fully compatible with Visual Studio Code's WinForms Editor

**Constraints**:
- Use only standard WinForms controls and custom controls already in the project
- Avoid complex designer-generated code that VS Code cannot parse
- Keep designer initialization code simple and straightforward
- Use TableLayoutPanel for all layouts (VS Code WinForms Editor friendly)
- Avoid advanced designer features (visual inheritance beyond direct inheritance, custom designer attributes)
- Test all designer files open correctly in VS Code WinForms Editor
- All control properties must be settable in designer (no code-only initialization where avoidable)

### Naming Conventions
- **Control**:  `Control_QuickButtonManagement_Edit`
- **Components**: `Control_QuickButtonManagement_Edit_{ControlType}_{Name}_{Number? }`
- **Methods**: `{Action}Async` for asynchronous methods
- **Fields**: `_camelCase` for private fields
- **DAO Methods**: Return standardized result wrapper type

### Code Organization
All files MUST use region-based organization:
1. Fields
2. Properties
3. Constructors
4. Methods
5. Events
6. Helpers
7. Cleanup/Dispose

---

## Feature Behavior

### Initial Load Sequence

1. **Control Initialization**
   - Control loads and immediately queries user's configured Quick Buttons
   - Retrieves all Quick Button data (ID, Part, Operation, Color, Work Order, Slot Number)
   - Determines count of configured Quick Buttons

2. **Conditional Display Logic**
   - **IF Quick Buttons exist** → Display Edit Interface Panel
   - **IF NO Quick Buttons exist (0/10)** → Display No Quick Buttons Panel

3. **Default State**
   - No Quick Button pre-selected
   - Right column content area shows prompt: "Select a Quick Button to edit"
   - Save and Reset buttons disabled

4. **Logging**
   - Log control initialization
   - Log configured Quick Button count
   - Log which panel is displayed

---

## User Interface Layouts

### Layout 1: No Quick Buttons Panel

**Display Condition**: User has 0 configured Quick Buttons

**Purpose**: Inform user that no Quick Buttons exist to edit

#### Visual Structure

**Panel Organization**:
- Vertically centered content
- Information icon with message
- Action button

#### Components

**1. Information Icon**
- System information icon (48x48 pixels)
- Positioned to left of message text
- Spans both message rows
- Control Type: PictureBox

**2. Primary Message**
- Text: "No Quick Buttons to Edit"
- Bold font, 12pt
- Themed primary text color
- Prominent positioning
- Control Type: Label

**3. Secondary Message**
- Text: "You don't have any Quick Buttons configured.  Use the Add Quick Button feature to create your first Quick Button."
- Regular font, 10pt
- Themed secondary text color
- Word wrapping enabled (max width:  400px)
- Control Type: Label

**4. Close Button**
- Text: "Close"
- Size: 100x35
- Centered horizontally
- Closes the form
- Logs close action
- Control Type: Button

#### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_NoQuickButtons`
- **Dock**: Fill
- **Columns**: 1
- **Rows**: 4
- **Row Styles**:
  - Row 0:  Percent, 25F (top spacer)
  - Row 1: AutoSize (icon and messages)
  - Row 2: AutoSize (close button)
  - Row 3:  Percent, 75F (bottom spacer)

**Row 1 Content TableLayoutPanel**:  `Control_QuickButtonManagement_Edit_TableLayoutPanel_NoQuickButtonsContent`
- **Columns**: 2
- **Rows**: 2
- **Column Styles**:
  - Column 0: AutoSize (icon)
  - Column 1: Percent, 100F (messages)
- **Row Styles**:
  - Row 0: AutoSize (primary message)
  - Row 1: AutoSize (secondary message)

#### User Interaction Flow

1. User clicks "Edit Quick Button" action button
2. System checks for configured Quick Buttons
3. No Quick Buttons found (0 configured)
4. No Quick Buttons Panel displays
5. User reads message
6. User clicks "Close"
7. Form closes, returns to main interface
8. Action logged

---

### Layout 2: Edit Interface Panel

**Display Condition**: User has 1 or more configured Quick Buttons

**Purpose**: Provide interface for selecting and editing Quick Buttons

#### Visual Structure (Two-Column Layout)

**Main Layout**:
- **Left Column**: Quick Button selector (AutoSize width)
- **Right Column**: Edit interface (100% remaining width)
- Divider between columns for visual separation

---

## Left Column:  Quick Button Selector

### Purpose
Display all configured Quick Buttons in same visual format as main Quick Buttons panel, allowing user to select which one to edit

### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_LeftColumn`
- **Dock**: Fill
- **Columns**: 1
- **Rows**: Dynamic (based on Quick Button count)
- **AutoSize**: True
- **Row Styles**:  All AutoSize

### Components

**Header Label**:
- **Name**: `Control_QuickButtonManagement_Edit_Label_QuickButtonsHeader`
- **Text**: "Your Quick Buttons"
- **Font**: Bold, 10pt
- **Dock**: Fill
- **Padding**: 5px all sides
- Control Type: Label

**Quick Button Display**:
- Reuse existing `Control_QuickButton_Single` components
- One instance per configured Quick Button
- Display same information as main Quick Buttons panel: 
  - Quick Button number (slot number 1-10)
  - Part Number
  - Operation
  - Color Code (if applicable)
  - Work Order (if applicable)
- Vertical stacking (one per row)
- Each in separate row in TableLayoutPanel

### Interaction Behavior

**Selection**:
- User clicks on any Quick Button component to select it
- Selected Quick Button shows visual highlight (border change or background tint)
- Only one Quick Button can be selected at a time
- Previous selection clears when new selection made

**Visual Feedback**:
- **Hover State**: Subtle highlight (lighter background or border)
- **Selected State**:  Prominent highlight (distinct border color/width or background)
- **Unselected State**: Normal themed styling

**Selection Event**:
- When Quick Button selected, trigger load of that Quick Button's data into right column
- Populate current values display (header section)
- Populate edit fields with current values
- Enable Reset and Save buttons (appropriate states)
- Log selection action

### Design Considerations

**Scrolling**:
- If more than ~8 Quick Buttons configured, scrollable area
- Use Panel with AutoScroll = true containing the TableLayoutPanel
- Maintain fixed width to prevent horizontal scrolling

**Width**:
- AutoSize based on Quick Button component width
- Minimum width: 200px
- Maximum width: 300px (prevents excessive space usage)

---

## Right Column: Edit Interface

### Purpose
Display current Quick Button configuration and provide editable fields for changes

### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_Content`
- **Dock**: Fill
- **Columns**: 1
- **Rows**: 3
- **Row Styles**: 
  - Row 0: AutoSize (Header - Current Values Display)
  - Row 1: Percent, 100F (Input Fields)
  - Row 2: AutoSize (Action Buttons)

---

## Row 0: Header - Current Values Display (Read-Only)

### Purpose
Show current Quick Button configuration before changes for reference and comparison

### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_Header`
- **Dock**: Fill
- **Columns**: 2
- **Rows**: 4 (Quick Button #, Part/Operation, Color, Work Order - conditional visibility)
- **Column Styles**:
  - Column 0: 50% (Left - Label + Value pairs)
  - Column 1: 50% (Right - Label + Value pairs)
- **Row Styles**:  All AutoSize
- **Padding**: 10px all sides
- **BorderStyle**: FixedSingle (visual grouping)
- **BackColor**: Themed slightly different from background (subtle distinction)

### Components Layout

**Row 0 (Quick Button Number - spans both columns)**:
- **Column 0-1 (ColumnSpan = 2)**:
  - Label: `Control_QuickButtonManagement_Edit_Label_HeaderQuickButtonNumber`
  - Text: "Editing Quick Button #{X}" (dynamic)
  - Font: Bold, 11pt
  - Themed primary text color
  - Dock: Fill

**Row 1 (Part and Operation)**:
- **Column 0**:
  - TableLayoutPanel: `Control_QuickButtonManagement_Edit_TableLayoutPanel_HeaderPart`
  - 2 Columns, 1 Row
  - Column 0 (AutoSize): Label "Current Part:"
  - Column 1 (Percent, 100F): Label with value `{PartNumber}`
  
- **Column 1**: 
  - TableLayoutPanel: `Control_QuickButtonManagement_Edit_TableLayoutPanel_HeaderOperation`
  - 2 Columns, 1 Row
  - Column 0 (AutoSize): Label "Current Operation:"
  - Column 1 (Percent, 100F): Label with value `{Operation}`

**Row 2 (Color - Conditional Visibility)**:
- **Column 0**:
  - TableLayoutPanel: `Control_QuickButtonManagement_Edit_TableLayoutPanel_HeaderColor`
  - 2 Columns, 1 Row
  - Column 0 (AutoSize): Label "Current Color:"
  - Column 1 (Percent, 100F): Label with value `{ColorCode}`
  - **Visible**: Only if current Quick Button has Color Code

- **Column 1**:
  - Empty or spacer

**Row 3 (Work Order - Conditional Visibility)**:
- **Column 0**: 
  - TableLayoutPanel: `Control_QuickButtonManagement_Edit_TableLayoutPanel_HeaderWorkOrder`
  - 2 Columns, 1 Row
  - Column 0 (AutoSize): Label "Current Work Order:"
  - Column 1 (Percent, 100F): Label with value `{WorkOrder}`
  - **Visible**: Only if current Quick Button has Work Order

- **Column 1**: 
  - Empty or spacer

### Visibility Logic

**Initial State** (no selection):
- Entire header panel hidden or shows placeholder message:  "Select a Quick Button to edit"

**After Selection**:
- Show header panel
- Always show:  Quick Button #, Part, Operation
- Show Color row only if current Quick Button configuration includes Color Code
- Show Work Order row only if current Quick Button configuration includes Work Order

### Design Rationale

This read-only header provides context so users can: 
- Confirm they selected the correct Quick Button
- Compare current values against new values they're entering
- Understand what will change when they click Save
- Reference the original values if they want to revert specific fields

---

## Row 1: Input Fields Section

### Purpose
Provide editable fields for modifying Quick Button configuration

### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_Inputs`
- **Dock**: Fill
- **Columns**: 1
- **Rows**: 4 (one per suggestion textbox)
- **Row Styles**:  All AutoSize
- **Padding**: 10px all sides

### Components (Using Component_SuggestionTextBoxWithLabel)

All fields use the existing `Component_SuggestionTextBoxWithLabel` component for consistency with Control_InventoryTab

#### 1. Part Number Field

**Component Name**: `Control_QuickButtonManagement_Edit_SuggestionBox_Part`
- **Label Text**: "Part Number"
- **Dock**: Fill
- **Tab Index**: 0
- **Data Source**: Active parts from database (same as Control_InventoryTab)
- **Validation**: Required, must exist in database
- **Initial Value**:  Populated with current Part Number from selected Quick Button
- **Event Handler**: TextChanged event triggers conditional field visibility logic

**DAO Method**: GetAllActiveParts (reuse existing)

#### 2. Operation Field

**Component Name**: `Control_QuickButtonManagement_Edit_SuggestionBox_Operation`
- **Label Text**: "Operation"
- **Dock**: Fill
- **Tab Index**: 1
- **Data Source**: Active operations from database (same as Control_InventoryTab)
- **Validation**: Required, must exist in database
- **Initial Value**: Populated with current Operation from selected Quick Button
- **Event Handler**: TextChanged event triggers Save button state update

**DAO Method**: GetAllActiveOperations (reuse existing)

#### 3. Color Code Field (Conditional Visibility)

**Component Name**: `Control_QuickButtonManagement_Edit_SuggestionBox_ColorCode`
- **Label Text**: "Color Code"
- **Dock**: Fill
- **Tab Index**: 2
- **Data Source**: Active color codes from database (same as Control_InventoryTab)
- **Validation**: Required when visible, must exist in database or be "OTHER"
- **Initial Value**:  Populated with current Color Code if exists, otherwise empty
- **Visibility Logic**: 
  - Shown if new Part selection requires Color Code
  - Hidden if new Part selection does not require Color Code
- **Special Handling**: "OTHER" option triggers custom color entry dialog (same as Control_InventoryTab)
- **Event Handler**: TextChanged event triggers Save button state update

**DAO Method**: GetAllActiveColorCodes (reuse existing)

#### 4. Work Order Field (Conditional Visibility)

**Component Name**: `Control_QuickButtonManagement_Edit_SuggestionBox_WorkOrder`
- **Label Text**: "Work Order"
- **Dock**: Fill
- **Tab Index**: 3
- **Data Source**: Active work orders from database (same as Control_InventoryTab)
- **Validation**: Required when visible, alphanumeric format
- **Initial Value**: Populated with current Work Order if exists, otherwise empty
- **Visibility Logic**:
  - Shown if new Part selection requires Work Order
  - Hidden if new Part selection does not require Work Order
- **Event Handler**: TextChanged event triggers Save button state update

**DAO Method**:  GetAllActiveWorkOrders (reuse existing)

### Conditional Field Visibility Logic

**Trigger**:  Part Number field value changes

**Process**:
1. Query database for selected Part's metadata (requires Color Code?  requires Work Order?)
2. Show/hide Color Code field based on Part requirements
3. Show/hide Work Order field based on Part requirements
4. Clear hidden fields if they become invisible
5. Preserve values if fields remain visible
6. Update Save button enabled state
7. Log visibility change

**Scenarios**: 

**Scenario A:  Part A → Part B (both require Color and Work Order)**
- User changes Part A to Part B
- Both fields remain visible
- Field values preserved (user can keep or change)
- Save button state updates based on validation

**Scenario B: Part A (requires Color/WO) → Part C (does not require Color/WO)**
- User changes Part A to Part C
- Color and Work Order fields hide
- Field values cleared
- Save button state updates

**Scenario C: Part C (no Color/WO) → Part A (requires Color/WO)**
- User changes Part C to Part A
- Color and Work Order fields appear
- Fields are empty (require input)
- Save button disabled until fields populated

**Reference Implementation**:  Exact same logic as Control_InventoryTab conditional field visibility

### Custom Color Entry

**Trigger**: User selects "OTHER" from Color Code dropdown

**Process** (same as Control_InventoryTab and Add feature):
1. Display modal input dialog (simple form)
2. User enters custom color name
3. Validate color name (non-empty, reasonable length)
4. Format to title case
5. Call DAO method to add custom color to database
6. On success, replace "OTHER" text with custom color value
7. Close dialog
8. Log custom color creation

**Dialog Specification**:
- **Title**: "Enter Custom Color"
- **Size**: 400x150
- **FormBorderStyle**: FixedDialog
- **StartPosition**: CenterParent
- **Controls**:
  - Label: "Color Name:" (Left:  20, Top: 20, Width: 100)
  - TextBox: Input field (Left: 130, Top: 20, Width:  230)
  - Button OK:  "OK" (Left: 130, Top: 60, Width: 100, DialogResult: OK)
  - Button Cancel: "Cancel" (Left: 240, Top: 60, Width:  100, DialogResult: Cancel)

**DAO Method**: AddCustomColorCode (reuse existing or create if not exists)

---

## Row 2: Action Buttons Section

### Purpose
Provide Save and Reset actions for Quick Button edits

### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_Buttons`
- **Dock**: Fill
- **Columns**: 5
- **Rows**: 1
- **Column Styles**:
  - Column 0: Percent, 33.33F (spacer)
  - Column 1: AutoSize (Reset button)
  - Column 2:  Percent, 33.34F (spacer)
  - Column 3: AutoSize (Save button)
  - Column 4: Percent, 33.33F (spacer)
- **Padding**: 10px all sides

### Components

#### Reset Button

**Button Name**: `Control_QuickButtonManagement_Edit_Button_Reset`
- **Text**: "Reset"
- **Size**: 100x35
- **Tab Index**: 4
- **Dock**: Fill in cell
- **Anchor**: None (centered)

**Enabled Logic**:
- Enabled when at least one input field has been modified from its original value
- Disabled when all input fields match original values
- Disabled when no Quick Button selected

**Click Action**:
1. Restore all input fields to original values (from selected Quick Button data)
2. Restore conditional field visibility to match original Part requirements
3. Update Save button state (should disable if all values match original)
4. Provide visual feedback (optional:  brief message "Fields reset to original values")
5. Log reset action
6. NO database operation

**Implementation Logic**:
- Store original values when Quick Button is selected
- On Reset click, repopulate fields with stored original values
- Re-trigger conditional visibility logic based on original Part

#### Save Button

**Button Name**:  `Control_QuickButtonManagement_Edit_Button_Save`
- **Text**: "Save Changes"
- **Size**: 120x35
- **Tab Index**:  5
- **Dock**:  Fill in cell
- **Anchor**: None (centered)
- **Visual Style**: Primary action button (themed accent color)

**Enabled Logic**: 
- ALL of the following must be true:
  1. A Quick Button is selected
  2. Part Number is valid (non-empty, exists in database)
  3. Operation is valid (non-empty, exists in database)
  4. IF Color Code is visible → Color Code is valid (non-empty, exists or is custom)
  5. IF Work Order is visible → Work Order is valid (non-empty, valid format)
  6. At least ONE field has been modified from original value (prevent saving with no changes)

**Implementation Note**: Validation logic identical to Control_InventoryTab Save button

**Click Action**:
1. Re-validate all inputs (defensive programming)
2. Confirm at least one value changed (prevent no-op saves)
3. Gather all field values (Part, Operation, Color, Work Order)
4. Call DAO method to update Quick Button in database
5. Handle result: 
   - **Success**: 
     - Display success message:  "Quick Button #{X} updated successfully!"
     - Refresh left column Quick Button display (show updated values)
     - Refresh parent Quick Buttons display
     - Update header section with new "current" values
     - Keep form open (allow further edits or selection change)
     - Log success
   - **Failure**:
     - Display user-friendly error message
     - Keep form open (allow retry)
     - Log failure with error details
6. Update button states (Reset may disable if no further changes)

**Change Detection Logic**:
- Compare current field values against stored original values
- String comparison (trimmed, case-sensitive for Part/Operation, case-insensitive for Color/WO)
- Null-safe comparison for optional fields (Color, Work Order)
- Return true if ANY field differs from original

---

## Main Layout Assembly

### TableLayoutPanel Structure (VS Code Compatible)

**Panel Name**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_Main`
- **Dock**: Fill
- **Columns**: 2
- **Rows**: 1
- **Column Styles**: 
  - Column 0: AutoSize (Left column - Quick Button selector)
  - Column 1: Percent, 100F (Right column - Edit interface)
- **CellBorderStyle**: Single (visual divider between columns)

**Column 0 Content**:  `Control_QuickButtonManagement_Edit_TableLayoutPanel_LeftColumn` (see Left Column section)

**Column 1 Content**: `Control_QuickButtonManagement_Edit_TableLayoutPanel_Content` (see Right Column section)

---

## Validation Rules

### Part Number Validation
- **Rule**: Non-empty string
- **Rule**: Must exist in Parts table
- **Rule**: Must be active Part
- **Feedback**:  Suggestion textbox shows only valid active Parts
- **Error Message**: "Please select a valid Part Number"
- **Validation Timing**: On field blur and on Save button click

### Operation Validation
- **Rule**: Non-empty string
- **Rule**: Must exist in Operations table
- **Rule**: Must be active Operation
- **Feedback**: Suggestion textbox shows only valid active Operations
- **Error Message**:  "Please select a valid Operation"
- **Validation Timing**: On field blur and on Save button click

### Color Code Validation (When Visible)
- **Rule**: Non-empty string when field is visible
- **Rule**: Must exist in Color Codes table OR be newly added custom color
- **Rule**: "OTHER" triggers custom color entry process
- **Feedback**: Suggestion textbox shows valid color codes plus "OTHER" option
- **Error Message**: "Please select or enter a valid Color Code"
- **Validation Timing**: On field blur, on Save button click, on custom color entry

### Work Order Validation (When Visible)
- **Rule**: Non-empty string when field is visible
- **Rule**:  Alphanumeric format (letters, numbers, hyphens allowed)
- **Rule**: Must exist in Work Orders table or be valid format for new entry
- **Feedback**: Suggestion textbox shows valid work orders
- **Error Message**: "Please enter a valid Work Order"
- **Validation Timing**: On field blur, on Save button click

### Change Detection Validation
- **Rule**: At least one field must differ from original value to enable Save
- **Purpose**: Prevent unnecessary database updates with no changes
- **Feedback**:  Save button remains disabled if no changes detected
- **Comparison**: 
  - Part: Case-sensitive string comparison
  - Operation: Case-sensitive string comparison
  - Color Code: Case-insensitive string comparison (if visible)
  - Work Order: Case-insensitive string comparison (if visible)
  - Null/empty treated as equivalent for optional fields

### Overall Form Validation
- All required fields must pass individual validation
- Conditional fields only validated when visible
- Change detection must pass (at least one change)
- Validation occurs on: 
  - Individual field change (for real-time button state updates)
  - Save button click (comprehensive validation before database operation)

---

## Database Operations

### DAO Methods Required

All DAO methods MUST: 
- Return `Model_Dao_Result<T>` wrapper type
- Use stored procedures via Helper_Database_StoredProcedure
- Include comprehensive error handling
- Log operations

#### 1. Get User Quick Buttons (Reuse from Remove feature)

**Method Signature**:  `Task<Model_Dao_Result<DataTable>> GetAllQuickButtonsByUserAsync(string userId)`

**Purpose**: Retrieve all configured Quick Buttons for current user with full details

**Input**:  User ID (from Model_Application_Variables.User)

**Output**: DataTable with columns:
- QuickButtonId (INT)
- SlotNumber (INT) - 1 to 10
- PartId (VARCHAR)
- PartDescription (VARCHAR)
- OperationId (VARCHAR)
- OperationDescription (VARCHAR)
- ColorCode (VARCHAR, nullable)
- WorkOrder (VARCHAR, nullable)
- Hotkey (VARCHAR, nullable)
- CreatedDate (DATETIME)
- LastModifiedDate (DATETIME)

**Business Logic**:
- Query QuickButtons table filtered by user ID
- JOIN with Parts table for Part description
- JOIN with Operations table for Operation description
- ORDER BY SlotNumber
- Include only active (non-deleted) Quick Buttons

**Error Handling**:  Return error result with user-friendly message if query fails

**Stored Procedure**: `md_quickbutton_GetAllByUser`

#### 2. Get Part Metadata (Reuse from Add feature)

**Method Signature**:  `Task<Model_Dao_Result<PartMetadata>> GetPartMetadataAsync(string partId)`

**Purpose**: Determine if Part requires Color Code and/or Work Order

**Input**: Part ID

**Output**: PartMetadata object with properties:
- PartId (string)
- Description (string)
- RequiresColorCode (bool)
- RequiresWorkOrder (bool)

**Business Logic**:  Query Part configuration table for specified Part

**Error Handling**: Return error result if Part not found or query fails

**Stored Procedure**:  `md_part_GetMetadata`

#### 3. Update Quick Button

**Method Signature**: 
```
Task<Model_Dao_Result<bool>> UpdateQuickButtonAsync(
    int quickButtonId, 
    string userId,
    string partId, 
    string operationId, 
    string?  colorCode, 
    string? workOrder)
```

**Purpose**: Update existing Quick Button configuration

**Input**:
- quickButtonId:  ID of Quick Button to update
- userId: User ID (for authorization check)
- partId: New Part ID
- operationId:  New Operation ID
- colorCode: New Color Code (null if not applicable)
- workOrder: New Work Order (null if not applicable)

**Output**: Boolean (true = success, false = failure)

**Business Logic**:
1. Validate Quick Button exists
2. Validate Quick Button belongs to user (authorization)
3. Validate Part exists and is active
4. Validate Operation exists and is active
5. Validate Color Code if provided
6. Validate Work Order if provided
7. UPDATE QuickButtons table
8. Update LastModifiedDate to current timestamp
9. Return success/failure

**Error Handling**: 
- Return error result if Quick Button not found
- Return error result if unauthorized (Quick Button belongs to different user)
- Return error result if validation fails
- Return error result if update operation fails

**Stored Procedure**:  `md_quickbutton_Update`

**Transaction**:  Use transaction for atomicity

#### 4. Get Quick Button Details

**Method Signature**: `Task<Model_Dao_Result<QuickButtonDetails>> GetQuickButtonDetailsAsync(int quickButtonId)`

**Purpose**: Retrieve detailed information for a specific Quick Button (used when populating edit fields)

**Input**: Quick Button ID

**Output**: QuickButtonDetails object with properties:
- QuickButtonId (int)
- SlotNumber (int)
- PartId (string)
- PartDescription (string)
- OperationId (string)
- OperationDescription (string)
- ColorCode (string, nullable)
- WorkOrder (string, nullable)
- Hotkey (string, nullable)

**Business Logic**: Query Quick Button by ID with JOINs to related tables

**Error Handling**: Return error result if Quick Button not found

**Stored Procedure**: `md_quickbutton_GetDetails`

---

### Stored Procedures Required

All stored procedures must be MySQL 5.7.24 compatible (NO CTEs, window functions, JSON functions)

#### 1. md_quickbutton_GetAllByUser

**Parameters**:  
- `p_UserId` VARCHAR(50)

**Returns**: ResultSet

**Columns**:  QuickButtonId, SlotNumber, PartId, PartDescription, OperationId, OperationDescription, ColorCode, WorkOrder, Hotkey, CreatedDate, LastModifiedDate

**Logic**:
```sql
SELECT 
    qb.QuickButtonId,
    qb.SlotNumber,
    qb.PartId,
    p.Description AS PartDescription,
    qb.OperationId,
    o.Description AS OperationDescription,
    qb.ColorCode,
    qb.WorkOrder,
    qb.Hotkey,
    qb.CreatedDate,
    qb. LastModifiedDate
FROM QuickButtons qb
INNER JOIN Parts p ON qb.PartId = p.PartId
INNER JOIN Operations o ON qb. OperationId = o. OperationId
WHERE qb. UserId = p_UserId
  AND qb.IsDeleted = 0
ORDER BY qb.SlotNumber;
```

#### 2. md_part_GetMetadata

**Parameters**:  
- `p_PartId` VARCHAR(50)

**Returns**: Single row

**Columns**: PartId, Description, RequiresColorCode (TINYINT/BOOL), RequiresWorkOrder (TINYINT/BOOL)

**Logic**:
```sql
SELECT 
    PartId,
    Description,
    RequiresColorCode,
    RequiresWorkOrder
FROM Parts
WHERE PartId = p_PartId
  AND IsActive = 1;
```

#### 3. md_quickbutton_Update

**Parameters**:
- `p_QuickButtonId` INT
- `p_UserId` VARCHAR(50)
- `p_PartId` VARCHAR(50)
- `p_OperationId` VARCHAR(50)
- `p_ColorCode` VARCHAR(50) (nullable)
- `p_WorkOrder` VARCHAR(50) (nullable)

**Returns**: Status code
- 0 = Success
- 1 = Quick Button not found
- 2 = Unauthorized (belongs to different user)
- 3 = Part not found or inactive
- 4 = Operation not found or inactive
- 5 = Color Code invalid (when provided)
- 6 = Work Order invalid (when provided)
- -1 = General error

**Logic**:
```sql
-- Validation:  Quick Button exists and belongs to user
IF NOT EXISTS (
    SELECT 1 FROM QuickButtons 
    WHERE QuickButtonId = p_QuickButtonId 
      AND UserId = p_UserId
      AND IsDeleted = 0
) THEN
    -- Return 1 if not found, 2 if wrong user
    -- Implementation specific based on error code strategy
END IF;

-- Validation: Part is active
IF NOT EXISTS (
    SELECT 1 FROM Parts 
    WHERE PartId = p_PartId 
      AND IsActive = 1
) THEN
    -- Return 3
END IF;

-- Validation:  Operation is active
IF NOT EXISTS (
    SELECT 1 FROM Operations 
    WHERE OperationId = p_OperationId 
      AND IsActive = 1
) THEN
    -- Return 4
END IF;

-- Validation: Color Code (if provided)
IF p_ColorCode IS NOT NULL THEN
    IF NOT EXISTS (
        SELECT 1 FROM ColorCodes 
        WHERE ColorCode = p_ColorCode 
          AND IsActive = 1
    ) THEN
        -- Return 5
    END IF;
END IF;

-- Validation: Work Order (if provided)
-- Validation logic depends on Work Order table structure
-- Assume similar pattern to Color Code

-- Update operation
UPDATE QuickButtons
SET 
    PartId = p_PartId,
    OperationId = p_OperationId,
    ColorCode = p_ColorCode,
    WorkOrder = p_WorkOrder,
    LastModifiedDate = NOW()
WHERE QuickButtonId = p_QuickButtonId;

-- Return 0 for success
```

**Transaction**:  Entire procedure runs in transaction, rollback on any error

#### 4. md_quickbutton_GetDetails

**Parameters**:
- `p_QuickButtonId` INT

**Returns**: Single row

**Columns**: QuickButtonId, SlotNumber, PartId, PartDescription, OperationId, OperationDescription, ColorCode, WorkOrder, Hotkey

**Logic**:
```sql
SELECT 
    qb.QuickButtonId,
    qb.SlotNumber,
    qb.PartId,
    p.Description AS PartDescription,
    qb.OperationId,
    o.Description AS OperationDescription,
    qb.ColorCode,
    qb.WorkOrder,
    qb.Hotkey
FROM QuickButtons qb
INNER JOIN Parts p ON qb.PartId = p.PartId
INNER JOIN Operations o ON qb.OperationId = o. OperationId
WHERE qb.QuickButtonId = p_QuickButtonId
  AND qb.IsDeleted = 0;
```

---

## Code-Behind Logic (Functional Description)

### Constructor Logic

**Initialization Sequence**:
1. Call base constructor (ThemedUserControl)
2. Call InitializeComponent (designer-generated)
3. Initialize private fields (selected Quick Button ID, original values storage)
4. Wire up event handlers for input field changes
5. Set initial UI state (buttons disabled, right column shows "Select a Quick Button" message)
6. Log control construction

### Control Load Event Handler

**Async Load Sequence**:
1. Set progress/loading indicator (if applicable)
2. Call DAO method to get user's Quick Buttons
3. Handle DAO result:
   - **Success with data**:  
     - Populate left column with Quick Button components
     - Show Edit Interface Panel
     - Hide No Quick Buttons Panel
   - **Success with no data**:
     - Show No Quick Buttons Panel
     - Hide Edit Interface Panel
   - **Failure**:
     - Display error message
     - Log error
     - Optionally close form or allow retry
4. Clear progress/loading indicator
5. Log load completion

### Quick Button Selection Event Handler

**Selection Logic**:
1. Clear previous selection visual state
2. Set new selection visual state (highlight selected Quick Button)
3. Store selected Quick Button ID
4. Retrieve full details for selected Quick Button (from cached data or database query)
5. Populate header section with current values: 
   - Quick Button number
   - Part Number and Description
   - Operation and Description
   - Color Code (if exists)
   - Work Order (if exists)
6. Store original values (for reset and change detection)
7. Populate input fields with current values: 
   - Part Number SuggestionTextBox
   - Operation SuggestionTextBox
   - Color Code SuggestionTextBox (if applicable)
   - Work Order SuggestionTextBox (if applicable)
8. Trigger conditional field visibility logic based on current Part
9. Update button states (Reset disabled, Save disabled - no changes yet)
10. Show right column edit interface (hide "Select a Quick Button" message if shown)
11. Log selection action

### Part Number Field Change Event Handler

**Async Change Logic**:
1. Get new Part ID from field
2. If empty, hide Color and Work Order fields, return
3. Call DAO method to get Part metadata (requires Color?  requires Work Order?)
4. Handle DAO result:
   - **Success**:
     - Show/hide Color Code field based on metadata
     - Show/hide Work Order field based on metadata
     - Clear hidden fields
     - Log visibility change
   - **Failure**:
     - Log error
     - Optionally show error to user
5. Trigger change detection logic
6. Update Save and Reset button states

### Input Field Change Event Handlers (Operation, Color, Work Order)

**Change Logic** (for each field):
1. Trigger change detection logic
2. Update Save and Reset button states
3. Log field change (optional, may be verbose)

### Change Detection Logic

**Detection Process**:
1. Compare current Part value vs original Part value
2. Compare current Operation value vs original Operation value
3. Compare current Color Code value vs original Color Code value (if originally existed or now exists)
4. Compare current Work Order value vs original Work Order value (if originally existed or now exists)
5. Return true if ANY field differs, false if all match
6. Handle null/empty comparisons carefully (null == empty for optional fields)

### Reset Button Click Event Handler

**Reset Logic**: 
1. Restore Part field to original value
2. Restore Operation field to original value
3. Restore Color Code field to original value (if originally existed)
4. Restore Work Order field to original value (if originally existed)
5. Re-trigger conditional field visibility logic based on original Part
6. Update button states (Reset should disable, Save should disable)
7. Optional: Show brief feedback message "Fields reset to original values"
8. Log reset action

### Save Button Click Event Handler

**Async Save Logic**:
1. Re-validate all input fields (defensive programming)
2. Confirm at least one field has changed (should be guaranteed by button enabled state)
3. Gather all field values:
   - Quick Button ID (from selection)
   - Part ID
   - Operation ID
   - Color Code (null if not visible/empty)
   - Work Order (null if not visible/empty)
4. Call DAO method to update Quick Button
5. Handle DAO result:
   - **Success**:
     - Display success message:  "Quick Button #{X} updated successfully!"
     - Update stored original values to new values (for further change detection)
     - Refresh left column Quick Button display (re-query or update in place)
     - Refresh parent Quick Buttons display (call MainForm refresh method)
     - Update header section to show new "current" values
     - Update button states (Reset disabled - no changes, Save disabled - no changes)
     - Log success
   - **Failure**: 
     - Display user-friendly error message
     - Log error with details
     - Keep form open for retry
6. Keep Quick Button selected (allow further edits if desired)

### Update Button States Logic

**Called whenever input changes or after Reset/Save**: 

**Reset Button State**:
- Enabled: If change detection returns true (at least one field differs from original)
- Disabled: If no changes detected OR no Quick Button selected

**Save Button State**:
- Enabled: If ALL of: 
  1. Quick Button is selected
  2. Part field is valid (non-empty, exists in database)
  3. Operation field is valid (non-empty, exists in database)
  4. IF Color field visible: Color field is valid
  5. IF Work Order field visible: Work Order field is valid
  6. At least one field has changed from original
- Disabled: If any condition above is false

### Form Close/Cleanup Logic

**Cleanup Sequence**:
1. Log form close
2. Unsubscribe from event handlers (prevent memory leaks)
3. Dispose of child controls (if not automatically disposed)
4. Call base Dispose method

---

## User Interaction Flows

### Flow 1: Successful Quick Button Edit

1. User clicks "Edit Quick Button" action button
2. Form opens with Edit control loaded
3. System loads user's configured Quick Buttons (e.g., 7 Quick Buttons)
4. Edit Interface Panel displays with left column showing 7 Quick Buttons
5. Right column shows "Select a Quick Button to edit" message
6. Save and Reset buttons are disabled
7. User clicks on Quick Button #3
8. Quick Button #3 highlights (selected state)
9. Header section populates: 
   - "Editing Quick Button #3"
   - "Current Part: MTM-12345 - Widget Assembly"
   - "Current Operation:  Welding"
   - "Current Color: Red"
   - "Current Work Order: WO-2024-001"
10. Input fields populate with current values
11. Color and Work Order fields are visible (Part requires them)
12. Reset disabled (no changes), Save disabled (no changes)
13. User changes Part Number to "MTM-67890"
14. System queries Part metadata for MTM-67890
15. Part MTM-67890 also requires Color and Work Order
16. Color and Work Order fields remain visible
17. Reset button enables (change detected)
18. Save button still disabled (validation pending on other fields)
19. User changes Operation to "Painting"
20. Save button enables (all validations pass, changes detected)
21. User clicks "Save Changes"
22. System validates inputs
23. System calls DAO to update Quick Button #3
24. Success message:  "Quick Button #3 updated successfully!"
25. Header section updates to show new values as "current"
26. Left column Quick Button #3 display updates to show new Part/Operation
27. Original values updated to new values
28. Reset disabled (no changes), Save disabled (no changes)
29. Parent Quick Buttons display refreshes
30. User can select another Quick Button to edit or close form
31. All actions logged

### Flow 2: Edit with Conditional Field Changes

1. User follows flow to select Quick Button #5
2. QB #5 current config:  Part ABC (requires Color/WO), Operation "Assembly", Color "Blue", WO "WO-100"
3. Header and fields populate
4. User changes Part to "XYZ-999" (does NOT require Color/WO)
5. System queries Part metadata
6. Color and Work Order fields hide
7. Color and Work Order values cleared
8. Reset enables, Save enables (Part changed, other validations pass)
9. User clicks "Save Changes"
10. Update sent to database:  Part "XYZ-999", Operation "Assembly", Color NULL, WO NULL
11. Success message displays
12. QB #5 display updates (no Color/WO shown)
13. Header updates (Color and Work Order rows hide)
14. All actions logged

### Flow 3: Using Reset Button

1. User selects Quick Button #2
2. Fields populate with current values:  Part "AAA", Operation "Test", Color "Green"
3. User changes Part to "BBB"
4. User changes Operation to "Review"
5. Reset button enables
6. Save button enables (validations pass, changes detected)
7. User decides to revert changes
8. User clicks "Reset"
9. Part field resets to "AAA"
10. Operation field resets to "Test"
11. Color field remains "Green" (unchanged originally)
12. Reset button disables (no changes)
13. Save button disables (no changes)
14. Optional feedback message: "Fields reset to original values"
15. User can make new edits or close form
16. Reset action logged

### Flow 4: Custom Color Entry During Edit

1. User selects Quick Button #8
2. Current config: Part "WIDGET" (requires Color), Operation "Polish", Color "Silver"
3. Fields populate
4. User decides to change Color
5. User opens Color Code dropdown
6. User selects "OTHER"
7. Custom color entry dialog appears
8. User enters "Metallic Teal"
9. User clicks OK
10. System formats to "Metallic Teal"
11. System adds color to database (if not exists)
12. Color Code field updates to "Metallic Teal"
13. Dialog closes
14. Save button enables (change detected)
15. User clicks "Save Changes"
16. Update successful
17. QB #8 now shows "Metallic Teal" color
18. All actions logged

### Flow 5: Validation Failure - Missing Required Field

1. User selects Quick Button #4
2. Current config: Part "PART-A" (requires Color/WO), Operation "Weld", Color "Red", WO "WO-50"
3. Fields populate
4. User clears Operation field (testing)
5. Save button disables (Operation validation fails)
6. User attempts to click Save (button is disabled, no action)
7. User notices disabled state
8. User re-enters Operation "Assembly"
9. Save button enables
10. Flow continues normally

### Flow 6: Database Error During Save

1. User selects Quick Button #6
2. User makes changes (Part "NEW-PART", Operation "New-Op")
3. Save button enables
4. User clicks "Save Changes"
5. System calls DAO to update
6. Database connection fails (network issue)
7. DAO returns error result
8. Error message displays:  "Unable to update Quick Button due to a system error.  Please try again."
9. Form remains open
10. Quick Button #6 remains selected
11. Changes remain in input fields
12. Save button remains enabled (user can retry)
13. Error logged with full details
14. User waits, clicks "Save Changes" again
15. Connection restored, update succeeds
16. Success message displays
17. All actions logged

### Flow 7: No Quick Buttons to Edit

1. User clicks "Edit Quick Button" action button
2. Form opens with Edit control loaded
3. System checks for configured Quick Buttons
4. No Quick Buttons found (0 configured)
5. No Quick Buttons Panel displays
6. User sees message: "No Quick Buttons to Edit"
7. User sees guidance: "Use the Add Quick Button feature..."
8. Close button displayed
9. User clicks "Close"
10. Form closes
11. Action logged

### Flow 8: Editing Multiple Quick Buttons in Same Session

1. User opens Edit form with 5 configured Quick Buttons
2. User selects QB #1, makes changes, saves successfully
3. QB #1 display updates in left column
4. User (without closing form) selects QB #3
5. QB #1 deselects, QB #3 selects
6. Header and fields populate with QB #3 data
7. Original values updated to QB #3 current values
8. Reset disabled, Save disabled (no changes yet)
9. User makes changes to QB #3
10. User saves QB #3 successfully
11. User selects QB #5
12. Process repeats
13. User closes form when done
14. All edits logged separately

---

## Error Handling Requirements

### User-Facing Error Messages

**Validation Errors**:
- "Please select a valid Part Number"
- "Please select a valid Operation"
- "Please select or enter a valid Color Code"
- "Please enter a valid Work Order"
- "All required fields must be completed before saving"

**Data Load Errors**:
- "Unable to load Quick Buttons.  Please try again."
- "Unable to load Part information. Please try again."
- "Unable to retrieve Quick Button details. Please try again."

**Save Errors**:
- "Unable to update Quick Button due to a system error. Please try again."
- "This Quick Button no longer exists.  The list has been refreshed."
- "You do not have permission to edit this Quick Button." (authorization failure)
- "The selected Part is no longer active.  Please select a different Part."
- "The selected Operation is no longer active. Please select a different Operation."

**Custom Color Errors**:
- "Unable to add custom color. This color may already exist."
- "Custom color name cannot be empty."
- "Custom color name is too long (maximum 50 characters)."

**General Errors**:
- "An unexpected error occurred.  Please try again or contact support."

### Internal Error Handling

**All operations must**:
- Use centralized Service_ErrorHandler
- Never display technical error messages (stack traces, SQL errors, etc.) to users
- Log full error details for debugging
- Provide actionable user-friendly messages
- Allow retry where applicable
- Gracefully degrade (e.g., if one Quick Button fails to load, show others)

**Error Context Data** (logged):
- User ID
- Timestamp
- Operation name
- Quick Button ID
- Input values (Part, Operation, Color, Work Order)
- Original values (for comparison)
- Error message
- Stack trace
- Control name
- Method name

---

## Logging Requirements

### Events to Log

**Control Lifecycle**:
- Control initialization
- Control load start/completion
- Quick Button count on load
- Panel display decision (No Quick Buttons vs Edit Interface)

**User Actions**:
- Quick Button selection (which QB selected)
- Quick Button deselection
- Part Number field change
- Operation field change
- Color Code field change
- Work Order field change
- Reset button click
- Save button click
- Close button click
- Custom color entry dialog shown
- Custom color entry dialog result (OK/Cancel)

**Validation Events**:
- Validation failures (which field, why)
- Validation successes
- Button state changes (Save/Reset enabled/disabled)

**Database Operations**:
- Quick Buttons load attempt/success/failure
- Part metadata query attempt/success/failure
- Quick Button update attempt/success/failure
- Quick Button details query attempt/success/failure
- Custom color insert attempt/success/failure

**Conditional Logic**:
- Color Code field visibility change (shown/hidden, reason)
- Work Order field visibility change (shown/hidden, reason)
- Part metadata query results (requires Color?  requires WO?)

**Change Detection**:
- Changes detected (which fields changed)
- No changes detected
- Original values stored
- Original values updated after save

**UI Updates**:
- Left column refresh after save
- Parent display refresh trigger
- Header section update

### Log Format

All logs must include:
- Timestamp (ISO 8601 format)
- User ID
- Action name (descriptive constant or enum)
- Quick Button ID (when applicable)
- Context data (relevant field values, counts, etc.)
- Result (success/failure)
- Error details (if failure)
- Duration (for performance-sensitive operations)

**Example Log Entries**:
```
2025-12-09T14:32:15.123Z | JDKoll1982 | QUICK_BUTTON_EDIT_LOAD_START | QuickButtons:  7
2025-12-09T14:32:15.456Z | JDKoll1982 | QUICK_BUTTON_EDIT_LOAD_SUCCESS | Duration: 333ms
2025-12-09T14:32:20.789Z | JDKoll1982 | QUICK_BUTTON_SELECTED | QuickButtonId: 3
2025-12-09T14:32:35.123Z | JDKoll1982 | PART_FIELD_CHANGED | From: MTM-12345, To: MTM-67890
2025-12-09T14:32:35.234Z | JDKoll1982 | PART_METADATA_QUERY_SUCCESS | RequiresColor: true, RequiresWO: true
2025-12-09T14:32:45.567Z | JDKoll1982 | OPERATION_FIELD_CHANGED | From:  Welding, To:  Painting
2025-12-09T14:33:00.890Z | JDKoll1982 | SAVE_BUTTON_CLICKED | QuickButtonId: 3
2025-12-09T14:33:01.234Z | JDKoll1982 | QUICK_BUTTON_UPDATE_SUCCESS | QuickButtonId: 3, Duration: 344ms
```

---

## UI/UX Requirements

### Theme Integration
- Control inherits from ThemedUserControl base class
- All colors automatically applied by theme system
- No manual color assignments in control code
- Respects user's selected theme (light/dark)
- Theme changes apply dynamically if theme system supports it

### Accessibility
- Full keyboard navigation support
- Logical tab order (Left column Quick Buttons → Part → Operation → Color → Work Order → Reset → Save)
- Arrow keys navigate between Quick Buttons in left column (optional enhancement)
- Tooltips on all buttons
- Clear focus indicators on all interactive elements
- Screen reader friendly labels
- High contrast support via theming
- Visual indicators for required fields
- Error messages associated with fields (for screen readers)

### Responsiveness
- Form resizes gracefully based on content
- Left column scrolls if more than ~8 Quick Buttons
- Right column content stays within viewport
- Long Part/Operation descriptions truncate with ellipsis or wrap
- Suggestion dropdowns scroll if many options
- All operations < 2 seconds (target)
- UI remains responsive during database queries (async/await)
- Progress feedback for operations > 500ms (e.g., loading spinner on Save)

### Visual Feedback
- Clear hover effects on selectable Quick Buttons (left column)
- Prominent highlight on selected Quick Button (border or background)
- Disabled button states clearly visible (grayed out)
- Enabled button states have clear interactive styling
- Success messages clearly visible with appropriate styling (green/checkmark)
- Error messages clearly visible with warning styling (red/yellow)
- Field validation feedback (optional:  red border on invalid field)
- Loading indicators during async operations
- Smooth transitions (fade in/out, slide animations - optional)

### User Guidance
- "Select a Quick Button to edit" message when none selected
- Header section clearly shows "current" values for comparison
- Field labels clearly indicate what to enter
- Required fields indicated (asterisk or "Required" label)
- Tooltips provide additional guidance on hover
- Success/error messages are specific and actionable
- Button text is action-oriented ("Save Changes" not just "Save")

### Data Display
- Left column Quick Buttons display same format as main panel (consistency)
- Header section uses clear label/value pairs
- Truncate long text with ellipsis, show full text in tooltip
- Empty/null values displayed as "N/A" or "(None)" rather than blank
- Dates formatted consistently (if displayed)

---

## Performance Requirements

### Response Time Targets
- Initial load: < 500ms
- Quick Buttons list load: < 300ms
- Quick Button selection: < 50ms (should feel instant)
- Part metadata query: < 200ms
- Quick Button update (save): < 500ms
- Left column refresh after save: < 300ms
- Conditional field visibility update: < 100ms

### UI Responsiveness
- All database operations MUST be asynchronous
- UI MUST remain interactive during async operations
- NO UI freezing or "Not Responding" states
- Progress indicators for operations > 500ms
- Smooth animations (60 FPS target if animations used)

### Data Loading Optimization
- Load all user Quick Buttons once on initial load
- Cache loaded Quick Button data in memory
- Avoid redundant queries (e.g., don't re-query QB details if already loaded)
- Refresh only changed Quick Button after save (not entire list if possible)
- Use efficient database queries (indexed fields, minimal joins)
- Load suggestion dropdown data on-demand or cache after first load

### Memory Management
- Properly dispose of controls when no longer needed
- Unsubscribe from event handlers to prevent memory leaks
- Clear cached data when control disposed
- Monitor memory usage during long edit sessions

---

## Security Requirements

### Authorization
- Users can ONLY edit their own Quick Buttons
- User ID validation at database layer (stored procedure checks ownership)
- Authorization failures logged as security events
- No exposure of other users' Quick Button IDs in UI or logs

### Data Protection
- Input sanitization to prevent SQL injection (covered by stored procedures)
- Output encoding for user-entered data (e.g., custom color names)
- Audit trail of all edits (LastModifiedDate, optionally UserId in audit table)
- Original values logged before change (for audit/recovery)

### Input Validation
- Quick Button ID validated as positive integer
- User ID validated as non-empty string
- Part ID validated against allowed characters (alphanumeric, hyphens)
- Operation ID validated against allowed characters
- Color Code validated against allowed characters
- Work Order validated against allowed characters
- String length limits enforced (prevent buffer overflows, database errors)

### Concurrent Edit Handling
- Detect if Quick Button was modified by another session since load
- Option 1: Last-write-wins (simpler, current implementation assumption)
- Option 2: Optimistic locking (compare LastModifiedDate, reject if changed)
- If implementing Option 2: Show error "This Quick Button was modified by another user.  Your changes were not saved.  Please refresh and try again."

---

## Integration Requirements

### Parent Form Integration
- Form launched from Quick Buttons action button panel
- Passes action type (Edit) to hub form `Form_QuickButtonManagement`
- Hub form loads `Control_QuickButtonManagement_Edit` control
- Control refreshes parent Quick Buttons display after successful save
- Uses existing `Control_QuickButtons. MainFormInstance` pattern

### Sibling Feature Integration
- Shares same hub form container as Add and Remove features
- Consistent UI/UX patterns across Add/Edit/Remove
- Shared validation logic (reuse from Control_InventoryTab)
- Shared DAO methods where applicable (e.g., GetAllActiveParts)
- Similar logging patterns

### Database Integration
- All operations use stored procedures ONLY
- NO inline SQL permitted
- All queries use Helper_Database_StoredProcedure helper class
- All results wrapped in Model_Dao_Result<T>
- Transaction support for update operations
- Referential integrity maintained (foreign keys to Parts, Operations tables)

### Logging Integration
- Uses centralized LoggingUtility
- Structured log format (CSV as per project standard)
- Consistent log levels (Info, Warning, Error)
- 90-day retention minimum (as per constitution)
- Security events flagged appropriately

### Component Reuse
- **Component_SuggestionTextBoxWithLabel**: For all input fields
- **Control_QuickButton_Single**: For displaying Quick Buttons in left column
- **ThemedUserControl**: Base class for control
- **Service_ErrorHandler**: For all error handling
- **LoggingUtility**: For all logging
- **Helper_Database_StoredProcedure**: For all database access
- **Model_Dao_Result**: For all DAO return types

---

## Testing Requirements

### Unit Testing
- Validation logic for each field (Part, Operation, Color, Work Order)
- Change detection logic (various scenarios)
- Button enable/disable logic (Reset, Save)
- Conditional field visibility logic (Color, Work Order)
- Original values storage and retrieval
- Reset functionality (restore to original values)

### Integration Testing
- Database operations (load Quick Buttons, update Quick Button, query metadata)
- DAO method calls with various inputs
- Quick Button selection and data population
- Part metadata query and visibility changes
- Custom color entry and database insert
- Save operation and all side effects (refresh displays, update UI)
- Error handling paths (database failures, validation failures)

### UI Testing
- All user interaction flows (see User Interaction Flows section)
- Keyboard navigation and tab order
- Quick Button selection via mouse and keyboard
- Theme application and changes
- Responsive behavior (window resize, long text handling)
- Accessibility features (screen reader, high contrast)
- Visual feedback (hover, selection, disabled states)

### Security Testing
- Authorization validation (users can only edit own Quick Buttons)
- SQL injection attempts (should be blocked by stored procedures)
- Invalid Quick Button ID handling
- Cross-user Quick Button access attempts
- Input validation bypass attempts

### Edge Case Testing
- No Quick Buttons configured (No Quick Buttons Panel)
- Exactly 1 Quick Button (minimal left column)
- Exactly 10 Quick Buttons (maximum capacity)
- Very long Part/Operation names (truncation, wrapping)
- Special characters in input data (apostrophes, quotes, etc.)
- Empty optional fields (Color, Work Order null handling)
- Part that requires Color/WO changed to Part that doesn't (field hiding)
- Part that doesn't require Color/WO changed to Part that does (field showing)
- Network failures during database operations
- Database unavailable scenarios
- Concurrent edits (same Quick Button edited in two sessions)
- Rapid successive saves (prevent double-save)
- Attempting to save with no changes (button should be disabled)

### Performance Testing
- Load time with 1, 5, 10 Quick Buttons
- Quick Button selection response time
- Save operation response time
- Part metadata query response time
- Memory usage during extended editing session
- UI responsiveness during async operations

---

## Visual Studio Code WinForms Designer Compatibility

### Critical Requirements

**Designer File Structure**:
- All controls MUST be declared in Designer. cs file
- All control initialization MUST occur in InitializeComponent method
- Control properties MUST be settable via designer (avoid code-only properties)
- Avoid complex nested initialization that VS Code cannot parse

**Control Types Allowed**:
- Standard WinForms controls (Button, Label, TextBox, Panel, etc.)
- TableLayoutPanel (preferred for all layouts)
- Existing custom controls in project (Component_SuggestionTextBoxWithLabel, Control_QuickButton_Single)
- NO third-party controls not already in project
- NO custom-drawn controls unless already in project

**TableLayoutPanel Usage**:
- Use TableLayoutPanel for ALL layouts (main layout, sections, sub-sections)
- Avoid FlowLayoutPanel if possible (TableLayoutPanel more predictable in VS Code)
- Set all properties in designer (ColumnCount, RowCount, ColumnStyles, RowStyles)
- Use Dock = Fill for nested TableLayoutPanels
- Avoid complex anchoring (use Dock and TableLayoutPanel sizing instead)

**Property Settings**:
- Set all visual properties in designer where possible (Size, Location, Dock, Anchor, etc.)
- Avoid setting properties in constructor or load event unless necessary (e.g., dynamic content)
- Use designer-friendly property values (avoid enums not recognized by VS Code)

**Control Naming**:
- Follow project naming conventions strictly
- All controls MUST have meaningful names (no "button1", "label2")
- Names MUST match pattern: `{ControlName}_{ControlType}_{Name}_{Number? }`

**Testing in VS Code**:
- After creating Designer file, open in VS Code WinForms Editor
- Verify all controls display correctly
- Verify properties can be edited
- Verify layout renders as expected
- Fix any parsing errors or display issues before finalizing

**Common Pitfalls to Avoid**:
- Complex inheritance beyond direct ThemedUserControl inheritance
- Custom designer attributes (Designer Serialization Visibility)
- Controls added programmatically that should be in designer
- Lambda expressions in designer-generated code
- Complex initialization logic in InitializeComponent
- Resource files referenced incorrectly

---

## Success Criteria

### Functional Requirements Met
- ✅ Users can edit Quick Buttons when configured
- ✅ No Quick Buttons panel displays when none configured
- ✅ Left column displays all user Quick Buttons in familiar format
- ✅ Selection mechanism is intuitive and responsive
- ✅ Header section clearly shows current values for comparison
- ✅ Input fields populate with current values on selection
- ✅ Conditional fields (Color, Work Order) show/hide based on Part selection
- ✅ Change detection prevents saving with no changes
- ✅ Reset functionality restores original values
-