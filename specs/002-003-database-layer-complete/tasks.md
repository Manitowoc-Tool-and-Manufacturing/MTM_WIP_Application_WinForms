# Task Breakdown: Comprehensive Database Layer Standardization

**Branch**: `002-003-database-layer-complete`
**Spec**: [spec.md](./spec.md)
**Plan**: [plan.md](./plan.md)
**Created**: 2025-10-17

---

## Overview

This document consolidates the task inventory for Phase 2.5 (stored procedure refresh) and the downstream DAO refactor work (Phases 3–8). Tasks retain their original identifiers (T100–T132, etc.) to align with prior documentation while providing a single progress tracker for the combined branch.

---

## Phase 2.5 – Stored Procedure Standardization (Blocking)

### Part A – Discovery & Analysis
- [X] **T100** – Discover all stored procedure call sites
- [X] **T101** – Extract complete database schema snapshot
- [X] **T102** – Generate individual SQL files for each stored procedure
- [X] **T103** – Audit procedures + generate transaction analysis CSV
- [X] **T104** – Document parameter prefix conventions
- [X] **T105** – Build refactoring priority matrix
- [X] **T106** – Produce stored procedure test coverage matrix
- [x] **T106a** – *(Agent-run)* Review and correct transaction analysis CSV (gates refactoring) using provided discovery artifacts
- [x] **T106b** – *(Agent-run)* Complete stored procedure user validation checklist (75 procedures) with ReadyForVerification SQL set and supporting reports

### Part B – Test Implementation
- [X] **T107** – Create BaseIntegrationTest with verbose diagnostics
- [X] **T108** – Author inventory procedure integration tests
  - **2025-10-17 Progress**: Discovered 19 missing stored procedures via codebase scan. Created 15 new procedures in ReadyForVerification: `inv_inventory_Get_All`, `inv_inventory_GetNextBatchNumber`, `inv_transactions_Search` (complex 17-param search), `md_item_types_Exists_ByType`, `md_item_types_GetDistinct`, `md_locations_Exists_ByLocation`, `md_operation_numbers_Exists_ByOperation`, `sys_GetRoleIdByName`, `sys_last_10_transactions_Add_AtPosition`, `sys_last_10_transactions_DeleteAll_ByUser`, `sys_last_10_transactions_Delete_ByUserAndPosition_1`, `usr_ui_settings_Delete_ByUserId`, `usr_ui_settings_GetJsonSetting`, `usr_users_SetUserSetting_ByUserAndField`. Renamed 3 procedure calls in code: `md_part_ids_GetItemType_ByPartID`→`md_part_ids_Get_ByItemNumber`, `sys_last_10_transactions_Move`→`sys_last_10_transactions_Move_1`, `usr_user_roles_GetRoleId_ByUserId`→`sys_user_roles_Get_ById`. See `specs/002-003-database-layer-complete/missing-stored-procedures-action-plan.csv` for details. 
  - **2025-10-18 Completion**: Inventory integration tests complete in `Tests/Integration/Dao_Inventory_Tests.cs` with 14 test methods covering search, add, remove, transfer operations, connection pooling (100 consecutive operations), and transaction rollback validation.
- [X] **T109** – Author transaction/user/role integration tests
  - **2025-10-18**: Created `Dao_Transactions_Tests.cs` with 8 test methods covering search (with pagination, filtering, date ranges), SmartSearch, and analytics operations.
- [X] **T110** – Author master data integration tests  
  - **2025-10-18**: Created `Dao_MasterData_Tests.cs` with 12 test methods covering ItemType (3), Location (3), Operation (3), and Part (3) CRUD validation operations.
- [X] **T111** – Author logging/quick button integration tests
  - **Completed**: 2025-10-18 - Created Dao_Logging_Tests.cs with 11 test methods covering error log query/delete operations and transaction history recording. Created Dao_QuickButtons_Tests.cs with 16 test methods covering CRUD, position management, edge cases, and complete workflow validation. All methods follow discovery-first workflow with verified static patterns and Async suffix conventions.
  - **Reference**: `.github/instructions/integration-testing.instructions.md` - Follow discovery-first workflow (grep_search → verify signatures → write tests). Check static vs instance patterns in Dao_ErrorLog, Dao_History, Dao_QuickButtons. Verify method names (some DAOs omit Async suffix). Include null safety checks for all DaoResult.Data access.
- [X] **T112** – Validate test isolation (sequential vs parallel)
  - **Completed**: 2025-10-18 - Created comprehensive test isolation validation report analyzing all 5 integration test classes (61 tests total). Validated transaction-based isolation via BaseIntegrationTest, GUID-based unique identifiers, and proper test data management. No isolation issues detected. Documented parallelization recommendations: read-only tests (Transactions, MasterData) can run parallel, write-heavy tests (Inventory, Logging, QuickButtons) should run sequentially, connection pool stress test must run alone. Report saved to specs/002-003-database-layer-complete/test-isolation-validation.md.
  - **Reference**: `.github/instructions/integration-testing.instructions.md` - Review test data management patterns and isolation strategies. Ensure tests use unique GUIDs for write operations and don't depend on execution order.

