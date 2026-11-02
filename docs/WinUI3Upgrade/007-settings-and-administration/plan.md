# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `007-settings-and-administration`
**Project**: `MTM_WIP.Module_Settings`
**Tasks**: TASK-097 … TASK-108
**Governing rules**: [`../constitution.md`](../constitution.md) §1, §2 Rules 1, 2, 5, 6, 9, 10, 11, 12, §3.3

---

## 1. Project Topology & Directory Mapping

`MTM_WIP.Module_Settings` already exists as an empty project (created by `001` TASK-005). This spec fills it. Reference graph per the constitution: the module references `Core`, `Shared` and `Data` only; it may not reference another `Module_*` or `Startup`.

### 1.1 Directory layout (create a folder only when it has content)

```
MTM_WIP.Module_Settings/
├── Views/
│   ├── View_SettingsPage.xaml            # NavigationView pane + Frame
│   ├── View_SettingsHome.xaml            # Home tile grid (default content)
│   ├── View_UserManagement.xaml          # Add / Edit / Remove sections
│   ├── View_MasterDataPage.xaml          # ONE page type, parameterised per entity
│   ├── View_Shortcuts.xaml               # 9 collapsible cards
│   ├── View_Database.xaml                # server / port / database + test
│   ├── View_Appearance.xaml              # mode selector + 3 toggles
│   └── View_About.xaml                   # version + release notes
├── ViewModels/
│   ├── ViewModel_SettingsPage.cs
│   ├── ViewModel_SettingsHome.cs
│   ├── ViewModel_UserManagement.cs
│   ├── ViewModel_MasterDataPage.cs       # parameterised by a descriptor
│   ├── ViewModel_Shortcuts.cs
│   ├── ViewModel_Database.cs
│   ├── ViewModel_Appearance.cs
│   └── ViewModel_About.cs
├── Models/
│   ├── Model_MasterDataField.cs          # field descriptor for the shared page
│   ├── Model_MasterDataDescriptor.cs     # entity descriptor (fields, DAO facade, hooks)
│   ├── Model_ShortcutItem.cs
│   └── Model_ReleaseNoteEntry.cs
├── Contracts/
│   ├── ISettingsNavigationService.cs     # category key → route resolution + role filter
│   └── IMasterDataFacade.cs              # the per-entity seam behind the shared page
├── Services/
│   ├── Service_SettingsNavigation.cs
│   └── Service_SettingsDatabase.cs       # single-write save + restart request
├── Enums/
│   └── Enum_SettingsCategory.cs
└── Converters/
    └── (created only when a converter is needed)
```

`ViewModels` never reference `Views`; `Views` reference their ViewModel in the same module. All `Services` are instance classes behind interfaces in `Contracts/`.

### 1.2 Route / page structure

The settings frame is a child of the shell's frame (`004`). Within it, `SettingsPage` owns a `NavigationView` pane whose `MenuItems` are generated from the role-filtered category list; selection navigates the inner `Frame`.

| Category key | Page type | ViewModel | Role filter |
| :--- | :--- | :--- | :--- |
| `Home` | `View_SettingsHome` | `ViewModel_SettingsHome` | all |
| `Database` | `View_Database` | `ViewModel_Database` | 1, 4 |
| `UserManagement` | `View_UserManagement` | `ViewModel_UserManagement` | 1, 4 |
| `PartNumbers` | `View_MasterDataPage` (Parts descriptor) | `ViewModel_MasterDataPage` | 1, 3, 4 |
| `Operations` | `View_MasterDataPage` (Operations descriptor) | `ViewModel_MasterDataPage` | 1, 3, 4 |
| `Locations` | `View_MasterDataPage` (Locations descriptor) | `ViewModel_MasterDataPage` | 1, 3, 4 |
| `ItemTypes` | `View_MasterDataPage` (Item Types descriptor) | `ViewModel_MasterDataPage` | 1, 3, 4 |
| `Appearance` | `View_Appearance` | `ViewModel_Appearance` | all |
| `Shortcuts` | `View_Shortcuts` | `ViewModel_Shortcuts` | 1, 3, 4 |
| `About` | `View_About` | `ViewModel_About` | all |

Notes:

