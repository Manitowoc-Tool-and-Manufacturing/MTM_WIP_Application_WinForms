# Category 3: Helper & Validation Tests

**Priority**: 🟢 LOW  
**Status**: 🟡 Not Started  
**Tests**: 0/5 passing (0%)  
**Estimated Effort**: 2-3 hours

---

## Root Cause Analysis

**Problem**: Timing issues, validation edge cases, and helper method assumptions causing intermittent or consistent failures.

**Investigation Findings** (via MCP tools):
- `validate_error_handling` showed helpers follow Service_ErrorHandler patterns
- `analyze_performance` revealed no blocking operations in helpers
- `check_security` confirmed validation helpers properly sanitize inputs
- Manual investigation showed edge cases not fully tested

**Why Tests Are Failing**:
1. **DateTime Helper**: Edge cases with different date formats not handled
2. **Validation Helper**: Part number formats have regional variations not covered
3. **String Helper**: Truncation logic edge cases (empty strings, exact length strings)
4. **Part Number Validation**: InvalidFormats test expects specific error messages not returned
5. **Location Code Validation**: Invalid codes test assumptions don't match actual validation logic

**Root Cause**: Tests make assumptions about helper behavior that don't match actual implementation OR helpers have edge case bugs. Requires case-by-case investigation and either test fix OR helper fix.

**Note**: These are not data setup issues like Categories 1 & 2. Each test needs individual analysis.

---

## Fix Strategy

### High-Level Approach

1. **Investigate Each Test Individually**
   - Run test in isolation to understand exact failure
   - Review helper method implementation
   - Determine if test assumption is wrong OR helper has bug

2. **Fix Based on Findings**
   - **Test Wrong**: Update test expectations to match correct helper behavior
   - **Helper Wrong**: Fix helper method and verify all usages
   - **Edge Case**: Add edge case handling to helper if missing

3. **Add Edge Case Coverage**
   - Expand test coverage for edge cases discovered
   - Document helper behavior in XML comments
   - Update helper documentation in reference/

### Investigation Workflow (Per Test)

1. **Read test code**: Understand what's being tested
2. **Read helper code**: Understand actual implementation
3. **Run test with debugger**: See exact failure point and values
4. **Determine root cause**: Test assumption vs helper bug vs edge case
5. **Implement fix**: Either test OR helper (not both unless justified)
6. **Verify**: Run test to confirm fix, run ALL tests to prevent regressions

### Code Changes Needed

**Location**: Varies by test
- `Helpers/Helper_DateTime.cs` (if DateTimeHelper test reveals bug)
- `Helpers/Helper_Validation.cs` (if ValidationHelper test reveals bug)
- `Helpers/Helper_String.cs` (if StringHelper test reveals bug)
- `Tests/Integration/Helper_Tests.cs` (update test expectations)
- `Tests/Integration/Validation_Tests.cs` (update validation assertions)

**No Stored Procedures Involved**: These are pure C# helper tests, no database operations.

### Verification Steps

After fixing each test:
1. Run individual test to confirm pass
2. Run ALL helper tests to check for regressions: `dotnet test --filter "FullyQualifiedName~Helper_Tests"`
3. Run ALL validation tests: `dotnet test --filter "FullyQualifiedName~Validation_Tests"`
4. Check helper usage in application code to ensure fix doesn't break existing functionality

---

## Test List

### Tests to Fix (0/5 complete)

- [ ] **DateTimeHelper_ParseDate_ValidFormats_ReturnsDateTime** | `Tests/Integration/Helper_Tests.cs:22`
  - **Error**: "ParseDate failed for format 'MM/dd/yyyy' with valid date"
  - **Fix approach**: TBD after investigation - could be culture-specific parsing issue
  - **Verification**: Test passes with multiple date formats (MM/dd/yyyy, yyyy-MM-dd, dd-MM-yyyy)
  - **Investigation needed**: Check if helper uses CurrentCulture vs InvariantCulture

- [ ] **ValidationHelper_ValidatePartNumber_ValidFormats_ReturnsTrue** | `Tests/Integration/Helper_Tests.cs:56`
  - **Error**: "Valid part number 'PART-12345' rejected by validation"
  - **Fix approach**: TBD after investigation - review part number regex pattern
  - **Verification**: Test passes with various valid formats (PART-12345, P-001, ASSY-999)
  - **Investigation needed**: Check if regex pattern is too restrictive

- [ ] **StringHelper_TruncateWithEllipsis_LongString_TruncatesCorrectly** | `Tests/Integration/Helper_Tests.cs:90`
  - **Error**: "Truncate returned 'LongStri...' expected 'LongString...'"
  - **Fix approach**: TBD after investigation - verify truncation length calculation
  - **Verification**: Truncation includes ellipsis and respects max length parameter
  - **Investigation needed**: Off-by-one error in truncation logic?

- [ ] **PartNumberValidation_InvalidFormats_ReturnsFalse** | `Tests/Integration/Validation_Tests.cs:34`
  - **Error**: "Invalid part number 'INVALID PART' not rejected"
  - **Fix approach**: TBD after investigation - check what makes part number invalid
  - **Verification**: All invalid formats rejected (spaces, special chars, empty)
  - **Investigation needed**: What are the actual part number validation rules?

- [ ] **LocationCodeValidation_InvalidCodes_ReturnsFalse** | `Tests/Integration/Validation_Tests.cs:68`
  - **Error**: "Invalid location 'BADLOC' not rejected"
  - **Fix approach**: TBD after investigation - check location code validation logic
  - **Verification**: Only valid locations (FLOOR, RECEIVING, SHIPPING, etc.) pass
  - **Investigation needed**: Is validation checking against database OR config file?

