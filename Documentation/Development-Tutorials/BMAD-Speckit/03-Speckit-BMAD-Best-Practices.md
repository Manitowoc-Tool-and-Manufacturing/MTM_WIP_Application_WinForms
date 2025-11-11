# BMAD + Speckit Best Practices

**Time to Complete:** 8 minutes  
**What You'll Learn:** Optimal workflows, when to use which approach, common patterns  
**Use Case:** Improve efficiency with BMAD + Speckit combination

---

## Decision Matrix: Which Workflow To Use?

| Scenario | Recommended Workflow | Why |
|----------|---------------------|-----|
| Quick feature (< 4 hours) | [Pure BMAD](../BMAD-Only/02-Create-Feature-Pure-BMAD.md) | Planning overhead exceeds implementation time |
| Medium feature (1-2 weeks) | [BMAD + Speckit](./01-Create-New-Spec-With-Speckit.md) | Structure prevents scope creep |
| Complex feature (> 2 weeks) | [BMAD + Speckit](./01-Create-New-Spec-With-Speckit.md) | Formal docs enable collaboration |
| Refactoring existing code | [Pure BMAD Refactor](../BMAD-Only/03-Refactor-With-BMAD.md) | No new features, just cleanup |
| Resume partial spec | [Implement Existing Spec](./02-Implement-Existing-Spec.md) | Spec already exists |
| Prototype/experiment | [Pure BMAD](../BMAD-Only/02-Create-Feature-Pure-BMAD.md) | Move fast, formalize later if needed |

---

## Workflow Optimization Patterns

### Pattern 1: Hybrid Approach

**For Medium Features:**

```
Phase 1: Quick Start (Pure BMAD)
@pm Create lightweight PRD
@architect Design architecture
@dev Implement MVP (core functionality)

Phase 2: Formalize (Add Speckit)
@pm Convert PRD to Speckit spec.md
@architect Enhance with plan.md
@sm Generate tasks.md for remaining work

Phase 3: Execute (BMAD + Speckit)
@dev Implement remaining tasks from tasks.md
@qa Review at checkpoints
```

**Why**: Gets you coding fast while building formal structure for team handoff

---

### Pattern 2: Spec-First Then Code

**For Complex Features:**

```
Phase 1: Full Spec Creation (BMAD + Speckit)
/speckit.plan [feature description]
@pm Enhance generated spec
@architect Create detailed architecture
@sm Generate comprehensive tasks
@po Validate alignment

Phase 2: Team Review
[Share spec with stakeholders]
[Get approval before coding]

Phase 3: Implementation (Focused)
@dev Implement task-by-task
@qa Review at story boundaries
[Commit frequently]
```

**Why**: Upfront planning prevents costly rework on large features

---

### Pattern 3: Documentation Debt Reduction

**For Existing Undocumented Code:**

```
Step 1: Document Current State
@architect *create-doc brownfield-architecture-tmpl.yaml
[Document actual implementation]

Step 2: Create Retrofit Spec
@pm Create spec.md describing what it currently does
@architect Merge architecture into plan.md

Step 3: Enhancement Planning
@pm Add user stories for improvements
@sm Generate tasks for enhancements

Step 4: Gradual Improvement
@dev Implement enhancement tasks
[Build on documented baseline]
```

**Why**: Converts legacy code to documented, improvable features

---

## Agent Selection Best Practices

### When To Use Each Agent

**@pm (Product Manager)**
- **Always Use For**: User-facing features, business requirements
- **Skip For**: Pure refactoring, technical debt, infrastructure
- **Time Investment**: 30-60 minutes for good PRD

**@architect (System Architect)**
- **Always Use For**: Any non-trivial implementation
- **Skip For**: Tiny UI tweaks (button color change)
- **Time Investment**: 15-45 minutes for architecture guidance

**@dev (Developer)**
- **Always Use For**: All code implementation
- **Skip For**: Nothing - @dev is your workhorse
- **Time Investment**: Varies widely (minutes to hours)

**@qa (QA Engineer)**
- **Always Use For**: Before merging, complex features, high-risk changes
- **Skip For**: Trivial changes you can manually validate in 30 seconds
- **Time Investment**: 10-30 minutes per review

**@sm (Scrum Master)**
- **Always Use For**: Breaking specs into tasks
- **Skip For**: Single-task features
- **Time Investment**: 20-40 minutes for task breakdown

