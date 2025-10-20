# Test Fix Workspace - Feature Description for /speckit.specify

**Created**: 2025-10-19  
**Purpose**: Organize and track the fixing of 23 failing integration tests across multiple test categories

---

## Feature Overview

Create a comprehensive Test Fix Workspace that organizes test failure information, investigation findings, fix strategies, and progress tracking into a maintainable, modular structure. This workspace will serve as the central hub for managing the resolution of 23 failing integration tests (out of 136 total tests).

The workspace should replace the current monolithic checklist approach with a folder-based organization that prevents repeated editing of the same files and provides clear separation between active work, reference materials, and historical records.

---

## Current Situation

**Test Status**: 113/136 passing (83%), 23 failed, 0 skipped  
**Goal**: Achieve 136/136 passing (100%)

**Test Failure Breakdown**:
- Category 1: Skipped Tests - 9 tests (NOW: 7 passing, 2 failing after Phase 1 fix)
- Category 2: Quick Button Failures - 10 tests (HIGH PRIORITY - root cause identified: missing test data)
- Category 3: System DAO Failures - 6 tests (MEDIUM PRIORITY - root cause identified: missing test users)
- Category 4: Helper & Validation Tests - 5 tests (LOW PRIORITY - timing and validation tests)

**Investigation Complete**: Using MCP tools (analyze_stored_procedures, validate_dao_patterns, validate_schema, check_security, validate_error_handling, check_xml_docs, suggest_refactoring), we've identified root causes for most failures:
- Quick Button tests: Tests attempt UPDATE/MOVE/DELETE operations on non-existent records
- System DAO tests: Tests reference non-existent test users in usr_users table
- Both categories need test data setup helpers

**Context**: This is part of the larger "002-003-database-layer-complete" feature branch that standardized 95 stored procedures, created parameter prefix override system, and established comprehensive integration test suite.

---

## User Needs

### Primary Users

**Developers fixing tests**: Need clear, focused information about each failing test category without wading through investigation history or redundant documentation. Want to quickly understand root cause, see fix strategy, access relevant commands, and mark progress.

**QA/Test Engineers**: Need to verify test fixes don't introduce regressions, understand test isolation strategies, and validate that fixes address actual problems rather than masking them.

**Project Managers/Tech Leads**: Need high-level progress tracking, understand which categories are blocking vs low priority, and see completion metrics without diving into technical details.

---

## What Users Need to Accomplish

### For Developers Fixing Tests

1. **Understand failure context quickly**: See test name, root cause analysis, and fix strategy in under 2 minutes
2. **Access relevant tools immediately**: Get MCP tool references, PowerShell commands, SQL test scripts without searching
3. **Track progress granularly**: Mark individual tests complete, see category completion percentages
4. **Avoid repeating investigation**: Reference historical findings without re-reading session logs
5. **Find templates fast**: Access SQL test templates, DAO test patterns, seed data examples

### For QA/Test Engineers

1. **Verify fix correctness**: Understand expected behavior vs actual behavior for each test
2. **Validate test isolation**: Confirm fixes don't create dependencies between tests
3. **Check for regressions**: See which tests were passing before and ensure they still pass
4. **Review test data strategies**: Understand seed data approach, cleanup strategies

### For Project Managers

1. **Get status at a glance**: See dashboard with completion percentages by category
2. **Understand priority order**: Know which categories are blocking deployment
3. **Track velocity**: See progress over time (tests fixed per session)
4. **Identify blockers**: Quickly spot categories with stalled progress

---

## Success Outcomes

### For Test Suite Health

- All 136 integration tests passing consistently
- No skipped tests remaining
- Test execution completes in under 10 minutes
- Zero test interdependencies (full isolation)
- Deterministic test results (no flaky tests)

### For Workspace Usability

- Developers can find relevant test information in under 1 minute
- Zero need to re-read investigation findings multiple times
- Progress tracking updated in under 30 seconds per test fix
- MCP tool usage examples accessible without searching documentation
- Commands copy-pasteable directly from workspace files

### For Maintainability

