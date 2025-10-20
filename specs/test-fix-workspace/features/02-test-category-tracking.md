# Feature 2: Test Category Tracking System

**Created**: 2025-10-19  
**Purpose**: Create focused tracking files for each test failure category with root cause analysis, fix strategies, and progress checkboxes

---

## Feature Overview

Build individual category tracking files for the 4 groups of failing tests. Each file provides focused information about that category's test failures, root cause analysis, fix strategy, relevant tools, and checkbox-based progress tracking. This replaces monolithic checklists with modular, single-purpose files that can be updated independently.

---

## Current Situation

**Test Status**: 113/136 passing (83%), 23 failed, 0 skipped  
**Goal**: Achieve 136/136 passing (100%)

**Test Failure Breakdown**:
- Category 1: Quick Button Failures - 10 tests (HIGH PRIORITY)
- Category 2: System DAO Failures - 6 tests (MEDIUM PRIORITY)
- Category 3: Helper & Validation Tests - 5 tests (LOW PRIORITY)
- Category 4: Phase 1 New Failures - 2 tests (INVESTIGATION NEEDED)

**Problem**: Root cause analysis, fix strategies, and test lists are scattered across session logs and investigation notes. Developers waste time re-reading findings or searching for relevant commands.

---

## User Needs

### Primary Users

**Developers fixing specific test categories**: Need all relevant information for ONE category in a single file under 300 lines. Want to see test names, understand root cause, follow fix strategy, check progress, and access relevant tools without switching files.

**QA Engineers validating fixes**: Need to understand what changed, why the test was failing, and what the expected behavior is. Want to verify fixes don't mask problems or create dependencies.

**Tech Leads prioritizing work**: Need to quickly assess which categories are blocking vs low priority, understand effort required, and see progress at category level.

---

## What Users Need to Accomplish

### For Developers Fixing Tests

1. **Understand category context in 2 minutes**: Read root cause, see affected tests, understand fix approach
2. **Access relevant tools immediately**: See which MCP tools apply to this category, get exact commands
3. **Track granular progress**: Check off individual tests as fixed, see completion percentage
4. **Find code locations fast**: Get exact file paths for DAOs, stored procedures, test files
5. **Copy commands directly**: Get PowerShell test commands with correct filters ready to run

### For QA Engineers

1. **Verify fix approach**: Understand if fix addresses root cause or just symptoms
2. **Check test isolation**: Confirm fixes don't create dependencies between tests
3. **Review test data strategy**: See how test data is set up and cleaned up
4. **Validate expected behavior**: Understand what passing test should demonstrate

### For Tech Leads

1. **Assess effort quickly**: Understand if category is 1-hour or 1-day effort
2. **Identify blockers**: See if category has dependencies on other work
3. **Track velocity**: Monitor progress within category over time
4. **Prioritize resources**: Know which categories need immediate attention

---

## Success Outcomes

### For Test Fixing Efficiency

- Developers find all category info in single file (no cross-referencing)
- Time to understand test failure: < 2 minutes (baseline: 5-10 minutes)
- Progress updates take < 30 seconds (baseline: 2+ minutes)
- Zero need to re-read investigation findings

### For Fix Quality

- 100% of fixes address root cause (not symptoms)
- Zero new test dependencies created
- All test data properly isolated
- Fixes follow documented patterns from integration-testing.instructions.md

### For Progress Visibility

- Category completion percentage auto-calculated
- Individual test status clear (pending/in-progress/complete)
- Blockers identified explicitly
- Next test to work on obvious

---

## Category File Structure

Each category file must include these sections:

### 1. Category Header
- Category name and priority level
- Test count and completion status: X/Y passing (Z%)
- Root cause summary (1-2 sentences)
- Estimated effort (hours)

### 2. Root Cause Analysis
- What's failing and why (specific error messages)
- Investigation findings from MCP tools
- References to stored procedures or code causing issue
- Why tests are failing (missing data, wrong parameters, timing issues)

### 3. Fix Strategy
- High-level approach (numbered steps)
- Test data setup requirements
- Code changes needed (DAO vs stored procedure vs test)
- Verification steps after fix

### 4. Test List with Checkboxes
- [ ] Test name | File location | Specific failure reason
- One checkbox per test
- Include test method name and class
- Note any unique considerations per test

### 5. Relevant MCP Tools
- Tools to use for this category (with one-line descriptions)
- Example commands with actual parameters
- When to use each tool in fix workflow

### 6. Commands
- PowerShell commands to run category tests
- SQL commands for manual testing (if applicable)
- Commands for test data setup
- Commands for verification

### 7. Code Locations
- DAO files involved: `Data/Dao_*.cs`
- Test files: `Tests/Integration/Dao_*_Tests.cs`
- Stored procedures: `Database/UpdatedStoredProcedures/ReadyForVerification/*.sql`
- Helper classes if relevant

