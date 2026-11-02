# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `004-shell-and-startup`
**Tasks**: TASK-053 … TASK-065

---

## 1. Project Topology & Directory Mapping

This spec lands in **`MTM_WIP`** (the exe), **`MTM_WIP.Startup`**, and **`MTM_WIP.Module_Shell`**. It consumes services registered by `001`–`003`; it does not add projects.

```text
MTM_WIP/                              # the exe — composition root
├── App.xaml                          # ApplicationDefinition; hooks App.UnhandledException
├── App.xaml.cs                       # IHost build; AddAppServices(context); GetService<T>()
├── MainWindow.xaml                   # WindowEx, StageHost ContentPresenter
├── MainWindow.xaml.cs                # title bar wiring; lifecycle only
└── appsettings.json                  # Serilog + DatabaseOptions binding (consumes 002)

MTM_WIP.Startup/                      # launch stages
├── Contracts/
│   ├── ILaunchPipeline.cs
│   ├── ILaunchStage.cs               # Name, IsCritical, RunAsync(context, progress, ct)
│   └── ILaunchContext.cs             # scoped state shared across stages
├── Services/
│   ├── Service_LaunchPipeline.cs     # ordered stage runner; critical vs non-critical
│   ├── Service_SingleInstance.cs     # mutex + bring-to-front P/Invoke
│   ├── Service_AppExit.cs            # orderly exit + relaunch-then-exit
│   ├── Service_GlobalExceptionHandler.cs
│   └── Stages/
│       ├── Stage_Connectivity.cs
│       ├── Stage_ParameterPrefixCache.cs
│       ├── Stage_SignIn.cs
│       ├── Stage_QuickUserCreation.cs
│       ├── Stage_RoleGate.cs
│       ├── Stage_UserSettings.cs
│       ├── Stage_MasterDataCache.cs
│       ├── Stage_VersionCheck.cs
│       ├── Stage_Theme.cs
│       └── Stage_ShellHandoff.cs
├── Views/
│   ├── View_Splash.xaml               # progress bound to IProgress<LaunchProgress>
│   ├── View_SignIn.xaml
│   └── View_QuickUserCreation.xaml
├── ViewModels/
│   ├── ViewModel_Splash.cs
│   ├── ViewModel_SignIn.cs
│   └── ViewModel_QuickUserCreation.cs
├── Models/
│   └── Model_LaunchProgress.cs
└── DependencyInjection/
    └── StartupServiceRegistration.cs  # AddStartupServices(...)

MTM_WIP.Module_Shell/                 # navigation shell
├── Contracts/
│   └── INavigationService.cs         # NavigateTo<TViewModel>(); route table
├── Services/
│   ├── Service_Navigation.cs         # ViewModel type -> Page type map
│   └── Service_Accelerator.cs        # app-wide accelerator -> RelayCommand mapping
├── Views/
│   ├── View_Shell.xaml               # NavigationView + Frame + shell footer bar (D25)
│   └── Control_ShellFooter.xaml      # last-saved indicator, transient status text, progress
├── ViewModels/
│   ├── ViewModel_Shell.cs
│   └── ViewModel_ShellFooter.cs      # subscribes to the shell status contract
├── Contracts/
│   └── IShellStatusPublisher.cs      # pages publish status/progress/saved to the footer
├── Models/
│   └── Model_NavigationItem.cs
└── DependencyInjection/
    └── ShellServiceRegistration.cs   # AddShellServices(...)
```

- `MTM_WIP` references every project (`001` inventory). `MTM_WIP.Startup` references `Core` and `Shared`; it reaches data through contracts in `Core`/`Shared` and the DAOs are injected, so it does **not** reference `MTM_WIP.Data` directly unless a stage needs a concrete DAO — it does not: every DAO dependency is an `IDao_*` registered by `002`. `MTM_WIP.Module_Shell` references `Core` and `Shared` (`constitution.md` §2 Rule 10).
- `MainWindow` stays in the exe because `001` fixes it there ("Composition root, `App.xaml`, `MainWindow`"). The shell *content* (`View_Shell`) is in `Module_Shell`; `MainWindow` only owns the window chrome and the `StageHost`.

---

## 2. Component Migration Strategy

### 2.1 Composition root (TASK-053)

`App.xaml.cs` builds the host exactly as the sibling applications do, then activates the window from the host's services.

