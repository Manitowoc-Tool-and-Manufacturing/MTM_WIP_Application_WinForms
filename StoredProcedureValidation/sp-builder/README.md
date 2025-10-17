# MySQL 5.7 Stored Procedure Builder

**Version**: 1.0.0 (Phase 1-2 Complete)  
**Status**: Ready for Testing  
**Branch**: `004-interactive-mysql-5`

---

## Quick Start

### Prerequisites

- MAMP with MySQL 5.7.24+ running on port 3306
- PHP 7.4+ with mysqli extension
- Chrome Browser 86+ (recommended)
- Database: `mtm_wip_application_winforms_test`

### Installation

1. **Start MAMP** and ensure MySQL and Apache are running

2. **Navigate to application**:
   ```
   http://localhost:8888/StoredProcedureValidation/sp-builder/index.html
   ```

3. **Click "Start New Procedure"** to launch the wizard

---

## Features (Current Phase)

✅ **Step 1: Procedure Name**
- Real-time validation of `domain_table_action` pattern
- Required description field
- Auto-save every 30 seconds

✅ **Step 2: Parameters**
- Add/remove parameters with visual UI
- Automatic p_ prefix enforcement
- Mandatory p_Status and p_ErrorMsg (cannot remove)
- Data type handling (VARCHAR, INT, DECIMAL, etc.)

✅ **Step 7: SQL Preview**
- Live MySQL 5.7 SQL generation
- Syntax highlighting with Prism.js
- DELIMITER management
- Error handler blocks

✅ **Auto-Save & Session Restoration**
- Saves every 30 seconds to localStorage
- Resume incomplete sessions from homepage
- Version history (up to 5 versions per procedure)

✅ **PHP API Endpoints**
- `get-tables.php` - Fetch table list
- `get-columns.php` - Fetch columns for table
- `validate-syntax.php` - Server-side validation
- `check-procedure-exists.php` - Check name conflicts

---

## Usage Example

### 1. Create New Procedure

**Step 1: Enter Name**
```
Name: inv_inventory_Add_Item
Description: Adds a new inventory item to the Parts table
Author: John Developer
```

**Step 2: Add Parameters**
```
Parameter 1:
- Name: PartNumber (becomes p_PartNumber)
- Direction: IN
- Type: VARCHAR(50)
- Description: Unique part identifier

Parameter 2:
- Name: Quantity
- Direction: IN
- Type: DECIMAL(10,2)
- Description: Initial quantity
```

**Step 7: Preview Generated SQL**
```sql
DELIMITER $$

DROP PROCEDURE IF EXISTS inv_inventory_Add_Item$$

CREATE PROCEDURE inv_inventory_Add_Item(
    IN p_PartNumber VARCHAR(50),
    IN p_Quantity DECIMAL(10,2),
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
    
    -- TODO: Add validation rules here
    -- TODO: Add DML operations here
    
    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
    COMMIT;
END$$

DELIMITER ;
```

---

## Keyboard Shortcuts

- **Ctrl + →**: Next step
- **Ctrl + ←**: Previous step
- **Ctrl + S**: Save progress
- **F1**: Open help (coming soon)

---

## File Structure

```
sp-builder/
├── index.html              # Homepage
├── wizard.html             # 8-step wizard
├── css/
│   ├── main.css           # Layout & theme
│   └── components.css     # UI components
├── js/
│   ├── app.js             # Homepage controller
│   ├── procedure-model.js # Data models
│   ├── wizard-controller.js # Wizard logic
│   ├── storage-manager.js # Persistence
│   └── sql-generator.js   # SQL generation
├── api/
│   ├── config.php         # Database config
│   ├── get-tables.php
│   ├── get-columns.php
│   ├── validate-syntax.php
│   └── check-procedure-exists.php
└── lib/                   # Third-party libraries
```

---

## Troubleshooting

### "Database connection failed"
- **Cause**: MAMP not running or wrong credentials
- **Fix**: Start MAMP, verify MySQL is running on port 3306
- **Test**: `mysql -u root -p -h localhost -P 3306`

