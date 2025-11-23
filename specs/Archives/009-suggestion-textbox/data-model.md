# Phase 1: Data Model & Component Design

**Feature**: Universal Suggestion System for TextBox Inputs  
**Date**: November 12, 2025  
**Status**: Completed

## Component Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Form/UserControl                          │
│  ┌────────────────────────────────────────────────────────┐ │
│  │         SuggestionTextBox (inherits TextBox)           │ │
│  │  ┌──────────────────────────────────────────────────┐  │ │
│  │  │  Properties:                                      │  │ │
│  │  │  - DataProvider: Func<Task<List<string>>>       │  │ │
│  │  │  - MaxResults: int                               │  │ │
│  │  │  - EnableWildcards: bool                         │  │ │
│  │  │  - ShowLoadingIndicator: bool                    │  │ │
│  │  └──────────────────────────────────────────────────┘  │ │
│  │                      │                                  │ │
│  │                      │ OnLostFocus                      │ │
│  │                      ▼                                  │ │
│  │  ┌──────────────────────────────────────────────────┐  │ │
│  │  │  Service_SuggestionFilter                        │  │ │
│  │  │  - FilterSuggestions()                           │  │ │
│  │  │  - ConvertWildcardToRegex()                      │  │ │
│  │  │  - SortByRelevance()                             │  │ │
│  │  └──────────────────────────────────────────────────┘  │ │
│  │                      │                                  │ │
│  │                      │ Filtered List                    │ │
│  │                      ▼                                  │ │
│  │  ┌──────────────────────────────────────────────────┐  │ │
│  │  │  SuggestionOverlayForm (inherits ThemedForm)     │  │ │
│  │  │  - suggestionListBox                             │  │ │
│  │  │  - lblMatchCount                                 │  │ │
│  │  │  - ShowDialog() - MODAL                          │  │ │
│  │  └──────────────────────────────────────────────────┘  │ │
│  │            │                    │                       │ │
│  │      Enter/DoubleClick      Escape/ClickOutside        │ │
│  │            │                    │                       │ │
│  │            ▼                    ▼                       │ │
│  │  ┌─────────────────┐   ┌─────────────────┐            │ │
│  │  │ Select & Accept │   │ Cancel & Keep   │            │ │
│  │  └─────────────────┘   └─────────────────┘            │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  Data Flow:                                                  │
│  1. User types text                                          │
│  2. User tabs away (LostFocus)                               │
│  3. DataProvider invoked -> List<string>                     │
│  4. Service_SuggestionFilter filters + sorts                 │
│  5. SuggestionOverlayForm displays (ShowDialog)              │
│  6. User selects or cancels                                  │
│  7. SuggestionTextBox updates Text + fires event             │
│  8. Focus moves to next control (if accepted)                │
└─────────────────────────────────────────────────────────────┘
```

## Entity Definitions

### 1. SuggestionTextBox (Control)

**Purpose**: Enhanced TextBox that triggers suggestion overlay on LostFocus event

**Base Class**: `System.Windows.Forms.TextBox`

**Properties**:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| DataProvider | Func<Task<List<string>>> | null | Async delegate that returns master data strings |
| MaxResults | int | 100 | Maximum suggestions to display |
| EnableWildcards | bool | true | Allow % wildcard pattern matching |
| ShowLoadingIndicator | bool | true | Display loading overlay for slow data sources |
| SuppressExactMatch | bool | true | Don't show overlay if user typed exact match |
| ClearOnInvalid | bool | true | Clear field when no matches found (MTM pattern) |

**Events**:

| Event | Args | When Fired |
|-------|------|------------|
| SuggestionSelected | SuggestionSelectedEventArgs | User accepts selection (Enter/DoubleClick) |
| SuggestionCancelled | SuggestionCancelledEventArgs | User cancels (Escape/ClickOutside) |
| SuggestionOverlayOpened | EventArgs | Overlay displayed |
| SuggestionOverlayClosed | EventArgs | Overlay closed (any reason) |

**Methods**:

| Method | Return | Description |
|--------|--------|-------------|
| ShowSuggestionsAsync() | Task | Manually trigger suggestion display |
| RefreshDataSource() | void | Clear cached data (if caching implemented) |

**State**:

| Field | Type | Description |
|-------|------|-------------|
| _currentOverlay | SuggestionOverlayForm | Reference to open overlay (null if closed) |
| _isOverlayVisible | bool | Flag to control keyboard interception |
| _lastFilteredResults | List<string> | Cache for retry scenarios |

---

### 2. SuggestionOverlayForm (Form)

**Purpose**: Modal popup displaying filtered suggestions with keyboard/mouse navigation

**Base Class**: `ThemedForm`

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| Suggestions | List<string> | Filtered list of suggestions to display |
| SelectedItem | string | Currently selected item (return value) |
| SelectedIndex | int | Index of selected item in ListBox |

**Controls**:

| Control | Type | Purpose |
|---------|------|---------|
| suggestionListBox | ListBox | Displays suggestions, handles selection |
| lblMatchCount | Label | Shows "N matches found" |
| pnlLoading | Panel | Loading indicator (optional) |

**Methods**:

| Method | Return | Description |
|--------|--------|-------------|
| SelectNext() | void | Move selection down one item |
| SelectPrevious() | void | Move selection up one item |
| SelectFirst() | void | Move to first item (Home key) |
| SelectLast() | void | Move to last item (End key) |
| AcceptSelection() | void | Close with DialogResult.OK |
| CancelSelection() | void | Close with DialogResult.Cancel |

**Behavior**:

- **Modal**: Uses `ShowDialog()` to block parent form
- **Position**: Calculated relative to TextBox screen coordinates
- **Light dismiss**: Clicking outside or Deactivate event cancels
- **Theme aware**: Inherits from ThemedForm for automatic styling

---

### 3. Model_Suggestion_Config (Configuration)

**Purpose**: Configuration model for suggestion behavior

**Properties**:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| DataProvider | Func<Task<List<string>>> | null | Data source delegate |
| MaxResults | int | 100 | Maximum items to display |
| EnableWildcards | bool | true | Support % wildcard matching |
| ShowLoadingIndicator | bool | true | Display loading for slow sources |
| LoadingThresholdMs | int | 100 | Show loading after this delay |
| SuppressExactMatch | bool | true | Hide overlay for exact matches |
| ClearOnNoMatch | bool | true | Clear field if no suggestions |
| MinimumInputLength | int | 0 | Minimum characters before triggering |

**Usage**:
```csharp
var config = new Model_Suggestion_Config
{
    DataProvider = async () => await GetPartNumbersAsync(),
    MaxResults = 50,
    EnableWildcards = true
};
suggestionTextBox1.ApplyConfig(config);
```

---

### 4. Model_Suggestion_Item (Data Model)

**Purpose**: Individual suggestion item with metadata (future enhancement for multi-column)

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| DisplayValue | string | Text shown to user |
| InternalValue | string | Value set in TextBox (same as DisplayValue in v1) |
| Description | string | Optional description (not used in v1) |
| Metadata | Dictionary<string, object> | Extensibility for future features |

**Note**: Version 1 uses `List<string>` directly. This model reserved for future multi-column enhancements.

---

### 5. SuggestionSelectedEventArgs (Event Args)

**Purpose**: Event data when user selects suggestion

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| OriginalInput | string | User's typed text before selection |
| SelectedValue | string | Chosen suggestion value |
| SelectionIndex | int | Index in filtered list |
| Timestamp | DateTime | When selection occurred |
| FieldName | string | Name of TextBox control (for logging) |

**Usage**:
```csharp
suggestionTextBox1.SuggestionSelected += (sender, e) =>
{
    
    // Optional: trigger dependent field updates
};
```

---

### 6. SuggestionCancelledEventArgs (Event Args)

**Purpose**: Event data when user cancels selection

**Properties**:

| Property | Type | Description |
|----------|------|-------------|
| OriginalInput | string | User's typed text (preserved) |
| Reason | SuggestionCancelReason | Why cancelled (Escape, ClickOutside, NoMatches) |
| Timestamp | DateTime | When cancellation occurred |
| FieldName | string | Name of TextBox control (for logging) |

**SuggestionCancelReason Enum**:
- `Escape` - User pressed Escape key
- `ClickOutside` - User clicked outside overlay
- `NoMatches` - No suggestions found (auto-cancelled)
- `EmptyInput` - User typed nothing (no overlay shown)

---

### 7. Service_SuggestionFilter (Service)

**Purpose**: Filtering and sorting logic for suggestions

**Methods**:

| Method | Return | Description |
|--------|--------|-------------|
| FilterSuggestions() | List<string> | Apply substring or wildcard filter |
| ConvertWildcardToRegex() | Regex | Convert % pattern to regex |
| SortByRelevance() | List<string> | Sort by length then alphabetically |
| IsExactMatch() | bool | Check if input matches exactly one item |

**Static Class**: No state, all methods static for performance

**Implementation**:
```csharp
public static class Service_SuggestionFilter
{
    public static List<string> FilterSuggestions(
        List<string> source,
        string filter,
        int maxResults,
        bool enableWildcards)
    {
        if (string.IsNullOrWhiteSpace(filter)) return new List<string>();
        
        // Exact match check
        if (source.Any(s => s.Equals(filter, StringComparison.OrdinalIgnoreCase)))
            return new List<string>(); // Suppress overlay
        
        // Wildcard filtering
        if (enableWildcards && filter.Contains('%'))
        {
            var regex = ConvertWildcardToRegex(filter);
            return source.Where(s => regex.IsMatch(s))
                         .OrderBy(s => s.Length)
                         .ThenBy(s => s, StringComparer.OrdinalIgnoreCase)
                         .Take(maxResults)
                         .ToList();
        }
        
        // Substring filtering
        return source.Where(s => s.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                     .OrderBy(s => s.Length)
                     .ThenBy(s => s, StringComparer.OrdinalIgnoreCase)
                     .Take(maxResults)
                     .ToList();
    }
    
    private static Regex ConvertWildcardToRegex(string wildcardPattern)
    {
        var pattern = "^" + Regex.Escape(wildcardPattern).Replace("%", ".*") + "$";
        return new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
```

---

## State Diagrams

### SuggestionTextBox Lifecycle

```
┌──────────┐
│  Idle    │ Initial state, no overlay
└────┬─────┘
     │ User types text
     ▼
┌──────────┐
│ Modified │ Text changed, validation pending
└────┬─────┘
     │ LostFocus event
     ▼
┌──────────────┐
│ Loading Data │ DataProvider invoked (async)
└────┬─────────┘
     │ Data returned
     ▼
┌──────────────┐
│  Filtering   │ Service_SuggestionFilter applied
└────┬─────────┘
     │
     ├─ No matches ──> Clear field (if ClearOnNoMatch) ──> Idle
     │
     ├─ Exact match ──> Keep value (if SuppressExactMatch) ──> Idle
     │
     └─ Has matches ──> Display overlay ──> Awaiting User Input
                                              │
                        ┌─────────────────────┴──────────────────────┐
                        │                                             │
                        ▼                                             ▼
                  ┌──────────┐                                 ┌──────────┐
                  │ Selected │ Enter/DoubleClick               │ Cancelled│ Escape/ClickOutside
                  └────┬─────┘                                 └────┬─────┘
                       │                                             │
                       │ Update Text                                 │ Keep original
                       │ Fire SuggestionSelected                     │ Fire SuggestionCancelled
                       │ Move focus to next                          │ Keep focus on control
                       │                                             │
                       └─────────────────┬───────────────────────────┘
                                         ▼
                                    ┌──────────┐
                                    │   Idle   │ Ready for next input
                                    └──────────┘
```

### SuggestionOverlayForm Interaction

```
┌─────────────┐
│  Displayed  │ Modal, focus on ListBox
└──────┬──────┘
       │
       ├─ Arrow Down ──> SelectNext() ──> Update ListBox.SelectedIndex
       ├─ Arrow Up ────> SelectPrevious() ──> Update ListBox.SelectedIndex
       ├─ Home ────────> SelectFirst() ──> SelectedIndex = 0
       ├─ End ─────────> SelectLast() ──> SelectedIndex = Count - 1
       │
       ├─ Enter ───────> AcceptSelection() ──> DialogResult.OK ──> Close
       ├─ DoubleClick ─> AcceptSelection() ──> DialogResult.OK ──> Close
       │
       ├─ Escape ──────> CancelSelection() ──> DialogResult.Cancel ──> Close
       └─ ClickOutside > CancelSelection() ──> DialogResult.Cancel ──> Close
```

---

## Data Flow Scenarios

### Scenario 1: Successful Part Number Selection

```
1. User types "R-" in Part Number field
2. User presses Tab (LostFocus)
3. SuggestionTextBox.OnLostFocus():
   a. Call DataProvider() -> await Dao_Part.GetAllPartIDsAsync()
   b. Result: List<string> { "R-ABC-01", "R-XYZ-02", "R-DEF-03", ... }
4. Service_SuggestionFilter.FilterSuggestions():
   a. Substring match: items containing "R-"
   b. Sort by length, then alphabetically
   c. Take(100)
   d. Result: 23 matches
5. SuggestionOverlayForm.ShowDialog():
   a. Position below TextBox
   b. Populate ListBox with 23 items
   c. Show "23 matches found"
   d. Set focus to ListBox
6. User presses Down arrow twice -> "R-DEF-03" highlighted
7. User presses Enter -> AcceptSelection()
8. SuggestionTextBox:
   a. Set Text = "R-DEF-03"
   b. Fire SuggestionSelected event
   c. Log: "User selected 'R-DEF-03' from 'R-'"
   d. Move focus to Operation field (next in tab order)
```

### Scenario 2: Wildcard Search for Locations

```
1. User types "SHOP-%-A" in Location field
2. User presses Tab (LostFocus)
3. DataProvider() -> await Dao_Location.GetAllLocationsAsync()
4. Service_SuggestionFilter.FilterSuggestions():
   a. Detect wildcard (contains '%')
   b. Convert to regex: "^SHOP-.*-A$"
   c. Compile regex with IgnoreCase
   d. Filter: regex.IsMatch(location)
   e. Result: { "SHOP-123-A", "SHOP-456-A", "SHOP-789-A" }
5. Display overlay with 3 matches
6. User double-clicks "SHOP-123-A"
7. Accept selection, update field, move focus
```

### Scenario 3: No Matches - Clear Field

```
1. User types "INVALID-99" in Part Number field
2. User presses Tab (LostFocus)
3. DataProvider() returns List<string> { "R-ABC-01", "R-XYZ-02", ... }
4. Service_SuggestionFilter.FilterSuggestions():
   a. Substring match: no items contain "INVALID-99"
   b. Result: empty list
5. SuggestionTextBox:
   a. Check ClearOnNoMatch == true
   b. Set Text = ""
   c. Fire SuggestionCancelled(Reason = NoMatches)
   d. Log: "No matches found for 'INVALID-99', field cleared"
   e. Show user message: Service_ErrorHandler.ShowWarning("No matching part numbers found")
```

### Scenario 4: Exact Match - No Overlay

```
1. User types "R-ABC-01" (exact existing part)
2. User presses Tab (LostFocus)
3. DataProvider() returns List<string> { "R-ABC-01", "R-ABC-02", ... }
4. Service_SuggestionFilter.FilterSuggestions():
   a. Check exact match: "R-ABC-01" exists exactly
   b. Return empty list (suppress overlay)
5. SuggestionTextBox:
   a. Keep Text = "R-ABC-01"
   b. Move focus to next field (no overlay shown)
```

---

## Database Integration

### Data Provider Implementation Examples

**Example 1: Part Numbers**
```csharp
private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    try
    {
        var dao = new Dao_Part();
        var result = await dao.GetAllPartIDsAsync();
        
        if (result.IsSuccess && result.Data != null)
        {
            return result.Data.AsEnumerable()
                              .Select(r => r["PartID"]?.ToString() ?? "")
                              .Where(s => !string.IsNullOrWhiteSpace(s))
                              .Distinct()
                              .ToList();
        }
        
        Service_ErrorHandler.HandleDatabaseError(
            new Exception(result.ErrorMessage),
            contextData: new Dictionary<string, object> { ["Source"] = "GetPartNumberSuggestions" }
        );
        
        return new List<string>();
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
        return new List<string>();
    }
}
```

**Example 2: Operations (In-Memory)**
```csharp
// If operations are cached at application startup
private Task<List<string>> GetOperationSuggestionsAsync()
{
    return Task.FromResult(Model_Application_Variables.CachedOperations.ToList());
}
```

**Example 3: Locations with Building Filter**
```csharp
private async Task<List<string>> GetLocationSuggestionsAsync(string buildingFilter = null)
{
    var dao = new Dao_Location();
    var result = await dao.GetLocationsByBuildingAsync(buildingFilter);
    
    if (result.IsSuccess && result.Data != null)
    {
        return result.Data.AsEnumerable()
                          .Select(r => r["Location"]?.ToString() ?? "")
                          .Where(s => !string.IsNullOrWhiteSpace(s))
                          .ToList();
    }
    
    return new List<string>();
}
```

---

## Validation Rules

### Field-Level Validation

| Rule | Description | When Applied |
|------|-------------|--------------|
| Not Empty | Field must not be blank after suggestion | OnLostFocus (after overlay) |
| From Master Data | Value must exist in master data table | OnLostFocus (after overlay) |
| Format Validation | Part numbers match expected pattern (if configured) | OnLostFocus (before overlay) |

### Suggestion-Level Validation

| Rule | Description | Behavior |
|------|-------------|----------|
| Minimum Input | If MinimumInputLength > 0, require N characters | Don't trigger overlay if below threshold |
| Exact Match Suppression | If SuppressExactMatch, hide overlay for exact matches | Return empty list from filter |
| No Matches | If ClearOnNoMatch, clear field when no suggestions | Set Text = "" and show warning |

---

## Memory Management

### Control Lifecycle

**Creation**:
- SuggestionTextBox: ~1KB (control overhead)
- DataProvider delegate: ~100 bytes

**Overlay Display**:
- SuggestionOverlayForm: ~500KB (form + ListBox)
- List<string>: ~1KB per 100 items (assumes 10-char average)
- Total: ~2MB for 1000 items

**Disposal**:
- Overlay disposed immediately after ShowDialog() returns
- Data list cleared after filtering
- No long-lived references retained

**Peak Memory**: ~2MB per active control with overlay open

---

## Threading Model

### UI Thread

- All WinForms operations (Text property, focus management, overlay display)
- OnLostFocus event handler
- Event firing (SuggestionSelected, SuggestionCancelled)

### Background Thread (Task Pool)

- DataProvider invocation (async database calls)
- Filtering and sorting (Service_SuggestionFilter - runs synchronously but fast)

### Thread Safety

- No shared mutable state between controls
- Each SuggestionTextBox instance independent
- Service_SuggestionFilter is static and stateless (thread-safe)

---

## Implementation Readiness

✅ **All entities defined with properties, methods, events**  
✅ **State diagrams documented for control and overlay lifecycles**  
✅ **Data flow scenarios cover all user stories**  
✅ **Database integration patterns provided**  
✅ **Validation rules specified**  
✅ **Memory management analyzed**  
✅ **Threading model documented**

**Status**: Ready to proceed to contracts generation
