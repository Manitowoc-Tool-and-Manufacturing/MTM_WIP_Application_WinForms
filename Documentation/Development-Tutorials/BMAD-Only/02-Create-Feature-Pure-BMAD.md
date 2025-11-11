# Create Feature with Pure BMAD

**Time to Complete:** 10 minutes  
**What You'll Learn:** End-to-end feature development using only BMAD agents (no Speckit)  
**Use Case:** Quick features, prototypes, solo development

---

## When To Use This Workflow

✅ **Good For:**
- Small to medium features (< 2 weeks)
- Solo development
- Prototypes or experiments
- Learning BMAD
- Features without formal doc requirements

❌ **Use Speckit Instead If:**
- Complex multi-week features
- Team collaboration required
- Formal documentation needed
- Multiple stakeholders involved

---

## Prerequisites

- [ ] VS Code open in MTM project root
- [ ] Familiarity with BMAD agents ([Quick Start](./01-BMAD-Quick-Start.md))
- [ ] Feature idea or requirements in mind

---

## Workflow Overview

```
Idea → PRD → Architecture → Implementation → QA Review → Done
 ↓       ↓        ↓             ↓              ↓          ↓
@pm   @architect @dev        @dev           @qa      Commit
```

**Time Estimate:** 2-4 hours for simple feature

---

## Step 1: Define Requirements with @pm

### Task Checklist
- [ ] Describe feature idea to @pm
- [ ] Review generated PRD
- [ ] Refine until requirements are clear

### Commands

```
@pm I need to add a "Clear Filters" button to the Transaction Viewer that resets all search filters to defaults. Create a lightweight PRD.
```

**What @pm Will Create:**
- User stories
- Acceptance criteria
- Success metrics
- Out of scope items

### Example Output Location
\docs/prd-clear-filters.md\ (or @pm may provide inline)

**Validation Checklist:**
- [ ] User story clearly describes WHO/WHAT/WHY
- [ ] Acceptance criteria are testable
- [ ] Technical constraints mentioned (WinForms, theme integration)

**Why**: Clear requirements prevent rework during implementation

---

## Step 2: Design Architecture with @architect

### Task Checklist
- [ ] Share PRD with @architect
- [ ] Review architecture proposal
- [ ] Confirm it follows MTM patterns

### Commands

```
@architect Based on this PRD, design the architecture for adding a Clear Filters button to the Transaction Viewer. Reference Documentation/BROWNFIELD_ARCHITECTURE.md and follow MTM constitution rules.
```

**What @architect Will Provide:**
- Component modifications needed
- Method signatures
- Event wiring approach
- Theme integration requirements
- Testing strategy

### Validation Checklist
- [ ] Uses existing MTM patterns (no new frameworks)
- [ ] Follows WinForms architecture standards
- [ ] Integrates with \Core_Themes\
- [ ] Error handling via \Service_ErrorHandler\
- [ ] Event naming follows conventions

**Why**: Architecture prevents technical debt and rework

---

## Step 3: Implement with @dev

### Task Checklist
- [ ] Share architecture with @dev
- [ ] Let @dev implement incrementally
- [ ] Review each change before proceeding

### Commands

```
@dev Implement the Clear Filters button based on this architecture. Start with the button UI in TransactionSearchControl, then implement the reset logic.
```

**Implementation Steps @dev Will Follow:**

1. **Add Button to Designer**
   - Follows naming: \TransactionSearchControl_Button_ClearFilters\
   - Applies theme via \Core_Themes\
   - Sets proper AutoSize and docking

2. **Wire Up Event Handler**
   - Creates \Button_ClearFilters_Click\ method
   - Places in \#region Button Clicks\

3. **Implement Reset Logic**
   - Resets all ComboBox selections
   - Clears DateTimePickers
   - Resets to default quick filter

4. **Add Error Handling**
   - Wraps in try-catch
   - Uses \Service_ErrorHandler\ for errors
   - Logs via \LoggingUtility\

### Monitor Progress
- [ ] @dev creates button with correct naming
- [ ] @dev applies theme integration
- [ ] @dev follows region organization
- [ ] @dev adds XML documentation
- [ ] @dev implements error handling

**Pro Tip**: Ask @dev to show you the code incrementally:
```
@dev Show me the button creation first before implementing the logic
```

**Why**: Incremental implementation allows course correction early

---

## Step 4: Add Testing Guidance with @qa

### Task Checklist
- [ ] Ask @qa for test strategy
- [ ] Review risk assessment
- [ ] Create manual test scenarios

### Commands

```
@qa *design Create a test strategy for the Clear Filters button. Focus on manual validation scenarios.
```

**What @qa Will Provide:**
- Test scenarios (happy path, edge cases)
- Manual test steps
- Risk assessment
- Validation checklist

### Example Test Scenarios @qa Might Create

**Scenario 1: Happy Path**
- Given: User has selected Part ABC, Date range 11/1-11/10, User filter JohnK
- When: User clicks Clear Filters
- Then: All filters reset to defaults, grid shows all transactions

**Scenario 2: Empty State**
- Given: No filters applied
- When: User clicks Clear Filters
- Then: Button does nothing (already at defaults)

**Scenario 3: Theme Integration**
- Given: Application in Dark theme
- When: Button rendered
- Then: Button uses theme colors correctly

### Validation Checklist
- [ ] Test scenarios cover acceptance criteria
- [ ] Edge cases identified
- [ ] Manual test steps documented

**Why**: Test strategy catches issues before users find them

---

## Step 5: Quality Review with @qa

### Task Checklist
- [ ] Ask @qa to review implementation
- [ ] Address any concerns raised
- [ ] Get final approval

### Commands

