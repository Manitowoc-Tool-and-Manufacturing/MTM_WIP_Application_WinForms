# WinUI 3 Upgrade — Specification Pack

**Target**: rebuild the MTM WIP WinForms application (6.8.0) as MTM WIP, a WinUI 3 desktop application.
**Ratified**: 2026-11-02
**Status**: Draft for review — the decisions in §5 are settled; the questions in §6 are not.

---

## 1. How the pack is organised

The pack is twelve numbered spec folders plus one shared constitution:

```text
docs/WinUI3Upgrade/
├── constitution.md               # shared, single, binding
├── 00-index.md                   # this file
├── 000-pre-foundation/           # decisions the other eleven assume
│   ├── spec.md                   # ratified; §4 still open
│   ├── plan.md                   # pending Round 2
│   └── tasks.md                  # pending Round 2
├── 001-foundation-and-tooling/
│   ├── spec.md
│   ├── plan.md
│   └── tasks.md
├── 002-data-access-layer/
├── …
└── 011-deployment-and-cutover/
```

Each numbered folder carries three files: `spec.md` (what and why), `plan.md` (how), `tasks.md` (in what order). `constitution.md` is a single shared file at the root, not duplicated per folder, because its amendment rule — a rule may be broken only by changing that same document in the same pull request — breaks the moment copies exist that can drift from each other.

Mandated structure each file uses:

| File | Mandated structure |
| :--- | :--- |
| `spec.md` | Metadata block (`Spec`, `Depends on`, `Unblocks`, `Tasks`, `Governing rules`), then `## 1. Executive Summary & Scope`, `## 2. Legacy Feature Assessment & UI Mapping Table`, `## 3. Behavioral Edge Cases & State Constraints`, `## 4. Gaps, Missing Data & Clarifications` |
| `plan.md` | `## 1. Project Topology & Directory Mapping`, `## 2. Component Migration Strategy`, `## 3. Data & State Migration` |
| `tasks.md` | `## Phase 1 …` phases in dependency order, then `## Drift log` |

---

## 2. Spec map

| Folder | Scope | Tasks | Depends on | Unblocks |
| :--- | :--- | :--- | :--- | :--- |
| [000-pre-foundation](./000-pre-foundation/) | Repository, model policy, toolchain, UI package line and control build list, MVVM depth, database target, platform floor; amends the constitution | TASK-000a … 000f | none | 001, and through it every other spec |
| [001-foundation-and-tooling](./001-foundation-and-tooling/) | Solution skeleton, build settings, package pinning, project reference matrix, forbidden-API gate, assets | TASK-001 … 013 | 000 | every other spec |
| [002-data-access-layer](./002-data-access-layer/) | Result type, DatabaseOptions, stored-procedure helper on MySqlConnector, prefix cache, 22 DAOs, Visual ERP reader, DI | TASK-014 … 034 | 001 | 003 (contracts), 005, 006, 007, 008, 010 |
| [003-crosscutting-services-and-theming](./003-crosscutting-services-and-theming/) | Dialogs, error service, Serilog, dispatcher, current user, master-data cache, preferences, shortcuts, native theming, suggestion control, shared grid, print document model, Excel export, progress, focus, diagnostics | TASK-035 … 052 | 001, 002 | 004 (composition root), 005–009 (every screen) |
| [004-shell-and-startup](./004-shell-and-startup/) | Window, composition root, single instance, launch pipeline, splash, sign-in, first-run user, role gate, global exceptions, exit, NavigationView shell | TASK-053 … 065 | 001, 003 | 005–009 |
| [005-inventory-operations](./005-inventory-operations/) | Inventory entry, Remove, Transfer, Advanced Inventory/Remove, Quick Buttons, right panel, status area | TASK-066 … 084 | 001, 002, 003, 004 | 006, 011 |
| [006-transaction-history](./006-transaction-history/) | Search, results grid, detail panel, analytics cards, batch lifecycle tree, admin delete, export/print; also the reference grid implementation | TASK-085 … 096 | 001, 002, 003, 004, 005 | 011 |
| [007-settings-and-administration](./007-settings-and-administration/) | Category navigation, users, master data, Appearance, Shortcuts, Database page, About + release notes | TASK-097 … 108 | 001, 002, 003, 004 | 011 |
| [008-visual-analytics](./008-visual-analytics/) | Infor Visual ERP tools, native LiveCharts, PO details | TASK-109 … 119 | 001, 002, 003, 004 | 011 |
| [009-help-and-feedback](./009-help-and-feedback/) | Native help over the 15 JSON categories / 54 topics, search, six feedback flows | TASK-120 … 126 | 001, 003, 004 | 011 |
| [010-developer-tools-scaffold](./010-developer-tools-scaffold/) | Developer Tools and Database Maintenance — scaffolded, no behaviour in v1 | TASK-127 … 129 | 001, 004 | 011 |
| [011-deployment-and-cutover](./011-deployment-and-cutover/) | Publish script, build-manifest.json, MTM Loader contract, verification, parity, pack close | TASK-130 … 137 | 001, 005–010 | end of pack |

