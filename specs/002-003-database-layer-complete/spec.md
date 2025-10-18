# Feature Specification: Comprehensive Database Layer Standardization

**Branch**: `002-003-database-layer-complete`
**Created**: 2025-10-17
**Status**: Active

## Overview

The MTM WIP Application depends on a MySQL 5.7 stored-procedure-only database layer that has grown inconsistent across 60+ procedures, 12 DAO classes, and ~220 call sites. Parameter prefix mismatches, mixed async/sync patterns, ad-hoc error handling, and missing documentation create instability, obscure root-cause analysis, and slow onboarding. This feature consolidates the prior "phase 1-2 foundation" and "phase 2.5 stored procedure refresh" initiatives into a single, authoritative specification and introduces unified execution standards for DAO methods, stored procedures, testing, deployment, and documentation.

The effort creates a single source of truth for the combined scope under one spec directory, enabling consistent planning, implementation, validation, and knowledge transfer on the `002-003-database-layer-complete` branch.

## Clarifications

### Session 2025-10-13 (Original Scope)

1. **Async execution mode** – All DAO methods must be async. No synchronous wrapper will be provided; all callers migrate immediately.
2. **Startup failure handling** – Application terminates gracefully if database unavailable at startup. During runtime, show retry prompt and preserve state where possible.
3. **Slow query thresholds** – Category-based thresholds (Query 500 ms, Modification 1000 ms, Report 2000 ms, Batch 5000 ms) stored in configuration.
4. **Transactions** – Every multi-step database operation requires explicit transactions with rollback on failure.
5. **Logging severity** – Three levels: Critical (app/data integrity at risk), Error (operation failure), Warning (unexpected but handled). Criteria documented in logging helpers.
6. **Async migration scope** – No `DaoLegacy` wrapper. All forms, services, and controls migrate to async/await now.
7. **Parameter prefixes** – Query INFORMATION_SCHEMA at startup, cache results, and fall back to conventions (p_ / in_) only if metadata unavailable.
8. **Test database** – Use schema-only copy `mtm_wip_application_winform_test` with per-test-class transactions for isolation.

### Session 2025-10-15 (Refresh & User Feedback)

9. **Stored procedure coverage** – Phase 2.5 is blocking. All procedures audited and refactored before DAO work.
10. **Documentation workflow** – Documentation updated concurrently with refactoring. Tracking via Documentation Update Matrix.
11. **Schema drift** – Allow production hotfixes during phase. Re-audit prior to deployment and reconcile (Category A/B/C) before go-live.
12. **Developer tooling** – Introduce Developer role (Admin prerequisite) + parameter prefix maintenance form for overrides.
13. **CSV transaction analysis** – Automated detection of procedure transaction patterns with human review (T106a) before refactoring.
14. **Verbose integration tests** – Base test helper outputs structured JSON diagnostics (seven fields) on failure.
15. **Roslyn enforcement** – Custom analyzer (warnings → errors) ensures DAO code routes through helpers and checks status outputs.
16. **Startup retry** – Replace convention fallback with three-attempt retry dialog; terminate cleanly if cache cannot load.
17. **Parameter overrides** – Persist overrides in database table with audit trail, consumable by maintenance UI and startup cache.

## Assumptions

- Existing stored procedures reflect authoritative business logic; refactoring preserves behavior while repairing interfaces.
- Developers have local MySQL instances with both production and test schemas available.
- DBA support is available for schema drift reconciliation and production deployments.
- Existing logging, progress helpers, and configuration infrastructure remain in place and reusable.
- No additional UI/UX changes beyond developer-facing tooling are required.

## Scope

### In Scope

- Standardization of all stored procedures, including status/error outputs and parameter prefixes.
- Refactoring of all DAO classes to async-only DaoResult patterns.
- Migration of all call sites (forms, controls, services) to async/await.
- Comprehensive integration test suite covering every stored procedure.
- Performance benchmarking, error logging enhancements, and retry policies.
- Documentation unification (single spec directory, matrices, completion report).
- Developer role, maintenance UI, and Roslyn analyzer for ongoing compliance.

### Out of Scope

- Introducing new business workflows or stored procedures beyond standardization needs.
- Decommissioning or redesigning existing UI beyond necessary async changes.
- Replacing MySQL 5.7 or altering stored procedure business logic semantics.
- Automated deployment tooling beyond specified scripts and documentation.

## User Scenarios & Testing

### User Story 1 – Developer Adds New Database Operation (Priority P1)

**Scenario**: Developer creates `Dao_Inventory.AddCycleCountAsync`, calls stored procedure `inv_inventory_AddCycleCount`, and relies on helper patterns.
- Valid data returns `DaoResult.Success` with confirmation message.
- Invalid parameters produce `DaoResult.Failure` with actionable message and log entry.
- Forced connection loss surfaces retry prompt without crashing and logs severity "Error".

