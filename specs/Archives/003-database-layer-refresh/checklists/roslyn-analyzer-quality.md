# Checklist: Roslyn Analyzer for Parameter Prefix Requirements Quality

**Phase**: Part E - Validation and CI/CD Integration (T124a)  
**Purpose**: Validate that Roslyn analyzer requirements are complete, clear, and measurable  
**Type**: Requirements Quality Validation (NOT implementation verification)  
**Created**: 2025-10-15

---

## Overview

This checklist validates the **quality of Roslyn analyzer requirements** as defined in FR-027, SC-016, and T124a. It does NOT test the implementation—that happens during execution. Use this checklist during Checkpoint 5 review (after T124a planning, before analyzer development begins).

**Target Audience**: .NET Architect, DevOps Lead, Development Manager  
**When to Use**: Before starting T124a execution, to ensure requirements are ready  
**Pass Criteria**: All sections score ≥80% (individual items can fail if documented)

---

## Section 1: Requirement Completeness

### 1.1 Diagnostic Rule Definitions

-   [ ] **MTM001 - Missing p\_ prefix**: Detects MySqlParameter construction without "p\_" prefix (e.g., `new MySqlParameter("UserID", ...)` should be `new MySqlParameter("p_UserID", ...)`)
-   [ ] **MTM002 - Inconsistent casing**: Detects parameter name casing mismatch between C# parameter and MySqlParameter (e.g., `userId` parameter → `p_userid` MySqlParameter should be `p_UserId`)
-   [ ] **MTM003 - Missing parameter documentation**: Detects MySqlParameter usage without corresponding XML documentation `<param>` tag (requires XML doc coverage for database parameters)
-   [ ] **MTM004 - Deprecated Helper_Database_StoredProcedure usage**: Detects direct MySqlCommand construction instead of Helper_Database_StoredProcedure wrapper (enforces DAO layer abstraction)

**Score**: \_\_\_ / 4 (requires ≥3 pass)

### 1.2 Code Fix Provider Definitions

-   [ ] **MTM001 fix**: Adds "p\_" prefix to MySqlParameter name (e.g., `"UserID"` → `"p_UserID"`)
-   [ ] **MTM002 fix**: Corrects casing to match C# parameter PascalCase (e.g., `"p_userid"` → `"p_UserId"`)
-   [ ] **MTM003 fix**: Inserts XML `<param>` documentation tag above method with placeholder description
-   [ ] **MTM004 fix**: No automatic fix (manual refactoring required—too complex for code fix)

**Score**: \_\_\_ / 4 (requires ≥3 pass)

### 1.3 Severity Configuration

-   [ ] **Version 1.0.0 (initial rollout)**: All diagnostics configured as **Warning** severity (non-blocking builds, allows gradual adoption)
-   [ ] **Version 2.0.0 (after 30-day adoption)**: MTM001/MTM002/MTM003 upgraded to **Error** severity (blocks builds), MTM004 remains Warning
-   [ ] **Configuration override**: Allow `.editorconfig` override for legacy code (disable MTM001-MTM004 in specific files if needed)
-   [ ] **CI/CD integration**: GitHub Actions workflow runs analyzer during PR builds (before merge approval)

**Score**: \_\_\_ / 4 (requires ≥3 pass)

### 1.4 Analyzer Packaging

- [ ] **NuGet package name**: MTM.CodeAnalysis.DatabaseParameters
- [ ] **Target framework**: .NET Standard 2.0 (compatible with Visual Studio 2019+, Rider, VS Code OmniSharp)
- [ ] **Package dependencies**: Microsoft.CodeAnalysis.CSharp v4.0.1 (Roslyn SDK), Microsoft.CodeAnalysis.Analyzers v3.3.3
- [ ] **Installation method**: Add `<PackageReference Include="MTM.CodeAnalysis.DatabaseParameters" Version="1.0.0" />` to MTM_WIP_Application_Winforms.csproj

**Score**: \_\_\_ / 4 (requires ≥3 pass)

---

## Section 2: Requirement Clarity

### 2.1 Task Instructions Unambiguous

