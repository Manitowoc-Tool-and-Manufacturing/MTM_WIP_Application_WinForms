# Data Model: Developer Tools Suite

**Feature**: Developer Tools Suite Integration  
**Sub-Feature ID**: 002-003-001  
**Date**: 2025-10-18

---

## Overview

This document defines the data entities, database schema, and data flow patterns for the Developer Tools Suite. The feature introduces one new database table (`sys_parameter_prefix_overrides`) and several runtime data structures for caching and displaying developer-focused information.

---

## Database Entities

### 1. ParameterPrefixOverride (Database Table)

**Purpose**: Store non-standard parameter prefix mappings for stored procedures that don't follow the p\_ convention, enabling gradual migration and supporting legacy/vendor procedures.

**Table Name**: `sys_parameter_prefix_overrides`

**Schema**:

| Column Name    | Data Type    | Constraints                         | Description                                                                 |
| -------------- | ------------ | ----------------------------------- | --------------------------------------------------------------------------- |
| OverrideId     | INT          | PRIMARY KEY, AUTO_INCREMENT         | Unique identifier for override record                                       |
| ProcedureName  | VARCHAR(128) | NOT NULL                            | Name of stored procedure (e.g., "inv_inventory_Add_Item")                   |
| ParameterName  | VARCHAR(128) | NOT NULL                            | Parameter name WITHOUT prefix (e.g., "UserID")                              |
| OverridePrefix | VARCHAR(10)  | NOT NULL                            | Prefix to use (e.g., "p*", "in*", "" for no prefix)                         |
| Reason         | VARCHAR(500) | NULL                                | Why this override exists (e.g., "Legacy 2020 procedure", "Vendor-supplied") |
| CreatedBy      | VARCHAR(50)  | NOT NULL                            | Username who created the override                                           |
| CreatedDate    | DATETIME     | NOT NULL, DEFAULT CURRENT_TIMESTAMP | When override was created                                                   |
| ModifiedBy     | VARCHAR(50)  | NULL                                | Username who last modified the override                                     |
| ModifiedDate   | DATETIME     | NULL                                | When override was last modified                                             |
| IsActive       | TINYINT(1)   | NOT NULL, DEFAULT 1                 | Whether override is currently active (1) or disabled (0)                    |

**Indexes**:

```sql
UNIQUE KEY UQ_ProcParam (ProcedureName, ParameterName)  -- Prevent duplicate overrides
INDEX IDX_ProcName (ProcedureName)                      -- Fast lookup by procedure
INDEX IDX_IsActive (IsActive)                           -- Filter active overrides
```

**Relationships**:

-   References `usr_users` table via CreatedBy and ModifiedBy (foreign key not enforced for simplicity)
-   No direct foreign keys to INFORMATION_SCHEMA (stored procedures are metadata)

**Validation Rules**:

-   ProcedureName must not be empty
-   ParameterName must not be empty
-   OverridePrefix can be empty string (for no-prefix procedures)
-   Unique constraint enforces one override per procedure-parameter pair
-   IsActive allows soft-delete pattern (set to 0 instead of DELETE)

**Example Records**:

```sql
-- Legacy procedure with in_ prefix
INSERT INTO sys_parameter_prefix_overrides
(ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, IsActive)
VALUES
('sys_old_procedure', 'UserID', 'in_', 'Legacy 2020 procedure uses in_ convention', 'JOHNK', 1);

-- Vendor procedure with custom prefix
INSERT INTO sys_parameter_prefix_overrides
(ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, IsActive)
VALUES
('vendor_import_data', 'ImportID', 'vnd_', 'Vendor-supplied procedure, cannot modify', 'JOHNK', 1);

-- Procedure with no prefix (rare but exists)
INSERT INTO sys_parameter_prefix_overrides
(ProcedureName, ParameterName, OverridePrefix, Reason, CreatedBy, IsActive)
VALUES
('md_legacy_check', 'ItemType', '', 'Old procedure has no parameter prefixes', 'JOHNK', 1);
```

---

## Application Data Models (C# POCOs)

### 2. Model_ParameterPrefix_Override

**Purpose**: C# representation of database override record for in-memory operations.

