# Research: Centralize DataGridView Logic

**Feature**: Centralize DataGridView Logic
**Status**: Complete
**Date**: 2025-11-22

## Decisions

### 1. Service Architecture
- **Decision**: Create a static class `Service_DataGridView` in `Services` namespace.
- **Rationale**: Matches existing service patterns (`Service_ErrorHandler`, `Service_Validation`). Static methods are appropriate for stateless utility functions that operate on passed-in controls.
- **Alternatives**:
  - *Extension Methods*: Could define extensions on `DataGridView`, but a dedicated service is more discoverable and aligns with the "Service_" naming convention used in the project.
  - *Base Class*: Creating a `BaseDataGridView` subclass. Rejected because we want to apply logic to existing `DataGridView` controls (often created by designer) without changing their type in the designer code, which can be risky.

### 2. Column Configuration
- **Decision**: `ConfigureColumns` will take a `DataGridView` and a list of visible column names. It will handle:
  - Hiding columns not in the list.
  - Setting `DisplayIndex` based on the list order.
  - Renaming headers (optional dictionary).
- **Rationale**: This encapsulates the repetitive loop logic found in all controls.

### 3. Theming & Settings
- **Decision**: `ApplyStandardSettingsAsync` will wrap `Core_Themes.ApplyThemeToDataGridView` and `Core_Themes.LoadAndApplyGridSettingsAsync`.
- **Rationale**: Ensures these two are always called together and in the correct order (theme first, then user settings).

### 4. Color Coding
- **Decision**: Centralize the color list and application logic in `Service_DataGridView`.
- **Rationale**: The color list (Red, Blue, Green, etc.) and the logic to apply `DefaultCellStyle.BackColor` is identical in Transfer and Remove tabs. Centralizing it prevents drift.
- **Implementation**:
  - `ApplyInventoryColorCoding(DataGridView dgv)`: Checks for "ColorCode" column.
  - `SortInventoryByColorPriority(DataTable dt)`: Implements the custom sorting (ColorCode priority + Location).

### 5. Printing
- **Decision**: `PrintGridAsync` will handle the "No records" validation and call `Helper_PrintManager.ShowPrintDialogAsync`.
- **Rationale**: Every control currently repeats the `if (Rows.Count == 0)` check. Centralizing this ensures consistent validation messages.

## Unknowns & Clarifications

- **Resolved**: `Core_Themes` signatures confirmed.
- **Resolved**: `Helper_PrintManager` signature confirmed.
- **Resolved**: Color coding logic confirmed (standard set of colors, specific sorting rules).

## Dependencies

- `Core_Themes`: For theming and settings.
- `Helper_PrintManager`: For printing.
- `Service_ErrorHandler`: For error display.
- `LoggingUtility`: For logging.
- `Dao_User`: For loading settings (via Core_Themes).
