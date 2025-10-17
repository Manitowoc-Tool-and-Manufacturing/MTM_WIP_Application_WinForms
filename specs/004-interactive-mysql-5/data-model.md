# Data Model: Interactive MySQL 5.7 Stored Procedure Builder

**Feature Branch**: `004-interactive-mysql-5`  
**Date**: 2025-10-17  
**Status**: Phase 1 Complete

---

## Overview

This document defines the core data entities and their relationships for the Stored Procedure Builder application. All entities are implemented as JavaScript classes with JSON serialization for localStorage persistence and version control.

---

## Core Entities

### 1. ProcedureDefinition

The root entity representing a complete stored procedure being designed or edited.

**Properties**:
```javascript
{
    // Identity
    name: String,                    // Procedure name (domain_table_action pattern)
    description: String,             // Human-readable purpose
    version: Number,                 // Version number (increments on export)
    
    // Metadata
    author: String,                  // System user or creator name
    createdDate: Date,               // Initial creation timestamp
    modifiedDate: Date,              // Last modification timestamp
    
    // Components (ordered collections)
    parameters: Parameter[],         // IN/OUT/INOUT parameters
    localVariables: LocalVariable[], // DECLARE statements
    validations: ValidationRule[],   // Validation checks (pre-DML)
    operations: DMLOperation[],      // DML operations sequence
    advancedFeatures: {              // Optional advanced constructs
        loops: Loop[],
        cursors: Cursor[],
        nestedCalls: ProcedureCall[]
    },
    
    // Settings
    transactionControl: {
        enabled: Boolean,            // Wrap in START TRANSACTION / COMMIT
        isolationLevel: String       // DEFAULT | READ COMMITTED | REPEATABLE READ | SERIALIZABLE
    },
    
    // Wizard state
    currentStep: Number,             // Current wizard step (1-8)
    completedSteps: Number[],        // Steps with valid data
    
    // Flow diagram
    flowDiagram: FlowDiagram         // Visual operation sequence
}
```

**Validation Rules**:
- `name` MUST match pattern: `/^[a-z]+_[a-z]+_[A-Z][a-zA-Z_]+$/` (domain_table_action)
- `name` MUST be unique (checked against database)
- `parameters` MUST include `p_Status INT OUT` and `p_ErrorMsg VARCHAR(500) OUT`
- `operations` MUST NOT be empty for valid procedure
- `validations` execute before `operations` in generated SQL

**Methods**:
```javascript
class ProcedureDefinition {
    toJSON()                         // Serialize for localStorage
    static fromJSON(data)            // Deserialize from localStorage
    validate()                       // Returns validation errors array
    toSQL()                          // Generate SQL via SQLGenerator
    addParameter(param)              // Add parameter with validation
    removeParameter(paramName)       // Remove parameter and update references
    reorderOperations(fromIdx, toIdx) // Reorder operation sequence
}
```

---

### 2. Parameter

Represents an input or output parameter for the stored procedure.

**Properties**:
```javascript
{
    // Identity
    name: String,                    // Parameter name (with p_ prefix enforced)
    direction: String,               // IN | OUT | INOUT
    
    // Type definition
    dataType: String,                // VARCHAR | INT | DECIMAL | DATETIME | BOOLEAN | TEXT
    length: Number,                  // For VARCHAR (e.g., 50)
    precision: Number,               // For DECIMAL (e.g., 10)
    scale: Number,                   // For DECIMAL (e.g., 2)
    
    // Metadata
    description: String,             // Documentation
    defaultValue: any,               // Optional default (for IN parameters)
    
    // Usage tracking
    usedInValidations: String[],     // IDs of validations referencing this
    usedInOperations: String[],      // IDs of operations referencing this
    
    // UI state
    order: Number                    // Display order in wizard
}
```

**Validation Rules**:
- `name` MUST start with `p_` (auto-added if omitted)
- `name` MUST be unique within procedure
- `direction` MUST be one of IN, OUT, INOUT
- `dataType` MUST be valid MySQL 5.7 type
- `length` required when `dataType === 'VARCHAR'`
- `precision` and `scale` required when `dataType === 'DECIMAL'`

