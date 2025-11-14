# Stored Procedure Contract: md_part_ids_GetAllColorCodeFlagged

**Purpose**: Retrieve all part IDs that require color code tracking for cache population

**Domain**: Master Data - Part IDs  
**Type**: SELECT query  
**Returns**: DataTable with part IDs flagged for color codes

## Signature

```sql
CREATE PROCEDURE md_part_ids_GetAllColorCodeFlagged(
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
| PartID | VARCHAR(300) | Part number requiring color code tracking |

**Sort Order**: `PartID ASC` (alphabetical)

**Expected Row Count**: Variable (0-100+ depending on shop floor requirements)

## SQL Implementation

```sql
DELIMITER $$

CREATE PROCEDURE md_part_ids_GetAllColorCodeFlagged(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Error retrieving color-coded part IDs';
        ROLLBACK;
    END;

    START TRANSACTION;

    -- Retrieve all part IDs where RequiresColorCode flag is TRUE
    SELECT PartID
    FROM md_part_ids
    WHERE RequiresColorCode = TRUE
    ORDER BY PartID ASC;

    -- Set success status
    IF FOUND_ROWS() > 0 THEN
        SET p_Status = 1;  -- Success with data
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 0;  -- Success but no data (no parts flagged yet)
        SET p_ErrorMsg = NULL;
    END IF;

    COMMIT;
END$$

DELIMITER ;
```

## C# DAO Usage

```csharp
/// <summary>
/// Gets all part IDs that require color code tracking.
/// Used to populate cache at application startup.
/// </summary>
/// <returns>
/// Model_Dao_Result with DataTable containing PartID column.
/// Returns empty DataTable if no parts flagged (IsSuccess=true, Data.Rows.Count=0).
/// </returns>
public static async Task<Model_Dao_Result<DataTable>> GetAllColorCodeFlaggedAsync()
{
    try
    {
        var result = await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync(
                "md_part_ids_GetAllColorCodeFlagged",
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
                ["Operation"] = "GetAllColorCodeFlagged"
            },
            callerName: nameof(GetAllColorCodeFlaggedAsync));
            
        return Model_Dao_Result<DataTable>.Failure(
            "Failed to retrieve color-coded parts. Please try again.", ex);
    }
}
```

## Cache Population Pattern

```csharp
// In Helper_UI_ComboBoxes.cs or MainForm initialization
public static async Task ReloadColorCodeCachesAsync()
{
    var result = await Dao_Part.GetAllColorCodeFlaggedAsync();
    
    if (result.IsSuccess && result.Data != null)
    {
        Model_Application_Variables.ColorFlaggedParts = 
            result.Data.AsEnumerable()
            .Select(row => row["PartID"].ToString()!)
            .ToList();
            
        LoggingUtility.Log($"Loaded {Model_Application_Variables.ColorFlaggedParts.Count} color-flagged parts into cache");
    }
    else
    {
        Model_Application_Variables.ColorFlaggedParts = new List<string>();
        LoggingUtility.Log("No color-flagged parts found (cache empty)");
    }
}
```

## Expected Results

### Success Case (Parts Flagged)
```
p_Status: 1
p_ErrorMsg: NULL
DataTable: 5 rows
┌──────────────┐
│ PartID       │
├──────────────┤
│ ABC-12345    │
│ DEF-67890    │
│ GHI-11111    │
│ JKL-22222    │
│ MNO-33333    │
└──────────────┘
```

### Success Case (No Parts Flagged)
```
p_Status: 0
p_ErrorMsg: NULL
DataTable: 0 rows (empty but valid)
```

## Performance

- **Expected Execution Time**: <10ms
- **Index Usage**: idx_requires_colorcode on md_part_ids(RequiresColorCode)
- **Caching**: Results cached in `Model_Application_Variables.ColorFlaggedParts` (List<string>)
- **Refresh Trigger**: App startup, app restart after Settings changes, Shift+Click Reset

## Test Scenarios

1. **Test No Flagged Parts**: Ensure all RequiresColorCode=FALSE → expect p_Status=0, empty DataTable
2. **Test Multiple Flagged Parts**: Flag 5 parts → verify all 5 returned alphabetically
3. **Test Alphabetical Sort**: Flag parts in random order → verify sorted by PartID ASC
4. **Test Cache Population**: Call procedure → verify cache populated correctly
5. **Test After Settings Change**: Flag new part → restart app → verify cache updated
