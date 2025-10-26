# Data Model: View Error Reports Window

**Feature**: View Error Reports Window  
**Created**: 2025-10-25  
**Status**: Complete

## Entity Definitions

### 1. ErrorReportGridRow (View Model)

**Purpose**: Represents a single row displayed in the DataGridView grid.

**Source**: Populated from `sp_error_reports_GetAll` stored procedure result set.

**Fields**:

| Field Name | Type | Nullable | Description | Display Format |
|------------|------|----------|-------------|----------------|
| ReportID | int | No | Primary key, auto-increment | Plain integer |
| DisplayDate | DateTime | No | ReportDate from database | "yyyy-MM-dd HH:mm:ss" |
| UserName | string | No | Windows username who submitted report | Truncate to 20 chars |
| MachineName | string | Yes | Computer name where error occurred | Truncate to 15 chars, "(none)" if NULL |
| ErrorType | string | Yes | Exception type name | Truncate to 30 chars |
| SummaryTruncated | string | Yes | First 100 chars of ErrorSummary | Add "..." if truncated |
| Status | string | No | Current status: "New", "Reviewed", "Resolved" | Plain text, color-coded |

**Validation Rules**:
- ReportID must be > 0
- DisplayDate cannot be in future (sanity check)
- Status must be one of: "New", "Reviewed", "Resolved"
- Truncation preserves whole words when possible

**State Transitions**: N/A (read-only view model)

**Relationships**:
- One-to-one with ErrorReportDetail (via ReportID)
- Source table: error_reports

**Example Row**:
```csharp
{
    ReportID = 123,
    DisplayDate = DateTime.Parse("2025-10-25 09:15:33"),
    UserName = "John.Smith",
    MachineName = "DESK-01",
    ErrorType = "DatabaseConnectionException",
    SummaryTruncated = "Connection to database server lost unexpectedly during transaction commit...",
    Status = "New"
}
```

---

### 2. ErrorReportDetail (Complete Report)

**Purpose**: Represents the complete error report with all fields for detail view display.

**Source**: Populated from `sp_error_reports_GetByID` stored procedure result.

**Fields**:

| Field Name | Type | Nullable | Max Length | Description |
|------------|------|----------|------------|-------------|
| ReportID | int | No | N/A | Primary key |
| ReportDate | DateTime | No | N/A | When error occurred |
| UserName | string | No | 100 | Windows username |
| MachineName | string | Yes | 200 | Computer name |
| AppVersion | string | Yes | 50 | Application version (e.g., "5.0.0.142") |
| ErrorType | string | Yes | 255 | Exception type (e.g., "NullReferenceException") |
| ErrorSummary | string | Yes | 64KB (TEXT) | User-friendly error message |
| **UserNotes** | string | Yes | 64KB (TEXT) | **User-provided context** (highlighted in UI) |
| TechnicalDetails | string | Yes | 64KB (TEXT) | Full exception.ToString() output |
| CallStack | string | Yes | 64KB (TEXT) | Complete stack trace |
| Status | string | No | 20 | "New", "Reviewed", "Resolved" |
| ReviewedBy | string | Yes | 100 | Developer username who reviewed |
| ReviewedDate | DateTime | Yes | N/A | When status changed from "New" |
| DeveloperNotes | string | Yes | 64KB (TEXT) | Developer comments/findings |

**Validation Rules**:
- ReportID must exist in database
- Status must be valid enum value
- If Status is "Reviewed" or "Resolved", ReviewedBy and ReviewedDate should be populated
- ReviewedDate cannot be before ReportDate
- DeveloperNotes only editable by developers (not end users)

**State Transitions**:
```
New → Reviewed (on "Mark as Reviewed" button)
New → Resolved (on "Mark as Resolved" button)
Reviewed → Resolved (on "Mark as Resolved" button)
Resolved → Reviewed (on "Mark as Reviewed" button - reopen case)
```

**Relationships**:
- Belongs to error_reports table
- Has one ErrorReportGridRow view representation

**Example Instance**:
```csharp
{
    ReportID = 123,
    ReportDate = DateTime.Parse("2025-10-25 09:15:33"),
    UserName = "John.Smith",
    MachineName = "DESK-01",
    AppVersion = "5.0.0.142",
    ErrorType = "DatabaseConnectionException",
    ErrorSummary = "Connection to database server lost unexpectedly during transaction commit. Operation was rolled back.",
    UserNotes = "I was clicking the Save button after entering a transfer transaction. The screen froze for about 30 seconds, then showed this error.",
    TechnicalDetails = "MySql.Data.MySqlClient.MySqlException (0x80004005): Fatal error encountered during command execution.\n ---> System.TimeoutException: Timeout expired...",
    CallStack = "   at MySql.Data.MySqlClient.MySqlCommand.ExecuteNonQuery()\n   at MTM.Data.Dao_Transactions.SaveTransferAsync(...)",
    Status = "New",
    ReviewedBy = null,
    ReviewedDate = null,
    DeveloperNotes = null
}
```

