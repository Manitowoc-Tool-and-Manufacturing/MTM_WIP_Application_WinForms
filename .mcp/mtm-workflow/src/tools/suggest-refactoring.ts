import * as fs from "fs";
import * as path from "path";

interface RefactoringSuggestion {
  file: string;
  line: number;
  severity: "high" | "medium" | "low";
  category: string;
  issue: string;
  suggestion: string;
  example?: string;
}

interface RefactoringResult {
  file: string;
  suggestions: RefactoringSuggestion[];
  total_suggestions: number;
  high_priority: number;
  medium_priority: number;
  low_priority: number;
}

export async function suggestRefactoring(args: {
  source_dir: string;
  recursive?: boolean;
  file_type?: "csharp" | "sql" | "all";
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { source_dir, recursive = true, file_type = "all" } = args;

  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  const files = findSourceFiles(source_dir, recursive, file_type);

  if (files.length === 0) {
    throw new Error(`No ${file_type} files found in ${source_dir}`);
  }

  const results: RefactoringResult[] = [];

  for (const file of files) {
    const result = analyzeFileForRefactoring(file);
    if (result.suggestions.length > 0) {
      results.push(result);
    }
  }

  const totalSuggestions = results.reduce((sum, r) => sum + r.total_suggestions, 0);
  const highPriority = results.reduce((sum, r) => sum + r.high_priority, 0);

  const message = generateRefactoringReport(results, totalSuggestions, highPriority);

  return {
    content: [{ type: "text", text: message }],
  };
}

function findSourceFiles(
  dir: string,
  recursive: boolean,
  fileType: "csharp" | "sql" | "all"
): string[] {
  const files: string[] = [];
  const items = fs.readdirSync(dir);

  for (const item of items) {
    const fullPath = path.join(dir, item);
    const stat = fs.statSync(fullPath);

    if (stat.isDirectory() && recursive) {
      if (!item.match(/^(bin|obj|\.vs|\.git|node_modules)$/)) {
        files.push(...findSourceFiles(fullPath, recursive, fileType));
      }
    } else if (stat.isFile()) {
      const ext = path.extname(item);
      if (
        (fileType === "all" && (ext === ".cs" || ext === ".sql")) ||
        (fileType === "csharp" && ext === ".cs") ||
        (fileType === "sql" && ext === ".sql")
      ) {
        if (!item.includes(".Designer.cs") && !item.includes(".g.cs")) {
          files.push(fullPath);
        }
      }
    }
  }

  return files;
}

function analyzeFileForRefactoring(filePath: string): RefactoringResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const suggestions: RefactoringSuggestion[] = [];
  const fileName = path.basename(filePath);
  const isCSharp = filePath.endsWith(".cs");

  if (isCSharp) {
    analyzeCSharpFile(lines, fileName, suggestions);
  } else {
    analyzeSqlFile(lines, fileName, suggestions);
  }

  const highPriority = suggestions.filter((s) => s.severity === "high").length;
  const mediumPriority = suggestions.filter((s) => s.severity === "medium").length;
  const lowPriority = suggestions.filter((s) => s.severity === "low").length;

  return {
    file: fileName,
    suggestions,
    total_suggestions: suggestions.length,
    high_priority: highPriority,
    medium_priority: mediumPriority,
    low_priority: lowPriority,
  };
}

