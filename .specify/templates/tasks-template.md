---
description: "Task list template for feature implementation"
---

# Tasks: [FEATURE NAME]

**Input**: Design documents from `/specs/[###-feature-name]/`
**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Tests**: The examples below include test tasks. Tests are OPTIONAL - only include them if explicitly requested in the feature specification.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

---

## Task Structure Standards

**⚠️ CRITICAL**: All tasks MUST follow this exact format for MCP tool parsing and agent execution.

### Task Line Format

```markdown
- [ ] **TXXX** [Story: USX] [P] - Task title
```

**Components** (in order):
1. **Checkbox**: `- [ ]` (not started), `- [x]` (completed), or `- [X]` (completed)
2. **Task ID**: `**TXXX**` where XXX is zero-padded sequential number (T001, T002, T010, T100)
3. **Story Tag** (optional): `[Story: Foundation]`, `[Story: US1]`, `[Story: US2]`, `[Story: Integration]`
4. **Parallel Marker** (optional): `[P]` indicates task can run in parallel with others in same phase
5. **Checkpoint Marker** (optional): `[CHECKPOINT]` for manual validation milestones
6. **Separator**: Single hyphen `-` (or em-dash `–` or `—`)
7. **Title**: Clear, action-oriented task description

**Valid Examples**:
```markdown
- [ ] **T001** - Initialize project structure
- [x] **T015** [Story: US1] [P] - Create Entity1 model
- [ ] **T042** [Story: US4] - Implement CSV export helper
- [ ] **T099** [Story: Integration] [CHECKPOINT] - Execute end-to-end validation
```

### Task Body Structure

**Required Fields** (must appear in this order):

1. **File** (if task creates/edits specific file):
   ```markdown
   **File**: `path/to/file.ext`
   ```

2. **Description** (always required):
   ```markdown
   **Description**: Clear, detailed explanation of implementation requirements. Include specific classes, methods, parameters, or configuration details needed.
   ```

3. **Reference** (one or more instruction files):
   ```markdown
   **Reference**: `.github/instructions/[filename].instructions.md` - Brief context on what to follow
   ```

4. **Acceptance** (always required):
   ```markdown
   **Acceptance**: Specific, testable criteria for task completion
   ```

5. **Completed** (optional, added when task finishes):
   ```markdown
   **Completed**: YYYY-MM-DD – Brief summary of work completed
   ```

6. **Note** (optional, for context or warnings):
   ```markdown
   **Note**: Additional context, dependencies, or implementation notes
   ```

### Complete Task Example

```markdown
- [ ] **T007** [Story: Foundation] - Extend Dao_ErrorReports with GetAllErrorReportsAsync
  **File**: `Data/Dao_ErrorReports.cs`
  **Description**: Add async method accepting Model_ErrorReport_Core_Filter parameter. Build Dictionary with DBNull.Value for null filters. Call sp_error_reports_GetAll via Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync. Return Model_Dao_Result<DataTable>. Add to #region Database Operations.
  **Reference**: `.github/instructions/mysql-database.instructions.md` - Helper_Database_StoredProcedure usage pattern
  **Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Async/await patterns and region organization
  **Acceptance**: Method compiles, uses async correctly, returns Model_Dao_Result<DataTable>, handles null filters with DBNull.Value
  **Note**: This method is called by the grid control's LoadReportsAsync method
```

### Instruction File Reference Rules

**Pattern**: `**Reference**: \`.github/instructions/[filename].instructions.md\` - [Context]`

**Requirements**:
- Always wrap path in backticks
- Always use `.github/instructions/` prefix
- Always use `.instructions.md` suffix
- Include hyphen followed by brief context (what pattern/guidance to follow)
- Can include multiple Reference lines per task
- Place after Description, before Acceptance

**Available Instruction Files**:
- `integration-testing.instructions.md` - Test development, discovery-first workflow, null safety
- `testing-standards.instructions.md` - Manual validation, success criteria, test scenarios
- `mysql-database.instructions.md` - Stored procedures, connection management, Helper patterns
- `csharp-dotnet8.instructions.md` - Language features, async/await, region organization, WinForms
- `security-best-practices.instructions.md` - Input validation, SQL injection prevention
- `performance-optimization.instructions.md` - Async I/O, connection pooling, memory management
- `documentation.instructions.md` - XML docs, README structure, code comments
- `code-review-standards.instructions.md` - Quality checklist, review process
- `ui-scaling-consistency.instructions.md` - DPI scaling, WinForms layout, theme application

