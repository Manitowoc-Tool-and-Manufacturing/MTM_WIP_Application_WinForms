# Developer Tools Suite - Quick Start Guide

**Feature**: Developer Tools Suite Integration  
**Sub-Feature ID**: 002-003-001  
**Last Updated**: 2025-10-18  
**Target Audience**: Developers working on Phase 2.5 database layer standardization

---

## Overview

The Developer Tools Suite provides five integrated utilities accessible through Settings → Developer for database analysis, debugging, and code generation. This guide helps you get started quickly with each tool.

---

## Prerequisites

### 1. Enable Developer Role

You must have **Admin + Developer** permissions to access developer tools.

#### Check Your Current Role

```sql
-- Run against: mtm_wip_application_winforms_test or mtm_wip_application
SELECT 
    UserName, 
    IsAdmin, 
    IsDeveloper,
    CASE 
        WHEN IsAdmin = 1 AND IsDeveloper = 1 THEN 'Full Developer Access'
        WHEN IsAdmin = 1 THEN 'Admin Only (No Developer Tools)'
        ELSE 'No Access'
    END AS AccessLevel
FROM usr_users
WHERE UserName = 'JOHNK'; -- Replace with your username
```

#### Grant Developer Role (If Needed)

```sql
-- File: Database/Scripts/Grant-Developer-Role.sql
-- Run against: mtm_wip_application_winforms_test (Development only)

UPDATE usr_users
SET 
    IsAdmin = 1,
    IsDeveloper = 1,
    ModifiedBy = 'SYSTEM',
    ModifiedDate = NOW()
WHERE UserName = 'JOHNK'; -- Replace with your username

-- Verify
SELECT UserName, IsAdmin, IsDeveloper 
FROM usr_users 
WHERE UserName = 'JOHNK';
```

**⚠️ Important**: Only grant Developer role in Development/Test environments. Production users should not have Developer access.

### 2. Verify Database Artifacts

Some tools require analysis artifacts. Check if they exist:

```powershell
# From repository root
Test-Path "Database/call-hierarchy-complete.json"  # Should be True
Test-Path "Database/STORED_PROCEDURE_CALLSITES.csv"  # Should be True
```

If artifacts are missing, regenerate them:

```powershell
# From Database/ directory
cd Database
.\2-Trace-Complete-CallHierarchy-v2.ps1
```

---

## Accessing Developer Tools

### Step 1: Launch Application
```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms
dotnet run --project MTM_Inventory_Application.csproj
```

### Step 2: Login with Developer Credentials
- Username: `JOHNK` (or your developer account)
- Password: `[your password]`

### Step 3: Open Settings Form
- Click **Settings** button in MainForm toolbar
- OR press **F10** keyboard shortcut

### Step 4: Navigate to Developer Category
- Settings form opens with TreeView on left
- Expand **Developer** node (appears after "About" category)
- Six tools available:
  - 🐛 Debug Dashboard
  - 📝 Parameter Prefix Maintenance
  - 📊 Schema Inspector
  - 🔗 Procedure Call Hierarchy
  - 💻 Code Generator
  - 🔌 Database (connection monitor)

---

## Tool #1: Debug Dashboard

### Purpose
Real-time monitoring of application activity for debugging stored procedure execution, tracing business logic, and diagnosing performance issues.

### Quick Start

1. **Navigate**: Settings → Developer → Debug Dashboard
2. **Enable Tracing**: Check boxes for desired trace categories
   - ✅ Database Operations (stored procedure calls)
   - ✅ Business Logic (DAO methods, service layer)
   - ✅ UI Actions (button clicks, form events)
   - ✅ Performance (slow operations, timing)
3. **Set Debug Level**: Select from dropdown
   - Low: Only critical operations
   - Medium: Standard development logging
   - High: Verbose diagnostic output
4. **Watch Output**: Black console shows green text with timestamps

### Common Tasks

