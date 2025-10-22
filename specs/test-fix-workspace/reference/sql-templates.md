# SQL Test Templates

**Purpose**: Copy-paste ready SQL templates for manual stored procedure testing  
**Last Updated**: 2025-10-19  
**Database**: mtm_wip_application_winforms_test

---

## Quick Navigation

- [Stored Procedure Testing](#stored-procedure-testing-template)
- [Test User Creation](#test-user-creation)
- [Quick Button Test Data](#quick-button-test-data)
- [Test Data Cleanup](#test-data-cleanup)
- [Manual Testing Workflow](#manual-testing-workflow)

---

## Stored Procedure Testing Template

**Purpose**: Test any stored procedure with output parameters before writing C# tests.

```sql
-- ==============================================================================
-- Test Stored Procedure: [PROCEDURE_NAME]
-- Purpose: [What you're testing]
-- Expected: [Expected outcome]
-- ==============================================================================

USE mtm_wip_application_winforms_test;

-- 1. Set up output parameter variables
SET @p_Status = 0;
SET @p_ErrorMsg = '';

-- 2. Call procedure with test parameters
CALL [PROCEDURE_NAME](
    'TestValue1',          -- p_Parameter1 (string)
    123,                   -- p_Parameter2 (int)
    '2025-01-01',          -- p_Parameter3 (date)
    @p_Status,             -- OUT p_Status
    @p_ErrorMsg            -- OUT p_ErrorMsg
);

-- 3. Check output parameters
SELECT 
    @p_Status AS Status,
    @p_ErrorMsg AS ErrorMessage,
    CASE 
        WHEN @p_Status = 0 THEN '✓ SUCCESS'
        WHEN @p_Status = 1 THEN '✓ SUCCESS (No Data)'
        ELSE '✗ ERROR'
    END AS Result;

-- 4. Verify data results (if procedure returns data)
-- Results appear above the SELECT statement

-- 5. Clean up test data (if needed)
-- DELETE FROM [table] WHERE [condition];
```

**Status Codes**:
- `0` = Success
- `1` = Success (no data)
- `-1` = General error
- `-2` = Validation error
- `-3` = Database error
- `-4` = Permission error
- `-5` = Data integrity error

---

## Test User Creation

**Purpose**: Create test users for integration tests (Categories 1 & 2).

```sql
-- ==============================================================================
-- Create Test Users
-- Required for: Quick Button tests, System DAO tests, Transaction tests
-- ==============================================================================

USE mtm_wip_application_winforms_test;

-- Create test users with various profiles
INSERT INTO usr_users (
    UserID,
    UserName,
    PasswordHash,
    IsActive,
    IsAdmin,
    CreatedDate,
    LastLoginDate
)
VALUES
    -- Regular active user (for most tests)
    ('TEST-USER', 'Test User', SHA2('TestPass123', 256), 1, 0, NOW(), NULL),
    
    -- Admin user (for permission tests)
    ('TEST-ADMIN', 'Test Admin', SHA2('AdminPass123', 256), 1, 1, NOW(), NOW()),
    
    -- Inactive user (for active/inactive filtering tests)
    ('TEST-INACTIVE', 'Test Inactive User', SHA2('password', 256), 0, 0, NOW(), NULL),
    
    -- Second regular user (for multi-user tests)
    ('TEST-USER-2', 'Test User 2', SHA2('password', 256), 1, 0, NOW(), NULL)
ON DUPLICATE KEY UPDATE 
    UserID = UserID;  -- Prevent errors if users already exist

-- Verify users created
SELECT 
    UserID,
    UserName,
    IsActive,
    IsAdmin,
    CreatedDate
FROM usr_users 
WHERE UserID LIKE 'TEST-%'
ORDER BY UserID;

-- Expected: 4 rows (TEST-USER, TEST-ADMIN, TEST-INACTIVE, TEST-USER-2)
```

**Known Credentials** (for credential validation tests):
- `TEST-USER`: password = `TestPass123`
- `TEST-ADMIN`: password = `AdminPass123`
- `TEST-INACTIVE`: password = `password`
- `TEST-USER-2`: password = `password`

---

## Quick Button Test Data

**Purpose**: Create test quick buttons for integration tests (Category 1).

```sql
-- ==============================================================================
-- Create Test Quick Buttons
-- Prerequisites: Test users must exist (run Test User Creation first)
-- ==============================================================================

USE mtm_wip_application_winforms_test;

-- Ensure test users exist
SELECT COUNT(*) AS TestUserCount 
FROM usr_users 
WHERE UserID LIKE 'TEST-%';
-- Expected: At least 2 users

-- Create test quick buttons
INSERT INTO sys_quick_buttons (
    ButtonID,           -- NULL for AUTO_INCREMENT
    UserID,
    ButtonText,
    Operation,
    Location,
    PartNumber,
    Position,
    IsActive
)
VALUES
    -- TEST-USER buttons
    (NULL, 'TEST-USER', 'Test Button 1', '100', 'FLOOR', 'PART-001', 1, 1),
    (NULL, 'TEST-USER', 'Test Button 2', '100', 'FLOOR', 'PART-002', 2, 1),
    (NULL, 'TEST-USER', 'Test Button 3', '110', 'RECEIVING', 'PART-003', 3, 1),
    
    -- TEST-USER-2 buttons
    (NULL, 'TEST-USER-2', 'Test Button A', '110', 'SHIPPING', 'PART-100', 1, 1),
    (NULL, 'TEST-USER-2', 'Test Button B', '100', 'FLOOR', 'PART-101', 2, 1);

-- Verify buttons created
SELECT 
    ButtonID,
    UserID,
    ButtonText,
    Operation,
    Location,
    Position,
    IsActive
FROM sys_quick_buttons 
WHERE UserID LIKE 'TEST-%'
ORDER BY UserID, Position;

-- Expected: 5 rows (3 for TEST-USER, 2 for TEST-USER-2)
```

---

## Test Data Cleanup

**Purpose**: Remove all test data after test runs to maintain database cleanliness.

```sql
-- ==============================================================================
-- Clean Up Test Data
-- CAUTION: Deletes all TEST-* data. Run ONLY in test database.
-- ==============================================================================

USE mtm_wip_application_winforms_test;

-- SAFETY CHECK: Verify we're in test database
SELECT DATABASE() AS CurrentDatabase;
-- Expected: mtm_wip_application_winforms_test
-- If NOT test database, STOP HERE!

-- Count records before deletion
SELECT 
    (SELECT COUNT(*) FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%') AS QuickButtons,
    (SELECT COUNT(*) FROM usr_users WHERE UserID LIKE 'TEST-%') AS Users;

-- Delete in correct order (children first, then parents)
-- Step 1: Delete quick buttons (child records)
DELETE FROM sys_quick_buttons 
WHERE UserID LIKE 'TEST-%';

-- Step 2: Delete transaction history (if exists)
DELETE FROM sys_transaction_history 
WHERE UserID LIKE 'TEST-%';

-- Step 3: Delete inventory records (if exists)
DELETE FROM inv_current_inventory 
WHERE LastModifiedBy LIKE 'TEST-%';

-- Step 4: Delete users (parent records)
DELETE FROM usr_users 
WHERE UserID LIKE 'TEST-%';

-- Verify cleanup
SELECT 
    (SELECT COUNT(*) FROM sys_quick_buttons WHERE UserID LIKE 'TEST-%') AS QuickButtons,
    (SELECT COUNT(*) FROM usr_users WHERE UserID LIKE 'TEST-%') AS Users;
-- Expected: 0, 0
```

**⚠️ WARNING**: Only run cleanup in test database! Verify database name before executing.

---

## Manual Testing Workflow

**Purpose**: Step-by-step workflow for manually testing stored procedures before writing C# tests.

### Phase 1: Setup Test Environment

```sql
-- 1. Connect to test database
USE mtm_wip_application_winforms_test;

-- 2. Verify database
SELECT DATABASE() AS CurrentDB, VERSION() AS MySQLVersion;

-- 3. Create test users (if not exists)
-- [Run Test User Creation section]

-- 4. Create test data specific to procedure being tested
-- [Run relevant test data section]
```

### Phase 2: Test Stored Procedure

```sql
-- 1. Test SUCCESS scenario
SET @p_Status = 0;
SET @p_ErrorMsg = '';

CALL sp_YourProcedure('ValidParam', @p_Status, @p_ErrorMsg);

SELECT @p_Status AS Status, @p_ErrorMsg AS Message;
-- Expected: Status=0, Message='' or 'Success'

-- 2. Test VALIDATION ERROR scenario
SET @p_Status = 0;
SET @p_ErrorMsg = '';

CALL sp_YourProcedure('', @p_Status, @p_ErrorMsg);  -- Empty param

SELECT @p_Status AS Status, @p_ErrorMsg AS Message;
-- Expected: Status=-2, Message='Validation error message'

-- 3. Test NOT FOUND scenario
SET @p_Status = 0;
SET @p_ErrorMsg = '';

CALL sp_YourProcedure('NONEXISTENT', @p_Status, @p_ErrorMsg);

SELECT @p_Status AS Status, @p_ErrorMsg AS Message;
-- Expected: Status=1 (success no data) OR Status=-1 (error)
```

### Phase 3: Verify Results

```sql
-- Check that procedure modified data correctly
SELECT * FROM [affected_table] WHERE [condition];

-- Compare before/after counts
SELECT COUNT(*) AS RecordCount FROM [table];
```

### Phase 4: Cleanup (Optional)

```sql
-- Remove test data created during testing
DELETE FROM [table] WHERE [test_data_condition];

-- Or run full cleanup
-- [Run Test Data Cleanup section]
```

---

## Quick Button Stored Procedure Examples

### Test sp_QuickButtons_Add

```sql
USE mtm_wip_application_winforms_test;

-- Setup
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES ('TEST-QB', 'Test QB User', SHA2('password', 256), 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID;

-- Test Add
SET @p_Status = 0;
SET @p_ErrorMsg = '';
SET @p_NewButtonID = 0;

CALL sp_QuickButtons_Add(
    'TEST-QB',              -- p_UserID
    'My Test Button',       -- p_ButtonText
    '100',                  -- p_Operation
    'FLOOR',                -- p_Location
    'PART-TEST',            -- p_PartNumber
    1,                      -- p_Position
    @p_NewButtonID,         -- OUT p_NewButtonID
    @p_Status,              -- OUT p_Status
    @p_ErrorMsg             -- OUT p_ErrorMsg
);

-- Check results
SELECT @p_Status AS Status, @p_ErrorMsg AS Message, @p_NewButtonID AS NewButtonID;
SELECT * FROM sys_quick_buttons WHERE ButtonID = @p_NewButtonID;

-- Cleanup
DELETE FROM sys_quick_buttons WHERE UserID = 'TEST-QB';
DELETE FROM usr_users WHERE UserID = 'TEST-QB';
```

### Test sp_QuickButtons_GetByUser

```sql
USE mtm_wip_application_winforms_test;

-- Setup (create test user and buttons first)
-- [Run Test User Creation and Quick Button Test Data sections]

-- Test Get By User
SET @p_Status = 0;
SET @p_ErrorMsg = '';

CALL sp_QuickButtons_GetByUser(
    'TEST-USER',            -- p_UserID
    @p_Status,              -- OUT p_Status
    @p_ErrorMsg             -- OUT p_ErrorMsg
);

-- Check results
SELECT @p_Status AS Status, @p_ErrorMsg AS Message;
-- Procedure should return DataTable with 3 buttons for TEST-USER
```

---

## Transaction History Examples

### Check Existing Transactions

```sql
USE mtm_wip_application_winforms_test;

-- Check transaction count by date range
SELECT 
    DATE(TransactionDate) AS TransactionDay,
    COUNT(*) AS TransactionCount
FROM sys_transaction_history
WHERE TransactionDate BETWEEN '2025-01-01' AND '2025-12-31'
GROUP BY DATE(TransactionDate)
ORDER BY TransactionDay DESC;

-- Check transactions for test users
SELECT * 
FROM sys_transaction_history
WHERE UserID LIKE 'TEST-%'
ORDER BY TransactionDate DESC
LIMIT 10;
```

---

## Troubleshooting

### Issue: "Foreign Key Constraint Fails"

**Problem**: Cannot insert quick button because UserID doesn't exist.

**Solution**:
```sql
-- Check if user exists
SELECT * FROM usr_users WHERE UserID = 'TEST-USER';

-- If not, create user first
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID;
```

### Issue: "Duplicate Entry Error"

**Problem**: Test data already exists from previous test run.

**Solution**:
```sql
-- Use ON DUPLICATE KEY UPDATE to make inserts idempotent
INSERT INTO usr_users (UserID, UserName, PasswordHash, IsActive, CreatedDate)
VALUES ('TEST-USER', 'Test User', SHA2('password', 256), 1, NOW())
ON DUPLICATE KEY UPDATE UserID = UserID;  -- No-op if exists

-- Or delete existing data first
DELETE FROM sys_quick_buttons WHERE UserID = 'TEST-USER';
DELETE FROM usr_users WHERE UserID = 'TEST-USER';
```

### Issue: "Cannot Delete User - Referenced by Quick Buttons"

**Problem**: Foreign key constraint prevents user deletion.

**Solution**:
```sql
-- Delete in correct order: children first, then parents
DELETE FROM sys_quick_buttons WHERE UserID = 'TEST-USER';  -- Child first
DELETE FROM usr_users WHERE UserID = 'TEST-USER';          -- Parent second
```

---

**Template Count**: 10+ ready-to-use SQL templates  
**Last Updated**: 2025-10-19  
**Maintained by**: Development Team
