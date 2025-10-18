---
description: Execute comprehensive database layer standardization implementation for MTM WIP Application (002-003 combined)
---

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Overview

This prompt implements the **complete database layer standardization workflow** combining:
- **002-comprehensive-database-layer**: Foundation (Phases 1-2) and DAO refactoring (Phases 3-8)
- **003-database-layer-refresh**: Stored procedure standardization (Phase 2.5 - 41 tasks, T100-T132)

**Branch**: `002-003-database-layer-complete` (current active branch)

**Critical Path**:
1. Phase 1-2: Foundation (DaoResult, Helper refactoring) - **IF NOT COMPLETE**
2. **Phase 2.5: Stored Procedure Standardization (T100-T132) - BLOCKS ALL DAO WORK**
3. Phase 3-8: DAO Implementation (after Phase 2.5 complete)

**Total Scope**: 41 Phase 2.5 tasks + remaining Phase 3-8 tasks = ~150-200 implementation items

---

## Step 1: Load Implementation Context

**REQUIRED**: Read all specification files to understand complete scope.

### Core Specifications

Read these files in order:

1. **002 Specification Files**:
   - `specs/002-comprehensive-database-layer/spec.md` - Original feature specification with 5 user stories
   - `specs/002-comprehensive-database-layer/plan.md` - Implementation plan with Phase 1-8 breakdown
   - `specs/002-comprehensive-database-layer/tasks.md` - Detailed task list (includes Phase 2.5 as lines 850-1150)
   - `specs/002-comprehensive-database-layer/data-model.md` - DaoResult pattern, entities, relationships
   - `specs/002-comprehensive-database-layer/quickstart.md` - Developer guide for DAO patterns
   - `specs/002-comprehensive-database-layer/clarification-questions.md` - 8 resolved Q&A from Session 2025-10-13

2. **003 Refresh Files**:
   - `specs/003-database-layer-refresh/spec.md` - Enhanced specification with Session 3 additions (SC-011 through SC-018)
   - `specs/003-database-layer-refresh/plan.md` - Phase 2.5 detailed breakdown with Session 3 enhancements
   - `specs/003-database-layer-refresh/tasks.md` - Task organization and checklist references
   - `specs/003-database-layer-refresh/UPDATE-SUMMARY-SESSION-3.md` - Latest changes summary

3. **Contract Files** (if needed for schema validation):
   - `specs/002-comprehensive-database-layer/contracts/dao-result-schema.json`
   - `specs/002-comprehensive-database-layer/contracts/parameter-schema.json`
   - `specs/002-comprehensive-database-layer/contracts/stored-procedure-contract.json`

### Implementation State Files

4. **Current State**:
   - `AGENTS.md` - Project structure, setup commands, development workflow
   - `.github/copilot-instructions.md` - Active coding standards and patterns
   - Review latest commits on `002-003-database-layer-complete` branch to understand completed work

---

## Step 2: Determine Current Phase

**Check which phases are complete** to avoid duplicate work:

### Phase 1-2 Completion Check

Verify these foundation files exist and are complete:

- [ ] `Models/Model_DaoResult.cs` - Base DaoResult class exists
- [ ] `Models/Model_DaoResult_Generic.cs` - Generic DaoResult<T> exists  
- [ ] `Models/Model_ParameterPrefixCache.cs` - Cache structure exists
- [ ] `Program.cs` - Contains INFORMATION_SCHEMA parameter cache initialization
- [ ] `Helpers/Helper_Database_StoredProcedure.cs` - Four async execution methods refactored
- [ ] `Tests/Integration/BaseIntegrationTest.cs` - Test infrastructure exists

**If any items missing**: Execute Phase 1-2 tasks (T001-T013) before proceeding.

**If all items present**: Skip to Phase 2.5.

### Phase 2.5 Completion Check

Verify stored procedure standardization:

- [ ] `Database/UpdatedStoredProcedures/` - Contains 60-100+ refactored .sql files
- [ ] `specs/003-database-layer-refresh/Documentation-Update-Matrix.md` - Exists with status tracking
- [ ] `Database/STORED_PROCEDURE_CALLSITES.csv` - Discovery report exists
- [ ] `Database/COMPLIANCE_REPORT.csv` - Audit results exist
- [ ] Test database deployed with refactored procedures
- [ ] All 280+ integration tests passing (100% pass rate)

**If Phase 2.5 incomplete**: This is the **CRITICAL BLOCKING PHASE** - must complete before any DAO work.

**If Phase 2.5 complete**: Proceed to Phase 3-8 DAO refactoring.

---

## Step 3: Phase 2.5 - Stored Procedure Standardization (T100-T132)

**CRITICAL**: This phase BLOCKS all DAO refactoring. Complete ALL 41 tasks before Phase 3.

### Part A: Discovery and Analysis (T100-T106a)

**Objective**: Discover and audit all stored procedures, generate compliance reports.

**Checklist Reference**: `specs/003-database-layer-refresh/task-helpers/discovery-quality.md`

#### T100: Discover All Stored Procedure Call Sites (2 hours)

Execute search pattern to find all stored procedure calls:

```powershell
# Search for stored procedure execution patterns
Get-ChildItem -Path . -Include *.cs -Recurse | 
    Select-String -Pattern "ExecuteNonQuery|ExecuteScalar|ExecuteDataTable|ExecuteReader|CommandType.StoredProcedure|CALL |MySqlCommand" |
    Export-Csv -Path "Database/STORED_PROCEDURE_CALLSITES.csv" -NoTypeInformation
```

**Deliverable**: `Database/STORED_PROCEDURE_CALLSITES.csv` (~220 call sites)

**Validation**: Run discovery quality checklist items 1-5

---

#### T101: Extract Database Schema (1 hour)

Query INFORMATION_SCHEMA for complete schema snapshot:

```sql
-- Execute against mtm_wip_application database
SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_SCHEMA = 'mtm_wip_application';

SELECT * FROM INFORMATION_SCHEMA.PARAMETERS 
WHERE SPECIFIC_SCHEMA = 'mtm_wip_application';

SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = 'mtm_wip_application';

SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'mtm_wip_application';
```

**Deliverable**: `Database/database-schema-snapshot.json` (4 sections)

**Validation**: Run discovery quality checklist items 6-10