- The route key is the category key; the page/ViewModel pair is resolved from the DI container, never `new`-ed in code-behind.
- `PartNumbers`, `Operations`, `Locations`, `ItemTypes` navigate to the **same page type** with a different descriptor passed as the navigation parameter. The descriptor chooses the DAO facade, the field list, and the post-save hook.
- A forbidden key resolves to `Home` and never renders the target page (spec §3.1).

### 1.3 Reference direction and what the module consumes

| Contract | From | Used by |
| :--- | :--- | :--- |
| `Model_Result<T>` | `Core/Models` (`002`) | every DAO call in this module |
| `IDao_User`, `IDao_Part`, `IDao_Location`, `IDao_Operation`, `IDao_ItemType`, `IDao_ColorCode`, `IDao_Shortcuts`, `IDao_System` | `Data/Contracts` (`002`) | user management, master data, shortcuts, database |
| `IDialogService`, `IErrorService`, `IDispatcherService` | `Core` (`003`) | all pages |
| `ICurrentUserService` | `Core` (`003`) | role filtering, role radios |
| `IMasterDataCache` | `Core` (`003`) | item-type combo, colour-code refresh |
| `IUserPreferences` | `Core` (`003`) | appearance toggles, database target keys |
| `IShortcutService` | `Core` (`003`) | shortcuts page |
| `IThemeService` | `Core` (`003`) | appearance mode |

The module must **not** consume `Model_Application_Variables`, `Core_AppThemes`, `Core_Themes`, `ThemedForm`, or any WinForms type — they do not exist in the new solution.

---

## 2. Component Migration Strategy

### 2.1 SettingsPage and navigation

`SettingsPage.xaml` contains only a `NavigationView` and a `Frame`; its code-behind holds `InitializeComponent`, the constructor, and an `OnNavigatedTo` that forwards the category to `ViewModel_SettingsPage`. The ViewModel owns:

- the role-filtered category list (from `ICurrentUserService`),
- the selected-category state,
- a `[RelayCommand]` that resolves the route and navigates the frame,
- deep-link handling: an initial category key arriving as a navigation parameter is selected directly (Home is the default only when no key is supplied).

No `Task.Delay`, no progress bar, no `Panel.Visible` dictionary, no reflection.

### 2.2 Master-data page — one parameterised page type (chosen, with justification)

**Decision: one `View_MasterDataPage.xaml` + one `ViewModel_MasterDataPage`, parameterised by a `Model_MasterDataDescriptor`, serving all four entities.** Four page types were considered and rejected.

Justification:

1. The four entities share the **same interaction contract**: Add / Edit / Remove as three sections of one page, an entity list, a blank-check validator and an `*_Exists` pre-check. The only differences are the field list, the DAO facade, and one hook (Parts' colour-code cache refresh). Those differences are data, not behaviour — a descriptor is the right abstraction.
2. One ViewModel means one validation, dirty-tracking and load/refresh implementation, tested once, instead of four near-identical copies that can drift (which is exactly the defect class `003` §3.5 documents for the old caches).
3. The heterogeneous fields (Parts has a checkbox and an item-type combo; the others are one or two text boxes) are rendered from a `Fields` collection with a small field-definition model (`Model_MasterDataField`: label, key, editor kind — text / combo / checkbox). This avoids a XAML template selector without forking the page.

The descriptor carries: entity name (for titles/messages), the `IMasterDataFacade` (a thin per-entity seam wrapping the DAO: list, exists, add, update, delete, read-for-edit), the `Fields` list, and an optional `OnWriteCompletedAsync` hook (Parts uses it to call `IMasterDataCache.RefreshAsync` when `RequiresColorCode` changed).

```csharp
// Contracts/IMasterDataFacade.cs — the per-entity seam, implemented once per entity
public interface IMasterDataFacade
{
    Task<Model_Result<IReadOnlyList<string>>> ListAsync();
    Task<Model_Result<bool>> ExistsAsync(string key);
    Task<Model_Result<bool>> AddAsync(IReadOnlyDictionary<string, object> fields);
    Task<Model_Result<bool>> UpdateAsync(string key, IReadOnlyDictionary<string, object> fields);
    Task<Model_Result<bool>> DeleteAsync(string key);
}
```

Each entity gets a small facade (`Service_MasterDataParts`, `Service_MasterDataLocations`, …) that maps the dictionary to the DAO's strongly-typed parameters. The DAO methods themselves stay strongly-typed; the dictionary is the shared page's transport, not the DAO's contract. (The precise facade surface is confirmed at TASK-098; the shape above is the target, not yet code.)

The entity list is a `ListView` bound to the facade's typed rows — master data has no per-user column state, so the shared grid component from `003` is not required here.

### 2.3 Role → category availability table

Implemented as a single lookup owned by `ViewModel_SettingsPage` (or `Service_SettingsNavigation`), consumed by route resolution and by the pane. Same table as spec §3.1; the implementation encodes it once:

```csharp
// role id → allowed category keys (Home and About are always present)
1, 4 → all
3   → Home, PartNumbers, Operations, Locations, ItemTypes (add-only), Appearance, Shortcuts, About
2   → Home, Appearance, About
```

Master-data add-only for Normal is a second dimension: `ICurrentUserService.IsInRole(3)` suppresses the edit/remove sections in `ViewModel_MasterDataPage`.

### 2.4 User management — the atomic create sequence

`ViewModel_UserManagement.AddAsync` executes, in order:

1. Validate: first, last, user, PIN non-blank; shift selected (index > 0). Uppercase the username.
2. `IDao_User.UserExistsAsync(uppercased)` → if true, `IDialogService` "User already exists" and stop.
3. Open one transaction via the `002` helper's caller-supplied transaction path.
4. `usr_users_Add_User` (same parameters as legacy, including `ThemeName = "Default"`, `ThemeFontSize = 9`, `HideChangeLog = "false"`, `VitsUser = false`, default connection triple).
5. `usr_users_Get_ByUser` → read the new `ID` **inside the same transaction**.
6. `sys_user_roles_Add(UserID, RoleID, AssignedBy = Environment.UserName)` **inside the same transaction**.
7. Commit only if step 6 returns success; otherwise roll back and surface the role-add error.

Result: a user is never observable without a role. The sequence is exercised end-to-end in the TASK-101 integration test (success, duplicate, and forced role-add failure with rollback).

Edit and Remove:

- Edit: populate the user combo from `usr_users_Get_All`; on selection load `usr_users_Get_ByUser` and `sys_user_roles_Get_ById`, but **never** populate a PIN. Save validates PIN per the confirmed rule (spec §4 item 2), calls `usr_users_Update_User` then `sys_user_roles_Update`.
- Remove: confirmation via `IDialogService.ShowConfirmationAsync`, then `IDao_User.DeleteUserAsync` (→ `usr_users_Delete_User`, which cascades). No client-side second delete.

### 2.5 Shortcuts

`ViewModel_Shortcuts` calls `IShortcutService` only — no direct DAO access, no cache mutation. On load it awaits `IShortcutService.EnsureDefaultsExistAsync()` and `GetShortcutsAsync()`, then builds nine category groups of `Model_ShortcutItem`. The capture dialog is a `WindowedContentDialog` via `IDialogService` whose content ViewModel receives the raw key chord, rejects bare modifier keys, and returns the formatted chord. Save rejects `Alt+0..9` and duplicates (validated against the current effective set, user and system). Reset confirms and calls `IShortcutService.ResetAsync()`.

The legacy fire-and-forget `_ = LoadShortcuts()` becomes an awaited load in `OnNavigatedTo`. The unlocked `Dictionary` cache and the seeding "write only when changed" rule live inside `003`'s `IShortcutService`; this module neither reimplements nor bypasses them.

### 2.6 Database settings — the single-write sequence

`ViewModel_Database.LoadAsync` reads `usr_settings_Get` JSON and binds `WipServerAddress`, `WipServerPort`, `WIPDatabase` (fallbacks `172.16.1.104`, `3306`, `mtm_wip_application_winforms`).

`ViewModel_Database.SaveAsync`:

1. Validate required fields and port ∈ 1–65535.
2. Read the current `SettingsJson` **once**.
3. Replace the three keys in the single JSON object.
4. Write **once** via `usr_settings_SaveFullJson`.
5. If the active connection differs from the proposed one, raise **one** restart request to `004`; otherwise do nothing.

No parallel merges, no `Application.Restart`. The `Service_SettingsDatabase` owns steps 2–4 so the ViewModel stays unit-testable; the restart request is an injected callback/contract owned by `004`.

Test Connection builds a proposed connection string from the form values, opens a short-lived probe, and reports latency or a plain failure reason. Reset restores the three defaults without saving.

### 2.7 Appearance and About

- `ViewModel_Appearance` binds the three-way mode selector (`IThemeService`: Light / Dark / System) and three toggles (`IUserPreferences`: EnableAnimations, AutoExpandPanels, ShowTotalSummaryPanel). The mode persists immediately to the new `usr_settings` key; the toggles persist to their existing keys. No palette, no theme-name list, no font-size slider (rationale: `003` §3.2).
- `ViewModel_About` reads assembly version and copyright, and loads `Assets/ReleaseNotes/RELEASE_NOTES.json` into `Model_ReleaseNoteEntry` items for native rendering. No WebView2, no HTML template, no watermark substitution.

---

## 3. Data & State Migration

### 3.1 DAOs and procedures touched (all pre-existing, none changed)

| Entity | DAO (`002`) | Procedures consumed |
| :--- | :--- | :--- |
| Users | `IDao_User` | `usr_users_Add_User`, `usr_users_Get_ByUser`, `usr_users_Get_All`, `usr_users_Update_User`, `usr_users_Delete_User`, `usr_users_Exists` |
| Roles | `IDao_System` | `sys_user_roles_Add`, `sys_user_roles_Update`, `sys_user_roles_Get_ById` |
| Parts | `IDao_Part` | `md_part_ids_Get_All`, `md_part_ids_Get_ByItemNumber`, `md_part_ids_GetAllColorCodeFlagged`, `md_part_ids_Update_Part`, `md_part_ids_Delete_ByItemNumber`, `md_part_ids_UpdateColorCodeFlag`, `md_item_types_GetDistinct` |
| Locations | `IDao_Location` | `md_locations_Get_All`, `md_locations_Exists_ByLocation`, `md_locations_Add_Location`, `md_locations_Update_Location`, `md_locations_Delete_ByLocation` |
| Operations | `IDao_Operation` | `md_operation_numbers_Get_All`, `md_operation_numbers_Exists_ByOperation`, `md_operation_numbers_Add_Operation`, `md_operation_numbers_Update_Operation`, `md_operation_numbers_Delete_ByOperation` |
| Item Types | `IDao_ItemType` | `md_item_types_Get_All`, `md_item_types_Exists_ByType`, `md_item_types_Add_ItemType`, `md_item_types_Update_ItemType`, `md_item_types_Delete_ByType` |
| Colour Codes | `IDao_ColorCode` | `md_color_codes_GetAll`, `md_color_codes_Add` — **read/consume only; no settings UI** |
| Shortcuts | `IDao_Shortcuts` | `sys_shortcuts_GetAll`, `usr_user_shortcuts_GetByUser`, `sys_shortcuts_Upsert`, `usr_user_shortcuts_Upsert`, `usr_user_shortcuts_Reset` |
| Settings | `IDao_System` / `IUserPreferences` | `usr_settings_Get`, `usr_settings_SaveFullJson` |

### 3.2 State introduced

| State | Owner | Lifetime |
| :--- | :--- | :--- |
| Selected settings category + deep-link key | `ViewModel_SettingsPage` | page lifetime |
| Per-page dirty flag | each page ViewModel | page lifetime |
| Master-data descriptor registry (four descriptors) | `Service_SettingsNavigation` or a constants class | process (immutable) |
| Restart request | `Service_SettingsDatabase` → `004` contract | transient |

### 3.3 State eliminated

`Model_Application_Variables` user/role flags and connection statics, `SettingsForm` panel state, `Service_Shortcut._shortcutCache` (moved to `003`), the WebView2 release-notes surface, and the simulated progress state. None of these are ported; their replacements are named in §2 and in `003` §3.5.

### 3.4 Persistence compatibility

- `usr_settings` JSON keys `WipServerAddress`, `WipServerPort`, `WIPDatabase`, `EnableAnimations`, `AutoExpandPanels`, `ShowTotalSummaryPanel` load unchanged; the appearance mode is a **new** key, and the old `Theme_Name` / `Theme_FontSize` are ignored but not deleted.
- `usr_user_shortcuts` / `sys_shortcuts` rows are unchanged.
- `usr_users` / `sys_user_roles` rows are unchanged; the only behavioural change is the atomic write.
- `RELEASE_NOTES.json` content is copied as data by `001`; no user data is migrated.