#### Trace a Stored Procedure Execution
```text
1. Enable "Database Operations" tracing
2. Set Debug Level to "High"
3. Click "Start Capture" (if paused)
4. Perform action in main application (e.g., add inventory item)
5. Watch Debug Dashboard for stored procedure call:
   [2025-10-18 14:23:15] [DB] Executing: inv_inventory_Add_Item
   [2025-10-18 14:23:15] [DB] Parameters: UserID=12345, PartNumber=ABC123
   [2025-10-18 14:23:15] [DB] Result: p_Status=0, p_ErrorMsg=Success
```

#### Save Debug Log for Analysis
```text
1. Reproduce issue while capturing
2. Click "Pause Capture" to freeze output
3. Click "Save Log" button
4. Choose location: Logs/DebugCapture_2025-10-18_142315.txt
5. Open in text editor for analysis
```

#### Find Performance Bottlenecks
```text
1. Enable "Performance" tracing
2. Perform slow operation
3. Look for timing entries:
   [PERF] Operation took 2345ms (SLOW QUERY DETECTED)
4. Identify stored procedure or DAO method responsible
```

### Keyboard Shortcuts
- **F5**: Refresh display
- **Ctrl+P**: Pause/Resume capture
- **Ctrl+S**: Save log
- **Ctrl+L**: Clear output

---

## Tool #2: Parameter Prefix Maintenance

### Purpose
Manage parameter prefix overrides for stored procedures that don't follow the p_ convention, enabling gradual standardization during Phase 2.5.

### Quick Start

1. **Navigate**: Settings → Developer → Parameter Prefix Maintenance
2. **View Existing Overrides**: DataGridView shows all active overrides
3. **Add New Override**: Click "Add Override" button
4. **Edit Override**: Double-click row or select + click "Edit Override"
5. **Delete Override**: Select row + click "Delete Override" (soft delete)

### Common Tasks

#### Add Override for Legacy Procedure

**Scenario**: You discover `sys_old_procedure` uses `in_` prefix instead of `p_`

```text
1. Click "Add Override" button
2. Fill in dialog fields:
   - Procedure Name: sys_old_procedure
   - Parameter Name: UserID (without prefix)
   - Override Prefix: in_
   - Reason: Legacy 2020 procedure, uses in_ convention
3. Click "Save"
4. Confirmation: "Override added successfully"
5. Verify: Override appears in DataGridView
```

**Result**: Next time `sys_old_procedure` is called, Helper_Database_StoredProcedure will use `in_UserID` parameter name.

#### Handle Production Hotfix (Procedure Not Yet in Dev)

**Scenario**: Production database has `inv_inventory_EmergencyFix` procedure, but it won't sync to Dev until Friday.

```text
1. Get procedure definition from Production DBA
2. Note parameter names (e.g., p_InventoryID, p_Reason)
3. Click "Add Override" for each parameter:
   - Procedure: inv_inventory_EmergencyFix
   - Parameter: InventoryID
   - Prefix: p_
   - Reason: Production hotfix, not yet in Dev database
4. Write C# code calling procedure (override prevents INFORMATION_SCHEMA lookup)
5. Code works immediately without waiting for database sync
6. Remove override after Friday sync (optional)
```

#### Find and Fix Non-Standard Procedures

```text
1. Run SQL query to find non-standard parameters:
   
   SELECT 
       SPECIFIC_NAME AS ProcedureName,
       PARAMETER_NAME,
       CASE 
           WHEN PARAMETER_NAME LIKE 'p\\_%' THEN 'Standard (p_)'
           WHEN PARAMETER_NAME LIKE 'in\\_%' THEN 'Legacy (in_)'
           ELSE 'No Prefix'
       END AS PrefixStatus
   FROM INFORMATION_SCHEMA.PARAMETERS
   WHERE SPECIFIC_SCHEMA = 'mtm_wip_application'
   AND PARAMETER_MODE = 'IN'
   AND PARAMETER_NAME NOT LIKE 'p\\_%'
   ORDER BY SPECIFIC_NAME;

2. For each non-standard parameter, add override
3. Track progress: Goal is empty override table when standardization complete
```

