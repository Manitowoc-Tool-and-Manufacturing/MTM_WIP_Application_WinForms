# Control_AdvancedInventory - Realistic Space-Constrained Redesign

**Document Version:** 2.0  
**Date:** November 15, 2025  
**Status:** Proposal - Realistic Layout  
**Target Control:** `Controls/MainForm/Control_AdvancedInventory.cs`  
**Available Space:** ~731x372 pixels (TabPage client area)

---

## Space Constraint Analysis

### Actual Available Dimensions
```csharp
// From MainForm.Designer.cs:
MainForm_TabPage_Inventory.Size = new Size(731, 372);
MainForm_UserControl_AdvancedInventory.Dock = DockStyle.Fill;
```

**Working Space:** 731 pixels wide × 372 pixels tall  
**Constraints:**
- Must share tab with `Control_InventoryTab` (toggle visibility)
- Docked to fill, no manual positioning
- Must fit within standard laptop screen (1366×768 minimum)
- QuickButtons panel takes ~141px on right (SplitContainer)
- Status strip and menu strip reduce vertical space

**Reality Check:** This is NOT enough space for a wizard with step navigation. Need single-page optimized layout.

---

## Revised Design Philosophy

**Compact Entry Over Wizard**: Single-page interface with smart collapsible sections  
**Visual Density**: Pack more information using data density techniques  
**Progressive Enhancement**: Hide complexity until needed  
**Tab-Based Modes**: Keep existing tab concept but improve each tab's UX  
**Minimize Vertical Scrolling**: Fit primary operations in viewport

---

## Mockup 1: Redesigned "Single Item, Multiple Times" Tab

```
┌──────────────────────────────────────────────────────────────────────┐
│ Single Item, Multiple Times               [Switch to Multi-Location →]│
├──────────────────────────────────────────────────────────────────────┤
│ ┌──────────────────────────────────────────────────┐ ┌─────────────┐│
│ │ Entry Fields                                     │ │ Staged (3)  ││
│ ├──────────────────────────────────────────────────┤ │             ││
│ │ Part:      [R-123-01____________] [F4] 🔍 ✓     │ │ A110146 Op90││
│ │            Part valid • Req. Color Code ⚠️       │ │ Loc:R3 x500 ││
│ │                                                  │ │             ││
│ │ Op:        [90________________] [F4] 🔍 ✓       │ │ A110147 Op90││
│ │            Operation: Assembly                   │ │ Loc:R4 x500 ││
│ │                                                  │ │             ││
│ │ Location:  [A1-B2-R3__________] [F4] 🔍 ✓       │ │ 0K2142 Op19 ││
│ │            Building A1 • Area B2 • Row 3         │ │ Loc:R5 x16  ││
│ │                                                  │ │             ││
│ │ Qty:  [500___]  Count: [1___]  ✓ Valid          │ │ Total: 1016 ││
│ │                                                  │ │             ││
│ │ Notes: ┌──────────────────────────────────────┐ │ │[Edit]       ││
│ │        │ Received batch #12345                │ │ │[Remove]     ││
│ │        └──────────────────────────────────────┘ │ │[Clear All]  ││
│ │                                                  │ │             ││
│ │ [Send to List →] [Reset] [⚙ Advanced Options]  │ │[💾 Save All]││
│ └──────────────────────────────────────────────────┘ └─────────────┘│
│                                                                      │
│ ℹ️ Tips: F4=Browse • %= Wildcard • Ctrl+S=Save • Shift+Reset=Hard  │
└──────────────────────────────────────────────────────────────────────┘
```

**Key Changes:**
- **Two-column layout**: Entry form (left 70%) + staged list (right 30%)
- **Inline validation**: Status icons next to each field (✓ ⚠️ ❌)
- **Contextual hints**: Show "Req. Color Code" inline, not modal
- **Compact staged list**: Preview with edit/remove, scrolls if needed
- **Collapsible advanced**: Hide Excel import behind "Advanced Options"
- **Persistent tips bar**: Bottom hint bar for keyboard shortcuts

### Field Layout Details

```
Part Field Stack (vertical):
┌─────────────────────────────────────────────┐
│ [Label] Part:  [TextBox________] [F4] [🔍] │  ← 24px height
│ 💡 Part valid • Req. Color Code ⚠️          │  ← 18px status line
└─────────────────────────────────────────────┘
Total: ~42px per field with status
```

