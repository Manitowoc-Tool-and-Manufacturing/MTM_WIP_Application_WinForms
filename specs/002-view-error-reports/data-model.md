# Data Model: View Error Reports Window

**Feature**: 002-view-error-reports  
**Created**: 2025-10-25  
**Status**: Complete

---

## Entity Overview

This feature primarily **reads** from existing entities created in Feature 001. No new database tables are required.

---

## Core Entities

### Model_ErrorReport (Existing from Feature 001)

**Purpose**: Represents a single error report submitted by a user

**Source**: `error_reports` table via `Dao_ErrorReports`

**Properties**:
```csharp
public class Model_ErrorReport
{
    public int ReportID { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserName { get; set; }
    public string MachineName { get; set; }
    public string ApplicationVersion { get; set; }
    public string ErrorType { get; set; }
    public string ErrorSummary { get; set; }
    public string? UserNotes { get; set; }  // What user was doing
    public string? TechnicalDetails { get; set; }
    public string? CallStack { get; set; }
    public string Status { get; set; }  // "New", "Reviewed", "Resolved"
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public string? DeveloperNotes { get; set; }
}
```

**Validation Rules**:
- ReportID: Positive integer (primary key)
- Timestamp: Not in future
- UserName: Not null or empty
- ErrorType: Not null or empty
- ErrorSummary: Not null, max 5000 chars (truncated for grid)
- Status: Must be one of ("New", "Reviewed", "Resolved")

**State Transitions**:
```
New → Reviewed → Resolved
 ↑      ↓          ↓
 └──────┴──────────┘ (any direction allowed)
```

**Relationships**:
- Belongs to: User (UserName)
- Belongs to: Machine (MachineName)
- Audited by: Developer (ReviewedBy)

---

## View Models

### ErrorReportFilterCriteria

**Purpose**: Encapsulates user-selected filter options for database query

**Usage**: Passed to `Dao_ErrorReports.GetFilteredReportsAsync()`

**Properties**:
```csharp
public class ErrorReportFilterCriteria
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<string> UserNames { get; set; } = new();
    public List<string> MachineNames { get; set; } = new();
    public List<string> Statuses { get; set; } = new();  // "New", "Reviewed", "Resolved"
    public string? SearchText { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public string? SortColumn { get; set; }
    public bool SortAscending { get; set; } = true;
}
```

**Validation**:
- DateFrom ≤ DateTo (if both specified)
- PageNumber ≥ 1
- PageSize between 10 and 500
- At least one filter must be applied (prevent loading all reports)

---

### ErrorReportGridRow

**Purpose**: Flattened representation for DataGridView display

**Source**: Transformed from Model_ErrorReport in DAO

**Properties**:
```csharp
public class ErrorReportGridRow
{
    public int ReportID { get; set; }
    public string DisplayDate { get; set; }  // Formatted: "10/25 9:15 AM"
    public string UserName { get; set; }
    public string MachineName { get; set; }
    public string ErrorType { get; set; }
    public string SummaryTruncated { get; set; }  // First 100 chars
    public string Status { get; set; }
    public Color BackgroundColor { get; set; }  // Based on status
    
    // Hidden properties for detail view (not displayed in grid)
    public string FullSummary { get; set; }
    public string? UserNotes { get; set; }
    public string? TechnicalDetails { get; set; }
    public string? CallStack { get; set; }
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public string? DeveloperNotes { get; set; }
}
```

**Transformation Logic**:
```csharp
ErrorReportGridRow MapToGridRow(Model_ErrorReport report)
{
    return new ErrorReportGridRow
    {
        ReportID = report.ReportID,
        DisplayDate = report.Timestamp.ToString("MM/dd HH:mm"),
        UserName = report.UserName,
        MachineName = report.MachineName,
        ErrorType = report.ErrorType,
        SummaryTruncated = report.ErrorSummary.Length > 100 
            ? report.ErrorSummary.Substring(0, 97) + "..." 
            : report.ErrorSummary,
        Status = report.Status,
        BackgroundColor = GetStatusColor(report.Status),
        FullSummary = report.ErrorSummary,
        UserNotes = report.UserNotes,
        TechnicalDetails = report.TechnicalDetails,
        CallStack = report.CallStack,
        ReviewedBy = report.ReviewedBy,
        ReviewedDate = report.ReviewedDate,
        DeveloperNotes = report.DeveloperNotes
    };
}
```

