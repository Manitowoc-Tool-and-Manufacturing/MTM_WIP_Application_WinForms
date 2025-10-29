# Data Model: Transaction Viewer Form

**Feature**: Transaction Viewer Form Redesign  
**Date**: 2025-10-29  
**Status**: Complete

## Overview

This document defines the data models, entities, and data flow for the Transaction Viewer Form redesign. The feature reuses existing `Model_Transactions` from the database schema and introduces new models for search criteria, ViewModel state, and pagination.

---

## Entity Definitions

### 1. Model_Transactions (EXISTING - No Changes)

**Location**: `Models/Model_Transactions.cs`  
**Purpose**: Represents a single transaction record from the `inv_transaction` table  
**Source**: Database table via stored procedures

```csharp
/// <summary>
/// Represents a transaction record in the inventory system.
/// </summary>
internal class Model_Transactions
{
    /// <summary>
    /// Gets or sets the unique transaction ID.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Gets or sets the transaction type (IN/OUT/TRANSFER).
    /// </summary>
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Gets or sets the batch number associated with this transaction.
    /// </summary>
    public string? BatchNumber { get; set; }

    /// <summary>
    /// Gets or sets the part ID (part number).
    /// </summary>
    public string? PartID { get; set; }

    /// <summary>
    /// Gets or sets the source location (for IN and TRANSFER).
    /// </summary>
    public string? FromLocation { get; set; }

    /// <summary>
    /// Gets or sets the destination location (for TRANSFER only).
    /// </summary>
    public string? ToLocation { get; set; }

    /// <summary>
    /// Gets or sets the operation number (manufacturing routing step).
    /// </summary>
    public string? Operation { get; set; }

    /// <summary>
    /// Gets or sets the transaction quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets optional notes associated with the transaction.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the username who created the transaction.
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Gets or sets the item type (WIP, FG, RM, etc.).
    /// </summary>
    public string? ItemType { get; set; }

    /// <summary>
    /// Gets or sets the transaction date and time.
    /// </summary>
    public DateTime DateTime { get; set; }
}
```

**Validation Rules**:
- `ID`: Unique, auto-increment (assigned by database)
- `TransactionType`: Must be IN, OUT, or TRANSFER
- `PartID`: Required, max 300 characters
- `Quantity`: Required, must be > 0
- `User`: Required, max 100 characters
- `DateTime`: Defaults to current timestamp if not specified

**Relationships**:
- `BatchNumber` → Multiple transactions can share same batch
- `PartID` → Foreign key to `md_part_ids` table (not enforced at DB level)
- `User` → Foreign key to `usr_users` table (not enforced at DB level)

---

### 2. TransactionType (EXISTING - Enum)

**Location**: `Models/Model_Transactions.cs` (same file)  
**Purpose**: Defines the three transaction types in the system

```csharp
/// <summary>
/// Defines the types of inventory transactions.
/// </summary>
internal enum TransactionType
{
    /// <summary>
    /// Receiving inventory into the system.
    /// </summary>
    IN,

    /// <summary>
    /// Removing inventory from the system.
    /// </summary>
    OUT,

    /// <summary>
    /// Moving inventory between locations.
    /// </summary>
    TRANSFER
}
```

**Usage**:
- IN: Incoming shipments, returned items, production output
- OUT: Shipments, scrap, consumption, adjustments (decrease)
- TRANSFER: Internal movements between operations or locations

---

### 3. TransactionSearchCriteria (NEW)

**Location**: `Models/TransactionSearchCriteria.cs`  
**Purpose**: Encapsulates user-specified search filters for transaction queries  
**Used By**: TransactionSearchControl, TransactionViewModel, Dao_Transactions

