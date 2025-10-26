# DAO Contract: Dao_ErrorReports (Extensions for Feature 002)

**Feature**: 002-view-error-reports  
**Purpose**: Define additional methods needed for View Error Reports window  
**Base**: Extends existing Dao_ErrorReports from Feature 001

---

## Method Contracts

### GetFilteredReportsAsync

**Purpose**: Retrieve paginated, filtered error reports

**Signature**:
```csharp
public static async Task<DaoResult<PaginatedResult<Model_ErrorReport>>> GetFilteredReportsAsync(
    ErrorReportFilterCriteria criteria,
    CancellationToken cancellationToken = default)
```

**Input**:
- `criteria`: Filter criteria (date range, users, statuses, search text, pagination)
- `cancellationToken`: Cancellation token for async operation

**Output**:
- `DaoResult<PaginatedResult<Model_ErrorReport>>` with:
  - `IsSuccess`: true if query executed successfully
  - `Data.Items`: List of matching error reports
  - `Data.TotalCount`: Total matching records (all pages)
  - `Data.PageNumber`: Current page number
  - `Data.PageSize`: Records per page
  - `Message`: Success message or error description

**Behavior**:
- Calls `sp_error_reports_GetAll` stored procedure
- Applies all filters (date, user, status, machine, search)
- Returns paginated results
- Handles database errors gracefully
- Logs exceptions via LoggingUtility

**Error Cases**:
- Invalid date range: Returns DaoResult.Failure with validation message
- Database connection failure: Returns DaoResult.Failure with error
- Empty result set: Returns DaoResult.Success with empty list

---

### GetUserListAsync

**Purpose**: Get distinct list of users who have submitted error reports

**Signature**:
```csharp
public static async Task<DaoResult<List<string>>> GetUserListAsync(
    CancellationToken cancellationToken = default)
```

**Output**:
- `DaoResult<List<string>>` with distinct UserName values, sorted alphabetically

**Behavior**:
- Calls `sp_error_reports_GetUserList` stored procedure
- Returns empty list if no reports exist
- Caches results for 5 minutes to reduce database calls

---

### GetMachineListAsync

**Purpose**: Get distinct list of machines that have submitted error reports

**Signature**:
```csharp
public static async Task<DaoResult<List<string>>> GetMachineListAsync(
    CancellationToken cancellationToken = default)
```

**Output**:
- `DaoResult<List<string>>` with distinct MachineName values, sorted alphabetically

**Behavior**:
- Calls `sp_error_reports_GetMachineList` stored procedure
- Returns empty list if no reports exist
- Caches results for 5 minutes to reduce database calls

---

### UpdateReportStatusAsync

**Purpose**: Change status of error report and add developer notes

**Signature**:
```csharp
public static async Task<DaoResult<Model_ErrorReport>> UpdateReportStatusAsync(
    int reportId,
    string newStatus,
    string reviewedBy,
    string? developerNotes,
    CancellationToken cancellationToken = default)
```

**Input**:
- `reportId`: ID of report to update
- `newStatus`: "New", "Reviewed", or "Resolved"
- `reviewedBy`: Username of developer making change
- `developerNotes`: Optional notes about status change

**Output**:
- `DaoResult<Model_ErrorReport>` with updated report data

**Behavior**:
- Calls `sp_error_reports_UpdateStatus` stored procedure
- Sets ReviewedDate to current timestamp
- Validates newStatus against allowed values
- Returns updated report with new status and ReviewedDate

**Error Cases**:
- Invalid reportId: Returns DaoResult.Failure("Report not found")
- Invalid newStatus: Returns DaoResult.Failure("Invalid status value")
- Database error: Returns DaoResult.Failure with exception details

---

### ExportToCSVAsync

**Purpose**: Export filtered reports to CSV file

**Signature**:
```csharp
public static async Task<DaoResult<string>> ExportToCSVAsync(
    ErrorReportFilterCriteria criteria,
    string outputPath,
    CancellationToken cancellationToken = default)
```

**Input**:
- `criteria`: Same filter criteria as GetFilteredReportsAsync (ignores pagination)
- `outputPath`: Full path to output CSV file

**Output**:
- `DaoResult<string>` with:
  - `Data`: Path to created CSV file
  - `Message`: "Exported X reports to {path}"

