# Quick Start Guide: Color Code & Work Order Tracking

**Feature**: Color Code & Work Order Tracking  
**Developer**: Implementation Guide  
**Date**: 2025-11-13

## Overview

This guide helps developers set up their environment, understand the architecture, and begin implementing the color code and work order tracking feature for MTM WIP Application.

## Prerequisites

- .NET 8.0 SDK installed
- Visual Studio 2022 or VS Code with C# extension
- MySQL 5.7.24 (MAMP) running on localhost:3306
- Git access to MTM_WIP_Application_WinForms repository
- Branch: `001-color-code-work-order-tracking` checked out

## Quick Setup (5 minutes)

### 1. Clone and Checkout

```powershell
# Clone repository (if not already cloned)
git clone https://github.com/Dorotel/MTM_WIP_Application_WinForms.git
cd MTM_WIP_Application_WinForms

# Checkout feature branch
git checkout 001-color-code-work-order-tracking

# Restore dependencies
dotnet restore MTM_WIP_Application_Winforms.csproj
```

### 2. Database Setup

```powershell
# Connect to MySQL (MAMP)
& "C:\MAMP\bin\mysql\bin\mysql.exe" -h localhost -P 3306 -u root -proot

# Select test database
USE mtm_wip_application_winforms_test;

# Run migration scripts (in order)
source Database/UpdatedTables/001_add_md_color_codes_table.sql;
source Database/UpdatedTables/002_add_requires_colorcode_to_parts.sql;
source Database/UpdatedTables/003_add_colorcode_workorder_to_inventory.sql;
source Database/UpdatedTables/004_add_colorcode_workorder_to_transaction.sql;

# Seed initial data
source Database/Scripts/seed_color_codes.sql;

# Verify schema
source Database/Scripts/validate_color_code_schema.sql;
```

### 3. Build and Run

```powershell
# Build solution
dotnet build MTM_WIP_Application_Winforms.csproj

# Run application
dotnet run --project MTM_WIP_Application_Winforms.csproj
```

## Architecture Overview

### Layered Architecture

```
┌─────────────────────────────────────────────────────┐
│  Presentation Layer (Forms / Controls)              │
│  - Control_InventoryTab.cs                          │
│  - Control_RemoveTab.cs                             │
│  - Control_TransferTab.cs                           │
│  - Control_Add_PartID.cs / Control_Edit_PartID.cs  │
└──────────────────────┬──────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────┐
│  Service Layer (Business Logic)                     │
│  - Service_ColorCodeValidator.cs (NEW)              │
│  - Service_ErrorHandler.cs (EXISTING)               │
└──────────────────────┬──────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────┐
│  Data Access Layer (DAOs)                           │
│  - Dao_ColorCode.cs (NEW)                           │
│  - Dao_Part.cs (MODIFIED)                           │
│  - Dao_Inventory.cs (MODIFIED)                      │
│  - Dao_Transactions.cs (MODIFIED)                   │
└──────────────────────┬──────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────┐
│  Database Layer (MySQL 5.7.24)                      │
│  - md_color_codes (NEW)                             │
│  - md_part_ids (MODIFIED: RequiresColorCode)        │
│  - inv_inventory (MODIFIED: ColorCode, WorkOrder)   │
│  - inv_transaction (MODIFIED: ColorCode, WorkOrder) │
└─────────────────────────────────────────────────────┘
```

### Key Components

| Component | Purpose | Status |
|-----------|---------|--------|
| `Dao_ColorCode.cs` | Color code master data operations | NEW |
| `Service_ColorCodeValidator.cs` | Work order validation & formatting | NEW |
| `Model_ColorCode.cs` | Color code entity model | NEW |
| `Model_Application_Variables.cs` | In-memory caches for flagged parts | MODIFIED |
| `Helper_UI_ComboBoxes.cs` | Cache management | MODIFIED |
| `Control_InventoryTab.cs` | Dynamic color code fields | MODIFIED |
| `Control_RemoveTab.cs` | Column display + auto-sort | MODIFIED |
| `Control_TransferTab.cs` | Read-only column display | MODIFIED |
| `Control_AdvancedInventory.cs` | Validation + redirect | MODIFIED |
| `Control_AdvancedRemove.cs` | Show All button | MODIFIED |
| `Control_Add_PartID.cs` | RequiresColorCode checkbox | MODIFIED |
| `Control_Edit_PartID.cs` | RequiresColorCode checkbox | MODIFIED |

