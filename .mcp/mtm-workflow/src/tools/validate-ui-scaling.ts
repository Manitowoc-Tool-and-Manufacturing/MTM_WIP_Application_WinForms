import * as fs from "fs";
import * as path from "path";

interface UiScalingIssue {
  file: string;
  line?: number;
  severity: "critical" | "error" | "warning" | "info";
  category: string;
  message: string;
  recommendation?: string;
}

interface UiScalingResult {
  file: string;
  passed: boolean;
  issues: UiScalingIssue[];
  checks: {
    has_autoscalemode_dpi: boolean;
    has_theme_dpi_scaling: boolean;
    has_theme_layout_adjustments: boolean;
    font_sizes_relative: boolean;
    button_sizes_adequate: boolean;
    datagridview_autosizemode: boolean;
    no_fixed_pixel_layouts: boolean;
  };
}

interface ValidationSummary {
  total_files: number;
  passed_files: number;
  failed_files: number;
  critical_issues: number;
  error_issues: number;
  warning_issues: number;
  results: UiScalingResult[];
  summary_table: string;
}

export async function validateUiScaling(args: {
  source_dir: string;
  recursive?: boolean;
  file_types?: string[];
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const {
    source_dir,
    recursive = true,
    file_types = ["*.cs", "*.Designer.cs"],
  } = args;

  // Validate directory exists
  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  // Find all relevant files
  const files = findUiFiles(source_dir, recursive, file_types);

  if (files.length === 0) {
    throw new Error(`No UI files found in ${source_dir}`);
  }

  const results: UiScalingResult[] = [];

  // Validate each file
  for (const file of files) {
    const result = validateUiFile(file);
    results.push(result);
  }

  // Generate summary
  const passedCount = results.filter((r) => r.passed).length;
  const criticalCount = results.reduce(
    (sum, r) => sum + r.issues.filter((i) => i.severity === "critical").length,
    0
  );
  const errorCount = results.reduce(
    (sum, r) => sum + r.issues.filter((i) => i.severity === "error").length,
    0
  );
  const warningCount = results.reduce(
    (sum, r) => sum + r.issues.filter((i) => i.severity === "warning").length,
    0
  );

  const table = generateValidationTable(results);

  const summary: ValidationSummary = {
    total_files: results.length,
    passed_files: passedCount,
    failed_files: results.length - passedCount,
    critical_issues: criticalCount,
    error_issues: errorCount,
    warning_issues: warningCount,
    results,
    summary_table: table,
  };

  const message = `
## UI Scaling Validation Results

${table}

**Summary**:
- Total Files: ${summary.total_files}
- Passed: ${summary.passed_files} (${Math.round((summary.passed_files / summary.total_files) * 100)}%)
- Failed: ${summary.failed_files}
- 🔴 Critical: ${summary.critical_issues}
- ⚠️ Errors: ${summary.error_issues}
- ⚡ Warnings: ${summary.warning_issues}

${generateIssueDetails(results)}

${generateRecommendations(results)}
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

function findUiFiles(
  dir: string,
  recursive: boolean,
  patterns: string[]
): string[] {
  const files: string[] = [];

  function traverse(currentDir: string) {
    const entries = fs.readdirSync(currentDir, { withFileTypes: true });

    for (const entry of entries) {
      const fullPath = path.join(currentDir, entry.name);

      if (entry.isDirectory()) {
        // Skip common non-UI directories
        if (
          entry.name === "bin" ||
          entry.name === "obj" ||
          entry.name === "Properties" ||
          entry.name === "Resources"
        ) {
          continue;
        }

        if (recursive) {
          traverse(fullPath);
        }
      } else if (entry.isFile()) {
        // Check if file matches UI patterns
        const isUiFile =
          (fullPath.includes("\\Forms\\") ||
            fullPath.includes("\\Controls\\") ||
            fullPath.includes("/Forms/") ||
            fullPath.includes("/Controls/")) &&
          (entry.name.endsWith(".cs") || entry.name.endsWith(".Designer.cs"));

        if (isUiFile) {
          files.push(fullPath);
        }
      }
    }
  }

  traverse(dir);
  return files;
}

function validateUiFile(filePath: string): UiScalingResult {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const issues: UiScalingIssue[] = [];

  // Initialize checks
  const checks = {
    has_autoscalemode_dpi: false,
    has_theme_dpi_scaling: false,
    has_theme_layout_adjustments: false,
    font_sizes_relative: true,
    button_sizes_adequate: true,
    datagridview_autosizemode: true,
    no_fixed_pixel_layouts: true,
  };

  // Check 1: AutoScaleMode = Dpi
  const hasAutoScaleModeDpi = content.includes("AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi");
  const hasAutoScaleModeFont = content.includes("AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font");
  const hasAutoScaleModeInherit = content.includes("AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit");

  if (filePath.endsWith(".Designer.cs")) {
    if (hasAutoScaleModeFont) {
      issues.push({
        file: filePath,
        severity: "critical",
        category: "AutoScaleMode",
        message: "AutoScaleMode set to Font instead of Dpi",
        recommendation:
          "Change to AutoScaleMode.Dpi for consistent DPI scaling",
      });
    } else if (hasAutoScaleModeDpi) {
      checks.has_autoscalemode_dpi = true;
    } else if (!hasAutoScaleModeInherit && !hasAutoScaleModeDpi) {
      issues.push({
        file: filePath,
        severity: "error",
        category: "AutoScaleMode",
        message: "AutoScaleMode not set or set to incorrect value",
        recommendation: "Set to AutoScaleMode.Dpi in designer",
      });
    }
  }

  // Check 2: Theme DPI Scaling Call
  const hasThemeDpiScaling = content.includes("Core_Themes.ApplyDpiScaling(this)");
  if (!filePath.endsWith(".Designer.cs") && content.includes("class ") && content.includes(": UserControl")) {
    if (!hasThemeDpiScaling) {
      issues.push({
        file: filePath,
        severity: "critical",
        category: "Theme DPI",
        message: "Missing Core_Themes.ApplyDpiScaling(this) in constructor",
        recommendation:
          "Add Core_Themes.ApplyDpiScaling(this) after InitializeComponent()",
      });
    } else {
      checks.has_theme_dpi_scaling = true;
    }
  }

  // Check 3: Theme Layout Adjustments Call
  const hasThemeLayoutAdjustments = content.includes("Core_Themes.ApplyRuntimeLayoutAdjustments(this)");
  if (!filePath.endsWith(".Designer.cs") && content.includes("class ") && content.includes(": UserControl")) {
    if (!hasThemeLayoutAdjustments) {
      issues.push({
        file: filePath,
        severity: "error",
        category: "Theme Layout",
        message: "Missing Core_Themes.ApplyRuntimeLayoutAdjustments(this) in constructor",
        recommendation:
          "Add Core_Themes.ApplyRuntimeLayoutAdjustments(this) after ApplyDpiScaling()",
      });
    } else {
      checks.has_theme_layout_adjustments = true;
    }
  }

  // Check 4: Absolute Font Sizes
  const absoluteFontRegex = /new\s+Font\([^)]*,\s*(\d+)f?\s*[,)]/g;
  let match;
  while ((match = absoluteFontRegex.exec(content)) !== null) {
    const fontSize = parseInt(match[1], 10);
    if (fontSize > 0) {
      const lineNum = content.substring(0, match.index).split("\n").length;
      issues.push({
        file: filePath,
        line: lineNum,
        severity: "warning",
        category: "Font Size",
        message: `Absolute font size detected: ${fontSize}pt`,
        recommendation:
          "Use relative font sizes or Core_Themes.ApplyDpiScaling to adjust fonts",
      });
      checks.font_sizes_relative = false;
    }
  }

  // Check 5: Button Sizes (Width < 75 or Height < 30)
  const buttonSizeRegex = /\.Size\s*=\s*new\s+Size\((\d+),\s*(\d+)\)/g;
  while ((match = buttonSizeRegex.exec(content)) !== null) {
    const width = parseInt(match[1], 10);
    const height = parseInt(match[2], 10);
    const lineNum = content.substring(0, match.index).split("\n").length;

    if (width < 75 || height < 30) {
      issues.push({
        file: filePath,
        line: lineNum,
        severity: "warning",
        category: "Button Size",
        message: `Small button size detected: ${width}x${height}`,
        recommendation: "Minimum recommended: 75x30 for DPI scaling",
      });
      checks.button_sizes_adequate = false;
    }
  }

  // Check 6: DataGridView AutoSizeColumnsMode
  const hasDataGridView = content.includes("DataGridView");
  const hasAutoSizeColumnsMode = content.includes("AutoSizeColumnsMode");
  if (hasDataGridView && filePath.endsWith(".Designer.cs")) {
    if (!hasAutoSizeColumnsMode) {
      issues.push({
        file: filePath,
        severity: "warning",
        category: "DataGridView",
        message: "DataGridView missing AutoSizeColumnsMode setting",
        recommendation:
          "Set AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill or AllCells",
      });
      checks.datagridview_autosizemode = false;
    } else {
      checks.datagridview_autosizemode = true;
    }
  }

  // Check 7: Fixed Pixel Layout Panels
  const fixedPanelRegex = /\.Size\s*=\s*new\s+Size\((\d{3,}),\s*(\d{3,})\)/g;
  while ((match = fixedPanelRegex.exec(content)) !== null) {
    const width = parseInt(match[1], 10);
    const height = parseInt(match[2], 10);
    const lineNum = content.substring(0, match.index).split("\n").length;

    // Large fixed sizes likely indicate main panels/forms
    if (width > 800 || height > 600) {
      issues.push({
        file: filePath,
        line: lineNum,
        severity: "info",
        category: "Fixed Layout",
        message: `Large fixed size detected: ${width}x${height}`,
        recommendation:
          "Consider using Dock/Anchor or TableLayoutPanel for responsive layout",
      });
      checks.no_fixed_pixel_layouts = false;
    }
  }

  // Determine if passed (no critical or error issues)
  const criticalOrErrorIssues = issues.filter(
    (i) => i.severity === "critical" || i.severity === "error"
  );
  const passed = criticalOrErrorIssues.length === 0;

  return {
    file: filePath,
    passed,
    issues,
    checks,
  };
}

function generateValidationTable(results: UiScalingResult[]): string {
  const rows: string[] = [
    "| File | Status | Critical | Errors | Warnings | Info |",
    "|------|--------|----------|--------|----------|------|",
  ];

  for (const result of results) {
    const fileName = path.basename(result.file);
    const status = result.passed ? "✅ PASS" : "❌ FAIL";
    const critical = result.issues.filter((i) => i.severity === "critical").length;
    const errors = result.issues.filter((i) => i.severity === "error").length;
    const warnings = result.issues.filter((i) => i.severity === "warning").length;
    const info = result.issues.filter((i) => i.severity === "info").length;

    rows.push(
      `| ${fileName} | ${status} | ${critical} | ${errors} | ${warnings} | ${info} |`
    );
  }

  return rows.join("\n");
}

function generateIssueDetails(results: UiScalingResult[]): string {
  const sections: string[] = [];

  // Critical Issues
  const criticalIssues = results.flatMap((r) =>
    r.issues.filter((i) => i.severity === "critical")
  );
  if (criticalIssues.length > 0) {
    sections.push("### 🔴 Critical Issues (MUST FIX)\n");
    for (const issue of criticalIssues) {
      const location = issue.line ? `:${issue.line}` : "";
      sections.push(
        `**${path.basename(issue.file)}${location}** - ${issue.category}: ${issue.message}`
      );
      if (issue.recommendation) {
        sections.push(`   → ${issue.recommendation}`);
      }
      sections.push("");
    }
  }

  // Error Issues
  const errorIssues = results.flatMap((r) =>
    r.issues.filter((i) => i.severity === "error")
  );
  if (errorIssues.length > 0) {
    sections.push("### ⚠️ Errors (SHOULD FIX)\n");
    for (const issue of errorIssues) {
      const location = issue.line ? `:${issue.line}` : "";
      sections.push(
        `**${path.basename(issue.file)}${location}** - ${issue.category}: ${issue.message}`
      );
      if (issue.recommendation) {
        sections.push(`   → ${issue.recommendation}`);
      }
      sections.push("");
    }
  }

  // Warning Issues (only show count if many)
  const warningIssues = results.flatMap((r) =>
    r.issues.filter((i) => i.severity === "warning")
  );
  if (warningIssues.length > 0) {
    sections.push(
      `### ⚡ Warnings (${warningIssues.length} total - showing first 10)\n`
    );
    for (const issue of warningIssues.slice(0, 10)) {
      const location = issue.line ? `:${issue.line}` : "";
      sections.push(
        `**${path.basename(issue.file)}${location}** - ${issue.category}: ${issue.message}`
      );
      sections.push("");
    }
    if (warningIssues.length > 10) {
      sections.push(`... and ${warningIssues.length - 10} more warnings\n`);
    }
  }

  return sections.join("\n");
}