**Behavior**:
- Fetches all matching reports (no pagination limit)
- Writes to CSV using StreamWriter
- UTF-8 encoding with BOM for Excel compatibility
- Quotes fields containing commas or newlines
- Creates directory if doesn't exist

**Error Cases**:
- File path inaccessible: Returns DaoResult.Failure with IO exception
- Too many reports (>50,000): Returns DaoResult.Failure("Dataset too large, apply more filters")

---

### ExportToExcelAsync

**Purpose**: Export filtered reports to Excel (.xlsx) file

**Signature**:
```csharp
public static async Task<DaoResult<string>> ExportToExcelAsync(
    ErrorReportFilterCriteria criteria,
    string outputPath,
    CancellationToken cancellationToken = default)
```

**Input**:
- `criteria`: Same filter criteria as GetFilteredReportsAsync (ignores pagination)
- `outputPath`: Full path to output Excel file

**Output**:
- `DaoResult<string>` with:
  - `Data`: Path to created Excel file
  - `Message`: "Exported X reports to {path}"

**Behavior**:
- Fetches all matching reports (no pagination limit)
- Uses ClosedXML to create formatted Excel workbook
- Applies column formatting, freeze panes, auto-fit widths
- Sheet name: "Error Reports"
- Creates directory if doesn't exist

**Error Cases**:
- File path inaccessible: Returns DaoResult.Failure with IO exception
- ClosedXML exception: Returns DaoResult.Failure with library error
- Too many reports (>50,000): Returns DaoResult.Failure("Dataset too large, apply more filters")

---

## Supporting Classes

### ErrorReportFilterCriteria

```csharp
public class ErrorReportFilterCriteria
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<string> UserNames { get; set; } = new();
    public List<string> MachineNames { get; set; } = new();
    public List<string> Statuses { get; set; } = new();
    public string? SearchText { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public string? SortColumn { get; set; }
    public bool SortAscending { get; set; } = true;
    
    public bool IsValid()
    {
        if (DateFrom.HasValue && DateTo.HasValue && DateFrom > DateTo)
            return false;
        if (PageNumber < 1 || PageSize < 10 || PageSize > 500)
            return false;
        return true;
    }
}
```

### PaginatedResult<T>

```csharp
public class PaginatedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
```

---

## Error Handling Standards

All DAO methods MUST:
- Return `DaoResult<T>` (never throw for expected failures)
- Log exceptions via `LoggingUtility.LogApplicationError(ex)`
- Wrap database errors in DaoResult.Failure with user-friendly message
- Include exception in DaoResult for diagnostic purposes
- Check `IsSuccess` on stored procedure results before accessing data

---

## Constitution Compliance

✅ **Principle I**: All methods use stored procedures via Helper_Database_StoredProcedure  
✅ **Principle II**: All methods return DaoResult<T> wrapper  
✅ **Principle III**: Methods organized in #region Database Operations  
✅ **Principle VII**: All public methods have XML documentation  
✅ **Principle IX**: Async/await patterns used for all I/O operations

---

## Usage Examples

### Filtering Reports

```csharp
var criteria = new ErrorReportFilterCriteria
{
    DateFrom = DateTime.Today.AddDays(-7),
    DateTo = DateTime.Today,
    Statuses = new List<string> { "New", "Reviewed" },
    PageNumber = 1,
    PageSize = 100
};

var result = await Dao_ErrorReports.GetFilteredReportsAsync(criteria);
if (result.IsSuccess)
{
    foreach (var report in result.Data.Items)
    {
        // Process report
    }
    int totalPages = result.Data.TotalPages;
}
```

### Updating Status

```csharp
var result = await Dao_ErrorReports.UpdateReportStatusAsync(
    reportId: 123,
    newStatus: "Reviewed",
    reviewedBy: "jsmith",
    developerNotes: "Investigating database timeout issue"
);

if (result.IsSuccess)
{
    // Refresh grid with updated report
    var updatedReport = result.Data;
}
```

### Exporting Data

```csharp
var criteria = new ErrorReportFilterCriteria
{
    DateFrom = DateTime.Today.AddMonths(-1),
    Statuses = new List<string> { "New" }
};

var result = await Dao_ErrorReports.ExportToExcelAsync(
    criteria,
    @"C:\Exports\NewErrorReports_2025-10-25.xlsx"
);

if (result.IsSuccess)
{
    MessageBox.Show($"Exported to {result.Data}");
}
```