-   [ ] **T124a step 1**: Create .NET Standard 2.0 library project named `MTM.CodeAnalysis.DatabaseParameters`
-   [ ] **T124a step 2**: Implement DiagnosticAnalyzer subclass for each rule (MTM001DiagnosticAnalyzer, MTM002DiagnosticAnalyzer, etc.)
-   [ ] **T124a step 3**: Implement CodeFixProvider for MTM001/MTM002/MTM003 (MTM004 has no fix)
-   [ ] **T124a step 4**: Configure severity in `.editorconfig` (Warning for v1.0.0, upgrade to Error in v2.0.0)
-   [ ] **T124a step 5**: Package as NuGet, publish to internal NuGet feed (or local folder for pilot), install in MTM project
-   [ ] **T124a step 6**: Test analyzer in Visual Studio IDE (verify Quick Actions lightbulb appears for violations)

**Score**: \_\_\_ / 6 (requires ≥5 pass)

### 2.2 Definitions Consistent

-   [ ] **"Diagnostic"**: Static analysis rule that detects code pattern violation (e.g., missing "p\_" prefix)
-   [ ] **"Code Fix"**: Automated refactoring triggered by IDE Quick Actions to resolve diagnostic
-   [ ] **"Severity"**: Warning (non-blocking) vs Error (blocks build) vs Info (informational only)
-   [ ] **"Roslyn analyzer"**: .NET compiler platform extension that runs during compilation and provides real-time IDE feedback
-   [ ] **"MySqlParameter construction"**: Any code creating `new MySqlParameter(...)` or assigning to MySqlCommand.Parameters collection

**Score**: \_\_\_ / 5 (requires ≥4 pass)

### 2.3 Edge Cases Addressed

