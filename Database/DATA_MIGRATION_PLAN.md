# Data Migration Plan - Fix IN Transaction Column Swap Bug

**Date**: November 3, 2025
**Database**: mtm_wip_application_winforms (MAMP MySQL 5.7)
**Issue**: IN transactions have FromLocation/ToLocation columns swapped
**Impact**: 6,317 affected records (100% of all IN transactions)
**Risk Level**: HIGH - Affects transaction lifecycle visualization and reporting

---

## Verification Results

### 1. Database Schema Validation ✅

**Table**: `inv_transaction`

| Column       | Type         | Purpose                                |
| ------------ | ------------ | -------------------------------------- |
| FromLocation | varchar(100) | Source location (for OUT/TRANSFER)     |
| ToLocation   | varchar(100) | Destination location (for IN/TRANSFER) |

### 2. Bug Confirmation ✅

**Query Result**:

```sql
SELECT TransactionType, COUNT(*) AS Total,
       SUM(CASE WHEN FromLocation IS NOT NULL THEN 1 ELSE 0 END) AS HasFrom,
       SUM(CASE WHEN ToLocation IS NOT NULL THEN 1 ELSE 0 END) AS HasTo
FROM inv_transaction
GROUP BY TransactionType;

+-----------------+-------+---------+-------+
| TransactionType | Total | HasFrom | HasTo |
+-----------------+-------+---------+-------+
| IN              |  6317 |    6317 |     0 | ❌ WRONG
| OUT             | 11307 |   11307 |     0 | ✅ CORRECT
| TRANSFER        |   341 |     341 |   341 | ✅ CORRECT
+-----------------+-------+---------+-------+
```

**Critical Findings**:

-   ✅ **TRANSFER transactions are correct** (both FromLocation and ToLocation populated)
-   ✅ **OUT transactions are correct** (FromLocation populated, ToLocation NULL)
-   ❌ **ALL IN transactions are wrong** (FromLocation populated, ToLocation NULL)
-   ❌ **0 correctly formatted IN transactions** exist in the database

### 3. User's Batch Verification ✅

**Part**: 21-28841-006
**Batch**: 0000010374

```
+-------+-----------------+-------------+-------------------+------------+----------+---------------------+
| ID    | TransactionType | BatchNumber | FromLocation      | ToLocation | Quantity | ReceiveDate         |
+-------+-----------------+-------------+-------------------+------------+----------+---------------------+
| 26907 | OUT             | 0000010374  | FLOOR - WIP AISLE | NULL       | 28       | 2025-09-24 09:15:00 |
| 26608 | IN              | 0000010374  | FLOOR - WIP AISLE | NULL       | 28       | 2025-09-24 09:15:00 |
| 26897 | OUT             | 0000010374  | FLOOR - WIP AISLE | NULL       | 28       | 2025-09-25 11:56:39 |
+-------+-----------------+-------------+-------------------+------------+----------+---------------------+
```

**Analysis**:

-   Transaction 26608 (IN): Has `FromLocation='FLOOR - WIP AISLE'` but `ToLocation=NULL` ❌
-   This makes it **indistinguishable from an OUT transaction** by location columns alone
-   Only the `TransactionType` enum column correctly identifies it as IN

### 4. TransactionLifecycleForm Impact Analysis ✅

**File**: `Forms/Transactions/TransactionLifecycleForm.cs`

**Current Logic** (Lines 178-228):

```csharp
// Initialize location quantity tracking
if (!string.IsNullOrEmpty(firstTransaction.ToLocation))
{
    locationQuantities[firstTransaction.ToLocation] = firstTransaction.Quantity;
}
```

**❌ CRITICAL BUG**: This code expects IN transactions to have `ToLocation` populated. With the current bug:

-   `firstTransaction.ToLocation` is **NULL** for all IN transactions
-   Location tracking dictionary **never gets initialized**
-   Split detection logic **completely fails**
-   TreeView hierarchy **will be incorrect**

**Example Failure Scenario**:

