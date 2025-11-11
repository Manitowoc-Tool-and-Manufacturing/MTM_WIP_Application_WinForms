# Stored Procedure Specification: inv_transactions_GetBatchLifecycle

**Feature**: Transaction Viewer Form Redesign (P1 - Transaction Lifecycle Viewer)  
**Date**: 2025-11-01  
**Status**: Planning  
**Priority**: P1

## Overview

New stored procedure to retrieve all transactions for a specific batch number in chronological order. Required for Transaction Lifecycle TreeView visualization feature that shows batch history with split tracking.

---

## Stored Procedure Definition

### Name
`inv_transactions_GetBatchLifecycle`

### Purpose
Retrieve all transactions associated with a specific batch number, ordered chronologically for client-side tree building algorithm that detects splits.

### Parameters

#### Input Parameters
- **`p_BatchNumber`** VARCHAR(300) - The batch number to query (required)

#### Output Parameters
- **`p_Status`** INT - Status code
  - `0` = Success (records returned)
  - `1` = Success (no records found for batch)
  - `-1` = Error (invalid batch number or SQL error)
- **`p_ErrorMsg`** VARCHAR(500) - Error message (empty string on success)

### Return Type
**DataTable** with columns matching `inv_transaction` table structure:

| Column           | Type         | Nullable | Description                                    |
|------------------|--------------|----------|------------------------------------------------|
| ID               | INT(11)      | No       | Unique transaction identifier                  |
| TransactionType  | VARCHAR(20)  | No       | IN/OUT/TRANSFER                                |
| PartID           | VARCHAR(300) | No       | Part number                                    |
| BatchNumber      | VARCHAR(300) | Yes      | Batch identifier                               |
| Quantity         | INT(11)      | No       | Transaction quantity                           |
| FromLocation     | VARCHAR(100) | Yes      | Source location                                |
| ToLocation       | VARCHAR(100) | Yes      | Destination location (TRANSFER only)           |
| Operation        | VARCHAR(100) | Yes      | Manufacturing operation number                 |
| User             | VARCHAR(100) | No       | Username who created transaction               |
| ItemType         | VARCHAR(100) | No       | WIP/FG/RM/etc                                  |
| Notes            | TEXT         | Yes      | Optional transaction notes                     |
| ReceiveDate      | DATETIME     | No       | Transaction timestamp                          |

---

## SQL Implementation

```sql
DELIMITER $$

CREATE PROCEDURE inv_transactions_GetBatchLifecycle(
    IN p_BatchNumber VARCHAR(300),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Error retrieving batch lifecycle: ', SUBSTRING(MESSAGE_TEXT, 1, 400));
        ROLLBACK;
    END;
    
    -- Validate input
    IF p_BatchNumber IS NULL OR TRIM(p_BatchNumber) = '' THEN
        SET p_Status = -1;
        SET p_ErrorMsg = 'BatchNumber parameter is required';
        SELECT NULL LIMIT 0; -- Return empty result set
    ELSE
        -- Query transactions for batch in chronological order
        SELECT 
            ID,
            TransactionType,
            PartID,
            BatchNumber,
            Quantity,
            FromLocation,
            ToLocation,
            Operation,
            User,
            ItemType,
            Notes,
            ReceiveDate
        FROM inv_transaction
        WHERE BatchNumber = p_BatchNumber
        ORDER BY ReceiveDate ASC, ID ASC; -- Chronological order critical for tree building
        
        -- Set status based on result count
        IF FOUND_ROWS() > 0 THEN
            SET p_Status = 0;
            SET p_ErrorMsg = '';
        ELSE
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('No transactions found for batch: ', p_BatchNumber);
        END IF;
    END IF;
END$$

DELIMITER ;
```

---

## Database Schema Requirements

### Index Requirements

**Required Index**: `idx_batchnumber` on `inv_transaction.BatchNumber`

```sql
-- Check if index exists
SHOW INDEX FROM inv_transaction WHERE Key_name = 'idx_batchnumber';

-- Create index if missing (execute once during deployment)
CREATE INDEX idx_batchnumber ON inv_transaction(BatchNumber);
```

