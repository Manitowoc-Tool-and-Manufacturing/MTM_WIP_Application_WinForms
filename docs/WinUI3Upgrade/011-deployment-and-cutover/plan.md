# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `011-deployment-and-cutover`
**Tasks**: TASK-130 … TASK-137

---

## 1. Project Topology & Directory Mapping

### 1.1 What this spec touches

No project, source file, XAML, or test under the WinUI 3 solution changes in this spec. The build is the one produced by `001`–`010`; this spec only packages, ships, and signs it off.

```text
scripts/Publish-MtmWipPayload.ps1            # adapted: reads Directory.Build.props, publishes MTM_WIP.csproj
artifacts/payload/win-x64/                    # local staging (git-ignored by spec 001)
\\mtmanu-fs01\Expo Drive\MH_RESOURCE\Material_Handler\MTM Application Store\MTM WIP Application\
                                              # the shared store folder the loader reads
```

The payload folder the loader reads contains exactly:

```text
MTM WIP Application/
├── MTM_Inventory_Application.exe             # named in the loader catalogue
├── build-manifest.json                       # written last; the loader's update trigger
└── … self-contained win-x64 runtime files
```

### 1.2 Exact publish command line

`AssemblyName` and `<Version>` live in the project artefacts created by `001` (`MTM_WIP.csproj` and `Directory.Build.props`). The script re-derives them from source and never hard-codes them:

```powershell
dotnet publish MTM_WIP/MTM_WIP.csproj -c Release -r win-x64 --self-contained true `
  -p:Platform=x64 `
  -p:WindowsPackageType=None `
  -p:WindowsAppSDKSelfContained=true `
  -p:PublishSingleFile=false `
  -p:PublishTrimmed=false `
  -p:AssemblyName=MTM_Inventory_Application `
  -o <staging>
