---
description: Generate an actionable, dependency-ordered tasks.md for the feature based on available design artifacts.
---

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt generates task breakdowns that will be executed across multiple sessions. To maximize value:

- **Generate complete task lists** with all phases in a single session
- **Include comprehensive instruction file references** so implementers have full context
- **Add detailed task descriptions** with file paths and acceptance criteria
- **Create parallel execution opportunities** to enable concurrent work
- **Continue through all generation steps** without stopping prematurely
- **Document task dependencies clearly** to enable efficient execution planning

**Do NOT generate partial task lists** or stop after one phase. Complete the entire task breakdown including all user stories, dependencies, and parallel opportunities.

---

## Available MCP Tools

This prompt has access to MCP tools from the **mtm-workflow** server for task generation and validation:

### Speckit Task Tools

**parse_tasks** - Parse and structure existing tasks.md files
- **Input:** `tasks_file` (absolute path to tasks.md)
- **Output:** Task phases, completion status, next actionable tasks, dependencies
- **Use when:** Validating generated tasks, understanding existing task structure, checking for completeness

**load_instructions** - Load instruction file references from tasks
- **Input:** `tasks_file` (absolute path), `instructions_dir` (absolute path to .github/instructions)
- **Output:** Instruction file mapping, existence checks, content loaded
- **Use when:** Validating instruction file references in generated tasks, ensuring all referenced files exist

**mark_task_complete** - Update task completion status
- **Input:** `tasks_file` (absolute path), `task_ids` (array like ["T001", "T002"]), `note` (optional)
- **Output:** Updated tasks.md with [X] markers and completion timestamps
- **Use when:** Tracking task generation progress, marking meta-tasks complete

**analyze_spec_context** - Extract implementation context from specs
- **Input:** `feature_dir` (absolute path to specs directory)
- **Output:** Available docs, tech stack, entities, contracts, recommendations
- **Use when:** Understanding feature scope before task generation, identifying missing documentation

### Usage in Task Generation

```typescript
// Before generating tasks - understand context
analyze_spec_context(feature_dir: "/absolute/path/to/specs/feature-name")

// After generating tasks - validate structure
parse_tasks(tasks_file: "/absolute/path/to/specs/feature-name/tasks.md")

// Validate instruction references
load_instructions(
  tasks_file: "/absolute/path/to/specs/feature-name/tasks.md",
  instructions_dir: "/absolute/path/to/.github/instructions"
)
```

---

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Outline

1. **Setup**: Run `.specify/scripts/powershell/check-prerequisites.ps1 -Json` from repo root and parse FEATURE_DIR and AVAILABLE_DOCS list. All paths must be absolute. For single quotes in args like "I'm Groot", use escape syntax: e.g 'I'\''m Groot' (or double-quote if possible: "I'm Groot").

2. **Load design documents**: Read from FEATURE_DIR:
   - **Required**: plan.md (tech stack, libraries, structure), spec.md (user stories with priorities)
   - **Optional**: data-model.md (entities), contracts/ (API endpoints), research.md (decisions), quickstart.md (test scenarios)
   - Note: Not all projects have all documents. Generate tasks based on what's available.

3. **Execute task generation workflow** (follow the template structure):
   - Load plan.md and extract tech stack, libraries, project structure
   - **Load spec.md and extract user stories with their priorities (P1, P2, P3, etc.)**
   - If data-model.md exists: Extract entities → map to user stories
   - If contracts/ exists: Each file → map endpoints to user stories
   - If research.md exists: Extract decisions → generate setup tasks
   - **Scan .github/instructions/ for relevant instruction files** to reference in task descriptions
   - **Generate tasks ORGANIZED BY USER STORY**:
     - Setup tasks (shared infrastructure needed by all stories)
     - **Foundational tasks (prerequisites that must complete before ANY user story can start)**
     - For each user story (in priority order P1, P2, P3...):
       - Group all tasks needed to complete JUST that story
       - Include models, services, endpoints, UI components specific to that story
       - Mark which tasks are [P] parallelizable
       - If tests requested: Include tests specific to that story
       - **Add instruction file references** for guidance on completing the task
     - Polish/Integration tasks (cross-cutting concerns)
   - **Tests are OPTIONAL**: Only generate test tasks if explicitly requested in the feature spec or user asks for TDD approach
   - Apply task rules:
     - Different files = mark [P] for parallel
     - Same file = sequential (no [P])
     - If tests requested: Tests before implementation (TDD order)
   - Number tasks sequentially (T001, T002...)
   - Generate dependency graph showing user story completion order
   - Create parallel execution examples per user story
   - Validate task completeness (each user story has all needed tasks, independently testable)

