# Checklist: Schema Drift Detection and Reconciliation Requirements Quality

**Phase**: Part D - Database Deployment (T119b, T119c, T119d, T119e)  
**Purpose**: Validate that schema drift reconciliation requirements are complete, clear, and measurable  
**Type**: Requirements Quality Validation (NOT implementation verification)  
**Created**: 2025-10-15

---

## Overview

This checklist validates the **quality of schema drift detection and reconciliation requirements** as defined in FR-025, SC-014, and T119b/c/d/e. It does NOT test the implementation—that happens during execution. Use this checklist during Checkpoint 3 review (after T119b/c/d/e planning, before execution begins).

**Target Audience**: DBA, DevOps Lead, Architect  
**When to Use**: Before starting T119b execution, to ensure requirements are ready  
**Pass Criteria**: All sections score ≥80% (individual items can fail if documented)

---

## Section 1: Requirement Completeness

### 1.1 Drift Detection Methodology

- [ ] **Baseline audit source**: T101 audit from Phase 2.5 start used as baseline (stored in version control with timestamp)
- [ ] **Re-audit timing**: Executed immediately before T119 deployment (captures all production changes during Phase 2.5)
- [ ] **Re-audit query**: Same INFORMATION_SCHEMA queries as T101 (ROUTINES, PARAMETERS tables) - ensures consistent comparison
- [ ] **Diff calculation**: Compare ProcedureName, ParameterCount, ModifiedDate between baseline and re-audit
- [ ] **Drift types detected**: Added (in re-audit, not baseline), Modified (different ModifiedDate), Deleted (in baseline, not re-audit)
- [ ] **Output format**: drift-report.csv with columns (ProcedureName, DriftType, Category, BaselineVersion, CurrentVersion, ConflictRisk)

**Score**: ___ / 6 (requires ≥5 pass)

### 1.2 Category A - Independent Hotfix Criteria

- [ ] **Definition**: Production procedure changes unrelated to Phase 2.5 refactoring scope (no overlap with T113-T118 procedures)
- [ ] **Identification method**: Compare drifted procedure names to T105 refactoring priority matrix (not in top 70 procedures = Category A)
- [ ] **Business logic preservation**: Original hotfix logic must be retained verbatim (no modifications during standardization)
- [ ] **Standardization approach**: Apply 00_STATUS_CODE_STANDARDS.md template (add OUT params, prefixes) without altering business logic
- [ ] **Documentation requirement**: Procedure header comment must include "HOTFIX: Applied during Phase 2.5, standardized YYYY-MM-DD"
- [ ] **Expected count**: 2-5 procedures (low volume, typical hotfix rate during 15-25 day Phase 2.5)

**Score**: ___ / 6 (requires ≥5 pass)

### 1.3 Category B - Conflicting Change Criteria

- [ ] **Definition**: Production procedure changes overlapping with Phase 2.5 refactoring (same procedure modified in production AND refactored in T113-T118)
- [ ] **Identification method**: Drifted procedure names match T105 priority matrix procedures (in top 70 procedures = Category B)
- [ ] **Three-way merge requirement**: Baseline (Phase 2.5 start) vs Refactored (T113-T118) vs Production (current) - manual merge required
- [ ] **Merge strategy**: Keep Phase 2.5 standardization (OUT params, error handling) + integrate production business logic changes
- [ ] **Conflict resolution log**: Document merge decisions in T119d-conflict-resolution.md (rationale for each conflict resolution)
- [ ] **Expected count**: 1-3 procedures (low volume, indicates good production freeze adherence)

**Score**: ___ / 6 (requires ≥5 pass)

### 1.4 Category C - New Procedure Criteria

- [ ] **Definition**: Procedures added to production during Phase 2.5 (not present in baseline audit)
- [ ] **Identification method**: ProcedureName in re-audit but not in baseline (DriftType=Added)
- [ ] **Full refactoring requirement**: Apply complete T113-T118 workflow (standards, integration tests, documentation)
- [ ] **Compliance audit**: Run T103 audit checks against new procedures to establish baseline scores
- [ ] **Integration test creation**: Create 4-test pattern (success with data, success no data, validation error, database error)
- [ ] **Expected count**: 1-4 procedures (low volume, normal new development during multi-week Phase 2.5)

**Score**: ___ / 6 (requires ≥5 pass)

---

## Section 2: Requirement Clarity

### 2.1 Task Instructions Unambiguous

- [ ] **T119b re-audit steps**: Execute T101 queries against production → save as re-audit.json → diff against baseline.json → classify drift
- [ ] **T119c hotfix refactoring**: Extract Category A procedures → apply standards template → preserve business logic → save with _hotfix suffix → test
- [ ] **T119d conflict merge**: For each Category B procedure → load 3 versions (baseline/refactored/production) → use diff tool → merge manually → document resolution
- [ ] **T119e new procedure refactoring**: Extract Category C procedures → run T103 audit → apply T113-T118 workflow → create tests → document
- [ ] **Diff tool recommendation**: VS Code Compare, Beyond Compare, or command-line diff3 suggested for three-way merge

**Score**: ___ / 5 (requires ≥4 pass)