---

### 3. ErrorReportFilter (Filter Criteria)

**Purpose**: Encapsulates filter parameters for querying error reports.

**Source**: Populated from UI filter controls (ComboBoxes, DateTimePickers, TextBox).

**Fields**:

| Field Name | Type | Nullable | Description | Default Value |
|------------|------|----------|-------------|---------------|
| DateFrom | DateTime | Yes | Start of date range filter | null (no date filter) |
| DateTo | DateTime | Yes | End of date range filter | null (no date filter) |
| UserName | string | Yes | Filter by specific user | null (all users) |
| MachineName | string | Yes | Filter by specific machine | null (all machines) |
| Status | string | Yes | Filter by status: "New", "Reviewed", "Resolved", or null | null (all statuses) |
| SearchText | string | Yes | Search term for ErrorSummary, UserNotes, TechnicalDetails | null (no search) |

**Validation Rules**:
- DateFrom cannot be after DateTo
- DateTo defaults to end-of-day (23:59:59) when specified
- UserName must exist in dropdown list or be null
- MachineName must exist in dropdown list or be null
- Status must be valid enum value or null
- SearchText is case-insensitive, minimum 3 characters when specified

**State Transitions**: N/A (value object)

**Relationships**:
- Passed to `sp_error_reports_GetAll` stored procedure as parameters
- Bound to UI filter panel controls

**Example Instance**:
```csharp
{
    DateFrom = DateTime.Parse("2025-10-18 00:00:00"),
    DateTo = DateTime.Parse("2025-10-25 23:59:59"),
    UserName = null,
    MachineName = null,
    Status = "New",
    SearchText = "database"
}
```

---

### 4. ErrorReportExport (Export Configuration)

**Purpose**: Configuration object for CSV/Excel export operations.

**Source**: Created based on user's export choices.

**Fields**:

| Field Name | Type | Nullable | Description |
|------------|------|----------|-------------|
| ExportFormat | ExportFormat enum | No | CSV or Excel |
| FilePath | string | No | User-selected save path from SaveFileDialog |
| DataSource | DataTable | No | Grid data to export (filtered results) |
| IncludeHeaders | bool | No | Include column headers in export |
| SelectedRowsOnly | bool | No | Export only selected rows vs all filtered results |

**Validation Rules**:
- FilePath must be writable
- FilePath extension must match ExportFormat (.csv or .xlsx)
- DataSource cannot be null or empty
- If SelectedRowsOnly is true, at least one row must be selected

**State Transitions**: N/A (value object)

**Relationships**:
- Used by Helper_ErrorReportExport.ExportAsync() method

**Example Instance**:
```csharp
{
    ExportFormat = ExportFormat.CSV,
    FilePath = @"C:\Reports\ErrorReports_2025-10-25.csv",
    DataSource = filteredDataTable,
    IncludeHeaders = true,
    SelectedRowsOnly = false
}
```

---

## Supporting Enumerations

### ExportFormat

```csharp
public enum ExportFormat
{
    CSV = 1,
    Excel = 2
}
```

### ReportStatus

```csharp
public enum ReportStatus
{
    New,
    Reviewed,
    Resolved
}
```

**Note**: Status is stored as VARCHAR in database but should be treated as enum in application code for type safety.

---

## Data Flow Diagrams

### Grid Population Flow

```
User Action: Open Window / Apply Filters
    ↓
UI Layer: Collect filter criteria → ErrorReportFilter
    ↓
Presentation: Call Dao_ErrorReports.GetAllErrorReportsAsync(filter)
    ↓
DAO Layer: Build parameters Dictionary, call sp_error_reports_GetAll
    ↓
Database: Execute stored procedure with WHERE filters, return DataTable
    ↓
DAO Layer: Wrap DataTable in DaoResult<DataTable>
    ↓
Presentation: Check IsSuccess, bind DataTable to DataGridView.DataSource
    ↓
UI Layer: Apply color-coding via CellFormatting event
    ↓
Display: Grid shows ErrorReportGridRow representations
```

### Detail View Flow

