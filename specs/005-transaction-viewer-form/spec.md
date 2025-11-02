# Feature Specification: Transaction Viewer Form Redesign

**Feature ID**: F005  
**Status**: Draft  
**Created**: 2025-10-25  
**Last Updated**: 2025-11-01  
**Priority**: High  
**Complexity**: High  
**Architecture**: Clean, spec-compliant rewrite from scratch  

---

## Executive Summary

Complete architectural redesign of the Transactions form (`Forms/Transactions/Transactions.cs`) to create a maintainable, reliable, and spec-compliant transaction viewing system. The existing 2136-line implementation has grown organically and suffers from mixed concerns, inconsistent error handling, and complex state management. This specification defines a clean-slate rewrite following all MTM architectural patterns and spec-compliance standards.

**Goal**: Replace the existing transaction viewer with a modern, maintainable implementation that provides superior user experience while adhering to established architectural patterns.

**Database**: `inv_transaction` table with 24,000+ historical records  
**Stored Procedures**: `inv_transactions_Search`, `inv_transactions_SmartSearch`, `inv_transactions_GetAnalytics`

---

## Background and Current State Analysis

### Existing Implementation Issues

**Current File**: `Forms/Transactions/Transactions.cs` (2136 lines)

**Identified Problems**:
1. **Mixed Concerns**: Business logic, UI logic, and data access intermingled
2. **Inline SQL**: Direct MySqlCommand construction violates stored procedure mandate
3. **Inconsistent Error Handling**: Mix of MessageBox.Show and Service_ErrorHandler
4. **Complex State**: Multiple view modes, pagination, filtering all in one class
5. **Performance**: No async/await optimization, blocking operations
6. **Testing**: Impossible to unit test due to tight coupling
7. **Maintainability**: 2136 lines makes changes risky and time-consuming

**Current Features**:
- ✅ Smart search with debouncing
- ✅ Multiple view modes (Grid, Chart, Timeline)
- ✅ Advanced filtering (date, user, part, location, operation, type)
- ✅ Pagination (20 records/page)
- ✅ Export to Excel/CSV
- ✅ Print functionality
- ✅ Branch history viewing
- ✅ Side panel with details

---

## User Stories (Prioritized)

### Priority 1 (P1) - Core Viewing

**US-001**: As a manufacturing user, I want to view all my transactions so I can track my inventory activities.
- **Acceptance**: Grid displays transactions with all columns (ID, Type, Part, Locations, Quantity, Date, User)
- **Acceptance**: Transactions load within 2 seconds for last 30 days
- **Acceptance**: Latest transactions appear first (newest on top)

**US-002**: As a manufacturing user, I want to search transactions by part number so I can find specific item movements.
- **Acceptance**: Part search field with autocomplete dropdown
- **Acceptance**: Results appear within 1 second
- **Acceptance**: Empty search shows validation message

**US-003**: As a manufacturing user, I want to filter transactions by date range so I can review specific time periods.
- **Acceptance**: Date range picker with From/To dates
- **Acceptance**: Quick filters: Today, This Week, This Month, All
- **Acceptance**: Invalid date ranges show validation error

**US-004**: As a manufacturing user, I want to filter by transaction type (IN/OUT/TRANSFER) so I can focus on specific operations.
- **Acceptance**: Checkboxes for each transaction type
- **Acceptance**: At least one type must be selected
- **Acceptance**: Real-time filtering without re-clicking search

**US-005**: As an administrator, I want to view all users' transactions so I can audit system activity.
- **Acceptance**: User dropdown populated from active users
- **Acceptance**: Admin sees all users' data
- **Acceptance**: Regular users only see their own data

### Priority 2 (P2) - Advanced Features

**US-006**: As a manufacturing user, I want to search across multiple fields simultaneously so I can find complex transaction patterns.
- **Acceptance**: Combined search: Part + Location + User + Date + Notes
- **Acceptance**: Partial text matching on Notes field
- **Acceptance**: All filters apply together (AND logic)

**US-007**: As a manufacturing user, I want to navigate large result sets with pagination so the interface remains responsive.
- **Acceptance**: Page size: 50 records per page (configurable)
- **Acceptance**: Previous/Next buttons with page indicator
- **Acceptance**: Jump to specific page number
- **Acceptance**: Total record count displayed

**US-008**: As a manufacturing user, I want to export search results to Excel so I can analyze data offline.
- **Acceptance**: Export button respects current filters
- **Acceptance**: Excel file includes all visible columns
- **Acceptance**: Filename: `Transactions_[Date]_[User].xlsx`
- **Acceptance**: Export completes within 5 seconds for 1000 records (P95 latency)

**US-009**: As a manufacturing user, I want to print transaction reports so I can maintain physical records.
- **Acceptance**: Print preview with formatted layout
- **Acceptance**: Header shows filter criteria and date range
- **Acceptance**: Footer shows page numbers and totals

**US-010**: As a manufacturing user, I want to view transaction details in a side panel so I can see full information without navigating away.
- **Acceptance**: Click on row shows side panel
- **Acceptance**: Side panel displays all transaction fields
- **Acceptance**: Related transactions shown (same batch, same part)

### Priority 3 (P3) - Analytics & Visualization

**US-011**: As an administrator, I want to see transaction analytics so I can understand inventory patterns.
- **Acceptance**: Summary cards: Total Transactions, Total IN, Total OUT, Total TRANSFER
- **Acceptance**: Charts: Transactions by Type, Transactions Over Time
- **Acceptance**: Analytics respect current date filter

