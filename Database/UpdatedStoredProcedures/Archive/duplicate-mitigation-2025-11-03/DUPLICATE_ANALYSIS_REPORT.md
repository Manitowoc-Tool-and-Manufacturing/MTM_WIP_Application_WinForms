# Transaction Table Duplicate Analysis and Fix

**Date**: November 3, 2025  
**Database**: mtm_wip_application_winforms  
**Table**: inv_transaction  
**Issue**: Exact duplicate transaction records causing quantity mismatches

## Root Cause Identified

The `inv_transaction` table contains **EXACT DUPLICATE RECORDS** where multiple rows have identical values for:
- BatchNumber, PartID, TransactionType
- FromLocation, ToLocation, Operation
- Quantity, User, ReceiveDate (same timestamp!)

This causes data integrity violations where OUT quantities exceed IN quantities for the same batch.

## Examples of Duplicates Found

| BatchNumber | PartID | Type | Duplicates | All IDs | Keep ID | Delete IDs |
|------------|--------|------|-----------|---------|---------|------------|
| 0000010441 | A0006876776 | OUT | 4 | 27686,27688,27690,27700 | 27686 | 27688,27690,27700 |
| 0000007768 | 78612A-ISE | IN | 2 | 18892,18893 | 18892 | 18893 |
| 0000007768 | 78612A-ISE | OUT | 2 | 21763,21764 | 21763 | 21764 |
| 0000007768 | 78612A-ISE | OUT | 2 | 21751,21752 | 21751 | 21752 |
| 0000008527 | A0003883721 | OUT | 2 | 21341,21342 | 21341 | 21342 |

## Impact Analysis

**Batch 0000010441 (A0006876776)**: 
- Has 4 IDENTICAL OUT transactions
- Results in 4x overcounting of consumed inventory
- Should be: 1 OUT of 64 units
- Actual: 4 OUT of 64 units each = 256 units total (4x error!)

**Batch 0000007768 (78612A-ISE)**:
- Has 2 duplicate IN + 4 duplicate OUT transactions
- Results in 2x IN (10,000 units) vs 4x OUT (20,000 units)
- Creates artificial inventory deficit

## Solution Strategy

**Approach**: Keep the **lowest ID** from each duplicate group (first inserted record)

**Rationale**:
1. Lowest ID represents the original transaction
2. Higher IDs are accidental duplicates (double-click, network retry, etc.)
3. Preserves transaction history timeline
4. Minimal data loss (keeping genuine first entry)

## Safety Measures

✅ **Backup Table**: `inv_transaction_duplicates_backup_20251103`  
✅ **Backup Before Delete**: All duplicates backed up with deletion reason  
✅ **Verification Queries**: Confirm remaining duplicates after fix  
✅ **Restore Procedure**: Documented SQL to restore if needed  

## Fix Execution Steps

### Step 1: Count Duplicates
```sql
SELECT 
    COUNT(*) AS Total_Duplicate_Records,
    COUNT(DISTINCT CONCAT(BatchNumber, PartID, TransactionType, 
                          FromLocation, ToLocation, Operation, Quantity, User, ReceiveDate)) AS Unique_Groups
FROM inv_transaction
GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation, Operation, Quantity, User, ReceiveDate
HAVING COUNT(*) > 1;
```

### Step 2: Create Backup Table
```sql
CREATE TABLE inv_transaction_duplicates_backup_20251103 (
    ID INT PRIMARY KEY,
    TransactionType VARCHAR(50),
    BatchNumber VARCHAR(50),
    PartID VARCHAR(100),
    FromLocation VARCHAR(100),
    ToLocation VARCHAR(100),
    Operation VARCHAR(100),
    Quantity INT,
    Notes VARCHAR(1000),
    User VARCHAR(100),
    ItemType VARCHAR(200),
    ReceiveDate DATETIME,
    backup_date DATETIME DEFAULT CURRENT_TIMESTAMP,
    deletion_reason VARCHAR(255),
    INDEX idx_batch_part (BatchNumber, PartID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

### Step 3: Backup Duplicates (Before Deletion)
```sql
INSERT INTO inv_transaction_duplicates_backup_20251103
    (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation,
     Quantity, Notes, User, ItemType, ReceiveDate, deletion_reason)
SELECT 
    t.ID, t.TransactionType, t.BatchNumber, t.PartID, t.FromLocation, t.ToLocation,
    t.Operation, t.Quantity, t.Notes, t.User, t.ItemType, t.ReceiveDate,
    CONCAT('Duplicate - Kept ID ', minid.MinID)
