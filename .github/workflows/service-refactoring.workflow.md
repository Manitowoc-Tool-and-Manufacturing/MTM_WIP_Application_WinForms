# Service & Code Refactoring Workflow

This workflow defines the process for refactoring individual C# files to comply with the MTM WIP Application's service and architectural guidelines.

## Scope
This workflow applies to:
- Forms (`.cs` code-behind)
- UserControls (`.cs` code-behind)
- DAOs (`Data/Dao_*.cs`)
- Services (`Services/Service_*.cs`)

## Refactoring Checklist

### 1. Error Handling Standardization
**Goal**: Eliminate `MessageBox.Show` and ensure centralized error management.

1.  **Scan for `MessageBox.Show`**:
    -   **Error Messages**: Replace with `Service_ErrorHandler.ShowUserError(message)`.
    -   **Info Messages**: Replace with `Service_ErrorHandler.ShowUserInfo(message)`.
    -   **Success Messages**: Replace with `Service_ErrorHandler.ShowUserSuccess(message)`.
    -   **Confirmations**: Keep `MessageBox.Show` ONLY for Yes/No confirmations, or use a standardized dialog if available.
2.  **Update `try-catch` Blocks**:
    -   In **Event Handlers** (Buttons, Load):
        ```csharp
        try {
            // code
        } catch (Exception ex) {
            Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, ...);
        }
        ```
    -   In **DAOs/Services**:
        -   Catch exceptions, log them via `LoggingUtility` (or let `HandleException` do it if bubbling up), and return a Failure result.
        -   *Do not* show UI dialogs in DAOs.

### 2. Logging Implementation
**Goal**: Ensure consistent CSV logging via `LoggingUtility`.

1.  **Add Entry Logging**:
    -   At the start of significant operations (e.g., `Save`, `Delete`, `Load`), add:
        `LoggingUtility.Log($"[{Context}] Operation started", $"Details={...}");`
2.  **Verify Error Logging**:
    -   Ensure `Service_ErrorHandler` is being used (it handles logging automatically).
    -   If manually handling an exception in a background task, ensure `LoggingUtility.LogApplicationError(ex)` is called.

### 3. Async/Await Conversion
**Goal**: Remove blocking I/O operations.

1.  **Identify Blocking Calls**: Look for `.Result`, `.Wait()`, or synchronous DAO calls.
2.  **Convert to Async**:
    -   Change method signature: `void` -> `async Task` (or `async void` for event handlers).
    -   Await the call: `var data = dao.GetData();` -> `var data = await dao.GetDataAsync();`.
3.  **Rename Methods**: Append `Async` to the method name (e.g., `LoadData` -> `LoadDataAsync`).

### 4. DAO Pattern Compliance (If refactoring a DAO)
**Goal**: Standardize database access.

1.  **Return Types**: Ensure method returns `Model_Dao_Result<T>`.
2.  **Helper Usage**: Verify usage of `Helper_Database_StoredProcedure`.
3.  **No UI**: Ensure NO `MessageBox` or UI references exist in the DAO.

### 5. Code Organization
**Goal**: Standard file structure.

1.  **Apply Regions**: Ensure the file has the standard `#region` blocks:
    -   Fields
    -   Properties
    -   Constructors
    -   Methods
    -   Events
    -   Helpers
2.  **XML Documentation**: Add `/// <summary>` comments to all public members.

## Execution Steps

1.  **Select File**: Choose a file to refactor (e.g., `MyForm.cs`).
2.  **Create Temporary File**:
    -   Create a new file (e.g., `MyForm_New.cs`) to build the refactored code from scratch.
    -   Use the original file as a reference but write clean, compliant code in the new file.
    -   *Reason*: This prevents corruption and ensures a clean slate for regions and structure.
3.  **Apply Changes**: Implement the refactored logic in the new file following the checklist.
4.  **Swap Files**:
    -   Delete the original file (`MyForm.cs`).
    -   Rename the new file to the original name (`MyForm_New.cs` -> `MyForm.cs`).
5.  **Verify**:
    -   Build the project.
    -   Test the specific functionality to ensure no regression.
    -   Check Logs (`%APPDATA%\MTM\Logs`) to verify logging works.
