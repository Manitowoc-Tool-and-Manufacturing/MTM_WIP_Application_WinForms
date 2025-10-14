<!--
Sync Impact Report:
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
- Every DAO method MUST call stored procedures via `Helper_Database_StoredProcedure`
- All stored procedures MUST include `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)` parameters
- C# parameters MUST NOT include `p_` prefix (helper adds automatically)
- Parameter names MUST use PascalCase matching C# model properties
- MySQL 5.7 compatibility MUST be maintained (no CTEs, no window functions)

**Rationale**: Stored procedures provide centralized business logic, performance optimization through query plan caching, and defense against SQL injection. The uniform OUT parameter pattern enables consistent error handling across 74+ stored procedures.

**Enforcement**: Code review MUST reject any direct `MySqlConnection`, `MySqlCommand`, or string concatenation of SQL. Static analysis tools SHOULD flag violations.

### II. DaoResult<T> Wrapper Pattern (MANDATORY)

All data access methods MUST return structured `DaoResult` or `DaoResult<T>` responses, never throwing exceptions for expected failures.

**Requirements**:
- Success case: `DaoResult<T>.Success(data, message)`
- Failure case: `DaoResult<T>.Failure(message, exception)`
- Properties: `IsSuccess`, `Data`, `Message`, `Exception`
- Database errors MUST be caught and wrapped in DaoResult
- Callers MUST check `IsSuccess` before accessing `Data`

**Rationale**: Eliminates exception-driven control flow for expected failures (record not found, validation errors). Provides consistent API contract across all DAOs, enabling predictable error handling in UI layer.

**Enforcement**: All methods in `Data/` folder MUST return DaoResult variants. Code review MUST verify exception wrapping and null-safety.

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
- Public methods first
- Protected methods second
- Private methods third
- Static methods at the end of each access level
- Async methods grouped together when possible

**Rationale**: Consistent code organization reduces cognitive load when navigating large files (many UserControls exceed 1000 lines). Predictable structure enables faster code reviews and reduces merge conflicts in team environments.

**Enforcement**: Refactoring workflow MUST include region reorganization analysis. Pre-refactor reports MUST document current vs. target region structure. Code review checklist MUST verify region compliance.

### IV. Manual Validation Testing Approach

The project uses manual validation as the primary QA approach. Automated unit tests are aspirational but not currently implemented.

**Requirements**:
- Every feature MUST define clear success criteria before implementation
- Manual test scenarios MUST cover: happy path, error conditions, edge cases
- Database operations MUST be verified through stored procedure outputs and history tables
- UI workflows MUST be exercised on Windows (primary platform)
- Performance benchmarks (sub-100ms UI response, 30s DB timeout) MUST be validated manually
- Test results MUST be documented in feature branch or specification artifacts

**Rationale**: WinForms application complexity and MySQL stored procedure dependencies make integration testing more valuable than isolated unit tests. Manual validation provides real-world user experience verification and catches UI rendering issues that unit tests miss.

**Exceptions**: Helper classes and services MAY include unit tests when testability supports it. Future refactors SHOULD increase test coverage incrementally without blocking current development.

**Enforcement**: Feature specifications MUST include manual test scenarios. Code review MUST verify success criteria are testable. Release checklist MUST confirm manual validation completed.

### V. Environment-Aware Database Selection

Database connections MUST adapt automatically based on build configuration and machine environment.

**Requirements**:
- Debug builds MUST use `mtm_wip_application_winforms_test` database
- Release builds MUST use `mtm_wip_application` database
- Server selection logic: Release always uses `172.16.1.104`; Debug uses `172.16.1.104` if current machine matches, otherwise `localhost`
- Connection strings MUST be centralized in `Helper_Database_Variables` (no hardcoding)
- Test database name for new features: `mtm_wip_application_winform_test`

**Rationale**: Prevents accidental production database modifications during development. Supports both local MAMP development and shared server scenarios. Reduces deployment risk by enforcing test-first validation.

**Enforcement**: Build scripts MUST validate database selection logic. Deployment checklist MUST verify Release build targets production database. Code review MUST reject hardcoded connection strings.

### VI. Async-First UI Responsiveness

Long-running operations MUST execute asynchronously to maintain sub-100ms UI responsiveness.

**Requirements**:
- All database operations MUST be asynchronous (methods end with `Async`)
- UI thread MUST NOT be blocked with `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()`
- Background operations MUST marshal back to UI thread with `Invoke`/`BeginInvoke`
- Progress reporting MUST use `Helper_StoredProcedureProgress` for visual feedback
- Cancellation tokens SHOULD be supported when user can interrupt operations

**Rationale**: Manufacturing applications run continuously on shop floor devices. Frozen UI causes user frustration and perceived instability. Async patterns prevent UI freezes during database queries, file I/O, and network operations.

**Enforcement**: Code review MUST reject synchronous database calls from UI thread. Performance testing MUST verify button click responsiveness. Static analysis SHOULD warn on blocking async patterns.

### VII. Centralized Error Handling with Service_ErrorHandler

All error presentation MUST route through `Service_ErrorHandler`, never direct `MessageBox.Show()` calls.

**Requirements**:
- Use `Service_ErrorHandler.HandleException()` with severity classification (Low/Medium/High/Fatal)
- Provide retry actions when operations can be retried
- Include context data for debugging (user ID, operation details)
- Error cooldown mechanism prevents duplicate messages within 5 seconds
- All exceptions MUST be logged to `log_error` table with full context

**Rationale**: Centralized error handling provides consistent user experience, prevents error spam, enables telemetry collection, and supports user-friendly error messages with technical details hidden in expandable panels.

**Enforcement**: Code review MUST reject direct MessageBox usage. Refactoring workflow MUST convert MessageBox to Service_ErrorHandler. Static analysis SHOULD detect MessageBox.Show() calls.

