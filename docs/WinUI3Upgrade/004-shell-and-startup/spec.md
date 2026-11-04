# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `004-shell-and-startup`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`, `003-crosscutting-services-and-theming`
**Unblocks**: `005`–`009` (every screen assumes the shell and launch pipeline exist), `011` (publishing assumes the executable and window exist)
**Tasks**: TASK-053 … TASK-065
**Governing rules**: [`../constitution.md`](../constitution.md) §1, §2 Rules 5, 6, 9, 11; §3.4

---

## 1. Executive Summary & Scope

This spec builds the application's lifetime and its shell: the one window, the composition root, the single-instance guard, the launch pipeline (splash → sign-in → first-run user creation → role gate → shell), global exception handling, orderly exit, and the NavigationView-based navigation shell with its navigation services.

The WinForms application boots through a synchronous `Program.Main` and a splash `ApplicationContext` that runs a 13-step sequence, deletes two AppData folders on every launch, performs a synchronous blocking database call before any UI exists, and pumps messages with `Application.DoEvents`. None of that survives. The new application boots through an `IHost` composition root, swaps launch stages inside a single `ContentPresenter`, and does all I/O asynchronously with the result types from `002`.

### In scope

- The composition root: `IHost` built in `App.xaml.cs`, the `AddAppServices` registration shape, `App.GetService<T>()`.
- The single `MainWindow` (WinUIEx `WindowEx`): Mica backdrop with a Windows 10 fallback, unified title bar, and a `StageHost` `ContentPresenter`.
- The single-instance mutex and bring-existing-instance-to-front behaviour.
- The launch pipeline as an ordered set of named stages, each marked critical or non-critical.
- The splash stage, the shared sign-in stage, the first-run user-creation stage, and the role gate.
- Global exception handling and the `Fatal`-to-orderly-exit path.
- Orderly exit and the relaunch-then-exit replacement for `Application.Restart`.
- The NavigationView shell, the navigation service, and the route table.
- App-wide accelerators (the replacement for `MainForm.ProcessCmdKey`).

### Out of scope

- The operation screens themselves (New / Remove / Transfer / Quick Buttons / advanced variants) — `005`.
- Transaction search, grid, lifecycle, analytics — `006`.
- Settings, users, master data, shortcuts pages — `007`.
- Error reports, developer tools — `008` / `010`.
- Help — `009`.
- Publishing and the loader payload — `011`.
- Any database change. The database is frozen (`constitution.md` §2 Rule 4).

### Success condition

`dotnet build MTM_WIP.sln -c Debug` and `dotnet test MTM_WIP.Tests` pass; launching the exe shows a splash, runs the pipeline, lands on the NavigationView shell signed in as the correct user; a second launch brings the first window forward and exits; and a grep gate finds no `Application.DoEvents`, no synchronous `IdentifyUser` call, and no AppData wipe in the new solution.

---

## 2. Legacy Feature Assessment & UI Mapping Table

