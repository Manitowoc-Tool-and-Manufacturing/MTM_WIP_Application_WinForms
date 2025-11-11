# Implementation Plan: Transaction Viewer Form Redesign

**Branch**: `005-transaction-viewer-form` | **Date**: 2025-10-29 | **Spec**: [spec.md](spec.md)  
**Input**: Feature specification from `/specs/005-transaction-viewer-form/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Complete architectural redesign of the Transactions form to create a maintainable, reliable, and spec-compliant transaction viewing system. The existing 2136-line implementation will be replaced with a clean SOLID-based architecture using separate UserControls for search, grid display, and detail viewing. All database operations will use stored procedures via Helper_Database_StoredProcedure, error handling will route through Service_ErrorHandler, and async/await patterns will ensure UI responsiveness. The redesign targets <500 lines per file, 95%+ XML documentation coverage, and full compliance with MTM constitution principles.

## Technical Context

**Language/Version**: C# 12 / .NET 8.0 Windows Forms  
**Primary Dependencies**: MySql.Data 9.4.0, System.Text.Json, Microsoft.Web.WebView2, ClosedXML (Excel export)  
**Storage**: MySQL 5.7.24+ (MAMP compatible) - stored procedures only, no inline SQL permitted  
**Testing**: Manual validation testing (integration tests via BaseIntegrationTest pattern)  
**Target Platform**: Windows desktop (primary), with DPI scaling support for 100%-200%  
**Project Type**: Single Windows Forms desktop application  
**Performance Goals**: <2s transaction search (90-day range), <100ms UI interactions, <5s Excel export (1000 records)  
**Constraints**: Sub-100ms UI response, 30-second DB timeout, <500 lines per file, 95%+ XML docs coverage  
**Scale/Scope**: 24,000+ historical transaction records, 5 new UserControls, 1 ViewModel, 1 refactored DAO, 74+ existing stored procedures

## Constitution Check

_GATE: Must pass before Phase 0 research. Re-check after Phase 1 design._

### Initial Check (Pre-Phase 0)

| Principle                                                     | Status  | Justification                                                                                                                                                                   |
| ------------------------------------------------------------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **I. Stored Procedure Only Database Access**                  | ✅ PASS | Spec mandates `inv_transactions_Search`, `inv_transactions_SmartSearch`, `inv_transactions_GetAnalytics` via Helper_Database_StoredProcedure. No inline SQL.                    |
| **II. Model_Dao_Result<T> Wrapper Pattern**                          | ✅ PASS | DAO methods return `Model_Dao_Result<List<Model_Transactions_Core>>` and `Model_Dao_Result<DataTable>`. UI checks `IsSuccess` before accessing `Data`.                                             |
| **III. Region Organization and Method Ordering**              | ✅ PASS | All new files (Transactions.cs, UserControls, ViewModel) will follow standard 10-region organization. Target <500 lines per file enforces structure.                            |
| **IV. Manual Validation Testing Approach**                    | ✅ PASS | Spec includes 8 manual test scenarios (Basic Search, Part Number Search, Multi-Filter, Export, Error Handling, Pagination, Detail View, Performance). Success criteria defined. |
| **V. Environment-Aware Database Selection**                   | ✅ PASS | Feature uses `Helper_Database_Variables.GetConnectionString()` which respects Debug/Release configuration and machine IP detection.                                             |
| **VI. Async-First UI Responsiveness**                         | ✅ PASS | All database operations async (`SearchTransactionsAsync`, `LoadPartsDropdownAsync`). UI event handlers are async void. Progress reporting via Helper_StoredProcedureProgress.   |
| **VII. Centralized Error Handling with Service_ErrorHandler** | ✅ PASS | Spec mandates `Service_ErrorHandler.HandleException()` with severity classification, retry actions, and context data. Zero MessageBox.Show() calls.                             |
| **VIII. Documentation and XML Comments**                      | ✅ PASS | Spec requires 95%+ XML documentation coverage. All public classes, methods, properties have `<summary>`, `<param>`, `<returns>` tags. MCP tool `check_xml_docs` validates.      |

**Overall**: ✅ **PASS** - All constitution principles satisfied. No violations requiring justification in Complexity Tracking table.

**Re-check Required After Phase 1**: Verify data-model.md entities align with existing Model_Transactions_Core, contracts/ directory contains no new API endpoints (not applicable for WinForms), and research.md technical decisions don't introduce forbidden practices.

---

### Post-Phase 1 Re-Check

| Principle                                                     | Status  | Justification                                                                                                                                                                            |
| ------------------------------------------------------------- | ------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **I. Stored Procedure Only Database Access**                  | ✅ PASS | research.md confirms existing stored procedures used without modification. quickstart.md demonstrates Helper_Database_StoredProcedure pattern. No inline SQL detected.                   |
| **II. Model_Dao_Result<T> Wrapper Pattern**                          | ✅ PASS | data-model.md defines Model_Transactions_SearchResult wrapper. quickstart.md shows Model_Dao_Result<List<Model_Transactions_Core>> return types. ViewModel layer propagates Model_Dao_Result to Form layer.         |
| **III. Region Organization and Method Ordering**              | ✅ PASS | quickstart.md code examples follow standard region organization (#region Fields, #region Constructors, etc.). File size targets enforced (Form <500, UserControls <300, ViewModel <400). |
| **IV. Manual Validation Testing Approach**                    | ✅ PASS | quickstart.md includes 8-item manual validation checklist. Integration tests defined for DAO layer. ViewModel unit tests planned.                                                        |
| **V. Environment-Aware Database Selection**                   | ✅ PASS | quickstart.md confirms Debug builds use `mtm_wip_application_winforms_test`, Release uses `MTM_WIP_Application_Winforms`. No hardcoded connection strings.                               |
| **VI. Async-First UI Responsiveness**                         | ✅ PASS | quickstart.md demonstrates async/await throughout (SearchTransactionsAsync, ExportToExcelAsync). Event handlers are async void. Progress reporting integrated.                           |
| **VII. Centralized Error Handling with Service_ErrorHandler** | ✅ PASS | quickstart.md shows Service_ErrorHandler.HandleException pattern with retry actions. Common Pitfalls section explicitly warns against MessageBox.Show().                                 |
| **VIII. Documentation and XML Comments**                      | ✅ PASS | data-model.md includes full XML comments for all entities. quickstart.md code examples demonstrate <summary> tags. MCP validation tools include check_xml_docs.                          |
| **IX. Theme System Integration via Core_Themes**              | ✅ PASS | All Forms/UserControls include Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments() in constructors. Spec references `.github/instructions/ui-compliance/theming-compliance.instructions.md` and `Documentation/Theme-System-Reference.md`. Theme tokens via Model_Shared_UserUiColors with SystemColors fallbacks. AutoScaleMode.Dpi set in all UI components. |

**Overall**: ✅ **PASS** - All constitution principles satisfied after Phase 1 design.

**Key Findings**:

-   ✅ data-model.md entities (Model_Transactions_SearchCriteria, Model_Transactions_SearchResult, Model_Transactions_Core_Analytics) align with existing Model_Transactions_Core
-   ✅ contracts/ directory not applicable (WinForms, not REST API)
-   ✅ research.md decisions (Passive ViewModel, ClosedXML export, BaseIntegrationTest pattern) fully compliant with constitution
-   ✅ No new forbidden practices introduced (no ORM, no inline SQL, no MessageBox, no blocking async)

## Project Structure

### Documentation (this feature)

```
specs/005-transaction-viewer-form/
├── plan.md              # This file (/speckit.plan command output) ✅
├── spec.md              # Feature specification ✅
├── research.md          # Phase 0 output (/speckit.plan command) - TO BE GENERATED
├── data-model.md        # Phase 1 output (/speckit.plan command) - TO BE GENERATED
├── quickstart.md        # Phase 1 output (/speckit.plan command) - TO BE GENERATED
├── contracts/           # Phase 1 output (/speckit.plan command) - NOT APPLICABLE (WinForms)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```
Controls/
├── Transactions/                    # NEW: Transaction viewer UserControls
│   ├── TransactionSearchControl.cs        # Search filters UI (300 lines target) [P1] ✅
│   ├── TransactionSearchControl.Designer.cs
│   ├── TransactionGridControl.cs          # DataGridView + pagination (300 lines target) [P1] ✅
│   ├── TransactionGridControl.Designer.cs
│   └── TransactionDetailPanel.cs          # Detail panel (embedded in grid) (200 lines target) [P1] ✅
│       └── TransactionDetailPanel.Designer.cs
│
Forms/
├── Transactions/
│   ├── Transactions.cs                    # REFACTORED: Main form orchestration (500 lines target, down from 2136) [P1] ✅
│   ├── Transactions.Designer.cs
│   └── TransactionLifecycleForm.cs       # NEW: TreeView lifecycle modal dialog (400 lines target) [P1]
│       └── TransactionLifecycleForm.Designer.cs
│       └── TransactionDetailPanel.Designer.cs
│
Models/
├── Model_Transactions_SearchCriteria.cs     # NEW: Search filter encapsulation [P1] ✅
├── Model_Transactions_SearchResult.cs       # NEW: Paginated results wrapper [P1] ✅
├── TransactionLifecycleNode.cs      # NEW: Tree node for lifecycle visualization [P1]
├── Model_Transactions_ViewModel.cs          # NEW: Business logic and state management (400 lines target) [P1] ✅
└── Model_Transactions_Core.cs            # EXISTING: Transaction data model (no changes)