Folders 004–011 are scaffolded and their three files are still being written; their scope and task ranges above come from the pack table and stand until each spec refines them.

---

## 3. Task → spec lookup

| Task band | Folder |
| :--- | :--- |
| TASK-000a … 000f | [000-pre-foundation](./000-pre-foundation/) |
| TASK-001 … 013 | [001-foundation-and-tooling](./001-foundation-and-tooling/) |
| TASK-014 … 034 | [002-data-access-layer](./002-data-access-layer/) |
| TASK-035 … 052 | [003-crosscutting-services-and-theming](./003-crosscutting-services-and-theming/) |
| TASK-053 … 065 | [004-shell-and-startup](./004-shell-and-startup/) |
| TASK-066 … 084 | [005-inventory-operations](./005-inventory-operations/) |
| TASK-085 … 096 | [006-transaction-history](./006-transaction-history/) |
| TASK-097 … 108 | [007-settings-and-administration](./007-settings-and-administration/) |
| TASK-109 … 119 | [008-visual-analytics](./008-visual-analytics/) |
| TASK-120 … 126 | [009-help-and-feedback](./009-help-and-feedback/) |
| TASK-127 … 129 | [010-developer-tools-scaffold](./010-developer-tools-scaffold/) |
| TASK-130 … 137 | [011-deployment-and-cutover](./011-deployment-and-cutover/) |

A task keeps its global number when it moves between specs — the number is the identity; only the folder it lives in changes. TASK-137 is the pack close.

---

## 4. Reading order for different roles

- **Implementer**: read [constitution.md](./constitution.md) once, then the folders 001 → 011 in order, reading each `spec.md` → `plan.md` → `tasks.md` before starting its first task.
- **Reviewer**: read [constitution.md](./constitution.md) plus the nine rules that matter most for review — §2 Rules 1, 2, 4, 5, 7, 8, 9, 10, 11 (code-behind purity, MVVM commands, the frozen database, the one error path, async-only, results-not-exceptions, native theming, compile-time module boundaries, no static mutable state) — then each spec's §2 mapping table and §4 gaps.
- **Decision-maker**: read only §2, §5, and §6 of this index.

---

## 5. Decision register