FROM inv_transaction t
INNER JOIN (
    SELECT BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
           Operation, Quantity, User, ReceiveDate, MIN(ID) AS MinID
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
) minid ON t.BatchNumber = minid.BatchNumber
       AND t.PartID = minid.PartID
       AND t.TransactionType = minid.TransactionType
       AND COALESCE(t.FromLocation, '') = COALESCE(minid.FromLocation, '')
       AND COALESCE(t.ToLocation, '') = COALESCE(minid.ToLocation, '')
       AND t.Operation = minid.Operation
       AND t.Quantity = minid.Quantity
       AND t.User = minid.User
       AND t.ReceiveDate = minid.ReceiveDate
       AND t.ID > minid.MinID;
```

### Step 4: Delete Duplicates (Keep Lowest ID)
```sql
DELETE t FROM inv_transaction t
INNER JOIN (
    SELECT BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
           Operation, Quantity, User, ReceiveDate, MIN(ID) AS MinID
    FROM inv_transaction
    GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
             Operation, Quantity, User, ReceiveDate
    HAVING COUNT(*) > 1
) minid ON t.BatchNumber = minid.BatchNumber
       AND t.PartID = minid.PartID
       AND t.TransactionType = minid.TransactionType
       AND COALESCE(t.FromLocation, '') = COALESCE(minid.FromLocation, '')
       AND COALESCE(t.ToLocation, '') = COALESCE(minid.ToLocation, '')
       AND t.Operation = minid.Operation
       AND t.Quantity = minid.Quantity
       AND t.User = minid.User
       AND t.ReceiveDate = minid.ReceiveDate
       AND t.ID > minid.MinID;
```

### Step 5: Verify Fix
```sql
-- Should return 0 rows
SELECT 
    BatchNumber, PartID, TransactionType, COUNT(*) AS Remaining_Duplicates
FROM inv_transaction
GROUP BY BatchNumber, PartID, TransactionType, FromLocation, ToLocation,
         Operation, Quantity, User, ReceiveDate
HAVING COUNT(*) > 1;
```

### Step 6: Verify Quantity Fixes
```sql
-- Batches that were previously showing OUT > IN should now be correct
SELECT 
    t.BatchNumber,
    t.PartID,
    SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END) AS Total_IN,
    SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END) AS Total_OUT,
    (SUM(CASE WHEN t.TransactionType = 'IN' THEN t.Quantity ELSE 0 END) -
     SUM(CASE WHEN t.TransactionType = 'OUT' THEN t.Quantity ELSE 0 END)) AS Net_Inventory
FROM inv_transaction t
WHERE t.BatchNumber IN ('0000010441', '0000007768', '0000008527')
GROUP BY t.BatchNumber, t.PartID
ORDER BY t.BatchNumber;
```

## Restore Procedure (If Needed)

```sql
-- Restore deleted duplicates if fix needs to be reverted
INSERT INTO inv_transaction 
    (ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, 
     Operation, Quantity, Notes, User, ItemType, ReceiveDate)
SELECT 
    ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation,
    Operation, Quantity, Notes, User, ItemType, ReceiveDate
FROM inv_transaction_duplicates_backup_20251103
WHERE ID NOT IN (SELECT ID FROM inv_transaction);
```

## Prevention Measures

**To prevent future duplicates**:

1. **Application Code Review**: Check for double-click handlers, retry logic
2. **Database Constraints**: Consider adding unique index (may break existing workflow)
3. **Transaction Isolation**: Review stored procedure transaction handling
4. **UI Debouncing**: Add button disabling during transaction processing
5. **Duplicate Detection**: Add validation before INSERT in stored procedures

## Files Created

1. `Database/Scripts/find_batches_with_multiple_in_out.sql` - Original analysis script
2. `Database/Scripts/analyze_and_fix_duplicates.sql` - Comprehensive fix script
3. `Database/UpdatedStoredProcedures/ReadyForVerification/inventory/inv_transaction_Fix_Duplicates_Analysis.sql` - Stored procedure version

## Next Steps

1. ✅ **Review this analysis** with stakeholders
2. ⚠️ **Schedule maintenance window** for production fix
3. ⚠️ **Execute backup** (Step 3)
4. ⚠️ **Execute deletion** (Step 4)
5. ✅ **Verify results** (Steps 5-6)
6. ✅ **Monitor application** after fix
7. ✅ **Implement prevention measures**

---

**Status**: Analysis Complete - Ready for Review  
**Action Required**: Stakeholder approval before executing deletion
