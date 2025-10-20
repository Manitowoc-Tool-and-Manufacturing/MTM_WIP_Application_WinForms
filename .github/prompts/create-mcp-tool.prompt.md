---
description: 'Guide for creating new MCP tools for the mtm-workflow server'
applyTo: 'C:\.mcp\mtm-workflow\src\tools\**'
---

# MCP Tool Creation Guide

Expert guide for creating validation and automation tools for the mtm-workflow MCP server.

## MCP Server Location

**Root**: `C:\.mcp\mtm-workflow\`  
**Tools**: `C:\.mcp\mtm-workflow\src\tools\`  
**Global Command**: `mtm-workflow-mcp` (via npm link)

## Quick Start Workflow

When user asks to create a new MCP tool:

1. **Clarify Purpose**: What does the tool validate/analyze/generate?
2. **Choose Pattern**: Validation, Analysis, Generation, or Comparison
3. **Create Tool File**: `C:\.mcp\mtm-workflow\src\tools\tool-name.ts`
4. **Register in index.ts**: Import, define, and add case handler
5. **Build**: `cd C:\.mcp\mtm-workflow; npm run build`
6. **Test**: Run with Node.js or reload VS Code

## Tool Template Structure

Every tool follows this pattern:

```typescript
import * as fs from "fs";
import * as path from "path";

// 1. Define interfaces for issues and results
interface ToolIssue {
  file: string;
  line?: number;
  severity: "critical" | "error" | "warning" | "info";
  category: string;
  message: string;
  recommendation?: string;
}

interface ToolResult {
  file: string;
  passed: boolean;
  issues: ToolIssue[];
  checks: Record<string, boolean>;
}

// 2. Main tool export
export async function myTool(args: {
  source_dir: string;
  recursive?: boolean;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  // Validate inputs
  if (!fs.existsSync(args.source_dir)) {
    throw new Error(`Directory not found: ${args.source_dir}`);
  }

  // Find files
  const files = findFiles(args.source_dir, args.recursive ?? true);
  
  // Process each file
  const results: ToolResult[] = files.map(processFile);
  
  // Generate report
  const report = generateReport(results);
  
  return {
    content: [{ type: "text", text: report }],
  };
}

// 3. Helper functions
function findFiles(dir: string, recursive: boolean): string[] {
  // Implementation
}

function processFile(filePath: string): ToolResult {
  // Implementation
}

function generateReport(results: ToolResult[]): string {
  // Markdown formatted report
}
```

## Four Tool Patterns

### Pattern 1: Validation Tool
Checks code against standards and reports violations.

**Examples**: `validate_dao_patterns`, `validate_ui_scaling`, `validate_error_handling`

**Use for**: 
- Code compliance checking
- Standards enforcement
- Pattern verification

### Pattern 2: Analysis Tool
Analyzes code structure and generates insights.

**Examples**: `analyze_dependencies`, `analyze_performance`, `analyze_stored_procedures`

**Use for**:
- Metrics calculation
- Dependency mapping
- Performance profiling

### Pattern 3: Generation Tool
Creates new code from templates or patterns.

**Examples**: `generate_dao_wrapper`, `generate_unit_tests`

**Use for**:
- Boilerplate generation
- Test scaffolding
- Code templates

### Pattern 4: Comparison Tool
Compares two states and reports differences.

**Examples**: `compare_databases`

**Use for**:
- Schema drift detection
- Version comparison
- Change tracking

## Registration Checklist

When adding a tool to `src/index.ts`:

### Step 1: Add Import (top of file)
```typescript
import { myTool } from "./tools/my-tool.js";
```

### Step 2: Add Tool Definition (in `tools` array)
```typescript
{
  name: "my_tool",  // snake_case, appears as mcp_mtm-workflow_my_tool
  description: "Clear description of what tool does and output format",
  inputSchema: {
    type: "object",
    properties: {
      source_dir: {
        type: "string",
        description: "Absolute path to source directory",
      },
      recursive: {
        type: "boolean",
        description: "Whether to search subdirectories",
        default: true,
      },
    },
    required: ["source_dir"],
  },
},
```

### Step 3: Add Case Handler (in switch statement)
```typescript
case "my_tool":
  return await myTool(
    args as { source_dir: string; recursive?: boolean }
  );
```

## Output Format Standards

All tools should return markdown with this structure:

```markdown
## Tool Name Results

| File | Status | Issues |
|------|--------|--------|
| file1.cs | ✅ PASS | 0 |
| file2.cs | ❌ FAIL | 3 |

**Summary**:
- Total Files: 10
- Passed: 8 (80%)
- Failed: 2

### 🔴 Critical Issues (must fix)

**file2.cs:45** - Issue description
   → Recommendation: How to fix

### ⚠️ Warnings (should review)

**file3.cs:120** - Warning description

## Recommendations

1. Fix critical issues first
2. Address errors next
3. Review warnings based on priority
```

## Best Practices

### Error Handling
- Always validate input paths exist
- Provide clear error messages
- Catch and handle exceptions gracefully

### Performance
- Process files in batches for large directories
- Skip binary files and build artifacts
- Use streaming for large files
- Cache expensive operations

### User Experience
- Show progress for long operations (console.error)
- Provide actionable recommendations
- Use consistent severity levels
- Include line numbers when possible

### Maintainability
- Add JSDoc comments to exports
- Keep functions focused and single-purpose
- Use descriptive variable names
- Follow existing tool patterns

## Testing Tools

### Manual Test
```powershell
cd C:\.mcp\mtm-workflow
npm run build

node -e "
import('./dist/tools/my-tool.js').then(m => {
  m.myTool({ source_dir: 'C:\\TestPath', recursive: true })
    .then(r => console.log(r.content[0].text))
    .catch(e => console.error(e));
});
"
```

### VS Code Test
1. Build: `npm run build`
2. Reload: Ctrl+Shift+P → "Developer: Reload Window"
3. Use in chat: `Use mcp_mtm-workflow_my_tool with source_dir "C:\Path"`

## Common Patterns

### Find Files Pattern
```typescript
function findFiles(dir: string, recursive: boolean): string[] {
  const files: string[] = [];
  
  function traverse(currentDir: string) {
    const entries = fs.readdirSync(currentDir, { withFileTypes: true });
    
    for (const entry of entries) {
      const fullPath = path.join(currentDir, entry.name);
      
      if (entry.isDirectory()) {
        if (entry.name === "bin" || entry.name === "obj") continue;
        if (recursive) traverse(fullPath);
      } else if (matchesPattern(entry.name)) {
        files.push(fullPath);
      }
    }
  }
  
  traverse(dir);
  return files;
}
```

### Process File Pattern
```typescript
function processFile(filePath: string): ToolResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: ToolIssue[] = [];
  
  // Check patterns
  if (!content.includes("RequiredPattern")) {
    issues.push({
      file: filePath,
      line: findLineNumber(content, "RequiredPattern"),
      severity: "error",
      category: "Missing Pattern",
      message: "File missing required pattern",
      recommendation: "Add RequiredPattern at top of file",
    });
  }
  
  return {
    file: filePath,
    passed: issues.filter(i => i.severity === "critical" || i.severity === "error").length === 0,
    issues,
    checks: { has_required_pattern: content.includes("RequiredPattern") },
  };
}
```

### Generate Summary Table Pattern
```typescript
function generateSummaryTable(results: ToolResult[]): string {
  const rows: string[] = [
    "| File | Status | Critical | Errors | Warnings |",
    "|------|--------|----------|--------|----------|",
  ];
  
  for (const result of results) {
    const name = path.basename(result.file);
    const status = result.passed ? "✅ PASS" : "❌ FAIL";
    const critical = result.issues.filter(i => i.severity === "critical").length;
    const errors = result.issues.filter(i => i.severity === "error").length;
    const warnings = result.issues.filter(i => i.severity === "warning").length;
    
    rows.push(`| ${name} | ${status} | ${critical} | ${errors} | ${warnings} |`);
  }
  
  return rows.join("\n");
}
```

## Tool Naming Conventions

- Use **snake_case**: `validate_ui_scaling`
- Start with verb: `validate_`, `analyze_`, `generate_`, `check_`
- Include domain when helpful: `dao_`, `ui_`, `security_`
- Keep concise: `check_xml_docs` not `check_xml_documentation_coverage`

## Deployment Workflow

After creating/updating tools:

```powershell
# 1. Build
cd C:\.mcp\mtm-workflow
npm run build