**Space Budget:**
- Header: 30px
- 4 fields × 42px = 168px
- Qty/Count row: 40px
- Notes: 60px
- Buttons: 35px
- Tips bar: 25px
- **Total:** ~358px (fits in 372px with 14px margin)

---

## Mockup 2: Redesigned "Same Item, Multiple Locations" Tab

```
┌──────────────────────────────────────────────────────────────────────┐
│ Same Item, Multiple Locations              [Switch to Single Item →] │
├──────────────────────────────────────────────────────────────────────┤
│ ┌──────────────────────────────────────────┐ ┌────────────────────┐ │
│ │ Base Transaction (Locked after 1st loc)  │ │ Locations (3)      │ │
│ ├──────────────────────────────────────────┤ │ Total Qty: 1100    │ │
│ │ Part: A110146  ✓ [🔒 Change] │ Op: 90 ✓ │ ├────────────────────┤ │
│ └──────────────────────────────────────────┘ │ ☑ A1-B2-R3   500   │ │
│                                              │ ☑ A1-B2-R4   500   │ │
│ ┌──────────────────────────────────────────┐ │ ☑ A1-B2-R5   100   │ │
│ │ Add Location                             │ │                    │ │
│ ├──────────────────────────────────────────┤ │ [Edit Sel] [Del]   │ │
│ │ Location: [A1-B2-R6_______] [F4] 🔍     │ └────────────────────┘ │
│ │           Ready to add ✓                 │                        │
│ │                                          │ ┌────────────────────┐ │
│ │ Quantity: [250____] ✓ Valid              │ │ Preview Summary    │ │
│ │                                          │ ├────────────────────┤ │
│ │ Notes:    [Shelf 4_________________]     │ │ Part: A110146      │ │
│ │                                          │ │ Op:   90 Assembly  │ │
│ │ [+ Add to List] [Reset Fields]           │ │ Locs: 3            │ │
│ └──────────────────────────────────────────┘ │ Qty:  1,100        │ │
│                                              │                    │ │
│ [Clear All Locations] [💾 Save Batch]        │ [Review] [Export]  │ │
│                                              └────────────────────┘ │
│ ℹ️ Lock prevents accidental part/op change • Shift+Reset=Hard Clear│
└──────────────────────────────────────────────────────────────────────┘
```

**Key Changes:**
- **Visual lock indicator**: Shows Part/Op as locked with "🔒 Change" button
- **Checkbox selection**: Can select multiple locations for batch delete
- **Right panel split**: Location list + summary card
- **Inline add form**: No modal, just add fields at top of left panel
- **Summary card**: Quick stats about the batch operation

---

## Mockup 3: Excel Import Tab (Collapsed by Default)

```
┌──────────────────────────────────────────────────────────────────────┐
│ Import from Excel                       [Switch to Manual Entry →]   │
├──────────────────────────────────────────────────────────────────────┤
│ ┌────────────────────────────────────────────────────────────────────┤
│ │ Step 1: Manage Excel File                                         │
│ ├────────────────────────────────────────────────────────────────────┤
│ │ Template: C:\...\JOHNK_import.xlsx                                │
│ │ Status: ✓ File exists • Modified: 11/15/2025 10:25 AM • 47 rows  │
│ │                                                                    │
│ │ [📂 Open in Excel] [📥 Import Now] [📄 Download Fresh Template]   │
│ └────────────────────────────────────────────────────────────────────┤
│                                                                      │
│ ┌────────────────────────────────────────────────────────────────────┤
│ │ ▼ Validation Results (Click to expand)               ⚠️ Issues: 5  │
│ └────────────────────────────────────────────────────────────────────┤
│                                                                      │
│ ┌────────────────────────────────────────────────────────────────────┤
│ │ Data Preview (42 valid • 3 warnings • 2 errors)                   │
│ ├────┬─────────┬──────┬──────────┬─────┬────────────────────────────┤
│ │ ✓  │ A110146 │  90  │ A1-B2-R3 │ 500 │                            │
│ │ ✓  │ A110147 │  90  │ A1-B2-R4 │ 500 │                            │
│ │ ❌ │ XYZ123  │  90  │ A1-B2-R5 │ 100 │ Part not found             │
│ │ ⚠️  │ 0K2142  │ 999  │ Z9-X1    │  16 │ Op uncommon, Loc not cached│
│ │ ... (scroll for 43 more rows)                                     │
│ └────┴─────────┴──────┴──────────┴─────┴────────────────────────────┤
│                                                                      │
│ [Skip Invalid Rows & Save 42] [Fix in Excel & Re-import] [Cancel]   │
│                                                                      │
│ ℹ️ Click row for details • Green=Valid • Yellow=Warning • Red=Error │
└──────────────────────────────────────────────────────────────────────┘
```

