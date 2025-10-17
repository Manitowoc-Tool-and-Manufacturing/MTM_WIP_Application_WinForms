# Quick Start Guide: Interactive MySQL 5.7 Stored Procedure Builder

**Feature Branch**: `004-interactive-mysql-5`  
**Date**: 2025-10-17

---

## Prerequisites

### Required Software

- **MAMP** (Windows/Mac) with:
  - MySQL 5.7.24+ running on port 3306
  - PHP 7.4+ with mysqli extension enabled
  - Apache web server running (for PHP endpoints)
  
- **Chrome Browser 86+** (primary)
  - File System Access API support
  - ES6 module support
  - localStorage enabled

- **Git** (for cloning repository and tracking custom templates)

### Database Setup

1. Start MAMP and verify MySQL is running:
   ```bash
   # Check MySQL status
   ps aux | grep mysqld
   ```

2. Verify test database exists:
   ```sql
   mysql -u root -p
   > SHOW DATABASES LIKE 'mtm_wip_application_winforms_test';
   > USE mtm_wip_application_winforms_test;
   > SHOW TABLES;
   ```

3. If database doesn't exist, create it:
   ```sql
   CREATE DATABASE mtm_wip_application_winforms_test;
   ```

---

## Installation

### 1. Navigate to Project Directory

```bash
cd c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms
git checkout 004-interactive-mysql-5
```

### 2. Verify File Structure

```bash
StoredProcedureValidation/
└── sp-builder/
    ├── index.html
    ├── wizard.html
    ├── api/
    │   ├── config.php
    │   ├── get-tables.php
    │   ├── get-columns.php
    │   ├── validate-syntax.php
    │   └── check-procedure-exists.php
    ├── js/
    │   ├── app.js
    │   ├── wizard-controller.js
    │   └── ... (other modules)
    ├── css/
    │   └── ... (stylesheets)
    └── templates/
        └── ... (template JSON files)
```

### 3. Configure PHP API

Edit `StoredProcedureValidation/sp-builder/api/config.php`:

```php
<?php
// Database connection
define('DB_HOST', 'localhost');
define('DB_PORT', '3306');
define('DB_NAME', 'mtm_wip_application_winforms_test');
define('DB_USER', 'root');
define('DB_PASS', 'root');
?>
```

### 4. Test API Endpoints

Open browser and navigate to:
```
http://localhost:8888/StoredProcedureValidation/sp-builder/api/get-tables.php
```

Expected response:
```json
{
    "success": true,
    "data": {
        "database": "mtm_wip_application_winforms_test",
        "tables": [ ... ]
    }
}
```

If you see an error, verify:
- MAMP is running
- Port 8888 is correct (check MAMP preferences)
- MySQL user/password are correct

---

## First-Time User Guide

### Step 1: Launch the Builder

1. Open Chrome browser
2. Navigate to: `http://localhost:8888/StoredProcedureValidation/sp-builder/index.html`
3. You should see the welcome screen with "Start New Procedure" and "Load Template" buttons

### Step 2: Create Your First Procedure (Simple INSERT)

#### 2.1 Name the Procedure

- Click "Start New Procedure"
- Enter name: `test_parts_Add_Part`
  - Pattern: `domain_table_action` (lowercase_lowercase_PascalCase)
- Enter description: "Adds a new part to the Parts table"
- Click "Next Step"

#### 2.2 Add Parameters

The wizard auto-includes `p_Status` and `p_ErrorMsg`. Add:

1. Click "Add Parameter"
   - Name: `PartNumber`
   - Direction: `IN`
   - Data Type: `VARCHAR`
   - Length: `50`
   - Description: "Unique part identifier"
   - Click "Save"

2. Click "Add Parameter"
   - Name: `Description`
   - Direction: `IN`
   - Data Type: `VARCHAR`
   - Length: `200`
   - Description: "Part description"
   - Click "Save"

3. Click "Next Step"

#### 2.3 Add Validation Rules

1. Drag "Required Field" card to validation area
   - Parameter: `p_PartNumber`
   - Error Message: "Part Number is required"
   - Error Code: `-1`
   - Click "Save"

2. Drag "String Length" card to validation area
   - Parameter: `p_PartNumber`
   - Min Length: `3`
   - Max Length: `50`
   - Error Message: "Part Number must be 3-50 characters"
   - Error Code: `-2`
   - Click "Save"

