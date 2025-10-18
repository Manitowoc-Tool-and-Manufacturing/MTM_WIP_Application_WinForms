# MTM Workflow MCP Server

Model Context Protocol (MCP) server providing workflow automation tools for the MTM WIP Application.

## Tools Provided

### 1. check_checklists
Analyze markdown checklist files to determine completion status.

**Usage:**
```
check_checklists(checklist_dir: "C:/path/to/specs/feature/checklists")
```

**Returns:**
- Summary table with completion counts per checklist
- Overall PASS/FAIL status
- Detailed breakdown of incomplete items

### 2. validate_dao_patterns
Validate C# DAO files for compliance with MTM coding standards.

**Usage:**
```
validate_dao_patterns(
  dao_dir: "C:/path/to/Data",
  recursive: true
)
```

**Checks:**
- ✅ Region organization (#region structure)
- ✅ Helper_Database_StoredProcedure usage
- ✅ Async/await patterns
- ✅ Service_ErrorHandler usage (no MessageBox.Show)
- ✅ XML documentation (<summary> tags)
- ❌ Anti-patterns (.Result, .Wait(), blocking calls)

**Returns:**
- Per-file validation status
- Issue counts by severity (error, warning, info)
- Detailed issue descriptions with line numbers

### 3. analyze_stored_procedures
Scan SQL stored procedure files for compliance with MTM standards.

**Usage:**
```
analyze_stored_procedures(
  procedures_dir: "C:/path/to/Database/UpdatedStoredProcedures",
  recursive: true
)
```

**Checks:**
- ✅ Required output parameters (p_Status INT, p_ErrorMsg VARCHAR)
- ✅ Transaction management (START TRANSACTION, COMMIT, ROLLBACK)
- ✅ Error handling (DECLARE HANDLER)
- ✅ Parameter naming conventions (p_ prefix)
- ❌ SQL injection vulnerabilities (CONCAT with EXECUTE)

**Returns:**
- Per-procedure compliance status
- Issue counts by severity
- Detailed compliance report

### 4. compare_databases
Detect schema drift between Current and Updated database directories.

**Usage:**
```
compare_databases(
  current_dir: "C:/path/to/Database/CurrentDatabase",
  updated_dir: "C:/path/to/Database/UpdatedDatabase"
)
```

**Returns:**
- Tables added/removed/modified
- Stored procedures added/removed/modified
- Detailed difference report
- Schema drift summary

### 5. generate_dao_wrapper
Auto-generate C# DAO wrapper code from a stored procedure file.

**Usage:**
```
generate_dao_wrapper(
  procedure_file: "C:/path/to/procedure.sql",
  output_dir: "C:/path/to/Data"  // Optional
)
```

**Generates:**
- Complete DAO method with proper patterns
- Helper_Database_StoredProcedure usage
- Error handling with Service_ErrorHandler
- XML documentation
- Parameter mapping

**Returns:**
- Generated C# code
- Parameter list
- Optional: Writes file to output_dir

### 6. validate_error_handling
Check C# source files for proper error handling patterns.

**Usage:**
```
validate_error_handling(
  source_dir: "C:/path/to/source",
  recursive: true
)
```

**Checks:**
- ❌ MessageBox.Show usage (anti-pattern)
- ✅ Service_ErrorHandler usage
- ✅ Try-catch blocks in async methods
- ⚠️ Empty or minimal catch blocks

**Returns:**
- Per-file error handling statistics
- MessageBox.Show vs Service_ErrorHandler counts
- Detailed issue report

### 7. analyze_dependencies
Map stored procedure call hierarchies and dependency graphs.

**Usage:**
```
analyze_dependencies(
  procedures_dir: "C:/path/to/Database/UpdatedStoredProcedures"
)
```

**Returns:**
- Root procedures (entry points)
- Leaf procedures (terminal operations)
- Circular dependency detection
- Visual dependency tree
- Call depth statistics

### 8. check_xml_docs
Validate XML documentation coverage for C# code.

**Usage:**
```
check_xml_docs(
  source_dir: "C:/path/to/source",
  recursive: true,
  min_coverage: 80
)
```

**Returns:**
- Coverage percentage per file
- Total documented vs undocumented members
- Detailed list of missing documentation
- Pass/fail based on minimum coverage threshold

## Installation

1. **Install dependencies:**
```bash
cd .mcp/mtm-workflow
npm install
```

2. **Build the server:**
```bash
npm run build
```

3. **Configure VS Code:**

Add to your VS Code `settings.json`:

```json
{
  "mcp.servers": {
    "mtm-workflow": {
      "command": "node",
      "args": [
        "C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/.mcp/mtm-workflow/dist/index.js"
      ],
      "cwd": "C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms"
    }
  }
}
```

4. **Restart VS Code** to activate the MCP server.

## Development

**Watch mode (auto-rebuild on changes):**
```bash
npm run watch
```

**Test the server:**
```bash
npm run dev
```

## Usage Examples

### Check Checklists Before Implementation
```
check_checklists("C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/specs/002-003-database-layer-complete/checklists")
```

### Validate All DAOs
```
validate_dao_patterns(
  dao_dir: "C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/Data",
  recursive: false
)
```

### Validate DAOs Recursively (including subdirectories)
```
validate_dao_patterns(
  dao_dir: "C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms",
  recursive: true
)
```

## Architecture

```
.mcp/mtm-workflow/
├── src/
│   ├── index.ts              # MCP server entry point
│   └── tools/
│       ├── check-checklists.ts      # Checklist analyzer
│       └── validate-dao-patterns.ts # DAO validator
├── dist/                     # Compiled JavaScript (generated)
├── package.json
├── tsconfig.json
└── README.md
```

## Using Tools in Custom Prompts

All MTM SpecKit prompts (in `.github/prompts/`) have been updated to utilize these MCP tools. Here's how to integrate them:

### 1. Declare Required Tools

Add this section at the top of your prompt file:

```markdown
## Required MCP Tools

This prompt requires the following MCP tools from the **mtm-workflow** server:
- `check_checklists` - Validate checklist completion status
- `validate_dao_patterns` - Verify DAO implementation correctness
```

### 2. Use Tools in Workflow Steps

Replace manual analysis with MCP tool calls:

**Before:**
```markdown
- Scan all checklist files
- Count total, completed, and incomplete items
- Generate status table
```

**After:**
```markdown
**USE MCP TOOL**: `mcp_mtm-workflow_check_checklists(checklist_dir: "path/to/checklists")`
```

### 3. Integrate Tool Results

Tools return structured data that can guide next steps:

```markdown
**USE MCP TOOL**: `mcp_mtm-workflow_check_xml_docs(source_dir: "Data", min_coverage: 80)`

If coverage < 80%:
- Identify files needing documentation from tool output
- Prioritize based on public API surface
- Generate XML comments for missing members
```

### Example: Database Operation Prompt

```markdown
1. **USE MCP TOOL**: `mcp_mtm-workflow_analyze_stored_procedures()`
   - Verify procedure compliance
   
2. **USE MCP TOOL**: `mcp_mtm-workflow_generate_dao_wrapper()`
   - Generate initial code template
   
3. Customize generated code
   
4. **USE MCP TOOL**: `mcp_mtm-workflow_validate_dao_patterns()`
   - Verify implementation correctness
```

## Future Enhancements

All planned tools have been implemented! ✅
- ~~`suggest_refactoring`~~ ✅ IMPLEMENTED
- ~~`generate_unit_tests`~~ ✅ IMPLEMENTED  
- ~~`analyze_performance`~~ ✅ IMPLEMENTED
- ~~`check_security`~~ ✅ IMPLEMENTED

## Troubleshooting

**Server not appearing in VS Code:**
1. Check VS Code output panel (View → Output → MCP)
2. Verify the `node` command is in your PATH
3. Ensure the `dist/index.js` path is correct
4. Restart VS Code

**TypeScript compilation errors:**
```bash
npm install
npm run build
```

**Runtime errors:**
- Check that file paths are absolute
- Verify directory permissions
- Review error messages in VS Code output panel
