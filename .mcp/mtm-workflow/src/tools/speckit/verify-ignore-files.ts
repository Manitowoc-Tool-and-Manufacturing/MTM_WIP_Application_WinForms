import * as fs from "fs";
import * as path from "path";

interface IgnoreFileCheck {
  file: string;
  exists: boolean;
  hasCriticalPatterns: boolean;
  missingPatterns: string[];
  recommendations: string[];
}

const IGNORE_PATTERNS = {
  gitignore: {
    csharp: [
      "bin/",
      "obj/",
      "*.user",
      "*.suo",
      "*.cache",
      ".vs/",
      "packages/",
      "*.DotSettings.user",
    ],
    universal: [
      ".DS_Store",
      "Thumbs.db",
      "*.tmp",
      "*.swp",
      "*.log",
      ".vscode/",
      ".idea/",
    ],
  },
  dockerignore: [
    ".git/",
    ".gitignore",
    ".dockerignore",
    "Dockerfile*",
    "*.log",
    "*.md",
    ".env*",
    "node_modules/",
    "bin/",
    "obj/",
    ".vs/",
  ],
  eslintignore: [
    "node_modules/",
    "dist/",
    "build/",
    "coverage/",
    "*.min.js",
    "*.bundle.js",
  ],
  prettierignore: [
    "node_modules/",
    "dist/",
    "build/",
    "coverage/",
    "package-lock.json",
    "yarn.lock",
    "pnpm-lock.yaml",
    "*.min.js",
  ],
};

export async function verifyIgnoreFiles(args: {
  workspace_root: string;
  tech_stack?: string[];
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { workspace_root, tech_stack = [] } = args;

  if (!fs.existsSync(workspace_root)) {
    throw new Error(`Workspace root not found: ${workspace_root}`);
  }

  const results: IgnoreFileCheck[] = [];

  // Check .gitignore
  const gitignorePath = path.join(workspace_root, ".gitignore");
  const gitignoreCheck = checkIgnoreFile(
    gitignorePath,
    "gitignore",
    tech_stack
  );
  results.push(gitignoreCheck);

  // Check for Dockerfile
  const hasDockerfile = fs
    .readdirSync(workspace_root)
    .some((f) => f.startsWith("Dockerfile"));
  if (hasDockerfile) {
    const dockerignorePath = path.join(workspace_root, ".dockerignore");
    results.push(
      checkIgnoreFile(dockerignorePath, "dockerignore", tech_stack)
    );
  }

  // Check for ESLint config
  const hasEslint = fs
    .readdirSync(workspace_root)
    .some(
      (f) =>
        f.startsWith(".eslintrc") ||
        f === "eslint.config.js" ||
        f === "eslint.config.mjs"
    );
  if (hasEslint) {
    const eslintignorePath = path.join(workspace_root, ".eslintignore");
    results.push(
      checkIgnoreFile(eslintignorePath, "eslintignore", tech_stack)
    );
  }

  // Check for Prettier config
  const hasPrettier = fs
    .readdirSync(workspace_root)
    .some((f) => f.startsWith(".prettierrc") || f === "prettier.config.js");
  if (hasPrettier) {
    const prettierignorePath = path.join(workspace_root, ".prettierignore");
    results.push(
      checkIgnoreFile(prettierignorePath, "prettierignore", tech_stack)
    );
  }

  const summary = generateSummary(results);

  return {
    content: [
      {
        type: "text",
        text: summary,
      },
    ],
  };
}

function checkIgnoreFile(
  filePath: string,
  type: string,
  techStack: string[]
): IgnoreFileCheck {
  const exists = fs.existsSync(filePath);
  let content = "";
  const missingPatterns: string[] = [];
  const recommendations: string[] = [];

  if (exists) {
    content = fs.readFileSync(filePath, "utf-8");
  }

  // Determine required patterns based on type and tech stack
  let requiredPatterns: string[] = [];

  if (type === "gitignore") {
    requiredPatterns = [...IGNORE_PATTERNS.gitignore.universal];

    if (
      techStack.includes("csharp") ||
      techStack.includes("dotnet") ||
      fs.existsSync(path.join(path.dirname(filePath), "*.csproj"))
    ) {
      requiredPatterns.push(...IGNORE_PATTERNS.gitignore.csharp);
    }
  } else if (type in IGNORE_PATTERNS) {
    requiredPatterns = (IGNORE_PATTERNS as any)[type];
  }

  // Check for missing patterns
  for (const pattern of requiredPatterns) {
    const escapedPattern = pattern.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
    const regex = new RegExp(`^${escapedPattern}`, "m");

    if (!regex.test(content)) {
      missingPatterns.push(pattern);
    }
  }

  // Generate recommendations
  if (!exists) {
    recommendations.push(`Create ${path.basename(filePath)}`);
    recommendations.push(`Add ${requiredPatterns.length} essential patterns`);
  } else if (missingPatterns.length > 0) {
    recommendations.push(
      `Add ${missingPatterns.length} missing critical patterns`
    );
  } else {
    recommendations.push("✓ All critical patterns present");
  }

  return {
    file: path.basename(filePath),
    exists,
    hasCriticalPatterns: missingPatterns.length === 0,
    missingPatterns,
    recommendations,
  };
}

function generateSummary(results: IgnoreFileCheck[]): string {
  const allGood = results.every((r) => r.exists && r.hasCriticalPatterns);

  const table = [
    "| File | Exists | Status | Missing Patterns |",
    "|------|--------|--------|------------------|",
    ...results.map((r) => {
      const statusIcon = r.hasCriticalPatterns
        ? "✅ Complete"
        : "⚠️ Incomplete";
      return `| ${r.file} | ${r.exists ? "✅" : "❌"} | ${statusIcon} | ${
        r.missingPatterns.length
      } |`;
    }),
  ].join("\n");

  let summary = `
## Ignore Files Verification

${table}

**Overall Status**: ${allGood ? "✅ All ignore files are properly configured" : "⚠️ Some ignore files need attention"}

`;

  // Add detailed recommendations
  for (const result of results) {
    if (!result.exists || result.missingPatterns.length > 0) {
      summary += `\n### ${result.file}\n\n`;
      summary += result.recommendations.map((r) => `- ${r}`).join("\n");

      if (result.missingPatterns.length > 0) {
        summary += "\n\n**Missing Patterns:**\n```\n";
        summary += result.missingPatterns.join("\n");
        summary += "\n```\n";
      }
    }
  }

  return summary;
}
