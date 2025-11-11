# Create New Feature Spec with BMAD + Speckit

**Time to Complete:** 15 minutes  
**What You'll Learn:** Structured feature planning with Speckit framework + BMAD agents  
**Use Case:** Complex features needing formal documentation, team collaboration

---

## When To Use This Workflow

✅ **Good For:**
- Complex features (> 2 weeks development)
- Team collaboration required
- Formal documentation needed
- Long-term maintenance expected
- Features with multiple stakeholders

❌ **Use Pure BMAD Instead If:**
- Quick prototypes or experiments
- Solo development on small features
- Informal requirements acceptable

---

## Prerequisites

- [ ] Speckit installed (\.specify/\ folder exists)
- [ ] BMAD installed (\.bmad-core/\ folder exists)
- [ ] Feature idea clearly defined
- [ ] Understanding of Speckit workflow (planning framework)

---

## Workflow Overview

```
Idea → Speckit Plan → BMAD Agents Fill Spec → Tasks → Implementation
  ↓         ↓              ↓                      ↓          ↓
User   /speckit.plan    @pm, @architect      @sm, @dev    @dev
```

**Time Estimate:** 2-4 hours for planning, days/weeks for implementation

---

## Step 1: Create Feature Folder

### Task Checklist
- [ ] Identify next feature number
- [ ] Create feature folder in \/specs\
- [ ] Create initial spec files

### Commands

```powershell
# Find next feature number
cd specs
ls | Select-Object Name

# Create feature folder (example: 007-inventory-export)
New-Item -ItemType Directory -Path "007-inventory-export"
cd 007-inventory-export

# Create spec scaffold
New-Item -ItemType File -Path "spec.md"
New-Item -ItemType File -Path "plan.md"
New-Item -ItemType File -Path "tasks.md"
New-Item -ItemType File -Path "data-model.md"
New-Item -ItemType File -Path "research.md"
```

**Folder Structure Created:**
```
specs/007-inventory-export/
├── spec.md           # Feature specification (Speckit managed)
├── plan.md          # Implementation plan
├── tasks.md         # Task breakdown
├── data-model.md    # Data structures
└── research.md      # Research findings
```

**Why**: Consistent folder structure enables team navigation and tooling

---

## Step 2: Initial Spec Template

### Task Checklist
- [ ] Copy spec template from Speckit
- [ ] Fill in feature name and description
- [ ] Save spec.md

### Commands

```
@pm I need help creating an initial spec for an Inventory Export feature that lets users export transaction history to Excel. Use the Speckit spec template structure.
```

**What @pm Will Provide:**
- Spec template with sections
- Initial feature description
- Placeholder user stories
- Acceptance criteria structure

### Example spec.md Sections

```markdown
# Specification: Inventory Export to Excel

## Feature Overview
Export transaction history to Excel format for offline analysis and reporting.

## User Stories

### US1: Export Current View
As a floor supervisor  
I want to export the current transaction grid view to Excel  
So that I can analyze data offline in pivot tables

**Acceptance Criteria:**
- [ ] Export button visible in grid toolbar
- [ ] Exports visible columns only
- [ ] Opens Excel automatically after export

[Continue with more user stories...]
```

**Why**: Spec template ensures consistent documentation across features

---

## Step 3: Run Speckit Planning Workflow

### Task Checklist
- [ ] Invoke Speckit planning command
- [ ] Answer clarification questions
- [ ] Review generated artifacts

### Commands

In your Speckit-integrated environment (if using):
```
/speckit.plan Create specification for inventory export to Excel feature
```

**Speckit Will:**
1. Ask clarification questions
2. Generate research findings
3. Create data model
4. Propose implementation plan
5. Generate task breakdown

### Clarification Questions Example

```
Q1: What file format(s) should be supported?
   A: Excel (.xlsx) only

Q2: Should historical data be exportable or current view only?
   A: Both - user selects scope

Q3: Column selection - all columns or user-configurable?
   A: User-configurable (checkbox selection)

Q4: Maximum record limit for performance?
   A: 10,000 records with warning, 50,000 hard limit
```

**Why**: Clarification questions prevent ambiguity and rework

---

## Step 4: Enhance Spec with @pm

### Task Checklist
- [ ] Review Speckit-generated spec
- [ ] Ask @pm to enhance user stories
- [ ] Refine acceptance criteria

### Commands

```
@pm Review specs/007-inventory-export/spec.md and enhance the user stories with:
- Clear WHO/WHAT/WHY format
- Detailed acceptance criteria
- Edge cases and error scenarios
- MTM-specific constraints (WinForms, theme integration)
```

**What @pm Will Add:**
- Detailed user stories
- Priority levels (P1, P2, P3)
- Success metrics
- Out of scope items
- Non-functional requirements

### Validation Checklist
- [ ] Each user story has clear acceptance criteria
- [ ] Success metrics are measurable
- [ ] Technical constraints mentioned (MTM patterns)
- [ ] Out of scope explicitly documented