### User Story 2 – Application Executes Reliable Database Operations (Priority P1)

**Scenario**: Operations team processes 100 inventory adjustments sequentially.
- All succeed with consistent status handling and no parameter errors.
- Mid-run database outage surfaces retry dialog and maintains UI responsiveness.
- Malformed parameters are caught before hitting MySQL, returning validation failures.
- Connection pool metrics stay between configured min/max (5–100 connections).

### User Story 3 – Developer Troubleshoots Database Issues (Priority P2)

**Scenario**: Debugging an intermit tent transaction failure in production.
- Error entries contain user, severity, method, and serialized parameters.
- Service_DebugTracer entry/exit logs expose full call chains for comparison.
- Error spam suppressed via cooldown (single dialog per 5 s window) while logs capture all instances.

### User Story 4 – Database Administrator Maintains Consistent Schema (Priority P2)

**Scenario**: DBA audits procedures after a production hotfix.
- INFORMATION_SCHEMA cache confirms consistent prefixes and OUT parameters.
- Drift report categorizes the hotfix, and reconciliation integrates it before release.
- Validation script reports zero inconsistencies before deployment.

### User Story 5 – Performance Analyst Reviews Query Execution (Priority P3)

**Scenario**: Analyst runs 100 concurrent inventory searches returning 10,000+ rows.
- All operations log execution times; thresholds flag slow queries as warnings.
- Transaction batches rollback fully when step fails (verified via tests).
- Benchmark suite confirms ±5 % variance relative to baseline.

## Functional Requirements

1. **FR-001** – Provide four standardized helper methods (non-query, DataTable, scalar, custom output) for executing stored procedures.
2. **FR-002** – Cache stored procedure parameter metadata from INFORMATION_SCHEMA at startup and apply correct prefixes without requiring prefixes in C# dictionaries.
3. **FR-003** – DAO methods must return `DaoResult`/`DaoResult<T>` envelopes with message, data (when applicable), and optional exception.
4. **FR-004** – Every stored procedure must expose `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)` with documented status semantics.
5. **FR-005** – Log database errors with complete context (user, severity, type, message, stack, module, method, additional info, host, OS, app version, timestamp).
6. **FR-006** – Handle logging failures by falling back to file logging without recursion.
7. **FR-007** – Suppress duplicate error dialogs within a configurable cooldown while logging all occurrences.
8. **FR-008** – Configure connection pooling (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30 s) and expose diagnostics.
9. **FR-009** – Retry transient MySQL errors (1205, 1213, 2006, 2013) up to three times with exponential backoff.
10. **FR-010** – Enforce async-only DAO architecture with no synchronous escape hatches; all callers adopt async/await.
11. **FR-011** – Wrap multi-step operations in explicit transactions and roll back on any failure.
12. **FR-012** – Integrate Service_DebugTracer entry/exit tracing for every DAO method.
13. **FR-013** – Centralize connection strings in `Helper_Database_Variables`; no credentials in code.
14. **FR-014** – Validate database connectivity prior to showing MainForm; terminate gracefully on failure.
15. **FR-015** – Standardize DAO file structure (regions, async methods, helper routing, parameter matching).
16. **FR-016** – Eliminate direct MySqlConnection/MySqlCommand usage outside helper classes; enforce via analyzer.
17. **FR-017** – Align stored procedure parameter names with C# PascalCase properties (prefix removed).
18. **FR-018** – Provide integration tests using `mtm_wip_application_winform_test` with per-test-class transactions.
19. **FR-019** – Distinguish startup vs runtime errors: fatal at startup, retry dialog during operation.
20. **FR-020** – Record execution times, categorize operations, and log threshold violations as warnings.
21. **FR-021** – Classify errors into Critical, Error, and Warning with documented criteria.
22. **FR-022** – Output verbose JSON diagnostics (seven fields) in integration tests upon failure.
23. **FR-023** – Implement Developer role (Admin prerequisite), parameter override table, and maintenance UI gated by role.
24. **FR-024** – Maintain Documentation Update Matrix tracking per-procedure header and DAO XML updates during refactoring.
25. **FR-025** – Detect schema drift before deployment, categorize differences, and reconcile prior to go-live.
26. **FR-026** – Generate, review, and apply CSV-based transaction strategy analysis before refactoring.
27. **FR-027** – Provide Roslyn analyzer package enforcing helper routing, status checks, and banning inline SQL.
28. **FR-028** – Persist parameter prefix overrides with audit trail and surface via maintenance UI and startup cache.
29. **FR-029** – Present three-attempt startup retry dialog for parameter cache failures; terminate if cache cannot load.

## Success Criteria

