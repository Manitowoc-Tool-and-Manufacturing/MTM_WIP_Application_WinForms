import * as fs from "fs";
import * as path from "path";

interface ChecklistResult {
  checklist: string;
  total: number;
  completed: number;
  incomplete: number;
  status: "PASS" | "FAIL";
}

interface ChecklistSummary {
  overall_status: "PASS" | "FAIL";
  total_checklists: number;
  passed_checklists: number;
  failed_checklists: number;
  results: ChecklistResult[];
  summary_table: string;
}

export async function checkChecklists(args: {
  checklist_dir: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { checklist_dir } = args;

  // Validate directory exists
  if (!fs.existsSync(checklist_dir)) {
    throw new Error(`Directory not found: ${checklist_dir}`);
  }

  // Read all markdown files in directory
  const files = fs
    .readdirSync(checklist_dir)
    .filter((f) => f.endsWith(".md"))
    .map((f) => path.join(checklist_dir, f));

  if (files.length === 0) {
    throw new Error(`No markdown files found in ${checklist_dir}`);
  }

  const results: ChecklistResult[] = [];

  // Analyze each checklist file
  for (const file of files) {
    const content = fs.readFileSync(file, "utf-8");
    const lines = content.split("\n");

    // Count checklist items
    const totalItems = lines.filter((line) =>
      /^\s*- \[( |x|X)\]/.test(line)
    ).length;
    const completedItems = lines.filter((line) =>
      /^\s*- \[(x|X)\]/.test(line)
    ).length;
    const incompleteItems = lines.filter((line) =>
      /^\s*- \[ \]/.test(line)
    ).length;

    results.push({
      checklist: path.basename(file),
      total: totalItems,
      completed: completedItems,
      incomplete: incompleteItems,
      status: incompleteItems === 0 ? "PASS" : "FAIL",
    });
  }

  // Calculate overall status
  const failedCount = results.filter((r) => r.status === "FAIL").length;
  const overallStatus = failedCount === 0 ? "PASS" : "FAIL";

  // Generate summary table
  const table = generateTable(results);

  const summary: ChecklistSummary = {
    overall_status: overallStatus,
    total_checklists: results.length,
    passed_checklists: results.filter((r) => r.status === "PASS").length,
    failed_checklists: failedCount,
    results,
    summary_table: table,
  };

  const message = `
## Checklist Analysis Results

${table}

**Overall Status**: ${overallStatus === "PASS" ? "✅ PASS" : "❌ FAIL"}

**Summary**:
- Total Checklists: ${summary.total_checklists}
- Passed: ${summary.passed_checklists}
- Failed: ${summary.failed_checklists}

${
  overallStatus === "FAIL"
    ? "\n⚠️ Some checklists have incomplete items. Review the table above for details."
    : "\n✅ All checklists are complete!"
}
`;

  return {
    content: [
      {
        type: "text",
        text: message,
      },
    ],
  };
}

function generateTable(results: ChecklistResult[]): string {
  const header = "| Checklist | Total | Completed | Incomplete | Status |";
  const separator = "|-----------|-------|-----------|------------|--------|";

  const rows = results.map((r) => {
    const statusIcon = r.status === "PASS" ? "✅" : "❌";
    return `| ${r.checklist} | ${r.total} | ${r.completed} | ${r.incomplete} | ${statusIcon} ${r.status} |`;
  });

  return [header, separator, ...rows].join("\n");
}
