# MTM Workflow MCP Server – Setup Guide

Use this guide when bootstrapping a new machine or recovering a broken environment. The process keeps the development workspace (`repo/.mcp`) and the global runtime (`C:\.mcp`) in lockstep.

## 1. Prerequisites

- Node.js 18+ (`node --version`)
- npm (`npm --version`)
- PowerShell execution policy that allows running local scripts (`Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass` if needed)

## 2. Build the Development Copy

```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow
npm install
npm run build
```

This produces `dist/index.js`, which the MCP runtime executes.

## 3. Sync to the Global Runtime

```powershell
$repo = 'C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow'
$global = 'C:\.mcp\mtm-workflow'
New-Item -ItemType Directory -Force -Path $global | Out-Null
robocopy $repo $global /MIR /XD node_modules .git /NFL /NDL /NJH /NJS
```

- `/MIR` mirrors the folders so stale files are removed.
- `node_modules` is excluded; the global copy uses the built JavaScript in `dist/`.

## 4. Register the Server with VS Code

Update `%APPDATA%\Code\User\settings.json` (or `mcp.json` if you prefer MCP-specific config):

```json
{
  "mcpServers": {
    "mtm-workflow": {
      "command": "node",
      "args": ["C:/./mcp/mtm-workflow/dist/index.js"],
      "cwd": "C:/./mcp/mtm-workflow"
    }
  }
}
```

*A future improvement is to expose a packaged command (see `GLOBAL-INSTALL.md`).*

Restart VS Code after editing the settings file.

## 5. Validate the Installation

```powershell
node C:\.mcp\mtm-workflow\dist\index.js --list-tools
```

You should see every tool listed in `MCP-TOOLS-REFERENCE.md`. If not, repeat the build and sync steps.

## 6. Daily Development Loop

1. Modify or add tools inside `.mcp/mtm-workflow/src`.
2. Run `npm run build`.
3. Mirror the folder to `C:\.mcp` using the robocopy command above.
4. Reload VS Code (`Ctrl+Shift+P` → “Developer: Reload Window”).

Keeping both directories in sync prevents the “works locally but not globally” class of bugs.