function generateRecommendations(results: UiScalingResult[]): string {
  const sections: string[] = ["## Recommendations\n"];

  const criticalCount = results.reduce(
    (sum, r) => sum + r.issues.filter((i) => i.severity === "critical").length,
    0
  );
  const errorCount = results.reduce(
    (sum, r) => sum + r.issues.filter((i) => i.severity === "error").length,
    0
  );

  if (criticalCount > 0) {
    sections.push(
      `### Priority 1: Fix ${criticalCount} Critical Issues`
    );
    sections.push(
      "- AutoScaleMode must be set to Dpi for all Forms and UserControls"
    );
    sections.push(
      "- All UserControls must call Core_Themes.ApplyDpiScaling(this) in constructor"
    );
    sections.push("");
  }

  if (errorCount > 0) {
    sections.push(`### Priority 2: Fix ${errorCount} Error Issues`);
    sections.push(
      "- Add Core_Themes.ApplyRuntimeLayoutAdjustments(this) to all UserControls"
    );
    sections.push("- Set AutoScaleMode for Forms missing the setting");
    sections.push("");
  }

  sections.push("### Priority 3: Review Warnings");
  sections.push("- Consider adjusting small button sizes for better DPI scaling");
  sections.push("- Review absolute font sizes and use relative sizing where possible");
  sections.push("- Configure DataGridView AutoSizeColumnsMode for responsive columns");
  sections.push("");

  sections.push("### Next Steps");
  sections.push(
    "1. Run validation: `mcp_mtm-workflow_validate_ui_scaling` with source_dir"
  );
  sections.push("2. Fix all critical issues first (AutoScaleMode, Theme DPI calls)");
  sections.push("3. Address errors (Layout adjustment calls)");
  sections.push("4. Review and fix warnings based on priority");
  sections.push("5. Re-run validation to confirm compliance");

  return sections.join("\n");
}
