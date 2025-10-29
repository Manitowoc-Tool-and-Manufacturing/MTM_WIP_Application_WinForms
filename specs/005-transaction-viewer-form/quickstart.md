# Quick Start Guide: Transaction Viewer Form Development

**Feature**: Transaction Viewer Form Redesign  
**Date**: 2025-10-29  
**For**: Developers implementing the transaction viewer redesign

## Prerequisites

### Environment Setup

**Required Software**:
- Visual Studio 2022 (or VS Code with C# DevKit)
- .NET 8.0 SDK
- MySQL 5.7+ (MAMP or standalone)
- Git for version control

**Database Access**:
- **Development**: `mtm_wip_application_winforms_test` on `localhost` or `172.16.1.104`
- **Production**: `mtm_wip_application` on `172.16.1.104` (Release builds only)

**Test Data**:
- Test database contains 24,000+ transaction records
- Sample transactions available from 2024-01-01 to present
- Test user: `JOHNK` (or create your own via usr_users table)

### Documentation Review

**Read these files BEFORE coding**:
1. `specs/005-transaction-viewer-form/spec.md` - Feature specification (52 KB)
2. `specs/005-transaction-viewer-form/research.md` - Technical decisions (current file's sibling)
3. `specs/005-transaction-viewer-form/data-model.md` - Entity definitions (current file's sibling)
4. `.specify/memory/constitution.md` - Constitution principles

**Reference during coding**:
- `.github/instructions/csharp-dotnet8.instructions.md` - Language patterns
- `.github/instructions/mysql-database.instructions.md` - Database patterns
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach

---

## Build and Run

### Initial Build

```powershell
# Navigate to repository root
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms

# Restore dependencies (if needed)
dotnet restore

# Build Debug configuration (uses test database)
dotnet build MTM_Inventory_Application.csproj -c Debug

# Run application
dotnet run --project MTM_Inventory_Application.csproj
```

### Verify Existing Form

1. Launch application
2. Navigate to "Transactions" tab in main form
3. Observe existing 2136-line implementation
4. Test search functionality to understand current behavior
5. Note performance and usability issues (baseline for comparison)

---

## Development Workflow

### Step 1: Create Feature Branch

```powershell
# Ensure you're on master and up-to-date
git checkout master
git pull origin master

# Create feature branch (already exists: 005-transaction-viewer-form)
git checkout 005-transaction-viewer-form

# If starting fresh, create and push:
# git checkout -b 005-transaction-viewer-form
# git push -u origin 005-transaction-viewer-form
```

### Step 2: Create New Models

**File**: `Models/TransactionSearchCriteria.cs`

```csharp
namespace MTM_Inventory_Application.Models;

/// <summary>
/// Encapsulates search criteria for transaction queries.
/// </summary>
internal class TransactionSearchCriteria
{
    public string? PartID { get; set; }
    public string? User { get; set; }
    public string? FromLocation { get; set; }
    public string? ToLocation { get; set; }
    public string? Operation { get; set; }
    public TransactionType? TransactionType { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string? Notes { get; set; }

    public bool IsValid() { /* See data-model.md for implementation */ }
    public bool IsDateRangeValid() { /* See data-model.md */ }
    public override string ToString() { /* See data-model.md */ }
}
```

**File**: `Models/TransactionSearchResult.cs`

```csharp
namespace MTM_Inventory_Application.Models;

/// <summary>
/// Represents search results with pagination metadata.
/// </summary>
internal class TransactionSearchResult
{
    public List<Model_Transactions> Transactions { get; set; } = new();
    public int TotalRecordCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalRecordCount / PageSize) : 0;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public string PaginationSummary => $"Page {CurrentPage} of {TotalPages} ({TotalRecordCount} total records)";
}
```

### Step 3: Create ViewModel

**File**: `Models/TransactionViewModel.cs`

```csharp
namespace MTM_Inventory_Application.Models;

/// <summary>
/// Business logic and state management for transaction viewer.
/// </summary>
internal class TransactionViewModel
{
    #region Fields
    private readonly Dao_Transactions _dao;
    private TransactionSearchCriteria? _currentCriteria;
    private TransactionSearchResult? _currentResults;
    #endregion

    #region Constructors
    public TransactionViewModel()
    {
        _dao = new Dao_Transactions();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Searches for transactions matching the specified criteria.
    /// </summary>
    public async Task<DaoResult<TransactionSearchResult>> SearchTransactionsAsync(
        TransactionSearchCriteria criteria,
        int page = 1,
        int pageSize = 50,
        Helper_StoredProcedureProgress? progress = null)
    {
        // Implementation: Validate, call DAO, wrap results
        // See research.md and data-model.md for details
    }

    /// <summary>
    /// Exports current results to Excel.
    /// </summary>
    public async Task<DaoResult<string>> ExportToExcelAsync(
        string filePath,
        Helper_StoredProcedureProgress? progress = null)
    {
        // Implementation: Use ClosedXML to generate Excel file
        // See research.md for pattern
    }
    #endregion

    #region Private Methods
    private Dictionary<string, object> MapCriteriaToParameters(TransactionSearchCriteria criteria)
    {
        // Map criteria properties to stored procedure parameters
        // See mysql-database.instructions.md for parameter naming rules
    }
    #endregion
}
```

**Target**: 400 lines or less

### Step 4: Create UserControls

**File**: `Controls/Transactions/TransactionSearchControl.cs`

```csharp
namespace MTM_Inventory_Application.Controls.Transactions;

/// <summary>
/// Search filter UI for transaction viewer.
/// </summary>
public partial class TransactionSearchControl : UserControl
{
    #region Events
    /// <summary>
    /// Raised when user clicks Search button with valid criteria.
    /// </summary>
    public event EventHandler<TransactionSearchCriteria>? SearchRequested;
    #endregion

    #region Constructors
    public TransactionSearchControl()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    }
    #endregion

    #region Button Clicks
    private void btnSearch_Click(object sender, EventArgs e)
    {
        var criteria = BuildCriteria();
        if (!criteria.IsValid())
        {
            Service_ErrorHandler.HandleValidationError(
                "Please provide at least one search criterion.",
                "Search Validation");
            return;
        }
        SearchRequested?.Invoke(this, criteria);
    }
    #endregion

    #region Helpers
    private TransactionSearchCriteria BuildCriteria()
    {
        // Read from UI controls (combo boxes, date pickers, checkboxes)
        // Return populated criteria object
    }
    #endregion
}
```

**Target**: 300 lines or less

**Similar pattern for**:
- `TransactionGridControl.cs` (DataGridView + pagination, 300 lines)
- `TransactionDetailPanel.cs` (Detail display, 200 lines)

### Step 5: Refactor Main Form

**File**: `Forms/Transactions/Transactions.cs`

```csharp
namespace MTM_Inventory_Application.Forms.Transactions;

/// <summary>
/// Main transaction viewer form - orchestration only.
/// </summary>
public partial class Transactions : Form
{
    #region Fields
    private TransactionViewModel _viewModel = null!;
    private Helper_StoredProcedureProgress? _progressHelper;
    #endregion

    #region Constructors
    public Transactions()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        
        InitializeViewModel();
        InitializeProgressReporting();
        WireUpEvents();
    }
    #endregion

    #region Initialization
    private void InitializeViewModel()
    {
        _viewModel = new TransactionViewModel();
    }

    private void InitializeProgressReporting()
    {
        _progressHelper = Helper_StoredProcedureProgress.Create(
            toolStripProgressBar,
            toolStripStatusLabel,
            this);
    }

    private void WireUpEvents()
    {
        searchControl.SearchRequested += SearchControl_SearchRequested;
        gridControl.PageChanged += GridControl_PageChanged;
        gridControl.RowSelected += GridControl_RowSelected;
    }
    #endregion

    #region Event Handlers
    private async void SearchControl_SearchRequested(object? sender, TransactionSearchCriteria e)
    {
        await ExecuteSearchAsync(e);
    }

    private async void GridControl_PageChanged(object? sender, int newPage)
    {
        await ExecuteSearchAsync(_viewModel.CurrentCriteria, newPage);
    }

    private void GridControl_RowSelected(object? sender, Model_Transactions transaction)
    {
        detailPanel.DisplayTransaction(transaction);
    }
    #endregion

    #region Search Execution
    private async Task ExecuteSearchAsync(TransactionSearchCriteria criteria, int page = 1)
    {
        try
        {
            var result = await _viewModel.SearchTransactionsAsync(criteria, page, 50, _progressHelper);
            
            if (result.IsSuccess && result.Data != null)
            {
                gridControl.DisplayResults(result.Data);
            }
            else
            {
                Service_ErrorHandler.HandleValidationError(result.Message, "Search");
            }
        }
        catch (Exception ex)
        {
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                retryAction: async () => await ExecuteSearchAsync(criteria, page),
                controlName: nameof(Transactions));
        }
    }
    #endregion
}
```

**Target**: 500 lines or less (down from 2136 lines)

### Step 6: Refactor DAO

**File**: `Data/Dao_Transactions.cs`

Add new method:

```csharp
/// <summary>
/// Searches for transactions matching the specified criteria with pagination.
/// </summary>
/// <param name="criteria">The search criteria.</param>
/// <param name="page">The page number (1-indexed).</param>
/// <param name="pageSize">The number of records per page.</param>
/// <returns>A DaoResult containing the list of transactions or an error.</returns>
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
        ["ToLocation"] = criteria.ToLocation ?? (object)DBNull.Value,
        ["Operation"] = criteria.Operation ?? (object)DBNull.Value,
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

private Model_Transactions MapDataRowToModel(DataRow row)
{
    // Map DataRow columns to Model_Transactions properties
    // Handle DBNull.Value appropriately
}
```

---

## Testing Strategy

### Unit Tests (ViewModel)

**File**: `Tests/Unit/TransactionViewModelTests.cs`

```csharp
[TestClass]
public class TransactionViewModelTests
{
    [TestMethod]
    public async Task SearchAsync_WithValidCriteria_ReturnsSuccess()
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
    }

    [TestMethod]
    public async Task SearchAsync_WithEmptyCriteria_ReturnsValidationError()
    {
        // Arrange
        var viewModel = new TransactionViewModel();
        var criteria = new TransactionSearchCriteria();

        // Act
        var result = await viewModel.SearchTransactionsAsync(criteria);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        StringAssert.Contains(result.Message, "at least one criterion");
    }
}
```

### Integration Tests (DAO)

**File**: `Tests/Integration/Dao_Transactions_Tests.cs`

```csharp
[TestClass]
public class Dao_Transactions_IntegrationTests : BaseIntegrationTest
{
    [TestMethod]
    public async Task SearchAsync_WithDateRange_ReturnsTransactions()
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
        Assert.IsTrue(result.IsSuccess, result.Message);
        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Data.Count > 0, "Expected transactions in date range");
    }
}
```

### Manual Validation

**Checklist** (see `specs/005-transaction-viewer-form/spec.md` Appendix for full scenarios):

1. ✅ Basic search with Today filter
2. ✅ Part number search with autocomplete
3. ✅ Multi-filter search (Part + User + Type + Date)
4. ✅ Export to Excel (verify file opens)
5. ✅ Pagination navigation (Previous/Next)
6. ✅ Error handling (invalid date range)
7. ✅ Detail view (click row, verify dialog)
8. ✅ Performance (<2s search, <100ms UI response)

---

## Common Pitfalls

### 1. Inline SQL

❌ **WRONG**:
```csharp
var query = $"SELECT * FROM inv_transaction WHERE PartID = '{partId}'";
```

✅ **CORRECT**:
```csharp
var parameters = new Dictionary<string, object> { ["PartID"] = partId };
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
    connectionString, "inv_transactions_Search", parameters, useAsync: true);
```

### 2. MessageBox.Show()

❌ **WRONG**:
```csharp
MessageBox.Show("Error: " + ex.Message);
```

✅ **CORRECT**:
```csharp
Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, 
    retryAction: () => RetryOperation(), controlName: nameof(Transactions));
```

### 3. Blocking Async Calls

❌ **WRONG**:
```csharp
var result = dao.SearchAsync(criteria).Result; // Deadlock risk!
```

✅ **CORRECT**:
```csharp
var result = await dao.SearchAsync(criteria);
```

### 4. Parameter Prefix

❌ **WRONG**:
```csharp
["p_PartID"] = partId // Helper adds p_ automatically
```

✅ **CORRECT**:
```csharp
["PartID"] = partId // No p_ prefix in C#
```

### 5. Missing DPI Scaling

❌ **WRONG**:
```csharp
public MyControl()
{
    InitializeComponent(); // Missing theme application
}
```

✅ **CORRECT**:
```csharp
public MyControl()
{
    InitializeComponent();
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
}
```

---

## MCP Validation Tools

Run these tools before submitting PR:

```powershell
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
- ✅ 0 MessageBox.Show calls
- ✅ 0 inline SQL statements
- ✅ 95%+ XML documentation coverage
- ✅ No HIGH severity performance issues
- ✅ No CRITICAL/HIGH security vulnerabilities
- ✅ Clean build (0 errors, 0 warnings)

---

## Git Workflow

### Commit Messages

Use conventional commit format:

```
feat: add TransactionSearchCriteria model with validation
feat: implement TransactionViewModel with async search
refactor: decompose Transactions.cs into UserControls
test: add integration tests for Dao_Transactions.SearchAsync
docs: update XML comments for TransactionViewModel
```

### Pull Request Checklist

Before submitting PR:

- [ ] All files under line count limits (Form <500, UserControls <300)
- [ ] Constitution Check passes (all 8 principles)
- [ ] MCP validation tools pass (listed above)
- [ ] Manual test scenarios completed (8 scenarios)
- [ ] XML documentation coverage >95%
- [ ] Build succeeds with 0 errors, 0 warnings
- [ ] Feature branch up-to-date with master
- [ ] Commit history clean (squash if needed)

---

## Troubleshooting

### Issue: Database Connection Fails

**Symptom**: "Unable to connect to MySQL server"

**Solution**:
1. Verify MAMP is running (or standalone MySQL service)
2. Check `Helper_Database_Variables.GetConnectionString()` returns correct server
3. Debug configuration should use `localhost` or `172.16.1.104`
4. Test connection manually: `mysql -h localhost -u root -p`

### Issue: Stored Procedure Not Found

**Symptom**: "Procedure inv_transactions_Search does not exist"

**Solution**:
1. Verify you're connected to test database: `mtm_wip_application_winforms_test`
2. Check stored procedures exist: `SHOW PROCEDURE STATUS WHERE Db = 'mtm_wip_application_winforms_test';`
3. If missing, run SQL scripts from `Database/UpdatedStoredProcedures/ReadyForVerification/`

### Issue: WinForms Designer Errors

**Symptom**: "The designer could not be shown for this file"

**Solution**:
1. Rebuild solution: `dotnet build -c Debug`
2. Close and reopen Visual Studio
3. Check for compilation errors in `.Designer.cs` files
4. Verify all dependencies installed: `dotnet restore`

### Issue: DPI Scaling Looks Wrong

**Symptom**: UI elements too small/large at different DPI settings

**Solution**:
1. Verify `Core_Themes.ApplyDpiScaling(this)` called in constructor
2. Check `AutoScaleMode = AutoScaleMode.Dpi` set on form/control
3. Test at 100%, 125%, 150%, 200% scaling
4. Use TableLayoutPanel with percentage sizing (not absolute pixels)

---

## Next Steps

After completing development:

1. **Phase 2**: Generate `tasks.md` using `/speckit.tasks` command
2. **Implementation**: Work through tasks in priority order
3. **Testing**: Execute manual validation scenarios
4. **Code Review**: Submit PR with MCP validation results
5. **Deployment**: Follow migration strategy (parallel running period)

---

## Resources

### Documentation
- Feature Specification: `specs/005-transaction-viewer-form/spec.md`
- Research Decisions: `specs/005-transaction-viewer-form/research.md`
- Data Models: `specs/005-transaction-viewer-form/data-model.md`
- Constitution: `.specify/memory/constitution.md`

### Instruction Files
- C# Patterns: `.github/instructions/csharp-dotnet8.instructions.md`
- Database Patterns: `.github/instructions/mysql-database.instructions.md`
- Testing Standards: `.github/instructions/testing-standards.instructions.md`
- Integration Testing: `.github/instructions/integration-testing.instructions.md`

### Tools
- MCP Server: `mtm-workflow` (installed)
- Excel Library: ClosedXML (already included)
- Progress Reporting: `Helper_StoredProcedureProgress`
- Error Handling: `Service_ErrorHandler`

### Support
- GitHub Issues: Report bugs and feature requests
- Code Review: Submit PR for team review
- Documentation: Update this quickstart as patterns evolve

---

**Last Updated**: 2025-10-29  
**Version**: 1.0