**Key Changes:**
- **Collapsible sections**: Validation hidden until import triggered
- **Status-based coloring**: Visual row highlighting (green/yellow/red)
- **Action buttons**: Clear next steps (skip invalid vs fix in Excel)
- **Summary counts**: Show valid/warning/error counts prominently
- **File status**: Shows last modified time and row count before import

---

## Mockup 4: Advanced Options Flyout (Overlay)

When user clicks "[⚙ Advanced Options]" button:

```
┌──────────────────────────────────────────────────────────────────────┐
│ Single Item, Multiple Times               [Switch to Multi-Location →]│
├──────────────────────────────────────────────────────────────────────┤
│ ┌──────────────────────────────┐ ┌─────────────┐ ╔════════════════╗│
│ │ Part:      [R-123-01____] ✓ │ │ Staged (3)  │ ║ Adv. Options   ║│
│ │ Op:        [90__________] ✓ │ │             │ ║                ║│
│ │ Location:  [A1-B2-R3____] ✓ │ │ A110146 ...│ ║ ☐ Auto-incr Loc║│
│ │ Qty:  [500]  Count: [1]  ✓  │ │ A110147 ...│ ║ ☐ Email on save║│
│ │ Notes: ┌──────────────────┐  │ │ 0K2142 ... │ ║ ☐ Print summary║│
│ │        │...               │  │ │             │ ║                ║│
│ │        └──────────────────┘  │ │ [Edit]      │ ║ Default Qty:   ║│
│ │                              │ │ [Remove]    │ ║ [100_____]     ║│
│ │ [Send→] [Reset] [⚙Options]  │ │ [Clear All] │ ║                ║│
│ └──────────────────────────────┘ │             │ ║ [Apply] [Close]║│
│                                  │ [Save All]  │ ╚════════════════╝│
│ ℹ️ Tips: F4=Browse • %= Wildcard└─────────────┘                    │
└──────────────────────────────────────────────────────────────────────┘
```

**Flyout Panel:**
- Slides in from right side (250px width)
- Semi-transparent backdrop dims main content
- Contains advanced features:
  - Auto-increment location numbers
  - Email notification on save
  - Print transaction summary
  - Default quantity preset
  - Batch operation templates

---

## Tab Structure Comparison

### Current Tabs (3 separate)
1. Single Item, Multiple Times
2. Same Item, Multiple Locations  
3. Import Excel (Under Construction)

### Proposed Tabs (3 enhanced)
1. **Quick Entry** (Single item mode - enhanced)
2. **Batch Entry** (Multi-location mode - enhanced)
3. **Excel Import** (Validation-first approach)

**Toggle Pattern:**
- Quick Entry ↔ Batch Entry via header button
- Excel Import separate tab (less frequent use)
- All modes use SuggestionTextBox for Part/Op/Location

---

## UI Component Breakdown

### SuggestionTextBox Integration

**Replace ALL ComboBox instances:**
```csharp
// Old (6 ComboBoxes):
AdvancedInventory_Single_ComboBox_Part
AdvancedInventory_Single_ComboBox_Op
AdvancedInventory_Single_ComboBox_Loc
AdvancedInventory_MultiLoc_ComboBox_Part
AdvancedInventory_MultiLoc_ComboBox_Op
AdvancedInventory_MultiLoc_ComboBox_Loc

// New (6 SuggestionTextBoxes):
AdvancedInventory_Single_SuggestionTextBox_Part
AdvancedInventory_Single_SuggestionTextBox_Op
AdvancedInventory_Single_SuggestionTextBox_Loc
AdvancedInventory_MultiLoc_SuggestionTextBox_Part
AdvancedInventory_MultiLoc_SuggestionTextBox_Op
AdvancedInventory_MultiLoc_SuggestionTextBox_Loc
```

### Status Label Pattern

