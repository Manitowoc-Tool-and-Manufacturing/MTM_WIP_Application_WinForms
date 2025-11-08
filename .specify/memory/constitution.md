<!--
Sync Impact Report:
Version Change: 1.1.0 → 1.2.0
Rationale: Added WinForms UI Architecture Standards and Documentation/Instruction File Standards sections
Modified Principles: None
Added Sections: 
  - WinForms UI Architecture Standards (under Additional Constraints)
  - Documentation and Instruction File Standards (under Additional Constraints)
Removed Sections: None
Templates Requiring Updates:
  ✅ constitution.md - Comprehensive UI architecture and documentation standards added
  ✅ WinForms-UI-Compliance-Checklist.md - Tracks compliance with architecture standards
  ✅ UI-Architecture-Analysis.md - Documents discovered patterns and best practices
  ✅ UI-Compliance-Clarification-Questions.md - Clarifies application of standards
  ⚠ .github/instructions/ files - Need updates (csharp-dotnet8, winforms-responsive-layout, ui-scaling-consistency)
  ⚠ .github/instructions/ui-compliance/ - New directory with theming-compliance.instructions.md required
  ⚠ .github/prompts/ - New refactor-theme-compliance.prompt.md required
  ⚠ specs/*/spec.md - Must reference Documentation/Theme-System-Reference.md
  ⚠ specs/*/tasks.md - Must include theme validation checkpoints
Follow-up TODOs: 
  - Create .github/instructions/ui-compliance/theming-compliance.instructions.md with YAML front matter
  - Create .github/prompts/refactor-theme-compliance.prompt.md
  - Update existing instruction files with theme cross-references
  - Add theme testing scenarios to all feature specifications
-->

Previous Sync Impact Reports:
[v1.1.0] Version Change: 1.0.0 → 1.1.0
Rationale: Added Principle IX (Theme System Integration) and WinForms UI Architecture standards
Added Sections: Principle IX: Theme System Integration via Core_Themes

[v1.0.0] Version Change: Initial (template) → 1.0.0
Rationale: First ratified constitution for MTM_WIP_Application_WinForms project
-->

Previous Sync Impact Report (1.0.0):
Version Change: Initial (template) → 1.0.0
Rationale: First ratified constitution for MTM_WIP_Application_WinForms project
Modified Principles: All principles established from templates and project documentation
Added Sections: Complete constitution based on README.md, instruction files, and refactoring workflow
Removed Sections: None (template placeholders replaced)
Templates Requiring Updates:
  ✅ plan-template.md - Constitution Check section aligns with all principles
  ✅ spec-template.md - Requirements alignment verified
  ✅ tasks-template.md - Task categorization reflects testing, documentation, quality principles
  ⚠ Command files - May need updates to reference specific constitution principles
Follow-up TODOs: None - all placeholders resolved
-->

# MTM WinForms Manufacturing Application Constitution

## Core Principles

### I. Stored Procedure Only Database Access (NON-NEGOTIABLE)

All database operations MUST use stored procedures exclusively. No inline SQL is permitted in application code.

**Requirements**:

-   Every DAO method MUST call stored procedures via `Helper_Database_StoredProcedure`
-   All stored procedures MUST include `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)` parameters
-   C# parameters MUST NOT include `p_` prefix (helper adds automatically)
-   Parameter names MUST use PascalCase matching C# model properties
-   MySQL 5.7 compatibility MUST be maintained (no CTEs, no window functions)

**Rationale**: Stored procedures provide centralized business logic, performance optimization through query plan caching, and defense against SQL injection. The uniform OUT parameter pattern enables consistent error handling across 74+ stored procedures.

**Enforcement**: Code review MUST reject any direct `MySqlConnection`, `MySqlCommand`, or string concatenation of SQL. Static analysis tools SHOULD flag violations.

### II. Model_Dao_Result<T> Wrapper Pattern (MANDATORY)

All data access methods MUST return structured `Model_Dao_Result` or `Model_Dao_Result<T>` responses, never throwing exceptions for expected failures.

**Requirements**:

-   Success case: `Model_Dao_Result<T>.Success(data, message)`
-   Failure case: `Model_Dao_Result<T>.Failure(message, exception)`
-   Properties: `IsSuccess`, `Data`, `Message`, `Exception`
-   Database errors MUST be caught and wrapped in Model_Dao_Result
-   Callers MUST check `IsSuccess` before accessing `Data`