**US-012**: As a manufacturing user, I want to view the complete lifecycle of a batch with split visualization so I can trace how inventory moved through operations and locations.
- **Priority**: P1 (promoted from P3 - core feature)
- **Acceptance**: "Transaction Lifecycle" button on transaction detail panel
- **Acceptance**: Opens modal dialog showing TreeView with batch timeline
- **Acceptance**: TreeView displays splits as branches when TRANSFERs have partial quantities
- **Acceptance**: Shows full timeline from initial IN to current state(s)
- **Acceptance**: Detail panel on right updates when TreeView node selected
- **Acceptance**: Icon legend at bottom (📥 IN=Green, 🔄 TRANSFER=Blue, 📤 OUT=Red, 📦 Split=Orange)

---

## Functional Requirements

### FR-001: Architecture and Code Organization

**Requirement**: Implement clean separation of concerns following SOLID principles.

**Components**:
1. **Transactions.cs** (Form) - UI orchestration only
   - Event wiring
   - Control state management
   - Layout/theming
   - Navigation between child controls
   - **Max size**: 500 lines

2. **TransactionSearchControl.cs** (UserControl) - Search filters
   - Filter UI (date range, type checkboxes, dropdowns)
   - Validation logic
   - Filter state management
   - Raises SearchRequested event
   - **Max size**: 300 lines

3. **TransactionGridControl.cs** (UserControl) - Data grid display
   - DataGridView configuration
   - Column setup
   - Pagination controls
   - Selection handling
   - Raises RowSelected event
   - **Max size**: 300 lines

4. **TransactionDetailPanel.cs** (UserControl) - Side panel details
   - Read-only detail display
   - Related transaction links
   - Batch history navigation
   - **Max size**: 200 lines

5. **TransactionViewModel.cs** (ViewModel) - State and business logic
   - Filter criteria management
   - Pagination state
   - Search orchestration
   - Async operations
   - Error handling
   - **Max size**: 400 lines

**Validation**:
- ✅ Each file under maximum line count
- ✅ No business logic in form or controls
- ✅ All database calls route through ViewModel → DAO
- ✅ ViewMode testable without UI dependencies

### FR-002: Database Access Patterns

**Requirement**: All database operations must use stored procedures via Helper_Database_StoredProcedure.

**Stored Procedures to Use**:
1. **inv_transactions_Search** - Main search with filters
   - Parameters: `p_PartID`, `p_User`, `p_FromLocation`, `p_ToLocation`, `p_Operation`, `p_TransactionType`, `p_DateFrom`, `p_DateTo`, `p_Notes`, `p_Page`, `p_PageSize`
   - Returns: Transaction rows + `p_Status`, `p_ErrorMsg`

2. **inv_transactions_SmartSearch** - Quick search with WHERE clause builder
   - Parameters: `p_WhereClause`, `p_Page`, `p_PageSize`
   - Returns: Transaction rows + `p_Status`, `p_ErrorMsg`

3. **inv_transactions_GetAnalytics** - Summary statistics
   - Parameters: `p_UserName`, `p_IsAdmin`, `p_FromDate`, `p_ToDate`
   - Returns: Analytics data + `p_Status`, `p_ErrorMsg`

**Implementation Pattern**:
```csharp
// In Dao_Transactions.cs
public async Task<DaoResult<List<Model_Transactions>>> SearchAsync(
    TransactionSearchCriteria criteria,
    int page = 1,
    int pageSize = 50)
{
    var parameters = new Dictionary<string, object>
    {
        ["PartID"] = criteria.PartID ?? (object)DBNull.Value,
        ["User"] = criteria.User ?? (object)DBNull.Value,
        ["FromLocation"] = criteria.FromLocation ?? (object)DBNull.Value,
        ["TransactionType"] = criteria.TransactionType?.ToString() ?? (object)DBNull.Value,
        ["DateFrom"] = criteria.DateFrom ?? (object)DBNull.Value,
        ["DateTo"] = criteria.DateTo ?? (object)DBNull.Value,
        ["Notes"] = criteria.Notes ?? (object)DBNull.Value,
        ["Page"] = page,
        ["PageSize"] = pageSize
    };

    var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
        Helper_Database_Variables.GetConnectionString(),
        "inv_transactions_Search",
        parameters,
        useAsync: true
    );

    if (!result.IsSuccess || result.Data == null)
    {
        return DaoResult<List<Model_Transactions>>.Failure(result.StatusMessage);
    }

    var transactions = result.Data.AsEnumerable()
        .Select(MapDataRowToModel)
        .ToList();

    return DaoResult<List<Model_Transactions>>.Success(transactions);
}

// NO inline SQL - this is FORBIDDEN
// ❌ var query = $"SELECT * FROM inv_transaction WHERE PartID = '{partId}'";
```

**Validation**:
- ✅ `grep_search("SELECT.*FROM inv_transaction")` returns 0 results in Transactions code
- ✅ `grep_search("MySqlCommand")` returns 0 results in Transactions code
- ✅ All database calls use `Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync`
- ✅ All parameters passed via Dictionary<string, object>
- ✅ NO `p_` prefix in C# parameter names (added by helper)

### FR-003: Error Handling Standards

**Requirement**: All errors must route through Service_ErrorHandler.

