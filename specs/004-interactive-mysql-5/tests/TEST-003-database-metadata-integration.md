# Manual Test: Database Metadata Integration

**Test ID**: TEST-003  
**User Story**: US5 - Database Metadata Integration (Priority P1)  
**Feature**: Interactive MySQL 5.7 Stored Procedure Builder  
**Test Type**: Integration Test  
**Estimated Duration**: 15 minutes

---

## Test Objective

Verify that the builder connects to the live MySQL database, fetches real-time table and column metadata, populates dropdowns accurately, shows smart defaults based on column naming patterns, and handles connection failures gracefully.

---

## Prerequisites

- [ ] MAMP running with MySQL 5.7 on localhost:3306
- [ ] Database `mtm_wip_application_winforms_test` accessible
- [ ] Tables exist: Inventory, Parts, Locations (minimum)
- [ ] Chrome browser 86+
- [ ] PHP backend API endpoints functional

---

## Test Data

**Expected Tables** (minimum):
- Inventory
- Parts  
- Locations
- Inventory_Transactions
- Users

**Expected Columns in Inventory**:
- InventoryID (INT, AUTO_INCREMENT, PRIMARY KEY)
- PartNumber (VARCHAR(50))
- Quantity (DECIMAL(10,2))
- LocationCode (VARCHAR(20))
- ReceivedDate (DATETIME)
- ReceivedUser (INT)
- LastUpdated (DATETIME)
- LastUpdatedUser (INT)
- IsActive (TINYINT/BOOLEAN)

---

## Test Steps

### Step 1: Test Database Connection on Load

1. Open builder application index.html
2. Observe connection status indicator

**Expected Result**:
- [ ] Connection status shows "Connecting..." briefly
- [ ] Status changes to "Connected to mtm_wip_application_winforms_test"
- [ ] Green indicator or checkmark displayed
- [ ] No console errors related to database connection

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 2: Test Connection Failure Handling

1. Stop MAMP MySQL service
2. Refresh builder page or click "Refresh Metadata" button
3. Observe error handling

