# Part ID Management Control - UI Structure and Flow

> **Component**: `Control_PartIDManagement`  
> **Location**: `Controls/SettingsForm/Control_PartIDManagement.cs`  
> **Purpose**: Unified interface for adding, editing, and removing part numbers in the MTM WIP Application

## Visual Hierarchy and Navigation Flow

```mermaid
flowchart TD
    Start([User Opens Part ID Management]) --> Home[Home Screen]
    
    Home --> |Contains| HomeLayout[TableLayout_Home<br/>3 Columns]
    
    HomeLayout --> Tile1[HomeTile_Add<br/>Green Accent<br/>🆕 Add Part]
    HomeLayout --> Tile2[HomeTile_Edit<br/>Blue Accent<br/>✏️ Edit Part]
    HomeLayout --> Tile3[HomeTile_Remove<br/>Red Accent<br/>🗑️ Remove Part]
    
    Tile1 --> |Click Anywhere| ShowAdd[Show Add Card<br/>Hide Home]
    Tile2 --> |Click Anywhere| ShowEdit[Show Edit Card<br/>Hide Home]
    Tile3 --> |Click Anywhere| ShowRemove[Show Remove Card<br/>Hide Home]
    
    ShowAdd --> AddCard[AddCard Panel<br/>Visible]
    ShowEdit --> EditCard[EditCard Panel<br/>Visible]
    ShowRemove --> RemoveCard[RemoveCard Panel<br/>Visible]
    
    AddCard --> BackBtn1[Back Button<br/>Appears]
    EditCard --> BackBtn2[Back Button<br/>Appears]
    RemoveCard --> BackBtn3[Back Button<br/>Appears]
    
    BackBtn1 --> |Click| ReturnHome1[Return to Home<br/>Clear All Fields]
    BackBtn2 --> |Click| ReturnHome2[Return to Home<br/>Clear All Fields]
    BackBtn3 --> |Click| ReturnHome3[Return to Home<br/>Clear All Fields]
    
    ReturnHome1 --> Home
    ReturnHome2 --> Home
    ReturnHome3 --> Home
    
    style Home fill:#e1f5e1
    style Tile1 fill:#d4edda
    style Tile2 fill:#d1ecf1
    style Tile3 fill:#f8d7da
    style AddCard fill:#d4edda
    style EditCard fill:#d1ecf1
    style RemoveCard fill:#f8d7da
```

## Control Hierarchy Structure

```mermaid
flowchart TB
    Main[TableLayout_Main<br/>Single Column<br/>Full Container]
    
    Main --> Header[Label_Header<br/>'Part Numbers'<br/>20pt Bold]
    Main --> Subtitle[Label_Subtitle<br/>'Select an action...'<br/>10pt Regular]
    Main --> Container[Panel_Container<br/>Houses All Views]
    
    Container --> HomePanel[Panel_Home<br/>Initial View<br/>Visible by Default]
    Container --> CardsLayout[TableLayout_Cards<br/>Hidden Initially<br/>3 Rows Stack]
    Container --> BackFlow[FlowPanel_BackButton<br/>Hidden Initially<br/>Bottom Dock]
    
    HomePanel --> HomeTable[TableLayout_Home<br/>3 Equal Columns]
    
    HomeTable --> TileAdd[Panel_HomeTile_Add<br/>Clickable Green Tile]
    HomeTable --> TileEdit[Panel_HomeTile_Edit<br/>Clickable Blue Tile]
    HomeTable --> TileRemove[Panel_HomeTile_Remove<br/>Clickable Red Tile]
    
    TileAdd --> AccentAdd[Accent Bar Top<br/>Green 4px Height]
    TileAdd --> ContentAdd[Icon + Title + Description]
    
    TileEdit --> AccentEdit[Accent Bar Top<br/>Blue 4px Height]
    TileEdit --> ContentEdit[Icon + Title + Description]
    
    TileRemove --> AccentRemove[Accent Bar Top<br/>Red 4px Height]
    TileRemove --> ContentRemove[Icon + Title + Description]
    
    CardsLayout --> CardAdd[Panel_AddCard<br/>Row 0<br/>Hidden Initially]
    CardsLayout --> CardEdit[Panel_EditCard<br/>Row 1<br/>Hidden Initially]
    CardsLayout --> CardRemove[Panel_RemoveCard<br/>Row 2<br/>Hidden Initially]
    
    style Main fill:#f0f0f0
    style Container fill:#fff
    style HomePanel fill:#e8f5e9
    style CardsLayout fill:#fff3cd
    style BackFlow fill:#d1ecf1
```

