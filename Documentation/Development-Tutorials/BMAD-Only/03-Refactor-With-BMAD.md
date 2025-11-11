# Refactor Code with BMAD

**Time to Complete:** 10 minutes  
**What You'll Learn:** Brownfield refactoring workflow, pre-refactor reports, atomic commits  
**Use Case:** Refactoring existing code, creating custom WinForms controls

---

## When To Use This Workflow

✅ **Good For:**
- Refactoring large files (> 500 lines)
- Improving code organization
- Converting legacy patterns to MTM standards
- Creating custom UserControls from scratch
- Splitting monolithic code into modules

❌ **Not For:**
- Greenfield development (use [Create Feature](./02-Create-Feature-Pure-BMAD.md))
- Bug fixes (direct fix is faster)

---

## Prerequisites

- [ ] VS Code open in MTM project root
- [ ] Target file identified for refactoring
- [ ] File compiles successfully (start from working state)
- [ ] Git working tree is clean (commit other work first)

---

## Refactoring Workflow Overview

```
Identify File → Pre-Refactor Report → Approval → Refactor → Validate → Commit
      ↓                ↓                  ↓          ↓         ↓         ↓
   @dev            @dev Review          User      @dev     Build    Atomic
                                                                   Commits
```

**Time Estimate:** 1-3 hours depending on file size

---

## Step 1: Generate Pre-Refactor Report

### Task Checklist
- [ ] Identify file needing refactoring
- [ ] Request pre-refactor analysis from @dev
- [ ] Review report before approving changes

### Commands

```
@dev Generate a pre-refactor report for Forms/Transactions/Transactions.cs. Analyze:
- Current region organization
- MTM constitution compliance
- Refactoring opportunities
- Estimated complexity
```

**What @dev Will Analyze:**

1. **Current Structure**
   - Region organization vs MTM standard
   - Method count and placement
   - Code duplication
   - Naming conventions

2. **Constitution Compliance**
   - DAO pattern usage
   - Error handling (Service_ErrorHandler)
   - Theme integration (Core_Themes)
   - Async patterns
   - XML documentation coverage

3. **Refactoring Opportunities**
   - Region reorganization needed
   - Methods to extract
   - Patterns to apply
   - Dependencies to resolve

4. **Risk Assessment**
   - Breaking change likelihood
   - Dependent files affected
   - Testing recommendations

### Example Report Section

```markdown
## Current State Analysis

**File**: Forms/Transactions/Transactions.cs  
**Lines**: 1,842  
**Regions**: 8 (non-standard order)

### Issues Found:
1. ❌ Regions not in standard MTM order
2. ❌ Missing #region Constructors
3. ❌ Button clicks mixed with helper methods
4. ⚠️ Some hardcoded colors (no theme tokens)
5. ✅ Uses Model_Dao_Result<T> correctly
6. ✅ Async patterns properly applied

### Recommended Actions:
1. Reorganize regions per constitution standard order
2. Extract 3 helper methods to separate region
3. Apply theme tokens to hardcoded colors
4. Add XML docs to 12 public methods
```

### Validation Checklist
- [ ] Report clearly identifies issues
- [ ] Recommended actions are specific
- [ ] Risk assessment included
- [ ] Estimated effort seems reasonable

**Why**: Pre-refactor report prevents surprises and documents baseline

---

## Step 2: Review and Approve Refactoring Plan

### Task Checklist
- [ ] Read pre-refactor report carefully
- [ ] Understand proposed changes
- [ ] Explicitly approve before proceeding

### Decision Points

**Ask Yourself:**
- Does the refactoring align with current priorities?
- Can I afford 1-3 hours for this refactoring now?
- Are there dependent files that will be affected?
- Should I create a feature branch first?

### Approval Command

```
@dev Approved. Create feature branch refactor/transactions-form/20251111 and proceed with the refactoring plan using atomic commits by category.
```

**Why**: Explicit approval prevents @dev from making unwanted changes

---

## Step 3: Execute Refactoring with Atomic Commits

### Task Checklist
- [ ] @dev creates feature branch
- [ ] @dev applies changes in logical groups
- [ ] Each commit is buildable and testable
- [ ] Review each commit before proceeding

### Refactoring Categories (Atomic Commits)

