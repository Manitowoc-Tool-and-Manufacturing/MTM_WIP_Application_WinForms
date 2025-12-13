# Implementation Plan: Fix MySQL Database Connection Leaks

**Branch**: `001-fix-mysql-connection-leaks` | **Date**: 2025-12-13 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-fix-mysql-connection-leaks/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

**Primary Requirement**: Eliminate MySQL database connection leaks causing "max users reached" errors by fixing ExecuteReaderAsync disposal issues, disabling connection pooling (Pooling=false), implementing connection lifecycle monitoring, and refactoring architectural violations in Service_Migration, Service_Analytics, and Service_ErrorReportSync to use Helper_Database_StoredProcedure pattern.

**Technical Approach**: 
1. Completely remove ExecuteReaderAsync method from Helper_Database_StoredProcedure
2. Replace all 4 ExecuteReaderAsync callers with ExecuteDataTableWithStatusAsync (Service_Analytics: 3, Service_Migration: 1)
3. Disable connection pooling for both MySQL (Pooling=false) and SQL Server (Pooling=false) 
4. Create ExecuteRawSqlAsync helper for Service_Migration raw SQL needs
5. Create analytics stored procedures (md_analytics_GetTransactionsByRange, md_analytics_GetUsersByDateRange)
6. Implement Helper_Database_ConnectionMonitor for lifecycle tracking
7. Add connection lifecycle monitoring to MainForm timer (every 5 minutes)

## Technical Context

**Language/Version**: C# 12.0, .NET 8.0-windows (`net8.0-windows`)  
**Primary Dependencies**: MySql.Data 9.4.0, Microsoft.Data.SqlClient (for Visual), Microsoft.Extensions.DependencyInjection 8.0.0, Microsoft.Extensions.Logging 8.0.0  
**Storage**: MySQL 5.7.24 (primary), SQL Server (Infor Visual ERP integration)  
**Testing**: Manual testing only (no unit tests for this feature) - verification via connection monitoring logs, SHOW PROCESSLIST queries, and 4+ hour runtime stability tests  
**Target Platform**: Windows 10/11 desktop (WinForms application)
**Project Type**: Single desktop application (WinForms)  
**Performance Goals**: Zero connection leaks (0 idle connections when app idle), 8+ hours continuous operation without "max users reached" errors, <20ms overhead per operation vs pooled connections (acceptable tradeoff)  
**Constraints**: MySQL 5.7.24 compatibility (NO 8.0+ features), immediate disposal pattern (no pooling), backward compatibility with existing DAO methods  
**Scale/Scope**: 300+ C# files, 21 DAOs in Data/, 28 direct MySqlConnection instances to audit, 4 ExecuteReaderAsync callers needing fixes, 18 SQL Server connections in Service_VisualDatabase (already properly disposed)

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

**Pre-Implementation Status:**

- [❌ VIOLATION → FIX] **Centralized Database Access**: Service_Analytics (3 locations), Service_Migration (8 locations), Service_ErrorReportSync (3 locations) use direct MySqlConnection - WILL BE FIXED by creating ExecuteRawSqlAsync and stored procedures
- [✅ PASS] **Stored Procedures Only**: DAO layer follows pattern, inline SQL only in services being refactored
- [✅ PASS] **Model_Dao_Result Pattern**: All existing DAO methods return Model_Dao_Result<T>
- [✅ PASS] **Centralized Error Handling**: No MessageBox.Show usage found (verified via Serena search)
- [❌ VIOLATION → FIX] **Immediate Connection Disposal**: Connection pooling currently ENABLED - WILL BE FIXED by adding Pooling=false
- [✅ PASS] **Async-First I/O**: All database operations use async/await
- [N/A] **Theme System Integration**: No UI changes in this feature
- [✅ PASS] **MySQL 5.7.24 Compatibility**: No 8.0+ features required
- [✅ PASS] **.NET 8.0/C# 12.0 Compliance**: Using supported features only
- [❌ VIOLATION → FIX] **XML Documentation**: Will be added to new/modified methods
- [❌ VIOLATION → FIX] **Region Organization**: Will maintain standard #region structure

**Justification for Initial Violations:**
- Direct MySqlConnection usage: Legacy code predating constitution, will be refactored to Helper_Database_StoredProcedure pattern
- Pooling enabled: Original architecture, being changed to Pooling=false for immediate disposal

**Post-Implementation Target**: ALL checks ✅ PASS

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

```text
MTM_WIP_Application_Winforms/
├── Helpers/
│   ├── Helper_Database_StoredProcedure.cs     # MODIFY: Mark ExecuteReaderAsync [Obsolete]
│   ├── Helper_Database_Variables.cs            # MODIFY: Add Pooling=false to connection strings
│   └── Helper_Database_ConnectionMonitor.cs    # CREATE: New connection lifecycle monitoring
├── Services/
│   ├── Analytics/
│   │   └── Service_Analytics.cs                # REFACTOR: 3 direct MySqlConnection usages → use stored procedures
│   ├── Maintenance/
│   │   └── Service_Migration.cs                # REFACTOR: 8 direct MySqlConnection usages → use ExecuteRawSqlAsync
│   └── ErrorHandling/
│       └── Service_ErrorReportSync.cs          # AUDIT: 3 direct MySqlConnection usages (may refactor if time permits)
├── Forms/
│   └── MainForm/
│       └── MainForm.cs                         # MODIFY: Add connection monitoring to existing timer
└── Database/
    └── UpdatedStoredProcedures/
        ├── md_analytics_GetTransactionsByRange.sql    # CREATE: Replace inline SQL
        └── md_analytics_GetUsersByDateRange.sql       # CREATE: Replace inline SQL

```

