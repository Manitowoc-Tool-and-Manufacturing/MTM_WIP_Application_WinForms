# Functional Specification: WinForms to WinUI 3 Mapping

**Spec**: `005-inventory-operations`
**Depends on**: `001-foundation-and-tooling`, `002-data-access-layer`, `003-crosscutting-services-and-theming`, `004-shell-and-startup`
**Unblocks**: `006-transaction-history` (transfer/remove write the history rows it reads), `007-settings-and-administration` (master-data edits this module consumes)
**Tasks**: TASK-066 … TASK-084
**Governing rules**: [`../constitution.md`](../constitution.md) §2 Rules 1, 2, 3, 6, 7, 8, 10, 11, 12

---

## 1. Executive Summary & Scope

This spec ports the three live inventory operations screens — New (entry), Remove, and Transfer — plus their two Advanced modes and the Quick Buttons panel, into `MTM_WIP.Module_Inventory`. It is the first screen spec in the migration: it is where the shared services delivered by `003` are first consumed end to end.

The legacy screen is a single `MainForm` tab control whose three pages each hold a normal control and a sibling "Advanced" control toggled with `Visible`, inside a `SplitContainer` that puts the tabs on the left and ten Quick Buttons on the right. The whole assembly shares one `StatusStrip` (progress bar, status text, saved indicator) and one connection-strength control. The migration keeps that shape: one `Page` with three in-page operation modes, an in-page Advanced sub-mode per operation, a collapsible right panel holding the ten quick buttons, and status publishing to the shell footer. The shell's `MenuStrip`, the window chrome and the **one docked footer bar** (D25) belong to `004`; this page publishes to that footer rather than drawing its own.

The point of the migration is not new behaviour. It is to move the behaviour onto `Model_Result<T>` results, `[RelayCommand]`s, and injected services — and to delete the legacy machinery that WinUI 3 cannot host: `async void` handlers, `InvokeRequired`/`Invoke` marshalling, reflection into another control's private suggestion boxes, a `static MainFormInstance` on every control, and `ProcessCmdKey` global hotkeys.

### In scope

- Inventory Entry (`Control_InventoryTab` → `View_InventoryTab` + `ViewModel_InventoryTab`).
- Remove (`Control_RemoveTab` → `View_RemoveTab` + `ViewModel_RemoveTab`).
- Transfer (`Control_TransferTab` → `View_TransferTab` + `ViewModel_TransferTab`).
- Advanced Inventory (Single / MultiLoc / Import+Excel) and Advanced Remove, as on-page modes.
- Quick Buttons: ten fixed slots, auto-create, edit, remove, reorder, replay, F1–F10 hotkeys.
- The collapsible right panel and status publishing to the shell footer.
- The threading/state clean-up: `async void`, `InvokeRequired`, reflection replay, static form instances, global hotkeys.

### Out of scope

- Transactions search / lifecycle / analytics — `006`.
- Settings, users, master-data editing, shortcuts editor — `007`.
- The shell, navigation, window chrome, and launch sequence — `004`.
- Any database change. The database is frozen (constitution §2 Rule 4). Every procedure below already exists and keeps its signature.

### Success condition

An operator can add, remove, transfer, search-advanced, import-advanced, and replay a quick button on one page with a collapsible right panel, with every operation asynchronous, every failure through `IErrorService`, every DAO result checked via `Model_Result<T>`, and no `System.Windows.Forms`, `MessageBox`, `MySql.Data`, `Control.Invoke`, reflection replay, or `ProcessCmdKey` anywhere in the module.

---

## 2. Legacy Feature Assessment & UI Mapping Table

