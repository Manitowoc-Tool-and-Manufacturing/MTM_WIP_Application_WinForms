# Migration Task List (Execution Plan)

**Spec**: `004-shell-and-startup`
**Range**: TASK-053 … TASK-065
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** The composition root (TASK-053) and the single window (TASK-054) come first because every later task registers or renders into them. The pipeline stages (TASK-057 … TASK-062) are built against the stage contract, and the shell (TASK-063, TASK-064) is last before the downstream sweep because it is the pipeline's final handoff target.

---

## Phase 1 — Composition root, window, lifetime

- [ ] **TASK-053** — Implement the composition root in `App.xaml.cs`: build the `IHost` with `Host.CreateDefaultBuilder().UseContentRoot(AppContext.BaseDirectory).UseSerilog(...).ConfigureServices(ctx => ctx.AddAppServices(ctx))`; add `App.GetService<T>()` that throws with the type name when a service is unregistered; declare the ten per-module `Add*Services` partial methods and wire them into `AddAppServices` in the order Core, Shared, Data, Startup, Shell, Inventory, Transactions, Settings, Visual, DeveloperTools. Stub the modules not yet implemented (`Inventory`, `Transactions`, `Settings`, `Visual`, `DeveloperTools`) as empty partials in this spec's scope, with a note that `005`–`010` own their bodies.
  - **Verify**: a unit test builds the host from `appsettings.json`, resolves `MainWindow`, `ILaunchPipeline`, `INavigationService`, and every `IDao_*`/`IErrorService`/`IDialogService` registered by `001`–`003`; resolving a deliberately unregistered type throws with the type name in the message. `dotnet build` is clean.

- [ ] **TASK-054** — Implement `MainWindow` as a WinUIEx `WindowEx` with the `StageHost` `ContentPresenter`, `ExtendsContentIntoTitleBar = true`, `SetTitleBar(grid)`, `SystemBackdrop = new MicaBackdrop()` guarded by `MicaController.IsSupported()`, and a solid `ThemeResource` background fallback on Windows 10.
  - **Verify**: a grep over the solution finds exactly one `Window`-derived class; XAML inspection shows `StageHost` and `SetTitleBar`; running on Windows 10 shows the solid fallback and running on Windows 11 shows Mica.

- [ ] **TASK-055** — Implement `Service_SingleInstance`: acquire `Global\MTM_WIP_WinUI3_SingleInstance`; on a non-new instance, run the bring-to-front sequence (`ShowWindow(SW_RESTORE=9)` if `IsIconic`, then `SetForegroundWindow`) against the first other PID with `MainWindowHandle != 0`, with no IPC and no argument forwarding, then return `false` so the app exits without building further UI.
  - **Verify**: launching a second copy while the first is running brings the first window forward and the second process exits with code 0; the mutex name matches `Global\MTM_WIP_WinUI3_SingleInstance`.

- [ ] **TASK-056** — Implement `Service_AppExit` with `Exit(bool fatal)` (release mutex, run cleanup hooks, dispose the host, close the window — no `Environment.Exit`) and `Relaunch()` (release mutex, `Process.Start` the current executable, then exit) as the `Application.Restart` replacement. Read the exact `PerformAppCleanup` action list from the WinForms source and port it; record the list in the drift log.
  - **Verify**: a unit test asserts `Relaunch` releases the mutex before starting the child process; a manual test triggers relaunch from the settings flow (or a stub) and the new process acquires the mutex cleanly.

---

## Phase 2 — Launch pipeline

- [ ] **TASK-057** — Implement the stage contract (`ILaunchStage` with `Name`, `IsCritical`, `RunAsync`) and `Service_LaunchPipeline` running stages in registration order, stopping on the first critical failure (surfaced as `Fatal` via `IErrorService` then orderly exit) and logging-and-continuing past non-critical failures. Register the ten stages from `spec.md` §3.1 in the exact listed order.
  - **Verify**: a test with fake stages asserts the runner stops on the first critical failure, continues past a non-critical failure, and that execution order equals registration order; `dotnet test` green.

- [ ] **TASK-058** — Implement the splash stage: `View_Splash` + `ViewModel_Splash` bound to `IProgress<Model_LaunchProgress>`, with the initial short delay expressed as an awaited `Task.Delay`, never `Thread.Sleep`. Confirm no `Application.DoEvents` exists anywhere in the new solution.
  - **Verify**: a grep for `DoEvents` returns zero hits; the splash renders progress values 0→100 from a fake pipeline without blocking the UI thread.

