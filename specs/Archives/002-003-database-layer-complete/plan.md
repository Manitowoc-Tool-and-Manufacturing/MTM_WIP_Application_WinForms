# Implementation Plan: Comprehensive Database Layer Standardization

**Branch**: `002-003-database-layer-complete`
**Spec**: [spec.md](./spec.md)
**Last Updated**: 2025-10-17

## Objectives

1. Establish a single authoritative plan combining the phase 1-2 foundation work and the phase 2.5 stored procedure refresh.
2. Deliver uniform stored procedure standards, async-only DAO patterns, comprehensive testing, and deployment safeguards.
3. Provide clear sequencing, dependencies, and risk controls for completing the database layer refactor under one branch.

## High-Level Timeline (Single Developer Estimate)

| Phase | Scope | Est. Effort |
|-------|-------|-------------|
| Foundation Review | Confirm Phase 1-2 artifacts (Model_Dao_Result, helpers, quickstart) | 1 day |
| Phase 2.5 Parts A–F | Discovery through documentation (T100–T132) | 19–30 days |
| Phase 3–8 | DAO refactor & async migration | 20–30 days |
| Validation & Sign-off | Benchmarks, manual testing, post-deploy monitoring | 5 days |

Parallel work by three developers can reduce Phase 2.5 and Phase 3–8 durations to ~15 days each, assuming domain ownership is respected.

## Dependencies & Prerequisites

- Phase 1-2 artifacts (`Models/Model_Dao_Result*.cs`, helper refactor, quickstart) must be confirmed before new work begins.
- Phase 2.5 completion is a hard gate for all downstream DAO changes.
- DBA availability is required for schema drift reconciliation and production deployment windows.
- Developers must have access to the test database `mtm_wip_application_winform_test` and supporting scripts.
- Analyzer package distribution (NuGet feed or source references) must be prepared before Phase 5 enforcement.

## Phase Breakdown

### Foundation Review (Day 0–1)
- Verify Model_Dao_Result classes, helper methods, and parameter cache are in place.
- Ensure quickstart.md and contracts accurately reflect current patterns.
- Confirm no regressions were introduced after Phase 1-2 completion.

### Phase 2.5 – Stored Procedure Standardization (Blocking)

#### Part A – Discovery & Analysis (T100–T106a)
- Inventory every stored procedure call site.
- Extract schema metadata and generate individual SQL files.
- Produce compliance and transaction strategy CSVs.
- Assign the automation agent to review and correct the transaction analysis CSV and to execute the stored procedure validation checklist using the provided artifacts (`ReadyForVerification/`, `STORED_PROCEDURE_CALLSITES.csv`, `sql-operations-detailed.json`, `procedure-base-analysis.csv`, `compliance-report.csv`, `call-hierarchy-complete.json`, `database-schema-snapshot.json`, `PROCEDURE_ANALYSIS_GUIDE.md`) before refactoring begins.

#### Part B – Test Implementation (T107–T112)
- Build BaseIntegrationTest with verbose JSON diagnostics.
- Author ~280 integration tests covering all procedures.
- Validate isolation by running suites sequentially and in parallel.

#### Part C – Stored Procedure Refactoring & Tooling (T113c–T118)
- **Sub-Feature**: Developer Tools Suite Integration (see `002-003-001-developer-tools-suite/`)
  - Implement Developer role, parameter prefix override table and CRUD stored procedures (T113c) → Sub-feature Phase 2
  - Build Parameter Prefix Maintenance UI in Settings → Developer (T113d) → Sub-feature Phase 4 (US2)
  - **Optional**: Debug Dashboard, Schema Inspector, Procedure Call Hierarchy, Code Generator → Sub-feature Phases 3, 5-7
  - **Integration Point**: Complete Phase 1-2 (Foundational) of sub-feature before starting T113. Complete Phase 4 (US2) before marking T113d complete.
- Refactor procedures in priority order, updating documentation concurrently via matrix.
- Add explicit transactions to multi-step operations.