- Single source of truth for each test category
- No duplicate information across files
- Changes to one category don't require updates to other files
- Historical session logs preserved but out of primary workflow
- Clear separation between "what to do" and "how we got here"

---

## Key Information to Organize

### Test Failure Categories (Active Work)

Each category should be a separate file with:
- Test IDs and names
- Root cause analysis
- Fix strategy
- Checkbox progress tracking
- Relevant MCP tools to use
- Code file locations
- Test-specific commands

**Categories**:
1. Quick Button Failures (10 tests) - Missing test data setup
2. System DAO Failures (6 tests) - Missing test users
3. Helper & Validation Tests (5 tests) - Timing and validation issues
4. New Failures from Phase 1 (2 tests) - Uncovered by table name fix

### MCP Tools Reference

Split into two levels:
1. **Quick Reference** (in main TOC): Tool names + one-line descriptions
2. **Full Tool Guide** (in reference folder): Complete descriptions, parameters, examples, when to use each tool

**Tools to document**:
- analyze_stored_procedures, validate_dao_patterns, validate_schema
- check_security, validate_error_handling, check_xml_docs
- suggest_refactoring, generate_test_seed_sql, verify_test_seed
- install_stored_procedures, run_integration_harness, audit_database_cleanup
- And 16 more tools from mtm-workflow server

### Testing Templates & Commands

Separate files for:
- **SQL Test Templates**: How to manually test stored procedures with exact parameter examples
- **PowerShell Commands**: Build, test, database commands organized by purpose
- **Test Data Setup Patterns**: Examples of creating test users, quick buttons, seed data
- **Integration Test Patterns**: How to write tests that follow BaseIntegrationTest standards

### Progress Tracking

Central dashboard showing:
- Overall status: X/136 passing
- By category: completion percentages
- Phase completion: Phase 1 ✅, Phase 2 🔄, Phase 3 ⏳
- Velocity: tests fixed per session
- Next priority: which category to work on

### Historical Context (Archived)

Session logs moved to history folder:
- Session 2025-10-19 Part 1: _1 Cleanup & Phase 1 (7 tests fixed)
- Session 2025-10-19 Part 2: MCP Tools Investigation (root causes identified)
- Future sessions...

**Important**: History is reference-only, not part of daily workflow

---

## Task Migration from Other Specs

### Incomplete Tasks to Review

**From 002-003-database-layer-complete/tasks.md** (19 incomplete):
- Roslyn analyzer development (T124a, T124, T501)
- Manual testing workflows (T126)
- Performance validation (T128)
- Transaction rollback testing (T127)
- Error logging recursive prevention (T125)

**From 002-003-001-developer-tools-suite/tasks.md** (41 incomplete):
- Parameter Prefix Maintenance UI (T030-T035)
- Schema Inspector tool (T036-T039)
- Developer role implementation (various)

**From 002-comprehensive-database-layer/tasks.md** (109 incomplete):
- Many likely superseded by newer specs
- Need manual review to identify still-relevant tasks

**Migration Strategy**:
- Only migrate tasks that directly support fixing the 23 tests
- Check if tasks are already complete (may be marked incomplete but work is done)
- For each task, verify it won't break current branch
- Skip tasks marked DEFERRED
- Skip UI feature tasks (out of scope for test fixing)

---

## Workspace Structure Requirements

### Folder Organization

```
test-fix-workspace/
├── TOC.md                          # Main entry point, navigation hub
├── DASHBOARD.md                    # Current status, progress metrics
├── categories/                     # One file per test category
│   ├── 01-quick-buttons.md        # 10 tests, root cause, fix strategy
│   ├── 02-system-dao.md           # 6 tests, test user setup needed
│   ├── 03-helper-validation.md    # 5 tests, timing issues
│   └── 04-phase1-failures.md      # 2 new failures to investigate
├── reference/                      # Read-only reference materials
│   ├── mcp-tools-quick.md         # One-line tool descriptions
│   ├── mcp-tools-full.md          # Complete tool documentation
│   ├── sql-templates.md           # SQL test script examples
│   ├── powershell-commands.md     # All commands organized by purpose
│   └── test-patterns.md           # Testing best practices
├── tools/                          # Helper scripts (safe automation)
│   ├── update-progress.ps1        # Mark tests complete, update metrics
│   ├── run-category-tests.ps1     # Run specific test category
│   └── generate-dashboard.ps1     # Rebuild dashboard from category files
└── history/                        # Historical session logs
    ├── CHANGELOG.md               # All session summaries
    └── sessions/                  # Detailed session logs
        ├── 2025-10-19-part1.md   # Phase 1 completion
        └── 2025-10-19-part2.md   # MCP investigation
```