```csharp
// Batch 0000010374:
// Transaction 26608 (IN): FromLocation='FLOOR', ToLocation=NULL

if (!string.IsNullOrEmpty(firstTransaction.ToLocation))  // FALSE - ToLocation is NULL!
{
    locationQuantities[firstTransaction.ToLocation] = 28; // NEVER EXECUTES
}

// Later transactions expect 'FLOOR' to be in the dictionary
// Result: KeyNotFoundException or incorrect split detection
```

**Lines 235-260 (Split Detection)**:

```csharp
if (!string.IsNullOrEmpty(transaction.FromLocation) &&
    locationQuantities.ContainsKey(transaction.FromLocation))
{
    var remainingQty = locationQuantities[transaction.FromLocation];
    // Will throw KeyNotFoundException because location was never added!
}
```

**Verdict**: TransactionLifecycleForm **WILL FAIL** with current database state. Migration is **MANDATORY** before feature can work.

---

## Migration Plan

### Phase 1: Pre-Migration Safety Checks

#### Step 1.1: Full Database Backup

Not Required, running on test server already

**Success Criteria**: Backup file created with size > 1MB

#### Step 1.2: Create Migration Audit Table

```sql
-- Create table to track migration results
CREATE TABLE IF NOT EXISTS migration_audit (
    id INT AUTO_INCREMENT PRIMARY KEY,
    migration_name VARCHAR(100) NOT NULL,
    execution_date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    records_affected INT NOT NULL,
    rollback_available BOOLEAN NOT NULL DEFAULT TRUE,
    status ENUM('IN_PROGRESS', 'COMPLETED', 'ROLLED_BACK', 'FAILED') NOT NULL,
    notes TEXT,
    INDEX idx_migration_name (migration_name),
    INDEX idx_execution_date (execution_date)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

#### Step 1.3: Create Rollback Safety Table

```sql
-- Backup affected records BEFORE migration
CREATE TABLE IF NOT EXISTS inv_transaction_rollback_20251103 AS
SELECT * FROM inv_transaction
WHERE TransactionType = 'IN'
  AND FromLocation IS NOT NULL
  AND ToLocation IS NULL;

-- Verify backup count matches affected count
SELECT COUNT(*) AS BackupCount FROM inv_transaction_rollback_20251103;
-- Expected: 6317
```

**Success Criteria**: Rollback table contains 6,317 records

#### Step 1.4: Validation Query - Current State

```sql
-- Document current state before migration
SELECT
    'BEFORE_MIGRATION' AS Stage,
    COUNT(*) AS TotalIN,
    SUM(CASE WHEN FromLocation IS NOT NULL AND ToLocation IS NULL THEN 1 ELSE 0 END) AS IncorrectIN,
    SUM(CASE WHEN FromLocation IS NULL AND ToLocation IS NOT NULL THEN 1 ELSE 0 END) AS CorrectIN
FROM inv_transaction
WHERE TransactionType = 'IN';

-- Expected Result:
-- Stage: BEFORE_MIGRATION
-- TotalIN: 6317
-- IncorrectIN: 6317
-- CorrectIN: 0
```

---

### Phase 2: Data Migration Execution

#### Step 2.1: Log Migration Start

```sql
INSERT INTO migration_audit (migration_name, records_affected, status, notes)
VALUES (
    'fix_in_transaction_column_swap',
    6317,
    'IN_PROGRESS',
    'Swapping FromLocation and ToLocation for all IN transactions'
);
```

#### Step 2.2: Execute Migration (DRY RUN FIRST)

```sql
-- DRY RUN: Verify what will change (DO NOT COMMIT)
START TRANSACTION;

-- Preview changes
SELECT
    ID,
    TransactionType,
    FromLocation AS 'Current_FromLocation (WRONG)',
    ToLocation AS 'Current_ToLocation (WRONG)',
    FromLocation AS 'Will_Become_ToLocation (CORRECT)',
    NULL AS 'Will_Become_FromLocation (CORRECT)',
    PartID,
    ReceiveDate
