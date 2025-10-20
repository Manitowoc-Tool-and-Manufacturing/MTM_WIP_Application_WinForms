# Checklist: Verbose Test Failure Diagnostics Requirements Quality

**Phase**: Part B - Integration Test Generation (T107, T108, T109, T110, T111)  
**Purpose**: Validate that verbose test failure diagnostic requirements are complete, clear, and measurable  
**Type**: Requirements Quality Validation (NOT implementation verification)  
**Created**: 2025-10-17

---

## Overview

This checklist validates the **quality of verbose test failure diagnostic requirements** as defined in FR-022, SC-011, and T107-T111 for the combined database initiative. It does NOT test the implementation—that happens during execution. Use this checklist during Checkpoint 2 review (after T107 planning, before integration test generation begins).

**Target Audience**: QA Lead, Development Manager, Test Engineer  
**When to Use**: Before starting T107 execution, to ensure requirements are ready  
**Pass Criteria**: All sections score ≥80% (individual items can fail if documented)

---

## Section 1: Requirement Completeness

### 1.1 AssertProcedureResult Helper Method

- [ ] **Method location**: Added to BaseIntegrationTest.cs abstract class (inherited by all integration test classes)
- [ ] **Method signature**: `protected void AssertProcedureResult(Exception ex, Dictionary<string, object> parameters, object expected, object actual, long executionTimeMs, Dictionary<string, int> dbState, [CallerMemberName] string testMethod = "")`
- [ ] **JSON output format**: Serializes 7-field diagnostic object to JSON, writes to test output stream
- [ ] **Timestamp field**: Captures UTC timestamp of test failure (ISO 8601 format: `2025-01-15T14:32:18.123Z`)

**Score**: ___ / 4 (requires ≥3 pass)

### 1.2 Seven Diagnostic Fields

- [ ] **Field 1 - Exception**: Full exception message + stack trace (includes inner exceptions)
- [ ] **Field 2 - Parameters**: Serialized Dictionary<string, object> of stored procedure input parameters (e.g., `{"p_UserID": 123, "p_PartNumber": "ABC-001"}`)
- [ ] **Field 3 - Expected**: Serialized expected test outcome (e.g., `{"Status": 0, "RowCount": 5}`)
- [ ] **Field 4 - Actual**: Serialized actual test result (e.g., `{"Status": -1, "RowCount": 0, "ErrorMsg": "Part not found"}`)
- [ ] **Field 5 - ExecutionTimeMs**: Stored procedure execution duration in milliseconds (helps identify timeout vs logic failure)
- [ ] **Field 6 - DatabaseState**: Pre-execution table row counts (e.g., `{"sys_inventory": 42, "sys_transaction_log": 158}`)
- [ ] **Field 7 - TestMethod**: Test method name (captured via [CallerMemberName] attribute, e.g., `Test_sp_TransferInventory_Success`)

**Score**: ___ / 7 (requires ≥6 pass)

### 1.3 CaptureTableRowCounts Helper Method

- [ ] **Method location**: Added to BaseIntegrationTest.cs abstract class
- [ ] **Method signature**: `protected Dictionary<string, int> CaptureTableRowCounts(params string[] tableNames)`
- [ ] **Implementation**: Executes `SELECT COUNT(*) FROM {tableName}` for each table, returns Dictionary with table names as keys
- [ ] **Usage pattern**: Call before executing stored procedure to capture pre-execution state (e.g., `var dbStateBefore = CaptureTableRowCounts("sys_inventory", "sys_transaction_log");`)

**Score**: ___ / 4 (requires ≥3 pass)

### 1.4 JSON Serialization Configuration

- [ ] **Library choice**: System.Text.Json (already referenced in project, no new dependencies)
- [ ] **Options configuration**: `JsonSerializerOptions` with WriteIndented=true (human-readable JSON), PropertyNamingPolicy=CamelCase (consistent with project standards)
- [ ] **Exception serialization**: Custom handling for Exception objects (serialize Message + StackTrace + InnerException, not entire object graph)
- [ ] **Parameter value sanitization**: Redact password/credential parameters (replace values with "[REDACTED]" in JSON output)