### TOC.md Requirements

The Table of Contents file must:

1. **Provide clear navigation**: Links to all workspace files organized by purpose
2. **Show current status at top**: Quick metrics (113/136 passing, next priority)
3. **Include clarification protocol**: Easy-to-read format for when AI needs guidance
4. **Explain workspace purpose**: Why this structure exists, how to use it
5. **Link to quick start**: Essential commands to run tests immediately

**Clarification Protocol in TOC**:
```
## When I Need Clarification

If I encounter ambiguity while working on fixes, I will STOP and ask you a question in this format:

**Question**: [Clear, non-jargon question]

**Why I'm asking**: [Explanation of why this matters]

**Your options**:
- **A**: [Option 1 with plain explanation]
- **B**: [Option 2 with plain explanation]  
- **C**: [Option 3 with plain explanation]
- **Custom**: [How to provide your own answer]

**Suggested answer**: A [Why this is recommended]

Please respond with: "A" or "B" or "C" or "Custom: [your answer]"
```

### File Content Guidelines

Each file should:
- Be under 300 lines (preferably under 200)
- Have single, clear purpose
- Include only relevant information for that purpose
- Use checkboxes for actionable items
- Link to related files rather than duplicating content
- Update independently without affecting other files

### Dashboard Requirements

Dashboard must show:
- Overall test count and percentage
- By-category breakdown with progress bars (visual)
- Current phase and next phase
- Last updated timestamp
- Quick links to active categories
- Velocity metric (tests fixed per session)

---

## Special Requirements

### Tool Safety

Any PowerShell scripts in tools/ folder must:
- Include dry-run mode (test without making changes)
- Create backups before modifying files
- Validate inputs before execution
- Return clear success/failure status
- Include comprehensive help comments
- Use -WhatIf parameter where applicable

### MCP Tool Integration

Document how to use MCP tools effectively:
- When to use each tool (triggers)
- Required parameters with examples
- Expected output format
- How to interpret results
- Common troubleshooting

### Test Data Management

Provide clear guidance on:
- Creating test users (SQL scripts + stored procedure approach)
- Setting up quick button test data
- Test data cleanup strategies
- Avoiding test interdependencies
- Using BaseIntegrationTest helpers

### Progress Tracking

Support two update modes:
1. **Manual**: Developer checks box in category file, runs update-progress.ps1
2. **Automated** (future): Tests update status automatically on pass

---

## Out of Scope

This workspace does NOT handle:
- Creating new tests (only fixing existing failures)
- Refactoring test infrastructure (BaseIntegrationTest changes)
- Stored procedure modifications (procedures are working correctly)
- DAO refactoring (DAOs are compliant per MCP validation)
- UI feature development (Parameter Prefix Maintenance, Schema Inspector)
- Roslyn analyzer development
- Performance optimization beyond test execution time

---

## Assumptions

- Workspace is specific to current "002-003-database-layer-complete" branch
- After merge to main, this becomes reference for future test fixing
- MCP tools from mtm-workflow server remain available and stable
- Test database (mtm_wip_application_winforms_test) structure is stable
- Developers have basic familiarity with MSTest and C# integration testing
- PowerShell 7+ available for running workspace automation scripts

---

## Dependencies

- Existing test files in Tests/Integration/ directory
- MCP mtm-workflow server tools
- MySQL test database with proper schema
- .NET 8 SDK for running tests
- Current task files from three spec folders (for migration analysis)
- Integration test patterns from BaseIntegrationTest.cs

---

## Questions to Resolve (If Needed)

