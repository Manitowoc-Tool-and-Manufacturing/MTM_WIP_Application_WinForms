# Installation Workflow Summary

1. **Clone repository** – `git clone` the MTM WinForms solution.
2. **Restore Node dependencies** – `npm install` inside `.mcp/mtm-workflow`.
3. **Build** – `npm run build` to produce the `dist/` output.
4. **Sync to global** – mirror the folder to `C:\.mcp\mtm-workflow`.
5. **Configure VS Code** – point MCP settings to the global copy.
6. **Verify** – run `node C:\.mcp\mtm-workflow\dist\index.js --list-tools`.

Re-run steps 3–6 whenever the toolset changes.