| WinForms Artefact | Target WinUI 3 Artefact | Functional Requirement / Core Behavior |
| :--- | :--- | :--- |
| `MainForm` tab control, pages "New" / "Remove" / "Transfer" | `View_InventoryTab` — one `Page` with an in-page mode selector (WinUI `TabView`) | Three operation modes of **one page**, not three routes. The right panel and status area are shared across all three. |
| Sibling "Advanced" control per page, toggled with `Visible` | In-page Advanced sub-mode flag on the Entry and Remove modes | Advanced Inventory and Advanced Remove stay modes on the page; navigating away would lose partially entered data (see §3.8). |
| `SplitContainer` — tabs left, Quick Buttons right | `Grid` with two columns; right column is the collapsible panel | `Toggle_RightPanel` collapses/expands the right column. Default state reads `IUserPreferences.AutoExpandPanels`. |
| `MenuStrip` + `StatusStrip` (progress bar, status text, saved indicator) + connection-strength control | Shell navigation + **one shell footer bar** (`004`, D25) + shared diagnostics control (`003`) | The page publishes "Last Inventoried Part: …", transient success/failure and progress to the shell footer; it draws no status bar of its own. The menu and window chrome are `004`'s. |
| `Control_InventoryTab` (New) | `View_InventoryTab` + `ViewModel_InventoryTab` | Entry fields Part → Operation → Location → Quantity → (ColorCode) → (WorkOrder) → Notes; validation order and save gating in §3.1. |
| `Component_SuggestionTextBoxWithLabel` for Part / Operation / Location | Suggestion control from `003` | Same behaviour: `%` wildcards, 100-result cap, F4 full list, exact-match suppression, master-data no-match clears the field. |
| `NumericUpDown` quantity, transfer quantity | WinUI `NumberBox` | Integer values only; quantity must be `int > 0`. Transfer quantity clamps to the row's quantity (§3.6). |
| `RichTextBox` notes | Multiline `TextBox` | Optional free text, 1000-char column, no rich formatting carried over. |
| `Control_RemoveTab` | `View_RemoveTab` + `ViewModel_RemoveTab` | Search / Delete / Undo semantics in §3.5. |
| `Control_TransferTab` | `View_TransferTab` + `ViewModel_TransferTab` | Partial vs whole vs multi transfer decision table in §3.6. |
| Quick Buttons panel — 10 fixed slots, positions 1–10 | `QuickButtonsPanel` of 10 slot cards | Auto-create on save, edit, remove, reorder, replay; F1–F10 hotkeys. See §3.7. |
| `Form_QuickButtonEdit` (part combo, op combo, qty) | Dialog opened through `IDialogService` | Edits one slot: part, operation, quantity. |
| `ListView` drag-reorder of quick buttons | Explicit Reorder command (move-up / move-down or position picker) | WinUI has no drag-reorder primitive matching this; reorder is command-driven. |
| `Control_InventoryTab_Advanced` inner `TabControl` Single / MultiLoc / Import+Excel | Advanced Entry mode with Single / MultiLoc / Import sub-modes | §3.8. Every path keeps the same `AddInventoryItemAsync` semantics. |
| Advanced Remove filters + `DateTimePicker` | Advanced Remove mode; WinUI `DatePicker` for From/To | `inv_inventory_Search_Advanced`; validate at least one field, From ≤ To. |
| `ProcessCmdKey` global hotkeys for F1–F10 | `IShortcutService` + page accelerators (`004`/`003`) | Shortcut ids `Shortcut_QuickButton_01..10`; no `ProcessCmdKey`. |
| Reflection into the target tab's private suggestion boxes + `PerformClick` for replay | `IQuickButtonReplayTarget` implemented by each operation ViewModel | No reflection in the new client (§3.7.4). |
| `static MainFormInstance` / `Model_Application_Variables` statics on every control | Injected `ICurrentUserService`, `IMasterDataCache`, `IUserPreferences` | No static mutable state (constitution §2 Rule 11). |

---

## 3. Behavioral Edge Cases & State Constraints

### 3.1 Inventory entry — validation and save gating

- Field order: Part → Operation → Location → Quantity → (ColorCode) → (WorkOrder) → Notes.
- Required: Part, Operation, Location, Quantity. Notes optional.
- Validation order on save: part non-empty → operation non-empty → location non-empty → `int.TryParse(quantity) > 0`. Each failure focuses the offending box and stops; it does not advance to the next field.
- `Update_SaveButtonState` (ported as a `CanExecute`-driven predicate, re-evaluated on every relevant property change) enables Save only when:
  - Part, Operation and Location each either match master data **or** are present in the "last used" cache; and
  - Quantity parses to an int greater than 0; and
  - when the ColorCode box is visible, a ColorCode is present **and** is in `ValidColorCodes`; and
  - when the WorkOrder box is visible, a WorkOrder is present.
