# Installing MTM Workflow MCP Server Globally

This guide shows how to make the mtm-workflow MCP server available to ALL VS Code workspaces, not just this project.

## Option 1: Install as Global NPM Package (Recommended)

### Step 1: Update package.json for Global Installation

The `package.json` is already configured with:
- `"name": "mtm-workflow-mcp"` - package name
- `"bin": { "mtm-workflow-mcp": "./dist/index.js" }` - executable entry point
- Proper dependencies and build scripts

### Step 2: Build the Package

```powershell
cd C:\.mcp\mtm-workflow
npm run build
```

### Step 3: Install Globally via NPM

```powershell
# From inside the mtm-workflow directory
npm install -g .

# OR if you want to link for development (changes reflect immediately)
npm link
```

This installs the package globally and creates a `mtm-workflow-mcp` command.

### Step 4: Update VS Code User Settings

Update your **User Settings** (not workspace settings) to use the global installation:

**File**: `%APPDATA%\Code\User\settings.json`

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

**Benefits**:
- ✅ Available to ALL VS Code workspaces
- ✅ Single installation to maintain
- ✅ Easy to update (`npm update -g mtm-workflow-mcp`)
- ✅ Can be uninstalled (`npm uninstall -g mtm-workflow-mcp`)

---

## Option 2: Use Absolute Path in User Settings

If you don't want to install globally, you can reference the project directly from user settings:

**File**: `%APPDATA%\Code\User\settings.json`

```json
{
  "mcpServers": {
    "mtm-workflow": {
      "command": "node",
      "args": [
        "C:\\Users\\johnk\\source\\repos\\MTM_WIP_Application_WinForms\\.mcp\\mtm-workflow\\dist\\index.js"
      ]
    }
  }
}
```

**Benefits**:
- ✅ No installation needed
- ✅ Easy to update (just rebuild)

**Drawbacks**:
- ⚠️ Tied to specific project location
- ⚠️ If project moves, settings break

---

## Option 3: Publish to NPM Registry (For Sharing with Others)

If you want to share this MCP server with others:

### Step 1: Create NPM Account
```powershell
npm login
```

### Step 2: Update package.json Metadata
```json
{
  "name": "@dorotel/mtm-workflow-mcp",
  "version": "1.0.0",
  "description": "MCP server for MTM WIP Application workflow automation",
  "keywords": ["mcp", "mtm", "workflow", "manufacturing", "validation"],
  "author": "Your Name <email@example.com>",
  "license": "MIT",
  "repository": {
    "type": "git",
    "url": "https://github.com/Dorotel/MTM_WIP_Application_WinForms.git"
  },
  "homepage": "https://github.com/Dorotel/MTM_WIP_Application_WinForms/tree/master/.mcp/mtm-workflow"
}
```

### Step 3: Publish to NPM
```powershell
cd C:\.mcp\mtm-workflow
npm publish --access public
```

### Step 4: Anyone Can Install
```powershell
npm install -g @dorotel/mtm-workflow-mcp
```

Then users add to their VS Code settings:
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

---

## Option 4: Create Windows Installer (Advanced)

Create a standalone installer using `pkg` to bundle Node.js:

### Step 1: Install pkg
```powershell
npm install -g pkg
```

### Step 2: Create Executable
```powershell
cd C:\.mcp\mtm-workflow
pkg . --targets node18-win-x64 --output mtm-workflow-mcp.exe
```

### Step 3: Move to System Path
```powershell
# Option A: Add to PATH
move mtm-workflow-mcp.exe C:\Program Files\MTM\
# Then add C:\Program Files\MTM\ to system PATH

# Option B: Move to existing PATH location
move mtm-workflow-mcp.exe C:\Windows\System32\
```

### Step 4: VS Code User Settings
```json
{
  "mcpServers": {
    "mtm-workflow": {
      "command": "mtm-workflow-mcp.exe",
      "args": []
    }
  }
}
```

**Benefits**:
- ✅ No Node.js required on target machine
- ✅ Single .exe file
- ✅ Easy to distribute

---

## Recommended Approach for You

Based on your setup, I recommend **Option 1 (Global NPM Install with npm link)**:

### Quick Setup Commands

```powershell
# 1. Navigate to MCP server directory
cd C:\.mcp\mtm-workflow

# 2. Build the server
npm run build

# 3. Link globally (for development - changes reflect immediately)
npm link

# 4. Verify installation
mtm-workflow-mcp --version  # Should show version

# 5. Test the server works
echo '{"jsonrpc":"2.0","id":1,"method":"tools/list"}' | mtm-workflow-mcp
```

### Update VS Code User Settings

**File**: `C:\Users\johnk\AppData\Roaming\Code\User\settings.json`

```json
{
  "mcpServers": {
    "mtm-workflow": {
      "command": "mtm-workflow-mcp",
      "args": [],
      "env": {}
    }
  }
}
```

### Verify in VS Code

1. Restart VS Code
2. Open Command Palette (Ctrl+Shift+P)
3. Type "MCP" - you should see MCP-related commands
4. The tools should be available: `mcp_mtm-workflow_validate_ui_scaling`, etc.

---

## Troubleshooting

### "mtm-workflow-mcp: command not found"

**Issue**: Global NPM bin directory not in PATH

**Fix**:
```powershell
# Find NPM global bin directory
npm config get prefix
# Output: C:\Users\johnk\AppData\Roaming\npm

# Add to PATH if not already there
$env:PATH += ";C:\Users\johnk\AppData\Roaming\npm"
```

### "Cannot find module '@modelcontextprotocol/sdk'"

**Issue**: Dependencies not installed

**Fix**:
```powershell
cd C:\.mcp\mtm-workflow
npm install
npm run build
npm link
```

### VS Code Not Loading MCP Server

**Issue**: VS Code hasn't detected changes

**Fix**:
1. Reload VS Code window (Ctrl+Shift+P → "Reload Window")
2. Check Output panel (View → Output → Select "MCP Servers")
3. Verify settings.json syntax is correct

### Tools Not Showing Up

**Issue**: MCP server running but tools not registered

**Fix**:
```powershell
# Test MCP server directly
echo '{"jsonrpc":"2.0","id":1,"method":"tools/list"}' | mtm-workflow-mcp

# Should output list of tools including validate_ui_scaling
```

---

## Updating the Server After Changes

When you make changes to the MCP server:

```powershell
# 1. Rebuild
cd C:\.mcp\mtm-workflow
npm run build

# 2. If using npm link, changes are automatic
# If using npm install -g, reinstall:
npm install -g .

# 3. Reload VS Code window
# Ctrl+Shift+P → "Reload Window"
```

---

## Summary

**Best Choice**: Option 1 with `npm link`
- Available globally
- Changes reflect immediately (no reinstall)
- Works across all VS Code workspaces
- Easy to maintain

**Commands**:
```powershell
cd .mcp\mtm-workflow
npm run build
npm link
# Update VS Code User settings.json
# Restart VS Code
```