3. Click "Next Step"

#### 2.4 Add DML Operations

1. Click "Add INSERT Operation"
   - Target Table: Select "Parts" from dropdown
   - Columns to insert:
     - ✅ PartNumber → Value: `p_PartNumber`
     - ✅ Description → Value: `p_Description`
     - ✅ CreatedDate → Value: `NOW()` (smart default)
     - ✅ CreatedUser → Value: `p_UserID` (if you add this parameter)
     - ✅ IsActive → Value: `1` (smart default)
   - Click "Save Operation"

2. Click "Next Step"

#### 2.5 View Flow Diagram

- Flow diagram automatically shows:
  - START → Validation 1 → Validation 2 → INSERT Parts → END
- Drag nodes to rearrange (optional)
- Click "Next Step"

#### 2.6 Preview SQL

View generated SQL:

```sql
DELIMITER $$

DROP PROCEDURE IF EXISTS test_parts_Add_Part$$

CREATE PROCEDURE test_parts_Add_Part(
    IN p_PartNumber VARCHAR(50),
    IN p_Description VARCHAR(200),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_Status = -99;
        SET p_ErrorMsg = 'Database error occurred';
    END;
    
    START TRANSACTION;
    
    -- Validation: Required Field
    IF p_PartNumber IS NULL OR p_PartNumber = '' THEN
        ROLLBACK;
        SET p_Status = -1;
        SET p_ErrorMsg = 'Part Number is required';
    -- Validation: String Length
    ELSEIF LENGTH(p_PartNumber) < 3 OR LENGTH(p_PartNumber) > 50 THEN
        ROLLBACK;
        SET p_Status = -2;
        SET p_ErrorMsg = 'Part Number must be 3-50 characters';
    ELSE
        -- DML: INSERT Parts
        INSERT INTO Parts (PartNumber, Description, CreatedDate, IsActive)
        VALUES (p_PartNumber, p_Description, NOW(), 1);
        
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
        COMMIT;
    END IF;
END$$

DELIMITER ;
```

- Syntax highlighting shows keywords, strings, comments
- Error indicators (if any) highlighted in red
- Click "Validate Syntax" to run server-side check

#### 2.7 Export

1. Click "Export SQL"
2. Choose file location (Chrome File System Access API)
3. File saved as: `test_parts_Add_Part.sql`
4. Prompt appears: "Run RUN-COMPLETE-ANALYSIS.ps1 to update procedure analysis?"

### Step 3: Test the Exported Procedure

1. Open MySQL Workbench or command line:
   ```sql
   mysql -u root -p mtm_wip_application_winforms_test
   ```

2. Load the exported file:
   ```sql
   source C:/path/to/test_parts_Add_Part.sql
   ```

3. Test the procedure:
   ```sql
   CALL test_parts_Add_Part('TEST-001', 'Test Part Description', @status, @errMsg);
   SELECT @status, @errMsg;
   ```

4. Verify results:
   - `@status = 0` (success)
   - `@errMsg = NULL`
   - `SELECT * FROM Parts WHERE PartNumber = 'TEST-001';` shows new row

---

## Common Workflows

### Editing an Existing Procedure

1. Open procedure-review-tool.html
2. Find procedure in list
3. Click "Edit in Builder"
4. Builder pre-populates all wizard steps from SQL file
5. Make changes
6. Export (version increments automatically)

### Using Templates

1. Click "Load Template" on welcome screen
2. Browse templates by category: CRUD, Batch, Transfer, Audit
3. Select "CRUD Operations" template
4. Enter customization values:
   - Domain: `test`
   - Table: `Parts`
   - Primary Key: `PartID`
5. Builder generates 4 procedures: Create, Read, Update, Delete
6. Review and export each

### Saving Custom Templates

1. Complete a procedure definition in wizard
2. On Preview step, click "Save as Template"
3. Enter template name and description
4. Template saved to `templates/custom-templates.json`
5. Available for reuse in future procedures

---

## Keyboard Shortcuts

### Wizard Navigation
- `Ctrl+→`: Next step
- `Ctrl+←`: Previous step
- `Ctrl+S`: Save current state
- `Ctrl+P`: Preview SQL
- `Ctrl+E`: Export SQL