---

### PaginatedResult<T>

**Purpose**: Wraps paginated data with metadata

**Usage**: Return type for paginated DAO queries

**Properties**:
```csharp
public class PaginatedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
```

---

## Database Schema (Reference Only - from Feature 001)

### error_reports Table

```sql
CREATE TABLE error_reports (
    ReportID INT AUTO_INCREMENT PRIMARY KEY,
    Timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UserName VARCHAR(100) NOT NULL,
    MachineName VARCHAR(200) NOT NULL,
    ApplicationVersion VARCHAR(50) NOT NULL,
    ErrorType VARCHAR(200) NOT NULL,
    ErrorSummary VARCHAR(5000) NOT NULL,
    UserNotes TEXT NULL,
    TechnicalDetails TEXT NULL,
    CallStack TEXT NULL,
    Status ENUM('New', 'Reviewed', 'Resolved') NOT NULL DEFAULT 'New',
    ReviewedBy VARCHAR(100) NULL,
    ReviewedDate DATETIME NULL,
    DeveloperNotes TEXT NULL,
    INDEX idx_timestamp (Timestamp),
    INDEX idx_username (UserName),
    INDEX idx_status (Status),
    INDEX idx_machinename (MachineName)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

**Indexes**:
- `idx_timestamp`: Optimizes date range filters
- `idx_username`: Optimizes user filter
- `idx_status`: Optimizes status filter
- `idx_machinename`: Optimizes machine filter

---

## Stored Procedure Interfaces (from Feature 001)

### sp_error_reports_GetAll

**Purpose**: Retrieve filtered and paginated error reports

**Parameters**:
```sql
IN  p_DateFrom DATETIME,
IN  p_DateTo DATETIME,
IN  p_UserNames TEXT,           -- Comma-separated list
IN  p_MachineNames TEXT,        -- Comma-separated list
IN  p_Statuses TEXT,            -- Comma-separated: "New,Reviewed"
IN  p_SearchText VARCHAR(500),
IN  p_PageNumber INT,
IN  p_PageSize INT,
IN  p_SortColumn VARCHAR(50),
IN  p_SortAscending BOOLEAN,
OUT p_Status INT,
OUT p_ErrorMsg VARCHAR(500)
```

**Returns**: ResultSet with all Model_ErrorReport columns + TotalCount

**Status Codes**:
- 0: Success
- 1: Success, no data found
- -1: Invalid parameters
- -2: Database error

---

### sp_error_reports_GetByID

**Purpose**: Retrieve single error report details

**Parameters**:
```sql
IN  p_ReportID INT,
OUT p_Status INT,
OUT p_ErrorMsg VARCHAR(500)
```

**Returns**: Single row with all Model_ErrorReport columns

---

### sp_error_reports_UpdateStatus

**Purpose**: Change report status and add developer notes

**Parameters**:
```sql
IN  p_ReportID INT,
IN  p_NewStatus VARCHAR(20),
IN  p_ReviewedBy VARCHAR(100),
IN  p_DeveloperNotes TEXT,
OUT p_Status INT,
OUT p_ErrorMsg VARCHAR(500)
```

**Side Effects**:
- Sets ReviewedDate = NOW()
- Logs audit trail entry
- Returns updated row

---

### sp_error_reports_GetUserList

**Purpose**: Populate user filter dropdown

**Returns**: Distinct UserName values with report counts

```sql
SELECT DISTINCT UserName, COUNT(*) as ReportCount
FROM error_reports
ORDER BY UserName;
```

---

### sp_error_reports_GetMachineList

**Purpose**: Populate machine filter dropdown

**Returns**: Distinct MachineName values with report counts

```sql
SELECT DISTINCT MachineName, COUNT(*) as ReportCount
FROM error_reports
ORDER BY MachineName;
```

---

## Data Flow Diagram

```
User Applies Filters
        ↓