## Add Card Internal Structure

```mermaid
flowchart TB
    AddCard[Panel_AddCard<br/>Green Accent Bar]
    
    AddCard --> AddLayout[TableLayout_Add<br/>2 Columns<br/>Icon + Content]
    
    AddLayout --> AddIcon[Label_AddIcon<br/>🆕 28pt Emoji]
    AddLayout --> AddTitle[Label_AddTitle<br/>'Add Part' 16pt Bold]
    AddLayout --> AddContent[TableLayout_AddContent<br/>Spans Both Columns<br/>5 Rows Stack]
    
    AddContent --> Row1[Suggestion_AddPartNumber<br/>No Autocomplete<br/>Manual Entry Only]
    AddContent --> Row2[Suggestion_AddItemType<br/>F4 Suggestions Enabled<br/>Cached Item Types]
    AddContent --> Row3[IssuedBy Row<br/>Label + Value Display<br/>Current User]
    AddContent --> Row4[CheckBox_AddRequiresColorCode<br/>'Requires color code<br/>and work order']
    AddContent --> Row5[FlowPanel_AddActions<br/>Right-Aligned Buttons]
    
    Row5 --> BtnClear[Button_AddClear<br/>Secondary Style<br/>Gray Background]
    Row5 --> BtnSave[Button_AddSave<br/>Primary Style<br/>Blue Background]
    
    BtnClear --> |Click| ClearAction[Clear All Fields<br/>Uncheck Box<br/>Focus Part Number]
    BtnSave --> |Click| ValidateAdd{Validate Input}
    
    ValidateAdd --> |Empty Part#| WarnPart[Show Warning<br/>Focus Part Number]
    ValidateAdd --> |Empty ItemType| WarnItem[Show Warning<br/>Focus Item Type]
    ValidateAdd --> |Valid| CheckExists{Part Already<br/>Exists?}
    
    CheckExists --> |Yes| WarnExists[Show Warning<br/>'Use Edit instead']
    CheckExists --> |No| SaveDB[(Save to Database<br/>CreatePartAsync)]
    
    SaveDB --> |Success| Success1[Show Success Message]
    SaveDB --> |Fail| Error1[Handle Database Error]
    
    Success1 --> PostSave[Refresh Part Caches<br/>Clear Fields<br/>Notify Parent]
    
    style AddCard fill:#d4edda
    style ValidateAdd fill:#fff3cd
    style CheckExists fill:#fff3cd
    style SaveDB fill:#cce5ff
    style Success1 fill:#d4edda
    style Error1 fill:#f8d7da
```

## Edit Card Internal Structure