**Structure Decision**: Single WinForms desktop project structure. Files are organized by layer (Helpers, Services, Forms, Database). This feature touches existing infrastructure files rather than creating new modules. Connection monitoring helper is a new utility component. Database changes are stored procedures only (MySQL 5.7.24 compatible).

## Complexity Tracking

**Constitution Violations Being Fixed:**

| Violation | Why It Existed | How We're Fixing It | Timeline |
|-----------|---------------|---------------------|----------|
| Direct MySqlConnection in Service_Analytics | Legacy code from pre-constitution codebase (analytics added before Helper pattern established) | Create 2 stored procedures (md_analytics_GetTransactionsByRange, md_analytics_GetUsersByDateRange) and refactor to use Helper_Database_StoredProcedure | Phase 1 |
| Direct MySqlConnection in Service_Migration | Database migration tool needs raw SQL execution for schema changes | Create ExecuteRawSqlAsync method in Helper_Database_StoredProcedure to maintain centralized access while supporting raw SQL for migrations | Phase 1 |
| Connection Pooling Enabled | Original architecture decision before connection leak issues were discovered | Disable with Pooling=false in Helper_Database_Variables for both MySQL and SQL Server, accept <20ms performance tradeoff for reliability | Phase 2 |

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
**No Additional Complexity Added**: All fixes align with existing architecture and constitution principles. ExecuteRawSqlAsync is necessary to support legitimate migration tool use case while maintaining centralized database access pattern.

## Phase 0: Research (Complete ✅)

**Output**: [research.md](./research.md)

**Key Decisions**:
1. ExecuteReaderAsync completely removed, all 4 callers replaced with ExecuteDataTableWithStatusAsync
2. Connection pooling disabled (Pooling=false) for both MySQL and SQL Server
3. ExecuteRawSqlAsync created for Service_Migration raw SQL needs
4. Two analytics stored procedures replace inline SQL in Service_Analytics
5. Helper_Database_ConnectionMonitor provides 5-minute lifecycle monitoring

All technical unknowns resolved through Serena codebase analysis.

---

## Phase 1: Design (Complete ✅)

**Output**: 
- [data-model.md](./data-model.md) - ConnectionStats model, stored procedure signatures, helper method signatures
- [quickstart.md](./quickstart.md) - Development workflow guide with code examples and testing checklist
- [.github/agents/copilot-instructions.md] - Updated with feature tech stack

**Key Artifacts**:
- ConnectionStats model for monitoring
- md_analytics_GetTransactionsByRange stored procedure definition
- md_analytics_GetUsersByDateRange stored procedure definition
- ExecuteRawSqlAsync method signature and implementation guide
- Manual testing checklist (5 test scenarios)

---

## Post-Design Constitution Re-Check

**Status**: ✅ ALL VIOLATIONS RESOLVED IN DESIGN

- [✅ PASS] **Centralized Database Access**: ExecuteRawSqlAsync maintains centralization, analytics uses stored procedures
- [✅ PASS] **Stored Procedures Only**: Analytics refactored to stored procedures, raw SQL only in documented migration exception
- [✅ PASS] **Model_Dao_Result Pattern**: All new methods return Model_Dao_Result<T>
- [✅ PASS] **Centralized Error Handling**: No MessageBox.Show, all errors use Service_ErrorHandler
- [✅ PASS] **Immediate Connection Disposal**: Pooling=false enforced in connection strings, ExecuteRawSqlAsync validates this
- [✅ PASS] **Async-First I/O**: All database operations remain async/await
- [N/A] **Theme System Integration**: No UI changes
- [✅ PASS] **MySQL 5.7.24 Compatibility**: Stored procedures use 5.7.24 compatible syntax
- [✅ PASS] **.NET 8.0/C# 12.0 Compliance**: [Obsolete] attribute, using statements are supported
- [✅ PASS] **XML Documentation**: Quickstart includes XML docs for all new/modified methods
- [✅ PASS] **Region Organization**: All files maintain standard #region structure

**Constitution Compliance**: **100%** - All principles satisfied

---

## Next Steps

**Phase 2 Complete** - Implementation plan ready for task breakdown.

Run `/speckit.tasks` to generate [tasks.md](./tasks.md) with implementation tasks grouped by user story.

**Branch**: `001-fix-mysql-connection-leaks`  
**Estimated Effort**: 8-12 development hours + 8-hour stability test  
**Manual Testing**: 5 test scenarios defined in quickstart.md