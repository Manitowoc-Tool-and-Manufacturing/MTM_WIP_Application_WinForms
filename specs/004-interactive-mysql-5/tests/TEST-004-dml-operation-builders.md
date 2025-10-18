# Manual Test: DML Operation Builders

**Test ID**: TEST-004  
**User Story**: US7 - DML Operation Builders with Auto-Complete (Priority P1)  
**Feature**: Interactive MySQL 5.7 Stored Procedure Builder  
**Test Type**: Functional Test  
**Estimated Duration**: 25 minutes

---

## Test Objective

Verify that visual builders for INSERT, UPDATE, DELETE, and SELECT operations function correctly, populate from database metadata, show live SQL preview, handle WHERE clauses safely, and support ON DUPLICATE KEY UPDATE for INSERT operations.

---

## Prerequisites

- [ ] TEST-003 passed (database metadata integration working)
- [ ] MAMP with MySQL 5.7 running
- [ ] Test database has Inventory, Parts, Locations tables
- [ ] Wizard Steps 1-3 functional

---

## Test Steps

### INSERT Operation Builder

#### Step 1: Create Basic INSERT Operation

1. Start wizard, proceed to Step 4 "DML Operations"
2. Click "Add Operation" → Select "INSERT"
3. Select table: "Inventory"
4. Check columns: PartNumber, Quantity, LocationCode, ReceivedDate, ReceivedUser, IsActive
5. Fill values:
   - PartNumber: `p_PartNumber`
   - Quantity: `p_Quantity`
   - LocationCode: `p_LocationCode`
   - ReceivedDate: `NOW()` (should be pre-filled)
   - ReceivedUser: `p_UserID` (should be pre-filled)
   - IsActive: `1` (should be pre-filled)

**Expected Result**:
- [ ] Column checkboxes populate from database
- [ ] InventoryID (auto-increment) grayed out
- [ ] Smart defaults pre-fill for date/user/active columns
- [ ] Live SQL preview shows: `INSERT INTO Inventory (PartNumber, Quantity, LocationCode, ReceivedDate, ReceivedUser, IsActive) VALUES (p_PartNumber, p_Quantity, p_LocationCode, NOW(), p_UserID, 1);`
- [ ] Syntax highlighting applied in preview

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 2: Test Column Selection Changes

1. Uncheck "ReceivedDate" column
2. Check "LastUpdated" column instead

**Expected Result**:
- [ ] SQL preview updates immediately (debounced ~300ms)
- [ ] ReceivedDate removed from column list
- [ ] LastUpdated added with smart default `NOW()`
- [ ] VALUES list matches column list length

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 3: Test ON DUPLICATE KEY UPDATE

1. Toggle "On Duplicate Key Update" switch ON
2. Second column mapping section appears
3. Check UPDATE columns: Quantity, LastUpdated
4. Set values:
   - Quantity: `Quantity + p_Quantity`
   - LastUpdated: `NOW()`

**Expected Result**:
- [ ] ON DUPLICATE KEY UPDATE section shows
- [ ] Column checkboxes populate
- [ ] SQL preview appends: `ON DUPLICATE KEY UPDATE Quantity = Quantity + p_Quantity, LastUpdated = NOW();`
- [ ] Syntax correct for MySQL 5.7

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### UPDATE Operation Builder

#### Step 4: Create Basic UPDATE Operation

1. Add new operation → Select "UPDATE"
2. Select table: "Inventory"
3. SET clause: Check columns Quantity, LastUpdatedUser
4. Set values:
   - Quantity: `Quantity + p_QuantityDelta`
   - LastUpdatedUser: `p_UserID`
5. WHERE clause: Add condition `InventoryID = p_InventoryID`

**Expected Result**:
- [ ] SET column checkboxes show all updatable columns
- [ ] WHERE clause builder provides:
  - Column dropdown
  - Operator dropdown (=, !=, >, <, >=, <=, LIKE, IN)
  - Value field with parameter suggestions
- [ ] SQL preview: `UPDATE Inventory SET Quantity = Quantity + p_QuantityDelta, LastUpdatedUser = p_UserID WHERE InventoryID = p_InventoryID;`

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 5: Test Multiple WHERE Conditions

1. Add second WHERE condition: `IsActive = 1`
2. Select AND/OR operator between conditions

