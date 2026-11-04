# Functional Specification: Pre-Foundation Decisions

**Spec**: `000-pre-foundation`
**Depends on**: nothing — this is now the first spec in the pack
**Unblocks**: [`001-foundation-and-tooling`](../001-foundation-and-tooling/) and, through it, all eleven
**Tasks**: TASK-000a … TASK-000f (see [`tasks.md`](./tasks.md))
**Governing rules**: [`../constitution.md`](../constitution.md) — which this spec **amends** (§3.4)

> **Status**: §1–§3 are settled and binding (decisions D28–D62, all ratified 2026-11-04).
> §4.1 records every question asked and the decision it became — there are **no unasked questions left**.
> §4.2 is accepted risk, not an open item: each row is a consequence that was chosen knowingly.

---

## 1. Executive Summary & Scope

The pack was written without a recorded decision on nine foundational tool-and-platform questions: where the code lives, how it is built, which UI packages exist, which model writes it, which database it targets, and what hardware it must run on. `001` silently assumed answers to all nine. This spec records them, so `001` stops containing guesses.

Three findings forced decisions that the pack had got wrong:

1. **The grid `003`/`006` depend on is archived.** `CommunityToolkit.WinUI.UI.Controls` 7.1.2 was last published 2021-11-18 and the 8.x line never ported `DataGrid`. Twelve-plus screens were planned against it.
2. **MySQL 8.0 is end-of-life**, and the development machine is not running it — it runs a 26.x Innovation release instead. The pack assumed a frozen 5.7.24 server.
3. **`DevWinUI` is a single-maintainer library of roughly 250 controls**, of which this application uses about ten.

### In scope

- Repository topology, ownership, and the branch/CI model.
- The model policy that governs which model may touch this source.
- The toolchain: code intelligence, project scaffolding, package selection.
- The UI control strategy, down to a named build list.
- The database target, and the boundary between what is migrated and what is only documented.
- The platform floor.

### Out of scope

- Any code, project file, or asset. This spec produces no buildable artefact.
- Anything §4 still lists as open.
- Anything one of the eleven existing specs already settles and this spec does not contradict.

### Success condition

`001` can be executed start to finish without a single sentence in it resting on an assumption recorded here as a guess. Every change this spec mandates on a downstream spec is applied before the change is made in code.

---

## 2. Legacy Feature Assessment & UI Mapping Table

This spec ports no visual controls. The table maps the **hand-built widgets** that existed only because WinForms forced them, and states where each one lands. Every entry was read from `Components/Shared/`, `Helpers/`, and `Controls/` in the WinForms solution.

| WinForms artefact | Target artefact | Functional requirement / Core behavior |
| :--- | :--- | :--- |
| `Component_SuggestionTextBoxWithLabel` + `Helper_ValidatedTextBox` | **Build** `LabeledField` | Label, input, validation state, and inline error in one control with a fixed spacing contract. WinUI 3 ships no `Label` control and `TextBox` has no validation adorners, so this is a real gap, not a port. |
| `Component_EmptyState` | **Adopt** DevWinUI `Empty` | "No results / no data" panel. Delete the hand-rolled version; do not wrap the replacement. |
| `Component_ColumnOrderDialog` | **Adopt** `WinUI.TableView` column options | Column reordering and visibility. `TableView` ships options buttons for both columns and the grid itself. |
| `Component_ProgressBarUserControl` | Native `ProgressRing`/`ProgressBar`, plus **build** `BusyOverlay` | Progress display, and the modal input-blocking overlay that WinUI 3 does not provide. |
| `Component_SuggestionTextBox` | **Wrapper** `FilterableSearchBox` | Filter-as-you-type over `AutoSuggestBox`. `TokenizingTextBox` does **not** self-filter and must not be substituted. |
| `Helper_UI_ComboBoxes` / `Helper_UI_SuggestionBoxes` | **Wrapper** `FilterableSearchBox` | Static helper that populated combo suggestions becomes the wrapper's data contract. |
| `Control_QuickButton_Single` / `Control_QuickButtons` | Not a shared control | Feature UI owned by `005`. Listed to record that it is **not** in the shared build list. |
| `TransactionGridControl` / `TransactionSearchControl` | **Wrapper** `PagedDataGrid` | Server-paged grid chrome over `WinUI.TableView` (§3.3). |
| *(none — WinForms had no equivalent)* | **Build** `MaskedTextBox` | Fixed-format input. Nothing in WinUI 3, WCT 8.2, or the locally catalogued DevWinUI surface provides a general mask; `PinBox` is fixed-length PIN only. |
| *(none — WinForms had no equivalent)* | **Wrapper** `ToastQueue`, **build** nothing | Transient success/failure banner. DevWinUI `Growl` provides the surface; queueing and auto-dismiss are a thin manager, not a control. |
| *(none — WinForms had no equivalent)* | **Wrapper** `PageHeaderBar` | Title, breadcrumb, primary action row. Composed from `BreadcrumbBar` + `CommandBar`; no third-party control justified. |