### 2.2 Definitions Consistent

- [ ] **"Baseline"**: Audit snapshot from Phase 2.5 start (T101 timestamp documented in plan.md)
- [ ] **"Drift"**: Any change to production procedures during Phase 2.5 implementation period
- [ ] **"Hotfix"**: Emergency production bug fix applied outside Phase 2.5 workflow
- [ ] **"Conflict"**: Overlapping changes where production and Phase 2.5 both modified same procedure
- [ ] **"Reconciliation"**: Process of integrating drift into Phase 2.5 deployment (not rollback, not ignore)

**Score**: ___ / 5 (requires ≥4 pass)

### 2.3 Edge Cases Addressed

- [ ] **Zero drift detected**: If re-audit matches baseline exactly, skip T119c/d/e (proceed directly to T120)
- [ ] **Category B with no actual conflict**: Production change to different section than refactoring → reclassify as Category A (simpler merge)
- [ ] **Category C procedure already refactored**: New procedure accidentally follows standards → minimal T119e work (validation only)
- [ ] **Deleted procedures (DriftType=Deleted)**: Document in drift report but take no action (assume intentional removal)
- [ ] **ModifiedDate-only changes**: Procedure content identical but ModifiedDate differs → investigate, may be metadata-only update (no reconciliation needed)

**Score**: ___ / 5 (requires ≥4 pass)

---

## Section 3: Requirement Measurability

### 3.1 Quantitative Metrics Defined

- [ ] **T119b duration**: 0.25 days (2 hours) for re-audit execution and categorization
- [ ] **T119c duration**: 0.25-0.5 days (1-2 hours per hotfix, assumes 2-5 hotfixes)
- [ ] **T119d duration**: 0.25-0.5 days (2-3 hours per conflict, assumes 1-3 conflicts)
- [ ] **T119e duration**: 0.25-0.5 days (1-2 hours per new procedure, assumes 1-4 procedures)
- [ ] **Total drift reconciliation**: 0.75-1.5 days added to deployment timeline

**Score**: ___ / 5 (requires ≥4 pass)

### 3.2 Qualitative Acceptance Criteria

- [ ] **Drift report completeness**: All production procedures accounted for (baseline + re-audit union)
- [ ] **Category classification accuracy**: DBA review confirms categorization (no mis-categorized procedures)
- [ ] **Hotfix logic preservation**: Category A procedures pass original hotfix test cases (business logic intact)
- [ ] **Conflict merge correctness**: Category B procedures include both production fixes AND Phase 2.5 standardization
- [ ] **New procedure compliance**: Category C procedures score 100% on T103 audit (same as T113-T118 procedures)

**Score**: ___ / 5 (requires ≥4 pass)

### 3.3 Success Criteria Traceability

- [ ] **SC-014 mapped to T119b/c/d/e**: Schema drift detection accuracy and categorization requirement directly fulfilled
- [ ] **FR-025 components traced**: Drift detection (T119b), Category A/B/C processing (T119c/d/e), reconciliation report all addressed
- [ ] **R-NEW-2 risk mitigation**: Conflict merge complexity risk addressed by systematic three-way merge process

**Score**: ___ / 3 (requires ≥2 pass)

---

## Section 4: Requirement Testability

### 4.1 Validation Method Specified

- [ ] **Drift detection test**: Manually add test procedure to production → run T119b → verify detected as Category C (Added)
- [ ] **Category A test**: Verify hotfix procedures not in T105 priority matrix (no overlap with refactoring)
- [ ] **Category B test**: Verify conflict procedures in T105 priority matrix (overlap with refactoring)
- [ ] **Category C test**: Verify new procedures not in baseline audit (DriftType=Added)
- [ ] **Hotfix preservation test**: Compare Category A procedure business logic to production version → should be identical (no changes)
- [ ] **Conflict merge test**: Run integration tests on Category B merged procedures → should pass (both production logic and Phase 2.5 standards)
- [ ] **New procedure compliance test**: Run T103 audit on Category C procedures → should score 100%

**Score**: ___ / 7 (requires ≥5 pass)

### 4.2 Expected Outcomes Documented

- [ ] **Successful drift detection**: drift-report.csv contains 4-12 rows (typical drift volume for 15-25 day period)
- [ ] **Successful categorization**: Category A (2-5), Category B (1-3), Category C (1-4) within expected ranges
- [ ] **Successful hotfix refactoring**: Category A procedures save to UpdatedStoredProcedures/ with _hotfix suffix
- [ ] **Successful conflict merge**: Category B procedures save to UpdatedStoredProcedures/ (no suffix, merged version)
- [ ] **Successful new procedure refactoring**: Category C procedures save to UpdatedStoredProcedures/ with standard organization
- [ ] **Successful deployment integration**: T120 deployment includes all baseline + drift procedures (complete procedure set)

**Score**: ___ / 6 (requires ≥5 pass)

### 4.3 Failure Scenarios Defined

