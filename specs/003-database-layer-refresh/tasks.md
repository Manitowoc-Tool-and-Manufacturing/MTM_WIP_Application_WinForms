# Task Breakdown: Database Layer Standardization Refresh

**Feature**: Comprehensive Database Layer Standardization  
**Spec Reference**: `specs/003-database-layer-refresh/spec.md`  
**Plan Reference**: `specs/003-database-layer-refresh/plan.md`  
**Created**: 2025-10-15

---

## Overview

This tasks.md file provides organizational structure and checklist references for Phase 2.5 stored procedure standardization work. The detailed task list resides in `specs/002-comprehensive-database-layer/tasks.md` as Phase 2.5 (T100-T132).

**Cross-Reference**: See `specs/002-comprehensive-database-layer/tasks.md` lines 850-1150 for complete Phase 2.5 task descriptions.

---

## Task Organization

Phase 2.5 consists of 41 tasks organized into 6 parts (updated with Session 3 changes):

- **Part A: Discovery and Analysis** (T100-T106, T106a) - 8 tasks, 20-24 hours (~2.5-3 days)
  - **New**: T106a - CSV Review and Correction (1-2 days, gates T113)
- **Part B: Test Implementation** (T107-T112) - 6 tasks, 58 hours (~7 days)
  - **Updated**: T107 +1h (verbose helper), T108-T110 verbose requirements
- **Part C: Stored Procedure Refactoring** (T113-T118, T113c, T113d) - 8 tasks, 111 hours (~14 days)
  - **New**: T113c - Developer Role Infrastructure (4 hours)
  - **New**: T113d - Parameter Prefix Maintenance Form (8 hours)
  - **Updated**: T113-T116 +20% duration (concurrent documentation)
- **Part D: Database Deployment** (T119-T121, T119b/c/d/e) - 7 tasks, 19-21 hours (~2.5-3 days)
  - **New**: T119b - Re-audit for Schema Drift (0.25 days)
  - **New**: T119c - Refactor Category A Hotfixes (0.25-0.5 days)
  - **New**: T119d - Merge Category B Conflicts (0.25-0.5 days)
  - **New**: T119e - Refactor Category C New Procedures (0.25-0.5 days)
- **Part E: End-to-End Integration Testing** (T122-T128, T124a) - 8 tasks, 28-29 hours (~3.5-4 days)
  - **New**: T124a - Develop Roslyn Analyzer (2-3 hours)
  - **Updated**: T123 +1h (retry strategy testing), T124 (analyzer execution)
- **Part F: Documentation and Knowledge Transfer** (T129-T132) - 4 tasks, 15 hours (~2 days)
  - **Updated**: T129 (matrix generation), T130 (bulk updates), T131 (validation), T132 (appendices)

**Total Estimated Effort**: 251-263 hours (19-30 days single developer, 10-15 days with 3 developers parallel)

**Major Changes from Session 3**:
- Added 8 new tasks: T106a, T113c, T113d, T119b/c/d/e, T124a
- Modified 12 existing tasks: T103, T107-T111, T113-T116, T120, T123-T124, T129-T132
- Increased total duration by 22-34 hours (approximately 3-5 days single developer)

---

## Checklist References

For intensive task phases requiring detailed validation, refer to these requirement quality checklists:

1. **Discovery Quality** (Part A): `checklists/discovery-quality.md`
   - Validates: Completeness of procedure inventory, schema extraction accuracy, compliance audit thoroughness, CSV transaction analysis quality
   - Applies to: T100-T106, T106a

2. **CSV Transaction Analysis Quality** (Part A): `checklists/csv-transaction-analysis-quality.md` **[NEW]**
   - Validates: Pattern detection accuracy, recommendation quality, Git workflow compliance, developer corrections, implementation integration
   - Applies to: T103, T106a

3. **Testing Quality** (Part B): `checklists/testing-quality.md`
   - Validates: Test coverage completeness, test pattern consistency, isolation validation, verbose failure diagnostics
   - Applies to: T107-T112

4. **Verbose Test Failure Quality** (Part B): `checklists/verbose-test-failure-quality.md` **[NEW]**
   - Validates: AssertProcedureResult helper completeness, 7-field diagnostic capture, JSON format, database state capture, developer troubleshooting experience
   - Applies to: T107-T111

5. **Refactoring Quality** (Part C): `checklists/refactoring-quality.md`
   - Validates: Standards compliance, parameter naming consistency, error handling completeness, concurrent documentation tracking
   - Applies to: T113-T118