```
User Action: Double-click grid row or select + View Details button
    ↓
UI Layer: Extract ReportID from selected row
    ↓
Presentation: Call Dao_ErrorReports.GetErrorReportByIdAsync(reportId)
    ↓
DAO Layer: Call sp_error_reports_GetByID with ReportID parameter
    ↓
Database: Return single row with all fields
    ↓
DAO Layer: Map DataRow to ErrorReportDetail, wrap in DaoResult<ErrorReportDetail>
    ↓
Presentation: Check IsSuccess, populate detail panel controls
    ↓
UI Layer: Highlight UserNotes section, enable status change buttons
    ↓
Display: Detail panel shows complete ErrorReportDetail
```

### Status Update Flow

```
User Action: Click "Mark as Reviewed" button
    ↓
UI Layer: Show confirmation dialog with DeveloperNotes input
    ↓
User Action: Confirm and provide notes
    ↓
Presentation: Call Dao_ErrorReports.UpdateErrorReportStatusAsync(reportId, newStatus, notes)
    ↓
DAO Layer: Build parameters (ReportID, NewStatus, DeveloperNotes, ReviewedBy, ReviewedDate)
    ↓
DAO Layer: Call sp_error_reports_UpdateStatus
    ↓
Database: UPDATE error_reports SET Status=?, ReviewedBy=?, ReviewedDate=NOW(), DeveloperNotes=?
    ↓
DAO Layer: Wrap success/failure in DaoResult<bool>
    ↓
Presentation: Check IsSuccess, refresh grid and detail view
    ↓
UI Layer: Show success message, update row color-coding
    ↓
Display: Grid and detail reflect new Status
```

---

## Database Schema Reference

### error_reports Table (from error-reporting-system.md)

```sql
CREATE TABLE error_reports (
    ReportID INT AUTO_INCREMENT PRIMARY KEY,
    ReportDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UserName VARCHAR(100) NOT NULL,
    MachineName VARCHAR(200),
    AppVersion VARCHAR(50),
    ErrorType VARCHAR(255),
    ErrorSummary TEXT,
    UserNotes TEXT,
    TechnicalDetails TEXT,
    CallStack TEXT,
    Status VARCHAR(20) NOT NULL DEFAULT 'New',
    ReviewedBy VARCHAR(100),
    ReviewedDate DATETIME,
    DeveloperNotes TEXT,
    INDEX idx_status (Status),
    INDEX idx_reportdate (ReportDate),
    INDEX idx_username (UserName),
    INDEX idx_machinename (MachineName)
);
```

**Indexes Support**:
- `idx_status` - Fast filtering by Status
- `idx_reportdate` - Date range filtering
- `idx_username` - User dropdown population and filtering
- `idx_machinename` - Machine dropdown population and filtering

---

## Model Classes Mapping

### Existing Models (from Models folder)

**Model_ErrorReport.cs** - Already exists, maps to error_reports table:
```csharp
public class Model_ErrorReport
{
    public int ReportID { get; set; }
    public DateTime ReportDate { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? MachineName { get; set; }
    public string? AppVersion { get; set; }
    public string? ErrorType { get; set; }
    public string? ErrorSummary { get; set; }
    public string? UserNotes { get; set; }
    public string? TechnicalDetails { get; set; }
    public string? CallStack { get; set; }
    public string Status { get; set; } = "New";
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public string? DeveloperNotes { get; set; }
}
```

**Usage**: Model_ErrorReport serves as ErrorReportDetail. No new model needed.

### New Models Required

**Model_ErrorReportFilter.cs** - Filter criteria:
```csharp
public class Model_ErrorReportFilter
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? UserName { get; set; }
    public string? MachineName { get; set; }
    public string? Status { get; set; }
    public string? SearchText { get; set; }
}
```

**Note**: ErrorReportGridRow does not need a separate model class - it's represented directly by DataTable columns in the grid binding.

---

## Validation Summary

### Grid Row Validation (UI Layer)
- Color-coding applied based on Status column value
- Truncation logic in DataGridView CellFormatting event
- No edits allowed directly in grid

### Detail View Validation (UI Layer)
- All fields read-only except DeveloperNotes (when changing status)
- Status change buttons enabled/disabled based on current Status
- Confirmation required before status change

### Filter Validation (UI Layer)
- Date range validation: From <= To
- Search text minimum length: 3 characters
- Status dropdown restricted to valid enum values

### DAO Validation (Data Layer)
- ReportID existence check before GetById
- DaoResult pattern ensures no unhandled database errors
- Parameter null-safety with DBNull.Value conversions
