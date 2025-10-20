# Stored Procedure Parameter Prefix Conventions

**Last Updated:** 2025-10-17  
**Source:** database-schema-snapshot.json (T101)

---

## 1. Summary

All stored procedures in the current MySQL 5.7 schema use a **parameter name prefix** to distinguish input/output arguments. Analysis of 337 parameters across 75 procedures shows:

| Prefix | Count | Percentage | Notes |
| ------ | -----:| ----------:| ----- |
| `p_`   | 337   | 100 %      | Standard prefix for all IN/OUT parameters, including `p_Status` / `p_ErrorMsg` |
| `in_`  | 0     | 0 %        | Historically used for transfer-style procedures; no active procedures currently use it |
| `o_` / `out_` | 0 | 0 % | Legacy pattern; not present in active schema |
| *(none)* | 0 | 0 % | Application enforces prefixes; none detected |

**Key Takeaway:** The live schema is fully normalized to the `p_` prefix. The helper layer must continue to *default* to `p_` while preserving fallbacks for legacy prefixes to remain forward/backward compatible.

---

## 2. Standard Prefix Rules

1. **General Inputs & Outputs (`p_`)**  
   - Applies to *all* parameters today.  
   - Examples: `p_PartID`, `p_FromLocation`, `p_Status`, `p_ErrorMsg`.  
   - OUT parameters follow the same prefix to keep the stored procedure signature uniform.

2. **Return Envelope**  
   - Every procedure must provide `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)`.  
   - Status Codes: `1` (success with data), `0` (success without data), `-1..-5` (error classes).  
   - MTM DAO helpers rely on these OUT parameters to populate `DaoResult` envelopes.

3. **Parameter Naming**  
   - PascalCase after the prefix (e.g., `p_UserId`, `p_FromLocation`).  
   - Avoid underscores beyond the prefix—aligns with .NET property naming and Dapper-style helpers.  
   - Optional arguments still require the prefix (pass `DBNull.Value` when omitting values).

---

## 3. Fallback & Legacy Considerations

While no current procedures use alternative prefixes, the helper layer retains compatibility logic for:

- **`in_`** — Reserved for transfer-centric or multi-step operations (historical pattern from earlier MTM releases).  
- **`o_` / `out_`** — Rare legacy output prefix used before the 2025 refactor.

**Detection Algorithm (Helper_Database_StoredProcedure):**

1. Query cache lookup from `INFORMATION_SCHEMA.PARAMETERS` (loaded at startup by `Helper_Database_StoredProcedure.InitializeParameterCacheAsync`).
2. For each parameter supplied by C# callers:
   - Try `p_` + name.  
   - If not found, try `in_` + name.  
   - If not found, try `o_` / `out_`.  
   - If still missing, fall back to default prefix `p_` (maintains compatibility for newly added procedures before cache refresh).
3. Cache miss handling logs the procedure/parameter combination and returns a descriptive DaoResult error so the calling feature can fail gracefully.

**When to Use Fallbacks:**
- Supporting historical database snapshots during drift reconciliation (Part D tasks).  
- Running legacy integration tests that reference pre-standardization procedures.  
- Allowing DBA teams to introduce `in_` prefixed procedures temporarily while tooling catches up.

---

## 4. Development Guidance

- **Procedure Authors:** Continue to define parameters with the `p_` prefix. New procedure templates should enforce it via snippets or stored-procedure scaffolding scripts.  
- **DAO Developers:** Supply *un-prefixed* parameter keys (e.g., `parameters["PartID"]`). Let the helper add prefixes so the calling code remains consistent regardless of the underlying convention.  
- **Code Review Checklist:** Confirm that new/updated procedures retain `p_Status` and `p_ErrorMsg`, and that any new prefixes are documented here **before** merging.  
- **Automation:** Phase 2.5 Parts C & F (T113+ and T130+) must update this document whenever procedure signatures change.

---

## 5. Action Items & Next Steps

- **Immediate:** No schema changes required—helpers already default to `p_`.  
- **Future:** If new procedures introduce `in_` or `o_` prefixes, update the detection logic and this document with concrete examples.  
- **Testing:** T123 (Parameter cache retry validation) should include simulated cache misses to confirm fallback logic still succeeds.

---

**Maintainer:** Database/DAO Working Group (Phase 2.5)