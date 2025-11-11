# Stored Procedure Contract: sp_error_reports_GetMachineList

**Purpose**: Retrieve distinct list of machine names from error_reports table for filter dropdown population.

**Created**: 2025-10-25  
**Database**: MTM_WIP_Application_Winforms (Release) / mtm_wip_application_winforms_test (Debug)  
**Table**: error_reports

---

## Input Parameters

None.

---

## Output Parameters

| Parameter  | Type         | Description                   | Values                                                                            |
| ---------- | ------------ | ----------------------------- | --------------------------------------------------------------------------------- |
| p_Status   | INT          | Operation result code         | 0 = Success<br>1 = Success (no data)<br>-1 = Database error                       |
| p_ErrorMsg | VARCHAR(500) | Human-readable status message | 'Retrieved N unique machines'<br>'No machines found'<br>'Database error occurred' |

---

## Result Set

Returns a DataTable with a single column:

| Column      | Type         | Nullable | Description                  | Notes                                 |
| ----------- | ------------ | -------- | ---------------------------- | ------------------------------------- |
| MachineName | VARCHAR(200) | No       | Distinct machine name values | Excludes NULLs, sorted alphabetically |

---

## SQL Logic

```sql
SELECT DISTINCT MachineName
FROM error_reports
WHERE MachineName IS NOT NULL
ORDER BY MachineName ASC;

-- Set status based on row count
IF ROW_COUNT() = 0 THEN
    SET p_Status = 1;
    SET p_ErrorMsg = 'No machines found';
ELSE
    SET p_Status = 0;
    SET p_ErrorMsg = CONCAT('Retrieved ', ROW_COUNT(), ' unique machines');
END IF;
```

**Logic Notes**:

-   Excludes NULL MachineName values (reports without machine info)
-   DISTINCT removes duplicates
-   ORDER BY ensures consistent dropdown ordering
-   Alphabetical sorting helps developers find machines quickly

**Performance Notes**:

-   Uses index on MachineName column for fast DISTINCT query
-   Result set typically small (< 50 machine names)
-   WHERE MachineName IS NOT NULL may slightly reduce performance but improves user experience

---

## C# Usage Example

```csharp
public static async Task<Model_Dao_Result<List<string>>> GetMachineListAsync()
{
    try
    {
        string connectionString = Helper_Database_Variables.GetConnectionString();

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            connectionString,
            "sp_error_reports_GetMachineList",
            parameters: null,
            progressHelper: null);

        if (!result.IsSuccess)
        {
            LoggingUtility.Log($"[Dao_ErrorReports] Failed to retrieve machine list: {result.StatusMessage}");
            return Model_Dao_Result<List<string>>.Failure(result.StatusMessage);
        }

        if (result.Data == null || result.Data.Rows.Count == 0)
        {
            return Model_Dao_Result<List<string>>.Success(
                new List<string>(),
                "No machines found");
        }

        // Convert DataTable to List<string>
        var machines = result.Data.AsEnumerable()
            .Select(row => row["MachineName"].ToString() ?? string.Empty)
            .Where(machine => !string.IsNullOrEmpty(machine))
            .ToList();

        return Model_Dao_Result<List<string>>.Success(
            machines,
            $"Retrieved {machines.Count} machines");
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return Model_Dao_Result<List<string>>.Failure(
            "An unexpected error occurred while retrieving machine list.");
    }
}
```

---

## UI Usage

### ComboBox Population

```csharp
private async void PopulateMachineFilterComboBox()
{
    var result = await Dao_ErrorReports.GetMachineListAsync();

    if (result.IsSuccess)
    {
        cmbMachineFilter.Items.Clear();
        cmbMachineFilter.Items.Add("[ All Machines ]"); // Default option

        foreach (string machine in result.Data)
        {
            cmbMachineFilter.Items.Add(machine);
        }

        cmbMachineFilter.SelectedIndex = 0; // Select "All Machines" by default
    }
    else
    {
        Service_ErrorHandler.HandleException(
            result.Exception,
            Enum_ErrorSeverity.Low,
            message: "Failed to load machine list for filter");

        // Fallback: Add "All Machines" only
        cmbMachineFilter.Items.Clear();
        cmbMachineFilter.Items.Add("[ All Machines ]");
        cmbMachineFilter.SelectedIndex = 0;
    }
}
```

### Filter Logic

