# Transaction Duplicate Mitigation Scripts Archive

**Date**: November 3-4, 2025  
**Branch**: 005-transaction-viewer-form  
**Issue**: Duplicate transaction records in `inv_transaction` table

## Problem Summary

Two types of duplicate transactions were discovered:

1. **Exact Duplicates** (15 records) - All fields including timestamp matched
2. **Same-Timestamp OUT Duplicates** (4,322 records) - OUT transactions with exact same timestamp as their IN transaction

## Resolution

These ad-hoc scripts were used to analyze and fix the duplicates. They have been **superseded by permanent stored procedures**:

- `maint_transactions_FindDuplicates` - Finds duplicates (EXACT, SAME_TIMESTAMP, or ALL)
- `maint_transactions_RemoveDuplicates` - Removes duplicates with optional backup

## Archived Scripts

### Analysis Scripts

1. **analyze_and_fix_duplicates.sql**
   - Initial analysis script to identify duplicate patterns
   - Used to understand the scope of the problem

2. **find_batches_with_multiple_in_out.sql**
   - Analyzed batch lifecycle patterns
   - Identified batches with more OUT than IN transactions

3. **monitor_transaction_duplicates.sql**
   - Ongoing monitoring script
   - Tracked duplicate counts during investigation

### Fix Scripts (Applied Successfully)

4. **fix_transaction_duplicates_SAFE.sql**
   - Early version of duplicate removal
   - Included safety checks and backup creation

5. **fix_all_transaction_duplicates_COMPREHENSIVE.sql**
   - Comprehensive duplicate detection and removal
   - Handled both exact and same-timestamp duplicates
   - **Applied**: Removed 15 exact duplicates

6. **fix_same_timestamp_out_duplicates.sql**
   - Targeted fix for same-timestamp OUT duplicates
   - **Applied**: Removed 4,322 same-timestamp OUT records
   - Created backup: `inv_transaction_same_timestamp_backup_20251103`

## Results

### Before Fix
- Total transactions: 24,127
- IN: 14,911
- OUT: 8,951 (included 4,322 erroneous same-timestamp duplicates)
- TRANSFER: 265

### After Fix
- Total transactions: 19,805
- IN: 14,911
- OUT: 4,629 (removed 4,322 duplicates)
- TRANSFER: 265

### Data Integrity Restored
- ✅ Zero exact duplicates remaining
- ✅ Zero same-timestamp OUT duplicates
- ✅ Backup tables created: 
  - `inv_transaction_backup_exact_20251103_HHMMSS`
  - `inv_transaction_same_timestamp_backup_20251103`

## Future Maintenance

**DO NOT use these archived scripts**. Instead, use the permanent stored procedures:

```sql
-- Find duplicates
CALL maint_transactions_FindDuplicates('ALL', @status, @msg);

-- Remove duplicates with backup
CALL maint_transactions_RemoveDuplicates('ALL', TRUE, @status, @msg);
```

See: `Database/UpdatedStoredProcedures/ReadyForVerification/maintenance/README_MAINTENANCE_PROCEDURES.md`

## Root Cause

The same-timestamp duplicates were caused by a data entry error or system bug that created OUT transactions at the exact same second as their IN transactions. This was corrected before the system was properly configured.

## Prevention

Run the maintenance stored procedures regularly (weekly/monthly) to catch any future duplicate occurrences.
