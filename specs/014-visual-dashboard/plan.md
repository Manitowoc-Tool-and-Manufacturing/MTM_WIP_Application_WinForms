# Implementation Plan: Infor Visual Dashboard

**Branch**: `014-visual-dashboard` | **Date**: 2025-11-26 | **Spec**: [specs/014-visual-dashboard/spec.md](specs/014-visual-dashboard/spec.md)
**Input**: Feature specification from `/specs/014-visual-dashboard/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Implement a read-only dashboard for Infor Visual ERP data. This involves creating a new `InforVisualDashboard` form, a `Service_VisualDatabase` to handle SQL Server connections using `System.Data.SqlClient`, and a set of embedded SQL queries. The UI will be modernized but inherit from `ThemedForm`. A shared `Control_EmptyState` will be refactored from existing code.

## Technical Context

**Language/Version**: C# 12.0 / .NET 8.0
**Primary Dependencies**: `System.Data.SqlClient` (New), `MySql.Data` (Existing), `ClosedXML` (Existing)
**Storage**: Infor Visual SQL Server (Read-Only), MySQL 5.7.24 (App Data)
**Testing**: xUnit 2.6.2
**Target Platform**: Windows Desktop (WinForms)
**Project Type**: Desktop Application
**Performance Goals**: Dashboard load < 3s
**Constraints**: Read-Only access to ERP, No dynamic SQL (embedded resources only), No Stored Procedures in ERP
**Scale/Scope**: New Form, ~7 Data Categories, New Service, New DAO-like structure for SQL Server

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

*   **Centralized Error Handling**: ✅ `Service_ErrorHandler` will be used for all exceptions, including SQL Server connection errors.
*   **Structured Logging**: ✅ `LoggingUtility` will be used.
*   **Model_Dao_Result Pattern**: ✅ `Service_VisualDatabase` will return `Model_Dao_Result<T>`.
*   **Async-First**: ✅ All database operations will be `async`.
*   **Stored Procedures**: ❌ **VIOLATION**. We cannot create stored procedures in the vendor's database.
    *   *Justification*: We must use raw SQL.
    *   *Mitigation*: SQL will be stored in embedded `.sql` files, not inline strings.
*   **WinForms Best Practices**: ✅ `InforVisualDashboard` will inherit `ThemedForm`.
*   **XML Documentation**: ✅ All public members will be documented.
*   **Null Safety**: ✅ Nullable reference types enabled.

## Project Structure

### Documentation (this feature)

```text
specs/014-visual-dashboard/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output (Not applicable for WinForms, used for Interfaces)
└── tasks.md             # Phase 2 output
```

### Source Code (repository root)

```text
MTM_WIP_Application_WinForms/
├── Forms/
│   └── Visual/
│       └── InforVisualDashboard.cs      # New Dashboard Form
├── Services/
│   └── Visual/
│       └── Service_VisualDatabase.cs    # New Service for SQL Server access
├── Controls/
│   └── Shared/
│       └── Control_EmptyState.cs        # Refactored Empty State Control
├── Resources/
│   └── Sql/
│       └── Visual/                      # Embedded SQL files
│           ├── Inventory.sql
│           ├── Receiving.sql
│           └── ...
└── specs/
    └── 014-visual-dashboard/
        └── prompts/                     # AI Prompt Assets
            ├── Inventory.instruction.md
            ├── Inventory.prompt.md
            └── ...
```

**Structure Decision**: Single project structure with new folders for the feature.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| **No Stored Procedures** | Cannot modify vendor (Infor Visual) database schema. | Inline SQL strings rejected (Constitution violation). Embedded resources chosen as middle ground. |
| **System.Data.SqlClient** | Required to connect to SQL Server (Infor Visual). | ODBC rejected (performance/config). Linked Server rejected (security/complexity). |
