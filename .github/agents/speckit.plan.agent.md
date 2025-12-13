---
description: Execute the implementation planning workflow using the plan template to generate design artifacts.
handoffs: 
  - label: Create Tasks
    agent: speckit.tasks
    prompt: Break the plan into tasks
    send: true
  - label: Create Checklist
    agent: speckit.checklist
    prompt: Create a checklist for the following domain...
---

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Serena MCP Server Usage (REQUIRED)

**For MTM WIP Application**: This agent MUST use Serena MCP server for efficient codebase analysis (300+ C# files).

**Token Efficiency**: Serena provides 80-90% token savings vs reading entire files.

**Required Tools**:
- `mcp_oraios_serena_list_dir` - List directory contents to understand structure
- `mcp_oraios_serena_find_file` - Find files by pattern (e.g., `Dao_*.cs`)
- `mcp_oraios_serena_get_symbols_overview` - Get file structure without reading full content
- `mcp_oraios_serena_find_symbol` - Read specific classes/methods only
- `mcp_oraios_serena_find_referencing_symbols` - Find where symbols are used
- `mcp_oraios_serena_search_for_pattern` - Regex search for patterns (e.g., `MessageBox\.Show`)
- `mcp_oraios_serena_read_memory` - Access project knowledge (architectural_patterns, codebase_structure)
- `mcp_oraios_serena_think_about_collected_information` - Validate findings before proceeding

**Workflow Pattern**:
1. **Discovery**: `list_dir("Data")` → `find_file("Dao_*.cs")` → `get_symbols_overview(file, depth=1)`
2. **Analysis**: `find_symbol("Class/Method", include_body=true)` → `find_referencing_symbols("Method", file)`
3. **Validation**: `search_for_pattern("MessageBox\\.Show")` → `read_memory("architectural_patterns")`
4. **Verification**: `think_about_collected_information()` before generating plan

**Constitution Check**: Use `search_for_pattern` to verify:
- No `MessageBox.Show` usage (forbidden)
- Direct `MySqlConnection`/`SqlConnection` usage limited to documented exceptions
- All DAO methods return `Model_Dao_Result<T>`

See `.github/instructions/serena-semantic-tools.instructions.md` for complete documentation.

## Outline

1. **Setup**: Run `.specify/scripts/powershell/setup-plan.ps1 -Json` from repo root and parse JSON for FEATURE_SPEC, IMPL_PLAN, SPECS_DIR, BRANCH. For single quotes in args like "I'm Groot", use escape syntax: e.g 'I'\''m Groot' (or double-quote if possible: "I'm Groot").

2. **Load context**: Read FEATURE_SPEC and `.specify/memory/constitution.md`. Load IMPL_PLAN template (already copied).

3. **Execute plan workflow**: Follow the structure in IMPL_PLAN template to:
   - Fill Technical Context (mark unknowns as "NEEDS CLARIFICATION")
   - Fill Constitution Check section from constitution
   - Evaluate gates (ERROR if violations unjustified)
   - Phase 0: Generate research.md (resolve all NEEDS CLARIFICATION)
   - Phase 1: Generate data-model.md, contracts/, quickstart.md
   - Phase 1: Update agent context by running the agent script
   - Re-evaluate Constitution Check post-design

4. **Stop and report**: Command ends after Phase 2 planning. Report branch, IMPL_PLAN path, and generated artifacts.

## Phases

### Phase 0: Outline & Research

1. **Extract unknowns from Technical Context** above:
   - For each NEEDS CLARIFICATION → research task
   - For each dependency → best practices task
   - For each integration → patterns task

2. **Generate and dispatch research agents**:

   ```text
   For each unknown in Technical Context:
     Task: "Research {unknown} for {feature context}"
   For each technology choice:
     Task: "Find best practices for {tech} in {domain}"
   ```

3. **Consolidate findings** in `research.md` using format:
   - Decision: [what was chosen]
   - Rationale: [why chosen]
   - Alternatives considered: [what else evaluated]

**Output**: research.md with all NEEDS CLARIFICATION resolved

### Phase 1: Design & Contracts

**Prerequisites:** `research.md` complete

1. **Extract entities from feature spec** → `data-model.md`:
   - Entity name, fields, relationships
   - Validation rules from requirements
   - State transitions if applicable

2. **Generate API contracts** from functional requirements:
   - For each user action → endpoint
   - Use standard REST/GraphQL patterns
   - Output OpenAPI/GraphQL schema to `/contracts/`

3. **Agent context update**:
   - Run `.specify/scripts/powershell/update-agent-context.ps1 -AgentType copilot`
   - These scripts detect which AI agent is in use
   - Update the appropriate agent-specific context file
   - Add only new technology from current plan
   - Preserve manual additions between markers

**Output**: data-model.md, /contracts/*, quickstart.md, agent-specific file

## Key rules

- Use absolute paths
- ERROR on gate failures or unresolved clarifications
