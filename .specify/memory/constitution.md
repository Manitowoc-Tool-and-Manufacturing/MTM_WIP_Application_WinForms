<!--
SYNC IMPACT REPORT
==================
Version Change: NEW → 1.0.0 (Initial ratification)
Generated: 2025-12-13

PRINCIPLES ESTABLISHED:
- I. Centralized Database Access (NON-NEGOTIABLE) - Mandates Helper_Database_StoredProcedure usage, documents 2 architectural exceptions
- II. Stored Procedures Only (NON-NEGOTIABLE) - Forbids inline SQL, establishes naming convention
- III. Model_Dao_Result Pattern (NON-NEGOTIABLE) - Standardizes DAO return types and error handling
- IV. Centralized Error Handling (NON-NEGOTIABLE) - Forbids MessageBox.Show, mandates Service_ErrorHandler
- V. Immediate Connection Disposal (NON-NEGOTIABLE) - Disables pooling (Pooling=false), enforces using statements
- VI. Async-First I/O (NON-NEGOTIABLE) - Mandates async/await for all I/O, forbids blocking calls
- VII. Theme System Integration (MANDATORY) - Requires ThemedForm/ThemedUserControl inheritance

SECTIONS ADDED:
- Technology Constraints (MySQL 5.7.24 compatibility, .NET 8.0/C# 12.0, DI hybrid strategy)
- Quality Standards (XML documentation, #region organization, structured logging, testing requirements)
- Governance (Amendment process, compliance verification, runtime guidance references)

TEMPLATES REQUIRING UPDATES:
✅ .specify/templates/plan-template.md - Verify constitution compliance checks
✅ .specify/templates/spec-template.md - Add database access pattern requirements
✅ .specify/templates/tasks-template.md - Include constitution verification in task checklist
✅ .github/copilot-instructions.md - Already aligned (referenced in Governance section)
✅ .github/instructions/*.instructions.md - Already aligned (specific component guidance)

FOLLOW-UP ACTIONS: ✅ ALL COMPLETED
✅ Review template files for constitution alignment
   - plan-template.md: Added 11-point constitution checklist
   - spec-template.md: Added database access requirements section
   - tasks-template.md: Added 9-point constitution verification per user story
✅ Add automated checks for MessageBox.Show and direct connection usage
   - Created: .specify/scripts/powershell/validate-constitution-compliance.ps1
   - Scans for: MessageBox.Show, direct connections, blocking async calls, missing using statements
   - Respects approved architectural exceptions
✅ Document architectural exceptions in code
   - Service_OnStartup_Database.cs: Added comprehensive XML remarks documenting exception rationale
   - Helper_Control_MySqlSignal.cs: Added comprehensive XML remarks documenting exception rationale
✅ Add constitution compliance to PR checklist
   - Created: .github/PULL_REQUEST_TEMPLATE.md
   - Includes: 11-point constitution compliance checklist, automated check verification, reviewer guidelines

CODEBASE ANALYSIS COMPLETED USING SERENA:
✅ Helper_Database_StoredProcedure structure analyzed (14 methods identified)
✅ ExecuteReaderAsync usage mapped (18 SQL Server usages in Service_VisualDatabase, 4 MySQL usages in Service_Analytics/Service_Migration)
✅ Direct MySqlConnection usage identified (9 code files, 28 total instances)
✅ DAO pattern validated (21 DAO files in Data/, all use Helper_Database_StoredProcedure)
✅ MessageBox.Show usage checked (NONE found - principle already followed)
✅ Architectural patterns memory reviewed (patterns match codebase reality)
✅ Service_VisualDatabase GetConnectionString analyzed (needs Pooling=false addition)

CONSTITUTION ALIGNS WITH:
- Feature 001-fix-mysql-connection-leaks specification (immediate disposal, Pooling=false)
- Existing codebase patterns (DAO structure, error handling, theme system)
- Project constraints (MySQL 5.7.24, .NET 8.0, WinForms architecture)
-->

# MTM WIP Application Constitution

## Core Principles

### I. Centralized Database Access (NON-NEGOTIABLE)
ALL database access MUST go through `Helper_Database_StoredProcedure`. Direct `MySqlConnection` or `SqlConnection` instantiation is FORBIDDEN except for two documented architectural exceptions: (1) `Service_OnStartup_Database` for parameter cache initialization, and (2) `Helper_Control_MySqlSignal` for network diagnostics. Both exceptions MUST use `using` statements for proper disposal. This principle ensures consistent connection management, automatic parameter prefix detection, and uniform error handling across the entire application.

**Rationale**: Centralized access prevents connection leaks, enforces stored procedure usage, provides standardized `Model_Dao_Result<T>` pattern, and enables consistent performance tracing through `Service_DebugTracer` integration.

### II. Stored Procedures Only (NON-NEGOTIABLE)
ALL database operations MUST use stored procedures - inline SQL is FORBIDDEN. The only exception is diagnostic queries (e.g., `SELECT 1` for connection testing) which MUST still go through `Helper_Database_StoredProcedure.ExecuteScalarWithStatusAsync`. Stored procedure names follow convention: `{prefix}_{entity}_{action}` (e.g., `inv_inventory_Get_ByUser`, `md_analytics_GetTransactionsByRange`).

**Rationale**: Stored procedures provide security through parameterization, performance through query plan caching, maintainability through centralized SQL logic, and compatibility with MySQL 5.7.24 constraints (no CTEs, window functions, or JSON functions).

### III. Model_Dao_Result Pattern (NON-NEGOTIABLE)
ALL DAO methods MUST return `Model_Dao_Result<T>` with `IsSuccess`, `Data`, and `ErrorMessage` properties. DAO methods MUST NOT throw exceptions to callers - exceptions are caught, logged via `LoggingUtility`, and returned as failure results with user-friendly messages. Callers MUST check `IsSuccess` before accessing `Data`. This applies to ALL data access operations without exception.

**Rationale**: Consistent error handling eliminates unhandled exceptions, provides user-friendly error messages, enables caller to make decisions based on result state, and ensures all database errors are logged centrally.

### IV. Centralized Error Handling (NON-NEGOTIABLE)
ALL error presentation MUST use `Service_ErrorHandler` - direct use of `MessageBox.Show` is ABSOLUTELY FORBIDDEN. Exceptions MUST be handled through `Service_ErrorHandler.HandleException()` with severity classification (`Enum_ErrorSeverity`), context data dictionary, and caller identification. User-facing errors MUST use `Service_ErrorHandler.ShowUserError()` for consistent presentation.

**Rationale**: Centralized error handling ensures consistent user experience, automatic error logging with context, severity-based routing (log vs show vs close app), and structured error reporting for troubleshooting.

### V. Immediate Connection Disposal (NON-NEGOTIABLE)
Connection pooling MUST be disabled (`Pooling=false`) for both MySQL and SQL Server connections. Every database operation MUST follow the pattern: Open → Execute → Close. ALL connections MUST use `using` statements or `using var` pattern to guarantee immediate disposal. Helper_Database_StoredProcedure methods MUST create a new connection for each operation and MUST NOT cache connections. Zero idle connections MUST remain when application is idle.

**Rationale**: Immediate disposal eliminates connection pool complexity, prevents connection leaks (impossible to leak to non-existent pool), provides explicit resource management with clear lifecycle visibility, and simplifies debugging (connection issues are immediately apparent rather than hidden in pool).

### VI. Async-First I/O (NON-NEGOTIABLE)
ALL I/O operations (database, file, network) MUST be async using `async`/`await` keywords. Blocking calls like `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` are FORBIDDEN. All database methods MUST use `ExecuteDataTableWithStatusAsync`, `ExecuteNonQueryWithStatusAsync`, `ExecuteScalarWithStatusAsync`, or `ExecuteReaderAsync` (marked [Obsolete] - prefer ExecuteDataTableWithStatusAsync). `ConfigureAwait(false)` SHOULD be used in library code.

**Rationale**: Async operations prevent UI freezing in WinForms, improve scalability by freeing threads during I/O waits, and align with modern .NET best practices for responsive applications.

### VII. Theme System Integration (MANDATORY)
ALL forms MUST inherit from `ThemedForm` (NOT `Form`). ALL user controls MUST inherit from `ThemedUserControl` (NOT `UserControl`). Direct color setting in InitializeComponent() is FORBIDDEN - themes are applied automatically by base classes through dependency injection. Theme providers (`IThemeProvider`) are injected and managed by the DI container.

**Rationale**: Consistent theming across application, centralized theme management, user preference persistence, and automatic color application without manual form updates.

## Technology Constraints

### MySQL 5.7.24 Compatibility (NON-NEGOTIABLE)
Application targets MySQL 5.7.24 (LEGACY version). The following MySQL 8.0+ features are ABSOLUTELY FORBIDDEN: JSON functions (`JSON_EXTRACT`, `JSON_CONTAINS`), Common Table Expressions (CTEs with `WITH` clause), Window functions (`ROW_NUMBER()`, `RANK()`), `CHECK` constraints, and `LATERAL` joins. Use stored procedures with legacy-compatible SQL only.

**Rationale**: Production environment runs MySQL 5.7.24 with no planned upgrade path. Using 8.0+ features would break production deployments and require costly server upgrades.

### .NET 8.0 and C# 12.0 (MANDATORY)
Application uses .NET 8.0 (`<TargetFramework>net8.0-windows</TargetFramework>`) and C# 12.0 language features. FORBIDDEN: .NET 9.0+ features, C# 13.0+ syntax. REQUIRED: Nullable reference types enabled (`<Nullable>enable</Nullable>`), implicit usings enabled, file-scoped namespaces, primary constructors (for new code only), and collection expressions where appropriate.

**Rationale**: .NET 8.0 is LTS (Long-Term Support) with support until November 2026. Newer versions introduce breaking changes and instability not acceptable for production WinForms applications.

### Dependency Injection Hybrid Strategy
LEGACY components (static DAOs, static services) remain unchanged unless explicitly refactored. ALL NEW components MUST be designed for dependency injection: interfaces defined (`IDao_Entity`, `IService_Entity`), registered in Program.cs ServiceCollection, and injected via constructor. Forms and controls use property injection for services. No service locator pattern.

**Rationale**: Gradual migration to DI without breaking existing working code. New code follows modern practices while respecting legacy architectural decisions.

## Quality Standards

### XML Documentation (MANDATORY)
ALL public classes, methods, properties, and events MUST have XML documentation (`/// <summary>`). Include `<param>`, `<returns>`, `<exception>`, and `<remarks>` tags as applicable. For `Model_Dao_Result<T>` returns, MUST document: "Check IsSuccess before accessing Data. ErrorMessage contains user-friendly message on failure."

**Rationale**: Enables IntelliSense for developers, generates API documentation automatically, improves code maintainability, and clarifies expected behavior and error handling.

### Region Organization (MANDATORY)
ALL C# files MUST use #region blocks in this EXACT order: Fields, Properties, Constructors, Methods (public → protected → private → static), Events, Helpers, Cleanup/Dispose. NO deviations allowed. Empty regions may be omitted.

**Rationale**: Consistent file structure improves code navigation, reduces time to find members, enables predictable code reviews, and enforces organizational discipline across 300+ files.

### Structured Logging (MANDATORY)
ALL logging MUST use `LoggingUtility.Log()` with CSV-structured format. Log entries include timestamp, log level, component, message, user (if applicable), and context. Database errors MUST use `LoggingUtility.LogDatabaseError()`. NO `Console.WriteLine()` or `Debug.WriteLine()` in production code. Logs stored in `%APPDATA%\MTM\Logs\`.

**Rationale**: Structured logs enable parsing, filtering, and analysis. CSV format allows Excel-based troubleshooting. Centralized logging ensures all errors are captured for post-mortem analysis.

## Governance

This constitution supersedes all other coding guidelines and practices for the MTM WIP Application. All code changes MUST comply with these principles. Non-compliance requires documented justification and explicit approval. Complexity additions MUST be justified against simplicity principles. Architecture violations MUST be documented with rationale in code comments (e.g., Service_OnStartup_Database parameter cache exception).

**Amendment Process**: Constitution changes require: (1) Documented rationale with impact analysis, (2) Review by project maintainers, (3) Version bump (MAJOR for principle changes, MINOR for additions, PATCH for clarifications), (4) Migration plan for existing code if applicable.

**Compliance Verification**: ALL pull requests MUST pass automated checks for: MessageBox.Show usage (forbidden), direct MySqlConnection/SqlConnection usage (forbidden except approved exceptions), Model_Dao_Result return types on DAO methods, XML documentation on public members, and #region organization. Manual code review MUST verify async patterns and error handling.

**Runtime Development Guidance**: Refer to `.github/copilot-instructions.md` for detailed coding patterns, `.github/instructions/*.instructions.md` for specific component guidance, and `AGENTS.md` for onboarding and project setup.

**Version**: 1.0.0 | **Ratified**: 2025-12-13 | **Last Amended**: 2025-12-13