```mermaid
flowchart TB
    EditCard[Panel_EditCard<br/>Blue Accent Bar]
    
    EditCard --> EditLayout[TableLayout_Edit<br/>2 Columns<br/>Icon + Content]
    
    EditLayout --> EditIcon[Label_EditIcon<br/>✏️ 28pt Emoji]
    EditLayout --> EditTitle[Label_EditTitle<br/>'Edit Part' 16pt Bold]
    EditLayout --> EditContent[TableLayout_EditContent<br/>Spans Both Columns<br/>6 Rows Stack]
    
    EditContent --> ERow1[Suggestion_EditSelectPart<br/>F4 Suggestions Enabled<br/>Cached Part Numbers]
    EditContent --> ERow2[Suggestion_EditNewPartNumber<br/>No Autocomplete<br/>Initially Disabled]
    EditContent --> ERow3[Suggestion_EditItemType<br/>F4 Suggestions Enabled<br/>Initially Disabled]
    EditContent --> ERow4[IssuedBy Row<br/>Shows Original User<br/>Read-Only Display]
    EditContent --> ERow5[CheckBox_EditRequiresColorCode<br/>Initially Disabled]
    EditContent --> ERow6[FlowPanel_EditActions<br/>Right-Aligned Buttons<br/>Initially Disabled]
    
    ERow1 --> |SuggestionSelected Event| LoadPart[LoadEditPartAsync<br/>Fetch from Database]
    
    LoadPart --> |Success| PopulateFields[Populate All Fields<br/>Store Original Data<br/>Enable Editing]
    LoadPart --> |Not Found| WarnNotFound[Show Warning<br/>Clear Section]
    LoadPart --> |DB Error| HandleError[Handle Database Error<br/>Clear Section]
    
    PopulateFields --> EnableEdit[Enable:<br/>New Part Number<br/>Item Type<br/>CheckBox<br/>Buttons]
    
    ERow6 --> BtnReset[Button_EditReset<br/>Secondary Style]
    ERow6 --> BtnEditSave[Button_EditSave<br/>Primary Style]
    
    BtnReset --> |Click| ClearEdit[Clear All Fields<br/>Disable Edit Section<br/>Focus Select Part]
    
    BtnEditSave --> |Click| ValidateEdit{Validate Input}
    
    ValidateEdit --> |No Part Selected| WarnNoPart[Show Warning<br/>'Select a part first']
    ValidateEdit --> |Empty Part#| WarnNewPart[Show Warning<br/>Focus New Part Number]
    ValidateEdit --> |Empty ItemType| WarnNewItem[Show Warning<br/>Focus Item Type]
    ValidateEdit --> |Valid| CheckChange{Part Number<br/>Changed?}
    
    CheckChange --> |Yes| CheckNewExists{New Part#<br/>Exists?}
    CheckChange --> |No| UpdateDB
    
    CheckNewExists --> |Yes| WarnDupe[Show Warning<br/>'Choose another number']
    CheckNewExists --> |No| UpdateDB[(Update Database<br/>UpdatePartAsync)]
    
    UpdateDB --> |Success| Success2[Show Success Message]
    UpdateDB --> |Fail| Error2[Handle Database Error]
    
    Success2 --> PostEdit[Check ColorCode Change<br/>Refresh Caches<br/>Clear Section<br/>Notify Parent]
    
    style EditCard fill:#d1ecf1
    style LoadPart fill:#cce5ff
    style PopulateFields fill:#d4edda
    style ValidateEdit fill:#fff3cd
    style CheckChange fill:#fff3cd
    style CheckNewExists fill:#fff3cd
    style UpdateDB fill:#cce5ff
    style Success2 fill:#d4edda
    style Error2 fill:#f8d7da
```

## Remove Card Internal Structure

