# Transaction Duplicate Maintenance Procedures

## Overview

Two stored procedures for maintaining transaction data integrity by finding and removing duplicate records.

**Created**: 2025-11-03  
**Location**: `Database/UpdatedStoredProcedures/ReadyForVerification/maintenance/`

## Procedures

### 1. maint_transactions_FindDuplicates

**Purpose**: Find duplicate transaction records based on specified criteria.

**Signature**:
```sql
CALL maint_transactions_FindDuplicates(
    IN p_DuplicateType VARCHAR(50),  -- 'EXACT', 'SAME_TIMESTAMP', 'ALL'
    OUT p_Status INT,                -- 1=Success, -1=Error
    OUT p_ErrorMsg VARCHAR(500)      -- Success or error message
);
```

**Parameters**:
- `p_DuplicateType`: Type of duplicates to find
  - `'EXACT'` - Exact duplicates (all fields including timestamp match)
  - `'SAME_TIMESTAMP'` - OUT transactions with same timestamp as their IN transaction
  - `'ALL'` - All types of duplicates
- `p_Status`: Return status (1=Success, -1=Error, -2=Invalid input)
- `p_ErrorMsg`: Return message

**Returns**: Result set with duplicate transaction details including:
- `DuplicateType` - Type of duplicate found
- `ID` - Transaction ID
- `BatchNumber`, `PartID`, `TransactionType`, etc. - Transaction details
- `MinID_ToKeep` or `Reference_ID` - Related record information
- `Action` - Recommended action (DELETE)

**Usage Examples**:
```sql
-- Find exact duplicates
CALL maint_transactions_FindDuplicates('EXACT', @status, @msg);
SELECT @status, @msg;

-- Find same-timestamp OUT duplicates
CALL maint_transactions_FindDuplicates('SAME_TIMESTAMP', @status, @msg);
SELECT @status, @msg;

-- Find all types of duplicates
CALL maint_transactions_FindDuplicates('ALL', @status, @msg);
SELECT @status, @msg;
```

---

### 2. maint_transactions_RemoveDuplicates

**Purpose**: Remove duplicate transaction records based on specified criteria with optional backup.

**Signature**:
```sql
CALL maint_transactions_RemoveDuplicates(
    IN p_DuplicateType VARCHAR(50),  -- 'EXACT', 'SAME_TIMESTAMP', 'ALL'
    IN p_CreateBackup BOOLEAN,       -- TRUE to create backup, FALSE to skip
    OUT p_Status INT,                -- 1=Success with deletions, 0=No duplicates, -1=Error
    OUT p_ErrorMsg VARCHAR(500)      -- Success message with count or error
);
```

**Parameters**:
- `p_DuplicateType`: Type of duplicates to remove
  - `'EXACT'` - Exact duplicates (keeps lowest ID)
  - `'SAME_TIMESTAMP'` - OUT transactions with same timestamp as IN
  - `'ALL'` - All types of duplicates
- `p_CreateBackup`: TRUE to create backup table before deletion, FALSE to skip
- `p_Status`: Return status (1=Deleted records, 0=No duplicates found, -1=Error, -2=Invalid input)
- `p_ErrorMsg`: Return message with count of deleted records and backup table name

**Safety Features**:
- Wrapped in transaction (automatic rollback on error)
- Always keeps lowest ID for exact duplicates
- Optional backup table creation before deletion
- Backup table name format: `inv_transaction_backup_{type}_YYYYMMDD_HHMMSS`

**Usage Examples**:
```sql
-- Remove exact duplicates WITH backup
CALL maint_transactions_RemoveDuplicates('EXACT', TRUE, @status, @msg);
SELECT @status, @msg;

-- Remove same-timestamp duplicates WITHOUT backup
CALL maint_transactions_RemoveDuplicates('SAME_TIMESTAMP', FALSE, @status, @msg);
SELECT @status, @msg;

-- Remove all duplicates WITH backup (RECOMMENDED)
CALL maint_transactions_RemoveDuplicates('ALL', TRUE, @status, @msg);
SELECT @status, @msg;
```

## Recommended Workflow

### 1. Regular Maintenance (Weekly/Monthly)

```sql
-- Step 1: Check for duplicates
CALL maint_transactions_FindDuplicates('ALL', @status, @msg);
SELECT @status AS Status, @msg AS Message;

-- Step 2: If duplicates found, remove them with backup
CALL maint_transactions_RemoveDuplicates('ALL', TRUE, @status, @msg);
SELECT @status AS Status, @msg AS Message;

-- Step 3: Verify cleanup
CALL maint_transactions_FindDuplicates('ALL', @status, @msg);
SELECT @status AS Status, @msg AS Message;
```

### 2. Emergency Cleanup (No Backup)

**⚠️ WARNING**: Only use without backup if you're absolutely certain or storage is critical.

```sql
-- Remove all duplicates immediately
CALL maint_transactions_RemoveDuplicates('ALL', FALSE, @status, @msg);
SELECT @status AS Status, @msg AS Message;
```

### 3. Targeted Cleanup

```sql
-- Find and remove only same-timestamp issues
CALL maint_transactions_FindDuplicates('SAME_TIMESTAMP', @status, @msg);
CALL maint_transactions_RemoveDuplicates('SAME_TIMESTAMP', TRUE, @status, @msg);
SELECT @status AS Status, @msg AS Message;
```

## Backup Table Management

### Listing Backup Tables
```sql
SHOW TABLES LIKE 'inv_transaction_backup_%';
```

### Restoring from Backup
```sql
-- Example: Restore from specific backup
INSERT INTO inv_transaction 
SELECT * FROM inv_transaction_backup_all_20251103_193907;

-- Verify restoration
SELECT COUNT(*) FROM inv_transaction;
```

### Dropping Old Backups
```sql
-- After verifying data integrity
DROP TABLE IF EXISTS inv_transaction_backup_all_20251103_193907;
```

## Historical Context

**Issue Fixed**: 2025-11-03
- Removed 4,322 same-timestamp OUT duplicates
- These were OUT transactions created at the exact same second as their IN transactions
- Caused inventory integrity issues (removing more than was received)
- Root cause: Data entry error or system bug before proper configuration

**Prevention**: 
- These procedures help catch future occurrences
- Run regularly as part of database maintenance
- Monitor for new duplicates after system updates

## Integration with Application

These procedures can be called from C# using `Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync`:

```csharp
// Find duplicates
var findResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "maint_transactions_FindDuplicates",
    new Dictionary<string, object>
    {
        ["DuplicateType"] = "ALL"
    }
);

if (findResult.IsSuccess && findResult.Data.Rows.Count > 0)
{
    // Remove duplicates with backup
    var removeResult = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
        connectionString,
        "maint_transactions_RemoveDuplicates",
        new Dictionary<string, object>
        {
            ["DuplicateType"] = "ALL",
            ["CreateBackup"] = true
        }
    );
    
    if (removeResult.IsSuccess)
    {
        MessageBox.Show(removeResult.StatusMessage);
    }
}
```

## Performance Notes

- **EXACT duplicate check**: O(n²) for small datasets, uses indexed columns
- **SAME_TIMESTAMP check**: O(n) with index on (BatchNumber, TransactionType, ReceiveDate)
- **ALL mode**: Runs both checks sequentially
- **Recommended**: Run during off-peak hours for large databases
- **Indexes**: Ensure BatchNumber, TransactionType, and ReceiveDate are indexed

## Error Codes

- `1`: Success (duplicates found/removed)
- `0`: Success (no duplicates found)
- `-1`: Database error (see error message)
- `-2`: Invalid input parameter