---

#### T102: Generate Individual SQL Files (3 hours)

Extract each stored procedure to version-controlled .sql file:

```powershell
# For each procedure from T101, execute:
# SHOW CREATE PROCEDURE <procedureName>
# Save to Database/UpdatedStoredProcedures/<procedureName>.sql
```

**Deliverable**: ~70 .sql files organized by domain (inventory/, transactions/, users/, etc.)

**Validation**: Run discovery quality checklist items 11-15

---

#### T103: Audit Procedures + CSV Transaction Analysis (7 hours)

**Dual Output Task**: Compliance audit + transaction pattern detection

Compare each procedure against `Database/CurrentStoredProcedures/00_STATUS_CODE_STANDARDS.md`:

- Check OUT p_Status INT parameter exists
- Check OUT p_ErrorMsg VARCHAR(500) parameter exists  
- Validate status code logic (1=success, 0=no data, -1 to -5=errors)
- Validate parameter naming (PascalCase with p_ prefix)
- **NEW**: Count INSERT/UPDATE/DELETE statements (detect multi-step operations)
- **NEW**: Analyze call graph for nested procedure calls
- **NEW**: Classify pattern (single-step/multi-step/batch/reporting)

**Output 1**: `Database/COMPLIANCE_REPORT.csv` with columns:
- ProcedureName, HasStatus, HasErrorMsg, StatusLogicCorrect, ParameterPrefixConsistent, ComplianceScore (0-100%)

**Output 2**: `Database/procedure-transaction-analysis.csv` with columns:
- ProcedureName, DetectedPattern, RecommendedStrategy, Confidence, Rationale, DeveloperCorrection (empty), RefactoringNotes (empty)

**Deliverable**: Two CSV files for compliance and transaction analysis

**Validation**: Run discovery quality checklist items 16-20 + CSV transaction analysis checklist items 1-10

---

#### T104: Document Parameter Prefix Conventions (2 hours)

Analyze parameter prefixes to establish fallback conventions:

**Deliverable**: `Database/parameter-prefix-conventions.md` documenting:
- Standard Prefix: p_ (70% of procedures)
- Multi-Step Operations: in_ (Transfer*, transaction*)
- Output Parameters: o_ (5% rare)
- Fallback Convention: p_ for CRUD, in_ for Transfer*/transaction*

**Validation**: Run discovery quality checklist items 21-25

---

#### T105: Create Refactoring Priority Matrix (2 hours)

Cross-reference T100 call site count with T103 compliance scores:

Priority = (CallSiteCount × 0.4) + (ComplianceDeficiency × 0.6)

**Deliverable**: `Database/refactoring-priority.csv` with top 20 critical procedures flagged

**Validation**: Run discovery quality checklist items 26-30

---

#### T106: Generate Test Coverage Matrix (3 hours)

Map existing integration tests to procedures:

**Deliverable**: `Database/test-coverage-matrix.csv` showing current ~30% test coverage

**Validation**: Run discovery quality checklist items 31-35

---

#### T106a: CSV Review and Correction (1-2 days, GATES T113)

**CRITICAL**: Git-based PR workflow for developer validation of transaction analysis CSV.

**Process**:
1. T103 commits `procedure-transaction-analysis.csv` to `Database/AnalysisReports/`
2. Tech lead assigns procedure domains to developers:
   - Developer 1: Inventory procedures
   - Developer 2: User/Transaction/Role procedures  
   - Developer 3: Master Data procedures
3. Each developer reviews assigned procedures (4-6 hours):
   - Fill `DeveloperCorrection` column if automated detection incorrect
   - Leave blank if automated recommendation correct
   - Add rationale in `RefactoringNotes` column
4. Create PR with domain-specific corrections
5. Peer developer reviews corrections
6. Merge after approval

**Deliverable**: Corrected CSV with all procedures reviewed and validated

**Validation**: Run CSV transaction analysis checklist items 11-20

**GATE**: T113 cannot start until this CSV review complete and merged.

---

#### T106b: Automated Stored Procedure User Validation (Variable Duration)

**CRITICAL GATE**: This task processes ALL stored procedures based on user instructions in the CSV and MUST complete 100% before ANY refactoring begins.

**Checklist Reference**: `specs/003-database-layer-refresh/task-helpers/stored-procedure-user-validation-quality.md` (75 procedures)

**Prerequisites**:
- T106a complete (CSV reviewed and corrected by developers)
- `procedure-transaction-analysis.csv` has NeedsAttention column with False default
- All developer corrections and notes captured in CSV

**Process**:

1. **Load CSV and Checklist**:
   ```powershell
   $csv = Import-Csv "Database\procedure-transaction-analysis.csv"
   $checklistPath = "specs\003-database-layer-refresh\checklists\stored-procedure-user-validation-quality.md"
   ```

2. **For Each Stored Procedure** (iterate through all 75 rows):
   
   **Step A: Read Row Data**
   ```powershell
   $procName = $row.ProcedureName
   $devCorrection = $row.DeveloperCorrection.Trim()
   $notes = $row.Notes.Trim()
   $needsAttention = $row.NeedsAttention
   $callHierarchy = $row.CallHierarchy | ConvertFrom-Json
   ```
   
   **Step B: Apply Validation Logic**
   
   - **Case 1**: Both `DeveloperCorrection` AND `Notes` have content
     - Action: Follow instructions from both fields sequentially
     - Execute corrections specified in DeveloperCorrection
     - Apply additional context/validation from Notes
     - Test the stored procedure to verify corrections
   
   - **Case 2**: Only `DeveloperCorrection` has content
     - Action: Follow those specific instructions precisely
     - Make required changes to stored procedure
     - Test changes against integration tests
   
   - **Case 3**: Only `Notes` has content
     - Action: Follow instructions in Notes field
     - May be testing requirements, validation steps, or context
   
   - **Case 4**: Neither has content BUT `NeedsAttention = True`
     - Action: Validate/test stored procedure using CallHierarchy
     - Analyze call chain: Event → Method → DAO → SP → Tables → UI Update
     - Verify procedure parameters match DAO expectations
     - Test success and failure paths
     - If CallHierarchy is inconclusive: **STOP and ASK for clarification**
   
   - **Case 5**: Neither has content AND `NeedsAttention = False`
     - Action: Mark as passing (procedure validated during T106a review)
     - Check off in checklist
     - Move to next procedure

   **Step C: Update Checklist**
   ```powershell
   # Find procedure checkbox in stored-procedure-user-validation-quality.md
   # Change: - [ ] **ProcedureName**
   # To:     - [x] **ProcedureName**
   # Add validation notes under "Validation Notes" for that procedure
   ```

