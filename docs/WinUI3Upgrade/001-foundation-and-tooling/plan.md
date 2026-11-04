# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `001-foundation-and-tooling`
**Tasks**: TASK-001 … TASK-013

---

## 1. Project Topology & Directory Mapping

### 1.1 What this spec creates

```text
MTM_WIP.sln
├── Directory.Build.props
├── Directory.Packages.props              # every package version; Central Package Management (D49)
├── global.json                           # pins the .NET 10 SDK feature band (D49)
├── .editorconfig
├── .gitignore                            # must include appsettings.local.json (D51)
│
├── MTM_WIP/                              # WinExe
│   ├── MTM_WIP.csproj
│   ├── app.manifest
│   ├── appsettings.json                  # committed; credentials redacted (D51)
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

### 1.1a Build-reproducibility files (`000-pre-foundation` D49, D50)

Three files exist before the first project does, because retrofitting them after forty package references exist is a rewrite rather than an edit.

**`global.json`** pins the SDK feature band. Without it, "it builds on my machine" means "whatever .NET SDK that machine had", which is the state both sibling repositories are in today.

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestFeature"
  }
}
```

`latestFeature` is deliberate: it accepts a newer patch or feature band but never a new major version, so a servicing update on one machine does not change the toolchain.

**`Directory.Packages.props`** holds every `PackageReference` version. Project files then carry `<PackageReference Include="..." />` with **no `Version` attribute**, which is the only form Central Package Management accepts. Two consequences worth stating: a version appears exactly once in the repository, and a `<PackageReference>` with an inline `Version` is a build-breaking review defect rather than a style preference.

**Lock files.** `<RestorePackagesWithLockFile>true</RestorePackagesWithLockFile>` in `Directory.Build.props` makes restore emit `packages.lock.json` per project; those files are committed. A change in a transitive dependency then fails `dotnet restore --locked-mode` instead of arriving silently between two builds.


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
    <TargetPlatformMinVersion>10.0.19041.0</TargetPlatformMinVersion>
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
    <TargetPlatformMinVersion>10.0.19041.0</TargetPlatformMinVersion>
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

### 3.1 Configuration files (`000-pre-foundation` D51)

Two files, following `MTM_Receiving_Application`: a **committed** `appsettings.json` and an **untracked** `appsettings.local.json` override.

```jsonc
// MTM_WIP/appsettings.json — committed, credentials redacted
{
  "ConnectionStrings": {
    "MySql":      "Server=172.16.1.104;Port=3306;Database=mtm_wip_application_winforms;Uid=...;******;CharSet=utf8mb4;",
    "InforVisual":"Server=VISUAL;Database=MTMFG;User Id=SHOP2;******;TrustServerCertificate=True;ApplicationIntent=ReadOnly;"
  },
  "Serilog": {
    "MinimumLevel": { "Default": "Information", "Override": { "Microsoft": "Warning", "System": "Warning" } },
    "WriteTo": [ { "Name": "File", "Args": { "path": "logs/app-.txt", "rollingInterval": "Day",
                   "retainedFileCountLimit": 30, "shared": true } } ]
  }
}
```

**The override loads from `%PROGRAMDATA%\MTM_WIP\appsettings.local.json`, not from `AppContext.BaseDirectory`.** This is the one place this spec deliberately diverges from `MTM_Receiving_Application`. That repository reads its override from beside the executable; the MTM Loader replaces the payload folder wholesale on every release (D16, D29), so a file there is deleted at each update. `%PROGRAMDATA%` is outside the payload, so the override survives — same filename, same keys, same precedence (`AddJsonFile` last wins), one directory that is not swapped.

Three consequences this spec must honour, because getting any of them wrong is silent:

1. **`.gitignore` must exclude `appsettings.local.json`.** A committed credential is a release blocker (`constitution.md` §2 Rule 4). `MTM_Receiving_Application` has no such rule today, which is the defect this avoids repeating.
2. **`.csproj` carries `appsettings.json` as `None` + `CopyToOutputDirectory=PreserveNewest`.** Without it the committed defaults never reach the output folder and the application has no connection string at all.
3. **The override is optional.** `AddJsonFile(..., optional: true, reloadOnChange: false)`. An absent override must leave behaviour identical to the committed defaults — that is the property `003`'s configuration store is tested against.

Binding is `services.Configure<DatabaseSettings>(configuration.GetSection("ConnectionStrings"))` plus `configuration.GetConnectionString("MySql")`, matching the sibling repository so a developer moving between the two applications sees one pattern.
