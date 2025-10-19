---
description: Execute the implementation plan by processing and executing all tasks defined in tasks.md
---

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt handles multi-phase implementation workflows. To maximize the value of each premium request:

- **Work through multiple related tasks** in a single session rather than stopping after one
- **Jump between tasks when beneficial** to maintain momentum and complete more work
- **Document partial progress** using task completion notes when switching between tasks
- **Mark completed tasks immediately** with [x] and timestamp
- **Continue until natural checkpoint** (phase boundary, complex blocker, or significant user input needed)
- **Maintain code integrity** - don't leave tasks in broken/non-functional state

**Task Completion Tracking**:
- Partially completed: Add note documenting what was done
- Fully completed: Mark [x] and add completion summary
- Use `mark_task_complete` MCP tool to update tasks.md automatically

**Do NOT stop prematurely** just because one task is complete. Keep working through related tasks until reaching a natural stopping point.

---

## Available MCP Tools

This prompt has access to powerful MCP tools from the **mtm-workflow** server. These tools provide automated analysis, validation, and code generation capabilities specifically designed for the MTM project.

### Speckit Implementation Tools

**parse_tasks** - Parse and structure tasks.md workflow
- **Input:** `tasks_file` (absolute path to tasks.md)
- **Output:** Task phases, completion status, next actionable tasks, dependencies
- **Use when:** Starting implementation, understanding workflow, identifying next tasks

**load_instructions** - Load instruction file references from tasks
- **Input:** `tasks_file` (absolute path), `instructions_dir` (absolute path)
- **Output:** Instruction file mapping, existence checks, content loaded
- **Use when:** Preparing to implement tasks, validating instruction file availability

**mark_task_complete** - Update task completion status
- **Input:** `tasks_file` (absolute path), `task_ids` (array of task IDs), `note` (optional)
- **Output:** Updated tasks.md with [X] markers and completion timestamps
- **Use when:** Completing tasks, tracking progress through implementation

**validate_build** - Verify compilation and run tests
- **Input:** `workspace_root` (absolute path), `project_file` (optional), `run_tests` (optional), `check_errors` (optional)
- **Output:** Build results, error/warning counts, test results
- **Use when:** After implementing tasks, before committing, validating changes

**verify_ignore_files** - Check and update ignore files
- **Input:** `workspace_root` (absolute path), `tech_stack` (optional array)
- **Output:** Ignore file status, missing patterns, recommendations
- **Use when:** Project setup phase, ensuring proper file exclusions

**analyze_spec_context** - Extract implementation context from specs
- **Input:** `feature_dir` (absolute path to specs directory)
- **Output:** Available docs, tech stack, entities, contracts, recommendations
- **Use when:** Beginning implementation, understanding feature scope, validating spec completeness

### Core Validation Tools

**check_checklists** - Validate checklist completion status
- **Input:** `checklist_dir` (absolute path to directory containing markdown checklists)
- **Output:** Table with total/completed/incomplete counts, overall PASS/FAIL status
- **Use when:** Starting implementation to verify prerequisites are complete

**validate_dao_patterns** - Verify DAO compliance with MTM coding standards
- **Input:** `dao_dir` (absolute path), `recursive` (optional, default true)
- **Output:** Validation results with issues by severity (Error/Warning/Info)
- **Checks:** Region organization, Helper_Database_StoredProcedure usage, async/await patterns, Service_ErrorHandler usage, XML docs, anti-patterns (MessageBox.Show, .Result, .Wait())
- **Use when:** Reviewing existing DAOs, validating new DAO implementations, pre-commit quality checks

**validate_error_handling** - Check C# files for proper error handling patterns
- **Input:** `source_dir` (absolute path), `recursive` (optional, default true)
- **Output:** Error handling statistics and issue report
- **Checks:** MessageBox.Show usage (anti-pattern), Service_ErrorHandler usage, try-catch in async methods, empty catch blocks
- **Use when:** Code review, refactoring error handling, ensuring Service_ErrorHandler adoption