# 2. Changes are live globally (npm link)
# Just reload VS Code

# 3. Test in any workspace
# Ctrl+Shift+P → "Developer: Reload Window"
```

## Tool Ideas for MTM

### High Priority
- `validate_form_layout` - Check WinForms layout patterns
- `analyze_dao_coverage` - Map DAO ↔ stored procedure coverage
- `validate_async_patterns` - Check async/await usage
- `check_connection_disposal` - Ensure proper resource cleanup

### Medium Priority
- `validate_transaction_boundaries` - Analyze transaction patterns
- `check_parameter_conventions` - Validate parameter naming
- `analyze_form_dependencies` - Map form/control relationships
- `validate_resource_disposal` - Check IDisposable usage

### Nice to Have
- `generate_dao_tests` - Auto-generate DAO test scaffolding
- `analyze_complexity` - Calculate cyclomatic complexity
- `check_logging_coverage` - Ensure all errors logged
- `validate_documentation` - Check XML doc completeness

## Reference Files

**Full Guide**: `C:\.mcp\mtm-workflow\CREATING-TOOLS.md`  
**Example Tool**: `C:\.mcp\mtm-workflow\src\tools\validate-ui-scaling.ts`  
**Server Index**: `C:\.mcp\mtm-workflow\src\index.ts`  
**Quick Setup**: `C:\.mcp\mtm-workflow\QUICK-SETUP.md`

## Quick Commands Reference

```powershell
# Navigate to MCP server
cd C:\.mcp\mtm-workflow

# Build
npm run build

# Test tool
node dist/index.js

# List available tools
echo '{"jsonrpc":"2.0","id":1,"method":"tools/list"}' | mtm-workflow-mcp

# Verify global link
where.exe mtm-workflow-mcp
```

## When to Create a New Tool

✅ **Create tool when**:
- Validation needs to be repeatable
- Pattern checking is complex
- Multiple files need analysis
- Automation saves significant time
- Standards need enforcement

❌ **Don't create tool when**:
- One-time check is sufficient
- Manual review is faster
- Pattern is too variable
- Maintenance cost exceeds value

## Agent Communication

When creating tools, agents should:
1. **Clarify requirements** before implementation
2. **Choose appropriate pattern** (validation/analysis/generation/comparison)
3. **Follow existing conventions** from similar tools
4. **Test thoroughly** with real data
5. **Document clearly** in tool description
6. **Provide examples** in recommendations section

Remember: MCP tools should be **reliable**, **fast**, and **actionable**. Users depend on them for quality gates and workflow automation.
