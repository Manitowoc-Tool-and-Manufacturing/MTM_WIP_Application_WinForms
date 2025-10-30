# Checklist: Discovery Phase Requirements Quality

**Phase**: Part A - Discovery and Analysis (T100-T106)  
**Purpose**: Validate that discovery requirements are complete, clear, and measurable  
**Type**: Requirements Quality Validation (NOT implementation verification)  
**Created**: 2025-10-15

---

## Overview

This checklist validates the **quality of discovery phase requirements** as defined in spec.md and plan.md. It does NOT test the implementation—that happens during execution. Use this checklist during Checkpoint 1 review (after T100-T106 planning, before execution begins).

**Target Audience**: Product Owner, Architect, Lead Developer  
**When to Use**: Before starting T100 execution, to ensure requirements are ready  
**Pass Criteria**: All sections score ≥80% (individual items can fail if documented)

---

## Section 1: Requirement Completeness

### 1.1 Scope Definition

-   [ ] **Discovery target clearly defined**: Production database (`MTM_WIP_Application_Winforms`) specified as source of truth (Q1 clarification)
-   [ ] **Call site inventory scope documented**: All .cs files in Data/, Helpers/, Forms/, Controls/, Services/ included in T100 search
-   [ ] **Schema extraction completeness**: Four INFORMATION_SCHEMA queries defined (ROUTINES, PARAMETERS, TABLES, COLUMNS) in T101
-   [ ] **Procedure file organization**: Directory structure specified (`Database/UpdatedStoredProcedures/<domain>/<name>.sql`) in T102
-   [ ] **Audit criteria documented**: Five validation checks listed in T103 (HasStatus, HasErrorMsg, StatusLogicCorrect, ParameterPrefixConsistent, ComplianceScore)
-   [ ] **Convention documentation scope**: Parameter prefix patterns categorized (p*, in*, o\_, none) with fallback rules in T104
-   [ ] **Priority matrix inputs defined**: Hybrid scoring formula specified (CallSiteCount × 0.4 + ComplianceDeficiency × 0.6) in T105
-   [ ] **Test coverage matrix structure**: CoverageStatus categories defined (Covered/Partial/None) in T106

**Score**: \_\_\_ / 8 (requires ≥6 pass)

### 1.2 Deliverable Specifications

-   [ ] **T100 output format**: CSV with columns (FilePath, LineNumber, StoredProcedureName, CallPattern, Notes) specified
-   [ ] **T101 output format**: JSON with 4 sections (routines, parameters, tables, columns) specified
-   [ ] **T102 output format**: Individual .sql files, one per procedure, organized by domain subfolder
-   [ ] **T103 output format**: CSV with columns (ProcedureName, HasStatus, HasErrorMsg, StatusLogicCorrect, ParameterPrefixConsistent, ComplianceScore) specified
-   [ ] **T104 output format**: Markdown document with convention sections (Standard Prefix, Multi-Step Operations, Output Parameters, No Prefix, Fallback Convention)
-   [ ] **T105 output format**: CSV sorted by priority score with columns (ProcedureName, CallSiteCount, ComplianceScore, PriorityScore, RefactoringComplexity)
-   [ ] **T106 output format**: CSV with columns (ProcedureName, HasIntegrationTest, TestFilePath, TestMethodNames, CoverageStatus)

**Score**: \_\_\_ / 7 (requires ≥5 pass)

### 1.3 Success Criteria Linkage

-   [ ] **SC-002 mapped to T100**: Call site discovery feeds compliance validation (0 direct MySQL API usage)
-   [ ] **Baseline compliance documented**: T103 expected to show 40-60% current compliance rate (realistic estimate)
-   [ ] **Test coverage baseline documented**: T106 expected to show ~30% current test coverage (realistic estimate)
-   [ ] **Priority threshold defined**: Top 20 procedures constitute "critical" tier for T113 refactoring
-   [ ] **Convention fallback accuracy**: T104 fallback rules expected to cover ~95% of procedures (Q7 clarification)

**Score**: \_\_\_ / 5 (requires ≥4 pass)

---

## Section 2: Requirement Clarity

### 2.1 Task Instructions Unambiguous

