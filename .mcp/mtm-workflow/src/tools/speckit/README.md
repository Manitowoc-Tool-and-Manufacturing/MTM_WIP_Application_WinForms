# Speckit Implementation Tools

This directory contains MCP tools specifically designed to automate and streamline the speckit implementation workflow. These tools reduce repetitive work for the agent and provide structured automation for common implementation tasks.

## Tools Overview

### parse-tasks.ts
**Purpose**: Parse and structure tasks.md workflow files

**Capabilities**:
- Extracts task phases, parts, and individual tasks
- Identifies completion status for each task
- Detects task references to instruction files
- Finds next actionable tasks based on dependencies
- Parses task notes and progress updates

**Use Cases**:
- Starting implementation to understand workflow
- Identifying which tasks to execute next
- Understanding task dependencies and sequencing
- Tracking overall implementation progress

**Input**: Absolute path to tasks.md file

**Output**: Structured task data with phases, completion stats, and next actionable tasks

---

### load-instructions.ts
**Purpose**: Load and validate instruction file references from tasks

**Capabilities**:
- Scans tasks.md for instruction file references
- Verifies instruction files exist in .github/instructions/
- Loads instruction file content for context
- Maps tasks to their required instruction files
- Identifies missing instruction files

**Use Cases**:
- Preparing to implement tasks
- Validating instruction file availability before starting
- Understanding which patterns apply to which tasks
- Loading context for task execution

**Input**: 
- Absolute path to tasks.md file
- Absolute path to .github/instructions directory

**Output**: Instruction file mapping, existence checks, loaded content summary

---

### mark-task-complete.ts
**Purpose**: Update task completion status in tasks.md

**Capabilities**:
- Marks tasks as complete (changes [ ] to [X])
- Adds completion timestamp
- Adds optional completion notes
- Updates multiple tasks in a single operation
- Preserves file formatting

**Use Cases**:
- Completing tasks and tracking progress
- Adding completion notes and timestamps
- Batch-updating multiple completed tasks
- Maintaining accurate progress tracking

**Input**:
- Absolute path to tasks.md file
- Array of task IDs to mark complete (e.g., ["T100", "T101"])
- Optional completion note

**Output**: Summary of updates applied with success/error status

---

### validate-build.ts
**Purpose**: Run dotnet build and validate compilation

**Capabilities**:
- Auto-detects .csproj files in workspace
- Runs dotnet build with error/warning capture
- Optionally runs tests after build
- Extracts compilation errors and warnings
- Scans for common error patterns in source files
- Provides detailed build output

**Use Cases**:
- After implementing tasks to verify compilation
- Before committing changes
- Running tests as part of validation
- Identifying compilation errors to fix

**Input**:
- Absolute path to workspace root
- Optional: Specific project file path
- Optional: run_tests flag (default: false)
- Optional: check_errors flag (default: true)

**Output**: Build results with error/warning counts, test results, detailed output

---

### verify-ignore-files.ts
**Purpose**: Check and update .gitignore and other ignore files

**Capabilities**:
- Detects which ignore files are needed (git, docker, eslint, prettier, etc.)
- Checks if ignore files exist
- Validates ignore files contain essential patterns
- Identifies missing critical patterns
- Generates recommendations for fixes
- Supports multiple technology stacks

**Use Cases**:
- Project setup phase
- Ensuring proper file exclusions
- Validating ignore file coverage
- Adding missing patterns

**Input**:
- Absolute path to workspace root
- Optional: Technology stack array (e.g., ["csharp", "dotnet", "mysql"])

**Output**: Ignore file status, missing patterns, recommendations

---

### analyze-spec-context.ts
**Purpose**: Extract implementation context from specification directory

**Capabilities**:
- Scans for standard spec files (spec.md, plan.md, tasks.md, etc.)
- Checks for contracts/ and checklists/ directories
- Extracts tech stack from plan.md
- Extracts entities from data-model.md
- Extracts contract types
- Provides recommendations for missing documentation

**Use Cases**:
- Beginning implementation
- Understanding feature scope
- Validating spec completeness
- Getting overview of available documentation

**Input**: Absolute path to feature specification directory

**Output**: Available docs, tech stack, entities, contracts, recommendations

---

## Integration with speckit.implement.prompt.md

These tools are integrated into the speckit implementation workflow:

1. **Initialization** (Step 1): `analyze_spec_context` - Get feature overview
2. **Task Parsing** (Step 3): `parse_tasks` + `load_instructions` - Understand workflow and load instruction files
3. **Setup Verification** (Step 4): `verify_ignore_files` - Ensure proper file exclusions
4. **Progress Tracking** (Step 7): `mark_task_complete` - Track completed tasks
5. **Validation** (Step 8): `validate_build` - Verify compilation after each phase

## Benefits

### For the Agent
- **Reduced repetitive work**: Automates parsing, validation, and status tracking
- **Structured context**: Provides organized data instead of raw file parsing
- **Error prevention**: Validates prerequisites before implementation
- **Progress tracking**: Simplifies task completion tracking

### For the User
- **Faster implementation**: Agent spends less time on overhead tasks
- **Better tracking**: Clear visibility into progress and status
- **Higher quality**: Automated validation catches issues early
- **Consistent workflow**: Standardized implementation process

## Usage Patterns

### Starting New Implementation
```typescript
// 1. Analyze spec context
analyze_spec_context(feature_dir: "/path/to/specs/feature-name")

// 2. Parse tasks
parse_tasks(tasks_file: "/path/to/specs/feature-name/tasks.md")

// 3. Load instruction files
load_instructions(
  tasks_file: "/path/to/specs/feature-name/tasks.md",
  instructions_dir: "/path/to/.github/instructions"
)

// 4. Verify ignore files
verify_ignore_files(
  workspace_root: "/path/to/workspace",
  tech_stack: ["csharp", "dotnet", "mysql"]
)
```

### During Implementation
```typescript
// Mark tasks complete as you go
mark_task_complete(
  tasks_file: "/path/to/specs/feature-name/tasks.md",
  task_ids: ["T100", "T101", "T102"],
  note: "Implemented initial database schema"
)
```

### After Each Phase
```typescript
// Validate build before moving forward
validate_build(
  workspace_root: "/path/to/workspace",
  run_tests: true,
  check_errors: true
)
```

## Future Enhancements

Potential additions to this toolkit:

1. **run-integration-tests.ts** - Execute integration tests and capture results
2. **generate-documentation-matrix.ts** - Auto-generate documentation update tracking
3. **validate-instruction-compliance.ts** - Check code against instruction file patterns
4. **analyze-task-blockers.ts** - Identify blocked tasks and missing prerequisites
5. **generate-progress-report.ts** - Create formatted progress report with metrics

## Maintenance

When adding new tools to this directory:

1. Create tool in `tools/speckit/` directory
2. Update `index.ts` to register the tool
3. Add tool documentation to this README
4. Update `speckit.implement.prompt.md` with usage instructions
5. Add integration patterns to the workflow outline
