# Removed Print System Entry Points

**Feature**: Print and Export System Refactor  
**Created**: 2025-11-08  
**Purpose**: Audit trail of all print entry points documented before Phase 1 removal

---

## Summary

This document catalogs all print-related entry points found in the codebase before the print system refactor. These entry points will be temporarily disabled in Phase 1 and reconnected in Phase 7 (Integration & Polish).

**Total Entry Points Found**: 6

---

## Entry Point Details

### 1. Remove Tab - Print Button

**File**: `Controls/MainForm/Control_RemoveTab.cs`  
**Handler**: `Control_RemoveTab_Button_Print_Click(object? sender, EventArgs? e)`  
**Line**: 828  
**Wired**: Line 117-118  
**Context**: Print button in the Remove tab for printing removal transaction history  
**Current Implementation**: Opens `PrintForm` dialog with `Control_RemoveTab_DataGridView_History` DataGridView

```csharp
// Line 117-118
Control_RemoveTab_Button_Print.Click -= Control_RemoveTab_Button_Print_Click;
Control_RemoveTab_Button_Print.Click += Control_RemoveTab_Button_Print_Click;

// Line 828 (handler method)
private void Control_RemoveTab_Button_Print_Click(object? sender, EventArgs? e)
```

---

### 2. Transfer Tab - Print Button

**File**: `Controls/MainForm/Control_TransferTab.cs`  
**Handler**: `Control_TransferTab_Button_Print_Click(object? sender, EventArgs? e)`  
**Line**: 639  
**Wired**: Line 136-137  
**Context**: Print button in the Transfer tab for printing transfer transaction history  
**Current Implementation**: Opens `PrintForm` dialog with transfer history DataGridView

```csharp
// Line 136-137
Control_TransferTab_Button_Print.Click -= Control_TransferTab_Button_Print_Click;
Control_TransferTab_Button_Print.Click += Control_TransferTab_Button_Print_Click;

// Line 639 (handler method)
private void Control_TransferTab_Button_Print_Click(object? sender, EventArgs? e)
```

---

### 3. Advanced Remove - Print Button

**File**: `Controls/MainForm/Control_AdvancedRemove.cs`  
**Handler**: `Control_AdvancedRemove_Button_Print_Click(object? sender, EventArgs? e)`  
**Line**: 718  
**Wired**: Line 142-143  
**Context**: Print button in the Advanced Remove dialog for printing batch removal results  
**Current Implementation**: Opens `PrintForm` dialog with advanced remove results grid

```csharp
// Line 142-143
printButton.Click -= Control_AdvancedRemove_Button_Print_Click;
printButton.Click += Control_AdvancedRemove_Button_Print_Click;

// Line 718 (handler method)
private void Control_AdvancedRemove_Button_Print_Click(object? sender, EventArgs? e)
```

---

### 4. Transaction Grid Control - Print Button

**File**: `Controls/Transactions/TransactionGridControl.cs`  
**Handler**: `BtnPrint_Click(object? sender, EventArgs e)`  
**Line**: 443  
**Wired**: Line 229  
**Context**: Print button in the reusable Transaction Grid Control for printing search results  
**Current Implementation**: Raises `PrintRequested` event which is handled by parent form

```csharp
// Line 229
TransactionGridControl_Button_Print.Click += BtnPrint_Click;

// Line 443 (handler method)
private void BtnPrint_Click(object? sender, EventArgs e)
```

---

### 5. Transaction Lifecycle Form - Print Button

**File**: `Forms/Transactions/TransactionLifecycleForm.cs`  
**Handler**: `BtnPrint_Click(object? sender, EventArgs e)`  
**Line**: 380  
**Wired**: Line 75  
**Context**: Print button in the Transaction Lifecycle dialog for printing transaction history  
**Current Implementation**: Opens `PrintForm` dialog with transaction lifecycle grid

```csharp
// Line 75
TransactionLifecycleForm_Button_Print.Click += BtnPrint_Click;

// Line 380 (handler method)
private void BtnPrint_Click(object? sender, EventArgs e)
```

---

### 6. Transactions Form - Print Event Handler

**File**: `Forms/Transactions/Transactions.cs`  
**Handler**: `SearchControl_PrintRequested(object? sender, EventArgs e)`  
**Line**: 292  
**Wired**: Lines 57, 62  
**Context**: Centralized print handler for both Transaction Search and Grid controls  
**Current Implementation**: Validates results exist, then opens `PrintForm` with `Transactions_UserControl_Grid.DataGridView`