### Drag-Drop Reordering
- `Ctrl+↑`: Move selected item up
- `Ctrl+↓`: Move selected item down
- `Enter`: Edit selected item
- `Delete`: Remove selected item

### Flow Diagram
- `Mouse Wheel`: Zoom in/out
- `Mouse Drag`: Pan
- `Click Node`: Highlight operation in wizard
- `Ctrl+0`: Reset zoom to 100%

---

## Troubleshooting

### "Database connection failed"

**Cause**: MAMP MySQL not running or wrong credentials

**Solution**:
1. Check MAMP status (green lights for Apache and MySQL)
2. Verify MySQL port: MAMP Preferences → Ports (default 3306)
3. Test connection manually:
   ```bash
   mysql -u root -p -h localhost -P 3306
   ```
4. Update `api/config.php` if credentials differ

### "Table dropdown is empty"

**Cause**: Test database has no tables or wrong database selected

**Solution**:
1. Verify database name in `api/config.php`
2. Check tables exist:
   ```sql
   USE mtm_wip_application_winforms_test;
   SHOW TABLES;
   ```
3. Click "Refresh Metadata" button in wizard

### "File System Access API not supported"

**Cause**: Using browser other than Chrome 86+

**Solution**:
- Switch to Chrome 86 or higher, OR
- Use fallback file download (automatic - files go to Downloads folder)

### "localStorage quota exceeded"

**Cause**: Too many procedure versions stored

**Solution**:
1. Click "Settings" → "Clear Version History"
2. Export important procedures before clearing
3. Reduce version history depth: Settings → "Max Versions" = 3

### "SQL syntax error on export"

**Cause**: Invalid configuration or MySQL 5.7 incompatibility

**Solution**:
1. Click "Preview" step
2. Click "Validate Syntax" button
3. Review error messages with line numbers
4. Common issues:
   - Missing WHERE clause on UPDATE/DELETE (warning, can proceed)
   - Using MySQL 8.0 syntax (CTE, window functions) - not supported
   - Parameter name typos
   - Undefined variables

---

## Performance Tips

1. **Metadata Caching**: Metadata refreshes every 10 minutes. Click "Refresh" only when schema changes.

2. **Flow Diagram**: Limit procedures to 25 operations for optimal rendering. Complex procedures may require zoom/pan.

3. **Auto-Save**: Runs every 30 seconds. Disable in Settings if working with sensitive procedures.

4. **localStorage**: Monitor usage in Settings. Clear old versions when approaching 5MB limit.

5. **Browser Tab**: Keep only one builder tab open to avoid localStorage conflicts.

---

## Advanced Features

### Cursors and Loops

1. Navigate to "Advanced Features" step (after DML Operations)
2. Click "Add Cursor"
3. Configure:
   - Cursor name: `cur_Parts`
   - Query: `SELECT PartID, PartNumber FROM Parts WHERE IsActive = 1`
4. Add operations inside cursor loop
5. Generated SQL includes DECLARE CURSOR, FETCH loop, CLOSE CURSOR

### Conditional Branches (IF/ELSE)

1. In DML Operations, click operation
2. Enable "Conditional Branch"
3. Configure:
   - Condition: `v_RowsAffected = 0`
   - True branch: Select operation to execute if true
   - False branch: Select operation to execute if false
4. Flow diagram shows branching paths

### Nested Stored Procedure Calls

1. In Advanced Features, click "Add CALL Statement"
2. Configure:
   - Procedure name: `sp_LogTransaction`
   - Parameters: `p_PartID, @status, @errMsg`
3. Handle output: Check `@status` and branch on error

---

## Next Steps

1. **Create more procedures**: Practice with UPDATE, DELETE, SELECT operations
2. **Explore templates**: See how built-in templates work and customize them
3. **Build custom templates**: Save your team's common patterns
4. **Integrate with workflow**: Use alongside procedure-review-tool.html for full cycle

---

## Getting Help

- **Help Sidebar**: Click "?" icon in wizard for context-sensitive help
- **Tutorial**: First-time user tutorial activates on first launch (can restart in Settings)
- **Examples**: Sample procedures in `templates/examples/`
- **Documentation**: See `/specs/004-interactive-mysql-5/` for full technical docs

---

## Feedback and Issues

Report issues or suggestions to the development team. Include:
- Browser version
- Steps to reproduce
- Screenshots of error messages
- Exported SQL file (if applicable)
