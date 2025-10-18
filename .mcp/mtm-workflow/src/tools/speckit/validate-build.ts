import * as fs from "fs";
import * as path from "path";
import { execSync } from "child_process";

interface ValidationResult {
  check: string;
  status: "pass" | "fail" | "warn";
  message: string;
  details?: string[];
}

interface BuildValidation {
  overallStatus: "pass" | "fail";
  checks: ValidationResult[];
  buildOutput?: string;
  testOutput?: string;
  errorCount: number;
  warningCount: number;
}

export async function validateBuild(args: {
  workspace_root: string;
  project_file?: string;
  run_tests?: boolean;
  check_errors?: boolean;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const {
    workspace_root,
    project_file,
    run_tests = false,
    check_errors = true,
  } = args;

  if (!fs.existsSync(workspace_root)) {
    throw new Error(`Workspace root not found: ${workspace_root}`);
  }

  const checks: ValidationResult[] = [];
  let buildOutput = "";
  let testOutput = "";

  // Find project file if not specified
  let projectPath = project_file;
  if (!projectPath) {
    const csprojFiles = fs
      .readdirSync(workspace_root)
      .filter((f) => f.endsWith(".csproj"));

    if (csprojFiles.length === 0) {
      checks.push({
        check: "Project File",
        status: "fail",
        message: "No .csproj file found in workspace root",
      });
    } else if (csprojFiles.length === 1) {
      projectPath = path.join(workspace_root, csprojFiles[0]);
      checks.push({
        check: "Project File",
        status: "pass",
        message: `Found project: ${csprojFiles[0]}`,
      });
    } else {
      checks.push({
        check: "Project File",
        status: "warn",
        message: `Multiple projects found: ${csprojFiles.join(", ")}`,
        details: ["Specify project_file parameter to select one"],
      });
      projectPath = path.join(workspace_root, csprojFiles[0]);
    }
  } else {
    if (!fs.existsSync(projectPath)) {
      checks.push({
        check: "Project File",
        status: "fail",
        message: `Project file not found: ${projectPath}`,
      });
    } else {
      checks.push({
        check: "Project File",
        status: "pass",
        message: `Using project: ${path.basename(projectPath)}`,
      });
    }
  }

  // Run dotnet build
  if (projectPath && fs.existsSync(projectPath)) {
    try {
      buildOutput = execSync(
        `dotnet build "${projectPath}" -c Debug --no-incremental`,
        {
          cwd: workspace_root,
          encoding: "utf-8",
          maxBuffer: 10 * 1024 * 1024, // 10MB
        }
      );

      const buildFailed = buildOutput.includes("Build FAILED");
      const errorMatch = buildOutput.match(/(\d+)\s+Error\(s\)/);
      const warningMatch = buildOutput.match(/(\d+)\s+Warning\(s\)/);

      const errorCount = errorMatch ? parseInt(errorMatch[1]) : 0;
      const warningCount = warningMatch ? parseInt(warningMatch[1]) : 0;

      if (buildFailed || errorCount > 0) {
        checks.push({
          check: "Build",
          status: "fail",
          message: `Build failed with ${errorCount} error(s)`,
          details: extractErrors(buildOutput),
        });
      } else {
        checks.push({
          check: "Build",
          status: warningCount > 0 ? "warn" : "pass",
          message:
            warningCount > 0
              ? `Build succeeded with ${warningCount} warning(s)`
              : "Build succeeded",
          details: warningCount > 0 ? extractWarnings(buildOutput) : undefined,
        });
      }
    } catch (error: any) {
      buildOutput = error.stdout || error.message;
      checks.push({
        check: "Build",
        status: "fail",
        message: "Build command failed",
        details: [error.message],
      });
    }
  }

  // Check for compilation errors using VS Code diagnostics approach
  if (check_errors) {
    const errorFiles = findFilesWithErrors(workspace_root);
    if (errorFiles.length > 0) {
      checks.push({
        check: "Compilation Errors",
        status: "warn",
        message: `Found ${errorFiles.length} files with potential errors`,
        details: errorFiles.slice(0, 10),
      });
    } else {
      checks.push({
        check: "Compilation Errors",
        status: "pass",
        message: "No obvious compilation errors detected",
      });
    }
  }

  // Run tests if requested
  if (run_tests && projectPath && fs.existsSync(projectPath)) {
    try {
      testOutput = execSync(`dotnet test "${projectPath}" --no-build`, {
        cwd: workspace_root,
        encoding: "utf-8",
        maxBuffer: 10 * 1024 * 1024,
      });

      const testsFailed = testOutput.includes("Failed:");
      const passedMatch = testOutput.match(/Passed!\s+-\s+Failed:\s+(\d+)/);
      const failedCount = passedMatch ? parseInt(passedMatch[1]) : 0;

      if (testsFailed || failedCount > 0) {
        checks.push({
          check: "Tests",
          status: "fail",
          message: `Tests failed: ${failedCount} failure(s)`,
          details: extractTestFailures(testOutput),
        });
      } else {
        checks.push({
          check: "Tests",
          status: "pass",
          message: "All tests passed",
        });
      }
    } catch (error: any) {
      testOutput = error.stdout || error.message;
      checks.push({
        check: "Tests",
        status: "fail",
        message: "Test command failed",
        details: [error.message],
      });
    }
  }

  const failCount = checks.filter((c) => c.status === "fail").length;
  const warnCount = checks.filter((c) => c.status === "warn").length;

  const summary = `
## Build Validation Results

**Overall Status**: ${failCount > 0 ? "❌ FAIL" : warnCount > 0 ? "⚠️ WARNINGS" : "✅ PASS"}

### Validation Checks

${checks
  .map((c) => {
    const icon =
      c.status === "pass" ? "✅" : c.status === "warn" ? "⚠️" : "❌";
    let result = `${icon} **${c.check}**: ${c.message}`;

    if (c.details && c.details.length > 0) {
      result += "\n" + c.details.map((d) => `  - ${d}`).join("\n");
    }

    return result;
  })
  .join("\n\n")}

### Summary

- **Total Checks**: ${checks.length}
- **Passed**: ${checks.filter((c) => c.status === "pass").length}
- **Warnings**: ${warnCount}
- **Failed**: ${failCount}

${
  buildOutput && buildOutput.length < 5000
    ? `\n### Build Output (Last 100 lines)\n\`\`\`\n${buildOutput.split("\n").slice(-100).join("\n")}\n\`\`\`\n`
    : ""
}
`;

  return {
    content: [
      {
        type: "text",
        text: summary,
      },
    ],
  };
}

function extractErrors(output: string): string[] {
  const lines = output.split("\n");
  return lines
    .filter((line) => line.includes("error CS") || line.includes("error MSB"))
    .slice(0, 10)
    .map((line) => line.trim());
}

function extractWarnings(output: string): string[] {
  const lines = output.split("\n");
  return lines
    .filter((line) => line.includes("warning CS") || line.includes("warning MSB"))
    .slice(0, 10)
    .map((line) => line.trim());
}

function extractTestFailures(output: string): string[] {
  const lines = output.split("\n");
  const failures: string[] = [];

  for (let i = 0; i < lines.length; i++) {
    if (lines[i].includes("Failed") || lines[i].includes("Error Message:")) {
      failures.push(lines[i].trim());
      if (failures.length >= 5) break;
    }
  }

  return failures;
}

function findFilesWithErrors(workspaceRoot: string): string[] {
  // Simple heuristic: look for common error patterns in C# files
  const errorFiles: string[] = [];

  function scanDir(dir: string) {
    if (errorFiles.length >= 20) return; // Limit search

    const entries = fs.readdirSync(dir, { withFileTypes: true });

    for (const entry of entries) {
      if (errorFiles.length >= 20) break;

      const fullPath = path.join(dir, entry.name);

      // Skip common directories
      if (
        entry.isDirectory() &&
        !["bin", "obj", "node_modules", ".git", ".vs"].includes(entry.name)
      ) {
        scanDir(fullPath);
      } else if (entry.isFile() && entry.name.endsWith(".cs")) {
        try {
          const content = fs.readFileSync(fullPath, "utf-8");

          // Check for common error patterns
          if (
            content.includes("// ERROR:") ||
            content.includes("// FIXME:") ||
            content.match(/\bError\b.*\bCS\d{4}\b/)
          ) {
            errorFiles.push(path.relative(workspaceRoot, fullPath));
          }
        } catch {
          // Skip files that can't be read
        }
      }
    }
  }

  try {
    scanDir(workspaceRoot);
  } catch {
    // Ignore scan errors
  }

  return errorFiles;
}
