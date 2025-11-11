# Feature Specification: Comprehensive Database Layer Standardization

**Feature Branch**: `002-comprehensive-database-layer` (active)  
**Created**: 2025-10-15  
**Status**: Refresh - Enhanced Documentation  
**Original Spec**: Based on `specs/002-comprehensive-database-layer/spec.md`

## Overview

This specification defines a comprehensive refactor and standardization of the entire database access layer for the MTM WIP Application. The goal is to eliminate MySQL parameter prefix errors, establish uniform stored procedure standards, and create a maintainable, testable database layer that supports manufacturing operations reliably.

**Problem Statement**: The current database layer suffers from inconsistent stored procedure patterns, non-uniform parameter naming (mixed p*, in*, o\_ prefixes), incomplete error handling, and scattered async/sync patterns. This leads to MySQL parameter errors, difficult troubleshooting, and unreliable database operations in production.

**Solution Approach**: Implement a three-phase standardization:

1. **Phase 1-2**: Foundation - Model_Dao_Result pattern, Helper refactoring, parameter prefix detection
2. **Phase 2.5**: Stored Procedure Discovery & Standardization - Comprehensive audit and refactoring of ALL 60+ stored procedures
3. **Phase 3-8**: DAO Implementation - Systematic refactoring of 12 DAO classes and 100+ call sites

## Clarifications

### Session 2025-10-13 (Original Clarifications)

**Q1: Async Execution Mode Strategy**  
**A**: Async-only for new DAO methods, create separate sync wrapper class for legacy code

**Refined Decision (Q6)**: No DaoLegacy wrapper; all calling code must migrate to async immediately. Forces best practices everywhere immediately with clean, modern codebase and no technical debt.

---

**Q2: Application Termination Policy for Database Errors**  
**A**: Terminate on startup, show retry prompt to user during operation

**Implementation**: Startup failure prevents app initialization without database. During operation, user sees "Connection lost. Retry?" dialog with ability to retry or close app, preserving in-progress work where possible.

---

**Q3: Slow Query Threshold Configuration**  
**A**: Configurable per operation type (queries vs modifications vs batch vs reports)

**Thresholds Defined**:

-   Query operations: 500ms
-   Modification operations: 1000ms
-   Batch operations: 5000ms
-   Report operations: 2000ms

Configuration stored in `Model_Application_Variables` or appsettings file.

---

**Q4: Transaction Scope and Multi-Step Operation Policy**  
**A**: All multi-step operations MUST use explicit transactions

**Coverage**: Maximum data integrity approach covering all scenarios. Every multi-call DAO method includes transaction management with proper rollback on any step failure. Applies to inventory transfers, batch modifications, user creation with roles, etc.

---

**Q5: Error Logging Severity Classification**  
**A**: Three-tier severity (Critical / Error / Warning) with documented criteria

**Criteria**:

-   **Critical**: Data integrity risk or application cannot continue (e.g., database corruption, schema mismatch)
-   **Error**: Operation failed, user sees error message (e.g., constraint violation, connection failure)
-   **Warning**: Unexpected but handled, no user impact (e.g., slow query, retry succeeded)

Aligns with industry standards for effective triage.

---

**Q7: Parameter Prefix Auto-Detection Strategy**  
**A**: Query INFORMATION_SCHEMA.PARAMETERS at startup with fallback to convention

**Implementation**: 100% accurate parameter prefix information by querying database schema directly. One-time startup cost (~100-200ms) to query all procedures and cache results. Handles all prefix types (p*, in*, o*) automatically. Fallback to convention (p* default, in\_ for Transfer*/transaction* procedures) if schema query fails.

---

**Q8: Integration Test Database Management**  
**A**: Schema-only copy with per-test-class transactions

**Database Name**: `mtm_wip_application_winform_test` (Note: singular "winform", not "winforms")

**Approach**: Each developer maintains local test database with schema matching production. Test classes begin transaction, insert test data, run assertions, then roll back for clean slate. Fast execution (transactions cheaper than full resets), isolated tests (no interference between runs), parallel-safe (each developer independent).

---

### Session 2025-10-15 (Refresh Clarifications)

**Q9: Stored Procedure Standardization Scope**  
**A**: ALL stored procedures in codebase must be audited and standardized

**Decision**: Phase 2.5 added as blocking prerequisite that discovers, validates, and refactors ALL stored procedures before any DAO work proceeds. No exceptions - every procedure must meet `00_STATUS_CODE_STANDARDS.md` requirements.

---

**Q10: Checklist Requirements for Complex Tasks**  
**A**: Intensive multi-step tasks require dedicated validation checklists

**Implementation**: Tasks with 10+ subtasks or high complexity (T100-T132 in Phase 2.5, T033 with 25 UI settings methods) receive dedicated requirement quality checklists following `checklist-template.md` pattern. Checklists validate completeness, not implementation.

## User Scenarios & Testing _(mandatory)_

### User Story 1 - Developer Adds New Database Operation (Priority: P1) 🎯 MVP

**As a** developer adding new inventory features  
**I need** a standardized DAO pattern that eliminates parameter prefix errors and provides consistent error handling  
**So that** I can implement new database operations quickly without encountering MySQL errors

**Why this priority**: This is the foundation that enables all other database work. Without a uniform pattern, every new feature risks introducing MySQL parameter errors that currently plague the system.

**Independent Test Criteria**:

-   Create test stored procedure `test_operation_Add` with standard OUT parameters
-   Generate corresponding DAO method using standard Model_Dao_Result pattern
-   Execute with valid data → returns Model_Dao_Result.Success with confirmation
-   Execute with invalid data → returns Model_Dao_Result.Failure with user-friendly message
-   Force database error → returns Model_Dao_Result.Failure without crash, logs to database

**Acceptance Scenarios**:

1. **Valid Operation Execution**

    - **Given**: New stored procedure `test_operation_Add` exists with parameters (p_PartID, p_Quantity, OUT p_Status, OUT p_ErrorMsg)
    - **When**: Developer creates DAO method following standard pattern and calls with valid data (PartID="ABC123", Quantity=10)
    - **Then**: Method returns `Model_Dao_Result.Success` with `IsSuccess=true` and confirmation message "Operation completed successfully"

2. **Invalid Parameter Handling**

    - **Given**: Same DAO method exists
    - **When**: Developer calls with invalid/missing parameters (PartID=null, Quantity=-5)
    - **Then**: Method returns `Model_Dao_Result.Failure` with `IsSuccess=false`, user-friendly message "PartID is required" or "Quantity must be positive", and error logged to log_error table with full context

3. **Database Connection Failure Recovery**
    - **Given**: DAO method executing normally
    - **When**: Database connection fails mid-operation (network disconnect, server restart)
    - **Then**: Method catches exception, returns `Model_Dao_Result.Failure` with actionable message "Database connection lost. Please retry.", logs detailed error with stack trace, and application remains stable (no crash)

**Dependencies**: Blocks all other user stories - must complete first

---

### User Story 2 - Application Executes Reliable Database Operations (Priority: P1)

**As the** manufacturing application  
**I need** all database operations to handle errors gracefully with consistent status processing  
**So that** users experience stable behavior without unexpected crashes or data corruption

**Why this priority**: Directly addresses the current problem of non-uniform DAO files causing MySQL errors. Reliability is critical for manufacturing operations where data accuracy is essential.