```csharp
/// <summary>
/// Encapsulates search criteria for transaction queries.
/// </summary>
internal class TransactionSearchCriteria
{
    /// <summary>
    /// Gets or sets the part number to filter by.
    /// </summary>
    public string? PartID { get; set; }

    /// <summary>
    /// Gets or sets the username to filter by.
    /// </summary>
    public string? User { get; set; }

    /// <summary>
    /// Gets or sets the source location to filter by.
    /// </summary>
    public string? FromLocation { get; set; }

    /// <summary>
    /// Gets or sets the destination location to filter by (TRANSFER only).
    /// </summary>
    public string? ToLocation { get; set; }

    /// <summary>
    /// Gets or sets the operation number to filter by.
    /// </summary>
    public string? Operation { get; set; }

    /// <summary>
    /// Gets or sets the transaction type to filter by. Null = all types.
    /// </summary>
    public TransactionType? TransactionType { get; set; }

    /// <summary>
    /// Gets or sets the start date for the date range filter.
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Gets or sets the end date for the date range filter.
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Gets or sets the notes text to search for (partial match).
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Validates that at least one search criterion is specified.
    /// </summary>
    /// <returns>True if at least one criterion is specified; otherwise false.</returns>
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(PartID) ||
               !string.IsNullOrWhiteSpace(User) ||
               !string.IsNullOrWhiteSpace(FromLocation) ||
               !string.IsNullOrWhiteSpace(ToLocation) ||
               !string.IsNullOrWhiteSpace(Operation) ||
               TransactionType.HasValue ||
               DateFrom.HasValue ||
               DateTo.HasValue ||
               !string.IsNullOrWhiteSpace(Notes);
    }

    /// <summary>
    /// Validates that the date range logic is correct.
    /// </summary>
    /// <returns>True if date range is valid or not specified; otherwise false.</returns>
    public bool IsDateRangeValid()
    {
        if (!DateFrom.HasValue || !DateTo.HasValue)
            return true;

        return DateFrom.Value <= DateTo.Value;
    }

    /// <summary>
    /// Gets a human-readable summary of the search criteria.
    /// </summary>
    /// <returns>A string describing the active filters.</returns>
    public override string ToString()
    {
        var filters = new List<string>();
        
        if (!string.IsNullOrWhiteSpace(PartID))
            filters.Add($"Part: {PartID}");
        if (!string.IsNullOrWhiteSpace(User))
            filters.Add($"User: {User}");
        if (!string.IsNullOrWhiteSpace(FromLocation))
            filters.Add($"From: {FromLocation}");
        if (!string.IsNullOrWhiteSpace(ToLocation))
            filters.Add($"To: {ToLocation}");
        if (!string.IsNullOrWhiteSpace(Operation))
            filters.Add($"Op: {Operation}");
        if (TransactionType.HasValue)
            filters.Add($"Type: {TransactionType}");
        if (DateFrom.HasValue && DateTo.HasValue)
            filters.Add($"Date: {DateFrom:MM/dd/yy} - {DateTo:MM/dd/yy}");
        else if (DateFrom.HasValue)
            filters.Add($"From: {DateFrom:MM/dd/yy}");
        else if (DateTo.HasValue)
            filters.Add($"To: {DateTo:MM/dd/yy}");
        if (!string.IsNullOrWhiteSpace(Notes))
            filters.Add($"Notes: {Notes}");
        
        return filters.Count > 0 ? string.Join(" | ", filters) : "No filters";
    }
}
```

**Validation Rules**:
- At least one criterion must be specified (`IsValid()` returns false if all null/empty)
- `DateFrom` must be <= `DateTo` if both specified
- String fields trimmed before storage
- Null values represent "no filter" for that criterion

**Default Values**:
- All properties default to null (no filtering)
- Quick filters populate DateFrom/DateTo:
  - Today: DateFrom = Today 00:00, DateTo = Today 23:59
  - This Week: DateFrom = Monday, DateTo = Sunday
  - This Month: DateFrom = 1st of month, DateTo = last day of month

---

### 4. TransactionSearchResult (NEW)

**Location**: `Models/TransactionSearchResult.cs`  
**Purpose**: Wraps search results with pagination metadata  
**Used By**: TransactionViewModel, TransactionGridControl

```csharp
/// <summary>
/// Represents the result of a transaction search operation with pagination metadata.
/// </summary>
internal class TransactionSearchResult
{
    /// <summary>
    /// Gets or sets the list of transactions matching the search criteria.
    /// </summary>
    public List<Model_Transactions> Transactions { get; set; } = new();

    /// <summary>
    /// Gets or sets the total number of records matching the criteria (across all pages).
    /// </summary>
    public int TotalRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the current page number (1-indexed).
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the number of records per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets the total number of pages based on TotalRecordCount and PageSize.
    /// </summary>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalRecordCount / PageSize) : 0;

    /// <summary>
    /// Gets whether there is a previous page available.
    /// </summary>
    public bool HasPreviousPage => CurrentPage > 1;

    /// <summary>
    /// Gets whether there is a next page available.
    /// </summary>
    public bool HasNextPage => CurrentPage < TotalPages;

    /// <summary>
    /// Gets a human-readable summary of the pagination state.
    /// </summary>
    public string PaginationSummary => $"Page {CurrentPage} of {TotalPages} ({TotalRecordCount} total records)";
}
```