Data/
├── Dao_Transactions.cs              # REFACTORED: Add SearchAsync, GetBatchLifecycleAsync methods [P1]
└── [other DAOs]                     # No changes

Database/
├── UpdatedStoredProcedures/
│   └── ReadyForVerification/
│       ├── inv_transactions_Search.sql         # EXISTING: Main search procedure
│       ├── inv_transactions_SmartSearch.sql    # EXISTING: Dynamic WHERE clause search
│       ├── inv_transactions_GetAnalytics.sql   # EXISTING: Summary statistics [P3]
│       └── inv_transactions_GetBatchLifecycle.sql  # NEW: Batch lifecycle chronological query [P1]
│
Tests/
└── Integration/
    └── Dao_Transactions_Tests.cs    # NEW: Integration tests for DAO methods [P2]
```

**Structure Decision**: Single project WinForms application following existing MTM architecture. New `Controls/Transactions/` directory separates transaction-specific UserControls from shared controls. ViewModel pattern introduced for testability while maintaining WinForms event-driven model. Existing stored procedures reused without modification (except new GetBatchLifecycle for P1 lifecycle viewer). Transaction Lifecycle feature promoted to P1 (previously P3 "Batch History View").

**File Count**: 6 new files (3 UserControls with .Designer.cs, 1 Form with .Designer.cs, 1 ViewModel, 3 Models) + 1 new stored procedure + 1 refactored DAO

## Complexity Tracking

_Fill ONLY if Constitution Check has violations that must be justified_

**No violations** - All constitution principles satisfied. This section intentionally left empty.

---

## Relevant Instruction Files

**Note**: These instruction files provide coding patterns and standards for implementation. Review relevant files when moving from planning to task execution.

### Core Development:

-   `.github/instructions/csharp-dotnet8.instructions.md` - Language features, WinForms patterns, async/await, file organization
-   `.github/instructions/mysql-database.instructions.md` - Stored procedures, connection management, Helper_Database_StoredProcedure usage
-   `.github/instructions/documentation.instructions.md` - XML comments, README structure, code documentation

### Quality & Security:

-   `.github/instructions/testing-standards.instructions.md` - Manual validation approach, success criteria, test scenarios
-   `.github/instructions/integration-testing.instructions.md` - Discovery-first workflow, method signature verification, DAO testing patterns
-   `.github/instructions/security-best-practices.instructions.md` - Input validation, credential management, SQL injection prevention
-   `.github/instructions/performance-optimization.instructions.md` - Async patterns, connection pooling, memory management, caching strategies
-   `.github/instructions/code-review-standards.instructions.md` - Quality gates, review process, common issues

### When to Use:

-   **During task generation** (`/speckit.tasks`): Reference to add instruction file pointers to specific tasks
-   **During implementation** (`/speckit.implement`): Load instruction files to apply correct patterns
-   **During code review**: Validate compliance with documented standards

**Instruction File Reference Format in Tasks**:

```markdown
-   [ ] T100 - Implement DAO method for inventory queries
    -   **Reference**: .github/instructions/mysql-database.instructions.md - Use Helper_Database_StoredProcedure pattern
    -   **Reference**: .github/instructions/csharp-dotnet8.instructions.md - Follow async/await patterns
```

---
