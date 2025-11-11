## 📋 Data Integrity Validation & Fix Plan

### **Objective**
Create a comprehensive system for validating and fixing inventory transaction data integrity that can be run at any time, integrated into a future Developer Maintenance Panel.

### **Reference Files**
**Location**: `specs/000-future-specs/TransactionIntegrityMaintenance.md/`

- **cleanup_old_transactions.sql** - Removes transactions older than 3 months
- **validate_inventory_integrity.sql** - Comprehensive validation query showing all integrity issues
- **run_validation.bat** - Windows batch file to execute validation

**Current Validation Results** (2025-11-07):
- ❌ 489 Missing Unexplained INs
- ❌ 217 Orphaned OUTs (no matching IN)
- ❌ 19 Orphaned TRANSFERs (no matching IN)
- ❌ 33 Timestamp Violations (OUT/TRANSFER before IN)
- ✅ 0 Transfer Location Issues

---

## **Phase 1: Stored Procedures (SQL)**

### **1.1 Validation Stored Procedure**
**File**: `sp_validate_inventory_integrity.sql`
- **Purpose**: Returns all integrity metrics as result sets
- **Output**: Multiple result sets showing counts and problematic batches
- **Parameters**: 
  - `p_date_range_months INT` (default 3) - How far back to validate
  - `p_status OUT INT` - Success/failure status
  - `p_error_msg OUT VARCHAR(500)` - Error message if any

### **1.2 Fix Stored Procedures (Separate for Safety)**

**A. `sp_fix_orphaned_outs.sql`**
- **Purpose**: Delete OUT transactions that have no corresponding IN
- **Logic**: Delete any OUT where no IN exists with same BatchNumber
- **Parameters**:
  - `p_batch_number VARCHAR(300)` (NULL = all orphans)
  - `p_dry_run BOOLEAN` (default TRUE) - Preview changes without executing
  - `p_status OUT INT`
  - `p_error_msg OUT VARCHAR(500)`
  - `p_rows_affected OUT INT`

**B. `sp_fix_orphaned_transfers.sql`**
- **Purpose**: Delete TRANSFER transactions that have no corresponding IN
- **Logic**: Delete any TRANSFER where no IN exists with same BatchNumber
- **Parameters**: Same as above

**C. `sp_fix_timestamp_violations.sql`**
- **Purpose**: Fix OUT/TRANSFER transactions that occurred before their IN
- **Logic**: 
  - Option 1: Update OUT/TRANSFER timestamp to be 1 second after IN
  - Option 2: Delete the OUT/TRANSFER (configurable)
- **Parameters**:
  - `p_fix_method ENUM('UPDATE_TIMESTAMP','DELETE')` (default UPDATE_TIMESTAMP)
  - `p_batch_number VARCHAR(300)` (NULL = all violations)
  - `p_dry_run BOOLEAN`
  - `p_status OUT INT`
  - `p_error_msg OUT VARCHAR(500)`
  - `p_rows_affected OUT INT`

**D. `sp_fix_missing_unexplained_ins.sql`**
- **Purpose**: Handle INs that vanished without OUT/TRANSFER
- **Logic**: 
  - Option 1: Create compensating OUT transaction (reconciliation)
  - Option 2: Delete the orphaned IN (cleanup)
  - Option 3: Report only (default - manual review required)
- **Parameters**:
  - `p_fix_method ENUM('CREATE_OUT','DELETE_IN','REPORT_ONLY')` (default REPORT_ONLY)
  - `p_batch_number VARCHAR(300)` (NULL = all missing)
  - `p_dry_run BOOLEAN`
  - `p_status OUT INT`
  - `p_error_msg OUT VARCHAR(500)`
  - `p_rows_affected OUT INT`

**E. `sp_fix_transfer_location_violations.sql`**
- **Purpose**: Fix TRANSFERs with missing or invalid locations
- **Logic**: Delete TRANSFERs with NULL locations or same From/To
- **Parameters**: Same pattern as above

---

## **Phase 2: C# DAO Layer**

### **2.1 New DAO Class**
**File**: `Data/Dao_DataIntegrity.cs`

**Methods**:
```csharp
// Validation
Task<Model_Dao_Result<DataIntegrityReport>> ValidateInventoryIntegrityAsync(int monthsBack = 3);

// Fix operations (all with dry-run support)
Task<Model_Dao_Result<FixResult>> FixOrphanedOutsAsync(string? batchNumber = null, bool dryRun = true);
Task<Model_Dao_Result<FixResult>> FixOrphanedTransfersAsync(string? batchNumber = null, bool dryRun = true);
Task<Model_Dao_Result<FixResult>> FixTimestampViolationsAsync(
    TimestampFixMethod method, string? batchNumber = null, bool dryRun = true);
Task<Model_Dao_Result<FixResult>> FixMissingExplainedInsAsync(
    MissingInFixMethod method, string? batchNumber = null, bool dryRun = true);
Task<Model_Dao_Result<FixResult>> FixTransferLocationViolationsAsync(string? batchNumber = null, bool dryRun = true);

// Bulk fix (applies all fixes with single transaction)
Task<Model_Dao_Result<BulkFixResult>> FixAllIssuesAsync(FixAllOptions options, bool dryRun = true);
```

