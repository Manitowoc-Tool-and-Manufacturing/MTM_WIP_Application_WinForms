# SuggestionTextBox Constitution Compliance Audit

## OBJECTIVE
Audit an existing file that uses SuggestionTextBox controls to ensure full compliance with the MTM constitution. This prompt performs a comprehensive review and refactors code to meet all standards without losing any logic. It also generates a proposed improvements section for enhancements beyond the audit scope.

## INPUT REQUIREMENTS

Before executing this prompt:
1. **Target File**: Full path to the .cs file to audit (must contain SuggestionTextBox controls)
2. **Audit Scope**: Full audit or specific focus areas (error handling, logging, async patterns, etc.)

## EXECUTION STEPS

### Step 1: Read Constitution and Instructions

<tool>read_file</tool> on:
```
c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.specify\memory\constitution.md
c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.github\instructions\suggestiontextbox-implementation.instructions.md
{target_file}
```

### Step 2: Analyze Current Implementation

Create a checklist of constitution requirements:

#### 2.1: Error Handling (Principle I)
- [ ] All exceptions caught and handled
- [ ] Service_ErrorHandler used (NEVER MessageBox.Show directly)
- [ ] Appropriate severity levels (Low, Medium, High, Fatal)
- [ ] Context dictionaries provided for debugging
- [ ] Specialized handlers used (HandleDatabaseError, HandleValidationError, etc.)
- [ ] ShowWarning/ShowInformation/ShowConfirmation for non-errors

#### 2.2: Structured Logging (Principle II)
- [ ] LoggingUtility.Log() used for operations
- [ ] LoggingUtility.LogApplicationError() for app errors
- [ ] LoggingUtility.LogDatabaseError() for DB errors
- [ ] No Console.WriteLine() or Debug.WriteLine() (except in LoggingUtility itself)
- [ ] Meaningful log messages with context

#### 2.3: Model_Dao_Result Pattern (Principle III)
- [ ] All DAO methods return Model_Dao_Result<T>
- [ ] Result.IsSuccess checked before using Result.Data
- [ ] Result.ErrorMessage used for user-friendly errors
- [ ] No direct exception throwing from DAO layer

#### 2.4: Async-First Database Operations (Principle IV)
- [ ] All database operations use async/await
- [ ] No .Result or .Wait() blocking calls
- [ ] Async event handlers use async void pattern correctly
- [ ] Proper exception handling in async methods

#### 2.5: WinForms Best Practices (Principle V)
- [ ] Event handlers subscribed and unsubscribed properly
- [ ] Dispose() method properly implemented
- [ ] No memory leaks from event subscriptions
- [ ] Proper null-conditional operators used

#### 2.6: Stored Procedure Parameter Conventions (Principle VI)
- [ ] Parameters passed WITHOUT prefixes in C# (helper adds them)
- [ ] Model_ParameterPrefix_Cache used for auto-detection
- [ ] Standard output parameters (p_Status, p_ErrorMsg) checked
- [ ] Status code conventions followed (1=success with data, 0=success no data, negative=error)

#### 2.7: XML Documentation Standards (Principle VII)
- [ ] ALL public members have XML documentation
- [ ] `<summary>` tags on all public classes, methods, properties, events
- [ ] `<param>` tags for each method parameter
- [ ] `<returns>` tags for methods returning values (including Model_Dao_Result usage guidance)
- [ ] `<exception>` tags for documented exceptions
- [ ] `<remarks>` tags for complex scenarios where needed
- [ ] Inline comments (`//`) only for non-obvious logic, workarounds, complex algorithms

#### 2.8: Null Safety Requirements (Principle VIII)
- [ ] Nullable reference types enabled (`<Nullable>enable</Nullable>`)
- [ ] `?` suffix used for nullable reference types (string?, DataRow?)
- [ ] Null-conditional operators (`?.`) used for safe access
- [ ] Null-coalescing (`??`) used appropriately
- [ ] DBNull.Value checked before accessing DataRow/DataTable columns
- [ ] Column existence verified with DataTable.Columns.Contains() before access
- [ ] ArgumentNullException.ThrowIfNull() or ArgumentNullException.ThrowIfNullOrEmpty() for parameter validation