3. **Handle Ambiguous Cases**:
   - If CallHierarchy data is incomplete or contradictory: **STOP**
   - Log the specific issue and procedure name
   - Request human clarification with context
   - Do NOT proceed until clarification received

4. **Generate Validation Report**:
   - Total procedures processed: 75
   - Corrections applied: X
   - Tests executed: Y
   - Failures requiring attention: Z
   - Checklist completion: Must be 100%

**Deliverable**: 
- Updated checklist with all 75 procedures checked off
- Validation report with detailed results
- Any corrected stored procedures saved to UpdatedStoredProcedures/
- Test results for all validated procedures

**Validation**: Run stored procedure user validation checklist (75 items) - must be 100% complete

**CRITICAL GATE**: Phase 2.5C (T113-T118 refactoring) CANNOT start until:
- [ ] All 75 stored procedures validated
- [ ] stored-procedure-user-validation-quality.md shows 75/75 complete
- [ ] All corrections applied and tested
- [ ] Zero ambiguous cases remaining (all clarifications resolved)

---

**Part A Checkpoint**: Run complete discovery quality checklist validation (35 items). All discovery reports generated, CSV reviewed and corrected.

---

### Part B: Test Implementation (T107-T112)

**Objective**: Create comprehensive integration test suite (280+ tests) covering all procedures.

**Checklist Reference**: `specs/003-database-layer-refresh/task-helpers/testing-quality.md`

#### T107: Create BaseIntegrationTest + Verbose Helper (5 hours)

Implement base test class with transaction management and **verbose failure diagnostics**:

**Key Component - AssertProcedureResult Helper**:

```csharp
protected void AssertProcedureResult(
    DaoResult result, 
    bool expectedSuccess, 
    string expectedMessagePattern = null)
{
    if (result.IsSuccess != expectedSuccess)
    {
        var diagnostic = new {
            Exception = result.Exception?.ToString(),
            Parameters = TestCurrentParameters,  // Captured from test
            Expected = new { 
                IsSuccess = expectedSuccess, 
                MessagePattern = expectedMessagePattern 
            },
            Actual = new { 
                IsSuccess = result.IsSuccess, 
                Message = result.Message 
            },
            ExecutionTimeMs = TestCurrentExecutionTime,
            DatabaseState = CaptureTableRowCounts(),
            TestMethod = TestContext.TestName,
            Timestamp = DateTime.Now
        };
        
        Assert.Fail($"Procedure result assertion failed.\n" +
            $"{JsonSerializer.Serialize(diagnostic, new JsonSerializerOptions { WriteIndented = true })}");
    }
}
```

**Deliverable**: `Tests/Integration/BaseIntegrationTest.cs` with verbose diagnostics

**Validation**: Run testing quality checklist items 1-10 + verbose test failure checklist items 1-15

---

#### T108: Generate Inventory Procedure Tests (12 hours)

Create 4-test pattern for 15 inventory procedures = 60 test methods:

1. Success with data → returns DaoResult.Success, Data populated
2. Success no data → returns DaoResult.Success, Data empty  
3. Validation error → returns DaoResult.Failure, clear error message
4. Database error → returns DaoResult.Failure, exception logged

**All tests use AssertProcedureResult()** for structured JSON diagnostics on failure.

**Deliverable**: `Tests/Integration/InventoryProcedures_IntegrationTests.cs` (~800 LOC)

**Validation**: Run testing quality checklist items 11-25 + verbose test failure checklist items 16-25

---

#### T109: Generate Transaction/User/Role Tests (15 hours)

Create tests for 20 procedures × 4 tests = 80 test methods.

**Deliverable**: 3 test files:
- `TransactionProcedures_IntegrationTests.cs` (~400 LOC)
- `UserProcedures_IntegrationTests.cs` (~600 LOC)
- `RoleProcedures_IntegrationTests.cs` (~300 LOC)

**Validation**: Run testing quality checklist items 26-40

---

#### T110: Generate Master Data Tests (12 hours)

Create tests for 20 master data procedures × 4 tests = 80 test methods.

**Deliverable**: `MasterDataProcedures_IntegrationTests.cs` (~800 LOC)

**Validation**: Run testing quality checklist items 41-55

---

#### T111: Generate Logging/Quick Button Tests (10 hours)

Create tests for 15 procedures × 4 tests = 60 test methods.

**Deliverable**: 2 test files:
- `LoggingProcedures_IntegrationTests.cs` (~400 LOC)
- `QuickButtonProcedures_IntegrationTests.cs` (~400 LOC)

**Validation**: Run testing quality checklist items 56-70

---

#### T112: Validate Test Isolation (4 hours)

Run all tests sequentially, then in parallel (4 threads), compare results.

**Success Criteria**: 100% pass rate in both modes, zero leftover test data.

**Deliverable**: Test isolation validation report

**Validation**: Run testing quality checklist items 71-80

---

**Part B Checkpoint**: 280+ integration tests created, 100% pass rate validated, verbose diagnostics confirmed. Run testing quality checklist (80 items).

---

### Part C: Stored Procedure Refactoring (T113c/d, T113-T118)

**Objective**: Refactor all non-compliant procedures to 100% standards compliance with concurrent documentation.

**Checklist References**: 
- `specs/003-database-layer-refresh/task-helpers/refactoring-quality.md`
- `specs/003-database-layer-refresh/task-helpers/developer-role-quality.md`
- `specs/003-database-layer-refresh/task-helpers/parameter-prefix-maintenance-quality.md`

#### T113c: Developer Role Infrastructure (4 hours)

**Prerequisite for T113d**: Create database schema and user management UI.