**Rationale**: Eliminates exception-driven control flow for expected failures (record not found, validation errors). Provides consistent API contract across all DAOs, enabling predictable error handling in UI layer.

**Enforcement**: All methods in `Data/` folder MUST return Model_Dao_Result variants. Code review MUST verify exception wrapping and null-safety.

### III. Region Organization and Method Ordering (MANDATORY)

All C# files MUST follow standardized region organization with explicit method ordering within each region.

**Standard Region Order**:

1. `#region Fields` - Private fields, static instances, progress helpers
2. `#region Properties` - Public properties, getters/setters
3. `#region Progress Control Methods` - SetProgressControls and progress-related methods
4. `#region Constructors` - Constructor and initialization (with optional `#region Initialization` sub-region)
5. `#region [Specific Functionality]` - Business logic regions (e.g., "Database Operations", "UI Events")
6. `#region Key Processing` - ProcessCmdKey and keyboard shortcuts
7. `#region Button Clicks` - Event handlers for button clicks
8. `#region ComboBox & UI Events` - UI event handlers and validation
9. `#region Helpers` or `#region Private Methods` - Helper and utility methods
10. `#region Cleanup` or `#region Disposal` - Cleanup and disposal methods

**Method Ordering Within Regions**:

-   Public methods first
-   Protected methods second
-   Private methods third
-   Static methods at the end of each access level
-   Async methods grouped together when possible

**Rationale**: Consistent code organization reduces cognitive load when navigating large files (many UserControls exceed 1000 lines). Predictable structure enables faster code reviews and reduces merge conflicts in team environments.

**Enforcement**: Refactoring workflow MUST include region reorganization analysis. Pre-refactor reports MUST document current vs. target region structure. Code review checklist MUST verify region compliance.

### IV. Manual Validation Testing Approach

The project uses manual validation as the primary QA approach. Automated unit tests are aspirational but not currently implemented.

**Requirements**:

-   Every feature MUST define clear success criteria before implementation
-   Manual test scenarios MUST cover: happy path, error conditions, edge cases
-   Database operations MUST be verified through stored procedure outputs and history tables
-   UI workflows MUST be exercised on Windows (primary platform)
-   Performance benchmarks (sub-100ms UI response, 30s DB timeout) MUST be validated manually
-   Test results MUST be documented in feature branch or specification artifacts

**Rationale**: WinForms application complexity and MySQL stored procedure dependencies make integration testing more valuable than isolated unit tests. Manual validation provides real-world user experience verification and catches UI rendering issues that unit tests miss.

**Exceptions**: Helper classes and services MAY include unit tests when testability supports it. Future refactors SHOULD increase test coverage incrementally without blocking current development.

**Enforcement**: Feature specifications MUST include manual test scenarios. Code review MUST verify success criteria are testable. Release checklist MUST confirm manual validation completed.

### V. Environment-Aware Database Selection

Database connections MUST adapt automatically based on build configuration and machine environment.

**Requirements**:

-   Debug builds MUST use `mtm_wip_application_winforms_test` database
-   Release builds MUST use `MTM_WIP_Application_Winforms` database
-   Server selection logic: Release always uses `172.16.1.104`; Debug uses `172.16.1.104` if current machine matches, otherwise `localhost`
-   Connection strings MUST be centralized in `Helper_Database_Variables` (no hardcoding)
-   Test database name for new features: `mtm_wip_application_winform_test`

**Rationale**: Prevents accidental production database modifications during development. Supports both local MAMP development and shared server scenarios. Reduces deployment risk by enforcing test-first validation.

**Enforcement**: Build scripts MUST validate database selection logic. Deployment checklist MUST verify Release build targets production database. Code review MUST reject hardcoded connection strings.

### VI. Async-First UI Responsiveness

Long-running operations MUST execute asynchronously to maintain sub-100ms UI responsiveness.

**Requirements**:

-   All database operations MUST be asynchronous (methods end with `Async`)
-   UI thread MUST NOT be blocked with `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()`
-   Background operations MUST marshal back to UI thread with `Invoke`/`BeginInvoke`
-   Progress reporting MUST use `Helper_StoredProcedureProgress` for visual feedback
-   Cancellation tokens SHOULD be supported when user can interrupt operations

**Rationale**: Manufacturing applications run continuously on shop floor devices. Frozen UI causes user frustration and perceived instability. Async patterns prevent UI freezes during database queries, file I/O, and network operations.

