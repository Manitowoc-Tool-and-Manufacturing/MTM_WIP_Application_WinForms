# Implementation Plan: Print and Export System Refactor

**Branch**: `006-print-and-export` | **Date**: 2025-11-08 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/006-print-and-export/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Complete refactor of print and export functionality to resolve fundamental architectural issues with page range handling. The current system uses row-based estimation (31 rows/page) which doesn't match actual print pagination. The new architecture passes ALL data to the Windows print system with proper PrintRange/FromPage/ToPage settings, allowing the print engine to handle exact page boundaries. Implements 5-phase approach: systematic removal of old code, core infrastructure rebuild, Compact Sidebar print dialog UI, unified PDF/Excel export with exact page ranges, and integration with existing UI entry points.

## Technical Context

**Language/Version**: C# 12 / .NET 8.0 Windows Forms  
**Primary Dependencies**: MySql.Data 9.4.0, System.Text.Json, Microsoft.Web.WebView2, ClosedXML (Excel export), System.Drawing.Printing  
**Storage**: MySQL 5.7+ (stored procedures only for settings persistence), Local JSON files in AppData for per-grid preferences  
**Testing**: Manual validation approach (no automated UI tests), Integration tests for Helper/Core classes  
**Target Platform**: Windows 10+ (x64), DPI scaling support 100%-200%  
**Project Type**: Single Windows Forms desktop application (MTM WIP manufacturing inventory system)  
**Performance Goals**: Preview generation < 2 seconds for 100-row datasets, Cancel response < 500ms, UI interactions sub-100ms  
**Constraints**: Must integrate with existing MTM theme system, No NuGet packaging (internal app only), Page count must be 100% accurate (no row-based estimation)  
**Scale/Scope**: 30 functional requirements across 5 phases, 4 prioritized user stories, 74+ existing stored procedures in ecosystem

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

### Principle I: Stored Procedure Only Database Access
**Status**: ✅ COMPLIANT  
**Analysis**: Feature uses stored procedures only for settings persistence (printer selection, column preferences). No inline SQL required. If database persistence used, will follow `Helper_Database_StoredProcedure` pattern.

### Principle II: DaoResult<T> Wrapper Pattern
**Status**: ✅ COMPLIANT  
**Analysis**: New DAO methods for settings persistence will return `DaoResult` or `DaoResult<T>`. All database operations wrapped in structured responses. File-based persistence (JSON in AppData) uses try-catch with structured result objects.

### Principle III: Region Organization and Method Ordering
**Status**: ✅ COMPLIANT  
**Analysis**: All new C# files (Core_TablePrinter, Helper_PrintManager, Helper_ExportManager, PrintForm, Model_PrintJob, Model_PrintSettings) will follow standard region organization: Fields, Properties, Progress Control Methods, Constructors, specific functionality regions, Helpers, Cleanup.

### Principle IV: Manual Validation Testing Approach
**Status**: ✅ COMPLIANT  
**Analysis**: Specification defines comprehensive manual validation approach with 4 user stories, edge cases, and acceptance testing checklist. Success criteria are measurable and testable. Integration tests planned for core helper classes but not blocking Phase 1-5 implementation.

### Principle V: Environment-Aware Database Selection
**Status**: ✅ COMPLIANT  
**Analysis**: Feature uses existing Helper_Database_Variables for connection strings. No new database access patterns introduced. Environment awareness maintained through existing infrastructure.

### Principle VI: Async-First UI Responsiveness
**Status**: ✅ COMPLIANT  
**Analysis**: Preview generation runs async with CancellationToken support (FR-019). Progress dialog prevents UI freezes. Export operations async with file I/O. No .Result or .Wait() blocking calls. Marshaling handled by PrintPreviewControl.

### Principle VII: Centralized Error Handling with Service_ErrorHandler
**Status**: ✅ COMPLIANT  
**Analysis**: All error presentation will use Service_ErrorHandler (MTM app only, not NuGet). No MessageBox.Show() calls. FR-003 explicitly mentions using Service_ErrorHandler for MTM integration.

### Principle VIII: Documentation and XML Comments
**Status**: ✅ COMPLIANT  
**Analysis**: All public APIs will have XML documentation. Core_TablePrinter, Helper_PrintManager, Helper_ExportManager, Model_PrintJob, Model_PrintSettings classes require comprehensive XML docs per FR requirements.

### Principle IX: Theme System Integration via Core_Themes
**Status**: ✅ COMPLIANT  
**Analysis**: 
- **FR-020**: Print dialog MUST integrate with MTM theme system (Model_UserUiColors)
- **FR-021**: Print dialog MUST apply Core_Themes.ApplyDpiScaling() and ApplyRuntimeLayoutAdjustments()
- **Mockup 3 Compact Sidebar**: UI follows WinForms architecture standards with proper control naming, AutoSize cascade, and DPI awareness
- **Theme Reference**: Specification explicitly mentions `Documentation/Theme-System-Reference.md`
- Constructor pattern will follow: InitializeComponent() → ApplyDpiScaling() → ApplyRuntimeLayoutAdjustments() → custom init