**Independent Test Criteria**:

-   Execute 100 consecutive inventory additions sequentially
-   Force mid-operation database disconnect during operation #50
-   Attempt operations with malformed parameters (invalid types, out of range)
-   Monitor connection pool statistics (5-100 connections maintained)
-   Verify all scenarios return proper status codes without crashes

**Acceptance Scenarios**:

1. **Sequential Operation Reliability**

    - **Given**: 100 inventory items to add with valid data
    - **When**: All operations execute via `Dao_Inventory.AddInventoryAsync` sequentially
    - **Then**: All 100 succeed with `Model_Dao_Result.IsSuccess=true`, stored procedures return `p_Status=1` (success with data), and all items appear in database

2. **Mid-Operation Failure Recovery**

    - **Given**: Active database connection with operation in progress
    - **When**: Database becomes unavailable mid-operation (cable disconnect, server shutdown)
    - **Then**: System catches transient error, attempts retry (up to 3 times with exponential backoff), returns error status if retries exhausted, user receives actionable message "Unable to reach database. Check network connection.", application remains stable

3. **Parameter Validation at DAO Boundary**

    - **Given**: DAO method expecting (PartID:string, Quantity:decimal)
    - **When**: Called with malformed parameters (PartID="", Quantity="invalid")
    - **Then**: Method validates before database call, returns `Model_Dao_Result.Failure` with validation message "PartID cannot be empty" or "Quantity must be numeric", NOT MySQL exception

4. **Connection Pool Health Under Load**
    - **Given**: Connection pool configured (MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s)
    - **When**: 50 concurrent database operations execute simultaneously
    - **Then**: Pool maintains healthy connection count (5-100 range), operations queue appropriately, all complete successfully without timeout errors

**Dependencies**: Depends on US1 (DAO pattern must exist)

---

### User Story 3 - Developer Troubleshoots Database Issues (Priority: P2)

**As a** developer debugging production problems  
**I need** comprehensive error logging with full context and clear messages  
**So that** I can quickly identify root causes without guessing

**Why this priority**: After establishing reliable operations (P1), effective troubleshooting becomes the next critical need for maintaining the system.

**Independent Test Criteria**:

-   Trigger connection failure → review log_error entry for completeness
-   Call DAO with null required parameter → verify user-friendly message returned
-   Enable Service_DebugTracer → review output for method entry/exit with parameters
-   Trigger same error 10 times in 5 seconds → verify UI cooldown (1 MessageBox, 10 log entries)

**Acceptance Scenarios**:

1. **Comprehensive Error Logging Context**

    - **Given**: Database connection failure occurs during operation
    - **When**: Reviewing `log_error` table after error
    - **Then**: Error entry includes all required fields: User (current user), Severity (Error), ErrorType (DatabaseConnectionFailure), ErrorMessage (readable), StackTrace (full call stack), MethodName (calling DAO method), MachineName, OSVersion, AppVersion, ErrorTime (timestamp)

2. **User-Friendly vs Technical Error Separation**

    - **Given**: DAO method called with null required parameter (PartID=null)
    - **When**: Validation fails at DAO boundary
    - **Then**: User sees friendly message "Part ID is required" in UI dialog, while detailed technical info ("ArgumentNullException in Dao_Inventory.AddInventoryAsync, parameter: partId") logged to database

3. **Debug Tracing for Troubleshooting**

    - **Given**: Service_DebugTracer enabled via configuration
    - **When**: Reviewing debug output after DAO operation
    - **Then**: Output shows complete call stack with method entry (`[ENTRY] Dao_Inventory.AddInventoryAsync(partId=ABC123, quantity=10)`) and exit (`[EXIT] Dao_Inventory.AddInventoryAsync -> Success`), all parameters visible (sanitized for sensitive data like passwords)

4. **Error Message Cooldown Mechanism**
    - **Given**: Same error occurs multiple times rapidly (10 database connection failures in 5 seconds)
    - **When**: Errors are processed by Service_ErrorHandler
    - **Then**: All 10 errors logged to database with timestamps, but UI shows MessageBox only once (cooldown=5s), preventing error spam and allowing user to work

**Dependencies**: Depends on US1, US2 (DAO methods and error handling infrastructure must exist)

---

### User Story 4 - Database Administrator Maintains Consistent Schema (Priority: P2)

**As a** database administrator  
**I need** all stored procedures to follow uniform parameter naming and output conventions  
**So that** schema changes are predictable and maintenance is straightforward

**Why this priority**: Schema consistency enables the uniform DAO pattern. This supports the P1 developer experience but is secondary to having basic reliability.

**Independent Test Criteria**:

-   Query INFORMATION_SCHEMA.ROUTINES for all procedures
-   Verify all have OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500)
-   Run parameter prefix validation script → confirm 0 inconsistencies
-   Compare procedure parameter names to C# model properties → verify PascalCase match

**Acceptance Scenarios**:

1. **Uniform Parameter Prefix Standards**

    - **Given**: All 60+ stored procedures in `MTM_WIP_Application_Winforms` database
    - **When**: Querying INFORMATION_SCHEMA.PARAMETERS for parameter definitions
    - **Then**: All parameters use standard prefixes: `p_` for CRUD parameters (p_PartID, p_LocationCode), `IN` for multi-step operations (in_FromLocation, in_ToLocation), `OUT` for return values (p_Status, p_ErrorMsg), no mixed or inconsistent prefixes

2. **Mandatory Output Parameters Present**

    - **Given**: Any stored procedure in the database
    - **When**: Checking procedure signature via `SHOW CREATE PROCEDURE`
    - **Then**: Procedure declares `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)` parameters, implements proper status code logic (1=success with data, 0=success no data, -1=error, -2=validation error, -3=business logic error, -4=not found, -5=duplicate), sets appropriate status based on operation outcome

3. **C# Model Property Alignment**

    - **Given**: Stored procedure parameters and C# model classes
    - **When**: Comparing parameter names to model properties (removing p\_ prefix)
    - **Then**: Names match exactly in PascalCase (p_PartID → PartID, p_LocationCode → LocationCode, p_OperationNumber → OperationNumber), enabling automatic mapping without transformation logic

4. **Parameter Validation Script Verification**
    - **Given**: Parameter prefix validation PowerShell script
    - **When**: Executed against production database
    - **Then**: Script reports 0 inconsistencies, confirms all procedures follow conventions, generates compliance report showing 100% adherence to `00_STATUS_CODE_STANDARDS.md`

**Dependencies**: Depends on Phase 2.5 (stored procedure standardization must complete first)

---

### User Story 5 - Performance Analyst Reviews Query Execution (Priority: P3)

**As a** performance analyst  
**I need** visibility into database operation timing and connection pool metrics  
**So that** I can identify and optimize bottlenecks

**Why this priority**: Performance monitoring is important but secondary to reliability and maintainability. The system must first work correctly before optimizing speed.

**Independent Test Criteria**:

-   Execute inventory search returning 10,000+ rows → verify timing logged
-   Run 100 concurrent database operations → verify pool statistics healthy
-   Execute batch removal of 100 items in transaction → force mid-batch error → verify complete rollback
-   Review slow query logs → confirm queries >threshold flagged as warnings

**Acceptance Scenarios**:

1. **Operation Timing Logging with Thresholds**

    - **Given**: Inventory search operation returning 10,000+ rows
    - **When**: Operation completes via `Dao_Inventory.SearchInventoryAsync`
    - **Then**: Execution time logged to Service_DebugTracer, operation categorized as "Query" (threshold=500ms), any execution exceeding threshold flagged as WARNING in logs with message "Slow query detected: SearchInventoryAsync took 1250ms"

2. **Connection Pool Health Monitoring**

    - **Given**: Connection pool configured (5-100 connections)
    - **When**: Executing 100 concurrent `GetInventoryAsync` operations simultaneously
    - **Then**: Pool handles load without exhaustion (no timeout errors), pool statistics available (Active=45, Idle=5, Total=50, within healthy range), all operations complete successfully, average response time within acceptable range

3. **Transaction Rollback Verification**

    - **Given**: Batch removal operation deleting 100 inventory items within explicit transaction
    - **When**: Error occurs mid-batch (item #50 fails constraint check)
    - **Then**: Transaction rolls back completely (0 items deleted, not 49), database state unchanged from before operation, error logged with full context, user receives clear message "Batch removal failed. No items were deleted."

4. **Slow Query Detection and Reporting**
    - **Given**: Slow query monitoring enabled with category-based thresholds
    - **When**: Reviewing query execution plans and logs
    - **Then**: No full table scans occur on indexed columns, queries exceeding category threshold (Query=500ms, Modification=1000ms, Batch=5000ms, Report=2000ms) logged as warnings with execution plan, performance dashboard shows top 10 slowest operations for tuning

**Dependencies**: Depends on US1-4 (all DAOs must be refactored with performance monitoring integrated)

---

### Edge Cases & Exception Scenarios

**Unexpected Status Code Handling**

-   **Scenario**: Stored procedure returns status code not in standard range (e.g., p_Status=99)
-   **Behavior**: System logs warning "Unexpected status code 99 from procedure inv_inventory_Add", treats as error condition (equivalent to p_Status=-1), returns `Model_Dao_Result.Failure` with message "Unexpected database response. Contact support."
-   **Rationale**: Prevents undefined behavior from non-standard status codes

**Database Timeout During Long Operation**

-   **Scenario**: Query execution exceeds 30-second timeout (ConnectionTimeout configuration)
-   **Behavior**: Retry logic attempts up to 3 times with exponential backoff (2s, 4s, 8s) for transient errors (1205/1213/2006/2013), permanent failures (timeout, syntax error) return immediate error without retry, user sees message "Database operation timed out. Try narrowing your search criteria."
-   **Rationale**: Distinguishes transient (retryable) from permanent (not retryable) failures

**Recursive Error Logging Failure**

-   **Scenario**: Error occurs while attempting to log error to database (log_error table unavailable)
-   **Behavior**: `LogErrorToDatabaseAsync` catches exception in logging method, falls back to file-based logging (writes to `Logs/database-errors.log`), prevents recursive loop, application remains stable
-   **Rationale**: Last-resort error capture ensures errors never lost even during database failures

**Mixed Parameter Prefixes in Single Procedure**

-   **Scenario**: Stored procedure has both `p_` and `in_` prefixes (e.g., p_PartID, in_Quantity)
-   **Behavior**: Helper_Database_StoredProcedure queries INFORMATION_SCHEMA.PARAMETERS, detects prefix per parameter individually, applies correct prefix to each parameter in Dictionary<string, object>, works correctly with mixed prefixes
-   **Rationale**: Real-world procedures (especially transfer/transaction types) legitimately use multiple prefix conventions

**Connection Pool Exhaustion**

-   **Scenario**: All 100 connections in pool are active, 101st request arrives
-   **Behavior**: New request waits up to ConnectionTimeout (30 seconds) for available connection, if timeout expires returns error with message "Database connection pool exhausted. Too many concurrent operations.", suggests reducing concurrent requests or increasing MaxPoolSize
-   **Rationale**: Graceful degradation under extreme load rather than silent failure

**Transaction Deadlock Detection**

-   **Scenario**: Two transactions attempt to modify same inventory record simultaneously (MySQL error 1205)
-   **Behavior**: Retry logic detects deadlock error code (1205), waits with exponential backoff (2s, 4s, 8s), retries transaction up to 3 times, if all retries fail returns error with message "Unable to complete operation due to concurrent access. Please retry."
-   **Rationale**: Deadlocks are transient and often resolve on retry

**INFORMATION_SCHEMA Query Failure at Startup**

-   **Scenario**: Startup parameter cache query fails (database unreachable, insufficient permissions)
-   **Behavior**: Falls back to convention-based prefix detection (p* default, in* for Transfer*/transaction* procedures), logs warning "Parameter cache initialization failed, using fallback conventions", application continues with reduced accuracy
-   **Rationale**: Allows application to function with degraded parameter detection rather than failing startup

**Null vs Empty String Handling**

-   **Scenario**: DAO receives null vs empty string for optional parameter (LocationCode: null vs "")
-   **Behavior**: Both treated as "no value provided", converted to DBNull.Value when passed to stored procedure, stored procedure receives SQL NULL, can implement appropriate default logic
-   **Rationale**: Consistent handling of "no value" regardless of C# representation

## Requirements _(mandatory)_

### Functional Requirements

**FR-001: Standardized Helper Execution Methods**  
System MUST provide `Helper_Database_StoredProcedure` class with four execution methods that route all database access:

-   `ExecuteNonQueryWithStatusAsync` - INSERT/UPDATE/DELETE operations returning Model_Dao_Result
-   `ExecuteDataTableWithStatusAsync` - SELECT operations returning Model_Dao_Result<DataTable>
-   `ExecuteScalarWithStatusAsync<T>` - SELECT single value returning Model_Dao_Result<T>
-   `ExecuteWithCustomOutputAsync` - Non-standard outputs returning Model_Dao_Result<Dictionary<string, object>>

All methods include automatic parameter prefix detection, retry logic, and performance monitoring.

---

**FR-002: Automatic Parameter Prefix Detection**  
System MUST query INFORMATION_SCHEMA.PARAMETERS at application startup to:

-   Cache all stored procedure parameter names and prefixes in memory (ParameterPrefixCache dictionary)
-   Detect prefix per parameter (p*, in*, o\_, or none)
-   Apply detected prefix when executing stored procedures
-   Fall back to convention (p* default, in* for Transfer*/transaction*) if schema query fails
-   Allow developers to provide unprefixed parameter names in Dictionary<string, object>

Startup cache initialization completes within 100-200ms with logging of cache hit/miss rates.

---

**FR-003: Model_Dao_Result Wrapper Pattern**  
All DAO methods MUST return structured result objects:

-   **Model_Dao_Result** for non-data operations: Properties include `IsSuccess` (bool), `Message` (string), `Exception` (Exception or null)
-   **Model_Dao_Result<T>** for data operations: Extends Model_Dao_Result with `Data` (T - DataTable, DataRow, primitive, or custom type)
-   **Factory methods**: `Model_Dao_Result.Success(message)`, `Model_Dao_Result.Failure(message, exception)`, `Model_Dao_Result<T>.Success(data, message)`, `Model_Dao_Result<T>.Failure(message, exception)`

Pattern eliminates exception-driven control flow for expected database failures.

---

**FR-004: Stored Procedure Output Parameter Standards**  
All stored procedures MUST declare standardized output parameters:

-   `OUT p_Status INT` - Status code indicating operation result
-   `OUT p_ErrorMsg VARCHAR(500)` - Human-readable error or success message

Status codes follow this convention:

-   **1**: Success with data (SELECT returned rows)
-   **0**: Success with no data (SELECT returned 0 rows, or no-op operation)
-   **-1**: Database error (SQL exception, connection failure)
-   **-2**: Validation error (input validation failed)
-   **-3**: Business logic error (business rule violation)
-   **-4**: Not found error (specific record not found)
-   **-5**: Duplicate error (unique constraint violation)

---

**FR-005: Comprehensive Error Logging**  
System MUST log all database errors to `log_error` table with complete context:

-   **User** - Current authenticated user (username)
-   **Severity** - Error severity level (Critical/Error/Warning per FR-021)
-   **ErrorType** - Classification (DatabaseConnectionFailure, ValidationError, BusinessRuleViolation, etc.)
-   **ErrorMessage** - User-friendly error description
-   **StackTrace** - Full call stack for troubleshooting
-   **ModuleName** - Assembly or module name
-   **MethodName** - Calling method (DAO method name)
-   **AdditionalInfo** - Serialized parameters, database connection string (sanitized), retry attempts
-   **MachineName** - Computer name
-   **OSVersion** - Operating system version
-   **AppVersion** - Application version number
-   **ErrorTime** - Timestamp of error occurrence

Logging includes correlation ID for tracking related errors across multiple operations.

---

**FR-006: Recursive Error Logging Prevention**  
Error logging MUST prevent recursive failures by:

-   Wrapping `LogErrorToDatabaseAsync` in try/catch block
-   Catching exceptions during error logging attempt
-   Falling back to file-based logging (`Logs/database-errors.log`) when database unavailable
-   Preventing infinite loop if error logging itself causes error
-   Maintaining application stability even when logging infrastructure fails

File fallback includes same context fields as database logging for consistency.

---

**FR-007: Error Message Cooldown Mechanism**  
System MUST implement UI-level error cooldown:

-   Track last error message text and timestamp per error type
-   Suppress duplicate UI MessageBox for same error within 5 seconds (configurable cooldown period)
-   Continue logging all error occurrences to database (no suppression)
-   Reset cooldown after period expires or different error occurs
-   Provide `ClearErrorCooldownState()` method for testing

Prevents error spam while maintaining complete audit trail.

---

**FR-008: Connection Pooling Configuration**  
System MUST use connection pooling for all database connections with:

-   `MinPoolSize=5` - Minimum warm connections maintained
-   `MaxPoolSize=100` - Maximum concurrent connections allowed
-   `ConnectionTimeout=30` seconds - Maximum wait for available connection
-   Pool statistics available via diagnostic API (active, idle, total connections)
-   Automatic connection lifecycle management (no manual disposal required)

Connection string centralized in `Helper_Database_Variables.GetConnectionString()`.

---

**FR-009: Transient Error Retry Logic**  
System MUST implement automatic retry for transient database errors:

-   Retry up to 3 attempts for transient error codes: 1205 (deadlock), 1213 (lock wait timeout), 2006 (server gone away), 2013 (lost connection during query)
-   Use exponential backoff delay: 2 seconds, 4 seconds, 8 seconds
-   Log each retry attempt with attempt number and delay
-   Return permanent failure if all retries exhausted
-   Skip retry for non-transient errors (syntax error, validation error, constraint violation)

Retry logic integrated into all four Helper execution methods.

---

**FR-010: Async-Only DAO Architecture**  
All new DAO methods MUST be purely asynchronous:

-   Method names end with `Async` suffix
-   Return `Task<Model_Dao_Result>` or `Task<Model_Dao_Result<T>>`
-   No `useAsync` parameter (removed from all methods)
-   All calling code (Forms event handlers, Services, Controls) must use `async/await` patterns
-   No synchronous wrapper class provided - forces immediate async migration

WinForms event handlers use `async void` pattern for compatibility.

---

**FR-011: Explicit Transaction Management**  
All multi-step operations MUST use explicit database transactions:

-   Operations involving multiple stored procedure calls (inventory transfer = deduct + add + log)
-   Batch modifications (delete 100 records)
-   User creation with role assignment (create user + assign role)
-   Use `MySqlConnection.BeginTransaction()` before first operation
-   Call `Commit()` after all operations succeed
-   Call `Rollback()` if any operation fails
-   Wrap in try/catch/finally for guaranteed cleanup

Ensures atomic operations - all steps succeed or all steps rollback.

---

**FR-012: Service_DebugTracer Integration**  
All DAO methods MUST integrate Service_DebugTracer for diagnostics:

-   Call `TraceMethodEntry(methodName, parameters)` at method start
-   Call `TraceMethodExit(methodName, result)` before return
-   Log all input parameters (sanitize sensitive data like passwords)
-   Log operation result (success/failure, row counts, data summaries)
-   Include execution timing in trace output
-   Enable/disable via configuration (Service_DebugTracer.IsEnabled)

Provides complete audit trail for troubleshooting production issues.

---

**FR-013: Centralized Connection String Management**  
System MUST centralize database connection strings:

-   All connection strings retrieved via `Helper_Database_Variables.GetConnectionString()`
-   No hardcoded credentials in code files
-   Environment-aware selection (Debug → test database, Release → production database)
-   Connection string includes: Server, Port, Database, User, Password, SslMode, AllowPublicKeyRetrieval, MinPoolSize, MaxPoolSize, ConnectionTimeout
-   Support for multiple connection string profiles (production, test, development)

Enables easy environment switching and credential rotation.

---

**FR-014: Startup Database Validation**  
System MUST validate database connectivity before loading MainForm:

-   Call `Dao_System.CheckConnectivityAsync()` during `Program.cs` startup
-   Display actionable error message if database unavailable ("Unable to connect to database at localhost:3306. Check network connection.")
-   Terminate gracefully with exit code 1 if validation fails
-   Log startup validation result with timing
-   Prevent partial application initialization without database

Ensures application never loads with non-functional database layer.

---

**FR-015: Uniform DAO Structure**  
All DAO classes MUST follow identical structure:

-   All methods are `async` returning `Task<Model_Dao_Result>` or `Task<Model_Dao_Result<T>>`
-   All methods route through `Helper_Database_StoredProcedure` (no direct MySqlConnection/MySqlCommand)
-   Parameters match stored procedure signature exactly (PascalCase names, types aligned)
-   XML documentation on all public methods (summary, param, returns, exception tags)
-   Region organization: #region Initialization, #region Public Methods, #region Private Methods, #region Dispose

Structure enables code generation and enforces consistency.

---

**FR-016: Elimination of Direct MySQL API Usage**  
System MUST eliminate direct MySQL API usage in Data/ folder:

-   No `MySqlConnection` instantiation in DAO classes
-   No `MySqlCommand` creation in DAO classes
-   No `MySqlDataAdapter` or `MySqlDataReader` usage in DAO classes
-   All database access routes through `Helper_Database_StoredProcedure` execution methods
-   Static code analysis validates 100% compliance (verified during T085 validation task)

Enforces architectural boundary and enables centralized monitoring/logging.

---

**FR-017: Parameter Naming Alignment**  
Stored procedure parameters MUST align with C# model properties:

-   Use PascalCase for all parameter names (PartID, LocationCode, OperationNumber, ItemType)
-   Remove prefix when matching to C# properties (p_PartID → PartID)
-   Consistent naming across all procedures (no synonyms: Location vs LocationCode)
-   Type alignment (VARCHAR → string, INT → int, DECIMAL → decimal, DATETIME → DateTime)
-   Nullable parameters use nullable types in C# (int? for optional OperationNumber)

Enables automatic mapping and reduces transformation logic.

---

**FR-018: Integration Test Infrastructure**  
System MUST provide separate test database for integration testing:

-   **Test database name**: `mtm_wip_application_winform_test` (singular "winform")
-   Schema-only copy synchronized with production via migration scripts
-   Test classes inherit from `BaseIntegrationTest` with transaction management
-   `[TestInitialize]` begins transaction, inserts test data
-   `[TestCleanup]` rolls back transaction for clean slate
-   Tests isolated (no interference between test runs)
-   Parallel-safe (each developer has independent test database)

Enables fast, reliable integration tests without affecting production data.

---

**FR-019: Application Termination Policy**  
System MUST handle database errors based on operation context:

-   **Startup**: Terminate application gracefully with exit code 1 if database unavailable (after logging)
-   **Critical errors during operation**: Show retry prompt dialog ("Connection lost. Retry?") allowing user to retry or close app
-   **Preserve in-progress work**: Cache unsaved user input before showing error dialog
-   **Graceful degradation**: For non-critical operations, log error and continue operation (e.g., analytics failure doesn't block inventory addition)

Balances user experience with data integrity requirements.

---

**FR-020: Performance Monitoring with Configurable Thresholds**  
System MUST measure and log execution time for all database operations:

-   Categorize operations by type: Query (SELECT with filters), Modification (INSERT/UPDATE/DELETE), Batch (multiple row operations), Report (aggregation/analytics)
-   Define threshold per category: Query=500ms, Modification=1000ms, Batch=5000ms, Report=2000ms
-   Log operations exceeding threshold as WARNING with details: operation name, parameters (sanitized), execution time, category, threshold
-   Store thresholds in `Model_Application_Variables` or appsettings for easy adjustment
-   Provide performance dashboard via Service_DebugTracer showing top 10 slowest operations

Enables proactive performance optimization and bottleneck identification.

---

**FR-021: Three-Tier Error Severity Classification**  
System MUST classify all errors into three severity tiers:

-   **Critical**: Data integrity risk or application cannot continue (database corruption, schema mismatch, OutOfMemoryException) → Log and terminate gracefully
-   **Error**: Operation failed, user sees error message (constraint violation, connection failure, validation error) → Log, show error dialog, allow retry
-   **Warning**: Unexpected but handled, no user impact (slow query, retry succeeded, fallback to convention) → Log only, continue operation

Severity level influences logging detail, user notification, and application behavior.

---

**FR-022: Verbose Test Failure Diagnostics**  
Integration tests MUST output comprehensive diagnostic information on failure:

-   Full exception message and stack trace
-   All input parameters (name/value pairs) passed to stored procedure
-   Expected output values (status codes, specific data rows)
-   Actual output values received
-   Procedure execution time in milliseconds
-   Relevant database state (row counts in affected tables before/after)
-   Test method name and timestamp
-   Format as structured JSON block for easy parsing

Enables rapid diagnosis without re-running tests or attaching debugger, critical during Phase 2.5 refactoring when test failures common.

---

**FR-023: Developer Role and Development Tools Access**  
System MUST implement Developer user role with restricted access to diagnostic tools:

-   Role hierarchy: Basic User < Admin < Developer (Developer inherits all Admin permissions)
-   Developer role requires Admin as prerequisite (cannot be granted independently)
-   Settings Form includes new "Development" TreeView category visible only to Developer role users
-   Development tools include: Parameter Prefix Maintenance, performance baseline tools, database connection diagnostics, stored procedure call history viewer, log file viewer with filtering, cache inspection and refresh tools
-   User management form updated with Developer checkbox (enabled only if Admin checked)
-   Role validation in Control base constructors for all Development tools

Prevents accidental exposure of diagnostic tools to regular users while providing power-user capabilities for developers.

---

**FR-024: Concurrent Documentation Update System**  
Documentation updates MUST occur concurrently with code/procedure refactoring:

-   When refactoring stored procedure, developer MUST immediately update: (1) Procedure header comments, (2) DAO XML documentation for calling method, (3) 00_STATUS_CODE_STANDARDS.md examples if procedure demonstrates new pattern, (4) quickstart.md if procedure commonly used
-   Documentation Update Matrix (`Documentation-Update-Matrix.md`) maps each T113-T118 refactoring task to required documentation updates
-   Matrix structure: Markdown table with columns: Task ID, Procedure Name, Header Comments (link), DAO XML Docs (link), Standards Update (Required/N/A), Quickstart Update (Required/N/A), Status (⬜/🔄/✅/⚠️)
-   T113-T118 task definitions include documentation checkboxes as deliverable requirements
-   Matrix becomes single source of truth for documentation completion tracking

Prevents documentation drift and reduces end-of-phase documentation burden by integrating documentation as part of development workflow.

---

**FR-025: Schema Drift Detection and Re-Audit System**  
System MUST detect and reconcile production database changes during Phase 2.5 implementation:

-   T101 baseline audit timestamped and versioned
-   T119b re-audit production procedures before test deployment to capture drift
-   Drift report categorizes each changed procedure: (A) Independent hotfix - preserve production logic, apply standards separately, (B) Conflicting change - manual three-way merge required, (C) New procedure - full Phase 2.5 refactoring required
-   Separate tasks handle each category: T119c refactors Category A, T119d merges Category B conflicts, T119e refactors Category C
-   T120 deployment uses post-reconciliation procedure set (refactored baseline + integrated production changes)
-   Reconciliation report documents all drift handling decisions as Phase 2.5 appendix

Allows emergency hotfixes during Phase 2.5 (15-25 days) without blocking critical business operations while maintaining refactoring progress.

---

**FR-026: CSV Transaction Analysis and Review Workflow**  
T103 audit MUST generate CSV file with procedure transaction strategy recommendations:

-   CSV structure: `ProcedureName, DetectedPattern, RecommendedStrategy, Confidence, Rationale, DeveloperCorrection, RefactoringNotes`
-   Detected patterns: single-step, multi-step, batch, reporting
-   Recommended strategies: explicit transaction, implicit transaction, none
-   Confidence levels: High/Medium/Low based on code analysis
-   Rationale explains why recommendation made based on detected code patterns
-   DeveloperCorrection column initially empty for developer review
-   Git-based review workflow: Commit CSV, assign domains to developers, developers fill corrections, create PR, peer review, merge
-   T106a task gates T113: CSV review and correction complete before refactoring begins
-   T114-T118 refactoring uses corrected CSV as authoritative source for transaction management decisions

Structured documentation ensures correct transaction handling decisions with peer review validation before implementation.

---

**FR-027: Roslyn Analyzer for Database Access Compliance**  
Custom Roslyn analyzer MUST enforce Helper routing compliance at compile-time:

-   Analyzer package name: `MTM.CodeAnalysis.DatabaseAccess`
-   Diagnostic rules: (1) Flag `new MySqlConnection()` outside Helper_Database_StoredProcedure.cs, (2) Flag `new MySqlCommand()` outside Helper classes, (3) Flag `MySqlDataAdapter`/`MySqlDataReader` outside Helper classes, (4) Provide code fix suggestions redirecting to Helper_Database_StoredProcedure methods
-   Phased severity enforcement: v1.0.0 emits Warnings during Phase 2.5 (allows refactoring), v2.0.0 emits Errors post-Phase 2.5 (prevents regression)
-   IDE integration: Real-time feedback (red squiggles) during development
-   CI/CD integration: Analyzer runs on every build, v2.0.0 blocks PR merge if violations detected

Prevents direct MySQL API usage violations during development, not just detection during validation phase.

---

**FR-028: Parameter Prefix Override Database Table**  
System MUST persist parameter prefix overrides in database for multi-user access:

-   Table name: `sys_parameter_prefix_override`
-   Columns: OverrideID (PK), ProcedureName, ParameterName, DetectedPrefix, OverridePrefix, Confidence, Reason, CreatedBy (User ID), CreatedDate, ModifiedBy, ModifiedDate, IsActive
-   Unique constraint: (ProcedureName, ParameterName)
-   Cache loads overrides from table at startup, merges with schema detection results
-   Parameter Prefix Maintenance form performs CRUD operations on override table
-   Export/import functionality allows transferring overrides between environments
-   Audit trail tracks who made changes and when for troubleshooting

Provides persistent multi-user access to prefix overrides with complete audit trail, allows DBA to review override patterns and fix root causes in stored procedures.

---

**FR-029: Startup Parameter Prefix Retry Strategy**  
Application startup MUST implement retry dialog for parameter prefix cache initialization failure:

-   INFORMATION_SCHEMA query failure shows MessageBox: "Failed to load database parameter metadata (Attempt X of 3). [Retry] [Quit]"
-   Display remaining attempts on each retry
-   After 3rd failed retry, show final message: "Unable to connect to database after 3 attempts. Application will close. Please check database connectivity and try again."
-   Application terminates if cache initialization fails (no fallback to convention-based guessing)
-   Logging captures retry attempts and final failure reason

Prevents application from running with incomplete/fallback parameter data which could cause subtle bugs, ensures 100% accurate parameter metadata or refuses to start.

---

### Key Entities

**Model_Dao_Result**  
Non-generic wrapper class for operations that don't return data.

-   **IsSuccess** (bool): True if operation succeeded, false if failed
-   **Message** (string): Human-readable outcome description
-   **Exception** (Exception?): Original exception if operation failed, null if succeeded
-   **Factory Methods**: `Success(message)`, `Failure(message, exception)`
-   **Usage**: INSERT, UPDATE, DELETE operations, multi-step transactions

---

**Model_Dao_Result<T>**  
Generic wrapper class for operations returning data, extends Model_Dao_Result.

-   **Data** (T): Query results (DataTable, DataRow, string, int, custom model)
-   **Inherits**: IsSuccess, Message, Exception from Model_Dao_Result
-   **Factory Methods**: `Success(data, message)`, `Failure(message, exception)`
-   **Usage**: SELECT operations, scalar queries, custom output procedures

---

**Helper_Database_StoredProcedure**  
Central helper class providing standardized stored procedure execution.

-   **ExecuteNonQueryWithStatusAsync**: Returns Model_Dao_Result for INSERT/UPDATE/DELETE
-   **ExecuteDataTableWithStatusAsync**: Returns Model_Dao_Result<DataTable> for SELECT
-   **ExecuteScalarWithStatusAsync<T>**: Returns Model_Dao_Result<T> for scalar SELECT
-   **ExecuteWithCustomOutputAsync**: Returns Model_Dao_Result<Dictionary<string, object>> for custom outputs
-   **Parameter Prefix Detection**: Queries INFORMATION_SCHEMA at startup, applies prefixes automatically
-   **Retry Logic**: Handles transient errors (1205, 1213, 2006, 2013) with exponential backoff
-   **Performance Monitoring**: Logs execution time, flags slow queries per category threshold

---

**ParameterPrefixCache**  
Internal dictionary structure caching parameter prefixes from INFORMATION_SCHEMA.

-   **Key**: Stored procedure name (e.g., "inv_inventory_Add")
-   **Value**: Dictionary<string, string> mapping parameter name to prefix
-   **Example**: `{ "PartID": "p_", "Quantity": "p_", "FromLocation": "in_", "ToLocation": "in_" }`
-   **Initialization**: Populated at application startup via `Program.cs`
-   **Fallback**: Uses convention (p* default, in* for Transfer*/transaction*) if query fails

---

**DAO Classes (12 Total)**  
Data Access Object classes providing domain-specific database operations:

-   **Dao_Inventory**: Inventory add/remove/transfer/search operations
-   **Dao_User**: User authentication, settings, preferences
-   **Dao_Transactions**: Transaction history logging and retrieval
-   **Dao_Part**: Part master data management
-   **Dao_Location**: Location master data management
-   **Dao_Operation**: Operation number master data management
-   **Dao_ItemType**: Item type master data management
-   **Dao_QuickButtons**: User quick button preferences
-   **Dao_History**: Historical data queries (inventory/remove/transfer history)
-   **Dao_ErrorLog**: Error logging operations
-   **Dao_System**: System metadata, roles, themes, access control
-   **Common Structure**: All use async methods, Model_Dao_Result pattern, route through Helper

---

**Stored Procedures (60+ Total)**  
Database procedures organized by domain with standardized parameters/outputs:

-   **Inventory Operations** (~15 procedures): inv_inventory_Add, inv_inventory_Remove, inv_inventory_Transfer_Part, inv_inventory_Transfer_Quantity, inv_inventory_Get_ByPartID, etc.
-   **Transaction Logging** (~5 procedures): inv_transaction_Add, inv_transactions_GetAnalytics, inv_transactions_SmartSearch, etc.
-   **User Management** (~10 procedures): sys_user_GetByName, sys_user_Authenticate, sys_user_settings_Update, etc.
-   **Role Management** (~5 procedures): sys_role_GetIdByName, sys_user_role_Add, sys_user_role_Remove, etc.
-   **Master Data** (~20 procedures): md_part_ids_Add, md_locations_Add, md_operation_numbers_Add, md_item_types_Add, and corresponding Get/Update/Delete variants
-   **Error Logging** (~5 procedures): log_error_Add, log_error_Get_All, log_error_Get_ByUser, log_error_Delete, etc.
-   **Quick Buttons** (~10 procedures): sys_last_10_transactions_AddQuickButton, sys_last_10_transactions_Get_ByUser, sys_last_10_transactions_Update_ByUserAndPosition, etc.

All procedures follow `00_STATUS_CODE_STANDARDS.md` with OUT p_Status and OUT p_ErrorMsg.

---

**Connection Pool**  
MySql.Data connection pool managing database connections efficiently.

-   **Configuration**: MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30s
-   **Lifecycle**: Automatic creation, maintenance, disposal
-   **Statistics**: Available via diagnostic API (Active connections, Idle connections, Total connections)
-   **Behavior**: Warm connections ready for immediate use, queues requests when pool exhausted, throws timeout error after 30 seconds

---

**Error Log Entry**  
Database record in log_error table capturing comprehensive error context.

-   **Schema**: User, Severity, ErrorType, ErrorMessage, StackTrace, ModuleName, MethodName, AdditionalInfo, MachineName, OSVersion, AppVersion, ErrorTime
-   **Purpose**: Troubleshooting production issues, audit trail, compliance
-   **Retention**: Configurable (default 90 days), indexed on ErrorTime and User for fast queries
-   **Access**: via Dao_ErrorLog methods (GetAll, GetByUser, GetByDateRange, Delete)

---

**Transaction History**  
Record in inv_transaction table tracking all inventory movements for audit.

-   **Schema**: TransactionType (IN/OUT/TRANSFER), PartID, FromLocation, ToLocation, Operation, Quantity, User, ItemType, BatchNumber, Notes, TransactionDate
-   **Purpose**: Compliance audit trail, inventory analytics, troubleshooting
-   **Indexed**: TransactionDate, PartID, User for fast historical queries
-   **Immutable**: Records never updated or deleted (append-only for integrity)

---

## Success Criteria _(mandatory)_

### Measurable Outcomes

**SC-001: Zero MySQL Parameter Errors**  
After refactor completion, zero MySQL parameter errors occur in application logs.

-   **Current State**: Parameter prefix errors occur regularly (p* vs in* mismatch)
-   **Target State**: 0 parameter errors in 30-day post-deployment period
-   **Validation**: Query log_error table for ErrorType="MySQLParameterError", confirm count=0
-   **Timeline**: Measured continuously starting from deployment date

---

**SC-002: 100% Helper Routing Compliance**  
All database operations in Data/ folder route through Helper_Database_StoredProcedure with no direct MySQL API usage.

-   **Validation Method**: Static code analysis script scans all \*.cs files in Data/ for MySqlConnection, MySqlCommand, MySqlDataAdapter patterns
-   **Target**: 0 direct usages found
-   **Report**: Generates compliance report listing any violations with file path and line number
-   **Enforcement**: T085 validation task in tasks.md

---

**SC-003: Comprehensive Stored Procedure Testing**  
All 60+ stored procedures successfully tested with valid and invalid inputs, returning consistent Model_Dao_Result responses.

-   **Test Coverage**: Each procedure has ≥4 test methods (success with data, success no data, validation error, database error)
-   **Pass Rate**: 100% of tests passing
-   **Validation**: Run all integration tests, generate test results report
-   **Scope**: Covers all procedures discovered in Phase 2.5 stored procedure audit

---

**SC-004: Performance Baseline Maintained**  
Database operations complete within 5% performance variance compared to pre-refactor baseline.

-   **Baseline**: Establish before refactor begins (10 key operations: inventory add, search, transfer, user auth, etc.)
-   **Measurement**: Execute same operations post-refactor, compare execution times
-   **Acceptable Variance**: ±5% (acknowledges measurement noise and minor overhead from Model_Dao_Result wrapping)
-   **Tool**: Benchmark suite using Stopwatch for precise timing

---

**SC-005: Connection Pool Health Under Load**  
Connection pool maintains healthy 5-100 connection range under load testing with 100 concurrent operations.

-   **Load Test**: Execute 100 concurrent Dao_Inventory.GetAllInventoryAsync operations simultaneously
-   **Monitoring**: Track pool statistics (Active, Idle, Total connections) during test
-   **Success Criteria**: No timeout errors, no connection exhaustion, pool stabilizes within 5-100 range
-   **Validation**: ConnectionPooling_Tests.cs in integration test suite

---

**SC-006: Recursive Error Logging Prevention**  
Error logging successfully captures and records all database errors without recursive failures.

-   **Test Scenario**: Simulate log_error table unavailability (rename table temporarily)
-   **Expected Behavior**: LogErrorToDatabaseAsync catches exception, falls back to file logging (Logs/database-errors.log)
-   **Success**: Application remains stable, error details captured in file, no infinite loop
-   **Validation**: ErrorLogging_Tests.cs recursive prevention test

---

**SC-007: Developer Productivity - New Operation Under 15 Minutes**  
Developer can implement new database operation following standard pattern in under 15 minutes.

-   **Test**: Follow quickstart.md guide to create new stored procedure and DAO method
-   **Steps**: Create stored procedure with standard parameters, add DAO method, write integration test, execute test
-   **Timer**: Start when developer reads quickstart.md, stop when test passes
-   **Acceptance**: ≥90% of developers complete in <15 minutes (measured across team)

---

**SC-008: 90% Reduction in Database-Related Tickets**  
90% reduction in database-related support tickets and bug reports within first month after deployment.

-   **Baseline**: Query ticket system for past 3 months, count database-related tickets (keyword search: MySQL, parameter, connection, stored procedure, DAO)
-   **Measurement**: Track 30 days post-deployment, count same keywords
-   **Target**: ≥90% reduction (if baseline=50 tickets/month, target ≤5 tickets/month)
-   **Review**: Monthly trend analysis to ensure sustained improvement

---

**SC-009: Zero Orphaned Records from Transaction Failures**  
All multi-step operations properly roll back when any step fails, with zero orphaned records in database.

-   **Test**: Execute TransferInventoryAsync (3-step operation: deduct from source, add to destination, log transaction)
-   **Failure Injection**: Force step 2 (add to destination) to fail with constraint violation
-   **Validation**: Query inventory tables, confirm source quantity unchanged (rollback successful), no partial transaction logs
-   **Scope**: Test all multi-step operations (transfers, batch modifications, user creation with roles)

---

**SC-010: Sub-3-Second Startup Database Validation**  
Application startup validates database connectivity and displays actionable error message within 3 seconds if database unavailable.

-   **Test 1 (Database Available)**: Measure time from Program.cs start to MainForm.Load event
-   **Test 2 (Database Unavailable)**: Stop MySQL service, launch application, measure time to error dialog display
-   **Success Criteria**: Both scenarios complete within 3 seconds
-   **Error Message Quality**: Message includes server address, port, and troubleshooting guidance (not raw exception)
-   **User Experience**: Prevents confusion from half-loaded application with broken database layer

---

**SC-011: Verbose Test Failure Output Quality**  
Integration test failures must include comprehensive diagnostic information in JSON format.

-   **Test 1**: Force test failure, verify output includes all required fields (exception, parameters, expected vs actual, execution time, database state)
-   **Test 2**: Parse JSON output programmatically, validate structure completeness
-   **Success Criteria**: 100% of test methods use base assertion helper producing structured diagnostics, all 7 required fields present on failure
-   **Developer Experience**: Failure output sufficient to diagnose issue without re-running test or attaching debugger
-   **Format Consistency**: All test failures follow identical JSON schema for tooling integration

---

**SC-012: Developer Role Access Control**  
Development tools must be accessible only to users with Developer role (Admin + Developer privileges).

-   **Test 1 (Basic User)**: Log in as Basic User, verify Development TreeView node not visible in Settings Form
-   **Test 2 (Admin without Developer)**: Log in as Admin user without Developer flag, verify Development node not visible
-   **Test 3 (Developer Role)**: Log in as user with Admin + Developer flags, verify Development node visible and Parameter Prefix Maintenance form accessible
-   **Success Criteria**: Zero unauthorized access, all Development tools gated by role check
-   **Security**: Role validation enforced in Control base constructors, cannot be bypassed

---

**SC-013: Documentation Update Matrix Completeness**  
All refactored procedures must have corresponding documentation updates tracked and validated.

-   **Test 1 (Matrix Generation)**: T129 generates Documentation-Update-Matrix.md with all T113-T118 procedures listed, file path links clickable
-   **Test 2 (Concurrent Updates)**: Developer refactors procedure, updates matrix status, validation confirms documentation files modified
-   **Test 3 (Completion Validation)**: T131 validation script passes only when all "Required" cells show "✅ Complete" status
-   **Success Criteria**: Zero undocumented procedures, 100% documentation synchronization with code changes
-   **Audit Trail**: Matrix commits show documentation progress alongside code commits

---

**SC-014: Schema Drift Detection Accuracy**  
System must detect and categorize all production database changes during Phase 2.5 implementation.

-   **Test 1 (Hotfix Detection)**: Add new procedure to production during Phase 2.5, T119b re-audit detects and flags as Category C (new procedure)
-   **Test 2 (Modification Detection)**: Modify existing procedure in production, T119b detects and flags as Category A or B based on conflict status
-   **Test 3 (Reconciliation Completeness)**: Drift report accounts for all changes, no procedures missing from categorization
-   **Success Criteria**: 100% drift detection accuracy, all changes categorized and handled before T120 deployment
-   **Safety**: Prevents deploying stale procedures that overwrite critical production hotfixes

---

**SC-015: CSV Transaction Analysis Coverage**  
Procedure transaction analysis CSV must cover all procedures with accurate recommendations.

-   **Test 1 (Coverage)**: T103 generates CSV with row for every stored procedure in database, no missing procedures
-   **Test 2 (Recommendation Quality)**: Review 20 procedures manually, verify DetectedPattern and RecommendedStrategy match code analysis
-   **Test 3 (Correction Workflow)**: Developer fills DeveloperCorrection column, commits via PR, T113 implementation references corrected CSV
-   **Success Criteria**: 100% procedure coverage, ≥90% detection accuracy (High/Medium confidence), developer corrections incorporated before refactoring
-   **Validation Gate**: T106a CSV review complete blocks T113 from starting until all procedures reviewed

---

**SC-016: Roslyn Analyzer Enforcement**  
Custom Roslyn analyzer must detect and prevent direct MySQL API usage violations.

-   **Test 1 (Violation Detection)**: Add `new MySqlConnection()` in DAO class, verify analyzer shows warning (Phase 2.5) or error (post-Phase 2.5)
-   **Test 2 (Helper Exemption)**: Verify analyzer allows `new MySqlConnection()` in Helper_Database_StoredProcedure.cs without warning
-   **Test 3 (Code Fix Suggestions)**: Trigger analyzer rule, verify code fix suggestion offers Helper_Database_StoredProcedure alternative
-   **Test 4 (CI/CD Integration)**: Build with violations present, verify CI/CD pipeline fails post-Phase 2.5 (v2.0.0 analyzer)
-   **Success Criteria**: Zero false positives, 100% violation detection, code fixes functional and idiomatic
-   **Regression Prevention**: Post-Phase 2.5, impossible to merge PR with direct MySQL API usage

---

**SC-017: Parameter Prefix Override Persistence**  
Parameter prefix overrides must persist in database with complete audit trail.

-   **Test 1 (CRUD Operations)**: Create override via maintenance form, verify saved to sys_parameter_prefix_override table
-   **Test 2 (Cache Integration)**: Add override, restart application, verify cache loads override and applies to procedure call
-   **Test 3 (Audit Trail)**: Create override, modify override, verify CreatedBy/CreatedDate and ModifiedBy/ModifiedDate populated correctly
-   **Test 4 (Multi-User Access)**: User A creates override, User B logs in, verify User B sees override in maintenance form
-   **Success Criteria**: 100% persistence across restarts, audit trail complete for all CRUD operations, no lost overrides
-   **Export/Import**: Verify export/import functionality transfers overrides between test and production environments

---

**SC-018: Startup Retry Strategy Behavior**  
Application must handle parameter prefix cache initialization failure with clear retry options.

-   **Test 1 (First Failure)**: Block INFORMATION_SCHEMA access, launch app, verify MessageBox shows "Attempt 1 of 3" with Retry/Quit buttons
-   **Test 2 (Retry Success)**: Click Retry, unblock access, verify application continues startup successfully
-   **Test 3 (Exhausted Retries)**: Block access, exhaust 3 retries, verify application terminates with clear message
-   **Test 4 (Logging)**: Review logs after failed retry sequence, verify all attempts logged with failure reasons
-   **Success Criteria**: Zero crashes during retry sequence, clear user feedback on every attempt, accurate attempt counter, clean termination after 3 failures
-   **User Experience**: User understands what failed and has clear action path (fix database connectivity)

---

## Assumptions

1. **MySQL 5.7 Compatibility**: All stored procedures and queries must remain compatible with MySQL 5.7.24+ (no use of MySQL 8.0+ features like CTEs or window functions)

2. **MAMP Development Environment**: Local development uses MAMP MySQL installation on localhost:3306 with root/root credentials

3. **Windows Platform Primary**: Application primarily targets Windows 10/11 desktop environments (WinForms dependency)

4. **Manual Testing Primary QA**: Integration tests supplement but don't replace manual validation testing by users

5. **Single Database Server**: No horizontal scaling or database clustering - single MySQL server handles all operations

6. **English-Only Interface**: Error messages, logging, and documentation in English only (no localization required)

7. **No ORM Framework**: Direct stored procedure access preferred over Entity Framework or other ORMs for performance and control

8. **Existing Schema Preserved**: Database schema changes limited to stored procedure modifications only (no table structure changes)

9. **.NET 8.0 Available**: Target environment has .NET 8.0 runtime installed

10. **Network Reliability**: Database server accessible via network connection with <100ms latency for LAN environments

---

## Out of Scope

-   **Horizontal Database Scaling**: Multi-server database clusters, read replicas, or sharding strategies
-   **MySQL Version Upgrade**: Migration from MySQL 5.7 to MySQL 8.0 (future enhancement)
-   **ORM Integration**: Entity Framework, Dapper, or other ORM frameworks
-   **Cross-Platform Support**: Linux or macOS desktop builds (WinForms Windows-only)
-   **Automated UI Testing**: Selenium, WinAppDriver, or UI automation frameworks
-   **Database Schema Changes**: Adding/removing tables, columns, or indexes (stored procedures only)
-   **Real-Time Sync**: WebSocket or SignalR for multi-user real-time data synchronization
-   **Offline Mode**: Local SQLite database for offline operation
-   **Data Migration Tools**: ETL processes or bulk data migration utilities
-   **Advanced Analytics**: Built-in reporting dashboards or BI tool integration
-   **Multi-Language Support**: Localization or internationalization (i18n)
-   **Mobile Applications**: iOS or Android clients for inventory management
-   **Web Interface**: ASP.NET Core web portal or REST API for external access
-   **Audit Log Viewer**: Dedicated UI for browsing transaction history (use existing queries)
-   **Automated Performance Tuning**: AI-driven query optimization or index recommendations

---

**Document Version**: 1.0 (Refresh)  
**Last Updated**: 2025-10-15  
**Review Cycle**: After Phase 2.5 completion, review for updates based on stored procedure audit findings