### Part C – Refactoring & Tooling
- [ ] **T113c** – Implement Developer role & prefix override table
  - **Reference**: `002-003-001-developer-tools-suite/tasks.md` Phase 2 (T006-T015) - Complete foundational infrastructure
  - **Sub-feature**: Developer Tools Suite Integration - Parameter Prefix Maintenance foundation
  - **Estimated**: Handled by sub-feature tasks (Phase 1-2, ~10 hours)
- [ ] **T113d** – Build parameter prefix maintenance form (Developer tools)
  - **Reference**: `002-003-001-developer-tools-suite/tasks.md` Phase 4 (T023-T035) - Full CRUD interface implementation
  - **Sub-feature**: Developer Tools Suite Integration - User Story 2 (Parameter Prefix Maintenance)
  - **Estimated**: Handled by sub-feature tasks (Phase 4, ~12 hours)
- [ ] **T113** – Refactor top priority procedures (with documentation matrix updates)
  - **Reference**: `.github/instructions/mysql-database.instructions.md` - Follow stored procedure standards (p_Status, p_ErrorMsg outputs, p_ parameter prefix). Use `.github/instructions/integration-testing.instructions.md` to verify tests after refactoring.
- [ ] **T114** – Refactor remaining inventory procedures
  - **Reference**: `.github/instructions/mysql-database.instructions.md` + `.github/instructions/integration-testing.instructions.md` - Update stored procedures, then run Dao_Inventory_Tests to validate changes.
- [ ] **T115** – Refactor user/role procedures
  - **Reference**: `.github/instructions/mysql-database.instructions.md` + `.github/instructions/integration-testing.instructions.md` - Standardize user/role stored procedures, validate with integration tests.
- [ ] **T116** – Refactor master data procedures
  - **Reference**: `.github/instructions/mysql-database.instructions.md` + `.github/instructions/integration-testing.instructions.md` - Update ItemType/Location/Operation/Part procedures, run Dao_MasterData_Tests.
- [ ] **T117** – Refactor logging/quick button/system procedures
  - **Reference**: `.github/instructions/mysql-database.instructions.md` + `.github/instructions/integration-testing.instructions.md` - Standardize system procedures, validate with T111 tests.
- [ ] **T118** – Add explicit transaction management to multi-step procedures
  - **Reference**: `.github/instructions/mysql-database.instructions.md` (Transaction Management section) - Use proper BEGIN TRANSACTION/COMMIT/ROLLBACK patterns.

### Part D – Deployment & Drift Reconciliation
- [ ] **T119** – Create deployment script with safety checks
- [ ] **T119b** – Re-audit production for schema drift
- [ ] **T119c** – Refactor Category A hotfix procedures
- [ ] **T119d** – Merge Category B conflict procedures
- [ ] **T119e** – Refactor Category C new procedures
- [ ] **T120** – Deploy to test database and validate
- [ ] **T121** – Deploy to production (post-DBA approval)

### Part E – Integration Validation
- [ ] **T122** – Execute integration suite post-deployment
  - **Reference**: `.github/instructions/integration-testing.instructions.md` - Run all integration tests (Dao_Inventory_Tests, Dao_Transactions_Tests, Dao_MasterData_Tests) against production-like environment. Verify all tests pass.
- [ ] **T123** – Validate startup parameter cache retry strategy
  - **Reference**: `.github/instructions/mysql-database.instructions.md` (Transient Error Retry section) - Test retry logic with simulated database failures.
- [ ] **T124a** – Develop Roslyn analyzer package (v1.0.0)
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Create analyzer to enforce DAO patterns and stored procedure usage.
- [ ] **T124** – Verify helper routing compliance via analyzer
  - **Reference**: `.github/instructions/mysql-database.instructions.md` (Helper_Database_StoredProcedure section) - Ensure all database calls route through helpers.
- [ ] **T125** – Test error logging recursive prevention
  - **Reference**: `.github/instructions/security-best-practices.instructions.md` + `.github/instructions/integration-testing.instructions.md` - Validate error handling doesn't cause recursive loops.
- [ ] **T126** – Manual functional testing of all forms/workflows
  - **Reference**: `.github/instructions/testing-standards.instructions.md` (Manual Validation Approach section) - Execute comprehensive manual test scenarios.
- [ ] **T127** – Validate transaction rollback scenarios
  - **Reference**: `.github/instructions/mysql-database.instructions.md` (Transaction Management section) + `.github/instructions/integration-testing.instructions.md` - Test Dao_Inventory_Tests rollback validation.
