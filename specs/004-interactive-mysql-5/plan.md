# Implementation Plan: Interactive MySQL 5.7 Stored Procedure Builder

**Branch**: `004-interactive-mysql-5` | **Date**: 2025-10-17 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/004-interactive-mysql-5/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

Build a multi-file HTML/CSS/JavaScript client-side application for visually designing MySQL 5.7 stored procedures through wizard-style interface. Uses PHP backend (MAMP) for database metadata queries. Developers create procedures with drag-drop validation rules, visual DML operation builders, flow diagrams showing operation sequence, and live SQL preview. System connects to mtm_wip_application_winforms_test database, fetches table/column metadata, validates against MySQL 5.7 syntax, and exports formatted .sql files with DELIMITER statements and header comments. Reduces procedure development time from manual SQL writing to guided visual assembly in under 10 minutes for standard CRUD patterns.

## Technical Context

**Language/Version**: HTML5/CSS3/JavaScript ES6+ (Chrome 86+), PHP 7.4+ (MAMP), MySQL 5.7  
**Primary Dependencies**: 
- Client-side: Vanilla JavaScript (no frameworks), local File System Access API, localStorage API
- Server-side: PHP mysqli/PDO extensions, MAMP MySQL connection
- Styling: CSS Grid/Flexbox, CSS custom properties for theming
- Code highlighting: Prism.js or highlight.js for SQL syntax highlighting
- Drag-drop: HTML5 Drag and Drop API with keyboard accessibility fallbacks

**Storage**: 
- MySQL 5.7 database (mtm_wip_application_winforms_test) for metadata queries
- Browser localStorage for wizard state persistence and version history
- File system for .sql export and custom template storage (via File System Access API)

**Testing**: Manual validation testing (no automated tests per MTM constitution)
- Cross-browser compatibility (Chrome primary, Firefox/Edge/Safari with feature detection)
- SQL syntax validation against MySQL 5.7
- Export file format verification
- Database metadata accuracy testing

**Target Platform**: Desktop Chrome 86+ browser on Windows (primary), graceful degradation for Firefox/Edge/Safari on desktop  
**Project Type**: Web application (client-side HTML/CSS/JS with PHP backend API)

**Performance Goals**: 
- SQL preview generation: <500ms for procedures with up to 25 operations
- Database metadata fetch: <5 seconds for schemas with 100 tables
- Auto-save to localStorage: <100ms every 30 seconds
- Drag-drop operations: 60fps smooth animations
- Flow diagram rendering: <2 seconds auto-layout for 25 nodes