**Expected Result**:
- [ ] Connection status shows "Connection Failed"
- [ ] User-friendly error dialog appears: "Unable to connect to database. Please check that MAMP is running."
- [ ] "Retry Connection" button provided
- [ ] Technical details available in expandable section
- [ ] Application remains usable (doesn't crash)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 3: Reconnect After Failure

1. Restart MAMP MySQL service
2. Click "Retry Connection" button in error dialog

**Expected Result**:
- [ ] Connection retries successfully
- [ ] Status changes to "Connected"
- [ ] Metadata fetches automatically
- [ ] Error dialog closes

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 4: Verify Table Dropdown Population

1. Start wizard and proceed to Step 4 "DML Operations"
2. Click "Add Operation" → Select "INSERT"
3. View table dropdown

**Expected Result**:
- [ ] Dropdown shows all tables from mtm_wip_application_winforms_test
- [ ] Tables sorted alphabetically
- [ ] Table count displayed (e.g., "25 tables found")
- [ ] Common tables visible: Inventory, Parts, Locations
- [ ] No system tables (information_schema, mysql, performance_schema)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 5: Verify Column Metadata for Inventory Table

1. Select table: "Inventory" from dropdown
2. Observe column checklist population

**Expected Result**:
- [ ] All Inventory columns displayed with types in parentheses:
  - InventoryID (INT)
  - PartNumber (VARCHAR(50))
  - Quantity (DECIMAL(10,2))
  - LocationCode (VARCHAR(20))
  - ReceivedDate (DATETIME)
  - ReceivedUser (INT)
  - LastUpdated (DATETIME)
  - LastUpdatedUser (INT)
  - IsActive (TINYINT)
- [ ] InventoryID checkbox grayed out with tooltip: "Auto-increment column cannot be manually set"
- [ ] Other columns have enabled checkboxes
- [ ] Column order matches database ordinal position

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 6: Test Smart Defaults for Common Column Patterns

1. In INSERT operation builder, select table: "Inventory"
2. Check columns: ReceivedDate, ReceivedUser, IsActive
3. Observe value field suggestions

**Expected Result**:
- [ ] ReceivedDate value auto-fills with: `NOW()`
- [ ] ReceivedUser value suggests: `p_UserID` (parameter name)
- [ ] IsActive value auto-fills with: `1`
- [ ] LastUpdated (if selected) suggests: `NOW()`
- [ ] LastUpdatedUser (if selected) suggests: `p_UserID`
- [ ] All suggestions are editable (can override)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 7: Test Foreign Key Detection

1. Select table: "Inventory"
2. Check if foreign key columns are marked
3. Look for LocationCode column

**Expected Result**:
- [ ] LocationCode shows foreign key badge/icon
- [ ] Tooltip or indicator shows: "References Locations.LocationCode"
- [ ] Clicking FK badge offers to add FK validation rule automatically

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 8: Test Primary Key Detection

1. View column list for Inventory table

**Expected Result**:
- [ ] InventoryID marked as primary key (key icon or badge)
- [ ] Tooltip shows: "Primary Key - Auto Increment"
- [ ] Column grayed out for INSERT operations
- [ ] Column available for UPDATE WHERE clauses

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 9: Test Metadata Caching

1. Select table: "Parts"
2. Note load time
3. Select table: "Inventory"
4. Select table: "Parts" again
5. Note load time (should be instant from cache)

**Expected Result**:
- [ ] First load: <2 seconds (fetches from API)
- [ ] Second load: <100ms (loads from cache)
- [ ] No duplicate API calls in browser Network tab
- [ ] Cached metadata used for 10 minutes (per spec)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 10: Test Metadata Staleness Detection

1. Wait 10+ minutes after initial metadata fetch (or manually set cache timestamp)
2. View any table dropdown

**Expected Result**:
- [ ] Warning banner appears: "Schema data may be outdated (fetched 11 minutes ago). Refresh?"
- [ ] "Refresh Metadata" button prominent
- [ ] Yellow/orange indicator for staleness
- [ ] Metadata still usable (not blocked)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 11: Test Manual Metadata Refresh

1. Click "Refresh Metadata" button in toolbar
2. Observe refresh process

**Expected Result**:
- [ ] Loading spinner appears
- [ ] Status shows: "Refreshing metadata..."
- [ ] Table/column data refetches from API
- [ ] Timestamp updates to current time
- [ ] Staleness warning disappears
- [ ] Refresh completes in <5 seconds (per spec SC-014)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 12: Test Schema Change Detection

1. Add new column to Inventory table in MySQL:
   ```sql
   ALTER TABLE Inventory ADD COLUMN TestColumn VARCHAR(100);
   ```
2. Click "Refresh Metadata" in builder
3. Select Inventory table in INSERT builder

**Expected Result**:
- [ ] TestColumn appears in column list
- [ ] Column type shown correctly: VARCHAR(100)
- [ ] Column is selectable and functional

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 13: Test Non-Existent Table Handling

1. Manually edit URL or use browser console to force table selection: "NonExistentTable"
2. Observe error handling

**Expected Result**:
- [ ] Error message: "Table 'NonExistentTable' does not exist"
- [ ] Column list remains empty
- [ ] Validation prevents proceeding with invalid table
- [ ] Suggestion to refresh metadata if table was recently added

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 14: Test Large Schema Performance

**Note**: This test requires database with 50+ tables, or simulated delay

1. Connect to database with many tables (or simulate with delay in API)
2. Open table dropdown

**Expected Result**:
- [ ] Dropdown handles 50+ tables without UI freezing
- [ ] Type-ahead search/filter available
- [ ] Virtual scrolling for large lists (if implemented)
- [ ] Loading completes in <5 seconds (per spec SC-010)

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 15: Test Column Type Mapping

1. Create test table with all supported types:
   ```sql
   CREATE TABLE TypeTest (
       col_int INT,
       col_varchar VARCHAR(255),
       col_decimal DECIMAL(10,2),
       col_datetime DATETIME,
       col_date DATE,
       col_text TEXT,
       col_bool BOOLEAN,
       col_enum ENUM('A','B','C')
   );
   ```
2. Select TypeTest table in builder
3. View column types

**Expected Result**:
- [ ] All types displayed correctly
- [ ] VARCHAR shows length: VARCHAR(255)
- [ ] DECIMAL shows precision/scale: DECIMAL(10,2)
- [ ] BOOLEAN mapped to TINYINT (MySQL native)
- [ ] ENUM shows possible values in tooltip

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 16: Test Metadata in Validation Rules

1. Add "Foreign Key Check" validation rule
2. Select parameter to validate
3. View "Reference Table" dropdown

**Expected Result**:
- [ ] Reference Table dropdown shows all tables
- [ ] After selecting reference table, Reference Column dropdown populates
- [ ] Reference Column shows columns from selected reference table
- [ ] Only valid columns for FK (matching parameter type) highlighted/suggested

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

### Step 17: Test API Error Response Handling

1. Simulate API error by temporarily breaking PHP endpoint (rename file)
2. Refresh metadata

**Expected Result**:
- [ ] Error dialog shows user-friendly message
- [ ] Technical details available: "HTTP 404: File not found"
- [ ] Retry option provided
- [ ] Cached metadata still available if previously loaded

**Actual Result**: ___________________________

**Status**: ☐ PASS ☐ FAIL

---

## Success Criteria

**Test passes if**:
- Database connection succeeds and shows status
- All tables and columns from test database populate correctly
- Smart defaults suggest appropriate values based on column patterns
- Metadata caching reduces repeated API calls
- Staleness detection warns when metadata is >10 minutes old
- Schema changes detected after manual refresh
- Large schemas (50+ tables) handled without performance degradation

---

## Acceptance Scenario Mapping

This test validates **US5 Acceptance Scenarios**:

1. ✅ **Scenario 1**: Builder connected to mtm_wip_application_winforms_test → Developer selects Inventory table in INSERT builder → Column checkboxes show all columns with types (InventoryID grayed out, others enabled). Smart suggestions appear (ReceivedDate = NOW(), ReceivedUser = p_UserID, IsActive = 1)

2. ✅ **Scenario 2**: Developer checks columns for INSERT → Builder shows value mapping → Smart suggestions appear with option to override

3. ✅ **Scenario 3**: Database connection fails → Developer tries to load metadata → System displays clear error with troubleshooting steps and offers to retry connection

---

## Defects Found

| ID | Severity | Description | Steps to Reproduce | Status |
|----|----------|-------------|-------------------|--------|
| | | | | |

---

## Test Evidence

**Screenshots**:
- [ ] Connection status indicator (connected)
- [ ] Connection error dialog with retry button
- [ ] Table dropdown populated with database tables
- [ ] Column checklist for Inventory with types
- [ ] Smart defaults in INSERT value fields
- [ ] Metadata staleness warning
- [ ] Refresh metadata in progress

**API Response Samples**:
- [ ] GET /api/get-tables.php response JSON
- [ ] GET /api/get-columns.php?table=Inventory response JSON

---

## Notes

_Record metadata accuracy, performance observations, edge cases_

---

## Tester Information

**Tester Name**: _________________________  
**Test Date**: _________________________  
**Database**: mtm_wip_application_winforms_test  
**Table Count**: _____ tables  
**MySQL Version**: 5.7._____

**Overall Test Result**: ☐ PASS ☐ FAIL ☐ BLOCKED

**Sign-off**: _________________________ Date: _____________