4. **Generate tasks.md**: Use `.specify.specify/templates/tasks-template.md` as structure, fill with:
   - Correct feature name from plan.md
   - Phase 1: Setup tasks (project initialization)
   - Phase 2: Foundational tasks (blocking prerequisites for all user stories)
   - Phase 3+: One phase per user story (in priority order from spec.md)
     - Each phase includes: story goal, independent test criteria, tests (if requested), implementation tasks
     - Clear [Story] labels (US1, US2, US3...) for each task
     - [P] markers for parallelizable tasks within each story
     - Checkpoint markers after each story phase
     - **Reference**: Relevant `.github/instructions/*.instructions.md` files with specific guidance
   - Final Phase: Polish & cross-cutting concerns
   - Numbered tasks (T001, T002...) in execution order
   - Clear file paths for each task
   - Dependencies section showing story completion order
   - Parallel execution examples per story
   - Implementation strategy section (MVP first, incremental delivery)
   
   **Instruction File Reference Format**:
   - Add after task description: `**Reference**: .github/instructions/[file].instructions.md - [Brief context on what to follow]`
   - Example: `**Reference**: .github/instructions/integration-testing.instructions.md - Follow discovery-first workflow (grep_search → verify signatures → write tests)`

5. **Report**: Output path to generated tasks.md and summary:
   - Total task count
   - Task count per user story
   - Parallel opportunities identified
   - Independent test criteria for each story
   - Suggested MVP scope (typically just User Story 1)
   - List of instruction files referenced in tasks

Context for task generation: $ARGUMENTS

The tasks.md should be immediately executable - each task must be specific enough that an LLM can complete it without additional context.

## Task Generation Rules

**IMPORTANT**: Tests are optional. Only generate test tasks if the user explicitly requested testing or TDD approach in the feature specification.

**CRITICAL**: Tasks MUST be organized by user story to enable independent implementation and testing.

1. **From User Stories (spec.md)** - PRIMARY ORGANIZATION:
   - Each user story (P1, P2, P3...) gets its own phase
   - Map all related components to their story:
     - Models needed for that story
     - Services needed for that story
     - Endpoints/UI needed for that story
     - If tests requested: Tests specific to that story
   - Mark story dependencies (most stories should be independent)
   - **Add instruction file references** to guide implementation:
     - Testing tasks → `.github/instructions/integration-testing.instructions.md` or `testing-standards.instructions.md`
     - Database tasks → `.github/instructions/mysql-database.instructions.md`
     - Code quality → `.github/instructions/csharp-dotnet8.instructions.md`
     - Security → `.github/instructions/security-best-practices.instructions.md`
     - Performance → `.github/instructions/performance-optimization.instructions.md`
     - Documentation → `.github/instructions/documentation.instructions.md`
   
2. **From Contracts**:
   - Map each contract/endpoint → to the user story it serves
   - If tests requested: Each contract → contract test task [P] before implementation in that story's phase
   
3. **From Data Model**:
   - Map each entity → to the user story(ies) that need it
   - If entity serves multiple stories: Put in earliest story or Setup phase
   - Relationships → service layer tasks in appropriate story phase
   
4. **From Setup/Infrastructure**:
   - Shared infrastructure → Setup phase (Phase 1)
   - Foundational/blocking tasks → Foundational phase (Phase 2)
     - Examples: Database schema setup, authentication framework, core libraries, base configurations
     - These MUST complete before any user story can be implemented
   - Story-specific setup → within that story's phase

5. **Ordering**:
   - Phase 1: Setup (project initialization)
   - Phase 2: Foundational (blocking prerequisites - must complete before user stories)
   - Phase 3+: User Stories in priority order (P1, P2, P3...)
     - Within each story: Tests (if requested) → Models → Services → Endpoints → Integration
   - Final Phase: Polish & Cross-Cutting Concerns
   - Each user story phase should be a complete, independently testable increment
