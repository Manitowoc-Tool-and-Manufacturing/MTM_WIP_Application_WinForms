# Manual Test: Create New Stored Procedure from Scratch

**Test ID**: TEST-001  
**User Story**: US1 - Create New Stored Procedure from Scratch (Priority P1)  
**Feature**: Interactive MySQL 5.7 Stored Procedure Builder  
**Test Type**: End-to-End Functional Test  
**Estimated Duration**: 15 minutes

---

## Test Objective

Verify that a developer can create a complete single-table stored procedure from scratch using the wizard interface, including parameter definition, validation rules, DML operations, and SQL export without writing manual SQL.

---

## Prerequisites

- [ ] MAMP running with MySQL 5.7 on localhost:3306
- [ ] Database `mtm_wip_application_winforms_test` exists and is accessible
- [ ] Chrome browser 86+ installed
- [ ] Stored procedure builder application accessible at `http://localhost:8888/StoredProcedureValidation/sp-builder/`
- [ ] `Inventory` table exists in test database with columns: InventoryID, PartNumber, Quantity, LocationCode, ReceivedDate, ReceivedUser, IsActive

---

## Test Data

**Procedure Name**: `inv_inventory_Adjust_Quantity`  
**Parameters**:
- `p_InventoryID` INT IN
- `p_QuantityDelta` INT IN
- `p_UserID` INT IN
- `p_Status` INT OUT (auto-included)
- `p_ErrorMsg` VARCHAR(500) OUT (auto-included)

**Validation Rules**:
1. Check `p_QuantityDelta` > 0 → Status -1, "Quantity delta must be positive"
2. Check inventory record exists → Status -2, "Inventory record not found"

**DML Operations**:
1. UPDATE `Inventory` SET `Quantity = Quantity + p_QuantityDelta`, `LastUpdatedUser = p_UserID` WHERE `InventoryID = p_InventoryID`

---

## Test Steps

### Step 1: Access Application

1. Open Chrome browser
2. Navigate to `http://localhost:8888/StoredProcedureValidation/sp-builder/`
3. Verify index.html loads without errors

**Expected Result**:
- [ ] Main navigation menu displays with links: Wizard, Templates, Help
- [ ] No JavaScript console errors
- [ ] Database connection status indicator shows "Connected" or prompts for connection

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 2: Start Wizard

1. Click "New Procedure" or "Start Wizard" button
2. Verify wizard.html loads

**Expected Result**:
- [ ] Wizard interface displays with 8 steps visible in progress indicator
- [ ] Step 1 "Procedure Name" is active/highlighted
- [ ] Progress indicator shows 0% or "Step 1 of 8"

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 3: Enter Procedure Name

1. In Step 1, enter procedure name: `inv_inventory_Adjust_Quantity`
2. Tab out of field or click elsewhere

**Expected Result**:
- [ ] Name validates successfully (green checkmark or no error)
- [ ] No error message displayed (domain_table_action pattern is valid)
- [ ] "Next" button becomes enabled

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 4: Test Name Validation (Negative Test)

1. Change procedure name to: `InvalidName` (no underscores)
2. Tab out of field

**Expected Result**:
- [ ] Red error message appears: "Procedure name must follow pattern: domain_table_action"
- [ ] "Next" button remains disabled
- [ ] Example shown: "Example: inv_inventory_Add_Item"

3. Correct name back to: `inv_inventory_Adjust_Quantity`
4. Verify error clears

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 5: Add Parameters

1. Click "Next" to proceed to Step 2 "Parameters"
2. Verify p_Status INT OUT and p_ErrorMsg VARCHAR(500) OUT are already listed and grayed out
3. Click "Add Parameter" button
4. Fill in parameter form:
   - Name: `p_InventoryID` (or `InventoryID` - system should add `p_` prefix)
   - Direction: IN (radio button)
   - Data Type: INT (dropdown)
   - Description: "Inventory record ID"
5. Click "Add" or "Save Parameter"
6. Repeat for `p_QuantityDelta INT IN` and `p_UserID INT IN`