**Patterns**:
```csharp
// Exception handling
try
{
    var result = await _viewModel.SearchTransactionsAsync(criteria);
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.HandleValidationError(result.ErrorMessage, "Transaction Search");
        return;
    }
    // Display results
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
        retryAction: async () => await SearchTransactionsAsync(criteria),
        controlName: nameof(Transactions));
}

// Validation errors
if (string.IsNullOrWhiteSpace(criteria.PartID) && 
    string.IsNullOrWhiteSpace(criteria.User) &&
    !criteria.DateFrom.HasValue)
{
    Service_ErrorHandler.HandleValidationError(
        "Please provide at least one search criterion.", 
        "Search Validation");
    return;
}

// Confirmation dialogs
var result = Service_ErrorHandler.ShowConfirmation(
    "Export may take several minutes. Continue?",
    "Export Confirmation");
if (!result) return;
```

**Validation**:
- ✅ `grep_search("MessageBox.Show")` returns 0 results in Transactions code
- ✅ All try/catch blocks use Service_ErrorHandler.HandleException
- ✅ Validation errors use Service_ErrorHandler.HandleValidationError
- ✅ User prompts use Service_ErrorHandler.ShowConfirmation

### FR-004: Async/Await Patterns

**Requirement**: All I/O operations must be asynchronous and non-blocking.

**Patterns**:
```csharp
// Form load
private async void Transactions_Load(object sender, EventArgs e)
{
    await InitializeAsync();
}

private async Task InitializeAsync()
{
    _progressHelper?.ShowProgress("Initializing...");
    
    await Task.WhenAll(
        LoadPartsDropdownAsync(),
        LoadUsersDropdownAsync(),
        LoadLocationsDropdownAsync()
    );
    
    await LoadDefaultTransactionsAsync();
    
    _progressHelper?.ShowSuccess("Ready");
}

// Search button
private async void btnSearch_Click(object sender, EventArgs e)
{
    await SearchTransactionsAsync();
}

private async Task SearchTransactionsAsync()
{
    btnSearch.Enabled = false;
    try
    {
        _progressHelper?.ShowProgress("Searching...");
        
        var criteria = BuildSearchCriteria();
        var result = await _viewModel.SearchTransactionsAsync(criteria);
        
        if (result.IsSuccess)
        {
            DisplayResults(result.Data);
            _progressHelper?.ShowSuccess($"Found {result.Data.Count} transactions");
        }
        else
        {
            Service_ErrorHandler.HandleValidationError(result.ErrorMessage, "Search");
        }
    }
    finally
    {
        btnSearch.Enabled = true;
    }
}

// NO blocking calls
// ❌ var result = dao.SearchTransactions().Result;
// ❌ Task.WaitAll(tasks);
```

**Validation**:
- ✅ All database methods use async/await
- ✅ All event handlers calling async methods are async void
- ✅ No `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` calls
- ✅ ConfigureAwait(false) used in ViewModel/DAO layers

### FR-005: Progress Reporting

**Requirement**: Long-running operations must show progress feedback.

**Implementation**:
```csharp
// In Transactions.cs (Form)
private Helper_StoredProcedureProgress? _progressHelper;

private void InitializeProgressReporting()
{
    _progressHelper = Helper_StoredProcedureProgress.Create(
        toolStripProgressBar,
        toolStripStatusLabel,
        this
    );
}

// In ViewModel
public async Task<DaoResult<List<Model_Transactions>>> SearchTransactionsAsync(
    TransactionSearchCriteria criteria,
    Helper_StoredProcedureProgress? progress = null)
{
    progress?.ShowProgress("Preparing search...");
    progress?.UpdateProgress(20, "Validating criteria...");
    
    var dao = new Dao_Transactions();
    progress?.UpdateProgress(40, "Executing database query...");
    
    var result = await dao.SearchAsync(criteria);
    progress?.UpdateProgress(80, "Processing results...");
    
    // Transform/validate results
    
    progress?.ShowSuccess($"Found {result.Data?.Count ?? 0} transactions");
    return result;
}
```

**Validation**:
- ✅ Progress helper initialized on form load
- ✅ All operations >1 second show progress
- ✅ Progress messages descriptive and user-friendly
- ✅ Success message shows result summary

### FR-006: Theme and DPI Scaling

**Requirement**: All controls must respect user theme and DPI settings.

