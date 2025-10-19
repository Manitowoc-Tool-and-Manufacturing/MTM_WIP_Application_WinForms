# Installation Instructions

## Update VS Code Settings

You already have an MCP server named "mtm-workflow" configured. To use this new Node.js version, update your VS Code settings:

1. Open VS Code Settings (`Ctrl+,`)
2. Search for "MCP"
3. Click "Edit in settings.json"
4. **Replace** the existing `mtm-workflow` entry:

**BEFORE (Python version):**
```json
"mtm-workflow": {
    "command": "python",
    "args": ["-m", "mtm_workflow_mcp.server"],
    "type": "stdio"
}
```

**AFTER (Node.js version):**
```json
"mtm-workflow": {
    "command": "node",
    "args": [
        "C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/.mcp/mtm-workflow/dist/index.js"
    ],
    "type": "stdio"
}
```

5. **Save** the settings file
6. **Restart VS Code** to load the new server

## Alternative: Run Both Servers

If you want to keep both, rename this one:

```json
"mtm-workflow-node": {
    "command": "node",
    "args": [
        "C:/Users/johnk/source/repos/MTM_WIP_Application_WinForms/.mcp/mtm-workflow/dist/index.js"
    ],
    "type": "stdio"
}
```

## Verify Installation

After restart, you should see these tools:
- ✅ `check_checklists` - Analyze markdown checklist completion
- ✅ `validate_dao_patterns` - Validate C# DAO files for MTM standards

The Python server had:
- `check_checklists_status` (different name)
- `validate_stored_procedures` (different functionality)

Choose which implementation you prefer!
