# Additional Test Tasks for Comprehensive Database Layer Refactor

**Purpose**: Tests identified from tasks.md.backup that were not included in the refactored tasks.md

**Organization**: Grouped by phase and user story, following Task-SubTask format

---

## Phase 4: User Story 2 - Additional Tests

### T021b [P] [US2-Test] Enhanced Dao_Inventory_Tests Coverage

**File**: `Tests/Integration/Dao_Inventory_Tests.cs`

**Missing Test Scenarios**:
- [ ] T021b-1 Test SearchInventoryAsync with empty results - verify DaoResult.IsSuccess = true with empty DataTable
- [ ] T021b-2 Test SearchInventoryAsync with wildcard patterns - verify partial matches work correctly
- [ ] T021b-3 Test UpdateInventoryAsync with invalid InventoryID - verify DaoResult.IsSuccess = false with appropriate error message
- [ ] T021b-4 Test GetAllInventoryAsync pagination support - verify large result sets handled correctly
- [ ] T021b-5 Test concurrent AddInventoryAsync operations - verify no deadlocks or duplicate keys

---

## Phase 5: User Story 3 - Additional Tests

### T030b [P] [US3-Test] Extended ErrorLogging_Tests Coverage

**File**: `Tests/Integration/ErrorLogging_Tests.cs`

**Missing Test Scenarios**:
- [ ] T030b-1 Test GetUniqueErrorsAsync filters duplicate error messages correctly
- [ ] T030b-2 Test GetErrorsByDateRangeAsync with invalid date range (end before start) - verify validation error
- [ ] T030b-3 Test DeleteErrorByIdAsync with non-existent ID - verify graceful handling
- [ ] T030b-4 Test LogDatabaseError with NULL exception - verify fallback to file logging
- [ ] T030b-5 Test error logging under high concurrency (100 simultaneous errors) - verify all logged without loss

### T031b [P] [US3-Test] Extended ValidationErrors_Tests Coverage

**File**: `Tests/Integration/ValidationErrors_Tests.cs`

**Missing Test Scenarios**:
- [ ] T031b-1 Test Dao_User.CreateUserAsync with duplicate username - verify user-friendly "Username already exists" message
- [ ] T031b-2 Test Dao_Part.CreatePartAsync with invalid PartNumber format - verify format validation message
- [ ] T031b-3 Test Dao_Inventory.TransferInventoryAsync with insufficient quantity - verify business rule validation
- [ ] T031b-4 Test Dao_Location.CreateLocationAsync with NULL LocationCode - verify required field validation
- [ ] T031b-5 Test parameter type mismatch (string passed to int parameter) - verify type validation error

### T032b [P] [US3-Test] Extended ErrorCooldown_Tests Coverage

**File**: `Tests/Integration/ErrorCooldown_Tests.cs`

**Missing Test Scenarios**:
- [ ] T032b-1 Test ClearErrorCooldownState() method - verify cooldown reset works correctly
- [ ] T032b-2 Test error cooldown with different error messages - verify only identical messages suppressed
- [ ] T032b-3 Test error cooldown expiration - trigger error, wait 6 seconds, trigger again, verify both logged and both displayed
- [ ] T032b-4 Test error cooldown across multiple users - verify user-specific cooldown tracking
- [ ] T032b-5 Test error cooldown during rapid-fire errors (50 identical errors in 1 second) - verify only first shown in UI

---

## Phase 6: User Story 4 - Additional Tests

### T039b [P] [US4-Test] Extended StoredProcedureValidation_Tests Coverage

**File**: `Tests/Integration/StoredProcedureValidation_Tests.cs`

**Missing Test Scenarios**:
- [ ] T039b-1 Test all stored procedures return consistent status codes (0=success, -1 to -5=errors)
- [ ] T039b-2 Test all stored procedures have consistent error message format (OUT p_ErrorMsg VARCHAR(500))
- [ ] T039b-3 Test stored procedures with INFORMATION_SCHEMA query - verify parameter prefixes match (p_ prefix)
- [ ] T039b-4 Test stored procedure naming convention - verify all use sp_ or sys_ prefix
- [ ] T039b-5 Test stored procedures have no TRUNCATE or DROP statements (safety check)

### T041b [P] [US4-Test] Extended ParameterNaming_Tests Coverage

**File**: `Tests/Integration/ParameterNaming_Tests.cs`

**Missing Test Scenarios**:
- [ ] T041b-1 Test parameter names match C# conventions (no underscores except p_ prefix)
- [ ] T041b-2 Test IN/OUT parameter direction matches stored procedure definition
- [ ] T041b-3 Test parameter data types match between C# Dictionary<string, object> and MySQL stored procedure
- [ ] T041b-4 Test optional parameters (NULL allowed) vs required parameters
- [ ] T041b-5 Test parameter order independence - verify Dictionary key lookup works regardless of parameter order

---

## Phase 7: User Story 5 - Additional Tests

### T050b [P] [US5-Test] Extended PerformanceMonitoring_Tests Coverage

**File**: `Tests/Integration/PerformanceMonitoring_Tests.cs`

**Missing Test Scenarios**:
- [ ] T050b-1 Test operation category detection accuracy - verify Query/Modification/Batch/Report classifications
- [ ] T050b-2 Test performance threshold configuration - verify QueryThresholdMs, ModificationThresholdMs values applied
- [ ] T050b-3 Test slow query logging format - verify log includes SP name, duration, parameters (sanitized)
- [ ] T050b-4 Test performance monitoring overhead - verify tracing adds <5ms overhead per operation
- [ ] T050b-5 Test performance dashboard aggregation - verify average execution times calculated correctly