### 2.1 Boot order and lifetime

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Program.Main` boot order: mutex → debug init → global exception handling → cleanup hooks → WinForms init → `ConfigureServices` → logging no-op → `IdentifyUser` → fire-and-forget `SyncOnStartupAsync` → `SetupTrace` → `Application.Run(splash)` → `finally` (release mutex, dispose, cleanup, `Environment.Exit(0)`) | `App.xaml.cs` builds an `IHost`; `MainWindow` activates after the host is ready. The mutex, exception handling, cleanup hooks and orderly exit are split into named services | Same ordering intent, but the boot sequence becomes the launch pipeline (§2.2), not a single `Main` method. `Environment.Exit(0)` is replaced by the orderly-exit path (§3.7). |
| `new Mutex(true, "Global\\MTM_WIP_Application_Winforms_SingleInstance", out isNewInstance)`; if not new → `MessageBox` + `BringExistingInstanceToFront()` + return | `Global\MTM_WIP_WinUI3_SingleInstance` mutex (name fixed by [`001`](../001-foundation-and-tooling/spec.md)), acquired before the window exists; on failure, bring the existing instance forward and exit | A second launch must never open a second window. The two applications use different mutex names so both can run side by side during the transition. |
| `BringExistingInstanceToFront`: `GetCurrentProcess` → `GetProcessesByName` → first other PID with `MainWindowHandle != 0` → `ShowWindow(SW_RESTORE=9)` if `IsIconic` → `SetForegroundWindow`. No IPC, no argument forwarding | Same P/Invoke sequence, no IPC, no argument forwarding | Preserved exactly. The loader launches the exe with no meaningful arguments; nothing is forwarded. |
| `Service_OnStartup_AppLifecycle.SetupGlobalExceptionHandling` (winForms `ThreadException` + `AppDomain.UnhandledException`) | `App.UnhandledException` + `AppDomain.CurrentDomain.UnhandledException`, routed to the branch logic in §2.5 | WinUI 3 has no `Application.ThreadException`; the two hooks are the WinUI/CLR equivalents. |
| `Service_OnStartup_AppLifecycle` cleanup hooks (`AppDomain.ProcessExit` + `Application.ApplicationExit`) | `AppDomain.ProcessExit` + `MainWindow.Closed` (or `App` exit) → `PerformAppCleanup` equivalent | Cleanup still runs on exit; the exact list of cleanup actions is ported, not invented (§3.7). |
| `Service_OnStartup_System.InitializeWinForms` (`SetHighDpiMode(PerMonitorV2)`, `EnableVisualStyles`) | Not needed | WinUI 3 is per-monitor-DPI aware by default and has no visual-styles switch. |
| `ConfigureServices` → `ServiceProvider` (static, reachable from anywhere) | `IHost` built once in `App.xaml.cs`; `App.GetService<T>()` throws if unregistered | The service provider is not a global mutable static; it lives on the `App` instance. See §3.9. |
| `Service_OnStartup_User.IdentifyUser()` — **synchronous blocking** `Dao_System.System_GetUserName()` with fallbacks `Environment.UserName` then `"SHOP2"` | An async identity probe in the launch pipeline, using `IDao_System`, with the same fallback chain | The blocking call is not carried over (§3.8). The fallback chain and the resulting "shared account" gate for sign-in are preserved. |
| `_ = SyncOnStartupAsync()` fire-and-forget error-report sync | A non-critical pipeline stage (log and continue) | Same behaviour: sync happens in the background and its failure never stops launch. |

### 2.2 Launch pipeline (splash)

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Service_Onstartup_StartupSplashApplicationContext.RunStartupAsync` — 13 sequential awaited steps | `ILaunchPipeline` + ordered `IStage` implementations in `MTM_WIP.Startup` | The full sequence is preserved as an ordered stage list; each stage is tagged critical (failure stops launch) or non-critical (log and continue). The stage list is in §3.1. |
| `SplashScreenForm.UpdateProgress` calling `Application.DoEvents()` | A splash `Page`/`UserControl` bound to an `IProgress<LaunchProgress>` | `DoEvents` is forbidden (`constitution.md` §1.3) and not reproduced (§3.8). Progress is reported through `IProgress`, rendered by binding, not by pumping a message loop. |
| Progress 0 "Starting startup sequence" → `await Task.Delay(100)` → `ExecuteStartupSequenceAsync` | Splash shown, then the pipeline starts after the first frame renders | The short initial delay exists to let the splash paint. Kept as a non-blocking `await`, never a `Thread.Sleep`. |
| Stage 5 `LoggingUtility.InitializeLoggingAsync`; Program step 7 `InitializeLogging` (a no-op because paths are empty) | Serilog configured in `UseSerilog(...)` during host build; no pipeline stage | Logging is configured before any stage runs, which is the whole point of the old no-op's intended-but-missing behaviour. |
| Stage 15 `CleanUpOldLogsIfNeededAsync` | Serilog `retainedFileCountLimit: 30` (from `003`) plus a startup log-cleanup stage if still needed | Log retention is owned by the sink. Any residual cleanup is a non-critical stage. |
| Stage 25 `Task.Run(WipeAppDataFolders)` — deletes `%APPDATA%\MTM_WIP_Application_Winforms` and `%LOCALAPPDATA%\MTM_WIP_Application_WinForms` | **Not carried over** | Confirmed intentional deletion, but a folder that is wiped cannot hold preferences. `003` §3.6 already forbids startup deletion; this spec re-affirms it (§3.8). |
| Stage 31 shared-login callback, **only if** the identified user is `"SHOP2"` or `"MTMDC"` | Sign-in stage, conditional on the same two account names | The shared/kiosk accounts still get a login gate; individually identified users skip it. |
| Stage 32 `LoadUserSettingsAsync` | Non-critical stage populating `IUserPreferences` and the initial theme state | Same stored keys (from `003`). |
| Stage 38 `ValidateConnectivityAsync` | Critical stage probing `IDao_System` connectivity | If the database is unreachable the app fails gracefully, exactly as today. |
| Stage 45 `Task.Run(InitializeParameterCache)` — non-critical, failure continues | Non-critical stage populating `IParameterPrefixCache` (from `002`) | `002` §2.6 owns the cache; `004` only schedules the population. |
| Stage 50 `LoadUserAccessAsync`; if no role flag set → `Form_QuickUserCreation`, cancel = fail | Role gate stage: load roles into `ICurrentUserService`; if the user has no role, run the first-run user-creation stage; cancel = abort launch | The "no role → create" branch is preserved. See §3.5. |
| Stage 60 `Helper_UI_ComboBoxes.SetupDataTables`; stage 66 `ReloadColorCodePartsAsync` (swallowed) | One non-critical stage refreshing `IMasterDataCache` (from `003`) | Two refresh triggers that can drift apart collapse into one cache refresh. |
| Stage 70 `Service_Timer_VersionChecker.Initialize` (swallowed) | Non-critical version-check stage | Failure is logged and launch continues. |
| Stage 80 theme init + `LoadFromDatabaseAsync` + `SetThemeAsync`; stages 90–93 read `GetThemeEnabledAsync` / `GetAnimationsEnabledAsync` / `GetThemeFontSizeAsync` / `GetUserThemeColorsAsync` | Non-critical `IThemeService` initialisation from `003` | The per-user colour palette (`GetUserThemeColorsAsync`) has no WinUI equivalent and is not applied; only the requested mode (`Light`/`Dark`/`System`) survives (`003` §3.2). |
| Stage 95 `Task.Delay(200)` → `new MainForm()`; stage 100 `Task.Delay(50)` → `ShowMainForm` → close splash | Shell handoff stage: swap `StageHost` to the shell, close the splash stage | One window throughout; the splash is a stage, not a second form. |
| `ConfigureFormInstances()` assigning a static `MainFormInstance` on 8 controls | Deleted | `003`/`constitution.md` Rule 11: no static mutable state. Controls obtain dependencies by injection. |