**Implementation**:
```csharp
// In Transactions.cs
public Transactions()
{
    InitializeComponent();
    
    // Apply DPI scaling
    AutoScaleMode = AutoScaleMode.Dpi;
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    
    // Initialize child controls
    InitializeComponents();
    
    // Apply theme
    Core_Themes.ApplyTheme(this);
}

// In each UserControl
public TransactionSearchControl()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

**Validation**:
- ✅ All forms/controls call Core_Themes.ApplyDpiScaling in constructor
- ✅ AutoScaleMode = AutoScaleMode.Dpi set
- ✅ ApplyRuntimeLayoutAdjustments called after InitializeComponent
- ✅ UI renders correctly at 100%, 125%, 150%, 200% scaling

### FR-007: XML Documentation

**Requirement**: All public methods and classes must have XML documentation.

**Pattern**:
```csharp
/// <summary>
/// Searches for transactions matching the specified criteria.
/// </summary>
/// <param name="criteria">The search criteria including filters and pagination.</param>
/// <param name="cancellationToken">Cancellation token for async operation.</param>
/// <returns>
/// A <see cref="DaoResult{T}"/> containing the list of matching transactions
/// or an error message if the search failed.
/// </returns>
/// <exception cref="ArgumentNullException">Thrown when criteria is null.</exception>
/// <remarks>
/// This method calls the inv_transactions_Search stored procedure.
/// Results are limited by the PageSize property in the criteria.
/// </remarks>
public async Task<DaoResult<List<Model_Transactions>>> SearchTransactionsAsync(
    TransactionSearchCriteria criteria,
    CancellationToken cancellationToken = default)
{
    // Implementation
}
```

**Validation**:
- ✅ MCP tool: `check_xml_docs(source_dir="Forms/Transactions", min_coverage=95)`
- ✅ All public classes have <summary>
- ✅ All public methods have <summary>, <param>, <returns>
- ✅ Complex methods have <remarks> explaining usage

---

## Database Schema

### Table: inv_transaction

```sql
CREATE TABLE inv_transaction (
    ID INT(11) AUTO_INCREMENT PRIMARY KEY,
    TransactionType ENUM('IN', 'OUT', 'TRANSFER') NOT NULL,
    BatchNumber VARCHAR(300),
    PartID VARCHAR(300) NOT NULL,
    FromLocation VARCHAR(100),
    ToLocation VARCHAR(100),
    Operation VARCHAR(100),
    Quantity INT(11) NOT NULL,
    Notes VARCHAR(1000),
    User VARCHAR(100) NOT NULL,
    ItemType VARCHAR(100) NOT NULL DEFAULT 'WIP',
    ReceiveDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_partid (PartID),
    INDEX idx_user (User),
    INDEX idx_receivedate (ReceiveDate)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

**Current Record Count**: 24,122 transactions (as of 2025-10-24)

**Sample Data**:
```
ID: 40345
TransactionType: IN
BatchNumber: 0000021310
PartID: 21-28841-006
FromLocation: CS-04
ToLocation: NULL
Operation: 90
Quantity: 500
Notes: (empty)
User: JOHNK
ItemType: WIP
ReceiveDate: 2025-10-24 19:07:46
```

### Stored Procedures (Reference Only)

**Note**: These procedures are already implemented and tested. Specification documents their interface for reference.

#### inv_transactions_Search

```sql
CALL inv_transactions_Search(
    IN p_PartID VARCHAR(300),
    IN p_User VARCHAR(100),
    IN p_FromLocation VARCHAR(100),
    IN p_ToLocation VARCHAR(100),
    IN p_Operation VARCHAR(100),
    IN p_TransactionType VARCHAR(20),
    IN p_DateFrom DATETIME,
    IN p_DateTo DATETIME,
    IN p_Notes TEXT,
    IN p_Page INT,
    IN p_PageSize INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
);
```

**Returns**: Result set with transaction rows matching filters, plus output parameters.

#### inv_transactions_SmartSearch

```sql
CALL inv_transactions_SmartSearch(
    IN p_WhereClause TEXT,
    IN p_Page INT,
    IN p_PageSize INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
);
```

**Returns**: Result set based on dynamic WHERE clause (for advanced/custom searches).

#### inv_transactions_GetAnalytics

```sql
CALL inv_transactions_GetAnalytics(
    IN p_UserName VARCHAR(100),
    IN p_IsAdmin TINYINT(1),
    IN p_FromDate DATETIME,
    IN p_ToDate DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
);
```

**Returns**: Summary statistics (total transactions by type, date ranges).

---

## UI Mockups and Layout Options

### Current Implementation: 3-Panel Layout (Search + Grid + Detail)

**Layout**: Search control at top, transaction grid on left (60%), detail panel on right (40%)

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│ MTM WIP Application - Transaction History System                       [_][□][X]│
├─────────────────────────────────────────────────────────────────────────────────┤
│ Step 1: Enter Search Criteria                                                   │
├──────────────────────────────────┬──────────────────────────────────────────────┤
│ 🌲 Part Number [_________▼]     │ 👤 User [All Users    ▼]  ⚙ Operation [▼]  │
│                                   │                                              │
│ 📍 From Location [All Locations▼]│ 📍 To Location [All Locations▼]              │
│                                   │ 📝 Notes Keyword [_______________]          │
│                                   │    Partial match supported                   │
├───────────────────────────────────┴──────────────────────────────────────────────┤
│ Select a Date Range (Custom Filter must be selected below)                      │
│ From: [11/ 1/2020 📅▼]  To: [11/30/2025 📅▼]                          [🔍]     │
│ Simple Date Filter:  ( ) Today  ( ) Week  ( ) Month  (•) Custom                │
│ Filter by Transaction Types:  [✓] IN  [✓] OUT  [✓] TRANSFER                    │
├────────────────────────────────────────────────┬─────────────────────────────────┤
│ ┌─ Transaction Results ───────────────────────┐│ Transaction Details - ID: 29171 │
│ │┌────┬──────┬─────────────┬────┬───────┬────┐││  ID: 29171                      │
│ ││ ID │ Type │ Part Number │ Qty│ From  │ To │││  Type: IN                       │
│ │├────┼──────┼─────────────┼────┼───────┼────┤││  Item Type: WIP                 │
│ ││29171│IN   │10000000825  │ 200│FLOOR-R│—  │││|  Part Number: 10000000825       │
│ ││29144│IN   │10000000825  │ 180│FLOOR-R│—  │││|  Batch: 0000011234              │
│ ││29143│IN   │10000000825  │ 200│FLOOR-R│—  │││|  Quantity: 200                  │
│ ││29142│IN   │10000000825  │ 200│FLOOR-R│—  │││|  From: FLOOR - R C/D            │
│ ││29141│IN   │10000000825  │ 200│FLOOR-R│—  │││|  To: —                          │
│ ││29126│IN   │GM102555     │ 254│R-N2-05│—  │││|  Operation: 19                  │
│ ││29949│IN   │A110147      │ 500│R-G0-13│—  │││|  User: DHAMMONS                 │
│ ││29948│IN   │A110146      │ 500│R-G0-13│—  │││|  Date/Time: 10/06/2025 11:44:43 │
│ ││29940│IN   │A110147      │ 500│R-G0-14│—  │││| Notes                           │
│ ││29939│IN   │A110146      │ 500│R-G0-15│—  │││|┌──────────────────────────────┐ │
│ ││29934│IN   │A110147      │ 500│R-G0-15│—  │││|│                              │ │
│ ││29933│IN   │A110146      │ 500│R-G4-15│—  │││|│                              │ │
│ ││ ... │ ...  │...          │ ..│...    │ ..│││|│                              │ │
│ │└────┴──────┴─────────────┴────┴───────┴────┘│││                              │ │
│ │                                             ||└──────────────────────────────┘ │
│ │ [← Previous] Page 1 of 2 [Next →]           |│                                 │
│ └─────────────────────────────────────────────┘│ Select a transaction to view... │
│                                                | ┌─ Related Transactions ─────┐  │
│                                                | │  [Transaction Life Cycle]  │  │
│                                                | └────────────────────────────┘  │
├────────────────────────────────────────────────┴─────────────────────────────────┤
│ 🔄 ← Previous  Next →  Page 1 of 2  58 records found       Page Number: [___] Go│
└──────────────────────────────────────────────────────────────────────────────────┘
```

**Key Features**:
- ✅ Search control collapses/expands at top (TransactionSearchControl)
- ✅ Grid shows paginated results with horizontal scroll (TransactionGridControl)
- ✅ Detail panel on right updates on row selection (TransactionDetailPanel)
- ✅ "Transaction Life Cycle" button opens modal TreeView dialog
- ✅ All theme-aware with Core_Themes integration

**Implemented Components**:
- `TransactionSearchControl.cs` - Top search filters
- `TransactionGridControl.cs` - Left grid with pagination
- `TransactionDetailPanel.cs` - Right detail panel (embedded)
- `Transactions.cs` - Parent form coordinating all controls

---

### Transaction Life Cycle Modal Dialog

Opened when user clicks "Transaction Life Cycle" button in TransactionDetailPanel:

```
┌─ 21-28841-006 - 0000021324 - Transaction Life Cycle ───────────────[X]┐
│                                                                         │
│ ┌─ Lifecycle Tree ──────────────┬─ Transaction Details ──────────────┐│
│ │                                │                                    ││
│ │ 📦 Batch 0000021324 (500 units)│ Transaction ID: 40361              ││
│ │ ├─ 📥 IN - X-00                │ Type: IN                           ││
│ │ │   ID: 40361 | 500 units      │ Part Number: 21-28841-006          ││
│ │ │                               │ Batch: 0000021324                  ││
│ │ ├─ 🔄 TRANSFER - X-00 → X-04   │ Quantity: 500 units                ││
│ │ │   ID: 40362 | 250 units      │ From: X-00                         ││
│ │ │   ├─ 📦 Split: 250 @ X-04    │ To: —                              ││
│ │ │   │   └─ 🔄 TRANSFER - X-04 →│ Operation: 90                      ││
│ │ │   │       ID: 40363 | 100 →X-│ User: JOHNK                        ││
│ │ │   │       ├─ 📦 Split: 100 @ │ Date/Time: 11/01/2025 20:47:31     ││
│ │ │   │       └─ 📦 Split: 150 @ │                                    ││
│ │ │   └─ 📦 Split: 250 @ X-00    │ Notes:                             ││
│ │ │                               │ ┌────────────────────────────────┐││
│ │ │                               │ │                                │││
│ │ │                               │ │                                │││
│ │ │                               │ │                                │││
│ │ │                               │ └────────────────────────────────┘││
│ │ │                               │                                    ││
│ │ │                               │ [View in Main Grid]                ││
│ │ │                               │                                    ││
│ └────────────────────────────────┴────────────────────────────────────┘│
│                                                                         │
│ Icon Legend: 📥 IN (Green) | 🔄 TRANSFER (Blue) | 📤 OUT (Red) | 📦 Split (Orange) │
│                                                                         │
│                                    [Export] [Print] [Close]            │
└─────────────────────────────────────────────────────────────────────────┘
```

**Tree Structure Logic**:
- **Root node**: First IN transaction for the batch
- **Linear branches**: Transactions that move entire quantity
- **Split branches**: TRANSFER where quantity < source inventory creates child nodes
- **Node selection**: Updates detail panel on right with full transaction info
- **No dates in tree**: Chronological order implied, dates shown in detail panel

**Split Detection**:
```
Example: Batch 0000021324
- Transaction 40361: IN 500 units @ X-00
- Transaction 40362: TRANSFER 250 units X-00 → X-04
  → Split detected: 250 moved, 250 remaining at X-00
  → Create 2 child branches under 40362
- Transaction 40363: TRANSFER 100 units X-04 → X-03
  → Split detected: 100 moved, 150 remaining at X-04
  → Create 2 child branches under 40363
```

---

## Non-Functional Requirements

### NFR-001: Performance

- Transaction search completes in < 2 seconds for date ranges up to 90 days
- Grid renders 50 rows in < 500ms
- Pagination navigation instantaneous (< 100ms)
- Export to Excel completes in < 5 seconds for 1000 records
- Form load time < 3 seconds including initial data load
- Lifecycle tree builds in < 1 second for batches with up to 100 transactions

### NFR-002: Scalability

- Support displaying up to 100,000 transactions (with pagination)
- Lifecycle viewer handles batches with up to 100 transactions efficiently
- Handle up to 100 concurrent user searches
- Dropdown autocomplete responsive with 10,000+ parts

### NFR-003: Usability

- Keyboard shortcuts for common actions:
  - F5: Refresh/Search
  - Ctrl+E: Export
  - Ctrl+P: Print
  - Ctrl+R: Reset filters
  - Escape: Clear selection
- Tab order logical (top-to-bottom, left-to-right)
- Error messages actionable and user-friendly
- Progress indicators for operations > 1 second

### NFR-004: Accessibility

- All controls accessible via keyboard
- Screen reader compatible
- High contrast theme support
- Minimum font size: 9pt (scales with DPI)
- Color-blind friendly (no red/green only indicators)

### NFR-005: Maintainability

- Each file < 500 lines
- Cyclomatic complexity < 10 per method
- XML documentation coverage > 95% (verified by MCP tool `check_xml_docs`)
- No code duplication (DRY principle)
- SOLID principles followed throughout

---

## Success Criteria

### Development Phase

- ✅ All Priority 1 (P1) user stories implemented
- ✅ All functional requirements (FR-001 through FR-007) satisfied
- ✅ Code passes all MCP validation tools:
  - `validate_dao_patterns`: PASS
  - `validate_error_handling`: PASS
  - `check_xml_docs`: > 95% coverage
  - `analyze_performance`: No HIGH issues
  - `check_security`: No CRITICAL/HIGH issues
- ✅ Compilation succeeds with zero errors, zero warnings
- ✅ All files under line count limits (per FR-001)

### Testing Phase

- ✅ Manual test scenarios pass (see Testing section below)
- ✅ Search returns correct results for all filter combinations
- ✅ Pagination navigation works correctly
- ✅ Export produces valid Excel file with correct data
- ✅ Print preview displays correctly
- ✅ No crashes or unhandled exceptions during 1-hour continuous use
- ✅ Performance metrics met (NFR-001)

### Deployment Phase

- ✅ Existing users can perform all workflows from old form
- ✅ No data loss or corruption
- ✅ Help documentation updated
- ✅ Training materials prepared
- ✅ Rollback plan documented

---

## Testing Strategy

### Unit Tests (ViewModel Layer)

```csharp
[TestClass]
public class TransactionViewModelTests
{
    [TestMethod]
    public async Task SearchAsync_WithValidCriteria_ReturnsResults()
    {
        // Arrange
        var viewModel = new TransactionViewModel();
        var criteria = new TransactionSearchCriteria
        {
            DateFrom = DateTime.Today.AddDays(-7),
            DateTo = DateTime.Today
        };

        // Act
        var result = await viewModel.SearchTransactionsAsync(criteria);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Data.Count > 0);
    }

    [TestMethod]
    public async Task SearchAsync_WithEmptyCriteria_ReturnsValidationError()
    {
        // Arrange
        var viewModel = new TransactionViewModel();
        var criteria = new TransactionSearchCriteria(); // Empty

        // Act
        var result = await viewModel.SearchTransactionsAsync(criteria);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.IsTrue(result.ErrorMessage.Contains("at least one criterion"));
    }
}
```

### Integration Tests (DAO Layer)

```csharp
[TestClass]
public class Dao_Transactions_IntegrationTests : BaseIntegrationTest
{
    [TestMethod]
    public async Task SearchAsync_ReturnsTransactionsFromDatabase()
    {
        // Arrange
        var dao = new Dao_Transactions();
        var criteria = new TransactionSearchCriteria
        {
            DateFrom = DateTime.Parse("2025-10-24"),
            DateTo = DateTime.Parse("2025-10-25")
        };

        // Act
        var result = await dao.SearchAsync(criteria, page: 1, pageSize: 50);

        // Assert
        Assert.IsTrue(result.IsSuccess, result.ErrorMessage);
        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Data.Count > 0, "Expected transactions in date range");
    }
}
```

### Manual Test Scenarios

#### Scenario 1: Basic Search
1. Open Transaction Viewer
2. Select "Today" date filter
3. Click Search
4. **Expected**: Grid shows today's transactions, newest first

#### Scenario 2: Part Number Search
1. Type "21-28841" in Part Number field
2. Click Search
3. **Expected**: Only transactions for that part appear
4. Verify pagination if > 50 results

#### Scenario 3: Multi-Filter Search
1. Select Part: "21-28841-006"
2. Select User: "JOHNK"
3. Select Type: IN only (uncheck OUT and TRANSFER)
4. Select Date: This Week
5. Click Search
6. **Expected**: Results match ALL criteria

#### Scenario 4: Export to Excel
1. Perform any search with results
2. Click Export button
3. Choose save location
4. **Expected**: Excel file created with all visible columns, opens correctly

#### Scenario 5: Error Handling
1. Enter invalid date range (To date before From date)
2. Click Search
3. **Expected**: Validation error dialog appears with helpful message

#### Scenario 6: Pagination
1. Perform search with > 50 results
2. Click Next Page
3. **Expected**: Page 2 displays, different records shown
4. Click Previous Page
5. **Expected**: Returns to Page 1 with original records

#### Scenario 7: Detail View
1. Click on any transaction row
2. **Expected**: Detail dialog opens showing all transaction fields
3. Verify Related Transactions section shows other transactions in same batch
4. Close dialog

#### Scenario 8: Performance
1. Select "This Month" date filter
2. Click Search
3. **Expected**: Results appear in < 2 seconds
4. Verify progress indicator shows during load

#### Scenario 9: Theme Switching and DPI Scaling
1. Open Transaction Viewer at 100% DPI scaling
2. **Expected**: All controls render correctly with theme colors applied
3. Change Windows Display Settings to 125% DPI scaling
4. Relaunch application
5. **Expected**: Form scales properly, no layout breakage, all text readable
6. Open Settings → Theme Selection
7. Change theme from "Default" to "Midnight"
8. **Expected**: Transaction Viewer colors update immediately to match theme
9. Verify no hardcoded colors override theme (buttons, panels, labels match selected theme)
10. Test at 150% and 200% DPI scaling
11. **Expected**: Form remains usable at all DPI levels, controls maintain proper sizing

**Theme Compliance Validation**:
- Constructor includes `Core_Themes.ApplyDpiScaling(this)` and `ApplyRuntimeLayoutAdjustments(this)`
- All custom colors use `Model_UserUiColors` tokens with `SystemColors` fallbacks
- No hardcoded colors without `// ACCEPTABLE:` justification comments
- Control names follow `{ComponentName}_{ControlType}_{Purpose}` convention