**Calculation Logic**:
- `TotalPages` = Ceiling(TotalRecordCount / PageSize)
- `HasPreviousPage` = CurrentPage > 1
- `HasNextPage` = CurrentPage < TotalPages

**Example**:
- TotalRecordCount = 245, PageSize = 50 → TotalPages = 5
- CurrentPage = 1 → HasPreviousPage = false, HasNextPage = true
- CurrentPage = 5 → HasPreviousPage = true, HasNextPage = false

---

### 5. TransactionAnalytics (NEW)

**Location**: `Models/TransactionAnalytics.cs`  
**Purpose**: Represents summary statistics for analytics display  
**Used By**: TransactionViewModel (Priority 3 feature)

```csharp
/// <summary>
/// Represents summary analytics for transactions within a date range.
/// </summary>
internal class TransactionAnalytics
{
    /// <summary>
    /// Gets or sets the total number of transactions.
    /// </summary>
    public int TotalTransactions { get; set; }

    /// <summary>
    /// Gets or sets the count of IN transactions.
    /// </summary>
    public int TotalIN { get; set; }

    /// <summary>
    /// Gets or sets the count of OUT transactions.
    /// </summary>
    public int TotalOUT { get; set; }

    /// <summary>
    /// Gets or sets the count of TRANSFER transactions.
    /// </summary>
    public int TotalTRANSFER { get; set; }

    /// <summary>
    /// Gets the percentage of IN transactions.
    /// </summary>
    public double PercentageIN => TotalTransactions > 0 ? (double)TotalIN / TotalTransactions * 100 : 0;

    /// <summary>
    /// Gets the percentage of OUT transactions.
    /// </summary>
    public double PercentageOUT => TotalTransactions > 0 ? (double)TotalOUT / TotalTransactions * 100 : 0;

    /// <summary>
    /// Gets the percentage of TRANSFER transactions.
    /// </summary>
    public double PercentageTRANSFER => TotalTransactions > 0 ? (double)TotalTRANSFER / TotalTransactions * 100 : 0;

    /// <summary>
    /// Gets or sets the date range for these analytics.
    /// </summary>
    public (DateTime From, DateTime To) DateRange { get; set; }
}
```

**Usage** (Priority 3):
- Displayed in analytics cards on Analytics tab
- Calculated by `inv_transactions_GetAnalytics` stored procedure
- Updated when date range filter changes

---

## Data Flow Diagrams

### Flow 1: User Initiates Search

```
┌─────────────┐
│   User      │
│ (Enter      │
│  Filters)   │
└──────┬──────┘
       │ clicks Search
       ▼
┌───────────────────────────┐
│ TransactionSearchControl  │
│ - Read UI controls        │
│ - Create criteria object  │
│ - Validate inputs         │
└──────────┬────────────────┘
           │ raises SearchRequested event
           ▼
┌───────────────────────────┐
│  Transactions (Form)      │
│ - Receive event           │
│ - Pass to ViewModel       │
└──────────┬────────────────┘
           │ calls SearchTransactionsAsync(criteria)
           ▼
┌───────────────────────────┐
│ TransactionViewModel      │
│ - Validate criteria       │
│ - Show progress           │
│ - Call DAO                │
└──────────┬────────────────┘
           │ calls SearchAsync(criteria, page, pageSize)
           ▼
┌───────────────────────────┐
│   Dao_Transactions        │
│ - Map criteria → params   │
│ - Call stored procedure   │
│ - Wrap in DaoResult<T>    │
└──────────┬────────────────┘
           │ calls inv_transactions_Search
           ▼
┌───────────────────────────┐
│   MySQL Database          │
│ - Execute query           │
│ - Apply filters           │
│ - Paginate results        │
│ - Return DataTable        │
└──────────┬────────────────┘
           │ returns DaoResult<DataTable>
           ▼
┌───────────────────────────┐
│   Dao_Transactions        │
│ - Map rows → Model        │
│ - Return DaoResult<List>  │
└──────────┬────────────────┘
           │ returns DaoResult<List<Model_Transactions>>
           ▼
┌───────────────────────────┐
│ TransactionViewModel      │
│ - Wrap in SearchResult    │
│ - Calculate pagination    │
│ - Show success            │
└──────────┬────────────────┘
           │ returns DaoResult<TransactionSearchResult>
           ▼
┌───────────────────────────┐
│  Transactions (Form)      │
│ - Check IsSuccess         │
│ - Pass to GridControl     │
└──────────┬────────────────┘
           │ calls DisplayResults(result.Data)
           ▼
┌───────────────────────────┐
│ TransactionGridControl    │
│ - Bind to DataGridView    │
│ - Update pagination UI    │
│ - Show record count       │
└───────────────────────────┘
```