### "Table dropdown is empty"
- **Cause**: Database name mismatch or no tables
- **Fix**: Verify `api/config.php` has correct DB_NAME
- **Test**: Open `http://localhost:8888/.../api/get-tables.php` in browser

### "localStorage quota exceeded"
- **Cause**: Too many versions stored
- **Fix**: Click "Settings" → "Clear Version History"
- **Prevention**: Reduce MAX_VERSIONS in storage-manager.js

### Wizard won't advance to next step
- **Cause**: Validation errors on current step
- **Fix**: Check for red error messages under form fields
- **Step 1**: Ensure name follows `domain_table_action` pattern
- **Step 1**: Description must be at least 10 characters

---

## API Endpoints

### GET /api/get-tables.php

Returns list of tables from database.

**Response:**
```json
{
  "success": true,
  "data": {
    "database": "mtm_wip_application_winforms_test",
    "tables": [
      {
        "name": "Parts",
        "type": "BASE TABLE",
        "comment": "",
        "created": "2025-01-01 12:00:00",
        "updated": "2025-01-15 08:30:00"
      }
    ],
    "count": 1
  }
}
```

### GET /api/get-columns.php?table=Parts

Returns columns for specified table.

**Response:**
```json
{
  "success": true,
  "data": {
    "table": "Parts",
    "columns": [
      {
        "name": "PartID",
        "type": "int",
        "columnType": "int(11)",
        "nullable": false,
        "isPrimaryKey": true,
        "autoIncrement": true
      },
      {
        "name": "PartNumber",
        "type": "varchar",
        "columnType": "varchar(50)",
        "length": 50,
        "nullable": false
      }
    ]
  }
}
```

### POST /api/validate-syntax.php

Validates SQL syntax.

**Request:** `sql=<SQL code>`

**Response:**
```json
{
  "success": true,
  "data": {
    "valid": true,
    "message": "SQL syntax is valid"
  }
}
```

---

## Known Limitations (Phase 1-2)

- **Steps 3-6**: Placeholder content only
  - Validation rules → Phase 6
  - DML operations → Phase 4
  - Flow diagram → Phase 7
  - Advanced features → Phase 10

- **Export**: Not yet functional → Phase 5
- **Templates**: No built-in templates yet → Phase 3
- **SQL Parser**: Import only captures name → Phase 9

---

## Development Notes

### Adding New Data Types

Edit `procedure-model.js`:
```javascript
export const DATA_TYPES = [
    'VARCHAR',
    'INT',
    // Add new type here
    'JSON'  // MySQL 5.7 has limited JSON support
];
```

### Customizing Auto-Save Interval

Edit `storage-manager.js`:
```javascript
this.AUTO_SAVE_INTERVAL = 60000; // Change to 60 seconds
```

### Adjusting Version History Limit

Edit `storage-manager.js`:
```javascript
this.MAX_VERSIONS = 10; // Keep 10 versions instead of 5
```

---

## Testing Checklist

- [ ] Homepage loads without errors
- [ ] Wizard Step 1: Name validation works
- [ ] Wizard Step 2: Parameters add/remove correctly
- [ ] Step 7: SQL preview shows valid syntax
- [ ] Auto-save triggers after 30 seconds
- [ ] Session restore prompt appears after page refresh
- [ ] PHP endpoints return valid JSON
- [ ] No console errors in DevTools

---

## Next Phases

**Phase 3**: Template System  
**Phase 4**: DML Operations Builder  
**Phase 5**: File Export  
**Phase 6**: Validation Rules  
**Phase 7**: Flow Diagram  
**Phases 8-10**: Advanced Features

---

## Support

- **Documentation**: See `IMPLEMENTATION_PROGRESS.md` for detailed status
- **Specs**: See `/specs/004-interactive-mysql-5/` for full technical documentation
- **Issues**: Report to development team

---

**Last Updated**: October 17, 2025  
**Next Review**: After Phase 3 completion