FROM inv_transaction
WHERE TransactionType = 'IN'
  AND FromLocation IS NOT NULL
  AND ToLocation IS NULL
LIMIT 10;

ROLLBACK; -- DO NOT COMMIT DRY RUN
```

**Review Output**: Ensure FromLocation values look like valid location codes (e.g., "FLOOR - WIP AISLE", "R-B1-33")

#### Step 2.3: Execute ACTUAL Migration

```sql
-- ACTUAL MIGRATION: Swap columns for IN transactions
START TRANSACTION;

-- Update inv_transaction table
UPDATE inv_transaction
SET
    ToLocation = FromLocation,    -- Move location to correct column
    FromLocation = NULL           -- Clear incorrect column
WHERE
    TransactionType = 'IN'
    AND FromLocation IS NOT NULL
    AND ToLocation IS NULL;

-- Verify affected row count
SELECT ROW_COUNT() AS UpdatedRows;
-- Expected: 6317

-- Validate migration results
SELECT
    'AFTER_MIGRATION' AS Stage,
    COUNT(*) AS TotalIN,
    SUM(CASE WHEN FromLocation IS NOT NULL AND ToLocation IS NULL THEN 1 ELSE 0 END) AS IncorrectIN,
    SUM(CASE WHEN FromLocation IS NULL AND ToLocation IS NOT NULL THEN 1 ELSE 0 END) AS CorrectIN
FROM inv_transaction
WHERE TransactionType = 'IN';

-- Expected Result:
-- Stage: AFTER_MIGRATION
-- TotalIN: 6317
-- IncorrectIN: 0      ✅
-- CorrectIN: 6317     ✅

-- If validation passes, COMMIT; otherwise ROLLBACK
COMMIT;
```

#### Step 2.4: Update Migration Audit

```sql
UPDATE migration_audit
SET
    status = 'COMPLETED',
    notes = CONCAT(notes, ' | Migration completed successfully at ', NOW())
WHERE
    migration_name = 'fix_in_transaction_column_swap'
    AND status = 'IN_PROGRESS';
```

---

### Phase 3: Post-Migration Validation

#### Step 3.1: Comprehensive Data Validation

```sql
-- Validate all transaction types have correct column usage
SELECT
    TransactionType,
    COUNT(*) AS Total,
    SUM(CASE WHEN FromLocation IS NULL AND ToLocation IS NOT NULL THEN 1 ELSE 0 END) AS CorrectFormat,
    SUM(CASE WHEN FromLocation IS NOT NULL AND ToLocation IS NULL THEN 1 ELSE 0 END) AS WrongFormat
FROM inv_transaction
WHERE TransactionType = 'IN'
GROUP BY TransactionType;

-- Expected:
-- TransactionType: IN
-- Total: 6317
-- CorrectFormat: 6317   ✅
-- WrongFormat: 0        ✅
```

#### Step 3.2: Verify User's Batch

```sql
-- Re-query the user's specific batch
SELECT
    ID,
    TransactionType,
    BatchNumber,
    FromLocation,
    ToLocation,
    PartID,
    Operation,
    Quantity,
    User,
    ReceiveDate
FROM inv_transaction
WHERE PartID = '21-28841-006'
  AND BatchNumber = '0000010374'
ORDER BY ReceiveDate;

-- Expected Result:
-- ID: 26608 | Type: IN  | From: NULL              | To: FLOOR - WIP AISLE   ✅
-- ID: 26907 | Type: OUT | From: FLOOR - WIP AISLE | To: NULL                ✅
-- ID: 26897 | Type: OUT | From: FLOOR - WIP AISLE | To: NULL                ✅
```

#### Step 3.3: Validate Recent Transactions

```sql
-- Check most recent IN transactions
SELECT
    ID,
    TransactionType,
    FromLocation,
    ToLocation,
    PartID,
    Quantity,
    ReceiveDate
FROM inv_transaction
WHERE TransactionType = 'IN'
ORDER BY ReceiveDate DESC
LIMIT 10;