- [ ] **T128** – Compare performance benchmarks pre/post refactor
  - **Reference**: `.github/instructions/performance-optimization.instructions.md` - Measure query times, connection pool usage, memory allocation.

### Part F – Documentation & Knowledge Transfer
- [ ] **T129** – Generate Documentation Update Matrix with validation script
- [ ] **T130** – Complete outstanding documentation items
- [ ] **T131** – Validate matrix completeness (100 %)
- [ ] **T132** – Publish Phase 2.5 implementation report (metrics, drift, CSV, analyzer)

---

## Phase 3 – Inventory DAO Refactor & Async Migration
- [ ] **T201** – Refactor `Dao_Inventory` to async DaoResult patterns
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (Data Access & Async section) + `.github/instructions/integration-testing.instructions.md` - Standardize to async DaoResult<T>, validate with Dao_Inventory_Tests.
- [ ] **T202** – Update inventory-related forms/controls to async event handlers
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns section) + `.github/instructions/performance-optimization.instructions.md` - Keep event handlers thin, use async/await, marshal to UI thread.
- [ ] **T203** – Refresh inventory DAO documentation & quickstart examples
  - **Reference**: `.github/instructions/documentation.instructions.md` (XML Documentation Comments section) - Update XML docs and code examples.

## Phase 4 – User, Transaction, and Error Logging DAO Refactor
- [ ] **T301** – Refactor `Dao_User`, `Dao_Transactions`, `Dao_ErrorLog`, `Dao_History`
  - **Reference**: `.github/instructions/integration-testing.instructions.md` - Use discovery-first workflow (grep_search method signatures), validate with Dao_Transactions_Tests and new Dao_User_Tests.
- [ ] **T302** – Migrate dependent forms/services to async patterns
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (Async/Await section) + `.github/instructions/performance-optimization.instructions.md` (Async/Await Patterns) - Convert to async, avoid blocking.
- [ ] **T303** – Ensure analyzer compliance and tests for user/transaction flows
  - **Reference**: `.github/instructions/code-review-standards.instructions.md` (Async/Await section) - Verify no .Result/.Wait() calls, all async methods end with Async.

## Phase 5 – Master Data DAO Refactor
- [ ] **T401** – Refactor `Dao_Part`, `Dao_Location`, `Dao_Operation`, `Dao_ItemType`, `Dao_QuickButtons`
  - **Reference**: `.github/instructions/integration-testing.instructions.md` (Discovery-First Workflow) - Verify method signatures, validate with Dao_MasterData_Tests.
- [ ] **T402** – Update Master Data UI components to async
  - **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` (WinForms Patterns) - Update forms to async event handlers, proper UI marshaling.
- [ ] **T403** – Extend documentation and quickstart for master data scenarios
  - **Reference**: `.github/instructions/documentation.instructions.md` - Complete XML docs, add usage examples.

## Phase 6 – Analyzer Enforcement & Tooling Hardening
- [ ] **T501** – Integrate analyzer into build & CI pipeline
- [ ] **T502** – Resolve warning backlog and promote rules to error severity
- [ ] **T503** – Document suppression guidelines and developer workflow

## Phase 7 – Performance, Regression, and Manual Validation
- [ ] **T601** – Re-run benchmark suite (inventory, user auth, transaction searches)
- [ ] **T602** – Execute comprehensive regression manual testing plan
- [ ] **T603** – Update monitoring dashboards and support runbook

## Phase 8 – Release & Post-Deployment Monitoring
- [ ] **T701** – Coordinate final release window with stakeholders
- [ ] **T702** – Execute release checklist and smoke tests
- [ ] **T703** – Monitor support channels & logs for 30 days; capture success metrics
- [ ] **T704** – Archive documentation and finalize branch hand-off

---

## Checkpoint Reviews

1. **Checkpoint A** – Part A complete (T100–T106b) ✔
2. **Checkpoint B** – Part B & Part C tooling ready (T107–T118) ✔
3. **Checkpoint C** – Test deployment validated (T119–T120) ✔
4. **Checkpoint D** – Production deployment validated (T121) ✔
5. **Checkpoint E** – Integration & performance validation (T122–T128) ✔
6. **Checkpoint F** – Documentation & report complete (T129–T132) ✔
7. **Checkpoint G** – DAO refactor phases (T201–T704) ✔

Each checkpoint requires peer review sign-off before moving forward.

---

## Progress Tracking Guidance

- Update this file at the end of each working session with status indicators (e.g., ✅/🔄/⬜) and dates.
- Reference Documentation Update Matrix for concurrent documentation status.
- Use checklist statuses to ensure quality gates are met prior to each checkpoint.
