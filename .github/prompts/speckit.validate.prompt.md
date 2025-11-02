---
description: Validate implementation completeness by comparing delivered code against spec requirements, generating remediation tasks for any gaps discovered.
---

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt performs comprehensive validation across multiple dimensions. To maximize value:

- **Run all validation passes** (requirements, tasks, code quality, tests, constitution, documentation, edge cases, integration) in a single session
- **Generate complete validation report** with all findings categorized and prioritized
- **Auto-generate remediation tasks** for gaps without stopping for approval
- **Continue through all MCP tool validations** (DAO patterns, XML docs, security, performance) without pausing
- **Provide actionable next steps** with specific commands to fix issues
- **Only stop when validation complete** or when critical blockers require immediate user attention

**Do NOT stop after one validation dimension** - complete all checks and generate full report with remediation tasks in one session.

---

## Required MCP Tools

This prompt leverages ALL available MCP tools from the **mtm-workflow** server for comprehensive validation:

### Task & Spec Analysis
- `parse_tasks` - Parse tasks.md structure and completion status
- `analyze_spec_context` - Extract spec requirements and context
- `load_instructions` - Verify instruction file references
- `check_checklists` - Validate quality gate checklists

### Code Quality Validation
- `validate_dao_patterns` - Verify DAO compliance with MTM standards
- `validate_error_handling` - Check Service_ErrorHandler adoption
- `check_xml_docs` - Validate documentation coverage
- `validate_build` - Verify compilation and run tests

### Security & Performance
- `check_security` - Security vulnerability scanning
- `analyze_performance` - Performance bottleneck detection

### Database Analysis
- `analyze_stored_procedures` - SQL compliance checking
- `analyze_dependencies` - Procedure call hierarchy mapping
- `compare_databases` - Schema drift detection

### Project Structure
- `verify_ignore_files` - Check .gitignore completeness

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Validation Modes

The validation depth is controlled by command-line flags:

### Mode Flags

**--full** (default if no flag specified)
- Validates all 8 dimensions comprehensively
- Runs all MCP tools for complete quality assessment
- Checks every requirement against implementation
- Suitable for: Final validation, PR readiness, release gates

**--quick**
- Critical path validation only (P1 user stories, blocking requirements)
- Essential MCP tools only (build, DAO patterns, security)
- Faster execution for iterative development
- Suitable for: Mid-development checks, quick sanity validation

**--T{TaskNumber}** (e.g., --T042, --T100)
- Validates specific task implementation
- Checks only requirements/files related to that task
- Runs relevant MCP tools based on task type
- Suitable for: Task-level validation, focused debugging

**--phase {N}** (e.g., --phase 2)
- Validates all tasks in specified phase
- Checks requirements covered by that phase
- Phase-appropriate MCP tool suite
- Suitable for: Phase gate validation, incremental delivery

**--requirements-only**
- Validates requirement coverage without code quality checks
- Skips MCP tools, focuses on spec-to-code traceability
- Fastest validation mode
- Suitable for: Initial gap detection, planning remediation

**--quality-only**
- Runs all MCP quality tools without requirement tracing
- Validates code standards, security, performance, docs
- Skips spec comparison
- Suitable for: Code review preparation, technical debt assessment

**--completed-only**
- Validates only tasks marked [X] as complete
- Verifies completed tasks have actual implementations (not just checked off)
- Focuses on Task Completion Verification dimension (dimension B)
- Runs relevant MCP tools based on completed task types
- Suitable for: Checking off accuracy, catching premature task completion, pre-merge verification

### Flag Combinations

Flags can be combined for custom validation scope:
```bash
/speckit.validate --phase 2 --quality-only
/speckit.validate --T042 --T043 --T044
/speckit.validate --quick --requirements-only
/speckit.validate --completed-only --quality-only
```

## Execution Steps

### 1. Initialize Validation Context

Run `.specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks` once from repo root and parse JSON for FEATURE_DIR and AVAILABLE_DOCS. Derive absolute paths:

- SPEC = FEATURE_DIR/spec.md
- PLAN = FEATURE_DIR/plan.md
- TASKS = FEATURE_DIR/tasks.md
- WORKSPACE_ROOT = repository root
- CHECKLISTS_DIR = FEATURE_DIR/checklists (if exists)