#### 2.9: Theme System Integration (Principle IX)
- [ ] Forms inherit from ThemedForm (NEVER Form directly)
- [ ] User controls inherit from ThemedUserControl (NEVER UserControl directly)
- [ ] NO manual BackColor, ForeColor, or Font assignment
- [ ] NO direct MessageBox.Show() - use Service_ErrorHandler for themed dialogs
- [ ] Theme tokens used for all styling

#### 2.10: Resource Disposal (Principle X)
- [ ] All IDisposable resources properly disposed
- [ ] `using` statements for database connections, readers, streams
- [ ] Dispose(bool disposing) override in forms/controls
- [ ] ALL event handlers unsubscribed in Dispose() to prevent memory leaks
- [ ] DataTables disposed after use
- [ ] Try-catch in Dispose() to prevent exceptions during cleanup

#### 2.11: Input Validation Service (Principle XI)
- [ ] ALL user input validated through Service_Validation before database operations
- [ ] Validation at UI layer BEFORE calling DAOs
- [ ] Service_ErrorHandler.HandleValidationError() used for validation errors
- [ ] Standard transformations applied (trim whitespace, ToUpperInvariant for codes)
- [ ] Registered validators used for specific field types
- [ ] Custom validators implement IValidator interface

#### 2.12: Code Organization
- [ ] Proper #region structure (Fields, Events, Constructors, etc.)
- [ ] Methods in appropriate regions
- [ ] Consistent naming conventions

### Step 3: Create Audit Report

Generate a detailed audit report:

```markdown
# Constitution Compliance Audit Report
**File**: {target_file}
**Date**: {current_date}
**Auditor**: AI Agent
**Version**: v1.0

## Executive Summary
- **Overall Compliance**: {percentage}%
- **Critical Issues**: {count}
- **Warnings**: {count}
- **Suggestions**: {count}

## Principle I: Error Handling
### Critical Issues
- [ ] Line {X}: Direct MessageBox.Show() usage - MUST use Service_ErrorHandler
- [ ] Line {Y}: Exception swallowed without logging

### Warnings
- [ ] Line {X}: Missing context dictionary for exception
- [ ] Line {Y}: Generic error severity - could be more specific

### Compliant
- ✅ Lines {X-Y}: Proper Service_ErrorHandler usage
- ✅ Line {Z}: Good context enrichment

## Principle II: Structured Logging
### Critical Issues
- [ ] Line {X}: Console.WriteLine() used - MUST use LoggingUtility

### Warnings
- [ ] Line {X}: Log message lacks context

### Compliant
- ✅ Lines {X-Y}: Proper LoggingUtility usage
- ✅ Line {Z}: Meaningful log messages

## Principle III: Model_Dao_Result Pattern
{Similar detailed breakdown}

## Principle IV: Async-First Database Operations
{Similar detailed breakdown}

## Principle V: WinForms Best Practices
{Similar detailed breakdown}

## Principle VI: Stored Procedure Parameter Conventions
{Similar detailed breakdown}

## Principle VII: XML Documentation Standards
### Critical Issues
- [ ] Line {X}: Public method missing XML documentation - MUST add <summary>
- [ ] Line {Y}: Method parameters missing <param> tags

### Warnings
- [ ] Line {X}: <returns> tag missing Model_Dao_Result usage guidance
- [ ] Line {Y}: Complex method missing <remarks> tag

### Compliant
- ✅ Lines {X-Y}: Comprehensive XML documentation
- ✅ Line {Z}: Model_Dao_Result return documented properly

## Principle VIII: Null Safety Requirements
### Critical Issues
- [ ] Line {X}: Nullable reference types not enabled in project file
- [ ] Line {Y}: DataRow access without DBNull.Value check - potential crash

### Warnings
- [ ] Line {X}: Could use null-conditional operator (?.)
- [ ] Line {Y}: Missing column existence check

### Compliant
- ✅ Lines {X-Y}: Proper null-safe DataRow access
- ✅ Line {Z}: Good use of null-coalescing operator

## Principle IX: Theme System Integration
### Critical Issues
- [ ] Line {X}: Inherits from Form directly - MUST inherit from ThemedForm
- [ ] Line {Y}: Direct MessageBox.Show() - MUST use Service_ErrorHandler

### Warnings
- [ ] Line {X}: Manual BackColor assignment - should use theme tokens

### Compliant
- ✅ Line {X}: Properly inherits from ThemedForm
- ✅ Line {Y}: Uses Service_ErrorHandler for dialogs

## Principle X: Resource Disposal
### Critical Issues
- [ ] Line {X}: Event handler not unsubscribed in Dispose() - memory leak!
- [ ] Line {Y}: Database connection not disposed - resource leak!

### Warnings
- [ ] Line {X}: DataTable not disposed after use
- [ ] Line {Y}: Missing try-catch in Dispose() method

### Compliant
- ✅ Lines {X-Y}: All event handlers properly unsubscribed
- ✅ Line {Z}: using statement for database resources

## Principle XI: Input Validation Service
### Critical Issues
- [ ] Line {X}: Direct DAO call without validation - MUST validate first
- [ ] Line {Y}: Input validation missing for user input field

### Warnings
- [ ] Line {X}: Could use Service_Validation registered validator
- [ ] Line {Y}: Missing input transformation (trim, uppercase)

### Compliant
- ✅ Lines {X-Y}: Proper Service_Validation usage before DAO
- ✅ Line {Z}: Good use of HandleValidationError()

## Code Organization
{Similar detailed breakdown}
```