### 2.3 Sign-in and first-run user creation

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `Form_SharedLogin`: username + PIN text boxes, `MaxAttempts = 3`, PIN compared as **plaintext** against the `Pin` column, failure message `"Invalid username or PIN.\nAttempts remaining: N"` + refocus, third failure `"Maximum login attempts exceeded. Application will close."` with `DialogResult.Abort`; Cancel = Cancel; caller sets the global user | `View_SignIn` stage + `ViewModel_SignIn` in `MTM_WIP.Startup` | **Upgraded per D17.** New PINs are stored hashed with a marker prefix; a value without the marker is legacy plaintext and is upgraded on the next successful sign-in. Three attempts, the exact messages, and abort-on-third are preserved unchanged. |
| `Form_QuickUserCreation`: first/last name, shift combo, username, PIN, optional Visual user/pass; creates via `Dao_User.CreateUserAsync` with role id 3 (Normal); no-spaces `KeyPress` filter; duplicate check `UserExistsAsync` | `View_QuickUserCreation` stage + `ViewModel_QuickUserCreation` in `MTM_WIP.Startup` | Same fields, same role id 3, same no-spaces validation, same duplicate check. Creation goes through `IDao_User`. |

### 2.4 Shell and navigation

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `MainForm` with `MenuStrip` (File / View / Tools / Help), a `TabControl` with New / Remove / Transfer, a `SplitContainer` with Quick Buttons on the right, a `StatusStrip` (progress bar + status text + saved indicator), and a `ProcessCmdKey` override for app-wide hotkeys | `View_Shell` (`NavigationView` + `Frame`) in `MTM_WIP.Module_Shell`; app-wide accelerators via a shortcut/accelerator service from `003`; **one docked shell footer bar** (D25) | Only the shell, its menu, its navigation, its footer and its accelerators are built here. Tabs, advanced variants and Quick Buttons belong to `005`. The menu maps to NavigationView items; the `StatusStrip` becomes the shell footer, which owns the last-saved indicator, transient success/failure text and progress for whichever page is showing. |
| `MainFormInstance` static used by 8 controls | Deleted | See §3.9. |
| `SettingsForm.OnFormClosing` releases the mutex and calls `Application.Restart()` | Orderly relaunch: release the mutex, start a new process, exit the current one | WinUI 3 has no `Application.Restart`. Relaunch-then-exit is required (§3.7). |

