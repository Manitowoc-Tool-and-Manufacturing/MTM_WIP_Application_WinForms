# Stored Procedure Contract: sp_error_reports_GetAll

**Purpose**: Retrieve error reports with optional filtering by date range, user, status, machine, and search text.

**Created**: 2025-10-25  
**Database**: MTM_WIP_Application_Winforms (Release) / mtm_wip_application_winforms_test (Debug)  
**Table**: error_reports

---

## Input Parameters

| Parameter     | Type         | Required | Default | Description                                     | Example               |
| ------------- | ------------ | -------- | ------- | ----------------------------------------------- | --------------------- |
| p_DateFrom    | DATETIME     | No       | NULL    | Start of date range filter (inclusive)          | '2025-10-18 00:00:00' |
| p_DateTo      | DATETIME     | No       | NULL    | End of date range filter (inclusive)            | '2025-10-25 23:59:59' |
| p_UserName    | VARCHAR(100) | No       | NULL    | Filter by specific username (exact match)       | 'John.Smith'          |
| p_MachineName | VARCHAR(200) | No       | NULL    | Filter by specific machine name (exact match)   | 'DESK-01'             |
| p_Status      | VARCHAR(20)  | No       | NULL    | Filter by status: 'New', 'Reviewed', 'Resolved' | 'New'                 |
| p_SearchText  | VARCHAR(255) | No       | NULL    | Search term for LIKE query across text fields   | 'database'            |

---

## Output Parameters

| Parameter  | Type         | Description                   | Values                                                                               |
| ---------- | ------------ | ----------------------------- | ------------------------------------------------------------------------------------ |
| p_Status   | INT          | Operation result code         | 0 = Success<br>1 = Success (no data)<br>-1 = Database error                          |
| p_ErrorMsg | VARCHAR(500) | Human-readable status message | 'Retrieved N error reports'<br>'No error reports found'<br>'Database error occurred' |

---

## Result Set

Returns a DataTable with the following columns:

| Column       | Type         | Nullable | Description                 | Notes                         |
| ------------ | ------------ | -------- | --------------------------- | ----------------------------- |
| ReportID     | INT          | No       | Primary key                 | Auto-increment ID             |
| ReportDate   | DATETIME     | No       | When error occurred         | Format: 'yyyy-MM-dd HH:mm:ss' |
| UserName     | VARCHAR(100) | No       | Windows username            | Submitter of report           |
| MachineName  | VARCHAR(200) | Yes      | Computer name               | May be NULL                   |
| ErrorType    | VARCHAR(255) | Yes      | Exception type name         | May be NULL                   |
| ErrorSummary | TEXT         | Yes      | User-friendly error message | Truncate in UI to 100 chars   |
| Status       | VARCHAR(20)  | No       | Current status              | 'New', 'Reviewed', 'Resolved' |
| ReviewedBy   | VARCHAR(100) | Yes      | Developer who reviewed      | NULL if Status='New'          |
| ReviewedDate | DATETIME     | Yes      | When status changed         | NULL if Status='New'          |

---

## SQL Logic

```sql
SELECT
    ReportID,
    ReportDate,
    UserName,
    MachineName,
    ErrorType,
    ErrorSummary,
    Status,
    ReviewedBy,
    ReviewedDate
FROM error_reports
WHERE
    (p_DateFrom IS NULL OR ReportDate >= p_DateFrom) AND
    (p_DateTo IS NULL OR ReportDate <= p_DateTo) AND
    (p_UserName IS NULL OR UserName = p_UserName) AND
    (p_MachineName IS NULL OR MachineName = p_MachineName) AND
    (p_Status IS NULL OR Status = p_Status) AND
    (p_SearchText IS NULL OR
        ErrorSummary LIKE CONCAT('%', p_SearchText, '%') OR
        UserNotes LIKE CONCAT('%', p_SearchText, '%') OR
        TechnicalDetails LIKE CONCAT('%', p_SearchText, '%')
    )
ORDER BY ReportDate DESC;
```

**Performance Notes**:

-   Indexes on Status, ReportDate, UserName, MachineName support fast filtering
-   LIKE queries on TEXT fields may be slow with large datasets
-   Consider FULLTEXT index if search performance degrades

---

## C# Usage Example