- `Model_Result<T>` note: master-data lookups return status `0` ("no rows") as **success** with empty data. A cache miss is therefore `IsSuccess == true` with no row, never an exception.

### 3.2 ColorCode and WorkOrder — conditional visibility and normalisation

- ColorCode and WorkOrder boxes are visible **only** when the selected Part is in `ColorCodeParts` (parts flagged `RequiresColorCode`).
- WorkOrder is normalised by `Service_ColorCodeValidator.ValidateAndFormatWorkOrder` to `WO-######` zero-padded; a blank WorkOrder becomes `"Unknown"` on save.
- ColorCode typed as `OTHER` triggers: `Dao_ColorCode.AddCustomColorAsync(...)` then a master-data cache refresh of the colour collections (`IMasterDataCache.RefreshAsync()`, replacing `Model_Application_Variables.ReloadColorCodePartsAsync()`). An invalid colour clears the text and reopens the suggestion overlay.
- The colour-code and combo caches are one `IMasterDataCache`; the legacy drift where the colour-code cache and combo caches refresh on different triggers is eliminated.

### 3.3 Dunnage forcing

- If `Dao_Part.GetPartByNumberAsync(...)` reports `ItemType == "Dunnage"`, Quantity is forced to `"1"`, Operation is forced to `"N/A"`, and both fields are disabled for the draft.

### 3.4 Save flow

`Save` calls `Dao_Inventory.AddInventoryItemAsync(partId, loc, op, qty, batch: "", user, notes, colorCode, workOrder, ...)`. The DAO:

1. Resolves `ItemType` from `md_part_ids_Get_ByItemNumber`.
2. If no batch is supplied, calls `inv_inventory_GetNextBatchNumber` and formats the result as a 10-digit zero-padded string (fallback `"0000000001"`).
3. Calls `inv_inventory_Add_Item` with parameters `PartID, Location, Operation, Quantity, ItemType, User, BatchNumber, Notes, ColorCode` (default `"Unknown"`), `WorkOrder` (default `"Unknown"`).

After success the shell footer shows "Last Inventoried Part: …" (D25), the draft is soft-reset (quantity cleared, notes cleared, focus returned to Part), and the save is registered with quick buttons via `AddToLast10TransactionsIfUniqueAsync` → `Dao_QuickButtons.AddOrShiftQuickButtonAsync`, then the quick buttons are reloaded.

- The DAO does not return the generated batch number in the current contract; the status line uses the Part, not the batch (verified current behaviour).

### 3.5 Remove

- Search: Part required ("Please select a valid Part."); Operation optional. With an operation → `inv_inventory_Get_ByPartIDandOperation`; without → `inv_inventory_Get_ByPartID`. "Show All" → `inv_inventory_Get_All`.
- Delete:
  - Requires at least one selected row; zero selected rows returns silently (no dialog).
  - A Yes/No confirmation lists PartID / Location / Quantity of the selection.
  - Per row: `inv_inventory_Remove_Item` with `PartID, Location, Operation, Quantity, ItemType, User, BatchNumber, Notes`.
  - Rows missing part, location **or** operation are **skipped**, not failed.
  - Whole-row only; no partial quantity.
  - A result status of `0` means "not found": the row is collected as *not deleted* and reported, not treated as an error.
- Undo:
  - Re-calls `AddInventoryItemAsync` per stored row with the note `"Removal reversed via Undo Button."`.
  - Enabled only after a delete; single level (one undo, then the stored rows are discarded).

### 3.6 Transfer decision table

Search: Part required; Operation optional; with an operation → `inv_inventory_Get_ByPartIDandOperation`, without → `inv_inventory_Get_ByPartID`. Grid columns: Location, PartID, ColorCode, WorkOrder, Operation, Quantity, Notes.

**Result order (verified from source).** There is no business colour-priority list. `Service_DataGridView.SortByColorPriority` sorts by three keys, in this order:

1. rows with a non-empty `ColorCode` first, rows with an empty one last;
2. then `ColorCode` alphabetically;
3. then `Location` alphabetically.

The non-colour-tracked path sorts by `ColorCode`, falling back to `Location` when the column is absent. The source comment in `SortByColorPriority` states the original intent was never known and that the current order was assumed at the time. This spec preserves the observed behaviour exactly and does **not** invent a priority list. If a real priority order is ever wanted, it is a new requirement, not a port.