---

## Implementation Tasks

See `specs/transaction-viewer-redesign/tasks.md` for detailed task breakdown.

**High-level phases**:
1. **Phase 1**: Architecture Setup (ViewModel, Models, Search Criteria)
2. **Phase 2**: DAO Layer Refactoring (Stored procedure wrappers)
3. **Phase 3**: Core Form and Search Control
4. **Phase 4**: Grid Control and Pagination
5. **Phase 5**: Detail Dialog
6. **Phase 6**: Export and Print
7. **Phase 7**: Testing and Validation
8. **Phase 8**: Documentation and Deployment

---

## Migration Strategy

### Parallel Running Period

**Week 1-2**: New form available via Settings → Developer → "Use New Transaction Viewer"
- Default: Old form
- Opt-in: New form
- Collect feedback

**Week 3-4**: New form becomes default, old form available via "Use Legacy Transaction Viewer"
- Default: New form
- Opt-out: Old form for compatibility
- Monitor for issues

**Week 5+**: Retire old form
- Remove old Transactions.cs (archive in git history)
- Remove legacy toggle
- Full transition complete

### Rollback Plan

If critical issues discovered:
1. Restore `Transactions.cs` from git history
2. Disable new form in Settings
3. Document issue in bug tracker
4. Fix identified problems
5. Re-deploy when stable

