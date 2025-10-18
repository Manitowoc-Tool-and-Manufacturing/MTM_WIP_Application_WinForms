# Manual Test: Edit Existing Stored Procedure

**Test ID**: TEST-002  
**User Story**: US2 - Edit Existing Stored Procedure (Priority P1)  
**Feature**: Interactive MySQL 5.7 Stored Procedure Builder  
**Test Type**: End-to-End Functional Test  
**Estimated Duration**: 20 minutes

---

## Test Objective

Verify that a developer can import an existing stored procedure from a .sql file, the wizard correctly parses and pre-populates all fields (parameters, validation rules, DML operations), modifications can be made, and a side-by-side comparison shows changes before export.

---

## Prerequisites

- [ ] TEST-001 completed successfully (basic create flow works)
- [ ] MAMP running with MySQL 5.7 on localhost:3306
- [ ] Chrome browser 86+ installed
- [ ] Sample procedure file exists: `inv_inventory_Add_Item.sql` with known structure
- [ ] Stored procedure builder application accessible

---

## Test Data

**Existing Procedure to Import**: `inv_inventory_Add_Item`

```sql
DELIMITER $$

DROP PROCEDURE IF EXISTS inv_inventory_Add_Item$$

CREATE PROCEDURE inv_inventory_Add_Item(
    IN p_PartNumber VARCHAR(50),
    IN p_Quantity DECIMAL(10,2),
    IN p_LocationCode VARCHAR(20),
    IN p_UserID INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    
    START TRANSACTION;
    
    -- Validation: Check if part exists
    SELECT COUNT(*) INTO v_Exists 
    FROM Parts 
    WHERE PartNumber = p_PartNumber;
    
    IF v_Exists = 0 THEN
        ROLLBACK;
        SET p_Status = -1;
        SET p_ErrorMsg = 'Part number does not exist';
    ELSE
        -- Insert inventory record
        INSERT INTO Inventory (
            PartNumber, Quantity, LocationCode, 
            ReceivedDate, ReceivedUser, IsActive
        ) VALUES (
            p_PartNumber, p_Quantity, p_LocationCode,
            NOW(), p_UserID, 1
        );
        
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
        COMMIT;
    END IF;
END$$

DELIMITER ;
```

**Modification to Make**: Add new validation check for quantity > 0

---

## Test Steps

### Step 1: Access Import Functionality

1. Open stored procedure builder at index.html
2. Locate "Import Existing Procedure" button or link
3. Click to open import dialog

**Expected Result**:
- [ ] Import modal/dialog appears
- [ ] Dialog contains textarea for pasting SQL
- [ ] Dialog contains file upload button
- [ ] "Import" button present

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 2: Import via File Upload

1. Click "Upload File" button
2. Select `inv_inventory_Add_Item.sql` from file system
3. Click "Import" or "Load"

**Expected Result**:
- [ ] File contents load into textarea
- [ ] No JavaScript errors in console
- [ ] "Parse Procedure" or "Continue" button becomes enabled

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 3: Parse Imported SQL

1. Click "Parse Procedure" button
2. Wait for parsing to complete

**Expected Result**:
- [ ] Success message: "Procedure parsed successfully"
- [ ] Wizard automatically opens/redirects to step 1
- [ ] Progress indicator shows data loaded

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 4: Verify Procedure Name Parsed

1. View Wizard Step 1 "Procedure Name"

**Expected Result**:
- [ ] Procedure name field populated: `inv_inventory_Add_Item`
- [ ] Name validation passes (green checkmark)
- [ ] Field is editable (can modify if needed)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 5: Verify Parameters Parsed

1. Navigate to Step 2 "Parameters"

**Expected Result**:
- [ ] Parameter list shows 6 parameters:
  - p_PartNumber VARCHAR(50) IN
  - p_Quantity DECIMAL(10,2) IN
  - p_LocationCode VARCHAR(20) IN
  - p_UserID INT IN
  - p_Status INT OUT
  - p_ErrorMsg VARCHAR(500) OUT