**check_xml_docs** - Validate XML documentation coverage
- **Input:** `source_dir` (absolute path), `recursive` (optional, default true), `min_coverage` (optional, default 80)
- **Output:** Coverage report with per-file percentages and undocumented member list
- **Use when:** Documentation compliance checks, pre-PR validation, documentation sprints

### Database Analysis Tools

**analyze_stored_procedures** - Scan SQL procedures for compliance with MTM standards
- **Input:** `procedures_dir` (absolute path), `recursive` (optional, default true)
- **Output:** Compliance report with issues by severity
- **Checks:** Required output parameters (p_Status, p_ErrorMsg), transaction management (COMMIT/ROLLBACK), error handling (DECLARE HANDLER), parameter naming (p_ prefix), SQL injection risks
- **Use when:** Database layer refactoring, stored procedure code review, security audits

**compare_databases** - Detect schema drift between Current and Updated databases
- **Input:** `current_dir` (absolute path), `updated_dir` (absolute path)
- **Output:** Detailed difference report showing added/removed/modified tables and procedures
- **Use when:** Database migration validation, change detection before deployment, schema synchronization

**analyze_dependencies** - Map stored procedure call hierarchies and dependency graphs
- **Input:** `procedures_dir` (absolute path)
- **Output:** Dependency graph, root/leaf procedures, circular dependency detection, call depth analysis, visual dependency tree
- **Use when:** Understanding procedure relationships, refactoring impact analysis, identifying circular dependencies

### Code Generation Tools

**generate_dao_wrapper** - Auto-generate C# DAO method from stored procedure
- **Input:** `procedure_file` (absolute path to SQL file), `output_dir` (optional)
- **Output:** Complete DAO method code with Helper_Database_StoredProcedure integration, parameter mapping, error handling, XML documentation
- **Use when:** Creating new DAOs, standardizing DAO patterns, reducing boilerplate code