```

This is the same switch set the WinForms script already uses, with the project path changed. `-p:AssemblyName` is kept even though `001` set it in the csproj, so the shipped name can never silently diverge from the loader catalogue.

### 1.3 The `build-manifest.json` shape (schemaVersion 1)

```json
{
  "schemaVersion": 1,
  "applicationId": "wip",
  "version": "1.0.0",
  "publishedUtc": "2026-11-02T00:00:00Z",
  "executable": "MTM_Inventory_Application.exe",
  "fileCount": 3,
  "totalBytes": 5123456,
  "files": [
    { "path": "MTM_Inventory_Application.exe", "size": 1048576, "sha256": "0a1b2c…lowercase-hex…" },
    { "path": "MTM_WIP.dll", "size": 4096, "sha256": "…" }
  ]
}
```

Rules: `version` is the 3-part semver from `Directory.Build.props`; `applicationId` is `wip`; `executable` is `MTM_Inventory_Application.exe`; `files[]` is sorted by `path`, paths use forward slashes, `sha256` is lowercase hex, `fileCount` and `totalBytes` are computed from the folder, and the manifest is **written last**.

---

## 2. Component Migration Strategy

### 2.1 The publish script — adapt, do not rewrite

`scripts\Publish-MtmWipPayload.ps1` already implements the loader contract for the WinForms build: it reads `<Version>`, publishes self-contained, computes the manifest, verifies the staged copy file-by-file, and swaps by atomic rename with the manifest written last. The WinUI 3 script is the **same script** with two deltas:

1. The project argument is `MTM_WIP/MTM_WIP.csproj`, not `MTM_WIP_Application_Winforms.csproj`.
2. The version source is `Directory.Build.props` at the solution root, not a `<Version>` inside the csproj.

The sibling reference `MTM_Receiving_Application\Database\Publish_and_Installation\Publish-App-To-Loader.ps1` provides the parameter shape the WinUI script must accept so all three applications publish identically: `-Version` (must match `\d+\.\d+\.\d+` and the application's own version source), `-DestinationRoot`, `-StagingDirectory`, `-ApplicationId`, `-Executable`, `-SkipPublish`, `-FullVerify`, `-Force`. The WinUI script exposes the equivalent surface (`-Destination`, `-Version`, `-Force`, `-FullVerify`) with `-ApplicationId` fixed to `wip` and `-Executable` fixed to `MTM_Inventory_Application.exe`.

### 2.2 Version source and the single-truth rule

`Directory.Build.props` is the only persisted version value. The script:

1. Reads `<Version>` from `Directory.Build.props`; if absent, it errors.
2. Reduces it to 3-part semver (appends `0` parts if fewer than three).
3. Writes that exact value into the manifest.
4. Refuses to proceed when a caller-supplied `-Version` disagrees with `Directory.Build.props`.

### 2.3 Staged-swap publish sequence (numbered, must be preserved)

1. **Stage locally.** `dotnet publish` (command in §1.2) into a clean local staging folder; wipe it first.
2. **Assert the payload.** Refuse if the folder is empty or does not contain `MTM_Inventory_Application.exe`.
3. **Enumerate and hash.** Every file → `{ path (forward slashes), size, sha256 (lowercase hex) }`, sorted by path.
4. **Write the manifest locally**, last, into the staging folder.
5. **Stage beside the destination.** Create `"$Destination.staging"` in the destination's parent (same volume); copy the payload, **excluding** the manifest, into it.
6. **Verify file-by-file.** Re-hash every staged file against the manifest; on any missing file or hash mismatch, delete the staging folder and abort — the live folder is untouched.
7. **Version-reuse guard.** If the destination already has a manifest with the same `version` and a different executable hash, refuse unless `-Force` (see §2.4).
8. **Swap.** Rename the live destination to `"$Destination.previous"`, rename `"$Destination.staging"` into place. (Cross-volume rename is refused in step 5/8.)
9. **Advertise last.** Copy `build-manifest.json` into the destination now — the loader sees the new version only at this point.
10. **Delete the previous copy** only after the swap is complete. On any failure before step 9, the previous folder is restored or left in place.

### 2.4 Version-reuse rule

A version is never reused with a different executable. The guard compares the incoming `MTM_Inventory_Application.exe` SHA-256 against the hash recorded for the same `version` at the destination (or in a local published-versions ledger when the share is unreachable). A difference aborts publish. `-Force` bypasses the guard, and the bypass is written to the publish output. Stale destination files — files at the destination not present in the incoming manifest — are reported in the output, never silently deleted; the swap replaces the folder and the report records what the replacement removed.

### 2.5 Cutover — decided

Both generations ship `MTM_Inventory_Application.exe`.

**Chosen (2026-11-02, product owner): option (a) — the two applications deliberately share the name, and the WinUI 3 payload replaces the WinForms payload as a wholesale folder swap once the migration is complete.**

The loader catalogue, the `applicationId: "wip"` value and the manifest schema are all untouched. The swap reuses the sequence already specified in §2.3, which is what makes the decision safe:

1. Publish to `<Destination>.staging` — a sibling on the **same volume**.
2. Verify staging file-by-file against the incoming manifest (size and SHA-256).
3. Rename the live folder aside to `<Destination>.previous-<version>`. Do not delete it.
4. Rename staging into place with an atomic same-volume rename.
5. Write `build-manifest.json` into the destination **last**.
6. Re-verify the destination against its own manifest.
7. Keep `<Destination>.previous-<version>` until the new version has started successfully on a floor machine.

**Rejected, recorded so they are not re-litigated:**

| Option | Why rejected |
| :--- | :--- |
| **(b) Second catalogue id / second source folder** | Requires amending the loader catalogue and breaks the fixed `applicationId: "wip"` rule. The requirement is one application, not two. |
| **(c) Rename the payload folder at cutover** | Leaves an inert rollback folder that still shares the executable name, inviting confusion about which folder the loader will pick up. |

**What this costs.** No loader-mediated side-by-side and no staged rollout — a bad build reaches every machine in one move. That is precisely why TASK-136's full verification and TASK-137's parity walk gate the cutover, and why step 7 is mandatory.

**What it does not change.** The WinForms build keeps its source, its publish script and its distinct mutex (`Global\MTM_WIP_Application_Winforms_SingleInstance`). It is retired by replacing the payload folder, not by deleting anything from source control.

### 2.6 Side-by-side operation

- The publish script never edits the live folder in place and never writes a file the loader may be mid-copy on; it stages beside and swaps (§2.3).
- The WinForms build keeps its own publish path and its own mutex name. Under the chosen cutover (§2.5) it is **not** available through the loader after the swap; it remains reachable from source or from the preserved `.previous-<version>` folder.
- The local loader install (`%LOCALAPPDATA%\MTM Applications\MTM Loader\wip\<version>\`) is written by the loader, never by the publish script.

### 2.7 Loader contract

- Catalogue entry: `id: "wip"`, source folder `MTM WIP Application`, executable `MTM_Inventory_Application.exe`.
- The loader refuses to update an application whose folder has no `build-manifest.json`; the script therefore always writes it, last.
- The loader stores and compares UNC paths and never writes to the share. The publish writes to the share only via the staged swap.
- Legacy copy under `%USERPROFILE%\Documents\MTM Applications\` is removed only after the new local version is verified and active — verified in TASK-134, not enforced by this script.

---

## 3. Data & State Migration

### 3.1 No database migration

The database is frozen ([`../constitution.md`](../constitution.md) Core Rule 4). Cutover moves an executable and a manifest; it moves no rows, creates no table, and changes no procedure. The production database `mtm_wip_application_winforms` and the Visual ERP reads are unaffected by the publish.

### 3.2 State that moves (and the ordering that protects it)

| State | Owner | Migration rule |
| :--- | :--- | :--- |
| Shared store payload folder | Publish script | Replaced whole-folder via staged swap; manifest last. |
| Loader local install `%LOCALAPPDATA%\MTM Applications\MTM Loader\wip\<version>\` | MTM Loader | Created by the loader on update; never touched by the script. |
| Legacy copy `%USERPROFILE%\Documents\MTM Applications\` | MTM Loader | Removed by the loader only after the new local version is verified and active; the script must not delete it. |
| User settings / DGV layouts / shortcuts (DB) | Application | Unchanged; same database, same tables, same procedures. |
| Version number | `Directory.Build.props` | Single 3-part semver; copied into the manifest; never reused with a different executable hash unless `-Force`. |

### 3.3 State constraints

- `publishedUtc` is generated at publish time and recorded in the manifest; it is not persisted anywhere else.
- The local staging folder (`artifacts/payload/win-x64`) is disposable and git-ignored; it is wiped before each publish.
- No credential, connection string, or share password appears in the script, the manifest, or the publish output. (The share is accessed with the runner's existing session.)