### Step 4: Fix Critical Issues

Address all CRITICAL issues first:

#### 4.1: Replace MessageBox.Show
```csharp
// ❌ CRITICAL ISSUE
MessageBox.Show("Error occurred!");

// ✅ FIX
Service_ErrorHandler.ShowWarning("Error occurred!");
```

#### 4.2: Fix Form/UserControl Inheritance
```csharp
// ❌ CRITICAL ISSUE - Direct Form inheritance
public partial class MyForm : Form
{
    public MyForm()
    {
        InitializeComponent();
        this.BackColor = Color.White; // Manual styling
    }
}

// ✅ FIX - Inherit from ThemedForm
public partial class MyForm : ThemedForm
{
    public MyForm()
    {
        InitializeComponent();
        // Theme automatically applied by base class
        // NO manual color/font setting needed
    }
}
```

#### 4.3: Add Input Validation Before DAO Calls
```csharp
// ❌ CRITICAL ISSUE - No validation before DAO
private async void SaveButton_Click(object? sender, EventArgs e)
{
    await Dao_Part.InsertAsync(partNumberTextBox.Text, quantityTextBox.Text);
}

// ✅ FIX - Validate first
private async void SaveButton_Click(object? sender, EventArgs e)
{
    // Validate part number
    var partResult = Service_Validation.Validate("PartNumber", partNumberTextBox.Text, "Part Number");
    if (partResult.IsFailure)
    {
        Service_ErrorHandler.HandleValidationError(
            partResult.ErrorMessage,
            field: partResult.FieldName);
        partNumberTextBox.Focus();
        return;
    }

    // Validate quantity
    var qtyResult = Service_Validation.Validate("Quantity", quantityTextBox.Text, "Quantity");
    if (qtyResult.IsFailure)
    {
        Service_ErrorHandler.HandleValidationError(qtyResult.ErrorMessage);
        quantityTextBox.Focus();
        return;
    }

    // All validations passed - safe to call DAO
    await Dao_Part.InsertAsync(partNumberTextBox.Text, quantityTextBox.Text);
}
```

#### 4.5: Add Missing Error Handling
```csharp
// ❌ CRITICAL ISSUE - No error handling
private async void SaveButton_Click(object sender, EventArgs e)
{
    await SaveDataAsync();
}

// ✅ FIX
private async void SaveButton_Click(object? sender, EventArgs e)
{
    try
    {
        await SaveDataAsync();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
            contextData: new Dictionary<string, object>
            {
                ["Operation"] = "SaveData"
            },
            controlName: nameof(MyControl),
            callerName: nameof(SaveButton_Click));
    }
}
```

