# Clarification Questions: Comprehensive Database Layer Refactor

**Feature**: Comprehensive Database Layer Refactor  
**Branch**: `002-comprehensive-database-layer`  
**Date**: 2025-10-13  
**Status**: ✅ All Answered (2 Rounds Complete - 8 questions resolved)

---

## Round 1: Critical Architecture Decisions (COMPLETE)

### Your Answers Summary

| Question | Your Answer | Status |
|----------|-------------|--------|
| Q1: Async Strategy | **C** - Async-only for new code, legacy wrappers | ✅ Resolved |
| Q2: Termination Policy | **C** - Terminate on startup, user prompt during operation | ✅ Resolved |
| Q3: Slow Query Threshold | **B** - Configurable per operation type | ✅ Resolved |
| Q4: Transaction Scope | **A** - All multi-step operations use explicit transactions | ✅ Resolved |
| Q5: Error Severity | **B** - Three-tier severity with documented criteria | ✅ Resolved |

All answers have been integrated into `spec.md` with [NEEDS CLARIFICATION] markers removed.

---

## Round 2: Implementation Details (COMPLETE)

Based on your Round 1 answers, I identified 3 additional critical questions about implementation details.

### Your Answers Summary

| Question | Your Answer | Status |
|----------|-------------|--------|
| Q6: DaoLegacy Wrapper Scope | **C** - No wrapper; all calling code migrates to async immediately | ✅ Resolved |
| Q7: Parameter Prefix Detection | **B** - Query INFORMATION_SCHEMA at startup with fallback | ✅ Resolved |
| Q8: Test Database Management | **B** - Schema-only copy with per-test transactions (DB: `mtm_wip_application_winform_test`) | ✅ Resolved |

All answers have been integrated into `spec.md` with functional requirements updated.

---

## Question 6: DaoLegacy Wrapper Class Scope (RESOLVED)

**Context**: Your Q1 answer specified creating a "DaoLegacy wrapper class" for synchronous access during the transition period. This wrapper needs clear boundaries.

**Real-World Impact**: 

The legacy wrapper determines migration complexity:

- **Scope Decision**: Does DaoLegacy wrap ALL DAO operations or only frequently-used ones?
- **Maintenance Window**: 3-6 month transition means tracking which code still uses legacy patterns
- **Deprecation Strategy**: Clear guidance needed on when/how to remove legacy support

Your current codebase has ~100+ DAO method calls across Forms, Services, and Controls. Some are in critical paths (login, inventory add/remove), others are in rarely-used admin screens.

**Recommended Answer**: **Option B - Wrap only high-frequency operations with documented migration plan**

**Reasoning**: Creating wrappers for ALL DAO methods doubles the API surface and maintenance burden. Instead, identify the top 10-15 most-called operations (likely: GetInventoryByPartId, AddInventoryItem, RemoveInventoryItem, GetAllUsers, AuthenticateUser) and provide legacy wrappers only for those. This covers 80% of usage while keeping scope manageable. Less-used operations must migrate to async immediately (forcing best practices where impact is low).

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **DaoLegacy wraps ALL DAO operations (complete 1:1 coverage)** | Zero refactoring required in calling code initially. Doubles API surface (~100+ wrapper methods). High maintenance overhead during transition. May delay full async adoption if developers lean on wrappers too heavily. Longer transition period needed. |
| **B** | **DaoLegacy wraps only high-frequency operations (~10-15 methods)** | Focused wrapper scope (GetInventoryByPartId, AddInventoryItem, RemoveInventoryItem, AuthenticateUser, GetAllUsers, etc.). Forces async migration for low-usage code paths (good learning). Documented list of wrapped methods with migration deadline. Manageable maintenance burden. 80/20 rule: covers 80% of usage with 20% of wrapper effort. |
| **C** | **No DaoLegacy wrapper; all calling code must migrate to async immediately** | Forces best practices everywhere immediately. Clean, modern codebase with no technical debt. Highest upfront migration cost: all Forms event handlers, all Services, all background workers need async updates simultaneously. Risk of introducing deadlocks if async migration done hastily. May delay entire refactor by 2-3 months for calling code updates. |
| **Custom** | Provide your own wrapper strategy | |

