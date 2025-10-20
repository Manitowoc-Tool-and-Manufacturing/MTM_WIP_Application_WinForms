# Feature 3: MCP Tools Reference Hub

**Created**: 2025-10-19  
**Purpose**: Provide comprehensive documentation for all MCP tools from mtm-workflow server, split into quick reference and detailed guides

---

## Feature Overview

Create a two-tier MCP tools documentation system: a quick reference with one-line descriptions for fast lookup, and a comprehensive guide with full details, parameters, examples, and usage patterns. This enables developers to quickly find the right tool and understand how to use it effectively during test fixing.

---

## Current Situation

**Available Tools**: 26+ MCP tools from mtm-workflow server for database analysis, validation, testing, and automation

**Problem**: Tool capabilities are scattered across:
- MCP server source code documentation
- Instruction files (.github/instructions/*.instructions.md)
- Session logs showing tool usage
- No single reference showing all tools and when to use them

**Impact**: Developers don't know which tools exist, when to use them, or how to call them correctly. Results in:
- Manual work that could be automated
- Missed validation opportunities
- Incorrect tool usage (wrong parameters)
- Time wasted searching for examples

---

## User Needs

### Primary Users

**Developers fixing tests**: Need to quickly find which tool can help with current problem (e.g., "how do I validate stored procedure compliance?"), then see exact command with parameters.

**QA Engineers validating fixes**: Need to understand what validation tools exist and which ones should be run before/after test fixes to ensure quality.

**AI Agents (GitHub Copilot)**: Need comprehensive tool reference to choose correct tool for task and construct proper parameters without trial-and-error.

---

## What Users Need to Accomplish

### For Quick Lookup (Quick Reference)

1. **Find tool by name**: Alphabetical list with one-line descriptions
2. **Find tool by purpose**: Grouped by category (Database, DAO, Testing, UI, etc.)
3. **See tool at a glance**: Understand what it does in under 10 seconds
4. **Jump to details**: Link to full documentation for selected tool

### For Detailed Usage (Full Guide)

1. **Understand tool purpose**: When to use it and what problems it solves
2. **See parameters**: Required vs optional, types, descriptions
3. **Copy examples**: Real commands with actual parameters from this project
4. **Learn patterns**: Common workflows that combine multiple tools
5. **Avoid pitfalls**: Known issues, limitations, troubleshooting tips

---

## Success Outcomes

### For Tool Discovery

- Developers find relevant tool in under 30 seconds
- Zero cases of "didn't know that tool existed"
- Tool selection correct 95%+ of time (right tool for the job)

### For Tool Usage

- Commands run successfully on first try (no parameter errors)
- Examples are copy-pasteable with minimal editing
- Troubleshooting tips prevent common mistakes
- Workflow patterns show tool combinations

### For Documentation Quality

- Quick reference under 150 lines (scannable in 2 minutes)
- Full guide under 500 lines per major section
- Zero broken links or outdated examples
- Every tool has at least one real example

---

## MCP Tools to Document

### Database Analysis (8 tools)

1. **analyze_stored_procedures** - Scan SQL files for compliance (p_Status, p_ErrorMsg, transaction handling)
2. **analyze_dependencies** - Map stored procedure call hierarchies and dependency graphs
3. **compare_databases** - Detect schema drift between Current and Updated directories
4. **validate_schema** - Compare live MySQL schema against database-schema-snapshot.json
5. **install_stored_procedures** - Apply stored procedure scripts and report drift vs live database
6. **generate_test_seed_sql** - Generate SQL seed scripts from JSON configuration
7. **verify_test_seed** - Validate seeded data against expected results
8. **audit_database_cleanup** - Inspect and clear residual TEST-* rows

### DAO & Code Analysis (6 tools)

9. **validate_dao_patterns** - Check DAO compliance with MTM patterns (regions, async, XML docs)
10. **validate_error_handling** - Check Service_ErrorHandler usage, find MessageBox.Show
11. **check_security** - Scan for SQL injection, hardcoded credentials, vulnerabilities
12. **analyze_performance** - Identify N+1 queries, blocking async, UI thread issues
13. **check_xml_docs** - Validate XML documentation coverage
14. **suggest_refactoring** - AI-powered refactoring suggestions

### UI Validation (3 tools)

15. **validate_ui_scaling** - Check WinForms DPI scaling compliance
16. **generate_ui_fix_plan** - Create JSON fix plan from UI validation results
17. **apply_ui_fixes** - Apply UI fixes with backups and corruption detection

### Testing Support (4 tools)

18. **generate_unit_tests** - Auto-generate test scaffolding for C# classes
19. **run_integration_harness** - Execute integration test harness with seeding/teardown
20. **check_checklists** - Analyze markdown checklists for completion status
21. **validate_build** - Run dotnet build and validate compilation

### Code Analysis (3 tools)

22. **generate_dao_wrapper** - Auto-generate C# DAO wrapper from stored procedure
23. **parse_tasks** - Extract structured task information from tasks.md
24. **mark_task_complete** - Mark tasks as complete in tasks.md
25. **load_instructions** - Load and analyze instruction file references

### Project Management (2 tools)

26. **verify_ignore_files** - Check .gitignore and ignore files for essential patterns
27. **analyze_spec_context** - Extract implementation context from spec directory

---

## Quick Reference Structure

File: `reference/mcp-tools-quick.md`

Format:
```markdown
# MCP Tools Quick Reference

**Purpose**: Fast lookup of tool names and capabilities  
**Last Updated**: 2025-10-19  
**Full Documentation**: See [mcp-tools-full.md](mcp-tools-full.md)

---

## By Category

### Database Analysis
- **analyze_stored_procedures** - Scan SQL files for compliance with p_Status/p_ErrorMsg outputs
- **analyze_dependencies** - Map stored procedure call hierarchies
- **compare_databases** - Detect schema drift between directories
- **validate_schema** - Compare live schema against snapshot
- **install_stored_procedures** - Apply SP scripts and report drift
- **generate_test_seed_sql** - Generate SQL seed scripts from JSON
- **verify_test_seed** - Validate seeded data
- **audit_database_cleanup** - Clear residual TEST-* rows

### DAO & Code Analysis
- **validate_dao_patterns** - Check DAO compliance (regions, async, XML)
- **validate_error_handling** - Check Service_ErrorHandler usage
- **check_security** - Scan for vulnerabilities
- **analyze_performance** - Identify performance bottlenecks
- **check_xml_docs** - Validate XML documentation
- **suggest_refactoring** - AI refactoring suggestions

### UI Validation
- **validate_ui_scaling** - Check WinForms DPI scaling
- **generate_ui_fix_plan** - Create JSON UI fix plan
- **apply_ui_fixes** - Apply fixes with backups

### Testing Support
- **generate_unit_tests** - Auto-generate test scaffolding
- **run_integration_harness** - Execute test harness
- **check_checklists** - Analyze checklist completion
- **validate_build** - Run build and validate

### Code Generation
- **generate_dao_wrapper** - Generate DAO from stored procedure
- **parse_tasks** - Extract task information
- **mark_task_complete** - Mark tasks complete
- **load_instructions** - Load instruction file references

### Project Management
- **verify_ignore_files** - Check .gitignore patterns
- **analyze_spec_context** - Extract spec implementation context

---

## Alphabetical Index

[Links to each tool in full guide with anchor tags]
```

**Requirements**:
- Under 150 lines
- One-line descriptions only (10 words max)
- Grouped by logical category
- Alphabetical index at bottom
- Links to full guide

---

## Full Guide Structure

File: `reference/mcp-tools-full.md`

Each tool gets a dedicated section:

```markdown
## [Tool Name]

**Category**: [Database/DAO/UI/Testing/etc.]  
**Purpose**: [2-3 sentence description]  
**When to use**: [Specific triggers/scenarios]

### Parameters

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| param_name | string | Yes | What this parameter does |
| optional_param | boolean | No | Optional behavior (default: false) |

### Output

[Description of what tool returns]
- Success format: [JSON/text/table description]
- Error format: [How errors are reported]

### Example Usage

**Scenario**: [Describe what you're trying to accomplish]

```bash
# Command with actual parameters from this project
tool_name(
  param1: "/absolute/path/to/actual/file",
  param2: true
)
```

**Expected output**:
```
[Sample output showing what success looks like]
```

### Common Workflows

**Workflow 1**: [Name of workflow]
1. Run tool_name with [parameters]
2. Review output for [specific information]
3. Use results to [next action]

### Troubleshooting

**Issue**: [Common problem]  
**Solution**: [How to fix it]

**Issue**: [Another problem]  
**Solution**: [How to fix it]

---
```

**Requirements**:
- Every tool documented completely
- Real examples from MTM project (not generic)
- At least one workflow showing tool in context
- Troubleshooting for common issues
- Cross-references to related tools

---

## Tool Usage Patterns

Document common multi-tool workflows:

### Pattern 1: Validating DAO Changes

```markdown
**Purpose**: Ensure DAO changes meet all MTM standards before committing

**Steps**:
1. **validate_dao_patterns** - Check region layout, async patterns, XML docs
   ```
   validate_dao_patterns(dao_dir: "Data/", recursive: true)
   ```

2. **validate_error_handling** - Ensure Service_ErrorHandler usage
   ```
   validate_error_handling(source_dir: "Data/", recursive: true)
   ```

3. **check_xml_docs** - Verify documentation coverage
   ```
   check_xml_docs(source_dir: "Data/", min_coverage: 80)
   ```

4. **analyze_performance** - Check for performance issues
   ```
   analyze_performance(source_dir: "Data/", focus: "database")
   ```

**Success criteria**: All tools report PASS with no critical issues
```

### Pattern 2: Preparing for Integration Test Run

```markdown
**Purpose**: Set up test environment and validate before running tests

**Steps**:
1. **validate_schema** - Confirm test database schema matches snapshot
2. **generate_test_seed_sql** - Create seed data for tests
3. **verify_test_seed** - Confirm seed data is correct
4. **run_integration_harness** - Execute full test suite with setup/teardown
5. **audit_database_cleanup** - Verify no residual test data

**Success criteria**: Clean test run with no environmental issues
```

### Pattern 3: Fixing Stored Procedure Issues

```markdown
**Purpose**: Update stored procedures and ensure compliance

**Steps**:
1. **analyze_stored_procedures** - Identify non-compliant procedures
2. **analyze_dependencies** - Understand call hierarchy before changes
3. **install_stored_procedures** - Apply updated procedures to test DB
4. **validate_schema** - Confirm schema still matches after SP updates
5. **generate_dao_wrapper** - Update DAO code if signatures changed

**Success criteria**: All procedures compliant, DAOs updated, tests passing
```

---

## Special Requirements

### Real Examples Only

- All examples use actual file paths from MTM project
- Parameter values reference real database names, file locations
- Output samples show actual tool responses (not made up)
- Commands are copy-pasteable with minimal editing

### Keep Current

- Tools marked as "mtm-workflow server" (not generic MCP)
- If tool parameters change, update examples
- Mark deprecated tools clearly
- Note version if tool behavior changes

### Cross-Referencing

- Link to instruction files that reference tools
- Link between quick reference and full guide
- Link related tools (e.g., generate_test_seed_sql ↔ verify_test_seed)
- Link to category files showing tool usage

### Plain Language

- Explain what tools do in business terms first
- Use technical details in parameter tables
- Avoid jargon in "When to use" sections
- Provide context for why tool exists

---

## Out of Scope

This feature does NOT include:
- Creating the MCP tools themselves (they exist)
- Modifying tool behavior or parameters
- Creating new tools
- Tool execution automation (just documentation)
- Integration with other workspace features (just reference)

Only creates **documentation** for existing tools.

---

## Dependencies

**Depends on**: Feature 1 (Workspace Foundation) - needs reference/ folder

**Depended on by**: Feature 2 (Test Category Tracking) - category files reference tools

**Related to**: 
- .github/instructions files (mention tools)
- MCP server implementation (defines tools)

---

## Assumptions

- MCP mtm-workflow server is stable and tools won't change drastically
- Tool names and parameters are current as of 2025-10-19
- Developers have access to MCP server (tools are available)
- VS Code Copilot can invoke MCP tools via function calls
- Tool output formats remain consistent

---

## Acceptance Criteria

### Quick Reference Complete
- [ ] All 27 tools listed with one-line descriptions
- [ ] Tools grouped by logical category (8 groups)
- [ ] Alphabetical index with links to full guide
- [ ] File under 150 lines
- [ ] No broken links

### Full Guide Complete
- [ ] All 27 tools documented with full sections
- [ ] Every tool has parameters table
- [ ] Every tool has at least one real example
- [ ] Examples use actual MTM project paths/names
- [ ] Output format described for each tool
- [ ] Troubleshooting section for each tool

### Workflows Documented
- [ ] At least 5 common multi-tool workflows
- [ ] Each workflow shows complete command sequence
- [ ] Workflows tied to actual test fixing scenarios
- [ ] Success criteria defined for each workflow

### Usability
- [ ] Developers find tool in under 30 seconds
- [ ] Examples are copy-pasteable
- [ ] Plain language used in "When to use" sections
- [ ] Technical jargon explained on first use

### Accuracy
- [ ] Tool names match actual MCP tool names
- [ ] Parameters match current tool signatures
- [ ] Examples tested and confirmed working
- [ ] No outdated information

---

## Success Metrics

**Discovery Speed**:
- Time to find relevant tool: < 30 seconds (baseline: 5+ minutes)
- Tool selection accuracy: 95%+ (right tool for the job)

**Usage Success**:
- Commands work on first try: 90%+ (baseline: ~50%)
- Time to understand tool: < 2 minutes (baseline: 5+ minutes trial-and-error)

**Documentation Quality**:
- Quick reference lines: < 150 (scannable)
- Full guide completeness: 100% (all tools documented)
- Example accuracy: 100% (all examples work)
- Dead links: 0

---

## Notes for /speckit.specify

This feature focuses purely on **documentation** of existing MCP tools. No code changes, no new tools, just comprehensive reference material.

Tool list and parameters are known from current MCP server implementation, so **no clarifications needed**.

Can be implemented in parallel with other features since it's reference-only (doesn't depend on test fixing work).