**Score**: ___ / 4 (requires ≥3 pass)

---

## Section 2: Requirement Clarity

### 2.1 Task Instructions Unambiguous

- [ ] **T107 step 1**: Add AssertProcedureResult method to BaseIntegrationTest.cs with 7 parameters
- [ ] **T107 step 2**: Add CaptureTableRowCounts method to BaseIntegrationTest.cs with params string[] parameter
- [ ] **T107 step 3**: Configure JsonSerializerOptions with WriteIndented=true, add Exception custom converter
- [ ] **T108-T111 integration**: Wrap existing Assert.Equal/Assert.NotNull calls with AssertProcedureResult (capture context before assertion)
- [ ] **Example pattern**: `AssertProcedureResult(ex: null, parameters: testParams, expected: expectedResult, actual: result, executionTimeMs: stopwatch.ElapsedMilliseconds, dbState: dbStateBefore, testMethod: nameof(Test_sp_GetInventory_Success));`

**Score**: ___ / 5 (requires ≥4 pass)

### 2.2 Definitions Consistent

- [ ] **"Verbose diagnostics"**: Comprehensive test failure context (7 fields) that enables troubleshooting without reproducing failure
- [ ] **"Database state"**: Snapshot of table row counts before stored procedure execution (detects unexpected data changes)
- [ ] **"Expected vs Actual"**: Test assertion comparison values (what should happen vs what did happen)
- [ ] **"Execution time"**: Stored procedure duration in milliseconds (from MySqlCommand.ExecuteReader start to end)
- [ ] **"Parameter sanitization"**: Removing sensitive values from diagnostic output (passwords, credentials, PII)

**Score**: ___ / 5 (requires ≥4 pass)

### 2.3 Edge Cases Addressed

- [ ] **Null parameter values**: JSON serialization handles DBNull.Value and C# null gracefully (outputs `"paramName": null`)
- [ ] **Large parameter collections**: Limit parameter JSON to first 50 parameters (if procedure has 100+ params, truncate with "... 52 more parameters")
- [ ] **Exception without stack trace**: Handle exceptions where StackTrace property is null (e.g., manually thrown exceptions)
- [ ] **Database state capture failure**: If CaptureTableRowCounts query fails (table doesn't exist, permission denied), log warning and return empty Dictionary (don't block test execution)
- [ ] **Test method name capture failure**: [CallerMemberName] may return empty string in some scenarios (fallback to "UnknownTestMethod")

**Score**: ___ / 5 (requires ≥4 pass)

---

## Section 3: Requirement Measurability

### 3.1 Quantitative Metrics Defined

- [ ] **T107 duration**: +1 hour to Phase B (base helper implementation + testing)
- [ ] **T108-T111 overhead**: Verbose requirements add ~15% duration per test generation task (integration time for helper calls)
- [ ] **JSON output size target**: <5KB per test failure (ensures log files remain manageable)
- [ ] **Diagnostic capture time**: <100ms overhead per test (CaptureTableRowCounts + JSON serialization should not slow tests significantly)

**Score**: ___ / 4 (requires ≥3 pass)

### 3.2 Qualitative Acceptance Criteria

- [ ] **Seven-field completeness**: Every test failure JSON output contains all 7 fields (Exception, Parameters, Expected, Actual, ExecutionTimeMs, DatabaseState, TestMethod)
- [ ] **JSON structure validity**: Output parses as valid JSON (no syntax errors, proper escaping of special characters)
- [ ] **Human readability**: WriteIndented=true produces formatted JSON with line breaks and indentation (easy to read in test logs)
- [ ] **Developer troubleshooting experience**: QA Lead confirms diagnostic output sufficient to diagnose failure without re-running test (pilot with 5 test failures)
- [ ] **No sensitive data leakage**: Manual audit of test output confirms no passwords or credentials visible in JSON

