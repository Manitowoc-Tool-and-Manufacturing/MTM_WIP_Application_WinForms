# MCP Tools Reference for MTM Project

**Generated:** 2025-10-18T16:56:00.722Z
**Workspace:** c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms

---

## MCP Extension Status

```clojure
{:found false, :message "MCP extension not found"}
```

## Configured MCP Servers

```clojure
{:awesome-copilot {:command "docker", :args ["run" "-i" "--rm" "ghcr.io/microsoft/mcp-dotnet-samples/awesome-copilot:latest"], :type "stdio"}, :mtm-workflow {:type "stdio", :command "node", :args ["C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/.mcp/mtm-workflow/dist/index.js"]}}
```

### Server Details:

#### awesome-copilot
- **Command:** `docker`
- **Args:** ["run" "-i" "--rm" "ghcr.io/microsoft/mcp-dotnet-samples/awesome-copilot:latest"]
- **Type:** stdio

#### mtm-workflow
- **Command:** `node`
- **Args:** ["C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/.mcp/mtm-workflow/dist/index.js"]
- **Type:** stdio

## MCP-Related Extensions

*No MCP-related extensions found*

## Known MTM Workflow Tools

### mtm-workflow Server

#### check_checklists
- **Purpose:** Analyze markdown checklist completion status
- **Input:** `checklist_dir` (absolute path)
- **Output:** Table with total/completed/incomplete counts
- **Example:** `check_checklists("C:/...specs/.../checklists")`

#### validate_dao_patterns
- **Purpose:** Validate C# DAO files for MTM coding standards
- **Input:** `dao_dir` (absolute path), `recursive` (bool)
- **Output:** Validation results with issues by severity
- **Checks:**
  - Region organization
  - Helper_Database_StoredProcedure usage
  - Async/await patterns
  - Service_ErrorHandler usage
  - XML documentation
  - Anti-patterns (MessageBox.Show, .Result, .Wait())

#### analyze_stored_procedures ✨ NEW
- **Purpose:** Scan SQL procedures for compliance with MTM standards
- **Input:** `procedures_dir` (absolute path), `recursive` (bool)
- **Output:** Compliance report with issues by severity
- **Checks:**
  - Required output parameters (p_Status, p_ErrorMsg)
  - Transaction management (COMMIT/ROLLBACK)
  - Error handling (DECLARE HANDLER)
  - Parameter naming (p_ prefix)
  - SQL injection risks

#### compare_databases ✨ NEW
- **Purpose:** Detect schema drift between Current and Updated databases
- **Input:** `current_dir`, `updated_dir` (absolute paths)
- **Output:** Detailed difference report
- **Returns:**
  - Tables/procedures added/removed/modified
  - Content change detection
  - Deployment recommendations

#### generate_dao_wrapper ✨ NEW
- **Purpose:** Auto-generate C# DAO code from stored procedure
- **Input:** `procedure_file` (absolute path), `output_dir` (optional)
- **Output:** Complete DAO method code
- **Generates:**
  - Helper_Database_StoredProcedure integration
  - Parameter mapping
  - Error handling
  - XML documentation

#### validate_error_handling ✨ NEW
- **Purpose:** Check C# files for proper error handling patterns
- **Input:** `source_dir` (absolute path), `recursive` (bool)
- **Output:** Error handling statistics and issue report
- **Checks:**
  - MessageBox.Show usage (anti-pattern)
  - Service_ErrorHandler usage
  - Try-catch in async methods
  - Empty catch blocks

#### analyze_dependencies ✨ NEW
- **Purpose:** Map stored procedure call hierarchies
- **Input:** `procedures_dir` (absolute path)
- **Output:** Dependency graph and statistics
- **Returns:**
  - Root/leaf procedure lists
  - Circular dependency detection
  - Visual dependency tree
  - Call depth analysis

#### check_xml_docs ✨ NEW
- **Purpose:** Validate XML documentation coverage in C# code
- **Input:** `source_dir` (absolute path), `recursive` (bool), `min_coverage` (number)
- **Output:** Coverage report with missing documentation details
- **Returns:**
  - Per-file coverage percentage
  - Undocumented member list
  - Pass/fail based on threshold

---

## Discovering More Tools

To find tools from other MCP servers:

1. Check the MCP output panel in VS Code (View → Output → MCP)
2. Review server documentation
3. Query server directly if it supports introspection

## Potential Future Tools for MTM

Based on project needs:

- ~~**analyze_stored_procedures**~~ ✅ **IMPLEMENTED**
- ~~**compare_databases**~~ ✅ **IMPLEMENTED**
- ~~**generate_dao_wrapper**~~ ✅ **IMPLEMENTED**
- ~~**validate_error_handling**~~ ✅ **IMPLEMENTED**
- ~~**analyze_dependencies**~~ ✅ **IMPLEMENTED**
- ~~**check_xml_docs**~~ ✅ **IMPLEMENTED**
- **suggest_refactoring** - AI-powered refactoring suggestions
- **generate_unit_tests** - Auto-generate test scaffolding
- **analyze_performance** - Identify performance bottlenecks in DAOs
- **check_security** - Security vulnerability scanning