**Enforcement**: Code review MUST reject synchronous database calls from UI thread. Performance testing MUST verify button click responsiveness. Static analysis SHOULD warn on blocking async patterns.

### VII. Centralized Error Handling with Service_ErrorHandler

All error presentation MUST route through `Service_ErrorHandler`, never direct `MessageBox.Show()` calls.

**Requirements**:

-   Use `Service_ErrorHandler.HandleException()` with severity classification (Low/Medium/High/Fatal)
-   Provide retry actions when operations can be retried
-   Include context data for debugging (user ID, operation details)
-   Error cooldown mechanism prevents duplicate messages within 5 seconds
-   All exceptions MUST be logged to `log_error` table with full context

**Rationale**: Centralized error handling provides consistent user experience, prevents error spam, enables telemetry collection, and supports user-friendly error messages with technical details hidden in expandable panels.

**Enforcement**: Code review MUST reject direct MessageBox usage. Refactoring workflow MUST convert MessageBox to Service_ErrorHandler. Static analysis SHOULD detect MessageBox.Show() calls.

### VIII. Documentation and XML Comments

Public APIs MUST include XML documentation explaining intent, parameters, return values, and exceptions.

**Requirements**:

-   All public classes, methods, properties MUST have `<summary>` tags
-   Complex internal methods MUST have XML comments when logic is non-obvious
-   Parameters require `<param>` tags, returns require `<returns>`, exceptions require `<exception>` tags
-   Inline comments MUST focus on "why" not "what"
-   Breaking changes or deprecated methods MUST include `<remarks>` with migration guidance

**Rationale**: Long-lived application requires maintainability across team changes. IntelliSense tooltips reduce context-switching when calling methods. XML docs enable automated documentation generation.

**Enforcement**: Code review MUST verify XML docs on public APIs. Missing documentation SHOULD trigger warnings in build output.

### IX. Theme System Integration via Core_Themes (MANDATORY)

All Forms and UserControls MUST integrate the MTM theme system for DPI scaling, runtime layout adjustments, and consistent visual theming.

**Requirements**:

-   **Constructor Pattern**: Every Form/UserControl constructor MUST call:
    ```csharp
    Core_Themes.ApplyDpiScaling(this);
    Core_Themes.ApplyRuntimeLayoutAdjustments(this);
    ```
-   **Call Order**: Theme methods MUST be called immediately after `InitializeComponent()`
-   **Color Tokens**: Custom colors MUST use `Model_Shared_UserUiColors` theme tokens with `SystemColors` fallbacks
-   **Hardcoded Colors**: ANY hardcoded `Color.FromArgb()` or `Color.Blue` MUST include `// ACCEPTABLE: [reason]` comment
-   **AutoScaleMode**: All Forms/UserControls MUST set `AutoScaleMode = AutoScaleMode.Dpi`
-   **Database Themes**: Theme colors are stored in MySQL `app_themes` table (9 themes, 203 properties each)

**Approved Color Patterns**:

```csharp
// ✅ CORRECT: Theme token with fallback
var colors = Model_Application_Variables.UserUiColors;
button.BackColor = colors.ButtonBackColor ?? SystemColors.Control;

// ✅ CORRECT: Semantic theme color
label.ForeColor = colors.ErrorColor ?? Color.Red;

// ✅ CORRECT: Documented brand color
// ACCEPTABLE: Company logo brand color (not user-themeable)
panelLogo.BackColor = Color.FromArgb(0, 122, 204);

// ❌ WRONG: Undocumented hardcoded color
button.BackColor = Color.Blue;  // Missing justification comment
```

**WinForms UI Architecture Requirements**:

-   **Control Naming**: `{ComponentName}_{ControlType}_{Purpose}` (e.g., `Transactions_Panel_Main`)
-   **No Abbreviations**: Use `ComboBox` not `cbo`, `TextBox` not `txt`, `Label` not `lbl`
-   **AutoSize Pattern**: All containers MUST use `AutoSize = true` with `AutoSizeMode.GrowAndShrink`
-   **Docking Cascade**: Apply `Dock = DockStyle.Fill` from root Panel to leaf controls
-   **Leaf Control Sizing**: All input controls (TextBox, ComboBox, DateTimePicker) MUST have `MinimumSize` and `MaximumSize` (typically 175x23)
-   **NO Hardcoded Sizes**: Container controls MUST NOT have hardcoded `Size` properties (rely on AutoSize)
-   **NO Widths > 1000px**: Excessive widths indicate missing AutoSize configuration