**Expected Result**:
- [ ] Parameter list displays 5 parameters total (3 custom + 2 auto-included)
- [ ] p_ prefix automatically added if omitted
- [ ] Data types displayed correctly in list (INT, VARCHAR(500))
- [ ] p_Status and p_ErrorMsg cannot be removed (delete button disabled/hidden)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 6: Add Validation Rules

1. Click "Next" to proceed to Step 3 "Validation Rules"
2. Locate validation palette with rule type cards
3. Click or drag "Positive Number" validation card
4. Configure validation:
   - Parameter: `p_QuantityDelta` (dropdown)
   - Error Message: "Quantity delta must be positive"
   - Status Code: -1 (dropdown)
5. Click "Add Validation"
6. Add second validation: "Foreign Key Check"
   - Parameter: `p_InventoryID`
   - Reference Table: `Inventory`
   - Reference Column: `InventoryID`
   - Error Message: "Inventory record not found"
   - Status Code: -2

**Expected Result**:
- [ ] Validation rules list shows 2 active rules
- [ ] Rules can be reordered with up/down buttons or drag-drop
- [ ] Each rule shows type, parameter, error message in summary
- [ ] "Preview SQL" panel updates showing IF blocks with ROLLBACK

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 7: Add DML Operation (UPDATE)

1. Click "Next" to proceed to Step 4 "DML Operations"
2. Click "Add Operation" button
3. Select operation type: UPDATE
4. Fill in UPDATE form:
   - Table: `Inventory` (dropdown - should populate from database)
   - SET Columns: Check `Quantity` and `LastUpdatedUser`
   - Quantity value: `Quantity + p_QuantityDelta`
   - LastUpdatedUser value: `p_UserID`
   - WHERE Conditions: Add condition `InventoryID = p_InventoryID`
5. Click "Add Operation"

**Expected Result**:
- [ ] Table dropdown shows tables from mtm_wip_application_winforms_test
- [ ] Column checkboxes populate after table selection
- [ ] Smart default suggestions appear (LastUpdatedUser suggests p_UserID)
- [ ] WHERE clause builder allows adding multiple conditions with AND/OR
- [ ] Operation appears in operations list with summary
- [ ] Live SQL preview updates showing UPDATE statement

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 8: Review Flow Diagram (Optional)

1. Click "Next" to proceed to Step 5 "Flow Diagram"
2. Verify visual flow diagram displays

**Expected Result**:
- [ ] Canvas shows nodes for validation rules (2 nodes) and DML operation (1 node)
- [ ] Nodes connected by arrows showing execution sequence
- [ ] Validation nodes orange, operation nodes blue
- [ ] Zoom/pan controls work (mouse wheel, drag)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 9: Preview Generated SQL

1. Click "Next" to proceed to Step 7 "Preview" (skip Step 6 Advanced if present)
2. Review generated SQL in syntax-highlighted preview pane

**Expected Result**:
- [ ] SQL includes DELIMITER $$ at top
- [ ] SQL includes DROP PROCEDURE IF EXISTS inv_inventory_Adjust_Quantity;
- [ ] SQL includes CREATE PROCEDURE inv_inventory_Adjust_Quantity(...)
- [ ] All 5 parameters declared correctly
- [ ] START TRANSACTION present
- [ ] Two IF blocks for validation with ROLLBACK
- [ ] UPDATE statement with SET and WHERE clauses
- [ ] COMMIT at end
- [ ] Status set to 0 (success) before COMMIT
- [ ] DELIMITER ; at bottom
- [ ] Syntax highlighting applied (keywords blue, strings green, etc.)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 10: Validate SQL Syntax

1. Click "Validate Syntax" button in preview step
2. Wait for validation result

**Expected Result**:
- [ ] Loading spinner appears during validation
- [ ] Success message: "SQL syntax is valid for MySQL 5.7"
- [ ] No red underlines or error markers in SQL preview
- [ ] "Export" button becomes enabled

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 11: Export SQL File

