# Technical Implementation Plan: WinForms to WinUI 3

**Spec**: `005-inventory-operations`
**Tasks**: TASK-066 … TASK-084

---

## 1. Project Topology & Directory Mapping

All screen code lands in **`MTM_WIP.Module_Inventory`**. The shared grid component and the suggestion control already live in `MTM_WIP.Shared` (delivered by `003`); the quick-button slot card and the replay contract are used only here, so they stay module-local.

```text
MTM_WIP.Module_Inventory/
├── Views/
│   ├── View_InventoryTab.xaml / .xaml.cs          # one page: New / Remove / Transfer modes
│   ├── View_InventoryEntry.xaml                   # Entry (New) form content (UserControl)
│   ├── View_Remove.xaml                           # Remove form content
│   ├── View_Transfer.xaml                         # Transfer form content
│   ├── View_AdvancedInventory.xaml                # Single / MultiLoc / Import sub-modes
│   ├── View_AdvancedRemove.xaml                   # advanced filters
│   └── Controls/
│       ├── Control_QuickButtonsPanel.xaml         # 10-slot right panel
│       └── Control_QuickButtonSlot.xaml           # one slot card (hotkey badge, part, op, qty, WO, colour accent)
├── ViewModels/
│   ├── ViewModel_InventoryTab.cs                  # page-level: mode, panel collapse, status
│   ├── ViewModel_InventoryEntry.cs                # New
│   ├── ViewModel_Remove.cs
│   ├── ViewModel_Transfer.cs
│   ├── ViewModel_AdvancedInventory.cs
│   ├── ViewModel_AdvancedRemove.cs
│   └── ViewModel_QuickButtonsPanel.cs
├── Services/
│   ├── Service_ColorCodeValidator.cs              # WorkOrder normalisation + colour validation rules
│   └── Service_QuickButtons.cs                    # slot orchestration over IDao_QuickButtons
├── Contracts/
│   ├── IQuickButtonReplayTarget.cs                # replay contract (no reflection)
│   └── IService_Inventory.cs                      # entry/remove/transfer orchestration seam for tests
├── Models/
│   ├── Model_InventoryDraft.cs                    # entry draft state
│   ├── Model_QuickButtonSlot.cs                   # one slot row model
│   ├── Model_RemoveRow.cs                         # remove grid row model
│   └── Model_TransferRow.cs                       # transfer grid row model
├── Enums/
│   ├── Enum_InventoryMode.cs                      # New / Remove / Transfer
│   └── Enum_AdvancedInventoryMode.cs              # Single / MultiLoc / Import
├── Helpers/
│   └── Helper_TransferDecision.cs                 # pure decision-table resolver (unit-tested)
├── Converters/                                    # created only when a converter is needed
└── DependencyInjection/
    └── InventoryServiceRegistration.cs            # AddInventoryServices(IServiceCollection)
```

`MTM_WIP.Module_Inventory` references **`Core`, `Shared`, `Data`** only (constitution §2 Rule 10). `Contracts/` holds only module-internal contracts; if a contract ever needs a sibling module, it moves to `Core`/`Shared`, but nothing here does.

### 1.1 DI registration surface

`InventoryServiceRegistration.AddInventoryServices` registers: `Service_ColorCodeValidator`, `Service_QuickButtons`, `IService_Inventory`, and the six ViewModels (transient), alongside the page view. DAOs, `IMasterDataCache`, `IUserPreferences`, `IShortcutService`, `IDialogService`, `IErrorService`, `IDispatcherService` are registered by `002`/`003`/`004` and resolved from the container, not re-registered.

---

## 2. Component Migration Strategy

### 2.1 One page, three modes, shared chrome

The page shell is built before any operation logic, so the three modes share the right panel and status area from the start:

```
View_InventoryTab (Grid, 2 columns)
├── column 0 (star): mode selector (TabView: New / Remove / Transfer)
│                    + content host (the active mode's UserControl)
│                    + page status area (ProgressBar, status TextBlock, saved indicator)
└── column 1 (auto → collapsed): Control_QuickButtonsPanel
```

- The mode selector is bound to `ViewModel_InventoryTab.SelectedMode` (`Enum_InventoryMode`).
- The right column's width is toggled by `Toggle_RightPanel`; default from `IUserPreferences.AutoExpandPanels`.
- The page status area is a module surface. The window-level status strip and connection-strength control are `004`'s.

### 2.2 ViewModel composition per screen