- [ ] **Drift detection failure**: Re-audit query fails (database access denied) → fallback to manual DBA-provided procedure list
- [ ] **Categorization ambiguity**: Cannot determine Category A vs B → escalate to DBA for business context
- [ ] **Hotfix refactoring breaks logic**: Integration test fails after standardization → rollback to production version, investigate
- [ ] **Conflict merge deadlock**: Cannot resolve three-way merge (business logic incompatible) → escalate to product owner for decision
- [ ] **New procedure non-compliant**: Category C procedure fails T103 audit → full refactoring required (may extend timeline)
- [ ] **Excessive drift volume**: >15 drifted procedures detected → re-evaluate deployment timeline, may need additional reconciliation time

**Score**: ___ / 6 (requires ≥5 pass)

---

## Section 5: Requirement Dependencies

### 5.1 Prerequisite Requirements

- [ ] **T101 baseline audit**: Stored in version control with timestamp (required for diff calculation)
- [ ] **T113-T118 complete**: Refactored procedures available for conflict comparison
- [ ] **Production database access**: DBA credentials available for re-audit execution

**Score**: ___ / 3 (requires ≥2 pass)

### 5.2 Dependent Requirements

- [ ] **T120 depends on T119b/c/d/e**: Test deployment uses post-reconciliation procedure set (baseline + drift)
- [ ] **T121 depends on T119b/c/d/e**: Production deployment includes reconciled procedures (DBA approval requires drift report review)
- [ ] **T132 depends on T119b/c/d/e**: Completion report documents drift reconciliation (Appendix A includes drift report and resolution strategies)

**Score**: ___ / 3 (requires ≥2 pass)

### 5.3 Integration Points

- [ ] **T105 integration**: Refactoring priority matrix used for Category A vs B classification
- [ ] **T103 integration**: Compliance audit re-run for Category C procedures
- [ ] **T113-T118 integration**: Refactored procedures provide merge input for Category B
- [ ] **T108-T111 integration**: Integration tests re-run for Category A/B procedures after reconciliation

**Score**: ___ / 4 (requires ≥3 pass)

---

## Section 6: Requirement Risks

### 6.1 Technical Risks Identified

- [ ] **R-NEW-2 documented**: Schema drift reconciliation complexity risk (Category B three-way merge) addressed in risk assessment
- [ ] **Mitigation - systematic merge**: Three-way merge process (baseline/refactored/production) documented with diff tool recommendations
- [ ] **Mitigation - conflict log**: T119d-conflict-resolution.md documents all merge decisions for review
- [ ] **Mitigation - integration tests**: Post-merge testing validates correctness (no regression)

**Score**: ___ / 4 (requires ≥3 pass)

### 6.2 Process Risks Identified

- [ ] **Timeline risk**: Drift reconciliation adds 0.75-1.5 days to deployment window → mitigation: parallel processing of Category A/B/C where possible
- [ ] **DBA availability risk**: Re-audit requires DBA database access → mitigation: schedule DBA time in advance, document credentials
- [ ] **Hotfix identification risk**: Cannot distinguish hotfix from normal development → mitigation: query production change log, ask DBA for hotfix list
- [ ] **Merge skill gap risk**: Developers unfamiliar with three-way merge tools → mitigation: provide training or pair programming for Category B

**Score**: ___ / 4 (requires ≥3 pass)

### 6.3 Quality Risks Identified

- [ ] **Hotfix regression risk**: Standardization breaks hotfix business logic → mitigation: run original hotfix test cases before/after refactoring
- [ ] **Conflict merge error risk**: Merge introduces bugs → mitigation: integration tests + manual testing of merged procedures
- [ ] **Incomplete reconciliation risk**: Some drift procedures missed → mitigation: DBA final review of drift report completeness
- [ ] **Documentation gap risk**: Merge decisions not documented → mitigation: T119d conflict log required before T120 approval

**Score**: ___ / 4 (requires ≥3 pass)

---

## Scoring Summary

| Section | Score | Pass Threshold | Status |
|---------|-------|----------------|--------|
| 1. Requirement Completeness | ___ / 24 | ≥19 | ☐ |
| 2. Requirement Clarity | ___ / 15 | ≥12 | ☐ |
| 3. Requirement Measurability | ___ / 13 | ≥10 | ☐ |
| 4. Requirement Testability | ___ / 19 | ≥15 | ☐ |
| 5. Requirement Dependencies | ___ / 10 | ≥8 | ☐ |
| 6. Requirement Risks | ___ / 12 | ≥10 | ☐ |
| **Total** | **___ / 93** | **≥74 (80%)** | **☐** |

---

## Validation Instructions

1. **Pre-Execution Review**: Run this checklist during Checkpoint 3 planning, before T119b execution begins
2. **Scoring**: Check each item as Pass (☑) or Fail (☐), calculate section scores
3. **Gap Analysis**: For any section scoring <80%, document missing requirements in clarification-questions.md
4. **Approval Gate**: All sections must achieve ≥80% before T119b execution approved
5. **Revision Tracking**: Document checklist version and review date at bottom

---

**Checklist Version**: 1.0  
**Last Reviewed**: _________  
**Reviewed By**: _________  
**Overall Status**: ☐ PASS | ☐ FAIL | ☐ NEEDS REVISION