### Validation Warnings

**Warning Message**: "Procedure 'xyz' not found in INFORMATION_SCHEMA. Continue anyway?"

**What This Means**: 
- Procedure doesn't exist in current database
- Could be typo, or future procedure, or Production-only procedure

**What To Do**:
- Click "Yes" if you're sure procedure name is correct (e.g., Production hotfix)
- Click "No" to fix typo and try again

---

## Tool #3: Schema Inspector

### Purpose
Read-only browser for database schema metadata (tables, columns, stored procedures, parameters) without needing MySQL Workbench.

### Quick Start

1. **Navigate**: Settings → Developer → Schema Inspector
2. **View Tables**: Tables tab shows all database tables with row counts
3. **Inspect Table**: Click table row to see columns in detail panel
4. **View Procedures**: Stored Procedures tab shows all procedures
5. **Inspect Procedure**: Click procedure row to see parameters
6. **Refresh**: Click "Refresh" button or press **F5** to reload schema

### Common Tasks

#### Find Table Structure

```text
1. Go to "Tables" tab
2. Scroll or search for table (e.g., "sys_parameter_prefix_overrides")
3. Click table row
4. Detail panel shows columns:
   - OverrideId (INT, PRIMARY KEY, auto_increment)
   - ProcedureName (VARCHAR(128), NOT NULL)
   - ParameterName (VARCHAR(128), NOT NULL)
   - ...
5. Note data types for DAO model creation
```

#### Verify Stored Procedure Parameters

```text
1. Go to "Stored Procedures" tab
2. Find procedure (e.g., "inv_inventory_Add_Item")
3. Click procedure row
4. Detail panel shows parameters:
   - p_PartNumber (VARCHAR(50), IN)
   - p_Quantity (INT, IN)
   - p_Location (VARCHAR(10), IN)
   - p_Status (INT, OUT)
   - p_ErrorMsg (VARCHAR(500), OUT)
5. Verify all parameters use p_ prefix
```

#### Check Row Counts

```text
1. Tables tab shows TABLE_ROWS column
2. Quick sanity check:
   - usr_users: 15 rows (users)
   - inv_inventory: 5,432 rows (inventory records)
   - sys_parameter_prefix_overrides: 3 rows (overrides added)
3. Unexpectedly large/small counts indicate data issues
```

### Performance Note

Schema Inspector queries INFORMATION_SCHEMA views, which can be slow on large databases. Use "Refresh" sparingly (not on every navigation).

---

## Tool #4: Procedure Call Hierarchy

### Purpose
Visualize stored procedure dependencies and C# call sites to understand impact of refactoring changes.

### Quick Start

1. **Navigate**: Settings → Developer → Procedure Call Hierarchy
2. **Search Procedure**: Type procedure name in search box (autocomplete enabled)
3. **View Call Sites**: C# files that call this procedure
4. **View Dependencies**: Other procedures called by this procedure
5. **Show Tree**: Click "Show Dependency Tree" for hierarchical view

### Common Tasks

#### Find Where Procedure Is Called

**Scenario**: You want to refactor `inv_inventory_Add_Item` and need to find all call sites.

```text
1. Type "inv_inventory_Add_Item" in search box
2. Autocomplete suggests matching procedures
3. Select procedure from list
4. "C# Call Sites" panel shows:
   - File: Data/Dao_Inventory.cs, Line: 145
   - File: Forms/Transactions/AddInventoryForm.cs, Line: 289
   - File: Services/Service_InventoryImport.cs, Line: 67
5. Click file path to note locations for testing after refactoring
```

#### Understand Procedure Dependencies

**Scenario**: You're refactoring `inv_inventory_Transfer` and need to know what it calls.

```text
1. Search for "inv_inventory_Transfer"
2. "Called Procedures" panel shows:
   - inv_inventory_Validate_Item (check if item exists)
   - inv_inventory_Update_Location (update source location)
   - inv_inventory_Add_History (log transaction)
3. You now know 3 other procedures will be affected by changes
```