**Rationale**: Manufacturing environment requires pixel-perfect rendering across diverse display configurations (100%-200% DPI scaling). Database-backed themes enable user personalization without code changes. Consistent UI architecture prevents the "12,000 pixel control" issue where AI generates oversized controls by referencing non-compliant examples. Theme token usage ensures visual consistency and enables dynamic theme switching.

**Enforcement**:

-   Code review MUST verify `ApplyDpiScaling` and `ApplyRuntimeLayoutAdjustments` in constructors
-   MCP tool `validate_ui_scaling` MUST pass before merging
-   Designer files MUST follow naming convention and AutoSize patterns
-   Hardcoded colors without `// ACCEPTABLE:` comment trigger review rejection
-   Static analysis SHOULD detect missing theme integration
-   Refactoring workflow MUST include theme compliance in pre-refactor analysis

**Reference Documentation**:

-   `Documentation/Theme-System-Reference.md` - Complete theme system API and patterns
-   `specs/005-transaction-viewer-form/RefactorPortion/UI-Architecture-Analysis.md` - WinForms layout best practices
-   `specs/005-transaction-viewer-form/RefactorPortion/WinForms-UI-Compliance-Checklist.md` - Compliance tracking
-   `.github/instructions/ui-compliance/theming-compliance.instructions.md` - MCP-parseable rules

## Additional Constraints

### Technology Stack Requirements

**Mandated Technologies**:

-   .NET 8.0 (no multi-targeting, file-scoped namespaces permitted)
-   Windows Forms (WinForms) for UI
-   MySQL 5.7+ via MySql.Data connector
-   Stored procedures exclusively for data access

**Forbidden Practices**:

-   Entity Framework, Dapper, or other ORMs
-   Inline SQL or dynamic SQL construction
-   Direct MySqlConnection/MySqlCommand usage outside helpers
-   Third-party UI frameworks (Avalonia, WPF, etc.)

**Rationale**: Consistency with existing 5-year codebase. MySQL stored procedures provide battle-tested business logic. WinForms designer integration requirements.

### Security Best Practices

**Requirements**:

-   Never log passwords, connection strings, or sensitive PII
-   Parameterized queries only (via stored procedures)
-   Input validation at UI boundary before database calls
-   Connection strings stored in configuration files (not source code)
-   Credentials rotated regularly in production environments

**Rationale**: Manufacturing data includes business-sensitive inventory, user information, and operational metrics. SQL injection defense through stored procedures. Audit trail via `log_error` table.

### Performance Standards

**Measurable Requirements**:

-   UI interactions: sub-100ms response time (button clicks, field updates)
-   Database queries: complete within 30-second timeout
-   Connection pool: MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s
-   Startup time: application launches within 5 seconds
-   Memory usage: working set under 500MB during typical operations

**Monitoring**:

-   Query execution time logged with warnings for operations exceeding thresholds
-   Configurable thresholds per operation category: Query (500ms), Modification (1000ms), Batch (5000ms), Report (2000ms)
-   Connection pool metrics tracked in Service_DebugTracer

**Rationale**: Shop floor environment requires responsive, reliable application. Database timeouts prevent hung connections. Performance monitoring enables proactive optimization.

### WinForms UI Architecture Standards

**Control Naming Convention (MANDATORY)**:

-   **Format**: `{ComponentName}_{ControlType}_{Purpose}`
-   **No Abbreviations**: Use `ComboBox` not `cbo`, `TextBox` not `txt`, `Label` not `lbl`, `Button` not `btn`
-   **Examples**: `TransactionSearchControl_ComboBox_PartNumber`, `InventoryTab_Panel_Main`, `MainForm_TableLayout_Content`
-   **Context Preservation**: Child control names MUST include parent component context

**Rationale**: Consistent naming prevents ambiguity when AI references controls for code generation. Full type names prevent confusion (`ComboBox` vs `ComboBoxEx`, `TextBox` vs `TextBoxBase`). Context preservation enables search/replace refactoring and clarifies component ownership.

**Layout Architecture (MANDATORY)**:

-   **Root Container**: Every Form/UserControl MUST start with Panel or TableLayoutPanel docked fill
-   **AutoSize Cascade**: All containers MUST use `AutoSize = true` with `AutoSizeMode = AutoSizeMode.GrowAndShrink`
-   **Docking Pattern**: Apply `Dock = DockStyle.Fill` from root to leaves in container hierarchy
-   **Per-Field Containers**: Each input field MUST have dedicated TableLayoutPanel with label (Row 0) and control (Row 1)
-   **Star Sizing**: Expansion rows/columns MUST use `SizeType.Percent, 100F` for responsive behavior
-   **Leaf Control Constraints**: Input controls (TextBox, ComboBox, DateTimePicker) MUST have `MinimumSize` and `MaximumSize` (typically 175x23)
-   **NO Hardcoded Sizes on Containers**: Container `Size` property MUST be removed (rely on AutoSize)
-   **Maximum Width Limit**: NO control width SHOULD exceed 1000 pixels (indicates missing AutoSize)

**Rationale**: Deep nesting with AutoSize cascade prevents the "12,000 pixel control" issue where AI generates oversized controls. Consistent hierarchy enables predictable layout behavior across DPI levels. Per-field containers ensure uniform label/control spacing. Leaf constraints prevent unwanted expansion while maintaining flexibility.

**Enforcement**:

-   Code review MUST verify naming convention compliance
-   Designer files exceeding 1000px width trigger immediate investigation
-   MCP tool `validate_ui_scaling` MUST detect architecture violations
-   Refactoring workflow MUST include UI architecture compliance analysis
-   Pre-refactor reports MUST document non-compliant patterns

**Reference Documentation**:

-   `specs/005-transaction-viewer-form/RefactorPortion/UI-Architecture-Analysis.md` - Complete pattern documentation
-   `specs/005-transaction-viewer-form/RefactorPortion/WinForms-UI-Compliance-Checklist.md` - Compliance tracking

### Documentation and Instruction File Standards

**Instruction File Organization (MANDATORY)**:

-   **Location**: All instruction files MUST reside in `.github/instructions/` or `.github/prompts/`
-   **Naming**: Use kebab-case with `.instructions.md` or `.prompt.md` suffix
-   **Subdirectories**: Group related files: `ui-compliance/`, `database/`, `testing/`, etc.
-   **YAML Front Matter**: MCP-parseable instruction files MUST include YAML metadata:
    ```yaml
    ---
    description: 'Brief description of file purpose'
    applyTo: '**/*.cs' # Glob pattern for applicable files
    mcpTools: [validate_ui_scaling, apply_ui_fixes] # Optional: MCP tools that use this file
    ---
    ```

**Rationale**: Structured organization enables AI agents to locate applicable guidance efficiently. YAML front matter enables MCP tools to parse and validate compliance programmatically. Glob patterns clarify which files each instruction set governs.

**Instruction File Content Requirements**:

-   **Scope Declaration**: Opening section MUST define what the file governs
-   **Required vs Optional**: Use MUST/SHOULD/MAY consistently per RFC 2119 semantics
-   **Code Examples**: Include before/after code blocks for complex patterns
-   **Rationale Sections**: Explain WHY rules exist, not just WHAT they are
-   **Exception Handling**: Document approved exceptions with decision criteria
-   **Cross-References**: Link to related instruction files and documentation

**Prompt File Standards**:

-   **Agent Communication Rules**: Define when to stop for user input vs continue autonomously
-   **Tool Requirements**: List required MCP tools explicitly
-   **Execution Flow**: Numbered steps showing prompt workflow
-   **Validation Criteria**: Define success/failure conditions
-   **Output Format**: Specify expected deliverables

**Reference Documentation Standards**:

-   **Comprehensive Docs**: Major systems MUST have dedicated reference docs (e.g., `Documentation/Theme-System-Reference.md`)
-   **Table of Contents**: Reference docs exceeding 200 lines MUST include TOC
-   **Section Markers**: Use HTML comments for future-proof section discovery: `<!-- {SectionName} Start -->` ... `<!-- {SectionName} End -->`
-   **Update Timestamps**: Reference docs MUST include "Last Updated: YYYY-MM-DD" and version number
-   **Cross-Platform Examples**: When applicable, show Windows/macOS/Linux variations

**Enforcement**:

-   Pull requests adding instruction files MUST include YAML front matter validation
-   Instruction files without rationale sections trigger review comments
-   Prompt files MUST be tested with actual agent execution before merging
-   Reference docs MUST be validated for broken internal links

**Rationale**: Instruction files are the foundation of AI-assisted development quality. Structured metadata enables tooling. Clear rationale helps future maintainers understand constraints. Tested prompts prevent agent confusion.