| ID | Decision |
| :--- | :--- |
| D1 | Packaging: Unpackaged, WindowsPackageType=None, because the MTM Loader launches a folder payload from a network share. |
| D2 | Solution layout: 11 projects — four foundation libraries plus one Module_* per feature area; boundaries enforced by a test, not review. |
| D3 | MVVM: CommunityToolkit.Mvvm 8.4.2. |
| D4 | Runtime: .NET 10 + Windows App SDK pinned to the MTM_Waitlist version, net10.0-windows10.0.19041.0; DevWinUI 10.9.1 pins the platform version (raised from 10.8.0 by D36). |
| D5 | Navigation: one window + Frame navigation; dialogs via DevWinUI.WindowedContentDialog behind IDialogService. |
| D6 | Strategy: full rewrite; THE DATABASE IS FROZEN — same tables, same ~120 procedures, same parameter names, same p_Status/p_ErrorMsg. |
| D7 | Naming: MTM_WIP.* |
| D8 | Data access: port the stored-procedure helper 1:1, provider switched to MySqlConnector, pooling turned on. |
| D9 | ViewModels: inside each module (Views/, ViewModels/). |
| D10 | DI: IHost + App.GetService<T>(). |
| D11 | Theming: native Light/Dark/System + Mica; the ~200-token colour engine and 18 appliers are DELETED. |
| D12 | Logging: Serilog file sink, day-rolled, 30 retained. |
| D13 | HTML surfaces: rebuilt natively; dashboards become LiveCharts 2.0.5. |
| D14 | Testing: xUnit integration tests against mtm_wip_application_winforms_test with rollback, plus UI-free ViewModel tests. |
| D15 | v1 scope: everything except Developer Tools, which is scaffolded only. |
| D16 | **Cutover: the new application keeps the same executable name (`MTM_Inventory_Application.exe`) and replaces the WinForms payload as a wholesale folder swap once migration is complete.** No loader catalogue change, no second `applicationId`, no side-by-side. Decided 2026-11-02. Consequence: cutover is a single event, so `011`'s verification and parity tasks gate it rather than advise. |
| D17 | **PINs: transparent upgrade.** New PINs are stored hashed with a marker prefix; a stored value without the marker is treated as legacy plaintext and upgraded on the next successful sign-in. No schema change and no mass `UPDATE`. Constraint: `Pin` is `varchar(50)`, so a per-user salt does not fit — use a per-install pepper with a truncated HMAC-SHA256. Decided 2026-11-02. |
| D18 | **Credentials: keep current behaviour, with one improvement.** The WIP connection string continues to carry no password and rely on server configuration. Visual ERP credentials move out of committed source into configuration. **Accepted risk:** a credential in a deployed config file is readable by anyone with access to the share; recorded deliberately, not overlooked. Decided 2026-11-02. |
| D19 | **Printing: HTML/CSS generated from the print document model, printed through WebView2.** No GDI+, no new PDF dependency. The model remains the seam, so the route can be swapped later without touching any screen. Decided 2026-11-02. |
| D20 | **Help stays HTML, rendered in one WebView2 host, and is updated as features are built.** Category index, search and the six feedback flows are native; only the topic body is HTML. The template engine, `{{component:}}` preprocessor, JS bridge and `help://` interception are deleted. Help content is part of every feature's definition of done (constitution Rule 13). Decided 2026-11-02. |
| D21 | **Grids do not sort by column header.** Results are server-paged, so a header sort would order only the visible page and mislead. The sort order is stated in the UI instead. Decided 2026-11-02. |
| D22 | **Email notifications stay a no-op in v1.** Nothing has ever been sent; no behaviour is lost by deferring. Decided 2026-11-02. |
| D23 | **Analytics keeps both "My Analytics" and the Team view.** Both already work; neither is deferred. Decided 2026-11-02. |
| D24 | **Release notes keep both the current release and the version-comparison view**, rendered natively from `RELEASE_NOTES.json`. Decided 2026-11-02. |
| D25 | **The shell has one docked footer bar** carrying the last-saved indicator, transient success/failure text and progress — replacing the old `StatusStrip` in kind rather than splitting it across `InfoBar`, in-page progress and a badge. Decided 2026-11-02. |
| D26 | **Cutover is performed by the application owner on a weekend**, once the WinUI 3 build is ready: the old payload folder is replaced with the new one. No scheduled maintenance window, no pilot phase. Consequence: `011`'s verification and parity tasks are the only thing standing between a defect and every machine. |
| D27 | **The startup AppData wipe is not ported** and needs no replacement. It was clearing stale caches; the new application owns its own cache folder and never deletes it on launch. Decided 2026-11-02. |
| D28–D46 | **Pre-foundation decisions, Round 1** — repository topology, model policy, toolchain, UI package line, control build list, MVVM depth, database target, platform floor, and the constitution amendments they require. Recorded once, in [000-pre-foundation/spec.md §3.1](./000-pre-foundation/spec.md), deliberately **not** duplicated here: a decision kept in two places drifts, which is the same argument §1 makes for keeping the constitution single. Ratified 2026-11-04. |
| D47–D62 | **Pre-foundation decisions, Round 2** — pack governance, repo and solution shape, SDK and package pinning, secrets and configuration location, code signing, PIN pepper, log destination and share resilience, crash evidence, test-data seeding, performance budget, hardware floor, accessibility scope, parity evidence, AI review obligation, help authoring. Recorded in the same place for the same reason. Ratified 2026-11-04. |
| — | The four that change other specs: **D37** replaces the archived Toolkit `DataGrid` with `WinUI.TableView` (rewrites `006`'s reference grid and `003`'s shared grid); **D45** raises the floor to Windows 10 22H2 (changes `001`'s `TargetPlatformMinVersion` and every Mica assumption); **D31** leaves CI absent (`011`'s verification becomes manual and evidenced); **D36** skews `DevWinUI` to 10.9.1 against the fleet's 10.8.0. |

---

## 6. Open questions

