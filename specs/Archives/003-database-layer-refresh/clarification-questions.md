# Clarification Questions: Database Layer Standardization Refresh

**Feature**: Comprehensive Database Layer Standardization  
**Spec Reference**: `specs/003-database-layer-refresh/spec.md`  
**Created**: 2025-10-15  
**Status**: Complete

---

## Session 1: Phase 2.5 Scope Definition (2025-10-15)

### Q1: Database Source - Which database should Phase 2.5 audit?

**Context**: Codebase references stored procedures, but production vs test database may have different procedures.

**Options**:

1. Production database only (`MTM_WIP_Application_Winforms` on 172.16.1.104)
2. Test database only (`mtm_wip_application_winforms_test` on localhost)
3. Both databases (audit both, reconcile differences)
4. Code analysis only (discover procedures from C# call sites)

**User Response**: Production database (`MTM_WIP_Application_Winforms`) as source of truth. Test database should mirror production after deployment. Code analysis (T100) validates call sites match actual procedures.

**Decision Impact**: T101 queries production INFORMATION_SCHEMA, T102 extracts from production, T120 deploys to test first for validation before T121 production deployment.

---

### Q2: Deployment Strategy - Wipe/recreate vs incremental updates?

**Context**: Refactoring 60-100 procedures requires deployment strategy balancing safety vs completeness.

**Options**:

1. **Complete wipe and reinstall**: Drop all procedures, install fresh from UpdatedStoredProcedures/ (destructive but clean)
2. **Incremental ALTER**: Update only modified procedures, leave unchanged procedures intact (safer but complex)
3. **Blue/green deployment**: Create parallel schema with new procedures, cutover at once (zero downtime but double storage)
4. **Gradual rollout**: Deploy procedures in batches over multiple weeks (lowest risk but longest timeline)

**User Response**: Complete wipe and reinstall with safety mechanisms (backup, environment checks, test-first deployment). Accepts higher risk for clean slate and elimination of legacy code. Maintenance window acceptable.

**Decision Impact**: T119 implements wipe script with environment safety, T120 validates test deployment, T121 production deployment requires DBA approval and backup validation. Rollback plan documented in plan.md.

---

### Q3: Testing Scope - MVP test coverage vs comprehensive coverage?

**Context**: Creating integration tests for 60-100 procedures requires significant effort. MVP approach tests critical procedures first.

**Options**:

1. **MVP coverage**: Test top 20 critical procedures (high usage, low compliance), defer remaining tests (fast initial deployment)
2. **Comprehensive coverage**: Test ALL procedures before deployment (100% safety net, longer timeline)
3. **Phased testing**: Test procedures as refactored, deploy in batches (balanced approach)
4. **Manual testing only**: Skip automated integration tests, rely on manual validation (fastest but risky)

**User Response**: Comprehensive coverage (Option 2) - all procedures tested before deployment. Accepts longer timeline (Part B = 7.5 days) for complete safety net. 100% test pass rate required (SC-003).

**Decision Impact**: T108-T111 create ~280 test methods covering all 70 procedures. T112 validates test isolation. T122 executes full suite post-deployment.

---

### Q4: Priority Determination - How to rank procedures for refactoring order?

**Context**: Not all procedures equal priority. Scoring system determines refactoring sequence within Part C.

**Options**:

1. **Usage-based**: High call site count = high priority (addresses most errors first)
2. **Compliance-based**: Low compliance score = high priority (worst procedures first)
3. **Hybrid scoring**: Combine usage and compliance with weighted formula (balanced approach)
4. **Domain-based**: Refactor by domain (inventory → users → master data → logging) regardless of usage (organized but may leave critical errors)

**User Response**: Hybrid scoring (Option 3) with formula: Priority = (CallSiteCount × 0.4) + (ComplianceDeficiency × 0.6). Weights compliance slightly higher than usage because low-compliance procedures cause more errors even with moderate usage.

**Decision Impact**: T105 generates priority matrix using hybrid formula. T113 refactors top 20 by priority score before T114-T117 refactor remaining procedures by domain.

---

### Q5: Deployment Timing - When to deploy (business hours vs off-hours)?

**Context**: Database wipe requires maintenance window. Timing affects user impact and rollback options.

**Options**:

1. **Off-hours deployment** (evening/weekend): Minimal user impact, requires on-call staff
2. **Business hours deployment**: Staff available immediately, affects active users
3. **Phased deployment**: Deploy to subset of users first (A/B testing approach)
4. **Deployment window flexibility**: Let ops team choose based on business calendar

**User Response**: Off-hours deployment (evening) with 30-minute maintenance window. Notify users 24 hours in advance. DBA and developer on-call for 1-hour monitoring period post-deployment. Acceptable user impact for clean slate.

**Decision Impact**: T121 production deployment scheduled for evening maintenance window. Rollback plan documented with 15-minute RTO. Smoke tests and monitoring cover 1-hour window before declaring success.

---

### Q6: Stored Procedure Compliance Standards - Strict enforcement vs flexible interpretation?

**Context**: `00_STATUS_CODE_STANDARDS.md` defines template patterns. Some procedures may have legitimate deviations.

**Options**:

1. **Strict 100% compliance**: All procedures match template exactly, no exceptions (uniform but may force awkward refactors)
2. **Flexible interpretation**: Allow deviations if documented and justified (pragmatic but risks inconsistency)
3. **Core requirements only**: Enforce OUT p_Status and OUT p_ErrorMsg, allow flexibility elsewhere (balanced) - **Make testing return verbose results on failure for easier troubleshooting**
4. **Tiered compliance**: Critical procedures (high usage) require strict compliance, low-usage procedures flexible (risk-based)

**User Response**: Core requirements with verbose testing (Option 3). All procedures MUST have OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500) with standard status codes (-5 to +1). Allow flexibility in internal implementation details if properly documented. **Critical addition**: Integration tests must return verbose results on failure including: full exception message, stack trace, parameter values passed, expected vs actual output values, and procedure execution time. Verbose output aids rapid troubleshooting during Phase 2.5 refactoring when test failures common.

**Decision Impact**: T103 audit reports compliance score focusing on mandatory outputs and error handling. T113-T118 refactor procedures enforcing core requirements with documented flexibility. T108-T111 test creation includes verbose failure reporting with custom assertion messages. SC-003 requires 100% test pass rate; verbose failures accelerate debugging when failures occur.