**Why**: Clear requirements prevent scope creep and enable accurate estimation

---

## Step 5: Design Architecture with @architect

### Task Checklist
- [ ] Share spec with @architect
- [ ] Request architecture design
- [ ] Document in plan.md

### Commands

```
@architect Based on specs/007-inventory-export/spec.md, design the architecture for Excel export. Reference:
- Documentation/BROWNFIELD_ARCHITECTURE.md
- specs/006-print-and-export/ (similar export functionality)
- Follow MTM constitution rules

Create architecture section for plan.md including:
- Component modifications needed
- New classes/services required
- Integration points
- Theme integration approach
- Error handling strategy
```

**What @architect Will Provide:**

```markdown
## Architecture

### Components

**New:**
- \Helpers/Helper_ExcelExporter.cs\ - ClosedXML wrapper
- \Models/Model_Excel_ExportJob.cs\ - Configuration object

**Modified:**
- \Forms/Transactions/Transactions.cs\ - Add Export button
- \Controls/Transactions/TransactionGridControl.cs\ - Wire export event

### Integration

- Uses existing \Helper_ExportManager\ pattern
- Follows \Helper_PrintManager\ async workflow
- Integrates with \Service_ErrorHandler\ for errors
- Theme integration via \Core_Themes\

### Dependencies

- ClosedXML NuGet package (v0.105.0)
- Existing \Model_Dao_Result<T>\ pattern
```

**Why**: Architecture guides implementation and prevents technical debt

---

## Step 6: Create Data Model

### Task Checklist
- [ ] Identify entities and structures
- [ ] Document in data-model.md
- [ ] Get @architect validation

### Commands

```
@architect Create a detailed data model for specs/007-inventory-export/data-model.md. Include:
- Model_Excel_ExportJob structure
- Model_Excel_ExportSettings structure
- Enum for export scope (CurrentView, DateRange, AllData)
- Property descriptions and constraints
```

**Example Data Model:**

```markdown
## Model_Excel_ExportJob

Configuration object for Excel export operations.

### Properties

| Property | Type | Description | Constraints |
|----------|------|-------------|-------------|
| Data | DataTable | Source data | Required, max 50,000 rows |
| FileName | string | Output filename | Required, .xlsx extension |
| VisibleColumns | List<string> | Columns to export | Optional, null = all |
| IncludeHeaders | bool | Export column headers | Default: true |
| AutoOpen | bool | Open Excel after export | Default: true |

### Dependencies

- \Helper_ExportManager\ for execution
- \Model_Dao_Result<string>\ for return type (file path)
```

**Why**: Clear data model prevents implementation confusion

---

## Step 7: Generate Tasks with @sm

### Task Checklist
- [ ] Request task breakdown from @sm
- [ ] Review and organize by user story
- [ ] Save to tasks.md

### Commands

```
@sm Based on specs/007-inventory-export/spec.md and plan.md, create a detailed task breakdown for tasks.md. Follow the tasks-template.md format with:
- Tasks organized by user story
- Parallel opportunities marked [P]
- Reference to instruction files
- Clear acceptance criteria per task
```

**What @sm Will Create:**

```markdown
## Phase 1: Foundation

- [ ] **T001** - Install ClosedXML NuGet package
  **Reference**: \.github/instructions/csharp-dotnet8.instructions.md\
  **Acceptance**: Package installed, project compiles

- [ ] **T002** [P] - Create Model_Excel_ExportJob
  **File**: \Models/Model_Excel_ExportJob.cs\
  **Reference**: \.github/instructions/csharp-dotnet8.instructions.md\
  **Acceptance**: Model created with XML docs, follows naming conventions

## Phase 2: User Story 1 - Export Current View

- [ ] **T003** [US1] - Add Export button to TransactionGridControl
  **File**: \Controls/Transactions/TransactionGridControl.cs\
  **Reference**: \.github/instructions/ui-scaling-consistency.instructions.md\
  **Acceptance**: Button follows naming convention, theme integration applied

[Continue with more tasks...]
```

**Why**: Task breakdown enables incremental development and progress tracking

---

## Step 8: Validate with @po

### Task Checklist
- [ ] Run PO master checklist
- [ ] Ensure PRD-Architecture alignment
- [ ] Fix any gaps identified

### Commands

```
@po Run master checklist on specs/007-inventory-export/. Validate:
- spec.md completeness
- plan.md alignment with spec
- data-model.md consistency
- tasks.md coverage of user stories
```

**What @po Will Check:**
- All user stories have corresponding tasks
- Architecture addresses all requirements
- Data model supports all user stories
- No conflicting requirements
- MTM constitution compliance in design

### Common @po Findings

**Finding**: User Story 3 not reflected in tasks  
**Action**: Add tasks for US3