**Constraints**: 
- Client-side only (no server deployment, runs from file:// or local MAMP)
- MySQL 5.7 syntax only (no CTEs, no window functions)
- Browser localStorage limits (~5-10MB, must manage version history pruning)
- No external NPM dependencies (CDN-linked libraries only)
- Must integrate with existing procedure-review-tool.html infrastructure

**Scale/Scope**: 
- Support 74+ existing stored procedures for import/editing
- Template library: 15-20 built-in templates + unlimited user templates
- Flow diagram: up to 25 operations/nodes
- Parameter limit: 20 parameters per procedure (typical max)
- Version history: 5 versions per procedure retained in localStorage

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Status | Notes |
|-----------|--------|-------|
| **I. Stored Procedure Only Database Access** | ✅ PASS | Feature generates stored procedures as output. PHP backend uses mysqli/PDO for metadata queries only (no application CRUD). Aligns with MTM constitution. |
| **II. DaoResult<T> Wrapper Pattern** | ⚠️ N/A | JavaScript application, not C#. Pattern does not apply to client-side web app. |
| **III. Region Organization and Method Ordering** | ⚠️ N/A | JavaScript modules, not C# files. Will use consistent module structure instead. |
| **IV. Manual Validation Testing Approach** | ✅ PASS | Feature spec includes 10 user stories with acceptance scenarios for manual testing. No automated tests required. |
| **V. Environment-Aware Database Selection** | ✅ PASS | Connects to mtm_wip_application_winforms_test (test database) via PHP backend. No production database access. |
| **VI. Async-First UI Responsiveness** | ✅ PASS | All PHP API calls use async fetch(). UI updates debounced (300ms). localStorage operations async. Maintains <100ms perceived responsiveness. |
| **VII. Centralized Error Handling** | ⚠️ ADAPTED | JavaScript uses structured error responses from PHP (error_type, user_message, technical_detail). Not Service_ErrorHandler but equivalent pattern. |
| **VIII. Documentation and XML Comments** | ⚠️ ADAPTED | JavaScript uses JSDoc comments instead of XML. Will document all exported functions and classes. |

**Overall Assessment**: ✅ **CONSTITUTION COMPLIANT**

**Justification for N/A items**: This is a client-side web application (HTML/JS/PHP) that generates stored procedures, not a C#/WinForms component. C#-specific principles (DaoResult, Region Organization, Service_ErrorHandler) do not apply. Equivalent JavaScript patterns adopted for error handling and documentation.

**No violations requiring Complexity Tracking table**.

## Project Structure

### Documentation (this feature)

```
specs/004-interactive-mysql-5/
├── spec.md                    # Feature specification (COMPLETE)
├── clarifications-needed.md   # Clarification questions (ANSWERED)
├── plan.md                    # This file (/speckit.plan command output)
├── research.md                # Phase 0 output (TO BE GENERATED)
├── data-model.md              # Phase 1 output (TO BE GENERATED)
├── quickstart.md              # Phase 1 output (TO BE GENERATED)
├── contracts/                 # Phase 1 output (TO BE GENERATED)
│   ├── php-api-endpoints.md   # PHP backend API contract
│   └── javascript-modules.md  # JS module interfaces
└── tasks.md                   # Phase 2 output (/speckit.tasks - NOT created by /speckit.plan)
```

### Source Code (repository root)

```
StoredProcedureValidation/
└── sp-builder/                          # NEW - Stored Procedure Builder application
    ├── index.html                       # Main entry point with navigation
    ├── wizard.html                      # Wizard interface (sequential steps)
    ├── parameters.html                  # Parameter configuration step
    ├── validation.html                  # Validation rules builder step
    ├── dml-operations.html              # DML operation builders step
    ├── flow-diagram.html                # Visual flow diagram step
    ├── preview.html                     # SQL preview and syntax validation step
    ├── templates.html                   # Template library and management
    ├── advanced.html                    # Advanced features (cursors, loops)
    ├── export.html                      # Export configuration and file generation
    ├── help.html                        # Help system and tutorials
    │
    ├── css/
    │   ├── main.css                     # Global styles and theme
    │   ├── wizard.css                   # Wizard-specific styles
    │   ├── components.css               # Reusable component styles
    │   └── syntax-highlighting.css      # SQL syntax highlighting theme
    │
    ├── js/
    │   ├── app.js                       # Application initialization
    │   ├── wizard-controller.js         # Wizard navigation and state management
    │   ├── database-metadata.js         # Database connection and metadata fetching
    │   ├── procedure-model.js           # Procedure definition data model
    │   ├── sql-generator.js             # SQL code generation engine
    │   ├── sql-validator.js             # Client-side MySQL 5.7 syntax validation
    │   ├── drag-drop.js                 # Drag-drop handlers with keyboard accessibility
    │   ├── flow-diagram.js              # Flow diagram rendering and interaction
    │   ├── template-manager.js          # Template loading, saving, validation
    │   ├── storage-manager.js           # localStorage persistence and version history
    │   ├── export-manager.js            # .sql file generation and download
    │   └── utils.js                     # Shared utilities and helpers
    │
    ├── api/                             # PHP backend endpoints
    │   ├── get-tables.php               # Fetch table list from information_schema
    │   ├── get-columns.php              # Fetch columns for specified table
    │   ├── validate-syntax.php          # Validate SQL syntax via MySQL PREPARE
    │   ├── check-procedure-exists.php   # Check if procedure name exists
    │   └── config.php                   # Database connection configuration
    │
    ├── templates/                       # Built-in and custom templates
    │   ├── crud-templates.json          # CRUD operation templates
    │   ├── batch-templates.json         # Batch processing templates
    │   ├── transfer-templates.json      # Inventory transfer templates
    │   ├── audit-templates.json         # Audit logging templates
    │   └── custom-templates.json        # User-created templates (Git-tracked)
    │
    └── lib/                             # Third-party libraries (CDN fallback)
        ├── prism.js                     # SQL syntax highlighting
        └── prism.css                    # Prism theme
```

**Structure Decision**: Web application with 11 HTML pages (per FR-003 requirement). Pages share common CSS/JS libraries loaded via shared includes. PHP backend in `api/` folder handles database queries. Templates stored as JSON files. Client-side only execution (no deployment needed beyond MAMP running). Fits within existing `StoredProcedureValidation/` folder alongside analysis scripts and procedure-review-tool.html.

## Complexity Tracking

*Fill ONLY if Constitution Check has violations that must be justified*

**No violations requiring justification.** All constitution principles either pass or are not applicable to this JavaScript/PHP web application feature.