#### Visualize Dependency Tree

```text
1. Search for "inv_inventory_Add_Item"
2. Click "Show Dependency Tree" button
3. Tree view displays:
   inv_inventory_Add_Item (root)
   ├── inv_inventory_Validate_Part (called by root)
   ├── md_part_ids_Get_Details (called by root)
   └── inv_history_Add_Record (called by root)
4. Depth indicator shows complexity (0 = leaf, higher = more dependencies)
```

#### Regenerate Missing Artifacts

**If you see this error**: "Call hierarchy artifacts not found. Click Regenerate to create them."

```text
1. Click "Regenerate" button in error message
2. PowerShell window opens running: 2-Trace-Complete-CallHierarchy-v2.ps1
3. Wait for script completion (~30 seconds)
4. Click "Refresh" button in Procedure Call Hierarchy
5. Artifacts loaded, search now works
```

---

## Tool #5: Code Generator

### Purpose
Quickly scaffold C# DAO method code from stored procedure definitions, reducing boilerplate and ensuring MTM pattern compliance.

### Quick Start

1. **Navigate**: Settings → Developer → Code Generator
2. **Select Procedure**: Choose stored procedure from dropdown
3. **Generate Code**: Click "Generate DAO Method" button
4. **Review Output**: Generated C# code appears in text box
5. **Copy to Clipboard**: Click "Copy to Clipboard" button
6. **Paste into DAO**: Open appropriate DAO class and paste method

### Common Tasks

#### Generate DAO Method for New Stored Procedure

**Scenario**: You created `inv_inventory_EmergencyAdjust` procedure and need C# wrapper.

```text
1. Select "inv_inventory_EmergencyAdjust" from dropdown
2. Code Generator queries INFORMATION_SCHEMA for parameters:
   - p_InventoryID (BIGINT, IN)
   - p_AdjustmentReason (VARCHAR(200), IN)
   - p_Status (INT, OUT)
   - p_ErrorMsg (VARCHAR(500), OUT)
3. Click "Generate DAO Method"
4. Generated code appears:

/// <summary>
/// Emergency inventory adjustment operation.
/// Executes stored procedure: inv_inventory_EmergencyAdjust
/// </summary>
/// <param name="inventoryId">Inventory record ID</param>
/// <param name="adjustmentReason">Reason for adjustment</param>
/// <returns>DaoResult with operation status</returns>
public static async Task<DaoResult<DataTable>> EmergencyAdjustAsync(
    long inventoryId,
    string adjustmentReason)
{
    try
    {
        var parameters = new Dictionary<string, object>
        {
            ["InventoryID"] = inventoryId,
            ["AdjustmentReason"] = adjustmentReason
        };

        var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
            Model_AppVariables.ConnectionString,
            "inv_inventory_EmergencyAdjust",
            parameters,
            useAsync: true);

        if (result.IsSuccess)
        {
            return DaoResult<DataTable>.Success(result.Data);
        }
        else
        {
            LoggingUtility.LogApplicationError(result.Exception, result.StatusMessage);
            return DaoResult<DataTable>.Failure(result.StatusMessage);
        }
    }
    catch (Exception ex)
    {
        Dao_ErrorLog.HandleException_GeneralError_CloseApp(
            ex,
            nameof(EmergencyAdjustAsync),
            nameof(Dao_Inventory));
        return DaoResult<DataTable>.Failure(ex.Message);
    }
}

5. Click "Copy to Clipboard"
6. Open Data/Dao_Inventory.cs
7. Paste into appropriate region (e.g., #region Inventory Operations)
8. Review and adjust if needed (usually minimal changes required)
```

#### Handle Complex Procedures (10+ Parameters)

**Scenario**: Procedure `inv_transactions_Search` has 17 parameters.

