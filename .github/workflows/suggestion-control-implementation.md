# Workflow: Implement SuggestionTextBoxWithLabel

This workflow guides the implementation or migration of user inputs to the `SuggestionTextBoxWithLabel` control.

## Phase 1: Analysis & Preparation

1.  **Identify Target Input**:
    -   Is this a new input or a migration?
    -   What entity does it represent? (Part, Operation, Location, ItemType, Other)
    -   Does it require validation?
    -   Does it require autocomplete/suggestions?

2.  **Check Prerequisites**:
    -   Ensure `Helper_UI_SuggestionBoxes` supports the entity type.
    -   If not, plan to add data loading logic to `Helper_UI_SuggestionBoxes.cs` and configuration logic to `Helper_SuggestionTextBox.cs`.

## Phase 2: Designer Implementation

1.  **Open Designer File** (`.Designer.cs`):
    -   Locate the container where the control should reside.

2.  **Remove Legacy Controls** (If Migration):
    -   Remove `System.Windows.Forms.Label` (Header/Label).
    -   Remove `System.Windows.Forms.TextBox` (Input).
    -   Remove `System.Windows.Forms.Button` (Search/F4) if present.

3.  **Add SuggestionTextBoxWithLabel**:
    -   Declare field: `private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_Name;`
    -   Instantiate in `InitializeComponent`: `Control_Name = new ...SuggestionTextBoxWithLabel();`
    -   Set Properties:
        -   `Name`: `Control_{Context}_SuggestionBox_{Entity}`
        -   `LabelText`: "Entity Name"
        -   `PlaceholderText`: "Enter or select..."
        -   `Dock`: `DockStyle.Top` or `Fill`
        -   `TabIndex`: Correct order in form.

4.  **Update Layout**:
    -   Add to parent container (`Controls.Add(...)`).
    -   Adjust `TableLayoutPanel` spans if necessary (Composite control handles its own internal layout, so it usually occupies the space of the previous Label+TextBox combo).

## Phase 3: Code-Behind Configuration

1.  **Open Code File** (`.cs`):
    -   Go to Constructor or `OnLoad`.

2.  **Configure Data Provider**:
    -   **Standard Entity**:
        ```csharp
        Helper_SuggestionTextBox.ConfigureFor{Entity}(
            Control_Name,
            Helper_SuggestionTextBox.GetCached{Entity}sAsync);
        ```
    -   **Custom Entity**:
        ```csharp
        Control_Name.TextBox.DataProvider = MyCustomDataProviderAsync;
        ```

3.  **Wire Events**:
    -   Subscribe to `SuggestionSelected`:
        ```csharp
        Control_Name.SuggestionSelected += (s, e) => {
            // Handle selection
            var selectedValue = e.SelectedValue;
        };
        ```

4.  **Cleanup**:
    -   Unsubscribe events in `Dispose()`.

## Phase 4: Verification

1.  **Build Project**: Ensure no compilation errors.
2.  **Runtime Check**:
    -   Verify Label text is correct.
    -   Verify Placeholder text is visible.
    -   **Test Typing**: Suggestions should appear after typing.
    -   **Test F4**: Full list should appear.
    -   **Test Selection**: Selecting an item should trigger the event.
    -   **Test Validation**: Invalid input should show red border (if configured).

## Checklist

- [ ] Control named correctly (`Control_..._SuggestionBox_...`)
- [ ] `LabelText` set
- [ ] `PlaceholderText` set
- [ ] `ConfigureFor...` called in Constructor
- [ ] Events wired up
- [ ] Events unwired in Dispose
- [ ] Layout verified in Designer
