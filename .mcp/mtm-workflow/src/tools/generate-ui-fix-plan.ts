import * as fs from "fs";
import * as path from "path";

interface UiFixAction {
  file: string;
  lineNumber: number | null;
  currentCode: string;
  issueType: "critical" | "error" | "warning" | "info";
  category: string;
  problem: string;
  fix: {
    action: "replace" | "insert" | "modify";
    location: "line" | "constructor" | "designer";
    newCode: string;
    searchPattern?: string;
    insertAfter?: string;
  };
  validation: {
    requiredPattern: string;
    backupRequired: boolean;
    verificationSteps: string[];
  };
  metadata: {
    className?: string;
    controlType?: string;
    isDesignerFile: boolean;
    relatedFiles: string[];
  };
}

interface UiFixPlan {
  generatedAt: string;
  sourceDirectory: string;
  totalFiles: number;
  filesRequiringFixes: number;
  totalActions: number;
  actionsBySeverity: {
    critical: number;
    error: number;
    warning: number;
    info: number;
  };
  actions: UiFixAction[];
  summary: {
    criticalIssues: string[];
    errorIssues: string[];
    estimatedTimeMinutes: number;
  };
}

export async function generateUiFixPlan(args: {
  source_dir: string;
  recursive?: boolean;
  include_warnings?: boolean;
  output_file?: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const {
    source_dir,
    recursive = true,
    include_warnings = false,
    output_file,
  } = args;

  // Validate directory exists
  if (!fs.existsSync(source_dir)) {
    throw new Error(`Directory not found: ${source_dir}`);
  }

  // Find all UI files
  const files = findUiFiles(source_dir, recursive);

  if (files.length === 0) {
    throw new Error(`No UI files found in ${source_dir}`);
  }

  // Process each file and generate fix actions
  const actions: UiFixAction[] = [];
  const fileSet = new Set<string>();

  for (const file of files) {
    const fileActions = analyzeFileForFixes(file, include_warnings);
    actions.push(...fileActions);
    if (fileActions.length > 0) {
      fileSet.add(file);
    }
  }

  // Generate fix plan
  const plan: UiFixPlan = {
    generatedAt: new Date().toISOString(),
    sourceDirectory: source_dir,
    totalFiles: files.length,
    filesRequiringFixes: fileSet.size,
    totalActions: actions.length,
    actionsBySeverity: {
      critical: actions.filter((a) => a.issueType === "critical").length,
      error: actions.filter((a) => a.issueType === "error").length,
      warning: actions.filter((a) => a.issueType === "warning").length,
      info: actions.filter((a) => a.issueType === "info").length,
    },
    actions,
    summary: {
      criticalIssues: Array.from(
        new Set(
          actions
            .filter((a) => a.issueType === "critical")
            .map((a) => a.category)
        )
      ),
      errorIssues: Array.from(
        new Set(
          actions.filter((a) => a.issueType === "error").map((a) => a.category)
        )
      ),
      estimatedTimeMinutes: estimateFixTime(actions),
    },
  };

  // Write to file if specified
  if (output_file) {
    fs.writeFileSync(output_file, JSON.stringify(plan, null, 2), "utf-8");
  }

  // Generate report
  const report = generateReport(plan, output_file);

  return {
    content: [
      {
        type: "text",
        text: report,
      },
    ],
  };
}

function findUiFiles(dir: string, recursive: boolean): string[] {
  const files: string[] = [];

  function traverse(currentDir: string) {
    const entries = fs.readdirSync(currentDir, { withFileTypes: true });

    for (const entry of entries) {
      const fullPath = path.join(currentDir, entry.name);

      if (entry.isDirectory()) {
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

function analyzeFileForFixes(
  filePath: string,
  includeWarnings: boolean
): UiFixAction[] {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");
  const actions: UiFixAction[] = [];
  const isDesigner = filePath.endsWith(".Designer.cs");
  const className = extractClassName(content);

  // Check 1: AutoScaleMode.Font → Dpi (CRITICAL)
  const autoScaleFontMatch = content.match(
    /AutoScaleMode\s*=\s*System\.Windows\.Forms\.AutoScaleMode\.Font/
  );
  if (autoScaleFontMatch && isDesigner) {
    const lineNum = findLineNumber(content, autoScaleFontMatch[0]);
    const currentLine = lines[lineNum - 1]?.trim() || "";

    actions.push({
      file: filePath,
      lineNumber: lineNum,
      currentCode: currentLine,
      issueType: "critical",
      category: "AutoScaleMode",
      problem: "AutoScaleMode set to Font instead of Dpi",
      fix: {
        action: "replace",
        location: "line",
        searchPattern:
          "AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font",
        newCode: "AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi",
      },
      validation: {
        requiredPattern: "AutoScaleMode.Dpi",
        backupRequired: true,
        verificationSteps: [
          "Verify line contains AutoScaleMode.Dpi",
          "Ensure no other AutoScaleMode.Font references exist",
          "Check file compiles without errors",
        ],
      },
      metadata: {
        className,
        isDesignerFile: true,
        relatedFiles: [filePath.replace(".Designer.cs", ".cs")],
      },
    });
  }

  // Check 2: Missing AutoScaleMode (ERROR)
  const hasAutoScaleMode =
    content.includes("AutoScaleMode = ") || content.includes("AutoScaleMode=");
  if (
    !hasAutoScaleMode &&
    isDesigner &&
    (content.includes(": Form") || content.includes(": UserControl"))
  ) {
    // Find InitializeComponent method
    const initMatch = content.match(/private void InitializeComponent\(\)/);
    if (initMatch) {
      const initLine = findLineNumber(content, initMatch[0]);

      actions.push({
        file: filePath,
        lineNumber: initLine,
        currentCode: lines[initLine - 1]?.trim() || "",
        issueType: "error",
        category: "AutoScaleMode",
        problem: "AutoScaleMode not set",
        fix: {
          action: "insert",
          location: "designer",
          insertAfter: "this.SuspendLayout();",
          newCode:
            "            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;",
        },
        validation: {
          requiredPattern: "AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi",
          backupRequired: true,
          verificationSteps: [
            "Verify AutoScaleMode.Dpi added after SuspendLayout",
            "Check proper indentation (12 spaces)",
            "Ensure file compiles",
          ],
        },
        metadata: {
          className,
          isDesignerFile: true,
          relatedFiles: [filePath.replace(".Designer.cs", ".cs")],
        },
      });
    }
  }

  // Check 3: Missing Core_Themes.ApplyDpiScaling (CRITICAL)
  if (!isDesigner && content.includes(": UserControl")) {
    const hasApplyDpiScaling = content.includes("Core_Themes.ApplyDpiScaling(this)");
    const hasConstructor = content.match(
      new RegExp(`public\\s+${className}\\s*\\([^)]*\\)`)
    );

    if (!hasApplyDpiScaling && hasConstructor) {
      // Handle both block-body and expression-body constructors
      // Block: public ClassName() { ... }
      // Expression: public ClassName() => InitializeComponent();
      const blockConstructorMatch = content.match(
        new RegExp(`public\\s+${className}\\s*\\([^)]*\\)\\s*{`)
      );
      const expressionConstructorMatch = content.match(
        new RegExp(`public\\s+${className}\\s*\\([^)]*\\)\\s*=>`)
      );
      
      const constructorMatch = blockConstructorMatch || expressionConstructorMatch;
      
      if (constructorMatch) {
        const constructorLine = findLineNumber(content, constructorMatch[0]);

        // Determine fix strategy based on constructor type
        let fixAction, newCode, insertAfter;
        
        if (expressionConstructorMatch) {
          // Expression-body: public ClassName() => InitializeComponent();
          // Strategy: Replace entire line to convert to block body with both calls
          fixAction = "replace";
          const currentConstructor = lines[constructorLine - 1]?.trim() || "";
          newCode = `        public ${className}()\n        {\n            InitializeComponent();\n            Core_Themes.ApplyDpiScaling(this);\n        }`;
          insertAfter = undefined;
        } else {
          // Block-body: public ClassName() { InitializeComponent(); }
          // Strategy: Insert after InitializeComponent();
          fixAction = "insert";
          newCode = "            Core_Themes.ApplyDpiScaling(this);";
          insertAfter = "InitializeComponent();";
        }

        actions.push({
          file: filePath,
          lineNumber: constructorLine,
          currentCode: lines[constructorLine - 1]?.trim() || "",
          issueType: "critical",
          category: "Theme DPI",
          problem: "Missing Core_Themes.ApplyDpiScaling(this) in constructor",
          fix: {
            action: fixAction as "insert" | "replace",
            location: "constructor",
            insertAfter,
            searchPattern: expressionConstructorMatch ? lines[constructorLine - 1]?.trim() : undefined,
            newCode,
          },
          validation: {
            requiredPattern: "Core_Themes.ApplyDpiScaling(this)",
            backupRequired: true,
            verificationSteps: [
              expressionConstructorMatch
                ? "Verify constructor converted from expression-body to block-body"
                : "Verify Core_Themes.ApplyDpiScaling(this) added after InitializeComponent",
              "Check proper indentation",
              "Ensure file compiles",
              "Verify using statement for Core exists",
            ],
          },
          metadata: {
            className,
            controlType: "UserControl",
            isDesignerFile: false,
            relatedFiles: [filePath.replace(".cs", ".Designer.cs")],
          },
        });
      }
    }
  }

  // Check 4: Missing Core_Themes.ApplyRuntimeLayoutAdjustments (ERROR)
  if (!isDesigner && content.includes(": UserControl")) {
    const hasLayoutAdjustments = content.includes(
      "Core_Themes.ApplyRuntimeLayoutAdjustments(this)"
    );
    const hasApplyDpiScaling = content.includes("Core_Themes.ApplyDpiScaling(this)");

    if (!hasLayoutAdjustments && hasApplyDpiScaling) {
      const dpiScalingMatch = content.match(
        /Core_Themes\.ApplyDpiScaling\(this\);/
      );
      if (dpiScalingMatch) {
        const dpiLine = findLineNumber(content, dpiScalingMatch[0]);

        actions.push({
          file: filePath,
          lineNumber: dpiLine,
          currentCode: lines[dpiLine - 1]?.trim() || "",
          issueType: "error",
          category: "Theme Layout",
          problem:
            "Missing Core_Themes.ApplyRuntimeLayoutAdjustments(this) in constructor",
          fix: {
            action: "insert",
            location: "constructor",
            insertAfter: "Core_Themes.ApplyDpiScaling(this);",
            newCode:
              "            Core_Themes.ApplyRuntimeLayoutAdjustments(this);",
          },
          validation: {
            requiredPattern: "Core_Themes.ApplyRuntimeLayoutAdjustments(this)",
            backupRequired: true,
            verificationSteps: [
              "Verify ApplyRuntimeLayoutAdjustments added after ApplyDpiScaling",
              "Check proper indentation",
              "Ensure file compiles",
            ],
          },
          metadata: {
            className,
            controlType: "UserControl",
            isDesignerFile: false,
            relatedFiles: [filePath.replace(".cs", ".Designer.cs")],
          },
        });
      }
    }
  }

  // Check 5: Small button sizes (WARNING) - only if includeWarnings
  if (includeWarnings && isDesigner) {
    const buttonSizeRegex = /\.Size\s*=\s*new\s+Size\((\d+),\s*(\d+)\)/g;
    let match;
    while ((match = buttonSizeRegex.exec(content)) !== null) {
      const width = parseInt(match[1], 10);
      const height = parseInt(match[2], 10);

      if (width < 75 || height < 30) {
        const lineNum = findLineNumber(content, match[0]);

        actions.push({
          file: filePath,
          lineNumber: lineNum,
          currentCode: lines[lineNum - 1]?.trim() || "",
          issueType: "warning",
          category: "Button Size",
          problem: `Small button size detected: ${width}x${height}`,
          fix: {
            action: "replace",
            location: "line",
            searchPattern: `Size(${width}, ${height})`,
            newCode: `Size(${Math.max(width, 75)}, ${Math.max(height, 30)})`,
          },
          validation: {
            requiredPattern: "Size(",
            backupRequired: true,
            verificationSteps: [
              "Verify new size meets minimum requirements (75x30)",
              "Check button still fits in layout",
              "Test button clickability at different DPI",
            ],
          },
          metadata: {
            className,
            isDesignerFile: true,
            relatedFiles: [],
          },
        });
      }
    }
  }

  // Check 6: Absolute font sizes (WARNING) - only if includeWarnings
  if (includeWarnings) {
    const absoluteFontRegex = /new\s+Font\([^)]*,\s*(\d+)f?\s*[,)]/g;
    let match;
    while ((match = absoluteFontRegex.exec(content)) !== null) {
      const fontSize = parseInt(match[1], 10);
      if (fontSize > 0) {
        const lineNum = findLineNumber(content, match[0]);

        actions.push({
          file: filePath,
          lineNumber: lineNum,
          currentCode: lines[lineNum - 1]?.trim() || "",
          issueType: "warning",
          category: "Font Size",
          problem: `Absolute font size detected: ${fontSize}pt`,
          fix: {
            action: "modify",
            location: "line",
            searchPattern: match[0],
            newCode: `// TODO: Consider using relative font sizing or DPI-aware font adjustment for ${fontSize}pt`,
          },
          validation: {
            requiredPattern: "Font(",
            backupRequired: true,
            verificationSteps: [
              "Review font size for DPI appropriateness",
              "Consider using SystemFonts or DPI-scaled fonts",
              "Test text readability at different DPI settings",
            ],
          },
          metadata: {
            className,
            isDesignerFile: isDesigner,
            relatedFiles: [],
          },
        });
      }
    }
  }

  return actions;
}

function extractClassName(content: string): string {
  const classMatch = content.match(/class\s+(\w+)/);
  return classMatch ? classMatch[1] : "Unknown";
}

function findLineNumber(content: string, searchText: string): number {
  const index = content.indexOf(searchText);
  if (index === -1) return 0;
  return content.substring(0, index).split("\n").length;
}

function estimateFixTime(actions: UiFixAction[]): number {
  // Estimate based on severity and action count
  const critical = actions.filter((a) => a.issueType === "critical").length;
  const errors = actions.filter((a) => a.issueType === "error").length;
  const warnings = actions.filter((a) => a.issueType === "warning").length;

  // Critical: 2 min each, Errors: 1 min each, Warnings: 0.5 min each
  return critical * 2 + errors * 1 + warnings * 0.5;
}

function generateReport(plan: UiFixPlan, outputFile?: string): string {
  const sections: string[] = [];

  sections.push("## UI Fix Plan Generated\n");
  sections.push(`**Generated At**: ${plan.generatedAt}`);
  sections.push(`**Source Directory**: ${plan.sourceDirectory}`);
  sections.push(`**Total Files Analyzed**: ${plan.totalFiles}`);
  sections.push(`**Files Requiring Fixes**: ${plan.filesRequiringFixes}\n`);

  sections.push("### Fix Action Summary\n");
  sections.push(`- **Total Actions**: ${plan.totalActions}`);
  sections.push(`- 🔴 Critical: ${plan.actionsBySeverity.critical}`);
  sections.push(`- ⚠️ Errors: ${plan.actionsBySeverity.error}`);
  sections.push(`- ⚡ Warnings: ${plan.actionsBySeverity.warning}`);
  sections.push(`- ℹ️ Info: ${plan.actionsBySeverity.info}\n`);

  sections.push(
    `**Estimated Fix Time**: ${Math.ceil(plan.summary.estimatedTimeMinutes)} minutes\n`
  );

  if (plan.summary.criticalIssues.length > 0) {
    sections.push("### Critical Issue Types\n");
    for (const issue of plan.summary.criticalIssues) {
      sections.push(`- ${issue}`);
    }
    sections.push("");
  }

  if (plan.summary.errorIssues.length > 0) {
    sections.push("### Error Issue Types\n");
    for (const issue of plan.summary.errorIssues) {
      sections.push(`- ${issue}`);
    }
    sections.push("");
  }

  // Show sample actions by severity
  const criticalActions = plan.actions.filter(
    (a) => a.issueType === "critical"
  );
  if (criticalActions.length > 0) {
    sections.push("### Sample Critical Actions (first 5)\n");
    for (const action of criticalActions.slice(0, 5)) {
      sections.push(`**${path.basename(action.file)}:${action.lineNumber}**`);
      sections.push(`- Problem: ${action.problem}`);
      sections.push(`- Current: \`${action.currentCode}\``);
      sections.push(`- Fix: ${action.fix.action} → \`${action.fix.newCode}\``);
      sections.push("");
    }
  }

  if (outputFile) {
    sections.push(`\n### JSON Output\n`);
    sections.push(
      `Fix plan saved to: \`${outputFile}\`\n`
    );
    sections.push("This file contains:");
    sections.push("- Complete list of all fix actions");
    sections.push("- Line numbers and current code");
    sections.push("- Exact fix instructions");
    sections.push("- Validation requirements");
    sections.push("- Backup and verification steps\n");
  }

  sections.push("## Next Steps\n");
  sections.push(
    "1. Review the generated JSON file for all fix actions"
  );
  sections.push(
    "2. Use the `apply_ui_fixes` tool to automatically apply fixes (with backups)"
  );
  sections.push("3. Verify all changes compile correctly");
  sections.push("4. Re-run `validate_ui_scaling` to confirm fixes");
  sections.push("5. Test application at different DPI settings\n");

  sections.push("## Safety Measures\n");
  sections.push("All fixes include:");
  sections.push("- ✅ Automatic backup creation before modification");
  sections.push("- ✅ Pre/post validation comparison");
  sections.push("- ✅ Corruption detection");
  sections.push("- ✅ Rollback capability");
  sections.push("- ✅ Detailed verification steps");

  return sections.join("\n");
}