**@po (Product Owner)**
- **Always Use For**: Multi-agent spec creation (validate alignment)
- **Skip For**: Pure BMAD workflows (fewer moving parts)
- **Time Investment**: 10-20 minutes for validation

---

## Task Granularity Guidelines

### Optimal Task Size

**Too Small** (❌ Anti-pattern):
```markdown
- [ ] **T001** - Create Model_Foo.cs file
- [ ] **T002** - Add property Bar to Model_Foo
- [ ] **T003** - Add property Baz to Model_Foo
- [ ] **T004** - Add XML docs to Model_Foo
```

**Too Large** (❌ Anti-pattern):
```markdown
- [ ] **T001** - Implement entire Excel export feature including UI, data layer, error handling, testing, and documentation
```

**Just Right** (✅ Correct):
```markdown
- [ ] **T001** - Create Model_Excel_ExportJob with properties
  **Description**: Create model with Data, FileName, VisibleColumns, IncludeHeaders, AutoOpen properties. Add XML documentation.
  **Acceptance**: Model created, all properties documented, follows naming conventions

- [ ] **T002** - Implement Helper_ExcelExporter.ExportAsync method
  **Description**: Create async method accepting Model_Excel_ExportJob, use ClosedXML to generate .xlsx file, return Model_Dao_Result<string> with file path
  **Acceptance**: Method compiles, handles errors via Model_Dao_Result, includes progress reporting
```

**Guidelines**:
- **1 task = 1-4 hours** of focused work
- **1 file per task** ideal (except small related files)
- **Clear acceptance criteria** (3-5 bullet points)
- **Testable independently** when possible

---

## Spec Quality Checklist

**Before Implementing**, ensure spec has:

### User Stories Section
- [ ] Each story has WHO/WHAT/WHY format
- [ ] Acceptance criteria are testable
- [ ] Priority assigned (P1/P2/P3)
- [ ] Success metrics defined

### Architecture Section (in plan.md)
- [ ] Components clearly identified (new vs modified)
- [ ] Integration points documented
- [ ] MTM patterns referenced (DAO, themes, error handling)
- [ ] Dependencies listed (NuGet, services, helpers)

### Data Model Section (in data-model.md)
- [ ] All entities/models documented
- [ ] Property descriptions and constraints clear
- [ ] Relationships between models explained

### Tasks Section (in tasks.md)
- [ ] Tasks organized by user story
- [ ] Dependencies between tasks clear
- [ ] Parallel opportunities marked [P]
- [ ] Instruction files referenced
- [ ] Acceptance criteria per task

**If ANY checkbox unchecked** → Use agents to fill gaps before implementing

---

## Checkpoint Patterns

### When To Pause For Review

**User Story Boundaries:**
```
Story 1 Complete → @qa *review → Manual Test → Commit → Checkpoint
Story 2 Start → ...
```

**Phase Transitions:**
```
Foundation Phase Complete → @po Validate → Team Review → Checkpoint
Story Implementation Phase Start → ...
```

**Before Major Refactoring:**
```
Current State Documented → @architect Pre-Refactor Report → Approval → Checkpoint
Refactoring Begins → ...
```

**Risk-Based:**
```
High-Risk Task Complete → @qa *risk → @qa *review → Extra Testing → Checkpoint
Next Task → ...
```

---

## Common Anti-Patterns

### ❌ **Anti-Pattern 1: Agent Hopping**

**Problem:**
```
@pm Create spec
@architect Actually, can you create the spec?
@dev No wait, you create it
```

**Fix:**
```
@pm Create spec (stick with PM for requirements)
@architect Design architecture (switch to architect for technical)
@dev Implement (switch to dev for coding)
```

**Why**: Each agent has specialized context; switching mid-task loses context

---

### ❌ **Anti-Pattern 2: Spec Drift**

**Problem:**
- Implement features not in spec
- Modify requirements mid-implementation
- Spec becomes stale documentation

**Fix:**
```
# If requirements change:
@pm Update spec.md with new user story
@architect Update plan.md with architecture changes
@sm Add new tasks to tasks.md
@po Validate alignment

# Then continue implementation
```

**Why**: Spec drift breaks traceability and team communication

---

### ❌ **Anti-Pattern 3: No Manual Validation**

**Problem:**
- Trust @dev implementation without testing
- Skip QA review to save time
- Discover bugs in production

**Fix:**
```
@dev Implement task
@qa *review (catch static issues)
[Manual test] (catch runtime issues)
[Commit]
```