**Score**: ___ / 5 (requires ≥4 pass)

### 3.3 Success Criteria Traceability

- [ ] **SC-011 mapped to T107**: Test diagnostics include execution context requirement directly fulfilled by 7-field JSON output
- [ ] **FR-022 components traced**: Exception capture (Field 1), parameter capture (Field 2), expected vs actual (Fields 3-4), timing (Field 5), database state (Field 6), test identity (Field 7) all addressed
- [ ] **T108-T111 dependency**: Verbose diagnostics requirement flows into all integration test generation tasks

**Score**: ___ / 3 (requires ≥2 pass)

---

## Section 4: Requirement Testability

### 4.1 Validation Method Specified

- [ ] **Base helper test**: Create integration test that calls AssertProcedureResult with sample data → verify JSON output contains 7 fields
- [ ] **JSON format test**: Parse AssertProcedureResult output with System.Text.Json → verify no deserialization errors
- [ ] **Exception serialization test**: Pass Exception with inner exception to AssertProcedureResult → verify JSON includes inner exception message
- [ ] **Parameter sanitization test**: Pass parameter Dictionary with "password" key → verify JSON shows `"password": "[REDACTED]"`
- [ ] **Database state capture test**: Call CaptureTableRowCounts with valid table names → verify Dictionary contains correct row counts
- [ ] **Database state failure test**: Call CaptureTableRowCounts with invalid table name → verify method returns empty Dictionary, logs warning
- [ ] **Execution time test**: Pass executionTimeMs=1500 → verify JSON field shows `"executionTimeMs": 1500`

**Score**: ___ / 7 (requires ≥5 pass)

### 4.2 Expected Outcomes Documented

- [ ] **Successful JSON generation**: AssertProcedureResult call produces 200-500 character JSON string (varies by parameter count)
- [ ] **Successful test integration**: T108 test class uses AssertProcedureResult in all 4 test methods (success/success-no-data/validation-error/database-error)
- [ ] **Successful database state capture**: CaptureTableRowCounts returns Dictionary with 3-5 table names (typical test scenario)
- [ ] **Successful troubleshooting**: Developer reproduces test failure from JSON diagnostic output alone (no debugger needed)

**Score**: ___ / 4 (requires ≥3 pass)

### 4.3 Failure Scenarios Defined

- [ ] **JSON serialization failure**: Exception object has circular reference → mitigation: custom Exception converter handles this case
- [ ] **Parameter capture performance issue**: Procedure has 200 parameters, JSON serialization takes >500ms → mitigation: truncate to first 50 parameters
- [ ] **Database state capture timeout**: CaptureTableRowCounts query takes >5 seconds → mitigation: add query timeout, log warning, return partial results
- [ ] **Test output stream failure**: Writing JSON to test output throws IOException → mitigation: catch exception, log warning, don't fail test
- [ ] **Sensitive data leak**: Developer accidentally logs plaintext password in parameter → mitigation: code review checklist includes "verify parameter sanitization"

**Score**: ___ / 5 (requires ≥4 pass)

---

## Section 5: Requirement Dependencies

### 5.1 Prerequisite Requirements

- [ ] **BaseIntegrationTest.cs exists**: Abstract base class available for adding helper methods
- [ ] **System.Text.Json available**: NuGet package already referenced in test project
- [ ] **Database connection available**: Integration tests can execute queries for CaptureTableRowCounts

**Score**: ___ / 3 (requires ≥2 pass)

### 5.2 Dependent Requirements

- [ ] **T108 depends on T107**: User authentication test generation requires verbose diagnostics (all tests use AssertProcedureResult)
- [ ] **T109 depends on T107**: Inventory operations test generation requires verbose diagnostics
- [ ] **T110 depends on T107**: Transaction logging test generation requires verbose diagnostics
- [ ] **T111 depends on T107**: Reporting test generation requires verbose diagnostics

