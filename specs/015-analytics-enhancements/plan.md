# Implementation Plan: Analytics & Inventory Management Enhancements

**Branch**: `015-analytics-enhancements` | **Date**: December 4, 2025 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/015-analytics-enhancements/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

This feature implements comprehensive enhancements to analytics and inventory management, including a new "Fair Grading" system for material handlers based on shift-weighted scoring, integration with Infor Visual for user metadata (shifts/names), and significant UX improvements for Inventory Audit, Die Tool Discovery, and PO Details.

## Technical Context

**Language/Version**: C# 12.0 (.NET 8.0)
**Primary Dependencies**: WinForms, MySQL.Data 9.4.0, ClosedXML 0.105.0
**Storage**: MySQL 5.7.24 (Legacy)
**Testing**: xUnit 2.6.2 (Integration Tests)
**Target Platform**: Windows Desktop
**Project Type**: Single Project (WinForms)
**Performance Goals**: <500ms for standard queries, <2s for complex analytics reports
**Constraints**: MySQL 5.7 compatibility (No CTEs, Window Functions, JSON functions), Strict Layered Architecture (Forms -> DAOs -> Stored Procedures)
**Scale/Scope**: ~50 functional requirements, 10 new/modified controls, 1 new database table

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- [x] **I. Centralized Error Handling**: All new error paths will use `Service_ErrorHandler`.
- [x] **II. Structured Logging**: All logging will use `LoggingUtility`.
- [x] **III. Model_Dao_Result Pattern**: All new DAOs will return `Model_Dao_Result<T>`.
- [x] **IV. Async-First Database Operations**: All DB operations will be async.
- [x] **V. WinForms Best Practices**: UI updates will be marshaled; forms/controls will inherit from `ThemedForm`/`ThemedUserControl`.
- [x] **VI. Stored Procedure Parameter Conventions**: New procedures will follow naming conventions.
- [x] **VII. XML Documentation Standards**: All public members will be documented.
- [x] **VIII. Null Safety Requirements**: Nullable reference types enabled and checked.
- [x] **IX. Theme System Integration**: All UI components will use the theme system.
- [x] **X. Resource Disposal**: `IDisposable` resources will be properly managed.
- [x] **XI. Input Validation Service**: Input validation will use `Service_Validation`.

## Project Structure

### Documentation (this feature)

```text
specs/015-analytics-enhancements/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```text
MTM_WIP_Application_WinForms/
├── Controls/
│   ├── Visual/
│   │   ├── Control_MaterialHandlerAnalytics.cs  # Refactor target
│   │   ├── Control_ReceivingAnalytics.cs        # Refactor target
│   │   ├── Control_InventoryAudit.cs            # Refactor target
│   │   └── Control_DieToolDiscovery.cs          # Refactor target
│   ├── MainForm/
│   │   └── Control_AdvancedInventory.cs         # Refactor target
│   └── SettingsForm/                            # Suggestion control refactors
├── Forms/
│   └── Transactions/
│       └── Form_PODetails.cs                    # Complete refactor
├── Data/
│   ├── Dao_VisualAnalytics.cs                   # New DAO for sys_visual & analytics
│   └── Dao_Inventory.cs                         # Updates for new filters
├── Models/
│   └── Analytics/
│       └── Model_Visual_UserShift.cs            # New model
├── Database/
│   └── UpdatedStoredProcedures/                 # New SPs for sys_visual
└── Services/
    └── Analytics/
        └── Service_UserShiftLogic.cs            # New service for shift calculation
```

**Structure Decision**: Standard WinForms layered architecture (Forms/Controls -> Services -> DAOs -> Database).

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| N/A | | |

## Phase 5 – XML Documentation
- Inventory every public surface area touched by this plan.
- Produce or update XML comments (summary/param/returns/exception) that match implementation intent.
- Call out any intentional exclusions and obtain reviewer sign-off before build.
