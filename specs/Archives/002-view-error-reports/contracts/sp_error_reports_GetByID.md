# Stored Procedure Contract: sp_error_reports_GetByID

**Purpose**: Retrieve complete details for a single error report by ReportID.

**Created**: 2025-10-25  
**Database**: MTM_WIP_Application_Winforms (Release) / mtm_wip_application_winforms_test (Debug)  
**Table**: error_reports

---

## Input Parameters

| Parameter  | Type | Required | Default | Description                             | Example |
| ---------- | ---- | -------- | ------- | --------------------------------------- | ------- |
| p_ReportID | INT  | Yes      | N/A     | Primary key of error report to retrieve | 123     |

---

## Output Parameters

| Parameter  | Type         | Description                   | Values                                                                                     |
| ---------- | ------------ | ----------------------------- | ------------------------------------------------------------------------------------------ |
| p_Status   | INT          | Operation result code         | 0 = Success<br>-1 = Database error<br>-2 = ReportID not found                              |
| p_ErrorMsg | VARCHAR(500) | Human-readable status message | 'Error report retrieved successfully'<br>'ReportID not found'<br>'Database error occurred' |

---

## Result Set

Returns a single row DataTable with ALL fields from error_reports table:

| Column           | Type         | Nullable | Max Size | Description                                            |
| ---------------- | ------------ | -------- | -------- | ------------------------------------------------------ |
| ReportID         | INT          | No       | N/A      | Primary key                                            |
| ReportDate       | DATETIME     | No       | N/A      | When error occurred                                    |
| UserName         | VARCHAR(100) | No       | 100      | Windows username (submitter)                           |
| MachineName      | VARCHAR(200) | Yes      | 200      | Computer name                                          |
| AppVersion       | VARCHAR(50)  | Yes      | 50       | Application version (e.g., "5.0.0.142")                |
| ErrorType        | VARCHAR(255) | Yes      | 255      | Exception type name                                    |
| ErrorSummary     | TEXT         | Yes      | 64KB     | **Full** user-friendly error message (not truncated)   |
| UserNotes        | TEXT         | Yes      | 64KB     | **User-provided context** (highlighted in detail view) |
| TechnicalDetails | TEXT         | Yes      | 64KB     | Full exception.ToString() output                       |
| CallStack        | TEXT         | Yes      | 64KB     | Complete stack trace                                   |
| Status           | VARCHAR(20)  | No       | 20       | 'New', 'Reviewed', 'Resolved'                          |
| ReviewedBy       | VARCHAR(100) | Yes      | 100      | Developer username who reviewed                        |
| ReviewedDate     | DATETIME     | Yes      | N/A      | When status changed from 'New'                         |
| DeveloperNotes   | TEXT         | Yes      | 64KB     | Developer comments/findings                            |

---

## SQL Logic

```sql
SELECT
    ReportID,
    ReportDate,
    UserName,
    MachineName,
    AppVersion,
    ErrorType,
    ErrorSummary,
    UserNotes,
    TechnicalDetails,
    CallStack,
    Status,
    ReviewedBy,
    ReviewedDate,
    DeveloperNotes
FROM error_reports
WHERE ReportID = p_ReportID;

-- Check if row exists
IF ROW_COUNT() = 0 THEN
    SET p_Status = -2;
    SET p_ErrorMsg = 'ReportID not found';
ELSE
    SET p_Status = 0;
    SET p_ErrorMsg = 'Error report retrieved successfully';
END IF;
```

**Performance Notes**:

-   Primary key lookup (ReportID) is very fast
-   All TEXT fields returned in full (may be 10KB+ each)
-   Consider connection timeout for large CallStack/TechnicalDetails fields

---

## C# Usage Example

