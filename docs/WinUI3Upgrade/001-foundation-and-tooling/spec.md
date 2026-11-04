# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `001-foundation-and-tooling`
**Depends on**: nothing — this is the first spec
**Unblocks**: every other spec
**Tasks**: TASK-001 … TASK-013
**Governing rules**: [`../constitution.md`](../constitution.md) §1, §2 Rule 10, §4

---

## 1. Executive Summary & Scope

This spec delivers the solution skeleton, the build guardrails, and the assets the rest of the migration assumes exist. It builds no screens and calls no database. Its job is to make every later mistake impossible to commit.

The WinForms application is one project with folders and 300+ files. It has no compile-time boundary between Forms, DAOs and Database, which is why the constitution's "Forms → DAOs → DB" rule is currently enforced only by review. This spec replaces that with a reference graph the compiler and a test both reject.

### In scope

- A solution with one app project, four foundation libraries, six module libraries, and one test project.
- Fixed target framework, packaging mode, assembly name and version source.
- Build settings that turn warnings into errors and enable style enforcement.
- A pinned SDK (`global.json`), central package management, and committed lock files so the toolchain is reproducible (`000-pre-foundation` D49/D50).
- The configuration file pair — committed `appsettings.json` with redacted credentials, plus an untracked override outside the payload (`000-pre-foundation` D51).
- Two automated gates: the project reference matrix, and a forbidden-API scan.
- The asset payload (icon, logo, help content, release notes, shortcuts, Excel template).
- An application manifest and DPI awareness.

### Out of scope

- Any UI beyond the template's blank window.
- Any data access.
- Any theming beyond the default WinUI resources.
- Publishing — that is `011`.

### Success condition

`dotnet build` and `dotnet test` both pass on an application that opens an empty window, and adding a forbidden reference or a `MessageBox.Show` fails the build automatically.

---

## 2. Legacy Feature Assessment & UI Mapping Table

This spec ports no visual controls. The table below maps the *structural* artefacts that must survive.

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `MTM_WIP_Application_Winforms.csproj` (`net8.0-windows`, `UseWindowsForms`) | `MTM_WIP/MTM_WIP.csproj` (`net10.0-windows10.0.19041.0`, `UseWinUI`, `WindowsPackageType=None`) | One WinExe. Target framework, minimum platform version and packaging mode fixed by [`../constitution.md`](../constitution.md) §1. |
| Published name `MTM_Inventory_Application.exe`, produced today by passing `-p:AssemblyName` at publish time | `<AssemblyName>MTM_Inventory_Application</AssemblyName>` in the project | The MTM Loader catalogue names this executable. Setting it in the project makes debug output match the shipped binary and removes a place for the two to diverge. |
| `<Version>6.8.0</Version>` repeated across four assembly attributes in the csproj | Single `<Version>` in `Directory.Build.props` | One source of truth. The publish script in `011` reads it and refuses to write a manifest when it disagrees with the folder name. |
| Folder structure: `Forms/`, `Controls/`, `Data/`, `Services/`, `Helpers/`, `Models/`, `Core/` all in one assembly | 11 projects with a declared reference graph | A module may reference `Core`, `Shared`, `Data`. No module may reference another module. `Core` references nothing. |
| `<Compile Remove="Database\**" />`, `<None Remove="specs\**" />` exclusions | Not needed | The new solution contains no SQL folders; scripts stay in the WinForms repository. |
| `<Content Include="Documentation\Help\**\*">` with a `Link` target | `Assets/Help/Content/**` as `Content` + `PreserveNewest` | 15 category files and 54 topics ported verbatim as data. |
| `<Content Include="Resources\MTM.png">`, `<ApplicationIcon>MTMWIPApp.ico</ApplicationIcon>` | `Assets/MTM.png`, `Assets/mtm.ico` with `ApplicationIcon` set | Same logo; new executable name. |
| `<Content Include="RELEASE_NOTES.json">` | `Assets/ReleaseNotes/RELEASE_NOTES.json` | Read by the About surface in `007`. |
| `<Content Include="Resources\default_shortcuts.json">` | `Assets/default_shortcuts.json` | Seeded into the database only when values differ (see `003`). |
| `<Content Include="Controls\MainForm\WIPAppTemplate.xlsx">` | `Assets/Templates/WIPAppTemplate.xlsx` | Opened by Advanced Inventory import in `005`. |
| `Properties/Resources.resx` with an embedded logo | `Assets/` files, no `.resx` | WinUI resolves assets by URI; there is no designer resource file. |
| `<NoWarn>$(NoWarn);CA1416;MSB3277</NoWarn>` | Not carried over | Platform-compatibility warnings are meaningful now. Warnings are errors. |
| No SDK pinning anywhere in the fleet | `global.json` + `Directory.Packages.props` + committed `packages.lock.json` | `000-pre-foundation` D49/D50. A version appears once, and a changed transitive dependency fails `--locked-mode` restore instead of arriving silently. |
| `App.config` with plaintext Visual credentials | `appsettings.json` (committed, redacted) + `%PROGRAMDATA%\MTM_WIP\appsettings.local.json` (untracked) | `000-pre-foundation` D51, following `MTM_Receiving_Application`'s pattern with the override moved outside the swapped payload. |
| `specs/` and `.specify/` inside the source tree | Left in the repository, outside the solution | Documentation is not compiled. |