**Reference Example Patterns**:
```markdown
**Reference**: `.github/instructions/mysql-database.instructions.md` - Follow stored procedure standards (p_ prefix, OUT parameters)
**Reference**: `.github/instructions/integration-testing.instructions.md` - Discovery-first workflow (grep_search → verify signatures → write tests)
**Reference**: `.github/instructions/csharp-dotnet8.instructions.md` - Use async Task<T>, proper region organization
**Reference**: `.github/instructions/security-best-practices.instructions.md` - Input validation and parameterized queries
```

---

## Format: `[ID] [P?] [Story] Description`
- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3)
- Include exact file paths in descriptions
- **Reference**: Add instruction file references for guidance (e.g., `.github/instructions/integration-testing.instructions.md`)

## Task Completion Tracking

**⚠️ IMPORTANT - Premium Request Maximization**:
- Agents are ENCOURAGED to jump between tasks to maximize work completed per session
- When working on multiple tasks:
  - **Partially completed tasks**: Add completion note with `**Completed**: YYYY-MM-DD - [description of work done]`
  - **Fully completed tasks**: Mark with `[x]` and add `**Completed**: YYYY-MM-DD - [brief summary]`
  - **Must maintain integrity**: Don't leave tasks in broken/non-functional state
  - **Continue until checkpoint**: Work through related tasks until natural stopping point

**Completion Note Format**:
```markdown
- [x] **T001** – Task description
  - **Completed**: 2025-10-18 - Successfully implemented feature X, added tests, verified build
  - **Reference**: .github/instructions/[file].instructions.md
```

**Partial Completion Format**:
```markdown
- [ ] **T002** – Task description  
  - **Completed**: 2025-10-18 - Created base class and interface, wired up events. Still need: validation logic and error handling
  - **Reference**: .github/instructions/[file].instructions.md
```

## Path Conventions
- **Single project**: `src/`, `tests/` at repository root
- **Web app**: `backend/src/`, `frontend/src/`
- **Mobile**: `api/src/`, `ios/src/` or `android/src/`
- Paths shown below assume single project - adjust based on plan.md structure

<!-- 
  ============================================================================
  IMPORTANT: The tasks below are SAMPLE TASKS for illustration purposes only.
  
  The /speckit.tasks command MUST replace these with actual tasks based on:
  - User stories from spec.md (with their priorities P1, P2, P3...)
  - Feature requirements from plan.md
  - Entities from data-model.md
  - Endpoints from contracts/
  
  Tasks MUST be organized by user story so each story can be:
  - Implemented independently
  - Tested independently
  - Delivered as an MVP increment
  
  DO NOT keep these sample tasks in the generated tasks.md file.
  ============================================================================
-->

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [ ] T001 Create project structure per implementation plan
  - **Reference**: .github/instructions/[relevant-file].instructions.md
- [ ] T002 Initialize [language] project with [framework] dependencies
  - **Reference**: .github/instructions/[relevant-file].instructions.md
- [ ] T003 [P] Configure linting and formatting tools
  - **Reference**: .github/instructions/[relevant-file].instructions.md

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

Examples of foundational tasks (adjust based on your project):

- [ ] T004 Setup database schema and migrations framework
  - **Reference**: .github/instructions/mysql-database.instructions.md - Follow stored procedure patterns
- [ ] T005 [P] Implement authentication/authorization framework
  - **Reference**: .github/instructions/security-best-practices.instructions.md
- [ ] T006 [P] Setup API routing and middleware structure
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T007 Create base models/entities that all stories depend on
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T008 Configure error handling and logging infrastructure
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md - Use Service_ErrorHandler patterns
- [ ] T009 Setup environment configuration management
  - **Reference**: .github/instructions/security-best-practices.instructions.md

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - [Title] (Priority: P1) 🎯 MVP

**Goal**: [Brief description of what this story delivers]

**Independent Test**: [How to verify this story works on its own]

### Tests for User Story 1 (OPTIONAL - only if tests requested) ⚠️