**Database Schema**:
```sql
-- Add Developer flag to sys_user table
ALTER TABLE sys_user ADD COLUMN IsDeveloper BOOLEAN DEFAULT FALSE AFTER IsAdmin;

-- Create parameter prefix override table
CREATE TABLE sys_parameter_prefix_override (
    OverrideID INT AUTO_INCREMENT PRIMARY KEY,
    ProcedureName VARCHAR(100) NOT NULL,
    ParameterName VARCHAR(100) NOT NULL,
    DetectedPrefix VARCHAR(10),
    OverridePrefix VARCHAR(10) NOT NULL,
    Confidence DECIMAL(3,2),
    Reason VARCHAR(500),
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedBy INT,
    ModifiedDate DATETIME ON UPDATE CURRENT_TIMESTAMP,
    IsActive BOOLEAN DEFAULT TRUE,
    UNIQUE KEY unique_proc_param (ProcedureName, ParameterName),
    FOREIGN KEY (CreatedBy) REFERENCES sys_user(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES sys_user(UserID)
);
```

**User Management UI**:
- Add Developer checkbox to user management form
- Enforce prerequisite: Developer requires IsAdmin = TRUE
- Update role hierarchy documentation

**Deliverable**: Schema migration script, updated user management form, role validation helpers

**Validation**: Run developer role quality checklist items 1-25

---

#### T113d: Parameter Prefix Maintenance Form (8 hours)

**Developer Tools UI**: Create Settings Form → Development → Parameter Prefix Management

**UserControl**: `Control_Settings_ParameterPrefixMaintenance`

**Features**:
- DataGridView: ProcedureName, ParameterName, DetectedPrefix, OverridePrefix, Confidence, Reason
- Buttons: Add Override, Edit Override, Delete Override, Save All, Reload Cache, Export, Import
- Audit Log Panel: Recent changes with CreatedBy/ModifiedDate
- Filter Controls: Search by procedure name, filter by confidence level
- Role Validation: Visible only to IsAdmin=TRUE AND IsDeveloper=TRUE

**Deliverable**: Fully functional maintenance form with database persistence

**Validation**: Run parameter prefix maintenance checklist items 1-30

---

#### T113: Refactor Top 20 Priority Procedures (24 hours with concurrent docs)

**For each procedure** (1.2 hours average including documentation):

1. Add `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)` if missing
2. Standardize parameter prefixes (p_ for CRUD, in_ for multi-step per corrected CSV)
3. Implement proper error handling (SET p_Status=-1, SET p_ErrorMsg='...')
4. Add success status logic (SET p_Status=1 for data, SET p_Status=0 for no data)
5. **Concurrent Documentation** (tracked in Documentation-Update-Matrix.md):
   - ☐ Update procedure header comments
   - ☐ Update DAO XML documentation for calling method
   - ☐ Update 00_STATUS_CODE_STANDARDS.md if new pattern
   - ☐ Update quickstart.md if commonly used procedure
6. Save to `Database/UpdatedStoredProcedures/<name>.sql`
7. Mark documentation checkboxes ✅ in matrix

**Deliverable**: 20 refactored .sql files + concurrent documentation updates

**Validation**: Run refactoring quality checklist items 1-30

---

#### T114: Refactor Remaining Inventory Procedures (12 hours with concurrent docs)

Apply same refactoring workflow to ~10 remaining inventory procedures.

**Deliverable**: 10 refactored .sql files + documentation updates

**Validation**: Run refactoring quality checklist items 31-50

---

#### T115: Refactor User/Role Procedures (18 hours with concurrent docs)

Apply workflow to ~15 user/role management procedures.

**Deliverable**: 15 refactored .sql files + documentation updates

**Validation**: Run refactoring quality checklist items 51-70

---

#### T116: Refactor Master Data Procedures (24 hours with concurrent docs)

Apply workflow to ~20 master data procedures.

**Deliverable**: 20 refactored .sql files + documentation updates

**Validation**: Run refactoring quality checklist items 71-90

---

#### T117: Refactor Logging/Quick Button/System Procedures (15 hours)

Apply workflow to ~15 logging/quick button/system procedures.

**Deliverable**: 15 refactored .sql files

**Validation**: Run refactoring quality checklist items 91-110

---

#### T118: Add Explicit Transaction Management (15 hours)

Identify ~10 multi-step procedures requiring transaction safety:

```sql
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Transaction failed. All changes rolled back.';
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Step 1: Deduct from source
    -- Step 2: Add to destination
    -- Step 3: Log transaction
    
    COMMIT;
    SET p_Status = 1;
    SET p_ErrorMsg = 'Transfer completed successfully';
END;
```

**Deliverable**: 10 refactored .sql files with explicit transactions

**Validation**: Run refactoring quality checklist items 111-120

---

**Part C Checkpoint**: All ~90 procedures refactored to 100% compliance. Run complete refactoring quality checklist (120 items) + developer role checklist (25 items) + parameter prefix maintenance checklist (30 items).

---

### Part D: Database Deployment (T119-T121, T119b/c/d/e)

**Objective**: Deploy refactored procedures to test and production databases safely with schema drift reconciliation.

**Checklist References**: 
- `specs/003-database-layer-refresh/task-helpers/deployment-quality.md`
- `specs/003-database-layer-refresh/task-helpers/schema-drift-quality.md`

#### T119: Create Deployment Script (6 hours)

Build PowerShell script with environment safety checks:

**Key Features**:
- Environment validation (Test vs Production)
- 10-second confirmation for production wipes
- Backup creation before deployment
- DROP all existing procedures
- Install all procedures from UpdatedStoredProcedures/
- Validate installation (procedure count)

**Deliverable**: `Database/Deploy-StoredProcedures.ps1`

**Validation**: Run deployment quality checklist items 1-15

---

#### T119b: Re-audit for Schema Drift (2 hours)

**CRITICAL**: Detect production changes during Phase 2.5 implementation period.

**Process**:
1. Execute T101 audit steps against **current production** (fresh timestamp)
2. Compare re-audit results to baseline audit from Phase 2.5 start
3. Identify drift: procedures added, modified, or deleted since baseline
4. **Categorize** each drifted procedure:
   - **Category A - Independent Hotfix**: Production change unrelated to Phase 2.5 (preserve logic, apply standards separately)
   - **Category B - Conflicting Change**: Production change affects same procedure being refactored (manual three-way merge required)
   - **Category C - New Procedure**: Procedure added during Phase 2.5 (full refactoring required)
5. Generate drift report CSV