**Net effect**: three genuinely built controls (`LabeledField`, `MaskedTextBox`, `BusyOverlay`), four thin wrappers (`FilterableSearchBox`, `PageHeaderBar`, `ToastQueue`, `PagedDataGrid`), three adopted (DevWinUI `Empty`, `Growl`, `WindowedContentDialog`), and one native (`NumberBox`). Three WinForms widgets are retired outright. The earlier plan of seven or twelve custom controls is superseded.

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 Decision register

| ID | Decision |
| :--- | :--- |
| D28 | **This spec is `000-pre-foundation/`, placed before `001`. Nothing renumbers.** `001` keeps TASK-001…013; this folder takes TASK-000a…f. Ratified 2026-11-04. |
| D29 | **The WinUI 3 rewrite lives in its own repository.** The WinForms repository is frozen at 6.8.0 and is not modified again. Consequence, recorded deliberately: `MTM_WIP.Core`/`Models` cannot be shared by project reference during the transition, and parity comparison is done by running two binaries, not by comparing two projects in one solution. |
| D30 | **The rewrite belongs to the `Manitowoc-Tool-and-Manufacturing` organisation**, matching this repository and `WinUI3LearningLab`, not to the personal `JDKoll1982` account that holds `MTM_Waitlist` and `MTM_Receiving_Application`. |
| D31 | **Branch model and CI: unchanged from today.** Work is pushed to `master`; there is no branch protection and no GitHub Actions workflow. **Accepted risk, recorded not overlooked:** `001`'s two architecture gates (TASK-007, TASK-008) and `011`'s verification and parity walk become the only thing standing between a defect and every machine, and nothing forces them to run. Revisit if a second developer joins. |
| D32 | **Model policy is BYOK-strict.** No GitHub-Copilot-hosted model may be used on this source — not for implementation, not for review, not for general questions, not for non-proprietary snippets. Every model request is sent to a provider configured by the owner, and the provider list plus each provider's data terms are recorded here. Consequence: there is no second model in the loop and no organisation-managed data agreement behind the one that writes the code. |
| D33 | **The named BYOK provider is DeepSeek**, reached through VS Code's bring-your-own-key configuration. Models in use: `deepseek-v4-pro` for architecture and multi-file work; `deepseek-flash` for boilerplate. Delegated subagents run on `deepseek/deepseek-v4-pro`. |
| D34 | **Serena is adopted, on the Roslyn language-server backend, scoped to `MTM_WIP.sln`.** It requires .NET 10 and PowerShell 7 (`pwsh`) on every development machine. It does not parse XAML, so it is used for symbol navigation over C# only. The `csharp_omnisharp` fallback is not used. |
| D35 | **Projects are created with `dotnet new`** (`Microsoft.WindowsAppSDK.WinUI.CSharp.Templates`), converted to unpackaged, then split into the twelve projects by script. The template set was still preview when this was decided; that preview dependency is accepted in exchange for a reproducible scaffold. |
| D36 | **DevWinUI is pinned at 10.9.1**, the current release, not the 10.8.0 that `MTM_Waitlist` builds with. **Accepted consequence:** the fleet is now version-skewed on a single-maintainer dependency, and the two sibling applications are upgraded together at a later, unscheduled point. |
| D37 | **The UI package line is Windows Community Toolkit 8.2 per-control packages, and the grid is `WinUI.TableView`.** `CommunityToolkit.WinUI.UI.Controls` 7.1.2 and its archived `DataGrid` are **not** used anywhere. **Consequence:** `006`'s reference grid is a new build against `TableView`, not a port; `003`'s shared grid component is built on it; `001` TASK-004's package list changes. |
| D38 | **The custom control build list is three controls plus four wrappers**, exactly as §2. Seven-control and twelve-control alternatives are rejected as unnecessary. |
| D39 | **MVVM uses the toolkit fully**: `ObservableValidator` for form validation, `WeakReferenceMessenger` for cross-ViewModel events, `[RelayCommand]` for every command. Hand-rolled validation in services is rejected. |
| D40 | **The database target is MySQL 8.4 LTS**, not 8.0. MySQL 8.0 reached end-of-life 2026-04-30 and MySQL Workbench 8.0 is likewise end-of-life, replaced by Workbench 26, which officially supports 8.4 LTS and newer only. |
| D41 | **Development and production deliberately run different server versions.** This machine runs MySQL **26.10.0** (`C:\Program Files\MySQL\MySQL Server 26.10`, service `MySQLServer`, the only instance listening on 3306/33060). 8.4.9 is also installed but is not the running service. The cutover target remains 8.4 LTS. **Consequence:** generated SQL must be validated against both versions, and a construct that works on 26.10 but not 8.4 is a live risk for every future stored procedure. |
| D42 | **The existing MySQL 5.7.24 server is never modified by this migration.** No DDL, no DML, no restore against it. The new application's database work is produced as SQL documents (§3.3) and applied only to the validation database. |
| D43 | **SQL is documented as numbered `.sql` files, one per object** — one file per table and one per stored procedure — mirroring how `MTM_Receiving_Application` organises its database scripts. A single `mysqldump` and a two-file split are both rejected. |
| D44 | **The validation database is `mtm_wip_application_winforms_test`.** Generated SQL is applied and asserted there, never against a live server. |
| D45 | **The platform floor is Windows 10 22H2** (`10.0.19041.0`), raised from 1809. This supersedes `001`'s `TargetPlatformMinVersion` of `10.0.17763.0`. Mica remains unavailable on Windows 10, so `004`'s `MicaController.IsSupported()` fallback to a solid `ThemeResource` background is still required and still load-bearing. |
| D46 | **Round 2 scope is the eleven categories selected on 2026-11-04**, listed in §4. **`OSS licence & dependency governance` was explicitly not selected** and is therefore an accepted gap — which is notable given D36 and D37 both increase exposure to single-maintainer packages. |
| D47 | **Pack governance: the owner ratifies, and decisions are append-only.** A changed decision receives a new `D` number, and the superseded row is struck through with the number that replaced it — never deleted. The pack therefore carries a history of what was believed at each point, which is the only defence a solo author has against quietly re-deciding something. |
| D48 | **Repository and solution are named `MTM_WIP`** (repo, solution file, and `MTM_WIP.Module_*` projects), mirroring `MTM_Waitlist` rather than adopting `WinUI3LearningLab`'s `src/`+`tests/` layout. Consistency with the fleet beats a cleaner layout, because a developer moving between the three applications sees one shape. |
| D49 | **Both `global.json` and Central Package Management are adopted.** The .NET 10 SDK feature band is pinned, and every package version lives in one `Directory.Packages.props`. Neither sibling has either file; this application becomes the only one where the toolchain is reproducible, and the other two retain the drift they already have. |
| D50 | **NuGet restore is locked.** `RestorePackagesWithLockFile` is enabled and `packages.lock.json` is committed, so a changed transitive dependency fails the build instead of arriving silently. A package upgrade becomes a visible diff. |
| D51 | **Configuration copies the `MTM_Receiving_Application` pattern.** A committed `appsettings.json` beside the executable carries every non-secret setting plus the Serilog configuration, with credentials redacted exactly as that repository does; an optional untracked `appsettings.local.json` overrides any key and supplies the real Visual ERP credential; both load through `AddJsonFile`, and values bind via `Configure<DatabaseSettings>` on the `ConnectionStrings` section plus `GetConnectionString`. **One deliberate difference:** that repository loads its override from `AppContext.BaseDirectory`, but the MTM Loader swaps the payload folder wholesale (D16, D29), so a file beside the executable is destroyed on every release. The override therefore loads from `%PROGRAMDATA%\MTM_WIP\appsettings.local.json` — same filename, same keys, same precedence, one directory that survives a swap. **Consequence:** `appsettings.local.json` must be git-ignored, because a committed credential is a release blocker. |
| D52 | **The payload is unsigned.** Accepted because the MTM Loader launches the executable directly, where SmartScreen does not intervene. **Accepted consequence:** the first time anyone runs it from Explorer — a developer, a support technician diagnosing a fault, a user who finds the `.exe` on the share — Windows shows a SmartScreen warning attributed to an unknown publisher. |
| D53 | **The D17 PIN pepper is a single value in the deployed configuration, identical on every machine.** This is the only option that lets a shared database verify a PIN set on a different PC. **Accepted consequence:** anyone holding that configuration can attack a 4-digit PIN space offline, so the pepper raises the cost of a database-only leak and does nothing against a configuration leak. |
| D54 | **Serilog writes locally first, always, and a background copier pushes to the share.** This supersedes the straight-to-UNC answer recorded as Q27, which research showed would throw in the `FileSink` constructor and fail to start the application when the share was unreachable, then silently discard events if the share dropped mid-session. **Consequence:** startup can never depend on the network, nothing is lost, and support reads the share one copy interval behind. |
| D55 | **Both crash-evidence mechanisms are used.** Windows Error Reporting `LocalDumps` is configured per application, and an in-application managed handler also writes a minidump. The first catches access violations and stack overflows that no managed handler can see; the second catches managed exceptions with better context. **Consequence:** LocalDumps needs a one-time administrator step per machine, which must be part of machine setup and is not something the payload can do for itself. |
| D56 | **The test database is populated from a deterministic seed script**, never from production data. No real part numbers, locations or transactions leave the frozen 5.7.24 server, and every test run starts from an identical state. |
| D57 | **Cold start is budgeted at ≤ 5 s to interactive and ≤ 1.5 s for the first grid page**, measured on floor hardware launched from the share. A miss is a defect. Microsoft publishes no WinUI 3 cold-start figures, so this budget is a commitment, not a benchmark — and it is the constraint that bars synchronous database work during launch. |
| D58 | **The design target is 1366×768 at 100 % DPI, scaling upward.** Everything must be usable at the smallest size, which constrains every layout in `005`–`009`. Touch and high-DPI are supported but are not the design point. |
| D59 | **Accessibility scope is automation annotations only.** Every interactive control carries `AutomationProperties.Name` matching its visible label, plus `HelpText` where the label is not self-explanatory and a stable `AutomationId` for tooling. **High contrast and text-scaling hardening are explicitly out of scope** — recorded as accepted gaps, not oversights. Keyboard reachability is not counted as accessibility work; it is baseline usability, because the floor runs keyboard-wedge scanners. **Consequence:** the application will not be reliably legible under a Windows High Contrast theme. The annotations pay a second dividend, though: they are what makes the D60 walkthrough repeatable and a future automated pass possible at all. |
| D60 | **Feature parity is evidenced by an owner walkthrough** of every screen, recorded in writing. **Accepted risk, and the sharpest one in the pack:** with no CI (D31), no pilot, and a single weekend swap (D26), one person's walkthrough is the entire barrier between a defect and every machine. There is no second pair of eyes and no automated check behind it. |
| D61 | **Every model-authored change is reviewed line by line before commit, and the commit records which files were generated.** This is the only control that survives D32's removal of a second model and D31's removal of CI. |
| D62 | **Help topics are drafted by the model from the existing 54 WinForms topics as each feature is built, then edited and approved by the owner.** This satisfies Constitution Rule 13 without requiring every topic to be written from scratch, and keeps help on the same clock as the feature. |

