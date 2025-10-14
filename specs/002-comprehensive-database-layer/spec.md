# Feature Specification: Comprehensive Database Layer Refactor

**Feature Branch**: `002-comprehensive-database-layer`  
**Created**: 2025-10-13  
**Status**: Draft  
**Input**: User description: "Comprehensive Database Layer Refactor"

## Clarifications

### Session 2025-10-13

- **Q1: Async Execution Mode Strategy** → **A: Async-only for new DAO methods, create separate sync wrapper class for legacy code** - Clean separation with new code purely async while legacy code uses DaoLegacy wrapper class with `.GetAwaiter().GetResult()` internally. Clear migration path with deprecation timeline during 3-6 month transition period.

- **Q2: Application Termination Policy for Database Errors** → **A: Terminate on startup, show retry prompt to user during operation** - Startup failure prevents app initialization without database. During operation, user sees "Connection lost. Retry?" dialog and decides whether to retry or close app, preserving in-progress work where possible.

- **Q3: Slow Query Threshold Configuration** → **A: Configurable per operation type (queries vs modifications vs batch vs reports)** - Define categories with thresholds: "Query" (500ms), "Modification" (1000ms), "Batch" (5000ms), "Report" (2000ms). Configuration stored in Model_AppVariables or appsettings file.

- **Q4: Transaction Scope and Multi-Step Operation Policy** → **A: All multi-step operations MUST use explicit transactions** - Maximum data integrity approach covering all scenarios. Every multi-call DAO method includes transaction management with proper rollback on any step failure.

- **Q5: Error Logging Severity Classification** → **A: Three-tier severity (Critical / Error / Warning) with documented criteria** - Critical: data integrity risk or app cannot continue. Error: operation failed, user sees error message. Warning: unexpected but handled, no user impact. Aligns with industry standards for effective triage.

- **Q6: DaoLegacy Wrapper Class Scope** → **A: No DaoLegacy wrapper; all calling code must migrate to async immediately** - Forces best practices everywhere immediately. Clean, modern codebase with no technical debt. All Forms event handlers, Services, and background workers updated to async patterns. Higher upfront migration cost but eliminates ongoing wrapper maintenance and ensures full async adoption from day one.

- **Q7: Parameter Prefix Auto-Detection Strategy** → **A: Query INFORMATION_SCHEMA.PARAMETERS at startup with fallback to convention** - 100% accurate parameter prefix information by querying database schema directly. One-time startup cost (~100-200ms) to query all procedures and cache results. Handles all prefix types (p_, in_, o_) automatically. Fallback to convention (p_ default, in_ for Transfer*/transaction* procedures) if schema query fails. Industry-standard approach providing robust parameter matching.

- **Q8: Integration Test Database Management** → **A: Schema-only copy with per-test-class transactions** - Each developer maintains local `mtm_wip_application_winform_test` database with schema matching production. Test classes begin transaction, insert test data, run assertions, then roll back for clean slate. Fast execution (transactions cheaper than full resets), isolated tests (no interference between runs), parallel-safe (each developer independent). Schema synchronized via migration scripts. Note: Test database name is `mtm_wip_application_winform_test` (not `mtm_wip_application_test` which is already in use).

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Developer Adds New Database Operation (Priority: P1)

As a developer adding new inventory features, I need a standardized DAO pattern that eliminates parameter prefix errors and provides consistent error handling, so that I can implement new database operations quickly without encountering MySQL errors.

**Why this priority**: This is the foundation that enables all other database work. Without a uniform pattern, every new feature risks introducing MySQL parameter errors that currently plague the system.

**Independent Test**: Create a new stored procedure with standard parameters, generate a corresponding DAO method following the standard pattern, execute it with valid and invalid data, and verify consistent DaoResult responses with proper error logging.

**Acceptance Scenarios**:

1. **Given** a new stored procedure `test_operation_Add` exists with standard parameters, **When** developer creates DAO method using standard pattern and calls it with valid data, **Then** method returns DaoResult.Success with confirmation message
2. **Given** the same DAO method, **When** developer calls it with invalid/missing parameters, **Then** method returns DaoResult.Failure with user-friendly error message and logs error to database
3. **Given** the DAO method encounters a database connection failure, **When** execution is attempted, **Then** method returns DaoResult.Failure without crashing application and logs detailed error information

