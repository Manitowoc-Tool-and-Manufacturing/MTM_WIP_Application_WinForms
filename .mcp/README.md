# MTM Workflow MCP Server

This folder stores the development copy of the MTM workflow Model Context Protocol server. The live server that VS Code executes resides in `C:\.mcp`. Keep the two directories mirrored at all times.

## Key Documents

- `MCP-TOOLS-REFERENCE.md` – authoritative list of available tools.
- `SETUP-GUIDE.md` – end-to-end environment setup.
- `GLOBAL-INSTALL.md` – options for copying the server to the global runtime.
- `INSTALLATION.md` – quick summary of the installation workflow.
- `QUICK-SETUP.md` – short checklist for experienced contributors.

## Tool Development Flow

1. Implement new logic under `mtm-workflow/src/`.
2. Export the tool from `src/index.ts`.
3. Update documentation in this folder.
4. `npm run build` to refresh `dist/`.
5. `robocopy` the folder into `C:\.mcp`.
6. Reload VS Code and run the new tool via MCP.

## Sample Artifacts

- `seed-config.sample.json` – example configuration for the `generate_test_seed_sql` and `verify_test_seed` tools. Copy and modify for new test datasets and verification steps.
- `artifacts/` – generated outputs (e.g., SQL scripts) are written here when running tools locally.

## Branch Safety

Never modify files inside `C:\.mcp` by hand. Always edit the repository copy, then mirror the results. This avoids configuration drift and ensures reproducible builds.