**NOTE: Write these tests FIRST, ensure they FAIL before implementation**

- [ ] T010 [P] [US1] Contract test for [endpoint] in tests/contract/test_[name].py
  - **Reference**: .github/instructions/integration-testing.instructions.md - Follow discovery-first workflow
- [ ] T011 [P] [US1] Integration test for [user journey] in tests/integration/test_[name].py
  - **Reference**: .github/instructions/testing-standards.instructions.md

### Implementation for User Story 1

- [ ] T012 [P] [US1] Create [Entity1] model in src/models/[entity1].py
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md - Follow naming conventions
- [ ] T013 [P] [US1] Create [Entity2] model in src/models/[entity2].py
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T014 [US1] Implement [Service] in src/services/[service].py (depends on T012, T013)
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md - Async patterns
- [ ] T015 [US1] Implement [endpoint/feature] in src/[location]/[file].py
  - **Reference**: .github/instructions/mysql-database.instructions.md - Use Helper_Database_StoredProcedure
- [ ] T016 [US1] Add validation and error handling
  - **Reference**: .github/instructions/security-best-practices.instructions.md
- [ ] T017 [US1] Add logging for user story 1 operations
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - [Title] (Priority: P2)

**Goal**: [Brief description of what this story delivers]

**Independent Test**: [How to verify this story works on its own]

### Tests for User Story 2 (OPTIONAL - only if tests requested) ⚠️

- [ ] T018 [P] [US2] Contract test for [endpoint] in tests/contract/test_[name].py
  - **Reference**: .github/instructions/integration-testing.instructions.md
- [ ] T019 [P] [US2] Integration test for [user journey] in tests/integration/test_[name].py
  - **Reference**: .github/instructions/testing-standards.instructions.md

### Implementation for User Story 2

- [ ] T020 [P] [US2] Create [Entity] model in src/models/[entity].py
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T021 [US2] Implement [Service] in src/services/[service].py
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T022 [US2] Implement [endpoint/feature] in src/[location]/[file].py
  - **Reference**: .github/instructions/mysql-database.instructions.md
- [ ] T023 [US2] Integrate with User Story 1 components (if needed)
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - [Title] (Priority: P3)

**Goal**: [Brief description of what this story delivers]

**Independent Test**: [How to verify this story works on its own]

### Tests for User Story 3 (OPTIONAL - only if tests requested) ⚠️

- [ ] T024 [P] [US3] Contract test for [endpoint] in tests/contract/test_[name].py
  - **Reference**: .github/instructions/integration-testing.instructions.md
- [ ] T025 [P] [US3] Integration test for [user journey] in tests/integration/test_[name].py
  - **Reference**: .github/instructions/testing-standards.instructions.md

### Implementation for User Story 3

- [ ] T026 [P] [US3] Create [Entity] model in src/models/[entity].py
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T027 [US3] Implement [Service] in src/services/[service].py
  - **Reference**: .github/instructions/csharp-dotnet8.instructions.md
- [ ] T028 [US3] Implement [endpoint/feature] in src/[location]/[file].py
  - **Reference**: .github/instructions/mysql-database.instructions.md

**Checkpoint**: All user stories should now be independently functional

---

[Add more user story phases as needed, following the same pattern]

---

## Phase N: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] TXXX [P] Documentation updates in docs/
  - **Reference**: .github/instructions/documentation.instructions.md
- [ ] TXXX Code cleanup and refactoring
  - **Reference**: .github/instructions/code-review-standards.instructions.md
- [ ] TXXX Performance optimization across all stories
  - **Reference**: .github/instructions/performance-optimization.instructions.md
- [ ] TXXX [P] Additional unit tests (if requested) in tests/unit/
  - **Reference**: .github/instructions/testing-standards.instructions.md
- [ ] TXXX Security hardening
  - **Reference**: .github/instructions/security-best-practices.instructions.md
- [ ] TXXX Run quickstart.md validation

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
  - User stories can then proceed in parallel (if staffed)
  - Or sequentially in priority order (P1 → P2 → P3)
- **Polish (Final Phase)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) - No dependencies on other stories
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) - May integrate with US1 but should be independently testable
- **User Story 3 (P3)**: Can start after Foundational (Phase 2) - May integrate with US1/US2 but should be independently testable

