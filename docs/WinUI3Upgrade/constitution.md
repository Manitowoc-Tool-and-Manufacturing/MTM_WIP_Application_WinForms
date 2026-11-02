# Architecture & Engineering Constitution: WinUI 3 Migration

> **Project**: MTM WIP Application (WinForms `6.8.0`) → **MTM WIP** (WinUI 3)
> **Ratified**: 2026-11-02
> **Scope**: Binding on every project, file, and pull request created under the new WinUI 3 solution.
> **Relationship to existing docs**: `.github/copilot-instructions.md`, `AGENTS.md`, and `.specify/memory/constitution.md` govern the **WinForms** solution and remain in force for it. Where they conflict with this document, **this document wins for new WinUI 3 code** and the older documents win for code still living in the WinForms solution.

---

## 1. Target Tech Stack & Environment

| Concern | Decision |
| :--- | :--- |
| Target Framework | `net10.0-windows10.0.19041.0` |
| Target Platform Minimum | `10.0.17763.0` (Windows 10 1809) |
| UI Framework | WinUI 3 (Windows App SDK, desktop) |
| Packaging | **Unpackaged** (`<WindowsPackageType>None</WindowsPackageType>`) |
| Architecture | `x64` only |
| MVVM | `CommunityToolkit.Mvvm` 8.4.2 — `ObservableObject`, `[ObservableProperty]`, `[RelayCommand]` |
| DI | `Microsoft.Extensions.Hosting` (`IHost`) + `Microsoft.Extensions.DependencyInjection` |
| Database | **MySQL 5.7.24 — frozen. No schema, procedure, or column changes.** |
| DB Provider | `MySqlConnector` |
| Logging | `Serilog` (`Serilog.Sinks.File`) — **replaces** `LoggingUtility` CSV |
| Charts | `LiveChartsCore.SkiaSharpView.WinUI` 2.0.5 |
| UI Kit | `DevWinUI` — `WindowedContentDialog` and selected controls |
| Tests | `xUnit` — see §4 |

### 1.1 Why these versions

These are not preferences; they are the versions already proven in the two sibling WinUI 3 applications, so a defect found in one is a defect fixed in all three.

- `net10.0-windows10.0.19041.0` and `Microsoft.WindowsAppSDK` **2.4.0** — as pinned by `MTM_Waitlist` (`MTM_Waitlist.csproj`).
- `DevWinUI` **10.8.0** ships `net10.0-windows10.0.19041` assemblies, which is why the platform version above is not negotiable.
- `CommunityToolkit.Mvvm` **8.4.2** — as pinned by both `MTM_Waitlist` and `MTM_Receiving_Application`.
- `LiveChartsCore.SkiaSharpView.WinUI` **2.0.5** — as pinned by `MTM_Receiving_Application` and used by `Module_ShipRec_Tools/Views/View_Tool_ReceivingAnalytics.xaml`.

> **Verification step**: at scaffold time, confirm the exact `Microsoft.WindowsAppSDK` patch that `MTM_Waitlist` currently builds with and adopt that identical version. Do not independently upgrade.

### 1.2 Packaging and deployment (unpackaged — required)

The MTM Loader launches this application from a network share by reading a folder that contains `MTM_Inventory_Application.exe` and `build-manifest.json`. An MSIX application cannot be launched this way.

Therefore:

- Packaging is **unpackaged**. `WindowsPackageType` is `None`.
- Deployment is a **self-contained `win-x64`** folder publish, with `PublishSingleFile=false` and `PublishTrimmed=false`.
- The publish is staged, verified file-by-file, swapped into place, and **given its manifest last**, so a half-copied payload is never advertised.
- A version is never reused with a different executable. Bump `<Version>` instead.
- `<Version>` is 3-part semver and is the single source of truth copied into `build-manifest.json`.

### 1.3 Superseded technology — forbidden in new code

The following are **hard forbidden** in any project under the new solution. A pull request containing them fails review:

| Forbidden | Because |
| :--- | :--- |
| `System.Windows.Forms` (any type, any using) | The point of the migration. Enforced by the fact that no project references WinForms. |
| `MessageBox.Show` | Replaced by §2 Rule 5. |
| `Application.Run`, `Application.DoEvents`, `Control.Invoke`/`BeginInvoke` | WinForms lifetime and threading model. See §2 Rule 6. |
| `MySql.Data` | Replaced by `MySqlConnector`. Mixing providers in one process causes duplicate-connector failures and divergent pooling. |
| Inline SQL strings (`"SELECT ..."`) | §2 Rule 3. All data access is stored procedures. |
| `BackgroundWorker` | Replaced by `Task` + `DispatcherQueue`. |
| `Thread.Sleep`, `.Result`, `.Wait()`, `GetAwaiter().GetResult()` on the UI thread | Deadlocks the UI thread. See §2 Rule 7. |
| MySQL 8.0+ SQL syntax — JSON functions, CTEs (`WITH`), window functions (`OVER()`), `CHECK` constraints | The server is MySQL 5.7.24. |
| `LoggingUtility` CSV logging | Replaced by Serilog. |
| The `Core/Theming` engine (`ThemeManager`, `ThemeStore`, all `Appliers`) | Replaced by native WinUI theming. See §2 Rule 9. |