---

## Question 7: Parameter Prefix Auto-Detection Strategy (RESOLVED)

**Context**: FR-002 requires automatic parameter prefix detection (p_, in_, o_). The Helper needs to know which prefix to apply for each parameter.

**Real-World Impact**:

Your stored procedures use mixed prefixes:
- Standard CRUD: `p_PartID`, `p_User`, `p_Location`
- Transfers: `in_BatchNumber`, `in_PartID`, `in_NewLocation`
- Special outputs: `o_Operation`

**How does Helper_Database_StoredProcedure know which prefix to use?**

Option A (Convention): Prefix by procedure name pattern (procedures with "Transfer" use `in_`, others use `p_`)  
Option B (Query Schema): Query `INFORMATION_SCHEMA.PARAMETERS` at startup, cache parameter names  
Option C (Configuration): Developer explicitly specifies prefix in DAO method call

**Recommended Answer**: **Option B - Query schema at startup with fallback to convention**

**Reasoning**: Querying `INFORMATION_SCHEMA.PARAMETERS` provides 100% accurate prefix information directly from the database schema. Cache results at startup (one-time cost ~100-200ms). Fallback to convention (p_ default, in_ for Transfer*/transaction* procedures) handles edge cases. This eliminates guesswork and prevents parameter mismatch errors. Most robust approach for 60+ procedures with mixed conventions.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **Convention-based prefix detection by procedure name patterns** | Simple logic: procedures containing "Transfer" or "transaction" use `in_` prefix, others use `p_`. Fast, no database queries. Brittle: breaks if procedure named inconsistently (e.g., `inv_inventory_Move_Part` - is that a transfer?). Requires strict naming standards. No handling for `o_` prefixes (special outputs). May cause subtle bugs. |
| **B** | **Query INFORMATION_SCHEMA.PARAMETERS at startup, cache results** | 100% accurate: reads actual parameter names from database schema. One-time startup cost (~100-200ms to query all procedures). Cache invalidated on app restart (acceptable). Handles all prefix types automatically. Requires database connectivity at startup (already required per FR-014). Industry-standard approach. Fallback to convention if query fails. |
| **C** | **Developer explicitly specifies prefix strategy in DAO method calls** | Maximum control: `ExecuteNonQueryWithStatus(..., prefixMode: PrefixMode.Standard)` or `PrefixMode.Transfer`. No magic, no guessing. Verbose: every DAO call needs prefix mode parameter. Easy to forget or specify incorrectly. Shifts burden to developers rather than automating. More test cases needed (test all modes). |
| **Custom** | Provide your own prefix detection strategy | |

---

## Question 8: Integration Test Database Management (RESOLVED)

**Context**: FR-018 requires separate test database `mtm_wip_application_winform_test`. Integration tests will exercise all 60+ stored procedures.

**Real-World Impact**:

Test database management affects developer workflow:

- **Test Data Initialization**: How do tests get clean, known state before each run?
- **Parallel Test Execution**: Can multiple developers run tests simultaneously?
- **CI/CD Integration**: How does automated testing run in build pipelines?

Current situation: No test database exists yet, production database has live data, developers may run tests locally.

**Recommended Answer**: **Option B - Test database with schema-only copy + per-test-class transactions**

