# Migration Task List (Execution Plan)

**Spec**: `007-settings-and-administration`
**Range**: TASK-097 … TASK-108
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** The navigation shell and role matrix (TASK-097) gate every page. The master-data page pattern (TASK-098) is proven before any entity is wired (TASK-099–100). The atomic user-creation sequence (TASK-101) is proven before Edit/Remove reuse the page (TASK-102). No page may call a DAO directly; all calls go through the `002` interfaces.

---

## Phase 1 — Module scaffold & navigation

- [ ] **TASK-097** — Create the `MTM_WIP.Module_Settings` folder structure (`Views/`, `ViewModels/`, `Models/`, `Contracts/`, `Services/`, `Enums/`) and implement `View_SettingsPage` + `ViewModel_SettingsPage`: a `NavigationView` pane + `Frame`, the ten category keys, Home as the default, deep-link handling, and the role → category filter from `ICurrentUserService` (spec §3.1, plan §2.3). Register the module's services via `AddSettingsServices()`. Route resolution must be container-based and forbid hidden categories by returning Home.
  - **Verify**: a ViewModel test asserts Home is default when no key is supplied; asserts role 2 resolves only `Home`, `Appearance`, `About`; asserts role 3 resolves `Database` and `UserManagement` to Home; `dotnet build` succeeds with no new warnings.

## Phase 2 — Master data

- [ ] **TASK-098** — Implement the shared master-data pattern: `Model_MasterDataField`, `Model_MasterDataDescriptor`, `IMasterDataFacade`, and `ViewModel_MasterDataPage` with the Add / Edit / Remove three-section flow, entity `ListView`, blank-check + `*_Exists` pre-check validation, and the optional `OnWriteCompletedAsync` hook (plan §2.2). Wire the four descriptors (Parts, Operations, Locations, Item Types) with their field lists and facades.
  - **Verify**: a ViewModel unit test drives the Add happy path and the blank-field failure path against a fake `IMasterDataFacade`; a second test asserts the Edit section is hidden for role 3 (Normal). No ViewModel references a concrete DAO.

- [ ] **TASK-099** — Implement the Parts facade over `IDao_Part`: list via `md_part_ids_Get_All`, existence via `md_part_ids_Get_ByItemNumber`, add/update/delete via `md_part_ids_Update_Part` / `md_part_ids_Delete_ByItemNumber`, item types from `IMasterDataCache`, and the `RequiresColorCode` hook that calls `IMasterDataCache.RefreshAsync` on flag change (also `md_part_ids_UpdateColorCodeFlag`). Customer and Description are written as empty strings.
  - **Verify**: three integration tests against `mtm_wip_application_winforms_test` (add + rollback, duplicate-existence pre-check, renumber re-checks existence). A unit test asserts the hook fires only when the flag actually changed.

- [ ] **TASK-100** — Implement the Locations, Operations and Item Types facades over `IDao_Location`, `IDao_Operation`, `IDao_ItemType` (procedures per plan §3.1), each with the existence pre-check and the rename/update semantics from spec §3.3. Confirm no colour-code page exists anywhere in the module.
  - **Verify**: one integration test per entity (add inside a rolled-back transaction + existence pre-check on a duplicate). `git grep` across `MTM_WIP.Module_Settings` finds no `ColorCode` management page and no direct DAO reference outside the facades.

## Phase 3 — User management

- [ ] **TASK-101** — Implement the Add-user section of `View_UserManagement` + `ViewModel_UserManagement`: fields, shift combo, role radios (Normal default), password visibility, uppercased username, duplicate check via `IDao_User.UserExistsAsync`, and the **atomic** create — `usr_users_Add_User` → `usr_users_Get_ByUser` → `sys_user_roles_Add` in one caller-supplied transaction, rolling back the user when the role add fails (spec §3.2, plan §2.4).
  - **Verify**: three integration tests — success creates a user **with** a role; duplicate returns the "User already exists" failure; a forced role-add failure leaves no user row (rollback verified). A unit test asserts validation rejects blank first/last/user/PIN and no-shift.

- [ ] **TASK-102** — Implement the Edit and Remove sections: Edit loads `usr_users_Get_ByUser` + `sys_user_roles_Get_ById` **without populating a PIN**, then `usr_users_Update_User` + `sys_user_roles_Update`; Remove confirms via `IDialogService` then `IDao_User.DeleteUserAsync` (→ `usr_users_Delete_User`, which cascades). Follow the confirmed PIN rule from spec §4 item 2.
  - **Verify**: an integration test edits a user's shift/role and asserts the PIN column is untouched; a second removes a user and asserts the `sys_user_roles` row is gone (cascade). A unit test asserts the loaded model never exposes a PIN value.

## Phase 4 — Shortcuts

- [ ] **TASK-103** — Implement `View_Shortcuts` + `ViewModel_Shortcuts`: nine collapsible cards built dynamically (Action / Shortcut / "…"), an awaited `LoadAsync` (no fire-and-forget), and the capture dialog via `IDialogService` that rejects bare modifier keys, rejects duplicates and reserved `Alt+0..9`, and Reset with confirmation — all through `IShortcutService` (spec §3.4). No direct DAO access and no cache mutation in this module.
  - **Verify**: a unit test with a fake `IShortcutService` asserts nine categories render, a bare-modifier chord is rejected, an `Alt+1` chord is rejected, and Reset is gated on confirmation. `dotnet test` green.

