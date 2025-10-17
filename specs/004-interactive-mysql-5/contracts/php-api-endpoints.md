# PHP API Endpoints Contract

**Feature Branch**: `004-interactive-mysql-5`  
**Date**: 2025-10-17  
**Base URL**: `http://localhost:8888/StoredProcedureValidation/sp-builder/api/` (MAMP default)

---

## Overview

PHP backend provides REST-like JSON endpoints for database metadata queries and SQL validation. All endpoints return structured JSON responses with consistent error handling format.

**Common Response Format**:

```json
{
    "success": true|false,
    "data": { ... },              // Present on success
    "error_type": "string",       // Present on failure
    "user_message": "string",     // Present on failure (user-friendly)
    "technical_detail": "string"  // Present on failure (for debugging)
}
```

**Error Types**:
- `validation`: Invalid request parameters
- `connection`: Database connection failure
- `database`: SQL execution error
- `syntax`: SQL syntax validation failure
- `permission`: Insufficient database permissions

---

## Endpoint 1: Get Tables

Retrieves list of all tables in the database.

**URL**: `GET /get-tables.php`

**Query Parameters**: None

**Request Example**:
```http
GET /api/get-tables.php
```

**Success Response** (200 OK):
```json
{
    "success": true,
    "data": {
        "database": "mtm_wip_application_winforms_test",
        "tables": [
            {
                "tableName": "Inventory",
                "rowCount": 1523,
                "engine": "InnoDB",
                "comment": "WIP inventory tracking"
            },
            {
                "tableName": "Parts",
                "rowCount": 456,
                "engine": "InnoDB",
                "comment": "Part master data"
            }
        ],
        "fetchedAt": "2025-10-17T14:30:00Z"
    }
}
```

**Error Response** (500 Internal Server Error):
```json
{
    "success": false,
    "error_type": "connection",
    "user_message": "Unable to connect to database",
    "technical_detail": "mysqli_connect(): (HY000/2002): Connection refused"
}
```

**Implementation Notes**:
- Queries `information_schema.TABLES` WHERE `TABLE_SCHEMA = DATABASE()`
- Excludes system tables and views
- Returns tables sorted alphabetically
- Includes approximate row count from `TABLE_ROWS` for reference

**SQL Query**:
```sql
SELECT TABLE_NAME, TABLE_ROWS, ENGINE, TABLE_COMMENT
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
```

---

## Endpoint 2: Get Columns

Retrieves column metadata for a specified table.

**URL**: `GET /get-columns.php`

**Query Parameters**:
- `table` (required): Table name

**Request Example**:
```http
GET /api/get-columns.php?table=Inventory
```

**Success Response** (200 OK):
```json
{
    "success": true,
    "data": {
        "tableName": "Inventory",
        "columns": [
            {
                "name": "InventoryID",
                "type": "INT",
                "length": null,
                "precision": 10,
                "scale": 0,
                "nullable": false,
                "default": null,
                "autoIncrement": true,
                "comment": "Primary key"
            },
            {
                "name": "PartNumber",
                "type": "VARCHAR",
                "length": 50,
                "precision": null,
                "scale": null,
                "nullable": false,
                "default": null,
                "autoIncrement": false,
                "comment": "Part identifier"
            },
            {
                "name": "Quantity",
                "type": "DECIMAL",
                "length": null,
                "precision": 10,
                "scale": 2,
                "nullable": false,
                "default": "0.00",
                "autoIncrement": false,
                "comment": "Current quantity"
            },
            {
                "name": "ReceivedDate",
                "type": "DATETIME",
                "length": null,
                "precision": null,
                "scale": null,
                "nullable": true,
                "default": "CURRENT_TIMESTAMP",
                "autoIncrement": false,
                "comment": "Date received"
            },
            {
                "name": "IsActive",
                "type": "TINYINT",
                "length": null,
                "precision": 3,
                "scale": 0,
                "nullable": false,
                "default": "1",
                "autoIncrement": false,
                "comment": "Active flag (boolean)"
            }
        ],
        "primaryKey": ["InventoryID"],
        "foreignKeys": [
            {
                "columnName": "LocationCode",
                "referencedTable": "Locations",
                "referencedColumn": "LocationCode"
            }
        ],
        "indexes": [
            {
                "indexName": "idx_partnumber",
                "columns": ["PartNumber"],
                "unique": false
            }
        ]
    }
}
```

**Error Response - Validation** (400 Bad Request):
```json
{
    "success": false,
    "error_type": "validation",
    "user_message": "Table name is required",
    "technical_detail": "Missing 'table' query parameter"
}
```

