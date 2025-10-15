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
1. Production database only (`mtm_wip_application` on 172.16.1.104)
2. Test database only (`mtm_wip_application_winforms_test` on localhost)
3. Both databases (audit both, reconcile differences)
4. Code analysis only (discover procedures from C# call sites)

**User Response**: Production database (`mtm_wip_application`) as source of truth. Test database should mirror production after deployment. Code analysis (T100) validates call sites match actual procedures.

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
3. **Core requirements only**: Enforce OUT p_Status and OUT p_ErrorMsg, allow flexibility elsewhere (balanced)
4. **Tiered compliance**: Critical procedures (high usage) require strict compliance, low-usage procedures flexible (risk-based)

**User Response**: Strict 100% compliance (Option 1) for ALL procedures. No exceptions - every procedure must have OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500), use standard status codes (-5 to +1), implement proper error handling. Uniform pattern more valuable than accommodating edge cases.

**Decision Impact**: T103 audit reports compliance score with 0 tolerance for missing outputs. T113-T118 refactor all procedures to 100% compliance. SC-003 requires 100% test pass rate validating standard behavior.

---

### Q7: Parameter Prefix Fallback Strategy - What if INFORMATION_SCHEMA query fails?

**Context**: FR-002 requires parameter prefix detection at startup. Fallback needed if database unreachable or permissions insufficient.

**Options**:
1. **Fail startup**: Terminate application if cache can't populate (strict but prevents prefix errors)
2. **Convention fallback**: Use naming conventions (p_ default, in_ for Transfer*/transaction*) if query fails (graceful degradation)
3. **Ask user**: Prompt DBA for database connection fix during startup (interactive but blocks startup)
4. **Offline cache**: Ship application with pre-built cache file, refresh periodically (robust but requires maintenance)

**User Response**: Convention fallback (Option 2) with startup warning logged. Application continues with reduced accuracy (~95% coverage from convention vs 100% from schema query). Better to run with fallback than fail startup completely. Monitoring alerts if fallback activates.

**Decision Impact**: T123 tests both schema query (primary path) and fallback (secondary path). Startup logs cache source (schema vs convention). T104 documents convention rules for fallback accuracy.

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

**User Response**: Comprehensive updates (Option 2) - update all affected documentation as part of Phase 2.5. Documentation is deliverable, not afterthought. T129-T132 dedicated to documentation with 20 hours budgeted. Includes standards template, developer quickstart, DAO XML comments, and completion report.

**Decision Impact**: Part F (T129-T132) expanded to cover all documentation. T131 specifically addresses DAO XML comments (~150 methods × 4 minutes = 10 hours). T132 creates Phase 2.5 completion report for stakeholders.

---

## Summary of Decisions

| Question | Decision | Rationale |
|----------|----------|-----------|
| Q1: Database Source | Production database | Source of truth, test mirrors production |
| Q2: Deployment Strategy | Complete wipe with safety | Clean slate, eliminates legacy code |
| Q3: Testing Scope | Comprehensive coverage | 100% safety net, accepts longer timeline |
| Q4: Priority Determination | Hybrid scoring (usage + compliance) | Balanced approach, addresses critical errors first |
| Q5: Deployment Timing | Off-hours with maintenance window | Minimal user impact, rollback support |
| Q6: Compliance Standards | Strict 100% compliance | Uniform pattern, no exceptions |
| Q7: Parameter Prefix Fallback | Convention fallback with warning | Graceful degradation, ~95% accuracy |
| Q8: Test Isolation | Per-test transactions | Guaranteed isolation, acceptable overhead |
| Q9: Performance Baseline | Before Phase 2.5 begins | Captures pre-refactor state accurately |
| Q10: Documentation Scope | Comprehensive updates | Documentation as deliverable, not afterthought |

---

---

## Session 2: Implementation Details and Edge Cases (2025-10-15)

### Q11: Stored Procedure Versioning - How to handle schema evolution during Phase 2.5?

**Context**: Phase 2.5 takes 15-25 days. Production database may change during this period (hotfixes, emergency procedures).

**Options**:
1. **Schema freeze**: No production changes during Phase 2.5 (simplest but blocks hotfixes)
2. **Version control integration**: Track procedure versions in Git, merge conflicts manually (flexible but complex)
3. **Timestamp-based validation**: Record procedure modified dates, detect drift during deployment (automated detection)
4. **Accept drift**: Deploy anyway, handle conflicts during T122 integration testing (risky but pragmatic)

**User Response**: Timestamp-based validation (Option 3) with conflict detection during T120 deployment. T101 records `LAST_ALTERED` timestamp from INFORMATION_SCHEMA during audit. T120 pre-deployment check compares timestamps - if production changed after T101 audit, abort deployment and flag conflict for manual review. Allows emergency hotfixes without blocking Phase 2.5 progress.

**Decision Impact**: T101 audit includes `LAST_ALTERED` timestamp capture. T120 deployment script pre-flight check queries production timestamps, compares to audit baseline. Conflicts logged with procedure names for manual resolution before proceeding.

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

**Context**: Q7 specified convention fallback, but specific rules not documented. T104 needs explicit convention definition.

**Options**:
1. **Universal p_ prefix**: All parameters use `p_` prefix (simplest but may cause errors for Transfer/Transaction procedures)
2. **Pattern-based detection**: Procedures matching `*Transfer*` or `*Transaction*` use `in_`, others use `p_` (heuristic-based)
3. **Configuration file**: Ship `parameter-conventions.json` mapping procedures to prefixes (explicit but requires maintenance)
4. **First-call learning**: Attempt `p_`, if fails try `in_`, cache successful prefix (adaptive but requires error tolerance)

