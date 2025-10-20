# 002-003-001: Developer Tools Suite Integration

**Parent Feature**: [002-003-database-layer-complete](../)  
**Status**: Planning Complete - Ready for Implementation  
**Created**: 2025-10-18

---

## Overview

This sub-feature delivers a comprehensive suite of developer tools integrated into the MTM WIP Application Settings form, enabling efficient database layer maintenance, debugging, and code generation during the Phase 2.5 stored procedure standardization initiative.

## Quick Links

- **[Specification](./spec.md)** - User stories, acceptance criteria, and success metrics
- **[Implementation Plan](./plan.md)** - Architecture, dependencies, and integration strategy
- **[Task Breakdown](./tasks.md)** - 70 detailed tasks organized by user story
- **[Quickstart Guide](./quickstart.md)** - Usage workflows and examples
- **[Data Model](./data-model.md)** - Database schema and C# models
- **[API Contracts](./contracts/)** - UserControl APIs and stored procedure signatures

## User Stories (5 Total)

| ID | Story | Priority | Status | Tasks |
|----|-------|----------|--------|-------|
| US1 | Debug Dashboard | P1 | ⬜ Not Started | T016-T022 (7 tasks) |
| US2 | Parameter Prefix Maintenance | P1 | ⬜ Not Started | T023-T035 (13 tasks) |
| US3 | Schema Inspector | P2 | ⬜ Not Started | T036-T043 (8 tasks) |
| US4 | Procedure Call Hierarchy | P2 | ⬜ Not Started | T044-T052 (9 tasks) |
| US5 | Code Generator | P3 | ⬜ Not Started | T053-T060 (8 tasks) |

**Total**: 70 tasks across 9 phases (~52 hours estimated)

## MVP Scope (Critical Path)

To unblock parent feature T113c/T113d, implement **minimum viable product**:

1. **Phase 1**: Setup & Infrastructure (T001-T005)
2. **Phase 2**: Foundational Prerequisites (T006-T015) - **BLOCKING**
3. **Phase 3**: User Story 1 - Debug Dashboard (T016-T022)
4. **Phase 4**: User Story 2 - Parameter Prefix Maintenance (T023-T035)

**MVP Deliverable**: 25 tasks, ~22 hours, enables Phase 2.5 Part C refactoring work to proceed.

## Integration Points with Parent Feature

### Blocks Parent Tasks

| Parent Task | Sub-Feature Phase | Description |
|-------------|-------------------|-------------|
| T113c | Phase 2 (T006-T015) | Developer role, parameter prefix override table, CRUD stored procedures |
| T113d | Phase 4 (T023-T035) | Parameter Prefix Maintenance UI in Settings → Developer |

### Extends Parent Capabilities

- **Debug Dashboard** (US1) - Validates stored procedure execution during refactoring
- **Schema Inspector** (US3) - Reduces context switching for schema queries
- **Call Hierarchy** (US4) - Visualizes procedure relationships using T100-T102 artifacts
- **Code Generator** (US5) - Accelerates Phase 3-5 DAO refactoring with boilerplate generation

## Architecture Overview

### Settings Form Integration

```
SettingsForm (TreeView)
├── User Management
├── Application Settings
├── Theme
├── Database
│   └── Schema Inspector (US3 - also accessible here)
├── About
└── Developer (IsDeveloper role gate)
    ├── Debug Dashboard (US1)
    ├── Parameter Prefix Maintenance (US2)
    ├── Schema Inspector (US3)
    ├── Procedure Call Hierarchy (US4)
    └── Code Generator (US5)
```

### Database Infrastructure (Foundational - Phase 2)

**New Table**: `sys_parameter_prefix_overrides`
- Stores per-procedure, per-parameter prefix overrides
- Audit trail (CreatedBy/Date, ModifiedBy/Date, IsActive)
- UNIQUE constraint on (ProcedureName, ParameterName)

**New Stored Procedures** (5 total):
1. `sys_parameter_prefix_overrides_Get_All` - Startup cache loading
2. `sys_parameter_prefix_overrides_Get_ById` - Edit operation
3. `sys_parameter_prefix_overrides_Add` - Create override
4. `sys_parameter_prefix_overrides_Update_ById` - Modify override
5. `sys_parameter_prefix_overrides_Delete_ById` - Soft delete