```text
1. Select "inv_transactions_Search" from dropdown
2. Code Generator adds warning comment:

/// <summary>
/// Search inventory transactions with advanced filters.
/// Executes stored procedure: inv_transactions_Search
/// ⚠️ WARNING: This procedure has 17 parameters. Consider review for:
///   - Grouping parameters into DTO object
///   - Using optional parameters
///   - Simplifying query logic
/// </summary>

3. Generated code includes all 17 parameters in method signature
4. Parameters dictionary has all 17 entries
5. Code compiles but consider refactoring to DTO pattern:

public class SearchCriteria
{
    public string? PartNumber { get; set; }
    public DateTime? StartDate { get; set; }
    // ... 15 more properties
}

public static async Task<DaoResult<DataTable>> SearchTransactionsAsync(
    SearchCriteria criteria) // Single parameter instead of 17
{
    // Convert criteria to dictionary
}
```

### Best Practices

- **Always Review Generated Code**: Generator provides 95% solution, but verify error handling and return types
- **Update XML Comments**: Add specific business rules to `<summary>` tag
- **Test Compilation**: Build project after pasting to catch any type mismatches
- **Version Control**: Commit generated code separately from manual changes for easier review

---

## Tool #6: Database Connection Monitor

### Purpose
Monitor database connection health and strength (existing tool, moved from Database category to Developer category).

### Quick Start

1. **Navigate**: Settings → Developer → Database
2. **View Connection Strength**: Colored indicator shows connection health
   - 🟢 Green: Excellent (< 50ms ping)
   - 🟡 Yellow: Good (50-100ms ping)
   - 🟠 Orange: Fair (100-200ms ping)
   - 🔴 Red: Poor (> 200ms ping or connection failure)
3. **Test Connection**: Click "Test Connection" button to refresh
4. **View Details**: Hover over indicator for ping time and server info

### Common Tasks

#### Diagnose Slow Application Performance

```text
1. Open Database Connection Monitor
2. Check connection strength indicator
3. If Red/Orange:
   - Network issue between client and database server
   - Database server under heavy load
   - Firewall/VPN introducing latency
4. If Green but application still slow:
   - Problem is query performance, not connection
   - Use Debug Dashboard to identify slow stored procedures
```

---

## Workflow Examples

### Workflow 1: Add Override for Legacy Procedure

**Goal**: Enable calling a legacy procedure without refactoring it.

```text
1. Developer discovers old procedure in code: sys_legacy_import
2. Check parameters in Schema Inspector:
   - Stored Procedures tab → sys_legacy_import
   - Parameters show: in_FileID, in_ImportType, p_Status, p_ErrorMsg
3. Notice non-standard in_ prefix on input parameters
4. Add overrides in Parameter Prefix Maintenance:
   - Procedure: sys_legacy_import, Parameter: FileID, Prefix: in_
   - Procedure: sys_legacy_import, Parameter: ImportType, Prefix: in_
5. Write C# code normally:
   var params = new Dictionary<string, object>
   {
       ["FileID"] = fileId,        // Helper converts to in_FileID
       ["ImportType"] = importType // Helper converts to in_ImportType
   };
6. Code works without modifying stored procedure
7. Add to Phase 2.5 backlog: Refactor sys_legacy_import to use p_ prefix
```

### Workflow 2: Scaffold New Feature DAO Methods

**Goal**: Quickly implement DAO layer for new feature.

```text
1. DBA creates 3 new stored procedures:
   - ftr_newfeature_Get_Items
   - ftr_newfeature_Add_Item
   - ftr_newfeature_Update_Item
2. Developer opens Code Generator
3. Select "ftr_newfeature_Get_Items" → Generate → Copy → Paste into Dao_NewFeature.cs
4. Select "ftr_newfeature_Add_Item" → Generate → Copy → Paste into Dao_NewFeature.cs
5. Select "ftr_newfeature_Update_Item" → Generate → Copy → Paste into Dao_NewFeature.cs
6. Build project: dotnet build -c Debug
7. 3 DAO methods implemented in 5 minutes vs 30 minutes manual coding
8. Proceed to UI layer implementation
```

