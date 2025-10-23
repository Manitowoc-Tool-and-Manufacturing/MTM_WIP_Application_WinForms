# Checklist: Refactoring Phase Requirements Quality

**Phase**: Part C - Stored Procedure Refactoring (T113-T118)  
**Purpose**: Validate refactoring requirements are complete, clear, and measurable  
**Type**: Requirements Quality Validation (NOT implementation verification)  
**Created**: 2025-10-17

---

## Scoring Summary (Quick Reference)

| Section | Items | Pass Threshold | Score | Status |
|---------|-------|----------------|-------|--------|
| 1. Completeness | 18 | ≥14 (78%) | ___ / 18 | ⬜ |
| 2. Clarity | 16 | ≥13 (81%) | ___ / 16 | ⬜ |
| 3. Measurability | 15 | ≥12 (80%) | ___ / 15 | ⬜ |
| 4. Consistency | 14 | ≥11 (79%) | ___ / 14 | ⬜ |
| 5. Traceability | 13 | ≥10 (77%) | ___ / 13 | ⬜ |
| 6. Risk/Dependencies | 16 | ≥13 (81%) | ___ / 16 | ⬜ |
| **TOTAL** | **92** | **≥74 (80%)** | **___ / 92** | **⬜** |

---

## Section 1: Completeness (18 items)

### 1.1 Standards Compliance (6 items)
- [ ] **OUT parameters required**: All procedures must add `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)` if missing
- [ ] **Status code logic**: Procedures implement proper status codes (1=success with data, 0=success no data, -1 to -5=errors per 00_STATUS_CODE_STANDARDS.md)
- [ ] **Parameter prefixes**: All parameters use consistent prefixes (p_ for CRUD, in_ for multi-step per T104 conventions)
- [ ] **PascalCase naming**: Parameters match C# model properties (p_PartID → PartID, p_LocationCode → LocationCode)
- [ ] **Error handling**: All procedures include exception handler setting p_Status=-1 and descriptive p_ErrorMsg
- [ ] **Success messaging**: Procedures set appropriate success messages in p_ErrorMsg (not just status code)

### 1.2 Refactoring Scope (6 items)
- [ ] **Top 20 procedures**: T113 targets highest priority from T105 matrix (usage × 0.4 + compliance deficiency × 0.6)
- [ ] **Inventory domain**: T114 covers remaining inv_inventory_* and inv_transaction_* procedures (~10 procedures)
- [ ] **User/Role domain**: T115 covers sys_user_*, sys_role_*, sys_user_role_* procedures (~15 procedures)
- [ ] **Master data domain**: T116 covers md_part_ids_*, md_locations_*, md_operation_numbers_*, md_item_types_* procedures (~20 procedures)
- [ ] **Logging/System domain**: T117 covers log_error_*, sys_last_10_transactions_*, and remaining system procedures (~15 procedures)
- [ ] **Transaction management**: T118 adds explicit BEGIN/COMMIT/ROLLBACK to ~10 multi-step procedures (transfers, batch operations)

### 1.3 Deliverables (6 items)
- [ ] **Refactored SQL files**: All ~90 procedures saved to `Database/UpdatedStoredProcedures/<domain>/<name>.sql` (replacing originals from T102)
- [ ] **100% compliance**: Every procedure meets all 5 validation checks from T103 audit criteria post-refactor
- [ ] **Transaction safety**: Multi-step procedures include explicit transaction management with EXIT HANDLER for rollback
- [ ] **Comment updates**: Procedure headers updated with parameter documentation and business logic notes
- [ ] **Version control**: All refactored .sql files committed to git with clear commit messages per procedure domain
- [ ] **Validation logs**: Refactoring notes documenting significant changes or edge cases per procedure

---

## Section 2: Clarity (16 items)

### 2.1 Refactoring Steps (5 items)
- [ ] **Step 1 clear**: Add OUT parameters (explicit SQL: `OUT p_Status INT, OUT p_ErrorMsg VARCHAR(500)` appended to parameter list)
- [ ] **Step 2 clear**: Standardize parameter prefixes (rename using ALTER PROCEDURE or DROP/CREATE with corrected names)
- [ ] **Step 3 clear**: Implement error handling (add `DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ... END;` block)
- [ ] **Step 4 clear**: Add success logic (insert `SET p_Status=1/0; SET p_ErrorMsg='...'` at appropriate locations)
- [ ] **Step 5 clear**: Update procedure comments (add parameter documentation per 00_STATUS_CODE_STANDARDS.md template)