**User Response**: Pattern-based detection (Option 2) with explicit configuration override. Default rules: `p_` prefix for all parameters EXCEPT procedures matching regex `(Transfer|Transaction|Batch|Multi)` which use `in_` prefix for flow parameters (FromLocation, ToLocation, etc.). Configuration file `parameter-prefix-overrides.json` allows explicit overrides for edge cases (loaded at startup, merged with pattern rules).

**Decision Impact**: T104 documents pattern rules in 00_STATUS_CODE_STANDARDS.md. T123 fallback implementation includes regex pattern matching and configuration file loading. Configuration file structure: `{ "inv_inventory_Special": { "PartID": "p_", "SpecialParam": "sp_" } }` for explicit overrides.

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
4. **Documentation-based**: Rely on procedure comments indicating multi-step (requires documentation discipline)

**User Response**: Pattern detection (Option 2) with manual review for edge cases. T103 audit scans procedure body for multiple DML statements (count INSERT/UPDATE/DELETE). Procedures with ≥2 DML statements flagged as multi-step candidates. Developer reviews flagged procedures, confirms which require transactions (some may be independent operations). T114 refactors confirmed multi-step procedures with BEGIN/COMMIT/ROLLBACK wrappers.

**Decision Impact**: T103 audit report includes "MultiStepCandidate" column (boolean based on DML count). T105 priority matrix includes multi-step flag (affects refactoring complexity estimate). T114 checklist: 10 confirmed multi-step procedures requiring transaction management. T127 tests these 10 procedures with forced rollback.

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
5. **Separate config file**: Untracked `appsettings.Production.json` with credentials (.gitignore protected)

**User Response**: Separate config file (Option 5) for development, environment variables (Option 2) for production deployment. Development: `appsettings.Development.json` (in .gitignore) contains localhost credentials. Production: Application reads connection string from environment variable `MTM_DB_CONNECTION_STRING` if present, falls back to appsettings.json. Deployment script sets environment variable from secure storage. Credentials never committed to Git.

**Decision Impact**: T013 refactors Helper_Database_Variables.GetConnectionString() to check environment variable first. .gitignore updated to exclude appsettings.Development.json and appsettings.Production.json. T121 production deployment checklist includes environment variable configuration step. README.md documents credential management for new developers.

---

### Q24: Static Analysis Tool Selection - Which tool for T124 Helper routing compliance?

**Context**: T124 validates 100% Helper routing (no direct MySQL API usage). Tool choice affects accuracy and automation.

**Options**:
1. **Grep/ripgrep**: Simple text search for "new MySqlConnection", "new MySqlCommand" (fast but may miss dynamic usage)
2. **Roslyn analyzers**: Custom C# code analyzer with IDE integration (accurate but requires development)
3. **PowerShell script**: AST parsing with Get-Content and regex (scriptable and adequate accuracy)
4. **SonarQube rules**: Configure code quality rules to flag violations (enterprise but requires infrastructure)

**User Response**: PowerShell script (Option 3) with pattern matching. Script `Validate-DaoCompliance.ps1`: Scans Data/*.cs files using Select-String for patterns `new MySqlConnection`, `new MySqlCommand`, `MySqlDataAdapter`, `MySqlDataReader`. Excludes Helper_Database_StoredProcedure.cs (allowed location). Outputs CSV report: FileName, LineNumber, Pattern, CodeSnippet. Exit code 0 if zero violations, 1 if violations found. Runs in T124, integrated into CI/CD pipeline (future).

**Decision Impact**: T124 deliverable includes Validate-DaoCompliance.ps1 script and compliance report CSV. Script versioned in `Scripts/` folder for ongoing use. T085 manual validation task uses same script. Future CI/CD integration prevents regression (PR blocked if violations detected).

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

| Question | Decision | Rationale |
|----------|----------|-----------|
| Q11: Schema Evolution | Timestamp-based validation | Detects conflicts, allows emergency hotfixes |
| Q12: Rollback Testing | Audit table inspection | Leverages existing audit trail efficiently |
| Q13: Prefix Conventions | Pattern-based with overrides | Balance between automatic and explicit rules |
| Q14: Compliance Scoring | Multi-factor weighted scoring | Detailed prioritization of violations |
| Q15: Test Data | Seed data scripts | Repeatable, safe, version-controlled |
| Q16: File Fallback Testing | Test DB with manual prod validation | Safe automation + documented prod verification |
| Q17: Threshold Storage | appsettings.json | Adjustable without recompile, restart acceptable |
| Q18: Procedure Files | Individual files by domain | Git-friendly, code review granularity |
| Q19: Test Execution | Hybrid local + CI/CD | Fast feedback + consistent validation |
| Q20: Backup Validation | Count for test, full for prod | Balanced speed vs thoroughness |
| Q21: Multi-Step Detection | Pattern detection + manual review | Automated flagging, human verification |
| Q22: DAO Naming | PascalCase + Async suffix | Standard C# convention, clear intent |
| Q23: Credential Security | Separate config + env vars | Dev-friendly, production-secure |
| Q24: Static Analysis Tool | PowerShell script | Adequate accuracy, scriptable, no infrastructure |
| Q25: Rollback Criteria | Smoke test gated + 15min window | Objective criteria, time-bounded investigation |

---

## Open Questions

*None* - All clarifications resolved during Sessions 1-2 (2025-10-15).

---

**Document Version**: 2.0  
**Last Updated**: 2025-10-15  
**Status**: Complete - All clarifications answered (25 total questions)