### 8. Notes & Gotchas
- Common mistakes to avoid
- Dependencies on other categories
- Test isolation considerations
- Performance considerations

---

## The 4 Categories

### Category 1: Quick Button Failures (10 tests)

**Priority**: HIGH  
**Root Cause**: Tests attempt UPDATE/MOVE/DELETE operations on quick buttons that don't exist in test database  
**Fix Strategy**: Create test data setup helper or use stored procedures to seed quick button records before tests  
**Estimated Effort**: 3-4 hours

**Tests**:
1. Dao_QuickButtons_Tests.AddQuickButton_ValidData_InsertsButton
2. Dao_QuickButtons_Tests.GetQuickButton_ExistingButton_ReturnsButton
3. Dao_QuickButtons_Tests.GetQuickButtonsByUser_ExistingUser_ReturnsButtons
4. Dao_QuickButtons_Tests.UpdateQuickButton_ExistingButton_UpdatesButton
5. Dao_QuickButtons_Tests.UpdateQuickButtonPositions_ValidPositions_UpdatesPositions
6. Dao_QuickButtons_Tests.DeleteQuickButton_ExistingButton_DeletesButton
7. Dao_QuickButtons_Tests.GetNextPosition_ExistingButtons_ReturnsNextPosition
8. Dao_QuickButtons_Tests.MoveQuickButton_ValidMove_UpdatesPosition
9. Dao_QuickButtons_Tests.GetQuickButtonCount_ExistingUser_ReturnsCount
10. Dao_QuickButtons_Tests.QuickButtonExists_ExistingButton_ReturnsTrue

**MCP Tools**: generate_test_seed_sql, verify_test_seed, validate_dao_patterns

---

### Category 2: System DAO Failures (6 tests)

**Priority**: MEDIUM  
**Root Cause**: Tests reference non-existent users in usr_users table (TEST-USER, TESTUSER2, etc.)  
**Fix Strategy**: Create test user records in usr_users table, update tests to use BaseIntegrationTest helpers  
**Estimated Effort**: 2-3 hours

**Tests**:
1. Dao_System_Tests.GetUserByUsername_ExistingUser_ReturnsUser
2. Dao_System_Tests.GetUserById_ExistingUser_ReturnsUser
3. Dao_System_Tests.UserExists_ExistingUser_ReturnsTrue
4. Dao_System_Tests.GetAllUsers_Execution_ReturnsUsers
5. Dao_System_Tests.GetActiveUsers_Execution_ReturnsActiveUsers
6. Dao_System_Tests.ValidateUserCredentials_ValidCredentials_ReturnsTrue

**MCP Tools**: generate_test_seed_sql, verify_test_seed, audit_database_cleanup

---

### Category 3: Helper & Validation Tests (5 tests)

**Priority**: LOW  
**Root Cause**: Timing issues, validation edge cases, helper method assumptions  
**Fix Strategy**: Review each test individually, may need test refactoring or helper adjustments  
**Estimated Effort**: 2-3 hours

**Tests**:
1. Helper_Tests.DateTimeHelper_ParseDate_ValidFormats_ReturnsDateTime
2. Helper_Tests.ValidationHelper_ValidatePartNumber_ValidFormats_ReturnsTrue
3. Helper_Tests.StringHelper_TruncateWithEllipsis_LongString_TruncatesCorrectly
4. Validation_Tests.PartNumberValidation_InvalidFormats_ReturnsFalse
5. Validation_Tests.LocationCodeValidation_InvalidCodes_ReturnsFalse

**MCP Tools**: validate_dao_patterns, check_security, validate_error_handling

---

### Category 4: Phase 1 New Failures (2 tests)

**Priority**: INVESTIGATION NEEDED  
**Root Cause**: Uncovered by Phase 1 table name fix, need investigation  
**Fix Strategy**: TBD - investigate why these started failing after sys_ui_tables_name fix  
**Estimated Effort**: 1-2 hours investigation + fix time

**Tests**:
1. Dao_Inventory_Tests.GetInventoryByLocation_ValidLocation_ReturnsInventory
2. Dao_Transactions_Tests.GetTransactionHistory_DateRange_ReturnsTransactions

**MCP Tools**: analyze_stored_procedures, validate_schema, validate_dao_patterns

---

## File Naming Convention

```
categories/
├── 01-quick-buttons.md           # Category 1
├── 02-system-dao.md              # Category 2
├── 03-helper-validation.md       # Category 3
└── 04-phase1-failures.md         # Category 4
```

Number prefix ensures consistent ordering, name describes category clearly.

---

## Progress Tracking Format

Each test should follow this format:

```markdown
- [ ] **Test Name** | `File/Path/TestClass.cs` | Specific failure reason
  - **Error**: Exact error message or symptom
  - **Fix approach**: What needs to change
  - **Verification**: How to confirm fix worked
```

