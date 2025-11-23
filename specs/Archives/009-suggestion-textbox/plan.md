# Implementation Plan: Universal Suggestion System for TextBox Inputs

**Branch**: `001-suggestion-textbox` | **Date**: November 12, 2025 | **Spec**: [spec.md](./spec.md)  
**Input**: Feature specification from `/specs/001-suggestion-textbox/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Replace all existing ComboBox controls that use master data tables with a universal suggestion/autocomplete TextBox system. The system provides intelligent filtering, wildcard search (% symbol), keyboard and mouse navigation, and works with any database table (parts, operations, locations, customers, users, roles). This eliminates the need for pre-populated ComboBoxes, reduces memory usage, and provides better UX for large datasets. Implementation follows MTM patterns: Service_ErrorHandler for errors, LoggingUtility for events, Model_Dao_Result for data operations, and ThemedForm integration for visual consistency.

## Technical Context

**Language/Version**: C# 12.0 (.NET 8.0 Windows Forms)  
**Primary Dependencies**: 
- MySql.Data 9.4.0 (MySQL connector)
- Microsoft.Extensions.DependencyInjection 8.0.0 (DI container)
- Microsoft.Extensions.Logging 8.0.0 (logging infrastructure)

**Storage**: MySQL 5.7.24 (LEGACY - no JSON functions, CTEs, window functions, or CHECK constraints)  
**Testing**: Manual testing only (no xUnit, Moq, or automated tests per user request)  
**Target Platform**: Windows 10/11 (x64), .NET 8.0 Desktop Runtime  
**Project Type**: Single WinForms desktop application  
**Performance Goals**: 
- Overlay display: <100ms (95% of queries)
- Filtering 1000 items: <50ms
- Memory per control: <10MB

**Constraints**: 
- Must integrate with existing ThemedForm system
- Must use Service_ErrorHandler (NO MessageBox.Show)
- Must use LoggingUtility (NO Console.WriteLine)
- All database operations must be async
- All DAO methods must return Model_Dao_Result<T>
- MySQL 5.7.24 limitations (no modern SQL features)

**Scale/Scope**: 
- 24 form/control fields requiring conversion
- 6 master data tables (md_part_ids, md_operation_numbers, md_locations, md_item_types, usr_users, sys_roles)
- 3 main tabs (Inventory, Transfer, Remove) + Settings form
- Estimated 1000-10,000 items per master data table

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### ✅ PASSED: Centralized Error Handling

**Requirement**: All exceptions MUST be handled through Service_ErrorHandler (no MessageBox.Show)

**Plan Compliance**:
- SuggestionTextBox will use `Service_ErrorHandler.HandleException()` for all errors
- SuggestionOverlay will use `Service_ErrorHandler.ShowUserError()` for user-facing messages
- Data provider errors will use `Service_ErrorHandler.HandleDatabaseError()`
- No direct MessageBox.Show() calls planned

**Evidence**: spec.md FR-029 states "System MUST use Service_ErrorHandler for all exceptions (NO MessageBox.Show)"

---

### ✅ PASSED: Structured Logging with CSV Format

**Requirement**: All logging MUST use LoggingUtility methods with thread-safe CSV formatting

**Plan Compliance**:
- LoggingUtility.Log() for user actions (overlay opened, item selected, cancelled)
- LoggingUtility.LogApplicationInfo() for suggestion system initialization
- LoggingUtility.LogApplicationError() for component exceptions
- LoggingUtility.LogDatabaseError() for data provider failures

**Evidence**: spec.md FR-030 states "System MUST use LoggingUtility for event tracking (structured CSV logging)"

---

### ✅ PASSED: Model_Dao_Result Pattern for Data Layer

**Requirement**: All DAO methods MUST return Model_Dao_Result<T>

**Plan Compliance**:
- Data providers will call existing DAOs (Dao_Part, Dao_Operation, Dao_Location, etc.)
- All existing DAOs already return Model_Dao_Result<DataTable>
- SuggestionTextBox will check `result.IsSuccess` before processing data
- Errors will be handled via Service_ErrorHandler when `result.IsFailure`

**Evidence**: spec.md states suggestion system works with existing DAO layer which already follows Model_Dao_Result pattern

---

### ✅ PASSED: Async-First Database Operations

**Requirement**: All database operations MUST be async

**Plan Compliance**:
- Data provider delegate signature: `Func<Task<List<string>>>`
- SuggestionTextBox will use `await` for all data operations
- Overlay display will use async/await pattern: `await LoadSuggestionsAsync()`
- FR-024 explicitly requires async operations

**Evidence**: spec.md FR-024 states "System MUST use async operations for data loading to prevent UI blocking"

---

### ✅ PASSED: WinForms Best Practices

**Requirement**: UI thread marshaling, proper disposal, form lifecycle management

**Plan Compliance**:
- SuggestionOverlay will inherit from ThemedForm (proper disposal built-in)
- SuggestionTextBox will implement IDisposable pattern (FR-031)
- Async operations will use ConfigureAwait(false) where appropriate
- UI updates will use Invoke() if called from background threads

**Evidence**: spec.md FR-031 states "System MUST implement IDisposable pattern for proper resource cleanup"

---

### ✅ PASSED: Stored Procedure Parameter Conventions

**Requirement**: Parameter prefix auto-detection, Model_Dao_Result<T> pattern

**Plan Compliance**:
- Data providers will use existing DAOs which already use Helper_Database_StoredProcedure
- No new stored procedures required (uses existing md_* table queries)
- Existing DAOs handle parameter prefixes automatically
- Status code conventions already followed by existing DAO layer

**Evidence**: Suggestion system integrates with existing DAO infrastructure, no direct stored procedure calls

---

### Constitution Compliance Summary

**Status**: ✅ ALL GATES PASSED

- No violations requiring justification
- All MTM patterns will be followed
- No complexity exceptions needed
- Ready to proceed to Phase 0 research

## Project Structure

### Documentation (this feature)

```text
specs/001-suggestion-textbox/
├── spec.md              # Feature specification (completed)
├── plan.md              # This file (implementation plan)
├── research.md          # Phase 0: Technical research and decisions
├── data-model.md        # Phase 1: Component model and relationships
├── quickstart.md        # Phase 1: Developer integration guide
├── contracts/           # Phase 1: Public API signatures
│   ├── SuggestionTextBox.cs        # Enhanced TextBox control interface
│   ├── SuggestionProvider.cs       # Data provider delegate pattern
│   ├── SuggestionConfig.cs         # Configuration model
│   └── SuggestionOverlay.cs        # Overlay form interface
├── tasks.md             # Phase 2: Implementation task breakdown (NOT created yet)
└── checklists/
    └── requirements.md  # Specification quality checklist (completed)
