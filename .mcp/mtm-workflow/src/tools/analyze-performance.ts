import * as fs from "fs";
import * as path from "path";

interface PerformanceIssue {
  file: string;
  line: number;
  severity: "critical" | "warning" | "info";
  category: string;
  issue: string;
  impact: string;
  recommendation: string;
}

interface PerformanceResult {
  file: string;
  issues: PerformanceIssue[];
  critical_count: number;
  warning_count: number;
  info_count: number;
  performance_score: number; // 0-100
}

export async function analyzePerformance(args: {
  source_dir: string;
  recursive?: boolean;
  focus?: "database" | "ui" | "all";
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { source_dir, recursive = true, focus = "all" } = args;

  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  const files = findCSharpFiles(source_dir, recursive);

  if (files.length === 0) {
    throw new Error(`No C# files found in ${source_dir}`);
  }

  const results: PerformanceResult[] = [];

  for (const file of files) {
    const result = analyzeFilePerformance(file, focus);
    if (result.issues.length > 0) {
      results.push(result);
    }
  }

  const totalCritical = results.reduce((sum, r) => sum + r.critical_count, 0);
  const totalWarnings = results.reduce((sum, r) => sum + r.warning_count, 0);

  const message = generatePerformanceReport(results, totalCritical, totalWarnings);

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
      if (!item.match(/^(bin|obj|\.vs|\.git)$/)) {
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

function analyzeFilePerformance(
  filePath: string,
  focus: "database" | "ui" | "all"
): PerformanceResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: PerformanceIssue[] = [];
  const fileName = path.basename(filePath);

  lines.forEach((line, idx) => {
    const lineNum = idx + 1;

    // Database performance issues
    if (focus === "database" || focus === "all") {
      // N+1 query pattern
      if (/foreach\s*\([^)]*\)/.test(line)) {
        const nextLines = lines.slice(idx, idx + 10).join("\n");
        if (/ExecuteDataTable|ExecuteScalar|ExecuteNonQuery/.test(nextLines)) {
          issues.push({
            file: fileName,
            line: lineNum,
            severity: "critical",
            category: "Database",
            issue: "Potential N+1 query pattern in loop",
            impact: "Executes separate database query for each iteration",
            recommendation:
              "Move database call outside loop or use batch operation",
          });
        }
      }

      // Synchronous database calls
      if (/\.Result|\.Wait\(\)/.test(line) && /Execute/.test(line)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "critical",
          category: "Database",
          issue: "Blocking async database operation",
          impact: "Blocks thread pool, reduces scalability",
          recommendation: "Use await instead of .Result or .Wait()",
        });
      }

      // Large dataset without pagination
      if (/ExecuteDataTable.*GetAll/i.test(line)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "warning",
          category: "Database",
          issue: "Potentially loading all records without pagination",
          impact: "High memory usage with large datasets",
          recommendation: "Implement pagination or filtering",
        });
      }

      // No connection disposal
      const contextLines = lines.slice(Math.max(0, idx - 5), idx + 10).join("\n");
      if (/new MySqlConnection/.test(line) && !/using\s*\(/.test(contextLines)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "warning",
          category: "Database",
          issue: "MySqlConnection without using statement",
          impact: "Connection may not be properly disposed",
          recommendation: "Wrap connection in using statement",
        });
      }
    }

    // UI performance issues
    if (focus === "ui" || focus === "all") {
      // UI updates in loops
      if (/foreach|for\s*\(/.test(line)) {
        const loopContent = lines.slice(idx, idx + 20).join("\n");
        if (/\.Text\s*=|\.Items\.Add|\.Rows\.Add/.test(loopContent)) {
          issues.push({
            file: fileName,
            line: lineNum,
            severity: "warning",
            category: "UI",
            issue: "UI updates inside loop",
            impact: "Causes frequent redraws, UI flicker",
            recommendation:
              "Use SuspendLayout/ResumeLayout or batch updates",
          });
        }
      }

      // DataGridView without VirtualMode
      if (/new DataGridView\(\)/.test(line)) {
        const nextLines = lines.slice(idx, idx + 20).join("\n");
        if (!/VirtualMode\s*=\s*true/i.test(nextLines)) {
          issues.push({
            file: fileName,
            line: lineNum,
            severity: "info",
            category: "UI",
            issue: "DataGridView without VirtualMode",
            impact: "May perform poorly with large datasets",
            recommendation:
              "Consider VirtualMode for datasets >1000 rows",
          });
        }
      }

      // Synchronous operations on UI thread
      if (/private\s+void\s+.*_Click/.test(line)) {
        const methodContent = lines.slice(idx, idx + 50).join("\n");
        if (
          /\.Result|\.Wait\(\)|Thread\.Sleep/.test(methodContent) &&
          !/Task\.Run/.test(methodContent)
        ) {
          issues.push({
            file: fileName,
            line: lineNum,
            severity: "critical",
            category: "UI",
            issue: "Blocking operation in UI event handler",
            impact: "Freezes UI, poor user experience",
            recommendation: "Use async/await or Task.Run for long operations",
          });
        }
      }
    }

    // General performance issues
    if (focus === "all") {
      // String concatenation in loops
      if (/\+=.*["']/.test(line)) {
        issues.push({
          file: fileName,
          line: lineNum,
          severity: "warning",
          category: "Memory",
          issue: "String concatenation (potential loop)",
          impact: "Creates many intermediate string objects",
          recommendation: "Use StringBuilder for repeated concatenation",
        });
      }

      // LINQ deferred execution issues
      if (/\.Where\(|\.Select\(/.test(line) && !/.ToList\(\)|.ToArray\(\)/.test(line)) {
        const nextLines = lines.slice(idx, idx + 5).join("\n");
        if (/\.Count\(\)|\.Any\(\)|foreach/.test(nextLines)) {
          issues.push({
            file: fileName,
            line: lineNum,
            severity: "info",
            category: "LINQ",
            issue: "Potentially multiple LINQ enumerations",
            impact: "Query may execute multiple times",
            recommendation:
              "Call .ToList() if collection will be enumerated multiple times",
          });
        }
      }

      // Large objects in closures
      if (/=>\s*{/.test(line)) {
        const closureContent = lines.slice(idx, idx + 10).join("\n");
        if (/DataTable|DataSet|List<.*>/.test(closureContent)) {
          issues.push({
            file: fileName,
            line: lineNum,
            severity: "info",
            category: "Memory",
            issue: "Large object captured in closure",
            impact: "May extend object lifetime unnecessarily",
            recommendation: "Extract needed values before closure",
          });
        }
      }
    }
  });

  const criticalCount = issues.filter((i) => i.severity === "critical").length;
  const warningCount = issues.filter((i) => i.severity === "warning").length;
  const infoCount = issues.filter((i) => i.severity === "info").length;

  // Calculate performance score (0-100)
  const score = Math.max(
    0,
    100 - criticalCount * 20 - warningCount * 5 - infoCount * 1
  );

  return {
    file: fileName,
    issues,
    critical_count: criticalCount,
    warning_count: warningCount,
    info_count: infoCount,
    performance_score: score,
  };
}

function generatePerformanceReport(
  results: PerformanceResult[],
  totalCritical: number,
  totalWarnings: number
): string {
  const avgScore =
    results.reduce((sum, r) => sum + r.performance_score, 0) / results.length;

  let report = `
## Performance Analysis Report

**Files Analyzed:** ${results.length}
**Average Performance Score:** ${avgScore.toFixed(1)}/100
**Critical Issues:** ${totalCritical} 🔴
**Warnings:** ${totalWarnings} 🟡

---

### Summary Table

| File | Score | Critical | Warnings | Info |
|------|-------|----------|----------|------|
${results
  .sort((a, b) => a.performance_score - b.performance_score)
  .slice(0, 10)
  .map(
    (r) =>
      `| ${r.file} | ${r.performance_score}/100 | ${r.critical_count} | ${r.warning_count} | ${r.info_count} |`
  )
  .join("\n")}

---

`;

  // Critical issues
  const criticalFiles = results.filter((r) => r.critical_count > 0);
  if (criticalFiles.length > 0) {
    report += "### 🔴 Critical Performance Issues\n\n";

    for (const result of criticalFiles.slice(0, 5)) {
      report += `**${result.file}** (Score: ${result.performance_score}/100):\n\n`;

      const criticalIssues = result.issues.filter((i) => i.severity === "critical");
      for (const issue of criticalIssues.slice(0, 3)) {
        report += `  🔴 **Line ${issue.line}** [${issue.category}]: ${issue.issue}\n`;
        report += `     📊 **Impact:** ${issue.impact}\n`;
        report += `     💡 **Fix:** ${issue.recommendation}\n\n`;
      }
    }
  }

  // Top recommendations
  report += "\n### 🎯 Top Performance Recommendations\n\n";

  const allIssues = results.flatMap((r) => r.issues);
  const issuesByCategory = groupBy(allIssues, (i) => i.category);

  for (const [category, issues] of Object.entries(issuesByCategory).slice(0, 3)) {
    const criticalCount = issues.filter((i) => i.severity === "critical").length;
    if (criticalCount > 0) {
      report += `- **${category}**: ${criticalCount} critical issues found\n`;
      report += `  ${issues[0].recommendation}\n\n`;
    }
  }

  return report;
}

function groupBy<T>(array: T[], keyFn: (item: T) => string): Record<string, T[]> {
  return array.reduce(
    (result, item) => {
      const key = keyFn(item);
      if (!result[key]) {
        result[key] = [];
      }
      result[key].push(item);
      return result;
    },
    {} as Record<string, T[]>
  );
}
