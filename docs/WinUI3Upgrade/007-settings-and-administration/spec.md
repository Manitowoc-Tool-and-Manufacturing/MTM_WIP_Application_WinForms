# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `007-settings-and-administration`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`, `003-crosscutting-services-and-theming`, `004-shell-and-startup`
**Unblocks**: none directly — this is a leaf screen module. `008` shares contracts with it but reads `002`/`003`, not `007`.
**Tasks**: TASK-097 … TASK-108
**Governing rules**: [`../constitution.md`](../constitution.md) §2 Rules 1, 2, 5, 6, 9, 10, 11, 12

---

## 1. Executive Summary & Scope

This spec rebuilds the WinForms `SettingsForm` and its ten categories as `MTM_WIP.Module_Settings`, a WinUI 3 module inside the single-window shell. It covers category navigation, user management, the four master-data pages (Parts, Operations, Locations, Item Types), the Shortcuts page, the Database page, the Appearance page, and the About / release-notes surface.

It is a **UI and architecture** port, not a data migration. The database stays frozen: every procedure named below already exists and is already called by the WinForms DAOs; none is renamed, none gains a parameter. The module consumes the contracts `002` and `003` already define — `IDao_User`, `IDao_Part`, `IDao_Location`, `IDao_Operation`, `IDao_ItemType`, `IDao_ColorCode`, `IDao_Shortcuts`, `IDao_System`, `Model_Result<T>`; and `IDialogService`, `IErrorService`, `IDispatcherService`, `ICurrentUserService`, `IMasterDataCache`, `IUserPreferences`, `IShortcutService`, `IThemeService`.

### In scope

| Group | Delivered |
| :--- | :--- |
| Navigation | One `SettingsPage` with a `NavigationView` pane + `Frame`; Home default; deep-linkable categories |
| Privileges | Role → category availability, enforced at the route level, not by hiding buttons |
| User management | Add (atomic user + role), Edit (no clear-text PIN), Remove (cascade) |
| Master data | One reusable page pattern parameterised for Parts, Operations, Locations, Item Types |
| Shortcuts | Nine-category page, capture dialog, validation, reset — over `003`'s `IShortcutService` |
| Database | Server / port / database page, Test Connection, **single-write** save, restart-once |
| Appearance | Light / Dark / System mode selector + three option toggles |
| About | Version, copyright, native release notes from `RELEASE_NOTES.json` |

### Out of scope

- Theming internals. `003` owns the theme-engine deletion and `IThemeService`; this spec only renders the mode selector and the three user options. The palette editor, theme-name list and font-size slider are removed for the reasons `003` §3.2 records — this spec references, not re-argues, them.
- Help rendering — `009`.
- Developer Tools, Database Maintenance, Error Reports — `010`.
- The shell, navigation host and composition root — `004`.
- The inventory entry screen's colour-code consumption — `005`.

### Success condition

A user with Administrator rights can navigate all ten categories, create a user that is guaranteed to have a role, manage every master-data entity, edit shortcuts, change and test database settings with a single restart, and read release notes — all through injected interfaces, all unit-tested without a UI thread.

---

