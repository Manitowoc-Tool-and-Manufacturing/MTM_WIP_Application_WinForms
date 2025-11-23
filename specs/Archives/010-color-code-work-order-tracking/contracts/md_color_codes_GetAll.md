# Stored Procedure Contract: md_color_codes_GetAll

**Purpose**: Retrieve all color codes from master table for dropdown/suggestion lists

**Domain**: Master Data - Color Codes  
**Type**: SELECT query  
**Returns**: DataTable with color codes

## Signature

```sql
CREATE PROCEDURE md_color_codes_GetAll(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
```

## Parameters

### Output Parameters

| Name | Type | Description |
|------|------|-------------|
| p_Status | INT | 1 = success with data, 0 = success no data, negative = error |
| p_ErrorMsg | VARCHAR(500) | NULL on success, error message on failure |

## Return Data

**Columns**:
| Column Name | Type | Description |
|-------------|------|-------------|
| ColorCode | VARCHAR(50) | Color name (e.g., "RED", "BLUE", "Blueberry") |
| IsUserDefined | BOOLEAN | FALSE for predefined, TRUE for custom colors |
| CreatedDate | DATETIME | When color was added to system |

**Sort Order**: `ColorCode ASC` (alphabetical)

**Expected Row Count**: 10-20 rows (10 predefined + custom colors)

## SQL Implementation

```sql
DELIMITER $$

CREATE PROCEDURE md_color_codes_GetAll(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error retrieving color codes';
        ROLLBACK;
    END;

    -- Start transaction
    START TRANSACTION;

    -- Retrieve all color codes except "OTHER" (UI-only trigger)
    SELECT 
        ColorCode,
        IsUserDefined,
        CreatedDate
    FROM md_color_codes
    WHERE ColorCode != 'Other'  -- "OTHER" handled by UI dialog
    ORDER BY ColorCode ASC;

    -- Set success status
    IF FOUND_ROWS() > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;  -- Success but no data (unusual, should have predefined colors)
        SET p_ErrorMsg = 'No color codes found';
    END IF;

    COMMIT;
END$$

DELIMITER ;
```

## C# DAO Usage

```csharp
public class Dao_ColorCode
{
    /// <summary>
    /// Gets all color codes from master table.
    /// </summary>
    /// <returns>
    /// Model_Dao_Result with DataTable containing ColorCode, IsUserDefined, CreatedDate.
    /// Check IsSuccess before accessing Data.
    /// </returns>
    public static async Task<Model_Dao_Result<DataTable>> GetAllAsync()
    {
        try
        {
            var result = await Helper_Database_StoredProcedure
                .ExecuteDataTableWithStatusAsync(
                    "md_color_codes_GetAll",
                    parameters: null);  // No input parameters

            return result.IsSuccess
                ? Model_Dao_Result<DataTable>.Success(result.Data, result.StatusMessage)
                : Model_Dao_Result<DataTable>.Failure(result.ErrorMessage);
        }
        catch (Exception ex)
        {
            Service_ErrorHandler.HandleDatabaseError(
                ex,
                contextData: new Dictionary<string, object>
                {
                    ["Operation"] = "GetAllColorCodes"
                },
                callerName: nameof(GetAllAsync));
                
            return Model_Dao_Result<DataTable>.Failure(
                "Failed to retrieve color codes. Please try again.", ex);
        }
    }
}
```

## Expected Results

### Success Case (Normal)
```
p_Status: 1
p_ErrorMsg: NULL
DataTable: 12 rows
┌──────────────┬────────────────┬─────────────────────┐
│ ColorCode    │ IsUserDefined  │ CreatedDate         │
├──────────────┼────────────────┼─────────────────────┤
│ Black        │ 0              │ 2025-11-13 10:00:00 │
│ Blue         │ 0              │ 2025-11-13 10:00:00 │
│ Blueberry    │ 1              │ 2025-11-13 14:30:00 │
│ Green        │ 0              │ 2025-11-13 10:00:00 │
│ Orange       │ 0              │ 2025-11-13 10:00:00 │
│ Pink         │ 0              │ 2025-11-13 10:00:00 │
│ Purple       │ 0              │ 2025-11-13 10:00:00 │
│ Red          │ 0              │ 2025-11-13 10:00:00 │
│ Unknown      │ 0              │ 2025-11-13 10:00:00 │
│ White        │ 0              │ 2025-11-13 10:00:00 │
│ Yellow       │ 0              │ 2025-11-13 10:00:00 │
└──────────────┴────────────────┴─────────────────────┘
```

### Error Case (Database Unavailable)
```
p_Status: -1
p_ErrorMsg: "Error retrieving color codes"
DataTable: NULL
```

## Performance

- **Expected Execution Time**: <5ms
- **Row Count**: 10-20 rows (small dataset)
- **Index Usage**: PRIMARY KEY on ColorCode (clustered)
- **Caching**: Results cached in `Model_Application_Variables.ColorCodes`
- **Refresh Trigger**: App startup, Shift+Click Reset

## Test Scenarios

1. **Test Empty Table**: Delete all colors → expect p_Status=0
2. **Test Predefined Only**: Verify 10 standard colors returned
3. **Test With Custom Colors**: Add custom color → verify included in results
4. **Test Alphabetical Sort**: Verify ColorCode alphabetical order
5. **Test "OTHER" Exclusion**: Verify "OTHER" not in results (UI trigger only)
6. **Test Connection Failure**: Simulate DB down → verify error handling