### 2.5 Global exception handling

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `ThreadException` + `AppDomain.UnhandledException` → log → `HandleGlobalException` | `App.UnhandledException` + `AppDomain.UnhandledException` → `IErrorService` (log) → branch logic | Same branch table below. All presentation goes through `IDialogService`; no `MessageBox`. |
| Branch: `MySqlException` → Fatal + retry restart | Same via `MySqlConnector`'s `MySqlException` | Fatal, offer restart. |
| Branch: `InvalidOperationException` containing `"database"` → DB config dialog | Same | Route to a database-configuration flow through `IDialogService`. |
| Branch: `FileNotFound` / `DirectoryNotFound` → file dialog | Same | — |
| Branch: `UnauthorizedAccess` / `SecurityException` → security dialog | Same | — |
| Branch: `COMException` / `ExternalException` → system dialog | Same | — |
| Branch: `TimeoutException` → timeout dialog | Same | — |
| Branch: else → `ShowFatalError` and exit | Same, through the orderly-exit path (§3.7), not `Environment.Exit` | Fatal termination goes through `004`'s orderly exit. |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 The launch pipeline stage list

Ordered, and each stage is tagged critical (failure stops launch and shows a fatal error through `IErrorService`) or non-critical (log and continue). Ordering matches the legacy sequence; the deliberate differences are in §3.8.

| # | Stage | Critical | Legacy reference | Populates / Depends on |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Host built; Serilog live; window created, splash shown | critical | Program 5/7 + splash 0/5 | `IHost`, `ILogger<T>` |
| 2 | Database connectivity probe | critical | 38 | `IDao_System` |
| 3 | Parameter-prefix cache population | non-critical | 45 | `IParameterPrefixCache` (`002`) |
| 4 | Sign-in (conditional: only `"SHOP2"` / `"MTMDC"`) | critical | 31 | `IDao_System`, `ICurrentUserService` |
| 5 | First-run user creation (conditional: user has no role) | critical | 50-adjacent | `IDao_User` |
| 6 | User access / role gate | critical | 50 | `ICurrentUserService` |
| 7 | User settings load | non-critical | 32 | `IUserPreferences`, `IThemeService` |
| 8 | Master-data cache refresh | non-critical | 60 + 66 | `IMasterDataCache` |
| 9 | Version check | non-critical | 70 | version-check service (ownership to confirm, §4) |
| 10 | Theme apply | non-critical | 80 + 90–93 | `IThemeService` |
| 11 | Shell handoff | critical | 95 + 100 | `StageHost` → `View_Shell` |