#### Part D – Deployment & Drift Reconciliation (T119–T121, T119b–e)
- Create safe deployment script with backup and wipe safeguards.
- Re-audit production for drift, categorize (A/B/C), and reconcile before test deployment.
- Deploy to test, run full test suite, then deploy to production with DBA approval.

#### Part E – Integration Validation (T122–T128, T124a)
- Re-run test suites on deployed procedures.
- Validate startup retry strategy, helper routing via analyzer, error logging fallback, manual workflows, and performance benchmarks.

#### Part F – Documentation & Knowledge Transfer (T129–T132)
- Generate Documentation Update Matrix, complete outstanding docs, and validate completeness.
- Produce final implementation guide with metrics, drift summary, CSV insights, and analyzer integration details.

### Phase 3 – Inventory DAO Refactor (after Phase 2.5)
- Update Dao_Inventory methods to new helper + Model_Dao_Result patterns.
- Migrate forms/controls using inventory DAOs to async event handlers.
- Confirm tests pass against standardized procedures.

### Phase 4 – User & Transaction DAO Refactor
- Apply patterns to Dao_User, Dao_Transactions, Dao_ErrorLog, and Dao_History.
- Ensure Service_ErrorHandler alignment and analyzer compliance.

### Phase 5 – Master Data DAO Refactor
- Refactor Dao_Part, Dao_Location, Dao_Operation, Dao_ItemType, Dao_QuickButtons.
- Update dependent forms, background services, and shared controls.

### Phase 6 – Analyzer Enforcement
- Integrate Roslyn analyzer into solution.
- Address warning backlog; promote to errors once clean.

### Phase 7 – Performance & Manual Validation
- Re-run benchmarks, regression tests, and manual workflows (login, adjustments, transfers, reports).
- Validate connection pooling metrics under load.
- Document residual risks and monitoring plan.

### Phase 8 – Deployment & Post-Release Monitoring
- Coordinate release of DAO refactor, analyzer enforcement, and documentation updates.
- Monitor logs and support channels for 30 days, confirming SC-001, SC-008, and SC-018 outcomes.

## Work Allocation Strategy

- **Developer 1**: Discovery, deployment scripting, drift reconciliation.
- **Developer 2**: Test infrastructure, analyzer development, integration validation.
- **Developer 3**: Procedure refactoring, documentation matrix maintenance, developer tooling.

Rotating peer reviews are required at the end of each Part.

## Risk Management

| Risk | Phase | Mitigation |
|------|-------|------------|
| CSV review bottleneck | 2.5 Part A | Domain assignment, strict 48-hour turnaround |
| Procedure drift | 2.5 Part D | Mandatory re-audit + reconciliation before deployments |
| Analyzer false positives | Phase 6 | Start with warnings, add suppression guidance, gather feedback |
| Documentation lag | 2.5 Parts C/F | Matrix tracking with daily standup review |
| Production outage | Deployment | Backup/rollback scripts, off-hours window, smoke tests |

## Exit Criteria per Phase

- **Phase 2.5**: All tasks T100–T132 complete, documentation matrix at 100 %, success criteria SC-001 through SC-018 validated.
- **Phase 3–5**: Analyzer reports zero violations, integration tests green, manual workflows pass.
- **Phase 6–7**: Benchmarks within tolerance, monitoring dashboards updated, support runbook revised.
- **Phase 8**: Post-release metrics meet success criteria, documentation archived, stakeholders sign off.

## Deliverables

- Updated stored procedures (`Database/UpdatedStoredProcedures/*`).
- Comprehensive integration tests (`Tests/Integration/*`).
- Roslyn analyzer package (`MTM.CodeAnalysis.DatabaseAccess`).
- Developer maintenance UI (`Control_Settings_ParameterPrefixMaintenance`).
- Documentation Update Matrix and Phase 2.5 completion report.
- Final drift reconciliation and deployment validation reports.
