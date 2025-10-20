# Stored Procedure Contracts

**Feature**: Developer Tools Suite Integration  
**Sub-Feature ID**: 002-003-001  
**Date**: 2025-10-18

---

## Overview

This document defines the stored procedure contracts for parameter prefix override CRUD operations. All procedures follow MTM standards with p_ parameter prefixes, OUT p_Status and p_ErrorMsg parameters, and comprehensive error handling.

---

## 1. sys_parameter_prefix_overrides_Get_All

### Purpose
Retrieve all active parameter prefix overrides for loading into application cache at startup.

### Signature
```sql
CREATE PROCEDURE sys_parameter_prefix_overrides_Get_All(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

### Parameters
| Name | Type | Mode | Description |
|------|------|------|-------------|
| p_Status | INT | OUT | 0 = Success, 1 = Success no data, -1 = Error |
| p_ErrorMsg | VARCHAR(500) | OUT | Error message or success details |

### Return Data
**Result Set**: All active override records (IsActive = 1)

| Column | Type | Description |
|--------|------|-------------|
| OverrideId | INT | Unique identifier |
| ProcedureName | VARCHAR(128) | Stored procedure name |
| ParameterName | VARCHAR(128) | Parameter name without prefix |
| OverridePrefix | VARCHAR(10) | Prefix to apply |
| Reason | VARCHAR(500) | Why override exists |
| CreatedBy | VARCHAR(50) | Creator username |
| CreatedDate | DATETIME | Creation timestamp |
| ModifiedBy | VARCHAR(50) | Last modifier username |
| ModifiedDate | DATETIME | Last modification timestamp |
| IsActive | TINYINT(1) | Always 1 for this query |

### Business Rules
- Returns only active overrides (IsActive = 1)
- Ordered by ProcedureName, ParameterName for predictable cache loading
- Returns empty result set if no overrides exist (p_Status = 1)

### Example Usage
```sql
CALL sys_parameter_prefix_overrides_Get_All(@status, @errorMsg);
SELECT @status, @errorMsg;
```

### C# Integration
```csharp
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "sys_parameter_prefix_overrides_Get_All",
    null, // No input parameters
    useAsync: true);