**Deliverable**: `Database/schema-drift-report.csv` with Category A/B/C classification

**Validation**: Run schema drift quality checklist items 1-20

---

#### T119c: Refactor Category A Hotfixes (2-4 hours)

**Process**:
1. Extract Category A procedures from **current production** (not baseline)
2. Apply 00_STATUS_CODE_STANDARDS.md template (OUT params, error handling, prefixes)
3. **Preserve** all production business logic changes
4. Document hotfix origin in header: "HOTFIX: Applied during Phase 2.5, standardized YYYY-MM-DD"
5. Save to UpdatedStoredProcedures/ with _hotfix suffix for tracking
6. Test with integration tests

**Deliverable**: Category A procedures refactored (typically 2-5 procedures)

**Validation**: Run schema drift quality checklist items 21-35

---

#### T119d: Merge Category B Conflicts (2-4 hours)

**Process**:
1. For each Category B procedure, gather **three versions**:
   - **Baseline**: Original from Phase 2.5 start (T102 extraction)
   - **Refactored**: Phase 2.5 standardized version (from T113-T118)
   - **Production**: Current production with hotfix changes
2. Perform **three-way merge** using diff tool (VS Code, Beyond Compare)
3. Merge strategy:
   - Keep Phase 2.5 standardization (OUT params, error handling, prefixes)
   - Integrate production business logic changes
   - Resolve conflicts favoring production business logic
4. Document merge decisions in conflict resolution log
5. Test merged procedures thoroughly

**Deliverable**: Category B procedures merged (typically 1-3 procedures)

**Validation**: Run schema drift quality checklist items 36-50

---

#### T119e: Refactor Category C New Procedures (2-4 hours)

**Process**:
1. Extract Category C procedures from current production
2. Run compliance audit (T103 checks) to establish baseline score
3. Apply full refactoring workflow (OUT params, prefixes, error handling, docs)
4. Create integration tests (4-test pattern from T108-T111)
5. Update Documentation-Update-Matrix.md with new procedure entries
6. Save to UpdatedStoredProcedures/ with standard organization

**Deliverable**: Category C procedures refactored (typically 1-4 procedures)

**Validation**: Run schema drift quality checklist items 51-65

---

#### T120: Test Database Deployment (4 hours)

Deploy to `mtm_wip_application_winforms_test` with post-reconciliation procedure set:

**Process**:
1. Run `Deploy-StoredProcedures.ps1 -Environment Test`
2. **Deployment includes**: Refactored baseline (T113-T118) + Category A hotfixes (T119c) + Category B merged (T119d) + Category C new (T119e)
3. Execute all integration tests (T108-T111) against test database
4. Review deployment logs for errors
5. Manually test 5 high-priority procedures

**Success Criteria**: 100% test pass rate, no deployment errors, drift procedures integrated

**Deliverable**: Test deployment validation report with drift reconciliation confirmation

**Validation**: Run deployment quality checklist items 16-35

---

#### T121: Production Deployment (3 hours with monitoring)

Deploy to `mtm_wip_application` after DBA approval:

**Prerequisites**:
- DBA reviews deployment plan + drift reconciliation report
- Full backup created and restore tested
- Rollback plan documented
- Maintenance window scheduled (off-hours, 30 minutes)

**Process**:
1. Notify users of maintenance window
2. Stop application services
3. Create fresh backup immediately before deployment
4. Run `Deploy-StoredProcedures.ps1 -Environment Production`
5. Execute smoke tests (5 critical procedures)
6. Restart application services
7. Monitor error logs for 1 hour post-deployment

**Success Criteria**: All smoke tests pass, zero errors in monitoring window

**Rollback Plan**: Restore from backup (15-minute RTO)

**Deliverable**: Production deployment validation report

**Validation**: Run deployment quality checklist items 36-55

---

**Part D Checkpoint**: Test and production deployments complete, drift reconciliation integrated, 100% procedure standardization. Run deployment quality checklist (55 items) + schema drift quality checklist (65 items).

---

### Part E: End-to-End Integration Testing (T122-T128, T124a)

**Objective**: Validate refactored database layer works correctly in application context.

**Checklist References**:
- `specs/003-database-layer-refresh/task-helpers/integration-quality.md`
- `specs/003-database-layer-refresh/task-helpers/roslyn-analyzer-quality.md`

#### T122: Execute All Integration Tests (2 hours)

Re-run complete test suite (T108-T111) against deployed database:

**Success Criteria**: 100% pass rate (same as pre-deployment)

**Regression Detection**: Any failures indicate behavior changes requiring investigation

**Deliverable**: Integration test results report

**Validation**: Run integration quality checklist items 1-15

---

#### T123: Test Parameter Prefix Cache + Retry Strategy (4 hours)

**Success Path Testing**:
1. Enable Service_DebugTracer detailed logging
2. Launch application (triggers Program.cs cache initialization)
3. Review logs for cache population messages
4. Verify cache contains all 70+ procedures with correct prefixes

**Retry Dialog Testing**:
1. Block INFORMATION_SCHEMA access (firewall/permissions)
2. Launch application
3. Verify MessageBox appears with clear error message
4. Test "Retry" button (3 attempts with connection retries)
5. Test "Quit" button (clean termination, no fallback)
6. Verify cache loads from sys_parameter_prefix_override for stored overrides

**Success Criteria**: Cache populates <200ms on success, retry dialog within 5 seconds on failure, 3 attempts before termination

**Deliverable**: Startup validation report with retry dialog screenshots

**Validation**: Run integration quality checklist items 16-30

---

#### T124a: Develop Roslyn Analyzer (2-3 hours)

**PREREQUISITE for T124**: Create custom analyzer for database access compliance.

**Project Setup**:
- Create `MTM.CodeAnalysis.DatabaseAccess` class library (.NET Standard 2.0)
- Target Roslyn SDK compatibility

**Diagnostic Rules** (4 rules):
- **MTM001**: Direct MySqlConnection usage → "Use Helper_Database_StoredProcedure.ExecuteDataTableWithStatus()"
- **MTM002**: Direct MySqlCommand usage → "Use Helper_Database_StoredProcedure for stored procedure execution"
- **MTM003**: Inline SQL detected → "Only stored procedures permitted"
- **MTM004**: Missing p_Status/p_ErrorMsg validation → "Must validate stored procedure status"