**Add inline validation labels:**
```csharp
// Under each SuggestionTextBox:
AdvancedInventory_Single_Status_Part       // "✓ Part valid • Req. Color Code ⚠️"
AdvancedInventory_Single_Status_Op         // "✓ Operation: Assembly"
AdvancedInventory_Single_Status_Loc        // "✓ Building A1 • Area B2 • Row 3"
AdvancedInventory_Single_Status_Qty        // "✓ Valid quantity"
```

**Status Label Properties:**
- Font: Segoe UI, 8pt
- Height: 18px
- ForeColor: Dynamic (Green=#28a745, Yellow=#ffc107, Red=#dc3545)
- Text alignment: Left
- Margin: 2px top

### ListView Enhancement

**Replace basic ListView with details:**
```csharp
// Column headers for Single mode:
Part | Op | Location | Qty | Notes | Status

// Column headers for Multi mode:
☑ | Location | Qty | Notes | Status

// Features:
- Checkboxes for multi-select
- Inline editing (double-click row)
- Color-coded rows (green/yellow/red)
- Context menu (Edit/Delete/Copy)
- Footer row with totals
```

### Button Layout

**Optimize button placement:**
```
Left-aligned action buttons:
[Primary Action] [Secondary] [Tertiary]

Right-aligned utility:
                        [Advanced ▼] [Help ?]

Status/summary area between them
```

---

## Color-Code Warning Integration

### Inline Warning Banner

**Instead of modal dialog:**
```
┌────────────────────────────────────────────────────────┐
│ Part: [R-123-01____________] [F4] 🔍 ✓                │
│ ⚠️  This part requires COLOR CODE • [Switch to Inventory Tab →] │
└────────────────────────────────────────────────────────┘
```

**Banner Properties:**
- Background: #fff3cd (warning yellow)
- Border: 1px solid #ffc107
- Icon: ⚠️ or warning icon
- Action button: "Switch to Inventory Tab →"
- Dismissible: X button on right
- Only shows when part requires color code

**Click behavior:**
```csharp
private void ColorCodeBanner_Click(object sender, EventArgs e)
{
    // Auto-switch to inventory tab
    MainFormInstance.MainForm_TabControl.SelectedIndex = 0;
    MainFormInstance.MainForm_UserControl_InventoryTab.Visible = true;
    MainFormInstance.MainForm_UserControl_AdvancedInventory.Visible = false;
    
    // Pre-populate part
    var invTab = MainFormInstance.MainForm_UserControl_InventoryTab;
    invTab.Control_InventoryTab_TextBox_Part.Text = selectedPart;
    invTab.Control_InventoryTab_TextBox_Part.Focus();
}
```

---

## Space-Saving Techniques

### 1. Collapsible Sections
```
▼ Advanced Options (Click to expand)
▶ Import from Excel (Click to expand)
▼ Transaction History (3 recent - Click to expand)
```

### 2. Tabbed Sub-Panels
```
┌─────────────────────────────────┐
│ [Recent] [Templates] [Settings] │
├─────────────────────────────────┤
│ (Context-based content)         │
└─────────────────────────────────┘
```

### 3. Flyout Panels
- Slide from right edge
- Overlay main content
- Semi-transparent backdrop
- Close on click outside

### 4. Tooltips for Details
- Hover over status icons for full message
- Hover over field labels for help text
- Hover over buttons for keyboard shortcuts

### 5. Compact Fonts
- Labels: Segoe UI 9pt
- Input fields: Segoe UI 10pt
- Status text: Segoe UI 8pt
- Headers: Segoe UI Semibold 10pt

---

## Keyboard Shortcuts

### Global Shortcuts
- **F4**: Open suggestion list (any SuggestionTextBox)
- **Ctrl+S**: Save all staged items
- **Ctrl+R**: Reset current form
- **Ctrl+N**: Focus first field (Part)
- **Escape**: Clear current field or cancel operation
- **Tab**: Navigate fields in logical order

### Context-Specific
- **Shift+Reset**: Hard reset (reload data from database)
- **Ctrl+E**: Open Excel file (Import tab)
- **Ctrl+I**: Trigger import (Import tab)
- **Ctrl+Del**: Remove selected items from list
- **Ctrl+A**: Select all in list

### Field Navigation
```
Part → Op → Location → Qty → Count → Notes → [Send to List]
                                           ↓
[Tab cycles back to Part]              [Staged List]
```

---

## Validation Strategy

### Real-Time Validation

**As user types:**
1. Debounce input (300ms delay)
2. Check against cache
3. Update status label
4. Change field border color
5. Enable/disable action buttons

**Validation States:**
```csharp
public enum ValidationState
{
    Pending,    // ⏳ Waiting for input
    Valid,      // ✓ Green - good to go
    Warning,    // ⚠️ Yellow - usable but check
    Invalid     // ❌ Red - cannot proceed
}
```

### Status Label Content Examples

```
✓ Part valid                              (Green)
✓ Part valid • Req. Color Code ⚠️         (Yellow - warning)
❌ Part not found                          (Red)
⏳ Checking part...                        (Gray - pending)

✓ Operation: Assembly                     (Green)
⚠️ Operation '999' uncommon - confirm?    (Yellow)
❌ Operation does not exist                (Red)

✓ Building A1 • Area B2 • Row 3           (Green)
⚠️ Location not in cache - will create    (Yellow)
❌ Invalid location format                 (Red)

✓ Valid quantity                          (Green)
❌ Quantity must be > 0                    (Red)
```

---

## Data Flow Architecture

### Suggestion Data Loading

```csharp
// On form load (async):
1. Initialize Helper_SuggestionTextBox cache
2. Configure all SuggestionTextBox controls
3. Wire up SuggestionSelected events
4. Set initial focus

// On suggestion selected:
1. SuggestionTextBox fires SuggestionSelected event
2. Validate selection against master data
3. Update status label with details
4. Enable/disable downstream fields
5. Check for color-code requirement
6. Update button states
```

### Save Operation Flow

```csharp
// Single mode save:
1. Validate all staged items
2. Show progress bar
3. Loop through ListView items
4. Call Dao_Inventory.AddInventoryItemAsync for each
5. Remove successful rows from ListView
6. Highlight failed rows in red
7. Show summary dialog (X saved, Y failed)
8. Update status strip

// Multi mode save:
1. Validate Part/Op (locked)
2. Validate all locations in preview list
3. Show confirmation with totals
4. Batch insert with progress updates
5. Clear successful, highlight failures
6. Show summary dialog
```

---

## Implementation Checklist

### Phase 1: Foundation (Week 1)
- [ ] Replace all 6 ComboBoxes with SuggestionTextBoxes
- [ ] Add status labels under each input field
- [ ] Configure Helper_SuggestionTextBox for Part/Op/Location
- [ ] Wire up SuggestionSelected events

### Phase 2: Validation (Week 2)
- [ ] Implement real-time validation logic
- [ ] Add status label update methods
- [ ] Create color-code detection banner
- [ ] Update button enable/disable logic

### Phase 3: Single Mode Enhancement (Week 3)
- [ ] Redesign single-item tab layout
- [ ] Add two-column split (entry + staged list)
- [ ] Enhance ListView with checkboxes and context menu
- [ ] Add inline editing to staged items
- [ ] Create advanced options flyout panel

### Phase 4: Multi Mode Enhancement (Week 4)
- [ ] Redesign multi-location tab layout
- [ ] Add visual lock indicators for Part/Op
- [ ] Create summary card panel
- [ ] Add batch selection and delete
- [ ] Implement preview totals

### Phase 5: Excel Import (Week 5)
- [ ] Create validation-first import workflow
- [ ] Add pre-import validation preview
- [ ] Color-code rows by validation status
- [ ] Add "Skip invalid" vs "Fix in Excel" options
- [ ] Implement inline error details

### Phase 6: Polish (Week 6)
- [ ] Add keyboard shortcuts
- [ ] Create tooltip help system
- [ ] Implement collapsible sections
- [ ] Add session persistence (remember last values)
- [ ] Performance testing and optimization

---

## Technical Specifications

### Control Sizing

```csharp
// Tab client area
Width: 731px
Height: 372px

// Two-column split
Left panel: 510px (70%)
Right panel: 210px (30%)
Splitter: 5px

// Field heights
SuggestionTextBox: 24px
Status label: 18px
Field spacing: 6px
Field stack total: 48px

// ListView
Min height: 120px
Max height: fills remaining space
Row height: 22px
```

### Color Scheme

```csharp
// Validation colors
Valid: #28a745 (green)
Warning: #ffc107 (yellow/gold)
Invalid: #dc3545 (red)
Pending: #6c757d (gray)

// Background colors
Primary bg: SystemColors.Window
Secondary bg: #f8f9fa
Panel bg: SystemColors.Control
Hover: #e9ecef

// Border colors
Default: #ced4da
Focus: #80bdff
Valid: #28a745
Invalid: #dc3545
```

### Font Specifications

```csharp
// Typography
Headers: Segoe UI Semibold, 10pt
Labels: Segoe UI, 9pt
Inputs: Segoe UI, 10pt
Status: Segoe UI, 8pt
Monospace: Consolas, 9pt (for part numbers)

// Icon fonts
Material Icons or Segoe UI Emoji for ✓ ⚠️ ❌ 🔒 🔍
```

---

## Performance Targets

### Load Time
- Form load to ready: < 500ms
- Suggestion cache initialization: < 200ms
- Tab switch: < 100ms

### Responsiveness
- Suggestion filter: < 50ms
- Validation check: < 100ms
- Status label update: < 50ms
- ListView refresh: < 100ms

### Save Operations
- Single transaction: < 500ms
- Batch (10 items): < 2 seconds
- Batch (50 items): < 8 seconds
- Excel import (100 rows): < 10 seconds

---

## Migration Strategy

### Backward Compatibility

**Preserve all existing methods:**
- Don't delete ComboBox helper methods (used by other controls)
- Keep existing DAO signatures unchanged
- Maintain status strip integration
- Preserve MainFormInstance references

**New helper methods needed:**
```csharp
Helper_SuggestionTextBox.GetCachedPartNumbersAsync()
Helper_SuggestionTextBox.GetCachedOperationsAsync()
Helper_SuggestionTextBox.GetCachedLocationsAsync()
Helper_SuggestionTextBox.ValidateSelectionAsync(control, "Part")
Helper_SuggestionTextBox.RefreshCacheAsync(cacheType)
```

### Gradual Rollout

1. **Week 1-2**: Implement new control side-by-side (visible:false)
2. **Week 3**: A/B test with select users
3. **Week 4**: Feature flag toggle (users can switch back)
4. **Week 5**: Default to new UI, old UI available via settings
5. **Week 6**: Deprecate old UI, remove ComboBox dependencies

---

## User Testing Plan

### Test Scenarios

**Scenario 1: Quick single entry**
- Time to add one inventory item (target: < 15 seconds)
- Errors made during entry (target: < 0.5 per entry)

**Scenario 2: Batch multi-location**
- Time to add 1 part to 5 locations (target: < 30 seconds)
- Confusion about locked fields (target: 0 users confused)

**Scenario 3: Excel import**
- Time to identify and fix validation errors (target: < 2 minutes for 5 errors)
- Successful import on first try (target: > 80% of users)

### Success Metrics

**Quantitative:**
- 25% reduction in time-per-transaction
- 50% reduction in validation errors at save time
- 75% reduction in "part not found" errors
- 30% increase in Excel import first-pass success

**Qualitative:**
- User reports "easier to understand"
- Support tickets decrease by 40%
- Training time for new users reduces by 50%
- Positive feedback score > 4/5

---

## Open Questions & Decisions Needed

1. **Color-code redirect**: Inline banner vs dismissible modal?
   - **Recommendation**: Inline banner (less disruptive)

2. **Staged list editing**: Double-click row vs Edit button?
   - **Recommendation**: Both (double-click for power users, button for discoverability)

3. **Default quantity**: Should it remember last-used value?
   - **Recommendation**: Yes, per session (cleared on app restart)

4. **Excel template**: Auto-download on first use or require manual download?
   - **Recommendation**: Auto-download with notification

5. **Lock change behavior**: Confirm dialog or instant clear?
   - **Recommendation**: Confirm dialog with summary of what will be cleared

---

## Appendix: ASCII Layout Templates

### Compact Two-Column Pattern
```
┌──────────────────────────┬───────────────┐
│ Primary Content (70%)    │ Secondary (30%)│
│                          │               │
│ [Input fields]           │ [List/Summary]│
│ [Input fields]           │ [Actions]     │
│ [Input fields]           │               │
│                          │               │
│ [Action buttons]         │ [Save button] │
└──────────────────────────┴───────────────┘
```

### Collapsible Section Pattern
```
▼ Section Title (Expanded)        [Collapse ▲]
┌────────────────────────────────────────────┐
│ Section content visible                    │
│ ...                                        │
└────────────────────────────────────────────┘

▶ Section Title (Collapsed)       [Expand ▼]
```

### Status Label Pattern
```
Input Field:
[TextBox_________________] [F4] [🔍]
✓ Status message in green with context
```

---

**End of Realistic Redesign Document**