**Why**: Agents are smart but can't catch all runtime issues

---

### ❌ **Anti-Pattern 4: Mega-Commits**

**Problem:**
```
git commit -m "Implemented entire feature - 50 files changed"
```

**Fix:**
```
# Task-based commits
git commit -m "feat: Add Model_Excel_ExportJob (T001)"
git commit -m "feat: Implement Helper_ExcelExporter (T002)"
git commit -m "feat: Wire export button to helper (T003)"
```

**Why**: Small commits enable surgical rollbacks and easier code review

---

## Efficiency Tips

### ✅ **Tip 1: Batch Related Tasks**

**Instead of:**
```
@dev Implement T001
[Review, test, commit]
@dev Implement T002
[Review, test, commit]
```

**Do:**
```
@dev Implement tasks T001-T003 (all model creation tasks)
[Review, test, commit as logical group]
```

**Saves**: Context switching time

---

### ✅ **Tip 2: Parallel Agent Usage**

**With Multiple Developers:**

```
Developer A: @dev Implement User Story 1 tasks
Developer B: @dev Implement User Story 2 tasks (independent)
Developer C: @qa Review completed stories
```

**Saves**: Calendar time (parallel execution)

---

### ✅ **Tip 3: Reuse Patterns**

**For Similar Features:**

```
@architect Review specs/006-print-and-export/plan.md and create similar architecture for PDF export, reusing Helper_PrintManager pattern
```

**Saves**: Architecture design time

---

### ✅ **Tip 4: Pre-Load Context**

**At Session Start:**

```
@dev Review:
- specs/006-print-and-export/spec.md
- specs/006-print-and-export/plan.md
- Documentation/BROWNFIELD_ARCHITECTURE.md

[Now @dev has full context for entire session]
```

**Saves**: Repeated file loading throughout session

---

## Transition Strategies

### From Pure BMAD to Speckit

**When feature grows:**

```
# Current state: Pure BMAD, no spec
# Future need: Team collaboration, formal docs

Step 1: Create spec folder
mkdir specs/XXX-feature-name

Step 2: Capture existing work
@pm Create spec.md from our conversation history
@architect Create plan.md documenting what we built
@dev Create quickstart.md explaining how to use it

Step 3: Plan remaining work
@sm Create tasks.md for remaining features
@po Validate docs align

Step 4: Continue with Speckit workflow
[Follow Implement Existing Spec workflow]
```

---

### From Speckit to Pure BMAD

**When overhead too high:**

```
# Current state: Full Speckit workflow
# Future need: Faster iteration

Option 1: Complete current spec, then switch
[Finish implementing all tasks]
[Next feature use Pure BMAD]

Option 2: Simplify docs
@pm Strip spec down to essential user stories
@sm Convert tasks to simple checklist
[Continue with lighter weight process]
```

---

## Measuring Success

### Track These Metrics

**Planning Efficiency:**
- Time from idea to approved spec
- Rework rate (spec changes after approval)
- Alignment issues caught by @po

**Implementation Efficiency:**
- Tasks completed per day
- QA review pass rate
- Manual test failure rate

**Quality Metrics:**
- Constitution violations per @qa review
- Production bugs per feature
- Code review feedback volume

**Process Metrics:**
- Spec completeness score (checklist %)
- Task granularity score (avg task hours)
- Commit cleanliness (files changed per commit)

---

## Next Steps

**Optimize Your Workflow:**
- [ ] Identify your most common feature type
- [ ] Choose primary workflow (Pure BMAD vs BMAD+Speckit)
- [ ] Create personal templates for common patterns
- [ ] Track metrics for 2-3 features

**Level Up:**
- [ ] Try [Copilot Integration](../Copilot-Integration/01-Copilot-BMAD-Together.md)
- [ ] Explore advanced @qa commands (*risk, *design, *trace)
- [ ] Build team workflow standards document

---

## Key Takeaways

✅ Choose workflow based on feature complexity and team needs  
✅ Each agent has specialized role - use appropriately  
✅ Task granularity: 1-4 hours per task ideal  
✅ Checkpoint at story boundaries and phase transitions  
✅ Manual validation still required despite smart agents  
✅ Transition between workflows as needs change

---

**Last Updated:** November 11, 2025  
**Estimated Read Time:** 8 minutes  
**See Also:** [BMAD Quick Start](../BMAD-Only/01-BMAD-Quick-Start.md)
