# Session Log Template

**Copy this template when documenting a new work session**

---

## Session: [Brief Session Title]

**Date**: Month Day, Year  
**Start Time**: HH:MM  
**End Time**: HH:MM  
**Duration**: X hours Y minutes  
**Participants**: [Names or roles]

---

## Session Goals

**Primary Goal**: [What was the main objective?]

**Secondary Goals**:
- Goal 1
- Goal 2
- Goal 3

**Success Criteria**:
- [ ] Criterion 1
- [ ] Criterion 2
- [ ] Criterion 3

---

## Context & Background

**Why this work?**: [Explain what led to this session]

**Previous Related Work**: [Link to prior sessions or related work]

**Dependencies**: [What had to be done first? What was blocking this?]

---

## Work Performed

### Phase 1: [Phase Name] (HH:MM - HH:MM, Duration: X min)

**Objective**: [What were you trying to accomplish in this phase?]

**Actions Taken**:
1. Action 1 - Brief description
2. Action 2 - Brief description
3. Action 3 - Brief description

**Results**:
- ✅ Success 1
- ✅ Success 2
- ⚠️ Warning or issue discovered
- ❌ Failure or blocker

**Files Modified**:
- `path/to/file1.cs` - What changed
- `path/to/file2.md` - What changed

**Commands Run**:
```powershell
# Command 1 with purpose
dotnet test --filter "FullyQualifiedName~TestName"

# Command 2 with purpose
mysql -h localhost -u root -proot database_name -e "SELECT * FROM table;"
```

---

### Phase 2: [Phase Name] (HH:MM - HH:MM, Duration: X min)

[Repeat structure from Phase 1]

---

## Tools & Techniques Used

### MCP Tools

**Tool Name**: `tool_name`  
**Purpose**: Why was this tool used?  
**Command**:
```typescript
tool_name({
  parameter1: "value",
  parameter2: "value"
})
```
**Result**: What did the tool reveal or accomplish?

---

### PowerShell Commands

**Purpose**: [Why these commands?]

```powershell
# Command 1
dotnet build -c Debug

# Command 2
dotnet test --filter "FullyQualifiedName~Dao_Something_Tests"
```

**Results**: [What did commands show?]

---

### SQL Queries

**Purpose**: [What were you checking?]

```sql
-- Query 1
SELECT COUNT(*) FROM usr_users WHERE UserID LIKE 'TEST-%';

-- Query 2
SELECT * FROM sys_quick_buttons WHERE UserID = 'TEST-USER';
```

**Results**: [What did queries reveal?]

---

## Problems Encountered & Solutions

### Problem 1: [Brief Problem Description]

**Symptoms**: [What was going wrong?]

**Investigation Steps**:
1. Step 1 - What you checked
2. Step 2 - What you tried
3. Step 3 - What you discovered

**Root Cause**: [What was actually wrong?]

**Solution**: [How did you fix it?]

**Prevention**: [How to avoid this in future?]

---

### Problem 2: [Brief Problem Description]

[Repeat structure from Problem 1]

---

## Outcomes & Results

### Tests Fixed

| Test Name | Status Before | Status After | Time to Fix | Notes |
|-----------|---------------|--------------|-------------|-------|
| TestName1 | ❌ Failing | ✅ Passing | 15 min | Root cause: X |
| TestName2 | ❌ Failing | ✅ Passing | 22 min | Root cause: Y |
| TestName3 | ❌ Failing | ⚠️ In Progress | - | Blocked by Z |

**Total Fixed**: X/Y tests  
**Success Rate**: XX%  
**Average Time per Test**: XX minutes

---

### Code Changes

**Files Created**:
- `path/to/new/file.cs` - Purpose
- `path/to/another/file.sql` - Purpose

**Files Modified**:
- `path/to/existing/file.cs` - Changes made
- `path/to/another/file.md` - Changes made

**Lines Changed**: +XXX / -YYY

**Impact**: [What areas of the codebase are affected?]

---

### Documentation Updated

- [ ] Category files updated with progress
- [ ] DASHBOARD.md regenerated
- [ ] CHANGELOG.md entry added
- [ ] Investigation notes captured
- [ ] Reference documentation updated

---

## Lessons Learned

### What Went Well ✅

- Learning 1 - Why it worked
- Learning 2 - Why it was effective
- Learning 3 - Worth repeating

---

### What Could Be Improved ⚠️

- Challenge 1 - What slowed progress
- Challenge 2 - What was confusing
- Challenge 3 - What should change

---

### Specific Technical Insights

**Insight 1**: [Technical pattern or gotcha discovered]

**Example**:
```csharp
// Before (problematic)
var result = await Dao.Method();
Assert.IsTrue(result.Data.Rows.Count > 0);  // NullReferenceException!

// After (correct)
var result = await Dao.Method();
Assert.IsTrue(result.IsSuccess && result.Data != null && result.Data.Rows.Count > 0);
```

**Why This Matters**: [Explain implications]

---

**Insight 2**: [Another discovery]

[Same structure as Insight 1]

---

## Velocity & Metrics

**Tests Fixed**: X  
**Time Spent**: Y hours  
**Average Time per Test**: Z minutes  
**Estimated vs Actual**: [How did estimates compare?]

**Efficiency Factors**:
- Factor 1 that sped things up
- Factor 2 that slowed things down

**Comparison to Previous Sessions**: [How does this compare?]

---

## Next Steps

### Immediate Actions (Next Session)

1. [ ] Action 1 - Estimated time: XX min
2. [ ] Action 2 - Estimated time: XX min
3. [ ] Action 3 - Estimated time: XX min

**Priority**: [HIGH / MEDIUM / LOW]  
**Blockers**: [Any known obstacles?]

---

### Future Work (Later Sessions)

- Task 1 - When to do it
- Task 2 - When to do it
- Task 3 - When to do it

---

## References & Links

**Related Sessions**:
- [Session Name](YYYY-MM-DD-session-name.md) - How they relate

**Workspace Files Updated**:
- [Category 1](../categories/01-quick-buttons.md) - What changed
- [DASHBOARD](../DASHBOARD.md) - Progress updated

**External Documentation**:
- [Integration Testing Instructions](../../.github/instructions/integration-testing.instructions.md)
- [MCP Tool Reference](../reference/mcp-tools-full.md)

**Code References**:
- `Data/Dao_Something.cs` - DAO modified
- `Tests/Integration/Dao_Something_Tests.cs` - Tests updated

---

## Appendix: Detailed Output

### Build Output

```
[Paste relevant build output if needed]
```

### Test Results

```
[Paste test execution results if needed]
```

### SQL Results

```
[Paste SQL query results if needed]
```

---

**Session Logged By**: [Name]  
**Reviewed By**: [Name, if applicable]  
**Session Log Version**: 1.0  
**Template Version**: 1.0