```csharp
// Line 57 (Search control event wire)
Transactions_UserControl_Search.PrintRequested += SearchControl_PrintRequested;

// Line 62 (Grid control event wire)
Transactions_UserControl_Grid.PrintRequested += SearchControl_PrintRequested;

// Line 292 (handler method)
private void SearchControl_PrintRequested(object? sender, EventArgs e)
{
    LoggingUtility.Log("[Transactions] Print requested.");
    
    // Check if there are results to print
    if (_viewModel.CurrentResults.Transactions.Count == 0)
    {
        LoggingUtility.Log("[Transactions] Print aborted - no results to print.");
        Service_ErrorHandler.HandleValidationError("No transactions to print. Please perform a search first.", "Print");
        return;
    }
    
    LoggingUtility.Log($"[Transactions] Printing {_viewModel.CurrentResults.Transactions.Count} transactions.");
    
    // Open print dialog with the grid's DataGridView
    using var printForm = new Shared.PrintForm(Transactions_UserControl_Grid.DataGridView);
    printForm.ShowDialog(this);
}
```

---

## Additional Print Infrastructure

### Print Form Dialog

**File**: `Forms/Shared/PrintForm.cs` and `PrintForm.Designer.cs`  
**Purpose**: Main print dialog with preview, settings, and column customization  
**Status**: This form will be COMPLETELY REPLACED with new implementation in Phase 3  
**Key Methods**:
- `PrintForm_Button_Print_Click` (Line 271) - Execute print
- `PrintForm_Button_ExportSettings_Click` (Line 300) - Export dropdown menu
- Multiple column management methods (SelectAll, DeselectAll, MoveUp, MoveDown)
- Preview navigation methods (FirstPage, PreviousPage, NextPage, LastPage)
- Zoom methods (ZoomIn, ZoomOut)
- Preset management (SavePreset, DeletePreset)

---

## Supporting Files to be Removed

These files support the current print system and will be deleted in Phase 1 (Task T003):

1. **`Helpers/Helper_PrintExport.cs`**
   - Old export functionality for Excel, PDF, and Image export
   - Will be replaced by new `Helper_ExportManager.cs` (already exists in Phase 2)

2. **`Core/Core_DgvPrinter.cs`**
   - Old DataGridView printer rendering engine
   - Will be replaced by new `Core_TablePrinter.cs` (already exists in Phase 2)

3. **`Forms/Shared/PrintForm.cs` and `PrintForm.Designer.cs`** (OLD VERSION)
   - Old print dialog implementation
   - Will be replaced by completely new PrintForm in Phase 3
   - **Note**: New version already exists, so this refers to removing outdated patterns if any

---

## Keyboard Shortcuts

**Search Query Used**: No keyboard shortcuts (Ctrl+P, etc.) found in current implementation  
**Status**: Keyboard shortcut support (Ctrl+P) will be added in Phase 7 - Task T062

---

## Context Menus

**Search Query Used**: No DataGridView right-click context menu print options found  
**Status**: Context menu "Print..." support will be added in Phase 7 - Task T063

---

## Phase 1 Action Items (T002)

All print button click handlers listed above will be updated to show temporary message:

```csharp
Service_ErrorHandler.ShowInformation(
    "Print functionality is being rebuilt. Coming soon!", 
    "Feature Temporarily Unavailable"
);
```

Old implementation code will be commented out (not deleted) for reference during Phase 7 reconnection.

---

## Phase 7 Reconnection Plan (T061-T063)

All entry points will be reconnected to use new print system:

1. **T061**: Wire Transactions tab print button to `Helper_PrintManager.ShowPrintDialogAsync(dataGridView)`
2. **T062**: Add Ctrl+P keyboard shortcut in `MainForm.cs` using `ProcessCmdKey` override
3. **T063**: Add "Print..." context menu to all DataGridViews

Each entry point handler will follow new pattern:

```csharp
private async void SomeControl_Button_Print_Click(object? sender, EventArgs e)
{
    await Helper_PrintManager.ShowPrintDialogAsync(someDataGridView);
}
```

---

## Validation

**Documentation Complete**: ✅ Yes  
**All Entry Points Documented**: ✅ Yes (6 total)  
**Handler Signatures Captured**: ✅ Yes  
**File Paths Recorded**: ✅ Yes  
**Line Numbers Documented**: ✅ Yes  
**Phase 7 Reconnection Plan**: ✅ Yes

---

**Next Phase**: Task T002 - Replace all handlers with temporary messages