1. Click "Export" button
2. If File System Access API available, choose save location
3. If fallback, file downloads automatically

**Expected Result**:
- [ ] File save dialog appears (Chrome 86+) or file downloads
- [ ] Filename defaults to: `inv_inventory_Adjust_Quantity.sql`
- [ ] File saved successfully
- [ ] Confirmation dialog appears with file path
- [ ] Dialog includes "Run Analysis Script" button/prompt

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 12: Execute Exported SQL in MySQL

1. Open exported .sql file in text editor
2. Copy entire contents
3. Open MySQL Workbench or command-line client
4. Connect to mtm_wip_application_winforms_test database
5. Paste and execute SQL

**Expected Result**:
- [ ] SQL executes without syntax errors
- [ ] Procedure created successfully
- [ ] Procedure visible in database: `SHOW PROCEDURE STATUS WHERE Name = 'inv_inventory_Adjust_Quantity';`
- [ ] Can call procedure: `CALL inv_inventory_Adjust_Quantity(1, 5, 100, @status, @msg);`
- [ ] Validation rules work correctly

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 13: Test Auto-Save and Session Recovery

1. Return to wizard (do not export yet)
2. Modify procedure name to: `inv_inventory_Adjust_Quantity_v2`
3. Wait 30 seconds for auto-save
4. Refresh browser page (F5)

**Expected Result**:
- [ ] Restore session prompt appears: "Resume 'inv_inventory_Adjust_Quantity_v2'? Last edited: X seconds ago"
- [ ] Click "Resume"
- [ ] Wizard restores to last step with all data intact
- [ ] All parameters, validations, operations preserved

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

## Success Criteria

**Test passes if**:
- All 13 steps complete without critical failures
- Generated SQL is syntactically valid for MySQL 5.7
- Exported procedure executes successfully in database
- Auto-save and session recovery work correctly
- Total time to create procedure: < 10 minutes (per spec SC-001)

---

## Acceptance Scenario Mapping

This test validates **US1 Acceptance Scenarios**:

1. ✅ **Scenario 1**: Developer enters procedure name following pattern, adds parameters (p_InventoryID, p_QuantityDelta, p_UserID, auto p_Status, p_ErrorMsg), defines UPDATE operation → System generates valid MySQL 5.7 SQL with DELIMITER, transaction control, status handling

2. ✅ **Scenario 2**: Procedure definition complete → Developer clicks Export → System generates .sql file named correctly, includes header comment with metadata, includes DROP PROCEDURE IF EXISTS, prompts to run analysis script

3. ✅ **Scenario 3**: Developer defines validation rules (p_QuantityDelta > 0, inventory exists) → Validation fails → Generated SQL includes ROLLBACK, sets p_Status to error code, sets p_ErrorMsg to developer-defined message

---

## Defects Found

| ID | Severity | Description | Steps to Reproduce | Status |
|----|----------|-------------|-------------------|--------|
| | | | | |

---

## Test Evidence

**Screenshots**:
- [ ] Wizard Step 1 (Procedure Name)
- [ ] Wizard Step 2 (Parameters list with 5 parameters)
- [ ] Wizard Step 3 (Validation rules)
- [ ] Wizard Step 4 (DML operations)
- [ ] Wizard Step 7 (SQL Preview with syntax highlighting)
- [ ] Export confirmation dialog
- [ ] MySQL Workbench showing procedure created successfully

**Exported Files**:
- [ ] `inv_inventory_Adjust_Quantity.sql` attached

---

## Notes

_Record any observations, edge cases discovered, or recommendations here_

---

## Tester Information

**Tester Name**: _________________________  
**Test Date**: _________________________  
**Test Environment**: 
- OS: Windows 11
- Browser: Chrome _____ (version)
- MAMP Version: _____
- MySQL Version: 5.7.___

**Overall Test Result**: ☐ PASS ☐ FAIL ☐ BLOCKED

**Sign-off**: _________________________ Date: _____________