```csharp
// Build filter parameters
var filter = new Model_ErrorReport_Core_Filter
{
    DateFrom = DateTime.Parse("2025-10-18"),
    DateTo = DateTime.Parse("2025-10-25").AddDays(1).AddTicks(-1),
    Status = "New",
    SearchText = "database"
};

var parameters = new Dictionary<string, object>
{
    ["DateFrom"] = filter.DateFrom ?? (object)DBNull.Value,
    ["DateTo"] = filter.DateTo ?? (object)DBNull.Value,
    ["UserName"] = filter.UserName ?? (object)DBNull.Value,
    ["MachineName"] = filter.MachineName ?? (object)DBNull.Value,
    ["Status"] = filter.Status ?? (object)DBNull.Value,
    ["SearchText"] = filter.SearchText ?? (object)DBNull.Value
};

// Execute stored procedure
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString,
    "sp_error_reports_GetAll",
    parameters,
    progressHelper: _progressHelper);

if (result.IsSuccess)
{
    DataTable reports = result.Data;
    // Bind to DataGridView
    dgvErrorReports.DataSource = reports;
}
else
{
    Service_ErrorHandler.HandleException(
        result.Exception,
        Enum_ErrorSeverity.Medium,
        message: result.StatusMessage);
}
```

---

## Test Cases

### Test Case 1: No Filters (All Reports)

**Input**: All parameters NULL  
**Expected Output**: All error reports, ordered by ReportDate DESC  
**Expected Status**: 0 (Success)

### Test Case 2: Date Range Filter

**Input**: DateFrom='2025-10-18', DateTo='2025-10-25'  
**Expected Output**: Reports between dates (inclusive)  
**Expected Status**: 0 (Success)

### Test Case 3: Status Filter

**Input**: Status='New'  
**Expected Output**: Only reports with Status='New'  
**Expected Status**: 0 (Success)

### Test Case 4: Search Text

**Input**: SearchText='database'  
**Expected Output**: Reports where ErrorSummary, UserNotes, or TechnicalDetails contain 'database'  
**Expected Status**: 0 (Success)

### Test Case 5: Combined Filters

**Input**: DateFrom='2025-10-18', Status='New', SearchText='database'  
**Expected Output**: Reports matching all criteria  
**Expected Status**: 0 (Success)

### Test Case 6: No Matches

**Input**: DateFrom='2025-01-01', DateTo='2025-01-01'  
**Expected Output**: Empty DataTable  
**Expected Status**: 1 (Success, no data)

### Test Case 7: Invalid Date Range

**Input**: DateFrom='2025-10-25', DateTo='2025-10-18' (From > To)  
**Expected Output**: Empty DataTable (no matches)  
**Expected Status**: 1 (Success, no data)  
**Note**: Validation should occur in C# layer before calling SP

---

## Validation Rules

1. Date range validation (DateFrom <= DateTo) should be enforced in UI layer
2. Status must be one of: 'New', 'Reviewed', 'Resolved' (validated in UI dropdown)
3. SearchText minimum 3 characters (validated in UI layer)
4. All parameters nullable - passing NULL disables that filter
5. Case-insensitive search via LIKE (inherent in MySQL collation)

---

## Error Handling

-   **Database connection failure**: Returns p_Status=-1, p_ErrorMsg='Database error occurred'
-   **SQL syntax error**: Returns p_Status=-1, p_ErrorMsg with error details
-   **No results found**: Returns p_Status=1, p_ErrorMsg='No error reports found', empty DataTable
-   **Success with data**: Returns p_Status=0, p_ErrorMsg='Retrieved N error reports'

---

## Performance Benchmarks

**Target Performance** (from spec.md):

-   1000 reports → 50 matching: < 500ms
-   100 reports with filters: < 400ms

**Optimization Strategies**:

-   Use indexed columns for WHERE filters (Status, ReportDate, UserName, MachineName)
-   LIMIT clause if implementing paging (TOP 1000)
-   Avoid LIKE on TEXT fields when possible (use exact match filters first)

---

## Change History

| Date       | Version | Changes                     | Author            |
| ---------- | ------- | --------------------------- | ----------------- |
| 2025-10-25 | 1.0.0   | Initial contract definition | AI Planning Agent |
