# Task Checklist: Consolidate User Management Controls into Single User Control

## Phase 1: Analysis & Planning

### 1.1 Read Existing Implementations
- [x] Read `Control_PartId_Management.cs` - Analyze UI layout, event handlers, data binding patterns, **SuggestionTextBoxWithLabel usage**
- [x] Read `Control_Operation_Management.cs` - Study validation logic, save/update workflows, **SuggestionTextBoxWithLabel patterns**
- [x] Read `Control_Location_Management.cs` - Review delete confirmation patterns, grid operations, **SuggestionTextBoxWithLabel styling**
- [x] Read `Control_ItemType_Management.cs` - Examine error handling, async patterns, **SuggestionTextBoxWithLabel implementation**
- [x] Read all `.Designer.cs` files for the 4 management controls - Document visual layout approach and **SuggestionTextBoxWithLabel configuration**
- [x] Read `Settings/` folder backup files - Compare old vs new implementation differences
- [x] **Read `SuggestionTextBoxWithLabel.cs` + `.Designer.cs`** - Understand component properties, theming, and usage patterns

### 1.2 Analyze Current User Controls
- [x] Read `Control_User_Add.cs` + `.Designer.cs` - Document current Add functionality and **existing TextBox/Label patterns**
- [x] Read `Control_User_Edit.cs` + `.Designer.cs` - Document current Edit functionality and **existing TextBox/Label patterns**
- [x] Read `Control_User_Remove.cs` + `.Designer.cs` - Document current Remove functionality and **existing TextBox/Label patterns**
- [x] Identify common UI elements across the 3 user controls
- [x] Identify unique functionality in each user control
- [x] **Document current styling/spacing to match in new consolidated control**

### 1.3 Study Supporting Services
- [x] Read `Service_ErrorHandler.cs` - Understand error display patterns
- [x] Read `LoggingUtility.cs` - Document logging requirements
- [x] Read `Dao_User.cs` (or equivalent) - Map all database operations used
- [x] Read stored procedures in `Database/CurrentStoredProcedures/` for user management
- [x] Identify all Helper classes used by the 4 management controls

### 1.4 Pattern Documentation
- [x] Document the "management control pattern" used in the 4 existing controls:
    - Tab/view switching mechanism (Add/Edit/Delete modes)
    - DataGridView population and refresh logic
    - Form validation approach
    - Save/Update/Delete workflow
    - Success/failure messaging
    - Control state management (enable/disable)
    - **SuggestionTextBoxWithLabel layout and spacing patterns**
    - **Visual hierarchy and grouping (GroupBox/Panel usage)**
- [x] Create UI wireframe matching Visual Studio WinForms designer approach and **4 management controls styling**
- [x] **Document exact control positioning, margins, and spacing from existing management controls**

## Phase 2: Design New Consolidated Control

### 2.1 Control Structure Planning
- [ ] Define control name: `Control_User_Management.cs`
- [ ] Design tab/button layout for mode switching (Add/Edit/Delete) **matching 4 management controls pattern**
- [ ] Map all UI elements needed:
    - [ ] **SuggestionTextBoxWithLabel** for Username
    - [ ] **SuggestionTextBoxWithLabel** for Full Name
    - [ ] **SuggestionTextBoxWithLabel** (with ComboBox) for Role/Permissions
    - [ ] **SuggestionTextBoxWithLabel** (with CheckBox or ComboBox) for Active status
    - [ ] Buttons (Save/Update/Delete/Clear) **matching management control button styling**
    - [ ] DataGridView for user list **matching management control grid layout**
    - [ ] Labels for instructions **matching management control label patterns**
    - [ ] GroupBox/Panel containers **matching management control grouping**