```
@qa *review Review the Clear Filters button implementation in Forms/Transactions/TransactionSearchControl.cs. Check for:
- MTM constitution compliance
- Theme integration
- Error handling
- Code organization
```

**What @qa Will Check:**
- Region organization
- Naming conventions
- Theme integration (\Core_Themes.ApplyDpiScaling\)
- Error handling (\Service_ErrorHandler\)
- XML documentation
- Null safety

### Common Issues @qa Finds

**Issue**: Button doesn't use theme colors  
**Fix**: Apply \Core_Themes\ in constructor

**Issue**: No error handling in event handler  
**Fix**: Wrap logic in try-catch with \Service_ErrorHandler\

**Issue**: Hardcoded size values  
**Fix**: Use \AutoSize\ and \MinimumSize\

### Validation Checklist
- [ ] @qa gives PASS or CONCERNS rating
- [ ] All concerns addressed
- [ ] No constitution violations
- [ ] Code ready for commit

**Why**: QA review catches violations before code review

---

## Step 6: Manual Validation

### Task Checklist
- [ ] Build the application
- [ ] Execute manual test scenarios
- [ ] Verify all acceptance criteria

### Manual Test Execution

```powershell
# Build
dotnet build MTM_WIP_Application_Winforms.csproj

# Run application
dotnet run

# Execute test scenarios from Step 4
```

### Acceptance Criteria Validation
- [ ] Clear Filters button visible in Transaction Search panel
- [ ] Button uses theme colors correctly
- [ ] Clicking button resets all filters to defaults
- [ ] Grid refreshes after filters cleared
- [ ] No errors in console or log files

**Why**: Manual validation ensures real-world functionality

---

## Step 7: Commit Changes

### Task Checklist
- [ ] Create feature branch
- [ ] Commit with descriptive message
- [ ] Push to remote

### Commands

```powershell
# Create branch
git checkout -b feature/clear-filters-button

# Stage changes
git add .

# Commit
git commit -m "feat: Add Clear Filters button to Transaction Viewer

- Added TransactionSearchControl_Button_ClearFilters with theme integration
- Implemented reset logic for all filter controls
- Added error handling via Service_ErrorHandler
- Includes manual validation scenarios

Acceptance criteria:
- Resets Part, User, Location ComboBoxes to default
- Resets Date range to Today quick filter
- Refreshes grid after filter clear"

# Push
git push origin feature/clear-filters-button
```

**Why**: Clear commit messages enable code review and future reference

---

## Complete Example Session

Here's a real end-to-end conversation flow:

```
USER: @pm I need to add keyboard shortcut Ctrl+R to refresh the transaction grid. Create a quick PRD.

@PM: [Creates PRD with user story, acceptance criteria]

USER: @architect Design this following MTM patterns. Reference Forms/Transactions/Transactions.cs.

@ARCHITECT: [Provides architecture using ProcessCmdKey override pattern]

USER: @dev Implement this architecture. Start with the ProcessCmdKey override.

@DEV: [Implements code with proper regions, error handling, logging]

USER: @qa *review Review the implementation for constitution compliance.

@QA: PASS - Implementation follows MTM patterns. Minor suggestion: add XML documentation for ProcessCmdKey override.

USER: @dev Add the XML documentation @qa requested.

@DEV: [Adds documentation]

USER: Looks good, committing!
```

**Total Time:** ~30 minutes for simple feature

---

## Common Pitfalls

### ❌ **Pitfall 1: Skipping Architecture**

**Problem**: Jump straight from idea to @dev implementation  
**Result**: Violates MTM patterns, requires rework  
**Fix**: Always get @architect guidance first

### ❌ **Pitfall 2: Vague Requirements**

**Problem**: Tell @pm "I need a button"  
**Result**: Unclear acceptance criteria, scope creep  
**Fix**: Describe WHO, WHAT, WHY clearly

### ❌ **Pitfall 3: Ignoring @qa Concerns**

**Problem**: Skip @qa review to save time  
**Result**: Constitution violations caught in code review  
**Fix**: Always run @qa review before committing

### ❌ **Pitfall 4: No Manual Testing**

**Problem**: Trust agent code without validation  
**Result**: Runtime errors in production  
**Fix**: Always build and test manually

---

## When This Workflow Breaks Down

**Switch to Speckit if:**
- Feature grows beyond initial scope
- Multiple developers need coordination
- Stakeholders request formal documentation
- Feature needs long-term maintenance plan

**Transition Path:**
```
@pm Convert this PRD to Speckit format
@architect Create formal architecture document
```

Then follow [Create New Spec With Speckit](../BMAD-Speckit/01-Create-New-Spec-With-Speckit.md)

---

## Next Steps

**Practice This Workflow:**
- [ ] Pick a small enhancement from your backlog
- [ ] Follow steps 1-7
- [ ] Time yourself (target: < 1 hour)

**Level Up:**
- [ ] Try [Refactor With BMAD](./03-Refactor-With-BMAD.md)
- [ ] Learn [BMAD + Speckit](../BMAD-Speckit/01-Create-New-Spec-With-Speckit.md)
- [ ] Explore [Copilot Integration](../Copilot-Integration/01-Copilot-BMAD-Together.md)

---

## Key Takeaways

✅ Pure BMAD is fast for small features (< 2 hours)  
✅ Always go PRD → Architecture → Implementation → QA  
✅ Agents enforce MTM constitution automatically  
✅ Manual validation is still required  
✅ Commit with clear messages for code review

---

**Last Updated:** November 11, 2025  
**Estimated Read Time:** 10 minutes  
**Next:** [Refactor With BMAD](./03-Refactor-With-BMAD.md)
