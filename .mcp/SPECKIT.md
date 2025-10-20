# SpecKit Tools Overview

The SpecKit helpers ship with the mtm-workflow MCP server and cover research, planning, task execution, and validation workflows. The full set is:

- `analyze_spec_context`
- `load_instructions`
- `mark_task_complete`
- `parse_tasks`
- `validate_build`
- `verify_ignore_files`

All code lives under `mtm-workflow/src/tools/speckit/`. After editing these tools, follow the standard build + robocopy sync process documented in `SETUP-GUIDE.md`.

## When to Use Each Tool

| Tool | Purpose | Typical Prompts |
| --- | --- | --- |
| `analyze_spec_context` | Scans the feature directory to report available docs, tech stack, entities, and recommended follow-up work. Kick off every SpecKit workflow by understanding what already exists. | `/speckit.plan`, `/speckit.analyze`, `/speckit.implement`, `/speckit.specify` |
| `parse_tasks` | Parses `tasks.md` into phases, parts, dependencies, and next actions so the agent knows the current execution order. | `/speckit.tasks`, `/speckit.implement`, `/speckit.analyze` |
| `load_instructions` | Resolves **Reference** links in `tasks.md`, verifies instruction files exist, and inlines their content for quick access. | `/speckit.tasks`, `/speckit.implement` |
| `mark_task_complete` | Marks tasks as finished, adds completion notes, and timestamps progress updates directly in `tasks.md`. | `/speckit.implement` |
| `validate_build` | Runs `dotnet build` (and optionally tests) to validate that the current branch compiles before switching phases or closing work. | `/speckit.implement`, `/speckit.constitution` (gate checks) |
| `verify_ignore_files` | Audits `.gitignore` and related ignore files to ensure new assets will not pollute the repo. Use it whenever a prompt sets up a new feature or workspace. | `/speckit.plan`, `/speckit.implement`, `/speckit.specify` |

## Prompt Integration Checklist

- **Plan/Specify prompts**: Always run `analyze_spec_context` first, then `verify_ignore_files` to confirm scaffolding hygiene.
- **Tasks prompt**: Combine `parse_tasks` and `load_instructions` to build the execution graph before assigning work.
- **Implement prompt**: Chain `parse_tasks` → `load_instructions` → task execution, then `mark_task_complete` and `validate_build` at each phase boundary.
- **Analyze prompt**: Use `analyze_spec_context` for context and `parse_tasks` for coverage mapping before running report logic.

## Best Practices

1. **Resolve context once** – capture the output of `analyze_spec_context` and reference it throughout the session instead of re-scanning.
2. **Stage instruction files** – if `load_instructions` reports missing files, pause implementation and create the required guidance before continuing.
3. **Automate task bookkeeping** – prefer `mark_task_complete` over manual edits so timestamps and completion notes stay consistent.
4. **Validate early** – run `validate_build` whenever a phase completes to catch regressions before moving on.
5. **Keep runtime in sync** – after tool updates, `npm run build` under `.mcp/mtm-workflow`, then mirror to `C:\.mcp` and restart VS Code.