```csharp
private void ApplyFilters()
{
    string selectedMachine = cmbMachineFilter.Text;

    // If "All Machines" selected, pass NULL to stored procedure
    string machineFilter = (selectedMachine == "[ All Machines ]")
        ? null
        : selectedMachine;

    // Build filter object
    var filter = new Model_ErrorReport_Core_Filter
    {
        MachineName = machineFilter,
        // ... other filters
    };

    // Apply filter via GetAllErrorReportsAsync()
}
```

---

## Test Cases

### Test Case 1: Multiple Machines Exist

**Input**: None  
**Expected Output**: DataTable with multiple MachineName rows, sorted alphabetically  
**Expected Status**: 0 (Success)  
**Example**: "DESK-01", "DESK-02", "LAPTOP-ABC", "WORKSTATION-42"

### Test Case 2: Single Machine Exists

**Input**: None  
**Expected Output**: DataTable with single MachineName row  
**Expected Status**: 0 (Success)

### Test Case 3: No Machines Exist (All NULL)

**Input**: error_reports table has reports but all MachineName values are NULL  
**Expected Output**: Empty DataTable  
**Expected Status**: 1 (Success, no data)

### Test Case 4: Duplicate Machine Name Handling

**Input**: error_reports has 20 reports from "DESK-01"  
**Expected Output**: Single row with "DESK-01" (DISTINCT behavior)  
**Expected Status**: 0 (Success)

### Test Case 5: Mixed NULL and Non-NULL

**Input**: error_reports has 10 reports with MachineName, 5 with NULL  
**Expected Output**: Only non-NULL machine names (10 distinct values)  
**Expected Status**: 0 (Success)

---

## Validation Rules

1. No input validation needed (no parameters)
2. Output always non-null list (may be empty)
3. NULL MachineName values excluded from result set
4. Results sorted alphabetically for consistent UI experience
5. MachineName values conform to Windows computer name format (no special validation in SP)

---

## Error Handling

-   **Database connection failure**: Returns p_Status=-1, empty result set
-   **No data**: Returns p_Status=1, empty result set (not an error)
-   **Success**: Returns p_Status=0, populated result set

---

## Performance Benchmarks

**Target Performance**:

-   Retrieve 50 distinct machines: < 100ms

**Optimization Notes**:

-   Index on MachineName column ensures fast DISTINCT query
-   WHERE MachineName IS NOT NULL uses index efficiently
-   Result set size limited by number of unique machines (typically < 50)
-   No JOINs or complex logic

---

## UI Design Notes

### ComboBox Display

-   **Default item**: "[ All Machines ]" (indicates no filter)
-   **Item format**: Plain machine name (e.g., "DESK-01")
-   **Selection behavior**: SelectedIndexChanged triggers filter re-application
-   **Placeholder**: Use Helper_UI_ComboBoxes.ValidateComboBoxItem() pattern
-   **Tooltip**: Consider showing full machine name if truncated in ComboBox

### Refresh Strategy

-   **On window load**: Populate ComboBox once
-   **On data changes**: Refresh if new error reports added with new machines (rare)
-   **Manual refresh**: Optional "Refresh Filters" button

### NULL Handling in UI

-   **Spec note**: "Grid sorting with null values: How to sort when Machine or ReviewedBy is NULL? Sort nulls last."
-   ComboBox excludes NULLs (only shows machines with names)
-   Grid filter "All Machines" includes reports with NULL MachineName
-   Grid sorting: NULL MachineNames sort last (handled in grid CellFormatting or sort comparer)

---

## Alternative Implementations

### Option A: Include "(Unknown)" for NULL Machine Names

**Modified SQL**:

```sql
SELECT DISTINCT COALESCE(MachineName, '(Unknown)') AS MachineName
FROM error_reports
ORDER BY MachineName ASC;
```

**ComboBox Display**: Includes "(Unknown)" as an option

**Pros**: Allows filtering reports without machine names  
**Cons**: Adds complexity, "(Unknown)" is not a real machine name

**Decision**: Use simple version (exclude NULLs) for MVP. "All Machines" filter includes NULL values.

### Option B: Include Report Count per Machine

**Modified SQL**:

```sql
SELECT MachineName, COUNT(*) AS ReportCount
FROM error_reports
WHERE MachineName IS NOT NULL
GROUP BY MachineName
ORDER BY MachineName ASC;
```

**ComboBox Display**: "DESK-01 (8 reports)"

**Pros**: Provides context to developers  
**Cons**: More complex UI formatting, performance overhead

**Decision**: Use simple version (Option B not implemented) for MVP

---

## Change History

| Date       | Version | Changes                     | Author            |
| ---------- | ------- | --------------------------- | ----------------- |
| 2025-10-25 | 1.0.0   | Initial contract definition | AI Planning Agent |
