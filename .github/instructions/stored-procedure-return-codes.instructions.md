# Stored Procedure Status Code Standards

## Status Code Convention

All stored procedures MUST follow this status code convention:

| Status Code | Meaning | When to Use |
|-------------|---------|-------------|
| **1** | Success with data | Query returned one or more rows |
| **0** | Success with no data | Query executed successfully but returned zero rows |
| **-1** | Database error | SQL exception occurred |
| **-2** | Validation error | Input validation failed |
| **-3** | Business logic error | Business rule violation |
| **-4** | Not found error | Specific record not found |
| **-5** | Duplicate error | Duplicate key/constraint violation |

## Template Pattern for SELECT Procedures

```sql
DELIMITER $$

DROP PROCEDURE IF EXISTS `procedureName`$$

CREATE PROCEDURE `procedureName`(
    IN p_Parameter1 VARCHAR(100),
    IN p_Parameter2 INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while executing procedureName';
    END;

    -- Validate inputs
    IF p_Parameter1 IS NULL OR p_Parameter1 = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Parameter1 is required';
    ELSE
        -- Execute query
        SELECT * FROM table_name WHERE column = p_Parameter1;
        
        -- Check row count
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = NULL;
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = 'No records found';
        END IF;
    END IF;
END$$

DELIMITER ;
```

## Template Pattern for INSERT/UPDATE/DELETE Procedures

```sql
DELIMITER $$

DROP PROCEDURE IF EXISTS `procedureName`$$

CREATE PROCEDURE `procedureName`(
    IN p_Parameter1 VARCHAR(100),
    IN p_Parameter2 INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        GET DIAGNOSTICS CONDITION 1
            @sqlstate = RETURNED_SQLSTATE, 
            @errno = MYSQL_ERRNO, 
            @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error: ', @text);
    END;

    START TRANSACTION;
    
    -- Validate inputs
    IF p_Parameter1 IS NULL OR p_Parameter1 = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Parameter1 is required';
        ROLLBACK;
    ELSE
        -- Execute operation
        INSERT INTO table_name (column1, column2) VALUES (p_Parameter1, p_Parameter2);
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            COMMIT;
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Successfully affected ', v_RowsAffected, ' row(s)');
        ELSE
            ROLLBACK;
            SET p_Status = 0;  -- No rows affected
            SET p_ErrorMsg = 'No rows were affected';
        END IF;
    END IF;
END$$

DELIMITER ;
```

## Critical Rules

1. **ALWAYS use FOUND_ROWS() or ROW_COUNT()** to determine if data was returned/affected
2. **NEVER set p_Status = 0 unconditionally** at the end of a procedure
3. **Use explicit transaction handling** for INSERT/UPDATE/DELETE operations
4. **Validate all required inputs** before executing queries
5. **Return meaningful error messages** that help with debugging

## Common Mistakes to Avoid

❌ **WRONG** - Unconditional status 0:
```sql
SELECT * FROM users;
SET p_Status = 0;  -- WRONG! Should check if data was returned
```

✅ **CORRECT** - Check row count:
```sql
SELECT * FROM users;
SELECT FOUND_ROWS() INTO v_Count;
IF v_Count > 0 THEN
    SET p_Status = 1;
ELSE
    SET p_Status = 0;
END IF;
```
