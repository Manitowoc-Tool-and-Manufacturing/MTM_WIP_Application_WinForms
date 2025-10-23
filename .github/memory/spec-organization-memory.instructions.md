---
description: 'Specification and initiative organization patterns, consolidation strategies, and artifact management'
applyTo: 'specs/**/*.md,.github/instructions/**/*.md'
---

# Spec Organization Memory

Patterns for organizing specifications, consolidating related initiatives, and managing documentation artifacts across project phases.

## Initiative Consolidation Pattern

When merging related initiatives into a unified effort, use structured folder naming and preserve historical context.

**Naming Convention for Merged Initiatives:**
```
specs/
├── 002-comprehensive-database-layer/     # Original initiative (archived)
├── 003-database-layer-refresh/           # Related initiative (archived)
└── 002-003-database-layer-complete/      # Consolidated initiative (active)
```

**Pattern**: Use hyphenated numeric prefixes to indicate merged scope (`002-003`) while keeping the original initiative numbers for traceability.

**Benefits:**
- Maintains clear lineage to source initiatives
- Single source of truth for active work
- Git history preserves original context
- Easy to reference in commit messages and PRs

## Checklist Migration Strategy

When consolidating initiatives, port relevant quality checklists into the unified spec folder with updated metadata.

**Migration Checklist:**
1. Identify all quality gate checklists from source initiatives
2. Copy to consolidated `checklists/` directory
3. Update frontmatter dates to reflect consolidation date
4. Update cross-references to point to consolidated spec files
5. Remove duplicate or superseded checklists
6. Validate checklist items still apply to merged scope

**Example Update:**
```markdown
# Before (in 002-comprehensive-database-layer/checklists/)
Last Updated: 2025-01-15
See: [spec.md](../spec.md)

# After (in 002-003-database-layer-complete/checklists/)
Last Updated: 2025-10-17 (consolidated from 002/003)
See: [spec.md](../spec.md)
```

**Pattern**: Keep checklist content stable but update metadata to reflect new organizational context.

## Legacy Artifact Retention Policy

Archive superseded spec folders rather than deleting immediately to maintain recovery options and dependency verification.

**Before Deletion Checklist:**
- [ ] Verify no scripts reference legacy spec paths
- [ ] Check documentation links point to consolidated folder
- [ ] Confirm PR descriptions reference new unified spec
- [ ] Search codebase for hardcoded paths to old specs
- [ ] Validate no external tools depend on folder structure

**Recommended Approach:**
1. Create consolidated spec folder
2. Leave original folders intact initially
3. Update all active references to new structure
4. After 2-4 weeks, archive originals to `specs/Archive/`
5. Only delete after extended validation period (3+ months)

**Pattern**: Progressive deprecation with explicit verification gates prevents broken dependencies.

## Task ID Preservation Pattern

When consolidating task lists from multiple initiatives, retain original task identifiers to maintain alignment with historical documentation.

**Task ID Strategy:**
```markdown
## Phase 2.5 – Stored Procedure Standardization
- [ ] **T100** – Discover all stored procedure call sites
- [ ] **T101** – Extract complete database schema snapshot

## Phase 3 – Inventory DAO Refactor
- [ ] **T201** – Refactor Dao_Inventory to async patterns
- [ ] **T202** – Update inventory forms to async handlers
```

**Benefits:**
- Commit messages referencing T100-T106 remain valid
- PR descriptions maintain consistent task references
- Historical progress tracking stays intact
- Easy to map consolidated tasks back to source initiatives

**Pattern**: Use non-overlapping numeric ranges for different initiative phases (T100-T199 for phase 2.5, T201-T299 for phase 3, etc.).

## Spec Folder Structure Standard

Maintain consistent structure across all spec folders for predictable navigation.

**Required Files:**
```
specs/[initiative-name]/
├── spec.md                    # Primary specification document
├── plan.md                    # Implementation plan and approach
├── tasks.md                   # Task breakdown with progress tracking
├── checklists/                # Quality gate checklists
│   ├── requirements.md
│   └── [domain]-quality.md
└── contracts/                 # JSON schemas and API contracts
    └── [contract-name].json
```

**Optional Files:**
- `data-model.md` – Entity relationships and schema design
- `research.md` – Discovery notes and design decisions
- `quickstart.md` – Developer onboarding guide
- `clarification-questions.md` – Outstanding questions and answers

**Pattern**: Core spec/plan/tasks trio provides consistent entry points; optional files add domain-specific depth as needed.

## Multi-Feature Specification Strategy

When multiple related features are identified together, create separate specifications rather than a single combined mega-spec.

**Separate Specifications Enable:**
- Independent implementation and deployment cycles
- Parallel development by different team members
- Focused testing with distinct success criteria
- Flexible prioritization (ship high-priority first)
- Manageable scope for review and estimation

**Anti-Pattern: Combined Mega-Spec**
Avoid combining unrelated features into single spec because it:
- Creates 50+ page documents that overwhelm reviewers
- Mixes HIGH and LOW priority work (can't ship incrementally)
- Has unclear dependencies between unrelated features
- Takes months to complete (all-or-nothing approach)
- Becomes difficult to review and maintain

**Recommended Workflow:**
1. Identify all features and categorize by priority (HIGH/MEDIUM/LOW)
2. Create one `/speckit.specify` command per feature
3. Include complete requirements in each command (self-contained)
4. Execute commands in priority order as resources allow
5. Each spec gets own branch, development cycle, and deployment

**Example Pattern:**
```
# Instead of one mega-spec:
/speckit.specify Create analyzer, performance suite, regression tests, monitoring, and UI automation...

# Use multiple focused specs:
/speckit.specify Create Roslyn analyzer for database layer compliance...
/speckit.specify Create performance benchmarking suite...
/speckit.specify Create regression test plan...
/speckit.specify Create monitoring dashboard...
/speckit.specify Create UI test automation framework...
```

**Quality Indicator**: Each `/speckit.specify` command should:
- Fit in 3-5 paragraphs
- Address a single feature domain
- Have independent success criteria
- Be implementable in 1-4 weeks
- Not depend on other pending specs

**Pattern**: Organize by priority, develop in sequence, deploy incrementally.