### Data Migration

**No database migration required** - both forms read from same `inv_transaction` table.

### User Training

- **Quick Start Guide**: 1-page PDF showing new search layout
- **Video Tutorial**: 3-minute walkthrough of key features
- **Help Button**: Opens context-sensitive help in form
- **Tooltips**: Hover help on all controls

---

## Risks and Mitigations

### Risk 1: Users Resist Change

**Impact**: Medium  
**Likelihood**: High  

**Mitigation**:
- Parallel running period allows gradual adoption
- New form uses familiar terminology and workflow
- Training materials and help readily available
- Legacy form available as fallback

### Risk 2: Performance Degradation with Large Datasets

**Impact**: High  
**Likelihood**: Medium  

**Mitigation**:
- Pagination limits result set size
- Database indexes on search columns
- Async operations prevent UI blocking
- Performance testing with 100k+ records
- Caching of dropdown data

### Risk 3: Bugs in DAO Refactoring

**Impact**: High  
**Likelihood**: Low  

**Mitigation**:
- Comprehensive integration tests
- Existing stored procedures already tested
- No changes to database schema or procedures
- Side-by-side comparison testing (old vs new form)
- Extensive manual testing before rollout

### Risk 4: Incomplete Feature Parity

**Impact**: Medium  
**Likelihood**: Medium  