---

## Relevant MCP Tools

### For This Category

- **validate_error_handling** - Verify helpers use Service_ErrorHandler
  ```
  validate_error_handling(
    source_dir: "Helpers/",
    recursive: false
  )
  ```
  *Use when*: After modifying helper methods

- **check_security** - Verify validation helpers properly sanitize
  ```
  check_security(
    source_dir: "Helpers/",
    scan_type: "code"
  )
  ```
  *Use when*: Fixing validation helper methods

- **analyze_performance** - Check for performance issues in helpers
  ```
  analyze_performance(
    source_dir: "Helpers/",
    focus: "all"
  )
  ```
  *Use when*: After helper modifications to ensure no regressions

- **check_xml_docs** - Verify helper methods are documented
  ```
  check_xml_docs(
    source_dir: "Helpers/",
    min_coverage: 80
  )
  ```
  *Use when*: After fixing helpers, ensure XML docs updated

---

## Commands

### PowerShell Test Commands

```powershell
# Run all helper tests
dotnet test --filter "FullyQualifiedName~Helper_Tests"

# Run all validation tests
dotnet test --filter "FullyQualifiedName~Validation_Tests"

# Run single test
dotnet test --filter "FullyQualifiedName~Helper_Tests.DateTimeHelper_ParseDate_ValidFormats_ReturnsDateTime"

# Run both categories together
dotnet test --filter "FullyQualifiedName~Helper_Tests|FullyQualifiedName~Validation_Tests"

# Run with detailed output for debugging
dotnet test --filter "FullyQualifiedName~Helper_Tests" --logger "console;verbosity=detailed"
```

### Debugging Commands

```powershell
# Run single test with debugger attached
dotnet test --filter "FullyQualifiedName~Helper_Tests.DateTimeHelper_ParseDate" --logger "console;verbosity=detailed" --collect:"XPlat Code Coverage"

# Build in debug mode for better debugging
dotnet build MTM_Inventory_Application.csproj -c Debug
```

---

## Code Locations

### Helper Files (May Need Fixes)
- `Helpers/Helper_DateTime.cs` - Date/time parsing and formatting helpers
- `Helpers/Helper_Validation.cs` - Input validation helpers (part numbers, locations)
- `Helpers/Helper_String.cs` - String manipulation helpers (truncate, format)

### Test Files
- `Tests/Integration/Helper_Tests.cs` - Helper method tests (3 tests)
- `Tests/Integration/Validation_Tests.cs` - Validation logic tests (2 tests)

### Configuration (May Affect Validation)
- `Core/Core_WipAppVariables.cs` - May contain valid location codes
- `Models/Model_AppVariables.cs` - May contain validation rules

### No Stored Procedures
- These are pure C# unit/integration tests, no database involved

---

## Notes & Gotchas

### Common Mistakes to Avoid

1. **Culture-Specific Assumptions**: DateTime parsing may fail in different locales, use InvariantCulture
2. **Regex Edge Cases**: Validation regex may not cover all valid cases, test thoroughly
3. **Off-By-One Errors**: String truncation often has fencepost errors with length calculations
4. **Null/Empty Handling**: Ensure helpers gracefully handle null, empty, and whitespace-only inputs

### Dependencies

- **Depends on**: None - can be fixed independently
- **Blocks**: None - these are utility tests, don't block other categories

### Test Isolation Considerations

- Helper tests are pure unit tests, no shared state or database
- Each test should be completely independent
- No setup/teardown needed (unlike Categories 1 & 2)

### Performance Considerations

- Helper methods should be fast (< 1ms typically)
- If helpers are slow, may indicate inefficient implementation
- DateTime parsing can be slow if using multiple format attempts

### Debugging Tips

1. **Use debugger**: Step through helper method with test input to see exact behavior
2. **Check assumptions**: Print actual vs expected values to see difference
3. **Test edge cases**: Empty string, null, max length, special characters
4. **Review helper history**: Check git blame to see if recent changes broke tests

---

## Investigation Notes

### Per-Test Investigation Results

*(To be filled in as tests are investigated)*

#### DateTimeHelper_ParseDate_ValidFormats_ReturnsDateTime
- **Investigation Date**: TBD
- **Root Cause Found**: TBD
- **Fix Applied**: TBD
- **Verification**: TBD

#### ValidationHelper_ValidatePartNumber_ValidFormats_ReturnsTrue
- **Investigation Date**: TBD
- **Root Cause Found**: TBD
- **Fix Applied**: TBD
- **Verification**: TBD

#### StringHelper_TruncateWithEllipsis_LongString_TruncatesCorrectly
- **Investigation Date**: TBD
- **Root Cause Found**: TBD
- **Fix Applied**: TBD
- **Verification**: TBD

#### PartNumberValidation_InvalidFormats_ReturnsFalse
- **Investigation Date**: TBD
- **Root Cause Found**: TBD
- **Fix Applied**: TBD
- **Verification**: TBD

#### LocationCodeValidation_InvalidCodes_ReturnsFalse
- **Investigation Date**: TBD
- **Root Cause Found**: TBD
- **Fix Applied**: TBD
- **Verification**: TBD

---

## Progress Tracking

**Last Updated**: 2025-10-19  
**Current Status**: Not started - requires individual investigation per test  
**Next Test to Fix**: DateTimeHelper_ParseDate_ValidFormats_ReturnsDateTime (investigate first)

**Completion**: 0/5 (0%)  
**Progress Bar**: [░░░░░░░░░░] 0%

---

**Category Maintainer**: Development Team  
**Reference**: .github/instructions/integration-testing.instructions.md