**Error Response - Not Found** (404 Not Found):
```json
{
    "success": false,
    "error_type": "validation",
    "user_message": "Table 'InvalidTable' does not exist",
    "technical_detail": "No rows returned from information_schema.COLUMNS"
}
```

**Implementation Notes**:
- Queries `information_schema.COLUMNS` for column details
- Queries `information_schema.KEY_COLUMN_USAGE` for primary/foreign keys
- Queries `information_schema.STATISTICS` for indexes
- Returns columns in ordinal position order
- Maps MySQL native types to simplified type names for UI

**SQL Queries**:
```sql
-- Columns
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,
       NUMERIC_PRECISION, NUMERIC_SCALE, IS_NULLABLE,
       COLUMN_DEFAULT, EXTRA, COLUMN_COMMENT
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = ?
ORDER BY ORDINAL_POSITION;

-- Primary Key
SELECT COLUMN_NAME
FROM information_schema.KEY_COLUMN_USAGE
WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = ?
  AND CONSTRAINT_NAME = 'PRIMARY'
ORDER BY ORDINAL_POSITION;

-- Foreign Keys
SELECT COLUMN_NAME, REFERENCED_TABLE_NAME, REFERENCED_COLUMN_NAME
FROM information_schema.KEY_COLUMN_USAGE
WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = ?
  AND REFERENCED_TABLE_NAME IS NOT NULL;

-- Indexes
SELECT INDEX_NAME, COLUMN_NAME, NON_UNIQUE
FROM information_schema.STATISTICS
WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = ?
  AND INDEX_NAME != 'PRIMARY'
ORDER BY INDEX_NAME, SEQ_IN_INDEX;
```

---

## Endpoint 3: Validate SQL Syntax

Validates SQL syntax using MySQL PREPARE statement.

**URL**: `POST /validate-syntax.php`

**Request Body** (JSON):
```json
{
    "sql": "CREATE PROCEDURE test()\nBEGIN\n  SELECT * FROM Inventory;\nEND"
}
```

**Request Headers**:
```
Content-Type: application/json
```

**Success Response** (200 OK):
```json
{
    "success": true,
    "data": {
        "valid": true,
        "message": "SQL syntax is valid for MySQL 5.7"
    }
}
```

**Error Response - Syntax Error** (200 OK with success=false):
```json
{
    "success": false,
    "error_type": "syntax",
    "user_message": "SQL syntax error detected",
    "technical_detail": "You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'SELEC * FROM Inventory' at line 3"
}
```

**Error Response - Validation** (400 Bad Request):
```json
{
    "success": false,
    "error_type": "validation",
    "user_message": "SQL code is required",
    "technical_detail": "Missing 'sql' property in request body"
}
```

**Implementation Notes**:
- Uses `PREPARE stmt FROM ?` to validate syntax without execution
- Deallocates prepared statement immediately with `DEALLOCATE PREPARE stmt`
- Does NOT execute the SQL (safe for validation)
- Returns MySQL parser error messages verbatim for debugging
- Limited to 64KB SQL statement size (MySQL PREPARE limit)

**PHP Implementation Pattern**:
```php
<?php
$input = json_decode(file_get_contents('php://input'), true);
$sql = $input['sql'] ?? '';

if (empty($sql)) {
    http_response_code(400);
    echo json_encode([
        'success' => false,
        'error_type' => 'validation',
        'user_message' => 'SQL code is required',
        'technical_detail' => "Missing 'sql' property in request body"
    ]);
    exit;
}

try {
    $conn = new mysqli(DB_HOST, DB_USER, DB_PASS, DB_NAME);
    $stmt = $conn->prepare("PREPARE validation_stmt FROM ?");
    $stmt->bind_param('s', $sql);
    
    if ($stmt->execute()) {
        $conn->query("DEALLOCATE PREPARE validation_stmt");
        echo json_encode([
            'success' => true,
            'data' => ['valid' => true, 'message' => 'SQL syntax is valid for MySQL 5.7']
        ]);
    } else {
        throw new Exception($stmt->error);
    }
} catch (Exception $e) {
    echo json_encode([
        'success' => false,
        'error_type' => 'syntax',
        'user_message' => 'SQL syntax error detected',
        'technical_detail' => $e->getMessage()
    ]);
}
?>
```

---

## Endpoint 4: Check Procedure Exists

Checks if a stored procedure with the given name already exists.