### Workflow 3: Debug Phase 2.5 Refactoring Issue

**Goal**: Identify why refactored procedure is failing.

```text
1. Enable Debug Dashboard → Database Operations + Performance tracing
2. Set Debug Level to "High"
3. Reproduce issue in main application
4. Debug Dashboard shows:
   [DB] Executing: inv_inventory_Update_Location
   [DB] Parameters: InventoryID=12345, NewLocation=FLOOR
   [ERROR] MySqlException: Unknown column 'p_InventoryID' in 'procedure'
5. Aha! Stored procedure was refactored but parameter prefix changed
6. Check Schema Inspector → inv_inventory_Update_Location parameters
7. Parameters show: LocationID (no prefix), NewLocation (no prefix)
8. Two options:
   a) Add overrides for LocationID and NewLocation with empty prefix
   b) Update stored procedure to use p_ prefix (preferred)
9. Proceed with option (b): Update procedure, redeploy, retest
10. Debug Dashboard shows:
    [DB] Executing: inv_inventory_Update_Location
    [DB] Parameters: p_InventoryID=12345, p_NewLocation=FLOOR
    [DB] Result: p_Status=0, p_ErrorMsg=Success
```

### Workflow 4: Analyze Refactoring Impact

**Goal**: Understand which code will be affected by refactoring a procedure.

```text
1. Open Procedure Call Hierarchy
2. Search for "inv_inventory_Add_Item" (procedure to refactor)
3. Note C# call sites:
   - Data/Dao_Inventory.cs (line 145)
   - Forms/Transactions/AddInventoryForm.cs (line 289)
   - Services/Service_InventoryImport.cs (line 67)
4. Note called procedures:
   - inv_inventory_Validate_Part
   - md_part_ids_Get_Details
   - inv_history_Add_Record
5. Create test plan:
   - Test add inventory from AddInventoryForm (direct call)
   - Test bulk import from Service_InventoryImport (indirect call)
   - Verify history records created (dependency)
   - Validate part validation still works (dependency)
6. Refactor with confidence knowing all affected areas
```

---

## Troubleshooting

### Issue: Developer Category Not Visible

**Symptoms**: Settings form opens but no Developer node in TreeView.

**Causes**:
1. User lacks Developer role
2. Application not restarted after granting Developer role

**Solutions**:
```sql
-- 1. Verify role
SELECT UserName, IsAdmin, IsDeveloper FROM usr_users WHERE UserName = 'JOHNK';

-- 2. Grant role if missing
UPDATE usr_users SET IsDeveloper = 1 WHERE UserName = 'JOHNK';

-- 3. Restart application
-- 4. Log in again
-- 5. Developer category should now appear
```

### Issue: Procedure Call Hierarchy Shows "Artifacts Not Found"

**Symptoms**: "Call hierarchy artifacts not found. Click Regenerate to create them."

**Causes**:
1. First-time use (artifacts never generated)
2. Artifacts deleted or moved
3. Running from different directory

**Solutions**:
```powershell
# Option 1: Click "Regenerate" button in UI (easiest)

# Option 2: Run script manually
cd Database
.\2-Trace-Complete-CallHierarchy-v2.ps1

# Option 3: Verify file locations
Test-Path "Database/call-hierarchy-complete.json"
Test-Path "Database/STORED_PROCEDURE_CALLSITES.csv"

# If files exist but error persists, check application BaseDirectory:
# Application expects files in: bin/Debug/net8.0-windows/Database/
# Copy files from Database/ to bin/Debug/net8.0-windows/Database/
```

### Issue: Schema Inspector Shows "Connection Failure"

**Symptoms**: "Failed to load schema metadata. Retry?"

**Causes**:
1. Database server offline
2. Connection string incorrect
3. Network issue