```csharp
public partial class App : Application
{
    public static IHost Host { get; private set; } = null!;

    public App()
    {
        InitializeComponent();
        Host = Host.CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .UseSerilog((context, services, config) =>
                config.ReadFrom.Configuration(context.Configuration)
                      .WriteTo.File("logs/app-.txt",
                          rollingInterval: RollingInterval.Day,
                          retainedFileCountLimit: 30,
                          shared: true))
            .ConfigureServices((context, services) =>
                services.AddAppServices(context))
            .Build();
    }

    public static T GetService<T>() where T : class
        => Host.Services.GetService(typeof(T)) as T
            ?? throw new InvalidOperationException($"Service {typeof(T).Name} is not registered.");

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var mainWindow = GetService<MainWindow>();
        mainWindow.Activate();
    }
}
```

`AddAppServices(this IServiceCollection services, HostBuilderContext context)` is the single registration entry point, split into per-module `partial` methods — one per project. `004` owns `AddStartupServices` and `AddShellServices`; the others are declared here and implemented in their modules (`002` owns `AddDataServices`, `003` owns `AddCoreServices`/`AddSharedServices`; `005`–`010` own the remaining four). Unimplemented modules register nothing in v1 (they are stubs), but the partial methods exist so a module never edits the composition root to add itself.

```csharp
public static partial class AppServiceRegistration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, HostBuilderContext context)
    {
        services.AddCoreServices(context);
        services.AddSharedServices(context);
        services.AddDataServices(context);
        services.AddStartupServices(context);
        services.AddShellServices(context);
        services.AddInventoryServices(context);
        services.AddTransactionsServices(context);
        services.AddSettingsServices(context);
        services.AddVisualServices(context);
        services.AddDeveloperToolsServices(context);
        return services;
    }

    static partial void AddCoreServices(HostBuilderContext context);            // 003
    static partial void AddSharedServices(HostBuilderContext context);          // 003
    static partial void AddDataServices(HostBuilderContext context);            // 002
    static partial void AddStartupServices(HostBuilderContext context);         // 004
    static partial void AddShellServices(HostBuilderContext context);           // 004
    static partial void AddInventoryServices(HostBuilderContext context);       // 005
    static partial void AddTransactionsServices(HostBuilderContext context);    // 006
    static partial void AddSettingsServices(HostBuilderContext context);        // 007
    static partial void AddVisualServices(HostBuilderContext context);          // 008
    static partial void AddDeveloperToolsServices(HostBuilderContext context);  // 010
}
```

**Constraints**

- `GetService<T>()` throws when a type is unregistered — this is the mechanism `002` §2.8's DI test and every ViewModel factory rely on. It must never silently return `null`.
- `MainWindow` is resolved from the host; it is not `new`ed in `OnLaunched` directly, because its constructor takes injected services (the launch pipeline).
- Code-behind stays boilerplate-only (`constitution.md` §2 Rule 1): `OnLaunched` activates a window; it does not decide anything.

### 2.2 The single window (TASK-054)

`MainWindow.xaml`:

```xml
<winex:WindowEx x:Class="MTM_WIP.MainWindow"
                xmlns:... xmlns:winex="using:WinUIEx">
  <Grid x:Name="AppTitleBar">
    <ContentPresenter x:Name="StageHost" />
  </Grid>
</winex:WindowEx>
```

`MainWindow.xaml.cs` wires only window chrome:

- `SystemBackdrop = new MicaBackdrop()` guarded by `MicaController.IsSupported()`; on Windows 10 the fallback is a solid `ThemeResource` background set on the root `Grid` — never an unstyled default.
- `ExtendsContentIntoTitleBar = true;` `SetTitleBar(AppTitleBar);`
- `StageHost` is the single swap point for launch stages and, after handoff, the shell `Frame`.

No other window type exists in the solution. A grep gate asserts there is exactly one `Window`-derived class and every `Page` is a `Frame` child.

### 2.3 Launch pipeline shape (TASK-057)

`ILaunchStage`:

```csharp
public interface ILaunchStage
{
    string Name { get; }
    bool IsCritical { get; }
    Task RunAsync(ILaunchContext context, IProgress<Model_LaunchProgress> progress, CancellationToken ct);
}
```

