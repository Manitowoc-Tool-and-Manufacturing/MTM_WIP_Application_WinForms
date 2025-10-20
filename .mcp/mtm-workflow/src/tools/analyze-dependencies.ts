import * as fs from "fs";
import * as path from "path";

interface DependencyNode {
  procedure: string;
  calls: string[];
  called_by: string[];
  depth: number;
}

interface DependencyMap {
  [procedure: string]: DependencyNode;
}

export async function analyzeDependencies(args: {
  procedures_dir: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { procedures_dir } = args;

  if (!fs.existsSync(procedures_dir)) {
    throw new Error(`Directory not found: ${procedures_dir}`);
  }

  const files = findSqlFiles(procedures_dir);

  if (files.length === 0) {
    throw new Error(`No SQL files found in ${procedures_dir}`);
  }

  const dependencyMap = buildDependencyMap(files);
  const analysis = analyzeDependencyMap(dependencyMap);

  const message = `
## Stored Procedure Dependency Analysis

### Overview
- **Total Procedures:** ${analysis.total_procedures}
- **Root Procedures:** ${analysis.root_procedures.length} (not called by others)
- **Leaf Procedures:** ${analysis.leaf_procedures.length} (don't call others)
- **Max Depth:** ${analysis.max_depth}
- **Circular Dependencies:** ${analysis.circular_dependencies.length > 0 ? "❌ Found" : "✅ None"}

---

### Call Hierarchy Statistics

| Metric | Count |
|--------|-------|
| Total Procedures | ${analysis.total_procedures} |
| Root Procedures | ${analysis.root_procedures.length} |
| Leaf Procedures | ${analysis.leaf_procedures.length} |
| Isolated Procedures | ${analysis.isolated_procedures.length} |
| Most Called Procedure | ${analysis.most_called.procedure} (${analysis.most_called.count} times) |
| Most Calling Procedure | ${analysis.most_calling.procedure} (calls ${analysis.most_calling.count} others) |

---

${generateDependencyTree(dependencyMap, analysis)}

${analysis.circular_dependencies.length > 0 ? generateCircularDependencyWarning(analysis.circular_dependencies) : ""}

---

### Root Procedures (Entry Points)

These procedures are not called by any other procedure and serve as API entry points:

${analysis.root_procedures.map((p) => `- \`${p}\` → calls ${dependencyMap[p].calls.length} procedure(s)`).join("\n")}

---

### Leaf Procedures (Terminal Operations)

These procedures don't call any other procedures:

${analysis.leaf_procedures.map((p) => `- \`${p}\` → called by ${dependencyMap[p].called_by.length} procedure(s)`).join("\n")}

${analysis.isolated_procedures.length > 0 ? `
---

### Isolated Procedures ⚠️

These procedures neither call nor are called by other procedures:

${analysis.isolated_procedures.map((p) => `- \`${p}\``).join("\n")}
` : ""}
`;

  return {
    content: [{ type: "text", text: message }],
  };
}

function findSqlFiles(dir: string): string[] {
  const files: string[] = [];
  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory()) {
      files.push(...findSqlFiles(fullPath));
    } else if (stat.isFile() && item.endsWith(".sql")) {
      files.push(fullPath);
    }
  }

  return files;
}