| ViewModel | Commands | State | Services |
| :--- | :--- | :--- | :--- |
| `ViewModel_InventoryTab` | `ToggleRightPanelCommand` | `SelectedMode`, `IsRightPanelOpen`, `StatusText`, `IsBusy` | `IUserPreferences`, `IDispatcherService` |
| `ViewModel_InventoryEntry` | `SaveCommand`, `ResetCommand`, `HelpCommand`, `ToggleAdvancedCommand` | `Draft` (`Model_InventoryDraft`), `CanSave`, `IsDunnage`, `IsColorCodeVisible`, `IsWorkOrderVisible`, `ValidColorCodes` | `IDao_Inventory`, `IDao_Part`, `IDao_ColorCode`, `IMasterDataCache`, `ICurrentUserService`, `IService_ColorCodeValidator`, `IDao_QuickButtons`, `Service_QuickButtons`, `IErrorService` |
| `ViewModel_Remove` | `SearchCommand`, `ShowAllCommand`, `DeleteCommand`, `UndoCommand`, `PrintCommand`, `SearchVisualCommand`, `ToggleAdvancedCommand` | `SearchPart`, `SearchOperation`, `Rows`, `SelectedRows`, `CanUndo`, `UndoRows` | `IDao_Inventory`, `IErrorService`, `IDialogService`, print/export from `003` |
| `ViewModel_Transfer` | `SearchCommand`, `TransferCommand`, `ToggleAdvancedCommand` | `SearchPart`, `SearchOperation`, `ToLocation`, `TransferQuantity`, `Rows`, `SelectedRows` | `IDao_Inventory`, `IDao_TransactionHistory`, `IErrorService`, `Helper_TransferDecision` |
| `ViewModel_AdvancedInventory` | `SaveAllCommand`, `ImportCommand`, `ValidateImportCommand` | `AdvancedMode`, `MultiLocRows`, `ImportRows`, `IsImportFileLocked` | `IDao_Inventory`, `IDao_Part`, `IErrorService`, `IDialogService` |
| `ViewModel_AdvancedRemove` | `SearchAdvancedCommand`, `DeleteCommand`, `UndoCommand`, `SearchVisualCommand` | `Filters`, `Rows`, `SelectedRows`, `UndoRows` | `IDao_Inventory`, `IErrorService`, `IDialogService` |
| `ViewModel_QuickButtonsPanel` | `EditSlotCommand`, `RemoveSlotCommand`, `ReorderCommand`, `ReplayCommand` | `Slots` (10 `Model_QuickButtonSlot`), `Targets` (the three `IQuickButtonReplayTarget`s) | `IDao_QuickButtons`, `Service_QuickButtons`, `IShortcutService`, `IDialogService`, `IErrorService` |

`ViewModel_QuickButtonsPanel` is given the active replay target by `ViewModel_InventoryTab` when the mode changes, so `ReplayCommand` always targets the visible operation.

### 2.3 Transfer decision table (the one source of truth)

`Helper_TransferDecision` is a pure static resolver taking (selection count, transfer quantity, original quantity) and returning an enum + procedure selection. The ViewModel computes nothing itself; tests exercise the table directly.

| Selection | Condition | Decision | Procedure | History |
| :--- | :--- | :--- | :--- | :--- |
| 1 row | `Min(qty, original) < original` | `Split` | `inv_inventory_transfer_quantity (BatchNumber, PartID, Operation, TransferQuantity, OriginalQuantity, NewLocation, User, ColorCode, WorkOrder)` | 1 row |
| 1 row | `Min(qty, original) == original` | `Whole` | `inv_inventory_Transfer_Part (BatchNumber, PartID, Operation, NewLocation)` | 1 row |
| > 1 row | always | `MultiWhole` | `TransferPartSimpleAsync` per row (never split) | 1 row per row |

Preconditions checked before the table runs: Part non-empty; `ToLocation` non-empty; at least one row selected (else "Please select a row to transfer from."). Dunnage rows force quantity `1` and disable the box on selection change.

### 2.4 Quick-button replay contract

`IQuickButtonReplayTarget` (in `Contracts/`, spec §3.7.4) is implemented by the three operation ViewModels. `Control_QuickButtonsPanel` holds a reference to the **active** target only — it never names a concrete ViewModel and never touches another ViewModel's internals.

- `CanReplay` gates the per-slot replay affordance (e.g. an Entry draft with a required field missing is still replayable; a transfer in the middle of a multi-select is not).
- `ReplayAsync(slot)` fills the target's public state and invokes the target's own Save/Search command, so the normal validation and error paths still run.
- The F1–F10 accelerators resolve through `IShortcutService` (ids `Shortcut_QuickButton_01..10`); no `ProcessCmdKey`.