## Development Workflow

### Refactoring Standards

**Process**:

1. Generate Pre-Refactor Report with recursive dependency analysis
2. Await approval before making any code changes
3. Create feature branch: `refactor/<file-stem>/<yyyyMMdd>`
4. Apply atomic commits by category: region reorganization, DAO patterns, UI improvements
5. Update documentation only if behavior changes
6. Generate post-refactor compliance summary
7. Provide rollback diff

**Compliance Checklist**:

-   Region organization and method ordering
-   DAO pattern (Model_Dao_Result<T>, stored procedures, parameter naming)
-   Null safety and error handling
-   Progress reporting integration
-   Theme and DPI scaling
-   Logging standards
-   Stored procedure contract verification

**Rationale**: Large files (1000+ lines) require careful refactoring to avoid regressions. Recursive dependency analysis surfaces upstream/downstream impacts. Atomic commits enable surgical rollbacks.

### Code Review Requirements

**Mandatory Checks**:

-   Region organization follows standard order
-   No [NEEDS CLARIFICATION] markers remain in specifications
-   All public APIs have XML documentation
-   Error handling uses Service_ErrorHandler (no MessageBox)
-   Database operations use stored procedures (no inline SQL)
-   Async patterns used correctly (no blocking calls on UI thread)
-   Manual validation scenarios defined and tested

**Quality Gates**:

-   Compilation succeeds with zero errors
-   WinForms designer opens without errors
-   Manual validation scenarios pass
-   Performance benchmarks met (if applicable)
-   Documentation updated (if behavior changed)

**Rationale**: Quality gates prevent regressions in production manufacturing environment. Consistent enforcement builds technical discipline. Documentation requirements ensure knowledge transfer.

### Branch and Commit Conventions

**Branch Naming**:

-   Feature: `###-feature-name` (e.g., `002-comprehensive-database-layer`)
-   Refactor: `refactor/<file-stem>/<yyyyMMdd>` (e.g., `refactor/dao-inventory/20251013`)
-   Bugfix: `fix/<issue-description>` (e.g., `fix/null-ref-combo-box`)

**Commit Messages**:

-   Format: `<type>: <description>` (e.g., `refactor: reorganize Dao_Inventory into standard regions`)
-   Types: `feat`, `fix`, `refactor`, `docs`, `test`, `perf`, `chore`
-   Breaking changes: Append `!` (e.g., `feat!: change Model_Dao_Result API signature`)

**Rationale**: Consistent naming enables branch discovery and automated tooling. Conventional commits support changelog generation and semantic versioning.

## Governance

### Constitution Authority

This constitution supersedes all other practices and documentation. When conflicts arise, constitution principles take precedence.

### Amendment Process

**Requirements**:

1. Amendments require documented rationale and team approval
2. Version number MUST increment per semantic versioning:
    - MAJOR: Backward-incompatible governance changes (e.g., removing principle)
    - MINOR: New principle added or material expansion
    - PATCH: Clarifications, wording, typo fixes
3. Amendment history MUST be tracked in Sync Impact Report comment
4. Affected templates and documentation MUST be updated within same commit

### Compliance Review

**Process**:

-   All pull requests MUST verify compliance with applicable principles
-   Complexity violations (e.g., skipping stored procedures for "just this one case") MUST be justified in Complexity Tracking table
-   Refactoring workflow Pre-Refactor Reports MUST include constitution compliance analysis
-   Code review checklist directly references constitution sections

### Specification Framework Integration

**Requirements**:

-   `/speckit.plan` command MUST evaluate Constitution Check gates before Phase 0 research
-   Gates MUST be re-evaluated after Phase 1 design
-   Violations MUST be justified in Complexity Tracking table or specification revised
-   No implementation proceeds while constitution violations remain unresolved

**Rationale**: Constitution provides objective quality bar preventing technical debt accumulation. Early gate enforcement prevents late-stage rework. Complexity tracking makes conscious trade-offs visible.

### Living Document

This constitution is versioned and maintained as a living document. Amendments reflect lessons learned from production incidents, developer pain points, and evolving best practices.

**Review Cadence**: Annually or when major architectural changes proposed (e.g., .NET version upgrade, database migration).

**Feedback Channels**: Development team retrospectives, code review observations, production incident post-mortems.

**Version**: 1.2.0 | **Ratified**: 2025-10-13 | **Last Amended**: 2025-11-01
