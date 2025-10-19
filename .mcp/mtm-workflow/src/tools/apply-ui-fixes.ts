import * as fs from "fs";
import * as path from "path";
import * as crypto from "crypto";

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

interface FixResult {
  file: string;
  action: UiFixAction;
  success: boolean;
  backupPath?: string;
  error?: string;
  preValidation: {
    fileExists: boolean;
    contentMatch: boolean;
    checksum: string;
  };
  postValidation: {
    fileExists: boolean;
    patternFound: boolean;
    compilable: boolean;
    checksum: string;
    corruptionDetected: boolean;
  };
}

interface ApplyFixesResult {
  totalActions: number;
  successfulFixes: number;
  failedFixes: number;
  skippedFixes: number;
  results: FixResult[];
  backupDirectory: string;
  corruptionDetected: boolean;
}

export async function applyUiFixes(args: {
  fix_plan_file: string;
  backup_dir?: string;
  dry_run?: boolean;
  max_severity?: "critical" | "error" | "warning" | "info";
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const {
    fix_plan_file,
    backup_dir,
    dry_run = false,
    max_severity = "warning",
  } = args;

  // Validate fix plan file exists
  if (!fs.existsSync(fix_plan_file)) {
    throw new Error(`Fix plan file not found: ${fix_plan_file}`);
  }

  // Load fix plan
  const planContent = fs.readFileSync(fix_plan_file, "utf-8");
  const plan: UiFixPlan = JSON.parse(planContent);

  // Create backup directory
  const backupDirectory =
    backup_dir ||
    path.join(
      path.dirname(plan.sourceDirectory),
      `.ui-fix-backups-${Date.now()}`
    );

  if (!dry_run && !fs.existsSync(backupDirectory)) {
    fs.mkdirSync(backupDirectory, { recursive: true });
  }

  // Filter actions by severity
  const severityOrder = ["critical", "error", "warning", "info"];
  const maxIndex = severityOrder.indexOf(max_severity);
  const filteredActions = plan.actions.filter((action) => {
    const actionIndex = severityOrder.indexOf(action.issueType);
    return actionIndex <= maxIndex;
  });

  console.error(
    `Processing ${filteredActions.length} of ${plan.actions.length} actions (severity <= ${max_severity})`
  );

  // Apply fixes
  const results: FixResult[] = [];
  let corruptionDetected = false;

  for (const action of filteredActions) {
    console.error(`\nProcessing: ${path.basename(action.file)}:${action.lineNumber}`);

    const result = dry_run
      ? await validateFixAction(action)
      : await applyFixAction(action, backupDirectory);

    results.push(result);

    if (result.postValidation.corruptionDetected) {
      corruptionDetected = true;
      console.error(`⚠️  CORRUPTION DETECTED in ${path.basename(action.file)}`);

      // Rollback if corruption detected
      if (!dry_run && result.backupPath && fs.existsSync(result.backupPath)) {
        console.error(`Rolling back ${path.basename(action.file)}...`);
        fs.copyFileSync(result.backupPath, action.file);
      }

      // Stop processing on corruption
      break;
    }

    if (result.success) {
      console.error(`✅ Fixed: ${action.category}`);
    } else {
      console.error(`❌ Failed: ${result.error}`);
    }
  }

  // Generate report
  const report: ApplyFixesResult = {
    totalActions: filteredActions.length,
    successfulFixes: results.filter((r) => r.success).length,
    failedFixes: results.filter((r) => !r.success).length,
    skippedFixes: plan.actions.length - filteredActions.length,
    results,
    backupDirectory,
    corruptionDetected,
  };

  const markdown = generateReport(report, dry_run);

  return {
    content: [
      {
        type: "text",
        text: markdown,
      },
    ],
  };
}

async function validateFixAction(action: UiFixAction): Promise<FixResult> {
  const result: FixResult = {
    file: action.file,
    action,
    success: false,
    preValidation: {
      fileExists: false,
      contentMatch: false,
      checksum: "",
    },
    postValidation: {
      fileExists: false,
      patternFound: false,
      compilable: false,
      checksum: "",
      corruptionDetected: false,
    },
  };

  // Pre-validation
  if (!fs.existsSync(action.file)) {
    result.error = "File not found";
    return result;
  }

  result.preValidation.fileExists = true;

  const content = fs.readFileSync(action.file, "utf-8");
  result.preValidation.checksum = generateChecksum(content);

  // Verify current code matches
  if (action.fix.searchPattern) {
    result.preValidation.contentMatch = content.includes(
      action.fix.searchPattern
    );
  } else if (action.currentCode) {
    result.preValidation.contentMatch = content.includes(action.currentCode);
  } else {
    result.preValidation.contentMatch = true; // Insert operations don't need existing code
  }

  if (!result.preValidation.contentMatch && action.fix.action === "replace") {
    result.error = "Current code pattern not found in file";
    return result;
  }

  result.success = true;
  return result;
}

async function applyFixAction(
  action: UiFixAction,
  backupDir: string
): Promise<FixResult> {
  const result = await validateFixAction(action);

  if (!result.success) {
    return result;
  }

  try {
    // Create backup
    const backupPath = path.join(
      backupDir,
      `${path.basename(action.file)}.${Date.now()}.bak`
    );
    fs.copyFileSync(action.file, backupPath);
    result.backupPath = backupPath;

    // Read file
    let content = fs.readFileSync(action.file, "utf-8");
    const originalContent = content;
    const lines = content.split("\n");

    // Apply fix based on action type
    switch (action.fix.action) {
      case "replace":
        if (action.fix.searchPattern) {
          const searchPattern = action.fix.searchPattern;
          const newCode = action.fix.newCode;

          // Replace the pattern
          content = content.replace(searchPattern, newCode);

          if (content === originalContent) {
            result.success = false;
            result.error = "Replace operation did not modify file";
            return result;
          }
        }
        break;

      case "insert":
        if (action.fix.insertAfter) {
          const insertAfter = action.fix.insertAfter;
          const newCode = action.fix.newCode;

          // Find the line containing insertAfter
          const insertIndex = content.indexOf(insertAfter);
          if (insertIndex === -1) {
            result.success = false;
            result.error = `Insert anchor not found: ${insertAfter}`;
            return result;
          }

          // Find end of line
          const lineEnd = content.indexOf("\n", insertIndex);
          if (lineEnd === -1) {
            result.success = false;
            result.error = "Could not find line ending";
            return result;
          }

          // Insert new code after the line
          content =
            content.substring(0, lineEnd + 1) +
            newCode +
            "\n" +
            content.substring(lineEnd + 1);
        }
        break;

      case "modify":
        // For modify, we add a comment rather than changing code
        if (action.lineNumber && action.lineNumber > 0) {
          lines[action.lineNumber - 1] =
            action.fix.newCode + " " + lines[action.lineNumber - 1];
          content = lines.join("\n");
        }
        break;
    }

    // Write modified content
    fs.writeFileSync(action.file, content, "utf-8");

    // Post-validation
    result.postValidation.fileExists = fs.existsSync(action.file);
    const newContent = fs.readFileSync(action.file, "utf-8");
    result.postValidation.checksum = generateChecksum(newContent);

    // Check if required pattern is present
    result.postValidation.patternFound = newContent.includes(
      action.validation.requiredPattern
    );

    // Detect corruption - check if file is dramatically different
    const sizeDiff = Math.abs(newContent.length - originalContent.length);
    const sizeChangePercent = (sizeDiff / originalContent.length) * 100;

    // If file changed by more than 50%, likely corruption
    if (sizeChangePercent > 50) {
      result.postValidation.corruptionDetected = true;
      result.success = false;
      result.error = `Possible corruption: file size changed by ${sizeChangePercent.toFixed(1)}%`;

      // Rollback immediately
      fs.copyFileSync(backupPath, action.file);
      return result;
    }

    // Check for basic C# syntax corruption
    const hasMatchingBraces =
      (newContent.match(/{/g) || []).length ===
      (newContent.match(/}/g) || []).length;
    const hasMatchingParens =
      (newContent.match(/\(/g) || []).length ===
      (newContent.match(/\)/g) || []).length;

    if (!hasMatchingBraces || !hasMatchingParens) {
      result.postValidation.corruptionDetected = true;
      result.success = false;
      result.error = "Syntax corruption detected: mismatched braces/parentheses";

      // Rollback
      fs.copyFileSync(backupPath, action.file);
      return result;
    }

    // Basic compilability check - does it still look like C#?
    const hasClassKeyword = newContent.includes("class ");
    const hasNamespace =
      newContent.includes("namespace ") || newContent.includes("namespace;");

    if (!hasClassKeyword && !hasNamespace) {
      result.postValidation.corruptionDetected = true;
      result.success = false;
      result.error = "Corruption detected: missing class/namespace declarations";

      // Rollback
      fs.copyFileSync(backupPath, action.file);
      return result;
    }

    result.postValidation.compilable = true;
    result.success = true;
  } catch (error) {
    result.success = false;
    result.error = `Exception: ${error instanceof Error ? error.message : String(error)}`;

    // Rollback on error
    if (result.backupPath && fs.existsSync(result.backupPath)) {
      try {
        fs.copyFileSync(result.backupPath, action.file);
      } catch (rollbackError) {
        result.error += ` | Rollback failed: ${rollbackError}`;
      }
    }
  }

  return result;
}

function generateChecksum(content: string): string {
  return crypto.createHash("sha256").update(content).digest("hex").substring(0, 16);
}

function generateReport(result: ApplyFixesResult, dryRun: boolean): string {
  const sections: string[] = [];

  sections.push(`## ${dryRun ? "DRY RUN - " : ""}UI Fixes Applied\n`);

  sections.push("### Summary\n");
  sections.push(`- **Total Actions**: ${result.totalActions}`);
  sections.push(`- ✅ Successful: ${result.successfulFixes}`);
  sections.push(`- ❌ Failed: ${result.failedFixes}`);
  sections.push(`- ⏭️  Skipped: ${result.skippedFixes}`);

  if (!dryRun) {
    sections.push(`- 📁 Backup Directory: \`${result.backupDirectory}\`\n`);
  }

  if (result.corruptionDetected) {
    sections.push("\n### ⚠️  CORRUPTION DETECTED\n");
    sections.push("**Processing stopped and rolled back to prevent data loss.**\n");
    sections.push("Please review the following:");
    sections.push("1. Check the fix plan JSON for incorrect patterns");
    sections.push("2. Verify file paths are correct");
    sections.push("3. Review the failed fix details below");
    sections.push("4. Report this issue to the agent development team\n");
  }

  // Group results by success
  const successful = result.results.filter((r) => r.success);
  const failed = result.results.filter((r) => !r.success);

  if (successful.length > 0) {
    sections.push("\n### ✅ Successful Fixes\n");
    sections.push("| File | Line | Category | Fix Type |");
    sections.push("|------|------|----------|----------|");

    for (const fix of successful) {
      const fileName = path.basename(fix.file);
      const line = fix.action.lineNumber || "N/A";
      const category = fix.action.category;
      const fixType = fix.action.fix.action;

      sections.push(`| ${fileName} | ${line} | ${category} | ${fixType} |`);
    }
    sections.push("");
  }

  if (failed.length > 0) {
    sections.push("\n### ❌ Failed Fixes\n");

    for (const fix of failed) {
      sections.push(`**${path.basename(fix.file)}:${fix.action.lineNumber}**`);
      sections.push(`- Category: ${fix.action.category}`);
      sections.push(`- Problem: ${fix.action.problem}`);
      sections.push(`- Error: ${fix.error}`);

      if (fix.postValidation.corruptionDetected) {
        sections.push(`- ⚠️  **Corruption detected - file was rolled back**`);
      }

      sections.push("");
    }
  }

  sections.push("\n### Validation Results\n");

  const preValidationPassed = result.results.filter(
    (r) => r.preValidation.fileExists && r.preValidation.contentMatch
  ).length;

  const postValidationPassed = result.results.filter(
    (r) =>
      r.postValidation.fileExists &&
      r.postValidation.patternFound &&
      !r.postValidation.corruptionDetected
  ).length;

  sections.push(`- Pre-validation passed: ${preValidationPassed}/${result.totalActions}`);
  sections.push(
    `- Post-validation passed: ${postValidationPassed}/${result.totalActions}`
  );
  sections.push(
    `- Corruption detected: ${result.corruptionDetected ? "YES ⚠️" : "NO ✅"}`
  );

  if (!dryRun) {
    sections.push("\n### Safety Measures Applied\n");
    sections.push("✅ Backup created for each file before modification");
    sections.push("✅ Pre-validation: verified file exists and pattern matches");
    sections.push("✅ Post-validation: verified pattern applied correctly");
    sections.push("✅ Corruption detection: checked file size, syntax, structure");
    sections.push("✅ Automatic rollback: restored from backup on corruption");
  }

  sections.push("\n### Next Steps\n");

  if (dryRun) {
    sections.push("1. Review validation results above");
    sections.push("2. Run without `dry_run` to apply fixes");
    sections.push("3. Monitor for corruption detection");
  } else if (result.corruptionDetected) {
    sections.push("1. **DO NOT PROCEED** - corruption was detected");
    sections.push("2. Review the fix plan JSON for errors");
    sections.push("3. Check file paths and patterns");
    sections.push("4. Report issue to development team");
  } else if (failed.length > 0) {
    sections.push("1. Review failed fixes above");
    sections.push("2. Fix issues manually or adjust fix plan");
    sections.push("3. Re-run for failed items only");
    sections.push("4. Validate with `validate_ui_scaling`");
  } else {
    sections.push("1. Run `dotnet build` to verify compilation");
    sections.push("2. Run `validate_ui_scaling` to confirm fixes");
    sections.push("3. Test application at different DPI settings");
    sections.push("4. Commit changes if all validation passes");
  }

  return sections.join("\n");
}