| ID | Question | Blocks |
| :--- | :--- | :--- |
| Q1 | ~~PINs are compared in plaintext — hash or accept the risk.~~ | **RESOLVED 2026-11-02** — transparent upgrade: new PINs stored hashed with a marker prefix, legacy plaintext verified and upgraded on next successful sign-in. No schema change, no mass update. Constraint: `Pin` is `varchar(50)`, so the stored form must fit 50 characters. See D17. |
| Q2 | ~~Where shop-floor database credentials come from.~~ | **RESOLVED 2026-11-02** — keep current behaviour: the WIP connection string carries no password and relies on server configuration; Visual ERP credentials move from source into configuration. Accepted risk recorded as D18. |
| Q3 | ~~Print rendering route.~~ | **RESOLVED 2026-11-02** — build HTML/CSS from the print document model and print through WebView2. See D19. |
| Q4 | ~~Help topic renderer.~~ | **RESOLVED 2026-11-02** — HTML topic bodies in one WebView2 host; index, search and feedback native; content kept current per feature. See D20 and constitution Rule 13. |
| Q5 | ~~Column-header sorting on a server-paged grid sorts only the page.~~ | **RESOLVED 2026-11-02** — header sorting removed; the sort order is stated in the UI. See D21. |
| Q6 | ~~The startup routine deletes %APPDATA% and %LOCALAPPDATA% application folders on every launch.~~ | **RESOLVED 2026-11-02** — confirmed as stale-cache clearing; not ported and needs no replacement. See D27. |
| Q7 | ~~Real SMTP or keep the existing no-op.~~ | **RESOLVED 2026-11-02** — keep the no-op in v1. See D22. |
| Q8 | ~~Two hard-coded log paths exist — a network share and a developer's personal OneDrive folder.~~ | **RESOLVED 2026-11-02** — both removed; Serilog writes to a configured per-machine folder (D12). Nothing depends on either path. |

**All eight questions are now closed.** What remains in the individual specs' §4 tables are implementation-time `(to confirm)` items — things a developer verifies while building (exact column widths, a procedure's behaviour on an empty set) — not product decisions.

### 6.1 Pre-foundation questions

Raised after the pack was ratified, because the pack had assumed answers to nine tool-and-platform questions without recording them. Full reasoning and downstream impact are in [000-pre-foundation/spec.md](./000-pre-foundation/spec.md).

| Round | Scope | Status |
| :--- | :--- | :--- |
| **Round 1** | GitHub, DeepSeek, Serena, project creation, DevWinUI, Community Toolkit and the grid, WinUI 3 custom controls, CommunityToolkit.Mvvm, MySQL, Windows floor — 19 decisions | **CLOSED 2026-11-04** — D28–D46 |
| **Round 2** | Eleven categories selected on 2026-11-04: pack governance, repo and solution topology, build reproducibility, secrets and code signing, observability, testing and test-data lifecycle, shop-floor hardware and accessibility, performance budgets, feature-parity evidence, AI-assisted development governance, help authoring — 16 decisions | **CLOSED 2026-11-04** — D47–D62 |
| **Not selected** | OSS licence and dependency-governance policy | **Accepted gap** — recorded, not deferred silently |

**No unasked questions remain in either round.** What is left in `000-pre-foundation/spec.md` §4.2 is accepted risk — consequences chosen knowingly — not open items. The Round 2 decisions are recorded but **not yet propagated** into the specs they change; that is TASK-000g … 000l.

---

## 7. Grounding

This pack was produced by inspecting the WinForms application read-only, not by trusting its prose documentation. Ten parallel agents covered disjoint scopes: data layer, inventory/quick buttons, transactions, settings, visual analytics, developer tools, startup/logging, help, shared dialogs/print/export, and theming. Two sibling applications — MTM_Waitlist and MTM_Receiving_Application — were read for house conventions, and the MTM_Loader_WinUI3 spec for the manifest contract. No claim rests on the project's prose documentation alone; every behaviour cited was found in source. Serena's MCP server and Context7 were unavailable in the session, so code-intelligence and MS Learn were used instead.

---

## 8. The findings that most changed the plan

- **(a) A second database exists**: Infor Visual ERP on SQL Server, read by inline read-only SQL, with credentials previously literal in a committed config.
- **(b) DataGridView → `WinUI.TableView` is a rewrite, not a mapping**: the 12+ screens that own a grid must be rebuilt against `TableView` (the archived Toolkit `DataGrid` is not used — D37), which is why 003 lands a shared grid before any screen uses it.
- **(c) Global mutable state touches nearly every screen**: `Model_Application_Variables` (40+ members) plus static DAOs and a service locator, which is why it is migrated behind interfaces first.

---

## 9. Cross-cutting risks

Risks that span more than one spec. A risk owned by a single spec lives in that spec's §4 Gaps table instead.

