# Implementation Plan: Modern JSON-Driven Help System

**Branch**: `016-new-help-system` | **Date**: 2025-12-06 | **Spec**: [specs/016-new-help-system/spec.md](specs/016-new-help-system/spec.md)
**Input**: Feature specification from `/specs/016-new-help-system/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Implement a modern, HTML-based help system for the MTM WIP Application that replaces the current static HTML help files with a dynamic, JSON-driven system modeled after the existing Release Notes viewer. The system will load content from external JSON files, render it using a unified HTML template in a WebView2 control, and provide search, navigation, and deep-linking capabilities.

## Technical Context

**Language/Version**: C# 12.0 / .NET 8.0
**Primary Dependencies**: Microsoft.Web.WebView2, System.Text.Json
**Storage**: JSON files (read-only content)
**Testing**: xUnit 2.6.2 (Integration tests for loader/parser)
**Target Platform**: Windows Forms (Desktop)
**Project Type**: Single project (WinForms)
**Performance Goals**: Index load < 1s, Search < 200ms
**Constraints**: Must run on existing hardware, no database dependency for help content
**Scale/Scope**: ~13 initial help categories, extensible via JSON

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **I. Centralized Error Handling**: ✅ Plan uses `Service_ErrorHandler` for JSON parsing and file loading errors.
- **II. Structured Logging**: ✅ Plan uses `LoggingUtility` for logging missing files or parsing failures.
- **III. Model_Dao_Result Pattern**: N/A (No database access required for help content).
- **IV. Async-First Database Operations**: N/A (File I/O will be async).
- **V. WinForms Best Practices**: ✅ Plan uses `WebView2` (modern control) and proper disposal patterns.
- **VI. Stored Procedure Parameter Conventions**: N/A.
- **VII. XML Documentation Standards**: ✅ All new services and models will have XML documentation.
- **VIII. Null Safety Requirements**: ✅ Nullable reference types enabled in project.
- **IX. Theme System Integration**: ✅ Help viewer form inherits `ThemedForm`.
- **X. Resource Disposal**: ✅ `WebView2` and streams will be properly disposed.
- **XI. Input Validation Service**: N/A (Read-only content, search input is local/safe).

## Project Structure

### Documentation (this feature)

```text
specs/016-new-help-system/
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
├── Documentation/
│   └── Help/
│       ├── JSON/               # New JSON content files
│       └── Templates/          # HTML templates
├── Forms/
│   └── Help/
│       ├── HelpViewerForm.cs
│       ├── HelpViewerForm.Designer.cs
│       └── HelpViewerForm.resx
├── Models/
│   └── Help/
│       ├── Model_HelpCategory.cs
│       ├── Model_HelpTopic.cs
│       └── Model_HelpSearchResult.cs
└── Services/
    └── Help/
        ├── Service_HelpSystem.cs
        ├── Service_HelpContentLoader.cs
        └── Service_HelpTemplateEngine.cs
```

**Structure Decision**: Standard WinForms layered architecture (Forms, Models, Services) with dedicated folders for the Help feature to maintain separation of concerns.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| None | | |

## Phase 5 – XML Documentation
- Inventory every public surface area touched by this plan.
- Produce or update XML comments (summary/param/returns/exception) that match implementation intent.
- Call out any intentional exclusions and obtain reviewer sign-off before build.