### 3.2 The grid decision, stated plainly

D37 is the highest-risk decision in this spec, so its reasoning is recorded rather than referenced.

- The pack pinned `CommunityToolkit.WinUI.UI.Controls` 7.1.2. Its `DataGrid` was last published **2021-11-18**. The 7.x→8.x migration guide states there was an abandoned successor and that the 7.x grid is safe to use alongside 8.x packages but is unmaintained. NuGet reports `net5.0-windows10.0.18362` as the only explicit target framework; every later TFM is inference, not a vendor-tested target.
- `WinUI.TableView` is MIT, derived from `ListView` (so it inherits WinUI's own styling and virtualisation), actively released, and ships column filtering, frozen columns, row details and CSV export. Its existence was independently verified against NuGet during this decision, because a research pass asserted the opposite.
- The cost is real and is accepted: `006`'s reference grid is rebuilt, and `003`'s shared grid component is written against a different API than `003`'s plan currently describes.

### 3.3 State and persistence introduced

None in code. Two new artefacts are introduced as documents only:

1. The numbered `.sql` object files of D43, validated per D44.
2. This spec's own decision register, which is the record against which `001`–`011` are checked.

### 3.4 Constitution amendments this spec requires

`constitution.md` states that a rule may be broken only by changing that same document in the same pull request. These four changes must land with this spec, not after it:

| Constitution location | Currently says | Must say |
| :--- | :--- | :--- |
| §1 table, `Database` row | `MySQL 5.7.24 — frozen. No schema, procedure, or column changes.` | `MySQL 8.4 LTS (target). Schema, procedures, columns and parameter names remain frozen — no DDL, no changed signature, no changed p_Status/p_ErrorMsg contract. Only the server version changes.` |
| §1 table, `UI Kit` row and §1.1 bullet | `DevWinUI 10.8.0` | `DevWinUI 10.9.1` |
| §2 Rule 4 | MySQL 5.7.24 frozen; "no new stored procedure" | Unchanged for schema and procedures. **Additionally**: the existing 5.7.24 server is read-only to this migration (D42), and no MySQL 8.0+-only syntax may be relied upon unless it is also valid on 26.10 (D41). |
| §1, `Target Platform Minimum` | `10.0.17763.0` (Windows 10 1809) | `10.0.19041.0` (Windows 10 22H2) |

**Not amended, deliberately:** Rule 9 (native theming only) survives D45 intact — Mica is still unavailable on the supported floor, so the fallback stays mandatory.

### 3.5 Downstream changes this spec mandates

| Spec | Change required |
| :--- | :--- |
| `001` | TASK-004's package list: drop `CommunityToolkit.WinUI.UI.Controls`, add the WCT 8.2 per-control packages and `WinUI.TableView`; pin `DevWinUI` 10.9.1; add `pwsh` 7 as a stated prerequisite for TASK-003's verification (D34). `TargetPlatformMinVersion` becomes `10.0.19041.0` (D45). |
| `001` | TASK-001's scaffold route is `dotnet new` (D35), which makes the "from the WinUI 3 blank-app template" wording in TASK-001 explicit about which template source. |
| `003` | The shared grid is written against `WinUI.TableView`, not the Toolkit `DataGrid`. The `DevWinUI` version in its package table becomes 10.9.1. |
| `006` | The reference grid is a new build on `TableView` (D37, §3.2), and the risk table row asserting `DataGridView → Toolkit DataGrid` is a rewrite becomes a rewrite onto `TableView` instead. |
| `011` | No CI exists (D31), so every verification and parity step is manual and must be evidenced in writing rather than by a green check. |
| all | Any `TargetPlatformMinVersion` or Mica assumption written against 1809 (D45). |

---

## 4. Gaps, Missing Data & Clarifications

### 4.1 Round 2 — asked and closed 2026-11-04

D46 fixed the scope; these are the questions that scope produced and the decision each one became. **There are no unasked questions left in Round 2.**

| # | Category | Question asked | Decision |
| :--- | :--- | :--- | :--- |
| 1 | Spec-pack governance & numbering | Who may change a decision, and how is a change recorded? | D47 — owner ratifies, append-only |
| 2 | Repo & solution topology | What is the repo/solution/folder shape? | D48 — `MTM_WIP` throughout, mirroring `MTM_Waitlist` |
| 3 | Build reproducibility & SDK pinning | `global.json`? Central Package Management? | D49 — both |
| 4 | NuGet restore strategy | Lock files, local feed, or default? | D50 — committed `packages.lock.json` |
| 5 | Secrets & configuration | Where do the connection string and Visual ERP credential live? | D51 — `MTM_Receiving_Application` pattern, override in `%PROGRAMDATA%` |
| 6 | Code signing | Signed, self-signed, or unsigned? | D52 — unsigned, accepted |
| 7 | PIN pepper location | D17's per-install pepper contradicts a shared database. Where does it live? | D53 — one shared value in configuration |
| 8 | Observability | Where does Serilog write, and how does it survive a dead share? | D54 — local first, copied to the share |
| 9 | Crash evidence | What exists after a hard crash? | D55 — WER `LocalDumps` plus an in-app handler |
| 10 | Testing & test-data lifecycle | How is `_test` populated with the live server frozen? | D56 — deterministic seed, no production data |
| 11 | Performance budgets | What is the cold-start and grid-paging target? | D57 — ≤ 5 s / ≤ 1.5 s |
| 12 | Shop-floor hardware & accessibility | What screen size, DPI and input must work? | D58 — 1366×768 at 100 %, scaling up |
| 13 | Accessibility scope | How far does accessibility go, given there is none today? | D59 — automation annotations only; no high contrast |
| 14 | Feature-parity evidence | What proves parity before a no-pilot swap? | D60 — owner walkthrough |
| 15 | AI-assisted development governance | What is the review obligation under BYOK-strict? | D61 — line-by-line review plus disclosure |
| 16 | Help authoring workflow | Who writes the 54 topics, and when? | D62 — model-drafted per feature, owner approves |

### 4.2 Accepted gaps

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | OSS licence & dependency-governance policy | **Not selected for Round 2.** D36 pins a single-maintainer library at its newest release; D37 adds a second single-maintainer package (`WinUI.TableView`). Both are MIT, neither has a written acceptance. |
| 2 | Evidence produced before D32 | The four research reports and the first control-gap analysis that informed D28–D46 were produced on GitHub-Copilot-hosted models, before the BYOK-strict policy existed. They are treated as pre-policy evidence and were re-verified against NuGet where a decision depended on them (D37, §3.2). |
| 3 | Whether any floor machine is actually below Windows 10 22H2 | D45 raises the floor on the assumption that none is. Unverified. If one exists it is excluded by this spec. |
| 4 | MySQL 26.10 vs 8.4 LTS syntax divergence | D41 accepts the split without enumerating which constructs differ. The first divergence discovered becomes a spec change, not a workaround. |
| 5 | `MTM_Receiving_Application`'s SQL layout | D43 mirrors it, but its exact numbering convention has not been read and adopted formally. |
| 6 | **Machine-level configuration override (D51)** | One `%PROGRAMDATA%\MTM_WIP\appsettings.local.json` per machine, created by hand or by whatever provisions the machine. Nothing seeds it, and the Database settings page in `007` is the only in-app recovery path. If it is edited with a bad credential, the application cannot reach the database to fix it. |
| 7 | **Unsigned payload (D52)** | Nothing prevents a SmartScreen warning if the executable is ever launched from Explorer or the share rather than by the loader. |
| 8 | **Parity trusts one person (D60)** | With no CI and no pilot, a single owner walkthrough gates the cutover. A defect the owner does not think to try reaches every machine. |
| 9 | **No high contrast, no text scaling (D59)** | Both are out of scope by decision. Under a Windows High Contrast theme the application will render with whatever brushes were not expressed as `ThemeResource`, and is not promised to be legible. Accepted knowingly. |
| 10 | **PIN pepper is shared (D53)** | One value protects the database-leak case only; a configuration leak defeats it entirely. |
| 11 | **`LocalDumps` needs admin per machine (D55)** | The payload cannot configure its own crash capture. If machine setup omits it, hard crashes leave no dump — and nobody discovers that until one happens. |

### 4.3 Assumptions

1. `pwsh` 7 can be installed on every development machine that runs Serena (D34).
2. The owner is the only person who will run the rewrite for now, which is what makes D31's absent CI survivable.
3. A BYOK provider is reachable from the development network and its terms are acceptable for proprietary manufacturing source code (D32, D33).