---

### User Story 2 - Application Executes Reliable Database Operations (Priority: P1)

As the manufacturing application, I need all database operations to handle errors gracefully with consistent status processing, so that users experience stable behavior without unexpected crashes or data corruption.

**Why this priority**: This directly addresses the current problem of non-uniform DAO files causing MySQL errors. Reliability is critical for manufacturing operations where data accuracy is essential.

**Independent Test**: Execute 100 consecutive inventory additions, force a mid-operation database disconnect, attempt operations with malformed parameters, and verify all scenarios return proper status codes without application crashes.

**Acceptance Scenarios**:

1. **Given** 100 inventory items to add sequentially, **When** all operations execute, **Then** all succeed with consistent status code processing (Status=0 for success)
2. **Given** an active database connection, **When** database becomes unavailable mid-operation, **Then** system returns error status without crashing and user receives actionable error message
3. **Given** a DAO method called with malformed parameters, **When** validation occurs, **Then** method returns validation failure (not MySQL exception) with clear guidance on correct format
4. **Given** connection pool configured for 5-100 connections, **When** checking pool statistics under load, **Then** pool maintains healthy connection count within configured range

---

### User Story 3 - Developer Troubleshoots Database Issues (Priority: P2)

As a developer debugging production problems, I need comprehensive error logging with full context and clear messages, so that I can quickly identify root causes without guessing.

**Why this priority**: After establishing reliable operations (P1), effective troubleshooting becomes the next critical need for maintaining the system.

**Independent Test**: Trigger various error conditions (connection failure, null parameters, constraint violations), review error logs and Service_DebugTracer output, and verify all errors include calling method, parameters, and user-friendly messages.

**Acceptance Scenarios**:

1. **Given** a database connection failure occurs, **When** reviewing the log_error table, **Then** error entry includes User, Severity, ErrorType, ErrorMessage, StackTrace, MethodName, MachineName, OSVersion, AppVersion, and ErrorTime
2. **Given** a DAO method called with null required parameter, **When** validation fails, **Then** user sees friendly validation message (e.g., "PartID is required") while detailed technical info logged
3. **Given** Service_DebugTracer enabled, **When** reviewing debug output, **Then** complete call stack visible with method entry/exit and all parameter values (sanitized for sensitive data)
4. **Given** same error occurs multiple times within 5 seconds, **When** errors are logged, **Then** all logged to database but UI shows error MessageBox only once (cooldown mechanism)

---

### User Story 4 - Database Administrator Maintains Consistent Schema (Priority: P2)

As a database administrator, I need all stored procedures to follow uniform parameter naming and output conventions, so that schema changes are predictable and maintenance is straightforward.

**Why this priority**: Schema consistency enables the uniform DAO pattern. This supports the P1 developer experience but is secondary to having basic reliability.

**Independent Test**: Query all stored procedures for parameter consistency, verify all have p_Status and p_ErrorMsg outputs, run parameter prefix validation script, and confirm 0 inconsistencies reported.

**Acceptance Scenarios**:

1. **Given** all 60+ stored procedures in the database, **When** querying parameter definitions, **Then** all use standard prefixes (p_ for CRUD, in_ for transactions, o_ for special outputs)
2. **Given** any stored procedure, **When** checking output parameters, **Then** procedure declares OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500)
3. **Given** stored procedure parameters, **When** comparing to C# model properties, **Then** names match exactly in PascalCase (PartID, Location, Operation)
4. **Given** parameter prefix validation script, **When** executed against database, **Then** reports 0 inconsistencies and confirms all procedures follow conventions

---

### User Story 5 - Performance Analyst Reviews Query Execution (Priority: P3)

As a performance analyst, I need visibility into database operation timing and connection pool metrics, so that I can identify and optimize bottlenecks.

**Why this priority**: Performance monitoring is important but secondary to reliability and maintainability. The system must first work correctly before optimizing speed.