### Priority 1 - Task Migration Scope
**Question**: Should we migrate tasks from 002-comprehensive-database-layer (109 incomplete) or assume they're superseded?
**Option A**: Skip entirely, only migrate from 002-003 folders
**Option B**: Review each task with context and filter by keywords (test, integration, DAO)
**Suggested**: Option A (safer, less risk of outdated tasks)

### Priority 2 - Tool Scripts Scope
**Question**: Should workspace tools include scripts to automatically fix test issues or only progress tracking?
**Option A**: Progress tracking only (safer)
**Option B**: Include safe automation like test data setup scripts
**Suggested**: Option A initially, Option B in future iteration

### Priority 3 - Dashboard Update Frequency
**Question**: How should dashboard metrics be updated?
**Option A**: Manual update via PowerShell script after each test fix
**Option B**: Automatically scan category files and regenerate
**Suggested**: Option B (reduces manual steps, stays accurate)

---

## Acceptance Criteria

### Workspace Structure
- [ ] All folders created with clear purpose
- [ ] TOC.md provides complete navigation
- [ ] Each category file is under 300 lines
- [ ] No duplicate content across files
- [ ] Clarification protocol documented in TOC

### Test Categories
- [ ] 4 category files created (Quick Buttons, System DAO, Helper/Validation, Phase 1 Failures)
- [ ] Each category includes: root cause, fix strategy, checkboxes, relevant tools
- [ ] Test IDs and names match actual test file names
- [ ] Fix strategies are actionable (specific steps, not vague suggestions)

### Reference Materials
- [ ] MCP tools split into quick reference and full guide
- [ ] SQL test templates with copy-paste examples
- [ ] PowerShell commands organized by purpose (build, test, database)
- [ ] Test patterns document common scenarios

### Progress Tracking
- [ ] Dashboard shows current status visually
- [ ] Update script works correctly (marks tests complete, recalculates percentages)
- [ ] Velocity tracking implemented
- [ ] Next priority clearly indicated

### Tool Safety
- [ ] All scripts include dry-run mode
- [ ] Scripts validate inputs before execution
- [ ] Clear help documentation in script headers
- [ ] No risk of data corruption or unintended changes

### Migration Complete
- [ ] Relevant tasks identified from other specs
- [ ] Tasks reviewed for completion status
- [ ] Test-related tasks added to appropriate categories
- [ ] Non-test tasks documented in reference (if relevant) or excluded

### Usability
- [ ] Developer can find test info in under 1 minute
- [ ] Commands are copy-paste ready
- [ ] Progress updates take under 30 seconds
- [ ] Historical context accessible but not in primary workflow

---

## Success Metrics

**Efficiency Gains**:
- Time to find test information: < 1 minute (vs 5+ minutes with monolithic file)
- Time to update progress: < 30 seconds (vs 2+ minutes finding right section)
- Duplicate information instances: 0 (vs 3-4 in current structure)

**Workspace Quality**:
- Files edited per test fix: 1-2 max (vs 1 large file for everything)
- Lines read per task: < 200 (vs 1000+ scanning monolithic file)
- Navigation depth: 2 clicks max to any information

**Test Fixing Velocity**:
- Baseline: 7 tests fixed in Phase 1 (table name fix)
- Goal: 10+ tests fixed per session with better organization
- Stretch: Complete all 23 tests in 3 focused sessions

---

## Notes for /speckit.specify

This feature description is comprehensive and ready for spec generation. Key aspects:

1. **Clear user needs**: Three distinct user types with specific goals
2. **Measurable outcomes**: Specific metrics for success (time, counts, percentages)
3. **Well-defined scope**: What's included and what's explicitly excluded
4. **Concrete structure**: Exact folder layout and file purposes defined
5. **Safety requirements**: Tool scripts must have safeguards
6. **Migration strategy**: Clear approach to handling tasks from other specs

**Suggested clarifications** (if /speckit.specify needs them):
1. Confirm task migration approach (skip 002-comprehensive or review?)
2. Confirm tool script scope (tracking only or include automation?)
3. Confirm dashboard update mechanism (manual trigger or auto-scan?)

All other details have reasonable defaults and don't need clarification.