`Service_LaunchPipeline.RunAsync` iterates the registered stages in registration order. A stage that throws: if `IsCritical`, the pipeline stops and surfaces the failure through `IErrorService` as `Fatal`, then orderly exit; if non-critical, the exception is logged and the next stage runs. Registration order **is** the stage order from `spec.md` §3.1; the runner does not sort.

Stages are registered in `AddStartupServices` in this order, matching `spec.md` §3.1:

1. `Stage_Connectivity` — **critical**. `IDao_System` probe.
2. `Stage_ParameterPrefixCache` — non-critical. Populates `IParameterPrefixCache`.
3. `Stage_SignIn` — **critical**, conditional on the identified user being `"SHOP2"` or `"MTMDC"`.
4. `Stage_QuickUserCreation` — **critical**, conditional on the role gate finding no role.
5. `Stage_RoleGate` — **critical**. Loads roles into `ICurrentUserService`; triggers stage 4 when empty.
6. `Stage_UserSettings` — non-critical. Populates `IUserPreferences` + initial theme state.
7. `Stage_MasterDataCache` — non-critical. `IMasterDataCache.RefreshAsync`.
8. `Stage_VersionCheck` — non-critical.
9. `Stage_Theme` — non-critical. `IThemeService` initialisation.
10. `Stage_ShellHandoff` — **critical**. Swaps `StageHost` to the shell.

The sign-in / creation stages use the conditional ordering via the `ILaunchContext` (identity set by stage 3, role flags set by stage 5); the runner executes all stages in order but the conditional ones return immediately when their gate is closed. The identity probe (`Environment.UserName` → `"SHOP2"` fallback) runs in stage 3's precondition, asynchronously through `IDao_System`.

### 2.4 Route table and navigation (TASK-064)

`Service_Navigation` holds an immutable route table keyed by **ViewModel type**:

```csharp
private static readonly Dictionary<Type, Type> Routes = new()
{
    [typeof(ViewModel_Shell)] = typeof(View_Shell),
    // 005/006 register additional entries through a shared registration hook,
    // never by editing this class.
};
```

`INavigationService.NavigateTo<TViewModel>()` resolves the `Page` type from the table, constructs it through `App.GetService<Page>` (or the registered page factory), and sets the shell `Frame`'s content. `NavigateBack()` and `CanGoBack` delegate to the `Frame`'s back stack. A route registered twice, or a missing route, throws at startup — a navigation bug is caught when the map is built, not on first use.

The shell menu (`NavigationView` items) is data-driven from `Model_NavigationItem` records; selecting an item calls `NavigateTo<TViewModel>()`. The `MenuStrip` (File / View / Tools / Help) maps to NavigationView top-level and footer items; the exact item list and which module owns each is refined with `005`–`009` (the shell owns the *frame*, the modules own the *entries*).

### 2.5 Single-instance guard (TASK-055)

`Service_SingleInstance.TryAcquire()` creates `new Mutex(true, "Global\\MTM_WIP_WinUI3_SingleInstance", out bool isNew)`. When `isNew` is false it runs the bring-to-front sequence (§2.1 of `spec.md`) and returns false; the app then returns from `Main` without building further UI. The P/Invoke surface (`ShowWindow`, `SetForegroundWindow`, `IsIconic`) is the same sequence as the WinForms code. No IPC, no argument forwarding.

### 2.6 Global exception handling (TASK-062)

`Service_GlobalExceptionHandler` is wired to `App.UnhandledException` (marking the event handled where a retry/restart is offered) and `AppDomain.CurrentDomain.UnhandledException`. Its `Handle(ex)` reproduces the branch table from `spec.md` §2.5, logging first via `IErrorService`, then presenting through `IDialogService` with `Owner` set. The `Fatal` branch and the "else" branch both end in `Service_AppExit.Exit(fatal: true)`.

### 2.7 Orderly exit and relaunch (TASK-056)

`Service_AppExit` owns two operations:

- `Exit(bool fatal)` — release the mutex, run cleanup hooks, dispose the host, close the window. No `Environment.Exit`.
- `Relaunch()` — the replacement for `SettingsForm`'s release-mutex + `Application.Restart()`: release the mutex, `Process.Start` the current executable, then `Exit(fatal: false)`. The new process is a fresh PID, so the single-instance guard does not collide with its own parent (the parent has already released the mutex before the child acquires it).