**Reasoning**: Each test class (or test method) begins a transaction, runs tests, then rolls back (clean slate for next test). Schema matches production via migration scripts. No shared test data between developers (each uses local test DB). Fast test execution (transactions cheaper than full DB reset). Supports parallel test runs. Common pattern in .NET integration testing (EntityFramework InMemory pattern, Docker test containers). Downside: can't test transaction failures (since test itself in transaction), but those are unit-testable at Helper level.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **Shared test database with full reset before each test run** | Single test database instance (local or remote). Before test run: truncate all tables, re-seed with known test data. Simple conceptually. Slow: full reset for 60+ procedure tests = minutes. Doesn't support parallel execution (tests clobber each other). CI/CD needs dedicated test DB instance. Flaky tests from race conditions. |
| **B** | **Test database with schema-only copy + per-test-class transactions** | Each developer has local `mtm_wip_application_winform_test` database (schema only, no data). Each test class starts transaction, inserts test data, runs tests, rolls back. Fast (transactions), isolated (each test independent), parallel-safe. Matches production schema via migrations. Doesn't test transaction failure scenarios (but those tested at unit level). Requires transaction-aware test framework (xUnit with IAsyncLifetime, NUnit with SetUp/TearDown). |
| **C** | **Docker test containers with full database per test run** | Each test run spins up isolated MySQL Docker container with schema + data. Perfect isolation: tests can't interfere with each other or developers. Slow startup (~5-10 seconds per container). Requires Docker on all dev machines + CI. High resource usage (multiple containers). Best for end-to-end tests, overkill for integration tests. Consider for smoke tests only. |
| **Custom** | Provide your own test database strategy | |

---

## Summary of All Questions (Both Rounds)

### Round 1: Core Architecture (5 questions)

| Question | Your Answer | Key Impact |
|----------|-------------|------------|
| Q1: Async Strategy | **C** - Async-only with legacy wrappers | Clean async code with transition support |
| Q2: Termination Policy | **C** - Terminate on startup, retry prompt during operation | Clear startup requirement, user control during runtime |
| Q3: Slow Query Threshold | **B** - Configurable per operation type | Granular performance monitoring by category |
| Q4: Transaction Scope | **A** - All multi-step operations use explicit transactions | Maximum data integrity approach |
| Q5: Error Severity | **B** - Three-tier severity with documented criteria | Industry-standard error classification |

### Round 2: Implementation Details (3 questions)

| Question | Your Answer | Key Impact |
|----------|-------------|------------|
| Q6: DaoLegacy Scope | **C** - No wrapper; immediate async migration | Clean codebase, higher upfront refactor cost |
| Q7: Prefix Detection | **B** - Query INFORMATION_SCHEMA at startup | 100% accurate parameter matching |
| Q8: Test Database | **B** - Schema-only copy with per-test transactions | Fast, isolated integration testing (DB: `mtm_wip_application_winform_test`) |

**Total Questions**: 8 questions across 2 rounds  
**All Resolved**: ✅ Specification ready for planning phase

---

## Final Notes

### Critical Decision: No DaoLegacy Wrapper (Q6: Answer C)