### 2.5 Advanced modes stay on the page

Advanced Inventory and Advanced Remove are mode flags on the Entry/Remove page, **not** navigation routes. Rationale recorded in spec §3.8: navigating away would lose partially entered MultiLoc rows and import grid rows. The shell cannot preserve in-progress draft state across pages.

- Advanced Inventory sub-modes: Single (part/op/loc/qty + count), MultiLoc (part/op + list preview; `SaveAll` loops `AddInventoryItemAsync` per row), Import (open `WIPAppTemplate.xlsx`, verify not locked, import to grid, batch save).
- Advanced Remove: filters PartID / Operation / Location / QtyMin / QtyMax / Notes / User / FilterByDate / DateFrom / DateTo → `inv_inventory_Search_Advanced`; at least one field, `From <= To`; Delete/Undo reuse the Remove DAO paths; SearchVisual launches Infor Visual.

### 2.6 Threading and state elimination (compiler-visible)

| Legacy mechanism | Ported form | Verification hook |
| :--- | :--- | :--- |
| `async void` handlers | `[RelayCommand]` / `AsyncRelayCommand` | Forbidden-API and review gate |
| `InvokeRequired`/`Invoke` | `IDispatcherService.TryEnqueue` | no `Control.Invoke` compiles |
| reflection replay + `PerformClick` | `IQuickButtonReplayTarget` | `typeof(...).GetMethod` / `PerformClick` zero hits |
| `static MainFormInstance` | injected services | no `static` mutable state |
| `ProcessCmdKey` | `IShortcutService` + `004` accelerators | zero hits |
| `NumericUpDown` / `DateTimePicker` / `RichTextBox` / `TabControl` / drag-reorder | `NumberBox` / `DatePicker` / multiline `TextBox` / mode selector / Reorder command | XAML review |

---

## 3. Data & State Migration

### 3.1 State per screen (all ViewModel-owned, none static)

| Screen | State | Lifecycle |
| :--- | :--- | :--- |
| Entry | `Draft`, `CanSave`, Dunnage/colour/work-order visibility, last-used cache | per page instance; soft-reset after save |
| Remove | search terms, `Rows`, `SelectedRows`, `UndoRows`, `CanUndo` | per page instance; `UndoRows` cleared after undo |
| Transfer | search terms, `Rows`, `SelectedRows`, `TransferQuantity` | per page instance |
| Quick Buttons | 10 `Slots` | reloaded on Load and after every save/edit/remove/reorder |
| Advanced | `AdvancedMode`, `MultiLocRows`, `ImportRows` | per page instance |

### 3.2 Persistence touched

- **Inventory writes**: `inv_inventory_Add_Item`, `inv_inventory_Remove_Item`, `inv_inventory_transfer_quantity`, `inv_inventory_Transfer_Part`, and `TransferPartSimpleAsync` — all pre-existing, no signature change.
- **Quick buttons**: `sys_last_10_transactions_Get_ByUser`, `_Update_ByUserAndPosition`, `_RemoveAndShift_ByUser`, `_Add_AtPosition` (plus the DAO's `AddOrShiftQuickButtonAsync`, `DeleteAllQuickButtonsForUserAsync`, `AddQuickButtonAsync`, `CleanupGapsAndDuplicatesAsync`).
- **Master-data reads** go through `IMasterDataCache`, never the DAOs directly at the UI layer.
- **No schema, procedure, column, or `p_Status`/`p_ErrorMsg` change.** The frozen-database rule (constitution §2 Rule 4) applies in full.

### 3.3 Persistence explicitly not migrated

- `Model_Application_Variables.ColorCodeParts` / `ValidColorCodes` statics → `IMasterDataCache` collections.
- The `WipDataGridTheme` per-grid theme → not carried (deleted in `003`).
- `Form_QuickButtonEdit`'s WinForms layout → a dialog over `IDialogService`; the edited fields (part, op, qty) are unchanged.
- The legacy template path `Controls\MainForm\WIPAppTemplate.xlsx` → `Assets/Templates/WIPAppTemplate.xlsx` from `001`; per-user writability is gap #3 in the spec.

### 3.4 Porting order

```
scaffold + DI → page shell (mode selector, panel, status) → Service_ColorCodeValidator
→ Service_QuickButtons + slot card → Entry ViewModel/View → Remove → Transfer
→ Advanced Inventory → Advanced Remove → replay wiring → reload/hotkey orchestration
→ threading/state sweep → full test + smoke pass → downstream validation (006)
```
