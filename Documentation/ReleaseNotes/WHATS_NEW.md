# What's New - MTM WIP Application

> **Latest updates for shop floor and office staff**  
> Last Updated: November 13, 2025

---

## 📋 Recent Releases

| Version | Date | Main Features | Action Needed? |
|---------|------|---------------|----------------|
| [6.2.1](#version-621-november-13-2025) | Nov 13 | Startup shortcuts, Help page | ℹ️ Optional |
| [6.2.0](#version-620-november-13-2025) | Nov 13 | Smart suggestions, Confirmations | ✅ Automatic |
| [6.1.0](#version-610-november-12-2025) | Nov 12 | Instant theme changes | ✅ Automatic |
| [6.0.2](#version-602-november-8-2025) | Nov 8 | Theme saving fix, QuickButtons | ✅ Recommended |
| [6.0.1](#version-601-november-8-2025) | Nov 8 | QuickButtons reliability | ✅ Recommended |

[View All Release History →](RELEASE_HISTORY.md) | [Common Questions →](FAQ.md)

---

## Version 6.2.1 (November 13, 2025)

**Summary**: Launch the app directly into Production or Test environments using desktop shortcuts.

### 🎯 What's New

#### Startup Environment Selection
Create shortcuts that launch directly into Production or Test mode without changing settings each time.

**Benefits:**
- Switch between environments without manual configuration
- Set custom username for shared workstations (better log tracking)
- Safer training/demo setups with dedicated test shortcuts

**How to use:**
1. Right-click desktop → New Shortcut
2. Point to the MTM WIP Application executable
3. Add arguments to the Target field (see Help → "Startup Arguments")
4. Create separate shortcuts for Production and Test if needed

**Examples:**
- Production mode: Add `-db=prod` to shortcut target
- Test mode: Add `-db=test` to shortcut target  
- Custom user: Add `-user="John Smith"` to shortcut target

📖 **Full instructions**: Press F1 in the app → Search "Startup Arguments"

⚠️ **Security note**: Don't include passwords in shortcuts on shared computers

---

---

## Version 6.2.0 (November 13, 2025)

> ⚠️ **DEVELOPMENT STATUS**: This version describes features currently in development.  
> The `SuggestionTextBox` control exists in the codebase but is **NOT YET INTEGRATED** into production forms.  
> Features described below are planned but not yet available to users.  
> **Current Status**: Code complete, integration pending.

**Summary**: Planned usability improvements including type-to-search suggestions and confirmation dialogs (implementation in progress).

### 🎯 What's New

#### 1. Smart Type-to-Search Fields (Planned)

> **Implementation Note**: The `SuggestionTextBox` control has been implemented in `Controls/Shared/SuggestionTextBox.cs`  
> but is not yet integrated into Inventory, Transfer, or Remove tabs. Integration work is ongoing.

**What's planned:**
Old dropdown lists (500+ parts to scroll through) → New autocomplete suggestions (type to filter instantly)

**Where it will work:**
- **Inventory Tab**: Part Number, Operation, Location (pending integration)
- **Transfer Tab**: Part Number, Operation, To Location (pending integration)
- **Remove Tab**: Part Number, Operation (pending integration)
- **QuickButton Editor**: Part Number, Operation (pending integration)

**How to use:**
1. Start typing in any field (e.g., type "R-" in Part Number) then hit [TAB] or [ENTER]
2. Matching suggestions appear automatically
3. Use arrow keys to navigate, Enter to select, Escape to cancel
4. Or just type the exact Part Number, Operation, Location or User Name and press Tab (auto-validates)

**Keyboard shortcuts:**
- **↑/↓ arrows**: Navigate suggestions
- **Home/End**: Jump to first/last
- **Enter**: Select highlighted item
- **Escape**: Cancel, keep what you typed
- **Double-click**: Select with mouse

**Expected benefits (when integrated):**
- **3x faster**: Find parts in seconds, not minutes
- **5x faster Tab loading**: No need to reload dropdown menus with 1000+ items
- **Zero typos**: Only valid codes accepted
- **Power user friendly**: Type exact codes and tab through without waiting

---

---

## Version 6.1.0 (November 12, 2025)

**Summary**: Complete theme system overhaul - instant updates, live preview, 3x faster performance.

### 🎯 What's New

#### Lightning-Fast Theme Switching

**What changed:**
- Theme updates now happen in **under 100ms** (was 300-500ms)
- **All open windows update simultaneously** when you change themes
- **Live preview** in Settings → Theme tab (no need to save to preview)
- **Zero flicker** - smooth transitions without screen redraw

**How to try it:**
1. Open **Settings** → **Theme** tab
2. Keep Settings open, also open Transaction Viewer or Inventory
3. Click through different themes in the dropdown
4. **Watch both windows update instantly**
5. Click **Save** when you find one you like

**Available themes:**
- Arctic, Default, Fire Storm, Glacier, Ice, Lavender, Midnight, Neon, Sunset, Wood

**Benefits:**
- **Personalize faster**: Try multiple themes in seconds
- **Less disruption**: No need to close your work to see changes
- **Better ergonomics**: Switch between high/low contrast for different lighting
- **Memory efficient**: 10% less memory usage, no memory leaks

---

---

## Version 6.0.2 (November 8, 2025)

**Summary**: Theme selection now saves correctly + QuickButtons reliability improvements.

### 🎯 What's New

#### Theme Selection Finally Saves

**What's fixed:**
- Theme choices now **persist across restarts** (was reverting to Lavender)
- Settings saved to correct location in database
- Error recovery if theme can't load (uses safe default)

**How to change theme:**
1. Settings → Theme tab
2. Pick a theme
3. Click Save
4. ✅ Theme now remembered on next login

---

#### QuickButtons List Cleanup

**What's fixed:**
- Duplicates removed automatically on app restart
- List behaves reliably without position shifts

---

---

## Version 6.0.1 (November 8, 2025)

**Summary**: QuickButtons now 100% reliable - no duplicates, proper editing, correct display.

### 🎯 What's New

#### QuickButtons Complete Fix

**What's fixed:**
- ✅ **No more duplicates** - Same part/operation won't appear multiple times
- ✅ **Proper display** - Clicking buttons fills fields with full descriptions (not raw codes)
- ✅ **Smart edit dialog** - Right-click Edit uses autocomplete dropdowns
- ✅ **Newest at top** - New QuickButtons appear at top of list (was going to bottom)
- ✅ **Data validation** - Edit dialog only accepts valid Part IDs/Operations

**How to edit QuickButtons:**
1. Right-click any QuickButton
2. Select "Edit"
3. Modify Part ID, Operation, or Quantity
4. Click OK to save

**How to reorder:**
1. Right-click any QuickButton  
2. Select "Change Order"
3. Drag rows or use Shift+Up/Down arrows
4. Click OK to save

**Benefits:**
- **Clean list**: No duplicates cluttering your buttons
- **Accurate data**: Fields populate correctly on first click
- **Easy maintenance**: Edit with same autocomplete as main tabs
- **Smart organization**: Most recent at top where you need them

---

---

## 🆘 Need Help?

**Questions**: Contact John Koll (ext. 323) | Dan Smith (ext. 311) | Ka Lee (ext. TBD)  
**Found a bug**: Use the "Report Issue" button in error messages  
**Can't login**: Contact IT immediately

---

## 📚 More Information

- [Full Release History](RELEASE_HISTORY.md) - All versions since October 2025
- [Should I Update?](UPGRADE_GUIDE.md) - Decision guide for each version
- [Common Questions](FAQ.md) - Frequently asked questions
- [Developer Changelog](DEVELOPER_CHANGELOG.md) - Technical details for IT/developers

---

**Document Version**: 1.0  
**Last Updated**: November 13, 2025
