# Serena MCP Server: Capabilities & Workflows

This document provides a comprehensive guide to the **Serena Model Context Protocol (MCP) Server** integrated into this workspace. Serena enhances GitHub Copilot by providing **symbolic understanding**, **memory management**, and **precise code editing** capabilities that go beyond standard text-based AI interactions.

---

## 1. Core Concepts

Before diving into features, it is essential to understand how Serena views your code:

*   **Symbols**: Serena "sees" code as a tree of symbols (Classes, Methods, Properties) rather than just lines of text.
*   **Name Paths**: Symbols are identified by a path, e.g., `Dao_Inventory/GetAllAsync`.
*   **Memories**: A persistent storage system where Serena can save context about the project (architecture, patterns, decisions) to recall in future sessions.

---

## 2. Feature Breakdown

### A. Project & Memory Management

These tools allow Serena to maintain context across different sessions and understand the project's high-level structure.

#### 1. Project Activation & Onboarding
*   **Tools**: `activate_project`, `onboarding`, `check_onboarding_performed`
*   **Description**: Initializes the session. `onboarding` scans the codebase to create an initial "mental map" or summary of the project.
*   **Plain Copilot vs. Serena**: Copilot starts "fresh" every session. Serena loads the project context you've previously saved.

#### 2. Memory System
*   **Tools**: `read_memory`, `write_memory`, `edit_memory`, `list_memories`
*   **Description**: Read/Write access to markdown files stored in `.specify/memory/` (or similar). Use this to store architectural decisions, active tasks, or "Constitution" rules.
*   **Prompt Example**: "Save this architectural decision about using `Service_ErrorHandler` to memory so we don't forget it."
*   **Codebase Example**:
    ```text
    User: "Check our memory for the rules regarding database access."
    Serena Action: calls read_memory("constitution.md") or read_memory("database_rules.md")
    ```

---

### B. Code Exploration (The "Smart" Read)

Serena avoids reading entire files (which wastes tokens). It uses **symbolic** and **pattern** search.

#### 3. Symbolic Search (`find_symbol`)
*   **Description**: Finds specific code elements (classes, methods) without needing to know the file path.
*   **Key Parameters**: `name_path_pattern` (e.g., `*Controller/index`), `include_body` (fetch the code).
*   **Prompt Example**: "Show me the `GetAllAsync` method in the Inventory DAO."
*   **Codebase Example**:
    *   *Plain Copilot*: You must open `Dao_Inventory.cs` or hope it finds it in the context.
    *   *Serena*:
        ```text
        User: "How does Dao_Inventory handle database connections?"
        Serena Action: calls find_symbol(name_path_pattern="Dao_Inventory", include_body=True)
        Result: Returns only the class definition of Dao_Inventory, pinpointing the exact logic.
        ```

#### 4. Symbol Overview (`get_symbols_overview`)
*   **Description**: Returns a high-level outline of a file (classes, methods, fields) *without* the implementation details. Great for understanding a file's responsibility.
*   **Prompt Example**: "What methods are available in `Service_ErrorHandler.cs`?"
*   **Codebase Example**:
    ```text
    User: "Summarize the capabilities of the Email Notification service."
    Serena Action: calls get_symbols_overview(relative_path="Services/Service_EmailNotification.cs")
    Result: Lists methods like `SendNotification`, `ProcessNotificationAsync` without dumping 200 lines of code.
    ```

#### 5. Reference Finding (`find_referencing_symbols`)
*   **Description**: Finds exactly *who* calls a specific method or uses a class.
*   **Prompt Example**: "Where is `Service_ErrorHandler.HandleException` used?"
*   **Codebase Example**:
    *   *Plain Copilot*: "Find in Files" text search (noisy, finds comments).
    *   *Serena*:
        ```text
        User: "Find all call sites of the InsertAsync method in Dao_Inventory to ensure we update them."
        Serena Action: calls find_referencing_symbols(name_path="Dao_Inventory/InsertAsync")
        Result: Returns a precise list of methods (e.g., in `TransactionLifecycleForm`) that call this specific method.
        ```

#### 6. Pattern Search (`search_for_pattern`)
*   **Description**: A powerful regex/text search for when you don't know the symbol name or are searching non-code files.
*   **Prompt Example**: "Find all TODOs in the codebase."
*   **Codebase Example**:
    ```text
    User: "Find where we are manually instantiating MySqlConnection."
    Serena Action: calls search_for_pattern(substring_pattern="new MySqlConnection")
    Result: Lists specific lines in `Service_Migration.cs`, `Service_Analytics.cs`, etc.
    ```

