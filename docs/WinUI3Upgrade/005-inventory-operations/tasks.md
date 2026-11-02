# Migration Task List (Execution Plan)

**Spec**: `005-inventory-operations`
**Range**: TASK-066 … TASK-084
**Definition of done for every task**: its `Verify` line passes, and `dotnet build MTM_WIP.sln -c Debug` plus `dotnet test MTM_WIP.Tests` are both clean.

> **Ordering is load-bearing.** The page shell (TASK-067) precedes every screen, and `Service_ColorCodeValidator` (TASK-068) precedes the Entry screen, because Entry is the first consumer of colour/work-order normalisation and of the quick-button save hook. Transfer (TASK-076) depends on the decision table being pure and tested. Replay (TASK-080) is last among the features because it targets ViewModels that must already exist.

---

## Phase 1 — Module scaffold and page shell

- [ ] **TASK-066** — Create `MTM_WIP.Module_Inventory` folders (`Views/`, `ViewModels/`, `Services/`, `Contracts/`, `Models/`, `Enums/`, `Helpers/`, `Converters/`, `DependencyInjection/`), the `Enums/` values `Enum_InventoryMode` (New/Remove/Transfer) and `Enum_AdvancedInventoryMode` (Single/MultiLoc/Import), and `InventoryServiceRegistration.AddInventoryServices(IServiceCollection)` registering `Service_ColorCodeValidator`, `Service_QuickButtons`, `IService_Inventory`, the six ViewModels and the page view.
  - **Verify**: `dotnet build MTM_WIP.sln -c Debug` succeeds; `InventoryServiceRegistration` is callable from `MTM_WIP`'s composition root without referencing any `Module_*` other than `Module_Inventory`.

- [ ] **TASK-067** — Build `View_InventoryTab` page shell: a two-column `Grid` with a `TabView` mode selector (New/Remove/Transfer) and content host in the left column, `Control_QuickButtonsPanel` in the collapsible right column, and **no page-local status bar** — status, progress and the last-saved indicator are published to the shell footer (D25) through `004`'s `IShellStatusPublisher`. `ToggleRightPanelCommand` on `ViewModel_InventoryTab` collapses/expands the right column; default reads `IUserPreferences.AutoExpandPanels`.
  - **Verify**: the page renders with all three modes switching content; the right panel collapses and expands; saving publishes a last-saved value to the shell footer and a failed save publishes a transient failure there, with neither drawn inside the page. `dotnet build` clean. Manual smoke at 100 % and 150 % scaling.

---

## Phase 2 — Shared services and contracts

- [ ] **TASK-068** — Implement `Service_ColorCodeValidator`: `ValidateAndFormatWorkOrder` normalising to `WO-######` zero-padded with blank → `"Unknown"`; colour validation against `ValidColorCodes`; and the `"OTHER"` path (add custom colour via `IDao_ColorCode.AddCustomColorAsync`, then `IMasterDataCache.RefreshAsync()`; invalid colour clears and reopens the suggestion overlay).
  - **Verify**: unit tests cover `"42"` → `"WO-000042"`, blank → `"Unknown"`, an `"OTHER"` add calling the DAO then refreshing the cache, and an invalid colour returning "invalid" without calling the DAO. `dotnet test` green.

- [ ] **TASK-069** — Implement `IQuickButtonReplayTarget` (in `Contracts/`) with `CanReplay` and `ReplayAsync(QuickButtonSlot, CancellationToken)`, per [`plan.md`](./plan.md) §2.4 and spec §3.7.4.
  - **Verify**: the interface compiles with no `GetMethod`, `Invoke`, or `PerformClick` anywhere in its namespace; a grep of the module for `PerformClick` and `GetMethod` returns zero hits.

