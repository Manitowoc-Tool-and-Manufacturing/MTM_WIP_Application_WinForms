# Stored Procedure Contract: sp_error_reports_GetUserList

**Purpose**: Retrieve distinct list of usernames from error_reports table for filter dropdown population.

**Created**: 2025-10-25  
**Database**: mtm_wip_application (Release) / mtm_wip_application_winforms_test (Debug)  
**Table**: error_reports

---

## Input Parameters

None.

---

## Output Parameters

| Parameter | Type | Description | Values |
|-----------|------|-------------|--------|
| p_Status | INT | Operation result code | 0 = Success<br>1 = Success (no data)<br>-1 = Database error |
| p_ErrorMsg | VARCHAR(500) | Human-readable status message | 'Retrieved N unique users'<br>'No users found'<br>'Database error occurred' |

---

## Result Set

Returns a DataTable with a single column:

| Column | Type | Nullable | Description | Notes |
|--------|------|----------|-------------|-------|
| UserName | VARCHAR(100) | No | Distinct username values | Sorted alphabetically |

---

## SQL Logic

```sql
SELECT DISTINCT UserName
FROM error_reports
ORDER BY UserName ASC;

-- Set status based on row count
IF ROW_COUNT() = 0 THEN
    SET p_Status = 1;
    SET p_ErrorMsg = 'No users found';
ELSE
    SET p_Status = 0;
    SET p_ErrorMsg = CONCAT('Retrieved ', ROW_COUNT(), ' unique users');
END IF;
```

**Performance Notes**:
- Uses index on UserName column for fast DISTINCT query
- Result set typically small (< 100 usernames)
- ORDER BY ensures consistent dropdown ordering

---

## C# Usage Example

```csharp
public static async Task<DaoResult<List<string>>> GetUserListAsync()
{
    try
    {
        string connectionString = Helper_Database_Variables.GetConnectionString();
        
        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            connectionString,
            "sp_error_reports_GetUserList",
            parameters: null,
            progressHelper: null);
        
        if (!result.IsSuccess)
        {
            LoggingUtility.Log($"[Dao_ErrorReports] Failed to retrieve user list: {result.StatusMessage}");
            return DaoResult<List<string>>.Failure(result.StatusMessage);
        }
        
        if (result.Data == null || result.Data.Rows.Count == 0)
        {
            return DaoResult<List<string>>.Success(
                new List<string>(), 
                "No users found");
        }
        
        // Convert DataTable to List<string>
        var users = result.Data.AsEnumerable()
            .Select(row => row["UserName"].ToString() ?? string.Empty)
            .Where(user => !string.IsNullOrEmpty(user))
            .ToList();
        
        return DaoResult<List<string>>.Success(
            users, 
            $"Retrieved {users.Count} users");
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return DaoResult<List<string>>.Failure(
            "An unexpected error occurred while retrieving user list.");
    }
}
```

---

## UI Usage

### ComboBox Population

```csharp
private async void PopulateUserFilterComboBox()
{
    var result = await Dao_ErrorReports.GetUserListAsync();
    
    if (result.IsSuccess)
    {
        cmbUserFilter.Items.Clear();
        cmbUserFilter.Items.Add("[ All Users ]"); // Default option
        
        foreach (string user in result.Data)
        {
            cmbUserFilter.Items.Add(user);
        }
        
        cmbUserFilter.SelectedIndex = 0; // Select "All Users" by default
    }
    else
    {
        Service_ErrorHandler.HandleException(
            result.Exception,
            ErrorSeverity.Low,
            message: "Failed to load user list for filter");
        
        // Fallback: Add "All Users" only
        cmbUserFilter.Items.Clear();
        cmbUserFilter.Items.Add("[ All Users ]");
        cmbUserFilter.SelectedIndex = 0;
    }
}
```

### Filter Logic

```csharp
private void ApplyFilters()
{
    string selectedUser = cmbUserFilter.Text;
    
    // If "All Users" selected, pass NULL to stored procedure
    string userFilter = (selectedUser == "[ All Users ]") 
        ? null 
        : selectedUser;
    
    // Build filter object
    var filter = new Model_ErrorReportFilter
    {
        UserName = userFilter,
        // ... other filters
    };
    
    // Apply filter via GetAllErrorReportsAsync()
}
```

---

## Test Cases

### Test Case 1: Multiple Users Exist
**Input**: None  
**Expected Output**: DataTable with multiple UserName rows, sorted alphabetically  
**Expected Status**: 0 (Success)  
**Example**: "Dev.Smith", "John.Doe", "Test.User"

### Test Case 2: Single User Exists
**Input**: None  
**Expected Output**: DataTable with single UserName row  
**Expected Status**: 0 (Success)

### Test Case 3: No Users Exist
**Input**: None  
**Expected Output**: Empty DataTable  
**Expected Status**: 1 (Success, no data)

### Test Case 4: Duplicate Username Handling
**Input**: error_reports has 10 reports from "John.Doe"  
**Expected Output**: Single row with "John.Doe" (DISTINCT behavior)  
**Expected Status**: 0 (Success)

---

## Validation Rules

1. No input validation needed (no parameters)
2. Output always non-null list (may be empty)
3. UserName values are never NULL in database (per table definition)
4. Results sorted alphabetically for consistent UI experience

---

## Error Handling

- **Database connection failure**: Returns p_Status=-1, empty result set
- **No data**: Returns p_Status=1, empty result set (not an error)
- **Success**: Returns p_Status=0, populated result set

---

## Performance Benchmarks

**Target Performance**:
- Retrieve 100 distinct users: < 100ms

**Optimization Notes**:
- Index on UserName column ensures fast DISTINCT query
- Result set size limited by number of unique users (typically < 100)
- No JOINs or complex logic

---

## UI Design Notes

### ComboBox Display

- **Default item**: "[ All Users ]" (indicates no filter)
- **Item format**: Plain username (e.g., "John.Smith")
- **Selection behavior**: SelectedIndexChanged triggers filter re-application
- **Placeholder**: Use Helper_UI_ComboBoxes.ValidateComboBoxItem() pattern

### Refresh Strategy

- **On window load**: Populate ComboBox once
- **On data changes**: Refresh if new error reports added with new users (rare)
- **Manual refresh**: Optional "Refresh Filters" button

---

## Alternative Implementations

### Option A: Include Row Count per User

**Modified SQL**:
```sql
SELECT UserName, COUNT(*) AS ReportCount
FROM error_reports
GROUP BY UserName
ORDER BY UserName ASC;
```

**ComboBox Display**: "John.Smith (15 reports)"

**Pros**: Provides context to developers  
**Cons**: More complex UI formatting, performance overhead

**Decision**: Use simple version (Option A not implemented) for MVP

---

## Change History

| Date | Version | Changes | Author |
|------|---------|---------|--------|
| 2025-10-25 | 1.0.0 | Initial contract definition | AI Planning Agent |
