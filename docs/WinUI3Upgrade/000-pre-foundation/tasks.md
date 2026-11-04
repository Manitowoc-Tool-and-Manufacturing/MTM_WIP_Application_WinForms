# Migration Task List (Execution Plan)

**Spec**: `000-pre-foundation`
**Range**: TASK-000a … TASK-000f
**Definition of done for every task**: its `Verify` line passes and the amended document no longer contradicts any decision in [`spec.md`](./spec.md) §3.1.

> **All tasks in this file are complete as of 2026-11-04.** TASK-000a–000f settled the decisions and amended the constitution; TASK-000g–000l propagated both rounds into the specs they change. Every decision in [`spec.md`](./spec.md) §3.1 now has a matching downstream change, and the drift log below is closed.

---

## Phase 1 — Constitutional amendment

- [x] **TASK-000a** — Amend [`../constitution.md`](../constitution.md) in one change, exactly as [`spec.md`](./spec.md) §3.4 specifies: the §1 `Database` row (server version 8.4 LTS, schema still frozen); the §1 `UI Kit` row and the §1.1 `DevWinUI` bullet (10.8.0 → 10.9.1); the §1 `Target Platform Minimum` row (`10.0.17763.0` → `10.0.19041.0`, Windows 10 22H2); and a note under §2 Rule 4 that the existing 5.7.24 server is read-only to this migration and that no construct may be relied upon that is invalid on 26.10.
  - **Verify**: grep the constitution for `5.7.24` and `10.8.0` and `17763`; each remaining hit is inside a sentence that explains what was superseded, not a live requirement. Rule 9 (native theming only) is unchanged and still requires the Windows 10 Mica fallback.

## Phase 2 — Downstream spec corrections

- [x] **TASK-000b** — Correct `001-foundation-and-tooling`: TASK-004's package list (drop `CommunityToolkit.WinUI.UI.Controls`; add the Windows Community Toolkit 8.2 per-control packages and `WinUI.TableView`; pin `DevWinUI` 10.9.1); TASK-001's template wording (D35, `dotnet new`); `TargetPlatformMinVersion` in `plan.md` §1.2 to `10.0.19041.0` (D45); and `pwsh` 7 as a stated prerequisite (D34).
  - **Verify**: no file under `001-` names `CommunityToolkit.WinUI.UI.Controls` or `10.8.0` or `17763` as a requirement.

- [x] **TASK-000c** — Correct `003-crosscutting-services-and-theming`: the shared grid is written against `WinUI.TableView`, not the Toolkit `DataGrid`; the package table's `DevWinUI` row becomes 10.9.1; the `AutoScrollView`/`WindowedContentDialog` entries are unchanged.
  - **Verify**: `003/plan.md` names `WinUI.TableView` wherever it previously named the Toolkit `DataGrid`.

- [x] **TASK-000d** — Correct `006-transaction-history`: the reference grid is a **new build on `WinUI.TableView`** (D37, `spec.md` §3.2), not a port of the Toolkit `DataGrid`; the cross-cutting risk row that says `DataGridView → Toolkit DataGrid is a rewrite` restates the target as `TableView`.
  - **Verify**: `006/spec.md`, `006/plan.md` and `006/tasks.md` contain no requirement to use `CommunityToolkit.WinUI.UI.Controls.DataGrid`.

- [x] **TASK-000e** — Correct `011-deployment-and-cutover`: record that no CI exists (D31), so every verification and parity step is **manual and evidenced in writing** rather than evidenced by a green check, and add the acceptance that this is the sole gate before a no-pilot weekend swap.
  - **Verify**: `011`'s verification phase names, for each step, the artefact that records its result. A step whose evidence is "CI passed" no longer exists.

## Phase 3 — Round 2 (complete)

- [x] **TASK-000f** — Round 2 asked and recorded. Sixteen questions across the eleven categories in D46 became **D47–D62**. [`spec.md`](./spec.md) §4.1 records every question and its decision; §4.2 records the consequences accepted. Completed 2026-11-04.
  - **Verified**: §4.1 has no unasked row; every decision has a `D` number in §3.1; §4.2 lists the accepted consequences of D51, D52, D53, D55, D59 and D60.