**Expected Result**:
- [ ] Multiple condition support
- [ ] AND/OR toggle between conditions
- [ ] SQL preview: `WHERE InventoryID = p_InventoryID AND IsActive = 1`
- [ ] Parentheses grouping (if complex logic)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 6: Test UPDATE Without WHERE Warning

1. Remove all WHERE conditions
2. Attempt to save operation or proceed

**Expected Result**:
- [ ] Warning dialog appears: "UPDATE without WHERE clause will affect all rows. Are you sure?"
- [ ] Options: "Add WHERE Condition" (returns to builder), "Continue Anyway" (risky), "Cancel"
- [ ] If "Continue Anyway", SQL includes comment: `-- WARNING: No WHERE clause`

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 7: Test ROW_COUNT Tracking

1. In UPDATE operation, check "Track rows affected" option

**Expected Result**:
- [ ] After UPDATE statement, SQL adds: `SET v_RowsAffected = ROW_COUNT();`
- [ ] Local variable v_RowsAffected declared in DECLARE section
- [ ] Can use v_RowsAffected in subsequent validation (IF v_RowsAffected = 0...)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### DELETE Operation Builder

#### Step 8: Create DELETE Operation

1. Add operation → Select "DELETE"
2. Select table: "Inventory"
3. WHERE clause: Add condition `InventoryID = p_InventoryID`
4. Add second condition: `IsActive = 0` (soft delete check)