## Development Workflow

### Phase 1: Database Foundation (Start Here)

**Goal**: Set up database schema and stored procedures

**Tasks**:
1. Create migration scripts in `Database/UpdatedTables/`
2. Create stored procedures in `Database/UpdatedStoredProcedures/`
3. Test migrations in test database first
4. Verify schema with validation script

**Files to Create**:
- `Database/UpdatedTables/001_add_md_color_codes_table.sql`
- `Database/UpdatedTables/002_add_requires_colorcode_to_parts.sql`
- `Database/UpdatedTables/003_add_colorcode_workorder_to_inventory.sql`
- `Database/UpdatedTables/004_add_colorcode_workorder_to_transaction.sql`
- `Database/UpdatedStoredProcedures/md_color_codes_GetAll.sql`
- `Database/UpdatedStoredProcedures/md_color_codes_Add.sql`
- `Database/UpdatedStoredProcedures/md_part_ids_GetAllColorCodeFlagged.sql`
- `Database/UpdatedStoredProcedures/md_part_ids_UpdateColorCodeFlag.sql`
- `Database/Scripts/seed_color_codes.sql`

**Testing**:
```sql
-- Verify table creation
SHOW TABLES LIKE '%color%';

-- Verify columns
DESCRIBE md_color_codes;
DESCRIBE md_part_ids;

-- Verify seeded data
SELECT * FROM md_color_codes;

-- Test stored procedure
CALL md_color_codes_GetAll(@status, @msg);
SELECT @status, @msg;
```

### Phase 2: DAO Layer

**Goal**: Create data access objects following Model_Dao_Result pattern

**Tasks**:
1. Create `Dao_ColorCode.cs` in `Data/`
2. Add methods: `GetAllAsync()`, `AddCustomColorAsync()`
3. Update `Dao_Part.cs`: Add `GetAllColorCodeFlaggedAsync()`
4. Update `Dao_Inventory.cs`: Modify `AddAsync()` and `SearchAsync()`
5. Add XML documentation to all public methods

**Pattern to Follow**:
```csharp
public async Task<Model_Dao_Result<DataTable>> GetAllAsync()
{
    try
    {
        var result = await Helper_Database_StoredProcedure
            .ExecuteDataTableWithStatusAsync("md_color_codes_GetAll", null);
            
        return result.IsSuccess
            ? Model_Dao_Result<DataTable>.Success(result.Data, result.StatusMessage)
            : Model_Dao_Result<DataTable>.Failure(result.ErrorMessage);
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleDatabaseError(ex, ...);
        return Model_Dao_Result<DataTable>.Failure("User-friendly message", ex);
    }
}
```

**Testing**:
- Use xUnit integration tests with `BaseIntegrationTest`
- Test success cases, error cases, edge cases
- Verify `Model_Dao_Result<T>` properties (IsSuccess, Data, ErrorMessage)

### Phase 3: Service Layer

**Goal**: Implement work order validation and color formatting

**Tasks**:
1. Create `Service_ColorCodeValidator.cs` in `Services/`
2. Implement `ValidateAndFormatWorkOrder(string input)`
3. Implement `FormatColorToTitleCase(string input)`
4. Add unit tests for validation logic

**Validation Rules**:
- Work order: 5-6 digits, optional WO- prefix
- Auto-format: `64153` → `WO-064153`
- Reject: Letters, symbols, < 5 digits, > 6 digits
- Color: Title case (first letter uppercase, rest lowercase)

### Phase 4: Caching

**Goal**: Load color codes and flagged parts into memory at startup

**Tasks**:
1. Add properties to `Model_Application_Variables.cs`:
   - `public static List<string> ColorFlaggedParts { get; set; }`
   - `public static DataTable ColorCodes { get; set; }`
2. Update `Helper_UI_ComboBoxes.cs`:
   - Add `ReloadColorCodeCachesAsync()` method
3. Call from `MainForm.cs` initialization
4. Add Shift+Click refresh to Reset buttons

