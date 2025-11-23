# Stored Procedure Contract: md_color_codes_Add

**Purpose**: Add custom color code to master table with duplicate prevention

**Domain**: Master Data - Color Codes  
**Type**: INSERT operation  
**Returns**: Status code indicating success/failure/duplicate

## Signature

```sql
CREATE PROCEDURE md_color_codes_Add(
    IN p_ColorCode VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

## Parameters

### Input Parameters

| Name | Type | Required | Description |
|------|------|----------|-------------|
| p_ColorCode | VARCHAR(50) | YES | Color name in Title Case (e.g., "Blueberry") |

### Output Parameters

| Name | Type | Description |
|------|------|-------------|
| p_Status | INT | 1 = inserted new, 0 = duplicate exists (silent success), negative = error |
| p_ErrorMsg | VARCHAR(500) | NULL on success, error message on failure |

## Business Rules

1. **Duplicate Handling**: If color already exists, return success (p_Status=0) without error
2. **Title Case**: Caller must provide Title Case formatted string
3. **User-Defined Flag**: All added colors marked IsUserDefined=TRUE
4. **Reserved Colors**: Cannot add "Unknown" or "OTHER" (system reserved)

## SQL Implementation

```sql
DELIMITER $$

CREATE PROCEDURE md_color_codes_Add(
    IN p_ColorCode VARCHAR(50),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE color_exists INT DEFAULT 0;
    
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error adding color code';
        ROLLBACK;
    END;

    -- Validation: Required field
    IF p_ColorCode IS NULL OR p_ColorCode = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'ColorCode is required';
        ROLLBACK;
        LEAVE;
    END IF;

    -- Validation: Reserved colors
    IF p_ColorCode IN ('Unknown', 'Other') THEN
        SET p_Status = -3;
        SET p_ErrorMsg = 'Cannot add reserved color codes';
        ROLLBACK;
        LEAVE;
    END IF;

    START TRANSACTION;

    -- Check for duplicate
    SELECT COUNT(*) INTO color_exists
    FROM md_color_codes
    WHERE ColorCode = p_ColorCode;

    IF color_exists > 0 THEN
        -- Duplicate exists, return success without inserting
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
        COMMIT;
    ELSE
        -- Insert new color
        INSERT INTO md_color_codes (ColorCode, IsUserDefined, CreatedDate)
        VALUES (p_ColorCode, TRUE, NOW());

        SET p_Status = 1;
        SET p_ErrorMsg = NULL;
        COMMIT;
    END IF;
END$$

DELIMITER ;
```

## C# DAO Usage

```csharp
/// <summary>
/// Adds custom color code to master table with duplicate prevention.
/// </summary>
/// <param name="colorCode">Color name in Title Case (e.g., "Blueberry")</param>
/// <returns>
/// Model_Dao_Result with bool indicating if new color was inserted.
/// IsSuccess=true for both new insert and duplicate (silent success).
/// </returns>
public static async Task<Model_Dao_Result<bool>> AddCustomColorAsync(string colorCode)
{
    try
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(colorCode))
        {
            return Model_Dao_Result<bool>.Failure("Color code cannot be empty");
        }

        // Format to Title Case
        var titleCased = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(colorCode.ToLower());

        var parameters = new Dictionary<string, object>
        {
            { "ColorCode", titleCased }  // No p_ prefix - auto-detected
        };

        var result = await Helper_Database_StoredProcedure
            .ExecuteNonQueryWithStatusAsync(
                "md_color_codes_Add",
                parameters);

        if (result.IsSuccess)
        {
            bool wasInserted = result.StatusCode == 1;  // 1=new, 0=duplicate
            return Model_Dao_Result<bool>.Success(wasInserted, result.StatusMessage);
        }
        else
        {
            return Model_Dao_Result<bool>.Failure(result.ErrorMessage);
        }
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleDatabaseError(
            ex,
            contextData: new Dictionary<string, object>
            {
                ["ColorCode"] = colorCode,
                ["Operation"] = "AddCustomColor"
            },
            callerName: nameof(AddCustomColorAsync));
            
        return Model_Dao_Result<bool>.Failure(
            "Failed to add custom color. Please try again.", ex);
    }
}
```

## Expected Results

### Success Case (New Color)
```
Input: p_ColorCode = "Blueberry"
Output:
  p_Status: 1 (inserted new)
  p_ErrorMsg: NULL
  DataTable: Empty (non-query operation)
```

### Success Case (Duplicate)
```
Input: p_ColorCode = "Blueberry" (already exists)
Output:
  p_Status: 0 (duplicate exists, silently succeeded)
  p_ErrorMsg: NULL
  DataTable: Empty
```

### Error Case (Reserved Color)
```
Input: p_ColorCode = "Unknown"
Output:
  p_Status: -3
  p_ErrorMsg: "Cannot add reserved color codes"
  DataTable: NULL
```

### Error Case (Empty Input)
```
Input: p_ColorCode = ""
Output:
  p_Status: -2
  p_ErrorMsg: "ColorCode is required"
  DataTable: NULL
```

## Performance

- **Expected Execution Time**: <10ms
- **Index Usage**: PRIMARY KEY lookup on ColorCode
- **Transaction**: INSERT wrapped in transaction with duplicate check
- **Cache Impact**: Requires cache refresh (Shift+Click Reset or app restart)

## Test Scenarios

1. **Test New Color**: Add "Blueberry" → verify p_Status=1, color in table
2. **Test Duplicate**: Add "Blueberry" again → verify p_Status=0, no error
3. **Test Reserved "Unknown"**: Try add "Unknown" → verify p_Status=-3, error
4. **Test Reserved "OTHER"**: Try add "OTHER" → verify p_Status=-3, error
5. **Test Empty Input**: Add "" → verify p_Status=-2, error
6. **Test Title Case**: Add "dark GREEN" → verify stored as "Dark Green"
7. **Test Cache Refresh**: Add color → verify visible after Shift+Click Reset