```csharp
public static async Task<Model_Dao_Result<Model_ErrorReport_Core>> GetErrorReportByIdAsync(int reportId)
{
    try
    {
        string connectionString = Helper_Database_Variables.GetConnectionString();

        var parameters = new Dictionary<string, object>
        {
            ["ReportID"] = reportId
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            connectionString,
            "sp_error_reports_GetByID",
            parameters,
            progressHelper: null);

        if (!result.IsSuccess)
        {
            
            return Model_Dao_Result<Model_ErrorReport_Core>.Failure(result.StatusMessage);
        }

        if (result.Data == null || result.Data.Rows.Count == 0)
        {
            return Model_Dao_Result<Model_ErrorReport_Core>.Failure($"Error report {reportId} not found.");
        }

        // Map DataRow to Model_ErrorReport_Core
        DataRow row = result.Data.Rows[0];
        var report = new Model_ErrorReport_Core
        {
            ReportID = Convert.ToInt32(row["ReportID"]),
            ReportDate = Convert.ToDateTime(row["ReportDate"]),
            UserName = row["UserName"]?.ToString() ?? string.Empty,
            MachineName = row["MachineName"]?.ToString(),
            AppVersion = row["AppVersion"]?.ToString(),
            ErrorType = row["ErrorType"]?.ToString(),
            ErrorSummary = row["ErrorSummary"]?.ToString(),
            UserNotes = row["UserNotes"]?.ToString(),
            TechnicalDetails = row["TechnicalDetails"]?.ToString(),
            CallStack = row["CallStack"]?.ToString(),
            Status = row["Status"]?.ToString() ?? "New",
            ReviewedBy = row["ReviewedBy"]?.ToString(),
            ReviewedDate = row["ReviewedDate"] != DBNull.Value
                ? Convert.ToDateTime(row["ReviewedDate"])
                : (DateTime?)null,
            DeveloperNotes = row["DeveloperNotes"]?.ToString()
        };

        return Model_Dao_Result<Model_ErrorReport_Core>.Success(
            report,
            $"Retrieved error report {reportId}");
    }
    catch (Exception ex)
    {
        LoggingUtility.LogApplicationError(ex);
        return Model_Dao_Result<Model_ErrorReport_Core>.Failure(
            "An unexpected error occurred while retrieving error report.");
    }
}
```

---

## Test Cases

### Test Case 1: Valid ReportID

**Input**: ReportID=123 (exists in database)  
**Expected Output**: Single row with all 14 columns populated  
**Expected Status**: 0 (Success)

### Test Case 2: Invalid ReportID

**Input**: ReportID=99999 (does not exist)  
**Expected Output**: Empty DataTable  
**Expected Status**: -2 (ReportID not found)

### Test Case 3: Report with NULL Optional Fields

**Input**: ReportID=456 (has NULL for MachineName, AppVersion, etc.)  
**Expected Output**: Single row with NULLs handled gracefully  
**Expected Status**: 0 (Success)  
**Note**: C# code must check for DBNull.Value

### Test Case 4: Report with Large TEXT Fields

**Input**: ReportID=789 (has 10KB CallStack)  
**Expected Output**: Single row with full CallStack returned  
**Expected Status**: 0 (Success)  
**Performance**: Should complete within 300ms

---

## Validation Rules

1. ReportID must be positive integer (validated in C# before call)
2. ReportID existence checked by stored procedure (returns -2 if not found)
3. All TEXT fields may be NULL - C# code must handle DBNull.Value
4. DateTime fields (ReportDate, ReviewedDate) should be validated for reasonable ranges

---

## Error Handling

-   **ReportID not found**: Returns p_Status=-2, p_ErrorMsg='ReportID not found', empty DataTable
-   **Database connection failure**: Returns p_Status=-1, p_ErrorMsg='Database error occurred'
-   **SQL syntax error**: Returns p_Status=-1, p_ErrorMsg with error details
-   **Success**: Returns p_Status=0, p_ErrorMsg='Error report retrieved successfully', single row

---

## Performance Benchmarks

**Target Performance** (from spec.md):

-   Retrieve single report including 10KB CallStack: < 300ms

**Optimization Notes**:

-   Primary key index ensures fast lookup
-   No JOINs required (single table query)
-   Connection timeout sufficient for largest TEXT fields

---

## UI Display Notes

When displaying the retrieved report in the detail view:

1. **Highlight UserNotes section** - This is what the user provided and should be visually distinct
2. **Format CallStack** - Display in monospace font, consider scroll or "View Full" expandable section
3. **Format TechnicalDetails** - Similar to CallStack, consider expandable section
4. **Status-based button visibility**:
    - Status='New': Show "Mark as Reviewed" and "Mark as Resolved"
    - Status='Reviewed': Show "Mark as Resolved"
    - Status='Resolved': Show "Mark as Reviewed" (reopen)
5. **ReviewedBy/ReviewedDate** - Show only if populated (Status != 'New')

---

## Change History

| Date       | Version | Changes                     | Author            |
| ---------- | ------- | --------------------------- | ----------------- |
| 2025-10-25 | 1.0.0   | Initial contract definition | AI Planning Agent |