**Code Fix Providers**:
- Implement for MTM001/MTM002 with "Convert to Helper" quick action

**Severity Configuration**:
- **v1.0.0**: All rules as Warning (educational phase)
- **v2.0.0**: MTM001/MTM002/MTM003 as Error, MTM004 as Warning

**NuGet Package**:
- Build and publish to internal feed or local source
- Add to MTM_Inventory_Application.csproj: `<PackageReference Include="MTM.CodeAnalysis.DatabaseAccess" Version="1.0.0" />`

**Deliverable**: NuGet package + integration documentation in quickstart.md

**Validation**: Run roslyn analyzer quality checklist items 1-30

---

#### T124: Validate DAO Helper Routing + Analyzer (3 hours)

**Process**:
1. **Run Roslyn analyzer** (from T124a) against entire solution
2. Review analyzer output for diagnostic violations (MTM001-MTM004)
3. Search Data/ for `MySqlConnection`, `MySqlCommand`, `MySqlDataAdapter` patterns
4. Review T100 callsite-inventory.csv for non-Helper patterns
5. Generate violation report with file locations and suggested fixes

**Success Criteria**: Zero violations, 100% Helper routing compliance

**Deliverable**: Compliance validation report with analyzer output

**Validation**: Run integration quality checklist items 31-45 + roslyn analyzer checklist items 31-45

---

#### T125: Test Error Logging Recursive Prevention (2 hours)

Force error logging failure to validate file fallback:

**Process**:
1. Rename log_error table to simulate unavailability
2. Trigger database error (force constraint violation)
3. Verify error logged to `Logs/database-errors.log` file
4. Check application stability (no hang/crash)
5. Restore log_error table, verify database logging resumes

**Success Criteria**: Fallback activates, file log complete, application stable

**Deliverable**: Recursive prevention validation report

**Validation**: Run integration quality checklist items 46-55

---

#### T126: Manual Testing of All Forms (8 hours)

Manually test all 25 forms to validate workflows:

**Key Workflows**:
- MainForm: Login, inventory grid load, part search
- Control_Inventory: Add inventory (IN transaction)
- Control_Transactions_Remove: Remove inventory (OUT transaction)
- Control_Transactions_Transfer: Transfer between locations (TRANSFER, 3-step atomic)
- Control_Settings: Update preferences, change password
- Control_QuickButtons: Add/remove/reorder buttons
- Control_History: View history, filter by date/user/part
- Control_Reports: Generate Excel reports, print grids

**Success Criteria**: All workflows complete, data persists, error dialogs user-friendly

**Deliverable**: Manual testing checklist with pass/fail status

**Validation**: Run integration quality checklist items 56-70

---

#### T127: Validate Transaction Rollback (4 hours)

Test explicit transaction management by forcing mid-operation failures:

**Test Scenarios**:
1. TransferInventoryAsync: Force step 2 failure → verify step 1 rolled back
2. Batch removal: Force failure at item #50 → verify all 100 items still present
3. User creation with role: Force role assignment failure → verify user not created

**Success Criteria**: Zero orphaned records in all scenarios

**Deliverable**: Transaction rollback validation report

**Validation**: Run integration quality checklist items 71-80

---

#### T128: Performance Benchmark Comparison (4 hours)

Execute benchmark suite for 10 key operations (100 times each):

**Operations**: Inventory add, search, transfer, user auth, transaction log retrieval, part search, location get, operation get, quick button get, error log retrieval

**Metrics**: Mean, median, P95, P99 execution times

**Success Criteria**: All operations within ±5% of pre-refactor baseline

**Deliverable**: Performance comparison report with charts

**Validation**: Run integration quality checklist items 81-95

---

**Part E Checkpoint**: End-to-end integration validated, Roslyn analyzer enforcing compliance, 100% manual testing complete. Run integration quality checklist (95 items) + roslyn analyzer checklist (45 items).

---

### Part F: Documentation and Knowledge Transfer (T129-T132)

**Objective**: Update all documentation with concurrent tracking, validate completeness, document lessons learned.

**Checklist References**:
- `specs/003-database-layer-refresh/task-helpers/documentation-quality.md`
- `specs/003-database-layer-refresh/task-helpers/documentation-matrix-quality.md`

#### T129: Generate Documentation-Update-Matrix.md (2 hours)

**Create tracking matrix** for concurrent documentation updates during T113-T118:

**Matrix Structure**:
- Markdown table with columns: File Path (link), Status (⬜/🔄/✅/⚠️), Last Updated, Assigned To, Notes
- Rows for all documentation targets:
  - **Per-procedure rows**: 70+ procedures × 2 files (header comments + DAO XML) = ~140 rows
  - **Standards documents**: 00_STATUS_CODE_STANDARDS.md, DEVELOPMENT-STANDARDS.md
  - **Quickstart sections**: quickstart.md updates per DAO domain

**Validation Script**:
```powershell
# Check matrix completeness
# Exit code 0 = 100% complete (all ✅)
# Exit code 1 = incomplete (any ⬜/🔄/⚠️)
```

**Deliverable**: `specs/003-database-layer-refresh/Documentation-Update-Matrix.md` with ~145 trackable items + validation script

**Update During T113-T118**: Mark checkboxes ✅ as procedures refactored

**Validation**: Run documentation quality checklist items 1-20 + documentation matrix checklist items 1-25

---

#### T130: Bulk Documentation Updates (8 hours)

**Complete any remaining items** from Documentation-Update-Matrix.md:

**Process**:
1. Run validation script to identify incomplete items (⬜/🔄/⚠️)
2. Prioritize core procedures first
3. Complete updates:
   - **Procedure header comments**: Purpose, parameters, return codes, examples
   - **DAO XML documentation**: Summary, param, returns, exceptions
   - **Standards updates**: Add lessons learned to 00_STATUS_CODE_STANDARDS.md
   - **Quickstart sections**: DAO-specific examples
4. Update matrix status to ✅
5. Re-run validation script until exit code 0

**Deliverable**: 100% complete Documentation-Update-Matrix.md (all ✅)

**Validation**: Run documentation quality checklist items 21-45 + documentation matrix checklist items 26-50

---

#### T131: Validate Matrix Completeness (1 hour)