6. **Developer Role Quality** (Part C): `checklists/developer-role-quality.md` **[NEW]**
   - Validates: Database schema correctness, user management UI integration, role hierarchy enforcement, security testing, audit trail completeness
   - Applies to: T113c

7. **Parameter Prefix Maintenance Quality** (Part C): `checklists/parameter-prefix-maintenance-quality.md` **[NEW]**
   - Validates: UserControl functionality, data binding, CRUD operations, role access control, export/import features, cache integration
   - Applies to: T113d

8. **Deployment Quality** (Part D): `checklists/deployment-quality.md`
   - Validates: Safety mechanisms, backup verification, rollback plan completeness, drift reconciliation integration
   - Applies to: T119-T121

9. **Schema Drift Quality** (Part D): `checklists/schema-drift-quality.md` **[NEW]**
   - Validates: Drift detection accuracy, Category A/B/C classification, hotfix preservation, three-way merge correctness, reconciliation report completeness
   - Applies to: T119b/c/d/e

10. **Integration Quality** (Part E): `checklists/integration-quality.md`
    - Validates: End-to-end testing thoroughness, performance validation, transaction rollback verification, Roslyn analyzer enforcement
    - Applies to: T122-T128

11. **Roslyn Analyzer Quality** (Part E): `checklists/roslyn-analyzer-quality.md` **[NEW]**
    - Validates: Diagnostic rule accuracy, code fix provider functionality, severity configuration, IDE integration, CI/CD integration, false positive rate
    - Applies to: T124a, T124

12. **Documentation Quality** (Part F): `checklists/documentation-quality.md`
    - Validates: Template completeness, quickstart usability, XML documentation coverage, Documentation-Update-Matrix completeness
    - Applies to: T129-T132

13. **Documentation Matrix Quality** (Part F): `checklists/documentation-matrix-quality.md` **[NEW]**
    - Validates: Matrix generation accuracy, status tracking functionality, file path link validation, concurrent update workflow, validation script correctness
    - Applies to: T129, T130, T131

**Total Checklists**: 13 (7 original + 7 new from Session 3 changes, 1 combined)  
**Total Checkpoints**: ~295 validation checkpoints across all checklists

---

## Task Dependency Graph

```
T100-T106 (Discovery) ──┐
         ↓              │
      T106a (CSV Review)├──> T107-T112 (Testing) ──┐
         ↓              │    + T107 verbose helper │
         │              │                          │
         │              └──> T113c (Dev Role) ──┐  │
         │                   T113d (Prefix Form)│  │
         │                        ↓             │  │
         └─────────────────> T113-T118 ────────┼──┼──> T119b (Re-audit) ──┐
                             (Refactor +20%)    │  │    T119c (Cat A)     │
                                                │  │    T119d (Cat B)     │
                                                │  │    T119e (Cat C)     │
                                                │  │         ↓             │
                                                │  └──> T119-T121 ─────────┼──> T122-T128 (Integration) ──┐
                                                │       (Deploy)           │    + T124a Roslyn analyzer   │
                                                │                          │    + T123 retry testing      │
                                                └──────────────────────────┘                               │
                                                                                                           ↓
                                                                                                      T129-T132 (Docs)
                                                                                                      + T129 matrix gen
                                                                                                      + T131 validation
                                                                                                      + T132 appendices
```

**Parallelization Opportunities**:
- **Part A + Part B**: Discovery (T100-T106) completes first 2.5-3 days, CSV review (T106a) parallelized across 3 developers (4-6h each), then Part B testing begins while Part C refactoring starts
- **Part B + Part C**: Testing (T107-T112) and Refactoring (T113-T118) can run in parallel after discovery
- **T113c/d Sequential**: Developer role infrastructure (T113c) must complete before parameter prefix maintenance form (T113d) - 12 hours total
- **Part D → Part E → Part F**: Deployment, Integration Testing, and Documentation must be sequential
- **T119b → T119c/d/e**: Re-audit (T119b) must complete before drift reconciliation tasks (T119c/d/e) which can partially parallelize by category

---

## Success Criteria Mapping

Each part maps to specific success criteria from spec.md (updated with Session 3 additions):