**Independent Test**: Execute large dataset queries, run concurrent operations, perform batch modifications, and verify timing logged with connection pool remaining healthy.

**Acceptance Scenarios**:

1. **Given** inventory search returning 10,000+ rows, **When** operation completes, **Then** timing is logged and any operation exceeding 1000ms flagged as warning
2. **Given** 100 concurrent database operations, **When** executing simultaneously, **Then** connection pool handles load without exhaustion and operations complete successfully
3. **Given** batch removal of 100 inventory items within transaction, **When** error occurs mid-batch, **Then** transaction rolls back completely with no partial commits
4. **Given** slow query monitoring enabled, **When** reviewing query execution plans, **Then** no full table scans occur for queries on indexed columns

---

### Edge Cases

- What happens when stored procedure returns unexpected status code (not 0, 1, or -1)? System logs warning and treats as error condition.
- How does system handle database timeout during long-running operation? Retry logic attempts up to 3 times with exponential backoff for transient errors; permanent failures return error status.
- What happens when error logging itself fails (log_error table unavailable)? System catches exception in logging method and falls back to file-based logging to prevent recursive failures.
- How does system handle mixed parameter prefixes in single stored procedure (both p_ and in_)? Helper_Database_StoredProcedure detects prefixes per parameter and applies correctly based on stored procedure signature.
- What happens when connection pool is exhausted? New requests wait up to ConnectionTimeout (30 seconds) then return timeout error with user-friendly message.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST provide standardized Helper_Database_StoredProcedure class with four execution methods: ExecuteNonQueryWithStatus (INSERT/UPDATE/DELETE), ExecuteDataTableWithStatus (SELECT multiple rows), ExecuteScalarWithStatus (SELECT single value), and ExecuteWithCustomOutput (non-standard outputs)

- **FR-002**: System MUST automatically detect and apply parameter prefixes (p_, in_, o_) by querying INFORMATION_SCHEMA.PARAMETERS at application startup, caching results in memory, and using fallback convention (p_ default, in_ for Transfer*/transaction* procedures) if schema query fails, allowing developers to provide unprefixed parameter names in Dictionary<string, object>

- **FR-003**: All DAO methods MUST return structured DaoResult or DaoResult\<T> objects containing IsSuccess boolean, Data (if applicable), Message string, and Exception (if applicable)

- **FR-004**: All stored procedures MUST declare OUT p_Status INT and OUT p_ErrorMsg VARCHAR(500) output parameters with standardized return codes: 0=success, 1=not found/no-op, -1=error

- **FR-005**: System MUST log all database errors to log_error table including User, Severity, ErrorType, ErrorMessage, StackTrace, ModuleName, MethodName, AdditionalInfo, MachineName, OSVersion, AppVersion, and ErrorTime

- **FR-006**: Error logging MUST prevent recursive failures by catching exceptions in LogErrorToDatabaseAsync and falling back to file-based logging when database unavailable

- **FR-007**: System MUST implement error message cooldown mechanism that suppresses duplicate UI messages for same error within 5 seconds while still logging all occurrences

- **FR-008**: System MUST use connection pooling with MinPoolSize=5, MaxPoolSize=100, ConnectionTimeout=30 seconds for all database connections

- **FR-009**: System MUST implement retry logic for transient database errors (deadlock=1205, lock timeout=1213, server gone away=2006, lost connection=2013) with up to 3 attempts using exponential backoff

- **FR-010**: All new DAO methods MUST be purely asynchronous (ending with Async suffix) without useAsync parameter; all existing calling code (Forms, Services, Controls) MUST be refactored to async/await patterns immediately as no synchronous wrapper class will be provided

- **FR-011**: All multi-step operations (inventory transfers, batch modifications, user creation with roles) MUST use explicit database transactions with rollback on any step failure to ensure maximum data integrity

- **FR-012**: System MUST integrate Service_DebugTracer for method entry/exit tracking with all parameters logged at method start and result logged before return

- **FR-013**: All database connection strings MUST be centralized in Helper_Database_Variables with no hardcoded credentials in code