-- Verify ALL have:
-- FromLocation: NULL     ✅
-- ToLocation: NOT NULL   ✅
```

#### Step 3.4: Cross-Check with Business Logic

```sql
-- Ensure no orphaned location references
SELECT DISTINCT ToLocation AS Locations
FROM inv_transaction
WHERE TransactionType = 'IN'
  AND ToLocation IS NOT NULL
ORDER BY ToLocation;

-- Review output: All locations should be valid location codes
-- Compare against md_locations table if available
```

---

### Phase 4: Application Testing

#### Step 4.1: Test TransactionLifecycleForm

**Manual Test Procedure**:

1. Open MTM WIP Application
2. Navigate to Transaction Viewer
3. Search for batch: `0000010374`
4. Click "View Lifecycle" button
5. Verify TreeView displays:
    ```
    ✅ IN - → FLOOR - WIP AISLE (Qty: 28)
        ✅ OUT - FLOOR - WIP AISLE → (Qty: 28)
        ✅ OUT - FLOOR - WIP AISLE → (Qty: 28)
    ```

**Success Criteria**:

-   ✅ TreeView loads without exceptions
-   ✅ First node shows IN transaction with destination location
-   ✅ Subsequent nodes show OUT transactions
-   ✅ No KeyNotFoundException errors
-   ✅ Location quantities track correctly

#### Step 4.2: Test Multiple Batch Lifecycles

**Test Cases**:

1. **Simple IN → OUT Lifecycle**

    - Find batch with 1 IN, 1 OUT
    - Verify linear progression

2. **IN → TRANSFER → OUT Lifecycle**

    - Find batch with transfers
    - Verify location changes tracked

3. **Split Batch Lifecycle**
    - Find batch with partial transfers
    - Verify child nodes created correctly

#### Step 4.3: Regression Testing

**Areas to Test**:

1. **Inventory Reports**

    - Verify location-based filters work
    - Check "Items Received" report

2. **Transaction Search**

    - Search by ToLocation for IN transactions
    - Should return results (previously returned empty)

3. **Audit Trails**
    - Verify transaction history displays correctly
    - Check that location flow makes sense (→ Destination for IN)

---

### Phase 5: Stored Procedure Fix (Future Transactions)

⚠️ **CRITICAL**: Migration fixes historical data, but stored procedure must be updated to prevent future bugs.

#### Step 5.1: Update inv_inventory_Add_Item

**File**: `Database/UpdatedStoredProcedures/ReadyForVerification/inventory/inv_inventory_Add_Item.sql`

**Current Code** (Lines 52-71):

```sql
INSERT INTO inv_transaction
    (
        TransactionType,
        BatchNumber,
        PartID,
        FromLocation,      -- ❌ WRONG
        ToLocation,        -- ❌ WRONG
        Operation,
        Quantity,
        Notes,
        User,
        ItemType
    )
VALUES
    (
        'IN',
        batchStr,
        p_PartID,
        p_Location,        -- ❌ Goes to FromLocation
        NULL,              -- ❌ Goes to ToLocation
        p_Operation,
        p_Quantity,
        p_Notes,
        p_User,
        p_ItemType
    );
```

**Fixed Code**:

```sql
INSERT INTO inv_transaction
    (
        TransactionType,
        BatchNumber,
        PartID,
        FromLocation,      -- ✅ NULL for IN (no source)
        ToLocation,        -- ✅ p_Location (destination)
        Operation,
        Quantity,
        Notes,
        User,
        ItemType
    )
VALUES
    (
        'IN',
        batchStr,
        p_PartID,
        NULL,              -- ✅ CORRECT: No source for IN
        p_Location,        -- ✅ CORRECT: Destination is p_Location
        p_Operation,
        p_Quantity,
        p_Notes,
        p_User,
        p_ItemType
    );
```

#### Step 5.2: Deploy Updated Stored Procedure

```sql
-- Deploy to database
SOURCE Database/UpdatedStoredProcedures/ReadyForVerification/inventory/inv_inventory_Add_Item.sql;