| Part | Success Criteria | Measurement |
|------|------------------|-------------|
| Part A (Discovery) | SC-002: 100% Helper Routing Compliance | Static analysis finds 0 direct MySQL API usage |
| Part A (Discovery) | **SC-015: CSV Transaction Analysis Coverage** | 100% procedures analyzed, ≥90% pattern accuracy |
| Part B (Testing) | SC-003: Comprehensive Procedure Testing | 280+ tests, 100% pass rate |
| Part B (Testing) | **SC-011: Verbose Test Failure Output Quality** | All test failures include 7 diagnostic fields in JSON |
| Part C (Refactoring) | SC-001: Zero MySQL Parameter Errors | 0 parameter errors in 30-day period post-deployment |
| Part C (Refactoring) | **SC-012: Developer Role Access Control** | Developer tools gated by role hierarchy |
| Part C (Refactoring) | **SC-013: Documentation Matrix Completeness** | 100% tracking, validated before deployment |
| Part C (Refactoring) | **SC-017: Parameter Prefix Override Persistence** | Overrides survive application restarts, audit trail intact |
| Part D (Deployment) | SC-010: Sub-3-Second Startup Validation | Startup validates database in <3 seconds |
| Part D (Deployment) | **SC-014: Schema Drift Detection Accuracy** | Categorization validated by DBA, 100% reconciled |
| Part D (Deployment) | **SC-018: Startup Retry Strategy Behavior** | 3 attempts, clean termination, no fallback |
| Part E (Integration) | SC-004: Performance Baseline Maintained | ±5% variance from baseline |
| Part E (Integration) | SC-005: Connection Pool Health | No timeout errors under 100 concurrent operations |
| Part E (Integration) | SC-009: Zero Orphaned Records | Complete rollback in multi-step operations |
| Part E (Integration) | **SC-016: Roslyn Analyzer Enforcement** | Zero violations in production code, zero false positives |
| Part F (Documentation) | SC-007: Developer Productivity | New operation implemented in <15 minutes |

**New Success Criteria (Session 3)**: SC-011 (Verbose tests), SC-012 (Developer role), SC-013 (Documentation matrix), SC-014 (Drift detection), SC-015 (CSV analysis), SC-016 (Roslyn analyzer), SC-017 (Prefix override), SC-018 (Retry strategy)

---

## Task Status Tracking

Track Phase 2.5 progress using this table (update as tasks complete):

| Task ID | Description | Status | Owner | Completed Date | Notes |
|---------|-------------|--------|-------|----------------|-------|
| T100 | Discover all SP call sites | Not Started | - | - | - |
| T101 | Extract complete database schema | Not Started | - | - | - |
| T102 | Generate individual SQL files | Not Started | - | - | - |
| T103 | Audit procedures + CSV transaction analysis | Not Started | - | - | Updated: +CSV generation |
| T104 | Document parameter prefix conventions | Not Started | - | - | - |
| T105 | Create refactoring priority matrix | Not Started | - | - | - |
| T106 | Generate test coverage matrix | Not Started | - | - | - |
| **T106a** | **CSV Review and Correction** | Not Started | - | - | **NEW: Gates T113** |
| T107 | Create BaseIntegrationTest + verbose helper | Not Started | - | - | Updated: +AssertProcedureResult |
| T108 | Generate inventory procedure tests | Not Started | - | - | Updated: +verbose failures |
| T109 | Generate transaction/user/role tests | Not Started | - | - | Updated: +verbose failures |
| T110 | Generate master data procedure tests | Not Started | - | - | Updated: +verbose failures |
| T111 | Generate logging/quick button tests | Not Started | - | - | Updated: +verbose failures |
| T112 | Validate test isolation | Not Started | - | - | - |
| **T113c** | **Developer Role Infrastructure** | Not Started | - | - | **NEW: 4 hours** |
| **T113d** | **Parameter Prefix Maintenance Form** | Not Started | - | - | **NEW: 8 hours** |
| T113 | Refactor top 20 priority procedures | Not Started | - | - | Updated: +concurrent docs |
| T114 | Refactor remaining inventory procedures | Not Started | - | - | Updated: +concurrent docs |
| T115 | Refactor user/role procedures | Not Started | - | - | Updated: +concurrent docs |
| T116 | Refactor master data procedures | Not Started | - | - | Updated: +concurrent docs |
| T117 | Refactor logging/quick button procedures | Not Started | - | - | - |
| T118 | Add transaction management to multi-step | Not Started | - | - | - |
| T119 | Create deployment script | Not Started | - | - | - |
| **T119b** | **Re-audit Production for Schema Drift** | Not Started | - | - | **NEW: 0.25 days** |
| **T119c** | **Refactor Category A Hotfixes** | Not Started | - | - | **NEW: 0.25-0.5 days** |
| **T119d** | **Merge Category B Conflicts** | Not Started | - | - | **NEW: 0.25-0.5 days** |
| **T119e** | **Refactor Category C New Procedures** | Not Started | - | - | **NEW: 0.25-0.5 days** |
| T120 | Execute test database deployment | Not Started | - | - | Updated: +drift procedures |
| T121 | Execute production deployment | Not Started | - | - | Updated: +drift procedures |
| T122 | Execute all integration tests post-deploy | Not Started | - | - | - |
| T123 | Test parameter prefix cache + retry strategy | Not Started | - | - | Updated: +retry dialog |
| **T124a** | **Develop Roslyn Analyzer** | Not Started | - | - | **NEW: 2-3 hours** |
| T124 | Validate DAO Helper routing compliance | Not Started | - | - | Updated: +analyzer execution |
| T125 | Test error logging recursive prevention | Not Started | - | - | - |
| T126 | Execute manual testing of all forms | Not Started | - | - | - |
| T127 | Validate transaction rollback | Not Started | - | - | - |
| T128 | Performance benchmark comparison | Not Started | - | - | - |
| T129 | Generate Documentation-Update-Matrix.md | Not Started | - | - | Updated: matrix generation |
| T130 | Perform bulk documentation updates | Not Started | - | - | Updated: complete backlog |
| T131 | Validate Documentation-Update-Matrix | Not Started | - | - | Updated: validation script |
| T132 | Create Phase 2.5 implementation guide | Not Started | - | - | Updated: +appendices |