function analyzeCSharpFile(
  lines: string[],
  fileName: string,
  suggestions: RefactoringSuggestion[]
): void {
  lines.forEach((line, idx) => {
    const lineNum = idx + 1;

    // Long methods (more than 50 lines)
    if (/^\s*(public|private|protected|internal)\s+(static\s+)?(async\s+)?[\w<>]+\s+\w+\s*\(/i.test(line)) {
      const methodStart = idx;
      let braceCount = 0;
      let methodLength = 0;

      for (let i = methodStart; i < lines.length; i++) {
        braceCount += (lines[i].match(/{/g) || []).length;
        braceCount -= (lines[i].match(/}/g) || []).length;
        methodLength++;

        if (braceCount === 0 && methodLength > 1) {
          break;
        }
      }

      if (methodLength > 50) {
        suggestions.push({
          file: fileName,
          line: lineNum,
          severity: "medium",
          category: "Method Complexity",
          issue: `Method is ${methodLength} lines long`,
          suggestion: "Consider breaking this method into smaller, focused methods",
          example: "Extract logical blocks into private helper methods",
        });
      }
    }

    // Magic numbers
    if (/\d{2,}/.test(line) && !line.includes("//") && !line.includes("string")) {
      const match = line.match(/\d{2,}/);
      if (match && !line.includes("DateTime") && !line.includes("TimeSpan")) {
        suggestions.push({
          file: fileName,
          line: lineNum,
          severity: "low",
          category: "Magic Numbers",
          issue: `Hardcoded number: ${match[0]}`,
          suggestion: "Extract to named constant or configuration",
          example: `private const int MaxRetryAttempts = ${match[0]};`,
        });
      }
    }

    // Nested if statements (more than 3 levels)
    const indentLevel = (line.match(/^\s*/)?.[0].length || 0) / 4;
    if (/^\s*if\s*\(/.test(line) && indentLevel > 3) {
      suggestions.push({
        file: fileName,
        line: lineNum,
        severity: "high",
        category: "Complexity",
        issue: "Deeply nested if statement (>3 levels)",
        suggestion: "Consider early returns, guard clauses, or extracting to methods",
        example: "if (!condition) return; // Early return pattern",
      });
    }

    // String concatenation in loops
    if (/\+=.*"/.test(line) && line.includes("+")) {
      suggestions.push({
        file: fileName,
        line: lineNum,
        severity: "medium",
        category: "Performance",
        issue: "String concatenation in potential loop",
        suggestion: "Use StringBuilder for better performance",
        example: "var sb = new StringBuilder(); sb.Append(...);",
      });
    }

    // Empty catch blocks
    if (/^\s*catch\s*\([^)]*\)\s*{\s*}\s*$/.test(line)) {
      suggestions.push({
        file: fileName,
        line: lineNum,
        severity: "high",
        category: "Error Handling",
        issue: "Empty catch block swallows exceptions",
        suggestion: "Add proper error handling or at minimum log the exception",
        example: "catch (Exception ex) { LoggingUtility.LogApplicationError(ex); }",
      });
    }

    // Large switch statements
    if (/^\s*switch\s*\(/i.test(line)) {
      let caseCount = 0;
      for (let i = idx; i < Math.min(idx + 100, lines.length); i++) {
        if (/^\s*case\s+/.test(lines[i])) caseCount++;
        if (/^\s*}\s*$/.test(lines[i])) break;
      }

      if (caseCount > 7) {
        suggestions.push({
          file: fileName,
          line: lineNum,
          severity: "medium",
          category: "Maintainability",
          issue: `Switch statement with ${caseCount} cases`,
          suggestion: "Consider using strategy pattern or dictionary lookup",
          example: "var strategies = new Dictionary<Type, Action>() { ... };",
        });
      }
    }

    // TODO comments
    if (/\/\/\s*TODO:/i.test(line)) {
      suggestions.push({
        file: fileName,
        line: lineNum,
        severity: "low",
        category: "Technical Debt",
        issue: "TODO comment found",
        suggestion: "Create a tracked issue and implement or remove comment",
      });
    }
  });
}

function analyzeSqlFile(
  lines: string[],
  fileName: string,
  suggestions: RefactoringSuggestion[]
): void {
  const content = lines.join("\n");

  // Large stored procedures (>200 lines)
  if (lines.length > 200) {
    suggestions.push({
      file: fileName,
      line: 1,
      severity: "medium",
      category: "Complexity",
      issue: `Stored procedure is ${lines.length} lines long`,
      suggestion: "Consider breaking into smaller procedures",
    });
  }

  // Multiple SELECT statements
  const selectCount = (content.match(/SELECT\s+/gi) || []).length;
  if (selectCount > 5) {
    suggestions.push({
      file: fileName,
      line: 1,
      severity: "low",
      category: "Performance",
      issue: `${selectCount} SELECT statements found`,
      suggestion: "Consider combining related queries or using temp tables",
    });
  }

  // Cursor usage
  lines.forEach((line, idx) => {
    if (/DECLARE\s+.*CURSOR\s+FOR/i.test(line)) {
      suggestions.push({
        file: fileName,
        line: idx + 1,
        severity: "high",
        category: "Performance",
        issue: "Cursor usage detected",
        suggestion: "Replace cursor with set-based operation for better performance",
        example: "Use INSERT...SELECT or UPDATE with JOIN instead",
      });
    }

    // SELECT *
    if (/SELECT\s+\*/i.test(line)) {
      suggestions.push({
        file: fileName,
        line: idx + 1,
        severity: "low",
        category: "Maintainability",
        issue: "SELECT * usage",
        suggestion: "Explicitly list required columns",
        example: "SELECT Column1, Column2 FROM Table",
      });
    }

    // NOLOCK hints
    if (/WITH\s*\(\s*NOLOCK\s*\)/i.test(line)) {
      suggestions.push({
        file: fileName,
        line: idx + 1,
        severity: "medium",
        category: "Data Integrity",
        issue: "NOLOCK hint may cause dirty reads",
        suggestion: "Consider READ COMMITTED SNAPSHOT or proper transaction isolation",
      });
    }
  });
}

function generateRefactoringReport(
  results: RefactoringResult[],
  totalSuggestions: number,
  highPriority: number
): string {
  let report = `
## Refactoring Suggestions Report

**Total Files Analyzed:** ${results.length}
**Total Suggestions:** ${totalSuggestions}
**High Priority:** ${highPriority}

---

`;

  // Group by priority
  const highPriorityFiles = results.filter((r) => r.high_priority > 0);
  const mediumPriorityFiles = results.filter((r) => r.medium_priority > 0 && r.high_priority === 0);

  if (highPriorityFiles.length > 0) {
    report += "### 🔴 High Priority Refactoring\n\n";

    for (const result of highPriorityFiles.slice(0, 5)) {
      report += `**${result.file}** (${result.high_priority} high, ${result.medium_priority} medium, ${result.low_priority} low):\n\n`;

      const highSuggestions = result.suggestions.filter((s) => s.severity === "high");
      for (const suggestion of highSuggestions.slice(0, 3)) {
        report += `  🔴 **Line ${suggestion.line}** [${suggestion.category}]: ${suggestion.issue}\n`;
        report += `     💡 ${suggestion.suggestion}\n`;
        if (suggestion.example) {
          report += `     \`\`\`\n     ${suggestion.example}\n     \`\`\`\n`;
        }
        report += "\n";
      }
    }

    if (highPriorityFiles.length > 5) {
      report += `\n*...and ${highPriorityFiles.length - 5} more files with high priority issues*\n\n`;
    }
  }

  if (mediumPriorityFiles.length > 0) {
    report += "### 🟡 Medium Priority Refactoring\n\n";

    for (const result of mediumPriorityFiles.slice(0, 3)) {
      report += `**${result.file}**: ${result.medium_priority} suggestions\n`;
    }

    if (mediumPriorityFiles.length > 3) {
      report += `\n*...and ${mediumPriorityFiles.length - 3} more files*\n`;
    }
  }

  report += "\n---\n\n";
  report += "**Recommendation:** Address high priority issues first, especially those affecting performance and maintainability.\n";

  return report;
}
