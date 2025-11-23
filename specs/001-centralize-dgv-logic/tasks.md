# Tasks: Centralize DataGridView Logic

**Feature Branch**: `001-centralize-dgv-logic`
**Spec**: [spec.md](spec.md)

## Phase 1: Setup

**Goal**: Initialize the new service structure.

- [x] T001 Create `Service_DataGridView.cs` in `Services` folder with basic class structure
- [x] T002 Implement `ConfigureColumns` method in `Service_DataGridView` to handle visibility, ordering, and header renaming
- [x] T003 Implement `ApplyStandardSettingsAsync` method in `Service_DataGridView` to wrap `Core_Themes` calls
- [x] T004 Implement `ApplyInventoryColorCoding` method in `Service_DataGridView` with centralized color list
- [x] T005 Implement `SortByColorPriority` method in `Service_DataGridView` for custom sorting logic
- [x] T006 Implement `ApplyTransactionRowColors` method in `Service_DataGridView` for transaction type coloring
- [x] T007 Implement `PrintGridAsync` method in `Service_DataGridView` with validation and error handling

## Phase 3: User Story 1 - Developer Maintenance

**Goal**: Refactor existing controls to use the new service, reducing code duplication.

**Independent Test**: Verify that `Control_TransferTab` behaves exactly as before but uses the new service methods.

- [x] T008 [US1] Refactor `Control_TransferTab.cs` to use `Service_DataGridView.ConfigureColumns` and `ApplyStandardSettingsAsync`
- [x] T009 [US1] Refactor `Control_TransferTab.cs` to use `Service_DataGridView.ApplyInventoryColorCoding` and `SortByColorPriority`
- [x] T010 [US1] Refactor `Control_TransferTab.cs` to use `Service_DataGridView.PrintGridAsync`
- [x] T011 [US1] Remove duplicated private methods (`ApplyColorCodingToRows`, `SortInventoryByColorPriority`) from `Control_TransferTab.cs`
- [x] T012 [US1] Refactor `Control_RemoveTab.cs` to use `Service_DataGridView` methods (Configure, Theme, Color, Sort, Print)
- [x] T013 [US1] Remove duplicated private methods from `Control_RemoveTab.cs`
- [x] T014 [US1] Refactor `Control_AdvancedRemove.cs` to use `Service_DataGridView` for column setup and printing
- [x] T015 [US1] Refactor `TransactionGridControl.cs` to use `Service_DataGridView.ApplyTransactionRowColors` and `PrintGridAsync`

## Phase 4: User Story 2 - Consistent User Experience

**Goal**: Ensure consistent behavior across all grids (verified by testing, no new code needed if refactoring is correct).

**Independent Test**: Compare Transfer and Remove tabs side-by-side to ensure identical behavior for color coding and printing.

- [x] T016 [US2] Verify consistent "No records to print" validation message across all 4 controls
- [x] T017 [US2] Verify consistent row coloring for color-coded parts in Transfer and Remove tabs

## Final Phase: Polish

**Goal**: Final cleanup and verification.

- [x] T018 Verify all XML documentation in `Service_DataGridView` is complete and accurate
- [x] T019 Perform final regression test on all 4 controls (Transfer, Remove, AdvancedRemove, TransactionGrid)

## Dependencies

- Phase 2 (Service Implementation) MUST be completed before Phase 3 (Refactoring).
- Phase 3 tasks for different controls (T008-T015) can be executed in parallel.

## Implementation Strategy

1. **MVP**: Implement `Service_DataGridView` and refactor `Control_TransferTab` first to prove the concept.
2. **Rollout**: Apply to `Control_RemoveTab`, `Control_AdvancedRemove`, and `TransactionGridControl` sequentially or in parallel.
