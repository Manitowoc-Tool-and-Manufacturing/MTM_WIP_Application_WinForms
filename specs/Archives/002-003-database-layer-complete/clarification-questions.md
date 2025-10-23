# Clarification Log: Comprehensive Database Layer Standardization

**Branch**: `002-003-database-layer-complete`
**Spec**: [spec.md](./spec.md)
**Last Updated**: 2025-10-17

This document consolidates all resolved clarification questions from the original phase 1-2 specification (2025-10-13) and the phase 2.5 refresh session (2025-10-15), providing a single reference for decision history.

---

## Session 2025-10-13 – Initial Specification

| # | Question | Decision | Notes |
|---|----------|----------|-------|
| Q1 | Async execution strategy? | Async-only DAO methods; no synchronous wrappers. | Forces complete migration across codebase. |
| Q2 | Startup failure policy? | Terminate application with message; runtime shows retry dialog. | Ensures deterministic startup behavior. |
| Q3 | Slow query thresholds? | Category-based thresholds (Query 500 ms, Modification 1000 ms, Report 2000 ms, Batch 5000 ms). | Configurable via Model_AppVariables. |
| Q4 | Multi-step transaction handling? | Explicit transactions with rollback on failure for all multi-step operations. | Applies to transfers, batch updates, composite workflows. |
| Q5 | Error logging severity levels? | Critical / Error / Warning taxonomy with documented criteria. | Aligns LoggingUtility and Service_ErrorHandler behaviors. |
| Q6 | DaoLegacy wrapper? | Reject wrapper; migrate all callers to async/await immediately. | Eliminates technical debt. |
| Q7 | Parameter prefix detection? | Query INFORMATION_SCHEMA at startup with convention fallback. | Provides 100 % accurate prefixes when metadata available. |
| Q8 | Integration test database strategy? | Use schema-only copy `mtm_wip_application_winform_test` with per-test-class transactions. | Guarantees isolation and fast cleanup. |

---

## Session 2025-10-15 – Refresh & Feedback

| # | Question | Decision | Notes |
|---|----------|----------|-------|
| Q9 | Must Phase 2.5 block DAO work? | Yes. Stored procedure standardization is a hard prerequisite. | Prevents divergence between DAO expectations and SP contracts. |
| Q10 | Documentation cadence? | Update concurrently via Documentation Update Matrix. | Avoids backlog and drift. |
| Q11 | Handling production schema drift? | Re-audit before deployment, categorize (A/B/C), and reconcile. | Ensures hotfixes persist post-release. |
| Q12 | Developer tooling scope? | Add Developer role (Admin prerequisite) and prefix maintenance form. | Grants controlled override capabilities. |
| Q13 | Transaction analysis workflow? | Generate CSV automatically; require developer review (T106a) before refactoring. | Guarantees human validation of complex flows. |
| Q14 | Integration test diagnostics? | Base helper must emit JSON with seven diagnostic fields on failure. | Simplifies triage and automation. |
| Q15 | Static analysis enforcement? | Provide Roslyn analyzer (warnings first, later errors) guarding helper usage and status checks. | Sustains pattern compliance. |
| Q16 | Startup fallback strategy? | Remove convention fallback; add three-attempt retry dialog then terminate. | Prevents silent misconfigurations. |
| Q17 | Parameter override persistence? | Store overrides in DB table with audit trail; integrate with startup cache and maintenance UI. | Supports edge cases without code changes. |

---

## Outstanding Clarifications

None. All decisions above are incorporated into the specification and plan.