Abort if spec.md or tasks.md missing (instruct user to run prerequisite commands).

### 2. Parse Validation Mode

Extract mode from `$ARGUMENTS`:
- Default: `--full` (if no flags)
- Parse flags: `--quick`, `--full`, `--T###`, `--phase N`, `--requirements-only`, `--quality-only`, `--completed-only`
- Validate flag combinations (error if contradictory)
- Set validation scope based on mode

### 3. Load Artifacts (Progressive Disclosure)

**From spec.md:**
- Functional Requirements (with IDs if present)
- Non-Functional Requirements
- User Stories (with priority tags P1/P2/P3)
- Edge Cases
- Acceptance Criteria

**From tasks.md using `parse_tasks` MCP tool:**
- Task IDs, descriptions, completion status
- Phase grouping and dependencies
- File paths referenced in tasks
- Instruction file references

**From plan.md (if exists):**
- Architecture decisions
- Data model entities
- API contracts
- Technical constraints

**From checklists using `check_checklists` MCP tool:**
- Quality gate status
- Incomplete prerequisite items

### 4. Build Validation Models

Create internal semantic models (do not dump raw content):

**Requirements Inventory:**
- Key: Requirement slug or ID
- Source: spec.md section reference
- Priority: P1/P2/P3/None
- Type: Functional/Non-Functional/Edge Case
- Acceptance Criteria: Measurable outcomes

**Task Coverage Map:**
- Task ID → Requirement(s) it implements
- Use keyword matching, explicit references, file path analysis
- Mark orphan tasks (no requirement) and orphan requirements (no task)

**Code Artifact Map:**
- File paths created/modified during implementation
- Link files to tasks and requirements
- Detect files created outside task scope

**Quality Gate Matrix:**
- MCP tool → Pass/Fail/Warning status
- Issue count by severity
- Remediation recommendations

### 5. Validation Dimensions (8 Comprehensive Checks)

Run checks based on validation mode. For `--full`, execute all dimensions. For `--quick`, run dimensions 1, 2, 3, 5 only. For task-specific modes, run relevant dimensions.

#### A. Requirement Coverage Validation

**Goal**: Verify all spec requirements have corresponding implementation

**Process**:
1. Extract all requirements from spec.md (FR, NFR, User Stories)
2. For each requirement:
   - Find tasks claiming to implement it
   - Check if those tasks are marked complete [X]
   - Verify file paths in tasks actually exist
   - If requirement has acceptance criteria, check if testable artifacts exist
3. Classify results:
   - **✅ Covered**: Requirement has completed task(s) with existing files
   - **⚠️ Partial**: Tasks exist but not all marked complete
   - **❌ Missing**: No tasks reference this requirement
   - **🔍 Untestable**: Requirement has no measurable acceptance criteria

**Output**: Coverage table with requirement key, status, task IDs, gaps

#### B. Task Completion Validation

**Goal**: Verify tasks marked [X] are actually implemented (not just checked off)

**Process**:
1. For each task marked complete:
   - Extract file path from task description
   - Check if file exists in workspace
   - If file exists, verify it contains relevant implementation:
     - For models: Check class definition exists
     - For DAOs: Check method signatures exist
     - For forms: Check designer file and code-behind exist
     - For tests: Check test methods exist
   - If task references instruction file, verify patterns are followed
2. Classify results:
   - **✅ Verified**: Task complete AND implementation exists
   - **⚠️ Questionable**: Task complete BUT file missing or stub-only
   - **❌ False Positive**: Task checked off but no evidence of completion

**Output**: Task verification table with task ID, claimed status, actual status, gaps

#### C. Code Quality Validation

**Goal**: Ensure implemented code follows MTM coding standards

**MCP Tools to Run**:
1. **validate_dao_patterns** (Data/ directory)
   - Check: Region organization, Helper_Database_StoredProcedure usage, async patterns, Service_ErrorHandler, XML docs
   - Severity: Error for missing required patterns, Warning for style issues

