---
description: 'MCP server development patterns, tool implementation workflows, and VS Code integration'
applyTo: '**/.mcp/**/*.ts,**/mcp.json'
---

# MCP Development Memory

Model Context Protocol server development patterns, TypeScript implementation strategies, and VS Code tool discovery workflows.

## MySQL Stored Procedure Parsing with DEFINER Clause

When parsing MySQL stored procedures in TypeScript, account for the DEFINER clause that appears in real database exports:

```typescript
// ❌ FAILS: Doesn't handle DEFINER clause
const procedureRegex = /CREATE\s+PROCEDURE\s+`?(\w+)`?/i;

// ✅ WORKS: Handles both formats
const procedureRegex = /CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`?(\w+)`?/i;
```

**Pattern**: `CREATE DEFINER=\`root\`@\`localhost\` PROCEDURE \`procedure_name\``

Real stored procedure files from MySQL exports include the DEFINER clause. Regex patterns must handle:
- Optional DEFINER clause with backtick-quoted user and host
- Optional backticks around procedure name
- Case-insensitive CREATE/PROCEDURE keywords

Apply this pattern to all MCP tools that parse SQL files: `analyze-stored-procedures.ts`, `analyze-dependencies.ts`, `generate-dao-wrapper.ts`.

## MCP Tool Discovery and VS Code Caching

VS Code aggressively caches MCP tool lists. When new tools aren't appearing:

**Solution sequence:**
1. `workbench.mcp.resetCachedTools` - Clear VS Code's tool cache
2. `workbench.mcp.restartServer` with server name - Restart the specific MCP server
3. **Full VS Code restart** - Close and reopen VS Code completely (not just reload)

**Key insight**: Tool discovery happens at extension load time. Adding tools to a running server requires cache reset AND server restart. If that fails, only a full application restart guarantees fresh tool discovery.

**Joyride command pattern:**
```clojure
(vscode/commands.executeCommand "workbench.mcp.resetCachedTools")
(vscode/commands.executeCommand "workbench.mcp.restartServer" "server-name")
```

**Critical**: The MCP extension does NOT hot-reload tool definitions. Plan for full restart when adding new tools during development.

## MCP Tool Registration Checklist

When adding a new tool to an MCP server, update **all three locations** in `index.ts`:

1. **Import statement** (top of file):
   ```typescript
   import { newToolFunction } from "./tools/new-tool.js";
   ```

2. **Tools array** (ListToolsRequestSchema handler):
   ```typescript
   {
     name: "new_tool_name",
     description: "Clear description of what tool does",
     inputSchema: {
       type: "object",
       properties: { /* parameters */ },
       required: ["param1", "param2"]
     }
   }
   ```

3. **CallToolRequestSchema switch statement**:
   ```typescript
   case "new_tool_name":
     return await newToolFunction(request.params.arguments);
   ```

**Common mistake**: Forgetting one location causes silent tool failures or "tool does not exist" errors.

**Validation**: After adding tool, grep for tool name across `index.ts` - should appear exactly 3 times.

## Building TypeScript MCP Servers

Standard build workflow for TypeScript-based MCP servers:

```powershell
cd .mcp/server-name
npm install          # Restore dependencies
npm run build        # Compile TypeScript to JavaScript
```

**Output location**: `dist/` directory contains compiled `.js` files

**Common issues:**
- Missing dependencies: Run `npm install` first
- Compilation errors: Check `tsconfig.json` target and module settings
- Import path mismatches: Ensure `.js` extensions in imports (ESM requirement)

**VS Code integration**: `mcp.json` points to `dist/index.js`, not TypeScript source.

## Systematic Documentation Updates with MCP Tools

When updating multiple documentation files to reference MCP tools:

**Pattern:**
1. **Discovery phase**: Use Joyride to scan and categorize files by priority
2. **Checklist creation**: Build todo list with priority tiers (HIGH/MEDIUM/LOW)
3. **Batch updates**: Work through priority tiers systematically
4. **Standard format**: Add consistent "Required MCP Tools" or "Available MCP Tools" section

**Section template:**
```markdown
## Required MCP Tools

This prompt [requires/can utilize] the following MCP tools from the **mtm-workflow** server:
- `tool_name` - Brief description of when to use it
```

**Location**: Place after description frontmatter, before main content.

**Benefit**: Users know which tools are available for each prompt/instruction context.