Your choice to eliminate the DaoLegacy wrapper (contradicting Q1's "legacy wrappers" mention) means:

- **Immediate Impact**: All Forms event handlers, Services, and Controls must migrate to async/await patterns
- **Refactor Scope**: Estimated 100+ call sites need async conversion simultaneously with DAO refactor
- **Clean Architecture**: No technical debt from wrapper maintenance, forces modern patterns everywhere
- **Timeline Impact**: May extend refactor by 2-4 weeks for comprehensive async migration

This is the more aggressive but cleaner approach. The spec has been updated to reflect pure async implementation without any synchronous compatibility layer.

### Test Database Name Clarification (Q8)

Database name **MUST be** `mtm_wip_application_winform_test` (not `mtm_wip_application_test` which is already in use). This has been documented in FR-018.

---

## Next Steps

1. ✅ **Clarifications Complete** - All 8 questions answered and integrated into spec.md
2. ⏭️ **Update Requirements Checklist** - Mark specification as 100% complete
3. ⏭️ **Run Validation** - Confirm spec ready for planning phase
4. ⏭️ **Proceed to Planning** - Execute `/speckit.plan` to generate implementation plan

**Recommendation**: Run `specify check` to validate spec completeness, then proceed to `/speckit.plan` for detailed technical planning and task breakdown.

---

## How to Respond

~~Please provide your answers in one of these formats:~~

~~**Format 1 - Accept All New Recommendations:**~~
~~```~~
~~Accept Q6, Q7, Q8~~
~~```~~

~~**Format 2 - Mixed Responses:**~~
~~```~~
~~Accept Q6~~
~~Q7: A~~
~~Q8: Custom - Use SQLite in-memory for fast tests~~
~~```~~

~~**Format 3 - Select Specific Options:**~~
~~```~~
~~Q6: B~~
~~Q7: B~~
~~Q8: C~~
~~```~~

---

## Next Steps After This Round

Once you answer Q6-Q8:
1. I'll update `spec.md` with final clarifications
2. Update requirements checklist to show 100% complete
3. Generate implementation planning tasks
4. You can proceed to `/speckit.plan` for detailed technical planning

**Total Questions Across Both Rounds**: 8 (within reasonable scope for complex refactor)

---

## Instructions

This document contains 5 critical clarification questions that need to be resolved before proceeding to implementation planning. Each question includes:

- **Context**: Where this appears in the spec and why it matters
- **Real-World Impact**: Practical consequences of this decision in production
- **Recommended Answer**: AI's suggested best choice with reasoning
- **Options**: All available choices with implications

Please review each question and provide your answers by either:
1. Accepting the recommendation (write "Accept Q1", "Accept Q2", etc.)
2. Selecting an option letter (write "Q1: A", "Q2: C", etc.)
3. Providing a custom answer (write "Q1: Your custom answer")

---

## Question 1: Async Execution Mode Strategy

**Context**: FR-010 currently states "All DAO methods MUST be asynchronous (ending with Async suffix) and support both async and sync execution modes via useAsync parameter"

**Real-World Impact**: 

Your current codebase has mixed async/sync patterns throughout. The `useAsync` parameter appears in many DAO methods (e.g., `GetInventoryByPartIdAsync(string partId, bool useAsync = false)`). This decision affects:

- **Developer Experience**: Simpler or more complex API surface
- **Migration Path**: How much existing code needs immediate changes
- **Performance**: Async-only forces non-blocking I/O, sync support allows legacy blocking patterns
- **Code Maintenance**: Dual code paths increase complexity and testing surface

In manufacturing applications, database operations are I/O-bound and benefit from async patterns. However, WinForms event handlers and some legacy code may currently use synchronous patterns.

**Recommended Answer**: **Option C - Async-only for new code, legacy wrappers for old code**

**Reasoning**: This provides the cleanest path forward. New refactored DAO methods are purely async (modern best practice), while temporary sync wrappers allow gradual migration of calling code. After the refactor stabilizes, legacy wrappers can be deprecated. This avoids the pitfall of maintaining dual code paths permanently while not forcing a "big bang" rewrite of all calling code.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **Async-only (remove useAsync parameter completely)** | Forces best practices throughout codebase. All calling code must be updated to use `await`. Clean, modern API. Requires immediate changes to all Forms event handlers and synchronous code paths. Risk of introducing deadlocks if not careful with `.Result`/`.Wait()` during migration. |
| **B** | **Keep useAsync parameter for backwards compatibility** | Zero breaking changes to existing code. Allows gradual adoption of async patterns. Maintains complexity of dual execution paths. Every DAO method needs both sync and async test coverage. Parameter overhead in every method signature. May enable bad practices to persist. |
| **C** | **Async-only for new DAO methods, create separate sync wrapper class for legacy code** | Clean separation: new code is purely async, old code uses `DaoLegacy` wrapper class with `.GetAwaiter().GetResult()` internally. Clear migration path with deprecation timeline. Temporary code duplication during 3-6 month transition. Developers know which pattern is "correct" vs "compatibility". |
| **Custom** | Provide your own strategy (<=5 words or brief description) | |

---

## Question 2: Application Termination Policy for Database Errors

**Context**: FR-019 states "Critical errors (OutOfMemoryException, StackOverflowException, AccessViolationException) MUST terminate application gracefully after logging"

**Real-World Impact**:

Database connectivity issues happen in manufacturing environments:
- Network hiccups during switch failover
- Database maintenance windows
- Temporary overload/deadlocks
- Network cable disconnections

The question is: **When should the application terminate vs. attempt recovery?**

Current code shows `Dao_ErrorLog.HandleException_SQLError_CloseApp` which appears to terminate on connection errors. But this could be frustrating for operators if temporary network blips force app restarts, potentially losing in-progress data entry.

**Recommended Answer**: **Option C - Terminate on startup, retry with user prompt during operation**

**Reasoning**: Manufacturing operators often work in time-sensitive environments. An app that crashes during data entry due to a 2-second network glitch creates frustration and potential data loss. However, starting the app without database connectivity is pointless (can't load metadata, authenticate, etc.). Option C provides the best user experience: fail fast on startup (can't proceed without DB), but during operation give the user control ("Database connection lost. Retry? Yes/Cancel"). This aligns with desktop application UX conventions.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **Terminate application on ALL database connection errors** | Strictest data integrity approach. No possibility of operating with stale data or in inconsistent state. Frustrating for users during transient network issues. Operators lose in-progress work. May trigger multiple restarts during unstable network periods. Aligns with "fail-fast" philosophy. |
| **B** | **Terminate only on startup connection errors, allow retry during operation** | App won't start without database (good). During operation, silent retries for transient errors. Risk: user may not realize database unavailable, sees errors but app keeps running. Could lead to confusion if operations silently fail. Requires robust status indicators in UI. |
| **C** | **Terminate on startup, show retry prompt to user during operation** | Best user experience. Startup failure is clear (can't proceed). During operation, user sees "Connection lost. Retry?" dialog. User decides whether to retry or close app. Saves in-progress work where possible. Requires careful transaction handling (don't prompt mid-transaction, roll back first). Most common pattern in enterprise desktop apps. |
| **Custom** | Provide your own termination policy | |

---

## Question 3: Slow Query Threshold Configuration

**Context**: FR-020 states "System MUST measure and log execution time for all database operations, flagging operations exceeding 1000ms as warnings"

**Real-World Impact**:

In manufacturing systems, different database operations have naturally different performance profiles:

- **Simple lookups**: `GetInventoryByPartId` - should be <100ms (indexed query)
- **Complex reports**: `GetLast10TransactionsByPart` with joins - might legitimately take 300-500ms
- **Batch operations**: `RemoveInventoryItemsFromDataGridViewAsync` processing 100 items - could take 2-5 seconds
- **Metadata reloads**: `sys_reload_metadata` - might scan multiple tables, 1-2 seconds expected

A single 1000ms threshold will either:
- Generate false alarms for legitimate operations (if too strict)
- Miss actual performance problems in fast operations (if too lenient)

**Recommended Answer**: **Option B - Configurable per operation type**

**Reasoning**: Manufacturing systems need accurate performance monitoring without alarm fatigue. Option B provides practical flexibility: set 200ms for simple queries, 1000ms for reports, 5000ms for batch operations. This allows performance regression detection (batch operation suddenly takes 10 seconds = problem) without false positives. Configuration can live in `Model_AppVariables` or appsettings. More intelligent than single threshold, more maintainable than adaptive learning.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **Single 1000ms threshold for all operations** | Simplest to implement: one constant, one log check. Easy to understand and document. Will generate false warnings for legitimate slow operations (reports, batch processing). May miss performance regressions in fast operations (query goes from 50ms to 300ms = still under 1000ms but 6x slower). Good for MVP, may need refinement later. |
| **B** | **Configurable per operation type (queries vs modifications vs batch vs reports)** | Practical middle ground. Define categories: "Query" (500ms), "Modification" (1000ms), "Batch" (5000ms), "Report" (2000ms). DAO methods tagged with category. Accurate performance monitoring without alarm fatigue. Requires category classification effort. Can be stored in config file or Model_AppVariables. Industry-standard approach. |
| **C** | **Adaptive thresholds based on baseline metrics** | Most intelligent: system learns normal performance, alerts on deviations. Requires baseline collection period (7-30 days). Statistical analysis overhead (mean, std dev, percentiles). Complex to implement and explain to users. Best for mature systems with stable workloads. Overkill for current refactor scope - consider for future enhancement. |
| **Custom** | Provide your own threshold strategy | |

---

## Question 4: Transaction Scope and Multi-Step Operation Policy

**Context**: FR-011 states "Multi-step operations (inventory transfers, batch modifications, user creation with roles) MUST use explicit database transactions with rollback on any step failure"

**Real-World Impact**:

Transactions provide atomicity but add complexity. Consider these scenarios in your codebase:

1. **Inventory Transfer**: Remove from Location A, Add to Location B, Log to inv_transaction
   - Without transaction: If Add fails, item lost (removed but not added back)
   - With transaction: Atomic, but holds locks during entire operation

2. **Batch Remove from DataGridView**: Remove 100 items sequentially
   - Without transaction: Partial completion possible (50 succeed, 50 fail)
   - With transaction: All-or-nothing, but long lock duration

3. **Single Add Operation**: Just insert one inventory item
   - Transaction overhead may be unnecessary if operation is atomic

The question: Should ALL multi-step operations use transactions, or only specific high-risk scenarios?

**Recommended Answer**: **Option B - Use transactions for specific high-risk operations (transfers, batch, user+role), not single CRUD**

**Reasoning**: Transactions are powerful but have costs (lock contention, deadlock risk, complexity). Use them where data integrity absolutely requires atomicity. Inventory transfers MUST be transactional (can't lose parts). Batch operations benefit from all-or-nothing behavior. But a single `AddInventoryItemAsync` call that executes one stored procedure doesn't need explicit C# transaction wrapping—the stored procedure itself is atomic. This approach balances integrity with performance and complexity.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **All multi-step operations MUST use explicit transactions** | Maximum data integrity. Covers all scenarios including edge cases you haven't thought of. Increases complexity: every multi-call DAO method needs transaction management. Higher risk of deadlocks under load. Lock contention may impact performance. Conservative, safe approach. |
| **B** | **Use transactions for specific high-risk operations: transfers, batch, user+role creation** | Balanced approach. Documented list of operations requiring transactions: `TransferPartAsync`, `TransferQuantityAsync`, `RemoveItemsFromDataGridViewAsync`, `AddUserWithRolesAsync`. Single-call operations (individual adds/removes) rely on stored procedure atomicity. Lower deadlock risk, better performance. Clear guidelines for developers. Industry best practice. |
| **C** | **Stored procedures handle transactions internally; C# code uses transactions only for cross-procedure operations** | Pushes transaction logic to database layer. Stored procedures use `START TRANSACTION` / `COMMIT` / `ROLLBACK`. C# only wraps multiple procedure calls. Centralized transaction management. Requires updating 60+ stored procedures with transaction logic. May be harder to debug across layers. Good long-term architecture but high refactor cost now. |
| **Custom** | Provide your own transaction policy | |

---

## Question 5: Error Logging Severity Classification

**Context**: Current code logs errors with severity levels ("Critical", "Error", "Warning"). The spec mentions `Dao_ErrorLog.HandleException_SQLError_CloseApp` and `HandleException_GeneralError_CloseApp` but doesn't define when to use each severity.

**Real-World Impact**:

Error severity affects:
- **Alerting**: Critical → page on-call engineer, Error → email, Warning → log only
- **Support Triage**: Severity helps prioritize which errors to investigate first
- **Metrics/SLAs**: Critical errors may trigger SLA breaches
- **User Experience**: Should app show different UI for Critical vs Error?

Current code has some severity logic but it's inconsistent. For example:
- Connection errors = Critical (terminates app)
- General exceptions = Error or Critical based on type
- But what about validation failures, constraint violations, deadlocks?

**Recommended Answer**: **Option B - Three-tier severity with documented criteria**

**Reasoning**: Manufacturing systems need clear severity classification for effective incident response. Three tiers provide enough granularity without over-complicating:
- **Critical**: Data integrity at risk OR app cannot continue (connection errors, transaction failures, corruption)
- **Error**: Operation failed but app stable (constraint violation, deadlock after retries, user permission denied)
- **Warning**: Unexpected but handled (slow query, retry succeeded, missing optional data)

This aligns with industry standards (syslog, log4j severity models) and provides clear guidance for developers.

**Options**:

| Option | Description | Implications |
|--------|-------------|--------------|
| **A** | **Two-tier severity: Critical (app-terminating) and Error (all others)** | Simplest model. Critical = call `Process.Kill()`, Error = log and continue. Easy for developers to choose. Doesn't distinguish between "this shouldn't happen but we handled it" (constraint violation) vs "this might happen occasionally" (deadlock that retried successfully). Limited triage capability. |
| **B** | **Three-tier severity: Critical / Error / Warning with documented criteria** | Industry-standard approach. **Critical**: Data integrity risk, app cannot continue, requires immediate attention (connection failure, transaction rollback failure, corrupted state). **Error**: Operation failed, user sees error message, requires investigation (constraint violation, permission denied, deadlock after retries). **Warning**: Unexpected but handled, no user impact (slow query, retry succeeded, missing optional data). Clear guidelines in documentation. Most common in enterprise systems. |
| **C** | **Five-tier severity: Fatal / Critical / Error / Warning / Info** | Most granular. Fatal (app terminating), Critical (data risk), Error (user-facing failure), Warning (unexpected but handled), Info (noteworthy events). Provides maximum filtering/alerting flexibility. Risk of developer confusion about which level to use. May be overkill for current scale. Consider if system grows to high transaction volume with ops team. |
| **Custom** | Provide your own severity model | |

---

## Summary of Recommendations

| Question | Recommended Answer | Key Reasoning |
|----------|-------------------|---------------|
| Q1: Async Strategy | **Option C** - Async-only for new code, legacy wrappers | Clean modern API with practical migration path |
| Q2: Termination Policy | **Option C** - Terminate on startup, user prompt during operation | Best UX: fail fast on startup, user control during operation |
| Q3: Slow Query Threshold | **Option B** - Configurable per operation type | Practical performance monitoring without false alarms |
| Q4: Transaction Scope | **Option B** - Transactions for specific high-risk operations | Balances integrity with performance and complexity |
| Q5: Error Severity | **Option B** - Three-tier severity with documented criteria | Industry-standard, clear triage, good granularity |

---

## How to Respond

Please provide your answers in one of these formats:

**Format 1 - Accept All Recommendations:**
```
Accept all recommendations
```

**Format 2 - Accept Individual Recommendations:**
```
Accept Q1
Accept Q2
Q3: A
Q4: Custom - Use transactions for all operations that modify inventory
Accept Q5
```

**Format 3 - Select Specific Options:**
```
Q1: C
Q2: B
Q3: A
Q4: B
Q5: B
```

Once you provide your answers, I will:
1. Update the spec.md file with your chosen answers
2. Remove all [NEEDS CLARIFICATION] markers
3. Add a Clarifications section documenting the decisions
4. Update the requirements checklist
5. Mark the spec as ready for `/speckit.plan`
