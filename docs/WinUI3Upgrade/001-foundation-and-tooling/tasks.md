# Migration Task List (Execution Plan)

**Spec**: `001-foundation-and-tooling`
**Range**: TASK-001 … TASK-013
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **No task in this spec may be skipped or deferred.** Tasks 007, 008 and 009 are the gates every later spec is checked against; a later spec cannot be reviewed without them.
>
> **Performance constraint from `000-pre-foundation` D57:** cold start is budgeted at ≤ 5 s to interactive from a network-share launch. That budget is why no synchronous database call may occur during launch (`002`, `004`), and it is the test any build setting added here must not undermine.

---

## Phase 1 — Solution skeleton

- [ ] **TASK-001** — Scaffold via `dotnet new` per [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D35: `dotnet new install Microsoft.WindowsAppSDK.WinUI.CSharp.Templates`, then `dotnet new winui` to produce the app project `MTM_WIP/MTM_WIP.csproj`, and add it to a new `MTM_WIP.sln`. Convert it to unpackaged: `<WindowsPackageType>None</WindowsPackageType>`, delete `Package.appxmanifest`, keep `EnableMsixTooling=true`. Set `AssemblyName` to `MTM_Inventory_Application`, `Platforms`/`PlatformTarget` to `x64`, `RuntimeIdentifier` to `win-x64`, `WindowsAppSDKSelfContained` to `true`.
  - **Verify**: `dotnet build MTM_WIP/MTM_WIP.csproj` succeeds and `bin/x64/Debug/net10.0-windows10.0.19041.0/win-x64/MTM_Inventory_Application.exe` exists. No `Package.appxmanifest` remains in the tree.

- [ ] **TASK-002** — Add `Directory.Build.props` at the solution root with `Version`, `UseRidGraph`, `Nullable`, `ImplicitUsings`, `TreatWarningsAsErrors`, `EnforceCodeStyleInBuild`, `WindowsAppSDKDeploymentManagerInitialize=false` per [`plan.md`](./plan.md) §1.2.
  - **Verify**: insert `int unused = 1;` into `App.xaml.cs`; the build fails with `CS0219` treated as an error. Remove it and the build passes.

- [ ] **TASK-002a** — Add the build-reproducibility files per [`plan.md`](./plan.md) §1.1a and [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D49/D50: `global.json` pinning SDK `10.0.100` with `rollForward: latestFeature`; `Directory.Packages.props` holding every package version; `RestorePackagesWithLockFile=true` in `Directory.Build.props`; commit every `packages.lock.json`.
  - **Verify**: no `.csproj` contains a `Version=` attribute on a `PackageReference`. `dotnet restore --locked-mode` succeeds; hand-editing a version in `Directory.Packages.props` then restoring with `--locked-mode` fails. `dotnet --version` reports an SDK inside the pinned band.

- [ ] **TASK-002b** — Add the configuration files per [`plan.md`](./plan.md) §3.1 and D51: committed `MTM_WIP/appsettings.json` with `ConnectionStrings:MySql`, `ConnectionStrings:InforVisual` (both redacted) and the `Serilog` block; `appsettings.json` as `None` + `CopyToOutputDirectory=PreserveNewest`; `appsettings.local.json` added to `.gitignore`; the override path `%PROGRAMDATA%\MTM_WIP\appsettings.local.json` recorded as the only override location.
  - **Verify**: `bin/x64/Debug/.../appsettings.json` exists after a build and contains no real credential. Creating `appsettings.local.json` in the repository root leaves `git status` clean. Creating one in `%PROGRAMDATA%\MTM_WIP\` is not tracked and not copied into the output.

- [ ] **TASK-003** — Determine the exact `Microsoft.WindowsAppSDK` version `MTM_Waitlist` builds with (read its `.csproj` and `obj/project.assets.json`, do not guess), and pin that identical version here. Record the value in [`../00-index.md`](../00-index.md) and [`../../WinUI3Upgrade/001-foundation-and-tooling/plan.md`](./plan.md) if it differs from 2.4.0.
  - **Verify**: `dotnet list package --include-transitive` shows the pinned version; `DevWinUI`'s target framework `net10.0-windows10.0.19041` matches this project's TFM.

- [ ] **TASK-004** — Add UI packages per [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D36/D37: `WinUIEx`, `CommunityToolkit.Mvvm` 8.4.2, `DevWinUI` **10.9.1**, the Windows Community Toolkit **8.2** per-control packages (`CommunityToolkit.WinUI.Controls.Primitives`, `.SettingsControls`, `.Sizers`, and any other control actually used), and **`WinUI.TableView`** for the grid. Do **not** reference `CommunityToolkit.WinUI.UI.Controls` — its 7.1.2 `DataGrid` is archived. Add `DevWinUI`, the Toolkit packages and `WinUI.TableView` to `MTM_WIP` and `MTM_WIP.Shared` only.
  - **Verify**: a throwaway `UserControl` in `MTM_WIP.Shared` declares `xmlns:dev="using:DevWinUI"` and a `<dev:AutoScrollView/>`, plus a `xmlns:tv="using:WinUI.TableView"` `<tv:TableView/>`, and the build succeeds. `dotnet list package` shows no `CommunityToolkit.WinUI.UI.Controls` anywhere.

- [ ] **TASK-005** — Create the eight library projects `MTM_WIP.Core`, `MTM_WIP.Shared`, `MTM_WIP.Data`, `MTM_WIP.Startup`, `MTM_WIP.Module_{Shell,Inventory,Transactions,Settings,Visual,DeveloperTools}` with the exact `ProjectReference` graph in [`plan.md`](./plan.md) §1.2 and §1.3. Create the empty folders `Models/`, `Contracts/` in `Core` and `Contracts/` in `Data`.
  - **Verify**: `dotnet build MTM_WIP.sln` succeeds; inspecting every `.csproj` matches §1.3 with no extra or missing reference.

- [ ] **TASK-006** — Create `MTM_WIP.Tests` as xUnit with `UseWinUI=false`, `RuntimeIdentifier=win-x64`, referencing `Core`, `Data` and all six modules. Add `Moq`, `FluentAssertions`, `Microsoft.NET.Test.Sdk`, `xunit.runner.visualstudio`.
  - **Verify**: `dotnet test MTM_WIP.Tests` runs, reports 0 tests, exits 0. `dotnet test` works on a machine without the Windows App SDK runtime installed.

---

## Phase 2 — Gates

- [ ] **TASK-007** — Implement `Architecture/ProjectReferenceMatrixTests.cs`: read each project assembly's referenced assemblies and assert the graph in [`plan.md`](./plan.md) §1.3.
  - **Verify**: add `<ProjectReference Include="..\MTM_WIP.Module_Inventory\MTM_WIP.Module_Inventory.csproj" />` to `MTM_WIP.Core.csproj`; the test fails naming both projects. Remove it; the test passes.

- [ ] **TASK-008** — Implement `Architecture/ForbiddenApiTests.cs`: walk `*.cs` and `*.xaml` outside `bin`, `obj`, `docs`, `specs`, `.specify`, and assert zero matches for `System.Windows.Forms`, `MessageBox`, `MySql.Data`, `BackgroundWorker`, `Colors.`, and literal `#RRGGBB` inside XAML.
  - **Verify**: add a temporary class containing `System.Windows.Forms.MessageBox.Show("x");`; the test fails listing the file and line. Remove it; the test passes.

- [ ] **TASK-009** — Add `.editorconfig` carried over from the WinForms repository (file-scoped namespaces, 4 spaces, LF, `dotnet_diagnostic` severities), plus the WinForms application's existing ignore patterns in `.gitignore` extended with `logs/` and the publish staging folder.
  - **Verify**: `dotnet format MTM_WIP.sln --verify-no-changes` exits 0. `git status --short` shows no build output after a clean build.

---

## Phase 3 — Assets and manifest

- [ ] **TASK-010** — Add `MTM_WIP/app.manifest` with per-monitor v2 DPI awareness and a `<supportedOS>` list matching `TargetPlatformMinVersion` 10.0.19041. Reference it via `<ApplicationManifest>`.
  - **Verify**: run the app at 150 % display scaling; the window and its text are not bitmap-stretched.

- [ ] **TASK-011** — Copy the six asset groups listed in [`plan.md`](./plan.md) §2.3 into `MTM_WIP/Assets/`, preserving relative paths. Mark each `Content` with `CopyToOutputDirectory=PreserveNewest`. Set `<ApplicationIcon>Assets\mtm.ico</ApplicationIcon>`.
  - **Verify**: `docs` content is *not* copied, and every listed asset appears under `bin/x64/Debug/.../Assets/` with the expected relative path. `Get-ChildItem Assets/Help/Content` returns 15 `.json` files.

- [ ] **TASK-012** — Confirm the help content is intact by counting categories and topics across the copied files rather than eyeballing one file.
  - **Verify**: a small script or test reads every JSON in `Assets/Help/Content` and asserts 15 files, 15 distinct `CategoryId` values and 54 topics in total.

---

## Phase 4 — Downstream consistency validation

- [ ] **TASK-013** — **Downstream validation: `002-data-access-layer` (and a sweep of `003`–`011`).** Before declaring this spec done, re-read [`../002-data-access-layer/spec.md`](../002-data-access-layer/spec.md), [`../002-data-access-layer/plan.md`](../002-data-access-layer/plan.md) and [`../002-data-access-layer/tasks.md`](../002-data-access-layer/tasks.md), and confirm every assumption this spec made about what comes next still holds. Then sweep the remaining specs for anything that depends on this spec's output.

  Record the result in the **Drift log** below. Specifically confirm:
  1. `Model_Result<T>` is placed in `MTM_WIP.Core/Models` and `MTM_WIP.Data` can return it without referencing `Shared` — spec `002` §3.1 and TASK-014 assume this.
  2. `MTM_WIP.Data` has a `Contracts/` folder and no reference to `Shared` — spec `002` §1 and TASK-033 assume this.
  3. The pinned `Microsoft.WindowsAppSDK` version is recorded, and `DevWinUI` still compiles against it — specs `003` and `004` both build on the dialog wrapper.
  4. `MTM_WIP.Tests` runs without the Windows App SDK — every spec from `002` onward adds tests and relies on this.
  5. No asset path in `Assets/` differs from the path a later spec expects to read.

  If any item is false, fix **this** spec (or amend the downstream spec) before proceeding. Do not carry a known mismatch forward.

  - **Verify**: a written entry exists in the Drift log below for each of the five items, each marked *confirmed* or *amended*, and `dotnet test MTM_WIP.Tests` is green.

---

## Drift log

Filled in by TASK-013. One row per downstream spec checked.

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `002-data-access-layer` | `Model_Result<T>` location and reference direction | *(pending)* | — |
| `002-data-access-layer` | `Data/Contracts` present, no `Shared` reference | *(pending)* | — |
| `002-data-access-layer` | Pinned SDK version recorded | *(pending)* | — |
| `003`–`009` | Test project runs without Windows App SDK | *(pending)* | — |
| `003`–`011` | Asset paths match consumers | *(pending)* | — |