### T051b [P] [US5-Test] Extended ConcurrentOperations_Tests Coverage

**File**: `Tests/Integration/ConcurrentOperations_Tests.cs`

**Missing Test Scenarios**:
- [ ] T051b-1 Test connection pool statistics - verify MinPoolSize=5, MaxPoolSize=100 enforced
- [ ] T051b-2 Test connection pool under sustained load (1000 operations over 60 seconds)
- [ ] T051b-3 Test connection pool recovery after transient failure (server restart simulation)
- [ ] T051b-4 Test connection timeout handling - verify 30 second timeout enforced
- [ ] T051b-5 Test connection disposal - verify all connections returned to pool after operations

### T052b [P] [US5-Test] Extended TransactionRollback_Tests Coverage

**File**: `Tests/Integration/TransactionRollback_Tests.cs`

**Missing Test Scenarios**:
- [ ] T052b-1 Test nested transaction handling - verify inner transaction rollback doesn't affect outer
- [ ] T052b-2 Test transaction isolation levels - verify default READ COMMITTED behavior
- [ ] T052b-3 Test deadlock detection and retry - verify transient error retry logic (3 attempts)
- [ ] T052b-4 Test transaction timeout - verify operations exceeding 30 seconds rollback
- [ ] T052b-5 Test multi-table transaction consistency - verify all-or-nothing across multiple tables

---

## Phase 8: Polish - Additional Validation Tests

### T083k [Validation] Integration Test Code Coverage

**Purpose**: Verify integration test coverage meets minimum standards

**Validation Steps**:
- [ ] T083k-1 Generate test coverage report for Tests/Integration/ folder
- [ ] T083k-2 Verify ≥80% DAO method coverage (public methods in Data/ folder)
- [ ] T083k-3 Verify ≥60% error path coverage (catch blocks, validation failures)
- [ ] T083k-4 Identify untested DAO methods - create tracking issue for gaps
- [ ] T083k-5 Verify each stored procedure exercised by ≥1 integration test

### T083l [Validation] End-to-End Workflow Tests

**Purpose**: Validate complete user workflows beyond individual DAO tests

**Validation Steps**:
- [ ] T083l-1 Test complete inventory add workflow (MainForm → Dao_Inventory → stored procedure → database → UI refresh)
- [ ] T083l-2 Test complete transfer workflow with rollback (error mid-transfer → verify rollback → retry success)
- [ ] T083l-3 Test complete user management workflow (create user → assign role → authenticate → update settings)
- [ ] T083l-4 Test error logging workflow (trigger error → verify log_error entry → verify Service_DebugTracer output)
- [ ] T083l-5 Test startup validation workflow (database unavailable → verify error message → graceful shutdown)

### T083m [Validation] Performance Regression Tests

**Purpose**: Establish performance baselines and detect regressions

**Validation Steps**:
- [ ] T083m-1 Establish baseline execution times for all 60+ stored procedures (pre-refactor)
- [ ] T083m-2 Measure post-refactor execution times - verify <5% variance per SC-004
- [ ] T083m-3 Test memory usage under load (1000 operations) - verify <10% increase from baseline
- [ ] T083m-4 Test UI responsiveness during heavy database operations - verify sub-100ms UI thread
- [ ] T083m-5 Test startup time with cold/warm parameter cache - verify <3 seconds per SC-010

### T083n [Validation] Security Validation Tests

**Purpose**: Verify security requirements met across refactored codebase

**Validation Steps**:
- [ ] T083n-1 Scan all DAO files for SQL injection vulnerabilities (inline SQL, string concatenation)
- [ ] T083n-2 Verify no hardcoded credentials in Data/, Helpers/, Services/ folders
- [ ] T083n-3 Test parameter validation at DAO boundaries - verify all user input validated
- [ ] T083n-4 Verify sensitive data not logged (passwords, connection strings) - scan LoggingUtility calls
- [ ] T083n-5 Test connection string security - verify encrypted storage in production config

---

## Summary of Additional Tests

**Total New Test Scenarios**: 60+ additional test cases across 5 phases

**Test Distribution**:
- Phase 4 (US2): 5 additional Dao_Inventory tests
- Phase 5 (US3): 15 additional tests (ErrorLogging, ValidationErrors, ErrorCooldown)
- Phase 6 (US4): 10 additional tests (StoredProcedureValidation, ParameterNaming)
- Phase 7 (US5): 15 additional tests (PerformanceMonitoring, ConcurrentOperations, TransactionRollback)
- Phase 8 (Polish): 15 additional validation tests (Coverage, E2E, Performance, Security)

**Rationale for Additions**:
1. **Error path coverage** - Original tasks.md focused on happy paths, added failure scenarios
2. **Concurrency testing** - Added multi-user, high-load scenarios
3. **Security validation** - Added explicit security checks per best practices
4. **Performance baselines** - Added regression detection tests
5. **End-to-end workflows** - Added integration beyond unit-level DAO tests

**Implementation Priority**:
1. **High**: T031b (ValidationErrors), T051b (ConcurrentOperations), T083n (Security) - critical for production readiness
2. **Medium**: T030b (ErrorLogging), T050b (PerformanceMonitoring), T083m (Performance) - important for observability
3. **Low**: T039b (StoredProcedureValidation), T083k (Coverage), T083l (E2E) - nice-to-have for comprehensive testing

---

**Last Updated**: 2025-10-14  
**Source**: Comparison of tasks.md vs tasks.md.backup  
**Integration**: Add these tests to main tasks.md after current test implementation complete