**New Model**: `Model_ParameterPrefixOverride` (POCO)

**New DAO**: `Dao_ParameterPrefixOverrides` (5 async methods)

**Helper Extension**: `Helper_Database_StoredProcedure.LoadParameterPrefixOverridesAsync()` - Startup cache

### UserControls (5 total)

All inherit from `UserControl` with `ReloadAsync()` and optional `ClearAsync()` methods:

1. **Control_Developer_DebugDashboard** - Service_DebugTracer integration, real-time trace capture
2. **Control_Developer_ParameterPrefixMaintenance** - DataGridView + CRUD dialogs
3. **Control_Developer_SchemaInspector** - TreeView + DataGridView schema browser
4. **Control_Developer_ProcedureCallHierarchy** - JSON-based visualization
5. **Control_Developer_CodeGenerator** - Template-based code generation

## Development Timeline (2 Developers)

### Week 1: Foundation
- Days 1-3: Phase 1 (Setup) + Phase 2 (Foundational) - **Sequential**
- Phase 2 MUST complete before ANY user story begins

### Week 2-3: Parallel User Story Development
- Developer A: US1 (Debug Dashboard) + US3 (Schema Inspector) + US5 (Code Generator)
- Developer B: US2 (Parameter Prefix) + US4 (Call Hierarchy) + Control_Database integration

### Week 4: Integration & Polish
- Days 1-2: Phase 8 (Control_Database integration)
- Days 3-4: Phase 9 (Polish, documentation, comprehensive testing)

**Total Duration**: 4 weeks (2 developers) or 8 weeks (1 developer)

## Success Criteria

Feature is **COMPLETE** when:

- [X] All 5 user stories pass integration tests (T022, T035, T043, T052, T060)
- [X] Developer role gates all tools correctly (IsDeveloper users only)
- [X] Parameter prefix override system operational (cache loads at startup, applied during SP execution)
- [X] All tools accessible through Settings → Developer
- [X] Schema Inspector also accessible through Settings → Database
- [X] Documentation complete (quickstart.md, user guide, help system, AGENTS.md)
- [X] No compilation warnings or errors
- [X] Performance targets met (<2 second load time per tool)
- [X] Parent spec T113c and T113d marked complete

## Risk Mitigation

| Risk | Mitigation Strategy |
|------|---------------------|
| Phase 2 foundational delay blocks all user stories | Prioritize Phase 2 completion; allocate best developer |
| Parameter prefix cache loading fails at startup | Implement retry dialog per parent spec requirement (T123) |
| Non-existent procedures in override table | Warning dialog but allow save (Production hotfix scenario) |
| Duplicate override attempts | UNIQUE constraint + user-friendly error handling |
| Schema Inspector performance with large database | Lazy loading, TreeView node expansion triggers, caching |
| Code Generator produces incorrect code | Template testing, user validation, "generated code" comments |

## Dependencies

### Parent Feature Dependencies
- Phase 2.5 Part A complete (T100-T106) - Provides call-hierarchy artifacts for US4
- Helper_Database_StoredProcedure refactored - Extension point for override cache
- Service_DebugTracer operational - Integration point for US1

### External Dependencies
- MySQL 5.7.24+ with INFORMATION_SCHEMA access
- Model_AppVariables.CurrentUser.IsDeveloper flag implementation
- Settings form TreeView node navigation pattern
- Core_Themes DPI scaling and layout adjustment utilities

### Optional Dependencies
- ClosedXML library (for US4 Excel export feature)
- Syntax highlighting library (for US5 Code Generator, if desired)

## Related Documentation

### Parent Feature
- [Parent Spec](../spec.md)
- [Parent Plan](../plan.md)
- [Parent Tasks](../tasks.md)

### Project Context
- [AGENTS.md](../../../AGENTS.md) - Agent development guidelines
- [.github/instructions/](../../../.github/instructions/) - Coding standards
- [Documentation/](../../../Documentation/) - User guides and help system

### Sub-Feature Artifacts
- [Clarification Q&A](./clarification-questions.md) - Stakeholder decisions
- [Research Notes](./research.md) - Technical decisions and alternatives
- [Checklists](./checklists/) - Quality gates and validation checklists

---

**Next Steps**: Review spec.md for detailed requirements, then begin Phase 1 (T001-T005) after parent Phase 2.5 Part B completion.