function buildDependencyMap(files: string[]): DependencyMap {
  const map: DependencyMap = {};

  // First pass: identify all procedures
  for (const file of files) {
    const content = fs.readFileSync(file, "utf-8");
    // Match: CREATE [DEFINER=...] PROCEDURE `procedure_name` or procedure_name
    const procMatch = content.match(/CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`?([a-zA-Z0-9_]+)`?/i);

    if (procMatch) {
      const procName = procMatch[1];
      map[procName] = {
        procedure: procName,
        calls: [],
        called_by: [],
        depth: 0,
      };
    }
  }

  // Second pass: identify CALL statements
  for (const file of files) {
    const content = fs.readFileSync(file, "utf-8");
    // Match: CREATE [DEFINER=...] PROCEDURE `procedure_name` or procedure_name
    const procMatch = content.match(/CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`?([a-zA-Z0-9_]+)`?/i);

    if (procMatch) {
      const callerProc = procMatch[1];

      // Find all CALL statements in this procedure
      // Match: CALL `procedure_name` or CALL procedure_name
      const callMatches = content.matchAll(/CALL\s+`?([a-zA-Z0-9_]+)`?\s*\(/gi);

      for (const match of callMatches) {
        const calledProc = match[1];

        if (map[callerProc] && map[calledProc]) {
          if (!map[callerProc].calls.includes(calledProc)) {
            map[callerProc].calls.push(calledProc);
          }
          if (!map[calledProc].called_by.includes(callerProc)) {
            map[calledProc].called_by.push(callerProc);
          }
        }
      }
    }
  }

  // Calculate depths
  calculateDepths(map);

  return map;
}

function calculateDepths(map: DependencyMap): void {
  // Find root procedures (not called by anyone)
  const roots = Object.values(map).filter((node) => node.called_by.length === 0);

  // BFS to calculate depths
  const queue: Array<{ proc: string; depth: number }> = roots.map((r) => ({
    proc: r.procedure,
    depth: 0,
  }));
  const visited = new Set<string>();

  while (queue.length > 0) {
    const { proc, depth } = queue.shift()!;

    if (visited.has(proc)) continue;
    visited.add(proc);

    map[proc].depth = Math.max(map[proc].depth, depth);

    for (const called of map[proc].calls) {
      queue.push({ proc: called, depth: depth + 1 });
    }
  }
}

function analyzeDependencyMap(map: DependencyMap) {
  const procedures = Object.values(map);

  const rootProcedures = procedures
    .filter((p) => p.called_by.length === 0)
    .map((p) => p.procedure);

  const leafProcedures = procedures
    .filter((p) => p.calls.length === 0)
    .map((p) => p.procedure);

  const isolatedProcedures = procedures
    .filter((p) => p.calls.length === 0 && p.called_by.length === 0)
    .map((p) => p.procedure);

  const mostCalled = procedures.reduce(
    (max, p) =>
      p.called_by.length > max.count
        ? { procedure: p.procedure, count: p.called_by.length }
        : max,
    { procedure: "", count: 0 }
  );

  const mostCalling = procedures.reduce(
    (max, p) =>
      p.calls.length > max.count
        ? { procedure: p.procedure, count: p.calls.length }
        : max,
    { procedure: "", count: 0 }
  );

  const maxDepth = Math.max(...procedures.map((p) => p.depth));

  // Detect circular dependencies
  const circularDependencies = detectCircularDependencies(map);

  return {
    total_procedures: procedures.length,
    root_procedures: rootProcedures,
    leaf_procedures: leafProcedures,
    isolated_procedures: isolatedProcedures,
    most_called: mostCalled,
    most_calling: mostCalling,
    max_depth: maxDepth,
    circular_dependencies: circularDependencies,
  };
}

function detectCircularDependencies(map: DependencyMap): string[][] {
  const cycles: string[][] = [];
  const visited = new Set<string>();
  const recStack = new Set<string>();

  function dfs(proc: string, path: string[]): void {
    visited.add(proc);
    recStack.add(proc);
    path.push(proc);

    for (const called of map[proc].calls) {
      if (!visited.has(called)) {
        dfs(called, [...path]);
      } else if (recStack.has(called)) {
        // Found a cycle
        const cycleStart = path.indexOf(called);
        const cycle = path.slice(cycleStart);
        cycle.push(called);
        cycles.push(cycle);
      }
    }

    recStack.delete(proc);
  }

  for (const proc of Object.keys(map)) {
    if (!visited.has(proc)) {
      dfs(proc, []);
    }
  }

  return cycles;
}

function generateDependencyTree(
  map: DependencyMap,
  analysis: any
): string {
  let tree = "### Dependency Tree (Top-Level Procedures)\n\n```\n";

  const roots = analysis.root_procedures;

  for (const root of roots.slice(0, 10)) {
    // Limit to top 10
    tree += buildTree(map, root, 0);
  }

  if (roots.length > 10) {
    tree += `\n... and ${roots.length - 10} more root procedures\n`;
  }

  tree += "```\n";

  return tree;
}

function buildTree(
  map: DependencyMap,
  proc: string,
  depth: number,
  visited: Set<string> = new Set()
): string {
  if (visited.has(proc) || depth > 5) {
    return `${"  ".repeat(depth)}├─ ${proc} (recursive/deep)\n`;
  }

  visited.add(proc);

  let result = `${"  ".repeat(depth)}├─ ${proc}`;

  if (map[proc].calls.length > 0) {
    result += ` → calls ${map[proc].calls.length}\n`;
    for (const called of map[proc].calls) {
      result += buildTree(map, called, depth + 1, new Set(visited));
    }
  } else {
    result += "\n";
  }

  return result;
}

function generateCircularDependencyWarning(cycles: string[][]): string {
  let warning = "### ⚠️ Circular Dependencies Detected\n\n";
  warning +=
    "Circular dependencies can cause infinite loops and should be refactored:\n\n";

  for (let i = 0; i < cycles.length; i++) {
    warning += `**Cycle ${i + 1}:** ${cycles[i].join(" → ")}\n\n`;
  }

  return warning;
}