**Additional Constraints**:
- ✅ Technology Stack: .NET 8.0 Windows Forms, MySQL 5.7+, stored procedures only
- ✅ Security: File path validation, no hardcoded credentials, input validation on page ranges
- ✅ Performance: Sub-2 second preview for 100 rows, sub-500ms cancel response, connection pooling maintained
- ✅ WinForms UI Architecture: Compact Sidebar follows naming conventions, AutoSize patterns, leaf control constraints
- ✅ Documentation Standards: Text descriptions only (no screenshots), instruction file references included in spec

**Complexity Tracking**: No constitution violations requiring justification

---

## Post-Design Constitution Re-Check

*Re-evaluated after Phase 1 design completion*

All principles remain ✅ COMPLIANT after generating research.md, data-model.md, contracts/, and quickstart.md.

**Design Artifacts Validation**:
- ✅ **data-model.md**: PrintJob and PrintSettings entities follow DaoResult pattern, validation rules defined
- ✅ **contracts/PrintJob.json**: JSON schema for print job configuration with proper constraints
- ✅ **contracts/PrintSettings.json**: JSON schema for user preferences with validation
- ✅ **research.md**: All 7 research questions resolved (Windows print system, Excel export, zoom, cancellation, storage, DataTable conversion, theme integration)
- ✅ **quickstart.md**: Developer implementation guide with region organization, error handling patterns, testing workflow

**Architecture Decisions Confirmed**:
- ✅ No database schema changes (settings in local JSON files)
- ✅ Theme integration fully specified (ApplyDpiScaling, ApplyRuntimeLayoutAdjustments, color tokens)
- ✅ Page boundaries calculated during rendering (no row-based estimation)
- ✅ Export uses exact page boundaries (no approximation)
- ✅ Async-first with CancellationToken support throughout

**Ready for Task Generation**: All unknowns resolved, architecture validated, constitution compliance confirmed.

---

## Project Structure

### Documentation (this feature)

```
specs/006-print-and-export/
├── spec.md              # Feature specification (completed)
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
│   └── PrintJob.json    # Print job configuration contract
├── checklists/          # Quality validation checklists
│   └── requirements.md  # Spec quality checklist (completed)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```
MTM_WIP_Application_WinForms/
├── Core/
│   └── Core_TablePrinter.cs          # NEW: DataTable → PrintDocument renderer with exact pagination
├── Helpers/
│   ├── Helper_PrintManager.cs        # NEW: Print orchestration and preview generation
│   └── Helper_ExportManager.cs       # NEW: PDF/Excel export with exact page ranges
├── Models/
│   ├── Model_PrintJob.cs             # NEW: Print/export configuration object
│   └── Model_PrintSettings.cs        # NEW: Per-grid persistence (printer, columns)
├── Forms/
│   └── Shared/
│       ├── PrintForm.cs              # NEW: Compact Sidebar print dialog
│       └── PrintForm.Designer.cs     # NEW: WinForms designer file
├── Controls/
│   └── MainForm/
│       └── Control_RemoveTab.cs      # MODIFIED: Reconnect print button (Phase 5)
├── Documentation/
│   └── Theme-System-Reference.md     # REFERENCE: Theme integration guidance
├── .github/
│   └── instructions/
│       ├── ui-compliance/
│       │   └── theming-compliance.instructions.md  # REFERENCE: Theme patterns
│       ├── winforms-responsive-layout.instructions.md  # REFERENCE: Layout architecture
│       └── ui-scaling-consistency.instructions.md      # REFERENCE: DPI scaling
└── specs/
    └── 006-print-and-export/         # This feature directory
```

**Structure Decision**: Single WinForms desktop application. New print system components added to existing Core/, Helpers/, Models/, Forms/Shared/ folders following established patterns. Old print files deleted in Phase 1. No new top-level directories required. Per-grid settings stored as JSON in user AppData (e.g., `%APPDATA%\MTM\PrintSettings\{GridName}.json`) to avoid database schema changes.

---

## Relevant Instruction Files

**Note**: These instruction files provide coding patterns and standards for implementation. Review relevant files when moving from planning to task execution.

### Core Development:
- `.github/instructions/csharp-dotnet8.instructions.md` - Language features, WinForms patterns, async/await, file organization
- `.github/instructions/mysql-database.instructions.md` - Stored procedures, connection management, Helper_Database_StoredProcedure usage
- `.github/instructions/documentation.instructions.md` - XML comments, README structure, code documentation

### Quality & Security:
- `.github/instructions/testing-standards.instructions.md` - Manual validation approach, success criteria, test scenarios
- `.github/instructions/integration-testing.instructions.md` - Discovery-first workflow, method signature verification, DAO testing patterns
- `.github/instructions/security-best-practices.instructions.md` - Input validation, credential management, SQL injection prevention
- `.github/instructions/performance-optimization.instructions.md` - Async patterns, connection pooling, memory management, caching strategies
- `.github/instructions/code-review-standards.instructions.md` - Quality gates, review process, common issues

### When to Use:
- **During task generation** (`/speckit.tasks`): Reference to add instruction file pointers to specific tasks
- **During implementation** (`/speckit.implement`): Load instruction files to apply correct patterns
- **During code review**: Validate compliance with documented standards

**Instruction File Reference Format in Tasks**:
```markdown
- [ ] T100 - Implement DAO method for inventory queries
  - **Reference**: .github/instructions/mysql-database.instructions.md - Use Helper_Database_StoredProcedure pattern
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md - Follow async/await patterns
```

---
