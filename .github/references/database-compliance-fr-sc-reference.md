# MTM Database Standardization - Functional Requirements & Success Criteria Reference

**Generated**: 2025-10-24  
**Purpose**: Consolidated reference for all database compliance requirements  
**Source**: Extracted from specs/Archives/ directory (103 specification files analyzed)

This file provides a quick reference for all Functional Requirements (FR) and Success Criteria (SC) across the MTM database standardization initiative. Use this instead of reading all spec files individually.

---

## Functional Requirements (FR-001 to FR-029)

### Foundation & Helper Methods

**FR-001** - Standardized Helper Methods  
Provide four standardized helper methods (non-query, DataTable, scalar, custom output) for executing stored procedures. All database operations must route through Helper_Database_StoredProcedure class.

**FR-002** - Parameter Prefix Detection  
Cache stored procedure parameter metadata from INFORMATION_SCHEMA at startup and apply correct prefixes automatically. Developers provide unprefixed PascalCase parameter names in Dictionary<string, object>.

**FR-003** - Model_Dao_Result Pattern  
ALL DAO methods must return Model_Dao_Result or Model_Dao_Result<T> envelopes containing: IsSuccess (bool), Message (string), Data (when applicable), Exception (if error occurred).

**FR-004** - Stored Procedure Output Standards  
Every stored procedure MUST declare OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500) with documented status codes:
- 0 = success
- 1 = not found/no-op  
- -1 = error
- -2 = validation error
- -3 = business rule violation

### Error Handling & Logging

**FR-005** - Comprehensive Error Logging  
Log ALL database errors to log_error table with complete context: User, Severity, ErrorType, ErrorMessage, StackTrace, ModuleName, MethodName, AdditionalInfo, MachineName, OSVersion, AppVersion, ErrorTime.

**FR-006** - Logging Failure Fallback  
Handle logging failures by falling back to file logging without recursion. Prevents infinite loops if log_error table unavailable.

**FR-007** - Error Message Cooldown  
Suppress duplicate error dialogs within configurable cooldown (5 seconds) while still logging all occurrences to prevent error spam.

**FR-008** - Connection Pooling Configuration  
Configure connection pooling with MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s. Expose diagnostics for monitoring pool health.

**FR-009** - Transient Error Retry Logic  
Retry transient MySQL errors (1205 deadlock, 1213 lock timeout, 2006 server gone away, 2013 lost connection) up to 3 times with exponential backoff.

### Async Architecture & Patterns

**FR-010** - Async-Only DAO Architecture  
Enforce async-only DAO architecture with NO synchronous escape hatches. All callers (Forms, Services, Controls) must adopt async/await patterns immediately.

**FR-011** - Transaction Management  
Wrap ALL multi-step operations in explicit database transactions with proper rollback on any step failure. Ensures data integrity for inventory transfers, batch modifications, etc.

**FR-012** - Service_DebugTracer Integration  
Integrate Service_DebugTracer entry/exit tracing for EVERY DAO method. Log sanitized parameters at entry and results at exit for troubleshooting.

**FR-013** - Centralized Connection Strings  
Centralize ALL connection strings in Helper_Database_Variables. NO credentials hardcoded in code files. Use secure configuration for production.

**FR-014** - Startup Database Validation  
Validate database connectivity prior to showing MainForm. Terminate gracefully on startup failure. Show retry dialog during runtime failures.

### Code Structure & Standards

**FR-015** - Standardized DAO File Structure  
Standardize DAO file structure: organized regions, async method signatures, helper routing, parameter matching. Consistent patterns across all 12 DAO classes.

**FR-016** - Eliminate Direct Database Access  
Eliminate ALL direct MySqlConnection/MySqlCommand usage outside helper classes. Enforce via Roslyn analyzer (violations = compilation errors).

**FR-017** - Parameter Naming Alignment  
Align stored procedure parameter names with C# PascalCase properties. Prefix removed in C# code (PartID not p_PartID). Helper applies prefix automatically.

### Testing & Integration

**FR-018** - Integration Test Infrastructure  
Provide integration tests using mtm_wip_application_winform_test database with per-test-class transactions. Fast isolated testing without affecting production data.

**FR-019** - Startup vs Runtime Error Handling  
Distinguish startup vs runtime errors: fatal termination at startup (no database = no app), retry dialog during runtime operation (preserve user work).

**FR-020** - Performance Monitoring  
Record execution times for ALL database operations. Categorize operations (Query 500ms, Modification 1000ms, Batch 5000ms, Report 2000ms). Log threshold violations as warnings.

**FR-021** - Error Severity Classification  
Classify errors into three tiers with documented criteria:
- Critical: Data integrity risk, app cannot continue
- Error: Operation failed, user sees error message
- Warning: Unexpected but handled, no user impact

### Advanced Features & Tooling

**FR-022** - Verbose Test Diagnostics  
Output verbose JSON diagnostics with 7 required fields in integration tests upon failure: TestName, ErrorType, ErrorMessage, StackTrace, DatabaseState, ExpectedOutcome, ActualOutcome.

**FR-023** - Developer Role & Maintenance UI  
Implement Developer role (requires Admin prerequisite), parameter override table, maintenance UI gated by role for advanced troubleshooting.

**FR-024** - Documentation Update Matrix  
Maintain Documentation Update Matrix tracking per-procedure header and DAO XML documentation updates during refactoring. Ensures docs stay current.