**Cache Loading**:
```csharp
public static async Task ReloadColorCodeCachesAsync()
{
    var flaggedResult = await Dao_Part.GetAllColorCodeFlaggedAsync();
    if (flaggedResult.IsSuccess)
    {
        Model_Application_Variables.ColorFlaggedParts =
            flaggedResult.Data.AsEnumerable()
            .Select(row => row["PartID"].ToString()!)
            .ToList();
    }

    var colorsResult = await Dao_ColorCode.GetAllAsync();
    if (colorsResult.IsSuccess)
    {
        Model_Application_Variables.ColorCodes = colorsResult.Data;
    }
}
```

### Phase 5: UI - Inventory Tab

**Goal**: Add dynamic color code and work order input fields

**Tasks**:
1. Open `Controls/MainForm/Control_InventoryTab.cs`
2. Add SuggestionTextBox for ColorCode (reuse existing control)
3. Add TextBox for WorkOrder with validation
4. Implement Part ID TextChanged event:
   - Check if part in `Model_Application_Variables.ColorFlaggedParts`
   - Show/hide color code and work order fields dynamically
5. Add "OTHER" color code dialog
6. Update Save button validation

**Key Code**:
```csharp
private async void PartIDTextBox_TextChanged(object sender, EventArgs e)
{
    var partId = PartIDTextBox.Text.Trim();
    bool requiresColorCode = Model_Application_Variables.ColorFlaggedParts
        .Contains(partId, StringComparer.OrdinalIgnoreCase);
    
    ColorCodeTextBox.Visible = requiresColorCode;
    ColorCodeLabel.Visible = requiresColorCode;
    WorkOrderTextBox.Visible = requiresColorCode;
    WorkOrderLabel.Visible = requiresColorCode;
}
```

### Phase 6: UI - Remove/Transfer Tabs

**Goal**: Display color code and work order columns with auto-sort

**Tasks**:
1. Update `Control_RemoveTab.cs`:
   - Add Color and Work Order columns
   - Implement dynamic column visibility
   - Add auto-sort by Color (ASC), Location (ASC)
   - Add >1000 record warning for Show All
2. Update `Control_TransferTab.cs`:
   - Add read-only Color and Work Order columns
3. Update `Control_AdvancedRemove.cs`:
   - Add Show All button
   - Implement >1000 record warning

**Auto-Sort Implementation**:
```csharp
// In stored procedure or LINQ
ORDER BY 
    CASE WHEN ColorCode = 'Unknown' THEN 1 ELSE 0 END,
    ColorCode ASC,
    Location ASC
```

### Phase 7: UI - Settings Forms

**Goal**: Add RequiresColorCode checkbox to Part ID forms

**Tasks**:
1. Update `Control_Add_PartID.cs` and `.Designer.cs`:
   - Add CheckBox "Requires Color Code & Work Order"
   - Place near Part ID input
2. Update `Control_Edit_PartID.cs` and `.Designer.cs`:
   - Add same checkbox
   - Load current value from database
3. Implement save logic:
   - Call `Dao_Part.UpdateColorCodeFlagAsync(partId, requiresColorCode)`
   - Set `parentForm.requiresRestart = true` if changed
4. Test restart prompt on Settings form close

### Phase 8: Testing

**Goal**: Comprehensive testing of all feature components

**Test Types**:
1. **Unit Tests**: Service_ColorCodeValidator logic
2. **Integration Tests**: DAO methods with test database
3. **UI Tests**: Manual testing of dynamic field visibility
4. **Performance Tests**: Show All with >1000 records
5. **Edge Case Tests**: Invalid work orders, duplicate colors, legacy data

**Sample Integration Test**:
```csharp
[Fact]
public async Task AddCustomColor_NewColor_ReturnsSuccess()
{
    // Arrange
    var customColor = "Blueberry";
    
    // Act
    var result = await Dao_ColorCode.AddCustomColorAsync(customColor);
    
    // Assert
    Assert.True(result.IsSuccess);
    Assert.True(result.Data);  // Was inserted (not duplicate)
    
    // Verify in database
    var allColors = await Dao_ColorCode.GetAllAsync();
    Assert.Contains(allColors.Data.AsEnumerable(),
        row => row["ColorCode"].ToString() == "Blueberry");
}
```

## Common Patterns

### Error Handling

