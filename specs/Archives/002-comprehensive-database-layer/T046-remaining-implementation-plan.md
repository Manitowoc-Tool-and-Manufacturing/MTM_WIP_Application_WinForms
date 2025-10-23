# T046g-u Implementation Plan

**Status**: T046a-f COMPLETE (7/21 subtasks done)  
**Remaining**: T046g-u (14 subtasks)

## Completed Tasks (T046a-f)
- ✅ T046a: Control_Add_ItemType - DaoResult checks added
- ✅ T046b: Control_Edit_ItemType - DaoResult checks added  
- ✅ T046c: Control_Remove_ItemType - DaoResult checks added
- ✅ T046d: Control_Remove_Location - DaoResult checks added
- ✅ T046e: Control_Remove_Operation - DaoResult checks added
- ✅ T046f: Control_Remove_PartID - DaoResult checks added (using new Async methods)

## Remaining SettingsForm Controls

### T046g: Control_Shortcuts.cs
**File**: `Controls/SettingsForm/Control_Shortcuts.cs`

**Line 39** - GetShortcutsJsonAsync:
```csharp
// CURRENT (INCORRECT):
string shortcutsJson = await Dao_User.GetShortcutsJsonAsync(user);

// SHOULD BE:
var shortcutsResult = await Dao_User.GetShortcutsJsonAsync(user);
if (!shortcutsResult.IsSuccess)
{
    StatusMessageChanged?.Invoke(this, $"Error loading shortcuts: {shortcutsResult.ErrorMessage}");
    return;
}
string shortcutsJson = shortcutsResult.Data ?? string.Empty;
```

**Line 399** - SetShortcutsJsonAsync:
```csharp
// CURRENT (INCORRECT):
await Dao_User.SetShortcutsJsonAsync(user, json);

// SHOULD BE:
var saveResult = await Dao_User.SetShortcutsJsonAsync(user, json);
if (!saveResult.IsSuccess)
{
    StatusMessageChanged?.Invoke(this, $"Error saving shortcuts: {saveResult.ErrorMessage}");
    return;
}
```

---

### T046h: Control_Theme.cs
**File**: `Controls/SettingsForm/Control_Theme.cs`

**Line 34** - GetThemeNameAsync:
```csharp
// CURRENT (INCORRECT):
string? themeName = await Dao_User.GetThemeNameAsync(user);

// SHOULD BE:
var themeResult = await Dao_User.GetThemeNameAsync(user);
if (!themeResult.IsSuccess)
{
    StatusMessageChanged?.Invoke(this, $"Error loading theme: {themeResult.ErrorMessage}");
}
else
{
    string? themeName = themeResult.Data;
    // ... rest of logic
}
```

**Line 75** - SetThemeNameAsync:
```csharp
// CURRENT (INCORRECT):
await Dao_User.SetThemeNameAsync(user, selectedTheme);

// SHOULD BE:
var saveResult = await Dao_User.SetThemeNameAsync(user, selectedTheme);
if (!saveResult.IsSuccess)
{
    StatusMessageChanged?.Invoke(this, $"Error saving theme: {saveResult.ErrorMessage}");
    return;
}
```

---

### T046i: Control_Edit_User.cs (Verify - partially done in T038)
**File**: `Controls/SettingsForm/Control_Edit_User.cs`

**Status**: Per T038, this was already updated. Need to VERIFY all calls properly check DaoResult.

Methods to verify:
- Line 147: `GetUserByUsernameAsync` - Check result.Data
- Line 239: `GetUserByUsernameAsync` - Check result.Data
- Line 169: `GetUserRoleIdAsync` - Check result.Data
- Line 221: `UpdateUserAsync` - Check result.IsSuccess
- Line 253: `SetUserRoleAsync` - Check result.IsSuccess

**Action**: Read file and confirm all checks are present.

---

### T046j: Control_Remove_User.cs (Verify - partially done in T038)
**File**: `Controls/SettingsForm/Control_Remove_User.cs`

**Status**: Per T038, this was already updated. Need to VERIFY all calls properly check DaoResult.

Methods to verify:
- Line 129: `GetUserByUsernameAsync` - Check result.Data
- Line 210: `GetUserByUsernameAsync` - Check result.Data
- Line 141: `GetUserRoleIdAsync` - Check result.Data
- Line 242: `GetUserRoleIdAsync` - Check result.Data
- Line 228: `DeleteUserSettingsAsync` - Check result.IsSuccess
- Line 246: `RemoveUserRoleAsync` - Check result.IsSuccess
- Line 255: `DeleteUserAsync` - Check result.IsSuccess

**Action**: Read file and confirm all checks are present.

