import * as fs from "fs";
import * as path from "path";

interface XmlDocResult {
  file: string;
  total_public_members: number;
  documented_members: number;
  missing_docs: Array<{
    member: string;
    type: "class" | "method" | "property" | "field";
    line: number;
  }>;
  coverage_percentage: number;
  passed: boolean;
}

export async function checkXmlDocs(args: {
  source_dir: string;
  recursive?: boolean;
  min_coverage?: number;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { source_dir, recursive = true, min_coverage = 80 } = args;

  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  const files = findCSharpFiles(source_dir, recursive);

  if (files.length === 0) {
    throw new Error(`No C# files found in ${source_dir}`);
  }

  const results: XmlDocResult[] = [];

  for (const file of files) {
    const result = checkFileXmlDocs(file, min_coverage);
    results.push(result);
  }

  const passedCount = results.filter((r) => r.passed).length;
  const avgCoverage =
    results.reduce((sum, r) => sum + r.coverage_percentage, 0) / results.length;

  const table = generateXmlDocsTable(results);

  const message = `
## XML Documentation Coverage Report

**Minimum Required Coverage:** ${min_coverage}%
**Average Coverage:** ${avgCoverage.toFixed(1)}%

${table}

**Summary**:
- Total Files: ${results.length}
- Passed (≥${min_coverage}%): ${passedCount}
- Failed (<${min_coverage}%): ${results.length - passedCount}

${generateXmlDocsDetails(results)}
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
      if (
        !item.match(/^(bin|obj|\.vs|\.git)$/) &&
        !fullPath.includes(".Designer.cs")
      ) {
        files.push(...findCSharpFiles(fullPath, recursive));
      }
    } else if (
      stat.isFile() &&
      item.endsWith(".cs") &&
      !item.includes(".Designer.cs") &&
      !item.includes(".g.cs")
    ) {
      files.push(fullPath);
    }
  }

  return files;
}

function checkFileXmlDocs(
  filePath: string,
  minCoverage: number
): XmlDocResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");

  const publicMembers: Array<{
    member: string;
    type: "class" | "method" | "property" | "field";
    line: number;
    hasDoc: boolean;
  }> = [];

  // Find public classes
  lines.forEach((line, idx) => {
    if (/^\s*public\s+(static\s+)?class\s+(\w+)/i.test(line)) {
      const match = line.match(/class\s+(\w+)/i);
      if (match) {
        const hasDoc = idx > 0 && /\/\/\/\s*<summary>/i.test(lines[idx - 1]);
        publicMembers.push({
          member: match[1],
          type: "class",
          line: idx + 1,
          hasDoc,
        });
      }
    }
  });

  // Find public methods
  lines.forEach((line, idx) => {
    if (/^\s*public\s+(static\s+)?(async\s+)?[\w<>]+\s+(\w+)\s*\(/i.test(line)) {
      const match = line.match(/\s+(\w+)\s*\(/i);
      if (match) {
        const hasDoc = idx > 0 && /\/\/\/\s*<summary>/i.test(lines[idx - 1]);
        publicMembers.push({
          member: match[1],
          type: "method",
          line: idx + 1,
          hasDoc,
        });
      }
    }
  });

  // Find public properties
  lines.forEach((line, idx) => {
    if (/^\s*public\s+(static\s+)?[\w<>]+\s+(\w+)\s*\{\s*get/i.test(line)) {
      const match = line.match(/\s+(\w+)\s*\{/i);
      if (match) {
        const hasDoc = idx > 0 && /\/\/\/\s*<summary>/i.test(lines[idx - 1]);
        publicMembers.push({
          member: match[1],
          type: "property",
          line: idx + 1,
          hasDoc,
        });
      }
    }
  });

  // Find public fields
  lines.forEach((line, idx) => {
    if (/^\s*public\s+(static\s+)?(readonly\s+)?[\w<>]+\s+(\w+)\s*[;=]/i.test(line)) {
      const match = line.match(/\s+(\w+)\s*[;=]/i);
      if (match) {
        const hasDoc = idx > 0 && /\/\/\/\s*<summary>/i.test(lines[idx - 1]);
        publicMembers.push({
          member: match[1],
          type: "field",
          line: idx + 1,
          hasDoc,
        });
      }
    }
  });

  const totalPublicMembers = publicMembers.length;
  const documentedMembers = publicMembers.filter((m) => m.hasDoc).length;
  const missingDocs = publicMembers
    .filter((m) => !m.hasDoc)
    .map((m) => ({
      member: m.member,
      type: m.type,
      line: m.line,
    }));

  const coveragePercentage =
    totalPublicMembers > 0 ? (documentedMembers / totalPublicMembers) * 100 : 100;

  const passed = coveragePercentage >= minCoverage;

  return {
    file: path.basename(filePath),
    total_public_members: totalPublicMembers,
    documented_members: documentedMembers,
    missing_docs: missingDocs,
    coverage_percentage: coveragePercentage,
    passed,
  };
}

function generateXmlDocsTable(results: XmlDocResult[]): string {
  const header = "| File | Total Members | Documented | Missing | Coverage | Status |";
  const separator = "|------|---------------|------------|---------|----------|--------|";

  const rows = results.map((r) => {
    const statusIcon = r.passed ? "✅" : "❌";
    const coverage = `${r.coverage_percentage.toFixed(1)}%`;

    return `| ${r.file} | ${r.total_public_members} | ${r.documented_members} | ${r.missing_docs.length} | ${coverage} | ${statusIcon} |`;
  });

  return [header, separator, ...rows].join("\n");
}

function generateXmlDocsDetails(results: XmlDocResult[]): string {
  const filesWithMissing = results.filter((r) => r.missing_docs.length > 0);

  if (filesWithMissing.length === 0) {
    return "✅ **Perfect!** All public members have XML documentation.";
  }

  let details = "### Missing Documentation Details\n\n";

  // Group by coverage level
  const critical = filesWithMissing.filter((r) => r.coverage_percentage < 50);
  const needsWork = filesWithMissing.filter(
    (r) => r.coverage_percentage >= 50 && r.coverage_percentage < 80
  );
  const almostThere = filesWithMissing.filter((r) => r.coverage_percentage >= 80);

  if (critical.length > 0) {
    details += "#### 🔴 Critical (< 50% coverage)\n\n";
    for (const result of critical) {
      details += `**${result.file}** (${result.coverage_percentage.toFixed(1)}% coverage):\n`;
      details += result.missing_docs
        .slice(0, 5)
        .map((m) => `  - \`${m.member}\` (${m.type}, line ${m.line})`)
        .join("\n");
      if (result.missing_docs.length > 5) {
        details += `\n  ... and ${result.missing_docs.length - 5} more`;
      }
      details += "\n\n";
    }
  }

  if (needsWork.length > 0) {
    details += "#### 🟡 Needs Work (50-80% coverage)\n\n";
    for (const result of needsWork) {
      details += `**${result.file}** (${result.coverage_percentage.toFixed(1)}% coverage): ${result.missing_docs.length} missing\n`;
    }
    details += "\n";
  }

  if (almostThere.length > 0) {
    details += "#### 🟢 Almost There (≥80% coverage)\n\n";
    for (const result of almostThere) {
      details += `**${result.file}** (${result.coverage_percentage.toFixed(1)}% coverage): ${result.missing_docs.length} missing\n`;
    }
    details += "\n";
  }

  return details;
}