---

### Q7: Parameter Prefix Fallback Strategy - What if INFORMATION_SCHEMA query fails?

**Context**: FR-002 requires parameter prefix detection at startup. Fallback needed if database unreachable or permissions insufficient.

**Options**:

1. **Retry Strategy**: Stop startup process with MessageBox allowing user to retry or quit. After 3rd failed retry, close app showing remaining attempts (chosen)
2. **Convention fallback**: Use naming conventions (p* default, in* for Transfer*/transaction*) if query fails (graceful degradation)
3. **Ask user**: Prompt DBA for database connection fix during startup (interactive but blocks startup)
4. **Offline cache**: Ship application with pre-built cache file, refresh periodically (robust but requires maintenance)

**User Response**: **Retry Strategy 1st** (Option 1) - Stop startup process with MessageBox prompting user to retry or quit when INFORMATION_SCHEMA query fails. Show clear message: "Failed to load database parameter metadata (Attempt X of 3). [Retry] [Quit]". After 3rd failed retry, close application with final message: "Unable to connect to database after 3 attempts. Application will close. Please check database connectivity and try again." Display remaining attempts on each retry. Prevents running application with incomplete/fallback parameter data which could cause subtle bugs.

**Decision Impact**: T123 implements retry dialog with attempt counter. Startup sequence blocks until schema query succeeds or user quits. No fallback to convention-based guessing - application requires accurate parameter metadata or refuses to start. Logging captures retry attempts and final failure reason. User documentation includes troubleshooting section for parameter cache failures.

---

### Q8: Integration Test Database Isolation - Per-test vs per-class transactions?

**Context**: FR-018 specifies transaction management for test isolation. Granularity affects test complexity and safety.

**Options**:

1. **Per-test transactions**: Begin/rollback in [TestInitialize]/[TestCleanup] (finest isolation but slower)
2. **Per-class transactions**: Begin in [ClassInitialize], rollback in [ClassCleanup] (faster but tests share transaction)
3. **Manual transaction management**: Tests explicitly manage transactions when needed (flexible but error-prone)
4. **Database reset**: Restore test database from snapshot before each test run (slowest but most realistic)

**User Response**: Per-test transactions (Option 1) via base class [TestInitialize]/[TestCleanup]. Accepts slight performance overhead (~50ms per test) for guaranteed isolation. Each test gets clean database state. Prevents interference between tests.