```mermaid
flowchart TB
    RemoveCard[Panel_RemoveCard<br/>Red Accent Bar]
    
    RemoveCard --> RemoveLayout[TableLayout_Remove<br/>2 Columns<br/>Icon + Content]
    
    RemoveLayout --> RemoveIcon[Label_RemoveIcon<br/>🗑️ 28pt Emoji]
    RemoveLayout --> RemoveTitle[Label_RemoveTitle<br/>'Remove Part' 16pt Bold]
    RemoveLayout --> RemoveContent[TableLayout_RemoveContent<br/>Spans Both Columns<br/>4 Rows Stack]
    
    RemoveContent --> RRow1[Suggestion_RemoveSelectPart<br/>F4 Suggestions Enabled<br/>Cached Part Numbers]
    RemoveContent --> RRow2[TableLayout_RemoveDetails<br/>5 Rows × 2 Columns<br/>Initially Hidden]
    RemoveContent --> RRow3[Label_RemoveWarning<br/>'Removal is permanent'<br/>Red Text<br/>Initially Hidden]
    RemoveContent --> RRow4[FlowPanel_RemoveActions<br/>Right-Aligned Buttons]
    
    RRow1 --> |SuggestionSelected Event| LoadRemove[LoadRemovePartAsync<br/>Fetch from Database]
    
    LoadRemove --> |Success| PopulateRemove[Populate Details Table:<br/>Part Number<br/>Customer<br/>Description<br/>Item Type<br/>Issued By]
    LoadRemove --> |Not Found| WarnNotFound2[Show Warning<br/>Clear Section]
    LoadRemove --> |DB Error| HandleError2[Handle Database Error<br/>Clear Section]
    
    PopulateRemove --> ShowDetails[Show Details Table<br/>Show Warning Label<br/>Enable Confirm Button]
    
    RRow2 --> DetailRow1[Item Number Label + Value]
    RRow2 --> DetailRow2[Customer Label + Value]
    RRow2 --> DetailRow3[Description Label + Value]
    RRow2 --> DetailRow4[Item Type Label + Value]
    RRow2 --> DetailRow5[Issued By Label + Value]
    
    RRow4 --> BtnCancel[Button_RemoveCancel<br/>Secondary Style<br/>Always Enabled]
    RRow4 --> BtnConfirm[Button_RemoveConfirm<br/>Danger Style<br/>Red Background<br/>Initially Disabled]
    
    BtnCancel --> |Click| ClearRemove[Clear All Fields<br/>Hide Details<br/>Disable Buttons<br/>Focus Select Part]
    
    BtnConfirm --> |Click| ValidateRemove{Validate<br/>Selection}
    
    ValidateRemove --> |No Part Selected| WarnNoSelection[Show Warning<br/>'Select a part first']
    ValidateRemove --> |Missing Part#| WarnMissing[Show Warning<br/>'Missing part number']
    ValidateRemove --> |Valid| Confirmation{User Confirms<br/>Deletion?}
    
    Confirmation --> |No| CancelRemove[Return to Remove Card<br/>No Changes]
    Confirmation --> |Yes| DeleteDB[(Delete from Database<br/>DeletePartAsync)]
    
    DeleteDB --> |Success| Success3[Show Success Message]
    DeleteDB --> |Fail| Error3[Handle Database Error]
    
    Success3 --> PostRemove[Refresh Part Caches<br/>Clear Section<br/>Notify Parent]
    
    style RemoveCard fill:#f8d7da
    style LoadRemove fill:#cce5ff
    style PopulateRemove fill:#d4edda
    style ShowDetails fill:#d4edda
    style ValidateRemove fill:#fff3cd
    style Confirmation fill:#ffeaa7
    style DeleteDB fill:#cce5ff
    style Success3 fill:#d4edda
    style Error3 fill:#f8d7da
    style BtnConfirm fill:#f8d7da
```

## Navigation State Management

```mermaid
stateDiagram-v2
    [*] --> HomeView: Control Loads
    
    HomeView --> AddCardView: Click Add Tile
    HomeView --> EditCardView: Click Edit Tile
    HomeView --> RemoveCardView: Click Remove Tile
    
    AddCardView --> HomeView: Click Back Button
    EditCardView --> HomeView: Click Back Button
    RemoveCardView --> HomeView: Click Back Button
    
    state HomeView {
        [*] --> ShowingTiles
        ShowingTiles: Panel_Home Visible
        ShowingTiles: TableLayout_Cards Hidden
        ShowingTiles: FlowPanel_BackButton Hidden
        ShowingTiles: All 3 Tiles Clickable
    }
    
    state AddCardView {
        [*] --> AddCardActive
        AddCardActive: Panel_Home Hidden
        AddCardActive: TableLayout_Cards Visible
        AddCardActive: Panel_AddCard Visible
        AddCardActive: Panel_EditCard Hidden
        AddCardActive: Panel_RemoveCard Hidden
        AddCardActive: FlowPanel_BackButton Visible
        AddCardActive: Focus on Part Number Field
    }
    
    state EditCardView {
        [*] --> SelectionMode
        SelectionMode: All Fields Disabled
        SelectionMode: Only Select Part Enabled
        SelectionMode --> EditingMode: Part Selected
        EditingMode: All Fields Enabled
        EditingMode: Part Data Loaded
        EditingMode --> SelectionMode: Reset Clicked
    }
    
    state RemoveCardView {
        [*] --> RemoveSelectionMode
        RemoveSelectionMode: Details Hidden
        RemoveSelectionMode: Confirm Button Disabled
        RemoveSelectionMode: Only Select Part Enabled
        RemoveSelectionMode --> ConfirmationMode: Part Selected
        ConfirmationMode: Details Visible
        ConfirmationMode: Warning Shown
        ConfirmationMode: Confirm Button Enabled
        ConfirmationMode --> RemoveSelectionMode: Cancel Clicked
    }
```

## Panel Visibility Matrix