1. **SC-001** – Zero MySQL parameter prefix errors recorded in logs during the first 30 days post-deployment.
2. **SC-002** – 100 % of DAO operations in repository reference helper methods (verified via analyzer).
3. **SC-003** – Integration suite covers every stored procedure with valid and invalid cases; 100 % pass rate.
4. **SC-004** – Benchmark results remain within ±5 % of pre-refactor execution times for key operations.
5. **SC-005** – Connection pool stays within configured bounds under 100 concurrent operations with zero timeouts.
6. **SC-006** – Error logging never enters recursive loops; fallback logging verified during testing.
7. **SC-007** – Developers implement a new DAO method end-to-end in <15 minutes using templates.
8. **SC-008** – Database-related support tickets drop by ≥90 % within one month of release.
9. **SC-009** – Transaction rollbacks leave zero orphaned records across tested scenarios.
10. **SC-010** – Startup detects database availability within 3 seconds and provides clear messaging if unavailable.
11. **SC-011** – All failing integration tests produce JSON diagnostics with the required seven fields.
12. **SC-012** – Only Admin+Developer users can access maintenance tooling; unauthorized access attempts logged and blocked.
13. **SC-013** – Documentation Update Matrix reports 100 % completion before sign-off.
14. **SC-014** – Drift report categorizes 100 % of production changes and all categories reconciled prior to deployment.
15. **SC-015** – CSV transaction analysis covers every procedure with ≥90 % pattern detection accuracy confirmed during review.
16. **SC-016** – Roslyn analyzer reports zero violations on main branch; code fixes produce helper-based patterns.
17. **SC-017** – Parameter overrides persist across restarts with full audit history available.
18. **SC-018** – Startup retry dialog executes at most three attempts and terminates cleanly on repeated failure.

## Key Entities

- **DaoResult / DaoResult<T>** – Standard result envelopes capturing success flag, message, optional data, and exception.
- **Helper_Database_StoredProcedure** – Central execution utility providing standardized stored procedure interactions.
- **ParameterPrefixCache** – Startup-loaded dictionary mapping stored procedure parameters to detected prefixes and modes.
- **DAO Classes** – `Dao_Inventory`, `Dao_User`, `Dao_Transactions`, `Dao_Part`, `Dao_Location`, `Dao_Operation`, `Dao_ItemType`, `Dao_QuickButtons`, `Dao_History`, `Dao_ErrorLog`, `Dao_System`.
- **Stored Procedures** – Inventory, transaction, system, and metadata procedures, all returning status/error outputs.
- **Error Log Entry** – Structured record persisted to `log_error` with full context for diagnostics.
- **Transaction History** – Records capturing transaction type, locations, quantity, user, and timestamp for audit.
- **Documentation Update Matrix** – Markdown tracker linking procedures, DAO docs, standards updates, and completion status.
- **Schema Drift Report** – Categorized diff (A/B/C) between baseline and production stored procedures.

## Edge Cases & Exception Scenarios

- **Unexpected status codes** – Treat any status outside documented range as error, log warning, and return failure.
- **Startup metadata failure** – Retry three times with delay; terminate with guidance if cache still fails to load.
- **Logging failure** – File fallback captures errors when database logging unavailable, preventing recursion.
- **Mixed prefix procedures** – Cache detects per-parameter prefixes; overrides available for anomalies.
- **Connection pool exhaustion** – After timeout, surface user-friendly message and encourage retry while logging warning.
- **Transaction deadlocks** – Retry up to three times with exponential backoff before failing gracefully.
- **Schema drift during implementation** – Re-audit before deployment; no production deployment until drift reconciled.
- **High-volume CSV review** – Domain-based assignments and PR workflow prevent bottlenecks and ensure coverage.

## Dependencies

- MySQL 5.7.24+, MySql.Data 8.x connector.
- Existing logging (LoggingUtility, Service_DebugTracer) and progress helpers.
- DBA availability for drift reconciliation and production deployment windows.
- PowerShell scripts for deployment and reporting (executed by maintainers, not by this spec).
- Development tooling support for Roslyn analyzer distribution.

## Out of Scope Risks & Mitigations

| Risk | Mitigation |
|------|------------|
| Long-running async migration blocking UI work | Stage migrations by module with interim QA checkpoints |
| Analyzer false positives slowing development | Start with warnings (v1.0.0), collect feedback, promote to errors once stable |
| Drift reconciliation delays deployment | Schedule re-audit early, allocate buffer for merge conflict resolution |
| Documentation backlog | Enforce concurrent updates via matrix and validation scripts |
| Production outage during deployment | Pre-deployment backup + rollback script + off-hours window |

## References

- Existing specs: `specs/002-comprehensive-database-layer/spec.md`, `specs/003-database-layer-refresh/spec.md`
- Contracts: `specs/002-comprehensive-database-layer/contracts/*.json`
- Checklists: `specs/003-database-layer-refresh/checklists/*.md`
- Branch PR: https://github.com/Dorotel/MTM_WIP_Application_WinForms/pull/59
