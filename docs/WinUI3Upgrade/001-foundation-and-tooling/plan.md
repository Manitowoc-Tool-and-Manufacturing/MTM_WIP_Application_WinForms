# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `001-foundation-and-tooling`
**Tasks**: TASK-001 … TASK-013

---

## 1. Project Topology & Directory Mapping

### 1.1 What this spec creates

```text
MTM_WIP.sln
├── Directory.Build.props
├── .editorconfig
├── .gitignore
│
├── MTM_WIP/                              # WinExe
│   ├── MTM_WIP.csproj
│   ├── app.manifest
│   ├── App.xaml
│   ├── App.xaml.cs                       # template default; replaced in 004
│   ├── MainWindow.xaml                   # template default; replaced in 004
│   ├── Assets/
│   │   ├── mtm.ico
│   │   ├── MTM.png
│   │   ├── default_shortcuts.json
│   │   ├── Help/Content/                 # 15 category files, verbatim
│   │   ├── ReleaseNotes/RELEASE_NOTES.json
│   │   └── Templates/WIPAppTemplate.xlsx
│   └── Themes/                           # empty; filled in 003
│
├── MTM_WIP.Core/MTM_WIP.Core.csproj
├── MTM_WIP.Shared/MTM_WIP.Shared.csproj
├── MTM_WIP.Data/MTM_WIP.Data.csproj
├── MTM_WIP.Startup/MTM_WIP.Startup.csproj
├── MTM_WIP.Module_Shell/MTM_WIP.Module_Shell.csproj
├── MTM_WIP.Module_Inventory/MTM_WIP.Module_Inventory.csproj
├── MTM_WIP.Module_Transactions/MTM_WIP.Module_Transactions.csproj
├── MTM_WIP.Module_Settings/MTM_WIP.Module_Settings.csproj
├── MTM_WIP.Module_Visual/MTM_WIP.Module_Visual.csproj
├── MTM_WIP.Module_DeveloperTools/MTM_WIP.Module_DeveloperTools.csproj
│
└── MTM_WIP.Tests/
    ├── MTM_WIP.Tests.csproj
    └── Architecture/
        ├── ProjectReferenceMatrixTests.cs
        └── ForbiddenApiTests.cs
```

Libraries are created empty in this spec. Each later spec fills only its own project:

| Project | Filled by |
| :--- | :--- |
| `MTM_WIP.Core` | `002` (result type), `003` (contracts and services) |
| `MTM_WIP.Data` | `002` |
| `MTM_WIP.Shared` | `003`, `009` |
| `MTM_WIP.Startup` | `004` |
| `MTM_WIP.Module_Shell` | `004`, `009` |
| `MTM_WIP.Module_Inventory` | `005` |
| `MTM_WIP.Module_Transactions` | `006` |
| `MTM_WIP.Module_Settings` | `007` |
| `MTM_WIP.Module_Visual` | `008` |
| `MTM_WIP.Module_DeveloperTools` | `010` |
| `MTM_WIP` (app) | `004` (composition root), `011` (publish) |

### 1.2 Exact project file contents

**`MTM_WIP/MTM_WIP.csproj`** — app project:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    <RootNamespace>MTM_WIP</RootNamespace>
    <AssemblyName>MTM_Inventory_Application</AssemblyName>
    <ApplicationIcon>Assets\mtm.ico</ApplicationIcon>
    <ApplicationManifest>app.manifest</ApplicationManifest>
    <Platforms>x64</Platforms>
    <PlatformTarget>x64</PlatformTarget>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <UseWinUI>true</UseWinUI>
    <EnableMsixTooling>true</EnableMsixTooling>
    <WindowsPackageType>None</WindowsPackageType>
    <WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>
  </PropertyGroup>
</Project>
```

**Library project** (`MTM_WIP.Core`, `Shared`, `Data`, `Startup`, all `Module_*`):

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
    <TargetPlatformMinVersion>10.0.17763.0</TargetPlatformMinVersion>
    <RootNamespace>MTM_WIP.Core</RootNamespace>
    <UseWinUI>true</UseWinUI>
    <RuntimeIdentifiers>win-x86;win-x64;win-arm64</RuntimeIdentifiers>
  </PropertyGroup>
</Project>
```

Libraries carry no `ApplicationManifest`, no `EnableMsixTooling`, and no `RuntimeIdentifier` — exactly as the sibling solution arranges it.

**`MTM_WIP.Tests/MTM_WIP.Tests.csproj`**:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0-windows10.0.19041.0</TargetFramework>
    <RootNamespace>MTM_WIP.Tests</RootNamespace>
    <UseWinUI>false</UseWinUI>
    <RuntimeIdentifier>win-x64</RuntimeIdentifier>
    <IsPackable>false</IsPackable>
  </PropertyGroup>
</Project>
```

**`Directory.Build.props`**:

```xml
<Project>
  <PropertyGroup>
    <Version>1.0.0</Version>
    <UseRidGraph>true</UseRidGraph>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
    <WindowsAppSDKDeploymentManagerInitialize>false</WindowsAppSDKDeploymentManagerInitialize>
  </PropertyGroup>