| View State | Panel_Home | TableLayout_Cards | AddCard | EditCard | RemoveCard | BackButton |
|------------|------------|-------------------|---------|----------|------------|------------|
| **Initial Load** | ✅ Visible | ❌ Hidden | ❌ Hidden | ❌ Hidden | ❌ Hidden | ❌ Hidden |
| **Add Mode** | ❌ Hidden | ✅ Visible | ✅ Visible | ❌ Hidden | ❌ Hidden | ✅ Visible |
| **Edit Mode** | ❌ Hidden | ✅ Visible | ❌ Hidden | ✅ Visible | ❌ Hidden | ✅ Visible |
| **Remove Mode** | ❌ Hidden | ✅ Visible | ❌ Hidden | ❌ Hidden | ✅ Visible | ✅ Visible |

## Event Wiring Flow

```mermaid
flowchart LR
    Init[InitializeComponent] --> Wire1[WireUpEventHandlers]
    Init --> Wire2[WireUpNavigationHandlers]
    Init --> Config[ConfigureSuggestionInputs]
    
    Wire1 --> E1[Add Button Clicks]
    Wire1 --> E2[Edit Button Clicks]
    Wire1 --> E3[Remove Button Clicks]
    Wire1 --> E4[SuggestionSelected Events]
    
    Wire2 --> N1[Home Tile Clicks]
    Wire2 --> N2[Recursive Child Control Clicks]
    Wire2 --> N3[Back Button Click]
    
    Config --> S1[Configure Part# Suggestions]
    Config --> S2[Configure ItemType Suggestions]
    Config --> S3[Set Validation Modes]
    
    E4 --> |Edit Card| AutoLoad1[Auto-load part data<br/>when suggestion selected]
    E4 --> |Remove Card| AutoLoad2[Auto-load part data<br/>when suggestion selected]
    
    N2 --> |Recursion| AllChildren[Every label, icon, and<br/>child control becomes clickable]
    
    style Init fill:#e1f5e1
    style Wire1 fill:#d1ecf1
    style Wire2 fill:#d1ecf1
    style Config fill:#fff3cd
```

## Key Behavioral Patterns

### 1. **Home Tile Interaction**
- Each tile (Add, Edit, Remove) and ALL its child controls are clickable
- Clicking anywhere on a tile triggers `ShowCard(index)`
- Cursor changes to hand pointer on hover
- Recursively wires up all nested controls for click events

### 2. **Card Visibility Toggle**
- Only ONE card visible at a time (mutually exclusive)
- `ShowCard(index)` hides home, shows cards container, reveals back button
- Automatically focuses appropriate input field when card appears

### 3. **Section Enabling/Disabling**
- **Add Card**: Always enabled (except when no privileges)
- **Edit Card**: Starts disabled, enables when part loaded
- **Remove Card**: Starts disabled, enables when part loaded

### 4. **Data Loading Pattern**
- User selects from suggestion dropdown → fires `SuggestionSelected` event
- Event handler calls `LoadEditPartAsync` or `LoadRemovePartAsync`
- Async database fetch via `Dao_Part.GetPartByNumberAsync`
- On success: populate fields, enable editing, store original values
- On failure: show error, clear section, keep disabled

### 5. **Validation Flow**
- Client-side validation before database operations
- Check for empty required fields → show warning, focus field
- Check for duplicates → query database, show warning if exists
- All validation uses `Service_ErrorHandler` (no direct MessageBox)

### 6. **Cache Refresh Pattern**
- After any successful add/edit/remove operation
- Calls `RefreshPartCachesAsync()` which updates:
  - `Helper_UI_ComboBoxes.SetupPartDataTable()`
  - `Helper_UI_SuggestionBoxes.LoadPartIdsAsync()`
- Notifies parent via `PartListChanged` event

### 7. **Privilege Enforcement**
- `ApplyPrivileges(canAdd, canEdit, canRemove)` method
- Hides/disables tiles and cards based on user permissions
- If no privileges: shows message, hides all functionality

### 8. **Color Code Special Handling**
- Tracks original `RequiresColorCode` state in Edit mode
- If changed, triggers `Model_Application_Variables.ReloadColorCodePartsAsync()`
- Critical for parts requiring color code + work order validation

---

**Document Created**: November 19, 2025  
**Related Files**:
- `Controls/SettingsForm/Control_PartIDManagement.cs`
- `Controls/SettingsForm/Control_PartIDManagement.Designer.cs`