FilterCriteria Built
        ↓
Dao_ErrorReports.GetFilteredReportsAsync(criteria)
        ↓
Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync()
        ↓
sp_error_reports_GetAll(@p_DateFrom, @p_DateTo, ...)
        ↓
MySQL Query Execution (indexed, optimized)
        ↓
DataTable Returned
        ↓
Map to List<Model_ErrorReport>
        ↓
Transform to List<ErrorReportGridRow>
        ↓
Wrap in PaginatedResult<ErrorReportGridRow>
        ↓
Return DaoResult<PaginatedResult<ErrorReportGridRow>>
        ↓
Bind to DataGridView
        ↓
Apply Row Coloring
        ↓
Display "Showing 1-100 of 10,243 reports"
```

---

## Export Data Model

### CSV Export Format

```csv
ReportID,Timestamp,User,Machine,ErrorType,Status,Summary,UserNotes,ReviewedBy,DeveloperNotes
123,"2025-10-25 09:15:33",jsmith,DESK-01,DBConnectionException,New,"Connection lost...","Saving transfer","",""
```

**Column Order**: ReportID, Timestamp, User, Machine, ErrorType, Status, Summary, UserNotes, ReviewedBy, DeveloperNotes

**Encoding**: UTF-8 with BOM for Excel compatibility

---

### Excel Export Format

**Sheet Name**: "Error Reports"

**Columns**:
| A | B | C | D | E | F | G | H | I | J |
|---|---|---|---|---|---|---|---|---|---|
| ID | Date/Time | User | Machine | Error Type | Status | Summary | User Notes | Reviewed By | Developer Notes |

**Formatting**:
- Header row: Bold, background color
- Date column: `yyyy-MM-dd HH:mm:ss` format
- Summary: Text wrap enabled
- Freeze top row
- Auto-fit column widths

---

## State Management

### Form-Level State

```csharp
private ErrorReportFilterCriteria _currentFilters;
private PaginatedResult<ErrorReportGridRow> _currentPage;
private ErrorReportGridRow? _selectedReport;
private bool _isLoadingData;
```

### User Preferences (Persisted)

```csharp
public class ErrorReportViewerPreferences
{
    public DateTime? LastDateFrom { get; set; }
    public DateTime? LastDateTo { get; set; }
    public List<string> LastUserNames { get; set; }
    public List<string> LastStatuses { get; set; }
    public int PreferredPageSize { get; set; } = 100;
    public string? LastSortColumn { get; set; }
    public bool LastSortAscending { get; set; } = true;
}
```

**Persistence**: Serialized to JSON, stored in user profile directory

---

## Performance Considerations

### Database Query Optimization

- **Indexes**: All filter columns indexed for fast lookups
- **Pagination**: LIMIT/OFFSET in stored procedure reduces network transfer
- **Selective columns**: Only fetch columns needed for current view
- **Connection pooling**: Reuse connections via Helper_Database_Variables

### Memory Management

- **Paginated loading**: Never load >500 rows into memory
- **Lazy detail loading**: Technical details and call stacks loaded on-demand
- **DataTable disposal**: Dispose after mapping to prevent memory leaks
- **Grid virtualization**: WinForms DataGridView uses built-in row virtualization

---

## Validation Rules Summary

| Field | Validation |
|-------|------------|
| DateFrom/DateTo | DateFrom ≤ DateTo, not in future |
| UserNames | Must exist in database (validated by dropdown) |
| MachineNames | Must exist in database (validated by dropdown) |
| Statuses | Must be "New", "Reviewed", or "Resolved" |
| SearchText | Max 500 characters, no SQL injection (parameterized) |
| PageNumber | ≥ 1, ≤ TotalPages |
| PageSize | Between 10 and 500 |
| DeveloperNotes | Max 5000 characters (on status update) |

---

## Next Steps

1. Create plan.md with implementation architecture
2. Define DAO interface contracts in /contracts
3. Generate quickstart.md for development workflow
4. Update agent context with new entities and patterns