- **FR-014**: System MUST validate database connectivity on application startup before loading MainForm

- **FR-015**: All DAO classes MUST follow identical structure: methods are async, return DaoResult variants, match stored procedure parameters exactly, and route all database access through Helper_Database_StoredProcedure

- **FR-016**: System MUST eliminate all direct MySqlConnection and MySqlCommand usage in Data/ folder, routing exclusively through Helper_Database_StoredProcedure

- **FR-017**: Stored procedure parameter naming MUST use PascalCase matching C# model properties (PartID, Location, Operation, etc.)

- **FR-018**: System MUST provide separate test database (mtm_wip_application_winform_test) for integration testing with schema-only copy synchronized via migration scripts, where test classes use per-test-class transactions (begin transaction, insert test data, run tests, rollback) for fast isolated testing without affecting production data

- **FR-019**: Critical errors (OutOfMemoryException, StackOverflowException, AccessViolationException) and database connection errors at startup MUST terminate application gracefully after logging; database connection errors during operation MUST show user retry prompt dialog allowing user to retry or close application

- **FR-020**: System MUST measure and log execution time for all database operations with configurable thresholds per operation category: Query (500ms), Modification (1000ms), Batch (5000ms), Report (2000ms), flagging operations exceeding their category threshold as warnings

### Key Entities

- **DaoResult**: Wrapper class encapsulating operation outcome with IsSuccess boolean, Message string describing outcome, Exception object if error occurred, used for non-data-returning operations

- **DaoResult\<T>**: Generic wrapper class extending DaoResult with Data property of type T containing query results, used for data-returning operations like SELECT queries

- **Helper_Database_StoredProcedure**: Central helper class providing four execution methods, automatic parameter prefix detection, status code processing, connection pooling configuration, and retry logic for transient errors

- **DAO Classes**: Twelve data access object classes (Dao_Inventory, Dao_User, Dao_Transactions, Dao_Part, Dao_Location, Dao_Operation, Dao_ItemType, Dao_QuickButtons, Dao_History, Dao_ErrorLog, Dao_System) providing async methods that map to stored procedures

- **Stored Procedures**: 60+ database procedures organized by domain (inventory operations, transaction logging, user management, role management, metadata management, error logging, UI settings) with standardized parameters and outputs

- **Connection Pool**: MySql.Data connection pool configuration managing 5-100 database connections with 30-second timeout, handling concurrent operations efficiently

- **Error Log Entry**: Database record in log_error table containing User, Severity, ErrorType, ErrorMessage, StackTrace, ModuleName, MethodName, AdditionalInfo, MachineName, OSVersion, AppVersion, ErrorTime for troubleshooting

- **Transaction History**: Record in inv_transaction table tracking TransactionType (IN/OUT/TRANSFER), PartID, FromLocation, ToLocation, Operation, Quantity, User, ItemType, BatchNumber, Notes, TransactionDate for audit trail

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Zero MySQL parameter errors occur in application logs after refactor completion (currently experiencing parameter prefix errors)

- **SC-002**: 100% of database operations in Data/ folder route through Helper_Database_StoredProcedure with no direct MySQL API usage (verified by static code analysis)

- **SC-003**: All 60+ stored procedures successfully tested with both valid and invalid inputs, returning consistent DaoResult responses

- **SC-004**: Database operations complete within 5% performance variance compared to pre-refactor baseline (measured by benchmark suite)

- **SC-005**: Connection pool maintains healthy 5-100 connection range under load testing with 100 concurrent operations

- **SC-006**: Error logging successfully captures and records all database errors without recursive failures (tested by simulating log_error table unavailability)

- **SC-007**: Developer can implement new database operation following standard pattern in under 15 minutes including stored procedure creation and DAO method implementation

- **SC-008**: 90% reduction in database-related support tickets and bug reports within first month after deployment

- **SC-009**: All multi-step operations (transfers, batch modifications) properly roll back when any step fails, with zero orphaned records in database

- **SC-010**: Application startup validates database connectivity and displays actionable error message within 3 seconds if database unavailable, preventing user confusion