- [ ] **TASK-070** — Implement `Service_QuickButtons` over `IDao_QuickButtons`: reload (10 fixed slots, positions 1–10 created up-front), automatic `AddOrShiftQuickButtonAsync` on save, edit (`sys_last_10_transactions_Update_ByUserAndPosition` + `CleanupGapsAndDuplicatesAsync`), remove (`sys_last_10_transactions_RemoveAndShift_ByUser`, position clamped 1–10), reorder (`DeleteAllQuickButtonsForUserAsync` then `AddQuickButtonAsync` per position), and hotkey id mapping `Shortcut_QuickButton_01..10`.
  - **Verify**: unit tests with a mocked `IDao_QuickButtons` assert each operation calls the expected DAO method in order, that position is clamped to 1–10, and that a save triggers add-or-shift then reload.

- [ ] **TASK-071** — Implement `Control_QuickButtonSlot` and `Control_QuickButtonsPanel` (XAML + minimal code-behind): each slot shows hotkey badge, PartID, Operation, Qty, WorkOrder, and a ColorCode accent border. Panel binds `Slots` and exposes `EditSlotCommand`, `RemoveSlotCommand`, `ReorderCommand`, `ReplayCommand`.
  - **Verify**: the panel renders 10 slot cards; each card shows its five fields; the accent border colour follows the slot's ColorCode. `dotnet build` clean.

---

## Phase 3 — Inventory Entry (New)

- [ ] **TASK-072** — Implement `Model_InventoryDraft` and `ViewModel_InventoryEntry` with `SaveCommand`, `ResetCommand`, `HelpCommand`, `ToggleAdvancedCommand`. Port the validation order (part → operation → location → quantity `int > 0`, each failure focuses its field) and `Update_SaveButtonState` as a `CanSave` predicate re-evaluated on every property change (part/op/loc match master data **or** the last-used cache; qty > 0; ColorCode required when visible and in `ValidColorCodes`; WorkOrder required when visible).
  - **Verify**: unit tests cover each validation failure focusing the correct field, the full save-gate predicate truth table, and the "last used" acceptance path. `dotnet test` green.

- [ ] **TASK-073** — Implement `ViewModel_InventoryEntry` save flow: Dunnage forcing (ItemType "Dunnage" → Quantity "1", Operation "N/A", both disabled); `AddInventoryItemAsync(partId, loc, op, qty, batch:"", user, notes, colorCode, workOrder, ...)` with DAO-side batch resolution (`inv_inventory_GetNextBatchNumber`, D10 format, fallback `"0000000001"`); success → status "Last Inventoried Part: …", soft reset, `AddToLast10TransactionsIfUniqueAsync` → `AddOrShiftQuickButtonAsync`, reload quick buttons.
  - **Verify**: unit tests with mocked DAOs assert the exact `AddInventoryItemAsync` argument order, the fallback batch value, the Dunnage override, the status update, and the quick-button hook firing. Integration test adds an item in a rolled-back transaction and reads it back.

- [ ] **TASK-074** — Implement `View_InventoryEntry` XAML (suggestion fields for Part/Operation/Location, `NumberBox` quantity, conditional ColorCode/WorkOrder boxes visible only for `ColorCodeParts`, notes box, Save/Reset/Help/Advanced buttons) with code-behind limited to focus-on-validation-failure and value forwarding.
  - **Verify**: `dotnet build` clean; manual smoke: save a part without colour tracking, save a colour-tracked part, trigger each validation failure in order, and confirm the code-behind contains no `if`/`foreach`/`try`/`catch`/DB call.

---

## Phase 4 — Remove

- [ ] **TASK-075** — Implement `Model_RemoveRow` and `ViewModel_Remove` with `SearchCommand`, `ShowAllCommand`, `DeleteCommand`, `UndoCommand`, `PrintCommand`, `SearchVisualCommand`, `ToggleAdvancedCommand`. Port search rules (Part required "Please select a valid Part.", Operation optional → `inv_inventory_Get_ByPartIDandOperation` else `inv_inventory_Get_ByPartID`; Show All → `inv_inventory_Get_All`), delete rules (≥1 row or silent return; Yes/No confirmation listing PartID/Location/Qty; per-row `inv_inventory_Remove_Item`; skip rows missing part/location/operation; whole-row only; status `0` = "not found" collected as not-deleted), and single-level Undo (`AddInventoryItemAsync` with note `"Removal reversed via Undo Button."`, enabled only after a delete).
  - **Verify**: unit tests cover the three search dispatches, the silent-zero-selection path, the skip rule, the status-`0` "not found" path, and the single-level undo enable/disable. Integration test removes and undoes one row in a rolled-back transaction.