- [ ] Plan control naming convention: `UserManagement_{ControlType}_{Purpose}`
- [ ] Ensure all UI elements can be designed in Visual Studio WinForms designer (NO C# UI generation)
- [ ] **Plan exact spacing/margins to match the 4 management controls (typically 10-15px between controls)**

### 2.2 Data Model Planning
- [ ] List all user properties needed (Username, FullName, Role, Active, etc.)
- [ ] Define validation rules for each field
- [ ] Map DAO methods required: `GetAllAsync()`, `InsertAsync()`, `UpdateAsync()`, `DeleteAsync()`
- [ ] Plan `Model_Dao_Result<T>` return types for each operation

### 2.3 Mode-Specific Behavior Design
- [ ] **Add Mode**:
    - [ ] List required fields and default values
    - [ ] Define save workflow and validation
    - [ ] Plan success message and grid refresh
    - [ ] **Define which SuggestionTextBoxWithLabel controls are enabled/visible**
- [ ] **Edit Mode**:
    - [ ] Define row selection behavior from DataGridView
    - [ ] Plan field population from selected user
    - [ ] Define update workflow and conflict handling
    - [ ] **Define which SuggestionTextBoxWithLabel controls are enabled/visible**
- [ ] **Delete Mode**:
    - [ ] Design confirmation dialog pattern (matching other controls)
    - [ ] Plan cascade delete checks (if user has transactions/history)
    - [ ] Define deletion success workflow
    - [ ] **Define which SuggestionTextBoxWithLabel controls are read-only/visible**

## Phase 3: Implementation

### 3.1 Create Base Control File
- [ ] Create `Controls/Control_User_Management.cs`
- [ ] Inherit from `ThemedUserControl` (NOT `UserControl`)
- [ ] Add standard #region blocks:
    ```csharp
    #region Fields
    #region Properties  
    #region Constructors
    #region Methods
    #region Events
    #region Helpers
    #region Cleanup / Dispose
    ```
- [ ] Add XML documentation to class

### 3.2 Design UI in Visual Studio Designer (Match 4 Management Controls Styling)
- [ ] Open `.Designer.cs` in Visual Studio WinForms designer
- [ ] **Add mode selection controls (RadioButtons/TabControl/Buttons) - match positioning from management controls**
- [ ] **Add GroupBox/Panel for input fields section (match container styling from management controls)**
- [ ] Add input fields using **SuggestionTextBoxWithLabel**:
    - [ ] **SuggestionTextBoxWithLabel for Username**
        - Set `LabelText = "Username:"`
        - Set `PlaceholderText = "Enter username"`
        - Name: `UserManagement_SuggestionTextBox_Username`
        - **Match Width/Height/Location pattern from management controls**
    - [ ] **SuggestionTextBoxWithLabel for Full Name**
        - Set `LabelText = "Full Name:"`
        - Set `PlaceholderText = "Enter full name"`
        - Name: `UserManagement_SuggestionTextBox_FullName`
        - **Match Width/Height/Location pattern from management controls**
    - [ ] **SuggestionTextBoxWithLabel for Role** (configure for ComboBox mode if supported)
        - Set `LabelText = "Role:"`
        - Name: `UserManagement_SuggestionTextBox_Role`
        - **Match Width/Height/Location pattern from management controls**
        - **OR use separate ComboBox with Label if SuggestionTextBoxWithLabel doesn't support dropdown**
    - [ ] **SuggestionTextBoxWithLabel for Active Status** (configure for CheckBox or ComboBox)
        - Set `LabelText = "Active:"`
        - Name: `UserManagement_SuggestionTextBox_Active`
        - **Match Width/Height/Location pattern from management controls**
        - **OR use separate CheckBox/ComboBox with Label if needed**
    - [ ] **Any other required fields using SuggestionTextBoxWithLabel**
- [ ] **Set consistent spacing between SuggestionTextBoxWithLabel controls (match management controls - typically 10-15px vertical spacing)**
- [ ] **Add GroupBox/Panel for action buttons (match container styling from management controls)**
- [ ] Add action buttons:
    - [ ] Save button (for Add mode) - **match button size/styling from management controls**
    - [ ] Update button (for Edit mode) - **match button size/styling from management controls**
    - [ ] Delete button (for Delete mode) - **match button size/styling from management controls**
    - [ ] Clear/Reset button - **match button size/styling from management controls**
    - [ ] **Set button spacing to match management controls (typically 10px horizontal spacing)**
- [ ] Add DataGridView for user list display - **match grid styling, column headers, row height from management controls**
- [ ] Add status/instruction Label - **match font, color, positioning from management controls**
- [ ] Set control names following pattern: `UserManagement_{ControlType}_{Purpose}`
- [ ] Apply DPI scaling attributes: `AutoScaleMode = Font`, `AutoScaleDimensions = 96F, 96F`
- [ ] **Verify overall layout matches visual hierarchy of PartId/Operation/Location/ItemType management controls**

### 3.3 Implement Fields and Properties
- [ ] Add private fields for mode tracking: `_currentMode` enum
- [ ] Add private field for selected user: `_selectedUser`
- [ ] Add dependency injection fields if using services
- [ ] **Add private fields for SuggestionTextBoxWithLabel controls if needed for programmatic access**
- [ ] Add XML documentation to all public properties

### 3.4 Implement Constructor
- [ ] Initialize components: `InitializeComponent()`
- [ ] Set default mode (Add)
- [ ] Wire up event handlers
- [ ] **Configure SuggestionTextBoxWithLabel properties (validation, max length, etc.)**
- [ ] Call `LoadUsersAsync()` to populate grid
- [ ] Add XML documentation

### 3.5 Implement Mode Switching Logic
- [ ] Create `SwitchToAddMode()` method:
    - [ ] Clear all input fields (**call `.Clear()` on SuggestionTextBoxWithLabel controls**)
    - [ ] Enable/disable appropriate buttons
    - [ ] **Enable/disable SuggestionTextBoxWithLabel controls as needed**
    - [ ] Update instruction label
    - [ ] Set focus to first input (**focus first SuggestionTextBoxWithLabel**)
- [ ] Create `SwitchToEditMode()` method:
    - [ ] Enable row selection in grid
    - [ ] Enable/disable appropriate buttons
    - [ ] **Enable/disable SuggestionTextBoxWithLabel controls as needed**
    - [ ] Update instruction label
- [ ] Create `SwitchToDeleteMode()` method:
    - [ ] Disable input fields (read-only display) - **set SuggestionTextBoxWithLabel `.ReadOnly = true`**
    - [ ] Enable/disable appropriate buttons
    - [ ] Update instruction label
- [ ] Wire mode buttons to switch methods

### 3.6 Implement Data Loading
- [ ] Create `LoadUsersAsync()` method:
    ```csharp
    /// <summary>
    /// Loads all users from database and populates DataGridView.
    /// </summary>
    private async Task LoadUsersAsync()
    {
            try
            {
                    var result = await Dao_User.GetAllAsync();
                    if (!result.IsSuccess)
                    {
                            Service_ErrorHandler.ShowUserError(result.ErrorMessage);
                            return;
                    }
                    UserManagement_DataGridView_Users.DataSource = result.Data;
            }
            catch (Exception ex)
            {
                    Service_ErrorHandler.HandleException(ex, ...);
            }
    }
    ```
- [ ] Add XML documentation
- [ ] Test async/await pattern compliance

### 3.7 Implement Add Functionality
- [ ] Create `ValidateInputs()` helper method:
    - [ ] Check required fields not empty (**access `.Text` property of SuggestionTextBoxWithLabel controls**)
    - [ ] Validate username format
    - [ ] Check password strength (if applicable)
    - [ ] Return validation result with error messages
- [ ] Create `SaveNewUserAsync()` method:
    - [ ] Call `ValidateInputs()`
    - [ ] **Extract values from SuggestionTextBoxWithLabel controls (`.Text` property)**
    - [ ] Call `Dao_User.InsertAsync(...)` with field values
    - [ ] Check `result.IsSuccess`
    - [ ] Show success message via `Service_ErrorHandler.ShowUserSuccess(...)`
    - [ ] Call `LoadUsersAsync()` to refresh grid
    - [ ] Call `ClearInputFields()`
    - [ ] Add try-catch with `Service_ErrorHandler.HandleException(...)`
- [ ] Wire Save button click event to `SaveNewUserAsync()`
- [ ] Add XML documentation

### 3.8 Implement Edit Functionality
- [ ] Create `DataGridView_SelectionChanged` event handler:
    - [ ] Get selected row
    - [ ] **Populate SuggestionTextBoxWithLabel controls from selected user data (set `.Text` property)**
    - [ ] Store selected user in `_selectedUser` field
    - [ ] Enable Update button
- [ ] Create `UpdateUserAsync()` method:
    - [ ] Call `ValidateInputs()`
    - [ ] Check `_selectedUser` not null
    - [ ] **Extract modified values from SuggestionTextBoxWithLabel controls**
    - [ ] Call `Dao_User.UpdateAsync(...)` with modified values
    - [ ] Check `result.IsSuccess`
    - [ ] Show success message
    - [ ] Call `LoadUsersAsync()` to refresh grid
    - [ ] Add try-catch with error handling
- [ ] Wire Update button click event to `UpdateUserAsync()`
- [ ] Add XML documentation

### 3.9 Implement Delete Functionality
- [ ] Create `DeleteUserAsync()` method:
    - [ ] Check `_selectedUser` not null
    - [ ] Show confirmation dialog:
        ```csharp
        var confirm = MessageBox.Show(
                $"Are you sure you want to delete user '{username}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
        if (confirm != DialogResult.Yes) return;
        ```
    - [ ] Call `Dao_User.DeleteAsync(_selectedUser.UserId)`
    - [ ] Check `result.IsSuccess`
    - [ ] Show success message
    - [ ] Call `LoadUsersAsync()` to refresh grid
    - [ ] Call `ClearInputFields()`
    - [ ] Add try-catch with error handling
- [ ] Wire Delete button click event to `DeleteUserAsync()`
- [ ] Add XML documentation

### 3.10 Implement Helper Methods
- [ ] Create `ClearInputFields()` method:
    - [ ] **Call `.Clear()` on all SuggestionTextBoxWithLabel controls**
    - [ ] Reset ComboBoxes to default (if separate controls used)
    - [ ] Uncheck CheckBoxes (if separate controls used)
    - [ ] Set `_selectedUser = null`
- [ ] Create `SetControlsEnabled(bool enabled)` method for state management:
    - [ ] **Enable/disable SuggestionTextBoxWithLabel controls**
    - [ ] Enable/disable buttons
- [ ] **Create `PopulateFieldsFromUser(UserModel user)` method:**
    - [ ] **Set `.Text` property of each SuggestionTextBoxWithLabel control from user object**
- [ ] Add XML documentation to all helpers

## Phase 4: Integration & Cleanup

### 4.1 Replace Old Controls in Settings Form
- [ ] Open parent form that contains the 3 separate user controls
- [ ] Remove `Control_User_Add`, `Control_User_Edit`, `Control_User_Remove` from designer
- [ ] Add new `Control_User_Management` to form
- [ ] **Position and size to match layout of other management controls (same width/height/anchor settings)**
- [ ] Update any parent form logic that referenced old controls

### 4.2 Move Old Files to Backup
- [ ] Create `Settings/UserControls_Backup/` folder if not exists
- [ ] Move `Control_User_Add.cs` + `.Designer.cs` to backup folder
- [ ] Move `Control_User_Edit.cs` + `.Designer.cs` to backup folder
- [ ] Move `Control_User_Remove.cs` + `.Designer.cs` to backup folder
- [ ] Add README.md in backup folder documenting reason for backup

### 4.3 Code Review Checklist
- [ ] Verify all #region blocks present and organized correctly
- [ ] Verify XML documentation on all public members
- [ ] Verify `Service_ErrorHandler` used (no `MessageBox.Show` except confirmations)
- [ ] Verify all async methods use async/await correctly
- [ ] Verify all DAO calls return `Model_Dao_Result<T>`
- [ ] Verify control inherits from `ThemedUserControl`
- [ ] **Verify all TextBox/ComboBox inputs use SuggestionTextBoxWithLabel (matching 4 management controls)**
- [ ] Verify all UI elements designed in WinForms designer (not C# code)
- [ ] Verify DPI scaling attributes set correctly
- [ ] Verify no direct database access (only through DAOs)
- [ ] Verify control naming follows pattern: `UserManagement_{ControlType}_{Purpose}`
- [ ] **Verify spacing/layout matches the 4 management controls exactly**

## Phase 5: Testing

### 5.1 Manual Testing
- [ ] **Test UI Layout:**
    - [ ] Verify SuggestionTextBoxWithLabel controls display correctly
    - [ ] Verify spacing matches management controls
    - [ ] Verify labels align properly
    - [ ] Verify theme colors apply to SuggestionTextBoxWithLabel
    - [ ] Verify placeholder text displays in SuggestionTextBoxWithLabel
- [ ] Test Add Mode:
    - [ ] Add valid user - verify success
    - [ ] Add invalid user (missing fields) - verify validation
    - [ ] Add duplicate username - verify error handling
    - [ ] Verify grid refreshes after save
    - [ ] **Verify SuggestionTextBoxWithLabel controls clear after save**
- [ ] Test Edit Mode:
    - [ ] Select user from grid - **verify SuggestionTextBoxWithLabel controls populate correctly**
    - [ ] Update user - verify success
    - [ ] Update with invalid data - verify validation
    - [ ] Verify grid refreshes after update
- [ ] Test Delete Mode:
    - [ ] Select user from grid
    - [ ] **Verify SuggestionTextBoxWithLabel controls show user data as read-only**
    - [ ] Delete user - verify confirmation dialog
    - [ ] Confirm delete - verify success
    - [ ] Cancel delete - verify no action taken
    - [ ] Verify grid refreshes after delete
- [ ] Test mode switching - **verify SuggestionTextBoxWithLabel controls enable/disable correctly**
- [ ] Test theme application - **verify SuggestionTextBoxWithLabel colors match current theme**

### 5.2 Error Scenario Testing
- [ ] Test database connection failure - verify error message displayed
- [ ] Test DAO failure - verify `Service_ErrorHandler` called
- [ ] Test exception in event handler - verify logged and displayed
- [ ] Test null/empty grid - verify no crashes on selection
- [ ] **Test SuggestionTextBoxWithLabel validation (max length, invalid characters, etc.)**

### 5.3 Integration Testing
- [ ] Verify control loads in parent Settings form
- [ ] **Verify layout matches other management controls side-by-side**
- [ ] Verify no impact on other settings tabs
- [ ] Verify application startup with new control
- [ ] **Verify SuggestionTextBoxWithLabel keyboard navigation (Tab order)**

## Phase 6: Documentation

### 6.1 Code Documentation
- [ ] Verify XML documentation complete and accurate
- [ ] Add inline comments for complex logic only
- [ ] Document any known limitations or TODOs
- [ ] **Document SuggestionTextBoxWithLabel usage patterns for future developers**

### 6.2 Update Project Documentation
- [ ] Update `AGENTS.md` if control workflow differs from standard
- [ ] Add entry to CHANGELOG.md documenting consolidation
- [ ] Update any user guides/screenshots showing settings UI
- [ ] **Document SuggestionTextBoxWithLabel pattern adoption in user management**

## Phase 7: Commit & PR

### 7.1 Commit Changes
- [ ] Stage new `Control_User_Management.cs` + `.Designer.cs`
- [ ] Stage modified parent Settings form files
- [ ] Stage backup folder with old controls
- [ ] Commit message: `T###: Consolidate user management controls into single UserControl with SuggestionTextBoxWithLabel`

### 7.2 Create Pull Request
- [ ] Title: `[Settings] Consolidate User Add/Edit/Remove into single management control (SuggestionTextBoxWithLabel pattern)`
- [ ] Description: Reference pattern used by PartId/Operation/Location/ItemType controls, **emphasize SuggestionTextBoxWithLabel adoption for UI consistency**
- [ ] Link to task/issue number
- [ ] Request review from maintainer

---

## Reference Patterns (From Existing Controls)

### Pattern: SuggestionTextBoxWithLabel Usage (From 4 Management Controls)
```csharp
// In Designer.cs (Example from existing management controls)
this.UserManagement_SuggestionTextBox_Username = new SuggestionTextBoxWithLabel();
this.UserManagement_SuggestionTextBox_Username.LabelText = "Username:";
this.UserManagement_SuggestionTextBox_Username.PlaceholderText = "Enter username";
this.UserManagement_SuggestionTextBox_Username.Location = new System.Drawing.Point(20, 30);
this.UserManagement_SuggestionTextBox_Username.Size = new System.Drawing.Size(300, 50); // Adjust based on actual pattern
this.UserManagement_SuggestionTextBox_Username.Name = "UserManagement_SuggestionTextBox_Username";

// Accessing value in code
string username = UserManagement_SuggestionTextBox_Username.Text;

// Setting value
UserManagement_SuggestionTextBox_Username.Text = user.Username;

// Clearing value
UserManagement_SuggestionTextBox_Username.Clear();

// Setting read-only
UserManagement_SuggestionTextBox_Username.ReadOnly = true;
```

### Pattern: Mode Switching (Updated for SuggestionTextBoxWithLabel)
```csharp
private void SwitchToMode(ManagementMode mode)
{
        _currentMode = mode;
        
        switch (mode)
        {
                case ManagementMode.Add:
                        ClearInputFields(); // Calls .Clear() on all SuggestionTextBoxWithLabel controls
                        EnableControls(addMode: true);
                        UserManagement_SuggestionTextBox_Username.Focus(); // Focus first input
                        break;
                case ManagementMode.Edit:
                        EnableControls(editMode: true);
                        UserManagement_SuggestionTextBox_Username.ReadOnly = false; // Allow editing
                        break;
                case ManagementMode.Delete:
                        EnableControls(deleteMode: true);
                        UserManagement_SuggestionTextBox_Username.ReadOnly = true; // Read-only for delete
                        break;
        }
}
```

### Pattern: DAO Call with Error Handling (Updated for SuggestionTextBoxWithLabel)
```csharp
try
{
        // Extract values from SuggestionTextBoxWithLabel controls
        string username = UserManagement_SuggestionTextBox_Username.Text;
        string fullName = UserManagement_SuggestionTextBox_FullName.Text;
        string role = UserManagement_SuggestionTextBox_Role.Text; // Or ComboBox if separate
        
        var result = await Dao_User.InsertAsync(username, fullName, role);
        
        if (!result.IsSuccess)
        {
                Service_ErrorHandler.ShowUserError(result.ErrorMessage);
                return;
        }
        
        Service_ErrorHandler.ShowUserSuccess("User added successfully!");
        await LoadUsersAsync();
        ClearInputFields(); // Clears all SuggestionTextBoxWithLabel controls
}
catch (Exception ex)
{
        Service_ErrorHandler.HandleException(
                ex,
                Enum_ErrorSeverity.Medium,
                contextData: new Dictionary<string, object>
                {
                        ["User"] = Model_Application_Variables.User,
                        ["Operation"] = "AddUser"
                },
                callerName: nameof(SaveNewUserAsync),
                controlName: this.Name);
}
```

### Pattern: Grid Selection Event (Updated for SuggestionTextBoxWithLabel)
```csharp
private void DataGridView_SelectionChanged(object sender, EventArgs e)
{
        if (DataGridView.SelectedRows.Count == 0) return;
        
        var row = DataGridView.SelectedRows[0];
        _selectedUser = new UserModel
        {
                UserId = Convert.ToInt32(row.Cells["UserId"].Value),
                Username = row.Cells["Username"].Value?.ToString() ?? "",
                FullName = row.Cells["FullName"].Value?.ToString() ?? "",
                Role = row.Cells["Role"].Value?.ToString() ?? ""
        };
        
        // Populate SuggestionTextBoxWithLabel controls from selected user
        UserManagement_SuggestionTextBox_Username.Text = _selectedUser.Username;
        UserManagement_SuggestionTextBox_FullName.Text = _selectedUser.FullName;
        UserManagement_SuggestionTextBox_Role.Text = _selectedUser.Role;
}
```

### Pattern: Clear Input Fields (Updated for SuggestionTextBoxWithLabel)
```csharp
private void ClearInputFields()
{
        UserManagement_SuggestionTextBox_Username.Clear();
        UserManagement_SuggestionTextBox_FullName.Clear();
        UserManagement_SuggestionTextBox_Role.Clear();
        // Clear any other SuggestionTextBoxWithLabel controls
        
        _selectedUser = null;
}
```

