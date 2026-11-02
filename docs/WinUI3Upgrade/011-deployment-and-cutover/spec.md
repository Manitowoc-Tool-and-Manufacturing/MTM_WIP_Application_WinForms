# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `011-deployment-and-cutover`
**Depends on**: `001-foundation-and-tooling` (`AssemblyName`, the single `<Version>` in `Directory.Build.props`, asset layout), `010-developer-tools-scaffold` (the last functional spec)
**Unblocks**: nothing — this is the final spec; it closes the pack
**Tasks**: TASK-130 … TASK-137
**Governing rules**: [`../constitution.md`](../constitution.md) §1.2 (packaging and deployment), §3.4 (preserved contracts: manifest schema and executable name), §4 (verification gates)

---

## 1. Executive Summary & Scope

This spec publishes the finished WinUI 3 application as the folder payload the MTM Loader launches, executes the cutover from the WinForms build, and signs off the entire migration pack. It contains no application behaviour: nothing here changes a screen, a query, a procedure, or a theme. Its subject is the release path — how `MTM_Inventory_Application.exe` and `build-manifest.json` reach the shared application store, how the two generations of the application coexist during the transition, and how "done" is proven for the whole pack.

### In scope

- The publish script for the WinUI 3 project, reusing the staged-swap and manifest-last rules already proven in `scripts\Publish-MtmWipPayload.ps1`.
- The `build-manifest.json` schemaVersion 1 contract, byte-for-byte compatible with the WinForms script's output.
- The MTM Loader contract: catalogue entry, local install path, and the ordering rule that removes the legacy copy only after the new one is verified and active.
- Side-by-side operation of the WinForms build and the WinUI 3 build during the transition.
- The cutover decision (see §3) — how the shared folder transitions from the old payload to the new one.
- The full verification suite and the whole-pack parity sign-off.

### Out of scope

- Any screen, ViewModel, DAO, service, or theme. Those are `003`–`010`.
- Any database change. The database is frozen per [`../constitution.md`](../constitution.md) Core Rule 4; this spec moves no data and touches no procedure.
- Anything the MTM Loader itself does. The loader is a separate product; this spec only honours its contract.

### Success condition

The loader launches the published WinUI 3 payload from the shared store; the manifest validates against the folder it describes; republishing a version with a different executable is refused; the WinForms build still runs from its own path during the transition; and every row of every §2 mapping table in specs `001`–`010` is either implemented or filed as a defect with a reference.

---

## 2. Legacy Feature Assessment & UI Mapping Table