**Task Count**: 41 total (33 original + 8 new)  
**Modified Tasks**: 12 (T103, T107-T111, T113-T116, T120, T123-T124, T129-T132)

---

## Checkpoint Reviews

Schedule these checkpoint reviews during Phase 2.5 execution:

### Checkpoint 1: Discovery Complete (After T100-T106a)
- **Deliverables Review**:
  - callsite-inventory.csv (220+ call sites)
  - database-schema-snapshot.json (complete schema)
  - 70+ .sql files in UpdatedStoredProcedures/
  - compliance-report.csv (40-60% current compliance)
  - **procedure-transaction-analysis.csv** (pattern detection, recommendations, corrections) **[NEW]**
  - parameter-prefix-conventions.md (fallback rules)
  - refactoring-priority.csv (top 20 critical procedures)
  - test-coverage-matrix.csv (30% current coverage)
  - **CSV review PR merged** (developer corrections complete, peer review approved) **[NEW]**
- **Go/No-Go Decision**: Proceed to Part B (Testing) and Part C (Refactoring)
- **Checklists**: Run `checklists/discovery-quality.md` and `checklists/csv-transaction-analysis-quality.md` validation

### Checkpoint 2: Testing & Refactoring Complete (After T107-T118, T113c/d)
- **Deliverables Review**:
  - BaseIntegrationTest.cs (transaction management + **AssertProcedureResult verbose helper**) **[UPDATED]**
  - 280+ test methods across 5 test files (**all using verbose failure diagnostics**) **[UPDATED]**
  - 100% test pass rate (all tests green)
  - 70+ refactored .sql files (100% compliance)
  - Explicit transactions in 10+ multi-step procedures
  - **sys_parameter_prefix_override table** (database schema) **[NEW]**
  - **Control_Settings_ParameterPrefixMaintenance UserControl** (Developer maintenance form) **[NEW]**
  - **Documentation-Update-Matrix.md populated** (concurrent documentation tracking) **[NEW]**
- **Go/No-Go Decision**: Proceed to Part D (Deployment)
- **Checklists**: Run `checklists/testing-quality.md`, `checklists/verbose-test-failure-quality.md`, `checklists/refactoring-quality.md`, `checklists/developer-role-quality.md`, `checklists/parameter-prefix-maintenance-quality.md`

### Checkpoint 3: Test Deployment Complete (After T120, T119b/c/d/e)
- **Deliverables Review**:
  - Deploy-StoredProcedures.ps1 (deployment script)
  - **Schema drift report** (Category A/B/C classification) **[NEW]**
  - **Drift reconciliation complete** (hotfixes preserved, conflicts merged, new procedures refactored) **[NEW]**
  - Test database deployment successful (0 errors, **includes drift procedures**) **[UPDATED]**
  - Integration tests pass on test database (100% pass rate, **verbose diagnostics on any failures**) **[UPDATED]**
  - Manual validation of 5 high-priority procedures
- **Go/No-Go Decision**: Proceed to T121 (Production Deployment) with DBA approval
- **Checklists**: Run `checklists/deployment-quality.md` and `checklists/schema-drift-quality.md` validation