**File**: `Models/Model_ParameterPrefix_Override.cs`

**Properties**:

```csharp
namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Represents a parameter prefix override for a stored procedure parameter.
    /// Enables non-standard parameter naming without modifying the stored procedure itself.
    /// </summary>
    public class Model_ParameterPrefix_Override
    {
        /// <summary>
        /// Unique identifier for the override record
        /// </summary>
        public int OverrideId { get; set; }

        /// <summary>
        /// Name of the stored procedure (e.g., "inv_inventory_Add_Item")
        /// </summary>
        public string ProcedureName { get; set; } = string.Empty;

        /// <summary>
        /// Parameter name WITHOUT prefix (e.g., "UserID")
        /// </summary>
        public string ParameterName { get; set; } = string.Empty;

        /// <summary>
        /// Prefix to apply (e.g., "p_", "in_", "" for no prefix)
        /// </summary>
        public string OverridePrefix { get; set; } = string.Empty;

        /// <summary>
        /// Reason for override existence
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// Username who created this override
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// When override was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Username who last modified this override
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// When override was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Whether override is currently active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Computed property: Full parameter name with prefix applied
        /// </summary>
        public string FullParameterName => $"{OverridePrefix}{ParameterName}";
    }
}
```

**Validation Rules** (enforced in DAO layer):

-   ProcedureName and ParameterName required (not null or empty)
-   OverridePrefix can be empty (valid for no-prefix procedures)
-   CreatedBy required
-   CreatedDate automatically set on insert
-   ModifiedBy/ModifiedDate set on update

---

### 3. Model_SchemaTable (Runtime Only)

**Purpose**: Represents a database table from INFORMATION_SCHEMA.TABLES for display in Schema Inspector.

**Properties**:

```csharp
public class Model_SchemaTable
{
    public string TableName { get; set; } = string.Empty;
    public string TableType { get; set; } = "BASE TABLE"; // or "VIEW"
    public long RowCount { get; set; }
    public string Engine { get; set; } = "InnoDB";
    public DateTime? CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public string? TableComment { get; set; }
}
```

**Data Source**: `SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'MTM_WIP_Application_Winforms'`

---

### 4. Model_SchemaColumn (Runtime Only)

**Purpose**: Represents a table column from INFORMATION_SCHEMA.COLUMNS for display in Schema Inspector.

**Properties**:

```csharp
public class Model_SchemaColumn
{
    public string ColumnName { get; set; } = string.Empty;
    public int OrdinalPosition { get; set; }
    public string? ColumnDefault { get; set; }
    public bool IsNullable { get; set; }
    public string DataType { get; set; } = string.Empty;
    public long? CharacterMaximumLength { get; set; }
    public int? NumericPrecision { get; set; }
    public int? NumericScale { get; set; }
    public string? ColumnKey { get; set; } // "PRI", "UNI", "MUL"
    public string? Extra { get; set; } // "auto_increment", etc.
    public string? ColumnComment { get; set; }
}
```

**Data Source**: `SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'MTM_WIP_Application_Winforms' AND TABLE_NAME = @TableName`

---

### 5. Model_StoredProcedure (Runtime Only)

**Purpose**: Represents a stored procedure from INFORMATION_SCHEMA.ROUTINES for display in Schema Inspector.

**Properties**:

```csharp
public class Model_StoredProcedure
{
    public string RoutineName { get; set; } = string.Empty;
    public string RoutineType { get; set; } = "PROCEDURE";
    public DateTime? Created { get; set; }
    public DateTime? LastAltered { get; set; }
    public string? RoutineDefinition { get; set; } // SQL code
    public string? RoutineComment { get; set; }
    public int ParameterCount { get; set; }
}
```

**Data Source**: `SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = 'MTM_WIP_Application_Winforms' AND ROUTINE_TYPE = 'PROCEDURE'`

---

### 6. Model_ProcedureParameter (Runtime Only)

**Purpose**: Represents a procedure parameter from INFORMATION_SCHEMA.PARAMETERS for display in Schema Inspector.

**Properties**:

```csharp
public class Model_ProcedureParameter
{
    public int OrdinalPosition { get; set; }
    public string ParameterMode { get; set; } = "IN"; // "IN", "OUT", "INOUT"
    public string ParameterName { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public long? CharacterMaximumLength { get; set; }
    public int? NumericPrecision { get; set; }
    public int? NumericScale { get; set; }
    public string? DetectedPrefix { get; set; } // Extracted from ParameterName
}
```

**Data Source**: `SELECT * FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_SCHEMA = 'MTM_WIP_Application_Winforms' AND SPECIFIC_NAME = @ProcedureName ORDER BY ORDINAL_POSITION`

**Computed Field**: `DetectedPrefix` extracts prefix from ParameterName (e.g., "p*UserID" → "p*")

---

### 7. Model_ProcedureCallSite (Runtime Only)

**Purpose**: Represents a C# call site for a stored procedure from STORED_PROCEDURE_CALLSITES.csv.

**Properties**:

```csharp
public class Model_ProcedureCallSite
{
    public string ProcedureName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public int LineNumber { get; set; }
    public string CallContext { get; set; } = string.Empty; // Method name
    public string CodeSnippet { get; set; } = string.Empty; // Line of code
}
```

**Data Source**: `Database/STORED_PROCEDURE_CALLSITES.csv` (parsed at runtime)

---

### 8. Model_ProcedureDependency (Runtime Only)

**Purpose**: Represents procedure-to-procedure dependencies from call-hierarchy-complete.json.

**Properties**:

```csharp
public class Model_ProcedureDependency
{
    public string ProcedureName { get; set; } = string.Empty;
    public List<string> CallsProcedures { get; set; } = new();
    public List<string> CalledByProcedures { get; set; } = new();
    public int DependencyDepth { get; set; } // 0 = leaf, higher = more dependencies
    public bool IsRootProcedure => CalledByProcedures.Count == 0;
    public bool IsLeafProcedure => CallsProcedures.Count == 0;
    public bool HasCircularDependency { get; set; }
}
```

**Data Source**: `Database/call-hierarchy-complete.json` (parsed at runtime)

---

### 9. Model_GeneratedDaoMethod (Runtime Only)

**Purpose**: Represents generated C# DAO method code from Code Generator.

**Properties**:

```csharp
public class Model_GeneratedDaoMethod
{
    public string ProcedureName { get; set; } = string.Empty;
    public string MethodName { get; set; } = string.Empty;
    public string GeneratedCode { get; set; } = string.Empty;
    public List<Model_ProcedureParameter> Parameters { get; set; } = new();
    public string ReturnType { get; set; } = "Task<Model_Dao_Result<DataTable>>";
    public bool HasComplexityWarning => Parameters.Count > 10;
    public DateTime GeneratedDate { get; set; } = DateTime.Now;
}
```

**Computed Fields**:

-   `MethodName`: Derived from ProcedureName (e.g., "inv_inventory_Add_Item" → "AddItemAsync")
-   `HasComplexityWarning`: True if parameter count exceeds 10 (triggers warning comment in generated code)

---

## Data Flow Patterns

### Parameter Prefix Override Cache Loading (Startup)

```
Application Startup
    ↓
Helper_Database_StoredProcedure.LoadParameterPrefixOverridesAsync()
    ↓
Dao_ParameterPrefixOverrides.GetAllActiveAsync()
    ↓
sys_parameter_prefix_overrides_Get_All stored procedure
    ↓
Returns: List<Model_ParameterPrefix_Override>
    ↓
Store in static Dictionary<string, Dictionary<string, string>>
    Key1: ProcedureName
    Key2: ParameterName
    Value: OverridePrefix
    ↓
Cache populated for runtime use
```

### Parameter Prefix Resolution (Runtime)

```
DAO Method Execution
    ↓
Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    procedureName: "inv_inventory_Add_Item",
    parameters: { ["UserID"] = 12345 }
)
    ↓
Check override cache: _parameterPrefixOverrides["inv_inventory_Add_Item"]["UserID"]
    ↓
IF override exists:
    Use override prefix (e.g., "p_") → "p_UserID" = 12345
ELSE:
    Query INFORMATION_SCHEMA.PARAMETERS (or use convention fallback "p_")
    ↓
Build MySqlCommand with correctly prefixed parameters
    ↓
Execute stored procedure
```