**Final validation** before approval:

**Process**:
1. Execute validation script
2. Generate completeness report: total items, completed items, completion %, incomplete list
3. If <100%: Loop back to T130
4. If 100%: Generate final approval report, archive matrix

**Success Criteria**: Exit code 0, all 145+ items marked ✅

**Deliverable**: Documentation completeness validation report

**Validation**: Run documentation quality checklist items 46-60 + documentation matrix checklist items 51-65

---

#### T132: Phase 2.5 Completion Report (4 hours)

**Compile comprehensive reference document** covering all refactoring work:

**Report Sections**:
1. **Executive Summary**: 2-paragraph overview including drift reconciliation
2. **Metrics**:
   - Stored Procedures Refactored: 70+
   - Schema Drift Procedures: Category A/B/C counts
   - Compliance Score: 100%
   - Integration Tests: 280+ methods, 100% pass rate
   - Parameter Prefix Errors: 0 (baseline ~20/month)
   - Performance Variance: ±3% (within ±5% tolerance)
   - Database Tickets: 92% reduction (50/month → 4/month)
   - Roslyn Analyzer Violations: 0
3. **Success Criteria Results**: SC-001 through SC-018 actual outcomes
4. **Timeline**: Actual vs estimated
5. **Lessons Learned**: Top 15 insights
6. **Next Steps**: Phase 3-8 unblocked
7. **Appendices**:
   - **Appendix A**: Schema drift reconciliation report
   - **Appendix B**: CSV transaction analysis summary
   - **Appendix C**: Roslyn analyzer integration guide

**Deliverable**: `specs/003-database-layer-refresh/phase-2.5-completion-report.md` with appendices

**Validation**: Run documentation quality checklist items 61-80

---

**Part F Checkpoint**: Documentation 100% complete, validation passed, Phase 2.5 implementation guide published. Run documentation quality checklist (80 items) + documentation matrix checklist (65 items).

---

## Step 4: Phase 3-8 DAO Refactoring (After Phase 2.5 Complete)

**PREREQUISITE**: Phase 2.5 must be 100% complete before starting DAO work.

**Phases Overview**:
- **Phase 3**: User Story 1 - Developer Adds New Database Operation (Dao_System, Dao_ErrorLog)
- **Phase 4**: User Story 2 - Reliable Database Operations (Dao_Inventory, Dao_Transactions, Dao_History)
- **Phase 5**: User Story 3 - Troubleshoot Database Issues (Dao_User, Dao_Part)
- **Phase 6**: User Story 4 - Schema Consistency (Dao_Location, Dao_Operation, Dao_ItemType, Dao_QuickButtons)
- **Phase 7**: User Story 5 - Performance Monitoring (remaining Forms/Controls async migration)
- **Phase 8**: Polish & Cross-Cutting Concerns (documentation, validation, cleanup)

**Note**: Phase 2.5 supersedes stored procedure work in these phases. Focus on:
- DAO method refactoring (async, DaoResult pattern)
- Helper routing (no direct MySQL API)
- Forms/Controls async migration
- Service integration

**Detailed tasks**: Refer to `specs/002-comprehensive-database-layer/tasks.md` lines 1-850 and 1150-end for Phase 3-8 task breakdowns.

---

## Step 5: Progress Tracking and Reporting

### Documentation-Update-Matrix.md Maintenance

**During T113-T118 refactoring**:

For each procedure refactored, **immediately update matrix**:

```markdown
| File Path | Status | Last Updated | Assigned To | Notes |
|-----------|--------|--------------|-------------|-------|
| Database/UpdatedStoredProcedures/inv_inventory_Add.sql | ✅ | 2025-10-15 | Dev1 | Header comments updated |
| Data/Dao_Inventory.cs (AddInventoryAsync XML) | ✅ | 2025-10-15 | Dev1 | XML docs complete |
| Database/00_STATUS_CODE_STANDARDS.md | 🔄 | 2025-10-15 | Dev1 | Adding multi-step example |
```

**Status Legend**:
- ⬜ Not Started
- 🔄 In Progress  
- ✅ Complete
- ⚠️ Needs Review

**Validation Frequency**: Run validation script after each T113-T118 task completes.

---

### Task Status Updates

Update task completion status in `specs/002-comprehensive-database-layer/tasks.md`:

**Pattern**:
```markdown
- [X] T100 [P] [SP-Discovery] Comprehensive Stored Procedure Discovery - ✅ COMPLETE (2025-10-15)
- [X] T101 [P] [SP-Discovery] Database Schema Extraction - ✅ COMPLETE (2025-10-15)
- [ ] T102 [SP-Discovery] Generate Individual SQL Files - 🔄 IN PROGRESS
```

**Frequency**: Update after each task/subtask completion.

---

### Checkpoint Reviews

**Execute checkpoint reviews** at end of each Part:

1. **Part A Complete**: Run discovery quality checklist + CSV transaction analysis checklist
2. **Part B Complete**: Run testing quality checklist + verbose test failure checklist
3. **Part C Complete**: Run refactoring quality checklist + developer role checklist + parameter prefix maintenance checklist
4. **Part D Complete**: Run deployment quality checklist + schema drift checklist
5. **Part E Complete**: Run integration quality checklist + roslyn analyzer checklist
6. **Part F Complete**: Run documentation quality checklist + documentation matrix checklist

**Decision Point**: Each checkpoint requires Go/No-Go decision before proceeding to next Part.

---

### Daily Summary Reports

**Provide brief summary** at end of each work session:

**Template**:
```
## Daily Summary - [Date]

**Phase**: 2.5 Part [A/B/C/D/E/F]

**Tasks Completed**: T100, T101 (2/41 Phase 2.5 tasks)

**Deliverables**:
- Database/STORED_PROCEDURE_CALLSITES.csv (220 call sites)
- Database/database-schema-snapshot.json (4 sections)

**Blockers**: None

**Next Session**: T102 (Generate Individual SQL Files)

**Matrix Status**: 0/145 documentation items complete (Phase C not started)
```

---

## Step 6: Completion Validation

### Final Success Criteria Verification

**Before marking Phase 2.5 complete**, validate all 18 success criteria:

#### Foundation Success Criteria (SC-001 through SC-010)