**Mitigation**:
- Feature matrix comparing old vs new
- Priority 1 features cover 95% of usage
- Optional Priority 2/3 features can be added post-launch
- User feedback drives feature prioritization

---

## Dependencies

### Internal Dependencies

- **Core_Themes**: DPI scaling and theming
- **Service_ErrorHandler**: Error dialog system
- **Helper_Database_StoredProcedure**: Database access layer
- **Helper_StoredProcedureProgress**: Progress reporting
- **Dao_Part**: Part number dropdown population
- **Dao_User**: User dropdown population
- **Dao_Location**: Location dropdown population

### External Dependencies

- **MySQL 5.7**: Database server
- **Stored Procedures**: inv_transactions_Search, inv_transactions_SmartSearch, inv_transactions_GetAnalytics
- **ClosedXML**: Excel export library
- **System.Drawing.Printing**: Print functionality

### Database Dependencies

- **Tables**: `inv_transaction` (24,000+ rows), `md_part_ids`, `usr_users`, `md_locations`
- **Indexes**: Existing indexes on PartID, User, ReceiveDate must be maintained

---

## Appendix A: Model Definitions

### TransactionSearchCriteria.cs

```csharp
/// <summary>
/// Encapsulates search criteria for transaction queries.
/// </summary>
public class TransactionSearchCriteria
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
    /// Gets or sets the transaction type to filter by.
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
    /// Validates the search criteria.
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
    /// Validates date range logic.
    /// </summary>
    /// <returns>True if date range is valid or not specified; otherwise false.</returns>
    public bool IsDateRangeValid()
    {
        if (!DateFrom.HasValue || !DateTo.HasValue)
            return true;

        return DateFrom.Value <= DateTo.Value;
    }
}
```

