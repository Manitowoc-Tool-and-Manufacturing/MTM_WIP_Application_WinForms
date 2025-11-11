# Implement Existing Spec with BMAD

**Time to Complete:** 10 minutes  
**What You'll Learn:** Continue partial specs, implement tasks incrementally with @dev  
**Use Case:** Resume work on specs like \06-print-and-export\, implement existing task lists

---

## When To Use This Workflow

✅ **Good For:**
- Resuming partially implemented features
- Implementing well-defined task lists
- Working from existing specifications
- Team handoffs (continuing someone else's work)

❌ **Use Other Workflows If:**
- No spec exists yet → [Create New Spec](./01-Create-New-Spec-With-Speckit.md)
- Quick feature without formal spec → [Pure BMAD](../BMAD-Only/02-Create-Feature-Pure-BMAD.md)

---

## Prerequisites

- [ ] Spec exists in \/specs/{feature-name}/\
- [ ] \spec.md\ and \	asks.md\ files present
- [ ] Understanding of feature requirements
- [ ] Git working tree clean (commit other work)

---

## Workflow Overview

```
Load Spec → Review Tasks → Implement → QA Review → Test → Commit
    ↓           ↓             ↓           ↓          ↓       ↓
  @dev        @dev          @dev         @qa       Manual  Git
```

**Time Estimate:** Varies by task complexity (1-4 hours per task)

---

## Step 1: Understand the Spec

### Task Checklist
- [ ] Read \spec.md\ to understand feature goals
- [ ] Review \plan.md\ for architecture
- [ ] Check \	asks.md\ for current progress

### Commands

```
@dev Review specs/006-print-and-export/ and summarize:
- What feature we're building
- What's already implemented (completed tasks)
- What's next to implement
- Any blockers or dependencies
```

**What @dev Will Provide:**

```markdown
## Feature Summary
Advanced print preview and Excel export system

## Completed Tasks (✅)
- T001-T035: Core infrastructure complete
- T036-T042: Print preview UI complete
- T043-T050: Excel export implemented

## Next Task: T051
Implement column selection UI in PrintForm

## Dependencies
Requires PrintForm.cs (created in T040)

## Blockers
None identified
```

### Validation Checklist
- [ ] Feature goal is clear
- [ ] Current progress understood
- [ ] Next task dependencies satisfied
- [ ] No blockers preventing progress

**Why**: Understanding context prevents implementing wrong features

---

## Step 2: Identify Next Task

### Task Checklist
- [ ] Open \	asks.md\
- [ ] Find first uncompleted task
- [ ] Verify dependencies satisfied

### Commands

```
@dev What is the next incomplete task in specs/006-print-and-export/tasks.md? Show me the full task details including file path, description, and acceptance criteria.
```

**Example Task:**

```markdown
- [ ] **T051** [US2] - Implement column selection CheckedListBox
  **File**: \Forms/Shared/PrintForm.cs\
  **Description**: Add CheckedListBox for column selection, populate from Model_Print_Job.VisibleColumns, wire up SelectAll/DeselectAll buttons, update preview on selection change
  **Reference**: \.github/instructions/ui-scaling-consistency.instructions.md\ - WinForms layout patterns
  **Acceptance**: CheckedListBox populated, selection changes update preview, follows naming conventions
```

### Validation Checklist
- [ ] Task dependencies completed (check previous tasks)
- [ ] Files mentioned in task exist
- [ ] Acceptance criteria are clear
- [ ] Instruction files referenced are accessible

**Why**: Implementing tasks out of order causes dependency issues

---

## Step 3: Implement the Task

### Task Checklist
- [ ] Ask @dev to implement the task
- [ ] Review generated code
- [ ] Request modifications if needed

### Commands

```
@dev Implement task T051 from specs/006-print-and-export/tasks.md. Follow the instruction files referenced and ensure MTM constitution compliance.
```

**What @dev Will Do:**

1. **Read Task Details**
   - Understands file, description, acceptance criteria
   - Loads referenced instruction files

2. **Generate Implementation**
   - Creates/modifies specified file
   - Follows MTM patterns (regions, naming, theme integration)
   - Adds XML documentation
   - Includes error handling

3. **Validate Against Acceptance**
   - Checks each acceptance criterion
   - Ensures constitution compliance
   - Reviews instruction file requirements

### Monitor Implementation
- [ ] @dev follows naming conventions
- [ ] @dev applies theme integration
- [ ] @dev uses correct region organization
- [ ] @dev adds error handling via Service_ErrorHandler
- [ ] @dev includes XML documentation

**Pro Tip**: Review incrementally:
```
@dev Show me the CheckedListBox creation first before wiring up events
```

**Why**: Incremental review catches issues early

---

## Step 4: Mark Task Complete

### Task Checklist
- [ ] Update \	asks.md\ with completion marker
- [ ] Add completion date and summary

### Commands

```
@dev Mark task T051 as complete in specs/006-print-and-export/tasks.md with today's date and a brief summary of what was implemented.
```

**Updated Task Format:**

```markdown
- [x] **T051** [US2] - Implement column selection CheckedListBox
  **File**: \Forms/Shared/PrintForm.cs\
  **Description**: Add CheckedListBox for column selection...
  **Reference**: \.github/instructions/ui-scaling-consistency.instructions.md\
  **Acceptance**: CheckedListBox populated, selection changes update preview
  **Completed**: 2025-11-11 - Created PrintForm_CheckedListBox_Columns with theme integration, wired SelectAll/DeselectAll buttons, preview updates on selection change
```

**Why**: Progress tracking enables team visibility and handoffs

---

## Step 5: QA Review

### Task Checklist
- [ ] Request @qa review of implementation
- [ ] Address any concerns
- [ ] Get approval before proceeding

### Commands

```
@qa *review Review task T051 implementation in Forms/Shared/PrintForm.cs. Check:
- MTM constitution compliance (naming, regions, theme integration)
- Instruction file compliance
- Code quality and null safety
- Acceptance criteria satisfaction
```

**What @qa Will Validate:**

```markdown
## QA Review: Task T051

### Constitution Compliance ✅
- Naming: \PrintForm_CheckedListBox_Columns\ follows convention
- Region: Placed in \#region UI Controls\ correctly
- Theme: Uses \Core_Themes.ApplyDpiScaling\ in constructor

### Instruction File Compliance ✅
- Follows ui-scaling-consistency.instructions.md layout patterns
- AutoSize and MinimumSize properly configured

### Code Quality ✅
- Null safety: Event handler checks for null
- Error handling: Try-catch with Service_ErrorHandler
- XML documentation: Complete

### Acceptance Criteria ✅
- CheckedListBox populated: ✅
- Selection changes update preview: ✅
- Naming conventions: ✅

### Rating: PASS
```

### Common @qa Concerns

**Concern**: Missing error handling in event handler  
**Action**: @dev adds try-catch with Service_ErrorHandler

**Concern**: CheckedListBox not using theme colors  
**Action**: @dev applies theme tokens

**Concern**: No XML documentation  
**Action**: @dev adds <summary> tags

**Why**: QA review catches violations before merge

---

## Step 6: Manual Testing

### Task Checklist
- [ ] Build application
- [ ] Test implemented functionality
- [ ] Verify acceptance criteria

### Manual Test Steps

```powershell
# Build
dotnet build MTM_WIP_Application_Winforms.csproj

# Run
dotnet run
```

**Test Scenario (for T051):**
1. Open Transaction Viewer
2. Click Print button
3. Verify CheckedListBox displays all columns
4. Click \"Select All\" → all columns checked
5. Click \"Deselect All\" → all columns unchecked
6. Check/uncheck individual columns
7. Verify print preview updates immediately
8. Verify theme colors applied correctly

### Validation Checklist
- [ ] Application builds with zero errors
- [ ] Feature behaves per acceptance criteria
- [ ] No runtime errors or exceptions
- [ ] Theme integration renders correctly
- [ ] Performance acceptable (UI responsive)

**Why**: Manual testing catches runtime issues static analysis misses

---

## Step 7: Commit Changes

### Task Checklist
- [ ] Stage changes
- [ ] Write descriptive commit message
- [ ] Push to feature branch

### Commands

```powershell
# Stage files
git add Forms/Shared/PrintForm.cs
git add specs/006-print-and-export/tasks.md

# Commit
git commit -m \"feat(print): Implement column selection UI (T051)

- Added PrintForm_CheckedListBox_Columns with theme integration
- Wired SelectAll/DeselectAll buttons
- Preview updates on selection change
- Follows MTM constitution (naming, regions, theme)

Acceptance criteria:
✅ CheckedListBox populated from Model_Print_Job
✅ Selection changes update preview
✅ Naming conventions followed

Task: T051 from specs/006-print-and-export/tasks.md\"

# Push
git push origin 006-print-and-export
```

**Why**: Clear commits enable code review and future reference

---

## Step 8: Continue to Next Task

### Task Checklist
- [ ] Identify next incomplete task
- [ ] Repeat Steps 2-7
- [ ] Track progress through task list

### Commands

```
@dev What is the next incomplete task in specs/006-print-and-export/tasks.md?
```

**Iteration Pattern:**

```
Task T051 → Implement → QA Review → Test → Commit ✅
Task T052 → Implement → QA Review → Test → Commit ✅
Task T053 → Implement → QA Review → Test → Commit ✅
...
```

**Pro Tip**: Group related tasks:
```
@dev Implement tasks T052-T055 (all column selection related tasks) from specs/006-print-and-export/tasks.md
```

**Why**: Batching related tasks maintains context and reduces context switching

---

## Advanced: Handling Blocked Tasks

### If Dependencies Not Met

**Scenario**: Task T060 depends on T055, but T055 is blocked

**Options:**

1. **Skip and come back:**
   ```
   @dev Skip task T060 for now. What's the next non-blocked task?
   ```

2. **Implement dependency first:**
   ```
   @dev Task T060 is blocked by T055. Let's implement T055 first.
   ```

3. **Document blocker:**
   ```
   @dev Add a note to task T060 in tasks.md documenting the blocker: \"Blocked by T055 - requires Model_Print_Settings to be completed\"
   ```

**Why**: Blocker tracking prevents wasted effort

---

## Working with Partially Completed Tasks

### If Previous Developer Started Work

**Scenario**: Task marked partially complete

```markdown
- [ ] **T042** - Implement Excel export helper
  **Completed**: 2025-11-10 - Created Helper_ExcelExporter.cs base class. Still need: error handling and progress reporting
```

**Commands:**

```
@dev Continue task T042 from specs/006-print-and-export/tasks.md. Previous developer created the base class but error handling and progress reporting still needed. Review the existing code first, then add the missing pieces.
```

**Why**: Builds on existing work rather than starting over

---

## Checkpoint Pattern

### At Natural Stopping Points

After completing a logical group of tasks:

```
@dev Generate a checkpoint summary for specs/006-print-and-export/:
- Tasks completed since last checkpoint
- Current feature state
- What's working
- What's left to implement
- Any issues discovered
```

**Example Checkpoint Summary:**

```markdown
## Checkpoint: Column Selection Complete (2025-11-11)

### Completed Tasks
- T051-T055: Column selection UI implemented
- T056-T058: Column reordering functionality

### Current State
- Column selection working correctly
- Preview updates live
- Select All / Deselect All functional
- Column reordering with Up/Down buttons complete

### What's Working
✅ All column selection acceptance criteria met
✅ Theme integration correct
✅ Manual testing passed

### Remaining Work
- T059-T065: Settings persistence (next up)
- T066-T070: Export functionality
- T071-T075: Polish and error handling

### Issues
None - implementation on track
```

**Why**: Checkpoints provide progress visibility for team and stakeholders

---

## Common Pitfalls

### ❌ **Pitfall 1: Implementing Tasks Out Of Order**

**Problem**: Implement T060 before T055 (dependency)  
**Result**: Code doesn't compile, requires rework  
**Fix**: Always check task dependencies first

### ❌ **Pitfall 2: Not Reading Spec**

**Problem**: Implement task without understanding feature context  
**Result**: Solution doesn't align with user stories  
**Fix**: Always review spec.md before implementing

### ❌ **Pitfall 3: Skipping QA Review**

**Problem**: Implement multiple tasks without @qa reviews  
**Result**: Constitution violations accumulate  
**Fix**: Review after each task or after related task groups

### ❌ **Pitfall 4: No Manual Testing**

**Problem**: Trust @dev implementation without validation  
**Result**: Runtime errors in production  
**Fix**: Always build and test manually

---

## Real-World Example

**Scenario**: Continue \specs/006-print-and-export\ from task T051

```
# Step 1: Understand spec
@dev Summarize specs/006-print-and-export/

# Step 2: Find next task
@dev Next incomplete task in specs/006-print-and-export/tasks.md?
> Task T051: Column selection UI

# Step 3: Implement
@dev Implement task T051 from specs/006-print-and-export/tasks.md

# Step 4: Mark complete
@dev Mark T051 complete with today's date

# Step 5: QA Review
@qa *review Forms/Shared/PrintForm.cs for task T051

# Step 6: Manual Test
dotnet build && dotnet run
[Test column selection manually]

# Step 7: Commit
git add Forms/Shared/PrintForm.cs specs/006-print-and-export/tasks.md
git commit -m \"feat(print): Implement column selection UI (T051)\"

# Step 8: Continue
@dev Next incomplete task?
> Task T052: Wire SelectAll button
[Repeat cycle...]
```

**Total Time:** ~1 hour per task on average

---

## Next Steps

**Practice This Workflow:**
- [ ] Pick an existing spec with incomplete tasks
- [ ] Implement 3-5 tasks following this workflow
- [ ] Track your time per task

**Level Up:**
- [ ] Learn [Speckit Best Practices](./03-Speckit-BMAD-Best-Practices.md)
- [ ] Try [Copilot Integration](../Copilot-Integration/01-Copilot-BMAD-Together.md)

---

## Key Takeaways

✅ Always review spec.md before implementing tasks  
✅ Implement tasks in order (respect dependencies)  
✅ Mark tasks complete with date and summary  
✅ QA review after each task or task group  
✅ Manual testing validates implementation  
✅ Clear commit messages reference task IDs

---

**Last Updated:** November 11, 2025  
**Estimated Read Time:** 10 minutes  
**Next:** [Speckit BMAD Best Practices](./03-Speckit-BMAD-Best-Practices.md)
