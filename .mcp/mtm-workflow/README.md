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

## Future Enhancements

Planned tools:
- `analyze_stored_procedures` - Scan stored procedures for compliance
- `compare_databases` - Detect schema drift between Current/Updated databases
- `suggest_refactoring` - Identify refactoring opportunities
- `generate_dao_wrapper` - Auto-generate DAO code from stored procedures

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