### **2.2 Models**
**File**: `Models/Model_DataIntegrity.cs`

**Classes**:
```csharp
internal class DataIntegrityReport
{
    public InTransactionMetrics InMetrics { get; set; }
    public OutTransactionMetrics OutMetrics { get; set; }
    public TransferTransactionMetrics TransferMetrics { get; set; }
    public TimestampViolations TimestampIssues { get; set; }
    public List<ProblematicBatch> ProblematicBatches { get; set; }
    public DateTime ValidationDate { get; set; }
    public int MonthsValidated { get; set; }
}

internal class FixResult
{
    public bool Success { get; set; }
    public int RowsAffected { get; set; }
    public string Message { get; set; }
    public List<string> AffectedBatches { get; set; }
}

internal enum TimestampFixMethod { UpdateTimestamp, Delete }
internal enum MissingInFixMethod { CreateOut, DeleteIn, ReportOnly }
```

---

## **Phase 3: UI - Developer Maintenance Panel**

### **3.1 New Form**
**File**: `Forms/Development/DataIntegrityMaintenance.cs`

**Layout**:
```
┌─────────────────────────────────────────────────────────┐
│  Data Integrity Validation & Maintenance               │
├─────────────────────────────────────────────────────────┤
│  [Run Validation]  Date Range: [3 months ▼]  [Refresh]│
├─────────────────────────────────────────────────────────┤
│  📊 VALIDATION SUMMARY                                  │
│  ┌────────────────────────────────────────────────────┐│
│  │ ✅ IN Transactions: 14,062                         ││
│  │ ⚠️  Missing Unexplained: 489                       ││
│  │ ❌ Orphaned OUTs: 217                              ││
│  │ ❌ Orphaned TRANSFERs: 19                          ││
│  │ ❌ Timestamp Violations: 33                        ││
│  └────────────────────────────────────────────────────┘│
├─────────────────────────────────────────────────────────┤
│  🛠️ FIX OPTIONS                                        │
│  ┌────────────────────────────────────────────────────┐│
│  │ [☑] Fix Orphaned OUTs (217 batches)               ││
│  │ [☑] Fix Orphaned TRANSFERs (19 batches)           ││
│  │ [☑] Fix Timestamp Violations (33 records)         ││
│  │     Method: [Update Timestamp ▼]                   ││
│  │ [☐] Fix Missing INs (489 batches) ⚠️ DANGEROUS     ││
│  │     Method: [Report Only ▼]                        ││
│  └────────────────────────────────────────────────────┘│
│                                                         │
│  [🔍 Preview Changes (Dry Run)]  [✅ Apply Fixes]      │
├─────────────────────────────────────────────────────────┤
│  📋 DETAILED RESULTS                                    │
│  ┌────────────────────────────────────────────────────┐│
│  │ [DataGridView showing problematic batches]         ││
│  │ BatchNumber | Type | Issue | Recommended Fix       ││
│  └────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────┘
```

**Features**:
- Real-time validation
- Dry-run preview before applying fixes
- Selective fix application (checkboxes)
- Detailed batch-level grid showing issues
- Export results to Excel
- Logging all operations

---

## **Phase 4: Implementation Steps**

### **Step 1: Create Stored Procedures**
1. Create validation SP (base query exists in `validate_inventory_integrity.sql`)
2. Create each fix SP with dry-run support
3. Test each SP individually with known data
4. Add to `Database/UpdatedStoredProcedures/ReadyForVerification/`

**Note**: Current validation query (`validate_inventory_integrity.sql`) serves as the foundation for the validation stored procedure.

### **Step 2: Create DAO & Models**
1. Create `Model_DataIntegrity.cs` with all DTOs
2. Create `Dao_DataIntegrity.cs` wrapping all SPs
3. Add integration tests for each method
4. Validate dry-run vs actual execution

### **Step 3: Create UI**
1. Create `Forms/Development/DataIntegrityMaintenance.cs`
2. Wire up validation button → DAO call → display results
3. Add fix preview (dry-run) functionality
4. Add actual fix execution with confirmation dialog
5. Add export to Excel capability

### **Step 4: Integration**
1. Add menu item to MainForm (Developer section)
2. Add role-based access (admin only)
3. Add comprehensive logging to `log_changelog`
4. Create user documentation

---

## **Phase 5: Safety Features**

### **Built-in Safeguards**:
1. **Dry-Run Default**: All fix operations default to `dryRun = true`
2. **Confirmation Dialogs**: Multi-step confirmation for destructive operations
3. **Batch Backups**: Create backup table before bulk operations
4. **Transaction Rollback**: Wrap all fixes in transactions
5. **Audit Logging**: Log every validation and fix to `log_changelog`
6. **User Permissions**: Restrict to admin users only

### **Validation Before Fix**:
- Re-validate before applying fixes (data may have changed)
- Show preview of what will be affected
- Require explicit user confirmation

---

## **Deliverables**

1. ✅ 6 Stored Procedures (1 validate + 5 fix)
2. ✅ DAO class with full async support
3. ✅ Model classes for all DTOs
4. ✅ Developer Maintenance Panel UI
5. ✅ Integration tests for DAOs
6. ✅ User documentation
7. ✅ Audit logging integration

---

**Approve this plan or request modifications?**