- [ ] **TASK-076** — Implement `View_Remove` XAML (search fields, shared grid component bound to `Rows`/`SelectedRows`, Search/ShowAll/Delete/Undo/Print/SearchVisual/Advanced buttons) with code-behind limited to focus and value forwarding.
  - **Verify**: `dotnet build` clean; manual smoke of search, delete with confirmation, and undo.

---

## Phase 5 — Transfer

- [ ] **TASK-077** — Implement `Helper_TransferDecision` (pure resolver) and `ViewModel_Transfer` with `SearchCommand`, `TransferCommand`, `ToggleAdvancedCommand`. Port search (Part required, Operation optional), the **verified** result order (non-empty `ColorCode` first, then `ColorCode` alphabetical, then `Location` alphabetical — there is no priority list), selection change (`TransferQuantity.Maximum` = row qty, `Value` = row qty; Dunnage → 1 and disabled), and the decision table (split via `inv_inventory_transfer_quantity`, whole via `inv_inventory_Transfer_Part`, multi via `TransferPartSimpleAsync` full-quantity only, one history row per row via `IDao_TransactionHistory.AddTransactionHistoryAsync("TRANSFER", From, To, qty)`); `ToLocation` required, else "Please select a row to transfer from."
  - **Verify**: fixture rows mixing empty and non-empty colour codes come back in the documented order; each row of the decision table is covered by a unit test; a partial quantity never calls the whole-row procedure.
  - **Verify**: unit tests exercise every row of the decision table from [`plan.md`](./plan.md) §2.3, the ToLocation/empty-selection guards, and the per-row history count. Integration test performs a whole-row transfer in a rolled-back transaction.

- [ ] **TASK-078** — Implement `View_Transfer` XAML (Part required, Operation optional, ToLocation required, `NumberBox` quantity, grid columns Location/PartID/ColorCode/WorkOrder/Operation/Quantity/Notes) with code-behind limited to focus and value forwarding.
  - **Verify**: `dotnet build` clean; manual smoke of a split transfer, a whole transfer, and a multi-row transfer.

---

## Phase 6 — Advanced modes

- [ ] **TASK-079** — Implement `ViewModel_AdvancedInventory` and `View_AdvancedInventory` as on-page sub-modes (Single / MultiLoc / Import+Excel). Single = part/op/loc/qty + count. MultiLoc = part/op + location/qty list preview; `SaveAll` loops `AddInventoryItemAsync` per row. Import = open `WIPAppTemplate.xlsx`, validate the file is not locked, import into the grid, batch save. Every path keeps `AddInventoryItemAsync` semantics.
  - **Verify**: unit tests cover the MultiLoc save loop (N rows → N `AddInventoryItemAsync` calls in order) and the locked-file guard. Manual smoke of Single, MultiLoc, and Import. `dotnet build` clean.

- [ ] **TASK-080** — Implement `ViewModel_AdvancedRemove` and `View_AdvancedRemove` as an on-page mode: filters PartID/Operation/Location/QtyMin/QtyMax/Notes/User/FilterByDate/DateFrom/DateTo → `inv_inventory_Search_Advanced`; validate at least one field and `From <= To`; Delete/Undo reuse the Remove DAO paths; SearchVisual launches Infor Visual.
  - **Verify**: unit tests cover "at least one field" validation, `From > To` rejection, and that Delete/Undo delegate to the Remove paths. Manual smoke of a filtered search and delete. `dotnet build` clean.

---

## Phase 7 — Quick-button integration

- [ ] **TASK-081** — Wire replay: each operation ViewModel implements `IQuickButtonReplayTarget` (Entry fills Part/Operation/Quantity then runs Save; Remove and Transfer fill their search fields then run Search). `ViewModel_InventoryTab` hands the active target to `ViewModel_QuickButtonsPanel` on mode change.
  - **Verify**: unit tests assert each target's `ReplayAsync` sets the expected fields and invokes its own command, and that `CanReplay` is false while a multi-select transfer is in progress. A grep finds no reflection API in the module.