---

## 2. Structural & Architectural Guardrails

- **Core Rule 1**: Code-behind (`.xaml.cs`) files must only contain UI-lifecycle boilerplate. Absolutely no business logic, data access, or state management in the view layer.

  Permitted in code-behind, and nothing else: `InitializeComponent()`, constructor, `OnNavigatedTo`/`OnNavigatedFrom` overrides that only forward to the ViewModel, event handlers that only move a value into the ViewModel or a UI-only service, and disposal. Any `if`, `foreach`, `try`/`catch`, or database call in a `.xaml.cs` is a defect.

- **Core Rule 2**: Legacy event-driven patterns (e.g., `Button_Click`) must be mapped to modern XAML bindings and MVVM `RelayCommand`s (`[RelayCommand]`). A `Click=` in XAML is allowed only when the handler body is a single call that cannot be expressed as a binding (for example, focusing a control). `Executed +=` / `+= new EventHandler` in code-behind is forbidden.

- **Core Rule 3**: WinForms direct graphics or synchronous IO must be refactored into modern asynchronous `Task`s (`async`/`await`) and WinUI 3 compatible rendering. `System.Drawing` (`Bitmap`, `Graphics`, `PrintDocument`) is not used to render UI. Charts use LiveCharts. Export uses ClosedXML. Document output uses a WinUI-native path.

- **Core Rule 4 — The database is frozen.** This migration is a **UI and architecture** migration, not a data migration.

  - The target database is `mtm_wip_application_winforms` (production) and `mtm_wip_application_winforms_test` (tests). Both already exist and are already correct.
  - **No** new table, **no** new or altered column, **no** renamed column, **no** new stored procedure, **no** changed procedure signature, **no** changed `p_Status`/`p_ErrorMsg` contract.
  - The ~120 procedures already called by `Data/Dao_*.cs` are the complete API surface. If a screen appears to need data that no procedure returns, that is an escalation to the database owner — **not** a change made during this port.
  - Parameter names are passed **without** the `p_` prefix from C#; the data helper adds it.

  **Second database — Infor Visual ERP (Microsoft SQL Server, `Microsoft.Data.SqlClient`).** The Visual/Analytics module also reads `MTMFG`/`VMFG` on the `VISUAL`/`VISUALSQL` server. That database is equally frozen and equally read-only. Today those queries are **inline SQL inside `Services/Visual/Service_VisualDatabase.cs`** — the one sanctioned exception to "stored procedures only", because we do not own the ERP schema. The exception is carried forward **only** into `MTM_WIP.Data`, and only for read-only `SELECT` statements: no DDL, no DML, no `INSERT`/`UPDATE`/`DELETE` against Visual, ever.

  **Credentials come from configuration, never from source.** The current connection string reaches the server with **no password clause at all** (the literal source contains a redaction placeholder), and the Visual credentials sit in `App.config` as plain text. The new application reads both connections from a user-scope configuration store, keeps them out of structure logs, and never prints them in an error dialog. A committed credential is a release blocker.

- **Core Rule 5 — One error path.** Every user-visible error flows through a single `IErrorService` that mirrors today's `Service_ErrorHandler`: it logs via Serilog, then presents the user message in a windowed dialog. Dialogs are opened only through `IDialogService`. A ViewModel never constructs a dialog, never touches a `Window`, and never writes a log file directly.

  Severity vocabulary is preserved exactly: `Information`, `Warning`, `Error`, `Critical`, `High` — because these values are stored in `log_error.Severity` and filtered by the existing Developer Tools screens.

- **Core Rule 6 — One window, one UI thread.** The application has exactly one `MainWindow`. Screens are `Page`s in a `Frame`. Document-style secondary windows are allowed only where the spec names them. Modeless "dialog windows" are `DevWinUI.WindowedContentDialog` hosted by a single `IDialogService` wrapper — never a raw `ContentDialog` built at a call site.

  All UI mutation happens on the UI thread via `DispatcherQueue`. No ViewModel may reference a `DispatcherQueue` member directly; an `IDispatcherService` abstraction is injected instead so the ViewModel stays unit-testable (§4).

- **Core Rule 7 — Async all the way down, no exceptions.** Every data-access method is `async Task<T>` returning `Result<T>`. There is no synchronous counterpart and no `bool useAsync` parameter. `ConfigureAwait(false)` is used in library layers only, never in ViewModels or Views.