## 2. Legacy Feature Assessment & UI Mapping Table

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `SettingsForm` (`ThemedForm`) with left `TreeView` (`SettingsForm_TreeView_Category`) | `View_SettingsPage.xaml` — `NavigationView` (pane) + `Frame` | One page, one frame. Category selection swaps page content. Home is the default. No node is auto-selected at startup beyond Home. |
| `Control_SettingsHome` (category tiles raising `NavigationRequested`) | Home category content (`View_SettingsHome.xaml`) | The pane already lists categories; Home's content is a tile grid whose tiles raise the same navigation command. Deep-linking a category does not first require a Home visit. |
| `ShowPanel(string)` switching panels from a dictionary | Route registration: category key → `Page`/`ViewModel` type | Same string-key concept, resolved from the container, not a `Panel` dictionary. |
| `ReloadComboBoxDataAsync()` called **by reflection** on panel select | `IInitializePage` / navigation-aware load, called **explicitly** | Reflection is deleted. The page's own activation hook awaits the load; the framework invokes it by type, not by name. |
| Simulated 300 ms progress bar before showing a panel | *(deleted)* | No `Task.Delay` fake progress. The page loads asynchronously with its own busy indicator where a load actually exists. |
| `HasChanges` flag set by child change events; `UpdateStatus` status text | Per-page dirty tracking + a status-text service | The status bar (owned by `004`'s shell) renders the active page's status string. |
| Legacy designer panels (Edit/Remove User, Operation, Location, ItemType) still present with `Visible = false` | *(deleted)* | Folded into sections of their pages; no invisible designer surfaces are carried over. |
| Role-based category hiding | `ICurrentUserService` + route filter | Admin and Developer see all. Normal sees no Database and no User Management and may only **add** master data. Read-Only sees Appearance and About only. A hidden category has no navigable route, not merely a hidden button. |
| `Control_User_Management` (3-tile home Add/Edit/Remove + `SwitchToMode`) | `View_UserManagement.xaml` — three sections or a mode selector | Add / Edit / Remove as three sections of one page. |
| `Control_Add_User` | Add-user section | First name, last name, user name, shift combo, PIN, visual user name, visual password, show/hide password, "Visual access" checkbox, role radios. See §3.2. |
| `Control_Edit_User` | Edit-user section | User combo from `usr_users_Get_All`; selection loads the record **without the PIN**; role from `sys_user_roles_Get_ById`. A blank PIN keeps the existing value. See §3.2. |
| `Control_Remove_User` | Remove-user section | Combo + role label, confirmation, `usr_users_Delete_User` (cascades roles and settings). |
| `Control_PartIDManagement` + `Dao_Part` | `View_MasterDataPage.xaml` parameterised for Parts | Add requires PartNumber + ItemType; `RequiresColorCode` is the only extra field actually written — Customer and Description are written as empty strings. See §3.3. |
| Locations (`Dao_Location`) | same page type, Locations descriptor | Location + Building. See §3.3. |
| Operations (`Dao_Operation`) | same page type, Operations descriptor | Number only. See §3.3. |
| Item Types (`Dao_ItemType`) | same page type, Item Types descriptor | Name only. See §3.3. |
| Colour Codes (`Dao_ColorCode`) — **no settings UI today** | *(not a page)* | `md_color_codes_GetAll` / `md_color_codes_Add` exist and are consumed by the inventory entry screen. v1 must **not** invent a colour-code management page. |
| `Control_Shortcuts` (9 collapsible cards) + `Form_ShortcutEdit` capture dialog | `View_Shortcuts.xaml` + capture dialog | Nine categories (Inventory, Remove, Transfer, Advanced Inventory, Advanced Remove, Quick Buttons, Navigation, Transactions, General). Rows built dynamically. See §3.4. |
| `Control_Database` (server / port / database + test + reset) | `View_Database.xaml` | Load from `usr_settings_Get` JSON keys with fallback defaults; validate; Test Connection; save as **one** write. See §3.5. |
| `Control_Theme` (theme combo, animations, auto-expand, summary panel, save/preview) | `View_Appearance.xaml` | Three-way mode selector + three toggles. Palette editor and font-size are removed (`003`). See §3.6. |
| About (assembly version + copyright) + `SettingsForm_ViewReleaseNotesHTML` (WebView2, HTML template, `{{RELEASE_NOTES_JSON}}` / `{{WATERMARK_BASE64}}` substitution) | `View_About.xaml` + native release-notes surface | Renders from the same `RELEASE_NOTES.json` with **no WebView2 and no HTML template**. See §3.7. |

---

## 3. Behavioral Edge Cases & State Constraints

> **Screen constraints** (`000-pre-foundation` D58, D59): every screen must be fully usable at **1366×768 with 100 % DPI**, and every interactive control carries `AutomationProperties.Name` matching its visible label plus a stable `AutomationId`. High contrast and text scaling are out of scope by decision.

### 3.1 Navigation and role → category availability

Categories and their legacy keys: **Home, Database, User Management, Part Numbers, Operations, Locations, Item Types, Theme, Shortcuts, About**. The legacy "Theme" category is renamed **Appearance** in the new client; the rename is the only label change.

| Role id | Role | Database | User Management | Master data | Appearance | Shortcuts | About |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | Administrator | ✓ | ✓ | add + edit + remove | ✓ | ✓ | ✓ |
| 2 | Read-Only | — | — | — | ✓ | — | ✓ |
| 3 | Normal | — | — | **add only** | ✓ | ✓ | ✓ |
| 4 | Developer | ✓ | ✓ | add + edit + remove | ✓ | ✓ | ✓ |

Constraints:

- Home and the pane are always present for every role; the pane simply omits categories the role may not reach.
- Hiding is **route-level**: requesting a forbidden category key navigates to Home (or no-ops) and never renders the page. Master-data **add-only** for Normal is enforced at the command level — the edit/remove affordances are absent, not merely disabled.
- Role checks happen when an action runs and when a route is resolved, not only when the pane is built (mirrors `003`'s `ICurrentUserService` rule).

### 3.2 User management — the three operations, exactly

**Add** (`Control_Add_User` behaviour):

- Fields: FirstName, LastName, UserName, Shift combo (an index > 0 must be selected), Pin, VisualUserName, VisualPassword, a show/hide passwords toggle, a "Visual access" checkbox, and role radios Administrator / Read-Only / Developer with **Normal as the default**.
- Validation is exactly: first, last, user and PIN non-blank, and a shift selected. No length, format or entropy rule — none exists today and none is invented.
- The username is **uppercased** before the duplicate check and before save.
- Duplicate check: `IDao_User.UserExistsAsync` (→ `usr_users_Get_ByUser`); an existing user produces "User already exists".
- PIN input uses a `PasswordBox`; the legacy `UseSystemPasswordChar` plus no-spaces `KeyPress` filter becomes the `PasswordBox` default plus a no-space input rule on the ViewModel.

**The defect fixed here.** Today `Save` runs three independent calls: `usr_users_Add_User` (with `User`, `FullName` = "First Last", `Shift`, `VitsUser = false`, `Pin`, `LastShownVersion`, `HideChangeLog = "false"`, `ThemeName = "Default"`, `ThemeFontSize = 9`, `VisualUserName`, `VisualPassword`, and default `WipServerAddress` / `WipServerPort` / `WIPDatabase`), **then** `usr_users_Get_ByUser` to read back the new ID, **then** `sys_user_roles_Add(UserID, RoleID, AssignedBy = Environment.UserName)`. This is not atomic: if the role add fails, the user exists with no role. The new client executes the create-then-role sequence inside **one database transaction** through `002`'s caller-supplied transaction path, and rolls the user back when the role add fails. The user is never observable without a role.

**Edit** (`Control_Edit_User` behaviour):

- User combo populated from `usr_users_Get_All`; selecting a user loads `usr_users_Get_ByUser`.
- **The stored PIN is not loaded or displayed.** The legacy form fills the PIN box in clear text from the database; the new client shows an empty `PasswordBox`. **A blank PIN on edit means "keep the existing PIN unchanged"** — an admin changing someone's shift must not silently reset their PIN. When the admin does type a new PIN, it is hashed per D17 before it is written.
- Role loads from `sys_user_roles_Get_ById`. Save calls `usr_users_Update_User` then `sys_user_roles_Update` — the two-write ordering is kept, but both belong to one logical operation and run in one transaction where the helper's transaction path permits.

**Remove** (`Control_Remove_User` behaviour):

- Combo + role label, a confirmation prompt, then `IDao_User.DeleteUserAsync` (→ `usr_users_Delete_User`). The procedure cascades `sys_user_roles` and `usr_settings`; the client adds no second delete.

### 3.3 Master data — one pattern, four entities

All four pages share the Add / Edit / Remove three-card flow (`ShowCard` / `ShowHome` in legacy terms). Validation everywhere is **blank-check plus an `*_Exists` pre-check**; no regex, no length rules — none exist in these paths and none are invented.

| Entity | Fields written | Existence pre-check | Read | Rename / update | Delete |
| :--- | :--- | :--- | :--- | :--- | :--- |
| Parts | PartNumber, ItemType, RequiresColorCode; Customer and Description are written as **empty strings** | `PartExistsAsync` (→ `md_part_ids_Get_ByItemNumber`) | `md_part_ids_Get_All`, `md_part_ids_GetAllColorCodeFlagged`, `md_item_types_GetDistinct` | `md_part_ids_Update_Part` (renumbering re-checks existence) | `md_part_ids_Delete_ByItemNumber` |
| Locations | Location, Building | `md_locations_Exists_ByLocation` | `md_locations_Get_All` | `md_locations_Update_Location` (old → new + building) | `md_locations_Delete_ByLocation` |
| Operations | Operation | `md_operation_numbers_Exists_ByOperation` | `md_operation_numbers_Get_All` | `md_operation_numbers_Update_Operation` (Operation, NewOperation) | `md_operation_numbers_Delete_ByOperation` |
| Item Types | ItemType | `md_item_types_Exists_ByType` | `md_item_types_Get_All` | `md_item_types_Update_ItemType` (ID, ItemType) | `md_item_types_Delete_ByType` |

Constraints:

- Changing the Parts `RequiresColorCode` flag triggers the colour-code cache refresh — today `ReloadColorCodePartsAsync()`; in the new client `IMasterDataCache.RefreshAsync` (the single cache `003` owns). `md_part_ids_UpdateColorCodeFlag` remains the write.
- **Colour Codes are not a page.** The new client renders no management UI for them; the cache is read-only as far as Settings is concerned.

### 3.4 Shortcuts

- Two tables, unchanged: `sys_shortcuts` (ShortcutName, ShortcutKeys, Description, Category) and `usr_user_shortcuts` (UserName, ShortcutName, CustomKeys, IsCustom).
- DAO operations: `sys_shortcuts_GetAll`, `usr_user_shortcuts_GetByUser` (returns EffectiveKeys, IsCustom), `sys_shortcuts_Upsert`, `usr_user_shortcuts_Upsert`, `usr_user_shortcuts_Reset`.
- Defaults are seeded by `IShortcutService.EnsureDefaultsExistAsync` reading `Assets/default_shortcuts.json` and upserting **only changed/missing rows** (this logic lives in `003`; the page consumes the service).
- UI: nine collapsible cards, rows built dynamically (Action / Shortcut / "…" button). The capture dialog stores the key chord, **ignores bare modifier keys**, displays Ctrl / Alt / Shift + key, and save rejects reserved `Alt+0..9` and duplicates. Reset confirms, then calls `usr_user_shortcuts_Reset`.
- Firing: key handlers call `IShortcutService.GetShortcutKey("Shortcut_…")` with compiled fallbacks — the precedence is override → system default → compiled default (`003`).

### 3.5 Database page — single write, single restart

- Load reads `usr_settings_Get` JSON keys `WipServerAddress`, `WipServerPort`, `WIPDatabase`, falling back to `172.16.1.104`, `3306`, `mtm_wip_application_winforms`.
- Validation: all fields required, port parses to 1–65535.
- **The defect fixed here.** Today `Save` issues **three parallel JSON merges** (`SetWipServerAddressAsync` / `SetWipServerPortAsync` / `SetDatabaseAsync`, each a `usr_settings_SetThemeJson` merge on the same row) — a race where the last write wins on a key it did not set. The new client reads the current JSON once, replaces the three keys in one object, and writes **once** via `usr_settings_SaveFullJson`.
- Test Connection builds a proposed connection string from the form values and reports latency or a plain failure reason, comparing against the active connection.
- Reset restores `172.16.1.104` / `3306` / `mtm_wip_application_winforms`.
- Changing the database requires a restart. Today `SettingsForm.OnFormClosing` releases the mutex and calls `Application.Restart()`. `Application.Restart` has no WinUI equivalent: the new client raises a **single** restart request consumed by `004` (release mutex, relaunch through the loader path), and never calls it three times. (Mechanism ownership: `004`; see §4 item 1.)

### 3.6 Appearance

- Today: theme-name combo (`Core_AppThemes.GetThemeNames()`), EnableAnimations, AutoExpandPanels, ShowTotalSummaryPanel, Save/Preview.
- New client: a three-way mode selector **Light / Dark / System** plus the same three option toggles.
- The mode persists as a **new** `usr_settings` JSON key; the old `Theme_Name` / `Theme_FontSize` keys are left in place and ignored (`003` §3.6).
- The palette editor, the theme-name list and the font-size slider are **removed** — the rationale is `003` §3.2 and is not re-argued here.
- No Save/Preview pair: the mode applies immediately and persists; a preview is meaningless when the only choice is the OS-rendered mode.

### 3.7 About and release notes

- Shows assembly version and copyright.
- "View Release Notes" reads `RELEASE_NOTES.json` from the app base directory (the legacy 3-levels-up fallback disappears with the single publish folder; `001` places the asset at `Assets/ReleaseNotes/RELEASE_NOTES.json`).
- Rendering is **native**: parse the JSON into a list of version entries rendered by a page. There is no `WebView2`, no `release-notes.html`, no `{{RELEASE_NOTES_JSON}}` / `{{WATERMARK_BASE64}}` substitution, and no `MTM.png` watermark injection.
- The legacy toggle to `version-comparison.html` **is in v1** (D24): both the current release and the comparison view render natively from `RELEASE_NOTES.json`.

### 3.8 Legacy defects explicitly fixed by this module

| Defect | Fix | Owner |
| :--- | :--- | :--- |
| Reflection call to `ReloadComboBoxDataAsync` | Explicit `IInitializePage` load by type | `007` |
| `async void` event handlers | `AsyncRelayCommand` only | `007` |
| Fire-and-forget `_ = LoadShortcuts()` | awaited load | `007` |
| `Task.Delay` fake 300 ms progress | deleted | `007` |
| Three concurrent JSON-merge writes to one `usr_settings` row | one `usr_settings_SaveFullJson` write | `007` |
| `Application.Restart` (no WinUI equivalent) | restart request to `004`, single | `007` / `004` |
| User created without a role on role-add failure | one transaction, rollback | `007` |
| PIN read back in clear text | never load/display PIN | `007` |
| `Service_Shortcut._shortcutCache` plain `Dictionary` mutated without locking | thread-safe cache in `IShortcutService` | `003` |

### 3.9 State introduced / eliminated

| Legacy state | Replacement | Lifetime |
| :--- | :--- | :--- |
| `SettingsForm` panels and their `Visible` flags | routes in the settings frame | per navigation |
| `HasChanges` per form | per-page dirty flag | per page |
| `Service_Shortcut._shortcutCache` | `IShortcutService` cache (thread-safe) | singleton (`003`) |
| `WipServerAddress` / `WipServerPort` / `WIPDatabase` statics | `IUserPreferences` / `DatabaseOptions.Wip` | singleton |

**Persistence touched**: `usr_settings` (appearance mode — new key; database target — same keys), `usr_user_shortcuts` / `sys_shortcuts` (unchanged), `usr_users` / `sys_user_roles` (unchanged), `md_*` (unchanged). No new table, column, procedure or file format.

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Restart mechanism ownership.** `Application.Restart` has no WinUI equivalent; the orderly-restart path belongs to `004`. Whether the restart relaunches via the MTM Loader or a plain process spawn is undecided. | **(to confirm)** with `004`. Until then, `007` raises a restart request against a `004` contract and unit-tests the request, not the relaunch. |
| 2 | **Edit-user PIN semantics** | **Resolved** — blank on edit means "keep the existing PIN unchanged"; a typed PIN is hashed per D17 before writing. The sign-in path in `004` already verifies both stored forms, so a kept legacy plaintext PIN keeps working and upgrades on that user's next sign-in. |
| 3 | Version-comparison view | **Resolved (D24)** — v1 keeps both the current release notes and the version-comparison view, rendered natively from `RELEASE_NOTES.json`. |
| 4 | **`004` route and restart contracts** | **Resolved** — `004` is written. It exposes `INavigationService.NavigateTo<TViewModel>()` over a route table keyed by ViewModel type → Page type, registered per module from that module's `AddXxxServices`. This spec's routes and its restart request follow that shape. |
| 5 | **Atomic user-creation via helper transaction.** `usr_users_Add_User` and `sys_user_roles_Add` are two procedures; whether the helper's caller-supplied transaction can wrap two procedure calls with output parameters is asserted by `002` TASK-016 but not yet exercised against these two procedures. | **(to confirm)** at TASK-101 integration test. |

### Assumptions

1. Every procedure below already declares `OUT p_Status INT` and `OUT p_ErrorMsg VARCHAR(500)` and returns `p_Status >= 0` on success.
2. `usr_settings_SaveFullJson` replaces the whole JSON document for the calling user, so a single write is sufficient for the three database keys.
3. `ICurrentUserService` exposes role ids `1` Administrator, `2` Read-Only, `3` Normal, `4` Developer, matching the legacy matrix.
4. `IMasterDataCache` (from `003`) holds item types for the Parts page's item-type field and is refreshed on colour-code-flag changes.
5. `Assets/default_shortcuts.json` is the same JSON the legacy `Resources/default_shortcuts.json` shipped; `003` seeds from it.
