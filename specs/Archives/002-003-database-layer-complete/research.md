# Phase 0: Research - Comprehensive Database Layer Standardization

**Generated**: 2025-10-17  
**Feature**: Comprehensive Database Layer Standardization  
**Branch**: `002-003-database-layer-complete`

## Purpose

Resolve all historical NEEDS CLARIFICATION items, capture best-practice guidance from the combined phase 1-2 and phase 2.5 initiatives, and document additional decisions introduced by the centralized workflow (including automation responsibilities for discovery validation tasks).

## Research Findings

### R1: INFORMATION_SCHEMA.PARAMETERS Query Pattern

**Decision**: Query `INFORMATION_SCHEMA.PARAMETERS` at application startup, cache results in a static dictionary, and rely on convention-based fallback only when metadata cannot be retrieved.

**Rationale**:
- MySQL 5.7 exposes reliable parameter metadata via `INFORMATION_SCHEMA.PARAMETERS`.
- One-time startup cost (~100–200 ms for 60+ procedures) sits well within the 5-second splash budget.
- Cached lookups eliminate per-call schema traffic and provide deterministic prefix detection.
- Fallback keeps the application operational when permissions block INFORMATION_SCHEMA access.

**Implementation Pattern**:
```csharp
private static readonly Dictionary<string, Dictionary<string, string>> _procedureParameterCache = new();

public static async Task InitializeParameterCacheAsync(string connectionString)
{
    const string query = @"
        SELECT ROUTINE_NAME, PARAMETER_NAME, PARAMETER_MODE
        FROM INFORMATION_SCHEMA.PARAMETERS
        WHERE ROUTINE_SCHEMA = DATABASE()
          AND ROUTINE_TYPE = 'PROCEDURE'
        ORDER BY ROUTINE_NAME, ORDINAL_POSITION";

    await using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();

    await using var command = new MySqlCommand(query, connection);
    await using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        var procedureName = reader.GetString(0);
        var parameterName = reader.GetString(1);
        var parameterMode = reader.GetString(2);

        if (!_procedureParameterCache.TryGetValue(procedureName, out var map))
        {
            map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _procedureParameterCache[procedureName] = map;
        }

        map[parameterName] = parameterMode;
    }
}

private static string GetParameterPrefix(string procedureName, string parameterName)
{
    if (_procedureParameterCache.TryGetValue(procedureName, out var map))
    {
        if (map.ContainsKey($"p_{parameterName}")) return "p_";
        if (map.ContainsKey($"in_{parameterName}")) return "in_";
        if (map.ContainsKey($"o_{parameterName}")) return "o_";
    }

    if (procedureName.Contains("Transfer", StringComparison.OrdinalIgnoreCase) ||
        procedureName.Contains("transaction", StringComparison.OrdinalIgnoreCase))
    {
        return "in_";
    }

    return "p_";
}
```

**Alternatives Considered**: Convention-only detection (unreliable), developer-specified prefixes (verbose), per-call INFORMATION_SCHEMA queries (performance penalty).

**Risk Mitigation**: Startup logging surfaces cache success/failure; integration tests cover both cached and fallback paths.

---

### R2: Async Migration Strategy for 100+ Call Sites

**Decision**: Migrate all forms, controls, and services to async/await immediately. No legacy synchronous wrapper will be introduced.

**Rationale**:
- Eliminates technical debt and ensures uniform async behavior.
- Aligns with Constitution Principle VI (Async-First UI Responsiveness).
- Simplifies future maintenance—developers see one pattern everywhere.

**Implementation Highlights**:
- Use `async void` for WinForms event handlers, wrapping UI disabling/enabling in try/finally.
- Use `Load` events or explicit initialization tasks for UserControls.
- Run background services via timers that invoke async delegates.

**Alternatives Considered**: Phased migration with a `DaoLegacy` wrapper (rejected by clarification), partial async adoption (creates inconsistent patterns).

**Risk Mitigation**: Atomic commits by category (DAOs → helpers → UI), manual validation after each category, clear rollback strategy via Git.

---

### R3: DaoResult<T> Wrapper Pattern

**Decision**: Standardize on `DaoResult`/`DaoResult<T>` wrappers with static `Success`/`Failure` constructors.

**Rationale**:
- Prevents exception-driven control flow for expected outcomes.
- Supplies consistent messaging to UI layer and logging subsystems.
- Enables pattern matching and simplifies testing.

**Alternatives Considered**: Exceptions for control flow, raw tuples, discriminated unions. All were less maintainable in this WinForms context.

---

### R4: Transaction Management Pattern

**Decision**: Wrap all multi-step database operations in explicit `MySqlTransaction` scopes with rollback on any failure.

**Rationale**:
- Clarification mandates and manufacturing domain require strict integrity.
- Provides deterministic rollback regardless of which step fails.

**Alternatives Considered**: Implicit transactions, TransactionScope (unnecessary overhead), stored procedure–only transactions (harder to coordinate across multiple procedures).

---

### R5: Integration Test Database Management

**Decision**: Use `mtm_wip_application_winform_test` schema with per-test-class transactions for isolation.

**Rationale**:
- Fast setup/teardown via rollback.
- Supports parallel execution across developers.
- Avoids the overhead of full database resets or containers per run.

**Alternatives Considered**: Shared reset database (slow), SQLite in-memory (incompatible with MySQL stored procedures), containerized MySQL per run (resource-heavy).

---

### R6: Performance Monitoring & Thresholds

**Decision**: Measure execution time for every DAO call, compare to category thresholds (Query 500 ms, Modification 1000 ms, Report 2000 ms, Batch 5000 ms), and log warnings when exceeded.

**Rationale**:
- Provides actionable telemetry for analysts.
- Aligns with success criteria requiring ±5 % performance variance.

**Alternatives Considered**: Single threshold for all operations (too coarse), deferred monitoring (loss of visibility), adaptive learning (overkill at current scale).

---

### R7: Automation Agent Responsibilities for Discovery Validation

**Decision**: Delegate T106a (transaction CSV review) and T106b (stored procedure checklist) to the automation agent using the curated artifact set: `Database/UpdatedStoredProcedures/ReadyForVerification`, `STORED_PROCEDURE_CALLSITES.csv`, `sql-operations-detailed.json`, `procedure-base-analysis.csv`, `compliance-report.csv`, `call-hierarchy-complete.json`, `database-schema-snapshot.json`, and `PROCEDURE_ANALYSIS_GUIDE.md`.

**Rationale**:
- Ensures consistent application of discovery heuristics at scale.
- Offloads repetitive validation work, freeing developers for refactoring tasks.
- Guarantees the gate (T106a/T106b) is satisfied before any procedure modifications begin.

**Alternatives Considered**: Manual review (error-prone, time-consuming), partial automation (still requires manual gatekeeping).

**Risk Mitigation**: Agent instructions captured in plan/tasks; developers review generated results before proceeding.

---

## Research Summary

All open questions from the combined initiatives are resolved. Established patterns cover parameter detection, async adoption, DaoResult usage, transaction safety, integration testing, performance monitoring, and the new automation workflow for CSV/checklist validation.

**Next Phase**: Continue with design artifacts (`data-model.md`, `/contracts/`, `quickstart.md`) using these decisions as the architectural baseline.