- **Core Rule 8 — Results, not exceptions, cross the data boundary.** Data methods return a `Result<T>` that mirrors today's `Model_Dao_Result<T>` — `IsSuccess`, `Data`, `ErrorMessage`, `Exception`, `StatusMessage`, `RowsAffected` — and additionally carries the raw `p_Status` as `StatusCode` (the WinForms type discards it, which is why every negative status collapses into one indistinguishable failure). The mapping is preserved **exactly**: any status `>= 0` is success, any status `< 0` is failure, and the `p_ErrorMsg` text is passed through unchanged (§3.4). Throw only for programmer errors (null argument, invalid state), never for a database failure.

- **Core Rule 9 — Native theming only.** WinUI 3 light/dark/system plus a Mica backdrop replaces the entire WinForms theme engine. There is no theme editor, no per-user colour palette, and no JSON theme document in the new application.

  - All colour, brush, corner-radius, and typography values come from `ThemeResource` / `StaticResource` in `Themes/`. A literal `#RRGGBB` or `Colors.X` in a View is a review defect.
  - User preference is one value: the requested theme (`Light`, `Dark`, or `System`). It is persisted, not authored.
  - Mica is applied at window level and falls back gracefully on Windows 10 (where it is unavailable) to a solid `ThemeResource` background — never to an unstyled default.

- **Core Rule 10 — Module boundaries are compile-time, not conventions.**

  - A `Module_*` project may reference `Core`, `Shared`, and `Data` only.
  - A `Module_*` project may **not** reference another `Module_*` project. Cross-module needs are expressed as contracts in `Core`/`Shared`.
  - `Data` may not reference `Shared`, `Core` UI types, or any `Module_*`.
  - `Core` may not reference `Data` or any `Module_*`.
  - Views reference ViewModels (in the same module). ViewModels never reference Views.
  - No project may reference the WinForms solution or its assemblies.

- **Core Rule 11 — No static mutable state.** `Model_Application_Variables` and `Core_WipAppVariables` do not get ported. Every value they held becomes either an injected singleton service or state owned by a ViewModel. A `public static` field or property with a setter on application state is a review defect; `public static` members are limited to immutable constants.

- **Core Rule 12 — Culture and formatting.** All displayed numbers, dates, and currencies use the current UI culture. Persisted and transmitted values use invariant culture. Parametrised SQL is never built by concatenation.

- **Core Rule 13 — Help ships with the feature, not after it.** A feature that changes user-visible behaviour must update its help content **in the same change**. Help is not a documentation phase at the end of the project; it is part of the definition of done for the feature that made it necessary.

  - Help content stays HTML (decision D20). A topic body is rendered in one WebView2 host; the surrounding surface — index, search, feedback — is native.
  - A new or materially changed screen must have a help topic before its task is considered complete. "The help is out of date" is a defect, not a backlog item.
  - The help content lives beside the application as data (`Assets/Help/Content/*.json`), so updating it never requires a rebuild of the help code.
  - A topic must render offline: no CDN, no remote fonts, no remote images, and no temporary file written to disk to display it.

---

## 3. Style & Conventions

### 3.1 XAML conventions

- **Controls**: `PascalCase`, and named only when code-behind or a binding actually needs them (`x:Name="InventoryGrid"`).
- **Resource keys**: `CamelCase` for local keys inside a page (`x:Key="inventoryRowHeight"`), `PascalCase` for shared theme keys in `Themes/` (`x:Key="CardStrokeBrush"`).
- **Converters**: `PascalCase` suffixed `Converter`, declared once in `Themes/Converters.xaml`.
- **Styles**: keyed, never implicit-at-a-distance. `TargetType` styles live in `Themes/`.
- **No inline `Style` setters** repeated across pages — promote to `Themes/`.
- **Namespaces**: `xmlns:controls="using:MTM_WIP.Module_Inventory.Controls"`.

### 3.2 C# conventions

- File-scoped namespaces, 4 spaces, LF endings, nullable enabled, implicit usings enabled (inherited from `.editorconfig`).
- Private fields `_camelCase`; parameters `camelCase`; properties `PascalCase`; constants `PascalCase`.
- Type naming, per the sibling applications:

| Suffix | Meaning | Example |
| :--- | :--- | :--- |
| `View_` | XAML page | `View_InventoryTab` |
| `ViewModel_` | ViewModel | `ViewModel_InventoryTab` |
| `Service_` | business/coordination service | `Service_Inventory` |
| `Dao_` | data access, one per entity family | `Dao_Inventory` |
| `I` + contract | interface in `Contracts/` | `IService_Inventory`, `IDao_Inventory` |
| `Model_` | data model / DTO | `Model_InventoryItem` |
| `Enum_` | enumeration | `Enum_TransactionType` |
| `Helper_` | stateless helper | `Helper_Database_StoredProcedure` |