- [ ] **TASK-082** — Wire reload and hotkeys: reload quick buttons on Load and after every save/edit/remove/reorder; register F1–F10 via `IShortcutService` ids `Shortcut_QuickButton_01..10` (accelerator wiring owned by `004`).
  - **Verify**: unit tests with mocked services assert reload fires on each of the five triggers; `IShortcutService` receives the ten ids in order. Manual smoke: F1 replays slot 1.

---

## Phase 8 — Threading/state sweep and full pass

- [ ] **TASK-083** — Sweep the module for legacy threading/state mechanisms and map the no-WinUI-equivalent controls: no `async void`, no `InvokeRequired`/`Invoke`/`BeginInvoke`, no reflection replay or `PerformClick`, no `static MainFormInstance`/mutable statics, no `ProcessCmdKey`; `NumericUpDown`/`DateTimePicker`/`RichTextBox`/`TabControl`/drag-reorder all replaced by their WinUI counterparts.
  - **Verify**: the forbidden-API gate (`001` TASK-008) passes over the module; a targeted grep returns zero hits for `Invoke`, `PerformClick`, `GetMethod`, `ProcessCmdKey`, `async void`; `dotnet build -c Debug` clean with zero warnings introduced.

- [ ] **TASK-084** — **Downstream validation: `006-transaction-history` (and a sweep of `007`–`011`).** Before declaring this spec done, re-read [`../006-transaction-history/spec.md`](../006-transaction-history/spec.md), [`../006-transaction-history/plan.md`](../006-transaction-history/plan.md) and [`../006-transaction-history/tasks.md`](../006-transaction-history/tasks.md), and confirm every assumption this spec made about what comes next still holds. Then sweep `007`–`011` for anything that depends on this spec's output.

  Record the result in the **Drift log** below. Specifically confirm:
  1. The procedure behind `IDao_TransactionHistory.AddTransactionHistoryAsync` exists and its parameters match what Transfer passes (`TRANSFER`, From, To, qty) — spec §3.6 gap #2.
  2. The transfer history rows written here are exactly the rows `006` expects to read, with matching column semantics.
  3. The quick-button slots (`sys_last_10_transactions_*`) are not claimed by any other spec; `006` reads transaction history, not quick buttons.
  4. `007`'s master-data editing refreshes `IMasterDataCache`, so entry/remove/transfer see edited parts/locations/operations without restart.
  5. The Infor Visual launch target for SearchVisual is owned and implemented (this spec only invokes it) — gap #4.
  6. `008` does not expect any inventory grid model this spec owns (the shared grid component contract is unchanged).
  7. `011`'s publish payload includes `Assets/Templates/WIPAppTemplate.xlsx` for the Advanced Inventory import — gap #3.

  If any item is false, fix **this** spec (or amend the downstream spec) before proceeding. Do not carry a known mismatch forward.

  - **Verify**: a written entry exists in the Drift log below for each of the seven items, each marked *confirmed* or *amended*, and `dotnet test MTM_WIP.Tests` is green.

---

## Drift log

Filled in by TASK-084. One row per downstream spec checked.

| Checked spec | Item | Result | Action taken |
| :--- | :--- | :--- | :--- |
| `006-transaction-history` | `AddTransactionHistoryAsync` procedure and parameters match | *(pending)* | — |
| `006-transaction-history` | Transfer history rows match `006`'s read expectations | *(pending)* | — |
| `006-transaction-history` | Quick-button procedures not claimed by `006` | *(pending)* | — |
| `007-settings-and-administration` | Master-data edits refresh `IMasterDataCache` | *(pending)* | — |
| `008-visual-analytics` | SearchVisual launch target owned and implemented | *(pending)* | — |
| `008-visual-analytics` | No grid-model contract overlap | *(pending)* | — |
| `011-deployment-and-cutover` | `WIPAppTemplate.xlsx` included in publish payload | *(pending)* | — |
