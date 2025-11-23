# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]
**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Refactor `Control_Shortcuts` to replace the legacy DataGridView with a modern, card-based UI using `Control_SettingsCollapsibleCard`. Ensure all shortcuts are managed via `Service_Shortcut`, enforce QuickButton exclusivity (Alt+0-9), and align default shortcuts with the codebase.

## Technical Context

**Language/Version**: C# 12.0, .NET 8.0
**Primary Dependencies**: WinForms, `Service_Shortcut`, `Control_SettingsCollapsibleCard` (new), `Helper_Database_StoredProcedure`
**Storage**: MySQL 5.7.24 (via DAOs), `default_shortcuts.json` (Resources)
**Testing**: xUnit 2.6.2 (Integration), Manual UI Testing
**Target Platform**: Windows Desktop (WinForms)
**Project Type**: Desktop Application
**Performance Goals**: Immediate UI updates, smooth animations
**Constraints**: No direct DB access in forms, strict `Model_Dao_Result` usage, centralized error handling
**Scale/Scope**: Refactor 1 control, update ~10 files, add ~10 default shortcuts

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- [x] **I. Centralized Error Handling**: Will use `Service_ErrorHandler` for all UI and data errors.
- [x] **II. Structured Logging**: Will use `LoggingUtility` for error logging.
- [x] **III. Model_Dao_Result Pattern**: `Service_Shortcut` uses DAOs that return `Model_Dao_Result`.
- [x] **IV. Async-First**: All DB operations in `Service_Shortcut` are async.
- [x] **V. WinForms Best Practices**: New control will inherit `ThemedUserControl`.
- [x] **IX. Theme System Integration**: Will use theme tokens/base classes.
- [x] **X. Resource Disposal**: Will implement `Dispose` pattern in new controls.
- [x] **XI. Input Validation**: Will validate shortcut keys against duplicates and reserved keys.

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)
<!--
  ACTION REQUIRED: Replace the placeholder tree below with the concrete layout
  for this feature. Delete unused options and expand the chosen structure with
  real paths (e.g., apps/admin, packages/something). The delivered plan must
  not include Option labels.
-->

```text
# [REMOVE IF UNUSED] Option 1: Single project (DEFAULT)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