### 2.2 Transaction Management Pattern (5 items)
- [ ] **BEGIN/COMMIT pattern**: Multi-step procedures wrap operations in `START TRANSACTION; ... COMMIT;` block
- [ ] **ROLLBACK handler**: EXIT HANDLER catches SQLEXCEPTION and executes `ROLLBACK; SET p_Status=-1; SET p_ErrorMsg='...'`
- [ ] **Step isolation**: Each logical step clearly delimited (-- Step 1: Deduct from source, -- Step 2: Add to destination, etc.)
- [ ] **Validation placement**: Input validation before START TRANSACTION (fail fast without starting transaction)
- [ ] **Success confirmation**: COMMIT only after all steps succeed, with final status/message set post-COMMIT

### 2.3 Edge Cases (6 items)
- [ ] **Optional parameters**: Handling of nullable parameters (accept NULL, use `IF ... IS NOT NULL THEN` logic)
- [ ] **Empty result sets**: Success no data scenario (SET p_Status=0 when SELECT returns 0 rows)
- [ ] **Duplicate detection**: Handling ON DUPLICATE KEY UPDATE or checking existence before INSERT (SET p_Status=-5 for duplicates)
- [ ] **Foreign key violations**: Catching constraint errors in EXIT HANDLER (descriptive p_ErrorMsg like "Invalid LocationCode")
- [ ] **Concurrent modifications**: Handling deadlocks/lock timeouts (caller retry logic, not procedure responsibility)
- [ ] **Mixed prefixes**: Procedures with both p_ and in_ parameters retain distinction (don't force uniform prefix if logically different)

---

## Section 3: Measurability (15 items)

### 3.1 Quantitative Targets (5 items)
- [ ] **T113 target**: 20 procedures refactored (top priority from T105 matrix)
- [ ] **T114-T117 target**: ~70 total procedures refactored (90 total including T113 overlap)
- [ ] **T118 target**: ~10 multi-step procedures enhanced with transactions
- [ ] **Compliance target**: 100% procedures meet all 5 validation checks (up from 40-60% baseline)
- [ ] **Part C duration**: 95 hours estimated (T113=20h, T114=10h, T115=15h, T116=20h, T117=15h, T118=15h)

### 3.2 Acceptance Criteria (5 items)
- [ ] **T113 complete**: 20 refactored .sql files committed to `Database/UpdatedStoredProcedures/` (observable commit)
- [ ] **T114-T117 complete**: All domain procedures refactored (observable file updates per domain folder)
- [ ] **T118 complete**: 10 procedures contain START TRANSACTION/COMMIT/ROLLBACK keywords (observable grep search)
- [ ] **Compliance validation**: Re-run T103 audit script → 100% ComplianceScore for all procedures (observable report)
- [ ] **Test validation**: Run T108-T111 integration tests → 100% pass rate (observable test results)

### 3.3 Quality Gates (5 items)
- [ ] **Before/after comparison**: T103 compliance report shows improvement (40-60% → 100%)
- [ ] **Syntax validation**: All refactored procedures execute `SHOW CREATE PROCEDURE` without MySQL errors
- [ ] **Integration test pass**: Every refactored procedure passes all 4 tests (success with/without data, validation error, database error)
- [ ] **Transaction test**: Multi-step procedures tested with forced mid-operation failure → complete rollback (T127 preview)
- [ ] **Peer review**: Each domain's refactored procedures reviewed by second developer (code review sign-off)

---

## Section 4: Consistency (14 items)

### 4.1 Internal Consistency (5 items)
- [ ] **T105 → T113**: Top 20 procedures from priority matrix are T113 targets (data flows correctly)
- [ ] **T113-T117 → T103**: All procedures re-audited post-refactor (compliance validation complete)
- [ ] **T118 → T127**: Multi-step transaction procedures validated in integration testing (transaction safety confirmed)
- [ ] **Refactored files → T119**: Deployment script installs from UpdatedStoredProcedures/ (deployment source correct)
- [ ] **Domain organization**: Procedures grouped by domain in folders match task breakdowns (inventory/, users/, master-data/, logging/)

### 4.2 Cross-Document Consistency (5 items)
- [ ] **spec.md FR-004**: All procedures have OUT p_Status and OUT p_ErrorMsg (requirement satisfied)
- [ ] **spec.md FR-017**: Parameter naming aligns with C# models (PascalCase, type matching) (requirement satisfied)
- [ ] **spec.md US4**: DBA can validate uniform schema post-refactor (user story addressed)
- [ ] **clarification-questions.md Q6**: Strict 100% compliance enforced, no exceptions (clarification applied)
- [ ] **plan.md Phase 2.5 Part C**: All 6 tasks (T113-T118) present and described (no missing tasks)

### 4.3 Pattern Consistency (4 items)
- [ ] **Error handling pattern**: All procedures use same EXIT HANDLER structure (consistent exception handling)
- [ ] **Status code pattern**: All procedures use same status code meanings (1/0/-1 to -5 as defined)
- [ ] **Transaction pattern**: All multi-step procedures use same BEGIN/COMMIT/ROLLBACK structure (consistent transaction management)
- [ ] **Comment pattern**: All procedures include parameter documentation and business logic notes (consistent documentation)

---

## Section 5: Traceability (13 items)

### 5.1 Requirements to Tasks (4 items)
- [ ] **FR-004 (Status/Error Outputs)**: T113-T117 add OUT parameters to all procedures (requirement implemented)
- [ ] **FR-011 (Transaction Management)**: T118 adds explicit transactions to multi-step operations (requirement implemented)
- [ ] **FR-017 (Parameter Naming)**: T113-T117 standardize prefixes and PascalCase (requirement implemented)
- [ ] **SC-001 (Zero Parameter Errors)**: Refactoring eliminates prefix inconsistencies causing MySQL errors (success criteria addressed)

### 5.2 Tasks to Deliverables (6 items)
- [ ] **T113**: Produces 20 refactored .sql files (clear output)
- [ ] **T114**: Produces ~10 refactored inventory .sql files (clear output)
- [ ] **T115**: Produces ~15 refactored user/role .sql files (clear output)
- [ ] **T116**: Produces ~20 refactored master data .sql files (clear output)
- [ ] **T117**: Produces ~15 refactored logging/system .sql files (clear output)
- [ ] **T118**: Produces ~10 transaction-enhanced .sql files (clear output)

### 5.3 Deliverables to Next Phase (3 items)
- [ ] **Refactored procedures → T119**: Deployment script sources from UpdatedStoredProcedures/ (feeds deployment)
- [ ] **100% compliance → T122**: Integration tests validate standardized behavior (feeds integration testing)
- [ ] **Transaction safety → T127**: Multi-step procedures tested for rollback (feeds integration testing)

---

## Section 6: Risk and Dependencies (16 items)

### 6.1 Dependency Clarity (6 items)
- [ ] **T113 dependencies**: T105 (priority matrix), T102 (original .sql files) - clear prerequisites
- [ ] **T114 dependencies**: T105 (priority matrix), T113 (high-priority done first) - clear prerequisites
- [ ] **T115 dependencies**: T105, T113 - clear prerequisites
- [ ] **T116 dependencies**: T105, T113 - clear prerequisites
- [ ] **T117 dependencies**: T105, T113 - clear prerequisites
- [ ] **T118 dependencies**: T113-T117 (basic structure refactored first) - clear prerequisite

### 6.2 Risk Identification (5 items)
- [ ] **Breaking changes risk**: Refactored procedures change behavior, breaking existing DAO calls - mitigated by integration tests (T108-T111) validating behavior preservation
- [ ] **Transaction overhead risk**: BEGIN/COMMIT adds performance cost - mitigated by SC-004 performance benchmark validation (±5% tolerance)
- [ ] **Complex logic risk**: Large procedures difficult to refactor without breaking logic - mitigated by peer review and integration testing
- [ ] **Timeline pressure**: 95 hours (~12 days) ambitious - mitigated by parallelizing with Part B testing
- [ ] **Regression risk**: Refactored procedures introduce new bugs - mitigated by 100% test coverage and manual validation (T126)

### 6.3 Mitigation Completeness (5 items)
- [ ] **Breaking changes mitigation**: Integration tests (T108-T111) run post-refactor to catch behavior changes (explicit validation)
- [ ] **Performance mitigation**: T128 benchmark validates ±5% performance (explicit measurement)
- [ ] **Complex logic mitigation**: Peer review required for T113-T117 (explicit quality gate)
- [ ] **Timeline mitigation**: Parallelization with Part B in resource allocation plan (explicit strategy)
- [ ] **Regression mitigation**: 100% test coverage + manual testing (T126) catches issues (explicit safety net)

---

## Findings and Actions

### Critical Issues (Must Fix Before T113 Execution)
1. 
2. 
3. 

### Minor Issues (Document as Known Limitation)
1. 
2. 
3. 

### Observations (No Action Required)
1. 
2. 
3. 

---

## Approval

**Checklist Completed By**: ___________________  
**Date**: ___________________  
**Approval to Proceed to T113**: ⬜ Approved ⬜ Revisions Required

**Approver (Architect / Lead Developer)**: ___________________  
**Date**: ___________________

---

**Checklist Version**: 1.1  
**Last Updated**: 2025-10-17