**Commit 1: Region Reorganization**
```
refactor: Reorganize Transactions.cs regions per MTM standard

- Moved methods to correct regions
- Added missing #region Constructors
- Sorted methods by access level (public → private)
```

**Commit 2: Theme Integration**
```
refactor: Apply theme tokens to hardcoded colors in Transactions.cs

- Replaced Color.Blue with colors.PrimaryColor ?? SystemColors.Highlight
- Replaced Color.FromArgb() with theme tokens
- Added // ACCEPTABLE: comments for brand colors
```

**Commit 3: Extract Helper Methods**
```
refactor: Extract helper methods in Transactions.cs

- Created #region Helpers
- Moved ValidateFilters() to helpers
- Moved BuildQueryParameters() to helpers
- Moved FormatDateRange() to helpers
```

**Commit 4: Add XML Documentation**
```
docs: Add XML documentation to Transactions.cs public methods

- Added <summary> tags to 12 public methods
- Added <param> tags for all parameters
- Added <returns> tags for non-void methods
```

### Monitor Progress

After each atomic commit:
- [ ] Verify build succeeds: \dotnet build\
- [ ] Test basic functionality manually
- [ ] Review diff before approving next commit

**Pro Tip**: Ask @dev to pause between commits:
```
@dev Show me the region reorganization diff before proceeding to theme integration.
```

**Why**: Atomic commits enable surgical rollback if issues arise

---

## Step 4: Validate with @qa

### Task Checklist
- [ ] Request @qa review of refactored code
- [ ] Address any concerns
- [ ] Get final approval

### Commands

```
@qa *review Review the refactored Forms/Transactions/Transactions.cs. Check for:
- Constitution compliance
- Region organization
- Theme integration
- Breaking changes
```

**What @qa Will Validate:**

1. **Constitution Compliance**
   - Regions in correct order
   - Naming conventions followed
   - Error handling patterns correct
   - Theme integration complete

2. **Code Quality**
   - No code duplication
   - XML documentation complete
   - Async patterns correct
   - Null safety maintained

3. **Breaking Changes**
   - Public API unchanged
   - Event signatures intact
   - Dependencies still satisfied

### Common @qa Findings

**Finding**: Some hardcoded colors remain  
**Action**: Apply theme tokens or add \// ACCEPTABLE:\ comments

**Finding**: XML docs incomplete  
**Action**: Add missing <param> and <returns> tags

**Finding**: Method moved to wrong region  
**Action**: Move to correct region per constitution

### Validation Checklist
- [ ] @qa gives PASS rating
- [ ] No breaking changes introduced
- [ ] All constitution violations fixed
- [ ] Code ready for merge

**Why**: QA validation catches subtle issues before code review

---

## Step 5: Manual Validation

### Task Checklist
- [ ] Build application successfully
- [ ] Test refactored functionality
- [ ] Verify no regressions introduced

### Testing Strategy

**Smoke Test**: Basic functionality still works
```powershell
# Build
dotnet build MTM_WIP_Application_Winforms.csproj

# Run
dotnet run
```

**Functional Test**: Exercise refactored code paths
- Open refactored form
- Trigger button clicks in refactored region
- Verify data operations work
- Check theme rendering

**Regression Test**: Verify nothing broke
- Test related forms/controls
- Check error handling still works
- Verify database operations succeed

### Validation Checklist
- [ ] Application builds with zero errors
- [ ] Refactored form/control opens successfully
- [ ] All user interactions work as before
- [ ] No new errors in logs
- [ ] Theme colors render correctly

**Why**: Manual validation catches runtime issues that static analysis misses

---

## Step 6: Generate Post-Refactor Summary

### Task Checklist
- [ ] Request summary from @dev
- [ ] Review what changed
- [ ] Document any deviations from plan

### Commands

```
@dev Generate a post-refactor summary for Transactions.cs. Include:
- Changes made
- Constitution violations fixed
- Commits created
- Rollback instructions if needed
```

**Example Summary:**