### Flow 2: User Changes Page

```
┌─────────────┐
│   User      │
│ (Clicks     │
│  Next Page) │
└──────┬──────┘
       │ clicks btnNext
       ▼
┌───────────────────────────┐
│ TransactionGridControl    │
│ - Increment current page  │
│ - Raise PageChanged event │
└──────────┬────────────────┘
           │ raises PageChanged(newPage)
           ▼
┌───────────────────────────┐
│  Transactions (Form)      │
│ - Receive event           │
│ - Pass to ViewModel       │
└──────────┬────────────────┘
           │ calls SearchTransactionsAsync(criteria, newPage)
           ▼
[Same flow as Flow 1 from ViewModel onward, but with different page number]
```

### Flow 3: User Exports to Excel

```
┌─────────────┐
│   User      │
│ (Clicks     │
│  Export)    │
└──────┬──────┘
       │ clicks btnExport
       ▼
┌───────────────────────────┐
│  Transactions (Form)      │
│ - Show SaveFileDialog     │
│ - Get current results     │
└──────────┬────────────────┘
           │ calls ExportToExcelAsync(results, filePath)
           ▼
┌───────────────────────────┐
│ TransactionViewModel      │
│ - Show progress           │
│ - Create ClosedXML book   │
│ - Format headers          │
│ - Write data rows         │
│ - Save file               │
└──────────┬────────────────┘
           │ returns DaoResult<string> (filePath)
           ▼
┌───────────────────────────┐
│  Transactions (Form)      │
│ - Show success message    │
│ - Optionally open file    │
└───────────────────────────┘
```

---

## Database Schema Reference

### Table: inv_transaction

**Columns**:
- `ID` INT(11) AUTO_INCREMENT PRIMARY KEY
- `TransactionType` ENUM('IN', 'OUT', 'TRANSFER') NOT NULL
- `BatchNumber` VARCHAR(300)
- `PartID` VARCHAR(300) NOT NULL
- `FromLocation` VARCHAR(100)
- `ToLocation` VARCHAR(100)
- `Operation` VARCHAR(100)
- `Quantity` INT(11) NOT NULL
- `Notes` VARCHAR(1000)
- `User` VARCHAR(100) NOT NULL
- `ItemType` VARCHAR(100) NOT NULL DEFAULT 'WIP'
- `ReceiveDate` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP

**Indexes**:
- PRIMARY KEY (`ID`)
- INDEX `idx_partid` (`PartID`)
- INDEX `idx_user` (`User`)
- INDEX `idx_receivedate` (`ReceiveDate`)

**Current Record Count**: 24,122 transactions (as of 2025-10-24)

### Stored Procedures (Reference)

#### inv_transactions_Search

**Parameters**:
- IN `p_PartID` VARCHAR(300)
- IN `p_User` VARCHAR(100)
- IN `p_FromLocation` VARCHAR(100)
- IN `p_ToLocation` VARCHAR(100)
- IN `p_Operation` VARCHAR(100)
- IN `p_TransactionType` VARCHAR(20)
- IN `p_DateFrom` DATETIME
- IN `p_DateTo` DATETIME
- IN `p_Notes` TEXT
- IN `p_Page` INT
- IN `p_PageSize` INT
- OUT `p_Status` INT
- OUT `p_ErrorMsg` VARCHAR(500)

**Returns**: DataTable with transaction rows + output parameters

#### inv_transactions_SmartSearch

**Parameters**:
- IN `p_WhereClause` TEXT
- IN `p_Page` INT
- IN `p_PageSize` INT
- OUT `p_Status` INT
- OUT `p_ErrorMsg` VARCHAR(500)

**Returns**: DataTable with transaction rows + output parameters

#### inv_transactions_GetAnalytics