No visual controls are in scope. The table maps the deployment artefacts that must survive, or be deliberately replaced, at cutover.

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `scripts\Publish-MtmWipPayload.ps1` — reads `<Version>` from `MTM_WIP_Application_Winforms.csproj`, publishes `net8.0` self-contained, writes `build-manifest.json` last, then verifies and swaps | Same script, parameterised for the WinUI 3 project — reads `<Version>` from `Directory.Build.props`, publishes `net10.0-windows10.0.19041.0` self-contained `win-x64`, writes the same manifest, same staged swap | The publish sequence is identical; only the project path and version source change. The WinForms script is the reference implementation and must not diverge from it. |
| `-p:AssemblyName=MTM_Inventory_Application` passed at publish time | `<AssemblyName>MTM_Inventory_Application</AssemblyName>` set in `MTM_WIP.csproj` by `001` | The published executable must be `MTM_Inventory_Application.exe`, the exact name in the loader catalogue. The publish command still passes the property so the shipped name can never silently drift from the catalogue. |
| `<Version>6.8.0</Version>` repeated in the WinForms csproj | Single `<Version>` in `Directory.Build.props` (created by `001`) | One source of truth, 3-part semver. The script reads it and refuses to write a manifest whose `version` differs. |
| `build-manifest.json` produced by the WinForms script | Identical schemaVersion 1 output | Fields `schemaVersion`, `applicationId`, `version`, `publishedUtc`, `executable`, `fileCount`, `totalBytes`, `files[]` with per-file `{ path, size, sha256 }`. Forward-slash paths, lowercase-hex SHA-256. |
| Loader catalogue entry `id: "wip"`, source folder `MTM WIP Application`, executable `MTM_Inventory_Application.exe` | Unchanged | The loader reads this entry; this spec must not rename the id, the folder, or the executable. |
| Single-instance mutex `Global\MTM_WIP_Application_Winforms_SingleInstance` | `Global\MTM_WIP_WinUI3_SingleInstance` (set in `004`) | Distinct names so both applications can hold their own mutex at the same time. Do not reuse or "tidy" either name. |
| Legacy install copy under `%USERPROFILE%\Documents\MTM Applications\` | Removed **only after** the new local install is verified and active | The loader owns this ordering; this spec must not delete the legacy copy early. |
| Loader local install path `%LOCALAPPDATA%\MTM Applications\MTM Loader\wip\<version>\` | Unchanged | The loader copies the folder payload here and launches from it. The spec asserts this path, it does not create it. |
| Destination share `\\mtmanu-fs01\Expo Drive\MH_RESOURCE\Material_Handler\MTM Application Store\MTM WIP Application` | Unchanged | The publish writes here and nowhere else. |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 The executable-name collision — **decided**

Today **both** the WinForms build and the WinUI 3 build ship an executable named `MTM_Inventory_Application.exe`. The WinForms build gets the name from `-p:AssemblyName` at publish time; the WinUI 3 build gets it from `<AssemblyName>` in its csproj. The loader catalogue names the same file for `id: "wip"`. Two different programs with the same name cannot occupy the same loader source folder at the same time.

**Decision (2026-11-02, product owner): option (a) — the two applications deliberately share the name, and the WinUI 3 payload replaces the WinForms payload as a wholesale folder swap once the migration is complete.**

Consequences, all of which are now binding:

- **The executable name does not change.** `MTM_Inventory_Application.exe` stays, `ApplicationId` stays `"wip"`, the catalogue entry stays untouched, and the manifest schema gains no new field.
- **There is no loader-mediated side-by-side.** Only one generation is live at a time. The WinForms build survives only as a pre-cutover copy on the share or in source control, reachable by restoring the folder by hand.
- **Cutover is a single event, not a staged rollout.** This raises the cost of shipping a broken build, which is why TASK-136's full verification and TASK-137's parity walk are gating, not advisory.
- **Rollback is a folder restore.** The publish script must leave a recoverable copy of the replaced folder until the new version has been confirmed running on a floor machine. Any rollback that depends on rebuilding from source is not a rollback.
- Options (b) and (c) in [`plan.md`](./plan.md) §2.5 are recorded as rejected with their reasons, so they are not re-litigated later.

### 3.2 Edge cases

- **A half-copied payload is never advertised.** The manifest is the loader's trigger. It is written into the destination folder **last**, after the payload is staged, verified file-by-file, and swapped into place. An interrupted copy leaves either the old manifest or no manifest, so the loader either sees the previous version or refuses to update.
- **Publish refuses an empty folder or a folder without the executable.** Both are errors that abort before any swap; a payload the loader cannot launch is never advertised.
- **The executable named in the manifest must equal the catalogue executable.** A mismatch is a publish error, not a warning.
- **A version is never reused with a different executable hash.** If the destination already carries version `N` and the incoming `MTM_Inventory_Application.exe` hashes differently, publish is refused unless explicitly forced (`-Force`). Bump `<Version>` instead.
- **Stale destination files are reported, never silently deleted.** A file present at the destination but absent from the incoming manifest is listed in the publish output. The swap replaces the folder wholesale; the report records what the replacement removed so nothing disappears without a record.
- **The publish must not overwrite a live folder the loader may be copying.** Staging happens in a sibling folder (`<Destination>.staging`) on the same volume; the live folder is swapped by atomic rename, never edited in place.
- **The swap must be a same-volume atomic rename.** If the staging folder and the destination are on different volumes, `Rename-Item` fails; the script must detect this and refuse rather than fall back to a copy. The existing script stages as `"$Destination.staging"` in the destination's parent precisely for this reason.
- **Publishing happens from the release script, never from the loader.** The loader reads the share and copies to the local install path; it never writes to the share. The loader stores and compares UNC paths.
- **The legacy copy is removed only after the new local version is verified and active.** Removing it early strands a user whose new version failed to start. This ordering is the loader's, and this spec's verification (TASK-134) checks it is honoured.
- **Side-by-side during transition.** The two mutex names differ, so a floor machine can run the WinForms build and the WinUI 3 build concurrently. Under the chosen cutover (D16) the loader serves only one at a time; the preserved `.previous-<version>` folder is the rollback path.
- **The Microsoft Edge WebView2 Runtime is a machine prerequisite.** Both the print renderer (D19) and the help topic host (D20) depend on it. It is present on Windows 11 and on Windows 10 machines that have Edge; it is **not** guaranteed on a stripped-down floor image. If absent, the application must still start, must show a plain message where the HTML surface would be, and must not crash. The runtime is a prerequisite of the machine, not of the payload — it is not bundled into the self-contained publish and must be verified per machine before cutover.

### State and persistence introduced

This spec writes the publish output (the payload folder and its manifest) to a local staging folder and, when `-Destination` is supplied, to the shared store. It creates no user-data folder, opens no log, and touches no database row. All application state — user settings, DGV layouts, shortcut overrides — continues to live where `001`–`010` placed it; the loader's local install is the only new location, and it is written by the loader, not by this spec.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Which cutover option is chosen** | **Resolved 2026-11-02** — option (a): same executable name, wholesale folder swap on completion. See §3.1. |
| 2 | Whether the loader catalogue supports a **second entry** for the same application (option (b) in `plan.md` §2.5). | **Moot** — option (b) was rejected (D16). Kept only as a record. |
| 3 | Whether the staging folder and the live destination are **on the same volume**, so the swap is a true atomic rename. | (to confirm) at publish time; the script must detect and refuse a cross-volume swap. |
| 4 | **Microsoft Edge WebView2 Runtime present on every floor machine.** Required by the print renderer and the help host; not bundled in the payload. | (to confirm) per machine before cutover. Add to the release checklist and to TASK-136's verification.
| 4 | The **maintenance window** for the wholesale folder swap. | **Resolved (D26)** — the application owner performs the swap on a weekend once the WinUI 3 build is ready. No formal window is scheduled; the release preconditions in TASK-136 stand in for one. |
| 5 | **Rollback posture** — whether the WinForms publish script and its share folder remain live after cutover as a rollback path, or are retired. | (to confirm). |
| 6 | **Who runs the release** | **Resolved (D26)** — the application owner runs it, on a weekend, once the WinUI 3 build is ready. The share credentials are the owner's; no build-machine automation is required. |

### Assumptions

1. `MTM_WIP.csproj` already carries `<AssemblyName>MTM_Inventory_Application</AssemblyName>` and `Directory.Build.props` carries the single `<Version>` — both delivered by `001`.
2. The shared store path is reachable from the machine that runs the publish script.
3. The database and the Visual ERP are frozen; nothing in this spec reads or writes them.
4. The loader catalogue entry `id: "wip"` is fixed for v1 and is not changed by this spec.
5. `MTM_WIP.Tests`, the forbidden-API gate (`001`) and the per-DAO integration tests (`002`) are green before deployment tasks run.
