# Quickstart: Shortcuts UI

## Overview
The Shortcuts UI allows users to view and customize keyboard shortcuts for the application. It is located in the Settings menu.

## Usage

### Viewing Shortcuts
1. Navigate to **Settings** > **Shortcuts**.
2. Shortcuts are grouped by category (e.g., Inventory, General).
3. Click a category header to expand or collapse it.

### Changing a Shortcut
1. Locate the shortcut you want to change.
2. Click the **Change** button next to it.
3. A dialog will appear. Press the new key combination on your keyboard.
4. Click **Save**.
   - If the key is already in use, an error message will appear.
   - If the key is reserved (e.g., Alt+1 for QuickButtons), an error message will appear.

### Resetting Defaults
1. Click the **Reset to Defaults** button at the bottom of the page (if available, or per shortcut if implemented). *Note: Global reset is usually a separate action.*

## Developer Notes

### Adding a New Shortcut
1. Add the shortcut definition to `Resources/default_shortcuts.json`.
2. Use `Service_Shortcut.GetShortcutKey("Your_Shortcut_Name")` in your code.
3. Do **not** hardcode `Keys.SomeKey`.

### QuickButtons
- QuickButtons 1-10 are reserved as `Alt+1` through `Alt+0`.
- These cannot be reassigned to other functions.