- [ ] **TASK-059** — Implement the sign-in stage (`View_SignIn` + `ViewModel_SignIn`): username + PIN, three attempts, exact messages `"Invalid username or PIN.\nAttempts remaining: N"` and `"Maximum login attempts exceeded. Application will close."`, refocus on failure, abort on the third, and a gate that runs only when the identified user is `"SHOP2"` or `"MTMDC"`. Implement PIN verification per **D17**: a stored value with the hash marker is verified as a hash; a value without the marker is verified as legacy plaintext and rewritten as a hash on success. Use a per-install pepper with a truncated HMAC-SHA256 so the result fits the `varchar(50)` column. Run the identity probe (`Environment.UserName` → `"SHOP2"` fallback) asynchronously in the precondition.
  - **Verify**: a ViewModel test (no UI thread) asserts the three-attempt countdown, the abort path on attempt 3, and that a non-shared user skips the stage entirely. A second test signs in with a legacy plaintext PIN, asserts success, and asserts the stored value is rewritten with the marker and then still verifies. A third asserts a wrong PIN never rewrites the stored value and never appears in a log line. Assert the hashed form is at most 50 characters.

- [ ] **TASK-060** — Implement the first-run user-creation stage (`View_QuickUserCreation` + `ViewModel_QuickUserCreation`): first/last name, shift combo, username, PIN, optional Visual user/pass; create via `IDao_User.CreateUserAsync` with role id 3; no-spaces validation; duplicate check `UserExistsAsync`.
  - **Verify**: a ViewModel test asserts role id 3 is passed, a username containing a space is rejected, and an existing username is rejected with the duplicate message; an integration test creates the user inside a rolled-back transaction and reads it back.

- [ ] **TASK-061** — Implement the role gate stage: load the user's roles into `ICurrentUserService`; when no role flag is set, trigger the first-run creation stage and abort launch on cancel. After the gate, `ICurrentUserService` is read-only.
  - **Verify**: a pipeline test with a fake `ICurrentUserService` asserts the creation stage runs when roles are empty and not when roles exist; a second test asserts cancel aborts launch and routes through `Service_AppExit`.

- [ ] **TASK-062** — Implement `Service_GlobalExceptionHandler` wired to `App.UnhandledException` + `AppDomain.CurrentDomain.UnhandledException`, logging first via `IErrorService`, then reproducing the branch table (`MySqlException` → Fatal + retry restart; `InvalidOperationException` containing `"database"` → DB config dialog; `FileNotFound`/`DirectoryNotFound` → file dialog; `UnauthorizedAccess`/`SecurityException` → security dialog; `COMException`/`ExternalException` → system dialog; `TimeoutException` → timeout dialog; else → fatal + exit) all through `IDialogService` with `Owner` set.
  - **Verify**: a table-driven unit test asserts each exception type routes to the expected dialog intent and that `Fatal`/else both end in `Service_AppExit.Exit(fatal: true)`; a grep finds no `Environment.Exit` in the new solution.

---

## Phase 3 — Shell and navigation

- [ ] **TASK-063** — Implement `View_Shell` (NavigationView + `Frame`) and `ViewModel_Shell`, data-driven from `Model_NavigationItem` records; map the `MenuStrip` (File / View / Tools / Help) to NavigationView items, leaving the per-module entries to `005`–`009`. Stub a status surface for the old `StatusStrip` (progress + status text + saved indicator) with the `(to confirm)` shape recorded in the drift log.
  - **Verify**: the shell renders in both `Light` and `Dark` themes; selecting a NavigationView item navigates the `Frame`; the old `MenuStrip` top-level groups are each represented.

- [ ] **TASK-064** — Implement `INavigationService` / `Service_Navigation` with the immutable route table keyed by **ViewModel type** → **Page type**, `NavigateTo<TViewModel>()`, `NavigateBack()`, `CanGoBack`, and a startup check that a duplicate or missing route throws. Implement `IShellStatusPublisher` and `Control_ShellFooter` per **D25**: one docked footer bar carrying the last-saved indicator, transient success/failure text and progress, fed by whichever page is showing. Implement `Service_Accelerator` mapping app-wide accelerators (the `ProcessCmdKey` replacement) to `RelayCommand`s via the `003` shortcut service. Enumerate the eight controls that read `MainFormInstance` and record their replacement dependencies in the drift log.
  - **Verify**: a route-table test asserts every registered ViewModel type maps to a Page type and that a duplicate registration throws at startup; an accelerator test asserts a registered key gesture executes its command and an unregistered one does not; a footer test asserts a page publishing progress, a saved value and a failure message renders all three in the one bar, and that navigating away clears the transient text but keeps the saved indicator.
  - **Verify**: a route-table test asserts every registered ViewModel type maps to a Page type and that a duplicate registration throws at startup; an accelerator test asserts a registered key gesture executes its command and an unregistered one does not.