**FR-025** - Schema Drift Detection  
Detect schema drift before deployment, categorize differences (A/B/C), reconcile prior to go-live. Handles production hotfixes during development.

**FR-026** - CSV Transaction Analysis  
Generate, review, and apply CSV-based transaction strategy analysis before refactoring stored procedures. Automated pattern detection with human review.

**FR-027** - Roslyn Analyzer Enforcement  
Provide Roslyn analyzer package enforcing: helper routing, status output checks, banning inline SQL. Warnings promoted to errors for strict enforcement.

**FR-028** - Parameter Override Persistence  
Persist parameter prefix overrides with full audit trail in database table. Surface via maintenance UI. Consumed by startup cache for edge case handling.

**FR-029** - Startup Retry Dialog  
Present three-attempt startup retry dialog for parameter cache failures. Terminate cleanly if cache cannot load after retries. No silent failures.

---

## Success Criteria (SC-001 to SC-018)

### Zero Defects & Reliability

**SC-001** - Zero MySQL Parameter Errors  
Zero MySQL parameter prefix errors recorded in logs during first 30 days post-deployment. Validates FR-002 parameter prefix detection.

**SC-002** - 100% Helper Method Usage  
100% of DAO operations in repository reference helper methods (verified via Roslyn analyzer). No direct MySQL API usage allowed.

**SC-003** - Complete Test Coverage  
Integration suite covers EVERY stored procedure with both valid and invalid test cases. 100% pass rate required before deployment.

**SC-004** - Performance Baseline Maintained  
Benchmark results remain within ±5% of pre-refactor execution times for key operations. No performance regression from standardization.

**SC-005** - Connection Pool Health  
Connection pool stays within configured bounds (5-100) under 100 concurrent operations with zero timeout errors.

**SC-006** - No Logging Recursion  
Error logging never enters recursive loops. Fallback file logging verified during testing when database unavailable.

### Developer Experience

**SC-007** - Rapid DAO Development  
Developers implement new DAO method end-to-end in <15 minutes using standard templates and patterns.

**SC-008** - Support Ticket Reduction  
Database-related support tickets drop by ≥90% within one month of release. Measures reliability improvement.

### Data Integrity

**SC-009** - Clean Transaction Rollbacks  
Transaction rollbacks leave zero orphaned records across ALL tested scenarios. Validates FR-011 transaction management.

**SC-010** - Fast Startup Validation  
Startup detects database availability within 3 seconds and provides clear messaging if unavailable.

### Testing & Diagnostics

**SC-011** - Verbose Test Failures  
ALL failing integration tests produce JSON diagnostics with required 7 fields. No incomplete error reports.

**SC-012** - Secure Maintenance Access  
Only Admin+Developer users can access maintenance tooling. Unauthorized access attempts logged and blocked.

### Documentation & Deployment

**SC-013** - Complete Documentation  
Documentation Update Matrix reports 100% completion before sign-off. No undocumented procedures or DAO methods.

**SC-014** - Schema Drift Reconciliation  
Drift report categorizes 100% of production changes. ALL categories reconciled prior to deployment.

**SC-015** - Transaction Analysis Coverage  
CSV transaction analysis covers every procedure with ≥90% pattern detection accuracy confirmed during review.

### Code Quality Enforcement

**SC-016** - Zero Analyzer Violations  
Roslyn analyzer reports zero violations on main branch. Code fixes produce helper-based patterns automatically.

**SC-017** - Persistent Parameter Overrides  
Parameter overrides persist across application restarts with full audit history available for troubleshooting.

**SC-018** - Controlled Startup Failures  
Startup retry dialog executes at most 3 attempts. Application terminates cleanly on repeated failure with clear user messaging.

---

## Quick Reference Matrix

| FR # | Category | SC Validation |
|------|----------|---------------|
| FR-001 to FR-004 | Foundation | SC-001, SC-002 |
| FR-005 to FR-009 | Error Handling | SC-006, SC-010 |
| FR-010 to FR-014 | Async Architecture | SC-007, SC-008 |
| FR-015 to FR-017 | Code Standards | SC-002, SC-016 |
| FR-018 to FR-021 | Testing & Monitoring | SC-003, SC-004, SC-005, SC-011 |
| FR-022 to FR-029 | Advanced Features | SC-012, SC-013, SC-014, SC-015, SC-017, SC-018 |

---

## Usage Notes

1. **All requirements are mandatory** - No requirement is optional. Every FR must be implemented and validated by corresponding SC.

2. **Requirements are cumulative** - Later specs refine earlier ones but do not invalidate them. When conflicts arise, newest spec takes precedence.

3. **Cross-file validation required** - Many violations occur in dependency files (DAOs, Helpers) not the target UI file. Always scan dependencies.

4. **Status codes are standardized** - All stored procedures use: 0=success, 1=not found, -1=error, -2=validation, -3=business rule.

5. **Column naming critical** - DataTable/DataGridView columns NEVER use p_ prefix. Stored procedure parameters DO use p_ prefix.

---

## Source Files Analyzed

This reference was generated by analyzing 103 markdown files across:
- specs/Archives/002-comprehensive-database-layer/ (Original draft - 8+ files)
- specs/Archives/002-003-database-layer-complete/ (Consolidated - 30+ files)
- specs/Archives/003-database-layer-refresh/ (Enhanced - 20+ files)
- specs/Archives/test-fix-workspace/ (Test documentation - 45+ files)

For detailed implementation guidance, clarification questions, and quality checklists, refer to the original spec files.

---

**End of FR/SC Reference**