- [ ] **SC-001**: Zero MySQL parameter errors in logs (30-day post-deployment)
- [ ] **SC-002**: 100% Helper routing compliance (0 direct MySQL API usage in Data/)
- [ ] **SC-003**: All 60+ procedures tested (280+ tests, 100% pass rate)
- [ ] **SC-004**: Performance within ±5% variance (benchmark comparison)
- [ ] **SC-005**: Connection pool healthy under 100 concurrent operations
- [ ] **SC-006**: Error logging captures errors without recursion (file fallback tested)
- [ ] **SC-007**: New operation implemented in <15 minutes (developer productivity)
- [ ] **SC-008**: 90% reduction in database tickets (baseline vs post-deployment)
- [ ] **SC-009**: Zero orphaned records from transaction failures (rollback tested)
- [ ] **SC-010**: Sub-3-second startup validation (database available/unavailable)

#### Session 3 Enhancement Success Criteria (SC-011 through SC-018)

- [ ] **SC-011**: Verbose test failure output (7 fields in JSON on all test failures)
- [ ] **SC-012**: Developer role access control (Development tools gated by role)
- [ ] **SC-013**: Documentation matrix 100% complete (validation script exit code 0)
- [ ] **SC-014**: Schema drift detection 100% accurate (Category A/B/C reconciled)
- [ ] **SC-015**: CSV transaction analysis 100% coverage (≥90% detection accuracy)
- [ ] **SC-016**: Roslyn analyzer enforcement (0 violations, 0 false positives)
- [ ] **SC-017**: Parameter prefix overrides persist (audit trail intact across restarts)
- [ ] **SC-018**: Startup retry strategy behavior (3 attempts, clean termination)

**Validation**: All 18 criteria must be ✅ before Phase 2.5 sign-off.

---

### Checklist Summary Table

**Generate summary** of all checklist validations:

| Checklist | Total Items | Passed | Failed | Status |
|-----------|-------------|--------|--------|--------|
| Discovery Quality | 35 | 35 | 0 | ✅ PASS |
| CSV Transaction Analysis | 20 | 20 | 0 | ✅ PASS |
| Testing Quality | 80 | 80 | 0 | ✅ PASS |
| Verbose Test Failure | 25 | 25 | 0 | ✅ PASS |
| Refactoring Quality | 120 | 120 | 0 | ✅ PASS |
| Developer Role | 25 | 25 | 0 | ✅ PASS |
| Parameter Prefix Maintenance | 30 | 30 | 0 | ✅ PASS |
| Deployment Quality | 55 | 55 | 0 | ✅ PASS |
| Schema Drift | 65 | 65 | 0 | ✅ PASS |
| Integration Quality | 95 | 95 | 0 | ✅ PASS |
| Roslyn Analyzer | 45 | 45 | 0 | ✅ PASS |
| Documentation Quality | 80 | 80 | 0 | ✅ PASS |
| Documentation Matrix | 65 | 65 | 0 | ✅ PASS |
| **TOTAL** | **740** | **740** | **0** | **✅ COMPLETE** |

**Requirement**: All checklists must show ✅ PASS before proceeding to Phase 3-8.

---

### Phase 2.5 Sign-Off

**Final approval** requires:

1. ✅ All 41 Phase 2.5 tasks (T100-T132) complete
2. ✅ All 18 success criteria validated
3. ✅ All 13 checklists passed (740/740 items)
4. ✅ Documentation-Update-Matrix.md 100% complete (145/145 items)
5. ✅ Phase 2.5 completion report published
6. ✅ DBA approval for production deployment
7. ✅ 30-day post-deployment monitoring clean (SC-001, SC-008)

**Sign-Off**: "Phase 2.5 Stored Procedure Standardization COMPLETE. Phase 3-8 DAO refactoring UNBLOCKED."

---

## Critical Reminders

### Execution Discipline

1. **No skipping Phase 2.5**: All 41 tasks are mandatory before any DAO work
2. **Checklist-driven validation**: Run appropriate checklist after each Part
3. **Documentation concurrent**: Update matrix during T113-T118, not after
4. **CSV review gates refactoring**: T113 blocked until T106a PR merged
5. **Drift reconciliation mandatory**: T119b/c/d/e must execute before T120
6. **100% test pass rate required**: No deployment with failing tests
7. **All success criteria validated**: 18/18 must pass before sign-off

### Common Pitfalls to Avoid

- ❌ Starting DAO refactoring before Phase 2.5 complete (DAO methods depend on standardized procedures)
- ❌ Skipping CSV review (T106a) and using uncorrected transaction recommendations
- ❌ Deploying without drift reconciliation (overwrites production hotfixes)
- ❌ Deferring documentation to end (creates massive backlog, ensures drift)
- ❌ Ignoring checklist validation (quality gates exist for reason)
- ❌ Proceeding with <100% test pass rate (unstable foundation)

### Success Indicators

- ✅ Steady task completion velocity (~2-3 tasks/day for Phase 2.5)
- ✅ Documentation matrix staying current (updated same day as code)
- ✅ Checklist pass rates >95% on first run (indicates quality)
- ✅ Test pass rates 100% throughout (no regression)
- ✅ Production deployment smooth with zero hotfixes in first week

---

## Summary

This prompt implements the **complete database layer standardization** combining both specification folders:

**Phase 2.5 (41 tasks, T100-T132)**: Stored procedure discovery, testing, refactoring, deployment with schema drift reconciliation
- **Part A**: Discovery and CSV analysis (8 tasks)
- **Part B**: Test implementation with verbose diagnostics (6 tasks)
- **Part C**: Refactoring with developer tools and concurrent docs (8 tasks)
- **Part D**: Deployment with drift reconciliation (7 tasks)
- **Part E**: Integration testing with Roslyn analyzer (8 tasks)
- **Part F**: Documentation validation and completion report (4 tasks)

**Phase 3-8**: DAO refactoring and async migration (after Phase 2.5 complete)

**Quality Gates**: 13 checklists with 740 validation checkpoints ensure comprehensive quality control.

**Branch**: `002-003-database-layer-complete` (active PR #59)

**Estimated Duration**: 19-30 days single developer, 10-15 days with 3 developers parallel (Phase 2.5 only)

All specification content from both folders is **mandatory** - no optional items.