**Auto-Included Parameters** (cannot be removed):
```javascript
{ name: 'p_Status', direction: 'OUT', dataType: 'INT' }
{ name: 'p_ErrorMsg', direction: 'OUT', dataType: 'VARCHAR', length: 500 }
```

**Methods**:
```javascript
class Parameter {
    getTypeDeclaration()             // Returns "VARCHAR(50)" or "DECIMAL(10,2)"
    isRequired()                     // True if IN parameter with no default
    validate()                       // Returns validation errors
    findReferences(procedure)        // Finds all usages in validations/operations
}
```

---

### 3. ValidationRule

Represents a data validation check executed before DML operations.

**Properties**:
```javascript
{
    // Identity
    id: String,                      // Unique ID (UUID)
    type: String,                    // See ValidationRuleType enum below
    
    // Configuration (varies by type)
    targetParameter: String,         // Parameter name being validated
    targetExpression: String,        // Custom SQL expression (for Custom type)
    
    // Conditions (type-specific)
    minValue: Number,                // For Positive Number, Numeric Range
    maxValue: Number,                // For Numeric Range, String Length
    minLength: Number,               // For String Length
    maxLength: Number,               // For String Length
    referenceTable: String,          // For Foreign Key Check
    referenceColumn: String,         // For Foreign Key Check
    dateFrom: String,                // For Date Range (parameter name or literal)
    dateTo: String,                  // For Date Range (parameter name or literal)
    
    // Error handling
    errorCode: Number,               // Status code on failure (-1 to -99)
    errorMessage: String,            // User-facing error message
    
    // Execution
    order: Number                    // Execution order (validated in sequence)
}
```

**ValidationRuleType Enum**:
```javascript
const ValidationRuleType = {
    REQUIRED_FIELD: 'Required Field',      // Check parameter IS NOT NULL AND parameter != ''
    POSITIVE_NUMBER: 'Positive Number',    // Check parameter > 0
    NUMERIC_RANGE: 'Numeric Range',        // Check parameter BETWEEN minValue AND maxValue
    STRING_LENGTH: 'String Length',        // Check LENGTH(parameter) BETWEEN minLength AND maxLength
    DATE_RANGE: 'Date Range',              // Check parameter BETWEEN dateFrom AND dateTo
    FOREIGN_KEY: 'Foreign Key Check',      // Check EXISTS in referenceTable.referenceColumn
    CUSTOM: 'Custom SQL'                   // User-defined SQL expression
};
```

**SQL Generation Pattern**:
```sql
-- Required Field example
IF p_PartNumber IS NULL OR p_PartNumber = '' THEN
    ROLLBACK;
    SET p_Status = -1;
    SET p_ErrorMsg = 'Part Number is required';
END IF;

-- Foreign Key Check example
SELECT COUNT(*) INTO v_Exists 
FROM Locations 
WHERE LocationCode = p_LocationCode;

IF v_Exists = 0 THEN
    ROLLBACK;
    SET p_Status = -2;
    SET p_ErrorMsg = 'Invalid location code';
END IF;
```

**Methods**:
```javascript
class ValidationRule {
    toSQL()                          // Generate SQL IF block
    validate()                       // Validate rule configuration
    getDependencies()                // Returns required local variables
}
```

---

### 4. DMLOperation

Represents a single database operation (INSERT, UPDATE, DELETE, SELECT).

**Properties**:
```javascript
{
    // Identity
    id: String,                      // Unique ID (UUID)
    type: String,                    // INSERT | UPDATE | DELETE | SELECT
    
    // Target
    targetTable: String,             // Table name from database metadata
    
    // Column mappings (for INSERT/UPDATE)
    columnMappings: ColumnMapping[], // Array of column-to-value assignments
    
    // WHERE clause (for UPDATE/DELETE/SELECT)
    whereConditions: WhereCondition[], // Array of conditions (AND logic)
    
    // SELECT-specific
    selectColumns: String[],         // Column names (or ['*'] for all)
    joins: Join[],                   // JOIN clauses
    orderBy: OrderByClause[],        // ORDER BY clauses
    groupBy: String[],               // GROUP BY columns
    limit: Number,                   // LIMIT value
    outputVariable: String,          // Variable name for SELECT INTO
    
    // UPDATE-specific
    trackRowCount: Boolean,          // Add ROW_COUNT() INTO v_RowsAffected
    
    // INSERT-specific
    onDuplicateKeyUpdate: {
        enabled: Boolean,
        updateMappings: ColumnMapping[]
    },
    
    // Conditional execution
    conditionalBranch: {
        condition: String,           // SQL expression (e.g., "v_RowsAffected = 0")
        trueOperation: String,       // Operation ID to execute if true
        falseOperation: String       // Operation ID to execute if false
    },
    
    // Execution
    order: Number                    // Execution order in sequence
}
```

