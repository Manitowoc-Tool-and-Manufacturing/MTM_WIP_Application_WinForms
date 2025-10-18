---
description: Execute the implementation plan by processing and executing all tasks defined in tasks.md
---

## Required MCP Tools

This prompt requires the following MCP tools from the **mtm-workflow** server:
- `check_checklists` - Validate checklist completion status

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Outline

1. Run `.specify/scripts/powershell/check-prerequisites.ps1 -Json -RequireTasks -IncludeTasks` from repo root and parse FEATURE_DIR and AVAILABLE_DOCS list. All paths must be absolute. For single quotes in args like "I'm Groot", use escape syntax: e.g 'I'\''m Groot' (or double-quote if possible: "I'm Groot").

2. **Check checklists status** (if FEATURE_DIR/checklists/ exists):
   - **USE MCP TOOL**: `mcp_mtm-workflow_check_checklists(checklist_dir: "FEATURE_DIR/checklists")`
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

3. Load and analyze the implementation context:
   - **REQUIRED**: Read tasks.md for the complete task list and execution plan
   - **REQUIRED**: Read plan.md for tech stack, architecture, and file structure
   - **REQUIRED**: Scan .github/instructions/ directory for instruction files referenced in tasks
   - **IF EXISTS**: Read data-model.md for entities and relationships
   - **IF EXISTS**: Read contracts/ for API specifications and test requirements
   - **IF EXISTS**: Read research.md for technical decisions and constraints
   - **IF EXISTS**: Read quickstart.md for integration scenarios

4. **Project Setup Verification**:
   - **REQUIRED**: Create/verify ignore files based on actual project setup:
   
   **Detection & Creation Logic**:
   - Check if the following command succeeds to determine if the repository is a git repo (create/verify .gitignore if so):

     ```sh
     git rev-parse --git-dir 2>/dev/null
     ```
   - Check if Dockerfile* exists or Docker in plan.md → create/verify .dockerignore
   - Check if .eslintrc* or eslint.config.* exists → create/verify .eslintignore
   - Check if .prettierrc* exists → create/verify .prettierignore
   - Check if .npmrc or package.json exists → create/verify .npmignore (if publishing)
   - Check if terraform files (*.tf) exist → create/verify .terraformignore
   - Check if .helmignore needed (helm charts present) → create/verify .helmignore
   
   **If ignore file already exists**: Verify it contains essential patterns, append missing critical patterns only
   **If ignore file missing**: Create with full pattern set for detected technology
   
   **Common Patterns by Technology** (from plan.md tech stack):
   - **Node.js/JavaScript**: `node_modules/`, `dist/`, `build/`, `*.log`, `.env*`
   - **Python**: `__pycache__/`, `*.pyc`, `.venv/`, `venv/`, `dist/`, `*.egg-info/`
   - **Java**: `target/`, `*.class`, `*.jar`, `.gradle/`, `build/`
   - **C#/.NET**: `bin/`, `obj/`, `*.user`, `*.suo`, `packages/`
   - **Go**: `*.exe`, `*.test`, `vendor/`, `*.out`
   - **Universal**: `.DS_Store`, `Thumbs.db`, `*.tmp`, `*.swp`, `.vscode/`, `.idea/`
   
   **Tool-Specific Patterns**:
   - **Docker**: `node_modules/`, `.git/`, `Dockerfile*`, `.dockerignore`, `*.log*`, `.env*`, `coverage/`
   - **ESLint**: `node_modules/`, `dist/`, `build/`, `coverage/`, `*.min.js`
   - **Prettier**: `node_modules/`, `dist/`, `build/`, `coverage/`, `package-lock.json`, `yarn.lock`, `pnpm-lock.yaml`
   - **Terraform**: `.terraform/`, `*.tfstate*`, `*.tfvars`, `.terraform.lock.hcl`

5. Parse tasks.md structure and extract:
   - **Task phases**: Setup, Tests, Core, Integration, Polish
   - **Task dependencies**: Sequential vs parallel execution rules
   - **Task details**: ID, description, file paths, parallel markers [P]
   - **Execution flow**: Order and dependency requirements

6. Execute implementation following the task plan:
   - **Phase-by-phase execution**: Complete each phase before moving to the next
   - **Respect dependencies**: Run sequential tasks in order, parallel tasks [P] can run together  
   - **Follow TDD approach**: Execute test tasks before their corresponding implementation tasks
   - **File-based coordination**: Tasks affecting the same files must run sequentially
   - **Validation checkpoints**: Verify each phase completion before proceeding
   - **Follow instruction file references**: For each task, read and apply guidance from referenced `.github/instructions/*.instructions.md` files before implementing

7. Implementation execution rules:
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

8. Progress tracking and error handling:
   - Report progress after each completed task
   - Halt execution if any non-parallel task fails
   - For parallel tasks [P], continue with successful tasks, report failed ones
   - Provide clear error messages with context for debugging
   - Suggest next steps if implementation cannot proceed
   - **IMPORTANT** For completed tasks, mark as [X] in tasks.md and note which instruction files were applied
   - **If pitfalls encountered**: Document in feature spec directory and consider updating relevant instruction files

9. Completion validation:
   - Verify all required tasks are completed
   - Check that implemented features match the original specification
   - Validate that tests pass and coverage meets requirements
   - Confirm the implementation follows the technical plan
   - **Verify instruction file compliance**: Check that code follows patterns from referenced instruction files
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