---

### C. Precise Code Editing

Serena edits code structurally, reducing the risk of breaking syntax or indentation.

#### 7. Replace Symbol Body (`replace_symbol_body`)
*   **Description**: Replaces the *entire* implementation of a method or class.
*   **Prompt Example**: "Rewrite `CalculateTotal` to use the new tax rate."
*   **Codebase Example**:
    *   *Plain Copilot*: Generates a code block and asks you to copy-paste it, or attempts a diff that might miss a closing brace.
    *   *Serena*:
        ```text
        User: "Refactor Dao_Inventory.GetAllAsync to use the new Helper_Database_StoredProcedure."
        Serena Action: calls replace_symbol_body(
            name_path="Dao_Inventory/GetAllAsync",
            body="public async Task<Model_Dao_Result<DataTable>> GetAllAsync() { ... new implementation ... }"
        )
        Result: The method is cleanly swapped in the file.
        ```

#### 8. Insert Code (`insert_after_symbol`, `insert_before_symbol`)
*   **Description**: Inserts new methods, fields, or classes relative to existing ones.
*   **Prompt Example**: "Add a `ValidateInput` method after `SaveData`."
*   **Codebase Example**:
    ```text
    User: "Add a new private helper method 'SanitizeInput' to Service_FeedbackManager."
    Serena Action: calls insert_after_symbol(
        name_path="Service_FeedbackManager/SubmitFeedbackAsync",
        body="private string SanitizeInput(string input) { ... }"
    )
    ```

#### 9. Rename Symbol (`rename_symbol`)
*   **Description**: Renames a symbol (class, method, variable) and updates references (refactoring).
*   **Prompt Example**: "Rename `ProcessData` to `ProcessDataAsync`."
*   **Codebase Example**:
    ```text
    User: "Rename the 'Description' property in Model_UserFeedback to 'Content' to match the database."
    Serena Action: calls rename_symbol(name_path="Model_UserFeedback/Description", new_name="Content")
    Result: Updates the property definition AND all usages in DAOs and Forms.
    ```

---

### D. Meta-Cognition (Thinking Tools)

These tools allow Serena to pause and "think" before acting, improving complex task execution.

*   **Tools**: `think_about_collected_information`, `think_about_task_adherence`, `think_about_whether_you_are_done`
*   **Description**: Forces the agent to review its context, ensure it hasn't drifted from the user's request, and verify completeness.
*   **Usage**: You don't invoke these; Serena uses them automatically during complex multi-step chains.

---

## 3. Serena vs. Plain Copilot: A Comparison

| Feature | Plain Copilot | Copilot + Serena |
| :--- | :--- | :--- |
| **Context** | Limited to open files and recent chat. | **Project-wide**, persistent memory, symbolic map. |
| **Search** | Text-based "Find in Files". | **Symbolic** (Find usages, Find implementations). |
| **Reading** | Reads raw text (token heavy). | Reads **Symbols/Outlines** (token efficient). |
| **Editing** | Suggests code blocks for you to paste. | **Applies edits directly** to files with precision. |
| **Refactoring**| Manual (Find & Replace). | **Automated** (Rename Symbol, Update References). |
| **Architecture**| Guesses based on snippets. | **Understands** via `onboarding` and memory. |

## 4. Workflow Example: Refactoring a DAO

**Goal**: Refactor `Dao_Inventory` to use Dependency Injection.

1.  **Explore**:
    *   User: "How is `Dao_Inventory` currently implemented?"
    *   Serena: `get_symbols_overview("Data/Dao_Inventory.cs")` -> Sees it's a static class.
2.  **Plan**:
    *   User: "Create an interface `IDao_Inventory` based on the public methods."
    *   Serena: `find_symbol("Dao_Inventory")` -> Reads methods -> Generates Interface code.
3.  **Edit**:
    *   User: "Extract the interface to a new file and make `Dao_Inventory` implement it."
    *   Serena: `create_file("Data/IDao_Inventory.cs")` -> `replace_symbol_body("Dao_Inventory")` (to add `: IDao_Inventory` and remove `static`).
4.  **Verify**:
    *   User: "Find all references to `Dao_Inventory` so we can update them to use `IDao_Inventory`."
    *   Serena: `find_referencing_symbols("Dao_Inventory")` -> Returns list of Forms using it.