### Project inventory this spec creates

| Project | Kind | References | Purpose |
| :--- | :--- | :--- | :--- |
| `MTM_WIP` | WinExe | everything | Composition root, `App.xaml`, `MainWindow` |
| `MTM_WIP.Core` | library | none | Contracts, models, result type, cross-cutting services, theme dictionaries |
| `MTM_WIP.Shared` | library | `Core` | Reusable views, dialogs, controls used by two or more modules |
| `MTM_WIP.Data` | library | `Core` | DAOs, stored-procedure helper, connection options, Visual ERP reads |
| `MTM_WIP.Startup` | library | `Core`, `Shared` | Launch pipeline and its stages |
| `MTM_WIP.Module_Shell` | library | `Core`, `Shared` | Navigation shell |
| `MTM_WIP.Module_Inventory` | library | `Core`, `Shared`, `Data` | Inventory, Remove, Transfer, Quick Buttons |
| `MTM_WIP.Module_Transactions` | library | `Core`, `Shared`, `Data` | Transaction search, grid, lifecycle, analytics |
| `MTM_WIP.Module_Settings` | library | `Core`, `Shared`, `Data` | Settings, users, master data, shortcuts |
| `MTM_WIP.Module_Visual` | library | `Core`, `Shared`, `Data` | ERP analytics and charts |
| `MTM_WIP.Module_DeveloperTools` | library | `Core`, `Shared`, `Data` | Scaffold only in v1 |
| `MTM_WIP.Tests` | test | `Core`, `Data`, all modules | xUnit, `UseWinUI=false` |

---

## 3. Behavioral Edge Cases & State Constraints

- **Platform version is not a free choice.** `DevWinUI` 10.9.1 ships `net10.0-windows10.0.19041` assemblies. Choosing a different `TargetFramework` silently breaks the dialog wrapper that `003` depends on. The value is fixed, not preferred.
- **Windows App SDK version follows `MTM_Waitlist`.** Three applications share one framework. An independent upgrade here creates a version skew that only shows up on a machine that runs two of them.
- **Minimum platform is Windows 10 22H2 (`10.0.19041.0`)**, raised from 1809 by [`../000-pre-foundation/spec.md`](../000-pre-foundation/spec.md) D45. Mica is still unavailable on Windows 10, so nothing here or in `004` may assume Windows 11, and the solid `ThemeResource` fallback stays mandatory.
- **Both applications must run side by side during the transition.** They use different mutex names (`Global\MTM_WIP_WinUI3_SingleInstance` vs the WinForms `Global\MTM_WIP_Application_Winforms_SingleInstance`) and different executable names. Do not "tidy" either.
- **No project may reference WinForms.** Enforced structurally: the new solution has no `UseWindowsForms`, so any `using System.Windows.Forms` fails to compile — and the forbidden-API gate catches a copy-pasted file before the compiler has to.
- **The test project must not need the Windows App SDK.** `UseWinUI=false`. A ViewModel that only works inside a WinUI host cannot be tested, and `003`–`009` depend on that being possible.
- **Assets are data, not code.** Help content, release notes and the shortcut defaults are copied, not retyped. Retyping content would silently change user-facing text.

### State and persistence introduced

None. This spec writes no user data, opens no log file, and creates no application-state folder. It only places read-only assets in the build output.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | The exact `Microsoft.WindowsAppSDK` patch that `MTM_Waitlist` currently builds with | Must be read from that repository at scaffold time. Do not guess, and do not take the latest. |
| 2 | Whether any machine that must run the new application is below Windows 10 22H2 (`10.0.19041.0`) | Assumed no — the floor was raised from 1809 by `000-pre-foundation` D45 and no inventory was taken. A machine below 22H2 is excluded by the new floor. |
| 3 | Whether `RELEASE_NOTES_USER_FRIENDLY.md` (83 KB, rendered by nothing today) should ship | Undecided. Not ported in this spec; recorded in [`../00-index.md`](../00-index.md) Q-list. |
| 4 | Whether the six placeholder `Resources/Sql/Visual/*.sql` files have any owner | They contain single-line `SELECT '… Placeholder'` statements and are dead. Not ported. |
| 5 | Whether the solution ships a `Directory.Packages.props` (central package management) and a `global.json` | **Adopted** — both, by `000-pre-foundation` D49/D50, together with committed lock files. This application becomes the only MTM application with a reproducible toolchain; the two siblings keep their per-project versions. |

### Assumptions

1. .NET 10 SDK and Visual Studio 2022 (or the `dotnet new` WinUI templates, which are still preview — `000-pre-foundation` D35) are installed.
2. The machine builds `x64` only; no `arm64` floor machine exists.
3. **The WinUI 3 solution lives in its own repository** (`000-pre-foundation` D29). The WinForms solution is frozen at 6.8.0 and remains buildable in its own repository for side-by-side comparison; the two are never in one solution.
4. **PowerShell 7 (`pwsh`) is installed** on every machine that runs Serena (`000-pre-foundation` D34), in addition to Windows PowerShell 5.1.
