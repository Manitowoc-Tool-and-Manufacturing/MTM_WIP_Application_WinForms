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
- [ ] **T108** – Author inventory procedure integration tests
- [ ] **T109** – Author transaction/user/role integration tests
- [ ] **T110** – Author master data integration tests
- [ ] **T111** – Author logging/quick button integration tests
- [ ] **T112** – Validate test isolation (sequential vs parallel)

### Part C – Refactoring & Tooling
- [ ] **T113c** – Implement Developer role & prefix override table
- [ ] **T113d** – Build parameter prefix maintenance form (Developer tools)
- [ ] **T113** – Refactor top priority procedures (with documentation matrix updates)
- [ ] **T114** – Refactor remaining inventory procedures
- [ ] **T115** – Refactor user/role procedures
- [ ] **T116** – Refactor master data procedures
- [ ] **T117** – Refactor logging/quick button/system procedures
- [ ] **T118** – Add explicit transaction management to multi-step procedures

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
- [ ] **T123** – Validate startup parameter cache retry strategy
- [ ] **T124a** – Develop Roslyn analyzer package (v1.0.0)
- [ ] **T124** – Verify helper routing compliance via analyzer
- [ ] **T125** – Test error logging recursive prevention
- [ ] **T126** – Manual functional testing of all forms/workflows
- [ ] **T127** – Validate transaction rollback scenarios
- [ ] **T128** – Compare performance benchmarks pre/post refactor

### Part F – Documentation & Knowledge Transfer
- [ ] **T129** – Generate Documentation Update Matrix with validation script
- [ ] **T130** – Complete outstanding documentation items
- [ ] **T131** – Validate matrix completeness (100 %)
- [ ] **T132** – Publish Phase 2.5 implementation report (metrics, drift, CSV, analyzer)

---

## Phase 3 – Inventory DAO Refactor & Async Migration
- [ ] **T201** – Refactor `Dao_Inventory` to async DaoResult patterns
- [ ] **T202** – Update inventory-related forms/controls to async event handlers
- [ ] **T203** – Refresh inventory DAO documentation & quickstart examples

## Phase 4 – User, Transaction, and Error Logging DAO Refactor
- [ ] **T301** – Refactor `Dao_User`, `Dao_Transactions`, `Dao_ErrorLog`, `Dao_History`
- [ ] **T302** – Migrate dependent forms/services to async patterns
- [ ] **T303** – Ensure analyzer compliance and tests for user/transaction flows

## Phase 5 – Master Data DAO Refactor
- [ ] **T401** – Refactor `Dao_Part`, `Dao_Location`, `Dao_Operation`, `Dao_ItemType`, `Dao_QuickButtons`
- [ ] **T402** – Update Master Data UI components to async
- [ ] **T403** – Extend documentation and quickstart for master data scenarios

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