---

### T046q: Control_Add_PartID.cs
**File**: `Controls/SettingsForm/Control_Add_PartID.cs`

**Line 108** - PartExists (OBSOLETE method):
```csharp
// CURRENT (INCORRECT - uses obsolete method):
if (await Dao_Part.PartExists(itemNumberTextBox.Text.Trim()))

// SHOULD BE (use new PartExistsAsync):
var existsResult = await Dao_Part.PartExistsAsync(itemNumberTextBox.Text.Trim());
if (!existsResult.IsSuccess)
{
    MessageBox.Show($"Error checking part: {existsResult.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}
if (existsResult.Data)
```

**Line 135** - AddPartWithStoredProcedure (OBSOLETE method):
```csharp
// CURRENT (INCORRECT - uses obsolete method):
await Dao_Part.AddPartWithStoredProcedure(itemNumber, null, null, issuedBy, type);

// SHOULD BE (use new CreatePartAsync):
var createResult = await Dao_Part.CreatePartAsync(itemNumber, null, null, issuedBy, type);
if (!createResult.IsSuccess)
{
    MessageBox.Show($"Error adding part: {createResult.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}
```

---

### T046r: Control_Edit_PartID.cs
**File**: `Controls/SettingsForm/Control_Edit_PartID.cs`

**Line 184** - GetPartByNumber (OBSOLETE method):
```csharp
// CURRENT (INCORRECT - uses obsolete method):
_currentPart = await Dao_Part.GetPartByNumber(selectedText);

// SHOULD BE (use new GetPartByNumberAsync):
var getResult = await Dao_Part.GetPartByNumberAsync(selectedText);
if (!getResult.IsSuccess)
{
    MessageBox.Show($"Error loading part: {getResult.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}
_currentPart = getResult.Data;
```

**Line 332** - PartExists (OBSOLETE method):
```csharp
// CURRENT (INCORRECT):
if (originalItemNumber != newItemNumber && await Dao_Part.PartExists(newItemNumber))

// SHOULD BE:
if (originalItemNumber != newItemNumber)
{
    var existsResult = await Dao_Part.PartExistsAsync(newItemNumber);
    if (!existsResult.IsSuccess)
    {
        MessageBox.Show($"Error checking part: {existsResult.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }
    if (existsResult.Data)
    {
        // Show duplicate message
    }
}
```

**Line 512** - UpdatePartWithStoredProcedure (OBSOLETE method):
```csharp
// CURRENT (INCORRECT - uses obsolete method):
await Dao_Part.UpdatePartWithStoredProcedure(id, itemNumber, customer, description, issuedBy, type);

// SHOULD BE (use new UpdatePartAsync):
var updateResult = await Dao_Part.UpdatePartAsync(id, itemNumber, customer, description, issuedBy, type);
if (!updateResult.IsSuccess)
{
    MessageBox.Show($"Error updating part: {updateResult.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}
```

---

## MainForm Controls

### T046k: Control_TransferTab.cs
**File**: `Controls/MainForm/Control_TransferTab.cs`

**Methods to update:**
- Line 145: `GetUserFullNameAsync` - Check result.Data
- Line 497: `GetInventoryByPartIdAndOperationAsync` - Check result.Data  
- Line 515: `GetInventoryByPartIdAsync` - Check result.Data
- Line 730: `TransferInventoryQuantityAsync` - Check result.IsSuccess
- Line 736: `TransferPartSimpleAsync` - Check result.IsSuccess
- Line 793: `TransferPartSimpleAsync` - Check result.IsSuccess
- Line 740: `AddTransactionHistoryAsync` - Check result.IsSuccess
- Line 795: `AddTransactionHistoryAsync` - Check result.IsSuccess

**Note**: Some of these calls (730, 736, 793, 740, 795) appear to be fire-and-forget. Verify if error handling is needed or if they should remain async void.

---

### T046l: Control_RemoveTab.cs
**File**: `Controls/MainForm/Control_RemoveTab.cs`

**Methods to update:**
- Line 194: `GetUserFullNameAsync` - Check result.Data
- Line 708: `GetInventoryByPartIdAndOperationAsync` - Check result.Data
- Line 721: `GetInventoryByPartIdAsync` - Check result.Data

**Already correct** (no changes needed):
- Line 304: `RemoveInventoryItemsFromDataGridViewAsync` - ✅ Already checks result.IsSuccess
- Line 362: `AddTransactionHistoryAsync` - ✅ Already checks result.IsSuccess  
- Line 429: `AddInventoryItemAsync` - ✅ Already checks result.IsSuccess

---