**Rationale**: BatchNumber queries will be frequent from Lifecycle viewer. Index ensures <50ms query time even with 24,000+ transaction records.

### Table Structure (Reference Only - No Changes)

Table: `inv_transaction`
- All columns exist in current schema
- No schema modifications required
- Procedure uses read-only SELECT

---

## Usage Example

### Direct MySQL Call
```sql
-- Declare output variables
SET @status = 0;
SET @errorMsg = '';

-- Execute procedure
CALL inv_transactions_GetBatchLifecycle('0000021324', @status, @errorMsg);

-- Check results
SELECT @status AS Status, @errorMsg AS ErrorMessage;
```

**Expected Result** (Batch 0000021324):
| ID    | TransactionType | PartID       | Quantity | FromLocation | ToLocation | ReceiveDate         |
|-------|-----------------|--------------|----------|--------------|------------|---------------------|
| 40361 | IN              | 21-28841-006 | 500      | X-00         | NULL       | 2025-11-01 20:47:31 |
| 40362 | TRANSFER        | 21-28841-006 | 250      | X-00         | X-04       | 2025-11-01 20:47:48 |
| 40363 | TRANSFER        | 21-28841-006 | 100      | X-04         | X-03       | 2025-11-01 20:48:37 |

### C# DAO Integration
```csharp
// File: Data/Dao_Transactions.cs

/// <summary>
/// Retrieves all transactions for a specific batch in chronological order.
/// </summary>
/// <param name="batchNumber">The batch number to query.</param>
/// <returns>Model_Dao_Result containing list of transactions, or error details.</returns>
internal static async Task<Model_Dao_Result<List<Model_Transactions_Core>>> GetBatchLifecycleAsync(string batchNumber)
{
    try
    {
        var connectionString = Helper_Database_Variables.GetConnectionString();
        var parameters = new Dictionary<string, object>
        {
            ["BatchNumber"] = batchNumber  // No p_ prefix in C# (helper adds it)
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            connectionString,
            "inv_transactions_GetBatchLifecycle",
            parameters,
            progressHelper: null,
            useAsync: true
        );

        if (!result.IsSuccess)
        {
            return Model_Dao_Result<List<Model_Transactions_Core>>.Failure(result.StatusMessage);
        }

        // Map DataTable to List<Model_Transactions_Core>
        var transactions = new List<Model_Transactions_Core>();
        foreach (DataRow row in result.Data.Rows)
        {
            transactions.Add(MapRowToTransaction(row));
        }

        return Model_Dao_Result<List<Model_Transactions_Core>>.Success(transactions);
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return Model_Dao_Result<List<Model_Transactions_Core>>.Failure($"Failed to retrieve batch lifecycle: {ex.Message}");
    }
}
```

---

## Performance Characteristics

### Expected Performance
- **Typical batch size**: 2-10 transactions per batch
- **Large batch size**: 20-50 transactions per batch
- **Maximum observed**: 100 transactions per batch
- **Query execution time**: <50ms with index (target <20ms)
- **Network transfer time**: <10ms (small result sets)
- **Total roundtrip**: <100ms for typical batches

### Optimization Strategies
- **Index on BatchNumber**: PRIMARY optimization - ensures fast WHERE clause execution
- **Covering index potential**: Could add (BatchNumber, ReceiveDate, ID) composite index if query becomes bottleneck
- **Result set size**: Chronological ordering happens in MySQL (efficient), no client-side sorting needed
- **Connection pooling**: Helper_Database_StoredProcedure reuses pooled connections

---

## Testing Strategy

### Manual Testing Checklist
- [ ] Execute procedure with valid batch number (0000021324) - verify chronological order
- [ ] Execute with non-existent batch number - verify p_Status = 1, p_ErrorMsg contains batch number
- [ ] Execute with NULL batch number - verify p_Status = -1, p_ErrorMsg = 'BatchNumber parameter is required'
- [ ] Execute with empty string batch number - verify p_Status = -1, validation message
- [ ] Verify idx_batchnumber index exists - query execution time <50ms
- [ ] Test with batch containing 1 transaction (IN only) - verify single row returned
- [ ] Test with batch containing splits (partial TRANSFERs) - verify all transactions returned in order