-- Verify procedure updated
SHOW CREATE PROCEDURE inv_inventory_Add_Item;
```

#### Step 5.3: Test New Inventory Entry

**Manual Test**:

1. Open Inventory Tab
2. Add new inventory:
    - Part: `TEST-MIGRATION-001`
    - Location: `FLOOR`
    - Operation: `100`
    - Quantity: `10`
3. Query database:
    ```sql
    SELECT ID, TransactionType, FromLocation, ToLocation, PartID, Quantity
    FROM inv_transaction
    WHERE PartID = 'TEST-MIGRATION-001'
    ORDER BY ReceiveDate DESC
    LIMIT 1;
    ```

**Expected Result**:

```
TransactionType: IN
FromLocation: NULL       ✅
ToLocation: FLOOR        ✅
```

---

## Rollback Plan (If Needed)

### Emergency Rollback Procedure

```sql
-- Step 1: Begin transaction
START TRANSACTION;

-- Step 2: Restore from rollback table
UPDATE inv_transaction t
INNER JOIN inv_transaction_rollback_20251103 r ON t.ID = r.ID
SET
    t.FromLocation = r.FromLocation,
    t.ToLocation = r.ToLocation
WHERE t.TransactionType = 'IN';

-- Step 3: Verify rollback
SELECT
    COUNT(*) AS RestoredCount,
    SUM(CASE WHEN FromLocation IS NOT NULL AND ToLocation IS NULL THEN 1 ELSE 0 END) AS BackToIncorrect
FROM inv_transaction
WHERE TransactionType = 'IN';

-- Expected: RestoredCount = 6317, BackToIncorrect = 6317

-- Step 4: Update audit log
UPDATE migration_audit
SET status = 'ROLLED_BACK', notes = CONCAT(notes, ' | Rolled back at ', NOW())
WHERE migration_name = 'fix_in_transaction_column_swap';