-   [ ] **String interpolation in parameter names**: `new MySqlParameter($"p_{columnName}", ...)` → MTM001 should NOT flag (already has prefix)
-   [ ] **Parameter name from variable**: `string paramName = "UserID"; new MySqlParameter(paramName, ...)` → MTM001 should flag with warning (cannot auto-fix, recommend constant)
-   [ ] **Legacy code exemption**: Files in `Database/CurrentStoredProcedures/` folder excluded from analysis (legacy code preserved as-is)
-   [ ] **Test project exemption**: Integration test setup code excluded from MTM004 (allowed to use MySqlCommand directly for test infrastructure)
-   [ ] **Roslyn version compatibility**: Analyzer built against Microsoft.CodeAnalysis.CSharp v4.0.1 (compatible with VS 2022, VS Code C# extension, Rider 2023+)

**Score**: \_\_\_ / 5 (requires ≥4 pass)

---

## Section 3: Requirement Measurability

### 3.1 Quantitative Metrics Defined

-   [ ] **T124a development time**: 2-3 hours (0.25-0.375 days) for 4 diagnostics + 3 code fixes + packaging
-   [ ] **False positive rate target**: <5% (manually review 20 violations across codebase, expect ≤1 false positive)
-   [ ] **Violation detection rate**: 100% of manually identified violations should trigger diagnostics (run analyzer on known violation examples)
-   [ ] **IDE integration response time**: <1 second for diagnostic to appear after typing violation (real-time feedback requirement)
-   [ ] **Build time overhead**: <10% increase in CI/CD build duration (analyzer adds ~5-10 seconds to typical 60-second build)

**Score**: \_\_\_ / 5 (requires ≥4 pass)

### 3.2 Qualitative Acceptance Criteria

-   [ ] **Zero false positives on core DAO classes**: Run analyzer on Data/Dao\_\*.cs files, verify all flagged violations are legitimate
-   [ ] **Code fix correctness**: Apply auto-fixes to 10 violations, verify resulting code compiles and passes unit tests
-   [ ] **IDE integration quality**: Diagnostic appears in Error List window, Quick Actions lightbulb shows fix, Ctrl+. triggers code fix
-   [ ] **Documentation completeness**: NuGet package README documents all 4 diagnostics with examples and fix guidance
-   [ ] **CI/CD integration success**: GitHub Actions workflow fails PR build when MTM001/MTM002/MTM003 violations exist (after v2.0.0 upgrade)

**Score**: \_\_\_ / 5 (requires ≥4 pass)

### 3.3 Success Criteria Traceability

-   [ ] **SC-016 mapped to T124a**: Zero parameter prefix violations in CI/CD requirement directly fulfilled by analyzer Error severity
-   [ ] **FR-027 components traced**: Static analysis (diagnostics), real-time IDE feedback (Roslyn integration), build-time validation (CI/CD integration) all addressed
-   [ ] **R-NEW-4 risk mitigation**: False positive risk addressed by manual review and .editorconfig override capability

**Score**: \_\_\_ / 3 (requires ≥2 pass)

---

## Section 4: Requirement Testability

### 4.1 Validation Method Specified

-   [ ] **Analyzer execution test**: Create test file with known violations → run analyzer → verify diagnostics appear in Error List
-   [ ] **MTM001 detection test**: `new MySqlParameter("UserID", 1)` → verify MTM001 diagnostic triggered
-   [ ] **MTM002 detection test**: C# parameter `userId` + `new MySqlParameter("p_userid", userId)` → verify MTM002 diagnostic triggered
-   [ ] **MTM003 detection test**: Method with MySqlParameter but no XML `<param>` documentation → verify MTM003 diagnostic triggered
-   [ ] **MTM004 detection test**: `new MySqlCommand("sp_GetUser", connection)` outside Helper wrapper → verify MTM004 diagnostic triggered
-   [ ] **Code fix test - MTM001**: Apply Quick Action → verify `"UserID"` becomes `"p_UserID"`
-   [ ] **Code fix test - MTM002**: Apply Quick Action → verify `"p_userid"` becomes `"p_UserId"` (matches C# parameter casing)
-   [ ] **Code fix test - MTM003**: Apply Quick Action → verify XML `<param name="p_UserId">TODO: Document parameter</param>` inserted
-   [ ] **Severity configuration test**: Change `.editorconfig` severity to Error → rebuild → verify build fails with error, not warning

**Score**: \_\_\_ / 9 (requires ≥7 pass)

### 4.2 Expected Outcomes Documented

-   [ ] **Successful diagnostic detection**: Analyzer flags 15-25 violations across MTM codebase (estimated based on manual code review)
-   [ ] **Successful code fix application**: 80%+ of violations fixed via Quick Actions (MTM004 requires manual refactoring)
-   [ ] **Successful IDE integration**: Visual Studio shows green squiggly underline for Warnings, red squiggly for Errors
-   [ ] **Successful CI/CD integration**: GitHub Actions logs show "MTM001: Missing p\_ prefix on parameter 'UserID' in Dao_User.cs:45" during PR build
-   [ ] **Successful phased rollout**: v1.0.0 Warning severity allows gradual cleanup, v2.0.0 Error severity enforces compliance

**Score**: \_\_\_ / 5 (requires ≥4 pass)

### 4.3 Failure Scenarios Defined

-   [ ] **Analyzer installation failure**: NuGet package not found → fallback to manual code review checklist (T124 baseline validation)
-   [ ] **False positive overload**: >20% violations are false positives → escalate to disable analyzer, revise diagnostic logic
-   [ ] **Code fix breaks compilation**: Auto-fix introduces syntax error → rollback fix, add to known limitations documentation
-   [ ] **IDE integration failure**: Roslyn SDK version mismatch → test against VS 2022 17.8+ and VS Code C# extension v2.0+
-   [ ] **Developer friction**: Team disables analyzer in `.editorconfig` → escalate to management for compliance enforcement discussion
-   [ ] **Build time regression**: CI/CD build duration >10% increase → profile analyzer performance, optimize or disable slow diagnostics

**Score**: \_\_\_ / 6 (requires ≥5 pass)

---

## Section 5: Requirement Dependencies

### 5.1 Prerequisite Requirements

-   [ ] **.NET SDK 8.0+**: Required to build .NET Standard 2.0 library and run Roslyn analyzers
-   [ ] **Visual Studio 2022 17.8+**: Required for analyzer development and IDE testing (includes Roslyn SDK)
-   [ ] **T124 complete**: Baseline validation script established (provides violation examples for analyzer testing)

**Score**: \_\_\_ / 3 (requires ≥2 pass)

### 5.2 Dependent Requirements

-   [ ] **T125 depends on T124a**: Final pre-deployment validation uses analyzer as automated check (supplements manual T124 script)
-   [ ] **CI/CD pipeline depends on T124a**: GitHub Actions workflow includes analyzer NuGet package installation step
-   [ ] **Developer onboarding depends on T124a**: New developer setup includes analyzer installation (enforces standards from day 1)

**Score**: \_\_\_ / 3 (requires ≥2 pass)

### 5.3 Integration Points

-   [ ] **Visual Studio IDE integration**: Analyzer DLL loaded by Roslyn compiler, diagnostics appear in Error List, Quick Actions available via Ctrl+.
-   [ ] **MSBuild integration**: Analyzer runs during `dotnet build`, violations logged to console output
-   [ ] **GitHub Actions integration**: Analyzer NuGet package restored in CI/CD, violations fail PR merge if severity=Error
-   [ ] **Helper_Database_StoredProcedure integration**: MTM004 diagnostic references Helper wrapper class (must remain stable for analyzer rule validity)

**Score**: \_\_\_ / 4 (requires ≥3 pass)

---

## Section 6: Requirement Risks

### 6.1 Technical Risks Identified

-   [ ] **R-NEW-4 documented**: False positive risk (analyzer flags legitimate code patterns as violations) addressed in risk assessment
-   [ ] **Mitigation - manual review**: False positive rate validation test (manual review of 20 violations) detects analyzer flaws
-   [ ] **Mitigation - configuration override**: `.editorconfig` exemption allows disabling analyzer for legacy/special-case files
-   [ ] **Mitigation - phased rollout**: Warning severity in v1.0.0 allows pilot testing before Error enforcement in v2.0.0

**Score**: \_\_\_ / 4 (requires ≥3 pass)

### 6.2 Process Risks Identified

-   [ ] **Developer friction risk**: Team perceives analyzer as productivity blocker → mitigation: gather feedback during 30-day Warning period, adjust rules if needed
-   [ ] **Disabled analyzer risk**: Developers bypass analyzer by removing NuGet package or disabling in `.editorconfig` → mitigation: CI/CD enforces analyzer presence, PR review checks for .editorconfig changes
-   [ ] **Maintenance burden risk**: Analyzer requires updates for new Roslyn versions or C# language features → mitigation: allocate quarterly maintenance window for analyzer updates
-   [ ] **NuGet package distribution risk**: Internal NuGet feed downtime prevents analyzer installation → mitigation: provide fallback local NuGet package folder in repository

**Score**: \_\_\_ / 4 (requires ≥3 pass)

### 6.3 Quality Risks Identified

-   [ ] **Incomplete violation detection**: Analyzer misses violations due to complex code patterns → mitigation: supplement with manual T124 validation script
-   [ ] **Code fix correctness risk**: Auto-fix introduces subtle bugs → mitigation: require integration test pass after bulk code fix application
-   [ ] **IDE performance degradation**: Analyzer slows down IntelliSense or background compilation → mitigation: profile analyzer with PerfView, optimize hot paths
-   [ ] **Versioning risk**: v1.0.0 → v2.0.0 severity upgrade breaks developer workflows → mitigation: announce upgrade 2 weeks in advance, provide cleanup sprint time

**Score**: \_\_\_ / 4 (requires ≥3 pass)

---

## Scoring Summary

| Section                      | Score           | Pass Threshold | Status |
| ---------------------------- | --------------- | -------------- | ------ |
| 1. Requirement Completeness  | \_\_\_ / 16     | ≥13            | ☐      |
| 2. Requirement Clarity       | \_\_\_ / 16     | ≥13            | ☐      |
| 3. Requirement Measurability | \_\_\_ / 13     | ≥10            | ☐      |
| 4. Requirement Testability   | \_\_\_ / 20     | ≥16            | ☐      |
| 5. Requirement Dependencies  | \_\_\_ / 10     | ≥8             | ☐      |
| 6. Requirement Risks         | \_\_\_ / 12     | ≥10            | ☐      |
| **Total**                    | **\_\_\_ / 87** | **≥70 (80%)**  | **☐**  |

---

## Validation Instructions

1. **Pre-Execution Review**: Run this checklist during Checkpoint 5 planning, before T124a execution begins
2. **Scoring**: Check each item as Pass (☑) or Fail (☐), calculate section scores
3. **Gap Analysis**: For any section scoring <80%, document missing requirements in clarification-questions.md
4. **Approval Gate**: All sections must achieve ≥80% before T124a execution approved
5. **Revision Tracking**: Document checklist version and review date at bottom

---

**Checklist Version**: 1.0  
**Last Reviewed**: ****\_****  
**Reviewed By**: ****\_****  
**Overall Status**: ☐ PASS | ☐ FAIL | ☐ NEEDS REVISION
