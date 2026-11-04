# Migration Task List (Execution Plan)

**Spec**: `011-deployment-and-cutover`
**Range**: TASK-130 … TASK-137
**Definition of done for every task**: its `Verify` line passes, and the full verification suite (TASK-136) is green. TASK-137 is the pack-close gate: nothing in any spec is considered done until its `## Pack close` summary is written.

> **Ordering is load-bearing.** The script (TASK-130 … TASK-133) is proven before the loader contract and cutover are exercised (TASK-134 … TASK-135), and the pack is only signed off after every verification gate and the whole-pack parity walk pass (TASK-136 … TASK-137).

---

## Phase 1 — Publish script and manifest

- [ ] **TASK-130** — Adapt `scripts/Publish-MtmWipPayload.ps1` to publish the WinUI 3 project: read `<Version>` from `Directory.Build.props`, reduce to 3-part semver, run the exact publish command from [`plan.md`](./plan.md) §1.2 against `MTM_WIP/MTM_WIP.csproj`, assert the staging folder contains `MTM_Inventory_Application.exe`, and write `build-manifest.json` last (schemaVersion 1, fields per [`plan.md`](./plan.md) §1.3, sorted forward-slash paths, lowercase-hex sha256). Keep `-Destination`, `-Version`, `-Force`, `-FullVerify` parameters and the `<Destination>.previous` rename-and-restore path.
  - **Verify**: a local publish (no `-Destination`) produces `artifacts/payload/win-x64/MTM_Inventory_Application.exe` and `build-manifest.json`, and `manifest.version` equals the `Directory.Build.props` version; a supplied `-Version` that disagrees with `Directory.Build.props` is refused.

- [ ] **TASK-131** — Enforce the loader-contract preconditions in the script: refuse an empty staging folder, refuse a folder without `MTM_Inventory_Application.exe`, and refuse a manifest whose `executable` differs from the catalogue name.
  - **Verify**: publishing an empty folder or a folder without the executable aborts before any swap; a manifest edited to name a different executable fails validation.

- [ ] **TASK-132** — Implement and verify the version-reuse guard and stale-file reporting: refuse to publish a `version` whose published `MTM_Inventory_Application.exe` hash differs from the incoming one unless `-Force`; report (never silently delete) any destination file absent from the incoming manifest.
  - **Verify**: republishing the same version with a changed binary is refused; the same command with `-Force` succeeds and the bypass is written to the output; a planted stale file at the destination is listed in the output, not silently removed.

- [ ] **TASK-133** — Implement and verify the staged-swap publish sequence exactly as numbered in [`plan.md`](./plan.md) §2.3: stage beside the destination on the same volume, verify file-by-file against the manifest, swap by atomic rename, delete the previous copy only after the swap, write the manifest **last**; refuse a cross-volume swap.
  - **Verify**: an interrupted copy (kill the script between step 5 and step 8) leaves the previous version installed and launchable, and the destination has either the old manifest or none — never a manifest describing a half-copied folder.

---

## Phase 2 — Loader contract and cutover

- [ ] **TASK-134** — Verify the loader contract end to end against the shared store: catalogue entry `id: "wip"`, source folder `MTM WIP Application`, executable `MTM_Inventory_Application.exe`; the loader refuses to update a folder without `build-manifest.json`; it copies to `%LOCALAPPDATA%\MTM Applications\MTM Loader\wip\<version>\`; it stores/compares UNC paths and never writes to the share; and the legacy copy under `%USERPROFILE%\Documents\MTM Applications\` is removed only after the new local version is verified and active.
  - **Verify**: the loader launches the published WinUI 3 payload from its local install; a folder with no manifest is refused; the legacy copy remains present immediately after install and disappears only after the new version has launched once.

- [ ] **TASK-135** — Implement the **decided** cutover ([`plan.md`](./plan.md) §2.5, option (a): same executable name, wholesale folder swap). Implement the seven-step swap: stage to a same-volume sibling, verify staging against the manifest, rename the live folder to `<Destination>.previous-<version>`, rename staging into place, write the manifest last, re-verify the destination, and retain `.previous-<version>` until a floor machine has started the new version. Confirm the publish script never overwrites a live folder the loader may be copying, and that the two single-instance mutexes (`Global\MTM_WIP_WinUI3_SingleInstance` vs `Global\MTM_WIP_Application_Winforms_SingleInstance`) remain distinct.
  - **Verify**: a dry run against a staging destination produces `.previous-*` plus the new folder with a matching manifest. Interrupting step 4 leaves the live folder launchable and its manifest intact. Deleting `.previous-*` by hand is the only way to lose the rollback, and the script prints its path on success.
  - **Verify**: per the chosen option, the WinForms build and the WinUI 3 build can each launch concurrently on one machine with no mutex conflict, and a publish during a loader read leaves the loader serving a complete payload (old or new, never mixed).

---

## Phase 3 — Verification and pack close

- [ ] **TASK-136** — Run the full verification suite and record every result in this file. Required gates: `dotnet build MTM_WIP.sln -c Debug` (zero warnings introduced); `dotnet test MTM_WIP.Tests`; a `dotnet build` of the WinForms project to prove it is unaffected; the forbidden-API gate from `001` (zero hits for `System.Windows.Forms`, `MessageBox`, `MySql.Data`, `BackgroundWorker`, `Colors.`, literal `#RRGGBB` in XAML); the per-DAO integration tests from `002`; a **Microsoft Edge WebView2 Runtime presence check on every target machine**; a check that **WER `LocalDumps` is configured for `MTM_Inventory_Application.exe`** and that a deliberate crash produces a dump (D55); a check that **`%PROGRAMDATA%\MTM_WIP\appsettings.local.json` exists** wherever the committed defaults are insufficient (D51); and a manual smoke of the touched screens in Light and Dark at 100 % and 150 % scaling.
  - **Verify**: every gate is recorded with its command and its result (not a bare claim). A machine without the WebView2 Runtime still starts the app, shows the plain "HTML surface unavailable" message for print preview and help, and does not crash. A machine without `LocalDumps` configured is reported as an unfinished prerequisite, not as a passing gate. Any failure is listed with an owner rather than waived.

