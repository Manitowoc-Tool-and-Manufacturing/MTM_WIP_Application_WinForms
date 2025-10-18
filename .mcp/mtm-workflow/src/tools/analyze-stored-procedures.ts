import * as fs from "fs";
import * as path from "path";

interface StoredProcedureIssue {
  procedure: string;
  line?: number;
  severity: "error" | "warning" | "info";
  category: string;
  message: string;
}

interface StoredProcedureResult {
  procedure: string;
  file: string;
  passed: boolean;
  issues: StoredProcedureIssue[];
  checks: {
    has_status_param: boolean;
    has_error_param: boolean;
    has_transaction: boolean;
    has_error_handling: boolean;
    uses_p_prefix: boolean;
  };
}

export async function analyzeStoredProcedures(args: {
  procedures_dir: string;
  recursive?: boolean;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { procedures_dir, recursive = true } = args;

  if (!fs.existsSync(procedures_dir)) {
    throw new Error(`Directory not found: ${procedures_dir}`);
  }

  const files = findSqlFiles(procedures_dir, recursive);

  if (files.length === 0) {
    throw new Error(`No SQL files found in ${procedures_dir}`);
  }

  const results: StoredProcedureResult[] = [];

  for (const file of files) {
    const result = analyzeStoredProcedure(file);
    results.push(result);
  }

  const passedCount = results.filter((r) => r.passed).length;
  const totalIssues = results.reduce((sum, r) => sum + r.issues.length, 0);

  const table = generateProcedureTable(results);

  const message = `
## Stored Procedure Analysis Results

${table}

**Summary**:
- Total Procedures: ${results.length}
- Passed: ${passedCount}
- Failed: ${results.length - passedCount}
- Total Issues: ${totalIssues}

${generateProcedureIssueDetails(results)}
`;

  return {
    content: [{ type: "text", text: message }],
  };
}

function findSqlFiles(dir: string, recursive: boolean): string[] {
  const files: string[] = [];
  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory() && recursive) {
      files.push(...findSqlFiles(fullPath, recursive));
    } else if (stat.isFile() && item.endsWith(".sql")) {
      files.push(fullPath);
    }
  }

  return files;
}

function analyzeStoredProcedure(filePath: string): StoredProcedureResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: StoredProcedureIssue[] = [];
  const fileName = path.basename(filePath);

  // Check for bypass flags at the beginning of the file
  const bypassFlags = new Set<string>();
  const bypassRegex = /--\s*BYPASS_MCP_CHECK:\s*(\w+)/gi;
  let match;
  while ((match = bypassRegex.exec(content)) !== null) {
    bypassFlags.add(match[1].toUpperCase());
  }

  // Extract procedure name - handle DEFINER clause
  // Match: CREATE [DEFINER=...] PROCEDURE `procedure_name` or procedure_name
  const procMatch = content.match(/CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`?([a-zA-Z0-9_]+)`?/i);
  const procName = procMatch ? procMatch[1] : fileName.replace(".sql", "");

  // Check for required output parameters
  const hasStatusParam = /OUT\s+p_Status\s+INT/i.test(content);
  const hasErrorParam = /OUT\s+p_ErrorMsg\s+(VARCHAR|TEXT)/i.test(content);

  if (!hasStatusParam) {
    issues.push({
      procedure: procName,
      severity: "error",
      category: "Output Parameters",
      message: "Missing required OUT p_Status INT parameter",
    });
  }

  if (!hasErrorParam) {
    issues.push({
      procedure: procName,
      severity: "error",
      category: "Output Parameters",
      message: "Missing required OUT p_ErrorMsg VARCHAR parameter",
    });
  }

  // Check for transaction handling
  // Only check for START TRANSACTION, not BEGIN (which is for procedure blocks)
  const hasTransaction = /START TRANSACTION/i.test(content);
  const hasCommit = /COMMIT/i.test(content);
  const hasRollback = /ROLLBACK/i.test(content);

  if (hasTransaction && !hasCommit && !bypassFlags.has("TRANSACTION_MANAGEMENT")) {
    issues.push({
      procedure: procName,
      severity: "error",
      category: "Transaction Management",
      message: "Transaction started but no COMMIT found",
    });
  }

  if (hasTransaction && !hasRollback && !bypassFlags.has("TRANSACTION_MANAGEMENT")) {
    issues.push({
      procedure: procName,
      severity: "warning",
      category: "Transaction Management",
      message: "Transaction started but no ROLLBACK in error handler",
    });
  }

  // Check for error handling
  const hasErrorHandling =
    /DECLARE\s+.*HANDLER\s+FOR/i.test(content) ||
    /BEGIN\s+DECLARE\s+EXIT\s+HANDLER/i.test(content);

  if (!hasErrorHandling) {
    issues.push({
      procedure: procName,
      severity: "warning",
      category: "Error Handling",
      message: "No error handler declared",
    });
  }

  // Check parameter naming convention
  const params = content.match(/IN\s+(\w+)/gi) || [];
  const usesPPrefix = params.every((p) => /IN\s+p_/i.test(p));

  if (params.length > 0 && !usesPPrefix) {
    issues.push({
      procedure: procName,
      severity: "info",
      category: "Naming Convention",
      message: "Input parameters should use p_ prefix convention",
    });
  }

  // Check for SQL injection vulnerabilities
  if (/CONCAT\s*\(/i.test(content) && /EXECUTE/i.test(content) && !bypassFlags.has("SQL_INJECTION")) {
    issues.push({
      procedure: procName,
      severity: "error",
      category: "Security",
      message:
        "Potential SQL injection: CONCAT used with EXECUTE. Use prepared statements.",
    });
  }

  const passed = issues.filter((i) => i.severity === "error").length === 0;

  return {
    procedure: procName,
    file: fileName,
    passed,
    issues,
    checks: {
      has_status_param: hasStatusParam,
      has_error_param: hasErrorParam,
      has_transaction: hasTransaction,
      has_error_handling: hasErrorHandling,
      uses_p_prefix: usesPPrefix,
    },
  };
}

function generateProcedureTable(results: StoredProcedureResult[]): string {
  const header = "| Procedure | File | Status | Errors | Warnings | Info |";
  const separator = "|-----------|------|--------|--------|----------|------|";

  const rows = results.map((r) => {
    const statusIcon = r.passed ? "✅" : "❌";
    const errors = r.issues.filter((i) => i.severity === "error").length;
    const warnings = r.issues.filter((i) => i.severity === "warning").length;
    const info = r.issues.filter((i) => i.severity === "info").length;

    return `| ${r.procedure} | ${r.file} | ${statusIcon} | ${errors} | ${warnings} | ${info} |`;
  });

  return [header, separator, ...rows].join("\n");
}

function generateProcedureIssueDetails(
  results: StoredProcedureResult[]
): string {
  const filesWithIssues = results.filter((r) => r.issues.length > 0);

  if (filesWithIssues.length === 0) {
    return "✅ **No issues found!** All stored procedures follow MTM standards.";
  }

  let details = "### Issue Details\n\n";

  for (const result of filesWithIssues) {
    details += `**${result.procedure}** (${result.file}):\n`;

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