```

### Source Code (repository root)

```text
MTM_WIP_Application_Winforms/
├── Controls/
│   └── Shared/
│       ├── SuggestionTextBox.cs          # NEW: Enhanced TextBox with suggestion support
│       ├── SuggestionTextBox.Designer.cs # NEW: Designer file
│       └── ThemedUserControl.cs          # EXISTING: Base for custom controls
├── Forms/
│   └── Shared/
│       ├── SuggestionOverlayForm.cs       # NEW: Modal overlay for suggestions
│       ├── SuggestionOverlayForm.Designer.cs
│       └── ThemedForm.cs                  # EXISTING: Base for forms
├── Services/
│   ├── Service_ErrorHandler.cs            # EXISTING: Error handling
│   ├── Service_SuggestionFilter.cs        # NEW: Filtering and sorting logic
│   └── Service_SuggestionProvider.cs      # NEW: Data provider abstraction
├── Models/
│   ├── Model_Dao_Result.cs                # EXISTING: DAO result wrapper
│   ├── Model_Suggestion_Config.cs         # NEW: Suggestion configuration
│   └── Model_Suggestion_Item.cs           # NEW: Suggestion data model
├── Helpers/
│   ├── Helper_Database_StoredProcedure.cs # EXISTING: Database access
│   └── Helper_UI_Shortcuts.cs             # EXISTING: UI utilities (may need updates)
├── Data/
│   ├── Dao_Part.cs                        # EXISTING: Part data access
│   ├── Dao_Operation.cs                   # EXISTING: Operation data access
│   ├── Dao_Location.cs                    # EXISTING: Location data access
│   ├── Dao_ItemType.cs                    # EXISTING: Item type data access
│   ├── Dao_User.cs                        # EXISTING: User data access
│   └── Dao_System.cs                      # EXISTING: System/role data access
└── [Integration Points - MODIFY EXISTING]
    ├── Controls/MainForm/
    │   ├── Control_InventoryTab.cs        # MODIFY: Replace ComboBoxes with SuggestionTextBox
    │   ├── Control_AdvancedInventory.cs   # MODIFY: Replace ComboBoxes
    │   ├── Control_TransferTab.cs         # MODIFY: Replace ComboBoxes
    │   ├── Control_RemoveTab.cs           # MODIFY: Replace ComboBoxes
    │   └── Control_AdvancedRemove.cs      # MODIFY: Replace ComboBoxes
    ├── Forms/Settings/
    │   └── SettingsForm.cs                # MODIFY: Replace ComboBoxes in part/user management
    └── Forms/Shared/
        └── Form_QuickButtonEdit.cs        # MODIFY: Replace ComboBoxes for part/operation
```

**Structure Decision**: Single WinForms desktop application with layered architecture. New components added to existing folders following MTM naming conventions:
- Controls/Shared/ for reusable UI controls (SuggestionTextBox)
- Forms/Shared/ for popup forms (SuggestionOverlayForm)
- Services/ for business logic (filtering, data provider abstraction)
- Models/ for data models and configuration
- Integration via modifications to 7 existing form/control files

## Complexity Tracking

> **No violations detected - all MTM patterns followed**

This section is empty because no constitution violations require justification. The implementation follows all MTM standards without exceptions.