### Integration Test (BaseIntegrationTest Pattern)
```csharp
[TestMethod]
public async Task GetBatchLifecycleAsync_ValidBatch_ReturnsChronologicalTransactions()
{
    // Arrange
    var batchNumber = "0000021324"; // Known test batch

    // Act
    var result = await Dao_Transactions.GetBatchLifecycleAsync(batchNumber);

    // Assert
    Assert.IsTrue(result.IsSuccess, $"Expected success, got: {result.ErrorMessage}");
    Assert.IsNotNull(result.Data, "Expected non-null transaction list");
    Assert.IsTrue(result.Data.Count > 0, "Expected at least one transaction for known batch");
    
    // Verify chronological order
    for (int i = 1; i < result.Data.Count; i++)
    {
        Assert.IsTrue(result.Data[i].DateTime >= result.Data[i-1].DateTime, 
            "Transactions must be in chronological order");
    }
}
```

---

## Deployment Plan

### Step 1: Create Stored Procedure
```bash
# File location: Database/UpdatedStoredProcedures/ReadyForVerification/inv_transactions_GetBatchLifecycle.sql

# Deploy to test database
mysql -h localhost -P 3306 -u root -p mtm_wip_application_winforms_test < inv_transactions_GetBatchLifecycle.sql

# Verify creation
mysql -h localhost -P 3306 -u root -p mtm_wip_application_winforms_test -e "SHOW PROCEDURE STATUS WHERE Name='inv_transactions_GetBatchLifecycle';"
```

### Step 2: Create Index (If Missing)
```bash
# Check if index exists
mysql -h localhost -P 3306 -u root -p mtm_wip_application_winforms_test -e "SHOW INDEX FROM inv_transaction WHERE Key_name='idx_batchnumber';"

# Create if missing
mysql -h localhost -P 3306 -u root -p mtm_wip_application_winforms_test -e "CREATE INDEX idx_batchnumber ON inv_transaction(BatchNumber);"
```

### Step 3: Implement DAO Method
- Add `GetBatchLifecycleAsync` to `Data/Dao_Transactions.cs`
- Follow discovery-first workflow from `.github/instructions/integration-testing.instructions.md`
- Use `Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync`

### Step 4: Integration Testing
- Create test in `Tests/Integration/Dao_Transactions_Tests.cs`
- Validate chronological ordering
- Validate error handling (invalid batch, null batch)

### Step 5: Production Deployment
- Deploy stored procedure to production database (172.16.1.104)
- Verify index exists in production
- Test with real batch numbers
- Monitor query performance (target <50ms)

---

## Related Documentation

- **Specification**: `specs/005-transaction-viewer-form/spec.md` (US-012: Transaction Lifecycle Viewer)
- **Data Model**: `specs/005-transaction-viewer-form/data-model.md` (TransactionLifecycleNode, Flow 4)
- **Tasks**: `specs/005-transaction-viewer-form/tasks.md` (T071: Create SP, T072: Implement DAO)
- **Instruction**: `.github/instructions/mysql-database.instructions.md` (Stored procedure patterns)
- **Instruction**: `.github/instructions/integration-testing.instructions.md` (DAO testing workflow)

---

## Approval Checklist

Before moving to implementation (T071):
- [ ] SQL syntax validated (no syntax errors)
- [ ] Parameter naming follows MTM conventions (p_ prefix)
- [ ] Output parameters (p_Status, p_ErrorMsg) included
- [ ] Error handling (EXIT HANDLER FOR SQLEXCEPTION) implemented
- [ ] Chronological ordering (ORDER BY ReceiveDate ASC, ID ASC) confirmed
- [ ] Index requirement documented (idx_batchnumber)
- [ ] Integration test pattern defined
- [ ] Deployment steps documented
- [ ] Performance targets specified (<50ms query time)

**Status**: ✅ Ready for implementation (T071)
