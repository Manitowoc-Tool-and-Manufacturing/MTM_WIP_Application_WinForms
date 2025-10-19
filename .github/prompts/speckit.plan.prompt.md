---
description: Execute the implementation planning workflow using the plan template to generate design artifacts.
---

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt handles planning workflow with research and design phases. To maximize value:

- **Complete all phases** (Phase 0 research → Phase 1 design) in a single session when possible
- **Generate all design artifacts** without stopping between files
- **Resolve research questions** and create data models in one continuous workflow
- **Run agent context updates** immediately after generating artifacts
- **Continue through constitution checks** without pausing
- **Only stop when blocked** by missing user input or external dependencies

**Do NOT stop after Phase 0** - continue through Phase 1 design generation in the same session.

---

## Available MCP Tools

This prompt has access to MCP tools from the **mtm-workflow** server for planning workflow:

### Speckit Planning Tools

**analyze_spec_context** - Extract implementation context from specification directory
- **Input:** `feature_dir` (absolute path to specs directory)
- **Output:** Available docs, tech stack, entities, contracts, recommendations
- **Use when:** Beginning planning phase, understanding spec completeness, identifying what needs to be generated
- **Returns:** List of existing files, tech stack from spec, entities to model, contract types needed

**verify_ignore_files** - Check and update ignore files (.gitignore, etc.)
- **Input:** `workspace_root` (absolute path), `tech_stack` (optional array like ["csharp", "dotnet", "mysql"])
- **Output:** Ignore file status, missing patterns, recommendations for fixes
- **Use when:** Phase 0 setup, ensuring proper file exclusions, project initialization
- **Checks:** .gitignore existence, essential patterns (bin/, obj/, node_modules/, etc.), technology-specific patterns

**check_checklists** - Validate checklist completion status
- **Input:** `checklist_dir` (absolute path to checklists directory)
- **Output:** Completion status with PASS/FAIL
- **Use when:** After plan generation, validating constitution checks, verifying prerequisites

### Usage in Planning Workflow

```typescript
// Before planning - understand current state
analyze_spec_context(feature_dir: "/absolute/path/to/specs/feature-name")

// During Phase 0 setup - verify ignore files
verify_ignore_files(
  workspace_root: "/absolute/path/to/workspace",
  tech_stack: ["csharp", "dotnet", "mysql"]
)

// After generating plan - validate checklists
check_checklists(checklist_dir: "/absolute/path/to/specs/feature-name/checklists")
```

---

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

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
   ```
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