| Risk | Impact | Mitigation | Specs |
| :--- | :--- | :--- | :--- |
| `DataGridView` → `WinUI.TableView` is a rewrite on 12+ screens, not a mapping, and the package is single-maintainer | Highest schedule risk in the pack | Build one grid end-to-end as the reference, extract the shared component, then apply it. The reference is `006`. | 003, 006, 007, 008 |
| Printing has no equivalent pipeline — GDI+ draws the page by hand today | Shop-floor paperwork must be visually identical | **Decided (D19)**: the print document model is the seam; it renders to HTML/CSS and prints through WebView2. Prove it on one real report before building the dialog. | 003, 006 |
| `MySqlConnector` error numbers differ from `MySql.Data` | Retry and friendly messages degrade silently | Re-validate the transient set per condition and assert each verdict in a test. | 002 |
| Removing global state touches nearly every screen | Cross-tab regressions (quick buttons, colour codes) | Migrate statics behind interfaces first; keep the WinForms build running for comparison. | 002, 003, 005 |
| Mica is unavailable on Windows 10, which is the supported floor | Unstyled window on some floor PCs | Explicit solid `ThemeResource` fallback per theme; smoke test on a Windows 10 build. | 001, 004 |
| Both generations ship `MTM_Inventory_Application.exe` under loader `id: "wip"` | No side-by-side; cutover is one event, so a bad build reaches every machine at once | **Decided (D16)**: wholesale folder swap with the preserved `.previous-<version>` folder as the only rollback. Full verification and parity walk gate the swap. | 011 |
| Help topic content is HTML | A native renderer may not reproduce nested tables and inline code | Constrain the renderer to the tag set actually used; keep one `WebView2` as the documented fallback. Undecided (Q4). | 009 |
| `DevWinUI` pins the Windows App SDK platform version, and the fleet is now skewed (10.9.1 here, 10.8.0 in the siblings) | An independent SDK upgrade breaks the dialog wrapper | Pin to the `MTM_Waitlist` version unless D36 supersedes it; upgrade all three applications together. | 001, 003 |
| Analytics dashboards currently load Chart.js from a public CDN | Offline shop-floor machines render blank charts | Native charting removes the network dependency entirely. | 008 |

---

## 10. How to use this pack with an AI agent or Spec Kit

1. Do not start a task whose predecessors are unchecked.
2. When code and a spec disagree, the spec wins until the pack is amended.
3. When a task appears to need a database change, stop — that is Q1/Q2 territory, not a code change.
4. The open-questions table is a hard stop, not a note.

---

## 11. Change log

| Date | Change |
| :--- | :--- |
| 2026-11-02 | Restructured from five flat files into eleven numbered spec folders plus a shared constitution; 137 tasks across 11 specs. |
| 2026-11-04 | Added [`000-pre-foundation`](./000-pre-foundation/) ahead of `001` (twelve folders now). Ratified D28–D46: new repository under the org (D29/D30), no CI (D31), BYOK-strict model policy on DeepSeek (D32/D33), Serena on Roslyn (D34), `dotnet new` scaffold (D35), `DevWinUI` 10.9.1 (D36), Windows Community Toolkit 8.2 plus `WinUI.TableView` replacing the archived `DataGrid` (D37), a three-control build list (D38), full MVVM toolkit (D39), MySQL 8.4 LTS target with 26.10 in development and 5.7.24 read-only (D40–D42), numbered SQL documents validated on `mtm_wip_application_winforms_test` (D43/D44), and a Windows 10 22H2 floor (D45). Amended the constitution (§1 Database/UI Kit/Grid/Target Platform Minimum, §1.1, §2 Rule 4, §4 test-server) and corrected `001`, `003`, `006` and `011` accordingly. |
| 2026-11-04 | Round 2 ratified as D47–D62: append-only pack governance (D47), `MTM_WIP` repo shape (D48), `global.json` plus Central Package Management (D49), committed lock files (D50), the `MTM_Receiving_Application` configuration pattern with a per-machine `%PROGRAMDATA%` override (D51, revised), unsigned payload (D52), shared PIN pepper (D53), local-first logging copied to the share (D54), WER `LocalDumps` plus an in-app minidump handler (D55), deterministic test seed (D56), ≤ 5 s cold-start budget (D57), 1366×768 design target (D58), automation annotations only with high contrast out of scope (D59, revised), owner-walkthrough parity gate (D60, confirmed), line-by-line model review with disclosure (D61), and model-drafted help with owner approval (D62). Propagation into `001`, `002`, `003`, `005`–`009` and `011` was completed the same day as TASK-000g … 000l, and the drift log in `000-pre-foundation/tasks.md` is closed. |