**Solutions**:
```text
1. Check Database Connection Monitor (Settings → Developer → Database)
2. If Red indicator:
   - Verify MAMP MySQL is running
   - Test connection: mysql -h localhost -u root -p
3. If Yellow/Orange indicator:
   - Slow connection, wait and retry
4. Click "Retry" button in Schema Inspector
5. If persists, check Helper_Database_Variables.GetConnectionString()
```

### Issue: Code Generator Produces Non-Compiling Code

**Symptoms**: Generated code has build errors after pasting into DAO.

**Causes**:
1. Return type mismatch (procedure returns multiple result sets)
2. Parameter types don't match C# types
3. Missing namespace imports

**Solutions**:
```csharp
// 1. Check return type - may need adjustment
// If procedure returns multiple result sets:
public static async Task<DaoResult<DataSet>> MethodAsync(...) // Use DataSet not DataTable

// 2. Check parameter types - generator uses best guess
// Adjust types as needed:
// INT → int or long depending on size
// VARCHAR → string
// DATETIME → DateTime
// DECIMAL → decimal

// 3. Add missing usings at top of file:
using System.Data;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
```

### Issue: Parameter Prefix Override Not Taking Effect

**Symptoms**: Added override but stored procedure call still fails with "Unknown column" error.

**Causes**:
1. Application not restarted after adding override
2. Override cache not refreshed
3. Typo in procedure or parameter name

**Solutions**:
```text
1. Restart application (override cache loads at startup)
2. Verify override details match exactly:
   - Procedure name case-sensitive: "inv_inventory_Add_Item" not "inv_Inventory_add_item"
   - Parameter name without prefix: "UserID" not "p_UserID"
3. Check Debug Dashboard for parameter name used:
   [DB] Parameters: p_UserID=12345 (should match override)
4. If mismatch, edit override and restart
```

---

## Performance Tips

1. **Debug Dashboard**: Use "Pause Capture" when not actively debugging to reduce overhead
2. **Schema Inspector**: Don't refresh unnecessarily - INFORMATION_SCHEMA queries are slow
3. **Procedure Call Hierarchy**: Artifacts load once at control initialization - no repeated file I/O
4. **Code Generator**: Generation is fast (<1 second) - no caching needed
5. **Parameter Overrides**: Cache loads once at startup - no per-call database queries

---

## Security Notes

### Developer Role Access Control
- Developer tools intentionally hidden from non-developers
- Production databases should have minimal Developer role grants
- Override table allows tracking who added/modified each override (audit trail)

### Production Warning Dialog
When accessing developer tools in Production (Release build):

```text
⚠️ WARNING: You are accessing Developer Tools in Production mode.

This should only be done for troubleshooting active issues.
Developer operations are logged and may impact performance.

Continue?  [Yes] [No]
```

### Sensitive Data
- Debug Dashboard output may contain business data (UserID, part numbers, etc.)
- Debug logs should be treated as sensitive - don't commit to source control
- Schema Inspector reveals database structure - acceptable for authorized developers only

---

## Next Steps

1. **Enable Developer Role**: Grant IsDeveloper flag to your user account
2. **Generate Artifacts**: Run call hierarchy script if not already done
3. **Explore Each Tool**: Spend 5-10 minutes with each tool to understand capabilities
4. **Integrate into Workflow**: Use tools during Phase 2.5 refactoring tasks
5. **Provide Feedback**: Report issues or enhancement requests to development team

---

## Additional Resources

- **Phase 2.5 Specification**: `specs/002-003-database-layer-complete/spec.md`
- **Developer Tools Specification**: `specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/spec.md`
- **Data Model Documentation**: `specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/data-model.md`
- **Stored Procedure Contracts**: `specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/contracts/stored-procedures.md`
- **UserControl API Documentation**: `specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/contracts/usercontrol-api.md`

---

## Support

For issues with developer tools:
1. Check this quickstart guide first
2. Review error messages in Debug Dashboard
3. Check AGENTS.md for general troubleshooting patterns
4. Contact development team lead with specific error details