### T046m: Control_AdvancedInventory.cs
**File**: `Controls/MainForm/Control_AdvancedInventory.cs`

**Verify these AddInventoryItemAsync calls:**
- Line 895: Check result.IsSuccess
- Line 1441: Check result.IsSuccess
- Line 1725: Check result.IsSuccess

**Action**: Read file and verify proper DaoResult checking.

---

### T046n: Control_AdvancedRemove.cs
**File**: `Controls/MainForm/Control_AdvancedRemove.cs`

**Verify these calls:**
- Line 450: `RemoveInventoryItemsFromDataGridViewAsync` - Check result.IsSuccess
- Line 684: `AddInventoryItemAsync` - Check result.IsSuccess

**Action**: Read file and verify proper DaoResult checking.

---

### T046o: Control_QuickButtons.cs
**File**: `Controls/MainForm/Control_QuickButtons.cs`

**Methods to update:**
- Line 505: `UpdateQuickButtonAsync` - Check result.IsSuccess
- Line 529: `RemoveQuickButtonAndShiftAsync` - Check result.IsSuccess
- Line 567: `DeleteAllQuickButtonsForUserAsync` - Check result.IsSuccess
- Line 577: `AddQuickButtonAtPositionAsync` - Check result.IsSuccess

**Pattern**:
```csharp
// CURRENT (uses try/catch only):
try
{
    await Dao_QuickButtons.UpdateQuickButtonAsync(...);
}
catch (MySqlException ex)
{
    LoggingUtility.LogDatabaseError(ex);
}

// SHOULD BE:
var result = await Dao_QuickButtons.UpdateQuickButtonAsync(...);
if (!result.IsSuccess)
{
    LoggingUtility.Log($"QuickButton update failed: {result.ErrorMessage}");
    MessageBox.Show($"Failed to update quick button: {result.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
```

---

### T046p: Control_InventoryTab.cs
**File**: `Controls/MainForm/Control_InventoryTab.cs`

**Line 675** - AddOrShiftQuickButtonAsync:
```csharp
// CURRENT (fire and forget):
await Dao_QuickButtons.AddOrShiftQuickButtonAsync(user, partId, operation, quantity);

// SHOULD BE:
var result = await Dao_QuickButtons.AddOrShiftQuickButtonAsync(user, partId, operation, quantity);
if (!result.IsSuccess)
{
    LoggingUtility.Log($"Failed to add to quick buttons: {result.ErrorMessage}");
    // Non-critical error - continue execution
}
```

**Already correct**:
- Line 604: `AddInventoryItemAsync` - ✅ Already checks result.IsSuccess

---

## Forms

### T046s: MainForm.cs
**File**: `Forms/MainForm/MainForm.cs`

**Line 547** - GetUserFullNameAsync:
```csharp
// CURRENT (INCORRECT):
Model_AppVariables.UserFullName = await Dao_User.GetUserFullNameAsync(Model_AppVariables.User);

// SHOULD BE:
var userNameResult = await Dao_User.GetUserFullNameAsync(Model_AppVariables.User);
if (userNameResult.IsSuccess && !string.IsNullOrEmpty(userNameResult.Data))
{
    Model_AppVariables.UserFullName = userNameResult.Data;
}
else
{
    Model_AppVariables.UserFullName = Model_AppVariables.User; // Fallback
    if (!userNameResult.IsSuccess)
    {
        LoggingUtility.Log($"Failed to load user full name: {userNameResult.ErrorMessage}");
    }
}
```

---

### T046t: MainFormUserSettingsHelper.cs
**File**: `Forms/MainForm/Classes/MainFormUserSettingsHelper.cs`

**ALL Dao_User calls need DaoResult checks:**