- [ ] **TASK-136a** — Perform and record the **owner walkthrough parity gate** (`000-pre-foundation` D60). Walk every screen in `005`–`009`, exercising each primary action, and record a dated artefact naming the screen, what was exercised, and the outcome. This is the only parity evidence that exists: there is no CI (D31), no pilot, and no second reviewer.
  - **Verify**: the record lists every screen from the pack's §2 mapping tables with a per-screen outcome; no screen is marked "not checked"; every defect found is either fixed or listed with an owner and an explicit accept/reject decision. **A walkthrough record with an unexercised screen does not satisfy this gate.**
  - **Verify**: every gate passes; any failure is listed below with an owner and a blocking reference — none is waived silently.

- [ ] **TASK-137** — **Downstream validation: whole-pack close.** This is the last spec, so it validates the entire pack. Re-read [`../00-index.md`](../00-index.md), [`../constitution.md`](../constitution.md), and every spec folder's `spec.md` (`001`–`010`); confirm the decision register (D1–D15) and the open-questions list (Q1–Q8) still match what was actually built; confirm no task in any spec is left unverified; and run the parity walk — every row of every §2 mapping table in every spec is either implemented or filed as a defect with a reference. Then append the `## Drift log` and `## Pack close` summary below.
  - **Verify**: a written `## Pack close` summary exists below covering decisions, open questions, the parity walk, and the verification results; every §2 row across `001`–`010` is accounted for as *implemented* or *defect (reference)*.

---

## Drift log

Filled in by TASK-137. One row per pack artefact checked; the result column is *confirmed* or *amended*.

| Checked artefact | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `../00-index.md` | Decision register D1–D15 matches what was built | *(pending)* | — |
| `../00-index.md` | Open questions Q1–Q8 still accurate | *(pending)* | — |
| `../constitution.md` | §1.2 packaging (unpackaged, self-contained, manifest last, version never reused) | *(pending)* | — |
| `../constitution.md` | §3.4 preserved contracts (manifest schema, executable name, single-instance) | *(pending)* | — |
| `../constitution.md` | §4 verification gates all run and green | *(pending)* | — |
| `001` | `AssemblyName`, single `<Version>`, asset layout match this spec's publish inputs | *(pending)* | — |
| `001` | Forbidden-API gate has zero hits | *(pending)* | — |
| `002` | Per-DAO integration tests green | *(pending)* | — |
| `003`–`010` | No task left unverified | *(pending)* | — |
| `003`–`010` | Parity walk: every §2 mapping-table row implemented or filed as a defect | *(pending)* | — |
| All | `dotnet build MTM_WIP.sln -c Debug` + `dotnet test MTM_WIP.Tests` + WinForms build | *(pending)* | — |
| All | Smoke: Light/Dark at 100 %/150 % | *(pending)* | — |

---

## Pack close

Filled in by TASK-137. Written once, after every row of the Drift log above is *confirmed* or *amended*.

| Item | Result |
| :--- | :--- |
| Decision register (D1–D15) | *(pending)* |
| Open questions (Q1–Q8) | *(pending)* |
| Verification suite | *(pending)* |
| Parity walk (all §2 tables) | *(pending)* |
| Defects filed (reference each) | *(pending)* |
| Cutover option chosen (plan §2.5) | *(pending)* |