- [ ] All types and directions correctly identified
- [ ] VARCHAR lengths parsed correctly (50, 20, 500)
- [ ] DECIMAL precision/scale parsed correctly (10,2)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 6: Verify Local Variables Parsed

1. Check if DECLARE statements were parsed
2. Look for variable display section (may be in Advanced step or separate section)

**Expected Result**:
- [ ] Local variable `v_Exists` INT DEFAULT 0 identified
- [ ] Variable type and default value correctly captured

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 7: Verify Validation Rules Parsed

1. Navigate to Step 3 "Validation Rules"

**Expected Result**:
- [ ] One validation rule present: "Foreign Key Check" or "Custom Validation"
- [ ] Rule shows: Check if part exists (SELECT COUNT(*) INTO v_Exists FROM Parts WHERE PartNumber = p_PartNumber)
- [ ] Error message: "Part number does not exist"
- [ ] Status code: -1
- [ ] ROLLBACK behavior captured

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 8: Verify DML Operations Parsed

1. Navigate to Step 4 "DML Operations"

**Expected Result**:
- [ ] One INSERT operation present
- [ ] Table: Inventory
- [ ] Columns: PartNumber, Quantity, LocationCode, ReceivedDate, ReceivedUser, IsActive
- [ ] Values: p_PartNumber, p_Quantity, p_LocationCode, NOW(), p_UserID, 1
- [ ] All column-to-value mappings correct

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 9: Verify Transaction Control Parsed

1. Check if START TRANSACTION and COMMIT were identified
2. Look for transaction settings (may be in Advanced or settings area)

**Expected Result**:
- [ ] Transaction control enabled
- [ ] START TRANSACTION present before operations
- [ ] COMMIT present after operations

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 10: Add New Validation Rule

1. In Step 3 "Validation Rules", click "Add Validation"
2. Select "Positive Number" validation type
3. Configure:
   - Parameter: p_Quantity
   - Error Message: "Quantity must be greater than zero"
   - Status Code: -2
4. Save validation
5. Drag to position BEFORE existing part number validation

**Expected Result**:
- [ ] New validation added to list
- [ ] Validation positioned first in execution order
- [ ] SQL preview updates showing new IF block before existing validation

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 11: View Side-by-Side Comparison

1. Navigate to Step 7 "Preview"
2. Click "Show Comparison" or "Compare with Original" button
3. View diff display

**Expected Result**:
- [ ] Split-pane view shows original SQL (left) vs modified SQL (right)
- [ ] Changes highlighted with colors:
  - Green: Added lines (new validation IF block)
  - Red: Removed lines (none expected)
  - Yellow: Modified lines (IF condition order may change)
- [ ] Line numbers shown for both versions
- [ ] Can scroll both panes independently

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 12: Verify Modified SQL Syntax

1. Click "Validate Syntax" button
2. Wait for validation

**Expected Result**:
- [ ] Validation succeeds: "SQL syntax is valid for MySQL 5.7"
- [ ] No errors in modified SQL
- [ ] New validation block syntax correct

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 13: Export Modified Procedure

1. Click "Export" button
2. Save file as `inv_inventory_Add_Item_modified.sql`

**Expected Result**:
- [ ] File exports successfully
- [ ] Filename suggests modification or version (v2, modified, etc.)
- [ ] Export confirmation shown

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 14: Verify Header Comment Updated

1. Open exported `inv_inventory_Add_Item_modified.sql`
2. Read header comment block

**Expected Result**:
- [ ] Header includes:
  - Procedure Name: inv_inventory_Add_Item
  - Description: (original description if present)
  - Modified Date: (current timestamp)
  - Version: (incremented if versioning enabled)
  - Parameters: (all 6 listed with types)
  - Tables Accessed: Parts, Inventory

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 15: Execute Modified Procedure in MySQL

