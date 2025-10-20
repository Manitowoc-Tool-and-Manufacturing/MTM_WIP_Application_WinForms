# Installing the MTM Workflow MCP Server Globally

The global copy in `C:\.mcp` is what VS Code loads. Use these instructions whenever you deploy updated tooling.

## Option A – Standard Sync (Recommended)

1. Build the repo copy:
   ```powershell
   cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow
   npm run build
   ```
2. Mirror the folder to `C:\.mcp`:
   ```powershell
   robocopy C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow C:\.mcp\mtm-workflow /MIR /XD node_modules .git /NFL /NDL /NJH /NJS
   ```
3. Reload VS Code.

This keeps the runtime byte-for-byte identical to the repository copy (minus `node_modules`).

## Option B – npm Link (Development Convenience)

If you prefer to avoid repeated copies while iterating:

```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow
npm install
npm run build
npm link
```

Update VS Code settings to use `mtm-workflow-mcp`:

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

When you finish development, still mirror the directory so `C:\.mcp` remains the single runtime location.

## Option C – npm Global Package

Package metadata in `package.json` already defines a `bin` entry. To install globally without linking:

```powershell
cd C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow
npm install -g .
```

VS Code command is again `mtm-workflow-mcp`. Use `npm update -g mtm-workflow-mcp` to refresh.

## Keeping Directories Identical

- After every merge or tool change, run the robocopy command.
- Periodically diff the directories:
  ```powershell
  robocopy C:\Users\johnk\source\repos\MTM_WIP_Application_WinForms\.mcp\mtm-workflow C:\.mcp\mtm-workflow /L /NJH /NJS /NDL /NP
  ```
  The `/L` flag lists differences without copying.
- Never edit files directly inside `C:\.mcp`; treat it as read-only runtime output.