2. **validate_error_handling** (all C# files)
   - Check: MessageBox.Show usage (anti-pattern), Service_ErrorHandler adoption, try-catch in async methods
   - Severity: Error for MessageBox.Show, Warning for missing error handling

3. **check_xml_docs** (all C# files)
   - Check: Public API documentation coverage
   - Threshold: 80% minimum (configurable in instruction files)
   - Severity: Warning if below threshold

4. **validate_build** (workspace root)
   - Check: Compilation succeeds, no errors
   - Run tests: If test tasks exist
   - Severity: Error for build failures, Warning for test failures

**Output**: Quality metrics table with tool, pass/fail, issue count, severity breakdown

#### D. Test Coverage Validation

**Goal**: Verify spec requirements have corresponding tests (if tests were requested)

**Process**:
1. Determine if testing was in scope:
   - Check tasks.md for test tasks (grep for "test", "Test_", ".Tests")
   - Check spec.md for testing requirements
   - If no test tasks exist, mark this dimension as "N/A - Tests not in scope"
2. If tests in scope:
   - For each requirement with acceptance criteria:
     - Find test tasks claiming to verify it
     - Check if test files exist (Tests/ directory)
     - Verify test methods exist (grep for [TestMethod], [Fact], [Test])
     - Check if tests actually exercise the requirement (keyword analysis)
3. Classify results:
   - **✅ Tested**: Requirement has passing test(s)
   - **⚠️ Test Exists**: Test file exists but may not cover requirement fully
   - **❌ Untested**: Requirement has acceptance criteria but no tests
   - **➖ N/A**: Requirement not testable or tests out of scope

**Output**: Test coverage table with requirement key, test files, test methods, gaps

#### E. Constitution Compliance Validation

**Goal**: Ensure implementation honors project constitution principles

**Process**:
1. Load `.specify/memory/constitution.md`
2. Extract all MUST and SHOULD principle statements
3. For each principle:
   - Determine which code artifacts it applies to
   - Check if those artifacts follow the principle
   - Use MCP tools for automated checks where applicable:
     - Security principle → run `check_security`
     - Performance principle → run `analyze_performance`
     - Database principle → run `analyze_stored_procedures`
4. Classify results:
   - **✅ Compliant**: Principle followed in all applicable code
   - **⚠️ Partial**: Some violations found, not critical
   - **❌ Violation**: Critical MUST principle violated
   - **🔍 Manual Review**: Principle requires human judgment

**Output**: Constitution compliance table with principle, status, violations, severity

#### F. Documentation Validation

**Goal**: Verify implementation includes required documentation

**Checks**:
1. **XML Documentation** (via `check_xml_docs`)
   - Public classes, methods, properties documented
   - Parameters, returns, exceptions documented
   - Coverage meets 80%+ threshold

2. **README Updates** (if applicable)
   - Feature mentioned in README
   - Usage examples provided
   - Configuration documented

3. **Architecture Docs** (plan.md, data-model.md)
   - Reflects as-built implementation
   - No outdated architecture decisions
   - Diagrams/schemas match code

4. **Code Comments**
   - Complex logic explained
   - TODO markers resolved or tracked
   - No commented-out code

**Output**: Documentation completeness table with doc type, status, gaps

#### G. Edge Case Coverage Validation

**Goal**: Ensure spec edge cases are handled in implementation

**Process**:
1. Extract edge cases from spec.md (usually in dedicated section or scattered in requirements)
2. For each edge case:
   - Find tasks/code claiming to handle it
   - Check implementation for explicit handling:
     - Error handling code (try-catch, validation)
     - Boundary condition checks (null checks, empty collections, range validation)
     - Fallback behaviors (default values, graceful degradation)
   - Look for tests that exercise the edge case
3. Classify results:
   - **✅ Handled**: Edge case has explicit handling in code and tests
   - **⚠️ Implicit**: Code may handle it but not explicitly documented
   - **❌ Unhandled**: No evidence of edge case handling
   - **🔍 Needs Verification**: Handling exists but needs manual testing

**Output**: Edge case coverage table with case description, handling status, gaps

#### H. Integration Point Validation

**Goal**: Verify components integrate correctly according to plan

**Checks**:
1. **Database Integration**:
   - Run `analyze_stored_procedures` - check compliance
   - Run `compare_databases` - check for schema drift
   - Verify all DAOs reference existing procedures
   - Check connection string configuration

2. **Service Integration**:
   - Check dependency injection setup
   - Verify service contracts match implementations
   - Test inter-service communication paths

3. **UI Integration**:
   - Verify forms wire up to services/DAOs
   - Check event handlers call correct methods
   - Validate data binding works

4. **External Integration** (if applicable):
   - API endpoints exist
   - External service calls have error handling
   - Integration tests pass (if present)

**Output**: Integration health table with integration type, status, issues

### 6. Gap Classification & Severity

Assign severity to all findings:

**CRITICAL** (🔴):
- P1 requirement with no implementation
- Constitution MUST principle violated
- Build failures or critical security issues
- Blocking integration failures

**HIGH** (🟠):
- P2 requirement with no implementation
- Test coverage below 50% for testable requirements
- Performance issues in critical paths
- Missing error handling in async methods

**MEDIUM** (🟡):
- P3 requirement with no implementation
- Documentation coverage below 80%
- Edge cases not explicitly handled
- Code quality warnings (style, unused code)

**LOW** (🟢):
- Minor documentation gaps
- Non-critical TODO markers
- Style inconsistencies
- Optional optimizations

### 7. Generate Validation Report

Create comprehensive Markdown report at `FEATURE_DIR/validation-report-{DATE}.md`:

```markdown
# Implementation Validation Report

**Feature**: [Feature Name from spec.md]
**Date**: [Current Date]
**Mode**: [Validation mode: full/quick/task-specific]
**Branch**: [Current git branch]

## Executive Summary

**Overall Status**: ✅ PASS | ⚠️ PASS WITH WARNINGS | ❌ FAIL

- Total Requirements: X
- Requirements Covered: Y (Z%)
- Critical Issues: N
- High Priority Issues: M
- Medium/Low Issues: K

---

## Dimension Results

### 1. Requirement Coverage

| Requirement | Priority | Status | Task IDs | Gap Description |
|-------------|----------|--------|----------|-----------------|
| [Req slug]  | P1       | ✅     | T001, T002 | - |
| [Req slug]  | P1       | ❌     | -      | No implementing tasks |

**Summary**: X/Y requirements covered (Z%)
**Action Needed**: See Remediation Tasks section

---

### 2. Task Completion Verification

| Task ID | Description | Status | Verification | Issue |
|---------|-------------|--------|--------------|-------|
| T001    | Create model | ✅ [X] | ✅ Verified | - |
| T042    | Implement DAO | ✅ [X] | ❌ File missing | File path incorrect |

**Summary**: X/Y tasks verified complete
**Action Needed**: See Remediation Tasks section

---

### 3. Code Quality Gates

#### DAO Pattern Compliance
**Status**: ⚠️ PASS WITH WARNINGS
- Errors: 0
- Warnings: 3
- Issues: [List from validate_dao_patterns tool]

#### Error Handling
**Status**: ❌ FAIL
- MessageBox.Show usage found: 2 instances
- Missing Service_ErrorHandler: 5 methods
- Issues: [List from validate_error_handling tool]

#### XML Documentation
**Status**: ✅ PASS
- Coverage: 87% (threshold: 80%)
- Undocumented members: 12

#### Build Status
**Status**: ✅ PASS
- Compilation: Success
- Tests: 45/45 passed

---

### 4. Test Coverage

| Requirement | Acceptance Criteria | Test File | Test Methods | Status |
|-------------|---------------------|-----------|--------------|--------|
| [Req slug]  | User can login      | User_Tests.cs | Login_Valid_Succeeds | ✅ |
| [Req slug]  | Handles invalid input | - | - | ❌ No test |

**Summary**: X/Y requirements tested (Z%)
**Status**: ⚠️ Test coverage below threshold

---

### 5. Constitution Compliance

| Principle | Status | Violations | Severity |
|-----------|--------|------------|----------|
| [Principle name] | ✅ | 0 | - |
| [Principle name] | ❌ | 2 | CRITICAL |

**Critical Violations**:
- [Principle]: [Specific violation description with file:line]

---

### 6. Documentation Completeness

| Documentation Type | Status | Coverage | Gaps |
|--------------------|--------|----------|------|
| XML Docs | ✅ | 87% | 12 undocumented members |
| README | ⚠️ | Partial | Missing usage examples |
| Architecture Docs | ✅ | Complete | - |

---

### 7. Edge Case Coverage

| Edge Case | Status | Implementation | Test |
|-----------|--------|----------------|------|
| [Description] | ✅ | File.cs:123 | Test.cs:45 |
| [Description] | ❌ | Missing | Missing |

**Summary**: X/Y edge cases handled (Z%)

---

### 8. Integration Health

| Integration Type | Status | Issues |
|------------------|--------|--------|
| Database | ✅ | None |
| Services | ⚠️ | 2 warnings |
| UI | ✅ | None |
| External APIs | ➖ N/A | Not in scope |

---

## Critical Issues (Must Fix Before Merge)

1. **🔴 CRITICAL**: [Issue description]
   - **Location**: [File:line or requirement ID]
   - **Impact**: [Why this is critical]
   - **Fix**: [Recommended action]
   - **Task**: T### (auto-generated remediation task)

2. **🔴 CRITICAL**: [Issue description]
   ...

---

## High Priority Issues (Should Fix Before Merge)

1. **🟠 HIGH**: [Issue description]
   - **Location**: [File:line]
   - **Impact**: [Why this matters]
   - **Fix**: [Recommended action]
   - **Task**: T### (auto-generated remediation task)

---

## Medium/Low Priority Issues (Can Fix Later)

1. **🟡 MEDIUM**: [Issue description]
   - **Task**: T### (optional remediation task)

---

## Remediation Tasks (Auto-Generated)

The following tasks have been added to tasks.md to address validation gaps:

- [ ] **T### [Story: Remediation]** - [Task description]
  **File**: `path/to/file.ext`
  **Description**: [What needs to be implemented/fixed]
  **Reference**: `.github/instructions/[relevant].instructions.md` - [Guidance]
  **Acceptance**: [How to verify fix]
  **Validation Issue**: [Link to issue above - e.g., "Addresses Critical Issue #1"]

---

## Next Steps

### If Status = ✅ PASS
- Ready for code review and merge
- Consider addressing medium/low priority issues in follow-up PRs

### If Status = ⚠️ PASS WITH WARNINGS
- Address high priority issues before merge
- Review critical issues for risk assessment
- Consider running `/speckit.validate --full` after fixes

### If Status = ❌ FAIL
- **MUST** address all critical issues before merge
- Run `/speckit.validate --T###` for specific task validation after fixes
- Re-run full validation after remediation: `/speckit.validate --full`

### Recommended Commands

```bash
# Fix specific task and re-validate
/speckit.validate --T###

# After fixing critical issues, re-validate quality gates
/speckit.validate --quality-only

# Full re-validation before merge
/speckit.validate --full
```

---

## Validation Metadata

**Validation Mode**: [full/quick/task-specific]
**MCP Tools Executed**: [List of MCP tools run]
**Total Findings**: [Count by severity]
**Execution Time**: [Duration]
**Generated**: [Timestamp]
```

### 8. Auto-Generate Remediation Tasks

For each gap/issue found (CRITICAL and HIGH only), generate remediation task and append to tasks.md:

**Task Generation Rules**:
1. **Task Numbering**: Continue from highest existing task number
2. **Phase Assignment**: Add to new "Phase X: Remediation" at end of tasks.md
3. **Story Tag**: Use `[Story: Remediation]` or `[Story: Fix]`
4. **Priority**: CRITICAL issues get priority, HIGH issues next
5. **Task Format**:
   ```markdown
   - [ ] **T### [Story: Remediation]** - Fix [Issue Description]
     **File**: `path/to/file.ext` (if applicable)
     **Description**: [Detailed fix description with context from validation]
     **Reference**: `.github/instructions/[relevant].instructions.md` - [Specific guidance]
     **Acceptance**: [How to verify fix - specific, testable]
     **Validation Issue**: [Link back to validation report issue number]
   ```

6. **Instruction File Mapping**:
   - DAO issues → `csharp-dotnet8.instructions.md`, `mysql-database.instructions.md`
   - Error handling → `csharp-dotnet8.instructions.md`, instruction file with Service_ErrorHandler patterns
   - Documentation → `documentation.instructions.md`
   - Security → `security-best-practices.instructions.md`
   - Performance → `performance-optimization.instructions.md`
   - Testing → `testing-standards.instructions.md` or `integration-testing.instructions.md`

7. **Append to tasks.md**:
   - Add new phase: `## Phase X: Remediation (Auto-Generated from Validation)`
   - Add header: `Generated by /speckit.validate on [DATE]`
   - List all remediation tasks
   - Update task count summary at top of tasks.md

### 9. Output Validation Summary

After generating report and remediation tasks, output to terminal:

```
╔══════════════════════════════════════════════════════════════╗
║          IMPLEMENTATION VALIDATION COMPLETE                   ║
╚══════════════════════════════════════════════════════════════╝

📊 Validation Results:
   Status: [✅ PASS | ⚠️ PASS WITH WARNINGS | ❌ FAIL]
   
   Requirements Coverage: X/Y (Z%)
   Task Verification: X/Y verified
   Code Quality: [PASS/FAIL]
   Test Coverage: X/Y tested (Z%)
   Constitution: [COMPLIANT/VIOLATIONS]
   
🔴 Critical Issues: N
🟠 High Priority: M  
🟡 Medium Priority: K
🟢 Low Priority: L

📄 Report: FEATURE_DIR/validation-report-{DATE}.md
📋 Remediation: N tasks added to tasks.md (Phase X)

🎯 Next Steps:
   [Specific next steps based on status]
   
   Recommended Commands:
   - /speckit.validate --T### (validate specific task fixes)
   - /speckit.validate --quality-only (re-run quality gates)
   - /speckit.validate --full (complete re-validation)
```

### 10. Handle Validation Modes

**Mode-Specific Behavior**:

**--full** (default):
- Execute all 8 validation dimensions
- Run all MCP tools
- Generate complete report
- Auto-generate all remediation tasks

**--quick**:
- Execute dimensions: 1 (Requirements), 2 (Tasks), 3 (Code Quality - build + DAO only), 5 (Constitution - MUST principles only)
- Run MCP tools: validate_build, validate_dao_patterns, check_security
- Generate abbreviated report
- Auto-generate CRITICAL remediation tasks only

**--T{TaskNumber}**:
- Extract task from tasks.md using `parse_tasks`
- Identify requirements task claims to implement
- Execute dimensions relevant to task type:
  - DAO task → validate_dao_patterns, analyze_stored_procedures
  - UI task → validate_build, check for theme integration
  - Test task → verify test exists and runs
  - Model task → validate_build, check file exists
- Generate focused report for that task only
- Auto-generate remediation tasks for that task's gaps only

**--phase {N}**:
- Extract all tasks in specified phase using `parse_tasks`
- Identify requirements covered by phase tasks
- Execute all dimensions for phase scope
- Run all MCP tools on phase-related files
- Generate phase-focused report
- Auto-generate phase remediation tasks

**--requirements-only**:
- Execute dimensions: 1 (Requirements), 2 (Tasks), 4 (Tests), 7 (Edge Cases)
- Skip MCP tool execution
- Focus on spec-to-code traceability
- Generate coverage report only
- Auto-generate gap-filling tasks

**--quality-only**:
- Execute dimension: 3 (Code Quality) only
- Run all MCP tools comprehensively
- Skip requirement tracing
- Generate quality metrics report
- Auto-generate code quality remediation tasks

**--completed-only**:
- Execute dimension: 2 (Task Completion Verification) only
- Parse only tasks marked [X] from tasks.md
- For each completed task:
  - Verify file paths exist
  - Check implementation artifacts present (classes, methods, tests)
  - Validate patterns from referenced instruction files
  - Run MCP tools based on task type (e.g., validate_dao_patterns for DAO tasks)
- Generate focused report on completion accuracy
- Auto-generate remediation tasks for false positives (tasks checked but not implemented)
- Suitable for: Catching premature check-offs, pre-merge verification of claimed completions

## Operating Principles

### Context Efficiency
- **Progressive disclosure**: Load only artifacts needed for active validation mode
- **Parallel MCP execution**: Run independent MCP tools concurrently when possible
- **Cached results**: Avoid re-analyzing unchanged files
- **Focused reporting**: In task/phase modes, report only relevant findings

### Validation Guidelines
- **Read-only by default**: Only writes are to validation report and tasks.md (remediation tasks)
- **No false positives**: Verify findings before reporting (e.g., check file exists before claiming "missing")
- **Actionable findings**: Every issue must have clear fix description and task
- **Measurable results**: Use percentages, counts, pass/fail - avoid vague assessments
- **Constitution authority**: Constitution violations are always CRITICAL severity

### Remediation Task Quality
- **Specific and actionable**: Task description must be implementable without additional research
- **Traceable**: Link back to validation issue that triggered task creation
- **Instruction-guided**: Always reference relevant instruction file
- **Testable acceptance**: Include clear verification criteria
- **Non-duplicate**: Check if similar remediation task already exists before adding

## Error Handling

### Missing Prerequisites
- **No spec.md**: ERROR "Run /speckit.specify first to create specification"
- **No tasks.md**: ERROR "Run /speckit.tasks first to generate task breakdown"
- **No code files**: WARNING "Implementation not started - validation will show 100% gaps"

### MCP Tool Failures
- **Tool unavailable**: WARNING "Skipping [tool name] - tool not available"
- **Tool error**: WARNING "Tool [name] failed: [error] - continuing with other validations"
- **Never halt**: If one MCP tool fails, continue with remaining validations

### Ambiguous Flags
- **Contradictory flags**: ERROR "Cannot combine --requirements-only with --quality-only"
- **Invalid task number**: ERROR "Task T### not found in tasks.md"
- **Invalid phase**: ERROR "Phase N does not exist in tasks.md"

## Examples

### Example 1: Full Validation Post-Implementation

```bash
/speckit.validate --full
```

**Behavior**:
- Runs all 8 dimensions
- Executes all MCP tools
- Generates comprehensive report
- Auto-generates all remediation tasks (CRITICAL and HIGH)
- Outputs full summary with next steps

---

### Example 2: Quick Mid-Development Check

```bash
/speckit.validate --quick
```

**Behavior**:
- Runs requirements, tasks, build, DAO patterns, security checks only
- Skips test coverage, documentation, edge cases
- Faster execution (< 2 minutes)
- Generates abbreviated report
- Auto-generates CRITICAL remediation tasks only

---

### Example 3: Single Task Validation After Fix

```bash
/speckit.validate --T042
```

**Behavior**:
- Parses task T042 from tasks.md
- Validates only that task's implementation
- Runs MCP tools relevant to task type (e.g., validate_dao_patterns if DAO task)
- Generates focused report for T042
- Updates T042 remediation status if previously flagged

---

### Example 4: Phase Gate Validation

```bash
/speckit.validate --phase 2
```

**Behavior**:
- Validates all tasks in Phase 2
- Checks requirements covered by Phase 2 stories
- Runs comprehensive MCP tools on Phase 2 files
- Generates phase-focused report
- Auto-generates phase-specific remediation tasks

---

### Example 5: Coverage Analysis Without Quality Gates

```bash
/speckit.validate --requirements-only
```

**Behavior**:
- Analyzes spec-to-code traceability
- Checks task completion
- Identifies gaps in requirement coverage
- Skips all MCP quality tools (fast)
- Generates coverage report with gap-filling tasks

---

### Example 6: Pre-Commit Quality Check

```bash
/speckit.validate --quality-only
```

**Behavior**:
- Runs all MCP tools (DAO, error handling, XML docs, security, performance, stored procedures)
- Skips requirement tracing (assumes coverage validated earlier)
- Generates quality metrics report
- Auto-generates code quality remediation tasks
- Perfect for: Code review preparation, technical debt assessment

---

### Example 7: Verify Completed Tasks Accuracy

```bash
/speckit.validate --completed-only
```

**Behavior**:
- Parses only tasks marked [X] as complete
- Verifies each completed task has actual implementation:
  - Files referenced in task exist
  - Classes/methods/tests are present (not stub-only)
  - Patterns from instruction files are followed
- Runs task-type-specific MCP tools (e.g., validate_dao_patterns for DAO tasks)
- Generates focused report on completion accuracy
- Auto-generates remediation for false positives (premature check-offs)
- Perfect for: Pre-merge verification, checking off accuracy, catching incomplete "complete" tasks

## Context

$ARGUMENTS