**Finding**: Architecture doesn't address error handling  
**Action**: @architect adds error handling section

**Finding**: Data model missing validation constraints  
**Action**: Add validation rules to data-model.md

**Why**: PO validation prevents incomplete specifications

---

## Step 9: Create Feature Branch

### Task Checklist
- [ ] Commit specification documents
- [ ] Create feature branch
- [ ] Begin implementation

### Commands

```powershell
# Stage spec files
git add specs/007-inventory-export/

# Commit
git commit -m "docs: Add specification for Inventory Export feature

- Created spec.md with 3 user stories
- Designed architecture in plan.md
- Documented data model
- Generated task breakdown (25 tasks)

Ready for implementation phase."

# Create feature branch
git checkout -b 007-inventory-export

# Now ready for implementation!
```

**Why**: Specification commits provide baseline for implementation tracking

---

## Step 10: Begin Implementation with @dev

### Task Checklist
- [ ] Share spec and tasks with @dev
- [ ] Implement tasks incrementally
- [ ] Use @qa reviews at checkpoints

### Commands

```
@dev Review specs/007-inventory-export/spec.md and tasks.md. Implement the next incomplete task starting with T001.
```

**Implementation Pattern:**

```
# Task 1
@dev Implement task T001 from specs/007-inventory-export/tasks.md

# Review
@qa *review Check the implementation against spec

# Task 2
@dev Implement task T002 from specs/007-inventory-export/tasks.md

# Continue...
```

**Pro Tip**: Group related tasks:
```
@dev Implement tasks T001-T003 (foundation phase) from specs/007-inventory-export/tasks.md
```

**Why**: Incremental implementation with reviews prevents rework

---

## Complete Example: Small Feature

Here's a condensed real-world example:

```
USER: I need to add "Export to PDF" to transaction viewer

[Step 1: Create folder]
mkdir specs/008-export-pdf

[Step 2: Initial spec]
@pm Create initial spec for PDF export feature in specs/008-export-pdf/spec.md

[Step 3-4: Skip Speckit, use BMAD directly for small feature]

[Step 5: Architecture]
@architect Design architecture referencing specs/006-print-and-export (reuse print-to-PDF)

[Step 6: Data model]
@architect The export uses existing Model_Print_Job, document in data-model.md

[Step 7: Tasks]
@sm Create task breakdown - should be ~5 tasks to add PDF export button and wire to existing Helper_PrintManager

[Step 8: Validate]
@po Quick validation - spec simple, should be quick

[Step 9-10: Implement]
@dev Implement T001-T005 from specs/008-export-pdf/tasks.md
@qa *review After implementation
```

**Total Time:** ~2 hours for small feature using BMAD+Speckit

---

## When To Skip Speckit Steps

**For simpler features**, you can streamline:

**Skip Speckit Planning (Step 3)** if:
- Feature is straightforward
- No research needed
- Data model is simple

**Use BMAD Directly** for Steps 2-6:
```
@pm Create spec.md
@architect Create plan.md with architecture
@architect Create data-model.md
@sm Create tasks.md
@po Validate alignment
```

**Why**: Don't over-engineer simple features with heavy process

---

## Common Pitfalls

### ❌ **Pitfall 1: Over-Specifying Simple Features**

**Problem**: Run full Speckit workflow for 2-hour feature  
**Result**: Planning takes longer than implementation  
**Fix**: Use [Pure BMAD](../BMAD-Only/02-Create-Feature-Pure-BMAD.md) for simple features

### ❌ **Pitfall 2: Skipping Architecture**

**Problem**: Jump from spec to tasks without architecture  
**Result**: Tasks don't align with MTM patterns  
**Fix**: Always have @architect review, even for small features

### ❌ **Pitfall 3: Incomplete Task Breakdown**

**Problem**: Tasks too vague or missing steps  
**Result**: @dev can't implement autonomously  
**Fix**: Use @sm to create detailed tasks with acceptance criteria

---

## Next Steps

**Practice This Workflow:**
- [ ] Pick a medium-complexity feature
- [ ] Create spec folder
- [ ] Use @pm, @architect, @sm to generate docs
- [ ] Validate with @po
- [ ] Begin implementation with @dev

**Level Up:**
- [ ] Try [Implement Existing Spec](./02-Implement-Existing-Spec.md)
- [ ] Learn [Best Practices](./03-Speckit-BMAD-Best-Practices.md)

---

## Key Takeaways

✅ Speckit + BMAD provides structured planning for complex features  
✅ BMAD agents fill in spec sections (PRD, architecture, tasks)  
✅ @po validation ensures spec-architecture-tasks alignment  
✅ Can streamline process for simpler features  
✅ Specification documents guide implementation and code review

---

**Last Updated:** November 11, 2025  
**Estimated Read Time:** 15 minutes  
**Next:** [Implement Existing Spec](./02-Implement-Existing-Spec.md)