-   [ ] **T100 search patterns explicit**: Seven patterns listed (ExecuteNonQuery, ExecuteScalar, ExecuteDataTable, ExecuteReader, CommandType.StoredProcedure, CALL, MySqlCommand)
-   [ ] **T101 SQL queries provided**: Four complete SELECT statements provided in plan.md for copy-paste execution
-   [ ] **T102 extraction method**: SHOW CREATE PROCEDURE command specified, file naming pattern clear (`<name>.sql`)
-   [ ] **T103 audit method**: Five validation checks with pass/fail criteria defined (e.g., "Has OUT p_Status INT" = yes/no)
-   [ ] **T104 grouping method**: Procedure name pattern analysis described (inv*inventory*_, inv*transaction_Transfer*_, etc.)
-   [ ] **T105 formula unambiguous**: Weighted scoring formula explicit with clear coefficients (0.4 and 0.6)
-   [ ] **T106 search method**: Test file location (Tests/ folder), parse strategy (test method names for procedure references)

**Score**: \_\_\_ / 7 (requires ≥5 pass)

### 2.2 Definitions Consistent

-   [ ] **"Stored procedure" vs "routine"**: Terms used consistently (procedures exclude functions/triggers)
-   [ ] **"Parameter prefix"**: Defined as p*, in*, o\_ prefixes in MySQL parameter names (vs unprefixed C# keys)
-   [ ] **"Call site"**: Defined as location in C# code where stored procedure executed (FilePath + LineNumber)
-   [ ] **"Compliance"**: Measured as 0-100% score based on five validation checks (T103 criteria)
-   [ ] **"Priority"**: Hybrid score combining usage frequency and compliance deficiency (not subjective)
-   [ ] **"Test coverage"**: Presence of integration test (Covered), partial test (Partial), no test (None) - clear categories

**Score**: \_\_\_ / 6 (requires ≥5 pass)

### 2.3 Edge Cases Addressed

-   [ ] **T100 false positives**: Handling of commented-out code, string literals containing "CALL" (Notes column documents ambiguous cases)
-   [ ] **T101 permissions**: Fallback if INFORMATION_SCHEMA access denied (skip schema extraction, rely on code analysis + manual review)
-   [ ] **T102 name collisions**: Handling of procedures with same name in different schemas (include schema in filename: `<schema>_<name>.sql`)
-   [ ] **T103 partial compliance**: Scoring procedures with some but not all required elements (ComplianceScore = passed checks / 5 × 100%)
-   [ ] **T104 ambiguous patterns**: Procedures not matching known conventions (document as "Custom" category with notes)
-   [ ] **T105 zero call sites**: Handling of procedures never referenced in code (low priority but still audited for cleanup consideration)
-   [ ] **T106 partial tests**: Distinguishing between complete test suite (4 tests) vs single test (Covered vs Partial status)

**Score**: \_\_\_ / 7 (requires ≥5 pass)

---

## Section 3: Requirement Measurability

### 3.1 Quantitative Metrics Defined

-   [ ] **T100 target count**: Expected ~220 call sites (plan.md estimate provides baseline for sanity check)
-   [ ] **T101 expected entities**: Expected 70 procedures, 40 tables, 300 columns (plan.md estimates enable completeness validation)
-   [ ] **T102 file count**: Expected ~70 .sql files matching T101 procedure count (1:1 mapping)
-   [ ] **T103 compliance threshold**: 40-60% current compliance provides baseline for improvement measurement
-   [ ] **T105 top tier size**: Top 20 procedures = 28% of total (clear critical vs remaining split)
-   [ ] **T106 coverage target**: Current 30% coverage → 100% post-Phase 2.5 (measurable improvement)
-   [ ] **Part A duration**: 18 hours estimated (~2.5 days) with per-task breakdowns (T100=2h, T101=1h, T102=3h, T103=6h, T104=2h, T105=2h, T106=3h)

**Score**: \_\_\_ / 7 (requires ≥5 pass)

### 3.2 Acceptance Criteria Observable

-   [ ] **T100 complete**: callsite-inventory.csv exists with ≥200 rows (observable file presence + row count)
-   [ ] **T101 complete**: database-schema-snapshot.json exists with 4 sections populated (observable file structure)
-   [ ] **T102 complete**: ≥60 .sql files in UpdatedStoredProcedures/ (observable file count)
-   [ ] **T103 complete**: compliance-report.csv shows ComplianceScore 0-100% for all procedures (observable scoring)
-   [ ] **T104 complete**: parameter-prefix-conventions.md exists with 5 documented sections (observable section headers)
-   [ ] **T105 complete**: refactoring-priority.csv sorted by PriorityScore descending (observable sort order)
-   [ ] **T106 complete**: test-coverage-matrix.csv shows CoverageStatus for all procedures (observable status values)

**Score**: \_\_\_ / 7 (requires ≥5 pass)

### 3.3 Quality Gates Testable

-   [ ] **Discovery completeness**: All procedures from T101 appear in T102, T103, T105, T106 (cross-reference validation)
-   [ ] **Call site accuracy**: Random sample of 10 call sites from T100 manually verified (spot-check validation)
-   [ ] **Schema accuracy**: T101 JSON validated against manual SHOW TABLES / SHOW CREATE PROCEDURE (schema consistency check)
-   [ ] **Compliance scoring**: T103 scoring logic testable (5 checks × 20% each = 100%)
-   [ ] **Priority formula**: T105 formula testable (reconstruct PriorityScore from CallSiteCount and ComplianceScore inputs)
-   [ ] **Coverage accuracy**: T106 coverage status validated against actual Tests/ folder contents (file existence check)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

---

## Section 4: Requirement Consistency

### 4.1 Internal Consistency

-   [ ] **T100 → T105**: CallSiteCount in priority matrix matches counts from callsite-inventory.csv (data flows correctly)
-   [ ] **T101 → T102**: Procedure count in schema snapshot matches .sql file count (1:1 mapping preserved)
-   [ ] **T102 → T103**: Procedures in compliance report match .sql files extracted (same entity set)
-   [ ] **T103 → T105**: ComplianceScore used correctly in priority formula (validated calculation)
-   [ ] **T101 → T106**: Procedures in test coverage matrix match schema snapshot (complete procedure list)
-   [ ] **T105 → T113**: Top 20 procedures from priority matrix feed T113 refactoring (clear handoff)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

### 4.2 Cross-Document Consistency

-   [ ] **spec.md FR-002**: T104 parameter prefix conventions support INFORMATION_SCHEMA fallback (requirement satisfied)
-   [ ] **spec.md US4**: T103 audit ensures uniform parameter naming and output conventions (user story addressed)
-   [ ] **plan.md Phase 2.5 Part A**: All 7 tasks (T100-T106) present and described (no missing tasks)
-   [ ] **clarification-questions.md Q1**: T101 queries production database per Q1 decision (clarification applied)
-   [ ] **clarification-questions.md Q4**: T105 uses hybrid scoring per Q4 decision (clarification applied)
-   [ ] **tasks.md Checkpoint 1**: All Part A deliverables listed in checkpoint review (deliverables tracked)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

### 4.3 Terminology Alignment

-   [ ] **"Stored procedure" usage**: Consistent across spec.md, plan.md, tasks.md (no switching to "routine" or "sproc")
-   [ ] **"Parameter prefix" usage**: Consistent meaning (MySQL p*/in*/o\_ vs C# unprefixed) across all docs
-   [ ] **"Call site" usage**: Consistent meaning (C# location) vs "procedure reference" or "invocation"
-   [ ] **"Compliance" usage**: Always refers to 00_STATUS_CODE_STANDARDS.md template matching (not general code quality)
-   [ ] **"Priority" usage**: Always refers to T105 hybrid scoring (not subjective importance)

**Score**: \_\_\_ / 5 (requires ≥4 pass)

---

## Section 5: Requirement Traceability

### 5.1 Requirements to Tasks

-   [ ] **FR-002 (Parameter Prefix Detection)**: T101 extracts INFORMATION_SCHEMA.PARAMETERS, T104 documents fallback conventions (requirement decomposed)
-   [ ] **FR-016 (Eliminate Direct MySQL API)**: T100 discovers direct API usage for SC-002 validation baseline (requirement supported)
-   [ ] **US4 (Schema Consistency)**: T101 schema extraction + T103 compliance audit provide DBA visibility (user story satisfied)
-   [ ] **SC-002 (100% Helper Routing)**: T100 callsite inventory feeds compliance validation (success criteria measurable)
-   [ ] **SC-003 (Comprehensive Testing)**: T106 test coverage matrix identifies gaps for Part B (success criteria scoped)

**Score**: \_\_\_ / 5 (requires ≥4 pass)

### 5.2 Tasks to Deliverables

-   [ ] **T100**: Produces callsite-inventory.csv (clear output)
-   [ ] **T101**: Produces database-schema-snapshot.json (clear output)
-   [ ] **T102**: Produces 70+ .sql files (clear output)
-   [ ] **T103**: Produces compliance-report.csv (clear output)
-   [ ] **T104**: Produces parameter-prefix-conventions.md (clear output)
-   [ ] **T105**: Produces refactoring-priority.csv (clear output)
-   [ ] **T106**: Produces test-coverage-matrix.csv (clear output)

**Score**: \_\_\_ / 7 (requires ≥6 pass)

### 5.3 Deliverables to Next Phase

-   [ ] **callsite-inventory.csv → T124**: Validation uses inventory to check Helper routing compliance (feeds integration testing)
-   [ ] **database-schema-snapshot.json → T123**: Startup cache validation compares to schema snapshot (feeds integration testing)
-   [ ] **Individual .sql files → T119**: Deployment script installs from UpdatedStoredProcedures/ (feeds deployment)
-   [ ] **compliance-report.csv → T132**: Completion report includes baseline compliance metrics (feeds documentation)
-   [ ] **refactoring-priority.csv → T113**: Top 20 procedures guide refactoring order (feeds refactoring)
-   [ ] **test-coverage-matrix.csv → T108-T111**: Gap analysis drives test creation (feeds testing)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

---

## Section 6: Risk and Dependency Analysis

### 6.1 Dependency Clarity

-   [ ] **T100 dependencies**: None (can start immediately)
-   [ ] **T101 dependencies**: None (can start immediately)
-   [ ] **T102 dependencies**: T101 (needs procedure list) - clear predecessor
-   [ ] **T103 dependencies**: T102 (needs procedure source files) - clear predecessor
-   [ ] **T104 dependencies**: T101 + T103 (needs parameter metadata + compliance analysis) - clear predecessors
-   [ ] **T105 dependencies**: T100 + T103 (needs call site count + compliance scores) - clear predecessors
-   [ ] **T106 dependencies**: T101 (needs procedure list) - clear predecessor

**Score**: \_\_\_ / 7 (requires ≥6 pass)

### 6.2 Risk Identification

-   [ ] **T100 risk**: False positives in grep search (mitigated by Notes column for ambiguous cases)
-   [ ] **T101 risk**: INFORMATION_SCHEMA access denied (mitigated by fallback to manual schema extraction)
-   [ ] **T102 risk**: SHOW CREATE PROCEDURE fails for some procedures (mitigated by error logging + manual extraction)
-   [ ] **T103 risk**: Manual audit subjective/inconsistent (mitigated by clear 5-check criteria with binary pass/fail)
-   [ ] **T105 risk**: Priority formula doesn't match actual importance (mitigated by hybrid scoring balancing usage + compliance)
-   [ ] **T106 risk**: Test naming conventions vary, hard to parse (mitigated by manual review of ambiguous cases)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

### 6.3 Mitigation Completeness

-   [ ] **T100 mitigation documented**: Notes column usage described in task (explicit handling)
-   [ ] **T101 mitigation documented**: Fallback to manual extraction described in plan.md edge cases (explicit handling)
-   [ ] **T102 mitigation documented**: Error logging for failed extractions in task description (explicit handling)
-   [ ] **T103 mitigation documented**: Five binary checks eliminate subjectivity in plan.md (explicit handling)
-   [ ] **T105 mitigation documented**: Hybrid formula rationale in Q4 clarification (explicit handling)
-   [ ] **T106 mitigation documented**: Manual review step in task description for ambiguous cases (explicit handling)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

---

## Scoring Summary

| Section              | Score            | Pass Threshold | Status              |
| -------------------- | ---------------- | -------------- | ------------------- |
| 1. Completeness      | \_\_\_ / 20      | ≥16            | ⬜ Pass ⬜ Fail     |
| 2. Clarity           | \_\_\_ / 20      | ≥16            | ⬜ Pass ⬜ Fail     |
| 3. Measurability     | \_\_\_ / 20      | ≥16            | ⬜ Pass ⬜ Fail     |
| 4. Consistency       | \_\_\_ / 17      | ≥14            | ⬜ Pass ⬜ Fail     |
| 5. Traceability      | \_\_\_ / 18      | ≥15            | ⬜ Pass ⬜ Fail     |
| 6. Risk/Dependencies | \_\_\_ / 19      | ≥15            | ⬜ Pass ⬜ Fail     |
| **TOTAL**            | **\_\_\_ / 114** | **≥92 (80%)**  | **⬜ Pass ⬜ Fail** |

---

## Findings and Actions

### Critical Issues (Must Fix Before T100 Execution)

1. _(Example: T105 priority formula not documented - ADD to plan.md)_
2.
3.

### Minor Issues (Document as Known Limitation)

1. _(Example: T100 may have 5-10 false positives - ACCEPT and document in Notes column)_
2.
3.

### Observations (No Action Required)

1. _(Example: T103 manual audit takes 6 hours - longer than expected but acceptable)_
2.
3.

---

## Approval

**Checklist Completed By**: ********\_\_\_********  
**Date**: ********\_\_\_********  
**Approval to Proceed to T100 Execution**: ⬜ Approved ⬜ Revisions Required

**Approver (Product Owner / Architect)**: ********\_\_\_********  
**Date**: ********\_\_\_********

---

**Checklist Version**: 1.0  
**Last Updated**: 2025-10-15