```markdown
## Post-Refactor Summary

**File**: Forms/Transactions/Transactions.cs  
**Commits**: 4 atomic commits  
**Lines Changed**: ~200

### Changes Applied:
✅ Reorganized regions to MTM standard order  
✅ Applied theme tokens (12 hardcoded colors fixed)  
✅ Extracted 3 helper methods  
✅ Added XML documentation to 12 public methods

### Constitution Violations Fixed:
- Principle III: Region organization now compliant
- Principle VIII: XML documentation complete
- Principle IX: Theme integration complete

### Rollback Instructions:
If issues arise, cherry-pick revert commits in reverse order:
```ash
git revert <commit-4>
git revert <commit-3>
git revert <commit-2>
git revert <commit-1>
```

**Why**: Summary provides documentation for code review and future reference

---

## Step 7: Merge Feature Branch

### Task Checklist
- [ ] Push feature branch to remote
- [ ] Create pull request
- [ ] Document refactoring in PR description
- [ ] Merge after approval

### Commands

```powershell
# Push branch
git push origin refactor/transactions-form/20251111

# Create PR (via GitHub web UI or CLI)
# Include post-refactor summary in PR description
```

**PR Template:**

```markdown
## Refactoring: Transactions.cs

### Summary
Refactored Forms/Transactions/Transactions.cs to MTM constitution compliance.

### Changes
- Region reorganization per standard order
- Theme integration (replaced hardcoded colors)
- Extracted helper methods
- Added XML documentation

### Validation
- [x] Build succeeds
- [x] Manual testing passed
- [x] @qa review PASS
- [x] No breaking changes

### Constitution Compliance
- Principle III: Region organization ✅
- Principle VIII: XML documentation ✅
- Principle IX: Theme integration ✅

### Rollback
See post-refactor summary for rollback instructions if needed.
```

**Why**: Clear PR documentation enables efficient code review

---

## Creating Custom WinForms Controls

**Same workflow applies!** Just adjust Step 1:

```
@dev I need to create a custom UserControl for a manufacturing field input (label + textbox with theme integration). Generate a pre-creation report including:
- Recommended control structure
- Theme integration approach
- Event wiring pattern
- Testing strategy
```

**@dev will provide:**
- UserControl template with MTM patterns
- Designer code structure
- Theme integration in constructor
- Naming conventions
- Event patterns

**Then follow Steps 2-7** to implement the control with atomic commits.

---

## Common Pitfalls

### ❌ **Pitfall 1: Skipping Pre-Refactor Report**

**Problem**: Start refactoring without analysis  
**Result**: Unexpected complexity, scope creep  
**Fix**: Always generate report first

### ❌ **Pitfall 2: Large Monolithic Commits**

**Problem**: Commit all changes at once  
**Result**: Can't rollback specific changes  
**Fix**: Use atomic commits by category

### ❌ **Pitfall 3: No Manual Testing**

**Problem**: Trust refactoring without validation  
**Result**: Broken functionality in production  
**Fix**: Always build and test manually

### ❌ **Pitfall 4: Ignoring Dependencies**

**Problem**: Refactor file without checking dependents  
**Result**: Breaking changes in other files  
**Fix**: Review pre-refactor report's dependency analysis

---

## Advanced: Recursive Dependency Analysis

For complex refactorings affecting multiple files:

```
@dev Perform recursive dependency analysis for Forms/Transactions/Transactions.cs. Identify:
- Direct dependencies (forms/controls this file uses)
- Dependent files (other code that uses this file)
- Shared dependencies (common utilities)
- Risk areas (tightly coupled code)
```

**Why**: Prevents breaking changes that ripple through codebase

---

## Next Steps

**Practice This Workflow:**
- [ ] Identify a file with > 500 lines
- [ ] Generate pre-refactor report
- [ ] Execute refactoring with atomic commits
- [ ] Time yourself (target: < 2 hours)

**Level Up:**
- [ ] Refactor a file with dependencies
- [ ] Create a custom UserControl from scratch
- [ ] Try [BMAD + Speckit](../BMAD-Speckit/01-Create-New-Spec-With-Speckit.md) for complex features

---

## Key Takeaways

✅ Always generate pre-refactor report before changes  
✅ Use atomic commits by category (region, theme, docs, extraction)  
✅ Each commit must build and pass basic tests  
✅ @qa review catches constitution violations early  
✅ Manual validation is still required  
✅ Document refactoring in PR for code review

---

**Last Updated:** November 11, 2025  
**Estimated Read Time:** 10 minutes  
**Next:** [Agent Command Reference](./04-Agent-Command-Reference.md)
