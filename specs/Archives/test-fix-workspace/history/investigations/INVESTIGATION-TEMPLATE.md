# Investigation Deep-Dive Template

**Use this template for complex debugging sessions requiring detailed analysis**

---

## Investigation: [Problem Being Investigated]

**Date**: YYYY-MM-DD  
**Investigator**: [Name]  
**Priority**: [HIGH / MEDIUM / LOW]  
**Status**: [🟠 Investigating / ✅ Resolved / ❌ Blocked]

---

## Problem Statement

**Summary**: [One paragraph describing the issue]

**Symptoms**:
- Symptom 1 - What is observed
- Symptom 2 - How it manifests
- Symptom 3 - When it occurs

**Impact**: [Who/what is affected? How critical?]

**Frequency**: [Always / Sometimes / Rarely]

---

## Context & Background

**When First Noticed**: [Date, session, or commit]

**Related Components**:
- Component 1 - `path/to/file.cs`
- Component 2 - `path/to/another.sql`
- Component 3 - Database table name

**Recent Changes**: [What changed before this appeared?]

**Previous Similar Issues**: [Any precedent?]

---

## Reproduction Steps

### Minimal Reproduction Case

**Environment**:
- OS: Windows 10/11
- .NET: 8.0.x
- Database: mtm_wip_application_winforms_test
- Branch: branch-name

**Steps to Reproduce**:
1. Step 1 - Specific action
2. Step 2 - Specific action
3. Step 3 - Expected vs actual outcome

**Reproduction Rate**: [X out of Y attempts]

**Variability Factors**: [What affects whether it reproduces?]

---

### Full Reproduction Code

```csharp
// Complete code to reproduce issue
// Include setup, execution, and cleanup
```

```sql
-- Any SQL needed to reproduce
```

```powershell
# Any commands needed to reproduce
```

---

## Hypothesis List

### Hypothesis 1: [Brief Description]

**Likelihood**: [HIGH / MEDIUM / LOW]  
**Evidence For**: [What supports this theory?]  
**Evidence Against**: [What contradicts this theory?]  
**Test**: [How to verify or disprove this?]  
**Status**: [Untested / Tested - Confirmed / Tested - Disproved]

---

### Hypothesis 2: [Brief Description]

[Same structure as Hypothesis 1]

---

### Hypothesis 3: [Brief Description]

[Same structure as Hypothesis 1]

---

## Investigation Steps

### Step 1: [Investigation Action] (YYYY-MM-DD HH:MM)

**Objective**: [What were you trying to learn?]

**Method**:
- Technique 1 used
- Technique 2 used
- Technique 3 used

**Tools Used**:
- Debugger breakpoints at lines X, Y, Z
- MCP tool: `tool_name`
- Logging statements added to methods

**Findings**:
```
[Raw data, stack traces, log output, etc.]
```

**Analysis**: [What does this tell us?]

**Conclusion**: [Does this support or reject any hypotheses?]

---

### Step 2: [Investigation Action] (YYYY-MM-DD HH:MM)

[Same structure as Step 1]

---

## Data Collected

### Debugger Session

**Breakpoint Locations**:
- File: `path/to/file.cs`, Line: XXX, Condition: `variable == value`
- File: `path/to/other.cs`, Line: YYY

**Variable Inspection**:

| Variable | Expected Value | Actual Value | Notes |
|----------|----------------|--------------|-------|
| result.IsSuccess | true | false | Why? |
| result.Data | DataTable | null | Unexpected |
| connectionString | localhost | 172.x.x.x | Wrong server? |

**Call Stack**:
```
Method1() at File1.cs:line 45
  Method2() at File2.cs:line 123
    Method3() at File3.cs:line 67
```

**Insights**: [What did debugging reveal?]

---

### Log Analysis

**Relevant Log Entries**:
```
[2025-10-19 14:23:45] INFO: Starting operation X
[2025-10-19 14:23:46] ERROR: Exception in Method Y: NullReferenceException
[2025-10-19 14:23:46] DEBUG: Variable state: { IsNull: true, Count: 0 }
```

**Patterns Observed**:
- Pattern 1 - What repeats
- Pattern 2 - Timing patterns
- Pattern 3 - Correlation with X

**Insights**: [What do logs tell us?]

---

### Database State Analysis

**Tables Examined**:
```sql
-- Query 1: Check test data
SELECT * FROM usr_users WHERE UserID LIKE 'TEST-%';
-- Result: 0 rows (AH HA! No test data!)

-- Query 2: Check related records
SELECT COUNT(*) FROM sys_quick_buttons WHERE UserID = 'TEST-USER';
-- Result: Error - foreign key violation
```

**Schema Issues**:
- Issue 1 discovered
- Issue 2 discovered

**Insights**: [What does database state reveal?]

---

### Code Review Findings

**Suspicious Code Section 1**:

```csharp
// File: path/to/file.cs, Lines: 45-60
public async Task<Model_Dao_Result<DataTable>> GetData()
{
    var result = await Helper.ExecuteAsync(...);
    return result;  // ⚠️ No null check on result.Data!
}
```

**Why Suspicious**: [Explain the issue]

**Potential Fix**: [How to address]

---

**Suspicious Code Section 2**:

[Same structure as Section 1]

---

## Root Cause Analysis

### Primary Root Cause

**Cause**: [What is fundamentally wrong?]

**Why It Happens**: [Explain the mechanism]

**Why It Wasn't Caught Earlier**: [How did this slip through?]

**Evidence Supporting This**: 
- Evidence 1
- Evidence 2
- Evidence 3

**Confidence Level**: [HIGH / MEDIUM / LOW]

---

### Contributing Factors

**Factor 1**: [What made this worse or more likely?]  
**Factor 2**: [Another contributing element]  
**Factor 3**: [Third factor]

---

## Solution Design

### Proposed Solution

**Approach**: [High-level fix strategy]

**Implementation**:

```csharp
// Proposed code changes
// Show before/after
```

**Why This Works**: [Explain how this addresses root cause]

**Alternatives Considered**:
- Alternative 1 - Why not chosen
- Alternative 2 - Why not chosen

---

### Validation Plan

**How to Verify Fix**:
1. Verification step 1
2. Verification step 2
3. Verification step 3

**Success Criteria**:
- [ ] Criterion 1
- [ ] Criterion 2
- [ ] Criterion 3

**Regression Tests**:
- Test 1 to ensure no side effects
- Test 2 to ensure no side effects

---

## Implementation

### Changes Made

**Files Modified**:
- `path/to/file.cs` - Changes
- `path/to/test.cs` - Changes
- `path/to/sql.sql` - Changes

**Code Diff Summary**:
```diff
- Old problematic line
+ New corrected line
```

**Migration Steps** (if applicable):
1. Step 1
2. Step 2
3. Step 3

---

### Testing Performed

**Unit Tests**:
- Test 1: ✅ Passed
- Test 2: ✅ Passed
- Test 3: ⚠️ Warning but acceptable

**Integration Tests**:
- Test 1: ✅ Passed
- Test 2: ✅ Passed

**Manual Testing**:
- Scenario 1: ✅ Passed
- Scenario 2: ✅ Passed

**Regression Testing**:
- Previously passing tests: ✅ Still passing
- Related functionality: ✅ No impact

---

## Prevention Measures

### Code Changes to Prevent Recurrence

**Guard Clauses Added**:
```csharp
if (result.Data == null)
{
    LoggingUtility.LogWarning("Data is null in GetData()");
    return Model_Dao_Result<DataTable>.Failure("No data returned");
}
```

**Validation Added**:
```csharp
ArgumentNullException.ThrowIfNull(parameter);
```

---

### Process Improvements

**What Should Change**:
1. Improvement 1 - How to implement
2. Improvement 2 - How to implement
3. Improvement 3 - How to implement

**Documentation Updates**:
- Update instruction file: path/to/file.md
- Add pattern to test-patterns.md
- Document gotcha in memory file

---

### Detection Improvements

**Earlier Detection Methods**:
- Static analysis rule to add
- Build warning to enable
- Test to add to suite

**Monitoring** (if applicable):
- Metric to track
- Alert to create
- Log statement to add

---

## Lessons Learned

### Technical Insights

**Key Learning 1**: [Important technical discovery]

**Example**:
```csharp
// Pattern to avoid
// Pattern to use instead
```

---

**Key Learning 2**: [Another discovery]

---

### Process Insights

**What Worked Well**:
- Technique 1 - Why effective
- Technique 2 - Why effective

**What Could Be Better**:
- Challenge 1 - How to improve
- Challenge 2 - How to improve

**Investigation Efficiency**:
- Total time: X hours
- Most helpful technique: Y
- Time wasters: Z

---

## Related Issues

**Similar Past Issues**:
- Issue 1 - How it relates
- Issue 2 - What's different

**Potential Future Issues**:
- Risk 1 - How to watch for it
- Risk 2 - How to prevent it

---

## References

**Code References**:
- Primary file: `path/to/file.cs`
- Related files: `path/to/related.cs`

**Documentation**:
- Relevant instruction file
- Related memory file
- External documentation

**Tools Used**:
- MCP tool documentation
- Debugger techniques
- Profiling tools

---

## Appendix: Raw Data

### Stack Traces

```
[Full stack traces if needed]
```

### Log Dumps

```
[Complete log sections if needed]
```

### Database Dumps

```sql
-- Full query results if needed
```

---

**Investigation Started**: YYYY-MM-DD HH:MM  
**Investigation Completed**: YYYY-MM-DD HH:MM  
**Total Time**: X hours  
**Status**: ✅ Resolved / ❌ Blocked  
**Investigator**: [Name]  
**Reviewer**: [Name, if applicable]  
**Template Version**: 1.0