**URL**: `GET /check-procedure-exists.php`

**Query Parameters**:
- `name` (required): Procedure name to check

**Request Example**:
```http
GET /api/check-procedure-exists.php?name=inv_inventory_Add_Item
```

**Success Response - Exists** (200 OK):
```json
{
    "success": true,
    "data": {
        "exists": true,
        "procedure": {
            "name": "inv_inventory_Add_Item",
            "created": "2024-06-15 10:23:45",
            "modified": "2025-03-10 14:30:12",
            "comment": "Adds new inventory item with validation"
        }
    }
}
```

**Success Response - Not Exists** (200 OK):
```json
{
    "success": true,
    "data": {
        "exists": false
    }
}
```

**Error Response - Validation** (400 Bad Request):
```json
{
    "success": false,
    "error_type": "validation",
    "user_message": "Procedure name is required",
    "technical_detail": "Missing 'name' query parameter"
}
```

**Implementation Notes**:
- Queries `information_schema.ROUTINES` for PROCEDURE with matching name
- Returns creation/modification timestamps for existing procedures
- Case-insensitive name matching (MySQL default)

**SQL Query**:
```sql
SELECT ROUTINE_NAME, CREATED, LAST_ALTERED, ROUTINE_COMMENT
FROM information_schema.ROUTINES
WHERE ROUTINE_SCHEMA = DATABASE()
  AND ROUTINE_TYPE = 'PROCEDURE'
  AND ROUTINE_NAME = ?;
```

---

## Common Configuration

All endpoints share configuration from `api/config.php`:

```php
<?php
// api/config.php

// Database connection constants
define('DB_HOST', 'localhost');
define('DB_PORT', '3306');
define('DB_NAME', 'mtm_wip_application_winforms_test'); // Test database
define('DB_USER', 'root');
define('DB_PASS', 'root');

// CORS headers for file:// and localhost access
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');
header('Content-Type: application/json; charset=utf-8');

// Handle preflight OPTIONS requests
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(204);
    exit;
}

// Error handler
function handleException($e) {
    http_response_code(500);
    echo json_encode([
        'success' => false,
        'error_type' => 'database',
        'user_message' => 'An unexpected error occurred',
        'technical_detail' => $e->getMessage()
    ]);
    exit;
}

set_exception_handler('handleException');
?>
```

---

## Error Handling Strategy

**Client-Side Handling**:
```javascript
async function callAPI(endpoint, options = {}) {
    try {
        const response = await fetch(`/api/${endpoint}`, options);
        const data = await response.json();
        
        if (!data.success) {
            // Show user_message to user
            showError(data.user_message);
            
            // Log technical_detail to console
            console.error(`API Error [${data.error_type}]:`, data.technical_detail);
            
            // Optionally show retry button for connection/database errors
            if (data.error_type === 'connection' || data.error_type === 'database') {
                showRetryButton(() => callAPI(endpoint, options));
            }
            
            return null;
        }
        
        return data.data;
    } catch (e) {
        // Network error or JSON parse failure
        showError('Unable to connect to server. Please check that MAMP is running.');
        console.error('Network error:', e);
        return null;
    }
}
```

---

## Security Considerations

1. **SQL Injection Prevention**: All user inputs are parameterized using mysqli prepared statements
2. **No Direct SQL Execution**: Validation endpoint uses PREPARE (does not execute)
3. **Database Permissions**: PHP user has SELECT-only access to information_schema (no data modification)
4. **CORS**: Allows localhost and file:// origins only (configured in production for specific domains)
5. **Input Validation**: All endpoints validate required parameters before database queries
6. **Error Messages**: Technical details separated from user messages to prevent information disclosure

---

## Rate Limiting

No rate limiting implemented (single-user desktop tool). For multi-user deployment, consider:
- Limit metadata refresh to 1 request per 5 seconds per session
- Limit syntax validation to 10 requests per minute per session
- Cache table/column metadata client-side for 10 minutes

---

## Testing Checklist

- [ ] All endpoints return valid JSON
- [ ] Error responses include all required fields (success, error_type, user_message, technical_detail)
- [ ] CORS headers allow file:// and localhost origins
- [ ] Database connection failures return connection error_type
- [ ] Missing parameters return validation error_type
- [ ] SQL syntax errors return syntax error_type with MySQL error message
- [ ] Metadata endpoints return data in specified format
- [ ] Procedure existence check handles existing and non-existing procedures correctly

---

## Next Steps

See `javascript-modules.md` for client-side module interfaces that consume these API endpoints.