### Schema Inspector Data Loading

```
User clicks Settings → Developer → Schema Inspector
    ↓
Control_Developer_SchemaInspector.LoadAsync()
    ↓
Query INFORMATION_SCHEMA.TABLES
    ↓
Populate DataGridView with Model_SchemaTable list
    ↓
User selects table
    ↓
Query INFORMATION_SCHEMA.COLUMNS for selected table
    ↓
Display Model_SchemaColumn list in details panel
```

### Procedure Call Hierarchy Loading

```
User clicks Settings → Developer → Procedure Call Hierarchy
    ↓
Control_Developer_ProcedureCallHierarchy.LoadAsync()
    ↓
Check if Database/call-hierarchy-complete.json exists
    ↓
IF NOT exists:
    Show friendly error message with "Regenerate" button
    Button click runs: Database/2-Trace-Complete-CallHierarchy-v2.ps1
ELSE:
    Parse JSON into List<Model_ProcedureDependency>
    Parse STORED_PROCEDURE_CALLSITES.csv into List<Model_ProcedureCallSite>
    ↓
User searches for procedure name (autocomplete enabled)
    ↓
Display:
    - C# call sites from Model_ProcedureCallSite
    - Called procedures from Model_ProcedureDependency
    - Dependency tree visualization
```

### Code Generator Workflow

```
User clicks Settings → Developer → Code Generator
    ↓
Control_Developer_CodeGenerator.LoadAsync()
    ↓
Query INFORMATION_SCHEMA.ROUTINES for procedure list
    ↓
Populate dropdown with stored procedure names
    ↓
User selects procedure
    ↓
Query INFORMATION_SCHEMA.PARAMETERS for selected procedure
    ↓
Build Model_GeneratedDaoMethod:
    - Parse parameters into C# Dictionary construction
    - Generate XML documentation comments
    - Generate method signature (async Task<Model_Dao_Result<DataTable>>)
    - Generate Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync call
    - Generate try-catch with Dao_ErrorLog.HandleException_GeneralError_CloseApp
    ↓
Display generated code in TextBox
    ↓
User clicks "Copy to Clipboard"
    ↓
Code copied for pasting into DAO class
```

---

## State Transitions

### Parameter Prefix Override Lifecycle

```
[Not Exists]
    ↓ (User clicks "Add Override")
[Created] (IsActive = 1, CreatedBy set, CreatedDate set)
    ↓ (User clicks "Edit Override")
[Modified] (IsActive = 1, ModifiedBy set, ModifiedDate set)
    ↓ (User clicks "Delete Override" → Soft Delete)
[Inactive] (IsActive = 0, ModifiedBy set, ModifiedDate set)
    ↓ (User clicks "Reactivate Override")
[Modified] (IsActive = 1, ModifiedBy set, ModifiedDate set)
```

**Note**: Physical DELETE is not used. Soft delete (IsActive = 0) preserves audit history.

### Debug Dashboard Capture State

```
[Stopped]
    ↓ (Form opens)
[Capturing] (Auto-start, _isCapturingDebug = true, Timer running)
    ↓ (User clicks "Pause Capture")
[Paused] (_isCapturingDebug = false, Timer stopped, output frozen)
    ↓ (User clicks "Resume Capture")
[Capturing]
    ↓ (User closes form)
[Stopped] (Disposed, output cleared)
```

---

## Data Validation Rules Summary

### Database-Level Validation (Enforced by MySQL)

-   UNIQUE constraint on (ProcedureName, ParameterName)
-   NOT NULL constraints on required fields
-   DEFAULT values for timestamps and IsActive flag

### Application-Level Validation (Enforced in DAO layer)

-   ProcedureName must not be empty or whitespace
-   ParameterName must not be empty or whitespace
-   OverridePrefix can be empty (valid for no-prefix procedures)
-   CreatedBy must be populated (current user)
-   Warn (but allow) if procedure doesn't exist in INFORMATION_SCHEMA

### UI-Level Validation (Enforced in UserControl)

-   Required field indicators in Parameter Prefix Maintenance form
-   Confirmation dialog before delete operations
-   Warning dialog when saving override for non-existent procedure
-   Character limits enforced in TextBox controls (MaxLength properties)

