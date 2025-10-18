import * as fs from "fs";
import * as path from "path";

interface DaoValidationIssue {
  file: string;
  line?: number;
  severity: "error" | "warning" | "info";
  category: string;
  message: string;
}

interface DaoValidationResult {
  file: string;
  passed: boolean;
  issues: DaoValidationIssue[];
  checks: {
    has_regions: boolean;
    uses_helper_database: boolean;
    has_async_await: boolean;
    uses_error_handler: boolean;
    has_xml_docs: boolean;
  };
}

interface ValidationSummary {
  total_files: number;
  passed_files: number;
  failed_files: number;
  total_issues: number;
  results: DaoValidationResult[];
  summary_table: string;
}

export async function validateDaoPatterns(args: {
  dao_dir: string;
  recursive?: boolean;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { dao_dir, recursive = true } = args;

  // Validate directory exists
  if (!fs.existsSync(dao_dir)) {
    throw new Error(`Directory not found: ${dao_dir}`);
  }

  // Find all C# files
  const files = findCSharpFiles(dao_dir, recursive);

  if (files.length === 0) {
    throw new Error(`No C# files found in ${dao_dir}`);
  }

  const results: DaoValidationResult[] = [];

  // Validate each file
  for (const file of files) {
    const result = validateDaoFile(file);
    results.push(result);
  }

  // Generate summary
  const passedCount = results.filter((r) => r.passed).length;
  const totalIssues = results.reduce((sum, r) => sum + r.issues.length, 0);

  const table = generateValidationTable(results);

  const summary: ValidationSummary = {
    total_files: results.length,
    passed_files: passedCount,
    failed_files: results.length - passedCount,
    total_issues: totalIssues,
    results,
    summary_table: table,
  };

  const message = `
## DAO Validation Results

${table}

**Summary**:
- Total Files: ${summary.total_files}
- Passed: ${summary.passed_files}
- Failed: ${summary.failed_files}
- Total Issues: ${summary.total_issues}

${generateIssueDetails(results)}
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

function findCSharpFiles(dir: string, recursive: boolean): string[] {
  const files: string[] = [];

  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory() && recursive) {
      files.push(...findCSharpFiles(fullPath, recursive));
    } else if (stat.isFile() && item.endsWith(".cs")) {
      files.push(fullPath);
    }
  }

  return files;
}

function validateDaoFile(filePath: string): DaoValidationResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: DaoValidationIssue[] = [];

  // Check for required patterns
  const hasRegions = /#region/.test(content);
  const usesHelperDatabase =
    /Helper_Database_StoredProcedure/.test(content) ||
    /Helper_Database_Variables/.test(content);
  const hasAsyncAwait = /async\s+Task/.test(content);
  const usesErrorHandler = /Service_ErrorHandler/.test(content);
  const hasXmlDocs = /\/\/\/\s*<summary>/.test(content);

  // Region organization check
  if (!hasRegions) {
    issues.push({
      file: path.basename(filePath),
      severity: "warning",
      category: "Organization",
      message: "Missing #region organization. DAOs should use standard region structure.",
    });
  }

  // Helper usage check
  if (!usesHelperDatabase) {
    issues.push({
      file: path.basename(filePath),
      severity: "error",
      category: "Database",
      message:
        "Must use Helper_Database_StoredProcedure for all database operations.",
    });
  }

  // Async/await check
  if (!hasAsyncAwait) {
    issues.push({
      file: path.basename(filePath),
      severity: "warning",
      category: "Performance",
      message: "No async/await patterns found. All I/O operations should be asynchronous.",
    });
  }

  // Error handler check
  if (!usesErrorHandler) {
    issues.push({
      file: path.basename(filePath),
      severity: "warning",
      category: "Error Handling",
      message:
        "Should use Service_ErrorHandler instead of MessageBox.Show for error handling.",
    });
  }

  // XML documentation check
  if (!hasXmlDocs) {
    issues.push({
      file: path.basename(filePath),
      severity: "info",
      category: "Documentation",
      message: "Missing XML documentation. Public APIs should have <summary> tags.",
    });
  }

  // Check for anti-patterns
  if (/MessageBox\.Show/.test(content)) {
    const lineNumbers = lines
      .map((line, idx) => ({ line, idx }))
      .filter(({ line }) => /MessageBox\.Show/.test(line))
      .map(({ idx }) => idx + 1);

    issues.push({
      file: path.basename(filePath),
      line: lineNumbers[0],
      severity: "error",
      category: "Error Handling",
      message: `Found MessageBox.Show on line(s) ${lineNumbers.join(", ")}. Use Service_ErrorHandler instead.`,
    });
  }

  if (/\.Result|\.Wait\(\)/.test(content)) {
    issues.push({
      file: path.basename(filePath),
      severity: "error",
      category: "Performance",
      message: "Found blocking async call (.Result or .Wait()). Use await instead.",
    });
  }

  const passed = issues.filter((i) => i.severity === "error").length === 0;

  return {
    file: path.basename(filePath),
    passed,
    issues,
    checks: {
      has_regions: hasRegions,
      uses_helper_database: usesHelperDatabase,
      has_async_await: hasAsyncAwait,
      uses_error_handler: usesErrorHandler,
      has_xml_docs: hasXmlDocs,
    },
  };
}

function generateValidationTable(results: DaoValidationResult[]): string {
  const header = "| File | Status | Errors | Warnings | Info |";
  const separator = "|------|--------|--------|----------|------|";

  const rows = results.map((r) => {
    const statusIcon = r.passed ? "✅" : "❌";
    const errors = r.issues.filter((i) => i.severity === "error").length;
    const warnings = r.issues.filter((i) => i.severity === "warning").length;
    const info = r.issues.filter((i) => i.severity === "info").length;

    return `| ${r.file} | ${statusIcon} | ${errors} | ${warnings} | ${info} |`;
  });

  return [header, separator, ...rows].join("\n");
}

function generateIssueDetails(results: DaoValidationResult[]): string {
  const filesWithIssues = results.filter((r) => r.issues.length > 0);

  if (filesWithIssues.length === 0) {
    return "✅ **No issues found!** All DAOs follow MTM coding standards.";
  }

  let details = "### Issue Details\n\n";

  for (const result of filesWithIssues) {
    details += `**${result.file}**:\n`;

    for (const issue of result.issues) {
      const icon =
        issue.severity === "error"
          ? "❌"
          : issue.severity === "warning"
            ? "⚠️"
            : "ℹ️";
      const lineInfo = issue.line ? ` (line ${issue.line})` : "";
      details += `  ${icon} [${issue.category}]${lineInfo}: ${issue.message}\n`;
    }

    details += "\n";
  }

  return details;
}