**Score**: ___ / 4 (requires ≥3 pass)

### 5.3 Integration Points

- [ ] **xUnit integration**: AssertProcedureResult writes to ITestOutputHelper (xUnit test output stream)
- [ ] **CI/CD integration**: Test failure JSON appears in GitHub Actions test summary (build logs include diagnostic output)
- [ ] **Existing Assert library integration**: AssertProcedureResult wraps xUnit Assert.Equal/Assert.NotNull (calls after capturing diagnostics)

**Score**: ___ / 3 (requires ≥2 pass)

---

## Section 6: Requirement Risks

### 6.1 Technical Risks Identified

- [ ] **JSON serialization performance risk**: Large parameter collections slow down test execution → mitigation: 50-parameter truncation limit, <100ms overhead validation
- [ ] **Diagnostic capture overhead risk**: CaptureTableRowCounts adds significant time to test suite → mitigation: profile test duration, disable for fast-running tests if needed
- [ ] **Exception serialization complexity**: Circular references or deep object graphs cause serialization failure → mitigation: custom Exception converter handles edge cases
- [ ] **Test output size risk**: Verbose diagnostics produce 10MB+ log files in CI/CD → mitigation: 5KB per failure target, compress logs in GitHub Actions

**Score**: ___ / 4 (requires ≥3 pass)

### 6.2 Process Risks Identified

- [ ] **Adoption resistance risk**: Developers find verbose diagnostics too complex, revert to simple Assert calls → mitigation: provide code snippets, automate integration in T108-T111
- [ ] **Sensitive data exposure risk**: Developer logs credentials in test parameters → mitigation: sanitization rules, code review checklist, security audit
- [ ] **Maintenance burden risk**: AssertProcedureResult requires updates for new diagnostic fields → mitigation: version helper method (v1.0 = 7 fields, v2.0 could add more)

**Score**: ___ / 3 (requires ≥2 pass)

### 6.3 Quality Risks Identified

- [ ] **Incomplete diagnostics risk**: Missing field provides insufficient troubleshooting context → mitigation: 7-field design reviewed by QA Lead before T107 execution
- [ ] **Inconsistent usage risk**: Some tests use verbose diagnostics, others don't → mitigation: T108-T111 requirements mandate AssertProcedureResult in all tests
- [ ] **False positive troubleshooting risk**: Diagnostic output misleads developer (e.g., wrong Expected value captured) → mitigation: validate diagnostic accuracy with pilot test failures

**Score**: ___ / 3 (requires ≥2 pass)

---

## Scoring Summary

| Section | Score | Pass Threshold | Status |
|---------|-------|----------------|--------|
| 1. Requirement Completeness | ___ / 19 | ≥15 | ☐ |
| 2. Requirement Clarity | ___ / 15 | ≥12 | ☐ |
| 3. Requirement Measurability | ___ / 12 | ≥10 | ☐ |
| 4. Requirement Testability | ___ / 16 | ≥13 | ☐ |
| 5. Requirement Dependencies | ___ / 10 | ≥8 | ☐ |
| 6. Requirement Risks | ___ / 10 | ≥8 | ☐ |
| **Total** | **___ / 82** | **≥66 (80%)** | **☐** |

---

## Validation Instructions

1. **Pre-Execution Review**: Run this checklist during Checkpoint 2 planning, before T107 execution begins
2. **Scoring**: Check each item as Pass (☑) or Fail (☐), calculate section scores
3. **Gap Analysis**: For any section scoring <80%, document missing requirements in clarification-questions.md
4. **Approval Gate**: All sections must achieve ≥80% before T107 execution approved
5. **Revision Tracking**: Document checklist version and review date at bottom

---

**Checklist Version**: 1.1  
**Last Reviewed**: 2025-10-17  
**Reviewed By**: _________  
**Overall Status**: ☐ PASS | ☐ FAIL | ☐ NEEDS REVISION