```csharp
// Line 18 - GetLastShownVersionAsync
var versionResult = await Dao_User.GetLastShownVersionAsync(Model_AppVariables.User);
if (versionResult.IsSuccess)
{
    var lastShownVersion = versionResult.Data;
    // ... existing logic
}

// Line 21 - SetHideChangeLogAsync
var hideChangeLogResult = await Dao_User.SetHideChangeLogAsync(Model_AppVariables.User, "false");
if (!hideChangeLogResult.IsSuccess)
{
    LoggingUtility.Log($"Failed to set hide changelog: {hideChangeLogResult.ErrorMessage}");
}

// Line 23 - SetLastShownVersionAsync
var setVersionResult = await Dao_User.SetLastShownVersionAsync(Model_AppVariables.User, Model_AppVariables.Version);
if (!setVersionResult.IsSuccess)
{
    LoggingUtility.Log($"Failed to set last shown version: {setVersionResult.ErrorMessage}");
}

// Line 26-30 - Multiple Get* methods
var serverAddressResult = await Dao_User.GetWipServerAddressAsync(Model_AppVariables.User);
Model_AppVariables.WipServerAddress = serverAddressResult.IsSuccess ? serverAddressResult.Data : string.Empty;

var serverPortResult = await Dao_User.GetWipServerPortAsync(Model_AppVariables.User);
Model_AppVariables.WipServerPort = serverPortResult.IsSuccess ? serverPortResult.Data : string.Empty;

var visualUserNameResult = await Dao_User.GetVisualUserNameAsync(Model_AppVariables.User);
Model_AppVariables.VisualUserName = visualUserNameResult.IsSuccess ? visualUserNameResult.Data : string.Empty;

var visualPasswordResult = await Dao_User.GetVisualPasswordAsync(Model_AppVariables.User);
Model_AppVariables.VisualPassword = visualPasswordResult.IsSuccess ? visualPasswordResult.Data : string.Empty;

var themeNameResult = await Dao_User.GetThemeNameAsync(Model_AppVariables.User);
Model_AppVariables.WipDataGridTheme = themeNameResult.IsSuccess ? themeNameResult.Data : Model_AppVariables.ThemeName;

// Line 36 - GetThemeFontSizeAsync
var fontSizeResult = await Dao_User.GetThemeFontSizeAsync(Model_AppVariables.User);
Model_AppVariables.ThemeFontSize = fontSizeResult.IsSuccess ? fontSizeResult.Data ?? 9 : 9;
```

---

## Services

### T046u: Service_OnStartup_StartupSplashApplicationContext.cs
**File**: `Services/Service_OnStartup_StartupSplashApplicationContext.cs`

**Line 639** - GetThemeFontSizeAsync:
```csharp
// CURRENT (INCORRECT):
int? fontSize = await Dao_User.GetThemeFontSizeAsync(Model_AppVariables.User);
Model_AppVariables.ThemeFontSize = fontSize ?? 9;

// SHOULD BE:
var fontSizeResult = await Dao_User.GetThemeFontSizeAsync(Model_AppVariables.User);
if (fontSizeResult.IsSuccess)
{
    Model_AppVariables.ThemeFontSize = fontSizeResult.Data ?? 9;
}
else
{
    Model_AppVariables.ThemeFontSize = 9; // Default fallback
    LoggingUtility.Log($"Failed to load theme font size: {fontSizeResult.ErrorMessage}");
}
```

---

## Overlap Analysis with T047-T049

### T047: Control_Add_Operation.cs - Refactor to use Dao_Operation methods
**Current status**: Control_Add_Operation.cs currently uses direct Helper_Database_StoredProcedure calls (bypasses DAO layer).

**Covered by T046**: NO - T046 tasks focus on DaoResult checking, not refactoring from Helper to DAO calls.

**Verdict**: **T047 is SEPARATE work** - needs to refactor Control_Add_Operation to use Dao_Operation.OperationExists() and Dao_Operation.InsertOperation() instead of direct stored procedure calls.

---

### T048: Control_QuickButtons.cs - async/await patterns
**Current status**: T046o updates Control_QuickButtons.cs to add DaoResult checks.
**T048 scope**: "LoadQuickButtonsAsync, btnQuickButton_Click event handlers"

**Overlap**: T046o covers button click handlers (UpdateQuickButtonAsync, RemoveQuickButtonAndShiftAsync, DeleteAllQuickButtonsForUserAsync, AddQuickButtonAtPositionAsync).

**Verdict**: **T048 PARTIALLY COVERED by T046o**. After T046o completes:
- ✅ Button click handlers covered (DaoResult checks added)
- ❓ LoadQuickButtonsAsync needs verification - check if it properly uses async/await patterns

**Amendment needed for T048**: "Verify LoadQuickButtonsAsync uses proper async/await patterns. Button click DaoResult checks completed in T046o."

---

### T049: Run Validate-Parameter-Prefixes.ps1
**Covered by T046**: NO - completely separate validation task.

**Verdict**: **T049 is SEPARATE work** - needs to run validation script.

---

## Summary

**Completed**: T046a-f (7 tasks)  
**Remaining**: T046g-u (14 tasks)

**T047-T049 Status**:
- T047: Separate work (refactor Control_Add_Operation)
- T048: Partially covered by T046o (need to verify LoadQuickButtonsAsync only)
- T049: Separate work (run validation script)

**Recommended Amendment to tasks.md**:
```markdown
- [ ] T048 [US4] Verify `Controls/MainForm/Control_QuickButtons.cs` LoadQuickButtonsAsync uses proper async/await patterns (button click DaoResult checks completed in T046o)
```