if (result.IsSuccess && result.Data != null)
{
    foreach (DataRow row in result.Data.Rows)
    {
        var override = new Model_ParameterPrefixOverride
        {
            OverrideId = Convert.ToInt32(row["OverrideId"]),
            ProcedureName = row["ProcedureName"].ToString() ?? "",
            ParameterName = row["ParameterName"].ToString() ?? "",
            OverridePrefix = row["OverridePrefix"].ToString() ?? "",
            Reason = row["Reason"]?.ToString(),
            CreatedBy = row["CreatedBy"].ToString() ?? "",
            CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
            ModifiedBy = row["ModifiedBy"]?.ToString(),
            ModifiedDate = row["ModifiedDate"] != DBNull.Value 
                ? Convert.ToDateTime(row["ModifiedDate"]) 
                : (DateTime?)null,
            IsActive = Convert.ToBoolean(row["IsActive"])
        };
        // Add to cache
    }
}
```

---

## 2. sys_parameter_prefix_overrides_Get_ById

### Purpose
Retrieve a specific parameter prefix override by its ID for editing operations.

### Signature
```sql
CREATE PROCEDURE sys_parameter_prefix_overrides_Get_ById(
    IN p_OverrideId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

### Parameters
| Name | Type | Mode | Description |
|------|------|------|-------------|
| p_OverrideId | INT | IN | Override record ID to retrieve |
| p_Status | INT | OUT | 0 = Success, 1 = Not found, -1 = Error |
| p_ErrorMsg | VARCHAR(500) | OUT | Error message or success details |

### Return Data
**Result Set**: Single override record (if found)

Same columns as Get_All procedure.

### Business Rules
- Returns record regardless of IsActive status
- p_Status = 1 if OverrideId not found
- p_Status = 0 if record found (even if IsActive = 0)

### Example Usage
```sql
CALL sys_parameter_prefix_overrides_Get_ById(5, @status, @errorMsg);
SELECT @status, @errorMsg;
```

### C# Integration
```csharp
var parameters = new Dictionary<string, object>
{
    ["OverrideId"] = overrideId
};

var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "sys_parameter_prefix_overrides_Get_ById",
    parameters,
    useAsync: true);

if (result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0)
{
    var row = result.Data.Rows[0];
    // Map to Model_ParameterPrefixOverride
}
```

---

## 3. sys_parameter_prefix_overrides_Add

### Purpose
Create a new parameter prefix override with validation and audit trail.

### Signature
```sql
CREATE PROCEDURE sys_parameter_prefix_overrides_Add(
    IN p_ProcedureName VARCHAR(128),
    IN p_ParameterName VARCHAR(128),
    IN p_OverridePrefix VARCHAR(10),
    IN p_Reason VARCHAR(500),
    IN p_CreatedBy VARCHAR(50),
    OUT p_OverrideId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

### Parameters
| Name | Type | Mode | Description |
|------|------|------|-------------|
| p_ProcedureName | VARCHAR(128) | IN | Stored procedure name |
| p_ParameterName | VARCHAR(128) | IN | Parameter name without prefix |
| p_OverridePrefix | VARCHAR(10) | IN | Prefix to apply (can be empty string) |
| p_Reason | VARCHAR(500) | IN | Why override exists (optional) |
| p_CreatedBy | VARCHAR(50) | IN | Current username |
| p_OverrideId | INT | OUT | Auto-generated ID of new record |
| p_Status | INT | OUT | 0 = Success, -2 = Validation error, -3 = Duplicate, -1 = Error |
| p_ErrorMsg | VARCHAR(500) | OUT | Error message or success details |

### Business Rules
- **Validation**:
  - p_ProcedureName must not be NULL or empty
  - p_ParameterName must not be NULL or empty
  - p_OverridePrefix can be empty string (valid for no-prefix procedures)
  - p_CreatedBy must not be NULL or empty
- **Duplicate Check**:
  - UNIQUE KEY violation on (ProcedureName, ParameterName)
  - Returns p_Status = -3, p_ErrorMsg = "Override already exists for this procedure-parameter combination"
- **Warning (not error)**: If procedure doesn't exist in INFORMATION_SCHEMA, log warning but allow insert
- **Audit Trail**:
  - CreatedDate set to CURRENT_TIMESTAMP automatically
  - IsActive set to 1 (active) by default
  - ModifiedBy and ModifiedDate remain NULL on insert

### Example Usage
```sql
CALL sys_parameter_prefix_overrides_Add(
    'inv_inventory_Add_Item',  -- p_ProcedureName
    'UserID',                  -- p_ParameterName
    'p_',                      -- p_OverridePrefix
    'Standard prefix',         -- p_Reason
    'JOHNK',                   -- p_CreatedBy
    @newId,                    -- OUT p_OverrideId
    @status,                   -- OUT p_Status
    @errorMsg                  -- OUT p_ErrorMsg
);
SELECT @newId, @status, @errorMsg;
```

### C# Integration
```csharp
var parameters = new Dictionary<string, object>
{
    ["ProcedureName"] = procedureName,
    ["ParameterName"] = parameterName,
    ["OverridePrefix"] = overridePrefix, // Can be empty string
    ["Reason"] = reason ?? (object)DBNull.Value,
    ["CreatedBy"] = Model_AppVariables.CurrentUser.UserName
};

var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "sys_parameter_prefix_overrides_Add",
    parameters,
    useAsync: true);