- `#region` organisation is retained from the WinForms house style, in this order: `Fields`, `Properties`, `Constructors`, `Methods`, `Events`, `Helpers`, `Cleanup / Dispose`.
- XML documentation is required on every public type and public member (`<summary>`, `<param>`, `<returns>`, `<exception>` where thrown).
- Comments explain *why*, not *what*. A comment restating the next line is a defect.

### 3.3 Project and folder layout

```
MTM_WIP.sln
├── MTM_WIP.Core/                # contracts, models, base types, helpers
├── MTM_WIP.Shared/              # reusable views/dialogs/controls used by 2+ modules
├── MTM_WIP.Data/                # DAOs + stored-procedure helper + connection settings
├── MTM_WIP.Startup/             # launch stage, splash, boot sequence
├── MTM_WIP.Module_Shell/        # MainWindow, NavigationView shell, frame
├── MTM_WIP.Module_Inventory/
├── MTM_WIP.Module_Transactions/
├── MTM_WIP.Module_Settings/
├── MTM_WIP.Module_Visual/
├── MTM_WIP.Module_DeveloperTools/   # out of v1 scope; scaffolded empty
├── MTM_WIP/                     # the exe: App.xaml, DI composition root, Themes/
└── MTM_WIP.Tests/
```

Inside every `Module_*`: `Views/`, `ViewModels/`, `Services/`, `Contracts/`, `Models/`, `Enums/`, `Helpers/`, `Converters/` — create a folder only when it has content.

**Where theming resources live.** `Themes/` sits in the **app project** (`MTM_WIP/Themes/`), not in `Core`. Resources consumed by `ThemeResource` must be packaged with the application, and putting them beside `App.xaml` keeps the merge list in one place. `Core` holds contracts, models and services; it holds no XAML.

### 3.4 Preserved contracts

These are legacy behaviours, not accidents. They must survive unchanged:

- The `p_Status` mapping producing `Result<T>` (§2 Rule 8) — including that `0` ("no rows") is a **success**, not an error.
- The `log_error.Severity` vocabulary of §2 Rule 5.
- `usr_settings` JSON keys, `usr_dgv_settings` payload shape, and `%APPDATA%\MTM\PrintSettings\{GridName}.json` — existing user data must load unchanged.
- The `build-manifest.json` schema and the `MTM_Inventory_Application.exe` entry point the loader expects.
- The single-instance behaviour: a second launch brings the running window forward and exits.

---

## 4. Testing & Verification Constraints

- **ViewModels must be unit-testable in isolation using `xUnit` without launching the WinUI 3 UI thread.** A ViewModel that cannot be constructed in a plain `new` without a `DispatcherQueue`, a `Window`, or static state is a defect (§2 Rule 6).

  Practical consequence: every ViewModel dependency is an interface, and UI-thread marshalling goes through `IDispatcherService` whose test double runs the callback inline.

- **Test project**: `MTM_WIP.Tests`, `xUnit`, referencing `MTM_WIP.Core`, `MTM_WIP.Data`, and the module projects. It is **not** a WinUI project and must not require the Windows App SDK at test time. Views and XAML are not instantiated in tests.

- **Integration tests** run against `mtm_wip_application_winforms_test` using the same MySQL 5.7.24 server, with per-test transaction rollback. They never touch `mtm_wip_application_winforms`.

- **Data-layer tests are mandatory for every ported DAO**: one success case, one "no rows" case asserting `IsSuccess == true` with empty data, and one invalid-parameter case asserting the mapped error status. If the procedure cannot be exercised safely, the test is explicitly skipped with a reason and the gap is recorded in `plan.md`.

- **Every ViewModel test** covers: initial state, each `[RelayCommand]`'s happy path, its failure path (service returns failure), its `CanExecute` transitions, and cancellation where `AsyncRelayCommand` is used.

- **Verification before merge** (all must pass):

  1. `dotnet build MTM_WIP.sln -c Debug` — zero warnings introduced.
  2. `dotnet test MTM_WIP.Tests` — green.
  3. `dotnet build` for the WinForms solution — still green, unaffected.
  4. A grep gate with **zero** hits across the new solution for: `System.Windows.Forms`, `MessageBox`, `MySql.Data`, `BackgroundWorker`, `Colors.`, and literal `#` colour hex in `*.xaml`.
  5. Manual smoke test of the touched screen in both `Light` and `Dark`, at 100 % and 150 % display scaling.

---

## 5. Amendment

A pull request that needs to break a rule in this document must first change this document in the same pull request, stating the rule, the reason, and the affected code. An unamended violation is a defect regardless of who wrote it.
