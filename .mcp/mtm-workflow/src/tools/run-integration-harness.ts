import { spawn } from "child_process";
import * as fs from "fs";
import * as path from "path";

interface HarnessStep {
  name: string;
  command: string;
  cwd?: string;
  shell?: boolean;
  continueOnFailure?: boolean;
  env?: Record<string, string>;
}

interface IntegrationHarnessConfig {
  working_directory?: string;
  steps: HarnessStep[];
  fail_fast?: boolean;
}

interface RunIntegrationHarnessArgs {
  config_file: string;
}

export async function runIntegrationHarness(
  args: RunIntegrationHarnessArgs
): Promise<{ content: Array<{ type: "text"; text: string }>; isError?: boolean }> {
  if (!args.config_file) {
    throw new Error("config_file is required.");
  }

  const config = await loadHarnessConfig(args.config_file);
  if (!config.steps || config.steps.length === 0) {
    throw new Error("No steps defined in integration harness config.");
  }

  const workingDirectory = config.working_directory
    ? path.resolve(config.working_directory)
    : process.cwd();

  const lines: string[] = [];
  let overallSuccess = true;

  for (const step of config.steps) {
    const result = await executeStep(step, workingDirectory);
    lines.push(formatStepResult(result));

    if (!result.success) {
      overallSuccess = false;
      if (config.fail_fast !== false && step.continueOnFailure !== true) {
        lines.push(`Fail-fast enabled. Stopping after step '${step.name}'.`);
        break;
      }
    }
  }

  if (overallSuccess) {
    lines.unshift("✅ Integration harness completed successfully.");
  } else {
    lines.unshift("❌ Integration harness encountered failures.");
  }

  return {
    content: [
      {
        type: "text",
        text: lines.join("\n"),
      },
    ],
    isError: !overallSuccess,
  };
}

async function loadHarnessConfig(configFile: string): Promise<IntegrationHarnessConfig> {
  const resolved = path.resolve(configFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Config file not found: ${resolved}`);
  }
  const raw = fs.readFileSync(resolved, "utf-8");
  try {
    return JSON.parse(raw) as IntegrationHarnessConfig;
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse JSON config: ${message}`);
  }
}

async function executeStep(step: HarnessStep, baseWorkingDir: string) {
  const command = step.command;
  const cwd = step.cwd ? path.resolve(step.cwd) : baseWorkingDir;
  const shell = step.shell !== false; // default true for compatibility

  return new Promise<{
    success: boolean;
    name: string;
    stdout: string;
    stderr: string;
    exitCode: number | null;
  }>((resolve) => {
    const child = spawn(command, {
      cwd,
      shell,
      env: {
        ...process.env,
        ...(step.env ?? {}),
      },
    });

    let stdout = "";
    let stderr = "";

    child.stdout?.on("data", (data) => {
      stdout += data.toString();
    });

    child.stderr?.on("data", (data) => {
      stderr += data.toString();
    });

    child.on("close", (code) => {
      resolve({
        success: code === 0,
        name: step.name,
        stdout: stdout.trim(),
        stderr: stderr.trim(),
        exitCode: code,
      });
    });
  });
}

function formatStepResult(result: {
  success: boolean;
  name: string;
  stdout: string;
  stderr: string;
  exitCode: number | null;
}): string {
  const status = result.success ? "✅" : "❌";
  const lines = [`${status} ${result.name} (exit code ${result.exitCode ?? "null"})`];
  if (result.stdout) {
    lines.push("  stdout:", indent(result.stdout));
  }
  if (result.stderr) {
    lines.push("  stderr:", indent(result.stderr));
  }
  return lines.join("\n");
}

function indent(text: string): string {
  return text
    .split(/\r?\n/)
    .map((line) => `    ${line}`)
    .join("\n");
}
