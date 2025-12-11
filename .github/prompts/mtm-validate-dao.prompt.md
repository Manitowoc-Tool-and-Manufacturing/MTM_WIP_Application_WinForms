# Database Code Analysis and Remediation Prompt

## Context
We've identified several critical database performance issues:
- 20% of database connections are being aborted (not closed properly)
- Queries performing full table scans instead of using indexes
- 36 table joins per minute without proper indexes
- 111+ table rows being read per second (excessive full scans)
- Temporary tables being written to disk 3+ times per minute
- High rate of sorting operations (43+ per second)

## Your Task
Analyze the attached DAO file and all stored procedures it references to identify and fix code patterns that cause these issues.

## Analysis Checklist

### 1. Connection Management Review
**Check for:**
- [ ] Are all database connections properly disposed in `finally` blocks or using `using` statements?
- [ ] Does every method that opens a connection guarantee it closes, even on error?
- [ ] Are there any long-running connections that should be closed sooner?
- [ ] Are async methods properly awaiting connection disposal?
- [ ] Check: Does the code use `Helper_Database_StoredProcedure` correctly (this handles connection management)?

**Look for anti-patterns:**
- Connections opened but not closed in all code paths
- Missing `using` statements or `finally` blocks
- Connections kept open longer than necessary
- Connection leaks in error handling paths

### 2. Query Optimization - Stored Procedures
**For each stored procedure called by this DAO:**

**Index Usage:**
- [ ] Does every `WHERE` clause use indexed columns?
- [ ] Are `JOIN` conditions using indexed columns?
- [ ] Are `ORDER BY` columns indexed?
- [ ] Check for `SELECT *` - should only return needed columns

**Join Analysis:**
- [ ] Do all joins specify ON conditions with indexed columns?
- [ ] Are there any cartesian joins (missing WHERE/ON)?
- [ ] Could any joins be eliminated by restructuring?

**Scan Prevention:**
- [ ] Are there any queries without WHERE clauses on large tables?
- [ ] Are LIKE queries using leading wildcards (`LIKE '%value'`) that prevent index use?
- [ ] Are functions being called on indexed columns in WHERE clauses (prevents index use)?

**Temporary Table Minimization:**
- [ ] Can any `ORDER BY` clauses be removed or use indexed columns?
- [ ] Are there `DISTINCT` operations that could be avoided?
- [ ] Are there subqueries that could be rewritten as joins?
- [ ] Check for `GROUP BY` on non-indexed columns

**MySQL 5.7.24 Compliance:**
- [ ] Verify NO use of JSON functions (not available in 5.7.24)
- [ ] Verify NO CTEs (Common Table Expressions - 8.0+ only)
- [ ] Verify NO window functions (8.0+ only)

### 3. Code Pattern Review - DAO Methods

**For each method in the DAO:**
- [ ] Returns `Model_Dao_Result<T>` (as required by architecture)?
- [ ] Uses `Helper_Database_StoredProcedure` for all database calls?
- [ ] Properly handles errors without connection leaks?
- [ ] Uses async/await correctly?
- [ ] Disposes of DataTables and other resources?

**Check for:**
```csharp
// ✅ GOOD: Proper disposal
var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(...);
// Connection auto-closed by helper

// ❌ BAD: Manual connection handling
using (var conn = new MySqlConnection(...)) // Should NEVER appear in DAOs!
```

### 4. Specific Code Smells to Find and Fix

**Connection Issues:**
- Any direct use of `MySqlConnection` (should only be in Helper classes)
- Missing disposal of `MySqlDataReader`, `DataTable`, or `DataSet`
- Async methods not properly awaiting disposal

**Query Issues:**
- Stored procedures with `SELECT *` on large tables
- WHERE clauses on non-indexed columns
- Joins without proper ON conditions
- LIKE with leading wildcard: `WHERE name LIKE '%value'`
- Functions in WHERE: `WHERE YEAR(created_date) = 2025` (should be `WHERE created_date >= '2025-01-01'`)

**Performance Issues:**
- Multiple separate queries that could be combined
- N+1 query patterns (query in a loop)
- Unnecessary sorting or grouping
- Large result sets when only counts are needed

## Output Required

### Part 1: Issues Found
For each issue, provide:
1. **File and Location**: DAO method name or stored procedure name and line number
2. **Issue Type**: Connection leak, missing index, inefficient query, etc.
3. **Current Code**: Show the problematic code
4. **Impact**: How this contributes to server issues
5. **Severity**: Critical, High, Medium, Low

### Part 2: Recommended Fixes
For each issue:
1. **Fixed Code**: Provide the corrected version
2. **Explanation**: Why this fix resolves the issue
3. **Index Requirements**: If indexes need to be added to tables, specify which columns

### Part 3: Summary Report
- Total issues found by category
- Estimated performance improvement
- Priority order for fixes
- Any database schema changes needed (new indexes)

## Example Output Format

### Issue #1: Connection Not Properly Disposed
**File**: `Dao_Inventory.cs`, method `GetAllPartsAsync()`, lines 45-60
**Issue Type**: Connection Management - Potential Connection Leak
**Severity**: CRITICAL

**Current Code**:
```csharp
// Missing proper disposal in error path
```

**Impact**: Contributes to the 20% connection abort rate. If this method errors, connection may not close.

**Fixed Code**:
```csharp
// Proper disposal with using statement
```

**Explanation**: Ensures connection closes even on exception.

---

### Issue #2: Full Table Scan - Missing Index
**File**: `md_inventory_GetByLocation.sql`, line 12
**Issue Type**: Query Optimization - Missing Index
**Severity**: HIGH

**Current Code**:
```sql
WHERE location_name = p_Location
```

**Impact**: Causes full table scan of inventory table (10,000+ rows). Contributes to high Handler_read_rnd_next count.

**Fixed Code**:
```sql
-- Same query, but requires index
WHERE location_name = p_Location
```

**Required Index**:
```sql
CREATE INDEX idx_inventory_location_name ON inventory(location_name);
```

**Explanation**: Index allows direct lookup instead of scanning all rows.

## Constraints
- Follow MTM WIP Application Constitution (.specify/memory/constitution.md)
- Maintain .NET 8.0 / C# 12.0 / MySQL 5.7.24 compatibility
- All changes must use `Helper_Database_StoredProcedure` for database access
- All changes must return `Model_Dao_Result<T>`
- All error handling through `Service_ErrorHandler`
- Follow existing #region organization
- Maintain XML documentation

## After Analysis
Once you've completed the analysis and provided fixes, I will:
1. Review the proposed changes
2. Approve specific fixes
3. Have you implement the approved changes
4. Test the changes to verify performance improvement

Please proceed with the analysis of the attached DAO file.