</Project>
```

The last property is set when `AppxPackage` is `false` in the sibling repositories; here it is unconditional because the application is always unpackaged.

### 1.3 Reference matrix as data

The matrix is declared once, in the test project, as a table the test walks:

```
Core        → (none)
Data        → Core
Shared      → Core
Startup     → Core, Shared
Module_*    → Core, Shared, Data
Tests       → Core, Data, all Module_*
```

Any reference not in the "may reference" column is a failure, and any missing reference that a project actually uses is caught by the compiler before the test runs.

---

## 2. Component Migration Strategy

### 2.1 Creating the solution

1. Generate the app project from the **Blank App, Packaged (WinUI 3 in Desktop)** template, then switch it to unpackaged by setting `WindowsPackageType=None` and deleting the `Package.appxmanifest` and the `EnableMsixTooling`-dependent packaging pieces the template adds.
2. Add eight `classlib` projects with `dotnet new classlib`, then set `UseWinUI=true` and the TFM manually. The WinUI template does not offer a library shape.
3. Add the test project with `dotnet new xunit`, then set `UseWinUI=false`.
4. Add `ProjectReference` entries strictly from §1.3.

A library that sets `UseWinUI=true` but references no XAML still needs the property: it is what brings in the WinUI reference assemblies so a `UserControl` in `Shared` compiles.

### 2.2 The two gates

**`ProjectReferenceMatrixTests`** — reads the loaded assemblies' `Assembly.GetReferencedAssemblies()` and asserts the graph. This catches a reference added in a `.csproj` and also a transitive leak. The test names each violation with both project names so the failure is actionable without reading the test.

**`ForbiddenApiTests`** — walks `*.cs` and `*.xaml` under the solution root (excluding `bin`, `obj`, `docs`, `specs`, `.specify`) and asserts zero matches for:

| Pattern | Reason |
| :--- | :--- |
| `System.Windows.Forms` | The migration's entire purpose |
| `MessageBox` | Replaced by `IErrorService`/`IDialogService` (`003`) |
| `MySql.Data` | Replaced by `MySqlConnector` (`002`) |
| `BackgroundWorker` | Replaced by `Task` + `IDispatcherService` (`003`) |
| `Colors.` | Literal colours break theming (`003`) |
| `#[0-9A-Fa-f]{6}` inside `*.xaml` | Same |

The scan is a plain file walk, not a Roslyn analysis, so it runs in under a second and has no dependency on the compiler.

### 2.3 Assets

Copy the following, byte-for-byte, from the WinForms repository:

| Source | Destination |
| :--- | :--- |
| `MTMWIPApp.ico` | `MTM_WIP/Assets/mtm.ico` |
| `Resources/MTM.png` | `MTM_WIP/Assets/MTM.png` |
| `Resources/default_shortcuts.json` | `MTM_WIP/Assets/default_shortcuts.json` |
| `Documentation/Help/JSON/*.json` (15 files) | `MTM_WIP/Assets/Help/Content/` |
| `RELEASE_NOTES.json` | `MTM_WIP/Assets/ReleaseNotes/RELEASE_NOTES.json` |
| `Controls/MainForm/WIPAppTemplate.xlsx` | `MTM_WIP/Assets/Templates/WIPAppTemplate.xlsx` |

Not copied: `Documentation/Help/Templates/**` (native rendering replaces them), `Documentation/Help/images/**` (referenced by no code), `Resources/Html/**` (native charts), `Resources/Sql/Visual/**` (dead placeholders), `App.config` (replaced by `appsettings.json` in `004`).

Asset items use `CopyToOutputDirectory=PreserveNewest` and preserve folder structure, so a content file's relative path in the output matches the path the loader code will ask for.

### 2.4 `app.manifest`

Per-monitor v2 DPI awareness and a `<supportedOS>` list that matches `TargetPlatformMinVersion` (Windows 10). Without the DPI section, the application is bitmap-scaled at 150 % and the grids blur — which is exactly the kind of defect that is blamed on WinUI rather than on the manifest.

---

## 3. Data & State Migration

This spec introduces no data access and no state. What it does introduce is the *shape* the later specs must fill:

| Introduced here | Filled by | Contract |
| :--- | :--- | :--- |
| `MTM_WIP.Core/Models/` folder | `002` | `Model_Result<T>` lives here so `Data` can return it without referencing `Shared` |
| `MTM_WIP.Core/Contracts/` folder | `003` | Every service interface; this is why `Data` and `Shared` never reference each other |
| `MTM_WIP.Data/Contracts/` folder | `002` | DAO interfaces, referenced only by modules |
| `MTM_WIP/Services/DependencyInjection/` folder | `004` | Composition root with per-module partial registration methods |
| `MTM_WIP/Themes/` folder | `003` | Merged resource dictionaries. Named `Themes/` per `constitution.md` §2 Rule 9, which is authoritative on this point |

`Version` in `Directory.Build.props` is the only persisted value this spec owns. `011` reads it at publish time and refuses to proceed when it does not match the manifest being written.