**Supporting Types**:

```javascript
// ColumnMapping
{
    columnName: String,              // Target column (from database metadata)
    value: String,                   // Expression: parameter name, literal, or function (NOW(), etc.)
    isExpression: Boolean            // True if value is SQL expression (e.g., "Quantity + p_Delta")
}

// WhereCondition
{
    columnName: String,              // Column name
    operator: String,                // = | != | > | < | >= | <= | LIKE | IN | IS NULL | IS NOT NULL
    value: String,                   // Parameter name, literal, or expression
    logicalOperator: String          // AND | OR (for next condition)
}

// Join
{
    type: String,                    // INNER JOIN | LEFT JOIN | RIGHT JOIN
    targetTable: String,             // Table to join
    onCondition: String              // JOIN condition (e.g., "Inventory.PartID = Parts.PartID")
}

// OrderByClause
{
    columnName: String,              // Column to sort by
    direction: String                // ASC | DESC
}
```

**SQL Generation Examples**:

```sql
-- INSERT
INSERT INTO Inventory (PartNumber, Quantity, LocationCode, ReceivedDate, ReceivedUser, IsActive)
VALUES (p_PartNumber, p_Quantity, p_LocationCode, NOW(), p_UserID, 1);

-- UPDATE with ROW_COUNT tracking
UPDATE Inventory
SET Quantity = Quantity + p_QuantityDelta,
    LastUpdated = NOW(),
    LastUpdatedUser = p_UserID
WHERE InventoryID = p_InventoryID;

SELECT ROW_COUNT() INTO v_RowsAffected;

-- DELETE with WHERE clause
DELETE FROM Inventory
WHERE InventoryID = p_InventoryID
  AND IsActive = 0;

-- SELECT with JOIN
SELECT i.PartNumber, i.Quantity, l.LocationName
FROM Inventory i
INNER JOIN Locations l ON i.LocationCode = l.LocationCode
WHERE i.IsActive = 1
ORDER BY i.PartNumber ASC;
```

**Methods**:
```javascript
class DMLOperation {
    toSQL()                          // Generate SQL statement
    validate()                       // Validate configuration
    getDependencies()                // Returns required local variables
    requiresWhereClause()            // True for UPDATE/DELETE (safety check)
}
```

---

### 5. LocalVariable

Represents a DECLARE statement for procedure-local variable.

**Properties**:
```javascript
{
    name: String,                    // Variable name (with v_ prefix enforced)
    dataType: String,                // VARCHAR | INT | DECIMAL | DATETIME | BOOLEAN
    length: Number,                  // For VARCHAR
    precision: Number,               // For DECIMAL
    scale: Number,                   // For DECIMAL
    defaultValue: any,               // Optional DEFAULT value
    usage: String                    // Description of what variable stores
}
```

**Auto-Included Variables** (when needed):
```javascript
// Added when any operation has trackRowCount = true
{ name: 'v_RowsAffected', dataType: 'INT' }

// Added when any validation or operation needs existence check
{ name: 'v_Exists', dataType: 'INT' }

// Added when using cursors
{ name: 'v_Done', dataType: 'BOOLEAN', defaultValue: false }
```

**Validation Rules**:
- `name` MUST start with `v_` (auto-added if omitted)
- `name` MUST be unique within procedure

---

### 6. FlowDiagram

Represents the visual flow diagram of operations and control flow.

**Properties**:
```javascript
{
    nodes: FlowNode[],               // Visual nodes representing operations
    connections: FlowConnection[],   // Arrows showing execution flow
    layout: {
        algorithm: String,           // 'dagre' | 'manual'
        direction: String            // 'TB' (top-bottom) | 'LR' (left-right)
    }
}
```

**Supporting Types**:

```javascript
// FlowNode
{
    id: String,                      // Matches Operation.id or Validation.id
    type: String,                    // 'operation' | 'validation' | 'condition' | 'start' | 'end'
    label: String,                   // Display text
    x: Number,                       // Canvas X coordinate (from Dagre layout)
    y: Number,                       // Canvas Y coordinate
    width: Number,                   // Node width (150px default)
    height: Number,                  // Node height (80px default)
    style: {
        backgroundColor: String,     // Node color based on type
        borderColor: String
    }
}

// FlowConnection
{
    from: String,                    // Source node ID
    to: String,                      // Target node ID
    label: String,                   // Condition label (for branches)
    type: String                     // 'sequential' | 'conditional-true' | 'conditional-false'
}
```

**Methods**:
```javascript
class FlowDiagram {
    addNode(operation)               // Add node for operation
    removeNode(nodeId)               // Remove node and connections
    connect(fromId, toId, type)      // Create connection
    autoLayout()                     // Run Dagre layout algorithm
    toJSON()                         // Serialize for storage
}
```

---

### 7. Template

Represents a reusable procedure pattern (built-in or user-created).

**Properties**:
```javascript
{
    // Identity
    id: String,                      // Unique ID
    name: String,                    // Template name
    category: String,                // 'crud' | 'batch' | 'transfer' | 'audit' | 'custom'
    description: String,             // What the template does
    
    // Authorship
    author: String,                  // 'system' or user name
    createdDate: Date,
    
    // Template definition
    procedureTemplate: {
        namePattern: String,         // Pattern with {placeholders}
        parameters: Parameter[],
        validations: ValidationRule[],
        operations: DMLOperation[],
        advancedFeatures: Object
    },
    
    // Customization
    customizationPoints: String[],   // Field paths that MUST be customized
    substitutionRules: {             // Table/column name replacements
        tables: Map<String, String>, // Placeholder -> actual table
        columns: Map<String, String> // Placeholder -> actual column
    },
    
    // Usage tracking
    usageCount: Number,              // How many times used
    lastUsed: Date
}
```

**Built-in Template Example** (CRUD):
```javascript
{
    id: 'crud-single-table',
    name: 'CRUD Operations',
    category: 'crud',
    description: 'Creates four procedures for Create, Read, Update, Delete on a single table',
    author: 'system',
    procedureTemplate: {
        namePattern: '{domain}_{table}_{action}',
        customizationPoints: ['domain', 'table', 'primaryKey'],
        // ... full procedure definition with {placeholders}
    }
}
```

**Methods**:
```javascript
class Template {
    apply(substitutions)             // Generate ProcedureDefinition from template
    validate(database)               // Check if referenced tables exist
    suggestSubstitutions(database)   // Fuzzy-match tables for missing references
    toJSON()                         // Serialize for file storage
    static fromJSON(data)            // Load template from file
}
```

---

### 8. DatabaseMetadata

Represents cached database schema information fetched from MySQL information_schema.

**Properties**:
```javascript
{
    database: String,                // Database name (mtm_wip_application_winforms_test)
    tables: TableMetadata[],         // All tables in database
    fetchedAt: Date,                 // Timestamp of last metadata refresh
    stale: Boolean                   // True if >10 minutes old
}
```

**Supporting Types**:

```javascript
// TableMetadata
{
    tableName: String,
    columns: ColumnMetadata[],
    primaryKey: String[],            // Column names
    foreignKeys: ForeignKey[],
    indexes: Index[]
}

// ColumnMetadata
{
    columnName: String,
    dataType: String,                // MySQL native type
    length: Number,                  // CHARACTER_MAXIMUM_LENGTH
    precision: Number,               // NUMERIC_PRECISION
    scale: Number,                   // NUMERIC_SCALE
    nullable: Boolean,               // IS_NULLABLE
    defaultValue: String,            // COLUMN_DEFAULT
    autoIncrement: Boolean,          // EXTRA contains 'auto_increment'
    comment: String                  // COLUMN_COMMENT
}

// ForeignKey
{
    columnName: String,
    referencedTable: String,
    referencedColumn: String
}

// Index
{
    indexName: String,
    columns: String[],
    unique: Boolean
}
```

**Methods**:
```javascript
class DatabaseMetadata {
    getTable(tableName)              // Find table by name
    getColumns(tableName)            // Get column list for table
    isStale()                        // Check if >10 minutes old
    refresh()                        // Fetch fresh metadata from database
}
```