Stages 4 and 5 are conditional and mutually ordered: sign-in must complete before the role gate can decide whether the first-run creation stage is needed. Stage 7 is marked non-critical deliberately: preferences and theme state have defaults, and a settings-read failure must not stop the shop floor. This is a decision, not a legacy guarantee (legacy awaited it with no catch).

### 3.2 One window, one Mica backdrop, one title bar

- `MainWindow` is the only window. All launch stages swap inside `StageHost` (`ContentPresenter`); after handoff the shell `Frame` hosts `Page`s. No screen may open a second window (`constitution.md` §2 Rule 6).
- `Window.SystemBackdrop = new MicaBackdrop()` when Mica is available. The minimum supported platform is Windows 10 22H2 (`000-pre-foundation` D45), where Mica is still unavailable; the fallback is a solid `ThemeResource` background, never an unstyled default (`constitution.md` §2 Rule 9).
- `ExtendsContentIntoTitleBar = true` with `SetTitleBar(grid)`: the caption area is drawn by the system but the client region extends under it.

### 3.3 Single instance

- Mutex name is `Global\MTM_WIP_WinUI3_SingleInstance` (fixed by `001`, distinct from the WinForms mutex so both applications run during the transition).
- The guard runs before the window activates. A non-new instance executes the bring-to-front sequence (§2.1) and exits without starting the pipeline.
- There is no IPC and no argument forwarding. If the loader ever needs to pass data to a running instance, that is a new feature and a new decision, not part of this spec.

### 3.4 Sign-in constraints

- PIN verification is **hashed with a transparent upgrade (D17)**: a stored value carrying the marker prefix is compared as a hash; a value without it is legacy plaintext, verified as today and rewritten as a hash on that successful sign-in. Three attempts, the exact failure messages and abort-on-third are unchanged. Constraint: `Pin` is `varchar(50)`, so a per-user salt does not fit — use a per-install pepper with a truncated HMAC-SHA256. The plaintext value is never logged and never re-displayed.
- Three attempts, then abort and exit. The exact messages are preserved.
- Sign-in runs only for the shared accounts `"SHOP2"` and `"MTMDC"`; all other identified users bypass it.
- The user identity set by sign-in (or by the identity probe) is the single source for `ICurrentUserService`; nothing may write a second, divergent copy.

### 3.5 First-run user creation and role gate

- The role gate loads the user's roles into `ICurrentUserService`. If the user has no role flag set, the first-run creation stage runs; cancelling it aborts launch (fatal, orderly exit).
- Quick user creation creates the user with role id 3 (Normal) through `IDao_User`, enforces the no-spaces filter, and rejects duplicates via `UserExistsAsync`.
- After the gate, `ICurrentUserService` is read-only. Role checks happen when an action runs, not only when a view is built (`003` §2).

### 3.6 Global exception handling

- Both hooks log **before** presenting, so a faulting UI thread still leaves a record (`003` §3.1).
- The branch table (§2.5) is preserved; every branch presents through `IDialogService` (`DevWinUI.WindowedContentDialog`, `Owner` set, awaited directly) — never a raw dialog at a call site.
- `Fatal` terminates through the orderly-exit path, not `Environment.Exit`.
- No dialog ever shows a connection string or password (`constitution.md` §2 Rule 5, `002` §3).

### 3.7 Orderly exit and relaunch

- The normal exit path releases the mutex, disposes the `IHost`, runs cleanup hooks, and returns from `Main` — no `Environment.Exit`.
- There is no `Application.Restart` in WinUI 3. The settings-triggered restart becomes: release the mutex, start a new process of the same executable, then exit the current process. The relaunch must not recurse into the single-instance guard's own process.
- Cleanup hooks map from `AppDomain.ProcessExit` + `Application.ApplicationExit` to `AppDomain.ProcessExit` + `MainWindow.Closed`.

### 3.8 Deliberately not carried over