**generate_unit_tests** - Auto-generate test scaffolding for C# classes
- **Input:** `source_file` (absolute path to C# file), `test_framework` (optional: xunit/nunit/mstest, default xunit), `output_dir` (optional)
- **Output:** Test class with happy path, error case, and edge case tests
- **Use when:** Starting TDD for new features, improving test coverage, creating test structure

### Code Quality Tools

**suggest_refactoring** - AI-powered refactoring suggestions for C# and SQL code
- **Input:** `source_dir` (absolute path), `file_type` (optional: csharp/sql/all, default all), `recursive` (optional, default true)
- **Output:** Prioritized refactoring recommendations with examples
- **Identifies:** Code smells, complexity issues, performance problems, maintainability concerns
- **Use when:** Technical debt reduction, code quality improvement, pre-refactoring planning

**analyze_performance** - Identify performance bottlenecks in C# code
- **Input:** `source_dir` (absolute path), `focus` (optional: database/ui/all, default all), `recursive` (optional, default true)
- **Output:** Performance score and recommendations
- **Checks:** N+1 queries, blocking async operations, UI thread blocking, inefficient LINQ, string concatenation issues, memory leaks
- **Use when:** Performance optimization, troubleshooting slow operations, code review for performance

**check_security** - Security vulnerability scanner for C# and SQL code
- **Input:** `source_dir` (absolute path), `scan_type` (optional: code/config/all, default all), `recursive` (optional, default true)
- **Output:** Security issue report with CWE IDs and severity levels
- **Detects:** SQL injection, hardcoded credentials, path traversal, weak cryptography, command injection, missing authorization
- **Use when:** Security audits, pre-deployment validation, vulnerability assessment

### Tool Usage Best Practices

1. **Run validation tools early** - Use check_checklists before starting implementation
2. **Validate continuously** - Run validate_dao_patterns and validate_error_handling during development
3. **Generate before writing** - Use generate_dao_wrapper to create standardized DAO methods
4. **Analyze before refactoring** - Use analyze_dependencies and suggest_refactoring to understand impact
5. **Check before committing** - Use check_xml_docs, check_security, and analyze_performance before PR submission
6. **Compare databases regularly** - Use compare_databases to catch schema drift early

### Integration with Instruction Files

MCP tools complement instruction files:
- **validate_dao_patterns** enforces patterns from `csharp-dotnet8.instructions.md`
- **analyze_stored_procedures** validates compliance with `mysql-database.instructions.md`
- **validate_error_handling** checks adherence to error handling standards
- **check_xml_docs** ensures compliance with `documentation.instructions.md`
- **check_security** validates patterns from `security-best-practices.instructions.md`
- **analyze_performance** checks patterns from `performance-optimization.instructions.md`

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Outline

1. **Initialize implementation context**:
   - Run `.specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks` from repo root
   - Parse FEATURE_DIR and AVAILABLE_DOCS list (all paths must be absolute)
   - **USE MCP TOOL**: `analyze_spec_context(feature_dir: "FEATURE_DIR")`
   - Review available documentation, tech stack, entities, and recommendations

2. **Check checklists status** (if FEATURE_DIR/checklists/ exists):
   - **USE MCP TOOL**: `check_checklists(checklist_dir: "FEATURE_DIR/checklists")`
   - The tool will automatically:
     * Scan all checklist files in the directory
     * Count total, completed, and incomplete items
     * Generate a status table
     * Calculate overall PASS/FAIL status
   
   - **If any checklist is incomplete**:
     * Display the table with incomplete item counts
     * **STOP** and ask: "Some checklists are incomplete. Do you want to proceed with implementation anyway? (yes/no)"
     * Wait for user response before continuing
     * If user says "no" or "wait" or "stop", halt execution
     * If user says "yes" or "proceed" or "continue", proceed to step 3
   
   - **If all checklists are complete**:
     * Display the table showing all checklists passed
     * Automatically proceed to step 3

3. **Parse task workflow**:
   - **USE MCP TOOL**: `parse_tasks(tasks_file: "FEATURE_DIR/tasks.md")`
   - Review task phases, completion status, and next actionable tasks
   - **USE MCP TOOL**: `load_instructions(tasks_file: "FEATURE_DIR/tasks.md", instructions_dir: ".github/instructions")`
   - Verify all referenced instruction files are available
   - Load instruction file content for context

4. **Project Setup Verification**:
   - **USE MCP TOOL**: `verify_ignore_files(workspace_root: "WORKSPACE_ROOT", tech_stack: ["csharp", "dotnet", "mysql"])`
   - Review ignore file status and apply recommended patterns if needed
   - Apply any missing critical patterns recommended by the tool

5. Execute implementation following the task plan:
   - **Phase-by-phase execution**: Complete each phase before moving to the next
   - **Respect dependencies**: Run sequential tasks in order, parallel tasks [P] can run together  
   - **Follow TDD approach**: Execute test tasks before their corresponding implementation tasks
   - **File-based coordination**: Tasks affecting the same files must run sequentially
   - **Validation checkpoints**: Verify each phase completion before proceeding
   - **Follow instruction file references**: For each task, read and apply guidance from referenced `.github/instructions/*.instructions.md` files before implementing
   - **Maximize Premium Request value**: ⚠️ **EXTREMELY IMPORTANT**: It is ENCOURAGED to jump between tasks to maximize work completed in a single session. Complete multiple related tasks rather than stopping after one. When jumping between tasks:
     * **MUST add completion notes**: For partially completed tasks, add a note in tasks.md documenting what work was completed
     * **MUST check off completed tasks**: Use `[x]` marker and add completion timestamp for fully finished tasks
     * **MUST maintain task integrity**: Don't leave tasks in broken/non-functional state
     * **Continue until natural checkpoint**: Keep working through related tasks until significant complexity requires user input or a phase boundary is reached
   - **Use MCP tools strategically**:
     * Before DAO work: Run `validate_dao_patterns` on existing DAOs to understand current patterns
     * Before database changes: Run `analyze_stored_procedures` and `compare_databases` to assess impact
     * During code generation: Use `generate_dao_wrapper` for new stored procedure wrappers
     * During testing: Use `generate_unit_tests` to scaffold test structure
     * Before completion: Run `check_xml_docs`, `check_security`, and `analyze_performance` for quality gates
     * For refactoring: Use `analyze_dependencies` and `suggest_refactoring` to plan changes safely
     * For task tracking: Use `mark_task_complete` to update tasks.md with completion notes and timestamps

6. Implementation execution rules:
   - **Setup first**: Initialize project structure, dependencies, configuration
   - **Tests before code**: If you need to write tests for contracts, entities, and integration scenarios
   - **Core development**: Implement models, services, CLI commands, endpoints
   - **Integration work**: Database connections, middleware, logging, external services
   - **Polish and validation**: Unit tests, performance optimization, documentation
   
   **Instruction File Application**:
   - **Before starting each task**: Read referenced instruction files from tasks.md
   - **Integration testing tasks**: Follow `.github/instructions/integration-testing.instructions.md` discovery-first workflow (grep_search method signatures before writing tests)
   - **Database operations**: Apply `.github/instructions/mysql-database.instructions.md` patterns (stored procedures, Helper_Database_StoredProcedure, connection pooling)
   - **C# code**: Follow `.github/instructions/csharp-dotnet8.instructions.md` (async/await, naming conventions, WinForms patterns)
   - **Security**: Apply `.github/instructions/security-best-practices.instructions.md` (input validation, SQL injection prevention)
   - **Performance**: Use `.github/instructions/performance-optimization.instructions.md` (async I/O, caching, collection optimization)
   - **Documentation**: Follow `.github/instructions/documentation.instructions.md` (XML comments, README structure)

7. Progress tracking and error handling:
   - Report progress after each completed task
   - **USE MCP TOOL**: `mark_task_complete(tasks_file: "FEATURE_DIR/tasks.md", task_ids: ["T100", "T101"], note: "Implementation note")`
   - Halt execution if any non-parallel task fails
   - For parallel tasks [P], continue with successful tasks, report failed ones
   - Provide clear error messages with context for debugging
   - Suggest next steps if implementation cannot proceed
   - Note which instruction files were applied for each task
   - **IMPORTANT** - **If pitfalls encountered**: Document in feature spec directory and consider updating relevant instruction files

8. Validation checkpoints:
   - After completing each phase, run: **USE MCP TOOL**: `validate_build(workspace_root: "WORKSPACE_ROOT", run_tests: true)`
   - Fix any compilation errors before proceeding
   - Review warnings and address critical ones

9. Completion validation:
   - Verify all required tasks are completed
   - Check that implemented features match the original specification
   - Validate that tests pass and coverage meets requirements
   - Confirm the implementation follows the technical plan
   - **Verify instruction file compliance**: Check that code follows patterns from referenced instruction files
   - **Run final MCP tool validation suite**:
     * `validate_dao_patterns` - Ensure all DAOs follow MTM patterns
     * `validate_error_handling` - Verify Service_ErrorHandler usage
     * `check_xml_docs` - Confirm documentation coverage meets threshold (80%+)
     * `check_security` - Final security vulnerability scan
     * `analyze_performance` - Check for performance issues
     * `analyze_stored_procedures` - Validate SQL compliance
   - Report final status with summary of completed work
   - **Document lessons learned**: If new patterns or pitfalls discovered, note them for potential instruction file updates

Note: This command assumes a complete task breakdown exists in tasks.md. If tasks are incomplete or missing, suggest running `/tasks` first to regenerate the task list.

## Instruction File Usage Guidelines

**For Task Execution**:
1. Read task description in tasks.md
2. Check for **Reference**: markers with instruction file paths
3. Read referenced instruction files BEFORE implementing
4. Apply patterns, avoid documented pitfalls
5. If conflict or ambiguity: Ask for clarification rather than assume

**Common Instruction File Mappings**:
- Testing/Test Development → `integration-testing.instructions.md` (discovery-first, method signatures, null safety)
- Database/DAO Work → `mysql-database.instructions.md` (stored procedures, connection management, retry logic)
- C# Development → `csharp-dotnet8.instructions.md` (async/await, naming, WinForms patterns)
- Security Concerns → `security-best-practices.instructions.md` (validation, SQL injection, credential management)
- Performance Optimization → `performance-optimization.instructions.md` (async I/O, caching, memory management)
- Documentation → `documentation.instructions.md` (XML docs, README, code comments)
- Code Review → `code-review-standards.instructions.md` (quality checklist, review process)