if (result.IsSuccess)
{
    // Extract p_OverrideId from OutputParameters
    int newOverrideId = Convert.ToInt32(result.OutputParameters["p_OverrideId"]);
}
else if (result.StatusMessage.Contains("already exists"))
{
    // Handle duplicate error
}
```

---

## 4. sys_parameter_prefix_overrides_Update_ById

### Purpose
Update an existing parameter prefix override with validation and audit trail.

### Signature
```sql
CREATE PROCEDURE sys_parameter_prefix_overrides_Update_ById(
    IN p_OverrideId INT,
    IN p_ProcedureName VARCHAR(128),
    IN p_ParameterName VARCHAR(128),
    IN p_OverridePrefix VARCHAR(10),
    IN p_Reason VARCHAR(500),
    IN p_IsActive TINYINT(1),
    IN p_ModifiedBy VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

### Parameters
| Name | Type | Mode | Description |
|------|------|------|-------------|
| p_OverrideId | INT | IN | Override record ID to update |
| p_ProcedureName | VARCHAR(128) | IN | Updated procedure name |
| p_ParameterName | VARCHAR(128) | IN | Updated parameter name |
| p_OverridePrefix | VARCHAR(10) | IN | Updated prefix |
| p_Reason | VARCHAR(500) | IN | Updated reason (optional) |
| p_IsActive | TINYINT(1) | IN | Active status (1 = active, 0 = inactive) |
| p_ModifiedBy | VARCHAR(50) | IN | Current username |
| p_Status | INT | OUT | 0 = Success, 1 = Not found, -2 = Validation error, -3 = Duplicate, -1 = Error |
| p_ErrorMsg | VARCHAR(500) | OUT | Error message or success details |

### Business Rules
- **Validation**: Same as Add procedure
- **Not Found**: p_Status = 1 if OverrideId doesn't exist
- **Duplicate Check**: UNIQUE KEY violation if changing ProcedureName/ParameterName to existing combination (excluding current record)
- **Audit Trail**:
  - ModifiedBy set to p_ModifiedBy parameter
  - ModifiedDate set to CURRENT_TIMESTAMP automatically
  - CreatedBy and CreatedDate remain unchanged

### Example Usage
```sql
CALL sys_parameter_prefix_overrides_Update_ById(
    5,                         -- p_OverrideId
    'inv_inventory_Add_Item',  -- p_ProcedureName
    'UserID',                  -- p_ParameterName
    'in_',                     -- p_OverridePrefix (changed)
    'Legacy prefix',           -- p_Reason (changed)
    1,                         -- p_IsActive
    'JOHNK',                   -- p_ModifiedBy
    @status,                   -- OUT p_Status
    @errorMsg                  -- OUT p_ErrorMsg
);
SELECT @status, @errorMsg;
```

### C# Integration
```csharp
var parameters = new Dictionary<string, object>
{
    ["OverrideId"] = overrideId,
    ["ProcedureName"] = procedureName,
    ["ParameterName"] = parameterName,
    ["OverridePrefix"] = overridePrefix,
    ["Reason"] = reason ?? (object)DBNull.Value,
    ["IsActive"] = isActive ? 1 : 0,
    ["ModifiedBy"] = Model_AppVariables.CurrentUser.UserName
};

var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "sys_parameter_prefix_overrides_Update_ById",
    parameters,
    useAsync: true);

if (result.IsSuccess)
{
    // Override updated successfully
}
```

---

## 5. sys_parameter_prefix_overrides_Delete_ById

### Purpose
Soft-delete a parameter prefix override (set IsActive = 0) with audit trail.

### Signature
```sql
CREATE PROCEDURE sys_parameter_prefix_overrides_Delete_ById(
    IN p_OverrideId INT,
    IN p_ModifiedBy VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

### Parameters
| Name | Type | Mode | Description |
|------|------|------|-------------|
| p_OverrideId | INT | IN | Override record ID to delete |
| p_ModifiedBy | VARCHAR(50) | IN | Current username |
| p_Status | INT | OUT | 0 = Success, 1 = Not found, -1 = Error |
| p_ErrorMsg | VARCHAR(500) | OUT | Error message or success details |

### Business Rules
- **Soft Delete**: Sets IsActive = 0 instead of physical DELETE
- **Not Found**: p_Status = 1 if OverrideId doesn't exist
- **Already Deleted**: p_Status = 0 if record already has IsActive = 0 (idempotent operation)
- **Audit Trail**:
  - ModifiedBy set to p_ModifiedBy parameter
  - ModifiedDate set to CURRENT_TIMESTAMP automatically
- **Recovery**: Record can be reactivated with Update_ById procedure (set IsActive = 1)

### Example Usage
```sql
CALL sys_parameter_prefix_overrides_Delete_ById(
    5,        -- p_OverrideId
    'JOHNK',  -- p_ModifiedBy
    @status,  -- OUT p_Status
    @errorMsg -- OUT p_ErrorMsg
);
SELECT @status, @errorMsg;
```

### C# Integration
```csharp
var parameters = new Dictionary<string, object>
{
    ["OverrideId"] = overrideId,
    ["ModifiedBy"] = Model_AppVariables.CurrentUser.UserName
};

var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "sys_parameter_prefix_overrides_Delete_ById",
    parameters,
    useAsync: true);

if (result.IsSuccess)
{
    // Override soft-deleted successfully
}
else if (result.StatusMessage.Contains("not found"))
{
    // Handle not found error
}
```

---

## Error Handling Standards

### Status Codes
| Code | Meaning | Usage |
|------|---------|-------|
| 0 | Success | Operation completed successfully |
| 1 | Success (No Data) | Query succeeded but no records found/affected |
| -1 | General Error | Unexpected error (database connection, syntax, etc.) |
| -2 | Validation Error | Input validation failed (empty required fields, invalid format) |
| -3 | Duplicate Error | UNIQUE constraint violation |
| -4 | Foreign Key Error | Referenced record doesn't exist (not used in this feature) |
| -5 | Permission Error | User lacks required permissions (not used in this feature) |

### Transaction Management
- **Add/Update/Delete**: Each procedure uses START TRANSACTION / COMMIT / ROLLBACK pattern
- **Get operations**: No transaction needed (read-only SELECT queries)
- **Rollback triggers**: Any validation failure or error condition triggers ROLLBACK
- **Commit triggers**: Only committed after all validation passes and operation succeeds

### Error Logging
- All procedures log errors to application error log (Dao_ErrorLog integration)
- Error messages are user-friendly and actionable
- Technical details logged separately for debugging
- No sensitive data exposed in error messages

---

## Performance Considerations

### Indexing Strategy
```sql
-- Primary key for fast ID lookups (Get_ById, Update_ById, Delete_ById)
PRIMARY KEY (OverrideId)