On selection change: the quantity box `Maximum` = the row's quantity, `Value` = the row's quantity. Dunnage rows force `Value = 1` and disable the box.

| Selection | Condition | Procedure called | History row |
| :--- | :--- | :--- | :--- |
| Single row | `transferQuantity = Min(NumericUpDown.Value, originalQuantity)` < `originalQuantity` | `inv_inventory_transfer_quantity (BatchNumber, PartID, Operation, TransferQuantity, OriginalQuantity, NewLocation, User, ColorCode, WorkOrder)` | one `"TRANSFER"` row |
| Single row | `transferQuantity == originalQuantity` | `inv_inventory_Transfer_Part (BatchNumber, PartID, Operation, NewLocation)` | one `"TRANSFER"` row |
| Multiple rows | always full quantity, never a split | `TransferPartSimpleAsync` per row only | one history row **per row** |

- `ToLocation` is required; without a source selection the command reports "Please select a row to transfer from." rather than proceeding.
- History is written via `Dao_TransactionHistory.AddTransactionHistoryAsync("TRANSFER", From, To, qty)` (the renamed `Dao_History` from `002`). Underlying procedure: *(to confirm)* — `006` owns transaction-history procedures.

### 3.7 Quick buttons

#### 3.7.1 Slots and hotkeys

- Exactly 10 fixed slots, positions 1–10, rows created up-front.
- Hotkeys F1–F10 by default via `IService_Shortcut`, ids `Shortcut_QuickButton_01..10`.
- Source of truth is `sys_last_10_transactions_Get_ByUser`; reloaded on Load and after every save / edit / remove / reorder.

#### 3.7.2 Create / Edit / Remove / Reorder

- **Create is automatic**: every inventory save calls `Dao_QuickButtons.AddOrShiftQuickButtonAsync`, which reads existing rows and shifts/inserts.
- **Edit** → quick-button edit dialog (part combo, op combo, qty) → `sys_last_10_transactions_Update_ByUserAndPosition`, then `CleanupGapsAndDuplicatesAsync`.
- **Remove** → `sys_last_10_transactions_RemoveAndShift_ByUser` (position clamped 1–10) + cleanup.
- **Reorder** → `DeleteAllQuickButtonsForUserAsync` then `AddQuickButtonAsync` per position (`sys_last_10_transactions_Add_AtPosition`).

#### 3.7.3 Slot card

Each button shows: hotkey badge, PartID, Operation, Qty, WorkOrder, and a ColorCode accent border.

#### 3.7.4 Replay contract

Legacy replay uses reflection into the target tab's private suggestion boxes plus `PerformClick`. That is replaced by a contract in `MTM_WIP.Module_Inventory/Contracts/`:

```csharp
public interface IQuickButtonReplayTarget
{
    /// <summary>True when this operation mode can accept a replay right now.</summary>
    bool CanReplay { get; }

    /// <summary>Fills the draft from a quick-button row and runs the mode's save action.</summary>
    Task ReplayAsync(QuickButtonSlot slot, CancellationToken cancellationToken = default);
}
```

Each operation ViewModel (`ViewModel_InventoryTab`, `ViewModel_RemoveTab`, `ViewModel_TransferTab`) implements it. `ViewModel_InventoryTab.ReplayAsync` fills Part / Operation / Quantity and invokes the Save command; the Remove and Transfer ViewModels fill their search fields and run Search. No reflection, no `PerformClick`, no cross-control private access.

### 3.8 Advanced modes stay on the page

Advanced Inventory (inner `TabControl` Single / MultiLoc / Import+Excel) and Advanced Remove are **modes on the Entry/Remove page**, not separate routes:

- Navigating away (e.g. to Settings) would lose partially entered rows in the MultiLoc preview or the import grid; the shell owns navigation and cannot preserve in-progress draft state across pages.
- Single = part/op/loc/qty + count. MultiLoc = part/op + several location/qty rows in a list preview, then SaveAll loops `AddInventoryItemAsync` per row. Import = opens the per-user `WIPAppTemplate.xlsx` via `Process.Start`, validates the file is not locked, imports into the grid, batch saves.
- Every Advanced path keeps the same `AddInventoryItemAsync` semantics (batch resolution, Dunnage forcing, colour/work-order defaults).
- Advanced Remove filters: PartID, Operation, Location, QtyMin, QtyMax, Notes, User, FilterByDate (false in "Everything" mode), DateFrom, DateTo → `inv_inventory_Search_Advanced`. Validates at least one field and `From <= To`. Delete/Undo reuse the Remove DAO paths. SearchVisual launches Infor Visual.