## Phase 5 — Database settings

- [ ] **TASK-104** — Implement `View_Database` + `ViewModel_Database` load and Test Connection: bind `WipServerAddress`, `WipServerPort`, `WIPDatabase` from `usr_settings_Get` with fallback defaults; validate required fields and port 1–65535; Test Connection probes the proposed connection and reports latency or a plain failure reason.
  - **Verify**: a unit test binds a JSON fragment and asserts the three values; asserts port `0` and `70000` are rejected. An integration test runs Test Connection against the test server and returns success.

- [ ] **TASK-105** — Implement `Service_SettingsDatabase` save as a **single write**: read the current `SettingsJson` once, replace the three keys, write once via `usr_settings_SaveFullJson`, then raise **one** restart request to the `004` contract when the database changed (no `Application.Restart`, no three parallel merges). Add Reset restoring the defaults.
  - **Verify**: an integration test saves the three values and reads them back in one row; a unit test asserts the service performs exactly one write and at most one restart request per save. `git grep` finds no `Application.Restart` and no three-way `SetThemeJson` call.

## Phase 6 — Appearance & About

- [ ] **TASK-106** — Implement `View_Appearance` + `ViewModel_Appearance`: Light / Dark / System selector bound to `IThemeService` (persisting the new `usr_settings` mode key) and the EnableAnimations / AutoExpandPanels / ShowTotalSummaryPanel toggles bound to `IUserPreferences`. No palette editor, no theme-name list, no font-size slider.
  - **Verify**: a unit test asserts mode changes persist immediately through `IThemeService`/`IUserPreferences` fakes and that the old `Theme_Name`/`Theme_FontSize` keys are neither read nor written. A manual smoke check shows both Light and Dark render correctly.

- [ ] **TASK-107** — Implement `View_About` + `ViewModel_About`: assembly version and copyright, and native release notes parsed from `Assets/ReleaseNotes/RELEASE_NOTES.json` into `Model_ReleaseNoteEntry` items — no WebView2, no HTML template, no watermark substitution (spec §3.7).
  - **Verify**: a unit test parses the shipped `RELEASE_NOTES.json` and asserts at least one version entry with notes; `git grep` finds no `WebView2` and no `release-notes.html` reference in this module.

## Phase 7 — Downstream consistency validation

- [ ] **TASK-108** — **Downstream validation: `008-visual-analytics` (and sweep of `009`–`011`).** Re-read [`../008-visual-analytics/spec.md`](../008-visual-analytics/spec.md), [`../008-visual-analytics/plan.md`](../008-visual-analytics/plan.md) and [`../008-visual-analytics/tasks.md`](../008-visual-analytics/tasks.md), and confirm every assumption this spec made about what comes next still holds. Then sweep `009`–`011` for anything that depends on this spec's output.

  Record the result in the **Drift log** below. Specifically confirm:
  1. `008` reads `ICurrentUserService`, `IMasterDataCache` and the shared grid component (`003`), not any Settings page, DAO or route — so this module's pages and routes can change without touching `008`.
  2. `008`'s Visual tools use `IDao_System` / `IDao_VisualAnalytics` (`002`), and no master-data CRUD delivered here is consumed downstream by `008`.
  3. The role ids `1`/`2`/`3`/`4` and the `Normal`-restricted matrix assumed by `008`'s role checks match `ICurrentUserService` exactly — a shared contract, not a Settings-only table.
  4. No `008` navigation key collides with a Settings category key (`Home`, `Database`, `UserManagement`, `PartNumbers`, `Operations`, `Locations`, `ItemTypes`, `Appearance`, `Shortcuts`, `About`).
  5. The database-target restart raised by `007` flows through `004`; `008`'s long-running ERP queries tolerate the restart request (or `004` defers it), and `008` assumes no second `Application.Restart`.
  6. `Assets/ReleaseNotes/RELEASE_NOTES.json` (read by `007`) and `Assets/default_shortcuts.json` (seeded by `003`) are the paths `008`/`009` expect — no asset-path drift.
  7. `009` (help) and `010` (developer tools) do not assume a colour-code settings page, a theme palette editor, or a WebView2 release-notes surface exists.

  If any item is false, fix **this** spec (or amend the downstream spec) before proceeding. Do not carry a known mismatch forward.

  - **Verify**: a written entry exists in the Drift log below for each of the seven items, each marked *confirmed* or *amended*, and `dotnet test MTM_WIP.Tests` is green.

---

## Drift log

Filled in by TASK-108. One row per downstream spec checked.

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `008-visual-analytics` | Reads `003` contracts, not Settings pages/routes | *(pending)* | — |
| `008-visual-analytics` | No master-data CRUD consumed downstream | *(pending)* | — |
| `008-visual-analytics` | Role ids and Normal matrix match `ICurrentUserService` | *(pending)* | — |
| `008-visual-analytics` | No navigation-key collision with Settings categories | *(pending)* | — |
| `008-visual-analytics` | Restart request flows through `004`; ERP queries tolerate it | *(pending)* | — |
| `008`–`009` | Release-notes / shortcut asset paths match consumers | *(pending)* | — |
| `009`–`010` | No colour-code page, palette editor, or WebView2 release notes assumed | *(pending)* | — |
