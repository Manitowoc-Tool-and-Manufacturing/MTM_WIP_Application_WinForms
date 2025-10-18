# SpecKit Prompt Updates - Instruction File Integration

**Date**: 2025-10-18  
**Context**: Phase 2.5 Integration Testing - Adding instruction file reference support to SpecKit

## Changes Made

### 1. Updated speckit.tasks.prompt.md

**Purpose**: Generate tasks with instruction file references to prevent common pitfalls

**Key Changes**:
- Added step to scan `.github/instructions/` directory during task generation
- Instruction file references added to task descriptions with format:
  ```markdown
  **Reference**: .github/instructions/[file].instructions.md - [Brief context]
  ```
- Added instruction file mappings for common task types:
  - Testing → `integration-testing.instructions.md`
  - Database → `mysql-database.instructions.md`
  - C# Code → `csharp-dotnet8.instructions.md`
  - Security → `security-best-practices.instructions.md`
  - Performance → `performance-optimization.instructions.md`
  - Documentation → `documentation.instructions.md`

**Example Task Format**:
```markdown
- [ ] **T111** – Author logging/quick button integration tests
  - **Reference**: .github/instructions/integration-testing.instructions.md - Follow discovery-first workflow (grep_search → verify signatures → write tests)
```

### 2. Updated speckit.implement.prompt.md

**Purpose**: Execute tasks while following referenced instruction files

**Key Changes**:
- Added requirement to scan `.github/instructions/` directory during setup
- New "Instruction File Application" section with specific guidance:
  - Read referenced instruction files BEFORE starting each task
  - Apply patterns and avoid documented pitfalls
  - Ask for clarification if conflicts arise
- Enhanced progress tracking to note which instruction files were applied
- Added documentation step for lessons learned/new patterns
- New "Instruction File Usage Guidelines" section with:
  - Task execution workflow incorporating instruction files
  - Common instruction file mappings
  - Conflict resolution guidance

**Workflow Integration**:
```
1. Read task from tasks.md
2. Check for **Reference**: markers
3. Read referenced instruction files
4. Apply patterns from instructions
5. Implement task
6. Mark task complete and note which instructions were followed
```

## Impact

### For Task Generation (`/tasks`)
- Tasks now include built-in guidance via instruction file references
- Prevents need to remember all patterns manually
- Creates self-documenting tasks that reference authoritative sources
- Reduces ambiguity in task execution

### For Implementation (`/implement`)
- Agents automatically read relevant instruction files before coding
- Pitfalls documented in instruction files are avoided
- Consistent patterns applied across all tasks
- Lessons learned can be captured back into instruction files

### For Future Development
- New instruction files can be added to `.github/instructions/`
- Task generation automatically includes them when relevant
- Implementation automatically applies them
- Creates a feedback loop: pitfalls → instruction files → task references → implementation

## Example Workflow

### Before (Without Instruction Files)
```
Task: Create integration tests for Dao_User
↓
Agent writes tests
↓
Compilation errors (wrong method names, missing null checks, etc.)
↓
Multiple rewrites needed
```

### After (With Instruction Files)
```
Task: Create integration tests for Dao_User
  Reference: .github/instructions/integration-testing.instructions.md
↓
Agent reads integration-testing.instructions.md
↓
Agent follows discovery-first workflow (grep_search first)
↓
Agent writes tests with correct signatures and null checks
↓
Compiles first time
```

## Benefits

1. **Prevents Rework**: Common pitfalls avoided before implementation
2. **Knowledge Transfer**: Patterns documented once, applied consistently
3. **Self-Documenting**: Tasks include their own implementation guidance
4. **Scalable**: New patterns can be added as instruction files
5. **Feedback Loop**: Lessons learned → instruction files → better tasks
6. **Consistency**: All tasks follow same patterns from authoritative sources

## Related Files

- `.github/instructions/integration-testing.instructions.md` - Created to prevent integration test pitfalls
- `specs/002-003-database-layer-complete/integration-testing-pitfalls-resolution.md` - Documents original pitfalls
- `specs/002-003-database-layer-complete/tasks.md` - Updated with instruction file references for T111-T403
- `.github/copilot-instructions.md` - Updated to include integration-testing.instructions.md

## Next Steps

1. When generating new tasks with `/tasks`, instruction file references will be included automatically
2. When implementing with `/implement`, instruction files will be read and applied
3. As new patterns emerge, create new instruction files in `.github/instructions/`
4. Update `speckit.tasks.prompt.md` mappings if new instruction file categories added