-- Unique constraint prevents duplicates and speeds up existence checks (Add, Update)
UNIQUE KEY UQ_ProcParam (ProcedureName, ParameterName)

-- Index for filtering active overrides (Get_All)
INDEX IDX_IsActive (IsActive)

-- Index for procedure name lookups (Get_All ordered by ProcedureName)
INDEX IDX_ProcName (ProcedureName)
```

### Query Optimization
- **Get_All**: Uses INDEX IDX_IsActive for WHERE IsActive = 1 filter
- **Get_ById**: Uses PRIMARY KEY for O(1) lookup
- **Add**: Uses UNIQUE KEY for duplicate check before INSERT
- **Update**: Uses PRIMARY KEY for target row, UNIQUE KEY for duplicate check
- **Delete**: Uses PRIMARY KEY for target row

### Expected Performance
- **Get_All**: <50ms for 100 override records
- **Get_ById**: <5ms (primary key lookup)
- **Add**: <10ms (includes validation and duplicate check)
- **Update**: <10ms (includes validation and duplicate check)
- **Delete**: <5ms (soft delete, no cascade)

---

## Testing Checklist

### Add Procedure Tests
- [ ] Add override with all fields populated
- [ ] Add override with NULL reason (optional field)
- [ ] Add override with empty string prefix (no-prefix procedures)
- [ ] Attempt duplicate add (should return p_Status = -3)
- [ ] Add with empty ProcedureName (should return p_Status = -2)
- [ ] Add with empty ParameterName (should return p_Status = -2)
- [ ] Add with empty CreatedBy (should return p_Status = -2)
- [ ] Verify CreatedDate auto-populated
- [ ] Verify IsActive defaults to 1

### Update Procedure Tests
- [ ] Update all fields successfully
- [ ] Update with non-existent OverrideId (should return p_Status = 1)
- [ ] Update causing duplicate (should return p_Status = -3)
- [ ] Update to IsActive = 0 (soft delete via Update)
- [ ] Verify ModifiedBy and ModifiedDate populated
- [ ] Verify CreatedBy and CreatedDate unchanged

### Delete Procedure Tests
- [ ] Delete existing override (should return p_Status = 0)
- [ ] Delete non-existent OverrideId (should return p_Status = 1)
- [ ] Delete already-deleted override (should remain IsActive = 0, return p_Status = 0)
- [ ] Verify ModifiedBy and ModifiedDate populated
- [ ] Verify record still exists in table (soft delete)

### Get Procedures Tests
- [ ] Get_All returns only active overrides
- [ ] Get_All returns empty result set if no active overrides
- [ ] Get_All orders by ProcedureName, ParameterName
- [ ] Get_ById returns existing override
- [ ] Get_ById returns empty result set for non-existent ID
- [ ] Get_ById returns inactive overrides (IsActive = 0)

---

## Deployment Script

```sql
-- File: Database/UpdatedStoredProcedures/ReadyForVerification/sys_parameter_prefix_overrides_ALL.sql
-- Description: All 5 stored procedures for parameter prefix override management
-- Date: 2025-10-18

DELIMITER $$

-- Procedure 1: Get All Active Overrides
DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Get_All$$
CREATE PROCEDURE sys_parameter_prefix_overrides_Get_All(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Implementation details in separate .sql files
END$$

-- Procedure 2: Get Override By ID
DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Get_ById$$
CREATE PROCEDURE sys_parameter_prefix_overrides_Get_ById(
    IN p_OverrideId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Implementation details in separate .sql files
END$$

-- Procedure 3: Add New Override
DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Add$$
CREATE PROCEDURE sys_parameter_prefix_overrides_Add(
    IN p_ProcedureName VARCHAR(128),
    IN p_ParameterName VARCHAR(128),
    IN p_OverridePrefix VARCHAR(10),
    IN p_Reason VARCHAR(500),
    IN p_CreatedBy VARCHAR(50),
    OUT p_OverrideId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Implementation details in separate .sql files
END$$

-- Procedure 4: Update Override By ID
DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Update_ById$$
CREATE PROCEDURE sys_parameter_prefix_overrides_Update_ById(
    IN p_OverrideId INT,
    IN p_ProcedureName VARCHAR(128),
    IN p_ParameterName VARCHAR(128),
    IN p_OverridePrefix VARCHAR(10),
    IN p_Reason VARCHAR(500),
    IN p_IsActive TINYINT(1),
    IN p_ModifiedBy VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Implementation details in separate .sql files
END$$

-- Procedure 5: Delete (Soft) Override By ID
DROP PROCEDURE IF EXISTS sys_parameter_prefix_overrides_Delete_ById$$
CREATE PROCEDURE sys_parameter_prefix_overrides_Delete_ById(
    IN p_OverrideId INT,
    IN p_ModifiedBy VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Implementation details in separate .sql files
END$$

DELIMITER ;
```