### Checkpoint 4: Production Deployment Complete (After T121)
- **Deliverables Review**:
  - Production database backup created and validated
  - Production deployment successful (0 errors, **drift procedures integrated**) **[UPDATED]**
  - Smoke tests pass (5 critical procedures)
  - No errors in 1-hour monitoring window
  - **Retry strategy validated** (3 attempts, clean termination on failure) **[NEW]**
- **Go/No-Go Decision**: Proceed to Part E (Integration Testing)
- **Checklist**: Run `checklists/deployment-quality.md` production section

### Checkpoint 5: Integration Testing Complete (After T122-T128, T124a)
- **Deliverables Review**:
  - All integration tests pass on production (100% pass rate)
  - Parameter prefix cache validated (<200ms, 100% accuracy, **retry strategy tested**) **[UPDATED]**
  - DAO Helper routing compliance (**0 Roslyn analyzer violations**) **[UPDATED]**
  - **MTM.CodeAnalysis.DatabaseAccess NuGet package** (v1.0.0 warnings deployed) **[NEW]**
  - Error logging recursive prevention validated
  - Manual testing complete (all 25 forms workflows pass)
  - Transaction rollback verified (zero orphaned records)
  - Performance within ±5% of baseline
- **Go/No-Go Decision**: Proceed to Part F (Documentation)
- **Checklists**: Run `checklists/integration-quality.md` and `checklists/roslyn-analyzer-quality.md` validation

### Checkpoint 6: Phase 2.5 Complete (After T129-T132)
- **Deliverables Review**:
  - **Documentation-Update-Matrix.md 100% complete** (validation script exit code 0) **[UPDATED]**
  - 00_STATUS_CODE_STANDARDS.md updated with lessons learned
  - quickstart.md created (developer guide)
  - All DAO XML documentation complete (150+ methods)
  - Phase 2.5 implementation guide generated (**includes drift reconciliation, CSV analysis, Roslyn integration appendices**) **[UPDATED]**
- **Sign-Off**: Phase 2.5 complete, Phases 3-8 unblocked
- **Checklists**: Run `checklists/documentation-quality.md` and `checklists/documentation-matrix-quality.md` validation

---

## Risk Mitigation Tracking

Monitor these high-risk areas during Phase 2.5 execution:

| Risk | Mitigation | Status | Owner |
|------|------------|--------|-------|
| Accidental production database wipe | Environment checks, 10-sec confirmation, backup | Not Started | - |
| Parameter prefix detection failure | Fallback to convention, startup logging | Not Started | - |
| Async migration breaking UI marshaling | Code review checklist, T123 validation | Not Started | - |
| Transaction rollback not covering all steps | T118 explicit transactions, T127 validation | Not Started | - |
| Performance regression | SC-004 ±5% tolerance, T128 benchmark | Not Started | - |
| Integration test flakiness | Per-test transactions, T112 isolation validation | Not Started | - |

---

## Communication Plan

### Daily Standups (During Active Work)
- **Frequency**: Daily 15-minute standup
- **Attendees**: Dev team, DBA, QA lead
- **Topics**: Progress on current tasks, blockers, checkpoint readiness

### Weekly Status Reports
- **Frequency**: Every Friday
- **Audience**: Project stakeholders, management
- **Content**: Tasks completed, upcoming checkpoint, risk status, timeline adjustments

### Checkpoint Notifications
- **Trigger**: Each checkpoint review complete
- **Audience**: All stakeholders
- **Content**: Go/no-go decision, deliverables status, next phase readiness

### Production Deployment Notification
- **Timeline**: 24 hours before T121
- **Audience**: All users, support staff, management
- **Content**: Maintenance window details, expected downtime (30 minutes), contact info

### Post-Deployment Monitoring
- **Frequency**: Hourly for first 8 hours, daily for first week
- **Audience**: Dev team, DBA, support staff
- **Content**: Error log summary, performance metrics, user-reported issues

---

## Lessons Learned (Update Post-Completion)

*To be populated during Phase 2.5 execution and T132 completion report creation.*

**Discovery Phase Lessons**:
- (Pending execution)

**Testing Phase Lessons**:
- (Pending execution)

**Refactoring Phase Lessons**:
- (Pending execution)

**Deployment Phase Lessons**:
- (Pending execution)

**Integration Phase Lessons**:
- (Pending execution)

**Documentation Phase Lessons**:
- (Pending execution)

---

**Document Version**: 1.0  
**Last Updated**: 2025-10-15  
**Next Review**: After each checkpoint review