## Phase 4 — Round 2 propagation (complete)

Each task below turned a decision into a change in a spec that would otherwise still tell a developer to do the old thing. All six are closed as of 2026-11-04.

- [x] **TASK-000g** — `001-foundation-and-tooling`: repo shape (D48), `global.json` + `Directory.Packages.props` (D49), committed lock files (D50), `appsettings.json` + `%PROGRAMDATA%` override with `appsettings.local.json` git-ignored (D51), cold-start budget recorded (D57). **Completed 2026-11-04** — plan §1.1a and §3.1 added, TASK-002a and TASK-002b added, gap 5 closed.

- [x] **TASK-000h** — `003-crosscutting-services-and-theming`: local-first logging with a share copier (D54), WER `LocalDumps` + in-app minidump (D55), configuration store loading committed then override (D51), automation annotations on shared controls (D59). **Completed 2026-11-04** — TASK-035 rewritten, TASK-035a/035b/047a added, plan §2.3 rewritten, gap 6 closed.

- [x] **TASK-000i** — `005`–`009`: 1366×768 at 100 % DPI design target (D58) and the automation checklist (D59), placed once in `constitution.md` §3.1 rather than duplicated five times, with a pointer at the head of each spec's §3. **Completed 2026-11-04**.

- [x] **TASK-000j** — `002-data-access-layer`: deterministic seed instead of production data (D56) and the no-synchronous-database-access-at-launch constraint (D57). **Completed 2026-11-04** — plan §3.3 and spec assumptions updated.

- [x] **TASK-000k** — `011-deployment-and-cutover`: unsigned payload consequence (D52), owner walkthrough as the parity gate (D60), `LocalDumps` and the per-machine override as machine prerequisites (D55, D51). **Completed 2026-11-04** — three edge cases added, TASK-136 extended, TASK-136a added.

- [x] **TASK-000l** — `009-help-and-feedback`: model-drafted topics from the legacy set with owner approval (D62). **Completed 2026-11-04** — spec §3.7 extended, TASK-125a added.

---

## Drift log

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `constitution.md` | Database, UI Kit, Grid, platform floor amended | **confirmed** | Amended 2026-11-04 (TASK-000a) |
| `001-foundation-and-tooling` | Package list, template route, platform floor, pwsh | **confirmed** | Corrected 2026-11-04 (TASK-000b) |
| `002-data-access-layer` | `AllowPublicKeyRetrieval` mandatory; JSON-parameter row | **confirmed** | Corrected 2026-11-04 |
| `003` | Shared grid on `TableView` | **confirmed** | Corrected 2026-11-04 (TASK-000c) |
| `004` | Mica fallback restated against 22H2 | **confirmed** | Corrected 2026-11-04 |
| `006` | Reference grid is a new build | **confirmed** | Corrected 2026-11-04 (TASK-000d) |
| `011` | Manual, evidenced verification | **confirmed** | Corrected 2026-11-04 (TASK-000e) |
| `000` §4.1 | Round 2 complete | **confirmed** | D47–D62 recorded 2026-11-04 (TASK-000f) |
| `001` | Repo shape, SDK pinning + CPM + lock files, config pair, cold-start budget | **confirmed** | TASK-000g — plan §1.1a, §3.1; TASK-002a, TASK-002b |
| `003` | Logging is local-first; crash evidence; config store; automation annotations | **confirmed** | TASK-000h — TASK-035 rewritten, TASK-035a/035b/047a; gap 6 closed |
| `005`–`009` | 1366×768 and automation checklist | **confirmed** | TASK-000i — `constitution.md` §3.1 plus a pointer in each spec's §3 |
| `002` | Deterministic seed; no synchronous DB access at launch | **confirmed** | TASK-000j — plan §3.3, assumptions 1 and 5 |
| `011` | Unsigned consequence, parity walkthrough, machine prerequisites | **confirmed** | TASK-000k — three edge cases, TASK-136 extended, TASK-136a |
| `009` | Help authoring workflow | **confirmed** | TASK-000l — spec §3.7, TASK-125a |