---

### 9. ExportConfiguration

Settings for SQL file generation.

**Properties**:
```javascript
{
    // File naming
    fileNamePattern: String,         // '{procedureName}.sql' or custom
    includeTimestamp: Boolean,       // Append timestamp to filename
    
    // SQL formatting
    includeDelimiter: Boolean,       // Wrap in DELIMITER $$ / DELIMITER ;
    includeDropStatement: Boolean,   // Include DROP PROCEDURE IF EXISTS
    includeHeaderComment: Boolean,   // Include documentation header
    indentSize: Number,              // Spaces for indentation (2 or 4)
    
    // Header comment template
    headerTemplate: String,          // Template with {placeholders}
    
    // Version control
    versionControlEnabled: Boolean,  // Save version to localStorage
    autoIncrementVersion: Boolean,   // Increment version on each export
    
    // Post-export actions
    promptAnalysisScript: Boolean,   // Show RUN-COMPLETE-ANALYSIS.ps1 prompt
    openInEditor: Boolean            // Attempt to open .sql in external editor
}
```

**Default Header Template**:
```sql
/*
 * Procedure: {procedureName}
 * Description: {description}
 * 
 * Parameters:
{parameters}
 * 
 * Tables Accessed: {tables}
 * 
 * Author: {author}
 * Created: {createdDate}
 * Modified: {modifiedDate}
 * Version: {version}
 */
```

---

## Entity Relationships

```
ProcedureDefinition (1) ──> (0..*) Parameter
ProcedureDefinition (1) ──> (0..*) ValidationRule
ProcedureDefinition (1) ──> (0..*) DMLOperation
ProcedureDefinition (1) ──> (0..*) LocalVariable
ProcedureDefinition (1) ──> (1) FlowDiagram
ProcedureDefinition (1) ──> (0..1) Template (when loaded from template)

DMLOperation (1) ──> (0..*) ColumnMapping
DMLOperation (1) ──> (0..*) WhereCondition
DMLOperation (1) ──> (0..*) Join

FlowDiagram (1) ──> (0..*) FlowNode
FlowDiagram (1) ──> (0..*) FlowConnection

Template (1) ──> (1) ProcedureDefinition (template definition)

DatabaseMetadata (1) ──> (0..*) TableMetadata
TableMetadata (1) ──> (0..*) ColumnMetadata
```

---

## Serialization and Storage

All entities implement `toJSON()` and `static fromJSON(data)` methods for:

1. **localStorage persistence** (wizard auto-save)
2. **Template file export/import**
3. **Version history tracking**

**localStorage Keys**:
- `sp_builder_state`: Current wizard state (ProcedureDefinition)
- `sp_builder_autosave`: Auto-save timestamp + data
- `sp_versions_{procedureName}`: Version history array
- `sp_database_metadata`: Cached database metadata
- `sp_custom_templates`: User-created templates array
- `sp_export_config`: Export configuration settings

**Size Management**:
- Monitor total localStorage usage
- Prune old versions when approaching 5MB limit
- Compress JSON with whitespace removal

---

## Validation Rules Summary

### Cross-Entity Validation

1. **Parameter references**: All parameter names in Validations and Operations MUST exist in Parameters array
2. **Table references**: All table names in Operations MUST exist in DatabaseMetadata
3. **Column references**: All column names in ColumnMappings MUST exist in TableMetadata for target table
4. **Operation ordering**: Operations with dependencies (e.g., SELECT INTO before UPDATE using variable) MUST be ordered correctly
5. **Mandatory parameters**: p_Status and p_ErrorMsg MUST always be included

### State Transitions

Wizard steps are sequential but allow backward navigation:

1. Procedure Name → Parameters
2. Parameters → Validation Rules
3. Validation Rules → DML Operations
4. DML Operations → Flow Diagram (auto-generated)
5. Flow Diagram → Advanced Features (optional)
6. Advanced Features → Preview (validates all)
7. Preview → Export (if no errors)

---

## Next Steps

With data model defined, Phase 1 continues with:
- **contracts/php-api-endpoints.md**: API specifications
- **contracts/javascript-modules.md**: Module interfaces
- **quickstart.md**: Developer setup guide