---

## Phase 4 — Downstream consistency validation

- [ ] **TASK-065** — **Downstream validation: `005-inventory-operations` (and a sweep of `006`–`011`).**

  Re-read [`../005-inventory-operations/spec.md`](../005-inventory-operations/spec.md), [`plan.md`](../005-inventory-operations/plan.md) and [`tasks.md`](../005-inventory-operations/tasks.md), then sweep `006`–`011` for the same shape.

  Confirm and record these concrete assumptions; where one is false, fix **this** spec before proceeding — do not amend `005` to work around a defect here:

  1. **One window only.** `005` assumes a single `MainWindow` with `StageHost` is the only host, and that New / Remove / Transfer / Quick Buttons are `Page`s in the shell `Frame`, never extra windows.
  2. **Navigation by ViewModel type.** `005`'s screens are reachable through `INavigationService.NavigateTo<TViewModel>()` and register their routes without editing `Service_Navigation`.
  3. **Services injected, not static.** `005` calls `ICurrentUserService` (role checks, e.g. admin-only delete), `IMasterDataCache`, `IUserPreferences`, `IDao_Inventory`, `IDao_QuickButtons` — all as injected interfaces, never `Model_Application_Variables` or a static DAO.
  4. **Dialogs through `IDialogService`.** `005` presents every confirmation/warning through `IDialogService` (`WindowedContentDialog`, `Owner` set, awaited), never a raw dialog.
  5. **Role gate has run.** `005` assumes `ICurrentUserService` is populated before any screen loads — i.e. the launch pipeline in this spec has completed stage 6.
  6. **Accelerators via the shell.** `005` registers hotkeys through `Service_Accelerator`, not a `ProcessCmdKey` override.
  7. **No blocking data calls.** `005` assumes DAOs return `Model_Result<T>` asynchronously and never exposes a synchronous twin.

  - **Verify**: a written Drift-log entry exists for each of the seven items, each marked *confirmed* or *amended*, and the full solution test suite is green.

---

## Drift log

Filled in by TASK-065 (and the `(to confirm)` items from TASK-056, TASK-059, TASK-063, TASK-064).

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `005` | One window only; screens are `Page`s in the shell `Frame` | *(pending)* | — |
| `005` | Screens reachable via `INavigationService` by ViewModel type | *(pending)* | — |
| `005` | Injected services only; no `Model_Application_Variables` / static DAO | *(pending)* | — |
| `005` | Dialogs through `IDialogService` only | *(pending)* | — |
| `005` | `ICurrentUserService` populated before any screen loads | *(pending)* | — |
| `005` | Accelerators via `Service_Accelerator`, not `ProcessCmdKey` | *(pending)* | — |
| `005` | Async `Model_Result<T>` DAOs only; no synchronous twin | *(pending)* | — |
| `006`–`011` | Same seven-item shape holds across the remaining specs | *(pending)* | — |
| `003`/`005` | `StatusStrip` replacement surface shape | *(pending)* | — |
| `005` | Eight `MainFormInstance` consumers enumerated and re-dependency'd | *(pending)* | — |
| `—` | `PerformAppCleanup` action list ported verbatim | *(pending)* | — |
| `—` | PIN verify-and-upgrade path matches D17 | *(pending)* | — |

## Known deviations from the WinForms behaviour

Recorded so a reviewer does not mistake them for defects.

| Deviation | Rationale | Reviewed by |
| :--- | :--- | :--- |
| Synchronous blocking `IdentifyUser` is replaced by an async probe | Blocking I/O on the UI thread is forbidden (`constitution.md` §2 Rule 7) | *(this spec)* |
| `Application.DoEvents()` splash pumping is dropped | Forbidden; progress is bound through `IProgress<T>` | *(this spec)* |
| Startup wipe of `%APPDATA%` / `%LOCALAPPDATA%` app folders is dropped | A wiped folder cannot hold preferences (`003` §3.6) | *(this spec)* |
| `Environment.Exit(0)` is replaced by `Service_AppExit` orderly exit | One exit path for fatal and normal shutdown (Rule 5/6) | *(this spec)* |
| `Application.Restart()` is replaced by relaunch-then-exit | No WinUI 3 `Application.Restart` exists | *(this spec)* |
| Static `MainFormInstance` is deleted | No static mutable state (Rule 11) | *(this spec)* |
| Per-user theme colours (`GetUserThemeColorsAsync`) are ignored | No WinUI equivalent for per-control colours (`003` §3.2) | *(this spec)* |