**Decision Impact**: T107 BaseIntegrationTest implements per-test transaction pattern. T112 validates isolation (parallel test runs don't interfere). ~280 tests × 50ms = 14 seconds total test suite runtime (acceptable).

---

### Q9: Performance Baseline Measurement - When to establish baseline?

**Context**: SC-004 requires ±5% performance variance from baseline. Baseline must be measured before refactoring begins.

**Options**:

1. **Before Phase 2.5 begins**: Measure now, use as definitive baseline (most accurate)
2. **Before deployment**: Measure just before T120 test deployment (captures any recent changes)
3. **Historical data**: Use existing performance logs as baseline (no new measurement needed but may be outdated)
4. **Dual baseline**: Measure before Phase 2.5 AND before deployment, compare both (most thorough but complex)

**User Response**: Before Phase 2.5 begins (Option 1) - measure baseline immediately as prerequisite to T100. Captures current performance before any Phase 2.5 changes. T128 compares post-refactor performance to this baseline. Accepts that interim changes (Phase 1-2) already included in baseline.

**Decision Impact**: Add prerequisite task T099 (not numbered in spec): "Establish Performance Baseline" measuring 10 key operations 100 times each. Results stored in `performance-baseline-2025-10-15.csv`. T128 compares to this file.

---

### Q10: Documentation Update Scope - Which docs need updating?

**Context**: Phase 2.5 affects multiple documentation files. Scope determines documentation effort.

**Options**:

1. **Minimal updates**: Update only 00_STATUS_CODE_STANDARDS.md and quickstart.md (fast but incomplete)
2. **Comprehensive updates**: Update all docs (standards, quickstart, DAO XML comments, README, AGENTS.md) (thorough but time-consuming)
3. **Prioritized updates**: Update developer-facing docs (quickstart, DAO comments), defer user docs (README) (balanced)
4. **Just-in-time updates**: Update docs as developers request during adoption (minimal upfront effort but knowledge gap)
5. **Concurrent documentation**: Update all relevant documentation at the same time as refactoring code/creating stored procedure with documentation checklist (synchronized approach)

**User Response**: **Concurrent documentation** (Option 5) - Update all relevant documentation simultaneously during code/procedure refactoring rather than as separate phase. When refactoring a stored procedure in T113-T118, developer must immediately update: (1) Procedure header comments, (2) DAO XML documentation for calling method, (3) 00_STATUS_CODE_STANDARDS.md examples if procedure demonstrates new pattern, (4) quickstart.md if procedure commonly used. **Create documentation checklist** showing which documents need updates during which task steps. Documentation becomes integral deliverable for each refactoring task, not separate cleanup phase.

**Decision Impact**: Part F (T129-T132) restructured from bulk documentation phase to **documentation checklist creation**. New T129 deliverable: "Documentation Update Matrix" spreadsheet mapping each T113-T118 refactoring task to required documentation updates (columns: Task ID, Procedure Name, Header Comments Required, DAO XML Required, Standards Update Required, Quickstart Update Required). T113-T118 task definitions updated to include documentation checkboxes. T131 becomes documentation validation task ensuring all concurrent updates completed. Prevents documentation drift and reduces end-of-phase documentation burden.

---

## Summary of Decisions

| Question                      | Decision                                 | Rationale                                                                                 |
| ----------------------------- | ---------------------------------------- | ----------------------------------------------------------------------------------------- |
| Q1: Database Source           | Production database                      | Source of truth, test mirrors production                                                  |
| Q2: Deployment Strategy       | Complete wipe with safety                | Clean slate, eliminates legacy code                                                       |
| Q3: Testing Scope             | Comprehensive coverage                   | 100% safety net, accepts longer timeline                                                  |
| Q4: Priority Determination    | Hybrid scoring (usage + compliance)      | Balanced approach, addresses critical errors first                                        |
| Q5: Deployment Timing         | Off-hours with maintenance window        | Minimal user impact, rollback support                                                     |
| Q6: Compliance Standards      | Core requirements + verbose testing      | Mandatory outputs with flexible implementation, verbose test failures for troubleshooting |
| Q7: Parameter Prefix Fallback | Retry with app termination               | 3 retry attempts with user choice, prevents running with incomplete data                  |
| Q8: Test Isolation            | Per-test transactions                    | Guaranteed isolation, acceptable overhead                                                 |
| Q9: Performance Baseline      | Before Phase 2.5 begins                  | Captures pre-refactor state accurately                                                    |
| Q10: Documentation Scope      | Concurrent with code changes + checklist | Updates synchronized with refactoring, prevents documentation drift                       |

---

---

## Session 2: Implementation Details and Edge Cases (2025-10-15)

### Q11: Stored Procedure Versioning - How to handle schema evolution during Phase 2.5?

**Context**: Phase 2.5 takes 15-25 days. Production database may change during this period (hotfixes, emergency procedures).

**Options**:

1. **Schema freeze**: No production changes during Phase 2.5 (simplest but blocks hotfixes)
2. **Version control integration**: Track procedure versions in Git, merge conflicts manually (flexible but complex)
3. **Timestamp-based validation**: Record procedure modified dates, detect drift during deployment (automated detection)
4. **Accept drift and re-audit**: Deploy anyway, run fresh audit post-deployment to catch any drift (pragmatic approach) - **Selected**

**User Response**: **Accept drift and re-audit** (Option 4) - Phase 2.5 duration (15-25 days) makes schema freeze impractical. Allow emergency hotfixes and production changes during Phase 2.5 implementation. After completing refactoring work but before T120 test deployment, run fresh T101 audit against current production state to capture any procedures added/modified during Phase 2.5. Compare fresh audit to original baseline audit - flag differences for review. Integrate legitimate production changes into refactored procedure set before deployment. Pragmatic approach prevents blocking critical business hotfixes.

**Decision Impact**: T101 audit results timestamped and versioned (baseline vs pre-deployment). New task inserted before T120: **T119b - Re-audit Production Procedures** to capture drift. Deployment planning includes drift reconciliation step. T120 deployment uses re-audited procedure set, not original baseline. Documentation notes that Phase 2.5 procedures = refactored baseline + integrated production changes.

---

### Q12: Transaction Rollback Testing - How to verify incomplete operations leave no orphans?

**Context**: T127 requires validation of transaction rollback completeness. Need systematic approach to verify data integrity.

**Options**:

1. **Manual query verification**: Run SELECT queries after forced rollback, inspect results (simple but error-prone)
2. **Database snapshots**: Compare pre-operation and post-rollback snapshots (thorough but storage-intensive)
3. **Checksum validation**: Calculate table checksums before/after, verify match (fast but doesn't show what changed)
4. **Audit table inspection**: Query transaction log tables for orphaned entries (leverages existing audit trail)

**User Response**: Audit table inspection (Option 4) combined with targeted queries. After forced rollback, query `inv_transaction` for uncommitted transaction markers, query inventory tables for quantity mismatches (SUM of transactions ≠ current quantity). Use existing `TransactionID` correlation to detect orphans. Faster than snapshots, more precise than checksums.

**Decision Impact**: T127 test pattern: Begin transaction → Insert transaction log entry → Perform inventory operation → Force error → Rollback → Query transaction log for orphaned `TransactionID` → Query inventory for quantity drift. Expected results: 0 orphaned logs, 0 quantity mismatches.

---

### Q13: Parameter Prefix Convention Rules - What are the fallback conventions if schema query fails?

**Context**: Q7 specified retry strategy with app termination on failure. This question now addresses maintenance/development tool needs.

**Options**:

1. **Universal p\_ prefix**: All parameters use `p_` prefix (simplest but may cause errors for Transfer/Transaction procedures)
2. **Pattern-based detection**: Procedures matching `*Transfer*` or `*Transaction*` use `in_`, others use `p_` (heuristic-based)
3. **Development maintenance form**: Implement maintenance UI in Settings Form as new child UserControl under "Development" TreeView category. **User must have Developer role** (Admin + Developer privileges) to access (controlled management) - **Selected**
4. **First-call learning**: Attempt `p_`, if fails try `in_`, cache successful prefix (adaptive but requires error tolerance)

**User Response**: **Development maintenance form** (Option 3) - Implement comprehensive parameter prefix management UI as new UserControl under Settings Form. **New TreeView category "Development"** with child node "Parameter Prefix Management". Access requires **new Developer user role** (= Admin privileges + Developer flag in database). Form displays all procedures with current prefix mappings, allows manual override, shows detection confidence scores, provides bulk re-detection button, and logs all changes for audit. Prevents need for app restart when fixing prefix mismatches. Power-user tool for development and troubleshooting.

**Decision Impact**: New tasks required in Phase 2.5:

-   **T113c - Create Developer User Role**: Add `IsDeveloper` BOOLEAN column to `sys_user` table, update user management UI to assign role, restrict Development TreeView node visibility to Developer role
-   **T113d - Create Parameter Prefix Maintenance Form**: New UserControl `Control_Settings_ParameterPrefixMaintenance` with DataGridView showing all procedures/parameters, prefix columns (detected vs override), confidence score column, Save/Reload buttons, and audit log panel
-   **T123 update**: Parameter cache implements reload mechanism called from maintenance form, validates prefix changes before applying
-   Settings Form TreeView includes gated Development category (visible only to Developer role users)

---

### Q14: Compliance Score Calculation - What defines "compliance deficiency" in priority formula?

**Context**: Q4 specified hybrid priority formula with "ComplianceDeficiency" factor. Need precise definition for T105 priority matrix calculation.

**Options**:

1. **Binary scoring**: Compliant (0) or Non-Compliant (100) based on OUT parameters only (simple but coarse)
2. **Multi-factor scoring**: Weighted factors (missing OUT params 40%, wrong status codes 30%, no error handling 20%, parameter prefix issues 10%) (detailed but complex)
3. **Violation count**: Count total violations (0-10 scale), normalize to 0-100 (straightforward)
4. **Risk-weighted scoring**: Critical violations (missing OUT params) worth more than minor violations (inconsistent prefixes) (prioritizes safety)

**User Response**: Multi-factor scoring (Option 2) with explicit weights. ComplianceDeficiency = (MissingOUTParams × 40) + (WrongStatusCodes × 30) + (NoErrorHandling × 20) + (PrefixInconsistency × 10). Each factor scored 0-100% deficiency. Example: Procedure missing both OUT params (100% deficiency on factor 1) = 40 points. Procedure with all issues scores near 100.

**Decision Impact**: T103 audit calculates per-factor deficiency scores. T105 priority matrix uses formula: Priority = (CallSiteCount × 0.4) + (ComplianceDeficiency × 0.6) where ComplianceDeficiency is weighted sum. Top 20 procedures by priority score refactored in T113.

---

### Q15: Test Data Management - How to provide realistic test data without exposing production data?

**Context**: T108-T111 require test data for integration tests. Production data may contain sensitive information (part numbers, customer info).

**Options**:

1. **Synthetic data generation**: Script generates random part numbers, locations, users (safe but unrealistic)
2. **Production data anonymization**: Copy production, scramble sensitive fields (realistic but complex)
3. **Manual test data creation**: Developers create small dataset manually (controlled but limited scale)
4. **Seed data scripts**: Version-controlled SQL INSERT scripts for test database (repeatable and safe)

**User Response**: Seed data scripts (Option 4) with realistic patterns. Create `Database/TestData/seed-test-data.sql` containing ~100 parts, ~10 locations, ~5 users, ~20 operations with realistic naming patterns (PART-001, LOC-FLOOR, USER-TEST1) but no production data. Script idempotent (can run multiple times). Version controlled for consistency across developers.

**Decision Impact**: T107 BaseIntegrationTest setup executes seed script once per test class. T108-T111 tests reference known seed data (PART-001 exists, LOC-FLOOR valid, etc.). Each developer runs seed script locally during environment setup. Part of T120 test deployment validation.

---

### Q16: Error Logging Recursive Prevention - How to test file fallback without affecting production?

**Context**: T125 must validate recursive prevention, but dropping log_error table in production unacceptable.

**Options**:

1. **Test database only**: Drop log_error in test database temporarily (safe but doesn't validate production behavior)
2. **Mock implementation**: Unit test with mocked MySqlConnection throwing exceptions (fast but not integration test)
3. **Temporary rename**: RENAME TABLE log_error TO log_error_temp during test, restore after (reversible in production)
4. **Permission revocation**: REVOKE INSERT on log_error for test user temporarily (safer than drop/rename)

**User Response**: Test database only (Option 1) with documented manual validation procedure for production. T125 integration test drops log_error in `mtm_wip_application_winforms_test`, triggers error, verifies file fallback, restores table. Manual validation checklist for post-deployment: DBA temporarily revokes INSERT on log_error, developer triggers test error via UI, confirms file fallback, DBA restores permissions. Documented in T126 manual testing checklist.

**Decision Impact**: T125 automated test uses test database table drop. T126 manual testing includes production recursive prevention validation (Step 23 of 25 in manual checklist). Post-deployment validation report includes confirmation of file fallback test.

---

### Q17: Slow Query Threshold Configuration - Where to store thresholds and how to adjust?

**Context**: FR-020 defines category-based thresholds (Query=500ms, Modification=1000ms, Batch=5000ms, Report=2000ms). Storage location affects runtime adjustability.

**Options**:

1. **Hardcoded constants**: Define in `Model_AppVariables.cs` as const fields (fastest but requires recompile to change)
2. **appsettings.json**: Store in configuration file (adjustable without recompile but requires app restart)
3. **Database configuration table**: `sys_configuration` table with key-value pairs (runtime adjustable, no restart)
4. **Environment variables**: OS-level configuration (deployment-specific but requires container/process restart)

**User Response**: appsettings.json (Option 2) with Model_AppVariables wrapper. Configuration section: `"PerformanceThresholds": { "Query": 500, "Modification": 1000, "Batch": 5000, "Report": 2000 }`. Model_AppVariables loads on startup, caches in memory. Allows threshold tuning without recompile. Restart required to apply changes (acceptable for performance tuning).

**Decision Impact**: T002 (Phase 1) adds PerformanceThresholds section to appsettings.json. Model_AppVariables exposes properties: QueryThresholdMs, ModificationThresholdMs, BatchThresholdMs, ReportThresholdMs. Helper_Database_StoredProcedure reads thresholds for performance comparison. T128 benchmark uses same thresholds for consistency.

---

### Q18: Stored Procedure Extraction Format - Individual files vs single monolithic file?

**Context**: T102 extracts procedures from production. File organization affects maintainability and deployment.

**Options**:

1. **Individual files**: One .sql file per procedure in domain folders (organized but many files)
2. **Domain aggregation**: One file per domain (inv_inventory.sql, sys_user.sql) with all procedures (fewer files but large)
3. **Single monolithic file**: All procedures in ALL_PROCEDURES.sql (simplest deployment but hard to navigate)
4. **Hybrid**: Individual files + generated aggregate for deployment (best of both)

**User Response**: Individual files (Option 1) mirroring UpdatedStoredProcedures/ structure. Directory structure: `Database/UpdatedStoredProcedures/Inventory/inv_inventory_Add.sql`, `/Users/sys_user_GetByName.sql`, etc. One procedure per file with header comment (procedure name, description, parameters, status codes). Enables Git diff tracking, code review granularity, selective deployment if needed.

**Decision Impact**: T102 generates individual .sql files organized by domain. T119 deployment script uses `Get-ChildItem -Recurse *.sql` to discover files, executes in alphabetical order. T113-T118 refactoring updates individual files, preserving structure. Git shows procedure-level changes clearly.

---

### Q19: Integration Test Execution Environment - Local developer machines vs CI/CD pipeline?

**Context**: T108-T111 create 280 tests. Execution environment affects database access, configuration, and reporting.

**Options**:

1. **Local only**: Developers run tests on localhost test database (fast feedback but inconsistent environments)
2. **CI/CD only**: Tests run in GitHub Actions/Azure DevOps pipeline (consistent but slower feedback)
3. **Hybrid**: Developers run locally, CI/CD gates deployment (best of both but requires dual configuration)
4. **Containerized**: Docker container with MySQL for tests (portable but adds complexity)

**User Response**: Hybrid (Option 3) with shared test database configuration. Developers run tests locally against `localhost:3306/mtm_wip_application_winforms_test`. CI/CD pipeline uses same database on build agent (pre-seeded). Shared `appsettings.Test.json` configuration with connection string. Local runs provide fast feedback during development. CI/CD runs validate before PR merge (gates deployment to production).

**Decision Impact**: T107 BaseIntegrationTest reads configuration from appsettings.Test.json. Developers configure local test database during onboarding. CI/CD pipeline (future Phase 3 task) includes test execution step with connection to build agent database. T122 post-deployment tests run in both environments (local for DBA validation, CI/CD for audit trail).

---

### Q20: Backup Validation Thoroughness - Quick validation vs full restore test?

**Context**: T120 and T121 require backup validation before deployment. Thoroughness vs speed tradeoff.

**Options**:

1. **File size check only**: Verify backup file >1MB (fast but weak validation)
2. **Procedure count validation**: Restore to temp database, count procedures, compare to production (moderate validation)
3. **Full restore test**: Restore to temp database, run smoke tests, verify functionality (thorough but slow)
4. **Checksum validation**: Calculate mysqldump checksum, verify restore checksum matches (fast and reliable)

**User Response**: Procedure count validation (Option 2) for T120 test deployment, full restore test (Option 3) for T121 production deployment. Test deployment accepts moderate validation (5 minutes) since test database expendable. Production deployment requires full validation (15 minutes): Restore to temporary database `mtm_wip_application_restore_test`, count procedures (must match), run 5 smoke test queries (inventory count, user count, transaction log count), verify results. Ensures backup viable for rollback.

**Decision Impact**: T120 deployment script validates backup with procedure count check. T121 deployment script performs full restore test to temporary database before proceeding with production wipe. Rollback plan (plan.md) references validated backup for 15-minute RTO. Post-deployment monitoring includes backup validation report.

---

### Q21: Multi-Step Procedure Identification - How to detect which procedures require explicit transactions?

**Context**: FR-011 requires explicit transactions for multi-step operations. T114 needs criteria to identify these procedures.

**Options**:

1. **Manual inspection**: Developer reviews each procedure, tags multi-step (accurate but time-consuming)
2. **Pattern detection**: Scan for multiple INSERT/UPDATE/DELETE statements (automated but may miss complex logic)
3. **Call graph analysis**: Trace procedures that call other procedures (detects indirect multi-step)
4. **CSV generation with recommendations**: Generate CSV file for each stored procedure with recommended execution approach based on codebase analysis, include open correction column. Must complete before first procedure refactor (structured documentation) - **Selected**

**User Response**: **CSV generation with recommendations** (Option 4) - T103 audit must generate CSV file for EVERY stored procedure containing: (1) Procedure name, (2) Detected pattern (single-step, multi-step, batch, reporting), (3) Recommended transaction strategy (explicit, implicit, none), (4) Detection confidence (High/Medium/Low), (5) Rationale (why recommendation made based on code patterns), (6) **Correction column** (initially empty for developer review). Developer reviews CSV, fills correction column where automated detection wrong, returns corrected CSV as input to T114 refactoring. **Must be complete before first stored procedure refactor is complete** (gates T113).

**Decision Impact**:

-   T103 deliverable expanded: Audit produces `procedure-transaction-analysis.csv` alongside compliance report
-   CSV structure: `ProcedureName, DetectedPattern, RecommendedStrategy, Confidence, Rationale, DeveloperCorrection, RefactoringNotes`
-   New gate between T103 and T113: Developer review checkpoint requiring completed CSV with corrections
-   T114-T118 refactoring uses corrected CSV as authoritative source for transaction management decisions
-   T129 Documentation Update Matrix references CSV for procedure classification

---

### Q22: DAO Method Naming Consistency - Standardize naming convention for new methods?

**Context**: Existing DAOs have mixed naming (GetByName, Get_ByName, GetInventoryByPartId, FetchUser). T113-T118 refactoring opportunity to standardize.

**Options**:

1. **Preserve existing names**: No renaming, maintain backward compatibility (safest but inconsistent)
2. **PascalCase with descriptive verbs**: GetByName, AddInventory, UpdateUser (standard C# convention)
3. **CRUD prefix convention**: CreateUser, ReadInventory, UpdatePart, DeleteTransaction (explicit CRUD)
4. **Async suffix mandatory**: GetByNameAsync, AddInventoryAsync (enforces async awareness)

**User Response**: PascalCase with descriptive verbs + Async suffix mandatory (combination of Options 2 and 4). All DAO methods use pattern: `{Verb}{Entity}[By{Criteria}]Async`. Examples: `GetUserByNameAsync`, `AddInventoryAsync`, `UpdatePartQuantityAsync`, `DeleteTransactionByIdAsync`. Rename during T113-T118 refactoring. Breaking change acceptable since internal DAOs, not public API. Update all call sites during T055-T084 UI refactoring tasks.

**Decision Impact**: T115 establishes naming standard in 00_STATUS_CODE_STANDARDS.md. T113-T118 rename methods consistently. T055-T084 update call sites (Form and Control refactoring). IntelliSense shows clear method purpose. T130 quickstart includes naming convention examples.

---

### Q23: Connection String Security - How to protect credentials in appsettings.json?

**Context**: FR-013 centralizes connection strings in configuration. Production credentials need protection.

**Options**:

1. **User Secrets**: ASP.NET Core User Secrets for development (dev-only, not applicable to WinForms)
2. **Environment variables**: Read connection string from OS environment (deployment-specific but visible in process list)
3. **Encrypted configuration**: Encrypt appsettings.json sections using DPAPI (secure but complex key management)
4. **Azure Key Vault**: Store credentials in cloud vault (most secure but requires Azure subscription)
5. **Current implementation unchanged**: Keep existing Helper_Database_Variables pattern where users don't enter credentials on startup - **Selected**

**User Response**: **Current implementation unchanged** (Option 5) - Do NOT change existing credential management. Current system where users don't need to enter credentials on application startup works well. Helper_Database_Variables already handles connection string assembly securely using environment-specific configuration. No need to introduce additional complexity (environment variables, encryption, Key Vault) for credential management. Maintain status quo - existing approach sufficient for current deployment model.

**Decision Impact**:

-   FR-013 connection string centralization focuses on **code organization** not credential management changes
-   T013 refactoring consolidates scattered connection string assembly code into Helper_Database_Variables without changing underlying security model
-   No new tasks for credential encryption/Key Vault integration
-   Documentation clarifies that connection string centralization improves maintainability without altering credential protection approach
-   Current appsettings.json structure retained with existing credential storage pattern

---

### Q24: Static Analysis Tool Selection - Which tool for T124 Helper routing compliance?

**Context**: T124 validates 100% Helper routing (no direct MySQL API usage). Tool choice affects accuracy and automation.

**Options**:

1. **Grep/ripgrep**: Simple text search for "new MySqlConnection", "new MySqlCommand" (fast but may miss dynamic usage)
2. **Roslyn analyzers**: Custom C# code analyzer with IDE integration (accurate but requires development) - **Selected**
3. **PowerShell script**: AST parsing with Get-Content and regex (scriptable and adequate accuracy)
4. **SonarQube rules**: Configure code quality rules to flag violations (enterprise but requires infrastructure)

**User Response**: **Roslyn analyzers** (Option 2) - Invest in custom Roslyn analyzer for accurate compile-time detection of direct MySQL API usage. Analyzer rules:

1. Flag `new MySqlConnection()` outside Helper_Database_StoredProcedure.cs
2. Flag `new MySqlCommand()` outside Helper classes
3. Flag `MySqlDataAdapter`, `MySqlDataReader` outside Helper classes
4. Provide code fix suggestions redirecting to Helper_Database_StoredProcedure methods

Benefits over PowerShell script: Real-time IDE feedback (red squiggles), prevents violations during development not just detection during validation, integrates with CI/CD build process, reduces false positives through semantic analysis.

**Decision Impact**:

-   New task **T124a - Develop Roslyn Analyzer**: Create custom analyzer package `MTM.CodeAnalysis.DatabaseAccess` with 4 diagnostic rules (2-3 hours development)
-   T124 becomes analyzer validation task: Run analyzer across codebase, generate compliance report, fix any violations
-   Analyzer deployed via NuGet package reference in .csproj file
-   CI/CD integration: Analyzer runs on every build, treats violations as warnings (non-blocking initially, error-level post-Phase 2.5)
-   Developer onboarding improved: New developers see violations immediately in IDE

---

### Q25: Rollback Decision Criteria - What triggers production rollback vs persevering with fix?

**Context**: T121 deployment may encounter issues. Need clear criteria for rollback vs forward-fix decision.

**Options**:

1. **Zero tolerance**: Any error triggers immediate rollback (safest but may rollback for minor issues)
2. **Critical errors only**: Rollback only for data integrity risks or crashes (pragmatic but judgment call)
3. **Time-based**: Rollback if not resolved within 15 minutes (RTO enforcement)
4. **Smoke test gated**: Rollback if smoke tests fail, otherwise investigate (objective criteria)

**User Response**: Smoke test gated (Option 4) with 15-minute investigation window. Post-deployment smoke tests (5 critical procedures) must pass within 5 minutes. If smoke tests pass, deployment considered successful even if non-critical issues observed. If smoke tests fail, 15-minute investigation window begins. DBA and developer collaborate on fix. If not resolved in 15 minutes OR if data integrity risk identified, trigger rollback. Forward-fix acceptable for minor issues (performance degradation, cosmetic errors).

**Decision Impact**: T121 deployment procedure includes smoke test execution immediately post-deployment. Smoke tests defined: inventory add, user authentication, transaction log query, part search, location retrieval. Pass criteria: All return expected status codes, no exceptions, complete within 30 seconds total. Rollback script ready on standby. Post-deployment report documents smoke test results and any issues.

---

## Summary of Additional Decisions

| Question                   | Decision                                      | Rationale                                                |
| -------------------------- | --------------------------------------------- | -------------------------------------------------------- |
| Q11: Schema Evolution      | Accept drift and re-audit                     | Allows emergency hotfixes, fresh audit before deployment |
| Q12: Rollback Testing      | Audit table inspection                        | Leverages existing audit trail efficiently               |
| Q13: Prefix Conventions    | Development maintenance form (Developer role) | UI-based management, requires Developer role access      |
| Q14: Compliance Scoring    | Multi-factor weighted scoring                 | Detailed prioritization of violations                    |
| Q15: Test Data             | Seed data scripts                             | Repeatable, safe, version-controlled                     |
| Q16: File Fallback Testing | Test DB with manual prod validation           | Safe automation + documented prod verification           |
| Q17: Threshold Storage     | appsettings.json                              | Adjustable without recompile, restart acceptable         |
| Q18: Procedure Files       | Individual files by domain                    | Git-friendly, code review granularity                    |
| Q19: Test Execution        | Hybrid local + CI/CD                          | Fast feedback + consistent validation                    |
| Q20: Backup Validation     | Count for test, full for prod                 | Balanced speed vs thoroughness                           |
| Q21: Multi-Step Detection  | CSV generation with recommendations           | Structured documentation, developer review checkpoint    |
| Q22: DAO Naming            | PascalCase + Async suffix                     | Standard C# convention, clear intent                     |
| Q23: Credential Security   | Current implementation unchanged              | Existing approach sufficient, no changes needed          |
| Q24: Static Analysis Tool  | Roslyn analyzers                              | Real-time IDE feedback, accurate semantic analysis       |
| Q25: Rollback Criteria     | Smoke test gated + 15min window               | Objective criteria, time-bounded investigation           |

---

## Open Questions

_None_ - All clarifications resolved during Sessions 1-2 (2025-10-15).

---

## Session 3: Refinement from User Feedback (2025-10-15)

### Q26: Verbose Test Failure Output Format - What specific information should verbose test failures include?

**Context**: Q6 updated to require verbose test output on failure. Need to define exact output format for consistency.

**Options**:

1. **Exception-only**: Just exception message and stack trace (minimal)
2. **Parameters-included**: Exception + input parameters used (good for reproduction)
3. **Comprehensive diagnostic**: Exception + parameters + expected vs actual + execution time + database state snapshot (thorough)
4. **Custom assertion messages**: Hand-crafted messages per test with context-specific details (flexible but inconsistent)

**User Response**: **Comprehensive diagnostic** (Option 3) - Every integration test failure must output:

1. Exception message and full stack trace
2. All input parameters (name/value pairs) passed to stored procedure
3. Expected output values (status code, specific data rows)
4. Actual output values received
5. Procedure execution time in milliseconds
6. Relevant database state (row counts in affected tables before/after)
7. Test method name and timestamp

Format as structured JSON block for easy parsing. Enables rapid diagnosis without re-running tests or attaching debugger.

**Decision Impact**: T108-T111 test creation includes base assertion helper method `AssertProcedureResult()` that captures all diagnostic data and formats as JSON on failure. BaseIntegrationTest provides helper methods for table row count snapshots. Test output includes both human-readable summary and JSON diagnostic block.

---

### Q27: Developer Role Permission Scope - What additional privileges does Developer role grant beyond Admin?

**Context**: Q13 introduced Developer role for parameter prefix maintenance form. Need to define complete permission scope.

**Options**:

1. **Single-purpose**: Only access to Parameter Prefix Maintenance form (narrowest scope)
2. **Development tools category**: Access to all Development TreeView tools (logging viewer, cache management, diagnostic tools)
3. **Admin+Debug features**: All Admin permissions plus debug/diagnostic features (broader scope)
4. **Configurable permissions**: Admin can grant specific developer permissions per-user (most flexible)

**User Response**: **Development tools category** (Option 2) - Developer role grants access to entire Development TreeView category including:

-   Parameter Prefix Maintenance (primary tool from Q13)
-   Performance baseline measurement tools (future)
-   Database connection diagnostics
-   Stored procedure call history viewer
-   Log file viewer with filtering
-   Cache inspection and refresh tools

Role hierarchy: Basic User < Admin < Developer. Developer inherits all Admin permissions plus Development tools. Cannot be granted independently - user must be Admin first, then granted Developer flag. Prevents accidental exposure of diagnostic tools to regular users.

**Decision Impact**: T113c Developer role implementation includes:

-   Database flag `IsDeveloper` BOOLEAN (requires `IsAdmin = TRUE` as prerequisite)
-   Settings Form TreeView "Development" node visibility check: `CurrentUser.IsAdmin && CurrentUser.IsDeveloper`
-   User management form updated with Developer checkbox (enabled only if Admin checked)
-   Role validation in Control base constructors for all Development tools
-   Documentation updated with role permission matrix

---

### Q28: Documentation Update Matrix Structure - How should concurrent documentation checklist be organized?

**Context**: Q10 changed documentation approach to concurrent updates with checklist. Need to define checklist structure.

**Options**:

1. **Simple checkbox list**: Flat list of documentation items per task (easy but lacks context)
2. **Spreadsheet matrix**: Rows=tasks, Columns=doc types, Cells=checkboxes (visual but manual maintenance)
3. **Markdown table with links**: Task ID → Doc types with file path links (version-controllable and navigable)
4. **Automated tracking**: Script generates checklist from code comments (least manual effort but requires discipline)

**User Response**: **Markdown table with links** (Option 3) - Create `Documentation-Update-Matrix.md` in 003-database-layer-refresh folder with structure:

```markdown
| Task ID | Procedure Name       | Header Comments                                                             | DAO XML Docs                         | Standards Update | Quickstart Update | Status         |
| ------- | -------------------- | --------------------------------------------------------------------------- | ------------------------------------ | ---------------- | ----------------- | -------------- |
| T113-1  | inv_inventory_Add    | [Link](../Database/UpdatedStoredProcedures/Inventory/inv_inventory_Add.sql) | [Link](../Data/Dao_Inventory.cs#L45) | N/A              | N/A               | ⬜ Not Started |
| T113-2  | inv_inventory_Update | [Link](...)                                                                 | [Link](...)                          | Required         | N/A               | ⬜ Not Started |
```

Status values: ⬜ Not Started, 🔄 In Progress, ✅ Complete, ⚠️ Needs Review

Each cell contains file path link (clickable in VS Code/GitHub). "Required"/"N/A" indicates whether update needed. Matrix generated by T129, maintained during T113-T118, validated by T131.

**Decision Impact**: T129 deliverable changed from generic documentation phase to matrix generation. Matrix becomes single source of truth for documentation completion tracking. Daily standups reference matrix for progress updates. T131 validation script checks all "Required" cells have "✅ Complete" status before Phase 2.5 completion.

---

### Q29: Re-Audit Drift Reconciliation Process - How to handle procedures added/modified during Phase 2.5?

**Context**: Q11 changed to accept drift with re-audit. Need process for reconciling baseline audit vs pre-deployment audit differences.

**Options**:

1. **Manual merge**: Developer reviews diff, manually integrates changes into refactored procedures (accurate but time-consuming)
2. **Automated three-way merge**: Script compares baseline, refactored, and production versions, flags conflicts (faster but may miss semantic conflicts)
3. **Override with production**: If production changed, use production version, discard refactored version (safest but loses refactoring work)
4. **Staged reconciliation**: Tag production changes as "hotfix", refactor separately, merge last (organized but adds phase)

**User Response**: **Staged reconciliation** (Option 4) - T119b re-audit produces drift report identifying procedures added/modified after baseline. Developer categorizes each drifted procedure:

-   **Category A - Independent hotfix**: Production change unrelated to Phase 2.5 refactoring (keep production version as-is, apply Phase 2.5 standards separately)
-   **Category B - Conflicting change**: Production change affects same procedure being refactored (manual three-way merge required)
-   **Category C - New procedure**: Procedure added to production during Phase 2.5 (refactor according to standards before deployment)

Each category gets separate task:

-   **T119c - Refactor Category A procedures**: Apply standards to hotfixes (preserve business logic changes)
-   **T119d - Merge Category B conflicts**: Manual merge with conflict resolution documentation
-   **T119e - Refactor Category C procedures**: New procedures get full Phase 2.5 treatment

All three complete before T120 test deployment. Reconciliation report documents all drift handling decisions.

**Decision Impact**: T119b deliverable expanded to include categorized drift report. New tasks T119c/d/e added to Part D timeline (adds 0.5-1 day depending on drift volume). T120 deployment uses post-reconciliation procedure set. Documentation includes drift reconciliation report as Phase 2.5 appendix.

---

### Q30: CSV Transaction Analysis Correction Workflow - How should developers review and correct the generated CSV?

**Context**: Q21 requires CSV generation with correction column. Need workflow for developer review and correction process.

**Options**:

1. **Email review**: Send CSV via email, developer returns corrections (informal but simple)
2. **Spreadsheet collaboration**: Share Google Sheet or Excel Online for real-time editing (collaborative but requires external tool)
3. **Git-based review**: Commit CSV to Git, developer creates PR with corrections, merge after review (version-controlled)
4. **In-app review tool**: Create simple WinForms UI for CSV review with correction input (integrated but requires development)

**User Response**: **Git-based review** (Option 3) with structured review process:

1. **T103 completion**: Developer commits `procedure-transaction-analysis.csv` to `Database/AnalysisReports/` folder
2. **Review assignment**: Tech lead assigns procedure domains to developers (e.g., Developer A reviews Inventory procedures, Developer B reviews User procedures)
3. **Correction process**: Developers checkout branch, open CSV in Excel/VS Code, fill `DeveloperCorrection` column with corrections:
    - If detection correct: Leave blank or enter "✓ Confirmed"
    - If detection wrong: Enter corrected value + rationale (e.g., "Multi-step: Also updates transaction log")
4. **Commit corrections**: Developers commit corrected CSV rows, create PR
5. **Peer review**: Second developer reviews corrections, approves PR
6. **Gate T113**: T113 cannot start until CSV PR merged and all procedures have reviewed correction status

Leverage existing Git workflow, provides audit trail of all corrections, peer review ensures quality.

**Decision Impact**: T103 includes CSV commit step. New task between T103-T113: **T106a - CSV Review and Correction** (1-2 days, parallelizable by domain). GitHub PR template updated with CSV review checklist. T113 implementation reads corrected CSV for authoritative transaction strategy decisions.

---

### Q31: Roslyn Analyzer Diagnostic Severity - Should violations be warnings or errors?

**Context**: Q24 selected Roslyn analyzer. Need to decide diagnostic severity levels.

**Options**:

1. **Errors always**: All violations block build (strictest enforcement)
2. **Warnings during Phase 2.5, errors after**: Gradual enforcement to allow refactoring (balanced)
3. **Configurable per-project**: .editorconfig controls severity (flexible but inconsistent)
4. **Warnings with CI/CD failure**: Warnings in IDE, but CI/CD treats warnings as errors (developer-friendly, gates deployment)

**User Response**: **Warnings during Phase 2.5, errors after** (Option 2) - Phased enforcement approach:

**Phase 2.5 (T124a-T132)**: Diagnostics emit **Warnings**

-   Allows developers to see violations without blocking builds during active refactoring
-   Existing violations visible but don't prevent compilation/testing
-   T124 validation generates warning report, creates remediation tasks

**Post-Phase 2.5 (T133 onwards)**: Diagnostics emit **Errors**

-   After T124 validation confirms zero violations, upgrade severity to Error
-   New violations block build, prevent regression
-   CI/CD pipeline fails on any direct MySQL API usage outside Helpers

Configuration via analyzer package version: v1.0.0 (warnings) during Phase 2.5, v2.0.0 (errors) post-completion. .csproj file updated in T124 completion task.

**Decision Impact**: T124a analyzer implements configurable severity levels. Package published to internal NuGet feed with v1.0.0-preview (warnings). T132 Phase 2.5 completion includes analyzer upgrade to v2.0.0 (errors). Documentation notes analyzer version dependency for future developers.

---

### Q32: Parameter Prefix Maintenance Form Data Persistence - How should prefix overrides be stored?

**Context**: Q13 introduced maintenance form for parameter prefix management. Need storage mechanism for user modifications.

**Options**:

1. **Database table**: `sys_parameter_prefix_override` table (persistent, multi-user access)
2. **JSON configuration file**: `parameter-prefix-overrides.json` (simple, version-controllable)
3. **In-memory cache only**: No persistence, reloads from schema on restart (no storage needed)
4. **Hybrid**: Database for overrides, cache for performance (most robust)

**User Response**: **Database table** (Option 1) - Persistent storage in database for multi-user environment:

Table structure:

```sql
CREATE TABLE sys_parameter_prefix_override (
    OverrideID INT AUTO_INCREMENT PRIMARY KEY,
    ProcedureName VARCHAR(100) NOT NULL,
    ParameterName VARCHAR(100) NOT NULL,
    DetectedPrefix VARCHAR(10),
    OverridePrefix VARCHAR(10) NOT NULL,
    Confidence DECIMAL(3,2),  -- 0.00 to 1.00
    Reason VARCHAR(500),  -- Why override needed
    CreatedBy INT NOT NULL,  -- User ID
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedBy INT,
    ModifiedDate DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsActive BOOLEAN DEFAULT TRUE,
    UNIQUE KEY unique_proc_param (ProcedureName, ParameterName)
);
```

Cache loads overrides from table at startup, maintenance form updates table directly. Audit trail tracks who made changes and when. Allows DBA to review override patterns and potentially fix root cause in stored procedures.

**Decision Impact**: T113c Developer role implementation includes table creation script. T123 cache implementation queries override table, merges with schema detection results. Maintenance form performs CRUD operations on override table. Export/import functionality allows transferring overrides between environments. Backup procedures updated to include override table.

---

## Summary of Session 3 Decisions

| Question                     | Decision                                            | Rationale                                                 |
| ---------------------------- | --------------------------------------------------- | --------------------------------------------------------- |
| Q26: Verbose Test Output     | Comprehensive diagnostic (JSON format)              | Enables rapid diagnosis without re-running tests          |
| Q27: Developer Role Scope    | Development tools category (inherits Admin)         | Complete diagnostic toolset, hierarchical permissions     |
| Q28: Documentation Matrix    | Markdown table with links                           | Version-controllable, navigable, single source of truth   |
| Q29: Drift Reconciliation    | Staged reconciliation (categorize + separate tasks) | Organized handling of hotfixes, new procedures, conflicts |
| Q30: CSV Correction Workflow | Git-based review with PR process                    | Audit trail, peer review, leverages existing workflow     |
| Q31: Analyzer Severity       | Warnings during Phase 2.5, errors after             | Gradual enforcement, prevents regression post-completion  |
| Q32: Prefix Override Storage | Database table with audit trail                     | Persistent, multi-user, tracks change history             |

---

**Document Version**: 3.0  
**Last Updated**: 2025-10-15  
**Status**: Complete - All clarifications answered (32 total questions across 3 sessions)