#### 4.6: Replace Console.WriteLine
```csharp
// ❌ CRITICAL ISSUE
Console.WriteLine($"Part selected: {part}");

// ✅ FIX
LoggingUtility.Log($"[{nameof(MyControl)}] Part selected: {part}");
```

#### 4.7: Fix Blocking Async Calls
```csharp
// ❌ CRITICAL ISSUE
var result = GetDataAsync().Result; // Blocks UI thread!

// ✅ FIX
var result = await GetDataAsync();
```

#### 4.8: Add Missing XML Documentation
```csharp
// ❌ CRITICAL ISSUE - No XML documentation on public method
public async Task<Model_Dao_Result<DataTable>> GetPartDataAsync(string partId)
{
    // implementation
}

// ✅ FIX - Comprehensive XML documentation
/// <summary>
/// Retrieves part data from database by part ID.
/// </summary>
/// <param name="partId">The unique part identifier to search for</param>
/// <returns>
/// Model_Dao_Result containing DataTable with part data on success.
/// Check IsSuccess before accessing Data.
/// ErrorMessage contains user-friendly message on failure.
/// </returns>
public async Task<Model_Dao_Result<DataTable>> GetPartDataAsync(string partId)
{
    // implementation
}
```

#### 4.9: Fix Null Safety Issues
```csharp
// ❌ CRITICAL ISSUE - No DBNull check, no column existence check
private void LoadPartData(DataRow row)
{
    string partId = row["PartID"].ToString(); // Can throw NullReferenceException!
}

// ✅ FIX - Null-safe with DBNull and column checks
private void LoadPartData(DataRow? row)
{
    if (row == null)
    {
        return;
    }

    if (row.Table.Columns.Contains("PartID"))
    {
        var partIdValue = row["PartID"];
        string partId = partIdValue != DBNull.Value 
            ? partIdValue.ToString() ?? string.Empty 
            : string.Empty;
    }
}
```

#### 4.10: Fix Event Handler Memory Leaks
```csharp
// ❌ CRITICAL ISSUE - Events not unsubscribed in Dispose()
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        components?.Dispose();
    }
    base.Dispose(disposing);
}

// ✅ FIX - Properly unsubscribe all events
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        try
        {
            // Unsubscribe from ALL events to prevent memory leaks
            if (saveButton != null)
            {
                saveButton.Click -= SaveButton_Click;
            }

            if (partSuggestionTextBox != null)
            {
                partSuggestionTextBox.SuggestionSelected -= Part_SuggestionSelected;
            }

            // Dispose components
            components?.Dispose();
        }
        catch (Exception ex)
        {
            // Log but don't throw during disposal
            LoggingUtility.LogApplicationError(ex);
        }
    }

    base.Dispose(disposing);
}
```

### Step 5: Fix Warnings

Address warning-level issues:

#### 5.1: Add Context Dictionaries
```csharp
// ⚠️ WARNING - Minimal context
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
        controlName: nameof(MyControl));
}

// ✅ IMPROVED
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
        contextData: new Dictionary<string, object>
        {
            ["Operation"] = "LoadPartDetails",
            ["SelectedPart"] = selectedPart,
            ["User"] = Model_Application_Variables.User
        },
        controlName: nameof(MyControl),
        callerName: nameof(LoadPartDetails));
}
```

#### 5.2: Improve Error Severity Classification
```csharp
// ⚠️ WARNING - Generic severity
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, ...);
}

// ✅ IMPROVED - More specific
catch (MySqlException ex)
{
    // Database errors should be High severity
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.High, ...);
}
catch (Exception ex)
{
    // General errors can be Medium
    Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, ...);
}
```

#### 5.3: Enhance Log Messages
```csharp
// ⚠️ WARNING - Vague message
LoggingUtility.Log("Data loaded");

// ✅ IMPROVED - Specific and contextual
LoggingUtility.Log($"[{nameof(MyControl)}] Part '{selectedPart}' loaded with {rowCount} operations");
```