Cleanup hooks map from `AppDomain.ProcessExit` + `Application.ApplicationExit` to `AppDomain.ProcessExit` + `MainWindow.Closed`. The exact cleanup action list is read from `PerformAppCleanup` at implementation time and recorded in the drift log — it is ported, not invented.

### 2.8 Deliberately not carried over

| Not ported | Why |
| :--- | :--- |
| `Application.DoEvents()` in splash progress | Forbidden (`constitution.md` §1.3). Progress is bound through `IProgress<T>`. |
| Synchronous blocking `IdentifyUser` before any UI | `constitution.md` §2 Rule 7. Replaced by the async identity probe in the sign-in stage precondition. |
| Startup wipe of `%APPDATA%` / `%LOCALAPPDATA%` app folders | A wiped folder cannot hold preferences; `003` §3.6 already forbids it. |
| `Environment.Exit(0)` in `Main`'s `finally` | One orderly-exit path (`Service_AppExit`), consistent with Rule 5/6. |
| `Application.Restart()` | No WinUI 3 equivalent; `Service_AppExit.Relaunch()` (relaunch-then-exit). |
| `ConfigureFormInstances()` / static `MainFormInstance` | Rule 11; injection replaces the static. |
| `MessageBox.Show` in the second-instance path | Replaced by the bring-to-front sequence only — no dialog is shown to the second instance, matching today's *effect* (the first window comes forward). If today's `MessageBox` text must survive, it is a dialog through `IDialogService`; (to confirm). |

---

## 3. Data & State Migration

### 3.1 Who owns what state

| State | Writer | Reader | Lifetime |
| :--- | :--- | :--- | :--- |
| Current user + roles | `Stage_RoleGate` / `Stage_SignIn` | everything via `ICurrentUserService` | singleton, read-only after gate |
| Master-data caches | `Stage_MasterDataCache` | screens via `IMasterDataCache` | singleton |
| Preferences / theme mode | `Stage_UserSettings` / `Stage_Theme` | screens via `IUserPreferences` / `IThemeService` | singleton |
| Parameter-prefix cache | `Stage_ParameterPrefixCache` | `002` helper via `IParameterPrefixCache` | singleton |
| Launch progress | `Service_LaunchPipeline` → `IProgress<Model_LaunchProgress>` | `ViewModel_Splash` | per-launch |

No stage writes to `%APPDATA%` or `%LOCALAPPDATA%` application folders. Sign-in writes nothing; quick user creation writes only through `IDao_User.CreateUserAsync` (role id 3) — the same database write as today, and therefore the same `p_Status` contract.

### 3.2 Configuration

- Serilog is configured in `UseSerilog(...)` during host build, reading the same file-sink settings `003` specifies; no logging stage remains in the pipeline.
- `DatabaseOptions` (from `002`) is bound from `appsettings.json` in `AddDataServices` and is not re-declared here. `004` consumes it only through the registered `IDao_*` and `IDatabaseConnectionMonitor`.
- The mutex name and the relaunch executable path are compile-time constants, not configuration.

### 3.3 Persistence introduced

None beyond `003`. The only new durable effect of this spec is the user row created by the first-run stage, which is the same row the WinForms `Form_QuickUserCreation` creates today.

### 3.4 Test strategy

- **ViewModel tests** (no UI thread, per `constitution.md` §4): `ViewModel_SignIn` (three attempts, abort-on-third, skip-when-not-shared), `ViewModel_QuickUserCreation` (no-spaces filter, duplicate rejection, role id 3), `ViewModel_Shell` (menu selection navigates via a fake `INavigationService`).
- **Pipeline tests**: a fake stage set asserts the runner stops on the first critical failure and continues past a non-critical failure; a second test asserts stage order equals registration order.
- **Route table tests**: every registered `ViewModel` type maps to a `Page` type; a duplicate registration throws.
- **Single-instance / relaunch** are exercised manually (they require a real second process and a real window); the mutex acquisition logic is unit-tested with a real OS mutex but the bring-to-front P/Invoke is smoke-tested, not asserted.
- The mandatory gates from `constitution.md` §4 all apply, including the grep gate with zero hits for `System.Windows.Forms`, `MessageBox`, `Application.DoEvents` and literal colour hexes in `*.xaml`.