```csharp
try
{
    var result = await dao.SomeMethodAsync();
    if (!result.IsSuccess)
    {
        Service_ErrorHandler.ShowUserError(result.ErrorMessage);
        return;
    }
    // Process result.Data
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(
        ex,
        Enum_ErrorSeverity.Medium,
        contextData: new Dictionary<string, object>
        {
            ["Operation"] = "SomeOperation",
            ["User"] = Model_Application_Variables.User
        },
        callerName: nameof(MethodName));
}
```

### Logging

```csharp
// Application events


// Application errors
LoggingUtility.LogApplicationError(exception);

// Database errors
LoggingUtility.LogDatabaseError(exception, Enum_DatabaseEnum_ErrorSeverity.Error);
```

### Async UI Events

```csharp
private async void SaveButton_Click(object sender, EventArgs e)
{
    try
    {
        // Disable button to prevent double-click
        SaveButton.Enabled = false;
        
        var result = await dao.SaveDataAsync();
        if (result.IsSuccess)
        {
            MessageBox.Show("Saved successfully!");
        }
    }
    finally
    {
        SaveButton.Enabled = true;
    }
}
```

## Debugging Tips

### Database Issues

```powershell
# Check stored procedure exists
SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_NAME = 'md_color_codes_GetAll';

# Test stored procedure directly
CALL md_color_codes_GetAll(@s, @m);
SELECT @s, @m;

# Check parameter prefix detection
SELECT * FROM db_parameter_prefix_cache;
```

### Cache Issues

```csharp
// Verify cache loaded
Debug.WriteLine($"Color Flagged Parts: {Model_Application_Variables.ColorFlaggedParts.Count}");
Debug.WriteLine($"Color Codes: {Model_Application_Variables.ColorCodes.Rows.Count}");

// Force cache refresh
await Helper_UI_ComboBoxes.ReloadColorCodeCachesAsync();
```

### UI Issues

- Use Avalonia DevTools (if applicable) or Snoop for WinForms
- Check `InvokeRequired` for cross-thread UI updates
- Verify theme application (all controls inherit from `ThemedForm`/`ThemedUserControl`)
- Check control `Visible` property in debugger

## Useful Resources

### Documentation
- [Feature Spec](./spec.md) - Complete feature requirements
- [Data Model](./data-model.md) - Database schema and relationships
- [Research](./research.md) - Technology decisions and patterns
- [Contracts](./contracts/) - Stored procedure signatures

### Codebase References
- `Service_ErrorHandler.cs` - Error handling patterns
- `LoggingUtility.cs` - Logging patterns
- `Helper_Database_StoredProcedure.cs` - Database access patterns
- `Control_InventoryTab.cs` - Existing inventory UI patterns
- `Dao_Inventory.cs` - Existing DAO patterns

### External Links
- [MySQL 5.7.24 Documentation](https://dev.mysql.com/doc/refman/5.7/en/)
- [.NET 8.0 API Reference](https://learn.microsoft.com/en-us/dotnet/api/)
- [WinForms Documentation](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)

## Troubleshooting

### "Stored procedure not found"
- Verify procedure exists: `SHOW PROCEDURE STATUS WHERE Db='mtm_wip_application_winforms_test';`
- Check spelling in DAO call
- Ensure migration script ran successfully

### "Cache not loading"
- Check `MainForm.cs` initialization calls `ReloadColorCodeCachesAsync()`
- Verify database connection successful
- Check log files for errors: `%APPDATA%\MTM\Logs\`

### "Columns not showing in DataGridView"
- Verify column names match stored procedure output
- Check `Visible` property set correctly
- Ensure part is flagged in `Model_Application_Variables.ColorFlaggedParts`

### "Work order validation failing"
- Check `Service_ColorCodeValidator.ValidateAndFormatWorkOrder()` logic
- Verify regex pattern matches requirements
- Test with various inputs (5 digits, 6 digits, with/without WO-)

## Next Steps

1. Review [tasks.md](./tasks.md) (generated by `/speckit.tasks` command)
2. Start with Phase 1: Database Foundation
3. Work through phases sequentially
4. Test each phase before proceeding
5. Update this guide with learnings

---

**Need Help?**
- Review constitution compliance: `.specify/memory/constitution.md`
- Check AGENTS.md for project-specific guidance
- Reference existing codebase patterns in similar features