---

## Performance Considerations

### Caching Strategy

-   **Parameter prefix overrides**: Cached in memory at startup, refreshed only on explicit reload or application restart
-   **INFORMATION_SCHEMA queries**: No caching (manual refresh in Schema Inspector)
-   **Procedure call hierarchy**: Loaded from JSON file once at control initialization
-   **Debug dashboard output**: In-memory list with auto-truncation at 1000 lines

### Query Optimization

-   **INFORMATION_SCHEMA queries**: Use WHERE clauses to filter by TABLE_SCHEMA and TABLE_NAME
-   **Parameter prefix overrides**: Indexed on ProcedureName for O(1) lookup
-   **Procedure call hierarchy**: JSON parsing done once, not on every search

### Memory Management

-   Debug dashboard list limited to 1000 entries (oldest 100 removed when limit hit)
-   Large INFORMATION_SCHEMA result sets displayed in DataGridView (virtual scrolling)
-   Procedure call hierarchy JSON file (~500KB) loaded into memory structure
-   Generated DAO code stored in TextBox control (bounded by procedure complexity)

---

## Security Considerations

### Access Control

-   Developer tools access restricted by role check (Admin + Developer flag)
-   Parameter prefix overrides tracked by CreatedBy/ModifiedBy audit fields
-   No ability to modify database schema through Schema Inspector (read-only)
-   Generated code requires manual review before deployment

### Data Sensitivity

-   No passwords or secrets stored in parameter prefix overrides
-   INFORMATION_SCHEMA queries reveal database structure (acceptable for developers)
-   Debug dashboard output may contain business data (ephemeral only, not persisted)
-   Procedure call hierarchy reveals code organization (acceptable for developers)

---

## Migration Strategy

### Initial Deployment

1. Create `sys_parameter_prefix_overrides` table in Development database
2. Deploy 5 CRUD stored procedures
3. Grant JOHNK Developer role
4. Application loads empty override cache (no overrides yet)

### Gradual Population

1. As Phase 2.5 refactoring proceeds, developers add overrides for non-standard procedures
2. Override table acts as "technical debt tracker" - goal is empty table when standardization complete
3. Each override documents reason (legacy, vendor, temporary)

### Production Deployment

1. Deploy table and stored procedures to Production
2. Manually migrate overrides from Development if needed (or let Production discover its own needs)
3. Production overrides remain independent from Development overrides

---

## Future Enhancements (Out of Scope)

-   Import/export override sets between environments
-   Automated detection of needed overrides by analyzing INFORMATION_SCHEMA vs code
-   Override usage statistics (how often each override is used)
-   Automated cleanup of unused overrides (procedures that no longer exist)
-   Version history tracking (separate audit table for override changes)
-   Bulk override operations (add multiple overrides for one procedure at once)

---

## Appendix: Sample Queries

### Get All Active Overrides

```sql
SELECT * FROM sys_parameter_prefix_overrides
WHERE IsActive = 1
ORDER BY ProcedureName, ParameterName;
```

### Get Overrides for Specific Procedure

```sql
SELECT * FROM sys_parameter_prefix_overrides
WHERE ProcedureName = 'inv_inventory_Add_Item'
AND IsActive = 1;
```

### Find Unused Overrides (Procedures That No Longer Exist)

```sql
SELECT DISTINCT spo.ProcedureName
FROM sys_parameter_prefix_overrides spo
LEFT JOIN INFORMATION_SCHEMA.ROUTINES r
    ON spo.ProcedureName = r.ROUTINE_NAME
    AND r.ROUTINE_SCHEMA = 'MTM_WIP_Application_Winforms'
WHERE spo.IsActive = 1
AND r.ROUTINE_NAME IS NULL;
```

### Override History Audit

```sql
SELECT
    ProcedureName,
    ParameterName,
    OverridePrefix,
    Reason,
    CreatedBy,
    CreatedDate,
    ModifiedBy,
    ModifiedDate,
    CASE WHEN IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status
FROM sys_parameter_prefix_overrides
ORDER BY CreatedDate DESC;
```