**Expected Result**:
- [ ] No SET clause section (DELETE doesn't have SET)
- [ ] WHERE clause builder same as UPDATE
- [ ] SQL preview: `DELETE FROM Inventory WHERE InventoryID = p_InventoryID AND IsActive = 0;`

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 9: Test DELETE Without WHERE Warning

1. Remove WHERE conditions from DELETE
2. Attempt to save

**Expected Result**:
- [ ] Critical warning: "DELETE without WHERE clause will remove ALL rows from table. This is usually a mistake."
- [ ] Red/danger styling on warning
- [ ] Options: "Add WHERE Condition" (default), "I understand the risk - Continue" (requires confirmation checkbox)
- [ ] Stronger warning than UPDATE (more destructive)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### SELECT Operation Builder

#### Step 10: Create Basic SELECT Operation

1. Add operation → Select "SELECT"
2. Select table: "Parts"
3. Check columns: PartID, PartNumber, Description
4. Add WHERE: `IsActive = 1`
5. Add ORDER BY: `PartNumber ASC`
6. Set LIMIT: `10`

**Expected Result**:
- [ ] Column checkboxes for selecting output columns
- [ ] "Select All" checkbox to check/uncheck all
- [ ] WHERE clause builder (same as UPDATE/DELETE)
- [ ] ORDER BY dropdown with ASC/DESC
- [ ] LIMIT input field
- [ ] SQL preview: `SELECT PartID, PartNumber, Description FROM Parts WHERE IsActive = 1 ORDER BY PartNumber ASC LIMIT 10;`

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 11: Test SELECT with JOIN

1. In SELECT operation, click "Add JOIN"
2. Select join type: INNER JOIN
3. Select join table: Locations
4. ON clause: `Parts.LocationCode = Locations.LocationCode`
5. Select additional columns from Locations: LocationName

**Expected Result**:
- [ ] JOIN builder section appears
- [ ] Join type dropdown: INNER JOIN, LEFT JOIN, RIGHT JOIN
- [ ] Join table dropdown populated from database
- [ ] ON clause builder with table.column dropdowns
- [ ] Column list shows columns from both tables (prefixed with table name)
- [ ] SQL preview: `SELECT p.PartID, p.PartNumber, p.Description, l.LocationName FROM Parts p INNER JOIN Locations l ON p.LocationCode = l.LocationCode WHERE p.IsActive = 1 ORDER BY p.PartNumber ASC LIMIT 10;`
- [ ] Table aliases auto-generated (p, l)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 12: Test SELECT INTO Variable

1. In SELECT operation, toggle "Select INTO variable" ON
2. Enter variable name: `v_PartCount`
3. Modify SELECT to: `COUNT(*) AS PartCount`

**Expected Result**:
- [ ] Variable name field appears
- [ ] v_ prefix auto-added if omitted
- [ ] SQL preview: `SELECT COUNT(*) INTO v_PartCount FROM Parts WHERE IsActive = 1;`
- [ ] Variable v_PartCount declared in DECLARE section

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Auto-Complete and Suggestions

#### Step 13: Test Parameter Auto-Complete

1. In value field, type `p_`
2. Observe auto-complete dropdown

**Expected Result**:
- [ ] Dropdown shows all defined parameters starting with p_
- [ ] Arrow keys navigate list
- [ ] Enter or click selects parameter
- [ ] Parameter inserted into field

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 14: Test Column Name Auto-Complete

1. In WHERE condition value field, start typing column name
2. Observe suggestions

**Expected Result**:
- [ ] Columns from selected table suggested
- [ ] Case-insensitive matching
- [ ] Fuzzy matching (typing "part" suggests "PartNumber")

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Operation Management

#### Step 15: Test Operation Reordering

1. Add 3 operations: SELECT, UPDATE, INSERT
2. Use move up/down buttons to reorder
3. Move UPDATE to first position

**Expected Result**:
- [ ] Operations list updates order visually
- [ ] SQL preview shows operations in new order
- [ ] Up/down buttons enabled/disabled appropriately (can't move top item up)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 16: Test Operation Editing

1. Click "Edit" button on existing operation
2. Modify SET clause in UPDATE
3. Save changes

**Expected Result**:
- [ ] Operation builder reopens with existing values
- [ ] Changes save successfully
- [ ] Operation summary in list updates
- [ ] SQL preview reflects changes

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

#### Step 17: Test Operation Deletion

1. Click "Delete" button on operation
2. Confirm deletion

**Expected Result**:
- [ ] Confirmation dialog: "Delete this operation?"
- [ ] Operation removed from list
- [ ] SQL preview updates (operation removed)
- [ ] Remaining operations renumbered

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Live Preview Updates

#### Step 18: Test Preview Debouncing

1. In INSERT operation, rapidly type in value field
2. Observe SQL preview updates

**Expected Result**:
- [ ] Preview does NOT update on every keystroke (too expensive)
- [ ] Preview updates ~300ms after typing stops (debounced)
- [ ] Loading indicator shows briefly during generation
- [ ] No performance lag or UI freeze

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

## Success Criteria

**Test passes if**:
- All 4 operation types (INSERT/UPDATE/DELETE/SELECT) build correctly
- WHERE clause builder prevents unsafe operations without warnings
- JOINs and complex SELECT features work
- Auto-complete reduces typing by ~50% (per spec SC-006)
- Live preview updates within 500ms after changes stop
- Operations can be reordered and edited

---

## Acceptance Scenario Mapping

This test validates **US7 Acceptance Scenarios**:

1. ✅ **Scenario 1**: Developer selects UPDATE and target table Inventory → Checks Quantity, LastUpdatedUser columns → Maps values → Builds WHERE clause → SQL preview shows UPDATE statement

2. ✅ **Scenario 2**: Developer builds WHERE clause → Adds condition InventoryID = p_InventoryID → SQL preview shows UPDATE with WHERE and tracks ROW_COUNT() into v_RowsAffected

3. ✅ **Scenario 3**: Developer builds UPDATE without WHERE → System shows warning with option to add WHERE condition

4. ✅ **Scenario 4**: Developer selects INSERT operation → Uses ON DUPLICATE KEY UPDATE toggle → Builder adds second mapping section and generates INSERT...ON DUPLICATE KEY UPDATE syntax

---

## Defects Found

| ID | Severity | Description | Steps to Reproduce | Status |
|----|----------|-------------|-------------------|--------|
| | | | | |

---

## Test Evidence

**Screenshots**:
- [ ] INSERT builder with column selection
- [ ] UPDATE builder with WHERE clause
- [ ] DELETE warning dialog (no WHERE)
- [ ] SELECT builder with JOIN
- [ ] Auto-complete dropdown showing parameters
- [ ] SQL preview with all 4 operation types

---

## Notes

_Record observations about builder usability, performance, edge cases_

---

## Tester Information

**Tester Name**: _________________________  
**Test Date**: _________________________  

**Overall Test Result**: ☐ PASS ☐ FAIL ☐ BLOCKED

**Sign-off**: _________________________ Date: _____________
