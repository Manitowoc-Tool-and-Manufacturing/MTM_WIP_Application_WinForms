# Technical Implementation Plan: Pre-Foundation Decisions

**Spec**: `000-pre-foundation`
**Tasks**: TASK-000a … TASK-000f

This spec produces no code, so this plan exists to answer the one question the pack's plan template always answers: **which files does this change, and in what order**. The change surface is documentation, and the risk is not compile failure — it is a spec that still contains the old answer somewhere, so a developer follows it and rebuilds the thing this spec just retired.

---

## 1. Project Topology & Directory Mapping

### 1.1 What this spec creates

```text
docs/WinUI3Upgrade/
├── constitution.md               # AMENDED: §1 Database, UI Kit, Target Platform Minimum; §1.1 DevWinUI; §2 Rule 4 note
├── 00-index.md                   # AMENDED: §1 folder tree, §2 spec map, §3 task lookup, §5 register, §6.1 rounds
├── 000-pre-foundation/           # NEW
│   ├── spec.md                   # decisions D28–D46, amendments, downstream changes, open items
│   ├── plan.md                   # this file
│   └── tasks.md                  # TASK-000a … TASK-000f
├── 001-foundation-and-tooling/   # AMENDED: TASK-001, TASK-004, plan §1.2, pwsh prerequisite
├── 003-crosscutting-services-and-theming/   # AMENDED: package table, shared grid target
├── 006-transaction-history/      # AMENDED: reference grid is a new build on TableView
└── 011-deployment-and-cutover/   # AMENDED: verification is manual and evidenced
```

No new solution, project, or file inside a solution. `001` creates those.

### 1.2 Order of application

The order is load-bearing in one direction only: **the constitution is amended first**, because §2 Rule 4 is the rule `002`, `005`, `006`, `007` and `008` all cite when they assert the database is frozen. Amending a spec before the rule it cites is how the two end up contradicting each other.

1. `constitution.md` — TASK-000a.
2. `001` — TASK-000b. It is the first spec executed, so it is the one that must not contain a stale package list.
3. `003` — TASK-000c, because `006` consumes `003`'s shared grid.
4. `006` — TASK-000d.
5. `011` — TASK-000e.
6. Round 2 — **complete**. D47–D62 are recorded and propagated; this folder's `plan.md` §3 covers the configuration and SQL artefacts.

### 1.3 Verification that the amendment actually propagated

`dotnet build` proves nothing here. The check is a grep, and it is deliberately mechanical:

| Search term | Expected after amendment |
| :--- | :--- |
| `10.8.0` | Only in sentences explaining what was superseded (D36). |
| `17763` | Only in sentences explaining what was superseded (D45). |
| `CommunityToolkit.WinUI.UI.Controls` | Only where the archived package is being explained *and rejected* (D37). |
| `MySQL 5.7.24` | Only as the legacy server this migration reads but never writes (D42). |
| `eleven numbered spec folders` | Only in the §"Revision history" row dated 2026-11-02, which is an accurate record of that date. Zero anywhere else — there are twelve now. |

A hit that reads as a requirement rather than a supersession is an incomplete amendment.

---

## 2. Component Migration Strategy

This spec migrates no component. What it does is change **which** component five downstream specs are told to build. Recorded per decision so the amendment task has a target.

| Decision | What it retires | What it substitutes | Where the substitution must land |
| :--- | :--- | :--- | :--- |
| D37 | `CommunityToolkit.WinUI.UI.Controls` 7.1.2 and its 2021 `DataGrid` | Windows Community Toolkit 8.2 per-control packages plus `WinUI.TableView` | `001` TASK-004; `003` package table and shared grid; `006` reference grid |
| D36 | `DevWinUI` 10.8.0 | `DevWinUI` 10.9.1 | `constitution` §1 and §1.1; `001` TASK-004; `003` package table |
| D38 | The 7-control and 12-control build lists | 3 builds, 4 wrappers, 3 adopted, 1 native (`spec.md` §2) | `003`'s shared-control scope; `005`–`009` must not each hand-roll a field row |
| D45 | `TargetPlatformMinVersion` 10.0.17763.0 | 10.0.19041.0 | `constitution` §1; `001` plan §1.2; every Mica assumption in `004` |
| D31 | The assumption that a gate runs automatically | Manual, written evidence | `011` verification phase |

**What is deliberately not changed.** Rule 9 (native theming only) and the Mica fallback survive D45 intact — raising the floor to Windows 10 22H2 does not make Mica available, it only narrows the OS spread. Any amendment that quietly drops the fallback is a defect.

---

## 3. Data & State Migration

This is the section that matters, because the user-facing consequence of D40–D44 is a change in what "migrating the database" even means.

### 3.1 The rule

**Nothing is migrated.** The 5.7.24 server is read-only to this project (D42). What is produced is a **documented restatement** of the existing schema and stored procedures as numbered `.sql` files (D43), which are applied only to `mtm_wip_application_winforms_test` (D44) and asserted there.

This inverts the intuition. It is not "export production and import it into 8.4"; it is "write down what production already is, prove it builds on a clean server, and never touch production while doing so."

### 3.2 Artefact layout

```text
<new repo>/Database/
├── tables/
│   ├── 001-inv_inventory.sql
│   ├── 002-inv_transaction.sql
│   ├── …
├── procedures/
│   ├── 001-inv_inventory_Get_All.sql
│   ├── 002-inv_inventory_Add_Item.sql
│   ├── …                       # ~120 files, one per procedure
└── indexes/
    └── …
```

Numbering mirrors `MTM_Receiving_Application`. The exact convention from that repository has **not** been read yet — `spec.md` §4.2 records it as an open item, and TASK-000f closes it.

### 3.3 The two-version validation problem

D41 is the constraint that makes this section non-trivial: development runs MySQL **26.10.0**, production targets **8.4 LTS**.

- Every `.sql` file is applied to the validation database on the 26.10 instance and asserted.
- Every `.sql` file is **also** applied to a clean 8.4.9 instance, which is already installed on this machine but not running. A construct valid on 26.10 and invalid on 8.4 is the expected failure mode, and it will surface on the second run, not the first.
- Stored procedures are where this bites: `DECLARE … HANDLER`, `GET DIAGNOSTICS`, `ROW_COUNT()`, `FOUND_ROWS()` and `START TRANSACTION` inside a routine all need to be proven on both. The procedure templates in `.github/instructions/stored-procedure-status-codes.instructions.md` are the starting point and are already written for 5.7-era syntax, which is the safest common denominator.
- `FOUND_ROWS()` is deprecated in the 8.x line. It is used throughout the existing procedures. Whether to keep it verbatim (preserving the frozen contract) or replace it is a **spec change**, not a workaround, and belongs to `002` once `002`'s helper is ported.

### 3.4 State this spec introduces

None at runtime. The only persistent artefact is the documentation set above, which lives in the new repository and is not read by the application.