-- Step 5: Commit rollback
COMMIT;
```

---

## Execution Checklist

### Pre-Migration

-   [ ] **Backup database** (mysqldump)
-   [ ] **Create migration_audit table**
-   [ ] **Create rollback safety table** (inv_transaction_rollback_20251103)
-   [ ] **Verify rollback table has 6,317 records**
-   [ ] **Run validation query - current state**
-   [ ] **Review and approve migration plan with team**

### Migration Execution

-   [ ] **Log migration start** in migration_audit
-   [ ] **Run DRY RUN migration** (with ROLLBACK)
-   [ ] **Review DRY RUN output** for anomalies
-   [ ] **Execute ACTUAL migration** (with COMMIT)
-   [ ] **Verify ROW_COUNT() = 6317**
-   [ ] **Run post-migration validation query**
-   [ ] **Update migration_audit to COMPLETED**

### Post-Migration Validation

-   [ ] **Comprehensive data validation** (CorrectIN = 6317)
-   [ ] **Verify user's batch** (26608, 26907, 26897)
-   [ ] **Check recent transactions** (last 10 IN)
-   [ ] **Cross-check locations** with business logic

### Application Testing

-   [ ] **Test TransactionLifecycleForm** with batch 0000010374
-   [ ] **Test multiple batch lifecycles** (simple, transfer, split)
-   [ ] **Regression test inventory reports**
-   [ ] **Regression test transaction search**
-   [ ] **Regression test audit trails**

### Stored Procedure Fix

-   [ ] **Update inv_inventory_Add_Item.sql** (swap columns)
-   [ ] **Deploy updated stored procedure**
-   [ ] **Test new inventory entry** (verify ToLocation populated)
-   [ ] **Run integration tests** (Dao_Inventory_Tests.cs)

### Documentation & Monitoring

-   [ ] **Update RELEASE_NOTES.md** with migration details
-   [ ] **Document in DUPLICATE_TRANSACTION_ANALYSIS.md**
-   [ ] **Add monitoring query** to dashboard
-   [ ] **Schedule follow-up review** (1 week post-migration)

---

## Risk Assessment

### Risk Level: HIGH

**Affected Records**: 6,317 (100% of IN transactions)

**Mitigation Factors**:

-   ✅ Full database backup before migration
-   ✅ Rollback table created with original data
-   ✅ Transaction-based migration (atomic)
-   ✅ Comprehensive validation queries
-   ✅ DRY RUN capability
-   ✅ Simple single-table UPDATE (no JOINs)

### Potential Issues & Resolutions

| Issue                                    | Likelihood | Impact | Mitigation                                           |
| ---------------------------------------- | ---------- | ------ | ---------------------------------------------------- |
| Migration fails mid-execution            | LOW        | HIGH   | Use TRANSACTION + rollback table                     |
| Location codes invalid after swap        | LOW        | MEDIUM | Review DRY RUN output, validate against md_locations |
| TransactionLifecycleForm still fails     | MEDIUM     | HIGH   | Test thoroughly before production deployment         |
| New inventory entries still buggy        | MEDIUM     | HIGH   | Update stored procedure IMMEDIATELY after migration  |
| Performance degradation during migration | LOW        | LOW    | Run during off-hours, 6K records is manageable       |

---

## Timeline Estimate

| Phase                              | Duration     | Responsible     |
| ---------------------------------- | ------------ | --------------- |
| Phase 1: Pre-Migration Safety      | 15 minutes   | DBA / Developer |
| Phase 2: Migration Execution       | 10 minutes   | DBA             |
| Phase 3: Post-Migration Validation | 20 minutes   | DBA / QA        |
| Phase 4: Application Testing       | 30 minutes   | QA / Developer  |
| Phase 5: Stored Procedure Fix      | 20 minutes   | Developer       |
| **Total**                          | **~2 hours** | Team            |

**Recommended Execution Window**: Off-hours (after 6 PM or weekend)

---

## Clarification Questions for User

Before proceeding with migration, please confirm:

### Question 1: Migration Timing

**Q**: When would you like to execute this migration?

-   **Option A**: Immediately (during business hours with monitoring)
-   **Option B**: After hours (evening/weekend for safety)
-   **Option C**: Staged (test database first, then production)

**Recommendation**: Option C (staged) for maximum safety.

### Question 2: Stored Procedure Deployment

**Q**: Should the stored procedure fix be deployed:

-   **Option A**: Immediately after data migration (same session)
-   **Option B**: Separately after validating migration results (next day)
-   **Option C**: Hold until further testing

**Recommendation**: Option A to prevent new incorrect transactions.

### Question 3: Rollback Table Retention

**Q**: How long should we keep the rollback table `inv_transaction_rollback_20251103`?

-   **Option A**: 7 days (standard)
-   **Option B**: 30 days (extended safety)
-   **Option C**: Permanent (archive)

**Recommendation**: Option B (30 days).

### Question 4: Production vs Test Database

**Q**: Should we execute this plan on:

-   **Option A**: Test database first (`mtm_wip_application_winforms_test`)
-   **Option B**: Production database directly (`mtm_wip_application_winforms`)
-   **Option C**: Both (test first, then production if successful)

**Recommendation**: Option C (test then production).

### Question 5: User Communication

**Q**: Should we notify application users before migration?

-   **Option A**: Yes - schedule downtime window
-   **Option B**: No - migration is quick enough to run silently
-   **Option C**: Partial - notify key stakeholders only

**Recommendation**: Option C (notify stakeholders).

---

## Success Metrics

Migration is considered **successful** when:

1. ✅ All 6,317 IN transactions have `FromLocation = NULL` and `ToLocation = populated`
2. ✅ TransactionLifecycleForm loads batch 0000010374 without errors
3. ✅ TreeView displays correct hierarchy (IN → OUT → OUT)
4. ✅ No application exceptions in production logs (24 hours post-migration)
5. ✅ New inventory entries use correct column format
6. ✅ Integration tests pass (Dao_Inventory_Tests.cs)

---

**Document Author**: GitHub Copilot (AI Agent)
**Analysis Date**: November 3, 2025
**Database**: mtm_wip_application_winforms (MAMP MySQL 5.7)
**Status**: Ready for User Approval & Execution