1. Drop existing procedure: `DROP PROCEDURE IF EXISTS inv_inventory_Add_Item;`
2. Execute modified SQL from exported file
3. Test with valid call: `CALL inv_inventory_Add_Item('PART001', 10, 'FLOOR', 1, @s, @m);`
4. Test with negative quantity: `CALL inv_inventory_Add_Item('PART001', -5, 'FLOOR', 1, @s, @m);`
5. Check status: `SELECT @s AS Status, @m AS Message;`

**Expected Result**:
- [ ] Procedure creates without errors
- [ ] Valid call succeeds (Status = 0)
- [ ] Negative quantity call fails with Status = -2, Message = "Quantity must be greater than zero"
- [ ] New validation executes BEFORE part existence check

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 16: Test Import via Copy-Paste

1. Return to import dialog
2. Copy SQL text from file
3. Paste into textarea
4. Click "Import"

**Expected Result**:
- [ ] SQL parses successfully (same as file upload)
- [ ] All fields populate correctly
- [ ] No difference in behavior between file upload and paste methods

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 17: Test Import Error Handling

1. Import invalid SQL: `CREATE PROCEDURE test() BEGIN SELEC * FROM Inventory; END`
2. Click "Parse Procedure"

**Expected Result**:
- [ ] Error message displayed: "Unable to parse procedure. Check SQL syntax."
- [ ] Specific error highlighted if possible: "Unrecognized keyword: SELEC"
- [ ] Import dialog remains open for correction
- [ ] Can edit SQL and retry

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 18: Test Complex Procedure Parsing

**Note**: This step requires a more complex procedure file with multiple operations

1. Import procedure with:
   - 5+ parameters
   - 3+ validation rules
   - Multiple DML operations (INSERT, UPDATE, SELECT)
   - Cursor or loop (if applicable)

**Expected Result**:
- [ ] Parser handles complex procedures
- [ ] All parameters extracted
- [ ] All validation rules identified
- [ ] All DML operations in correct sequence
- [ ] Advanced features (if present) recognized

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

## Success Criteria

**Test passes if**:
- SQL parser correctly extracts procedure name, parameters, validation rules, DML operations
- At least 90% of imported procedure elements populate correctly (per spec SC-003)
- Modified procedure can be exported and executes successfully
- Side-by-side comparison shows changes clearly with color coding
- Import works via both file upload and copy-paste methods

---

## Acceptance Scenario Mapping

This test validates **US2 Acceptance Scenarios**:

1. ✅ **Scenario 1**: Developer opens procedure review tool, selects procedure, clicks "Edit in Builder" → Builder parses existing SQL and pre-populates procedure name, all IN/OUT parameters with types, DECLARE statements as variables, validation checks in order, all DML operations with table/column mappings, transaction control settings

2. ✅ **Scenario 2**: Developer modifies validation logic (adds new check for p_Quantity > 0) → Developer views Preview → Preview shows side-by-side diff highlighting added validation block in green

3. ✅ **Scenario 3**: Visual flow diagram shows 4 operations (validate, check duplicate, INSERT, log) → Developer drags INSERT operation before check duplicate → Generated SQL reflects new operation order with INSERT before duplicate key check

---

## Defects Found

| ID | Severity | Description | Steps to Reproduce | Status |
|----|----------|-------------|-------------------|--------|
| | | | | |

---

## Test Evidence

**Screenshots**:
- [ ] Import dialog with SQL pasted
- [ ] Wizard Step 2 showing all parsed parameters
- [ ] Wizard Step 3 showing parsed validation + new validation
- [ ] Side-by-side comparison view with green-highlighted changes
- [ ] MySQL Workbench showing modified procedure execution

**Files**:
- [ ] Original `inv_inventory_Add_Item.sql`
- [ ] Modified `inv_inventory_Add_Item_modified.sql`

---

## Notes

_Record observations about parser accuracy, any parsing failures, edge cases_

---

## Tester Information

**Tester Name**: _________________________  
**Test Date**: _________________________  
**Browser/Version**: Chrome _______________

**Overall Test Result**: ☐ PASS ☐ FAIL ☐ BLOCKED

**Sign-off**: _________________________ Date: _____________