### 3.9 Threading and state problems to eliminate

Each is a hard requirement, not a preference:

| Legacy mechanism | Replacement |
| :--- | :--- |
| `async void` event handlers | `[RelayCommand]` / `AsyncRelayCommand` returning `Task`; exceptions reach `IErrorService` instead of crashing the process |
| `InvokeRequired` / `Control.Invoke` / `BeginInvoke` | `IDispatcherService.TryEnqueue` (constitution §2 Rule 6) |
| Reflection into another control's private suggestion boxes + `PerformClick` | `IQuickButtonReplayTarget` (§3.7.4) |
| `static MainFormInstance` on every control | Constructor-injected services and a per-page ViewModel |
| `ProcessCmdKey` global hotkeys | `IShortcutService` + accelerators owned by `004` |
| `NumericUpDown` / `DateTimePicker` / `RichTextBox` / `TabControl` / `ListView` drag-reorder (no WinUI equivalent) | `NumberBox`, `DatePicker`, multiline `TextBox`, in-page mode selector, command-driven reorder |

---

## 4. Gaps, Missing Data & Clarifications

| # | Item | Status |
| :--- | :--- | :--- |
| 1 | **Transfer result ordering** | **Resolved from source** — there is no colour-priority list. Order is: non-empty `ColorCode` first, then `ColorCode` alphabetical, then `Location` alphabetical. The legacy code comment admits the original intent was unknown. Preserved exactly; no priority list is invented. See §3. |
| 2 | **The underlying procedure behind `Dao_TransactionHistory.AddTransactionHistoryAsync`.** The DAO method name is known (`Dao_History` renamed in `002`); the stored procedure it calls is outside this spec's verified list. | **(to confirm)** — owned by `006`. |
| 3 | **Advanced Inventory Import template path.** Legacy opens the per-user `Controls\MainForm\WIPAppTemplate.xlsx` via `Process.Start`. `001` relocates the template to `Assets/Templates/WIPAppTemplate.xlsx` (build output), which is read-only per-user. | **(to confirm)** — decide whether import copies the template to a per-user writable location or opens the shipped asset read-only. |
| 4 | **SearchVisual target.** "SearchVisual launches Infor Visual" — the exact executable / URL / arguments to launch the ERP for a part are not in the verified facts. | **(to confirm)** — read `AdvancedItemRemoval`'s SearchVisual handler; the ERP launch itself may belong to `008`. |
| 5 | **Replay field set per target.** The contract is fixed (§3.7.4), but which fields each legacy replay fills (Part/Operation/Quantity for Entry; search fields for Remove/Transfer) must be read from the reflection code, not assumed. | **(to confirm)** — enumerate the reflected field assignments during TASK-080. |
| 6 | **Quick-button slot "last used" cache in the save gate.** The gate accepts a part/op/loc that is in the "last used" cache. The exact source of that cache (quick buttons, or a separate in-memory list) is not in the verified facts. | **(to confirm)** — read `Update_SaveButtonState` before TASK-071. |
| 7 | **`DataTable` → typed row models.** The shared grid binds `IEnumerable` of typed rows (constitution `003` §3.3), so each grid screen needs its own row model; the colour/priority fields a row must carry follow from gap #1. | In scope; resolved with TASK-074/TASK-077. |

### Assumptions

1. `mtm_wip_application_winforms_test` exists and mirrors the production procedure set; integration tests roll back per test.
2. The inventory procedures named in §3 exist with the parameters listed; none is renamed or re-signed.
3. `IMasterDataCache` exposes the parts, operations, locations, item-types and colour-code collections plus `RefreshAsync`, and fires a change event so the save gate re-evaluates.
4. `IUserPreferences.AutoExpandPanels` supplies the right-panel default; its absence defaults to expanded.
5. The shell (`004`) owns navigation and F-key accelerator registration; this module only supplies shortcut ids and the replay target.