### Step 6: Improve Code Organization

#### 6.1: Reorganize Regions
Ensure proper order:
```csharp
#region Fields
  // Private fields only
#endregion

#region Events
  // Public event declarations
#endregion

#region Constructors
  // Constructor(s)
#endregion

#region Initialization
  // Setup methods called from constructor
  ConfigureSuggestionTextBoxes()
  WireUpEventHandlers()
#endregion

#region Events
  // Event handler implementations
  XXX_Click(object? sender, EventArgs e)
  XXX_SuggestionSelected(object? sender, SuggestionSelectedEventArgs e)
#endregion

#region Data Providers
  // SuggestionTextBox data providers
  GetXXXSuggestionsAsync()
#endregion

#region Validation
  // Validation methods
  ValidateXXX()
#endregion

#region Methods
  // Public methods
  // Protected methods
  // Private methods
#endregion

#region Helpers
  // Private helper methods
#endregion

#region Cleanup
  // Dispose() override
#endregion
```

#### 6.2: Add Missing XML Documentation
```csharp
// ⚠️ WARNING - No XML documentation
private async Task SaveDataAsync()
{
    // implementation
}

// ✅ IMPROVED
/// <summary>
/// Saves form data to database using DAO layer.
/// Validates inputs before saving and shows success/error messages.
/// </summary>
/// <returns>Task representing the async save operation</returns>
private async Task SaveDataAsync()
{
    // implementation
}
```

### Step 7: Remove Obsolete/Dead Code

Identify and remove:
- [ ] Commented-out code blocks (unless marked as backup)
- [ ] Unused private methods
- [ ] Unused fields/properties
- [ ] Duplicate logic
- [ ] Debug code left in accidentally

**IMPORTANT**: Only remove code you're certain is unused. When in doubt, leave it.

### Step 8: Enhance Null Safety

#### 8.1: Add Null-Conditional Operators
```csharp
// ⚠️ POTENTIAL NULL REFERENCE
string partId = _currentPart["PartID"].ToString();

// ✅ NULL-SAFE
string partId = _currentPart["PartID"]?.ToString() ?? string.Empty;
```

#### 8.2: Add DBNull Checks
```csharp
// ⚠️ POTENTIAL DBNULL ISSUE
bool requiresColorCode = Convert.ToBoolean(_currentPart["RequiresColorCode"]);

// ✅ DBNULL-SAFE
var colorCodeValue = _currentPart["RequiresColorCode"];
bool requiresColorCode = colorCodeValue != DBNull.Value && Convert.ToBoolean(colorCodeValue);
```

#### 8.3: Add Nullable Reference Checks
```csharp
// ⚠️ WARNING - Assumes not null
private void UpdateUI(DataRow data)
{
    labelValue.Text = data["Value"].ToString(); // Could throw NullReferenceException
}

// ✅ NULL-SAFE
private void UpdateUI(DataRow? data)
{
    if (data == null)
    {
        return;
    }

    labelValue.Text = data["Value"]?.ToString() ?? string.Empty;
}
```

### Step 9: Verify Dispose Pattern

Ensure proper cleanup:

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        try
        {
            // Unsubscribe from ALL events
            if (partSuggestionTextBox != null)
            {
                partSuggestionTextBox.SuggestionSelected -= PartSuggestionTextBox_SuggestionSelected;
            }

            if (operationSuggestionTextBox != null)
            {
                operationSuggestionTextBox.SuggestionSelected -= OperationSuggestionTextBox_SuggestionSelected;
            }

            if (saveButton != null)
            {
                saveButton.Click -= SaveButton_Click;
            }

            // Dispose any IDisposable fields
            _someDisposableResource?.Dispose();
        }
        catch (Exception ex)
        {
            // Log but don't throw during disposal
            LoggingUtility.LogApplicationError(ex);
        }
    }

    base.Dispose(disposing);
}
```

### Step 10: Performance Optimizations

Identify opportunities:

#### 10.1: List Capacity Hints
```csharp
// ⚠️ SUBOPTIMAL - List grows dynamically
var suggestions = new List<string>();

