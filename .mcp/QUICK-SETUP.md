# Quick Setup Checklist

1. `npm install`
2. `npm run build`
3. `robocopy .mcp\mtm-workflow C:\.mcp\mtm-workflow /MIR /XD node_modules .git`
4. Update VS Code MCP settings to use `C:\.mcp\mtm-workflow\dist\index.js`
5. Restart VS Code
6. Run a sanity command:
   ```powershell
   node C:\.mcp\mtm-workflow\dist\index.js --list-tools
   ```