### Within Each User Story

- Tests (if included) MUST be written and FAIL before implementation
- Models before services
- Services before endpoints
- Core implementation before integration
- Story complete before moving to next priority

### Parallel Opportunities

- All Setup tasks marked [P] can run in parallel
- All Foundational tasks marked [P] can run in parallel (within Phase 2)
- Once Foundational phase completes, all user stories can start in parallel (if team capacity allows)
- All tests for a user story marked [P] can run in parallel
- Models within a story marked [P] can run in parallel
- Different user stories can be worked on in parallel by different team members

---

## Parallel Example: User Story 1

```bash
# Launch all tests for User Story 1 together (if tests requested):
Task: "Contract test for [endpoint] in tests/contract/test_[name].py"
Task: "Integration test for [user journey] in tests/integration/test_[name].py"

# Launch all models for User Story 1 together:
Task: "Create [Entity1] model in src/models/[entity1].py"
Task: "Create [Entity2] model in src/models/[entity2].py"
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL - blocks all stories)
3. Complete Phase 3: User Story 1
4. **STOP and VALIDATE**: Test User Story 1 independently
5. Deploy/demo if ready

### Incremental Delivery

1. Complete Setup + Foundational → Foundation ready
2. Add User Story 1 → Test independently → Deploy/Demo (MVP!)
3. Add User Story 2 → Test independently → Deploy/Demo
4. Add User Story 3 → Test independently → Deploy/Demo
5. Each story adds value without breaking previous stories

### Parallel Team Strategy

With multiple developers:

1. Team completes Setup + Foundational together
2. Once Foundational is done:
   - Developer A: User Story 1
   - Developer B: User Story 2
   - Developer C: User Story 3
3. Stories complete and integrate independently

---

## Instruction File References

Tasks in this file reference instruction files from `.github/instructions/` for implementation guidance:

- **integration-testing.instructions.md** - Discovery-first workflow, method signature verification, null safety patterns
- **testing-standards.instructions.md** - Manual validation approach, success criteria patterns
- **mysql-database.instructions.md** - Stored procedure standards, connection management, Helper patterns
- **csharp-dotnet8.instructions.md** - Language features, naming conventions, async/await, WinForms patterns
- **security-best-practices.instructions.md** - Input validation, SQL injection prevention, credential management
- **performance-optimization.instructions.md** - Async I/O, connection pooling, memory management, caching
- **documentation.instructions.md** - XML documentation, README structure, code comments
- **code-review-standards.instructions.md** - Quality checklist, review process, anti-patterns

**How to use instruction files**:
1. Read referenced file BEFORE starting task implementation
2. Apply documented patterns and avoid documented pitfalls
3. If conflict or ambiguity: Ask for clarification rather than assume

---

## Available MCP Tools

Agents implementing these tasks have access to MCP tools from the **mtm-workflow** server:

### Validation Tools
- `validate_dao_patterns` - Check DAO compliance with MTM standards
- `validate_error_handling` - Verify Service_ErrorHandler usage
- `check_xml_docs` - Validate documentation coverage
- `analyze_stored_procedures` - Check SQL procedure compliance
- `check_security` - Security vulnerability scanner
- `analyze_performance` - Identify performance bottlenecks

### Code Generation Tools
- `generate_dao_wrapper` - Auto-generate DAO methods from stored procedures
- `generate_unit_tests` - Scaffold test classes

### Analysis Tools
- `analyze_dependencies` - Map stored procedure call hierarchies
- `compare_databases` - Detect schema drift
- `suggest_refactoring` - AI-powered refactoring recommendations

### Task Management Tools
- `parse_tasks` - Parse this tasks.md file
- `mark_task_complete` - Update task completion status with notes
- `load_instructions` - Load instruction file references
- `validate_build` - Verify compilation and tests

**Use MCP tools strategically**:
- Before implementation: Run validation tools to understand current patterns
- During implementation: Use generation tools to create standardized code
- After implementation: Run validation and build tools before committing

---

## Notes

- [P] tasks = different files, no dependencies
- [Story] label maps task to specific user story for traceability
- Each user story should be independently completable and testable
- Verify tests fail before implementing
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Avoid: vague tasks, same file conflicts, cross-story dependencies that break independence