### Model_Transactions.cs (Existing, for reference)

```csharp
internal class Model_Transactions
{
    public int ID { get; set; }
    public TransactionType TransactionType { get; set; }
    public string? BatchNumber { get; set; }
    public string? PartID { get; set; }
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public string? Operation { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
    public string? User { get; set; }
    public string? ItemType { get; set; }
    public DateTime DateTime { get; set; }
}

internal enum TransactionType
{
    IN,
    OUT,
    TRANSFER
}
```

---

## Appendix B: Instruction File References

This specification mandates compliance with the following instruction files:

1. **`.github/instructions/csharp-dotnet8.instructions.md`**
   - File-scoped namespaces
   - Async/await patterns
   - Naming conventions
   - Code organization (regions)

2. **`.github/instructions/mysql-database.instructions.md`**
   - Stored procedure usage mandate
   - Helper_Database_StoredProcedure patterns
   - Parameter naming (NO p_ prefix in C#)
   - Connection pooling
   - DaoResult patterns

3. **`.github/instructions/testing-standards.instructions.md`**
   - Manual validation approach
   - Success criteria definition
   - Test scenario documentation

4. **`.github/instructions/integration-testing.instructions.md`**
   - Discovery-first workflow for DAOs
   - Method signature verification
   - DaoResult null safety
   - BaseIntegrationTest usage

5. **`.github/instructions/documentation.instructions.md`**
   - XML documentation requirements
   - Code comment standards
   - README structure

6. **`.github/instructions/security-best-practices.instructions.md`**
   - Input validation
   - SQL injection prevention (via stored procedures)
   - Error message security

7. **`.github/instructions/performance-optimization.instructions.md`**
   - Async/await patterns
   - Connection pooling
   - Memory management
   - UI responsiveness

8. **`.github/instructions/code-review-standards.instructions.md`**
   - Quality gates
   - Review checklist
   - Compliance verification

9. **`.github/instructions/ui-compliance/theming-compliance.instructions.md`**
   - Theme system integration requirements (MANDATORY)
   - Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments() constructor patterns
   - Color token usage with Model_UserUiColors
   - Hardcoded color whitelist and justification rules
   - WinForms UI architecture standards (control naming, AutoSize patterns)

10. **`Documentation/Theme-System-Reference.md`**
    - Theme system architecture and color token catalog (203 properties)
    - DPI scaling system (Section 6) - 100%-200% scaling support
    - Runtime layout adjustments (Section 7)
    - Font sizing standards (Section 5)
    - Database storage (`app_themes` table, 9 themes available)
    - Complete API reference for Core_Themes integration

---

## Appendix C: MCP Validation Checklist

Before marking this specification as complete, run all applicable MCP tools:

```bash
# DAO Pattern Validation
mcp_mtm-workflow_validate_dao_patterns(
    dao_dir="Data",
    recursive=true
)

# Error Handling Validation
mcp_mtm-workflow_validate_error_handling(
    source_dir="Forms/Transactions",
    recursive=true
)

# XML Documentation Coverage
mcp_mtm-workflow_check_xml_docs(
    source_dir="Forms/Transactions",
    min_coverage=95,
    recursive=true
)

# Performance Analysis
mcp_mtm-workflow_analyze_performance(
    source_dir="Forms/Transactions",
    focus="all",
    recursive=true
)

# Security Scan
mcp_mtm-workflow_check_security(
    source_dir="Forms/Transactions",
    scan_type="all",
    recursive=true
)

# Build Validation
mcp_mtm-workflow_validate_build(
    workspace_root="C:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms"
)
```

**Expected Results**:
- ✅ validate_dao_patterns: 100% compliance
- ✅ validate_error_handling: 0 MessageBox.Show calls
- ✅ check_xml_docs: ≥95% coverage
- ✅ analyze_performance: No HIGH severity issues
- ✅ check_security: No CRITICAL/HIGH vulnerabilities
- ✅ validate_build: Clean build, 0 errors, 0 warnings

---

## Document Control

**Version**: 1.0  
**Status**: Draft  
**Author**: AI Agent (GitHub Copilot)  
**Review Required**: Yes  
**Approval Required**: Yes (User)  

**Change History**:
| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | 2025-10-25 | AI | Initial specification created |

**Next Steps**:
1. User reviews and approves specification
2. User selects UI mockup option (A, B, or C)
3. AI generates `tasks.md` with detailed implementation tasks
4. AI begins Phase 1 implementation upon approval

---

**END OF SPECIFICATION**