// ✅ OPTIMIZED - Pre-allocate capacity
var suggestions = new List<string>(result.Data.Rows.Count);
```

#### 10.2: String Concatenation
```csharp
// ⚠️ SUBOPTIMAL - Multiple string allocations
string message = "Part " + partId + " selected";

// ✅ OPTIMIZED - Single allocation
string message = $"Part {partId} selected";
```

### Step 11: Build and Verify

```powershell
dotnet build
```

Ensure:
- [ ] No compilation errors
- [ ] No new warnings introduced
- [ ] All changes backward compatible

### Step 12: Add Audit Results Reference (Optional)

If desired, add a minimal reference block to audited file indicating compliance status:

```csharp
#region AUDIT_RESULTS

/*
 * ═══════════════════════════════════════════════════════════════════════════════
 * CONSTITUTION COMPLIANCE AUDIT
 * ═══════════════════════════════════════════════════════════════════════════════
 * Audit Date: {YYYY-MM-DD}
 * Compliance Score: {XX}%
 * Critical Issues Fixed: {COUNT}
 * Warnings Fixed: {COUNT}
 * 
 * This file is now FULLY COMPLIANT with MTM Constitution requirements.
 * 
 * For enhancement suggestions, run:
 *   suggestiontextbox-generate-suggestions.prompt.md
 * 
 * ═══════════════════════════════════════════════════════════════════════════════
 */

#endregion
```

**Note**: This reference block is optional. The audit itself is complete once code is compliant.

## OUTPUT FORMAT

### 1. Audit Report Summary
```
Constitution Compliance Audit Complete
File: {target_file}
Overall Compliance: {BEFORE}% → {AFTER}% ✅

Critical Issues Fixed: {COUNT}
- {ISSUE_1}
- {ISSUE_2}
- {ISSUE_3}

Warnings Fixed: {COUNT}
- {WARNING_1}
- {WARNING_2}
- {WARNING_3}

Code Organization Improvements: {COUNT}
- {IMPROVEMENT_1}
- {IMPROVEMENT_2}
- {IMPROVEMENT_3}
```

### 2. Before/After Metrics
```
Code Quality Metrics:
- Lines of Code: {BEFORE} → {AFTER}
- Methods: {BEFORE} → {AFTER}
- XML Documented Methods: {BEFORE} → {AFTER}
- Error Handlers: {BEFORE} → {AFTER}
- Context Dictionaries: {BEFORE} → {AFTER}
- Log Statements: {BEFORE} → {AFTER}

Constitution Compliance:
- Error Handling: {BEFORE}% → {AFTER}% ✅
- Structured Logging: {BEFORE}% → {AFTER}% ✅
- Async Patterns: {BEFORE}% → {AFTER}% ✅
- Code Organization: {BEFORE}% → {AFTER}% ✅
- Null Safety: {BEFORE}% → {AFTER}% ✅
```

### 3. Testing Checklist
```
Verification Required:
[ ] All critical fixes tested manually
[ ] No regressions in existing functionality
[ ] Error messages still user-friendly
[ ] Performance not degraded
[ ] Memory leaks addressed (events properly unsubscribed)
[ ] Build succeeds with no new errors/warnings
```

### 4. Next Steps
```
✅ Constitution compliance achieved
✅ Build successful with no errors
✅ Code quality improved

RECOMMENDED ACTIONS:
1. Manual testing of all affected workflows
2. Code review by team member
3. User acceptance testing

NEXT PROMPTS:
- For enhancement suggestions:
  Run "generate-suggestions.prompt.md"

- For more audits:
  Run "constitution-audit.prompt.md" on next file

- If all audits complete:
  Session complete - all SuggestionTextBox implementations are constitution-compliant
```

## VERSION
v1.0 - 2025-11-15

## RELATED PROMPTS
- `suggestiontextbox-implement-new.prompt.md` - Create new controls
- `suggestiontextbox-refactor-combobox.prompt.md` - Migrate ComboBox
- `suggestiontextbox-refactor-textbox.prompt.md` - Migrate TextBox