**Parameters**:
- IN `p_UserName` VARCHAR(100)
- IN `p_IsAdmin` TINYINT(1)
- IN `p_FromDate` DATETIME
- IN `p_ToDate` DATETIME
- OUT `p_Status` INT
- OUT `p_ErrorMsg` VARCHAR(500)

**Returns**: Single row with analytics columns (TotalTransactions, TotalIN, TotalOUT, TotalTRANSFER)

---

## State Management

### ViewModel State

**Stored in TransactionViewModel**:
- `CurrentSearchCriteria` (TransactionSearchCriteria) - Last executed search
- `CurrentResults` (TransactionSearchResult) - Last returned results
- `CurrentPage` (int) - Current pagination page
- `PageSize` (int) - Records per page (from Model_AppVariables)
- `CachedParts` (List<string>) - Part numbers for dropdown autocomplete
- `CachedUsers` (List<string>) - Usernames for dropdown
- `CachedLocations` (List<string>) - Location codes for dropdown

**State Transitions**:
1. **Initial State**: All properties null/empty, page = 1
2. **Search Requested**: Criteria populated, CurrentPage reset to 1
3. **Search Completed**: Results populated, TotalRecordCount updated
4. **Page Changed**: CurrentPage updated, new search triggered
5. **Criteria Changed**: CurrentPage reset to 1, new search triggered

### Form State

**Stored in Transactions.cs**:
- `_viewModel` (TransactionViewModel) - Business logic instance
- `_progressHelper` (Helper_StoredProcedureProgress) - Progress reporting
- `_isLoading` (bool) - Prevents duplicate searches during load

**UI State** (reflected in controls):
- Search filters (in TransactionSearchControl)
- Grid selection (in TransactionGridControl)
- Detail panel visibility (in TransactionDetailPanel)
- Pagination buttons enabled/disabled state

---

## Validation Rules Summary

### TransactionSearchCriteria
- ✅ At least one criterion must be specified
- ✅ DateFrom <= DateTo if both specified
- ✅ String fields max lengths: PartID (300), User (100), Location (100), Operation (100)

### Model_Transactions (enforced by database)
- ✅ ID unique, auto-increment
- ✅ TransactionType must be IN/OUT/TRANSFER
- ✅ PartID required, max 300 characters
- ✅ Quantity > 0
- ✅ User required, max 100 characters

### TransactionSearchResult
- ✅ CurrentPage >= 1 and <= TotalPages
- ✅ PageSize > 0
- ✅ TotalRecordCount >= 0
- ✅ Transactions list can be empty (no results)

---

## Performance Considerations

### Caching Strategy
- **Part numbers**: Loaded once on form load, cached in ViewModel, refreshed on demand
- **Users**: Loaded once, cached indefinitely (users rarely change)
- **Locations**: Loaded once, cached indefinitely (locations rarely change)
- **Search results**: NOT cached (always fresh from database)

### Pagination Optimization
- **Page size**: 50 records balances responsiveness and usability
- **Total count**: Calculated by stored procedure (COUNT(*) query with same filters)
- **Index usage**: Database indexes on PartID, User, ReceiveDate ensure fast queries

### Memory Management
- **DataTable disposal**: DataTable from stored procedure mapped to List<Model_Transactions>, then disposed
- **Large result sets**: Pagination prevents loading 24,000+ records into memory
- **Export operations**: Process rows in batches if export exceeds memory limits (future enhancement)

---

## Future Enhancements

### Priority 3 Features (Post-MVP)
- **Analytics dashboard**: Display TransactionAnalytics in tabbed interface
- **Batch history view**: Filter by BatchNumber and show full lifecycle
- **Advanced filters**: Combine multiple criteria with OR logic
- **Export all pages**: Option to export entire result set (not just current page)
- **Quick filters**: Saved filter presets (e.g., "My IN transactions this week")

### Data Model Extensions
- **TransactionBatch** (new entity): Group related transactions by BatchNumber
- **TransactionAudit** (new entity): Track who viewed transaction details (compliance requirement)
- **TransactionNote** (new entity): Separate notes table for long-form annotations

---

## Conclusion

The data model reuses existing `Model_Transactions` and introduces minimal new models (TransactionSearchCriteria, TransactionSearchResult, TransactionAnalytics) to support the SOLID architecture. All entities align with constitution principles, existing stored procedures remain unchanged, and validation rules ensure data integrity at every layer.

**Next Step**: Generate quickstart.md (developer setup and implementation guide).