| Legacy behaviour | Decision | Why |
| :--- | :--- | :--- |
| `Application.DoEvents()` in `SplashScreenForm.UpdateProgress` | Not reproduced | Forbidden by `constitution.md` §1.3; progress is bound, not pumped. |
| Synchronous blocking `IdentifyUser` before any UI | Replaced by an async probe stage | `constitution.md` §2 Rule 7: no blocking I/O on the UI thread. |
| Startup wipe of `%APPDATA%` and `%LOCALAPPDATA%` application folders | Not carried over | `003` §3.6: a folder that is wiped cannot hold preferences. |
| `Environment.Exit(0)` in `Program.Main`'s `finally` | Replaced by orderly exit | `constitution.md` §2 Rule 5/6: fatal errors and normal exit both route through one path. |
| `ConfigureFormInstances()` static `MainFormInstance` | Deleted | `constitution.md` §2 Rule 11: no static mutable state. |
| `GetUserThemeColorsAsync` per-user palette application | Ignored (not read for colours) | `003` §3.2: per-control colours have no WinUI equivalent. |

### 3.9 State replaced by this spec

| Legacy static | Replacement | Lifetime |
| :--- | :--- | :--- |
| `Program.ServiceProvider` | `App.GetService<T>()` on the `IHost` | host lifetime |
| `Model_Application_Variables.User` + role flags | `ICurrentUserService` | singleton, read-only after the role gate |
| `Model_Application_Variables` combo caches / `ColorCodeParts` | `IMasterDataCache` | singleton |
| `Model_Application_Variables` preference flags | `IUserPreferences` | singleton |
| `Model_Application_Variables` theme state | `IThemeService` | singleton |
| `MainFormInstance` | constructor injection into the 8 controls | — |

The launch pipeline is the writer; every consumer reads the injected service. The pipeline must not recreate a static.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **PIN storage** | **Resolved (D17)** — transparent upgrade to hashed storage. This spec's sign-in stage verifies either form and upgrades on success. See §3. |
| 2 | **Where the version-check stage lives.** `Service_Timer_VersionChecker` has no natural module home in the new solution; it is a launch concern today. | (to confirm) — owner module for the ported version checker. |
| 3 | **The `StatusStrip` equivalent** | **Resolved (D25)** — one docked footer bar across the shell carrying the last-saved indicator, transient success/failure text and progress. It replaces the `StatusStrip` in kind rather than splitting the three signals across `InfoBar`, in-page progress and a badge. Pages publish to it through a shell status contract. |
| 4 | **Which eight controls read `MainFormInstance`.** They must be enumerated before `005` so their replacement dependencies are known. | (to confirm) — list produced during TASK-064. |
| 5 | **The cleanup action list.** `PerformAppCleanup`'s exact actions (temp files, stray locks, log rotation) must be read from source and mapped, not assumed. | (to confirm) — enumerated during TASK-056. |
| 6 | **Whether any machine in the fleet is below Windows 10 22H2.** The floor was raised from 1809 by `000-pre-foundation` D45 with no inventory taken; a machine below 22H2 is now excluded, and Mica remains unavailable on every supported Windows 10 build regardless. | Carried from `001` §4 #2. |
| 7 | **Relaunch-then-exit and the loader.** Whether the loader tolerates a settings-triggered self-relaunch (new process of the same exe) while it holds the app open. | (to confirm) — behaviour must be exercised with the loader before `011`. |

### Assumptions

1. The shell is the only consumer of the route table; operation screens in `005`/`006` register their pages through `AddInventoryServices` / `AddTransactionsServices`, not by editing the shell.
2. `ICurrentUserService`, `IMasterDataCache`, `IUserPreferences`, `IThemeService`, `IDialogService`, `IErrorService`, `IDispatcherService` exist exactly as `003` specifies and are registered by `003`'s module.
3. `IDao_System`, `IDao_User`, `IParameterPrefixCache`, `DatabaseOptions` and `Model_Result<T>` exist exactly as `002` specifies.
4. The shortcut/accelerator service from `003` can express app-wide accelerators (the `ProcessCmdKey` replacement) without a WinForms message loop.
5. No screen opens a window of its own; dialogs are always `WindowedContentDialog` through `IDialogService`.
