# Quick Setup: Making mtm-workflow-mcp Available Globally

## ✅ Already Completed

The MCP server has been successfully installed globally and is now available to ALL VS Code workspaces!

## Current Status

- ✅ Package built: `dist/index.js` exists
- ✅ Globally linked: `npm link` completed
- ✅ Command available: `mtm-workflow-mcp` is in your PATH
- ✅ 14 tools available including the new `validate_ui_scaling`

## Next Step: Update VS Code User Settings

To make the MCP server available in ALL VS Code workspaces (not just this project):

### 1. Open VS Code User Settings

**Windows**: Press `Ctrl+Shift+P` → Type "Preferences: Open User Settings (JSON)"

**File Location**: `C:\Users\johnk\AppData\Roaming\Code\User\settings.json`

### 2. Add or Update MCP Server Configuration

Add this to your user `settings.json`:

```json
{
  "mcpServers": {
    "mtm-workflow": {
      "command": "mtm-workflow-mcp",
      "args": []
    }
  }
}
```

**IMPORTANT**: This goes in **User Settings**, not Workspace Settings!

### 3. Restart VS Code

After updating settings:
1. Close VS Code completely
2. Reopen VS Code
3. The MCP server will now be available in ALL workspaces

## Verify Installation

### Test 1: Check Command Works
```powershell
mtm-workflow-mcp --version
```
Should show the server info.

### Test 2: List Tools
```powershell
echo '{"jsonrpc":"2.0","id":1,"method":"tools/list"}' | mtm-workflow-mcp
```
Should list all 14 tools.

### Test 3: Use in VS Code
After restart, the following tools should be available in any workspace:
- `mcp_mtm-workflow_validate_ui_scaling` ← NEW!
- `mcp_mtm-workflow_validate_dao_patterns`
- `mcp_mtm-workflow_check_security`
- `mcp_mtm-workflow_analyze_performance`
- And 10 more...

## Available Tools

| Tool | Description |
|------|-------------|
| `validate_ui_scaling` | **NEW** - Validate WinForms DPI/resolution consistency |
| `validate_dao_patterns` | Check DAO compliance with MTM standards |
| `analyze_stored_procedures` | Validate stored procedure compliance |
| `validate_error_handling` | Check error handling patterns |
| `check_security` | Security vulnerability scanning |
| `analyze_performance` | Performance bottleneck identification |
| `check_xml_docs` | XML documentation coverage |
| `suggest_refactoring` | AI-powered refactoring suggestions |
| `generate_unit_tests` | Auto-generate test scaffolding |
| `generate_dao_wrapper` | Generate DAO from stored procedures |
| `compare_databases` | Detect schema drift |
| `analyze_dependencies` | Map stored procedure dependencies |
| `check_checklists` | Analyze markdown checklist completion |
| **Speckit Tools** (6 tools) | Task management, build validation, etc. |

## Usage Example

Once configured globally, you can use the tools in ANY workspace:

```typescript
// In any VS Code workspace with Copilot
// Ask: "Validate UI scaling for my Forms directory"

// Copilot will use:
mcp_mtm-workflow_validate_ui_scaling({
  source_dir: "path/to/Forms",
  recursive: true
})
```

## Updating the Server

When you make changes to the MCP server:

```powershell
cd C:\.mcp\mtm-workflow

# 1. Rebuild
npm run build

# 2. Changes are automatic (npm link keeps it in sync)
# No need to reinstall!

# 3. Reload VS Code
# Ctrl+Shift+P → "Developer: Reload Window"
```

## Benefits of Global Installation

✅ **Available Everywhere**: Works in ALL VS Code workspaces
✅ **Single Source of Truth**: One installation, maintained in one place
✅ **Easy Updates**: Just rebuild, no reinstall needed (with npm link)
✅ **Shareable**: Other developers can install with `npm link` from your project
✅ **Publishable**: Can publish to NPM for wider distribution

## Troubleshooting

### "Command not found" after npm link

**Fix**: Add NPM global bin to PATH:
```powershell
$env:PATH += ";C:\Users\johnk\AppData\Roaming\npm"
```

### Tools not showing up in VS Code

**Fix**: 
1. Verify settings.json is in **User Settings** (not Workspace)
2. Restart VS Code completely (close all windows)
3. Check Output panel: View → Output → "MCP Servers"

### "Cannot find module" error

**Fix**: Reinstall dependencies:
```powershell
cd .mcp\mtm-workflow
npm install
npm run build
```

## Configuration Files

**Global Command**: `C:\Users\johnk\AppData\Roaming\npm\mtm-workflow-mcp.cmd`
**Source Code**: `C:\.mcp\mtm-workflow\`
**VS Code Settings**: `C:\Users\johnk\AppData\Roaming\Code\User\settings.json`

---

## Ready to Use! 🎉

The mtm-workflow MCP server is now globally installed. Just update your VS Code **User Settings** and restart VS Code to make it available in all workspaces!
