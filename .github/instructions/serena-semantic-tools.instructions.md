---
description: Serena semantic coding tools - efficient code navigation, symbol-level editing, and intelligent codebase exploration
applyTo: '**/*.cs'
---

# Serena Semantic Coding Tools

## Overview

**Serena** is a semantic coding agent toolkit that provides IDE-like capabilities for efficient code navigation and editing. It enables symbol-level operations (classes, methods, properties) rather than file-based text manipulation, significantly reducing token usage and improving accuracy.

**Official Documentation**: https://oraios.github.io/serena/

## When to Use Serena

### ✅ Use Serena For:
- **Large codebases** - Navigate efficiently without reading entire files
- **Symbol discovery** - Find classes, methods, properties by name or pattern
- **Relationship analysis** - Discover where symbols are referenced
- **Precise editing** - Replace entire methods/classes accurately
- **Pattern searches** - Regex-based searches across specific file types
- **Architecture exploration** - Understand code structure without reading everything

### ❌ Don't Use Serena For:
- **Single-line edits** - Use standard editing tools
- **Non-code files** - Serena is optimized for code (C#, Java, Python, etc.)
- **Simple file reading** - Use `read_file` for small, non-code files
- **Creating new files from scratch** - Use standard file creation tools

## Core Serena Tools

### Symbol Discovery

#### `find_symbol` - Find classes, methods, properties
```
# Find a class and its immediate methods
find_symbol("Dao_Inventory", depth=1, include_body=false)

# Read a specific method's body
find_symbol("Dao_Inventory/GetAllAsync", include_body=true)

# Substring matching for partial names
find_symbol("GetAll", substring_matching=true)

# Restrict search to specific file
find_symbol("Dao_Inventory", relative_path="Data/Dao_Inventory.cs")

# Find specific overload by index
find_symbol("MyClass/MyMethod[0]", include_body=true)
```

**Documentation**: https://oraios.github.io/serena/03-tools/010_find-symbol.html

#### `get_symbols_overview` - High-level file structure
```
# Get all top-level symbols in a file
get_symbols_overview("Data/Dao_Inventory.cs", depth=0)

# Include immediate children (e.g., methods in a class)
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
```

**Best Practice**: Use this FIRST when exploring a new file to understand its structure before diving into specific symbols.

**Documentation**: https://oraios.github.io/serena/03-tools/030_get-symbols-overview.html

#### `find_referencing_symbols` - Find all usages of a symbol
```
# Find where a method is called
find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")

# Find references to a class
find_referencing_symbols("Dao_Inventory", "Data/Dao_Inventory.cs")
```

**Use Case**: Before modifying a method, find all places it's used to ensure backward compatibility or update all references.

**Documentation**: https://oraios.github.io/serena/03-tools/020_find-referencing-symbols.html

### Code Editing

#### `replace_symbol_body` - Replace entire symbol definition
```
# Replace a method implementation
replace_symbol_body(
    name_path="Dao_Inventory/GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    body=new_method_code
)

# Replace a class
replace_symbol_body(
    name_path="Dao_Inventory",
    relative_path="Data/Dao_Inventory.cs",
    body=new_class_code
)
```

**Important**: The `body` includes the complete symbol definition (signature + implementation for methods, or entire class definition).

**Documentation**: https://oraios.github.io/serena/03-tools/040_replace-symbol-body.html

#### `insert_after_symbol` - Insert code after a symbol
```
# Add a new method after an existing one
insert_after_symbol(
    name_path="Dao_Inventory/GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    body=new_method_code
)

# Add code at end of file (after last top-level symbol)
insert_after_symbol(
    name_path="Dao_Inventory",  # Last class in file
    relative_path="Data/Dao_Inventory.cs",
    body=new_code
)
```

**Documentation**: https://oraios.github.io/serena/03-tools/050_insert-after-symbol.html

#### `insert_before_symbol` - Insert code before a symbol
```
# Add a new method before an existing one
insert_before_symbol(
    name_path="Dao_Inventory/GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    body=new_method_code
)

# Add imports at start of file (before first symbol)
insert_before_symbol(
    name_path="Dao_Inventory",  # First class in file
    relative_path="Data/Dao_Inventory.cs",
    body="using System.Collections.Generic;\n"
)
```

**Documentation**: https://oraios.github.io/serena/03-tools/060_insert-before-symbol.html

#### `rename_symbol` - Rename across entire codebase
```
# Rename a method everywhere it's used
rename_symbol(
    name_path="GetAllAsync",
    relative_path="Data/Dao_Inventory.cs",
    new_name="GetAllInventoryAsync"
)
```

**Documentation**: https://oraios.github.io/serena/03-tools/070_rename-symbol.html

### File and Pattern Search

#### `find_file` - Locate files by name/pattern
```
# Find all DAO files
find_file("Dao_*.cs", relative_path="Data")

# Find a specific file
find_file("Dao_Inventory.cs", relative_path=".")
```

**Documentation**: https://oraios.github.io/serena/03-tools/080_find-file.html

#### `search_for_pattern` - Regex pattern search
```
# Find anti-pattern usage
search_for_pattern(
    substring_pattern="MessageBox\\.Show",
    restrict_search_to_code_files=true
)

# Search in specific directory
search_for_pattern(
    substring_pattern="ExecuteDataTableWithStatusAsync",
    relative_path="Data",
    restrict_search_to_code_files=true
)

# Include context lines
search_for_pattern(
    substring_pattern="Helper_Database_StoredProcedure",
    context_lines_before=2,
    context_lines_after=2
)
```

**Documentation**: https://oraios.github.io/serena/03-tools/090_search-for-pattern.html

#### `list_dir` - List directory contents
```
# List files and folders
list_dir("Data", recursive=false)

# Recursive listing
list_dir(".", recursive=true, skip_ignored_files=true)
```

**Documentation**: https://oraios.github.io/serena/03-tools/100_list-dir.html

## Workflow Patterns

### Exploring a New Component

1. **Get file structure**:
   ```
   get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
   ```

2. **Read specific methods**:
   ```
   find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
   find_symbol("Dao_Inventory/InsertAsync", include_body=true)
   ```

3. **Find dependencies**:
   ```
   find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")
   ```

### Modifying a Method

1. **Find the method**:
   ```
   find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
   ```

2. **Check where it's used**:
   ```
   find_referencing_symbols("GetAllAsync", "Data/Dao_Inventory.cs")
   ```

3. **Replace the method**:
   ```
   replace_symbol_body("Dao_Inventory/GetAllAsync", "Data/Dao_Inventory.cs", new_code)
   ```

### Adding a New Method

1. **Find where to insert**:
   ```
   get_symbols_overview("Data/Dao_Inventory.cs", depth=1)
   ```

2. **Insert after related method**:
   ```
   insert_after_symbol("Dao_Inventory/GetAllAsync", "Data/Dao_Inventory.cs", new_method)
   ```

### Finding Anti-Patterns

```
# Find direct MessageBox.Show usage (anti-pattern)
search_for_pattern("MessageBox\\.Show", restrict_search_to_code_files=true)

# Find direct MySqlConnection usage (anti-pattern)
search_for_pattern("new MySqlConnection", restrict_search_to_code_files=true)

# Find missing async/await
search_for_pattern("public.*Task.*\\(", restrict_search_to_code_files=true)
```

## Name Path Syntax

Symbols are identified by their **name path** within a file:

- **Class**: `MyClass`
- **Method**: `MyClass/MyMethod`
- **Nested class**: `OuterClass/InnerClass`
- **Property**: `MyClass/MyProperty`
- **Overloaded method**: `MyClass/MyMethod[0]` (0-based index)

### Name Path Patterns

- **Simple name**: `"GetAllAsync"` - Matches any symbol with that name
- **Relative path**: `"Dao_Inventory/GetAllAsync"` - Matches this path suffix
- **Absolute path**: `"/Dao_Inventory/GetAllAsync"` - Exact match only
- **Substring matching**: `"GetAll"` with `substring_matching=true` - Matches `GetAllAsync`, `GetAllData`, etc.

## Token Efficiency

### ❌ Inefficient Approach
```
# Reading entire files repeatedly
read_file("Data/Dao_Inventory.cs", 1, 500)
read_file("Data/Dao_User.cs", 1, 500)
read_file("Data/Dao_Transactions.cs", 1, 500)
# Result: 10,000+ tokens consumed
```

### ✅ Efficient Approach with Serena
```
# Get overview first
get_symbols_overview("Data/Dao_Inventory.cs", depth=1)

# Read only what you need
find_symbol("Dao_Inventory/GetAllAsync", include_body=true)
find_symbol("Dao_Inventory/InsertAsync", include_body=true)
# Result: 500-1000 tokens consumed
```

## Project-Specific Best Practices

### For MTM WIP Application

1. **DAO Exploration**: Always use `get_symbols_overview` first to see all methods
2. **Pattern Validation**: Use `search_for_pattern` to find architectural violations:
   - `MessageBox\\.Show` (should use `Service_ErrorHandler`)
   - `new MySqlConnection` (should use `Helper_Database_StoredProcedure`)
3. **Method Editing**: Use `replace_symbol_body` for entire method rewrites
4. **Adding Methods**: Use `insert_after_symbol` to maintain file organization
5. **Refactoring**: Use `find_referencing_symbols` before changing method signatures

### Memory Access

Serena maintains project memories with coding standards:
```
# Read project standards
read_memory("architectural_patterns.md")
read_memory("code_style_conventions.md")
read_memory("task_completion_checklist.md")

# List all memories
list_memories()
```

**Documentation**: https://oraios.github.io/serena/03-tools/110_memory-tools.html

## Common Pitfalls

### ❌ Don't:
- Use Serena for single-line edits (use standard tools)
- Read entire files when you only need one method
- Forget to check `find_referencing_symbols` before modifying
- Use `relative_path` parameter incorrectly (must be file or directory path)

### ✅ Do:
- Start with `get_symbols_overview` for new files
- Use `find_symbol` with `include_body=true` only when needed
- Check references before breaking changes
- Restrict searches with `relative_path` when possible
- Use `substring_matching` for fuzzy name searches

## Advanced Features

### Symbol Filtering by Kind
```
# Find only classes
find_symbol("*", include_kinds=[5])  # 5 = class

# Find only methods
find_symbol("*", include_kinds=[6])  # 6 = method

# Exclude constructors
find_symbol("MyClass/*", exclude_kinds=[9])  # 9 = constructor
```

**Kind Reference**:
- 5 = Class
- 6 = Method
- 7 = Property
- 8 = Field
- 9 = Constructor
- 12 = Function
- 13 = Variable

**Full list**: https://oraios.github.io/serena/03-tools/010_find-symbol.html#symbol-kinds

### Thinking Tools

Use these to organize your workflow:
- `think_about_collected_information` - After gathering data
- `think_about_task_adherence` - Before making changes
- `think_about_whether_you_are_done` - Before finishing

**Documentation**: https://oraios.github.io/serena/03-tools/120_thinking-tools.html

## Dashboard

Monitor Serena usage in real-time:
- **URL**: http://127.0.0.1:24282/dashboard/
- **Features**: Tool usage stats, active tools, memory list, execution queue

**Documentation**: https://oraios.github.io/serena/02-usage/060_dashboard.html

## Further Reading

- **Getting Started**: https://oraios.github.io/serena/02-usage/010_quick-start.html
- **Configuration**: https://oraios.github.io/serena/02-usage/050_configuration.html
- **All Tools**: https://oraios.github.io/serena/03-tools/000_intro.html
- **Best Practices**: https://oraios.github.io/serena/04-best-practices/000_intro.html
- **GitHub Repository**: https://github.com/oraios/serena

## Summary

**Key Principle**: Use Serena to navigate code at the symbol level (classes, methods) rather than reading entire files. This dramatically reduces token consumption and improves accuracy for large codebases.

**Workflow**: Overview → Find → Read (only what you need) → Edit (precisely) → Verify (references)

**MTM WIP Application**: Serena is especially valuable for navigating the 20+ DAO files, finding Service_ErrorHandler usages, and verifying architectural compliance across 300+ files.