Example:
```markdown
- [ ] **AddQuickButton_ValidData_InsertsButton** | `Tests/Integration/Dao_QuickButtons_Tests.cs` | Attempts insert without test data
  - **Error**: "Cannot add quick button - user ID 'TEST-USER' does not exist"
  - **Fix approach**: Create TEST-USER in usr_users table before test runs
  - **Verification**: Test passes, button appears in sys_quick_buttons table
```

---

## Update Workflow

### Manual Update (Initial Implementation)

1. Developer fixes test
2. Runs test to confirm passing
3. Opens category file
4. Checks box for fixed test: `- [ ]` becomes `- [x]`
5. Updates category header completion count
6. Runs `.\tools\update-progress.ps1` to update dashboard

### Automated Update (Future Enhancement)

- Test runner hooks update checklist automatically
- Dashboard auto-regenerates from category files
- Completion percentage calculated on file read

---

## Special Requirements

### Single Source of Truth

Each test appears in exactly ONE category file. No duplication across files.

### Root Cause Validation

Before marking "Root Cause Analysis" complete, must:
- Use MCP tools to validate findings
- Reference specific error messages or logs
- Identify exact code or data causing failure
- Distinguish symptoms from actual cause

### Fix Strategy Actionability

Fix strategy must include:
- Specific steps (not vague suggestions like "fix the test")
- Code file locations with line numbers if known
- Test data requirements with examples
- Verification commands to confirm fix

### Code Location Precision

All code references must be:
- Absolute paths from repo root
- Current as of branch 002-003-database-layer-complete
- Verified to exist (no broken links)
- Include line numbers for specific methods

---

## Dependencies

**Depends on**: Feature 1 (Workspace Foundation) - needs categories/ folder structure

**Depended on by**: 
- Feature 5 (Progress Dashboard) - reads category files for metrics
- Feature 6 (Automation Tools) - updates category file checkboxes

---

## Out of Scope

This feature does NOT include:
- Actually fixing the tests (that's implementation work)
- Creating automation scripts (separate feature)
- Building dashboard (separate feature)
- Writing reference documentation (separate feature)
- Migrating tasks from other specs (separate consideration)

Only creates the **4 category tracking files** with analysis and checklists.

---

## Assumptions

- Test failure analysis from 2025-10-19 sessions is accurate
- Root causes identified via MCP tools are correct
- Test database schema is stable
- No new test failures introduced during fix work
- Category groupings make logical sense (can adjust if needed)

---

## Acceptance Criteria

### Category Files Created
- [ ] All 4 category files exist in categories/ folder
- [ ] Files named with number prefix and descriptive name
- [ ] Each file under 300 lines

### Content Completeness
- [ ] Each file has all 8 required sections
- [ ] Root cause analysis references MCP tool findings
- [ ] Fix strategy includes specific, actionable steps
- [ ] All tests listed with checkboxes
- [ ] Test counts match actual failing tests

### Code References
- [ ] All DAO paths are absolute from repo root
- [ ] All test file paths verified to exist
- [ ] Stored procedure paths correct
- [ ] No broken links

### MCP Tool Integration
- [ ] Relevant tools identified per category
- [ ] Example commands include actual parameters
- [ ] Tool usage tied to fix workflow steps

### Commands Ready
- [ ] PowerShell test commands copy-pasteable
- [ ] Commands use correct test name filters
- [ ] SQL commands (if any) include parameter examples
- [ ] Verification commands provided

### Progress Tracking
- [ ] Checkbox format consistent across all files
- [ ] Each test has error message and fix approach
- [ ] Completion percentage calculation method documented
- [ ] Update workflow explained in each file

### Usability
- [ ] Developers can understand category in under 2 minutes
- [ ] All info for category in single file (no cross-referencing)
- [ ] Commands ready to run without modification
- [ ] Fix strategy is actionable (not vague)

---

## Success Metrics

**Information Access**:
- Time to find test details: < 30 seconds (baseline: 2+ minutes)
- Time to understand root cause: < 2 minutes (baseline: 5+ minutes)
- Commands ready to run: 100% (baseline: need manual editing)

**File Size**:
- Lines per category file: < 300 (target: 200-250)
- Cross-references needed: 0 (all info in category file)

**Progress Tracking**:
- Time to update status: < 30 seconds (baseline: 2+ minutes finding section)
- Completion percentage accuracy: 100% (auto-calculated correctly)

---

## Notes for /speckit.specify

This feature focuses on creating the 4 category tracking files with analysis and checklists. It uses findings from existing investigation work (2025-10-19 sessions) to populate root cause analysis and fix strategies.

**No clarifications needed** - test failures, root causes, and category groupings are already known from investigation work.

The feature can be implemented immediately after Feature 1 (Workspace Foundation) establishes the folder structure.
