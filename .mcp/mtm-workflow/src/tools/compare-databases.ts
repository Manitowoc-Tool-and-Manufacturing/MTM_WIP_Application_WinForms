import * as fs from "fs";
import * as path from "path";

interface SchemaDifference {
  type: "table" | "procedure" | "column" | "constraint";
  name: string;
  status: "added" | "removed" | "modified";
  details: string;
}

interface DatabaseComparison {
  current_path: string;
  updated_path: string;
  tables_added: string[];
  tables_removed: string[];
  tables_modified: string[];
  procedures_added: string[];
  procedures_removed: string[];
  procedures_modified: string[];
  differences: SchemaDifference[];
  summary: string;
}

export async function compareDatabases(args: {
  current_dir: string;
  updated_dir: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { current_dir, updated_dir } = args;

  if (!fs.existsSync(current_dir)) {
    throw new Error(`Current database directory not found: ${current_dir}`);
  }

  if (!fs.existsSync(updated_dir)) {
    throw new Error(`Updated database directory not found: ${updated_dir}`);
  }

  // Get all SQL files from both directories
  const currentFiles = getAllSqlFiles(current_dir);
  const updatedFiles = getAllSqlFiles(updated_dir);

  const currentSet = new Set(currentFiles.map((f) => path.basename(f)));
  const updatedSet = new Set(updatedFiles.map((f) => path.basename(f)));

  // Identify added, removed, and common files
  const added = [...updatedSet].filter((f) => !currentSet.has(f));
  const removed = [...currentSet].filter((f) => !updatedSet.has(f));
  const common = [...currentSet].filter((f) => updatedSet.has(f));

  const differences: SchemaDifference[] = [];

  // Categorize by type
  const tablesAdded = added.filter((f) => !f.startsWith("sp_") && !f.includes("_"));
  const tablesRemoved = removed.filter((f) => !f.startsWith("sp_") && !f.includes("_"));
  const proceduresAdded = added.filter((f) => f.startsWith("sp_") || f.includes("_"));
  const proceduresRemoved = removed.filter((f) => f.startsWith("sp_") || f.includes("_"));

  // Add differences for new files
  for (const file of tablesAdded) {
    differences.push({
      type: "table",
      name: file.replace(".sql", ""),
      status: "added",
      details: "New table definition",
    });
  }

  for (const file of proceduresAdded) {
    differences.push({
      type: "procedure",
      name: file.replace(".sql", ""),
      status: "added",
      details: "New stored procedure",
    });
  }

  // Add differences for removed files
  for (const file of tablesRemoved) {
    differences.push({
      type: "table",
      name: file.replace(".sql", ""),
      status: "removed",
      details: "Table removed or renamed",
    });
  }

  for (const file of proceduresRemoved) {
    differences.push({
      type: "procedure",
      name: file.replace(".sql", ""),
      status: "removed",
      details: "Stored procedure removed or renamed",
    });
  }

  // Check for modifications in common files
  const modified: string[] = [];
  for (const file of common) {
    const currentPath = currentFiles.find((f) => path.basename(f) === file)!;
    const updatedPath = updatedFiles.find((f) => path.basename(f) === file)!;

    const currentContent = fs.readFileSync(currentPath, "utf-8");
    const updatedContent = fs.readFileSync(updatedPath, "utf-8");

    if (currentContent !== updatedContent) {
      modified.push(file);
      
      const isTable = !file.startsWith("sp_") && !file.includes("_");
      differences.push({
        type: isTable ? "table" : "procedure",
        name: file.replace(".sql", ""),
        status: "modified",
        details: `Content changed (${updatedContent.length - currentContent.length} chars difference)`,
      });
    }
  }

  const comparison: DatabaseComparison = {
    current_path: current_dir,
    updated_path: updated_dir,
    tables_added: tablesAdded,
    tables_removed: tablesRemoved,
    tables_modified: modified.filter((f) => !f.startsWith("sp_")),
    procedures_added: proceduresAdded,
    procedures_removed: proceduresRemoved,
    procedures_modified: modified.filter((f) => f.startsWith("sp_") || f.includes("_")),
    differences,
    summary: generateSummary(differences),
  };

  const message = generateComparisonReport(comparison);

  return {
    content: [{ type: "text", text: message }],
  };
}

function getAllSqlFiles(dir: string): string[] {
  const files: string[] = [];
  
  if (!fs.existsSync(dir)) {
    return files;
  }

  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory()) {
      files.push(...getAllSqlFiles(fullPath));
    } else if (stat.isFile() && item.endsWith(".sql")) {
      files.push(fullPath);
    }
  }

  return files;
}

function generateSummary(differences: SchemaDifference[]): string {
  const added = differences.filter((d) => d.status === "added").length;
  const removed = differences.filter((d) => d.status === "removed").length;
  const modified = differences.filter((d) => d.status === "modified").length;

  if (added === 0 && removed === 0 && modified === 0) {
    return "✅ No schema drift detected. Databases are in sync.";
  }

  return `⚠️ Schema drift detected: ${added} added, ${removed} removed, ${modified} modified`;
}

function generateComparisonReport(comparison: DatabaseComparison): string {
  const { differences, summary } = comparison;

  let report = `
## Database Comparison Results

**Current Database:** \`${path.basename(comparison.current_path)}\`
**Updated Database:** \`${path.basename(comparison.updated_path)}\`

---

### Summary

${summary}

### Statistics

| Category | Added | Removed | Modified |
|----------|-------|---------|----------|
| Tables | ${comparison.tables_added.length} | ${comparison.tables_removed.length} | ${comparison.tables_modified.length} |
| Stored Procedures | ${comparison.procedures_added.length} | ${comparison.procedures_removed.length} | ${comparison.procedures_modified.length} |

---

`;

  if (differences.length === 0) {
    report += "✅ **Databases are identical.** No differences found.\n";
    return report;
  }

  report += "### Detailed Differences\n\n";

  // Group by type and status
  const tables = differences.filter((d) => d.type === "table");
  const procedures = differences.filter((d) => d.type === "procedure");

  if (tables.length > 0) {
    report += "#### Tables\n\n";
    for (const diff of tables) {
      const icon =
        diff.status === "added" ? "➕" : diff.status === "removed" ? "➖" : "📝";
      report += `${icon} **${diff.name}** - ${diff.status}: ${diff.details}\n`;
    }
    report += "\n";
  }

  if (procedures.length > 0) {
    report += "#### Stored Procedures\n\n";
    for (const diff of procedures) {
      const icon =
        diff.status === "added" ? "➕" : diff.status === "removed" ? "➖" : "📝";
      report += `${icon} **${diff.name}** - ${diff.status}: ${diff.details}\n`;
    }
    report += "\n";
  }

  report += "---\n\n";
  report += "**Recommendation:** Review all changes before deploying to production.\n";

  return report;
}