### VIII. Documentation and XML Comments

Public APIs MUST include XML documentation explaining intent, parameters, return values, and exceptions.

**Requirements**:
- All public classes, methods, properties MUST have `<summary>` tags
- Complex internal methods MUST have XML comments when logic is non-obvious
- Parameters require `<param>` tags, returns require `<returns>`, exceptions require `<exception>` tags
- Inline comments MUST focus on "why" not "what"
- Breaking changes or deprecated methods MUST include `<remarks>` with migration guidance

**Rationale**: Long-lived application requires maintainability across team changes. IntelliSense tooltips reduce context-switching when calling methods. XML docs enable automated documentation generation.

**Enforcement**: Code review MUST verify XML docs on public APIs. Missing documentation SHOULD trigger warnings in build output.

## Additional Constraints

### Technology Stack Requirements

**Mandated Technologies**:
- .NET 8.0 (no multi-targeting, file-scoped namespaces permitted)
- Windows Forms (WinForms) for UI
- MySQL 5.7+ via MySql.Data connector
- Stored procedures exclusively for data access

**Forbidden Practices**:
- Entity Framework, Dapper, or other ORMs
- Inline SQL or dynamic SQL construction
- Direct MySqlConnection/MySqlCommand usage outside helpers
- Third-party UI frameworks (Avalonia, WPF, etc.)

**Rationale**: Consistency with existing 5-year codebase. MySQL stored procedures provide battle-tested business logic. WinForms designer integration requirements.

### Security Best Practices

**Requirements**:
- Never log passwords, connection strings, or sensitive PII
- Parameterized queries only (via stored procedures)
- Input validation at UI boundary before database calls
- Connection strings stored in configuration files (not source code)
- Credentials rotated regularly in production environments

**Rationale**: Manufacturing data includes business-sensitive inventory, user information, and operational metrics. SQL injection defense through stored procedures. Audit trail via `log_error` table.

### Performance Standards

**Measurable Requirements**:
- UI interactions: sub-100ms response time (button clicks, field updates)
- Database queries: complete within 30-second timeout
- Connection pool: MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s
- Startup time: application launches within 5 seconds
- Memory usage: working set under 500MB during typical operations

**Monitoring**:
- Query execution time logged with warnings for operations exceeding thresholds
- Configurable thresholds per operation category: Query (500ms), Modification (1000ms), Batch (5000ms), Report (2000ms)
- Connection pool metrics tracked in Service_DebugTracer

**Rationale**: Shop floor environment requires responsive, reliable application. Database timeouts prevent hung connections. Performance monitoring enables proactive optimization.

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
- Region organization and method ordering
- DAO pattern (DaoResult<T>, stored procedures, parameter naming)
- Null safety and error handling
- Progress reporting integration
- Theme and DPI scaling
- Logging standards
- Stored procedure contract verification

**Rationale**: Large files (1000+ lines) require careful refactoring to avoid regressions. Recursive dependency analysis surfaces upstream/downstream impacts. Atomic commits enable surgical rollbacks.

### Code Review Requirements

**Mandatory Checks**:
- Region organization follows standard order
- No [NEEDS CLARIFICATION] markers remain in specifications
- All public APIs have XML documentation
- Error handling uses Service_ErrorHandler (no MessageBox)
- Database operations use stored procedures (no inline SQL)
- Async patterns used correctly (no blocking calls on UI thread)
- Manual validation scenarios defined and tested

**Quality Gates**:
- Compilation succeeds with zero errors
- WinForms designer opens without errors
- Manual validation scenarios pass
- Performance benchmarks met (if applicable)
- Documentation updated (if behavior changed)

**Rationale**: Quality gates prevent regressions in production manufacturing environment. Consistent enforcement builds technical discipline. Documentation requirements ensure knowledge transfer.

### Branch and Commit Conventions

**Branch Naming**:
- Feature: `###-feature-name` (e.g., `002-comprehensive-database-layer`)
- Refactor: `refactor/<file-stem>/<yyyyMMdd>` (e.g., `refactor/dao-inventory/20251013`)
- Bugfix: `fix/<issue-description>` (e.g., `fix/null-ref-combo-box`)

**Commit Messages**:
- Format: `<type>: <description>` (e.g., `refactor: reorganize Dao_Inventory into standard regions`)
- Types: `feat`, `fix`, `refactor`, `docs`, `test`, `perf`, `chore`
- Breaking changes: Append `!` (e.g., `feat!: change DaoResult API signature`)

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
- All pull requests MUST verify compliance with applicable principles
- Complexity violations (e.g., skipping stored procedures for "just this one case") MUST be justified in Complexity Tracking table
- Refactoring workflow Pre-Refactor Reports MUST include constitution compliance analysis
- Code review checklist directly references constitution sections

### Specification Framework Integration

**Requirements**:
- `/speckit.plan` command MUST evaluate Constitution Check gates before Phase 0 research
- Gates MUST be re-evaluated after Phase 1 design
- Violations MUST be justified in Complexity Tracking table or specification revised
- No implementation proceeds while constitution violations remain unresolved

**Rationale**: Constitution provides objective quality bar preventing technical debt accumulation. Early gate enforcement prevents late-stage rework. Complexity tracking makes conscious trade-offs visible.

### Living Document

This constitution is versioned and maintained as a living document. Amendments reflect lessons learned from production incidents, developer pain points, and evolving best practices.

**Review Cadence**: Annually or when major architectural changes proposed (e.g., .NET version upgrade, database migration).

**Feedback Channels**: Development team retrospectives, code review observations, production incident post-mortems.

**Version**: 1.0.0 | **Ratified**: 2025-10-13 | **Last Amended**: 2025-10-13