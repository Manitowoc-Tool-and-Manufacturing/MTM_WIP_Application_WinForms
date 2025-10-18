import * as fs from "fs";
import * as path from "path";

interface ErrorHandlingIssue {
  file: string;
  line: number;
  severity: "error" | "warning";
  type: string;
  message: string;
  code_snippet?: string;
}

interface ErrorHandlingResult {
  file: string;
  total_error_sites: number;
  using_error_handler: number;
  using_messagebox: number;
  missing_handling: number;
  issues: ErrorHandlingIssue[];
  passed: boolean;
}

export async function validateErrorHandling(args: {
  source_dir: string;
  recursive?: boolean;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { source_dir, recursive = true } = args;

  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  const files = findCSharpFiles(source_dir, recursive);

  if (files.length === 0) {
    throw new Error(`No C# files found in ${source_dir}`);
  }

  const results: ErrorHandlingResult[] = [];

  for (const file of files) {
    const result = validateFileErrorHandling(file);
    results.push(result);
  }

  const passedCount = results.filter((r) => r.passed).length;
  const totalMessageBox = results.reduce((sum, r) => sum + r.using_messagebox, 0);
  const totalErrorHandler = results.reduce((sum, r) => sum + r.using_error_handler, 0);

  const table = generateErrorHandlingTable(results);

  const message = `
## Error Handling Validation Results

${table}

**Summary**:
- Total Files: ${results.length}
- Passed: ${passedCount}
- Failed: ${results.length - passedCount}
- MessageBox.Show Usage: ${totalMessageBox} ❌
- Service_ErrorHandler Usage: ${totalErrorHandler} ✅

${generateErrorHandlingDetails(results)}
`;

  return {
    content: [{ type: "text", text: message }],
  };
}

function findCSharpFiles(dir: string, recursive: boolean): string[] {
  const files: string[] = [];
  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory() && recursive) {
      // Skip bin, obj, and designer files
      if (
        !item.match(/^(bin|obj|\.vs|\.git)$/) &&
        !fullPath.includes(".Designer.cs")
      ) {
        files.push(...findCSharpFiles(fullPath, recursive));
      }
    } else if (
      stat.isFile() &&
      item.endsWith(".cs") &&
      !item.includes(".Designer.cs")
    ) {
      files.push(fullPath);
    }
  }

  return files;
}

function validateFileErrorHandling(filePath: string): ErrorHandlingResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: ErrorHandlingIssue[] = [];

  let usingErrorHandler = 0;
  let usingMessageBox = 0;
  let missingHandling = 0;

  // Find all MessageBox.Show usages
  lines.forEach((line, idx) => {
    if (/MessageBox\.Show/i.test(line)) {
      usingMessageBox++;
      issues.push({
        file: path.basename(filePath),
        line: idx + 1,
        severity: "error",
        type: "MessageBox Usage",
        message: "Using MessageBox.Show instead of Service_ErrorHandler",
        code_snippet: line.trim(),
      });
    }
  });

  // Find Service_ErrorHandler usages (good)
  const errorHandlerMatches = content.match(/Service_ErrorHandler\./g);
  usingErrorHandler = errorHandlerMatches ? errorHandlerMatches.length : 0;

  // Find try-catch blocks without proper error handling
  const tryCatchRegex = /try\s*\{[\s\S]*?\}\s*catch\s*\([^)]*\)\s*\{([\s\S]*?)\}/g;
  let match;

  while ((match = tryCatchRegex.exec(content)) !== null) {
    const catchBlock = match[1];

    // Check if catch block uses Service_ErrorHandler
    if (!/Service_ErrorHandler/i.test(catchBlock)) {
      // Check what it does instead
      if (/MessageBox\.Show/i.test(catchBlock)) {
        // Already counted above
      } else if (
        /throw\s*;/i.test(catchBlock) ||
        /throw\s+new/i.test(catchBlock)
      ) {
        // Re-throwing is acceptable
      } else if (catchBlock.trim().length < 10) {
        // Empty or minimal catch block
        missingHandling++;
        issues.push({
          file: path.basename(filePath),
          line: getLineNumber(content, match.index),
          severity: "warning",
          type: "Missing Error Handler",
          message:
            "Catch block doesn't use Service_ErrorHandler or proper error handling",
        });
      }
    }
  }

  // Find async methods without try-catch
  const asyncMethodRegex =
    /(?:public|private|protected|internal)\s+async\s+Task(?:<[^>]+>)?\s+(\w+)\s*\([^)]*\)\s*\{([\s\S]*?)(?=\n\s*(?:public|private|protected|internal|$))/g;

  while ((match = asyncMethodRegex.exec(content)) !== null) {
    const methodBody = match[2];
    const methodName = match[1];

    if (!/try\s*\{/i.test(methodBody)) {
      issues.push({
        file: path.basename(filePath),
        line: getLineNumber(content, match.index),
        severity: "warning",
        type: "Missing Try-Catch",
        message: `Async method '${methodName}' lacks try-catch block`,
      });
    }
  }

  const passed = issues.filter((i) => i.severity === "error").length === 0;

  return {
    file: path.basename(filePath),
    total_error_sites: usingErrorHandler + usingMessageBox + missingHandling,
    using_error_handler: usingErrorHandler,
    using_messagebox: usingMessageBox,
    missing_handling: missingHandling,
    issues,
    passed,
  };
}

function getLineNumber(content: string, index: number): number {
  return content.substring(0, index).split("\n").length;
}

function generateErrorHandlingTable(results: ErrorHandlingResult[]): string {
  const header =
    "| File | Status | ErrorHandler ✅ | MessageBox ❌ | Missing ⚠️ | Issues |";
  const separator =
    "|------|--------|-----------------|---------------|-----------|--------|";

  const rows = results.map((r) => {
    const statusIcon = r.passed ? "✅" : "❌";
    const issueCount = r.issues.length;

    return `| ${r.file} | ${statusIcon} | ${r.using_error_handler} | ${r.using_messagebox} | ${r.missing_handling} | ${issueCount} |`;
  });

  return [header, separator, ...rows].join("\n");
}

function generateErrorHandlingDetails(
  results: ErrorHandlingResult[]
): string {
  const filesWithIssues = results.filter((r) => r.issues.length > 0);

  if (filesWithIssues.length === 0) {
    return "✅ **Perfect!** All files use Service_ErrorHandler for error handling.";
  }

  let details = "### Issue Details\n\n";

  for (const result of filesWithIssues) {
    const criticalIssues = result.issues.filter((i) => i.severity === "error");

    if (criticalIssues.length > 0) {
      details += `**${result.file}** (${criticalIssues.length} critical issues):\n`;

      for (const issue of criticalIssues) {
        details += `  ❌ [${issue.type}] Line ${issue.line}: ${issue.message}\n`;
        if (issue.code_snippet) {
          details += `     \`${issue.code_snippet}\`\n`;
        }
      }

      details += "\n";
    }
  }

  const warningFiles = filesWithIssues.filter(
    (r) => r.issues.some((i) => i.severity === "warning") && r.passed
  );

  if (warningFiles.length > 0) {
    details += "### Warnings (Non-Blocking)\n\n";

    for (const result of warningFiles) {
      details += `**${result.file}**:\n`;

      for (const issue of result.issues.filter((i) => i.severity === "warning")) {
        details += `  ⚠️ [${issue.type}] Line ${issue.line}: ${issue.message}\n`;
      }

      details += "\n";
    }
  }

  return details;
}
